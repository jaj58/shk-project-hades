// Decompiled with JetBrains decompiler
// Type: Kingdoms.MonksManager
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Globalization;

//#nullable disable
namespace Kingdoms
{
  public class MonksManager
  {
    private RemoteServices.SendPeople_UserCallBack m_uicallback;

    public string getDescription(int command, double hours, int numMonks)
    {
      NumberFormatInfo nfi = GameEngine.NFI;
      switch (command)
      {
        case 1:
          return SK.Text("SendMonksPanel_Increase_Popularity", "Increase Popularity within the Parish by :") + " " + numMonks.ToString() + " (" + SK.Text("TOOLTIP_CARD_DURATION", "Duration") + " : " + hours.ToString("N", (IFormatProvider) nfi) + " " + SK.Text("ResearchEffect_X_Hours", nameof (hours)) + ")";
        case 2:
        case 8:
          int votes = this.getVotes(numMonks);
          return votes != 1 ? SK.Text("SendMonksPanel_Send_Influence", "Influence Election by :") + " " + votes.ToString() + " " + SK.Text("SendMonksPanel_X_Votes", "votes") : SK.Text("SendMonksPanel_Send_Influence", "Influence Election by :") + " " + votes.ToString() + " " + SK.Text("SendMonksPanel_X_Vote", "vote");
        case 3:
          return SK.Text("SendMonksPanel_Descrease_Popularity", "Decrease Popularity within the Parish by :") + " " + numMonks.ToString() + " (" + SK.Text("TOOLTIP_CARD_DURATION", "Duration") + " : " + hours.ToString("N", (IFormatProvider) nfi) + " " + SK.Text("ResearchEffect_X_Hours", nameof (hours)) + ")";
        case 4:
          return SK.Text("SendMonksPanel_Protect", "Protect the Village from attack for :") + " " + hours.ToString() + " " + SK.Text("ResearchEffect_X_Hours", nameof (hours));
        case 5:
          int restoration = this.getRestoration(numMonks);
          return SK.Text("SendMonksPanel_Remove_Disease", "Points of Disease healed :") + " " + restoration.ToString();
        case 6:
          return SK.Text("SendMonksPanel_Reduce_Excommunication", "Reduce Excommunication Time in Village by :") + " " + hours.ToString("N", (IFormatProvider) nfi) + " " + SK.Text("ResearchEffect_X_Hours", nameof (hours));
        case 7:
          return SK.Text("SendMonksPanel_Remove_Powers", "Remove Church powers from the Village for :") + " " + hours.ToString("N", (IFormatProvider) nfi) + " " + SK.Text("ResearchEffect_X_Hours", nameof (hours));
        default:
          return "Select an ability";
      }
    }

    public double getEffectDuration(int command, int numMonks)
    {
      double effectDuration = 0.0;
      int index1 = (int) GameEngine.Instance.World.UserResearchData.Research_Marriage;
      if (index1 < 1)
        index1 = 1;
      int index2 = (int) GameEngine.Instance.World.UserResearchData.Research_Confirmation;
      if (index2 < 1)
        index2 = 1;
      int index3 = (int) GameEngine.Instance.World.UserResearchData.Research_Confession;
      if (index3 < 1)
        index3 = 1;
      int index4 = (int) GameEngine.Instance.World.UserResearchData.Research_ExtremeUnction;
      if (index4 < 1)
        index4 = 1;
      switch (command)
      {
        case 1:
          effectDuration = (double) ResearchData.blessingTimes[index1] * CardTypes.getBlessingMultipier(GameEngine.Instance.cardsManager.UserCardData);
          break;
        case 3:
          effectDuration = (double) ResearchData.confirmationTimes[index2] * CardTypes.getInquisitionMultipier(GameEngine.Instance.cardsManager.UserCardData);
          break;
        case 4:
          double currentLevel1 = (double) (numMonks * 4);
          effectDuration = (double) CardTypes.adjustInterdictionLevel(GameEngine.Instance.cardsManager.UserCardData, (int) currentLevel1);
          break;
        case 6:
          double currentLevel2 = (double) (ResearchData.confessionTimes[index3] * numMonks);
          effectDuration = CardTypes.adjustAbsolutionLevel(GameEngine.Instance.cardsManager.UserCardData, currentLevel2);
          break;
        case 7:
          double currentLevel3 = (double) (ResearchData.extremeUnctionTimes[index4] * numMonks);
          effectDuration = CardTypes.adjustExcommunicationLevel(GameEngine.Instance.cardsManager.UserCardData, currentLevel3);
          break;
      }
      return effectDuration;
    }

    public int getRestoration(int numMonks)
    {
      int index = (int) GameEngine.Instance.World.UserResearchData.Research_Baptism;
      if (index < 1)
        index = 1;
      int currentLevel = numMonks * ResearchData.baptismRestoreAmount[index];
      return CardTypes.adjustRestorationLevel(GameEngine.Instance.cardsManager.UserCardData, currentLevel);
    }

    public int getVotes(int numMonks)
    {
      return CardTypes.getInfluenceMultipier(GameEngine.Instance.cardsManager.UserCardData) * numMonks;
    }

    public int getBasePoints(int command, int targetVillageID, int targetUserRank)
    {
      switch (command)
      {
        case 1:
          return GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Blessings;
        case 2:
        case 8:
          return GameEngine.Instance.World.isCountyCapital(targetVillageID) ? GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Influence * 2 : GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Influence;
        case 3:
          return GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Inquisition;
        case 4:
          return GameEngine.Instance.World.isCapital(targetVillageID) ? GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Interdicts * 10 : TradingCalcs.adjustInterdictionCostByTargetRank(GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Interdicts, targetUserRank, GameEngine.Instance.World.SecondAgeWorld);
        case 5:
          return GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Restoration;
        case 6:
          return GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Absolution;
        case 7:
          return GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Excommunication;
        default:
          return 0;
      }
    }

    public void sendMonks(
      RemoteServices.SendPeople_UserCallBack uicallback,
      int numMonks,
      int currentCommand,
      int votedUser)
    {
      this.m_uicallback = uicallback;
      if (numMonks <= 0)
        return;
      int data = -1;
      if (currentCommand == 2 || currentCommand == 8)
        data = votedUser;
      RemoteServices.Instance.set_SendPeople_UserCallBack(new RemoteServices.SendPeople_UserCallBack(this.sendPeopleCallback));
      RemoteServices.Instance.SendPeople(InterfaceMgr.Instance.OwnSelectedVillage, InterfaceMgr.Instance.SelectedVillage, 4, numMonks, currentCommand, data);
      AllVillagesPanel.travellersChanged();
    }

    public void sendPeopleCallback(SendPeople_ReturnType returnData)
    {
      try
      {
        if (returnData.Success)
        {
          GameEngine.Instance.World.importOrphanedPeople(returnData.people, returnData.currentTime, -2);
          GameEngine.Instance.World.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
        }
      }
      catch (Exception ex)
      {
        UniversalDebugLog.Log("Got Exception sending monks: " + ex.Message.ToString());
      }
      if (this.m_uicallback == null)
        return;
      this.m_uicallback(returnData);
    }
  }
}
