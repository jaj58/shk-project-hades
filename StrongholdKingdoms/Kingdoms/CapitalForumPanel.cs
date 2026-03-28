// Decompiled with JetBrains decompiler
// Type: Kingdoms.CapitalForumPanel
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
  public class CapitalForumPanel : CustomSelfDrawPanel, IDockableControl, IForumPostParent
  {
    public const int PANEL_ID = 45;
    public static CapitalForumPanel instance;
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
    private CustomSelfDrawPanel.CSDButton newTopicButton = new CustomSelfDrawPanel.CSDButton();
    public string titleForum = "";
    private long currentlySelectedForum = -1;
    private FactionNewTopicPopup m_popup;
    private List<CapitalForumPanel.ForumThreadLine> lineList = new List<CapitalForumPanel.ForumThreadLine>();
    public CapitalForumPanel.ForumComparer forumComparer = new CapitalForumPanel.ForumComparer();
    private CapitalForumPanel.ThreadComparer threadComparer = new CapitalForumPanel.ThreadComparer();
    private DockableControl dockableControl;
    private IContainer components;

    public void clearForum()
    {
      this.forumArray = new SparseArray();
      this.lastRefreshTime = DateTime.MinValue;
      this.forumThreadArray = new SparseArray();
      this.currentlySelectedForum = -1L;
    }

    public CapitalForumPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void setArea(int parishID, int areaType)
    {
      this.selectedAreaID = parishID;
      this.selectedAreaType = areaType;
    }

    public void init(bool resized)
    {
      int height = this.Height;
      CapitalForumPanel.instance = this;
      this.clearControls();
      this.mainBackgroundImage.FillColor = Color.FromArgb(134, 153, 165);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = new Size(this.Width, height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundFade.Image = (Image) GFXLibrary.background_top;
      this.backgroundFade.Position = new Point(0, 0);
      this.backgroundFade.Size = new Size(this.Width, this.backgroundFade.Image.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundFade);
      this.headerLabelsImage.Size = new Size(this.Width - 25 - 23, 28);
      this.headerLabelsImage.Position = new Point(25, 9);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider1Image.Position = new Point(415, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider1Image);
      this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider2Image.Position = new Point(549, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider2Image);
      if (this.selectedAreaType == 3)
        this.titleForum = SK.Text("ParishForumPanel_Parish_Forum", "Parish Forum");
      if (this.selectedAreaType == 2)
        this.titleForum = SK.Text("ParishForumPanel_County_Forum", "County Forum");
      if (this.selectedAreaType == 1)
        this.titleForum = SK.Text("ParishForumPanel_Province_Forum", "Province Forum");
      if (this.selectedAreaType == 0)
        this.titleForum = SK.Text("ParishForumPanel_Country_Forum", "Country Forum");
      InterfaceMgr.Instance.setVillageHeading(this.titleForum);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage, 16, new Point(this.Width - 30 + 2, 7), true);
      this.newTopicButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      this.newTopicButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      this.newTopicButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      this.newTopicButton.Position = new Point(20, height - 30);
      this.newTopicButton.Text.Text = SK.Text("FORUMS_New_Topic", "New Topic");
      this.newTopicButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.newTopicButton.TextYOffset = -3;
      this.newTopicButton.Text.Color = ARGBColors.Black;
      this.newTopicButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.newTopicClick), "CapitalForumPanel_new_topic");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.newTopicButton);
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
      this.wallScrollArea.Position = new Point(25, 38);
      this.wallScrollArea.Size = new Size(915, height - 80);
      this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(915, height - 50 - 90 + 60));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollArea);
      this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
      this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
      int num = this.wallScrollBar.Value;
      this.wallScrollBar.Visible = false;
      this.wallScrollBar.Position = new Point(943, 38);
      this.wallScrollBar.Size = new Size(24, height - 80);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollBar);
      this.wallScrollBar.Value = 0;
      this.wallScrollBar.Max = 100;
      this.wallScrollBar.NumVisibleLines = 25;
      this.wallScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      if (!resized)
      {
        bool flag = false;
        foreach (CapitalForumPanel.ForumData forum in this.forumArray)
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

    public void update()
    {
    }

    public void logout()
    {
    }

    public void getForumListCallback(GetForumList_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      foreach (ForumInfo forumInfo in returnData.forumInfo)
      {
        CapitalForumPanel.ForumData forumData = new CapitalForumPanel.ForumData()
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
      List<CapitalForumPanel.ForumData> forumDataList = new List<CapitalForumPanel.ForumData>();
      foreach (CapitalForumPanel.ForumData forum in this.forumArray)
      {
        if (forum.areaID == this.selectedAreaID && forum.areaType == this.selectedAreaType)
          forumDataList.Add(forum);
      }
      forumDataList.Sort((IComparer<CapitalForumPanel.ForumData>) this.forumComparer);
      bool flag = false;
      foreach (CapitalForumPanel.ForumData forumData in forumDataList)
      {
        if (forumData.forumID == this.currentlySelectedForum)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        this.currentlySelectedForum = forumDataList.Count <= 0 ? -1L : forumDataList[0].forumID;
      if (this.currentlySelectedForum >= 0L)
      {
        RemoteServices.Instance.set_GetForumThreadList_UserCallBack(new RemoteServices.GetForumThreadList_UserCallBack(this.forumThreadListCallback));
        if (this.forumThreadArray[this.currentlySelectedForum] == null || ((CapitalForumPanel.ForumInfoData) this.forumThreadArray[this.currentlySelectedForum]).lastTime.Year < 1900)
          RemoteServices.Instance.GetForumThreadList(this.currentlySelectedForum, DateTime.MinValue, true);
        else if ((DateTime.Now - ((CapitalForumPanel.ForumInfoData) this.forumThreadArray[this.currentlySelectedForum]).lastTime).TotalMinutes > 1.0)
          RemoteServices.Instance.GetForumThreadList(this.currentlySelectedForum, ((CapitalForumPanel.ForumInfoData) this.forumThreadArray[this.currentlySelectedForum]).lastTime, false);
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
        this.forumThreadArray[forumID] = (object) new CapitalForumPanel.ForumInfoData()
        {
          forumID = forumID
        };
      CapitalForumPanel.ForumInfoData forumThread = (CapitalForumPanel.ForumInfoData) this.forumThreadArray[forumID];
      foreach (ForumThreadInfo forumThreadInfo in threadData)
      {
        CapitalForumPanel.ForumThreadData forumThreadData = new CapitalForumPanel.ForumThreadData();
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
      forumThread.forumThreads.Sort((IComparer<CapitalForumPanel.ForumThreadData>) this.threadComparer);
      this.updateForum();
    }

    private void wallScrollBarMoved()
    {
      int y = this.wallScrollBar.Value;
      this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 38 - y);
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
        CapitalForumPanel.ForumInfoData forumThread1 = (CapitalForumPanel.ForumInfoData) this.forumThreadArray[this.currentlySelectedForum];
        if (forumThread1 != null && forumThread1.forumThreads != null)
        {
          int position = 0;
          foreach (CapitalForumPanel.ForumThreadData forumThread2 in forumThread1.forumThreads)
          {
            CapitalForumPanel.ForumThreadLine control = new CapitalForumPanel.ForumThreadLine();
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
      RemoteServices.Instance.DeleteForumThread(this.selectedAreaID, this.selectedAreaType, this.OrigForumName, this.currentlySelectedForum, threadID);
    }

    public void deleteForumThreadCallback(DeleteForumThread_ReturnType returnData)
    {
      if (!returnData.Success || returnData.threadID < 0L)
        return;
      CapitalForumPanel.ForumInfoData forumThread1 = (CapitalForumPanel.ForumInfoData) this.forumThreadArray[returnData.forumID];
      if (forumThread1 == null)
        return;
      foreach (CapitalForumPanel.ForumThreadData forumThread2 in forumThread1.forumThreads)
      {
        if (forumThread2.threadID == returnData.threadID)
        {
          forumThread1.forumThreads.Remove(forumThread2);
          this.updateForum();
          break;
        }
      }
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
      this.SuspendLayout();
      this.AutoScaleMode = AutoScaleMode.None;
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (CapitalForumPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
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
      public List<CapitalForumPanel.ForumThreadData> forumThreads = new List<CapitalForumPanel.ForumThreadData>();
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
      private CapitalForumPanel.ForumThreadData m_ForumThreadData;
      private CapitalForumPanel m_parent;
      private MyMessageBoxPopUp deletePostPopUp;

      public void init(
        CapitalForumPanel.ForumThreadData threadData,
        int position,
        CapitalForumPanel parent)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.m_ForumThreadData = threadData;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_02_dark : (Image) GFXLibrary.lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = new Size(900, this.backgroundImage.Size.Height);
        this.ClipVisible = true;
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
        if (!RemoteServices.Instance.Admin && !RemoteServices.Instance.Moderator)
          return;
        this.deleteThread.ImageNorm = (Image) GFXLibrary.trashcan_normal;
        this.deleteThread.ImageOver = (Image) GFXLibrary.trashcan_over;
        this.deleteThread.ImageClick = (Image) GFXLibrary.trashcan_clicked;
        this.deleteThread.Position = new Point(870, 4);
        this.deleteThread.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteClicked), "CapitalForumPanel_delete");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.deleteThread);
      }

      public void update()
      {
      }

      public void lineClicked()
      {
        GameEngine.Instance.playInterfaceSound("CapitalForumPanel_thread");
        InterfaceMgr.Instance.showCapitalForumPosts(this.m_ForumThreadData.threadID, this.m_parent.currentlySelectedForum, this.m_ForumThreadData.title, this.m_parent.selectedAreaID, this.m_parent.selectedAreaType, this.m_parent.titleForum);
      }

      private void deleteClicked()
      {
        this.ClosePopUp();
        InterfaceMgr.Instance.openGreyOutWindow(false);
        this.deletePostPopUp = new MyMessageBoxPopUp();
        this.deletePostPopUp.init(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FORUMS_Delete_Topic", "Delete This Topic"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.PopUpOkClick));
        this.deletePostPopUp.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
      }

      private void PopUpOkClick()
      {
        if (this.m_parent != null)
          this.m_parent.deleteTopic(this.m_ForumThreadData.threadID);
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
    }

    public class ForumComparer : IComparer<CapitalForumPanel.ForumData>
    {
      public int Compare(CapitalForumPanel.ForumData x, CapitalForumPanel.ForumData y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null || x.forumID > y.forumID)
          return 1;
        return x.forumID < y.forumID ? -1 : 0;
      }
    }

    public class ThreadComparer : IComparer<CapitalForumPanel.ForumThreadData>
    {
      public int Compare(CapitalForumPanel.ForumThreadData x, CapitalForumPanel.ForumThreadData y)
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
