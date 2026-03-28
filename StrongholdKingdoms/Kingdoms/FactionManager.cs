// Decompiled with JetBrains decompiler
// Type: Kingdoms.FactionManager
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;

//#nullable disable
namespace Kingdoms
{
  public class FactionManager
  {
    private FactionManager.FactionInfoUpdatedCallback OnUpdate;

    public void ApplyToFaction(int factionID, FactionManager.FactionInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      RemoteServices.Instance.set_FactionApplication_UserCallBack(new RemoteServices.FactionApplication_UserCallBack(this.factionApplicationCallback));
      RemoteServices.Instance.FactionApplication(factionID);
    }

    private void factionApplicationCallback(FactionApplication_ReturnType returnData)
    {
      if (returnData.Success)
      {
        GameEngine.Instance.World.FactionInvites = returnData.invites;
        GameEngine.Instance.World.FactionApplications = returnData.applications;
      }
      if (this.OnUpdate == null)
        return;
      this.OnUpdate(returnData.Success);
    }

    public void MakeAlly(int factionID, FactionManager.FactionInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      RemoteServices.Instance.set_CreateFactionRelationship_UserCallBack(new RemoteServices.CreateFactionRelationship_UserCallBack(this.createFactionRelationshipCallback));
      RemoteServices.Instance.CreateFactionRelationship(factionID, 1);
    }

    public void MakeEnemy(int factionID, FactionManager.FactionInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      RemoteServices.Instance.set_CreateFactionRelationship_UserCallBack(new RemoteServices.CreateFactionRelationship_UserCallBack(this.createFactionRelationshipCallback));
      RemoteServices.Instance.CreateFactionRelationship(factionID, -1);
    }

    public void BreakAlliance(int factionID, FactionManager.FactionInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      RemoteServices.Instance.set_CreateFactionRelationship_UserCallBack(new RemoteServices.CreateFactionRelationship_UserCallBack(this.createFactionRelationshipCallback));
      RemoteServices.Instance.CreateFactionRelationship(factionID, 0);
    }

    private void createFactionRelationshipCallback(CreateFactionRelationship_ReturnType returnData)
    {
      if (returnData.Success)
      {
        GameEngine.Instance.World.FactionAllies = returnData.yourAllies;
        GameEngine.Instance.World.FactionEnemies = returnData.yourEnemies;
      }
      if (this.OnUpdate == null)
        return;
      this.OnUpdate(returnData.Success);
    }

    public void declineClicked(int factionID, FactionManager.FactionInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      if (GameEngine.Instance.World.YourFaction != null && factionID == GameEngine.Instance.World.YourFaction.factionID)
      {
        RemoteServices.Instance.set_FactionReplyToInvite_UserCallBack(new RemoteServices.FactionReplyToInvite_UserCallBack(this.factionReplyToInviteCallback));
        RemoteServices.Instance.FactionReplyToInvite(factionID, false);
      }
      else
      {
        RemoteServices.Instance.set_FactionApplication_UserCallBack(new RemoteServices.FactionApplication_UserCallBack(this.factionCancelApplicationCallback));
        RemoteServices.Instance.FactionApplicationCancel(factionID);
      }
    }

    public void factionCancelApplicationCallback(FactionApplication_ReturnType returnData)
    {
      if (returnData.Success)
      {
        GameEngine.Instance.World.FactionInvites = returnData.invites;
        GameEngine.Instance.World.FactionApplications = returnData.applications;
      }
      if (this.OnUpdate == null)
        return;
      this.OnUpdate(returnData.Success);
    }

    public void acceptClicked(int factionID, FactionManager.FactionInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      RemoteServices.Instance.set_FactionReplyToInvite_UserCallBack(new RemoteServices.FactionReplyToInvite_UserCallBack(this.factionReplyToInviteCallback));
      RemoteServices.Instance.FactionReplyToInvite(factionID, true);
    }

