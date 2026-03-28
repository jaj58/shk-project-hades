// Decompiled with JetBrains decompiler
// Type: Kingdoms.TradeReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  internal class TradeReportPanelDerived : GenericReportPanelBasic
  {
    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      switch (returnData.reportType)
      {
        case 73:
          this.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Sent_Resources_To", "Has sent resources to");
          this.lblSecondaryText.Text = this.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          break;
        case 78:
          this.lblMainText.Text = GameEngine.Instance.World.getVillageName(returnData.attackingVillage);
          this.lblSubTitle.Text = SK.Text("Report_Auto_Sent_Resources_To", "Has Auto-Sent resources to");
          this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
          break;
      }
      this.imgFurther.Image = (Image) GFXLibrary.getCommodity32Image(this.m_returnData.genericData1);
      this.imgFurther.setSizeToImage();
      this.imgFurther.Position = new Point(this.Width / 2 - this.imgFurther.Width, this.btnDelete.Position.Y);
      this.lblFurther.Text = this.m_returnData.genericData2.ToString("N", (IFormatProvider) this.nfi);
      this.lblFurther.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblFurther.Size = new Size(this.Width, this.imgFurther.Height);
      this.lblFurther.Position = new Point(this.imgFurther.Rectangle.Right + 10, this.imgFurther.Position.Y);
      this.showFurtherInfo();
    }
  }
}
