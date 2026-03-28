// Decompiled with JetBrains decompiler
// Type: Kingdoms.ScoutPopupWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ScoutPopupWindow : Form
  {
    private bool closing;
    private IContainer components;
    private ScoutPopupPanel scoutPopupPanel;

    public ScoutPopupWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(int villageID, bool reset) => this.scoutPopupPanel.init(villageID, reset);

    public void update() => this.scoutPopupPanel.update();

    public void villageLoaded(int villageID)
    {
      this.scoutPopupPanel.onVillageLoadUpdate(villageID, true);
    }

    private void ScoutPopupPanel_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing || this.closing)
        return;
      this.closing = true;
      InterfaceMgr.Instance.closeScoutPopupWindow();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.scoutPopupPanel = new ScoutPopupPanel();
      this.SuspendLayout();
      this.scoutPopupPanel.Location = new Point(0, 0);
      this.scoutPopupPanel.Name = "scoutPopupPanel";
      this.scoutPopupPanel.PanelActive = true;
      this.scoutPopupPanel.Size = new Size(700, 482);
      this.scoutPopupPanel.StoredGraphics = (Graphics) null;
      this.scoutPopupPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(700, 482);
      this.ControlBox = false;
      this.Controls.Add((Control) this.scoutPopupPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximumSize = new Size(700, 482);
      this.MinimumSize = new Size(700, 482);
      this.Name = nameof (ScoutPopupWindow);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (ScoutPopupWindow);
      this.FormClosing += new FormClosingEventHandler(this.ScoutPopupPanel_FormClosing);
      this.ResumeLayout(false);
    }
  }
}
