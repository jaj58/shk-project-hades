// Decompiled with JetBrains decompiler
// Type: Kingdoms.UnitsPanel2
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
  public class UnitsPanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    public static UnitsPanel2 instance;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea mainBackgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel barracksLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage troop1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troop2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troop3Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troop4Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troop5Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea troopsMade1Disband = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea troopsMade2Disband = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea troopsMade3Disband = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea troopsMade4Disband = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea troopsMade5Disband = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel troopsMade1Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopsMade2Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopsMade3Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopsMade4Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopsMade5Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopGoldCost1Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopGoldCost2Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopGoldCost3Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopGoldCost4Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopGoldCost5Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton troopMake1Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake1ButtonA = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake1ButtonB = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake1ButtonX = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake2Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake2ButtonA = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake2ButtonB = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake2ButtonX = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake3Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake4Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake4ButtonA = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake4ButtonB = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake4ButtonX = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopMake5Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel troopUnitSize1Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopUnitSize2Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopUnitSize3Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopUnitSize4Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel troopUnitSize5Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage troopGold1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troopGold2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troopGold3Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troopGold4Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troopGold5Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troop1WeaponImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troop2WeaponImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troop3WeaponImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troop4WeaponImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage troop5WeaponImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel typeLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage item1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel item1AmountLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel SpaceLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDColorBar fullBar = new CustomSelfDrawPanel.CSDColorBar();
    private CustomSelfDrawPanel.CSDExtendingPanel noResearchWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel noResearchText = new CustomSelfDrawPanel.CSDLabel();
    private CardBarGDI cardbar = new CardBarGDI();
    private int numMonk;
    private int numTrader;
    private int numScouts;
    private int numSpies;
    private int mouseOver = -1;
    private DisbandUnitsPopup m_disbandPopup;
    private int disbandOver = -1;

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
      this.Name = nameof (UnitsPanel2);
      this.MaximumSize = new Size(992, 566);
      this.MinimumSize = new Size(992, 566);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public UnitsPanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init()
    {
      UnitsPanel2.instance = this;
      this.clearControls();
      this.mainBackgroundImage.Image = (Image) GFXLibrary.people_background;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.mainBackgroundArea.Position = new Point(0, 0);
      this.mainBackgroundArea.Size = new Size(992, 566);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea);
      InterfaceMgr.Instance.setVillageHeading(SK.Text("UnitsPanel_Units", "Units"));
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(948, 10);
      this.closeButton.CustomTooltipID = 701;
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "UnitsPanel2_close");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea, 6, new Point(898, 10));
      this.troop1Image.Image = (Image) GFXLibrary.people_01;
      this.troop1Image.Position = new Point(140, 48);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.troop1Image);
      this.troopsMade1Label.Text = "0";
      this.troopsMade1Label.Color = Color.FromArgb(208, 165, 102);
      this.troopsMade1Label.Position = new Point(67, 5);
      this.troopsMade1Label.Size = new Size(87, 28);
      this.troopsMade1Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopsMade1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.troop1Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopsMade1Label);
      this.troopsMade1Disband.Position = new Point(0, 0);
      this.troopsMade1Disband.Size = this.troopsMade1Label.Size;
      this.troopsMade1Disband.Data = 1;
      this.troopsMade1Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "UnitsPanel2_disband_monks");
      this.troopsMade1Disband.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopLeave));
      this.troopsMade1Disband.CustomTooltipID = 600;
      this.troopsMade1Label.addControl((CustomSelfDrawPanel.CSDControl) this.troopsMade1Disband);
      this.troopGoldCost1Label.Text = "0";
      this.troopGoldCost1Label.Color = Color.FromArgb(208, 165, 102);
      this.troopGoldCost1Label.Position = new Point(59, 137);
      this.troopGoldCost1Label.Size = new Size(44, 47);
      this.troopGoldCost1Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopGoldCost1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.troop1Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGoldCost1Label);
      this.troopMake1Button.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
      this.troopMake1Button.ImageOver = (Image) GFXLibrary.int_but_delete_over;
      this.troopMake1Button.Position = new Point(10, 189);
      this.troopMake1Button.Text.Text = "0";
      this.troopMake1Button.TextYOffset = 1;
      this.troopMake1Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopMake1Button.Text.Color = ARGBColors.Black;
      this.troopMake1Button.Data = 1;
      this.troopMake1Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
      this.troopMake1ButtonA.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
      this.troopMake1ButtonA.ImageOver = (Image) GFXLibrary.button3comp_left_over;
      this.troopMake1ButtonA.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
      this.troopMake1ButtonA.Position = new Point(10, 195);
      this.troopMake1ButtonA.Text.Text = "0";
      this.troopMake1ButtonA.TextYOffset = 1;
      this.troopMake1ButtonA.Text.Size = new Size(this.troopMake1ButtonA.Width - 5, this.troopMake1ButtonA.Height);
      this.troopMake1ButtonA.Text.Position = new Point(5, 0);
      this.troopMake1ButtonA.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake1ButtonA.Text.Color = ARGBColors.Black;
      this.troopMake1ButtonA.Data = 1;
      this.troopMake1ButtonA.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake1ButtonA.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
      this.troop1Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake1ButtonA);
      this.troopMake1ButtonX.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
      this.troopMake1ButtonX.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
      this.troopMake1ButtonX.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
      this.troopMake1ButtonX.Position = new Point(60, 195);
      this.troopMake1ButtonX.Text.Text = "0";
      this.troopMake1ButtonX.TextYOffset = 1;
      this.troopMake1ButtonX.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake1ButtonX.Text.Color = ARGBColors.Black;
      this.troopMake1ButtonX.Data = 11;
      this.troopMake1ButtonX.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake1ButtonX.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
      this.troop1Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake1ButtonX);
      this.troopMake1ButtonB.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
      this.troopMake1ButtonB.ImageOver = (Image) GFXLibrary.button3comp_right_over;
      this.troopMake1ButtonB.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
      this.troopMake1ButtonB.Position = new Point(108, 195);
      this.troopMake1ButtonB.Text.Text = "0";
      this.troopMake1ButtonB.TextYOffset = 1;
      this.troopMake1ButtonB.Text.Size = new Size(this.troopMake1ButtonB.Width - 5, this.troopMake1ButtonB.Height);
      this.troopMake1ButtonB.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake1ButtonB.Text.Color = ARGBColors.Black;
      this.troopMake1ButtonB.Data = 21;
      this.troopMake1ButtonB.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake1ButtonB.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
      this.troop1Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake1ButtonB);
      this.troopGold1Image.Image = (Image) GFXLibrary.com_32_money;
      this.troopGold1Image.Position = new Point(107, 145);
      this.troop1Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGold1Image);
      this.troop1WeaponImage.Image = (Image) GFXLibrary.people_unitspace_icon_01;
      this.troop1WeaponImage.Position = new Point(107, 113);
      this.troop1WeaponImage.CustomTooltipID = 702;
      this.troop1Image.addControl((CustomSelfDrawPanel.CSDControl) this.troop1WeaponImage);
      this.troopUnitSize1Label.Text = "0";
      this.troopUnitSize1Label.Color = Color.FromArgb(208, 165, 102);
      this.troopUnitSize1Label.Position = new Point(59, 107);
      this.troopUnitSize1Label.Size = new Size(44, 47);
      this.troopUnitSize1Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopUnitSize1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.troop1Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopUnitSize1Label);
      this.troop2Image.Image = (Image) GFXLibrary.people_02;
      this.troop2Image.Position = new Point(410, 48);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.troop2Image);
      this.troopsMade2Label.Text = "0";
      this.troopsMade2Label.Color = Color.FromArgb(208, 165, 102);
      this.troopsMade2Label.Position = new Point(67, 5);
      this.troopsMade2Label.Size = new Size(87, 28);
      this.troopsMade2Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopsMade2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.troop2Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopsMade2Label);
      this.troopsMade2Disband.Position = new Point(0, 0);
      this.troopsMade2Disband.Size = this.troopsMade2Label.Size;
      this.troopsMade2Disband.Data = 2;
      this.troopsMade2Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "UnitsPanel2_disband_traders");
      this.troopsMade2Disband.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopLeave));
      this.troopsMade2Disband.CustomTooltipID = 600;
      this.troopsMade2Label.addControl((CustomSelfDrawPanel.CSDControl) this.troopsMade2Disband);
      this.troopGoldCost2Label.Text = "0";
      this.troopGoldCost2Label.Color = Color.FromArgb(208, 165, 102);
      this.troopGoldCost2Label.Position = new Point(59, 137);
      this.troopGoldCost2Label.Size = new Size(44, 47);
      this.troopGoldCost2Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopGoldCost2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.troop2Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGoldCost2Label);
      this.troopMake2Button.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
      this.troopMake2Button.ImageOver = (Image) GFXLibrary.int_but_delete_over;
      this.troopMake2Button.Position = new Point(10, 189);
      this.troopMake2Button.Text.Text = "0";
      this.troopMake2Button.TextYOffset = 1;
      this.troopMake2Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopMake2Button.Text.Color = ARGBColors.Black;
      this.troopMake2Button.Data = 2;
      this.troopMake2Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
      this.troopMake2ButtonA.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
      this.troopMake2ButtonA.ImageOver = (Image) GFXLibrary.button3comp_left_over;
      this.troopMake2ButtonA.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
      this.troopMake2ButtonA.Position = new Point(10, 195);
      this.troopMake2ButtonA.Text.Text = "0";
      this.troopMake2ButtonA.TextYOffset = 1;
      this.troopMake2ButtonA.Text.Size = new Size(this.troopMake2ButtonA.Width - 5, this.troopMake2ButtonA.Height);
      this.troopMake2ButtonA.Text.Position = new Point(5, 0);
      this.troopMake2ButtonA.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake2ButtonA.Text.Color = ARGBColors.Black;
      this.troopMake2ButtonA.Data = 2;
      this.troopMake2ButtonA.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake2ButtonA.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
      this.troop2Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake2ButtonA);
      this.troopMake2ButtonX.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
      this.troopMake2ButtonX.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
      this.troopMake2ButtonX.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
      this.troopMake2ButtonX.Position = new Point(60, 195);
      this.troopMake2ButtonX.Text.Text = "0";
      this.troopMake2ButtonX.TextYOffset = 1;
      this.troopMake2ButtonX.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake2ButtonX.Text.Color = ARGBColors.Black;
      this.troopMake2ButtonX.Data = 12;
      this.troopMake2ButtonX.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake2ButtonX.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
      this.troop2Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake2ButtonX);
      this.troopMake2ButtonB.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
      this.troopMake2ButtonB.ImageOver = (Image) GFXLibrary.button3comp_right_over;
      this.troopMake2ButtonB.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
      this.troopMake2ButtonB.Position = new Point(108, 195);
      this.troopMake2ButtonB.Text.Text = "0";
      this.troopMake2ButtonB.TextYOffset = 1;
      this.troopMake2ButtonB.Text.Size = new Size(this.troopMake2ButtonB.Width - 5, this.troopMake2ButtonB.Height);
      this.troopMake2ButtonB.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake2ButtonB.Text.Color = ARGBColors.Black;
      this.troopMake2ButtonB.Data = 22;
      this.troopMake2ButtonB.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake2ButtonB.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
      this.troop2Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake2ButtonB);
      this.troopGold2Image.Image = (Image) GFXLibrary.com_32_money;
      this.troopGold2Image.Position = new Point(107, 145);
      this.troop2Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGold2Image);
      this.troop2WeaponImage.Image = (Image) GFXLibrary.people_unitspace_icon_02;
      this.troop2WeaponImage.Position = new Point(107, 113);
      this.troop2WeaponImage.CustomTooltipID = 702;
      this.troop2Image.addControl((CustomSelfDrawPanel.CSDControl) this.troop2WeaponImage);
      this.troopUnitSize2Label.Text = "0";
      this.troopUnitSize2Label.Color = Color.FromArgb(208, 165, 102);
      this.troopUnitSize2Label.Position = new Point(59, 107);
      this.troopUnitSize2Label.Size = new Size(44, 47);
      this.troopUnitSize2Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopUnitSize2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.troop2Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopUnitSize2Label);
      this.troop3Image.Image = (Image) GFXLibrary.people_03;
      this.troop3Image.Position = new Point(410, 48);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.troop3Image);
      this.troopsMade3Label.Text = "0";
      this.troopsMade3Label.Color = Color.FromArgb(208, 165, 102);
      this.troopsMade3Label.Position = new Point(67, 5);
      this.troopsMade3Label.Size = new Size(87, 28);
      this.troopsMade3Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopsMade3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.troop3Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopsMade3Label);
      this.troopsMade3Disband.Position = new Point(0, 0);
      this.troopsMade3Disband.Size = this.troopsMade3Label.Size;
      this.troopsMade3Disband.Data = 3;
      this.troopsMade3Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick));
      this.troopsMade3Disband.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopLeave));
      this.troopsMade3Disband.CustomTooltipID = 600;
      this.troopsMade3Label.addControl((CustomSelfDrawPanel.CSDControl) this.troopsMade3Disband);
      this.troopGoldCost3Label.Text = "0";
      this.troopGoldCost3Label.Color = Color.FromArgb(208, 165, 102);
      this.troopGoldCost3Label.Position = new Point(59, 137);
      this.troopGoldCost3Label.Size = new Size(44, 47);
      this.troopGoldCost3Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopGoldCost3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.troop3Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGoldCost3Label);
      this.troopMake3Button.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
      this.troopMake3Button.ImageOver = (Image) GFXLibrary.int_but_delete_over;
      this.troopMake3Button.Position = new Point(10, 189);
      this.troopMake3Button.Text.Text = "0";
      this.troopMake3Button.TextYOffset = 1;
      this.troopMake3Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopMake3Button.Text.Color = ARGBColors.Black;
      this.troopMake3Button.Data = 3;
      this.troopMake3Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
      this.troop3Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake3Button);
      this.troopGold3Image.Image = (Image) GFXLibrary.com_32_money;
      this.troopGold3Image.Position = new Point(107, 145);
      this.troop3Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGold3Image);
      this.troop3WeaponImage.Image = (Image) GFXLibrary.people_unitspace_icon_03;
      this.troop3WeaponImage.Position = new Point(107, 113);
      this.troop3WeaponImage.CustomTooltipID = 702;
      this.troop3Image.addControl((CustomSelfDrawPanel.CSDControl) this.troop3WeaponImage);
      this.troopUnitSize3Label.Text = "0";
      this.troopUnitSize3Label.Color = Color.FromArgb(208, 165, 102);
      this.troopUnitSize3Label.Position = new Point(59, 107);
      this.troopUnitSize3Label.Size = new Size(44, 47);
      this.troopUnitSize3Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopUnitSize3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.troop3Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopUnitSize3Label);
      this.troop4Image.Image = (Image) GFXLibrary.people_04;
      this.troop4Image.Position = new Point(680, 48);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.troop4Image);
      this.troopsMade4Label.Text = "0";
      this.troopsMade4Label.Color = Color.FromArgb(208, 165, 102);
      this.troopsMade4Label.Position = new Point(67, 5);
      this.troopsMade4Label.Size = new Size(87, 28);
      this.troopsMade4Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopsMade4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.troop4Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopsMade4Label);
      this.troopsMade4Disband.Position = new Point(0, 0);
      this.troopsMade4Disband.Size = this.troopsMade4Label.Size;
      this.troopsMade4Disband.Data = 4;
      this.troopsMade4Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "UnitsPanel2_disband_scouts");
      this.troopsMade4Disband.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopLeave));
      this.troopsMade4Disband.CustomTooltipID = 600;
      this.troopsMade4Label.addControl((CustomSelfDrawPanel.CSDControl) this.troopsMade4Disband);
      this.troopGoldCost4Label.Text = "0";
      this.troopGoldCost4Label.Color = Color.FromArgb(208, 165, 102);
      this.troopGoldCost4Label.Position = new Point(59, 137);
      this.troopGoldCost4Label.Size = new Size(44, 47);
      this.troopGoldCost4Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopGoldCost4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.troop4Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGoldCost4Label);
      this.troopMake4Button.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
      this.troopMake4Button.ImageOver = (Image) GFXLibrary.int_but_delete_over;
      this.troopMake4Button.Position = new Point(10, 193);
      this.troopMake4Button.Text.Text = "0";
      this.troopMake4Button.TextYOffset = 1;
      this.troopMake4Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopMake4Button.Text.Color = ARGBColors.Black;
      this.troopMake4Button.Data = 4;
      this.troopMake4Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
      this.troopMake4ButtonA.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
      this.troopMake4ButtonA.ImageOver = (Image) GFXLibrary.button3comp_left_over;
      this.troopMake4ButtonA.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
      this.troopMake4ButtonA.Position = new Point(10, 195);
      this.troopMake4ButtonA.Text.Text = "0";
      this.troopMake4ButtonA.TextYOffset = 1;
      this.troopMake4ButtonA.Text.Size = new Size(this.troopMake4ButtonA.Width - 5, this.troopMake4ButtonA.Height);
      this.troopMake4ButtonA.Text.Position = new Point(5, 0);
      this.troopMake4ButtonA.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake4ButtonA.Text.Color = ARGBColors.Black;
      this.troopMake4ButtonA.Data = 4;
      this.troopMake4ButtonA.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake4ButtonA.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
      this.troop4Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake4ButtonA);
      this.troopMake4ButtonX.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
      this.troopMake4ButtonX.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
      this.troopMake4ButtonX.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
      this.troopMake4ButtonX.Position = new Point(60, 195);
      this.troopMake4ButtonX.Text.Text = "0";
      this.troopMake4ButtonX.TextYOffset = 1;
      this.troopMake4ButtonX.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake4ButtonX.Text.Color = ARGBColors.Black;
      this.troopMake4ButtonX.Data = 14;
      this.troopMake4ButtonX.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake4ButtonX.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
      this.troop4Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake4ButtonX);
      this.troopMake4ButtonB.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
      this.troopMake4ButtonB.ImageOver = (Image) GFXLibrary.button3comp_right_over;
      this.troopMake4ButtonB.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
      this.troopMake4ButtonB.Position = new Point(108, 195);
      this.troopMake4ButtonB.Text.Text = "0";
      this.troopMake4ButtonB.TextYOffset = 1;
      this.troopMake4ButtonB.Text.Size = new Size(this.troopMake4ButtonB.Width - 5, this.troopMake4ButtonB.Height);
      this.troopMake4ButtonB.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.troopMake4ButtonB.Text.Color = ARGBColors.Black;
      this.troopMake4ButtonB.Data = 24;
      this.troopMake4ButtonB.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake4ButtonB.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
      this.troop4Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake4ButtonB);
      this.troopGold4Image.Image = (Image) GFXLibrary.com_32_money;
      this.troopGold4Image.Position = new Point(107, 145);
      this.troop4Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGold4Image);
      this.troop4WeaponImage.Image = (Image) GFXLibrary.people_unitspace_icon_04;
      this.troop4WeaponImage.Position = new Point(107, 113);
      this.troop4WeaponImage.CustomTooltipID = 702;
      this.troop4Image.addControl((CustomSelfDrawPanel.CSDControl) this.troop4WeaponImage);
      this.troopUnitSize4Label.Text = "0";
      this.troopUnitSize4Label.Color = Color.FromArgb(208, 165, 102);
      this.troopUnitSize4Label.Position = new Point(59, 107);
      this.troopUnitSize4Label.Size = new Size(44, 47);
      this.troopUnitSize4Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopUnitSize4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.troop4Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopUnitSize4Label);
      this.troop5Image.Image = (Image) GFXLibrary.people_05;
      this.troop5Image.Position = new Point(770, 48);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.troop5Image);
      this.troopsMade5Label.Text = "0";
      this.troopsMade5Label.Color = Color.FromArgb(208, 165, 102);
      this.troopsMade5Label.Position = new Point(67, 5);
      this.troopsMade5Label.Size = new Size(87, 28);
      this.troopsMade5Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopsMade5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.troop5Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopsMade5Label);
      this.troopsMade5Disband.Position = new Point(0, 0);
      this.troopsMade5Disband.Size = this.troopsMade5Label.Size;
      this.troopsMade5Disband.Data = 5;
      this.troopsMade5Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick));
      this.troopsMade5Disband.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopLeave));
      this.troopsMade5Disband.CustomTooltipID = 600;
      this.troopsMade5Label.addControl((CustomSelfDrawPanel.CSDControl) this.troopsMade5Disband);
      this.troopGoldCost5Label.Text = "0";
      this.troopGoldCost5Label.Color = Color.FromArgb(208, 165, 102);
      this.troopGoldCost5Label.Position = new Point(59, 137);
      this.troopGoldCost5Label.Size = new Size(44, 47);
      this.troopGoldCost5Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopGoldCost5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.troop5Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGoldCost5Label);
      this.troopMake5Button.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
      this.troopMake5Button.ImageOver = (Image) GFXLibrary.int_but_delete_over;
      this.troopMake5Button.Position = new Point(10, 189);
      this.troopMake5Button.Text.Text = "0";
      this.troopMake5Button.TextYOffset = 1;
      this.troopMake5Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopMake5Button.Text.Color = ARGBColors.Black;
      this.troopMake5Button.Data = 5;
      this.troopMake5Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
      this.troopMake5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
      this.troop5Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopMake5Button);
      this.troopGold5Image.Image = (Image) GFXLibrary.com_32_money;
      this.troopGold5Image.Position = new Point(107, 145);
      this.troop5Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopGold5Image);
      this.troop5WeaponImage.Image = (Image) GFXLibrary.people_unitspace_icon_05;
      this.troop5WeaponImage.Position = new Point(107, 113);
      this.troop5WeaponImage.CustomTooltipID = 702;
      this.troop5Image.addControl((CustomSelfDrawPanel.CSDControl) this.troop5WeaponImage);
      this.troopUnitSize5Label.Text = "0";
      this.troopUnitSize5Label.Color = Color.FromArgb(208, 165, 102);
      this.troopUnitSize5Label.Position = new Point(59, 107);
      this.troopUnitSize5Label.Size = new Size(44, 47);
      this.troopUnitSize5Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.troopUnitSize5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.troop5Image.addControl((CustomSelfDrawPanel.CSDControl) this.troopUnitSize5Label);
      int num = 73;
      this.item1Image.Image = (Image) GFXLibrary.com_32_people;
      this.item1Image.Position = new Point(num + 7, 310);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.item1Image);
      this.item1AmountLabel.Text = "0";
      this.item1AmountLabel.Color = ARGBColors.Black;
      this.item1AmountLabel.Position = new Point(num - 3, 352);
      this.item1AmountLabel.Size = new Size(50, 20);
      this.item1AmountLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.item1AmountLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.item1AmountLabel);
      this.typeLabel.Text = "";
      this.typeLabel.Color = ARGBColors.Black;
      this.typeLabel.Position = new Point(num + 80, 348);
      this.typeLabel.Size = new Size(250, 20);
      this.typeLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.typeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.typeLabel);
      this.SpaceLabel.Text = SK.Text("BARRACKS_Spare_Unit_Space", "Spare Unit Space") + ": 0";
      this.SpaceLabel.Color = ARGBColors.Black;
      this.SpaceLabel.Position = new Point(560, 334);
      if (Program.mySettings.LanguageIdent == "pl" || Program.mySettings.LanguageIdent == "it" || Program.mySettings.LanguageIdent == "pt")
        this.SpaceLabel.Size = new Size(420, 20);
      else
        this.SpaceLabel.Size = new Size(220, 20);
      this.SpaceLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.SpaceLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.SpaceLabel);
      this.fullBar.setImages((Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right, (Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right);
      this.fullBar.Number = 0.0;
      this.fullBar.MaxValue = 9.0;
      this.fullBar.SetMargin(2, 2, 2, 3);
      if (Program.mySettings.LanguageIdent == "pl" || Program.mySettings.LanguageIdent == "it" || Program.mySettings.LanguageIdent == "pt")
        this.fullBar.Position = new Point(770, 359);
      else
        this.fullBar.Position = new Point(770, 339);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.fullBar);
      this.troop3Image.Visible = false;
      this.troop5Image.Visible = false;
      this.troop1Image.Alpha = 0.3f;
      this.troop2Image.Alpha = 0.3f;
      this.troop4Image.Alpha = 0.3f;
      this.cardbar.Position = new Point(0, 0);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.cardbar);
      this.cardbar.init(5);
      int researchOrdination = (int) GameEngine.Instance.World.UserResearchData.Research_Ordination;
      int researchMerchantGuilds = (int) GameEngine.Instance.World.UserResearchData.Research_Merchant_Guilds;
      int researchScouts = (int) GameEngine.Instance.World.UserResearchData.Research_Scouts;
      this.update();
    }

    public void update()
    {
      this.updateValues();
      this.cardbar.update();
    }

    public void closeClick() => InterfaceMgr.Instance.setVillageTabSubMode(-1);

    public void updateValues()
    {
      NumberFormatInfo nfi = GameEngine.NFI;
      WorldData localWorldData = GameEngine.Instance.LocalWorldData;
      VillageMap village = GameEngine.Instance.Village;
      CastleMap castle = GameEngine.Instance.Castle;
      if (village != null && castle != null)
      {
        int locallyMadeScouts = village.LocallyMade_Scouts;
        int val1 = village.m_spareWorkers - locallyMadeScouts;
        this.item1AmountLabel.Text = val1.ToString("N", (IFormatProvider) nfi);
        this.item1Image.Colorise = val1 != 0 || this.mouseOver < 0 ? ARGBColors.White : Color.FromArgb((int) byte.MaxValue, 128, 128);
        int num1 = (int) GameEngine.Instance.World.getCurrentGold() - locallyMadeScouts * localWorldData.ScoutGoldCost;
        village.calcTotalTroops();
        int num2 = village.calcUnitUsages() + locallyMadeScouts * GameEngine.Instance.LocalWorldData.UnitSize_Scout;
        int num3 = village.calcTotalTraders();
        int numMonks = village.calcTotalMonks();
        int num4 = village.calcTotalScouts() + locallyMadeScouts;
        this.numMonk = Math.Min(val1, num1 / localWorldData.getMonkCost(numMonks));
        this.numTrader = Math.Min(val1, num1 / localWorldData.TraderGoldCost);
        this.numScouts = Math.Min(val1, num1 / localWorldData.ScoutGoldCost);
        this.numSpies = 0;
        int researchScoutsLevel = ResearchData.scoutResearchScoutsLevels[(int) GameEngine.Instance.World.userResearchData.Research_Scouts];
        int num5 = village.countWorkingMarkets() * ResearchData.numMerchantGuildsTraders[(int) GameEngine.Instance.World.userResearchData.Research_Merchant_Guilds];
        int researchMonkLevel = ResearchData.ordinationResearchMonkLevels[(int) GameEngine.Instance.World.userResearchData.Research_Ordination];
        if (this.numScouts > researchScoutsLevel)
          this.numScouts = researchScoutsLevel;
        if (this.numTrader > num5)
          this.numTrader = num5;
        if (this.numMonk > researchMonkLevel)
          this.numMonk = researchMonkLevel;
        if (num5 - num3 < this.numTrader)
          this.numTrader = num5 - num3;
        if (researchScoutsLevel - num4 < this.numScouts)
          this.numScouts = researchScoutsLevel - num4;
        if (researchMonkLevel - numMonks < this.numMonk)
          this.numMonk = researchMonkLevel - numMonks;
        if (this.numTrader < 0)
          this.numTrader = 0;
        if (this.numScouts < 0)
          this.numScouts = 0;
        if (this.numMonk < 0)
          this.numMonk = 0;
        this.SpaceLabel.Text = SK.Text("BARRACKS_Spare_Unit_Space", "Spare Unit Space") + ": " + Math.Max(GameEngine.Instance.LocalWorldData.Village_UnitCapacity - num2, 0).ToString("N", (IFormatProvider) nfi);
        this.troopUnitSize1Label.Text = GameEngine.Instance.LocalWorldData.UnitSize_Priests.ToString();
        this.troopUnitSize2Label.Text = GameEngine.Instance.LocalWorldData.UnitSize_Trader.ToString();
        this.troopUnitSize3Label.Text = GameEngine.Instance.LocalWorldData.UnitSize_Spies.ToString();
        this.troopUnitSize4Label.Text = GameEngine.Instance.LocalWorldData.UnitSize_Scout.ToString();
        this.troop1WeaponImage.CustomTooltipData = GameEngine.Instance.LocalWorldData.UnitSize_Priests;
        this.troop2WeaponImage.CustomTooltipData = GameEngine.Instance.LocalWorldData.UnitSize_Trader;
        this.troop3WeaponImage.CustomTooltipData = GameEngine.Instance.LocalWorldData.UnitSize_Spies;
        this.troop4WeaponImage.CustomTooltipData = GameEngine.Instance.LocalWorldData.UnitSize_Scout;
        while (this.numMonk > 0 && num2 + GameEngine.Instance.LocalWorldData.UnitSize_Priests * this.numMonk > GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
          --this.numMonk;
        while (this.numTrader > 0 && num2 + GameEngine.Instance.LocalWorldData.UnitSize_Trader * this.numTrader > GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
          --this.numTrader;
        if (num2 + GameEngine.Instance.LocalWorldData.UnitSize_Spies > GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
          this.numSpies = 0;
        while (this.numScouts > 0 && num2 + GameEngine.Instance.LocalWorldData.UnitSize_Scout * this.numScouts > GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
          --this.numScouts;
        if (num2 >= GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
        {
          this.numMonk = 0;
          this.numScouts = 0;
          this.numSpies = 0;
          this.numTrader = 0;
        }
        if (GameEngine.Instance.World.UserResearchData.Research_Ordination == (byte) 0)
        {
          this.numMonk = 0;
          this.troop1Image.Alpha = 0.3f;
          this.troopMake1ButtonA.Visible = false;
          this.troopMake1ButtonB.Visible = false;
          this.troopMake1ButtonX.Visible = false;
          this.troop1Image.CustomTooltipID = 707;
          this.troopsMade1Disband.Visible = false;
        }
        else
        {
          this.troop1Image.Alpha = 1f;
          this.troopMake1ButtonA.Visible = true;
          this.troopMake1ButtonB.Visible = true;
          this.troopMake1ButtonX.Visible = true;
          this.troop1Image.CustomTooltipID = 704;
          this.troopsMade1Disband.Visible = true;
        }
        if (GameEngine.Instance.World.UserResearchData.Research_Merchant_Guilds == (byte) 0)
        {
          this.numTrader = 0;
          this.troop2Image.Alpha = 0.3f;
          this.troopMake2ButtonA.Visible = false;
          this.troopMake2ButtonB.Visible = false;
          this.troopMake2ButtonX.Visible = false;
          this.troop2Image.CustomTooltipID = 706;
          this.troopsMade2Disband.Visible = false;
        }
        else
        {
          this.troop2Image.Alpha = 1f;
          this.troopMake2ButtonA.Visible = true;
          this.troopMake2ButtonB.Visible = true;
          this.troopMake2ButtonX.Visible = true;
          this.troop2Image.CustomTooltipID = 703;
          this.troopsMade2Disband.Visible = true;
        }
        this.numSpies = 0;
        this.troop3Image.Visible = false;
        if (GameEngine.Instance.World.UserResearchData.Research_Scouts == (byte) 0)
        {
          this.numScouts = 0;
          this.troop4Image.Alpha = 0.3f;
          this.troopMake4ButtonA.Visible = false;
          this.troopMake4ButtonB.Visible = false;
          this.troopMake4ButtonX.Visible = false;
          this.troop4Image.CustomTooltipID = 708;
          this.troopsMade4Disband.Visible = false;
        }
        else
        {
          this.troop4Image.Alpha = 1f;
          this.troopMake4ButtonA.Visible = true;
          this.troopMake4ButtonB.Visible = true;
          this.troopMake4ButtonX.Visible = true;
          this.troop4Image.CustomTooltipID = 705;
          this.troopsMade4Disband.Visible = true;
        }
        this.troop5Image.Visible = false;
        if (this.numMonk > 0)
        {
          this.troopMake1Button.Active = true;
          this.troopMake1Button.Alpha = 1f;
          this.troopMake1ButtonA.Active = true;
          this.troopMake1ButtonA.Alpha = 1f;
          this.troopMake1ButtonB.Active = true;
          this.troopMake1ButtonB.Alpha = 1f;
          this.troopMake1ButtonX.Active = true;
          this.troopMake1ButtonX.Alpha = 1f;
          this.troopMake1Button.CustomTooltipID = 0;
          this.troopMake1ButtonA.CustomTooltipID = 0;
          this.troopMake1ButtonB.CustomTooltipID = 0;
          this.troopMake1ButtonX.CustomTooltipID = 0;
        }
        else
        {
          this.troopMake1Button.Active = false;
          this.troopMake1Button.Alpha = 0.5f;
          this.troopMake1ButtonA.Active = false;
          this.troopMake1ButtonA.Alpha = 0.5f;
          this.troopMake1ButtonB.Active = false;
          this.troopMake1ButtonB.Alpha = 0.5f;
          this.troopMake1ButtonX.Active = false;
          this.troopMake1ButtonX.Alpha = 0.5f;
          int num6 = 0;
          if (val1 == 0)
            num6 |= 1;
          if (num1 < localWorldData.getMonkCost(numMonks))
            num6 |= 2;
          if (researchMonkLevel - numMonks <= 0)
            num6 |= 8;
          if (num2 + GameEngine.Instance.LocalWorldData.UnitSize_Priests > GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
            num6 |= 4;
          this.troopMake1Button.CustomTooltipID = 2700;
          this.troopMake1ButtonA.CustomTooltipID = 2700;
          this.troopMake1ButtonB.CustomTooltipID = 2700;
          this.troopMake1ButtonX.CustomTooltipID = 2700;
          this.troopMake1Button.CustomTooltipData = num6;
          this.troopMake1ButtonA.CustomTooltipData = num6;
          this.troopMake1ButtonB.CustomTooltipData = num6;
          this.troopMake1ButtonX.CustomTooltipData = num6;
        }
        if (this.numTrader > 0)
        {
          this.troopMake2Button.Active = true;
          this.troopMake2Button.Alpha = 1f;
          this.troopMake2ButtonA.Active = true;
          this.troopMake2ButtonA.Alpha = 1f;
          this.troopMake2ButtonB.Active = true;
          this.troopMake2ButtonB.Alpha = 1f;
          this.troopMake2ButtonX.Active = true;
          this.troopMake2ButtonX.Alpha = 1f;
          this.troopMake2Button.CustomTooltipID = 0;
          this.troopMake2ButtonA.CustomTooltipID = 0;
          this.troopMake2ButtonB.CustomTooltipID = 0;
          this.troopMake2ButtonX.CustomTooltipID = 0;
        }
        else
        {
          this.troopMake2Button.Active = false;
          this.troopMake2Button.Alpha = 0.5f;
          this.troopMake2ButtonA.Active = false;
          this.troopMake2ButtonA.Alpha = 0.5f;
          this.troopMake2ButtonB.Active = false;
          this.troopMake2ButtonB.Alpha = 0.5f;
          this.troopMake2ButtonX.Active = false;
          this.troopMake2ButtonX.Alpha = 0.5f;
          int num7 = 0;
          if (val1 == 0)
            num7 |= 1;
          if (num1 < localWorldData.TraderGoldCost)
            num7 |= 2;
          if (num5 - num3 <= 0)
            num7 |= 8;
          if (num2 + GameEngine.Instance.LocalWorldData.UnitSize_Trader > GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
            num7 |= 4;
          this.troopMake2Button.CustomTooltipID = 2700;
          this.troopMake2ButtonA.CustomTooltipID = 2700;
          this.troopMake2ButtonB.CustomTooltipID = 2700;
          this.troopMake2ButtonX.CustomTooltipID = 2700;
          this.troopMake2Button.CustomTooltipData = num7;
          this.troopMake2ButtonA.CustomTooltipData = num7;
          this.troopMake2ButtonB.CustomTooltipData = num7;
          this.troopMake2ButtonX.CustomTooltipData = num7;
        }
        if (this.numSpies > 0)
        {
          this.troopMake3Button.Active = true;
          this.troopMake3Button.Alpha = 1f;
        }
        else
        {
          this.troopMake3Button.Active = false;
          this.troopMake3Button.Alpha = 0.5f;
        }
        if (this.numScouts > 0)
        {
          this.troopMake4Button.Active = true;
          this.troopMake4Button.Alpha = 1f;
          this.troopMake4ButtonA.Active = true;
          this.troopMake4ButtonA.Alpha = 1f;
          this.troopMake4ButtonB.Active = true;
          this.troopMake4ButtonB.Alpha = 1f;
          this.troopMake4ButtonX.Active = true;
          this.troopMake4ButtonX.Alpha = 1f;
          this.troopMake4Button.CustomTooltipID = 0;
          this.troopMake4ButtonA.CustomTooltipID = 0;
          this.troopMake4ButtonB.CustomTooltipID = 0;
          this.troopMake4ButtonX.CustomTooltipID = 0;
        }
        else
        {
          this.troopMake4Button.Active = false;
          this.troopMake4Button.Alpha = 0.5f;
          this.troopMake4ButtonA.Active = false;
          this.troopMake4ButtonA.Alpha = 0.5f;
          this.troopMake4ButtonB.Active = false;
          this.troopMake4ButtonB.Alpha = 0.5f;
          this.troopMake4ButtonX.Active = false;
          this.troopMake4ButtonX.Alpha = 0.5f;
          int num8 = 0;
          if (val1 == 0)
            num8 |= 1;
          if (num1 < localWorldData.ScoutGoldCost)
            num8 |= 2;
          if (researchScoutsLevel - num4 <= 0)
            num8 |= 8;
          if (num2 + GameEngine.Instance.LocalWorldData.UnitSize_Scout > GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
            num8 |= 4;
          this.troopMake4Button.CustomTooltipID = 2700;
          this.troopMake4ButtonA.CustomTooltipID = 2700;
          this.troopMake4ButtonB.CustomTooltipID = 2700;
          this.troopMake4ButtonX.CustomTooltipID = 2700;
          this.troopMake4Button.CustomTooltipData = num8;
          this.troopMake4ButtonA.CustomTooltipData = num8;
          this.troopMake4ButtonB.CustomTooltipData = num8;
          this.troopMake4ButtonX.CustomTooltipData = num8;
        }
        this.troopsMade1Label.Text = numMonks.ToString("N", (IFormatProvider) nfi) + " / " + researchMonkLevel.ToString("N", (IFormatProvider) nfi);
        this.troopsMade2Label.Text = num3.ToString("N", (IFormatProvider) nfi) + " / " + num5.ToString("N", (IFormatProvider) nfi);
        this.troopsMade3Label.Text = "0";
        this.troopsMade4Label.Text = num4.ToString("N", (IFormatProvider) nfi) + " / " + researchScoutsLevel.ToString("N", (IFormatProvider) nfi);
        this.troopMake1Button.Text.Text = this.numMonk.ToString("N", (IFormatProvider) nfi);
        this.troopMake1ButtonB.Text.Text = this.numMonk.ToString("N", (IFormatProvider) nfi);
        if (this.numMonk > 0)
        {
          this.troopMake1ButtonA.Text.Text = "1";
          this.troopMake1ButtonX.Text.Text = this.numMonk < 4 ? this.numMonk.ToString("N", (IFormatProvider) nfi) : "4";
        }
        else
        {
          this.troopMake1ButtonA.Text.Text = "0";
          this.troopMake1ButtonX.Text.Text = "0";
        }
        this.troopMake2Button.Text.Text = this.numTrader.ToString("N", (IFormatProvider) nfi);
        this.troopMake2ButtonB.Text.Text = this.numTrader.ToString("N", (IFormatProvider) nfi);
        if (this.numTrader > 0)
        {
          this.troopMake2ButtonA.Text.Text = "1";
          this.troopMake2ButtonX.Text.Text = this.numTrader < 4 ? this.numTrader.ToString("N", (IFormatProvider) nfi) : "4";
        }
        else
        {
          this.troopMake2ButtonA.Text.Text = "0";
          this.troopMake2ButtonX.Text.Text = "0";
        }
        this.troopMake3Button.Text.Text = this.numSpies.ToString("N", (IFormatProvider) nfi);
        this.troopMake4Button.Text.Text = this.numScouts.ToString("N", (IFormatProvider) nfi);
        this.troopMake4ButtonB.Text.Text = this.numScouts.ToString("N", (IFormatProvider) nfi);
        if (this.numScouts > 0)
        {
          this.troopMake4ButtonA.Text.Text = "1";
          this.troopMake4ButtonX.Text.Text = this.numScouts < 4 ? this.numScouts.ToString("N", (IFormatProvider) nfi) : "4";
        }
        else
        {
          this.troopMake4ButtonA.Text.Text = "0";
          this.troopMake4ButtonX.Text.Text = "0";
        }
        this.troopGoldCost1Label.Text = localWorldData.getMonkCost(numMonks).ToString();
        this.troopGoldCost2Label.Text = localWorldData.TraderGoldCost.ToString();
        this.troopGoldCost3Label.Text = "0";
        this.troopGoldCost4Label.Text = localWorldData.ScoutGoldCost.ToString();
        this.troopGold1Image.Colorise = num1 >= localWorldData.getMonkCost(numMonks) ? ARGBColors.White : Color.FromArgb((int) byte.MaxValue, 128, 128);
        this.troopGold2Image.Colorise = num1 >= localWorldData.TraderGoldCost ? ARGBColors.White : Color.FromArgb((int) byte.MaxValue, 128, 128);
        this.troopGold3Image.Colorise = num1 >= 0 ? ARGBColors.White : Color.FromArgb((int) byte.MaxValue, 128, 128);
        this.troopGold4Image.Colorise = num1 >= localWorldData.ScoutGoldCost ? ARGBColors.White : Color.FromArgb((int) byte.MaxValue, 128, 128);
        this.fullBar.MaxValue = (double) GameEngine.Instance.LocalWorldData.Village_UnitCapacity;
        this.fullBar.Number = num2 >= GameEngine.Instance.LocalWorldData.Village_UnitCapacity ? (double) GameEngine.Instance.LocalWorldData.Village_UnitCapacity : (double) num2;
        this.SpaceLabel.Color = this.mouseOver < 0 || GameEngine.Instance.LocalWorldData.Village_UnitCapacity - num2 > 0 ? ARGBColors.Black : Color.FromArgb((int) byte.MaxValue, 64, 64);
      }
      if (!GameEngine.Instance.World.WorldEnded)
        return;
      this.troopMake1ButtonA.Visible = false;
      this.troopMake1ButtonB.Visible = false;
      this.troopMake1ButtonX.Visible = false;
      this.troopsMade1Disband.Visible = false;
      this.troopMake2ButtonA.Visible = false;
      this.troopMake2ButtonB.Visible = false;
      this.troopMake2ButtonX.Visible = false;
      this.troopsMade2Disband.Visible = false;
      this.troopMake4ButtonA.Visible = false;
      this.troopMake4ButtonB.Visible = false;
      this.troopMake4ButtonX.Visible = false;
      this.troopsMade4Disband.Visible = false;
    }

    private void makeTroopClick()
    {
      if (this.OverControl == null)
        return;
      try
      {
        CustomSelfDrawPanel.CSDButton overControl = (CustomSelfDrawPanel.CSDButton) this.OverControl;
        int data = overControl.Data;
        VillageMap village = GameEngine.Instance.Village;
        if (village != null && overControl.Active)
        {
          switch (data)
          {
            case 1:
              GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_monks");
              village.makePeople(4);
              break;
            case 2:
              GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_traders");
              village.makeTroops(-5);
              break;
            case 4:
              GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_scouts");
              village.makeTroops(76, 1, false);
              break;
            case 11:
              if (this.numMonk > 0)
              {
                GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_monks");
                if (this.numMonk >= 4)
                {
                  village.makePeople(1004);
                  break;
                }
                village.makePeople(1000 + this.numMonk);
                break;
              }
              GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_failed");
              break;
            case 12:
              if (this.numTrader > 0)
              {
                GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_traders");
                if (this.numTrader >= 4)
                {
                  village.makeTroops(-5, 4, false);
                  break;
                }
                village.makeTroops(-5, this.numTrader, false);
                break;
              }
              GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_failed");
              break;
            case 14:
              if (this.numScouts > 0)
              {
                GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_scouts");
                if (this.numScouts >= 4)
                {
                  village.makeTroops(76, 4, false);
                  break;
                }
                village.makeTroops(76, this.numScouts, false);
                break;
              }
              GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_failed");
              break;
            case 21:
              if (this.numMonk > 0)
              {
                GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_monks");
                village.makePeople(1000 + this.numMonk);
                break;
              }
              GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_failed");
              break;
            case 22:
              if (this.numTrader > 0)
              {
                GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_traders");
                village.makeTroops(-5, this.numTrader, false);
                break;
              }
              GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_failed");
              break;
            case 24:
              if (this.numScouts > 0)
              {
                GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_scouts");
                village.makeTroops(76, this.numScouts, false);
                break;
              }
              GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_failed");
              break;
          }
        }
        else
          GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_failed");
      }
      catch (Exception ex)
      {
      }
    }

    private void makeTroopOver()
    {
      if (this.OverControl == null)
        return;
      int data = this.OverControl.Data;
      this.mouseOver = data;
      switch (data)
      {
        case 1:
        case 11:
        case 21:
          this.typeLabel.Text = SK.Text("UnitsPanel_Monks", "Monks");
          break;
        case 2:
        case 12:
        case 22:
          this.typeLabel.Text = SK.Text("UnitsPanel_Traders", "Traders");
          break;
        case 3:
          this.typeLabel.Text = SK.Text("UnitsPanel_Spies", "Spies");
          break;
        case 4:
        case 14:
        case 24:
          this.typeLabel.Text = SK.Text("UnitsPanel_Scouts", "Scouts");
          break;
      }
    }

    private void makeTroopLeave()
    {
      this.mouseOver = -1;
      this.typeLabel.Text = "";
    }

    private void disbandTroopsClick()
    {
      if (this.OverControl == null)
        return;
      int data = this.OverControl.Data;
      if (GameEngine.Instance.Village == null)
        return;
      this.closeDisbandPopup();
      this.m_disbandPopup = new DisbandUnitsPopup();
      this.m_disbandPopup.init(data);
      this.m_disbandPopup.Show();
    }

    public void closeDisbandPopup()
    {
      if (this.m_disbandPopup == null)
        return;
      if (this.m_disbandPopup.Created)
        this.m_disbandPopup.Close();
      this.m_disbandPopup = (DisbandUnitsPopup) null;
    }

    private void disbandTroopOver()
    {
      if (this.OverControl == null)
        return;
      this.disbandOver = this.OverControl.Data;
      switch (this.disbandOver)
      {
        case 1:
          this.troopsMade1Label.Color = ARGBColors.Red;
          break;
        case 2:
          this.troopsMade2Label.Color = ARGBColors.Red;
          break;
        case 3:
          this.troopsMade3Label.Color = ARGBColors.Red;
          break;
        case 4:
          this.troopsMade4Label.Color = ARGBColors.Red;
          break;
      }
    }

    private void disbandTroopLeave()
    {
      switch (this.disbandOver)
      {
        case 1:
          this.troopsMade1Label.Color = Color.FromArgb(208, 165, 102);
          break;
        case 2:
          this.troopsMade2Label.Color = Color.FromArgb(208, 165, 102);
          break;
        case 3:
          this.troopsMade3Label.Color = Color.FromArgb(208, 165, 102);
          break;
        case 4:
          this.troopsMade4Label.Color = Color.FromArgb(208, 165, 102);
          break;
      }
      this.disbandOver = -1;
    }

    private void attackInfoClick() => InterfaceMgr.Instance.setVillageTabSubMode(7);

    private void reinforcementInfoClick() => InterfaceMgr.Instance.setVillageTabSubMode(6);
  }
}
