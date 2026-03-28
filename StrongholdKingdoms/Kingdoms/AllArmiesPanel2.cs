// Decompiled with JetBrains decompiler
// Type: Kingdoms.AllArmiesPanel2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class AllArmiesPanel2 : CustomSelfDrawPanel, IDockableControl
  {
    public static AllArmiesPanel2 instance = (AllArmiesPanel2) null;
    public static bool TradersUpdated = false;
    public static bool MonksUpdated = false;
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage backgroundLeftEdge = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton monkButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton scoutButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton reinforcementsButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider4Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider5Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider6Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider7Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel outGoingAttacksLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel outGoingFromLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel outGoingArrivesLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel outGoingCarryingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel outGoingStatusLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel incomingAttacksLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel incomingAttackingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel incomingArrivesLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage2 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDVertScrollBar outgoingScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea outgoingScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDVertScrollBar incomingScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea incomingScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel diplomacyHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel diplomacyTextLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage smallPeasantImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage smallPeasantImage2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton btnDiplomacy = new CustomSelfDrawPanel.CSDButton();
    private int blockYSize;
    private List<WorldMap.LocalArmyData> armyList = new List<WorldMap.LocalArmyData>();
    private List<WorldMap.LocalTrader> merchantList = new List<WorldMap.LocalTrader>();
    private List<WorldMap.LocalPerson> monkList = new List<WorldMap.LocalPerson>();
    private List<WorldMap.LocalArmyData> reinforcementList = new List<WorldMap.LocalArmyData>();
    private static int mode = 0;
    private CustomSelfDrawPanel.CSDArea sortArea1 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea2 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea3 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea4 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea5 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea6 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea7 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea8 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea9 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea1a = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea2a = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea3a = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea4a = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea5a = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea6a = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea7a = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea8a = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea sortArea9a = new CustomSelfDrawPanel.CSDArea();
    public static int sortMode = 8;
    private DateTime lastfullUpdate = DateTime.MinValue;
    private List<AllArmiesPanel2.ArmyLine> armyLineList = new List<AllArmiesPanel2.ArmyLine>();
    private List<AllArmiesPanel2.ArmyLine> armyLineList2 = new List<AllArmiesPanel2.ArmyLine>();
    private List<AllArmiesPanel2.ScoutLine> scoutLineList = new List<AllArmiesPanel2.ScoutLine>();
    private List<AllArmiesPanel2.ScoutLine> scoutLineList2 = new List<AllArmiesPanel2.ScoutLine>();
    private List<AllArmiesPanel2.ReinforcementLine> reinforcementLineList = new List<AllArmiesPanel2.ReinforcementLine>();
    private List<AllArmiesPanel2.ReinforcementLine> reinforcementLineList2 = new List<AllArmiesPanel2.ReinforcementLine>();
    private List<AllArmiesPanel2.MerchantLine> merchantLineList = new List<AllArmiesPanel2.MerchantLine>();
    private List<AllArmiesPanel2.MerchantLine> merchantLineList2 = new List<AllArmiesPanel2.MerchantLine>();
    private List<AllArmiesPanel2.MonkLine> monkLineList = new List<AllArmiesPanel2.MonkLine>();
    private List<AllArmiesPanel2.MonkLine> monkLineList2 = new List<AllArmiesPanel2.MonkLine>();
    private AllArmiesPanel2.ArmyComparer armyComparer = new AllArmiesPanel2.ArmyComparer();
    private AllArmiesPanel2.MerchantComparer merchantComparer = new AllArmiesPanel2.MerchantComparer();
    private AllArmiesPanel2.MonkComparer monkComparer = new AllArmiesPanel2.MonkComparer();
    private DockableControl dockableControl;
    private IContainer components;
    private Panel focusPanel;

    public AllArmiesPanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.focusPanel.Focus();
    }

    public void preInit() => AllArmiesPanel2.sortMode = 8;

    public void init(bool resized, int newMode)
    {
      if (newMode >= 0)
        AllArmiesPanel2.mode = newMode;
      int height = this.Height;
      AllArmiesPanel2.instance = this;
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
      switch (AllArmiesPanel2.mode)
      {
        case 0:
          this.parishNameLabel.Text = SK.Text("AllArmiesPanel_Attacks", "Attacks");
          break;
        case 1:
          this.parishNameLabel.Text = SK.Text("AllArmiesPanel_Merchants", "Merchants");
          break;
        case 2:
          this.parishNameLabel.Text = SK.Text("AllArmiesPanel_Monks", "Monks");
          break;
        case 3:
          this.parishNameLabel.Text = SK.Text("AllArmiesPanel_Scouts", "Scouts");
          break;
        case 4:
          this.parishNameLabel.Text = SK.Text("AllArmiesPanel_Reinforcements", "Reinforcements");
          break;
      }
      this.parishNameLabel.Color = ARGBColors.White;
      this.parishNameLabel.DropShadowColor = ARGBColors.Black;
      this.parishNameLabel.Position = new Point(20, 0);
      this.parishNameLabel.Size = new Size(this.Width - 40, 40);
      this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
      this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.parishNameLabel);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.headerImage, 20, new Point(this.Width - 44, 3));
      this.attackButton.Position = new Point(614, 4);
      if (AllArmiesPanel2.mode == 0)
      {
        this.attackButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[0];
        this.attackButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[0];
        this.attackButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[0];
        this.attackButton.CustomTooltipID = 0;
        this.attackButton.MoveOnClick = false;
        this.attackButton.Active = false;
      }
      else
      {
        this.attackButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[10];
        this.attackButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[5];
        this.attackButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[5];
        this.attackButton.MoveOnClick = true;
        this.attackButton.CustomTooltipID = 2900;
        this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggleToAttacks), "AllArmiesPanel2_attack");
        this.attackButton.Active = true;
      }
      this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.attackButton);
      this.scoutButton.Position = new Point(678, 4);
      if (AllArmiesPanel2.mode == 3)
      {
        this.scoutButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[1];
        this.scoutButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[1];
        this.scoutButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[1];
        this.scoutButton.CustomTooltipID = 0;
        this.scoutButton.MoveOnClick = false;
        this.scoutButton.Active = false;
      }
      else
      {
        this.scoutButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[11];
        this.scoutButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[6];
        this.scoutButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[6];
        this.scoutButton.CustomTooltipID = 2901;
        this.scoutButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggleToScouts), "AllArmiesPanel2_scouts");
        this.scoutButton.MoveOnClick = true;
        this.scoutButton.Active = true;
      }
      this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.scoutButton);
      this.reinforcementsButton.Position = new Point(742, 4);
      if (AllArmiesPanel2.mode == 4)
      {
        this.reinforcementsButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[2];
        this.reinforcementsButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[2];
        this.reinforcementsButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[2];
        this.reinforcementsButton.CustomTooltipID = 0;
        this.reinforcementsButton.MoveOnClick = false;
        this.reinforcementsButton.Active = false;
      }
      else
      {
        this.reinforcementsButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[12];
        this.reinforcementsButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[7];
        this.reinforcementsButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[7];
        this.reinforcementsButton.CustomTooltipID = 2902;
        this.reinforcementsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggleToReinforcements), "AllArmiesPanel2_reinforcements");
        this.reinforcementsButton.MoveOnClick = true;
        this.reinforcementsButton.Active = true;
      }
      this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.reinforcementsButton);
      this.tradeButton.Position = new Point(806, 4);
      if (AllArmiesPanel2.mode == 1)
      {
        this.tradeButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[3];
        this.tradeButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[3];
        this.tradeButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[3];
        this.tradeButton.CustomTooltipID = 0;
        this.tradeButton.MoveOnClick = false;
        this.tradeButton.Active = false;
      }
      else
      {
        this.tradeButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[13];
        this.tradeButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[8];
        this.tradeButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[8];
        this.tradeButton.CustomTooltipID = 2903;
        this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggleToMerchants), "AllArmiesPanel2_trade");
        this.tradeButton.MoveOnClick = true;
        this.tradeButton.Active = true;
      }
      this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.tradeButton);
      this.monkButton.Position = new Point(870, 4);
      if (AllArmiesPanel2.mode == 2)
      {
        this.monkButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[4];
        this.monkButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[4];
        this.monkButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[4];
        this.monkButton.CustomTooltipID = 0;
        this.monkButton.MoveOnClick = false;
        this.monkButton.Active = false;
      }
      else
      {
        this.monkButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[14];
        this.monkButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[9];
        this.monkButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[9];
        this.monkButton.CustomTooltipID = 2904;
        this.monkButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggleToMonks), "AllArmiesPanel2_monks");
        this.monkButton.MoveOnClick = true;
        this.monkButton.Active = true;
      }
      this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.monkButton);
      this.blockYSize = (height - 40 - 56) / 2;
      this.headerLabelsImage.Size = new Size(this.Width - 25 - 23, 28);
      this.headerLabelsImage.Position = new Point(25, 5);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.brown_mail2_field_bar_mail_left, (Image) GFXLibrary.brown_mail2_field_bar_mail_middle, (Image) GFXLibrary.brown_mail2_field_bar_mail_right);
      this.divider1Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
      this.divider1Image.Position = new Point(228, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider1Image);
      this.divider2Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
      this.divider2Image.Position = new Point(450, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider2Image);
      this.divider3Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
      this.divider3Image.Position = new Point(828, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider3Image);
      if (AllArmiesPanel2.mode == 1)
      {
        this.divider7Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
        this.divider7Image.Position = new Point(570, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider7Image);
      }
      else if (AllArmiesPanel2.mode == 2)
      {
        this.divider7Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
        this.divider7Image.Position = new Point(520, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider7Image);
      }
      if (AllArmiesPanel2.mode == 0)
        this.outGoingAttacksLabel.Text = SK.Text("AllArmiesPanel_Outgoing_Attacks_To", "Outgoing Attacks To");
      else if (AllArmiesPanel2.mode == 1)
        this.outGoingAttacksLabel.Text = SK.Text("AllArmiesPanel_Your_Trades", "Your Merchants To");
      else if (AllArmiesPanel2.mode == 2)
        this.outGoingAttacksLabel.Text = SK.Text("AllArmiesPanel_Your_Monks", "Your Monks To");
      else if (AllArmiesPanel2.mode == 3)
        this.outGoingAttacksLabel.Text = SK.Text("AllArmiesPanel_Your_Scouts", "Your Scouting of");
      else if (AllArmiesPanel2.mode == 4)
        this.outGoingAttacksLabel.Text = SK.Text("AllArmiesPanel_Your_Reinforcements", "Outgoing Reinforcements");
      this.outGoingAttacksLabel.Color = ARGBColors.Black;
      this.outGoingAttacksLabel.Position = new Point(12, -2);
      this.outGoingAttacksLabel.Size = new Size(223, this.headerLabelsImage.Height);
      this.outGoingAttacksLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.outGoingAttacksLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.outGoingAttacksLabel);
      this.outGoingFromLabel.Text = SK.Text("AllArmiesPanel_From", "From");
      this.outGoingFromLabel.Color = ARGBColors.Black;
      this.outGoingFromLabel.Position = new Point(233, -2);
      this.outGoingFromLabel.Size = new Size(223, this.headerLabelsImage.Height);
      this.outGoingFromLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.outGoingFromLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.outGoingFromLabel);
      this.outGoingArrivesLabel.Text = SK.Text("AllArmiesPanel_Arrives", "Arrives");
      this.outGoingArrivesLabel.Color = ARGBColors.Black;
      this.outGoingArrivesLabel.Position = new Point(833, -2);
      this.outGoingArrivesLabel.Size = new Size(114, this.headerLabelsImage.Height);
      this.outGoingArrivesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.outGoingArrivesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.outGoingArrivesLabel);
      if (AllArmiesPanel2.mode == 1)
      {
        this.outGoingCarryingLabel.Text = SK.Text("AllArmiesPanel_Trader_Carrying", "Carrying");
        this.outGoingCarryingLabel.Color = ARGBColors.Black;
        this.outGoingCarryingLabel.Position = new Point(455, -2);
        this.outGoingCarryingLabel.Size = new Size(110, this.headerLabelsImage.Height);
        this.outGoingCarryingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.outGoingCarryingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.outGoingCarryingLabel);
        this.outGoingStatusLabel.Text = SK.Text("AllArmiesPanel_Trader_Status", "Status");
        this.outGoingStatusLabel.Color = ARGBColors.Black;
        this.outGoingStatusLabel.Position = new Point(575, -2);
        this.outGoingStatusLabel.Size = new Size(250, this.headerLabelsImage.Height);
        this.outGoingStatusLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.outGoingStatusLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.outGoingStatusLabel);
      }
      if (AllArmiesPanel2.mode == 2)
      {
        this.outGoingStatusLabel.Text = SK.Text("AllArmiesPanel_Monk_Command", "Command");
        this.outGoingStatusLabel.Color = ARGBColors.Black;
        this.outGoingStatusLabel.Position = new Point(525, -2);
        this.outGoingStatusLabel.Size = new Size(250, this.headerLabelsImage.Height);
        this.outGoingStatusLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.outGoingStatusLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.outGoingStatusLabel);
      }
      this.headerLabelsImage2.Size = new Size(this.Width - 25 - 23, 28);
      this.headerLabelsImage2.Position = new Point(25, this.blockYSize + 5);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage2);
      this.headerLabelsImage2.Create((Image) GFXLibrary.brown_mail2_field_bar_mail_left, (Image) GFXLibrary.brown_mail2_field_bar_mail_middle, (Image) GFXLibrary.brown_mail2_field_bar_mail_right);
      this.divider4Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
      this.divider4Image.Position = new Point(228, 0);
      this.headerLabelsImage2.addControl((CustomSelfDrawPanel.CSDControl) this.divider4Image);
      this.divider5Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
      this.divider5Image.Position = new Point(450, 0);
      this.headerLabelsImage2.addControl((CustomSelfDrawPanel.CSDControl) this.divider5Image);
      this.divider6Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
      this.divider6Image.Position = new Point(828, 0);
      this.headerLabelsImage2.addControl((CustomSelfDrawPanel.CSDControl) this.divider6Image);
      if (AllArmiesPanel2.mode == 0)
        this.incomingAttacksLabel.Text = SK.Text("AllArmiesPanel_Incoming_Attacks_From", "Incoming Attacks From");
      else if (AllArmiesPanel2.mode == 1)
        this.incomingAttacksLabel.Text = SK.Text("AllArmiesPanel_Trades_from", "Incoming Merchants From");
      else if (AllArmiesPanel2.mode == 2)
        this.incomingAttacksLabel.Text = SK.Text("AllArmiesPanel_Incoming_Monks", "Incoming Monks From");
      else if (AllArmiesPanel2.mode == 3)
        this.incomingAttacksLabel.Text = SK.Text("AllArmiesPanel_Incoming_Scouts", "Incoming Scouts From");
      else if (AllArmiesPanel2.mode == 4)
        this.incomingAttacksLabel.Text = SK.Text("AllArmiesPanel_Incoming_Reinforcements", "Incoming Reinforcements From");
      this.incomingAttacksLabel.Color = ARGBColors.Black;
      this.incomingAttacksLabel.Position = new Point(12, -2);
      this.incomingAttacksLabel.Size = new Size(224, this.headerLabelsImage.Height);
      this.incomingAttacksLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.incomingAttacksLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage2.addControl((CustomSelfDrawPanel.CSDControl) this.incomingAttacksLabel);
      this.incomingAttackingLabel.Text = AllArmiesPanel2.mode != 0 ? SK.Text("GENERIC_To", "To") : SK.Text("GENERIC_Attacking", "Attacking");
      this.incomingAttackingLabel.Color = ARGBColors.Black;
      this.incomingAttackingLabel.Position = new Point(233, -2);
      this.incomingAttackingLabel.Size = new Size(224, this.headerLabelsImage.Height);
      this.incomingAttackingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.incomingAttackingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage2.addControl((CustomSelfDrawPanel.CSDControl) this.incomingAttackingLabel);
      this.incomingArrivesLabel.Text = SK.Text("AllArmiesPanel_Arrives", "Arrives");
      this.incomingArrivesLabel.Color = ARGBColors.Black;
      this.incomingArrivesLabel.Position = new Point(833, -2);
      this.incomingArrivesLabel.Size = new Size(114, this.headerLabelsImage.Height);
      this.incomingArrivesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.incomingArrivesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage2.addControl((CustomSelfDrawPanel.CSDControl) this.incomingArrivesLabel);
      this.outgoingScrollArea.Position = new Point(25, 40);
      this.outgoingScrollArea.Size = new Size(915, this.blockYSize - 40 - 10);
      this.outgoingScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(915, this.blockYSize - 40 - 10));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.outgoingScrollArea);
      int num1 = this.outgoingScrollBar.Value;
      this.outgoingScrollBar.Position = new Point(943, 40);
      this.outgoingScrollBar.Size = new Size(24, this.blockYSize - 40 - 10);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.outgoingScrollBar);
      this.outgoingScrollBar.Value = 0;
      this.outgoingScrollBar.Max = 100;
      this.outgoingScrollBar.NumVisibleLines = 25;
      this.outgoingScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary.brown_24wide_thumb_top, (Image) GFXLibrary.brown_24wide_thumb_middle, (Image) GFXLibrary.brown_24wide_thumb_bottom);
      this.outgoingScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      this.incomingScrollArea.Position = new Point(25, 35 + this.blockYSize + 5);
      this.incomingScrollArea.Size = new Size(915, this.blockYSize - 40 - 10);
      this.incomingScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(915, this.blockYSize - 40 - 10));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.incomingScrollArea);
      int num2 = this.incomingScrollBar.Value;
      this.incomingScrollBar.Position = new Point(943, 35 + this.blockYSize + 5);
      this.incomingScrollBar.Size = new Size(24, this.blockYSize - 40 - 10);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.incomingScrollBar);
      this.incomingScrollBar.Value = 0;
      this.incomingScrollBar.Max = 100;
      this.incomingScrollBar.NumVisibleLines = 25;
      this.incomingScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary.brown_24wide_thumb_top, (Image) GFXLibrary.brown_24wide_thumb_middle, (Image) GFXLibrary.brown_24wide_thumb_bottom);
      this.incomingScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.incomingWallScrollBarMoved));
      this.smallPeasantImage2.Visible = false;
      if (AllArmiesPanel2.mode == 0)
      {
        this.smallPeasantImage.Image = (Image) GFXLibrary.armies_screen_troops;
        this.smallPeasantImage.Position = new Point(473, -10);
        this.smallPeasantImage.ClipRect = new Rectangle(0, 0, this.smallPeasantImage.Width - 60, this.smallPeasantImage.Height);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.smallPeasantImage);
        this.smallPeasantImage2.Image = (Image) GFXLibrary.barracks_screen_bottom_units;
        this.smallPeasantImage2.Position = new Point(348, -10);
        this.smallPeasantImage2.ClipRect = new Rectangle(this.smallPeasantImage2.Width - 60, 0, 60, this.smallPeasantImage2.Height);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.smallPeasantImage2);
        this.smallPeasantImage2.Visible = true;
      }
      else if (AllArmiesPanel2.mode == 4)
      {
        this.smallPeasantImage.Image = (Image) GFXLibrary.armies_screen_troops;
        this.smallPeasantImage.Position = new Point(503, -10);
        this.smallPeasantImage.ClipRect = new Rectangle(0, 0, this.smallPeasantImage.Width - 60, this.smallPeasantImage.Height);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.smallPeasantImage);
      }
      else if (AllArmiesPanel2.mode == 3)
      {
        this.smallPeasantImage.Image = (Image) GFXLibrary.armies_screen_troops;
        this.smallPeasantImage.Position = new Point(503 - (this.smallPeasantImage.Width - 60) - 8, -10);
        this.smallPeasantImage.ClipRect = new Rectangle(this.smallPeasantImage.Width - 60, 0, 60, this.smallPeasantImage.Height);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.smallPeasantImage);
      }
      else if (AllArmiesPanel2.mode == 2)
      {
        this.smallPeasantImage.Image = (Image) GFXLibrary.monk_icon;
        this.smallPeasantImage.Position = new Point(468, -10);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.smallPeasantImage);
      }
      if (AllArmiesPanel2.mode == 0 || AllArmiesPanel2.mode == 3)
      {
        SparseArray armyArray = GameEngine.Instance.World.getArmyArray();
        this.armyList.Clear();
        foreach (WorldMap.LocalArmyData localArmyData in armyArray)
          this.armyList.Add(localArmyData);
        this.armyList.Sort((IComparer<WorldMap.LocalArmyData>) this.armyComparer);
        switch (AllArmiesPanel2.mode)
        {
          case 0:
            this.addArmies();
            break;
          case 3:
            this.addScouts();
            break;
        }
      }
      else
      {
        switch (AllArmiesPanel2.mode)
        {
          case 1:
            SparseArray traderArray = GameEngine.Instance.World.getTraderArray();
            this.merchantList.Clear();
            foreach (WorldMap.LocalTrader localTrader in traderArray)
              this.merchantList.Add(localTrader);
            this.merchantList.Sort((IComparer<WorldMap.LocalTrader>) this.merchantComparer);
            this.addMerchants();
            break;
          case 2:
            SparseArray peopleArray = GameEngine.Instance.World.getPeopleArray();
            this.monkList.Clear();
            foreach (WorldMap.LocalPerson localPerson in peopleArray)
              this.monkList.Add(localPerson);
            this.monkList.Sort((IComparer<WorldMap.LocalPerson>) this.monkComparer);
            this.addMonks();
            break;
          case 4:
            SparseArray reinforcementsArray = GameEngine.Instance.World.getReinforcementsArray();
            this.reinforcementList.Clear();
            foreach (WorldMap.LocalArmyData localArmyData in reinforcementsArray)
              this.reinforcementList.Add(localArmyData);
            this.reinforcementList.Sort((IComparer<WorldMap.LocalArmyData>) this.armyComparer);
            this.addReinforcements();
            break;
        }
      }
      if (resized)
      {
        this.outgoingScrollBar.Value = num1;
        this.incomingScrollBar.Value = num2;
      }
      if (AllArmiesPanel2.mode == 0)
      {
        this.diplomacyHeaderLabel.Text = SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy");
        this.diplomacyHeaderLabel.Color = ARGBColors.White;
        this.diplomacyHeaderLabel.DropShadowColor = ARGBColors.Black;
        this.diplomacyHeaderLabel.Position = new Point(20, height - 40 - 40 - 2);
        this.diplomacyHeaderLabel.Size = new Size(100, 30);
        this.diplomacyHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.diplomacyHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyHeaderLabel);
        this.diplomacyTextLabel.Text = SK.Text("AllArmiesPanel_Avoiding_Attacks", "Chance of avoiding an Incoming AI / Enemy Attack") + " : ";
        this.diplomacyTextLabel.Color = ARGBColors.White;
        this.diplomacyTextLabel.DropShadowColor = ARGBColors.Black;
        this.diplomacyTextLabel.Position = new Point(125, height - 40 - 40);
        this.diplomacyTextLabel.Size = new Size(630, 30);
        this.diplomacyTextLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.diplomacyTextLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyTextLabel);
        this.btnDiplomacy.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
        this.btnDiplomacy.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
        this.btnDiplomacy.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
        this.btnDiplomacy.Position = new Point(this.Width - 230, height - 40 - 40 - 4);
        this.btnDiplomacy.Text.Text = SK.Text("AllArmiesPanel_Turn_Off_Diplomacy", "Turn Off Diplomacy");
        this.btnDiplomacy.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.btnDiplomacy.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.btnDiplomacy.TextYOffset = -3;
        this.btnDiplomacy.Text.Color = ARGBColors.Black;
        this.btnDiplomacy.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.diplomacyClick), "AllArmiesPanel2_diplomacy");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnDiplomacy);
        this.updateDiplomacyStatus();
      }
      if (AllArmiesPanel2.mode == 3)
      {
        if (Math.Abs(AllArmiesPanel2.sortMode) >= 3 && Math.Abs(AllArmiesPanel2.sortMode) <= 7 || Math.Abs(AllArmiesPanel2.sortMode) > 9)
          AllArmiesPanel2.sortMode = 0;
      }
      else if (AllArmiesPanel2.mode == 0 || AllArmiesPanel2.mode == 4)
      {
        if (Math.Abs(AllArmiesPanel2.sortMode) > 8 && Math.Abs(AllArmiesPanel2.sortMode) != 20)
          AllArmiesPanel2.sortMode = 0;
      }
      else if (AllArmiesPanel2.mode == 1)
      {
        if (Math.Abs(AllArmiesPanel2.sortMode) >= 3 && Math.Abs(AllArmiesPanel2.sortMode) <= 7 || Math.Abs(AllArmiesPanel2.sortMode) == 9 || Math.Abs(AllArmiesPanel2.sortMode) >= 12)
          AllArmiesPanel2.sortMode = 0;
      }
      else if (AllArmiesPanel2.mode == 2 && (Math.Abs(AllArmiesPanel2.sortMode) >= 3 && Math.Abs(AllArmiesPanel2.sortMode) <= 7 || Math.Abs(AllArmiesPanel2.sortMode) >= 9 && Math.Abs(AllArmiesPanel2.sortMode) < 12))
        AllArmiesPanel2.sortMode = 0;
      this.sortArea1.Position = new Point(7, 0);
      this.sortArea1.Size = new Size(214, 24);
      this.sortArea1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
      this.sortArea1.CustomTooltipData = this.sortArea1.Data = 1;
      this.sortArea1.CustomTooltipID = 2905;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea1);
      this.sortArea1a.Position = new Point(7, 0);
      this.sortArea1a.Size = new Size(214, 24);
      this.sortArea1a.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
      this.sortArea1a.CustomTooltipData = this.sortArea1a.Data = 2;
      this.sortArea1a.CustomTooltipID = 2905;
      this.headerLabelsImage2.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea1a);
      this.sortArea2.Position = new Point(229, 0);
      this.sortArea2.Size = new Size(214, 24);
      this.sortArea2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
      this.sortArea2.CustomTooltipData = this.sortArea2.Data = 2;
      this.sortArea2.CustomTooltipID = 2905;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea2);
      this.sortArea2a.Position = new Point(229, 0);
      this.sortArea2a.Size = new Size(214, 24);
      this.sortArea2a.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
      this.sortArea2a.CustomTooltipData = this.sortArea2a.Data = 1;
      this.sortArea2a.CustomTooltipID = 2905;
      this.headerLabelsImage2.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea2a);
      this.sortArea8.Position = new Point(828, 0);
      this.sortArea8.Size = new Size(100, 24);
      this.sortArea8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
      this.sortArea8.CustomTooltipData = this.sortArea8.Data = 8;
      this.sortArea8.CustomTooltipID = 2905;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea8);
      this.sortArea8a.Position = new Point(828, 0);
      this.sortArea8a.Size = new Size(100, 24);
      this.sortArea8a.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
      this.sortArea8a.CustomTooltipData = this.sortArea8a.Data = 8;
      this.sortArea8a.CustomTooltipID = 2905;
      this.headerLabelsImage2.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea8a);
      switch (AllArmiesPanel2.mode)
      {
        case 0:
          this.sortArea3.Position = new Point(447, 0);
          this.sortArea3.Size = new Size(60, 24);
          this.sortArea3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea3.CustomTooltipData = this.sortArea3.Data = 3;
          this.sortArea3.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea3);
          this.sortArea4.Position = new Point(513, 0);
          this.sortArea4.Size = new Size(60, 24);
          this.sortArea4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea4.CustomTooltipData = this.sortArea4.Data = 4;
          this.sortArea4.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea4);
          this.sortArea5.Position = new Point(570, 0);
          this.sortArea5.Size = new Size(60, 24);
          this.sortArea5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea5.CustomTooltipData = this.sortArea5.Data = 5;
          this.sortArea5.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea5);
          this.sortArea6.Position = new Point(631, 0);
          this.sortArea6.Size = new Size(60, 24);
          this.sortArea6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea6.CustomTooltipData = this.sortArea6.Data = 6;
          this.sortArea6.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea6);
          this.sortArea7.Position = new Point(685, 0);
          this.sortArea7.Size = new Size(60, 24);
          this.sortArea7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea7.CustomTooltipData = this.sortArea7.Data = 7;
          this.sortArea7.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea7);
          this.sortArea9.Position = new Point(739, 0);
          this.sortArea9.Size = new Size(60, 24);
          this.sortArea9.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea9.CustomTooltipData = this.sortArea9.Data = 20;
          this.sortArea9.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea9);
          break;
        case 1:
          this.sortArea3.Position = new Point(449, 0);
          this.sortArea3.Size = new Size(110, 24);
          this.sortArea3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea3.CustomTooltipData = this.sortArea3.Data = 10;
          this.sortArea3.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea3);
          this.sortArea4.Position = new Point(569, 0);
          this.sortArea4.Size = new Size(250, 24);
          this.sortArea4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea4.CustomTooltipData = this.sortArea4.Data = 11;
          this.sortArea4.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea4);
          break;
        case 2:
          this.sortArea3.Position = new Point(457, 0);
          this.sortArea3.Size = new Size(50, 24);
          this.sortArea3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea3.CustomTooltipData = this.sortArea3.Data = 12;
          this.sortArea3.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea3);
          this.sortArea4.Position = new Point(522, 0);
          this.sortArea4.Size = new Size(300, 24);
          this.sortArea4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea4.CustomTooltipData = this.sortArea4.Data = 13;
          this.sortArea4.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea4);
          break;
        case 3:
          this.sortArea3.Position = new Point(477, 0);
          this.sortArea3.Size = new Size(60, 24);
          this.sortArea3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea3.CustomTooltipData = this.sortArea3.Data = 9;
          this.sortArea3.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea3);
          break;
        case 4:
          this.sortArea3.Position = new Point(477, 0);
          this.sortArea3.Size = new Size(60, 24);
          this.sortArea3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea3.CustomTooltipData = this.sortArea3.Data = 3;
          this.sortArea3.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea3);
          this.sortArea4.Position = new Point(543, 0);
          this.sortArea4.Size = new Size(60, 24);
          this.sortArea4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea4.CustomTooltipData = this.sortArea4.Data = 4;
          this.sortArea4.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea4);
          this.sortArea5.Position = new Point(600, 0);
          this.sortArea5.Size = new Size(60, 24);
          this.sortArea5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea5.CustomTooltipData = this.sortArea5.Data = 5;
          this.sortArea5.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea5);
          this.sortArea6.Position = new Point(661, 0);
          this.sortArea6.Size = new Size(60, 24);
          this.sortArea6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea6.CustomTooltipData = this.sortArea6.Data = 6;
          this.sortArea6.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea6);
          this.sortArea7.Position = new Point(715, 0);
          this.sortArea7.Size = new Size(60, 24);
          this.sortArea7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
          this.sortArea7.CustomTooltipData = this.sortArea7.Data = 7;
          this.sortArea7.CustomTooltipID = 2905;
          this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.sortArea7);
          break;
      }
      AllArmiesPanel2.TradersUpdated = false;
      AllArmiesPanel2.MonksUpdated = false;
      this.Invalidate();
    }

    public void update()
    {
      bool flag = false;
      double localTime = DXTimer.GetCurrentMilliseconds() / 1000.0;
      foreach (AllArmiesPanel2.ArmyLine armyLine in this.armyLineList)
      {
        if (!armyLine.update(localTime))
          flag = true;
      }
      foreach (AllArmiesPanel2.ArmyLine armyLine in this.armyLineList2)
      {
        if (!armyLine.update(localTime))
          flag = true;
      }
      foreach (AllArmiesPanel2.MerchantLine merchantLine in this.merchantLineList)
      {
        if (!merchantLine.update(localTime))
          flag = true;
      }
      foreach (AllArmiesPanel2.MerchantLine merchantLine in this.merchantLineList2)
      {
        if (!merchantLine.update(localTime))
          flag = true;
      }
      foreach (AllArmiesPanel2.MonkLine monkLine in this.monkLineList)
      {
        if (!monkLine.update(localTime))
          flag = true;
      }
      foreach (AllArmiesPanel2.MonkLine monkLine in this.monkLineList2)
      {
        if (!monkLine.update(localTime))
          flag = true;
      }
      foreach (AllArmiesPanel2.ScoutLine scoutLine in this.scoutLineList)
      {
        if (!scoutLine.update(localTime))
          flag = true;
      }
      foreach (AllArmiesPanel2.ScoutLine scoutLine in this.scoutLineList2)
      {
        if (!scoutLine.update(localTime))
          flag = true;
      }
      foreach (AllArmiesPanel2.ReinforcementLine reinforcementLine in this.reinforcementLineList)
      {
        if (!reinforcementLine.update(localTime))
          flag = true;
      }
      foreach (AllArmiesPanel2.ReinforcementLine reinforcementLine in this.reinforcementLineList2)
      {
        if (!reinforcementLine.update(localTime))
          flag = true;
      }
      if (!flag && (!InterfaceMgr.Instance.getMainTabBar().isArmiesFlashing() || AllArmiesPanel2.mode != 0) && (AllArmiesPanel2.mode != 1 || !AllArmiesPanel2.TradersUpdated) && (AllArmiesPanel2.mode != 2 || !AllArmiesPanel2.MonksUpdated))
        return;
      DateTime now = DateTime.Now;
      if ((now - this.lastfullUpdate).TotalSeconds <= 1.5)
        return;
      this.lastfullUpdate = now;
      long highestAttackingArmy = -1;
      int numAttacks = GameEngine.Instance.World.countIncomingAttacks(ref highestAttackingArmy);
      InterfaceMgr.Instance.getMainTabBar().incomingAttacks(numAttacks, highestAttackingArmy);
      this.init(true, AllArmiesPanel2.mode);
    }

    public void sortClicked()
    {
      CustomSelfDrawPanel.CSDControl clickedControl = this.ClickedControl;
      if (clickedControl == null)
        return;
      int data = clickedControl.Data;
      AllArmiesPanel2.sortMode = data != AllArmiesPanel2.sortMode ? data : -data;
      this.init(false, AllArmiesPanel2.mode);
    }

    public void updateDiplomacyStatus()
    {
      if (RemoteServices.Instance.ReportFilters.diplomacyActive)
      {
        this.btnDiplomacy.Text.Text = SK.Text("AllArmiesPanel_Turn_Off_Diplomacy", "Turn Off Diplomacy");
        int num = CardTypes.cards_diplomacyExtraPercent(GameEngine.Instance.cardsManager.UserCardData) + ResearchData.diplomacyPercent[(int) GameEngine.Instance.World.UserResearchData.Research_Diplomacy];
        this.diplomacyTextLabel.Text = SK.Text("AllArmiesPanel_Avoiding_Attacks", "Chance of avoiding an Incoming AI / Enemy Attack") + " : " + num.ToString() + "%";
      }
      else
      {
        this.btnDiplomacy.Text.Text = SK.Text("AllArmiesPanel_Turn_On_Diplomacy", "Turn On Diplomacy");
        this.diplomacyTextLabel.Text = SK.Text("AllArmiesPanel_Avoiding_Attacks", "Chance of avoiding an Incoming AI / Enemy Attack") + " : 0% (" + SK.Text("AllArmiesPanel_Diplomacy_Is_Off", "Diplomacy is Off") + ")";
      }
    }

    public void toggleToAttacks()
    {
      if (!this.attackButton.Active)
        return;
      this.init(false, 0);
    }

    public void toggleToScouts()
    {
      if (!this.scoutButton.Active)
        return;
      this.init(false, 3);
    }

    public void toggleToReinforcements()
    {
      if (!this.reinforcementsButton.Active)
        return;
      this.init(false, 4);
    }

    public void toggleToMerchants()
    {
      if (!this.tradeButton.Active)
        return;
      this.init(false, 1);
    }

    public void toggleToMonks()
    {
      if (!this.monkButton.Active)
        return;
      this.init(false, 2);
    }

    public void logout()
    {
    }

    private void wallScrollBarMoved()
    {
      int y = this.outgoingScrollBar.Value;
      this.outgoingScrollArea.Position = new Point(this.outgoingScrollArea.X, 40 - y);
      this.outgoingScrollArea.ClipRect = new Rectangle(this.outgoingScrollArea.ClipRect.X, y, this.outgoingScrollArea.ClipRect.Width, this.outgoingScrollArea.ClipRect.Height);
      this.outgoingScrollArea.invalidate();
      this.outgoingScrollBar.invalidate();
    }

    private void incomingWallScrollBarMoved()
    {
      int y = this.incomingScrollBar.Value;
      this.incomingScrollArea.Position = new Point(this.incomingScrollArea.X, 35 + this.blockYSize + 5 - y);
      this.incomingScrollArea.ClipRect = new Rectangle(this.incomingScrollArea.ClipRect.X, y, this.incomingScrollArea.ClipRect.Width, this.incomingScrollArea.ClipRect.Height);
      this.incomingScrollArea.invalidate();
      this.incomingScrollBar.invalidate();
    }

    public void closing() => InterfaceMgr.Instance.closeDonatePopup();

    public void addArmies()
    {
      this.outgoingScrollArea.clearControls();
      this.incomingScrollArea.clearControls();
      int num1 = 0;
      int num2 = 0;
      this.armyLineList.Clear();
      this.armyLineList2.Clear();
      this.merchantLineList.Clear();
      this.merchantLineList2.Clear();
      this.monkLineList.Clear();
      this.monkLineList2.Clear();
      this.scoutLineList.Clear();
      this.scoutLineList2.Clear();
      this.reinforcementLineList.Clear();
      this.reinforcementLineList2.Clear();
      long highestArmyIDSeen = -1;
      int position1 = 0;
      int position2 = 0;
      double num3 = DXTimer.GetCurrentMilliseconds() / 1000.0;
      foreach (WorldMap.LocalArmyData army in this.armyList)
      {
        if (army.numScouts <= 0)
        {
          if ((GameEngine.Instance.World.isUserVillage(army.travelFromVillageID) || GameEngine.Instance.World.isUserVillage(army.homeVillageID)) && army.attackType != 13)
          {
            AllArmiesPanel2.ArmyLine control = new AllArmiesPanel2.ArmyLine();
            if (num1 != 0)
              num1 += 5;
            control.Position = new Point(0, num1);
            bool showButton = army.lootType < 0;
            if (army.localEndTime == 0.0)
            {
              showButton = false;
            }
            else
            {
              double localEndTime = army.localEndTime;
              if (army.localStartTime + (double) (GameEngine.Instance.LocalWorldData.AttackCancelDuration * 60) < num3)
                showButton = false;
            }
            control.initSent(position1, army.targetVillageID, army.travelFromVillageID, army.numPeasants, army.numArchers, army.numPikemen, army.numSwordsmen, army.numCatapults, army.numScouts, army.serverEndTime, army.armyID, showButton, this, army.lootType >= 0, army.attackType, army.numCaptains);
            this.outgoingScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
            num1 += control.Height;
            this.armyLineList.Add(control);
            ++position1;
          }
          else if (GameEngine.Instance.World.isUserVillage(army.targetVillageID) && army.lootType < 0)
          {
            AllArmiesPanel2.ArmyLine control = new AllArmiesPanel2.ArmyLine();
            if (num2 != 0)
              num2 += 5;
            if (army.armyID > highestArmyIDSeen)
              highestArmyIDSeen = army.armyID;
            control.Position = new Point(0, num2);
            bool tutorial = false;
            if (army.attackType == 13)
              tutorial = true;
            control.initIncoming(position2, army.travelFromVillageID, army.targetVillageID, 0, 0, 0, 0, 0, 0, army.serverEndTime, army.armyID, false, this, false, tutorial, army.attackType, army.numCaptains);
            this.incomingScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
            num2 += control.Height;
            this.armyLineList2.Add(control);
            ++position2;
          }
        }
      }
      if (highestArmyIDSeen > GameEngine.Instance.World.HighestArmyIDSeen)
      {
        GameEngine.Instance.World.HighestArmyIDSeen = highestArmyIDSeen;
        RemoteServices.Instance.SetHighestArmySeen(highestArmyIDSeen);
      }
      this.outgoingScrollArea.Size = new Size(this.outgoingScrollArea.Width, num1);
      if (num1 < this.outgoingScrollBar.Height)
      {
        this.outgoingScrollBar.Visible = false;
      }
      else
      {
        this.outgoingScrollBar.Visible = true;
        this.outgoingScrollBar.NumVisibleLines = this.outgoingScrollBar.Height;
        this.outgoingScrollBar.Max = num1 - this.outgoingScrollBar.Height;
      }
      this.outgoingScrollArea.invalidate();
      this.outgoingScrollBar.invalidate();
      this.incomingScrollArea.Size = new Size(this.incomingScrollArea.Width, num2);
      if (num2 < this.incomingScrollBar.Height)
      {
        this.incomingScrollBar.Visible = false;
      }
      else
      {
        this.incomingScrollBar.Visible = true;
        this.incomingScrollBar.NumVisibleLines = this.incomingScrollBar.Height;
        this.incomingScrollBar.Max = num2 - this.incomingScrollBar.Height;
      }
      this.incomingScrollArea.invalidate();
      this.incomingScrollBar.invalidate();
      this.backgroundImage.invalidate();
      this.update();
    }

    public void addScouts()
    {
      this.outgoingScrollArea.clearControls();
      this.incomingScrollArea.clearControls();
      int num1 = 0;
      int num2 = 0;
      this.armyLineList.Clear();
      this.armyLineList2.Clear();
      this.merchantLineList.Clear();
      this.merchantLineList2.Clear();
      this.monkLineList.Clear();
      this.monkLineList2.Clear();
      this.scoutLineList.Clear();
      this.scoutLineList2.Clear();
      this.reinforcementLineList.Clear();
      this.reinforcementLineList2.Clear();
      long highestArmyIDSeen = -1;
      int position1 = 0;
      int position2 = 0;
      double num3 = DXTimer.GetCurrentMilliseconds() / 1000.0;
      foreach (WorldMap.LocalArmyData army in this.armyList)
      {
        if (army.numScouts != 0)
        {
          if (GameEngine.Instance.World.isUserVillage(army.travelFromVillageID) || GameEngine.Instance.World.isUserVillage(army.homeVillageID))
          {
            AllArmiesPanel2.ScoutLine control = new AllArmiesPanel2.ScoutLine();
            if (num1 != 0)
              num1 += 5;
            control.Position = new Point(0, num1);
            bool showButton = army.lootType < 0;
            if (army.localEndTime == 0.0)
            {
              showButton = false;
            }
            else
            {
              double localEndTime = army.localEndTime;
              if (army.localStartTime + (double) (GameEngine.Instance.LocalWorldData.AttackCancelDuration * 60) < num3)
                showButton = false;
            }
            control.initSent(position1, army.targetVillageID, army.travelFromVillageID, army.numPeasants, army.numArchers, army.numPikemen, army.numSwordsmen, army.numCatapults, army.numScouts, army.serverEndTime, army.armyID, showButton, this, army.lootType >= 0);
            this.outgoingScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
            num1 += control.Height;
            this.scoutLineList.Add(control);
            ++position1;
          }
          else if (GameEngine.Instance.World.isUserVillage(army.targetVillageID) && army.lootType < 0)
          {
            AllArmiesPanel2.ScoutLine control = new AllArmiesPanel2.ScoutLine();
            if (num2 != 0)
              num2 += 5;
            if (army.armyID > highestArmyIDSeen)
              highestArmyIDSeen = army.armyID;
            control.Position = new Point(0, num2);
            bool tutorial = false;
            control.initIncoming(position2, army.travelFromVillageID, army.targetVillageID, 0, 0, 0, 0, 0, 0, army.serverEndTime, army.armyID, false, this, false, tutorial, army.attackType);
            this.incomingScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
            num2 += control.Height;
            this.scoutLineList2.Add(control);
            ++position2;
          }
        }
      }
      if (highestArmyIDSeen > GameEngine.Instance.World.HighestArmyIDSeen)
      {
        GameEngine.Instance.World.HighestArmyIDSeen = highestArmyIDSeen;
        RemoteServices.Instance.SetHighestArmySeen(highestArmyIDSeen);
      }
      this.outgoingScrollArea.Size = new Size(this.outgoingScrollArea.Width, num1);
      if (num1 < this.outgoingScrollBar.Height)
      {
        this.outgoingScrollBar.Visible = false;
      }
      else
      {
        this.outgoingScrollBar.Visible = true;
        this.outgoingScrollBar.NumVisibleLines = this.outgoingScrollBar.Height;
        this.outgoingScrollBar.Max = num1 - this.outgoingScrollBar.Height;
      }
      this.outgoingScrollArea.invalidate();
      this.outgoingScrollBar.invalidate();
      this.incomingScrollArea.Size = new Size(this.incomingScrollArea.Width, num2);
      if (num2 < this.incomingScrollBar.Height)
      {
        this.incomingScrollBar.Visible = false;
      }
      else
      {
        this.incomingScrollBar.Visible = true;
        this.incomingScrollBar.NumVisibleLines = this.incomingScrollBar.Height;
        this.incomingScrollBar.Max = num2 - this.incomingScrollBar.Height;
      }
      this.incomingScrollArea.invalidate();
      this.incomingScrollBar.invalidate();
      this.backgroundImage.invalidate();
      this.update();
    }

    public void addReinforcements()
    {
      this.outgoingScrollArea.clearControls();
      this.incomingScrollArea.clearControls();
      int num1 = 0;
      int num2 = 0;
      this.armyLineList.Clear();
      this.armyLineList2.Clear();
      this.merchantLineList.Clear();
      this.merchantLineList2.Clear();
      this.monkLineList.Clear();
      this.monkLineList2.Clear();
      this.scoutLineList.Clear();
      this.scoutLineList2.Clear();
      this.reinforcementLineList.Clear();
      this.reinforcementLineList2.Clear();
      long highestArmyIDSeen = -1;
      int position1 = 0;
      int position2 = 0;
      double num3 = DXTimer.GetCurrentMilliseconds() / 1000.0;
      foreach (WorldMap.LocalArmyData reinforcement in this.reinforcementList)
      {
        if (reinforcement.attackType != 21 || !(reinforcement.serverEndTime < VillageMap.getCurrentServerTime()))
        {
          if (GameEngine.Instance.World.isUserVillage(reinforcement.travelFromVillageID) || GameEngine.Instance.World.isUserVillage(reinforcement.homeVillageID))
          {
            AllArmiesPanel2.ReinforcementLine control = new AllArmiesPanel2.ReinforcementLine();
            if (num1 != 0)
              num1 += 5;
            control.Position = new Point(0, num1);
            control.initSent(position1, reinforcement.targetVillageID, reinforcement.travelFromVillageID, reinforcement.numPeasants, reinforcement.numArchers, reinforcement.numPikemen, reinforcement.numSwordsmen, reinforcement.numCatapults, reinforcement.numScouts, reinforcement.serverEndTime, reinforcement.armyID, reinforcement.attackType == 20, this, reinforcement.attackType == 21);
            this.outgoingScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
            num1 += control.Height;
            this.reinforcementLineList.Add(control);
            ++position1;
          }
          else if (GameEngine.Instance.World.isUserVillage(reinforcement.targetVillageID))
          {
            AllArmiesPanel2.ReinforcementLine control = new AllArmiesPanel2.ReinforcementLine();
            if (num2 != 0)
              num2 += 5;
            control.Position = new Point(0, num2);
            bool tutorial = false;
            control.initIncoming(position2, reinforcement.travelFromVillageID, reinforcement.targetVillageID, reinforcement.numPeasants, reinforcement.numArchers, reinforcement.numPikemen, reinforcement.numSwordsmen, reinforcement.numCatapults, reinforcement.numScouts, reinforcement.serverEndTime, reinforcement.armyID, false, this, reinforcement.attackType == 21, tutorial, 0);
            this.incomingScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
            num2 += control.Height;
            this.reinforcementLineList2.Add(control);
            ++position2;
          }
        }
      }
      if (highestArmyIDSeen > GameEngine.Instance.World.HighestArmyIDSeen)
      {
        GameEngine.Instance.World.HighestArmyIDSeen = highestArmyIDSeen;
        RemoteServices.Instance.SetHighestArmySeen(highestArmyIDSeen);
      }
      this.outgoingScrollArea.Size = new Size(this.outgoingScrollArea.Width, num1);
      if (num1 < this.outgoingScrollBar.Height)
      {
        this.outgoingScrollBar.Visible = false;
      }
      else
      {
        this.outgoingScrollBar.Visible = true;
        this.outgoingScrollBar.NumVisibleLines = this.outgoingScrollBar.Height;
        this.outgoingScrollBar.Max = num1 - this.outgoingScrollBar.Height;
      }
      this.outgoingScrollArea.invalidate();
      this.outgoingScrollBar.invalidate();
      this.incomingScrollArea.Size = new Size(this.incomingScrollArea.Width, num2);
      if (num2 < this.incomingScrollBar.Height)
      {
        this.incomingScrollBar.Visible = false;
      }
      else
      {
        this.incomingScrollBar.Visible = true;
        this.incomingScrollBar.NumVisibleLines = this.incomingScrollBar.Height;
        this.incomingScrollBar.Max = num2 - this.incomingScrollBar.Height;
      }
      this.incomingScrollArea.invalidate();
      this.incomingScrollBar.invalidate();
      this.backgroundImage.invalidate();
      this.update();
    }

    public void addMerchants()
    {
      this.outgoingScrollArea.clearControls();
      this.incomingScrollArea.clearControls();
      int num1 = 0;
      int num2 = 0;
      this.armyLineList.Clear();
      this.armyLineList2.Clear();
      this.merchantLineList.Clear();
      this.merchantLineList2.Clear();
      this.monkLineList.Clear();
      this.monkLineList2.Clear();
      this.scoutLineList.Clear();
      this.scoutLineList2.Clear();
      this.reinforcementLineList.Clear();
      this.reinforcementLineList2.Clear();
      long highestArmyIDSeen = -1;
      int position1 = 0;
      int position2 = 0;
      double num3 = DXTimer.GetCurrentMilliseconds() / 1000.0;
      foreach (WorldMap.LocalTrader merchant in this.merchantList)
      {
        if (merchant.trader.traderState != 0)
        {
          if (GameEngine.Instance.World.isUserVillage(merchant.trader.homeVillageID) && ((merchant.serverEndTime - VillageMap.getCurrentServerTime()).TotalSeconds > 0.0 || merchant.trader.traderState == 0 || merchant.trader.traderState == 1 || merchant.trader.traderState == 3 || merchant.trader.traderState == 5))
          {
            AllArmiesPanel2.MerchantLine control = new AllArmiesPanel2.MerchantLine();
            if (num1 != 0)
              num1 += 5;
            control.Position = new Point(0, num1);
            control.initSent(position1, merchant.trader.targetVillageID, merchant.trader.homeVillageID, merchant.trader.numTraders, merchant.serverEndTime, merchant.traderID, this, merchant.trader.traderState != 0 && merchant.trader.traderState != 1 && merchant.trader.traderState != 3 && merchant.trader.traderState != 5, merchant.trader.traderState, merchant.trader.resource, merchant.trader.amount);
            this.outgoingScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
            num1 += control.Height;
            this.merchantLineList.Add(control);
            ++position1;
          }
          if (GameEngine.Instance.World.isUserVillage(merchant.trader.targetVillageID) && !GameEngine.Instance.World.isCapital(merchant.trader.targetVillageID) && (merchant.trader.traderState == 0 || merchant.trader.traderState == 1 || merchant.trader.traderState == 3 || merchant.trader.traderState == 5) && (merchant.serverEndTime - VillageMap.getCurrentServerTime()).TotalSeconds > 0.0)
          {
            AllArmiesPanel2.MerchantLine control = new AllArmiesPanel2.MerchantLine();
            if (num2 != 0)
              num2 += 5;
            control.Position = new Point(0, num2);
            control.initIncoming(position2, merchant.trader.homeVillageID, merchant.trader.targetVillageID, 0, merchant.serverEndTime, merchant.traderID, this, false);
            this.incomingScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
            num2 += control.Height;
            this.merchantLineList2.Add(control);
            ++position2;
          }
        }
      }
      if (highestArmyIDSeen > GameEngine.Instance.World.HighestArmyIDSeen)
      {
        GameEngine.Instance.World.HighestArmyIDSeen = highestArmyIDSeen;
        RemoteServices.Instance.SetHighestArmySeen(highestArmyIDSeen);
      }
      this.outgoingScrollArea.Size = new Size(this.outgoingScrollArea.Width, num1);
      if (num1 < this.outgoingScrollBar.Height)
      {
        this.outgoingScrollBar.Visible = false;
      }
      else
      {
        this.outgoingScrollBar.Visible = true;
        this.outgoingScrollBar.NumVisibleLines = this.outgoingScrollBar.Height;
        this.outgoingScrollBar.Max = num1 - this.outgoingScrollBar.Height;
      }
      this.outgoingScrollArea.invalidate();
      this.outgoingScrollBar.invalidate();
      this.incomingScrollArea.Size = new Size(this.incomingScrollArea.Width, num2);
      if (num2 < this.incomingScrollBar.Height)
      {
        this.incomingScrollBar.Visible = false;
      }
      else
      {
        this.incomingScrollBar.Visible = true;
        this.incomingScrollBar.NumVisibleLines = this.incomingScrollBar.Height;
        this.incomingScrollBar.Max = num2 - this.incomingScrollBar.Height;
      }
      this.incomingScrollArea.invalidate();
      this.incomingScrollBar.invalidate();
      this.backgroundImage.invalidate();
      this.update();
    }

    public void addMonks()
    {
      this.outgoingScrollArea.clearControls();
      this.incomingScrollArea.clearControls();
      int num1 = 0;
      int num2 = 0;
      this.armyLineList.Clear();
      this.armyLineList2.Clear();
      this.merchantLineList.Clear();
      this.merchantLineList2.Clear();
      this.monkLineList.Clear();
      this.monkLineList2.Clear();
      this.scoutLineList.Clear();
      this.scoutLineList2.Clear();
      this.reinforcementLineList.Clear();
      this.reinforcementLineList2.Clear();
      long highestArmyIDSeen = -1;
      int position1 = 0;
      int position2 = 0;
      double num3 = DXTimer.GetCurrentMilliseconds() / 1000.0;
      foreach (WorldMap.LocalPerson monk in this.monkList)
      {
        if (monk.person.personType == 4 && monk.person.state > 0 && monk.parentPerson < 0L)
        {
          if (GameEngine.Instance.World.isUserVillage(monk.person.homeVillageID) && (monk.serverEndTime - VillageMap.getCurrentServerTime()).TotalSeconds > 1.0)
          {
            AllArmiesPanel2.MonkLine control = new AllArmiesPanel2.MonkLine();
            if (num1 != 0)
              num1 += 5;
            control.Position = new Point(0, num1);
            control.initSent(position1, monk.person.targetVillageID, monk.person.homeVillageID, monk.childrenCount + 1, monk.serverEndTime, monk.personID, this, monk.person.command);
            this.outgoingScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
            num1 += control.Height;
            this.monkLineList.Add(control);
            ++position1;
          }
          if (GameEngine.Instance.World.isUserVillage(monk.person.targetVillageID) && !GameEngine.Instance.World.isCapital(monk.person.targetVillageID) && (monk.serverEndTime - VillageMap.getCurrentServerTime()).TotalSeconds > 1.0)
          {
            AllArmiesPanel2.MonkLine control = new AllArmiesPanel2.MonkLine();
            if (num2 != 0)
              num2 += 5;
            control.Position = new Point(0, num2);
            control.initIncoming(position2, monk.person.homeVillageID, monk.person.targetVillageID, 0, monk.serverEndTime, monk.personID, this);
            this.incomingScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
            num2 += control.Height;
            this.monkLineList2.Add(control);
            ++position2;
          }
        }
      }
      if (highestArmyIDSeen > GameEngine.Instance.World.HighestArmyIDSeen)
      {
        GameEngine.Instance.World.HighestArmyIDSeen = highestArmyIDSeen;
        RemoteServices.Instance.SetHighestArmySeen(highestArmyIDSeen);
      }
      this.outgoingScrollArea.Size = new Size(this.outgoingScrollArea.Width, num1);
      if (num1 < this.outgoingScrollBar.Height)
      {
        this.outgoingScrollBar.Visible = false;
      }
      else
      {
        this.outgoingScrollBar.Visible = true;
        this.outgoingScrollBar.NumVisibleLines = this.outgoingScrollBar.Height;
        this.outgoingScrollBar.Max = num1 - this.outgoingScrollBar.Height;
      }
      this.outgoingScrollArea.invalidate();
      this.outgoingScrollBar.invalidate();
      this.incomingScrollArea.Size = new Size(this.incomingScrollArea.Width, num2);
      if (num2 < this.incomingScrollBar.Height)
      {
        this.incomingScrollBar.Visible = false;
      }
      else
      {
        this.incomingScrollBar.Visible = true;
        this.incomingScrollBar.NumVisibleLines = this.incomingScrollBar.Height;
        this.incomingScrollBar.Max = num2 - this.incomingScrollBar.Height;
      }
      this.incomingScrollArea.invalidate();
      this.incomingScrollBar.invalidate();
      this.backgroundImage.invalidate();
      this.update();
    }

    private void diplomacyClick()
    {
      if (GameEngine.Instance.World.WorldEnded)
        return;
      bool state = !RemoteServices.Instance.ReportFilters.diplomacyActive;
      this.btnDiplomacy.Enabled = false;
      RemoteServices.Instance.ReportFilters.diplomacyActive = state;
      RemoteServices.Instance.set_UpdateDiplomacyStatus_UserCallBack(new RemoteServices.UpdateDiplomacyStatus_UserCallBack(this.UpdateDiplomacyStatusCallBack));
      RemoteServices.Instance.UpdateDiplomacyStatus(state);
      this.updateDiplomacyStatus();
    }

    public void UpdateDiplomacyStatusCallBack(UpdateDiplomacyStatus_ReturnType returnData)
    {
      this.btnDiplomacy.Enabled = true;
      int num = returnData.Success ? 1 : 0;
    }

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
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (AllArmiesPanel2);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public class ArmyComparer : IComparer<WorldMap.LocalArmyData>
    {
      public int Compare(WorldMap.LocalArmyData x, WorldMap.LocalArmyData y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (AllArmiesPanel2.sortMode < 0)
        {
          WorldMap.LocalArmyData localArmyData = x;
          x = y;
          y = localArmyData;
        }
        bool flag1 = true;
        switch (AllArmiesPanel2.mode)
        {
          case 0:
            switch (Math.Abs(AllArmiesPanel2.sortMode))
            {
              case 3:
              case 4:
              case 5:
              case 6:
              case 7:
              case 20:
                bool flag2 = false;
                bool flag3 = false;
                if ((GameEngine.Instance.World.isUserVillage(x.travelFromVillageID) || GameEngine.Instance.World.isUserVillage(x.homeVillageID)) && x.attackType != 13)
                  flag2 = true;
                if ((GameEngine.Instance.World.isUserVillage(y.travelFromVillageID) || GameEngine.Instance.World.isUserVillage(y.homeVillageID)) && y.attackType != 13)
                  flag3 = true;
                if (!flag2 && !flag3)
                  flag1 = false;
                if (!flag2)
                  return 1;
                if (!flag3)
                  return -1;
                break;
            }
            break;
          case 3:
            if (Math.Abs(AllArmiesPanel2.sortMode) == 9)
            {
              bool flag4 = false;
              bool flag5 = false;
              if (GameEngine.Instance.World.isUserVillage(x.travelFromVillageID) || GameEngine.Instance.World.isUserVillage(x.homeVillageID))
                flag4 = true;
              if (GameEngine.Instance.World.isUserVillage(y.travelFromVillageID) || GameEngine.Instance.World.isUserVillage(y.homeVillageID))
                flag5 = true;
              if (!flag4 && !flag5)
                flag1 = false;
              if (!flag4)
                return 1;
              if (!flag5)
                return -1;
              break;
            }
            break;
        }
        if (flag1)
        {
          switch (AllArmiesPanel2.sortMode)
          {
            case -20:
            case 20:
              int num1 = x.numCaptains.CompareTo(y.numCaptains);
              if (num1 != 0)
                return num1;
              break;
            case -9:
            case 9:
              int num2 = x.numScouts.CompareTo(y.numScouts);
              if (num2 != 0)
                return num2;
              break;
            case -8:
            case 8:
              int num3 = x.serverEndTime.CompareTo(y.serverEndTime);
              if (num3 != 0)
                return num3;
              break;
            case -7:
            case 7:
              int num4 = x.numCatapults.CompareTo(y.numCatapults);
              if (num4 != 0)
                return num4;
              break;
            case -6:
            case 6:
              int num5 = x.numSwordsmen.CompareTo(y.numSwordsmen);
              if (num5 != 0)
                return num5;
              break;
            case -5:
            case 5:
              int num6 = x.numPikemen.CompareTo(y.numPikemen);
              if (num6 != 0)
                return num6;
              break;
            case -4:
            case 4:
              int num7 = x.numArchers.CompareTo(y.numArchers);
              if (num7 != 0)
                return num7;
              break;
            case -3:
            case 3:
              int num8 = x.numPeasants.CompareTo(y.numPeasants);
              if (num8 != 0)
                return num8;
              break;
            case -2:
            case 2:
              int num9 = GameEngine.Instance.World.getVillageName(x.travelFromVillageID).CompareTo(GameEngine.Instance.World.getVillageName(y.travelFromVillageID));
              if (num9 != 0)
                return num9;
              break;
            case -1:
            case 1:
              int num10 = GameEngine.Instance.World.getVillageName(x.targetVillageID).CompareTo(GameEngine.Instance.World.getVillageName(y.targetVillageID));
              if (num10 != 0)
                return num10;
              break;
          }
        }
        if (x.armyID > y.armyID)
          return 1;
        return x.armyID < y.armyID ? -1 : 0;
      }
    }

    public class MerchantComparer : IComparer<WorldMap.LocalTrader>
    {
      public int Compare(WorldMap.LocalTrader x, WorldMap.LocalTrader y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (AllArmiesPanel2.sortMode < 0)
        {
          WorldMap.LocalTrader localTrader = x;
          x = y;
          y = localTrader;
        }
        bool flag1 = true;
        if (AllArmiesPanel2.sortMode == 10)
        {
          bool flag2 = false;
          bool flag3 = false;
          if (GameEngine.Instance.World.isUserVillage(x.trader.homeVillageID) && ((x.serverEndTime - VillageMap.getCurrentServerTime()).TotalSeconds > 0.0 || x.trader.traderState == 0 || x.trader.traderState == 1 || x.trader.traderState == 3 || x.trader.traderState == 5))
            flag2 = true;
          if (GameEngine.Instance.World.isUserVillage(y.trader.homeVillageID) && ((y.serverEndTime - VillageMap.getCurrentServerTime()).TotalSeconds > 0.0 || y.trader.traderState == 0 || y.trader.traderState == 1 || y.trader.traderState == 3 || y.trader.traderState == 5))
            flag3 = true;
          if (!flag2 && !flag3)
            flag1 = false;
          if (!flag2)
            return 1;
          if (!flag3)
            return -1;
        }
        if (flag1)
        {
          switch (AllArmiesPanel2.sortMode)
          {
            case -11:
            case 11:
              int num1 = 0;
              switch (x.trader.traderState)
              {
                case 1:
                  num1 = 1;
                  break;
                case 2:
                case 4:
                case 6:
                  num1 = 4;
                  break;
                case 3:
                  num1 = 2;
                  break;
                case 5:
                  num1 = 3;
                  break;
              }
              int num2 = 0;
              switch (y.trader.traderState)
              {
                case 1:
                  num2 = 1;
                  break;
                case 2:
                case 4:
                case 6:
                  num2 = 4;
                  break;
                case 3:
                  num2 = 2;
                  break;
                case 5:
                  num2 = 3;
                  break;
              }
              int num3 = num1.CompareTo(num2);
              if (num3 != 0)
                return num3;
              break;
            case -10:
            case 10:
              int num4 = 0;
              int num5 = 0;
              int num6 = 0;
              int num7 = 0;
              if (x.trader.traderState == 1 || x.trader.traderState == 3 || x.trader.traderState == 6)
              {
                num4 = x.trader.resource;
                num6 = x.trader.amount;
              }
              if (y.trader.traderState == 1 || y.trader.traderState == 3 || y.trader.traderState == 6)
              {
                num5 = y.trader.resource;
                num7 = y.trader.amount;
              }
              int num8 = num4.CompareTo(num5);
              if (num8 != 0)
                return num8;
              int num9 = num7.CompareTo(num6);
              if (num9 != 0)
                return num9;
              break;
            case -8:
            case 8:
              int num10 = x.serverEndTime.CompareTo(y.serverEndTime);
              if (num10 != 0)
                return num10;
              break;
            case -2:
            case 2:
              int num11 = GameEngine.Instance.World.getVillageName(x.trader.homeVillageID).CompareTo(GameEngine.Instance.World.getVillageName(y.trader.homeVillageID));
              if (num11 != 0)
                return num11;
              break;
            case -1:
            case 1:
              int num12 = GameEngine.Instance.World.getVillageName(x.trader.targetVillageID).CompareTo(GameEngine.Instance.World.getVillageName(y.trader.targetVillageID));
              if (num12 != 0)
                return num12;
              break;
          }
        }
        if (x.traderID > y.traderID)
          return 1;
        return x.traderID < y.traderID ? -1 : 0;
      }
    }

    public class MonkComparer : IComparer<WorldMap.LocalPerson>
    {
      public int Compare(WorldMap.LocalPerson x, WorldMap.LocalPerson y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        bool flag1 = true;
        if (AllArmiesPanel2.sortMode == 10)
        {
          bool flag2 = false;
          bool flag3 = false;
          if ((x.person.personType != 4 || x.person.state <= 0 || x.parentPerson >= 0L) && GameEngine.Instance.World.isUserVillage(x.person.homeVillageID))
            flag2 = true;
          if ((y.person.personType != 4 || y.person.state <= 0 || y.parentPerson >= 0L) && GameEngine.Instance.World.isUserVillage(y.person.homeVillageID))
            flag3 = true;
          if (!flag2 && !flag3)
            flag1 = false;
          if (!flag2)
            return 1;
          if (!flag3)
            return -1;
        }
        if (flag1)
        {
          switch (AllArmiesPanel2.sortMode)
          {
            case -13:
            case 13:
              int num1 = 0;
              int num2 = 0;
              if (GameEngine.Instance.World.isUserVillage(x.person.homeVillageID))
                num1 = x.person.command;
              if (GameEngine.Instance.World.isUserVillage(y.person.homeVillageID))
                num2 = y.person.command;
              int num3 = num1.CompareTo(num2);
              if (num3 != 0)
                return num3;
              break;
            case -12:
            case 12:
              int num4 = 0;
              int num5 = 0;
              if (GameEngine.Instance.World.isUserVillage(x.person.homeVillageID))
                num4 = x.childrenCount + 1;
              if (GameEngine.Instance.World.isUserVillage(y.person.homeVillageID))
                num5 = y.childrenCount + 1;
              int num6 = num5.CompareTo(num4);
              if (num6 != 0)
                return num6;
              break;
            case -8:
            case 8:
              int num7 = x.serverEndTime.CompareTo(y.serverEndTime);
              if (num7 != 0)
                return num7;
              break;
            case -2:
            case 2:
              int num8 = GameEngine.Instance.World.getVillageName(x.person.homeVillageID).CompareTo(GameEngine.Instance.World.getVillageName(y.person.homeVillageID));
              if (num8 != 0)
                return num8;
              break;
            case -1:
            case 1:
              int num9 = GameEngine.Instance.World.getVillageName(x.person.targetVillageID).CompareTo(GameEngine.Instance.World.getVillageName(y.person.targetVillageID));
              if (num9 != 0)
                return num9;
              break;
          }
        }
        if (x.personID > y.personID)
          return 1;
        return x.personID < y.personID ? -1 : 0;
      }
    }

    public class ArmyLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblTarget = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblArchers = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblPikemen = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblSwordsmen = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblCatapults = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblScouts = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblCaptains = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblArrivalTime = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblExtraInfo = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage imgCaptain = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage imgAttackType = new CustomSelfDrawPanel.CSDImage();
      private int m_position = -1000;
      private AllArmiesPanel2 m_parent;
      private int leftVillageID = -1;
      private int rightVillageID = -1;
      private int m_villageID = -1;
      private int m_otherVillageID = -1;
      private bool m_returning;
      private long m_armyID = -1;
      private DateTime m_arrivalTime = DateTime.Now;
      private WorldMap.LocalArmyData m_army;
      private int m_origLoot = -1;
      private bool incoming = true;

      public void initSent(
        int position,
        int villageID,
        int otherVillage,
        int numPeasants,
        int numArchers,
        int numPikemen,
        int numSwordsmen,
        int numCatapults,
        int numScouts,
        DateTime arrivalTime,
        long armyID,
        bool showButton,
        AllArmiesPanel2 parent,
        bool returning,
        int attackType,
        int numCaptains)
      {
        this.incoming = true;
        this.initText(position, villageID, otherVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, true, false, attackType, numCaptains);
      }

      public void initIncoming(
        int position,
        int villageID,
        int otherVillage,
        int numPeasants,
        int numArchers,
        int numPikemen,
        int numSwordsmen,
        int numCatapults,
        int numScouts,
        DateTime arrivalTime,
        long armyID,
        bool showButton,
        AllArmiesPanel2 parent,
        bool returning,
        bool tutorial,
        int attackType,
        int numCaptains)
      {
        this.incoming = false;
        this.initText(position, villageID, otherVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, false, tutorial, attackType, numCaptains);
      }

      private void initText(
        int position,
        int villageID,
        int otherVillage,
        int numPeasants,
        int numArchers,
        int numPikemen,
        int numSwordsmen,
        int numCatapults,
        int numScouts,
        DateTime arrivalTime,
        long armyID,
        bool showButton,
        AllArmiesPanel2 parent,
        bool returning,
        bool showTroops,
        bool tutorial,
        int attackType,
        int numCaptains)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.ClipVisible = true;
        this.m_army = GameEngine.Instance.World.getArmy(armyID);
        this.m_origLoot = this.m_army.lootType;
        this.m_armyID = armyID;
        this.m_villageID = villageID;
        this.m_otherVillageID = otherVillage;
        this.m_arrivalTime = arrivalTime;
        this.m_returning = returning;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.brown_lineitem_strip_02_dark : (Image) GFXLibrary.brown_lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.lblVillage.Text = tutorial ? SK.Text("GENERIC_TUTORIAL", "Tutorial") : GameEngine.Instance.World.getVillageNameOrType(villageID);
        this.lblVillage.Color = ARGBColors.Black;
        this.lblVillage.RolloverColor = ARGBColors.White;
        this.lblVillage.Position = new Point(9, 0);
        this.lblVillage.Size = new Size(223, this.backgroundImage.Height);
        this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "AllArmiesPanel2_village");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblVillage);
        this.lblTarget.Text = GameEngine.Instance.World.getVillageNameOrType(otherVillage);
        this.lblTarget.Color = ARGBColors.Black;
        this.lblTarget.RolloverColor = ARGBColors.White;
        this.lblTarget.Position = new Point(233, 0);
        this.lblTarget.Size = new Size(224, this.backgroundImage.Height);
        this.lblTarget.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblTarget.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblTarget.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblTarget_Click), "AllArmiesPanel2_targetVillage");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblTarget);
        this.leftVillageID = villageID;
        this.rightVillageID = otherVillage;
        this.lblArrivalTime.Text = "";
        this.lblArrivalTime.Color = ARGBColors.Black;
        this.lblArrivalTime.Position = new Point(833, 0);
        this.lblArrivalTime.Size = new Size(114, this.backgroundImage.Height);
        this.lblArrivalTime.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblArrivalTime.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblArrivalTime);
        if (showTroops)
        {
          this.lblPeasants.Text = numPeasants.ToString();
          this.lblPeasants.Color = ARGBColors.Black;
          this.lblPeasants.Position = new Point(455, 0);
          this.lblPeasants.Size = new Size(55, this.backgroundImage.Height);
          this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
          this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblPeasants);
          this.lblArchers.Text = numArchers.ToString();
          this.lblArchers.Color = ARGBColors.Black;
          this.lblArchers.Position = new Point(515, 0);
          this.lblArchers.Size = new Size(55, this.backgroundImage.Height);
          this.lblArchers.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
          this.lblArchers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblArchers);
          this.lblPikemen.Text = numPikemen.ToString();
          this.lblPikemen.Color = ARGBColors.Black;
          this.lblPikemen.Position = new Point(575, 0);
          this.lblPikemen.Size = new Size(55, this.backgroundImage.Height);
          this.lblPikemen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
          this.lblPikemen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblPikemen);
          this.lblSwordsmen.Text = numSwordsmen.ToString();
          this.lblSwordsmen.Color = ARGBColors.Black;
          this.lblSwordsmen.Position = new Point(635, 0);
          this.lblSwordsmen.Size = new Size(55, this.backgroundImage.Height);
          this.lblSwordsmen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
          this.lblSwordsmen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblSwordsmen);
          this.lblCatapults.Text = numCatapults.ToString();
          this.lblCatapults.Color = ARGBColors.Black;
          this.lblCatapults.Position = new Point(695, 0);
          this.lblCatapults.Size = new Size(55, this.backgroundImage.Height);
          this.lblCatapults.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
          this.lblCatapults.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblCatapults);
          this.lblCaptains.Text = numCaptains.ToString();
          this.lblCaptains.Color = ARGBColors.Black;
          this.lblCaptains.Position = new Point(755, 0);
          this.lblCaptains.Size = new Size(55, this.backgroundImage.Height);
          this.lblCaptains.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
          this.lblCaptains.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblCaptains);
        }
        else if (numCaptains > 0)
        {
          this.imgCaptain.Image = (Image) GFXLibrary.barracks_screen_bottom_units;
          this.imgCaptain.Position = new Point(200, -10);
          this.imgCaptain.ClipRect = new Rectangle(this.imgCaptain.Width - 60, 0, 60, this.imgCaptain.Height);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.imgCaptain);
        }
        if (!this.incoming)
        {
          if (attackType == 30)
          {
            this.lblPeasants.Text = SK.Text("AllArmiesSentLine_Vassal_Support", "Vassal Support");
            this.lblPeasants.Color = ARGBColors.Black;
            this.lblPeasants.Position = new Point(455, 0);
            this.lblPeasants.Size = new Size(250, this.backgroundImage.Height);
            this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
            this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblPeasants);
          }
          if (attackType == 31)
          {
            this.lblPeasants.Text = SK.Text("AllArmiesSentLine_Capital_Support", "Capital Support");
            this.lblPeasants.Color = ARGBColors.Black;
            this.lblPeasants.Position = new Point(455, 0);
            this.lblPeasants.Size = new Size(250, this.backgroundImage.Height);
            this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
            this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblPeasants);
          }
        }
        else
        {
          bool flag1 = false;
          if (attackType == 30)
          {
            this.lblExtraInfo.Text = SK.Text("AllArmiesSentLine_Vassal_Support", "Vassal Support");
            this.lblExtraInfo.Color = ARGBColors.Black;
            this.lblExtraInfo.Position = new Point(455, 7);
            this.lblExtraInfo.Size = new Size(360, this.backgroundImage.Height);
            this.lblExtraInfo.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
            this.lblExtraInfo.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblExtraInfo);
            flag1 = true;
          }
          if (attackType == 31)
          {
            this.lblExtraInfo.Text = SK.Text("AllArmiesSentLine_Capital_Support", "Capital Support");
            this.lblExtraInfo.Color = ARGBColors.Black;
            this.lblExtraInfo.Position = new Point(455, 7);
            this.lblExtraInfo.Size = new Size(360, this.backgroundImage.Height);
            this.lblExtraInfo.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
            this.lblExtraInfo.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblExtraInfo);
            flag1 = true;
          }
          if (flag1)
          {
            this.lblPeasants.Position = new Point(485, -7);
            this.lblArchers.Position = new Point(545, -7);
            this.lblPikemen.Position = new Point(605, -7);
            this.lblSwordsmen.Position = new Point(665, -7);
            this.lblCatapults.Position = new Point(725, -7);
          }
          bool flag2 = true;
          switch (attackType)
          {
            case 1:
              this.imgAttackType.Image = (Image) GFXLibrary.send_army_buttons[3];
              break;
            case 2:
            case 4:
            case 5:
            case 6:
            case 7:
              this.imgAttackType.Image = (Image) GFXLibrary.send_army_buttons[5];
              break;
            case 3:
              this.imgAttackType.Image = (Image) GFXLibrary.send_army_buttons[2];
              break;
            case 9:
              this.imgAttackType.Image = (Image) GFXLibrary.send_army_buttons[4];
              break;
            case 11:
              this.imgAttackType.Image = (Image) GFXLibrary.send_army_buttons[1];
              break;
            case 12:
              this.imgAttackType.Image = (Image) GFXLibrary.send_army_buttons[0];
              break;
            default:
              flag2 = false;
              break;
          }
          if (flag2)
          {
            this.imgAttackType.setSizeToImage();
            this.imgAttackType.setScale(0.40000000596046448);
            this.imgAttackType.Position = new Point(480, -2);
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.imgAttackType);
          }
        }
        this.update(DXTimer.GetCurrentMilliseconds() / 1000.0);
        this.invalidate();
      }

      public bool update(double localTime)
      {
        WorldMap.LocalArmyData army = GameEngine.Instance.World.getArmy(this.m_armyID);
        if (army == null || army.lootType != this.m_origLoot)
          return false;
        int secsLeft = (int) ((this.m_arrivalTime - VillageMap.getCurrentServerTime()).TotalSeconds + 0.5);
        if (secsLeft < 1)
          secsLeft = 0;
        this.lblArrivalTime.Text = VillageMap.createBuildTimeString(secsLeft);
        if (this.m_returning)
        {
          CustomSelfDrawPanel.CSDLabel lblArrivalTime = this.lblArrivalTime;
          lblArrivalTime.Text = lblArrivalTime.Text + " (" + SK.Text("AllArmiesSentLine_Return_Abbreviation", "Rtrn") + ")";
        }
        return true;
      }

      private void lblVillage_Click()
      {
        if (this.leftVillageID < 0)
          return;
        Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.leftVillageID);
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.closeParishPanel();
        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.leftVillageID, false, true, true, false);
      }

      private void lblTarget_Click()
      {
        if (this.rightVillageID < 0)
          return;
        Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.rightVillageID);
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.closeParishPanel();
        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.rightVillageID, false, true, true, false);
      }
    }

    public class ReinforcementLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblTarget = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblArchers = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblPikemen = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblSwordsmen = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblCatapults = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblScouts = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblArrivalTime = new CustomSelfDrawPanel.CSDLabel();
      private int m_position = -1000;
      private AllArmiesPanel2 m_parent;
      private int leftVillageID = -1;
      private int rightVillageID = -1;
      private int m_villageID = -1;
      private int m_otherVillageID = -1;
      private bool m_returning;
      private long m_armyID = -1;
      private DateTime m_arrivalTime = DateTime.Now;
      private WorldMap.LocalArmyData m_army;
      private int m_origLoot = -1;
      private bool m_moving;
      private bool m_sent;

      public void initSent(
        int position,
        int villageID,
        int otherVillage,
        int numPeasants,
        int numArchers,
        int numPikemen,
        int numSwordsmen,
        int numCatapults,
        int numScouts,
        DateTime arrivalTime,
        long armyID,
        bool showButton,
        AllArmiesPanel2 parent,
        bool returning)
      {
        this.m_sent = true;
        this.initText(position, villageID, otherVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, true, false, 0);
      }

      public void initIncoming(
        int position,
        int villageID,
        int otherVillage,
        int numPeasants,
        int numArchers,
        int numPikemen,
        int numSwordsmen,
        int numCatapults,
        int numScouts,
        DateTime arrivalTime,
        long armyID,
        bool showButton,
        AllArmiesPanel2 parent,
        bool returning,
        bool tutorial,
        int attackType)
      {
        this.m_sent = false;
        this.initText(position, villageID, otherVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, false, tutorial, attackType);
      }

      private void initText(
        int position,
        int villageID,
        int otherVillage,
        int numPeasants,
        int numArchers,
        int numPikemen,
        int numSwordsmen,
        int numCatapults,
        int numScouts,
        DateTime arrivalTime,
        long armyID,
        bool showButton,
        AllArmiesPanel2 parent,
        bool returning,
        bool showTroops,
        bool tutorial,
        int attackType)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.ClipVisible = true;
        this.m_army = GameEngine.Instance.World.getReinforcement(armyID);
        this.m_origLoot = this.m_army.lootType;
        this.m_armyID = armyID;
        this.m_villageID = villageID;
        this.m_otherVillageID = otherVillage;
        this.m_arrivalTime = arrivalTime;
        this.m_returning = returning;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.brown_lineitem_strip_02_dark : (Image) GFXLibrary.brown_lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        if (this.m_arrivalTime > VillageMap.getCurrentServerTime())
        {
          this.m_moving = true;
          if (!this.m_sent)
            showButton = false;
        }
        else
          this.m_moving = false;
        this.lblVillage.Text = GameEngine.Instance.World.getVillageNameOrType(villageID);
        this.lblVillage.Color = ARGBColors.Black;
        this.lblVillage.RolloverColor = ARGBColors.White;
        this.lblVillage.Position = new Point(9, 0);
        this.lblVillage.Size = new Size(223, this.backgroundImage.Height);
        this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "AllArmiesPanel2_village");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblVillage);
        this.lblTarget.Text = GameEngine.Instance.World.getVillageNameOrType(otherVillage);
        this.lblTarget.Color = ARGBColors.Black;
        this.lblTarget.RolloverColor = ARGBColors.White;
        this.lblTarget.Position = new Point(233, 0);
        this.lblTarget.Size = new Size(224, this.backgroundImage.Height);
        this.lblTarget.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblTarget.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblTarget.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblTarget_Click), "AllArmiesPanel2_targetVillage");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblTarget);
        this.leftVillageID = villageID;
        this.rightVillageID = otherVillage;
        this.lblArrivalTime.Text = "";
        this.lblArrivalTime.Color = ARGBColors.Black;
        this.lblArrivalTime.Position = new Point(833, 0);
        this.lblArrivalTime.Size = new Size(114, this.backgroundImage.Height);
        this.lblArrivalTime.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblArrivalTime.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblArrivalTime);
        this.lblPeasants.Text = numPeasants.ToString();
        this.lblPeasants.Color = ARGBColors.Black;
        this.lblPeasants.Position = new Point(485, 0);
        this.lblPeasants.Size = new Size(55, this.backgroundImage.Height);
        this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblPeasants);
        this.lblArchers.Text = numArchers.ToString();
        this.lblArchers.Color = ARGBColors.Black;
        this.lblArchers.Position = new Point(545, 0);
        this.lblArchers.Size = new Size(55, this.backgroundImage.Height);
        this.lblArchers.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblArchers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblArchers);
        this.lblPikemen.Text = numPikemen.ToString();
        this.lblPikemen.Color = ARGBColors.Black;
        this.lblPikemen.Position = new Point(605, 0);
        this.lblPikemen.Size = new Size(55, this.backgroundImage.Height);
        this.lblPikemen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblPikemen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblPikemen);
        this.lblSwordsmen.Text = numSwordsmen.ToString();
        this.lblSwordsmen.Color = ARGBColors.Black;
        this.lblSwordsmen.Position = new Point(665, 0);
        this.lblSwordsmen.Size = new Size(55, this.backgroundImage.Height);
        this.lblSwordsmen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblSwordsmen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblSwordsmen);
        this.lblCatapults.Text = numCatapults.ToString();
        this.lblCatapults.Color = ARGBColors.Black;
        this.lblCatapults.Position = new Point(725, 0);
        this.lblCatapults.Size = new Size(55, this.backgroundImage.Height);
        this.lblCatapults.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblCatapults.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblCatapults);
        this.update(DXTimer.GetCurrentMilliseconds() / 1000.0);
        this.invalidate();
      }

      public bool update(double localTime)
      {
        if (GameEngine.Instance.World.getReinforcement(this.m_armyID) == null)
          return false;
        DateTime currentServerTime = VillageMap.getCurrentServerTime();
        if (this.m_arrivalTime.AddSeconds(5.0) < currentServerTime)
        {
          this.lblArrivalTime.Text = "";
          if (this.m_returning && this.m_moving)
          {
            this.m_moving = false;
            return false;
          }
        }
        else
        {
          int secsLeft = (int) ((this.m_arrivalTime - currentServerTime).TotalSeconds + 0.5);
          if (secsLeft < 1)
            secsLeft = 0;
          this.lblArrivalTime.Text = VillageMap.createBuildTimeString(secsLeft);
          if (this.m_sent && this.m_moving)
          {
            CustomSelfDrawPanel.CSDLabel lblArrivalTime = this.lblArrivalTime;
            lblArrivalTime.Text = lblArrivalTime.Text + " (" + SK.Text("AllArmiesSentLine_Return_Abbreviation", "Rtrn") + ")";
          }
        }
        return true;
      }

      private void lblVillage_Click()
      {
        if (this.leftVillageID < 0)
          return;
        Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.leftVillageID);
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.closeParishPanel();
        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.leftVillageID, false, true, true, false);
      }

      private void lblTarget_Click()
      {
        if (this.rightVillageID < 0)
          return;
        Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.rightVillageID);
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.closeParishPanel();
        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.rightVillageID, false, true, true, false);
      }
    }

    public class ScoutLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblTarget = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblScouts = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblArrivalTime = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblCarryingAmount = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage imgCarrying = new CustomSelfDrawPanel.CSDImage();
      private int m_position = -1000;
      private AllArmiesPanel2 m_parent;
      private int leftVillageID = -1;
      private int rightVillageID = -1;
      private int m_villageID = -1;
      private int m_otherVillageID = -1;
      private bool m_returning;
      private long m_armyID = -1;
      private DateTime m_arrivalTime = DateTime.Now;
      private WorldMap.LocalArmyData m_army;
      private int m_origLoot = -1;

      public void initSent(
        int position,
        int villageID,
        int otherVillage,
        int numPeasants,
        int numArchers,
        int numPikemen,
        int numSwordsmen,
        int numCatapults,
        int numScouts,
        DateTime arrivalTime,
        long armyID,
        bool showButton,
        AllArmiesPanel2 parent,
        bool returning)
      {
        this.initText(position, villageID, otherVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, true, false, 0);
      }

      public void initIncoming(
        int position,
        int villageID,
        int otherVillage,
        int numPeasants,
        int numArchers,
        int numPikemen,
        int numSwordsmen,
        int numCatapults,
        int numScouts,
        DateTime arrivalTime,
        long armyID,
        bool showButton,
        AllArmiesPanel2 parent,
        bool returning,
        bool tutorial,
        int attackType)
      {
        this.initText(position, villageID, otherVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, false, tutorial, attackType);
      }

      private void initText(
        int position,
        int villageID,
        int otherVillage,
        int numPeasants,
        int numArchers,
        int numPikemen,
        int numSwordsmen,
        int numCatapults,
        int numScouts,
        DateTime arrivalTime,
        long armyID,
        bool showButton,
        AllArmiesPanel2 parent,
        bool returning,
        bool showTroops,
        bool tutorial,
        int attackType)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.ClipVisible = true;
        this.m_army = GameEngine.Instance.World.getArmy(armyID);
        this.m_origLoot = this.m_army.lootType;
        this.m_armyID = armyID;
        this.m_villageID = villageID;
        this.m_otherVillageID = otherVillage;
        this.m_arrivalTime = arrivalTime;
        this.m_returning = returning;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.brown_lineitem_strip_02_dark : (Image) GFXLibrary.brown_lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.lblVillage.Text = tutorial ? SK.Text("GENERIC_TUTORIAL", "Tutorial") : GameEngine.Instance.World.getVillageNameOrType(villageID);
        this.lblVillage.Color = ARGBColors.Black;
        this.lblVillage.RolloverColor = ARGBColors.White;
        this.lblVillage.Position = new Point(9, 0);
        this.lblVillage.Size = new Size(223, this.backgroundImage.Height);
        this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "AllArmiesPanel2_village");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblVillage);
        this.lblTarget.Text = GameEngine.Instance.World.getVillageNameOrType(otherVillage);
        this.lblTarget.Color = ARGBColors.Black;
        this.lblTarget.RolloverColor = ARGBColors.White;
        this.lblTarget.Position = new Point(233, 0);
        this.lblTarget.Size = new Size(224, this.backgroundImage.Height);
        this.lblTarget.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblTarget.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblTarget.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblTarget_Click), "AllArmiesPanel2_targetVillage");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblTarget);
        this.leftVillageID = villageID;
        this.rightVillageID = otherVillage;
        this.lblArrivalTime.Text = "";
        this.lblArrivalTime.Color = ARGBColors.Black;
        this.lblArrivalTime.Position = new Point(833, 0);
        this.lblArrivalTime.Size = new Size(114, this.backgroundImage.Height);
        this.lblArrivalTime.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblArrivalTime.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblArrivalTime);
        if (showTroops)
        {
          this.lblScouts.Text = numScouts.ToString();
          this.lblScouts.Color = ARGBColors.Black;
          this.lblScouts.Position = new Point(485, 0);
          this.lblScouts.Size = new Size(55, this.backgroundImage.Height);
          this.lblScouts.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
          this.lblScouts.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblScouts);
        }
        if (this.m_origLoot >= 100 && this.m_origLoot < 199)
        {
          this.imgCarrying.Image = (Image) GFXLibrary.getCommodity32DSImage(this.m_origLoot - 100);
          this.imgCarrying.Position = new Point(695, -3);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.imgCarrying);
          this.lblCarryingAmount.Text = this.m_army.lootLevel.ToString("N", (IFormatProvider) GameEngine.NFI);
          this.lblCarryingAmount.Color = ARGBColors.Black;
          this.lblCarryingAmount.Position = new Point(635, 0);
          this.lblCarryingAmount.Size = new Size(55, this.backgroundImage.Height);
          this.lblCarryingAmount.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
          this.lblCarryingAmount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblCarryingAmount);
        }
        this.update(DXTimer.GetCurrentMilliseconds() / 1000.0);
        this.invalidate();
      }

      public bool update(double localTime)
      {
        WorldMap.LocalArmyData army = GameEngine.Instance.World.getArmy(this.m_armyID);
        if (army == null || army.lootType != this.m_origLoot)
          return false;
        int secsLeft = (int) ((this.m_arrivalTime - VillageMap.getCurrentServerTime()).TotalSeconds + 0.5);
        if (secsLeft < 1)
          secsLeft = 0;
        this.lblArrivalTime.Text = VillageMap.createBuildTimeString(secsLeft);
        if (this.m_returning)
        {
          CustomSelfDrawPanel.CSDLabel lblArrivalTime = this.lblArrivalTime;
          lblArrivalTime.Text = lblArrivalTime.Text + " (" + SK.Text("AllArmiesSentLine_Return_Abbreviation", "Rtrn") + ")";
        }
        return true;
      }

      private void lblVillage_Click()
      {
        if (this.leftVillageID < 0)
          return;
        Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.leftVillageID);
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.closeParishPanel();
        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.leftVillageID, false, true, true, false);
      }

      private void lblTarget_Click()
      {
        if (this.rightVillageID < 0)
          return;
        Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.rightVillageID);
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.closeParishPanel();
        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.rightVillageID, false, true, true, false);
      }
    }

    public class MerchantLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblTarget = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblArrivalTime = new CustomSelfDrawPanel.CSDLabel();
      private int m_position = -1000;
      private CustomSelfDrawPanel.CSDImage resourceImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel resourceAmount = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel description = new CustomSelfDrawPanel.CSDLabel();
      private AllArmiesPanel2 m_parent;
      private int leftVillageID = -1;
      private int rightVillageID = -1;
      private int m_villageID = -1;
      private int m_otherVillageID = -1;
      private bool m_returning;
      private long m_armyID = -1;
      private DateTime m_arrivalTime = DateTime.Now;
      private WorldMap.LocalTrader m_army;
      private int m_traderState = -1;
      private int m_realTraderState = -1;

      public void initSent(
        int position,
        int villageID,
        int otherVillage,
        int numTraders,
        DateTime arrivalTime,
        long armyID,
        AllArmiesPanel2 parent,
        bool returning,
        int traderState,
        int resource,
        int amount)
      {
        this.initText(position, villageID, otherVillage, numTraders, arrivalTime, armyID, parent, returning, traderState, resource, amount);
      }

      public void initIncoming(
        int position,
        int villageID,
        int otherVillage,
        int numTraders,
        DateTime arrivalTime,
        long armyID,
        AllArmiesPanel2 parent,
        bool returning)
      {
        this.initText(position, villageID, otherVillage, numTraders, arrivalTime, armyID, parent, returning, -1, 0, 0);
      }

      private void initText(
        int position,
        int villageID,
        int otherVillage,
        int numTraders,
        DateTime arrivalTime,
        long armyID,
        AllArmiesPanel2 parent,
        bool returning,
        int traderState,
        int resource,
        int amount)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.ClipVisible = true;
        this.m_traderState = traderState;
        this.m_army = GameEngine.Instance.World.getTrader(armyID);
        this.m_realTraderState = this.m_army.trader.traderState;
        this.m_armyID = armyID;
        this.m_villageID = villageID;
        this.m_otherVillageID = otherVillage;
        this.m_arrivalTime = arrivalTime;
        this.m_returning = returning;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.brown_lineitem_strip_02_dark : (Image) GFXLibrary.brown_lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.lblVillage.Text = GameEngine.Instance.World.getVillageNameOrType(villageID);
        this.lblVillage.Color = ARGBColors.Black;
        this.lblVillage.RolloverColor = ARGBColors.White;
        this.lblVillage.Position = new Point(9, 0);
        this.lblVillage.Size = new Size(223, this.backgroundImage.Height);
        this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "AllArmiesPanel2_village");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblVillage);
        this.lblTarget.Text = GameEngine.Instance.World.getVillageNameOrType(otherVillage);
        this.lblTarget.Color = ARGBColors.Black;
        this.lblTarget.RolloverColor = ARGBColors.White;
        this.lblTarget.Position = new Point(233, 0);
        this.lblTarget.Size = new Size(224, this.backgroundImage.Height);
        this.lblTarget.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblTarget.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblTarget.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblTarget_Click), "AllArmiesPanel2_targetVillage");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblTarget);
        this.leftVillageID = villageID;
        this.rightVillageID = otherVillage;
        if (traderState == 1 || traderState == 3 || traderState == 6)
        {
          this.resourceImage.Image = (Image) GFXLibrary.getCommodity32DSImage(resource);
          this.resourceImage.Position = new Point(455, -3);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.resourceImage);
          NumberFormatInfo nfi = GameEngine.NFI;
          this.resourceAmount.Text = amount.ToString("N", (IFormatProvider) nfi);
          this.resourceAmount.Color = ARGBColors.Black;
          this.resourceAmount.Position = new Point(490, 0);
          this.resourceAmount.Size = new Size(60, this.backgroundImage.Height);
          this.resourceAmount.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
          this.resourceAmount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.resourceAmount);
        }
        if (traderState >= 1)
        {
          switch (traderState - 1)
          {
            case 0:
              this.description.Text = SK.Text("SelectArmyPanel_Delivering", "Delivering");
              break;
            case 1:
            case 3:
            case 5:
              this.description.Text = SK.Text("SelectArmyPanel_Returning", "Returning");
              break;
            case 2:
              this.description.Text = SK.Text("SelectArmyPanel_Selling", "Selling");
              break;
            case 4:
              this.description.Text = SK.Text("SelectArmyPanel_Collecting", "Collecting");
              break;
          }
          this.description.Color = ARGBColors.Black;
          this.description.Position = new Point(575, 0);
          this.description.Size = new Size(240, this.backgroundImage.Height);
          this.description.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
          this.description.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.description);
        }
        this.lblArrivalTime.Text = "";
        this.lblArrivalTime.Color = ARGBColors.Black;
        this.lblArrivalTime.Position = new Point(833, 0);
        this.lblArrivalTime.Size = new Size(114, this.backgroundImage.Height);
        this.lblArrivalTime.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblArrivalTime.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblArrivalTime);
        this.update(DXTimer.GetCurrentMilliseconds() / 1000.0);
        this.invalidate();
      }

      public bool update(double localTime)
      {
        WorldMap.LocalTrader trader = GameEngine.Instance.World.getTrader(this.m_armyID);
        if (trader == null)
          return false;
        int secsLeft = (int) ((this.m_arrivalTime - VillageMap.getCurrentServerTime()).TotalSeconds + 0.5);
        if (secsLeft < 1)
          secsLeft = 0;
        if (trader.trader.traderState != this.m_realTraderState)
          return false;
        this.lblArrivalTime.Text = VillageMap.createBuildTimeString(secsLeft);
        if (this.m_returning)
        {
          CustomSelfDrawPanel.CSDLabel lblArrivalTime = this.lblArrivalTime;
          lblArrivalTime.Text = lblArrivalTime.Text + " (" + SK.Text("AllArmiesSentLine_Return_Abbreviation", "Rtrn") + ")";
        }
        return true;
      }

      private void lblVillage_Click()
      {
        if (this.leftVillageID < 0)
          return;
        Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.leftVillageID);
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.closeParishPanel();
        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.leftVillageID, false, true, true, false);
      }

      private void lblTarget_Click()
      {
        if (this.rightVillageID < 0)
          return;
        Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.rightVillageID);
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.closeParishPanel();
        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.rightVillageID, false, true, true, false);
      }
    }

    public class MonkLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblTarget = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblMonks = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblCommand = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblArrivalTime = new CustomSelfDrawPanel.CSDLabel();
      private int m_position = -1000;
      private AllArmiesPanel2 m_parent;
      private int leftVillageID = -1;
      private int rightVillageID = -1;
      private int m_villageID = -1;
      private int m_otherVillageID = -1;
      private bool m_returning;
      private long m_armyID = -1;
      private DateTime m_arrivalTime = DateTime.Now;
      private WorldMap.LocalTrader m_army;

      public void initSent(
        int position,
        int villageID,
        int otherVillage,
        int numTraders,
        DateTime arrivalTime,
        long armyID,
        AllArmiesPanel2 parent,
        int command)
      {
        this.initText(position, villageID, otherVillage, numTraders, arrivalTime, armyID, parent, command);
      }

      public void initIncoming(
        int position,
        int villageID,
        int otherVillage,
        int numTraders,
        DateTime arrivalTime,
        long armyID,
        AllArmiesPanel2 parent)
      {
        this.initText(position, villageID, otherVillage, numTraders, arrivalTime, armyID, parent, 0);
      }

      private void initText(
        int position,
        int villageID,
        int otherVillage,
        int numMonks,
        DateTime arrivalTime,
        long armyID,
        AllArmiesPanel2 parent,
        int command)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.ClipVisible = true;
        this.m_army = GameEngine.Instance.World.getTrader(armyID);
        this.m_armyID = armyID;
        this.m_villageID = villageID;
        this.m_otherVillageID = otherVillage;
        this.m_arrivalTime = arrivalTime;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.brown_lineitem_strip_02_dark : (Image) GFXLibrary.brown_lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.lblVillage.Text = GameEngine.Instance.World.getVillageNameOrType(villageID);
        this.lblVillage.Color = ARGBColors.Black;
        this.lblVillage.RolloverColor = ARGBColors.White;
        this.lblVillage.Position = new Point(9, 0);
        this.lblVillage.Size = new Size(223, this.backgroundImage.Height);
        this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "AllArmiesPanel2_village");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblVillage);
        this.lblTarget.Text = GameEngine.Instance.World.getVillageNameOrType(otherVillage);
        this.lblTarget.Color = ARGBColors.Black;
        this.lblTarget.RolloverColor = ARGBColors.White;
        this.lblTarget.Position = new Point(233, 0);
        this.lblTarget.Size = new Size(224, this.backgroundImage.Height);
        this.lblTarget.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblTarget.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblTarget.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblTarget_Click), "AllArmiesPanel2_targetVillage");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblTarget);
        this.leftVillageID = villageID;
        this.rightVillageID = otherVillage;
        if (numMonks > 0)
        {
          this.lblMonks.Text = numMonks.ToString();
          this.lblMonks.Color = ARGBColors.Black;
          this.lblMonks.Position = new Point(455, 0);
          this.lblMonks.Size = new Size(55, this.backgroundImage.Height);
          this.lblMonks.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
          this.lblMonks.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblMonks);
        }
        if (command > 0)
        {
          switch (command)
          {
            case 1:
              this.lblCommand.Text = SK.Text("VillageMapPanel_Blessing", "Blessing");
              break;
            case 2:
              this.lblCommand.Text = SK.Text("SendMonksPanel_Positive_Influence", "Positive Influence");
              break;
            case 3:
              this.lblCommand.Text = SK.Text("VillageMapPanel_Inquisition", "Inquisition");
              break;
            case 4:
              this.lblCommand.Text = SK.Text("SendMonksPanel_Interdiction", "Interdiction");
              break;
            case 5:
              this.lblCommand.Text = SK.Text("SendMonksPanel_Restoration", "Restoration");
              break;
            case 6:
              this.lblCommand.Text = SK.Text("SendMonksPanel_Absolution", "Absolution");
              break;
            case 7:
              this.lblCommand.Text = SK.Text("SendMonksPanel_Excommnunication", "Excommunication");
              break;
            case 8:
              this.lblCommand.Text = SK.Text("SendMonksPanel_Negative_Influence", "Negative Influence");
              break;
          }
          this.lblCommand.Color = ARGBColors.Black;
          this.lblCommand.Position = new Point(525, 0);
          this.lblCommand.Size = new Size(300, this.backgroundImage.Height);
          this.lblCommand.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
          this.lblCommand.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblCommand);
        }
        this.lblArrivalTime.Text = "";
        this.lblArrivalTime.Color = ARGBColors.Black;
        this.lblArrivalTime.Position = new Point(833, 0);
        this.lblArrivalTime.Size = new Size(114, this.backgroundImage.Height);
        this.lblArrivalTime.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblArrivalTime.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblArrivalTime);
        this.update(DXTimer.GetCurrentMilliseconds() / 1000.0);
        this.invalidate();
      }

      public bool update(double localTime)
      {
        if (GameEngine.Instance.World.getPerson(this.m_armyID) == null)
          return false;
        int secsLeft = (int) ((this.m_arrivalTime - VillageMap.getCurrentServerTime()).TotalSeconds + 0.5);
        if (secsLeft < 1)
          return false;
        this.lblArrivalTime.Text = VillageMap.createBuildTimeString(secsLeft);
        if (this.m_returning)
        {
          CustomSelfDrawPanel.CSDLabel lblArrivalTime = this.lblArrivalTime;
          lblArrivalTime.Text = lblArrivalTime.Text + " (" + SK.Text("AllArmiesSentLine_Return_Abbreviation", "Rtrn") + ")";
        }
        return true;
      }

      private void lblVillage_Click()
      {
        if (this.leftVillageID < 0)
          return;
        Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.leftVillageID);
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.closeParishPanel();
        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.leftVillageID, false, true, true, false);
      }

      private void lblTarget_Click()
      {
        if (this.rightVillageID < 0)
          return;
        Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.rightVillageID);
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.closeParishPanel();
        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.rightVillageID, false, true, true, false);
      }
    }
  }
}
