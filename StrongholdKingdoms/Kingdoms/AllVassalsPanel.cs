// Decompiled with JetBrains decompiler
// Type: Kingdoms.AllVassalsPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class AllVassalsPanel : CustomSelfDrawPanel, IDockableControl
  {
    public static AllVassalsPanel instance;
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage backgroundLeftEdge = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel yourVassalsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel outGoingFromLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDVertScrollBar vassalScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea vassalScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDImage smallPeasantImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage smallPeasantImage2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton btnClose = new CustomSelfDrawPanel.CSDButton();
    private int blockYSize;
    private VassalInfo liegeLordInfo = new VassalInfo();
    private VassalInfo[] cachedVassalInfo;
    private List<AllVassalsPanel.ArmyLine> lineList = new List<AllVassalsPanel.ArmyLine>();
    private int villageIDRef;
    private int yourVillageIDRef;
    private MyMessageBoxPopUp PopUpRef;
    private DockableControl dockableControl;
    private IContainer components;
    private Panel focusPanel;

    public AllVassalsPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.focusPanel.Focus();
    }

    public void reinit() => this.init(false);

    public void init(bool resized)
    {
      int height = this.Height;
      AllVassalsPanel.instance = this;
      this.clearControls();
      this.backgroundImage.Image = (Image) GFXLibrary.body_background_002;
      this.backgroundImage.Size = new Size(this.Width, height - 40);
      this.backgroundImage.Tile = true;
      this.backgroundImage.Position = new Point(0, 40);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
      this.backgroundLeftEdge.Image = (Image) GFXLibrary.body_background_canvas_left_edge;
      this.backgroundLeftEdge.Position = new Point(0, 0);
      this.backgroundLeftEdge.Size = new Size(this.backgroundLeftEdge.Image.Width, height - 40);
      this.backgroundLeftEdge.Tile = true;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundLeftEdge);
      this.headerImage.Size = new Size(this.Width, 40);
      this.headerImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage);
      this.headerImage.CreateX((Image) GFXLibrary.mail_top_drag_bar_left, (Image) GFXLibrary.mail_top_drag_bar_middle, (Image) GFXLibrary.mail_top_drag_bar_right, -2, 2);
      InterfaceMgr.Instance.getSelectedMenuVillage();
      this.parishNameLabel.Text = SK.Text("Vassals_Overview", "Vassals Overview");
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
      this.headerLabelsImage.Create((Image) GFXLibrary.brown_mail2_field_bar_mail_left, (Image) GFXLibrary.brown_mail2_field_bar_mail_middle, (Image) GFXLibrary.brown_mail2_field_bar_mail_right);
      this.divider2Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
      this.divider2Image.Position = new Point(580, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider2Image);
      this.yourVassalsLabel.Text = SK.Text("VassalControlPanel_Your_Vassals", "Your Vassals");
      this.yourVassalsLabel.Color = ARGBColors.Black;
      this.yourVassalsLabel.Position = new Point(12, -3);
      this.yourVassalsLabel.Size = new Size(223, this.headerLabelsImage.Height);
      this.yourVassalsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.yourVassalsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.yourVassalsLabel);
      this.vassalScrollArea.Position = new Point(25, 40);
      this.vassalScrollArea.Size = new Size(915, this.blockYSize - 40 - 10);
      this.vassalScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(915, this.blockYSize - 40 - 10));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.vassalScrollArea);
      int num1 = this.vassalScrollBar.Value;
      this.vassalScrollBar.Position = new Point(943, 40);
      this.vassalScrollBar.Size = new Size(24, this.blockYSize - 40 - 10);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.vassalScrollBar);
      this.vassalScrollBar.Value = 0;
      this.vassalScrollBar.Max = 100;
      this.vassalScrollBar.NumVisibleLines = 25;
      this.vassalScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary.brown_24wide_thumb_top, (Image) GFXLibrary.brown_24wide_thumb_middle, (Image) GFXLibrary.brown_24wide_thumb_bottom);
      this.vassalScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      this.smallPeasantImage.Image = (Image) GFXLibrary.armies_screen_troops;
      this.smallPeasantImage.Position = new Point(603, -10);
      this.smallPeasantImage.ClipRect = new Rectangle(0, 0, this.smallPeasantImage.Image.Width * 5 / 6, this.smallPeasantImage.Image.Height);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.smallPeasantImage);
      int num2 = resized ? 1 : 0;
      if (resized)
        this.vassalScrollBar.Value = num1;
      this.btnClose.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
      this.btnClose.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
      this.btnClose.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
      this.btnClose.Position = new Point(this.Width - 230, height - 40 - 40 - 4);
      this.btnClose.Text.Text = SK.Text("GENERIC_Close", "Close");
      this.btnClose.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.btnClose.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.btnClose.TextYOffset = -3;
      this.btnClose.Text.Color = ARGBColors.Black;
      this.btnClose.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "AllVassalsPanel_close");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnClose);
      this.cachedVassalInfo = (VassalInfo[]) null;
      RemoteServices.Instance.set_VassalInfo_UserCallBack(new RemoteServices.VassalInfo_UserCallBack(this.vassalInfoCallBack));
      if (GameEngine.Instance.Village != null)
        RemoteServices.Instance.VassalInfo(-1);
      this.reAddVassals();
    }

    public void vassalInfoCallBack(VassalInfo_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.cachedVassalInfo = returnData.vassals;
      this.reAddVassals();
      GameEngine.Instance.World.updateUserVassals();
    }

    public void update()
    {
    }

    public void logout()
    {
    }

    private void wallScrollBarMoved()
    {
      int y = this.vassalScrollBar.Value;
      this.vassalScrollArea.Position = new Point(this.vassalScrollArea.X, 40 - y);
      this.vassalScrollArea.ClipRect = new Rectangle(this.vassalScrollArea.ClipRect.X, y, this.vassalScrollArea.ClipRect.Width, this.vassalScrollArea.ClipRect.Height);
      this.vassalScrollArea.invalidate();
      this.vassalScrollBar.invalidate();
    }

    public void closing() => InterfaceMgr.Instance.closeDonatePopup();

    private void reAddVassals()
    {
      this.lineList.Clear();
      this.vassalScrollArea.clearControls();
      int num = 0;
      int position = 0;
      if (this.cachedVassalInfo != null)
      {
        foreach (VassalInfo vassalInfo in this.cachedVassalInfo)
        {
          if (num != 0)
            num += 5;
          AllVassalsPanel.ArmyLine control = new AllVassalsPanel.ArmyLine();
          control.Position = new Point(0, num);
          control.init(position, this, vassalInfo.yourVillageID, vassalInfo.villageID, vassalInfo.honourPerSecond, vassalInfo.stationed_Peasants, vassalInfo.stationed_Archers, vassalInfo.stationed_Pikemen, vassalInfo.stationed_Swordsmen, vassalInfo.stationed_Catapults, vassalInfo.vassalPlayerName);
          this.vassalScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
          num += control.Height;
          this.lineList.Add(control);
          ++position;
        }
      }
      this.vassalScrollArea.Size = new Size(this.vassalScrollArea.Width, num);
      if (num < this.vassalScrollBar.Height)
      {
        this.vassalScrollBar.Visible = false;
      }
      else
      {
        this.vassalScrollBar.Visible = true;
        this.vassalScrollBar.NumVisibleLines = this.vassalScrollBar.Height;
        this.vassalScrollBar.Max = num - this.vassalScrollBar.Height;
      }
      this.vassalScrollArea.invalidate();
      this.vassalScrollBar.invalidate();
      this.backgroundImage.invalidate();
    }

    public void breakVassalage(int yourVillageID, int villageID)
    {
      this.villageIDRef = villageID;
      this.yourVillageIDRef = yourVillageID;
      if (MyMessageBox.Show(SK.Text("VassalControlPanel_BreakVassalage_Warning", "Breaking from your vassal will mean any troops stationed there will be lost."), SK.Text("VassalControlPanel_BreakVassalage", "Break Vassalage?"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.BreakVassalage();
    }

    private void BreakVassalage()
    {
      RemoteServices.Instance.set_BreakVassalage_UserCallBack(new RemoteServices.BreakVassalage_UserCallBack(this.breakVassalageCallBack));
      RemoteServices.Instance.BreakVassalage(this.yourVillageIDRef, this.villageIDRef);
      GameEngine.Instance.World.breakVassal(this.yourVillageIDRef, this.villageIDRef);
    }

    public void breakVassalageCallBack(BreakVassalage_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.cachedVassalInfo = returnData.vassals;
        this.reAddVassals();
        GameEngine.Instance.World.updateUserVassals();
      }
      CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
    }

    private void closeClick()
    {
      InterfaceMgr.Instance.initVillageTab();
      InterfaceMgr.Instance.setVillageTabSubMode(8);
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
      this.Name = nameof (AllVassalsPanel);
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 566);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public class ArmyLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblYourVillage = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblArchers = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblPikemen = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblSwordsmen = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblCatapults = new CustomSelfDrawPanel.CSDLabel();
      private int m_position = -1000;
      private AllVassalsPanel m_parent;
      private int m_villageID = -1;
      private int m_yourVillageID = -1;

      public void init(
        int position,
        AllVassalsPanel parent,
        int yourVillageID,
        int villageID,
        double honourPerSecond,
        int numPeasants,
        int numArchers,
        int numPikemen,
        int numSwordsmen,
        int numCatapults,
        string username)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.m_villageID = villageID;
        this.m_yourVillageID = yourVillageID;
        this.ClipVisible = true;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.brown_lineitem_strip_02_dark : (Image) GFXLibrary.brown_lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.lblYourVillage.Text = GameEngine.Instance.World.getVillageNameOrType(this.m_yourVillageID);
        this.lblYourVillage.Color = ARGBColors.Black;
        this.lblYourVillage.RolloverColor = ARGBColors.White;
        this.lblYourVillage.Position = new Point(9, 0);
        this.lblYourVillage.Size = new Size(290, this.backgroundImage.Height);
        this.lblYourVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblYourVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblYourVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblYourVillage_Click), "AllVassalsPanel_village");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblYourVillage);
        this.lblVillage.Text = GameEngine.Instance.World.getVillageNameOrType(villageID);
        if (username.Length > 0)
        {
          CustomSelfDrawPanel.CSDLabel lblVillage = this.lblVillage;
          lblVillage.Text = lblVillage.Text + " (" + username + ")";
        }
        this.lblVillage.Color = ARGBColors.Black;
        this.lblVillage.RolloverColor = ARGBColors.White;
        this.lblVillage.Position = new Point(279, 0);
        this.lblVillage.Size = new Size(290, this.backgroundImage.Height);
        this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "AllVassalsPanel_other_village");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblVillage);
        this.lblPeasants.Text = numPeasants.ToString();
        this.lblPeasants.Color = ARGBColors.Black;
        this.lblPeasants.RolloverColor = ARGBColors.White;
        this.lblPeasants.Position = new Point(585, 0);
        this.lblPeasants.Size = new Size(55, this.backgroundImage.Height);
        this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.lblPeasants.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick), "AllVassalsPanel_troops");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblPeasants);
        this.lblArchers.Text = numArchers.ToString();
        this.lblArchers.Color = ARGBColors.Black;
        this.lblArchers.RolloverColor = ARGBColors.White;
        this.lblArchers.Position = new Point(645, 0);
        this.lblArchers.Size = new Size(55, this.backgroundImage.Height);
        this.lblArchers.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblArchers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.lblArchers.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick), "AllVassalsPanel_troops");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblArchers);
        this.lblPikemen.Text = numPikemen.ToString();
        this.lblPikemen.Color = ARGBColors.Black;
        this.lblPikemen.RolloverColor = ARGBColors.White;
        this.lblPikemen.Position = new Point(705, 0);
        this.lblPikemen.Size = new Size(55, this.backgroundImage.Height);
        this.lblPikemen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblPikemen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.lblPikemen.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick), "AllVassalsPanel_troops");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblPikemen);
        this.lblSwordsmen.Text = numSwordsmen.ToString();
        this.lblSwordsmen.Color = ARGBColors.Black;
        this.lblSwordsmen.RolloverColor = ARGBColors.White;
        this.lblSwordsmen.Position = new Point(765, 0);
        this.lblSwordsmen.Size = new Size(55, this.backgroundImage.Height);
        this.lblSwordsmen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblSwordsmen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.lblSwordsmen.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick), "AllVassalsPanel_troops");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblSwordsmen);
        this.lblCatapults.Text = numCatapults.ToString();
        this.lblCatapults.Color = ARGBColors.Black;
        this.lblCatapults.RolloverColor = ARGBColors.White;
        this.lblCatapults.Position = new Point(825, 0);
        this.lblCatapults.Size = new Size(55, this.backgroundImage.Height);
        this.lblCatapults.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.lblCatapults.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.lblCatapults.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick), "AllVassalsPanel_troops");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblCatapults);
        this.invalidate();
      }

      public bool update() => false;

      private void lblYourVillage_Click()
      {
        if (this.m_yourVillageID < 0)
          return;
        InterfaceMgr.Instance.selectUserVillage(this.m_yourVillageID, false);
        GameEngine.Instance.SkipVillageTab();
        InterfaceMgr.Instance.getMainTabBar().changeTab(9);
        InterfaceMgr.Instance.getMainTabBar().changeTab(1);
        InterfaceMgr.Instance.setVillageTabSubMode(8);
      }

      private void lblVillage_Click()
      {
        if (this.m_villageID < 0)
          return;
        Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.m_villageID);
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.closeParishPanel();
        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_villageID, false, true, true, false);
      }

      private void troopClick()
      {
        if (this.m_villageID < 0)
          return;
        InterfaceMgr.Instance.selectUserVillage(this.m_yourVillageID, false);
        GameEngine.Instance.SkipVillageTab();
        InterfaceMgr.Instance.getMainTabBar().changeTab(9);
        InterfaceMgr.Instance.getMainTabBar().changeTab(1);
        InterfaceMgr.Instance.setVassalArmiesVillage(this.m_villageID);
        InterfaceMgr.Instance.setVillageTabSubMode(15);
      }
    }
  }
}
