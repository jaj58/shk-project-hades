// Decompiled with JetBrains decompiler
// Type: Kingdoms.FactionNewForumPostsPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
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
  public class FactionNewForumPostsPanel : CustomSelfDrawPanel, IDockableControl, IForumReplyParent
  {
    public const int PANEL_ID = 48;
    public static FactionNewForumPostsPanel instance = (FactionNewForumPostsPanel) null;
    private SparseArray threadArray = new SparseArray();
    public static long ThreadID = -1;
    public static long parentForumID = -1;
    public static string ThreadTitle = "";
    public static string ForumTitle = "";
    private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel factionLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton newPostButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton backButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
    private FactionNewPostPopup m_popup;
    private List<FactionNewForumPostsPanel.FactionsPostLine> lineList = new List<FactionNewForumPostsPanel.FactionsPostLine>();
    private DockableControl dockableControl;
    private IContainer components;

    public FactionNewForumPostsPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      int height = this.Height;
      FactionNewForumPostsPanel.instance = this;
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
      this.headerLabelsImage.Position = new Point(25, 9);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.factionLabel.Text = FactionNewForumPostsPanel.ThreadTitle;
      this.factionLabel.Color = ARGBColors.Black;
      this.factionLabel.Position = new Point(9, -2);
      this.factionLabel.Size = new Size(this.headerLabelsImage.Width - 18, this.headerLabelsImage.Height);
      this.factionLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.factionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionLabel);
      InterfaceMgr.Instance.setVillageHeading(FactionNewForumPostsPanel.ForumTitle);
      this.wallScrollArea.Position = new Point(25, 38);
      this.wallScrollArea.Size = new Size(705, height - 70);
      this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(705, height - 50 - 20));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollArea);
      this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
      this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
      int num = this.wallScrollBar.Value;
      this.wallScrollBar.Visible = false;
      this.wallScrollBar.Position = new Point(733, 38);
      this.wallScrollBar.Size = new Size(24, height - 70);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollBar);
      this.wallScrollBar.Value = 0;
      this.wallScrollBar.Max = 100;
      this.wallScrollBar.NumVisibleLines = 25;
      this.wallScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      this.newPostButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      this.newPostButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      this.newPostButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      this.newPostButton.Position = new Point(20, height - 30);
      this.newPostButton.Text.Text = SK.Text("FORUMS_New_Post", "New Post");
      this.newPostButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.newPostButton.TextYOffset = -3;
      this.newPostButton.Text.Color = ARGBColors.Black;
      this.newPostButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.newPostClick), "FactionNewForumPostsPanel_new_post");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.newPostButton);
      this.backButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      this.backButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      this.backButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      this.backButton.Position = new Point(630, height - 30);
      this.backButton.Text.Text = SK.Text("FORUMS_Back", "Back");
      this.backButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.backButton.TextYOffset = -3;
      this.backButton.Text.Color = ARGBColors.Black;
      this.backButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.backClick), "FactionNewForumPostsPanel_back");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backButton);
      if (!resized)
      {
        RemoteServices.Instance.set_GetForumThread_UserCallBack(new RemoteServices.GetForumThread_UserCallBack(this.forumThreadCallback));
        if (this.threadArray[FactionNewForumPostsPanel.ThreadID] == null || ((FactionNewForumPostsPanel.ForumThreadInfoData) this.threadArray[FactionNewForumPostsPanel.ThreadID]).lastTime.Year < 1900)
        {
          RemoteServices.Instance.GetForumThread(FactionNewForumPostsPanel.parentForumID, FactionNewForumPostsPanel.ThreadID, DateTime.MinValue, true);
        }
        else
        {
          FactionNewForumPostsPanel.ForumThreadInfoData thread = (FactionNewForumPostsPanel.ForumThreadInfoData) this.threadArray[FactionNewForumPostsPanel.ThreadID];
          RemoteServices.Instance.GetForumThread(FactionNewForumPostsPanel.parentForumID, FactionNewForumPostsPanel.ThreadID, thread.lastTime, false);
        }
      }
      this.addPosts();
    }

    public void update() => this.sidebar.update();

    public void logout() => this.threadArray = new SparseArray();

    private void backClick() => InterfaceMgr.Instance.setVillageTabSubMode(45, false);

    private void newPostClick()
    {
      if (this.m_popup != null && this.m_popup.Created)
        return;
      this.m_popup = new FactionNewPostPopup();
      this.m_popup.init(FactionNewForumPostsPanel.ThreadID, (IForumReplyParent) this, FactionNewForumPostsPanel.ThreadTitle);
      this.m_popup.Show();
    }

    public void newPost(long threadID, string body)
    {
      RemoteServices.Instance.set_PostToForumThread_UserCallBack(new RemoteServices.PostToForumThread_UserCallBack(this.postToForumThreadCallback));
      RemoteServices.Instance.PostToForumThread(threadID, FactionNewForumPostsPanel.parentForumID, body);
    }

    public void postToForumThreadCallback(PostToForumThread_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      if (returnData.forumPosts != null && returnData.forumPosts.Count > 0 && RemoteServices.Instance.UserOptions.profanityFilter)
      {
        foreach (ForumPost forumPost in returnData.forumPosts)
          forumPost.postText = GameEngine.Instance.censorString(forumPost.postText);
      }
      this.importThread(returnData.forumPosts, returnData.threadID);
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

    public void forumThreadCallback(GetForumThread_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      if (returnData.forumPosts != null && returnData.forumPosts.Count > 0 && RemoteServices.Instance.UserOptions.profanityFilter)
      {
        foreach (ForumPost forumPost in returnData.forumPosts)
          forumPost.postText = GameEngine.Instance.censorString(forumPost.postText);
      }
      this.importThread(returnData.forumPosts, returnData.threadID);
    }

    public void importThread(List<ForumPost> posts, long threadID)
    {
      if (posts == null)
        return;
      if (this.threadArray[threadID] == null)
        this.threadArray[threadID] = (object) new FactionNewForumPostsPanel.ForumThreadInfoData()
        {
          threadID = threadID
        };
      FactionNewForumPostsPanel.ForumThreadInfoData thread = (FactionNewForumPostsPanel.ForumThreadInfoData) this.threadArray[threadID];
      thread.forumPosts = new List<FactionNewForumPostsPanel.ForumPostData>();
      foreach (ForumPost post in posts)
      {
        FactionNewForumPostsPanel.ForumPostData forumPostData = new FactionNewForumPostsPanel.ForumPostData();
        forumPostData.text = post.postText;
        forumPostData.postID = post.postID;
        forumPostData.postTime = post.date;
        forumPostData.userName = post.userName;
        forumPostData.userID = post.userID;
        if (forumPostData.postTime > thread.lastTime)
          thread.lastTime = forumPostData.postTime;
        thread.forumPosts.Add(forumPostData);
      }
      this.addPosts();
    }

    public void addPosts()
    {
      this.wallScrollArea.clearControls();
      int num = 0;
      int position = 0;
      this.lineList.Clear();
      int yourFactionRank = GameEngine.Instance.World.getYourFactionRank();
      FactionNewForumPostsPanel.ForumThreadInfoData thread = (FactionNewForumPostsPanel.ForumThreadInfoData) this.threadArray[FactionNewForumPostsPanel.ThreadID];
      if (thread != null)
      {
        foreach (FactionNewForumPostsPanel.ForumPostData forumPost in thread.forumPosts)
        {
          if (num != 0)
            num += 5;
          FactionNewForumPostsPanel.FactionsPostLine control = new FactionNewForumPostsPanel.FactionsPostLine();
          control.Position = new Point(0, num);
          control.init(forumPost, position, this, yourFactionRank);
          this.wallScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
          num += control.Height;
          this.lineList.Add(control);
          ++position;
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
      this.update();
      this.Invalidate();
    }

    public void deletePost(long postID)
    {
      RemoteServices.Instance.set_DeleteForumPost_UserCallBack(new RemoteServices.DeleteForumPost_UserCallBack(this.deleteForumPostCallback));
      RemoteServices.Instance.DeleteForumPost(RemoteServices.Instance.UserFactionID, 5, FactionNewForumPostsPanel.ThreadTitle, FactionNewForumPostsPanel.parentForumID, FactionNewForumPostsPanel.ThreadID, postID);
    }

    public void deleteForumPostCallback(DeleteForumPost_ReturnType returnData)
    {
      if (!returnData.Success || returnData.postID < 0L)
        return;
      FactionNewForumPostsPanel.ForumThreadInfoData thread = (FactionNewForumPostsPanel.ForumThreadInfoData) this.threadArray[FactionNewForumPostsPanel.ThreadID];
      if (thread == null)
        return;
      foreach (FactionNewForumPostsPanel.ForumPostData forumPost in thread.forumPosts)
      {
        if (forumPost.postID == returnData.postID)
        {
          thread.forumPosts.Remove(forumPost);
          this.addPosts();
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
      this.Name = nameof (FactionNewForumPostsPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public class ForumPostData
    {
      public string text = "";
      public long postID = -1;
      public DateTime postTime = DateTime.MinValue;
      public string userName = "";
      public int userID = -1;
    }

    public class ForumThreadInfoData
    {
      public long threadID = -1;
      public List<FactionNewForumPostsPanel.ForumPostData> forumPosts = new List<FactionNewForumPostsPanel.ForumPostData>();
      public DateTime lastTime = DateTime.MinValue;
    }

    public class FactionsPostLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDExtendingPanel lightArea1 = new CustomSelfDrawPanel.CSDExtendingPanel();
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel userName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel dateLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel bodyLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDButton deleteThread = new CustomSelfDrawPanel.CSDButton();
      private int m_position = -1000;
      private FactionNewForumPostsPanel.ForumPostData m_postData;
      private FactionNewForumPostsPanel m_parent;
      private MyMessageBoxPopUp deletePostPopUp;

      public void init(
        FactionNewForumPostsPanel.ForumPostData postData,
        int position,
        FactionNewForumPostsPanel parent,
        int yourRank)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.m_postData = postData;
        Graphics graphics = parent.CreateGraphics();
        Size size = graphics.MeasureString(postData.text, FontManager.GetFont("Arial", 9f, FontStyle.Regular), 630).ToSize();
        graphics.Dispose();
        int height = size.Height + 10;
        if (height < 32)
          height = 32;
        this.clearControls();
        this.ClipVisible = true;
        this.Size = new Size(680, 25 + height);
        this.lightArea1.Size = new Size(640, height);
        this.lightArea1.Position = new Point(40, 25);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lightArea1);
        this.lightArea1.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
        NumberFormatInfo nfi = GameEngine.NFI;
        this.userName.Text = postData.userName;
        this.userName.Color = ARGBColors.Black;
        this.userName.Position = new Point(9, 3);
        this.userName.Size = new Size(280, 30);
        this.userName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.userName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.userName);
        this.dateLabel.Text = postData.postTime.ToShortTimeString() + " " + postData.postTime.ToShortDateString();
        this.dateLabel.Color = ARGBColors.Black;
        this.dateLabel.Position = new Point(534, 3);
        this.dateLabel.Size = new Size(161, 30);
        this.dateLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.dateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.dateLabel);
        this.bodyLabel.Text = postData.text;
        this.bodyLabel.Color = ARGBColors.Black;
        this.bodyLabel.Position = new Point(5, 5);
        this.bodyLabel.Size = new Size(this.lightArea1.Width - 10, height - 5);
        this.bodyLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.bodyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.bodyLabel.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.copyTextToClipboardClick));
        this.lightArea1.addControl((CustomSelfDrawPanel.CSDControl) this.bodyLabel);
        if (yourRank == 1 || yourRank == 2 || postData.userID == RemoteServices.Instance.UserID || RemoteServices.Instance.Admin || RemoteServices.Instance.Moderator)
        {
          this.deleteThread.ImageNorm = (Image) GFXLibrary.trashcan_normal;
          this.deleteThread.ImageOver = (Image) GFXLibrary.trashcan_over;
          this.deleteThread.ImageClick = (Image) GFXLibrary.trashcan_clicked;
          this.deleteThread.Position = new Point(680, 4);
          this.deleteThread.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteClicked), "FactionNewForumPostsPanel_delete_post");
          this.addControl((CustomSelfDrawPanel.CSDControl) this.deleteThread);
        }
        this.invalidate();
      }

      public void update()
      {
      }

      public void clickedLine()
      {
      }

      private void PopUpOkClick()
      {
        if (this.m_parent != null)
          this.m_parent.deletePost(this.m_postData.postID);
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

      private void deleteClicked()
      {
        this.ClosePopUp();
        InterfaceMgr.Instance.openGreyOutWindow(false);
        this.deletePostPopUp = new MyMessageBoxPopUp();
        this.deletePostPopUp.init(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FORUMS_Delete_Post", "Delete This Post"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.PopUpOkClick));
        this.deletePostPopUp.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
      }

      private void copyTextToClipboardClick() => Clipboard.SetText(this.m_postData.text);
    }
  }
}
