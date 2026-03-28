// Decompiled with JetBrains decompiler
// Type: Kingdoms.WorldsEndPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class WorldsEndPanel : CustomSelfDrawPanel
  {
    public static EndOfTheWorldStats_ReturnType cachedData;
    private CustomSelfDrawPanel.CSDExtendingPanel background = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea backgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDFill transparentBackground = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage topImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage bottomImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage eowLogo = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage headerShield = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage headerGlobe = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton btnClose = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel headerLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lostMessageLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry1_name = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry1_you = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry1_all = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry2_name = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry2_you = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry2_all = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry3_name = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry3_you = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry3_all = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry4_name = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry4_you = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry4_all = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry5_name = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry5_you = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry5_all = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry6_name = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry6_you = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry6_all = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry7_name = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry7_you = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry7_all = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry8_name = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry8_you = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry8_all = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry9_name = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry9_you = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry9_all = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry10_name = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry10_you = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry10_all = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry11_name = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry11_you = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry11_all = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry12_name = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry12_you = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry12_all = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry13_name = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry13_you = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel Entry13_all = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel endDateLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel endDateLabel2 = new CustomSelfDrawPanel.CSDLabel();
    private WorldsEndWindow m_parent;
    private IContainer components;

    public WorldsEndPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(WorldsEndWindow parent)
    {
      int y = 25;
      int num = 22;
      int x1 = 30;
      int x2 = 200;
      int x3 = 350;
      int width = 150;
      this.m_parent = parent;
      this.clearControls();
      this.transparentBackground.Size = this.Size;
      this.transparentBackground.FillColor = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.transparentBackground);
      this.background.Position = new Point(0, 70);
      this.background.Size = new Size(this.Width, 446);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
      this.background.Create((Image) GFXLibrary._9sclice_fancy_top_left, (Image) GFXLibrary._9sclice_fancy_top_mid, (Image) GFXLibrary._9sclice_fancy_top_right, (Image) GFXLibrary._9sclice_fancy_mid_left, (Image) GFXLibrary._9sclice_fancy_mid_mid, (Image) GFXLibrary._9sclice_fancy_mid_right, (Image) GFXLibrary._9sclice_fancy_bottom_left, (Image) GFXLibrary._9sclice_fancy_bottom_mid, (Image) GFXLibrary._9sclice_fancy_bottom_right);
      this.background.ForceTiling();
      this.topImage.Image = (Image) GFXLibrary._9sclice_fancy_top_mid_over_01;
      this.topImage.Position = new Point((this.Width - this.topImage.Image.Width) / 2, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.topImage);
      this.bottomImage.Image = (Image) GFXLibrary._9sclice_fancy_bottom_mid_over;
      this.bottomImage.Position = new Point((this.Width - this.bottomImage.Image.Width) / 2, this.Height - this.bottomImage.Image.Height - 5);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.bottomImage);
      this.backgroundArea.Position = new Point(171, 134);
      this.backgroundArea.Size = new Size(514, 340);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundArea);
      if (!GameEngine.Instance.LocalWorldData.AIWorld)
      {
        this.eowLogo.Image = (Image) GFXLibrary.age_seventh_age_x130;
        this.eowLogo.Position = new Point((this.backgroundArea.Width - this.eowLogo.Image.Width) / 2, -133);
        this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.eowLogo);
      }
      this.headerShield.Image = GameEngine.Instance.World.getWorldShield(RemoteServices.Instance.UserID, 25, 28);
      this.headerShield.Position = new Point(320, -10);
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.headerShield);
      this.headerGlobe.Image = (Image) GFXLibrary.eow_globe;
      this.headerGlobe.Position = new Point(470, -10);
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.headerGlobe);
      this.btnClose.ImageNorm = (Image) GFXLibrary.worldSelect_swap_norm;
      this.btnClose.ImageOver = (Image) GFXLibrary.worldSelect_swap_over;
      this.btnClose.ImageClick = (Image) GFXLibrary.worldSelect_swap_pushed;
      this.btnClose.Position = new Point(260 - this.btnClose.ImageNorm.Width / 2, 327);
      this.btnClose.Text.Text = SK.Text("GENERIC_Close", "Close");
      this.btnClose.TextYOffset = -2;
      this.btnClose.Text.Color = ARGBColors.White;
      this.btnClose.Text.DropShadowColor = ARGBColors.Black;
      this.btnClose.Text.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
      this.btnClose.Text.Position = new Point(-3, 0);
      this.btnClose.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.logoutClick));
      this.btnClose.Enabled = true;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.btnClose);
      this.endDateLabel.Text = SK.Text("EOW_WorldEnded", "World Ended on Day") + " : ";
      this.endDateLabel.Position = new Point(x1 - 10, -10);
      this.endDateLabel.Size = new Size(this.backgroundArea.Width + 100, 150);
      this.endDateLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.endDateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.endDateLabel.Color = ARGBColors.Red;
      this.endDateLabel.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.endDateLabel);
      this.endDateLabel2.Text = SK.Text("EOW_AtTime", "At Time") + " : ";
      this.endDateLabel2.Position = new Point(x1 - 10, 7);
      this.endDateLabel2.Size = new Size(this.backgroundArea.Width + 100, 150);
      this.endDateLabel2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.endDateLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.endDateLabel2.Color = ARGBColors.Red;
      this.endDateLabel2.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.endDateLabel2);
      this.Entry1_name.Text = SK.Text("EOW_Stats_Captures", "Villages Captured");
      this.Entry1_name.Position = new Point(x1, y + num * 3);
      this.Entry1_name.Size = new Size(this.backgroundArea.Width, 20);
      this.Entry1_name.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry1_name.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.Entry1_name.Color = ARGBColors.Black;
      this.Entry1_name.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry1_name);
      this.Entry1_you.Text = "";
      this.Entry1_you.Position = new Point(x2, y + num * 3);
      this.Entry1_you.Size = new Size(width, 20);
      this.Entry1_you.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry1_you.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry1_you.Color = ARGBColors.Black;
      this.Entry1_you.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry1_you);
      this.Entry1_all.Text = "";
      this.Entry1_all.Position = new Point(x3, y + num * 3);
      this.Entry1_all.Size = new Size(width, 20);
      this.Entry1_all.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry1_all.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry1_all.Color = ARGBColors.Black;
      this.Entry1_all.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry1_all);
      this.Entry2_name.Text = SK.Text("EOW_Stats_razes", "Villages Razed");
      this.Entry2_name.Position = new Point(x1, y + num * 4);
      this.Entry2_name.Size = new Size(this.backgroundArea.Width, 20);
      this.Entry2_name.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry2_name.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.Entry2_name.Color = ARGBColors.Black;
      this.Entry2_name.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry2_name);
      this.Entry2_you.Text = "";
      this.Entry2_you.Position = new Point(x2, y + num * 4);
      this.Entry2_you.Size = new Size(width, 20);
      this.Entry2_you.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry2_you.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry2_you.Color = ARGBColors.Black;
      this.Entry2_you.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry2_you);
      this.Entry2_all.Text = "";
      this.Entry2_all.Position = new Point(x3, y + num * 4);
      this.Entry2_all.Size = new Size(width, 20);
      this.Entry2_all.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry2_all.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry2_all.Color = ARGBColors.Black;
      this.Entry2_all.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry2_all);
      this.Entry3_name.Text = SK.Text("EOW_Stats_trades", "Market Trades");
      this.Entry3_name.Position = new Point(x1, y + num * 11);
      this.Entry3_name.Size = new Size(this.backgroundArea.Width, 20);
      this.Entry3_name.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry3_name.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.Entry3_name.Color = ARGBColors.Black;
      this.Entry3_name.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry3_name);
      this.Entry3_you.Text = "";
      this.Entry3_you.Position = new Point(x2, y + num * 11);
      this.Entry3_you.Size = new Size(width, 20);
      this.Entry3_you.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry3_you.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry3_you.Color = ARGBColors.Black;
      this.Entry3_you.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry3_you);
      this.Entry3_all.Text = "";
      this.Entry3_all.Position = new Point(x3, y + num * 11);
      this.Entry3_all.Size = new Size(width, 20);
      this.Entry3_all.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry3_all.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry3_all.Color = ARGBColors.Black;
      this.Entry3_all.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry3_all);
      this.Entry4_name.Text = SK.Text("EOW_Stats_transfers", "Supply Carts Sent");
      this.Entry4_name.Position = new Point(x1, y + num * 10);
      this.Entry4_name.Size = new Size(this.backgroundArea.Width, 20);
      this.Entry4_name.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry4_name.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.Entry4_name.Color = ARGBColors.Black;
      this.Entry4_name.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry4_name);
      this.Entry4_you.Text = "";
      this.Entry4_you.Position = new Point(x2, y + num * 10);
      this.Entry4_you.Size = new Size(width, 20);
      this.Entry4_you.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry4_you.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry4_you.Color = ARGBColors.Black;
      this.Entry4_you.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry4_you);
      this.Entry4_all.Text = "";
      this.Entry4_all.Position = new Point(x3, y + num * 10);
      this.Entry4_all.Size = new Size(width, 20);
      this.Entry4_all.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry4_all.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry4_all.Color = ARGBColors.Black;
      this.Entry4_all.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry4_all);
      this.Entry5_name.Text = SK.Text("EOW_Stats_attacks", "Battles Fought");
      this.Entry5_name.Position = new Point(x1, y);
      this.Entry5_name.Size = new Size(this.backgroundArea.Width, 20);
      this.Entry5_name.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry5_name.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.Entry5_name.Color = ARGBColors.Black;
      this.Entry5_name.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry5_name);
      this.Entry5_you.Text = "";
      this.Entry5_you.Position = new Point(x2, y);
      this.Entry5_you.Size = new Size(width, 20);
      this.Entry5_you.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry5_you.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry5_you.Color = ARGBColors.Black;
      this.Entry5_you.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry5_you);
      this.Entry5_all.Text = "";
      this.Entry5_all.Position = new Point(x3, y);
      this.Entry5_all.Size = new Size(width, 20);
      this.Entry5_all.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry5_all.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry5_all.Color = ARGBColors.Black;
      this.Entry5_all.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry5_all);
      this.Entry6_name.Text = SK.Text("EOW_Stats_scouts", "Scouts Sent");
      this.Entry6_name.Position = new Point(x1, y + num);
      this.Entry6_name.Size = new Size(this.backgroundArea.Width, 20);
      this.Entry6_name.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry6_name.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.Entry6_name.Color = ARGBColors.Black;
      this.Entry6_name.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry6_name);
      this.Entry6_you.Text = "";
      this.Entry6_you.Position = new Point(x2, y + num);
      this.Entry6_you.Size = new Size(width, 20);
      this.Entry6_you.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry6_you.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry6_you.Color = ARGBColors.Black;
      this.Entry6_you.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry6_you);
      this.Entry6_all.Text = "";
      this.Entry6_all.Position = new Point(x3, y + num);
      this.Entry6_all.Size = new Size(width, 20);
      this.Entry6_all.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry6_all.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry6_all.Color = ARGBColors.Black;
      this.Entry6_all.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry6_all);
      this.Entry7_name.Text = SK.Text("EOW_Stats_Pillaged", "Goods Pillaged");
      this.Entry7_name.Position = new Point(x1, y + num * 5);
      this.Entry7_name.Size = new Size(this.backgroundArea.Width, 20);
      this.Entry7_name.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry7_name.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.Entry7_name.Color = ARGBColors.Black;
      this.Entry7_name.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry7_name);
      this.Entry7_you.Text = "";
      this.Entry7_you.Position = new Point(x2, y + num * 5);
      this.Entry7_you.Size = new Size(width, 20);
      this.Entry7_you.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry7_you.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry7_you.Color = ARGBColors.Black;
      this.Entry7_you.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry7_you);
      this.Entry7_all.Text = "";
      this.Entry7_all.Position = new Point(x3, y + num * 5);
      this.Entry7_all.Size = new Size(width, 20);
      this.Entry7_all.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry7_all.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry7_all.Color = ARGBColors.Black;
      this.Entry7_all.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry7_all);
      this.Entry8_name.Text = SK.Text("EOW_Stats_ransacked", "Buildings Ransacked");
      this.Entry8_name.Position = new Point(x1, y + num * 6);
      this.Entry8_name.Size = new Size(this.backgroundArea.Width, 20);
      this.Entry8_name.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry8_name.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.Entry8_name.Color = ARGBColors.Black;
      this.Entry8_name.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry8_name);
      this.Entry8_you.Text = "";
      this.Entry8_you.Position = new Point(x2, y + num * 6);
      this.Entry8_you.Size = new Size(width, 20);
      this.Entry8_you.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry8_you.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry8_you.Color = ARGBColors.Black;
      this.Entry8_you.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry8_you);
      this.Entry8_all.Text = "";
      this.Entry8_all.Position = new Point(x3, y + num * 6);
      this.Entry8_all.Size = new Size(width, 20);
      this.Entry8_all.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry8_all.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry8_all.Color = ARGBColors.Black;
      this.Entry8_all.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry8_all);
      this.Entry9_name.Text = SK.Text("EOW_Stats_glory", "Glory Gained");
      this.Entry9_name.Position = new Point(x1, y + num * 2);
      this.Entry9_name.Size = new Size(this.backgroundArea.Width, 20);
      this.Entry9_name.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry9_name.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.Entry9_name.Color = ARGBColors.Black;
      this.Entry9_name.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry9_name);
      this.Entry9_you.Text = "";
      this.Entry9_you.Position = new Point(x2, y + num * 2);
      this.Entry9_you.Size = new Size(width, 20);
      this.Entry9_you.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry9_you.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry9_you.Color = ARGBColors.Black;
      this.Entry9_you.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry9_you);
      this.Entry9_all.Text = "";
      this.Entry9_all.Position = new Point(x3, y + num * 2);
      this.Entry9_all.Size = new Size(width, 20);
      this.Entry9_all.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry9_all.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry9_all.Color = ARGBColors.Black;
      this.Entry9_all.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry9_all);
      this.Entry10_name.Text = SK.Text("EOW_Stats_wolves", "Wolves Killed");
      this.Entry10_name.Position = new Point(x1, y + num * 9);
      this.Entry10_name.Size = new Size(this.backgroundArea.Width, 20);
      this.Entry10_name.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry10_name.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.Entry10_name.Color = ARGBColors.Black;
      this.Entry10_name.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry10_name);
      this.Entry10_you.Text = "";
      this.Entry10_you.Position = new Point(x2, y + num * 9);
      this.Entry10_you.Size = new Size(width, 20);
      this.Entry10_you.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry10_you.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry10_you.Color = ARGBColors.Black;
      this.Entry10_you.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry10_you);
      this.Entry10_all.Text = "";
      this.Entry10_all.Position = new Point(x3, y + num * 9);
      this.Entry10_all.Size = new Size(width, 20);
      this.Entry10_all.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry10_all.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry10_all.Color = ARGBColors.Black;
      this.Entry10_all.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry10_all);
      this.Entry11_name.Text = SK.Text("EOW_Stats_bandits", "Bandits Killed");
      this.Entry11_name.Position = new Point(x1, y + num * 8);
      this.Entry11_name.Size = new Size(this.backgroundArea.Width, 20);
      this.Entry11_name.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry11_name.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.Entry11_name.Color = ARGBColors.Black;
      this.Entry11_name.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry11_name);
      this.Entry11_you.Text = "";
      this.Entry11_you.Position = new Point(x2, y + num * 8);
      this.Entry11_you.Size = new Size(width, 20);
      this.Entry11_you.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry11_you.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry11_you.Color = ARGBColors.Black;
      this.Entry11_you.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry11_you);
      this.Entry11_all.Text = "";
      this.Entry11_all.Position = new Point(x3, y + num * 8);
      this.Entry11_all.Size = new Size(width, 20);
      this.Entry11_all.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry11_all.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry11_all.Color = ARGBColors.Black;
      this.Entry11_all.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry11_all);
      this.Entry12_name.Text = SK.Text("EOW_Stats_AITroops", "AI Troops Killed");
      this.Entry12_name.Position = new Point(x1, y + num * 7);
      this.Entry12_name.Size = new Size(this.backgroundArea.Width, 20);
      this.Entry12_name.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry12_name.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.Entry12_name.Color = ARGBColors.Black;
      this.Entry12_name.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry12_name);
      this.Entry12_you.Text = "";
      this.Entry12_you.Position = new Point(x2, y + num * 7);
      this.Entry12_you.Size = new Size(width, 20);
      this.Entry12_you.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry12_you.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry12_you.Color = ARGBColors.Black;
      this.Entry12_you.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry12_you);
      this.Entry12_all.Text = "";
      this.Entry12_all.Position = new Point(x3, y + num * 7);
      this.Entry12_all.Size = new Size(width, 20);
      this.Entry12_all.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry12_all.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry12_all.Color = ARGBColors.Black;
      this.Entry12_all.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry12_all);
      this.Entry13_name.Text = SK.Text("EOW_Stats_stashes", "Stashes Uncovered");
      this.Entry13_name.Position = new Point(x1, y + num * 12);
      this.Entry13_name.Size = new Size(this.backgroundArea.Width, 20);
      this.Entry13_name.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry13_name.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.Entry13_name.Color = ARGBColors.Black;
      this.Entry13_name.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry13_name);
      this.Entry13_you.Text = "";
      this.Entry13_you.Position = new Point(x2, y + num * 12);
      this.Entry13_you.Size = new Size(width, 20);
      this.Entry13_you.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry13_you.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry13_you.Color = ARGBColors.Black;
      this.Entry13_you.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry13_you);
      this.Entry13_all.Text = "";
      this.Entry13_all.Position = new Point(x3, y + num * 12);
      this.Entry13_all.Size = new Size(width, 20);
      this.Entry13_all.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.Entry13_all.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.Entry13_all.Color = ARGBColors.Black;
      this.Entry13_all.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.Entry13_all);
      if (WorldsEndPanel.cachedData == null)
      {
        RemoteServices.Instance.set_EndOfTheWorldStats_UserCallBack(new RemoteServices.EndOfTheWorldStats_UserCallBack(this.endOfTheWorldCallback));
        RemoteServices.Instance.EndOfTheWorldStats();
      }
      else
        this.showData();
    }

    private void endOfTheWorldCallback(EndOfTheWorldStats_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      WorldsEndPanel.cachedData = returnData;
      this.showData();
    }

    private void showData()
    {
      NumberFormatInfo nfi = GameEngine.NFI;
      this.Entry1_you.Text = WorldsEndPanel.cachedData.yourData.numCaptures.ToString("N", (IFormatProvider) nfi);
      this.Entry1_all.Text = WorldsEndPanel.cachedData.globalData.numCaptures.ToString("N", (IFormatProvider) nfi);
      this.Entry2_you.Text = WorldsEndPanel.cachedData.yourData.numRazes.ToString("N", (IFormatProvider) nfi);
      this.Entry2_all.Text = WorldsEndPanel.cachedData.globalData.numRazes.ToString("N", (IFormatProvider) nfi);
      this.Entry3_you.Text = WorldsEndPanel.cachedData.yourData.numTradesSent.ToString("N", (IFormatProvider) nfi);
      this.Entry3_all.Text = WorldsEndPanel.cachedData.globalData.numTradesSent.ToString("N", (IFormatProvider) nfi);
      this.Entry4_you.Text = WorldsEndPanel.cachedData.yourData.numMarketTransfers.ToString("N", (IFormatProvider) nfi);
      this.Entry4_all.Text = WorldsEndPanel.cachedData.globalData.numMarketTransfers.ToString("N", (IFormatProvider) nfi);
      this.Entry5_you.Text = WorldsEndPanel.cachedData.yourData.numAttacks.ToString("N", (IFormatProvider) nfi);
      this.Entry5_all.Text = WorldsEndPanel.cachedData.globalData.numAttacks.ToString("N", (IFormatProvider) nfi);
      this.Entry6_you.Text = WorldsEndPanel.cachedData.yourData.numScouts.ToString("N", (IFormatProvider) nfi);
      this.Entry6_all.Text = WorldsEndPanel.cachedData.globalData.numScouts.ToString("N", (IFormatProvider) nfi);
      this.Entry7_you.Text = ((long) (WorldsEndPanel.cachedData.yourData.numGoodsPillaged * 500.0)).ToString("N", (IFormatProvider) nfi);
      this.Entry7_all.Text = ((long) (WorldsEndPanel.cachedData.globalData.numGoodsPillaged * 500.0)).ToString("N", (IFormatProvider) nfi);
      this.Entry8_you.Text = WorldsEndPanel.cachedData.yourData.numBuildingsRansacked.ToString("N", (IFormatProvider) nfi);
      this.Entry8_all.Text = WorldsEndPanel.cachedData.globalData.numBuildingsRansacked.ToString("N", (IFormatProvider) nfi);
      this.Entry9_you.Text = WorldsEndPanel.cachedData.yourData.gloryGained.ToString("N", (IFormatProvider) nfi);
      this.Entry9_all.Text = WorldsEndPanel.cachedData.globalData.gloryGained.ToString("N", (IFormatProvider) nfi);
      this.Entry10_you.Text = WorldsEndPanel.cachedData.yourData.numWolvesKilled.ToString("N", (IFormatProvider) nfi);
      this.Entry10_all.Text = WorldsEndPanel.cachedData.globalData.numWolvesKilled.ToString("N", (IFormatProvider) nfi);
      this.Entry11_you.Text = WorldsEndPanel.cachedData.yourData.numBanditsKilled.ToString("N", (IFormatProvider) nfi);
      this.Entry11_all.Text = WorldsEndPanel.cachedData.globalData.numBanditsKilled.ToString("N", (IFormatProvider) nfi);
      this.Entry12_you.Text = WorldsEndPanel.cachedData.yourData.numAITroopsKilled.ToString("N", (IFormatProvider) nfi);
      this.Entry12_all.Text = WorldsEndPanel.cachedData.globalData.numAITroopsKilled.ToString("N", (IFormatProvider) nfi);
      this.Entry13_you.Text = WorldsEndPanel.cachedData.yourData.numStashesUncovered.ToString("N", (IFormatProvider) nfi);
      this.Entry13_all.Text = WorldsEndPanel.cachedData.globalData.numStashesUncovered.ToString("N", (IFormatProvider) nfi);
      int totalDays = (int) (WorldsEndPanel.cachedData.globalData.endTime - GameEngine.Instance.World.m_worldStartDate).TotalDays;
      this.endDateLabel.Text = SK.Text("EOW_WorldEnded", "World Ended on Day") + " : " + totalDays.ToString();
      this.endDateLabel2.Text = SK.Text("EOW_AtTime", "At Time") + " : " + WorldsEndPanel.cachedData.globalData.endTime.ToShortTimeString();
    }

    private void logoutClick()
    {
      GameEngine.Instance.playInterfaceSound("AutoSelectVillageAreaPopup_logout");
      this.m_parent.closing = true;
      GameEngine.Instance.closeNoVillagePopup(false);
    }

    public void closePopup()
    {
    }

    public void update()
    {
    }

    public static void logout() => WorldsEndPanel.cachedData = (EndOfTheWorldStats_ReturnType) null;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Name = nameof (WorldsEndPanel);
      this.Size = new Size(600, 55);
      this.ResumeLayout(false);
    }
  }
}
