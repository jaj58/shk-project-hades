// Decompiled with JetBrains decompiler
// Type: Kingdoms.WheelSelectPopup
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
  public class WheelSelectPopup : Form
  {
    private IContainer components;
    private WheelSelectPanel wheelSelectPanel;

    public WheelSelectPopup()
    {
      this.InitializeComponent();
      this.TransparencyKey = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.BackColor = this.TransparencyKey;
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init() => this.wheelSelectPanel.init(true);

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.wheelSelectPanel = new WheelSelectPanel();
      this.SuspendLayout();
      this.wheelSelectPanel.ClickThru = false;
      this.wheelSelectPanel.Location = new Point(0, 0);
      this.wheelSelectPanel.Name = "wheelSelectPanel";
      this.wheelSelectPanel.NoDrawBackground = false;
      this.wheelSelectPanel.PanelActive = true;
      this.wheelSelectPanel.SelfDrawBackground = false;
      this.wheelSelectPanel.Size = new Size(700, 420);
      this.wheelSelectPanel.StoredGraphics = (Graphics) null;
      this.wheelSelectPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(700, 420);
      this.ControlBox = false;
      this.Controls.Add((Control) this.wheelSelectPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.MaximizeBox = false;
      this.MaximumSize = new Size(700, 420);
      this.MinimizeBox = false;
      this.MinimumSize = new Size(700, 420);
      this.Name = nameof (WheelSelectPopup);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Free Cards";
      this.TransparencyKey = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.ResumeLayout(false);
    }
  }
}
