// Decompiled with JetBrains decompiler
// Type: Kingdoms.LostVillageWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class LostVillageWindow : Form
  {
    private int lastMode = -1;
    public bool closing;
    private IContainer components;
    private LostVillagePanel customPanel;

    public LostVillageWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public bool isCardsPopup() => this.lastMode >= 0;

    public void init(int age, int cardsMode)
    {
      if (age == 1000)
      {
        this.Width = 688;
        this.Height = 625;
        this.customPanel.Width = 688;
        this.customPanel.Height = 625;
      }
      this.lastMode = cardsMode;
      this.customPanel.init(this, age, cardsMode);
      Form parentForm = InterfaceMgr.Instance.ParentForm;
      this.Location = new Point(parentForm.Location.X + parentForm.Width / 2 - this.Width / 2, parentForm.Location.Y + parentForm.Height / 2 - this.Height / 2);
    }

    public void update() => this.customPanel.update();

    public void closePopup() => this.customPanel.closePopup();

    private void LostVillageWindow_FormClosing(object sender, FormClosingEventArgs e)
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
      this.customPanel = new LostVillagePanel();
      this.SuspendLayout();
      this.customPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.customPanel.ClickThru = false;
      this.customPanel.Location = new Point(0, 0);
      this.customPanel.Name = "customPanel";
      this.customPanel.PanelActive = true;
      this.customPanel.Size = new Size(860, 620);
      this.customPanel.StoredGraphics = (Graphics) null;
      this.customPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Fuchsia;
      this.ClientSize = new Size(860, 620);
      this.ControlBox = false;
      this.Controls.Add((Control) this.customPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MinimumSize = new Size(10, 10);
      this.Name = nameof (LostVillageWindow);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (LostVillageWindow);
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.FormClosing += new FormClosingEventHandler(this.LostVillageWindow_FormClosing);
      this.ResumeLayout(false);
    }
  }
}
