// Decompiled with JetBrains decompiler
// Type: Kingdoms.FactionReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  internal class FactionReportPanelDerived : GenericReportPanelBasic
  {
    private Point mapTarget = new Point(-1, -1);
    private int subTypeID = -1;

    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      switch (returnData.reportType)
      {
        case 50:
          this.lblSubTitle.Text = SK.Text("Reports_Invite_to", "You have an Invitation To");
          FactionData faction1 = GameEngine.Instance.World.getFaction(returnData.genericData1);
          if (faction1 != null)
          {
            this.lblSecondaryText.Text = faction1.factionName;
            break;
          }
          break;
        case 107:
          this.lblMainText.Text = SK.Text("Reports_Faction_New_Member", "New Faction Member");
          this.lblSecondaryText.Text = returnData.otherUser;
          break;
        case 108:
          if (returnData.otherUser == "")
          {
            this.lblMainText.Text = SK.Text("Reports_Faction_Member_Leave_Self", "You are no longer a member of this Faction");
            FactionData faction2 = GameEngine.Instance.World.getFaction(returnData.genericData1);
            if (faction2 != null)
            {
              this.lblSecondaryText.Text = faction2.factionName;
              break;
            }
            break;
          }
          this.lblMainText.Text = SK.Text("Reports_Faction_Member_Leave", "Member Leaving Faction");
          this.lblSecondaryText.Text = returnData.otherUser;
          break;
        case 109:
          if (returnData.otherUser == "")
          {
            FactionData faction3 = GameEngine.Instance.World.getFaction(returnData.genericData1);
            if (faction3 != null)
              this.lblSecondaryText.Text = faction3.factionName;
            switch (returnData.genericData3)
            {
              case -1:
                this.lblMainText.Text = " ";
                break;
              case 1:
                this.lblMainText.Text = SK.Text("Reports_Faction_Member_Dismissed_Leader", "You were dismissed by the leader of the faction");
                break;
              case 2:
                this.lblMainText.Text = SK.Text("Reports_Faction_Member_Dismissed_Officer", "You were dismissed by an officer of the faction");
                break;
            }
          }
          else
          {
            this.lblMainText.Text = SK.Text("Reports_Faction_Member_Dismissed", "Faction Member Dismissed");
            this.lblSecondaryText.Text = returnData.otherUser;
            break;
          }
          break;
        case 110:
          if (returnData.genericData2 == 2)
            this.lblSubTitle.Text = SK.Text("Reports_Faction_Promotion_Officer", "You have been promoted To Officer");
          else
            this.lblSubTitle.Text = SK.Text("Reports_Faction_Promotion_Leader", "You have been promoted To Leader");
          FactionData faction4 = GameEngine.Instance.World.getFaction(returnData.genericData1);
          if (faction4 != null)
          {
            this.lblSecondaryText.Text = faction4.factionName;
            break;
          }
          break;
        case 111:
          if (returnData.genericData2 == 2)
            this.lblSubTitle.Text = SK.Text("Reports_Faction_Demotion_Officer", "You have been demoted To Officer");
          else
            this.lblSubTitle.Text = SK.Text("Reports_Faction_Demotion_Leader", "You have been demoted To Commoner");
          FactionData faction5 = GameEngine.Instance.World.getFaction(returnData.genericData1);
          if (faction5 != null)
          {
            this.lblSecondaryText.Text = faction5.factionName;
            break;
          }
          break;
        case 112:
          switch (returnData.genericData3)
          {
            case -1:
              this.lblSubTitle.Text = SK.Text("Reports_Faction_Enemy", "has set the following Faction as an Enemy") + " :";
              break;
            case 0:
              this.lblSubTitle.Text = SK.Text("Reports_Faction_Neutral", "has set the following Faction as neutral") + " :";
              break;
            case 1:
              this.lblSubTitle.Text = SK.Text("Reports_Faction_Friend", "has set the following Faction as a Friend") + " :";
              break;
          }
          FactionData faction6 = GameEngine.Instance.World.getFaction(returnData.genericData1);
          if (faction6 != null)
            this.lblMainText.Text = faction6.factionName;
          FactionData faction7 = GameEngine.Instance.World.getFaction(returnData.genericData2);
          if (faction6 != null)
          {
            this.lblSecondaryText.Text = faction7.factionName;
            break;
          }
          break;
        case 113:
          switch (returnData.genericData3)
          {
            case -1:
              this.lblSubTitle.Text = SK.Text("Reports_Faction_LeaveHouse", "has left");
              break;
            case 1:
              this.lblSubTitle.Text = SK.Text("Reports_Faction_JoinHouse", "has joined");
              break;
          }
          FactionData faction8 = GameEngine.Instance.World.getFaction(returnData.genericData1);
          if (faction8 != null)
            this.lblMainText.Text = faction8.factionName;
          this.lblSecondaryText.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + returnData.genericData2.ToString();
          break;
        case 114:
          switch (returnData.genericData3)
          {
            case -1:
              this.lblSubTitle.Text = SK.Text("Reports_House_Enemy", "has set the following House as an Enemy") + " :";
              break;
            case 0:
              this.lblSubTitle.Text = SK.Text("Reports_House_Neutral", "has set the following House as Neutral") + " :";
              break;
            case 1:
              this.lblSubTitle.Text = SK.Text("Reports_House_Friend", "has set the following House as a Friend") + " :";
              break;
          }
          this.lblMainText.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + returnData.genericData1.ToString();
          this.lblSecondaryText.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + returnData.genericData2.ToString();
          break;
        case 115:
          this.lblSubTitle.Text = "";
          this.lblMainText.Text = SK.Text("Reports_Faction_Application", "A Player Has Applied to your Faction");
          this.lblSecondaryText.Text = returnData.otherUser;
          break;
        case 116:
          this.lblSubTitle.Text = SK.Text("Reports_Faction_Application_accepted", "Your Faction Application has been Accepted.");
          FactionData faction9 = GameEngine.Instance.World.getFaction(returnData.genericData1);
          if (faction9 != null)
          {
            this.lblSecondaryText.Text = faction9.factionName;
            break;
          }
          break;
        case 117:
          this.lblSubTitle.Text = SK.Text("Reports_Faction_Application_rejected", "Your Faction Application has been Rejected.");
          FactionData faction10 = GameEngine.Instance.World.getFaction(returnData.genericData1);
          if (faction10 != null)
          {
            this.lblSecondaryText.Text = faction10.factionName;
            break;
          }
          break;
        case 118:
          this.lblSubTitle.Text = "";
          FactionData faction11 = GameEngine.Instance.World.getFaction(returnData.genericData1);
          if (faction11 != null)
            this.lblSecondaryText.Text = faction11.factionName;
          switch (returnData.genericData3)
          {
            case -1:
              this.lblMainText.Text = " ";
              break;
            case 1:
              this.lblMainText.Text = SK.Text("Reports_Faction_Member_Dismissed_Leader", "You were dismissed by the leader of the faction");
              break;
            case 2:
              this.lblMainText.Text = SK.Text("Reports_Faction_Member_Dismissed_Officer", "You were dismissed by an officer of the faction");
              break;
          }
          if (returnData.otherUser != "")
          {
            this.lblSubTitle.Text = returnData.otherUser;
            break;
          }
          break;
        case 120:
          if (returnData.genericData2 == 0)
          {
            this.lblMainText.Text = returnData.otherUser;
            this.lblSubTitle.Text = SK.Text("Reports_Faction_Glory_Obtained", "This player claimed glory points for your house");
            this.lblSecondaryText.Text = returnData.genericData1.ToString() + " " + SK.Text("Reports_Faction_Glory_Amount", "glory points awarded");
            break;
          }
          this.lblMainText.Text = "";
          this.lblSubTitle.Text = SK.Text("Reports_Faction_Glory_You_Obtained", "You claimed glory points for your house");
          this.lblSecondaryText.Text = returnData.genericData1.ToString() + " " + SK.Text("Reports_Faction_Glory_Amount", "glory points awarded");
          break;
        case 134:
          this.lblMainText.Text = returnData.otherUser;
          this.lblSubTitle.Text = SK.Text("Reports_Royal_Tower_Gained", "has captured a Royal Tower for your House!");
          break;
        case 135:
          this.lblMainText.Text = "";
          this.lblSubTitle.Text = SK.Text("ReportsPanel_Royal_Tower_Lost", "Royal Tower Lost!");
          break;
      }
      switch (returnData.reportType)
      {
        case 50:
          this.btnUtility.Visible = true;
          this.btnUtility.Text.Text = SK.Text("GENERIC_Factions", "Factions");
          this.subTypeID = 5;
          break;
        case 107:
        case 108:
        case 109:
        case 110:
        case 111:
        case 116:
        case 118:
        case 120:
          if (RemoteServices.Instance.UserFactionID == -1)
            break;
          this.btnUtility.Text.Text = SK.Text("GENERIC_Factions", "Factions");
          this.btnUtility.Visible = true;
          this.subTypeID = 2;
          break;
        case 112:
          if (RemoteServices.Instance.UserFactionID == -1)
            break;
          this.btnUtility.Visible = true;
          this.btnUtility.Text.Text = SK.Text("GENERIC_Factions", "Factions");
          this.subTypeID = 4;
          break;
        case 113:
        case 114:
        case 117:
          this.btnUtility.Text.Text = SK.Text("GENERIC_Factions", "Factions");
          this.btnUtility.Visible = true;
          this.subTypeID = 1;
          break;
        case 115:
          if (RemoteServices.Instance.UserFactionID == -1)
            break;
          this.btnUtility.Text.Text = SK.Text("GENERIC_Factions", "Factions");
          this.btnUtility.Visible = true;
          this.subTypeID = 3;
          break;
        case 134:
        case 135:
          this.btnUtility.Visible = true;
          this.btnUtility.Text.Text = SK.Text("Reports_View_Target", "View Target");
          this.subTypeID = 6;
          this.mapTarget = GameEngine.Instance.World.getVillageLocation(returnData.defendingVillage);
          break;
      }
    }

    protected override void utilityClick()
    {
      GameEngine.Instance.playInterfaceSound("FactionInviteReportPanel_faction");
      CustomSelfDrawPanel.FactionPanelSideBar.forceReUpdate();
      switch (this.subTypeID)
      {
        case 1:
          GameEngine.Instance.setNextFactionPage(51);
          break;
        case 2:
          InterfaceMgr.Instance.showFactionPanel(RemoteServices.Instance.UserFactionID);
          break;
        case 3:
          GameEngine.Instance.setNextFactionPage(46);
          break;
        case 4:
          GameEngine.Instance.setNextFactionPage(44);
          break;
        case 5:
          GameEngine.Instance.setNextFactionPage(41);
          break;
        case 6:
          if (this.mapTarget.X == -1)
            return;
          GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
          InterfaceMgr.Instance.changeTab(0);
          GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) this.mapTarget.X, (double) this.mapTarget.Y);
          this.m_parent.closeControl(true);
          InterfaceMgr.Instance.reactiveMainWindow();
          return;
      }
      if (this.subTypeID != 2)
        InterfaceMgr.Instance.getMainTabBar().changeTab(8);
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
    }
  }
}
