// Decompiled with JetBrains decompiler
// Type: Kingdoms.RankingsPanel
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
  public class RankingsPanel : CustomSelfDrawPanel, IDockableControl
  {
    private const int medalWindow_area_x = 9;
    private const int medalWindow_area_y = 213;
    private const double zoomStep = 0.02;
    private DockableControl dockableControl;
    private IContainer components;
    public static AchievementProgress_ReturnType progressData = (AchievementProgress_ReturnType) null;
    public static DateTime lastProgressDownload = DateTime.MinValue;
    public static bool animating = false;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea mainBackgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDFill filledArea = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDLabel currentRankLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel nextRankLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage rankImage01 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage02 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage03 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage04 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage05 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage06 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage07 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage08 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage09 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage10 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage11 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage12 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage13 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage14 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage15 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage16 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage17 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage18 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage19 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage20 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage21 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rankImage22 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton upgradeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage honourSymbol = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.MedalWindow medalWindow = new CustomSelfDrawPanel.MedalWindow();
    private CustomSelfDrawPanel.MedalWindow selectedMedalWindow = new CustomSelfDrawPanel.MedalWindow();
    private List<RankingsPanel.CSDSlot> progressSlots = new List<RankingsPanel.CSDSlot>();
    private bool allowSharePopup;
    private static int lastRank = 0;
    private static int lastRankSubLevel = 0;
    private static bool newIn = false;
    private bool ignoreSetCurrent;
    private int majorRankUp;
    private bool inUpgrade;
    private bool doingRankAnim;
    private CustomSelfDrawPanel.CSDArea animBack = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea unscaledArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDImage scaledImage = new CustomSelfDrawPanel.CSDImage();
    private double targetScale = 70.0 / 247.0;
    private Point startPos;
    private double dx;
    private double dy;
    private int zoomCount;
    private int numSteps;
    private int fadeCount;
    private CustomSelfDrawPanel.CSDImage cpImage = new CustomSelfDrawPanel.CSDImage();

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
      this.Size = new Size(992, 566);
      this.Name = "RankingsPanel2";
      this.ResumeLayout(false);
    }

    public RankingsPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.NoDrawBackground = true;
    }

    public void logout()
    {
      RankingsPanel.progressData = (AchievementProgress_ReturnType) null;
      RankingsPanel.lastProgressDownload = DateTime.MinValue;
    }

    public void init(bool initialCall)
    {
      RankingsPanel.animating = false;
      this.clearControls();
      this.progressSlots.Clear();
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea, 18, new Point(this.Width - 39, 11));
      this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.mainBackgroundArea.Position = new Point(0, 0);
      this.mainBackgroundArea.Size = new Size(992, 566);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea);
      this.filledArea.Position = new Point(0, 0);
      this.filledArea.Size = this.mainBackgroundArea.Size;
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea, 44, new Point(509, 243));
      this.currentRankLabel.Text = "";
      this.currentRankLabel.Color = Color.FromArgb(224, 203, 146);
      this.currentRankLabel.DropShadowColor = Color.FromArgb(56, 50, 36);
      this.currentRankLabel.Position = new Point(29, 12);
      this.currentRankLabel.Size = new Size(992, 50);
      this.currentRankLabel.Font = !(Program.mySettings.LanguageIdent == "it") ? FontManager.GetFont("Arial", 20f, FontStyle.Bold) : FontManager.GetFont("Arial", 18f, FontStyle.Bold);
      this.currentRankLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.currentRankLabel);
      this.nextRankLabel.Text = "";
      this.nextRankLabel.Color = ARGBColors.Black;
      this.nextRankLabel.Position = new Point(0, 17);
      this.nextRankLabel.Size = new Size(767, 50);
      this.nextRankLabel.Font = !(Program.mySettings.LanguageIdent == "it") ? FontManager.GetFont("Arial", 14f, FontStyle.Bold) : FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.nextRankLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.nextRankLabel);
      this.upgradeButton.Position = new Point(785, 10);
      this.upgradeButton.Size = new Size(168, 38);
      this.upgradeButton.Text.Text = "1,000";
      this.upgradeButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.upgradeButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.upgradeButton.TextYOffset = -1;
      this.upgradeButton.Text.Color = ARGBColors.Black;
      this.upgradeButton.ImageIcon = (Image) GFXLibrary.com_32_honour_DS;
      this.upgradeButton.ImageIconPosition = new Point(5, -5);
      this.upgradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.upgradeClick), "RankingsPanel_rank_up_click");
      this.upgradeButton.CustomTooltipID = 400;
      this.upgradeButton.Enabled = false;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.upgradeButton);
      this.upgradeButton.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
      this.upgradeButton.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      if (GameEngine.Instance.World.getRank() == 22)
      {
        this.rankImage01.Image = (Image) GFXLibrary.rank_progression_crown_prince;
        this.rankImage01.Position = new Point(8, 53);
        this.rankImage01.CustomTooltipID = 401;
        this.rankImage01.CustomTooltipData = 22;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage01);
      }
      else
      {
        this.rankImage01.Image = (Image) GFXLibrary.rank_images[44];
        this.rankImage01.Position = new Point(10, 53);
        this.rankImage01.CustomTooltipID = 401;
        this.rankImage01.CustomTooltipData = 0;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage01);
        this.rankImage02.Image = (Image) GFXLibrary.rank_images[45];
        this.rankImage02.Position = new Point(this.rankImage01.X + this.rankImage01.Width, 53);
        this.rankImage02.CustomTooltipID = 401;
        this.rankImage02.CustomTooltipData = 1;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage02);
        this.rankImage03.Image = (Image) GFXLibrary.rank_images[46];
        this.rankImage03.Position = new Point(this.rankImage02.X + this.rankImage02.Width, 53);
        this.rankImage03.CustomTooltipID = 401;
        this.rankImage03.CustomTooltipData = 2;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage03);
        this.rankImage04.Image = (Image) GFXLibrary.rank_images[47];
        this.rankImage04.Position = new Point(this.rankImage03.X + this.rankImage03.Width, 53);
        this.rankImage04.CustomTooltipID = 401;
        this.rankImage04.CustomTooltipData = 3;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage04);
        this.rankImage05.Image = (Image) GFXLibrary.rank_images[48];
        this.rankImage05.Position = new Point(this.rankImage04.X + this.rankImage04.Width, 53);
        this.rankImage05.CustomTooltipID = 401;
        this.rankImage05.CustomTooltipData = 4;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage05);
        this.rankImage06.Image = (Image) GFXLibrary.rank_images[49];
        this.rankImage06.Position = new Point(this.rankImage05.X + this.rankImage05.Width, 53);
        this.rankImage06.CustomTooltipID = 401;
        this.rankImage06.CustomTooltipData = 5;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage06);
        this.rankImage07.Image = (Image) GFXLibrary.rank_images[50];
        this.rankImage07.Position = new Point(this.rankImage06.X + this.rankImage06.Width, 53);
        this.rankImage07.CustomTooltipID = 401;
        this.rankImage07.CustomTooltipData = 6;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage07);
        this.rankImage08.Image = (Image) GFXLibrary.rank_images[51];
        this.rankImage08.Position = new Point(this.rankImage07.X + this.rankImage07.Width, 53);
        this.rankImage08.CustomTooltipID = 401;
        this.rankImage08.CustomTooltipData = 7;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage08);
        this.rankImage09.Image = (Image) GFXLibrary.rank_images[52];
        this.rankImage09.Position = new Point(this.rankImage08.X + this.rankImage08.Width, 53);
        this.rankImage09.CustomTooltipID = 401;
        this.rankImage09.CustomTooltipData = 8;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage09);
        this.rankImage10.Image = (Image) GFXLibrary.rank_images[53];
        this.rankImage10.Position = new Point(this.rankImage09.X + this.rankImage09.Width, 53);
        this.rankImage10.CustomTooltipID = 401;
        this.rankImage10.CustomTooltipData = 9;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage10);
        this.rankImage11.Image = (Image) GFXLibrary.rank_images[54];
        this.rankImage11.Position = new Point(this.rankImage10.X + this.rankImage10.Width, 53);
        this.rankImage11.CustomTooltipID = 401;
        this.rankImage11.CustomTooltipData = 10;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage11);
        this.rankImage12.Image = (Image) GFXLibrary.rank_images[55];
        this.rankImage12.Position = new Point(this.rankImage11.X + this.rankImage11.Width, 53);
        this.rankImage12.CustomTooltipID = 401;
        this.rankImage12.CustomTooltipData = 11;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage12);
        this.rankImage13.Image = (Image) GFXLibrary.rank_images[56];
        this.rankImage13.Position = new Point(this.rankImage12.X + this.rankImage12.Width, 53);
        this.rankImage13.CustomTooltipID = 401;
        this.rankImage13.CustomTooltipData = 12;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage13);
        this.rankImage14.Image = (Image) GFXLibrary.rank_images[57];
        this.rankImage14.Position = new Point(this.rankImage13.X + this.rankImage13.Width, 53);
        this.rankImage14.CustomTooltipID = 401;
        this.rankImage14.CustomTooltipData = 13;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage14);
        this.rankImage15.Image = (Image) GFXLibrary.rank_images[58];
        this.rankImage15.Position = new Point(this.rankImage14.X + this.rankImage14.Width, 53);
        this.rankImage15.CustomTooltipID = 401;
        this.rankImage15.CustomTooltipData = 14;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage15);
        this.rankImage16.Image = (Image) GFXLibrary.rank_images[59];
        this.rankImage16.Position = new Point(this.rankImage15.X + this.rankImage15.Width, 53);
        this.rankImage16.CustomTooltipID = 401;
        this.rankImage16.CustomTooltipData = 15;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage16);
        this.rankImage17.Image = (Image) GFXLibrary.rank_images[60];
        this.rankImage17.Position = new Point(this.rankImage16.X + this.rankImage16.Width, 53);
        this.rankImage17.CustomTooltipID = 401;
        this.rankImage17.CustomTooltipData = 16;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage17);
        this.rankImage18.Image = (Image) GFXLibrary.rank_images[61];
        this.rankImage18.Position = new Point(this.rankImage17.X + this.rankImage17.Width, 53);
        this.rankImage18.CustomTooltipID = 401;
        this.rankImage18.CustomTooltipData = 17;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage18);
        this.rankImage19.Image = (Image) GFXLibrary.rank_images[62];
        this.rankImage19.Position = new Point(this.rankImage18.X + this.rankImage18.Width, 53);
        this.rankImage19.CustomTooltipID = 401;
        this.rankImage19.CustomTooltipData = 18;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage19);
        this.rankImage20.Image = (Image) GFXLibrary.rank_images[63];
        this.rankImage20.Position = new Point(this.rankImage19.X + this.rankImage19.Width, 53);
        this.rankImage20.CustomTooltipID = 401;
        this.rankImage20.CustomTooltipData = 19;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage20);
        this.rankImage21.Image = (Image) GFXLibrary.rank_images[64];
        this.rankImage21.Position = new Point(this.rankImage20.X + this.rankImage20.Width, 53);
        this.rankImage21.CustomTooltipID = 401;
        this.rankImage21.CustomTooltipData = 20;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage21);
        this.rankImage22.Image = (Image) GFXLibrary.rank_images[65];
        this.rankImage22.Position = new Point(this.rankImage21.X + this.rankImage21.Width, 53);
        this.rankImage22.CustomTooltipID = 401;
        this.rankImage22.CustomTooltipData = 21;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankImage22);
      }
      WorldData localWorldData = GameEngine.Instance.LocalWorldData;
      int rankSubLevel = GameEngine.Instance.World.getRankSubLevel();
      int num1 = localWorldData.ranks_Levels[GameEngine.Instance.World.getRank()];
      if (num1 > 100)
        num1 = 1;
      int num2 = 925 / num1;
      for (int index = 0; index < num1; ++index)
      {
        RankingsPanel.CSDSlot control = new RankingsPanel.CSDSlot();
        control.Size = new Size(num2 - 4, 25);
        control.Position = new Point(33 + index * num2, 187);
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control);
        control.MaxValue = 999999999;
        control.CurrentValue = control.MaxValue;
        control.init(index < rankSubLevel, index >= num1 - 1);
        this.progressSlots.Add(control);
      }
      RankingsPanel.lastRank = -1;
      RankingsPanel.lastRankSubLevel = -1;
      if (GameEngine.Instance.World.getRank() >= 1 && !GameEngine.Instance.World.TutorialIsAdvancing() && GameEngine.Instance.World.getTutorialStage() == 7)
        GameEngine.Instance.World.forceTutorialToBeShown();
      List<int> userAchievements = RemoteServices.Instance.UserAchievements;
      this.medalWindow.Position = new Point(9, 213);
      this.medalWindow.init(userAchievements, true, false, 0);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.medalWindow);
      this.filledArea.FillColor = Color.FromArgb(0, 0, 0, 0);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.filledArea);
      this.selectedMedalWindow.Position = new Point(491, 313);
      this.selectedMedalWindow.init(new List<int>(), false, false, -150);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.selectedMedalWindow);
      this.selectedMedalWindow.Visible = false;
      this.medalWindow.setChildWindow(this.selectedMedalWindow);
      this.clearRankAnim();
      TimeSpan timeSpan = DateTime.Now - RankingsPanel.lastProgressDownload;
      if (RankingsPanel.progressData != null && timeSpan.TotalMinutes <= 5.0)
        return;
      RankingsPanel.lastProgressDownload = DateTime.Now;
      RemoteServices.Instance.set_AchievementProgress_UserCallBack(new RemoteServices.AchievementProgress_UserCallBack(this.achievementProgressCallback));
      RemoteServices.Instance.AchievementProgress();
    }

    private void achievementProgressCallback(AchievementProgress_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      RankingsPanel.progressData = returnData;
    }

    public static int getProgressValue(int achievement)
    {
      achievement &= 268435455;
      switch (achievement)
      {
        case 1:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_WOLVES_PROTECTOR;
          break;
        case 2:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_BANDITS_LAW_BRINGER;
          break;
        case 3:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_TROOPS_WARRIOR;
          break;
        case 4:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_WOLFLAIRS_WOLF_HUNTER;
          break;
        case 5:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_BANDITCAMPS_WEREGILD;
          break;
        case 6:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_RATSCASTLE_RATTY_LOST_AGAIN;
          break;
        case 7:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_SNAKESCASTLE_SNAKES_DOWNFALL;
          break;
        case 8:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_PIGSCASTLE_SQUEALPIGGY;
          break;
        case 9:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_WOLFSCASTLE_WOLFBANE;
          break;
        case 10:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_FLAGS_FLAG_RAIDER;
          break;
        case 11:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_RAZE_FIRESTARTER;
          break;
        case 12:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_CAPTURE_CONQUEROR;
          break;
        case 13:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_PILLAGE_VIKING;
          break;
        case 14:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_RANSACK_VANDAL;
          break;
        case 15:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_PALADINCASTLE_EVILLORD;
          break;
        case 16:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.ATTACKING_TREASURESCASTLE_TREASUREHUNTER;
          break;
        case 34:
          return -1;
        case 37:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.DEFENDING_KILLATTACKS_VANQUISHER;
          break;
        case 65:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.CASTLE_FIREBALLISTAS_BALLISTA_CRAZY;
          break;
        case 66:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.CASTLE_POUROIL_FEEL_THE_HEAT;
          break;
        case 67:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.CASTLE_STAKE_TRAPS_DEATHTRAP;
          break;
        case 100:
          return (int) GameEngine.Instance.World.getCurrentGold();
        case 101:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.VILLAGE_SENDGOODS_CHARITY;
          break;
        case 129:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.MONKS_CUREDISEASE_HEALER;
          break;
        case 130:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.MONKS_INTERDICT_PEACEBRINGER;
          break;
        case 131:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.MONKS_INFLUENCE_DIPLOMAT;
          break;
        case 161:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.SCOUTING_SCOUTRESOURCES_HORSE_MASTER;
          break;
        case 162:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.SCOUTING_UNCOVERSTASHES_LIGHTNING_SPEED;
          break;
        case 163:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.SCOUTING_PACKETSRETREIVED_MASTER_FORAGER;
          break;
        case 194:
          return -1;
        case 195:
          return GameEngine.Instance.World.getCurrentFactionDuration();
        case 225:
          return -1;
        case 226:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.RESEARCH_COMPLETED_LEARNED_SCHOLAR;
          break;
        case 257:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.MARKET_MAKEGOLD_STOCKBROKER;
          break;
        case 289:
          return -1;
        case 290:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.BANQUETING_HONOUR_BANQUET_KING;
          break;
        case 321:
          return -1;
        case 353:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.PARISH_DONATEPACKETS_TEAM_PLAYER;
          break;
        case 354:
          if (RankingsPanel.progressData != null)
            return RankingsPanel.progressData.PARISH_PLACEBUILDINGS_SKILLED_RULER;
          break;
        case 385:
          return GameEngine.Instance.World.numUserParishes();
        case 386:
          return GameEngine.Instance.World.numUserCounties();
        case 387:
          return GameEngine.Instance.World.numUserProvinces();
        case 388:
          return GameEngine.Instance.World.numUserCountries();
      }
      return -1;
    }

    public static void setRanking(int rank, int rankSubLevel)
    {
      RankingsPanel.lastRank = rank;
      RankingsPanel.lastRankSubLevel = rankSubLevel;
      RankingsPanel.newIn = true;
    }

    public void update()
    {
      this.setCurrentRankings(RankingsPanel.lastRank, RankingsPanel.lastRankSubLevel);
      this.updateRankRanim();
    }

    public void setCurrentRankings(int rank, int rankSubLevel)
    {
      bool flag = RankingsPanel.newIn;
      RankingsPanel.newIn = false;
      if (rank != RankingsPanel.lastRank || RankingsPanel.lastRankSubLevel != rankSubLevel)
        flag = true;
      if (rank < 0)
        rank = 0;
      else if (rank >= 23)
        rank = 22;
      RankingsPanel.lastRank = rank;
      RankingsPanel.lastRankSubLevel = rankSubLevel;
      NumberFormatInfo nfi = GameEngine.NFI;
      WorldData localWorldData = GameEngine.Instance.LocalWorldData;
      this.currentRankLabel.Text = Rankings.getRankingName(localWorldData, rank, rankSubLevel, false, RemoteServices.Instance.UserAvatar.male);
      double currentHonour = GameEngine.Instance.World.getCurrentHonour();
      this.upgradeButton.Text.Text = rank != 21 || rankSubLevel < 24 ? (rank >= 22 ? ((int) Rankings.calcHonourForCrownPrince(rankSubLevel)).ToString("N", (IFormatProvider) nfi) : localWorldData.ranks_HonourPerLevel[rank].ToString("N", (IFormatProvider) nfi)) : 10000000.ToString("N", (IFormatProvider) nfi);
      int rank1 = rank;
      int rankSubLevel1 = rankSubLevel + 1;
      if (rankSubLevel1 >= localWorldData.ranks_Levels[rank])
      {
        rankSubLevel1 = 0;
        ++rank1;
      }
      if (rank1 >= 23)
      {
        this.upgradeButton.Enabled = false;
      }
      else
      {
        this.nextRankLabel.Text = Rankings.getRankingName(localWorldData, rank1, rankSubLevel1, false, RemoteServices.Instance.UserAvatar.male);
        double num = (double) localWorldData.ranks_HonourPerLevel[rank];
        switch (rank)
        {
          case 21:
            if (rankSubLevel >= 24)
            {
              num = 10000000.0;
              break;
            }
            break;
          case 22:
            num = Rankings.calcHonourForCrownPrince(rankSubLevel);
            break;
        }
        if (currentHonour >= num)
          this.upgradeButton.Enabled = true;
        else
          this.upgradeButton.Enabled = false;
      }
      int ranksLevel = localWorldData.ranks_Levels[rank];
      if (ranksLevel < 100)
      {
        for (int index = rankSubLevel; index < ranksLevel && index < this.progressSlots.Count; ++index)
        {
          int currentValue = this.progressSlots[index].CurrentValue;
          if (currentHonour < (double) localWorldData.ranks_HonourPerLevel[rank])
          {
            this.progressSlots[index].CurrentValue = (int) currentHonour;
            this.progressSlots[index].MaxValue = localWorldData.ranks_HonourPerLevel[rank];
          }
          else
            this.progressSlots[index].CurrentValue = this.progressSlots[index].MaxValue;
          currentHonour -= (double) localWorldData.ranks_HonourPerLevel[rank];
          if (currentValue != this.progressSlots[index].CurrentValue)
            this.progressSlots[index].update();
        }
      }
      else
      {
        double num = Rankings.calcHonourForCrownPrince(rankSubLevel);
        int index = 0;
        if (currentHonour < num)
        {
          this.progressSlots[index].CurrentValue = (int) currentHonour;
          this.progressSlots[index].MaxValue = (int) num;
        }
        else
        {
          this.progressSlots[index].CurrentValue = (int) num;
          this.progressSlots[index].MaxValue = (int) num;
        }
        this.progressSlots[index].update();
      }
      if (flag && rank < 22)
      {
        for (int rank2 = 0; rank2 < 22; ++rank2)
        {
          CustomSelfDrawPanel.CSDImage rankImage = this.getRankImage(rank2);
          int index = rank2;
          if (rank > rank2)
            index += 22;
          else if (rank < rank2)
            index += 44;
          rankImage.Image = (Image) GFXLibrary.rank_images[index];
        }
      }
      if (!GameEngine.Instance.World.WorldEnded)
        return;
      this.upgradeButton.Enabled = false;
    }

    private CustomSelfDrawPanel.CSDImage getRankImage(int rank)
    {
      switch (rank)
      {
        case 0:
          return this.rankImage01;
        case 1:
          return this.rankImage02;
        case 2:
          return this.rankImage03;
        case 3:
          return this.rankImage04;
        case 4:
          return this.rankImage05;
        case 5:
          return this.rankImage06;
        case 6:
          return this.rankImage07;
        case 7:
          return this.rankImage08;
        case 8:
          return this.rankImage09;
        case 9:
          return this.rankImage10;
        case 10:
          return this.rankImage11;
        case 11:
          return this.rankImage12;
        case 12:
          return this.rankImage13;
        case 13:
          return this.rankImage14;
        case 14:
          return this.rankImage15;
        case 15:
          return this.rankImage16;
        case 16:
          return this.rankImage17;
        case 17:
          return this.rankImage18;
        case 18:
          return this.rankImage19;
        case 19:
          return this.rankImage20;
        case 20:
          return this.rankImage21;
        case 21:
          return this.rankImage22;
        default:
          return (CustomSelfDrawPanel.CSDImage) null;
      }
    }

    private void upgradeClick()
    {
      if (this.inUpgrade)
        return;
      this.ignoreSetCurrent = false;
      this.inUpgrade = true;
      this.upgradeButton.Enabled = false;
      int rankSubLevel = GameEngine.Instance.World.getRankSubLevel();
      int rank = GameEngine.Instance.World.getRank();
      RemoteServices.Instance.set_UpgradeRank_UserCallBack(new RemoteServices.UpgradeRank_UserCallBack(this.upgradeRankCallBack));
      RemoteServices.Instance.UpgradeRank(rank, rankSubLevel);
      if (rankSubLevel + 1 >= GameEngine.Instance.LocalWorldData.ranks_Levels[rank])
      {
        Sound.playVillageEnvironmental(20 + rank, false, true);
        this.ignoreSetCurrent = true;
        this.startRankAnim(rank + 1);
      }
      else
        GameEngine.Instance.playInterfaceSound("RankingsPanel_subrank_up");
    }

    private void upgradeRankCallBack(UpgradeRank_ReturnType returnData)
    {
      this.inUpgrade = false;
      if (returnData.Success)
      {
        GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
        InterfaceMgr.Instance.setHonour(returnData.currentHonourLevel, returnData.rank);
        GameEngine.Instance.World.setRanking(returnData.rank, returnData.rankSubLevel);
        if (!this.ignoreSetCurrent)
        {
          this.init(false);
          this.Invalidate();
          this.setCurrentRankings(returnData.rank, returnData.rankSubLevel);
        }
        GameEngine.Instance.World.setResearchData(returnData.researchData);
        InterfaceMgr.Instance.researchDataChanged(returnData.researchData);
        GameEngine.Instance.World.setPoints(returnData.currentPoints);
        if (returnData.rank == 1 && GameEngine.Instance.World.getTutorialStage() == 7)
          GameEngine.Instance.World.forceTutorialToBeShown();
        GameEngine.Instance.World.LastUpdatedCrowns = DateTime.MinValue;
        GameEngine.Instance.cardsManager.ResetPremiumOffers();
        GameEngine.Instance.cardsManager.RetrievePremiumOffers();
      }
      else
        this.upgradeButton.Enabled = true;
    }

    private void clearRankAnim() => this.doingRankAnim = false;

    private void startRankAnim(int rank)
    {
      if (rank > GFXLibrary.RankAnim_Images.Length)
        return;
      this.majorRankUp = rank;
      if (rank == 22)
      {
        this.cpImage.Image = (Image) GFXLibrary.rank_progression_crown_prince;
        this.cpImage.Position = new Point(8, 53);
        this.cpImage.Alpha = 0.0f;
        this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.cpImage);
      }
      this.animBack.Size = this.Size;
      this.animBack.clearControls();
      this.filledArea.addControl((CustomSelfDrawPanel.CSDControl) this.animBack);
      this.scaledImage.Scale = 1.0;
      this.scaledImage.Image = rank != GFXLibrary.RankAnim_Images.Length ? (Image) GFXLibrary.RankAnim_Images[rank] : (Image) GFXLibrary.RankAnim_Images23;
      this.scaledImage.Position = new Point(0, 0);
      this.scaledImage.Alpha = 0.0f;
      this.startPos = this.unscaledArea.Position = new Point((this.Width - this.scaledImage.Image.Width) / 2, 0);
      this.animBack.addControl((CustomSelfDrawPanel.CSDControl) this.unscaledArea);
      this.targetScale = 70.0 / 313.0;
      this.zoomCount = 0;
      switch (rank)
      {
        case 0:
          this.dx = 0.0;
          this.dy = 0.0;
          this.targetScale = 36.0 / 161.0;
          break;
        case 1:
          this.dx = -344.0;
          this.dy = 88.0;
          this.targetScale = 41.0 / 189.0;
          break;
        case 2:
          this.dx = -309.0;
          this.dy = 85.0;
          this.targetScale = 63.0 / 281.0;
          break;
        case 3:
          this.dx = -271.0;
          this.dy = 83.0;
          this.targetScale = 68.0 / 305.0;
          break;
        case 4:
          this.dx = -236.0;
          this.dy = 87.0;
          this.targetScale = 0.22;
          break;
        case 5:
          this.dx = -201.0;
          this.dy = 82.0;
          this.targetScale = 23.0 / 104.0;
          break;
        case 6:
          this.dx = -159.0;
          this.dy = 80.0;
          this.targetScale = 71.0 / 316.0;
          break;
        case 7:
          this.dx = -108.0;
          this.dy = 75.0;
          this.targetScale = 73.0 / 323.0;
          break;
        case 8:
          this.dx = -59.0;
          this.dy = 79.0;
          this.targetScale = 73.0 / 327.0;
          break;
        case 9:
          this.dx = -12.0;
          this.dy = 77.0;
          this.targetScale = 77.0 / 344.0;
          break;
        case 10:
          this.dx = 38.0;
          this.dy = 78.0;
          this.targetScale = 76.0 / 341.0;
          break;
        case 11:
          this.dx = 84.0;
          this.dy = 68.0;
          this.targetScale = 15.0 / 67.0;
          break;
        case 12:
          this.dx = 128.0;
          this.dy = 70.0;
          this.targetScale = 38.0 / 173.0;
          break;
        case 13:
          this.dx = 161.0;
          this.dy = 71.0;
          this.targetScale = 76.0 / 347.0;
          break;
        case 14:
          this.dx = 202.0;
          this.dy = 68.0;
          this.targetScale = 77.0 / 355.0;
          break;
        case 15:
          this.dx = 236.0;
          this.dy = 64.0;
          this.targetScale = 27.0 / 122.0;
          break;
        case 16:
          this.dx = 273.0;
          this.dy = 65.0;
          this.targetScale = 82.0 / 365.0;
          break;
        case 17:
          this.dx = 318.0;
          this.dy = 62.0;
          this.targetScale = 42.0 / 187.0;
          break;
        case 18:
          this.dx = 369.0;
          this.dy = 57.0;
          this.targetScale = 44.0 / 195.0;
          break;
        case 19:
          this.dx = 414.0;
          this.dy = 61.0;
          this.targetScale = 87.0 / 388.0;
          break;
        case 20:
          this.dx = 462.0;
          this.dy = 35.0;
          this.targetScale = 17.0 / 60.0;
          break;
        case 21:
          this.dx = 523.0;
          this.dy = 45.0;
          this.targetScale = 88.0 / 337.0;
          break;
        case 22:
          this.dx = 54.0;
          this.dy = 50.0;
          this.targetScale = 52.0 / 83.0;
          break;
      }
      this.numSteps = (int) ((1.0 - this.targetScale) / 0.02);
      this.dx /= (double) this.numSteps;
      this.dy /= (double) this.numSteps;
      this.unscaledArea.addControl((CustomSelfDrawPanel.CSDControl) this.scaledImage);
      this.doingRankAnim = true;
      this.fadeCount = 20;
      RankingsPanel.animating = true;
    }

    private void updateRankRanim()
    {
      if (!this.doingRankAnim)
        return;
      if (this.fadeCount > 0)
      {
        --this.fadeCount;
        this.scaledImage.Alpha += 0.05f;
        this.filledArea.FillColor = Color.FromArgb(160 - this.fadeCount * 8, 0, 0, 0);
      }
      else if (this.fadeCount < 0)
      {
        ++this.fadeCount;
        this.filledArea.FillColor = Color.FromArgb(-(this.fadeCount * 8), 0, 0, 0);
        if (this.fadeCount != 0)
          return;
        this.doingRankAnim = false;
        this.filledArea.removeControl((CustomSelfDrawPanel.CSDControl) this.animBack);
        this.filledArea.FillColor = Color.FromArgb(0, 0, 0, 0);
        this.init(false);
        this.Invalidate();
        this.setCurrentRankings(this.majorRankUp, 0);
      }
      else
      {
        ++this.zoomCount;
        this.scaledImage.Scale = 1.0 - (double) this.zoomCount * 0.02;
        this.unscaledArea.Position = new Point(this.startPos.X + (int) (this.dx * (double) this.zoomCount), this.startPos.Y + (int) (this.dy * (double) this.zoomCount));
        if (this.zoomCount >= this.numSteps)
        {
          this.setCurrentRankings(this.majorRankUp, 0);
          this.scaledImage.Scale = this.targetScale;
          this.scaledImage.Alpha = 0.0f;
          this.Invalidate();
          this.fadeCount = -20;
        }
        else
        {
          if (this.zoomCount > this.numSteps - 20 && (double) this.cpImage.Alpha < 1.0)
            this.cpImage.Alpha += 0.05f;
          this.Invalidate();
        }
      }
    }

    private class CSDSlot : CustomSelfDrawPanel.CSDControl
    {
      private int maxValue = 1;
      private int currentValue;
      public CustomSelfDrawPanel.CSDHorzExtendingPanel back = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
      public CustomSelfDrawPanel.CSDHorzExtendingPanel bar = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
      public CustomSelfDrawPanel.CSDImage divider = new CustomSelfDrawPanel.CSDImage();

      public int MaxValue
      {
        get => this.maxValue;
        set => this.maxValue = value;
      }

      public int CurrentValue
      {
        get => this.currentValue;
        set => this.currentValue = value;
      }

      public void init(bool green, bool ending)
      {
        this.back.Size = this.Size;
        this.back.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.back);
        this.back.Create((Image) GFXLibrary.honour_rank_slot_left, (Image) GFXLibrary.honour_rank_slot_middle, (Image) GFXLibrary.honour_rank_slot_right);
        this.bar.Position = new Point(2, 4);
        this.bar.Size = new Size(this.Width - 4, this.Height - 7);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.bar);
        if (green)
          this.bar.Create((Image) GFXLibrary.honour_rank_slot_green_left, (Image) GFXLibrary.honour_rank_slot_green_middle, (Image) GFXLibrary.honour_rank_slot_green_right);
        else
          this.bar.Create((Image) GFXLibrary.honour_rank_slot_yellow_left, (Image) GFXLibrary.honour_rank_slot_yellow_middle, (Image) GFXLibrary.honour_rank_slot_yellow_right);
        if (this.currentValue == 0)
          this.bar.Visible = false;
        this.divider.Image = (Image) GFXLibrary.honour_rank_slot_divider;
        this.divider.Position = new Point(this.Width - 8, 0);
        if (!ending)
          this.addControl((CustomSelfDrawPanel.CSDControl) this.divider);
        this.update();
      }

      public void update()
      {
        if (this.currentValue <= 0)
        {
          this.bar.Visible = false;
        }
        else
        {
          this.bar.Visible = true;
          this.bar.Size = new Size((int) ((Decimal) (this.Width - 4 - 30) * (Decimal) this.currentValue / (Decimal) Math.Max(1, this.maxValue)) + 30, this.Height - 7);
          this.bar.resize();
        }
        this.invalidate();
      }
    }
  }
}
