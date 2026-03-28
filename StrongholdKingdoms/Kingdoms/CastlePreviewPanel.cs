// Decompiled with JetBrains decompiler
// Type: Kingdoms.CastlePreviewPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class CastlePreviewPanel : BasePreviewPanel
  {
    protected override void previewClick()
    {
      if (this.m_preset == null)
        return;
      if (GameEngine.Instance.GameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_DEFAULT)
      {
        GameEngine.Instance.InitCastlePreview(this.m_preset);
      }
      else
      {
        GameEngine.Instance.villageTabChange(1);
        InterfaceMgr.Instance.initCastleTab();
      }
    }

    private int correctPlacementType(int placementType)
    {
      int num = placementType;
      switch (num)
      {
        case 65:
          num = 34;
          break;
        case 66:
          num = 33;
          break;
      }
      return num;
    }

    protected override void populateRequirements()
    {
      CustomSelfDrawPanel.CSDLabel control = new CustomSelfDrawPanel.CSDLabel();
      control.Text = SK.Text("Preset_Requirements", "Requirements");
      control.Color = ARGBColors.Black;
      control.Size = new Size(this.Width, 30);
      control.Position = new Point(0, this.Height / 6);
      control.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      control.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.addControl((CustomSelfDrawPanel.CSDControl) control);
      this.populateResearch();
      this.populateResources();
    }

    private void populateResearch()
    {
      CustomSelfDrawPanel.CSDExtendingPanel control1 = new CustomSelfDrawPanel.CSDExtendingPanel();
      control1.Size = new Size(this.Width / 3 - 10, this.Height * 2 / 3 - 5);
      control1.Position = new Point(this.Width * 2 / 3 + 5, this.Height / 3);
      control1.Create((Image) GFXLibrary.quest_9sclice_grey_inset_top_left, (Image) GFXLibrary.quest_9sclice_grey_inset_top_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_top_right, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_left, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_right, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_left, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_right);
      this.addControl((CustomSelfDrawPanel.CSDControl) control1);
      CustomSelfDrawPanel.CSDLabel control2 = new CustomSelfDrawPanel.CSDLabel();
      control2.Text = SK.Text("GENERIC_Research", "Research");
      control2.Color = ARGBColors.Black;
      control2.Size = new Size(control1.Width, 20);
      control2.Position = new Point(control1.X, control1.Y - control2.Height);
      control2.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.addControl((CustomSelfDrawPanel.CSDControl) control2);
      ResearchData forCurrentVillage = GameEngine.Instance.World.GetResearchDataForCurrentVillage();
      bool flag1 = this.m_preset.GetFortificationResearchLevel() <= (int) forCurrentVillage.Research_Fortification;
      bool flag2 = this.m_preset.GetDefenceResearchLevel() <= (int) forCurrentVillage.Research_Defences;
      CustomSelfDrawPanel.CSDImage control3 = new CustomSelfDrawPanel.CSDImage();
      control3.Image = (Image) (flag1 ? GFXLibrary.preset_req_fortification : GFXLibrary.preset_req_fortification_red);
      control3.setSizeToImage();
      control3.Position = new Point(control1.Width / 2 - control3.Width / 2, control1.Height / 6 - control3.Height / 2);
      control3.CustomTooltipID = 231;
      control3.CustomTooltipData = flag1 ? 1 : 0;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control3);
      CustomSelfDrawPanel.CSDLabel control4 = new CustomSelfDrawPanel.CSDLabel();
      control4.Text = this.m_preset.GetFortificationResearchLevel().ToString();
      control4.Color = ARGBColors.White;
      control4.Position = control3.Position;
      control4.Size = control3.Size;
      control4.Font = FontManager.GetFont("Arial", 30f, FontStyle.Bold);
      control4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control4);
      CustomSelfDrawPanel.CSDImage control5 = new CustomSelfDrawPanel.CSDImage();
      control5.Image = (Image) (flag2 ? GFXLibrary.preset_req_defence : GFXLibrary.preset_req_defence_red);
      control5.setSizeToImage();
      control5.Position = new Point(control1.Width / 2 - control5.Width / 2, control1.Height / 2 - control5.Height / 2);
      control5.CustomTooltipID = 230;
      control5.CustomTooltipData = flag2 ? 1 : 0;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control5);
      CustomSelfDrawPanel.CSDLabel control6 = new CustomSelfDrawPanel.CSDLabel();
      control6.Text = this.m_preset.GetDefenceResearchLevel().ToString();
      control6.Color = ARGBColors.White;
      control6.Position = control5.Position;
      control6.Size = control5.Size;
      control6.Font = FontManager.GetFont("Arial", 30f, FontStyle.Bold);
      control6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control6);
      if (!this.m_preset.RequiresParishBuilding())
        return;
      bool flag3 = this.m_preset.ParishRequirementsMet();
      CustomSelfDrawPanel.CSDImage control7 = new CustomSelfDrawPanel.CSDImage();
      control7.Image = (Image) (flag3 ? GFXLibrary.preset_req_capital : GFXLibrary.preset_req_capital_red);
      control7.setSizeToImage();
      control7.Position = new Point(control1.Width / 2 - control7.Width / 2, control1.Height * 5 / 6 - control7.Height / 2);
      control7.CustomTooltipID = 232;
      control7.CustomTooltipData = flag3 ? 1 : 0;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control7);
    }

    private void populateResources()
    {
      CustomSelfDrawPanel.CSDExtendingPanel control1 = new CustomSelfDrawPanel.CSDExtendingPanel();
      control1.Size = new Size(this.Width * 2 / 3 - 10, this.Height * 2 / 3 - 5);
      control1.Position = new Point(5, this.Height / 3);
      control1.Create((Image) GFXLibrary.quest_9sclice_grey_inset_top_left, (Image) GFXLibrary.quest_9sclice_grey_inset_top_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_top_right, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_left, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_right, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_left, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_right);
      this.addControl((CustomSelfDrawPanel.CSDControl) control1);
      CustomSelfDrawPanel.CSDLabel control2 = new CustomSelfDrawPanel.CSDLabel();
      control2.Text = SK.Text("GENERIC_Resources", "Resources");
      control2.Color = ARGBColors.Black;
      control2.Size = new Size(control1.Width, 20);
      control2.Position = new Point(control1.X, control1.Y - control2.Height);
      control2.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.addControl((CustomSelfDrawPanel.CSDControl) control2);
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      bool isCapital = GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.getSelectedMenuVillage());
      CardData cardData = new CardData();
      if (!isCapital)
        cardData = GameEngine.Instance.cardsManager.UserCardData;
      double num6 = 0.0;
      foreach (CastleMapPreset.CastleElementInfo castleElementInfo in this.m_preset.BasicData)
      {
        if (castleElementInfo.elementType <= (byte) 69 && castleElementInfo.elementType != (byte) 43)
        {
          int elementType = (int) castleElementInfo.elementType;
          if (elementType == 65)
            elementType = 34;
          if (elementType == 66)
            elementType = 33;
          int woodCost = 0;
          int stoneCost = 0;
          int ironCost = 0;
          int oilCost = 0;
          int goldCost = 0;
          CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, elementType, ref woodCost, ref stoneCost, ref goldCost, ref oilCost, ref ironCost);
          num1 += woodCost;
          num2 += stoneCost;
          num3 += ironCost;
          num4 += oilCost;
          num5 += goldCost;
          num6 += CastlesCommon.calcConstrTime(GameEngine.Instance.LocalWorldData, elementType, (int) GameEngine.Instance.World.GetResearchDataForCurrentVillage().Research_Construction, isCapital, cardData);
        }
      }
      VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
      GameEngine.Instance.Village.getStockpileLevels(levels);
      CustomSelfDrawPanel.CSDImage control3 = new CustomSelfDrawPanel.CSDImage();
      control3.Image = (Image) (levels.woodLevel >= (double) num1 ? GFXLibrary.preset_req_wood : GFXLibrary.preset_req_wood_red);
      control3.setSizeToImage();
      control3.Position = new Point(10, control1.Height / 6 - control3.Height / 2);
      control3.CustomTooltipID = 233;
      control3.CustomTooltipData = levels.woodLevel >= (double) num1 ? 1 : 0;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control3);
      CustomSelfDrawPanel.CSDLabel control4 = new CustomSelfDrawPanel.CSDLabel();
      control4.Text = num1.ToString();
      control4.Color = ARGBColors.White;
      control4.Position = new Point(control3.Rectangle.Right + 5, control3.Y);
      control4.Size = new Size(control1.Width / 2, control3.Height);
      control4.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      control4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control4);
      CustomSelfDrawPanel.CSDImage control5 = new CustomSelfDrawPanel.CSDImage();
      control5.Image = (Image) (levels.stoneLevel >= (double) num2 ? GFXLibrary.preset_req_stone : GFXLibrary.preset_req_stone_red);
      control5.setSizeToImage();
      control5.Position = new Point(control1.Width / 2 + 10, control1.Height / 6 - control5.Height / 2);
      control5.CustomTooltipID = 234;
      control5.CustomTooltipData = levels.stoneLevel >= (double) num2 ? 1 : 0;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control5);
      CustomSelfDrawPanel.CSDLabel control6 = new CustomSelfDrawPanel.CSDLabel();
      control6.Text = num2.ToString();
      control6.Color = ARGBColors.White;
      control6.Position = new Point(control5.Rectangle.Right + 5, control5.Y);
      control6.Size = new Size(control1.Width / 2, control5.Height);
      control6.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      control6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control6);
      CustomSelfDrawPanel.CSDImage control7 = new CustomSelfDrawPanel.CSDImage();
      control7.Image = (Image) (levels.ironLevel >= (double) num3 ? GFXLibrary.preset_req_iron : GFXLibrary.preset_req_iron_red);
      control7.setSizeToImage();
      control7.Position = new Point(10, control1.Height / 2 - control7.Height / 2);
      control7.CustomTooltipID = 235;
      control7.CustomTooltipData = levels.ironLevel >= (double) num3 ? 1 : 0;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control7);
      CustomSelfDrawPanel.CSDLabel control8 = new CustomSelfDrawPanel.CSDLabel();
      control8.Text = num3.ToString();
      control8.Color = ARGBColors.White;
      control8.Position = new Point(control7.Rectangle.Right + 5, control7.Y);
      control8.Size = new Size(control1.Width / 2, control7.Height);
      control8.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      control8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control8);
      CustomSelfDrawPanel.CSDImage control9 = new CustomSelfDrawPanel.CSDImage();
      control9.Image = (Image) (levels.pitchLevel >= (double) num4 ? GFXLibrary.preset_req_pitch : GFXLibrary.preset_req_pitch_red);
      control9.setSizeToImage();
      control9.Position = new Point(control1.Width / 2 + 10, control1.Height / 2 - control9.Height / 2);
      control9.CustomTooltipID = 236;
      control9.CustomTooltipData = levels.pitchLevel >= (double) num4 ? 1 : 0;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control9);
      CustomSelfDrawPanel.CSDLabel control10 = new CustomSelfDrawPanel.CSDLabel();
      control10.Text = num4.ToString();
      control10.Color = ARGBColors.White;
      control10.Position = new Point(control9.Rectangle.Right + 5, control9.Y);
      control10.Size = new Size(control1.Width / 2, control9.Height);
      control10.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      control10.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control10);
      bool flag = !isCapital ? (double) num5 <= GameEngine.Instance.World.getCurrentGold() : (double) num5 <= GameEngine.Instance.Village.m_capitalGold;
      CustomSelfDrawPanel.CSDImage control11 = new CustomSelfDrawPanel.CSDImage();
      control11.Image = (Image) (flag ? GFXLibrary.preset_req_gold : GFXLibrary.preset_req_gold_red);
      control11.setSizeToImage();
      control11.Position = new Point(10, control1.Height * 5 / 6 - control11.Height / 2);
      control11.CustomTooltipID = 237;
      control11.CustomTooltipData = flag ? 1 : 0;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control11);
      CustomSelfDrawPanel.CSDLabel control12 = new CustomSelfDrawPanel.CSDLabel();
      control12.Text = num5.ToString();
      control12.Color = ARGBColors.White;
      control12.Position = new Point(control11.Rectangle.Right + 5, control11.Y);
      control12.Size = new Size(control1.Width / 2, control11.Height);
      control12.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      control12.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control12);
      CustomSelfDrawPanel.CSDImage control13 = new CustomSelfDrawPanel.CSDImage();
      control13.Image = (Image) GFXLibrary.preset_req_time;
      control13.setSizeToImage();
      control13.Position = new Point(control1.Width / 2 + 10, control1.Height * 5 / 6 - control13.Height / 2);
      control13.CustomTooltipID = 238;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control13);
      CustomSelfDrawPanel.CSDLabel control14 = new CustomSelfDrawPanel.CSDLabel();
      control14.Text = VillageMap.createBuildTimeString((int) (num6 * 3600.0));
      control14.Color = ARGBColors.White;
      control14.Position = new Point(control13.Rectangle.Right + 5, control13.Y);
      control14.Size = new Size(control1.Width / 2, control13.Height);
      control14.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      control14.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control14);
    }
  }
}
