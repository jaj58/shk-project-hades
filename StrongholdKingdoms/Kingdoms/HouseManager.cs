// Decompiled with JetBrains decompiler
// Type: Kingdoms.HouseManager
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;

//#nullable disable
namespace Kingdoms
{
  public class HouseManager
  {
    private HouseManager.HouseInfoUpdatedCallback OnUpdate;
    private RemoteServices.SelfJoinHouse_UserCallBack joinHouseCallback;

    public void UpdateGloryPoints(HouseManager.HouseInfoUpdatedCallback callback)
    {
      this.UpdateGloryPoints(callback, false);
    }

    public void UpdateGloryPoints(HouseManager.HouseInfoUpdatedCallback callback, bool ignoreTest)
    {
      this.OnUpdate = callback;
      if (!ignoreTest && !GameEngine.Instance.World.testGloryPointsUpdate())
        return;
      RemoteServices.Instance.set_GetHouseGloryPoints_UserCallBack(new RemoteServices.GetHouseGloryPoints_UserCallBack(this.GetHouseGloryPointsCallBack));
      RemoteServices.Instance.GetHouseGloryPoints();
    }

    private void GetHouseGloryPointsCallBack(GetHouseGloryPoints_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      GameEngine.Instance.World.HouseGloryPoints = returnData.gloryPoints;
      GameEngine.Instance.World.HouseGloryRoundData = returnData.gloryRoundData;
      if (this.OnUpdate == null)
        return;
      this.OnUpdate();
    }

    public void selfJoinHouse(RemoteServices.SelfJoinHouse_UserCallBack callback, int houseID)
    {
      this.joinHouseCallback = callback;
      RemoteServices.Instance.set_SelfJoinHouse_UserCallBack(new RemoteServices.SelfJoinHouse_UserCallBack(this.selfJoinHouseCallback));
      RemoteServices.Instance.SelfJoinHouse(RemoteServices.Instance.UserFactionID, houseID, GameEngine.Instance.World.StoredFactionChangesPos);
    }

    public void selfJoinHouseCallback(SelfJoinHouse_ReturnType returnData)
    {
      if (returnData.Success)
      {
        if (returnData.factionsList != null)
          GameEngine.Instance.World.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
        GameEngine.Instance.World.HouseInfo = returnData.m_houseData;
        GameEngine.Instance.World.YourFaction = returnData.yourFaction;
        GameEngine.Instance.World.HouseVoteInfo = returnData.m_houseVoteData;
      }
      if (this.joinHouseCallback == null)
        return;
      this.joinHouseCallback(returnData);
    }

    public void LeaveHouse(int houseID, HouseManager.HouseInfoUpdatedCallback callback)
    {
      RemoteServices.Instance.set_LeaveHouse_UserCallBack(new RemoteServices.LeaveHouse_UserCallBack(this.leaveHouseCallback));
      RemoteServices.Instance.LeaveHouse(RemoteServices.Instance.UserFactionID, houseID, GameEngine.Instance.World.StoredFactionChangesPos);
      this.OnUpdate = callback;
    }

    private void leaveHouseCallback(LeaveHouse_ReturnType returnData)
    {
      if (returnData.Success)
      {
        if (returnData.factionsList != null)
          GameEngine.Instance.World.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
        GameEngine.Instance.World.HouseInfo = returnData.m_houseData;
        GameEngine.Instance.World.HouseVoteInfo = returnData.m_houseVoteData;
        GameEngine.Instance.World.YourFaction = returnData.yourFaction;
      }
      if (this.OnUpdate == null)
        return;
      this.OnUpdate();
    }

    public void houseVote(
      int targetFaction,
      bool application,
      bool vote,
      HouseManager.HouseInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      RemoteServices.Instance.set_HouseVote_UserCallBack(new RemoteServices.HouseVote_UserCallBack(this.houseVoteCallback));
      RemoteServices.Instance.HouseVote(targetFaction, application, vote, GameEngine.Instance.World.StoredFactionChangesPos);
    }

    private void houseVoteCallback(HouseVote_ReturnType returnData)
    {
      if (returnData.Success)
      {
        if (returnData.factionsList != null)
          GameEngine.Instance.World.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
        GameEngine.Instance.World.HouseInfo = returnData.m_houseData;
        GameEngine.Instance.World.YourFaction = returnData.yourFaction;
        GameEngine.Instance.World.HouseVoteInfo = returnData.m_houseVoteData;
      }
      if (this.OnUpdate == null)
        return;
      this.OnUpdate();
    }

    public void houseVoteHouseLeader(
      int targetFaction,
      HouseManager.HouseInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      int houseID = 0;
      FactionData yourFaction = GameEngine.Instance.World.YourFaction;
      if (yourFaction != null)
        houseID = yourFaction.houseID;
      RemoteServices.Instance.set_HouseVoteHouseLeader_UserCallBack(new RemoteServices.HouseVoteHouseLeader_UserCallBack(this.houseVoteHouseLeaderCallback));
      RemoteServices.Instance.HouseVoteHouseLeader(RemoteServices.Instance.UserFactionID, houseID, targetFaction, GameEngine.Instance.World.StoredFactionChangesPos);
    }

    private void houseVoteHouseLeaderCallback(HouseVoteHouseLeader_ReturnType returnData)
    {
      if (returnData.Success)
      {
        if (returnData.factionsList != null)
          GameEngine.Instance.World.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
        GameEngine.Instance.World.HouseInfo = returnData.m_houseData;
        GameEngine.Instance.World.YourFaction = returnData.yourFaction;
        GameEngine.Instance.World.HouseVoteInfo = returnData.m_houseVoteData;
      }
      if (this.OnUpdate == null)
        return;
      this.OnUpdate();
    }

    public delegate void HouseInfoUpdatedCallback();
  }
}
