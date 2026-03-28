// Decompiled with JetBrains decompiler
// Type: Kingdoms.ReligiousReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;

//#nullable disable
namespace Kingdoms
{
  internal class ReligiousReportPanelDerived : GenericReportPanelBasic
  {
    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      switch (returnData.reportType)
      {
        case 66:
          CustomSelfDrawPanel.CSDLabel lblMainText1 = this.lblMainText;
          lblMainText1.Text = lblMainText1.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Has_Influenced_Voting", "Has Influenced Voting at");
          this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
          break;
        case 67:
          CustomSelfDrawPanel.CSDLabel lblMainText2 = this.lblMainText;
          lblMainText2.Text = lblMainText2.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Removed_Disease", "Has Removed Disease From");
          this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
          break;
        case 68:
          CustomSelfDrawPanel.CSDLabel lblMainText3 = this.lblMainText;
          lblMainText3.Text = lblMainText3.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Has_Interdicted", "Has Interdict Protected");
          this.lblSecondaryText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          break;
        case 69:
          CustomSelfDrawPanel.CSDLabel lblMainText4 = this.lblMainText;
          lblMainText4.Text = lblMainText4.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Has_Inquisited", "Has Inquisited");
          this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
          break;
        case 70:
          CustomSelfDrawPanel.CSDLabel lblMainText5 = this.lblMainText;
          lblMainText5.Text = lblMainText5.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Has_Excommunicated", "Has Excommunicated");
          this.lblSecondaryText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          break;
        case 71:
          CustomSelfDrawPanel.CSDLabel lblMainText6 = this.lblMainText;
          lblMainText6.Text = lblMainText6.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Has_Absolved", "Has Absolved");
          this.lblSecondaryText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          break;
        case 72:
          CustomSelfDrawPanel.CSDLabel lblMainText7 = this.lblMainText;
          lblMainText7.Text = lblMainText7.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Has_Blessed", "Has Blessed");
          this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
          break;
        case 91:
          this.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Has_Excommunicated", "Has Excommunicated");
          this.lblSecondaryText.Text = this.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          break;
        case 103:
          this.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Has_Absolved", "Has Absolved");
          this.lblSecondaryText.Text = this.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          break;
        case 104:
          this.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Has_Inquisited", "Has Inquisited");
          this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
          break;
        case 105:
          this.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Has_Interdicted", "Has Interdict Protected");
          this.lblSecondaryText.Text = this.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          break;
        case 106:
          this.lblMainText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
          if (returnData.genericData1 == 0)
          {
            this.lblSubTitle.Text = SK.Text("Reports_Interdiction_Has_Ended", "Interdiction Has Ended");
            break;
          }
          this.lblSubTitle.Text = SK.Text("Reports_Interdiction_Termination", "Interdiction Was Terminated");
          break;
      }
      this.initFurtherInfo();
    }

