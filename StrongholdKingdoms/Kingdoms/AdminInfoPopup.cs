// Decompiled with JetBrains decompiler
// Type: Kingdoms.AdminInfoPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Kingdoms.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class AdminInfoPopup : MyFormBase
  {
    private static string adminMessage = "";
    private static AdminInfoPopup lastPopup = (AdminInfoPopup) null;
    private static char[] delims = new char[3]
    {
      '\n',
      '\r',
      ' '
    };
    private IContainer components;
    private TextBox textBox1;
    private BitmapButton btnExit;
    private BitmapButton btnSend;

    public AdminInfoPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public static void setMessage(string message) => AdminInfoPopup.adminMessage = message;

    public static void showMessage()
    {
      if (AdminInfoPopup.adminMessage.StartsWith("http://") || AdminInfoPopup.adminMessage.StartsWith("https://"))
      {
        string[] strArray = AdminInfoPopup.adminMessage.Split(AdminInfoPopup.delims);
        if (strArray.Length > 0)
        {
          VideoWindow.ShowVideo(strArray[0], false);
          return;
        }
      }
      AdminInfoPopup adminInfoPopup = new AdminInfoPopup();
      adminInfoPopup.btnSend.Visible = false;
      adminInfoPopup.textBox1.ReadOnly = true;
      adminInfoPopup.textBox1.Text = AdminInfoPopup.adminMessage;
      adminInfoPopup.init();
      adminInfoPopup.Show();
      adminInfoPopup.btnExit.Focus();
      AdminInfoPopup.lastPopup = adminInfoPopup;
      RemoteServices.Instance.ShowAdminMessage = false;
    }

    public static void showAdminEdit()
    {
      AdminInfoPopup adminInfoPopup = new AdminInfoPopup();
      adminInfoPopup.btnSend.Visible = true;
      adminInfoPopup.textBox1.ReadOnly = false;
      adminInfoPopup.textBox1.Text = AdminInfoPopup.adminMessage;
      adminInfoPopup.init();
      adminInfoPopup.Show();
      AdminInfoPopup.lastPopup = adminInfoPopup;
    }

    public void init()
    {
      this.btnExit.Text = SK.Text("Admin_Exit", "Exit");
      this.Text = this.Title = SK.Text("Admin_Message", "Admin's Message");
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("AdminInfoPopup_close");
      this.Close();
    }

    private void btnSend_Click(object sender, EventArgs e)
    {
      RemoteServices.Instance.SetAdminMessage(AdminInfoPopup.lastPopup.textBox1.Text, 0);
      AdminInfoPopup.adminMessage = AdminInfoPopup.lastPopup.textBox1.Text;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.textBox1 = new TextBox();
      this.btnExit = new BitmapButton();
      this.btnSend = new BitmapButton();
      this.SuspendLayout();
      this.textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.textBox1.BackColor = ARGBColors.White;
      this.textBox1.Location = new Point(12, 46);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ScrollBars = ScrollBars.Vertical;
      this.textBox1.Size = new Size(509, 366);
      this.textBox1.TabIndex = 0;
      this.btnExit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnExit.BorderColor = ARGBColors.DarkBlue;
      this.btnExit.BorderDrawing = true;
      this.btnExit.FocusRectangleEnabled = false;
      this.btnExit.Image = (Image) null;
      this.btnExit.ImageBorderColor = ARGBColors.Chocolate;
      this.btnExit.ImageBorderEnabled = true;
      this.btnExit.ImageDropShadow = true;
      this.btnExit.ImageFocused = (Image) null;
      this.btnExit.ImageInactive = (Image) null;
      this.btnExit.ImageMouseOver = (Image) null;
      this.btnExit.ImageNormal = (Image) null;
      this.btnExit.ImagePressed = (Image) null;
      this.btnExit.InnerBorderColor = ARGBColors.LightGray;
      this.btnExit.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnExit.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnExit.Location = new Point(425, 422);
      this.btnExit.Name = "btnExit";
      this.btnExit.OffsetPressedContent = true;
      this.btnExit.Padding2 = 5;
      this.btnExit.Size = new Size(96, 23);
      this.btnExit.StretchImage = false;
      this.btnExit.TabIndex = 1;
      this.btnExit.Text = "Exit";
      this.btnExit.TextDropShadow = false;
      this.btnExit.UseVisualStyleBackColor = true;
      this.btnExit.Click += new EventHandler(this.btnExit_Click);
      this.btnSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnSend.BorderColor = ARGBColors.DarkBlue;
      this.btnSend.BorderDrawing = true;
      this.btnSend.FocusRectangleEnabled = false;
      this.btnSend.Image = (Image) null;
      this.btnSend.ImageBorderColor = ARGBColors.Chocolate;
      this.btnSend.ImageBorderEnabled = true;
      this.btnSend.ImageDropShadow = true;
      this.btnSend.ImageFocused = (Image) null;
      this.btnSend.ImageInactive = (Image) null;
      this.btnSend.ImageMouseOver = (Image) null;
      this.btnSend.ImageNormal = (Image) null;
      this.btnSend.ImagePressed = (Image) null;
      this.btnSend.InnerBorderColor = ARGBColors.LightGray;
      this.btnSend.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnSend.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnSend.Location = new Point(12, 422);
      this.btnSend.Name = "btnSend";
      this.btnSend.OffsetPressedContent = true;
      this.btnSend.Padding2 = 5;
      this.btnSend.Size = new Size(96, 23);
      this.btnSend.StretchImage = false;
      this.btnSend.TabIndex = 2;
      this.btnSend.Text = "Send";
      this.btnSend.TextDropShadow = false;
      this.btnSend.UseVisualStyleBackColor = true;
      this.btnSend.Click += new EventHandler(this.btnSend_Click);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(533 * InterfaceMgr.UIScale, 457 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.btnSend);
      this.Controls.Add((Control) this.btnExit);
      this.Controls.Add((Control) this.textBox1);
      this.Icon = Resources.shk_icon;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (AdminInfoPopup);
      this.ShowClose = true;
      this.ShowIcon = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Admin's Message";
      this.TopMost = true;
      this.Controls.SetChildIndex((Control) this.textBox1, 0);
      this.Controls.SetChildIndex((Control) this.btnExit, 0);
      this.Controls.SetChildIndex((Control) this.btnSend, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
