// Decompiled with JetBrains decompiler
// Type: Kingdoms.ConfirmOpenPackPopup
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
  public class ConfirmOpenPackPopup : Form
  {
    private bool closing;
    private IContainer components;
    private ConfirmOpenPackPanel confirmPanel;

    public int Multiple => this.confirmPanel.Multiple;

    public ConfirmOpenPackPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(
      UICardPack pack,
      ConfirmOpenPackPanel.CardClickPlayDelegate callback)
    {
      this.confirmPanel.init(pack, callback, this);
    }

    public void update() => this.confirmPanel.update();

    private void ConfirmPlayCardPopup_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing || this.closing)
        return;
      this.closing = true;
      InterfaceMgr.Instance.closeConfirmPlayCardPopup();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.confirmPanel = new ConfirmOpenPackPanel();
      this.SuspendLayout();
      this.confirmPanel.ClickThru = false;
      this.confirmPanel.Location = new Point(0, 0);
      this.confirmPanel.Name = "confirmPanel";
      this.confirmPanel.PanelActive = true;
      this.confirmPanel.Size = new Size(400, 280);
      this.confirmPanel.StoredGraphics = (Graphics) null;
      this.confirmPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(400, 280);
      this.ControlBox = false;
      this.Controls.Add((Control) this.confirmPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ConfirmOpenPackPopup);
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "ConfirmPlayCardPopup";
      this.FormClosing += new FormClosingEventHandler(this.ConfirmPlayCardPopup_FormClosing);
      this.ResumeLayout(false);
    }
  }
}
