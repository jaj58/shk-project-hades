// Decompiled with JetBrains decompiler
// Type: Kingdoms.Building
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using DXGraphics;

//#nullable disable
namespace Kingdoms
{
  public class Building
  {
    private int m_type;
    private int m_UID;
    private int m_X;
    private int m_Y;
    private SpriteWrapper sprite;

    public Building(int buildingType, int mapX, int mapY, int UID)
    {
      this.m_type = buildingType;
      this.m_UID = UID;
      this.m_X = mapX;
      this.m_Y = mapY;
      this.sprite = new SpriteWrapper();
    }
  }
}