    private void factionReplyToInviteCallback(FactionReplyToInvite_ReturnType returnData)
    {
      int errorCode = (int) returnData.m_errorCode;
      if (returnData.Success)
      {
        GameEngine.Instance.World.FactionMembers = returnData.members;
        GameEngine.Instance.World.YourFaction = returnData.yourFaction;
        GameEngine.Instance.World.FactionInvites = returnData.invites;
        GameEngine.Instance.World.FactionApplications = returnData.applications;
        if (returnData.yourFaction != null)
        {
          GameEngine.Instance.World.updateYourVillageFactions(returnData.yourFaction.factionID);
          GameEngine.Instance.World.FactionAllies = returnData.yourAllies;
          GameEngine.Instance.World.FactionEnemies = returnData.yourEnemies;
          GameEngine.Instance.World.HouseAllies = returnData.yourHouseAllies;
          GameEngine.Instance.World.HouseEnemies = returnData.yourHouseEnemies;
        }
        else
          GameEngine.Instance.World.updateYourVillageFactions(-1);
        GameEngine.Instance.World.LastUpdatedCrowns = DateTime.MinValue;
      }
      if (this.OnUpdate == null)
        return;
      this.OnUpdate(returnData.Success);
    }

    public void RejectApplication(int userID, FactionManager.FactionInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      RemoteServices.Instance.set_FactionApplicationProcessing_UserCallBack(new RemoteServices.FactionApplicationProcessing_UserCallBack(this.factionApplicationProcessingCallback));
      RemoteServices.Instance.FactionApplicationReject(userID);
    }

    public void AcceptApplication(int userID, FactionManager.FactionInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      RemoteServices.Instance.set_FactionApplicationProcessing_UserCallBack(new RemoteServices.FactionApplicationProcessing_UserCallBack(this.factionApplicationProcessingCallback));
      RemoteServices.Instance.FactionApplicationAccept(userID);
    }

    private void factionApplicationProcessingCallback(
      FactionApplicationProcessing_ReturnType returnData)
    {
      int num = returnData.Success ? 1 : 0;
      if (returnData.members != null)
      {
        GameEngine.Instance.World.FactionMembers = returnData.members;
        GameEngine.Instance.World.YourFaction = returnData.yourFaction;
      }
      if (this.OnUpdate == null)
        return;
      this.OnUpdate(returnData.Success);
    }

    public void WithdrawInvite(int userID, FactionManager.FactionInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      RemoteServices.Instance.set_FactionWithdrawInvite_UserCallBack(new RemoteServices.FactionWithdrawInvite_UserCallBack(this.factionWithdrawInviteCallback));
      RemoteServices.Instance.FactionWithdrawInvite(userID);
    }

    private void factionWithdrawInviteCallback(FactionWithdrawInvite_ReturnType returnData)
    {
      if (returnData.members != null)
        GameEngine.Instance.World.FactionMembers = returnData.members;
      if (this.OnUpdate == null)
        return;
      this.OnUpdate(returnData.Success);
    }

    public void Promote(
      FactionMemberData memberData,
      FactionManager.FactionInfoUpdatedCallback callback)
    {
      int rank = memberData.status;
      if (memberData.status == 0)
        rank = 2;
      if (memberData.status == 2)
        rank = 1;
      this.changeRank(memberData.userID, rank, callback);
    }

    public void Demote(
      FactionMemberData memberData,
      FactionManager.FactionInfoUpdatedCallback callback)
    {
      int rank = memberData.status;
      if (memberData.status == 1)
        rank = 2;
      if (memberData.status == 2)
        rank = 0;
      this.changeRank(memberData.userID, rank, callback);
    }

    private void changeRank(
      int userID,
      int rank,
      FactionManager.FactionInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      RemoteServices.Instance.set_FactionChangeMemberStatus_UserCallBack(new RemoteServices.FactionChangeMemberStatus_UserCallBack(this.factionChangeMemberStatusCallback));
      RemoteServices.Instance.FactionChangeMemberStatus(userID, rank);
    }

    public void dismissMember(int userID, FactionManager.FactionInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      RemoteServices.Instance.set_FactionChangeMemberStatus_UserCallBack(new RemoteServices.FactionChangeMemberStatus_UserCallBack(this.factionChangeMemberStatusCallback));
      RemoteServices.Instance.FactionChangeMemberStatus(userID, -2);
    }

