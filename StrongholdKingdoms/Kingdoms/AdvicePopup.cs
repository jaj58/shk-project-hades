// Decompiled with JetBrains decompiler
// Type: Kingdoms.AdvicePopup
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
  public class AdvicePopup : Form
  {
    private IContainer components;
    private AdvicePanel customPanel;

    public AdvicePopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(Form parent, int screenID)
    {
      if (parent != null)
        this.Location = new Point(parent.Location.X + (parent.Width - this.Width) / 2, parent.Location.Y + (parent.Height - this.Height) / 2);
      this.customPanel.init((Form) this, screenID);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.customPanel = new AdvicePanel();
      this.SuspendLayout();
      this.customPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.customPanel.ClickThru = false;
      this.customPanel.Location = new Point(0, 0);
      this.customPanel.Name = "FirstTimePanel";
      this.customPanel.PanelActive = true;
      this.customPanel.Size = new Size(600, 300);
      this.customPanel.StoredGraphics = (Graphics) null;
      this.customPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(600, 300);
      this.Controls.Add((Control) this.customPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.MaximumSize = new Size(600, 300);
      this.MinimumSize = new Size(600, 300);
      this.Name = "AdviceWindow";
      this.Opacity = 0.9;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.ResumeLayout(false);
    }
  }
}
