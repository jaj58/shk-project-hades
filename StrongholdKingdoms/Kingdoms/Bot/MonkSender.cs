using System;
using System.Threading;
using CommonTypes;

namespace Kingdoms.Bot
{
    public enum MonkSendResult
    {
        Success,
        Failed,       // server answered with an error
        NoResponse,   // no reply within the timeout — the send may or may not have gone out
    }

    /// <summary>
    /// Monk plumbing shared by the background monk threads (Monk routes, InterdictRunner).
    ///
    /// Everything that touches game state or fires an RPC is marshaled onto the game
    /// thread through <see cref="InterdictRunner.UiInvoke"/>; the worker thread only
    /// waits. Firing RPCs from a worker races RemoteServices.processData's enumeration
    /// of its result list, and enumerating WorldMap's person array from a worker races
    /// the game loop adding/removing people.
    ///
    /// Sends are single-flight and confirmed: SendPeople reports through one global
    /// callback slot, so only one send may be outstanding, and the worker blocks until
    /// the server's reply lands (which imports the sent monks as in-transit people).
    /// </summary>
    public static class MonkSender
    {
        private const int GameThreadTimeoutMs = 5000;

        private static readonly object _sendLock = new object();
        private static readonly AutoResetEvent _sendDone = new AutoResetEvent(false);
        private static volatile int _expectedTarget = -1;
        private static volatile bool _lastSuccess;
        private static volatile string _lastErr = "";

        /// <summary>
        /// Sends <paramref name="count"/> monks and blocks until the server confirms or
        /// rejects the send, or <paramref name="timeoutMs"/> passes.
        /// </summary>
        public static MonkSendResult SendAndWait(int fromVillageId, int targetId, int count, int command,
                                                 int timeoutMs, out string error)
        {
            lock (_sendLock)
            {
                _sendDone.WaitOne(0);              // drain any stale signal
                _expectedTarget = targetId;
                _lastSuccess = false;
                _lastErr = "no response";

                RunOnGameThread(delegate
                {
                    RemoteServices.Instance.set_SendPeople_UserCallBack(
                        new RemoteServices.SendPeople_UserCallBack(OnSendPeople));
                    RemoteServices.Instance.SendPeople(fromVillageId, targetId, 4, count, command, -1);
                });

                bool answered = _sendDone.WaitOne(timeoutMs);
                _expectedTarget = -1;

                if (!answered)
                {
                    error = "no response within " + (timeoutMs / 1000) + "s";
                    return MonkSendResult.NoResponse;
                }
                error = _lastSuccess ? "" : _lastErr;
                return _lastSuccess ? MonkSendResult.Success : MonkSendResult.Failed;
            }
        }

        // Game thread. Imports people for every successful reply, including sends we
        // didn't make (Attacker module, a late reply after a timeout), since this
        // handler may be the one sitting in the global slot when they arrive.
        private static void OnSendPeople(SendPeople_ReturnType result)
        {
            bool mine = result != null && result.targetVillageID == _expectedTarget;
            try
            {
                if (result != null && result.Success)
                {
                    GameEngine.Instance.World.importOrphanedPeople(result.people, result.currentTime, -2);
                    GameEngine.Instance.World.setFaithPointsData(
                        result.currentFaithPointsLevel, result.currentFaithPointsRate);
                    if (mine) _lastSuccess = true;
                }
                else if (mine)
                {
                    _lastSuccess = false;
                    _lastErr = ErrorCodes.getErrorString(result.m_errorCode, result.m_errorID);
                }
            }
            catch (Exception ex)
            {
                if (mine)
                {
                    _lastSuccess = false;
                    _lastErr = ex.Message;
                }
            }
            finally
            {
                if (mine) _sendDone.Set();
            }
        }

        /// <summary>
        /// Hires up to <paramref name="more"/> monks and blocks until they show up in the
        /// village's people list (the hire is async). Returns how many actually arrived.
        /// The village must be loaded; returns 0 otherwise.
        /// </summary>
        public static int RecruitAndWait(int villageId, int more, int timeoutMs, Func<bool> cancelled)
        {
            if (more <= 0) return 0;

            int totalBefore, atHomeBefore;
            if (!CountMonks(villageId, out totalBefore, out atHomeBefore)) return 0;

            int made = OnGameThread<int>(delegate { return InterdictRunner.MakeMonksMore(villageId, more); }, 0);
            if (made <= 0) return 0;

            int waited = 0;
            int total = totalBefore, atHome;
            while (waited < timeoutMs)
            {
                if (cancelled != null && cancelled()) break;
                if (CountMonks(villageId, out total, out atHome) && total >= totalBefore + made) break;
                Thread.Sleep(250);
                waited += 250;
            }
            return Math.Max(0, total - totalBefore);
        }

