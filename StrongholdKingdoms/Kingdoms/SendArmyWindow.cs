// Decompiled with JetBrains decompiler
// Type: Kingdoms.SendArmyWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class SendArmyWindow : Form
  {
    private IContainer components;
    private SendArmyPanel sendArmyPanel;
    private bool closing;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.sendArmyPanel = new SendArmyPanel();
      this.SuspendLayout();
      this.sendArmyPanel.Location = new Point(0, 0);
      this.sendArmyPanel.Name = "sendArmyPanel";
      this.sendArmyPanel.PanelActive = true;
      this.sendArmyPanel.Size = new Size(700, 482);
      this.sendArmyPanel.StoredGraphics = (Graphics) null;
      this.sendArmyPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(700, 482);
      this.ControlBox = false;
      this.Controls.Add((Control) this.sendArmyPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximumSize = new Size(700, 482);
      this.MinimumSize = new Size(700, 482);
      this.Name = nameof (SendArmyWindow);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (SendArmyWindow);
      this.FormClosing += new FormClosingEventHandler(this.SendArmyWindow_FormClosing);
      this.ResumeLayout(false);
    }

    public SendArmyWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(
      int parentFromVillage,
      int fromVillageID,
      int toVillageID,
      string villageName,
      double distance,
      BattleHonourData honourData,
      bool gotCaptain,
      CastleMapAttackerSetupPanel parent)
    {
      this.sendArmyPanel.init(parentFromVillage, fromVillageID, toVillageID, villageName, distance, honourData, gotCaptain, parent);
    }

    public void update() => this.sendArmyPanel.update();

    public void villageLoaded(int villageID)
    {
    }

    private void SendArmyWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing || this.closing)
        return;
      this.closing = true;
      InterfaceMgr.Instance.closeLaunchAttackPopup();
    }
  }
}
