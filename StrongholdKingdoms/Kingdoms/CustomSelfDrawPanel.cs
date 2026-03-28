// Decompiled with JetBrains decompiler
// Type: Kingdoms.CustomSelfDrawPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class CustomSelfDrawPanel : UserControl
  {
    public static Color MailBodyColor = Color.FromArgb(234, 245, 253);
    public static Color MailLineColor = Color.FromArgb(223, 237, 249);
    public static Color MailOverColor = Color.FromArgb(247, 252, 254);
    public static Color MailLineOverColor = Color.FromArgb(223, 237, 249);
    public static Color MailSelectedColor = Color.FromArgb(192, 222, 237);
    public static Color MailSelectedOverColor = Color.FromArgb(221, 241, 249);
    private Graphics storedGraphics;
    private bool inDXDraw;
    private bool inNormalDraw;
    private static Rectangle screenClipRect = new Rectangle();
    private Rectangle currentClip = Rectangle.Empty;
    private Stack<Rectangle> clipRectStack = new Stack<Rectangle>();
    private Stack<Region> clipStack = new Stack<Region>();
    public static CustomSelfDrawPanel.CSDControl StaticClickedControl = (CustomSelfDrawPanel.CSDControl) null;
    public CustomSelfDrawPanel.CSDControl ClickedControl;
    public CustomSelfDrawPanel.CSDControl OverControl;
    public CustomSelfDrawPanel.CSDControl baseControl = new CustomSelfDrawPanel.CSDControl();
    public bool ClickHandled;
    private static Point mousePosition = new Point();
    private static Point clickedPosition = new Point();
    private Point lastMousePosition = new Point();
    private bool mouseReallyPressed;
    private List<CustomSelfDrawPanel.CSDControl> trapMouseEvents = new List<CustomSelfDrawPanel.CSDControl>();
    private Point mouseDownLocation = new Point();
    public bool tooltipSet;
    private bool panelActive = true;
    private static List<CustomSelfDrawPanel.InvalidRectpair> invalidRectList = new List<CustomSelfDrawPanel.InvalidRectpair>();
    private bool selfDrawBackground;
    private bool noDrawBackground;
    private bool clickThru;
    private IContainer components;

    public CustomSelfDrawPanel()
    {
      this.InitializeComponent();
      this.MouseWheel += new MouseEventHandler(this.CustomSelfDrawPanel_MouseWheel);
    }

    public Graphics StoredGraphics
    {
      get => this.storedGraphics;
      set => this.storedGraphics = value;
    }

    public void forceStyle()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public bool initOnPaint(PaintEventArgs e)
    {
      if (this.inDXDraw)
        return false;
      this.inNormalDraw = true;
      this.StoredGraphics = e.Graphics;
      this.StoredGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      return true;
    }

    public bool initFromDX(Graphics g, CustomSelfDrawPanel.CSDControl control)
    {
      if (this.inNormalDraw)
        return false;
      this.inDXDraw = true;
      this.StoredGraphics = g;
      return true;
    }

    public void endPaint()
    {
      this.inNormalDraw = false;
      this.inDXDraw = false;
      this.StoredGraphics = (Graphics) null;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      try
      {
        if (!this.initOnPaint(e))
          return;
        CustomSelfDrawPanel.screenClipRect = e.ClipRectangle;
        this.drawControls();
        this.endPaint();
      }
      catch (Exception ex)
      {
      }
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      try
      {
        if (!this.selfDrawBackground)
        {
          if (this.noDrawBackground)
            return;
          base.OnPaintBackground(pevent);
        }
        else
        {
          if (this.Parent == null || this.Parent.BackgroundImage == null || pevent.Graphics == null)
            return;
          Rectangle rectangle = new Rectangle(0, 0, this.Parent.BackgroundImage.Width, this.Parent.BackgroundImage.Height);
          Rectangle destRect = new Rectangle(-this.Location.X, -this.Location.Y, this.Parent.Size.Width, this.Parent.Size.Height);
          ImageAttributes imageAttr = new ImageAttributes();
          imageAttr.SetWrapMode(WrapMode.Tile);
          pevent.Graphics.DrawImage(this.Parent.BackgroundImage, destRect, 0, 0, this.Parent.BackgroundImage.Width, this.Parent.BackgroundImage.Height, GraphicsUnit.Pixel, imageAttr);
        }
      }
      catch (Exception ex)
      {
      }
    }

    public void setClipRegion(Rectangle clipRect)
    {
      if (this.StoredGraphics == null)
        return;
      this.clipRectStack.Push(this.currentClip);
      this.currentClip = clipRect;
      this.clipStack.Push(this.StoredGraphics.Clip);
      Region region = this.StoredGraphics.Clip.Clone();
      region.Intersect(clipRect);
      this.StoredGraphics.Clip = region;
    }

    public void restoreClipRegion()
    {
      if (this.StoredGraphics == null)
        return;
      this.StoredGraphics.Clip = this.clipStack.Pop();
      this.currentClip = this.clipRectStack.Pop();
    }

    public Rectangle getCurrentClip()
    {
      return this.StoredGraphics != null ? this.currentClip : Rectangle.Empty;
    }

    public void clearControls() => this.baseControl.clearControls();

    public void addControl(CustomSelfDrawPanel.CSDControl control)
    {
      this.addControl(control, false);
    }

    public void addControl(CustomSelfDrawPanel.CSDControl control, bool addAtBack)
    {
      this.baseControl.setCustomSelfDrawPanel(this);
      if (addAtBack)
        this.baseControl.addControlAtBack(control);
      else
        this.baseControl.addControl(control);
    }

    public void removeControl(CustomSelfDrawPanel.CSDControl control)
    {
      this.baseControl.setCustomSelfDrawPanel(this);
      this.baseControl.removeControl(control);
    }

    public void drawControls() => this.baseControl.drawControls(new Point(0, 0));

    protected void drawImage(Image image, Point dest)
    {
      Rectangle srcRect = new Rectangle(0, 0, image.Width, image.Height);
      Rectangle destRect = new Rectangle(dest.X, dest.Y, image.Width, image.Height);
      if (this.StoredGraphics == null)
        return;
      this.StoredGraphics.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);
    }

    protected void drawImage(Image image, Point dest, float alpha)
    {
      Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);
      Rectangle destRect = new Rectangle(dest.X, dest.Y, image.Width, image.Height);
      if (this.StoredGraphics == null)
        return;
      this.StoredGraphics.DrawImage(image, destRect, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, this.createAlpha(alpha));
    }

    protected void drawImageMirrorRotate(
      Image image,
      Rectangle source,
      Rectangle dest,
      bool mirrored,
      float rotate,
      PointF rotateCentre)
    {
      if (this.StoredGraphics == null)
        return;
      RectangleF srcRect = new RectangleF((float) source.X, (float) source.Y, (float) source.Width, (float) source.Height);
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddPolygon(new Point[3]
      {
        new Point(0, 0),
        new Point(dest.Width, 0),
        new Point(0, dest.Height)
      });
      Matrix matrix = new Matrix();
      if (mirrored)
        matrix = new Matrix(-1f, 0.0f, 0.0f, 1f, 0.0f, 0.0f);
      if ((double) rotate != 0.0)
      {
        if ((double) rotateCentre.X == -1000.0)
          matrix.RotateAt(rotate, new PointF((float) (source.Width / 2), (float) (source.Height / 2)));
        else
          matrix.RotateAt(rotate, rotateCentre);
      }
      if (mirrored)
        matrix.Translate((float) (dest.X + source.Width), (float) dest.Y, MatrixOrder.Append);
      else
        matrix.Translate((float) dest.X, (float) dest.Y, MatrixOrder.Append);
      Matrix transform = this.StoredGraphics.Transform;
      graphicsPath.Transform(matrix);
      PointF[] pathPoints = graphicsPath.PathPoints;
      this.StoredGraphics.DrawImage(image, pathPoints, srcRect, GraphicsUnit.Pixel);
      this.StoredGraphics.Transform = transform;
    }

    protected void drawImageMirrorRotateAlpha(
      Image image,
      Rectangle source,
      Rectangle dest,
      bool mirrored,
      float rotate,
      PointF rotateCentre,
      float alpha)
    {
      if (this.StoredGraphics == null)
        return;
      RectangleF srcRect = new RectangleF((float) source.X, (float) source.Y, (float) source.Width, (float) source.Height);
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddPolygon(new Point[3]
      {
        new Point(0, 0),
        new Point(dest.Width, 0),
        new Point(0, dest.Height)
      });
      Matrix matrix = new Matrix();
      if (mirrored)
        matrix = new Matrix(-1f, 0.0f, 0.0f, 1f, 0.0f, 0.0f);
      if ((double) rotate != 0.0)
      {
        if ((double) rotateCentre.X == -1000.0)
          matrix.RotateAt(rotate, new PointF((float) (source.Width / 2), (float) (source.Height / 2)));
        else
          matrix.RotateAt(rotate, rotateCentre);
      }
      if (mirrored)
        matrix.Translate((float) (dest.X + source.Width), (float) dest.Y, MatrixOrder.Append);
      else
        matrix.Translate((float) dest.X, (float) dest.Y, MatrixOrder.Append);
      Matrix transform = this.StoredGraphics.Transform;
      graphicsPath.Transform(matrix);
      PointF[] pathPoints = graphicsPath.PathPoints;
      this.StoredGraphics.DrawImage(image, pathPoints, srcRect, GraphicsUnit.Pixel, this.createAlpha(alpha));
      this.StoredGraphics.Transform = transform;
    }

    protected void drawImage(Image image, Rectangle source, Rectangle dest)
    {
      if (this.StoredGraphics == null)
        return;
      this.StoredGraphics.DrawImage(image, dest, source, GraphicsUnit.Pixel);
    }

    protected void drawImageColourMap(
      Image image,
      Rectangle source,
      Rectangle dest,
      ColorMap[] colourMap)
    {
      if (this.StoredGraphics == null)
        return;
      ImageAttributes imageAttrs = new ImageAttributes();
      imageAttrs.SetRemapTable(colourMap);
      this.StoredGraphics.DrawImage(image, dest, (float) source.X, (float) source.Y, (float) source.Width, (float) source.Height, GraphicsUnit.Pixel, imageAttrs);
    }

    protected void drawImage(Image image, RectangleF source, RectangleF dest)
    {
      if (this.StoredGraphics == null)
        return;
      this.StoredGraphics.DrawImage(image, dest, source, GraphicsUnit.Pixel);
    }

    protected void drawImage(Image image, RectangleF source, RectangleF dest, float alpha)
    {
      PointF[] destPoints = new PointF[3]
      {
        new PointF(dest.Left, dest.Top),
        new PointF(dest.Right, dest.Top),
        new PointF(dest.Left, dest.Bottom)
      };
      if (this.StoredGraphics == null)
        return;
      this.StoredGraphics.DrawImage(image, destPoints, source, GraphicsUnit.Pixel, this.createAlpha(alpha));
    }

    protected void drawImage(Image image, Rectangle source, Rectangle dest, float alpha)
    {
      if (this.StoredGraphics == null)
        return;
      this.StoredGraphics.DrawImage(image, dest, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, this.createAlpha(alpha));
    }

    protected void drawImageBrighten(Image image, Rectangle source, Rectangle dest, float alpha)
    {
      if (this.StoredGraphics == null)
        return;
      this.StoredGraphics.DrawImage(image, dest, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, this.createAlphaBrighten(alpha));
    }

    protected void drawImage(
      Image image,
      Rectangle source,
      Rectangle dest,
      float alpha,
      double scale)
    {
      double width = (double) dest.Width * scale;
      double height = (double) dest.Height * scale;
      Rectangle destRect = new Rectangle(dest.X, dest.Y, (int) width, (int) height);
      if (this.StoredGraphics == null)
        return;
      this.StoredGraphics.DrawImage(image, destRect, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, this.createAlpha(alpha));
    }

    protected void drawImage(Image image, Point dest, float alpha, double scale)
    {
      double width = (double) image.Width * scale;
      double height = (double) image.Height * scale;
      Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);
      Rectangle destRect = new Rectangle(dest.X, dest.Y, (int) width, (int) height);
      if (this.StoredGraphics == null)
        return;
      this.StoredGraphics.DrawImage(image, destRect, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, this.createAlpha(alpha));
    }

    protected void drawImage(
      Image image,
      RectangleF source,
      RectangleF dest,
      float alpha,
      Color color)
    {
      PointF[] destPoints = new PointF[3]
      {
        new PointF(dest.Left, dest.Top),
        new PointF(dest.Right, dest.Top),
        new PointF(dest.Left, dest.Bottom)
      };
      if (this.StoredGraphics == null)
        return;
      this.StoredGraphics.DrawImage(image, destPoints, source, GraphicsUnit.Pixel, this.createColour(color, alpha));
    }

    protected void drawImage(
      Image image,
      Rectangle source,
      Rectangle dest,
      float alpha,
      Color color)
    {
      if (this.StoredGraphics == null)
        return;
      this.StoredGraphics.DrawImage(image, dest, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, this.createColour(color, alpha));
    }

    protected void drawImage(
      Image image,
      Rectangle source,
      Rectangle dest,
      float alpha,
      double scale,
      Color color)
    {
      double width = (double) dest.Width * scale;
      double height = (double) dest.Height * scale;
      Rectangle destRect = new Rectangle(dest.X, dest.Y, (int) width, (int) height);
      if (this.StoredGraphics == null)
        return;
      this.StoredGraphics.DrawImage(image, destRect, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, this.createColour(color, alpha));
    }

    protected void drawImage(Image image, Point dest, float alpha, double scale, Color color)
    {
      double width = (double) image.Width * scale;
      double height = (double) image.Height * scale;
      Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);
      Rectangle destRect = new Rectangle(dest.X, dest.Y, (int) width, (int) height);
      if (this.StoredGraphics == null)
        return;
      this.StoredGraphics.DrawImage(image, destRect, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, this.createColour(color, alpha));
    }

    protected void drawImage(Image image, Rectangle source, Rectangle dest, double scale)
    {
      double width = (double) dest.Width * scale;
      double height = (double) dest.Height * scale;
      Rectangle destRect = new Rectangle(dest.X, dest.Y, (int) width, (int) height);
      if (this.StoredGraphics == null)
        return;
      this.StoredGraphics.DrawImage(image, destRect, source, GraphicsUnit.Pixel);
    }

    protected void drawSpecialGradient(Rectangle rect)
    {
      if (this.StoredGraphics == null)
        return;
      CustomSelfDrawPanel.drawGradientPanel(this.StoredGraphics, rect.Location, rect.Size);
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

    private ImageAttributes createAlphaBrighten(float alpha)
    {
      ColorMatrix newColorMatrix = new ColorMatrix();
      newColorMatrix.Matrix00 = newColorMatrix.Matrix11 = newColorMatrix.Matrix22 = newColorMatrix.Matrix44 = 1f;
      newColorMatrix.Matrix30 = newColorMatrix.Matrix31 = newColorMatrix.Matrix32 = 0.1f;
      newColorMatrix.Matrix33 = alpha;
      ImageAttributes alphaBrighten = new ImageAttributes();
      alphaBrighten.SetColorMatrix(newColorMatrix);
      return alphaBrighten;
    }

    private ImageAttributes createColour(Color color, float alpha)
    {
      ColorMatrix newColorMatrix = new ColorMatrix();
      newColorMatrix.Matrix00 = (float) color.R / (float) byte.MaxValue;
      newColorMatrix.Matrix11 = (float) color.G / (float) byte.MaxValue;
      newColorMatrix.Matrix22 = (float) color.B / (float) byte.MaxValue;
      newColorMatrix.Matrix44 = 1f;
      newColorMatrix.Matrix33 = alpha;
      ImageAttributes colour = new ImageAttributes();
      colour.SetColorMatrix(newColorMatrix);
      return colour;
    }

    protected void drawRect(Rectangle area, Color col)
    {
      if (this.StoredGraphics == null)
        return;
      Pen pen = new Pen(col);
      this.StoredGraphics.DrawRectangle(pen, area);
      pen.Dispose();
    }

    protected void drawLine(Color col, Point start, Point end)
    {
      if (this.StoredGraphics == null)
        return;
      Pen pen = new Pen(col);
      this.StoredGraphics.DrawLine(pen, start, end);
      pen.Dispose();
    }

    protected void drawString(
      string text,
      Rectangle displayRect,
      Color color,
      Font font,
      CustomSelfDrawPanel.CSD_Text_Alignment alignment)
    {
      SolidBrush solidBrush = new SolidBrush(color);
      StringFormat format = new StringFormat();
      switch (alignment)
      {
        case CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT:
          format.Alignment = StringAlignment.Near;
          format.LineAlignment = StringAlignment.Near;
          break;
        case CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER:
          format.Alignment = StringAlignment.Center;
          format.LineAlignment = StringAlignment.Near;
          break;
        case CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT:
          format.Alignment = StringAlignment.Far;
          format.LineAlignment = StringAlignment.Near;
          break;
        case CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT:
          format.Alignment = StringAlignment.Near;
          format.LineAlignment = StringAlignment.Center;
          break;
        case CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER:
          format.Alignment = StringAlignment.Center;
          format.LineAlignment = StringAlignment.Center;
          break;
        case CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT:
          format.Alignment = StringAlignment.Far;
          format.LineAlignment = StringAlignment.Center;
          break;
        case CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_LEFT:
          format.Alignment = StringAlignment.Near;
          format.LineAlignment = StringAlignment.Far;
          break;
        case CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER:
          format.Alignment = StringAlignment.Center;
          format.LineAlignment = StringAlignment.Far;
          break;
        case CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT:
          format.Alignment = StringAlignment.Far;
          format.LineAlignment = StringAlignment.Far;
          break;
      }
      RectangleF layoutRectangle = new RectangleF((float) displayRect.X, (float) displayRect.Y, (float) displayRect.Width, (float) displayRect.Height);
      if (this.StoredGraphics != null)
        this.StoredGraphics.DrawString(text, font, (Brush) solidBrush, layoutRectangle, format);
      solidBrush.Dispose();
    }

    protected SizeF getStringBounds(string text, int displayWidth, Font font)
    {
      return this.StoredGraphics != null ? this.StoredGraphics.MeasureString(text, font, displayWidth) : new SizeF();
    }

    private void fillRect(Rectangle fillArea, Color fillColor)
    {
      SolidBrush solidBrush = new SolidBrush(fillColor);
      if (this.StoredGraphics != null)
        this.StoredGraphics.FillRectangle((Brush) solidBrush, fillArea);
      solidBrush.Dispose();
    }

    private void CustomSelfDrawPanel_MouseClick(object sender, MouseEventArgs e)
    {
      if (!this.panelActive)
        return;
      if (e.Button == MouseButtons.Left)
      {
        Point location = e.Location;
        this.lastMousePosition = location;
        if (Math.Abs(this.lastMousePosition.X - this.mouseDownLocation.X) > 4 || Math.Abs(this.lastMousePosition.Y - this.mouseDownLocation.Y) > 4)
          return;
        this.ClickHandled = this.baseControl.parentClicked(location);
      }
      else
      {
        if (e.Button != MouseButtons.Right)
          return;
        Point location = e.Location;
        this.lastMousePosition = location;
        if (Math.Abs(this.lastMousePosition.X - this.mouseDownLocation.X) > 4 || Math.Abs(this.lastMousePosition.Y - this.mouseDownLocation.Y) > 4)
          return;
        this.baseControl.parentRightClicked(location);
      }
    }

    private void CustomSelfDrawPanel_MouseEnter(object sender, EventArgs e)
    {
      if (this.panelActive)
        ;
    }

    private void CustomSelfDrawPanel_MouseLeave(object sender, EventArgs e)
    {
      if (!this.panelActive)
        return;
      CustomTooltipManager.MouseLeaveTooltipArea();
      this.baseControl.handleMouseLeave((CustomSelfDrawPanel.CSDControl) null);
    }

    public static Point GetMousePosition() => CustomSelfDrawPanel.mousePosition;

    public Point LastMousePosition => this.lastMousePosition;

    public bool MouseReallyPressed => this.mouseReallyPressed;

    public void addTrapMouseEvent(CustomSelfDrawPanel.CSDControl control)
    {
      if (this.trapMouseEvents.Contains(control))
        return;
      this.trapMouseEvents.Add(control);
    }

    public void removeTrapMouseEvent(CustomSelfDrawPanel.CSDControl control)
    {
      this.trapMouseEvents.Remove(control);
    }

    public void clearTrappedMouseEvents()
    {
      foreach (CustomSelfDrawPanel.CSDControl trapMouseEvent in this.trapMouseEvents)
      {
        UniversalDebugLog.Log(trapMouseEvent.GetType().ToString() + " onclear mouseup");
        UniversalDebugLog.Log("Stop here");
        trapMouseEvent.mouseUpOutside();
      }
      this.trapMouseEvents.Clear();
    }

    public void manageTrappedMouseEvents()
    {
      List<CustomSelfDrawPanel.CSDControl> csdControlList = (List<CustomSelfDrawPanel.CSDControl>) null;
      bool flag = true;
      while (flag)
      {
        flag = false;
        try
        {
          foreach (CustomSelfDrawPanel.CSDControl trapMouseEvent in this.trapMouseEvents)
          {
            if (trapMouseEvent.Visible)
            {
              trapMouseEvent.mouseEventTrapped();
            }
            else
            {
              if (csdControlList == null)
                csdControlList = new List<CustomSelfDrawPanel.CSDControl>();
              csdControlList.Add(trapMouseEvent);
            }
          }
        }
        catch (Exception ex)
        {
          flag = true;
        }
      }
      if (csdControlList == null)
        return;
      foreach (CustomSelfDrawPanel.CSDControl csdControl in csdControlList)
        this.trapMouseEvents.Remove(csdControl);
    }

    private void CustomSelfDrawPanel_MouseDown(object sender, MouseEventArgs e)
    {
      if (!this.panelActive)
        return;
      if (e.Button == MouseButtons.Left)
      {
        Point location = e.Location;
        this.mouseReallyPressed = true;
        this.lastMousePosition = location;
        this.mouseDownLocation = location;
        this.baseControl.parentMouseDown(location);
      }
      if (e.Button != MouseButtons.Right)
        return;
      Point location1 = e.Location;
      this.lastMousePosition = location1;
      this.mouseDownLocation = location1;
    }

    private void CustomSelfDrawPanel_MouseUp(object sender, MouseEventArgs e)
    {
      if (!this.panelActive)
        return;
      if (e.Button == MouseButtons.Left)
      {
        Point location = e.Location;
        this.mouseReallyPressed = false;
        this.lastMousePosition = location;
        this.baseControl.parentMouseUp(location);
        this.manageTrappedMouseEvents();
      }
      if (e.Button != MouseButtons.Right)
        return;
      this.lastMousePosition = e.Location;
    }

    private void CustomSelfDrawPanel_MouseMove(object sender, MouseEventArgs e)
    {
      if (!this.panelActive)
        return;
      CustomTooltipManager.MouseLeaveTooltipArea();
      this.tooltipSet = false;
      Point location = e.Location;
      this.lastMousePosition = location;
      if (this.baseControl.parentMouseOver(location) == null)
        this.baseControl.handleMouseLeave((CustomSelfDrawPanel.CSDControl) null);
      this.manageTrappedMouseEvents();
    }

    private void CustomSelfDrawPanel_MouseWheel(object sender, MouseEventArgs e)
    {
      if (!this.panelActive)
        return;
      int delta = e.Delta / SystemInformation.MouseWheelScrollDelta;
      Point location = e.Location;
      this.lastMousePosition = location;
      this.baseControl.parentMouseWheel(location, delta);
    }

    protected void handleMouseLeave(CustomSelfDrawPanel.CSDControl control)
    {
      this.baseControl.handleMouseLeave(control);
    }

    public bool getToolTip(ref int data)
    {
      return this.baseControl.getToolTip(this.lastMousePosition, ref data);
    }

    public bool PanelActive
    {
      get => this.panelActive;
      set => this.panelActive = value;
    }

    public void InvalidateCached(Rectangle rect)
    {
      CustomSelfDrawPanel.invalidRectList.Add(new CustomSelfDrawPanel.InvalidRectpair()
      {
        rect = rect,
        panel = this
      });
    }

    public static void processInvalidRectCache()
    {
      if (CustomSelfDrawPanel.invalidRectList.Count <= 0)
        return;
      foreach (CustomSelfDrawPanel.InvalidRectpair invalidRect in CustomSelfDrawPanel.invalidRectList)
        invalidRect.panel.Invalidate(invalidRect.rect);
      CustomSelfDrawPanel.invalidRectList.Clear();
    }

    public static void drawGradientPanel(Graphics mGraphics, Size size)
    {
      CustomSelfDrawPanel.drawGradientPanel(mGraphics, new Point(), size);
    }

    public static void drawGradientPanel(Graphics mGraphics, Point pos, Size size)
    {
      Rectangle rect1 = new Rectangle(pos.X, pos.Y, size.Width, size.Height);
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect1, Color.FromArgb(86, 98, 106), Color.FromArgb(159, 180, 193), LinearGradientMode.Vertical);
      mGraphics.FillRectangle((Brush) linearGradientBrush, rect1);
      Pen pen1 = new Pen(Color.FromArgb(159, 180, 193), 1f);
      Rectangle rect2 = new Rectangle(pos.X, pos.Y, size.Width - 1, size.Height - 1);
      mGraphics.DrawRectangle(pen1, rect2);
      Pen pen2 = new Pen(Color.FromArgb(86, 98, 106), 1f);
      Rectangle rect3 = new Rectangle(pos.X + 1, pos.Y, size.Width - 3, size.Height - 2);
      mGraphics.DrawRectangle(pen2, rect3);
      pen1.Dispose();
      pen2.Dispose();
      linearGradientBrush.Dispose();
    }

    public bool SelfDrawBackground
    {
      get => this.selfDrawBackground;
      set => this.selfDrawBackground = value;
    }

    public bool NoDrawBackground
    {
      get => this.noDrawBackground;
      set => this.noDrawBackground = value;
    }

    public bool ClickThru
    {
      get => this.clickThru;
      set => this.clickThru = value;
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 132 && this.clickThru)
        m.Result = (IntPtr) (-1);
      else
        base.WndProc(ref m);
    }

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
      this.Name = nameof (CustomSelfDrawPanel);
      this.MouseLeave += new EventHandler(this.CustomSelfDrawPanel_MouseLeave);
      this.MouseMove += new MouseEventHandler(this.CustomSelfDrawPanel_MouseMove);
      this.MouseDoubleClick += new MouseEventHandler(this.CustomSelfDrawPanel_MouseClick);
      this.MouseClick += new MouseEventHandler(this.CustomSelfDrawPanel_MouseClick);
      this.MouseDown += new MouseEventHandler(this.CustomSelfDrawPanel_MouseDown);
      this.MouseUp += new MouseEventHandler(this.CustomSelfDrawPanel_MouseUp);
      this.MouseEnter += new EventHandler(this.CustomSelfDrawPanel_MouseEnter);
      this.ResumeLayout(false);
    }

    public class CSDListItem
    {
      private string text = "";
      private long data;

      public string Text
      {
        set => this.text = value;
        get => this.text;
      }

      public long DataL
      {
        set => this.data = value;
        get => this.data;
      }

      public int Data
      {
        set => this.data = (long) value;
        get => (int) this.data;
      }
    }

    public class CSDControl
    {
      public float Rotate;
      private PointF rotateCentre = (PointF) new Point(-1000, -1000);
      private bool rotationCentreSet;
      private Point position = new Point(0, 0);
      private Rectangle rect = new Rectangle(0, 0, 1, 1);
      private bool visible = true;
      private bool enabled = true;
      private Size size = new Size(1, 1);
      private Rectangle clipRect = Rectangle.Empty;
      private bool clipVisible;
      private Rectangle clickArea = Rectangle.Empty;
      private double scale = 1.0;
      private int customTooltipID;
      private int customTooltipData;
      private bool customTooltipWasOver;
      private bool tooltipActive;
      private int tooltip;
      private string soundTag = "";
      protected object tag;
      protected int dataValue;
      protected long dataValueL;
      protected object dataValueObject;
      private Point lastRelativeMousePos = new Point();
      private CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate clickDelegate;
      private CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate rightClickDelegate;
      protected bool mouseOverFlag;
      protected CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate mouseOverDelegate;
      protected CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate mouseLeaveDelegate;
      protected bool mouseDownFlag;
      protected CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate mouseDownDelegate;
      protected CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate mouseUpDelegate;
      public bool clickThrough;
      protected CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate mouseWheelDelegate;
      public List<CustomSelfDrawPanel.CSDControl> csdControls = new List<CustomSelfDrawPanel.CSDControl>();
      protected CustomSelfDrawPanel.CSDControl parent;
      protected CustomSelfDrawPanel m_csd;

      public PointF RotateCentre
      {
        get => this.rotateCentre;
        set
        {
          this.rotateCentre = value;
          this.rotationCentreSet = true;
        }
      }

      public Rectangle Rectangle => this.rect;

      public virtual Point Position
      {
        get => this.position;
        set
        {
          this.position = value;
          this.rect.Location = value;
        }
      }

      public int X
      {
        get => this.position.X;
        set
        {
          this.position.X = value;
          this.rect.X = value;
        }
      }

      public int Y
      {
        get => this.position.Y;
        set
        {
          this.position.Y = value;
          this.rect.Y = value;
        }
      }

      public bool Visible
      {
        get => this.visible;
        set
        {
          if (this.visible != value)
            this.invalidate();
          this.visible = value;
        }
      }

      public virtual bool Enabled
      {
        get => this.enabled;
        set
        {
          if (this.enabled != value)
            this.invalidate();
          this.enabled = value;
        }
      }

      public virtual Size Size
      {
        get => this.size;
        set
        {
          this.size = value;
          this.rect.Size = value;
        }
      }

      public int Width
      {
        get => this.size.Width;
        set
        {
          this.size.Width = value;
          this.rect.Width = value;
        }
      }

      public int Height
      {
        get => this.size.Height;
        set
        {
          this.size.Height = value;
          this.rect.Height = value;
        }
      }

      public Rectangle ClipRect
      {
        get => this.clipRect;
        set => this.clipRect = value;
      }

      public virtual bool ClipVisible
      {
        get => this.clipVisible;
        set => this.clipVisible = value;
      }

      public Rectangle ClickArea
      {
        get => this.clickArea;
        set => this.clickArea = value;
      }

      public double Scale
      {
        get => this.scale;
        set => this.scale = value;
      }

      public int CustomTooltipID
      {
        get => this.customTooltipID;
        set => this.customTooltipID = value;
      }

      public int CustomTooltipData
      {
        get => this.customTooltipData;
        set => this.customTooltipData = value;
      }

      public int Tooltip
      {
        get => this.tooltip;
        set
        {
          this.tooltip = value;
          this.tooltipActive = true;
        }
      }

      public bool getToolTip(Point mousePos, ref int data)
      {
        bool toolTip = false;
        if (this.Visible && this.Enabled)
        {
          mousePos = new Point((int) ((double) mousePos.X / this.Scale), (int) ((double) mousePos.Y / this.Scale));
          Point point = new Point(mousePos.X - this.X, mousePos.Y - this.Y);
          if (!this.ClipRect.IsEmpty && !this.ClipRect.Contains(point))
            return false;
          foreach (CustomSelfDrawPanel.CSDControl csdControl in this.csdControls)
          {
            if (csdControl.getToolTip(point, ref data))
              toolTip = true;
          }
          if (this.tooltipActive && this.mouseOver(mousePos))
          {
            toolTip = true;
            data = this.tooltip;
          }
        }
        return toolTip;
      }

      public string sTag
      {
        get => this.soundTag;
        set => this.soundTag = value;
      }

      public object Tag
      {
        get => this.tag;
        set => this.tag = value;
      }

      public int Data
      {
        get => this.dataValue;
        set => this.dataValue = value;
      }

      public long DataL
      {
        get => this.dataValueL;
        set => this.dataValueL = value;
      }

      public object dataObject
      {
        get => this.dataValueObject;
        set => this.dataValueObject = value;
      }

      public void setChildrensScale(double scale)
      {
        foreach (CustomSelfDrawPanel.CSDControl csdControl in this.csdControls)
          csdControl.setScale(scale);
      }

      public void setScale(double scale)
      {
        this.Scale = scale;
        foreach (CustomSelfDrawPanel.CSDControl csdControl in this.csdControls)
          csdControl.setScale(scale);
      }

      public Point LastRelativeMousePos => this.lastRelativeMousePos;

      public void setClickDelegate(
        CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate newDelegate)
      {
        this.clickDelegate = newDelegate;
      }

      public void setClickDelegate(
        CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate newDelegate,
        string tag)
      {
        this.clickDelegate = newDelegate;
        this.sTag = tag;
      }

      public void setRightClickDelegate(
        CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate newDelegate)
      {
        this.rightClickDelegate = newDelegate;
      }

      public bool mouseOver(Point mousePos)
      {
        Rectangle rect = this.rect;
        if (this.Scale != 1.0)
        {
          rect.Width = (int) ((double) rect.Width * this.Scale);
          rect.Height = (int) ((double) rect.Height * this.Scale);
        }
        if (!this.clickArea.IsEmpty)
        {
          rect.Width = this.clickArea.Width;
          rect.Height = this.clickArea.Height;
          rect.X += this.clickArea.X;
          rect.Y += this.clickArea.Y;
        }
        return rect.Contains(mousePos);
      }

      public bool parentClicked(Point mousePos)
      {
        if (this.Visible && this.Enabled)
        {
          Point point = new Point(mousePos.X - this.X, mousePos.Y - this.Y);
          if (!this.ClipRect.IsEmpty && !this.ClipRect.Contains(point))
            return false;
          for (int index = this.csdControls.Count - 1; index >= 0; --index)
          {
            if (this.csdControls[index].parentClicked(point))
              return true;
          }
          if (this.clickDelegate != null && this.mouseOver(mousePos))
          {
            this.csd.ClickedControl = this;
            CustomSelfDrawPanel.StaticClickedControl = this;
            this.lastRelativeMousePos = mousePos;
            if (this.sTag.Length > 0)
              GameEngine.Instance.playInterfaceSound(this.sTag);
            this.clickDelegate();
            return true;
          }
        }
        return false;
      }

      public bool parentRightClicked(Point mousePos)
      {
        if (this.Visible && this.Enabled)
        {
          Point point = new Point(mousePos.X - this.X, mousePos.Y - this.Y);
          if (!this.ClipRect.IsEmpty && !this.ClipRect.Contains(point))
            return false;
          for (int index = this.csdControls.Count - 1; index >= 0; --index)
          {
            if (this.csdControls[index].parentRightClicked(point))
              return true;
          }
          if (this.rightClickDelegate != null && this.mouseOver(mousePos))
          {
            this.csd.ClickedControl = this;
            this.lastRelativeMousePos = mousePos;
            if (this.sTag.Length > 0)
              GameEngine.Instance.playInterfaceSound(this.sTag);
            this.rightClickDelegate();
            return true;
          }
        }
        return false;
      }

      public void setMouseOverDelegate(
        CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate overDelegate,
        CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate leaveDelegate)
      {
        this.mouseOverDelegate = overDelegate;
        this.mouseLeaveDelegate = leaveDelegate;
      }

      public CustomSelfDrawPanel.CSDControl parentMouseOver(Point mousePos)
      {
        CustomSelfDrawPanel.CSDControl csdControl = (CustomSelfDrawPanel.CSDControl) null;
        if (this.Visible && this.Enabled)
        {
          Point point = new Point(mousePos.X - this.X, mousePos.Y - this.Y);
          if (!this.ClipRect.IsEmpty && !this.ClipRect.Contains(point))
            return (CustomSelfDrawPanel.CSDControl) null;
          for (int index = this.csdControls.Count - 1; index >= 0; --index)
          {
            csdControl = this.csdControls[index].parentMouseOver(point);
            if (csdControl != null)
              return csdControl;
          }
          if (this.customTooltipID != 0)
          {
            if (this.mouseOver(mousePos))
            {
              if (!this.csd.tooltipSet)
              {
                this.csd.tooltipSet = true;
                CustomTooltipManager.MouseEnterTooltipArea(this.customTooltipID, this.customTooltipData, this.csd.FindForm());
                this.customTooltipWasOver = true;
              }
            }
            else if (this.customTooltipWasOver)
            {
              this.customTooltipWasOver = false;
              CustomTooltipManager.MouseLeaveTooltipArea();
            }
          }
          if (this.mouseOverDelegate != null && this.mouseOver(mousePos))
          {
            if (this.csd.OverControl != this)
            {
              this.csd.handleMouseLeave(this);
              this.csd.OverControl = this;
            }
            if (!this.mouseOverFlag)
            {
              this.mouseOverFlag = true;
              this.lastRelativeMousePos = mousePos;
              this.mouseOverDelegate();
            }
            return this;
          }
        }
        return csdControl;
      }

      public void handleMouseLeave(CustomSelfDrawPanel.CSDControl overControl)
      {
        foreach (CustomSelfDrawPanel.CSDControl csdControl in this.csdControls)
          csdControl.handleMouseLeave(overControl);
        if (!this.mouseOverFlag || this == overControl)
          return;
        this.mouseOverFlag = false;
        if (this.mouseDownFlag)
        {
          this.mouseDownFlag = false;
          if (this.mouseUpDelegate != null)
            this.mouseUpDelegate();
        }
        if (this.mouseLeaveDelegate != null)
          this.mouseLeaveDelegate();
        this.csd.OverControl = (CustomSelfDrawPanel.CSDControl) null;
      }

      public virtual void mouseEventTrapped()
      {
      }

      public bool MouseDownFlag
      {
        get => this.mouseDownFlag;
        set => this.mouseDownFlag = value;
      }

      public void setMouseDownDelegate(
        CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate downDelegate,
        CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate upDelegate)
      {
        this.mouseDownDelegate = downDelegate;
        this.mouseUpDelegate = upDelegate;
      }

      public CustomSelfDrawPanel.CSDControl parentMouseDown(Point mousePos)
      {
        CustomSelfDrawPanel.CSDControl csdControl = (CustomSelfDrawPanel.CSDControl) null;
        if (this.Visible && this.Enabled)
        {
          Point point = new Point(mousePos.X - this.X, mousePos.Y - this.Y);
          if (!this.ClipRect.IsEmpty && !this.ClipRect.Contains(point))
            return (CustomSelfDrawPanel.CSDControl) null;
          for (int index = this.csdControls.Count - 1; index >= 0; --index)
          {
            csdControl = this.csdControls[index].parentMouseDown(point);
            if (csdControl != null)
              return csdControl;
          }
          if (this.mouseDownDelegate != null)
          {
            if (this.mouseOver(mousePos))
            {
              CustomSelfDrawPanel.StaticClickedControl = this;
              if (!this.mouseDownFlag)
              {
                this.mouseDownFlag = true;
                this.lastRelativeMousePos = mousePos;
                this.mouseDownDelegate();
              }
              if (!this.clickThrough)
                return this;
            }
            else
              this.mouseDownFlag = false;
          }
        }
        return csdControl;
      }

      public virtual void mouseUpOutside()
      {
      }

      public CustomSelfDrawPanel.CSDControl parentMouseUp(Point mousePos)
      {
        CustomSelfDrawPanel.CSDControl csdControl = (CustomSelfDrawPanel.CSDControl) null;
        if (this.Visible)
        {
          Point point = new Point(mousePos.X - this.X, mousePos.Y - this.Y);
          if (!this.ClipRect.IsEmpty && !this.ClipRect.Contains(point))
            return (CustomSelfDrawPanel.CSDControl) null;
          for (int index = this.csdControls.Count - 1; index >= 0; --index)
          {
            csdControl = this.csdControls[index].parentMouseUp(point);
            if (csdControl != null)
              return csdControl;
          }
          if (this.mouseUpDelegate != null && this.mouseOver(mousePos))
          {
            if (this.mouseDownFlag)
            {
              this.mouseDownFlag = false;
              this.lastRelativeMousePos = mousePos;
              this.mouseUpDelegate();
            }
            return this;
          }
        }
        return csdControl;
      }

      public void setMouseWheelDelegate(
        CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate newDelegate)
      {
        this.mouseWheelDelegate = newDelegate;
      }

      public CustomSelfDrawPanel.CSDControl parentMouseWheel(Point mousePos, int delta)
      {
        CustomSelfDrawPanel.CSDControl csdControl = (CustomSelfDrawPanel.CSDControl) null;
        if (this.Visible && this.Enabled)
        {
          Point point = new Point(mousePos.X - this.X, mousePos.Y - this.Y);
          if (!this.ClipRect.IsEmpty && !this.ClipRect.Contains(point))
            return (CustomSelfDrawPanel.CSDControl) null;
          for (int index = this.csdControls.Count - 1; index >= 0; --index)
          {
            csdControl = this.csdControls[index].parentMouseWheel(point, delta);
            if (csdControl != null)
              return csdControl;
          }
          if (this.mouseWheelDelegate != null && this.mouseOver(mousePos))
          {
            this.lastRelativeMousePos = mousePos;
            this.mouseWheelDelegate(delta);
            return this;
          }
        }
        return csdControl;
      }

      public void setCustomSelfDrawPanel(CustomSelfDrawPanel newCSD) => this.m_csd = newCSD;

      public CustomSelfDrawPanel.CSDControl Parent => this.parent;

      public CustomSelfDrawPanel csd
      {
        get
        {
          if (this.m_csd != null)
            return this.m_csd;
          if (this.parent == null)
            return (CustomSelfDrawPanel) null;
          this.m_csd = this.parent.csd;
          return this.m_csd;
        }
      }

      public void setParent(CustomSelfDrawPanel.CSDControl newParent) => this.parent = newParent;

      public virtual void clearControls()
      {
        for (int index = 0; index < this.csdControls.Count; ++index)
        {
          this.csdControls[index].clearControls();
          this.csdControls[index] = (CustomSelfDrawPanel.CSDControl) null;
        }
        this.csdControls.Clear();
        this.onClear();
      }

      public virtual void onClear()
      {
      }

      public void clearDirectControlsOnly() => this.csdControls.Clear();

      public void addControl(CustomSelfDrawPanel.CSDControl control)
      {
        if (control.parent != null && control.parent.csdControls.Contains(control))
          control.parent.removeControl(control);
        control.m_csd = this.csd;
        control.parent = this;
        this.csdControls.Add(control);
        control.addedToParent();
      }

      public void addControlAtBack(CustomSelfDrawPanel.CSDControl control)
      {
        if (control.parent != null && control.parent.csdControls.Contains(control))
          control.parent.removeControl(control);
        control.m_csd = this.csd;
        control.parent = this;
        this.csdControls.Insert(0, control);
        control.addedToParent();
      }

      public List<CustomSelfDrawPanel.CSDControl> Controls => this.csdControls;

      public virtual void addedToParent()
      {
      }

      public void removeControl(CustomSelfDrawPanel.CSDControl control)
      {
        if (control == null)
          return;
        this.csdControls.Remove(control);
        control.parent = (CustomSelfDrawPanel.CSDControl) null;
      }

      public void invalidate()
      {
        if (this.csd == null)
          return;
        Point point = new Point(this.X, this.Y);
        for (CustomSelfDrawPanel.CSDControl parent = this.parent; parent != null; parent = parent.parent)
        {
          point.X += parent.X;
          point.Y += parent.Y;
        }
        Rectangle rectangle = new Rectangle(point.X, point.Y, this.Width, this.Height);
        if (!this.csd.inNormalDraw)
          this.csd.Invalidate(rectangle);
        else
          this.csd.InvalidateCached(rectangle);
      }

      public void invalidateXtra()
      {
        if (this.csd == null)
          return;
        Point point = new Point(this.X, this.Y);
        for (CustomSelfDrawPanel.CSDControl parent = this.parent; parent != null; parent = parent.parent)
        {
          point.X += parent.X;
          point.Y += parent.Y;
        }
        this.csd.Invalidate(new Rectangle(point.X - 10, point.Y - 10, this.Width + 20, this.Height + 20));
      }

      public Point getPanelPosition()
      {
        Point panelPosition = new Point(this.X, this.Y);
        for (CustomSelfDrawPanel.CSDControl parent = this.parent; parent != null; parent = parent.parent)
        {
          panelPosition.X += parent.X;
          panelPosition.Y += parent.Y;
        }
        return panelPosition;
      }

      public void drawControls(Point parentLocation)
      {
        if (!this.Visible)
          return;
        if (this.clipVisible)
        {
          Rectangle currentClip = this.csd.getCurrentClip();
          if (currentClip != Rectangle.Empty)
          {
            Point location = this.Scale != 1.0 ? new Point(parentLocation.X + (int) ((double) this.X * this.scale), parentLocation.Y + (int) ((double) this.Y * this.scale)) : new Point(parentLocation.X + this.X, parentLocation.Y + this.Y);
            if (Rectangle.Intersect(currentClip, new Rectangle(location, this.Size)) == Rectangle.Empty || Rectangle.Intersect(CustomSelfDrawPanel.screenClipRect, new Rectangle(location, this.Size)) == Rectangle.Empty)
              return;
          }
        }
        if (this.clipRect != Rectangle.Empty)
          this.csd.setClipRegion(new Rectangle(this.clipRect.X + parentLocation.X + this.X, this.clipRect.Y + parentLocation.Y + this.Y, this.clipRect.Width, this.clipRect.Height));
        this.draw(parentLocation);
        Point parentLocation1 = this.Scale != 1.0 ? new Point(parentLocation.X + (int) ((double) this.X * this.scale), parentLocation.Y + (int) ((double) this.Y * this.scale)) : new Point(parentLocation.X + this.X, parentLocation.Y + this.Y);
        foreach (CustomSelfDrawPanel.CSDControl csdControl in this.csdControls)
          csdControl.drawControls(parentLocation1);
        if (!(this.clipRect != Rectangle.Empty))
          return;
        this.csd.restoreClipRegion();
      }

      public virtual void draw(Point parentLocation)
      {
      }

      public virtual void unityOverridableUpdate(Point parentLocation)
      {
      }

      public void fitContents()
      {
        foreach (CustomSelfDrawPanel.CSDControl control in this.Controls)
        {
          this.Width = Math.Max(this.Width, control.Rectangle.Right);
          this.Height = Math.Max(this.Height, control.Rectangle.Bottom);
        }
      }

      public delegate void CSD_ValueChangedDelegate();

      public delegate void CSD_ScrollBarChangedDelegate();

      public delegate void CSD_ClickDelegate();

      public delegate void CSD_MouseOverDelegate();

      public delegate void CSD_MouseDownDelegate();

      public delegate void CSD_MouseWheelDelegate(int delta);
    }

    private class CSDListEntry : CustomSelfDrawPanel.CSDControl
    {
      private bool setupComplete;
      private CustomSelfDrawPanel.CSDFill main = new CustomSelfDrawPanel.CSDFill();
      private CustomSelfDrawPanel.CSDFill line = new CustomSelfDrawPanel.CSDFill();
      private CustomSelfDrawPanel.CSDLabel textLabel = new CustomSelfDrawPanel.CSDLabel();
      private Color bodyColor = CustomSelfDrawPanel.MailBodyColor;
      private Color lineColor = CustomSelfDrawPanel.MailLineColor;
      private Color overColor = CustomSelfDrawPanel.MailOverColor;
      private Color lineOverColor = CustomSelfDrawPanel.MailLineOverColor;

      public Color BodyColor
      {
        set
        {
          if (this.setupComplete)
          {
            if (this.bodyColor != value)
              this.main.invalidate();
            this.main.FillColor = value;
          }
          this.bodyColor = value;
        }
      }

      public Color LineColor
      {
        set
        {
          if (this.setupComplete)
          {
            if (this.lineColor != value)
              this.line.invalidate();
            this.line.FillColor = value;
          }
          this.lineColor = value;
        }
      }

      public Color OverColor
      {
        set => this.overColor = value;
      }

      public Color LineOverColor
      {
        set => this.lineOverColor = value;
      }

      public void reset()
      {
        this.bodyColor = CustomSelfDrawPanel.MailBodyColor;
        this.lineColor = CustomSelfDrawPanel.MailLineColor;
        this.overColor = CustomSelfDrawPanel.MailOverColor;
        this.lineOverColor = CustomSelfDrawPanel.MailLineOverColor;
        this.main.FillColor = this.bodyColor;
        this.line.FillColor = this.lineColor;
      }

      public CustomSelfDrawPanel.CSDLabel Text => this.textLabel;

      public void setup()
      {
        this.main.Size = new Size(this.Size.Width, this.Size.Height - 1);
        this.main.FillColor = this.bodyColor;
        this.line.Position = new Point(0, this.Size.Height - 1);
        this.line.Size = new Size(this.Size.Width, 1);
        this.line.FillColor = this.lineColor;
        this.textLabel.Position = new Point(3, 2);
        this.textLabel.Size = new Size(259, this.Size.Height - 4);
        this.textLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseLeave));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.main);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.line);
        this.main.addControl((CustomSelfDrawPanel.CSDControl) this.textLabel);
        this.setupComplete = true;
      }

      private void mouseOver()
      {
        if (this.textLabel.Text.Length <= 0 || !this.parent.Enabled)
          return;
        this.main.FillColor = this.overColor;
        this.line.FillColor = this.lineOverColor;
      }

      private void mouseLeave()
      {
        this.main.FillColor = this.bodyColor;
        this.line.FillColor = this.lineColor;
        this.invalidate();
      }
    }

    public class CSDListBox : CustomSelfDrawPanel.CSDControl
    {
      private bool created;
      private CustomSelfDrawPanel.CSDListEntry[] entries;
      public List<CustomSelfDrawPanel.CSDListItem> highlightedItems = new List<CustomSelfDrawPanel.CSDListItem>();
      private int m_numRows;
      private CustomSelfDrawPanel.CSDVertScrollBar scrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
      private CustomSelfDrawPanel.CSDImage scrollTabLines = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDButton upArrow = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton downArrow = new CustomSelfDrawPanel.CSDButton();
      public List<CustomSelfDrawPanel.CSDListItem> dataItems = new List<CustomSelfDrawPanel.CSDListItem>();
      private int selectedItemID = -1;
      private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
      private CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate lineClickedDelegate;
      private CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate doubleClickedDelegate;
      private DateTime doubleClickTime = DateTime.MinValue;

      public void Create(int numRows, int elementHeight)
      {
        if (this.csd == null)
          return;
        if (!this.created)
        {
          this.created = true;
          this.m_numRows = numRows;
          this.entries = new CustomSelfDrawPanel.CSDListEntry[numRows];
          for (int index = 0; index < numRows; ++index)
          {
            CustomSelfDrawPanel.CSDListEntry control = new CustomSelfDrawPanel.CSDListEntry();
            control.Position = new Point(0, index * elementHeight);
            control.Size = new Size(this.Size.Width - 16, elementHeight);
            control.Data = index;
            control.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cellClicked), "CSD_List_Box_entry_clicked");
            this.entries[index] = control;
            this.addControl((CustomSelfDrawPanel.CSDControl) control);
            control.setup();
          }
          this.scrollBar.Position = new Point(this.Size.Width - 16, 17);
          this.scrollBar.Size = new Size(16, this.Size.Height - 17 - 17 - 1);
          this.addControl((CustomSelfDrawPanel.CSDControl) this.scrollBar);
          this.scrollBar.Value = 0;
          this.scrollBar.Max = 0;
          this.scrollBar.NumVisibleLines = numRows;
          this.scrollBar.TabMinSize = 26;
          this.scrollBar.OffsetTL = new Point(0, 0);
          this.scrollBar.OffsetBR = new Point(0, 0);
          this.scrollBar.Create((Image) GFXLibrary.mail2_blue_scrollbar_bar_top, (Image) GFXLibrary.mail2_blue_scrollbar_bar_middle, (Image) GFXLibrary.mail2_blue_scrollbar_bar_bottom, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_top, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_bottom);
          this.scrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.scrollBarValueMoved));
          this.scrollBar.setScrollChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ScrollBarChangedDelegate(this.scrollBarMoved));
          this.upArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_normal;
          this.upArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_over;
          this.upArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_in;
          this.upArrow.Position = new Point(this.scrollBar.Position.X, 0);
          this.upArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ScrollUp), "CSD_List_Box_scroll_up");
          this.addControl((CustomSelfDrawPanel.CSDControl) this.upArrow);
          this.downArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_normal;
          this.downArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_over;
          this.downArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_in;
          this.downArrow.Position = new Point(this.scrollBar.Position.X, this.scrollBar.Position.Y + this.scrollBar.Size.Height);
          this.downArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ScrollDown), "CSD_List_Box_scroll_down");
          this.addControl((CustomSelfDrawPanel.CSDControl) this.downArrow);
          this.scrollTabLines.Image = (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid_lines;
          this.scrollTabLines.Position = new Point(this.scrollBar.TabPosition.X, (this.scrollBar.TabSize - 8) / 2 + this.scrollBar.TabPosition.Y);
          this.scrollBar.addControl((CustomSelfDrawPanel.CSDControl) this.scrollTabLines);
          this.mouseWheelOverlay.Position = new Point(0, 0);
          this.mouseWheelOverlay.Size = this.Size;
          this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
          this.addControl(this.mouseWheelOverlay);
        }
        this.selectedItemID = -1;
        this.updateEntries();
      }

      public override bool Enabled
      {
        get => true;
        set
        {
          if (!this.created)
            return;
          if (this.entries[0].Enabled != value)
            this.invalidate();
          foreach (CustomSelfDrawPanel.CSDControl entry in this.entries)
            entry.Enabled = value;
        }
      }

      public void populate(List<CustomSelfDrawPanel.CSDListItem> items)
      {
        this.dataItems = items;
        this.updateEntries();
      }

      public void setLineClickedDelegate(
        CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate callback)
      {
        this.lineClickedDelegate = callback;
      }

      public void setDoubleClickedDelegate(
        CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate callback)
      {
        this.doubleClickedDelegate = callback;
      }

      public CustomSelfDrawPanel.CSDListItem getSelectedItem()
      {
        return this.selectedItemID < 0 || this.selectedItemID >= this.dataItems.Count ? (CustomSelfDrawPanel.CSDListItem) null : this.dataItems[this.selectedItemID];
      }

      public void clearSelectedItem()
      {
        this.selectedItemID = -1;
        this.updateEntries();
      }

      public void updateEntries()
      {
        if (this.scrollBar.Max + this.m_numRows > this.dataItems.Count)
        {
          int num = Math.Max(0, this.dataItems.Count - this.m_numRows);
          if (this.scrollBar.Value > num)
            this.scrollBar.Value = num;
          this.scrollBar.Max = num;
        }
        else
          this.scrollBar.Max = Math.Max(0, this.dataItems.Count - this.m_numRows);
        for (int index = 0; index < this.m_numRows; ++index)
        {
          CustomSelfDrawPanel.CSDListEntry entry = this.entries[index];
          entry.Text.Text = "";
          entry.reset();
        }
        int index1 = this.scrollBar.Value;
        for (int index2 = 0; index2 < this.m_numRows && index1 < this.dataItems.Count; ++index1)
        {
          CustomSelfDrawPanel.CSDListItem dataItem = this.dataItems[index1];
          CustomSelfDrawPanel.CSDListEntry entry = this.entries[index2];
          entry.Text.Text = dataItem.Text;
          if (index1 == this.selectedItemID)
          {
            if (this.highlightedItems.Contains(dataItem))
            {
              entry.BodyColor = ARGBColors.GreenYellow;
              entry.OverColor = ARGBColors.Honeydew;
            }
            else
            {
              entry.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
              entry.OverColor = CustomSelfDrawPanel.MailSelectedOverColor;
            }
          }
          else if (this.highlightedItems.Contains(dataItem))
          {
            entry.BodyColor = ARGBColors.LightGreen;
            entry.OverColor = ARGBColors.Chartreuse;
          }
          entry.invalidate();
          ++index2;
        }
        this.scrollBar.recalc();
        this.scrollBar.invalidate();
        this.scrollBarMoved();
      }

      private void scrollBarValueMoved() => this.updateEntries();

      private void scrollBarMoved()
      {
        this.scrollTabLines.Position = new Point(this.scrollBar.TabPosition.X, (this.scrollBar.TabSize - 8) / 2 + this.scrollBar.TabPosition.Y);
      }

      private void ScrollUp() => this.ScrollUp(1);

      private void ScrollDown() => this.ScrollDown(1);

      private void ScrollUp(int diff)
      {
        int num1 = this.scrollBar.Value;
        if (num1 <= 0)
          return;
        int num2 = num1 - diff;
        if (num2 < 0)
          num2 = 0;
        this.scrollBar.Value = num2;
        this.scrollBar.invalidate();
        this.scrollBarMoved();
        this.scrollBarValueMoved();
        this.invalidate();
      }

      private void ScrollDown(int diff)
      {
        int num1 = this.scrollBar.Value;
        if (num1 >= this.scrollBar.Max)
          return;
        int num2 = num1 + diff;
        if (num2 > this.scrollBar.Max)
          num2 = this.scrollBar.Max;
        this.scrollBar.Value = num2;
        this.scrollBar.invalidate();
        this.scrollBarValueMoved();
        this.scrollBarMoved();
        this.invalidate();
      }

      private void mouseWheelMoved(int delta)
      {
        if (!this.scrollBar.Visible)
          return;
        if (delta < 0)
        {
          this.ScrollDown(3);
        }
        else
        {
          if (delta <= 0)
            return;
          this.ScrollUp(3);
        }
      }

      private void cellClicked()
      {
        if (this.csd.ClickedControl == null)
          return;
        int index = this.csd.ClickedControl.Data + this.scrollBar.Value;
        DateTime now = DateTime.Now;
        bool flag = false;
        if (index < 0 || index >= this.dataItems.Count)
          return;
        if (index == this.selectedItemID && (now - this.doubleClickTime).TotalSeconds < 2.0)
        {
          flag = true;
          this.doubleClickTime = DateTime.MinValue;
        }
        this.doubleClickTime = now;
        this.selectedItemID = index;
        this.updateEntries();
        if (this.lineClickedDelegate != null)
          this.lineClickedDelegate(this.dataItems[index]);
        if (!flag || this.doubleClickedDelegate == null)
          return;
        this.doubleClickedDelegate(this.dataItems[index]);
      }

      public bool contains(string testText)
      {
        foreach (CustomSelfDrawPanel.CSDListItem dataItem in this.dataItems)
        {
          if (dataItem.Text == testText)
            return true;
        }
        return false;
      }

      public delegate void CSD_LineClickedDelegate(CustomSelfDrawPanel.CSDListItem item);
    }

    private class CSDTabItem : CustomSelfDrawPanel.CSDControl
    {
      public Image normImage;
      public Image selectedImage;
      private Image m_overlayNormImage;
      private Image m_overlaySelectedImage;
      private float m_overlayAlpha = 1f;
      public int overlayImageWidth = 1;
      private CustomSelfDrawPanel.CSDTabControl m_parentControl;
      private CustomSelfDrawPanel.CSDButton mainButton = new CustomSelfDrawPanel.CSDButton();
      private bool m_selected;
      private CustomSelfDrawPanel.CSDTabControl.TabClickedCallback m_callback;

      public Image overlayNormImage
      {
        get => this.m_overlayNormImage;
        set
        {
          this.m_overlayNormImage = value;
          if (value != null)
          {
            if (this.m_selected || this.mainButton.ImageIcon == value)
              return;
            this.mainButton.ImageIcon = value;
            this.mainButton.invalidate();
          }
          else
          {
            if (this.mainButton.ImageIcon == value)
              return;
            this.mainButton.ImageIcon = (Image) null;
            this.mainButton.invalidate();
          }
        }
      }

      public Image overlaySelectedImage
      {
        get => this.m_overlaySelectedImage;
        set
        {
          this.m_overlaySelectedImage = value;
          if (value != null)
          {
            if (!this.m_selected || this.mainButton.ImageIcon == value)
              return;
            this.mainButton.ImageIcon = value;
            this.mainButton.invalidate();
          }
          else
          {
            if (this.mainButton.ImageIcon == value)
              return;
            this.mainButton.ImageIcon = (Image) null;
            this.mainButton.invalidate();
          }
        }
      }

      public float overlayAlpha
      {
        get => this.m_overlayAlpha;
        set
        {
          this.m_overlayAlpha = value;
          this.mainButton.ImageIconAlpha = value;
        }
      }

      public int setup(
        int xPos,
        CustomSelfDrawPanel.CSDTabControl parentControl,
        int index,
        bool selected)
      {
        this.m_selected = selected;
        this.m_parentControl = parentControl;
        this.mainButton.ImageNormAndOver = selected ? this.selectedImage : this.normImage;
        this.mainButton.MoveOnClick = false;
        this.mainButton.Position = new Point(xPos, 0);
        this.mainButton.Data = index;
        this.mainButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked));
        this.mainButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
        this.mainButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
        this.mainButton.Text.Position = new Point(0, 5);
        this.mainButton.Text.Color = ARGBColors.White;
        this.mainButton.LateTextRender = true;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.mainButton);
        this.Size = this.mainButton.Size;
        return this.mainButton.ImageNormAndOver != null ? this.mainButton.ImageNormAndOver.Width : 1;
      }

      public void setCallback(
        CustomSelfDrawPanel.CSDTabControl.TabClickedCallback callback)
      {
        this.m_callback = callback;
      }

      public void setTooltip(int tooltip) => this.mainButton.CustomTooltipID = tooltip;

      public void updateImage(bool selected)
      {
        this.m_selected = selected;
        if (!selected)
        {
          if (this.normImage != this.mainButton.ImageNormAndOver)
          {
            this.mainButton.ImageNormAndOver = this.normImage;
            this.mainButton.invalidate();
          }
          if (this.overlayNormImage == this.mainButton.ImageIcon)
            return;
          this.overlayNormImage = this.overlayNormImage;
        }
        else
        {
          if (this.mainButton.ImageNormAndOver != this.selectedImage)
          {
            this.mainButton.ImageNormAndOver = this.selectedImage;
            this.mainButton.invalidate();
          }
          if (this.overlaySelectedImage == this.mainButton.ImageIcon)
            return;
          this.overlaySelectedImage = this.overlaySelectedImage;
        }
      }

      private void tabClicked()
      {
        if (this.m_parentControl == null)
          return;
        this.m_parentControl.tabClicked(this.csd.ClickedControl.Data, true);
      }

      public void runCallback()
      {
        if (this.m_callback == null)
          return;
        this.m_callback();
      }

      public void setText(string text) => this.mainButton.Text.Text = text;

      public void setOverlayWidth(int width)
      {
        if (width == this.mainButton.ImageIconClipRect.Width)
          return;
        this.mainButton.ImageIconClipRect = new Rectangle(0, 0, width, this.mainButton.Height);
        this.mainButton.invalidate();
      }
    }

    public class CSDTabControl : CustomSelfDrawPanel.CSDControl
    {
      private List<CustomSelfDrawPanel.CSDTabItem> items = new List<CustomSelfDrawPanel.CSDTabItem>();
      private int currentSelected;
      private CustomSelfDrawPanel.CSDTabControl.TabClickedCallback soundsCallback;

      public int SelectedIndex
      {
        get => this.currentSelected;
        set
        {
          this.currentSelected = value;
          this.tabClicked(value, false);
        }
      }

      public int Create(int numIcons, BaseImage[] images)
      {
        this.items.Clear();
        int xPos = 0;
        for (int index = 0; index < numIcons; ++index)
        {
          CustomSelfDrawPanel.CSDTabItem control = new CustomSelfDrawPanel.CSDTabItem();
          if (index * 2 < images.Length)
          {
            control.normImage = (Image) images[index * 2];
            control.selectedImage = (Image) images[index * 2 + 1];
          }
          xPos += control.setup(xPos, this, index, index == this.currentSelected);
          this.items.Add(control);
          this.addControl((CustomSelfDrawPanel.CSDControl) control);
        }
        this.currentSelected = 0;
        this.updateAll();
        return xPos;
      }

      public void setCallback(
        int tab,
        CustomSelfDrawPanel.CSDTabControl.TabClickedCallback callback,
        int tooltip)
      {
        if (tab < 0 || tab >= this.items.Count)
          return;
        this.items[tab].setCallback(callback);
        this.items[tab].setTooltip(tooltip);
      }

      public void setTooltip(int tab, int tooltip)
      {
        if (tab < 0 || tab >= this.items.Count)
          return;
        this.items[tab].setTooltip(tooltip);
      }

      public void updateImageArray(BaseImage[] images)
      {
        int num = 0;
        foreach (CustomSelfDrawPanel.CSDTabItem csdTabItem in this.items)
        {
          if (num * 2 < images.Length)
          {
            csdTabItem.normImage = (Image) images[num * 2];
            csdTabItem.selectedImage = (Image) images[num * 2 + 1];
          }
          csdTabItem.updateImage(num == this.currentSelected);
          ++num;
        }
      }

      public void tabClicked(int index, bool fromClick)
      {
        this.currentSelected = index;
        this.updateAll();
        if (this.currentSelected >= 0 && this.currentSelected < this.items.Count)
          this.items[this.currentSelected].runCallback();
        if (!fromClick || this.soundsCallback == null)
          return;
        this.soundsCallback();
      }

      private void updateAll()
      {
        int num = 0;
        foreach (CustomSelfDrawPanel.CSDTabItem csdTabItem in this.items)
        {
          csdTabItem.updateImage(num == this.currentSelected);
          ++num;
        }
      }

      public void setOverlayAlpha(int tab, int alpha)
      {
        if (tab < 0 || tab >= this.items.Count)
          return;
        float num = (float) alpha / (float) byte.MaxValue;
        if ((double) num == (double) this.items[tab].overlayAlpha)
          return;
        this.items[tab].overlayAlpha = num;
      }

      public void setOverlayWidth(int tab, int width)
      {
        if (tab < 0 || tab >= this.items.Count)
          return;
        this.items[tab].setOverlayWidth(width);
      }

      public void addOverlayImages(
        int tab,
        BaseImage overlayNorm,
        BaseImage overlaySelected,
        int alpha)
      {
        if (tab < 0 || tab >= this.items.Count)
          return;
        float num = (float) alpha / (float) byte.MaxValue;
        this.items[tab].overlayNormImage = (Image) overlayNorm;
        this.items[tab].overlaySelectedImage = (Image) overlaySelected;
        this.items[tab].overlayAlpha = num;
        if (overlayNorm == null)
          return;
        this.items[tab].overlayImageWidth = overlayNorm.Width;
      }

      public void setTabText(int tab, string text)
      {
        if (tab < 0 || tab >= this.items.Count)
          return;
        this.items[tab].setText(text);
      }

      public void setSoundCallback(
        CustomSelfDrawPanel.CSDTabControl.TabClickedCallback callback)
      {
        this.soundsCallback = callback;
      }

      public delegate void TabClickedCallback();
    }

    public class FactionPanelSideBar : CustomSelfDrawPanel.CSDControl
    {
      public const int SIDEBAR_WIDTH = 200;
      public const int SIDEBAR_MODE_INVITES = 0;
      public const int SIDEBAR_MODE_MY_FACTION = 1;
      public const int SIDEBAR_MODE_ALL_FACTIONS = 2;
      public const int SIDEBAR_MODE_OFFICERS = 3;
      public const int SIDEBAR_MODE_DIPLOMACY = 4;
      public const int SIDEBAR_MODE_START_FACTION = 5;
      public const int SIDEBAR_MODE_FORUM = 6;
      public const int SIDEBAR_MODE_HOUSE_LIST = 7;
      public const int SIDEBAR_MODE_HOUSE_INFO = 8;
      private CustomSelfDrawPanel.CSDVertExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDVertExtendingPanel();
      private CustomSelfDrawPanel.CSDButton factionShowAllButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton factionMyFactionButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton factionDiplomacyButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton factionOfficersButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton factionForumButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton factionMailFactionButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton factionInvitesButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton factionStartFactionButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton factionChatButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton factionHereticButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton factionLeaveFactionButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDImage factionButtonBackground = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDButton houseShowAllButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house1Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house2Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house3Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house4Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house5Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house6Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house7Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house8Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house9Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house10Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house11Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house12Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house13Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house14Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house15Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house16Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house17Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house18Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house19Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton house20Button = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDImage houseOverlay = new CustomSelfDrawPanel.CSDImage();
      private static int m_currentSidebarMode = -1;
      private CustomSelfDrawPanel m_parent;
      private MyMessageBoxPopUp leaveFactionPopup;
      private static bool m_factionDataUpdated = false;
      private static DateTime m_lastFactionUpdate = DateTime.MinValue;

      public void addSideBar(int mode, CustomSelfDrawPanel parent)
      {
        CustomSelfDrawPanel.FactionPanelSideBar.m_factionDataUpdated = false;
        CustomSelfDrawPanel.FactionPanelSideBar.m_currentSidebarMode = mode;
        this.m_parent = parent;
        this.clearControls();
        this.Position = new Point(parent.Width - 200, 0);
        this.Size = new Size(200, parent.Height);
        parent.removeControl((CustomSelfDrawPanel.CSDControl) this);
        parent.addControl((CustomSelfDrawPanel.CSDControl) this);
        this.backgroundImage.Position = new Point(0, 0);
        this.backgroundImage.Size = new Size(200, parent.Height);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.backgroundImage.Create((Image) GFXLibrary.faction_background, (Image) GFXLibrary.faction_background_bottom, (Image) GFXLibrary.faction_background_bottom);
        this.factionButtonBackground.Image = (Image) GFXLibrary.faction_button_background;
        this.factionButtonBackground.Position = new Point(0, 0);
        this.factionButtonBackground.Visible = false;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionButtonBackground);
        int y1 = 10;
        int num1 = 40;
        switch (mode)
        {
          case 0:
          case 1:
          case 2:
          case 3:
          case 4:
          case 5:
          case 6:
            this.factionShowAllButton.ImageNorm = (Image) GFXLibrary.faction_buttons[0];
            this.factionShowAllButton.ImageOver = (Image) GFXLibrary.faction_buttons[1];
            this.factionShowAllButton.Position = new Point(7, y1);
            this.factionShowAllButton.MoveOnClick = true;
            this.factionShowAllButton.Text.Text = SK.Text("FactionsSidebar_Show_All", "Show All");
            this.factionShowAllButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.factionShowAllButton.Text.Position = new Point(84, 0);
            this.factionShowAllButton.Text.Size = new Size(94, 40);
            this.factionShowAllButton.TextYOffset = -3;
            this.factionShowAllButton.Text.Color = ARGBColors.Black;
            this.factionShowAllButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showAllClicked), "FactionPanelSideBar_all_factions");
            this.factionShowAllButton.Active = true;
            this.factionShowAllButton.CustomTooltipID = 2350;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionShowAllButton);
            int y2 = y1 + num1;
            int yourFactionRank = GameEngine.Instance.World.getYourFactionRank();
            if (RemoteServices.Instance.UserFactionID >= 0)
            {
              this.factionMyFactionButton.ImageNorm = (Image) GFXLibrary.faction_buttons[2];
              this.factionMyFactionButton.ImageOver = (Image) GFXLibrary.faction_buttons[3];
              this.factionMyFactionButton.Position = new Point(7, y2);
              this.factionMyFactionButton.MoveOnClick = true;
              this.factionMyFactionButton.Text.Text = SK.Text("FactionsSidebar_My_Faction", "My Faction");
              this.factionMyFactionButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
              this.factionMyFactionButton.Text.Position = new Point(84, 0);
              this.factionMyFactionButton.Text.Size = new Size(94, 40);
              this.factionMyFactionButton.TextYOffset = -3;
              this.factionMyFactionButton.Text.Color = ARGBColors.Black;
              this.factionMyFactionButton.Active = true;
              this.factionMyFactionButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.myFactionClicked), "FactionPanelSideBar_my_faction");
              this.factionMyFactionButton.CustomTooltipID = 2351;
              this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionMyFactionButton);
              y2 += num1;
              if (!GameEngine.Instance.World.isHeretic())
              {
                this.factionDiplomacyButton.ImageNorm = (Image) GFXLibrary.faction_buttons[4];
                this.factionDiplomacyButton.ImageOver = (Image) GFXLibrary.faction_buttons[5];
                this.factionDiplomacyButton.Position = new Point(7, y2);
                this.factionDiplomacyButton.MoveOnClick = true;
                this.factionDiplomacyButton.Text.Text = SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy");
                this.factionDiplomacyButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                this.factionDiplomacyButton.Text.Position = new Point(84, 0);
                this.factionDiplomacyButton.Text.Size = new Size(94, 40);
                this.factionDiplomacyButton.TextYOffset = -3;
                this.factionDiplomacyButton.Text.Color = ARGBColors.Black;
                this.factionDiplomacyButton.Active = true;
                this.factionDiplomacyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.diplomacyClicked), "FactionPanelSideBar_diplomacy");
                this.factionDiplomacyButton.CustomTooltipID = 2352;
                this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionDiplomacyButton);
                int y3 = y2 + num1;
                if (yourFactionRank == 2 || yourFactionRank == 1)
                {
                  this.factionOfficersButton.ImageNorm = (Image) GFXLibrary.faction_buttons[6];
                  this.factionOfficersButton.ImageOver = (Image) GFXLibrary.faction_buttons[7];
                  this.factionOfficersButton.Position = new Point(7, y3);
                  this.factionOfficersButton.MoveOnClick = true;
                  this.factionOfficersButton.Text.Text = SK.Text("FactionsSidebar_Officers", "Officers");
                  this.factionOfficersButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                  this.factionOfficersButton.Text.Position = new Point(84, 0);
                  this.factionOfficersButton.Text.Size = new Size(94, 40);
                  this.factionOfficersButton.TextYOffset = -3;
                  this.factionOfficersButton.Text.Color = ARGBColors.Black;
                  this.factionOfficersButton.Active = true;
                  this.factionOfficersButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.officersClicked), "FactionPanelSideBar_officers");
                  this.factionOfficersButton.CustomTooltipID = 2353;
                  this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionOfficersButton);
                  y3 += num1;
                }
                this.factionForumButton.ImageNorm = (Image) GFXLibrary.faction_buttons[8];
                this.factionForumButton.ImageOver = (Image) GFXLibrary.faction_buttons[9];
                this.factionForumButton.Position = new Point(7, y3);
                this.factionForumButton.MoveOnClick = true;
                this.factionForumButton.Text.Text = SK.Text("FactionsSidebar_Forum", "Forum");
                this.factionForumButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                this.factionForumButton.Text.Position = new Point(84, 0);
                this.factionForumButton.Text.Size = new Size(94, 40);
                this.factionForumButton.TextYOffset = -3;
                this.factionForumButton.Text.Color = ARGBColors.Black;
                this.factionForumButton.Active = true;
                this.factionForumButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumClicked), "FactionPanelSideBar_forum");
                this.factionForumButton.CustomTooltipID = 2354;
                this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionForumButton);
                y2 = y3 + num1;
              }
            }
            if (!GameEngine.Instance.World.isHeretic())
            {
              if (yourFactionRank == 2 || yourFactionRank == 1)
              {
                this.factionMailFactionButton.ImageNorm = (Image) GFXLibrary.faction_buttons[10];
                this.factionMailFactionButton.ImageOver = (Image) GFXLibrary.faction_buttons[11];
                this.factionMailFactionButton.Position = new Point(7, y2);
                this.factionMailFactionButton.MoveOnClick = true;
                this.factionMailFactionButton.Text.Text = SK.Text("FactionsPanel_MailFaction", "Mail To Faction");
                this.factionMailFactionButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                this.factionMailFactionButton.Text.Position = new Point(84, 0);
                this.factionMailFactionButton.Text.Size = new Size(94, 40);
                this.factionMailFactionButton.TextYOffset = -3;
                this.factionMailFactionButton.Text.Color = ARGBColors.Black;
                this.factionMailFactionButton.Active = true;
                this.factionMailFactionButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailFactionClicked), "FactionPanelSideBar_mail_faction");
                this.factionMailFactionButton.CustomTooltipID = 2355;
                this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionMailFactionButton);
                y2 += num1;
              }
              this.factionInvitesButton.ImageNorm = (Image) GFXLibrary.faction_buttons[12];
              this.factionInvitesButton.ImageOver = (Image) GFXLibrary.faction_buttons[13];
              this.factionInvitesButton.Position = new Point(7, y2);
              this.factionInvitesButton.MoveOnClick = true;
              this.factionInvitesButton.Text.Text = SK.Text("FactionsPanel_Users", "Invites");
              this.factionInvitesButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
              this.factionInvitesButton.Text.Position = new Point(84, 0);
              this.factionInvitesButton.Text.Size = new Size(94, 40);
              this.factionInvitesButton.TextYOffset = -3;
              this.factionInvitesButton.Text.Color = ARGBColors.Black;
              this.factionInvitesButton.Active = true;
              this.factionInvitesButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.invitesClicked), "FactionPanelSideBar_invites");
              this.factionInvitesButton.CustomTooltipID = 2356;
              this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionInvitesButton);
              y2 += num1;
            }
            if (RemoteServices.Instance.UserFactionID < 0)
            {
              this.factionStartFactionButton.ImageNorm = (Image) GFXLibrary.faction_buttons[16];
              this.factionStartFactionButton.ImageOver = (Image) GFXLibrary.faction_buttons[17];
              this.factionStartFactionButton.Position = new Point(7, y2);
              this.factionStartFactionButton.MoveOnClick = true;
              this.factionStartFactionButton.Text.Text = SK.Text("FactionsSidebar_Start_Faction", "Start a Faction");
              this.factionStartFactionButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
              this.factionStartFactionButton.Text.Position = new Point(84, 0);
              this.factionStartFactionButton.Text.Size = new Size(94, 40);
              this.factionStartFactionButton.TextYOffset = -3;
              this.factionStartFactionButton.Text.Color = ARGBColors.Black;
              this.factionStartFactionButton.Active = true;
              this.factionStartFactionButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.startFactionClicked), "FactionPanelSideBar_start_faction");
              this.factionChatButton.CustomTooltipID = 2358;
              this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionStartFactionButton);
              int y4 = y2 + num1;
              if (GameEngine.Instance.LocalWorldData.IsHereticEUAIWorld)
              {
                this.factionHereticButton.ImageNorm = (Image) GFXLibrary.faction_buttons[22];
                this.factionHereticButton.ImageOver = (Image) GFXLibrary.faction_buttons[23];
                this.factionHereticButton.Position = new Point(7, y4);
                this.factionHereticButton.MoveOnClick = true;
                this.factionHereticButton.Text.Text = SK.Text("FactionsSidebar_Heretic", "Go Heretic!");
                this.factionHereticButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                this.factionHereticButton.Text.Position = new Point(84, 0);
                this.factionHereticButton.Text.Size = new Size(94, 40);
                this.factionHereticButton.TextYOffset = -3;
                this.factionHereticButton.Text.Color = ARGBColors.Black;
                this.factionHereticButton.Active = true;
                this.factionHereticButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.goHereticClicked), "FactionPanelSideBar_start_faction");
                this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionHereticButton);
                this.factionHereticButton.Enabled = false;
                if (GameEngine.Instance.World.getRank() >= 6)
                  this.factionHereticButton.Enabled = true;
                y4 += num1;
              }
              CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.backgroundImage, 23, new Point(this.backgroundImage.Width - 38, y4 + 5));
              break;
            }
            if (!GameEngine.Instance.World.isHeretic())
            {
              this.factionChatButton.ImageNorm = (Image) GFXLibrary.faction_buttons[14];
              this.factionChatButton.ImageOver = (Image) GFXLibrary.faction_buttons[15];
              this.factionChatButton.Position = new Point(7, y2);
              this.factionChatButton.MoveOnClick = true;
              this.factionChatButton.Text.Text = SK.Text("GENERIC_Chat", "Chat");
              this.factionChatButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
              this.factionChatButton.Text.Position = new Point(84, 0);
              this.factionChatButton.Text.Size = new Size(94, 40);
              this.factionChatButton.TextYOffset = -3;
              this.factionChatButton.Text.Color = ARGBColors.Black;
              this.factionChatButton.Active = true;
              this.factionChatButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.chatClicked), "FactionPanelSideBar_chat");
              this.factionChatButton.CustomTooltipID = 2357;
              this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionChatButton);
              int y5 = y2 + num1;
              if (GameEngine.Instance.LocalWorldData.IsHereticEUAIWorld)
              {
                this.factionHereticButton.ImageNorm = (Image) GFXLibrary.faction_buttons[22];
                this.factionHereticButton.ImageOver = (Image) GFXLibrary.faction_buttons[23];
                this.factionHereticButton.Position = new Point(7, y5);
                this.factionHereticButton.MoveOnClick = true;
                this.factionHereticButton.Text.Text = SK.Text("FactionsSidebar_Heretic", "Go Heretic!");
                this.factionHereticButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                this.factionHereticButton.Text.Position = new Point(84, 0);
                this.factionHereticButton.Text.Size = new Size(94, 40);
                this.factionHereticButton.TextYOffset = -3;
                this.factionHereticButton.Text.Color = ARGBColors.Black;
                this.factionHereticButton.Active = true;
                this.factionHereticButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.goHereticClicked), "FactionPanelSideBar_start_faction");
                this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionHereticButton);
                this.factionHereticButton.Enabled = false;
                if (GameEngine.Instance.World.getRank() >= 6)
                  this.factionHereticButton.Enabled = true;
                y5 += num1;
              }
              CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.backgroundImage, 23, new Point(this.backgroundImage.Width - 38, y5 + 5));
              this.factionLeaveFactionButton.ImageNorm = (Image) GFXLibrary.faction_buttons[18];
              this.factionLeaveFactionButton.ImageOver = (Image) GFXLibrary.faction_buttons[19];
              this.factionLeaveFactionButton.Position = new Point(7, this.Height - 45);
              this.factionLeaveFactionButton.MoveOnClick = true;
              this.factionLeaveFactionButton.Text.Text = SK.Text("FactionsPanel_Leave_Faction", "Leave Faction");
              this.factionLeaveFactionButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
              this.factionLeaveFactionButton.Text.Position = new Point(84, 0);
              this.factionLeaveFactionButton.Text.Size = new Size(94, 40);
              this.factionLeaveFactionButton.TextYOffset = -3;
              this.factionLeaveFactionButton.Text.Color = ARGBColors.Black;
              this.factionLeaveFactionButton.Active = true;
              this.factionLeaveFactionButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.leaveFactionClicked), "FactionPanelSideBar_leave_faction");
              this.factionLeaveFactionButton.CustomTooltipID = 2359;
              this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionLeaveFactionButton);
              break;
            }
            break;
          case 7:
          case 8:
            CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.backgroundImage, 24, new Point(this.backgroundImage.Width - 38, 52));
            this.houseShowAllButton.ImageNorm = (Image) GFXLibrary.faction_buttons[20];
            this.houseShowAllButton.ImageOver = (Image) GFXLibrary.faction_buttons[21];
            this.houseShowAllButton.Position = new Point(7, y1);
            this.houseShowAllButton.MoveOnClick = true;
            this.houseShowAllButton.Text.Text = SK.Text("FactionsSidebar_Show_All", "Show All");
            this.houseShowAllButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.houseShowAllButton.Text.Position = new Point(84, 0);
            this.houseShowAllButton.Text.Size = new Size(94, 40);
            this.houseShowAllButton.TextYOffset = -3;
            this.houseShowAllButton.Text.Color = ARGBColors.Black;
            this.houseShowAllButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showAllHousesClicked), "FactionPanelSideBar_all_houses");
            this.houseShowAllButton.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseShowAllButton);
            int y6 = y1 + (num1 + 1);
            int num2 = 1;
            int x1 = 47;
            int x2 = 107;
            int num3 = -15;
            int num4 = 25;
            this.house1Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num2 - 1);
            this.house1Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num2 - 1 + 20);
            this.house1Button.Position = new Point(x1, y6);
            this.house1Button.MoveOnClick = true;
            this.house1Button.CustomTooltipID = 2307;
            this.house1Button.CustomTooltipData = num2;
            CustomSelfDrawPanel.CSDButton house1Button = this.house1Button;
            int num5 = num2;
            int num6 = num5 + 1;
            house1Button.Data = num5;
            this.house1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house1Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house1Button);
            int num7 = y6 + num4;
            this.house2Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num6 - 1);
            this.house2Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num6 - 1 + 20);
            this.house2Button.Position = new Point(x2, num7 + num3);
            this.house2Button.MoveOnClick = true;
            this.house2Button.CustomTooltipID = 2307;
            this.house2Button.CustomTooltipData = num6;
            CustomSelfDrawPanel.CSDButton house2Button = this.house2Button;
            int num8 = num6;
            int num9 = num8 + 1;
            house2Button.Data = num8;
            this.house2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house2Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house2Button);
            int y7 = num7 + num4;
            this.house3Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num9 - 1);
            this.house3Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num9 - 1 + 20);
            this.house3Button.Position = new Point(x1, y7);
            this.house3Button.MoveOnClick = true;
            this.house3Button.CustomTooltipID = 2307;
            this.house3Button.CustomTooltipData = num9;
            CustomSelfDrawPanel.CSDButton house3Button = this.house3Button;
            int num10 = num9;
            int num11 = num10 + 1;
            house3Button.Data = num10;
            this.house3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house3Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house3Button);
            int num12 = y7 + num4;
            this.house4Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num11 - 1);
            this.house4Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num11 - 1 + 20);
            this.house4Button.Position = new Point(x2, num12 + num3);
            this.house4Button.MoveOnClick = true;
            this.house4Button.CustomTooltipID = 2307;
            this.house4Button.CustomTooltipData = num11;
            CustomSelfDrawPanel.CSDButton house4Button = this.house4Button;
            int num13 = num11;
            int num14 = num13 + 1;
            house4Button.Data = num13;
            this.house4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house4Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house4Button);
            int y8 = num12 + num4;
            this.house5Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num14 - 1);
            this.house5Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num14 - 1 + 20);
            this.house5Button.Position = new Point(x1, y8);
            this.house5Button.MoveOnClick = true;
            this.house5Button.CustomTooltipID = 2307;
            this.house5Button.CustomTooltipData = num14;
            CustomSelfDrawPanel.CSDButton house5Button = this.house5Button;
            int num15 = num14;
            int num16 = num15 + 1;
            house5Button.Data = num15;
            this.house5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house5Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house5Button);
            int num17 = y8 + num4;
            this.house6Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num16 - 1);
            this.house6Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num16 - 1 + 20);
            this.house6Button.Position = new Point(x2, num17 + num3);
            this.house6Button.MoveOnClick = true;
            this.house6Button.CustomTooltipID = 2307;
            this.house6Button.CustomTooltipData = num16;
            CustomSelfDrawPanel.CSDButton house6Button = this.house6Button;
            int num18 = num16;
            int num19 = num18 + 1;
            house6Button.Data = num18;
            this.house6Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house6Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house6Button);
            int y9 = num17 + num4;
            this.house7Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num19 - 1);
            this.house7Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num19 - 1 + 20);
            this.house7Button.Position = new Point(x1, y9);
            this.house7Button.MoveOnClick = true;
            this.house7Button.CustomTooltipID = 2307;
            this.house7Button.CustomTooltipData = num19;
            CustomSelfDrawPanel.CSDButton house7Button = this.house7Button;
            int num20 = num19;
            int num21 = num20 + 1;
            house7Button.Data = num20;
            this.house7Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house7Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house7Button);
            int num22 = y9 + num4;
            this.house8Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num21 - 1);
            this.house8Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num21 - 1 + 20);
            this.house8Button.Position = new Point(x2, num22 + num3);
            this.house8Button.MoveOnClick = true;
            this.house8Button.CustomTooltipID = 2307;
            this.house8Button.CustomTooltipData = num21;
            CustomSelfDrawPanel.CSDButton house8Button = this.house8Button;
            int num23 = num21;
            int num24 = num23 + 1;
            house8Button.Data = num23;
            this.house8Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house8Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house8Button);
            int y10 = num22 + num4;
            this.house9Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num24 - 1);
            this.house9Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num24 - 1 + 20);
            this.house9Button.Position = new Point(x1, y10);
            this.house9Button.MoveOnClick = true;
            this.house9Button.CustomTooltipID = 2307;
            this.house9Button.CustomTooltipData = num24;
            CustomSelfDrawPanel.CSDButton house9Button = this.house9Button;
            int num25 = num24;
            int num26 = num25 + 1;
            house9Button.Data = num25;
            this.house9Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house9Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house9Button);
            int num27 = y10 + num4;
            this.house10Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num26 - 1);
            this.house10Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num26 - 1 + 20);
            this.house10Button.Position = new Point(x2, num27 + num3);
            this.house10Button.MoveOnClick = true;
            this.house10Button.CustomTooltipID = 2307;
            this.house10Button.CustomTooltipData = num26;
            CustomSelfDrawPanel.CSDButton house10Button = this.house10Button;
            int num28 = num26;
            int num29 = num28 + 1;
            house10Button.Data = num28;
            this.house10Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house10Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house10Button);
            int y11 = num27 + num4;
            this.house11Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num29 - 1);
            this.house11Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num29 - 1 + 20);
            this.house11Button.Position = new Point(x1, y11);
            this.house11Button.MoveOnClick = true;
            this.house11Button.CustomTooltipID = 2307;
            this.house11Button.CustomTooltipData = num29;
            CustomSelfDrawPanel.CSDButton house11Button = this.house11Button;
            int num30 = num29;
            int num31 = num30 + 1;
            house11Button.Data = num30;
            this.house11Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house11Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house11Button);
            int num32 = y11 + num4;
            this.house12Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num31 - 1);
            this.house12Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num31 - 1 + 20);
            this.house12Button.Position = new Point(x2, num32 + num3);
            this.house12Button.MoveOnClick = true;
            this.house12Button.CustomTooltipID = 2307;
            this.house12Button.CustomTooltipData = num31;
            CustomSelfDrawPanel.CSDButton house12Button = this.house12Button;
            int num33 = num31;
            int num34 = num33 + 1;
            house12Button.Data = num33;
            this.house12Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house12Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house12Button);
            int y12 = num32 + num4;
            this.house13Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num34 - 1);
            this.house13Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num34 - 1 + 20);
            this.house13Button.Position = new Point(x1, y12);
            this.house13Button.MoveOnClick = true;
            this.house13Button.CustomTooltipID = 2307;
            this.house13Button.CustomTooltipData = num34;
            CustomSelfDrawPanel.CSDButton house13Button = this.house13Button;
            int num35 = num34;
            int num36 = num35 + 1;
            house13Button.Data = num35;
            this.house13Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house13Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house13Button);
            int num37 = y12 + num4;
            this.house14Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num36 - 1);
            this.house14Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num36 - 1 + 20);
            this.house14Button.Position = new Point(x2, num37 + num3);
            this.house14Button.MoveOnClick = true;
            this.house14Button.CustomTooltipID = 2307;
            this.house14Button.CustomTooltipData = num36;
            CustomSelfDrawPanel.CSDButton house14Button = this.house14Button;
            int num38 = num36;
            int num39 = num38 + 1;
            house14Button.Data = num38;
            this.house14Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house14Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house14Button);
            int y13 = num37 + num4;
            this.house15Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num39 - 1);
            this.house15Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num39 - 1 + 20);
            this.house15Button.Position = new Point(x1, y13);
            this.house15Button.MoveOnClick = true;
            this.house15Button.CustomTooltipID = 2307;
            this.house15Button.CustomTooltipData = num39;
            CustomSelfDrawPanel.CSDButton house15Button = this.house15Button;
            int num40 = num39;
            int num41 = num40 + 1;
            house15Button.Data = num40;
            this.house15Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house15Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house15Button);
            int num42 = y13 + num4;
            this.house16Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num41 - 1);
            this.house16Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num41 - 1 + 20);
            this.house16Button.Position = new Point(x2, num42 + num3);
            this.house16Button.MoveOnClick = true;
            this.house16Button.CustomTooltipID = 2307;
            this.house16Button.CustomTooltipData = num41;
            CustomSelfDrawPanel.CSDButton house16Button = this.house16Button;
            int num43 = num41;
            int num44 = num43 + 1;
            house16Button.Data = num43;
            this.house16Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house16Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house16Button);
            int y14 = num42 + num4;
            this.house17Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num44 - 1);
            this.house17Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num44 - 1 + 20);
            this.house17Button.Position = new Point(x1, y14);
            this.house17Button.MoveOnClick = true;
            this.house17Button.CustomTooltipID = 2307;
            this.house17Button.CustomTooltipData = num44;
            CustomSelfDrawPanel.CSDButton house17Button = this.house17Button;
            int num45 = num44;
            int num46 = num45 + 1;
            house17Button.Data = num45;
            this.house17Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house17Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house17Button);
            int num47 = y14 + num4;
            this.house18Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num46 - 1);
            this.house18Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num46 - 1 + 20);
            this.house18Button.Position = new Point(x2, num47 + num3);
            this.house18Button.MoveOnClick = true;
            this.house18Button.CustomTooltipID = 2307;
            this.house18Button.CustomTooltipData = num46;
            CustomSelfDrawPanel.CSDButton house18Button = this.house18Button;
            int num48 = num46;
            int num49 = num48 + 1;
            house18Button.Data = num48;
            this.house18Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house18Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house18Button);
            int y15 = num47 + num4;
            this.house19Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num49 - 1);
            this.house19Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num49 - 1 + 20);
            this.house19Button.Position = new Point(x1, y15);
            this.house19Button.MoveOnClick = true;
            this.house19Button.CustomTooltipID = 2307;
            this.house19Button.CustomTooltipData = num49;
            CustomSelfDrawPanel.CSDButton house19Button = this.house19Button;
            int num50 = num49;
            int num51 = num50 + 1;
            house19Button.Data = num50;
            this.house19Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house19Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house19Button);
            int num52 = y15 + num4;
            this.house20Button.ImageNorm = (Image) GFXLibrary.getHouseCircleMediumImage(num51 - 1);
            this.house20Button.ImageOver = (Image) GFXLibrary.getHouseCircleMediumImage(num51 - 1 + 20);
            this.house20Button.Position = new Point(x2, num52 + num3);
            this.house20Button.MoveOnClick = true;
            this.house20Button.CustomTooltipID = 2307;
            this.house20Button.CustomTooltipData = num51;
            CustomSelfDrawPanel.CSDButton house20Button = this.house20Button;
            int num53 = num51;
            int num54 = num53 + 1;
            house20Button.Data = num53;
            this.house20Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
            this.house20Button.Active = true;
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.house20Button);
            int num55 = num52 + num4;
            break;
        }
        int yourHouse = GameEngine.Instance.World.YourHouse;
        Point point = Point.Empty;
        switch (yourHouse)
        {
          case 1:
            point = this.house1Button.Position;
            break;
          case 2:
            point = this.house2Button.Position;
            break;
          case 3:
            point = this.house3Button.Position;
            break;
          case 4:
            point = this.house4Button.Position;
            break;
          case 5:
            point = this.house5Button.Position;
            break;
          case 6:
            point = this.house6Button.Position;
            break;
          case 7:
            point = this.house7Button.Position;
            break;
          case 8:
            point = this.house8Button.Position;
            break;
          case 9:
            point = this.house9Button.Position;
            break;
          case 10:
            point = this.house10Button.Position;
            break;
          case 11:
            point = this.house11Button.Position;
            break;
          case 12:
            point = this.house12Button.Position;
            break;
          case 13:
            point = this.house13Button.Position;
            break;
          case 14:
            point = this.house14Button.Position;
            break;
          case 15:
            point = this.house15Button.Position;
            break;
          case 16:
            point = this.house16Button.Position;
            break;
          case 17:
            point = this.house17Button.Position;
            break;
          case 18:
            point = this.house18Button.Position;
            break;
          case 19:
            point = this.house19Button.Position;
            break;
          case 20:
            point = this.house20Button.Position;
            break;
        }
        if (!point.IsEmpty)
        {
          this.houseOverlay.Image = (Image) GFXLibrary.house_circles_medium_selected_top;
          this.houseOverlay.Position = point;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseOverlay);
        }
        this.factionButtonBackground.Image = (Image) GFXLibrary.faction_button_background;
        switch (mode)
        {
          case 0:
            this.factionInvitesButton.Active = false;
            this.factionInvitesButton.ImageOver = (Image) GFXLibrary.faction_buttons[12];
            this.factionButtonBackground.Visible = true;
            this.factionButtonBackground.Position = new Point(0, this.factionInvitesButton.Position.Y - 3);
            break;
          case 1:
            if (FactionMyFactionPanel.SelectedFaction == RemoteServices.Instance.UserFactionID)
            {
              this.factionMyFactionButton.ImageOver = (Image) GFXLibrary.faction_buttons[2];
              this.factionMyFactionButton.Active = false;
              this.factionButtonBackground.Visible = true;
              this.factionButtonBackground.Position = new Point(0, this.factionMyFactionButton.Position.Y - 3);
              break;
            }
            break;
          case 2:
            this.factionShowAllButton.Active = false;
            this.factionShowAllButton.ImageOver = (Image) GFXLibrary.faction_buttons[0];
            this.factionButtonBackground.Visible = true;
            this.factionButtonBackground.Position = new Point(0, this.factionShowAllButton.Position.Y - 3);
            break;
          case 3:
            this.factionOfficersButton.Active = false;
            this.factionOfficersButton.ImageOver = (Image) GFXLibrary.faction_buttons[6];
            this.factionButtonBackground.Visible = true;
            this.factionButtonBackground.Position = new Point(0, this.factionOfficersButton.Position.Y - 3);
            break;
          case 4:
            this.factionDiplomacyButton.Active = false;
            this.factionDiplomacyButton.ImageOver = (Image) GFXLibrary.faction_buttons[4];
            this.factionButtonBackground.Visible = true;
            this.factionButtonBackground.Position = new Point(0, this.factionDiplomacyButton.Position.Y - 3);
            break;
          case 5:
            if (RemoteServices.Instance.UserFactionID < 0)
            {
              this.factionStartFactionButton.Active = false;
              this.factionStartFactionButton.ImageOver = (Image) GFXLibrary.faction_buttons[16];
              this.factionButtonBackground.Visible = true;
              this.factionButtonBackground.Position = new Point(0, this.factionStartFactionButton.Position.Y - 3);
              break;
            }
            break;
          case 6:
            this.factionForumButton.Active = false;
            this.factionForumButton.ImageOver = (Image) GFXLibrary.faction_buttons[8];
            this.factionButtonBackground.Visible = true;
            this.factionButtonBackground.Position = new Point(0, this.factionForumButton.Position.Y - 3);
            break;
          case 7:
            this.houseShowAllButton.Active = false;
            this.houseShowAllButton.ImageOver = (Image) GFXLibrary.faction_buttons[0];
            this.factionButtonBackground.Visible = true;
            this.factionButtonBackground.Position = new Point(0, this.houseShowAllButton.Position.Y - 3);
            break;
          case 8:
            CustomSelfDrawPanel.CSDButton csdButton = (CustomSelfDrawPanel.CSDButton) null;
            switch (HouseInfoPanel.SelectedHouse)
            {
              case 1:
                csdButton = this.house1Button;
                break;
              case 2:
                csdButton = this.house2Button;
                break;
              case 3:
                csdButton = this.house3Button;
                break;
              case 4:
                csdButton = this.house4Button;
                break;
              case 5:
                csdButton = this.house5Button;
                break;
              case 6:
                csdButton = this.house6Button;
                break;
              case 7:
                csdButton = this.house7Button;
                break;
              case 8:
                csdButton = this.house8Button;
                break;
              case 9:
                csdButton = this.house9Button;
                break;
              case 10:
                csdButton = this.house10Button;
                break;
              case 11:
                csdButton = this.house11Button;
                break;
              case 12:
                csdButton = this.house12Button;
                break;
              case 13:
                csdButton = this.house13Button;
                break;
              case 14:
                csdButton = this.house14Button;
                break;
              case 15:
                csdButton = this.house15Button;
                break;
              case 16:
                csdButton = this.house16Button;
                break;
              case 17:
                csdButton = this.house17Button;
                break;
              case 18:
                csdButton = this.house18Button;
                break;
              case 19:
                csdButton = this.house19Button;
                break;
              case 20:
                csdButton = this.house20Button;
                break;
            }
            if (csdButton != null)
            {
              csdButton.ImageNorm = csdButton.ImageHighlight = csdButton.ImageOver;
              csdButton.Active = false;
              break;
            }
            break;
        }
        if (this.factionButtonBackground.Position.Y < 10)
          this.factionButtonBackground.Image = (Image) GFXLibrary.faction_button_background1;
        else if (this.factionButtonBackground.Position.Y < 50)
        {
          this.factionButtonBackground.Image = (Image) GFXLibrary.faction_button_background2;
        }
        else
        {
          if (this.factionButtonBackground.Position.Y >= 90)
            return;
          this.factionButtonBackground.Image = (Image) GFXLibrary.faction_button_background3;
        }
      }

      private void showAllClicked() => InterfaceMgr.Instance.setVillageTabSubMode(43, false);

      private void invitesClicked() => InterfaceMgr.Instance.setVillageTabSubMode(41, false);

      private void startFactionClicked() => InterfaceMgr.Instance.showStartFactionPanel();

      private void myFactionClicked()
      {
        InterfaceMgr.Instance.showFactionPanel(RemoteServices.Instance.UserFactionID);
      }

      private void forumClicked() => InterfaceMgr.Instance.setVillageTabSubMode(45, false);

      private void officersClicked() => InterfaceMgr.Instance.setVillageTabSubMode(46, false);

      private void diplomacyClicked() => InterfaceMgr.Instance.setVillageTabSubMode(44, false);

      private void chatClicked()
      {
        if (RemoteServices.Instance.UserFactionID < 0)
          return;
        InterfaceMgr.Instance.initChatPanel(5, RemoteServices.Instance.UserFactionID);
      }

      private void LeaveFaction()
      {
        RemoteServices.Instance.set_FactionLeave_UserCallBack(new RemoteServices.FactionLeave_UserCallBack(this.factionLeaveCallback));
        RemoteServices.Instance.FactionLeave();
        InterfaceMgr.Instance.closeGreyOut();
        this.leaveFactionPopup.Close();
      }

      private void ClosePopUp()
      {
        if (this.leaveFactionPopup == null)
          return;
        if (this.leaveFactionPopup.Created)
          this.leaveFactionPopup.Close();
        InterfaceMgr.Instance.closeGreyOut();
        this.leaveFactionPopup = (MyMessageBoxPopUp) null;
      }

      private void leaveFactionClicked()
      {
        this.ClosePopUp();
        InterfaceMgr.Instance.openGreyOutWindow(false);
        this.leaveFactionPopup = new MyMessageBoxPopUp();
        this.leaveFactionPopup.init(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FactionsPanel_Leave_Faction", "Leave Faction"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.LeaveFaction));
        this.leaveFactionPopup.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
      }

      public void factionLeaveCallback(FactionLeave_ReturnType returnData)
      {
        if (!returnData.Success)
          return;
        RemoteServices.Instance.UserFactionID = -1;
        GameEngine.Instance.World.FactionMembers = (FactionMemberData[]) null;
        GameEngine.Instance.World.FactionInvites = returnData.invites;
        GameEngine.Instance.World.FactionApplications = returnData.applications;
        GameEngine.Instance.World.FactionAllies = (int[]) null;
        GameEngine.Instance.World.FactionEnemies = (int[]) null;
        GameEngine.Instance.World.HouseAllies = (int[]) null;
        GameEngine.Instance.World.HouseEnemies = (int[]) null;
        GameEngine.Instance.World.HouseInfo = returnData.m_houseData;
        GameEngine.Instance.setNextFactionPage(-1);
        InterfaceMgr.Instance.getFactionTabBar().forceChangeTab(1);
      }

      private void goHereticClicked()
      {
        this.ClosePopUp();
        InterfaceMgr.Instance.openGreyOutWindow(false);
        this.leaveFactionPopup = new MyMessageBoxPopUp();
        this.leaveFactionPopup.setCustomYesText(SK.Text("FactionsSidebar_Heretic", "Go Heretic!"));
        this.leaveFactionPopup.setCustomNoText(SK.Text("FactionsSidebar_StayFree", "Stay Free"));
        this.leaveFactionPopup.init(SK.Text("FactionsPanel_go_heretic_1a", "Beware, going 'Heretic' will put you in the Wolfs faction. He will not let you leave, ever!") + Environment.NewLine + Environment.NewLine + SK.Text("FactionsPanel_go_heretic_1b", "You will win no prizes and not be permitted to attack the wolf or his allies, only his enemies. Consider this choice carefully, it is a hard and lonely road to take.") + Environment.NewLine, SK.Text("FactionsSidebar_Heretic", "Go Heretic!"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.goHereticClicked2));
        this.leaveFactionPopup.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
      }

      private void goHereticClicked2()
      {
        this.leaveFactionPopup.Close();
        this.leaveFactionPopup = new MyMessageBoxPopUp();
        this.leaveFactionPopup.setCustomYesText(SK.Text("FactionsSidebar_Heretic", "Go Heretic!"));
        this.leaveFactionPopup.setCustomNoText(SK.Text("FactionsSidebar_StayFree", "Stay Free"));
        this.leaveFactionPopup.init(SK.Text("FactionsPanel_go_heretic_2a", "Are you really sure you want to do this?") + Environment.NewLine + Environment.NewLine + SK.Text("FactionsPanel_go_heretic_2b", "This is your last chance, clicking 'Go Heretic' again, will permanently put you in the clutches of the wolf") + Environment.NewLine, SK.Text("FactionsSidebar_Heretic", "Go Heretic!"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.GoHeretic));
        this.leaveFactionPopup.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
      }

      private void GoHeretic()
      {
        RemoteServices.Instance.set_FactionApplicationProcessing_UserCallBack(new RemoteServices.FactionApplicationProcessing_UserCallBack(this.GoHereticCallback));
        RemoteServices.Instance.FactionApplicationGoHeretic();
        InterfaceMgr.Instance.closeGreyOut();
        this.leaveFactionPopup.Close();
      }

      private void GoHereticCallback(FactionApplicationProcessing_ReturnType returnData)
      {
        if (!returnData.Success || returnData.members == null)
          return;
        GameEngine.Instance.World.FactionMembers = returnData.members;
        GameEngine.Instance.World.YourFaction = returnData.yourFaction;
        InterfaceMgr.Instance.openGreyOutWindow(false);
        this.leaveFactionPopup = new MyMessageBoxPopUp();
        this.leaveFactionPopup.init(SK.Text("FactionsPanel_go_heretic_3b", "You are now in the service of the Wolf, good hunting..") + Environment.NewLine, SK.Text("FactionsSidebar_Heretic_3a", "Heretic!"), 6, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.HereticFinished));
        this.leaveFactionPopup.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
      }

      private void HereticFinished()
      {
        InterfaceMgr.Instance.closeGreyOut();
        this.leaveFactionPopup.Close();
        GameEngine.Instance.setNextFactionPage(-1);
        InterfaceMgr.Instance.getFactionTabBar().forceChangeTab(1);
      }

      public void showAllHousesClicked() => InterfaceMgr.Instance.setVillageTabSubMode(51, false);

      public void selectHouseClicked()
      {
        GameEngine.Instance.playInterfaceSound("FactionPanelSideBar_house");
        InterfaceMgr.Instance.showHousePanel(this.m_parent.ClickedControl.Data);
      }

      public void update()
      {
        if (!CustomSelfDrawPanel.FactionPanelSideBar.m_factionDataUpdated)
          return;
        CustomSelfDrawPanel.FactionPanelSideBar.m_factionDataUpdated = false;
        if (this.m_parent == null)
          return;
        switch (CustomSelfDrawPanel.FactionPanelSideBar.m_currentSidebarMode)
        {
          case 0:
            ((FactionInvitePanel) this.m_parent).init(false);
            break;
          case 1:
            ((FactionMyFactionPanel) this.m_parent).init(false);
            break;
          case 2:
            ((FactionAllFactionsPanel) this.m_parent).init(false);
            break;
          case 4:
            ((FactionDiplomacyPanel) this.m_parent).init(false);
            break;
        }
      }

      public static void logout()
      {
        CustomSelfDrawPanel.FactionPanelSideBar.m_lastFactionUpdate = DateTime.MinValue;
        CustomSelfDrawPanel.FactionPanelSideBar.m_factionDataUpdated = false;
      }

      public static void forceReUpdate()
      {
        CustomSelfDrawPanel.FactionPanelSideBar.m_lastFactionUpdate = DateTime.MinValue;
      }

      public static void downloadCurrentFactionInfo()
      {
        if ((DateTime.Now - CustomSelfDrawPanel.FactionPanelSideBar.m_lastFactionUpdate).TotalMinutes <= 5.0)
          return;
        CustomSelfDrawPanel.FactionPanelSideBar.m_lastFactionUpdate = DateTime.Now;
        RemoteServices.Instance.set_GetFactionData_UserCallBack(new RemoteServices.GetFactionData_UserCallBack(CustomSelfDrawPanel.FactionPanelSideBar.getFactionDataCallback));
        RemoteServices.Instance.GetFactionData(RemoteServices.Instance.UserFactionID, GameEngine.Instance.World.StoredFactionChangesPos);
      }

      public static void getFactionDataCallback(GetFactionData_ReturnType returnData)
      {
        if (!returnData.Success)
          return;
        if (returnData.factionsList != null)
          GameEngine.Instance.World.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
        GameEngine.Instance.World.FactionMembers = returnData.members;
        GameEngine.Instance.World.YourFaction = returnData.yourFaction;
        GameEngine.Instance.World.FactionInvites = returnData.invites;
        GameEngine.Instance.World.FactionApplications = returnData.applications;
        GameEngine.Instance.World.HouseInfo = returnData.m_houseData;
        GameEngine.Instance.World.HouseVoteInfo = returnData.m_houseVoteData;
        GameEngine.Instance.World.FactionAllies = returnData.yourAllies;
        GameEngine.Instance.World.FactionEnemies = returnData.yourEnemies;
        GameEngine.Instance.World.HouseAllies = returnData.yourHouseAllies;
        GameEngine.Instance.World.HouseEnemies = returnData.yourHouseEnemies;
        GameEngine.Instance.World.YourFactionVote = returnData.yourLeaderVote;
        CustomSelfDrawPanel.FactionPanelSideBar.m_factionDataUpdated = true;
      }

      public void mailFactionClicked()
      {
        FactionMemberData[] factionMembers = GameEngine.Instance.World.FactionMembers;
        if (factionMembers == null)
          return;
        List<string> stringList = new List<string>();
        foreach (FactionMemberData factionMemberData in factionMembers)
        {
          if (factionMemberData.userID != RemoteServices.Instance.UserID && factionMemberData.status >= 0)
            stringList.Add(factionMemberData.userName);
        }
        MailScreen.setFromFaction();
        InterfaceMgr.Instance.getMainTabBar().selectDummyTab(21);
        InterfaceMgr.Instance.mailTo(RemoteServices.Instance.UserID, stringList.ToArray());
      }
    }

    public enum CSD_Text_Alignment
    {
      TOP_LEFT,
      TOP_CENTER,
      TOP_RIGHT,
      CENTER_LEFT,
      CENTER_CENTER,
      CENTER_RIGHT,
      BOTTOM_LEFT,
      BOTTOM_CENTER,
      BOTTOM_RIGHT,
    }

    public interface ICardsPanel
    {
      void init(int cardsection);

      void update();
    }

    public class CSDImage : CustomSelfDrawPanel.CSDControl
    {
      protected Image image;
      private float alpha = 1f;
      private Color colorise = ARGBColors.White;
      private bool tile;
      private bool mirrorFlip;

      public Image Image
      {
        get => this.image;
        set
        {
          if (this.image != value)
            this.invalidate();
          this.image = value;
          this.setSizeToImage();
        }
      }

      public Image ImageNoInvalidate
      {
        get => this.image;
        set => this.image = value;
      }

      public Image ImageNoSizing
      {
        get => this.image;
        set
        {
          if (this.image != value)
            this.invalidate();
          this.image = value;
        }
      }

      public void setSizeToImage()
      {
        if (this.image == null)
          return;
        this.Size = this.image.Size;
      }

      public float Alpha
      {
        get => this.alpha;
        set => this.alpha = value;
      }

      public Color Colorise
      {
        get => this.colorise;
        set
        {
          if (this.colorise != value)
            this.invalidate();
          this.colorise = value;
        }
      }

      public bool Tile
      {
        get => this.tile;
        set => this.tile = value;
      }

      public bool MirrorFlip
      {
        get => this.mirrorFlip;
        set => this.mirrorFlip = value;
      }

      public override void draw(Point parentLocation)
      {
        if (this.image == null)
          return;
        Rectangle rectangle = this.Rectangle;
        if (this.Scale != 1.0)
        {
          rectangle.X = (int) ((double) rectangle.X * this.Scale);
          rectangle.Y = (int) ((double) rectangle.Y * this.Scale);
        }
        rectangle.X += parentLocation.X;
        rectangle.Y += parentLocation.Y;
        Rectangle source1 = new Rectangle(0, 0, this.image.Width, this.image.Height);
        if (!this.Tile)
        {
          if (this.Scale == 1.0)
          {
            if ((double) this.alpha == 1.0 && this.colorise == ARGBColors.White)
            {
              if (source1.Width == rectangle.Width && source1.Height == rectangle.Height)
              {
                if (!this.mirrorFlip && (double) this.Rotate == 0.0)
                  this.csd.drawImage(this.image, source1, rectangle);
                else
                  this.csd.drawImageMirrorRotate(this.image, source1, rectangle, this.mirrorFlip, this.Rotate, this.RotateCentre);
              }
              else
              {
                float width = (float) source1.Width;
                float height = (float) source1.Height;
                if (source1.Width != rectangle.Width)
                  width -= 0.999f;
                if (source1.Height != rectangle.Height)
                  height -= 0.999f;
                this.csd.drawImage(this.image, new RectangleF((float) source1.X, (float) source1.Y, width, height), new RectangleF((float) rectangle.X, (float) rectangle.Y, (float) rectangle.Width, (float) rectangle.Height));
              }
            }
            else
            {
              if ((double) this.alpha <= 0.0 && !(this.colorise != ARGBColors.White))
                return;
              if (source1.Width == rectangle.Width && source1.Height == rectangle.Height)
              {
                if (!this.mirrorFlip && (double) this.Rotate == 0.0)
                  this.csd.drawImage(this.image, source1, rectangle, this.alpha, this.colorise);
                else
                  this.csd.drawImageMirrorRotateAlpha(this.image, source1, rectangle, this.mirrorFlip, this.Rotate, this.RotateCentre, this.alpha);
              }
              else
              {
                float width = (float) source1.Width;
                float height = (float) source1.Height;
                if (source1.Width != rectangle.Width)
                  width -= 0.999f;
                if (source1.Height != rectangle.Height)
                  height -= 0.999f;
                this.csd.drawImage(this.image, new RectangleF((float) source1.X, (float) source1.Y, width, height), new RectangleF((float) rectangle.X, (float) rectangle.Y, (float) rectangle.Width, (float) rectangle.Height), this.alpha, this.colorise);
              }
            }
          }
          else if ((double) this.alpha == 1.0 && this.colorise == ARGBColors.White)
          {
            this.csd.drawImage(this.image, source1, rectangle, this.Scale);
          }
          else
          {
            if ((double) this.alpha <= 0.0 && !(this.colorise != ARGBColors.White))
              return;
            this.csd.drawImage(this.image, source1, rectangle, this.alpha, this.Scale, this.colorise);
          }
        }
        else if (this.image.Size.Width == 1)
        {
          RectangleF source2 = new RectangleF((float) source1.X, (float) source1.Y, 1f / 1000f, (float) source1.Height);
          RectangleF dest = new RectangleF((float) rectangle.X, (float) rectangle.Y, (float) rectangle.Width, (float) rectangle.Height);
          if ((double) this.alpha == 1.0 && this.colorise == ARGBColors.White)
            this.csd.drawImage(this.image, source2, dest);
          else
            this.csd.drawImage(this.image, source2, dest, this.alpha, this.colorise);
        }
        else
        {
          for (int index1 = 0; index1 < this.Size.Height; index1 += this.image.Size.Height)
          {
            for (int index2 = 0; index2 < this.Size.Width; index2 += this.image.Size.Width)
            {
              Rectangle dest = new Rectangle(rectangle.X + index2, rectangle.Y + index1, this.image.Width, this.image.Height);
              if ((double) this.alpha == 1.0 && this.colorise == ARGBColors.White)
                this.csd.drawImage(this.image, source1, dest);
              else if ((double) this.alpha > 0.0 || this.colorise != ARGBColors.White)
                this.csd.drawImage(this.image, source1, dest, this.alpha, this.colorise);
            }
          }
        }
      }
    }

    public class CSDImageAnim : CustomSelfDrawPanel.CSDImage
    {
      public BaseImage[] Frames;
      public int[] FrameData;
      public int FirstFrame;
      public bool Playing;
      public int CurrentFrame;
      public double Interval;
      public int Step = 1;
      private int CurrentStep;

      public void SetFrames(BaseImage[] frames)
      {
        this.Playing = false;
        this.Frames = frames;
        this.FrameData = new int[frames.Length];
        for (int index = 0; index < this.FrameData.Length; ++index)
          this.FrameData[index] = 0;
        this.CurrentFrame = 0;
        this.Interval = 33.0;
        this.CurrentStep = 0;
      }

      public void Animate(double now) => this.Animate(now, -1);

      public bool Animate(double now, int target)
      {
        if (this.Playing)
        {
          this.CurrentFrame = (this.CurrentFrame + 1) % this.Frames.Length;
          this.Image = (Image) this.Frames[this.CurrentFrame];
          if (this.FrameData[this.CurrentFrame] == target)
            this.Playing = false;
        }
        return this.Playing;
      }

      public void NoLoopAnim()
      {
        if (!this.Playing)
          return;
        ++this.CurrentStep;
        if (this.CurrentStep < this.Step)
          return;
        ++this.CurrentFrame;
        this.CurrentStep = 0;
        if (this.CurrentFrame >= this.Frames.Length)
        {
          this.Playing = false;
          if (this.parent == null)
            return;
          this.parent.removeControl((CustomSelfDrawPanel.CSDControl) this);
        }
        else
          this.ImageNoSizing = (Image) this.Frames[this.CurrentFrame];
      }
    }

    public class CSDVertImageScroller : CustomSelfDrawPanel.CSDControl
    {
      public CustomSelfDrawPanel.CSDImage[] Images;
      public Dictionary<int, int> ImageOffsets = new Dictionary<int, int>();
      public Point initialPosition;
      public bool scrolling;

      public void init(Point position, BaseImage[] sourceImages, int[] sourceIDs)
      {
        this.initialPosition = position;
        this.Position = position;
        this.Images = new CustomSelfDrawPanel.CSDImage[sourceImages.Length + 1];
        int height = 0;
        int width = 0;
        for (int index = 0; index <= sourceImages.Length; ++index)
        {
          CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage();
          if (index == sourceImages.Length)
          {
            control.Size = sourceImages[0].Size;
            control.Image = (Image) sourceImages[0];
          }
          else
          {
            control.Image = (Image) sourceImages[index];
            control.Size = sourceImages[index].Size;
          }
          if (index == 0)
            control.Position = new Point(0, 0);
          else
            control.Position = new Point(0, this.Images[index - 1].Y - control.Height);
          this.Images[index] = control;
          if (index < sourceIDs.Length && !this.ImageOffsets.ContainsKey(sourceIDs[index]))
            this.ImageOffsets.Add(sourceIDs[index], -control.Y);
          else if (index == sourceIDs.Length)
            this.ImageOffsets[sourceIDs[0]] = -control.Y;
          this.addControl((CustomSelfDrawPanel.CSDControl) control);
          if (this.Width < control.Width)
            width = control.Width;
          if (index < sourceImages.Length)
            height += control.Height;
        }
        this.Size = new Size(width, height);
        this.ClipRect = new Rectangle(0, 0, this.Images[0].Width, this.Images[0].Height);
      }

      public void scroll(int speed)
      {
        this.scrolling = true;
        this.Position = new Point(this.Position.X, this.Position.Y + speed);
        this.ClipRect = new Rectangle(this.ClipRect.X, this.ClipRect.Y - speed, this.ClipRect.Width, this.ClipRect.Height);
        if (this.Position.Y <= this.initialPosition.Y + this.Height)
          return;
        this.Position = new Point(this.Position.X, this.Position.Y - this.Height);
        this.ClipRect = new Rectangle(this.ClipRect.X, this.ClipRect.Y + this.Height, this.ClipRect.Width, this.ClipRect.Height);
      }

      public void scroll(int speed, int stop)
      {
        if (this.Position.Y - this.initialPosition.Y <= this.ImageOffsets[stop] && this.Position.Y - this.initialPosition.Y + speed >= this.ImageOffsets[stop])
        {
          this.Position = new Point(this.Position.X, this.initialPosition.Y + this.ImageOffsets[stop]);
          this.ClipRect = new Rectangle(this.ClipRect.X, -this.ImageOffsets[stop], this.ClipRect.Width, this.ClipRect.Height);
          this.scrolling = false;
        }
        else
          this.scroll(speed);
      }
    }

    public class CSDLabel : CustomSelfDrawPanel.CSDControl
    {
      private string text = "";
      private CustomSelfDrawPanel.CSD_Text_Alignment alignment;
      private Color color = ARGBColors.Black;
      private Color rolloverColor = ARGBColors.Black;
      private Color defaultColor = ARGBColors.Black;
      private Color dropShadowColor = ARGBColors.Black;
      private bool dropShadow;
      private Font font = FontManager.GetFont("Arial", 8.25f);

      public string Text
      {
        get => this.text;
        set
        {
          this.text = value;
          this.invalidate();
        }
      }

      public string TextDiffOnly
      {
        get => this.text;
        set
        {
          if (!(this.text != value))
            return;
          this.text = value;
          this.invalidate();
        }
      }

      public Size TextSize
      {
        get
        {
          Size size1 = new Size();
          Graphics graphics = this.csd.CreateGraphics();
          Size size2 = graphics.MeasureString(this.text, this.Font, this.Width).ToSize();
          graphics.Dispose();
          return size2;
        }
      }

      public Size TextSizeX
      {
        get
        {
          Size size1 = new Size();
          Graphics graphics = this.csd.CreateGraphics();
          Size size2 = graphics.MeasureString(this.text, this.Font, this.Width).ToSize();
          graphics.Dispose();
          size2.Width += 2;
          size2.Height += 2;
          return size2;
        }
      }

      public CustomSelfDrawPanel.CSD_Text_Alignment Alignment
      {
        get => this.alignment;
        set => this.alignment = value;
      }

      public Color Color
      {
        get => this.color;
        set
        {
          this.color = value;
          this.invalidate();
        }
      }

      public Color RolloverColor
      {
        get => this.rolloverColor;
        set
        {
          this.rolloverColor = value;
          this.invalidate();
          this.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.colourRollover), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.colourRolloff));
          this.defaultColor = this.color;
        }
      }

      private void colourRollover() => this.Color = this.rolloverColor;

      private void colourRolloff() => this.Color = this.defaultColor;

      public Color DropShadowColor
      {
        get => this.dropShadowColor;
        set
        {
          this.dropShadowColor = value;
          this.dropShadow = true;
          this.invalidate();
        }
      }

      public void clearDropShadow() => this.dropShadow = false;

      public Font Font
      {
        set => this.font = value;
        get => this.font;
      }

      public override void draw(Point parentLocation)
      {
        Rectangle rectangle = this.Rectangle;
        Font font = this.font;
        if (this.Scale != 1.0)
        {
          if ((double) this.font.SizeInPoints * this.Scale < 6.0)
            return;
          rectangle.X = (int) ((double) rectangle.X * this.Scale);
          rectangle.Y = (int) ((double) rectangle.Y * this.Scale);
          rectangle.Width = (int) ((double) rectangle.Width * this.Scale);
          rectangle.Height = (int) ((double) rectangle.Height * this.Scale);
          font = new Font(this.font.FontFamily, this.font.SizeInPoints * (float) this.Scale, this.font.Style);
        }
        rectangle.X += parentLocation.X;
        rectangle.Y += parentLocation.Y;
        if (this.dropShadow)
        {
          ++rectangle.X;
          ++rectangle.Y;
          this.csd.drawString(this.Text, rectangle, this.dropShadowColor, font, this.alignment);
          --rectangle.X;
          --rectangle.Y;
        }
        this.csd.drawString(this.Text, rectangle, this.color, font, this.alignment);
      }
    }

    public class CSDFloatingText : CustomSelfDrawPanel.CSDLabel
    {
      public int dx;
      public int dy;
      public int da;
      public Color BaseColor;
      public Color BaseDrop;
      public int currentAlpha;
      public double interval;
      public double lifespan;
      public double start;
      public double last;
      public bool live = true;

      public void init(
        Point pos,
        Size size,
        Color basecolor,
        Color dropcolor,
        int _dx,
        int _dy,
        int _da,
        string text,
        int fontsize,
        double _interval,
        double _lifespan,
        double _start,
        CustomSelfDrawPanel.CSDControl _parent)
      {
        this.Position = pos;
        this.Size = size;
        this.Text = text;
        this.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.Font = FontManager.GetFont("Arial", (float) fontsize, FontStyle.Bold);
        this.Color = basecolor;
        this.DropShadowColor = dropcolor;
        this.BaseColor = basecolor;
        this.BaseDrop = dropcolor;
        this.dx = _dx;
        this.dy = _dy;
        this.da = _da;
        this.interval = _interval;
        this.lifespan = _lifespan;
        this.start = _start;
        _parent.addControl((CustomSelfDrawPanel.CSDControl) this);
        this.currentAlpha = (int) byte.MaxValue;
      }

      public void move(double now)
      {
        if (!this.live)
          return;
        if (now - this.start < this.lifespan)
        {
          if (now - this.last < this.interval)
            return;
          this.Position = new Point(this.Position.X + this.dx, this.Position.Y + this.dy);
          this.currentAlpha += this.da;
          if (this.currentAlpha < 0)
            this.currentAlpha = 0;
          else if (this.currentAlpha > (int) byte.MaxValue)
            this.currentAlpha = (int) byte.MaxValue;
          this.Color = Color.FromArgb(this.currentAlpha, this.BaseColor);
          this.DropShadowColor = Color.FromArgb(this.currentAlpha, this.BaseDrop);
          this.last = now;
        }
        else
        {
          if (this.parent != null)
            this.parent.removeControl((CustomSelfDrawPanel.CSDControl) this);
          this.live = false;
          if (this.parent == null)
            return;
          this.parent.invalidate();
        }
      }
    }

    public class UICardsButtons : CustomSelfDrawPanel.CSDControl
    {
      public CustomSelfDrawPanel.CSDButton buyButton;
      public CustomSelfDrawPanel.CSDButton premiumButton;
      public CustomSelfDrawPanel.CSDButton crownsButton;
      public CustomSelfDrawPanel.CSDButton manageButton;
      public CustomSelfDrawPanel.CSDButton inviteButton;
      public CustomSelfDrawPanel.CSDButton offersButton;
      public PlayCardsWindow cardsWindow;
      public bool mAvailable;

      public UICardsButtons(PlayCardsWindow window)
      {
        this.cardsWindow = window;
        this.mAvailable = true;
        this.buyButton = new CustomSelfDrawPanel.CSDButton();
        this.premiumButton = new CustomSelfDrawPanel.CSDButton();
        this.crownsButton = new CustomSelfDrawPanel.CSDButton();
        this.manageButton = new CustomSelfDrawPanel.CSDButton();
        this.inviteButton = new CustomSelfDrawPanel.CSDButton();
        this.offersButton = new CustomSelfDrawPanel.CSDButton();
        this.buyButton.ImageNorm = (Image) GFXLibrary.cardpanel_RH_button_v2_getcards_normal;
        this.premiumButton.ImageNorm = (Image) GFXLibrary.cardpanel_RH_button_v2_getpremium_normal;
        this.crownsButton.ImageNorm = (Image) GFXLibrary.cardpanel_RH_button_v2_buycrowns_normal;
        this.manageButton.ImageNorm = (Image) GFXLibrary.cardpanel_RH_button_v2_choose_cards_normal;
        this.inviteButton.ImageNorm = (Image) GFXLibrary.cardpanel_RH_button_v2_friend_normal;
        this.offersButton.ImageNorm = (Image) GFXLibrary.cardpanel_RH_button_v2_offers_normal;
        this.buyButton.ImageOver = (Image) GFXLibrary.cardpanel_RH_button_v2_getcards_over;
        this.premiumButton.ImageOver = (Image) GFXLibrary.cardpanel_RH_button_v2_getpremium_over;
        this.crownsButton.ImageOver = (Image) GFXLibrary.cardpanel_RH_button_v2_buycrowns_over;
        this.manageButton.ImageOver = (Image) GFXLibrary.cardpanel_RH_button_v2_choose_cards_over;
        this.inviteButton.ImageOver = (Image) GFXLibrary.cardpanel_RH_button_v2_friend_over;
        this.offersButton.ImageOver = (Image) GFXLibrary.cardpanel_RH_button_v2_offers_over;
        CustomSelfDrawPanel.CSDButton csdButton = (CustomSelfDrawPanel.CSDButton) null;
        switch (window.CurrentPanelID)
        {
          case 2:
            csdButton = this.buyButton;
            break;
          case 4:
            csdButton = this.premiumButton;
            break;
          case 6:
            csdButton = this.manageButton;
            break;
          case 7:
            csdButton = this.crownsButton;
            break;
          case 9:
            csdButton = this.offersButton;
            break;
        }
        if (csdButton != null)
        {
          csdButton.ImageNorm = (Image) GFXLibrary.cardpanel_RH_button_back_normal;
          csdButton.ImageOver = (Image) GFXLibrary.cardpanel_RH_button_back_over;
          CustomSelfDrawPanel.CSDLabel control = new CustomSelfDrawPanel.CSDLabel();
          control.Position = new Point(0, 31);
          control.Size = csdButton.Size;
          control.Text = SK.Text("CARDS_BackToPlayCarads", "Back to Play Cards");
          control.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          control.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
          control.Color = ARGBColors.Black;
          csdButton.addControl((CustomSelfDrawPanel.CSDControl) control);
        }
        this.inviteButton.Position = new Point(11, 7);
        this.offersButton.Position = new Point(11, 7);
        this.manageButton.Position = new Point(11, 117);
        this.buyButton.Position = new Point(11, 227);
        this.premiumButton.Position = new Point(11, 337);
        this.crownsButton.Position = new Point(11, 447);
        this.buyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buyclick), "UICardsButtons_get_cards");
        this.premiumButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.premiumclick), "UICardsButtons_premium");
        this.crownsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cardsWindow.GetCrowns), "UICardsButtons_get_crowns");
        this.manageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.manageclick), "UICardsButtons_swap_cards");
        this.inviteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cardsWindow.InviteAFriend), "UICardsButtons_invite_a_friend");
        this.offersButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.offersclick), "UICardsButtons_show_offers");
        this.Size = new Size(200, this.buyButton.Height + this.premiumButton.Height + this.crownsButton.Height + this.manageButton.Height);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.buyButton);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.premiumButton);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.crownsButton);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.manageButton);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.offersButton);
        if (GameEngine.Instance.World.isBigpointAccount || Program.bigpointInstall || Program.aeriaInstall || Program.bigpointPartnerInstall || window.CurrentPanelID == 9)
          return;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.inviteButton);
      }

      public bool Available
      {
        get => this.mAvailable;
        set
        {
          this.mAvailable = value;
          this.buyButton.Enabled = this.mAvailable;
          this.crownsButton.Enabled = this.mAvailable;
          this.manageButton.Enabled = this.mAvailable;
          this.premiumButton.Enabled = this.mAvailable;
          this.inviteButton.Enabled = this.mAvailable && !GameEngine.Instance.cardsManager.PremiumOfferAvailable();
          this.inviteButton.Visible = !GameEngine.Instance.cardsManager.PremiumOfferAvailable();
          this.offersButton.Enabled = this.mAvailable && GameEngine.Instance.cardsManager.PremiumOfferAvailable();
          this.offersButton.Visible = GameEngine.Instance.cardsManager.PremiumOfferAvailable();
        }
      }

      public void buyclick() => this.cardsWindow.SwitchPanel(2);

      public void premiumclick() => this.cardsWindow.SwitchPanel(4);

      public void manageclick() => this.cardsWindow.SwitchPanel(6);

      public void offersclick()
      {
        this.cardsWindow.SwitchPanel(9);
        GameEngine.Instance.cardsManager.PremiumOffersViewed = true;
      }
    }

    public class CSDArea : CustomSelfDrawPanel.CSDControl
    {
    }

    public class CSDScrollLabel : CustomSelfDrawPanel.CSDControl
    {
      private bool dirty = true;
      private string text = "";
      private int verticalOffset;
      private Color color = ARGBColors.Black;
      private Font font = FontManager.GetFont("Arial", 8.25f);
      private CustomSelfDrawPanel.CSDScrollLabel.CSD_TextHeightChanged textHeightDelegate;
      private int textHeight;

      public string Text
      {
        get => this.text;
        set
        {
          this.text = value;
          this.invalidate();
          this.dirty = true;
        }
      }

      public int VerticalOffset
      {
        get => this.verticalOffset;
        set
        {
          this.verticalOffset = value;
          this.invalidate();
        }
      }

      public Color Color
      {
        get => this.color;
        set
        {
          this.color = value;
          this.invalidate();
        }
      }

      public Font Font
      {
        set
        {
          this.font = value;
          this.dirty = true;
        }
      }

      public void setTextHeightChangedCallback(
        CustomSelfDrawPanel.CSDScrollLabel.CSD_TextHeightChanged callback)
      {
        this.textHeightDelegate = callback;
      }

      public int TextHeight => this.textHeight;

      public override void draw(Point parentLocation)
      {
        if (this.dirty)
        {
          this.textHeight = (int) this.csd.getStringBounds(this.Text, this.Rectangle.Width, this.font).Height;
          this.dirty = false;
          if (this.textHeightDelegate != null)
            this.textHeightDelegate(this.textHeight);
        }
        Rectangle rectangle = this.Rectangle;
        Font font = this.font;
        if (this.Scale != 1.0)
        {
          if ((double) this.font.SizeInPoints * this.Scale < 6.0)
            return;
          rectangle.X = (int) ((double) rectangle.X * this.Scale);
          rectangle.Y = (int) ((double) rectangle.Y * this.Scale);
          rectangle.Width = (int) ((double) rectangle.Width * this.Scale);
          rectangle.Height = (int) ((double) rectangle.Height * this.Scale);
          font = new Font(this.font.FontFamily, this.font.SizeInPoints * (float) this.Scale, this.font.Style);
        }
        rectangle.X += parentLocation.X;
        rectangle.Y += parentLocation.Y;
        this.csd.setClipRegion(rectangle);
        rectangle.Y -= this.verticalOffset;
        rectangle.Height = 100000;
        this.csd.drawString(this.Text, rectangle, this.color, font, CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT);
        this.csd.restoreClipRegion();
      }

      public delegate void CSD_TextHeightChanged(int textHeight);
    }

    public class CSDFactionFlagImage : CustomSelfDrawPanel.CSDImage
    {
      private ColorMap[] colourMap;
      private CustomSelfDrawPanel.CSDImage flagOverlayImage;

      public void createFromFlagData(int flagData)
      {
        int flag = 0;
        int colour1 = 0;
        int colour2 = 0;
        int colour3 = 0;
        int colour4 = 0;
        FactionData.getFlagData(flagData, ref flag, ref colour1, ref colour2, ref colour3, ref colour4);
        if (flag >= 0 && flag < GFXLibrary.factionFlags.Length)
          this.Image = (Image) GFXLibrary.factionFlags[flag];
        else
          this.Image = (Image) GFXLibrary.factionFlags[0];
        this.ColourMap = FactionData.getColourMap(colour1, colour2, colour3, colour4, (int) byte.MaxValue);
      }

      public override Size Size
      {
        get => base.Size;
        set => base.Size = value;
      }

      public ColorMap[] ColourMap
      {
        get => this.colourMap;
        set => this.colourMap = value;
      }

      public override void draw(Point parentLocation)
      {
        if (this.image == null)
          return;
        Rectangle rectangle = this.Rectangle;
        rectangle.X += parentLocation.X;
        rectangle.Y += parentLocation.Y;
        Rectangle source = new Rectangle(0, 0, this.image.Width, this.image.Height);
        if (this.Scale != 1.0)
        {
          rectangle.Width = (int) ((double) rectangle.Width * this.Scale);
          rectangle.Height = (int) ((double) rectangle.Height * this.Scale);
        }
        if (this.colourMap != null)
          this.csd.drawImageColourMap(this.image, source, rectangle, this.colourMap);
        else
          this.csd.drawImage(this.image, source, rectangle);
        if (this.Scale == 1.0)
          this.csd.drawImage((Image) GFXLibrary.faction_flag_outline_100, source, rectangle);
        else if (this.Scale > 0.40000000596046448)
          this.csd.drawImage((Image) GFXLibrary.faction_flag_outline_50, new Rectangle(0, 0, GFXLibrary.faction_flag_outline_50.Width, GFXLibrary.faction_flag_outline_50.Height), rectangle);
        else
          this.csd.drawImage((Image) GFXLibrary.faction_flag_outline_25, new Rectangle(0, 0, GFXLibrary.faction_flag_outline_25.Width, GFXLibrary.faction_flag_outline_25.Height), rectangle);
      }
    }

    public class CSDCheckBox : CustomSelfDrawPanel.CSDImage
    {
      private Image checkedImage;
      private Image uncheckedImage;
      private Image checkedOverImage;
      private Image uncheckedOverImage;
      private CustomSelfDrawPanel.CSDArea clickArea;
      private CustomSelfDrawPanel.CSDLabel textLabel;
      private bool over;
      private bool boxChecked;
      private CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate checkChangedDelegate;

      public CSDCheckBox()
      {
        this.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggled), "Generic_check_box_toggled");
        this.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.enterCB), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.leaveCB));
      }

      public CustomSelfDrawPanel.CSDLabel CBLabel
      {
        get
        {
          if (this.textLabel == null)
          {
            this.textLabel = new CustomSelfDrawPanel.CSDLabel();
            this.textLabel.CustomTooltipID = this.CustomTooltipID;
            this.textLabel.CustomTooltipData = this.CustomTooltipData;
            this.addControl((CustomSelfDrawPanel.CSDControl) this.textLabel);
          }
          return this.textLabel;
        }
      }

      public override void onClear() => this.textLabel = (CustomSelfDrawPanel.CSDLabel) null;

      public Image CheckedImage
      {
        set => this.checkedImage = value;
      }

      public Image UncheckedImage
      {
        set
        {
          this.uncheckedImage = value;
          this.Image = value;
        }
      }

      public Image CheckedOverImage
      {
        set => this.checkedOverImage = value;
      }

      public Image UncheckedOverImage
      {
        set => this.uncheckedOverImage = value;
      }

      public bool isMouseOver() => this.over;

      public bool Checked
      {
        get => this.boxChecked;
        set
        {
          this.boxChecked = value;
          this.updateImage();
        }
      }

      private void updateImage()
      {
        if (this.boxChecked)
        {
          if (!this.over || this.checkedOverImage == null)
          {
            if (this.Image != this.checkedImage)
              this.Image = this.checkedImage;
          }
          else if (this.Image != this.checkedOverImage)
            this.Image = this.checkedOverImage;
        }
        else if (!this.over || this.uncheckedOverImage == null)
        {
          if (this.Image != this.uncheckedImage)
            this.Image = this.uncheckedImage;
        }
        else if (this.Image != this.uncheckedOverImage)
          this.Image = this.uncheckedOverImage;
        this.invalidate();
      }

      private void toggled()
      {
        this.Checked = !this.boxChecked;
        if (this.checkChangedDelegate == null)
          return;
        this.csd.ClickedControl = (CustomSelfDrawPanel.CSDControl) this;
        this.checkChangedDelegate();
      }

      private void enterCB()
      {
        this.over = true;
        this.updateImage();
      }

      private void leaveCB()
      {
        this.over = false;
        this.updateImage();
      }

      public void setCheckChangedDelegate(
        CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate newDelegate)
      {
        this.checkChangedDelegate = newDelegate;
        this.initClickArea();
      }

      public void initClickArea()
      {
        if (this.CBLabel == null || this.clickArea != null || this.csd == null)
          return;
        Rectangle rectangle = new Rectangle();
        rectangle.X = this.Image.Width;
        rectangle.Y = this.CBLabel.Y;
        Size textSize = this.CBLabel.TextSize;
        rectangle.Width = this.CBLabel.Position.X + textSize.Width;
        rectangle.Height = textSize.Height;
        this.clickArea = new CustomSelfDrawPanel.CSDArea();
        this.clickArea.Position = rectangle.Location;
        this.clickArea.Size = rectangle.Size;
        this.clickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggled), "Generic_check_box_toggled");
        this.addControl((CustomSelfDrawPanel.CSDControl) this.clickArea);
      }

      public override void addedToParent() => this.initClickArea();

      public delegate void CSD_CheckChangedDelegate();
    }

    public class CSDColorBar : CustomSelfDrawPanel.CSDControl
    {
      private Image[] images = new Image[8];
      private double number;
      private double maxNumber = 1.0;
      private int leftMargin;
      private int rightMargin;
      private int topMargin;
      private int bottomMargin;
      private double markerValue;
      private bool markerShown;

      public void setImages(
        Image positiveBack,
        Image positiveLeft,
        Image positiveMid,
        Image positiveRight,
        Image negativeBack,
        Image negativeLeft,
        Image negativeMid,
        Image negativeRight)
      {
        this.invalidate();
        this.images[0] = positiveBack;
        this.images[1] = positiveLeft;
        this.images[2] = positiveMid;
        this.images[3] = positiveRight;
        this.images[4] = negativeBack;
        this.images[5] = negativeLeft;
        this.images[6] = negativeMid;
        this.images[7] = negativeRight;
        this.Size = this.images[0].Size;
      }

      public double Number
      {
        get => this.number;
        set
        {
          if (this.number != value)
            this.invalidate();
          this.number = value;
        }
      }

      public double MaxValue
      {
        get => this.maxNumber;
        set
        {
          if (this.maxNumber != value)
            this.invalidate();
          this.maxNumber = value;
        }
      }

      public void SetMargin(int lm, int tm, int rm, int bm)
      {
        this.leftMargin = lm;
        this.rightMargin = rm;
        this.topMargin = tm;
        this.bottomMargin = bm;
      }

      public void setMarker(double marker)
      {
        this.markerValue = marker;
        this.markerShown = true;
        this.invalidate();
      }

      public void clearMarker()
      {
        this.markerShown = false;
        this.invalidate();
      }

      public override void draw(Point parentLocation)
      {
        Rectangle rectangle = this.Rectangle;
        rectangle.X += parentLocation.X;
        rectangle.Y += parentLocation.Y;
        int index1 = 0;
        if (this.number < 0.0)
          index1 = 4;
        Rectangle source = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
        this.csd.drawImage(this.images[index1], source, rectangle);
        rectangle.X += this.leftMargin;
        rectangle.Y += this.topMargin;
        rectangle.Width -= this.leftMargin + this.rightMargin;
        rectangle.Height -= this.topMargin + this.bottomMargin;
        if (this.number != 0.0)
        {
          double num1 = Math.Abs(this.number);
          if (num1 > this.maxNumber)
            num1 = this.maxNumber;
          int num2 = (int) (((double) (rectangle.Width - this.images[index1 + 1].Size.Width - this.images[index1 + 3].Size.Width) - 1.0) / this.maxNumber * num1) - 1;
          ++rectangle.X;
          ++rectangle.Y;
          this.csd.drawImage(this.images[index1 + 1], rectangle.Location);
          Point location = rectangle.Location;
          location.X += this.images[index1 + 1].Size.Width;
          if (num2 > 0)
          {
            for (int index2 = 0; index2 < num2; ++index2)
            {
              Point dest = location;
              dest.X += index2;
              this.csd.drawImage(this.images[index1 + 2], dest);
            }
          }
          location.X += num2;
          this.csd.drawImage(this.images[index1 + 3], location);
          --rectangle.X;
          --rectangle.Y;
        }
        if (!this.markerShown)
          return;
        double num3 = Math.Abs(this.markerValue);
        if (num3 > this.maxNumber)
          num3 = this.maxNumber;
        double num4 = ((double) (this.Size.Width - this.images[index1 + 1].Size.Width - this.images[index1 + 3].Size.Width) - 1.0) / this.maxNumber * num3;
        this.csd.drawLine(ARGBColors.Black, new Point((int) ((double) (rectangle.X + 1) + num4), rectangle.Y), new Point((int) ((double) (rectangle.X + 1) + num4), rectangle.Y + rectangle.Height - 2));
      }
    }

    public class CSDButton : CustomSelfDrawPanel.CSDControl
    {
      private Image imageNorm;
      private Image imageOver;
      private Image imageClick;
      private Image imageHighlight;
      private Image imageIcon;
      private Point imageIconPosition = new Point(0, 0);
      private float imageIconAlpha = 1f;
      private Rectangle imageIconClipRect = new Rectangle(0, 0, 0, 0);
      private float alpha = 1f;
      private bool overBrighten;
      private int textYOffset = -3;
      private bool lateTextRender;
      private bool active = true;
      private bool moveOnClick = true;
      private bool useTextSize;
      private Color fillRectOverColor = ARGBColors.White;
      private bool fillRectOverVariant;
      private Color fillRectColor = ARGBColors.White;
      private bool fillRectVariant;
      private bool forceFillRect;
      private bool stretchButtons;
      private CustomSelfDrawPanel.CSDHorzExtendingPanel normalExt;
      private CustomSelfDrawPanel.CSDHorzExtendingPanel overExt;
      private CustomSelfDrawPanel.CSDHorzExtendingPanel clickExt;
      public CustomSelfDrawPanel.CSDLabel Text;
      public CustomSelfDrawPanel.CSDLabel Text2;
      protected CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate buttonMouseOverDelegate;
      protected CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate buttonMouseLeaveDelegate;

      public CSDButton()
      {
        this.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.buttonOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.buttonLeave));
        this.setMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonDown), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonUp));
        this.Text = new CustomSelfDrawPanel.CSDLabel();
        this.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.Text.setParent((CustomSelfDrawPanel.CSDControl) this);
      }

      public Image ImageNorm
      {
        get => this.imageNorm;
        set
        {
          if (this.imageNorm != value)
            this.invalidate();
          this.imageNorm = value;
          this.setSizeToImage();
        }
      }

      public Image ImageNormAndOver
      {
        get => this.imageNorm;
        set
        {
          if (this.imageNorm != value)
            this.invalidate();
          this.imageNorm = value;
          this.imageOver = value;
          this.setSizeToImage();
        }
      }

      public Image ImageOver
      {
        get => this.imageOver;
        set
        {
          if (this.imageOver != value)
            this.invalidate();
          this.imageOver = value;
        }
      }

      public Image ImageClick
      {
        get => this.imageClick;
        set
        {
          if (this.imageClick != value)
            this.invalidate();
          this.imageClick = value;
        }
      }

      public Image ImageHighlight
      {
        get => this.imageHighlight;
        set
        {
          if (this.imageHighlight != value)
            this.invalidate();
          this.imageHighlight = value;
        }
      }

      public Image ImageIcon
      {
        get => this.imageIcon;
        set
        {
          if (this.imageIcon != value)
            this.invalidate();
          this.imageIcon = value;
        }
      }

      public Point ImageIconPosition
      {
        get => this.imageIconPosition;
        set => this.imageIconPosition = value;
      }

      public float ImageIconAlpha
      {
        get => this.imageIconAlpha;
        set
        {
          if ((double) this.imageIconAlpha != (double) value)
            this.invalidate();
          this.imageIconAlpha = value;
        }
      }

      public Rectangle ImageIconClipRect
      {
        get => this.imageIconClipRect;
        set => this.imageIconClipRect = value;
      }

      public void setSizeToImage()
      {
        if (this.imageNorm == null)
          return;
        this.Size = this.imageNorm.Size;
        if (this.useTextSize)
          return;
        this.Text.Size = this.Size;
      }

      public float Alpha
      {
        get => this.alpha;
        set => this.alpha = value;
      }

      public bool OverBrighten
      {
        get => this.overBrighten;
        set => this.overBrighten = value;
      }

      public int TextYOffset
      {
        get => this.textYOffset;
        set => this.textYOffset = value;
      }

      public bool LateTextRender
      {
        get => this.lateTextRender;
        set => this.lateTextRender = value;
      }

      public bool Active
      {
        get => this.active;
        set => this.active = value;
      }

      public bool MoveOnClick
      {
        get => this.moveOnClick;
        set => this.moveOnClick = value;
      }

      public bool UseTextSize
      {
        get => this.useTextSize;
        set => this.useTextSize = value;
      }

      public Color FillRectOverColor
      {
        get => this.fillRectOverColor;
        set
        {
          this.fillRectOverColor = value;
          this.fillRectOverVariant = true;
        }
      }

      public bool FillRectVariant
      {
        get => this.fillRectVariant;
        set => this.fillRectVariant = value;
      }

      public Color FillRectColor
      {
        get => this.fillRectColor;
        set
        {
          this.fillRectColor = value;
          this.fillRectVariant = true;
        }
      }

      public bool ForceFillRect
      {
        get => this.forceFillRect;
        set => this.forceFillRect = value;
      }

      public void createSubText(string text)
      {
        this.Text2 = new CustomSelfDrawPanel.CSDLabel();
        this.Text2.Size = this.Size;
        this.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.Text2.Text = text;
        this.Text2.setParent((CustomSelfDrawPanel.CSDControl) this);
      }

      public void setNormalExtImage(Image left, Image mid, Image right)
      {
        this.stretchButtons = true;
        this.normalExt = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        this.normalExt.Size = this.Size;
        this.normalExt.Position = new Point(0, 0);
        this.normalExt.Create(left, mid, right);
        this.normalExt.setParent((CustomSelfDrawPanel.CSDControl) this);
        if (this.useTextSize)
          return;
        this.Text.Size = this.Size;
      }

      public void setOverExtImage(Image left, Image mid, Image right)
      {
        this.stretchButtons = true;
        this.overExt = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        this.overExt.Size = this.Size;
        this.overExt.Position = new Point(0, 0);
        this.overExt.Create(left, mid, right);
        this.overExt.setParent((CustomSelfDrawPanel.CSDControl) this);
        if (this.useTextSize)
          return;
        this.Text.Size = this.Size;
      }

      public void setClickExtImage(Image left, Image mid, Image right)
      {
        this.stretchButtons = true;
        this.clickExt = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        this.clickExt.Size = this.Size;
        this.clickExt.Position = new Point(0, 0);
        this.clickExt.Create(left, mid, right);
        if (this.useTextSize)
          return;
        this.Text.Size = this.Size;
      }

      public void setButtonMouseOverDelegate(
        CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate overDelegate,
        CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate leaveDelegate)
      {
        this.buttonMouseOverDelegate = overDelegate;
        this.buttonMouseLeaveDelegate = leaveDelegate;
      }

      public override void draw(Point parentLocation)
      {
        Rectangle rectangle = this.Rectangle;
        rectangle.X += parentLocation.X;
        rectangle.Y += parentLocation.Y;
        Point point = new Point(0, this.textYOffset);
        Image image = this.imageNorm;
        Rectangle source1 = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
        if (image != null)
          source1 = new Rectangle(0, 0, image.Width, image.Height);
        if (this.active)
        {
          if (this.stretchButtons)
          {
            CustomSelfDrawPanel.CSDHorzExtendingPanel horzExtendingPanel = this.normalExt;
            if (this.mouseDownFlag)
            {
              if (this.clickExt != null)
              {
                horzExtendingPanel = this.clickExt;
                if (this.moveOnClick)
                {
                  ++point.X;
                  ++point.Y;
                }
              }
              else
              {
                if (this.overExt != null)
                  horzExtendingPanel = this.overExt;
                if (this.moveOnClick)
                {
                  ++rectangle.X;
                  ++rectangle.Y;
                }
              }
            }
            else if (this.mouseOverFlag && this.overExt != null)
              horzExtendingPanel = this.overExt;
            if (horzExtendingPanel != null)
            {
              float alpha = this.alpha;
              if (!this.Enabled)
                alpha /= 2f;
              Point parentLocation1 = new Point(rectangle.X, rectangle.Y);
              horzExtendingPanel.forceDraw(parentLocation1, alpha);
            }
          }
          else if (this.imageHighlight == null)
          {
            if (this.mouseDownFlag)
            {
              if (this.imageClick != null)
              {
                image = this.imageClick;
                if (this.moveOnClick)
                {
                  ++point.X;
                  ++point.Y;
                }
              }
              else
              {
                image = this.imageOver;
                if (this.moveOnClick)
                {
                  ++rectangle.X;
                  ++rectangle.Y;
                }
              }
            }
            else if (this.mouseOverFlag)
              image = this.imageOver;
          }
          else if (this.mouseDownFlag && this.moveOnClick)
          {
            ++rectangle.X;
            ++rectangle.Y;
          }
        }
        float alpha1 = this.alpha;
        if (!this.Enabled)
          alpha1 /= 2f;
        if (image != null)
        {
          if ((double) alpha1 == 1.0)
            this.csd.drawImage(image, source1, rectangle);
          else
            this.csd.drawImage(image, source1, rectangle, alpha1);
        }
        else if (this.mouseOverFlag && this.imageOver == null && this.overBrighten)
        {
          if ((double) alpha1 == 1.0)
            this.csd.drawImageBrighten(this.imageNorm, source1, rectangle, 1f);
          else
            this.csd.drawImageBrighten(this.imageNorm, source1, rectangle, alpha1);
        }
        if (this.active && this.mouseOverFlag && this.imageHighlight != null)
          this.csd.drawImage(this.imageHighlight, source1, rectangle);
        if (this.fillRectVariant && !this.mouseOverFlag && !this.mouseDownFlag)
          this.csd.fillRect(rectangle, this.fillRectColor);
        if (this.fillRectOverVariant && (this.mouseOverFlag || this.mouseDownFlag || this.forceFillRect))
          this.csd.fillRect(rectangle, this.fillRectOverColor);
        if (!this.lateTextRender)
        {
          if (this.Text.Text.Length > 0)
          {
            if (image == null && !this.useTextSize)
              this.Text.Size = this.Size;
            Point parentLocation2 = new Point(rectangle.X, rectangle.Y);
            parentLocation2.X += point.X;
            parentLocation2.Y += point.Y;
            this.Text.draw(parentLocation2);
          }
          if (this.Text2 != null && this.Text2.Text.Length > 0)
          {
            Point parentLocation3 = new Point(rectangle.X, rectangle.Y);
            parentLocation3.X += point.X;
            parentLocation3.Y += point.Y;
            this.Text2.draw(parentLocation3);
          }
        }
        if (this.imageIcon != null)
        {
          Rectangle dest = new Rectangle(rectangle.X + this.imageIconPosition.X, rectangle.Y + this.imageIconPosition.Y, this.imageIcon.Width, this.imageIcon.Height);
          Rectangle source2 = new Rectangle(0, 0, this.imageIcon.Width, this.imageIcon.Height);
          if (!this.imageIconClipRect.IsEmpty)
            this.csd.setClipRegion(new Rectangle(rectangle.X + this.imageIconPosition.X + this.imageIconClipRect.X, rectangle.Y + this.imageIconPosition.Y + this.imageIconClipRect.Y, this.imageIconClipRect.Width, this.imageIconClipRect.Height));
          if ((double) this.imageIconAlpha == 1.0)
            this.csd.drawImage(this.imageIcon, source2, dest);
          else
            this.csd.drawImage(this.imageIcon, source2, dest, this.imageIconAlpha);
          if (!this.imageIconClipRect.IsEmpty)
            this.csd.restoreClipRegion();
        }
        if (!this.lateTextRender)
          return;
        if (this.Text.Text.Length > 0)
        {
          if (image == null && !this.useTextSize)
            this.Text.Size = this.Size;
          Point parentLocation4 = new Point(rectangle.X, rectangle.Y);
          parentLocation4.X += point.X;
          parentLocation4.Y += point.Y;
          this.Text.draw(parentLocation4);
        }
        if (this.Text2 == null || this.Text2.Text.Length <= 0)
          return;
        Point parentLocation5 = new Point(rectangle.X, rectangle.Y);
        parentLocation5.X += point.X;
        parentLocation5.Y += point.Y;
        this.Text2.draw(parentLocation5);
      }

      private void buttonOver()
      {
        this.invalidate();
        if (this.buttonMouseOverDelegate == null)
          return;
        this.buttonMouseOverDelegate();
      }

      private void buttonLeave()
      {
        this.invalidate();
        if (this.buttonMouseLeaveDelegate == null)
          return;
        this.buttonMouseLeaveDelegate();
      }

      private void buttonDown() => this.invalidate();

      private void buttonUp() => this.invalidate();
    }

    public class CSDExtendingPanel : CustomSelfDrawPanel.CSDControl
    {
      public CustomSelfDrawPanel.CSDImage TopLeft = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage TopMid = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage TopRight = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage Left = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage Mid = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage Right = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage BottomLeft = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage BottomMid = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage BottomRight = new CustomSelfDrawPanel.CSDImage();
      private float alpha = 1f;

      public float Alpha
      {
        get => this.alpha;
        set
        {
          this.alpha = (double) value <= 1.0 ? value : value / 256f;
          this.TopLeft.Alpha = this.alpha;
          this.TopRight.Alpha = this.alpha;
          this.TopMid.Alpha = this.alpha;
          this.Left.Alpha = this.alpha;
          this.Right.Alpha = this.alpha;
          this.Mid.Alpha = this.alpha;
          this.BottomLeft.Alpha = this.alpha;
          this.BottomMid.Alpha = this.alpha;
          this.BottomRight.Alpha = this.alpha;
        }
      }

      public void ForceTiling()
      {
        this.TopMid.Tile = true;
        this.Left.Tile = true;
        this.BottomMid.Tile = true;
        this.Right.Tile = true;
        this.Mid.Tile = true;
      }

      public void Create(
        Image topLeftImage,
        Image topMidImage,
        Image topRightImage,
        Image leftImage,
        Image midImage,
        Image rightImage,
        Image bottomLeftImage,
        Image bottomMidImage,
        Image bottomRightImage)
      {
        this.TopLeft.Image = topLeftImage;
        this.TopLeft.Position = new Point(0, 0);
        this.TopLeft.Alpha = this.alpha;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.TopLeft);
        this.TopRight.Image = topRightImage;
        this.TopRight.Position = new Point(this.Width - this.TopRight.Image.Width, 0);
        this.TopRight.Alpha = this.alpha;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.TopRight);
        this.TopMid.Image = topMidImage;
        this.TopMid.Position = new Point(this.TopLeft.Image.Width, 0);
        this.TopMid.Size = new Size(this.Width - this.TopLeft.Image.Width - this.TopRight.Image.Width, this.TopMid.Image.Height);
        this.TopMid.Alpha = this.alpha;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.TopMid);
        this.Left.Image = leftImage;
        this.Left.Position = new Point(0, this.TopLeft.Image.Height);
        this.Left.Size = new Size(this.Left.Image.Width, this.Height - this.TopLeft.Image.Height - bottomLeftImage.Width);
        this.Left.Alpha = this.alpha;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.Left);
        this.Right.Image = rightImage;
        this.Right.Position = new Point(this.Width - this.Right.Image.Width, this.TopRight.Image.Height);
        this.Right.Size = new Size(this.Right.Image.Width, this.Height - this.TopRight.Image.Height - bottomRightImage.Width);
        this.Right.Alpha = this.alpha;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.Right);
        this.BottomLeft.Image = bottomLeftImage;
        this.BottomLeft.Position = new Point(0, this.Height - this.BottomLeft.Height);
        this.BottomLeft.Alpha = this.alpha;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.BottomLeft);
        this.BottomRight.Image = bottomRightImage;
        this.BottomRight.Position = new Point(this.Width - this.BottomRight.Image.Width, this.Height - this.BottomRight.Height);
        this.BottomRight.Alpha = this.alpha;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.BottomRight);
        this.BottomMid.Image = bottomMidImage;
        this.BottomMid.Position = new Point(this.BottomLeft.Image.Width, this.Height - this.BottomMid.Image.Height);
        this.BottomMid.Size = new Size(this.Width - this.BottomLeft.Image.Width - this.BottomRight.Image.Width, this.BottomMid.Image.Height);
        this.BottomMid.Alpha = this.alpha;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.BottomMid);
        if (midImage == null)
          return;
        this.Mid.Image = midImage;
        this.Mid.Position = new Point(this.Left.Image.Width, this.TopMid.Image.Height);
        this.Mid.Size = new Size(this.Width - this.Left.Image.Width - this.Right.Image.Width, this.Height - this.TopMid.Image.Height - this.BottomMid.Image.Height);
        this.Mid.Alpha = this.alpha;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.Mid);
      }
    }

    public class CSDHorzExtendingPanel : CustomSelfDrawPanel.CSDControl
    {
      public CustomSelfDrawPanel.CSDImage Left = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage Mid = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage Right = new CustomSelfDrawPanel.CSDImage();

      public void Create(Image leftImage, Image midImage, Image rightImage)
      {
        this.removeControl((CustomSelfDrawPanel.CSDControl) this.Left);
        this.removeControl((CustomSelfDrawPanel.CSDControl) this.Right);
        this.removeControl((CustomSelfDrawPanel.CSDControl) this.Mid);
        this.Left.Image = leftImage;
        this.Left.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.Left);
        this.Right.Image = rightImage;
        this.Right.Position = new Point(this.Width - this.Right.Image.Width, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.Right);
        this.Mid.Image = midImage;
        this.Mid.Position = new Point(this.Left.Image.Width, 0);
        this.Mid.Size = new Size(this.Width - this.Left.Image.Width - this.Right.Image.Width, this.Mid.Image.Height);
        this.Mid.Tile = true;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.Mid);
        this.Size = new Size(this.Size.Width, this.Left.Image.Height);
      }

      public void CreateX(
        Image leftImage,
        Image midImage,
        Image rightImage,
        int midShorten,
        int rightExtra)
      {
        this.removeControl((CustomSelfDrawPanel.CSDControl) this.Left);
        this.removeControl((CustomSelfDrawPanel.CSDControl) this.Right);
        this.removeControl((CustomSelfDrawPanel.CSDControl) this.Mid);
        this.Left.Image = leftImage;
        this.Left.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.Left);
        this.Right.Image = rightImage;
        this.Right.Position = new Point(this.Width - this.Right.Image.Width + rightExtra, 0);
        this.Mid.Image = midImage;
        this.Mid.Position = new Point(this.Left.Image.Width, 0);
        this.Mid.Size = new Size(this.Width - this.Left.Image.Width - this.Right.Image.Width - midShorten, this.Mid.Image.Height);
        this.Mid.Tile = true;
        this.Mid.ClipRect = new Rectangle(0, 0, this.Mid.Size.Width, this.Mid.Size.Height);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.Mid);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.Right);
        this.Size = new Size(this.Size.Width, this.Left.Image.Height);
      }

      public void resize()
      {
        this.Right.Position = new Point(this.Width - this.Right.Image.Width, 0);
        this.Mid.Position = new Point(this.Left.Image.Width, 0);
        this.Mid.Size = new Size(this.Width - this.Left.Image.Width - this.Right.Image.Width, this.Mid.Image.Height);
      }

      public void forceDraw(Point parentLocation, float alpha)
      {
        Rectangle rectangle = this.Rectangle;
        rectangle.X += parentLocation.X;
        rectangle.Y += parentLocation.Y;
        Point parentLocation1 = new Point(rectangle.X, rectangle.Y);
        this.Left.Alpha = alpha;
        this.Left.draw(parentLocation1);
        this.Mid.Alpha = alpha;
        this.Mid.draw(parentLocation1);
        this.Right.Alpha = alpha;
        this.Right.draw(parentLocation1);
      }
    }

    public class CSDVertExtendingPanel : CustomSelfDrawPanel.CSDControl
    {
      protected CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate areaMouseDownDelegate;
      protected CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate areaMouseUpDelegate;
      public CustomSelfDrawPanel.CSDImage Top = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage Mid = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage Bottom = new CustomSelfDrawPanel.CSDImage();
      private bool created;
      private bool inResize;
      private bool held;
      private Point heldPosition = new Point(0, 0);
      private int yDiff;

      public CSDVertExtendingPanel()
      {
        this.setMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonDown), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonUp));
      }

      public void setAreaMouseDownDelegate(
        CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate downDelegate,
        CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate upDelegate)
      {
        this.areaMouseDownDelegate = downDelegate;
        this.areaMouseUpDelegate = upDelegate;
      }

      public void Create(Image topImage, Image midImage, Image bottomImage)
      {
        if (topImage == null || midImage == null || bottomImage == null)
          return;
        this.Top.Image = topImage;
        this.Top.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.Top);
        this.Bottom.Image = bottomImage;
        this.Bottom.Position = new Point(0, this.Height - this.Bottom.Image.Height);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.Bottom);
        this.Mid.Image = midImage;
        this.Mid.Position = new Point(0, this.Top.Image.Height);
        this.Mid.Size = new Size(this.Mid.Image.Width, this.Height - this.Top.Image.Height - this.Bottom.Image.Height);
        this.Mid.Tile = true;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.Mid);
        this.Size = new Size(this.Top.Image.Width, this.Size.Height);
        this.created = true;
      }

      public override Size Size
      {
        get => base.Size;
        set
        {
          base.Size = value;
          this.resize();
        }
      }

      public void resize()
      {
        if (!this.created || this.inResize)
          return;
        this.inResize = true;
        this.Bottom.Position = new Point(0, this.Height - this.Bottom.Image.Height);
        this.Mid.Size = new Size(this.Mid.Image.Width, this.Height - this.Top.Image.Height - this.Bottom.Image.Height);
        this.Size = new Size(this.Top.Image.Width, this.Size.Height);
        this.inResize = false;
      }

      public int getMinSize() => this.Top.Image.Height + this.Bottom.Image.Height + 1;

      public int YDiff => this.yDiff;

      private void buttonDown()
      {
        if (!this.held)
        {
          this.heldPosition = this.csd.LastMousePosition;
          this.held = true;
          this.yDiff = 0;
        }
        else
          this.yDiff = this.csd.LastMousePosition.Y - this.heldPosition.Y;
        if (this.areaMouseDownDelegate == null)
          return;
        this.areaMouseDownDelegate();
      }

      private void buttonUp()
      {
        this.held = false;
        this.yDiff = 0;
        if (this.areaMouseUpDelegate == null)
          return;
        this.areaMouseUpDelegate();
      }

      public override void draw(Point parentLocation)
      {
        if (!this.held)
          return;
        if (!this.csd.MouseReallyPressed)
        {
          this.mouseDownFlag = false;
          this.buttonUp();
        }
        else
        {
          this.yDiff = this.csd.LastMousePosition.Y - this.heldPosition.Y;
          if (this.areaMouseDownDelegate == null)
            return;
          this.areaMouseDownDelegate();
        }
      }
    }

    public class CSDVertScrollBar : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDVertExtendingPanel background = new CustomSelfDrawPanel.CSDVertExtendingPanel();
      private CustomSelfDrawPanel.CSDVertExtendingPanel tab = new CustomSelfDrawPanel.CSDVertExtendingPanel();
      private Point offsetTL = new Point();
      private Point offsetBR = new Point();
      private int currentValue;
      private int maxValue;
      private int visibleLines = 1;
      private int tabMinSize;
      private int stepSize;
      private bool created;
      protected CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate valueChangedDelegate;
      protected CustomSelfDrawPanel.CSDControl.CSD_ScrollBarChangedDelegate scrollChangedDelegate;
      private int minTabPosition;
      private int maxTabPosition;
      private bool held;
      private int baseYPos;
      private bool clickedOnBar;

      public Point OffsetTL
      {
        get => this.offsetTL;
        set => this.offsetTL = value;
      }

      public Point OffsetBR
      {
        get => this.offsetBR;
        set => this.offsetBR = value;
      }

      public int Value
      {
        get => this.currentValue;
        set
        {
          this.currentValue = value;
          this.recalc();
        }
      }

      public int Max
      {
        get => this.maxValue;
        set
        {
          this.maxValue = value;
          this.recalc();
        }
      }

      public int NumVisibleLines
      {
        get => this.visibleLines;
        set
        {
          this.visibleLines = value;
          this.recalc();
        }
      }

      public int TabMinSize
      {
        get => this.tabMinSize;
        set
        {
          this.tabMinSize = value;
          this.recalc();
        }
      }

      public int StepSize
      {
        get => this.stepSize;
        set => this.stepSize = value;
      }

      public int TabSize => this.tab.Size.Height;

      public Point TabPosition => this.tab.Position;

      public void Create(
        Image topImage,
        Image midImage,
        Image bottomImage,
        Image tabTopImage,
        Image tabMidImage,
        Image tabBottomImage)
      {
        this.background.Size = this.Size;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
        this.background.Create(topImage, midImage, bottomImage);
        this.tab.Size = new Size(this.background.Size.Width - this.offsetTL.X - this.offsetBR.X, this.background.Size.Height - this.offsetTL.Y - this.offsetBR.Y);
        this.tab.Position = new Point(0, this.offsetTL.Y);
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.tab);
        this.tab.Create(tabTopImage, tabMidImage, tabBottomImage);
        this.created = true;
        this.recalc();
        this.tab.setAreaMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonDown), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonUp));
        this.background.setAreaMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.backgroundButtonDown), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.backgroundButtonUp));
      }

      public void setValueChangeDelegate(
        CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate newDelegate)
      {
        this.valueChangedDelegate = newDelegate;
      }

      public void setScrollChangeDelegate(
        CustomSelfDrawPanel.CSDControl.CSD_ScrollBarChangedDelegate newDelegate)
      {
        this.scrollChangedDelegate = newDelegate;
      }

      public void recalc()
      {
        if (!this.created)
          return;
        int height1 = this.background.Size.Height - this.offsetTL.Y - this.offsetBR.Y;
        if (this.maxValue > 0)
        {
          int num1 = this.tab.getMinSize() + this.tabMinSize;
          int height2 = height1 * this.NumVisibleLines / Math.Max(this.maxValue + this.NumVisibleLines, 1);
          if (height2 < num1)
            height2 = num1;
          int num2 = height1 - height2;
          int num3 = num2 * this.Value / Math.Max(this.maxValue, 1);
          this.tab.Size = new Size(this.tab.Size.Width, height2);
          this.tab.Position = new Point(0, this.offsetTL.Y + num3);
          this.maxTabPosition = this.minTabPosition + num2;
        }
        else
        {
          this.tab.Size = new Size(this.tab.Size.Width, height1);
          this.tab.Position = new Point(0, this.offsetTL.Y);
          this.maxTabPosition = this.minTabPosition;
        }
        this.minTabPosition = this.offsetTL.Y;
      }

      private void buttonDown()
      {
        if (!this.held)
        {
          this.held = true;
          this.baseYPos = this.tab.Position.Y;
          this.tab.invalidate();
        }
        else
          this.moveTabPosition(this.baseYPos, this.tab.YDiff);
      }

      private void buttonUp() => this.held = false;

      private void backgroundButtonDown() => this.clickedOnBar = true;

      private void backgroundButtonUp()
      {
        if (this.clickedOnBar)
        {
          if (this.StepSize != 0)
          {
            if (this.background.LastRelativeMousePos.Y < this.tab.Y + this.tab.Height / 2)
            {
              int num1 = this.Value;
              int num2 = this.Value - this.stepSize;
              if (num2 < 0)
                num2 = 0;
              if (num2 != num1)
              {
                this.Value = num2;
                if (this.valueChangedDelegate != null)
                  this.valueChangedDelegate();
                if (this.scrollChangedDelegate != null)
                  this.scrollChangedDelegate();
              }
            }
            else
            {
              this.Value += this.stepSize;
              this.moveTabPosition(this.tab.Y, 0);
              if (this.valueChangedDelegate != null)
                this.valueChangedDelegate();
              if (this.scrollChangedDelegate != null)
                this.scrollChangedDelegate();
            }
          }
          else if (this.background.LastRelativeMousePos.Y < this.tab.Y + this.tab.Height / 2)
            this.moveTabPosition(this.tab.Y, -this.tab.Height);
          else
            this.moveTabPosition(this.tab.Y, this.tab.Height);
        }
        this.clickedOnBar = false;
      }

      private void moveTabPosition(int baseYPos, int diff)
      {
        int y = baseYPos + diff;
        if (y < this.minTabPosition)
          y = this.minTabPosition;
        if (y >= this.maxTabPosition)
          y = this.maxTabPosition;
        this.tab.Position = new Point(0, y);
        int currentValue = this.currentValue;
        this.currentValue = this.maxTabPosition - this.minTabPosition <= 0 ? 0 : this.maxValue * (y - this.minTabPosition) / Math.Max(this.maxTabPosition - this.minTabPosition, 1);
        if (currentValue != this.currentValue && this.valueChangedDelegate != null)
          this.valueChangedDelegate();
        if (this.scrollChangedDelegate != null)
          this.scrollChangedDelegate();
        this.background.invalidate();
        this.tab.invalidate();
      }

      public void scrollUp() => this.moveTabPosition(this.tab.Y, -this.tab.Height);

      public void scrollUp(int amount) => this.moveTabPosition(this.tab.Y, -amount);

      public void scrollDown() => this.moveTabPosition(this.tab.Y, this.tab.Height);

      public void scrollDown(int amount) => this.moveTabPosition(this.tab.Y, amount);
    }

    public class CSDTrackBar : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage background = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage leftTab = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage tab = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage rightTab = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
      private int currentValue;
      private int maxValue;
      private int stepValue = 1;
      private Rectangle margin = new Rectangle();
      private bool created;
      private Image m_tabImage;
      private Image m_tabDownImage;
      private Image m_tabOverImage;
      protected CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate valueChangedDelegate;
      private int minTabPosition;
      private int maxTabPosition;
      public bool held;
      private int baseXPos;
      private int baseValue;
      private int stepSize = 1;
      private bool m_mouseOverFlag;
      private bool clickedOnBar;

      public int Value
      {
        get => this.currentValue;
        set
        {
          this.currentValue = value;
          this.recalc();
        }
      }

      public int Max
      {
        get => this.maxValue;
        set
        {
          this.maxValue = value;
          this.recalc();
          this.calcStepSize();
        }
      }

      public int StepValue
      {
        get => this.stepValue;
        set
        {
          this.stepValue = value;
          if (this.stepValue >= 1)
            return;
          this.stepValue = 1;
        }
      }

      public Rectangle Margin
      {
        get => this.margin;
        set
        {
          this.margin = value;
          this.recalc();
        }
      }

      public void Create(
        Image backImage,
        Image tabImage,
        Image leftImage,
        Image rightImage,
        Image tabDownImage,
        Image tabOverImage)
      {
        this.Create(backImage, tabImage, leftImage, rightImage, tabDownImage, tabOverImage, true);
      }

      public void Create(
        Image backImage,
        Image tabImage,
        Image leftImage,
        Image rightImage,
        Image tabDownImage,
        Image tabOverImage,
        bool backClick)
      {
        this.m_tabImage = tabImage;
        this.m_tabDownImage = tabDownImage;
        this.m_tabOverImage = tabOverImage;
        Size size = this.Size;
        if (backImage != null)
        {
          this.background.Image = backImage;
          this.background.Size = backImage.Size;
          this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
          size = backImage.Size;
        }
        else
        {
          this.background.Size = size;
          this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
        }
        this.tab.Image = tabImage;
        this.minTabPosition = this.margin.Left - this.tab.Size.Width / 2;
        this.maxTabPosition = this.minTabPosition + (size.Width - this.margin.Left - this.margin.Width);
        this.tab.Position = new Point(this.minTabPosition, this.margin.Top);
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.tab);
        this.created = true;
        this.recalc();
        this.calcStepSize();
        this.Size = size;
        this.tab.setMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonDown), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonUp));
        this.tab.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.buttonOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.buttonLeave));
        if (backClick)
          this.background.setMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.backgroundButtonDown), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.backgroundButtonUp));
        this.mouseWheelOverlay.Position = new Point(0, 0);
        this.mouseWheelOverlay.Size = size;
        this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
        this.addControl(this.mouseWheelOverlay);
      }

      private void mouseWheelMoved(int delta)
      {
        this.tab.invalidate();
        int currentValue = this.currentValue;
        if (delta < 0)
        {
          this.currentValue -= this.stepValue;
          if (this.currentValue < 0)
            this.currentValue = 0;
          this.recalc();
        }
        else if (delta > 0)
        {
          this.currentValue += this.stepValue;
          if (this.currentValue > this.maxValue)
            this.currentValue = this.maxValue;
          this.recalc();
        }
        if (currentValue != this.currentValue && this.valueChangedDelegate != null)
          this.valueChangedDelegate();
        this.background.invalidate();
        this.tab.invalidate();
      }

      public void setValueChangeDelegate(
        CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate newDelegate)
      {
        this.valueChangedDelegate = newDelegate;
      }

      public void recalc()
      {
        if (!this.created)
          return;
        if (this.maxValue > 0)
          this.tab.Position = new Point(this.minTabPosition + (this.background.Size.Width - this.margin.Left - this.margin.Width) * this.Value / Math.Max(this.maxValue, 1), this.margin.Top);
        else
          this.tab.Position = new Point(this.minTabPosition, this.margin.Top);
      }

      private void calcStepSize()
      {
        int num = this.background.Size.Width - this.margin.Left - this.margin.Width;
        if (this.maxValue <= 1)
          this.stepSize = num;
        else
          this.stepSize = num / this.maxValue;
      }

      private void buttonDown()
      {
        if (!this.held)
        {
          this.baseXPos = this.csd.LastMousePosition.X;
          this.baseValue = this.Value;
          this.held = true;
          this.updateTabImages();
        }
        else
        {
          int diff = this.csd.LastMousePosition.X - this.baseXPos;
          if (diff == 0)
            return;
          this.moveTabPosition(this.baseXPos, diff);
        }
      }

      private void buttonUp()
      {
      }

      private void buttonOver()
      {
        this.m_mouseOverFlag = true;
        this.updateTabImages();
      }

      private void buttonLeave()
      {
        this.m_mouseOverFlag = false;
        this.updateTabImages();
      }

      private void updateTabImages()
      {
        this.tab.Image = !this.held ? (!this.m_mouseOverFlag ? this.m_tabImage : this.m_tabOverImage) : this.m_tabDownImage;
        this.tab.invalidate();
      }

      private void backgroundButtonDown() => this.clickedOnBar = true;

      private void backgroundButtonUp()
      {
        if (this.clickedOnBar)
        {
          int x = this.background.LastRelativeMousePos.X;
          int num = this.tab.Position.X + this.tab.Width / 2;
          int currentValue = this.currentValue;
          this.tab.invalidate();
          if (x < num)
          {
            this.currentValue -= this.stepValue;
            if (this.currentValue < 0)
              this.currentValue = 0;
            this.recalc();
          }
          else
          {
            this.currentValue += this.stepValue;
            if (this.currentValue > this.maxValue)
              this.currentValue = this.maxValue;
            this.recalc();
          }
          if (currentValue != this.currentValue && this.valueChangedDelegate != null)
            this.valueChangedDelegate();
          this.background.invalidate();
          this.tab.invalidate();
        }
        this.clickedOnBar = false;
      }

      private void moveTabPosition(int baseYPos, int diff)
      {
        int currentValue = this.currentValue;
        this.tab.invalidate();
        if (this.maxValue == 0)
        {
          this.currentValue = 0;
        }
        else
        {
          int num1 = this.background.Size.Width - this.margin.Left - this.margin.Width;
          int num2 = num1 * this.baseValue / Math.Max(this.maxValue, 1) + diff;
          if (num2 < 0)
            num2 = 0;
          if (num2 >= num1)
            num2 = num1;
          this.currentValue = this.maxValue * (num2 + this.stepSize / 2) / Math.Max(this.maxTabPosition - this.minTabPosition, 1);
        }
        this.recalc();
        if (currentValue != this.currentValue && this.valueChangedDelegate != null)
          this.valueChangedDelegate();
        this.invalidate();
        this.background.invalidate();
        this.tab.invalidate();
      }

      public void invalidateTab() => this.tab.invalidate();

      public override void draw(Point parentLocation)
      {
        if (!this.held)
          return;
        if (!this.csd.MouseReallyPressed)
        {
          this.tab.MouseDownFlag = false;
          this.held = false;
          this.updateTabImages();
        }
        else
          this.buttonDown();
      }
    }

    public class CSDHorzProgressBar : CustomSelfDrawPanel.CSDControl
    {
      public CustomSelfDrawPanel.CSDImage Left = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage Mid = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage Right = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage barLeft = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage barMid = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage barRight = new CustomSelfDrawPanel.CSDImage();
      private Point offset = new Point(0, 0);
      private bool created;

      public Point Offset
      {
        get => this.offset;
        set => this.offset = value;
      }

      public void Create(
        Image leftImage,
        Image midImage,
        Image rightImage,
        Image innerLeftImage,
        Image innerMidImage,
        Image innerRightImage)
      {
        if (leftImage != null && midImage != null && rightImage != null)
        {
          this.Left.Image = leftImage;
          this.Left.Position = new Point(0, 0);
          this.addControl((CustomSelfDrawPanel.CSDControl) this.Left);
          this.Right.Image = rightImage;
          this.Right.Position = new Point(this.Width - this.Right.Image.Width, 0);
          this.addControl((CustomSelfDrawPanel.CSDControl) this.Right);
          this.Mid.Image = midImage;
          this.Mid.Position = new Point(this.Left.Image.Width, 0);
          this.Mid.Size = new Size(this.Width - this.Left.Image.Width - this.Right.Image.Width, this.Mid.Image.Height);
          this.Mid.Tile = true;
          this.addControl((CustomSelfDrawPanel.CSDControl) this.Mid);
          this.Size = new Size(this.Size.Width, this.Left.Image.Height);
        }
        else
          this.Size = new Size(this.Size.Width, innerLeftImage.Height);
        this.barLeft.Image = innerLeftImage;
        this.barLeft.Position = this.offset;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.barLeft);
        this.barMid.Image = innerMidImage;
        this.barMid.Tile = true;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.barMid);
        this.barRight.Image = innerRightImage;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.barRight);
        this.created = true;
        this.setValues(1.0, 1.0);
      }

      public void setValues(double curValue, double maxValue)
      {
        if (!this.created)
          return;
        if (maxValue <= 0.0)
        {
          this.barLeft.Visible = false;
          this.barRight.Visible = false;
          this.barMid.Visible = false;
        }
        else
        {
          this.barLeft.Visible = true;
          this.barRight.Visible = true;
          this.barMid.Visible = true;
          if (curValue > maxValue)
            curValue = maxValue;
          double width = Math.Round((double) (this.Width - this.offset.X * 2 - this.barLeft.Image.Width - this.barRight.Image.Width) * curValue / maxValue);
          this.barRight.Position = new Point(this.offset.X + (int) width + this.barLeft.Width, this.offset.Y);
          this.barMid.Position = new Point(this.offset.X + this.barLeft.Width, this.offset.Y);
          this.barMid.Size = new Size((int) width, this.barLeft.Image.Height);
          this.barMid.ClipRect = new Rectangle(new Point(0, 0), this.barMid.Size);
        }
      }
    }

    public class CSDDragPanel : CustomSelfDrawPanel.CSDControl
    {
      protected CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate valueChangedDelegate;
      private float xMomentum;
      private float yMomentum;
      private float dampen = 0.97f;
      private float maxMomentum = 100f;
      private bool held;
      private Point heldPosition = new Point(0, 0);
      private int yDiff;
      private int xDiff;

      public CSDDragPanel()
      {
        this.setMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonDown), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonUp));
      }

      public void setValueChangeDelegate(
        CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate newDelegate)
      {
        this.valueChangedDelegate = newDelegate;
      }

      public int YDiff => this.yDiff;

      public int XDiff => this.xDiff;

      private void buttonDown()
      {
        if (!this.held)
        {
          this.heldPosition = this.csd.LastMousePosition;
          this.held = true;
          this.yDiff = 0;
        }
        else
        {
          this.xDiff = this.csd.LastMousePosition.X - this.heldPosition.X;
          this.yDiff = this.csd.LastMousePosition.Y - this.heldPosition.Y;
        }
        if (this.valueChangedDelegate != null && (this.xDiff != 0 || this.yDiff != 0))
          this.valueChangedDelegate();
        this.heldPosition = new Point(this.csd.LastMousePosition.X, this.csd.LastMousePosition.Y);
        this.csd.addTrapMouseEvent((CustomSelfDrawPanel.CSDControl) this);
      }

      private void buttonUp()
      {
        if (this.csd.MouseReallyPressed)
          return;
        this.held = false;
        this.yDiff = 0;
        this.xDiff = 0;
        if (this.valueChangedDelegate != null)
          this.valueChangedDelegate();
        this.csd.removeTrapMouseEvent((CustomSelfDrawPanel.CSDControl) this);
      }

      public override void mouseEventTrapped()
      {
        if (!this.held)
          return;
        if (!this.csd.MouseReallyPressed)
        {
          this.mouseDownFlag = false;
          this.buttonUp();
        }
        else
        {
          this.xDiff = this.csd.LastMousePosition.X - this.heldPosition.X;
          this.yDiff = this.csd.LastMousePosition.Y - this.heldPosition.Y;
          if (this.valueChangedDelegate != null && (Math.Abs(this.xDiff) > 5 || Math.Abs(this.yDiff) > 5))
          {
            InterfaceMgr.mouseDownOnDraggable = true;
            this.valueChangedDelegate();
          }
          this.heldPosition = new Point(this.csd.LastMousePosition.X, this.csd.LastMousePosition.Y);
        }
      }
    }

    public class CSDFill : CustomSelfDrawPanel.CSDControl
    {
      private float alpha = 1f;
      private Color fillColor;
      private bool border;
      private bool specialGradient;

      public float Alpha
      {
        get => this.alpha;
        set
        {
          if ((double) value > 1.0)
            this.alpha = 1f;
          else
            this.alpha = value;
        }
      }

      public Color FillColor
      {
        get => this.fillColor;
        set
        {
          if (this.fillColor != value)
            this.invalidate();
          this.fillColor = value;
        }
      }

      public bool Border
      {
        get => this.border;
        set
        {
          if (this.border != value)
            this.invalidate();
          this.border = value;
        }
      }

      public bool SpecialGradient
      {
        get => this.specialGradient;
        set
        {
          if (this.specialGradient != value)
            this.invalidate();
          this.specialGradient = value;
        }
      }

      public override void draw(Point parentLocation)
      {
        Rectangle rectangle = this.Rectangle;
        rectangle.X += parentLocation.X;
        rectangle.Y += parentLocation.Y;
        if (this.specialGradient)
        {
          this.csd.drawSpecialGradient(rectangle);
        }
        else
        {
          this.csd.fillRect(rectangle, this.fillColor);
          if (!this.border)
            return;
          Color black = ARGBColors.Black;
          this.csd.drawLine(black, new Point(rectangle.X, rectangle.Y), new Point(rectangle.X + this.Size.Width - 1, rectangle.Y));
          this.csd.drawLine(black, new Point(rectangle.X, rectangle.Y), new Point(rectangle.X, rectangle.Y + this.Size.Height - 2));
          this.csd.drawLine(black, new Point(rectangle.X + this.Size.Width - 1, rectangle.Y + this.Size.Height - 1), new Point(rectangle.X + this.Size.Width - 1, rectangle.Y));
          this.csd.drawLine(black, new Point(rectangle.X + this.Size.Width - 1, rectangle.Y + this.Size.Height - 1), new Point(rectangle.X, rectangle.Y + this.Size.Height - 1));
        }
      }
    }

    public class CSDLine : CustomSelfDrawPanel.CSDControl
    {
      private Color lineColor = ARGBColors.White;

      public Color LineColor
      {
        get => this.lineColor;
        set
        {
          if (this.lineColor != value)
            this.invalidate();
          this.lineColor = value;
        }
      }

      public override void draw(Point parentLocation)
      {
        Rectangle rectangle = this.Rectangle;
        rectangle.X += parentLocation.X;
        rectangle.Y += parentLocation.Y;
        this.csd.drawLine(this.lineColor, new Point(rectangle.X, rectangle.Y), new Point(rectangle.X + this.Size.Width, rectangle.Y + this.Size.Height));
      }
    }

    public class CSDRectangle : CustomSelfDrawPanel.CSDControl
    {
      private Color lineColor = ARGBColors.White;

      public Color LineColor
      {
        get => this.lineColor;
        set
        {
          if (this.lineColor != value)
            this.invalidate();
          this.lineColor = value;
        }
      }

      public override void draw(Point parentLocation)
      {
        Rectangle rectangle = this.Rectangle;
        rectangle.X += parentLocation.X;
        rectangle.Y += parentLocation.Y;
        this.csd.drawLine(this.lineColor, new Point(rectangle.X, rectangle.Y), new Point(rectangle.X + this.Size.Width - 1, rectangle.Y));
        this.csd.drawLine(this.lineColor, new Point(rectangle.X, rectangle.Y), new Point(rectangle.X, rectangle.Y + this.Size.Height - 2));
        this.csd.drawLine(this.lineColor, new Point(rectangle.X + this.Size.Width - 1, rectangle.Y + this.Size.Height - 1), new Point(rectangle.X + this.Size.Width - 1, rectangle.Y));
        this.csd.drawLine(this.lineColor, new Point(rectangle.X + this.Size.Width - 1, rectangle.Y + this.Size.Height - 1), new Point(rectangle.X, rectangle.Y + this.Size.Height - 1));
      }
    }

    private class InvalidRectpair
    {
      public Rectangle rect;
      public CustomSelfDrawPanel panel;
    }

    public class WikiLinkControl : CustomSelfDrawPanel.CSDButton
    {
      public const int WORLD_MAP = 0;
      public const int VILLAGE_VILLAGE_MAP = 1;
      public const int VILLAGE_CASTLE_MAP = 2;
      public const int VILLAGE_RESOURCES = 3;
      public const int VILLAGE_TRADE = 4;
      public const int VILLAGE_TROOPS = 5;
      public const int VILLAGE_UNITS = 6;
      public const int VILLAGE_HOLD_A_BANQUET = 7;
      public const int VILLAGE_VASSALS = 8;
      public const int PARISH_CAPITAL_VILLAGE_MAP = 9;
      public const int PARISH_CAPITAL_CASTLE_MAP = 10;
      public const int PARISH_CAPITAL_RESOURCES = 11;
      public const int PARISH_CAPITAL_TRADE = 12;
      public const int PARISH_CAPITAL_TROOPS = 13;
      public const int PARISH_CAPITAL_CAPITAL_INFO = 14;
      public const int PARISH_CAPITAL_VOTE = 15;
      public const int PARISH_CAPITAL_PARISH_FORUM = 16;
      public const int RESEARCH = 17;
      public const int RANK = 18;
      public const int QUESTS = 19;
      public const int ATTACKS = 20;
      public const int REPORTS = 21;
      public const int FACTIONS_HOUSES_GLORY = 22;
      public const int FACTIONS_HOUSES_FACTION = 23;
      public const int FACTIONS_HOUSES_HOUSE = 24;
      public const int CARDS = 25;
      public const int MAIL = 26;
      public const int CHAT = 27;
      public const int LEADERBOARD = 28;
      public const int MY_ACCOUNT = 29;
      public const int SETTINGS = 30;
      public const int COAT_OF_ARMS = 31;
      public const int QUEST_WHEEL = 32;
      public const int SEND_ATTACK = 33;
      public const int SEND_SCOUTS = 34;
      public const int SEND_MONKS = 35;
      public const int SEND_TRADER_MERCHANTS = 36;
      public const int BUY_PREMIUM_TOKENS = 37;
      public const int BUY_CROWNS = 38;
      public const int BUY_CARDS = 39;
      public const int SWAP_CARDS = 40;
      public const int LOGOUT = 41;
      public const int DONATE_TO_PARISH = 42;
      public const int VILLAGES_OVERVIEW = 43;
      public const int ACHIEVEMENTS = 44;
      public const int SECONDAGE = 45;
      public const int THIRDAGE = 46;
      public const int DOMINATION_RULES = 47;
      public const int FOURTHAGE = 48;
      public const int TREASURE_CASTLES = 49;
      public const int PALADIN_CASTLES = 50;
      public const int FIFTHAGE = 51;
      public const int SIXTHAGE = 52;
      public const int FINAL_AGE = 53;
      public const int ROYAL_TOWERS = 54;
      public const int ERAS = 55;
      public const int AI_RULES = 56;
      private int m_ID = -1;
      private static string[] urls = new string[627]
      {
        "http://strongholdkingdoms.gamepedia.com/World_Map",
        "http://strongholdkingdoms-de.gamepedia.com/Weltkarte",
        "http://strongholdkingdoms-fr.gamepedia.com/Carte_du_Monde",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9A%D0%B0%D1%80%D1%82%D0%B0_%D0%BC%D0%B8%D1%80%D0%B0",
        "http://strongholdkingdoms-es.gamepedia.com/Mapa_del_mundo",
        "http://strongholdkingdoms-pl.gamepedia.com/Mapa_%C5%9Bwiata",
        "http://strongholdkingdoms-tr.gamepedia.com/D%C3%BCnya_Haritas%C4%B1",
        "http://strongholdkingdoms-it.gamepedia.com/Mappa_del_mondo",
        "http://strongholdkingdoms-pt.gamepedia.com/Mapa_do_mundo",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Villages",
        "http://strongholdkingdoms-de.gamepedia.com/D%C3%B6rfer",
        "http://strongholdkingdoms-fr.gamepedia.com/Village",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A7%D0%B0%D0%92%D0%BE:%D0%92%D0%B0%D1%88%D0%B0_%D0%B4%D0%B5%D1%80%D0%B5%D0%B2%D0%BD%D1%8F",
        "http://strongholdkingdoms-es.gamepedia.com/Aldeas",
        "http://strongholdkingdoms-pl.gamepedia.com/Wioski",
        "http://strongholdkingdoms-tr.gamepedia.com/K%C3%B6yler",
        "http://strongholdkingdoms-it.gamepedia.com/Villaggi",
        "http://strongholdkingdoms-pt.gamepedia.com/Aldeia",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Castles",
        "http://strongholdkingdoms-de.gamepedia.com/Burgen",
        "http://strongholdkingdoms-fr.gamepedia.com/Ch%C3%A2teau",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%97%D0%B0%D0%BC%D0%BA%D0%B8",
        "http://strongholdkingdoms-es.gamepedia.com/Castillos",
        "http://strongholdkingdoms-pl.gamepedia.com/Zamki",
        "http://strongholdkingdoms-tr.gamepedia.com/Kaleler",
        "http://strongholdkingdoms-it.gamepedia.com/Castelli",
        "http://strongholdkingdoms-pt.gamepedia.com/Castelos",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Resources",
        "http://strongholdkingdoms-de.gamepedia.com/Ressourcen",
        "http://strongholdkingdoms-fr.gamepedia.com/Ressources",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A0%D0%B5%D1%81%D1%83%D1%80%D1%81%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Recursos",
        "http://strongholdkingdoms-pl.gamepedia.com/Surowce",
        "http://strongholdkingdoms-tr.gamepedia.com/Kaynaklar",
        "http://strongholdkingdoms-it.gamepedia.com/Risorse",
        "http://strongholdkingdoms-pt.gamepedia.com/Recursos",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Trading",
        "http://strongholdkingdoms-de.gamepedia.com/Handel",
        "http://strongholdkingdoms-fr.gamepedia.com/Commerce",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A7%D0%B0%D0%92%D0%BE:%D0%9D%D0%B0%D0%BB%D0%BE%D0%B3%D0%B8_%D0%B8_%D1%82%D0%BE%D1%80%D0%B3%D0%BE%D0%B2%D0%BB%D1%8F",
        "http://strongholdkingdoms-es.gamepedia.com/Comerciar",
        "http://strongholdkingdoms-pl.gamepedia.com/Handel",
        "http://strongholdkingdoms-tr.gamepedia.com/Ticaret",
        "http://strongholdkingdoms-it.gamepedia.com/Commercio",
        "http://strongholdkingdoms-pt.gamepedia.com/Comércio",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Barracks",
        "http://strongholdkingdoms-de.gamepedia.com/Kaserne",
        "http://strongholdkingdoms-fr.gamepedia.com/Garnison",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%91%D0%B0%D1%80%D0%B0%D0%BA%D0%B8",
        "http://strongholdkingdoms-es.gamepedia.com/Barracas",
        "http://strongholdkingdoms-pl.gamepedia.com/Koszary",
        "http://strongholdkingdoms-tr.gamepedia.com/K%C4%B1%C5%9Flalar",
        "http://strongholdkingdoms-it.gamepedia.com/Guarnigione",
        "http://strongholdkingdoms-pt.gamepedia.com/Quartel",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Units",
        "http://strongholdkingdoms-de.gamepedia.com/Einheiten",
        "http://strongholdkingdoms-fr.gamepedia.com/Sp%C3%A9cialistes",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%AE%D0%BD%D0%B8%D1%82%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Unidades",
        "http://strongholdkingdoms-pl.gamepedia.com/Jednostki",
        "http://strongholdkingdoms-tr.gamepedia.com/Birimler",
        "http://strongholdkingdoms-it.gamepedia.com/Unit%C3%A0",
        "http://strongholdkingdoms-pt.gamepedia.com/Unidades",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Banquets",
        "http://strongholdkingdoms-de.gamepedia.com/Bankette",
        "http://strongholdkingdoms-fr.gamepedia.com/Banquets",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9F%D0%BE%D1%81%D1%82%D1%80%D0%BE%D0%B9%D0%BA%D0%B8:%D0%91%D0%B0%D0%BD%D0%BA%D0%B5%D1%82%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Banquetes",
        "http://strongholdkingdoms-pl.gamepedia.com/Uczty",
        "http://strongholdkingdoms-tr.gamepedia.com/Ziyafetler",
        "http://strongholdkingdoms-it.gamepedia.com/Banchetti",
        "http://strongholdkingdoms-pt.gamepedia.com/Banquete",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Vassals_%26_Liege_Lords",
        "http://strongholdkingdoms-de.gamepedia.com/Vassalle_%26_Lehnsherren",
        "http://strongholdkingdoms-fr.gamepedia.com/Vassaux",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%92%D0%B0%D1%81%D1%81%D0%B0%D0%BB%D1%8B_%D0%B8_%D1%81%D0%B5%D0%BD%D1%8C%D0%BE%D1%80%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Vasallos_y_se%C3%B1ores_feudales",
        "http://strongholdkingdoms-pl.gamepedia.com/Wasale_i_seniorzy",
        "http://strongholdkingdoms-tr.gamepedia.com/Vasallar_ve_S%C3%BCzerenler",
        "http://strongholdkingdoms-it.gamepedia.com/Vassalli_e_feudatari",
        "http://strongholdkingdoms-pt.gamepedia.com/Vassalos_%26_Senhores_feudais",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Parishes_%26_Capitals#Capital_Town",
        "http://strongholdkingdoms-de.gamepedia.com/Gemeinden_%26_Hauptst%C3%A4dte",
        "http://strongholdkingdoms-fr.gamepedia.com/Pr%C3%A9v%C3%B4t%C3%A9s_et_Capitales#Bourg_de_capitale",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9E%D0%BA%D1%80%D1%83%D0%B3%D0%B0_%D0%B8_%D1%81%D1%82%D0%BE%D0%BB%D0%B8%D1%86%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Parroquias_y_capitales",
        "http://strongholdkingdoms-pl.gamepedia.com/Flagami",
        "http://strongholdkingdoms-tr.gamepedia.com/Pari%C5%9Fler_ve_Ba%C5%9Fkentler",
        "http://strongholdkingdoms-it.gamepedia.com/Distretti_e_capitali",
        "http://strongholdkingdoms-pt.gamepedia.com/Par%C3%B3quias_e_Capitais#Capital",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Parishes_%26_Capitals#Capital_Castle",
        "http://strongholdkingdoms-de.gamepedia.com/Gemeinden_%26_Hauptst%C3%A4dte",
        "http://strongholdkingdoms-fr.gamepedia.com/Pr%C3%A9v%C3%B4t%C3%A9s_et_Capitales#Ch.C3.A2teau_d.27une_capitale",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9E%D0%BA%D1%80%D1%83%D0%B3%D0%B0_%D0%B8_%D1%81%D1%82%D0%BE%D0%BB%D0%B8%D1%86%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Parroquias_y_capitales",
        "http://strongholdkingdoms-pl.gamepedia.com/Flagami",
        "http://strongholdkingdoms-tr.gamepedia.com/Pari%C5%9Fler_ve_Ba%C5%9Fkentler",
        "http://strongholdkingdoms-it.gamepedia.com/Distretti_e_capitali#Castello_della_capitale",
        "http://strongholdkingdoms-pt.gamepedia.com/Par%C3%B3quias_e_Capitais#Castelo_da_capital",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Resources",
        "http://strongholdkingdoms-de.gamepedia.com/Ressourcen",
        "http://strongholdkingdoms-fr.gamepedia.com/Ressources",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A0%D0%B5%D1%81%D1%83%D1%80%D1%81%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Recursos",
        "http://strongholdkingdoms-pl.gamepedia.com/Surowce",
        "http://strongholdkingdoms-tr.gamepedia.com/Kaynaklar",
        "http://strongholdkingdoms-it.gamepedia.com/Risorse",
        "http://strongholdkingdoms-pt.gamepedia.com/Recursos",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Parishes_%26_Capitals",
        "http://strongholdkingdoms-de.gamepedia.com/Gemeinden_%26_Hauptst%C3%A4dte",
        "http://strongholdkingdoms-fr.gamepedia.com/Pr%C3%A9v%C3%B4t%C3%A9s_et_Capitales",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9E%D0%BA%D1%80%D1%83%D0%B3%D0%B0_%D0%B8_%D1%81%D1%82%D0%BE%D0%BB%D0%B8%D1%86%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Parroquias_y_capitales",
        "http://strongholdkingdoms-pl.gamepedia.com/Flagami",
        "http://strongholdkingdoms-tr.gamepedia.com/Pari%C5%9Fler_ve_Ba%C5%9Fkentler",
        "http://strongholdkingdoms-it.gamepedia.com/Capitale",
        "http://strongholdkingdoms-pt.gamepedia.com/Par%C3%B3quias_e_Capitais",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Barracks",
        "http://strongholdkingdoms-de.gamepedia.com/Kaserne",
        "http://strongholdkingdoms-fr.gamepedia.com/Garnison",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%91%D0%B0%D1%80%D0%B0%D0%BA%D0%B8",
        "http://strongholdkingdoms-es.gamepedia.com/Barracas",
        "http://strongholdkingdoms-pl.gamepedia.com/Koszary",
        "http://strongholdkingdoms-tr.gamepedia.com/Pari%C5%9Fler_ve_Ba%C5%9Fkentler",
        "http://strongholdkingdoms-it.gamepedia.com/Guarnigione",
        "http://strongholdkingdoms-pt.gamepedia.com/Quartel",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Parishes_%26_Capitals",
        "http://strongholdkingdoms-de.gamepedia.com/Gemeinden_%26_Hauptst%C3%A4dte",
        "http://strongholdkingdoms-fr.gamepedia.com/Pr%C3%A9v%C3%B4t%C3%A9s_et_Capitales",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9E%D0%BA%D1%80%D1%83%D0%B3%D0%B0_%D0%B8_%D1%81%D1%82%D0%BE%D0%BB%D0%B8%D1%86%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Parroquias_y_capitales",
        "http://strongholdkingdoms-pl.gamepedia.com/Flagami",
        "http://strongholdkingdoms-tr.gamepedia.com/Pari%C5%9Fler_ve_Ba%C5%9Fkentler",
        "http://strongholdkingdoms-it.gamepedia.com/Capitale",
        "http://strongholdkingdoms-pt.gamepedia.com/Par%C3%B3quias_e_Capitais",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Parishes_%26_Capitals#Voting",
        "http://strongholdkingdoms-de.gamepedia.com/Gemeinden_%26_Hauptst%C3%A4dte#Abstimmen",
        "http://strongholdkingdoms-fr.gamepedia.com/Pr%C3%A9v%C3%B4t%C3%A9s_et_Capitales#.C3.89lection",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9E%D0%BA%D1%80%D1%83%D0%B3%D0%B0_%D0%B8_%D1%81%D1%82%D0%BE%D0%BB%D0%B8%D1%86%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Parroquias_y_capitales",
        "http://strongholdkingdoms-pl.gamepedia.com/Flagami",
        "http://strongholdkingdoms-tr.gamepedia.com/Pari%C5%9Fler_ve_Ba%C5%9Fkentler",
        "http://strongholdkingdoms-it.gamepedia.com/Capitale#Votare",
        "http://strongholdkingdoms-pt.gamepedia.com/Par%C3%B3quias_e_Capitais",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Communication",
        "http://strongholdkingdoms-de.gamepedia.com/Kommunikation",
        "http://strongholdkingdoms-fr.gamepedia.com/Communication",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9E%D0%B1%D1%89%D0%B5%D0%BD%D0%B8%D0%B5",
        "http://strongholdkingdoms-es.gamepedia.com/Medios_para_comunicarse",
        "http://strongholdkingdoms-pl.gamepedia.com/Czatu",
        "http://strongholdkingdoms-tr.gamepedia.com/%C4%B0leti%C5%9Fim",
        "http://strongholdkingdoms-it.gamepedia.com/Comunicazioni",
        "http://strongholdkingdoms-pt.gamepedia.com/Comunicação",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Research",
        "http://strongholdkingdoms-de.gamepedia.com/Forschung",
        "http://strongholdkingdoms-fr.gamepedia.com/Recherches",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%98%D1%81%D1%81%D0%BB%D0%B5%D0%B4%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F",
        "http://strongholdkingdoms-es.gamepedia.com/Investigaci%C3%B3n",
        "http://strongholdkingdoms-pl.gamepedia.com/Badania",
        "http://strongholdkingdoms-tr.gamepedia.com/Ara%C5%9Ft%C4%B1rma",
        "http://strongholdkingdoms-it.gamepedia.com/Ricerca",
        "http://strongholdkingdoms-pt.gamepedia.com/Pesquisar",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Ranks",
        "http://strongholdkingdoms-de.gamepedia.com/Rang",
        "http://strongholdkingdoms-fr.gamepedia.com/Rangs",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A0%D0%B0%D0%BD%D0%B3%D0%B8",
        "http://strongholdkingdoms-es.gamepedia.com/Niveles",
        "http://strongholdkingdoms-pl.gamepedia.com/Rang",
        "http://strongholdkingdoms-tr.gamepedia.com/Mertebeler",
        "http://strongholdkingdoms-it.gamepedia.com/Ranghi",
        "http://strongholdkingdoms-pt.gamepedia.com/Postos",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Quests",
        "http://strongholdkingdoms-de.gamepedia.com/Quest",
        "http://strongholdkingdoms-fr.gamepedia.com/Qu%C3%AAtes",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%97%D0%B0%D0%B4%D0%B0%D0%BD%D0%B8%D1%8F",
        "http://strongholdkingdoms-es.gamepedia.com/Misiones",
        "http://strongholdkingdoms-pl.gamepedia.com/Misji",
        "http://strongholdkingdoms-tr.gamepedia.com/G%C3%B6revler",
        "http://strongholdkingdoms-it.gamepedia.com/Missioni",
        "http://strongholdkingdoms-pt.gamepedia.com/Missões",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Combat",
        "http://strongholdkingdoms-de.gamepedia.com/Kampf",
        "http://strongholdkingdoms-fr.gamepedia.com/Combat",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%91%D0%B8%D1%82%D0%B2%D0%B0",
        "http://strongholdkingdoms-es.gamepedia.com/Combate",
        "http://strongholdkingdoms-pl.gamepedia.com/Walka",
        "http://strongholdkingdoms-tr.gamepedia.com/Muharebe",
        "http://strongholdkingdoms-it.gamepedia.com/Combattimento",
        "http://strongholdkingdoms-pt.gamepedia.com/Combate",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Reports",
        "http://strongholdkingdoms-de.gamepedia.com/Berichte",
        "http://strongholdkingdoms-fr.gamepedia.com/Rapports",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9E%D1%82%D1%87%D1%91%D1%82%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Informes",
        "http://strongholdkingdoms-pl.gamepedia.com/Raporty",
        "http://strongholdkingdoms-tr.gamepedia.com/Raporlar",
        "http://strongholdkingdoms-it.gamepedia.com/Rapporti",
        "http://strongholdkingdoms-pt.gamepedia.com/Relatórios",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Glory",
        "http://strongholdkingdoms-de.gamepedia.com/Herrlichkeit",
        "http://strongholdkingdoms-fr.gamepedia.com/Gloire",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A1%D0%BB%D0%B0%D0%B2%D0%B0",
        "http://strongholdkingdoms-es.gamepedia.com/Gloria",
        "http://strongholdkingdoms-pl.gamepedia.com/Punkty_chwa%C5%82y",
        "http://strongholdkingdoms-tr.gamepedia.com/%C5%9Ean",
        "http://strongholdkingdoms-it.gamepedia.com/Gloria",
        "http://strongholdkingdoms-pt.gamepedia.com/Glória",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Factions_%26_Houses",
        "http://strongholdkingdoms-de.gamepedia.com/H%C3%A4user",
        "http://strongholdkingdoms-fr.gamepedia.com/Maisons",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A4%D1%80%D0%B0%D0%BA%D1%86%D0%B8%D0%B8_%D0%B8_%D0%94%D0%BE%D0%BC%D0%B0",
        "http://strongholdkingdoms-es.gamepedia.com/Facciones_y_Casas",
        "http://strongholdkingdoms-pl.gamepedia.com/Dom",
        "http://strongholdkingdoms-tr.gamepedia.com/%C4%B0htilaflar_ve_Haneler",
        "http://strongholdkingdoms-it.gamepedia.com/Fazioni_e_casati",
        "http://strongholdkingdoms-pt.gamepedia.com/Facções_e_Casas",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Factions_%26_Houses",
        "http://strongholdkingdoms-de.gamepedia.com/H%C3%A4user",
        "http://strongholdkingdoms-fr.gamepedia.com/Maisons",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A4%D1%80%D0%B0%D0%BA%D1%86%D0%B8%D0%B8_%D0%B8_%D0%94%D0%BE%D0%BC%D0%B0",
        "http://strongholdkingdoms-es.gamepedia.com/Facciones_y_Casas",
        "http://strongholdkingdoms-pl.gamepedia.com/Dom",
        "http://strongholdkingdoms-tr.gamepedia.com/%C4%B0htilaflar_ve_Haneler",
        "http://strongholdkingdoms-it.gamepedia.com/Fazioni_e_casati",
        "http://strongholdkingdoms-pt.gamepedia.com/Facções_e_Casas",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Strategy_Cards",
        "http://strongholdkingdoms-de.gamepedia.com/Strategiekarten",
        "http://strongholdkingdoms-fr.gamepedia.com/Cartes_Strat%C3%A9giques",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A1%D1%82%D1%80%D0%B0%D1%82%D0%B5%D0%B3%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B5_%D0%BA%D0%B0%D1%80%D1%82%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Cartas_de_estrategia",
        "http://strongholdkingdoms-pl.gamepedia.com/Karty_strategiczne",
        "http://strongholdkingdoms-tr.gamepedia.com/%C3%9Ccretsiz_Strateji_Kartlar%C4%B1",
        "http://strongholdkingdoms-it.gamepedia.com/Carte_strategiche",
        "http://strongholdkingdoms-pt.gamepedia.com/Cartas_de_estratégia",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Communication",
        "http://strongholdkingdoms-de.gamepedia.com/Kommunikation",
        "http://strongholdkingdoms-fr.gamepedia.com/Communication",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9E%D0%B1%D1%89%D0%B5%D0%BD%D0%B8%D0%B5",
        "http://strongholdkingdoms-es.gamepedia.com/Medios_para_comunicarse",
        "http://strongholdkingdoms-pl.gamepedia.com/Czatu",
        "http://strongholdkingdoms-tr.gamepedia.com/%C4%B0leti%C5%9Fim",
        "http://strongholdkingdoms-it.gamepedia.com/Comunicazioni",
        "http://strongholdkingdoms-pt.gamepedia.com/Comunicação",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Communication",
        "http://strongholdkingdoms-de.gamepedia.com/Kommunikation",
        "http://strongholdkingdoms-fr.gamepedia.com/Communication",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9E%D0%B1%D1%89%D0%B5%D0%BD%D0%B8%D0%B5",
        "http://strongholdkingdoms-es.gamepedia.com/Medios_para_comunicarse",
        "http://strongholdkingdoms-pl.gamepedia.com/Czatu",
        "http://strongholdkingdoms-tr.gamepedia.com/%C4%B0leti%C5%9Fim",
        "http://strongholdkingdoms-it.gamepedia.com/Comunicazioni",
        "http://strongholdkingdoms-pt.gamepedia.com/Comunicação",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Leaderboard",
        "http://strongholdkingdoms-de.gamepedia.com/Bestenliste",
        "http://strongholdkingdoms-fr.gamepedia.com/Classement",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A0%D0%B5%D0%B9%D1%82%D0%B8%D0%BD%D0%B3",
        "http://strongholdkingdoms-es.gamepedia.com/Tabla_de_clasificaci%C3%B3n",
        "http://strongholdkingdoms-pl.gamepedia.com/Tablica_wynik%C3%B3w",
        "http://strongholdkingdoms-tr.gamepedia.com/Liderlik_Tablosu",
        "http://strongholdkingdoms-it.gamepedia.com/Classifica",
        "http://strongholdkingdoms-pt.gamepedia.com/Placar_de_Líderes",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Account_Details",
        "http://strongholdkingdoms-de.gamepedia.com/Konten_Einzelheiten",
        "http://strongholdkingdoms-fr.gamepedia.com/Profil_Joueur",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A3%D1%87%D0%B5%D1%82%D0%BD%D0%B0%D1%8F_%D0%B7%D0%B0%D0%BF%D0%B8%D1%81%D1%8C",
        "http://strongholdkingdoms-es.gamepedia.com/Detalles_de_la_cuenta",
        "http://strongholdkingdoms-pl.gamepedia.com/Szczeg%C3%B3%C5%82y_konta",
        "http://strongholdkingdoms-tr.gamepedia.com/Hesap_Bilgileri",
        "http://strongholdkingdoms-it.gamepedia.com/Dettagli_dell%E2%80%99account",
        "http://strongholdkingdoms-pt.gamepedia.com/Detalhes_da_conta",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Options_%26_Settings",
        "http://strongholdkingdoms-de.gamepedia.com/Optionen/Einstellungen",
        "http://strongholdkingdoms-fr.gamepedia.com/Options",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9D%D0%B0%D1%81%D1%82%D1%80%D0%BE%D0%B9%D0%BA%D0%B8",
        "http://strongholdkingdoms-es.gamepedia.com/Opciones_y_configuraci%C3%B3n",
        "http://strongholdkingdoms-pl.gamepedia.com/Opcje_i_ustawienia",
        "http://strongholdkingdoms-tr.gamepedia.com/Se%C3%A7enekler_ve_Ayarlar",
        "http://strongholdkingdoms-it.gamepedia.com/Opzioni_e_impostazioni",
        "http://strongholdkingdoms-pt.gamepedia.com/Opções_e_configurações",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Coat_of_Arms",
        "http://strongholdkingdoms-de.gamepedia.com/Wappen",
        "http://strongholdkingdoms-fr.gamepedia.com/Armoiries",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A1%D0%BE%D0%B7%D0%B4%D0%B0%D0%BD%D0%B8%D0%B5_%D0%B3%D0%B5%D1%80%D0%B1%D0%B0",
        "http://strongholdkingdoms-es.gamepedia.com/Escudo_de_armas",
        "http://strongholdkingdoms-pl.gamepedia.com/Herb",
        "http://strongholdkingdoms-tr.gamepedia.com/Armal%C4%B1_Kalkan",
        "http://strongholdkingdoms-it.gamepedia.com/Blasone",
        "http://strongholdkingdoms-pt.gamepedia.com/Brasão",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Wheel",
        "http://strongholdkingdoms-de.gamepedia.com/Quest_Gl%C3%BCcksrad",
        "http://strongholdkingdoms-fr.gamepedia.com/Roue_des_Qu%C3%AAtes",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9A%D0%BE%D0%BB%D0%B5%D1%81%D0%BE",
        "http://strongholdkingdoms-es.gamepedia.com/Ruleta",
        "http://strongholdkingdoms-pl.gamepedia.com/Ko%C5%82o",
        "http://strongholdkingdoms-tr.gamepedia.com/%C3%87ark",
        "http://strongholdkingdoms-it.gamepedia.com/Ruota",
        "http://strongholdkingdoms-pt.gamepedia.com/Roda",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Combat",
        "http://strongholdkingdoms-de.gamepedia.com/Kampf",
        "http://strongholdkingdoms-fr.gamepedia.com/Combat",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%91%D0%B8%D1%82%D0%B2%D0%B0",
        "http://strongholdkingdoms-es.gamepedia.com/Combate",
        "http://strongholdkingdoms-pl.gamepedia.com/Walki",
        "http://strongholdkingdoms-tr.gamepedia.com/Muharebe",
        "http://strongholdkingdoms-it.gamepedia.com/Combattimento",
        "http://strongholdkingdoms-pt.gamepedia.com/Combate",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Scouts",
        "http://strongholdkingdoms-de.gamepedia.com/Kundschafter",
        "http://strongholdkingdoms-fr.gamepedia.com/Eclaireurs",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A0%D0%B0%D0%B7%D0%B2%D0%B5%D0%B4%D0%BA%D0%B0",
        "http://strongholdkingdoms-es.gamepedia.com/Exploradores",
        "http://strongholdkingdoms-pl.gamepedia.com/Zwiadu",
        "http://strongholdkingdoms-tr.gamepedia.com/Ke%C5%9Fif_Erleri",
        "http://strongholdkingdoms-it.gamepedia.com/Esploratori",
        "http://strongholdkingdoms-pt.gamepedia.com/Batedores",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Monks",
        "http://strongholdkingdoms-de.gamepedia.com/M%C3%B6nche",
        "http://strongholdkingdoms-fr.gamepedia.com/Moine",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9C%D0%BE%D0%BD%D0%B0%D1%85",
        "http://strongholdkingdoms-es.gamepedia.com/Monjes",
        "http://strongholdkingdoms-pl.gamepedia.com/Mnichom",
        "http://strongholdkingdoms-tr.gamepedia.com/Ke%C5%9Fi%C5%9Fler",
        "http://strongholdkingdoms-it.gamepedia.com/Monaco",
        "http://strongholdkingdoms-pt.gamepedia.com/Monges",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Trading",
        "http://strongholdkingdoms-de.gamepedia.com/Handel",
        "http://strongholdkingdoms-fr.gamepedia.com/Commerce",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9F%D1%80%D0%B5%D0%BC%D0%B8%D1%83%D0%BC-%D0%B6%D0%B5%D1%82%D0%BE%D0%BD%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Comerciar",
        "http://strongholdkingdoms-pl.gamepedia.com/Handel",
        "http://strongholdkingdoms-tr.gamepedia.com/Ticaret",
        "http://strongholdkingdoms-it.gamepedia.com/Commercio",
        "http://strongholdkingdoms-pt.gamepedia.com/Comércio",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Premium_Tokens",
        "http://strongholdkingdoms-de.gamepedia.com/Premium-Token",
        "http://strongholdkingdoms-fr.gamepedia.com/Jetons_Premium",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9F%D1%80%D0%B5%D0%BC%D0%B8%D1%83%D0%BC-%D0%B6%D0%B5%D1%82%D0%BE%D0%BD%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Vales_Premium",
        "http://strongholdkingdoms-pl.gamepedia.com/Premium",
        "http://strongholdkingdoms-tr.gamepedia.com/Premium_Token",
        "http://strongholdkingdoms-it.gamepedia.com/Gettoni_Premium",
        "http://strongholdkingdoms-pt.gamepedia.com/Fichas_prêmio",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Firefly_Crowns",
        "http://strongholdkingdoms-de.gamepedia.com/Firefly-Kronen",
        "http://strongholdkingdoms-fr.gamepedia.com/Couronne",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9A%D1%80%D0%BE%D0%BD%D1%8B_Firefly",
        "http://strongholdkingdoms-es.gamepedia.com/Coronas_Firefly",
        "http://strongholdkingdoms-pl.gamepedia.com/Korony",
        "http://strongholdkingdoms-tr.gamepedia.com/Firefly_Sikkeleri",
        "http://strongholdkingdoms-it.gamepedia.com/Corone_Firefly",
        "http://strongholdkingdoms-pt.gamepedia.com/Coroas_Firefly",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Strategy_Cards",
        "http://strongholdkingdoms-de.gamepedia.com/Strategiekarten",
        "http://strongholdkingdoms-fr.gamepedia.com/Cartes_Strat%C3%A9giques",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A1%D1%82%D1%80%D0%B0%D1%82%D0%B5%D0%B3%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B5_%D0%BA%D0%B0%D1%80%D1%82%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Cartas_de_estrategia",
        "http://strongholdkingdoms-pl.gamepedia.com/Karty_strategiczne",
        "http://strongholdkingdoms-tr.gamepedia.com/Strateji_Kartlar%C4%B1",
        "http://strongholdkingdoms-it.gamepedia.com/Carte_strategiche",
        "http://strongholdkingdoms-pt.gamepedia.com/Cartas_de_estratégia",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Strategy_Cards",
        "http://strongholdkingdoms-de.gamepedia.com/Strategiekarten",
        "http://strongholdkingdoms-fr.gamepedia.com/Cartes_Strat%C3%A9giques",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A1%D1%82%D1%80%D0%B0%D1%82%D0%B5%D0%B3%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B5_%D0%BA%D0%B0%D1%80%D1%82%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Cartas_de_estrategia",
        "http://strongholdkingdoms-pl.gamepedia.com/Karty_strategiczne",
        "http://strongholdkingdoms-tr.gamepedia.com/Strateji_Kartlar%C4%B1",
        "http://strongholdkingdoms-it.gamepedia.com/Carte_strategiche",
        "http://strongholdkingdoms-pt.gamepedia.com/Cartas_de_estratégia",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Premium_Tokens",
        "http://strongholdkingdoms-de.gamepedia.com/Premium-Token",
        "http://strongholdkingdoms-fr.gamepedia.com/Jetons_Premium",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9F%D1%80%D0%B5%D0%BC%D0%B8%D1%83%D0%BC-%D0%B6%D0%B5%D1%82%D0%BE%D0%BD%D1%8B",
        "http://strongholdkingdoms-es.gamepedia.com/Vales_Premium",
        "http://strongholdkingdoms-pl.gamepedia.com/Premium",
        "http://strongholdkingdoms-tr.gamepedia.com/Premium_Token",
        "http://strongholdkingdoms-it.gamepedia.com/Gettoni_Premium",
        "http://strongholdkingdoms-pt.gamepedia.com/Fichas_prêmio",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Donate_to_Parish",
        "http://strongholdkingdoms-de.gamepedia.com/Ressourcen_an_die_Hauptstadt_spenden",
        "http://strongholdkingdoms-fr.gamepedia.com/Donation_%C3%A0_la_Pr%C3%A9v%C3%B4t%C3%A9",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9F%D0%BE%D0%B6%D0%B5%D1%80%D1%82%D0%B2%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F_%D0%B2_%D0%BE%D0%BA%D1%80%D1%83%D0%B3",
        "http://strongholdkingdoms-es.gamepedia.com/Donar_a_la_parroquia",
        "http://strongholdkingdoms-pl.gamepedia.com/Datki_na_rzecz_gminy",
        "http://strongholdkingdoms-tr.gamepedia.com/Pari%C5%9F%27e_Ba%C4%9F%C4%B1%C5%9F",
        "http://strongholdkingdoms-it.gamepedia.com/Donazioni_al_distretto",
        "http://strongholdkingdoms-pt.gamepedia.com/Doe_à_paróquia",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Villages_Overview",
        "http://strongholdkingdoms-de.gamepedia.com/Dorf%C3%BCbersichtsanzeige",
        "http://strongholdkingdoms-fr.gamepedia.com/Vue_d%27Ensemble_Village",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9E%D0%B1%D0%B7%D0%BE%D1%80_%D0%B4%D0%B5%D1%80%D0%B5%D0%B2%D0%BD%D0%B8",
        "http://strongholdkingdoms-es.gamepedia.com/Vistazo_general_de_las_aldeas",
        "http://strongholdkingdoms-pl.gamepedia.com/Przegl%C4%85d_wiosek",
        "http://strongholdkingdoms-tr.gamepedia.com/K%C3%B6ylere_genel_bak%C4%B1%C5%9F",
        "http://strongholdkingdoms-it.gamepedia.com/Quadro_dei_villaggi",
        "http://strongholdkingdoms-pt.gamepedia.com/Visão_geral_das_aldeias",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Achievements",
        "http://strongholdkingdoms-de.gamepedia.com/Errungenschaften",
        "http://strongholdkingdoms-fr.gamepedia.com/Ach%C3%A8vements",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%94%D0%BE%D1%81%D1%82%D0%B8%D0%B6%D0%B5%D0%BD%D0%B8%D1%8F",
        "http://strongholdkingdoms-es.gamepedia.com/Logros",
        "http://strongholdkingdoms-pl.gamepedia.com/Osi%C4%85gni%C4%99cie",
        "http://strongholdkingdoms-tr.gamepedia.com/Ba%C5%9Far%C4%B1lar",
        "http://strongholdkingdoms-it.gamepedia.com/Imprese",
        "http://strongholdkingdoms-pt.gamepedia.com/Conquistas",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/The_Second_Age",
        "http://strongholdkingdoms-de.gamepedia.com/Die_Zweite_Epoche",
        "http://strongholdkingdoms-fr.gamepedia.com/Deuxi%C3%A8me_%C3%88re",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%92%D1%82%D0%BE%D1%80%D0%B0%D1%8F_%D0%AD%D0%BF%D0%BE%D1%85%D0%B0",
        "http://strongholdkingdoms-es.gamepedia.com/La_segunda_edad",
        "http://strongholdkingdoms-pl.gamepedia.com/Druga_Epoka",
        "http://strongholdkingdoms-tr.gamepedia.com/%C4%B0kinci_%C3%87a%C4%9F",
        "http://strongholdkingdoms-it.gamepedia.com/La_Seconda_Epoca",
        "http://strongholdkingdoms-pt.gamepedia.com/A_Segunda_Era",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/The_Third_Age",
        "http://strongholdkingdoms-de.gamepedia.com/Die_Dritte_Epoche",
        "http://strongholdkingdoms-fr.gamepedia.com/Troisi%C3%A8me_%C3%88re",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A2%D1%80%D0%B5%D1%82%D1%8C%D1%8F_%D0%AD%D0%BF%D0%BE%D1%85%D0%B0",
        "http://strongholdkingdoms-es.gamepedia.com/La_tercera_edad",
        "http://strongholdkingdoms-pl.gamepedia.com/Trzecia_Epoka",
        "http://strongholdkingdoms-tr.gamepedia.com/%C3%9C%C3%A7%C3%BCnc%C3%BC_%C3%87a%C4%9F",
        "http://strongholdkingdoms-it.gamepedia.com/La_Terza_Epoca",
        "http://strongholdkingdoms-pt.gamepedia.com/A_Terceira_Era",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Domination_World_4",
        "http://strongholdkingdoms-de.gamepedia.com/Domination_Welt_4",
        "http://strongholdkingdoms-fr.gamepedia.com/Monde_de_Domination_4",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9C%D0%B8%D1%80_Domination_4",
        "http://strongholdkingdoms-es.gamepedia.com/Mundo_Domination_4",
        "http://strongholdkingdoms-pl.gamepedia.com/%C5%9Awiat_Domination_4",
        "http://strongholdkingdoms-tr.gamepedia.com/Domination_D%C3%BCnyas%C4%B1_4",
        "http://strongholdkingdoms-it.gamepedia.com/Mondo_Domination_4",
        "http://strongholdkingdoms-pt.gamepedia.com/Mundo_Domina%C3%A7%C3%A3o_4",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/The_Fourth_Age",
        "http://strongholdkingdoms-de.gamepedia.com/Die_Vierte_Epoche",
        "http://strongholdkingdoms-fr.gamepedia.com/Quatri%C3%A8me_%C3%88re",
        "http://strongholdkingdoms-ru.gamepedia.com/Четвёртая_Эпоха",
        "http://strongholdkingdoms-es.gamepedia.com/La_cuarta_edad",
        "http://strongholdkingdoms-pl.gamepedia.com/Czwarta_Epoka",
        "http://strongholdkingdoms-tr.gamepedia.com/Dördüncü_Çağ",
        "http://strongholdkingdoms-it.gamepedia.com/La_Quarta_Epoca",
        "http://strongholdkingdoms-pt.gamepedia.com/A_Quarta_Era",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Treasure_Castle",
        "http://strongholdkingdoms-de.gamepedia.com/Schatzburg",
        "http://strongholdkingdoms-fr.gamepedia.com/Ch%C3%A2teau_au_Tr%C3%A9sor",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%97%D0%B0%D0%BC%D0%BA%D0%B8_%D1%81_%D1%81%D0%BE%D0%BA%D1%80%D0%BE%D0%B2%D0%B8%D1%89%D0%B0%D0%BC%D0%B8",
        "http://strongholdkingdoms-es.gamepedia.com/Castillo_del_tesoro",
        "http://strongholdkingdoms-pl.gamepedia.com/Zamki_ze_skarbami",
        "http://strongholdkingdoms-tr.gamepedia.com/Define_Kaleleri",
        "http://strongholdkingdoms-it.gamepedia.com/Castello_del_tesoro",
        "http://strongholdkingdoms-pt.gamepedia.com/Castelos_de_Tesouro",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Paladin_Castles",
        "http://strongholdkingdoms-de.gamepedia.com/Burg_des_Paladins",
        "http://strongholdkingdoms-fr.gamepedia.com/Ch%C3%A2teau_du_Paladin",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%97%D0%B0%D0%BC%D0%BE%D0%BA_%D0%9F%D0%B0%D0%BB%D0%B0%D0%B4%D0%B8%D0%BD%D0%B0",
        "http://strongholdkingdoms-es.gamepedia.com/Castillos_de_Palad%C3%ADn",
        "http://strongholdkingdoms-pl.gamepedia.com/Zamek_Paladyna",
        "http://strongholdkingdoms-tr.gamepedia.com/Paladin_Kaleleri",
        "http://strongholdkingdoms-it.gamepedia.com/Castello_del_Paladino",
        "http://strongholdkingdoms-pt.gamepedia.com/Castelo_do_paladino",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/The_Fifth_Age",
        "http://strongholdkingdoms-de.gamepedia.com/Die_F%C3%BCnfte_Epoche",
        "http://strongholdkingdoms-fr.gamepedia.com/Cinqui%C3%A8me_Ere",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9F%D1%8F%D1%82%D0%B0%D1%8F_%D0%AD%D0%BF%D0%BE%D1%85%D0%B0",
        "http://strongholdkingdoms-es.gamepedia.com/La_quinta_edad",
        "http://strongholdkingdoms-pl.gamepedia.com/Piąta_Epoka",
        "http://strongholdkingdoms-tr.gamepedia.com/Beşinci_Çağ",
        "http://strongholdkingdoms-it.gamepedia.com/La_quinta_epoca",
        "http://strongholdkingdoms-pt.gamepedia.com/Quinta_Era",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/The_Sixth_Age",
        "http://strongholdkingdoms-de.gamepedia.com/Die_Sechste_Epoche",
        "http://strongholdkingdoms-fr.gamepedia.com/Sixième_Ere",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%A8%D0%B5%D1%81%D1%82%D0%B0%D1%8F_%D0%AD%D0%BF%D0%BE%D1%85%D0%B0",
        "http://strongholdkingdoms-es.gamepedia.com/La_Sexta_Edad",
        "http://strongholdkingdoms-pl.gamepedia.com/Szósta_Epoka",
        "http://strongholdkingdoms-tr.gamepedia.com/Altıncı_Çağ",
        "http://strongholdkingdoms-it.gamepedia.com/La_Sesta_Epoca",
        "http://strongholdkingdoms-pt.gamepedia.com/A_Sexta_Era",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/The_Final_Age",
        "http://strongholdkingdoms-de.gamepedia.com/Die_Letzte_Epoche",
        "http://strongholdkingdoms-fr.gamepedia.com/Ere_Finalee",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9F%D0%BE%D1%81%D0%BB%D0%B5%D0%B4%D0%BD%D1%8F%D1%8F_%D0%AD%D0%BF%D0%BE%D1%85%D0%B0",
        "http://strongholdkingdoms-es.gamepedia.com/La_Edad_Final",
        "http://strongholdkingdoms-pl.gamepedia.com/Ostatnia_Epoka",
        "http://strongholdkingdoms-tr.gamepedia.com/Son_Çağ",
        "http://strongholdkingdoms-it.gamepedia.com/L'Epoca_Finale",
        "http://strongholdkingdoms-pt.gamepedia.com/A_Era_Final",
        "",
        "",
        "http://strongholdkingdoms.gamepedia.com/Royal_Towers",
        "http://strongholdkingdoms-de.gamepedia.com/K%C3%B6nigliche_T%C3%BCrme",
        "http://strongholdkingdoms-fr.gamepedia.com/Tours_Royales",
        "http://strongholdkingdoms-ru.gamepedia.com/%D0%9A%D0%BE%D1%80%D0%BE%D0%BB%D0%B5%D0%B2%D1%81%D0%BA%D0%B0%D1%8F_%D0%91%D0%B0%D1%88%D0%BD%D1%8F",
        "http://strongholdkingdoms-es.gamepedia.com/Torres_Reales",
        "http://strongholdkingdoms-pl.gamepedia.com/Królewska_Wieża",
        "http://strongholdkingdoms-tr.gamepedia.com/Kraliyet_Kuleleri",
        "http://strongholdkingdoms-it.gamepedia.com/Torri_Reali",
        "http://strongholdkingdoms-pt.gamepedia.com/Torres_Reais",
        "",
        "",
        "https://strongholdkingdoms.gamepedia.com/Eras",
        "https://strongholdkingdoms-de.gamepedia.com/%C3%84ra",
        "https://strongholdkingdoms-fr.gamepedia.com/Epoques",
        "https://strongholdkingdoms-ru.gamepedia.com/%D0%AD%D1%80%D0%B0",
        "https://strongholdkingdoms-es.gamepedia.com/Eras",
        "https://strongholdkingdoms-pl.gamepedia.com/Ery",
        "https://strongholdkingdoms.gamepedia.com/Eras",
        "https://strongholdkingdoms.gamepedia.com/Eras",
        "https://strongholdkingdoms.gamepedia.com/Eras",
        "",
        "",
        "https://strongholdkingdoms.fandom.com/wiki/Wolf_Worlds",
        "https://strongholdkingdoms.fandom.com/wiki/Wolf_Worlds",
        "https://strongholdkingdoms.fandom.com/wiki/Wolf_Worlds",
        "https://strongholdkingdoms.fandom.com/wiki/Wolf_Worlds",
        "https://strongholdkingdoms.fandom.com/wiki/Wolf_Worlds",
        "https://strongholdkingdoms.fandom.com/wiki/Wolf_Worlds",
        "https://strongholdkingdoms.fandom.com/wiki/Wolf_Worlds",
        "https://strongholdkingdoms.fandom.com/wiki/Wolf_Worlds",
        "https://strongholdkingdoms.fandom.com/wiki/Wolf_Worlds",
        "",
        ""
      };

      public static CustomSelfDrawPanel.WikiLinkControl init(
        CustomSelfDrawPanel.CSDControl parent,
        int screenID,
        Point position)
      {
        return CustomSelfDrawPanel.WikiLinkControl.init(parent, screenID, position, false);
      }

      public static CustomSelfDrawPanel.WikiLinkControl init(
        CustomSelfDrawPanel.CSDControl parent,
        int screenID,
        Point position,
        bool scaledSmaller)
      {
        if (AdvicePanel.usesAdvicePanel(screenID))
          InterfaceMgr.Instance.openAdvicePopupFirstTime(screenID);
        CustomSelfDrawPanel.WikiLinkControl control = new CustomSelfDrawPanel.WikiLinkControl();
        control.ImageNorm = (Image) GFXLibrary.int_button_Q_normal;
        control.ImageOver = (Image) GFXLibrary.int_button_Q_over;
        control.ImageClick = (Image) GFXLibrary.int_button_Q_in;
        if (scaledSmaller)
          control.Size = new Size(28, 28);
        control.Position = position;
        control.m_ID = screenID;
        control.CustomTooltipData = screenID;
        control.CustomTooltipID = 4400;
        control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(control.helpClicked));
        parent.addControl((CustomSelfDrawPanel.CSDControl) control);
        return control;
      }

      public static CustomSelfDrawPanel.WikiLinkControl init(
        CustomSelfDrawPanel parent,
        int screenID,
        Point position)
      {
        if (AdvicePanel.usesAdvicePanel(screenID))
          InterfaceMgr.Instance.openAdvicePopupFirstTime(screenID);
        CustomSelfDrawPanel.WikiLinkControl control = new CustomSelfDrawPanel.WikiLinkControl();
        control.ImageNorm = (Image) GFXLibrary.int_button_Q_normal;
        control.ImageOver = (Image) GFXLibrary.int_button_Q_over;
        control.ImageClick = (Image) GFXLibrary.int_button_Q_in;
        control.Position = position;
        control.m_ID = screenID;
        control.CustomTooltipData = screenID;
        control.CustomTooltipID = 4400;
        control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(control.helpClicked));
        parent.addControl((CustomSelfDrawPanel.CSDControl) control);
        return control;
      }

      public void helpClicked() => CustomSelfDrawPanel.WikiLinkControl.openHelpLink(this.m_ID);

      public static void openHelpLink(int index)
      {
        if (AdvicePanel.usesAdvicePanel(index))
          InterfaceMgr.Instance.openAdvicePopupFromButton(index);
        else
          CustomSelfDrawPanel.WikiLinkControl.openWikiPage(index);
      }

      public static void openWikiPage(int index)
      {
        if (GameEngine.Instance.LocalWorldData != null && GameEngine.Instance.LocalWorldData.EraWorld)
        {
          switch (index)
          {
            case 45:
            case 46:
            case 48:
            case 51:
            case 52:
            case 53:
              index = 55;
              break;
          }
        }
        int num = 0;
        switch (Program.mySettings.LanguageIdent)
        {
          case "en":
            num = 0;
            break;
          case "de":
            num = 1;
            break;
          case "fr":
            num = 2;
            break;
          case "ru":
            num = 3;
            break;
          case "es":
            num = 4;
            break;
          case "pl":
            num = 5;
            break;
          case "tr":
            num = 6;
            break;
          case "it":
            num = 7;
            break;
          case "pt":
            num = 8;
            break;
        }
        if (CustomSelfDrawPanel.WikiLinkControl.urls[index * 11 + num].Length == 0)
          num = 0;
        try
        {
          Process.Start(CustomSelfDrawPanel.WikiLinkControl.urls[index * 11 + num]);
        }
        catch (Exception ex)
        {
        }
      }
    }

    public class ParishChatPanel : CustomSelfDrawPanel.CSDControl
    {
      private List<Chat_TextEntry> currentText = new List<Chat_TextEntry>();
      private int currentChatHeight;
      private ParishWallPanel m_parent;
      private int m_id = -1;
      private bool locked;
      private bool repopulate;
      private bool allowBackFill = true;
      private CustomSelfDrawPanel.CSDVertScrollBar chatScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
      private CustomSelfDrawPanel.CSDArea chatScrollArea = new CustomSelfDrawPanel.CSDArea();
      private CustomSelfDrawPanel.CSDLabel oldMessagesLabel = new CustomSelfDrawPanel.CSDLabel();

      public bool Locked => this.locked;

      public bool Repopulate
      {
        set => this.repopulate = value;
      }

      public void reset(ParishWallPanel parent, int id)
      {
        this.m_parent = parent;
        this.m_id = id;
        this.currentText.Clear();
        this.currentChatHeight = 0;
        this.clearControls();
        this.locked = false;
        this.allowBackFill = true;
        if (id != 3)
          return;
        this.locked = true;
      }

      public void setAsSteward() => this.locked = false;

      public void setUnreads(int numUnread)
      {
        string title = "";
        switch (this.m_id)
        {
          case 0:
            title = SK.Text("ParishWallPanel_General", "General");
            break;
          case 1:
            title = SK.Text("ParishWallPanel_War", "War");
            break;
          case 2:
            title = SK.Text("ParishWallPanel_inn", "Inn");
            break;
          case 3:
            title = SK.Text("ParishWallPanel_Steward", "Steward");
            break;
          case 4:
            title = SK.Text("GENERIC_Parish", "Parish");
            break;
          case 5:
            title = SK.Text("MENU_Help", "Help");
            break;
        }
        if (numUnread > 0)
          title = title + " (" + numUnread.ToString() + ")";
        this.m_parent.setTabText(this.m_id, title);
      }

      public void importText(Chat_TextEntry[] newText, bool backFill, long deleteID)
      {
        int num1 = newText.Length;
        if (num1 == 0 && !this.repopulate && !backFill && deleteID < 0L)
          return;
        this.repopulate = false;
        if (backFill && num1 == 0)
          this.allowBackFill = false;
        if (deleteID >= 0L)
        {
          this.allowBackFill = true;
          for (int index = 0; index < this.currentText.Count; ++index)
          {
            if (this.currentText[index].textID == deleteID)
            {
              this.currentText.Remove(this.currentText[index]);
              break;
            }
          }
        }
        else if (!backFill)
        {
          List<Chat_TextEntry> chatTextEntryList = new List<Chat_TextEntry>();
          chatTextEntryList.AddRange((IEnumerable<Chat_TextEntry>) newText);
          chatTextEntryList.AddRange((IEnumerable<Chat_TextEntry>) this.currentText);
          this.currentText = chatTextEntryList;
        }
        else
        {
          this.currentText.AddRange((IEnumerable<Chat_TextEntry>) newText);
          num1 = 0;
        }
        if (this.currentText.Count > 150)
          this.currentText.RemoveRange(150, this.currentText.Count - 150);
        int num2 = this.chatScrollBar.Value;
        bool flag = false;
        if (this.chatScrollArea.Y == 0)
          flag = true;
        this.clearControls();
        this.chatScrollArea.Position = new Point(0, 0);
        this.chatScrollArea.Size = new Size(this.Width - 60, this.Height);
        this.chatScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(this.Width - 60, this.Height));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.chatScrollArea);
        this.chatScrollBar.Visible = false;
        this.chatScrollBar.Position = new Point(this.Width - 26, 0);
        this.chatScrollBar.Size = new Size(24, this.Height);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.chatScrollBar);
        this.chatScrollBar.Value = 0;
        this.chatScrollBar.Max = 100;
        this.chatScrollBar.NumVisibleLines = 25;
        this.chatScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
        this.chatScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.chatScrollBarMoved));
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        foreach (Chat_TextEntry textEntry in this.currentText)
        {
          CustomSelfDrawPanel.ParishWallChatEntry control1 = new CustomSelfDrawPanel.ParishWallChatEntry();
          control1.Position = new Point(0, num3);
          control1.init(textEntry, this.m_parent);
          this.chatScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control1);
          num3 += control1.Height;
          this.currentChatHeight += control1.Height;
          if (num4 < this.currentText.Count - 1 || this.allowBackFill)
          {
            CustomSelfDrawPanel.CSDImage control2 = new CustomSelfDrawPanel.CSDImage();
            control2.Image = (Image) GFXLibrary.parishwall_dividing_line;
            control2.Position = new Point(0, num3 + 3);
            this.chatScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control2);
            num3 += 10;
          }
          if (num4 + 1 == num1)
            num5 = num3;
          ++num4;
        }
        if (this.allowBackFill)
        {
          this.oldMessagesLabel.Text = SK.Text("ParishWallPanel_Older_Messages", "Older Messages");
          this.oldMessagesLabel.Color = ARGBColors.Blue;
          this.oldMessagesLabel.Position = new Point(63, num3 + 3);
          this.oldMessagesLabel.Size = new Size(405, 30);
          this.oldMessagesLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
          this.oldMessagesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
          this.oldMessagesLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.downloadOlderMessages), "ParishChatPanel_view_older_messages");
          this.oldMessagesLabel.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.oldMessagesOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.oldMessagesLeave));
          this.oldMessagesLabel.Enabled = true;
          this.chatScrollArea.addControl((CustomSelfDrawPanel.CSDControl) this.oldMessagesLabel);
          num3 += 25;
        }
        this.chatScrollArea.Size = new Size(this.chatScrollArea.Width, num3);
        if (num3 < this.chatScrollBar.Height)
        {
          this.chatScrollBar.Visible = false;
        }
        else
        {
          this.chatScrollBar.Visible = true;
          this.chatScrollBar.NumVisibleLines = this.chatScrollBar.Height;
          this.chatScrollBar.Max = num3 - this.chatScrollBar.Height;
        }
        this.chatScrollArea.invalidate();
        this.chatScrollBar.invalidate();
        if (this.m_parent != null)
          this.m_parent.Invalidate();
        if (flag)
          return;
        int num6 = num2 + num5;
        if (num6 <= 0 || !this.chatScrollBar.Visible)
          return;
        if (num6 >= this.chatScrollBar.Max)
          num6 = this.chatScrollBar.Max;
        this.chatScrollBar.Value = num6;
        this.chatScrollBarMoved();
      }

      private void chatScrollBarMoved()
      {
        int y = this.chatScrollBar.Value;
        this.chatScrollArea.Position = new Point(this.chatScrollArea.X, -y);
        this.chatScrollArea.ClipRect = new Rectangle(this.chatScrollArea.ClipRect.X, y, this.chatScrollArea.ClipRect.Width, this.chatScrollArea.ClipRect.Height);
        this.chatScrollArea.invalidate();
        this.chatScrollBar.invalidate();
      }

      public void scrollToBottom()
      {
        this.chatScrollBar.Value = 0;
        this.chatScrollBarMoved();
      }

      public void downloadOlderMessages()
      {
        if (this.m_parent == null)
          return;
        this.m_parent.backfillPage(this.m_id);
        this.oldMessagesLabel.Enabled = false;
        this.oldMessagesLeave();
      }

      public void oldMessagesOver() => this.oldMessagesLabel.Color = ARGBColors.Aquamarine;

      public void oldMessagesLeave() => this.oldMessagesLabel.Color = ARGBColors.Blue;

      public void freeOldMessagesButton() => this.oldMessagesLabel.Enabled = true;
    }

    public class ParishWallEntry : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel playerName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel effectText = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage effectImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
      private int m_lineID;
      private WallInfo m_wallInfo;
      private int m_villageID = -1;
      private CustomSelfDrawPanel m_parent;
      private Point donatePopupLocation = new Point();

      public void init(WallInfo wallInfo, int lineID, int villageID, CustomSelfDrawPanel window)
      {
        this.m_parent = window;
        this.m_villageID = villageID;
        this.m_lineID = lineID;
        this.m_wallInfo = wallInfo;
        this.clearControls();
        this.backgroundImage.Image = (lineID & 1) != 0 ? (Image) GFXLibrary.parishwall_tan_bar_02 : (Image) GFXLibrary.parishwall_tan_bar_01;
        this.backgroundImage.Position = new Point(0, 0);
        this.backgroundImage.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
        this.backgroundImage.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null, (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.shieldImage.Image = GameEngine.Instance.World.getWorldShield(wallInfo.userID, 32, 36);
        this.shieldImage.Position = new Point(10, 5);
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.shieldImage);
        this.playerName.Text = wallInfo.username;
        this.playerName.Color = ARGBColors.Black;
        this.playerName.Position = new Point(60, 4);
        this.playerName.Size = new Size(214, 16);
        this.playerName.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.playerName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.playerName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playerClicked), "ParishChatPanel_user");
        this.playerName.Data = wallInfo.userID;
        this.playerName.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.playerOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.playerLeave));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.playerName);
        Graphics graphics = window.CreateGraphics();
        this.playerName.Size = new Size(graphics.MeasureString(wallInfo.username, this.playerName.Font, 214).ToSize().Width + 5, 16);
        graphics.Dispose();
        this.effectText.Text = "";
        this.effectText.Color = Color.FromArgb(38, 77, 0);
        this.effectText.Position = new Point(60, 19);
        this.effectText.Size = new Size(214, 28);
        this.effectText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.effectText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.effectText);
        int index = -1;
        switch (wallInfo.entryType)
        {
          case 1:
            this.effectText.Text = SK.Text("ParishWallPanel_Donates_Goods", "Donates Goods");
            index = 2;
            this.backgroundImage.Data = wallInfo.entryType;
            this.backgroundImage.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.overDonate), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.leaveDonate));
            this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickDonate), "ParishChatPanel_donate_popup");
            break;
          case 2:
            this.effectText.Text = SK.Text("ParishWallPanel_Upgrades", "Upgrades") + " : " + VillageBuildingsData.getBuildingName(wallInfo.data1);
            this.effectImage.Image = (Image) this.getCapitalBuildingImage(wallInfo.data1);
            this.effectImage.Size = new Size(60, 60);
            this.effectImage.Position = new Point(272, -7);
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.effectImage);
            break;
          case 10:
            this.effectText.Text = SK.Text("ParishWallPanel_Destroys_Bandit_Camp", "Destroys a Bandit Camp");
            index = 3;
            break;
          case 11:
            this.effectText.Text = SK.Text("ParishWallPanel_Destroys_Wolf_Lair", "Destroys a Wolf Lair");
            index = 1;
            break;
          case 12:
            this.effectText.Text = SK.Text("ParishWallPanel_Defeats_rat", "Defeats the Rat");
            index = 12;
            break;
          case 13:
            this.effectText.Text = SK.Text("ParishWallPanel_Defeats_Snake", "Defeats the Snake");
            index = 12;
            break;
          case 14:
            this.effectText.Text = SK.Text("ParishWallPanel_Defeats_Pig", "Defeats the Pig");
            index = 12;
            break;
          case 15:
            this.effectText.Text = SK.Text("Defeats_Wolf", "Defeats the Wolf");
            index = 12;
            break;
          case 16:
            this.effectText.Text = SK.Text("Defeats_Paladin", "Defeats Paladin's Castle");
            index = 14;
            break;
          case 17:
            this.effectText.Text = SK.Text("Defeats_Paladin", "Defeats Paladin's Castle");
            index = 14;
            break;
          case 18:
            this.effectText.Text = SK.Text("Defeats_Treasure_Castle", "Defeats a Treasure Castle");
            index = 15;
            break;
          case 20:
            this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Bandit_Camp", "Attacks a Bandit Camp");
            index = 3;
            break;
          case 21:
            this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Wolf_Lair", "Attacks a Wolf Lair");
            index = 1;
            break;
          case 22:
            this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Rat", "Attacks the Rat");
            index = 12;
            break;
          case 23:
            this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Snake", "Attacks the Snake");
            index = 12;
            break;
          case 24:
            this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Pig", "Attacks the Pig");
            index = 12;
            break;
          case 25:
            this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Wolf", "Attacks the Wolf");
            index = 12;
            break;
          case 26:
            this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Paladin", "Attacks Paladin's Castle");
            index = 14;
            break;
          case 27:
            this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Paladin", "Attacks Paladin's Castle");
            index = 14;
            break;
          case 28:
            this.effectText.Text = SK.Text("ParishWallPanel__Treasure_Castle", "Attacks a Treasure Castle");
            index = 15;
            break;
          case 30:
            this.effectText.Text = SK.Text("ParishWallPanel_Capture_Flag", "Captures a Flag");
            index = 4;
            break;
          case 31:
            this.effectText.Text = SK.Text("ParishWallPanel_Taken_Flag", "Taken Flag");
            index = 5;
            break;
          case 40:
            this.effectText.Text = SK.Text("ParishWallPanel_Blesses", "Blesses the Parish");
            index = 7;
            break;
          case 42:
            this.effectText.Text = SK.Text("ParishWallPanel_Influences", "Influences Election");
            index = 7;
            break;
          case 43:
            this.effectText.Text = SK.Text("ParishWallPanel_Inquisition", "Inquisitions the Parish");
            index = 7;
            break;
          case 44:
            this.effectText.Text = SK.Text("ParishWallPanel_Heals", "Heals some disease in the parish");
            index = 7;
            break;
          case 50:
            this.effectText.Text = SK.Text("ParishWallPanel_Joins_Parish", "Joins the Parish");
            index = 8;
            break;
          case 51:
            this.effectText.Text = SK.Text("ParishWallPanel_Promotes_To", "Promotes To") + " : " + Rankings.getRankingName(wallInfo.data1, wallInfo.data2 == 0);
            index = 0;
            break;
          case 52:
            this.effectText.Text = SK.Text("ParishWallPanel_Becomes_Steward", "Becomes Steward");
            index = 13;
            break;
          case 53:
            this.effectText.Text = SK.Text("ParishWallPanel_Becomes_Sheriff", "Becomes Sheriff");
            index = 13;
            break;
          case 54:
            this.effectText.Text = SK.Text("ParishWallPanel_Becomes_Governor", "Becomes Governor");
            index = 13;
            break;
          case 55:
            this.effectText.Text = SK.Text("ParishWallPanel_Becomes_King", "Becomes King");
            index = 13;
            break;
        }
        if (index < 0)
          return;
        this.effectImage.Image = (Image) GFXLibrary.parishwall_village_center_achievement_icons[index];
        this.effectImage.Position = new Point(274, -5);
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.effectImage);
      }

      public BaseImage getCapitalBuildingImage(int buildingType)
      {
        BaseImage capitalBuildingImage = (BaseImage) null;
        switch (buildingType)
        {
          case 79:
            capitalBuildingImage = GFXLibrary.townbuilding_Woodcutter_normal;
            break;
          case 80:
            capitalBuildingImage = GFXLibrary.townbuilding_stonequarry_normal;
            break;
          case 81:
            capitalBuildingImage = GFXLibrary.townbuilding_iron_normal;
            break;
          case 82:
            capitalBuildingImage = GFXLibrary.townbuilding_pitch_normal;
            break;
          case 83:
            capitalBuildingImage = GFXLibrary.townbuilding_ale_normal;
            break;
          case 84:
            capitalBuildingImage = GFXLibrary.townbuilding_apples_normal;
            break;
          case 85:
            capitalBuildingImage = GFXLibrary.townbuilding_cheese_normal;
            break;
          case 86:
            capitalBuildingImage = GFXLibrary.townbuilding_meat_normal;
            break;
          case 87:
            capitalBuildingImage = GFXLibrary.townbuilding_bread_normal;
            break;
          case 88:
            capitalBuildingImage = GFXLibrary.townbuilding_veg_normal;
            break;
          case 89:
            capitalBuildingImage = GFXLibrary.townbuilding_fish_normal;
            break;
          case 90:
            capitalBuildingImage = GFXLibrary.townbuilding_bows_normal;
            break;
          case 91:
            capitalBuildingImage = GFXLibrary.townbuilding_pikes_normal;
            break;
          case 92:
            capitalBuildingImage = GFXLibrary.townbuilding_armour_normal;
            break;
          case 93:
            capitalBuildingImage = GFXLibrary.townbuilding_sword_normal;
            break;
          case 94:
            capitalBuildingImage = GFXLibrary.townbuilding_catapults_normal;
            break;
          case 95:
            capitalBuildingImage = GFXLibrary.townbuilding_venison_normal;
            break;
          case 96:
            capitalBuildingImage = GFXLibrary.townbuilding_wine_normal;
            break;
          case 97:
            capitalBuildingImage = GFXLibrary.townbuilding_salt_normal;
            break;
          case 98:
            capitalBuildingImage = GFXLibrary.townbuilding_carpenter_normal;
            break;
          case 99:
            capitalBuildingImage = GFXLibrary.townbuilding_tailor_normal;
            break;
          case 100:
            capitalBuildingImage = GFXLibrary.townbuilding_metalware_normal;
            break;
          case 101:
            capitalBuildingImage = GFXLibrary.townbuilding_spice_normal;
            break;
          case 102:
            capitalBuildingImage = GFXLibrary.townbuilding_silk_normal;
            break;
          case 103:
            capitalBuildingImage = GFXLibrary.townbuilding_architectsguild_normal;
            break;
          case 104:
            capitalBuildingImage = GFXLibrary.townbuilding_Labourersbillets_normal;
            break;
          case 105:
            capitalBuildingImage = GFXLibrary.townbuilding_castellanshouse_normal;
            break;
          case 106:
            capitalBuildingImage = GFXLibrary.townbuilding_sergeantsatarmsoffice_normal;
            break;
          case 107:
            capitalBuildingImage = GFXLibrary.townbuilding_stables_normal;
            break;
          case 108:
            capitalBuildingImage = GFXLibrary.townbuilding_barracks_normal;
            break;
          case 109:
            capitalBuildingImage = GFXLibrary.townbuilding_peasntshall_normal;
            break;
          case 110:
            capitalBuildingImage = GFXLibrary.townbuilding_archeryrange_normal;
            break;
          case 111:
            capitalBuildingImage = GFXLibrary.townbuilding_pikemandrillyard_normal;
            break;
          case 112:
            capitalBuildingImage = GFXLibrary.townbuilding_combatarena_normal;
            break;
          case 113:
            capitalBuildingImage = GFXLibrary.townbuilding_siegeengineersguild_normal;
            break;
          case 114:
            capitalBuildingImage = GFXLibrary.townbuilding_officersquarters_normal;
            break;
          case 115:
            capitalBuildingImage = GFXLibrary.townbuilding_militaryschool_normal;
            break;
          case 116:
            capitalBuildingImage = GFXLibrary.townbuilding_supplydepot_normal;
            break;
          case 117:
            capitalBuildingImage = GFXLibrary.townbuilding_townhall_normal;
            break;
          case 118:
            capitalBuildingImage = GFXLibrary.townbuilding_church_normal;
            break;
          case 119:
            capitalBuildingImage = GFXLibrary.townbuilding_towngarden_normal;
            break;
          case 120:
            capitalBuildingImage = GFXLibrary.townbuilding_statue_normal;
            break;
          case 121:
            capitalBuildingImage = GFXLibrary.townbuilding_turretmaker_normal;
            break;
          case 122:
            capitalBuildingImage = GFXLibrary.townbuilding_tunnellorsguild_normal;
            break;
          case 123:
            capitalBuildingImage = GFXLibrary.townbuilding_ballistamaker_normal;
            break;
        }
        return capitalBuildingImage;
      }

      public void playerClicked()
      {
        if (this.csd.ClickedControl == null)
          return;
        int data = this.csd.ClickedControl.Data;
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.showUserInfoScreen(new WorldMap.CachedUserInfo()
        {
          userID = data
        });
      }

      public void playerOver() => this.playerName.Color = ARGBColors.White;

      public void playerLeave() => this.playerName.Color = ARGBColors.Black;

      public void overDonate()
      {
        if (!this.backgroundImage.Enabled)
          return;
        this.backgroundImage.Image = (Image) GFXLibrary.parishwall_tan_bar_03;
      }

      public void leaveDonate()
      {
        if ((this.m_lineID & 1) == 0)
          this.backgroundImage.Image = (Image) GFXLibrary.parishwall_tan_bar_01;
        else
          this.backgroundImage.Image = (Image) GFXLibrary.parishwall_tan_bar_02;
      }

      public void clickDonate()
      {
        if (this.csd.ClickedControl == null || !this.backgroundImage.Enabled)
          return;
        CustomSelfDrawPanel.CSDControl clickedControl = this.csd.ClickedControl;
        this.donatePopupLocation = clickedControl.getPanelPosition();
        this.donatePopupLocation = new Point(this.donatePopupLocation.X + clickedControl.Width, this.donatePopupLocation.Y);
        this.donatePopupLocation = this.m_parent.PointToScreen(this.donatePopupLocation);
        this.showDetailedInfo(this.m_wallInfo.userID, this.m_wallInfo.entryType);
        this.leaveDonate();
      }

      public void showDetailedInfo(int userID, int wallType)
      {
        ParishWallDetailInfo_ReturnType wallDonateDetails = GameEngine.Instance.World.getParishWallDonateDetails(this.m_villageID, userID);
        if (wallDonateDetails == null)
        {
          RemoteServices.Instance.set_ParishWallDetailInfo_UserCallBack(new RemoteServices.ParishWallDetailInfo_UserCallBack(this.parishWallDetailInfoCallBack));
          RemoteServices.Instance.ParishWallDetailInfo(this.m_villageID, userID, wallType);
          this.backgroundImage.Enabled = false;
        }
        else
          this.parishWallDetailInfoCallBack(wallDonateDetails);
      }

      public void parishWallDetailInfoCallBack(ParishWallDetailInfo_ReturnType returnData)
      {
        this.backgroundImage.Enabled = true;
        if (!returnData.Success || returnData.detailedInfo == null || returnData.detailedInfo.Count <= 0 || returnData.detailedInfo[0].entryType != 1)
          return;
        if (returnData.m_errorID != 999)
        {
          returnData.m_errorID = 999;
          GameEngine.Instance.World.registerParishWallDonateDetails(returnData);
        }
        DonatePopup.CreateDonatePopup(this.donatePopupLocation, returnData);
      }
    }

    public class ParishWallChatEntry : CustomSelfDrawPanel.CSDControl
    {
      private static int spaceWidth = -1;
      private CustomSelfDrawPanel.CSDArea textArea = new CustomSelfDrawPanel.CSDArea();
      private CustomSelfDrawPanel.CSDLabel playerName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel bodyText = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel dateText = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage effectImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDButton deleteButton = new CustomSelfDrawPanel.CSDButton();
      private long textID = -1;
      private ParishWallPanel parent;
      private MyMessageBoxPopUp PopUpRef;

      public void init(Chat_TextEntry textEntry, ParishWallPanel window)
      {
        this.parent = window;
        this.textID = textEntry.textID;
        this.shieldImage.Image = GameEngine.Instance.World.getWorldShield(textEntry.userID, 32, 36);
        this.shieldImage.Position = new Point(15, 7);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.shieldImage);
        this.playerName.Text = textEntry.username;
        this.playerName.Color = ARGBColors.Blue;
        this.playerName.Position = new Point(0, 0);
        this.playerName.Size = new Size(405, 30);
        this.playerName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.playerName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.playerName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playerClicked), "ParishChatPanel_user");
        this.playerName.Data = textEntry.userID;
        this.playerName.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.playerOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.playerLeave));
        this.playerName.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.copyTextToClipboardClick));
        this.bodyText.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        Graphics graphics = window.CreateGraphics();
        Size size = graphics.MeasureString(textEntry.username, this.playerName.Font, 1000000).ToSize();
        this.playerName.Size = new Size(size.Width + 5, 20);
        if (CustomSelfDrawPanel.ParishWallChatEntry.spaceWidth < 0)
          CustomSelfDrawPanel.ParishWallChatEntry.spaceWidth = graphics.MeasureString(" ", this.bodyText.Font, 1000000).ToSize().Width;
        string str = "";
        for (int index = size.Width + 15; index > 0; index -= CustomSelfDrawPanel.ParishWallChatEntry.spaceWidth)
          str += " ";
        string text = str + textEntry.text;
        this.textArea = new CustomSelfDrawPanel.CSDArea();
        this.textArea.Position = new Point(63, 0);
        this.textArea.Size = new Size(405, 1000);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.textArea);
        this.bodyText.Text = text;
        this.bodyText.Color = ARGBColors.Black;
        this.bodyText.Position = new Point(0, 0);
        this.bodyText.Size = new Size(405, 1000);
        this.bodyText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.bodyText.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.copyTextToClipboardClick));
        this.textArea.addControl((CustomSelfDrawPanel.CSDControl) this.bodyText);
        this.textArea.addControl((CustomSelfDrawPanel.CSDControl) this.playerName);
        int height = graphics.MeasureString(text, this.bodyText.Font, 405).ToSize().Height + 20;
        if (height < 63)
          height = 63;
        TimeSpan timeSpan = VillageMap.getCurrentServerTime() - textEntry.postedTime;
        if (timeSpan.TotalMinutes < 1.0)
        {
          int num = (int) timeSpan.TotalSeconds;
          if (num <= 0)
            num = 1;
          this.dateText.Text = num == 1 ? num.ToString() + " " + SK.Text("ParishWallPanel_X_Second_Ago", "second ago") : num.ToString() + " " + SK.Text("ParishWallPanel_X_Seconds_Ago", "seconds ago");
        }
        else if (timeSpan.TotalHours < 1.0)
        {
          int num = (int) timeSpan.TotalMinutes;
          if (num <= 0)
            num = 1;
          this.dateText.Text = num == 1 ? num.ToString() + " " + SK.Text("ParishWallPanel_X_Minute_Ago", "minute ago") : num.ToString() + " " + SK.Text("ParishWallPanel_X_Minutes_Ago", "minutes ago");
        }
        else if (timeSpan.TotalHours < 24.0)
        {
          int num = (int) timeSpan.TotalHours;
          if (num <= 0)
            num = 1;
          this.dateText.Text = num == 1 ? num.ToString() + " " + SK.Text("ParishWallPanel_X_Hour_Ago", "hour ago") : num.ToString() + " " + SK.Text("ParishWallPanel_X_Hours_Ago", "hours ago");
        }
        else
        {
          int num = (int) timeSpan.TotalDays;
          if (num <= 0)
            num = 1;
          this.dateText.Text = num == 1 ? num.ToString() + " " + SK.Text("ParishWallPanel_X_Day_Ago", "day ago") : num.ToString() + " " + SK.Text("ParishWallPanel_X_Days_Ago", "days ago");
        }
        this.dateText.Color = Color.FromArgb(77, 79, 81);
        this.dateText.Position = new Point(63, height - 20);
        this.dateText.Size = new Size(405, 30);
        this.dateText.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        this.dateText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.dateText);
        this.Size = new Size(405, height);
        this.textArea.ClipVisible = true;
        graphics.Dispose();
        if (!RemoteServices.Instance.Admin && !RemoteServices.Instance.Moderator && RemoteServices.Instance.UserID != ParishWallPanel.m_userIDOnCurrent)
          return;
        this.deleteButton.ImageNorm = (Image) GFXLibrary.trashcan_normal;
        this.deleteButton.ImageOver = (Image) GFXLibrary.trashcan_over;
        this.deleteButton.ImageClick = (Image) GFXLibrary.trashcan_clicked;
        this.deleteButton.Position = new Point(445, height - 20);
        this.deleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteClicked), "FactionNewForumPostsPanel_delete_post");
        this.addControl((CustomSelfDrawPanel.CSDControl) this.deleteButton);
      }

      private void deleteClicked()
      {
        if (MyMessageBox.Show(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FORUMS_Delete_Post", "Delete This Post"), MessageBoxButtons.YesNo) != DialogResult.Yes)
          return;
        this.DeletePost();
      }

      private void DeletePost()
      {
        RemoteServices.Instance.Chat_Admin_Command(6, (int) this.textID);
        if (this.parent == null)
          return;
        this.parent.deleteWallPost(this.textID);
      }

      public void playerClicked()
      {
        if (this.csd.ClickedControl == null)
          return;
        int data = this.csd.ClickedControl.Data;
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.showUserInfoScreen(new WorldMap.CachedUserInfo()
        {
          userID = data
        });
      }

      public void playerOver() => this.playerName.Color = ARGBColors.Aquamarine;

      public void playerLeave() => this.playerName.Color = ARGBColors.Blue;

      private void copyTextToClipboardClick()
      {
        if (this.csd == null || this.csd.ClickedControl == null || this.csd.ClickedControl.GetType() != typeof (CustomSelfDrawPanel.CSDLabel))
          return;
        Clipboard.SetText(((CustomSelfDrawPanel.CSDLabel) this.csd.ClickedControl).Text.TrimStart((char[]) null));
      }
    }

    public class MedalImage : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage ribbonBase = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage ribbonOverlay = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage nail = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage medalMetal = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage medalImage = new CustomSelfDrawPanel.CSDImage();
      private int m_achievement;
      private int m_achievementRank;
      private int m_rawAchievement;
      private CustomSelfDrawPanel.MedalWindow m_parent;

      public void init(int achievement, CustomSelfDrawPanel.MedalWindow parent)
      {
        this.m_rawAchievement = achievement;
        this.m_parent = parent;
        int num1 = 3002;
        bool flag = true;
        if (achievement < 0)
        {
          flag = false;
          achievement = -achievement;
          num1 = 3001;
        }
        if (parent != null && parent.ownPlayer)
          num1 += 2;
        this.m_achievement = achievement & 4095;
        int index;
        switch (achievement & 1879048192)
        {
          case 268435456:
            this.m_achievementRank = 1;
            index = 1;
            break;
          case 536870912:
            this.m_achievementRank = 2;
            index = 2;
            break;
          case 1073741824:
            this.m_achievementRank = 3;
            index = 3;
            break;
          case 1342177280:
            this.m_achievementRank = 4;
            index = 53;
            break;
          case 1610612736:
            this.m_achievementRank = 5;
            index = 54;
            break;
          case 1879048192:
            this.m_achievementRank = 6;
            index = 55;
            break;
          default:
            this.m_achievementRank = 0;
            index = 0;
            break;
        }
        this.clearControls();
        Color color = ARGBColors.White;
        float num2 = 1f;
        if (!flag)
        {
          color = Color.FromArgb(128, 128, 128, 128);
          num2 = 0.7f;
        }
        this.Size = new Size(81, 110);
        int ribbonColour = this.getRibbonColour(this.m_achievement);
        this.ribbonBase.Image = (Image) GFXLibrary.achievement_ribbons_base[ribbonColour];
        this.ribbonBase.Position = new Point(0, 0);
        this.ribbonBase.Colorise = color;
        this.ribbonBase.Alpha = num2;
        this.ribbonBase.CustomTooltipID = num1;
        this.ribbonBase.CustomTooltipData = achievement;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.ribbonBase);
        if (this.m_achievementRank != 0)
        {
          if (this.m_achievementRank == 1)
            this.ribbonOverlay.Image = (Image) GFXLibrary.achievement_ribbons_edges[ribbonColour];
          else if (this.m_achievementRank == 2)
            this.ribbonOverlay.Image = (Image) GFXLibrary.achievement_ribbons_centre[ribbonColour];
          else if (this.m_achievementRank >= 3)
            this.ribbonOverlay.Image = (Image) GFXLibrary.ribbon_comp_centerstripe_gold;
          this.ribbonOverlay.Position = new Point(0, 0);
          this.ribbonOverlay.Colorise = color;
          this.ribbonOverlay.Alpha = num2;
          this.ribbonOverlay.CustomTooltipID = num1;
          this.ribbonOverlay.CustomTooltipData = achievement;
          this.ribbonBase.addControl((CustomSelfDrawPanel.CSDControl) this.ribbonOverlay);
        }
        this.nail.Image = (Image) GFXLibrary.ribbon_comp_nail;
        this.nail.Position = new Point(0, 0);
        this.nail.Colorise = color;
        this.nail.Alpha = num2;
        this.nail.CustomTooltipID = num1;
        this.nail.CustomTooltipData = achievement;
        this.ribbonBase.addControl((CustomSelfDrawPanel.CSDControl) this.nail);
        this.medalMetal.Image = (Image) GFXLibrary.medal_images[index];
        this.medalMetal.Position = new Point(8, 58);
        this.medalMetal.Colorise = color;
        this.medalMetal.Alpha = num2;
        this.medalMetal.CustomTooltipID = num1;
        this.medalMetal.CustomTooltipData = achievement;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.medalMetal);
        this.medalImage.Image = (Image) GFXLibrary.medal_images[CustomSelfDrawPanel.MedalImage.getAchievementImage(this.m_achievement)];
        this.medalImage.Position = new Point(0, 0);
        this.medalImage.Colorise = color;
        this.medalImage.Alpha = num2;
        this.medalImage.CustomTooltipID = num1;
        this.medalImage.CustomTooltipData = achievement;
        this.medalMetal.addControl((CustomSelfDrawPanel.CSDControl) this.medalImage);
        if (this.m_rawAchievement >= 0)
          this.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.achClicked));
        this.invalidate();
      }

      private void achClicked()
      {
        if (this.m_parent == null)
          return;
        this.m_parent.achievementClicked(this.m_rawAchievement);
      }

      private int getRibbonColour(int achievement)
      {
        switch (achievement)
        {
          case 1:
          case 2:
          case 3:
          case 4:
          case 5:
          case 6:
          case 7:
          case 8:
          case 9:
          case 10:
          case 11:
          case 12:
          case 13:
          case 14:
          case 15:
          case 16:
            return 0;
          case 33:
          case 34:
          case 35:
          case 36:
          case 37:
            return 1;
          case 65:
          case 66:
          case 67:
            return 2;
          case 97:
          case 98:
          case 99:
          case 100:
          case 101:
            return 3;
          case 129:
          case 130:
          case 131:
            return 4;
          case 161:
          case 162:
          case 163:
            return 5;
          case 193:
          case 194:
          case 195:
            return 6;
          case 225:
          case 226:
            return 7;
          case 257:
            return 8;
          case 289:
          case 290:
            return 9;
          case 321:
            return 10;
          case 353:
          case 354:
            return 11;
          case 385:
          case 386:
          case 387:
          case 388:
            return 12;
          default:
            return 0;
        }
      }

      public static int getAchievementImage(int achievement)
      {
        switch (achievement)
        {
          case 1:
            return 4;
          case 2:
            return 5;
          case 3:
            return 6;
          case 4:
            return 7;
          case 5:
            return 8;
          case 6:
            return 49;
          case 7:
            return 50;
          case 8:
            return 51;
          case 9:
            return 52;
          case 10:
            return 10;
          case 11:
            return 11;
          case 12:
            return 12;
          case 13:
            return 13;
          case 14:
            return 14;
          case 15:
            return 56;
          case 16:
            return 57;
          case 33:
            return 15;
          case 34:
            return 16;
          case 35:
            return 17;
          case 36:
            return 18;
          case 37:
            return 19;
          case 65:
            return 20;
          case 66:
            return 21;
          case 67:
            return 22;
          case 97:
            return 23;
          case 98:
            return 24;
          case 99:
            return 25;
          case 100:
            return 26;
          case 101:
            return 27;
          case 129:
            return 28;
          case 130:
            return 29;
          case 131:
            return 30;
          case 161:
            return 31;
          case 162:
            return 32;
          case 163:
            return 33;
          case 193:
            return 34;
          case 194:
            return 35;
          case 195:
            return 36;
          case 225:
            return 37;
          case 226:
            return 38;
          case 257:
            return 39;
          case 289:
            return 40;
          case 290:
            return 41;
          case 321:
            return 42;
          case 353:
            return 43;
          case 354:
            return 44;
          case 385:
            return 45;
          case 386:
            return 46;
          case 387:
            return 47;
          case 388:
            return 48;
          default:
            return 0;
        }
      }
    }

    public class MedalWindow : CustomSelfDrawPanel.CSDControl
    {
      private const int ach_area_x = 0;
      private const int ach_area_y = 30;
      private CustomSelfDrawPanel.CSDLabel achievementsLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDVertScrollBar scrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
      private CustomSelfDrawPanel.CSDArea scrollArea = new CustomSelfDrawPanel.CSDArea();
      private CustomSelfDrawPanel.CSDImage scrollImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDArea scrollArea2 = new CustomSelfDrawPanel.CSDArea();
      private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
      private CustomSelfDrawPanel.CSDImage ach_top_overlay = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage ach_bottom_overlay = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage ach_top_inset = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage popupOverlayImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.MedalImage medal1 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal2 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal3 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal4 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal5 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal6 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal7 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal8 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal9 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal10 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal11 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal12 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal13 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal14 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal15 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal16 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal17 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal18 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal19 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal20 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal21 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal22 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal23 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal24 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal25 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal26 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal27 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal28 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal29 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal30 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal31 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal32 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal33 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal34 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal35 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal36 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal37 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal38 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal39 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal40 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal41 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal42 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal43 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal44 = new CustomSelfDrawPanel.MedalImage();
      private CustomSelfDrawPanel.MedalImage medal45 = new CustomSelfDrawPanel.MedalImage();
      public bool ownPlayer = true;
      private string fb_title = "";
      private string fb_caption = "";
      private int fb_achievement;
      private static int[] activeAchievements = new int[43]
      {
        1,
        2,
        5,
        11,
        12,
        13,
        14,
        34,
        37,
        163,
        226,
        257,
        289,
        4,
        6,
        7,
        8,
        9,
        15,
        16,
        10,
        3,
        65,
        66,
        67,
        129,
        131,
        130,
        290,
        162,
        161,
        354,
        353,
        101,
        100,
        388,
        386,
        387,
        385,
        225,
        194,
        195,
        321
      };
      private CustomSelfDrawPanel.MedalWindow _childWindow;
      public static CustomSelfDrawPanel.MedalWindow.AchievementComparer achievementComparer = new CustomSelfDrawPanel.MedalWindow.AchievementComparer();

      public void init(
        List<int> earnedAchievements,
        bool addUnearned,
        bool popupOverlay,
        int heightDiff)
      {
        this.ownPlayer = !popupOverlay;
        this.clearControls();
        this.Size = new Size(475, 350);
        this.scrollArea.Position = new Point(7, 30);
        this.scrollArea.Size = new Size(475, 2000);
        this.scrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(475, 305 + heightDiff));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.scrollArea);
        this.scrollImage.Image = (Image) GFXLibrary.achievement_woodback_middletile;
        this.scrollImage.Size = this.scrollArea.Size;
        this.scrollImage.Tile = true;
        this.scrollImage.Position = new Point(0, 0);
        this.scrollArea.addControl((CustomSelfDrawPanel.CSDControl) this.scrollImage);
        this.scrollArea2.Position = new Point(0, 0);
        this.scrollArea2.Size = this.scrollImage.Size;
        this.scrollImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollArea2);
        this.mouseWheelOverlay.Position = this.scrollArea.Position;
        this.mouseWheelOverlay.Size = this.scrollArea.Size;
        this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
        this.addControl(this.mouseWheelOverlay);
        this.scrollBar.Position = new Point(436, 55);
        this.scrollBar.Size = new Size(32, 275 + heightDiff);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.scrollBar);
        this.scrollBar.Value = 0;
        this.scrollBar.Max = 920 - (305 + heightDiff) + 20;
        this.scrollBar.NumVisibleLines = 305 + heightDiff;
        this.scrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary.scroll_thumb_top, (Image) GFXLibrary.scroll_thumb_mid, (Image) GFXLibrary.scroll_thumb_bottom);
        this.scrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.scrollBarMoved));
        this.ach_top_overlay.Image = (Image) GFXLibrary.panel_cover_top;
        this.ach_top_overlay.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.ach_top_overlay);
        this.ach_bottom_overlay.Image = (Image) GFXLibrary.panel_cover_bottom;
        this.ach_bottom_overlay.Position = new Point(0, 30 + (305 + heightDiff) - 45);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.ach_bottom_overlay);
        if (popupOverlay)
        {
          this.popupOverlayImage.Image = (Image) GFXLibrary.char_achievementOverlay;
          this.popupOverlayImage.Position = new Point(0, 0);
          this.addControl((CustomSelfDrawPanel.CSDControl) this.popupOverlayImage);
        }
        List<int> intList = CustomSelfDrawPanel.MedalWindow.processAchievements(earnedAchievements, addUnearned);
        int x = 25;
        int y = 10;
        int num1 = 80;
        int num2 = 115;
        if (intList.Count > 0)
        {
          this.medal1.init(intList[0], this);
          this.medal1.Position = new Point(x, y);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal1);
        }
        if (intList.Count > 1)
        {
          this.medal2.init(intList[1], this);
          this.medal2.Position = new Point(x + num1, y);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal2);
        }
        if (intList.Count > 2)
        {
          this.medal3.init(intList[2], this);
          this.medal3.Position = new Point(x + num1 * 2, y);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal3);
        }
        if (intList.Count > 3)
        {
          this.medal4.init(intList[3], this);
          this.medal4.Position = new Point(x + num1 * 3, y);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal4);
        }
        if (intList.Count > 4)
        {
          this.medal5.init(intList[4], this);
          this.medal5.Position = new Point(x + num1 * 4, y);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal5);
        }
        if (intList.Count > 5)
        {
          this.medal6.init(intList[5], this);
          this.medal6.Position = new Point(x, y + num2);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal6);
        }
        if (intList.Count > 6)
        {
          this.medal7.init(intList[6], this);
          this.medal7.Position = new Point(x + num1, y + num2);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal7);
        }
        if (intList.Count > 7)
        {
          this.medal8.init(intList[7], this);
          this.medal8.Position = new Point(x + num1 * 2, y + num2);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal8);
        }
        if (intList.Count > 8)
        {
          this.medal9.init(intList[8], this);
          this.medal9.Position = new Point(x + num1 * 3, y + num2);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal9);
        }
        if (intList.Count > 9)
        {
          this.medal10.init(intList[9], this);
          this.medal10.Position = new Point(x + num1 * 4, y + num2);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal10);
        }
        if (intList.Count > 10)
        {
          this.medal11.init(intList[10], this);
          this.medal11.Position = new Point(x, y + num2 * 2);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal11);
        }
        if (intList.Count > 11)
        {
          this.medal12.init(intList[11], this);
          this.medal12.Position = new Point(x + num1, y + num2 * 2);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal12);
        }
        if (intList.Count > 12)
        {
          this.medal13.init(intList[12], this);
          this.medal13.Position = new Point(x + num1 * 2, y + num2 * 2);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal13);
        }
        if (intList.Count > 13)
        {
          this.medal14.init(intList[13], this);
          this.medal14.Position = new Point(x + num1 * 3, y + num2 * 2);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal14);
        }
        if (intList.Count > 14)
        {
          this.medal15.init(intList[14], this);
          this.medal15.Position = new Point(x + num1 * 4, y + num2 * 2);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal15);
        }
        if (intList.Count > 15)
        {
          this.medal16.init(intList[15], this);
          this.medal16.Position = new Point(x, y + num2 * 3);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal16);
        }
        if (intList.Count > 16)
        {
          this.medal17.init(intList[16], this);
          this.medal17.Position = new Point(x + num1, y + num2 * 3);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal17);
        }
        if (intList.Count > 17)
        {
          this.medal18.init(intList[17], this);
          this.medal18.Position = new Point(x + num1 * 2, y + num2 * 3);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal18);
        }
        if (intList.Count > 18)
        {
          this.medal19.init(intList[18], this);
          this.medal19.Position = new Point(x + num1 * 3, y + num2 * 3);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal19);
        }
        if (intList.Count > 19)
        {
          this.medal20.init(intList[19], this);
          this.medal20.Position = new Point(x + num1 * 4, y + num2 * 3);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal20);
        }
        if (intList.Count > 20)
        {
          this.medal21.init(intList[20], this);
          this.medal21.Position = new Point(x, y + num2 * 4);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal21);
        }
        if (intList.Count > 21)
        {
          this.medal22.init(intList[21], this);
          this.medal22.Position = new Point(x + num1, y + num2 * 4);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal22);
        }
        if (intList.Count > 22)
        {
          this.medal23.init(intList[22], this);
          this.medal23.Position = new Point(x + num1 * 2, y + num2 * 4);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal23);
        }
        if (intList.Count > 23)
        {
          this.medal24.init(intList[23], this);
          this.medal24.Position = new Point(x + num1 * 3, y + num2 * 4);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal24);
        }
        if (intList.Count > 24)
        {
          this.medal25.init(intList[24], this);
          this.medal25.Position = new Point(x + num1 * 4, y + num2 * 4);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal25);
        }
        if (intList.Count > 25)
        {
          this.medal26.init(intList[25], this);
          this.medal26.Position = new Point(x, y + num2 * 5);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal26);
        }
        if (intList.Count > 26)
        {
          this.medal27.init(intList[26], this);
          this.medal27.Position = new Point(x + num1, y + num2 * 5);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal27);
        }
        if (intList.Count > 27)
        {
          this.medal28.init(intList[27], this);
          this.medal28.Position = new Point(x + num1 * 2, y + num2 * 5);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal28);
        }
        if (intList.Count > 28)
        {
          this.medal29.init(intList[28], this);
          this.medal29.Position = new Point(x + num1 * 3, y + num2 * 5);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal29);
        }
        if (intList.Count > 29)
        {
          this.medal30.init(intList[29], this);
          this.medal30.Position = new Point(x + num1 * 4, y + num2 * 5);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal30);
        }
        if (intList.Count > 30)
        {
          this.medal31.init(intList[30], this);
          this.medal31.Position = new Point(x, y + num2 * 6);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal31);
        }
        if (intList.Count > 31)
        {
          this.medal32.init(intList[31], this);
          this.medal32.Position = new Point(x + num1, y + num2 * 6);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal32);
        }
        if (intList.Count > 32)
        {
          this.medal33.init(intList[32], this);
          this.medal33.Position = new Point(x + num1 * 2, y + num2 * 6);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal33);
        }
        if (intList.Count > 33)
        {
          this.medal34.init(intList[33], this);
          this.medal34.Position = new Point(x + num1 * 3, y + num2 * 6);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal34);
        }
        if (intList.Count > 34)
        {
          this.medal35.init(intList[34], this);
          this.medal35.Position = new Point(x + num1 * 4, y + num2 * 6);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal35);
        }
        if (intList.Count > 35)
        {
          this.medal36.init(intList[35], this);
          this.medal36.Position = new Point(x, y + num2 * 7);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal36);
        }
        if (intList.Count > 36)
        {
          this.medal37.init(intList[36], this);
          this.medal37.Position = new Point(x + num1, y + num2 * 7);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal37);
        }
        if (intList.Count > 37)
        {
          this.medal38.init(intList[37], this);
          this.medal38.Position = new Point(x + num1 * 2, y + num2 * 7);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal38);
        }
        if (intList.Count > 38)
        {
          this.medal39.init(intList[38], this);
          this.medal39.Position = new Point(x + num1 * 3, y + num2 * 7);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal39);
        }
        if (intList.Count > 39)
        {
          this.medal40.init(intList[39], this);
          this.medal40.Position = new Point(x + num1 * 4, y + num2 * 7);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal40);
        }
        if (intList.Count > 40)
        {
          this.medal41.init(intList[40], this);
          this.medal41.Position = new Point(x, y + num2 * 8);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal41);
        }
        if (intList.Count > 41)
        {
          this.medal42.init(intList[41], this);
          this.medal42.Position = new Point(x + num1, y + num2 * 8);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal42);
        }
        if (intList.Count > 42)
        {
          this.medal43.init(intList[42], this);
          this.medal43.Position = new Point(x + num1 * 2, y + num2 * 8);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal43);
        }
        if (intList.Count > 43)
        {
          this.medal44.init(intList[43], this);
          this.medal44.Position = new Point(x + num1 * 3, y + num2 * 8);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal44);
        }
        if (intList.Count > 44)
        {
          this.medal45.init(intList[44], this);
          this.medal45.Position = new Point(x + num1 * 4, y + num2 * 8);
          this.scrollArea2.addControl((CustomSelfDrawPanel.CSDControl) this.medal45);
        }
        int num3 = (intList.Count + 4) / 5 * 115 - 305 + 20;
        if (num3 < 0)
        {
          num3 = 0;
          this.scrollBar.Visible = false;
        }
        else
          this.scrollBar.Visible = true;
        this.scrollBar.Max = num3;
        if (heightDiff == 0)
        {
          this.achievementsLabel.Text = SK.Text("GENERIC_Achievements", "Achievements");
          this.achievementsLabel.Position = new Point(0, 0);
          this.achievementsLabel.Size = new Size(this.ach_top_overlay.Width, 30);
          this.achievementsLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
          this.achievementsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          if (!popupOverlay)
          {
            this.achievementsLabel.Color = Color.FromArgb(224, 203, 146);
            this.achievementsLabel.DropShadowColor = Color.FromArgb(56, 50, 36);
            this.ach_top_overlay.addControl((CustomSelfDrawPanel.CSDControl) this.achievementsLabel);
          }
          else
          {
            this.achievementsLabel.Color = ARGBColors.White;
            this.achievementsLabel.DropShadowColor = ARGBColors.Black;
            this.popupOverlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.achievementsLabel);
          }
        }
        else
        {
          if (earnedAchievements.Count != 1)
            return;
          int earnedAchievement = earnedAchievements[0];
          int achievement = earnedAchievement & 4095;
          string achievementRank = CustomTooltipManager.getAchievementRank(earnedAchievement);
          int rankLevel;
          switch (earnedAchievement & 1879048192)
          {
            case 268435456:
              rankLevel = 1;
              break;
            case 536870912:
              rankLevel = 2;
              break;
            case 1073741824:
              rankLevel = 3;
              break;
            case 1342177280:
              rankLevel = 4;
              break;
            case 1610612736:
              rankLevel = 5;
              break;
            case 1879048192:
              rankLevel = 6;
              break;
            default:
              rankLevel = 0;
              break;
          }
          string achievementTitle = CustomTooltipManager.getAchievementTitle(achievement);
          this.fb_title = achievementTitle;
          string achievementRequirement = CustomTooltipManager.getAchievementRequirement(achievement, rankLevel);
          this.fb_caption = achievementRequirement;
          this.achievementsLabel.Text = achievementTitle + Environment.NewLine + achievementRank + Environment.NewLine + achievementRequirement;
          this.achievementsLabel.Position = new Point(105, 45);
          this.achievementsLabel.Size = new Size(350, 110);
          this.achievementsLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
          this.achievementsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
          this.achievementsLabel.Color = ARGBColors.White;
          this.ach_top_overlay.addControl((CustomSelfDrawPanel.CSDControl) this.achievementsLabel);
        }
      }

      private void scrollBarMoved()
      {
        int y = this.scrollBar.Value;
        this.scrollArea.Position = new Point(this.scrollArea.X, 30 - y);
        this.scrollArea.ClipRect = new Rectangle(this.scrollArea.ClipRect.X, y, this.scrollArea.ClipRect.Width, this.scrollArea.ClipRect.Height);
        this.scrollArea.invalidate();
      }

      private void mouseWheelMoved(int delta)
      {
        if (delta < 0)
        {
          this.scrollBar.scrollDown(6);
        }
        else
        {
          if (delta <= 0)
            return;
          this.scrollBar.scrollUp(6);
        }
      }

      public static int getAchievementRanking(int achievement)
      {
        switch (achievement & 268435455)
        {
          case 1:
            return 4;
          case 2:
            return 8;
          case 3:
            return 13;
          case 4:
            return 14;
          case 5:
            return 17;
          case 6:
            return 19;
          case 7:
            return 22;
          case 8:
            return 28;
          case 9:
            return 33;
          case 10:
            return 27;
          case 11:
            return 28;
          case 12:
            return 25;
          case 13:
            return 19;
          case 14:
            return 22;
          case 15:
            return 29;
          case 16:
            return 30;
          case 33:
            return 10;
          case 34:
            return 16;
          case 35:
            return 0;
          case 36:
            return 0;
          case 37:
            return 15;
          case 65:
            return 28;
          case 66:
            return 25;
          case 67:
            return 24;
          case 97:
            return 7;
          case 98:
            return 8;
          case 99:
            return 24;
          case 100:
            return 23;
          case 101:
            return 19;
          case 129:
            return 26;
          case 130:
            return 33;
          case 131:
            return 22;
          case 161:
            return 5;
          case 162:
            return 3;
          case 163:
            return 11;
          case 193:
            return 31;
          case 194:
            return 21;
          case 195:
            return 16;
          case 225:
            return 46;
          case 226:
            return 9;
          case 257:
            return 14;
          case 289:
            return 15;
          case 290:
            return 18;
          case 321:
            return 0;
          case 353:
            return 20;
          case 354:
            return 32;
          case 385:
            return 30;
          case 386:
            return 40;
          case 387:
            return 45;
          case 388:
            return 50;
          default:
            return 0;
        }
      }

      public static List<int> processAchievements(List<int> achievements, bool addUnEarned)
      {
        if (achievements == null)
          achievements = new List<int>();
        List<int> intList1 = new List<int>();
        foreach (int achievement in achievements)
        {
          int num1 = achievement & 268435455;
          int num2 = achievement & 1879048192;
          bool flag = false;
          for (int index = 0; index < intList1.Count; ++index)
          {
            int num3 = intList1[index];
            if ((num3 & 268435455) == num1)
            {
              int num4 = num3 & 1879048192;
              if (num2 > num4)
                intList1[index] = achievement;
              flag = true;
              break;
            }
          }
          if (!flag)
            intList1.Add(achievement);
        }
        intList1.Sort((IComparer<int>) CustomSelfDrawPanel.MedalWindow.achievementComparer);
        if (addUnEarned)
        {
          List<int> intList2 = new List<int>();
          foreach (int num5 in intList1)
          {
            int num6 = num5 & 268435455;
            intList2.Add(num6);
          }
          List<int> collection = new List<int>();
          foreach (int activeAchievement in CustomSelfDrawPanel.MedalWindow.activeAchievements)
          {
            if (!intList2.Contains(activeAchievement))
              collection.Add(activeAchievement);
          }
          if (collection.Count > 1)
          {
            collection.Sort((IComparer<int>) CustomSelfDrawPanel.MedalWindow.achievementComparer);
            collection.Reverse();
          }
          for (int index = 0; index < collection.Count; ++index)
            collection[index] = -collection[index];
          intList1.AddRange((IEnumerable<int>) collection);
        }
        return intList1;
      }

      public void setChildWindow(CustomSelfDrawPanel.MedalWindow child)
      {
        this._childWindow = child;
      }

      public void achievementClicked(int achievement)
      {
        if (this._childWindow == null)
          return;
        this._childWindow.init(new List<int>()
        {
          achievement
        }, false, false, -150);
        this._childWindow.Visible = true;
      }

      public class AchievementComparer : IComparer<int>
      {
        public int Compare(int x, int y)
        {
          int num1 = x & 1879048192;
          int num2 = y & 1879048192;
          if (num1 < num2)
            return 1;
          if (num1 > num2)
            return -1;
          int achievement1 = x & 268435455;
          int achievement2 = y & 268435455;
          int achievementRanking1 = CustomSelfDrawPanel.MedalWindow.getAchievementRanking(achievement1);
          int achievementRanking2 = CustomSelfDrawPanel.MedalWindow.getAchievementRanking(achievement2);
          if (achievementRanking1 < achievementRanking2)
            return 1;
          return achievementRanking1 > achievementRanking2 ? -1 : 0;
        }
      }
    }

    public class ResourceButton : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDButton baseButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDImage resourceImage = new CustomSelfDrawPanel.CSDImage();
      private LogoutPanel m_logoutParent;

      public void init(int resource, LogoutPanel logoutParent)
      {
        this.m_logoutParent = logoutParent;
        this.baseButton.ImageNorm = (Image) GFXLibrary.logout_bits[7];
        this.baseButton.ImageOver = (Image) GFXLibrary.logout_bits[8];
        this.baseButton.Position = new Point(0, 1);
        this.baseButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buttonClicked), "SelectTradingResourcePanel_resource");
        this.baseButton.Data = resource;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.baseButton);
        this.baseButton.CustomTooltipID = 1417;
        this.baseButton.CustomTooltipData = resource;
        this.Size = this.baseButton.Size;
        this.resourceImage.Image = (Image) GFXLibrary.getCommodity64DSImage(resource);
        this.resourceImage.Data = resource;
        this.resourceImage.Position = new Point(0, 0);
        this.resourceImage.Size = new Size(69, 69);
        this.baseButton.addControl((CustomSelfDrawPanel.CSDControl) this.resourceImage);
      }

      public void buttonClicked()
      {
        if (this.csd.ClickedControl == null)
          return;
        int data = this.csd.ClickedControl.Data;
        if (this.m_logoutParent == null)
          return;
        this.m_logoutParent.resourceSelected(data);
      }
    }

    public class MRHP_Background : CustomSelfDrawPanel.CSDControl
    {
      public const int HEADER_TYPE_NONE = 10000;
      public const int HEADER_TYPE_ATTACK = 1000;
      public const int HEADER_TYPE_MONK = 1001;
      public const int HEADER_TYPE_SCOUT = 1002;
      public const int HEADER_TYPE_REINFORCEMENT = 1003;
      public const int HEADER_TYPE_TRADE = 1004;
      public const int HEADER_TYPE_VASSAL = 1005;
      public const int HEADER_TYPE_RAT = 1006;
      public const int HEADER_TYPE_PARISH = 1500;
      public const int HEADER_TYPE_COUNTY = 1501;
      public const int HEADER_TYPE_PROVINCE = 1502;
      public const int HEADER_TYPE_COUNTRY = 1503;
      public const int HEADER_TYPE_PARISH_PLAGUE = 1504;
      public const int HEADER_TYPE_CHARTER = 1505;
      public const int HEADER_TYPE_FILTER = 1506;
      public const int HEADER_TYPE_TERRAIN_LOWLAND = 2000;
      public const int HEADER_TYPE_TERRAIN_HIGHLAND = 2001;
      public const int HEADER_TYPE_TERRAIN_MOUNTAIN_PEAK = 2002;
      public const int HEADER_TYPE_TERRAIN_RIVER1 = 2003;
      public const int HEADER_TYPE_TERRAIN_RIVER2 = 2004;
      public const int HEADER_TYPE_TERRAIN_SALT = 2005;
      public const int HEADER_TYPE_TERRAIN_MARSH = 2006;
      public const int HEADER_TYPE_TERRAIN_PLAINS = 2007;
      public const int HEADER_TYPE_TERRAIN_VALLEY = 2008;
      public const int HEADER_TYPE_TERRAIN_FOREST = 2009;
      private CustomSelfDrawPanel.CSDImage avatarUnderlayImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage backGroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage headerImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage headerIcon = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage actionIcon = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage headerGlowSmall = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage headerGlowLong = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel headingLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel subHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel panelLabel = new CustomSelfDrawPanel.CSDLabel();
      private int LastVillageType;
      private int headingVillageID = -1;

      public CustomSelfDrawPanel.CSDImage init(bool tall, int villageBackgroundType)
      {
        return this.init(tall, villageBackgroundType, "", "", "");
      }

      public CustomSelfDrawPanel.CSDImage init(
        bool tall,
        int villageBackgroundType,
        string heading,
        string subHeading,
        string panelText)
      {
        this.headingVillageID = -1;
        this.Size = new Size(199, 213);
        this.clearControls();
        this.avatarUnderlayImage.Image = (Image) GFXLibrary.mrhp_avatar_frame;
        this.avatarUnderlayImage.Position = new Point(0, 182);
        this.avatarUnderlayImage.ClipRect = new Rectangle(0, 0, 200, 31);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.avatarUnderlayImage);
        this.backGroundImage.Image = tall ? (Image) GFXLibrary.mrhp_world_panel_192 : (Image) GFXLibrary.mrhp_world_panel_102;
        this.backGroundImage.Position = new Point(6, 20);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backGroundImage);
        this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
        this.headerImage.Position = new Point(-1, -17);
        this.backGroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage);
        this.headerGlowLong.Image = (Image) GFXLibrary.mrhp_location_portrait_glow_long;
        this.headerGlowLong.Position = new Point(45, 10);
        this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerGlowLong);
        this.headerGlowSmall.Image = (Image) GFXLibrary.mrhp_location_portrait_glow_short;
        this.headerGlowSmall.Position = new Point(0, -9);
        this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerGlowSmall);
        this.headerIcon.Image = (Image) GFXLibrary.wl_moving_unit_icons[0];
        this.headerIcon.Position = new Point(17, 26);
        this.headerGlowSmall.addControl((CustomSelfDrawPanel.CSDControl) this.headerIcon);
        this.actionIcon.Image = (Image) GFXLibrary.wl_moving_unit_icons[0];
        this.actionIcon.Position = new Point(141, 17);
        this.actionIcon.Visible = false;
        this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.actionIcon);
        this.headingLabel.Text = "";
        this.headingLabel.Color = ARGBColors.Black;
        this.headingLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        this.headingLabel.Position = new Point(14, 5);
        this.headingLabel.Size = new Size(168, 23);
        this.headingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.headingLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.headingClicked), "MRHP_Background_heading");
        this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.headingLabel);
        this.subHeadingLabel.Text = "";
        this.subHeadingLabel.Color = ARGBColors.Black;
        this.subHeadingLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        this.subHeadingLabel.Position = new Point(12, 18);
        this.subHeadingLabel.Size = new Size(132, 20);
        this.subHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.headerGlowLong.addControl((CustomSelfDrawPanel.CSDControl) this.subHeadingLabel);
        this.panelLabel.Text = "";
        this.panelLabel.Color = ARGBColors.Black;
        this.panelLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        this.panelLabel.Position = new Point(0, 38);
        this.panelLabel.Size = new Size(this.backGroundImage.Width, 23);
        this.panelLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.backGroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.panelLabel);
        this.LastVillageType = -1;
        this.update(villageBackgroundType, heading, subHeading, panelText);
        return this.backGroundImage;
      }

      public void stretchBackground()
      {
        this.Size = new Size(199, 273);
        this.backGroundImage.Size = new Size(GFXLibrary.mrhp_world_panel_192.Width, GFXLibrary.mrhp_world_panel_192.Height + 60);
      }

      public void update()
      {
        if (this.avatarUnderlayImage.Visible == InterfaceMgr.Instance.isUserInfoVisible())
          return;
        this.avatarUnderlayImage.Visible = InterfaceMgr.Instance.isUserInfoVisible();
        this.invalidate();
      }

      public void showFade(bool state) => this.avatarUnderlayImage.Visible = state;

      public void updateHeading(string heading)
      {
        this.headingVillageID = -1;
        this.update(this.LastVillageType, heading, this.subHeadingLabel.Text, this.panelLabel.Text);
      }

      public void updateSubHeading(string subHeading)
      {
        this.update(this.LastVillageType, this.headingLabel.Text, subHeading, this.panelLabel.Text);
      }

      public void centerSubHeading()
      {
        this.headerGlowLong.Position = new Point((this.backGroundImage.Image.Width - this.headerGlowLong.Image.Width) / 2 - 20, 10);
        this.headerGlowLong.Size = new Size(this.headerGlowLong.Image.Width + 40, this.headerGlowLong.Height);
        this.subHeadingLabel.Size = new Size(this.headerGlowLong.Size.Width - 24, 20);
      }

      public void updatePanelText(string panelText)
      {
        this.update(this.LastVillageType, this.headingLabel.Text, this.subHeadingLabel.Text, panelText);
      }

      public void updatePanelType(int villageBackgroundType)
      {
        this.update(villageBackgroundType, this.headingLabel.Text, this.subHeadingLabel.Text, this.panelLabel.Text);
      }

      public void updatePanelTypeFromVillageID(int villageID)
      {
        this.headingVillageID = villageID;
        int villageBackgroundType;
        if (GameEngine.Instance.World.isSpecial(villageID))
        {
          villageBackgroundType = GameEngine.Instance.World.getSpecial(villageID);
          if (GameEngine.Instance.LocalWorldData.AIWorld)
          {
            switch (villageBackgroundType)
            {
              case 7:
              case 8:
              case 9:
              case 10:
              case 11:
              case 12:
              case 13:
              case 14:
                villageBackgroundType = 2000 + GameEngine.Instance.World.getVillageTerrainType(villageID);
                break;
            }
          }
        }
        else
          villageBackgroundType = !GameEngine.Instance.World.isRegionCapital(villageID) ? (!GameEngine.Instance.World.isCountyCapital(villageID) ? (!GameEngine.Instance.World.isProvinceCapital(villageID) ? (!GameEngine.Instance.World.isCountryCapital(villageID) ? (GameEngine.Instance.World.getVillageUserID(villageID) >= 0 ? 2000 + GameEngine.Instance.World.getVillageTerrainType(villageID) : 1505) : 1503) : 1502) : 1501) : 1500;
        this.updatePanelType(villageBackgroundType);
      }

      public void update(
        int villageBackgroundType,
        string heading,
        string subHeading,
        string panelText)
      {
        if (this.LastVillageType == villageBackgroundType && heading == this.headingLabel.Text && subHeading == this.subHeadingLabel.Text && this.panelLabel.Text == panelText)
          return;
        int num1 = 0;
        int num2 = 0;
        this.LastVillageType = villageBackgroundType;
        if (this.headingLabel.TextDiffOnly != heading)
        {
          int num3 = 0;
          Graphics graphics = InterfaceMgr.Instance.ParentForm.CreateGraphics();
          if (graphics.MeasureString(heading, FontManager.GetFont("Arial", 9f, FontStyle.Bold), 168).ToSize().Height > 18)
          {
            num3 = 1;
            if (graphics.MeasureString(heading, FontManager.GetFont("Arial", 8f, FontStyle.Bold), 168).ToSize().Height > 18)
            {
              num3 = 2;
              if (graphics.MeasureString(heading, FontManager.GetFont("Arial", 8f, FontStyle.Regular), 168).ToSize().Height > 18)
                num3 = 3;
            }
          }
          graphics.Dispose();
          switch (num3)
          {
            case 0:
              this.headingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
              this.headingLabel.Position = new Point(14, 5);
              this.headingLabel.Size = new Size(168, 23);
              this.headingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
              break;
            case 1:
              this.headingLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
              this.headingLabel.Position = new Point(14, 5);
              this.headingLabel.Size = new Size(168, 23);
              this.headingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
              break;
            case 2:
              this.headingLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
              this.headingLabel.Position = new Point(14, 5);
              this.headingLabel.Size = new Size(168, 23);
              this.headingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
              break;
            case 3:
              this.headingLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
              this.headingLabel.Position = new Point(18, 5);
              this.headingLabel.Size = new Size(500, 23);
              this.headingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
              break;
          }
        }
        this.headingLabel.TextDiffOnly = heading;
        this.subHeadingLabel.TextDiffOnly = subHeading;
        this.panelLabel.TextDiffOnly = panelText;
        if (subHeading.Length > 0)
          this.headerGlowLong.Visible = true;
        else
          this.headerGlowLong.Visible = false;
        this.headerIcon.Position = new Point(17, 26);
        switch (villageBackgroundType)
        {
          case 3:
          case 4:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[15];
            num1 = 2424;
            this.headerGlowSmall.Visible = false;
            break;
          case 5:
          case 6:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[16];
            num1 = 2425;
            this.headerGlowSmall.Visible = false;
            break;
          case 7:
          case 8:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[18];
            num1 = 2426;
            this.headerGlowSmall.Visible = false;
            break;
          case 9:
          case 10:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[18];
            num1 = 2427;
            this.headerGlowSmall.Visible = false;
            break;
          case 11:
          case 12:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[18];
            num1 = 2428;
            this.headerGlowSmall.Visible = false;
            break;
          case 13:
          case 14:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[18];
            num1 = 2429;
            this.headerGlowSmall.Visible = false;
            break;
          case 15:
          case 16:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[23];
            num1 = 2447;
            this.headerGlowSmall.Visible = false;
            break;
          case 17:
          case 18:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[23];
            num1 = 2448;
            this.headerGlowSmall.Visible = false;
            break;
          case 20:
          case 21:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[17];
            this.headerGlowSmall.Visible = false;
            break;
          case 30:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[29];
            this.headerGlowSmall.Visible = false;
            break;
          case 40:
          case 41:
          case 42:
          case 43:
          case 44:
          case 45:
          case 46:
          case 47:
          case 48:
          case 49:
          case 50:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[24];
            num1 = 2449;
            num2 = villageBackgroundType;
            this.headerGlowSmall.Visible = false;
            break;
          case 51:
          case 52:
          case 53:
          case 54:
          case 55:
          case 56:
          case 57:
          case 58:
          case 59:
          case 60:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[25];
            num1 = 2449;
            num2 = villageBackgroundType;
            this.headerGlowSmall.Visible = false;
            break;
          case 61:
          case 62:
          case 63:
          case 64:
          case 65:
          case 66:
          case 67:
          case 68:
          case 69:
          case 70:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[26];
            num1 = 2449;
            num2 = villageBackgroundType;
            this.headerGlowSmall.Visible = false;
            break;
          case 71:
          case 72:
          case 73:
          case 74:
          case 75:
          case 76:
          case 77:
          case 78:
          case 79:
          case 80:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[27];
            num1 = 2449;
            num2 = villageBackgroundType;
            this.headerGlowSmall.Visible = false;
            break;
          case 81:
          case 82:
          case 83:
          case 84:
          case 85:
          case 86:
          case 87:
          case 88:
          case 89:
          case 90:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[28];
            num1 = 2449;
            num2 = villageBackgroundType;
            this.headerGlowSmall.Visible = false;
            break;
          case 100:
          case 106:
          case 107:
          case 108:
          case 109:
          case 112:
          case 113:
          case 114:
          case 115:
          case 116:
          case 117:
          case 118:
          case 119:
          case 121:
          case 122:
          case 123:
          case 124:
          case 125:
          case 126:
          case 128:
          case 129:
          case 130:
          case 131:
          case 132:
          case 133:
            if (villageBackgroundType != 100)
            {
              this.headerIcon.Image = (Image) GFXLibrary.getCommodity32DSImage(villageBackgroundType - 100);
              this.headerGlowSmall.Visible = true;
            }
            else
            {
              this.headerIcon.Position = new Point(-19, -3);
              this.headerIcon.Image = HolidayPeriods.xmas(VillageMap.getCurrentServerTime()) ? (Image) GFXLibrary.scout_screen_icons[59] : (Image) GFXLibrary.scout_screen_icons[29];
              this.headerGlowSmall.Visible = true;
            }
            num2 = villageBackgroundType;
            num1 = 2430;
            break;
          case 200:
          case 201:
          case 202:
          case 203:
          case 204:
          case 205:
          case 206:
          case 207:
          case 208:
          case 209:
          case 210:
          case 211:
          case 212:
          case 213:
          case 214:
          case 215:
          case 216:
          case 217:
          case 218:
          case 219:
          case 220:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[30 + (villageBackgroundType - 200)];
            this.headerGlowSmall.Visible = false;
            break;
          case 1000:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
            this.headerIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[1];
            this.headerGlowSmall.Visible = true;
            break;
          case 1001:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
            this.headerIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[4];
            this.headerGlowSmall.Visible = true;
            break;
          case 1002:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
            this.headerIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[3];
            this.headerGlowSmall.Visible = true;
            break;
          case 1003:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
            this.headerIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[2];
            this.headerGlowSmall.Visible = true;
            break;
          case 1004:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
            this.headerIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0];
            this.headerGlowSmall.Visible = true;
            break;
          case 1005:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
            this.headerIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[5];
            this.headerGlowSmall.Visible = true;
            break;
          case 1006:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
            this.headerIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[21];
            this.headerGlowSmall.Visible = true;
            break;
          case 1500:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[11];
            this.headerGlowSmall.Visible = false;
            num1 = 2420;
            break;
          case 1501:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[12];
            this.headerGlowSmall.Visible = false;
            num1 = 2421;
            break;
          case 1502:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[13];
            this.headerGlowSmall.Visible = false;
            num1 = 2422;
            break;
          case 1503:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[14];
            this.headerGlowSmall.Visible = false;
            num1 = 2423;
            break;
          case 1504:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[20];
            this.headerGlowSmall.Visible = false;
            num1 = 2450;
            break;
          case 1505:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[21];
            this.headerGlowSmall.Visible = false;
            num1 = 2444;
            break;
          case 1506:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[22];
            this.headerGlowSmall.Visible = false;
            break;
          case 2000:
          case 2001:
          case 2002:
          case 2003:
          case 2004:
          case 2005:
          case 2006:
          case 2007:
          case 2008:
          case 2009:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[this.remapTerrainToGFX(villageBackgroundType - 2000)];
            this.headerGlowSmall.Visible = false;
            num1 = 2436;
            num2 = villageBackgroundType - 2000;
            break;
          case 10000:
            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
            this.headerGlowSmall.Visible = false;
            break;
        }
        this.headerImage.CustomTooltipID = num1;
        this.headerImage.CustomTooltipData = num2;
      }

      public void hideBackground() => this.backGroundImage.Size = new Size(1, 1);

      public void setAction(int action)
      {
        this.actionIcon.Position = new Point(141, 17);
        this.actionIcon.Visible = action != 10000;
        switch (action)
        {
          case 1000:
            this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[1];
            break;
          case 1001:
            this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[4];
            break;
          case 1002:
            this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[3];
            break;
          case 1003:
            this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[2];
            break;
          case 1004:
            this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0];
            break;
          case 1005:
            this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[5];
            break;
        }
      }

      public void setActionFromVillage(int selectedVillage, int targetVillage)
      {
        this.actionIcon.Position = new Point(141, 17);
        if (targetVillage < 0)
        {
          if (GameEngine.Instance.World.isUserVillage(selectedVillage))
          {
            this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[24];
            this.actionIcon.Visible = true;
            return;
          }
          if (GameEngine.Instance.World.isUserRelatedVillage(selectedVillage))
          {
            this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[27];
            this.actionIcon.Visible = true;
            return;
          }
        }
        else
        {
          if (GameEngine.Instance.World.isUserVillage(targetVillage))
          {
            this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[24];
            this.actionIcon.Visible = true;
            return;
          }
          if (GameEngine.Instance.World.isUserRelatedVillage(targetVillage))
          {
            this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[27];
            this.actionIcon.Visible = true;
            return;
          }
          if (GameEngine.Instance.World.isVassal(selectedVillage, targetVillage))
          {
            this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[30];
            this.actionIcon.Visible = true;
            return;
          }
          if (GameEngine.Instance.World.isVassal(targetVillage, selectedVillage))
          {
            this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[33];
            this.actionIcon.Visible = true;
            return;
          }
          int num = 0;
          VillageData villageData = GameEngine.Instance.World.getVillageData(targetVillage);
          if (villageData != null && villageData.factionID >= 0 && RemoteServices.Instance.UserFactionID >= 0)
          {
            if (villageData.factionID != RemoteServices.Instance.UserFactionID)
            {
              int house1 = GameEngine.Instance.World.getHouse(RemoteServices.Instance.UserFactionID);
              int house2 = GameEngine.Instance.World.getHouse(villageData.factionID);
              if (house1 != house2)
              {
                int yourHouseRelation = GameEngine.Instance.World.getYourHouseRelation(house2);
                if (yourHouseRelation > 0)
                  num = 1;
                else if (yourHouseRelation < 0)
                  num = -1;
              }
              if (num == 0)
              {
                int yourFactionRelation = GameEngine.Instance.World.getYourFactionRelation(villageData.factionID);
                if (yourFactionRelation > 0)
                  num = 1;
                else if (yourFactionRelation < 0)
                  num = -1;
              }
            }
            else
              num = 2;
          }
          if (num == 2)
          {
            this.actionIcon.Image = (Image) GFXLibrary.faction_relationships[1];
            this.actionIcon.Visible = true;
            this.actionIcon.Position = new Point(141, 20);
            return;
          }
          if (num == 1)
          {
            this.actionIcon.Image = (Image) GFXLibrary.faction_relationships[0];
            this.actionIcon.Visible = true;
            this.actionIcon.Position = new Point(141, 20);
            return;
          }
          if (num == -1)
          {
            this.actionIcon.Image = (Image) GFXLibrary.faction_relationships[2];
            this.actionIcon.Visible = true;
            this.actionIcon.Position = new Point(141, 20);
            return;
          }
        }
        this.actionIcon.Visible = false;
      }

      public void setTooltipData(int tooltipData)
      {
        this.headerImage.CustomTooltipData = tooltipData;
      }

      private void headingClicked()
      {
        if (this.headingVillageID < 0)
          return;
        GameEngine.Instance.World.zoomToVillage(this.headingVillageID);
      }

      public void initTravelButton(CustomSelfDrawPanel.CSDButton button)
      {
        button.ImageNorm = (Image) GFXLibrary.mrhp_travelling_buttons[0];
        button.ImageOver = (Image) GFXLibrary.mrhp_travelling_buttons[1];
        button.ImageClick = (Image) GFXLibrary.mrhp_travelling_buttons[2];
        button.Text.TextDiffOnly = "";
        button.Text.Color = ARGBColors.Black;
        button.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
        button.Text.Size = new Size(130, 52);
        button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        button.Text.Position = new Point(40, -9);
        button.ImageIcon = (Image) GFXLibrary.mrhp_village_type_miniicons[0];
        button.ImageIconPosition = new Point(10, 0);
      }

      public void updateTravelButton(CustomSelfDrawPanel.CSDButton button, string villageString)
      {
        button.Text.TextDiffOnly = villageString;
        button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[26];
        button.ImageIconPosition = new Point(-26, -33);
      }

      public void updateTravelButton(CustomSelfDrawPanel.CSDButton button, int villageID)
      {
        try
        {
          string villageNameOrType = GameEngine.Instance.World.getVillageNameOrType(villageID);
          int num = 0;
          Graphics graphics = InterfaceMgr.Instance.ParentForm.CreateGraphics();
          Size size = graphics.MeasureString(villageNameOrType, button.Text.Font, 98).ToSize();
          if (size.Height > 18)
          {
            num = 1;
            size = graphics.MeasureString(villageNameOrType, button.Text.Font, 128).ToSize();
            if (size.Height > 18)
              num = 2;
          }
          switch (num)
          {
            case 0:
              button.Text.Size = new Size(100, 52);
              button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
              button.Text.Position = new Point(40, -9);
              break;
            case 1:
              button.Text.Size = new Size(130, 52);
              button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
              button.Text.Position = new Point(40, -9);
              break;
            case 2:
              if (size.Width < 126)
                button.Text.Size = new Size(size.Width + 4, 52);
              else
                button.Text.Size = new Size(130, 52);
              button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
              button.Text.Position = new Point(40, -9);
              break;
          }
          graphics.Dispose();
          button.Text.TextDiffOnly = villageNameOrType;
          if (GameEngine.Instance.World.isSpecial(villageID))
          {
            int special = GameEngine.Instance.World.getSpecial(villageID);
            switch (special)
            {
              case 3:
              case 4:
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[24];
                button.ImageIconPosition = new Point(-26, -33);
                break;
              case 5:
              case 6:
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[25];
                button.ImageIconPosition = new Point(-18, -35);
                break;
              case 7:
              case 8:
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[28];
                button.ImageIconPosition = new Point(-26, -31);
                break;
              case 9:
              case 10:
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[28];
                button.ImageIconPosition = new Point(-26, -31);
                break;
              case 11:
              case 12:
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[28];
                button.ImageIconPosition = new Point(-26, -31);
                break;
              case 13:
              case 14:
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[28];
                button.ImageIconPosition = new Point(-26, -31);
                break;
              case 15:
              case 16:
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[53];
                button.ImageIconPosition = new Point(-26, -31);
                break;
              case 17:
              case 18:
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[53];
                button.ImageIconPosition = new Point(-26, -31);
                break;
              case 20:
              case 21:
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[26];
                button.ImageIconPosition = new Point(-26, -33);
                break;
              case 40:
              case 41:
              case 42:
              case 43:
              case 44:
              case 45:
              case 46:
              case 47:
              case 48:
              case 49:
              case 50:
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[54];
                button.ImageIconPosition = new Point(-26, -31);
                break;
              case 51:
              case 52:
              case 53:
              case 54:
              case 55:
              case 56:
              case 57:
              case 58:
              case 59:
              case 60:
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[55];
                button.ImageIconPosition = new Point(-26, -31);
                break;
              case 61:
              case 62:
              case 63:
              case 64:
              case 65:
              case 66:
              case 67:
              case 68:
              case 69:
              case 70:
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[56];
                button.ImageIconPosition = new Point(-26, -31);
                break;
              case 71:
              case 72:
              case 73:
              case 74:
              case 75:
              case 76:
              case 77:
              case 78:
              case 79:
              case 80:
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[57];
                button.ImageIconPosition = new Point(-26, -31);
                break;
              case 81:
              case 82:
              case 83:
              case 84:
              case 85:
              case 86:
              case 87:
              case 88:
              case 89:
              case 90:
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[58];
                button.ImageIconPosition = new Point(-26, -31);
                break;
              case 100:
                button.ImageIcon = HolidayPeriods.xmas(VillageMap.getCurrentServerTime()) ? (Image) GFXLibrary.scout_screen_icons[59] : (Image) GFXLibrary.scout_screen_icons[29];
                button.ImageIconPosition = new Point(-31, -33);
                break;
              case 106:
              case 107:
              case 108:
              case 109:
              case 112:
              case 113:
              case 114:
              case 115:
              case 116:
              case 117:
              case 118:
              case 119:
              case 121:
              case 122:
              case 123:
              case 124:
              case 125:
              case 126:
              case 133:
                button.ImageIcon = (Image) GFXLibrary.getCommodity32DSImage(special - 100);
                button.ImageIconPosition = new Point(6, -7);
                break;
              case 200:
              case 201:
              case 202:
              case 203:
              case 204:
              case 205:
              case 206:
              case 207:
              case 208:
              case 209:
              case 210:
              case 211:
              case 212:
              case 213:
              case 214:
              case 215:
              case 216:
              case 217:
              case 218:
              case 219:
              case 220:
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[65];
                button.ImageIconPosition = new Point(-26, -31);
                break;
            }
          }
          else if (GameEngine.Instance.World.isRegionCapital(villageID))
          {
            button.ImageIcon = (Image) GFXLibrary.parishwall_village_center_achievement_icons[8];
            button.ImageIconPosition = new Point(-6, -16);
          }
          else if (GameEngine.Instance.World.isCountyCapital(villageID))
          {
            button.ImageIcon = (Image) GFXLibrary.parishwall_village_center_achievement_icons[9];
            button.ImageIconPosition = new Point(-6, -16);
          }
          else if (GameEngine.Instance.World.isProvinceCapital(villageID))
          {
            button.ImageIcon = (Image) GFXLibrary.parishwall_village_center_achievement_icons[10];
            button.ImageIconPosition = new Point(-6, -16);
          }
          else if (GameEngine.Instance.World.isCountryCapital(villageID))
          {
            button.ImageIcon = (Image) GFXLibrary.parishwall_village_center_achievement_icons[11];
            button.ImageIconPosition = new Point(-6, -16);
          }
          else
          {
            button.ImageIcon = (Image) GFXLibrary.mrhp_village_type_miniicons[this.remapTerrainToGFX(GameEngine.Instance.World.getVillageTerrainType(villageID)) * 3];
            button.ImageIconPosition = new Point(10, 0);
          }
        }
        catch (Exception ex)
        {
        }
      }

      private int remapTerrainToGFX(int type)
      {
        switch (type)
        {
          case 2:
            return 3;
          case 3:
            return 4;
          case 4:
            return 2;
          default:
            return type;
        }
      }

      public CustomSelfDrawPanel.WikiLinkControl addWikiLink(int id)
      {
        return CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.headerImage, id, new Point(150, 21));
      }

      public void removeWikiLink(CustomSelfDrawPanel.WikiLinkControl wikiLink)
      {
        if (wikiLink == null)
          return;
        this.headerImage.removeControl((CustomSelfDrawPanel.CSDControl) wikiLink);
      }
    }
  }
}
