// Decompiled with JetBrains decompiler
// Type: Kingdoms.ServerDowntimePopup
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
  public class ServerDowntimePopup : MyFormBase
  {
    private IContainer components;
    private BitmapButton btnClose;
    private Label lblHeader;
    private Label lblMinutes;
    private Label lblExplanation;

    public ServerDowntimePopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.Text = this.Title = SK.Text("ServerDowntime_Scheduled_DownTime", "Scheduled Server Maintenance");
      this.lblHeader.Text = SK.Text("ServerDowntime_Planned", "There is a Planned Downtime in");
      this.lblMinutes.Text = "0 " + SK.Text("ServerDowntime_Minutes", "Minutes");
      this.lblExplanation.Text = SK.Text("ServerDowntime_Explanation", "Please ensure you have logged out safely in advance of this downtime.");
      this.lblHeader.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
      this.lblMinutes.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
      this.lblExplanation.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
    }

    public void show(int minutes)
    {
      if (InterfaceMgr.Instance.ParentForm != null)
      {
        InterfaceMgr.Instance.ParentForm.TopMost = true;
        InterfaceMgr.Instance.ParentForm.Focus();
        InterfaceMgr.Instance.ParentForm.BringToFront();
        InterfaceMgr.Instance.ParentForm.Focus();
        InterfaceMgr.Instance.ParentForm.TopMost = false;
      }
      if (minutes <= 120)
        this.lblMinutes.Text = minutes.ToString() + " " + SK.Text("ServerDowntime_Minutes", "Minutes");
      else
        this.lblMinutes.Text = (minutes / 60).ToString() + " " + SK.Text("VillageMapPanel_Hours", "Hours");
      bool flag = false;
      Form activeForm = Form.ActiveForm;
      if (activeForm != null && activeForm.ProductName == this.ProductName && activeForm.WindowState == FormWindowState.Normal)
        flag = true;
      if (flag)
        this.Show((IWin32Window) activeForm);
      else
        this.Show();
      this.TopMost = false;
      this.TopMost = true;
      this.Focus();
      this.BringToFront();
      this.Focus();
      this.TopMost = false;
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("ServerDowntimePopup_close");
      GameEngine.Instance.clearDowntimePopup();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.btnClose = new BitmapButton();
      this.lblHeader = new Label();
      this.lblMinutes = new Label();
      this.lblExplanation = new Label();
      this.SuspendLayout();
      this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnClose.BackColor = Color.FromArgb(203, 215, 223);
      this.btnClose.Location = new Point(274, 188);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new Size(75, 23);
      this.btnClose.TabIndex = 13;
      this.btnClose.Text = "OK";
      this.btnClose.UseVisualStyleBackColor = false;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.lblHeader.BackColor = ARGBColors.Transparent;
      this.lblHeader.Location = new Point(33, 50);
      this.lblHeader.Name = "lblHeader";
      this.lblHeader.Size = new Size(295, 29);
      this.lblHeader.TabIndex = 14;
      this.lblHeader.Text = "There is a Planned Downtime in";
      this.lblHeader.TextAlign = ContentAlignment.TopCenter;
      this.lblMinutes.BackColor = ARGBColors.Transparent;
      this.lblMinutes.Location = new Point(33, 85);
      this.lblMinutes.Name = "lblMinutes";
      this.lblMinutes.Size = new Size(295, 29);
      this.lblMinutes.TabIndex = 15;
      this.lblMinutes.Text = "X Minutes";
      this.lblMinutes.TextAlign = ContentAlignment.TopCenter;
      this.lblExplanation.BackColor = ARGBColors.Transparent;
      this.lblExplanation.Location = new Point(33, 120);
      this.lblExplanation.Name = "lblExplanation";
      this.lblExplanation.Size = new Size(295, 53);
      this.lblExplanation.TabIndex = 16;
      this.lblExplanation.Text = "Please ensure you have logged out safely in advance of this downtime.";
      this.lblExplanation.TextAlign = ContentAlignment.TopCenter;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(361 * InterfaceMgr.UIScale, 223 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.lblExplanation);
      this.Controls.Add((Control) this.lblMinutes);
      this.Controls.Add((Control) this.lblHeader);
      this.Controls.Add((Control) this.btnClose);
      this.Name = nameof (ServerDowntimePopup);
      this.ShowClose = true;
      this.Text = nameof (ServerDowntimePopup);
      this.Controls.SetChildIndex((Control) this.btnClose, 0);
      this.Controls.SetChildIndex((Control) this.lblHeader, 0);
      this.Controls.SetChildIndex((Control) this.lblMinutes, 0);
      this.Controls.SetChildIndex((Control) this.lblExplanation, 0);
      this.ResumeLayout(false);
    }
  }
}
