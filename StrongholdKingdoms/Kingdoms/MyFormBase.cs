// Decompiled with JetBrains decompiler
// Type: Kingdoms.MyFormBase
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using Kingdoms.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  [ComVisible(true)]
  [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
  public class MyFormBase : Form
  {
    public const int WM_NCLBUTTONDOWN = 161;
    public const int HT_CAPTION = 2;
    private const int cGrip = 16;
    private const int cCaption = 25;
    private Color topColor = Color.FromArgb(86, 98, 106);
    private Color bottomColor = Color.FromArgb(159, 180, 193);
    public MyFormBase.MFBClose closeCallback;
    private bool inSizeChanged;
    private bool rightResize;
    private Point RESIZESTART;
    private Size resizeOrig;
    private int lastWidth = -1;
    private bool resizable;
    private IContainer components;
    private MFBTitlePanel panel2;
    private Label lblTitle;
    private Label label3;
    private Panel panel1;
    private Panel panel3;
    private Panel panel4;

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    public MyFormBase()
    {
      this.InitializeComponent();
      try
      {
        this.panel1.BackgroundImage = (Image) GFXLibrary.messageboxclose;
        this.panel3.BackgroundImage = (Image) GFXLibrary.message_box_maximize_normal;
        this.panel4.BackgroundImage = (Image) GFXLibrary.message_box_minimize_normal;
        this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        this.lblTitle.Font = FontManager.GetFont("Microsoft Sans Serif", 9.75f, FontStyle.Bold);
        Form parentForm = InterfaceMgr.Instance.ParentForm;
        if (parentForm != null && parentForm.WindowState != FormWindowState.Minimized)
        {
          Point location = parentForm.Location;
          Size size1 = parentForm.Size;
          Size size2 = this.Size;
          this.Location = new Point((size1.Width - size2.Width) / 2 + location.X, (size1.Height - size2.Height) / 2 + location.Y);
        }
        else
          this.StartPosition = FormStartPosition.CenterScreen;
      }
      catch (Exception ex)
      {
        UniversalDebugLog.Log("An exception occurred in myformbase constructor");
      }
    }

    public string Title
    {
      set => this.lblTitle.Text = value;
    }

    public bool ShowClose
    {
      set => this.panel1.Visible = value;
      get => this.panel1.Visible;
    }

    public bool ShowBar
    {
      set => this.panel2.Visible = value;
      get => this.panel2.Visible;
    }

    public void setGradient(Color top, Color bottom)
    {
      this.topColor = top;
      this.bottomColor = bottom;
    }

    public void showMinMax()
    {
      this.panel3.Visible = true;
      this.panel4.Visible = true;
    }

    private void MyFormBase_Paint(object sender, PaintEventArgs e)
    {
      Graphics graphics = e.Graphics;
      Pen pen = new Pen(Color.FromArgb(86, 98, 106), 1f);
      Rectangle rect = new Rectangle(1, 1, this.Width - 1 - 2, this.Height - 1 - 2);
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, this.topColor, this.bottomColor, LinearGradientMode.Vertical);
      graphics.FillRectangle((Brush) linearGradientBrush, rect);
      graphics.DrawRectangle(pen, rect);
      if (this.resizable)
      {
        Rectangle bounds = new Rectangle(this.ClientSize.Width - 16, this.ClientSize.Height - 16, 16, 16);
        ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, bounds);
      }
      linearGradientBrush.Dispose();
      pen.Dispose();
    }

    private void panel2_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      MyFormBase.ReleaseCapture();
      MyFormBase.SendMessage(this.Handle, 161, 2, 0);
    }

    private void lblTitle_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      MyFormBase.ReleaseCapture();
      MyFormBase.SendMessage(this.Handle, 161, 2, 0);
    }

    private void panel1_MouseClick(object sender, MouseEventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("MyFormBase_close");
      if (this.closeCallback != null)
        this.closeCallback();
      this.Close();
    }

    private void panel1_MouseEnter(object sender, EventArgs e)
    {
      this.panel1.BackgroundImage = (Image) GFXLibrary.messageboxclose_over;
    }

    private void panel1_MouseLeave(object sender, EventArgs e)
    {
      this.panel1.BackgroundImage = (Image) GFXLibrary.messageboxclose;
    }

    private void panel2_SizeChanged(object sender, EventArgs e)
    {
      if (this.inSizeChanged || this.rightResize)
        return;
      this.inSizeChanged = true;
      this.panel2.Width = this.Width - 2;
      this.panel2.init(this.panel2.Width);
      if (this.WindowState != FormWindowState.Minimized)
      {
        this.panel1.Location = new Point(this.panel2.Width - 28, this.panel1.Location.Y);
        this.panel3.Location = new Point(this.panel2.Width - 28 - 24, this.panel3.Location.Y);
        this.panel4.Location = new Point(this.panel2.Width - 28 - 48, this.panel4.Location.Y);
      }
      this.inSizeChanged = false;
    }

    private void panel4_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("MyFormBase_minimized");
      this.WindowState = FormWindowState.Minimized;
    }

    private void panel3_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("MyFormBase_maximized");
      if (this.WindowState != FormWindowState.Maximized)
        this.WindowState = FormWindowState.Maximized;
      else
        this.WindowState = FormWindowState.Normal;
    }

    private void ucRightResize_MouseDown(object sender, MouseEventArgs e)
    {
      if (this.rightResize)
        return;
      this.rightResize = true;
      this.resizeOrig = this.Size;
      this.lastWidth = -1;
      this.RESIZESTART = this.PointToScreen(new Point(e.X, e.Y));
    }

    private void ucRightResize_MouseUp(object sender, MouseEventArgs e)
    {
      this.rightResize = false;
      this.Cursor = Cursors.Default;
    }

    private void ucRightResize_MouseMove(object sender, MouseEventArgs e)
    {
      Point screen = this.PointToScreen(new Point(e.X, e.Y));
      if (this.rightResize)
      {
        int width = this.resizeOrig.Width + (screen.X - this.RESIZESTART.X);
        if (width != this.lastWidth)
        {
          this.Size = new Size(width, this.Height);
          this.lastWidth = width;
          this.Invalidate();
          this.Title = width.ToString();
        }
      }
      if (!this.rightResize)
        return;
      this.Cursor = Cursors.SizeWE;
    }

    private void ucRightResize_MouseEnter(object sender, EventArgs e)
    {
      if (this.rightResize)
        return;
      this.Cursor = Cursors.SizeWE;
    }

    private void ucRightResize_MouseLeave(object sender, EventArgs e)
    {
      if (this.rightResize)
        return;
      this.Cursor = Cursors.Default;
    }

    private void panel3_MouseEnter(object sender, EventArgs e)
    {
      this.panel3.BackgroundImage = (Image) GFXLibrary.message_box_maximize_over;
    }

    private void panel3_MouseLeave(object sender, EventArgs e)
    {
      this.panel3.BackgroundImage = (Image) GFXLibrary.message_box_maximize_normal;
    }

    private void panel4_MouseLeave(object sender, EventArgs e)
    {
      this.panel4.BackgroundImage = (Image) GFXLibrary.message_box_minimize_normal;
    }

    private void panel4_MouseEnter(object sender, EventArgs e)
    {
      this.panel4.BackgroundImage = (Image) GFXLibrary.message_box_minimize_over;
    }

    public bool Resizable
    {
      get => this.resizable;
      set => this.resizable = value;
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 132 && this.resizable)
      {
        Point client = this.PointToClient(new Point(m.LParam.ToInt32() & (int) ushort.MaxValue, m.LParam.ToInt32() >> 16));
        if (client.X >= this.ClientSize.Width - 16 && client.Y >= this.ClientSize.Height - 16)
        {
          m.Result = (IntPtr) 17;
          return;
        }
        if (client.X >= this.ClientSize.Width - 4)
        {
          m.Result = (IntPtr) 11;
          return;
        }
        if (client.Y >= this.ClientSize.Height - 4)
        {
          m.Result = (IntPtr) 15;
          return;
        }
      }
      base.WndProc(ref m);
    }

    private void MyFormBase_SizeChanged(object sender, EventArgs e)
    {
      if (!this.resizable)
        return;
      this.Invalidate();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.panel1 = new Panel();
      this.panel3 = new Panel();
      this.panel4 = new Panel();
      this.panel2 = new MFBTitlePanel();
      this.label3 = new Label();
      this.lblTitle = new Label();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.panel1.BackColor = ARGBColors.Black;
      this.panel1.Location = new Point(331, 9);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(18, 18);
      this.panel1.TabIndex = 10;
      this.panel1.Visible = false;
      this.panel1.MouseLeave += new EventHandler(this.panel1_MouseLeave);
      this.panel1.MouseClick += new MouseEventHandler(this.panel1_MouseClick);
      this.panel1.MouseEnter += new EventHandler(this.panel1_MouseEnter);
      this.panel3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.panel3.BackColor = ARGBColors.Black;
      this.panel3.Location = new Point(307, 9);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(18, 18);
      this.panel3.TabIndex = 11;
      this.panel3.Visible = false;
      this.panel3.MouseLeave += new EventHandler(this.panel3_MouseLeave);
      this.panel3.Click += new EventHandler(this.panel3_Click);
      this.panel3.MouseEnter += new EventHandler(this.panel3_MouseEnter);
      this.panel4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.panel4.BackColor = ARGBColors.Black;
      this.panel4.Location = new Point(283, 9);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(18, 18);
      this.panel4.TabIndex = 11;
      this.panel4.Visible = false;
      this.panel4.MouseLeave += new EventHandler(this.panel4_MouseLeave);
      this.panel4.Click += new EventHandler(this.panel4_Click);
      this.panel4.MouseEnter += new EventHandler(this.panel4_MouseEnter);
      this.panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.panel2.ClickThru = false;
      this.panel2.Controls.Add((Control) this.label3);
      this.panel2.Controls.Add((Control) this.lblTitle);
      this.panel2.Location = new Point(1, 1);
      this.panel2.Name = "panel2";
      this.panel2.PanelActive = true;
      this.panel2.Size = new Size(359, 30);
      this.panel2.StoredGraphics = (Graphics) null;
      this.panel2.TabIndex = 12;
      this.panel2.MouseDown += new MouseEventHandler(this.panel2_MouseDown);
      this.panel2.SizeChanged += new EventHandler(this.panel2_SizeChanged);
      this.label3.AutoSize = true;
      this.label3.BackColor = ARGBColors.Transparent;
      this.label3.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label3.ForeColor = ARGBColors.White;
      this.label3.Location = new Point(179, 7);
      this.label3.Name = "label3";
      this.label3.Size = new Size(0, 16);
      this.label3.TabIndex = 9;
      this.lblTitle.AutoSize = true;
      this.lblTitle.BackColor = ARGBColors.Transparent;
      this.lblTitle.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblTitle.ForeColor = ARGBColors.White;
      this.lblTitle.Location = new Point(10, 8);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new Size(33, 16);
      this.lblTitle.TabIndex = 8;
      this.lblTitle.Text = "title";
      this.lblTitle.MouseDown += new MouseEventHandler(this.lblTitle_MouseDown);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(159, 180, 193);
      this.ClientSize = new Size(361, 123);
      this.ControlBox = false;
      this.Controls.Add((Control) this.panel4);
      this.Controls.Add((Control) this.panel3);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.panel2);
      this.FormBorderStyle = FormBorderStyle.None;
      this.ShowInTaskbar = false;
      this.Icon = Resources.shk_icon;
      this.Name = nameof (MyFormBase);
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Rename Village";
      this.Paint += new PaintEventHandler(this.MyFormBase_Paint);
      this.SizeChanged += new EventHandler(this.MyFormBase_SizeChanged);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.ResumeLayout(false);
    }

    public delegate void MFBClose();
  }
}
