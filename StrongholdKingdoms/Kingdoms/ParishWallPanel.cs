// Decompiled with JetBrains decompiler
// Type: Kingdoms.ParishWallPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ParishWallPanel : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private TextBox textBox1;
    private Panel focusPanel;
    public static ParishWallPanel instance = (ParishWallPanel) null;
    private SparseArray parishList = new SparseArray();
    private DateTime lastChatUpdate = DateTime.MinValue;
    private bool initialTextInTextbox = true;
    private int m_currentVillage = -1;
    private int currentParish = -1;
    private int electedLeaderID = -1;
    private string electedLeaderName = "";
    private int currentLeaderID = -1;
    private string currentLeaderName = "";
    public static int m_userIDOnCurrent = -1;
    private DateTime lastRequestTime = DateTime.MinValue;
    private int checkTextUpdateTime = 5;
    private bool forceNextUpdate;
    private CustomSelfDrawPanel.ParishChatPanel[] chatAreas;
    private int[] unreadMessages = new int[6];
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage illustrationImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel stewardLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDExtendingPanel wallInfoImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDImage textInputImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton tab1Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tab2Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tab3Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tab4Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tab5Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tab6Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDExtendingPanel areaWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
    private bool inSend;
    private int lastTab = -1;
    private List<CustomSelfDrawPanel.ParishWallEntry> lineList = new List<CustomSelfDrawPanel.ParishWallEntry>();
    private WallInfo[] origWallInfo;
    private List<WallInfo> wallList = new List<WallInfo>();

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
      this.textBox1 = new TextBox();
      this.focusPanel = new Panel();
      this.SuspendLayout();
      this.textBox1.BackColor = Color.FromArgb(134, 153, 165);
      this.textBox1.BorderStyle = BorderStyle.None;
      this.textBox1.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.textBox1.ForeColor = ARGBColors.Black;
      this.textBox1.Location = new Point(439, 94);
      this.textBox1.MaxLength = 100;
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(461, 18);
      this.textBox1.TabIndex = 99;
      this.textBox1.Text = "Enter text here";
      this.textBox1.WordWrap = false;
      this.textBox1.KeyPress += new KeyPressEventHandler(this.textBox1_KeyPress);
      this.textBox1.Enter += new EventHandler(this.textBox1_Enter);
      this.focusPanel.BackColor = ARGBColors.Transparent;
      this.focusPanel.ForeColor = ARGBColors.Transparent;
      this.focusPanel.Location = new Point(988, 3);
      this.focusPanel.Name = "focusPanel";
      this.focusPanel.Size = new Size(1, 1);
      this.focusPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.focusPanel);
      this.Controls.Add((Control) this.textBox1);
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (ParishWallPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public ParishWallPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.textBox1.Font = FontManager.GetFont("Microsoft Sans Serif", 9.75f);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.focusPanel.Focus();
    }

    public void init(bool resized)
    {
      int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
      this.m_currentVillage = selectedMenuVillage;
      int parishFromVillageId = GameEngine.Instance.World.getParishFromVillageID(selectedMenuVillage);
      int height = this.Height;
      ParishWallPanel.instance = this;
      this.clearControls();
      this.headerImage.Size = new Size(this.Width, 40);
      this.headerImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage);
      this.headerImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.headerImage, 14, new Point(this.Width - 44, 3));
      this.backgroundImage.Size = new Size(this.Width, height - 40);
      this.backgroundImage.Position = new Point(0, 40);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
      this.backgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
      this.parishNameLabel.Text = GameEngine.Instance.World.getParishName(parishFromVillageId);
      this.parishNameLabel.Color = ARGBColors.White;
      this.parishNameLabel.DropShadowColor = ARGBColors.Black;
      this.parishNameLabel.Position = new Point(20, 0);
      this.parishNameLabel.Size = new Size(this.Width - 40, 40);
      this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
      this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.parishNameLabel);
      this.illustrationImage.Image = (Image) GFXLibrary.parishwall_village_illlustration_01;
      this.illustrationImage.Position = new Point(17, 5);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.illustrationImage);
      this.stewardLabel.Text = SK.Text("ParishWallPanel_Steward", "Steward") + " : ";
      this.stewardLabel.Color = ARGBColors.Black;
      this.stewardLabel.Position = new Point(5, 5);
      this.stewardLabel.Size = new Size(this.illustrationImage.Width - 6, 30);
      this.stewardLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.stewardLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.illustrationImage.addControl((CustomSelfDrawPanel.CSDControl) this.stewardLabel);
      this.wallInfoImage.Size = new Size(396, height - 170);
      this.wallInfoImage.Position = new Point(8, 119);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallInfoImage);
      this.wallInfoImage.Create((Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_right);
      this.areaWindow.Size = new Size(564, height - 78);
      this.areaWindow.Position = new Point(411, 26);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.areaWindow);
      this.areaWindow.Create((Image) GFXLibrary.parishwall_village_center_tab_outline_top_left, (Image) GFXLibrary.parishwall_village_center_tab_outline_top_middle, (Image) GFXLibrary.parishwall_village_center_tab_outline_top_right, (Image) GFXLibrary.parishwall_village_center_tab_outline_middle_left, (Image) null, (Image) GFXLibrary.parishwall_village_center_tab_outline_middle_right, (Image) GFXLibrary.parishwall_village_center_tab_outline_bottom_left, (Image) GFXLibrary.parishwall_village_center_tab_outline_bottom_middle, (Image) GFXLibrary.parishwall_village_center_tab_outline_bottom_right);
      this.tab1Button.UseTextSize = true;
      this.tab1Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_up;
      this.tab1Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_up;
      this.tab1Button.Position = new Point(425, 6);
      this.tab1Button.Text.Text = SK.Text("ParishWallPanel_General", "General");
      this.tab1Button.Text.Size = new Size(this.tab1Button.Size.Width, this.tab1Button.Text.Size.Height + 20);
      this.tab1Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.tab1Button.TextYOffset = 3;
      this.tab1Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.tab1Button.Text.Color = ARGBColors.Black;
      this.tab1Button.Data = 0;
      this.tab1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClick), "ParishWallPanel_tab");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tab1Button);
      this.tab2Button.UseTextSize = true;
      this.tab2Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab2Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab2Button.Position = new Point(510, 6);
      this.tab2Button.Text.Text = SK.Text("ParishWallPanel_War", "War");
      this.tab2Button.Text.Size = new Size(this.tab2Button.Size.Width, this.tab2Button.Text.Size.Height + 20);
      this.tab2Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.tab2Button.TextYOffset = 3;
      this.tab2Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tab2Button.Text.Color = ARGBColors.Black;
      this.tab2Button.Data = 1;
      this.tab2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClick), "ParishWallPanel_tab");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tab2Button);
      this.tab3Button.UseTextSize = true;
      this.tab3Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab3Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab3Button.Position = new Point(595, 6);
      this.tab3Button.Text.Text = SK.Text("ParishWallPanel_inn", "Inn");
      this.tab3Button.Text.Size = new Size(this.tab3Button.Size.Width, this.tab3Button.Text.Size.Height + 20);
      this.tab3Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.tab3Button.TextYOffset = 3;
      this.tab3Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tab3Button.Text.Color = ARGBColors.Black;
      this.tab3Button.Data = 2;
      this.tab3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClick), "ParishWallPanel_tab");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tab3Button);
      this.tab4Button.UseTextSize = true;
      this.tab4Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab4Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab4Button.Position = new Point(680, 6);
      this.tab4Button.Text.Text = SK.Text("ParishWallPanel_Steward", "Steward");
      this.tab4Button.Text.Size = new Size(this.tab4Button.Size.Width, this.tab4Button.Text.Size.Height + 20);
      this.tab4Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.tab4Button.TextYOffset = 3;
      this.tab4Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tab4Button.Text.Color = ARGBColors.Black;
      this.tab4Button.Data = 3;
      this.tab4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClick), "ParishWallPanel_tab");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tab4Button);
      this.tab5Button.UseTextSize = true;
      this.tab5Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab5Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab5Button.Position = new Point(765, 6);
      this.tab5Button.Text.Text = SK.Text("ParishWallPanel_Free", "Free");
      this.tab5Button.Text.Size = new Size(this.tab5Button.Size.Width, this.tab5Button.Text.Size.Height + 20);
      this.tab5Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.tab5Button.TextYOffset = 3;
      this.tab5Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tab5Button.Text.Color = ARGBColors.Black;
      this.tab5Button.Data = 4;
      this.tab5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClick), "ParishWallPanel_tab");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tab5Button);
      this.tab6Button.UseTextSize = true;
      this.tab6Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab6Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab6Button.Position = new Point(850, 6);
      this.tab6Button.Text.Text = SK.Text("ParishWallPanel_Free", "Free");
      this.tab6Button.Text.Size = new Size(this.tab6Button.Size.Width, this.tab6Button.Text.Size.Height + 20);
      this.tab6Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.tab6Button.TextYOffset = 3;
      this.tab6Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tab6Button.Text.Color = ARGBColors.Black;
      this.tab6Button.Data = 5;
      this.tab6Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClick), "ParishWallPanel_tab");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tab6Button);
      this.textInputImage.Image = (Image) GFXLibrary.parishwall_what_say_thou_box;
      this.textInputImage.Position = new Point(432, 47);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.textInputImage);
      this.wallScrollArea.Position = new Point(15, 15);
      this.wallScrollArea.Size = new Size(337, height - 191);
      this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(337, height - 191));
      this.wallInfoImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollArea);
      int max = this.wallScrollBar.Value;
      this.wallScrollBar.Visible = false;
      this.wallScrollBar.Position = new Point(358, 15);
      this.wallScrollBar.Size = new Size(24, height - 191);
      this.wallInfoImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollBar);
      this.wallScrollBar.Value = 0;
      this.wallScrollBar.Max = 100;
      this.wallScrollBar.NumVisibleLines = 25;
      this.wallScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      if (!resized)
      {
        this.focusPanel.Focus();
        this.initialTextInTextbox = true;
        this.textBox1.Text = SK.Text("ParishWallPanel_Enter_Text_Here", "Enter Text Here");
        ParishWallPanel.StoredParishInfo parish = (ParishWallPanel.StoredParishInfo) this.parishList[parishFromVillageId];
        bool flag = false;
        if (parish == null || (DateTime.Now - parish.m_lastUpdateTime).TotalMinutes > 1.0 || parish.lastReturnData == null)
          flag = true;
        if (this.chatAreas == null)
        {
          this.chatAreas = new CustomSelfDrawPanel.ParishChatPanel[6];
          for (int index = 0; index < 6; ++index)
          {
            CustomSelfDrawPanel.ParishChatPanel parishChatPanel = new CustomSelfDrawPanel.ParishChatPanel();
            parishChatPanel.Position = new Point(20, 68);
            parishChatPanel.Size = new Size(534, height - 153);
            this.chatAreas[index] = parishChatPanel;
          }
        }
        if (this.currentParish != parishFromVillageId || this.forceNextUpdate)
        {
          this.forceNextUpdate = false;
          this.currentLeaderID = -1;
          this.electedLeaderID = -1;
          this.currentLeaderName = "";
          this.electedLeaderName = "";
          ParishWallPanel.m_userIDOnCurrent = -1;
          this.checkTextUpdateTime = 5;
          int num = 0;
          foreach (CustomSelfDrawPanel.ParishChatPanel chatArea in this.chatAreas)
          {
            this.areaWindow.addControl((CustomSelfDrawPanel.CSDControl) chatArea);
            chatArea.Visible = false;
            chatArea.reset(this, num);
            if (GameEngine.Instance.Village != null)
              chatArea.importText(GameEngine.Instance.World.getParishChat(parishFromVillageId, num, GameEngine.Instance.Village.m_ownedDate).ToArray(), false, -1L);
            chatArea.scrollToBottom();
            chatArea.Visible = false;
            ++num;
          }
          long[] readIDs = new long[6]
          {
            -1L,
            -1L,
            -1L,
            -1L,
            -1L,
            -1L
          };
          int[] numArray = GameEngine.Instance.World.setReadIDs(parishFromVillageId, readIDs);
          if (numArray != null)
          {
            for (int index = 0; index < 6; ++index)
              this.chatAreas[index].setUnreads(numArray[index]);
          }
          this.currentParish = parishFromVillageId;
          this.tabEntered(0);
        }
        else
        {
          foreach (CustomSelfDrawPanel.ParishChatPanel chatArea in this.chatAreas)
          {
            chatArea.Repopulate = true;
            chatArea.Size = new Size(534, height - 153);
            this.areaWindow.addControl((CustomSelfDrawPanel.CSDControl) chatArea);
            chatArea.Visible = false;
          }
          this.currentParish = parishFromVillageId;
          this.tabEntered(this.lastTab);
        }
        this.currentParish = parishFromVillageId;
        if (GameEngine.Instance.Village != null)
        {
          if (flag)
          {
            RemoteServices.Instance.set_GetParishFrontPageInfo_UserCallBack(new RemoteServices.GetParishFrontPageInfo_UserCallBack(this.getParishFrontPageCallback));
            RemoteServices.Instance.GetParishFrontPageInfo(this.m_currentVillage, DateTime.MinValue);
            Thread.Sleep(500);
          }
          else
          {
            DateTime lastUpdateTime = parish.m_lastUpdateTime;
            this.getParishFrontPageCallback(parish.lastReturnData);
            parish.m_lastUpdateTime = lastUpdateTime;
          }
          this.inSend = true;
          RemoteServices.Instance.set_Chat_ReceiveParishText_UserCallBack(new RemoteServices.Chat_ReceiveParishText_UserCallBack(this.chat_ReceiveParishTextCallback));
          RemoteServices.Instance.Chat_ReceiveParishText(this.currentParish, GameEngine.Instance.World.getParishChatNewestPostTime(this.currentParish, GameEngine.Instance.Village.m_ownedDate));
        }
        else
          this.forceNextUpdate = true;
      }
      else
      {
        this.updateWallArea();
        if (max > 0 && this.wallScrollBar.Visible)
        {
          if (max >= this.wallScrollBar.Max)
            max = this.wallScrollBar.Max;
          this.wallScrollBar.Value = max;
          this.wallScrollBarMoved();
        }
        int num = 0;
        foreach (CustomSelfDrawPanel.ParishChatPanel chatArea in this.chatAreas)
        {
          chatArea.Size = new Size(534, height - 153);
          this.areaWindow.addControl((CustomSelfDrawPanel.CSDControl) chatArea);
          chatArea.reset(this, num);
          if (GameEngine.Instance.Village != null)
            chatArea.importText(GameEngine.Instance.World.getParishChat(parishFromVillageId, num, GameEngine.Instance.Village.m_ownedDate).ToArray(), false, -1L);
          chatArea.scrollToBottom();
          chatArea.Visible = false;
          ++num;
        }
        this.tabEntered(this.lastTab);
      }
      this.updateLeaderInfo();
    }

    public void update()
    {
      if ((DateTime.Now - this.lastRequestTime).TotalSeconds <= (double) this.checkTextUpdateTime || this.inSend || !RemoteServices.Instance.ChatActive || GameEngine.Instance.Village == null)
        return;
      this.inSend = true;
      RemoteServices.Instance.set_Chat_ReceiveParishText_UserCallBack(new RemoteServices.Chat_ReceiveParishText_UserCallBack(this.chat_ReceiveParishTextCallback));
      RemoteServices.Instance.Chat_ReceiveParishText(this.currentParish, GameEngine.Instance.World.getParishChatNewestPostTime(this.currentParish, GameEngine.Instance.Village.m_ownedDate));
    }

    private void clearLastTabsUnreads(int pageID)
    {
      long[] readIDs = new long[6]
      {
        -1L,
        -1L,
        -1L,
        -1L,
        -1L,
        -1L
      };
      int[] numArray = GameEngine.Instance.World.setReadIDs(this.currentParish, readIDs);
      if (numArray == null || numArray[pageID] <= 0)
        return;
      long highestReadId = GameEngine.Instance.World.getHighestReadID(this.currentParish, pageID);
      RemoteServices.Instance.Chat_MarkParishTextRead(this.currentParish, pageID, highestReadId);
      readIDs[pageID] = highestReadId;
      GameEngine.Instance.World.setReadIDs(this.currentParish, readIDs);
    }

    public void leaving()
    {
      if (this.lastTab < 0)
        return;
      this.clearLastTabsUnreads(this.lastTab);
    }

    public void logout()
    {
      this.parishList.Clear();
      this.currentParish = -1;
    }

    public void flushData(int parishID)
    {
      this.parishList[parishID] = (object) null;
      GameEngine.Instance.World.flushParishWallDonation(GameEngine.Instance.World.getParishCapital(parishID), RemoteServices.Instance.UserID);
    }

    public void forceUpdateParish()
    {
    }

    private void tabClick()
    {
      if (this.ClickedControl == null)
        return;
      int data = this.ClickedControl.Data;
      if (this.lastTab != data)
        this.clearLastTabsUnreads(this.lastTab);
      this.tabEntered(data);
    }

    private void tabEntered(int pageID)
    {
      this.lastTab = pageID;
      long[] readIDs = new long[6]
      {
        -1L,
        -1L,
        -1L,
        -1L,
        -1L,
        -1L
      };
      int[] numArray = GameEngine.Instance.World.setReadIDs(this.currentParish, readIDs);
      if (numArray != null)
      {
        for (int index = 0; index < 6; ++index)
          this.chatAreas[index].setUnreads(numArray[index]);
        if (numArray[pageID] > 0)
        {
          long highestReadId = GameEngine.Instance.World.getHighestReadID(this.currentParish, pageID);
          RemoteServices.Instance.Chat_MarkParishTextRead(this.currentParish, pageID, highestReadId);
          readIDs[pageID] = highestReadId;
          GameEngine.Instance.World.setReadIDs(this.currentParish, readIDs);
        }
      }
      for (int index = 0; index < 6; ++index)
        this.chatAreas[index].Visible = index == pageID;
      this.tab1Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab1Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab2Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab2Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab3Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab3Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab4Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab4Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab5Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab5Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab6Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab6Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
      this.tab1Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tab2Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tab3Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tab4Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tab5Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tab6Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      switch (pageID)
      {
        case 0:
          this.tab1Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_up;
          this.tab1Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_up;
          this.tab1Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
          break;
        case 1:
          this.tab2Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_up;
          this.tab2Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_up;
          this.tab2Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
          break;
        case 2:
          this.tab3Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_up;
          this.tab3Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_up;
          this.tab3Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
          break;
        case 3:
          this.tab4Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_up;
          this.tab4Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_up;
          this.tab4Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
          break;
        case 4:
          this.tab5Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_up;
          this.tab5Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_up;
          this.tab5Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
          break;
        case 5:
          this.tab6Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_up;
          this.tab6Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_up;
          this.tab6Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
          break;
      }
      if (!this.chatAreas[pageID].Locked)
        this.textBox1.Enabled = true;
      else
        this.textBox1.Enabled = false;
    }

    public void setTabText(int tabID, string title)
    {
      switch (tabID)
      {
        case 0:
          this.tab1Button.Text.Text = title;
          break;
        case 1:
          this.tab2Button.Text.Text = title;
          break;
        case 2:
          this.tab3Button.Text.Text = title;
          break;
        case 3:
          this.tab4Button.Text.Text = title;
          break;
        case 4:
          this.tab5Button.Text.Text = title;
          break;
        case 5:
          this.tab6Button.Text.Text = title;
          break;
      }
    }

    public void chat_ReceiveParishTextCallback(Chat_ReceiveParishText_ReturnType returnData)
    {
      if (RemoteServices.Instance.UserOptions.profanityFilter && returnData.textList != null && returnData.textList.Count > 0)
      {
        foreach (Chat_TextEntry text in returnData.textList)
          text.text = GameEngine.Instance.censorString(text.text);
      }
      if (returnData.Success && returnData.parishID == this.currentParish && returnData.textList != null && returnData.textList.Count > 0)
      {
        this.checkTextUpdateTime = 10;
        this.importText(returnData.textList, returnData.unreadIDs);
      }
      else
        this.importText(returnData.textList, returnData.unreadIDs);
      this.checkTextUpdateTime += 2;
      if (this.checkTextUpdateTime >= 40)
        this.checkTextUpdateTime = 40;
      this.lastRequestTime = DateTime.Now;
      this.inSend = false;
    }

    private void importText(List<Chat_TextEntry> importTextList, long[] readIDs)
    {
      List<Chat_TextEntry> chatTextEntryList1 = GameEngine.Instance.World.addParishChat(this.currentParish, importTextList);
      int[] numArray = GameEngine.Instance.World.setReadIDs(this.currentParish, readIDs);
      List<Chat_TextEntry> chatTextEntryList2 = new List<Chat_TextEntry>();
      List<Chat_TextEntry> chatTextEntryList3 = new List<Chat_TextEntry>();
      List<Chat_TextEntry> chatTextEntryList4 = new List<Chat_TextEntry>();
      List<Chat_TextEntry> chatTextEntryList5 = new List<Chat_TextEntry>();
      List<Chat_TextEntry> chatTextEntryList6 = new List<Chat_TextEntry>();
      List<Chat_TextEntry> chatTextEntryList7 = new List<Chat_TextEntry>();
      if (chatTextEntryList1 != null)
      {
        foreach (Chat_TextEntry chatTextEntry in chatTextEntryList1)
        {
          switch (chatTextEntry.roomID)
          {
            case 0:
              chatTextEntryList2.Add(chatTextEntry);
              continue;
            case 1:
              chatTextEntryList3.Add(chatTextEntry);
              continue;
            case 2:
              chatTextEntryList4.Add(chatTextEntry);
              continue;
            case 3:
              chatTextEntryList5.Add(chatTextEntry);
              continue;
            case 4:
              chatTextEntryList6.Add(chatTextEntry);
              continue;
            case 5:
              chatTextEntryList7.Add(chatTextEntry);
              continue;
            default:
              continue;
          }
        }
      }
      this.chatAreas[0].importText(chatTextEntryList2.ToArray(), false, -1L);
      this.chatAreas[1].importText(chatTextEntryList3.ToArray(), false, -1L);
      this.chatAreas[2].importText(chatTextEntryList4.ToArray(), false, -1L);
      this.chatAreas[3].importText(chatTextEntryList5.ToArray(), false, -1L);
      this.chatAreas[4].importText(chatTextEntryList6.ToArray(), false, -1L);
      this.chatAreas[5].importText(chatTextEntryList7.ToArray(), false, -1L);
      for (int index = 0; index < 6; ++index)
        this.chatAreas[index].setUnreads(numArray[index]);
    }

    public void getParishFrontPageCallback(GetParishFrontPageInfo_ReturnType returnData)
    {
      if (returnData.Success)
      {
        ParishWallPanel.StoredParishInfo storedParishInfo = (ParishWallPanel.StoredParishInfo) this.parishList[returnData.parishID];
        if (storedParishInfo == null)
        {
          storedParishInfo = new ParishWallPanel.StoredParishInfo();
          this.parishList[returnData.parishID] = (object) storedParishInfo;
        }
        storedParishInfo.m_lastUpdateTime = DateTime.Now;
        storedParishInfo.lastReturnData = returnData;
        if (this.currentParish == returnData.parishID)
        {
          ParishWallPanel.m_userIDOnCurrent = -1;
          this.electedLeaderID = returnData.leaderID;
          this.electedLeaderName = returnData.leaderName;
          this.currentLeaderID = returnData.leaderID;
          this.currentLeaderName = returnData.leaderName;
          if (this.currentLeaderID == RemoteServices.Instance.UserID)
          {
            foreach (CustomSelfDrawPanel.ParishChatPanel chatArea in this.chatAreas)
              chatArea.setAsSteward();
          }
          this.createParishWall(returnData.parishWallInfo);
        }
      }
      this.updateLeaderInfo();
    }

    public void updateLeaderInfo()
    {
      this.stewardLabel.Text = SK.Text("ParishWallPanel_Steward", "Steward") + " : " + this.currentLeaderName;
      ParishWallPanel.m_userIDOnCurrent = this.currentLeaderID;
      this.update();
    }

    private void createParishWall(WallInfo[] wallInfos)
    {
      this.origWallInfo = wallInfos;
      List<WallInfo> wallInfoList = new List<WallInfo>();
      this.wallList.Clear();
      int num = 0;
      foreach (WallInfo wallInfo1 in wallInfos)
      {
        if (wallInfo1.entryType == 1)
        {
          bool flag = false;
          foreach (WallInfo wallInfo2 in wallInfoList)
          {
            if (wallInfo2.userID == wallInfo1.userID)
            {
              flag = true;
              wallInfo2.fData1 += wallInfo1.fData1;
              wallInfo2.data4 += wallInfo1.data4;
            }
          }
          if (!flag)
          {
            WallInfo wallInfo3 = new WallInfo();
            wallInfo3.data1 = wallInfo1.data1;
            wallInfo3.data2 = wallInfo1.data2;
            wallInfo3.data3 = wallInfo1.data3;
            wallInfo3.data4 = wallInfo1.data4;
            wallInfo3.fData1 = wallInfo1.fData1;
            wallInfo3.entryType = wallInfo1.entryType;
            wallInfo3.userID = wallInfo1.userID;
            wallInfo3.username = wallInfo1.username;
            wallInfoList.Add(wallInfo3);
            this.wallList.Add(wallInfo3);
          }
        }
        else
          this.wallList.Add(wallInfo1);
        ++num;
        if (num > 200)
          break;
      }
      this.updateWallArea();
    }

    public void updateWallArea()
    {
      this.wallScrollArea.clearControls();
      int num = 0;
      this.lineList.Clear();
      int lineID = 0;
      foreach (WallInfo wall in this.wallList)
      {
        CustomSelfDrawPanel.ParishWallEntry control = new CustomSelfDrawPanel.ParishWallEntry();
        if (num != 0)
          num += 5;
        control.Position = new Point(0, num);
        control.init(wall, lineID, this.m_currentVillage, (CustomSelfDrawPanel) this);
        this.wallScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num += control.Height;
        this.lineList.Add(control);
        ++lineID;
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
      this.wallScrollArea.invalidate();
      this.wallScrollBar.invalidate();
    }

    private void wallScrollBarMoved()
    {
      int y = this.wallScrollBar.Value;
      this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 15 - y);
      this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
      this.wallScrollArea.invalidate();
      this.wallScrollBar.invalidate();
    }

    public void sendParishText(string text, int id)
    {
      if (GameEngine.Instance.Village == null)
        return;
      text = text.Replace("\n", " ");
      text = text.Replace("\r", " ");
      text = text.Replace("\t", " ");
      RemoteServices.Instance.set_Chat_SendParishText_UserCallBack(new RemoteServices.Chat_SendParishText_UserCallBack(this.chat_SendParishTextCallback));
      RemoteServices.Instance.Chat_SendParishText(text, this.currentParish, id, GameEngine.Instance.World.getParishChatNewestPostTime(this.currentParish, GameEngine.Instance.Village.m_ownedDate));
    }

    public void chat_SendParishTextCallback(Chat_SendParishText_ReturnType returnData)
    {
      if (!this.chatAreas[this.lastTab].Locked)
        this.textBox1.Enabled = true;
      this.textBox1.Focus();
      if (!returnData.Success || returnData.textList == null || returnData.textList.Count <= 0)
        return;
      if (RemoteServices.Instance.UserOptions.profanityFilter)
      {
        foreach (Chat_TextEntry text in returnData.textList)
          text.text = GameEngine.Instance.censorString(text.text);
      }
      this.checkTextUpdateTime = 2;
      this.importText(returnData.textList, returnData.unreadIDs);
    }

    private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      if (this.textBox1.Text.Length > 0)
      {
        this.sendParishText(this.textBox1.Text, this.lastTab);
        this.textBox1.Text = "";
        this.textBox1.Enabled = false;
      }
      e.Handled = true;
    }

    private void textBox1_Enter(object sender, EventArgs e)
    {
      if (!this.initialTextInTextbox)
        return;
      this.initialTextInTextbox = false;
      this.textBox1.Text = "";
    }

    public void backfillPage(int pageID)
    {
      long oldestKnownID = -1;
      foreach (Chat_TextEntry chatTextEntry in GameEngine.Instance.World.getParishChat(this.currentParish, pageID, DateTime.MinValue))
      {
        if (oldestKnownID == -1L || chatTextEntry.textID < oldestKnownID)
          oldestKnownID = chatTextEntry.textID;
      }
      RemoteServices.Instance.set_Chat_BackFillParishText_UserCallBack(new RemoteServices.Chat_BackFillParishText_UserCallBack(this.chat_BackFillParishTextCallback));
      DateTime lastTime = GameEngine.Instance.Village.m_ownedDate;
      if (lastTime == DateTime.MaxValue && RemoteServices.Instance.Admin)
        lastTime = DateTime.MinValue;
      RemoteServices.Instance.Chat_BackFillParishText(this.currentParish, pageID, oldestKnownID, lastTime);
    }

    public void chat_BackFillParishTextCallback(Chat_BackFillParishText_ReturnType returnData)
    {
      if (returnData.Success && returnData.parishID == this.currentParish && returnData.textList != null)
      {
        if (RemoteServices.Instance.UserOptions.profanityFilter && returnData.textList != null && returnData.textList.Count > 0)
        {
          foreach (Chat_TextEntry text in returnData.textList)
            text.text = GameEngine.Instance.censorString(text.text);
        }
        if (returnData.textList.Count > 0)
        {
          List<Chat_TextEntry> chatTextEntryList = GameEngine.Instance.World.addParishChat(returnData.parishID, returnData.textList);
          this.chatAreas[returnData.pageID].importText(chatTextEntryList.ToArray(), true, -1L);
        }
        else
          this.chatAreas[returnData.pageID].importText(returnData.textList.ToArray(), true, -1L);
      }
      else
        this.chatAreas[returnData.pageID].freeOldMessagesButton();
    }

    public void deleteWallPost(long id)
    {
      this.chatAreas[0].importText(new Chat_TextEntry[0], false, id);
    }

    public void closing() => InterfaceMgr.Instance.closeDonatePopup();

    public class StoredParishInfo
    {
      public GetParishFrontPageInfo_ReturnType lastReturnData;
      public DateTime m_lastUpdateTime = DateTime.MinValue;
    }
  }
}
