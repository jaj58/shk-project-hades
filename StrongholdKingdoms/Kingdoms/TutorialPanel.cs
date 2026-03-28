// Decompiled with JetBrains decompiler
// Type: Kingdoms.TutorialPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class TutorialPanel : CustomSelfDrawPanel
  {
    private const int EXTRA_WIDTH = 110;
    private IContainer components;
    private CustomSelfDrawPanel.CSDLabel headerLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel bodyLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel rewardLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton advanceButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton continueButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton minimizeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton collectRewardButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage background = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDFill transparentBackground = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage advisor = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage illustration = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton quitButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel stageLabel = new CustomSelfDrawPanel.CSDLabel();
    private int lastTutorialID = -1;
    private static Form m_parent = (Form) null;
    private int lastStageID;
    private int preClosingTutorialID = -6;
    private static List<int> shownPizzazz = new List<int>();

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
      this.Name = nameof (TutorialPanel);
      this.Size = new Size(600, 55);
      this.ResumeLayout(false);
    }

    public TutorialPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void setText(int tutorialID, Form parent, bool force)
    {
      this.lastTutorialID = tutorialID;
      TutorialPanel.m_parent = parent;
      this.clearControls();
      this.transparentBackground.Size = this.Size;
      this.transparentBackground.FillColor = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.transparentBackground);
      this.background.Position = new Point(0, 0);
      this.background.Image = (Image) GFXLibrary.tutorial_background;
      this.background.Size = new Size(this.background.Image.Width, this.background.Image.Height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
      this.advisor.Image = (Image) GFXLibrary.tutorial_longarm1;
      int index1 = 0;
      switch (tutorialID)
      {
        case -25:
          index1 = -1;
          break;
        case 0:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm3;
          break;
        case 2:
          index1 = 2;
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm6;
          break;
        case 3:
          index1 = 4;
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm2;
          break;
        case 5:
          index1 = 5;
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm1;
          break;
        case 6:
          index1 = 6;
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm11;
          break;
        case 7:
          index1 = 7;
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm2;
          break;
        case 8:
          index1 = 8;
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm3;
          break;
        case 10:
          index1 = 11;
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm11;
          break;
        case 11:
          index1 = 12;
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm8;
          break;
        case 12:
          index1 = 13;
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm6;
          break;
        case 100:
          index1 = 1;
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm2;
          break;
        case 101:
          index1 = 3;
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm5;
          break;
        case 102:
          index1 = 9;
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm5;
          break;
        case 103:
          index1 = 10;
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm1;
          break;
        case 104:
          index1 = 14;
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm10;
          break;
        case 105:
          index1 = 15;
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm1;
          break;
      }
      this.advisor.Position = new Point(5, this.Height - this.advisor.Image.Height - 3);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.advisor);
      try
      {
        this.illustration.Image = (Image) GFXLibrary.tutorial_illustrations[index1];
        this.illustration.Position = new Point(618, 31);
        this.illustration.ClipRect = new Rectangle(0, 0, 150, 172);
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.illustration);
      }
      catch (Exception ex)
      {
      }
      this.headerLabel.Text = tutorialID != -25 ? Tutorials.getTutorialHeaderText(tutorialID) : SK.Text("QuestRewardPopup_Reward", "Reward");
      this.headerLabel.Color = ARGBColors.Black;
      this.headerLabel.Position = new Point(0, 2);
      this.headerLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
      this.headerLabel.Size = new Size(this.background.Width - 30, 40);
      this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabel);
      this.bodyLabel.Text = Tutorials.getTutorialBodyText(tutorialID);
      this.rewardLabel.Text = "";
      this.rewardLabel.Color = ARGBColors.Black;
      this.rewardLabel.Position = new Point(120, 40);
      this.rewardLabel.Font = FontManager.GetFont("Arial", 13f, FontStyle.Bold);
      this.rewardLabel.Size = new Size(510, 138);
      this.rewardLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.rewardLabel);
      this.bodyLabel.Color = ARGBColors.Black;
      this.bodyLabel.Position = new Point(120, 32);
      this.bodyLabel.Font = FontManager.GetFont("Arial", 13f, FontStyle.Bold);
      this.bodyLabel.Size = new Size(510, 138);
      this.bodyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.bodyLabel);
      Size textSize = this.bodyLabel.TextSize;
      if (textSize.Height > 120)
      {
        this.bodyLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
        textSize = this.bodyLabel.TextSize;
        if (textSize.Height > 120)
          this.bodyLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      }
      int num = this.lastStageID;
      for (int index2 = 0; index2 < Tutorials.tutorialOrdering.Length; ++index2)
      {
        if (Tutorials.tutorialOrdering[index2] == tutorialID)
        {
          num = index2;
          break;
        }
      }
      this.lastStageID = num;
      this.stageLabel.Text = (num + 1).ToString() + "/15";
      this.stageLabel.Color = ARGBColors.Black;
      this.stageLabel.Position = new Point(372, 7);
      this.stageLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.stageLabel.Size = new Size(318, 58);
      this.stageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.stageLabel);
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(this.background.Size.Width - 40, 0);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeTutorial), "TutorialPanel_close");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      this.minimizeButton.ImageNorm = (Image) GFXLibrary.minimize_Normal;
      this.minimizeButton.ImageOver = (Image) GFXLibrary.minimize_Over;
      this.minimizeButton.Position = new Point(this.background.Size.Width - 40 - 40, 0);
      this.minimizeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(TutorialPanel.minimizeTutorial), "TutorialPanel_minimize");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.minimizeButton);
      bool autoAdvance = false;
      QuestsPanel2.Instance.downloadedQuestInfo = false;
      if (tutorialID != -100)
      {
        if (!this.hasCollectableReward())
        {
          this.advanceButton.ImageNorm = (Image) GFXLibrary.tutorial_button_normal;
          this.advanceButton.ImageOver = (Image) GFXLibrary.tutorial_button_over;
          this.advanceButton.Position = new Point(280, 169);
          this.advanceButton.Text.Text = SK.Text("QuestRewardPopup_Next", "Next");
          this.advanceButton.TextYOffset = -3;
          this.advanceButton.Text.Color = ARGBColors.White;
          this.advanceButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
          this.advanceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.advanceTutorial), "TutorialPanel_advance");
          this.advanceButton.Visible = true;
          this.advanceButton.Enabled = this.isNextButtonAvailable(ref autoAdvance);
          this.background.addControl((CustomSelfDrawPanel.CSDControl) this.advanceButton);
          this.collectRewardButton.Visible = false;
        }
        else
        {
          this.advanceButton.ImageNorm = (Image) GFXLibrary.tutorial_button_normal;
          this.advanceButton.ImageOver = (Image) GFXLibrary.tutorial_button_over;
          this.advanceButton.Position = new Point(380, 169);
          this.advanceButton.Text.Text = SK.Text("QuestRewardPopup_Next", "Next");
          this.advanceButton.TextYOffset = -3;
          this.advanceButton.Text.Color = ARGBColors.White;
          this.advanceButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
          this.advanceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.advanceTutorial), "TutorialPanel_advance");
          this.advanceButton.Visible = false;
          this.advanceButton.Enabled = false;
          this.background.addControl((CustomSelfDrawPanel.CSDControl) this.advanceButton);
          this.collectRewardButton.ImageNorm = (Image) GFXLibrary.tutorial_button_normal;
          this.collectRewardButton.ImageOver = (Image) GFXLibrary.tutorial_button_over;
          this.collectRewardButton.Position = new Point(280, 169);
          this.collectRewardButton.Text.Text = SK.Text("QuestRewardPopup_Collect_Reward", "Collect Reward");
          this.collectRewardButton.TextYOffset = -3;
          this.collectRewardButton.Text.Color = ARGBColors.White;
          this.collectRewardButton.Text.Font = !(Program.mySettings.LanguageIdent == "fr") ? FontManager.GetFont("Arial", 12f, FontStyle.Bold) : FontManager.GetFont("Arial", 11f, FontStyle.Bold);
          this.collectRewardButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.collectReward), "TutorialPanel_collect_reward");
          this.collectRewardButton.Visible = true;
          if (tutorialID == -25)
            this.collectRewardButton.Enabled = true;
          else
            this.collectRewardButton.Enabled = false;
          this.background.addControl((CustomSelfDrawPanel.CSDControl) this.collectRewardButton);
        }
      }
      else
      {
        this.cancelButton.ImageNorm = (Image) GFXLibrary.tutorial_button_normal;
        this.cancelButton.ImageOver = (Image) GFXLibrary.tutorial_button_over;
        this.cancelButton.Position = new Point(180, 169);
        this.cancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
        this.cancelButton.TextYOffset = -3;
        this.cancelButton.Text.Color = ARGBColors.White;
        this.cancelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
        this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelTutorialQuit), "TutorialPanel_cancel");
        this.cancelButton.Visible = true;
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.cancelButton);
        this.quitButton.ImageNorm = (Image) GFXLibrary.tutorial_button_normal;
        this.quitButton.ImageOver = (Image) GFXLibrary.tutorial_button_over;
        this.quitButton.Position = new Point(380, 169);
        this.quitButton.Text.Text = SK.Text("QuestRewardPopup_Exit_Tutorial", "Exit Tutorial");
        this.quitButton.TextYOffset = -3;
        this.quitButton.Text.Color = ARGBColors.White;
        this.quitButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
        this.quitButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.quitTutorial), "TutorialPanel_quit");
        this.quitButton.Visible = true;
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.quitButton);
      }
      if (tutorialID == 104)
      {
        this.advanceButton.Text.Text = SK.Text("QuestRewardPopup_Complete_The_Tutorial", "Complete the Tutorial");
        if (Program.mySettings.LanguageIdent.ToLower() == "de")
          this.advanceButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      }
      this.Invalidate();
      parent?.Invalidate();
      if (autoAdvance && !GameEngine.Instance.World.TutorialIsAdvancing())
        this.advanceTutorial();
      else
        this.update();
    }

    public void advanceTutorial()
    {
      GameEngine.Instance.World.advanceTutorial();
      if (GameEngine.Instance.World.getTutorialStage() != 104)
        return;
      int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
      Point villageLocation = GameEngine.Instance.World.getVillageLocation(selectedMenuVillage);
      InterfaceMgr.Instance.changeTab(0);
      GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
      InterfaceMgr.Instance.selectTutorialArmy();
      this.collectRewardButton.Visible = false;
      this.advanceButton.Visible = false;
    }

    public static void minimizeTutorial()
    {
      if (InterfaceMgr.Instance.getTutorialArrowWindow() != null && TutorialArrowWindow.lastParent == TutorialPanel.m_parent)
        InterfaceMgr.Instance.closeTutorialArrowWindow();
      InterfaceMgr.Instance.closeTutorialWindow();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }

    public void cancelTutorialQuit()
    {
      this.setText(this.preClosingTutorialID, TutorialPanel.m_parent, true);
    }

    public void quitTutorial()
    {
      GameEngine.Instance.World.endTutorial();
      InterfaceMgr.Instance.closeTutorialWindow();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
      InterfaceMgr.Instance.closeTutorialArrowWindow();
    }

    public void closeTutorial()
    {
      this.preClosingTutorialID = this.lastTutorialID;
      this.setText(-100, TutorialPanel.m_parent, true);
    }

    public void collectReward()
    {
      PizzazzPopupWindow.closePizzazz();
      this.collectRewardButton.Visible = false;
      int tutorialQuest = Tutorials.getTutorialQuest(GameEngine.Instance.World.getTutorialStage());
      QuestsPanel2.Instance.completeQuest(tutorialQuest);
    }

    public void update()
    {
      bool autoAdvance = false;
      if (this.isNextButtonAvailable(ref autoAdvance))
      {
        if (this.hasCollectableReward())
        {
          if (!this.advanceButton.Enabled && !this.collectRewardButton.Enabled)
          {
            if (!QuestsPanel2.Instance.downloadedQuestInfo)
            {
              if ((GameEngine.Instance.World.getTutorialStage() != 7 || GameEngine.Instance.World.getRank() > 0 && !RankingsPanel.animating) && !QuestsPanel2.Instance.downloadingQuestInfo)
                QuestsPanel2.Instance.downloadQuestInfo();
            }
            else
            {
              int tutorialQuest = Tutorials.getTutorialQuest(GameEngine.Instance.World.getTutorialStage());
              this.collectRewardButton.Enabled = QuestsPanel2.Instance.isRewardAvailable(tutorialQuest);
              if (!this.collectRewardButton.Enabled && !GameEngine.Instance.World.TutorialIsAdvancing())
                this.advanceTutorial();
              else if (this.collectRewardButton.Enabled)
                this.replaceWithRewardText();
            }
          }
          else
          {
            bool enabled = this.collectRewardButton.Enabled;
            int tutorialQuest = Tutorials.getTutorialQuest(GameEngine.Instance.World.getTutorialStage());
            this.collectRewardButton.Enabled = QuestsPanel2.Instance.isRewardAvailable(tutorialQuest);
            if (!this.collectRewardButton.Enabled && !GameEngine.Instance.World.TutorialIsAdvancing())
              this.advanceTutorial();
            else if (this.collectRewardButton.Enabled && !enabled)
              this.replaceWithRewardText();
          }
        }
        else
        {
          this.advanceButton.Enabled = true;
          if (autoAdvance && !GameEngine.Instance.World.TutorialIsAdvancing())
            this.advanceTutorial();
        }
        if (this.advanceButton.Enabled)
        {
          if (InterfaceMgr.Instance.ParentForm.WindowState != FormWindowState.Maximized)
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(this.advanceButton.Position.X + this.advanceButton.Width / 2 + this.background.Position.X, this.advanceButton.Position.Y + this.advanceButton.Height + this.background.Position.Y - 5), AnchorStyles.Top | AnchorStyles.Left, TutorialPanel.m_parent);
          else
            TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point(this.advanceButton.Position.X + this.background.Position.X - 5, this.advanceButton.Position.Y + this.advanceButton.Height / 2 + this.background.Position.Y - 1), AnchorStyles.Top | AnchorStyles.Left, TutorialPanel.m_parent);
        }
        else if (this.collectRewardButton.Enabled)
        {
          if (InterfaceMgr.Instance.ParentForm.WindowState != FormWindowState.Maximized)
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(this.collectRewardButton.Position.X + this.collectRewardButton.Width / 2 + this.background.Position.X, this.collectRewardButton.Position.Y + this.collectRewardButton.Height + this.background.Position.Y - 5), AnchorStyles.Top | AnchorStyles.Left, TutorialPanel.m_parent);
          else
            TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point(this.collectRewardButton.Position.X + this.background.Position.X - 5, this.collectRewardButton.Position.Y + this.collectRewardButton.Height / 2 + this.background.Position.Y - 1), AnchorStyles.Top | AnchorStyles.Left, TutorialPanel.m_parent);
        }
        else
          InterfaceMgr.Instance.closeTutorialArrowWindow();
      }
      else
      {
        this.advanceButton.Enabled = false;
        this.collectRewardButton.Enabled = false;
        this.updateTutorialArrow();
      }
    }

    public void closing()
    {
    }

    public bool isNextButtonAvailable(ref bool autoAdvance)
    {
      autoAdvance = false;
      switch (GameEngine.Instance.World.getTutorialStage())
      {
        case 1:
          return true;
        case 2:
          if (GameEngine.Instance.Village == null)
            return false;
          bool flag = GameEngine.Instance.Village.allowTutorialAdvance();
          if (flag)
            autoAdvance = true;
          return flag;
        case 3:
          return GameEngine.Instance.Village != null && GameEngine.Instance.Village.allowTutorialAdvance();
        case 5:
          return GameEngine.Instance.World.UserResearchData.Research_Arts > (byte) 1;
        case 6:
          if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE && GameEngine.Instance.Village != null && InterfaceMgr.Instance.isVillageHonourTabOpen())
            return true;
          int tutorialQuest1 = Tutorials.getTutorialQuest(GameEngine.Instance.World.getTutorialStage());
          return QuestsPanel2.Instance.isQuestComplete(tutorialQuest1);
        case 7:
          return GameEngine.Instance.World.getRank() > 0;
        case 8:
          return GameEngine.Instance.World.isQuestObjectiveComplete(7);
        case 10:
          if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE && GameEngine.Instance.Castle != null)
            return true;
          int tutorialQuest2 = Tutorials.getTutorialQuest(GameEngine.Instance.World.getTutorialStage());
          return QuestsPanel2.Instance.isQuestComplete(tutorialQuest2);
        case 11:
          return GameEngine.Instance.Castle != null && GameEngine.Instance.Castle.isTutorialEnclosedComplete();
        case 12:
          return GameEngine.Instance.World.isQuestObjectiveComplete(11);
        case 100:
          if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_VILLAGE)
            return false;
          autoAdvance = true;
          return true;
        case 101:
          return GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_QUESTS && GameEngine.Instance.World.isQuestComplete(1);
        case 102:
          return GameEngine.Instance.World.isQuestObjectiveComplete(13);
        case 103:
          return GameEngine.Instance.World.isQuestObjectiveComplete(14);
        case 104:
          return true;
        case 105:
          return true;
        case 110:
          return true;
        default:
          return false;
      }
    }

    public bool hasCollectableReward()
    {
      switch (GameEngine.Instance.World.getTutorialStage())
      {
        case 2:
        case 3:
        case 5:
        case 6:
        case 7:
        case 8:
        case 10:
        case 11:
        case 12:
        case 102:
        case 103:
          return true;
        default:
          return false;
      }
    }

    public void invisiUpdate()
    {
      switch (GameEngine.Instance.World.getTutorialStage())
      {
        case 2:
        case 3:
        case 11:
          this.updateTutorialArrow();
          break;
      }
    }

    public void updateTutorialArrow()
    {
      switch (GameEngine.Instance.World.getTutorialStage())
      {
        case -1:
        case 104:
          InterfaceMgr.Instance.closeTutorialArrowWindow();
          break;
        case 1:
        case 100:
          if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_VILLAGE)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(382, 85), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          InterfaceMgr.Instance.closeTutorialArrowWindow();
          break;
        case 2:
          if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_VILLAGE && GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_CASTLE || InterfaceMgr.Instance.isSelectedVillageACapital())
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(382, 85), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          if (InterfaceMgr.Instance.getVillageTabBar().getCurrentTab() != 0)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(381, 121), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          if (!InterfaceMgr.Instance.isVillageMapPanelOnFoodTab())
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(94, 226 + InterfaceMgr.Instance.getVillageMapPanelBuildTabPos() - 55), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          bool flag1 = true;
          if (GameEngine.Instance.Village != null && GameEngine.Instance.Village.findBuildingTypeIncludingConstructing(13) != null)
            flag1 = false;
          if (flag1)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(57, 301), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          InterfaceMgr.Instance.closeTutorialArrowWindow();
          break;
        case 3:
          if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_VILLAGE && GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_CASTLE || InterfaceMgr.Instance.isSelectedVillageACapital())
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(382, 85), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          if (InterfaceMgr.Instance.getVillageTabBar().getCurrentTab() != 0)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(381, 121), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          if (!InterfaceMgr.Instance.isVillageMapPanelOnIndustryTab())
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(131, 226 + InterfaceMgr.Instance.getVillageMapPanelBuildTabPos() - 55), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          bool flag2 = true;
          bool flag3 = false;
          if (GameEngine.Instance.Village != null && GameEngine.Instance.Village.findBuildingTypeIncludingConstructing(6) != null)
          {
            flag2 = false;
            if (GameEngine.Instance.Village.findBuildingType(6) != null && GameEngine.Instance.Village.findBuildingTypeIncludingConstructing(7) == null)
              flag3 = true;
          }
          if (flag2)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(57, 301), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          if (flag3)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(141, 397), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          InterfaceMgr.Instance.closeTutorialArrowWindow();
          break;
        case 5:
          if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_RESEARCH)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(278, 85), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          if (ResearchPanel.TUTORIAL_artsTabPos < -9999)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(605, 334), AnchorStyles.Top | AnchorStyles.Left, InterfaceMgr.Instance.ParentForm);
            break;
          }
          if (GameEngine.Instance.World.UserResearchData.researchingType < 0)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point(42, 370 + ResearchPanel.TUTORIAL_artsTabPos), AnchorStyles.Top | AnchorStyles.Left, InterfaceMgr.Instance.ParentForm);
            break;
          }
          InterfaceMgr.Instance.closeTutorialArrowWindow();
          break;
        case 6:
          if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_VILLAGE || InterfaceMgr.Instance.isSelectedVillageACapital())
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(382, 85), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          if (!InterfaceMgr.Instance.isVillageHonourTabOpen())
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point(195, 151 + InterfaceMgr.Instance.getVillageMapPanelHonourTabPos()), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          InterfaceMgr.Instance.closeTutorialArrowWindow();
          break;
        case 7:
          if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_RANKINGS)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(228, 85), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(872, 137), AnchorStyles.None, InterfaceMgr.Instance.ParentForm);
          break;
        case 8:
          if (!InterfaceMgr.Instance.isCardPopupOpen())
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(InterfaceMgr.Instance.getTopLeftMenu().getCardAreaXPos() + 87, 80), AnchorStyles.Top | AnchorStyles.Left, InterfaceMgr.Instance.ParentForm);
            break;
          }
          if (InterfaceMgr.Instance.isConfirmPlayCardPopup())
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(300, 340), AnchorStyles.Top | AnchorStyles.Left, (Form) InterfaceMgr.Instance.getConfirmPlayCardPopup());
            break;
          }
          PlayCardsWindow cardWindow1 = (PlayCardsWindow) InterfaceMgr.Instance.getCardWindow();
          if (cardWindow1 != null)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(105, 293), AnchorStyles.Top | AnchorStyles.Left, (Form) cardWindow1);
            break;
          }
          InterfaceMgr.Instance.closeTutorialArrowWindow();
          break;
        case 10:
          if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_VILLAGE && GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_CASTLE || InterfaceMgr.Instance.isSelectedVillageACapital())
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(382, 85), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)
            break;
          TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(331, 121), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
          break;
        case 11:
          if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_VILLAGE && GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_CASTLE || InterfaceMgr.Instance.isSelectedVillageACapital())
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(382, 85), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_CASTLE)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(331, 121), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          if (!GameEngine.Instance.Castle.InBuilderMode)
          {
            if (!InterfaceMgr.Instance.TUTORIAL_openedWoodTab())
            {
              GameEngine.Instance.Castle.tutorialAutoPlace();
              TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(120, 174), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
              break;
            }
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(138, 264), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(140, 685), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
          break;
        case 12:
          if (!InterfaceMgr.Instance.isCardPopupOpen())
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(InterfaceMgr.Instance.getTopLeftMenu().getCardAreaXPos() + 87, 80), AnchorStyles.Top | AnchorStyles.Left, InterfaceMgr.Instance.ParentForm);
            break;
          }
          if (InterfaceMgr.Instance.isConfirmPlayCardPopup())
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(300, 340), AnchorStyles.Top | AnchorStyles.Left, (Form) InterfaceMgr.Instance.getConfirmPlayCardPopup());
            break;
          }
          PlayCardsWindow cardWindow2 = (PlayCardsWindow) InterfaceMgr.Instance.getCardWindow();
          GameEngine.Instance.cardsManager.countPlayableCardsInCardSection(0);
          if (cardWindow2 != null)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(105, 293), AnchorStyles.Top | AnchorStyles.Left, (Form) cardWindow2);
            break;
          }
          InterfaceMgr.Instance.closeTutorialArrowWindow();
          break;
        case 101:
          if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_QUESTS)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(177, 85), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(QuestsPanel2.questXPos + 649 + 230 - 12, 208), AnchorStyles.Top | AnchorStyles.Left, InterfaceMgr.Instance.ParentForm);
          break;
        case 102:
          if (!InterfaceMgr.Instance.isCardPopupOpen())
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(InterfaceMgr.Instance.getTopLeftMenu().getCardAreaXPos() + 87, 80), AnchorStyles.Top | AnchorStyles.Left, InterfaceMgr.Instance.ParentForm);
            break;
          }
          PlayCardsWindow cardWindow3 = (PlayCardsWindow) InterfaceMgr.Instance.getCardWindow();
          if (cardWindow3 == null)
            break;
          if (!cardWindow3.isCardWindowOnManage())
          {
            if (GameEngine.Instance.World.isBigpointAccount || Program.bigpointInstall || Program.aeriaInstall || Program.bigpointPartnerInstall)
            {
              TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point(813, 151), AnchorStyles.Top | AnchorStyles.Left, (Form) cardWindow3);
              break;
            }
            TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point(813, 206), AnchorStyles.Top | AnchorStyles.Left, (Form) cardWindow3);
            break;
          }
          if (cardWindow3.CardPanelManage.PanelMode == ManageCardsPanel.PANEL_MODE_CASH)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(740, 70), AnchorStyles.Top | AnchorStyles.Left, (Form) cardWindow3);
            break;
          }
          if (!cardWindow3.CardPanelManage.TUTORIAL_cardsInCart())
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(92, 459), AnchorStyles.Top | AnchorStyles.Left, (Form) cardWindow3);
            break;
          }
          TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(627, 170), AnchorStyles.Top | AnchorStyles.Left, (Form) cardWindow3);
          break;
        case 103:
          if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_VILLAGE)
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(382, 85), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          if (!InterfaceMgr.Instance.isVillageMapPanelOnPopularityBar())
          {
            TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point(200, 150), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
            break;
          }
          TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point(64, 203), AnchorStyles.Top | AnchorStyles.Right, InterfaceMgr.Instance.ParentForm);
          break;
        case 105:
          InterfaceMgr.Instance.closeTutorialArrowWindow();
          break;
      }
    }

    public static void logout()
    {
      TutorialPanel.shownPizzazz.Clear();
      InterfaceMgr.Instance.closeTutorialWindow();
      InterfaceMgr.Instance.closeTutorialArrowWindow();
    }

    public void replaceWithRewardText()
    {
      this.headerLabel.Text = SK.Text("QuestRewardPopup_Reward", "Reward");
      int tutorialStage = GameEngine.Instance.World.getTutorialStage();
      List<Quests.QuestReward> questRewards = Quests.getQuestRewards(Tutorials.getTutorialQuest(tutorialStage), true, GameEngine.NFI);
      string str = "";
      bool flag = true;
      foreach (Quests.QuestReward questReward in questRewards)
      {
        if (!flag)
          str += ", ";
        else
          flag = false;
        str += questReward.explanation;
      }
      this.rewardLabel.Text = str;
      this.bodyLabel.Text = Environment.NewLine + Tutorials.getTutorialRewardText(tutorialStage);
      this.bodyLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      if (this.bodyLabel.TextSize.Height > 120)
        this.bodyLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.illustration.Image = (Image) null;
      switch (tutorialStage)
      {
        case 0:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm3;
          break;
        case 2:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm5;
          break;
        case 3:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm3;
          break;
        case 5:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm12;
          break;
        case 6:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm12;
          break;
        case 7:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm4;
          break;
        case 8:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm12;
          break;
        case 10:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm12;
          break;
        case 11:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm1;
          break;
        case 12:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm7;
          break;
        case 100:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm2;
          break;
        case 102:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm12;
          break;
        case 103:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm7;
          break;
        case 104:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm7;
          break;
        case 105:
          this.advisor.Image = (Image) GFXLibrary.tutorial_longarm1;
          break;
      }
      if (TutorialPanel.shownPizzazz.Contains(tutorialStage))
        return;
      PizzazzPopupWindow.showPizzazzTutorial(tutorialStage);
      TutorialPanel.shownPizzazz.Add(tutorialStage);
    }
  }
}