    private void factionChangeMemberStatusCallback(FactionChangeMemberStatus_ReturnType returnData)
    {
      if (returnData.Success)
      {
        GameEngine.Instance.World.FactionMembers = returnData.members;
        GameEngine.Instance.World.YourFaction = returnData.yourFaction;
      }
      if (this.OnUpdate == null)
        return;
      this.OnUpdate(returnData.Success);
    }

    public void voteLeaderChange(int userID, FactionManager.FactionInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      RemoteServices.Instance.set_FactionLeadershipVote_UserCallBack(new RemoteServices.FactionLeadershipVote_UserCallBack(this.factionLeadershipVoteCallback));
      RemoteServices.Instance.FactionLeadershipVote(RemoteServices.Instance.UserFactionID, userID);
    }

    private void factionLeadershipVoteCallback(FactionLeadershipVote_ReturnType returnData)
    {
      if (returnData.Success)
      {
        GameEngine.Instance.World.YourFactionVote = returnData.yourLeaderVote;
        if (returnData.leaderChanged)
        {
          RemoteServices.Instance.UserFactionID = returnData.yourFaction.factionID;
          GameEngine.Instance.World.YourFaction = returnData.yourFaction;
          GameEngine.Instance.World.FactionMembers = returnData.members;
          GameEngine.Instance.World.FactionInvites = returnData.invites;
          GameEngine.Instance.World.FactionApplications = returnData.applications;
        }
      }
      if (this.OnUpdate == null)
        return;
      this.OnUpdate(returnData.Success);
    }

    public bool IsPlayerInFaction(int factionID)
    {
      return GameEngine.Instance.World.YourFaction != null && factionID == GameEngine.Instance.World.YourFaction.factionID;
    }

    public void createFaction(
      string factionName,
      string factionAbbreviation,
      string factionMotta,
      int flagdata,
      FactionManager.FactionInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      RemoteServices.Instance.CreateFaction(factionName, factionAbbreviation, factionMotta, flagdata);
      RemoteServices.Instance.set_CreateFaction_UserCallBack(new RemoteServices.CreateFaction_UserCallBack(this.createFactionCallback));
    }

    private void createFactionCallback(CreateFaction_ReturnType returnData)
    {
      if (returnData.Success && returnData.yourFaction != null)
      {
        RemoteServices.Instance.UserFactionID = returnData.yourFaction.factionID;
        GameEngine.Instance.World.YourFaction = returnData.yourFaction;
        GameEngine.Instance.World.FactionMembers = returnData.members;
        GameEngine.Instance.World.FactionAllies = (int[]) null;
        GameEngine.Instance.World.FactionEnemies = (int[]) null;
        GameEngine.Instance.World.HouseAllies = (int[]) null;
        GameEngine.Instance.World.HouseEnemies = (int[]) null;
        GameEngine.Instance.World.LastUpdatedCrowns = DateTime.MinValue;
      }
      if (this.OnUpdate == null)
        return;
      this.OnUpdate(returnData.Success);
    }

    public void leaveFaction(FactionManager.FactionInfoUpdatedCallback callback)
    {
      this.OnUpdate = callback;
      RemoteServices.Instance.set_FactionLeave_UserCallBack(new RemoteServices.FactionLeave_UserCallBack(this.leaveFactionCallback));
      RemoteServices.Instance.FactionLeave();
    }

    private void leaveFactionCallback(FactionLeave_ReturnType returnData)
    {
      if (returnData.Success)
      {
        RemoteServices.Instance.UserFactionID = -1;
        GameEngine.Instance.World.FactionMembers = (FactionMemberData[]) null;
        GameEngine.Instance.World.FactionInvites = returnData.invites;
        GameEngine.Instance.World.FactionApplications = returnData.applications;
        GameEngine.Instance.World.FactionAllies = (int[]) null;
        GameEngine.Instance.World.FactionEnemies = (int[]) null;
        GameEngine.Instance.World.HouseAllies = (int[]) null;
        GameEngine.Instance.World.HouseEnemies = (int[]) null;
        GameEngine.Instance.World.HouseInfo = returnData.m_houseData;
      }
      if (this.OnUpdate == null)
        return;
      this.OnUpdate(returnData.Success);
    }

    public delegate void FactionInfoUpdatedCallback(bool success);
  }
}
