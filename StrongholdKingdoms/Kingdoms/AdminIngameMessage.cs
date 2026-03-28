// Decompiled with JetBrains decompiler
// Type: Kingdoms.AdminIngameMessage
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using Kingdoms.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class AdminIngameMessage : MyFormBase
  {
    private IContainer components;
    private TextBox tbMaintenanceMessage;
    private BitmapButton btnSend;
    private BitmapButton btnCancel;
    private TextBox tbDuration;
    private Label label1;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AdminIngameMessage));
      this.tbMaintenanceMessage = new TextBox();
      this.btnSend = new BitmapButton();
      this.btnCancel = new BitmapButton();
      this.tbDuration = new TextBox();
      this.label1 = new Label();
      this.SuspendLayout();
      this.tbMaintenanceMessage.Location = new Point(12, 23);
      this.tbMaintenanceMessage.Multiline = true;
      this.tbMaintenanceMessage.Name = "tbMaintenanceMessage";
      this.tbMaintenanceMessage.ScrollBars = ScrollBars.Vertical;
      this.tbMaintenanceMessage.Size = new Size(450, 321);
      this.tbMaintenanceMessage.TabIndex = 0;
      this.btnSend.Location = new Point(387, 360);
      this.btnSend.Name = "btnSend";
      this.btnSend.Size = new Size(75, 23);
      this.btnSend.TabIndex = 1;
      this.btnSend.Text = "Send";
      this.btnSend.UseVisualStyleBackColor = true;
      this.btnSend.Click += new EventHandler(this.btnSend_Click);
      this.btnCancel.Location = new Point(306, 360);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.tbDuration.Location = new Point(12, 360);
      this.tbDuration.Name = "tbDuration";
      this.tbDuration.Size = new Size(100, 20);
      this.tbDuration.TabIndex = 3;
      this.tbDuration.Text = "5";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(118, 365);
      this.label1.Name = "label1";
      this.label1.Size = new Size(144, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Message Duration in Minutes";
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(474, 395);
      this.ControlBox = false;
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.tbDuration);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnSend);
      this.Controls.Add((Control) this.tbMaintenanceMessage);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Icon = Resources.shk_icon;
      this.Name = nameof (AdminIngameMessage);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Admin Ingame Message";
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public AdminIngameMessage()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.Text = this.Title = "Admin Ingame Message";
    }

    private void btnSend_Click(object sender, EventArgs e)
    {
      int num = this.getInt32FromString(this.tbDuration.Text);
      if (num < 1)
        num = 1;
      RemoteServices.Instance.SetAdminMessage(this.tbMaintenanceMessage.Text, 1000 + num);
      this.Close();
    }

    public int getInt32FromString(string text) => text.Length == 0 ? 0 : Convert.ToInt32(text);

    private void btnCancel_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("AdminIngameMessage_close");
      this.Close();
    }
  }
}
