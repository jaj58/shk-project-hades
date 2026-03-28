// Decompiled with JetBrains decompiler
// Type: Kingdoms.ReasonPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Kingdoms.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ReasonPopup : MyFormBase
  {
    private UserInfoScreen m_parent;
    private AGUR m_agur;
    private IContainer components;
    private BitmapButton btnOK;
    private BitmapButton btnCancel;
    private TextBox tbReason;
    private Label label1;

    public ReasonPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(UserInfoScreen parent)
    {
      this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
      this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.label1.Text = SK.Text("ReasonPopup_Enter_Reason", "Enter Reason For this Action");
      this.Text = this.Title = SK.Text("ReasonPopup_Reason", "Reason");
      this.m_parent = parent;
    }

    public void initResources(AGUR parent, int resource)
    {
      this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
      this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.label1.Text = SK.Text("ReasonPopup_Enter_Reason", "Enter Reason For this Action");
      this.Text = this.Title = SK.Text("ReasonPopup_Reason", "Reason") + " : " + VillageBuildingsData.getResourceNames(resource);
      this.m_agur = parent;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      if (this.m_parent != null)
        this.m_parent.setReasonString("");
      if (this.m_agur != null)
        this.m_agur.setReasonString("");
      this.Close();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (this.tbReason.Text.Length <= 0)
        return;
      if (this.m_parent != null)
        this.m_parent.setReasonString(this.tbReason.Text);
      if (this.m_agur != null)
        this.m_agur.setReasonString(this.tbReason.Text);
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ReasonPopup));
      this.btnOK = new BitmapButton();
      this.btnCancel = new BitmapButton();
      this.tbReason = new TextBox();
      this.label1 = new Label();
      this.SuspendLayout();
      this.btnOK.BackColor = Color.FromArgb(203, 215, 223);
      this.btnOK.Location = new Point(272, 124);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = false;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.btnCancel.BackColor = Color.FromArgb(203, 215, 223);
      this.btnCancel.Location = new Point(20, 124);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = false;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.tbReason.BackColor = Color.FromArgb(235, 240, 243);
      this.tbReason.Location = new Point(31, 64);
      this.tbReason.Multiline = true;
      this.tbReason.Name = "tbReason";
      this.tbReason.Size = new Size(303, 46);
      this.tbReason.TabIndex = 0;
      this.label1.AutoSize = true;
      this.label1.BackColor = ARGBColors.Transparent;
      this.label1.Location = new Point(20, 40);
      this.label1.Name = "label1";
      this.label1.Size = new Size(142, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Enter Reason For this Action";
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(361, 158);
      this.ControlBox = false;
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.tbReason);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOK);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.Name = nameof (ReasonPopup);
      this.ShowIcon = false;
      this.Text = "Reason";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
