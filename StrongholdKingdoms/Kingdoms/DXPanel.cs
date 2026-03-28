// Decompiled with JetBrains decompiler
// Type: Kingdoms.DXPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class DXPanel : Panel
  {
    public bool resizing;
    public bool allowDraw;
    public static bool skipPaint;

    public DXPanel()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.AllPaintingInWmPaint, true);
      this.BackColor = ARGBColors.Black;
      this.resizing = false;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (GameEngine.Instance != null)
      {
        if (!DXPanel.skipPaint && this.allowDraw)
        {
          this.allowDraw = false;
          GameEngine.Instance.OnPaintCallback();
        }
        DXPanel.skipPaint = false;
      }
      else
      {
        Pen pen = new Pen(ARGBColors.Black);
        e.Graphics.DrawRectangle(pen, e.ClipRectangle);
        pen.Dispose();
      }
    }

    public void AllowDraw() => this.allowDraw = true;

    protected override void OnResize(EventArgs e)
    {
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      if (this.Size.Width == 0 || this.Size.Height == 0)
        return;
      if (this.Visible)
      {
        this.Visible = false;
        base.OnSizeChanged(e);
        this.Visible = true;
      }
      else
        base.OnSizeChanged(e);
    }
  }
}
