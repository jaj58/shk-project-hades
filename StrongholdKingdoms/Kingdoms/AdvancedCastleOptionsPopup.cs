// Decompiled with JetBrains decompiler
// Type: Kingdoms.AdvancedCastleOptionsPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class AdvancedCastleOptionsPopup : Form
  {
    private IContainer components;
    private AdvancedCastleOptionsPanel advancedCastleOptionsPanel;

    public AdvancedCastleOptionsPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(bool castleSetup) => this.advancedCastleOptionsPanel.init(this, castleSetup);

    public void update() => this.advancedCastleOptionsPanel.update();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.advancedCastleOptionsPanel = new AdvancedCastleOptionsPanel();
      this.SuspendLayout();
      this.advancedCastleOptionsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.advancedCastleOptionsPanel.BackColor = ARGBColors.Fuchsia;
      this.advancedCastleOptionsPanel.Location = new Point(0, 0);
      this.advancedCastleOptionsPanel.Name = "advancedCastleOptionsPanel";
      this.advancedCastleOptionsPanel.Size = new Size(292, 266);
      this.advancedCastleOptionsPanel.StoredGraphics = (Graphics) null;
      this.advancedCastleOptionsPanel.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.White;
      this.ClientSize = new Size(292 * InterfaceMgr.UIScale, 266 * InterfaceMgr.UIScale);
      this.ControlBox = false;
      this.Controls.Add((Control) this.advancedCastleOptionsPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (AdvancedCastleOptionsPopup);
      this.Opacity = 0.95;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Report Capture";
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.ResumeLayout(false);
    }
  }
}
