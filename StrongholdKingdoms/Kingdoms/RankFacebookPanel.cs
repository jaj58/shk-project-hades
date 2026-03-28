// Decompiled with JetBrains decompiler
// Type: Kingdoms.RankFacebookPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class RankFacebookPanel : CustomSelfDrawPanel
  {
    public static bool shareClicked;
    private RankFacebookPopup m_parent;
    private CustomSelfDrawPanel.CSDLabel mainLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton facebookShareButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();

    public RankFacebookPanel()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      RankFacebookPanel.shareClicked = false;
    }

    public void init(RankFacebookPopup parent)
    {
      this.m_parent = parent;
      this.Size = this.m_parent.Size;
      this.BackColor = ARGBColors.Transparent;
      CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage();
      control.Alpha = 0.1f;
      control.Image = (Image) GFXLibrary.formations_img;
      control.Scale = 5.0;
      control.Position = new Point(0, 0);
      control.Size = this.Size;
      this.addControl((CustomSelfDrawPanel.CSDControl) control);
      this.mainLabel.Text = SK.Text("FACEBOOK_SHARE_Info_Body", "Congratulations on Reaching Rank 10 (Thane). Share this achievement on Facebook and receive a free Random Card Pack!");
      this.mainLabel.Color = ARGBColors.Black;
      this.mainLabel.Position = new Point(10, 0);
      this.mainLabel.Size = new Size(430, 75);
      this.mainLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
      this.mainLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.mainLabel);
      this.facebookShareButton.ImageNorm = (Image) GFXLibrary.facebookBrownNorm;
      this.facebookShareButton.ImageOver = (Image) GFXLibrary.facebookBrownOver;
      this.facebookShareButton.ImageClick = (Image) GFXLibrary.facebookBrownClick;
      this.facebookShareButton.Position = new Point(20, 80);
      this.facebookShareButton.UseTextSize = true;
      this.facebookShareButton.Text.Text = SK.Text("FACEBOOK_Share", "Share");
      this.facebookShareButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.facebookShareButton.Text.Position = new Point(20, 2);
      this.facebookShareButton.Text.Size = new Size(110, 21);
      this.facebookShareButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.facebookShareButton.TextYOffset = 0;
      this.facebookShareButton.Text.Color = ARGBColors.Black;
      this.facebookShareButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.facebookShareClicked));
      control.addControl((CustomSelfDrawPanel.CSDControl) this.facebookShareButton);
      this.closeButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      this.closeButton.Position = new Point(290, 80);
      this.closeButton.Text.Text = SK.Text("GENERIC_Close", "Close");
      this.closeButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.closeButton.TextYOffset = -3;
      this.closeButton.Text.Color = ARGBColors.Black;
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick));
      control.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
    }

    private void facebookShareClicked()
    {
      RankFacebookPanel.shareClicked = true;
      this.closeClick();
    }

    private void closeClick()
    {
      if (this.m_parent == null)
        return;
      this.m_parent.Close();
    }
  }
}
