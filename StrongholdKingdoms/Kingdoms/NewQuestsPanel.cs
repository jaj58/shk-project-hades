// Decompiled with JetBrains decompiler
// Type: Kingdoms.NewQuestsPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class NewQuestsPanel : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private Panel focusPanel;
    public static NewQuestsPanel Instance = (NewQuestsPanel) null;
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel insetImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDImage underlayImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage questImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage questIcon = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel lblQuestName = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblQuestDescription = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzProgressBar questProgressBar = new CustomSelfDrawPanel.CSDHorzProgressBar();
    private CustomSelfDrawPanel.CSDLabel progressTextLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDVertScrollBar questsScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea questsScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDDragPanel dragOverlay = new CustomSelfDrawPanel.CSDDragPanel();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDButton completeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage completeGlow = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage completeGlow2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton abandonButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel completedQuestsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel timeLeftLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel tutorialText = new CustomSelfDrawPanel.CSDLabel();
    private int glowValue;
    private int m_selectedQuest = -1;
    private DateTime lastFullUpdateTime = DateTime.MinValue;
    private List<NewQuestsPanel.NewQuestLine> lineList = new List<NewQuestsPanel.NewQuestLine>();
    private static DateTime m_lastQuestReportingUpdate = DateTime.MinValue;

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
      this.Size = new Size(992, 566);
      this.Name = nameof (NewQuestsPanel);
      this.ResumeLayout(false);
    }

    public NewQuestsPanel()
    {
      NewQuestsPanel.Instance = this;
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.focusPanel.Focus();
    }

    public void init(bool resized)
    {
      int height = this.Height;
      NewQuestsPanel.Instance = this;
      this.clearControls();
      this.headerImage.Size = new Size(this.Width, 40);
      this.headerImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage);
      this.headerImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.headerImage, 19, new Point(this.Width - 44, 3));
      this.backgroundImage.Size = new Size(this.Width, height - 40);
      this.backgroundImage.Position = new Point(0, 40);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
      this.backgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
      this.underlayImage.Image = (Image) GFXLibrary.quest_screen_warm;
      this.underlayImage.Position = new Point(6, 0);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.underlayImage);
      this.questImage.Image = (Image) GFXLibrary.quest_screen_top;
      this.questImage.Position = new Point(21, 18);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.questImage);
      this.parishNameLabel.Text = SK.Text("QuestPanel_Quests", "Quests");
      this.parishNameLabel.Color = ARGBColors.White;
      this.parishNameLabel.DropShadowColor = ARGBColors.Black;
      this.parishNameLabel.Position = new Point(20, 0);
      this.parishNameLabel.Size = new Size(this.Width - 40, 40);
      this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
      this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.parishNameLabel);
      this.questsScrollArea.Position = new Point(40, 230);
      this.questsScrollArea.Size = new Size(880, height - 230 - 20 - 40);
      this.questsScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(880, height - 230 - 20 - 40));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.questsScrollArea);
      this.insetImage.Position = new Point(21, 220);
      this.insetImage.Size = new Size(947, height - 230 - 20 - 40 + 20);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.insetImage);
      this.insetImage.Create((Image) GFXLibrary.quest_9sclice_grey_inset_top_left, (Image) GFXLibrary.quest_9sclice_grey_inset_top_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_top_right, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_left, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_right, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_left, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_right);
      int num1 = this.questsScrollBar.Value;
      this.questsScrollBar.Position = new Point(930, 230);
      this.questsScrollBar.Size = new Size(24, height - 230 - 20 - 40);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.questsScrollBar);
      this.questsScrollBar.Value = 0;
      this.questsScrollBar.Max = 100;
      this.questsScrollBar.NumVisibleLines = 25;
      this.questsScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.questsScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      this.mouseWheelOverlay.Position = this.questsScrollArea.Position;
      this.mouseWheelOverlay.Size = this.questsScrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.backgroundImage.addControl(this.mouseWheelOverlay);
      NewQuestsData newQuestData = GameEngine.Instance.World.getNewQuestData();
      if (newQuestData != null && newQuestData.questID >= 0)
      {
        int curValue = 0;
        int maxValue = 1;
        int num2 = 0;
        NewQuests.NewQuestDefinition newQuestDef = NewQuests.getNewQuestDef(newQuestData.questID);
        NewQuestsPanel.addRewardIcons((CustomSelfDrawPanel.CSDControl) this.questImage, new Point(170, 75), newQuestDef, 1);
        this.questIcon.Image = (Image) GFXLibrary.quest_icons[Math.Min(newQuestDef.questType, GFXLibrary.quest_icons.Length - 1)];
        this.questIcon.Position = new Point(170, 16);
        this.questImage.addControl((CustomSelfDrawPanel.CSDControl) this.questIcon);
        this.lblQuestName.Text = SK.NoStoreText("Z_QUESTS_" + newQuestDef.tagString);
        this.lblQuestName.Color = ARGBColors.Black;
        this.lblQuestName.Position = new Point(220, 19);
        this.lblQuestName.Size = new Size(700, 30);
        this.lblQuestName.Font = FontManager.GetFont("Arial", 13f, FontStyle.Bold);
        this.lblQuestName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.questImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblQuestName);
        NumberFormatInfo nfi = GameEngine.NFI;
        this.lblQuestDescription.Text = SK.Text("QUEST_PANEL_DESCRIPTION_OBJECTIVE", "Objective");
        this.lblQuestDescription.Text += " - ";
        switch (newQuestData.questID)
        {
          case 4:
          case 16:
          case 34:
          case 48:
          case 64:
          case 84:
          case 101:
          case 122:
            this.lblQuestDescription.Text += SK.Text("QUESTS_Spread_New_description", "Learn about Invite a Friend");
            break;
          default:
            if (newQuestDef.parameter > 0)
            {
              CustomSelfDrawPanel.CSDLabel questDescription = this.lblQuestDescription;
              questDescription.Text = questDescription.Text + SK.NoStoreText("Z_QUEST_DESCRIPTIONS_" + newQuestDef.tagString) + " : " + newQuestDef.parameter.ToString("N", (IFormatProvider) nfi);
              break;
            }
            this.lblQuestDescription.Text += SK.NoStoreText("Z_QUEST_DESCRIPTIONS_" + newQuestDef.tagString);
            break;
        }
        this.lblQuestDescription.Color = ARGBColors.Black;
        this.lblQuestDescription.RolloverColor = ARGBColors.White;
        this.lblQuestDescription.Position = new Point(220, 42);
        this.lblQuestDescription.Size = new Size(740, 50);
        this.lblQuestDescription.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
        this.lblQuestDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.lblQuestDescription.Data = newQuestData.questID;
        this.lblQuestDescription.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.OpenFurtherInformation));
        this.lblQuestDescription.Tag = (object) newQuestDef.tagString;
        this.questImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblQuestDescription);
        if (newQuestData.completionState >= 0 && QuestsHelper.isQuestComplete(newQuestData) && !GameEngine.Instance.World.WorldEnded)
        {
          this.completeGlow2.Image = (Image) GFXLibrary.quest_button_glow;
          this.completeGlow2.Position = new Point(632, 132);
          this.completeGlow2.Alpha = 1f;
          this.questImage.addControl((CustomSelfDrawPanel.CSDControl) this.completeGlow2);
          this.completeGlow.Image = (Image) GFXLibrary.quest_button_glow;
          this.completeGlow.Position = new Point(632, 132);
          this.completeGlow.Alpha = 1f;
          this.questImage.addControl((CustomSelfDrawPanel.CSDControl) this.completeGlow);
          this.glowValue = 0;
          this.abandonButton.Enabled = false;
          this.completeButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
          this.completeButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.completeButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
          this.completeButton.Position = new Point(648, 149);
          this.completeButton.Text.Text = SK.Text("QUESTS_Complete", "Complete");
          this.completeButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.completeButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
          this.completeButton.TextYOffset = -3;
          this.completeButton.Text.Color = ARGBColors.Black;
          this.completeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.completeQuest), "NewQuests_Complete_Clicked");
          this.completeButton.Enabled = true;
          this.questImage.addControl((CustomSelfDrawPanel.CSDControl) this.completeButton);
          switch (newQuestData.questID)
          {
            case 5:
            case 20:
            case 42:
            case 67:
            case 99:
            case 125:
            case 140:
            case 148:
            case 157:
            case 167:
              curValue = maxValue = newQuestDef.parameter;
              break;
            case 29:
              curValue = maxValue = 4;
              break;
            case 66:
              curValue = maxValue = 6;
              break;
            case 146:
              curValue = maxValue = 8;
              break;
            default:
              curValue = maxValue = newQuestDef.parameter;
              break;
          }
          if (maxValue == 0)
            maxValue = curValue = 1;
          num2 = curValue;
        }
        else
        {
          this.abandonButton.Enabled = true;
          if (GameEngine.Instance.World.WorldEnded)
            this.abandonButton.Enabled = false;
          if (newQuestDef.parameter > 0 || newQuestData.questID == 66 || newQuestData.questID == 146 || newQuestData.questID == 29)
          {
            switch (newQuestData.questID)
            {
              case 4:
              case 16:
              case 34:
              case 48:
              case 64:
              case 84:
              case 101:
              case 122:
                curValue = newQuestData.data;
                maxValue = newQuestDef.parameter;
                num2 = curValue;
                break;
              case 5:
              case 20:
              case 42:
              case 67:
              case 99:
              case 125:
              case 140:
              case 148:
              case 157:
              case 167:
                double num3 = GameEngine.Instance.World.getCurrentGold() - (double) newQuestData.startingData;
                num2 = (int) num3;
                if (num3 < 0.0)
                  num3 = 0.0;
                curValue = (int) num3;
                maxValue = newQuestDef.parameter;
                break;
              case 29:
                curValue = QuestsHelper.bitCount(newQuestData.data);
                maxValue = 4;
                num2 = curValue;
                break;
              case 66:
                curValue = QuestsHelper.bitCount(newQuestData.data);
                maxValue = 6;
                num2 = curValue;
                break;
              case 146:
                curValue = QuestsHelper.bitCount(newQuestData.data);
                maxValue = 8;
                num2 = curValue;
                break;
              default:
                curValue = newQuestData.data;
                maxValue = newQuestDef.parameter;
                num2 = curValue;
                break;
            }
          }
        }
        this.questProgressBar.Position = new Point(162, 124);
        this.questProgressBar.Size = new Size(766, 22);
        this.questProgressBar.Offset = new Point(0, 0);
        this.questImage.addControl((CustomSelfDrawPanel.CSDControl) this.questProgressBar);
        this.questProgressBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary.quest_screen_progbar_left, (Image) GFXLibrary.quest_screen_progbar_mid, (Image) GFXLibrary.quest_screen_progbar_right);
        this.questProgressBar.setValues((double) curValue, (double) maxValue);
        this.progressTextLabel.Text = num2.ToString("N", (IFormatProvider) nfi) + " / " + maxValue.ToString("N", (IFormatProvider) nfi);
        this.progressTextLabel.Color = ARGBColors.White;
        this.progressTextLabel.Position = new Point(0, -1);
        this.progressTextLabel.Size = new Size(this.questProgressBar.Width, this.questProgressBar.Height);
        this.progressTextLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.progressTextLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.questProgressBar.addControl((CustomSelfDrawPanel.CSDControl) this.progressTextLabel);
        this.abandonButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.abandonButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.abandonButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.abandonButton.Position = new Point(798, 149);
        this.abandonButton.Text.Text = SK.Text("QUESTS_Abandon", "Abandon");
        this.abandonButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.abandonButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.abandonButton.TextYOffset = -3;
        this.abandonButton.Text.Color = ARGBColors.Black;
        this.abandonButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.abandonQuest), "NewQuests_Abandon_Started_Quest_Clicked");
        this.abandonButton.Enabled = true;
        this.questImage.addControl((CustomSelfDrawPanel.CSDControl) this.abandonButton);
        switch (newQuestData.questID)
        {
          case 4:
          case 16:
          case 34:
          case 48:
          case 64:
          case 84:
          case 101:
          case 122:
            if (!GameEngine.Instance.World.isBigpointAccount && !Program.aeriaInstall && !Program.bigpointPartnerInstall && !Program.arcInstall)
            {
              CustomSelfDrawPanel.CSDButton control = new CustomSelfDrawPanel.CSDButton();
              control.ImageNorm = (Image) GFXLibrary.banner_ad_friend_quest;
              control.OverBrighten = true;
              control.Position = new Point(152, 5);
              control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.friendClicked), "LogoutPanel_invite_a_friend");
              this.questImage.addControl((CustomSelfDrawPanel.CSDControl) control);
              this.lblQuestDescription.Text = "";
              this.lblQuestName.Text = "";
              break;
            }
            this.lblQuestDescription.Text = SK.Text("QUESTS_Spread_New_description", "Learn about Invite a Friend");
            break;
        }
        if (newQuestDef.timed > 0)
        {
          if (newQuestData.completionState == 0)
          {
            int totalSeconds = (int) (new TimeSpan(newQuestDef.timed, 0, 0) - (VillageMap.getCurrentServerTime() - newQuestData.startTime)).TotalSeconds;
            this.timeLeftLabel.Text = SK.Text("QUESTS_TimeRemaining", "Time Remaining") + " : " + VillageMap.createBuildTimeStringFull(totalSeconds);
            this.timeLeftLabel.Color = ARGBColors.Black;
            this.timeLeftLabel.Position = new Point(170, 145);
            this.timeLeftLabel.Size = new Size(760, 50);
            this.timeLeftLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.timeLeftLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.timeLeftLabel.Visible = true;
            this.questImage.addControl((CustomSelfDrawPanel.CSDControl) this.timeLeftLabel);
          }
          else if (newQuestData.completionState < 0)
          {
            this.timeLeftLabel.Text = SK.Text("QUESTS_QuestFailed", "Quest Failed");
            this.timeLeftLabel.Color = ARGBColors.Black;
            this.timeLeftLabel.Position = new Point(170, 145);
            this.timeLeftLabel.Size = new Size(760, 50);
            this.timeLeftLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.timeLeftLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.timeLeftLabel.Visible = true;
            this.questImage.addControl((CustomSelfDrawPanel.CSDControl) this.timeLeftLabel);
          }
        }
      }
      else
      {
        this.lblQuestName.Text = SK.Text("QUESTS_No_Active_Quest", "No Active Quest");
        this.lblQuestName.Color = ARGBColors.Black;
        this.lblQuestName.Position = new Point(170, 19);
        this.lblQuestName.Size = new Size(700, 30);
        this.lblQuestName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
        this.lblQuestName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.questImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblQuestName);
      }
      this.completedQuestsLabel.Text = SK.Text("QUESTS_CompletedQuests", "Completed Quests") + " : " + newQuestData.totalCompleted.ToString();
      this.completedQuestsLabel.Color = ARGBColors.Black;
      this.completedQuestsLabel.RolloverColor = ARGBColors.White;
      this.completedQuestsLabel.Position = new Point(170, 165);
      this.completedQuestsLabel.Size = new Size(460, 50);
      this.completedQuestsLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.completedQuestsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.completedQuestsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showCompletedQuests));
      this.questImage.addControl((CustomSelfDrawPanel.CSDControl) this.completedQuestsLabel);
      this.tutorialText.Text = SK.Text("Quest_Tutorial_Inprogress", "The Tutorial is currently in progress. Please finish or quit the Tutorial to access Quests.");
      this.tutorialText.Color = ARGBColors.Black;
      this.tutorialText.Position = this.questsScrollArea.Position;
      this.tutorialText.Size = this.questsScrollArea.Size;
      this.tutorialText.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.tutorialText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.questImage.addControl((CustomSelfDrawPanel.CSDControl) this.tutorialText);
      if (!resized)
      {
        this.m_selectedQuest = -1;
        TimeSpan timeSpan = DateTime.Now - this.lastFullUpdateTime;
        RemoteServices.Instance.set_GetQuestData_UserCallBack(new RemoteServices.GetQuestData_UserCallBack(this.getQuestDataCallback));
        if (timeSpan.TotalMinutes > 1.0)
          RemoteServices.Instance.GetQuestData(true);
        else
          RemoteServices.Instance.GetQuestData(false);
      }
      this.rebuild(this.m_selectedQuest);
      if (!resized)
        return;
      this.questsScrollBar.Value = num1;
      this.questsScrollBar.scrollDown(0);
      this.wallScrollBarMoved();
    }

    private void getQuestDataCallback(GetQuestData_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      GameEngine.Instance.World.setNewQuestData(returnData.m_newQuestsData);
      this.init(true);
      NewQuestsPanel.handleClientSideQuestReporting(true);
    }

    protected void startQuest(int questID)
    {
      RemoteServices.Instance.set_StartNewQuest_UserCallBack(new RemoteServices.StartNewQuest_UserCallBack(this.startNewQuestCallback));
      RemoteServices.Instance.StartNewQuest(questID);
    }

    private void startNewQuestCallback(StartNewQuest_ReturnType returnData)
    {
      int num = returnData.Success ? 1 : 0;
      if (returnData.m_newQuestsData == null)
        return;
      GameEngine.Instance.World.setNewQuestData(returnData.m_newQuestsData);
      this.init(true);
    }

    public void update()
    {
      NewQuestsData newQuestData = GameEngine.Instance.World.getNewQuestData();
      if (newQuestData != null && newQuestData.questID >= 0 && newQuestData.completionState == 0)
      {
        NewQuests.NewQuestDefinition newQuestDef = NewQuests.getNewQuestDef(newQuestData.questID);
        if (newQuestDef.timed > 0)
        {
          int secsLeft = (int) (new TimeSpan(newQuestDef.timed, 0, 0) - (VillageMap.getCurrentServerTime() - newQuestData.startTime)).TotalSeconds;
          if (secsLeft < 0)
            secsLeft = 0;
          this.timeLeftLabel.TextDiffOnly = SK.Text("QUESTS_TimeRemaining", "Time Remaining") + " : " + VillageMap.createBuildTimeStringFull(secsLeft);
        }
        else
          this.timeLeftLabel.Visible = false;
      }
      else if (newQuestData.completionState < 0)
      {
        this.timeLeftLabel.TextDiffOnly = SK.Text("QUESTS_QuestFailed", "Quest Failed");
        this.timeLeftLabel.Visible = true;
      }
      else
        this.timeLeftLabel.Visible = false;
      this.glowValue += 15;
      if (this.glowValue > 511)
        this.glowValue -= 511;
      int num = this.glowValue;
      if (num > (int) byte.MaxValue)
        num = 511 - num;
      this.completeGlow.Alpha = (float) num / (float) byte.MaxValue;
      this.completeGlow.invalidate();
    }

    public void logout()
    {
    }

    private void wallScrollBarMoved()
    {
      int y = this.questsScrollBar.Value;
      this.questsScrollArea.Position = new Point(this.questsScrollArea.X, 230 - y);
      this.questsScrollArea.ClipRect = new Rectangle(this.questsScrollArea.ClipRect.X, y, this.questsScrollArea.ClipRect.Width, this.questsScrollArea.ClipRect.Height);
      this.questsScrollArea.invalidate();
      this.questsScrollBar.invalidate();
    }

    private void mouseWheelMoved(int delta)
    {
      if (delta < 0)
      {
        this.questsScrollBar.scrollDown(40);
      }
      else
      {
        if (delta <= 0)
          return;
        this.questsScrollBar.scrollUp(40);
      }
    }

    private void windowDragged()
    {
      int num = -this.dragOverlay.YDiff;
      int width = this.questsScrollArea.Size.Width;
      int height = (int) (double) this.questsScrollArea.Size.Height;
      if (this.questsScrollArea.ClipRect.Y + num < 0)
        num = -this.questsScrollArea.ClipRect.Y;
      if (this.questsScrollArea.ClipRect.Y + num > height - this.questsScrollArea.ClipRect.Height)
        num -= this.questsScrollArea.ClipRect.Y + num - (height - this.questsScrollArea.ClipRect.Height);
      this.questsScrollArea.Position = new Point(this.questsScrollArea.Position.X, this.questsScrollArea.Position.Y - num);
      this.questsScrollArea.ClipRect = new Rectangle(this.questsScrollArea.ClipRect.X, this.questsScrollArea.ClipRect.Y + num, this.questsScrollArea.ClipRect.Width, this.questsScrollArea.ClipRect.Height);
      this.questsScrollArea.invalidate();
    }

    public void expandQuest(int quest)
    {
      this.m_selectedQuest = quest;
      int num = this.questsScrollBar.Value;
      this.rebuild(quest);
      this.questsScrollBar.Value = num;
    }

    public void closing() => InterfaceMgr.Instance.closeDonatePopup();

    private void completeQuest()
    {
      NewQuestsData newQuestData = GameEngine.Instance.World.getNewQuestData();
      if (newQuestData == null)
        return;
      InterfaceMgr.Instance.openNewQuestRewardPopup(newQuestData.questID, -1, this);
    }

    public void doCompleteQuest(int questID, int villageID, bool glory)
    {
      this.completeButton.Enabled = false;
      RemoteServices.Instance.set_CompleteAbandonNewQuest_UserCallBack(new RemoteServices.CompleteAbandonNewQuest_UserCallBack(this.completeAbandonNewQuestCallback));
      RemoteServices.Instance.CompleteNewQuest(questID, glory, villageID);
    }

    private void abandonQuest()
    {
      if (GameEngine.Instance.World.getNewQuestData() == null)
        return;
      this.abandonButton.Enabled = false;
      RemoteServices.Instance.set_CompleteAbandonNewQuest_UserCallBack(new RemoteServices.CompleteAbandonNewQuest_UserCallBack(this.completeAbandonNewQuestCallback));
      RemoteServices.Instance.AbandonNewQuest(-1);
    }

    private void abandonQuest(int questID)
    {
      RemoteServices.Instance.set_CompleteAbandonNewQuest_UserCallBack(new RemoteServices.CompleteAbandonNewQuest_UserCallBack(this.completeAbandonNewQuestCallback));
      RemoteServices.Instance.AbandonNewQuest(questID);
    }

    private void restoreQuests()
    {
      RemoteServices.Instance.set_CompleteAbandonNewQuest_UserCallBack(new RemoteServices.CompleteAbandonNewQuest_UserCallBack(this.completeAbandonNewQuestCallback));
      RemoteServices.Instance.AbandonNewQuest(-2);
    }

    private void completeAbandonNewQuestCallback(CompleteAbandonNewQuest_ReturnType returnData)
    {
      this.abandonButton.Enabled = true;
      if (returnData.Success)
      {
        if (returnData.goldGiven)
          GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
        if (returnData.honourGiven)
          GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
        if (returnData.fpGiven)
          GameEngine.Instance.World.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
        if (returnData.premiumCardsGiven)
          GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens.Add(returnData.premiumCardID, new CardTypes.PremiumToken()
          {
            Reward = 1,
            UserPremiumTokenID = returnData.premiumCardID,
            WorldID = RemoteServices.Instance.ProfileWorldID,
            Type = 4113
          });
        if (returnData.cardPacksGiven > 0)
        {
          int key = 1;
          if (GameEngine.Instance.cardPackManager.ProfileUserCardPacks.ContainsKey(key))
            GameEngine.Instance.cardPackManager.ProfileUserCardPacks[key].Count += returnData.cardPacksGiven;
          else
            GameEngine.Instance.cardPackManager.ProfileUserCardPacks[key] = new CardTypes.UserCardPack()
            {
              OfferID = key,
              Count = returnData.cardPacksGiven
            };
        }
        if (returnData.gloryGiven)
          GameEngine.Instance.World.clearGloryHistory();
        if (returnData.villageUpdated >= 0)
          GameEngine.Instance.flushVillage(returnData.villageUpdated);
        if (returnData.ticketsGiven > 0)
          GameEngine.Instance.World.addTickets(-1, returnData.ticketsGiven);
      }
      else if (returnData.m_errorCode == ErrorCodes.ErrorCode.NEW_QUESTS_FAILED_REWARD)
      {
        int num = (int) MyMessageBox.Show(SK.Text("QUESTS_failed_reward_body", "We have been unable to give the correct reward for this quest, please wait a few minutes and try again. If this doesn't work, please contact support."), SK.Text("QUESTS_failed_reward", "Quest Reward Error"));
      }
      if (returnData.m_newQuestsData == null)
        return;
      GameEngine.Instance.World.setNewQuestData(returnData.m_newQuestsData);
      this.init(true);
    }

    private void showCompletedQuests()
    {
      InterfaceMgr.Instance.openNewQuestsCompletedPopup((List<int>) null);
    }

    public void rebuild(int activeQuest)
    {
      bool allowStart = true;
      NewQuestsData newQuestData = GameEngine.Instance.World.getNewQuestData();
      if (newQuestData == null)
        return;
      if (newQuestData.questID >= 0)
        allowStart = false;
      int[] numArray = newQuestData.availableQuests ?? new int[0];
      this.questsScrollArea.clearControls();
      int y = 0;
      this.lineList.Clear();
      if (!GameEngine.Instance.World.isTutorialActive())
      {
        double num = DXTimer.GetCurrentMilliseconds() / 1000.0;
        int position;
        for (position = 0; position < numArray.Length; ++position)
        {
          int quest = numArray[position];
          if (quest > 0)
          {
            NewQuestsPanel.NewQuestLine control = new NewQuestsPanel.NewQuestLine();
            if (y != 0)
              y += 5;
            control.Position = new Point(0, y);
            control.init(quest, this, position, activeQuest, allowStart, false);
            this.questsScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
            y += control.Height;
            this.lineList.Add(control);
          }
        }
        if (newQuestData.numAbandonedQuests > 0)
        {
          NewQuestsPanel.NewQuestLine control = new NewQuestsPanel.NewQuestLine();
          if (y != 0)
            y += 5;
          control.Position = new Point(0, y);
          control.init(-newQuestData.numAbandonedQuests, this, position, activeQuest, allowStart, false);
          this.questsScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
          y += control.Height;
          this.lineList.Add(control);
        }
        this.tutorialText.Visible = false;
      }
      else
        this.tutorialText.Visible = true;
      int height = y + 5;
      this.questsScrollArea.Size = new Size(this.questsScrollArea.Width, height);
      if (height < this.questsScrollBar.Height)
      {
        this.questsScrollBar.Visible = false;
      }
      else
      {
        this.questsScrollBar.Visible = true;
        this.questsScrollBar.NumVisibleLines = this.questsScrollBar.Height;
        this.questsScrollBar.Max = height - this.questsScrollBar.Height;
      }
      this.questsScrollArea.invalidate();
      this.questsScrollBar.invalidate();
      this.backgroundImage.invalidate();
      this.update();
    }

    public void OpenFurtherInformation()
    {
      int data = CustomSelfDrawPanel.StaticClickedControl.Data;
      InterfaceMgr.Instance.openNewQuestFurtherTextPopup((string) CustomSelfDrawPanel.StaticClickedControl.Tag, data);
    }

    public static void addRewardIcons(
      CustomSelfDrawPanel.CSDControl parentControl,
      Point baseLocation,
      NewQuests.NewQuestDefinition def,
      int gloryMode)
    {
      if (def == null)
        return;
      CustomSelfDrawPanel.CSDLabel control1 = (CustomSelfDrawPanel.CSDLabel) null;
      if (gloryMode > 0)
      {
        control1 = new CustomSelfDrawPanel.CSDLabel();
        control1.Color = ARGBColors.Black;
        control1.Position = baseLocation;
        control1.Size = new Size(110, GFXLibrary.quest_rewards[0].Height);
        control1.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
        control1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        parentControl.addControl((CustomSelfDrawPanel.CSDControl) control1);
        baseLocation.X += 100;
      }
      int num = 0;
      if (def.reward_charges.Length > 0 && gloryMode == 1)
      {
        parentControl.addControl((CustomSelfDrawPanel.CSDControl) NewQuestsPanel.createRewardIcon(9, -1, new Point(baseLocation.X, baseLocation.Y + 2), 3212));
        baseLocation.X += 60;
        ++num;
        if (def.getRewardGlory() > 0 && gloryMode != 0 && GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1 && gloryMode > 0)
        {
          CustomSelfDrawPanel.CSDLabel control2 = new CustomSelfDrawPanel.CSDLabel();
          control2.Text = "+";
          control2.Color = ARGBColors.Black;
          control2.Position = new Point(baseLocation.X - 18 - 2, baseLocation.Y + 12 - 2);
          control2.Size = new Size(50, 30);
          control2.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
          control2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
          parentControl.addControl((CustomSelfDrawPanel.CSDControl) control2);
          baseLocation.X += 30;
        }
      }
      if (gloryMode >= 0)
      {
        if (def.reward_honour > 0)
        {
          parentControl.addControl((CustomSelfDrawPanel.CSDControl) NewQuestsPanel.createRewardIcon(1, def.reward_honour, baseLocation, 3203));
          baseLocation.X += 120;
          ++num;
        }
        if (def.reward_gold > 0)
        {
          parentControl.addControl((CustomSelfDrawPanel.CSDControl) NewQuestsPanel.createRewardIcon(2, def.reward_gold, baseLocation, 3204));
          baseLocation.X += 120;
          ++num;
        }
        if (def.reward_wood > 0)
        {
          parentControl.addControl((CustomSelfDrawPanel.CSDControl) NewQuestsPanel.createRewardIcon(3, def.reward_wood, baseLocation, 3205));
          baseLocation.X += 120;
          ++num;
        }
        if (def.reward_stone > 0)
        {
          parentControl.addControl((CustomSelfDrawPanel.CSDControl) NewQuestsPanel.createRewardIcon(4, def.reward_stone, baseLocation, 3206));
          baseLocation.X += 120;
          ++num;
        }
        if (def.reward_apples > 0)
        {
          parentControl.addControl((CustomSelfDrawPanel.CSDControl) NewQuestsPanel.createRewardIcon(12, def.reward_apples, baseLocation, 3214));
          baseLocation.X += 120;
          ++num;
        }
        if (def.reward_card_pack > 0)
        {
          parentControl.addControl((CustomSelfDrawPanel.CSDControl) NewQuestsPanel.createRewardIcon(6, def.reward_card_pack, baseLocation, 3208));
          baseLocation.X += 120;
          ++num;
        }
        if (def.reward_2day_premium > 0)
        {
          parentControl.addControl((CustomSelfDrawPanel.CSDControl) NewQuestsPanel.createRewardIcon(7, def.reward_2day_premium, baseLocation, 3209));
          baseLocation.X += 120;
          ++num;
        }
        if (def.reward_faithpoints > 0)
        {
          parentControl.addControl((CustomSelfDrawPanel.CSDControl) NewQuestsPanel.createRewardIcon(8, def.reward_faithpoints, baseLocation, 3210));
          baseLocation.X += 120;
          ++num;
        }
        if (def.reward_tickets > 0)
        {
          parentControl.addControl((CustomSelfDrawPanel.CSDControl) NewQuestsPanel.createRewardIcon(10, def.reward_tickets, baseLocation, 3213));
          baseLocation.X += 120;
          ++num;
        }
      }
      if (def.getRewardGlory() > 0 && gloryMode != 0 && GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1)
      {
        if (gloryMode > 0)
        {
          CustomSelfDrawPanel.CSDLabel control3 = new CustomSelfDrawPanel.CSDLabel();
          control3.Text = SK.Text("QUESTS_or", "Or");
          control3.Color = ARGBColors.Black;
          control3.Position = new Point(baseLocation.X, baseLocation.Y + 12);
          control3.Size = new Size(50, 30);
          control3.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
          control3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
          parentControl.addControl((CustomSelfDrawPanel.CSDControl) control3);
          baseLocation.X += 50;
        }
        parentControl.addControl((CustomSelfDrawPanel.CSDControl) NewQuestsPanel.createRewardIcon(0, def.getRewardGlory(), baseLocation, 3211));
        baseLocation.X += 120;
        ++num;
      }
      if (control1 == null)
        return;
      if (num == 1)
        control1.Text = SK.Text("QUEST_Reward", "Reward");
      else
        control1.Text = SK.Text("QUEST_Rewards", "Rewards");
    }

    public static CustomSelfDrawPanel.CSDImage createRewardIcon(
      int iconID,
      int value,
      Point Location,
      int tooltipID)
    {
      CustomSelfDrawPanel.CSDImage rewardIcon = new CustomSelfDrawPanel.CSDImage();
      rewardIcon.Image = (Image) GFXLibrary.quest_rewards[iconID];
      rewardIcon.Position = Location;
      rewardIcon.CustomTooltipID = tooltipID;
      if (value >= 0)
      {
        CustomSelfDrawPanel.CSDLabel control = new CustomSelfDrawPanel.CSDLabel();
        control.Text = value.ToString();
        control.Color = ARGBColors.Black;
        control.Position = new Point(47, 0);
        control.Size = new Size(80, rewardIcon.Image.Height);
        control.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
        control.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        control.CustomTooltipID = tooltipID;
        rewardIcon.addControl((CustomSelfDrawPanel.CSDControl) control);
      }
      return rewardIcon;
    }

    public static void handleClientSideQuestReporting(bool timeRestricted)
    {
      NewQuestsData newQuestData = GameEngine.Instance.World.getNewQuestData();
      if (newQuestData == null || newQuestData.questID < 0 || newQuestData.completionState != 0)
        return;
      switch (newQuestData.questID)
      {
        case 4:
        case 5:
        case 16:
        case 20:
        case 34:
        case 42:
        case 48:
        case 64:
        case 67:
        case 84:
        case 99:
        case 101:
        case 122:
        case 125:
        case 140:
        case 148:
        case 157:
        case 167:
          if (!QuestsHelper.isQuestComplete(newQuestData) || timeRestricted && (DateTime.Now - NewQuestsPanel.m_lastQuestReportingUpdate).TotalSeconds < 300.0)
            break;
          NewQuestsPanel.m_lastQuestReportingUpdate = DateTime.Now;
          RemoteServices.Instance.set_CompleteAbandonNewQuest_UserCallBack(new RemoteServices.CompleteAbandonNewQuest_UserCallBack(NewQuestsPanel.completeAbandonNewQuestCallbackStatic));
          RemoteServices.Instance.AbandonNewQuest(-3);
          break;
      }
    }

    private static void completeAbandonNewQuestCallbackStatic(
      CompleteAbandonNewQuest_ReturnType returnData)
    {
      if (returnData.Success)
      {
        if (returnData.goldGiven)
          GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
        if (returnData.honourGiven)
          GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
        if (returnData.fpGiven)
          GameEngine.Instance.World.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
        if (returnData.premiumCardsGiven)
          GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens.Add(returnData.premiumCardID, new CardTypes.PremiumToken()
          {
            Reward = 1,
            UserPremiumTokenID = returnData.premiumCardID,
            WorldID = RemoteServices.Instance.ProfileWorldID,
            Type = 4113
          });
        if (returnData.cardPacksGiven > 0)
        {
          int key = 1;
          if (GameEngine.Instance.cardPackManager.ProfileUserCardPacks.ContainsKey(key))
            GameEngine.Instance.cardPackManager.ProfileUserCardPacks[key].Count += returnData.cardPacksGiven;
          else
            GameEngine.Instance.cardPackManager.ProfileUserCardPacks[key] = new CardTypes.UserCardPack()
            {
              OfferID = key,
              Count = returnData.cardPacksGiven
            };
        }
        if (returnData.gloryGiven)
          GameEngine.Instance.World.clearGloryHistory();
        if (returnData.villageUpdated >= 0)
          GameEngine.Instance.flushVillage(returnData.villageUpdated);
        if (returnData.ticketsGiven > 0)
          GameEngine.Instance.World.addTickets(-1, returnData.ticketsGiven);
      }
      else if (returnData.m_errorCode == ErrorCodes.ErrorCode.NEW_QUESTS_FAILED_REWARD)
      {
        int num = (int) MyMessageBox.Show(SK.Text("QUESTS_failed_reward_body", "We have been unable to give the correct reward for this quest, please wait a few minutes and try again. If this doesn't work, please contact support."), SK.Text("QUESTS_failed_reward", "Quest Reward Error"));
      }
      if (returnData.m_newQuestsData == null)
        return;
      GameEngine.Instance.World.setNewQuestData(returnData.m_newQuestsData);
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

    public class NewQuestLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage questImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel lblQuestName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblQuestDescription = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblObjective = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDButton startQuestButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton abandonQuestButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton furtherTextButton = new CustomSelfDrawPanel.CSDButton();
      private NewQuestsPanel m_parent;
      private int m_quest = -1;
      private int m_activeQuest = -1;

      public void init(
        int quest,
        NewQuestsPanel parent,
        int position,
        int activeQuest,
        bool allowStart,
        bool completed)
      {
        this.m_quest = quest;
        this.m_activeQuest = activeQuest;
        this.m_parent = parent;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.quest_screen_bar2 : (Image) GFXLibrary.quest_screen_bar1;
        this.backgroundImage.Position = new Point(60, 11);
        if (!completed)
          this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        if (quest < 0)
        {
          this.lblQuestName.Text = SK.Text("QUESTS_AbandonedQuests", "Abandoned Quests : ") + (-quest).ToString();
          this.lblQuestName.Color = ARGBColors.Black;
          if (activeQuest != quest)
            this.lblQuestName.RolloverColor = ARGBColors.White;
          this.lblQuestName.Position = new Point(9, 0);
          this.lblQuestName.Size = new Size(700, this.backgroundImage.Height);
          this.lblQuestName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
          this.lblQuestName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblQuestName);
          this.startQuestButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
          this.startQuestButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.startQuestButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
          this.startQuestButton.Position = new Point(670, 6);
          this.startQuestButton.Text.Text = SK.Text("QUESTS_Restore", "Restore");
          this.startQuestButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.startQuestButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
          this.startQuestButton.TextYOffset = -3;
          this.startQuestButton.Text.Color = ARGBColors.Black;
          this.startQuestButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.restoreQuests), "NewQuests_Restore_Quests_Clicked");
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.startQuestButton);
          this.Size = new Size(880, 60);
        }
        else
        {
          NewQuests.NewQuestDefinition newQuestDef = NewQuests.getNewQuestDef(quest);
          if (!completed)
            this.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
          if (activeQuest != quest || completed)
          {
            this.Size = new Size(880, 60);
          }
          else
          {
            this.Size = new Size(880, 130);
            NumberFormatInfo nfi = GameEngine.NFI;
            switch (newQuestDef.ID)
            {
              case 4:
              case 16:
              case 34:
              case 48:
              case 64:
              case 84:
              case 101:
              case 122:
                this.lblQuestDescription.Text += SK.Text("QUESTS_Spread_New_description", "Learn about Invite a Friend");
                break;
              default:
                if (newQuestDef.parameter > 0)
                {
                  CustomSelfDrawPanel.CSDLabel questDescription = this.lblQuestDescription;
                  questDescription.Text = questDescription.Text + SK.NoStoreText("Z_QUEST_DESCRIPTIONS_" + newQuestDef.tagString) + " : " + newQuestDef.parameter.ToString("N", (IFormatProvider) nfi);
                  break;
                }
                this.lblQuestDescription.Text += SK.NoStoreText("Z_QUEST_DESCRIPTIONS_" + newQuestDef.tagString);
                break;
            }
            this.lblQuestDescription.Color = ARGBColors.Black;
            this.lblQuestDescription.RolloverColor = ARGBColors.White;
            this.lblQuestDescription.Position = new Point(175, 57);
            this.lblQuestDescription.Size = new Size(760, 50);
            this.lblQuestDescription.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
            this.lblQuestDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.lblQuestDescription.Data = newQuestDef.ID;
            this.lblQuestDescription.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.OpenFurtherInformation));
            this.lblQuestDescription.Tag = (object) newQuestDef.tagString;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.lblQuestDescription);
            this.lblObjective.Text = SK.Text("QUEST_PANEL_DESCRIPTION_OBJECTIVE", "Objective");
            this.lblObjective.Color = ARGBColors.Black;
            this.lblObjective.Position = new Point(70, 57);
            this.lblObjective.Size = new Size(760, 50);
            this.lblObjective.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
            this.lblObjective.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.lblObjective);
            NewQuestsPanel.addRewardIcons((CustomSelfDrawPanel.CSDControl) this, new Point(70, 95), newQuestDef, 1);
          }
          this.questImage.Image = (Image) GFXLibrary.quest_icons[Math.Min(newQuestDef.questType, GFXLibrary.quest_icons.Length - 1)];
          this.questImage.Position = new Point(0, 6);
          if (!completed)
            this.questImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
          this.addControl((CustomSelfDrawPanel.CSDControl) this.questImage);
          this.lblQuestName.Text = SK.NoStoreText("Z_QUESTS_" + newQuestDef.tagString);
          this.lblQuestName.Color = ARGBColors.Black;
          if (activeQuest != quest && !completed)
            this.lblQuestName.RolloverColor = ARGBColors.White;
          this.lblQuestName.Position = new Point(9, 0);
          this.lblQuestName.Size = new Size(700, this.backgroundImage.Height);
          this.lblQuestName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
          this.lblQuestName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
          if (!completed)
            this.lblQuestName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblQuestName);
          if (GameEngine.Instance.World.WorldEnded)
            return;
          if (allowStart && !completed)
          {
            this.startQuestButton.ImageNorm = (Image) GFXLibrary.quest_checkboxes[0];
            this.startQuestButton.ImageOver = (Image) GFXLibrary.quest_checkboxes[2];
            this.startQuestButton.Position = new Point(715, 6);
            this.startQuestButton.MoveOnClick = true;
            this.startQuestButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.startQuest), "NewQuests_Start_Quest_Clicked");
            this.startQuestButton.CustomTooltipID = 3201;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.startQuestButton);
          }
          if (completed)
            return;
          this.abandonQuestButton.ImageNorm = (Image) GFXLibrary.quest_checkboxes[1];
          this.abandonQuestButton.ImageOver = (Image) GFXLibrary.quest_checkboxes[3];
          this.abandonQuestButton.Position = new Point(765, 6);
          this.abandonQuestButton.MoveOnClick = true;
          this.abandonQuestButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.abandonQuest), "NewQuests_Abandon_Clicked");
          this.abandonQuestButton.CustomTooltipID = 3202;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.abandonQuestButton);
        }
      }

      public bool update(double localTime) => true;

      public void OpenFurtherInformation()
      {
        int data = CustomSelfDrawPanel.StaticClickedControl.Data;
        InterfaceMgr.Instance.openNewQuestFurtherTextPopup((string) CustomSelfDrawPanel.StaticClickedControl.Tag, data);
      }

      private void lineClicked()
      {
        if (this.m_activeQuest == this.m_quest)
          return;
        GameEngine.Instance.playInterfaceSound("NewQuests_Expand_Quest_Description");
        this.m_parent.expandQuest(this.m_quest);
      }

      private void startQuest()
      {
        if (this.m_parent == null)
          return;
        this.m_parent.startQuest(this.m_quest);
      }

      private void abandonQuest()
      {
        if (this.m_parent == null)
          return;
        this.m_parent.abandonQuest(this.m_quest);
      }

      private void restoreQuests()
      {
        if (this.m_parent == null)
          return;
        this.m_parent.restoreQuests();
      }
    }
  }
}
