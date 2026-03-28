// Decompiled with JetBrains decompiler
// Type: Kingdoms.VassalReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;

//#nullable disable
namespace Kingdoms
{
  internal class VassalReportPanelDerived : GenericReportPanelBasic
  {
    private int villageID = -1;

    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      switch (returnData.reportType)
      {
        case 15:
          CustomSelfDrawPanel.CSDLabel lblMainText1 = this.lblMainText;
          lblMainText1.Text = lblMainText1.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Has_Lost_Vassal", "Has Lost a Vassal");
          this.lblSecondaryText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          this.villageID = returnData.attackingVillage;
          this.lblFurther.Text = SK.Text("Reports_Troops_Lost", "Troops Lost") + " : " + returnData.genericData1.ToString("N", (IFormatProvider) this.nfi);
          this.lblFurther.Visible = true;
          this.addControl((CustomSelfDrawPanel.CSDControl) this.lblFurther);
          break;
        case 16:
          CustomSelfDrawPanel.CSDLabel lblMainText2 = this.lblMainText;
          lblMainText2.Text = lblMainText2.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_No_Longer_Liege_Lord", "No longer has a liege lord");
          this.lblSecondaryText.Text = "";
          this.villageID = returnData.attackingVillage;
          break;
        case 46:
          this.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Offers_Liege_lord", "offers to be liege lord of");
          this.lblSecondaryText.Text = this.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          this.villageID = returnData.defendingVillage;
          break;
        case 47:
          this.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Accepted_Liege_Lord", "has accepted your liege lord offer and becomes your vassal to");
          this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.attackingVillage);
          this.villageID = returnData.attackingVillage;
          break;
        case 48:
          this.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Has_declined_Liege_lord_Offer", "has declined your liege lord offer from");
          this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.attackingVillage);
          this.villageID = returnData.attackingVillage;
          break;
        case 49:
          this.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Withdrawn_Liege_Lord_Offer", "has withdrawn the liege lord offer for");
          this.lblSecondaryText.Text = this.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          this.villageID = returnData.defendingVillage;
          break;
      }
      if (GameEngine.Instance.World.isUserVillage(this.villageID))
      {
        this.btnUtility.Text.Text = SK.Text("GENERIC_Vassals", "Vassals");
        this.btnUtility.Visible = true;
      }
      else
        this.btnUtility.Visible = false;
    }

    protected override void utilityClick()
    {
      if (this.villageID < 0)
        return;
      GameEngine.Instance.playInterfaceSound("VassalLostReportPanel_vassals");
      InterfaceMgr.Instance.selectUserVillage(this.villageID, false);
      GameEngine.Instance.SkipVillageTab();
      InterfaceMgr.Instance.getMainTabBar().changeTab(1);
      InterfaceMgr.Instance.setVillageTabSubMode(8);
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
    }
  }
}
