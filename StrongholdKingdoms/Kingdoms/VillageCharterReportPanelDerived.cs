// Decompiled with JetBrains decompiler
// Type: Kingdoms.VillageCharterReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  internal class VillageCharterReportPanelDerived : GenericReportPanelBasic
  {
    private Point mapTarget = new Point(-1, -1);
    private double targetZoomLevel;

    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      this.lblSubTitle.Text = SK.Text("Reports_purchased_charter_Failed", "Has Failed to Purchase Village Charter");
      this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
      switch (returnData.reportType)
      {
        case 93:
          this.lblSubTitle.Text = SK.Text("Reports_purchased_charter", "Has Purchased Village Charter");
          this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
          break;
        case 94:
          this.lblFurther.Text = SK.Text("Reports_purchased_charter_Failed_gold", "You had insufficient Gold to purchase this Charter when your Captain arrived at the village.");
          break;
        case 95:
          this.lblFurther.Text = SK.Text("Reports_purchased_charter_Failed_bought", "Someone has purchased this Charter before your captain arrived.");
          break;
        case 96:
          this.lblFurther.Text = SK.Text("Reports_purchased_charter_Failed_too_many", "You already have your maximum number of villages and cannot buy this Charter.");
          break;
      }
      if (returnData.reportType != (short) 93)
        this.showFurtherInfo();
      if (this.m_returnData.defendingVillage < 0)
        return;
      this.mapTarget = GameEngine.Instance.World.getVillageLocation(this.m_returnData.defendingVillage);
      this.targetZoomLevel = 10000.0;
      this.btnUtility.Visible = true;
      this.btnUtility.Text.Text = SK.Text("Reports_Show_On_Map", "Show On Map");
    }

    protected override void utilityClick()
    {
      if (this.mapTarget.X == -1)
        return;
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
      InterfaceMgr.Instance.changeTab(0);
      GameEngine.Instance.World.startMultiStageZoom(this.targetZoomLevel, (double) this.mapTarget.X, (double) this.mapTarget.Y);
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
    }
  }
}
