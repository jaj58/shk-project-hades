// Decompiled with JetBrains decompiler
// Type: Kingdoms.MapFilterPanel2
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
  public class MapFilterPanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton scoutButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton houseButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton factionButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton aiButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage selectedImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDCheckBox yourVillages = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDLabel diplomacyLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDCheckBox houseSymbols = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox factionSymbols = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox userSymbols = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDButton clearButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton searchButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage backImage;

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
      this.Name = nameof (MapFilterPanel2);
      this.Size = new Size(199, 273);
      this.ResumeLayout(false);
    }

    public MapFilterPanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
    }

    public void init()
    {
      this.clearControls();
      this.backImage = this.backGround.init(true, 1506);
      this.backGround.updateHeading(SK.Text("MapFilterSelectPanel_Map_Filtering", "Map Filtering"));
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround);
      this.backGround.stretchBackground();
      this.selectedImage.Image = (Image) GFXLibrary.mrhp_world_icons_filter_selected;
      this.selectedImage.Position = new Point(6, 45);
      this.selectedImage.Visible = false;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.selectedImage);
      this.tradeButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.TRADE);
      this.tradeButton.Position = new Point(115, 42);
      this.tradeButton.CustomTooltipID = 2454;
      this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tradeClick), "MapFilterPanel2_trade");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.tradeButton);
      this.attackButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.ATTACK);
      this.attackButton.Position = new Point(80, 76);
      this.attackButton.CustomTooltipID = 2455;
      this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.attackClick), "MapFilterPanel2_attack");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.attackButton);
      this.scoutButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.SCOUT);
      this.scoutButton.Position = new Point(45, 76);
      this.scoutButton.CustomTooltipID = 2456;
      this.scoutButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.scoutsClick), "MapFilterPanel2_scout");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.scoutButton);
      this.houseButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[6];
      this.houseButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[13];
      this.houseButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[20];
      this.houseButton.Position = new Point(45, 42);
      this.houseButton.CustomTooltipID = 2457;
      this.houseButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClick), "MapFilterPanel2_house");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseButton);
      this.factionButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[36];
      this.factionButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[37];
      this.factionButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[38];
      this.factionButton.Position = new Point(80, 42);
      if (RemoteServices.Instance.UserFactionID >= 0)
        this.factionButton.CustomTooltipID = 2458;
      else
        this.factionButton.CustomTooltipID = 2461;
      this.factionButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClick), "MapFilterPanel2_faction");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionButton);
      this.aiButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_ai[0];
      this.aiButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_ai[1];
      this.aiButton.ImageClick = (Image) GFXLibrary.mrhp_button_filter_ai[2];
      this.aiButton.Position = new Point(115, 76);
      this.aiButton.CustomTooltipID = 2462;
      this.aiButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.aiClick), "MapFilterPanel2_ai");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.aiButton);
      this.yourVillages.CheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[0];
      this.yourVillages.UncheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[1];
      this.yourVillages.Position = new Point(15, 117);
      this.yourVillages.Checked = GameEngine.Instance.World.worldMapFilter.FilterAlwaysShowYourVillages;
      this.yourVillages.CBLabel.Text = SK.Text("MapFilterPanel_Always_Show_Your_Villages", "Always Show Your Villages");
      this.yourVillages.CBLabel.Color = ARGBColors.Black;
      this.yourVillages.CBLabel.Position = new Point(20, -1);
      this.yourVillages.CBLabel.Size = new Size(180, 25);
      this.yourVillages.CBLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.yourVillages.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.yourToggled));
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.yourVillages);
      this.diplomacyLabel.Text = SK.Text("MapFilterPanel_Diplomacy", "Diplomacy Symbols");
      this.diplomacyLabel.Position = new Point(5, 137);
      this.diplomacyLabel.Color = ARGBColors.Black;
      this.diplomacyLabel.Size = new Size(180, 25);
      this.diplomacyLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
      this.diplomacyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyLabel);
      this.houseSymbols.CheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[0];
      this.houseSymbols.UncheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[1];
      this.houseSymbols.Position = new Point(15, 157);
      this.houseSymbols.Checked = GameEngine.Instance.World.worldMapFilter.FilterShowHouseSymbols;
      this.houseSymbols.CBLabel.Text = SK.Text("MapFilterPanel_Show_House_Symbols", "Show House Symbols");
      this.houseSymbols.CBLabel.Color = ARGBColors.Black;
      this.houseSymbols.CBLabel.Position = new Point(20, -1);
      this.houseSymbols.CBLabel.Size = new Size(180, 25);
      this.houseSymbols.CBLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.houseSymbols.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.houseToggled));
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseSymbols);
      this.factionSymbols.CheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[0];
      this.factionSymbols.UncheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[1];
      this.factionSymbols.Position = new Point(15, 177);
      this.factionSymbols.Checked = GameEngine.Instance.World.worldMapFilter.FilterShowFactionSymbols;
      this.factionSymbols.CBLabel.Text = SK.Text("MapFilterPanel_Show_Faction_Symbols", "Show Faction Symbols");
      this.factionSymbols.CBLabel.Color = ARGBColors.Black;
      this.factionSymbols.CBLabel.Position = new Point(20, -1);
      this.factionSymbols.CBLabel.Size = new Size(180, 25);
      this.factionSymbols.CBLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.factionSymbols.Data = 0;
      this.factionSymbols.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.factionToggled));
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionSymbols);
      this.userSymbols.CheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[0];
      this.userSymbols.UncheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[1];
      this.userSymbols.Position = new Point(15, 197);
      this.userSymbols.Checked = GameEngine.Instance.World.worldMapFilter.FilterShowUserSymbols;
      this.userSymbols.CBLabel.Text = SK.Text("MapFilterPanel_Show_User_Symbols", "Show Player Symbols");
      this.userSymbols.CBLabel.Color = ARGBColors.Black;
      this.userSymbols.CBLabel.Position = new Point(20, -1);
      this.userSymbols.CBLabel.Size = new Size(180, 25);
      this.userSymbols.CBLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.userSymbols.Data = 0;
      this.userSymbols.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.userToggled));
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.userSymbols);
      this.searchButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_search[0];
      this.searchButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_search[1];
      this.searchButton.ImageClick = (Image) GFXLibrary.mrhp_button_filter_search[2];
      this.searchButton.Position = new Point(103, 215);
      this.searchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.searchFilter), "StatsPanel_search");
      this.searchButton.CustomTooltipID = 2460;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.searchButton);
      this.clearButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_off[0];
      this.clearButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_off[1];
      this.clearButton.ImageClick = (Image) GFXLibrary.mrhp_button_filter_off[2];
      this.clearButton.Position = new Point(19, 215);
      this.clearButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clearFilter), "MapFilterPanel2_clear");
      this.clearButton.CustomTooltipID = 2459;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.clearButton);
      this.cancelButton.ImageNorm = (Image) GFXLibrary.mrhp_button_80_normal;
      this.cancelButton.ImageOver = (Image) GFXLibrary.mrhp_button_80_over;
      this.cancelButton.ImageClick = (Image) GFXLibrary.mrhp_button_80_pushed;
      this.cancelButton.Position = new Point(103, 215);
      this.cancelButton.Text.Text = SK.Text("GENERIC_Close", "Close");
      this.cancelButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.cancelButton.Text.Size = new Size(this.cancelButton.ImageNorm.Size.Width - 6, this.cancelButton.ImageNorm.Size.Height);
      this.cancelButton.TextYOffset = -3;
      this.cancelButton.Text.Color = ARGBColors.Black;
      this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "MapFilterPanel2_close");
      this.backImage.invalidate();
      InterfaceMgr.Instance.closeArmySelectedPanel();
      InterfaceMgr.Instance.closeTraderInfoPanel();
      InterfaceMgr.Instance.closeReinforcementSelectedPanel();
      InterfaceMgr.Instance.closePersonInfoPanel();
      InterfaceMgr.Instance.closeSelectedVillagePanel();
      int filterMode = GameEngine.Instance.World.worldMapFilter.FilterMode;
      if (!GameEngine.Instance.World.worldMapFilter.FilterActive)
        return;
      switch (filterMode)
      {
        case 1:
        case 7:
          this.selectedImage.Position = this.factionButton.Position;
          this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
          this.selectedImage.Visible = true;
          break;
        case 2:
          this.selectedImage.Position = this.houseButton.Position;
          this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
          this.selectedImage.Visible = true;
          break;
        case 3:
          this.selectedImage.Position = this.scoutButton.Position;
          this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
          this.selectedImage.Visible = true;
          break;
        case 4:
          this.selectedImage.Position = this.tradeButton.Position;
          this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
          this.selectedImage.Visible = true;
          break;
        case 5:
          this.selectedImage.Position = this.tradeButton.Position;
          this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
          this.selectedImage.Visible = true;
          break;
        case 6:
          this.selectedImage.Position = this.attackButton.Position;
          this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
          this.selectedImage.Visible = true;
          break;
        case 8:
          this.selectedImage.Position = this.aiButton.Position;
          this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
          this.selectedImage.Visible = true;
          break;
      }
    }

    public void update() => this.backGround.update();

    private void factionClick()
    {
      if (RemoteServices.Instance.UserFactionID >= 0)
        GameEngine.Instance.World.worldMapFilter.setFilterMode(1);
      else
        GameEngine.Instance.World.worldMapFilter.setFilterMode(7);
      this.selectedImage.Position = this.factionButton.Position;
      this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
      this.selectedImage.Visible = true;
      this.backImage.invalidate();
    }

    private void houseClick()
    {
      GameEngine.Instance.World.worldMapFilter.setFilterMode(2);
      this.selectedImage.Position = this.houseButton.Position;
      this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
      this.selectedImage.Visible = true;
      this.backImage.invalidate();
    }

    private void tradeClick()
    {
      GameEngine.Instance.World.worldMapFilter.setFilterMode(4);
      this.selectedImage.Position = this.tradeButton.Position;
      this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
      this.selectedImage.Visible = true;
      this.backImage.invalidate();
    }

    private void scoutsClick()
    {
      GameEngine.Instance.World.worldMapFilter.setFilterMode(3);
      this.selectedImage.Position = this.scoutButton.Position;
      this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
      this.selectedImage.Visible = true;
      this.backImage.invalidate();
    }

    private void attackClick()
    {
      GameEngine.Instance.World.worldMapFilter.setFilterMode(6);
      this.selectedImage.Position = this.attackButton.Position;
      this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
      this.selectedImage.Visible = true;
      this.backImage.invalidate();
    }

    private void aiClick()
    {
      GameEngine.Instance.World.worldMapFilter.setFilterMode(8);
      this.selectedImage.Position = this.aiButton.Position;
      this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
      this.selectedImage.Visible = true;
      this.backImage.invalidate();
    }

    private void yourToggled()
    {
      GameEngine.Instance.World.worldMapFilter.FilterAlwaysShowYourVillages = this.yourVillages.Checked;
    }

    private void houseToggled()
    {
      GameEngine.Instance.World.worldMapFilter.FilterShowHouseSymbols = this.houseSymbols.Checked;
    }

    private void factionToggled()
    {
      GameEngine.Instance.World.worldMapFilter.FilterShowFactionSymbols = this.factionSymbols.Checked;
    }

    private void userToggled()
    {
      GameEngine.Instance.World.worldMapFilter.FilterShowUserSymbols = this.userSymbols.Checked;
    }

    private void clearFilter()
    {
      GameEngine.Instance.World.worldMapFilter.setFilterMode(0);
      this.selectedImage.Visible = false;
      this.backImage.invalidate();
    }

    private void closeClick()
    {
      InterfaceMgr.Instance.clearControls();
      InterfaceMgr.Instance.showMapFilterSelectPanel(true, true);
      InterfaceMgr.Instance.selectCurrentUserVillage();
    }

    private void searchFilter()
    {
      SearchForVillagePopup searchForVillagePopup = new SearchForVillagePopup();
      int num = (int) searchForVillagePopup.ShowDialog((IWin32Window) InterfaceMgr.Instance.ParentForm);
      searchForVillagePopup.Dispose();
    }
  }
}
