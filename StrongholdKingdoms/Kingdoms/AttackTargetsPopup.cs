// Decompiled with JetBrains decompiler
// Type: Kingdoms.AttackTargetsPopup
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
  public class AttackTargetsPopup : MyFormBase
  {
    private bool closing;
    private IContainer components;
    private AttackTargetsPanel customPanel;

    public AttackTargetsPopup()
    {
      this.InitializeComponent();
      this.Title = SK.Text("Attack_Targets", "Attack Targets");
      this.customPanel.init(this);
    }

    private void closeFunction()
    {
    }

    private void AttackTargetsPoup_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing || this.closing)
        return;
      this.closing = true;
      InterfaceMgr.Instance.closeAttackTargetsPopup();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.customPanel = new AttackTargetsPanel();
      this.SuspendLayout();
      this.customPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.customPanel.ClickThru = false;
      this.customPanel.Location = new Point(0, 34);
      this.customPanel.Name = "customPanel";
      this.customPanel.PanelActive = true;
      this.customPanel.Size = this.Size;
      this.customPanel.StoredGraphics = (Graphics) null;
      this.customPanel.TabIndex = 99;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(700 * InterfaceMgr.UIScale, 450 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.customPanel);
      this.DoubleBuffered = true;
      this.FormClosing += new FormClosingEventHandler(this.AttackTargetsPoup_FormClosing);
      this.Name = nameof (AttackTargetsPopup);
      this.ShowClose = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Manage Formations";
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.Controls.SetChildIndex((Control) this.customPanel, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