    private void initFurtherInfo()
    {
      switch (this.m_returnData.reportType)
      {
        case 66:
          if (this.m_returnData.genericData1 > 0)
          {
            if (this.m_returnData.genericData1 != 1)
            {
              this.lblFurther.Text = SK.Text("Reports_Votes_Given", "Votes Given") + " : " + this.m_returnData.otherUser + " (" + this.m_returnData.genericData1.ToString() + " " + SK.Text("Reports_Votes", "Votes") + ")";
              break;
            }
            this.lblFurther.Text = SK.Text("Reports_Votes_Given", "Votes Given") + " : " + this.m_returnData.otherUser + " (" + SK.Text("Reports_1_Vote", "1 Vote") + ")";
            break;
          }
          if (this.m_returnData.genericData1 != -1)
          {
            this.lblFurther.Text = SK.Text("Reports_Votes_Lost", "Votes Lost") + " : " + this.m_returnData.otherUser + " (" + (-this.m_returnData.genericData1).ToString() + " " + SK.Text("Reports_Votes", "Votes") + ")";
            break;
          }
          this.lblFurther.Text = SK.Text("Reports_Votes_Lost", "Votes Lost") + " : " + this.m_returnData.otherUser + " (" + SK.Text("Reports_1_Vote", "1 Vote") + ")";
          break;
        case 67:
          this.lblFurther.Text = SK.Text("Reports_Disease_Points_Removed", "Disease points removed") + " : " + this.m_returnData.genericData1.ToString();
          CustomSelfDrawPanel.CSDLabel lblFurther1 = this.lblFurther;
          lblFurther1.Text = lblFurther1.Text + Environment.NewLine + Environment.NewLine;
          if (this.m_returnData.genericData1 > 0)
          {
            CustomSelfDrawPanel.CSDLabel lblFurther2 = this.lblFurther;
            lblFurther2.Text = lblFurther2.Text + SK.Text("GENERIC_Honour", "Honour") + " : " + (this.m_returnData.genericData1 * GameEngine.Instance.LocalWorldData.HonourForClearingDisease).ToString("N", (IFormatProvider) this.nfi);
            break;
          }
          break;
        case 68:
        case 105:
          if (this.m_returnData.genericData1 != 1)
          {
            this.lblFurther.Text = SK.Text("Reports_Protection", "Protection") + " : " + this.m_returnData.genericData1.ToString() + " " + SK.Text("Reports_Hours", "hours");
            break;
          }
          this.lblFurther.Text = SK.Text("Reports_Protection", "Protection") + " : " + SK.Text("Reports_1_hour", "1 hour");
          break;
        case 69:
        case 104:
          if (this.m_returnData.genericData2 != 10)
          {
            string str = this.m_returnData.genericData2 % 10 != 0 ? ((double) this.m_returnData.genericData2 / 10.0).ToString() : (this.m_returnData.genericData2 / 10).ToString();
            this.lblFurther.Text = SK.Text("Reports_Popularity_Penalty", "Popularity Penalty") + " : " + this.m_returnData.genericData1.ToString() + " (" + str + " " + SK.Text("Reports_Hours", "hours") + ")";
            break;
          }
          this.lblFurther.Text = SK.Text("Reports_Popularity_Penalty", "Popularity Penalty") + " : " + this.m_returnData.genericData1.ToString() + " (" + SK.Text("Reports_1_hour", "1 hour") + ")";
          break;
        case 70:
        case 91:
          if (this.m_returnData.genericData1 != 10)
          {
            string str = this.m_returnData.genericData1 % 10 != 0 ? ((double) this.m_returnData.genericData1 / 10.0).ToString() : (this.m_returnData.genericData1 / 10).ToString();
            this.lblFurther.Text = SK.Text("Reports_Excommunication", "Excommunication") + " : " + str + " " + SK.Text("Reports_Hours", "hours");
            break;
          }
          this.lblFurther.Text = SK.Text("Reports_Excommunication", "Excommunication") + " : " + SK.Text("Reports_1_hour", "1 hour");
          break;
        case 71:
        case 103:
          if (this.m_returnData.genericData1 != 10)
          {
            string str = this.m_returnData.genericData1 % 10 != 0 ? ((double) this.m_returnData.genericData1 / 10.0).ToString() : (this.m_returnData.genericData1 / 10).ToString();
            this.lblFurther.Text = SK.Text("Reports_Absolution", "Absolution") + " : " + str + " " + SK.Text("Reports_Hours", "hours");
            break;
          }
          this.lblFurther.Text = SK.Text("Reports_Absolution", "Absolution") + " : " + SK.Text("Reports_1_hour", "1 hour");
          break;
        case 72:
          if (this.m_returnData.genericData2 != 10)
          {
            string str = this.m_returnData.genericData2 % 10 != 0 ? ((double) this.m_returnData.genericData2 / 10.0).ToString() : (this.m_returnData.genericData2 / 10).ToString();
            this.lblFurther.Text = SK.Text("Reports_Popularity_Bonus", "Popularity Bonus") + " : " + this.m_returnData.genericData1.ToString() + " (" + str + " " + SK.Text("Reports_Hours", "hours") + ")";
            break;
          }
          this.lblFurther.Text = SK.Text("Reports_Popularity_Bonus", "Popularity Bonus") + " : " + this.m_returnData.genericData1.ToString() + " (" + SK.Text("Reports_1_hour", "1 hour") + ")";
          break;
      }
      this.showFurtherInfo();
    }
  }
}
