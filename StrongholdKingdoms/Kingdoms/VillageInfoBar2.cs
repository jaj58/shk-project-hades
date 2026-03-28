// Decompiled with JetBrains decompiler
// Type: Kingdoms.VillageInfoBar2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Drawing;
using System.Globalization;

//#nullable disable
namespace Kingdoms
{
  public class VillageInfoBar2 : CustomSelfDrawPanel.CSDControl
  {
    private CustomSelfDrawPanel.CSDLabel lblWoodName = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblWoodLevel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblStoneLevel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblFoodLevel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblFoodName = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblPeople = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblHeading = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage imgWood = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage imgStone = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage imgFood = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage imgBed = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage imgPeople = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage imgGold = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage imgFlag = new CustomSelfDrawPanel.CSDImage();
    private bool lastStockpile = true;
    private bool lastGranary = true;
    private bool lastViewOnly;

    public void init()
    {
      this.clearControls();
      this.lblWoodName.Text = SK.Text("VillageInfoBar_No_Stockpile", "No Stockpile");
      this.lblWoodName.Position = new Point(3, 0);
      this.lblWoodName.Size = new Size(250, 29);
      this.lblWoodName.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblWoodName.Color = ARGBColors.Yellow;
      this.lblWoodName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblWoodName.CustomTooltipID = 142;
      this.lblWoodName.Visible = false;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblWoodName);
      this.lblWoodLevel.Text = "";
      this.lblWoodLevel.Position = new Point(44, 3);
      this.lblWoodLevel.Size = new Size(75, 29);
      this.lblWoodLevel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblWoodLevel.Color = ARGBColors.Yellow;
      this.lblWoodLevel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblWoodLevel.CustomTooltipID = 142;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblWoodLevel);
      this.lblStoneLevel.Text = "";
      this.lblStoneLevel.Position = new Point(165, 3);
      this.lblStoneLevel.Size = new Size(75, 29);
      this.lblStoneLevel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblStoneLevel.Color = ARGBColors.Yellow;
      this.lblStoneLevel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblStoneLevel.CustomTooltipID = 143;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblStoneLevel);
      this.lblFoodLevel.Text = "";
      this.lblFoodLevel.Position = new Point(286, 3);
      this.lblFoodLevel.Size = new Size(78, 29);
      this.lblFoodLevel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblFoodLevel.Color = ARGBColors.Yellow;
      this.lblFoodLevel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblFoodLevel.CustomTooltipID = 144;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblFoodLevel);
      this.lblFoodName.Text = SK.Text("VillageInfoBar_No_Granary", "No Granary");
      this.lblFoodName.Position = new Point(269, 3);
      this.lblFoodName.Size = new Size(206, 29);
      this.lblFoodName.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblFoodName.Color = ARGBColors.Yellow;
      this.lblFoodName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblFoodName.CustomTooltipID = 142;
      this.lblFoodName.Visible = false;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblFoodName);
      this.lblPeople.Text = "";
      this.lblPeople.Position = new Point(418, 3);
      this.lblPeople.Size = new Size(51, 29);
      this.lblPeople.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblPeople.Color = ARGBColors.Yellow;
      this.lblPeople.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblPeople.CustomTooltipID = 141;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblPeople);
      this.lblPeasants.Text = "";
      this.lblPeasants.Position = new Point(503, 3);
      this.lblPeasants.Size = new Size(27, 29);
      this.lblPeasants.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblPeasants.Color = ARGBColors.Yellow;
      this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblPeasants.CustomTooltipID = 141;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblPeasants);
      this.lblHeading.Text = "";
      this.lblHeading.Position = new Point(0, 2);
      this.lblHeading.Size = new Size(500, 29);
      this.lblHeading.Font = FontManager.GetFont("Microsoft Sans Serif", 18f);
      this.lblHeading.Color = Color.FromArgb(224, 203, 146);
      this.lblHeading.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lblHeading.Visible = false;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblHeading);
      this.imgWood.Image = (Image) GFXLibrary.com_32_wood;
      this.imgWood.Position = new Point(7, 0);
      this.imgWood.CustomTooltipID = 142;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.imgWood);
      this.imgStone.Image = (Image) GFXLibrary.com_32_stone;
      this.imgStone.Position = new Point(128, 0);
      this.imgStone.CustomTooltipID = 143;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.imgStone);
      this.imgFood.Image = (Image) GFXLibrary.com_32_food;
      this.imgFood.Position = new Point(249, 0);
      this.imgFood.CustomTooltipID = 144;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.imgFood);
      this.imgBed.Image = (Image) GFXLibrary.population_bed;
      this.imgBed.Position = new Point(370, 0);
      this.imgBed.CustomTooltipID = 141;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.imgBed);
      this.imgPeople.Image = (Image) GFXLibrary.population_head;
      this.imgPeople.Position = new Point(469, 0);
      this.imgPeople.CustomTooltipID = 141;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.imgPeople);
      this.imgGold.Image = (Image) GFXLibrary.com_32_money;
      this.imgGold.Position = new Point(7, 0);
      this.imgGold.CustomTooltipID = 145;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.imgGold);
      this.imgFlag.Image = (Image) GFXLibrary.flag_blue_icon;
      this.imgFlag.Position = new Point(128, 8);
      this.imgFlag.CustomTooltipID = 146;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.imgFlag);
    }

    public void setHeading(string text)
    {
      this.lblWoodName.Visible = false;
      this.lblFoodLevel.Visible = false;
      this.lblFoodName.Visible = false;
      this.imgFood.Visible = false;
      this.imgWood.Visible = false;
      this.imgStone.Visible = false;
      this.lblPeasants.Visible = false;
      this.lblPeople.Visible = false;
      this.imgPeople.Visible = false;
      this.imgBed.Visible = false;
      this.lblStoneLevel.Visible = false;
      this.lblWoodLevel.Visible = false;
      this.imgGold.Visible = false;
      this.lblFoodName.Visible = false;
      this.imgFlag.Visible = false;
      this.lblHeading.Text = text;
      this.lblHeading.Visible = true;
    }

    public void removeHeading() => this.lblHeading.Visible = false;

    public void setDisplayedLevels(
      int woodLevel,
      int stoneLevel,
      int foodLevel,
      bool gotStockpile,
      bool gotGranary,
      int totalPeople,
      int housingCapacity,
      int spareWorkers,
      bool viewOnly,
      int capitalGold,
      int villageID,
      int numFlags)
    {
      if (this.lblHeading.Visible)
        return;
      NumberFormatInfo nfi = GameEngine.NFI;
      if (GameEngine.Instance.World.isCapital(villageID))
      {
        this.lblWoodLevel.Visible = true;
        this.lblStoneLevel.Visible = true;
        this.lblWoodName.Visible = false;
        this.lblFoodLevel.Visible = false;
        this.lblFoodName.Visible = false;
        this.imgFood.Visible = false;
        this.imgWood.Visible = false;
        this.imgStone.Visible = false;
        this.lblPeasants.Visible = false;
        this.lblPeople.Visible = false;
        this.imgPeople.Visible = false;
        this.imgBed.Visible = false;
        this.imgGold.Visible = true;
        this.imgFlag.Visible = true;
        this.lblWoodLevel.TextDiffOnly = capitalGold.ToString("N", (IFormatProvider) nfi);
        this.lblStoneLevel.TextDiffOnly = numFlags.ToString("N", (IFormatProvider) nfi);
        this.lblWoodLevel.CustomTooltipID = 145;
        this.lblStoneLevel.CustomTooltipID = 146;
      }
      else
      {
        this.imgGold.Visible = false;
        this.lblPeasants.Visible = true;
        this.lblPeople.Visible = true;
        this.lblWoodLevel.Visible = true;
        this.lblStoneLevel.Visible = true;
        this.lblWoodName.Visible = false;
        this.lblFoodLevel.Visible = true;
        this.imgFood.Visible = true;
        this.imgWood.Visible = true;
        this.imgStone.Visible = true;
        this.imgPeople.Visible = true;
        this.imgBed.Visible = true;
        this.imgFlag.Visible = false;
        this.lblWoodLevel.CustomTooltipID = 142;
        this.lblStoneLevel.CustomTooltipID = 143;
        if (!viewOnly)
        {
          if (this.lastViewOnly)
          {
            this.lastStockpile = !gotStockpile;
            this.lastGranary = !gotGranary;
            this.lastViewOnly = false;
          }
          this.lblWoodLevel.TextDiffOnly = woodLevel.ToString("N", (IFormatProvider) nfi);
          this.lblStoneLevel.TextDiffOnly = stoneLevel.ToString("N", (IFormatProvider) nfi);
          this.lblFoodLevel.TextDiffOnly = foodLevel.ToString("N", (IFormatProvider) nfi);
          this.lastStockpile = gotStockpile;
          if (gotStockpile)
          {
            this.lblWoodLevel.Visible = true;
            this.lblStoneLevel.Visible = true;
            this.imgWood.Visible = true;
            this.imgStone.Visible = true;
            this.lblWoodName.Visible = false;
          }
          else
          {
            this.lblWoodLevel.Visible = false;
            this.lblStoneLevel.Visible = false;
            this.imgWood.Visible = false;
            this.imgStone.Visible = false;
            this.lblWoodName.Visible = true;
          }
          this.lastGranary = gotGranary;
          if (gotGranary)
          {
            this.lblFoodLevel.Visible = true;
            this.lblFoodName.Visible = false;
            this.imgFood.Visible = true;
          }
          else
          {
            this.lblFoodLevel.Visible = false;
            this.lblFoodName.Visible = true;
            this.imgFood.Visible = false;
          }
        }
        else
        {
          this.lastViewOnly = true;
          this.lblWoodLevel.Visible = false;
          this.lblStoneLevel.Visible = false;
          this.lblWoodName.Visible = false;
          this.lblFoodLevel.Visible = false;
          this.lblFoodName.Visible = false;
          this.imgFood.Visible = false;
          this.imgWood.Visible = false;
          this.imgStone.Visible = false;
        }
        this.lblPeople.TextDiffOnly = totalPeople.ToString() + "/" + housingCapacity.ToString();
        this.lblPeasants.TextDiffOnly = spareWorkers.ToString();
      }
    }

    public bool isVisible() => this.Visible;

    public void show()
    {
      this.Visible = true;
      this.invalidate();
    }

    public void hide()
    {
      this.Visible = false;
      this.invalidate();
    }
  }
}
