// Decompiled with JetBrains decompiler
// Type: Kingdoms.MyMessageBoxPopUp
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using Kingdoms.Properties;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MyMessageBoxPopUp : MyFormBase
  {
    private MyMessageBoxPanel panel;
    public bool closing;

    public MyMessageBoxPopUp()
    {
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.ClientSize = new Size(400, 125);
      this.panel = new MyMessageBoxPanel();
      this.panel.Size = new Size(this.Size.Width, this.Size.Height - 34);
      this.panel.Location = new Point(0, 34);
      this.Icon = Resources.shk_icon;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Controls.Add((Control) this.panel);
      this.Text = "MessageBoxPopUp";
      this.Controls.SetChildIndex((Control) this.panel, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
      this.FormClosing += new FormClosingEventHandler(this.MyMessageBoxPopUps_FormClosing);
    }

    public void init(
      string message,
      string title,
      int type,
      CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate leftButton,
      bool leaveGreaoutOpenOnClose)
    {
      this.Text = this.Title = title;
      this.panel.init(this, message, type, leftButton, leaveGreaoutOpenOnClose);
      if (!leaveGreaoutOpenOnClose)
        return;
      this.closing = true;
    }

    public void init(
      string message,
      string title,
      int type,
      CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate leftButton)
    {
      this.Text = this.Title = title;
      this.panel.init(this, message, type, leftButton, (CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
    }

    public void setCustomYesText(string yesText) => this.panel.setCustomYesText(yesText);

    public void setCustomNoText(string noText) => this.panel.setCustomNoText(noText);

    private void MyMessageBoxPopUps_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing || this.closing)
        return;
      this.closing = true;
      InterfaceMgr.Instance.closeGreyOut();
    }
  }
}
