// Decompiled with JetBrains decompiler
// Type: Kingdoms.FreeCardsPopup
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
  public class FreeCardsPopup : Form
  {
    private IContainer components;
    private FreeCardsPanel freeCardsPanel;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FreeCardsPopup));
      this.freeCardsPanel = new FreeCardsPanel();
      this.SuspendLayout();
      this.freeCardsPanel.Location = new Point(0, 0);
      this.freeCardsPanel.Name = "freeCardsPanel";
      this.freeCardsPanel.PanelActive = true;
      this.freeCardsPanel.Size = new Size(1000, 600);
      this.freeCardsPanel.StoredGraphics = (Graphics) null;
      this.freeCardsPanel.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(1000, 600);
      this.ControlBox = false;
      this.Controls.Add((Control) this.freeCardsPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.MaximizeBox = false;
      this.MaximumSize = new Size(1000, 600);
      this.MinimizeBox = false;
      this.MinimumSize = new Size(1000, 600);
      this.Name = nameof (FreeCardsPopup);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Free Cards";
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.ResumeLayout(false);
    }

    public FreeCardsPopup()
    {
      this.InitializeComponent();
      this.TransparencyKey = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.BackColor = this.TransparencyKey;
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init() => this.freeCardsPanel.init(true);

    public void update() => this.freeCardsPanel.update();
  }
}
