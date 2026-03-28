// Decompiled with JetBrains decompiler
// Type: Kingdoms.ConnectionErrorWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ConnectionErrorWindow : MyFormBase
  {
    private static ConnectionErrorWindow popup;
    private DateTime lastRetry = DateTime.MinValue;
    private DateTime startTime = DateTime.MinValue;
    private IContainer components;
    private BitmapButton btnLogout;
    private Label lblMessage;

    public ConnectionErrorWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblMessage.Font = FontManager.GetFont("Microsoft Sans Serif", 9f, FontStyle.Bold);
      this.Text = this.Title = SK.Text("ConnectioError_Title", "Problem with Connection to Server");
      this.btnLogout.Text = SK.Text("ConnectioError_logout", "Quit to Login Screen");
      this.lblMessage.Text = SK.Text("ConnectioError_message", "Your Stronghold Kingdoms client is having problems connecting to the game servers. Trying to connect to the server again...");
      this.ShowClose = false;
    }

    public void init() => this.startTime = DateTime.Now;

    public void update()
    {
      if ((DateTime.Now - this.startTime).TotalMinutes > 10.0)
      {
        GameEngine.Instance.forceLogout();
      }
      else
      {
        if ((DateTime.Now - this.lastRetry).TotalSeconds <= 30.0)
          return;
        RemoteServices.Instance.clearQueues();
        this.lastRetry = DateTime.Now;
        RemoteServices.Instance.LeaderBoard(-3, -1, -1, DateTime.MinValue);
      }
    }

    private void btnLogout_Click(object sender, EventArgs e) => GameEngine.Instance.forceLogout();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.btnLogout = new BitmapButton();
      this.lblMessage = new Label();
      this.SuspendLayout();
      this.ClientSize = new Size(424, 149);
      this.btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnLogout.BackColor = Color.FromArgb(203, 215, 223);
      this.btnLogout.BorderColor = ARGBColors.DarkBlue;
      this.btnLogout.BorderDrawing = true;
      this.btnLogout.FocusRectangleEnabled = false;
      this.btnLogout.Image = (Image) null;
      this.btnLogout.ImageBorderColor = ARGBColors.Chocolate;
      this.btnLogout.ImageBorderEnabled = true;
      this.btnLogout.ImageDropShadow = true;
      this.btnLogout.ImageFocused = (Image) null;
      this.btnLogout.ImageInactive = (Image) null;
      this.btnLogout.ImageMouseOver = (Image) null;
      this.btnLogout.ImageNormal = (Image) null;
      this.btnLogout.ImagePressed = (Image) null;
      this.btnLogout.InnerBorderColor = ARGBColors.LightGray;
      this.btnLogout.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnLogout.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnLogout.Size = new Size(191, 26);
      this.btnLogout.Location = new Point(221, 111);
      this.btnLogout.Name = "btnLogout";
      this.btnLogout.OffsetPressedContent = true;
      this.btnLogout.Padding2 = 5;
      this.btnLogout.StretchImage = false;
      this.btnLogout.TabIndex = 20;
      this.btnLogout.Text = "Quit to Login Screen";
      this.btnLogout.TextDropShadow = false;
      this.btnLogout.UseVisualStyleBackColor = false;
      this.btnLogout.Click += new EventHandler(this.btnLogout_Click);
      this.lblMessage.BackColor = ARGBColors.Transparent;
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Location = new Point(11, 34);
      this.lblMessage.Size = new Size(401, 69);
      this.lblMessage.TabIndex = 21;
      this.lblMessage.Text = "label1";
      this.lblMessage.TextAlign = ContentAlignment.MiddleCenter;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(128, 145, 156);
      this.Controls.Add((Control) this.lblMessage);
      this.Controls.Add((Control) this.btnLogout);
      this.Name = nameof (ConnectionErrorWindow);
      this.ShowClose = true;
      this.Text = nameof (ConnectionErrorWindow);
      this.Controls.SetChildIndex((Control) this.btnLogout, 0);
      this.Controls.SetChildIndex((Control) this.lblMessage, 0);
      this.ResumeLayout(false);
    }
  }
}
