// Decompiled with JetBrains decompiler
// Type: Kingdoms.PlaybackBarDX
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  internal class PlaybackBarDX
  {
    private PlaybackBarGDI GDIBar = new PlaybackBarGDI();
    private CustomSelfDrawPanel csd = new CustomSelfDrawPanel();
    private bool awaitingInit;

    public void init()
    {
      this.awaitingInit = false;
      this.csd.clearControls();
      this.csd.addControl((CustomSelfDrawPanel.CSDControl) this.GDIBar);
      this.GDIBar.init();
      this.GDIBar.isDirty = true;
      this.update();
    }

    public void delayedInit() => this.awaitingInit = true;

    public void update()
    {
      if (!InterfaceMgr.Instance.allowDrawCircles())
        return;
      if (this.awaitingInit)
      {
        this.init();
      }
      else
      {
        this.GDIBar.update();
        if (!this.GDIBar.isDirty)
          return;
        Graphics dxPlaybackTexture = GameEngine.Instance.GFX.createDXPlaybackTexture(this.GDIBar.Size, new Point(GameEngine.Instance.World.m_screenWidth / 2 - this.GDIBar.Size.Width / 2, GameEngine.Instance.World.m_screenHeight - this.GDIBar.Size.Height));
        if (dxPlaybackTexture != null)
        {
          if (this.csd.initFromDX(dxPlaybackTexture, (CustomSelfDrawPanel.CSDControl) this.GDIBar))
          {
            this.csd.drawControls();
            this.csd.endPaint();
          }
          GameEngine.Instance.GFX.renderDXPlaybackTexture(dxPlaybackTexture);
        }
        this.GDIBar.flagAsRendered();
      }
    }

    public bool click(Point mousePos)
    {
      if (!GameEngine.Instance.GFX.drawPlaybackTexture)
        return false;
      Point mousePos1 = mousePos;
      mousePos1.X -= GameEngine.Instance.GFX.pbLocation.X;
      mousePos1.Y -= GameEngine.Instance.GFX.pbLocation.Y;
      return this.csd.baseControl.parentClicked(mousePos1);
    }

    public bool mouseUp(Point mousePos)
    {
      if (!GameEngine.Instance.GFX.drawPlaybackTexture)
        return false;
      Point mousePos1 = mousePos;
      mousePos1.X -= GameEngine.Instance.GFX.pbLocation.X;
      mousePos1.Y -= GameEngine.Instance.GFX.pbLocation.Y;
      return this.csd.baseControl.parentMouseUp(mousePos1) != null;
    }

    public bool mouseDown(Point mousePos)
    {
      if (!GameEngine.Instance.GFX.drawPlaybackTexture)
        return false;
      Point mousePos1 = mousePos;
      mousePos1.X -= GameEngine.Instance.GFX.pbLocation.X;
      mousePos1.Y -= GameEngine.Instance.GFX.pbLocation.Y;
      return this.csd.baseControl.parentMouseDown(mousePos1) != null;
    }

    public void toggleEnabled(bool value) => this.GDIBar.toggleActive(value);

    public void mouseMove(Point mousePos)
    {
      if (!GameEngine.Instance.GFX.drawPlaybackTexture)
        return;
      Point point = mousePos;
      point.X -= GameEngine.Instance.GFX.pbLocation.X;
      point.Y -= GameEngine.Instance.GFX.pbLocation.Y;
      this.csd.tooltipSet = false;
      CustomTooltipManager.MouseLeaveTooltipArea();
      this.csd.baseControl.parentMouseOver(point);
      this.GDIBar.setMouseRelative(point);
    }
  }
}
