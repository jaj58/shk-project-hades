// Decompiled with JetBrains decompiler
// Type: Kingdoms.RenameVillagePopup
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
  public class RenameVillagePopup : MyFormBase
  {
    private int m_villageID = -1;
    private bool parishNameMode;
    private IContainer components;
    private Label label1;
    private Label label2;
    private TextBox tbOldName;
    private TextBox tbNewName;
    private BitmapButton btnOK;
    private BitmapButton btnCancel;
    private Label label3;
    private BitmapButton btnHistory;

    public RenameVillagePopup() => this.InitializeComponent();

    public void setVillageID(int villageID, string oldName)
    {
      this.parishNameMode = false;
      this.label1.Text = SK.Text("ReinforcementsRetrieval_Original_Name", "Original Name");
      this.label2.Text = SK.Text("ReinforcementsRetrieval_New_Name", "New Name");
      this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
      this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.Text = this.Title = SK.Text("ReinforcementsRetrieval_Rename_Village", "Rename Village");
      this.m_villageID = villageID;
      this.tbOldName.Text = oldName;
      this.tbNewName.Text = oldName;
      this.btnOK.Enabled = false;
    }

    public void setParishVillageID(int villageID, string oldName)
    {
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("RenameVillagePopup_cancel");
      this.Close();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("RenameVillagePopup_rename");
      if (this.tbNewName.Text.Length <= 0 || this.tbNewName.Text.Length > 32 || !StringValidation.isValidGameString(this.tbNewName.Text) || !StringValidation.notAllSpaces(this.tbNewName.Text) || !(this.tbNewName.Text != this.tbOldName.Text))
        return;
      if (this.m_villageID >= 0 && !this.parishNameMode)
      {
        RemoteServices.Instance.set_VillageRename_UserCallBack(new RemoteServices.VillageRename_UserCallBack(this.testCallback));
        RemoteServices.Instance.VillageRename(this.m_villageID, this.tbNewName.Text);
      }
      this.Close();
    }

    public void testCallback(VillageRename_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      GameEngine.Instance.World.setVillageName(returnData.villageID, returnData.renamedName);
      if (InterfaceMgr.Instance.getSelectedMenuVillage() != returnData.villageID)
        return;
      InterfaceMgr.Instance.getTopRightMenu().setSelectedVillageName(returnData.renamedName, false);
    }

    private void tbNewName_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyValue != 13)
        return;
      this.btnOK_Click(sender, (EventArgs) e);
    }

    private void tbNewName_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
      {
        this.btnOK_Click(sender, (EventArgs) e);
        e.Handled = true;
      }
      else
      {
        if (StringValidation.isValidChar(e.KeyChar))
          return;
        e.Handled = true;
      }
    }

    private void tbNewName_TextChanged(object sender, EventArgs e)
    {
      if (this.tbNewName.Text.Length > 0 && this.tbNewName.Text.Length <= 32 && StringValidation.isValidGameString(this.tbNewName.Text) && StringValidation.notAllSpaces(this.tbNewName.Text))
        this.btnOK.Enabled = true;
      else
        this.btnOK.Enabled = false;
    }

    private bool notAllSpaces(string name)
    {
      foreach (char ch in name)
      {
        if (ch != ' ')
          return true;
      }
      return false;
    }

    private void btnHistory_Click(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.label2 = new Label();
      this.tbOldName = new TextBox();
      this.tbNewName = new TextBox();
      this.btnOK = new BitmapButton();
      this.btnCancel = new BitmapButton();
      this.label3 = new Label();
      this.btnHistory = new BitmapButton();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label1.Location = new Point(16, 51);
      this.label1.Name = "label1";
      this.label1.Size = new Size(73, 13);
      this.label1.TabIndex = 10;
      this.label1.Text = "Original Name";
      this.label2.AutoSize = true;
      this.label2.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label2.Location = new Point(16, 87);
      this.label2.Name = "label2";
      this.label2.Size = new Size(60, 13);
      this.label2.TabIndex = 11;
      this.label2.Text = "New Name";
      this.tbOldName.BackColor = Color.FromArgb(159, 180, 193);
      this.tbOldName.ForeColor = ARGBColors.Black;
      this.tbOldName.Location = new Point(122, 48);
      this.tbOldName.Name = "tbOldName";
      this.tbOldName.ReadOnly = true;
      this.tbOldName.Size = new Size(144, 20);
      this.tbOldName.TabIndex = 4;
      this.tbNewName.BackColor = Color.FromArgb(235, 240, 243);
      this.tbNewName.ForeColor = ARGBColors.Black;
      this.tbNewName.Location = new Point(122, 84);
      this.tbNewName.MaxLength = 32;
      this.tbNewName.Name = "tbNewName";
      this.tbNewName.Size = new Size(144, 20);
      this.tbNewName.TabIndex = 1;
      this.tbNewName.TextChanged += new EventHandler(this.tbNewName_TextChanged);
      this.tbNewName.KeyPress += new KeyPressEventHandler(this.tbNewName_KeyPress);
      this.btnOK.BackColor = Color.FromArgb(203, 215, 223);
      this.btnOK.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnOK.BorderDrawing = true;
      this.btnOK.FocusRectangleEnabled = false;
      this.btnOK.Image = (Image) null;
      this.btnOK.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnOK.ImageBorderEnabled = true;
      this.btnOK.ImageDropShadow = true;
      this.btnOK.ImageFocused = (Image) null;
      this.btnOK.ImageInactive = (Image) null;
      this.btnOK.ImageMouseOver = (Image) null;
      this.btnOK.ImageNormal = (Image) null;
      this.btnOK.ImagePressed = (Image) null;
      this.btnOK.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnOK.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnOK.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnOK.Location = new Point(301, 48);
      this.btnOK.Name = "btnOK";
      this.btnOK.OffsetPressedContent = true;
      this.btnOK.Padding2 = 5;
      this.btnOK.Size = new Size(79, 20);
      this.btnOK.StretchImage = false;
      this.btnOK.TabIndex = 2;
      this.btnOK.Text = "OK";
      this.btnOK.TextDropShadow = false;
      this.btnOK.UseVisualStyleBackColor = false;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.btnCancel.BackColor = Color.FromArgb(203, 215, 223);
      this.btnCancel.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnCancel.BorderDrawing = true;
      this.btnCancel.FocusRectangleEnabled = false;
      this.btnCancel.Image = (Image) null;
      this.btnCancel.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnCancel.ImageBorderEnabled = true;
      this.btnCancel.ImageDropShadow = true;
      this.btnCancel.ImageFocused = (Image) null;
      this.btnCancel.ImageInactive = (Image) null;
      this.btnCancel.ImageMouseOver = (Image) null;
      this.btnCancel.ImageNormal = (Image) null;
      this.btnCancel.ImagePressed = (Image) null;
      this.btnCancel.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnCancel.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnCancel.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnCancel.Location = new Point(301, 84);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.OffsetPressedContent = true;
      this.btnCancel.Padding2 = 5;
      this.btnCancel.Size = new Size(79, 20);
      this.btnCancel.StretchImage = false;
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.TextDropShadow = false;
      this.btnCancel.UseVisualStyleBackColor = false;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.label3.AutoSize = true;
      this.label3.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label3.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label3.ForeColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label3.Location = new Point(179, 7);
      this.label3.Name = "label3";
      this.label3.Size = new Size(0, 16);
      this.label3.TabIndex = 9;
      this.btnHistory.BackColor = Color.FromArgb(203, 215, 223);
      this.btnHistory.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnHistory.BorderDrawing = true;
      this.btnHistory.FocusRectangleEnabled = false;
      this.btnHistory.Image = (Image) null;
      this.btnHistory.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnHistory.ImageBorderEnabled = true;
      this.btnHistory.ImageDropShadow = true;
      this.btnHistory.ImageFocused = (Image) null;
      this.btnHistory.ImageInactive = (Image) null;
      this.btnHistory.ImageMouseOver = (Image) null;
      this.btnHistory.ImageNormal = (Image) null;
      this.btnHistory.ImagePressed = (Image) null;
      this.btnHistory.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnHistory.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnHistory.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnHistory.Location = new Point(301, 103);
      this.btnHistory.Name = "btnHistory";
      this.btnHistory.OffsetPressedContent = true;
      this.btnHistory.Padding2 = 5;
      this.btnHistory.Size = new Size(79, 20);
      this.btnHistory.StretchImage = false;
      this.btnHistory.TabIndex = 13;
      this.btnHistory.Text = "History";
      this.btnHistory.TextDropShadow = false;
      this.btnHistory.UseVisualStyleBackColor = false;
      this.btnHistory.Visible = false;
      this.btnHistory.Click += new EventHandler(this.btnHistory_Click);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(159, 180, 193);
      this.ClientSize = new Size(400 * InterfaceMgr.UIScale, 123 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.btnHistory);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOK);
      this.Controls.Add((Control) this.tbNewName);
      this.Controls.Add((Control) this.tbOldName);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (RenameVillagePopup);
      this.ShowBar = true;
      this.ShowClose = true;
      this.Controls.SetChildIndex((Control) this.label1, 0);
      this.Controls.SetChildIndex((Control) this.label2, 0);
      this.Controls.SetChildIndex((Control) this.tbOldName, 0);
      this.Controls.SetChildIndex((Control) this.tbNewName, 0);
      this.Controls.SetChildIndex((Control) this.btnOK, 0);
      this.Controls.SetChildIndex((Control) this.btnCancel, 0);
      this.Controls.SetChildIndex((Control) this.btnHistory, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
