// Decompiled with JetBrains decompiler
// Type: Kingdoms.ParishElectionReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  internal class ParishElectionReportPanelDerived : GenericReportPanelBasic
  {
    private Point mapTarget = new Point(-1, -1);
    private double targetZoomLevel;

    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      this.lblMainText.Text = returnData.otherUser;
      this.lblSubTitle.Text = SK.Text("Reports_Is_Elected_For", "Is Elected For");
      switch (returnData.reportType)
      {
        case 28:
          this.lblSecondaryText.Text = GameEngine.Instance.World.getParishNameFromVillageID(returnData.genericData8);
          break;
        case 53:
          this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.genericData8) + " / " + GameEngine.Instance.World.getCountyName(GameEngine.Instance.World.getCountyFromVillageID(returnData.genericData8));
          break;
        case 74:
          this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.genericData8) + " / " + GameEngine.Instance.World.getProvinceName(GameEngine.Instance.World.getProvinceFromVillageID(returnData.genericData8));
          break;
        case 75:
          this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.genericData8) + " / " + GameEngine.Instance.World.getCountryName(GameEngine.Instance.World.getCountryFromVillageID(returnData.genericData8));
          break;
      }
      if (returnData.genericData8 >= 0)
      {
        this.mapTarget = GameEngine.Instance.World.getVillageLocation(returnData.genericData8);
        this.targetZoomLevel = 10000.0;
        this.btnUtility.Text.Text = SK.Text("Reports_Show_On_Map", "Show On Map");
        this.btnUtility.Visible = true;
      }
      else
        this.btnUtility.Visible = false;
    }

    protected override void utilityClick()
    {
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
      if (this.mapTarget.X == -1)
        return;
      InterfaceMgr.Instance.changeTab(0);
      GameEngine.Instance.World.startMultiStageZoom(this.targetZoomLevel, (double) this.mapTarget.X, (double) this.mapTarget.Y);
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
    }
  }
}
