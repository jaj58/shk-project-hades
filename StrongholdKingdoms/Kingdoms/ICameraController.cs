// Decompiled with JetBrains decompiler
// Type: Kingdoms.ICameraController
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public interface ICameraController
  {
    Rectangle GetCameraRectangle();

    Point getCameraCentre();

    void MoveToPosition(Point position);

    void Drag(Point mousePos);

    void ChangeZoom(float delta);

    Point ScreenToWorldSpace(Point point);

    Point WorldToScreenSpace(Point point);

    Point WorldSpaceToMapTile(Point worldPos);

    Point MapTileToWorldSpace(Point mapTile);

    Point ScreenSpaceToMapTile(Point point);

    Point MapTileToScreenSpace(Point mapTile);
  }
}
