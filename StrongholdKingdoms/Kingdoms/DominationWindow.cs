// Decompiled with JetBrains decompiler
// Type: Kingdoms.DominationWindow
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
  public class DominationWindow : MyFormBase
  {
    private IContainer components;
    private Label lblDuration;
    private Label lblDominationInfo;
    private BitmapButton btnClose;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lblDuration = new Label();
      this.lblDominationInfo = new Label();
      this.btnClose = new BitmapButton();
      this.SuspendLayout();
      this.lblDuration.BackColor = ARGBColors.Transparent;
      this.lblDuration.ForeColor = ARGBColors.Black;
      this.lblDuration.Location = new Point(3, 98);
      this.lblDuration.Name = "lblDuration";
      this.lblDuration.Size = new Size(419, 20);
      this.lblDuration.TabIndex = 16;
      this.lblDuration.Text = "0";
      this.lblDuration.TextAlign = ContentAlignment.MiddleCenter;
      this.lblDominationInfo.BackColor = ARGBColors.Transparent;
      this.lblDominationInfo.ForeColor = ARGBColors.Black;
      this.lblDominationInfo.Location = new Point(0, 53);
      this.lblDominationInfo.Name = "lblDominationInfo";
      this.lblDominationInfo.Size = new Size(422, 24);
      this.lblDominationInfo.TabIndex = 17;
      this.lblDominationInfo.Text = "Domination World will end in";
      this.lblDominationInfo.TextAlign = ContentAlignment.MiddleCenter;
      this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnClose.BackColor = Color.FromArgb(203, 215, 223);
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
      this.btnClose.Location = new Point(283, 144);
      this.btnClose.Name = "btnClose";
      this.btnClose.OffsetPressedContent = true;
      this.btnClose.Padding2 = 5;
      this.btnClose.Size = new Size(129, 26);
      this.btnClose.StretchImage = false;
      this.btnClose.TabIndex = 20;
      this.btnClose.Text = "Close";
      this.btnClose.TextDropShadow = false;
      this.btnClose.UseVisualStyleBackColor = false;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(128, 145, 156);
      this.ClientSize = new Size(424 * InterfaceMgr.UIScale, 182 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.btnClose);
      this.Controls.Add((Control) this.lblDominationInfo);
      this.Controls.Add((Control) this.lblDuration);
      this.Name = nameof (DominationWindow);
      this.ShowClose = true;
      this.Text = nameof (DominationWindow);
      this.Controls.SetChildIndex((Control) this.lblDuration, 0);
      this.Controls.SetChildIndex((Control) this.lblDominationInfo, 0);
      this.Controls.SetChildIndex((Control) this.btnClose, 0);
      this.ResumeLayout(false);
    }

    public DominationWindow()
    {
      this.InitializeComponent();
      this.closeCallback = new MyFormBase.MFBClose(this.domCloseCallback);
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblDuration.Font = FontManager.GetFont("Microsoft Sans Serif", 9f, FontStyle.Bold);
      this.Text = this.Title = SK.Text("Domination_World", "Domination World");
      this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
      this.lblDominationInfo.Text = SK.Text("Domination_Info", "Domination World will end in");
      this.lblDuration.Text = "";
    }

    public void updateText(string text) => this.lblDuration.Text = text;

    private void btnClose_Click(object sender, EventArgs e)
    {
      InterfaceMgr.Instance.closeDominatonWindow();
    }

    private void domCloseCallback() => InterfaceMgr.Instance.closeDominatonWindow();
  }
}
