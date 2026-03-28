// Decompiled with JetBrains decompiler
// Type: Kingdoms.MyMessageBox
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Kingdoms.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MyMessageBox : Form
  {
    public const int WM_NCLBUTTONDOWN = 161;
    public const int HT_CAPTION = 2;
    private IContainer components;
    private BitmapButton btnCancel;
    private Label lblTimer;
    private BitmapButton btnOK;
    private Panel panel1;
    private Label lblMessage;
    private Label lblTitle;
    private Panel panel2;
    private static MyMessageBox newMessageBox;
    public Timer msgTimer;
    private static DialogResult result = DialogResult.OK;
    private static MessageBoxButtons buttons = MessageBoxButtons.OK;
    private static MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1;
    private static Form forcedForm = (Form) null;
    private static string customOKSound = "";
    private static string customCancelSound = "";

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MyMessageBox));
      this.btnCancel = new BitmapButton();
      this.lblTimer = new Label();
      this.btnOK = new BitmapButton();
      this.panel1 = new Panel();
      this.lblMessage = new Label();
      this.lblTitle = new Label();
      this.panel2 = new Panel();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.BackColor = Color.FromArgb(203, 215, 223);
      this.btnCancel.Location = new Point(184, 112);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(74, 25);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = false;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.lblTimer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblTimer.AutoSize = true;
      this.lblTimer.BackColor = ARGBColors.Transparent;
      this.lblTimer.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblTimer.Location = new Point(9, 120);
      this.lblTimer.Name = "lblTimer";
      this.lblTimer.Size = new Size(0, 16);
      this.lblTimer.TabIndex = 4;
      this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnOK.BackColor = Color.FromArgb(203, 215, 223);
      this.btnOK.Location = new Point(104, 112);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(74, 25);
      this.btnOK.TabIndex = 5;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = false;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.panel1.AutoScroll = true;
      this.panel1.BackColor = ARGBColors.Transparent;
      this.panel1.Controls.Add((Control) this.lblMessage);
      this.panel1.Location = new Point(13, 41);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(335, 58);
      this.panel1.TabIndex = 6;
      this.lblMessage.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblMessage.Location = new Point(0, 0);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new Size(335, 58);
      this.lblMessage.TabIndex = 0;
      this.lblMessage.Text = "Testing text";
      this.lblMessage.TextAlign = ContentAlignment.TopCenter;
      this.lblTitle.AutoSize = true;
      this.lblTitle.BackColor = ARGBColors.Transparent;
      this.lblTitle.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblTitle.ForeColor = ARGBColors.White;
      this.lblTitle.Location = new Point(10, 8);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new Size(0, 16);
      this.lblTitle.TabIndex = 8;
      this.lblTitle.MouseDown += new MouseEventHandler(this.lblTitle_MouseDown);
      this.panel2.Controls.Add((Control) this.lblTitle);
      this.panel2.Location = new Point(1, 1);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(359, 30);
      this.panel2.TabIndex = 9;
      this.panel2.MouseDown += new MouseEventHandler(this.panel2_MouseDown);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(159, 180, 193);
      this.ClientSize = new Size(361, 144);
      this.ControlBox = false;
      this.Controls.Add((Control) this.panel2);
      this.Controls.Add((Control) this.btnOK);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.lblTimer);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.Name = nameof (MyMessageBox);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (MyMessageBox);
      this.Load += new EventHandler(this.MyMessageBox_Load);
      this.Paint += new PaintEventHandler(this.MyMessageBox_Paint);
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    public static void setCustomSounds(string newOK, string newCancel)
    {
      MyMessageBox.customOKSound = newOK;
      MyMessageBox.customCancelSound = newCancel;
    }

    public static void resetCustomSounds()
    {
      MyMessageBox.customCancelSound = "";
      MyMessageBox.customOKSound = "";
    }

    public static void setForcedForm(Form form) => MyMessageBox.forcedForm = form;

    public MyMessageBox()
    {
      this.InitializeComponent();
      this.panel2.BackgroundImage = (Image) GFXLibrary.messageboxtop;
      this.lblTimer.Font = FontManager.GetFont("Arial", 9.75f, FontStyle.Bold);
      this.lblMessage.Font = FontManager.GetFont("Arial", 9.75f, FontStyle.Regular);
      this.lblTitle.Font = FontManager.GetFont("Arial", 9.75f, FontStyle.Bold);
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
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

    public static DialogResult Show(string txtMessage)
    {
      MyMessageBox.newMessageBox = new MyMessageBox();
      MyMessageBox.buttons = MessageBoxButtons.OK;
      MyMessageBox.defaultButton = MessageBoxDefaultButton.Button1;
      MyMessageBox.newMessageBox.lblMessage.Text = txtMessage;
      Graphics graphics = MyMessageBox.newMessageBox.lblMessage.CreateGraphics();
      Size size = graphics.MeasureString(txtMessage, MyMessageBox.newMessageBox.lblMessage.Font, 335).ToSize();
      graphics.Dispose();
      int height = size.Height;
      if (height < 50)
        height = 50;
      MyMessageBox.newMessageBox.lblMessage.Size = new Size(335, height);
      MyMessageBox.newMessageBox.panel1.Size = MyMessageBox.newMessageBox.lblMessage.Size;
      MyMessageBox.newMessageBox.Size = new Size(MyMessageBox.newMessageBox.Size.Width, height + 142 - 58);
      bool flag = false;
      Form owner = Form.ActiveForm;
      if (MyMessageBox.forcedForm != null)
      {
        owner = MyMessageBox.forcedForm;
        flag = true;
        MyMessageBox.newMessageBox.StartPosition = FormStartPosition.CenterParent;
      }
      else if (owner != null && owner.ProductName == MyMessageBox.newMessageBox.ProductName && owner.WindowState == FormWindowState.Normal)
        flag = true;
      if (flag)
      {
        int num1 = (int) MyMessageBox.newMessageBox.ShowDialog((IWin32Window) owner);
      }
      else
      {
        int num2 = (int) MyMessageBox.newMessageBox.ShowDialog();
      }
      MyMessageBox.newMessageBox.Dispose();
      MyMessageBox.forcedForm = (Form) null;
      if (owner != null && flag)
      {
        bool topMost = owner.TopMost;
        owner.TopMost = false;
        owner.TopMost = true;
        owner.Focus();
        owner.BringToFront();
        owner.Focus();
        owner.TopMost = topMost;
      }
      return MyMessageBox.result;
    }

    public static DialogResult Show(string txtMessage, string txtTitle)
    {
      MyMessageBox.newMessageBox = new MyMessageBox();
      MyMessageBox.buttons = MessageBoxButtons.OK;
      MyMessageBox.defaultButton = MessageBoxDefaultButton.Button1;
      MyMessageBox.newMessageBox.lblTitle.Text = txtTitle;
      MyMessageBox.newMessageBox.lblMessage.Text = txtMessage;
      Graphics graphics = MyMessageBox.newMessageBox.lblMessage.CreateGraphics();
      Size size = graphics.MeasureString(txtMessage, MyMessageBox.newMessageBox.lblMessage.Font, 335).ToSize();
      graphics.Dispose();
      int num1 = size.Height + 3;
      if (num1 < 50)
        num1 = 50;
      MyMessageBox.newMessageBox.lblMessage.Size = new Size(335, num1 + 20);
      MyMessageBox.newMessageBox.panel1.Size = MyMessageBox.newMessageBox.lblMessage.Size;
      MyMessageBox.newMessageBox.Size = new Size(MyMessageBox.newMessageBox.Size.Width, num1 + 142 - 58);
      bool flag = false;
      Form owner = Form.ActiveForm;
      if (MyMessageBox.forcedForm != null)
      {
        owner = MyMessageBox.forcedForm;
        flag = true;
        MyMessageBox.newMessageBox.StartPosition = FormStartPosition.CenterParent;
      }
      else if (owner != null && owner.ProductName == MyMessageBox.newMessageBox.ProductName && owner.WindowState == FormWindowState.Normal)
        flag = true;
      if (flag)
      {
        MyMessageBox.newMessageBox.StartPosition = FormStartPosition.CenterParent;
        int num2 = (int) MyMessageBox.newMessageBox.ShowDialog((IWin32Window) owner);
      }
      else
      {
        int num3 = (int) MyMessageBox.newMessageBox.ShowDialog();
      }
      MyMessageBox.newMessageBox.Dispose();
      MyMessageBox.forcedForm = (Form) null;
      if (owner != null && flag)
      {
        bool topMost = owner.TopMost;
        owner.TopMost = false;
        owner.TopMost = true;
        owner.Focus();
        owner.BringToFront();
        owner.Focus();
        owner.TopMost = topMost;
      }
      return MyMessageBox.result;
    }

    public static DialogResult Show(string txtMessage, string txtTitle, MessageBoxButtons buts)
    {
      MyMessageBox.newMessageBox = new MyMessageBox();
      MyMessageBox.buttons = buts;
      MyMessageBox.defaultButton = MessageBoxDefaultButton.Button1;
      MyMessageBox.newMessageBox.lblTitle.Text = txtTitle;
      MyMessageBox.newMessageBox.lblMessage.Text = txtMessage;
      Graphics graphics = MyMessageBox.newMessageBox.lblMessage.CreateGraphics();
      Size size = graphics.MeasureString(txtMessage, MyMessageBox.newMessageBox.lblMessage.Font, 335).ToSize();
      graphics.Dispose();
      int num1 = size.Height;
      if (num1 < 50)
        num1 = 50;
      MyMessageBox.newMessageBox.lblMessage.Size = new Size(335, num1 + 20);
      MyMessageBox.newMessageBox.panel1.Size = MyMessageBox.newMessageBox.lblMessage.Size;
      MyMessageBox.newMessageBox.Size = new Size(MyMessageBox.newMessageBox.Size.Width, num1 + 142 - 58);
      bool flag = false;
      Form owner = Form.ActiveForm;
      if (MyMessageBox.forcedForm != null)
      {
        owner = MyMessageBox.forcedForm;
        flag = true;
        MyMessageBox.newMessageBox.StartPosition = FormStartPosition.CenterParent;
      }
      else if (owner != null && owner.ProductName == MyMessageBox.newMessageBox.ProductName && owner.WindowState == FormWindowState.Normal)
        flag = true;
      if (flag)
      {
        int num2 = (int) MyMessageBox.newMessageBox.ShowDialog((IWin32Window) owner);
      }
      else
      {
        int num3 = (int) MyMessageBox.newMessageBox.ShowDialog();
      }
      MyMessageBox.newMessageBox.Dispose();
      MyMessageBox.forcedForm = (Form) null;
      if (owner != null && flag)
      {
        bool topMost = owner.TopMost;
        owner.TopMost = false;
        owner.TopMost = true;
        owner.Focus();
        owner.BringToFront();
        owner.Focus();
        owner.TopMost = topMost;
      }
      return MyMessageBox.result;
    }

    public static DialogResult Show(
      string txtMessage,
      string txtTitle,
      MessageBoxButtons buts,
      MessageBoxIcon x1,
      MessageBoxDefaultButton defaultBut,
      int x2)
    {
      MyMessageBox.newMessageBox = new MyMessageBox();
      MyMessageBox.buttons = buts;
      MyMessageBox.defaultButton = defaultBut;
      MyMessageBox.newMessageBox.lblTitle.Text = txtTitle;
      MyMessageBox.newMessageBox.lblMessage.Text = txtMessage;
      Graphics graphics = MyMessageBox.newMessageBox.lblMessage.CreateGraphics();
      Size size = graphics.MeasureString(txtMessage, MyMessageBox.newMessageBox.lblMessage.Font, 335).ToSize();
      graphics.Dispose();
      int num1 = size.Height;
      if (num1 < 50)
        num1 = 50;
      MyMessageBox.newMessageBox.lblMessage.Size = new Size(335, num1 + 20);
      MyMessageBox.newMessageBox.panel1.Size = MyMessageBox.newMessageBox.lblMessage.Size;
      MyMessageBox.newMessageBox.Size = new Size(MyMessageBox.newMessageBox.Size.Width, num1 + 142 - 58);
      bool flag = false;
      Form activeForm = Form.ActiveForm;
      if (activeForm != null && activeForm.ProductName == MyMessageBox.newMessageBox.ProductName && activeForm.WindowState == FormWindowState.Normal)
        flag = true;
      if (flag)
      {
        int num2 = (int) MyMessageBox.newMessageBox.ShowDialog((IWin32Window) activeForm);
      }
      else
      {
        int num3 = (int) MyMessageBox.newMessageBox.ShowDialog();
      }
      MyMessageBox.newMessageBox.Dispose();
      if (activeForm != null && flag)
      {
        bool topMost = activeForm.TopMost;
        activeForm.TopMost = false;
        activeForm.TopMost = true;
        activeForm.Focus();
        activeForm.BringToFront();
        activeForm.Focus();
        activeForm.TopMost = topMost;
      }
      return MyMessageBox.result;
    }

    private void MyMessageBox_Load(object sender, EventArgs e)
    {
      if (MyMessageBox.buttons == MessageBoxButtons.OK)
      {
        this.btnCancel.Location = new Point(143, this.btnCancel.Location.Y);
        this.btnCancel.Text = SK.Text("GENERIC_OK", "OK");
        this.btnCancel.Visible = true;
        this.btnOK.Visible = false;
        this.btnCancel.TabIndex = 1;
        this.btnOK.TabIndex = 2;
        this.btnCancel.Focus();
      }
      if (MyMessageBox.buttons == MessageBoxButtons.YesNo)
      {
        this.btnCancel.Location = new Point(184, this.btnCancel.Location.Y);
        this.btnCancel.Text = SK.Text("GENERIC_No", "No");
        this.btnCancel.Visible = true;
        this.btnOK.Text = SK.Text("GENERIC_Yes", "Yes");
        this.btnOK.Visible = true;
        if (MyMessageBox.defaultButton == MessageBoxDefaultButton.Button1)
        {
          this.btnOK.TabIndex = 1;
          this.btnCancel.TabIndex = 2;
          this.btnOK.Focus();
        }
        else
        {
          this.btnOK.TabIndex = 2;
          this.btnCancel.TabIndex = 1;
          this.btnCancel.Focus();
        }
      }
      if (MyMessageBox.buttons != MessageBoxButtons.OKCancel)
        return;
      this.btnCancel.Location = new Point(184, this.btnCancel.Location.Y);
      this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.btnCancel.Visible = true;
      this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
      this.btnOK.Visible = true;
      if (MyMessageBox.defaultButton == MessageBoxDefaultButton.Button1)
      {
        this.btnOK.TabIndex = 1;
        this.btnCancel.TabIndex = 2;
        this.btnOK.Focus();
      }
      else
      {
        this.btnOK.TabIndex = 2;
        this.btnCancel.TabIndex = 1;
        this.btnCancel.Focus();
      }
    }

    private void MyMessageBox_Paint(object sender, PaintEventArgs e)
    {
      Graphics graphics = e.Graphics;
      Pen pen = new Pen(Color.FromArgb(86, 98, 106), 1f);
      Rectangle rect = new Rectangle(1, 1, this.Width - 1 - 2, this.Height - 1 - 2);
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(86, 98, 106), Color.FromArgb(159, 180, 193), LinearGradientMode.Vertical);
      graphics.FillRectangle((Brush) linearGradientBrush, rect);
      graphics.DrawRectangle(pen, rect);
      linearGradientBrush.Dispose();
      pen.Dispose();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (MyMessageBox.customOKSound.Length == 0)
        GameEngine.Instance.playInterfaceSound("MyMessageBox_ok");
      else
        GameEngine.Instance.playInterfaceSound(MyMessageBox.customOKSound);
      if (MyMessageBox.buttons == MessageBoxButtons.OKCancel)
        MyMessageBox.result = DialogResult.OK;
      if (MyMessageBox.buttons == MessageBoxButtons.YesNo)
        MyMessageBox.result = DialogResult.Yes;
      MyMessageBox.newMessageBox.Dispose();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      if (MyMessageBox.customCancelSound.Length == 0)
        GameEngine.Instance.playInterfaceSound("MyMessageBox_cancel");
      else
        GameEngine.Instance.playInterfaceSound(MyMessageBox.customCancelSound);
      if (MyMessageBox.buttons == MessageBoxButtons.OK)
        MyMessageBox.result = DialogResult.OK;
      if (MyMessageBox.buttons == MessageBoxButtons.OKCancel)
        MyMessageBox.result = DialogResult.Cancel;
      if (MyMessageBox.buttons == MessageBoxButtons.YesNo)
        MyMessageBox.result = DialogResult.No;
      MyMessageBox.newMessageBox.Dispose();
    }

    private void panel2_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      MyMessageBox.ReleaseCapture();
      MyMessageBox.SendMessage(this.Handle, 161, 2, 0);
    }

    private void lblTitle_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      MyMessageBox.ReleaseCapture();
      MyMessageBox.SendMessage(this.Handle, 161, 2, 0);
    }
  }
}
