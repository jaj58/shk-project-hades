// Decompiled with JetBrains decompiler
// Type: Kingdoms.VillageInputHandler
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  internal class VillageInputHandler : InputHandler
  {
    private VillageMap village;
    private static bool wasOverDXWindow;

    public VillageInputHandler(VillageMap villagemap) => this.village = villagemap;

    public void handleInput(MouseInputState input)
    {
      if (this.village == null)
        return;
      if (!input.leftdown && input.isScrolling())
      {
        if (input.scrollLeft)
          this.village.Camera.Drag(new Point(10, 0));
        if (input.scrollRight)
          this.village.Camera.Drag(new Point(-10, 0));
        if (input.scrollUp)
          this.village.Camera.Drag(new Point(0, 10));
        if (input.scrollDown)
          this.village.Camera.Drag(new Point(0, -10));
      }
      if (!input.leftdown)
        this.village.mouseNotClicked(input.dxMousePos);
      bool flag = InterfaceMgr.Instance.isOverDXScreen(input.dxMousePos);
      if (flag || this.village.holdingLeftMouse())
        this.village.mouseMoveUpdate(input.dxMousePos, input.leftdown);
      else if (VillageInputHandler.wasOverDXWindow)
        CustomTooltipManager.MouseLeaveTooltipAreaMapSpecial();
      VillageInputHandler.wasOverDXWindow = flag;
      if (!input.rightdown)
        return;
      this.village.stopPlaceBuilding(true);
    }
  }
}
