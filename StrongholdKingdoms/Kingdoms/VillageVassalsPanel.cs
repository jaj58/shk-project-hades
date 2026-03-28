// Decompiled with JetBrains decompiler
// Type: Kingdoms.VillageVassalsPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class VillageVassalsPanel : CustomSelfDrawPanel, IDockableControl
  {
    public static VillageVassalsPanel instance;
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage backgroundLeftEdge = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton btnVassalsOverview = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDExtendingPanel liegeLordImageArea = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel currentLiegeLordLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel currentLiegeLordInfoLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblArchers = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblPikemen = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblSwordsmen = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblCatapults = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblHonourPerDay = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton btnBreakVassalage = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel yourVassalsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel outGoingFromLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel maxVassalsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDVertScrollBar vassalScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea vassalScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDImage smallPeasantImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage smallPeasantImage2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton btnClose = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton btnSelectVassal = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton btnRequestVassalage = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel tbSelectVassalName = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblVassalError = new CustomSelfDrawPanel.CSDLabel();
    private int blockYSize;
    private CustomSelfDrawPanel.CSDExtendingPanel noResearchWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel noResearchText = new CustomSelfDrawPanel.CSDLabel();
    private List<VillageVassalsPanel.ArmyLine> lineList = new List<VillageVassalsPanel.ArmyLine>();
    private int theirVillageID;
    private int yourVillageID;
    public bool validVassalTarget;
    public int m_selectedVillage = -1;
    private VillageVassalsPanel.ArmyComparer armyComparer = new VillageVassalsPanel.ArmyComparer();
    private DockableControl dockableControl;
    private IContainer components;
    private Panel focusPanel;

    public VillageVassalsPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.focusPanel.Focus();
    }

    public void reinit()
    {
      bool validVassalTarget = this.validVassalTarget;
      int selectedVillage = this.m_selectedVillage;
      this.init(false);
      this.validVassalTarget = validVassalTarget;
      this.m_selectedVillage = selectedVillage;
      if (this.validVassalTarget && GameEngine.Instance.World.numVassalsAllowed() > GameEngine.Instance.World.countVassals())
        this.btnRequestVassalage.Enabled = true;
      if (this.m_selectedVillage < 0)
        return;
      this.tbSelectVassalName.Text = GameEngine.Instance.World.getVillageName(this.m_selectedVillage);
    }

    public void init(bool resized)
    {
      int height = this.Height;
      VillageVassalsPanel.instance = this;
      this.clearControls();
      this.backgroundImage.Image = (Image) GFXLibrary.body_background_002;
      this.backgroundImage.Size = new Size(this.Width, height - 40);
      this.backgroundImage.Tile = true;
      this.backgroundImage.Position = new Point(0, 40);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
      this.backgroundLeftEdge.Image = (Image) GFXLibrary.body_background_canvas_left_edge;
      this.backgroundLeftEdge.Position = new Point(0, 0);
      this.backgroundLeftEdge.Size = new Size(this.backgroundLeftEdge.Image.Width, height - 40);
      this.backgroundLeftEdge.Tile = true;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundLeftEdge);
      this.headerImage.Size = new Size(this.Width, 40);
      this.headerImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage);
      this.headerImage.CreateX((Image) GFXLibrary.mail_top_drag_bar_left, (Image) GFXLibrary.mail_top_drag_bar_middle, (Image) GFXLibrary.mail_top_drag_bar_right, -2, 2);
      int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
      this.parishNameLabel.Text = SK.Text("GENERIC_Vassals", "Vassals") + " : " + GameEngine.Instance.World.getVillageNameOrType(selectedMenuVillage);
      this.parishNameLabel.Color = ARGBColors.White;
      this.parishNameLabel.DropShadowColor = ARGBColors.Black;
      this.parishNameLabel.Position = new Point(20, 0);
      this.parishNameLabel.Size = new Size(this.Width - 40, 40);
      this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
      this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.parishNameLabel);
      this.btnVassalsOverview.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
      this.btnVassalsOverview.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
      this.btnVassalsOverview.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
      this.btnVassalsOverview.Position = new Point(this.Width - 230, 7);
      this.btnVassalsOverview.Text.Text = SK.Text("Vassals_Overview", "Vassals Overview");
      this.btnVassalsOverview.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.btnVassalsOverview.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.btnVassalsOverview.TextYOffset = -3;
      this.btnVassalsOverview.Text.Color = ARGBColors.Black;
      this.btnVassalsOverview.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.allVassals), "VillageVassalsPanel_all_vassals");
      this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnVassalsOverview);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.headerImage, 8, new Point(this.Width - 230 - 50, 3));
      this.liegeLordImageArea.Size = new Size(this.Width - 50, 85);
      this.liegeLordImageArea.Position = new Point(25, 20);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.liegeLordImageArea);
      this.liegeLordImageArea.Create((Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_right);
      this.currentLiegeLordLabel.Text = SK.Text("VassalControlPanel_Current_Liege_Lord", "Current Liege Lord");
      this.currentLiegeLordLabel.Color = ARGBColors.Black;
      this.currentLiegeLordLabel.Position = new Point(5, 5);
      this.currentLiegeLordLabel.Size = new Size(500, 25);
      this.currentLiegeLordLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.currentLiegeLordLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.liegeLordImageArea.addControl((CustomSelfDrawPanel.CSDControl) this.currentLiegeLordLabel);
      Graphics graphics = this.CreateGraphics();
      Size size = graphics.MeasureString(this.currentLiegeLordLabel.Text, this.currentLiegeLordLabel.Font, 500).ToSize();
      graphics.Dispose();
      this.currentLiegeLordInfoLabel.Text = "";
      this.currentLiegeLordInfoLabel.Color = ARGBColors.Black;
      this.currentLiegeLordInfoLabel.Position = new Point(10 + size.Width + 5, 7);
      this.currentLiegeLordInfoLabel.Size = new Size(500, 50);
      this.currentLiegeLordInfoLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.currentLiegeLordInfoLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.currentLiegeLordInfoLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.liegeLordClicked));
      this.liegeLordImageArea.addControl((CustomSelfDrawPanel.CSDControl) this.currentLiegeLordInfoLabel);
      this.lblHonourPerDay.Text = SK.Text("VassalControlPanel_Honour_Gained_Per_Day", "Honour Gained Per Day") + " : ";
      this.lblHonourPerDay.Color = ARGBColors.Black;
      this.lblHonourPerDay.Position = new Point(243, 56);
      this.lblHonourPerDay.Size = new Size(500, 25);
      this.lblHonourPerDay.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.lblHonourPerDay.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.liegeLordImageArea.addControl((CustomSelfDrawPanel.CSDControl) this.lblHonourPerDay);
      this.smallPeasantImage2.Image = (Image) GFXLibrary.armies_screen_troops;
      this.smallPeasantImage2.Position = new Point(642, 5);
      this.smallPeasantImage2.ClipRect = new Rectangle(0, 0, this.smallPeasantImage2.Image.Width * 5 / 6, this.smallPeasantImage2.Image.Height);
      this.liegeLordImageArea.addControl((CustomSelfDrawPanel.CSDControl) this.smallPeasantImage2);
      this.lblPeasants.Text = "0";
      this.lblPeasants.Color = ARGBColors.Black;
      this.lblPeasants.Position = new Point(624, 55);
      this.lblPeasants.Size = new Size(55, 25);
      this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
      this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.liegeLordImageArea.addControl((CustomSelfDrawPanel.CSDControl) this.lblPeasants);
      this.lblArchers.Text = "0";
      this.lblArchers.Color = ARGBColors.Black;
      this.lblArchers.Position = new Point(684, 55);
      this.lblArchers.Size = new Size(55, 25);
      this.lblArchers.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
      this.lblArchers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.liegeLordImageArea.addControl((CustomSelfDrawPanel.CSDControl) this.lblArchers);
      this.lblPikemen.Text = "0";
      this.lblPikemen.Color = ARGBColors.Black;
      this.lblPikemen.Position = new Point(744, 55);
      this.lblPikemen.Size = new Size(55, 25);
      this.lblPikemen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
      this.lblPikemen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.liegeLordImageArea.addControl((CustomSelfDrawPanel.CSDControl) this.lblPikemen);
      this.lblSwordsmen.Text = "0";
      this.lblSwordsmen.Color = ARGBColors.Black;
      this.lblSwordsmen.Position = new Point(804, 55);
      this.lblSwordsmen.Size = new Size(55, 25);
      this.lblSwordsmen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
      this.lblSwordsmen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.liegeLordImageArea.addControl((CustomSelfDrawPanel.CSDControl) this.lblSwordsmen);
      this.lblCatapults.Text = "0";
      this.lblCatapults.Color = ARGBColors.Black;
      this.lblCatapults.Position = new Point(864, 55);
      this.lblCatapults.Size = new Size(55, 25);
      this.lblCatapults.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
      this.lblCatapults.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.liegeLordImageArea.addControl((CustomSelfDrawPanel.CSDControl) this.lblCatapults);
      this.btnBreakVassalage.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
      this.btnBreakVassalage.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
      this.btnBreakVassalage.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
      this.btnBreakVassalage.Position = new Point(37, 72);
      this.btnBreakVassalage.Text.Text = SK.Text("VassalControlPanel_Break_From_Liege_Lord", "Break From Liege Lord");
      this.btnBreakVassalage.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.btnBreakVassalage.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.btnBreakVassalage.TextYOffset = -3;
      this.btnBreakVassalage.Text.Color = ARGBColors.Black;
      this.btnBreakVassalage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.promptBreakFromYourLiegeLord), "VillageVassalsPanel_break_vassal");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnBreakVassalage);
      this.blockYSize = height - 40 - 56 - 124;
      this.headerLabelsImage.Size = new Size(this.Width - 25 - 23, 28);
      this.headerLabelsImage.Position = new Point(25, 129);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.brown_mail2_field_bar_mail_left, (Image) GFXLibrary.brown_mail2_field_bar_mail_middle, (Image) GFXLibrary.brown_mail2_field_bar_mail_right);
      this.divider2Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
      this.divider2Image.Position = new Point(300, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider2Image);
      this.yourVassalsLabel.Text = SK.Text("VassalControlPanel_Your_Vassals", "Your Vassals") + " (" + GameEngine.Instance.World.countVassals().ToString() + ")";
      this.yourVassalsLabel.Color = ARGBColors.Black;
      this.yourVassalsLabel.Position = new Point(12, -3);
      this.yourVassalsLabel.Size = new Size(223, this.headerLabelsImage.Height);
      this.yourVassalsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.yourVassalsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.yourVassalsLabel);
      this.maxVassalsLabel.Text = SK.Text("VassalControlPanel_Max_Vassals", "Maximum Vassals Allowed") + " : " + GameEngine.Instance.World.numVassalsAllowed().ToString();
      this.maxVassalsLabel.Color = ARGBColors.Black;
      this.maxVassalsLabel.Position = new Point(this.headerLabelsImage.Width - 333, -3);
      this.maxVassalsLabel.Size = new Size(319, this.headerLabelsImage.Height);
      this.maxVassalsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.maxVassalsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.maxVassalsLabel);
      this.vassalScrollArea.Position = new Point(25, 164);
      this.vassalScrollArea.Size = new Size(915, this.blockYSize - 40 - 10);
      this.vassalScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(915, this.blockYSize - 40 - 10));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.vassalScrollArea);
      this.vassalScrollArea.Visible = true;
      int num1 = this.vassalScrollBar.Value;
      this.vassalScrollBar.Position = new Point(943, 164);
      this.vassalScrollBar.Size = new Size(24, this.blockYSize - 40 - 10);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.vassalScrollBar);
      this.vassalScrollBar.Value = 0;
      this.vassalScrollBar.Max = 100;
      this.vassalScrollBar.NumVisibleLines = 25;
      this.vassalScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary.brown_24wide_thumb_top, (Image) GFXLibrary.brown_24wide_thumb_middle, (Image) GFXLibrary.brown_24wide_thumb_bottom);
      this.vassalScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      this.smallPeasantImage.Image = (Image) GFXLibrary.armies_screen_troops;
      this.smallPeasantImage.Position = new Point(323, -10);
      this.smallPeasantImage.ClipRect = new Rectangle(0, 0, this.smallPeasantImage.Image.Width * 5 / 6, this.smallPeasantImage.Image.Height);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.smallPeasantImage);
      int num2 = resized ? 1 : 0;
      if (resized)
        this.vassalScrollBar.Value = num1;
      this.btnClose.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
      this.btnClose.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
      this.btnClose.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
      this.btnClose.Position = new Point(this.Width - 230, height - 40 - 40 - 4);
      this.btnClose.Text.Text = SK.Text("GENERIC_Close", "Close");
      this.btnClose.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.btnClose.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.btnClose.TextYOffset = -3;
      this.btnClose.Text.Color = ARGBColors.Black;
      this.btnClose.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "VillageVassalsPanel_close");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnClose);
      this.btnSelectVassal.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
      this.btnSelectVassal.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
      this.btnSelectVassal.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
      this.btnSelectVassal.Position = new Point(20, height - 40 - 40 - 4);
      this.btnSelectVassal.Text.Text = SK.Text("VassalControlPanel_Select_Vassal", "Select Vassal");
      this.btnSelectVassal.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.btnSelectVassal.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.btnSelectVassal.TextYOffset = -3;
      this.btnSelectVassal.Text.Color = ARGBColors.Black;
      this.btnSelectVassal.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnSelectVassal_Click), "VillageVassalsPanel_select");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnSelectVassal);
      this.tbSelectVassalName.Text = "Selected Vassal";
      this.tbSelectVassalName.Color = ARGBColors.White;
      this.tbSelectVassalName.DropShadowColor = ARGBColors.Black;
      this.tbSelectVassalName.Position = new Point(240, height - 40 - 40 - 4 + 6);
      this.tbSelectVassalName.Size = new Size(200, 25);
      this.tbSelectVassalName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.tbSelectVassalName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tbSelectVassalName);
      this.lblVassalError.Text = "";
      this.lblVassalError.Color = ARGBColors.Black;
      this.lblVassalError.Position = new Point(20, height - 40 - 40 - 4 + 6 - 24);
      this.lblVassalError.Size = new Size(634, 25);
      this.lblVassalError.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.lblVassalError.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblVassalError);
      this.btnRequestVassalage.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
      this.btnRequestVassalage.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
      this.btnRequestVassalage.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
      this.btnRequestVassalage.Position = new Point(450, height - 40 - 40 - 4);
      this.btnRequestVassalage.Text.Text = SK.Text("VassalControlPanel_RequestVassalage", "Request Vassalage");
      this.btnRequestVassalage.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.btnRequestVassalage.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.btnRequestVassalage.TextYOffset = -3;
      this.btnRequestVassalage.Text.Color = ARGBColors.Black;
      this.btnRequestVassalage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnRequestVassalage_Click), "VillageVassalsPanel_request");
      this.btnRequestVassalage.Enabled = false;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnRequestVassalage);
      GameEngine.Instance.vassalsManager.Reset();
      this.lblVassalError.Visible = false;
      this.btnRequestVassalage.Enabled = false;
      this.btnSelectVassal.Visible = false;
      this.tbSelectVassalName.Visible = false;
      this.btnRequestVassalage.Visible = false;
      this.tbSelectVassalName.Text = "";
      this.noResearchWindow.Size = new Size(739, 150);
      this.noResearchWindow.Position = new Point(126, 230);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.noResearchWindow);
      this.noResearchWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
      this.noResearchWindow.Visible = false;
      this.noResearchText.Text = SK.Text("Vassal_Need_Rank", "You don't currently have the required Rank (8) to make another player your Vassal.");
      this.noResearchText.Color = Color.FromArgb(224, 203, 146);
      this.noResearchText.Position = new Point(20, 0);
      this.noResearchText.Size = new Size(this.noResearchWindow.Width - 40, this.noResearchWindow.Height);
      this.noResearchText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.noResearchText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.noResearchWindow.addControl((CustomSelfDrawPanel.CSDControl) this.noResearchText);
      RemoteServices.Instance.set_VassalInfo_UserCallBack(new RemoteServices.VassalInfo_UserCallBack(this.vassalInfoCallBack));
      VillageMap village = GameEngine.Instance.Village;
      if (village != null)
        RemoteServices.Instance.VassalInfo(village.VillageID);
      this.reAddVassals();
    }

    public void vassalInfoCallBack(VassalInfo_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      GameEngine.Instance.vassalsManager.importVassals(returnData.liegeLordInfo, returnData.vassals);
      GameEngine.Instance.vassalsManager.importVassalRequests(returnData.requestsYouveMade, returnData.requestsOfYou);
      this.reAddVassals();
      GameEngine.Instance.World.updateUserVassals();
      if (GameEngine.Instance.World.numVassalsAllowed() > GameEngine.Instance.World.countVassals() && !GameEngine.Instance.World.isHeretic())
      {
        this.btnSelectVassal.Visible = true;
        this.tbSelectVassalName.Visible = true;
        this.btnRequestVassalage.Visible = true;
        if (this.validVassalTarget)
          this.btnRequestVassalage.Enabled = true;
        else
          this.btnRequestVassalage.Enabled = false;
      }
      else
      {
        this.btnSelectVassal.Visible = false;
        this.tbSelectVassalName.Visible = false;
        this.btnRequestVassalage.Visible = false;
      }
    }

    public void update()
    {
    }

    public void logout()
    {
    }

    private void wallScrollBarMoved()
    {
      int y = this.vassalScrollBar.Value;
      this.vassalScrollArea.Position = new Point(this.vassalScrollArea.X, 164 - y);
      this.vassalScrollArea.ClipRect = new Rectangle(this.vassalScrollArea.ClipRect.X, y, this.vassalScrollArea.ClipRect.Width, this.vassalScrollArea.ClipRect.Height);
      this.vassalScrollArea.invalidate();
      this.vassalScrollBar.invalidate();
    }

    public void closing() => InterfaceMgr.Instance.closeDonatePopup();

    private void liegeLordClicked()
    {
      VassalInfo liegeLord = GameEngine.Instance.vassalsManager.GetLiegeLord();
      if (liegeLord == null || liegeLord.villageID < 0)
        return;
      int villageId = liegeLord.villageID;
      Point villageLocation = GameEngine.Instance.World.getVillageLocation(villageId);
      InterfaceMgr.Instance.changeTab(9);
      InterfaceMgr.Instance.changeTab(0);
      InterfaceMgr.Instance.closeParishPanel();
      GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
      InterfaceMgr.Instance.displaySelectedVillagePanel(villageId, false, true, true, false);
    }

    private void reAddVassals()
    {
      CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
      VassalInfo liegeLord = GameEngine.Instance.vassalsManager.GetLiegeLord();
      VassalInfo[] vassals = GameEngine.Instance.vassalsManager.GetVassals();
      VassalRequestInfo[] requestsSentByYou = GameEngine.Instance.vassalsManager.GetRequestsSentByYou();
      VassalRequestInfo[] requestsSentToYou = GameEngine.Instance.vassalsManager.GetRequestsSentToYou();
      this.lineList.Clear();
      if (liegeLord == null || liegeLord.villageID < 0)
      {
        this.currentLiegeLordInfoLabel.Text = SK.Text("VassalControlPanel_You_Have_No_Liege_Lord", "You currently have no Liege Lord. Accept offers from other players to become their Vassal to receive a daily Honour boost.");
        this.btnBreakVassalage.Visible = false;
        this.lblHonourPerDay.Visible = false;
        this.smallPeasantImage2.Visible = false;
        this.lblPeasants.Visible = false;
        this.lblArchers.Visible = false;
        this.lblPikemen.Visible = false;
        this.lblSwordsmen.Visible = false;
        this.lblCatapults.Visible = false;
      }
      else
      {
        this.btnBreakVassalage.Visible = true;
        this.smallPeasantImage2.Visible = true;
        this.lblPeasants.Visible = true;
        this.lblArchers.Visible = true;
        this.lblPikemen.Visible = true;
        this.lblSwordsmen.Visible = true;
        this.lblCatapults.Visible = true;
        NumberFormatInfo nfi = GameEngine.NFI;
        this.lblPeasants.Text = liegeLord.stationed_Peasants.ToString("N", (IFormatProvider) nfi);
        this.lblArchers.Text = liegeLord.stationed_Archers.ToString("N", (IFormatProvider) nfi);
        this.lblPikemen.Text = liegeLord.stationed_Pikemen.ToString("N", (IFormatProvider) nfi);
        this.lblSwordsmen.Text = liegeLord.stationed_Swordsmen.ToString("N", (IFormatProvider) nfi);
        this.lblCatapults.Text = liegeLord.stationed_Catapults.ToString("N", (IFormatProvider) nfi);
        this.currentLiegeLordInfoLabel.Text = GameEngine.Instance.World.getVillageName(liegeLord.villageID) + " (" + liegeLord.liegelordname + " - " + Rankings.getRankingName(GameEngine.Instance.LocalWorldData, liegeLord.rank, liegeLord.subrank, liegeLord.male) + ")";
        this.lblHonourPerDay.Visible = true;
        int num = (int) (liegeLord.honourPerSecond * 86400.0);
        this.lblHonourPerDay.Text = SK.Text("VassalControlPanel_Honour_Gained_Per_Day", "Honour Gained Per Day") + " : " + num.ToString("N", (IFormatProvider) nfi);
      }
      if (GameEngine.Instance.World.getRank() < 7)
        this.noResearchWindow.Visible = true;
      this.vassalScrollArea.clearControls();
      int num1 = 0;
      int position = 0;
      if (requestsSentToYou != null)
      {
        foreach (VassalRequestInfo vassalRequestInfo in requestsSentToYou)
        {
          if (num1 != 0)
            num1 += 5;
          VillageVassalsPanel.ArmyLine control = new VillageVassalsPanel.ArmyLine();
          control.Position = new Point(0, num1);
          control.initAsked(position, this, vassalRequestInfo.requesterVillageID, vassalRequestInfo.requesterUserName, vassalRequestInfo.requestMadeTime);
          this.vassalScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
          num1 += control.Height;
          this.lineList.Add(control);
          ++position;
        }
      }
      if (requestsSentByYou != null)
      {
        foreach (VassalRequestInfo vassalRequestInfo in requestsSentByYou)
        {
          if (num1 != 0)
            num1 += 5;
          VillageVassalsPanel.ArmyLine control = new VillageVassalsPanel.ArmyLine();
          control.Position = new Point(0, num1);
          control.initAsking(position, this, vassalRequestInfo.vassalVillageID, vassalRequestInfo.vassalUserName, vassalRequestInfo.requestMadeTime);
          this.vassalScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
          num1 += control.Height;
          this.lineList.Add(control);
          ++position;
        }
      }
      if (vassals != null)
      {
        foreach (VassalInfo vassalInfo in vassals)
        {
          if (num1 != 0)
            num1 += 5;
          VillageVassalsPanel.ArmyLine control = new VillageVassalsPanel.ArmyLine();
          control.Position = new Point(0, num1);
          control.init(position, this, vassalInfo.villageID, vassalInfo.honourPerSecond, vassalInfo.stationed_Peasants, vassalInfo.stationed_Archers, vassalInfo.stationed_Pikemen, vassalInfo.stationed_Swordsmen, vassalInfo.stationed_Catapults, vassalInfo.vassalPlayerName);
          this.vassalScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
          num1 += control.Height;
          this.lineList.Add(control);
          ++position;
        }
      }
      this.vassalScrollArea.Size = new Size(this.vassalScrollArea.Width, num1);
      if (num1 < this.vassalScrollBar.Height)
      {
        this.vassalScrollBar.Visible = false;
      }
      else
      {
        this.vassalScrollBar.Visible = true;
        this.vassalScrollBar.NumVisibleLines = this.vassalScrollBar.Height;
        this.vassalScrollBar.Max = num1 - this.vassalScrollBar.Height;
      }
      this.vassalScrollArea.invalidate();
      this.vassalScrollBar.invalidate();
      this.backgroundImage.invalidate();
    }

    public void promptBreakFromYourVassal(int villageID)
    {
      VillageMap village = GameEngine.Instance.Village;
      if (village == null)
        return;
      this.theirVillageID = villageID;
      this.yourVillageID = village.VillageID;
      if (MyMessageBox.Show(SK.Text("VassalControlPanel_BreakVassalage_Warning", "Breaking from your vassal will mean any troops stationed there will be lost."), SK.Text("VassalControlPanel_BreakVassalage", "Break Vassalage?"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.confirmBreakFromYourVassal();
    }

    private void confirmBreakFromYourVassal()
    {
      GameEngine.Instance.vassalsManager.BreakFromYourVassal(this.yourVillageID, this.theirVillageID, new VassalsManager.VassalsUpdatedCallback(this.reAddVassals));
    }

    private void promptBreakFromYourLiegeLord()
    {
      this.theirVillageID = GameEngine.Instance.vassalsManager.GetLiegeLord().villageID;
      VillageMap village = GameEngine.Instance.Village;
      this.yourVillageID = village.VillageID;
      if (village == null || MyMessageBox.Show(SK.Text("VassalControlPanel_BreakFromLiegeLord_Warning", "Breaking from your Liege Lord will remove any stationed troops from your village."), SK.Text("VassalControlPanel_BreakFromLiegeLord", "Break from Liege Lord?"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.confirmBreakFromYourLiegeLord();
    }

    private void confirmBreakFromYourLiegeLord()
    {
      GameEngine.Instance.vassalsManager.BreakFromYourLiegeLord(this.theirVillageID, this.yourVillageID, new VassalsManager.VassalsUpdatedCallback(this.reAddVassals));
    }

    public void btnRequestVassalage_Click()
    {
      if (this.m_selectedVillage < 0 || GameEngine.Instance.World.WorldEnded)
        return;
      VillageMap village = GameEngine.Instance.Village;
      if (village == null)
        return;
      this.btnRequestVassalage.Enabled = false;
      GameEngine.Instance.vassalsManager.AskSomeoneToBeYourVassal(village.VillageID, this.m_selectedVillage, new VassalsManager.VassalsUpdatedCallback(this.reAddVassals));
    }

    private void btnSelectVassal_Click()
    {
      VillageMap village = GameEngine.Instance.Village;
      if (village != null)
        GameEngine.Instance.World.zoomToVillage(village.VillageID);
      InterfaceMgr.Instance.getMainTabBar().selectDummyTabFast(13);
    }

    public void setVassalVillage(int villageID)
    {
      this.validVassalTarget = false;
      this.m_selectedVillage = villageID;
      this.tbSelectVassalName.Text = GameEngine.Instance.World.getVillageName(this.m_selectedVillage);
      if (villageID >= 0 && !GameEngine.Instance.World.WorldEnded)
      {
        RemoteServices.Instance.set_GetPreVassalInfo_UserCallBack(new RemoteServices.GetPreVassalInfo_UserCallBack(this.getPreVassalInfoCallBack));
        RemoteServices.Instance.GetPreVassalInfo(InterfaceMgr.Instance.OwnSelectedVillage, villageID);
      }
      this.btnRequestVassalage.Enabled = false;
      this.lblVassalError.Visible = false;
    }

    public void getPreVassalInfoCallBack(GetPreVassalInfo_ReturnType returnData)
    {
      if (returnData.Success)
      {
        if (returnData.alreadyHasLiegeLord || returnData.rankTooHigh || returnData.invalidTarget)
        {
          this.validVassalTarget = false;
          this.btnRequestVassalage.Enabled = false;
          if (returnData.alreadyHasLiegeLord)
            this.lblVassalError.Text = SK.Text("VassalControlPanel_Village_Has_Liege_Lord", "Village already has a liege lord");
          else if (returnData.rankTooHigh)
            this.lblVassalError.Text = SK.Text("VassalControlPanel_Rank_Too_High", "The Player's Rank is too high");
          else if (returnData.invalidTarget)
            this.lblVassalError.Text = SK.Text("VassalControlPanel_Invalid_Village", "Not a valid village for vassaling.");
          this.lblVassalError.Visible = true;
        }
        else
        {
          this.validVassalTarget = true;
          if (GameEngine.Instance.World.numVassalsAllowed() > GameEngine.Instance.World.countVassals())
            this.btnRequestVassalage.Enabled = true;
          this.lblVassalError.Visible = false;
        }
      }
      else
      {
        if (returnData.m_errorCode != ErrorCodes.ErrorCode.HERETIC)
          return;
        this.validVassalTarget = false;
        this.btnRequestVassalage.Enabled = false;
        this.lblVassalError.Text = SK.Text("VassalControlPanel_Village_Heretic", "Heretics can't use Vassalage");
        this.lblVassalError.Visible = true;
      }
    }

    public void PromptAcceptRequest(int villageID)
    {
      VassalInfo liegeLord = GameEngine.Instance.vassalsManager.GetLiegeLord();
      this.theirVillageID = villageID;
      if (liegeLord != null && liegeLord.villageID >= 0 && MyMessageBox.Show(SK.Text("VassalControlPanel_AcceptLiegeLordWarning", "Accepting a new Liege Lord will break you from your current Liege Lord and any troops stationed will be lost."), SK.Text("VassalControlPanel_AcceptLiegeLord", "Accept New Liege Lord?"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.onConfirmAcceptRequest();
    }

    private void onConfirmAcceptRequest()
    {
      GameEngine.Instance.vassalsManager.AcceptRequest(this.theirVillageID, GameEngine.Instance.Village.VillageID, new VassalsManager.VassalsUpdatedCallback(this.handleVassalRequestCallBack));
    }

    public void declineRequest(int villageID)
    {
      GameEngine.Instance.vassalsManager.DeclineRequest(villageID, GameEngine.Instance.Village.VillageID, new VassalsManager.VassalsUpdatedCallback(this.handleVassalRequestCallBack));
    }

    public void cancelRequest(int villageID)
    {
      GameEngine.Instance.vassalsManager.CancelRequest(villageID, GameEngine.Instance.Village.VillageID, new VassalsManager.VassalsUpdatedCallback(this.handleVassalRequestCallBack));
    }

    public void handleVassalRequestCallBack()
    {
      this.reAddVassals();
      this.lblVassalError.Visible = false;
      this.btnRequestVassalage.Enabled = false;
      this.tbSelectVassalName.Text = "";
    }

    private void allVassals() => InterfaceMgr.Instance.setVillageTabSubMode(24, false);

    private void closeClick() => InterfaceMgr.Instance.setVillageTabSubMode(-1);

    public void initProperties(bool dockable, string title, ContainerControl parent)
    {
      this.dockableControl.initProperties(dockable, title, parent);
    }

    public void display(ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(parent, x, y);
    }

    public void display(bool asPopup, ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(asPopup, parent, x, y);
    }

    public void controlDockToggle() => this.dockableControl.controlDockToggle();

    public void closeControl(bool includePopups)
    {
      this.dockableControl.closeControl(includePopups);
      this.clearControls();
      this.closing();
    }

    public bool isVisible() => this.dockableControl.isVisible();

    public bool isPopup() => this.dockableControl.isPopup();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.focusPanel = new Panel();
      this.SuspendLayout();
      this.focusPanel.BackColor = ARGBColors.Transparent;
      this.focusPanel.ForeColor = ARGBColors.Transparent;
      this.focusPanel.Location = new Point(988, 3);
      this.focusPanel.Name = "focusPanel";
      this.focusPanel.Size = new Size(1, 1);
      this.focusPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.focusPanel);
      this.Name = nameof (VillageVassalsPanel);
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 566);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public class ArmyComparer : IComparer<WorldMap.LocalArmyData>
    {
      public int Compare(WorldMap.LocalArmyData x, WorldMap.LocalArmyData y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null || x.armyID > y.armyID)
          return 1;
        return x.armyID < y.armyID ? -1 : 0;
      }
    }

    public class ArmyLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblArchers = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblPikemen = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblSwordsmen = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblCatapults = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblArrivalTime = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDButton btnBreakVassalage = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton btnAccept = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton btnReject = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton btnCancel = new CustomSelfDrawPanel.CSDButton();
      private int m_position = -1000;
      private VillageVassalsPanel m_parent;
      private int m_villageID = -1;
      private DateTime m_arrivalTime = DateTime.Now;

      public void initAsked(
        int position,
        VillageVassalsPanel parent,
        int villageID,
        string userName,
        DateTime requestTime)
      {
        this.m_villageID = villageID;
        this.m_parent = parent;
        this.m_position = position;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.brown_lineitem_strip_02_dark : (Image) GFXLibrary.brown_lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.lblVillage.Text = GameEngine.Instance.World.getVillageName(villageID) + " (" + userName + ")";
        this.lblVillage.Color = ARGBColors.Black;
        this.lblVillage.RolloverColor = ARGBColors.White;
        this.lblVillage.Position = new Point(9, 0);
        this.lblVillage.Size = new Size(290, this.backgroundImage.Height);
        this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "VillageVassalsPanel_village");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblVillage);
        this.lblPeasants.Text = SK.Text("VassalControlRequestLine_Request_Made", "Request Made") + " :" + requestTime.ToShortTimeString() + " : " + requestTime.ToShortDateString();
        this.lblPeasants.Color = ARGBColors.Black;
        this.lblPeasants.Position = new Point(305, 0);
        this.lblPeasants.Size = new Size(430, this.backgroundImage.Height);
        this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblPeasants);
        this.btnAccept.ImageNorm = (Image) GFXLibrary.brown_mail2_button_blue_141wide_normal;
        this.btnAccept.ImageOver = (Image) GFXLibrary.brown_mail2_button_blue_141wide_over;
        this.btnAccept.ImageClick = (Image) GFXLibrary.brown_mail2_button_blue_141wide_pushed;
        this.btnAccept.Position = new Point(626, 3);
        this.btnAccept.Text.Text = SK.Text("GENERIC_Accept", "Accept");
        this.btnAccept.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.btnAccept.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.btnAccept.TextYOffset = -3;
        this.btnAccept.Text.Color = ARGBColors.Black;
        this.btnAccept.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.acceptVassalageRequest), "VillageVassalsPanel_accept");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnAccept);
        this.btnReject.ImageNorm = (Image) GFXLibrary.brown_mail2_button_blue_141wide_normal;
        this.btnReject.ImageOver = (Image) GFXLibrary.brown_mail2_button_blue_141wide_over;
        this.btnReject.ImageClick = (Image) GFXLibrary.brown_mail2_button_blue_141wide_pushed;
        this.btnReject.Position = new Point(776, 3);
        this.btnReject.Text.Text = SK.Text("GENERIC_Decline", "Decline");
        this.btnReject.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.btnReject.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.btnReject.TextYOffset = -3;
        this.btnReject.Text.Color = ARGBColors.Black;
        this.btnReject.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.declineVassalageRequest), "VillageVassalsPanel_reject");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnReject);
      }

      public void initAsking(
        int position,
        VillageVassalsPanel parent,
        int villageID,
        string userName,
        DateTime requestTime)
      {
        this.m_villageID = villageID;
        this.m_parent = parent;
        this.m_position = position;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.brown_lineitem_strip_02_dark : (Image) GFXLibrary.brown_lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.lblVillage.Text = GameEngine.Instance.World.getVillageName(villageID) + " (" + userName + ")";
        this.lblVillage.Color = ARGBColors.Black;
        this.lblVillage.RolloverColor = ARGBColors.White;
        this.lblVillage.Position = new Point(9, 0);
        this.lblVillage.Size = new Size(290, this.backgroundImage.Height);
        this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "VillageVassalsPanel_village");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblVillage);
        this.lblPeasants.Text = SK.Text("VassalControlRequestLine_Request_Made", "Request Made") + " :" + requestTime.ToShortTimeString() + " : " + requestTime.ToShortDateString();
        this.lblPeasants.Color = ARGBColors.Black;
        this.lblPeasants.Position = new Point(305, 0);
        this.lblPeasants.Size = new Size(430, this.backgroundImage.Height);
        this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblPeasants);
        this.btnCancel.ImageNorm = (Image) GFXLibrary.brown_mail2_button_blue_141wide_normal;
        this.btnCancel.ImageOver = (Image) GFXLibrary.brown_mail2_button_blue_141wide_over;
        this.btnCancel.ImageClick = (Image) GFXLibrary.brown_mail2_button_blue_141wide_pushed;
        this.btnCancel.Position = new Point(776, 3);
        this.btnCancel.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
        this.btnCancel.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.btnCancel.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.btnCancel.TextYOffset = -3;
        this.btnCancel.Text.Color = ARGBColors.Black;
        this.btnCancel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelVassalageRequest), "VillageVassalsPanel_cancel");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnCancel);
      }

      public void init(
        int position,
        VillageVassalsPanel parent,
        int villageID,
        double honourPerSecond,
        int numPeasants,
        int numArchers,
        int numPikemen,
        int numSwordsmen,
        int numCatapults,
        string username)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.m_villageID = villageID;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.brown_lineitem_strip_02_dark : (Image) GFXLibrary.brown_lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.lblVillage.Text = GameEngine.Instance.World.getVillageNameOrType(villageID);
        if (username.Length > 0)
        {
          CustomSelfDrawPanel.CSDLabel lblVillage = this.lblVillage;
          lblVillage.Text = lblVillage.Text + " (" + username + ")";
        }
        this.lblVillage.Color = ARGBColors.Black;
        this.lblVillage.RolloverColor = ARGBColors.White;
        this.lblVillage.Position = new Point(9, 0);
        this.lblVillage.Size = new Size(290, this.backgroundImage.Height);
        this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "VillageVassalsPanel_village");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblVillage);
        this.lblPeasants.Text = numPeasants.ToString();
        this.lblPeasants.Color = ARGBColors.Black;
        this.lblPeasants.RolloverColor = ARGBColors.White;
        this.lblPeasants.Position = new Point(305, 0);
        this.lblPeasants.Size = new Size(55, this.backgroundImage.Height);
        this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.lblPeasants.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick));
        this.lblPeasants.CustomTooltipID = 2800;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblPeasants);
        this.lblArchers.Text = numArchers.ToString();
        this.lblArchers.Color = ARGBColors.Black;
        this.lblArchers.RolloverColor = ARGBColors.White;
        this.lblArchers.Position = new Point(365, 0);
        this.lblArchers.Size = new Size(55, this.backgroundImage.Height);
        this.lblArchers.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblArchers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.lblArchers.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick));
        this.lblArchers.CustomTooltipID = 2800;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblArchers);
        this.lblPikemen.Text = numPikemen.ToString();
        this.lblPikemen.Color = ARGBColors.Black;
        this.lblPikemen.RolloverColor = ARGBColors.White;
        this.lblPikemen.Position = new Point(425, 0);
        this.lblPikemen.Size = new Size(55, this.backgroundImage.Height);
        this.lblPikemen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblPikemen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.lblPikemen.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick));
        this.lblPikemen.CustomTooltipID = 2800;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblPikemen);
        this.lblSwordsmen.Text = numSwordsmen.ToString();
        this.lblSwordsmen.Color = ARGBColors.Black;
        this.lblSwordsmen.RolloverColor = ARGBColors.White;
        this.lblSwordsmen.Position = new Point(485, 0);
        this.lblSwordsmen.Size = new Size(55, this.backgroundImage.Height);
        this.lblSwordsmen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblSwordsmen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.lblSwordsmen.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick));
        this.lblSwordsmen.CustomTooltipID = 2800;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblSwordsmen);
        this.lblCatapults.Text = numCatapults.ToString();
        this.lblCatapults.Color = ARGBColors.Black;
        this.lblCatapults.RolloverColor = ARGBColors.White;
        this.lblCatapults.Position = new Point(545, 0);
        this.lblCatapults.Size = new Size(55, this.backgroundImage.Height);
        this.lblCatapults.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblCatapults.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.lblCatapults.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick));
        this.lblCatapults.CustomTooltipID = 2800;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblCatapults);
        this.btnBreakVassalage.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
        this.btnBreakVassalage.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
        this.btnBreakVassalage.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
        this.btnBreakVassalage.Position = new Point(706, 3);
        this.btnBreakVassalage.Text.Text = SK.Text("VassalControlSentLine_Break_Vassalage", "Break Vassalage");
        this.btnBreakVassalage.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.btnBreakVassalage.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.btnBreakVassalage.TextYOffset = -3;
        this.btnBreakVassalage.Text.Color = ARGBColors.Black;
        this.btnBreakVassalage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.breakVassalage), "VillageVassalsPanel_break_vassal_line");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnBreakVassalage);
        this.invalidate();
      }

      public bool update() => false;

      private void breakVassalage()
      {
        if (this.m_parent == null)
          return;
        this.m_parent.promptBreakFromYourVassal(this.m_villageID);
      }

      private void cancelVassalageRequest()
      {
        if (this.m_parent == null)
          return;
        this.m_parent.cancelRequest(this.m_villageID);
      }

      private void acceptVassalageRequest()
      {
        if (this.m_parent == null)
          return;
        this.m_parent.PromptAcceptRequest(this.m_villageID);
      }

      private void declineVassalageRequest()
      {
        if (this.m_parent == null)
          return;
        this.m_parent.declineRequest(this.m_villageID);
      }

      private void lblVillage_Click()
      {
        if (this.m_villageID < 0)
          return;
        Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.m_villageID);
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.closeParishPanel();
        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_villageID, false, true, true, false);
      }

      private void troopClick()
      {
        if (this.m_villageID < 0)
          return;
        GameEngine.Instance.playInterfaceSound("VillageVassalsPanel_troops");
        InterfaceMgr.Instance.setVassalArmiesVillage(this.m_villageID);
        InterfaceMgr.Instance.setVillageTabSubMode(15);
      }
    }
  }
}
