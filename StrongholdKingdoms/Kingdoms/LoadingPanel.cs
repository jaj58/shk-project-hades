// Decompiled with JetBrains decompiler
// Type: Kingdoms.LoadingPanel
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
  public class LoadingPanel : Form
  {
    private IContainer components;
    private Panel panel1;

    public LoadingPanel()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init()
    {
      this.Text = SK.Text("LoadingPanel_Loading", "Loading Stronghold Kingdoms");
    }

    private void LoadingPanel_Load(object sender, EventArgs e)
    {
      Graphics graphics = this.CreateGraphics();
      FontManager.setDPI(graphics);
      graphics.Dispose();
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
      this.SuspendLayout();
      this.panel1.BackgroundImage = (Image) Resources.splash_screen;
      this.panel1.Location = new Point(1, 1);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(454, 212);
      this.panel1.TabIndex = 15;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Black;
      this.ClientSize = new Size(456, 214);
      this.ControlBox = false;
      this.Controls.Add((Control) this.panel1);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.Name = nameof (LoadingPanel);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Loading Stronghold Kingdoms";
      this.Load += new EventHandler(this.LoadingPanel_Load);
      this.ResumeLayout(false);
    }
  }
}
