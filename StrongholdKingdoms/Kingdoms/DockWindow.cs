// Decompiled with JetBrains decompiler
// Type: Kingdoms.DockWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class DockWindow
  {
    private ContainerControl parentControl;

    public DockWindow(ContainerControl parent) => this.parentControl = parent;

    public void AddControl(UserControl control, int x, int y)
    {
      this.parentControl.SuspendLayout();
      control.Location = new Point(x, y);
      this.parentControl.Controls.Add((Control) control);
      this.parentControl.ResumeLayout(false);
      this.parentControl.PerformLayout();
    }

    public void RemoveControl(UserControl control)
    {
      this.parentControl.Controls.Remove((Control) control);
    }
  }
}
