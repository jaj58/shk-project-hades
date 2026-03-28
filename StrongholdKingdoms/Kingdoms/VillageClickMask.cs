// Decompiled with JetBrains decompiler
// Type: Kingdoms.VillageClickMask
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using DXGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class VillageClickMask
  {
    private GraphicsMgr gfx;
    private List<VillageClickMask.BuildingClickMask> buildings = new List<VillageClickMask.BuildingClickMask>();
    public byte[,] maskMap;
    public int mapWidth;
    public int mapHeight;
    public bool mapDirty;
    public bool mapClear = true;
    public long ignoredBuildingID = -1;

    public void init(int width, int height, GraphicsMgr graphics)
    {
      this.gfx = graphics;
      this.mapWidth = width;
      this.mapHeight = height;
      this.maskMap = new byte[width, height];
      this.clearMap();
      this.buildings.Clear();
    }

    public void clearMapAndBuildings()
    {
      this.clearMap();
      this.buildings.Clear();
    }

    public void clearMap()
    {
      if (this.maskMap != null && !this.mapClear)
      {
        for (int index1 = 0; index1 < this.mapWidth; ++index1)
        {
          for (int index2 = 0; index2 < this.mapHeight; ++index2)
            this.maskMap[index1, index2] = (byte) 0;
        }
      }
      this.mapClear = true;
      this.mapDirty = false;
    }

    public void addBuilding(
      long buildingID,
      int xPos,
      int yPos,
      int textureID,
      int spriteNo,
      PointF center)
    {
      if (buildingID < 0L)
        return;
      this.buildings.Add(new VillageClickMask.BuildingClickMask()
      {
        buildingID = buildingID,
        x = xPos,
        y = yPos,
        center = new Point((int) center.X, (int) center.Y),
        textureID = textureID,
        spriteNo = spriteNo,
        vcmID = this.buildings.Count
      });
      this.mapDirty = true;
    }

    public void removeBuilding(long buildingID)
    {
      if (buildingID < 0L)
        return;
      foreach (VillageClickMask.BuildingClickMask building in this.buildings)
      {
        if (building.buildingID == buildingID)
        {
          building.buildingID = -1L;
          this.mapDirty = true;
        }
      }
    }

    public long getBuildingIDFromWorldPos(Point worldPos)
    {
      this.rebuildMap();
      if (worldPos.X >= 0 && worldPos.X < this.mapWidth && worldPos.Y >= 0 && worldPos.Y < this.mapHeight)
      {
        int index = (int) this.maskMap[worldPos.X, worldPos.Y] - 1;
        if (index >= 0)
        {
          if (this.buildings.Count > 250 && worldPos.Y > this.mapHeight * 3 / 4 && index < 100 && index + (int) byte.MaxValue < this.buildings.Count)
            index += (int) byte.MaxValue;
          if (index < this.buildings.Count && this.buildings[index] != null)
            return this.buildings[index].buildingID;
        }
      }
      return -1;
    }

    public void forceDirtyMap() => this.mapDirty = true;

    private void rebuildMap()
    {
      if (!this.mapDirty)
        return;
      this.clearMap();
      this.mapDirty = false;
      this.mapClear = false;
      this.buildings.Sort(new Comparison<VillageClickMask.BuildingClickMask>(VillageClickMask.CompareBuildingByYpos));
      int num1 = 0;
      foreach (VillageClickMask.BuildingClickMask building in this.buildings)
      {
        building.vcmID = num1++;
        if (building.buildingID >= 0L && building.buildingID != this.ignoredBuildingID)
        {
          int TagID = 1;
          UVSpriteLoader spriteLoader = this.gfx.getSpriteLoader(building.textureID, ref TagID);
          if (spriteLoader != null)
          {
            UVSpriteLoader.MaskImage mask = spriteLoader.getMask(TagID, building.spriteNo);
            if (mask != null)
            {
              Rectangle sourceRect;
              PointF cornerOffset;
              spriteLoader.GetSpriteXYdata(TagID, building.spriteNo, out sourceRect, out cornerOffset, out SizeF _);
              byte num2 = (byte) (building.vcmID + 1);
              if (building.vcmID >= (int) byte.MaxValue)
                ++num2;
              int num3 = building.x + (int) cornerOffset.X - building.center.X;
              int num4 = building.y + (int) cornerOffset.Y - building.center.Y;
              int width = sourceRect.Width;
              int height = sourceRect.Height;
              for (int y = 0; y < height; ++y)
              {
                if (num4 + y >= 0 && num4 + y < this.mapHeight)
                {
                  for (int x = 0; x < width; ++x)
                  {
                    if (num3 + x >= 0 && num3 + x < this.mapWidth && mask.test(x, y))
                      this.maskMap[num3 + x, num4 + y] = num2;
                  }
                }
              }
            }
          }
        }
      }
      if (this.ignoredBuildingID < 0L)
        return;
      this.mapDirty = true;
      this.ignoredBuildingID = -1L;
    }

    private static int CompareBuildingByYpos(
      VillageClickMask.BuildingClickMask x,
      VillageClickMask.BuildingClickMask y)
    {
      if (x == null)
        return y == null ? 0 : -1;
      if (y == null || x.y > y.y)
        return 1;
      return x.y == y.y ? 0 : -1;
    }

    private class BuildingClickMask
    {
      public int vcmID = -1;
      public int textureID = -1;
      public int spriteNo;
      public int x;
      public int y;
      public Point center;
      public long buildingID;
    }
  }
}
