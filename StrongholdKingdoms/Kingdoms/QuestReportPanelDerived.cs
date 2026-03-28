// Decompiled with JetBrains decompiler
// Type: Kingdoms.QuestReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;

//#nullable disable
namespace Kingdoms
{
  internal class QuestReportPanelDerived : GenericReportPanelBasic
  {
    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      switch (returnData.reportType)
      {
        case 100:
          this.lblSubTitle.Text = SK.Text("Reports_Quest_Complete", "Completed Quest") + " :";
          this.lblSecondaryText.Text = SK.NoStoreText("Z_QUESTS_" + NewQuests.getNewQuestDef(returnData.genericData1).tagString);
          break;
        case 101:
          this.lblSubTitle.Text = SK.Text("Reports_Quest Failed", "Failed Quest") + " :";
          this.lblSecondaryText.Text = SK.NoStoreText("Z_QUESTS_" + NewQuests.getNewQuestDef(returnData.genericData1).tagString);
          break;
        case 102:
          this.lblSubTitle.Text = SK.Text("Reports_Spins", "Wheel Spin Prize");
          this.lblSecondaryText.Text = Wheel.getRewardText(returnData.genericData1, returnData.genericData2, this.nfi);
          return;
        case 129:
        case 130:
        case 131:
        case 136:
          if (returnData.reportType == (short) 129)
            this.lblSubTitle.Text = SK.Text("Reports_AI_Spins", "Wheel Spin Bonus from AI Razing");
          else if (returnData.reportType == (short) 136)
            this.lblSubTitle.Text = SK.Text("Reports_Heretic_Spins", "Wheel Spin Bonus from Player Razing");
          else if (returnData.reportType == (short) 131)
            this.lblSubTitle.Text = SK.Text("Reports_AI_Spins_capture", "Wheel Spin Bonus from AI Capture");
          else
            this.lblSubTitle.Text = SK.Text("Reports_Forage_Spins", "Wheel Spin Bonus from Foraging");
          switch (returnData.genericData1)
          {
            case 2:
              if (returnData.genericData2 <= 1)
              {
                this.lblSecondaryText.Text = SK.Text("REPORTS_SeasonalWheelSpins2", "Tier 2 Wheel Spin");
                return;
              }
              this.lblSecondaryText.Text = SK.Text("REPORTS_SeasonalWheelSpins2_bonus", "2 Tier 2 Wheel Spins");
              return;
            case 3:
              if (returnData.genericData2 <= 1)
              {
                this.lblSecondaryText.Text = SK.Text("REPORTS_SeasonalWheelSpins3", "Tier 3 Wheel Spin");
                return;
              }
              this.lblSecondaryText.Text = SK.Text("REPORTS_SeasonalWheelSpins3_bonus", "2 Tier 3 Wheel Spins");
              return;
            case 4:
              if (returnData.genericData2 <= 1)
              {
                this.lblSecondaryText.Text = SK.Text("REPORTS_SeasonalWheelSpins4", "Tier 4 Wheel Spin");
                return;
              }
              this.lblSecondaryText.Text = SK.Text("REPORTS_SeasonalWheelSpins4_bonus", "2 Tier 4 Wheel Spins");
              return;
            case 5:
              if (returnData.genericData2 <= 1)
              {
                this.lblSecondaryText.Text = SK.Text("REPORTS_SeasonalWheelSpins5", "Tier 5 Wheel Spin");
                return;
              }
              this.lblSecondaryText.Text = SK.Text("REPORTS_SeasonalWheelSpins5_bonus", "2 Tier 5 Wheel Spins");
              return;
            default:
              if (returnData.genericData2 <= 1)
              {
                this.lblSecondaryText.Text = SK.Text("REPORTS_SeasonalWheelSpins1", "Tier 1 Wheel Spin");
                return;
              }
              this.lblSecondaryText.Text = SK.Text("REPORTS_SeasonalWheelSpins1_bonus", "2 Tier 1 Wheel Spins");
              return;
          }
      }
      this.btnUtility.Visible = true;
      this.btnUtility.Text.Text = SK.Text("GENERIC_Quests", "Quests");
    }

    protected override void utilityClick()
    {
      InterfaceMgr.Instance.getMainTabBar().changeTab(5);
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
    }
  }
}
