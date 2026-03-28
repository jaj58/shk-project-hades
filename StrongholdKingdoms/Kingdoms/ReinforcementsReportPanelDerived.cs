// Decompiled with JetBrains decompiler
// Type: Kingdoms.ReinforcementsReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  internal class ReinforcementsReportPanelDerived : GenericReportPanelBasic
  {
    private ReportBattleValuesPanel valuesPanel;
    private Point mapTarget = new Point(-1, -1);
    private double targetZoomLevel;
    protected TroopCount reinforcementsTroopCount;

    public ReinforcementsReportPanelDerived()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.Size = new Size(580, 480);
    }

    public override void init(IDockableControl parent, Size size, object back)
    {
      base.init(parent, size, back);
      this.valuesPanel = new ReportBattleValuesPanel((CustomSelfDrawPanel) this, new Size(this.btnClose.X - this.btnForward.Rectangle.Right - 4, 200));
      this.valuesPanel.Position = new Point(this.btnForward.Rectangle.Right + 2, this.Height - this.valuesPanel.Height);
      this.valuesPanel.init(SK.Text("GENERIC_Reinforcements", "Reinforcements"), false, true);
      if (this.hasBackground())
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.valuesPanel);
      else
        this.addControl((CustomSelfDrawPanel.CSDControl) this.valuesPanel);
    }

    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      this.valuesPanel.setData(returnData.genericData1, returnData.genericData2, returnData.genericData3, returnData.genericData4, returnData.genericData5, 0);
      switch (returnData.reportType)
      {
        case 17:
          this.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Sent_Reinforcements_To", "sent reinforcements to");
          this.lblSecondaryText.Text = this.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          break;
        case 18:
          CustomSelfDrawPanel.CSDLabel lblMainText = this.lblMainText;
          lblMainText.Text = lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Reinforcements_Returned_From", "Reinforcements have returned from");
          this.lblSecondaryText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          break;
        case 19:
          this.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Retrieved_Reinforcements", "has retrieved reinforcements from");
          this.lblSecondaryText.Text = this.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          break;
      }
      if (returnData.defendingVillage >= 0)
      {
        this.mapTarget = GameEngine.Instance.World.getVillageLocation(returnData.defendingVillage);
        this.targetZoomLevel = 10000.0;
        this.btnUtility.Visible = true;
        this.btnUtility.Text.Text = SK.Text("Reports_Show_On_Map", "Show On Map");
      }
      else
        this.btnUtility.Visible = false;
      this.lblMainText.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.attackerDoubleClick), "Reports_Attacker_DClick");
      this.lblSecondaryText.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.defenderDoubleClick), "Reports_Defender_DClick");
    }

    protected override void utilityClick()
    {
      if (this.mapTarget.X == -1)
        return;
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
      InterfaceMgr.Instance.changeTab(0);
      GameEngine.Instance.World.startMultiStageZoom(this.targetZoomLevel, (double) this.mapTarget.X, (double) this.mapTarget.Y);
      this.m_parent.closeControl(true);
    }

    private void attackerDoubleClick()
    {
      if (this.m_returnData == null)
        return;
      Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.m_returnData.attackingVillage);
      double targetZoom = 10000.0;
      if (villageLocation.X == -1)
        return;
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
      InterfaceMgr.Instance.changeTab(0);
      GameEngine.Instance.World.startMultiStageZoom(targetZoom, (double) villageLocation.X, (double) villageLocation.Y);
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
      InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_returnData.attackingVillage, false, true, false, false);
    }

    private void defenderDoubleClick()
    {
      if (this.m_returnData == null)
        return;
      Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.m_returnData.defendingVillage);
      double targetZoom = 10000.0;
      if (villageLocation.X == -1)
        return;
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
      InterfaceMgr.Instance.changeTab(0);
      GameEngine.Instance.World.startMultiStageZoom(targetZoom, (double) villageLocation.X, (double) villageLocation.Y);
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
      InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_returnData.defendingVillage, false, true, false, false);
    }
  }
}
