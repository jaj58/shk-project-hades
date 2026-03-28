// Decompiled with JetBrains decompiler
// Type: Kingdoms.VillageMapBuildingInnExtension
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using DXGraphics;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class VillageMapBuildingInnExtension
  {
    public const int numPilesAtInn = 3;
    public const int innBaseY = -44;
    public const int innBaseX = -80;
    public SpriteWrapper[] cell = new SpriteWrapper[3];
    public static int[] innLayout = new int[6]
    {
      160,
      64,
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
        int SpriteNo = level - 1 + 192;
        this.cell[cellID].Initialize(gfx, GFXLibrary.Instance.Goods1TexID, SpriteNo);
        PointF pointF = new PointF(32f, 101f);
        this.cell[cellID].Center = pointF;
        this.cell[cellID].PosX = posX;
        this.cell[cellID].PosY = posY;
      }
    }

    public void colorSprites(Color col)
    {
      for (int index = 0; index < 3; ++index)
      {
        if (this.cell[index] != null)
          this.cell[index].ColorToUse = col;
      }
    }

    public void dispose()
    {
      for (int index = 0; index < 3; ++index)
        this.cell[index] = (SpriteWrapper) null;
    }
  }
}
