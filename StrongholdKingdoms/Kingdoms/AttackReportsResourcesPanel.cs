// Decompiled with JetBrains decompiler
// Type: Kingdoms.AttackReportsResourcesPanel
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
  public class AttackReportsResourcesPanel : MyFormBase
  {
    private IContainer components;
    private Panel img1;
    private Panel img3;
    private Panel img5;
    private Panel img7;
    private Label lblResource1;
    private Label lblResource3;
    private Label lblResource5;
    private Label lblResource7;
    private Label lblResource8;
    private Label lblResource6;
    private Label lblResource4;
    private Label lblResource2;
    private Panel img8;
    private Panel img6;
    private Panel img4;
    private Panel img2;
    private BitmapButton btnClose;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.img1 = new Panel();
      this.img3 = new Panel();
      this.img5 = new Panel();
      this.img7 = new Panel();
      this.lblResource1 = new Label();
      this.lblResource3 = new Label();
      this.lblResource5 = new Label();
      this.lblResource7 = new Label();
      this.lblResource8 = new Label();
      this.lblResource6 = new Label();
      this.lblResource4 = new Label();
      this.lblResource2 = new Label();
      this.img8 = new Panel();
      this.img6 = new Panel();
      this.img4 = new Panel();
      this.img2 = new Panel();
      this.btnClose = new BitmapButton();
      this.SuspendLayout();
      this.img1.BackColor = ARGBColors.Transparent;
      this.img1.BackgroundImage = (Image) GFXLibrary.com_32_wood;
      this.img1.BackgroundImageLayout = ImageLayout.Center;
      this.img1.Location = new Point(32, 46);
      this.img1.Name = "img1";
      this.img1.Size = new Size(32, 32);
      this.img1.TabIndex = 108;
      this.img3.BackColor = ARGBColors.Transparent;
      this.img3.BackgroundImage = (Image) GFXLibrary.com_32_wood;
      this.img3.BackgroundImageLayout = ImageLayout.Center;
      this.img3.Location = new Point(32, 84);
      this.img3.Name = "img3";
      this.img3.Size = new Size(32, 32);
      this.img3.TabIndex = 108;
      this.img5.BackColor = ARGBColors.Transparent;
      this.img5.BackgroundImage = (Image) GFXLibrary.com_32_wood;
      this.img5.BackgroundImageLayout = ImageLayout.Center;
      this.img5.Location = new Point(32, 122);
      this.img5.Name = "img5";
      this.img5.Size = new Size(32, 32);
      this.img5.TabIndex = 108;
      this.img7.BackColor = ARGBColors.Transparent;
      this.img7.BackgroundImage = (Image) GFXLibrary.com_32_wood;
      this.img7.BackgroundImageLayout = ImageLayout.Center;
      this.img7.Location = new Point(32, 160);
      this.img7.Name = "img7";
      this.img7.Size = new Size(32, 32);
      this.img7.TabIndex = 108;
      this.lblResource1.BackColor = ARGBColors.Transparent;
      this.lblResource1.Location = new Point(70, 55);
      this.lblResource1.Name = "lblResource1";
      this.lblResource1.Size = new Size(81, 13);
      this.lblResource1.TabIndex = 111;
      this.lblResource1.Text = "0";
      this.lblResource1.TextAlign = ContentAlignment.TopRight;
      this.lblResource3.BackColor = ARGBColors.Transparent;
      this.lblResource3.Location = new Point(70, 94);
      this.lblResource3.Name = "lblResource3";
      this.lblResource3.Size = new Size(81, 13);
      this.lblResource3.TabIndex = 112;
      this.lblResource3.Text = "0";
      this.lblResource3.TextAlign = ContentAlignment.TopRight;
      this.lblResource5.BackColor = ARGBColors.Transparent;
      this.lblResource5.Location = new Point(70, 131);
      this.lblResource5.Name = "lblResource5";
      this.lblResource5.Size = new Size(81, 13);
      this.lblResource5.TabIndex = 113;
      this.lblResource5.Text = "0";
      this.lblResource5.TextAlign = ContentAlignment.TopRight;
      this.lblResource7.BackColor = ARGBColors.Transparent;
      this.lblResource7.Location = new Point(70, 169);
      this.lblResource7.Name = "lblResource7";
      this.lblResource7.Size = new Size(81, 13);
      this.lblResource7.TabIndex = 114;
      this.lblResource7.Text = "0";
      this.lblResource7.TextAlign = ContentAlignment.TopRight;
      this.lblResource8.BackColor = ARGBColors.Transparent;
      this.lblResource8.Location = new Point(256, 169);
      this.lblResource8.Name = "lblResource8";
      this.lblResource8.Size = new Size(81, 13);
      this.lblResource8.TabIndex = 122;
      this.lblResource8.Text = "0";
      this.lblResource8.TextAlign = ContentAlignment.TopRight;
      this.lblResource6.BackColor = ARGBColors.Transparent;
      this.lblResource6.Location = new Point(256, 131);
      this.lblResource6.Name = "lblResource6";
      this.lblResource6.Size = new Size(81, 13);
      this.lblResource6.TabIndex = 121;
      this.lblResource6.Text = "0";
      this.lblResource6.TextAlign = ContentAlignment.TopRight;
      this.lblResource4.BackColor = ARGBColors.Transparent;
      this.lblResource4.Location = new Point(256, 94);
      this.lblResource4.Name = "lblResource4";
      this.lblResource4.Size = new Size(81, 13);
      this.lblResource4.TabIndex = 120;
      this.lblResource4.Text = "0";
      this.lblResource4.TextAlign = ContentAlignment.TopRight;
      this.lblResource2.BackColor = ARGBColors.Transparent;
      this.lblResource2.Location = new Point(256, 55);
      this.lblResource2.Name = "lblResource2";
      this.lblResource2.Size = new Size(81, 13);
      this.lblResource2.TabIndex = 119;
      this.lblResource2.Text = "0";
      this.lblResource2.TextAlign = ContentAlignment.TopRight;
      this.img8.BackColor = ARGBColors.Transparent;
      this.img8.BackgroundImage = (Image) GFXLibrary.com_32_wood;
      this.img8.BackgroundImageLayout = ImageLayout.Center;
      this.img8.Location = new Point(218, 160);
      this.img8.Name = "img8";
      this.img8.Size = new Size(32, 32);
      this.img8.TabIndex = 116;
      this.img6.BackColor = ARGBColors.Transparent;
      this.img6.BackgroundImage = (Image) GFXLibrary.com_32_wood;
      this.img6.BackgroundImageLayout = ImageLayout.Center;
      this.img6.Location = new Point(218, 122);
      this.img6.Name = "img6";
      this.img6.Size = new Size(32, 32);
      this.img6.TabIndex = 115;
      this.img4.BackColor = ARGBColors.Transparent;
      this.img4.BackgroundImage = (Image) GFXLibrary.com_32_wood;
      this.img4.BackgroundImageLayout = ImageLayout.Center;
      this.img4.Location = new Point(218, 84);
      this.img4.Name = "img4";
      this.img4.Size = new Size(32, 32);
      this.img4.TabIndex = 118;
      this.img2.BackColor = ARGBColors.Transparent;
      this.img2.BackgroundImage = (Image) GFXLibrary.com_32_wood;
      this.img2.BackgroundImageLayout = ImageLayout.Center;
      this.img2.Location = new Point(218, 46);
      this.img2.Name = "img2";
      this.img2.Size = new Size(32, 32);
      this.img2.TabIndex = 117;
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
      this.btnClose.Location = new Point(259, 208);
      this.btnClose.Name = "btnClose";
      this.btnClose.OffsetPressedContent = true;
      this.btnClose.Padding2 = 5;
      this.btnClose.Size = new Size(98, 23);
      this.btnClose.StretchImage = false;
      this.btnClose.TabIndex = 123;
      this.btnClose.Text = "Close";
      this.btnClose.TextDropShadow = false;
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(378 * InterfaceMgr.UIScale, 243 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.btnClose);
      this.Controls.Add((Control) this.lblResource8);
      this.Controls.Add((Control) this.lblResource6);
      this.Controls.Add((Control) this.lblResource4);
      this.Controls.Add((Control) this.lblResource2);
      this.Controls.Add((Control) this.img8);
      this.Controls.Add((Control) this.img6);
      this.Controls.Add((Control) this.img4);
      this.Controls.Add((Control) this.img2);
      this.Controls.Add((Control) this.lblResource7);
      this.Controls.Add((Control) this.lblResource5);
      this.Controls.Add((Control) this.lblResource3);
      this.Controls.Add((Control) this.lblResource1);
      this.Controls.Add((Control) this.img7);
      this.Controls.Add((Control) this.img5);
      this.Controls.Add((Control) this.img3);
      this.Controls.Add((Control) this.img1);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (AttackReportsResourcesPanel);
      this.ShowClose = false;
      this.ShowIcon = false;
      this.Text = "Resources";
      this.TopMost = true;
      this.Controls.SetChildIndex((Control) this.img1, 0);
      this.Controls.SetChildIndex((Control) this.img3, 0);
      this.Controls.SetChildIndex((Control) this.img5, 0);
      this.Controls.SetChildIndex((Control) this.img7, 0);
      this.Controls.SetChildIndex((Control) this.lblResource1, 0);
      this.Controls.SetChildIndex((Control) this.lblResource3, 0);
      this.Controls.SetChildIndex((Control) this.lblResource5, 0);
      this.Controls.SetChildIndex((Control) this.lblResource7, 0);
      this.Controls.SetChildIndex((Control) this.img2, 0);
      this.Controls.SetChildIndex((Control) this.img4, 0);
      this.Controls.SetChildIndex((Control) this.img6, 0);
      this.Controls.SetChildIndex((Control) this.img8, 0);
      this.Controls.SetChildIndex((Control) this.lblResource2, 0);
      this.Controls.SetChildIndex((Control) this.lblResource4, 0);
      this.Controls.SetChildIndex((Control) this.lblResource6, 0);
      this.Controls.SetChildIndex((Control) this.lblResource8, 0);
      this.Controls.SetChildIndex((Control) this.btnClose, 0);
      this.ResumeLayout(false);
    }

    public AttackReportsResourcesPanel()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void setResources(GetReport_ReturnType data)
    {
      this.Text = this.Title = SK.Text("GENERIC_Resources", "Resources");
      this.img1.Visible = false;
      this.img2.Visible = false;
      this.img3.Visible = false;
      this.img4.Visible = false;
      this.img5.Visible = false;
      this.img6.Visible = false;
      this.img7.Visible = false;
      this.img8.Visible = false;
      this.lblResource1.Visible = false;
      this.lblResource2.Visible = false;
      this.lblResource3.Visible = false;
      this.lblResource4.Visible = false;
      this.lblResource5.Visible = false;
      this.lblResource6.Visible = false;
      this.lblResource7.Visible = false;
      this.lblResource8.Visible = false;
      switch (data.genericData30)
      {
        case 2:
          this.img1.Visible = true;
          this.lblResource1.Visible = true;
          this.img1.BackgroundImage = (Image) GFXLibrary.com_32_wood;
          this.lblResource1.Text = data.genericData22.ToString();
          this.img2.Visible = true;
          this.lblResource2.Visible = true;
          this.img2.BackgroundImage = (Image) GFXLibrary.com_32_stone;
          this.lblResource2.Text = data.genericData23.ToString();
          this.img3.Visible = true;
          this.lblResource3.Visible = true;
          this.img3.BackgroundImage = (Image) GFXLibrary.com_32_iron;
          this.lblResource3.Text = data.genericData24.ToString();
          this.img4.Visible = true;
          this.lblResource4.Visible = true;
          this.img4.BackgroundImage = (Image) GFXLibrary.com_32_pitch;
          this.lblResource4.Text = data.genericData25.ToString();
          break;
        case 4:
          this.img1.Visible = true;
          this.lblResource1.Visible = true;
          this.img1.BackgroundImage = (Image) GFXLibrary.com_32_apples;
          this.lblResource1.Text = data.genericData22.ToString();
          this.img2.Visible = true;
          this.lblResource2.Visible = true;
          this.img2.BackgroundImage = (Image) GFXLibrary.com_32_bread;
          this.lblResource2.Text = data.genericData23.ToString();
          this.img3.Visible = true;
          this.lblResource3.Visible = true;
          this.img3.BackgroundImage = (Image) GFXLibrary.com_32_cheese;
          this.lblResource3.Text = data.genericData24.ToString();
          this.img4.Visible = true;
          this.lblResource4.Visible = true;
          this.img4.BackgroundImage = (Image) GFXLibrary.com_32_meat;
          this.lblResource4.Text = data.genericData25.ToString();
          this.img5.Visible = true;
          this.lblResource5.Visible = true;
          this.img5.BackgroundImage = (Image) GFXLibrary.com_32_fish;
          this.lblResource5.Text = data.genericData26.ToString();
          this.img6.Visible = true;
          this.lblResource6.Visible = true;
          this.img6.BackgroundImage = (Image) GFXLibrary.com_32_veg;
          this.lblResource6.Text = data.genericData27.ToString();
          break;
        case 5:
          this.img1.Visible = true;
          this.lblResource1.Visible = true;
          this.img1.BackgroundImage = (Image) GFXLibrary.com_32_furniture;
          this.lblResource1.Text = data.genericData22.ToString();
          this.img2.Visible = true;
          this.lblResource2.Visible = true;
          this.img2.BackgroundImage = (Image) GFXLibrary.com_32_clothing;
          this.lblResource2.Text = data.genericData23.ToString();
          this.img3.Visible = true;
          this.lblResource3.Visible = true;
          this.img3.BackgroundImage = (Image) GFXLibrary.com_32_venison;
          this.lblResource3.Text = data.genericData24.ToString();
          this.img4.Visible = true;
          this.lblResource4.Visible = true;
          this.img4.BackgroundImage = (Image) GFXLibrary.com_32_wine;
          this.lblResource4.Text = data.genericData25.ToString();
          this.img5.Visible = true;
          this.lblResource5.Visible = true;
          this.img5.BackgroundImage = (Image) GFXLibrary.com_32_salt;
          this.lblResource5.Text = data.genericData26.ToString();
          this.img6.Visible = true;
          this.lblResource6.Visible = true;
          this.img6.BackgroundImage = (Image) GFXLibrary.com_32_metalwork;
          this.lblResource6.Text = data.genericData27.ToString();
          this.img7.Visible = true;
          this.lblResource7.Visible = true;
          this.img7.BackgroundImage = (Image) GFXLibrary.com_32_spice;
          this.lblResource7.Text = data.genericData28.ToString();
          this.img8.Visible = true;
          this.lblResource8.Visible = true;
          this.img8.BackgroundImage = (Image) GFXLibrary.com_32_silk;
          this.lblResource8.Text = data.genericData29.ToString();
          break;
        case 6:
          this.img1.Visible = true;
          this.lblResource1.Visible = true;
          this.img1.BackgroundImage = (Image) GFXLibrary.com_32_ale;
          this.lblResource1.Text = data.genericData22.ToString();
          break;
        case 7:
          this.img1.Visible = true;
          this.lblResource1.Visible = true;
          this.img1.BackgroundImage = (Image) GFXLibrary.com_32_bows;
          this.lblResource1.Text = data.genericData22.ToString();
          this.img2.Visible = true;
          this.lblResource2.Visible = true;
          this.img2.BackgroundImage = (Image) GFXLibrary.com_32_pikes;
          this.lblResource2.Text = data.genericData23.ToString();
          this.img3.Visible = true;
          this.lblResource3.Visible = true;
          this.img3.BackgroundImage = (Image) GFXLibrary.com_32_swords;
          this.lblResource3.Text = data.genericData24.ToString();
          this.img4.Visible = true;
          this.lblResource4.Visible = true;
          this.img4.BackgroundImage = (Image) GFXLibrary.com_32_armour;
          this.lblResource4.Text = data.genericData25.ToString();
          this.img5.Visible = true;
          this.lblResource5.Visible = true;
          this.img5.BackgroundImage = (Image) GFXLibrary.com_32_catapults;
          this.lblResource5.Text = data.genericData26.ToString();
          break;
      }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_close");
      this.Close();
    }
  }
}
