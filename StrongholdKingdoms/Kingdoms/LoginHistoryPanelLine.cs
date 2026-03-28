// Decompiled with JetBrains decompiler
// Type: Kingdoms.LoginHistoryPanelLine
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class LoginHistoryPanelLine : UserControl
  {
    private IContainer components;
    private Label lblIP;
    private Label lblLoginTime;
    private Label lblDuration;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lblIP = new Label();
      this.lblLoginTime = new Label();
      this.lblDuration = new Label();
      this.SuspendLayout();
      this.lblIP.Location = new Point(3, 0);
      this.lblIP.Name = "lblIP";
      this.lblIP.Size = new Size(104, 15);
      this.lblIP.TabIndex = 0;
      this.lblIP.Text = "255.255.255.255";
      this.lblLoginTime.Location = new Point(113, 0);
      this.lblLoginTime.Name = "lblLoginTime";
      this.lblLoginTime.Size = new Size(197, 15);
      this.lblLoginTime.TabIndex = 1;
      this.lblLoginTime.Text = "label1";
      this.lblDuration.Location = new Point(316, 0);
      this.lblDuration.Name = "lblDuration";
      this.lblDuration.Size = new Size(91, 15);
      this.lblDuration.TabIndex = 2;
      this.lblDuration.Text = "label1";
      this.lblDuration.TextAlign = ContentAlignment.TopRight;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.White;
      this.Controls.Add((Control) this.lblDuration);
      this.Controls.Add((Control) this.lblLoginTime);
      this.Controls.Add((Control) this.lblIP);
      this.Name = nameof (LoginHistoryPanelLine);
      this.Size = new Size(410, 18);
      this.ResumeLayout(false);
    }

    public LoginHistoryPanelLine() => this.InitializeComponent();

    public void init(string ipAddr, DateTime lastTime, TimeSpan duration)
    {
      this.lblIP.Text = ipAddr;
      this.lblLoginTime.Text = lastTime.ToShortTimeString() + "  -  " + lastTime.ToLongDateString();
      if (duration != TimeSpan.MinValue)
        this.lblDuration.Text = VillageMap.createBuildTimeString((int) duration.TotalSeconds);
      else
        this.lblDuration.Text = "";
    }
  }
}
