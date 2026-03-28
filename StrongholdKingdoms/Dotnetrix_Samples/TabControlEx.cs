// Decompiled with JetBrains decompiler
// Type: Dotnetrix_Samples.TabControlEx
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

//#nullable disable
namespace Dotnetrix_Samples
{
  public class TabControlEx : TabControl
  {
    private const int TCN_FIRST = -550;
    private const int TCN_SELCHANGING = -552;
    private const int WM_USER = 1024;
    private const int WM_NOTIFY = 78;
    private const int WM_REFLECT = 8192;
    private System.ComponentModel.Container components;
    private Color m_Backcolor = Color.Empty;

    public TabControlEx()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent() => this.components = new System.ComponentModel.Container();

    [Description("The background color used to display text and graphics in a control.")]
    [Browsable(true)]
    public override Color BackColor
    {
      get
      {
        if (!this.m_Backcolor.Equals((object) Color.Empty))
          return this.m_Backcolor;
        return this.Parent == null ? Control.DefaultBackColor : this.Parent.BackColor;
      }
      set
      {
        if (this.m_Backcolor.Equals((object) value))
          return;
        this.m_Backcolor = value;
        this.Invalidate();
        this.OnBackColorChanged(EventArgs.Empty);
      }
    }

    public new bool ShouldSerializeBackColor() => !this.m_Backcolor.Equals((object) Color.Empty);

    public override void ResetBackColor()
    {
      this.m_Backcolor = Color.Empty;
      this.Invalidate();
    }

    protected override void OnParentBackColorChanged(EventArgs e)
    {
      base.OnParentBackColorChanged(e);
      this.Invalidate();
    }

    protected override void OnSelectedIndexChanged(EventArgs e)
    {
      base.OnSelectedIndexChanged(e);
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      e.Graphics.Clear(this.BackColor);
      Rectangle clientRectangle = this.ClientRectangle;
      if (this.TabCount <= 0)
        return;
      Rectangle bounds = this.SelectedTab.Bounds;
      StringFormat format = new StringFormat();
      format.Alignment = StringAlignment.Center;
      format.LineAlignment = StringAlignment.Center;
      bounds.Inflate(3, 3);
      SolidBrush solidBrush = new SolidBrush(this.TabPages[this.SelectedIndex].BackColor);
      Pen pen = new Pen(Color.FromArgb(207, 218, 224));
      e.Graphics.FillRectangle((Brush) solidBrush, bounds);
      ControlPaint.DrawBorder(e.Graphics, bounds, solidBrush.Color, ButtonBorderStyle.Outset);
      for (int index = 0; index <= this.TabCount - 1; ++index)
      {
        TabPage tabPage = this.TabPages[index];
        Rectangle rectangle = this.GetTabRect(index);
        ButtonBorderStyle style = ButtonBorderStyle.Solid;
        if (index == this.SelectedIndex)
          style = ButtonBorderStyle.Outset;
        solidBrush.Color = tabPage.BackColor;
        e.Graphics.FillRectangle((Brush) solidBrush, rectangle);
        solidBrush.Color = Color.FromArgb(130, 145, 155);
        ControlPaint.DrawBorder(e.Graphics, rectangle, solidBrush.Color, style);
        solidBrush.Color = tabPage.ForeColor;
        if (this.Alignment == TabAlignment.Left || this.Alignment == TabAlignment.Right)
        {
          float angle = 90f;
          if (this.Alignment == TabAlignment.Left)
            angle = 270f;
          PointF pointF = new PointF((float) (rectangle.Left + (rectangle.Width >> 1)), (float) (rectangle.Top + (rectangle.Height >> 1)));
          e.Graphics.TranslateTransform(pointF.X, pointF.Y);
          e.Graphics.RotateTransform(angle);
          rectangle = new Rectangle(-(rectangle.Height >> 1), -(rectangle.Width >> 1), rectangle.Height, rectangle.Width);
        }
        if (tabPage.Enabled)
          e.Graphics.DrawString(tabPage.Text, this.Font, (Brush) solidBrush, (RectangleF) rectangle, format);
        else
          ControlPaint.DrawStringDisabled(e.Graphics, tabPage.Text, this.Font, tabPage.BackColor, (RectangleF) rectangle, format);
        e.Graphics.ResetTransform();
        if (index != this.SelectedIndex)
          e.Graphics.DrawLine(pen, new Point(rectangle.Left, rectangle.Bottom - 1), new Point(rectangle.Right, rectangle.Bottom - 1));
      }
      pen.Dispose();
      solidBrush.Dispose();
    }

    [Description("Occurs as a tab is being changed.")]
    public event SelectedTabPageChangeEventHandler SelectedIndexChanging;

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 8270 && ((TabControlEx.NMHDR) Marshal.PtrToStructure(m.LParam, typeof (TabControlEx.NMHDR))).code == -552)
      {
        TabPage NextTab = this.TestTab(this.PointToClient(Cursor.Position));
        if (NextTab != null)
        {
          TabPageChangeEventArgs e = new TabPageChangeEventArgs(this.SelectedTab, NextTab);
          if (this.SelectedIndexChanging != null)
            this.SelectedIndexChanging((object) this, e);
          if (e.Cancel || !NextTab.Enabled)
          {
            m.Result = new IntPtr(1);
            return;
          }
        }
      }
      base.WndProc(ref m);
    }

    private TabPage TestTab(Point pt)
    {
      for (int index = 0; index <= this.TabCount - 1; ++index)
      {
        if (this.GetTabRect(index).Contains(pt.X, pt.Y))
          return this.TabPages[index];
      }
      return (TabPage) null;
    }

    private struct NMHDR
    {
      public IntPtr HWND;
      public uint idFrom;
      public int code;

      public override string ToString()
      {
        return string.Format("Hwnd: {0}, ControlID: {1}, Code: {2}", (object) this.HWND, (object) this.idFrom, (object) this.code);
      }
    }
  }
}
