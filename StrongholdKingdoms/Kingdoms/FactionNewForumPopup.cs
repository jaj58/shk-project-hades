// Decompiled with JetBrains decompiler
// Type: Kingdoms.FactionNewForumPopup
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
  public class FactionNewForumPopup : MyFormBase
  {
    private FactionNewForumPanel m_parent;
    private IContainer components;
    private TextBox tbForumName;
    private Label lblTopic;
    private BitmapButton btnCancel;
    private BitmapButton btnOK;

    public FactionNewForumPopup()
    {
      this.InitializeComponent();
      this.btnOK.Enabled = false;
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(FactionNewForumPanel parent)
    {
      this.lblTopic.Text = SK.Text("FORUMS_Sub_Name", "Forum Sub Name");
      this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
      this.Title = this.Text = SK.Text("FORUMS_New_Sub_Forum", "New Sub Forum");
      this.m_parent = parent;
      this.btnOK.Enabled = false;
      this.tbForumName.Focus();
    }

    public void setFocus() => this.tbForumName.Focus();

    private void btnCancel_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("FactionNewForumPopup_cancel");
      this.Close();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("FactionNewForumPopup_ok");
      if (this.m_parent != null)
        this.m_parent.createNewForum(this.tbForumName.Text);
      this.Close();
    }

    private void tbForumName_TextChanged(object sender, EventArgs e)
    {
      if (this.tbForumName.Text.Length > 0)
        this.btnOK.Enabled = true;
      else
        this.btnOK.Enabled = false;
    }

    private void tbForumName_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyValue != 13 || this.tbForumName.Text.Length <= 0)
        return;
      this.btnOK_Click(sender, (EventArgs) e);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.tbForumName = new TextBox();
      this.lblTopic = new Label();
      this.btnCancel = new BitmapButton();
      this.btnOK = new BitmapButton();
      this.SuspendLayout();
      this.tbForumName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tbForumName.Location = new Point(162, 50);
      this.tbForumName.MaxLength = 99;
      this.tbForumName.Name = "tbForumName";
      this.tbForumName.Size = new Size(460, 20);
      this.tbForumName.TabIndex = 4;
      this.tbForumName.TextChanged += new EventHandler(this.tbForumName_TextChanged);
      this.tbForumName.KeyUp += new KeyEventHandler(this.tbForumName_KeyUp);
      this.lblTopic.AutoSize = true;
      this.lblTopic.BackColor = ARGBColors.Transparent;
      this.lblTopic.Location = new Point(7, 53);
      this.lblTopic.Name = "lblTopic";
      this.lblTopic.Size = new Size(89, 13);
      this.lblTopic.TabIndex = 5;
      this.lblTopic.Text = "Forum Sub Name";
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.BorderColor = ARGBColors.DarkBlue;
      this.btnCancel.BorderDrawing = true;
      this.btnCancel.FocusRectangleEnabled = false;
      this.btnCancel.Image = (Image) null;
      this.btnCancel.ImageBorderColor = ARGBColors.Chocolate;
      this.btnCancel.ImageBorderEnabled = true;
      this.btnCancel.ImageDropShadow = true;
      this.btnCancel.ImageFocused = (Image) null;
      this.btnCancel.ImageInactive = (Image) null;
      this.btnCancel.ImageMouseOver = (Image) null;
      this.btnCancel.ImageNormal = (Image) null;
      this.btnCancel.ImagePressed = (Image) null;
      this.btnCancel.InnerBorderColor = ARGBColors.LightGray;
      this.btnCancel.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnCancel.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnCancel.Location = new Point(498, 85);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.OffsetPressedContent = true;
      this.btnCancel.Padding2 = 5;
      this.btnCancel.Size = new Size(124, 27);
      this.btnCancel.StretchImage = false;
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.TextDropShadow = false;
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnOK.BorderColor = ARGBColors.DarkBlue;
      this.btnOK.BorderDrawing = true;
      this.btnOK.FocusRectangleEnabled = false;
      this.btnOK.Image = (Image) null;
      this.btnOK.ImageBorderColor = ARGBColors.Chocolate;
      this.btnOK.ImageBorderEnabled = true;
      this.btnOK.ImageDropShadow = true;
      this.btnOK.ImageFocused = (Image) null;
      this.btnOK.ImageInactive = (Image) null;
      this.btnOK.ImageMouseOver = (Image) null;
      this.btnOK.ImageNormal = (Image) null;
      this.btnOK.ImagePressed = (Image) null;
      this.btnOK.InnerBorderColor = ARGBColors.LightGray;
      this.btnOK.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnOK.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnOK.Location = new Point(368, 85);
      this.btnOK.Name = "btnOK";
      this.btnOK.OffsetPressedContent = true;
      this.btnOK.Padding2 = 5;
      this.btnOK.Size = new Size(124, 27);
      this.btnOK.StretchImage = false;
      this.btnOK.TabIndex = 2;
      this.btnOK.Text = "OK";
      this.btnOK.TextDropShadow = false;
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(642 * InterfaceMgr.UIScale, (int) sbyte.MaxValue * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.btnOK);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.lblTopic);
      this.Controls.Add((Control) this.tbForumName);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (FactionNewForumPopup);
      this.ShowClose = true;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "New Sub Forum";
      this.Controls.SetChildIndex((Control) this.tbForumName, 0);
      this.Controls.SetChildIndex((Control) this.lblTopic, 0);
      this.Controls.SetChildIndex((Control) this.btnCancel, 0);
      this.Controls.SetChildIndex((Control) this.btnOK, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
