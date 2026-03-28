// Decompiled with JetBrains decompiler
// Type: Kingdoms.PanelImage
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PanelImage : Panel
  {
    private IContainer components;
    private float alpha = 1f;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();

    public float Alpha
    {
      get => this.alpha;
      set
      {
        if ((double) this.alpha == (double) value)
          return;
        this.alpha = value;
        this.Invalidate();
      }
    }

    public PanelImage()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Rectangle destRect = new Rectangle(0, 0, this.BackgroundImage.Width, this.BackgroundImage.Height);
      e.Graphics.DrawImage(this.BackgroundImage, destRect, destRect.X, destRect.Y, destRect.Width, destRect.Height, GraphicsUnit.Pixel, this.createAlpha(this.alpha));
    }

    private ImageAttributes createAlpha(float alpha)
    {
      ColorMatrix newColorMatrix = new ColorMatrix();
      newColorMatrix.Matrix00 = newColorMatrix.Matrix11 = newColorMatrix.Matrix22 = newColorMatrix.Matrix44 = 1f;
      newColorMatrix.Matrix33 = alpha;
      ImageAttributes alpha1 = new ImageAttributes();
      alpha1.SetColorMatrix(newColorMatrix);
      return alpha1;
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
      if (this.BackColor == ARGBColors.Transparent)
      {
        Rectangle rect = new Rectangle(-this.Location.X, -this.Location.Y, this.Parent.Width, this.Parent.Height);
        LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(197, 189, 158), Color.FromArgb(141, 126, 105), LinearGradientMode.Vertical);
        e.Graphics.FillRectangle((Brush) linearGradientBrush, rect);
        linearGradientBrush.Dispose();
      }
      else
      {
        Brush brush = (Brush) new SolidBrush(this.BackColor);
        Rectangle rect = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
        e.Graphics.FillRectangle(brush, rect);
        brush.Dispose();
      }
    }
  }
}
