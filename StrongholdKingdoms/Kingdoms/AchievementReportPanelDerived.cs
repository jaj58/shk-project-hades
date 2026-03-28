// Decompiled with JetBrains decompiler
// Type: Kingdoms.AchievementReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  internal class AchievementReportPanelDerived : GenericReportPanelBasic
  {
    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      this.lblSecondaryText.Text = CustomTooltipManager.getAchievementTitle(returnData.genericData1) + " - " + CustomTooltipManager.getAchievementRank(returnData.genericData1);
      this.lblSubTitle.Text = SK.Text("ReportsPanel_Achievement_Attained", "Achievement Attained");
      this.imgFurther.Image = (Image) GFXLibrary.com_32_honour_DS;
      this.imgFurther.setSizeToImage();
      this.imgFurther.Position = new Point(this.Width / 2 - this.imgFurther.Width, this.btnDelete.Position.Y);
      this.lblFurther.Text = this.m_returnData.genericData2.ToString("N", (IFormatProvider) this.nfi);
      this.lblFurther.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblFurther.Size = new Size(this.Width, this.imgFurther.Height);
      this.lblFurther.Position = new Point(this.imgFurther.Rectangle.Right + 10, this.imgFurther.Position.Y);
      this.showFurtherInfo();
      this.btnUtility.Visible = true;
      this.btnUtility.Text.Text = SK.Text("GENERIC_Achievements", "Achievements");
    }

    protected override void utilityClick()
    {
      GameEngine.Instance.playInterfaceSound("AchievementReportPanel_achievements");
      InterfaceMgr.Instance.getMainTabBar().changeTab(4);
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
    }
  }
}
