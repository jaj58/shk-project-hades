// Decompiled with JetBrains decompiler
// Type: Kingdoms.CapitalBarracksPanel
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
  public class CapitalBarracksPanel : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    public static CapitalBarracksPanel instance;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea mainBackgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel barracksLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage troop1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troop2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troop3Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troop4Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troop5Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton troopsMade1Disband = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopsMade2Disband = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopsMade3Disband = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopsMade4Disband = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopsMade5Disband = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel troopGoldCost1Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopGoldCost2Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopGoldCost3Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopGoldCost4Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopGoldCost5Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton troopMake1Button1 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake2Button1 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake3Button1 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake4Button1 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake5Button1 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake1Button5 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake2Button5 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake3Button5 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake4Button5 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake5Button5 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake1Button20 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake2Button20 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake3Button20 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake4Button20 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake5Button20 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage troopGold1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troopGold2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troopGold3Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troopGold4Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troopGold5Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel troopCtrl1Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopCtrl2Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopCtrl3Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopCtrl4Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopCtrl5Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage goldImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel goldLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel CurrentTroopsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel SpaceLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel MaxLabelLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDColorBar fullBar = new CustomSelfDrawPanel.CSDColorBar();
    private CustomSelfDrawPanel.CSDLabel BottomHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel LocalLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel InCastleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel AttackingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel ReinforcingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage smallPeasantImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel localPeasantsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel localArchersLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel localPikmenLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel localSwordsmenLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel localCatapultsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel localCaptainsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel inCastlePeasantsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel inCastleArchersLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel inCastlePikmenLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel inCastleSwordsmenLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel inCastleCatapultsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel inCastleCaptainsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel attackingPeasantsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel attackingArchersLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel attackingPikmenLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel attackingSwordsmenLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel attackingCatapultsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel attackingCaptainsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel reinforcingPeasantsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel reinforcingArchersLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel reinforcingPikmenLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel reinforcingSwordsmenLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel reinforcingCatapultsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel reinforcingCaptainsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton attackInfoButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton reinforcementInfoButton = new CustomSelfDrawPanel.CSDButton();
    private int makePeasants = 10;
    private int makeArchers = 10;
    private int makePikemen = 10;
    private int makeSwordsmen = 10;
    private int makeCatapults = 10;
    private int mouseOver = -1;
    private DisbandTroopsPopup m_disbandPopup;

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
      this.closeDisbandPopup();
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
      this.MaximumSize = new Size(992, 566);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (CapitalBarracksPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public CapitalBarracksPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init()
    {
      CapitalBarracksPanel.instance = this;
      this.clearControls();
      this.mainBackgroundImage.Image = (Image) GFXLibrary.barracks_background;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.mainBackgroundArea.Position = new Point(0, 0);
      this.mainBackgroundArea.Size = new Size(992, 566);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea);
      InterfaceMgr.Instance.setVillageHeading(SK.Text("CapitalBarracksPanel_Mercenaries", "Mercenaries"));
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(948, 10);
      this.closeButton.CustomTooltipID = 601;
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CapitalBarracksPanel_close");
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea, 13, new Point(898, 10));
      this.troopsMade1Disband.ImageNorm = (Image) GFXLibrary.barracks_little_button_normal;
      this.troopsMade1Disband.ImageOver = (Image) GFXLibrary.barracks_little_button_over;
      this.troopsMade1Disband.Position = new Point(109, 46);
      this.troopsMade1Disband.Text.Text = "0";
      this.troopsMade1Disband.Text.Color = ARGBColors.Black;
      this.troopsMade1Disband.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopsMade1Disband.TextYOffset = 0;
      this.troopsMade1Disband.Data = 70;
      this.troopsMade1Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "CapitalBarracksPanel_disband_peasants");
      this.troopsMade1Disband.CustomTooltipID = 600;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.troopsMade1Disband);
      this.troop1Image.Image = (Image) GFXLibrary.barracks_unit_peasant;
      this.troop1Image.Position = new Point(12, 12);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.troop1Image);
      this.troopGoldCost1Label.Text = "0";
      this.troopGoldCost1Label.Color = Color.FromArgb(208, 165, 102);
      this.troopGoldCost1Label.Position = new Point(69, 165);
      this.troopGoldCost1Label.Size = new Size(44, 47);
      this.troopGoldCost1Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopGoldCost1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.troop1Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGoldCost1Label);
      int y1 = 231;
      this.troopMake1Button1.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
      this.troopMake1Button1.ImageOver = (Image) GFXLibrary.button3comp_left_over;
      this.troopMake1Button1.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
      this.troopMake1Button1.Position = new Point(10, y1);
      this.troopMake1Button1.Text.Text = "1";
      this.troopMake1Button1.TextYOffset = 1;
      this.troopMake1Button1.Text.Size = new Size(this.troopMake1Button1.Width - 5, this.troopMake1Button1.Height);
      this.troopMake1Button1.Text.Position = new Point(5, 0);
      this.troopMake1Button1.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake1Button1.Text.Color = ARGBColors.Black;
      this.troopMake1Button1.Data = 70;
      this.troopMake1Button1.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake1Button1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_peasants");
      this.troop1Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake1Button1);
      this.troopMake1Button5.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
      this.troopMake1Button5.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
      this.troopMake1Button5.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
      this.troopMake1Button5.Position = new Point(60, y1);
      this.troopMake1Button5.Text.Text = "5";
      this.troopMake1Button5.TextYOffset = 1;
      this.troopMake1Button5.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake1Button5.Text.Color = ARGBColors.Black;
      this.troopMake1Button5.Data = 1070;
      this.troopMake1Button5.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake1Button5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_peasants");
      this.troop1Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake1Button5);
      this.troopMake1Button20.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
      this.troopMake1Button20.ImageOver = (Image) GFXLibrary.button3comp_right_over;
      this.troopMake1Button20.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
      this.troopMake1Button20.Position = new Point(108, y1);
      this.troopMake1Button20.Text.Text = "20";
      this.troopMake1Button20.TextYOffset = 1;
      this.troopMake1Button20.Text.Size = new Size(this.troopMake1Button20.Width - 5, this.troopMake1Button20.Height);
      this.troopMake1Button20.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake1Button20.Text.Color = ARGBColors.Black;
      this.troopMake1Button20.Data = 2070;
      this.troopMake1Button20.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake1Button20.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_peasants");
      this.troop1Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake1Button20);
      this.troopGold1Image.Image = (Image) GFXLibrary.com_32_money;
      this.troopGold1Image.Position = new Point(117, 173);
      this.troop1Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGold1Image);
      this.troopsMade2Disband.ImageNorm = (Image) GFXLibrary.barracks_little_button_normal;
      this.troopsMade2Disband.ImageOver = (Image) GFXLibrary.barracks_little_button_over;
      this.troopsMade2Disband.Position = new Point(270, 46);
      this.troopsMade2Disband.Text.Text = "0";
      this.troopsMade2Disband.Text.Color = ARGBColors.Black;
      this.troopsMade2Disband.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopsMade2Disband.Data = 72;
      this.troopsMade2Disband.TextYOffset = 0;
      this.troopsMade2Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "CapitalBarracksPanel_disband_archers");
      this.troopsMade2Disband.CustomTooltipID = 600;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.troopsMade2Disband);
      this.troop2Image.Image = (Image) GFXLibrary.barracks_unit_archer;
      this.troop2Image.Position = new Point(173, 12);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.troop2Image);
      this.troopGoldCost2Label.Text = "0";
      this.troopGoldCost2Label.Color = Color.FromArgb(208, 165, 102);
      this.troopGoldCost2Label.Position = new Point(69, 165);
      this.troopGoldCost2Label.Size = new Size(44, 47);
      this.troopGoldCost2Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopGoldCost2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.troop2Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGoldCost2Label);
      this.troopMake2Button1.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
      this.troopMake2Button1.ImageOver = (Image) GFXLibrary.button3comp_left_over;
      this.troopMake2Button1.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
      this.troopMake2Button1.Position = new Point(10, y1);
      this.troopMake2Button1.Text.Text = "1";
      this.troopMake2Button1.TextYOffset = 1;
      this.troopMake2Button1.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake2Button1.Text.Color = ARGBColors.Black;
      this.troopMake2Button1.Text.Size = new Size(this.troopMake2Button1.Width - 5, this.troopMake2Button1.Height);
      this.troopMake2Button1.Text.Position = new Point(5, 0);
      this.troopMake2Button1.Data = 72;
      this.troopMake2Button1.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake2Button1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_archers");
      this.troop2Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake2Button1);
      this.troopMake2Button5.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
      this.troopMake2Button5.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
      this.troopMake2Button5.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
      this.troopMake2Button5.Position = new Point(60, y1);
      this.troopMake2Button5.Text.Text = "5";
      this.troopMake2Button5.TextYOffset = 1;
      this.troopMake2Button5.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake2Button5.Text.Color = ARGBColors.Black;
      this.troopMake2Button5.Data = 1072;
      this.troopMake2Button5.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake2Button5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_archers");
      this.troop2Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake2Button5);
      this.troopMake2Button20.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
      this.troopMake2Button20.ImageOver = (Image) GFXLibrary.button3comp_right_over;
      this.troopMake2Button20.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
      this.troopMake2Button20.Position = new Point(108, y1);
      this.troopMake2Button20.Text.Text = "20";
      this.troopMake2Button20.TextYOffset = 1;
      this.troopMake2Button20.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake2Button20.Text.Color = ARGBColors.Black;
      this.troopMake2Button20.Text.Size = new Size(this.troopMake2Button20.Width - 5, this.troopMake2Button20.Height);
      this.troopMake2Button20.Data = 2072;
      this.troopMake2Button20.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake2Button20.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_archers");
      this.troop2Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake2Button20);
      this.troopGold2Image.Image = (Image) GFXLibrary.com_32_money;
      this.troopGold2Image.Position = new Point(117, 173);
      this.troop2Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGold2Image);
      this.troopsMade3Disband.ImageNorm = (Image) GFXLibrary.barracks_little_button_normal;
      this.troopsMade3Disband.ImageOver = (Image) GFXLibrary.barracks_little_button_over;
      this.troopsMade3Disband.Position = new Point(431, 46);
      this.troopsMade3Disband.Text.Text = "0";
      this.troopsMade3Disband.Text.Color = ARGBColors.Black;
      this.troopsMade3Disband.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopsMade3Disband.TextYOffset = 0;
      this.troopsMade3Disband.Data = 73;
      this.troopsMade3Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "CapitalBarracksPanel_disband_pikemen");
      this.troopsMade3Disband.CustomTooltipID = 600;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.troopsMade3Disband);
      this.troop3Image.Image = (Image) GFXLibrary.barracks_unit_pikemen;
      this.troop3Image.Position = new Point(334, 12);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.troop3Image);
      this.troopGoldCost3Label.Text = "0";
      this.troopGoldCost3Label.Color = Color.FromArgb(208, 165, 102);
      this.troopGoldCost3Label.Position = new Point(69, 165);
      this.troopGoldCost3Label.Size = new Size(44, 47);
      this.troopGoldCost3Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopGoldCost3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.troop3Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGoldCost3Label);
      this.troopMake3Button1.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
      this.troopMake3Button1.ImageOver = (Image) GFXLibrary.button3comp_left_over;
      this.troopMake3Button1.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
      this.troopMake3Button1.Position = new Point(10, y1);
      this.troopMake3Button1.Text.Text = "1";
      this.troopMake3Button1.TextYOffset = 1;
      this.troopMake3Button1.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake3Button1.Text.Color = ARGBColors.Black;
      this.troopMake3Button1.Text.Size = new Size(this.troopMake3Button1.Width - 5, this.troopMake3Button1.Height);
      this.troopMake3Button1.Text.Position = new Point(5, 0);
      this.troopMake3Button1.Data = 73;
      this.troopMake3Button1.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake3Button1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_pikemen");
      this.troop3Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake3Button1);
      this.troopMake3Button5.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
      this.troopMake3Button5.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
      this.troopMake3Button5.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
      this.troopMake3Button5.Position = new Point(60, y1);
      this.troopMake3Button5.Text.Text = "5";
      this.troopMake3Button5.TextYOffset = 1;
      this.troopMake3Button5.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake3Button5.Text.Color = ARGBColors.Black;
      this.troopMake3Button5.Data = 1073;
      this.troopMake3Button5.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake3Button5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_pikemen");
      this.troop3Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake3Button5);
      this.troopMake3Button20.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
      this.troopMake3Button20.ImageOver = (Image) GFXLibrary.button3comp_right_over;
      this.troopMake3Button20.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
      this.troopMake3Button20.Position = new Point(108, y1);
      this.troopMake3Button20.Text.Text = "20";
      this.troopMake3Button20.TextYOffset = 1;
      this.troopMake3Button20.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake3Button20.Text.Color = ARGBColors.Black;
      this.troopMake3Button20.Text.Size = new Size(this.troopMake3Button20.Width - 5, this.troopMake3Button20.Height);
      this.troopMake3Button20.Data = 2073;
      this.troopMake3Button20.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake3Button20.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_pikemen");
      this.troop3Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake3Button20);
      this.troopGold3Image.Image = (Image) GFXLibrary.com_32_money;
      this.troopGold3Image.Position = new Point(117, 173);
      this.troop3Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGold3Image);
      this.troopsMade4Disband.ImageNorm = (Image) GFXLibrary.barracks_little_button_normal;
      this.troopsMade4Disband.ImageOver = (Image) GFXLibrary.barracks_little_button_over;
      this.troopsMade4Disband.Position = new Point(592, 46);
      this.troopsMade4Disband.Text.Text = "0";
      this.troopsMade4Disband.Text.Color = ARGBColors.Black;
      this.troopsMade4Disband.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopsMade4Disband.TextYOffset = 0;
      this.troopsMade4Disband.Data = 71;
      this.troopsMade4Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "CapitalBarracksPanel_disband_swordsmen");
      this.troopsMade4Disband.CustomTooltipID = 600;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.troopsMade4Disband);
      this.troop4Image.Image = (Image) GFXLibrary.barracks_unit_swordsman;
      this.troop4Image.Position = new Point(495, 12);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.troop4Image);
      this.troopGoldCost4Label.Text = "0";
      this.troopGoldCost4Label.Color = Color.FromArgb(208, 165, 102);
      this.troopGoldCost4Label.Position = new Point(69, 165);
      this.troopGoldCost4Label.Size = new Size(44, 47);
      this.troopGoldCost4Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopGoldCost4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.troop4Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGoldCost4Label);
      this.troopMake4Button1.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
      this.troopMake4Button1.ImageOver = (Image) GFXLibrary.button3comp_left_over;
      this.troopMake4Button1.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
      this.troopMake4Button1.Position = new Point(10, y1);
      this.troopMake4Button1.Text.Text = "1";
      this.troopMake4Button1.TextYOffset = 1;
      this.troopMake4Button1.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake4Button1.Text.Color = ARGBColors.Black;
      this.troopMake4Button1.Text.Size = new Size(this.troopMake4Button1.Width - 5, this.troopMake4Button1.Height);
      this.troopMake4Button1.Text.Position = new Point(5, 0);
      this.troopMake4Button1.Data = 71;
      this.troopMake4Button1.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake4Button1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_swordsmen");
      this.troop4Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake4Button1);
      this.troopMake4Button5.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
      this.troopMake4Button5.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
      this.troopMake4Button5.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
      this.troopMake4Button5.Position = new Point(60, y1);
      this.troopMake4Button5.Text.Text = "5";
      this.troopMake4Button5.TextYOffset = 1;
      this.troopMake4Button5.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake4Button5.Text.Color = ARGBColors.Black;
      this.troopMake4Button5.Data = 1071;
      this.troopMake4Button5.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake4Button5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_swordsmen");
      this.troop4Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake4Button5);
      this.troopMake4Button20.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
      this.troopMake4Button20.ImageOver = (Image) GFXLibrary.button3comp_right_over;
      this.troopMake4Button20.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
      this.troopMake4Button20.Position = new Point(108, y1);
      this.troopMake4Button20.Text.Text = "20";
      this.troopMake4Button20.TextYOffset = 1;
      this.troopMake4Button20.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake4Button20.Text.Color = ARGBColors.Black;
      this.troopMake4Button20.Text.Size = new Size(this.troopMake4Button20.Width - 5, this.troopMake4Button20.Height);
      this.troopMake4Button20.Data = 2071;
      this.troopMake4Button20.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake4Button20.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_swordsmen");
      this.troop4Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake4Button20);
      this.troopGold4Image.Image = (Image) GFXLibrary.com_32_money;
      this.troopGold4Image.Position = new Point(117, 173);
      this.troop4Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGold4Image);
      this.troopsMade5Disband.ImageNorm = (Image) GFXLibrary.barracks_little_button_normal;
      this.troopsMade5Disband.ImageOver = (Image) GFXLibrary.barracks_little_button_over;
      this.troopsMade5Disband.Position = new Point(753, 46);
      this.troopsMade5Disband.Text.Text = "0";
      this.troopsMade5Disband.Text.Color = ARGBColors.Black;
      this.troopsMade5Disband.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopsMade5Disband.TextYOffset = 0;
      this.troopsMade5Disband.Data = 74;
      this.troopsMade5Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "CapitalBarracksPanel_disband_catapults");
      this.troopsMade5Disband.CustomTooltipID = 600;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.troopsMade5Disband);
      this.troop5Image.Image = (Image) GFXLibrary.barracks_unit_catapult;
      this.troop5Image.Position = new Point(656, 12);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.troop5Image);
      this.troopGoldCost5Label.Text = "0";
      this.troopGoldCost5Label.Color = Color.FromArgb(208, 165, 102);
      this.troopGoldCost5Label.Position = new Point(69, 165);
      this.troopGoldCost5Label.Size = new Size(44, 47);
      this.troopGoldCost5Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopGoldCost5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.troop5Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGoldCost5Label);
      this.troopMake5Button1.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
      this.troopMake5Button1.ImageOver = (Image) GFXLibrary.button3comp_left_over;
      this.troopMake5Button1.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
      this.troopMake5Button1.Position = new Point(10, y1);
      this.troopMake5Button1.Text.Text = "1";
      this.troopMake5Button1.TextYOffset = 1;
      this.troopMake5Button1.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake5Button1.Text.Color = ARGBColors.Black;
      this.troopMake5Button1.Text.Size = new Size(this.troopMake5Button1.Width - 5, this.troopMake5Button1.Height);
      this.troopMake5Button1.Text.Position = new Point(5, 0);
      this.troopMake5Button1.Data = 74;
      this.troopMake5Button1.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake5Button1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_catapults");
      this.troop5Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake5Button1);
      this.troopMake5Button5.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
      this.troopMake5Button5.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
      this.troopMake5Button5.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
      this.troopMake5Button5.Position = new Point(60, y1);
      this.troopMake5Button5.Text.Text = "5";
      this.troopMake5Button5.TextYOffset = 1;
      this.troopMake5Button5.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake5Button5.Text.Color = ARGBColors.Black;
      this.troopMake5Button5.Data = 1074;
      this.troopMake5Button5.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake5Button5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_catapults");
      this.troop5Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake5Button5);
      this.troopMake5Button20.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
      this.troopMake5Button20.ImageOver = (Image) GFXLibrary.button3comp_right_over;
      this.troopMake5Button20.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
      this.troopMake5Button20.Position = new Point(108, y1);
      this.troopMake5Button20.Text.Text = "20";
      this.troopMake5Button20.TextYOffset = 1;
      this.troopMake5Button20.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake5Button20.Text.Color = ARGBColors.Black;
      this.troopMake5Button20.Text.Size = new Size(this.troopMake5Button20.Width - 5, this.troopMake5Button20.Height);
      this.troopMake5Button20.Data = 2074;
      this.troopMake5Button20.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake5Button20.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_catapults");
      this.troop5Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake5Button20);
      this.troopGold5Image.Image = (Image) GFXLibrary.com_32_money;
      this.troopGold5Image.Position = new Point(117, 173);
      this.troop5Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGold5Image);
      int num1 = 73;
      this.goldImage.Image = (Image) GFXLibrary.com_32_money;
      this.goldImage.Position = new Point(num1 + 7, 340);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.goldImage);
      this.goldLabel.Text = "0";
      this.goldLabel.Color = ARGBColors.Black;
      this.goldLabel.Position = new Point(num1 + 7 + 32, 340);
      this.goldLabel.Size = new Size(300, this.goldImage.Image.Height);
      this.goldLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.goldLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.goldLabel);
      this.CurrentTroopsLabel.Text = SK.Text("BARRACKS_Troops", "Troops") + ": 0";
      this.CurrentTroopsLabel.Color = ARGBColors.Black;
      this.CurrentTroopsLabel.Position = new Point(560, 347);
      this.CurrentTroopsLabel.Size = new Size(200, 20);
      this.CurrentTroopsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.CurrentTroopsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.CurrentTroopsLabel);
      this.MaxLabelLabel.Text = SK.Text("CapitalBarracksPanel_Max_Army_Size", "Max Army Size") + ": 0";
      this.MaxLabelLabel.Color = ARGBColors.Black;
      this.MaxLabelLabel.Position = new Point(770, 357);
      this.MaxLabelLabel.Size = new Size(200, 20);
      this.MaxLabelLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.MaxLabelLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.MaxLabelLabel);
      this.fullBar.setImages((Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right, (Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right);
      this.fullBar.SetMargin(2, 2, 2, 3);
      this.fullBar.Number = 0.0;
      this.fullBar.MaxValue = 9.0;
      this.fullBar.Position = new Point(770, 339);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.fullBar);
      int y2 = 442;
      this.BottomHeaderLabel.Text = SK.Text("BARRACKS_Troop_Information", "Troop Information");
      this.BottomHeaderLabel.Color = Color.FromArgb(242, 202, 118);
      this.BottomHeaderLabel.DropShadowColor = ARGBColors.Black;
      this.BottomHeaderLabel.Position = new Point(54, 402);
      this.BottomHeaderLabel.Size = new Size(226, 27);
      this.BottomHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.BottomHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.BottomHeaderLabel);
      int num2 = 25;
      this.LocalLabel.Text = SK.Text("BARRACKS_In_Barracks", "In Barracks");
      this.LocalLabel.Color = ARGBColors.Black;
      this.LocalLabel.Position = new Point(79, y2);
      this.LocalLabel.Size = new Size(226, 27);
      this.LocalLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.LocalLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.LocalLabel);
      this.InCastleLabel.Text = SK.Text("BARRACKS_In_Castle", "In Castle");
      this.InCastleLabel.Color = ARGBColors.Black;
      this.InCastleLabel.Position = new Point(79, y2 + num2);
      this.InCastleLabel.Size = new Size(226, 27);
      this.InCastleLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.InCastleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.InCastleLabel);
      this.AttackingLabel.Text = SK.Text("GENERIC_Attacking", "Attacking");
      this.AttackingLabel.Color = ARGBColors.Black;
      this.AttackingLabel.Position = new Point(79, y2 + 2 * num2);
      this.AttackingLabel.Size = new Size(226, 27);
      this.AttackingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.AttackingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.AttackingLabel);
      this.ReinforcingLabel.Text = SK.Text("BARRACKS_Reinforcing", "Reinforcing");
      this.ReinforcingLabel.Color = ARGBColors.Black;
      this.ReinforcingLabel.Position = new Point(79, y2 + 3 * num2);
      this.ReinforcingLabel.Size = new Size(226, 27);
      this.ReinforcingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.ReinforcingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.ReinforcingLabel);
      this.smallPeasantImage.Image = (Image) GFXLibrary.barracks_screen_bottom_units;
      this.smallPeasantImage.Position = new Point(370, 394);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.smallPeasantImage);
      int num3 = 85;
      int x = 344;
      this.localPeasantsLabel.Text = "0";
      this.localPeasantsLabel.Color = ARGBColors.Black;
      this.localPeasantsLabel.Position = new Point(x, y2);
      this.localPeasantsLabel.Size = new Size(50, 27);
      this.localPeasantsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.localPeasantsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.localPeasantsLabel);
      this.localArchersLabel.Text = "0";
      this.localArchersLabel.Color = ARGBColors.Black;
      this.localArchersLabel.Position = new Point(x + num3, y2);
      this.localArchersLabel.Size = new Size(50, 27);
      this.localArchersLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.localArchersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.localArchersLabel);
      this.localPikmenLabel.Text = "0";
      this.localPikmenLabel.Color = ARGBColors.Black;
      this.localPikmenLabel.Position = new Point(x + 2 * num3, y2);
      this.localPikmenLabel.Size = new Size(50, 27);
      this.localPikmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.localPikmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.localPikmenLabel);
      this.localSwordsmenLabel.Text = "0";
      this.localSwordsmenLabel.Color = ARGBColors.Black;
      this.localSwordsmenLabel.Position = new Point(x + 3 * num3, y2);
      this.localSwordsmenLabel.Size = new Size(50, 27);
      this.localSwordsmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.localSwordsmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.localSwordsmenLabel);
      this.localCatapultsLabel.Text = "0";
      this.localCatapultsLabel.Color = ARGBColors.Black;
      this.localCatapultsLabel.Position = new Point(x + 4 * num3, y2);
      this.localCatapultsLabel.Size = new Size(50, 27);
      this.localCatapultsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.localCatapultsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.localCatapultsLabel);
      this.localCaptainsLabel.Text = "0";
      this.localCaptainsLabel.Color = ARGBColors.Black;
      this.localCaptainsLabel.Position = new Point(x + 5 * num3, y2);
      this.localCaptainsLabel.Size = new Size(50, 27);
      this.localCaptainsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.localCaptainsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.localCaptainsLabel);
      this.inCastlePeasantsLabel.Text = "0";
      this.inCastlePeasantsLabel.Color = ARGBColors.Black;
      this.inCastlePeasantsLabel.Position = new Point(x, y2 + num2);
      this.inCastlePeasantsLabel.Size = new Size(50, 27);
      this.inCastlePeasantsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.inCastlePeasantsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.inCastlePeasantsLabel);
      this.inCastleArchersLabel.Text = "0";
      this.inCastleArchersLabel.Color = ARGBColors.Black;
      this.inCastleArchersLabel.Position = new Point(x + num3, y2 + num2);
      this.inCastleArchersLabel.Size = new Size(50, 27);
      this.inCastleArchersLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.inCastleArchersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.inCastleArchersLabel);
      this.inCastlePikmenLabel.Text = "0";
      this.inCastlePikmenLabel.Color = ARGBColors.Black;
      this.inCastlePikmenLabel.Position = new Point(x + 2 * num3, y2 + num2);
      this.inCastlePikmenLabel.Size = new Size(50, 27);
      this.inCastlePikmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.inCastlePikmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.inCastlePikmenLabel);
      this.inCastleSwordsmenLabel.Text = "0";
      this.inCastleSwordsmenLabel.Color = ARGBColors.Black;
      this.inCastleSwordsmenLabel.Position = new Point(x + 3 * num3, y2 + num2);
      this.inCastleSwordsmenLabel.Size = new Size(50, 27);
      this.inCastleSwordsmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.inCastleSwordsmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.inCastleSwordsmenLabel);
      this.inCastleCatapultsLabel.Text = "0";
      this.inCastleCatapultsLabel.Color = ARGBColors.Black;
      this.inCastleCatapultsLabel.Position = new Point(x + 4 * num3, y2 + num2);
      this.inCastleCatapultsLabel.Size = new Size(50, 27);
      this.inCastleCatapultsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.inCastleCatapultsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.inCastleCatapultsLabel);
      this.inCastleCaptainsLabel.Text = "0";
      this.inCastleCaptainsLabel.Color = ARGBColors.Black;
      this.inCastleCaptainsLabel.Position = new Point(x + 5 * num3, y2 + num2);
      this.inCastleCaptainsLabel.Size = new Size(50, 27);
      this.inCastleCaptainsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.inCastleCaptainsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.inCastleCaptainsLabel);
      this.attackingPeasantsLabel.Text = "0";
      this.attackingPeasantsLabel.Color = ARGBColors.Black;
      this.attackingPeasantsLabel.Position = new Point(x, y2 + 2 * num2);
      this.attackingPeasantsLabel.Size = new Size(50, 27);
      this.attackingPeasantsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.attackingPeasantsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.attackingPeasantsLabel);
      this.attackingArchersLabel.Text = "0";
      this.attackingArchersLabel.Color = ARGBColors.Black;
      this.attackingArchersLabel.Position = new Point(x + num3, y2 + 2 * num2);
      this.attackingArchersLabel.Size = new Size(50, 27);
      this.attackingArchersLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.attackingArchersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.attackingArchersLabel);
      this.attackingPikmenLabel.Text = "0";
      this.attackingPikmenLabel.Color = ARGBColors.Black;
      this.attackingPikmenLabel.Position = new Point(x + 2 * num3, y2 + 2 * num2);
      this.attackingPikmenLabel.Size = new Size(50, 27);
      this.attackingPikmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.attackingPikmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.attackingPikmenLabel);
      this.attackingSwordsmenLabel.Text = "0";
      this.attackingSwordsmenLabel.Color = ARGBColors.Black;
      this.attackingSwordsmenLabel.Position = new Point(x + 3 * num3, y2 + 2 * num2);
      this.attackingSwordsmenLabel.Size = new Size(50, 27);
      this.attackingSwordsmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.attackingSwordsmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.attackingSwordsmenLabel);
      this.attackingCatapultsLabel.Text = "0";
      this.attackingCatapultsLabel.Color = ARGBColors.Black;
      this.attackingCatapultsLabel.Position = new Point(x + 4 * num3, y2 + 2 * num2);
      this.attackingCatapultsLabel.Size = new Size(50, 27);
      this.attackingCatapultsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.attackingCatapultsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.attackingCatapultsLabel);
      this.attackingCaptainsLabel.Text = "0";
      this.attackingCaptainsLabel.Color = ARGBColors.Black;
      this.attackingCaptainsLabel.Position = new Point(x + 5 * num3, y2 + 2 * num2);
      this.attackingCaptainsLabel.Size = new Size(50, 27);
      this.attackingCaptainsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.attackingCaptainsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.attackingCaptainsLabel);
      this.reinforcingPeasantsLabel.Text = "0";
      this.reinforcingPeasantsLabel.Color = ARGBColors.Black;
      this.reinforcingPeasantsLabel.Position = new Point(x, y2 + 3 * num2);
      this.reinforcingPeasantsLabel.Size = new Size(50, 27);
      this.reinforcingPeasantsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.reinforcingPeasantsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.reinforcingPeasantsLabel);
      this.reinforcingArchersLabel.Text = "0";
      this.reinforcingArchersLabel.Color = ARGBColors.Black;
      this.reinforcingArchersLabel.Position = new Point(x + num3, y2 + 3 * num2);
      this.reinforcingArchersLabel.Size = new Size(50, 27);
      this.reinforcingArchersLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.reinforcingArchersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.reinforcingArchersLabel);
      this.reinforcingPikmenLabel.Text = "0";
      this.reinforcingPikmenLabel.Color = ARGBColors.Black;
      this.reinforcingPikmenLabel.Position = new Point(x + 2 * num3, y2 + 3 * num2);
      this.reinforcingPikmenLabel.Size = new Size(50, 27);
      this.reinforcingPikmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.reinforcingPikmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.reinforcingPikmenLabel);
      this.reinforcingSwordsmenLabel.Text = "0";
      this.reinforcingSwordsmenLabel.Color = ARGBColors.Black;
      this.reinforcingSwordsmenLabel.Position = new Point(x + 3 * num3, y2 + 3 * num2);
      this.reinforcingSwordsmenLabel.Size = new Size(50, 27);
      this.reinforcingSwordsmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.reinforcingSwordsmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.reinforcingSwordsmenLabel);
      this.reinforcingCatapultsLabel.Text = "0";
      this.reinforcingCatapultsLabel.Color = ARGBColors.Black;
      this.reinforcingCatapultsLabel.Position = new Point(x + 4 * num3, y2 + 3 * num2);
      this.reinforcingCatapultsLabel.Size = new Size(50, 27);
      this.reinforcingCatapultsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.reinforcingCatapultsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.reinforcingCatapultsLabel);
      this.reinforcingCaptainsLabel.Text = "0";
      this.reinforcingCaptainsLabel.Color = ARGBColors.Black;
      this.reinforcingCaptainsLabel.Position = new Point(x + 5 * num3, y2 + 3 * num2);
      this.reinforcingCaptainsLabel.Size = new Size(50, 27);
      this.reinforcingCaptainsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.reinforcingCaptainsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.reinforcingCaptainsLabel);
      this.reinforcementInfoButton.ImageNorm = (Image) GFXLibrary.barracks_i_button_normal;
      this.reinforcementInfoButton.ImageOver = (Image) GFXLibrary.barracks_i_button_over;
      this.reinforcementInfoButton.Position = new Point(865, y2 + 3 * num2 - 1);
      this.reinforcementInfoButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.reinforcementInfoClick), "CapitalBarracksPanel_view_reinforcements");
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.reinforcementInfoButton);
      this.update();
    }

    public void update() => this.updateValues();

    public void closeClick() => InterfaceMgr.Instance.setVillageTabSubMode(-1);

    public void updateValues()
    {
      NumberFormatInfo nfi = GameEngine.NFI;
      WorldData localWorldData = GameEngine.Instance.LocalWorldData;
      VillageMap village = GameEngine.Instance.Village;
      CastleMap castle = GameEngine.Instance.Castle;
      if (village == null || castle == null)
        return;
      int locallyMadePeasants = village.LocallyMade_Peasants;
      int locallyMadeArchers = village.LocallyMade_Archers;
      int locallyMadePikemen = village.LocallyMade_Pikemen;
      int locallyMadeSwordsmen = village.LocallyMade_Swordsmen;
      int locallyMadeCatapults = village.LocallyMade_Catapults;
      int num1 = locallyMadeSwordsmen + locallyMadePikemen + locallyMadePeasants + locallyMadeCatapults + locallyMadeArchers;
      VillageMap.ArmouryLevels levels = new VillageMap.ArmouryLevels();
      village.getArmouryLevels(levels);
      int capitalGold = (int) village.m_capitalGold;
      int num2 = localWorldData.Barracks_GoldCost_Peasant * localWorldData.MercenaryCostMultiplier;
      int num3 = localWorldData.Barracks_GoldCost_Archer * localWorldData.MercenaryCostMultiplier;
      int num4 = localWorldData.Barracks_GoldCost_Pikeman * localWorldData.MercenaryCostMultiplier;
      int num5 = localWorldData.Barracks_GoldCost_Swordsman * localWorldData.MercenaryCostMultiplier;
      int num6 = localWorldData.Barracks_GoldCost_Catapult * localWorldData.MercenaryCostMultiplier;
      if (GameEngine.Instance.World.SixthAgeWorld)
      {
        num2 /= 2;
        num3 /= 2;
        num4 /= 2;
        num5 /= 2;
        num6 /= 2;
      }
      int num7 = capitalGold - locallyMadePeasants * num2 - locallyMadeArchers * num3 - locallyMadePikemen * num4 - locallyMadeSwordsmen * num5 - locallyMadeCatapults * num6;
      int numTroops1 = num7 / num2;
      int numTroops2 = num7 / num3;
      int numTroops3 = num7 / num4;
      int numTroops4 = num7 / num5;
      int numTroops5 = num7 / num6;
      int num8 = 0;
      if (GameEngine.Instance.World.isCapital(village.VillageID))
        num8 = ((int) village.m_parishCapitalResearchData.Research_Command + 1) * 25;
      int num9 = village.calcTotalTroops() + num1;
      int num10 = num9;
      this.goldLabel.Text = num7.ToString();
      this.CurrentTroopsLabel.Text = SK.Text("BARRACKS_Troops", "Troops") + ": " + num9.ToString("N", (IFormatProvider) nfi);
      this.MaxLabelLabel.Text = SK.Text("BARRACKS_Max_Army_Size", "Max Army Size") + ": " + num8.ToString("N", (IFormatProvider) nfi);
      int num11 = num8 - num9;
      if (numTroops1 > num11)
        numTroops1 = num11;
      if (numTroops2 > num11)
        numTroops2 = num11;
      if (numTroops3 > num11)
        numTroops3 = num11;
      if (numTroops4 > num11)
        numTroops4 = num11;
      if (numTroops5 > num11)
        numTroops5 = num11;
      if (num9 >= num8)
      {
        numTroops1 = 0;
        numTroops2 = 0;
        numTroops3 = 0;
        numTroops4 = 0;
        numTroops5 = 0;
      }
      ResearchData forCurrentVillage = GameEngine.Instance.World.GetResearchDataForCurrentVillage();
      if (forCurrentVillage.Research_Conscription == (byte) 0)
      {
        numTroops1 = 0;
        this.troop1Image.Visible = false;
      }
      else
        this.troop1Image.Visible = true;
      if (forCurrentVillage.Research_LongBow == (byte) 0)
      {
        numTroops2 = 0;
        this.troop2Image.Visible = false;
      }
      else
        this.troop2Image.Visible = true;
      if (forCurrentVillage.Research_Pike == (byte) 0)
      {
        numTroops3 = 0;
        this.troop3Image.Visible = false;
      }
      else
        this.troop3Image.Visible = true;
      if (forCurrentVillage.Research_Sword == (byte) 0)
      {
        numTroops4 = 0;
        this.troop4Image.Visible = false;
      }
      else
        this.troop4Image.Visible = true;
      if (forCurrentVillage.Research_Catapult == (byte) 0)
      {
        numTroops5 = 0;
        this.troop5Image.Visible = false;
      }
      else
        this.troop5Image.Visible = true;
      this.troopsMade1Disband.Visible = this.troop1Image.Visible;
      this.troopsMade2Disband.Visible = this.troop2Image.Visible;
      this.troopsMade3Disband.Visible = this.troop3Image.Visible;
      this.troopsMade4Disband.Visible = this.troop4Image.Visible;
      this.troopsMade5Disband.Visible = this.troop5Image.Visible;
      this.makePeasants = numTroops1;
      this.makeArchers = numTroops2;
      this.makePikemen = numTroops3;
      this.makeSwordsmen = numTroops4;
      this.makeCatapults = numTroops5;
      bool flag = true;
      if (!GameEngine.Instance.World.isUserVillage(InterfaceMgr.Instance.getSelectedMenuVillage()))
        flag = false;
      this.troopMake1Button1.invalidate();
      this.troopMake1Button1.Active = false;
      this.troopMake1Button1.Text.Color = Color.FromArgb(151, 134, 108);
      this.troopMake1Button5.Active = false;
      this.troopMake1Button5.Text.Color = Color.FromArgb(151, 134, 108);
      this.troopMake1Button20.Active = false;
      this.troopMake1Button20.Text.Color = Color.FromArgb(151, 134, 108);
      int num12 = this.calcMidSize(numTroops1);
      if (numTroops1 > 0)
      {
        if (flag)
        {
          this.troopMake1Button1.Active = true;
          this.troopMake1Button1.Text.Color = ARGBColors.Black;
        }
        if (numTroops1 >= num12)
        {
          this.troopMake1Button5.Text.Text = num12.ToString();
          if (flag)
          {
            this.troopMake1Button5.Active = true;
            this.troopMake1Button5.Text.Color = ARGBColors.Black;
          }
        }
        if (numTroops1 > 1)
        {
          this.troopMake1Button20.Text.Text = numTroops1.ToString();
          if (flag)
          {
            this.troopMake1Button20.Active = true;
            this.troopMake1Button20.Text.Color = ARGBColors.Black;
          }
        }
      }
      this.troopMake2Button1.invalidate();
      this.troopMake2Button1.Active = false;
      this.troopMake2Button1.Text.Color = Color.FromArgb(151, 134, 108);
      this.troopMake2Button5.Active = false;
      this.troopMake2Button5.Text.Color = Color.FromArgb(151, 134, 108);
      this.troopMake2Button20.Active = false;
      this.troopMake2Button20.Text.Color = Color.FromArgb(151, 134, 108);
      int num13 = this.calcMidSize(numTroops2);
      if (numTroops2 > 0)
      {
        if (flag)
        {
          this.troopMake2Button1.Active = true;
          this.troopMake2Button1.Text.Color = ARGBColors.Black;
        }
        if (numTroops2 >= num13)
        {
          this.troopMake2Button5.Text.Text = num13.ToString();
          if (flag)
          {
            this.troopMake2Button5.Active = true;
            this.troopMake2Button5.Text.Color = ARGBColors.Black;
          }
        }
        if (numTroops2 > 1)
        {
          this.troopMake2Button20.Text.Text = numTroops2.ToString();
          if (flag)
          {
            this.troopMake2Button20.Active = true;
            this.troopMake2Button20.Text.Color = ARGBColors.Black;
          }
        }
      }
      this.troopMake3Button1.invalidate();
      this.troopMake3Button1.Active = false;
      this.troopMake3Button1.Text.Color = Color.FromArgb(151, 134, 108);
      this.troopMake3Button5.Active = false;
      this.troopMake3Button5.Text.Color = Color.FromArgb(151, 134, 108);
      this.troopMake3Button20.Active = false;
      this.troopMake3Button20.Text.Color = Color.FromArgb(151, 134, 108);
      int num14 = this.calcMidSize(numTroops3);
      if (numTroops3 > 0)
      {
        if (flag)
        {
          this.troopMake3Button1.Active = true;
          this.troopMake3Button1.Text.Color = ARGBColors.Black;
        }
        if (numTroops3 >= num14)
        {
          this.troopMake3Button5.Text.Text = num14.ToString();
          if (flag)
          {
            this.troopMake3Button5.Active = true;
            this.troopMake3Button5.Text.Color = ARGBColors.Black;
          }
        }
        if (numTroops3 > 1)
        {
          this.troopMake3Button20.Text.Text = numTroops3.ToString();
          if (flag)
          {
            this.troopMake3Button20.Active = true;
            this.troopMake3Button20.Text.Color = ARGBColors.Black;
          }
        }
      }
      this.troopMake4Button1.invalidate();
      this.troopMake4Button1.Active = false;
      this.troopMake4Button1.Text.Color = Color.FromArgb(151, 134, 108);
      this.troopMake4Button5.Active = false;
      this.troopMake4Button5.Text.Color = Color.FromArgb(151, 134, 108);
      this.troopMake4Button20.Active = false;
      this.troopMake4Button20.Text.Color = Color.FromArgb(151, 134, 108);
      int num15 = this.calcMidSize(numTroops4);
      if (numTroops4 > 0 && flag)
      {
        if (flag)
        {
          this.troopMake4Button1.Active = true;
          this.troopMake4Button1.Text.Color = ARGBColors.Black;
        }
        if (numTroops4 >= num15)
        {
          this.troopMake4Button5.Text.Text = num15.ToString();
          if (flag)
          {
            this.troopMake4Button5.Active = true;
            this.troopMake4Button5.Text.Color = ARGBColors.Black;
          }
        }
        if (numTroops4 > 1)
        {
          this.troopMake4Button20.Text.Text = numTroops4.ToString();
          if (flag)
          {
            this.troopMake4Button20.Active = true;
            this.troopMake4Button20.Text.Color = ARGBColors.Black;
          }
        }
      }
      this.troopMake5Button1.invalidate();
      this.troopMake5Button1.Active = false;
      this.troopMake5Button1.Text.Color = Color.FromArgb(151, 134, 108);
      this.troopMake5Button5.Active = false;
      this.troopMake5Button5.Text.Color = Color.FromArgb(151, 134, 108);
      this.troopMake5Button20.Active = false;
      this.troopMake5Button20.Text.Color = Color.FromArgb(151, 134, 108);
      int num16 = this.calcMidSize(numTroops5);
      if (numTroops5 > 0 && flag)
      {
        if (flag)
        {
          this.troopMake5Button1.Active = true;
          this.troopMake5Button1.Text.Color = ARGBColors.Black;
        }
        if (numTroops5 >= num16)
        {
          this.troopMake5Button5.Text.Text = num16.ToString();
          if (flag)
          {
            this.troopMake5Button5.Active = true;
            this.troopMake5Button5.Text.Color = ARGBColors.Black;
          }
        }
        if (numTroops5 > 1)
        {
          this.troopMake5Button20.Text.Text = numTroops5.ToString();
          if (flag)
          {
            this.troopMake5Button20.Active = true;
            this.troopMake5Button20.Text.Color = ARGBColors.Black;
          }
        }
      }
      int numPeasants1 = 0;
      int numArchers1 = 0;
      int numPikemen1 = 0;
      int numSwordsmen1 = 0;
      int numCatapults = 0;
      int numCaptains1 = 0;
      int numReinfPeasants = 0;
      int numReinfArchers = 0;
      int numReinfPikemen = 0;
      int numReinfSwordsmen = 0;
      int numReinfCatapults = 0;
      int numReinfCaptains = 0;
      GameEngine.Instance.World.getTotalTroopsOutOfVillage(village.VillageID, ref numPeasants1, ref numArchers1, ref numPikemen1, ref numSwordsmen1, ref numCatapults, ref numCaptains1, ref numReinfPeasants, ref numReinfArchers, ref numReinfPikemen, ref numReinfSwordsmen, ref numReinfCatapults, ref numReinfCaptains);
      int numPeasants2 = 0;
      int numArchers2 = 0;
      int numPikemen2 = 0;
      int numSwordsmen2 = 0;
      int numCaptains2 = 0;
      castle.countOwnPlacedTroopTypes(ref numPeasants2, ref numArchers2, ref numPikemen2, ref numSwordsmen2, ref numCaptains2);
      this.inCastlePeasantsLabel.Text = numPeasants2.ToString("N", (IFormatProvider) nfi);
      this.inCastleArchersLabel.Text = numArchers2.ToString("N", (IFormatProvider) nfi);
      this.inCastlePikmenLabel.Text = numPikemen2.ToString("N", (IFormatProvider) nfi);
      this.inCastleSwordsmenLabel.Text = numSwordsmen2.ToString("N", (IFormatProvider) nfi);
      this.attackingPeasantsLabel.Text = numPeasants1.ToString("N", (IFormatProvider) nfi);
      this.attackingArchersLabel.Text = numArchers1.ToString("N", (IFormatProvider) nfi);
      this.attackingPikmenLabel.Text = numPikemen1.ToString("N", (IFormatProvider) nfi);
      this.attackingSwordsmenLabel.Text = numSwordsmen1.ToString("N", (IFormatProvider) nfi);
      this.attackingCatapultsLabel.Text = numCatapults.ToString("N", (IFormatProvider) nfi);
      this.reinforcingPeasantsLabel.Text = numReinfPeasants.ToString("N", (IFormatProvider) nfi);
      this.reinforcingArchersLabel.Text = numReinfArchers.ToString("N", (IFormatProvider) nfi);
      this.reinforcingPikmenLabel.Text = numReinfPikemen.ToString("N", (IFormatProvider) nfi);
      this.reinforcingSwordsmenLabel.Text = numReinfSwordsmen.ToString("N", (IFormatProvider) nfi);
      this.reinforcingCatapultsLabel.Text = numReinfCatapults.ToString("N", (IFormatProvider) nfi);
      int num17 = numPeasants1 + (village.m_numPeasants + locallyMadePeasants) + numPeasants2 + numReinfPeasants;
      numArchers1 += village.m_numArchers + locallyMadeArchers;
      numArchers1 += numArchers2;
      numArchers1 += numReinfArchers;
      int num18 = numPikemen1 + (village.m_numPikemen + locallyMadePikemen) + numPikemen2 + numReinfPikemen;
      numSwordsmen1 += village.m_numSwordsmen + locallyMadeSwordsmen;
      numSwordsmen1 += numSwordsmen2;
      numSwordsmen1 += numReinfSwordsmen;
      int num19 = numCatapults + (village.m_numCatapults + locallyMadeCatapults) + numReinfCatapults;
      this.troopsMade1Disband.Text.Text = num17.ToString("N", (IFormatProvider) nfi);
      this.troopsMade2Disband.Text.Text = numArchers1.ToString("N", (IFormatProvider) nfi);
      this.troopsMade3Disband.Text.Text = num18.ToString("N", (IFormatProvider) nfi);
      this.troopsMade4Disband.Text.Text = numSwordsmen1.ToString("N", (IFormatProvider) nfi);
      this.troopsMade5Disband.Text.Text = num19.ToString("N", (IFormatProvider) nfi);
      this.localPeasantsLabel.Text = (village.m_numPeasants + locallyMadePeasants).ToString("N", (IFormatProvider) nfi);
      this.localArchersLabel.Text = (village.m_numArchers + locallyMadeArchers).ToString("N", (IFormatProvider) nfi);
      this.localPikmenLabel.Text = (village.m_numPikemen + locallyMadePikemen).ToString("N", (IFormatProvider) nfi);
      this.localSwordsmenLabel.Text = (village.m_numSwordsmen + locallyMadeSwordsmen).ToString("N", (IFormatProvider) nfi);
      this.localCatapultsLabel.Text = (village.m_numCatapults + locallyMadeCatapults).ToString("N", (IFormatProvider) nfi);
      this.troopGoldCost1Label.Text = num2.ToString();
      this.troopGoldCost2Label.Text = num3.ToString();
      this.troopGoldCost3Label.Text = num4.ToString();
      this.troopGoldCost4Label.Text = num5.ToString();
      this.troopGoldCost5Label.Text = num6.ToString();
      this.troopGold1Image.Colorise = num7 >= num2 ? ARGBColors.White : Color.FromArgb((int) byte.MaxValue, 128, 128);
      this.troopGold2Image.Colorise = num7 >= num3 ? ARGBColors.White : Color.FromArgb((int) byte.MaxValue, 128, 128);
      this.troopGold3Image.Colorise = num7 >= num4 ? ARGBColors.White : Color.FromArgb((int) byte.MaxValue, 128, 128);
      this.troopGold4Image.Colorise = num7 >= num5 ? ARGBColors.White : Color.FromArgb((int) byte.MaxValue, 128, 128);
      this.troopGold5Image.Colorise = num7 >= num6 ? ARGBColors.White : Color.FromArgb((int) byte.MaxValue, 128, 128);
      this.fullBar.MaxValue = (double) GameEngine.Instance.LocalWorldData.Village_UnitCapacity;
      if (num10 < GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
        this.fullBar.Number = (double) num10;
      else
        this.fullBar.Number = (double) GameEngine.Instance.LocalWorldData.Village_UnitCapacity;
    }

    private int calcMidSize(int numTroops)
    {
      if (numTroops < 6)
        return 999999;
      return numTroops < 30 ? 5 : 10;
    }

    private void makeTroopClick()
    {
      if (this.OverControl == null)
        return;
      try
      {
        CustomSelfDrawPanel.CSDButton overControl = (CustomSelfDrawPanel.CSDButton) this.OverControl;
        if (!overControl.Active || !overControl.Visible)
          return;
        int data = overControl.Data;
        VillageMap village = GameEngine.Instance.Village;
        if (village == null)
          return;
        int amount = 1;
        int numTroops = 0;
        switch (data % 1000)
        {
          case 70:
            numTroops = this.makePeasants;
            break;
          case 71:
            numTroops = this.makeSwordsmen;
            break;
          case 72:
            numTroops = this.makeArchers;
            break;
          case 73:
            numTroops = this.makePikemen;
            break;
          case 74:
            numTroops = this.makeCatapults;
            break;
        }
        if (data >= 2000)
        {
          amount = numTroops;
          data -= 2000;
        }
        else if (data >= 1000)
        {
          amount = this.calcMidSize(numTroops);
          if (amount > 10000)
            return;
          data -= 1000;
        }
        bool quickSend = false;
        if (amount == numTroops)
          quickSend = true;
        village.makeTroops(data, amount, quickSend);
        this.updateValues();
      }
      catch (Exception ex)
      {
      }
    }

    private void makeTroopOver()
    {
      if (this.OverControl == null)
        return;
      this.mouseOver = this.OverControl.Data % 1000;
    }

    private void makeTroopLeave() => this.mouseOver = -1;

    private void disbandTroopsClick()
    {
      if (this.OverControl == null)
        return;
      int data = this.OverControl.Data;
      if (GameEngine.Instance.Village == null)
        return;
      this.closeDisbandPopup();
      this.m_disbandPopup = new DisbandTroopsPopup();
      this.m_disbandPopup.init(data);
      this.m_disbandPopup.Show();
    }

    public void closeDisbandPopup()
    {
      if (this.m_disbandPopup == null)
        return;
      if (this.m_disbandPopup.Created)
        this.m_disbandPopup.Close();
      this.m_disbandPopup = (DisbandTroopsPopup) null;
    }

    private void attackInfoClick()
    {
    }

    private void reinforcementInfoClick() => InterfaceMgr.Instance.setVillageTabSubMode(6);
  }
}
