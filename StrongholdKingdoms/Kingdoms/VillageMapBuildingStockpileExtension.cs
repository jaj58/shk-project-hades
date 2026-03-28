// Decompiled with JetBrains decompiler
// Type: Kingdoms.VillageMapBuildingStockpileExtension
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using DXGraphics;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class VillageMapBuildingStockpileExtension
  {
    public const int stockpileBaseY = -43;
    public const int stockpileBaseX = -96;
    public SpriteWrapper[] cell = new SpriteWrapper[16];
    public static int[] stockpileLayout = new int[32]
    {
      96,
      0,
      64,
      16,
      128,
      16,
      32,
      32,
      96,
      32,
      160,
      32,
      0,
      48,
      64,
      48,
      128,
      48,
      192,
      48,
      32,
      64,
      96,
      64,
      160,
      64,
      64,
      80,
      128,
      80,
      96,
      96
    };

    public void showGood(GraphicsMgr gfx, int cellID, int buildingType, int level)
    {
      if (buildingType < 0 || level == 0)
      {
        this.cell[cellID].Visible = false;
      }
      else
      {
        this.cell[cellID].Visible = true;
        float posX = this.cell[cellID].PosX;
        float posY = this.cell[cellID].PosY;
        int SpriteNo = level - 1;
        switch (buildingType)
        {
          case 6:
            SpriteNo += 144;
            break;
          case 7:
            SpriteNo += 224;
            break;
          case 8:
            SpriteNo += 48;
            break;
          case 9:
            SpriteNo += 208;
            break;
        }
        this.cell[cellID].Initialize(gfx, GFXLibrary.Instance.Goods1TexID, SpriteNo);
        PointF pointF = new PointF(32f, 101f);
        this.cell[cellID].Center = pointF;
        this.cell[cellID].PosX = posX;
        this.cell[cellID].PosY = posY;
      }
    }

    public void colorSprites(Color col)
    {
      for (int index = 0; index < 16; ++index)
      {
        if (this.cell[index] != null)
          this.cell[index].ColorToUse = col;
      }
    }

    public void dispose()
    {
      for (int index = 0; index < 16; ++index)
        this.cell[index] = (SpriteWrapper) null;
    }
  }
}
