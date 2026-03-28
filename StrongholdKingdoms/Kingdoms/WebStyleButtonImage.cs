// Decompiled with JetBrains decompiler
// Type: Kingdoms.WebStyleButtonImage
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

//#nullable disable
namespace Kingdoms
{
  public class WebStyleButtonImage
  {
    public static Image Generate(
      int width,
      int height,
      string text,
      Font font,
      Color forecolour,
      Color backcolour,
      int radius)
    {
      int num = radius * 2;
      Image image = (Image) new Bitmap(width + 1, height + 1);
      SolidBrush solidBrush1 = new SolidBrush(forecolour);
      SolidBrush solidBrush2 = new SolidBrush(backcolour);
      Pen pen1 = new Pen(forecolour);
      Pen pen2 = new Pen(backcolour);
      using (Graphics graphics = Graphics.FromImage(image))
      {
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        graphics.Clear(ARGBColors.Transparent);
        graphics.FillRectangle((Brush) solidBrush2, new Rectangle(0, radius, width, height - num));
        graphics.FillRectangle((Brush) solidBrush2, new Rectangle(radius, 0, width - num, height));
        graphics.FillEllipse((Brush) solidBrush2, new Rectangle(0, 0, num, num));
        graphics.FillEllipse((Brush) solidBrush2, new Rectangle(0, height - num, num, num));
        graphics.FillEllipse((Brush) solidBrush2, new Rectangle(width - num, 0, num, num));
        graphics.FillEllipse((Brush) solidBrush2, new Rectangle(width - num, height - num, num, num));
        StringFormat format = new StringFormat();
        format.LineAlignment = StringAlignment.Center;
        format.Alignment = StringAlignment.Center;
        font = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-BoldCond.ttf", 12f, FontStyle.Bold);
        graphics.DrawString(text, font, (Brush) solidBrush1, new RectangleF(0.0f, 0.0f, (float) (width + 1), (float) (height + 1)), format);
      }
      pen1.Dispose();
      pen2.Dispose();
      solidBrush1.Dispose();
      solidBrush2.Dispose();
      return image;
    }

    public static Image GenerateLabel(
      int width,
      int height,
      string text,
      Color forecolour,
      Color backcolour)
    {
      Image image = (Image) new Bitmap(width + 1, height + 1);
      SolidBrush solidBrush1 = new SolidBrush(forecolour);
      SolidBrush solidBrush2 = new SolidBrush(backcolour);
      using (Graphics graphics = Graphics.FromImage(image))
      {
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        graphics.Clear(backcolour);
        StringFormat format = new StringFormat();
        format.LineAlignment = StringAlignment.Near;
        format.Alignment = StringAlignment.Near;
        Font privateFont = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-BoldCond.ttf", 14f, FontStyle.Bold);
        graphics.DrawString(text, privateFont, (Brush) solidBrush1, new RectangleF(0.0f, 0.0f, (float) (width + 1), (float) (height + 1)), format);
      }
      solidBrush1.Dispose();
      solidBrush2.Dispose();
      return image;
    }
  }
}
