// Decompiled with JetBrains decompiler
// Type: Kingdoms.ViewAllCardsPanel
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
  public class ViewAllCardsPanel : CustomSelfDrawPanel, CustomSelfDrawPanel.ICardsPanel
  {
    private IContainer components;
    private string strOrderNow = SK.Text("BuyCrownsPanel_Order_Now", "Order Now");
    private string strCrowns = SK.Text("BuyCrownsPanel_Crowns", "Crowns");
    private CustomSelfDrawPanel.UICardsButtons cardButtons;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel labelBottom = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel labelFeedback = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage buybutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage managebutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage premiumbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage playbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage crownsbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDFill greyout = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
    private int currentCardSection = -1;
    private static int BorderPadding = 16;
    private int ContentWidth;
    private int AvailablePanelWidth;
    private CustomSelfDrawPanel.CSDExtendingPanel AvailablePanel;
    private CustomSelfDrawPanel.CSDImage AvailablePanelContent = new CustomSelfDrawPanel.CSDImage();
    private CardBarGDI cardsInPlay = new CardBarGDI();
    private CustomSelfDrawPanel.CSDVertScrollBar scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();

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

    public ViewAllCardsPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(int cardSection)
    {
      this.currentCardSection = cardSection;
      this.clearControls();
      this.mainBackgroundImage.Image = GFXLibrary.dummy;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = this.Size;
      this.mainBackgroundImage.Tile = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.ContentWidth = this.Width - 2 * ViewAllCardsPanel.BorderPadding;
      this.AvailablePanelWidth = 800;
      CustomSelfDrawPanel.CSDExtendingPanel control1 = new CustomSelfDrawPanel.CSDExtendingPanel();
      control1.Size = this.Size;
      control1.Position = new Point(0, 0);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control1);
      control1.Create((Image) GFXLibrary.cardpanel_panel_back_top_left, (Image) GFXLibrary.cardpanel_panel_back_top_mid, (Image) GFXLibrary.cardpanel_panel_back_top_right, (Image) GFXLibrary.cardpanel_panel_back_mid_left, (Image) GFXLibrary.cardpanel_panel_back_mid_mid, (Image) GFXLibrary.cardpanel_panel_back_mid_right, (Image) GFXLibrary.cardpanel_panel_back_bottom_left, (Image) GFXLibrary.cardpanel_panel_back_bottom_mid, (Image) GFXLibrary.cardpanel_panel_back_bottom_right);
      CustomSelfDrawPanel.CSDImage control2 = new CustomSelfDrawPanel.CSDImage();
      control2.Image = (Image) GFXLibrary.cardpanel_panel_gradient_top_left;
      control2.Size = GFXLibrary.cardpanel_panel_gradient_top_left.Size;
      control2.Position = new Point(0, 0);
      control1.addControl((CustomSelfDrawPanel.CSDControl) control2);
      CustomSelfDrawPanel.CSDImage control3 = new CustomSelfDrawPanel.CSDImage();
      control3.Image = (Image) GFXLibrary.cardpanel_panel_gradient_bottom_right;
      control3.Size = GFXLibrary.cardpanel_panel_gradient_bottom_right.Size;
      control3.Position = new Point(control1.Width - control3.Width - 6, control1.Height - control3.Height - 6);
      control1.addControl((CustomSelfDrawPanel.CSDControl) control3);
      this.AvailablePanel = new CustomSelfDrawPanel.CSDExtendingPanel();
      this.AvailablePanel.Size = new Size(this.AvailablePanelWidth, 550);
      this.AvailablePanel.Position = new Point(8, this.Height - 8 - 550);
      this.AvailablePanel.Alpha = 0.8f;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.AvailablePanel);
      this.AvailablePanel.Create((Image) GFXLibrary.cardpanel_panel_black_top_left, (Image) GFXLibrary.cardpanel_panel_black_top_mid, (Image) GFXLibrary.cardpanel_panel_black_top_right, (Image) GFXLibrary.cardpanel_panel_black_mid_left, (Image) GFXLibrary.cardpanel_panel_black_mid_mid, (Image) GFXLibrary.cardpanel_panel_black_mid_right, (Image) GFXLibrary.cardpanel_panel_black_bottom_left, (Image) GFXLibrary.cardpanel_panel_black_bottom_mid, (Image) GFXLibrary.cardpanel_panel_black_bottom_right);
      this.cardsInPlay.init(cardSection, 112, false, 14, 3, 0);
      this.cardsInPlay.Position = new Point(0, 5);
      this.AvailablePanel.addControl((CustomSelfDrawPanel.CSDControl) this.cardsInPlay);
      int width1 = this.Width;
      int borderPadding = ViewAllCardsPanel.BorderPadding;
      int width2 = this.AvailablePanel.Width;
      this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
      this.closeImage.Size = this.closeImage.Image.Size;
      this.closeImage.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal));
      this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick));
      this.closeImage.Position = new Point(this.Width - 14 - 17, 10);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeImage);
      CustomSelfDrawPanel.CSDFill control4 = new CustomSelfDrawPanel.CSDFill();
      control4.FillColor = Color.FromArgb((int) byte.MaxValue, 130, 129, 126);
      control4.Size = new Size(this.Width - 10, 1);
      control4.Position = new Point(5, 34);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control4);
      this.greyout.FillColor = Color.FromArgb(215, 25, 25, 25);
      this.greyout.Size = new Size(this.mainBackgroundImage.Width, this.AvailablePanel.Y + this.AvailablePanel.Height);
      this.greyout.Position = new Point(0, 0);
      this.greyout.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => { }));
      CustomSelfDrawPanel.CSDImage closeGrey = new CustomSelfDrawPanel.CSDImage();
      closeGrey.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
      closeGrey.Size = this.closeImage.Image.Size;
      closeGrey.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => closeGrey.Image = (Image) GFXLibrary.cardpanel_button_close_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => closeGrey.Image = (Image) GFXLibrary.cardpanel_button_close_normal));
      closeGrey.Position = new Point(this.Width - 14 - 17, 10);
      this.greyout.addControl((CustomSelfDrawPanel.CSDControl) closeGrey);
      this.labelTitle.Position = new Point(27, 8);
      this.labelTitle.Size = new Size(935, 64);
      this.labelTitle.Text = SK.Text("ViewAllCardsPanel_Cards_In_Play", "Cards In Play");
      this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.labelTitle.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.labelTitle.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelTitle);
      CustomSelfDrawPanel.UICardsButtons control5 = new CustomSelfDrawPanel.UICardsButtons((PlayCardsWindow) this.ParentForm);
      control5.Position = new Point(808, 37);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control5);
      this.cardButtons = control5;
      if (cardSection != 0)
      {
        CustomSelfDrawPanel.CSDButton control6 = new CustomSelfDrawPanel.CSDButton();
        control6.ImageNorm = (Image) GFXLibrary.button_cards_all_normal;
        control6.ImageOver = (Image) GFXLibrary.button_cards_all_over;
        control6.ImageClick = (Image) GFXLibrary.button_cards_all_over;
        control6.Position = new Point(750, 0);
        control6.Text.Text = SK.Text("PlayCardsPanel_All_Your_Cards", "All Your Cards");
        control6.TextYOffset = -3;
        control6.Text.Color = ARGBColors.Black;
        control6.Text.Size = new Size(control6.Size.Width - 45, control6.Size.Height);
        control6.Text.Position = new Point(45, 0);
        control6.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
        control6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showAllCardsClick), "PlayCardsPanel_show_all_cards");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control6);
      }
      CustomSelfDrawPanel.CSDButton control7 = new CustomSelfDrawPanel.CSDButton();
      control7.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
      control7.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
      control7.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
      control7.Position = new Point(580, 515);
      control7.Text.Text = SK.Text("PlayCardsPanel_Return", "Back To Play Cards");
      control7.TextYOffset = -2;
      control7.Text.Color = ARGBColors.Black;
      control7.Text.Font = Program.mySettings.LanguageIdent == "it" || Program.mySettings.LanguageIdent == "tr" ? FontManager.GetFont("Arial", 11f, FontStyle.Bold) : FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.returnClicked), "PlayCardsPanel_Back_To_PlayCards");
      this.AvailablePanel.addControl((CustomSelfDrawPanel.CSDControl) control7);
      CustomSelfDrawPanel.CSDLabel control8 = new CustomSelfDrawPanel.CSDLabel();
      control8.Position = new Point(27, 563);
      control8.Size = new Size(935, 64);
      control8.Text = SK.Text("ViewAllCardsPanel_Cancel", "Click on a Card Circle to cancel that card.");
      control8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control8.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      control8.Color = ARGBColors.White;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control8);
      this.mainBackgroundImage.invalidate();
    }

    public void update() => this.cardsInPlay.update();

    private void closeClick()
    {
      InterfaceMgr.Instance.closePlayCardsWindow();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }

    private void showAllCardsClick()
    {
      ((PlayCardsWindow) this.ParentForm).SetCardSection(0);
      this.init(0);
      this.Invalidate();
    }

    private void returnClicked() => ((PlayCardsWindow) this.ParentForm).SwitchPanel(1);
  }
}
