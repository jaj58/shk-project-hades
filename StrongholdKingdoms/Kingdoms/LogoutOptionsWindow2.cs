// Decompiled with JetBrains decompiler
// Type: Kingdoms.LogoutOptionsWindow2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using Kingdoms.Properties;
using StatTracking;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class LogoutOptionsWindow2 : Form
  {
    public static bool closing;
    private IContainer components;
    private LogoutPanel currentPanel;

    public LogoutOptionsWindow2()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.TransparencyKey = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.BackColor = this.TransparencyKey;
    }

    public void init(bool normalLogout) => this.currentPanel.init(normalLogout, false);

    public void init(bool normalLogout, bool advertOnly)
    {
      this.currentPanel.init(normalLogout, advertOnly);
    }

    public void update() => this.currentPanel.update();

    private void Logout_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing || LogoutOptionsWindow2.closing)
        return;
      LogoutOptionsWindow2.closing = true;
      this.currentPanel.vacationModeCloseCheck();
      this.currentPanel.closePopup();
      StatTrackingClient.Instance().ActivateTrigger(26, (object) false);
      InterfaceMgr.Instance.closeLogoutWindow();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LogoutOptionsWindow2));
      this.currentPanel = new LogoutPanel();
      this.SuspendLayout();
      this.currentPanel.Location = new Point(0, 0);
      this.currentPanel.Name = "logoutPanel";
      this.currentPanel.Size = new Size(1000, 600);
      this.currentPanel.StoredGraphics = (Graphics) null;
      this.currentPanel.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(1000, 600);
      this.ControlBox = false;
      this.Controls.Add((Control) this.currentPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (LogoutOptionsWindow2);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (LogoutOptionsWindow2);
      this.FormClosing += new FormClosingEventHandler(this.Logout_FormClosing);
      this.ResumeLayout(false);
    }
  }
}
