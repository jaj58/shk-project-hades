// Decompiled with JetBrains decompiler
// Type: Kingdoms.ReinforcementsRetrievalPopup
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
  public class ReinforcementsRetrievalPopup : MyFormBase
  {
    private VillageReinforcementsPanel2 parent;
    private long reinfID = -1;
    private int numPeasants;
    private int numArchers;
    private int numPikemen;
    private int numSwordsmen;
    private int numCatapults;
    private bool drawing = true;
    private IContainer components;
    private BitmapButton btnRetrieve;
    private BitmapButton btnAll;
    private BitmapButton btnCancel;
    private TrackBar tbPeasants;
    private Label lblPeasants;
    private Label label2;
    private Label label3;
    private Label lblArchers;
    private TrackBar tbArchers;
    private Label label5;
    private Label lblPikemen;
    private TrackBar tbPikemen;
    private Label label7;
    private Label lblSwordsmen;
    private TrackBar tbSwordsmen;
    private Label label9;
    private Label lblCatapults;
    private TrackBar tbCatapults;

    public ReinforcementsRetrievalPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(
      VillageReinforcementsPanel2 p,
      long reinforcementID,
      int totalPeasants,
      int totalArchers,
      int totalPikemen,
      int totalSwordsmen,
      int totalCatapults)
    {
      this.btnRetrieve.Text = SK.Text("ReinforcementsRetrieval_Retrieve", "Retrieve");
      this.btnAll.Text = SK.Text("ReinforcementsRetrieval_Select_All", "Select All");
      this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.label2.Text = SK.Text("GENERIC_Peasants", "Peasants");
      this.label3.Text = SK.Text("GENERIC_Archers", "Archers");
      this.label5.Text = SK.Text("GENERIC_Pikemen", "Pikemen");
      this.label7.Text = SK.Text("GENERIC_Swordsmens", "Swordsmen");
      this.label9.Text = SK.Text("GENERIC_Catapults", "Catapults");
      this.Title = this.Text = SK.Text("ReinforcementsRetrieval_Retrieve_Reinforcements", "Retrieve Reinforcements");
      this.parent = p;
      this.reinfID = reinforcementID;
      this.numPeasants = totalPeasants;
      this.numArchers = totalArchers;
      this.numPikemen = totalPikemen;
      this.numSwordsmen = totalSwordsmen;
      this.numCatapults = totalCatapults;
      this.drawing = false;
      this.tbPeasants.Value = 0;
      this.tbPeasants.Maximum = Math.Max(this.numPeasants, 0);
      this.tbPeasants.Value = this.tbPeasants.Maximum;
      this.tbArchers.Value = 0;
      this.tbArchers.Maximum = Math.Max(this.numArchers, 0);
      this.tbArchers.Value = this.tbArchers.Maximum;
      this.tbPikemen.Value = 0;
      this.tbPikemen.Maximum = Math.Max(this.numPikemen, 0);
      this.tbPikemen.Value = this.tbPikemen.Maximum;
      this.tbSwordsmen.Value = 0;
      this.tbSwordsmen.Maximum = Math.Max(this.numSwordsmen, 0);
      this.tbSwordsmen.Value = this.tbSwordsmen.Maximum;
      this.tbCatapults.Value = 0;
      this.tbCatapults.Maximum = Math.Max(this.numCatapults, 0);
      this.tbCatapults.Value = this.tbCatapults.Maximum;
      this.drawing = true;
      this.updateText();
    }

    private void btnRetrieve_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("ReinforcementsRetrievalPopup_retrieve");
      bool flag = true;
      if (this.tbPeasants.Value != this.numPeasants || this.tbArchers.Value != this.numArchers || this.tbPikemen.Value != this.numPikemen || this.tbSwordsmen.Value != this.numSwordsmen || this.tbCatapults.Value != this.numCatapults)
        flag = false;
      if (flag)
        RemoteServices.Instance.ReturnReinforcements(this.reinfID);
      else
        RemoteServices.Instance.ReturnReinforcements(this.reinfID, this.tbPeasants.Value, this.tbArchers.Value, this.tbPikemen.Value, this.tbSwordsmen.Value, this.tbCatapults.Value);
      this.Close();
    }

    private void updateText()
    {
      if (!this.drawing)
        return;
      this.lblPeasants.Text = this.tbPeasants.Value.ToString() + "/" + this.numPeasants.ToString();
      this.lblArchers.Text = this.tbArchers.Value.ToString() + "/" + this.numArchers.ToString();
      this.lblPikemen.Text = this.tbPikemen.Value.ToString() + "/" + this.numPikemen.ToString();
      this.lblSwordsmen.Text = this.tbSwordsmen.Value.ToString() + "/" + this.numSwordsmen.ToString();
      this.lblCatapults.Text = this.tbCatapults.Value.ToString() + "/" + this.numCatapults.ToString();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("ReinforcementsRetrievalPopup_cancel");
      this.Close();
    }

    private void btnAll_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("ReinforcementsRetrievalPopup_all");
      this.drawing = false;
      this.tbPeasants.Value = this.numPeasants;
      this.tbArchers.Value = this.numArchers;
      this.tbPikemen.Value = this.numPikemen;
      this.tbSwordsmen.Value = this.numSwordsmen;
      this.tbCatapults.Value = this.numCatapults;
      this.drawing = true;
      this.updateText();
    }

    private void tbPeasants_ValueChanged(object sender, EventArgs e) => this.updateText();

    private void tbArchers_ValueChanged(object sender, EventArgs e) => this.updateText();

    private void tbPikemen_ValueChanged(object sender, EventArgs e) => this.updateText();

    private void tbSwordsmen_ValueChanged(object sender, EventArgs e) => this.updateText();

    private void tbCatapults_ValueChanged(object sender, EventArgs e) => this.updateText();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.btnRetrieve = new BitmapButton();
      this.btnAll = new BitmapButton();
      this.btnCancel = new BitmapButton();
      this.tbPeasants = new TrackBar();
      this.lblPeasants = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.lblArchers = new Label();
      this.tbArchers = new TrackBar();
      this.label5 = new Label();
      this.lblPikemen = new Label();
      this.tbPikemen = new TrackBar();
      this.label7 = new Label();
      this.lblSwordsmen = new Label();
      this.tbSwordsmen = new TrackBar();
      this.label9 = new Label();
      this.lblCatapults = new Label();
      this.tbCatapults = new TrackBar();
      this.tbPeasants.BeginInit();
      this.tbArchers.BeginInit();
      this.tbPikemen.BeginInit();
      this.tbSwordsmen.BeginInit();
      this.tbCatapults.BeginInit();
      this.SuspendLayout();
      this.btnRetrieve.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnRetrieve.BorderColor = ARGBColors.DarkBlue;
      this.btnRetrieve.BorderDrawing = true;
      this.btnRetrieve.FocusRectangleEnabled = false;
      this.btnRetrieve.Image = (Image) null;
      this.btnRetrieve.ImageBorderColor = ARGBColors.Chocolate;
      this.btnRetrieve.ImageBorderEnabled = true;
      this.btnRetrieve.ImageDropShadow = true;
      this.btnRetrieve.ImageFocused = (Image) null;
      this.btnRetrieve.ImageInactive = (Image) null;
      this.btnRetrieve.ImageMouseOver = (Image) null;
      this.btnRetrieve.ImageNormal = (Image) null;
      this.btnRetrieve.ImagePressed = (Image) null;
      this.btnRetrieve.InnerBorderColor = ARGBColors.LightGray;
      this.btnRetrieve.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnRetrieve.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnRetrieve.Location = new Point(268, 323);
      this.btnRetrieve.Name = "btnRetrieve";
      this.btnRetrieve.OffsetPressedContent = true;
      this.btnRetrieve.Padding2 = 5;
      this.btnRetrieve.Size = new Size(90, 30);
      this.btnRetrieve.StretchImage = false;
      this.btnRetrieve.TabIndex = 56;
      this.btnRetrieve.Text = "Retrieve";
      this.btnRetrieve.TextDropShadow = false;
      this.btnRetrieve.UseVisualStyleBackColor = true;
      this.btnRetrieve.Click += new EventHandler(this.btnRetrieve_Click);
      this.btnAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnAll.BorderColor = ARGBColors.DarkBlue;
      this.btnAll.BorderDrawing = true;
      this.btnAll.FocusRectangleEnabled = false;
      this.btnAll.Image = (Image) null;
      this.btnAll.ImageBorderColor = ARGBColors.Chocolate;
      this.btnAll.ImageBorderEnabled = true;
      this.btnAll.ImageDropShadow = true;
      this.btnAll.ImageFocused = (Image) null;
      this.btnAll.ImageInactive = (Image) null;
      this.btnAll.ImageMouseOver = (Image) null;
      this.btnAll.ImageNormal = (Image) null;
      this.btnAll.ImagePressed = (Image) null;
      this.btnAll.InnerBorderColor = ARGBColors.LightGray;
      this.btnAll.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnAll.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnAll.Location = new Point(140, 323);
      this.btnAll.Name = "btnAll";
      this.btnAll.OffsetPressedContent = true;
      this.btnAll.Padding2 = 5;
      this.btnAll.Size = new Size(90, 30);
      this.btnAll.StretchImage = false;
      this.btnAll.TabIndex = 57;
      this.btnAll.Text = "Select All";
      this.btnAll.TextDropShadow = false;
      this.btnAll.UseVisualStyleBackColor = true;
      this.btnAll.Click += new EventHandler(this.btnAll_Click);
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
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
      this.btnCancel.Location = new Point(12, 323);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.OffsetPressedContent = true;
      this.btnCancel.Padding2 = 5;
      this.btnCancel.Size = new Size(90, 30);
      this.btnCancel.StretchImage = false;
      this.btnCancel.TabIndex = 58;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.TextDropShadow = false;
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.tbPeasants.BackColor = Color.FromArgb(99, 112, 121);
      this.tbPeasants.Location = new Point(80, 43);
      this.tbPeasants.Name = "tbPeasants";
      this.tbPeasants.Size = new Size(198, 45);
      this.tbPeasants.TabIndex = 59;
      this.tbPeasants.ValueChanged += new EventHandler(this.tbPeasants_ValueChanged);
      this.lblPeasants.BackColor = ARGBColors.Transparent;
      this.lblPeasants.Location = new Point(276, 60);
      this.lblPeasants.Name = "lblPeasants";
      this.lblPeasants.Size = new Size(82, 28);
      this.lblPeasants.TabIndex = 60;
      this.lblPeasants.Text = "0/0";
      this.lblPeasants.TextAlign = ContentAlignment.TopRight;
      this.label2.AutoSize = true;
      this.label2.BackColor = ARGBColors.Transparent;
      this.label2.Location = new Point(12, 60);
      this.label2.Name = "label2";
      this.label2.Size = new Size(51, 13);
      this.label2.TabIndex = 61;
      this.label2.Text = "Peasants";
      this.label3.AutoSize = true;
      this.label3.BackColor = ARGBColors.Transparent;
      this.label3.Location = new Point(12, 111);
      this.label3.Name = "label3";
      this.label3.Size = new Size(43, 13);
      this.label3.TabIndex = 64;
      this.label3.Text = "Archers";
      this.lblArchers.BackColor = ARGBColors.Transparent;
      this.lblArchers.Location = new Point(276, 111);
      this.lblArchers.Name = "lblArchers";
      this.lblArchers.Size = new Size(82, 28);
      this.lblArchers.TabIndex = 63;
      this.lblArchers.Text = "0/0";
      this.lblArchers.TextAlign = ContentAlignment.TopRight;
      this.tbArchers.BackColor = Color.FromArgb(109, 124, 133);
      this.tbArchers.Location = new Point(80, 94);
      this.tbArchers.Name = "tbArchers";
      this.tbArchers.Size = new Size(198, 45);
      this.tbArchers.TabIndex = 62;
      this.tbArchers.ValueChanged += new EventHandler(this.tbArchers_ValueChanged);
      this.label5.AutoSize = true;
      this.label5.BackColor = ARGBColors.Transparent;
      this.label5.Location = new Point(12, 162);
      this.label5.Name = "label5";
      this.label5.Size = new Size(48, 13);
      this.label5.TabIndex = 67;
      this.label5.Text = "Pikemen";
      this.lblPikemen.BackColor = ARGBColors.Transparent;
      this.lblPikemen.Location = new Point(276, 162);
      this.lblPikemen.Name = "lblPikemen";
      this.lblPikemen.Size = new Size(82, 28);
      this.lblPikemen.TabIndex = 66;
      this.lblPikemen.Text = "0/0";
      this.lblPikemen.TextAlign = ContentAlignment.TopRight;
      this.tbPikemen.BackColor = Color.FromArgb(121, 137, 148);
      this.tbPikemen.Location = new Point(80, 145);
      this.tbPikemen.Name = "tbPikemen";
      this.tbPikemen.Size = new Size(198, 45);
      this.tbPikemen.TabIndex = 65;
      this.tbPikemen.ValueChanged += new EventHandler(this.tbPikemen_ValueChanged);
      this.label7.AutoSize = true;
      this.label7.BackColor = ARGBColors.Transparent;
      this.label7.Location = new Point(12, 213);
      this.label7.Name = "label7";
      this.label7.Size = new Size(62, 13);
      this.label7.TabIndex = 70;
      this.label7.Text = "Swordsmen";
      this.lblSwordsmen.BackColor = ARGBColors.Transparent;
      this.lblSwordsmen.Location = new Point(276, 213);
      this.lblSwordsmen.Name = "lblSwordsmen";
      this.lblSwordsmen.Size = new Size(82, 28);
      this.lblSwordsmen.TabIndex = 69;
      this.lblSwordsmen.Text = "0/0";
      this.lblSwordsmen.TextAlign = ContentAlignment.TopRight;
      this.tbSwordsmen.BackColor = Color.FromArgb(130, 147, 158);
      this.tbSwordsmen.Location = new Point(80, 196);
      this.tbSwordsmen.Name = "tbSwordsmen";
      this.tbSwordsmen.Size = new Size(198, 45);
      this.tbSwordsmen.TabIndex = 68;
      this.tbSwordsmen.ValueChanged += new EventHandler(this.tbSwordsmen_ValueChanged);
      this.label9.AutoSize = true;
      this.label9.BackColor = ARGBColors.Transparent;
      this.label9.Location = new Point(12, 264);
      this.label9.Name = "label9";
      this.label9.Size = new Size(51, 13);
      this.label9.TabIndex = 73;
      this.label9.Text = "Catapults";
      this.lblCatapults.BackColor = ARGBColors.Transparent;
      this.lblCatapults.Location = new Point(276, 264);
      this.lblCatapults.Name = "lblCatapults";
      this.lblCatapults.Size = new Size(82, 28);
      this.lblCatapults.TabIndex = 72;
      this.lblCatapults.Text = "0/0";
      this.lblCatapults.TextAlign = ContentAlignment.TopRight;
      this.tbCatapults.BackColor = Color.FromArgb(140, 159, 170);
      this.tbCatapults.Location = new Point(80, 247);
      this.tbCatapults.Name = "tbCatapults";
      this.tbCatapults.Size = new Size(198, 45);
      this.tbCatapults.TabIndex = 71;
      this.tbCatapults.ValueChanged += new EventHandler(this.tbCatapults_ValueChanged);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(370, 365);
      this.Controls.Add((Control) this.label9);
      this.Controls.Add((Control) this.lblCatapults);
      this.Controls.Add((Control) this.tbCatapults);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.lblSwordsmen);
      this.Controls.Add((Control) this.tbSwordsmen);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.lblPikemen);
      this.Controls.Add((Control) this.tbPikemen);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.lblArchers);
      this.Controls.Add((Control) this.tbArchers);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.lblPeasants);
      this.Controls.Add((Control) this.tbPeasants);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnAll);
      this.Controls.Add((Control) this.btnRetrieve);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (ReinforcementsRetrievalPopup);
      this.ShowClose = true;
      this.ShowIcon = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Retrieve Reinforcements";
      this.Controls.SetChildIndex((Control) this.btnRetrieve, 0);
      this.Controls.SetChildIndex((Control) this.btnAll, 0);
      this.Controls.SetChildIndex((Control) this.btnCancel, 0);
      this.Controls.SetChildIndex((Control) this.tbPeasants, 0);
      this.Controls.SetChildIndex((Control) this.lblPeasants, 0);
      this.Controls.SetChildIndex((Control) this.label2, 0);
      this.Controls.SetChildIndex((Control) this.tbArchers, 0);
      this.Controls.SetChildIndex((Control) this.lblArchers, 0);
      this.Controls.SetChildIndex((Control) this.label3, 0);
      this.Controls.SetChildIndex((Control) this.tbPikemen, 0);
      this.Controls.SetChildIndex((Control) this.lblPikemen, 0);
      this.Controls.SetChildIndex((Control) this.label5, 0);
      this.Controls.SetChildIndex((Control) this.tbSwordsmen, 0);
      this.Controls.SetChildIndex((Control) this.lblSwordsmen, 0);
      this.Controls.SetChildIndex((Control) this.label7, 0);
      this.Controls.SetChildIndex((Control) this.tbCatapults, 0);
      this.Controls.SetChildIndex((Control) this.lblCatapults, 0);
      this.Controls.SetChildIndex((Control) this.label9, 0);
      this.tbPeasants.EndInit();
      this.tbArchers.EndInit();
      this.tbPikemen.EndInit();
      this.tbSwordsmen.EndInit();
      this.tbCatapults.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
