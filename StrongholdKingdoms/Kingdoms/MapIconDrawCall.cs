// Decompiled with JetBrains decompiler
// Type: Kingdoms.MapIconDrawCall
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class MapIconDrawCall
  {
    private bool mapEditing;
    private int village_y = 1;
    private double worldZoom;
    private double worldScale = 1.0;
    private GraphicsMgr gfx;
    private SpriteWrapper villageSprite;
    private Size screenSize;
    private int pulse;
    private int pulseValue;
    private bool xmasPresents;

    public MapIconDrawCall(
      GraphicsMgr graphicsManager,
      SpriteWrapper VillageSprite,
      double WorldZoom,
      double WorldScale,
      bool MapEditing,
      Size ScreenSize,
      int Pulse,
      int PulseValue,
      bool XmasPresents)
    {
      this.worldZoom = WorldZoom;
      this.worldScale = WorldScale;
      this.mapEditing = MapEditing;
      this.gfx = graphicsManager;
      this.villageSprite = VillageSprite;
      this.pulse = Pulse;
      this.pulseValue = PulseValue;
      this.xmasPresents = XmasPresents;
      this.screenSize = ScreenSize;
    }

    private int iScale(int i, float scale) => (int) ((double) i * (double) scale);

    public int getHouseIDFromVillage(VillageData village)
    {
      return village.userID < 0 ? 21 : GameEngine.Instance.World.getHouse(village.factionID);
    }

    private Color getColorFromArray(Color[] carray, int index)
    {
      return carray != null && index >= 0 && index < carray.Length ? carray[index] : ARGBColors.White;
    }

    public int fixupVillageSprites(int colourID)
    {
      switch (colourID)
      {
        case 0:
          colourID = 7;
          break;
        case 7:
          colourID = 0;
          break;
      }
      return colourID;
    }

    private void setSpriteToCountryCapital(
      ref bool drawSprite,
      ref int spriteID,
      VillageData village,
      ref Color villageColoriser,
      ref bool capitalVillage,
      ref float scale,
      ref string capitalName)
    {
      drawSprite = true;
      int houseIdFromVillage = this.getHouseIDFromVillage(village);
      villageColoriser = WorldMap.getVillageColor(houseIdFromVillage);
      capitalVillage = true;
      scale = (float) this.worldScale / 8.5f;
      spriteID = 58;
      if ((double) scale < 0.15000000596046448)
        scale = 0.15f;
      if ((double) scale > 1.0)
        scale = 1f;
      if (this.worldScale <= 3.0)
        return;
      capitalName = village.villageName;
    }

    private void setSpriteToProvinceCapital(
      ref bool drawSprite,
      ref int spriteID,
      VillageData village,
      ref Color villageColoriser,
      ref bool capitalVillage,
      ref float scale,
      ref string capitalName)
    {
      drawSprite = true;
      int houseIdFromVillage = this.getHouseIDFromVillage(village);
      villageColoriser = WorldMap.getVillageColor(houseIdFromVillage);
      capitalVillage = true;
      scale = (float) this.worldScale / 8.5f;
      spriteID = 57;
      if ((double) scale < 0.15000000596046448)
        scale = 0.15f;
      if ((double) scale > 1.0)
        scale = 1f;
      if (this.worldScale <= 3.0)
        return;
      capitalName = village.villageName;
    }

    private void setSpriteToCountyCapital(
      ref bool drawSprite,
      ref int spriteID,
      VillageData village,
      ref Color villageColoriser,
      ref bool capitalVillage,
      ref float scale,
      ref string capitalName)
    {
      drawSprite = true;
      int houseIdFromVillage = this.getHouseIDFromVillage(village);
      villageColoriser = WorldMap.getVillageColor(houseIdFromVillage);
      capitalVillage = true;
      scale = (float) this.worldScale / 11.333333f;
      spriteID = 56;
      if ((double) scale < 0.10000000149011612)
        scale = 0.1f;
      if ((double) scale > 1.0)
        scale = 1f;
      if (this.worldScale <= 3.0)
        return;
      capitalName = village.villageName;
    }

    private void setSpriteToParishCapital(
      ref bool drawSprite,
      ref int spriteID,
      VillageData village,
      ref Color villageColoriser,
      ref bool capitalVillage,
      ref float scale,
      ref string capitalName)
    {
      drawSprite = true;
      int houseIdFromVillage = this.getHouseIDFromVillage(village);
      villageColoriser = WorldMap.getVillageColor(houseIdFromVillage);
      capitalVillage = true;
      scale = (float) this.worldScale / 17f;
      spriteID = 55;
      if ((double) scale < 0.10000000149011612)
        scale = 0.1f;
      if ((double) scale > 1.0)
        scale = 1f;
      if (this.worldScale <= 5.0)
        return;
      capitalName = village.villageName;
    }

    private void setSpriteToInvasionMarkerAndDraw(
      ref bool allowSurroundDraw,
      ref bool drawSprite,
      ref float scale,
      VillageData village,
      ref int spriteID,
      double xPos,
      double yPos,
      ref Color villageColoriser)
    {
      allowSurroundDraw = false;
      drawSprite = true;
      scale = this.worldScale <= 0.67 ? (float) (this.worldScale / 0.66) : 1f;
      switch (GameEngine.Instance.World.getAIInvasionMarkerState(village.id))
      {
        case 0:
          spriteID = 418;
          break;
        case 2:
          this.gfx.beginSprites();
          this.villageSprite.PosX = (float) xPos * (float) this.screenSize.Width;
          this.villageSprite.PosY = (float) yPos * (float) this.screenSize.Height;
          int pulseValue = this.pulseValue;
          this.villageSprite.Scale = scale;
          this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
          this.villageSprite.SpriteNo = 420;
          this.villageSprite.ColorToUse = Color.FromArgb(pulseValue, ARGBColors.Yellow);
          this.villageSprite.Update();
          this.villageSprite.DrawAndClear_NoCenter();
          this.villageSprite.ColorToUse = ARGBColors.White;
          goto default;
        default:
          spriteID = 419;
          break;
      }
      villageColoriser = ARGBColors.White;
    }

    private void drawMapIconSelectedTint(float origScale, VillageData village, bool newVillage)
    {
      this.villageSprite.Scale = origScale;
      this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
      this.villageSprite.SpriteNo = InterfaceMgr.Instance.OwnSelectedVillage == village.id || InterfaceMgr.Instance.SelectedVassalVillage == village.id ? 34 : 30;
      if (newVillage)
      {
        this.villageSprite.SpriteNo += 2;
        this.villageSprite.Center = new PointF(44f, 47f);
      }
      else
      {
        if (village.regionCapital)
          ++this.villageSprite.SpriteNo;
        if (village.countyCapital)
          this.villageSprite.SpriteNo += 2;
        if (village.provinceCapital)
          this.villageSprite.SpriteNo += 3;
        if (village.countryCapital)
          this.villageSprite.SpriteNo += 3;
      }
      this.villageSprite.Update();
      this.villageSprite.DrawAndClear();
    }

    private void drawMapIconSurround(
      ref Color surroundColoriser,
      Color villageColoriser,
      float origScale,
      bool aiWorldSpecial,
      VillageData village,
      int spriteID,
      bool showSurround,
      int NORMAL_OFFSET_30,
      ref Color buildingsColoriser,
      int NORMAL_OFFSET_110)
    {
      surroundColoriser = Color.FromArgb(192, villageColoriser);
      if (villageColoriser == ARGBColors.White)
        surroundColoriser = Color.FromArgb(128, villageColoriser);
      this.villageSprite.Scale = origScale;
      int num1 = 0;
      int num2 = 35;
      if (aiWorldSpecial)
        num1 = Math.Min((int) village.villageInfo * 3 + 4, 19);
      else if (village.special == 0)
        num1 = Math.Min((int) village.villageInfo / 6, 19);
      else
        num2 = spriteID;
      if (village.special == 0 || aiWorldSpecial)
      {
        if (showSurround)
        {
          this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
          this.villageSprite.SpriteNo = num2 + NORMAL_OFFSET_30 + num1;
          this.villageSprite.ColorToUse = surroundColoriser;
          this.villageSprite.Update();
          this.villageSprite.DrawAndClear_NoCenter();
        }
        else if (aiWorldSpecial)
        {
          surroundColoriser = Color.FromArgb((int) byte.MaxValue, 41, 41, 48);
          this.villageSprite.Scale = origScale;
          this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
          this.villageSprite.SpriteNo = num2 + NORMAL_OFFSET_30 + num1;
          this.villageSprite.ColorToUse = surroundColoriser;
          this.villageSprite.Update();
          this.villageSprite.DrawAndClear_NoCenter();
          this.villageSprite.ColorToUse = ARGBColors.White;
          buildingsColoriser = Color.FromArgb((int) byte.MaxValue, 228, 228);
          surroundColoriser = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0, 0);
          this.villageSprite.Scale = origScale;
          this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
          this.villageSprite.SpriteNo = num2 == 63 || num2 == 64 ? num2 + NORMAL_OFFSET_110 + num1 + 1 : num2 + NORMAL_OFFSET_110 + num1;
          this.villageSprite.ColorToUse = surroundColoriser;
          this.villageSprite.Update();
          this.villageSprite.DrawAndClear_NoCenter();
          this.villageSprite.ColorToUse = ARGBColors.White;
          buildingsColoriser = Color.FromArgb((int) byte.MaxValue, 228, 228);
        }
        this.villageSprite.ColorToUse = ARGBColors.White;
      }
      if (InterfaceMgr.Instance.OwnSelectedVillage == village.id || InterfaceMgr.Instance.SelectedVassalVillage == village.id)
      {
        int pulseValue = this.pulseValue;
        surroundColoriser = Color.FromArgb(pulseValue, ARGBColors.Yellow);
        this.villageSprite.Scale = origScale;
        this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
        this.villageSprite.SpriteNo = num2 + NORMAL_OFFSET_110 + num1;
        this.villageSprite.ColorToUse = surroundColoriser;
        this.villageSprite.Update();
        this.villageSprite.DrawAndClear_NoCenter();
        this.villageSprite.ColorToUse = ARGBColors.White;
        buildingsColoriser = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192);
      }
      else if (InterfaceMgr.Instance.SelectedVillage == village.id)
      {
        int pulseValue = this.pulseValue;
        surroundColoriser = Color.FromArgb(pulseValue, 64, (int) byte.MaxValue, 64);
        this.villageSprite.Scale = origScale;
        this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
        this.villageSprite.SpriteNo = village.userID >= 0 || village.special != 0 && village.special != 30 ? (num2 == 63 || num2 == 64 ? num2 + NORMAL_OFFSET_110 + num1 + 1 : num2 + NORMAL_OFFSET_110 + num1) : 65;
        this.villageSprite.ColorToUse = surroundColoriser;
        this.villageSprite.Update();
        this.villageSprite.DrawAndClear_NoCenter();
        this.villageSprite.ColorToUse = ARGBColors.White;
        buildingsColoriser = Color.FromArgb(192, (int) byte.MaxValue, 192);
        if (SpecialVillageTypes.IS_ROYAL_TOWER(village.special))
          this.villageSprite.Center = new PointF(43f, 110f);
      }
      this.villageSprite.Scale = origScale;
      if (village.userID >= 0 || village.special != 0)
      {
        this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
        this.villageSprite.SpriteNo = num2 + num1;
      }
      else
      {
        this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
        this.villageSprite.SpriteNo = 401;
      }
      this.villageSprite.ColorToUse = buildingsColoriser;
      this.villageSprite.Update();
      this.villageSprite.DrawAndClear();
    }

    private void drawUnopenedStash(
      int villageTexture,
      int spriteID,
      float origScale,
      VillageData village)
    {
      this.villageSprite.TextureID = villageTexture;
      this.villageSprite.SpriteNo = spriteID;
      this.villageSprite.Scale = origScale;
      this.villageSprite.Update();
      this.villageSprite.DrawAndClear();
    }

    private void drawParishPlagueLevel(
      int villageTexture,
      int plagueLevel,
      float origScale,
      VillageData village)
    {
      this.villageSprite.TextureID = villageTexture;
      this.villageSprite.SpriteNo = plagueLevel <= 133 ? (plagueLevel <= 66 ? 143 : 144) : 176;
      this.villageSprite.Center = new PointF(61f, 61f);
      this.villageSprite.Scale = origScale;
      this.villageSprite.Update();
      this.villageSprite.DrawAndClear();
    }

    private void drawParishFlags(
      int villageTexture,
      float origScale,
      VillageData village,
      float scale)
    {
      this.villageSprite.TextureID = villageTexture;
      this.villageSprite.SpriteNo = 28;
      this.villageSprite.Center = new PointF(26f, 58f);
      this.villageSprite.Scale = origScale;
      this.villageSprite.Update();
      this.villageSprite.DrawAndClear();
      if ((double) scale != 1.0)
        return;
      Color black = ARGBColors.Black;
      if (village.whiteFlags && this.worldScale >= 11.0)
      {
        Color col = Color.FromArgb(240, 240, 240);
        GameEngine.Instance.World.addText(village.numFlags.ToString(), new PointF((float) ((double) this.villageSprite.PosX + 35.0 * (double) origScale + 1.0), (float) ((double) this.villageSprite.PosY + -27.0 * (double) origScale + 1.0)), ARGBColors.Black, false, 1, false);
        GameEngine.Instance.World.addText(village.numFlags.ToString(), new PointF(this.villageSprite.PosX + 35f * origScale, this.villageSprite.PosY + -27f * origScale), col, false, 1, false);
      }
      else
        GameEngine.Instance.World.addText(village.numFlags.ToString(), new PointF(this.villageSprite.PosX + 35f * origScale, this.villageSprite.PosY + -27f * origScale), black, false, 1, false);
    }

    private void drawCapitalName(VillageData village, float origScale, string capitalName)
    {
      if (GameEngine.Instance.World.DrawDebugNames)
      {
        if (village.regionCapital)
          GameEngine.Instance.World.addText("P:" + village.regionID.ToString() + " V:" + village.id.ToString(), new PointF(this.villageSprite.PosX, this.villageSprite.PosY + 30f * origScale), ARGBColors.Black, true, 1, true);
        if (village.countyCapital)
          GameEngine.Instance.World.addText("Cty:" + village.countyID.ToString() + " V:" + village.id.ToString(), new PointF(this.villageSprite.PosX, this.villageSprite.PosY + 30f * origScale), ARGBColors.Black, true, 1, true);
        if (village.provinceCapital)
        {
          int countyProvince = GameEngine.Instance.World.getCountyProvince((int) village.countyID);
          GameEngine.Instance.World.addText("Prv:" + countyProvince.ToString() + " V:" + village.id.ToString(), new PointF(this.villageSprite.PosX, this.villageSprite.PosY + 30f * origScale), ARGBColors.Black, true, 1, true);
        }
        if (!village.countryCapital)
          return;
        int countyProvince1 = GameEngine.Instance.World.getCountyProvince((int) village.countyID);
        int provinceCountry = GameEngine.Instance.World.getProvinceCountry(countyProvince1);
        GameEngine.Instance.World.addText("Ctry:" + provinceCountry.ToString() + " V:" + village.id.ToString(), new PointF(this.villageSprite.PosX, this.villageSprite.PosY + 30f * origScale), ARGBColors.Black, true, 1, true);
      }
      else
      {
        if (capitalName.Length <= 0)
          return;
        Color black = ARGBColors.Black;
        GameEngine.Instance.World.addText(capitalName, new PointF(this.villageSprite.PosX, this.villageSprite.PosY + 40f * origScale), black, true, 1, true);
      }
    }

    private void setSpriteToSpecialType(
      VillageData village,
      ref int resourceSpriteNo,
      ref int spriteID,
      ref float scale,
      ref bool newVillage,
      ref Color villageColoriser,
      ref bool aiWorldSpecial,
      ref int NORMAL_OFFSET_110,
      ref int NORMAL_OFFSET_30)
    {
      if (village.special >= 100 && village.special <= 199)
      {
        resourceSpriteNo = GFXLibrary.getCommodity32GFXno(village.special - 100);
        spriteID = resourceSpriteNo >= 0 ? -1 : (!this.xmasPresents ? 124 : 400);
        scale = 1f;
      }
      else if (village.special == 30)
      {
        scale = 1f;
        spriteID = 59;
        newVillage = true;
        villageColoriser = ARGBColors.White;
      }
      else if (village.special == 3)
      {
        spriteID = 59;
        newVillage = true;
        villageColoriser = ARGBColors.White;
      }
      else if (village.special == 4)
      {
        spriteID = 61;
        newVillage = true;
        villageColoriser = ARGBColors.White;
      }
      else if (village.special == 5)
      {
        spriteID = 60;
        newVillage = true;
        villageColoriser = ARGBColors.White;
      }
      else if (village.special == 6)
      {
        spriteID = 62;
        newVillage = true;
        villageColoriser = ARGBColors.White;
      }
      else if (village.isAICastle)
      {
        if (GameEngine.Instance.LocalWorldData.AIWorld)
        {
          villageColoriser = ARGBColors.Black;
          aiWorldSpecial = true;
          newVillage = true;
        }
        else
        {
          spriteID = 63;
          newVillage = true;
          villageColoriser = ARGBColors.White;
        }
      }
      else if (village.special == 8 || village.special == 10 || village.special == 12 || village.special == 14)
      {
        spriteID = 64;
        newVillage = true;
        villageColoriser = ARGBColors.White;
      }
      else if (village.special == 15 || village.special == 17)
      {
        spriteID = 388;
        newVillage = true;
        villageColoriser = ARGBColors.White;
        NORMAL_OFFSET_110 = -214;
      }
      else if (village.special == 16 || village.special == 18)
      {
        spriteID = 389;
        newVillage = true;
        villageColoriser = ARGBColors.White;
        NORMAL_OFFSET_110 = -214;
      }
      else if (village.special >= 41 && village.special <= 50)
      {
        spriteID = 390;
        newVillage = true;
        villageColoriser = ARGBColors.White;
        NORMAL_OFFSET_110 = -216;
        scale = 1f;
      }
      else if (village.special >= 51 && village.special <= 60)
      {
        spriteID = 392;
        newVillage = true;
        villageColoriser = ARGBColors.White;
        NORMAL_OFFSET_110 = -218;
        scale = 1f;
      }
      else if (village.special >= 61 && village.special <= 70)
      {
        spriteID = 394;
        newVillage = true;
        villageColoriser = ARGBColors.White;
        NORMAL_OFFSET_110 = -220;
        scale = 1f;
      }
      else if (village.special >= 71 && village.special <= 80)
      {
        spriteID = 396;
        newVillage = true;
        villageColoriser = ARGBColors.White;
        NORMAL_OFFSET_110 = -222;
        scale = 1f;
      }
      else if (village.special >= 81 && village.special <= 90)
      {
        spriteID = 398;
        newVillage = true;
        villageColoriser = ARGBColors.White;
        NORMAL_OFFSET_110 = -224;
        scale = 1f;
      }
      else if (village.special == 40)
      {
        spriteID = 391;
        newVillage = true;
        villageColoriser = ARGBColors.White;
        NORMAL_OFFSET_110 = -216;
      }
      else if (SpecialVillageTypes.IS_ROYAL_TOWER(village.special))
      {
        newVillage = true;
        villageColoriser = ARGBColors.White;
        switch (village.special)
        {
          case 200:
            spriteID = 433;
            NORMAL_OFFSET_110 = 454 - spriteID;
            break;
          case 201:
          case 202:
          case 203:
          case 204:
          case 205:
          case 206:
          case 207:
          case 208:
          case 209:
          case 210:
          case 211:
          case 212:
          case 213:
          case 214:
          case 215:
          case 216:
          case 217:
          case 218:
          case 219:
          case 220:
            spriteID = 433 + (village.special - 200);
            NORMAL_OFFSET_110 = 455 - spriteID;
            break;
        }
        scale = 1f;
      }
      else if (village.special == 21)
      {
        spriteID = 376;
        newVillage = true;
        villageColoriser = ARGBColors.White;
        NORMAL_OFFSET_30 = 2;
        NORMAL_OFFSET_110 = 4;
      }
      else
      {
        if (village.special != 2)
          return;
        village.visible = false;
      }
    }

    private void drawHouseColourSurroundOnCapital(
      int villageTexture,
      int spriteID,
      int NORMAL_OFFSET_30,
      Color surroundColoriser,
      float origScale,
      VillageData village)
    {
      this.villageSprite.TextureID = villageTexture;
      this.villageSprite.SpriteNo = spriteID + NORMAL_OFFSET_30;
      this.villageSprite.ColorToUse = surroundColoriser;
      this.villageSprite.Center = new PointF(75f, 105f);
      this.villageSprite.Scale = origScale;
      this.villageSprite.Update();
      this.villageSprite.DrawAndClear_NoCenter();
    }

    private void drawPulsingGlowOnCurrentPlayerControlledCapital(
      ref Color surroundColoriser,
      int villageTexture,
      int spriteID,
      int NORMAL_OFFSET_110,
      float origScale,
      VillageData village,
      ref Color buildingsColoriser)
    {
      int pulseValue = this.pulseValue;
      surroundColoriser = Color.FromArgb(pulseValue, ARGBColors.Yellow);
      this.villageSprite.TextureID = villageTexture;
      this.villageSprite.SpriteNo = spriteID + NORMAL_OFFSET_110;
      this.villageSprite.ColorToUse = surroundColoriser;
      this.villageSprite.Center = new PointF(75f, 105f);
      this.villageSprite.Scale = origScale;
      this.villageSprite.Update();
      this.villageSprite.DrawAndClear_NoCenter();
      buildingsColoriser = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192);
    }

    private void drawPulsingGlowOnTargetedCapital(
      ref Color surroundColoriser,
      int villageTexture,
      int spriteID,
      int NORMAL_OFFSET_110,
      float origScale,
      VillageData village,
      ref Color buildingsColoriser)
    {
      int pulseValue = this.pulseValue;
      surroundColoriser = Color.FromArgb(pulseValue, 64, (int) byte.MaxValue, 64);
      this.villageSprite.TextureID = villageTexture;
      this.villageSprite.SpriteNo = spriteID + NORMAL_OFFSET_110;
      this.villageSprite.ColorToUse = surroundColoriser;
      this.villageSprite.Center = new PointF(75f, 105f);
      this.villageSprite.Scale = origScale;
      this.villageSprite.Update();
      this.villageSprite.DrawAndClear_NoCenter();
      buildingsColoriser = Color.FromArgb(192, (int) byte.MaxValue, 192);
    }

    private void drawCapitalSprite(
      int villageTexture,
      int spriteID,
      Color buildingsColoriser,
      float origScale,
      VillageData village)
    {
      this.villageSprite.TextureID = villageTexture;
      this.villageSprite.SpriteNo = spriteID;
      this.villageSprite.ColorToUse = buildingsColoriser;
      this.villageSprite.Center = new PointF(75f, 105f);
      this.villageSprite.Scale = origScale;
      this.villageSprite.Update();
      this.villageSprite.DrawAndClear();
    }

    private void drawResourceStash(int resourceSpriteNo, float origScale, VillageData village)
    {
      this.gfx.beginSprites();
      this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
      this.villageSprite.SpriteNo = resourceSpriteNo + 95;
      this.villageSprite.Scale = origScale;
      this.villageSprite.Update();
      this.villageSprite.DrawAndClear();
    }

    private bool checkVillageForShieldRollover(
      VillageData rolloverTargetVillage,
      VillageData village,
      ref int shieldXOff,
      ref int shieldYOff,
      ref int shieldTypeID)
    {
      bool flag = false;
      if (rolloverTargetVillage.userID >= 0 && rolloverTargetVillage.userID == village.userID)
        flag = true;
      else if (!GameEngine.Instance.LocalWorldData.AIWorld)
      {
        if (rolloverTargetVillage.isAICastle && village.isAICastle)
        {
          flag = true;
          shieldXOff = -1;
          shieldYOff = -4;
          if (village.special == 7)
            shieldTypeID = -1;
          if (village.special == 9)
            shieldTypeID = -2;
          if (village.special == 11)
            shieldTypeID = -3;
          if (village.special == 13)
            shieldTypeID = -4;
        }
      }
      else if (rolloverTargetVillage.isAICastle && village.special == rolloverTargetVillage.special)
      {
        flag = true;
        shieldXOff = -1;
        shieldYOff = -4;
        if (village.special == 7)
          shieldTypeID = -1;
        if (village.special == 9)
          shieldTypeID = -2;
        if (village.special == 11)
          shieldTypeID = -3;
        if (village.special == 13)
          shieldTypeID = -4;
      }
      return flag;
    }

    public bool DrawShields(VillageData village, double xPos, double yPos, float scale)
    {
      bool flag1 = false;
      bool flag2 = false;
      bool force = false;
      int shieldXOff = 0;
      int shieldYOff = 0;
      int shieldTypeID = village.userID;
      if (shieldTypeID < 0)
        shieldTypeID = -10000;
      if (GameEngine.Instance.World.isUserVillage(village.id))
      {
        flag1 = true;
        force = true;
        flag2 = this.worldScale >= 7.0;
      }
      else if (GameEngine.Instance.World.isUserRelatedVillage(village.id))
      {
        flag1 = true;
        force = true;
        flag2 = this.worldScale >= 7.0;
      }
      else if (village.Capital && this.worldScale >= 7.0)
      {
        flag1 = true;
        force = true;
        flag2 = true;
      }
      else if (this.worldScale >= 11.0)
      {
        VillageData rolloverTargetVillage = GameEngine.Instance.World.rolloverTargetVillage;
        if (rolloverTargetVillage != null)
        {
          flag1 = this.checkVillageForShieldRollover(rolloverTargetVillage, village, ref shieldXOff, ref shieldYOff, ref shieldTypeID);
          flag2 = flag1;
        }
        if (GameEngine.Instance.World.m_userInfoShieldRolloverUserID != -1 && village.userID == GameEngine.Instance.World.m_userInfoShieldRolloverUserID)
        {
          flag1 = true;
          flag2 = true;
        }
      }
      bool flag3 = false;
      bool flag4 = false;
      bool flag5 = false;
      bool flag6 = false;
      float num1 = 5f;
      if (!flag3 && flag1 && shieldTypeID > -10000)
      {
        int worldShieldTexture = GameEngine.Instance.World.getWorldShieldTexture(shieldTypeID, force);
        if (worldShieldTexture > 0)
        {
          this.gfx.beginSprites();
          Color white = ARGBColors.White;
          if (village.userID == RemoteServices.Instance.UserID || GameEngine.Instance.World.isUserRelatedVillage(village.id))
          {
            this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
            Color baseColor;
            if (village.userID == RemoteServices.Instance.UserID)
            {
              this.villageSprite.SpriteNo = 386;
              this.villageSprite.Center = new PointF(33f, 69f);
              baseColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0);
            }
            else
            {
              this.villageSprite.SpriteNo = 387;
              this.villageSprite.Center = new PointF(33f, 69f);
              baseColor = Color.FromArgb(0, (int) byte.MaxValue, 0);
            }
            scale = (float) this.worldScale / 17f;
            if ((double) scale < 0.15000000596046448)
              scale = 0.15f;
            if ((double) scale > 1.0)
              scale = 1f;
            if (village.id == InterfaceMgr.Instance.getSelectedMenuVillage())
            {
              float num2 = (float) this.pulse / 128f;
              if ((double) num2 > 1.0)
                num2 = 2f - num2;
              baseColor = Color.FromArgb((int) ((double) byte.MaxValue - (double) byte.MaxValue * ((double) num2 / 2.0)), baseColor);
            }
            this.villageSprite.ColorToUse = baseColor;
            this.villageSprite.Scale = scale;
            this.villageSprite.Update();
            if (flag4)
              this.villageSprite.FakeDrawAndClear();
            else if (flag2)
              this.villageSprite.DrawAndClear();
            else
              this.villageSprite.FakeDrawAndClear();
          }
          scale = (float) this.worldScale / 17f;
          if ((double) scale < 0.15000000596046448)
            scale = 0.15f;
          if ((double) scale > 1.0)
            scale = 1f;
          int num3 = 32;
          int num4 = 14;
          if (village.userID == RemoteServices.Instance.UserID)
          {
            num3 = 64;
            num4 = 18;
          }
          if (flag5)
            scale = 1f;
          if (flag6)
            scale = num1;
          Rectangle srcRect = new Rectangle(0, 0, num3, num3);
          Size size = new Size(this.iScale(num3, scale), this.iScale(num3, scale));
          PointF renderPos = new PointF(this.villageSprite.PosX - (float) (num4 - shieldXOff) * scale, this.villageSprite.PosY - (float) (36 + num4 - shieldYOff) * scale);
          this.gfx.Draw2D(this.gfx.getTexture(worldShieldTexture), srcRect, (SizeF) size, renderPos, ARGBColors.White);
        }
        else
          flag1 = false;
      }
      return flag1;
    }

    public void draw(VillageData village, double xPos, double yPos)
    {
      bool drawSprite = false;
      float scale = 1f;
      int spriteID = 0;
      int worldMapIconsTexId = GFXLibrary.Instance.WorldMapIconsTexID;
      int resourceSpriteNo = -1;
      bool newVillage = false;
      bool capitalVillage = false;
      Color villageColoriser = ARGBColors.White;
      Color surroundColoriser = Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, 192);
      string capitalName = "";
      bool allowSurroundDraw = true;
      double num1 = 27.0 - this.worldZoom;
      int NORMAL_OFFSET_30 = 30;
      int NORMAL_OFFSET_110 = 110;
      bool aiWorldSpecial = false;
      if (village.countryCapital)
      {
        if (this.worldZoom <= 23.59)
          this.setSpriteToCountryCapital(ref drawSprite, ref spriteID, village, ref villageColoriser, ref capitalVillage, ref scale, ref capitalName);
      }
      else if (village.provinceCapital)
      {
        if (this.worldZoom <= 23.0)
          this.setSpriteToProvinceCapital(ref drawSprite, ref spriteID, village, ref villageColoriser, ref capitalVillage, ref scale, ref capitalName);
      }
      else if (village.countyCapital)
      {
        if (this.worldZoom <= 22.5 || GameEngine.Instance.World.PickingStartCounty)
          this.setSpriteToCountyCapital(ref drawSprite, ref spriteID, village, ref villageColoriser, ref capitalVillage, ref scale, ref capitalName);
      }
      else if (village.regionCapital)
      {
        if (this.worldZoom <= 19.5 || this.mapEditing)
          this.setSpriteToParishCapital(ref drawSprite, ref spriteID, village, ref villageColoriser, ref capitalVillage, ref scale, ref capitalName);
      }
      else if (village.special == 30)
      {
        this.setSpriteToInvasionMarkerAndDraw(ref allowSurroundDraw, ref drawSprite, ref scale, village, ref spriteID, xPos, yPos, ref villageColoriser);
        newVillage = true;
      }
      else if (num1 >= 8.0)
      {
        if (this.worldScale < 11.0 && ((village.special < 100 || village.special > 199) && !SpecialVillageTypes.IS_TREASURE_CASTLE(village.special) && !SpecialVillageTypes.IS_ROYAL_TOWER(village.special) && village.special != 30 || num1 < 6.0))
        {
          this.gfx.endSprites();
          this.gfx.drawLine(ARGBColors.Black, (float) xPos * (float) this.screenSize.Width, (float) yPos * (float) this.screenSize.Height, (float) (xPos * (double) this.screenSize.Width + 1.0), (float) yPos * (float) this.screenSize.Height);
        }
        else
        {
          drawSprite = true;
          scale = (float) ((this.worldScale - 8.5) / 8.5);
          if ((double) scale > 1.0)
            scale = 1f;
          if (village.special == 0)
          {
            int houseIdFromVillage = this.getHouseIDFromVillage(village);
            villageColoriser = WorldMap.getVillageColor(houseIdFromVillage);
            spriteID = this.fixupVillageSprites(houseIdFromVillage);
            worldMapIconsTexId = GFXLibrary.Instance.WorldMapIconsTexID;
            newVillage = true;
          }
          else
          {
            if (village.special == 20)
              return;
            this.setSpriteToSpecialType(village, ref resourceSpriteNo, ref spriteID, ref scale, ref newVillage, ref villageColoriser, ref aiWorldSpecial, ref NORMAL_OFFSET_110, ref NORMAL_OFFSET_30);
          }
        }
      }
      float origScale = scale;
      this.villageSprite.PosX = (float) xPos * (float) this.screenSize.Width;
      this.villageSprite.PosY = (float) yPos * (float) this.screenSize.Height;
      if (drawSprite)
      {
        bool showSurround = false;
        if (village.userID >= 0 && GameEngine.Instance.World.getHouse(village.factionID) > 0)
          showSurround = true;
        this.gfx.beginSprites();
        if ((InterfaceMgr.Instance.SelectedVillage == village.id || InterfaceMgr.Instance.OwnSelectedVillage == village.id || InterfaceMgr.Instance.SelectedVassalVillage == village.id) && !newVillage && !capitalVillage && allowSurroundDraw)
          this.drawMapIconSelectedTint(origScale, village, newVillage);
        if (SpecialVillageTypes.IS_ROYAL_TOWER(village.special))
          this.villageSprite.Center = new PointF(43f, 110f);
        if (!village.Capital)
        {
          Color white = ARGBColors.White;
          if (village.special == 0 || newVillage)
          {
            this.drawMapIconSurround(ref surroundColoriser, villageColoriser, origScale, aiWorldSpecial, village, spriteID, showSurround, NORMAL_OFFSET_30, ref white, NORMAL_OFFSET_110);
            if (GameEngine.Instance.World.DrawDebugNames)
              GameEngine.Instance.World.addText(village.id.ToString(), new PointF(this.villageSprite.PosX, this.villageSprite.PosY + 10f * origScale), ARGBColors.Black, true, 1);
            if (GameEngine.Instance.World.DrawDebugVillageNames)
              GameEngine.Instance.World.addText(village.villageName, new PointF(this.villageSprite.PosX, this.villageSprite.PosY + 10f * origScale), ARGBColors.Black, true, 1);
          }
        }
        if (spriteID >= 0 && !newVillage)
        {
          if (capitalVillage)
          {
            surroundColoriser = Color.FromArgb(192, villageColoriser);
            if (villageColoriser == ARGBColors.White)
              surroundColoriser = Color.FromArgb(128, villageColoriser);
            if (showSurround)
              this.drawHouseColourSurroundOnCapital(worldMapIconsTexId, spriteID, NORMAL_OFFSET_30, surroundColoriser, origScale, village);
            Color white = ARGBColors.White;
            if (InterfaceMgr.Instance.OwnSelectedVillage == village.id || InterfaceMgr.Instance.SelectedVassalVillage == village.id)
              this.drawPulsingGlowOnCurrentPlayerControlledCapital(ref surroundColoriser, worldMapIconsTexId, spriteID, NORMAL_OFFSET_110, origScale, village, ref white);
            else if (InterfaceMgr.Instance.SelectedVillage == village.id)
              this.drawPulsingGlowOnTargetedCapital(ref surroundColoriser, worldMapIconsTexId, spriteID, NORMAL_OFFSET_110, origScale, village, ref white);
            this.drawCapitalSprite(worldMapIconsTexId, spriteID, white, origScale, village);
            this.drawCapitalName(village, origScale, capitalName);
            int regionId = (int) village.regionID;
            int parishPlague = GameEngine.Instance.World.getParishPlague(regionId);
            int numFlags = (int) village.numFlags;
            bool flag = true;
            if (parishPlague > 0 && flag)
              this.drawParishPlagueLevel(worldMapIconsTexId, parishPlague, origScale, village);
            if (numFlags > 0 && flag)
              this.drawParishFlags(worldMapIconsTexId, origScale, village, scale);
          }
          else
            this.drawUnopenedStash(worldMapIconsTexId, spriteID, origScale, village);
        }
        if (resourceSpriteNo >= 0)
          this.drawResourceStash(resourceSpriteNo, origScale, village);
      }
      bool flag1 = this.DrawShields(village, xPos, yPos, scale);
      if (GameEngine.Instance.World.isUserVillage(village.id) || GameEngine.Instance.World.isUserRelatedVillage(village.id) || (village.factionID < 0 || RemoteServices.Instance.UserFactionID < 0 || !GameEngine.Instance.World.worldMapFilter.FilterShowHouseSymbols && !GameEngine.Instance.World.worldMapFilter.FilterShowFactionSymbols) && (village.userID < 0 || !GameEngine.Instance.World.worldMapFilter.FilterShowUserSymbols))
        return;
      bool flag2 = false;
      if (village.countryCapital)
        flag2 = true;
      else if (village.provinceCapital)
        flag2 = true;
      else if (village.countyCapital)
      {
        if (num1 >= 4.0)
          flag2 = true;
      }
      else if (village.regionCapital)
      {
        if (num1 >= 6.0)
          flag2 = true;
      }
      else if (num1 >= 8.0)
        flag2 = true;
      if (!flag2)
        return;
      bool flag3 = false;
      int num2 = -1;
      int num3 = -1;
      if (village.factionID == RemoteServices.Instance.UserFactionID)
      {
        if (GameEngine.Instance.World.worldMapFilter.FilterShowUserSymbols)
        {
          int userRelationship = GameEngine.Instance.World.getUserRelationship(village.userID);
          if (userRelationship > 0)
            num3 = 179;
          else if (userRelationship < 0)
            num3 = 180;
        }
        if (num3 == -1 && RemoteServices.Instance.UserFactionID >= 0)
          num3 = 178;
      }
      else
      {
        if (GameEngine.Instance.World.worldMapFilter.FilterShowHouseSymbols && num3 == -1)
        {
          int house1 = GameEngine.Instance.World.getHouse(RemoteServices.Instance.UserFactionID);
          int house2 = GameEngine.Instance.World.getHouse(village.factionID);
          if (house1 != house2)
          {
            int yourHouseRelation = GameEngine.Instance.World.getYourHouseRelation(house2);
            if (yourHouseRelation > 0)
              num3 = 179;
            else if (yourHouseRelation < 0)
              num3 = 180;
          }
        }
        if (GameEngine.Instance.World.worldMapFilter.FilterShowFactionSymbols)
        {
          if (num3 != -1)
            num2 = num3;
          int yourFactionRelation = GameEngine.Instance.World.getYourFactionRelation(village.factionID);
          if (yourFactionRelation > 0)
            num3 = 179;
          else if (yourFactionRelation < 0)
            num3 = 180;
          if (num2 != -1 && num3 != -1 && num3 != num2)
            flag3 = true;
        }
        if (num3 != -1)
          num2 = num3;
        int userRelationship = GameEngine.Instance.World.getUserRelationship(village.userID);
        if (userRelationship > 0)
          num3 = 179;
        else if (userRelationship < 0)
          num3 = 180;
        if (num2 != -1 && num3 != -1 && num3 != num2)
          flag3 = true;
      }
      if (num3 >= 0)
      {
        if (flag3)
          num3 = 179;
        this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
        this.villageSprite.SpriteNo = num3;
        int num4 = 0;
        if (flag1)
          num4 = 30;
        this.villageSprite.Center = num3 == 180 ? new PointF((float) (42 + num4), 77f) : new PointF((float) (39 + num4), 77f);
        scale = (float) this.worldScale / 17f;
        if ((double) scale < 0.15000000596046448)
          scale = 0.15f;
        if ((double) scale > 1.0)
          scale = 1f;
        this.gfx.beginSprites();
        this.villageSprite.Scale = scale;
        this.villageSprite.Update();
        this.villageSprite.DrawAndClear();
        if (flag3)
        {
          int num5 = 180;
          this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
          this.villageSprite.SpriteNo = num5;
          this.villageSprite.Center = num5 == 180 ? new PointF((float) (42 + num4 + 20), 77f) : new PointF((float) (39 + num4 + 20), 77f);
          scale = (float) this.worldScale / 17f;
          scale *= 0.75f;
          if ((double) scale < 0.15000000596046448)
            scale = 0.15f;
          if ((double) scale > 0.75)
            scale = 0.75f;
          this.villageSprite.Scale = scale;
          this.villageSprite.Update();
          this.villageSprite.DrawAndClear();
        }
      }
      UserMarker userMarker = GameEngine.Instance.World.getUserMarker(village.userID);
      if (userMarker == null || userMarker.markerType <= 0)
        return;
      int num6 = 30;
      if (num1 > 16.0)
        num6 = 10;
      int num7 = 459 + userMarker.markerType;
      this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
      this.villageSprite.SpriteNo = num7;
      this.villageSprite.Center = new PointF((float) num6, (float) num6);
      scale = (float) this.worldScale / 17f;
      scale *= 0.5f;
      if ((double) scale < 0.34999999403953552)
        scale = 0.35f;
      if ((double) scale > 0.64999997615814209)
        scale = 0.65f;
      this.gfx.beginSprites();
      this.villageSprite.Scale = 1f;
      this.villageSprite.ColorToUse = Color.FromArgb(180, ARGBColors.White);
      this.villageSprite.Update();
      this.villageSprite.DrawAndClear();
    }
  }
}
