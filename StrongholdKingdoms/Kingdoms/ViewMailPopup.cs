// Decompiled with JetBrains decompiler
// Type: Kingdoms.ViewMailPopup
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
  public class ViewMailPopup : MyFormBase
  {
    private MailScreen m_parent;
    private IContainer components;
    private Label label3;
    private BitmapButton btnClose;
    private TextBox tbBody;
    private TextBox textBox2;
    private Label lbFrom;
    private Label lblFromName;
    private Label lbDate;
    private Label lbDateValue;
    private BitmapButton btnCopyClipboard;
    private BitmapButton btnCopySelected;

    public ViewMailPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.tbBody.Focus();
    }

    public void init(MailScreen parent, string header, string body, string from, string date)
    {
      body = body.Replace("\r\n", "\n");
      body = body.Replace("\n", "\r\n");
      this.m_parent = parent;
      this.Title = SK.Text("MailScreen_Mail", "Mail");
      this.tbBody.Text = body;
      this.textBox2.Text = header;
      this.lblFromName.Text = from;
      this.lbDateValue.Text = date;
      this.lbFrom.Text = SK.Text("MailScreen_From", "From") + " :";
      this.lbDate.Text = SK.Text("MailScreen_Date", "Date") + " :";
      this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
      this.btnCopySelected.Text = SK.Text("MailScreen_CopySelected", "Copy Selected");
      this.btnCopyClipboard.Text = SK.Text("MailScreen_CopyAll", "Copy All");
      this.tbBody.Focus();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("MailUserBlockPopup_close");
      this.Close();
    }

    private void btnCopyClipboard_Click(object sender, EventArgs e)
    {
      try
      {
        Clipboard.SetText(this.tbBody.Text);
      }
      catch (Exception ex)
      {
      }
    }

    private void btnCopySelected_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.tbBody.SelectedText.Length <= 0)
          return;
        Clipboard.SetText(this.tbBody.SelectedText);
      }
      catch (Exception ex)
      {
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label3 = new Label();
      this.btnClose = new BitmapButton();
      this.tbBody = new TextBox();
      this.textBox2 = new TextBox();
      this.lbFrom = new Label();
      this.lblFromName = new Label();
      this.lbDate = new Label();
      this.lbDateValue = new Label();
      this.btnCopyClipboard = new BitmapButton();
      this.btnCopySelected = new BitmapButton();
      this.SuspendLayout();
      this.label3.AutoSize = true;
      this.label3.Location = new Point(7, 76);
      this.label3.Name = "label3";
      this.label3.Size = new Size(79, 13);
      this.label3.TabIndex = 16;
      this.label3.Text = "Search Results";
      this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnClose.BorderColor = ARGBColors.DarkBlue;
      this.btnClose.BorderDrawing = true;
      this.btnClose.FocusRectangleEnabled = false;
      this.btnClose.Image = (Image) null;
      this.btnClose.ImageBorderColor = ARGBColors.Chocolate;
      this.btnClose.ImageBorderEnabled = true;
      this.btnClose.ImageDropShadow = true;
      this.btnClose.ImageFocused = (Image) null;
      this.btnClose.ImageInactive = (Image) null;
      this.btnClose.ImageMouseOver = (Image) null;
      this.btnClose.ImageNormal = (Image) null;
      this.btnClose.ImagePressed = (Image) null;
      this.btnClose.InnerBorderColor = ARGBColors.LightGray;
      this.btnClose.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnClose.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnClose.Location = new Point(676, 487);
      this.btnClose.Name = "btnClose";
      this.btnClose.OffsetPressedContent = true;
      this.btnClose.Padding2 = 5;
      this.btnClose.Size = new Size(110, 27);
      this.btnClose.StretchImage = false;
      this.btnClose.TabIndex = 17;
      this.btnClose.Text = "Close";
      this.btnClose.TextDropShadow = false;
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.tbBody.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.tbBody.BackColor = ARGBColors.White;
      this.tbBody.ForeColor = ARGBColors.Black;
      this.tbBody.Location = new Point(14, 82);
      this.tbBody.Multiline = true;
      this.tbBody.Name = "textBox1";
      this.tbBody.ReadOnly = true;
      this.tbBody.ScrollBars = ScrollBars.Vertical;
      this.tbBody.Size = new Size(772, 399);
      this.tbBody.TabIndex = 18;
      this.textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.textBox2.BackColor = ARGBColors.White;
      this.textBox2.ForeColor = ARGBColors.Black;
      this.textBox2.Location = new Point(14, 56);
      this.textBox2.Name = "textBox2";
      this.textBox2.ReadOnly = true;
      this.textBox2.Size = new Size(772, 20);
      this.textBox2.TabIndex = 19;
      this.lbFrom.AutoSize = true;
      this.lbFrom.BackColor = ARGBColors.Transparent;
      this.lbFrom.ForeColor = ARGBColors.White;
      this.lbFrom.Location = new Point(18, 39);
      this.lbFrom.Name = "lbFrom";
      this.lbFrom.Size = new Size(35, 13);
      this.lbFrom.TabIndex = 20;
      this.lbFrom.Text = "label1";
      this.lblFromName.AutoSize = true;
      this.lblFromName.BackColor = ARGBColors.Transparent;
      this.lblFromName.ForeColor = ARGBColors.White;
      this.lblFromName.Location = new Point(83, 39);
      this.lblFromName.Name = "lblFromName";
      this.lblFromName.Size = new Size(35, 13);
      this.lblFromName.TabIndex = 21;
      this.lblFromName.Text = "label1";
      this.lbDate.AutoSize = true;
      this.lbDate.BackColor = ARGBColors.Transparent;
      this.lbDate.ForeColor = ARGBColors.White;
      this.lbDate.Location = new Point(594, 39);
      this.lbDate.Name = "lbDate";
      this.lbDate.Size = new Size(35, 13);
      this.lbDate.TabIndex = 22;
      this.lbDate.Text = "label1";
      this.lbDateValue.AutoSize = true;
      this.lbDateValue.BackColor = ARGBColors.Transparent;
      this.lbDateValue.ForeColor = ARGBColors.White;
      this.lbDateValue.Location = new Point(656, 39);
      this.lbDateValue.Name = "lbDateValue";
      this.lbDateValue.Size = new Size(35, 13);
      this.lbDateValue.TabIndex = 23;
      this.lbDateValue.Text = "label1";
      this.btnCopyClipboard.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCopyClipboard.BorderColor = ARGBColors.DarkBlue;
      this.btnCopyClipboard.BorderDrawing = true;
      this.btnCopyClipboard.FocusRectangleEnabled = false;
      this.btnCopyClipboard.Image = (Image) null;
      this.btnCopyClipboard.ImageBorderColor = ARGBColors.Chocolate;
      this.btnCopyClipboard.ImageBorderEnabled = true;
      this.btnCopyClipboard.ImageDropShadow = true;
      this.btnCopyClipboard.ImageFocused = (Image) null;
      this.btnCopyClipboard.ImageInactive = (Image) null;
      this.btnCopyClipboard.ImageMouseOver = (Image) null;
      this.btnCopyClipboard.ImageNormal = (Image) null;
      this.btnCopyClipboard.ImagePressed = (Image) null;
      this.btnCopyClipboard.InnerBorderColor = ARGBColors.LightGray;
      this.btnCopyClipboard.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnCopyClipboard.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnCopyClipboard.Location = new Point(14, 487);
      this.btnCopyClipboard.Name = "btnCopyClipboard";
      this.btnCopyClipboard.OffsetPressedContent = true;
      this.btnCopyClipboard.Padding2 = 5;
      this.btnCopyClipboard.Size = new Size(216, 27);
      this.btnCopyClipboard.StretchImage = false;
      this.btnCopyClipboard.TabIndex = 24;
      this.btnCopyClipboard.Text = "Copy All to Clipboard";
      this.btnCopyClipboard.TextDropShadow = false;
      this.btnCopyClipboard.UseVisualStyleBackColor = true;
      this.btnCopyClipboard.Click += new EventHandler(this.btnCopyClipboard_Click);
      this.btnCopySelected.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCopySelected.BorderColor = ARGBColors.DarkBlue;
      this.btnCopySelected.BorderDrawing = true;
      this.btnCopySelected.FocusRectangleEnabled = false;
      this.btnCopySelected.Image = (Image) null;
      this.btnCopySelected.ImageBorderColor = ARGBColors.Chocolate;
      this.btnCopySelected.ImageBorderEnabled = true;
      this.btnCopySelected.ImageDropShadow = true;
      this.btnCopySelected.ImageFocused = (Image) null;
      this.btnCopySelected.ImageInactive = (Image) null;
      this.btnCopySelected.ImageMouseOver = (Image) null;
      this.btnCopySelected.ImageNormal = (Image) null;
      this.btnCopySelected.ImagePressed = (Image) null;
      this.btnCopySelected.InnerBorderColor = ARGBColors.LightGray;
      this.btnCopySelected.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnCopySelected.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnCopySelected.Location = new Point(236, 487);
      this.btnCopySelected.Name = "btnCopySelected";
      this.btnCopySelected.OffsetPressedContent = true;
      this.btnCopySelected.Padding2 = 5;
      this.btnCopySelected.Size = new Size(216, 27);
      this.btnCopySelected.StretchImage = false;
      this.btnCopySelected.TabIndex = 25;
      this.btnCopySelected.Text = "Copy Selected to Clipboard";
      this.btnCopySelected.TextDropShadow = false;
      this.btnCopySelected.UseVisualStyleBackColor = true;
      this.btnCopySelected.Click += new EventHandler(this.btnCopySelected_Click);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(798, 526);
      this.Controls.Add((Control) this.btnCopySelected);
      this.Controls.Add((Control) this.btnCopyClipboard);
      this.Controls.Add((Control) this.lbDateValue);
      this.Controls.Add((Control) this.lbDate);
      this.Controls.Add((Control) this.lblFromName);
      this.Controls.Add((Control) this.lbFrom);
      this.Controls.Add((Control) this.textBox2);
      this.Controls.Add((Control) this.tbBody);
      this.Controls.Add((Control) this.btnClose);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (ViewMailPopup);
      this.ShowClose = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Add Users";
      this.TopMost = true;
      this.Controls.SetChildIndex((Control) this.btnClose, 0);
      this.Controls.SetChildIndex((Control) this.tbBody, 0);
      this.Controls.SetChildIndex((Control) this.textBox2, 0);
      this.Controls.SetChildIndex((Control) this.lbFrom, 0);
      this.Controls.SetChildIndex((Control) this.lblFromName, 0);
      this.Controls.SetChildIndex((Control) this.lbDate, 0);
      this.Controls.SetChildIndex((Control) this.lbDateValue, 0);
      this.Controls.SetChildIndex((Control) this.btnCopyClipboard, 0);
      this.Controls.SetChildIndex((Control) this.btnCopySelected, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
