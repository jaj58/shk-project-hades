// Decompiled with JetBrains decompiler
// Type: Kingdoms.BuildingMgr
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Collections;

//#nullable disable
namespace Kingdoms
{
  public class BuildingMgr
  {
    private static readonly BuildingMgr instance = new BuildingMgr();
    private ArrayList buildingList = new ArrayList();
    private int buildingUID;

    private BuildingMgr()
    {
    }

    public static BuildingMgr Instance => BuildingMgr.instance;

    public void AddBuilding(int buildingType, int mapX, int mapY)
    {
      this.buildingList.Add((object) new Building(buildingType, mapX, mapY, this.buildingUID));
      ++this.buildingUID;
    }
  }
}
