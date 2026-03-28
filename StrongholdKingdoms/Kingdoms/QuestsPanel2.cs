// Decompiled with JetBrains decompiler
// Type: Kingdoms.QuestsPanel2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using Stronghold.AuthClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class QuestsPanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private Panel focusPanel;
    public static QuestsPanel2 Instance;
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider4Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider5Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider6Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel objectivesLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel statusLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel rewardLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDVertScrollBar outgoingScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea outgoingScrollArea = new CustomSelfDrawPanel.CSDArea();
    private int blockYSize;
    private List<WorldMap.LocalArmyData> armyList = new List<WorldMap.LocalArmyData>();
    public static int questXPos;
    private bool[] completedActiveQuests;
    public bool downloadingQuestInfo;
    public bool downloadedQuestInfo;
    private List<QuestsPanel2.QuestLine> lineList = new List<QuestsPanel2.QuestLine>();
    private bool inCompleteQuest;
    private DateTime completedQuestTime = DateTime.MinValue;
    private QuestsPanel2.ArmyComparer armyComparer = new QuestsPanel2.ArmyComparer();

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
      this.Name = nameof (QuestsPanel2);
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 566);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public QuestsPanel2()
    {
      QuestsPanel2.Instance = this;
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.focusPanel.Focus();
    }

    public void init(bool resized)
    {
      int height = this.Height;
      QuestsPanel2.questXPos = this.Location.X;
      QuestsPanel2.Instance = this;
      this.clearControls();
      this.headerImage.Size = new Size(this.Width, 40);
      this.headerImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage);
      this.headerImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
      this.backgroundImage.Size = new Size(this.Width, height - 40);
      this.backgroundImage.Position = new Point(0, 40);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
      this.backgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
      this.parishNameLabel.Text = SK.Text("QuestPanel_TutorialQuests", "Tutorial Quests");
      this.parishNameLabel.Color = ARGBColors.White;
      this.parishNameLabel.DropShadowColor = ARGBColors.Black;
      this.parishNameLabel.Position = new Point(20, 0);
      this.parishNameLabel.Size = new Size(this.Width - 40, 40);
      this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
      this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.parishNameLabel);
      this.blockYSize = height - 40 - 56;
      this.headerLabelsImage.Size = new Size(this.Width - 25 - 23, 28);
      this.headerLabelsImage.Position = new Point(25, 5);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider1Image.Position = new Point(490, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider1Image);
      this.objectivesLabel.Text = SK.Text("QuestsPanel_Objectives", "Objectives");
      this.objectivesLabel.Color = ARGBColors.Black;
      this.objectivesLabel.Position = new Point(12, -2);
      this.objectivesLabel.Size = new Size(323, this.headerLabelsImage.Height);
      this.objectivesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.objectivesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.objectivesLabel);
      this.statusLabel.Text = SK.Text("QuestsPanel_Status", "Status");
      this.statusLabel.Color = ARGBColors.Black;
      this.statusLabel.Position = new Point(496, -2);
      this.statusLabel.Size = new Size(223, this.headerLabelsImage.Height);
      this.statusLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.statusLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.statusLabel);
      this.outgoingScrollArea.Position = new Point(25, 40);
      this.outgoingScrollArea.Size = new Size(915, this.blockYSize - 40 - 10);
      this.outgoingScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(915, this.blockYSize - 40 - 10));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.outgoingScrollArea);
      int num = this.outgoingScrollBar.Value;
      this.outgoingScrollBar.Position = new Point(943, 40);
      this.outgoingScrollBar.Size = new Size(24, this.blockYSize - 40 - 10);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.outgoingScrollBar);
      this.outgoingScrollBar.Value = 0;
      this.outgoingScrollBar.Max = 100;
      this.outgoingScrollBar.NumVisibleLines = 25;
      this.outgoingScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.outgoingScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      if (!resized)
        this.downloadQuestInfo();
      this.rebuild();
      if (!resized)
        return;
      this.outgoingScrollBar.Value = num;
    }

    public void downloadQuestInfo()
    {
      this.downloadedQuestInfo = false;
      this.downloadingQuestInfo = true;
      this.completedActiveQuests = (bool[]) null;
      RemoteServices.Instance.set_GetQuestStatus_UserCallBack(new RemoteServices.GetQuestStatus_UserCallBack(this.GetQuestStatusCallback));
      RemoteServices.Instance.GetQuestStatus();
    }

    public void GetQuestStatusCallback(GetQuestStatus_ReturnType returnData)
    {
      this.downloadedQuestInfo = true;
      this.downloadingQuestInfo = false;
      if (!returnData.Success)
        return;
      GameEngine.Instance.World.setTutorialInfo(returnData.m_tutorialInfo);
      this.completedActiveQuests = returnData.m_preCompletedQuests;
      this.rebuild();
    }

    public void update()
    {
      double localTime = DXTimer.GetCurrentMilliseconds() / 1000.0;
      foreach (QuestsPanel2.QuestLine line in this.lineList)
        line.update(localTime);
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

    public void closing() => InterfaceMgr.Instance.closeDonatePopup();

    public void rebuild()
    {
      int[] activeQuests = GameEngine.Instance.World.getActiveQuests();
      this.outgoingScrollArea.clearControls();
      int num1 = 0;
      this.lineList.Clear();
      double num2 = DXTimer.GetCurrentMilliseconds() / 1000.0;
      for (int position = 0; position < activeQuests.Length; ++position)
      {
        int quest = activeQuests[position];
        int completeState = 0;
        if (this.completedActiveQuests != null && position < this.completedActiveQuests.Length)
          completeState = !this.completedActiveQuests[position] ? 1 : 2;
        QuestsPanel2.QuestLine control = new QuestsPanel2.QuestLine();
        if (num1 != 0)
          num1 += 5;
        control.Position = new Point(0, num1);
        control.init(quest, this, completeState, position);
        this.outgoingScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num1 += control.Height;
        this.lineList.Add(control);
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
      this.backgroundImage.invalidate();
      this.update();
    }

    public bool isRewardAvailable(int quest)
    {
      int[] activeQuests = GameEngine.Instance.World.getActiveQuests();
      for (int index = 0; index < activeQuests.Length; ++index)
      {
        if (this.completedActiveQuests != null && index < this.completedActiveQuests.Length && this.completedActiveQuests[index] && activeQuests[index] == quest)
          return true;
      }
      return false;
    }

    public bool isQuestComplete(int quest)
    {
      foreach (int completedQuest in GameEngine.Instance.World.getCompletedQuests())
      {
        if (completedQuest == quest)
          return true;
      }
      return false;
    }

    public void completeQuest(int quest)
    {
      if (this.inCompleteQuest && (DateTime.Now - this.completedQuestTime).TotalMinutes < 2.0)
        return;
      this.completedQuestTime = DateTime.Now;
      this.inCompleteQuest = true;
      RemoteServices.Instance.set_CompleteQuest_UserCallBack(new RemoteServices.CompleteQuest_UserCallBack(this.CompleteQuestCallback));
      RemoteServices.Instance.CompleteQuest(quest);
    }

    public void CompleteQuestCallback(CompleteQuest_ReturnType returnData)
    {
      if (returnData.Success)
      {
        GameEngine.Instance.World.setTutorialInfo(returnData.m_tutorialInfo);
        this.completedActiveQuests = returnData.m_preCompletedQuests;
        if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_QUESTS)
          this.rebuild();
        if (returnData.questCompleted >= 0)
        {
          List<Quests.QuestReward> questRewards = Quests.getQuestRewards(returnData.questCompleted, false, (NumberFormatInfo) null);
          foreach (Quests.QuestReward questReward in questRewards)
          {
            switch (questReward.type)
            {
              case 20000:
                List<int> userVillageIdList = GameEngine.Instance.World.getUserVillageIDList();
                if (userVillageIdList.Count != 1)
                {
                  GameEngine.Instance.flushVillages();
                  if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE)
                  {
                    GameEngine.Instance.downloadCurrentVillage();
                    continue;
                  }
                  continue;
                }
                if (userVillageIdList.Count == 1)
                {
                  VillageMap village = GameEngine.Instance.getVillage(userVillageIdList[0]);
                  if (village != null)
                  {
                    village.addResources(questReward.data, questReward.amount);
                    continue;
                  }
                  continue;
                }
                continue;
              case 20001:
                GameEngine.Instance.World.addGold((double) questReward.amount);
                continue;
              case 20002:
                GameEngine.Instance.World.addHonour((double) questReward.amount);
                continue;
              case 20003:
                GameEngine.Instance.World.addResearchPoints(questReward.amount);
                continue;
              case 20004:
                if (returnData.cardAdded >= 0)
                {
                  if (questReward.data != 4113)
                  {
                    GameEngine.Instance.cardsManager.addProfileCard(returnData.cardAdded, CardTypes.getStringFromCard(questReward.data));
                    continue;
                  }
                  CardTypes.PremiumToken premiumToken = new CardTypes.PremiumToken();
                  premiumToken.Reward = 1;
                  premiumToken.UserPremiumTokenID = returnData.cardAdded;
                  premiumToken.WorldID = RemoteServices.Instance.ProfileWorldID;
                  premiumToken.Type = 4113;
                  bool flag = false;
                  if (GameEngine.Instance.cardsManager.UserCardData.premiumCard <= 0)
                  {
                    XmlRpcCardsProvider forEndpoint = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
                    XmlRpcCardsRequest req = new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""));
                    req.SessionGUID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", "");
                    req.WorldID = RemoteServices.Instance.ProfileWorldID.ToString();
                    req.UserCardID = returnData.cardAdded.ToString();
                    if (premiumToken.Type == 4112)
                      req.CardString = "CARDTYPE_PREMIUM";
                    if (premiumToken.Type == 4113)
                      req.CardString = "CARDTYPE_PREMIUM2";
                    if (premiumToken.Type == 4114)
                      req.CardString = "CARDTYPE_PREMIUM30";
                    XmlRpcCardsResponse rpcCardsResponse = forEndpoint.playPremium((ICardsRequest) req, 6000);
                    int? successCode = rpcCardsResponse.SuccessCode;
                    if ((successCode.GetValueOrDefault() != 1 ? 1 : (!successCode.HasValue ? 1 : 0)) != 0)
                    {
                      flag = true;
                      int num = (int) MyMessageBox.Show(rpcCardsResponse.Message, "Error playing premium");
                    }
                    else
                      GameEngine.Instance.cardsManager.CardPlayed(-1, premiumToken.Type, -1);
                  }
                  else
                    flag = true;
                  if (flag)
                  {
                    GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens.Add(returnData.cardAdded, premiumToken);
                    continue;
                  }
                  continue;
                }
                continue;
              case 20006:
                GameEngine.Instance.World.FakeCardPoints += questReward.amount;
                continue;
              default:
                continue;
            }
          }
          bool flag1 = false;
          foreach (Quests.QuestReward questReward in questRewards)
          {
            if (questReward.type == 20004 || questReward.type == 20006)
              flag1 = true;
          }
          if (flag1)
            PlayCardsWindow.resetRewardCardTimer();
        }
      }
      this.inCompleteQuest = false;
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

    public class QuestLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel lblQuestDescription = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblStatus = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblReward = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDButton collectButton = new CustomSelfDrawPanel.CSDButton();
      private QuestsPanel2 m_parent;
      private int m_quest = -1;

      public void init(int quest, QuestsPanel2 parent, int completeState, int position)
      {
        this.m_quest = quest;
        this.m_parent = parent;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_02_dark : (Image) GFXLibrary.lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.lblQuestDescription.Text = Quests.getQuestText(quest);
        this.lblQuestDescription.Color = ARGBColors.Black;
        this.lblQuestDescription.RolloverColor = ARGBColors.White;
        this.lblQuestDescription.Position = new Point(9, 0);
        this.lblQuestDescription.Size = new Size(480, this.backgroundImage.Height);
        this.lblQuestDescription.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblQuestDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblQuestDescription);
        this.collectButton.Visible = false;
        switch (completeState)
        {
          case -1:
            this.lblStatus.Visible = false;
            break;
          case 0:
            this.lblStatus.Text = "?";
            break;
          case 1:
            this.lblStatus.Text = SK.Text("QuestLine_Not_Complete", "Objective not complete");
            break;
          case 2:
            this.lblStatus.Text = SK.Text("QuestLine_Complete", "Objective Complete");
            if (!GameEngine.Instance.World.WorldEnded)
            {
              this.collectButton.Visible = true;
              break;
            }
            break;
        }
        this.lblStatus.Color = ARGBColors.Black;
        this.lblStatus.RolloverColor = ARGBColors.White;
        this.lblStatus.Position = new Point(496, 0);
        this.lblStatus.Size = new Size(288, this.backgroundImage.Height);
        this.lblStatus.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblStatus.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblStatus);
        this.collectButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.collectButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.collectButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.collectButton.Position = new Point(774, 4);
        this.collectButton.Text.Text = SK.Text("QuestLine_Collect_Reward", "Collect Reward");
        this.collectButton.Text.Color = ARGBColors.Black;
        this.collectButton.Text.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.collectButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked), "QuestsPanel2_collect");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.collectButton);
      }

      public bool update(double localTime) => true;

      private void lineClicked()
      {
        this.collectButton.Enabled = false;
        this.m_parent.completeQuest(this.m_quest);
      }
    }
  }
}
