// Decompiled with JetBrains decompiler
// Type: Kingdoms.NewQuestRewardPanel
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
  public class NewQuestRewardPanel : CustomSelfDrawPanel
  {
    private IContainer components;
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerBarImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel captureLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDFill backgroundImage = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDButton collectButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton collectGloryButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage strip1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage strip2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage strip3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage strip4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage questIcon = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel lblQuestName = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel orLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage villageIcon = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel targetVillageLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel villageNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage chargesImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDVertScrollBar questsScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea questsScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDVertExtendingPanel insetImage = new CustomSelfDrawPanel.CSDVertExtendingPanel();
    private int m_questID = -1;
    private int m_villageID = -1;
    private NewQuestsPanel m_questPanel;
    private NewQuests.NewQuestDefinition m_questDef;
    private bool m_AppleChecked;
    private bool m_AppleAvailable = true;
    private bool m_WoodChecked;
    private bool m_WoodAvailable = true;
    private bool m_StoneChecked;
    private bool m_StoneAvailable = true;
    private int m_buildingType;
    private bool m_awaitingResponse;

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
      this.BackColor = ARGBColors.White;
      this.Name = nameof (NewQuestRewardPanel);
      this.Size = new Size(600, 55);
      this.ResumeLayout(false);
    }

    public NewQuestRewardPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(
      int questID,
      int villageID,
      NewQuestsPanel questPanel,
      NewQuestRewardPopup parent)
    {
      this.m_questID = questID;
      this.m_villageID = -1;
      this.m_questPanel = questPanel;
      this.clearControls();
      bool flag = false;
      if (GameEngine.Instance.World.YourHouse > 0)
        flag = true;
      if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
        flag = false;
      List<int> userVillageIdList = GameEngine.Instance.World.getUserVillageIDList();
      NewQuests.NewQuestDefinition newQuestDef = NewQuests.getNewQuestDef(questID);
      int height = newQuestDef.reward_apples <= 0 && newQuestDef.reward_stone <= 0 && newQuestDef.reward_wood <= 0 || userVillageIdList.Count <= 1 ? (newQuestDef.getRewardGlory() <= 0 || !flag ? 200 : 270) : (newQuestDef.getRewardGlory() <= 0 || !flag ? 410 : 492);
      parent.Size = new Size(550, height);
      this.headerBarImage.Position = new Point(0, 0);
      this.headerBarImage.Size = new Size(this.Width, 30);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.headerBarImage);
      this.headerBarImage.Create((Image) GFXLibrary.messageboxtop_left, (Image) GFXLibrary.messageboxtop_middle, (Image) GFXLibrary.messageboxtop_right);
      this.backgroundImage.SpecialGradient = true;
      this.backgroundImage.Position = new Point(0, 30);
      this.backgroundImage.Size = new Size(this.Width, height - 30);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
      this.captureLabel.Text = SK.Text("QuestLine_Collect_Reward", "Collect Reward");
      this.captureLabel.Color = ARGBColors.White;
      this.captureLabel.Position = new Point(13, 7);
      this.captureLabel.Size = new Size(335, 20);
      this.captureLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.captureLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.headerBarImage.addControl((CustomSelfDrawPanel.CSDControl) this.captureLabel);
      this.strip1.Image = (Image) GFXLibrary.quest_popup_hz_strip_02;
      this.strip1.Position = new Point(4, 4);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.strip1);
      this.questIcon.Image = (Image) GFXLibrary.quest_icons[Math.Min(newQuestDef.questType, GFXLibrary.quest_icons.Length - 1)];
      this.questIcon.Position = new Point(12, 12);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.questIcon);
      this.lblQuestName.Text = SK.NoStoreText("Z_QUESTS_" + newQuestDef.tagString);
      this.lblQuestName.Color = ARGBColors.Black;
      this.lblQuestName.Position = new Point(70, 26);
      this.lblQuestName.Size = new Size(700, 30);
      this.lblQuestName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.lblQuestName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblQuestName);
      this.strip2.Image = (Image) GFXLibrary.quest_popup_hz_strip_01;
      this.strip2.Position = new Point(24, 79);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.strip2);
      NewQuestsPanel.addRewardIcons((CustomSelfDrawPanel.CSDControl) this.backgroundImage, new Point(30, 80), newQuestDef, 0);
      this.collectButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      this.collectButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      this.collectButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      this.collectButton.Position = new Point(385, 87);
      this.collectButton.Text.Text = SK.Text("QUESTS_Collect", "Collect");
      this.collectButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.collectButton.Text.Color = ARGBColors.Black;
      this.collectButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.okClicked), "NewQuests_Collect_Clicked");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.collectButton);
      if (newQuestDef.reward_charges.Length > 0)
      {
        this.chargesImage.Image = (Image) GFXLibrary.quest_rewards[9];
        this.chargesImage.Position = new Point(25, height - 82);
        this.chargesImage.CustomTooltipID = 3212;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.chargesImage);
      }
      if (newQuestDef.reward_apples > 0 || newQuestDef.reward_stone > 0 || newQuestDef.reward_wood > 0)
      {
        if (userVillageIdList.Count > 1)
        {
          this.strip4.Image = (Image) GFXLibrary.quest_popup_hz_strip_03;
          this.strip4.Position = new Point(24, 151);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.strip4);
          this.collectButton.Enabled = false;
          this.villageIcon.Image = (Image) GFXLibrary.char_village_icons[5];
          this.villageIcon.Position = new Point(30, 148);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.villageIcon);
          this.targetVillageLabel.Text = !(Program.mySettings.LanguageIdent == "fr") ? SK.Text("VillageArmiesPanel_Target_Village", "Target Village") : "Village Cible";
          this.targetVillageLabel.Color = ARGBColors.Black;
          this.targetVillageLabel.Position = new Point(0, 130);
          this.targetVillageLabel.Size = new Size(this.Width, 30);
          this.targetVillageLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
          this.targetVillageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.targetVillageLabel);
          this.villageNameLabel.Text = SK.Text("QUESTS_Pick_a_Village", "Pick a Village");
          this.villageNameLabel.Color = ARGBColors.Black;
          this.villageNameLabel.Position = new Point(90, 146);
          this.villageNameLabel.Size = new Size(this.Width - 90, this.villageIcon.Height);
          this.villageNameLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
          this.villageNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.villageNameLabel);
          this.questsScrollArea.Position = new Point(61, 200);
          this.questsScrollArea.Size = new Size(390, 115);
          this.questsScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(390, 115));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.questsScrollArea);
          this.insetImage.Position = new Point(55, 198);
          this.insetImage.Size = new Size(440, 119);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.insetImage);
          this.insetImage.Create((Image) GFXLibrary.quest_popup_inset_top, (Image) GFXLibrary.quest_popup_inset_middle, (Image) GFXLibrary.quest_popup_inset_bottom);
          this.questsScrollBar.Position = new Point(461, 205);
          this.questsScrollBar.Size = new Size(24, 105);
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
          this.addVillages(true);
        }
        else if (userVillageIdList.Count > 0)
          this.m_villageID = userVillageIdList[0];
      }
      if (newQuestDef.getRewardGlory() > 0 && flag)
      {
        this.orLabel.Text = SK.Text("QUESTS_or", "Or");
        this.orLabel.Color = ARGBColors.Black;
        this.orLabel.Position = new Point(0, height - 145);
        this.orLabel.Size = new Size(this.Width, 30);
        this.orLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
        this.orLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.orLabel);
        this.strip3.Image = (Image) GFXLibrary.quest_popup_hz_strip_01;
        this.strip3.Position = new Point(24, height - 123 - 1);
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.strip3);
        NewQuestsPanel.addRewardIcons((CustomSelfDrawPanel.CSDControl) this.backgroundImage, new Point(30, height - 123), newQuestDef, -1);
        this.collectGloryButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
        this.collectGloryButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
        this.collectGloryButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
        this.collectGloryButton.Position = new Point(316, height - 123 + 8 - 1);
        this.collectGloryButton.Text.Text = SK.Text("QUESTS_Collect_Glory", "Collect Glory");
        this.collectGloryButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.collectGloryButton.Text.Color = ARGBColors.Black;
        this.collectGloryButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.okGloryClicked), "NewQuests_Collect_Glory_Clicked");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.collectGloryButton);
      }
      this.cancelButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      this.cancelButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      this.cancelButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      this.cancelButton.Position = new Point(385, height - 75);
      this.cancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.cancelButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.cancelButton.Text.Color = ARGBColors.Black;
      this.cancelButton.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() =>
      {
        InterfaceMgr.Instance.closeNewQuestRewardPopup();
        InterfaceMgr.Instance.ParentForm.TopMost = true;
        InterfaceMgr.Instance.ParentForm.TopMost = false;
      }), "NewQuests_Cancel");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.cancelButton);
      this.Invalidate();
      parent.Invalidate();
    }

    private void wallScrollBarMoved()
    {
      int y = this.questsScrollBar.Value;
      this.questsScrollArea.Position = new Point(this.questsScrollArea.X, 200 - y);
      this.questsScrollArea.ClipRect = new Rectangle(this.questsScrollArea.ClipRect.X, y, this.questsScrollArea.ClipRect.Width, this.questsScrollArea.ClipRect.Height);
      this.questsScrollArea.invalidate();
      this.questsScrollBar.invalidate();
      this.insetImage.invalidate();
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

    public void okClicked()
    {
      this.m_questDef = NewQuests.getNewQuestDef(this.m_questID);
      VillageMap village = GameEngine.Instance.getVillage(this.m_villageID);
      if (village == null)
      {
        this.confirmAvailableSpace();
      }
      else
      {
        VillageMap.StockpileLevels levels1 = new VillageMap.StockpileLevels();
        VillageMap.GranaryLevels levels2 = new VillageMap.GranaryLevels();
        village.getStockpileLevels(levels1);
        village.getGranaryLevels(levels2);
        bool flag = false;
        if (this.m_questDef.reward_apples > 0 && Convert.ToInt32(GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, 18, false) * CardTypes.getResourceCapMultiplier(18, GameEngine.Instance.cardsManager.UserCardData) - levels2.fishLevel) < this.m_questDef.reward_apples)
          flag = true;
        if (this.m_questDef.reward_stone > 0 && !flag && Convert.ToInt32(GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, 7, false) * CardTypes.getResourceCapMultiplier(7, GameEngine.Instance.cardsManager.UserCardData) - levels1.stoneLevel) < this.m_questDef.reward_stone)
          flag = true;
        if (this.m_questDef.reward_wood > 0 && !flag && Convert.ToInt32(GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, 6, false) * CardTypes.getResourceCapMultiplier(6, GameEngine.Instance.cardsManager.UserCardData) - levels1.woodLevel) < this.m_questDef.reward_wood)
          flag = true;
        if (flag && MyMessageBox.Show(SK.Text("Quest_Reward_Insufficient_Space", "You do not have enough room to store all of the reward at this village. Are you sure you want to send the reward to this village?"), SK.Text("Quest_Reward_Insufficient_Space_header", "Insufficient Space"), MessageBoxButtons.YesNo) == DialogResult.No)
          return;
        this.CompleteQuest();
      }
    }

    private void CompleteQuest()
    {
      this.m_questPanel.doCompleteQuest(this.m_questID, this.m_villageID, false);
      InterfaceMgr.Instance.closeNewQuestRewardPopup();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }

    public void okGloryClicked()
    {
      if (MyMessageBox.Show(SK.Text("Quest_Reward_Glory_Confirm", "If you select this option you will receive glory points, but no other rewards. Do you wish to continue?"), SK.Text("Quest_Reward_Glory_Title", "Confirm Selection"), MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      this.StillRecieveGloryPoints();
    }

    private void StillRecieveGloryPoints()
    {
      this.m_questPanel.doCompleteQuest(this.m_questID, -1, true);
      InterfaceMgr.Instance.closeNewQuestRewardPopup();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }

    public void update()
    {
    }

    private void addVillages(bool autoSelect)
    {
      List<int> userVillageIdList = GameEngine.Instance.World.getUserVillageIDList();
      userVillageIdList.Sort((IComparer<int>) UserInfoScreen2.villageComparer);
      this.questsScrollArea.clearControls();
      int num = 0;
      for (int index = 0; index < userVillageIdList.Count; ++index)
      {
        int villageID = userVillageIdList[index];
        NewQuestRewardPanel.NewQuestVillageLine control = new NewQuestRewardPanel.NewQuestVillageLine();
        control.Position = new Point(0, num);
        bool selected = villageID == this.m_villageID;
        control.init(villageID, this, index, selected);
        this.questsScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num += control.Height;
      }
      this.questsScrollArea.Size = new Size(this.questsScrollArea.Width, num);
      if (num < this.questsScrollBar.Height)
      {
        this.questsScrollBar.Visible = false;
      }
      else
      {
        this.questsScrollBar.Visible = true;
        this.questsScrollBar.NumVisibleLines = this.questsScrollBar.Height;
        this.questsScrollBar.Max = num - this.questsScrollBar.Height;
      }
      this.questsScrollArea.invalidate();
      this.questsScrollBar.invalidate();
      if (!autoSelect || userVillageIdList.Count != 1)
        return;
      this.villageSelected(userVillageIdList[0]);
    }

    public void villageSelected(int villageID)
    {
      this.villageNameLabel.Text = GameEngine.Instance.World.getVillageName(villageID);
      this.collectButton.Enabled = true;
      this.m_villageID = villageID;
      int villageSize = GameEngine.Instance.World.getVillageSize(villageID);
      this.villageIcon.Image = (Image) GFXLibrary.char_village_icons[villageSize];
      int num = this.questsScrollBar.Value;
      this.addVillages(false);
      this.questsScrollBar.Value = num;
      this.questsScrollBar.scrollDown(0);
      this.wallScrollBarMoved();
      this.Invalidate();
    }

    public void confirmAvailableSpace()
    {
      if (this.m_awaitingResponse)
        return;
      if (!this.m_AppleChecked && this.m_questDef.reward_apples > 0)
      {
        this.m_buildingType = 18;
        this.checkResource();
      }
      else if (!this.m_WoodChecked && this.m_questDef.reward_wood > 0)
      {
        this.m_buildingType = 6;
        this.checkResource();
      }
      else if (!this.m_StoneChecked && this.m_questDef.reward_stone > 0)
      {
        this.m_buildingType = 7;
        this.checkResource();
      }
      else
      {
        if ((!this.m_AppleAvailable || !this.m_WoodAvailable || !this.m_StoneAvailable) && MyMessageBox.Show(SK.Text("Quest_Reward_Insufficient_Space", "You do not have enough room to store all of the reward at this village. Are you sure you want to send the reward to this village?"), SK.Text("Quest_Reward_Insufficient_Space_header", "Insufficient Space"), MessageBoxButtons.YesNo) == DialogResult.No)
          return;
        this.CompleteQuest();
      }
    }

    public void checkResource()
    {
      this.m_awaitingResponse = true;
      RemoteServices.Instance.set_GetResourceLevel_UserCallBack(new RemoteServices.GetResourceLevel_UserCallBack(this.checkResourceCallBack));
      RemoteServices.Instance.GetResourceLevel(this.m_villageID, this.m_buildingType);
    }

    public void checkResourceCallBack(GetResourceLevel_ReturnType returnData)
    {
      this.m_awaitingResponse = false;
      double num = GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, this.m_buildingType, false) - returnData.uncappedLevel;
      switch (this.m_buildingType)
      {
        case 6:
          this.m_WoodChecked = true;
          this.m_WoodAvailable = Convert.ToInt32(num) >= this.m_questDef.reward_wood;
          break;
        case 7:
          this.m_StoneChecked = true;
          this.m_StoneAvailable = Convert.ToInt32(num) >= this.m_questDef.reward_stone;
          break;
        case 18:
          this.m_AppleChecked = true;
          this.m_AppleAvailable = Convert.ToInt32(num) >= this.m_questDef.reward_apples;
          break;
        default:
          return;
      }
      this.confirmAvailableSpace();
    }

    public class NewQuestVillageLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage villageIcon = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel villageName = new CustomSelfDrawPanel.CSDLabel();
      private NewQuestRewardPanel m_parent;
      private int m_villageID = -1;

      public void init(int villageID, NewQuestRewardPanel parent, int position, bool selected)
      {
        this.m_villageID = villageID;
        this.m_parent = parent;
        this.clearControls();
        if (selected)
        {
          this.backgroundImage.Image = (Image) GFXLibrary.quest_popup_inset_highlight;
          this.backgroundImage.Position = new Point(0, 5);
          this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
          this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        }
        this.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.Size = new Size(390, 34);
        int villageSize = GameEngine.Instance.World.getVillageSize(villageID);
        this.villageIcon.Image = (Image) GFXLibrary.char_village_icons[villageSize];
        this.villageIcon.Position = new Point(0, -8);
        this.villageIcon.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.villageIcon);
        this.villageName.Text = GameEngine.Instance.World.getVillageName(villageID);
        this.villageName.Color = ARGBColors.Black;
        this.villageName.Position = new Point(50, 0);
        this.villageName.Size = new Size(330, this.Height);
        this.villageName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
        this.villageName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.villageName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.villageName);
      }

      public bool update(double localTime) => true;

      private void lineClicked()
      {
        GameEngine.Instance.playInterfaceSound("NewQuests_Village_Clicked");
        this.m_parent.villageSelected(this.m_villageID);
      }
    }
  }
}
