// Decompiled with JetBrains decompiler
// Type: Kingdoms.BuyVillagePopupWindow
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
  public class BuyVillagePopupWindow : Form
  {
    private IContainer components;
    private BuyVillagePopupPanel buyVillagePopupPanel;
    private bool closing;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.buyVillagePopupPanel = new BuyVillagePopupPanel();
      this.SuspendLayout();
      this.buyVillagePopupPanel.ClickThru = false;
      this.buyVillagePopupPanel.Location = new Point(0, 0);
      this.buyVillagePopupPanel.Name = "buyVillagePopupPanel";
      this.buyVillagePopupPanel.PanelActive = true;
      this.buyVillagePopupPanel.Size = new Size(700, 503);
      this.buyVillagePopupPanel.StoredGraphics = (Graphics) null;
      this.buyVillagePopupPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(700, 503);
      this.ControlBox = false;
      this.Controls.Add((Control) this.buyVillagePopupPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.MaximumSize = new Size(700, 503);
      this.MinimumSize = new Size(700, 503);
      this.Name = nameof (BuyVillagePopupWindow);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (BuyVillagePopupWindow);
      this.FormClosing += new FormClosingEventHandler(this.BuyVillagePopupPanel_FormClosing);
      this.ResumeLayout(false);
    }

    public BuyVillagePopupWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(int villageID, bool buy) => this.buyVillagePopupPanel.init(villageID, buy);

    public void update() => this.buyVillagePopupPanel.update();

    private void BuyVillagePopupPanel_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing || this.closing)
        return;
      this.closing = true;
      InterfaceMgr.Instance.closeBuyVillagePopupWindow();
    }
  }
}
