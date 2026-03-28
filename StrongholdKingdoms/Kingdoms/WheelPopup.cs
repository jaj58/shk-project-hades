// Decompiled with JetBrains decompiler
// Type: Kingdoms.WheelPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using Kingdoms.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class WheelPopup : Form
  {
    private IContainer components;
    private WheelPanel wheelPanel;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (WheelPopup));
      this.wheelPanel = new WheelPanel();
      this.SuspendLayout();
      this.wheelPanel.Location = new Point(0, 0);
      this.wheelPanel.Name = "wheelPanel";
      this.wheelPanel.PanelActive = true;
      this.wheelPanel.Size = new Size(1000, 600);
      this.wheelPanel.StoredGraphics = (Graphics) null;
      this.wheelPanel.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(1000, 600);
      this.ControlBox = false;
      this.Controls.Add((Control) this.wheelPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.MaximizeBox = false;
      this.MaximumSize = new Size(1000, 600);
      this.MinimizeBox = false;
      this.MinimumSize = new Size(1000, 600);
      this.Name = nameof (WheelPopup);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Free Cards";
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.ResumeLayout(false);
    }

    public WheelPopup()
    {
      this.InitializeComponent();
      this.TransparencyKey = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.BackColor = this.TransparencyKey;
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(int wheelType) => this.wheelPanel.init(true, wheelType);

    public void update() => this.wheelPanel.update();
  }
}
