// Decompiled with JetBrains decompiler
// Type: Kingdoms.ConfirmBuyOfferPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Kingdoms.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ConfirmBuyOfferPopup : Form
  {
    private bool closing;
    private IContainer components;
    private ConfirmBuyOfferPanel confirmPanel;

    public int Multiple => this.confirmPanel.Multiple;

    public ConfirmBuyOfferPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(
      UICardOffer offer,
      ConfirmBuyOfferPanel.CardClickPlayDelegate callback)
    {
      this.confirmPanel.init(offer.Offer, offer.nameLabel.Text, callback, this);
    }

    public void init(
      CardTypes.CardOffer offer,
      ConfirmBuyOfferPanel.CardClickPlayDelegate callback)
    {
      this.confirmPanel.init(offer, offer.Name, callback, this);
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
      this.confirmPanel = new ConfirmBuyOfferPanel();
      this.SuspendLayout();
      this.confirmPanel.ClickThru = false;
      this.confirmPanel.Location = new Point(0, 0);
      this.confirmPanel.Name = "confirmPanel";
      this.confirmPanel.PanelActive = true;
      this.confirmPanel.Size = new Size(400, 280);
      this.confirmPanel.StoredGraphics = (Graphics) null;
      this.confirmPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = this.confirmPanel.Size;
      this.ControlBox = false;
      this.Controls.Add((Control) this.confirmPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ConfirmBuyOfferPopup);
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "ConfirmPlayCardPopup";
      this.FormClosing += new FormClosingEventHandler(this.ConfirmPlayCardPopup_FormClosing);
      this.ResumeLayout(false);
    }
  }
}
