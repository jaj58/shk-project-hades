// Decompiled with JetBrains decompiler
// Type: Kingdoms.VillageLostReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;

//#nullable disable
namespace Kingdoms
{
  internal class VillageLostReportPanelDerived : GenericReportPanelBasic
  {
    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      this.lblMainText.Text = SK.Text("Reports_VillageLost", "Village Lost");
      if (returnData.otherUser.Length == 0)
        this.lblSubTitle.Text = SK.Text("Reports_VillageLost_inactivity", "Village Lost due to Inactivity");
      else if (returnData.reportType == (short) 128)
        this.lblSubTitle.Text = SK.Text("Reports_VillageLost_abandoned", "Village Abandoned");
      else
        this.lblSubTitle.Text = SK.Text("Reports_VillageLost_attacked by", "Attacked By") + " : " + returnData.otherUser;
      this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
    }
  }
}
