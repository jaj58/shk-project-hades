// Decompiled with JetBrains decompiler
// Type: Kingdoms.MainWindowPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MainWindowPanel : UserControl, IDockWindow
  {
    private IContainer components;
    private DockWindow dockWindow;
    private bool m_allowDraw = true;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Black;
      this.Name = nameof (MainWindowPanel);
      this.Size = new Size(439, 407);
      this.ResumeLayout(false);
    }

    public void AddControl(UserControl control, int x, int y)
    {
      this.dockWindow.AddControl(control, x, y);
    }

    public void RemoveControl(UserControl control) => this.dockWindow.RemoveControl(control);

    public MainWindowPanel()
    {
      this.dockWindow = new DockWindow((ContainerControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint, true);
    }

    public void doDraw(bool allowDraw) => this.m_allowDraw = allowDraw;

    protected override void OnPaint(PaintEventArgs e)
    {
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
      if (!this.m_allowDraw)
        return;
      base.OnPaintBackground(e);
    }
  }
}
