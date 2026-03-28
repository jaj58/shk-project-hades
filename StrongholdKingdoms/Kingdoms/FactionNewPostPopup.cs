// Decompiled with JetBrains decompiler
// Type: Kingdoms.FactionNewPostPopup
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
  public class FactionNewPostPopup : MyFormBase
  {
    private long ThreadID = -1;
    private IForumReplyParent m_parent;
    private IContainer components;
    private TextBox tbHeading;
    private Label lblTopic;
    private TextBox tbMainText;
    private BitmapButton btnCancel;
    private BitmapButton btnOK;

    public FactionNewPostPopup()
    {
      this.InitializeComponent();
      this.btnOK.Enabled = false;
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(long threadID, IForumReplyParent parent, string heading)
    {
      this.lblTopic.Text = SK.Text("FORUMS_Topic", "Topic");
      this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
      this.Title = this.Text = SK.Text("FORUMS_New_Post", "New Post");
      this.ThreadID = threadID;
      this.m_parent = parent;
      this.tbHeading.Text = heading;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("FactionNewPostPopup_cancel");
      this.Close();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("FactionNewPostPopup_ok");
      if (this.m_parent != null)
        this.m_parent.newPost(this.ThreadID, this.tbMainText.Text);
      this.Close();
    }

    private void tbMainText_TextChanged(object sender, EventArgs e)
    {
      if (this.tbMainText.Text.Length > 0)
        this.btnOK.Enabled = true;
      else
        this.btnOK.Enabled = false;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.tbHeading = new TextBox();
      this.lblTopic = new Label();
      this.tbMainText = new TextBox();
      this.btnCancel = new BitmapButton();
      this.btnOK = new BitmapButton();
      this.SuspendLayout();
      this.tbHeading.BackColor = Color.FromArgb(134, 153, 165);
      this.tbHeading.Location = new Point(122, 38);
      this.tbHeading.MaxLength = 99;
      this.tbHeading.Name = "tbHeading";
      this.tbHeading.ReadOnly = true;
      this.tbHeading.Size = new Size(500, 20);
      this.tbHeading.TabIndex = 4;
      this.lblTopic.AutoSize = true;
      this.lblTopic.BackColor = ARGBColors.Transparent;
      this.lblTopic.Location = new Point(12, 41);
      this.lblTopic.Name = "lblTopic";
      this.lblTopic.Size = new Size(34, 13);
      this.lblTopic.TabIndex = 5;
      this.lblTopic.Text = "Topic";
      this.tbMainText.Location = new Point(15, 64);
      this.tbMainText.Multiline = true;
      this.tbMainText.Name = "tbMainText";
      this.tbMainText.Size = new Size(607, 291);
      this.tbMainText.TabIndex = 1;
      this.tbMainText.TextChanged += new EventHandler(this.tbMainText_TextChanged);
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
      this.btnCancel.Location = new Point(498, 368);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.OffsetPressedContent = true;
      this.btnCancel.Padding2 = 5;
      this.btnCancel.Size = new Size(124, 29);
      this.btnCancel.StretchImage = false;
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.TextDropShadow = false;
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
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
      this.btnOK.Location = new Point(368, 368);
      this.btnOK.Name = "btnOK";
      this.btnOK.OffsetPressedContent = true;
      this.btnOK.Padding2 = 5;
      this.btnOK.Size = new Size(124, 29);
      this.btnOK.StretchImage = false;
      this.btnOK.TabIndex = 2;
      this.btnOK.Text = "OK";
      this.btnOK.TextDropShadow = false;
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(642 * InterfaceMgr.UIScale, 407 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.btnOK);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.lblTopic);
      this.Controls.Add((Control) this.tbHeading);
      this.Controls.Add((Control) this.tbMainText);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (FactionNewPostPopup);
      this.ShowClose = true;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "New post";
      this.Controls.SetChildIndex((Control) this.tbMainText, 0);
      this.Controls.SetChildIndex((Control) this.tbHeading, 0);
      this.Controls.SetChildIndex((Control) this.lblTopic, 0);
      this.Controls.SetChildIndex((Control) this.btnCancel, 0);
      this.Controls.SetChildIndex((Control) this.btnOK, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
