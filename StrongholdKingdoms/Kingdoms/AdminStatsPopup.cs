// Decompiled with JetBrains decompiler
// Type: Kingdoms.AdminStatsPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Kingdoms.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class AdminStatsPopup : MyFormBase
  {
    private IContainer components;
    private BitmapButton btnClose;
    private Label label1;
    private Label lblNumUsersLoggedIn;
    private Label lblLast7;
    private Label label3;
    private Label lblLast3;
    private Label label5;
    private Label lblLast24;
    private Label label7;
    private Label lblNumActiveUsers;
    private Label label4;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.btnClose = new BitmapButton();
      this.label1 = new Label();
      this.lblNumUsersLoggedIn = new Label();
      this.lblLast7 = new Label();
      this.label3 = new Label();
      this.lblLast3 = new Label();
      this.label5 = new Label();
      this.lblLast24 = new Label();
      this.label7 = new Label();
      this.lblNumActiveUsers = new Label();
      this.label4 = new Label();
      this.SuspendLayout();
      this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnClose.BorderColor = ARGBColors.DarkBlue;
      this.btnClose.BorderDrawing = true;
      this.btnClose.FocusRectangleEnabled = false;
      this.btnClose.Image = (Image) null;
      this.btnClose.ImageBorderColor = ARGBColors.Chocolate;
      this.btnClose.ImageBorderEnabled = true;
      this.btnClose.ImageDropShadow = true;
      this.btnClose.ImageFocused = (Image) null;
      this.btnClose.ImageInactive = (Image) null;
      this.btnClose.ImageMouseOver = (Image) null;
      this.btnClose.ImageNormal = (Image) null;
      this.btnClose.ImagePressed = (Image) null;
      this.btnClose.InnerBorderColor = ARGBColors.LightGray;
      this.btnClose.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnClose.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnClose.Location = new Point(233, 187);
      this.btnClose.Name = "btnClose";
      this.btnClose.OffsetPressedContent = true;
      this.btnClose.Padding2 = 5;
      this.btnClose.Size = new Size(75, 23);
      this.btnClose.StretchImage = false;
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.TextDropShadow = false;
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.label1.AutoSize = true;
      this.label1.BackColor = ARGBColors.Transparent;
      this.label1.Location = new Point(21, 42);
      this.label1.Name = "label1";
      this.label1.Size = new Size(109, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Spare Feedin Villages";
      this.lblNumUsersLoggedIn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.lblNumUsersLoggedIn.BackColor = ARGBColors.Transparent;
      this.lblNumUsersLoggedIn.Location = new Point(249, 42);
      this.lblNumUsersLoggedIn.Name = "lblNumUsersLoggedIn";
      this.lblNumUsersLoggedIn.Size = new Size(43, 13);
      this.lblNumUsersLoggedIn.TabIndex = 2;
      this.lblNumUsersLoggedIn.Text = "0";
      this.lblNumUsersLoggedIn.TextAlign = ContentAlignment.TopRight;
      this.lblLast7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.lblLast7.BackColor = ARGBColors.Transparent;
      this.lblLast7.Location = new Point(249, 162);
      this.lblLast7.Name = "lblLast7";
      this.lblLast7.Size = new Size(43, 13);
      this.lblLast7.TabIndex = 4;
      this.lblLast7.Text = "0";
      this.lblLast7.TextAlign = ContentAlignment.TopRight;
      this.label3.AutoSize = true;
      this.label3.BackColor = ARGBColors.Transparent;
      this.label3.Location = new Point(21, 162);
      this.label3.Name = "label3";
      this.label3.Size = new Size(196, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "Number of Users Logged In Last 7 Days";
      this.lblLast3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.lblLast3.BackColor = ARGBColors.Transparent;
      this.lblLast3.Location = new Point(249, 132);
      this.lblLast3.Name = "lblLast3";
      this.lblLast3.Size = new Size(43, 13);
      this.lblLast3.TabIndex = 6;
      this.lblLast3.Text = "0";
      this.lblLast3.TextAlign = ContentAlignment.TopRight;
      this.label5.AutoSize = true;
      this.label5.BackColor = ARGBColors.Transparent;
      this.label5.Location = new Point(21, 132);
      this.label5.Name = "label5";
      this.label5.Size = new Size(196, 13);
      this.label5.TabIndex = 5;
      this.label5.Text = "Number of Users Logged In Last 3 Days";
      this.lblLast24.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.lblLast24.BackColor = ARGBColors.Transparent;
      this.lblLast24.Location = new Point(249, 102);
      this.lblLast24.Name = "lblLast24";
      this.lblLast24.Size = new Size(43, 13);
      this.lblLast24.TabIndex = 8;
      this.lblLast24.Text = "0";
      this.lblLast24.TextAlign = ContentAlignment.TopRight;
      this.label7.AutoSize = true;
      this.label7.BackColor = ARGBColors.Transparent;
      this.label7.Location = new Point(21, 102);
      this.label7.Name = "label7";
      this.label7.Size = new Size(206, 13);
      this.label7.TabIndex = 7;
      this.label7.Text = "Number of Users Logged In Last 24 Hours";
      this.lblNumActiveUsers.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.lblNumActiveUsers.BackColor = ARGBColors.Transparent;
      this.lblNumActiveUsers.Location = new Point(249, 72);
      this.lblNumActiveUsers.Name = "lblNumActiveUsers";
      this.lblNumActiveUsers.Size = new Size(43, 13);
      this.lblNumActiveUsers.TabIndex = 10;
      this.lblNumActiveUsers.Text = "0";
      this.lblNumActiveUsers.TextAlign = ContentAlignment.TopRight;
      this.label4.AutoSize = true;
      this.label4.BackColor = ARGBColors.Transparent;
      this.label4.Location = new Point(21, 72);
      this.label4.Name = "label4";
      this.label4.Size = new Size(163, 13);
      this.label4.TabIndex = 9;
      this.label4.Text = "Number of Users Currently Active";
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(320, 220);
      this.Controls.Add((Control) this.lblNumActiveUsers);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.lblLast24);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.lblLast3);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.lblLast7);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.lblNumUsersLoggedIn);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.btnClose);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (AdminStatsPopup);
      this.ShowClose = true;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Admin Info";
      this.Controls.SetChildIndex((Control) this.btnClose, 0);
      this.Controls.SetChildIndex((Control) this.label1, 0);
      this.Controls.SetChildIndex((Control) this.lblNumUsersLoggedIn, 0);
      this.Controls.SetChildIndex((Control) this.label3, 0);
      this.Controls.SetChildIndex((Control) this.lblLast7, 0);
      this.Controls.SetChildIndex((Control) this.label5, 0);
      this.Controls.SetChildIndex((Control) this.lblLast3, 0);
      this.Controls.SetChildIndex((Control) this.label7, 0);
      this.Controls.SetChildIndex((Control) this.lblLast24, 0);
      this.Controls.SetChildIndex((Control) this.label4, 0);
      this.Controls.SetChildIndex((Control) this.lblNumActiveUsers, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public AdminStatsPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(GetAdminStats_ReturnType returnData)
    {
      NumberFormatInfo nfi = GameEngine.NFI;
      this.Text = this.Title = "Admin Info";
      this.lblNumUsersLoggedIn.Text = returnData.usersLoggedIn.ToString("N", (IFormatProvider) nfi);
      this.lblLast24.Text = returnData.usersLogged24Hours.ToString("N", (IFormatProvider) nfi);
      this.lblLast3.Text = returnData.usersLogged3Days.ToString("N", (IFormatProvider) nfi);
      this.lblLast7.Text = returnData.usersLogged7Days.ToString("N", (IFormatProvider) nfi);
      this.lblNumActiveUsers.Text = returnData.usersActiveLastHour.ToString("N", (IFormatProvider) nfi);
    }

    private void btnClose_Click(object sender, EventArgs e) => this.Close();
  }
}
