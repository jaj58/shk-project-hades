// Decompiled with JetBrains decompiler
// Type: Kingdoms.CastleCommitPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class CastleCommitPopup : MyFormBase
  {
    private IContainer components;
    private Label label1;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.SuspendLayout();
      this.label1.BackColor = ARGBColors.Transparent;
      this.label1.Location = new Point(3, 36);
      this.label1.Name = "label1";
      this.label1.Size = new Size(268, 35);
      this.label1.TabIndex = 0;
      this.label1.Text = "Updating Castle, Please wait....";
      this.label1.TextAlign = ContentAlignment.MiddleCenter;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(274, 79);
      this.Controls.Add((Control) this.label1);
      this.Name = nameof (CastleCommitPopup);
      this.ShowClose = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Updating Castle";
      this.TopMost = true;
      this.Load += new EventHandler(this.CastleCommitPopup_Load);
      this.Controls.SetChildIndex((Control) this.label1, 0);
      this.ResumeLayout(false);
    }

    public CastleCommitPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.ShowClose = false;
    }

    private void CastleCommitPopup_Load(object sender, EventArgs e)
    {
      this.label1.Text = SK.Text("CastleCommitPopup_Updating_Castle_Please_Wait", "Updating Castle, Please wait....");
      this.Text = this.Title = SK.Text("CastleCommitPopup_Updating_Castle", "Updating Castle");
    }
  }
}
