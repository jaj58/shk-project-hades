// Decompiled with JetBrains decompiler
// Type: Kingdoms.VacationCancelPopupWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class VacationCancelPopupWindow : Form
  {
    private bool closing;
    private IContainer components;
    private VacationCancelPopupPanel createPopupPanel;

    public VacationCancelPopupWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(int secondsLeft, int secondsLeftToCancel, bool canCancel)
    {
      this.createPopupPanel.init(secondsLeft, secondsLeftToCancel, canCancel);
    }

    public void update() => this.createPopupPanel.update();

    private void CreatePopupPanel_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing || this.closing)
        return;
      this.closing = true;
      InterfaceMgr.Instance.closeVacationCancelPopupWindow();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.createPopupPanel = new VacationCancelPopupPanel();
      this.SuspendLayout();
      this.createPopupPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.createPopupPanel.ClickThru = false;
      this.createPopupPanel.Font = new Font("Microsoft Sans Serif", 8.25f);
      this.createPopupPanel.Location = new Point(0, 0);
      this.createPopupPanel.Name = "createPopupPanel";
      this.createPopupPanel.PanelActive = true;
      this.createPopupPanel.Size = new Size(615, 347);
      this.createPopupPanel.StoredGraphics = (Graphics) null;
      this.createPopupPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(615, 347);
      this.ControlBox = false;
      this.Controls.Add((Control) this.createPopupPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximumSize = new Size(615, 347);
      this.MinimumSize = new Size(615, 347);
      this.Name = nameof (VacationCancelPopupWindow);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "ScoutPopupWindow";
      this.FormClosing += new FormClosingEventHandler(this.CreatePopupPanel_FormClosing);
      this.ResumeLayout(false);
    }
  }
}
