// Decompiled with JetBrains decompiler
// Type: Kingdoms.ConfirmOpenPackPanel
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
  public class ConfirmOpenPackPanel : CustomSelfDrawPanel
  {
    private NumericUpDown numMultiple;
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDExtendingPanel background = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel confirmLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton confirmButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton minButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton maxButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton middleButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDCheckBox confirmCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDLine left = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine right = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine top = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine bottom = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLabel packTypeLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage topLeftImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage bottomRightImage = new CustomSelfDrawPanel.CSDImage();
    private ConfirmOpenPackPanel.CardClickPlayDelegate m_callback;
    private ConfirmOpenPackPopup m_parent;
    private IContainer components;

    public int Multiple => (int) this.numMultiple.Value;

    public ConfirmOpenPackPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(
      UICardPack pack,
      ConfirmOpenPackPanel.CardClickPlayDelegate callback,
      ConfirmOpenPackPopup parent)
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
      string category = GameEngine.Instance.cardPackManager.ProfileCardOffers[pack.PackIDs[0]].Category;
      int num1 = 0;
      foreach (CardTypes.UserCardPack userCardPack in GameEngine.Instance.cardPackManager.ProfileUserCardPacks.Values)
      {
        if (GameEngine.Instance.cardPackManager.ProfileCardOffers[userCardPack.OfferID].Category == category)
          num1 += userCardPack.Count;
      }
      if (num1 > 10)
        num1 = 10;
      int num2 = num1;
      this.numMultiple = new NumericUpDown();
      this.Controls.Add((Control) this.numMultiple);
      this.numMultiple.Minimum = 1M;
      this.numMultiple.Maximum = (Decimal) num2;
      this.numMultiple.Increment = 1M;
      this.numMultiple.Left = this.Width / 2 - this.numMultiple.Width / 2;
      this.numMultiple.Top = this.Height / 2 - this.numMultiple.Height / 2 - 20;
      this.numMultiple.DecimalPlaces = 0;
      this.numMultiple.KeyUp += new KeyEventHandler(this.numMultiple_KeyUp);
      this.confirmLabel.Color = ARGBColors.Black;
      this.confirmLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.confirmLabel.Text = SK.Text("ConfirmOpenPack_HowMany", "How many packs of this type would you like to open?");
      this.confirmLabel.Position = new Point(20, 10);
      this.confirmLabel.Size = new Size(this.background.Width - 40, 80);
      this.confirmLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.confirmLabel);
      this.packTypeLabel.Text = pack.nameText;
      this.packTypeLabel.Color = ARGBColors.Black;
      this.packTypeLabel.Position = new Point(20, 80);
      this.packTypeLabel.Size = new Size(this.background.Width - 40, 80);
      this.packTypeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.packTypeLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.packTypeLabel);
      this.confirmButton.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
      this.confirmButton.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
      this.confirmButton.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
      this.confirmButton.TextYOffset = -2;
      this.confirmButton.Text.Color = ARGBColors.Black;
      this.confirmButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.confirmButton.Position = new Point(230, 190);
      this.confirmButton.Text.Text = SK.Text("ConfirmOpenPack_OpenPacks", "Open Packs");
      this.confirmButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playCard), "ConfirmOpenPackPanel_confirm_open_pack");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.confirmButton);
      this.minButton.ImageNorm = (Image) GFXLibrary.building_icon_circle;
      this.minButton.ImageOver = (Image) GFXLibrary.building_icon_circle;
      this.minButton.ImageClick = (Image) GFXLibrary.building_icon_circle;
      this.minButton.Position = new Point(this.numMultiple.Left, 135);
      this.minButton.Text.Text = this.numMultiple.Minimum.ToString();
      this.minButton.TextYOffset = -1;
      this.minButton.Text.Color = ARGBColors.Black;
      this.minButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.minButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.minAmount), "SetOpenPackAmount_Minimum");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.minButton);
      this.middleButton.ImageNorm = (Image) GFXLibrary.building_icon_circle;
      this.middleButton.ImageOver = (Image) GFXLibrary.building_icon_circle;
      this.middleButton.ImageClick = (Image) GFXLibrary.building_icon_circle;
      this.middleButton.Position = new Point(this.numMultiple.Left + this.numMultiple.Width / 2 - this.middleButton.Width / 2, 135);
      this.middleButton.TextYOffset = -1;
      this.middleButton.Text.Text = ((int) (this.numMultiple.Minimum + this.numMultiple.Maximum) / 2).ToString();
      this.middleButton.Text.Color = ARGBColors.Black;
      this.middleButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.middleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.middleAmount), "SetOpenPackAmount_Middle");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.middleButton);
      this.maxButton.ImageNorm = (Image) GFXLibrary.building_icon_circle;
      this.maxButton.ImageOver = (Image) GFXLibrary.building_icon_circle;
      this.maxButton.ImageClick = (Image) GFXLibrary.building_icon_circle;
      this.maxButton.Position = new Point(this.numMultiple.Left + this.numMultiple.Width - this.maxButton.Width, 135);
      this.maxButton.TextYOffset = -1;
      this.maxButton.Text.Text = this.numMultiple.Maximum.ToString();
      this.maxButton.Text.Color = ARGBColors.Black;
      this.maxButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.maxButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.maxAmount), "SetOpenPackAmount_Maximum");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.maxButton);
      this.left.Position = new Point(this.numMultiple.Left - 5, this.numMultiple.Top - 5);
      this.left.Height = this.minButton.Position.Y + this.minButton.Height - this.left.Position.Y + 5;
      this.left.LineColor = ARGBColors.Black;
      this.left.Width = 0;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.left);
      this.right.Position = new Point(this.numMultiple.Right + 5, this.numMultiple.Top - 5);
      this.right.Height = this.minButton.Position.Y + this.minButton.Height - this.right.Position.Y + 5;
      this.right.LineColor = ARGBColors.Black;
      this.right.Width = 0;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.right);
      this.top.Position = new Point(this.numMultiple.Left - 5, this.numMultiple.Top - 5);
      this.top.Width = this.right.Position.X - this.left.Position.X;
      this.top.LineColor = ARGBColors.Black;
      this.top.Height = 0;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.top);
      this.bottom.Position = new Point(this.numMultiple.Left - 5, this.minButton.Position.Y + this.minButton.Height + 5);
      this.bottom.Width = this.right.Position.X - this.left.Position.X;
      this.bottom.LineColor = ARGBColors.Black;
      this.bottom.Height = 0;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.bottom);
      this.cancelButton.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
      this.cancelButton.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
      this.cancelButton.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
      this.cancelButton.Position = new Point(30, 190);
      this.cancelButton.TextYOffset = -2;
      this.cancelButton.Text.Color = ARGBColors.Black;
      this.cancelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.cancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "ConfirmOpenPackPanel_cancel");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.cancelButton);
      this.confirmCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.confirmCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.confirmCheck.Position = new Point(20, 360);
      this.confirmCheck.Position = new Point(20, 240);
      this.confirmCheck.Checked = true;
      this.confirmCheck.CBLabel.Text = SK.Text("ConfirmOpenPack_AlwaysAsk", "Always ask to open multiple packs?");
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
        Program.mySettings.OpenMultipleCardPacks = false;
        Program.mySettings.Save();
      }
      InterfaceMgr.Instance.OpenPackMultiple = (int) this.numMultiple.Value;
      InterfaceMgr.Instance.closeConfirmOpenPackPopup();
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
        Program.mySettings.OpenMultipleCardPacks = false;
        Program.mySettings.Save();
      }
      InterfaceMgr.Instance.OpenPackMultiple = (int) this.numMultiple.Value;
      InterfaceMgr.Instance.closeConfirmOpenPackPopup();
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

    private void minAmount() => this.numMultiple.Value = 1M;

    private void middleAmount()
    {
      this.numMultiple.Value = (Decimal) (int) ((this.numMultiple.Minimum + this.numMultiple.Maximum) / 2M);
    }

    private void maxAmount() => this.numMultiple.Value = this.numMultiple.Maximum;

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
