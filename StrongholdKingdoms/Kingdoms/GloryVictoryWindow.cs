// Decompiled with JetBrains decompiler
// Type: Kingdoms.GloryVictoryWindow
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
  public class GloryVictoryWindow : Form
  {
    private IContainer components;
    private GloryVictoryPanel2 gloryVictoryPanel;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.gloryVictoryPanel = new GloryVictoryPanel2();
      this.SuspendLayout();
      this.gloryVictoryPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.gloryVictoryPanel.ClickThru = false;
      this.gloryVictoryPanel.Location = new Point(0, 0);
      this.gloryVictoryPanel.Name = "newQuestsCompletedPanel";
      this.gloryVictoryPanel.PanelActive = true;
      this.gloryVictoryPanel.Size = new Size(600, 500);
      this.gloryVictoryPanel.StoredGraphics = (Graphics) null;
      this.gloryVictoryPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(600, 500);
      this.Controls.Add((Control) this.gloryVictoryPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.MaximumSize = new Size(600, 500);
      this.MinimumSize = new Size(600, 500);
      this.Name = nameof (GloryVictoryWindow);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.ResumeLayout(false);
    }

    public GloryVictoryWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(Form parent)
    {
      if (parent != null)
        this.Location = new Point(parent.Location.X + (parent.Width - this.Width) / 2, parent.Location.Y + (parent.Height - this.Height) / 2);
      this.gloryVictoryPanel.init((Form) this);
    }

    public void initValues(Form parent)
    {
      if (parent != null)
        this.Location = new Point(parent.Location.X + (parent.Width - this.Width) / 2, parent.Location.Y + (parent.Height - this.Height) / 2);
      this.gloryVictoryPanel.initValues((Form) this);
    }
  }
}
