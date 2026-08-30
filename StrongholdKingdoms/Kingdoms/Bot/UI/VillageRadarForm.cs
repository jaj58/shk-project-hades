using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CommonTypes;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    /// <summary>
    /// Village Radar - lists every attack, scout army and monk group inbound on one
    /// village, soonest landing first, with live countdowns.
    ///
    /// The world map knows an incoming army exists but not what is in it, so troop
    /// counts are pulled per army from the server (RetrieveAttackResult) when the window
    /// opens and again on Refresh. Everything else - landing times, journey lengths,
    /// source villages, monks - comes straight from the client's own world arrays and
    /// costs nothing.
    /// </summary>
    internal class VillageRadarForm : MyFormBase
    {
        private static readonly Color FormBg = Color.FromArgb(28, 30, 38);
        private static readonly Color StripBg = Color.FromArgb(36, 38, 50);
        private static readonly Color ListBg = Color.FromArgb(24, 24, 32);
        private static readonly Color ButtonBg = Color.FromArgb(50, 52, 64);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Color TextDim = Color.FromArgb(120, 124, 138);

        private static readonly Color TypeSevere = Color.FromArgb(240, 80, 80);    // raze / capture / ransack
        private static readonly Color TypeRaid = Color.FromArgb(255, 150, 60);     // vandalise / pillage / gold raid
        private static readonly Color TypeScout = Color.FromArgb(240, 210, 80);
        private static readonly Color TypeMonk = Color.FromArgb(185, 130, 240);
        private static readonly Color TypeOther = Color.FromArgb(150, 175, 220);

        private const int TitleBarHeight = 32;
        private const int FrameInset = 5;      // leaves MyFormBase's resize border clickable
        private const int HeaderHeight = 30;
        private const int FooterHeight = 22;

        private static VillageRadarForm _instance;
        private static Point _lastLocation = Point.Empty;
        private static bool _hasLastLocation;

        private int _villageId = -1;
        private Panel _body;
        private Panel _headerStrip;
        private Panel _listPanel;
        private Panel _footerStrip;
        private Button _refreshButton;
        private Label _updatedLabel;
        private Label _footerLabel;
        private Label _emptyLabel;
        private Timer _tickTimer;

        private readonly List<VillageRadarRow> _rows = new List<VillageRadarRow>();
        private readonly List<long> _outstanding = new List<long>();
        private DateTime _lastRefresh = DateTime.Now;

        /// <summary>
        /// Opens the radar on a village, or retargets the window if it is already up.
        /// One window at a time - the side panel only ever shows one village too.
        /// </summary>
        public static void ShowFor(int villageId)
        {
            if (villageId < 0) return;

            try
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new VillageRadarForm();

                _instance.Retarget(villageId);

                if (!_instance.Visible)
                {
                    Form parent = InterfaceMgr.Instance != null ? InterfaceMgr.Instance.ParentForm : null;
                    if (parent != null)
                        _instance.Show(parent);
                    else
                        _instance.Show();
                }

                _instance.BringToFront();
            }
            catch (Exception ex)
            {
                UniversalDebugLog.Log("VillageRadarForm.ShowFor failed: " + ex.Message);
            }
        }

        /// <summary>
        /// Closes the window if it is open. Called on a world reinit, where every village
        /// id on screen is about to become meaningless.
        /// </summary>
        public static void CloseInstance()
        {
            if (_instance == null || _instance.IsDisposed) return;
            try { _instance.Close(); }
            catch (Exception) { }
            _instance = null;
        }

        private VillageRadarForm()
        {
            this.ShowClose = true;
            this.Resizable = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.DoubleBuffered = true;
            this.BackColor = FormBg;
            this.MinimumSize = new Size(460, 220);
            this.ClientSize = new Size(VillageRadarRow.DesignWidth + (FrameInset * 2), 500);
            this.setGradient(FormBg, Color.FromArgb(20, 20, 28));

            if (_hasLastLocation)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = _lastLocation;
            }

            BuildUI();

            _tickTimer = new Timer();
            _tickTimer.Interval = 1000;
            _tickTimer.Tick += new EventHandler(this.TickTimerTick);
            _tickTimer.Start();
        }

        private void BuildUI()
        {
            this.SuspendLayout();

            _body = new Panel();
            _body.BackColor = FormBg;
            _body.Location = new Point(FrameInset, TitleBarHeight);
            _body.Size = new Size(this.ClientSize.Width - (FrameInset * 2),
                                  this.ClientSize.Height - TitleBarHeight - FrameInset);
            _body.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(_body);

            _footerStrip = new Panel();
            _footerStrip.Dock = DockStyle.Bottom;
            _footerStrip.Height = FooterHeight;
            _footerStrip.BackColor = StripBg;

            _footerLabel = new Label();
            _footerLabel.AutoSize = false;
            _footerLabel.Dock = DockStyle.Fill;
            _footerLabel.Font = new Font("Segoe UI", 7.5f);
            _footerLabel.ForeColor = TextDim;
            _footerLabel.TextAlign = ContentAlignment.MiddleLeft;
            _footerLabel.Padding = new Padding(8, 0, 8, 0);
            _footerStrip.Controls.Add(_footerLabel);
            _body.Controls.Add(_footerStrip);

            _headerStrip = new Panel();
            _headerStrip.Dock = DockStyle.Top;
            _headerStrip.Height = HeaderHeight;
            _headerStrip.BackColor = StripBg;

            _refreshButton = new Button();
            _refreshButton.Text = "Refresh";
            _refreshButton.Font = new Font("Segoe UI", 8f);
            _refreshButton.FlatStyle = FlatStyle.Flat;
            _refreshButton.FlatAppearance.BorderColor = Color.FromArgb(70, 74, 90);
            _refreshButton.BackColor = ButtonBg;
            _refreshButton.ForeColor = TextPri;
            _refreshButton.Location = new Point(6, 4);
            _refreshButton.Size = new Size(72, 22);
            _refreshButton.Click += new EventHandler(this.RefreshClick);
            _headerStrip.Controls.Add(_refreshButton);

            _updatedLabel = new Label();
            _updatedLabel.AutoSize = false;
            _updatedLabel.Font = new Font("Segoe UI", 7.5f);
            _updatedLabel.ForeColor = TextSec;
            _updatedLabel.TextAlign = ContentAlignment.MiddleLeft;
            _updatedLabel.Location = new Point(86, 4);
            _updatedLabel.Size = new Size(300, 22);
            _updatedLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _headerStrip.Controls.Add(_updatedLabel);
            _body.Controls.Add(_headerStrip);

            _listPanel = new Panel();
            _listPanel.Dock = DockStyle.Fill;
            _listPanel.BackColor = ListBg;
            _listPanel.AutoScroll = true;

            _emptyLabel = new Label();
            _emptyLabel.AutoSize = false;
            _emptyLabel.Dock = DockStyle.Top;
            _emptyLabel.Height = 60;
            _emptyLabel.Font = new Font("Segoe UI", 9f);
            _emptyLabel.ForeColor = TextDim;
            _emptyLabel.TextAlign = ContentAlignment.MiddleCenter;
            _emptyLabel.Text = "No incoming armies, scouts or monks.";
            _emptyLabel.Visible = false;
            _listPanel.Controls.Add(_emptyLabel);

            // Docking is applied from the back of the z-order forwards, so the Fill panel
            // has to sit in front of the two strips or it would claim the whole body and
            // leave them overlapping it.
            _body.Controls.Add(_listPanel);
            _listPanel.BringToFront();

            this.ResumeLayout(false);
        }

        private void Retarget(int villageId)
        {
            _villageId = villageId;
            this.Title = "Incoming Attacks - " + DescribeVillage(villageId);
            Rebuild(true);
        }

        // =================================================================
        // Building the list
        // =================================================================

        private void Rebuild(bool fetchTroopCounts)
        {
            CancelOutstanding();

            List<VillageRadarEntry> entries = Collect();
            entries.Sort(new Comparison<VillageRadarEntry>(CompareByLanding));

            _listPanel.SuspendLayout();

            for (int i = 0; i < _rows.Count; i++)
            {
                _listPanel.Controls.Remove(_rows[i]);
                _rows[i].Dispose();
            }
            _rows.Clear();

            // Dock=Top stacks in reverse add order, so walk the sorted list backwards to
            // get soonest-landing at the top.
            for (int i = entries.Count - 1; i >= 0; i--)
            {
                VillageRadarEntry entry = entries[i];
                VillageRadarRow row = new VillageRadarRow(entry, DescribeSource(entry.SourceVillageId), i % 2 == 1);
                row.Dock = DockStyle.Top;
                _listPanel.Controls.Add(row);
                _rows.Insert(0, row);
            }

            _emptyLabel.Visible = entries.Count == 0;
            if (_emptyLabel.Visible)
                _emptyLabel.BringToFront();

            _listPanel.ResumeLayout(true);

            _lastRefresh = DateTime.Now;
            UpdateFooter();
            UpdateHeader();

            if (fetchTroopCounts)
                RequestTroopCounts();
        }

        private static int CompareByLanding(VillageRadarEntry a, VillageRadarEntry b)
        {
            return a.LandsAt.CompareTo(b.LandsAt);
        }

        private List<VillageRadarEntry> Collect()
        {
            List<VillageRadarEntry> list = new List<VillageRadarEntry>();
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return list;

            WorldMap world = GameEngine.Instance.World;
            DateTime now = VillageMap.getCurrentServerTime();

            try
            {
                SparseArray armies = world.getArmyArray();
                if (armies != null)
                {
                    foreach (WorldMap.LocalArmyData army in armies)
                    {
                        if (army == null) continue;
                        if (army.targetVillageID != _villageId) continue;
                        if (!IsHostileIncoming(army)) continue;
                        if ((army.serverEndTime - now).TotalSeconds <= 1.0) continue;
                        list.Add(BuildArmyEntry(army));
                    }
                }
            }
            catch (Exception ex)
            {
                UniversalDebugLog.Log("VillageRadarForm: army scan failed: " + ex.Message);
            }

            try
            {
                SparseArray people = world.getPeopleArray();
                if (people != null)
                {
                    foreach (WorldMap.LocalPerson person in people)
                    {
                        if (person == null || person.person == null) continue;
                        if (person.person.personType != 4) continue;      // monks only
                        if (person.person.state <= 0) continue;           // not travelling
                        if (person.parentPerson >= 0L) continue;          // follower of a group leader
                        if (person.person.targetVillageID != _villageId) continue;
                        if ((person.serverEndTime - now).TotalSeconds <= 1.0) continue;
                        list.Add(BuildMonkEntry(person));
                    }
                }
            }
            catch (Exception ex)
            {
                UniversalDebugLog.Log("VillageRadarForm: monk scan failed: " + ex.Message);
            }

            return list;
        }

        // Mirrors WorldMap.countIncomingAttacks: outbound only (lootType < 0), and not
        // one of the friendly army types that happen to travel the same way.
        private static bool IsHostileIncoming(WorldMap.LocalArmyData army)
        {
            if (army.lootType >= 0) return false;                 // returning home with loot
            if (army.reinforcements) return false;
            if (army.attackType == 13) return false;              // reinforcement packet
            if (army.attackType == 30 || army.attackType == 31) return false;  // vassal / capital support
            return true;
        }

        private VillageRadarEntry BuildArmyEntry(WorldMap.LocalArmyData army)
        {
            VillageRadarEntry entry = new VillageRadarEntry();
            entry.IsMonk = false;
            entry.Id = army.armyID;
            entry.SourceVillageId = army.travelFromVillageID;
            entry.TargetVillageId = army.targetVillageID;
            entry.LandsAt = army.serverEndTime;
            entry.Travel = army.serverEndTime - army.serverStartTime;

            entry.Troops[0] = army.numPeasants;
            entry.Troops[1] = army.numArchers;
            entry.Troops[2] = army.numPikemen;
            entry.Troops[3] = army.numSwordsmen;
            entry.Troops[4] = army.numCatapults;
            entry.Troops[5] = army.numCaptains;
            entry.Troops[6] = army.numScouts;
            entry.CountsConfirmed = false;

            bool scoutsOnly = army.numScouts > 0 && army.numPeasants == 0 && army.numArchers == 0 &&
                              army.numPikemen == 0 && army.numSwordsmen == 0 && army.numCatapults == 0;

            if (scoutsOnly)
            {
                entry.TypeLabel = "SCOUT";
                entry.TypeColor = TypeScout;
            }
            else
            {
                string name = SelectArmyPanel2.GetAttackTypeName(army.attackType);
                entry.TypeLabel = string.IsNullOrEmpty(name) ? "ATTACK" : name.ToUpper();
                entry.TypeColor = AttackTypeColor(army.attackType);
            }

            if (IsAISource(army.travelFromVillageID))
                entry.TypeLabel = "AI " + entry.TypeLabel;

            return entry;
        }

        private VillageRadarEntry BuildMonkEntry(WorldMap.LocalPerson person)
        {
            VillageRadarEntry entry = new VillageRadarEntry();
            entry.IsMonk = true;
            entry.Id = person.personID;
            entry.SourceVillageId = person.person.homeVillageID;
            entry.TargetVillageId = person.person.targetVillageID;
            entry.LandsAt = person.serverEndTime;
            entry.Travel = TimeSpan.Zero;   // people carry no start time on the client
            entry.MonkCount = person.childrenCount + 1;
            entry.CountsConfirmed = true;
            entry.TypeColor = TypeMonk;

            string command = RadarModule.GetMonkCommandLabel(person.person.command);
            entry.TypeLabel = command != null ? command.ToUpper() : "MONK";

            return entry;
        }

        private static Color AttackTypeColor(int attackType)
        {
            switch (attackType)
            {
                case 1:   // capture
                case 3:   // ransack
                case 9:   // raze
                case 17:  // invasion
                    return TypeSevere;
                case 2: case 4: case 5: case 6: case 7:   // pillage variants
                case 11:  // vandalise
                case 12:  // gold raid
                    return TypeRaid;
                default:
                    return TypeOther;
            }
        }

        private static bool IsAISource(int villageId)
        {
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.World == null) return false;
                return GameEngine.Instance.World.getSpecial(villageId) > 0;
            }
            catch (Exception) { return false; }
        }

        // =================================================================
        // Troop counts from the server
        // =================================================================

        private void RequestTroopCounts()
        {
            for (int i = 0; i < _rows.Count; i++)
            {
                VillageRadarEntry entry = _rows[i].Entry;
                if (entry.IsMonk) continue;

                long armyId = entry.Id;
                _outstanding.Add(armyId);
                AttackResultRouter.Request(armyId,
                    delegate(RetrieveAttackResult_ReturnType data) { OnTroopCounts(armyId, data); });
            }
        }

        // Runs on the UI thread - RemoteServices dispatches replies from the main loop.
        private void OnTroopCounts(long armyId, RetrieveAttackResult_ReturnType data)
        {
            if (this.IsDisposed) return;
            _outstanding.Remove(armyId);

            if (data == null || !data.Success || data.armyData == null) return;
            if (data.armyData.armyID != armyId) return;

            for (int i = 0; i < _rows.Count; i++)
            {
                VillageRadarRow row = _rows[i];
                if (row.Entry.Id != armyId || row.Entry.IsMonk) continue;

                VillageRadarEntry entry = row.Entry;
                entry.Troops[0] = data.armyData.numPeasants;
                entry.Troops[1] = data.armyData.numArchers;
                entry.Troops[2] = data.armyData.numPikemen;
                entry.Troops[3] = data.armyData.numSwordsmen;
                entry.Troops[4] = data.armyData.numCatapults;
                entry.Troops[5] = data.armyData.numCaptains;
                entry.CountsConfirmed = true;
                row.RefreshTroops();
                break;
            }

            UpdateFooter();
        }

        private void CancelOutstanding()
        {
            for (int i = 0; i < _outstanding.Count; i++)
                AttackResultRouter.Cancel(_outstanding[i]);
            _outstanding.Clear();
        }

        // =================================================================
        // Live countdown
        // =================================================================

        private void TickTimerTick(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;

            try
            {
                DateTime now = VillageMap.getCurrentServerTime();
                List<VillageRadarRow> landed = null;

                for (int i = 0; i < _rows.Count; i++)
                {
                    VillageRadarRow row = _rows[i];
                    TimeSpan remaining = row.Entry.LandsAt - now;

                    if (remaining.TotalSeconds <= 0 || !StillInFlight(row.Entry))
                    {
                        if (landed == null) landed = new List<VillageRadarRow>();
                        landed.Add(row);
                        continue;
                    }

                    row.UpdateCountdown(remaining);
                }

                if (landed != null)
                {
                    _listPanel.SuspendLayout();
                    for (int i = 0; i < landed.Count; i++)
                    {
                        _rows.Remove(landed[i]);
                        _listPanel.Controls.Remove(landed[i]);
                        landed[i].Dispose();
                    }
                    _emptyLabel.Visible = _rows.Count == 0;
                    if (_emptyLabel.Visible)
                        _emptyLabel.BringToFront();
                    _listPanel.ResumeLayout(true);
                    UpdateFooter();
                }

                UpdateHeader();
            }
            catch (Exception ex)
            {
                UniversalDebugLog.Log("VillageRadarForm tick failed: " + ex.Message);
            }
        }

        // An army or monk that vanished from the world arrays has landed, been recalled
        // or been killed - either way it is no longer inbound.
        private bool StillInFlight(VillageRadarEntry entry)
        {
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.World == null) return true;
                SparseArray array = entry.IsMonk
                    ? GameEngine.Instance.World.getPeopleArray()
                    : GameEngine.Instance.World.getArmyArray();
                if (array == null) return true;
                return array[entry.Id] != null;
            }
            catch (Exception) { return true; }
        }

        // =================================================================
        // Chrome
        // =================================================================

        private void RefreshClick(object sender, EventArgs e)
        {
            Rebuild(true);
        }

        private void UpdateHeader()
        {
            int age = (int)(DateTime.Now - _lastRefresh).TotalSeconds;
            if (age < 0) age = 0;
            _updatedLabel.Text = age < 2 ? "Updated just now" : "Updated " + age + "s ago";
        }

        private void UpdateFooter()
        {
            int armies = 0;
            int monks = 0;
            int pending = 0;

            for (int i = 0; i < _rows.Count; i++)
            {
                if (_rows[i].Entry.IsMonk) monks++;
                else
                {
                    armies++;
                    if (!_rows[i].Entry.CountsConfirmed) pending++;
                }
            }

            if (armies == 0 && monks == 0)
            {
                _footerLabel.Text = "Nothing incoming.";
                return;
            }

            string text = armies + (armies == 1 ? " army" : " armies");
            if (monks > 0) text += ", " + monks + (monks == 1 ? " monk group" : " monk groups");
            text += pending > 0
                ? "  -  fetching troop counts for " + pending + " from the server"
                : "  -  troop counts fetched from the server";

            _footerLabel.Text = text;
        }

        private static string DescribeVillage(int villageId)
        {
            string name = "";
            try { name = GameEngine.Instance.World.getVillageName(villageId); }
            catch (Exception) { }

            // getVillageName already prefixes "[id] " when View Village IDs is on.
            if (string.IsNullOrEmpty(name)) return "[" + villageId + "]";
            if (name.StartsWith("[")) return name;
            return "[" + villageId + "] " + name;
        }

        // Village name plus the owner's name when the client happens to have it cached
        // (it only does for villages rolled over recently). Never fires an RPC of its
        // own - one lookup per source village would be a burst for no real gain.
        private static string DescribeSource(int villageId)
        {
            string text = DescribeVillage(villageId);

            try
            {
                WorldMap.VillageRolloverInfo villageInfo = null;
                WorldMap.CachedUserInfo userInfo = null;
                GameEngine.Instance.World.retrieveUserData(villageId, -1, ref villageInfo, ref userInfo, false, false);
                if (userInfo != null && !string.IsNullOrEmpty(userInfo.userName))
                    text += "  -  " + userInfo.userName;
            }
            catch (Exception) { }

            return text;
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            if (this.Visible && this.WindowState == FormWindowState.Normal)
            {
                _lastLocation = this.Location;
                _hasLastLocation = true;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_tickTimer != null)
            {
                _tickTimer.Stop();
                _tickTimer.Dispose();
                _tickTimer = null;
            }

            CancelOutstanding();

            if (_instance == this)
                _instance = null;

            base.OnFormClosed(e);
        }
    }
}
