// Decompiled with JetBrains decompiler
// Type: Kingdoms.ResourcesPanel2
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
  public class ResourcesPanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea mainBackgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel resourcesLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel stockpileHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel stockpileLimitLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel woodLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel stoneLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel ironLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel pitchLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage woodImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage stoneImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage ironImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage pitchImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea woodClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea stoneClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea ironClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea pitchClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel hallHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel hallLimitLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel venisonLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel furnitureLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel metalwareLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel clothesLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel wineLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel saltLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel spicesLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel silkLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage venisonImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage furnitureImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage metalwareImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage clothesImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage wineImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage saltImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage spicesImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage silkImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea venisonClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea furnitureClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea metalwareClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea clothesClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea wineClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea saltClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea spicesClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea silkClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel granaryHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel granaryLimitLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel applesLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel cheeseLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel meatLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel vegLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel breadLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel fishLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage applesImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage cheeseImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage meatImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage vegImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage breadImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage fishImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea applesClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea cheeseClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea meatClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea vegClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea breadClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea fishClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel innHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel innLimitLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel aleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage aleImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea aleClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel armouryHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel armouryLimitLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel bowsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel pikesLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel armourLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel swordsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel catapultsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage bowsImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage pikesImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage armourImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage swordsImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage catapultsImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea bowsClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea pikesClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea armourClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea swordsClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea catapultsClickArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel selectedHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage selectedImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel dailyProductionHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel dailyProductionValueLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel totalBuildingsHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel totalBuildingsValueLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel workingBuildingsHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel workingBuildingsValueLabel = new CustomSelfDrawPanel.CSDLabel();
    private CardBarGDI cardbar = new CardBarGDI();
    private int selectedResource = -1;
    private DockableControl dockableControl;
    private IContainer components;

    public ResourcesPanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init()
    {
      this.clearControls();
      this.mainBackgroundImage.Image = (Image) GFXLibrary.goods_background;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.mainBackgroundArea.Position = new Point(0, 0);
      this.mainBackgroundArea.Size = new Size(992, 566);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea);
      InterfaceMgr.Instance.setVillageHeading(SK.Text("ResourcesPanel_Resources", "Resources"));
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(948, 10);
      this.closeButton.CustomTooltipID = 900;
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "ResourcesPanel2_close");
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea, 3, new Point(898, 10));
      Color color1 = Color.FromArgb((int) byte.MaxValue, 230, 167);
      Color color2 = Color.FromArgb(85, 76, 55);
      this.stockpileHeaderLabel.Text = SK.Text("BuildingTypes_Stockpile", "Stockpile");
      this.stockpileHeaderLabel.Color = color1;
      this.stockpileHeaderLabel.DropShadowColor = color2;
      this.stockpileHeaderLabel.Position = new Point(13, 63);
      this.stockpileHeaderLabel.Size = new Size(325, 50);
      this.stockpileHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.stockpileHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.stockpileHeaderLabel);
      this.stockpileLimitLabel.Text = "(0)";
      this.stockpileLimitLabel.Color = Color.FromArgb((int) byte.MaxValue, 230, 167);
      this.stockpileLimitLabel.DropShadowColor = Color.FromArgb(85, 76, 55);
      this.stockpileLimitLabel.Position = new Point(13, 83);
      this.stockpileLimitLabel.Size = new Size(325, 50);
      this.stockpileLimitLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.stockpileLimitLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.stockpileLimitLabel);
      this.woodLabel.Text = "0";
      this.woodLabel.Color = ARGBColors.Black;
      this.woodLabel.Position = new Point(13, 151);
      this.woodLabel.Size = new Size(81, 50);
      this.woodLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.woodLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.woodLabel);
      this.stoneLabel.Text = "0";
      this.stoneLabel.Color = ARGBColors.Black;
      this.stoneLabel.Position = new Point(93, 151);
      this.stoneLabel.Size = new Size(81, 50);
      this.stoneLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.stoneLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.stoneLabel);
      this.ironLabel.Text = "0";
      this.ironLabel.Color = ARGBColors.Black;
      this.ironLabel.Position = new Point(173, 151);
      this.ironLabel.Size = new Size(81, 50);
      this.ironLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.ironLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.ironLabel);
      this.pitchLabel.Text = "0";
      this.pitchLabel.Color = ARGBColors.Black;
      this.pitchLabel.Position = new Point(253, 151);
      this.pitchLabel.Size = new Size(81, 50);
      this.pitchLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.pitchLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.pitchLabel);
      this.woodImage.Image = (Image) GFXLibrary.com_32_wood_DS;
      this.woodImage.Position = new Point(53 - this.woodImage.Size.Width / 2, 102);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.woodImage);
      this.stoneImage.Image = (Image) GFXLibrary.com_32_stone_DS;
      this.stoneImage.Position = new Point(133 - this.stoneImage.Size.Width / 2, 102);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.stoneImage);
      this.ironImage.Image = (Image) GFXLibrary.com_32_iron_DS;
      this.ironImage.Position = new Point(213 - this.ironImage.Size.Width / 2, 102);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.ironImage);
      this.pitchImage.Image = (Image) GFXLibrary.com_32_pitch_DS;
      this.pitchImage.Position = new Point(293 - this.pitchImage.Size.Width / 2, 102);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.pitchImage);
      this.woodClickArea.Position = new Point(this.woodLabel.X, this.woodLabel.Y - 50);
      this.woodClickArea.Size = new Size(80, 70);
      this.woodClickArea.Data = 6;
      this.woodClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.woodClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.woodClickArea);
      this.stoneClickArea.Position = new Point(this.stoneLabel.X, this.stoneLabel.Y - 50);
      this.stoneClickArea.Size = new Size(80, 70);
      this.stoneClickArea.Data = 7;
      this.stoneClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.stoneClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.stoneClickArea);
      this.ironClickArea.Position = new Point(this.ironLabel.X, this.ironLabel.Y - 50);
      this.ironClickArea.Size = new Size(80, 70);
      this.ironClickArea.Data = 8;
      this.ironClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.ironClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.ironClickArea);
      this.pitchClickArea.Position = new Point(this.pitchLabel.X, this.pitchLabel.Y - 50);
      this.pitchClickArea.Size = new Size(80, 70);
      this.pitchClickArea.Data = 9;
      this.pitchClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.pitchClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.pitchClickArea);
      this.hallHeaderLabel.Text = SK.Text("BuildingTypes_Village_Hall", "Village Hall");
      this.hallHeaderLabel.Color = color1;
      this.hallHeaderLabel.DropShadowColor = color2;
      this.hallHeaderLabel.Position = new Point(348, 63);
      this.hallHeaderLabel.Size = new Size(633, 50);
      this.hallHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.hallHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.hallHeaderLabel);
      this.hallLimitLabel.Text = "(0)";
      this.hallLimitLabel.Color = Color.FromArgb((int) byte.MaxValue, 230, 167);
      this.hallLimitLabel.DropShadowColor = Color.FromArgb(85, 76, 55);
      this.hallLimitLabel.Position = new Point(348, 83);
      this.hallLimitLabel.Size = new Size(633, 50);
      this.hallLimitLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.hallLimitLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.hallLimitLabel);
      this.venisonLabel.Text = "0";
      this.venisonLabel.Color = ARGBColors.Black;
      this.venisonLabel.Position = new Point(348, 151);
      this.venisonLabel.Size = new Size(81, 50);
      this.venisonLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.venisonLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.venisonLabel);
      this.furnitureLabel.Text = "0";
      this.furnitureLabel.Color = ARGBColors.Black;
      this.furnitureLabel.Position = new Point(427, 151);
      this.furnitureLabel.Size = new Size(81, 50);
      this.furnitureLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.furnitureLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.furnitureLabel);
      this.metalwareLabel.Text = "0";
      this.metalwareLabel.Color = ARGBColors.Black;
      this.metalwareLabel.Position = new Point(506, 151);
      this.metalwareLabel.Size = new Size(81, 50);
      this.metalwareLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.metalwareLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.metalwareLabel);
      this.clothesLabel.Text = "0";
      this.clothesLabel.Color = ARGBColors.Black;
      this.clothesLabel.Position = new Point(585, 151);
      this.clothesLabel.Size = new Size(81, 50);
      this.clothesLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.clothesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.clothesLabel);
      this.wineLabel.Text = "0";
      this.wineLabel.Color = ARGBColors.Black;
      this.wineLabel.Position = new Point(664, 151);
      this.wineLabel.Size = new Size(81, 50);
      this.wineLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.wineLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.wineLabel);
      this.saltLabel.Text = "0";
      this.saltLabel.Color = ARGBColors.Black;
      this.saltLabel.Position = new Point(743, 151);
      this.saltLabel.Size = new Size(81, 50);
      this.saltLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.saltLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.saltLabel);
      this.spicesLabel.Text = "0";
      this.spicesLabel.Color = ARGBColors.Black;
      this.spicesLabel.Position = new Point(822, 151);
      this.spicesLabel.Size = new Size(81, 50);
      this.spicesLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.spicesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.spicesLabel);
      this.silkLabel.Text = "0";
      this.silkLabel.Color = ARGBColors.Black;
      this.silkLabel.Position = new Point(901, 151);
      this.silkLabel.Size = new Size(81, 50);
      this.silkLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.silkLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.silkLabel);
      this.venisonImage.Image = (Image) GFXLibrary.com_32_venison_DS;
      this.venisonImage.Position = new Point(387 - this.venisonImage.Size.Width / 2, 102);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.venisonImage);
      this.furnitureImage.Image = (Image) GFXLibrary.com_32_furniture_DS;
      this.furnitureImage.Position = new Point(466 - this.furnitureImage.Size.Width / 2, 102);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.furnitureImage);
      this.metalwareImage.Image = (Image) GFXLibrary.com_32_metalware_DS;
      this.metalwareImage.Position = new Point(545 - this.metalwareImage.Size.Width / 2, 102);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.metalwareImage);
      this.clothesImage.Image = (Image) GFXLibrary.com_32_clothes_DS;
      this.clothesImage.Position = new Point(624 - this.clothesImage.Size.Width / 2, 102);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.clothesImage);
      this.wineImage.Image = (Image) GFXLibrary.com_32_wine_DS;
      this.wineImage.Position = new Point(703 - this.wineImage.Size.Width / 2, 102);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.wineImage);
      this.saltImage.Image = (Image) GFXLibrary.com_32_salt_DS;
      this.saltImage.Position = new Point(782 - this.saltImage.Size.Width / 2, 102);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.saltImage);
      this.spicesImage.Image = (Image) GFXLibrary.com_32_spices_DS;
      this.spicesImage.Position = new Point(861 - this.spicesImage.Size.Width / 2, 102);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.spicesImage);
      this.silkImage.Image = (Image) GFXLibrary.com_32_silk_DS;
      this.silkImage.Position = new Point(940 - this.silkImage.Size.Width / 2, 102);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.silkImage);
      this.venisonClickArea.Position = new Point(this.venisonLabel.X, this.venisonLabel.Y - 50);
      this.venisonClickArea.Size = new Size(79, 70);
      this.venisonClickArea.Data = 22;
      this.venisonClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.venisonClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.venisonClickArea);
      this.furnitureClickArea.Position = new Point(this.furnitureLabel.X, this.furnitureLabel.Y - 50);
      this.furnitureClickArea.Size = new Size(79, 70);
      this.furnitureClickArea.Data = 21;
      this.furnitureClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.furnitureClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.furnitureClickArea);
      this.metalwareClickArea.Position = new Point(this.metalwareLabel.X, this.metalwareLabel.Y - 50);
      this.metalwareClickArea.Size = new Size(79, 70);
      this.metalwareClickArea.Data = 26;
      this.metalwareClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.metalwareClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.metalwareClickArea);
      this.clothesClickArea.Position = new Point(this.clothesLabel.X, this.clothesLabel.Y - 50);
      this.clothesClickArea.Size = new Size(79, 70);
      this.clothesClickArea.Data = 19;
      this.clothesClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.clothesClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.clothesClickArea);
      this.wineClickArea.Position = new Point(this.wineLabel.X, this.wineLabel.Y - 50);
      this.wineClickArea.Size = new Size(79, 70);
      this.wineClickArea.Data = 33;
      this.wineClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.wineClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.wineClickArea);
      this.saltClickArea.Position = new Point(this.saltLabel.X, this.saltLabel.Y - 50);
      this.saltClickArea.Size = new Size(79, 70);
      this.saltClickArea.Data = 23;
      this.saltClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.saltClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.saltClickArea);
      this.spicesClickArea.Position = new Point(this.spicesLabel.X, this.spicesLabel.Y - 50);
      this.spicesClickArea.Size = new Size(79, 70);
      this.spicesClickArea.Data = 24;
      this.spicesClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.spicesClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.spicesClickArea);
      this.silkClickArea.Position = new Point(this.silkLabel.X, this.silkLabel.Y - 50);
      this.silkClickArea.Size = new Size(79, 70);
      this.silkClickArea.Data = 25;
      this.silkClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.silkClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.silkClickArea);
      this.granaryHeaderLabel.Text = SK.Text("BuildingTypes_Granary", "Granary");
      this.granaryHeaderLabel.Color = color1;
      this.granaryHeaderLabel.DropShadowColor = color2;
      this.granaryHeaderLabel.Position = new Point(13, 225);
      this.granaryHeaderLabel.Size = new Size(478, 50);
      this.granaryHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.granaryHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.granaryHeaderLabel);
      this.granaryLimitLabel.Text = "(0)";
      this.granaryLimitLabel.Color = Color.FromArgb((int) byte.MaxValue, 230, 167);
      this.granaryLimitLabel.DropShadowColor = Color.FromArgb(85, 76, 55);
      this.granaryLimitLabel.Position = new Point(13, 245);
      this.granaryLimitLabel.Size = new Size(478, 50);
      this.granaryLimitLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.granaryLimitLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.granaryLimitLabel);
      this.applesLabel.Text = "0";
      this.applesLabel.Color = ARGBColors.Black;
      this.applesLabel.Position = new Point(13, 313);
      this.applesLabel.Size = new Size(81, 50);
      this.applesLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.applesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.applesLabel);
      this.cheeseLabel.Text = "0";
      this.cheeseLabel.Color = ARGBColors.Black;
      this.cheeseLabel.Position = new Point(93, 313);
      this.cheeseLabel.Size = new Size(81, 50);
      this.cheeseLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.cheeseLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.cheeseLabel);
      this.meatLabel.Text = "0";
      this.meatLabel.Color = ARGBColors.Black;
      this.meatLabel.Position = new Point(173, 313);
      this.meatLabel.Size = new Size(81, 50);
      this.meatLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.meatLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.meatLabel);
      this.vegLabel.Text = "0";
      this.vegLabel.Color = ARGBColors.Black;
      this.vegLabel.Position = new Point(333, 313);
      this.vegLabel.Size = new Size(81, 50);
      this.vegLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.vegLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.vegLabel);
      this.breadLabel.Text = "0";
      this.breadLabel.Color = ARGBColors.Black;
      this.breadLabel.Position = new Point(253, 313);
      this.breadLabel.Size = new Size(81, 50);
      this.breadLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.breadLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.breadLabel);
      this.fishLabel.Text = "0";
      this.fishLabel.Color = ARGBColors.Black;
      this.fishLabel.Position = new Point(413, 313);
      this.fishLabel.Size = new Size(81, 50);
      this.fishLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.fishLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.fishLabel);
      this.applesImage.Image = (Image) GFXLibrary.com_32_apples_DS;
      this.applesImage.Position = new Point(53 - this.applesImage.Size.Width / 2, 264);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.applesImage);
      this.cheeseImage.Image = (Image) GFXLibrary.com_32_cheese_DS;
      this.cheeseImage.Position = new Point(133 - this.cheeseImage.Size.Width / 2, 264);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.cheeseImage);
      this.meatImage.Image = (Image) GFXLibrary.com_32_meat_DS;
      this.meatImage.Position = new Point(213 - this.meatImage.Size.Width / 2, 264);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.meatImage);
      this.vegImage.Image = (Image) GFXLibrary.com_32_veg_DS;
      this.vegImage.Position = new Point(373 - this.vegImage.Size.Width / 2, 264);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.vegImage);
      this.breadImage.Image = (Image) GFXLibrary.com_32_bread_DS;
      this.breadImage.Position = new Point(293 - this.breadImage.Size.Width / 2, 264);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.breadImage);
      this.fishImage.Image = (Image) GFXLibrary.com_32_fish_DS;
      this.fishImage.Position = new Point(453 - this.fishImage.Size.Width / 2, 264);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.fishImage);
      this.applesClickArea.Position = new Point(this.applesLabel.X, this.applesLabel.Y - 50);
      this.applesClickArea.Size = new Size(80, 70);
      this.applesClickArea.Data = 13;
      this.applesClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.applesClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.applesClickArea);
      this.cheeseClickArea.Position = new Point(this.cheeseLabel.X, this.cheeseLabel.Y - 50);
      this.cheeseClickArea.Size = new Size(80, 70);
      this.cheeseClickArea.Data = 17;
      this.cheeseClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.cheeseClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.cheeseClickArea);
      this.meatClickArea.Position = new Point(this.meatLabel.X, this.meatLabel.Y - 50);
      this.meatClickArea.Size = new Size(80, 70);
      this.meatClickArea.Data = 16;
      this.meatClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.meatClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.meatClickArea);
      this.vegClickArea.Position = new Point(this.vegLabel.X, this.vegLabel.Y - 50);
      this.vegClickArea.Size = new Size(80, 70);
      this.vegClickArea.Data = 15;
      this.vegClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.vegClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.vegClickArea);
      this.breadClickArea.Position = new Point(this.breadLabel.X, this.breadLabel.Y - 50);
      this.breadClickArea.Size = new Size(80, 70);
      this.breadClickArea.Data = 14;
      this.breadClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.breadClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.breadClickArea);
      this.fishClickArea.Position = new Point(this.fishLabel.X, this.fishLabel.Y - 50);
      this.fishClickArea.Size = new Size(80, 70);
      this.fishClickArea.Data = 18;
      this.fishClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.fishClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.fishClickArea);
      this.innHeaderLabel.Text = SK.Text("BuildingTypes_Inn", "Inn");
      this.innHeaderLabel.Color = color1;
      this.innHeaderLabel.DropShadowColor = color2;
      this.innHeaderLabel.Position = new Point(475, 225);
      this.innHeaderLabel.Size = new Size(122, 50);
      this.innHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.innHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.innHeaderLabel);
      this.innLimitLabel.Text = "(0)";
      this.innLimitLabel.Color = Color.FromArgb((int) byte.MaxValue, 230, 167);
      this.innLimitLabel.DropShadowColor = Color.FromArgb(85, 76, 55);
      this.innLimitLabel.Position = new Point(500, 245);
      this.innLimitLabel.Size = new Size(72, 50);
      this.innLimitLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.innLimitLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.innLimitLabel);
      this.aleLabel.Text = "0";
      this.aleLabel.Color = ARGBColors.Black;
      this.aleLabel.Position = new Point(475, 313);
      this.aleLabel.Size = new Size(122, 50);
      this.aleLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.aleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.aleLabel);
      this.aleImage.Image = (Image) GFXLibrary.com_32_ale_DS;
      this.aleImage.Position = new Point(536 - this.aleImage.Size.Width / 2, 264);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.aleImage);
      this.aleClickArea.Position = new Point(this.aleLabel.X, this.aleLabel.Y - 50);
      this.aleClickArea.Size = new Size(72, 70);
      this.aleClickArea.Data = 12;
      this.aleClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.aleClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.aleClickArea);
      this.armouryHeaderLabel.Text = SK.Text("BuildingTypes_Armoury", "Armoury");
      this.armouryHeaderLabel.Color = color1;
      this.armouryHeaderLabel.DropShadowColor = color2;
      this.armouryHeaderLabel.Position = new Point(583, 225);
      this.armouryHeaderLabel.Size = new Size(398, 50);
      this.armouryHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.armouryHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.armouryHeaderLabel);
      this.armouryLimitLabel.Text = "(0)";
      this.armouryLimitLabel.Color = Color.FromArgb((int) byte.MaxValue, 230, 167);
      this.armouryLimitLabel.DropShadowColor = Color.FromArgb(85, 76, 55);
      this.armouryLimitLabel.Position = new Point(583, 245);
      this.armouryLimitLabel.Size = new Size(398, 50);
      this.armouryLimitLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.armouryLimitLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.armouryLimitLabel);
      this.bowsLabel.Text = "0";
      this.bowsLabel.Color = ARGBColors.Black;
      this.bowsLabel.Position = new Point(583, 313);
      this.bowsLabel.Size = new Size(81, 50);
      this.bowsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.bowsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.bowsLabel);
      this.pikesLabel.Text = "0";
      this.pikesLabel.Color = ARGBColors.Black;
      this.pikesLabel.Position = new Point(663, 313);
      this.pikesLabel.Size = new Size(81, 50);
      this.pikesLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.pikesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.pikesLabel);
      this.armourLabel.Text = "0";
      this.armourLabel.Color = ARGBColors.Black;
      this.armourLabel.Position = new Point(743, 313);
      this.armourLabel.Size = new Size(81, 50);
      this.armourLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.armourLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.armourLabel);
      this.swordsLabel.Text = "0";
      this.swordsLabel.Color = ARGBColors.Black;
      this.swordsLabel.Position = new Point(823, 313);
      this.swordsLabel.Size = new Size(81, 50);
      this.swordsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.swordsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.swordsLabel);
      this.catapultsLabel.Text = "0";
      this.catapultsLabel.Color = ARGBColors.Black;
      this.catapultsLabel.Position = new Point(903, 313);
      this.catapultsLabel.Size = new Size(81, 50);
      this.catapultsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.catapultsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.catapultsLabel);
      this.bowsImage.Image = (Image) GFXLibrary.com_32_bows_DS;
      this.bowsImage.Position = new Point(623 - this.bowsImage.Size.Width / 2, 264);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.bowsImage);
      this.pikesImage.Image = (Image) GFXLibrary.com_32_pikes_DS;
      this.pikesImage.Position = new Point(703 - this.pikesImage.Size.Width / 2, 264);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.pikesImage);
      this.armourImage.Image = (Image) GFXLibrary.com_32_armour_DS;
      this.armourImage.Position = new Point(783 - this.armourImage.Size.Width / 2, 264);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.armourImage);
      this.swordsImage.Image = (Image) GFXLibrary.com_32_swords_DS;
      this.swordsImage.Position = new Point(863 - this.swordsImage.Size.Width / 2, 264);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.swordsImage);
      this.catapultsImage.Image = (Image) GFXLibrary.com_32_catapults_DS;
      this.catapultsImage.Position = new Point(943 - this.catapultsImage.Size.Width / 2, 264);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.catapultsImage);
      this.bowsClickArea.Position = new Point(this.bowsLabel.X, this.bowsLabel.Y - 50);
      this.bowsClickArea.Size = new Size(80, 70);
      this.bowsClickArea.Data = 29;
      this.bowsClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.bowsClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.bowsClickArea);
      this.pikesClickArea.Position = new Point(this.pikesLabel.X, this.pikesLabel.Y - 50);
      this.pikesClickArea.Size = new Size(80, 70);
      this.pikesClickArea.Data = 28;
      this.pikesClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.pikesClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.pikesClickArea);
      this.armourClickArea.Position = new Point(this.armourLabel.X, this.armourLabel.Y - 50);
      this.armourClickArea.Size = new Size(80, 70);
      this.armourClickArea.Data = 31;
      this.armourClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.armourClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.armourClickArea);
      this.swordsClickArea.Position = new Point(this.swordsLabel.X, this.swordsLabel.Y - 50);
      this.swordsClickArea.Size = new Size(80, 70);
      this.swordsClickArea.Data = 30;
      this.swordsClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.swordsClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.swordsClickArea);
      this.catapultsClickArea.Position = new Point(this.catapultsLabel.X, this.catapultsLabel.Y - 50);
      this.catapultsClickArea.Size = new Size(80, 70);
      this.catapultsClickArea.Data = 32;
      this.catapultsClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
      this.catapultsClickArea.CustomTooltipID = 901;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.catapultsClickArea);
      this.selectedHeadingLabel.Text = SK.Text("ResourcesPanel_Resources", "Resources");
      this.selectedHeadingLabel.Color = Color.FromArgb(224, 203, 146);
      this.selectedHeadingLabel.DropShadowColor = Color.FromArgb(74, 67, 48);
      this.selectedHeadingLabel.Position = new Point(118, 364);
      this.selectedHeadingLabel.Size = new Size(992, 50);
      this.selectedHeadingLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.selectedHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.selectedHeadingLabel.Visible = false;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.selectedHeadingLabel);
      this.selectedResource = -1;
      this.selectedImage.Image = (Image) GFXLibrary.com_32_fish_DS;
      this.selectedImage.Position = new Point(25, 354);
      this.selectedImage.Visible = false;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.selectedImage);
      this.dailyProductionHeadingLabel.Text = SK.Text("ResourcesPanel_Daily_Production", "Daily Production") + " :";
      this.dailyProductionHeadingLabel.Color = ARGBColors.Black;
      this.dailyProductionHeadingLabel.Position = new Point(63, 441);
      this.dailyProductionHeadingLabel.Size = new Size(400, 50);
      this.dailyProductionHeadingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.dailyProductionHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.dailyProductionHeadingLabel.Visible = false;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.dailyProductionHeadingLabel);
      this.totalBuildingsHeadingLabel.Text = SK.Text("ResourcesPanel_Number_Of_Buildings", "Number of Buildings") + " :";
      this.totalBuildingsHeadingLabel.Color = ARGBColors.Black;
      this.totalBuildingsHeadingLabel.Position = new Point(63, 466);
      this.totalBuildingsHeadingLabel.Size = new Size(400, 50);
      this.totalBuildingsHeadingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.totalBuildingsHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.totalBuildingsHeadingLabel.Visible = false;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.totalBuildingsHeadingLabel);
      this.workingBuildingsHeadingLabel.Text = SK.Text("ResourcesPanel_Number_Of_Working_Buildings", "Number of Working Buildings") + " :";
      this.workingBuildingsHeadingLabel.Color = ARGBColors.Black;
      this.workingBuildingsHeadingLabel.Position = new Point(63, 491);
      this.workingBuildingsHeadingLabel.Size = new Size(400, 50);
      this.workingBuildingsHeadingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.workingBuildingsHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.workingBuildingsHeadingLabel.Visible = false;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.workingBuildingsHeadingLabel);
      this.dailyProductionValueLabel.Text = "0";
      this.dailyProductionValueLabel.Color = ARGBColors.Black;
      this.dailyProductionValueLabel.Position = new Point(330, 441);
      this.dailyProductionValueLabel.Size = new Size(400, 50);
      this.dailyProductionValueLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.dailyProductionValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.dailyProductionValueLabel.Visible = false;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.dailyProductionValueLabel);
      this.totalBuildingsValueLabel.Text = "0";
      this.totalBuildingsValueLabel.Color = ARGBColors.Black;
      this.totalBuildingsValueLabel.Position = new Point(330, 466);
      this.totalBuildingsValueLabel.Size = new Size(400, 50);
      this.totalBuildingsValueLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.totalBuildingsValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.totalBuildingsValueLabel.Visible = false;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.totalBuildingsValueLabel);
      this.workingBuildingsValueLabel.Text = "0";
      this.workingBuildingsValueLabel.Color = ARGBColors.Black;
      this.workingBuildingsValueLabel.Position = new Point(330, 491);
      this.workingBuildingsValueLabel.Size = new Size(400, 50);
      this.workingBuildingsValueLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.workingBuildingsValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.workingBuildingsValueLabel.Visible = false;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.workingBuildingsValueLabel);
      this.cardbar.Position = new Point(0, 0);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.cardbar);
      this.cardbar.init(4);
      this.update();
    }

    public void update()
    {
      VillageMap village = GameEngine.Instance.Village;
      if (village == null)
        return;
      NumberFormatInfo nfi = GameEngine.NFI;
      VillageMap.StockpileLevels levels1 = new VillageMap.StockpileLevels();
      village.getStockpileLevels(levels1);
      VillageMap.GranaryLevels levels2 = new VillageMap.GranaryLevels();
      village.getGranaryLevels(levels2);
      VillageMap.ArmouryLevels levels3 = new VillageMap.ArmouryLevels();
      village.getArmouryLevels(levels3);
      VillageMap.TownHallLevels levels4 = new VillageMap.TownHallLevels();
      village.getTownHallLevels(levels4);
      VillageMap.InnLevels levels5 = new VillageMap.InnLevels();
      village.getInnLevels(levels5);
      this.woodLabel.Text = levels1.woodLevel.ToString("N", (IFormatProvider) nfi);
      this.stoneLabel.Text = levels1.stoneLevel.ToString("N", (IFormatProvider) nfi);
      this.pitchLabel.Text = levels1.pitchLevel.ToString("N", (IFormatProvider) nfi);
      this.ironLabel.Text = levels1.ironLevel.ToString("N", (IFormatProvider) nfi);
      this.aleLabel.Text = levels5.aleLevel.ToString("N", (IFormatProvider) nfi);
      this.applesLabel.Text = levels2.applesLevel.ToString("N", (IFormatProvider) nfi);
      this.breadLabel.Text = levels2.breadLevel.ToString("N", (IFormatProvider) nfi);
      this.cheeseLabel.Text = levels2.cheeseLevel.ToString("N", (IFormatProvider) nfi);
      this.meatLabel.Text = levels2.meatLevel.ToString("N", (IFormatProvider) nfi);
      this.vegLabel.Text = levels2.vegLevel.ToString("N", (IFormatProvider) nfi);
      this.fishLabel.Text = levels2.fishLevel.ToString("N", (IFormatProvider) nfi);
      this.bowsLabel.Text = levels3.bowsLevel.ToString("N", (IFormatProvider) nfi);
      this.pikesLabel.Text = levels3.pikesLevel.ToString("N", (IFormatProvider) nfi);
      this.swordsLabel.Text = levels3.swordsLevel.ToString("N", (IFormatProvider) nfi);
      this.armourLabel.Text = levels3.armourLevel.ToString("N", (IFormatProvider) nfi);
      this.catapultsLabel.Text = levels3.catapultsLevel.ToString("N", (IFormatProvider) nfi);
      this.clothesLabel.Text = levels4.clothesLevel.ToString("N", (IFormatProvider) nfi);
      this.furnitureLabel.Text = levels4.furnitureLevel.ToString("N", (IFormatProvider) nfi);
      this.saltLabel.Text = levels4.saltLevel.ToString("N", (IFormatProvider) nfi);
      this.wineLabel.Text = levels4.wineLevel.ToString("N", (IFormatProvider) nfi);
      this.venisonLabel.Text = levels4.venisonLevel.ToString("N", (IFormatProvider) nfi);
      this.spicesLabel.Text = levels4.spicesLevel.ToString("N", (IFormatProvider) nfi);
      this.silkLabel.Text = levels4.silkLevel.ToString("N", (IFormatProvider) nfi);
      this.metalwareLabel.Text = levels4.metalwareLevel.ToString("N", (IFormatProvider) nfi);
      this.stockpileLimitLabel.Text = "(" + this.getCap(6).ToString("N", (IFormatProvider) nfi) + ")";
      this.innLimitLabel.Text = "(" + this.getCap(12).ToString("N", (IFormatProvider) nfi) + ")";
      this.granaryLimitLabel.Text = "(" + this.getCap(13).ToString("N", (IFormatProvider) nfi) + ")";
      this.armouryLimitLabel.Text = "(" + this.getCap(29).ToString("N", (IFormatProvider) nfi) + ")";
      this.hallLimitLabel.Text = "(" + this.getCap(23).ToString("N", (IFormatProvider) nfi) + ")";
      if (this.selectedResource >= 0)
      {
        int resourceLevel = (int) village.getResourceLevel(this.selectedResource);
        this.selectedHeadingLabel.Text = VillageBuildingsData.getResourceNames(this.selectedResource) + ": " + resourceLevel.ToString("N", (IFormatProvider) nfi);
        this.dailyProductionValueLabel.Text = ((int) village.getResourceProductionPerDay(this.selectedResource)).ToString("N", (IFormatProvider) nfi);
        this.totalBuildingsValueLabel.Text = village.numBuildingsOfType(this.selectedResource).ToString("N", (IFormatProvider) nfi);
        this.workingBuildingsValueLabel.Text = village.numWorkingBuildingsOfType(this.selectedResource).ToString("N", (IFormatProvider) nfi);
      }
      this.cardbar.update();
    }

    private int getCap(int resourceType)
    {
      return (int) (GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, resourceType, false) * CardTypes.getResourceCapMultiplier(resourceType, GameEngine.Instance.cardsManager.UserCardData));
    }

    private void resourceClicked()
    {
      if (this.ClickedControl == null)
        return;
      this.selectedResource = this.ClickedControl.Data;
      switch (this.selectedResource)
      {
        case 6:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_wood");
          break;
        case 7:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_stone");
          break;
        case 8:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_iron");
          break;
        case 9:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_pitch");
          break;
        case 12:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_ale");
          break;
        case 13:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_apples");
          break;
        case 14:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_bread");
          break;
        case 15:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_veg");
          break;
        case 16:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_meat");
          break;
        case 17:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_cheese");
          break;
        case 18:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_fish");
          break;
        case 19:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_clothes");
          break;
        case 21:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_furniture");
          break;
        case 22:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_venison");
          break;
        case 23:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_salt");
          break;
        case 24:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_spices");
          break;
        case 25:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_silk");
          break;
        case 26:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_metalware");
          break;
        case 28:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_pikes");
          break;
        case 29:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_bows");
          break;
        case 30:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_swords");
          break;
        case 31:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_armour");
          break;
        case 32:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_catapult");
          break;
        case 33:
          GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_wine");
          break;
      }
      this.selectedHeadingLabel.Visible = true;
      this.selectedImage.Image = (Image) GFXLibrary.getCommodity64DSImage(this.selectedResource);
      this.selectedImage.Visible = true;
      this.dailyProductionHeadingLabel.Visible = true;
      this.dailyProductionValueLabel.Visible = true;
      this.totalBuildingsHeadingLabel.Visible = true;
      this.totalBuildingsValueLabel.Visible = true;
      this.workingBuildingsHeadingLabel.Visible = true;
      this.workingBuildingsValueLabel.Visible = true;
      this.update();
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
      this.Name = nameof (ResourcesPanel2);
      this.MaximumSize = new Size(992, 566);
      this.MinimumSize = new Size(992, 566);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }
  }
}
