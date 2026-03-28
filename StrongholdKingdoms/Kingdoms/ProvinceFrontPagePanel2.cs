// Decompiled with JetBrains decompiler
// Type: Kingdoms.ProvinceFrontPagePanel2
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
  public class ProvinceFrontPagePanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private Panel focusPanel;
    public static ProvinceFrontPagePanel2 instance;
    private SparseArray provinceList = new SparseArray();
    private int currentProvince = -1;
    private DateTime nextElectionTime = DateTime.MinValue;
    private int currentLeaderID = -1;
    private string currentLeaderName = "";
    private int m_userIDOnCurrent = -1;
    private int m_currentVillage = -1;
    private CustomSelfDrawPanel.CSDExtendingPanel wallInfoImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel activityLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDExtendingPanel windowImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel sheriffLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel sheriffName = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel goldLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel goldValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel taxLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel taxValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton btnChat = new CustomSelfDrawPanel.CSDButton();
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
      this.MaximumSize = new Size(992, 566);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (ProvinceFrontPagePanel2);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public ProvinceFrontPagePanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.focusPanel.Focus();
    }

    public void init()
    {
      int height = this.Height;
      ProvinceFrontPagePanel2.instance = this;
      this.clearControls();
      int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
      int provinceFromVillageId = GameEngine.Instance.World.getProvinceFromVillageID(selectedMenuVillage);
      this.headerImage.Size = new Size(this.Width, 40);
      this.headerImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage);
      this.headerImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.headerImage, 14, new Point(this.Width - 44, 3));
      this.backgroundImage.Size = new Size(this.Width, height - 40);
      this.backgroundImage.Position = new Point(0, 40);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
      this.backgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
      this.parishNameLabel.Text = SK.Text("GENERIC_Province", "Province") + " : " + GameEngine.Instance.World.getProvinceName(provinceFromVillageId) + " (" + GameEngine.Instance.World.getVillageName(selectedMenuVillage) + ")";
      this.parishNameLabel.Color = ARGBColors.White;
      this.parishNameLabel.DropShadowColor = ARGBColors.Black;
      this.parishNameLabel.Position = new Point(20, 0);
      this.parishNameLabel.Size = new Size(this.Width - 40, 40);
      this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
      this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.parishNameLabel);
      this.windowImage.Size = new Size(400, 150);
      this.windowImage.Position = new Point(493, 130);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.windowImage);
      this.windowImage.Create((Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_right);
      this.sheriffLabel.Text = SK.Text("Provinces_Current_Governor", "Current Governor");
      this.sheriffLabel.Position = new Point(30, 26);
      this.sheriffLabel.Size = new Size(250, 40);
      this.sheriffLabel.Color = ARGBColors.Black;
      this.sheriffLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.sheriffLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.windowImage.addControl((CustomSelfDrawPanel.CSDControl) this.sheriffLabel);
      this.goldLabel.Text = SK.Text("GENERIC_Current_Gold", "Current Gold");
      this.goldLabel.Position = new Point(30, 66);
      this.goldLabel.Size = new Size(250, 40);
      this.goldLabel.Color = ARGBColors.Black;
      this.goldLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.goldLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.windowImage.addControl((CustomSelfDrawPanel.CSDControl) this.goldLabel);
      this.taxLabel.Text = SK.Text("GENERIC_Tax_Rate", "Tax Rate");
      this.taxLabel.Position = new Point(30, 106);
      this.taxLabel.Size = new Size(250, 40);
      this.taxLabel.Color = ARGBColors.Black;
      this.taxLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.taxLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.windowImage.addControl((CustomSelfDrawPanel.CSDControl) this.taxLabel);
      this.sheriffName.Text = "";
      this.sheriffName.Position = new Point(170, 26);
      this.sheriffName.Size = new Size(200, 40);
      this.sheriffName.Color = ARGBColors.Black;
      this.sheriffName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.sheriffName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.windowImage.addControl((CustomSelfDrawPanel.CSDControl) this.sheriffName);
      this.goldValue.Text = "";
      this.goldValue.Position = new Point(170, 66);
      this.goldValue.Size = new Size(200, 40);
      this.goldValue.Color = ARGBColors.Black;
      this.goldValue.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.goldValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.windowImage.addControl((CustomSelfDrawPanel.CSDControl) this.goldValue);
      this.taxValue.Text = "";
      this.taxValue.Position = new Point(170, 106);
      this.taxValue.Size = new Size(200, 40);
      this.taxValue.Color = ARGBColors.Black;
      this.taxValue.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.taxValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.windowImage.addControl((CustomSelfDrawPanel.CSDControl) this.taxValue);
      this.activityLabel.Text = SK.Text("WALL_recent_activity", "Recent Activity");
      this.activityLabel.Position = new Point(8, -16);
      this.activityLabel.Size = new Size(388, 40);
      this.activityLabel.Color = ARGBColors.Black;
      this.activityLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.activityLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.activityLabel);
      this.wallInfoImage.Size = new Size(396, height - 80);
      this.wallInfoImage.Position = new Point(8, 29);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallInfoImage);
      this.wallInfoImage.Create((Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_right);
      this.wallScrollArea.Position = new Point(15, 15);
      this.wallScrollArea.Size = new Size(337, height - 101);
      this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(337, height - 101));
      this.wallInfoImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollArea);
      int num = this.wallScrollBar.Value;
      this.wallScrollBar.Visible = false;
      this.wallScrollBar.Position = new Point(358, 15);
      this.wallScrollBar.Size = new Size(24, height - 101);
      this.wallInfoImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollBar);
      this.wallScrollBar.Value = 0;
      this.wallScrollBar.Max = 100;
      this.wallScrollBar.NumVisibleLines = 25;
      this.wallScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      ProvinceFrontPagePanel2.StoredProvinceInfo province = (ProvinceFrontPagePanel2.StoredProvinceInfo) this.provinceList[provinceFromVillageId];
      bool flag = false;
      if (province == null || (DateTime.Now - province.m_lastUpdateTime).TotalMinutes > 2.0 || province.lastReturnData == null)
        flag = true;
      this.m_currentVillage = selectedMenuVillage;
      if (this.currentProvince != provinceFromVillageId)
      {
        this.currentLeaderID = -1;
        this.currentLeaderName = "";
        this.m_userIDOnCurrent = -1;
      }
      this.currentProvince = provinceFromVillageId;
      if (flag)
      {
        RemoteServices.Instance.set_GetProvinceFrontPageInfo_UserCallBack(new RemoteServices.GetProvinceFrontPageInfo_UserCallBack(this.getProvinceFrontPageInfoCallback));
        RemoteServices.Instance.GetProvinceFrontPageInfo(this.m_currentVillage);
      }
      this.updateLeaderInfo();
      if (!flag)
        this.getProvinceFrontPageInfoCallback(province.lastReturnData);
      this.btnChat.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
      this.btnChat.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
      this.btnChat.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
      this.btnChat.Position = new Point(this.Width - 230, this.Height - 90);
      this.btnChat.Text.Text = SK.Text("GENERIC_Chat", "Chat");
      this.btnChat.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.btnChat.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.btnChat.TextYOffset = -3;
      this.btnChat.Text.Color = ARGBColors.Black;
      this.btnChat.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.chatClick), "ProvinceFrontPagePanel2_chat");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnChat);
    }

    public void update()
    {
    }

    public void logout() => this.provinceList.Clear();

    private void chatClick()
    {
      if (this.currentProvince < 0)
        return;
      InterfaceMgr.Instance.initChatPanel(1, this.currentProvince);
    }

    public void updateLeaderInfo()
    {
      this.sheriffName.Text = this.currentLeaderName;
      this.m_userIDOnCurrent = this.currentLeaderID;
      this.update();
    }

    public void getProvinceFrontPageInfoCallback(GetProvinceFrontPageInfo_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      ProvinceFrontPagePanel2.StoredProvinceInfo storedProvinceInfo = (ProvinceFrontPagePanel2.StoredProvinceInfo) this.provinceList[returnData.provinceID];
      if (storedProvinceInfo == null)
      {
        storedProvinceInfo = new ProvinceFrontPagePanel2.StoredProvinceInfo();
        this.provinceList[returnData.provinceID] = (object) storedProvinceInfo;
      }
      storedProvinceInfo.m_lastUpdateTime = DateTime.Now;
      storedProvinceInfo.lastReturnData = returnData;
      if (this.currentProvince != returnData.provinceID)
        return;
      this.m_userIDOnCurrent = -1;
      this.currentLeaderID = returnData.leaderID;
      this.currentLeaderName = returnData.leaderName;
      this.updateLeaderInfo();
      NumberFormatInfo nfi = GameEngine.NFI;
      switch (returnData.taxRate)
      {
        case 0:
          this.taxValue.Text = "0";
          break;
        case 1:
          this.taxValue.Text = "x1";
          break;
        case 2:
          this.taxValue.Text = "x2";
          break;
        case 3:
          this.taxValue.Text = "x3";
          break;
        case 4:
          this.taxValue.Text = "x4";
          break;
        case 5:
          this.taxValue.Text = "x5";
          break;
        case 6:
          this.taxValue.Text = "x6";
          break;
        case 7:
          this.taxValue.Text = "x7";
          break;
        case 8:
          this.taxValue.Text = "x8";
          break;
        case 9:
          this.taxValue.Text = "x9";
          break;
      }
      this.goldValue.Text = returnData.gold.ToString("N", (IFormatProvider) nfi);
      this.createParishWall(returnData.provinceWallInfo);
    }

    private void createParishWall(WallInfo[] wallInfos)
    {
      this.origWallInfo = wallInfos;
      List<WallInfo> wallInfoList = new List<WallInfo>();
      this.wallList.Clear();
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

    public class StoredProvinceInfo
    {
      public GetProvinceFrontPageInfo_ReturnType lastReturnData;
      public DateTime m_lastUpdateTime = DateTime.MinValue;
    }
  }
}
