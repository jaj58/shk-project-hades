// Decompiled with JetBrains decompiler
// Type: Kingdoms.EmailOptInPopup
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
  public class EmailOptInPopup : MyFormBase
  {
    public ProfileLoginWindow m_Parent;
    private IContainer components;
    private Label label3;
    private BitmapButton btnClose;
    private CheckBox cbMailOptIn;

    public EmailOptInPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.ShowClose = false;
      this.cbMailOptIn.Text = SK.Text("EMAIL_OptIn", "Please Tick here if you would like us to contact you via email with information related to Stronghold Kingdoms, including exclusive offers and competitions.");
      this.cbMailOptIn.Checked = false;
      this.Title = this.Text = SK.Text("EMAIL_OptInHeader", "Stronghold Kingdoms Email Preferences");
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("MailUserBlockPopup_close");
      if (this.m_Parent != null)
        this.m_Parent.SetEmailOptInState(this.cbMailOptIn.Checked);
      this.Close();
    }

    private void cbMailOptIn_CheckedChanged(object sender, EventArgs e)
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
      this.label3 = new Label();
      this.btnClose = new BitmapButton();
      this.cbMailOptIn = new CheckBox();
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
      this.btnClose.Location = new Point(336, 115);
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
      this.cbMailOptIn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.cbMailOptIn.BackColor = ARGBColors.Transparent;
      this.cbMailOptIn.Location = new Point(32, 37);
      this.cbMailOptIn.Name = "cbMailOptIn";
      this.cbMailOptIn.Size = new Size(398, 72);
      this.cbMailOptIn.TabIndex = 18;
      this.cbMailOptIn.Text = "Mail Opt In";
      this.cbMailOptIn.UseVisualStyleBackColor = false;
      this.cbMailOptIn.CheckedChanged += new EventHandler(this.cbMailOptIn_CheckedChanged);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(458, 154);
      this.Controls.Add((Control) this.cbMailOptIn);
      this.Controls.Add((Control) this.btnClose);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (EmailOptInPopup);
      this.ShowClose = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Add Users";
      this.TopMost = true;
      this.Controls.SetChildIndex((Control) this.btnClose, 0);
      this.Controls.SetChildIndex((Control) this.cbMailOptIn, 0);
      this.ResumeLayout(false);
    }
  }
}