        /// <summary>Total and at-home monk counts for one of the player's villages.</summary>
        public static bool CountMonks(int villageId, out int total, out int atHome)
        {
            int[] r = OnGameThread<int[]>(delegate
            {
                int home = 0;
                int all = GameEngine.Instance.World.countVillagePeople(villageId, 4, ref home);
                return new int[] { all, home };
            }, null);

            total = r != null ? r[0] : 0;
            atHome = r != null ? r[1] : 0;
            return r != null;
        }

        /// <summary>
        /// Monks currently travelling to <paramref name="targetId"/> with
        /// <paramref name="command"/>. -1 if the people list couldn't be read.
        /// </summary>
        public static int CountInTransit(int targetId, int command)
        {
            return OnGameThread<int>(delegate
            {
                int n = 0;
                DateTime now = VillageMap.getCurrentServerTime();
                foreach (WorldMap.LocalPerson p in GameEngine.Instance.World.getPeopleArray())
                {
                    if (p.person.personType == 4 && p.person.state > 0
                        && p.person.targetVillageID == targetId
                        && p.person.command == command
                        && p.serverEndTime > now)
                        n++;
                }
                return n;
            }, -1);
        }

        /// <summary>
        /// Re-downloads the target's rollover info (plague level, interdiction and
        /// excommunication times) — the same request the game makes when you hover the
        /// village on the map. Without it those values are whatever was last seen, or
        /// nothing at all for a parish the player has never looked at.
        /// Blocks until the reply has been applied; false on timeout or server error.
        /// </summary>
        public static bool RefreshTargetInfo(int villageId, int timeoutMs, Func<bool> cancelled)
        {
            VillageData vd = GameEngine.Instance.World.getVillageData(villageId);
            if (vd == null) return false;

            int userId = vd.userID;
            DateTime requestedAt = DateTime.Now;

            RunOnGameThread(delegate
            {
                RemoteServices.Instance.set_RetrieveVillageUserInfo_UserCallBack(
                    new RemoteServices.RetrieveVillageUserInfo_UserCallBack(
                        GameEngine.Instance.World.villageUserInfoCallback));
                RemoteServices.Instance.RetrieveVillageUserInfo(villageId, userId, false);
            });

            // The game's own callback stamps a fresh rolloverInfo; poll for it rather than
            // owning the callback, since map hovers re-register that same slot anyway.
            int waited = 0;
            while (waited < timeoutMs)
            {
                if (cancelled != null && cancelled()) return false;
                vd = GameEngine.Instance.World.getVillageData(villageId);
                if (vd != null && vd.rolloverInfo != null && vd.rolloverInfo.lastUpdateTime >= requestedAt)
                    return true;
                Thread.Sleep(200);
                waited += 200;
            }
            return false;
        }

        /// <summary>Fire-and-forget onto the game thread (direct call if the bot form isn't open).</summary>
        public static void RunOnGameThread(Action action)
        {
            Action safe = delegate
            {
                try { action(); }
                catch (Exception ex) { BotLogger.Log("Monk", BotLogLevel.Warning, "Game-thread call failed: " + ex.Message); }
            };
            Action<Action> inv = InterdictRunner.UiInvoke;
            if (inv != null) inv(safe);
            else safe();
        }

        /// <summary>
        /// Runs <paramref name="fn"/> on the game thread and returns its result, or
        /// <paramref name="fallback"/> if it throws or the game thread doesn't get to it in time.
        /// </summary>
        public static T OnGameThread<T>(Func<T> fn, T fallback)
        {
            Action<Action> inv = InterdictRunner.UiInvoke;
            if (inv == null)
            {
                try { return fn(); }
                catch { return fallback; }
            }

            T result = fallback;
            // Not disposed: after a timeout the queued delegate can still run and Set() it.
            ManualResetEvent done = new ManualResetEvent(false);
            inv(delegate
            {
                try { result = fn(); }
                catch { }
                finally { done.Set(); }
            });
            return done.WaitOne(GameThreadTimeoutMs) ? result : fallback;
        }
    }
}
