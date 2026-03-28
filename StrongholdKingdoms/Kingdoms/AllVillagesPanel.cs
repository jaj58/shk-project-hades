// Decompiled with JetBrains decompiler
// Type: Kingdoms.AllVillagesPanel
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
  public class AllVillagesPanel : CustomSelfDrawPanel, IDockableControl
  {
    private const int HEADER_SIZE = 47;
    private DockableControl dockableControl;
    private IContainer components;
    public static AllVillagesPanel instance = (AllVillagesPanel) null;
    private static DateTime lastUpdate = DateTime.MinValue;
    private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDExtendingPanel borderImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider4Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider5Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider6Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider7Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider8Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea rolloverArea1 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea rolloverArea2 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea rolloverArea3 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea rolloverArea4 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea rolloverArea5 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea rolloverArea6 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea rolloverArea7 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea rolloverArea8 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel villageLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage headerImage1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage headerImage2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage headerImage3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage headerImage4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage headerImage5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage headerImage6 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage headerImage7 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton tabBtnAll = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tabBtnTroops = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tabBtnUnits = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tabBtnVillage = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tabBtnResrouce = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel playersLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel membershipLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private int pageMode;
    private List<VillageSummaryData> allVillageData = new List<VillageSummaryData>();
    private static List<DateTime> tooltipDates = new List<DateTime>();
    public static List<VillageResourceReturnData> resourceReturnData = new List<VillageResourceReturnData>();
    private List<AllVillagesPanel.VillageOverviewLine> lineList = new List<AllVillagesPanel.VillageOverviewLine>();
    private AllVillagesPanel.NameComparer nameComparer = new AllVillagesPanel.NameComparer();

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
      this.SuspendLayout();
      this.AutoScaleMode = AutoScaleMode.None;
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (AllVillagesPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public AllVillagesPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      int height = this.Height;
      AllVillagesPanel.instance = this;
      this.clearControls();
      if (!resized)
        this.pageMode = 0;
      this.mainBackgroundImage.FillColor = Color.FromArgb(134, 153, 165);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = new Size(this.Width, height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundFade.Image = (Image) GFXLibrary.background_top;
      this.backgroundFade.Position = new Point(0, 0);
      this.backgroundFade.Size = new Size(this.Width, this.backgroundFade.Image.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundFade);
      this.titleLabel.Text = SK.Text("AllVillages_village_overview", "Villages Overview");
      this.titleLabel.Color = ARGBColors.Black;
      this.titleLabel.Position = new Point(5, 5);
      this.titleLabel.Size = new Size(323, 30);
      this.titleLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.titleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.titleLabel);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage, 43, new Point(this.Width - 44, 3));
      this.borderImage.Size = new Size(970, height - 38 - 7);
      this.borderImage.Position = new Point(10, 38);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.borderImage);
      this.borderImage.Create((Image) GFXLibrary.parishwall_village_center_tab_outline_top_left, (Image) GFXLibrary.parishwall_village_center_tab_outline_top_middle, (Image) GFXLibrary.parishwall_village_center_tab_outline_top_right, (Image) GFXLibrary.parishwall_village_center_tab_outline_middle_left, (Image) null, (Image) GFXLibrary.parishwall_village_center_tab_outline_middle_right, (Image) GFXLibrary.parishwall_village_center_tab_outline_bottom_left, (Image) GFXLibrary.parishwall_village_center_tab_outline_bottom_middle, (Image) GFXLibrary.parishwall_village_center_tab_outline_bottom_right);
      int num1 = 135;
      this.tabBtnAll.ImageNorm = (Image) GFXLibrary.villageOverTab_down;
      this.tabBtnAll.ImageOver = (Image) GFXLibrary.villageOverTab_down;
      this.tabBtnAll.ImageClick = (Image) GFXLibrary.villageOverTab_down;
      this.tabBtnAll.Position = new Point(235 + num1, 12);
      this.tabBtnAll.Text.Text = SK.Text("ALLVillages_Overview_Alt", "Overview");
      this.tabBtnAll.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.tabBtnAll.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.tabBtnAll.TextYOffset = 0;
      this.tabBtnAll.Text.Color = ARGBColors.Black;
      this.tabBtnAll.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabAllClicked));
      this.tabBtnAll.Active = true;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tabBtnAll);
      this.tabBtnTroops.ImageNorm = (Image) GFXLibrary.villageOverTab_down;
      this.tabBtnTroops.ImageOver = (Image) GFXLibrary.villageOverTab_down;
      this.tabBtnTroops.ImageClick = (Image) GFXLibrary.villageOverTab_down;
      this.tabBtnTroops.Position = new Point(370 + num1, 12);
      this.tabBtnTroops.Text.Text = SK.Text("SelectArmyPanel_Troops", "Troops");
      this.tabBtnTroops.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.tabBtnTroops.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.tabBtnTroops.TextYOffset = 0;
      this.tabBtnTroops.Text.Color = ARGBColors.Black;
      this.tabBtnTroops.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabTroopsClicked));
      this.tabBtnTroops.Active = true;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tabBtnTroops);
      this.tabBtnUnits.ImageNorm = (Image) GFXLibrary.villageOverTab_down;
      this.tabBtnUnits.ImageOver = (Image) GFXLibrary.villageOverTab_down;
      this.tabBtnUnits.ImageClick = (Image) GFXLibrary.villageOverTab_down;
      this.tabBtnUnits.Position = new Point(505 + num1, 12);
      this.tabBtnUnits.Text.Text = SK.Text("UnitsPanel_Units", "Units");
      this.tabBtnUnits.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.tabBtnUnits.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.tabBtnUnits.TextYOffset = 0;
      this.tabBtnUnits.Text.Color = ARGBColors.Black;
      this.tabBtnUnits.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabUnitsClicked));
      this.tabBtnUnits.Active = true;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tabBtnUnits);
      this.tabBtnVillage.ImageNorm = (Image) GFXLibrary.villageOverTab_down;
      this.tabBtnVillage.ImageOver = (Image) GFXLibrary.villageOverTab_down;
      this.tabBtnVillage.ImageClick = (Image) GFXLibrary.villageOverTab_down;
      this.tabBtnVillage.Position = new Point(640 + num1, 12);
      this.tabBtnVillage.Text.Text = SK.Text("GENERIC_Village", "Village");
      this.tabBtnVillage.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.tabBtnVillage.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.tabBtnVillage.TextYOffset = 0;
      this.tabBtnVillage.Text.Color = ARGBColors.Black;
      this.tabBtnVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabVillageClicked));
      this.tabBtnVillage.Active = true;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tabBtnVillage);
      this.headerLabelsImage.Size = new Size(this.Width - 25 - 23, -19);
      this.headerLabelsImage.Position = new Point(25, 56);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider1Image.Position = new Point(290, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider1Image);
      this.villageLabel.Text = SK.Text("GENERIC_Village", "Village");
      this.villageLabel.Color = ARGBColors.Black;
      this.villageLabel.Position = new Point(15, -3);
      this.villageLabel.Size = new Size(323, this.headerLabelsImage.Height);
      this.villageLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.villageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.villageLabel);
      if (this.pageMode == 0)
      {
        this.tabBtnAll.Active = false;
        this.tabBtnAll.ImageNorm = (Image) GFXLibrary.villageOverTab_up;
        this.tabBtnAll.ImageOver = (Image) GFXLibrary.villageOverTab_up;
        this.tabBtnAll.ImageClick = (Image) GFXLibrary.villageOverTab_up;
        this.tabBtnAll.TextYOffset = -3;
        this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider2Image.Position = new Point(375, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider2Image);
        this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider3Image.Position = new Point(460, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider3Image);
        this.divider4Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider4Image.Position = new Point(545, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider4Image);
        this.divider5Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider5Image.Position = new Point(630, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider5Image);
        this.divider6Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider6Image.Position = new Point(715, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider6Image);
        this.divider7Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider7Image.Position = new Point(800, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider7Image);
        this.headerImage1.Image = (Image) GFXLibrary.villageOverviewIcons[0];
        this.headerImage1.Position = new Point(290, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage1);
        this.headerImage2.Image = (Image) GFXLibrary.villageOverviewIcons[7];
        this.headerImage2.Position = new Point(375, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage2);
        this.headerImage3.Image = (Image) GFXLibrary.villageOverviewIcons[8];
        this.headerImage3.Position = new Point(460, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage3);
        this.headerImage4.Image = (Image) GFXLibrary.villageOverviewIcons[6];
        this.headerImage4.Position = new Point(545, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage4);
        this.headerImage5.Image = (Image) GFXLibrary.villageOverviewIcons[18];
        this.headerImage5.Position = new Point(630, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage5);
        this.headerImage6.Image = (Image) GFXLibrary.villageOverviewIcons[9];
        this.headerImage6.Position = new Point(715, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage6);
        this.rolloverArea1.Position = this.divider1Image.Position;
        this.rolloverArea1.Size = new Size(this.divider2Image.Position.X - this.divider1Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea1.CustomTooltipID = 4100;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea1);
        this.rolloverArea2.Position = this.divider2Image.Position;
        this.rolloverArea2.Size = new Size(this.divider3Image.Position.X - this.divider2Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea2.CustomTooltipID = 4101;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea2);
        this.rolloverArea3.Position = this.divider3Image.Position;
        this.rolloverArea3.Size = new Size(this.divider4Image.Position.X - this.divider3Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea3.CustomTooltipID = 4102;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea3);
        this.rolloverArea4.Position = this.divider4Image.Position;
        this.rolloverArea4.Size = new Size(this.divider5Image.Position.X - this.divider4Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea4.CustomTooltipID = 4103;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea4);
        this.rolloverArea5.Position = this.divider5Image.Position;
        this.rolloverArea5.Size = new Size(this.divider6Image.Position.X - this.divider5Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea5.CustomTooltipID = 4104;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea5);
        this.rolloverArea6.Position = this.divider6Image.Position;
        this.rolloverArea6.Size = new Size(this.divider7Image.Position.X - this.divider6Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea6.CustomTooltipID = 4105;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea6);
      }
      else if (this.pageMode == 1)
      {
        this.tabBtnTroops.Active = false;
        this.tabBtnTroops.ImageNorm = (Image) GFXLibrary.villageOverTab_up;
        this.tabBtnTroops.ImageOver = (Image) GFXLibrary.villageOverTab_up;
        this.tabBtnTroops.ImageClick = (Image) GFXLibrary.villageOverTab_up;
        this.tabBtnTroops.TextYOffset = -3;
        this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider2Image.Position = new Point(375, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider2Image);
        this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider3Image.Position = new Point(460, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider3Image);
        this.divider4Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider4Image.Position = new Point(545, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider4Image);
        this.divider5Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider5Image.Position = new Point(630, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider5Image);
        this.divider6Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider6Image.Position = new Point(715, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider6Image);
        this.headerImage1.Image = (Image) GFXLibrary.villageOverviewIcons[0];
        this.headerImage1.Position = new Point(290, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage1);
        this.headerImage2.Image = (Image) GFXLibrary.villageOverviewIcons[1];
        this.headerImage2.Position = new Point(375, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage2);
        this.headerImage3.Image = (Image) GFXLibrary.villageOverviewIcons[2];
        this.headerImage3.Position = new Point(460, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage3);
        this.headerImage4.Image = (Image) GFXLibrary.villageOverviewIcons[3];
        this.headerImage4.Position = new Point(545, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage4);
        this.headerImage5.Image = (Image) GFXLibrary.villageOverviewIcons[5];
        this.headerImage5.Position = new Point(630, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage5);
        this.headerImage6.Image = (Image) GFXLibrary.villageOverviewIcons[4];
        this.headerImage6.Position = new Point(715, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage6);
        this.rolloverArea1.Position = this.divider1Image.Position;
        this.rolloverArea1.Size = new Size(this.divider2Image.Position.X - this.divider1Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea1.CustomTooltipID = 4108;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea1);
        this.rolloverArea2.Position = this.divider2Image.Position;
        this.rolloverArea2.Size = new Size(this.divider3Image.Position.X - this.divider2Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea2.CustomTooltipID = 4109;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea2);
        this.rolloverArea3.Position = this.divider3Image.Position;
        this.rolloverArea3.Size = new Size(this.divider4Image.Position.X - this.divider3Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea3.CustomTooltipID = 4110;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea3);
        this.rolloverArea4.Position = this.divider4Image.Position;
        this.rolloverArea4.Size = new Size(this.divider5Image.Position.X - this.divider4Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea4.CustomTooltipID = 4111;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea4);
        this.rolloverArea5.Position = this.divider5Image.Position;
        this.rolloverArea5.Size = new Size(this.divider6Image.Position.X - this.divider5Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea5.CustomTooltipID = 4112;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea5);
        this.rolloverArea6.Position = this.divider6Image.Position;
        this.rolloverArea6.Size = this.rolloverArea1.Size;
        this.rolloverArea6.CustomTooltipID = 4113;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea6);
      }
      else if (this.pageMode == 2)
      {
        this.tabBtnUnits.Active = false;
        this.tabBtnUnits.ImageNorm = (Image) GFXLibrary.villageOverTab_up;
        this.tabBtnUnits.ImageOver = (Image) GFXLibrary.villageOverTab_up;
        this.tabBtnUnits.ImageClick = (Image) GFXLibrary.villageOverTab_up;
        this.tabBtnUnits.TextYOffset = -3;
        this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider3Image.Position = new Point(460, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider3Image);
        this.divider5Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider5Image.Position = new Point(630, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider5Image);
        this.headerImage1.Image = (Image) GFXLibrary.villageOverviewIcons[7];
        this.headerImage1.Position = new Point(330, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage1);
        this.headerImage2.Image = (Image) GFXLibrary.villageOverviewIcons[8];
        this.headerImage2.Position = new Point(500, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage2);
        this.headerImage3.Image = (Image) GFXLibrary.villageOverviewIcons[6];
        this.headerImage3.Position = new Point(670, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage3);
        this.rolloverArea1.Position = this.divider1Image.Position;
        this.rolloverArea1.Size = new Size(this.divider3Image.Position.X - this.divider1Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea1.CustomTooltipID = 4116;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea1);
        this.rolloverArea2.Position = this.divider3Image.Position;
        this.rolloverArea2.Size = new Size(this.divider5Image.Position.X - this.divider3Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea2.CustomTooltipID = 4117;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea2);
        this.rolloverArea3.Position = this.divider5Image.Position;
        this.rolloverArea3.Size = new Size(840 - this.divider5Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea3.CustomTooltipID = 4118;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea3);
      }
      else if (this.pageMode == 3)
      {
        this.tabBtnVillage.Active = false;
        this.tabBtnVillage.ImageNorm = (Image) GFXLibrary.villageOverTab_up;
        this.tabBtnVillage.ImageOver = (Image) GFXLibrary.villageOverTab_up;
        this.tabBtnVillage.ImageClick = (Image) GFXLibrary.villageOverTab_up;
        this.tabBtnVillage.TextYOffset = -3;
        this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider2Image.Position = new Point(375, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider2Image);
        this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider3Image.Position = new Point(460, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider3Image);
        this.divider4Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider4Image.Position = new Point(545, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider4Image);
        this.divider5Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider5Image.Position = new Point(650, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider5Image);
        this.divider6Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider6Image.Position = new Point(735, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider6Image);
        this.divider7Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider7Image.Position = new Point(820, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider7Image);
        this.headerImage1.Image = (Image) GFXLibrary.villageOverviewIcons[13];
        this.headerImage1.Position = new Point(290, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage1);
        this.headerImage2.Image = (Image) GFXLibrary.villageOverviewIcons[12];
        this.headerImage2.Position = new Point(375, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage2);
        this.headerImage3.Image = (Image) GFXLibrary.villageOverviewIcons[14];
        this.headerImage3.Position = new Point(460, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage3);
        this.headerImage4.Image = (Image) GFXLibrary.villageOverviewIcons[15];
        this.headerImage4.Position = new Point(555, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage4);
        this.headerImage5.Image = (Image) GFXLibrary.villageOverviewIcons[18];
        this.headerImage5.Position = new Point(650, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage5);
        this.headerImage6.Image = (Image) GFXLibrary.villageOverviewIcons[9];
        this.headerImage6.Position = new Point(735, -17);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage6);
        this.rolloverArea1.Position = this.divider1Image.Position;
        this.rolloverArea1.Size = new Size(this.divider2Image.Position.X - this.divider1Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea1.CustomTooltipID = 4119;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea1);
        this.rolloverArea2.Position = this.divider2Image.Position;
        this.rolloverArea2.Size = new Size(this.divider3Image.Position.X - this.divider2Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea2.CustomTooltipID = 4120;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea2);
        this.rolloverArea3.Position = this.divider3Image.Position;
        this.rolloverArea3.Size = new Size(this.divider4Image.Position.X - this.divider3Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea3.CustomTooltipID = 4121;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea3);
        this.rolloverArea4.Position = this.divider4Image.Position;
        this.rolloverArea4.Size = new Size(this.divider5Image.Position.X - this.divider4Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea4.CustomTooltipID = 4122;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea4);
        this.rolloverArea5.Position = this.divider5Image.Position;
        this.rolloverArea5.Size = new Size(this.divider6Image.Position.X - this.divider5Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea5.CustomTooltipID = 4104;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea5);
        this.rolloverArea6.Position = this.divider6Image.Position;
        this.rolloverArea6.Size = new Size(this.divider7Image.Position.X - this.divider6Image.Position.X, this.headerLabelsImage.Size.Height);
        this.rolloverArea6.CustomTooltipID = 4105;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea6);
      }
      else if (this.pageMode == 4)
      {
        this.tabBtnResrouce.Active = false;
        this.tabBtnResrouce.ImageNorm = (Image) GFXLibrary.villageOverTab_up;
        this.tabBtnResrouce.ImageOver = (Image) GFXLibrary.villageOverTab_up;
        this.tabBtnResrouce.ImageClick = (Image) GFXLibrary.villageOverTab_up;
        this.tabBtnResrouce.TextYOffset = -3;
        this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider2Image.Position = new Point(375, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider2Image);
        this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider3Image.Position = new Point(460, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider3Image);
        this.divider4Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider4Image.Position = new Point(545, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider4Image);
        this.divider5Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider5Image.Position = new Point(650, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider5Image);
        this.divider6Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider6Image.Position = new Point(735, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider6Image);
        this.divider7Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider7Image.Position = new Point(820, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider7Image);
        this.headerImage1.Image = (Image) GFXLibrary.donate_type_food;
        this.headerImage1.setSizeToImage();
        this.headerImage1.Position = new Point(this.divider1Image.X + (this.divider2Image.X - this.divider1Image.X) / 2 - this.headerImage1.Width / 2, -20);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage1);
        this.headerImage2.Image = (Image) GFXLibrary.com_32_ale_DS;
        this.headerImage2.setSizeToImage();
        this.headerImage2.Position = new Point(this.divider2Image.X + (this.divider3Image.X - this.divider2Image.X) / 2 - this.headerImage2.Width / 2, -8);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage2);
        this.headerImage3.Image = (Image) GFXLibrary.com_32_wood_DS;
        this.headerImage3.setSizeToImage();
        this.headerImage3.Position = new Point(this.divider3Image.X + (this.divider4Image.X - this.divider3Image.X) / 2 - this.headerImage3.Width / 2, -8);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage3);
        this.headerImage4.Image = (Image) GFXLibrary.com_32_stone_DS;
        this.headerImage4.setSizeToImage();
        this.headerImage4.Position = new Point(this.divider4Image.X + (this.divider5Image.X - this.divider4Image.X) / 2 - this.headerImage4.Width / 2, -8);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage4);
        this.headerImage5.Image = (Image) GFXLibrary.com_32_iron_DS;
        this.headerImage5.setSizeToImage();
        this.headerImage5.Position = new Point(this.divider5Image.X + (this.divider6Image.X - this.divider5Image.X) / 2 - this.headerImage5.Width / 2, -8);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage5);
        this.headerImage6.Image = (Image) GFXLibrary.com_32_pitch_DS;
        this.headerImage6.setSizeToImage();
        this.headerImage6.Position = new Point(this.divider6Image.X + (this.divider7Image.X - this.divider6Image.X) / 2 - this.headerImage6.Width / 2, -8);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage6);
        this.rolloverArea1.Position = this.divider1Image.Position;
        CustomSelfDrawPanel.CSDArea rolloverArea1 = this.rolloverArea1;
        int x1 = this.divider2Image.Position.X;
        Point position = this.divider1Image.Position;
        int x2 = position.X;
        Size size1 = new Size(x1 - x2, this.headerLabelsImage.Size.Height);
        rolloverArea1.Size = size1;
        this.rolloverArea1.CustomTooltipID = 144;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea1);
        this.rolloverArea2.Position = this.divider2Image.Position;
        CustomSelfDrawPanel.CSDArea rolloverArea2 = this.rolloverArea2;
        position = this.divider3Image.Position;
        int x3 = position.X;
        position = this.divider2Image.Position;
        int x4 = position.X;
        Size size2 = new Size(x3 - x4, this.headerLabelsImage.Size.Height);
        rolloverArea2.Size = size2;
        this.rolloverArea2.CustomTooltipID = 4121;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea2);
        this.rolloverArea3.Position = this.divider3Image.Position;
        CustomSelfDrawPanel.CSDArea rolloverArea3 = this.rolloverArea3;
        position = this.divider4Image.Position;
        int x5 = position.X;
        position = this.divider3Image.Position;
        int x6 = position.X;
        Size size3 = new Size(x5 - x6, this.headerLabelsImage.Size.Height);
        rolloverArea3.Size = size3;
        this.rolloverArea3.CustomTooltipID = 142;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea3);
        this.rolloverArea4.Position = this.divider4Image.Position;
        CustomSelfDrawPanel.CSDArea rolloverArea4 = this.rolloverArea4;
        position = this.divider5Image.Position;
        int x7 = position.X;
        position = this.divider4Image.Position;
        int x8 = position.X;
        Size size4 = new Size(x7 - x8, this.headerLabelsImage.Size.Height);
        rolloverArea4.Size = size4;
        this.rolloverArea4.CustomTooltipID = 143;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea4);
        this.rolloverArea5.Position = this.divider5Image.Position;
        CustomSelfDrawPanel.CSDArea rolloverArea5 = this.rolloverArea5;
        position = this.divider6Image.Position;
        int x9 = position.X;
        position = this.divider5Image.Position;
        int x10 = position.X;
        Size size5 = new Size(x9 - x10, this.headerLabelsImage.Size.Height);
        rolloverArea5.Size = size5;
        this.rolloverArea5.CustomTooltipID = 4127;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea5);
        this.rolloverArea6.Position = this.divider6Image.Position;
        CustomSelfDrawPanel.CSDArea rolloverArea6 = this.rolloverArea6;
        position = this.divider7Image.Position;
        int x11 = position.X;
        position = this.divider6Image.Position;
        int x12 = position.X;
        Size size6 = new Size(x11 - x12, this.headerLabelsImage.Size.Height);
        rolloverArea6.Size = size6;
        this.rolloverArea6.CustomTooltipID = 4128;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rolloverArea6);
      }
      this.wallScrollArea.Position = new Point(25, 85);
      this.wallScrollArea.Size = new Size(906, height - 85 - 9);
      this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(905, height - 85 - 10));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollArea);
      this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
      this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
      int num2 = this.wallScrollBar.Value;
      this.wallScrollBar.Position = new Point(933, 85);
      this.wallScrollBar.Size = new Size(24, height - 85 - 9);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollBar);
      this.wallScrollBar.Value = 0;
      this.wallScrollBar.Max = 100;
      this.wallScrollBar.NumVisibleLines = 25;
      this.wallScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      if (!resized)
      {
        if (GameEngine.Instance.World.isAccountPremium())
        {
          if ((DateTime.Now - AllVillagesPanel.lastUpdate).TotalSeconds > 30.0)
          {
            AllVillagesPanel.lastUpdate = DateTime.Now;
            RemoteServices.Instance.set_PremiumOverview_UserCallBack(new RemoteServices.PremiumOverview_UserCallBack(this.PremiumOverview_callback));
            RemoteServices.Instance.PremiumOverview();
          }
        }
        else
        {
          this.allVillageData.Clear();
          foreach (int userVillageId in GameEngine.Instance.World.getUserVillageIDList())
            this.allVillageData.Add(new VillageSummaryData()
            {
              villageID = userVillageId,
              fake = true
            });
        }
      }
      this.addVillages();
    }

    private void clearExpand()
    {
      foreach (VillageSummaryData villageSummaryData in this.allVillageData)
        villageSummaryData.expanded = false;
    }

    public static void travellersChanged() => AllVillagesPanel.lastUpdate = DateTime.MinValue;

    private void PremiumOverview_callback(PremiumOverview_ReturnType returnData)
    {
      if (returnData.Success && returnData.summaryData != null)
      {
        this.allVillageData = returnData.summaryData;
        this.allVillageData.Sort((IComparer<VillageSummaryData>) this.nameComparer);
        foreach (VillageSummaryData villageSummaryData in this.allVillageData)
        {
          GameEngine.Instance.World.getTotalTroopsOutOfVillage(villageSummaryData.villageID, ref villageSummaryData.numAttackingPeasants, ref villageSummaryData.numAttackingArchers, ref villageSummaryData.numAttackingPikemen, ref villageSummaryData.numAttackingSwordsmen, ref villageSummaryData.numAttackingCatapults, ref villageSummaryData.numAttackingCaptains, ref villageSummaryData.numReinforcingPeasants, ref villageSummaryData.numReinforcingArchers, ref villageSummaryData.numReinforcingPikemen, ref villageSummaryData.numReinforcingSwordsmen, ref villageSummaryData.numReinforcingCatapults, ref villageSummaryData.numReinforcingCaptains);
          villageSummaryData.numAttackingScouts = GameEngine.Instance.World.countYourArmyScouts(villageSummaryData.villageID);
          villageSummaryData.numTravellingMerchants = GameEngine.Instance.World.getTotalMerchantsFromVillage(villageSummaryData.villageID);
          int athome = 0;
          int num = GameEngine.Instance.World.countVillagePeople(villageSummaryData.villageID, 4, ref athome);
          villageSummaryData.numTravellingMonks = num - athome;
          villageSummaryData.numMonks = num;
        }
        foreach (VillageResourceReturnData resourceReturnData in returnData.resourceData)
          AllVillagesPanel.resourceReturnData.Add(resourceReturnData);
      }
      else
        this.allVillageData.Clear();
      this.addVillages();
    }

    private void tabAllClicked()
    {
      this.pageMode = 0;
      this.init(true);
    }

    private void tabTroopsClicked()
    {
      this.pageMode = 1;
      this.init(true);
    }

    private void tabUnitsClicked()
    {
      this.pageMode = 2;
      this.init(true);
    }

    private void tabVillageClicked()
    {
      this.pageMode = 3;
      this.init(true);
    }

    private void tabResourceClicked()
    {
      this.pageMode = 4;
      this.init(true);
    }

    public void update()
    {
    }

    public void logout() => this.allVillageData.Clear();

    private void wallScrollBarMoved()
    {
      int y = this.wallScrollBar.Value;
      this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 85 - y);
      this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
      this.wallScrollArea.invalidate();
      this.wallScrollBar.invalidate();
    }

    private void mouseWheelMoved(int delta)
    {
      if (!this.wallScrollBar.Visible)
        return;
      if (delta < 0)
      {
        this.wallScrollBar.scrollDown(40);
      }
      else
      {
        if (delta <= 0)
          return;
        this.wallScrollBar.scrollUp(40);
      }
    }

    public void closing() => InterfaceMgr.Instance.closeDonatePopup();

    public static string getTooltipDate(int id)
    {
      if (id >= 0 && id < AllVillagesPanel.tooltipDates.Count)
      {
        DateTime tooltipDate = AllVillagesPanel.tooltipDates[id];
        DateTime currentServerTime = VillageMap.getCurrentServerTime();
        if (tooltipDate > currentServerTime)
        {
          int totalSeconds = (int) (tooltipDate - currentServerTime).TotalSeconds;
          return SK.Text("TOOLTIP_DATE_ends", "Ends") + " : " + VillageMap.createBuildTimeString(totalSeconds);
        }
      }
      return "";
    }

    public int addTooltipDate(DateTime date)
    {
      AllVillagesPanel.tooltipDates.Add(date);
      return AllVillagesPanel.tooltipDates.Count - 1;
    }

    public void addVillages()
    {
      this.wallScrollArea.clearControls();
      AllVillagesPanel.tooltipDates.Clear();
      int num = 0;
      this.lineList.Clear();
      int position = 0;
      foreach (VillageSummaryData vsd in this.allVillageData)
      {
        AllVillagesPanel.VillageOverviewLine control = new AllVillagesPanel.VillageOverviewLine();
        if (num != 0)
          num += 5;
        control.Position = new Point(0, num);
        control.init(vsd, position, this.pageMode, vsd.expanded, this);
        this.wallScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num += control.Height;
        this.lineList.Add(control);
        ++position;
      }
      this.wallScrollArea.Size = new Size(this.wallScrollArea.Width, num);
      if (num < this.wallScrollBar.Height)
      {
        this.wallScrollBar.Value = 0;
        this.wallScrollBarMoved();
        this.wallScrollBar.Visible = false;
      }
      else
      {
        this.wallScrollBar.Visible = true;
        this.wallScrollBar.NumVisibleLines = this.wallScrollBar.Height;
        this.wallScrollBar.Max = num - this.wallScrollBar.Height;
      }
      this.wallScrollArea.invalidate();
      this.wallScrollBar.invalidate();
      this.update();
      this.Invalidate();
    }

    public void expand(int villageID)
    {
      foreach (VillageSummaryData villageSummaryData in this.allVillageData)
      {
        if (villageSummaryData.villageID == villageID)
        {
          villageSummaryData.expanded = !villageSummaryData.expanded;
          break;
        }
      }
      this.addVillages();
    }

    public class VillageOverviewLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel villageName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value1Label = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value2Label = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value3Label = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value4Label = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value5Label = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value6Label = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value1aLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value2aLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value3aLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value4aLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value5aLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value6aLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value1bLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value2bLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value3bLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value4bLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value5bLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value6bLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value1cLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value2cLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value3cLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value4cLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value5cLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value6cLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value1dLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value2dLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value3dLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value4dLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value5dLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel value6dLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel barracksLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel placedLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel attackingLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel reinforcingLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage enclosedImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage damageImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage idImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage excomdImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage peaceImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDButton expandButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDArea idRollover = new CustomSelfDrawPanel.CSDArea();
      private CustomSelfDrawPanel.CSDArea peaceRollover = new CustomSelfDrawPanel.CSDArea();
      private CustomSelfDrawPanel.CSDArea excomRollover = new CustomSelfDrawPanel.CSDArea();
      private CustomSelfDrawPanel.CSDArea clickArea = new CustomSelfDrawPanel.CSDArea();
      private int m_position = -1000;
      private VillageSummaryData m_vsd;
      private AllVillagesPanel m_parent;

      public void init(
        VillageSummaryData vsd,
        int position,
        int pageMode,
        bool expanded,
        AllVillagesPanel parent)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.ClipVisible = true;
        this.m_vsd = vsd;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_02_dark : (Image) GFXLibrary.lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(10, 0);
        this.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = new Size(890, this.backgroundImage.Size.Height);
        int height = GFXLibrary.lineitem_strip_02_light.Height;
        NumberFormatInfo nfi = GameEngine.NFI;
        this.villageName.Text = GameEngine.Instance.World.getVillageName(vsd.villageID);
        this.villageName.Color = ARGBColors.Black;
        this.villageName.Position = new Point(19, 0);
        this.villageName.Size = new Size(220, height);
        this.villageName.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.villageName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.villageName);
        int num1 = 275;
        switch (pageMode)
        {
          case 0:
            this.value1Label.Text = (vsd.numAttackingArchers + vsd.numAttackingCaptains + vsd.numAttackingCatapults + vsd.numAttackingPeasants + vsd.numAttackingPikemen + vsd.numAttackingSwordsmen + (vsd.numLocalArchers + vsd.numLocalCaptains + vsd.numLocalCatapults + vsd.numLocalPeasants + vsd.numLocalPikemen + vsd.numLocalSwordsmen) + (vsd.numPlacedArchers + vsd.numPlacedCaptains + vsd.numPlacedPeasants + vsd.numPlacedPikemen + vsd.numPlacedSwordsmen) + (vsd.numReinforcingArchers + vsd.numReinforcingCaptains + vsd.numReinforcingCatapults + vsd.numReinforcingPeasants + vsd.numReinforcingPikemen + vsd.numReinforcingSwordsmen)).ToString();
            if (vsd.fake)
              this.value1Label.Text = "?";
            this.value1Label.Color = ARGBColors.Black;
            this.value1Label.Position = new Point(num1 + 5, 0);
            this.value1Label.Size = new Size(70, height);
            this.value1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value1Label);
            this.value2Label.Text = (vsd.numAttackingScouts + vsd.numLocalScouts).ToString();
            if (vsd.fake)
              this.value2Label.Text = "?";
            this.value2Label.Color = ARGBColors.Black;
            this.value2Label.Position = new Point(num1 + 85 + 5, 0);
            this.value2Label.Size = new Size(60, height);
            this.value2Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value2Label);
            this.value3Label.Text = (vsd.numMerchantsAtHome + vsd.numTravellingMerchants).ToString();
            if (vsd.fake)
              this.value3Label.Text = "?";
            this.value3Label.Color = ARGBColors.Black;
            this.value3Label.Position = new Point(num1 + 170 + 5, 0);
            this.value3Label.Size = new Size(60, height);
            this.value3Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value3Label);
            this.value4Label.Text = vsd.numMonks.ToString();
            if (vsd.fake)
              this.value4Label.Text = "?";
            this.value4Label.Color = ARGBColors.Black;
            this.value4Label.Position = new Point(num1 + (int) byte.MaxValue + 5, 0);
            this.value4Label.Size = new Size(60, height);
            this.value4Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value4Label);
            int popularityLevel1 = vsd.popularityLevel;
            this.value5Label.Text = popularityLevel1.ToString();
            if (vsd.fake)
              this.value5Label.Text = "?";
            this.value5Label.Color = ARGBColors.Black;
            if (popularityLevel1 < 0)
              this.value5Label.Color = Color.FromArgb(170, 0, 0);
            this.value5Label.Position = new Point(num1 + 340 + 5, 0);
            this.value5Label.Size = new Size(60, height);
            this.value5Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value5Label);
            this.value6Label.Text = vsd.numBuildings.ToString();
            if (vsd.fake)
              this.value6Label.Text = "?";
            this.value6Label.Color = ARGBColors.Black;
            this.value6Label.Position = new Point(num1 + 425 + 5, 0);
            this.value6Label.Size = new Size(60, height);
            this.value6Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value6Label);
            if (!vsd.fake)
            {
              if (vsd.enclosedKeep)
              {
                this.enclosedImage.Image = (Image) GFXLibrary.villageOverviewIcons[10];
                this.idRollover.CustomTooltipID = 4106;
                this.enclosedImage.Position = new Point(num1 + 510, -15);
              }
              else
              {
                this.enclosedImage.Image = (Image) GFXLibrary.villageOverviewIcons[11];
                this.idRollover.CustomTooltipID = 4107;
                this.enclosedImage.Position = new Point(num1 + 510 + 2, -15);
              }
              this.addControl((CustomSelfDrawPanel.CSDControl) this.enclosedImage);
              this.idRollover.Size = new Size(25, 25);
              this.idRollover.Position = new Point(num1 + 510 + 29, 4);
              this.addControl((CustomSelfDrawPanel.CSDControl) this.idRollover);
              this.enclosedImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedCastle));
            }
            if (vsd.castleDamaged)
            {
              this.damageImage.Image = (Image) GFXLibrary.castle_damage;
              this.damageImage.setSizeToImage();
              this.damageImage.Position = new Point(num1 + 595 - 20, -6);
              this.damageImage.CustomTooltipID = 4140;
              this.damageImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedCastle));
              this.addControl((CustomSelfDrawPanel.CSDControl) this.damageImage);
              break;
            }
            break;
          case 1:
            this.value1Label.Text = (vsd.numAttackingPeasants + vsd.numLocalPeasants + vsd.numPlacedPeasants + vsd.numReinforcingPeasants).ToString();
            if (vsd.fake)
              this.value1Label.Text = "?";
            this.value1Label.Color = ARGBColors.Black;
            this.value1Label.Position = new Point(num1 + 5, 0);
            this.value1Label.Size = new Size(70, height);
            this.value1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value1Label);
            this.value2Label.Text = (vsd.numAttackingArchers + vsd.numLocalArchers + vsd.numPlacedArchers + vsd.numReinforcingArchers).ToString();
            if (vsd.fake)
              this.value2Label.Text = "?";
            this.value2Label.Color = ARGBColors.Black;
            this.value2Label.Position = new Point(num1 + 85 + 5, 0);
            this.value2Label.Size = new Size(60, height);
            this.value2Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value2Label);
            this.value3Label.Text = (vsd.numAttackingPikemen + vsd.numLocalPikemen + vsd.numPlacedPikemen + vsd.numReinforcingPikemen).ToString();
            if (vsd.fake)
              this.value3Label.Text = "?";
            this.value3Label.Color = ARGBColors.Black;
            this.value3Label.Position = new Point(num1 + 170 + 5, 0);
            this.value3Label.Size = new Size(60, height);
            this.value3Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value3Label);
            this.value4Label.Text = (vsd.numAttackingSwordsmen + vsd.numLocalSwordsmen + vsd.numPlacedSwordsmen + vsd.numReinforcingSwordsmen).ToString();
            if (vsd.fake)
              this.value4Label.Text = "?";
            this.value4Label.Color = ARGBColors.Black;
            this.value4Label.Position = new Point(num1 + (int) byte.MaxValue + 5, 0);
            this.value4Label.Size = new Size(60, height);
            this.value4Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value4Label);
            this.value5Label.Text = (vsd.numAttackingCatapults + vsd.numLocalCatapults + vsd.numReinforcingCatapults).ToString();
            if (vsd.fake)
              this.value5Label.Text = "?";
            this.value5Label.Color = ARGBColors.Black;
            this.value5Label.Position = new Point(num1 + 340 + 5, 0);
            this.value5Label.Size = new Size(60, height);
            this.value5Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value5Label);
            this.value6Label.Text = (vsd.numAttackingCaptains + vsd.numLocalCaptains + vsd.numReinforcingCaptains + vsd.numPlacedCaptains).ToString();
            if (vsd.fake)
              this.value6Label.Text = "?";
            this.value6Label.Color = ARGBColors.Black;
            this.value6Label.Position = new Point(num1 + 425 + 5, 0);
            this.value6Label.Size = new Size(60, height);
            this.value6Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value6Label);
            if (!vsd.fake)
            {
              if (expanded)
              {
                this.Size = new Size(890, this.backgroundImage.Size.Height + 100);
                this.barracksLabel.Text = SK.Text("BARRACKS_In_Barracks", "In Barracks");
                this.barracksLabel.Color = ARGBColors.Black;
                this.barracksLabel.Position = new Point(0, 26);
                this.barracksLabel.Size = new Size(295, height);
                this.barracksLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.barracksLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.barracksLabel);
                this.value1aLabel.Text = vsd.numLocalPeasants.ToString();
                this.value1aLabel.Color = ARGBColors.Black;
                this.value1aLabel.Position = new Point(num1 + 5, 26);
                this.value1aLabel.Size = new Size(70, height);
                this.value1aLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value1aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value1aLabel);
                this.value2aLabel.Text = vsd.numLocalArchers.ToString();
                this.value2aLabel.Color = ARGBColors.Black;
                this.value2aLabel.Position = new Point(num1 + 85 + 5, 26);
                this.value2aLabel.Size = new Size(60, height);
                this.value2aLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value2aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value2aLabel);
                this.value3aLabel.Text = vsd.numLocalPikemen.ToString();
                this.value3aLabel.Color = ARGBColors.Black;
                this.value3aLabel.Position = new Point(num1 + 170 + 5, 26);
                this.value3aLabel.Size = new Size(60, height);
                this.value3aLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value3aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value3aLabel);
                this.value4aLabel.Text = vsd.numLocalSwordsmen.ToString();
                this.value4aLabel.Color = ARGBColors.Black;
                this.value4aLabel.Position = new Point(num1 + (int) byte.MaxValue + 5, 26);
                this.value4aLabel.Size = new Size(60, height);
                this.value4aLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value4aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value4aLabel);
                this.value5aLabel.Text = vsd.numLocalCatapults.ToString();
                this.value5aLabel.Color = ARGBColors.Black;
                this.value5aLabel.Position = new Point(num1 + 340 + 5, 26);
                this.value5aLabel.Size = new Size(60, height);
                this.value5aLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value5aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value5aLabel);
                this.value6aLabel.Text = vsd.numLocalCaptains.ToString();
                this.value6aLabel.Color = ARGBColors.Black;
                this.value6aLabel.Position = new Point(num1 + 425 + 5, 26);
                this.value6aLabel.Size = new Size(60, height);
                this.value6aLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value6aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value6aLabel);
                this.placedLabel.Text = SK.Text("BARRACKS_In_Castle", "In Castle");
                this.placedLabel.Color = ARGBColors.Black;
                this.placedLabel.Position = new Point(0, 51);
                this.placedLabel.Size = new Size(295, height);
                this.placedLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.placedLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.placedLabel);
                this.value1bLabel.Text = vsd.numPlacedPeasants.ToString();
                this.value1bLabel.Color = ARGBColors.Black;
                this.value1bLabel.Position = new Point(num1 + 5, 51);
                this.value1bLabel.Size = new Size(70, height);
                this.value1bLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value1bLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value1bLabel);
                this.value2bLabel.Text = vsd.numPlacedArchers.ToString();
                this.value2bLabel.Color = ARGBColors.Black;
                this.value2bLabel.Position = new Point(num1 + 85 + 5, 51);
                this.value2bLabel.Size = new Size(60, height);
                this.value2bLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value2bLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value2bLabel);
                this.value3bLabel.Text = vsd.numPlacedPikemen.ToString();
                this.value3bLabel.Color = ARGBColors.Black;
                this.value3bLabel.Position = new Point(num1 + 170 + 5, 51);
                this.value3bLabel.Size = new Size(60, height);
                this.value3bLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value3bLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value3bLabel);
                this.value4bLabel.Text = vsd.numPlacedSwordsmen.ToString();
                this.value4bLabel.Color = ARGBColors.Black;
                this.value4bLabel.Position = new Point(num1 + (int) byte.MaxValue + 5, 51);
                this.value4bLabel.Size = new Size(60, height);
                this.value4bLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value4bLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value4bLabel);
                this.value6bLabel.Text = vsd.numPlacedCaptains.ToString();
                this.value6bLabel.Color = ARGBColors.Black;
                this.value6bLabel.Position = new Point(num1 + 425 + 5, 51);
                this.value6bLabel.Size = new Size(60, height);
                this.value6bLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value6bLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value6bLabel);
                this.attackingLabel.Text = SK.Text("GENERIC_Attacking", "Attacking");
                this.attackingLabel.Color = ARGBColors.Black;
                this.attackingLabel.Position = new Point(0, 76);
                this.attackingLabel.Size = new Size(295, height);
                this.attackingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.attackingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.attackingLabel);
                this.value1cLabel.Text = vsd.numAttackingPeasants.ToString();
                this.value1cLabel.Color = ARGBColors.Black;
                this.value1cLabel.Position = new Point(num1 + 5, 76);
                this.value1cLabel.Size = new Size(70, height);
                this.value1cLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value1cLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value1cLabel);
                this.value2cLabel.Text = vsd.numAttackingArchers.ToString();
                this.value2cLabel.Color = ARGBColors.Black;
                this.value2cLabel.Position = new Point(num1 + 85 + 5, 76);
                this.value2cLabel.Size = new Size(60, height);
                this.value2cLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value2cLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value2cLabel);
                this.value3cLabel.Text = vsd.numAttackingPikemen.ToString();
                this.value3cLabel.Color = ARGBColors.Black;
                this.value3cLabel.Position = new Point(num1 + 170 + 5, 76);
                this.value3cLabel.Size = new Size(60, height);
                this.value3cLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value3cLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value3cLabel);
                this.value4cLabel.Text = vsd.numAttackingSwordsmen.ToString();
                this.value4cLabel.Color = ARGBColors.Black;
                this.value4cLabel.Position = new Point(num1 + (int) byte.MaxValue + 5, 76);
                this.value4cLabel.Size = new Size(60, height);
                this.value4cLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value4cLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value4cLabel);
                this.value5cLabel.Text = vsd.numAttackingCatapults.ToString();
                this.value5cLabel.Color = ARGBColors.Black;
                this.value5cLabel.Position = new Point(num1 + 340 + 5, 76);
                this.value5cLabel.Size = new Size(60, height);
                this.value5cLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value5cLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value5cLabel);
                this.value6cLabel.Text = vsd.numAttackingCaptains.ToString();
                this.value6cLabel.Color = ARGBColors.Black;
                this.value6cLabel.Position = new Point(num1 + 425 + 5, 76);
                this.value6cLabel.Size = new Size(60, height);
                this.value6cLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value6cLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value6cLabel);
                this.reinforcingLabel.Text = SK.Text("BARRACKS_Reinforcing", "Reinforcing");
                this.reinforcingLabel.Color = ARGBColors.Black;
                this.reinforcingLabel.Position = new Point(0, 101);
                this.reinforcingLabel.Size = new Size(295, height);
                this.reinforcingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.reinforcingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.reinforcingLabel);
                this.value1dLabel.Text = vsd.numReinforcingPeasants.ToString();
                this.value1dLabel.Color = ARGBColors.Black;
                this.value1dLabel.Position = new Point(num1 + 5, 101);
                this.value1dLabel.Size = new Size(70, height);
                this.value1dLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value1dLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value1dLabel);
                this.value2dLabel.Text = vsd.numReinforcingArchers.ToString();
                this.value2dLabel.Color = ARGBColors.Black;
                this.value2dLabel.Position = new Point(num1 + 85 + 5, 101);
                this.value2dLabel.Size = new Size(60, height);
                this.value2dLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value2dLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value2dLabel);
                this.value3dLabel.Text = vsd.numReinforcingPikemen.ToString();
                this.value3dLabel.Color = ARGBColors.Black;
                this.value3dLabel.Position = new Point(num1 + 170 + 5, 101);
                this.value3dLabel.Size = new Size(60, height);
                this.value3dLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value3dLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value3dLabel);
                this.value4dLabel.Text = vsd.numReinforcingSwordsmen.ToString();
                this.value4dLabel.Color = ARGBColors.Black;
                this.value4dLabel.Position = new Point(num1 + (int) byte.MaxValue + 5, 101);
                this.value4dLabel.Size = new Size(60, height);
                this.value4dLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value4dLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value4dLabel);
                this.value5dLabel.Text = vsd.numReinforcingCatapults.ToString();
                this.value5dLabel.Color = ARGBColors.Black;
                this.value5dLabel.Position = new Point(num1 + 340 + 5, 101);
                this.value5dLabel.Size = new Size(60, height);
                this.value5dLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.value5dLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.addControl((CustomSelfDrawPanel.CSDControl) this.value5dLabel);
                this.expandButton.ImageNorm = (Image) GFXLibrary.blue_screen_button_array[1];
                this.expandButton.ImageOver = (Image) GFXLibrary.blue_screen_button_array[3];
                this.expandButton.ImageClick = (Image) GFXLibrary.blue_screen_button_array[5];
                this.expandButton.Position = new Point(840, 2);
                this.expandButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.expandClick));
                this.addControl((CustomSelfDrawPanel.CSDControl) this.expandButton);
                break;
              }
              this.expandButton.ImageNorm = (Image) GFXLibrary.blue_screen_button_array[0];
              this.expandButton.ImageOver = (Image) GFXLibrary.blue_screen_button_array[2];
              this.expandButton.ImageClick = (Image) GFXLibrary.blue_screen_button_array[4];
              this.expandButton.Position = new Point(840, 2);
              this.expandButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.expandClick));
              this.addControl((CustomSelfDrawPanel.CSDControl) this.expandButton);
              break;
            }
            break;
          case 2:
            int numAttackingScouts = vsd.numAttackingScouts;
            int numLocalScouts = vsd.numLocalScouts;
            this.value1Label.Text = (numAttackingScouts + numLocalScouts).ToString();
            if (vsd.fake)
              this.value1Label.Text = "?";
            this.value1Label.Color = ARGBColors.Black;
            this.value1Label.Position = new Point(304, 0);
            this.value1Label.Size = new Size(70, height);
            this.value1Label.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.value1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value1Label);
            if (numAttackingScouts > 0 && !vsd.fake)
            {
              this.value2Label.Text = "(" + numAttackingScouts.ToString() + ")";
              this.value2Label.Color = ARGBColors.Black;
              this.value2Label.Position = new Point(379, 0);
              this.value2Label.Size = new Size(70, height);
              this.value2Label.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
              this.value2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
              this.addControl((CustomSelfDrawPanel.CSDControl) this.value2Label);
            }
            int travellingMerchants = vsd.numTravellingMerchants;
            int numMerchantsAtHome = vsd.numMerchantsAtHome;
            this.value3Label.Text = (travellingMerchants + numMerchantsAtHome).ToString();
            if (vsd.fake)
              this.value3Label.Text = "?";
            this.value3Label.Color = ARGBColors.Black;
            this.value3Label.Position = new Point(474, 0);
            this.value3Label.Size = new Size(70, height);
            this.value3Label.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.value3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value3Label);
            if (travellingMerchants > 0 && !vsd.fake)
            {
              this.value4Label.Text = "(" + travellingMerchants.ToString() + ")";
              this.value4Label.Color = ARGBColors.Black;
              this.value4Label.Position = new Point(549, 0);
              this.value4Label.Size = new Size(70, height);
              this.value4Label.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
              this.value4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
              this.addControl((CustomSelfDrawPanel.CSDControl) this.value4Label);
            }
            int numTravellingMonks = vsd.numTravellingMonks;
            this.value5Label.Text = vsd.numMonks.ToString();
            if (vsd.fake)
              this.value5Label.Text = "?";
            this.value5Label.Color = ARGBColors.Black;
            this.value5Label.Position = new Point(644, 0);
            this.value5Label.Size = new Size(70, height);
            this.value5Label.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.value5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value5Label);
            if (numTravellingMonks > 0 && !vsd.fake)
            {
              this.value6Label.Text = "(" + numTravellingMonks.ToString() + ")";
              this.value6Label.Color = ARGBColors.Black;
              this.value6Label.Position = new Point(719, 0);
              this.value6Label.Size = new Size(70, height);
              this.value6Label.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
              this.value6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
              this.addControl((CustomSelfDrawPanel.CSDControl) this.value6Label);
              break;
            }
            break;
          case 3:
            int num2 = vsd.totalPeople;
            if (vsd.housingCapacity < vsd.totalPeople)
              num2 = vsd.housingCapacity;
            double num3 = (double) num2 * VillageBuildingsData.getTaxIncomeLevel(vsd.setTaxLevel, GameEngine.Instance.cardsManager.UserCardData) * GameEngine.Instance.LocalWorldData.goldIncomeRate;
            this.value1Label.Color = ARGBColors.Black;
            string str1;
            if (num3 > 0.0)
              str1 = "+" + ((int) num3).ToString("N", (IFormatProvider) nfi);
            else if (num3 < 0.0)
            {
              str1 = ((int) num3).ToString("N", (IFormatProvider) nfi);
              this.value1Label.Color = Color.FromArgb((int) byte.MaxValue, 200, 0);
            }
            else
              str1 = "0";
            this.value1Label.Text = str1;
            if (vsd.fake)
              this.value1Label.Text = "?";
            this.value1Label.Position = new Point(num1 + 5, 0);
            this.value1Label.Size = new Size(70, height);
            this.value1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value1Label);
            string str2 = "";
            string str3 = "";
            string str4 = vsd.currentRationsLevel < 6.0 ? (vsd.currentRationsLevel < 5.0 ? (vsd.currentRationsLevel < 4.0 ? (vsd.currentRationsLevel < 3.0 ? (vsd.currentRationsLevel < 2.0 ? (vsd.currentRationsLevel < 1.0 ? "0" : "1/4") : "1/2") : "x1") : "x2") : "x3") : "x4";
            this.value2Label.Text = str2 + str4;
            if (vsd.fake)
              this.value2Label.Text = "?";
            this.value2Label.Color = ARGBColors.Black;
            if ((double) vsd.setRationsLevel != vsd.currentRationsLevel)
              this.value2Label.Color = Color.FromArgb(170, 0, 0);
            this.value2Label.Position = new Point(num1 + 85 + 5, 0);
            this.value2Label.Size = new Size(60, height);
            this.value2Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value2Label);
            string str5 = "";
            str3 = "";
            string str6 = vsd.currentAleLevel < 4.0 ? (vsd.currentAleLevel < 3.0 ? (vsd.currentAleLevel < 2.0 ? (vsd.currentAleLevel < 1.0 ? "0" : "x1") : "x2") : "x3") : "x4";
            this.value3Label.Text = str5 + str6;
            if (vsd.fake)
              this.value3Label.Text = "?";
            this.value3Label.Color = ARGBColors.Black;
            if ((double) vsd.setAleLevel != vsd.currentAleLevel)
              this.value3Label.Color = Color.FromArgb(170, 0, 0);
            this.value3Label.Position = new Point(num1 + 170 + 5, 0);
            this.value3Label.Size = new Size(60, height);
            this.value3Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value3Label);
            this.value4Label.Text = vsd.totalPeople.ToString() + " / " + vsd.housingCapacity.ToString() + " ";
            if (vsd.fake)
              this.value4Label.Text = "?";
            this.value4Label.Color = ARGBColors.Black;
            this.value4Label.Position = new Point(num1 + (int) byte.MaxValue + 5, 0);
            this.value4Label.Size = new Size(75, height);
            this.value4Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value4Label);
            this.value4aLabel.Text = vsd.sparePeople.ToString();
            if (vsd.fake)
              this.value4aLabel.Text = "?";
            this.value4aLabel.Color = ARGBColors.Black;
            this.value4aLabel.Position = new Point(num1 + (int) byte.MaxValue + 5 + 11 + 20, 0);
            this.value4aLabel.Size = new Size(80, height);
            this.value4aLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value4aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value4aLabel);
            int popularityLevel2 = vsd.popularityLevel;
            this.value5Label.Text = popularityLevel2.ToString();
            if (vsd.fake)
              this.value5Label.Text = "?";
            this.value5Label.Color = ARGBColors.Black;
            if (popularityLevel2 < 0)
              this.value5Label.Color = Color.FromArgb(170, 0, 0);
            this.value5Label.Position = new Point(num1 + 340 + 5 + 20, 0);
            this.value5Label.Size = new Size(70, height);
            this.value5Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value5Label);
            this.value6Label.Text = vsd.numBuildings.ToString();
            if (vsd.fake)
              this.value6Label.Text = "?";
            this.value6Label.Color = ARGBColors.Black;
            this.value6Label.Position = new Point(num1 + 425 + 5 + 20, 0);
            this.value6Label.Size = new Size(60, height);
            this.value6Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.value6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.value6Label);
            if (!vsd.fake)
            {
              int num4 = num1 + 510 + 20;
              int x = num1 + 510 + 20 + 15;
              if (vsd.interdictProtectionEndTime > VillageMap.getCurrentServerTime())
              {
                this.idImage.Image = (Image) GFXLibrary.villageOverviewIcons[16];
                this.idImage.Position = new Point((int) ((double) num4 / 0.6), -2);
                this.idImage.setScale(0.6);
                this.addControl((CustomSelfDrawPanel.CSDControl) this.idImage);
                num4 += 29;
                this.idRollover.Size = new Size(22, 22);
                this.idRollover.Position = new Point(x, 4);
                this.idRollover.CustomTooltipID = 4123;
                this.idRollover.CustomTooltipData = parent.addTooltipDate(vsd.interdictProtectionEndTime);
                this.addControl((CustomSelfDrawPanel.CSDControl) this.idRollover);
                x += 29;
              }
              if (vsd.excommunicationEndTime > VillageMap.getCurrentServerTime())
              {
                this.excomdImage.Image = (Image) GFXLibrary.villageOverviewIcons[17];
                this.excomdImage.Position = new Point((int) ((double) num4 / 0.6), -2);
                this.excomdImage.setScale(0.6);
                this.addControl((CustomSelfDrawPanel.CSDControl) this.excomdImage);
                num4 += 29;
                this.excomRollover.Size = new Size(22, 22);
                this.excomRollover.Position = new Point(x, 4);
                this.excomRollover.CustomTooltipID = 4124;
                this.excomRollover.CustomTooltipData = parent.addTooltipDate(vsd.excommunicationEndTime);
                this.addControl((CustomSelfDrawPanel.CSDControl) this.excomRollover);
                x += 29;
              }
              if (vsd.peaceTimeEndTime > VillageMap.getCurrentServerTime())
              {
                this.peaceImage.Image = (Image) GFXLibrary.villageOverviewIcons[19];
                this.peaceImage.Position = new Point((int) ((double) num4 / 0.6), -2);
                this.peaceImage.setScale(0.6);
                this.addControl((CustomSelfDrawPanel.CSDControl) this.peaceImage);
                int num5 = num4 + 29;
                this.peaceRollover.Size = new Size(22, 22);
                this.peaceRollover.Position = new Point(x, 4);
                this.peaceRollover.CustomTooltipID = 4125;
                this.peaceRollover.CustomTooltipData = parent.addTooltipDate(vsd.peaceTimeEndTime);
                this.addControl((CustomSelfDrawPanel.CSDControl) this.peaceRollover);
                int num6 = x + 29;
                break;
              }
              break;
            }
            break;
          case 4:
            using (List<VillageResourceReturnData>.Enumerator enumerator = AllVillagesPanel.resourceReturnData.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                VillageResourceReturnData current = enumerator.Current;
                if (vsd.villageID == current.villageID)
                {
                  this.value1Label.Text = Math.Max(0, Convert.ToInt32(Math.Floor(current.applesLevel + current.breadLevel + current.cheeseLevel + current.fishLevel + current.meatLevel + current.vegLevel))).ToString();
                  this.value1Label.Color = ARGBColors.Black;
                  this.value1Label.Position = new Point(num1 + 5, 0);
                  this.value1Label.Size = new Size(70, height);
                  this.value1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                  this.value1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                  this.addControl((CustomSelfDrawPanel.CSDControl) this.value1Label);
                  this.value2Label.Text = current.aleLevel.ToString();
                  this.value2Label.Color = ARGBColors.Black;
                  this.value2Label.Position = new Point(num1 + 85 + 5, 0);
                  this.value2Label.Size = new Size(60, height);
                  this.value2Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                  this.value2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                  this.addControl((CustomSelfDrawPanel.CSDControl) this.value2Label);
                  this.value3Label.Text = current.woodLevel.ToString();
                  this.value3Label.Color = ARGBColors.Black;
                  this.value3Label.Position = new Point(num1 + 170 + 5, 0);
                  this.value3Label.Size = new Size(60, height);
                  this.value3Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                  this.value3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                  this.addControl((CustomSelfDrawPanel.CSDControl) this.value3Label);
                  this.value4Label.Text = current.stoneLevel.ToString();
                  this.value4Label.Color = ARGBColors.Black;
                  this.value4Label.Position = new Point(num1 + (int) byte.MaxValue + 5, 0);
                  this.value4Label.Size = new Size(75, height);
                  this.value4Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                  this.value4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                  this.addControl((CustomSelfDrawPanel.CSDControl) this.value4Label);
                  this.value5Label.Text = current.ironLevel.ToString();
                  this.value5Label.Color = ARGBColors.Black;
                  this.value5Label.Position = new Point(num1 + 340 + 5 + 20, 0);
                  this.value5Label.Size = new Size(70, height);
                  this.value5Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                  this.value5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                  this.addControl((CustomSelfDrawPanel.CSDControl) this.value5Label);
                  this.value6Label.Text = current.pitchLevel.ToString();
                  this.value6Label.Color = ARGBColors.Black;
                  this.value6Label.Position = new Point(num1 + 425 + 5 + 20, 0);
                  this.value6Label.Size = new Size(60, height);
                  this.value6Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                  this.value6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                  this.addControl((CustomSelfDrawPanel.CSDControl) this.value6Label);
                }
              }
              break;
            }
        }
        this.clickArea.Position = new Point(0, 0);
        this.clickArea.Size = new Size(790, this.backgroundImage.Height);
        this.clickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        if (vsd.fake)
          this.clickArea.CustomTooltipID = 4126;
        else
          this.clickArea.CustomTooltipID = 0;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.clickArea);
        this.invalidate();
      }

      public void update()
      {
      }

      private void expandClick()
      {
        if (this.m_parent == null)
          return;
        this.m_parent.expand(this.m_vsd.villageID);
      }

      public void clickedLine()
      {
        if (this.m_vsd == null)
          return;
        GameEngine.Instance.playInterfaceSound("UserinfoScreenLine_village");
        Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.m_vsd.villageID);
        GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
        InterfaceMgr.Instance.closeParishPanel();
        InterfaceMgr.Instance.getMainTabBar().changeTab(0);
        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_vsd.villageID, false, true, true, false);
      }

      public void clickedCastle()
      {
        InterfaceMgr.Instance.closeParishPanel();
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_vsd.villageID, false, true, true, false);
        GameEngine.Instance.SkipVillageTab();
        InterfaceMgr.Instance.getMainTabBar().changeTab(1);
        InterfaceMgr.Instance.getVillageTabBar().changeTab(1);
      }
    }

    public class NameComparer : IComparer<VillageSummaryData>
    {
      public int Compare(VillageSummaryData x, VillageSummaryData y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        int num = GameEngine.Instance.World.getVillageName(x.villageID).ToLowerInvariant().CompareTo(GameEngine.Instance.World.getVillageName(y.villageID).ToLowerInvariant());
        if (num != 0)
          return num;
        if (x.villageID < y.villageID)
          return -1;
        return x.villageID > y.villageID ? 1 : 0;
      }
    }
  }
}
