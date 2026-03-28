// Decompiled with JetBrains decompiler
// Type: Kingdoms.WorldMapInputHandler
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  internal class WorldMapInputHandler : InputHandler
  {
    private WorldMap world;
    private static bool wasOverDXWindow;

    public WorldMapInputHandler(WorldMap worldMap) => this.world = worldMap;

    public void handleInput(MouseInputState input)
    {
      if (!input.leftdown && input.isScrolling())
      {
        if (input.scrollLeft)
          this.world.moveMap(0.0 - 10.0 / this.world.WorldScale, 0.0);
        if (input.scrollRight)
          this.world.moveMap(10.0 / this.world.WorldScale, 0.0);
        if (input.scrollUp)
          this.world.moveMap(0.0, 0.0 - 10.0 / this.world.WorldScale);
        if (input.scrollDown)
          this.world.moveMap(0.0, 10.0 / this.world.WorldScale);
      }
      bool flag = InterfaceMgr.Instance.isOverDXScreen(input.dxMousePos);
      if (flag && !input.leftdown)
        this.world.mouseNotDown(input.dxMousePos);
      if (flag || this.world.holdingLeftMouse())
      {
        if (input.leftdown)
          this.world.leftMouseDown(input.dxMousePos);
        else if (input.rightdown)
          this.world.zoomOut();
        else if (input.mousebackward)
        {
          this.world.stopZoom();
          double worldZoom = this.world.WorldZoom;
          if (worldZoom > 26.899999998509884)
            this.world.setMouseWheelZoomOut(14f);
          else if (worldZoom > 13.899999618530273)
            this.world.setMouseWheelZoomOut(9.5f);
          else if (worldZoom > 9.3999996185302734)
            this.world.setMouseWheelZoomOut(6.5f);
          else if (worldZoom > 6.4000000953674316)
            this.world.setMouseWheelZoomOut(3.5f);
          else if (worldZoom > 3.4000000953674316)
            this.world.setMouseWheelZoomOut(2f);
          else
            this.world.setMouseWheelZoomOut(0.0f);
        }
        else if (input.mouseforward)
        {
          this.world.stopZoom();
          double worldZoom = this.world.WorldZoom;
          if (worldZoom < 0.10000000149011612)
            this.world.changeZoom(2f, input.dxMousePos);
          else if (worldZoom < 2.0999999046325684)
            this.world.changeZoom(3.5f, input.dxMousePos);
          else if (worldZoom < 3.5999999046325684)
            this.world.changeZoom(6.5f, input.dxMousePos);
          else if (worldZoom < 6.5999999046325684)
            this.world.changeZoom(9.5f, input.dxMousePos);
          else if (worldZoom < 9.6000003814697266)
            this.world.changeZoom(14f, input.dxMousePos);
          else
            this.world.changeZoom(27f, input.dxMousePos);
          if (worldZoom < 26.899999998509884)
            GameEngine.Instance.playInterfaceSound("WorldMap_mousewheel_zoomin");
          this.world.centreMap(false);
        }
        else
          this.world.moveMouse(input.dxMousePos);
        InterfaceMgr.Instance.mouseMoveDXCardBar(input.dxMousePos);
        InterfaceMgr.Instance.mouseMoveDXPlaybackBar(input.dxMousePos);
        GameEngine.Instance.World.freeCardTooltip(input.dxMousePos);
      }
      else
      {
        if (WorldMapInputHandler.wasOverDXWindow)
          CustomTooltipManager.MouseLeaveTooltipAreaMapSpecial();
        WorldMapInputHandler.wasOverDXWindow = flag;
        if (!(InterfaceMgr.Instance.ParentForm.Cursor == Cursors.Hand))
          return;
        InterfaceMgr.Instance.ParentForm.Cursor = Cursors.Default;
      }
    }
  }
}
