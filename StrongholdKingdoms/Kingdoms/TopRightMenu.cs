// Decompiled with JetBrains decompiler
// Type: Kingdoms.TopRightMenu
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
  public class TopRightMenu : CustomSelfDrawPanel
  {
    private CustomSelfDrawPanel.CSDButton worldmapButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton btnVillagesRight = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton btnVillageLeft = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage villageButton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel lblVillageName = new CustomSelfDrawPanel.CSDLabel();
    private MainTabBar2 mainTabBar1 = new MainTabBar2();
    private VillageTabBar2 villageTabBar1 = new VillageTabBar2();
    private FactionTabBar2 factionTabBar1 = new FactionTabBar2();
    public MainMenuBar2 mainMenuBar = new MainMenuBar2();
    private MenuPopup villageListMenu;
    private Color highlightColour = Color.FromArgb(232, 230, 228);
    private IContainer components;

    public TopRightMenu()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init()
    {
      this.clearControls();
      this.mainBackgroundImage.Image = (Image) GFXLibrary.menubar_top;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = new Size(463, 120);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.btnVillageLeft.ImageNorm = (Image) GFXLibrary.villagename_button_left_normal;
      this.btnVillageLeft.ImageOver = (Image) GFXLibrary.villagename_button_left_highlight;
      this.btnVillageLeft.ImageClick = (Image) GFXLibrary.villagename_button_left_selected;
      this.btnVillageLeft.Position = new Point(5, 29);
      this.btnVillageLeft.CustomTooltipID = 20;
      this.btnVillageLeft.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnVillageLeft_Click));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnVillageLeft);
      this.btnVillagesRight.ImageNorm = (Image) GFXLibrary.villagename_button_right_normal;
      this.btnVillagesRight.ImageOver = (Image) GFXLibrary.villagename_button_right_highlight;
      this.btnVillagesRight.ImageClick = (Image) GFXLibrary.villagename_button_right_selected;
      this.btnVillagesRight.Position = new Point(24, 29);
      this.btnVillagesRight.CustomTooltipID = 20;
      this.btnVillagesRight.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnVillagesRight_Click));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnVillagesRight);
      this.villageButton.Image = (Image) GFXLibrary.villagename_body;
      this.villageButton.Position = new Point(49, 29);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.villageButton);
      this.lblVillageName.Position = new Point(20, -1);
      this.lblVillageName.Size = new Size(this.villageButton.Size.Width - 35, this.villageButton.Height);
      this.lblVillageName.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblVillageName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblVillageName.Color = ARGBColors.Black;
      this.lblVillageName.CustomTooltipID = 21;
      this.lblVillageName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillageName_Click));
      this.villageButton.addControl((CustomSelfDrawPanel.CSDControl) this.lblVillageName);
      this.mainTabBar1.Position = new Point(3, 51);
      this.mainTabBar1.Size = new Size(460, 40);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainTabBar1);
      this.villageTabBar1.Position = new Point(3, 88);
      this.villageTabBar1.Size = new Size(460, 40);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.villageTabBar1);
      this.factionTabBar1.Position = new Point(3, 88);
      this.factionTabBar1.Size = new Size(460, 40);
      this.factionTabBar1.Visible = false;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionTabBar1);
      this.mainMenuBar.Position = new Point(0, 0);
      this.mainMenuBar.Size = new Size(this.Width, 25);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainMenuBar);
      this.mainTabBar1.init();
      this.villageTabBar1.init();
      this.factionTabBar1.init();
      this.mainMenuBar.init2();
      this.resize();
    }

    public MainTabBar2 getMainTabBar() => this.mainTabBar1;

    public VillageTabBar2 getVillageTabBar() => this.villageTabBar1;

    public FactionTabBar2 getFactionTabBar() => this.factionTabBar1;

    private void btnWorldClick()
    {
      GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(201));
      InterfaceMgr.Instance.getMainTabBar().changeTab(0);
    }

    public void setSelectedVillageName(string villageName, bool asCapital)
    {
      this.lblVillageName.Text = villageName;
    }

    private void btnVillageClick()
    {
      GameEngine.Instance.playInterfaceSound("WorldMapScreen_village_left");
      InterfaceMgr.Instance.centerOnVillage();
    }

    private void btnVillageLeft_Click()
    {
      GameEngine.Instance.playInterfaceSound("WorldMapScreen_village_left");
      InterfaceMgr.Instance.selectedVillageNameLeft();
    }

    private void btnVillagesRight_Click()
    {
      GameEngine.Instance.playInterfaceSound("WorldMapScreen_village_right");
      InterfaceMgr.Instance.selectedVillageNameRight();
    }

    private void lblVillageName_Click()
    {
      if (this.villageListMenu != null && this.villageListMenu.Visible || InterfaceMgr.Instance.menuPopupClosedRecently())
      {
        GameEngine.Instance.playInterfaceSound("WorldMapScreen_village_droplist_close");
        InterfaceMgr.Instance.closeMenuPopup();
      }
      else
      {
        GameEngine.Instance.playInterfaceSound("WorldMapScreen_village_droplist_open");
        this.villageListMenu = new MenuPopup();
        this.villageListMenu.setCallBack(new MenuPopup.MenuCallback(this.comboVillageList_SelectionChangeCommitted));
        this.villageListMenu.setDoubleClickCallBack(new MenuPopup.MenuCallback(this.doubleClickedItem));
        this.villageListMenu.setLineHeight(15);
        this.villageListMenu.closeOnClickOnly();
        this.villageListMenu.mouseOverDelegates(new MenuPopup.MenuItemRolloverDelegate(this.mouseOverItem), new MenuPopup.MenuItemRolloverDelegate(this.mouseLeaveItem));
        this.villageListMenu.setBackColour(Color.FromArgb((int) byte.MaxValue, 186, 175, 163));
        Point screen = this.PointToScreen(this.villageButton.Position);
        this.villageListMenu.setPosition(screen.X + 18, screen.Y + 21);
        List<WorldMap.VillageNameItem> namesListAndCapitals = GameEngine.Instance.World.getUserVillageNamesListAndCapitals();
        int num = 0;
        foreach (WorldMap.VillageNameItem villageNameItem in namesListAndCapitals)
        {
          if (!villageNameItem.capital)
          {
            if (villageNameItem.villageID >= 0)
              ++num;
            else
              break;
          }
          else
            break;
        }
        if (num >= 3)
          this.villageListMenu.addMenuItem(SK.Text("Menu_Your_Villages", "Your Villages") + " (" + num.ToString() + ")", -1).Enabled = false;
        else
          this.villageListMenu.addMenuItem(SK.Text("Menu_Your_Villages", "Your Villages"), -1).Enabled = false;
        this.villageListMenu.addBar();
        bool flag = false;
        foreach (WorldMap.VillageNameItem villageNameItem in namesListAndCapitals)
        {
          if (villageNameItem.villageID < 0)
          {
            this.villageListMenu.newColumn();
            flag = true;
          }
          bool bold = false;
          if (flag && GameEngine.Instance.World.isUserVillage(villageNameItem.villageID))
            bold = true;
          CustomSelfDrawPanel.CSDControl csdControl = (CustomSelfDrawPanel.CSDControl) this.villageListMenu.addMenuItem(villageNameItem.villageName, villageNameItem.villageID, bold);
          if (villageNameItem.villageID < 0)
          {
            csdControl.Enabled = false;
            this.villageListMenu.addBar();
          }
        }
        this.villageListMenu.showMenu();
        MainWindow.captureCloseMenuEvent = true;
      }
    }

    private void mouseOverItem(int id)
    {
      this.villageListMenu.clearHighlights();
      if (id < 0)
        return;
      List<WorldMap.VillageNameItem> namesListAndCapitals = GameEngine.Instance.World.getUserVillageNamesListAndCapitals();
      if (GameEngine.Instance.World.isRegionCapital(id))
      {
        this.highlightRegionsVillages(GameEngine.Instance.World.getParishFromVillageID(id), namesListAndCapitals);
        int countyFromVillageId = GameEngine.Instance.World.getCountyFromVillageID(id);
        this.villageListMenu.highlightByID(GameEngine.Instance.World.getCountyCapitalVillage(countyFromVillageId), this.highlightColour);
        int provinceFromVillageId = GameEngine.Instance.World.getProvinceFromVillageID(id);
        this.villageListMenu.highlightByID(GameEngine.Instance.World.getProvinceCapital(provinceFromVillageId), this.highlightColour);
        int countryFromVillageId = GameEngine.Instance.World.getCountryFromVillageID(id);
        this.villageListMenu.highlightByID(GameEngine.Instance.World.getCountryCapital(countryFromVillageId), this.highlightColour);
      }
      else if (GameEngine.Instance.World.isCountyCapital(id))
      {
        this.highlightCountiesVillages(GameEngine.Instance.World.getCountyFromVillageID(id), namesListAndCapitals);
        int provinceFromVillageId = GameEngine.Instance.World.getProvinceFromVillageID(id);
        this.villageListMenu.highlightByID(GameEngine.Instance.World.getProvinceCapital(provinceFromVillageId), this.highlightColour);
        int countryFromVillageId = GameEngine.Instance.World.getCountryFromVillageID(id);
        this.villageListMenu.highlightByID(GameEngine.Instance.World.getCountryCapital(countryFromVillageId), this.highlightColour);
      }
      else if (GameEngine.Instance.World.isProvinceCapital(id))
      {
        this.highlightProvincesVillages(GameEngine.Instance.World.getProvinceFromVillageID(id), namesListAndCapitals);
        int countryFromVillageId = GameEngine.Instance.World.getCountryFromVillageID(id);
        this.villageListMenu.highlightByID(GameEngine.Instance.World.getCountryCapital(countryFromVillageId), this.highlightColour);
      }
      else if (GameEngine.Instance.World.isCountryCapital(id))
      {
        this.highlightCountriesVillages(GameEngine.Instance.World.getCountryFromVillageID(id), namesListAndCapitals);
      }
      else
      {
        int parishFromVillageId = GameEngine.Instance.World.getParishFromVillageID(id);
        this.villageListMenu.highlightByID(GameEngine.Instance.World.getParishCapital(parishFromVillageId), this.highlightColour);
        int countyFromVillageId = GameEngine.Instance.World.getCountyFromVillageID(id);
        this.villageListMenu.highlightByID(GameEngine.Instance.World.getCountyCapitalVillage(countyFromVillageId), this.highlightColour);
        int provinceFromVillageId = GameEngine.Instance.World.getProvinceFromVillageID(id);
        this.villageListMenu.highlightByID(GameEngine.Instance.World.getProvinceCapital(provinceFromVillageId), this.highlightColour);
        int countryFromVillageId = GameEngine.Instance.World.getCountryFromVillageID(id);
        this.villageListMenu.highlightByID(GameEngine.Instance.World.getCountryCapital(countryFromVillageId), this.highlightColour);
      }
    }

    private void highlightRegionsVillages(
      int testRegionID,
      List<WorldMap.VillageNameItem> namesList)
    {
      foreach (WorldMap.VillageNameItem names in namesList)
      {
        if (names.villageID >= 0 && !names.capital && GameEngine.Instance.World.getParishFromVillageID(names.villageID) == testRegionID)
          this.villageListMenu.highlightByID(names.villageID, this.highlightColour);
      }
    }

    private void highlightCountiesVillages(
      int testCountyID,
      List<WorldMap.VillageNameItem> namesList)
    {
      foreach (WorldMap.VillageNameItem names in namesList)
      {
        if (names.villageID >= 0 && (GameEngine.Instance.World.isRegionCapital(names.villageID) || !names.capital) && GameEngine.Instance.World.getCountyFromVillageID(names.villageID) == testCountyID)
          this.villageListMenu.highlightByID(names.villageID, this.highlightColour);
      }
    }

    private void highlightProvincesVillages(
      int testProvinceID,
      List<WorldMap.VillageNameItem> namesList)
    {
      foreach (WorldMap.VillageNameItem names in namesList)
      {
        if (names.villageID >= 0 && (GameEngine.Instance.World.isRegionCapital(names.villageID) || GameEngine.Instance.World.isCountyCapital(names.villageID) || !names.capital) && GameEngine.Instance.World.getProvinceFromVillageID(names.villageID) == testProvinceID)
          this.villageListMenu.highlightByID(names.villageID, this.highlightColour);
      }
    }

    private void highlightCountriesVillages(
      int testCountryID,
      List<WorldMap.VillageNameItem> namesList)
    {
      foreach (WorldMap.VillageNameItem names in namesList)
      {
        if (names.villageID >= 0 && (GameEngine.Instance.World.isRegionCapital(names.villageID) || GameEngine.Instance.World.isCountyCapital(names.villageID) || GameEngine.Instance.World.isProvinceCapital(names.villageID) || !names.capital) && GameEngine.Instance.World.getCountryFromVillageID(names.villageID) == testCountryID)
          this.villageListMenu.highlightByID(names.villageID, this.highlightColour);
      }
    }

    private void mouseLeaveItem(int id)
    {
    }

    private void comboVillageList_SelectionChangeCommitted(int id)
    {
      if (id < 0)
        return;
      GameEngine.Instance.playInterfaceSound("WorldMapScreen_village_droplist_selected");
      InterfaceMgr.Instance.selectUserVillage(id, true);
    }

    private void doubleClickedItem(int id)
    {
      if (id < 0)
        return;
      if (!GameEngine.Instance.World.isCapital(id))
        InterfaceMgr.Instance.getMainTabBar().changeTab(1);
      else
        InterfaceMgr.Instance.getMainTabBar().changeTab(2);
    }

    public void showVillageTab(bool state)
    {
      if (state)
        this.Invalidate();
      this.villageTabBar1.Visible = state;
      if (!state)
        return;
      this.Invalidate();
    }

    public void showFactionTabBar(bool state) => this.factionTabBar1.Visible = state;

    public void resize()
    {
      this.mainBackgroundImage.Size = new Size(this.Width, 120);
      this.btnVillageLeft.Position = new Point(this.Width - 458, this.btnVillageLeft.Position.Y);
      this.btnVillagesRight.Position = new Point(this.Width - 439, this.btnVillagesRight.Position.Y);
      this.villageButton.Position = new Point(this.Width - 414, this.villageButton.Position.Y);
      this.factionTabBar1.Position = new Point(this.Width - 460, this.factionTabBar1.Position.Y);
      this.mainTabBar1.Position = new Point(this.Width - 460, this.mainTabBar1.Position.Y);
      this.villageTabBar1.Position = new Point(this.Width - 460, this.villageTabBar1.Position.Y);
      this.factionTabBar1.Position = new Point(this.Width - 460, this.factionTabBar1.Position.Y);
      this.mainMenuBar.Size = new Size(this.Width, 25);
      this.mainMenuBar.resize();
      this.Invalidate();
    }

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
      this.BackColor = ARGBColors.White;
      this.MinimumSize = new Size(463, 0);
      this.Name = nameof (TopRightMenu);
      this.Size = new Size(463, 120);
      this.ResumeLayout(false);
    }
  }
}
