// Decompiled with JetBrains decompiler
// Type: Kingdoms.NewAutoSelectVillageWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class NewAutoSelectVillageWindow : Form
  {
    public bool closing;
    private IContainer components;
    private NewAutoSelectVillagePanel customPanel;

    public NewAutoSelectVillageWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(int countyToJoin)
    {
      this.customPanel.init(countyToJoin, this);
      Form parentForm = InterfaceMgr.Instance.ParentForm;
      this.Location = new Point(parentForm.Location.X + parentForm.Width / 2 - this.Width / 2, parentForm.Location.Y + parentForm.Height / 2 - this.Height / 2);
    }

    public void update() => this.customPanel.update();

    public void closePopup() => this.customPanel.closePopup();

    private void NewAutoSelectVillageWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing || this.closing)
        return;
      this.closing = true;
      GameEngine.Instance.closeNoVillagePopup(false);
      LoggingOutPopup.open(true, false, false, false, false, false, false, 0, 100, false, false, false, false, false, false, 500, 500, 500, 500, 250);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.customPanel = new NewAutoSelectVillagePanel();
      this.SuspendLayout();
      this.customPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.customPanel.ClickThru = false;
      this.customPanel.Location = new Point(0, 0);
      this.customPanel.Name = "customPanel";
      this.customPanel.PanelActive = true;
      this.customPanel.Size = new Size(625, 668);
      this.customPanel.StoredGraphics = (Graphics) null;
      this.customPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Fuchsia;
      this.ClientSize = new Size(625, 668);
      this.ControlBox = false;
      this.Controls.Add((Control) this.customPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MinimumSize = new Size(10, 10);
      this.Name = nameof (NewAutoSelectVillageWindow);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (NewAutoSelectVillageWindow);
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.FormClosing += new FormClosingEventHandler(this.NewAutoSelectVillageWindow_FormClosing);
      this.ResumeLayout(false);
    }
  }
}
