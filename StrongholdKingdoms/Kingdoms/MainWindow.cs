// Decompiled with JetBrains decompiler
// Type: Kingdoms.MainWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using Kingdoms.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MainWindow : Form
  {
    private IContainer components;
    private DXPanel dxBasePanel;
    private MainRightHandPanel mainRightHandPanel1;
    private TopLeftMenu2 topLeftMenu1;
    private TopRightMenu topRightMenu1;
    private ToolTip m_wndToolTip;
    private CustomSelfDrawPanel.CSDImage debugBgd = new CustomSelfDrawPanel.CSDImage();
    private bool steamOverlayed;
    private Point origDXLoc = new Point();
    private Size origDXSize = new Size();
    private bool m_allowResizing;
    public static bool captureCloseMenuEvent;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MainWindow));
      this.m_wndToolTip = new ToolTip(this.components);
      this.dxBasePanel = new DXPanel();
      this.mainRightHandPanel1 = new MainRightHandPanel();
      this.topLeftMenu1 = new TopLeftMenu2();
      this.topRightMenu1 = new TopRightMenu();
      this.SuspendLayout();
      this.dxBasePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dxBasePanel.BackColor = ARGBColors.Black;
      this.dxBasePanel.Location = new Point(0, 120);
      this.dxBasePanel.Name = "dxBasePanel";
      this.dxBasePanel.Size = new Size(1313, 966);
      this.dxBasePanel.TabIndex = 0;
      this.mainRightHandPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.mainRightHandPanel1.BackColor = ARGBColors.Red;
      this.mainRightHandPanel1.Font = new Font("Microsoft Sans Serif", 8.25f);
      this.mainRightHandPanel1.Location = new Point(1313, 120);
      this.mainRightHandPanel1.Size = new Size(200, 240);
      this.mainRightHandPanel1.Name = "mainRightHandPanel1";
      this.mainRightHandPanel1.TabIndex = 2;
      this.topLeftMenu1.ClickThru = false;
      this.topLeftMenu1.Font = new Font("Microsoft Sans Serif", 8.25f);
      this.topLeftMenu1.Location = new Point(0, 0);
      this.topLeftMenu1.Name = "topLeftMenu1";
      this.topLeftMenu1.PanelActive = true;
      this.topLeftMenu1.Size = new Size(527, 120);
      this.topLeftMenu1.StoredGraphics = (Graphics) null;
      this.topLeftMenu1.TabIndex = 4;
      this.topRightMenu1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.topRightMenu1.BackColor = ARGBColors.Red;
      this.topRightMenu1.ClickThru = false;
      this.topRightMenu1.Location = new Point(527, 0);
      this.topRightMenu1.MinimumSize = new Size(463, 0);
      this.topRightMenu1.Name = "topRightMenu1";
      this.topRightMenu1.PanelActive = true;
      this.topRightMenu1.Size = new Size(985, 120);
      this.topRightMenu1.StoredGraphics = (Graphics) null;
      this.topRightMenu1.TabIndex = 5;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Black;
      this.ClientSize = new Size(1512, 1086);
      this.Controls.Add((Control) this.dxBasePanel);
      this.Controls.Add((Control) this.mainRightHandPanel1);
      this.Controls.Add((Control) this.topLeftMenu1);
      this.Controls.Add((Control) this.topRightMenu1);
      this.Icon = Resources.shk_icon;
      this.MaximumSize = new Size(1520, 1120);
      this.MinimumSize = new Size(1000, 720);
      this.Name = nameof (MainWindow);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Stronghold Kingdoms";
      this.Deactivate += new EventHandler(this.MainWindowLarge_Deactivate);
      this.Load += new EventHandler(this.MainWindow_Load);
      this.ResizeBegin += new EventHandler(this.MainWindowLarge_ResizeBegin);
      this.SizeChanged += new EventHandler(this.MainWindowLarge_SizeChanged);
      this.Activated += new EventHandler(this.MainWindowLarge_Activated);
      this.FormClosed += new FormClosedEventHandler(this.MainWindowLarge_FormClosed);
      this.FormClosing += new FormClosingEventHandler(this.MainWindow_FormClosing);
      this.LocationChanged += new EventHandler(this.MainWindow_LocationChanged);
      this.ResizeEnd += new EventHandler(this.MainWindowLarge_ResizeEnd);
      this.ResumeLayout(false);
    }

    public MainWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.mainRightHandPanel1.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.topLeftMenu1.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void setTooltipText(Control control, string text)
    {
      this.m_wndToolTip.SetToolTip(control, text);
    }

    public DXPanel getDXBasePanel() => this.dxBasePanel;

    public void makeFullDX()
    {
      this.origDXLoc = this.dxBasePanel.Location;
      this.origDXSize = this.dxBasePanel.Size;
      this.dxBasePanel.Location = new Point(0, 0);
      this.dxBasePanel.Size = this.ClientSize;
      this.steamOverlayed = true;
      if (!Program.arcInstall || GameEngine.Instance.World == null)
        return;
      GameEngine.Instance.World.setScreenSize(this.dxBasePanel.Width, this.dxBasePanel.Height);
    }

    public void restoreDXSize()
    {
      this.dxBasePanel.Location = this.origDXLoc;
      this.dxBasePanel.Size = this.origDXSize;
      this.steamOverlayed = false;
      InterfaceMgr.Instance.reShowDXWindow();
      if (!Program.arcInstall || GameEngine.Instance.World == null)
        return;
      GameEngine.Instance.World.setScreenSize(this.dxBasePanel.Width, this.dxBasePanel.Height);
    }

    public MainRightHandPanel getMainRightHandPanel() => this.mainRightHandPanel1;

    public MainTabBar2 getMainTabBar() => this.topRightMenu1.getMainTabBar();

    public VillageTabBar2 getVillageTabBar() => this.topRightMenu1.getVillageTabBar();

    public FactionTabBar2 getFactionTabBar() => this.topRightMenu1.getFactionTabBar();

    public TopLeftMenu2 getTopLeftMenu() => this.topLeftMenu1;

    public TopRightMenu getTopRightMenu() => this.topRightMenu1;

    public MainMenuBar2 getMainMenuBar() => this.topRightMenu1.mainMenuBar;

    public void setMainAreaVisible(bool state)
    {
      this.mainRightHandPanel1.Visible = state;
      this.dxBasePanel.Visible = state;
    }

    public bool isFullMainArea() => !this.mainRightHandPanel1.Visible;

    public void setMainWindowAreaVisible(bool state) => this.dxBasePanel.Visible = state;

    private void MainWindowLarge_Activated(object sender, EventArgs e)
    {
      GameEngine.Instance.WindowActive = true;
    }

    private void MainWindowLarge_Deactivate(object sender, EventArgs e)
    {
      GameEngine.Instance.WindowActive = false;
    }

    private void MainWindowLarge_FormClosed(object sender, FormClosedEventArgs e)
    {
      switch (e.CloseReason)
      {
        case CloseReason.WindowsShutDown:
        case CloseReason.UserClosing:
        case CloseReason.ApplicationExitCall:
          GameEngine.Instance.windowClosing();
          break;
      }
    }

    public void finaliseResize() => this.MainWindowLarge_ResizeEnd((object) null, (EventArgs) null);

    private void MainWindowLarge_ResizeBegin(object sender, EventArgs e)
    {
      this.dxBasePanel.resizing = true;
      GameEngine.Instance.startResizeWindow();
    }

    private void MainWindowLarge_ResizeEnd(object sender, EventArgs e)
    {
      this.dxBasePanel.resizing = false;
      if (GameEngine.Instance != null)
      {
        GameEngine.Instance.resizeWindow();
        if (GameEngine.Instance.World != null)
          GameEngine.Instance.World.setScreenSize(this.dxBasePanel.Width, this.dxBasePanel.Height);
      }
      Program.mySettings.ScreenWidth = this.ClientSize.Width;
      Program.mySettings.ScreenHeight = this.ClientSize.Height;
      this.topLeftMenu1.resize();
      this.topRightMenu1.resize();
    }

    public void allowResizing(bool state) => this.m_allowResizing = state;

    public void MainWindowLarge_SizeChanged(object sender, EventArgs e)
    {
      if (!this.m_allowResizing)
        return;
      GameEngine.Instance.finaliseResize = true;
      if (this.steamOverlayed)
      {
        this.origDXSize = new Size(this.ClientSize.Width - this.mainRightHandPanel1.Width, this.ClientSize.Height - 120);
        this.dxBasePanel.Size = this.ClientSize;
      }
      else
      {
        this.dxBasePanel.Width = this.ClientSize.Width - this.mainRightHandPanel1.Width;
        this.dxBasePanel.Height = this.ClientSize.Height - 120;
      }
      this.mainRightHandPanel1.Height = this.ClientSize.Height - 120;
      this.mainRightHandPanel1.Location = new Point(this.ClientSize.Width - this.mainRightHandPanel1.Width, this.mainRightHandPanel1.Location.Y);
      this.topRightMenu1.Size = new Size(this.ClientSize.Width - this.topLeftMenu1.Width, this.topRightMenu1.Height);
      if (GameEngine.Instance != null)
      {
        GameEngine.Instance.resizeWindow();
        if (GameEngine.Instance.World != null)
          GameEngine.Instance.World.setScreenSize(this.dxBasePanel.Width, this.dxBasePanel.Height);
      }
      if (this.ClientSize.Width >= 1100)
        this.topLeftMenu1.Size = new Size(this.ClientSize.Width / 2 + 86, this.topLeftMenu1.Size.Height);
      else
        this.topLeftMenu1.Size = new Size(this.ClientSize.Width - 463, this.topLeftMenu1.Size.Height);
      this.topRightMenu1.Size = new Size(this.ClientSize.Width - this.topLeftMenu1.Size.Width, this.topRightMenu1.Size.Height);
      this.topRightMenu1.Location = new Point(this.topLeftMenu1.Size.Width, this.topRightMenu1.Location.Y);
      this.topLeftMenu1.resize();
      this.topRightMenu1.resize();
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {
      Screen primaryScreen = Screen.PrimaryScreen;
      Point location = primaryScreen.WorkingArea.Location;
      location.X += (primaryScreen.WorkingArea.Width - this.Size.Width) / 2;
      location.Y += (primaryScreen.WorkingArea.Height - this.Size.Height) / 2;
      this.Location = location;
    }

    private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing || RemoteServices.Instance.UserID < 0)
        return;
      if (!InterfaceMgr.Instance.isLogoutPopupOpen())
        InterfaceMgr.Instance.openLogoutWindow(true);
      e.Cancel = true;
    }

    private void MainWindow_LocationChanged(object sender, EventArgs e)
    {
      InterfaceMgr.Instance.movePlayCardsWindow();
      InterfaceMgr.Instance.moveLogoutWindow();
      InterfaceMgr.Instance.moveReportCaptureWindow();
      InterfaceMgr.Instance.moveScoutPopupWindow();
      InterfaceMgr.Instance.moveGreyOutWindow();
      InterfaceMgr.Instance.moveTutorialWindow();
      InterfaceMgr.Instance.moveTutorialArrowWindow();
      InterfaceMgr.Instance.moveFreeCardsPopup();
      InterfaceMgr.Instance.moveWheelPopup();
      InterfaceMgr.Instance.moveWheelSelectPopup();
      InterfaceMgr.Instance.moveAdvancedCastleOptionsPopup();
      InterfaceMgr.Instance.moveAchievementPopup();
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 528 && MainWindow.captureCloseMenuEvent)
        InterfaceMgr.Instance.closeMenuPopup();
      else if (m.Msg == 1591 && Program.arcInstall)
      {
        if ((int) m.LParam != 0)
        {
          if (!this.steamOverlayed)
          {
            InterfaceMgr.Instance.ParentMainWindow.makeFullDX();
            GameEngine.Instance.GFX.fullDeviceReset();
          }
          InterfaceMgr.Instance.closeAllPopups();
          Program.arc_overlay_open = true;
        }
        else
        {
          InterfaceMgr.Instance.ParentMainWindow.restoreDXSize();
          GameEngine.Instance.GFX.resizeWindow();
          Program.arc_overlay_open = false;
        }
        Program.arc_overlay_delay = 5;
      }
      base.WndProc(ref m);
    }
  }
}
