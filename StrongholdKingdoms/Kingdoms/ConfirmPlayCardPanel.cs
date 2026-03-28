// Decompiled with JetBrains decompiler
// Type: Kingdoms.ConfirmPlayCardPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ConfirmPlayCardPanel : CustomSelfDrawPanel
  {
    private IContainer components;
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDExtendingPanel background = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel confirmLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton confirmButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDCheckBox confirmCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDImage topLeftImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage bottomRightImage = new CustomSelfDrawPanel.CSDImage();
    private ConfirmPlayCardPanel.CardClickPlayDelegate m_callback;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.None;
    }

    public ConfirmPlayCardPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(
      CardTypes.CardDefinition def,
      ConfirmPlayCardPanel.CardClickPlayDelegate callback)
    {
      this.m_callback = callback;
      this.clearControls();
      this.background.Size = this.Size;
      this.background.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
      this.background.Create((Image) GFXLibrary.cardpanel_grey_9slice_left_top, (Image) GFXLibrary.cardpanel_grey_9slice_middle_top, (Image) GFXLibrary.cardpanel_grey_9slice_right_top, (Image) GFXLibrary.cardpanel_grey_9slice_left_middle, (Image) GFXLibrary.cardpanel_grey_9slice_middle_middle, (Image) GFXLibrary.cardpanel_grey_9slice_right_middle, (Image) GFXLibrary.cardpanel_grey_9slice_left_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_middle_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_right_bottom);
      this.topLeftImage.Image = (Image) GFXLibrary.cardpanel_grey_9slice_gradation_top_left;
      this.topLeftImage.Position = new Point(0, 0);
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.topLeftImage);
      this.bottomRightImage.Image = (Image) GFXLibrary.cardpanel_grey_9slice_gradation_bottom;
      this.bottomRightImage.Position = new Point(this.background.Width - this.bottomRightImage.Image.Width, this.background.Height - this.bottomRightImage.Image.Height);
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.bottomRightImage);
      UICard control = BuyCardsPanel.makeUICard(def, RemoteServices.Instance.UserID, 10000);
      GFXLibrary.Instance.closeBigCardsLoader();
      control.Position = new Point(117, 50);
      this.background.addControl((CustomSelfDrawPanel.CSDControl) control);
      this.confirmLabel.Text = SK.Text("ConfirmPlayCardPanel_Are_You_Sure", "Are you sure you want to play this card?");
      this.confirmLabel.Color = ARGBColors.Black;
      this.confirmLabel.Position = new Point(0, 10);
      this.confirmLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.confirmLabel.Size = new Size(this.background.Width, 50);
      this.confirmLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.confirmLabel);
      this.confirmButton.Text.Text = SK.Text("ConfirmPlayCardPanel_Play_Card", "Play Card");
      this.confirmButton.TextYOffset = -2;
      this.confirmButton.Text.Color = ARGBColors.Black;
      this.confirmButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.confirmButton.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
      this.confirmButton.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
      this.confirmButton.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
      this.confirmButton.Position = new Point(230, 310);
      this.confirmButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playCard), "ConfirmPlayCardPanel_confirm_play_card");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.confirmButton);
      this.cancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.cancelButton.TextYOffset = -2;
      this.cancelButton.Text.Color = ARGBColors.Black;
      this.cancelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.cancelButton.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
      this.cancelButton.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
      this.cancelButton.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
      this.cancelButton.Position = new Point(30, 310);
      this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "ConfirmPlayCardPanel_cancel");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.cancelButton);
      this.confirmCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.confirmCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.confirmCheck.Position = new Point(20, 360);
      this.confirmCheck.Checked = true;
      this.confirmCheck.CBLabel.Text = SK.Text("ConfirmPlayCardPanel_Always", "Always confirm playing cards?");
      this.confirmCheck.CBLabel.Color = ARGBColors.Black;
      this.confirmCheck.CBLabel.Position = new Point(20, -1);
      this.confirmCheck.CBLabel.Size = new Size(360, 35);
      this.confirmCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.confirmCheck);
    }

    public void update()
    {
    }

    private void closeClick()
    {
      if (!this.confirmCheck.Checked)
      {
        Program.mySettings.ConfirmPlayCard = false;
        Program.mySettings.Save();
      }
      InterfaceMgr.Instance.closeConfirmPlayCardPopup();
      Form cardWindow = InterfaceMgr.Instance.getCardWindow();
      if (cardWindow == null)
        return;
      cardWindow.TopMost = true;
      cardWindow.TopMost = false;
    }

    private void playCard()
    {
      if (!this.confirmCheck.Checked)
      {
        Program.mySettings.ConfirmPlayCard = false;
        Program.mySettings.Save();
      }
      InterfaceMgr.Instance.closeConfirmPlayCardPopup();
      Form cardWindow = InterfaceMgr.Instance.getCardWindow();
      if (cardWindow != null)
      {
        cardWindow.TopMost = true;
        cardWindow.TopMost = false;
      }
      if (this.m_callback == null)
        return;
      this.m_callback(false, false);
    }

    public delegate void CardClickPlayDelegate(bool fromClick, bool fromValidate);
  }
}
