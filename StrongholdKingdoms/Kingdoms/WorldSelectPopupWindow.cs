// Decompiled with JetBrains decompiler
// Type: Kingdoms.WorldSelectPopupWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class WorldSelectPopupWindow : Form
  {
    private IContainer components;
    private WorldSelectPopupPanel createPopupPanel;
    private bool closing;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (WorldSelectPopupWindow));
      this.createPopupPanel = new WorldSelectPopupPanel();
      this.SuspendLayout();
      this.createPopupPanel.ClickThru = false;
      this.createPopupPanel.Font = new Font("Microsoft Sans Serif", 8.25f);
      this.createPopupPanel.Location = new Point(0, 0);
      this.createPopupPanel.Name = "createPopupPanel";
      this.createPopupPanel.NoDrawBackground = false;
      this.createPopupPanel.PanelActive = true;
      this.createPopupPanel.SelfDrawBackground = false;
      this.createPopupPanel.Size = new Size(824, 605);
      this.createPopupPanel.StoredGraphics = (Graphics) null;
      this.createPopupPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(824, 605);
      this.ControlBox = false;
      this.Controls.Add((Control) this.createPopupPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximumSize = new Size(824, 605);
      this.MinimumSize = new Size(824, 605);
      this.Name = nameof (WorldSelectPopupWindow);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "WorldSelect";
      this.FormClosing += new FormClosingEventHandler(this.CreatePopupPanel_FormClosing);
      this.ResumeLayout(false);
    }

    public WorldSelectPopupWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(int villageID, bool reset) => this.createPopupPanel.init(villageID, reset);

    public void update() => this.createPopupPanel.update();

    private void CreatePopupPanel_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing || this.closing)
        return;
      this.closing = true;
      InterfaceMgr.Instance.closeWorldSelectPopupWindow();
    }
  }
}
