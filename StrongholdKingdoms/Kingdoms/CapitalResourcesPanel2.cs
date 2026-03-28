// Decompiled with JetBrains decompiler
// Type: Kingdoms.CapitalResourcesPanel2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class CapitalResourcesPanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea mainBackgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel stockpileHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel stockpileLimitLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage illustration = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDExtendingPanel lightPanel = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel woodLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel stoneLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel ironLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel pitchLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage woodImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage stoneImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage ironImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage pitchImage = new CustomSelfDrawPanel.CSDImage();
    private DockableControl dockableControl;
    private IContainer components;

    public CapitalResourcesPanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init()
    {
      this.clearControls();
      NumberFormatInfo nfi = GameEngine.NFI;
      this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.mainBackgroundArea.Position = new Point(0, 0);
      this.mainBackgroundArea.Size = new Size(992, 566);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea);
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(948, 10);
      this.closeButton.CustomTooltipID = 900;
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CapitalResourcesPanel2_close");
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea, 11, new Point(898, 10));
      Color color1 = Color.FromArgb(224, 203, 146);
      Color color2 = Color.FromArgb(74, 67, 48);
      this.lightPanel.Position = new Point(157, 87);
      this.lightPanel.Size = new Size(343, 390);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.lightPanel);
      this.lightPanel.Create((Image) GFXLibrary.lite_9slice_panel_top_left, (Image) GFXLibrary.lite_9slice_panel_top_mid, (Image) GFXLibrary.lite_9slice_panel_top_right, (Image) GFXLibrary.lite_9slice_panel_mid_left, (Image) GFXLibrary.lite_9slice_panel_mid_mid, (Image) GFXLibrary.lite_9slice_panel_mid_right, (Image) GFXLibrary.lite_9slice_panel_bottom_left, (Image) GFXLibrary.lite_9slice_panel_bottom_mid, (Image) GFXLibrary.lite_9slice_panel_bottom_right);
      this.illustration.Image = (Image) GFXLibrary.donate_illustration;
      this.illustration.Position = new Point(513, 87);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.illustration);
      this.stockpileHeaderLabel.Text = SK.Text("ResourcesPanel_Parish_Resources", "Parish Resources");
      this.stockpileHeaderLabel.Color = color1;
      this.stockpileHeaderLabel.DropShadowColor = color2;
      this.stockpileHeaderLabel.Position = new Point(9, 9);
      this.stockpileHeaderLabel.Size = new Size(992, 50);
      this.stockpileHeaderLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
      this.stockpileHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.stockpileHeaderLabel);
      this.stockpileLimitLabel.Text = SK.Text("ResourcesPanel_Parish_Capacity", "Capacity of the Warehouse") + ": " + 100000.ToString("N", (IFormatProvider) nfi);
      this.stockpileLimitLabel.Color = color1;
      this.stockpileLimitLabel.DropShadowColor = color2;
      this.stockpileLimitLabel.Position = new Point(523, 418);
      this.stockpileLimitLabel.Size = new Size(325, 50);
      this.stockpileLimitLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.stockpileLimitLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.stockpileLimitLabel);
      this.woodLabel.Text = "0";
      this.woodLabel.Color = color1;
      this.woodLabel.DropShadowColor = color2;
      this.woodLabel.Position = new Point(120, 50);
      this.woodLabel.Size = new Size(200, 50);
      this.woodLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.woodLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lightPanel.addControl((CustomSelfDrawPanel.CSDControl) this.woodLabel);
      this.stoneLabel.Text = "0";
      this.stoneLabel.Color = color1;
      this.stoneLabel.DropShadowColor = color2;
      this.stoneLabel.Position = new Point(120, 135);
      this.stoneLabel.Size = new Size(200, 50);
      this.stoneLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.stoneLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lightPanel.addControl((CustomSelfDrawPanel.CSDControl) this.stoneLabel);
      this.ironLabel.Text = "0";
      this.ironLabel.Color = color1;
      this.ironLabel.DropShadowColor = color2;
      this.ironLabel.Position = new Point(120, 220);
      this.ironLabel.Size = new Size(200, 50);
      this.ironLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.ironLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lightPanel.addControl((CustomSelfDrawPanel.CSDControl) this.ironLabel);
      this.pitchLabel.Text = "0";
      this.pitchLabel.Color = color1;
      this.pitchLabel.DropShadowColor = color2;
      this.pitchLabel.Position = new Point(120, 305);
      this.pitchLabel.Size = new Size(200, 50);
      this.pitchLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.pitchLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lightPanel.addControl((CustomSelfDrawPanel.CSDControl) this.pitchLabel);
      this.woodImage.Image = (Image) GFXLibrary.getCommodity64DSImage(6);
      this.woodImage.Position = new Point(18, 24);
      this.lightPanel.addControl((CustomSelfDrawPanel.CSDControl) this.woodImage);
      this.stoneImage.Image = (Image) GFXLibrary.getCommodity64DSImage(7);
      this.stoneImage.Position = new Point(18, 109);
      this.lightPanel.addControl((CustomSelfDrawPanel.CSDControl) this.stoneImage);
      this.ironImage.Image = (Image) GFXLibrary.getCommodity64DSImage(8);
      this.ironImage.Position = new Point(18, 194);
      this.lightPanel.addControl((CustomSelfDrawPanel.CSDControl) this.ironImage);
      this.pitchImage.Image = (Image) GFXLibrary.getCommodity64DSImage(9);
      this.pitchImage.Position = new Point(18, 279);
      this.lightPanel.addControl((CustomSelfDrawPanel.CSDControl) this.pitchImage);
      this.update();
    }

    public void update()
    {
      VillageMap village = GameEngine.Instance.Village;
      if (village == null)
        return;
      if (GameEngine.Instance.World.isRegionCapital(village.VillageID))
        this.stockpileHeaderLabel.Text = SK.Text("ResourcesPanel_Parish_Resources", "Parish Resources");
      else if (GameEngine.Instance.World.isCountyCapital(village.VillageID))
        this.stockpileHeaderLabel.Text = SK.Text("ResourcesPanel_County_Resources", "County Resources");
      else if (GameEngine.Instance.World.isProvinceCapital(village.VillageID))
        this.stockpileHeaderLabel.Text = SK.Text("ResourcesPanel_Province_Resources", "Province Resources");
      else if (GameEngine.Instance.World.isCountryCapital(village.VillageID))
        this.stockpileHeaderLabel.Text = SK.Text("ResourcesPanel_Country_Resources", "Country Resources");
      NumberFormatInfo nfi = GameEngine.NFI;
      VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
      village.getStockpileLevels(levels);
      this.woodLabel.Text = SK.Text("ResourceTypeWood", "Wood") + ": " + levels.woodLevel.ToString("N", (IFormatProvider) nfi);
      this.stoneLabel.Text = SK.Text("ResourceType_Stone", "Stone") + ": " + levels.stoneLevel.ToString("N", (IFormatProvider) nfi);
      this.pitchLabel.Text = SK.Text("ResourceType_Pitch", "Pitch") + ": " + levels.pitchLevel.ToString("N", (IFormatProvider) nfi);
      this.ironLabel.Text = SK.Text("ResourceType_Iron", "Iron") + ": " + levels.ironLevel.ToString("N", (IFormatProvider) nfi);
    }

    private int getCap(int resourceType)
    {
      return (int) GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, resourceType, true);
    }

    public void closeClick() => InterfaceMgr.Instance.setVillageTabSubMode(-1);

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
      this.SuspendLayout();
      this.AutoScaleMode = AutoScaleMode.None;
      this.MaximumSize = new Size(992, 566);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (CapitalResourcesPanel2);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }
  }
}
