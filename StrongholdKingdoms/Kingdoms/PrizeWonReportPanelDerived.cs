// Decompiled with JetBrains decompiler
// Type: Kingdoms.PrizeWonReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  internal class PrizeWonReportPanelDerived : GenericReportPanelBasic
  {
    private int m_prizeID;
    private int m_contestID;

    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      this.m_prizeID = returnData.genericData1;
      this.m_contestID = returnData.genericData2;
      this.lblDate.Position = new Point(0, this.lblSubTitle.Rectangle.Bottom);
      this.lblMainText.Text = SK.Text("Reports_Prize_Won", "Prize Won!");
      if (GameEngine.Instance.World.pendingPrizes == null || GameEngine.Instance.World.pendingPrizes.Count <= 0)
        return;
      CustomSelfDrawPanel.CSDButton control = new CustomSelfDrawPanel.CSDButton();
      control.ImageNorm = (Image) GFXLibrary.button_132_normal_gold;
      control.ImageOver = (Image) GFXLibrary.button_132_over_gold;
      control.ImageClick = (Image) GFXLibrary.button_132_in_gold;
      control.setSizeToImage();
      control.Position = new Point(this.Width / 2 - control.Width / 2, this.btnClose.Y);
      control.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      control.TextYOffset = -2;
      control.Text.Color = ARGBColors.Black;
      control.Enabled = true;
      control.Visible = true;
      control.Text.Text = SK.Text("Event_Prizes_Header", "Prizes");
      control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showPrizesClick), "Reports_Show_Prizes");
      this.addControl((CustomSelfDrawPanel.CSDControl) control);
    }

    private void showPrizesClick() => PrizeClaimWindow.CreatePrizeClaimWindow();
  }
}
