// Decompiled with JetBrains decompiler
// Type: Kingdoms.UserInfoScreen
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class UserInfoScreen : UserControl, IDockableControl
  {
    private WorldMap.CachedUserInfo m_userInfo;
    private int m_userID = -1;
    private int[] m_villages;
    private List<UserinfoScreenLine> lineList = new List<UserinfoScreenLine>();
    private string m_reasonString = "";
    private DockableControl dockableControl;
    private IContainer components;
    private BitmapButton btnClose;
    private Panel pnlVillages;
    private Label lblUserName;
    private Label lblRank;
    private Label label1;
    private Label lblPoints;
    private Label lblStanding;
    private Label label3;
    private Label label2;
    private Label lblFaction;
    private Label label5;
    private UserControl imgAvatar;
    private BitmapButton btnSendMail;
    private Label lblIsAdmin;
    private Label lblIsModerator;
    private GroupBox gbModerator;
    private BitmapButton btnChatBanClear;
    private BitmapButton btnChatBanPerma;
    private BitmapButton btnChatBan7;
    private BitmapButton btnChatBan3;
    private Label label6;
    private BitmapButton btnChatBan1;
    private Label label4;
    private BitmapButton btnWalBanClear;
    private BitmapButton btnWalBanPerma;
    private BitmapButton btnWalBan7;
    private BitmapButton btnWalBan3;
    private Label label9;
    private BitmapButton btnWalBan1;
    private BitmapButton btnForumBanClear;
    private BitmapButton btnForumBanPerma;
    private BitmapButton btnForumBan7;
    private BitmapButton btnForumBan3;
    private Label label8;
    private BitmapButton btnForumBan1;
    private BitmapButton btnMailBanClear;
    private BitmapButton btnMailBanPerma;
    private BitmapButton btnMailBan7;
    private BitmapButton btnMailBan3;
    private Label label7;
    private BitmapButton btnMailBan1;
    private BitmapButton btnMakeModerator;
    private BitmapButton btnRemoveModerator;
    private TextBox tbHonour;
    private TextBox tbGold;
    private Label lblHonour;
    private Label lblGold;
    private BitmapButton btnApplyGold;
    private BitmapButton btnApplyHonour;
    private BitmapButton btnApplyRP;
    private TextBox tbRP;
    private Label lblRP;
    private BitmapButton btnFlushCaches;
    private BitmapButton btnKick;
    private BitmapButton btnAchievements;
    private BitmapButton btnInviteToFaction;
    private TextBox tbStuff;
    private BitmapButton btnEditAvatar;
    private BitmapButton btnFixAchievements;
    private BitmapButton btnGiveQuests;

    public UserInfoScreen()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.lblUserName.Font = FontManager.GetFont("Microsoft Sans Serif", 12f, FontStyle.Bold);
      this.lblRank.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
      this.label1.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
      this.lblPoints.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
      this.lblStanding.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
      this.label3.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
      this.label2.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
      this.lblFaction.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
      this.label5.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    }

    public void clear()
    {
      this.m_villages = (int[]) null;
      this.m_userInfo = (WorldMap.CachedUserInfo) null;
      this.imgAvatar.BackgroundImage = (Image) null;
      this.lblFaction.ForeColor = ARGBColors.Black;
    }

    public void init(WorldMap.CachedUserInfo userInfo)
    {
      this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
      this.lblUserName.Text = "";
      this.lblRank.Text = "";
      this.label1.Text = SK.Text("GENERIC_Points", "Points") + " : ";
      this.label3.Text = SK.Text("STATS_CATEGORY_TITLE_RANK", "Rank") + " : ";
      this.label2.Text = SK.Text("UserInfoScreen_Villages", "Villages");
      this.label5.Text = SK.Text("STATS_CATEGORY_TITLE_FACTION", "Faction") + " : ";
      this.btnSendMail.Text = SK.Text("UserInfoScreen_Send_Mail", "Send Mail");
      this.btnAchievements.Text = SK.Text("GENERIC_Achievements", "Achievements");
      this.btnInviteToFaction.Text = SK.Text("UserInfoScreen_InviteToFaction", "Invite To Faction");
      this.btnEditAvatar.Text = SK.Text("MENU_Edit_Avatar", "Edit Avatar");
      if (userInfo == null)
      {
        userInfo = new WorldMap.CachedUserInfo();
        userInfo.userID = this.m_userID;
      }
      this.m_userID = userInfo.userID;
      NumberFormatInfo nfi = GameEngine.NFI;
      WorldMap.VillageRolloverInfo villageInfo = (WorldMap.VillageRolloverInfo) null;
      GameEngine.Instance.World.retrieveUserData(-1, userInfo.userID, ref villageInfo, ref userInfo, true, true);
      this.btnEditAvatar.Visible = this.m_userID == RemoteServices.Instance.UserID;
      this.m_userInfo = userInfo;
      if (userInfo != null)
      {
        this.lblUserName.Text = userInfo.userName;
        this.lblPoints.Text = userInfo.points.ToString("N", (IFormatProvider) nfi);
        if (userInfo.standing >= 0)
          this.lblStanding.Text = userInfo.standing.ToString("N", (IFormatProvider) nfi);
        else
          this.lblStanding.Text = "?";
        if (userInfo.avatarData != null)
          this.lblRank.Text = Rankings.getRankingName(userInfo.rank, userInfo.avatarData.male);
        else
          this.lblRank.Text = Rankings.getRankingName(userInfo.rank);
        if (userInfo.factionID >= 0)
        {
          FactionData faction = GameEngine.Instance.World.getFaction(userInfo.factionID);
          if (faction != null)
            this.lblFaction.Text = faction.factionNameAbrv;
          else
            this.lblFaction.Text = "";
        }
        else
          this.lblFaction.Text = "";
        if (userInfo.avatarData != null && this.imgAvatar.BackgroundImage == null)
          this.imgAvatar.BackgroundImage = (Image) Avatar.CreateAvatar(userInfo.avatarData, ARGBColors.Transparent);
        if (!this.areEqual(userInfo.villages, this.m_villages))
        {
          this.m_villages = userInfo.villages;
          this.addVillages(this.m_villages);
        }
        if (!RemoteServices.Instance.Admin && !RemoteServices.Instance.Moderator)
        {
          this.lblIsAdmin.Visible = false;
          this.lblIsModerator.Visible = false;
          this.gbModerator.Visible = false;
        }
        else
        {
          this.gbModerator.Visible = true;
          this.lblIsAdmin.Visible = RemoteServices.Instance.Admin;
          this.lblIsModerator.Visible = RemoteServices.Instance.Moderator;
          this.btnChatBan7.Visible = RemoteServices.Instance.Admin;
          this.btnChatBanPerma.Visible = RemoteServices.Instance.Admin;
          this.btnMailBanClear.Visible = RemoteServices.Instance.Admin;
          this.btnMailBan1.Visible = RemoteServices.Instance.Admin;
          this.btnMailBan3.Visible = RemoteServices.Instance.Admin;
          this.btnMailBan7.Visible = RemoteServices.Instance.Admin;
          this.btnMailBanPerma.Visible = RemoteServices.Instance.Admin;
          this.btnForumBan7.Visible = RemoteServices.Instance.Admin;
          this.btnForumBanPerma.Visible = RemoteServices.Instance.Admin;
          this.btnWalBan7.Visible = RemoteServices.Instance.Admin;
          this.btnWalBanPerma.Visible = RemoteServices.Instance.Admin;
          this.btnMakeModerator.Visible = false;
          this.btnRemoveModerator.Visible = false;
          this.lblGold.Visible = RemoteServices.Instance.Admin;
          this.lblHonour.Visible = RemoteServices.Instance.Admin;
          this.lblRP.Visible = RemoteServices.Instance.Admin;
          this.tbGold.Visible = RemoteServices.Instance.Admin;
          this.tbHonour.Visible = RemoteServices.Instance.Admin;
          this.tbRP.Visible = RemoteServices.Instance.Admin;
          this.btnApplyGold.Visible = RemoteServices.Instance.Admin;
          this.btnApplyHonour.Visible = RemoteServices.Instance.Admin;
          this.btnApplyRP.Visible = RemoteServices.Instance.Admin;
          this.btnFlushCaches.Visible = RemoteServices.Instance.Admin;
          if (RemoteServices.Instance.Admin)
            this.tbStuff.Text = userInfo.stuff.Replace("-", "");
          this.btnFixAchievements.Visible = RemoteServices.Instance.Admin;
          this.btnGiveQuests.Visible = RemoteServices.Instance.Admin;
        }
      }
      else
      {
        this.lblUserName.Text = "";
        this.lblPoints.Text = "0";
        this.lblStanding.Text = "0";
        this.lblRank.Text = "";
        this.lblFaction.Text = "";
        this.imgAvatar.BackgroundImage = (Image) null;
        this.lblIsAdmin.Visible = false;
        this.lblIsModerator.Visible = false;
        this.gbModerator.Visible = false;
      }
      int yourFactionRank = GameEngine.Instance.World.getYourFactionRank();
      bool flag = false;
      if (GameEngine.Instance.World.YourFaction != null && this.m_userInfo != null && this.m_userInfo.userID != RemoteServices.Instance.UserID)
      {
        FactionMemberData[] factionMembers = GameEngine.Instance.World.FactionMembers;
        if (factionMembers != null && yourFactionRank > 0 && factionMembers.Length < GameEngine.Instance.LocalWorldData.Faction_MaxMembers)
          flag = true;
      }
      this.btnInviteToFaction.Visible = flag;
    }

    private bool areEqual(int[] villages1, int[] villages2)
    {
      if (villages1 == null && villages2 == null)
        return true;
      if (villages1 == null || villages2 == null || villages1.Length != villages2.Length)
        return false;
      List<int> intList = new List<int>();
      intList.AddRange((IEnumerable<int>) villages2);
      foreach (int num in villages1)
      {
        if (!intList.Contains(num))
          return false;
      }
      return true;
    }

    public void update() => this.init(this.m_userInfo);

    private void addVillages(int[] villages)
    {
      int y1 = this.pnlVillages.AutoScrollPosition.Y;
      this.lineList.Clear();
      this.pnlVillages.SuspendLayout();
      this.pnlVillages.Controls.Clear();
      int y2 = 0;
      if (villages != null)
      {
        foreach (int village in villages)
        {
          UserinfoScreenLine userinfoScreenLine = new UserinfoScreenLine();
          userinfoScreenLine.Location = new Point(0, y2);
          userinfoScreenLine.init(GameEngine.Instance.World.getVillageName(village), village);
          this.pnlVillages.Controls.Add((Control) userinfoScreenLine);
          y2 += userinfoScreenLine.Height;
          this.lineList.Add(userinfoScreenLine);
        }
      }
      this.pnlVillages.ResumeLayout(false);
      this.pnlVillages.PerformLayout();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("UserInfoScreen_close");
      InterfaceMgr.Instance.closeParishPanel();
    }

    private void lblFaction_MouseEnter(object sender, EventArgs e)
    {
      this.lblFaction.ForeColor = ARGBColors.Blue;
    }

    private void lblFaction_MouseLeave(object sender, EventArgs e)
    {
      this.lblFaction.ForeColor = ARGBColors.Black;
    }

    private void btnSendMail_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("UserInfoScreen_send_mail");
      if (this.m_userInfo == null)
        return;
      InterfaceMgr.Instance.getMainTabBar().selectDummyTab(21);
      InterfaceMgr.Instance.mailTo(this.m_userInfo.userID, this.m_userInfo.userName);
    }

    private void btnChatBan1_Click(object sender, EventArgs e) => this.sendCommandToServer(1, 1);

    private void btnChatBan3_Click(object sender, EventArgs e) => this.sendCommandToServer(1, 3);

    private void btnChatBan7_Click(object sender, EventArgs e) => this.sendCommandToServer(1, 7);

    private void btnChatBanPerma_Click(object sender, EventArgs e)
    {
      this.sendCommandToServer(1, 3650);
    }

    private void btnChatBanClear_Click(object sender, EventArgs e)
    {
      this.sendCommandToServer(1, -1);
    }

    private void btnMailBan1_Click(object sender, EventArgs e) => this.sendCommandToServer(2, 1);

    private void btnMailBan3_Click(object sender, EventArgs e) => this.sendCommandToServer(2, 3);

    private void btnMailBan7_Click(object sender, EventArgs e) => this.sendCommandToServer(2, 7);

    private void btnMailBanPerma_Click(object sender, EventArgs e)
    {
      this.sendCommandToServer(2, 3650);
    }

    private void btnMailBanClear_Click(object sender, EventArgs e)
    {
      this.sendCommandToServer(2, -1);
    }

    private void btnForumBan1_Click(object sender, EventArgs e) => this.sendCommandToServer(3, 1);

    private void btnForumBan3_Click(object sender, EventArgs e) => this.sendCommandToServer(3, 3);

    private void btnForumBan7_Click(object sender, EventArgs e) => this.sendCommandToServer(3, 7);

    private void btnForumBanPerma_Click(object sender, EventArgs e)
    {
      this.sendCommandToServer(3, 3650);
    }

    private void btnForumBanClear_Click(object sender, EventArgs e)
    {
      this.sendCommandToServer(3, -1);
    }

    private void btnWalBan1_Click(object sender, EventArgs e) => this.sendCommandToServer(4, 1);

    private void btnWalBan3_Click(object sender, EventArgs e) => this.sendCommandToServer(4, 3);

    private void btnWalBan7_Click(object sender, EventArgs e) => this.sendCommandToServer(4, 7);

    private void btnWalBanPerma_Click(object sender, EventArgs e)
    {
      this.sendCommandToServer(4, 3650);
    }

    private void btnWalBanClear_Click(object sender, EventArgs e)
    {
      this.sendCommandToServer(4, -1);
    }

    private void btnMakeModerator_Click(object sender, EventArgs e)
    {
    }

    private void btnRemoveModerator_Click(object sender, EventArgs e)
    {
    }

    private void sendCommandToServer(int command, int duration)
    {
      bool flag = false;
      if (RemoteServices.Instance.Admin && this.m_userID == RemoteServices.Instance.UserID)
      {
        switch (command)
        {
          case 21:
          case 22:
          case 23:
          case 1000:
            flag = true;
            break;
        }
      }
      if (this.m_userID != RemoteServices.Instance.UserID && (RemoteServices.Instance.Admin || RemoteServices.Instance.Moderator) || flag)
      {
        this.m_reasonString = "";
        ReasonPopup reasonPopup = new ReasonPopup();
        reasonPopup.init(this);
        int num1 = (int) reasonPopup.ShowDialog();
        if (this.m_reasonString.Length > 0)
        {
          RemoteServices.Instance.SendCommands(this.m_userID, command, duration, this.m_reasonString);
        }
        else
        {
          int num2 = (int) MyMessageBox.Show("Not reason given", "Admin Error");
        }
      }
      else
      {
        int num = (int) MyMessageBox.Show("Command not sent", "Admin Error");
      }
    }

    public void setReasonString(string reasonString) => this.m_reasonString = reasonString;

    private void btnApplyGold_Click(object sender, EventArgs e)
    {
      if (!RemoteServices.Instance.Admin)
        return;
      int int32FromString = UserInfoScreen.getInt32FromString(this.tbGold.Text);
      if (int32FromString > 0 && int32FromString < 1000000)
      {
        this.sendCommandToServer(21, int32FromString);
      }
      else
      {
        int num = (int) MyMessageBox.Show("Out of range", "Admin Error");
      }
    }

    private void btnApplyHonour_Click(object sender, EventArgs e)
    {
      if (!RemoteServices.Instance.Admin)
        return;
      int int32FromString = UserInfoScreen.getInt32FromString(this.tbHonour.Text);
      if (int32FromString > 0 && int32FromString < 10000000)
      {
        this.sendCommandToServer(22, int32FromString);
      }
      else
      {
        int num = (int) MyMessageBox.Show("Out of range", "Admin Error");
      }
    }

    public static int getInt32FromString(string text)
    {
      if (text.Length == 0)
        return 0;
      try
      {
        return Convert.ToInt32(text);
      }
      catch (Exception ex)
      {
      }
      return 0;
    }

    private void btnApplyRP_Click(object sender, EventArgs e)
    {
      if (!RemoteServices.Instance.Admin)
        return;
      int int32FromString = UserInfoScreen.getInt32FromString(this.tbRP.Text);
      if (int32FromString > 0 && int32FromString < 5)
      {
        this.sendCommandToServer(23, int32FromString);
      }
      else
      {
        int num = (int) MyMessageBox.Show("Out of range", "Admin Error");
      }
    }

    private void btnFlushCaches_Click(object sender, EventArgs e)
    {
      if (!RemoteServices.Instance.Admin)
        return;
      this.sendCommandToServer(31, 0);
    }

    private void btnKick_Click(object sender, EventArgs e)
    {
      RemoteServices.Instance.Chat_Admin_Command(5, this.m_userID);
    }

    private void lblUserName_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
        return;
      Clipboard.SetText(((Control) sender).Text);
    }

    private void lblFaction_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left && this.m_userInfo != null && this.m_userInfo.factionID >= 0)
      {
        GameEngine.Instance.playInterfaceSound("UserInfoScreen_faction");
        InterfaceMgr.Instance.closeParishPanel();
        InterfaceMgr.Instance.showFactionPanel(this.m_userInfo.factionID);
      }
      if (e.Button != MouseButtons.Right)
        return;
      Clipboard.SetText(((Control) sender).Text);
    }

    private void btnAchievements_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("UserInfoScreen_achievements");
      InterfaceMgr.Instance.openAchievements(this.m_userInfo.achievements);
    }

    private void btnInviteToFaction_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("UserInfoScreen_faction_invite");
      InterfaceMgr.Instance.clearControls();
      InterfaceMgr.Instance.inviteToFaction(this.m_userInfo.userName);
    }

    private void UserInfoScreen_Paint(object sender, PaintEventArgs e)
    {
      Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(86, 98, 106), Color.FromArgb(159, 180, 193), LinearGradientMode.Vertical);
      e.Graphics.FillRectangle((Brush) linearGradientBrush, rect);
      linearGradientBrush.Dispose();
    }

    private void UserInfoScreen_Resize(object sender, EventArgs e) => this.Invalidate();

    private void bitmapButton1_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("UserInfoScreen_edit_avatar");
      InterfaceMgr.Instance.getMainTabBar().selectDummyTab(10);
    }

    private void btnFixAchievements_Click(object sender, EventArgs e)
    {
      this.sendCommandToServer(42, 0);
    }

    private void btnGiveQuests_Click(object sender, EventArgs e) => this.sendCommandToServer(43, 0);

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
      this.pnlVillages = new Panel();
      this.lblUserName = new Label();
      this.lblRank = new Label();
      this.label1 = new Label();
      this.lblPoints = new Label();
      this.lblStanding = new Label();
      this.label3 = new Label();
      this.label2 = new Label();
      this.lblFaction = new Label();
      this.label5 = new Label();
      this.imgAvatar = new UserControl();
      this.lblIsAdmin = new Label();
      this.lblIsModerator = new Label();
      this.gbModerator = new GroupBox();
      this.btnFixAchievements = new BitmapButton();
      this.tbStuff = new TextBox();
      this.btnKick = new BitmapButton();
      this.btnFlushCaches = new BitmapButton();
      this.btnApplyRP = new BitmapButton();
      this.tbRP = new TextBox();
      this.lblRP = new Label();
      this.btnApplyHonour = new BitmapButton();
      this.btnApplyGold = new BitmapButton();
      this.tbHonour = new TextBox();
      this.tbGold = new TextBox();
      this.lblHonour = new Label();
      this.lblGold = new Label();
      this.btnRemoveModerator = new BitmapButton();
      this.btnMakeModerator = new BitmapButton();
      this.btnWalBanClear = new BitmapButton();
      this.btnWalBanPerma = new BitmapButton();
      this.btnWalBan7 = new BitmapButton();
      this.btnWalBan3 = new BitmapButton();
      this.label9 = new Label();
      this.btnWalBan1 = new BitmapButton();
      this.btnForumBanClear = new BitmapButton();
      this.btnForumBanPerma = new BitmapButton();
      this.btnForumBan7 = new BitmapButton();
      this.btnForumBan3 = new BitmapButton();
      this.label8 = new Label();
      this.btnForumBan1 = new BitmapButton();
      this.btnMailBanClear = new BitmapButton();
      this.btnMailBanPerma = new BitmapButton();
      this.btnMailBan7 = new BitmapButton();
      this.btnMailBan3 = new BitmapButton();
      this.label7 = new Label();
      this.btnMailBan1 = new BitmapButton();
      this.btnChatBanClear = new BitmapButton();
      this.btnChatBanPerma = new BitmapButton();
      this.btnChatBan7 = new BitmapButton();
      this.btnChatBan3 = new BitmapButton();
      this.label6 = new Label();
      this.btnChatBan1 = new BitmapButton();
      this.label4 = new Label();
      this.btnEditAvatar = new BitmapButton();
      this.btnInviteToFaction = new BitmapButton();
      this.btnAchievements = new BitmapButton();
      this.btnSendMail = new BitmapButton();
      this.btnClose = new BitmapButton();
      this.btnGiveQuests = new BitmapButton();
      this.gbModerator.SuspendLayout();
      this.SuspendLayout();
      this.pnlVillages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.pnlVillages.AutoScroll = true;
      this.pnlVillages.BackColor = Color.FromArgb(96, 109, 118);
      this.pnlVillages.Location = new Point(23, 317);
      this.pnlVillages.Name = "pnlVillages";
      this.pnlVillages.Size = new Size(336, 221);
      this.pnlVillages.TabIndex = 9;
      this.lblUserName.AutoSize = true;
      this.lblUserName.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.lblUserName.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblUserName.Location = new Point(36, 19);
      this.lblUserName.Name = "lblUserName";
      this.lblUserName.Size = new Size(93, 20);
      this.lblUserName.TabIndex = 10;
      this.lblUserName.Text = "UserName";
      this.lblUserName.MouseClick += new MouseEventHandler(this.lblUserName_MouseClick);
      this.lblRank.AutoSize = true;
      this.lblRank.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.lblRank.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblRank.Location = new Point(37, 49);
      this.lblRank.Name = "lblRank";
      this.lblRank.Size = new Size(43, 17);
      this.lblRank.TabIndex = 11;
      this.lblRank.Text = "name";
      this.label1.AutoSize = true;
      this.label1.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(37, 108);
      this.label1.Name = "label1";
      this.label1.Size = new Size(59, 17);
      this.label1.TabIndex = 12;
      this.label1.Text = "Points : ";
      this.lblPoints.AutoSize = true;
      this.lblPoints.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.lblPoints.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblPoints.Location = new Point(113, 108);
      this.lblPoints.Name = "lblPoints";
      this.lblPoints.Size = new Size(16, 17);
      this.lblPoints.TabIndex = 13;
      this.lblPoints.Text = "0";
      this.lblStanding.AutoSize = true;
      this.lblStanding.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.lblStanding.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblStanding.Location = new Point(113, 148);
      this.lblStanding.Name = "lblStanding";
      this.lblStanding.Size = new Size(16, 17);
      this.lblStanding.TabIndex = 15;
      this.lblStanding.Text = "0";
      this.label3.AutoSize = true;
      this.label3.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label3.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label3.Location = new Point(37, 148);
      this.label3.Name = "label3";
      this.label3.Size = new Size(49, 17);
      this.label3.TabIndex = 14;
      this.label3.Text = "Rank :";
      this.label2.AutoSize = true;
      this.label2.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2.Location = new Point(29, 297);
      this.label2.Name = "label2";
      this.label2.Size = new Size(57, 17);
      this.label2.TabIndex = 16;
      this.label2.Text = "Villages";
      this.lblFaction.AutoSize = true;
      this.lblFaction.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.lblFaction.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.lblFaction.Location = new Point(113, 235);
      this.lblFaction.Name = "lblFaction";
      this.lblFaction.Size = new Size(24, 17);
      this.lblFaction.TabIndex = 18;
      this.lblFaction.Text = "....";
      this.lblFaction.MouseLeave += new EventHandler(this.lblFaction_MouseLeave);
      this.lblFaction.MouseClick += new MouseEventHandler(this.lblFaction_MouseClick);
      this.lblFaction.MouseEnter += new EventHandler(this.lblFaction_MouseEnter);
      this.label5.AutoSize = true;
      this.label5.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5.Location = new Point(37, 235);
      this.label5.Name = "label5";
      this.label5.Size = new Size(62, 17);
      this.label5.TabIndex = 17;
      this.label5.Text = "Faction :";
      this.imgAvatar.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.imgAvatar.Location = new Point(616, 19);
      this.imgAvatar.Name = "imgAvatar";
      this.imgAvatar.Size = new Size(154, 500);
      this.imgAvatar.TabIndex = 19;
      this.lblIsAdmin.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblIsAdmin.AutoSize = true;
      this.lblIsAdmin.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.lblIsAdmin.Location = new Point(385, 213);
      this.lblIsAdmin.Name = "lblIsAdmin";
      this.lblIsAdmin.Size = new Size(47, 13);
      this.lblIsAdmin.TabIndex = 21;
      this.lblIsAdmin.Text = "Is Admin";
      this.lblIsAdmin.Visible = false;
      this.lblIsModerator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblIsModerator.AutoSize = true;
      this.lblIsModerator.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.lblIsModerator.Location = new Point(438, 213);
      this.lblIsModerator.Name = "lblIsModerator";
      this.lblIsModerator.Size = new Size(66, 13);
      this.lblIsModerator.TabIndex = 22;
      this.lblIsModerator.Text = "Is Moderator";
      this.lblIsModerator.Visible = false;
      this.gbModerator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.gbModerator.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.gbModerator.Controls.Add((Control) this.btnGiveQuests);
      this.gbModerator.Controls.Add((Control) this.btnFixAchievements);
      this.gbModerator.Controls.Add((Control) this.tbStuff);
      this.gbModerator.Controls.Add((Control) this.btnKick);
      this.gbModerator.Controls.Add((Control) this.btnFlushCaches);
      this.gbModerator.Controls.Add((Control) this.btnApplyRP);
      this.gbModerator.Controls.Add((Control) this.tbRP);
      this.gbModerator.Controls.Add((Control) this.lblRP);
      this.gbModerator.Controls.Add((Control) this.btnApplyHonour);
      this.gbModerator.Controls.Add((Control) this.btnApplyGold);
      this.gbModerator.Controls.Add((Control) this.tbHonour);
      this.gbModerator.Controls.Add((Control) this.tbGold);
      this.gbModerator.Controls.Add((Control) this.lblHonour);
      this.gbModerator.Controls.Add((Control) this.lblGold);
      this.gbModerator.Controls.Add((Control) this.btnRemoveModerator);
      this.gbModerator.Controls.Add((Control) this.btnMakeModerator);
      this.gbModerator.Controls.Add((Control) this.btnWalBanClear);
      this.gbModerator.Controls.Add((Control) this.btnWalBanPerma);
      this.gbModerator.Controls.Add((Control) this.btnWalBan7);
      this.gbModerator.Controls.Add((Control) this.btnWalBan3);
      this.gbModerator.Controls.Add((Control) this.label9);
      this.gbModerator.Controls.Add((Control) this.btnWalBan1);
      this.gbModerator.Controls.Add((Control) this.btnForumBanClear);
      this.gbModerator.Controls.Add((Control) this.btnForumBanPerma);
      this.gbModerator.Controls.Add((Control) this.btnForumBan7);
      this.gbModerator.Controls.Add((Control) this.btnForumBan3);
      this.gbModerator.Controls.Add((Control) this.label8);
      this.gbModerator.Controls.Add((Control) this.btnForumBan1);
      this.gbModerator.Controls.Add((Control) this.btnMailBanClear);
      this.gbModerator.Controls.Add((Control) this.btnMailBanPerma);
      this.gbModerator.Controls.Add((Control) this.btnMailBan7);
      this.gbModerator.Controls.Add((Control) this.btnMailBan3);
      this.gbModerator.Controls.Add((Control) this.label7);
      this.gbModerator.Controls.Add((Control) this.btnMailBan1);
      this.gbModerator.Controls.Add((Control) this.btnChatBanClear);
      this.gbModerator.Controls.Add((Control) this.btnChatBanPerma);
      this.gbModerator.Controls.Add((Control) this.btnChatBan7);
      this.gbModerator.Controls.Add((Control) this.btnChatBan3);
      this.gbModerator.Controls.Add((Control) this.label6);
      this.gbModerator.Controls.Add((Control) this.btnChatBan1);
      this.gbModerator.Controls.Add((Control) this.label4);
      this.gbModerator.Location = new Point(365, 229);
      this.gbModerator.Name = "gbModerator";
      this.gbModerator.Size = new Size(245, 325);
      this.gbModerator.TabIndex = 23;
      this.gbModerator.TabStop = false;
      this.gbModerator.Text = "Moderating Functions";
      this.gbModerator.Visible = false;
      this.btnFixAchievements.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnFixAchievements.BorderDrawing = true;
      this.btnFixAchievements.FocusRectangleEnabled = false;
      this.btnFixAchievements.Image = (Image) null;
      this.btnFixAchievements.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnFixAchievements.ImageBorderEnabled = true;
      this.btnFixAchievements.ImageDropShadow = true;
      this.btnFixAchievements.ImageFocused = (Image) null;
      this.btnFixAchievements.ImageInactive = (Image) null;
      this.btnFixAchievements.ImageMouseOver = (Image) null;
      this.btnFixAchievements.ImageNormal = (Image) null;
      this.btnFixAchievements.ImagePressed = (Image) null;
      this.btnFixAchievements.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnFixAchievements.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnFixAchievements.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnFixAchievements.Location = new Point(12, 302);
      this.btnFixAchievements.Name = "btnFixAchievements";
      this.btnFixAchievements.OffsetPressedContent = true;
      this.btnFixAchievements.Padding2 = 5;
      this.btnFixAchievements.Size = new Size(108, 23);
      this.btnFixAchievements.StretchImage = false;
      this.btnFixAchievements.TabIndex = 41;
      this.btnFixAchievements.Text = "Fix Achievements";
      this.btnFixAchievements.TextDropShadow = false;
      this.btnFixAchievements.UseVisualStyleBackColor = true;
      this.btnFixAchievements.Click += new EventHandler(this.btnFixAchievements_Click);
      this.tbStuff.Location = new Point(12, 280);
      this.tbStuff.Name = "tbStuff";
      this.tbStuff.ReadOnly = true;
      this.tbStuff.Size = new Size(227, 20);
      this.tbStuff.TabIndex = 40;
      this.btnKick.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnKick.BorderDrawing = true;
      this.btnKick.FocusRectangleEnabled = false;
      this.btnKick.Image = (Image) null;
      this.btnKick.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnKick.ImageBorderEnabled = true;
      this.btnKick.ImageDropShadow = true;
      this.btnKick.ImageFocused = (Image) null;
      this.btnKick.ImageInactive = (Image) null;
      this.btnKick.ImageMouseOver = (Image) null;
      this.btnKick.ImageNormal = (Image) null;
      this.btnKick.ImagePressed = (Image) null;
      this.btnKick.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnKick.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnKick.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnKick.Location = new Point(214, 31);
      this.btnKick.Name = "btnKick";
      this.btnKick.OffsetPressedContent = true;
      this.btnKick.Padding2 = 5;
      this.btnKick.Size = new Size(25, 23);
      this.btnKick.StretchImage = false;
      this.btnKick.TabIndex = 39;
      this.btnKick.Text = "K";
      this.btnKick.TextDropShadow = false;
      this.btnKick.UseVisualStyleBackColor = true;
      this.btnKick.Click += new EventHandler(this.btnKick_Click);
      this.btnFlushCaches.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnFlushCaches.BorderDrawing = true;
      this.btnFlushCaches.FocusRectangleEnabled = false;
      this.btnFlushCaches.Image = (Image) null;
      this.btnFlushCaches.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnFlushCaches.ImageBorderEnabled = true;
      this.btnFlushCaches.ImageDropShadow = true;
      this.btnFlushCaches.ImageFocused = (Image) null;
      this.btnFlushCaches.ImageInactive = (Image) null;
      this.btnFlushCaches.ImageMouseOver = (Image) null;
      this.btnFlushCaches.ImageNormal = (Image) null;
      this.btnFlushCaches.ImagePressed = (Image) null;
      this.btnFlushCaches.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnFlushCaches.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnFlushCaches.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnFlushCaches.Location = new Point(44, 254);
      this.btnFlushCaches.Name = "btnFlushCaches";
      this.btnFlushCaches.OffsetPressedContent = true;
      this.btnFlushCaches.Padding2 = 5;
      this.btnFlushCaches.Size = new Size(151, 23);
      this.btnFlushCaches.StretchImage = false;
      this.btnFlushCaches.TabIndex = 38;
      this.btnFlushCaches.Text = "Flush Client Village Cache";
      this.btnFlushCaches.TextDropShadow = false;
      this.btnFlushCaches.UseVisualStyleBackColor = true;
      this.btnFlushCaches.Click += new EventHandler(this.btnFlushCaches_Click);
      this.btnApplyRP.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnApplyRP.BorderDrawing = true;
      this.btnApplyRP.FocusRectangleEnabled = false;
      this.btnApplyRP.Image = (Image) null;
      this.btnApplyRP.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnApplyRP.ImageBorderEnabled = true;
      this.btnApplyRP.ImageDropShadow = true;
      this.btnApplyRP.ImageFocused = (Image) null;
      this.btnApplyRP.ImageInactive = (Image) null;
      this.btnApplyRP.ImageMouseOver = (Image) null;
      this.btnApplyRP.ImageNormal = (Image) null;
      this.btnApplyRP.ImagePressed = (Image) null;
      this.btnApplyRP.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnApplyRP.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnApplyRP.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnApplyRP.Location = new Point(182, 228);
      this.btnApplyRP.Name = "btnApplyRP";
      this.btnApplyRP.OffsetPressedContent = true;
      this.btnApplyRP.Padding2 = 5;
      this.btnApplyRP.Size = new Size(57, 23);
      this.btnApplyRP.StretchImage = false;
      this.btnApplyRP.TabIndex = 37;
      this.btnApplyRP.Text = "Give";
      this.btnApplyRP.TextDropShadow = false;
      this.btnApplyRP.UseVisualStyleBackColor = true;
      this.btnApplyRP.Click += new EventHandler(this.btnApplyRP_Click);
      this.tbRP.Location = new Point(76, 229);
      this.tbRP.Name = "tbRP";
      this.tbRP.Size = new Size(100, 20);
      this.tbRP.TabIndex = 36;
      this.lblRP.AutoSize = true;
      this.lblRP.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.lblRP.Location = new Point(9, 233);
      this.lblRP.Name = "lblRP";
      this.lblRP.Size = new Size(22, 13);
      this.lblRP.TabIndex = 35;
      this.lblRP.Text = "RP";
      this.btnApplyHonour.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnApplyHonour.BorderDrawing = true;
      this.btnApplyHonour.FocusRectangleEnabled = false;
      this.btnApplyHonour.Image = (Image) null;
      this.btnApplyHonour.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnApplyHonour.ImageBorderEnabled = true;
      this.btnApplyHonour.ImageDropShadow = true;
      this.btnApplyHonour.ImageFocused = (Image) null;
      this.btnApplyHonour.ImageInactive = (Image) null;
      this.btnApplyHonour.ImageMouseOver = (Image) null;
      this.btnApplyHonour.ImageNormal = (Image) null;
      this.btnApplyHonour.ImagePressed = (Image) null;
      this.btnApplyHonour.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnApplyHonour.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnApplyHonour.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnApplyHonour.Location = new Point(182, 202);
      this.btnApplyHonour.Name = "btnApplyHonour";
      this.btnApplyHonour.OffsetPressedContent = true;
      this.btnApplyHonour.Padding2 = 5;
      this.btnApplyHonour.Size = new Size(57, 23);
      this.btnApplyHonour.StretchImage = false;
      this.btnApplyHonour.TabIndex = 34;
      this.btnApplyHonour.Text = "Give";
      this.btnApplyHonour.TextDropShadow = false;
      this.btnApplyHonour.UseVisualStyleBackColor = true;
      this.btnApplyHonour.Click += new EventHandler(this.btnApplyHonour_Click);
      this.btnApplyGold.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnApplyGold.BorderDrawing = true;
      this.btnApplyGold.FocusRectangleEnabled = false;
      this.btnApplyGold.Image = (Image) null;
      this.btnApplyGold.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnApplyGold.ImageBorderEnabled = true;
      this.btnApplyGold.ImageDropShadow = true;
      this.btnApplyGold.ImageFocused = (Image) null;
      this.btnApplyGold.ImageInactive = (Image) null;
      this.btnApplyGold.ImageMouseOver = (Image) null;
      this.btnApplyGold.ImageNormal = (Image) null;
      this.btnApplyGold.ImagePressed = (Image) null;
      this.btnApplyGold.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnApplyGold.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnApplyGold.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnApplyGold.Location = new Point(182, 176);
      this.btnApplyGold.Name = "btnApplyGold";
      this.btnApplyGold.OffsetPressedContent = true;
      this.btnApplyGold.Padding2 = 5;
      this.btnApplyGold.Size = new Size(57, 23);
      this.btnApplyGold.StretchImage = false;
      this.btnApplyGold.TabIndex = 33;
      this.btnApplyGold.Text = "Give";
      this.btnApplyGold.TextDropShadow = false;
      this.btnApplyGold.UseVisualStyleBackColor = true;
      this.btnApplyGold.Click += new EventHandler(this.btnApplyGold_Click);
      this.tbHonour.Location = new Point(76, 203);
      this.tbHonour.Name = "tbHonour";
      this.tbHonour.Size = new Size(100, 20);
      this.tbHonour.TabIndex = 32;
      this.tbGold.Location = new Point(76, 178);
      this.tbGold.Name = "tbGold";
      this.tbGold.Size = new Size(100, 20);
      this.tbGold.TabIndex = 31;
      this.lblHonour.AutoSize = true;
      this.lblHonour.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.lblHonour.Location = new Point(9, 207);
      this.lblHonour.Name = "lblHonour";
      this.lblHonour.Size = new Size(42, 13);
      this.lblHonour.TabIndex = 30;
      this.lblHonour.Text = "Honour";
      this.lblGold.AutoSize = true;
      this.lblGold.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.lblGold.Location = new Point(9, 181);
      this.lblGold.Name = "lblGold";
      this.lblGold.Size = new Size(29, 13);
      this.lblGold.TabIndex = 29;
      this.lblGold.Text = "Gold";
      this.btnRemoveModerator.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnRemoveModerator.BorderDrawing = true;
      this.btnRemoveModerator.FocusRectangleEnabled = false;
      this.btnRemoveModerator.Image = (Image) null;
      this.btnRemoveModerator.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnRemoveModerator.ImageBorderEnabled = true;
      this.btnRemoveModerator.ImageDropShadow = true;
      this.btnRemoveModerator.ImageFocused = (Image) null;
      this.btnRemoveModerator.ImageInactive = (Image) null;
      this.btnRemoveModerator.ImageMouseOver = (Image) null;
      this.btnRemoveModerator.ImageNormal = (Image) null;
      this.btnRemoveModerator.ImagePressed = (Image) null;
      this.btnRemoveModerator.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnRemoveModerator.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnRemoveModerator.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnRemoveModerator.Location = new Point((int) sbyte.MaxValue, 147);
      this.btnRemoveModerator.Name = "btnRemoveModerator";
      this.btnRemoveModerator.OffsetPressedContent = true;
      this.btnRemoveModerator.Padding2 = 5;
      this.btnRemoveModerator.Size = new Size(112, 23);
      this.btnRemoveModerator.StretchImage = false;
      this.btnRemoveModerator.TabIndex = 28;
      this.btnRemoveModerator.Text = "Remove Moderator";
      this.btnRemoveModerator.TextDropShadow = false;
      this.btnRemoveModerator.UseVisualStyleBackColor = true;
      this.btnRemoveModerator.Click += new EventHandler(this.btnRemoveModerator_Click);
      this.btnMakeModerator.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnMakeModerator.BorderDrawing = true;
      this.btnMakeModerator.FocusRectangleEnabled = false;
      this.btnMakeModerator.Image = (Image) null;
      this.btnMakeModerator.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnMakeModerator.ImageBorderEnabled = true;
      this.btnMakeModerator.ImageDropShadow = true;
      this.btnMakeModerator.ImageFocused = (Image) null;
      this.btnMakeModerator.ImageInactive = (Image) null;
      this.btnMakeModerator.ImageMouseOver = (Image) null;
      this.btnMakeModerator.ImageNormal = (Image) null;
      this.btnMakeModerator.ImagePressed = (Image) null;
      this.btnMakeModerator.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnMakeModerator.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnMakeModerator.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnMakeModerator.Location = new Point(9, 147);
      this.btnMakeModerator.Name = "btnMakeModerator";
      this.btnMakeModerator.OffsetPressedContent = true;
      this.btnMakeModerator.Padding2 = 5;
      this.btnMakeModerator.Size = new Size(112, 23);
      this.btnMakeModerator.StretchImage = false;
      this.btnMakeModerator.TabIndex = 26;
      this.btnMakeModerator.Text = "Make Moderator";
      this.btnMakeModerator.TextDropShadow = false;
      this.btnMakeModerator.UseVisualStyleBackColor = true;
      this.btnMakeModerator.Click += new EventHandler(this.btnMakeModerator_Click);
      this.btnWalBanClear.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnWalBanClear.BorderDrawing = true;
      this.btnWalBanClear.FocusRectangleEnabled = false;
      this.btnWalBanClear.Image = (Image) null;
      this.btnWalBanClear.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnWalBanClear.ImageBorderEnabled = true;
      this.btnWalBanClear.ImageDropShadow = true;
      this.btnWalBanClear.ImageFocused = (Image) null;
      this.btnWalBanClear.ImageInactive = (Image) null;
      this.btnWalBanClear.ImageMouseOver = (Image) null;
      this.btnWalBanClear.ImageNormal = (Image) null;
      this.btnWalBanClear.ImagePressed = (Image) null;
      this.btnWalBanClear.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnWalBanClear.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnWalBanClear.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnWalBanClear.Location = new Point(178, 118);
      this.btnWalBanClear.Name = "btnWalBanClear";
      this.btnWalBanClear.OffsetPressedContent = true;
      this.btnWalBanClear.Padding2 = 5;
      this.btnWalBanClear.Size = new Size(30, 23);
      this.btnWalBanClear.StretchImage = false;
      this.btnWalBanClear.TabIndex = 24;
      this.btnWalBanClear.Text = "Clr";
      this.btnWalBanClear.TextDropShadow = false;
      this.btnWalBanClear.UseVisualStyleBackColor = true;
      this.btnWalBanClear.Click += new EventHandler(this.btnWalBanClear_Click);
      this.btnWalBanPerma.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnWalBanPerma.BorderDrawing = true;
      this.btnWalBanPerma.FocusRectangleEnabled = false;
      this.btnWalBanPerma.Image = (Image) null;
      this.btnWalBanPerma.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnWalBanPerma.ImageBorderEnabled = true;
      this.btnWalBanPerma.ImageDropShadow = true;
      this.btnWalBanPerma.ImageFocused = (Image) null;
      this.btnWalBanPerma.ImageInactive = (Image) null;
      this.btnWalBanPerma.ImageMouseOver = (Image) null;
      this.btnWalBanPerma.ImageNormal = (Image) null;
      this.btnWalBanPerma.ImagePressed = (Image) null;
      this.btnWalBanPerma.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnWalBanPerma.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnWalBanPerma.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnWalBanPerma.Location = new Point(138, 118);
      this.btnWalBanPerma.Name = "btnWalBanPerma";
      this.btnWalBanPerma.OffsetPressedContent = true;
      this.btnWalBanPerma.Padding2 = 5;
      this.btnWalBanPerma.Size = new Size(34, 23);
      this.btnWalBanPerma.StretchImage = false;
      this.btnWalBanPerma.TabIndex = 23;
      this.btnWalBanPerma.Text = "Prm";
      this.btnWalBanPerma.TextDropShadow = false;
      this.btnWalBanPerma.UseVisualStyleBackColor = true;
      this.btnWalBanPerma.Click += new EventHandler(this.btnWalBanPerma_Click);
      this.btnWalBan7.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnWalBan7.BorderDrawing = true;
      this.btnWalBan7.FocusRectangleEnabled = false;
      this.btnWalBan7.Image = (Image) null;
      this.btnWalBan7.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnWalBan7.ImageBorderEnabled = true;
      this.btnWalBan7.ImageDropShadow = true;
      this.btnWalBan7.ImageFocused = (Image) null;
      this.btnWalBan7.ImageInactive = (Image) null;
      this.btnWalBan7.ImageMouseOver = (Image) null;
      this.btnWalBan7.ImageNormal = (Image) null;
      this.btnWalBan7.ImagePressed = (Image) null;
      this.btnWalBan7.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnWalBan7.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnWalBan7.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnWalBan7.Location = new Point(107, 118);
      this.btnWalBan7.Name = "btnWalBan7";
      this.btnWalBan7.OffsetPressedContent = true;
      this.btnWalBan7.Padding2 = 5;
      this.btnWalBan7.Size = new Size(25, 23);
      this.btnWalBan7.StretchImage = false;
      this.btnWalBan7.TabIndex = 22;
      this.btnWalBan7.Text = "7";
      this.btnWalBan7.TextDropShadow = false;
      this.btnWalBan7.UseVisualStyleBackColor = true;
      this.btnWalBan7.Click += new EventHandler(this.btnWalBan7_Click);
      this.btnWalBan3.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnWalBan3.BorderDrawing = true;
      this.btnWalBan3.FocusRectangleEnabled = false;
      this.btnWalBan3.Image = (Image) null;
      this.btnWalBan3.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnWalBan3.ImageBorderEnabled = true;
      this.btnWalBan3.ImageDropShadow = true;
      this.btnWalBan3.ImageFocused = (Image) null;
      this.btnWalBan3.ImageInactive = (Image) null;
      this.btnWalBan3.ImageMouseOver = (Image) null;
      this.btnWalBan3.ImageNormal = (Image) null;
      this.btnWalBan3.ImagePressed = (Image) null;
      this.btnWalBan3.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnWalBan3.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnWalBan3.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnWalBan3.Location = new Point(76, 118);
      this.btnWalBan3.Name = "btnWalBan3";
      this.btnWalBan3.OffsetPressedContent = true;
      this.btnWalBan3.Padding2 = 5;
      this.btnWalBan3.Size = new Size(25, 23);
      this.btnWalBan3.StretchImage = false;
      this.btnWalBan3.TabIndex = 21;
      this.btnWalBan3.Text = "3";
      this.btnWalBan3.TextDropShadow = false;
      this.btnWalBan3.UseVisualStyleBackColor = true;
      this.btnWalBan3.Click += new EventHandler(this.btnWalBan3_Click);
      this.label9.AutoSize = true;
      this.label9.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label9.Location = new Point(8, 123);
      this.label9.Name = "label9";
      this.label9.Size = new Size(28, 13);
      this.label9.TabIndex = 20;
      this.label9.Text = "Wall";
      this.btnWalBan1.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnWalBan1.BorderDrawing = true;
      this.btnWalBan1.FocusRectangleEnabled = false;
      this.btnWalBan1.Image = (Image) null;
      this.btnWalBan1.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnWalBan1.ImageBorderEnabled = true;
      this.btnWalBan1.ImageDropShadow = true;
      this.btnWalBan1.ImageFocused = (Image) null;
      this.btnWalBan1.ImageInactive = (Image) null;
      this.btnWalBan1.ImageMouseOver = (Image) null;
      this.btnWalBan1.ImageNormal = (Image) null;
      this.btnWalBan1.ImagePressed = (Image) null;
      this.btnWalBan1.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnWalBan1.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnWalBan1.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnWalBan1.Location = new Point(45, 118);
      this.btnWalBan1.Name = "btnWalBan1";
      this.btnWalBan1.OffsetPressedContent = true;
      this.btnWalBan1.Padding2 = 5;
      this.btnWalBan1.Size = new Size(25, 23);
      this.btnWalBan1.StretchImage = false;
      this.btnWalBan1.TabIndex = 19;
      this.btnWalBan1.Text = "1";
      this.btnWalBan1.TextDropShadow = false;
      this.btnWalBan1.UseVisualStyleBackColor = true;
      this.btnWalBan1.Click += new EventHandler(this.btnWalBan1_Click);
      this.btnForumBanClear.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnForumBanClear.BorderDrawing = true;
      this.btnForumBanClear.FocusRectangleEnabled = false;
      this.btnForumBanClear.Image = (Image) null;
      this.btnForumBanClear.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnForumBanClear.ImageBorderEnabled = true;
      this.btnForumBanClear.ImageDropShadow = true;
      this.btnForumBanClear.ImageFocused = (Image) null;
      this.btnForumBanClear.ImageInactive = (Image) null;
      this.btnForumBanClear.ImageMouseOver = (Image) null;
      this.btnForumBanClear.ImageNormal = (Image) null;
      this.btnForumBanClear.ImagePressed = (Image) null;
      this.btnForumBanClear.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnForumBanClear.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnForumBanClear.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnForumBanClear.Location = new Point(178, 89);
      this.btnForumBanClear.Name = "btnForumBanClear";
      this.btnForumBanClear.OffsetPressedContent = true;
      this.btnForumBanClear.Padding2 = 5;
      this.btnForumBanClear.Size = new Size(30, 23);
      this.btnForumBanClear.StretchImage = false;
      this.btnForumBanClear.TabIndex = 18;
      this.btnForumBanClear.Text = "Clr";
      this.btnForumBanClear.TextDropShadow = false;
      this.btnForumBanClear.UseVisualStyleBackColor = true;
      this.btnForumBanClear.Click += new EventHandler(this.btnForumBanClear_Click);
      this.btnForumBanPerma.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnForumBanPerma.BorderDrawing = true;
      this.btnForumBanPerma.FocusRectangleEnabled = false;
      this.btnForumBanPerma.Image = (Image) null;
      this.btnForumBanPerma.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnForumBanPerma.ImageBorderEnabled = true;
      this.btnForumBanPerma.ImageDropShadow = true;
      this.btnForumBanPerma.ImageFocused = (Image) null;
      this.btnForumBanPerma.ImageInactive = (Image) null;
      this.btnForumBanPerma.ImageMouseOver = (Image) null;
      this.btnForumBanPerma.ImageNormal = (Image) null;
      this.btnForumBanPerma.ImagePressed = (Image) null;
      this.btnForumBanPerma.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnForumBanPerma.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnForumBanPerma.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnForumBanPerma.Location = new Point(138, 89);
      this.btnForumBanPerma.Name = "btnForumBanPerma";
      this.btnForumBanPerma.OffsetPressedContent = true;
      this.btnForumBanPerma.Padding2 = 5;
      this.btnForumBanPerma.Size = new Size(34, 23);
      this.btnForumBanPerma.StretchImage = false;
      this.btnForumBanPerma.TabIndex = 17;
      this.btnForumBanPerma.Text = "Prm";
      this.btnForumBanPerma.TextDropShadow = false;
      this.btnForumBanPerma.UseVisualStyleBackColor = true;
      this.btnForumBanPerma.Click += new EventHandler(this.btnForumBanPerma_Click);
      this.btnForumBan7.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnForumBan7.BorderDrawing = true;
      this.btnForumBan7.FocusRectangleEnabled = false;
      this.btnForumBan7.Image = (Image) null;
      this.btnForumBan7.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnForumBan7.ImageBorderEnabled = true;
      this.btnForumBan7.ImageDropShadow = true;
      this.btnForumBan7.ImageFocused = (Image) null;
      this.btnForumBan7.ImageInactive = (Image) null;
      this.btnForumBan7.ImageMouseOver = (Image) null;
      this.btnForumBan7.ImageNormal = (Image) null;
      this.btnForumBan7.ImagePressed = (Image) null;
      this.btnForumBan7.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnForumBan7.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnForumBan7.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnForumBan7.Location = new Point(107, 89);
      this.btnForumBan7.Name = "btnForumBan7";
      this.btnForumBan7.OffsetPressedContent = true;
      this.btnForumBan7.Padding2 = 5;
      this.btnForumBan7.Size = new Size(25, 23);
      this.btnForumBan7.StretchImage = false;
      this.btnForumBan7.TabIndex = 16;
      this.btnForumBan7.Text = "7";
      this.btnForumBan7.TextDropShadow = false;
      this.btnForumBan7.UseVisualStyleBackColor = true;
      this.btnForumBan7.Click += new EventHandler(this.btnForumBan7_Click);
      this.btnForumBan3.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnForumBan3.BorderDrawing = true;
      this.btnForumBan3.FocusRectangleEnabled = false;
      this.btnForumBan3.Image = (Image) null;
      this.btnForumBan3.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnForumBan3.ImageBorderEnabled = true;
      this.btnForumBan3.ImageDropShadow = true;
      this.btnForumBan3.ImageFocused = (Image) null;
      this.btnForumBan3.ImageInactive = (Image) null;
      this.btnForumBan3.ImageMouseOver = (Image) null;
      this.btnForumBan3.ImageNormal = (Image) null;
      this.btnForumBan3.ImagePressed = (Image) null;
      this.btnForumBan3.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnForumBan3.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnForumBan3.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnForumBan3.Location = new Point(76, 89);
      this.btnForumBan3.Name = "btnForumBan3";
      this.btnForumBan3.OffsetPressedContent = true;
      this.btnForumBan3.Padding2 = 5;
      this.btnForumBan3.Size = new Size(25, 23);
      this.btnForumBan3.StretchImage = false;
      this.btnForumBan3.TabIndex = 15;
      this.btnForumBan3.Text = "3";
      this.btnForumBan3.TextDropShadow = false;
      this.btnForumBan3.UseVisualStyleBackColor = true;
      this.btnForumBan3.Click += new EventHandler(this.btnForumBan3_Click);
      this.label8.AutoSize = true;
      this.label8.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label8.Location = new Point(8, 94);
      this.label8.Name = "label8";
      this.label8.Size = new Size(36, 13);
      this.label8.TabIndex = 14;
      this.label8.Text = "Forum";
      this.btnForumBan1.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnForumBan1.BorderDrawing = true;
      this.btnForumBan1.FocusRectangleEnabled = false;
      this.btnForumBan1.Image = (Image) null;
      this.btnForumBan1.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnForumBan1.ImageBorderEnabled = true;
      this.btnForumBan1.ImageDropShadow = true;
      this.btnForumBan1.ImageFocused = (Image) null;
      this.btnForumBan1.ImageInactive = (Image) null;
      this.btnForumBan1.ImageMouseOver = (Image) null;
      this.btnForumBan1.ImageNormal = (Image) null;
      this.btnForumBan1.ImagePressed = (Image) null;
      this.btnForumBan1.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnForumBan1.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnForumBan1.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnForumBan1.Location = new Point(45, 89);
      this.btnForumBan1.Name = "btnForumBan1";
      this.btnForumBan1.OffsetPressedContent = true;
      this.btnForumBan1.Padding2 = 5;
      this.btnForumBan1.Size = new Size(25, 23);
      this.btnForumBan1.StretchImage = false;
      this.btnForumBan1.TabIndex = 13;
      this.btnForumBan1.Text = "1";
      this.btnForumBan1.TextDropShadow = false;
      this.btnForumBan1.UseVisualStyleBackColor = true;
      this.btnForumBan1.Click += new EventHandler(this.btnForumBan1_Click);
      this.btnMailBanClear.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnMailBanClear.BorderDrawing = true;
      this.btnMailBanClear.FocusRectangleEnabled = false;
      this.btnMailBanClear.Image = (Image) null;
      this.btnMailBanClear.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnMailBanClear.ImageBorderEnabled = true;
      this.btnMailBanClear.ImageDropShadow = true;
      this.btnMailBanClear.ImageFocused = (Image) null;
      this.btnMailBanClear.ImageInactive = (Image) null;
      this.btnMailBanClear.ImageMouseOver = (Image) null;
      this.btnMailBanClear.ImageNormal = (Image) null;
      this.btnMailBanClear.ImagePressed = (Image) null;
      this.btnMailBanClear.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnMailBanClear.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnMailBanClear.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnMailBanClear.Location = new Point(178, 60);
      this.btnMailBanClear.Name = "btnMailBanClear";
      this.btnMailBanClear.OffsetPressedContent = true;
      this.btnMailBanClear.Padding2 = 5;
      this.btnMailBanClear.Size = new Size(30, 23);
      this.btnMailBanClear.StretchImage = false;
      this.btnMailBanClear.TabIndex = 12;
      this.btnMailBanClear.Text = "Clr";
      this.btnMailBanClear.TextDropShadow = false;
      this.btnMailBanClear.UseVisualStyleBackColor = true;
      this.btnMailBanClear.Click += new EventHandler(this.btnMailBanClear_Click);
      this.btnMailBanPerma.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnMailBanPerma.BorderDrawing = true;
      this.btnMailBanPerma.FocusRectangleEnabled = false;
      this.btnMailBanPerma.Image = (Image) null;
      this.btnMailBanPerma.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnMailBanPerma.ImageBorderEnabled = true;
      this.btnMailBanPerma.ImageDropShadow = true;
      this.btnMailBanPerma.ImageFocused = (Image) null;
      this.btnMailBanPerma.ImageInactive = (Image) null;
      this.btnMailBanPerma.ImageMouseOver = (Image) null;
      this.btnMailBanPerma.ImageNormal = (Image) null;
      this.btnMailBanPerma.ImagePressed = (Image) null;
      this.btnMailBanPerma.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnMailBanPerma.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnMailBanPerma.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnMailBanPerma.Location = new Point(138, 60);
      this.btnMailBanPerma.Name = "btnMailBanPerma";
      this.btnMailBanPerma.OffsetPressedContent = true;
      this.btnMailBanPerma.Padding2 = 5;
      this.btnMailBanPerma.Size = new Size(34, 23);
      this.btnMailBanPerma.StretchImage = false;
      this.btnMailBanPerma.TabIndex = 11;
      this.btnMailBanPerma.Text = "Prm";
      this.btnMailBanPerma.TextDropShadow = false;
      this.btnMailBanPerma.UseVisualStyleBackColor = true;
      this.btnMailBanPerma.Click += new EventHandler(this.btnMailBanPerma_Click);
      this.btnMailBan7.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnMailBan7.BorderDrawing = true;
      this.btnMailBan7.FocusRectangleEnabled = false;
      this.btnMailBan7.Image = (Image) null;
      this.btnMailBan7.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnMailBan7.ImageBorderEnabled = true;
      this.btnMailBan7.ImageDropShadow = true;
      this.btnMailBan7.ImageFocused = (Image) null;
      this.btnMailBan7.ImageInactive = (Image) null;
      this.btnMailBan7.ImageMouseOver = (Image) null;
      this.btnMailBan7.ImageNormal = (Image) null;
      this.btnMailBan7.ImagePressed = (Image) null;
      this.btnMailBan7.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnMailBan7.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnMailBan7.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnMailBan7.Location = new Point(107, 60);
      this.btnMailBan7.Name = "btnMailBan7";
      this.btnMailBan7.OffsetPressedContent = true;
      this.btnMailBan7.Padding2 = 5;
      this.btnMailBan7.Size = new Size(25, 23);
      this.btnMailBan7.StretchImage = false;
      this.btnMailBan7.TabIndex = 10;
      this.btnMailBan7.Text = "7";
      this.btnMailBan7.TextDropShadow = false;
      this.btnMailBan7.UseVisualStyleBackColor = true;
      this.btnMailBan7.Click += new EventHandler(this.btnMailBan7_Click);
      this.btnMailBan3.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnMailBan3.BorderDrawing = true;
      this.btnMailBan3.FocusRectangleEnabled = false;
      this.btnMailBan3.Image = (Image) null;
      this.btnMailBan3.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnMailBan3.ImageBorderEnabled = true;
      this.btnMailBan3.ImageDropShadow = true;
      this.btnMailBan3.ImageFocused = (Image) null;
      this.btnMailBan3.ImageInactive = (Image) null;
      this.btnMailBan3.ImageMouseOver = (Image) null;
      this.btnMailBan3.ImageNormal = (Image) null;
      this.btnMailBan3.ImagePressed = (Image) null;
      this.btnMailBan3.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnMailBan3.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnMailBan3.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnMailBan3.Location = new Point(76, 60);
      this.btnMailBan3.Name = "btnMailBan3";
      this.btnMailBan3.OffsetPressedContent = true;
      this.btnMailBan3.Padding2 = 5;
      this.btnMailBan3.Size = new Size(25, 23);
      this.btnMailBan3.StretchImage = false;
      this.btnMailBan3.TabIndex = 9;
      this.btnMailBan3.Text = "3";
      this.btnMailBan3.TextDropShadow = false;
      this.btnMailBan3.UseVisualStyleBackColor = true;
      this.btnMailBan3.Click += new EventHandler(this.btnMailBan3_Click);
      this.label7.AutoSize = true;
      this.label7.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label7.Location = new Point(8, 65);
      this.label7.Name = "label7";
      this.label7.Size = new Size(26, 13);
      this.label7.TabIndex = 8;
      this.label7.Text = "Mail";
      this.btnMailBan1.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnMailBan1.BorderDrawing = true;
      this.btnMailBan1.FocusRectangleEnabled = false;
      this.btnMailBan1.Image = (Image) null;
      this.btnMailBan1.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnMailBan1.ImageBorderEnabled = true;
      this.btnMailBan1.ImageDropShadow = true;
      this.btnMailBan1.ImageFocused = (Image) null;
      this.btnMailBan1.ImageInactive = (Image) null;
      this.btnMailBan1.ImageMouseOver = (Image) null;
      this.btnMailBan1.ImageNormal = (Image) null;
      this.btnMailBan1.ImagePressed = (Image) null;
      this.btnMailBan1.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnMailBan1.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnMailBan1.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnMailBan1.Location = new Point(45, 60);
      this.btnMailBan1.Name = "btnMailBan1";
      this.btnMailBan1.OffsetPressedContent = true;
      this.btnMailBan1.Padding2 = 5;
      this.btnMailBan1.Size = new Size(25, 23);
      this.btnMailBan1.StretchImage = false;
      this.btnMailBan1.TabIndex = 7;
      this.btnMailBan1.Text = "1";
      this.btnMailBan1.TextDropShadow = false;
      this.btnMailBan1.UseVisualStyleBackColor = true;
      this.btnMailBan1.Click += new EventHandler(this.btnMailBan1_Click);
      this.btnChatBanClear.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnChatBanClear.BorderDrawing = true;
      this.btnChatBanClear.FocusRectangleEnabled = false;
      this.btnChatBanClear.Image = (Image) null;
      this.btnChatBanClear.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnChatBanClear.ImageBorderEnabled = true;
      this.btnChatBanClear.ImageDropShadow = true;
      this.btnChatBanClear.ImageFocused = (Image) null;
      this.btnChatBanClear.ImageInactive = (Image) null;
      this.btnChatBanClear.ImageMouseOver = (Image) null;
      this.btnChatBanClear.ImageNormal = (Image) null;
      this.btnChatBanClear.ImagePressed = (Image) null;
      this.btnChatBanClear.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnChatBanClear.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnChatBanClear.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnChatBanClear.Location = new Point(178, 31);
      this.btnChatBanClear.Name = "btnChatBanClear";
      this.btnChatBanClear.OffsetPressedContent = true;
      this.btnChatBanClear.Padding2 = 5;
      this.btnChatBanClear.Size = new Size(30, 23);
      this.btnChatBanClear.StretchImage = false;
      this.btnChatBanClear.TabIndex = 6;
      this.btnChatBanClear.Text = "Clr";
      this.btnChatBanClear.TextDropShadow = false;
      this.btnChatBanClear.UseVisualStyleBackColor = true;
      this.btnChatBanClear.Click += new EventHandler(this.btnChatBanClear_Click);
      this.btnChatBanPerma.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnChatBanPerma.BorderDrawing = true;
      this.btnChatBanPerma.FocusRectangleEnabled = false;
      this.btnChatBanPerma.Image = (Image) null;
      this.btnChatBanPerma.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnChatBanPerma.ImageBorderEnabled = true;
      this.btnChatBanPerma.ImageDropShadow = true;
      this.btnChatBanPerma.ImageFocused = (Image) null;
      this.btnChatBanPerma.ImageInactive = (Image) null;
      this.btnChatBanPerma.ImageMouseOver = (Image) null;
      this.btnChatBanPerma.ImageNormal = (Image) null;
      this.btnChatBanPerma.ImagePressed = (Image) null;
      this.btnChatBanPerma.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnChatBanPerma.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnChatBanPerma.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnChatBanPerma.Location = new Point(138, 31);
      this.btnChatBanPerma.Name = "btnChatBanPerma";
      this.btnChatBanPerma.OffsetPressedContent = true;
      this.btnChatBanPerma.Padding2 = 5;
      this.btnChatBanPerma.Size = new Size(34, 23);
      this.btnChatBanPerma.StretchImage = false;
      this.btnChatBanPerma.TabIndex = 5;
      this.btnChatBanPerma.Text = "Prm";
      this.btnChatBanPerma.TextDropShadow = false;
      this.btnChatBanPerma.UseVisualStyleBackColor = true;
      this.btnChatBanPerma.Click += new EventHandler(this.btnChatBanPerma_Click);
      this.btnChatBan7.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnChatBan7.BorderDrawing = true;
      this.btnChatBan7.FocusRectangleEnabled = false;
      this.btnChatBan7.Image = (Image) null;
      this.btnChatBan7.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnChatBan7.ImageBorderEnabled = true;
      this.btnChatBan7.ImageDropShadow = true;
      this.btnChatBan7.ImageFocused = (Image) null;
      this.btnChatBan7.ImageInactive = (Image) null;
      this.btnChatBan7.ImageMouseOver = (Image) null;
      this.btnChatBan7.ImageNormal = (Image) null;
      this.btnChatBan7.ImagePressed = (Image) null;
      this.btnChatBan7.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnChatBan7.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnChatBan7.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnChatBan7.Location = new Point(107, 31);
      this.btnChatBan7.Name = "btnChatBan7";
      this.btnChatBan7.OffsetPressedContent = true;
      this.btnChatBan7.Padding2 = 5;
      this.btnChatBan7.Size = new Size(25, 23);
      this.btnChatBan7.StretchImage = false;
      this.btnChatBan7.TabIndex = 4;
      this.btnChatBan7.Text = "7";
      this.btnChatBan7.TextDropShadow = false;
      this.btnChatBan7.UseVisualStyleBackColor = true;
      this.btnChatBan7.Click += new EventHandler(this.btnChatBan7_Click);
      this.btnChatBan3.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnChatBan3.BorderDrawing = true;
      this.btnChatBan3.FocusRectangleEnabled = false;
      this.btnChatBan3.Image = (Image) null;
      this.btnChatBan3.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnChatBan3.ImageBorderEnabled = true;
      this.btnChatBan3.ImageDropShadow = true;
      this.btnChatBan3.ImageFocused = (Image) null;
      this.btnChatBan3.ImageInactive = (Image) null;
      this.btnChatBan3.ImageMouseOver = (Image) null;
      this.btnChatBan3.ImageNormal = (Image) null;
      this.btnChatBan3.ImagePressed = (Image) null;
      this.btnChatBan3.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnChatBan3.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnChatBan3.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnChatBan3.Location = new Point(76, 31);
      this.btnChatBan3.Name = "btnChatBan3";
      this.btnChatBan3.OffsetPressedContent = true;
      this.btnChatBan3.Padding2 = 5;
      this.btnChatBan3.Size = new Size(25, 23);
      this.btnChatBan3.StretchImage = false;
      this.btnChatBan3.TabIndex = 3;
      this.btnChatBan3.Text = "3";
      this.btnChatBan3.TextDropShadow = false;
      this.btnChatBan3.UseVisualStyleBackColor = true;
      this.btnChatBan3.Click += new EventHandler(this.btnChatBan3_Click);
      this.label6.AutoSize = true;
      this.label6.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label6.Location = new Point(8, 36);
      this.label6.Name = "label6";
      this.label6.Size = new Size(29, 13);
      this.label6.TabIndex = 2;
      this.label6.Text = "Chat";
      this.btnChatBan1.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnChatBan1.BorderDrawing = true;
      this.btnChatBan1.FocusRectangleEnabled = false;
      this.btnChatBan1.Image = (Image) null;
      this.btnChatBan1.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnChatBan1.ImageBorderEnabled = true;
      this.btnChatBan1.ImageDropShadow = true;
      this.btnChatBan1.ImageFocused = (Image) null;
      this.btnChatBan1.ImageInactive = (Image) null;
      this.btnChatBan1.ImageMouseOver = (Image) null;
      this.btnChatBan1.ImageNormal = (Image) null;
      this.btnChatBan1.ImagePressed = (Image) null;
      this.btnChatBan1.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnChatBan1.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnChatBan1.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnChatBan1.Location = new Point(45, 31);
      this.btnChatBan1.Name = "btnChatBan1";
      this.btnChatBan1.OffsetPressedContent = true;
      this.btnChatBan1.Padding2 = 5;
      this.btnChatBan1.Size = new Size(25, 23);
      this.btnChatBan1.StretchImage = false;
      this.btnChatBan1.TabIndex = 1;
      this.btnChatBan1.Text = "1";
      this.btnChatBan1.TextDropShadow = false;
      this.btnChatBan1.UseVisualStyleBackColor = true;
      this.btnChatBan1.Click += new EventHandler(this.btnChatBan1_Click);
      this.label4.AutoSize = true;
      this.label4.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label4.Location = new Point(6, 15);
      this.label4.Name = "label4";
      this.label4.Size = new Size(64, 13);
      this.label4.TabIndex = 0;
      this.label4.Text = "Bans (Days)";
      this.btnEditAvatar.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnEditAvatar.BorderDrawing = true;
      this.btnEditAvatar.FocusRectangleEnabled = false;
      this.btnEditAvatar.Image = (Image) null;
      this.btnEditAvatar.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnEditAvatar.ImageBorderEnabled = true;
      this.btnEditAvatar.ImageDropShadow = true;
      this.btnEditAvatar.ImageFocused = (Image) null;
      this.btnEditAvatar.ImageInactive = (Image) null;
      this.btnEditAvatar.ImageMouseOver = (Image) null;
      this.btnEditAvatar.ImageNormal = (Image) null;
      this.btnEditAvatar.ImagePressed = (Image) null;
      this.btnEditAvatar.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnEditAvatar.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnEditAvatar.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnEditAvatar.Location = new Point(627, 521);
      this.btnEditAvatar.Name = "btnEditAvatar";
      this.btnEditAvatar.OffsetPressedContent = true;
      this.btnEditAvatar.Padding2 = 5;
      this.btnEditAvatar.Size = new Size(131, 23);
      this.btnEditAvatar.StretchImage = false;
      this.btnEditAvatar.TabIndex = 26;
      this.btnEditAvatar.Text = "Edit Avatar";
      this.btnEditAvatar.TextDropShadow = false;
      this.btnEditAvatar.UseVisualStyleBackColor = true;
      this.btnEditAvatar.Visible = false;
      this.btnEditAvatar.Click += new EventHandler(this.bitmapButton1_Click);
      this.btnInviteToFaction.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnInviteToFaction.BorderDrawing = true;
      this.btnInviteToFaction.FocusRectangleEnabled = false;
      this.btnInviteToFaction.Image = (Image) null;
      this.btnInviteToFaction.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnInviteToFaction.ImageBorderEnabled = true;
      this.btnInviteToFaction.ImageDropShadow = true;
      this.btnInviteToFaction.ImageFocused = (Image) null;
      this.btnInviteToFaction.ImageInactive = (Image) null;
      this.btnInviteToFaction.ImageMouseOver = (Image) null;
      this.btnInviteToFaction.ImageNormal = (Image) null;
      this.btnInviteToFaction.ImagePressed = (Image) null;
      this.btnInviteToFaction.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnInviteToFaction.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnInviteToFaction.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnInviteToFaction.Location = new Point(479, 97);
      this.btnInviteToFaction.Name = "btnInviteToFaction";
      this.btnInviteToFaction.OffsetPressedContent = true;
      this.btnInviteToFaction.Padding2 = 5;
      this.btnInviteToFaction.Size = new Size(131, 33);
      this.btnInviteToFaction.StretchImage = false;
      this.btnInviteToFaction.TabIndex = 25;
      this.btnInviteToFaction.Text = "Invite To Faction";
      this.btnInviteToFaction.TextDropShadow = false;
      this.btnInviteToFaction.UseVisualStyleBackColor = true;
      this.btnInviteToFaction.Visible = false;
      this.btnInviteToFaction.Click += new EventHandler(this.btnInviteToFaction_Click);
      this.btnAchievements.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnAchievements.BorderDrawing = true;
      this.btnAchievements.FocusRectangleEnabled = false;
      this.btnAchievements.Image = (Image) null;
      this.btnAchievements.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnAchievements.ImageBorderEnabled = true;
      this.btnAchievements.ImageDropShadow = true;
      this.btnAchievements.ImageFocused = (Image) null;
      this.btnAchievements.ImageInactive = (Image) null;
      this.btnAchievements.ImageMouseOver = (Image) null;
      this.btnAchievements.ImageNormal = (Image) null;
      this.btnAchievements.ImagePressed = (Image) null;
      this.btnAchievements.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnAchievements.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnAchievements.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnAchievements.Location = new Point(479, 58);
      this.btnAchievements.Name = "btnAchievements";
      this.btnAchievements.OffsetPressedContent = true;
      this.btnAchievements.Padding2 = 5;
      this.btnAchievements.Size = new Size(131, 33);
      this.btnAchievements.StretchImage = false;
      this.btnAchievements.TabIndex = 24;
      this.btnAchievements.Text = "Achievements";
      this.btnAchievements.TextDropShadow = false;
      this.btnAchievements.UseVisualStyleBackColor = true;
      this.btnAchievements.Click += new EventHandler(this.btnAchievements_Click);
      this.btnSendMail.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnSendMail.BorderDrawing = true;
      this.btnSendMail.FocusRectangleEnabled = false;
      this.btnSendMail.Image = (Image) null;
      this.btnSendMail.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnSendMail.ImageBorderEnabled = true;
      this.btnSendMail.ImageDropShadow = true;
      this.btnSendMail.ImageFocused = (Image) null;
      this.btnSendMail.ImageInactive = (Image) null;
      this.btnSendMail.ImageMouseOver = (Image) null;
      this.btnSendMail.ImageNormal = (Image) null;
      this.btnSendMail.ImagePressed = (Image) null;
      this.btnSendMail.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnSendMail.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnSendMail.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnSendMail.Location = new Point(479, 19);
      this.btnSendMail.Name = "btnSendMail";
      this.btnSendMail.OffsetPressedContent = true;
      this.btnSendMail.Padding2 = 5;
      this.btnSendMail.Size = new Size(131, 33);
      this.btnSendMail.StretchImage = false;
      this.btnSendMail.TabIndex = 20;
      this.btnSendMail.Text = "Send Mail";
      this.btnSendMail.TextDropShadow = false;
      this.btnSendMail.UseVisualStyleBackColor = true;
      this.btnSendMail.Click += new EventHandler(this.btnSendMail_Click);
      this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnClose.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnClose.BorderDrawing = true;
      this.btnClose.FocusRectangleEnabled = false;
      this.btnClose.Image = (Image) null;
      this.btnClose.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnClose.ImageBorderEnabled = true;
      this.btnClose.ImageDropShadow = true;
      this.btnClose.ImageFocused = (Image) null;
      this.btnClose.ImageInactive = (Image) null;
      this.btnClose.ImageMouseOver = (Image) null;
      this.btnClose.ImageNormal = (Image) null;
      this.btnClose.ImagePressed = (Image) null;
      this.btnClose.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnClose.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnClose.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnClose.Location = new Point(695, 531);
      this.btnClose.Name = "btnClose";
      this.btnClose.OffsetPressedContent = true;
      this.btnClose.Padding2 = 5;
      this.btnClose.Size = new Size(75, 23);
      this.btnClose.StretchImage = false;
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.TextDropShadow = false;
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.btnGiveQuests.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnGiveQuests.BorderDrawing = true;
      this.btnGiveQuests.FocusRectangleEnabled = false;
      this.btnGiveQuests.Image = (Image) null;
      this.btnGiveQuests.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnGiveQuests.ImageBorderEnabled = true;
      this.btnGiveQuests.ImageDropShadow = true;
      this.btnGiveQuests.ImageFocused = (Image) null;
      this.btnGiveQuests.ImageInactive = (Image) null;
      this.btnGiveQuests.ImageMouseOver = (Image) null;
      this.btnGiveQuests.ImageNormal = (Image) null;
      this.btnGiveQuests.ImagePressed = (Image) null;
      this.btnGiveQuests.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnGiveQuests.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnGiveQuests.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnGiveQuests.Location = new Point((int) sbyte.MaxValue, 302);
      this.btnGiveQuests.Name = "btnGiveQuests";
      this.btnGiveQuests.OffsetPressedContent = true;
      this.btnGiveQuests.Padding2 = 5;
      this.btnGiveQuests.Size = new Size(108, 23);
      this.btnGiveQuests.StretchImage = false;
      this.btnGiveQuests.TabIndex = 42;
      this.btnGiveQuests.Text = "Give Quests";
      this.btnGiveQuests.TextDropShadow = false;
      this.btnGiveQuests.UseVisualStyleBackColor = true;
      this.btnGiveQuests.Click += new EventHandler(this.btnGiveQuests_Click);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(159, 180, 193);
      this.Controls.Add((Control) this.btnEditAvatar);
      this.Controls.Add((Control) this.btnInviteToFaction);
      this.Controls.Add((Control) this.btnAchievements);
      this.Controls.Add((Control) this.gbModerator);
      this.Controls.Add((Control) this.lblIsModerator);
      this.Controls.Add((Control) this.lblIsAdmin);
      this.Controls.Add((Control) this.btnSendMail);
      this.Controls.Add((Control) this.imgAvatar);
      this.Controls.Add((Control) this.lblFaction);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.lblStanding);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.lblPoints);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.lblRank);
      this.Controls.Add((Control) this.lblUserName);
      this.Controls.Add((Control) this.pnlVillages);
      this.Controls.Add((Control) this.btnClose);
      this.MinimumSize = new Size(792, 566);
      this.Name = nameof (UserInfoScreen);
      this.Size = new Size(792, 566);
      this.Paint += new PaintEventHandler(this.UserInfoScreen_Paint);
      this.Resize += new EventHandler(this.UserInfoScreen_Resize);
      this.gbModerator.ResumeLayout(false);
      this.gbModerator.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
