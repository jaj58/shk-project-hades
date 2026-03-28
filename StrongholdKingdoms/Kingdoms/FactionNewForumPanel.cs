// Decompiled with JetBrains decompiler
// Type: Kingdoms.FactionNewForumPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class FactionNewForumPanel : CustomSelfDrawPanel, IDockableControl, IForumPostParent
  {
    public const int PANEL_ID = 45;
    private DockableControl dockableControl;
    private IContainer components;
    public static FactionNewForumPanel instance;
    private SparseArray forumArray = new SparseArray();
    private SparseArray forumThreadArray = new SparseArray();
    private int selectedAreaID = -1;
    private int selectedAreaType = -1;
    private string OrigForumName = "";
    private DateTime lastRefreshTime = DateTime.MinValue;
    private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel threadTitleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel playersLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel dateLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDButton forum1Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton forum2Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton forum3Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton forum4Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton forum5Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton forum6Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton forum7Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton newTopicButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton createForumButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton deleteForumButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
    private int[] forums1Positions = new int[2]{ 326, 24 };
    private int[] forums2Positions = new int[4]
    {
      251,
      24,
      402,
      24
    };
    private int[] forums3Positions = new int[6]
    {
      175,
      24,
      326,
      24,
      477,
      24
    };
    private int[] forums4Positions = new int[8]
    {
      100,
      24,
      251,
      24,
      402,
      24,
      553,
      24
    };
    private int[] forums5Positions = new int[10]
    {
      175,
      7,
      326,
      7,
      477,
      7,
      251,
      40,
      402,
      40
    };
    private int[] forums6Positions = new int[12]
    {
      175,
      7,
      326,
      7,
      477,
      7,
      175,
      40,
      326,
      40,
      477,
      40
    };
    private int[] forums7Positions = new int[14]
    {
      100,
      7,
      251,
      7,
      402,
      7,
      553,
      7,
      175,
      40,
      326,
      40,
      477,
      40
    };
    private long currentlySelectedForum = -1;
    private FactionNewTopicPopup m_popup;
    private FactionNewForumPopup m_forumPopup;
    private MyMessageBoxPopUp deletePostPopUp;
    private List<FactionNewForumPanel.ForumThreadLine> lineList = new List<FactionNewForumPanel.ForumThreadLine>();
    public FactionNewForumPanel.ForumComparer forumComparer = new FactionNewForumPanel.ForumComparer();
    private FactionNewForumPanel.ThreadComparer threadComparer = new FactionNewForumPanel.ThreadComparer();

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
      this.Name = nameof (FactionNewForumPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public void clearForum()
    {
      this.forumArray = new SparseArray();
      this.lastRefreshTime = DateTime.MinValue;
      this.forumThreadArray = new SparseArray();
      this.currentlySelectedForum = -1L;
    }

    public FactionNewForumPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      int height = this.Height;
      FactionNewForumPanel.instance = this;
      this.clearControls();
      this.sidebar.addSideBar(6, (CustomSelfDrawPanel) this);
      this.mainBackgroundImage.FillColor = Color.FromArgb(134, 153, 165);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = new Size(this.Width - 200, height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundFade.Image = (Image) GFXLibrary.background_top;
      this.backgroundFade.Position = new Point(0, 0);
      this.backgroundFade.Size = new Size(this.Width - 200, this.backgroundFade.Image.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundFade);
      this.headerLabelsImage.Size = new Size(this.Width - 25 - 23 - 200, 28);
      this.headerLabelsImage.Position = new Point(25, 69);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider1Image.Position = new Point(415, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider1Image);
      this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider2Image.Position = new Point(549, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider2Image);
      InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsPanel_Faction_Forum", "Faction Forum"));
      this.newTopicButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      this.newTopicButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      this.newTopicButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      this.newTopicButton.Position = new Point(20, height - 30);
      this.newTopicButton.Text.Text = SK.Text("FORUMS_New_Topic", "New Topic");
      this.newTopicButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.newTopicButton.TextYOffset = -3;
      this.newTopicButton.Text.Color = ARGBColors.Black;
      this.newTopicButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.newTopicClick), "FactionNewForumPanel_new_topic");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.newTopicButton);
      this.createForumButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
      this.createForumButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
      this.createForumButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
      this.createForumButton.Position = new Point(330, height - 30);
      this.createForumButton.Text.Text = SK.Text("FORUMS_Create_New_Sub_Forum", "Create New Sub Forum");
      this.createForumButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.createForumButton.TextYOffset = -3;
      this.createForumButton.Text.Color = ARGBColors.Black;
      this.createForumButton.Visible = false;
      this.createForumButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.createForumClick), "FactionNewForumPanel_create_forum");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.createForumButton);
      this.deleteForumButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
      this.deleteForumButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
      this.deleteForumButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
      this.deleteForumButton.Position = new Point(560, height - 30);
      this.deleteForumButton.Text.Text = SK.Text("FORUMS_Delete_Sub_Forum", "Delete Sub Forum");
      this.deleteForumButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.deleteForumButton.TextYOffset = -3;
      this.deleteForumButton.Text.Color = ARGBColors.Black;
      this.deleteForumButton.Visible = false;
      this.deleteForumButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteForumClicked), "FactionNewForumPanel_delete_forum");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.deleteForumButton);
      this.threadTitleLabel.Text = SK.Text("FactionInvites_Thread_Title", "Thread Title");
      this.threadTitleLabel.Color = ARGBColors.Black;
      this.threadTitleLabel.Position = new Point(9, -2);
      this.threadTitleLabel.Size = new Size(323, this.headerLabelsImage.Height);
      this.threadTitleLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.threadTitleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.threadTitleLabel);
      this.playersLabel.Text = SK.Text("VillageMapPanel_Player", "Player");
      this.playersLabel.Color = ARGBColors.Black;
      this.playersLabel.Position = new Point(420, -2);
      this.playersLabel.Size = new Size(140, this.headerLabelsImage.Height);
      this.playersLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.playersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.playersLabel);
      this.dateLabel.Text = SK.Text("FactionInvites_Last_Post_Date", "Last Post Date");
      this.dateLabel.Color = ARGBColors.Black;
      this.dateLabel.Position = new Point(554, -2);
      this.dateLabel.Size = new Size(160, this.headerLabelsImage.Height);
      this.dateLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.dateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.dateLabel);
      this.wallScrollArea.Position = new Point(25, 98);
      this.wallScrollArea.Size = new Size(705, height - 140);
      this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(705, height - 50 - 90));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollArea);
      this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
      this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
      int num = this.wallScrollBar.Value;
      this.wallScrollBar.Visible = false;
      this.wallScrollBar.Position = new Point(733, 98);
      this.wallScrollBar.Size = new Size(24, height - 140);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollBar);
      this.wallScrollBar.Value = 0;
      this.wallScrollBar.Max = 100;
      this.wallScrollBar.NumVisibleLines = 25;
      this.wallScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      if (!resized)
      {
        this.selectedAreaID = RemoteServices.Instance.UserFactionID;
        this.selectedAreaType = 5;
        bool flag = false;
        foreach (FactionNewForumPanel.ForumData forum in this.forumArray)
        {
          if (forum.areaID == this.selectedAreaID && forum.areaType == this.selectedAreaType)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          RemoteServices.Instance.set_GetForumList_UserCallBack(new RemoteServices.GetForumList_UserCallBack(this.getForumListCallback));
          RemoteServices.Instance.GetForumList(this.selectedAreaID, this.selectedAreaType);
        }
        else
        {
          if ((DateTime.Now - this.lastRefreshTime).TotalMinutes > 5.0)
          {
            RemoteServices.Instance.set_GetForumList_UserCallBack(new RemoteServices.GetForumList_UserCallBack(this.getForumListCallback));
            RemoteServices.Instance.GetForumList(this.selectedAreaID, this.selectedAreaType);
          }
          this.initForum();
        }
      }
      else
        this.initForum();
    }

    public void update() => this.sidebar.update();

    public void logout()
    {
    }

    public void getForumListCallback(GetForumList_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      foreach (ForumInfo forumInfo in returnData.forumInfo)
      {
        FactionNewForumPanel.ForumData forumData = new FactionNewForumPanel.ForumData()
        {
          areaID = forumInfo.areaID,
          areaType = forumInfo.areaType,
          forumID = forumInfo.forumID,
          forumTitle = forumInfo.forumTitle,
          lastTime = forumInfo.lastDate,
          numPosts = forumInfo.numPosts,
          numReadPosts = forumInfo.numReadPosts
        };
        this.forumArray[forumData.forumID] = (object) forumData;
      }
      this.lastRefreshTime = DateTime.Now;
      this.initForum();
    }

    public void initForum()
    {
      this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.forum1Button);
      this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.forum2Button);
      this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.forum3Button);
      this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.forum4Button);
      this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.forum5Button);
      this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.forum6Button);
      this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.forum7Button);
      List<FactionNewForumPanel.ForumData> forumDataList = new List<FactionNewForumPanel.ForumData>();
      foreach (FactionNewForumPanel.ForumData forum in this.forumArray)
      {
        if (forum.areaID == RemoteServices.Instance.UserFactionID && forum.areaType == 5)
          forumDataList.Add(forum);
      }
      forumDataList.Sort((IComparer<FactionNewForumPanel.ForumData>) this.forumComparer);
      bool flag = false;
      foreach (FactionNewForumPanel.ForumData forumData in forumDataList)
      {
        if (forumData.forumID == this.currentlySelectedForum)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        this.currentlySelectedForum = forumDataList.Count <= 0 ? -1L : forumDataList[0].forumID;
      int count = forumDataList.Count;
      int[] numArray = (int[]) null;
      switch (count - 1)
      {
        case 0:
          numArray = this.forums1Positions;
          break;
        case 1:
          numArray = this.forums2Positions;
          break;
        case 2:
          numArray = this.forums3Positions;
          break;
        case 3:
          numArray = this.forums4Positions;
          break;
        case 4:
          numArray = this.forums5Positions;
          break;
        case 5:
          numArray = this.forums6Positions;
          break;
        case 6:
          numArray = this.forums7Positions;
          break;
      }
      if (count >= 1)
      {
        this.forum1Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.forum1Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.forum1Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.forum1Button.Position = new Point(numArray[0], numArray[1]);
        this.forum1Button.Text.Text = SK.Text("FORUMS_General", "General");
        this.forum1Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.forum1Button.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.forum1Button.TextYOffset = -3;
        this.forum1Button.Text.Color = ARGBColors.Black;
        this.forum1Button.Text.clearDropShadow();
        this.forum1Button.DataL = forumDataList[0].forumID;
        this.forum1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumSelectedClick), "FactionNewForumPanel_change_forum");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.forum1Button);
        this.forum1Button.Active = true;
        if (this.forum1Button.DataL == this.currentlySelectedForum)
        {
          this.forum1Button.Active = false;
          this.forum1Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.forum1Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.forum1Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsSidebar_Forum", "Forum") + " : " + this.forum1Button.Text.Text);
          this.forum1Button.Text.Color = ARGBColors.White;
          this.forum1Button.Text.DropShadowColor = ARGBColors.Black;
          this.OrigForumName = forumDataList[0].forumTitle;
        }
      }
      if (count >= 2)
      {
        this.forum2Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.forum2Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.forum2Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.forum2Button.Position = new Point(numArray[2], numArray[3]);
        this.forum2Button.Text.Text = forumDataList[1].forumTitle;
        this.forum2Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.forum2Button.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.forum2Button.TextYOffset = -3;
        this.forum2Button.Text.Color = ARGBColors.Black;
        this.forum2Button.Text.clearDropShadow();
        this.forum2Button.DataL = forumDataList[1].forumID;
        this.forum2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumSelectedClick), "FactionNewForumPanel_change_forum");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.forum2Button);
        if (this.forum2Button.DataL == this.currentlySelectedForum)
        {
          this.forum2Button.Active = false;
          this.forum2Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.forum2Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.forum2Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsSidebar_Forum", "Forum") + " : " + this.forum2Button.Text.Text);
          this.forum2Button.Text.Color = ARGBColors.White;
          this.forum2Button.Text.DropShadowColor = ARGBColors.Black;
          this.OrigForumName = forumDataList[1].forumTitle;
        }
      }
      if (count >= 3)
      {
        this.forum3Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.forum3Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.forum3Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.forum3Button.Position = new Point(numArray[4], numArray[5]);
        this.forum3Button.Text.Text = forumDataList[2].forumTitle;
        this.forum3Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.forum3Button.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.forum3Button.TextYOffset = -3;
        this.forum3Button.Text.Color = ARGBColors.Black;
        this.forum3Button.Text.clearDropShadow();
        this.forum3Button.DataL = forumDataList[2].forumID;
        this.forum3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumSelectedClick), "FactionNewForumPanel_change_forum");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.forum3Button);
        if (this.forum3Button.DataL == this.currentlySelectedForum)
        {
          this.forum3Button.Active = false;
          this.forum3Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.forum3Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.forum3Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsSidebar_Forum", "Forum") + " : " + this.forum3Button.Text.Text);
          this.forum3Button.Text.Color = ARGBColors.White;
          this.forum3Button.Text.DropShadowColor = ARGBColors.Black;
          this.OrigForumName = forumDataList[2].forumTitle;
        }
      }
      if (count >= 4)
      {
        this.forum4Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.forum4Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.forum4Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.forum4Button.Position = new Point(numArray[6], numArray[7]);
        this.forum4Button.Text.Text = forumDataList[3].forumTitle;
        this.forum4Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.forum4Button.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.forum4Button.TextYOffset = -3;
        this.forum4Button.Text.Color = ARGBColors.Black;
        this.forum4Button.Text.clearDropShadow();
        this.forum4Button.DataL = forumDataList[3].forumID;
        this.forum4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumSelectedClick), "FactionNewForumPanel_change_forum");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.forum4Button);
        if (this.forum4Button.DataL == this.currentlySelectedForum)
        {
          this.forum4Button.Active = false;
          this.forum4Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.forum4Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.forum4Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsSidebar_Forum", "Forum") + " : " + this.forum4Button.Text.Text);
          this.forum4Button.Text.Color = ARGBColors.White;
          this.forum4Button.Text.DropShadowColor = ARGBColors.Black;
          this.OrigForumName = forumDataList[3].forumTitle;
        }
      }
      if (count >= 5)
      {
        this.forum5Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.forum5Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.forum5Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.forum5Button.Position = new Point(numArray[8], numArray[9]);
        this.forum5Button.Text.Text = forumDataList[4].forumTitle;
        this.forum5Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.forum5Button.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.forum5Button.TextYOffset = -3;
        this.forum5Button.Text.Color = ARGBColors.Black;
        this.forum5Button.Text.clearDropShadow();
        this.forum5Button.DataL = forumDataList[4].forumID;
        this.forum5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumSelectedClick), "FactionNewForumPanel_change_forum");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.forum5Button);
        if (this.forum5Button.DataL == this.currentlySelectedForum)
        {
          this.forum5Button.Active = false;
          this.forum5Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.forum5Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.forum5Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsSidebar_Forum", "Forum") + " : " + this.forum5Button.Text.Text);
          this.forum5Button.Text.Color = ARGBColors.White;
          this.forum5Button.Text.DropShadowColor = ARGBColors.Black;
          this.OrigForumName = forumDataList[4].forumTitle;
        }
      }
      if (count >= 6)
      {
        this.forum6Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.forum6Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.forum6Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.forum6Button.Position = new Point(numArray[10], numArray[11]);
        this.forum6Button.Text.Text = forumDataList[5].forumTitle;
        this.forum6Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.forum6Button.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.forum6Button.TextYOffset = -3;
        this.forum6Button.Text.Color = ARGBColors.Black;
        this.forum6Button.Text.clearDropShadow();
        this.forum6Button.DataL = forumDataList[5].forumID;
        this.forum6Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumSelectedClick), "FactionNewForumPanel_change_forum");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.forum6Button);
        if (this.forum6Button.DataL == this.currentlySelectedForum)
        {
          this.forum6Button.Active = false;
          this.forum6Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.forum6Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.forum6Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsSidebar_Forum", "Forum") + " : " + this.forum6Button.Text.Text);
          this.forum6Button.Text.Color = ARGBColors.White;
          this.forum6Button.Text.DropShadowColor = ARGBColors.Black;
          this.OrigForumName = forumDataList[5].forumTitle;
        }
      }
      if (count >= 7)
      {
        this.forum7Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.forum7Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.forum7Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.forum7Button.Position = new Point(numArray[12], numArray[13]);
        this.forum7Button.Text.Text = forumDataList[6].forumTitle;
        this.forum7Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.forum7Button.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.forum7Button.TextYOffset = -3;
        this.forum7Button.Text.Color = ARGBColors.Black;
        this.forum7Button.Text.clearDropShadow();
        this.forum7Button.DataL = forumDataList[6].forumID;
        this.forum7Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumSelectedClick), "FactionNewForumPanel_change_forum");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.forum7Button);
        if (this.forum7Button.DataL == this.currentlySelectedForum)
        {
          this.forum7Button.Active = false;
          this.forum7Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.forum7Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.forum7Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsSidebar_Forum", "Forum") + " : " + this.forum7Button.Text.Text);
          this.forum7Button.Text.Color = ARGBColors.White;
          this.forum7Button.Text.DropShadowColor = ARGBColors.Black;
          this.OrigForumName = forumDataList[6].forumTitle;
        }
      }
      if (this.currentlySelectedForum >= 0L)
      {
        switch (GameEngine.Instance.World.getYourFactionRank())
        {
          case 1:
          case 2:
            if (count < GameEngine.Instance.LocalWorldData.Faction_MaxUserForums + 1)
              this.createForumButton.Visible = true;
            else
              this.createForumButton.Visible = false;
            if (this.forum1Button.DataL != this.currentlySelectedForum)
            {
              this.deleteForumButton.Visible = true;
              break;
            }
            this.deleteForumButton.Visible = false;
            break;
          default:
            this.createForumButton.Visible = false;
            this.deleteForumButton.Visible = false;
            break;
        }
        RemoteServices.Instance.set_GetForumThreadList_UserCallBack(new RemoteServices.GetForumThreadList_UserCallBack(this.forumThreadListCallback));
        if (this.forumThreadArray[this.currentlySelectedForum] == null || ((FactionNewForumPanel.ForumInfoData) this.forumThreadArray[this.currentlySelectedForum]).lastTime.Year < 1900)
          RemoteServices.Instance.GetForumThreadList(this.currentlySelectedForum, DateTime.MinValue, true);
        else if ((DateTime.Now - ((FactionNewForumPanel.ForumInfoData) this.forumThreadArray[this.currentlySelectedForum]).lastTime).TotalMinutes > 1.0)
          RemoteServices.Instance.GetForumThreadList(this.currentlySelectedForum, ((FactionNewForumPanel.ForumInfoData) this.forumThreadArray[this.currentlySelectedForum]).lastTime, false);
      }
      this.updateForum();
      this.mainBackgroundImage.invalidate();
    }

    private void forumSelectedClick()
    {
      long dataL = this.ClickedControl.DataL;
      if (dataL == this.currentlySelectedForum)
        return;
      this.currentlySelectedForum = dataL;
      this.initForum();
    }

    private void newTopicClick()
    {
      if (this.m_popup != null && this.m_popup.Created)
        return;
      this.m_popup = new FactionNewTopicPopup();
      this.m_popup.init(this.currentlySelectedForum, (IForumPostParent) this);
      this.m_popup.Show();
    }

    public void newTopic(long forumID, string heading, string body)
    {
      RemoteServices.Instance.set_NewForumThread_UserCallBack(new RemoteServices.NewForumThread_UserCallBack(this.newForumThreadCallback));
      RemoteServices.Instance.NewForumThread(forumID, heading, body);
    }

    public void newForumThreadCallback(NewForumThread_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      if (returnData.forumThreadInfo != null && returnData.forumThreadInfo.Count > 0 && RemoteServices.Instance.UserOptions.profanityFilter)
      {
        foreach (ForumThreadInfo forumThreadInfo in returnData.forumThreadInfo)
          forumThreadInfo.threadTitle = GameEngine.Instance.censorString(forumThreadInfo.threadTitle);
      }
      this.importThreadList(returnData.forumThreadInfo, returnData.forumID);
    }

    private void createForumClick()
    {
      if (this.m_forumPopup != null && this.m_forumPopup.Created)
        return;
      this.m_forumPopup = new FactionNewForumPopup();
      this.m_forumPopup.init(this);
      this.m_forumPopup.Show();
      this.m_forumPopup.setFocus();
    }

    public void createNewForum(string forumName)
    {
      RemoteServices.Instance.set_CreateForum_UserCallBack(new RemoteServices.CreateForum_UserCallBack(this.createForumCallback));
      RemoteServices.Instance.CreateForum(this.selectedAreaID, this.selectedAreaType, forumName);
    }

    public void createForumCallback(CreateForum_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      if (returnData.forumInfo != null)
      {
        FactionNewForumPanel.ForumData forumData = new FactionNewForumPanel.ForumData()
        {
          areaID = returnData.forumInfo.areaID,
          areaType = returnData.forumInfo.areaType,
          forumID = returnData.forumInfo.forumID,
          forumTitle = returnData.forumInfo.forumTitle,
          lastTime = returnData.forumInfo.lastDate,
          numPosts = returnData.forumInfo.numPosts,
          numReadPosts = returnData.forumInfo.numReadPosts
        };
        this.forumArray[forumData.forumID] = (object) forumData;
      }
      this.initForum();
    }

    private void PopUpOkClick()
    {
      RemoteServices.Instance.set_DeleteForum_UserCallBack(new RemoteServices.DeleteForum_UserCallBack(this.deleteForumCallback));
      RemoteServices.Instance.DeleteForum(this.selectedAreaID, 5, this.currentlySelectedForum);
      InterfaceMgr.Instance.closeGreyOut();
      this.deletePostPopUp.Close();
    }

    private void ClosePopUp()
    {
      if (this.deletePostPopUp == null)
        return;
      if (this.deletePostPopUp.Created)
        this.deletePostPopUp.Close();
      InterfaceMgr.Instance.closeGreyOut();
      this.deletePostPopUp = (MyMessageBoxPopUp) null;
    }

    private void deleteForumClicked()
    {
      this.ClosePopUp();
      InterfaceMgr.Instance.openGreyOutWindow(false);
      this.deletePostPopUp = new MyMessageBoxPopUp();
      this.deletePostPopUp.init(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FORUMS_Delete_Sub_Forum", "Delete Sub Forum"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.PopUpOkClick));
      this.deletePostPopUp.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
    }

    public void deleteForumCallback(DeleteForum_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      if (returnData.forumID >= 0L)
        this.forumArray[returnData.forumID] = (object) null;
      this.currentlySelectedForum = -1L;
      this.initForum();
    }

    public void forumThreadListCallback(GetForumThreadList_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      if (returnData.forumThreadInfo != null && returnData.forumThreadInfo.Count > 0 && RemoteServices.Instance.UserOptions.profanityFilter)
      {
        foreach (ForumThreadInfo forumThreadInfo in returnData.forumThreadInfo)
          forumThreadInfo.threadTitle = GameEngine.Instance.censorString(forumThreadInfo.threadTitle);
      }
      this.importThreadList(returnData.forumThreadInfo, returnData.forumID);
    }

    public void importThreadList(List<ForumThreadInfo> threadData, long forumID)
    {
      if (threadData == null)
        return;
      if (this.forumThreadArray[forumID] == null)
        this.forumThreadArray[forumID] = (object) new FactionNewForumPanel.ForumInfoData()
        {
          forumID = forumID
        };
      FactionNewForumPanel.ForumInfoData forumThread = (FactionNewForumPanel.ForumInfoData) this.forumThreadArray[forumID];
      foreach (ForumThreadInfo forumThreadInfo in threadData)
      {
        FactionNewForumPanel.ForumThreadData forumThreadData = new FactionNewForumPanel.ForumThreadData();
        forumThreadData.title = forumThreadInfo.threadTitle;
        forumThreadData.threadID = forumThreadInfo.threadID;
        forumThreadData.lastTime = forumThreadInfo.lastDate;
        forumThreadData.userName = forumThreadInfo.userName;
        forumThreadData.read = forumThreadInfo.threadRead;
        if (forumThreadData.lastTime > forumThread.lastTime)
          forumThread.lastTime = forumThreadData.lastTime;
        bool flag = false;
        for (int index = 0; index < forumThread.forumThreads.Count; ++index)
        {
          if (forumThread.forumThreads[index].threadID == forumThreadData.threadID)
          {
            forumThread.forumThreads[index] = forumThreadData;
            flag = true;
            break;
          }
        }
        if (!flag)
          forumThread.forumThreads.Add(forumThreadData);
      }
      forumThread.forumThreads.Sort((IComparer<FactionNewForumPanel.ForumThreadData>) this.threadComparer);
      this.updateForum();
    }

    private void wallScrollBarMoved()
    {
      int y = this.wallScrollBar.Value;
      this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 98 - y);
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

    public void updateForum()
    {
      this.wallScrollArea.clearControls();
      int num = 0;
      this.lineList.Clear();
      if (this.currentlySelectedForum >= 0L)
      {
        FactionNewForumPanel.ForumInfoData forumThread1 = (FactionNewForumPanel.ForumInfoData) this.forumThreadArray[this.currentlySelectedForum];
        if (forumThread1 != null && forumThread1.forumThreads != null)
        {
          int position = 0;
          foreach (FactionNewForumPanel.ForumThreadData forumThread2 in forumThread1.forumThreads)
          {
            FactionNewForumPanel.ForumThreadLine control = new FactionNewForumPanel.ForumThreadLine();
            if (num != 0)
              num += 5;
            control.Position = new Point(0, num);
            control.init(forumThread2, position, this);
            this.wallScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
            num += control.Height;
            this.lineList.Add(control);
            ++position;
          }
        }
      }
      this.wallScrollArea.Size = new Size(this.wallScrollArea.Width, num);
      if (num < this.wallScrollBar.Height)
      {
        this.wallScrollBar.Visible = false;
      }
      else
      {
        this.wallScrollBar.Visible = true;
        this.wallScrollBar.NumVisibleLines = this.wallScrollBar.Height;
        this.wallScrollBar.Max = num - this.wallScrollBar.Height;
      }
      this.mainBackgroundImage.invalidate();
    }

    public void deleteTopic(long threadID)
    {
      RemoteServices.Instance.set_DeleteForumThread_UserCallBack(new RemoteServices.DeleteForumThread_UserCallBack(this.deleteForumThreadCallback));
      RemoteServices.Instance.DeleteForumThread(this.selectedAreaID, 5, this.OrigForumName, this.currentlySelectedForum, threadID);
    }

    public void deleteForumThreadCallback(DeleteForumThread_ReturnType returnData)
    {
      if (!returnData.Success || returnData.threadID < 0L)
        return;
      FactionNewForumPanel.ForumInfoData forumThread1 = (FactionNewForumPanel.ForumInfoData) this.forumThreadArray[returnData.forumID];
      if (forumThread1 == null)
        return;
      foreach (FactionNewForumPanel.ForumThreadData forumThread2 in forumThread1.forumThreads)
      {
        if (forumThread2.threadID == returnData.threadID)
        {
          forumThread1.forumThreads.Remove(forumThread2);
          this.updateForum();
          break;
        }
      }
    }

    public class ForumData
    {
      public int areaID = -1;
      public int areaType = -1;
      public long forumID = -1;
      public string forumTitle = "";
      public int numPosts;
      public int numReadPosts;
      public DateTime lastTime = DateTime.MinValue;
    }

    public class ForumThreadData
    {
      public string title = "";
      public long threadID = -1;
      public DateTime lastTime = DateTime.MinValue;
      public string userName = "";
      public bool read;
    }

    public class ForumInfoData
    {
      public long forumID = -1;
      public List<FactionNewForumPanel.ForumThreadData> forumThreads = new List<FactionNewForumPanel.ForumThreadData>();
      public DateTime lastTime = DateTime.MinValue;
    }

    public class ForumThreadLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel threadTitleLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel userLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDButton deleteThread = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDLabel dateLabel = new CustomSelfDrawPanel.CSDLabel();
      private int m_position = -1000;
      private FactionNewForumPanel.ForumThreadData m_ForumThreadData;
      private FactionNewForumPanel m_parent;
      private MyMessageBoxPopUp PopUp;

      public void init(
        FactionNewForumPanel.ForumThreadData threadData,
        int position,
        FactionNewForumPanel parent)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.m_ForumThreadData = threadData;
        this.ClipVisible = true;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_02_dark : (Image) GFXLibrary.lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        FontStyle style = FontStyle.Regular;
        if (!threadData.read)
          style = FontStyle.Bold;
        this.threadTitleLabel.Text = threadData.title;
        this.threadTitleLabel.Color = ARGBColors.Black;
        this.threadTitleLabel.Position = new Point(8, 0);
        this.threadTitleLabel.Size = new Size(410, this.backgroundImage.Height);
        this.threadTitleLabel.Font = FontManager.GetFont("Arial", 9f, style);
        this.threadTitleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.threadTitleLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.threadTitleLabel);
        this.userLabel.Text = threadData.userName;
        this.userLabel.Color = ARGBColors.Black;
        this.userLabel.Position = new Point(420, 0);
        this.userLabel.Size = new Size(134, this.backgroundImage.Height);
        this.userLabel.Font = FontManager.GetFont("Arial", 9f, style);
        this.userLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.userLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.userLabel);
        this.dateLabel.Text = threadData.lastTime.ToShortTimeString() + " " + threadData.lastTime.ToShortDateString();
        this.dateLabel.Color = ARGBColors.Black;
        this.dateLabel.Position = new Point(554, 0);
        this.dateLabel.Size = new Size(171, this.backgroundImage.Height);
        this.dateLabel.Font = FontManager.GetFont("Arial", 9f, style);
        this.dateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.dateLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.dateLabel);
        this.invalidate();
        switch (GameEngine.Instance.World.getYourFactionRank())
        {
          case 1:
          case 2:
            this.deleteThread.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.deleteThread.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.deleteThread.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.deleteThread.Position = new Point(680, 4);
            this.deleteThread.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteClicked), "FactionNewForumPanel_delete_thread");
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.deleteThread);
            break;
          default:
            if (!RemoteServices.Instance.Admin && !RemoteServices.Instance.Moderator)
              break;
            goto case 1;
        }
      }

      public void update()
      {
      }

      public void lineClicked()
      {
        GameEngine.Instance.playInterfaceSound("FactionNewForumPanel_thread_clicked");
        InterfaceMgr.Instance.showFactionForumPosts(this.m_ForumThreadData.threadID, this.m_parent.currentlySelectedForum, this.m_ForumThreadData.title, SK.Text("FactionsPanel_Faction_Forum", "Faction Forum"));
      }

      private void PopUpOkClick()
      {
        if (this.m_parent != null)
          this.m_parent.deleteTopic(this.m_ForumThreadData.threadID);
        InterfaceMgr.Instance.closeGreyOut();
        this.PopUp.Close();
      }

      private void ClosePopUp()
      {
        if (this.PopUp == null)
          return;
        if (this.PopUp.Created)
          this.PopUp.Close();
        InterfaceMgr.Instance.closeGreyOut();
        this.PopUp = (MyMessageBoxPopUp) null;
      }

      private void deleteClicked()
      {
        this.ClosePopUp();
        InterfaceMgr.Instance.openGreyOutWindow(false);
        this.PopUp = new MyMessageBoxPopUp();
        this.PopUp.init(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FORUMS_Delete_Topic", "Delete This Topic"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.PopUpOkClick));
        this.PopUp.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
      }
    }

    public class ForumComparer : IComparer<FactionNewForumPanel.ForumData>
    {
      public int Compare(FactionNewForumPanel.ForumData x, FactionNewForumPanel.ForumData y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null || x.forumID > y.forumID)
          return 1;
        return x.forumID < y.forumID ? -1 : 0;
      }
    }

    public class ThreadComparer : IComparer<FactionNewForumPanel.ForumThreadData>
    {
      public int Compare(
        FactionNewForumPanel.ForumThreadData x,
        FactionNewForumPanel.ForumThreadData y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null || x.lastTime < y.lastTime)
          return 1;
        return x.lastTime > y.lastTime ? -1 : 0;
      }
    }
  }
}
