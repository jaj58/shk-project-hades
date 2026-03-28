// Decompiled with JetBrains decompiler
// Type: Kingdoms.NewQuestsCompletedPanel
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
  public class NewQuestsCompletedPanel : CustomSelfDrawPanel
  {
    private IContainer components;
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage overlayImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDVertScrollBar questsScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea questsScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDLabel headerLabel = new CustomSelfDrawPanel.CSDLabel();
    private Form m_parent;
    private bool isQuestList = true;
    private string questText;
    private int _questID;
    private List<int> completedQuests;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.None;
    }

    public NewQuestsCompletedPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void setCompletedQuests(List<int> quests) => this.completedQuests = quests;

    public void init(Form parent, bool forQuestList, string questTag, int questID)
    {
      this.m_parent = parent;
      this.clearControls();
      this.isQuestList = forQuestList;
      this.questText = questTag;
      this._questID = questID;
      this.mainBackgroundImage.Image = (Image) GFXLibrary.mail2_mail_panel_middle_middle;
      this.mainBackgroundImage.ClipRect = new Rectangle(new Point(), this.Size);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = this.Size;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.questsScrollArea.Position = new Point(27, 30);
      this.questsScrollArea.Size = new Size(409, 304);
      this.questsScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(409, 304));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.questsScrollArea);
      this.questsScrollBar.Position = new Point(444, 35);
      this.questsScrollBar.Size = new Size(24, 294);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.questsScrollBar);
      this.questsScrollBar.Value = 0;
      this.questsScrollBar.Max = 100;
      this.questsScrollBar.NumVisibleLines = 25;
      this.questsScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.questsScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      this.mouseWheelOverlay.Position = this.questsScrollArea.Position;
      this.mouseWheelOverlay.Size = this.questsScrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
      this.overlayImage.Image = (Image) GFXLibrary.char_achievementOverlay;
      this.overlayImage.Position = new Point(0, 0);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.overlayImage);
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(this.Width - 40, 0);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "NewQuestsCompletedPanel_close");
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      if (forQuestList)
      {
        this.headerLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
        this.headerLabel.Text = SK.Text("QUESTS_CompletedQuests", "Completed Quests");
      }
      else
      {
        this.headerLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
        this.headerLabel.Text = SK.NoStoreText("Z_QUESTS_" + this.questText) + " - ";
        this.headerLabel.Text += SK.Text("QUESTS_FurtherDetails", "Further Information");
      }
      this.headerLabel.Position = new Point(0, 0);
      this.headerLabel.Size = new Size(this.Width, 30);
      this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.headerLabel.Color = ARGBColors.White;
      this.headerLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabel);
      this.rebuild();
    }

    private void closeClick() => this.m_parent.Close();

    private void wallScrollBarMoved()
    {
      int y = this.questsScrollBar.Value;
      this.questsScrollArea.Position = new Point(this.questsScrollArea.X, 30 - y);
      this.questsScrollArea.ClipRect = new Rectangle(this.questsScrollArea.ClipRect.X, y, this.questsScrollArea.ClipRect.Width, this.questsScrollArea.ClipRect.Height);
      this.mainBackgroundImage.invalidate();
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

    public void rebuild()
    {
      int num = 0;
      this.questsScrollArea.clearControls();
      if (this.isQuestList)
      {
        int[] numArray;
        if (this.completedQuests == null)
        {
          NewQuestsData newQuestData = GameEngine.Instance.World.getNewQuestData();
          if (newQuestData == null)
            return;
          numArray = newQuestData.completedQuests;
        }
        else
          numArray = this.completedQuests.ToArray();
        for (int position = 0; position < numArray.Length; ++position)
        {
          int quest = numArray[position];
          NewQuestsCompletedPanel.NewQuestLine control = new NewQuestsCompletedPanel.NewQuestLine();
          if (num != 0)
            num += 5;
          control.Position = new Point(0, num);
          control.init(quest, position);
          this.questsScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
          num += control.Height;
        }
      }
      else
      {
        CustomSelfDrawPanel.CSDLabel control = new CustomSelfDrawPanel.CSDLabel();
        switch (this._questID)
        {
          case 4:
          case 16:
          case 34:
          case 48:
          case 64:
          case 84:
          case 101:
          case 122:
            control.Text = GameEngine.Instance.World.isBigpointAccount || Program.aeriaInstall || Program.bigpointPartnerInstall || Program.arcInstall ? SK.Text("QUESTS_IAF_Help2", "Why not invite a friend to play Kingdoms? They can fight alongside you and you will help us to further develop the game. ") : SK.Text("QUESTS_IAF_Help1", "Learn about how inviting your friends to the game can earn you up to $160 worth of crowns.");
            break;
          default:
            control.Text = SK.NoStoreText("Z_QUEST_HELP_" + this.questText);
            break;
        }
        control.Color = ARGBColors.Black;
        control.Position = new Point(36, 30);
        control.Size = new Size(this.questsScrollArea.Width, this.questsScrollArea.Height);
        control.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        control.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control);
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
      this.mainBackgroundImage.invalidate();
    }

    public class NewQuestLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage questImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel lblQuestName = new CustomSelfDrawPanel.CSDLabel();

      public void init(int quest, int position)
      {
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.quest_screen_bar2 : (Image) GFXLibrary.quest_screen_bar1;
        this.backgroundImage.Position = new Point(60, 11);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        NewQuests.NewQuestDefinition newQuestDef = NewQuests.getNewQuestDef(quest);
        this.Size = new Size(444, 60);
        this.questImage.Image = (Image) GFXLibrary.quest_icons[Math.Min(newQuestDef.questType, GFXLibrary.quest_icons.Length - 1)];
        this.questImage.Position = new Point(0, 6);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.questImage);
        this.lblQuestName.Text = SK.NoStoreText("Z_QUESTS_" + newQuestDef.tagString);
        this.lblQuestName.Color = ARGBColors.Black;
        this.lblQuestName.Position = new Point(9, 0);
        this.lblQuestName.Size = new Size(400, this.backgroundImage.Height);
        this.lblQuestName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
        this.lblQuestName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblQuestName);
      }
    }
  }
}
