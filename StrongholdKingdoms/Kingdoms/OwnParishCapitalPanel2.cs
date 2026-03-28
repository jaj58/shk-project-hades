// Decompiled with JetBrains decompiler
// Type: Kingdoms.OwnParishCapitalPanel2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class OwnParishCapitalPanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castleButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton resourcesButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopsButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel lblProtected = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblProtectionType = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton leftButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton rightButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel lblPlagueValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton mapEdit = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage backImage;
    private int lastPlague = -100;
    private int selectedProtection;
    private int numInfos;
    private int m_selectedVillage = -1;
    private DockableControl dockableControl;
    private IContainer components;

    public OwnParishCapitalPanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
    }

    public void init()
    {
      this.clearControls();
      this.backImage = this.backGround.init(true, 10000);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround);
      this.tradeButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.TRADE);
      this.tradeButton.Position = new Point(80, 142);
      this.tradeButton.CustomTooltipID = 2441;
      this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tradeClick), "OwnParishCapitalPanel2_trade");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.tradeButton);
      if (GameEngine.Instance.World.MapEditing)
      {
        this.mapEdit.ImageNorm = (Image) GFXLibrary.faction_pen;
        this.mapEdit.ImageOver = (Image) GFXLibrary.faction_pen;
        this.mapEdit.ImageClick = (Image) GFXLibrary.faction_pen;
        this.mapEdit.MoveOnClick = true;
        this.mapEdit.OverBrighten = true;
        this.mapEdit.Position = new Point(150, 142);
        this.mapEdit.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mapEditClicked));
        this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.mapEdit);
      }
      this.villageButton.ImageNorm = (Image) GFXLibrary.int_world_icon_village;
      this.villageButton.OverBrighten = true;
      this.villageButton.MoveOnClick = true;
      this.villageButton.Position = new Point(29, 112);
      this.villageButton.CustomTooltipID = 2437;
      this.villageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClick), "OwnParishCapitalPanel2_view_village");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.villageButton);
      this.castleButton.ImageNorm = (Image) GFXLibrary.int_world_icon_castle;
      this.castleButton.OverBrighten = true;
      this.castleButton.MoveOnClick = true;
      this.castleButton.Position = new Point(64, 112);
      this.castleButton.CustomTooltipID = 2438;
      this.castleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castleClick), "OwnParishCapitalPanel2_view_castle");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleButton);
      this.resourcesButton.ImageNorm = (Image) GFXLibrary.int_world_icon_resource;
      this.resourcesButton.OverBrighten = true;
      this.resourcesButton.MoveOnClick = true;
      this.resourcesButton.Position = new Point(99, 112);
      this.resourcesButton.CustomTooltipID = 2439;
      this.resourcesButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourcesClick), "OwnParishCapitalPanel2_view_resources");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.resourcesButton);
      this.troopsButton.ImageNorm = (Image) GFXLibrary.int_world_icon_troops;
      this.troopsButton.OverBrighten = true;
      this.troopsButton.MoveOnClick = true;
      this.troopsButton.Position = new Point(134, 112);
      this.troopsButton.CustomTooltipID = 2442;
      this.troopsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopsClick), "OwnParishCapitalPanel2_make_troops");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.troopsButton);
      this.lblPlagueValue.Text = "";
      this.lblPlagueValue.Color = ARGBColors.Black;
      this.lblPlagueValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.lblPlagueValue.Position = new Point(82, 10);
      this.lblPlagueValue.Size = new Size(48, 22);
      this.lblPlagueValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblPlagueValue);
      this.lblProtectionType.Text = "";
      this.lblProtectionType.Color = ARGBColors.Black;
      this.lblProtectionType.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.lblProtectionType.Position = new Point(0, 38);
      this.lblProtectionType.Size = new Size(this.backImage.Width, 23);
      this.lblProtectionType.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblProtectionType);
      this.lblProtected.Text = "";
      this.lblProtected.Color = ARGBColors.Black;
      this.lblProtected.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.lblProtected.Position = new Point(6, 48);
      this.lblProtected.Size = new Size(this.backImage.Width - 12, 74);
      this.lblProtected.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblProtected);
      this.leftButton.ImageNorm = (Image) GFXLibrary.r_arrow_small_left_norm;
      this.leftButton.ImageOver = (Image) GFXLibrary.r_arrow_small_left_over;
      this.leftButton.Position = new Point(5, 50);
      this.leftButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoLeft), "OwnParishCapitalPanel2_protection_left");
      this.leftButton.Visible = false;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.leftButton);
      this.rightButton.ImageNorm = (Image) GFXLibrary.r_arrow_small_right_norm;
      this.rightButton.ImageOver = (Image) GFXLibrary.r_arrow_small_right_over;
      this.rightButton.Position = new Point(170, 50);
      this.rightButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoRight), "OwnParishCapitalPanel2_protection_right");
      this.rightButton.Visible = false;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.rightButton);
      this.lastPlague = -100;
      this.numInfos = 0;
      this.selectedProtection = 0;
    }

    private void updateSize()
    {
      bool visible = this.lblProtected.Visible;
      int num1 = 0;
      int num2 = 0;
      if (!visible)
      {
        this.backImage.Image = (Image) GFXLibrary.mrhp_world_panel_132;
        num1 = -63;
        num2 = -4;
      }
      else
        this.backImage.Image = (Image) GFXLibrary.mrhp_world_panel_192;
      this.tradeButton.Position = new Point(80, 142 + num1);
      this.mapEdit.Position = new Point(150, 142 + num1);
      this.villageButton.Position = new Point(29, 112 + num1 + num2);
      this.castleButton.Position = new Point(64, 112 + num1 + num2);
      this.resourcesButton.Position = new Point(99, 112 + num1 + num2);
      this.troopsButton.Position = new Point(134, 112 + num1 + num2);
      this.backGround.invalidate();
    }

    public void update()
    {
      this.backGround.update();
      int[] numArray = new int[3];
      TimeSpan[] timeSpanArray = new TimeSpan[3];
      int numInfos = this.numInfos;
      this.numInfos = 0;
      int parishPlagueLevel = GameEngine.Instance.World.getParishPlagueLevel(this.m_selectedVillage);
      if (parishPlagueLevel != this.lastPlague)
      {
        if (parishPlagueLevel <= 0)
        {
          this.backGround.updatePanelType(1500);
          this.lblPlagueValue.TextDiffOnly = "";
        }
        else if (this.lastPlague <= 0)
        {
          this.backGround.updatePanelType(1504);
          this.lblPlagueValue.TextDiffOnly = parishPlagueLevel.ToString();
        }
        this.backGround.setTooltipData(parishPlagueLevel);
        this.lastPlague = parishPlagueLevel;
      }
      bool visible = this.lblProtected.Visible;
      int num = 0;
      TimeSpan timeSpan1 = new TimeSpan();
      if (GameEngine.Instance.World.isVillageExcommunicated(this.m_selectedVillage))
      {
        timeSpan1 = GameEngine.Instance.World.getExcommunicationTime(this.m_selectedVillage) - VillageMap.getCurrentServerTime();
        num = 3;
        timeSpanArray[this.numInfos] = timeSpan1;
        numArray[this.numInfos] = num;
        ++this.numInfos;
      }
      if (GameEngine.Instance.World.isVillageInterdictProtected(this.m_selectedVillage))
      {
        timeSpan1 = GameEngine.Instance.World.getInterdictTime(this.m_selectedVillage) - VillageMap.getCurrentServerTime();
        num = 1;
        timeSpanArray[this.numInfos] = timeSpan1;
        numArray[this.numInfos] = num;
        ++this.numInfos;
      }
      if (GameEngine.Instance.World.isVillagePeaceTimeProtected(this.m_selectedVillage))
      {
        TimeSpan timeSpan2 = GameEngine.Instance.World.getPeaceTime(this.m_selectedVillage) - VillageMap.getCurrentServerTime();
        if (timeSpan2 > timeSpan1)
        {
          timeSpan1 = timeSpan2;
          num = 2;
        }
        timeSpanArray[this.numInfos] = timeSpan2;
        numArray[this.numInfos] = 2;
        ++this.numInfos;
      }
      if (this.numInfos > 0)
      {
        if (this.selectedProtection < this.numInfos)
        {
          num = numArray[this.numInfos - 1 - this.selectedProtection];
          timeSpan1 = timeSpanArray[this.numInfos - 1 - this.selectedProtection];
        }
        else
          this.selectedProtection = 0;
      }
      switch (num)
      {
        case 1:
          string buildTimeStringFull1 = VillageMap.createBuildTimeStringFull((int) timeSpan1.TotalSeconds);
          this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked_For_X_Time", "Cannot be attacked for") + " : " + buildTimeStringFull1;
          this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Interdict", "Interdict");
          this.lblProtectionType.Visible = true;
          this.lblProtected.Visible = true;
          break;
        case 2:
          string buildTimeStringFull2 = VillageMap.createBuildTimeStringFull((int) timeSpan1.TotalSeconds);
          this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked_For_X_Time", "Cannot be attacked for") + " : " + buildTimeStringFull2;
          this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Peace", "Peace");
          this.lblProtectionType.Visible = true;
          this.lblProtected.Visible = true;
          break;
        case 3:
          string buildTimeStringFull3 = VillageMap.createBuildTimeStringFull((int) timeSpan1.TotalSeconds);
          this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Excom_For_X_Time", "No Monks for") + " : " + buildTimeStringFull3;
          this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Excom", "Excommunicated");
          this.lblProtectionType.Visible = true;
          this.lblProtected.Visible = true;
          break;
        default:
          this.lblProtectionType.Visible = false;
          this.lblProtected.Visible = false;
          break;
      }
      if (visible != this.lblProtected.Visible)
      {
        this.updateSize();
        if (!visible)
          this.selectedProtection = 0;
      }
      if (numInfos == this.numInfos)
        return;
      if (this.numInfos >= 2)
      {
        this.leftButton.Visible = true;
        this.rightButton.Visible = true;
      }
      else
      {
        this.leftButton.Visible = false;
        this.rightButton.Visible = false;
      }
    }

    public void updateOwnVillageText(int selectedVillage)
    {
      this.lastPlague = -100;
      this.m_selectedVillage = selectedVillage;
      this.backGround.updateHeading(GameEngine.Instance.World.getVillageName(selectedVillage));
      this.backGround.updatePanelTypeFromVillageID(selectedVillage);
      this.backGround.setActionFromVillage(selectedVillage, -1);
      this.lblPlagueValue.TextDiffOnly = "";
      this.updateSize();
      this.update();
    }

    private void villageClick()
    {
      InterfaceMgr.Instance.getMainTabBar().changeTab(2);
      InterfaceMgr.Instance.getVillageTabBar().changeTab(0);
    }

    private void castleClick()
    {
      InterfaceMgr.Instance.getMainTabBar().changeTab(2);
      InterfaceMgr.Instance.getVillageTabBar().changeTab(1);
    }

    private void resourcesClick()
    {
      InterfaceMgr.Instance.getMainTabBar().changeTab(2);
      InterfaceMgr.Instance.setVillageTabSubMode(1005);
    }

    private void troopsClick()
    {
      InterfaceMgr.Instance.getMainTabBar().changeTab(2);
      InterfaceMgr.Instance.setVillageTabSubMode(1004);
    }

    private void tradeClick()
    {
      InterfaceMgr.Instance.getMainTabBar().changeTab(2);
      InterfaceMgr.Instance.getVillageTabBar().changeTab(3);
    }

    private void infoLeft()
    {
      --this.selectedProtection;
      if (this.selectedProtection >= 0)
        return;
      this.selectedProtection = this.numInfos - 1;
    }

    private void infoRight()
    {
      ++this.selectedProtection;
      if (this.selectedProtection < this.numInfos)
        return;
      this.selectedProtection = 0;
    }

    private void mapEditClicked()
    {
      RenameVillagePopup renameVillagePopup = new RenameVillagePopup();
      renameVillagePopup.setParishVillageID(this.m_selectedVillage, GameEngine.Instance.World.getVillageName(this.m_selectedVillage));
      renameVillagePopup.Show((IWin32Window) InterfaceMgr.Instance.ParentForm);
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
      this.BackColor = ARGBColors.Transparent;
      this.Name = nameof (OwnParishCapitalPanel2);
      this.Size = new Size(199, 213);
      this.ResumeLayout(false);
    }
  }
}
