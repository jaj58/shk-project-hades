// Decompiled with JetBrains decompiler
// Type: Kingdoms.ConfirmPlayCardPopup
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
  public class ConfirmPlayCardPopup : Form
  {
    private bool closing;
    private IContainer components;
    private ConfirmPlayCardPanel confirmPanel;

    public ConfirmPlayCardPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(
      CardTypes.CardDefinition def,
      ConfirmPlayCardPanel.CardClickPlayDelegate callback)
    {
      this.confirmPanel.init(def, callback);
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ConfirmPlayCardPopup));
      this.confirmPanel = new ConfirmPlayCardPanel();
      this.SuspendLayout();
      Size size = new Size(400, 400);
      this.confirmPanel.Location = new Point(0, 0);
      this.confirmPanel.Name = "confirmPanel";
      this.confirmPanel.Size = size;
      this.confirmPanel.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = size;
      this.ControlBox = false;
      this.Controls.Add((Control) this.confirmPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ConfirmPlayCardPopup);
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (ConfirmPlayCardPopup);
      this.FormClosing += new FormClosingEventHandler(this.ConfirmPlayCardPopup_FormClosing);
      this.ResumeLayout(false);
    }
  }
}
