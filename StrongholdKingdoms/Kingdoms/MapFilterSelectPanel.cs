// Decompiled with JetBrains decompiler
// Type: Kingdoms.MapFilterSelectPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MapFilterSelectPanel : CustomSelfDrawPanel, IDockableControl
  {
    private CustomSelfDrawPanel.CSDButton filterButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
    private DockableControl dockableControl;
    private IContainer components;

    public MapFilterSelectPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
    }

    public void init(bool showAsOpen, bool doubleHeight)
    {
      this.clearControls();
      if (doubleHeight)
      {
        this.filterButton.MoveOnClick = true;
        this.filterButton.ImageClick = (Image) null;
        this.filterButton.Position = new Point(8, 32);
        if (showAsOpen)
        {
          if (GameEngine.Instance.World.worldMapFilter.FilterActive)
          {
            this.filterButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_normal;
            this.filterButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_over;
            this.filterButton.Text.Text = SK.Text("MapFilterSelectPanel_Filter_Active", "Filter Active");
            this.filterButton.CustomTooltipID = 93;
          }
          else
          {
            this.filterButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_off_normal;
            this.filterButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_off_over;
            this.filterButton.Text.Text = SK.Text("MapFilterSelectPanel_Map_Filtering", "Map Filtering");
            this.filterButton.CustomTooltipID = 91;
          }
          this.filterButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.filterClick), "MapFilterSelectPanel_filter");
        }
        else
        {
          this.filterButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_off_normal;
          this.filterButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_off_over;
          this.filterButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "MapFilterSelectPanel_filter");
          this.filterButton.Text.Text = SK.Text("GENERIC_Close", "Close");
          this.filterButton.CustomTooltipID = 92;
        }
        this.filterButton.Text.Color = ARGBColors.Black;
        this.filterButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.filterButton.Text.Size = new Size(130, 52);
        this.filterButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.filterButton.Text.Position = new Point(39, -10);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.filterButton);
        if (showAsOpen)
        {
          this.attackButton.Position = new Point(8, 2);
          this.attackButton.ImageNorm = (Image) GFXLibrary.mrhp_button_attack_normal;
          this.attackButton.ImageOver = (Image) GFXLibrary.mrhp_button_attack_over;
          this.attackButton.Text.Text = SK.Text("Attack_Targets", "Attack Targets");
          this.attackButton.Text.Color = ARGBColors.Black;
          this.attackButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
          this.attackButton.Text.Size = new Size(130, 52);
          this.attackButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
          this.attackButton.Text.Position = new Point(39, -10);
          this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.attackClick), "MapFilterSelectPanel_filter");
          this.attackButton.CustomTooltipID = 94;
          this.addControl((CustomSelfDrawPanel.CSDControl) this.attackButton);
        }
        this.Size = new Size(187, 57);
      }
      else
      {
        this.filterButton.Position = new Point(8, 2);
        if (showAsOpen)
        {
          if (GameEngine.Instance.World.worldMapFilter.FilterActive)
          {
            this.filterButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_off[6];
            this.filterButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_off[7];
            this.filterButton.ImageClick = (Image) GFXLibrary.mrhp_button_filter_off[8];
            this.filterButton.Text.Text = "";
            this.filterButton.CustomTooltipID = 93;
          }
          else
          {
            this.filterButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_off[3];
            this.filterButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_off[4];
            this.filterButton.ImageClick = (Image) GFXLibrary.mrhp_button_filter_off[5];
            this.filterButton.Text.Text = "";
            this.filterButton.CustomTooltipID = 91;
          }
          this.filterButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.filterClick), "MapFilterSelectPanel_filter");
        }
        else
        {
          this.filterButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_off[3];
          this.filterButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_off[4];
          this.filterButton.ImageClick = (Image) GFXLibrary.mrhp_button_filter_off[5];
          this.filterButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "MapFilterSelectPanel_filter");
          this.filterButton.Text.Text = "";
          this.filterButton.CustomTooltipID = 92;
        }
        this.addControl((CustomSelfDrawPanel.CSDControl) this.filterButton);
        this.attackButton.Position = new Point(102, 2);
        this.attackButton.Text.Text = "";
        this.attackButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_off[9];
        this.attackButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_off[10];
        this.attackButton.ImageClick = (Image) GFXLibrary.mrhp_button_filter_off[11];
        this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.attackClick), "MapFilterSelectPanel_filter");
        this.attackButton.CustomTooltipID = 94;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.attackButton);
        this.Size = new Size(187, 27);
      }
    }

    private void filterClick()
    {
      InterfaceMgr.Instance.showMapFilterPanel();
      InterfaceMgr.Instance.showMapFilterSelectPanel(true, false, true, true);
    }

    private void attackClick() => InterfaceMgr.Instance.openAttackTargetsPopup();

    private void closeClick()
    {
      InterfaceMgr.Instance.clearControls();
      InterfaceMgr.Instance.showMapFilterSelectPanel(true, true);
      InterfaceMgr.Instance.selectCurrentUserVillage();
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

    public void setPosition(int x, int y) => this.dockableControl.setPosition(x, y);

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
      this.Name = nameof (MapFilterSelectPanel);
      this.Size = new Size(187, 27);
      this.ResumeLayout(false);
    }
  }
}
