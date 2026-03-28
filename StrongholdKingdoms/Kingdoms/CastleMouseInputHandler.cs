// Decompiled with JetBrains decompiler
// Type: Kingdoms.CastleMouseInputHandler
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

//#nullable disable
namespace Kingdoms
{
  internal class CastleMouseInputHandler
  {
    private CastleMap castle;
    private GameEngine.GameDisplaySubModes gameDisplayModeSubMode;
    private static bool wasOverDXWindow;

    public CastleMouseInputHandler(
      CastleMap castlemap,
      GameEngine.GameDisplaySubModes castleSubMode)
    {
      this.castle = castlemap;
      this.gameDisplayModeSubMode = castleSubMode;
    }

    public void handleInput(MouseInputState input)
    {
      if (this.castle == null)
        return;
      if (input.leftdown && input.isScrolling())
      {
        if (input.scrollLeft)
          this.castle.moveMap(10, 0);
        if (input.scrollRight)
          this.castle.moveMap(-10, 0);
        if (input.scrollUp)
          this.castle.moveMap(0, 10);
        if (input.scrollDown)
          this.castle.moveMap(0, -10);
      }
      if (!input.leftdown)
        this.castle.mouseNotClicked(input.dxMousePos);
      bool flag = InterfaceMgr.Instance.isOverDXScreen(input.dxMousePos);
      if (flag || this.castle.holdingLeftMouse())
      {
        if (input.rightdown)
          this.castle.rightClick(input.dxMousePos);
        else
          this.castle.mouseMoveUpdate(input.dxMousePos, input.leftdown);
      }
      else if (CastleMouseInputHandler.wasOverDXWindow)
        CustomTooltipManager.MouseLeaveTooltipAreaMapSpecial();
      CastleMouseInputHandler.wasOverDXWindow = flag;
      if (!input.mousebackward && !input.mouseforward && GameEngine.Instance.GFX.keyCode != 32 || this.gameDisplayModeSubMode != GameEngine.GameDisplaySubModes.SUBMODE_DEFAULT)
        return;
      this.castle.mouseWheel();
    }
  }
}
