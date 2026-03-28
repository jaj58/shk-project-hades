// Decompiled with JetBrains decompiler
// Type: Kingdoms.LogoutPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using StatTracking;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class LogoutPanel : CustomSelfDrawPanel
  {
    private const int CHECK_HORZ_SPACING = 85;
    private IContainer components;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel labelCrowns = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDCheckBox tradingCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox scoutingCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox attackCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox recruitCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox repairCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox transferCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox attackCheck_Bandits = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox attackCheck_Wolves = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox attackCheck_AI = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox recruitCheck_Peasants = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox recruitCheck_Archers = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox recruitCheck_Pikemen = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox recruitCheck_Swordsmen = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox recruitCheck_Catapults = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDTrackBar recruitTrackBar_Peasants = new CustomSelfDrawPanel.CSDTrackBar();
    private CustomSelfDrawPanel.CSDTrackBar recruitTrackBar_Archers = new CustomSelfDrawPanel.CSDTrackBar();
    private CustomSelfDrawPanel.CSDTrackBar recruitTrackBar_Pikemen = new CustomSelfDrawPanel.CSDTrackBar();
    private CustomSelfDrawPanel.CSDTrackBar recruitTrackBar_Swordsmen = new CustomSelfDrawPanel.CSDTrackBar();
    private CustomSelfDrawPanel.CSDTrackBar recruitTrackBar_Catapults = new CustomSelfDrawPanel.CSDTrackBar();
    private CustomSelfDrawPanel.CSDLabel recruitNumber_Peasants = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel recruitNumber_Archers = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel recruitNumber_Pikemen = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel recruitNumber_Swordsmen = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel recruitNumber_Catapults = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel recruitmentInfoLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDArea tradingArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea scoutingArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea attackArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea recruitArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea repairArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea transferArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDButton tradingCircleButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage tradingResourceImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDTrackBar tradingTrackBar = new CustomSelfDrawPanel.CSDTrackBar();
    private CustomSelfDrawPanel.CSDLabel tradingPercentLabel = new CustomSelfDrawPanel.CSDLabel();
    private static Image hrImage;
    private bool premium;
    private bool m_normalLogout = true;
    private bool logoutPressed;
    private SelectTradingResourcePopup m_resourcePopup;

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

    public LogoutPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool normalLogout, bool advertOnly)
    {
      this.m_normalLogout = normalLogout;
      this.clearControls();
      this.mainBackgroundImage.Image = GFXLibrary.dummy;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = this.Size;
      this.mainBackgroundImage.Tile = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
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
      if (LogoutPanel.hrImage == null)
      {
        LogoutPanel.hrImage = (Image) new Bitmap(this.Width - 10, 1);
        using (Graphics graphics = Graphics.FromImage(LogoutPanel.hrImage))
          graphics.Clear(Color.FromArgb((int) byte.MaxValue, 130, 129, 126));
      }
      CustomSelfDrawPanel.CSDImage control4 = new CustomSelfDrawPanel.CSDImage();
      control4.Image = LogoutPanel.hrImage;
      control4.Size = LogoutPanel.hrImage.Size;
      control4.Position = new Point(5, 34);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control4);
      this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
      this.closeImage.Size = this.closeImage.Image.Size;
      this.closeImage.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal));
      this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "LogoutPanel_close");
      this.closeImage.Position = new Point(this.Width - 14 - 17, 10);
      this.closeImage.CustomTooltipID = 1400;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeImage);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage, 41, new Point(this.Width - 14 - 17 - 50 + 3, 5), true);
      CustomSelfDrawPanel.CSDImage control5 = new CustomSelfDrawPanel.CSDImage();
      control5.Image = (Image) GFXLibrary.logout_background_lhs;
      control5.Position = new Point(4, 40);
      control1.addControl((CustomSelfDrawPanel.CSDControl) control5);
      this.labelTitle.Position = new Point(27, 8);
      this.labelTitle.Size = new Size(600, 64);
      this.labelTitle.Text = !advertOnly ? SK.Text("LogoutPanel_Logout", "Logout") : SK.Text("LogoutPanel_Expiration", "Premium Token Expired");
      this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.labelTitle.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.labelTitle.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelTitle);
      NumberFormatInfo nfi = GameEngine.NFI;
      this.labelCrowns.Position = new Point(0, 8);
      this.labelCrowns.Size = new Size(900, 64);
      this.labelCrowns.Text = SK.Text("LogoutPanel_Crowns_In_Treasury", "Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString("N", (IFormatProvider) nfi);
      this.labelCrowns.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.labelCrowns.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.labelCrowns.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelCrowns);
      CardData userCardData = GameEngine.Instance.cardsManager.UserCardData;
      if (userCardData.premiumCard == 0 || GameEngine.Instance.World.WorldEnded)
      {
        this.premium = false;
        CustomSelfDrawPanel.CSDButton control6 = new CustomSelfDrawPanel.CSDButton();
        control6.ImageNorm = (Image) GFXLibrary.logout_ad_1premfor30crown_01;
        control6.ImageOver = (Image) GFXLibrary.logout_ad_1premfor30crown_01_over;
        control6.Position = new Point(375, 50);
        control6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cardsClicked), "LogoutPanel_premium");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control6);
        int num = 35;
        int y = 53;
        CustomSelfDrawPanel.CSDExtendingPanel control7 = new CustomSelfDrawPanel.CSDExtendingPanel();
        control7.Size = new Size(594, 356);
        control7.Position = new Point(control5.Position.X + 372, control5.Position.Y + 76 + 19);
        control7.Alpha = 0.1f;
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control7);
        control7.Create((Image) GFXLibrary.cardpanel_panel_black_top_left, (Image) GFXLibrary.cardpanel_panel_black_top_mid, (Image) GFXLibrary.cardpanel_panel_black_top_right, (Image) GFXLibrary.cardpanel_panel_black_mid_left, (Image) GFXLibrary.cardpanel_panel_black_mid_mid, (Image) GFXLibrary.cardpanel_panel_black_mid_right, (Image) GFXLibrary.cardpanel_panel_black_bottom_left, (Image) GFXLibrary.cardpanel_panel_black_bottom_mid, (Image) GFXLibrary.cardpanel_panel_black_bottom_right);
        CustomSelfDrawPanel.CSDLabel control8 = new CustomSelfDrawPanel.CSDLabel();
        control8.Text = SK.Text("LogoutPanel_Premium_1", "With premium you command");
        control8.Position = new Point(0, 5);
        control8.Color = ARGBColors.Black;
        control8.Size = new Size(control7.Width, 50);
        control8.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
        control8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        control7.addControl((CustomSelfDrawPanel.CSDControl) control8);
        CustomSelfDrawPanel.CSDLabel control9 = new CustomSelfDrawPanel.CSDLabel();
        control9.Text = SK.Text("LogoutPanel_Premium_2", "even when you are offline!");
        control9.Position = new Point(0, 33);
        control9.Color = ARGBColors.Black;
        control9.Size = new Size(control7.Width, 50);
        control9.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
        control9.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        control7.addControl((CustomSelfDrawPanel.CSDControl) control9);
        CustomSelfDrawPanel.CSDLabel control10 = new CustomSelfDrawPanel.CSDLabel();
        control10.Text = SK.Text("LogoutPanel_BuildQueue_1", "Build-queue, build up to 5 buildings in your village at one time.");
        control10.Position = new Point(65, y);
        control10.Color = ARGBColors.Black;
        control10.Size = new Size(control7.Width - 75, 50);
        control10.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        control10.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        control7.addControl((CustomSelfDrawPanel.CSDControl) control10);
        CustomSelfDrawPanel.CSDLabel control11 = new CustomSelfDrawPanel.CSDLabel();
        control11.Text = SK.Text("LogoutPanel_BuildQueue_2", "Research queue, 5 more items can be added to your research queue.");
        control11.Position = new Point(65, y + num);
        control11.Color = ARGBColors.Black;
        control11.Size = new Size(control7.Width - 75, 50);
        control11.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        control11.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        control7.addControl((CustomSelfDrawPanel.CSDControl) control11);
        CustomSelfDrawPanel.CSDLabel control12 = new CustomSelfDrawPanel.CSDLabel();
        control12.Text = SK.Text("LogoutPanel_Scouting_1", "Use Auto scouting to forage for goods.");
        control12.Position = new Point(65, y + num * 2);
        control12.Color = ARGBColors.Black;
        control12.Size = new Size(control7.Width - 75, 50);
        control12.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        control12.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        control7.addControl((CustomSelfDrawPanel.CSDControl) control12);
        CustomSelfDrawPanel.CSDLabel control13 = new CustomSelfDrawPanel.CSDLabel();
        control13.Text = SK.Text("LogoutPanel_Scouting_2", "Auto Trade - lets you set and trade the surplus of one goods type.");
        control13.Position = new Point(65, y + num * 3);
        control13.Color = ARGBColors.Black;
        control13.Size = new Size(control7.Width - 75, 50);
        control13.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        control13.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        control7.addControl((CustomSelfDrawPanel.CSDControl) control13);
        CustomSelfDrawPanel.CSDLabel control14 = new CustomSelfDrawPanel.CSDLabel();
        control14.Text = SK.Text("LogoutPanel_Attacks_1", "Specify targets and Auto Attack will dispatch your armies.");
        control14.Position = new Point(65, y + num * 4);
        control14.Color = ARGBColors.Black;
        control14.Size = new Size(control7.Width - 75, 50);
        control14.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        control14.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        control7.addControl((CustomSelfDrawPanel.CSDControl) control14);
        CustomSelfDrawPanel.CSDLabel control15 = new CustomSelfDrawPanel.CSDLabel();
        control15.Text = SK.Text("LogoutPanel_Attacks_2", "Keep your army topped up with Auto Recruit.");
        control15.Position = new Point(65, y + num * 5);
        control15.Color = ARGBColors.Black;
        control15.Size = new Size(control7.Width - 75, 50);
        control15.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        control15.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        control7.addControl((CustomSelfDrawPanel.CSDControl) control15);
        CustomSelfDrawPanel.CSDLabel control16 = new CustomSelfDrawPanel.CSDLabel();
        control16.Text = SK.Text("LogoutPanel_Overview", "Keep track of key stats across all your villages with the Village Overview.");
        control16.Position = new Point(65, y + num * 6);
        control16.Color = ARGBColors.Black;
        control16.Size = new Size(control7.Width - 75, 50);
        control16.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        control16.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        control7.addControl((CustomSelfDrawPanel.CSDControl) control16);
        CustomSelfDrawPanel.CSDImage control17 = new CustomSelfDrawPanel.CSDImage();
        control17.Image = (Image) GFXLibrary.icon_building;
        control17.Position = new Point(18, y + 7);
        control7.addControl((CustomSelfDrawPanel.CSDControl) control17);
        CustomSelfDrawPanel.CSDImage control18 = new CustomSelfDrawPanel.CSDImage();
        control18.Image = (Image) GFXLibrary.icon_research;
        control18.Position = new Point(18, y + 7 + num);
        control7.addControl((CustomSelfDrawPanel.CSDControl) control18);
        CustomSelfDrawPanel.CSDImage control19 = new CustomSelfDrawPanel.CSDImage();
        control19.Image = (Image) GFXLibrary.wl_moving_unit_icons[2];
        control19.Position = new Point(15, y + 5 + num * 2);
        control7.addControl((CustomSelfDrawPanel.CSDControl) control19);
        CustomSelfDrawPanel.CSDImage control20 = new CustomSelfDrawPanel.CSDImage();
        control20.Image = (Image) GFXLibrary.wl_moving_unit_icons[1];
        control20.Position = new Point(15, y + 5 + num * 3);
        control7.addControl((CustomSelfDrawPanel.CSDControl) control20);
        CustomSelfDrawPanel.CSDImage control21 = new CustomSelfDrawPanel.CSDImage();
        control21.Image = (Image) GFXLibrary.wl_moving_unit_icons[0];
        control21.Position = new Point(15, y + 5 + num * 4);
        control7.addControl((CustomSelfDrawPanel.CSDControl) control21);
        CustomSelfDrawPanel.CSDImage control22 = new CustomSelfDrawPanel.CSDImage();
        control22.Image = (Image) GFXLibrary.wl_moving_unit_icons[0];
        control22.Position = new Point(15, y + 5 + num * 5);
        control7.addControl((CustomSelfDrawPanel.CSDControl) control22);
        CustomSelfDrawPanel.CSDImage control23 = new CustomSelfDrawPanel.CSDImage();
        control23.Image = (Image) GFXLibrary.wl_moving_unit_icons[4];
        control23.Position = new Point(15, y + 5 + num * 6);
        control7.addControl((CustomSelfDrawPanel.CSDControl) control23);
      }
      else
      {
        this.premium = true;
        CustomSelfDrawPanel.CSDExtendingPanel control24 = new CustomSelfDrawPanel.CSDExtendingPanel();
        control24.Size = new Size(594, 432);
        control24.Alpha = 0.1f;
        control24.Position = new Point(control5.Position.X + 372, control5.Position.Y + 20);
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control24);
        control24.Create((Image) GFXLibrary.cardpanel_panel_black_top_left, (Image) GFXLibrary.cardpanel_panel_black_top_mid, (Image) GFXLibrary.cardpanel_panel_black_top_right, (Image) GFXLibrary.cardpanel_panel_black_mid_left, (Image) GFXLibrary.cardpanel_panel_black_mid_mid, (Image) GFXLibrary.cardpanel_panel_black_mid_right, (Image) GFXLibrary.cardpanel_panel_black_bottom_left, (Image) GFXLibrary.cardpanel_panel_black_bottom_mid, (Image) GFXLibrary.cardpanel_panel_black_bottom_right);
        CustomSelfDrawPanel.CSDImage control25 = new CustomSelfDrawPanel.CSDImage();
        control25.Image = userCardData.premiumCard != 4114 ? (userCardData.premiumCard != 4113 ? (userCardData.premiumCard != 4116 ? (Image) GFXLibrary.logout_premium_token : (Image) GFXLibrary.logout_premium_token_extendable) : (Image) GFXLibrary.logout_premium_token_2) : (Image) GFXLibrary.logout_premium_token_30;
        control25.Position = new Point(-8, -8);
        control25.CustomTooltipID = 1421;
        control5.addControl((CustomSelfDrawPanel.CSDControl) control25);
        CustomSelfDrawPanel.CSDImage control26 = new CustomSelfDrawPanel.CSDImage();
        control26.Image = (Image) GFXLibrary.logout_gradation_band;
        control26.Position = new Point(38, 30);
        control24.addControl((CustomSelfDrawPanel.CSDControl) control26);
        CustomSelfDrawPanel.CSDImage control27 = new CustomSelfDrawPanel.CSDImage();
        control27.Image = (Image) GFXLibrary.wl_moving_unit_icons[1];
        control27.Position = new Point(-4, -4);
        control27.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() =>
        {
          this.tradingCheck.Checked = !this.tradingCheck.Checked;
          this.tradingToggled();
        }), "Generic_check_box_toggled");
        control27.CustomTooltipID = 1401;
        control26.addControl((CustomSelfDrawPanel.CSDControl) control27);
        this.tradingCheck.Position = new Point(-30, 2);
        this.tradingCheck.CheckedImage = (Image) GFXLibrary.checkbox_checked;
        this.tradingCheck.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
        this.tradingCheck.Checked = RemoteServices.Instance.UserOptions.autoTrade;
        this.tradingCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.tradingToggled));
        this.tradingCheck.CustomTooltipID = 1401;
        control26.addControl((CustomSelfDrawPanel.CSDControl) this.tradingCheck);
        CustomSelfDrawPanel.CSDLabel control28 = new CustomSelfDrawPanel.CSDLabel();
        control28.Text = SK.Text("LogoutPanel_Auto_Trading", "Auto Trading");
        control28.Position = new Point(40, 0);
        control28.Color = ARGBColors.Black;
        control28.Size = new Size(140, control26.Height);
        control28.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        control28.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        control26.addControl((CustomSelfDrawPanel.CSDControl) control28);
        this.tradingArea.Position = new Point(135, -20);
        this.tradingArea.Size = new Size(428, control26.Height + 41);
        this.tradingArea.Visible = this.tradingCheck.Checked;
        control26.addControl((CustomSelfDrawPanel.CSDControl) this.tradingArea);
        int resource = RemoteServices.Instance.UserOptions.autoTradeResource;
        if (resource == -1)
          resource = 6;
        this.tradingCircleButton.ImageNorm = (Image) GFXLibrary.logout_bits[7];
        this.tradingCircleButton.ImageOver = (Image) GFXLibrary.logout_bits[8];
        this.tradingCircleButton.Position = new Point(0, 1);
        this.tradingCircleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tradingResourceClicked), "LogoutPanel_resources");
        this.tradingCircleButton.CustomTooltipID = 1407;
        this.tradingCircleButton.CustomTooltipData = resource;
        this.tradingArea.addControl((CustomSelfDrawPanel.CSDControl) this.tradingCircleButton);
        this.tradingResourceImage.Image = (Image) GFXLibrary.getCommodity64DSImage(resource);
        this.tradingResourceImage.Size = new Size(69, 69);
        this.tradingResourceImage.Data = resource;
        this.tradingResourceImage.Position = new Point(0, 0);
        this.tradingCircleButton.addControl((CustomSelfDrawPanel.CSDControl) this.tradingResourceImage);
        this.tradingTrackBar.Position = new Point(215, 25);
        this.tradingTrackBar.Margin = new Rectangle(73, -4, 0, 0);
        this.tradingTrackBar.Max = 100;
        this.tradingTrackBar.Value = RemoteServices.Instance.UserOptions.autoTradePercent;
        this.tradingTrackBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
        this.tradingArea.addControl((CustomSelfDrawPanel.CSDControl) this.tradingTrackBar);
        this.tradingTrackBar.CustomTooltipID = 1408;
        this.tradingTrackBar.Create((Image) GFXLibrary.logout_slider_back, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb);
        this.tradingPercentLabel.Text = "0%";
        this.tradingPercentLabel.Position = new Point(0, 0);
        this.tradingPercentLabel.Color = ARGBColors.Black;
        this.tradingPercentLabel.Size = new Size(58, 23);
        this.tradingPercentLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
        this.tradingPercentLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.tradingTrackBar.addControl((CustomSelfDrawPanel.CSDControl) this.tradingPercentLabel);
        CustomSelfDrawPanel.CSDLabel control29 = new CustomSelfDrawPanel.CSDLabel();
        control29.Text = SK.Text("LogoutPanel_Trade_Over", "Trade Over");
        control29.Position = new Point(0, 0);
        control29.Color = ARGBColors.Black;
        control29.Size = new Size(210, this.tradingArea.Height);
        control29.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        control29.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.tradingArea.addControl((CustomSelfDrawPanel.CSDControl) control29);
        this.tracksMoved();
        CustomSelfDrawPanel.CSDImage control30 = new CustomSelfDrawPanel.CSDImage();
        control30.Image = (Image) GFXLibrary.logout_gradation_band;
        control30.Position = new Point(38, 100);
        control24.addControl((CustomSelfDrawPanel.CSDControl) control30);
        CustomSelfDrawPanel.CSDImage control31 = new CustomSelfDrawPanel.CSDImage();
        control31.Image = (Image) GFXLibrary.wl_moving_unit_icons[2];
        control31.Position = new Point(-4, -4);
        control31.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() =>
        {
          this.scoutingCheck.Checked = !this.scoutingCheck.Checked;
          this.scoutingToggled();
        }), "Generic_check_box_toggled");
        control31.CustomTooltipID = 1402;
        control30.addControl((CustomSelfDrawPanel.CSDControl) control31);
        this.scoutingCheck.Position = new Point(-30, 2);
        this.scoutingCheck.CheckedImage = (Image) GFXLibrary.checkbox_checked;
        this.scoutingCheck.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
        this.scoutingCheck.Checked = RemoteServices.Instance.UserOptions.autoScout;
        this.scoutingCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.scoutingToggled));
        this.scoutingCheck.CustomTooltipID = 1402;
        control30.addControl((CustomSelfDrawPanel.CSDControl) this.scoutingCheck);
        CustomSelfDrawPanel.CSDLabel control32 = new CustomSelfDrawPanel.CSDLabel();
        control32.Text = SK.Text("LogoutPanel_Auto_Scouting", "Auto Scouting");
        control32.Position = new Point(40, 0);
        control32.Color = ARGBColors.Black;
        control32.Size = new Size(140, control30.Height);
        control32.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        control32.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        control30.addControl((CustomSelfDrawPanel.CSDControl) control32);
        this.scoutingArea.Position = new Point(135, -20);
        this.scoutingArea.Size = new Size(428, control30.Height + 41);
        this.scoutingArea.Visible = this.scoutingCheck.Checked;
        control30.addControl((CustomSelfDrawPanel.CSDControl) this.scoutingArea);
        CustomSelfDrawPanel.CSDLabel control33 = new CustomSelfDrawPanel.CSDLabel();
        control33.Text = SK.Text("LogoutPanel_Auto_Scouting2", "Scout within your Parishes");
        control33.Position = new Point(0, 0);
        control33.Color = ARGBColors.Black;
        control33.Size = new Size(398, this.scoutingArea.Height);
        control33.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        control33.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.scoutingArea.addControl((CustomSelfDrawPanel.CSDControl) control33);
        CustomSelfDrawPanel.CSDImage control34 = new CustomSelfDrawPanel.CSDImage();
        control34.Image = (Image) GFXLibrary.logout_bits[14];
        control34.Position = new Point(0, 1);
        this.scoutingArea.addControl((CustomSelfDrawPanel.CSDControl) control34);
        CustomSelfDrawPanel.CSDImage control35 = new CustomSelfDrawPanel.CSDImage();
        control35.Image = (Image) GFXLibrary.logout_gradation_band;
        control35.Position = new Point(38, 170);
        control24.addControl((CustomSelfDrawPanel.CSDControl) control35);
        CustomSelfDrawPanel.CSDImage control36 = new CustomSelfDrawPanel.CSDImage();
        control36.Image = (Image) GFXLibrary.wl_moving_unit_icons[24];
        control36.Position = new Point(-4, -4);
        control36.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() =>
        {
          this.attackCheck.Checked = !this.attackCheck.Checked;
          this.attackToggled();
        }), "Generic_check_box_toggled");
        control36.CustomTooltipID = 1403;
        control35.addControl((CustomSelfDrawPanel.CSDControl) control36);
        this.attackCheck.Position = new Point(-30, 2);
        this.attackCheck.CheckedImage = (Image) GFXLibrary.checkbox_checked;
        this.attackCheck.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
        this.attackCheck.Checked = RemoteServices.Instance.UserOptions.autoAttack;
        this.attackCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.attackToggled));
        this.attackCheck.CustomTooltipID = 1403;
        control35.addControl((CustomSelfDrawPanel.CSDControl) this.attackCheck);
        CustomSelfDrawPanel.CSDLabel control37 = new CustomSelfDrawPanel.CSDLabel();
        control37.Text = SK.Text("LogoutPanel_Auto_Attack", "Auto Attack");
        control37.Position = new Point(40, 0);
        control37.Color = ARGBColors.Black;
        control37.Size = new Size(140, control35.Height);
        control37.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        control37.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        control35.addControl((CustomSelfDrawPanel.CSDControl) control37);
        this.attackArea.Position = new Point(135, -20);
        this.attackArea.Size = new Size(428, control35.Height + 40);
        this.attackArea.Visible = this.attackCheck.Checked;
        control35.addControl((CustomSelfDrawPanel.CSDControl) this.attackArea);
        this.attackCheck_Bandits.Position = new Point(0, 1);
        this.attackCheck_Bandits.CheckedImage = (Image) GFXLibrary.logout_bits[2];
        this.attackCheck_Bandits.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
        this.attackCheck_Bandits.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
        this.attackCheck_Bandits.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
        this.attackCheck_Bandits.Checked = RemoteServices.Instance.UserOptions.autoAttackBandit;
        this.attackCheck_Bandits.CustomTooltipID = 1409;
        this.attackArea.addControl((CustomSelfDrawPanel.CSDControl) this.attackCheck_Bandits);
        CustomSelfDrawPanel.CSDImage control38 = new CustomSelfDrawPanel.CSDImage();
        control38.Image = (Image) GFXLibrary.scout_screen_icons[24];
        control38.Position = new Point(-20, -11);
        this.attackCheck_Bandits.addControl((CustomSelfDrawPanel.CSDControl) control38);
        this.attackCheck_Wolves.Position = new Point(85, 1);
        this.attackCheck_Wolves.CheckedImage = (Image) GFXLibrary.logout_bits[2];
        this.attackCheck_Wolves.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
        this.attackCheck_Wolves.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
        this.attackCheck_Wolves.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
        this.attackCheck_Wolves.Checked = RemoteServices.Instance.UserOptions.autoAttackWolf;
        this.attackCheck_Wolves.CustomTooltipID = 1410;
        this.attackArea.addControl((CustomSelfDrawPanel.CSDControl) this.attackCheck_Wolves);
        CustomSelfDrawPanel.CSDImage control39 = new CustomSelfDrawPanel.CSDImage();
        control39.Image = (Image) GFXLibrary.scout_screen_icons[25];
        control39.Position = new Point(-8, -14);
        this.attackCheck_Wolves.addControl((CustomSelfDrawPanel.CSDControl) control39);
        this.attackCheck_AI.Position = new Point(170, 1);
        this.attackCheck_AI.CheckedImage = (Image) GFXLibrary.logout_bits[2];
        this.attackCheck_AI.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
        this.attackCheck_AI.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
        this.attackCheck_AI.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
        this.attackCheck_AI.Checked = RemoteServices.Instance.UserOptions.autoAttackAI;
        this.attackCheck_AI.CustomTooltipID = 1411;
        this.attackArea.addControl((CustomSelfDrawPanel.CSDControl) this.attackCheck_AI);
        CustomSelfDrawPanel.CSDImage control40 = new CustomSelfDrawPanel.CSDImage();
        control40.Image = (Image) GFXLibrary.scout_screen_icons[28];
        control40.Position = new Point(-17, -11);
        this.attackCheck_AI.addControl((CustomSelfDrawPanel.CSDControl) control40);
        CustomSelfDrawPanel.CSDImage control41 = new CustomSelfDrawPanel.CSDImage();
        control41.Image = (Image) GFXLibrary.logout_gradation_band;
        control41.Position = new Point(38, 240);
        control24.addControl((CustomSelfDrawPanel.CSDControl) control41);
        CustomSelfDrawPanel.CSDImage control42 = new CustomSelfDrawPanel.CSDImage();
        control42.Image = (Image) GFXLibrary.wl_moving_unit_icons[0];
        control42.Position = new Point(-4, -4);
        control42.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() =>
        {
          this.recruitCheck.Checked = !this.recruitCheck.Checked;
          this.recruitToggled();
        }), "Generic_check_box_toggled");
        control42.CustomTooltipID = 1404;
        control41.addControl((CustomSelfDrawPanel.CSDControl) control42);
        this.recruitCheck.Position = new Point(-30, 2);
        this.recruitCheck.CheckedImage = (Image) GFXLibrary.checkbox_checked;
        this.recruitCheck.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
        this.recruitCheck.Checked = RemoteServices.Instance.UserOptions.autoRecruit;
        this.recruitCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.recruitToggled));
        this.recruitCheck.CustomTooltipID = 1404;
        control41.addControl((CustomSelfDrawPanel.CSDControl) this.recruitCheck);
        CustomSelfDrawPanel.CSDLabel control43 = new CustomSelfDrawPanel.CSDLabel();
        control43.Text = SK.Text("LogoutPanel_Auto_Recruit", "Auto Recruit");
        control43.Position = new Point(40, 0);
        control43.Color = ARGBColors.Black;
        control43.Size = new Size(140, control41.Height);
        control43.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        control43.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        control41.addControl((CustomSelfDrawPanel.CSDControl) control43);
        this.recruitArea.Position = new Point(135, -20);
        this.recruitArea.Size = new Size(428, control41.Height + 40 + 40 + 30);
        this.recruitArea.Visible = this.recruitCheck.Checked;
        control41.addControl((CustomSelfDrawPanel.CSDControl) this.recruitArea);
        this.recruitCheck_Peasants.Position = new Point(0, 1);
        this.recruitCheck_Peasants.CheckedImage = (Image) GFXLibrary.logout_bits[2];
        this.recruitCheck_Peasants.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
        this.recruitCheck_Peasants.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
        this.recruitCheck_Peasants.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
        this.recruitCheck_Peasants.Checked = RemoteServices.Instance.UserOptions.autoRecruitPeasants;
        this.recruitCheck_Peasants.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.recruitToggledUnit));
        this.recruitCheck_Peasants.CustomTooltipID = 1412;
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitCheck_Peasants);
        this.recruitTrackBar_Peasants.Position = new Point(this.recruitCheck_Peasants.Position.X + 3, 75);
        this.recruitTrackBar_Peasants.Margin = new Rectangle(0, -4, 0, 0);
        this.recruitTrackBar_Peasants.Max = 50;
        this.recruitTrackBar_Peasants.Value = RemoteServices.Instance.UserOptions.autoRecruitPeasants_Caps / 10;
        this.recruitTrackBar_Peasants.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.recruitTracksMoved));
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitTrackBar_Peasants);
        this.recruitTrackBar_Peasants.Create((Image) GFXLibrary.logout_slider_back2, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb);
        this.recruitNumber_Peasants.Text = "0";
        CustomSelfDrawPanel.CSDLabel recruitNumberPeasants = this.recruitNumber_Peasants;
        Point position = this.recruitCheck_Peasants.Position;
        Point point1 = new Point(position.X, 105);
        recruitNumberPeasants.Position = point1;
        this.recruitNumber_Peasants.Color = ARGBColors.Black;
        this.recruitNumber_Peasants.Size = new Size(this.recruitCheck_Peasants.Width, 20);
        this.recruitNumber_Peasants.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
        this.recruitNumber_Peasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitNumber_Peasants);
        CustomSelfDrawPanel.CSDImage control44 = new CustomSelfDrawPanel.CSDImage();
        control44.Image = (Image) GFXLibrary.logout_bits[9];
        control44.Position = new Point(0, 0);
        this.recruitCheck_Peasants.addControl((CustomSelfDrawPanel.CSDControl) control44);
        this.recruitCheck_Archers.Position = new Point(85, 1);
        this.recruitCheck_Archers.CheckedImage = (Image) GFXLibrary.logout_bits[2];
        this.recruitCheck_Archers.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
        this.recruitCheck_Archers.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
        this.recruitCheck_Archers.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
        this.recruitCheck_Archers.Checked = RemoteServices.Instance.UserOptions.autoRecruitArchers;
        this.recruitCheck_Archers.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.recruitToggledUnit));
        this.recruitCheck_Archers.CustomTooltipID = 1413;
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitCheck_Archers);
        CustomSelfDrawPanel.CSDTrackBar recruitTrackBarArchers = this.recruitTrackBar_Archers;
        position = this.recruitCheck_Archers.Position;
        Point point2 = new Point(position.X + 3, 75);
        recruitTrackBarArchers.Position = point2;
        this.recruitTrackBar_Archers.Margin = new Rectangle(0, -4, 0, 0);
        this.recruitTrackBar_Archers.Max = 50;
        this.recruitTrackBar_Archers.Value = RemoteServices.Instance.UserOptions.autoRecruitArchers_Caps / 10;
        this.recruitTrackBar_Archers.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.recruitTracksMoved));
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitTrackBar_Archers);
        this.recruitTrackBar_Archers.Create((Image) GFXLibrary.logout_slider_back2, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb);
        this.recruitNumber_Archers.Text = "0";
        CustomSelfDrawPanel.CSDLabel recruitNumberArchers = this.recruitNumber_Archers;
        position = this.recruitCheck_Archers.Position;
        Point point3 = new Point(position.X, 105);
        recruitNumberArchers.Position = point3;
        this.recruitNumber_Archers.Color = ARGBColors.Black;
        this.recruitNumber_Archers.Size = new Size(this.recruitCheck_Archers.Width, 20);
        this.recruitNumber_Archers.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
        this.recruitNumber_Archers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitNumber_Archers);
        CustomSelfDrawPanel.CSDImage control45 = new CustomSelfDrawPanel.CSDImage();
        control45.Image = (Image) GFXLibrary.logout_bits[10];
        control45.Position = new Point(0, 0);
        this.recruitCheck_Archers.addControl((CustomSelfDrawPanel.CSDControl) control45);
        this.recruitCheck_Pikemen.Position = new Point(170, 1);
        this.recruitCheck_Pikemen.CheckedImage = (Image) GFXLibrary.logout_bits[2];
        this.recruitCheck_Pikemen.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
        this.recruitCheck_Pikemen.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
        this.recruitCheck_Pikemen.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
        this.recruitCheck_Pikemen.Checked = RemoteServices.Instance.UserOptions.autoRecruitPikemen;
        this.recruitCheck_Pikemen.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.recruitToggledUnit));
        this.recruitCheck_Pikemen.CustomTooltipID = 1414;
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitCheck_Pikemen);
        CustomSelfDrawPanel.CSDTrackBar recruitTrackBarPikemen = this.recruitTrackBar_Pikemen;
        position = this.recruitCheck_Pikemen.Position;
        Point point4 = new Point(position.X + 3, 75);
        recruitTrackBarPikemen.Position = point4;
        this.recruitTrackBar_Pikemen.Margin = new Rectangle(0, -4, 0, 0);
        this.recruitTrackBar_Pikemen.Max = 50;
        this.recruitTrackBar_Pikemen.Value = RemoteServices.Instance.UserOptions.autoRecruitPikemen_Caps / 10;
        this.recruitTrackBar_Pikemen.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.recruitTracksMoved));
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitTrackBar_Pikemen);
        this.recruitTrackBar_Pikemen.Create((Image) GFXLibrary.logout_slider_back2, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb);
        this.recruitNumber_Pikemen.Text = "0";
        CustomSelfDrawPanel.CSDLabel recruitNumberPikemen = this.recruitNumber_Pikemen;
        position = this.recruitCheck_Pikemen.Position;
        Point point5 = new Point(position.X, 105);
        recruitNumberPikemen.Position = point5;
        this.recruitNumber_Pikemen.Color = ARGBColors.Black;
        this.recruitNumber_Pikemen.Size = new Size(this.recruitCheck_Pikemen.Width, 20);
        this.recruitNumber_Pikemen.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
        this.recruitNumber_Pikemen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitNumber_Pikemen);
        CustomSelfDrawPanel.CSDImage control46 = new CustomSelfDrawPanel.CSDImage();
        control46.Image = (Image) GFXLibrary.logout_bits[11];
        control46.Position = new Point(0, 0);
        this.recruitCheck_Pikemen.addControl((CustomSelfDrawPanel.CSDControl) control46);
        this.recruitCheck_Swordsmen.Position = new Point((int) byte.MaxValue, 1);
        this.recruitCheck_Swordsmen.CheckedImage = (Image) GFXLibrary.logout_bits[2];
        this.recruitCheck_Swordsmen.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
        this.recruitCheck_Swordsmen.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
        this.recruitCheck_Swordsmen.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
        this.recruitCheck_Swordsmen.Checked = RemoteServices.Instance.UserOptions.autoRecruitSwordsmen;
        this.recruitCheck_Swordsmen.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.recruitToggledUnit));
        this.recruitCheck_Swordsmen.CustomTooltipID = 1415;
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitCheck_Swordsmen);
        CustomSelfDrawPanel.CSDTrackBar trackBarSwordsmen = this.recruitTrackBar_Swordsmen;
        position = this.recruitCheck_Swordsmen.Position;
        Point point6 = new Point(position.X + 3, 75);
        trackBarSwordsmen.Position = point6;
        this.recruitTrackBar_Swordsmen.Margin = new Rectangle(0, -4, 0, 0);
        this.recruitTrackBar_Swordsmen.Max = 50;
        this.recruitTrackBar_Swordsmen.Value = RemoteServices.Instance.UserOptions.autoRecruitSwordsmen_Caps / 10;
        this.recruitTrackBar_Swordsmen.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.recruitTracksMoved));
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitTrackBar_Swordsmen);
        this.recruitTrackBar_Swordsmen.Create((Image) GFXLibrary.logout_slider_back2, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb);
        this.recruitNumber_Swordsmen.Text = "0";
        CustomSelfDrawPanel.CSDLabel recruitNumberSwordsmen = this.recruitNumber_Swordsmen;
        position = this.recruitCheck_Swordsmen.Position;
        Point point7 = new Point(position.X, 105);
        recruitNumberSwordsmen.Position = point7;
        this.recruitNumber_Swordsmen.Color = ARGBColors.Black;
        this.recruitNumber_Swordsmen.Size = new Size(this.recruitCheck_Swordsmen.Width, 20);
        this.recruitNumber_Swordsmen.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
        this.recruitNumber_Swordsmen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitNumber_Swordsmen);
        CustomSelfDrawPanel.CSDImage control47 = new CustomSelfDrawPanel.CSDImage();
        control47.Image = (Image) GFXLibrary.logout_bits[12];
        control47.Position = new Point(0, 0);
        this.recruitCheck_Swordsmen.addControl((CustomSelfDrawPanel.CSDControl) control47);
        this.recruitCheck_Catapults.Position = new Point(340, 1);
        this.recruitCheck_Catapults.CheckedImage = (Image) GFXLibrary.logout_bits[2];
        this.recruitCheck_Catapults.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
        this.recruitCheck_Catapults.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
        this.recruitCheck_Catapults.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
        this.recruitCheck_Catapults.Checked = RemoteServices.Instance.UserOptions.autoRecruitCatapults;
        this.recruitCheck_Catapults.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.recruitToggledUnit));
        this.recruitCheck_Catapults.CustomTooltipID = 1416;
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitCheck_Catapults);
        CustomSelfDrawPanel.CSDTrackBar trackBarCatapults = this.recruitTrackBar_Catapults;
        position = this.recruitCheck_Catapults.Position;
        Point point8 = new Point(position.X + 3, 75);
        trackBarCatapults.Position = point8;
        this.recruitTrackBar_Catapults.Margin = new Rectangle(0, -4, 0, 0);
        this.recruitTrackBar_Catapults.Max = 50;
        this.recruitTrackBar_Catapults.Value = RemoteServices.Instance.UserOptions.autoRecruitCatapults_Caps / 5;
        this.recruitTrackBar_Catapults.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.recruitTracksMoved));
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitTrackBar_Catapults);
        this.recruitTrackBar_Catapults.Create((Image) GFXLibrary.logout_slider_back2, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb);
        this.recruitNumber_Catapults.Text = "0";
        CustomSelfDrawPanel.CSDLabel recruitNumberCatapults = this.recruitNumber_Catapults;
        position = this.recruitCheck_Catapults.Position;
        Point point9 = new Point(position.X, 105);
        recruitNumberCatapults.Position = point9;
        this.recruitNumber_Catapults.Color = ARGBColors.Black;
        this.recruitNumber_Catapults.Size = new Size(this.recruitCheck_Catapults.Width, 20);
        this.recruitNumber_Catapults.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
        this.recruitNumber_Catapults.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitNumber_Catapults);
        CustomSelfDrawPanel.CSDImage control48 = new CustomSelfDrawPanel.CSDImage();
        control48.Image = (Image) GFXLibrary.logout_bits[13];
        control48.Position = new Point(0, 0);
        this.recruitCheck_Catapults.addControl((CustomSelfDrawPanel.CSDControl) control48);
        this.recruitmentInfoLabel.Text = SK.Text("Logout_Recruitment_Cap", "Set Recruitment Cap");
        this.recruitmentInfoLabel.Position = new Point(-7, 125);
        this.recruitmentInfoLabel.Color = ARGBColors.Black;
        this.recruitmentInfoLabel.Size = new Size(this.recruitArea.Width, 23);
        this.recruitmentInfoLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
        this.recruitmentInfoLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.recruitArea.addControl((CustomSelfDrawPanel.CSDControl) this.recruitmentInfoLabel);
        this.recruitTracksMoved();
        this.updateRecruitVisibility();
        CustomSelfDrawPanel.CSDImage control49 = new CustomSelfDrawPanel.CSDImage();
        control49.Image = (Image) GFXLibrary.logout_gradation_band;
        control49.Position = new Point(38, 310);
        control49.Visible = false;
        control24.addControl((CustomSelfDrawPanel.CSDControl) control49);
        CustomSelfDrawPanel.CSDImage control50 = new CustomSelfDrawPanel.CSDImage();
        control50.Image = (Image) GFXLibrary.wl_moving_unit_icons[1];
        control50.Position = new Point(-4, -4);
        control50.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() =>
        {
          this.transferCheck.Checked = !this.transferCheck.Checked;
          this.transferToggled();
        }), "Generic_check_box_toggled");
        control50.CustomTooltipID = 1406;
        control49.addControl((CustomSelfDrawPanel.CSDControl) control50);
        this.transferCheck.Position = new Point(-30, 2);
        this.transferCheck.CheckedImage = (Image) GFXLibrary.checkbox_checked;
        this.transferCheck.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
        this.transferCheck.Checked = false;
        this.transferCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.transferToggled));
        this.transferCheck.CustomTooltipID = 1406;
        control49.addControl((CustomSelfDrawPanel.CSDControl) this.transferCheck);
        CustomSelfDrawPanel.CSDLabel control51 = new CustomSelfDrawPanel.CSDLabel();
        control51.Text = SK.Text("LogoutPanel_Auto_transfer", "Auto Transfer");
        control51.Position = new Point(40, 0);
        control51.Color = ARGBColors.Black;
        control51.Size = new Size(140, control49.Height);
        control51.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        control51.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        control49.addControl((CustomSelfDrawPanel.CSDControl) control51);
        this.transferArea.Position = new Point(135, -20);
        this.transferArea.Size = new Size(428, control49.Height + 40);
        this.transferArea.Visible = this.transferCheck.Checked;
        control49.addControl((CustomSelfDrawPanel.CSDControl) this.transferArea);
        CustomSelfDrawPanel.CSDImage control52 = new CustomSelfDrawPanel.CSDImage();
        control52.Image = (Image) GFXLibrary.logout_gradation_band;
        control52.Position = new Point(38, 380);
        control52.Visible = false;
        control24.addControl((CustomSelfDrawPanel.CSDControl) control52);
        CustomSelfDrawPanel.CSDImage control53 = new CustomSelfDrawPanel.CSDImage();
        control53.Image = (Image) GFXLibrary.wl_moving_unit_icons[25];
        control53.Position = new Point(-4, -4);
        control53.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() =>
        {
          this.repairCheck.Checked = !this.repairCheck.Checked;
          this.repairToggled();
        }), "Generic_check_box_toggled");
        control53.CustomTooltipID = 1405;
        control52.addControl((CustomSelfDrawPanel.CSDControl) control53);
        this.repairCheck.Position = new Point(-30, 2);
        this.repairCheck.CheckedImage = (Image) GFXLibrary.checkbox_checked;
        this.repairCheck.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
        this.repairCheck.Checked = false;
        this.repairCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.repairToggled));
        this.repairCheck.CustomTooltipID = 1405;
        control52.addControl((CustomSelfDrawPanel.CSDControl) this.repairCheck);
        CustomSelfDrawPanel.CSDLabel control54 = new CustomSelfDrawPanel.CSDLabel();
        control54.Text = SK.Text("LogoutPanel_Auto_Rebuild", "Auto Rebuild");
        control54.Position = new Point(40, 0);
        control54.Color = ARGBColors.Black;
        control54.Size = new Size(140, control52.Height);
        control54.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        control54.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        control52.addControl((CustomSelfDrawPanel.CSDControl) control54);
        this.repairArea.Position = new Point(135, -20);
        this.repairArea.Size = new Size(428, control52.Height + 40);
        this.repairArea.Visible = this.repairCheck.Checked;
        control52.addControl((CustomSelfDrawPanel.CSDControl) this.repairArea);
        CustomSelfDrawPanel.CSDImage control55 = new CustomSelfDrawPanel.CSDImage();
        control55.Image = (Image) GFXLibrary.logout_bits[4];
        control55.Position = new Point(0, 0);
        this.repairArea.addControl((CustomSelfDrawPanel.CSDControl) control55);
      }
      if (!GameEngine.Instance.World.isBigpointAccount && !Program.bigpointInstall && !Program.aeriaInstall && !Program.bigpointPartnerInstall)
      {
        CustomSelfDrawPanel.CSDButton control56 = new CustomSelfDrawPanel.CSDButton();
        control56.ImageNorm = (Image) GFXLibrary.banner_ad_friend;
        control56.OverBrighten = true;
        control56.Position = new Point(375, 496);
        control56.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.friendClicked), "LogoutPanel_invite_a_friend");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control56);
      }
      if (normalLogout && !advertOnly)
      {
        CustomSelfDrawPanel.CSDButton control57 = new CustomSelfDrawPanel.CSDButton();
        control57.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        control57.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        control57.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        control57.Position = new Point(423, 453);
        control57.Text.Text = SK.Text("LogoutPanel_Swap_Worlds", "Swap Worlds");
        control57.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        control57.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        control57.TextYOffset = -2;
        control57.Text.Color = ARGBColors.Black;
        control57.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.doLogout), "LogoutPanel_swap_worlds");
        control57.CustomTooltipID = 1420;
        control1.addControl((CustomSelfDrawPanel.CSDControl) control57);
        this.logoutPressed = false;
      }
      if (!advertOnly)
      {
        CustomSelfDrawPanel.CSDButton control58 = new CustomSelfDrawPanel.CSDButton();
        control58.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        control58.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        control58.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        control58.Position = new Point(787, 453);
        control58.Text.Text = SK.Text("GENERIC_Exit", "Exit");
        control58.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        control58.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        control58.TextYOffset = -2;
        control58.Text.Color = ARGBColors.Black;
        control58.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.doQuit), "LogoutPanel_exit");
        control58.CustomTooltipID = 1418;
        control1.addControl((CustomSelfDrawPanel.CSDControl) control58);
      }
      if (normalLogout && !advertOnly)
      {
        CustomSelfDrawPanel.CSDButton control59 = new CustomSelfDrawPanel.CSDButton();
        control59.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        control59.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        control59.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        control59.Position = new Point(605, 453);
        control59.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
        control59.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        control59.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        control59.TextYOffset = -2;
        control59.Text.Color = ARGBColors.Black;
        control59.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "LogoutPanel_cancel");
        control59.CustomTooltipID = 1419;
        control1.addControl((CustomSelfDrawPanel.CSDControl) control59);
      }
      else if (advertOnly)
      {
        CustomSelfDrawPanel.CSDButton control60 = new CustomSelfDrawPanel.CSDButton();
        control60.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        control60.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        control60.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        control60.Position = new Point(605, 453);
        control60.Text.Text = SK.Text("GENERIC_OK", "OK");
        control60.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        control60.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        control60.TextYOffset = -2;
        control60.Text.Color = ARGBColors.Black;
        control60.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "LogoutPanel_cancel");
        control1.addControl((CustomSelfDrawPanel.CSDControl) control60);
      }
      this.update();
    }

    public void update()
    {
    }

    private void closeClick()
    {
      StatTrackingClient.Instance().ActivateTrigger(26, (object) false);
      if (this.m_normalLogout)
      {
        this.closePopup();
        InterfaceMgr.Instance.closeLogoutWindow();
        InterfaceMgr.Instance.ParentForm.TopMost = true;
        InterfaceMgr.Instance.ParentForm.TopMost = false;
      }
      else
        this.doQuit();
    }

    private void doLogout()
    {
      if (this.logoutPressed)
        return;
      this.logoutPressed = true;
      Sound.stopVillageEnvironmental();
      if (InterfaceMgr.Instance.ParentForm != null)
        InterfaceMgr.Instance.ParentForm.Hide();
      if (!this.premium)
        LoggingOutPopup.open(true, false, false, false, false, false, false, 6, 0, false, false, false, false, false, false, 500, 500, 500, 500, 250);
      else
        LoggingOutPopup.open(true, this.scoutingCheck.Checked, this.tradingCheck.Checked, this.attackCheck.Checked, this.attackCheck_Wolves.Checked, this.attackCheck_Bandits.Checked, this.attackCheck_AI.Checked, this.tradingResourceImage.Data, this.tradingTrackBar.Value, this.recruitCheck.Checked, this.recruitCheck_Peasants.Checked, this.recruitCheck_Archers.Checked, this.recruitCheck_Pikemen.Checked, this.recruitCheck_Swordsmen.Checked, this.recruitCheck_Catapults.Checked, this.recruitTrackBar_Peasants.Value * 10, this.recruitTrackBar_Archers.Value * 10, this.recruitTrackBar_Pikemen.Value * 10, this.recruitTrackBar_Swordsmen.Value * 10, this.recruitTrackBar_Catapults.Value * 5);
      this.closePopup();
      InterfaceMgr.Instance.closeLogoutWindow();
    }

    private void doQuit()
    {
      Sound.stopVillageEnvironmental();
      if (!this.premium)
        RemoteServices.Instance.LogOut(true, false, false, false, false, false, false, 6, 0, false, false, false, false, false, false, 500, 500, 500, 500, 250);
      else
        RemoteServices.Instance.LogOut(true, this.scoutingCheck.Checked, this.tradingCheck.Checked, this.attackCheck.Checked, this.attackCheck_Wolves.Checked, this.attackCheck_Bandits.Checked, this.attackCheck_AI.Checked, this.tradingResourceImage.Data, this.tradingTrackBar.Value, this.recruitCheck.Checked, this.recruitCheck_Peasants.Checked, this.recruitCheck_Archers.Checked, this.recruitCheck_Pikemen.Checked, this.recruitCheck_Swordsmen.Checked, this.recruitCheck_Catapults.Checked, this.recruitTrackBar_Peasants.Value * 10, this.recruitTrackBar_Archers.Value * 10, this.recruitTrackBar_Pikemen.Value * 10, this.recruitTrackBar_Swordsmen.Value * 10, this.recruitTrackBar_Catapults.Value * 5);
      GameEngine.Instance.sessionExpired(-1);
      GameEngine.Instance.FlagQuitGame();
      this.closePopup();
      LogoutOptionsWindow2.closing = true;
      InterfaceMgr.Instance.closeLogoutWindow();
    }

    private void cardsClicked()
    {
      StatTrackingClient.Instance().ActivateTrigger(26, (object) true);
      this.closePopup();
      InterfaceMgr.Instance.closeLogoutWindow();
      InterfaceMgr.Instance.openPlayCardsWindow(0).SwitchPanel(4);
    }

    private void friendClicked()
    {
      string fileName = URLs.InviteAFriendURL + "?webtoken=" + RemoteServices.Instance.WebToken + "&lang=" + Program.mySettings.LanguageIdent.ToLower() + "&colour=" + GFXLibrary.invite_ad_colour.ToString();
      try
      {
        Process.Start(fileName);
      }
      catch (Exception ex)
      {
        int num = (int) MyMessageBox.Show(SK.Text("ERROR_Browser1", "Stronghold Kingdoms encountered an error when trying to open your system's Default Web Browser. Please check that your web browser is working correctly and there are no unresponsive copies showing in task manager->Processes and then try again.") + Environment.NewLine + Environment.NewLine + SK.Text("ERROR_Browser2", "If this problem persists, please contact support."), SK.Text("ERROR_Browser3", "Error opening Web Browser"));
      }
    }

    private void tradingToggled() => this.tradingArea.Visible = this.tradingCheck.Checked;

    private void scoutingToggled() => this.scoutingArea.Visible = this.scoutingCheck.Checked;

    private void attackToggled() => this.attackArea.Visible = this.attackCheck.Checked;

    private void recruitToggled()
    {
      this.recruitArea.Visible = this.recruitCheck.Checked;
      this.mainBackgroundImage.invalidate();
      this.updateRecruitVisibility();
    }

    private void transferToggled() => this.transferArea.Visible = this.transferCheck.Checked;

    private void repairToggled() => this.repairArea.Visible = this.repairCheck.Checked;

    private void tradingResourceClicked()
    {
      if (this.closePopup())
        return;
      this.m_resourcePopup = new SelectTradingResourcePopup();
      Point p = this.tradingCircleButton.getPanelPosition();
      p = new Point(p.X + this.tradingCircleButton.Width / 2 - 300, p.Y + this.tradingCircleButton.Height + 20);
      this.m_resourcePopup.init(this.tradingResourceImage.Data, this.Parent.PointToScreen(p), this, (LogoutOptionsWindow2) this.Parent);
    }

    public void vacationModeCloseCheck()
    {
      if (this.m_normalLogout)
        return;
      this.doQuit();
    }

    public bool closePopup()
    {
      bool flag = false;
      if (this.m_resourcePopup != null)
      {
        if (this.m_resourcePopup.Created)
        {
          this.m_resourcePopup.Close();
          flag = true;
        }
        this.m_resourcePopup = (SelectTradingResourcePopup) null;
      }
      return flag;
    }

    public void resourceSelected(int resource)
    {
      this.tradingResourceImage.Image = (Image) GFXLibrary.getCommodity64DSImage(resource);
      this.tradingResourceImage.Size = new Size(69, 69);
      this.tradingResourceImage.Data = resource;
      this.tradingCircleButton.CustomTooltipData = resource;
      this.closePopup();
    }

    public void tracksMoved()
    {
      this.tradingPercentLabel.Text = this.tradingTrackBar.Value.ToString() + "%";
    }

    public void recruitTracksMoved()
    {
      this.recruitNumber_Peasants.Text = (this.recruitTrackBar_Peasants.Value * 10).ToString();
      this.recruitNumber_Archers.Text = (this.recruitTrackBar_Archers.Value * 10).ToString();
      this.recruitNumber_Pikemen.Text = (this.recruitTrackBar_Pikemen.Value * 10).ToString();
      this.recruitNumber_Swordsmen.Text = (this.recruitTrackBar_Swordsmen.Value * 10).ToString();
      this.recruitNumber_Catapults.Text = (this.recruitTrackBar_Catapults.Value * 5).ToString();
    }

    public void recruitToggledUnit()
    {
      this.updateRecruitVisibility();
      this.mainBackgroundImage.invalidate();
    }

    public void updateRecruitVisibility()
    {
      this.recruitTrackBar_Peasants.Visible = this.recruitNumber_Peasants.Visible = this.recruitCheck_Peasants.Checked;
      this.recruitTrackBar_Archers.Visible = this.recruitNumber_Archers.Visible = this.recruitCheck_Archers.Checked;
      this.recruitTrackBar_Pikemen.Visible = this.recruitNumber_Pikemen.Visible = this.recruitCheck_Pikemen.Checked;
      this.recruitTrackBar_Swordsmen.Visible = this.recruitNumber_Swordsmen.Visible = this.recruitCheck_Swordsmen.Checked;
      this.recruitTrackBar_Catapults.Visible = this.recruitNumber_Catapults.Visible = this.recruitCheck_Catapults.Checked;
    }

    private class ResourceItem
    {
      public string resourceName;
      public int resourceBuildingID;

      public ResourceItem(string name, int id)
      {
        this.resourceName = name;
        this.resourceBuildingID = id;
      }

      public override string ToString() => this.resourceName;
    }
  }
}
