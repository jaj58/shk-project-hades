// Decompiled with JetBrains decompiler
// Type: Kingdoms.SendMonkWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class SendMonkWindow : Form
  {
    private IContainer components;
    private SendMonkPanel sendMonkPanel;
    private bool closing;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.sendMonkPanel = new SendMonkPanel();
      this.SuspendLayout();
      this.sendMonkPanel.Location = new Point(0, 0);
      this.sendMonkPanel.Name = "sendMonkPanel";
      this.sendMonkPanel.PanelActive = true;
      this.sendMonkPanel.Size = new Size(700, 482);
      this.sendMonkPanel.StoredGraphics = (Graphics) null;
      this.sendMonkPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(700, 482);
      this.ControlBox = false;
      this.Controls.Add((Control) this.sendMonkPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximumSize = new Size(700, 482);
      this.MinimumSize = new Size(700, 482);
      this.Name = nameof (SendMonkWindow);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (SendMonkWindow);
      this.FormClosing += new FormClosingEventHandler(this.SendMonkPanel_FormClosing);
      this.ResumeLayout(false);
    }

    public SendMonkWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(int villageID) => this.sendMonkPanel.init(villageID);

    public void update() => this.sendMonkPanel.update();

    public void villageLoaded(int villageID)
    {
      this.sendMonkPanel.onVillageLoadUpdate(villageID, true);
    }

    private void SendMonkPanel_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing || this.closing)
        return;
      this.closing = true;
      InterfaceMgr.Instance.closeSendMonkWindow();
    }
  }
}
