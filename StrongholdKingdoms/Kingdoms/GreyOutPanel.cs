// Decompiled with JetBrains decompiler
// Type: Kingdoms.GreyOutPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class GreyOutPanel : UserControl
  {
    private IContainer components;
    private Rectangle innerArea = new Rectangle();
    private Bitmap _backBuffer;
    private bool forceBackgroundRedraw;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.None;
    }

    public GreyOutPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint, true);
    }

    public void setInnerArea(Rectangle area)
    {
      this.innerArea = area;
      this.forceBackgroundRedraw = true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this._backBuffer == null || this.forceBackgroundRedraw)
      {
        if (this._backBuffer == null)
          this._backBuffer = new Bitmap(this.Size.Width, this.Size.Height);
        this.forceBackgroundRedraw = false;
        Graphics g = Graphics.FromImage((Image) this._backBuffer);
        Brush brush = (Brush) new SolidBrush(ARGBColors.Black);
        g.FillRectangle(brush, new Rectangle(this.Location, this.Size));
        brush.Dispose();
        int x = this.innerArea.X;
        int y = this.innerArea.Y;
        if (x > 0 || y > 0)
        {
          g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_topleft, x - 128, y - 128, 128, 128);
          g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_topright, x + this.innerArea.Width, y - 128, 128, 128);
          g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_bottomleft, x - 128, y + this.innerArea.Height, 128, 128);
          g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_bottomright, x + this.innerArea.Width, y + this.innerArea.Height, 128, 128);
          if (x > 0)
          {
            this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_top, (float) x, (float) (y - 128), (float) this.innerArea.Width, 128f);
            this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_bottom, (float) x, (float) (y + this.innerArea.Height), (float) this.innerArea.Width, 128f);
          }
          if (y > 0)
          {
            this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_left, (float) (x - 128), (float) y, 128f, (float) this.innerArea.Height);
            this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_right, (float) (x + this.innerArea.Width), (float) y, 128f, (float) this.innerArea.Height);
          }
        }
        g.Dispose();
      }
      e?.Graphics.DrawImageUnscaled((Image) this._backBuffer, 0, 0);
    }

    private void drawImageStretched(
      Graphics g,
      Image image,
      float x,
      float y,
      float width,
      float height)
    {
      RectangleF srcRect = image.Width != 1 ? new RectangleF(0.0f, 0.0f, (float) image.Width, 1E-05f) : new RectangleF(0.0f, 0.0f, 1E-05f, (float) image.Height);
      RectangleF destRect = new RectangleF(x, y, width, height);
      g.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      if (this._backBuffer != null)
      {
        this._backBuffer.Dispose();
        this._backBuffer = (Bitmap) null;
        this.Invalidate();
      }
      base.OnSizeChanged(e);
    }
  }
}
