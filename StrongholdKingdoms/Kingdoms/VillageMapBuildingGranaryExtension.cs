// Decompiled with JetBrains decompiler
// Type: Kingdoms.VillageMapBuildingGranaryExtension
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using DXGraphics;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class VillageMapBuildingGranaryExtension
  {
    public const int granaryBaseY = -33;
    public const int granaryBaseX = 5;
    public SpriteWrapper[] cell = new SpriteWrapper[21];
    public static int[] granaryLayout = new int[42]
    {
      8,
      28,
      20,
      32,
      -6,
      27,
      -6,
      3,
      -6,
      -21,
      -37,
      40,
      -37,
      23,
      -37,
      6,
      32,
      36,
      41,
      43,
      25,
      39,
      25,
      23,
      25,
      7,
      -5,
      50,
      -5,
      28,
      -16,
      54,
      -16,
      24,
      11,
      58,
      11,
      36,
      0,
      62,
      0,
      32
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
          case 13:
            SpriteNo += 80;
            break;
          case 14:
            SpriteNo += 128;
            break;
          case 15:
            SpriteNo += 160;
            break;
          case 16:
            SpriteNo += 112;
            break;
          case 17:
            SpriteNo += 96;
            break;
          case 18:
            SpriteNo += 64;
            break;
        }
        this.cell[cellID].Initialize(gfx, GFXLibrary.Instance.Goods2TexID, SpriteNo);
        PointF pointF = new PointF(32f, 101f);
        this.cell[cellID].Center = pointF;
        this.cell[cellID].PosX = posX;
        this.cell[cellID].PosY = posY;
      }
    }

    public void colorSprites(Color col)
    {
      for (int index = 0; index < 21; ++index)
      {
        if (this.cell[index] != null)
          this.cell[index].ColorToUse = col;
      }
    }

    public void dispose()
    {
      for (int index = 0; index < 21; ++index)
        this.cell[index] = (SpriteWrapper) null;
    }
  }
}
