// Decompiled with JetBrains decompiler
// Type: Kingdoms.BitmapButton
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class BitmapButton : Button
  {
    private Image _ImageNormal;
    private Image _ImageFocused;
    private Image _ImagePressed;
    private Image _ImageMouseOver;
    private Image _ImageInactive;
    private Color _BorderColor = ARGBColors.DarkBlue;
    private Color _InnerBorderColor = ARGBColors.LightGray;
    private Color _InnerBorderColor_Focus = ARGBColors.LightBlue;
    private Color _InnerBorderColor_MouseOver = ARGBColors.Gold;
    private Color _ImageBorderColor = ARGBColors.Chocolate;
    private bool _StretchImage;
    private bool _TextDropShadow;
    private int _Padding = 5;
    private bool _OffsetPressedContent = true;
    private bool _ImageBorderEnabled = true;
    private bool _ImageDropShadow = true;
    private bool _FocusRectangleEnabled;
    private BtnState btnState = BtnState.Normal;
    private bool CapturingMouse;
    private bool _BorderDrawing = true;
    private System.ComponentModel.Container components;

    [Browsable(false)]
    public new Image Image
    {
      get => base.Image;
      set => base.Image = value;
    }

    [Browsable(false)]
    public new ImageList ImageList
    {
      get => base.ImageList;
      set => base.ImageList = value;
    }

    [Browsable(false)]
    public new int ImageIndex
    {
      get => base.ImageIndex;
      set => base.ImageIndex = value;
    }

    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Browsable(true)]
    [Description("enables the text to cast a shadow")]
    public bool TextDropShadow
    {
      get => this._TextDropShadow;
      set => this._TextDropShadow = value;
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Browsable(true)]
    [Category("Appearance")]
    [Description("enables the drawing of the border")]
    public bool BorderDrawing
    {
      get => this._BorderDrawing;
      set => this._BorderDrawing = value;
    }

    [Category("Appearance")]
    [Browsable(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("enables the focus rectangle")]
    public bool FocusRectangleEnabled
    {
      get => this._FocusRectangleEnabled;
      set => this._FocusRectangleEnabled = value;
    }

    [Browsable(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [Description("enables the image to cast a shadow")]
    public bool ImageDropShadow
    {
      get => this._ImageDropShadow;
      set => this._ImageDropShadow = value;
    }

    [Browsable(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [Description("Color of the border around the image")]
    public Color ImageBorderColor
    {
      get => this._ImageBorderColor;
      set => this._ImageBorderColor = value;
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Enables the bordering of the image")]
    public bool ImageBorderEnabled
    {
      get => this._ImageBorderEnabled;
      set => this._ImageBorderEnabled = value;
    }

    [Browsable(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [Description("Color of the border around the button")]
    public Color BorderColor
    {
      get => this._BorderColor;
      set => this._BorderColor = value;
    }

    [Browsable(true)]
    [Description("Color of the inner border when the button has focus")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    public Color InnerBorderColor_Focus
    {
      get => this._InnerBorderColor_Focus;
      set => this._InnerBorderColor_Focus = value;
    }

    [Browsable(true)]
    [Description("Color of the inner border when the button does not hvae focus")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    public Color InnerBorderColor
    {
      get => this._InnerBorderColor;
      set => this._InnerBorderColor = value;
    }

    [Browsable(true)]
    [Description("color of the inner border when the mouse is over the button")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    public Color InnerBorderColor_MouseOver
    {
      get => this._InnerBorderColor_MouseOver;
      set => this._InnerBorderColor_MouseOver = value;
    }

    [Description("stretch the impage to the size of the button")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [Browsable(true)]
    public bool StretchImage
    {
      get => this._StretchImage;
      set => this._StretchImage = value;
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Browsable(true)]
    [Category("Appearance")]
    [Description("padded pixels around the image and text")]
    public int Padding2
    {
      get => this._Padding;
      set => this._Padding = value;
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("Set to true if to offset image/text when button is pressed")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public bool OffsetPressedContent
    {
      get => this._OffsetPressedContent;
      set => this._OffsetPressedContent = value;
    }

    [Category("Appearance")]
    [Description("Image to be displayed while the button state is in normal state")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Browsable(true)]
    public Image ImageNormal
    {
      get => this._ImageNormal;
      set => this._ImageNormal = value;
    }

    [Browsable(true)]
    [Description("Image to be displayed while the button has focus")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    public Image ImageFocused
    {
      get => this._ImageFocused;
      set => this._ImageFocused = value;
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Image to be displayed while the button is inactive")]
    public Image ImageInactive
    {
      get => this._ImageInactive;
      set => this._ImageInactive = value;
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("Image to be displayed while the button state is pressed")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public Image ImagePressed
    {
      get => this._ImagePressed;
      set => this._ImagePressed = value;
    }

    [Category("Appearance")]
    [Description("Image to be displayed while the button state is MouseOver")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Browsable(true)]
    public Image ImageMouseOver
    {
      get => this._ImageMouseOver;
      set => this._ImageMouseOver = value;
    }

    public BitmapButton() => this.InitializeComponent();

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.Region != null)
        {
          this.Region.Dispose();
          this.Region = (Region) null;
        }
        if (this.components != null)
          this.components.Dispose();
      }
      base.Dispose(disposing);
    }

    private void InitializeComponent() => this.components = new System.ComponentModel.Container();

    protected override void OnPaint(PaintEventArgs e)
    {
      this.CreateRegion(0);
      this.paint_Background(e);
      this.paint_Text(e);
      this.paint_Image(e);
      if (!this.BorderDrawing)
        return;
      this.paint_Border(e);
      this.paint_InnerBorder(e);
      this.paint_FocusBorder(e);
    }

    private void paint_Background(PaintEventArgs e)
    {
      if (e == null || e.Graphics == null)
        return;
      Graphics graphics = e.Graphics;
      Rectangle rect = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
      Color baseColor = this.BackColor;
      if (this.btnState == BtnState.Inactive)
        baseColor = ARGBColors.LightGray;
      Color SColor = ControlPaint.Light(baseColor, 0.1f);
      Color[] colorArray;
      float[] numArray;
      if (this.btnState == BtnState.Pushed)
      {
        colorArray = new Color[6]
        {
          BitmapButton.Blend(this.BackColor, ARGBColors.White, 80),
          BitmapButton.Blend(this.BackColor, ARGBColors.White, 40),
          BitmapButton.Blend(this.BackColor, ARGBColors.Black, 0),
          BitmapButton.Blend(this.BackColor, ARGBColors.Black, 0),
          BitmapButton.Blend(this.BackColor, ARGBColors.White, 40),
          BitmapButton.Blend(this.BackColor, ARGBColors.White, 80)
        };
        numArray = new float[6]
        {
          0.0f,
          0.05f,
          0.4f,
          0.6f,
          0.95f,
          1f
        };
      }
      else
      {
        colorArray = new Color[6]
        {
          BitmapButton.Blend(SColor, ARGBColors.White, 80),
          BitmapButton.Blend(SColor, ARGBColors.White, 90),
          BitmapButton.Blend(SColor, ARGBColors.White, 30),
          BitmapButton.Blend(SColor, ARGBColors.White, 0),
          BitmapButton.Blend(SColor, ARGBColors.Black, 30),
          BitmapButton.Blend(SColor, ARGBColors.Black, 20)
        };
        numArray = new float[6]
        {
          0.0f,
          0.15f,
          0.4f,
          0.65f,
          0.95f,
          1f
        };
      }
      ColorBlend colorBlend = new ColorBlend();
      colorBlend.Colors = colorArray;
      colorBlend.Positions = numArray;
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, this.BackColor, BitmapButton.Blend(this.BackColor, this.BackColor, 10), LinearGradientMode.Vertical);
      linearGradientBrush.InterpolationColors = colorBlend;
      graphics.FillRectangle((Brush) linearGradientBrush, rect);
      linearGradientBrush.Dispose();
    }

    private void paint_Border(PaintEventArgs e)
    {
      if (e == null || e.Graphics == null)
        return;
      Pen pen = new Pen(this.BorderColor, 1f);
      Point[] points = this.border_Get(0, 0, this.Width - 1, this.Height - 1);
      e.Graphics.DrawLines(pen, points);
      pen.Dispose();
    }

    private void paint_FocusBorder(PaintEventArgs e)
    {
      if (e == null || e.Graphics == null || !this.Focused || !this.FocusRectangleEnabled)
        return;
      ControlPaint.DrawFocusRectangle(e.Graphics, new Rectangle(3, 3, this.Width - 6, this.Height - 6), ARGBColors.Black, this.BackColor);
    }

    private void paint_InnerBorder(PaintEventArgs e)
    {
      if (e == null || e.Graphics == null)
        return;
      Graphics graphics = e.Graphics;
      Rectangle rect = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
      Color SColor = this.BackColor;
      bool flag = false;
      switch (this.btnState)
      {
        case BtnState.Inactive:
          SColor = ARGBColors.Gray;
          break;
        case BtnState.Normal:
          if (this.Focused)
          {
            SColor = this.InnerBorderColor_Focus;
            flag = true;
            break;
          }
          SColor = this.InnerBorderColor;
          break;
        case BtnState.MouseOver:
          SColor = this.InnerBorderColor_MouseOver;
          break;
        case BtnState.Pushed:
          SColor = BitmapButton.Blend(this.InnerBorderColor_Focus, ARGBColors.Black, 10);
          flag = true;
          break;
      }
      Color[] colorArray;
      float[] numArray;
      if (this.btnState == BtnState.Pushed)
      {
        colorArray = new Color[6]
        {
          BitmapButton.Blend(SColor, ARGBColors.Black, 20),
          BitmapButton.Blend(SColor, ARGBColors.Black, 10),
          BitmapButton.Blend(SColor, ARGBColors.White, 0),
          BitmapButton.Blend(SColor, ARGBColors.White, 50),
          BitmapButton.Blend(SColor, ARGBColors.White, 85),
          BitmapButton.Blend(SColor, ARGBColors.White, 90)
        };
        numArray = new float[6]
        {
          0.0f,
          0.2f,
          0.5f,
          0.6f,
          0.9f,
          1f
        };
      }
      else
      {
        colorArray = new Color[6]
        {
          BitmapButton.Blend(SColor, ARGBColors.White, 80),
          BitmapButton.Blend(SColor, ARGBColors.White, 60),
          BitmapButton.Blend(SColor, ARGBColors.White, 10),
          BitmapButton.Blend(SColor, ARGBColors.White, 0),
          BitmapButton.Blend(SColor, ARGBColors.Black, 20),
          BitmapButton.Blend(SColor, ARGBColors.Black, 50)
        };
        numArray = new float[6]
        {
          0.0f,
          0.2f,
          0.5f,
          0.6f,
          0.9f,
          1f
        };
      }
      ColorBlend colorBlend = new ColorBlend();
      colorBlend.Colors = colorArray;
      colorBlend.Positions = numArray;
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, this.BackColor, BitmapButton.Blend(this.BackColor, this.BackColor, 10), LinearGradientMode.Vertical);
      linearGradientBrush.InterpolationColors = colorBlend;
      Pen pen = new Pen((Brush) linearGradientBrush, 1f);
      Point[] pts = this.border_Get(0, 0, this.Width - 1, this.Height - 1);
      this.border_Contract(1, ref pts);
      e.Graphics.DrawLines(pen, pts);
      this.border_Contract(1, ref pts);
      e.Graphics.DrawLines(pen, pts);
      if (flag)
      {
        this.border_Contract(1, ref pts);
        e.Graphics.DrawLines(pen, pts);
      }
      pen.Dispose();
      linearGradientBrush.Dispose();
    }

    private void paint_Text(PaintEventArgs e)
    {
      if (e == null || e.Graphics == null)
        return;
      Rectangle textDestinationRect = this.GetTextDestinationRect();
      StringFormat format = new StringFormat();
      format.Alignment = StringAlignment.Center;
      format.LineAlignment = StringAlignment.Center;
      if (this.btnState == BtnState.Pushed && this.OffsetPressedContent)
        textDestinationRect.Offset(1, 1);
      SizeF sizeF = this.txt_Size(e.Graphics, this.Text, this.Font);
      Point leftEdgeTopEdge = this.Calculate_LeftEdgeTopEdge(this.TextAlign, textDestinationRect, (int) sizeF.Width, (int) sizeF.Height);
      if (this.btnState == BtnState.Inactive)
      {
        textDestinationRect.Offset(1, 1);
        Brush brush1 = (Brush) new SolidBrush(ARGBColors.White);
        e.Graphics.DrawString(this.Text, this.Font, brush1, (RectangleF) textDestinationRect, format);
        brush1.Dispose();
        textDestinationRect.Offset(-1, -1);
        Brush brush2 = (Brush) new SolidBrush(Color.FromArgb(50, 50, 50));
        e.Graphics.DrawString(this.Text, this.Font, brush2, (RectangleF) textDestinationRect, format);
        brush2.Dispose();
      }
      else
      {
        if (this.TextDropShadow)
        {
          Brush brush3 = (Brush) new SolidBrush(Color.FromArgb(50, ARGBColors.Black));
          Brush brush4 = (Brush) new SolidBrush(Color.FromArgb(20, ARGBColors.Black));
          e.Graphics.DrawString(this.Text, this.Font, brush3, (float) leftEdgeTopEdge.X, (float) (leftEdgeTopEdge.Y + 1));
          e.Graphics.DrawString(this.Text, this.Font, brush3, (float) (leftEdgeTopEdge.X + 1), (float) leftEdgeTopEdge.Y);
          e.Graphics.DrawString(this.Text, this.Font, brush4, (float) (leftEdgeTopEdge.X + 1), (float) (leftEdgeTopEdge.Y + 1));
          e.Graphics.DrawString(this.Text, this.Font, brush4, (float) leftEdgeTopEdge.X, (float) (leftEdgeTopEdge.Y + 2));
          e.Graphics.DrawString(this.Text, this.Font, brush4, (float) (leftEdgeTopEdge.X + 2), (float) leftEdgeTopEdge.Y);
          brush3.Dispose();
          brush4.Dispose();
        }
        Brush brush = (Brush) new SolidBrush(this.ForeColor);
        e.Graphics.DrawString(this.Text, this.Font, brush, (RectangleF) textDestinationRect, format);
        brush.Dispose();
      }
    }

    private void paint_ImageBorder(Graphics g, Rectangle ImageRect)
    {
      Rectangle rect = ImageRect;
      if (this.ImageDropShadow)
      {
        Pen pen1 = new Pen(Color.FromArgb(80, 0, 0, 0));
        Pen pen2 = new Pen(Color.FromArgb(40, 0, 0, 0));
        g.DrawLine(pen1, new Point(rect.Right, rect.Bottom), new Point(rect.Right + 1, rect.Bottom));
        g.DrawLine(pen1, new Point(rect.Right + 1, rect.Top + 1), new Point(rect.Right + 1, rect.Bottom));
        g.DrawLine(pen2, new Point(rect.Right + 2, rect.Top + 2), new Point(rect.Right + 2, rect.Bottom + 1));
        g.DrawLine(pen1, new Point(rect.Left + 1, rect.Bottom + 1), new Point(rect.Right, rect.Bottom + 1));
        g.DrawLine(pen2, new Point(rect.Left + 1, rect.Bottom + 2), new Point(rect.Right + 1, rect.Bottom + 2));
        pen1.Dispose();
        pen2.Dispose();
      }
      if (!this.ImageBorderEnabled)
        return;
      Color SColor = this.ImageBorderColor;
      if (!this.Enabled)
        SColor = ARGBColors.LightGray;
      Color[] colorArray = new Color[6]
      {
        BitmapButton.Blend(SColor, ARGBColors.White, 40),
        BitmapButton.Blend(SColor, ARGBColors.White, 20),
        BitmapButton.Blend(SColor, ARGBColors.White, 30),
        BitmapButton.Blend(SColor, ARGBColors.White, 0),
        BitmapButton.Blend(SColor, ARGBColors.Black, 30),
        BitmapButton.Blend(SColor, ARGBColors.Black, 70)
      };
      float[] numArray = new float[6]
      {
        0.0f,
        0.2f,
        0.5f,
        0.6f,
        0.9f,
        1f
      };
      ColorBlend colorBlend = new ColorBlend();
      colorBlend.Colors = colorArray;
      colorBlend.Positions = numArray;
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, this.BackColor, BitmapButton.Blend(this.BackColor, this.BackColor, 10), LinearGradientMode.Vertical);
      linearGradientBrush.InterpolationColors = colorBlend;
      Pen pen3 = new Pen((Brush) linearGradientBrush, 1f);
      Pen pen4 = new Pen(ARGBColors.Black);
      rect.Inflate(1, 1);
      Point[] pts = this.border_Get(rect.Left, rect.Top, rect.Width, rect.Height);
      this.border_Contract(1, ref pts);
      g.DrawLines(pen4, pts);
      this.border_Contract(1, ref pts);
      g.DrawLines(pen3, pts);
      pen4.Dispose();
      pen3.Dispose();
      linearGradientBrush.Dispose();
    }

    private void paint_Image(PaintEventArgs e)
    {
      if (e == null || e.Graphics == null)
        return;
      Image currentImage = this.GetCurrentImage(this.btnState);
      if (currentImage == null)
        return;
      Graphics graphics = e.Graphics;
      Rectangle imageDestinationRect = this.GetImageDestinationRect();
      if (this.btnState == BtnState.Pushed && this._OffsetPressedContent)
        imageDestinationRect.Offset(1, 1);
      if (this.StretchImage)
      {
        graphics.DrawImage(currentImage, imageDestinationRect, 0, 0, currentImage.Width, currentImage.Height, GraphicsUnit.Pixel);
      }
      else
      {
        this.GetImageDestinationRect();
        graphics.DrawImage(currentImage, imageDestinationRect, 0, 0, currentImage.Width, currentImage.Height, GraphicsUnit.Pixel);
      }
      this.paint_ImageBorder(graphics, imageDestinationRect);
    }

    private SizeF txt_Size(Graphics g, string strText, Font font) => g.MeasureString(strText, font);

    private Rectangle GetTextDestinationRect()
    {
      Rectangle imageDestinationRect = this.GetImageDestinationRect();
      Rectangle textDestinationRect = new Rectangle(0, 0, 0, 0);
      switch (this.ImageAlign)
      {
        case ContentAlignment.TopLeft:
          textDestinationRect = new Rectangle(0, imageDestinationRect.Bottom, this.Width, this.Height - imageDestinationRect.Bottom);
          break;
        case ContentAlignment.TopCenter:
          textDestinationRect = new Rectangle(0, imageDestinationRect.Bottom, this.Width, this.Height - imageDestinationRect.Bottom);
          break;
        case ContentAlignment.TopRight:
          textDestinationRect = new Rectangle(0, imageDestinationRect.Bottom, this.Width, this.Height - imageDestinationRect.Bottom);
          break;
        case ContentAlignment.MiddleLeft:
          textDestinationRect = new Rectangle(imageDestinationRect.Right, 0, this.Width - imageDestinationRect.Right, this.Height);
          break;
        case ContentAlignment.MiddleCenter:
          textDestinationRect = new Rectangle(0, imageDestinationRect.Bottom, this.Width, this.Height - imageDestinationRect.Bottom);
          break;
        case ContentAlignment.MiddleRight:
          textDestinationRect = new Rectangle(0, 0, imageDestinationRect.Left, this.Height);
          break;
        case ContentAlignment.BottomLeft:
          textDestinationRect = new Rectangle(0, 0, this.Width, imageDestinationRect.Top);
          break;
        case ContentAlignment.BottomCenter:
          textDestinationRect = new Rectangle(0, 0, this.Width, imageDestinationRect.Top);
          break;
        case ContentAlignment.BottomRight:
          textDestinationRect = new Rectangle(0, 0, this.Width, imageDestinationRect.Top);
          break;
      }
      textDestinationRect.Inflate(-this.Padding2, -this.Padding2);
      return textDestinationRect;
    }

    private Rectangle GetImageDestinationRect()
    {
      Rectangle imageDestinationRect = new Rectangle(0, 0, 0, 0);
      Image currentImage = this.GetCurrentImage(this.btnState);
      if (currentImage != null)
      {
        if (this.StretchImage)
        {
          imageDestinationRect.Width = this.Width;
          imageDestinationRect.Height = this.Height;
        }
        else
        {
          imageDestinationRect.Width = currentImage.Width;
          imageDestinationRect.Height = currentImage.Height;
          Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
          rect.Inflate(-this.Padding2, -this.Padding2);
          Point leftEdgeTopEdge = this.Calculate_LeftEdgeTopEdge(this.ImageAlign, rect, currentImage.Width, currentImage.Height);
          imageDestinationRect.Offset(leftEdgeTopEdge);
        }
      }
      return imageDestinationRect;
    }

    private Image GetCurrentImage(BtnState btnState)
    {
      Image currentImage = this.ImageNormal;
      switch (btnState)
      {
        case BtnState.Inactive:
          if (this.ImageInactive != null)
          {
            currentImage = this.ImageInactive;
            break;
          }
          if (currentImage != null)
            this.ImageInactive = (Image) this.ConvertToGrayscale(new Bitmap(this.ImageNormal));
          currentImage = this.ImageNormal;
          break;
        case BtnState.Normal:
          if (this.Focused && this.ImageFocused != null)
          {
            currentImage = this.ImageFocused;
            break;
          }
          break;
        case BtnState.MouseOver:
          if (this.ImageMouseOver != null)
          {
            currentImage = this.ImageMouseOver;
            break;
          }
          break;
        case BtnState.Pushed:
          if (this.ImagePressed != null)
          {
            currentImage = this.ImagePressed;
            break;
          }
          break;
      }
      return currentImage;
    }

    public Bitmap ConvertToGrayscale(Bitmap source)
    {
      Bitmap grayscale = new Bitmap(source.Width, source.Height);
      for (int y = 0; y < grayscale.Height; ++y)
      {
        for (int x = 0; x < grayscale.Width; ++x)
        {
          Color pixel = source.GetPixel(x, y);
          int num = (int) ((double) pixel.R * 0.3 + (double) pixel.G * 0.59 + (double) pixel.B * 0.11);
          grayscale.SetPixel(x, y, Color.FromArgb(num, num, num));
        }
      }
      return grayscale;
    }

    private Point Calculate_LeftEdgeTopEdge(
      ContentAlignment Alignment,
      Rectangle rect,
      int nWidth,
      int nHeight)
    {
      Point leftEdgeTopEdge = new Point(0, 0);
      switch (Alignment)
      {
        case ContentAlignment.TopLeft:
          leftEdgeTopEdge.X = 0;
          leftEdgeTopEdge.Y = 0;
          break;
        case ContentAlignment.TopCenter:
          leftEdgeTopEdge.X = (rect.Width - nWidth) / 2;
          leftEdgeTopEdge.Y = 0;
          break;
        case ContentAlignment.TopRight:
          leftEdgeTopEdge.X = rect.Width - nWidth;
          leftEdgeTopEdge.Y = 0;
          break;
        case ContentAlignment.MiddleLeft:
          leftEdgeTopEdge.X = 0;
          leftEdgeTopEdge.Y = (rect.Height - nHeight) / 2;
          break;
        case ContentAlignment.MiddleCenter:
          leftEdgeTopEdge.X = (rect.Width - nWidth) / 2;
          leftEdgeTopEdge.Y = (rect.Height - nHeight) / 2;
          break;
        case ContentAlignment.MiddleRight:
          leftEdgeTopEdge.X = rect.Width - nWidth;
          leftEdgeTopEdge.Y = (rect.Height - nHeight) / 2;
          break;
        case ContentAlignment.BottomLeft:
          leftEdgeTopEdge.X = 0;
          leftEdgeTopEdge.Y = rect.Height - nHeight;
          break;
        case ContentAlignment.BottomCenter:
          leftEdgeTopEdge.X = (rect.Width - nWidth) / 2;
          leftEdgeTopEdge.Y = rect.Height - nHeight;
          break;
        case ContentAlignment.BottomRight:
          leftEdgeTopEdge.X = rect.Width - nWidth;
          leftEdgeTopEdge.Y = rect.Height - nHeight;
          break;
      }
      leftEdgeTopEdge.X += rect.Left;
      leftEdgeTopEdge.Y += rect.Top;
      return leftEdgeTopEdge;
    }

    private void CreateRegion(int nContract)
    {
      Point[] pts = this.border_Get(0, 0, this.Width, this.Height);
      this.border_Contract(nContract, ref pts);
      GraphicsPath path = new GraphicsPath();
      path.AddLines(pts);
      this.Region = new Region(path);
    }

    private void border_Contract(int nPixel, ref Point[] pts)
    {
      int num = nPixel;
      pts[0].X += num;
      pts[0].Y += num;
      pts[1].X -= num;
      pts[1].Y += num;
      pts[2].X -= num;
      pts[2].Y += num;
      pts[3].X -= num;
      pts[3].Y += num;
      pts[4].X -= num;
      pts[4].Y -= num;
      pts[5].X -= num;
      pts[5].Y -= num;
      pts[6].X -= num;
      pts[6].Y -= num;
      pts[7].X += num;
      pts[7].Y -= num;
      pts[8].X += num;
      pts[8].Y -= num;
      pts[9].X += num;
      pts[9].Y -= num;
      pts[10].X += num;
      pts[10].Y += num;
      pts[11].X += num;
      pts[10].Y += num;
    }

    private Point[] border_Get(int nLeftEdge, int nTopEdge, int nWidth, int nHeight)
    {
      int x = nWidth;
      int y = nHeight;
      Point[] pointArray = new Point[12]
      {
        new Point(1, 0),
        new Point(x - 1, 0),
        new Point(x - 1, 1),
        new Point(x, 1),
        new Point(x, y - 1),
        new Point(x - 1, y - 1),
        new Point(x - 1, y),
        new Point(1, y),
        new Point(1, y - 1),
        new Point(0, y - 1),
        new Point(0, 1),
        new Point(1, 1)
      };
      for (int index = 0; index < pointArray.Length; ++index)
        pointArray[index].Offset(nLeftEdge, nTopEdge);
      return pointArray;
    }

    private static Color Shade(Color SColor, int RED, int GREEN, int BLUE)
    {
      int r = (int) SColor.R;
      int g = (int) SColor.G;
      int b = (int) SColor.B;
      int red = r + RED;
      if (red > (int) byte.MaxValue)
        red = (int) byte.MaxValue;
      if (red < 0)
        red = 0;
      int green = g + GREEN;
      if (green > (int) byte.MaxValue)
        green = (int) byte.MaxValue;
      if (green < 0)
        green = 0;
      int blue = b + BLUE;
      if (blue > (int) byte.MaxValue)
        blue = (int) byte.MaxValue;
      if (blue < 0)
        blue = 0;
      return Color.FromArgb(red, green, blue);
    }

    private static Color Blend(Color SColor, Color DColor, int Percentage)
    {
      return Color.FromArgb((int) SColor.R + ((int) DColor.R - (int) SColor.R) * Percentage / 100, (int) SColor.G + ((int) DColor.G - (int) SColor.G) * Percentage / 100, (int) SColor.B + ((int) DColor.B - (int) SColor.B) * Percentage / 100);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.Capture = true;
      this.CapturingMouse = true;
      this.btnState = BtnState.Pushed;
      this.Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.btnState = !this.Enabled ? BtnState.Inactive : BtnState.Normal;
      this.Invalidate();
      this.CapturingMouse = false;
      this.Capture = false;
      this.Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      if (this.CapturingMouse)
        return;
      this.btnState = !this.Enabled ? BtnState.Inactive : BtnState.Normal;
      this.Invalidate();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.CapturingMouse)
      {
        Rectangle rectangle = new Rectangle(0, 0, this.Width, this.Height);
        this.btnState = BtnState.Normal;
        if (e.X >= rectangle.Left && e.X <= rectangle.Right && e.Y >= rectangle.Top && e.Y <= rectangle.Bottom)
          this.btnState = BtnState.Pushed;
        this.Capture = true;
        this.Invalidate();
      }
      else
      {
        if (this.btnState == BtnState.MouseOver)
          return;
        this.btnState = BtnState.MouseOver;
        this.Invalidate();
      }
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      if (this.Enabled)
      {
        this.btnState = BtnState.Normal;
      }
      else
      {
        this.btnState = BtnState.Inactive;
        this.CapturingMouse = false;
        this.Capture = false;
      }
      this.Invalidate();
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      if (this.Enabled)
        this.btnState = BtnState.Normal;
      this.Invalidate();
    }
  }
}
