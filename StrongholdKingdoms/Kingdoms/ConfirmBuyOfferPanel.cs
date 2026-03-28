// Decompiled with JetBrains decompiler
// Type: Kingdoms.ConfirmBuyOfferPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ConfirmBuyOfferPanel : CustomSelfDrawPanel
  {
    private NumericUpDown numMultiple;
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDExtendingPanel background = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel confirmLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton confirmButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDCheckBox confirmCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDLabel packTypeLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage topLeftImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage bottomRightImage = new CustomSelfDrawPanel.CSDImage();
    private ConfirmBuyOfferPanel.CardClickPlayDelegate m_callback;
    private ConfirmBuyOfferPopup m_parent;
    private IContainer components;

    public int Multiple => (int) this.numMultiple.Value;

    public ConfirmBuyOfferPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(
      CardTypes.CardOffer offer,
      string name,
      ConfirmBuyOfferPanel.CardClickPlayDelegate callback,
      ConfirmBuyOfferPopup parent)
    {
      this.m_parent = parent;
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
      int num = (int) Math.Floor((Decimal) (GameEngine.Instance.World.ProfileCrowns / offer.CrownCost));
      this.numMultiple = new NumericUpDown();
      this.Controls.Add((Control) this.numMultiple);
      this.numMultiple.Minimum = 1M;
      this.numMultiple.Maximum = (Decimal) num;
      this.numMultiple.Increment = 1M;
      this.numMultiple.Left = this.Width / 2 - this.numMultiple.Width / 2;
      this.numMultiple.Top = this.Height / 2 - this.numMultiple.Height / 2;
      this.numMultiple.DecimalPlaces = 0;
      this.numMultiple.KeyUp += new KeyEventHandler(this.numMultiple_KeyUp);
      this.confirmLabel.Color = ARGBColors.Black;
      this.confirmLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.confirmLabel.Position = new Point(20, 10);
      this.confirmLabel.Size = new Size(this.background.Width - 40, 80);
      this.confirmLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.confirmLabel.Text = SK.Text("ConfirmBuyOffer_PleaseConfirm", "Please Confirm how many of this type of card pack you want to buy.");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.confirmLabel);
      this.packTypeLabel.Text = name;
      this.packTypeLabel.Position = new Point(20, 100);
      this.packTypeLabel.Color = ARGBColors.Black;
      this.packTypeLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.packTypeLabel.Size = new Size(this.background.Width - 40, 80);
      this.packTypeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.packTypeLabel);
      this.confirmButton.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
      this.confirmButton.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
      this.confirmButton.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
      this.confirmButton.Position = new Point(230, 190);
      this.confirmButton.TextYOffset = -2;
      this.confirmButton.Text.Color = ARGBColors.Black;
      this.confirmButton.Text.Font = !(Program.mySettings.LanguageIdent == "pl") ? FontManager.GetFont("Arial", 12f, FontStyle.Bold) : FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.confirmButton.Text.Text = SK.Text("ConfirmBuyOffer_BuyOffer", "Buy Offer");
      this.confirmButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playCard), "ConfirmBuyOfferPanel_confirm_buy_pack");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.confirmButton);
      this.cancelButton.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
      this.cancelButton.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
      this.cancelButton.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
      this.cancelButton.Position = new Point(30, 190);
      this.cancelButton.TextYOffset = -2;
      this.cancelButton.Text.Color = ARGBColors.Black;
      this.cancelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.cancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "ConfirmBuyOfferPanel_cancel");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.cancelButton);
      this.confirmCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.confirmCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.confirmCheck.Position = new Point(20, 240);
      this.confirmCheck.Checked = true;
      this.confirmCheck.CBLabel.Text = SK.Text("ConfirmBuyOffer_AlwaysAsk", "Always ask to buy multiple card packs.");
      this.confirmCheck.CBLabel.Color = ARGBColors.Black;
      this.confirmCheck.CBLabel.Position = new Point(20, -1);
      this.confirmCheck.CBLabel.Size = new Size(360, 35);
      this.confirmCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.confirmCheck);
    }

    private void numMultiple_KeyUp(object sender, KeyEventArgs e)
    {
      try
      {
        if (!((Decimal) int.Parse(this.numMultiple.Text) < this.numMultiple.Minimum) && !((Decimal) int.Parse(this.numMultiple.Text) > this.numMultiple.Maximum))
          return;
        this.numMultiple.Text = "";
        this.numMultiple.Value = this.numMultiple.Minimum;
      }
      catch (Exception ex)
      {
        this.numMultiple.Text = "";
        this.numMultiple.Value = this.numMultiple.Minimum;
      }
    }

    public void update()
    {
    }

    private void closeClick()
    {
      if (!this.confirmCheck.Checked)
      {
        Program.mySettings.BuyMultipleCardPacks = false;
        Program.mySettings.Save();
      }
      InterfaceMgr.Instance.closeConfirmBuyOfferPopup();
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
        Program.mySettings.BuyMultipleCardPacks = false;
        Program.mySettings.Save();
      }
      InterfaceMgr.Instance.BuyOfferMultiple = (int) this.numMultiple.Value;
      InterfaceMgr.Instance.closeConfirmBuyOfferPopup();
      Form cardWindow = InterfaceMgr.Instance.getCardWindow();
      if (cardWindow != null)
      {
        cardWindow.TopMost = true;
        cardWindow.TopMost = false;
      }
      if (this.m_callback == null)
        return;
      this.m_callback(false);
    }

    private void clickPlus() => ++this.numMultiple.Value;

    private void clickMinus() => --this.numMultiple.Value;

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

    public delegate void CardClickPlayDelegate(bool fromClick);
  }
}
