// Decompiled with JetBrains decompiler
// Type: Kingdoms.CastleCameraWinforms
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using DXGraphics;
using System;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class CastleCameraWinforms : ICameraController
  {
    private SpriteWrapper m_backgroundSprite;

    public CastleCameraWinforms(SpriteWrapper backgroundSprite)
    {
      this.m_backgroundSprite = backgroundSprite;
    }

    public Rectangle GetCameraRectangle()
    {
      Point worldSpace = this.ScreenToWorldSpace(new Point(0, 0));
      return new Rectangle(worldSpace.X, worldSpace.Y, GameEngine.Instance.GFX.ViewportWidth, GameEngine.Instance.GFX.ViewportHeight);
    }

    public Point getCameraCentre()
    {
      Point worldSpace = this.ScreenToWorldSpace(new Point(0, 0));
      return new Point(worldSpace.X + GameEngine.Instance.GFX.ViewportWidth / 2, worldSpace.Y + GameEngine.Instance.GFX.ViewportHeight / 2);
    }

    public void MoveToPosition(Point position)
    {
      this.m_backgroundSprite.PosX = (float) position.X;
      this.m_backgroundSprite.PosY = (float) position.Y;
      this.Drag(new Point(0, 0));
    }

    public void Drag(Point delta)
    {
      if (this.m_backgroundSprite != null)
      {
        this.m_backgroundSprite.move(delta.X, delta.Y);
        this.m_backgroundSprite.keepBounded();
        this.m_backgroundSprite.centreSmallerSprite();
        this.m_backgroundSprite.fixup2DPos();
      }
      else
        UniversalDebugLog.Log("background sprite is null :(");
    }

    public void ChangeZoom(float delta) => throw new NotImplementedException();

    public Point ScreenToWorldSpace(Point point)
    {
      point.X -= (int) this.m_backgroundSprite.DrawPos.X;
      point.Y -= (int) this.m_backgroundSprite.DrawPos.Y;
      return point;
    }

    public Point WorldToScreenSpace(Point point)
    {
      point.X += (int) this.m_backgroundSprite.DrawPos.X;
      point.Y += (int) this.m_backgroundSprite.DrawPos.Y;
      return point;
    }

    public Point WorldSpaceToMapTile(Point worldPos)
    {
      worldPos.X -= 5;
      worldPos.Y -= 16;
      worldPos.Y *= 2;
      PointF point = new PointF((float) worldPos.X, (float) worldPos.Y);
      PointF pointF = GameEngine.Instance.GFX.rotatePoint(point, 315);
      pointF.X /= 22.6274166f;
      pointF.Y /= 22.6274166f;
      pointF.X += 58f;
      return new Point((int) pointF.X, (int) pointF.Y);
    }

    public Point MapTileToWorldSpace(Point mapTile)
    {
      return new Point()
      {
        X = mapTile.X * 16 + mapTile.Y * 16 - 922,
        Y = mapTile.Y * 8 - mapTile.X * 8 + 474
      };
    }

    public Point ScreenSpaceToMapTile(Point point)
    {
      return this.WorldSpaceToMapTile(this.ScreenToWorldSpace(point));
    }

    public Point MapTileToScreenSpace(Point mapTile)
    {
      return this.WorldToScreenSpace(this.MapTileToWorldSpace(mapTile));
    }
  }
}
