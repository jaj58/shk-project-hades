// Decompiled with JetBrains decompiler
// Type: Kingdoms.MouseInputState
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MouseInputState : InputState
  {
    public bool mousebackward;
    public bool mouseforward;
    public bool scrollLeft;
    public bool scrollRight;
    public bool scrollUp;
    public bool scrollDown;
    public Point clientMousePos;
    public Point dxMousePos;

    public override void getInput()
    {
      this.leftdown = GameEngine.Instance.GFX.leftmousedown;
      this.rightdown = GameEngine.Instance.GFX.rightClick;
      this.mousebackward = GameEngine.Instance.GFX.mouseBackward;
      this.mouseforward = GameEngine.Instance.GFX.mouseForward;
      this.mousePos = new Point(Cursor.Position.X, Cursor.Position.Y);
      this.clientMousePos = InterfaceMgr.Instance.ParentForm.PointToClient(this.mousePos);
      this.dxMousePos = InterfaceMgr.Instance.getDXBasePanel().PointToClient(this.mousePos);
      if (this.mousePos.Equals((object) GameEngine.Instance.lastMouseMovePosition))
        return;
      GameEngine.Instance.lastMouseMovePosition = this.mousePos;
      GameEngine.Instance.lastMouseMoveTime = DateTime.Now;
    }

    public bool isScrolling()
    {
      return this.scrollLeft || this.scrollRight || this.scrollUp || this.scrollDown;
    }
  }
}
