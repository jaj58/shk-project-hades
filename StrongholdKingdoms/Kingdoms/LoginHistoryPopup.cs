// Decompiled with JetBrains decompiler
// Type: Kingdoms.LoginHistoryPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Kingdoms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class LoginHistoryPopup : Form
  {
    private List<LoginHistoryInfo> loginHistory;
    private IContainer components;
    private Button btnClose;
    private Panel pnlList;
    private Label label1;
    private Label label2;
    private Label label3;

    public LoginHistoryPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.loginHistory = GameEngine.Instance.World.getLoginHistory(true);
      this.updateList();
    }

    public void update()
    {
      if (this.loginHistory != null)
        return;
      this.loginHistory = GameEngine.Instance.World.getLoginHistory(false);
      this.updateList();
    }

    public void updateList()
    {
      if (this.loginHistory == null)
        return;
      this.pnlList.Controls.Clear();
      int y = 0;
      foreach (LoginHistoryInfo loginHistoryInfo in this.loginHistory)
      {
        LoginHistoryPanelLine historyPanelLine = new LoginHistoryPanelLine();
        historyPanelLine.Location = new Point(0, y);
        historyPanelLine.init(loginHistoryInfo.ipAddress, loginHistoryInfo.LastLogin, loginHistoryInfo.duration);
        this.pnlList.Controls.Add((Control) historyPanelLine);
        y += historyPanelLine.Height;
      }
      this.pnlList.ResumeLayout(false);
      this.pnlList.PerformLayout();
    }

    private void btnClose_Click(object sender, EventArgs e) => this.Close();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.btnClose = new Button();
      this.pnlList = new Panel();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.SuspendLayout();
      this.btnClose.Location = new Point(368, 352);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new Size(75, 23);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.pnlList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnlList.AutoScroll = true;
      this.pnlList.BackColor = ARGBColors.White;
      this.pnlList.Location = new Point(12, 37);
      this.pnlList.Name = "pnlList";
      this.pnlList.Size = new Size(431, 300);
      this.pnlList.TabIndex = 1;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(12, 21);
      this.label1.Name = "label1";
      this.label1.Size = new Size(58, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "IP Address";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(121, 21);
      this.label2.Name = "label2";
      this.label2.Size = new Size(59, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Login Time";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(365, 21);
      this.label3.Name = "label3";
      this.label3.Size = new Size(47, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Duration";
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(455, 387);
      this.ControlBox = false;
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.pnlList);
      this.Controls.Add((Control) this.btnClose);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (LoginHistoryPopup);
      this.Text = "Login History";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
