// Decompiled with JetBrains decompiler
// Type: Kingdoms.BPPopupWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class BPPopupWindow : Form
  {
    private bool closing;
    private IContainer components;
    private BPPopupPanel createPopupPanel;

    public BPPopupWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(ProfileLoginWindow parent) => this.createPopupPanel.init(parent);

    public void update() => this.createPopupPanel.update();

    public void attempt1Failed() => this.createPopupPanel.attempt1Failed();

    private void CreatePopupPanel_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing || this.closing)
        return;
      this.closing = true;
      InterfaceMgr.Instance.closeBPPopupWindow();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.createPopupPanel = new BPPopupPanel();
      this.SuspendLayout();
      this.createPopupPanel.ClickThru = false;
      this.createPopupPanel.Font = new Font("Microsoft Sans Serif", 8.25f);
      this.createPopupPanel.Location = new Point(0, 0);
      this.createPopupPanel.Name = "createPopupPanel";
      this.createPopupPanel.NoDrawBackground = false;
      this.createPopupPanel.PanelActive = true;
      this.createPopupPanel.SelfDrawBackground = false;
      this.createPopupPanel.Size = new Size(600, 200);
      this.createPopupPanel.StoredGraphics = (Graphics) null;
      this.createPopupPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(600, 200);
      this.ControlBox = false;
      this.Controls.Add((Control) this.createPopupPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximumSize = new Size(600, 200);
      this.MinimumSize = new Size(600, 200);
      this.Name = nameof (BPPopupWindow);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "ScoutPopupWindow";
      this.FormClosing += new FormClosingEventHandler(this.CreatePopupPanel_FormClosing);
      this.ResumeLayout(false);
    }
  }
}
