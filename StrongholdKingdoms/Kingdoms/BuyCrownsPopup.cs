// Decompiled with JetBrains decompiler
// Type: Kingdoms.BuyCrownsPopup
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
  public class BuyCrownsPopup : MyFormBase
  {
    private IContainer components;
    private BitmapButton btnBuyCrowns;
    private Label label3;
    private Label lblMessage;
    private Form m_parent;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.btnBuyCrowns = new BitmapButton();
      this.label3 = new Label();
      this.lblMessage = new Label();
      this.SuspendLayout();
      this.btnBuyCrowns.BackColor = Color.FromArgb(203, 215, 223);
      this.btnBuyCrowns.BorderColor = ARGBColors.DarkBlue;
      this.btnBuyCrowns.BorderDrawing = true;
      this.btnBuyCrowns.FocusRectangleEnabled = false;
      this.btnBuyCrowns.Image = (Image) null;
      this.btnBuyCrowns.ImageBorderColor = ARGBColors.Chocolate;
      this.btnBuyCrowns.ImageBorderEnabled = true;
      this.btnBuyCrowns.ImageDropShadow = true;
      this.btnBuyCrowns.ImageFocused = (Image) null;
      this.btnBuyCrowns.ImageInactive = (Image) null;
      this.btnBuyCrowns.ImageMouseOver = (Image) null;
      this.btnBuyCrowns.ImageNormal = (Image) null;
      this.btnBuyCrowns.ImagePressed = (Image) null;
      this.btnBuyCrowns.InnerBorderColor = ARGBColors.LightGray;
      this.btnBuyCrowns.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnBuyCrowns.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnBuyCrowns.Location = new Point(115, 121);
      this.btnBuyCrowns.Name = "btnBuyCrowns";
      this.btnBuyCrowns.OffsetPressedContent = true;
      this.btnBuyCrowns.Padding2 = 5;
      this.btnBuyCrowns.Size = new Size(201, 39);
      this.btnBuyCrowns.StretchImage = false;
      this.btnBuyCrowns.TabIndex = 2;
      this.btnBuyCrowns.Text = "Buy Crowns";
      this.btnBuyCrowns.TextDropShadow = false;
      this.btnBuyCrowns.UseVisualStyleBackColor = false;
      this.btnBuyCrowns.Click += new EventHandler(this.btnOK_Click);
      this.label3.AutoSize = true;
      this.label3.BackColor = ARGBColors.Transparent;
      this.label3.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label3.ForeColor = ARGBColors.White;
      this.label3.Location = new Point(179, 7);
      this.label3.Name = "label3";
      this.label3.Size = new Size(0, 16);
      this.label3.TabIndex = 9;
      this.lblMessage.BackColor = ARGBColors.Transparent;
      this.lblMessage.Location = new Point(12, 44);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new Size(406, 65);
      this.lblMessage.TabIndex = 13;
      this.lblMessage.Text = "label1";
      this.lblMessage.TextAlign = ContentAlignment.MiddleCenter;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(159, 180, 193);
      this.ClientSize = new Size(430 * InterfaceMgr.UIScale, 199 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.lblMessage);
      this.Controls.Add((Control) this.btnBuyCrowns);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (BuyCrownsPopup);
      this.ShowClose = true;
      this.Controls.SetChildIndex((Control) this.btnBuyCrowns, 0);
      this.Controls.SetChildIndex((Control) this.lblMessage, 0);
      this.ResumeLayout(false);
    }

    public BuyCrownsPopup()
    {
      this.InitializeComponent();
      this.lblMessage.Font = FontManager.GetFont("Arial", 9.75f, FontStyle.Regular);
      this.Title = this.Text = SK.Text("BuyCardsPanel_Low_Crowns", "Crown stocks are too low m'lord");
      this.btnBuyCrowns.Text = SK.Text("BuyCrownsPanel_Buy_Crowns", "Buy Crowns");
      this.btnBuyCrowns.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
    }

    public void init(int numCrownsNeeded, Form parent)
    {
      this.m_parent = parent;
      this.lblMessage.Text = SK.Text("BuyCardsPanel_Cannot_Afford", "You cannot afford this.") + Environment.NewLine + Environment.NewLine + SK.Text("BuyCardsPanel_Extra_Crowns_Needed", "Extra Crowns Needed") + " : " + numCrownsNeeded.ToString();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      ((PlayCardsWindow) this.m_parent).GetCrowns();
      this.Close();
    }
  }
}
