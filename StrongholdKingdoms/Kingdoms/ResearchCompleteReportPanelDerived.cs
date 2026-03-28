// Decompiled with JetBrains decompiler
// Type: Kingdoms.ResearchCompleteReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;

//#nullable disable
namespace Kingdoms
{
  internal class ResearchCompleteReportPanelDerived : GenericReportPanelBasic
  {
    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      this.lblSecondaryText.Text = ResearchData.getResearchName(returnData.genericData1) + " : " + SK.Text("Reports_Research_Level", "Level") + " : " + (returnData.genericData2 + 1).ToString();
      this.lblSubTitle.Text = SK.Text("Reports_Research_Complete", "Research Complete");
      this.btnUtility.Text.Text = SK.Text("GENERIC_Research", "Research");
      this.btnUtility.Visible = true;
    }

    protected override void utilityClick()
    {
      GameEngine.Instance.playInterfaceSound("ResearchCompleteReportPanel_research");
      InterfaceMgr.Instance.getMainTabBar().changeTab(3);
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
    }
  }
}
