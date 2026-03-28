// Decompiled with JetBrains decompiler
// Type: Kingdoms.AGUR
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class AGUR : Form
  {
    private int m_villageID = -1;
    private string m_reasonString = "";
    private IContainer components;
    private Button btnGiveResources;
    private Button btnCancel;
    private TextBox tbAmount;
    private Label label1;
    private RadioButton rbWood;
    private RadioButton rbStone;
    private RadioButton rbIron;
    private RadioButton rbPitch;
    private RadioButton rbApples;
    private RadioButton rbMeat;
    private RadioButton rbCheese;
    private RadioButton rbBread;
    private RadioButton rbVeg;
    private RadioButton rbFish;
    private RadioButton rbAle;
    private RadioButton rbBows;
    private RadioButton rbPikes;
    private RadioButton rbSwords;
    private RadioButton rbArmour;
    private RadioButton rbCatapults;
    private RadioButton rbVenison;
    private RadioButton rbClothes;
    private RadioButton rbFurniture;
    private RadioButton rbWine;
    private RadioButton rbSalt;
    private RadioButton rbMetalware;
    private RadioButton rbSpices;
    private RadioButton rbSilk;

    public AGUR() => this.InitializeComponent();

    public void init(int villageID)
    {
      this.m_villageID = villageID;
      this.Text = "Give Resources To : " + GameEngine.Instance.World.getVillageName(this.m_villageID);
      if (!GameEngine.Instance.World.isCapital(this.m_villageID))
        return;
      this.btnGiveResources.Enabled = false;
    }

    private void btnCancel_Click(object sender, EventArgs e) => this.Close();

    private void btnGiveResources_Click(object sender, EventArgs e)
    {
      int int32FromString = UserInfoScreen2.getInt32FromString(this.tbAmount.Text);
      if (int32FromString <= 0 || int32FromString > 100000)
        return;
      int num = 0;
      if (this.rbWood.Checked)
        num = 6;
      if (this.rbStone.Checked)
        num = 7;
      if (this.rbIron.Checked)
        num = 8;
      if (this.rbPitch.Checked)
        num = 9;
      if (this.rbAle.Checked)
        num = 12;
      if (this.rbApples.Checked)
        num = 13;
      if (this.rbBread.Checked)
        num = 14;
      if (this.rbMeat.Checked)
        num = 16;
      if (this.rbCheese.Checked)
        num = 17;
      if (this.rbVeg.Checked)
        num = 15;
      if (this.rbFish.Checked)
        num = 18;
      if (this.rbBows.Checked)
        num = 29;
      if (this.rbPikes.Checked)
        num = 28;
      if (this.rbSwords.Checked)
        num = 30;
      if (this.rbArmour.Checked)
        num = 31;
      if (this.rbCatapults.Checked)
        num = 32;
      if (this.rbVenison.Checked)
        num = 22;
      if (this.rbClothes.Checked)
        num = 19;
      if (this.rbFurniture.Checked)
        num = 21;
      if (this.rbMetalware.Checked)
        num = 26;
      if (this.rbSalt.Checked)
        num = 23;
      if (this.rbWine.Checked)
        num = 33;
      if (this.rbSpices.Checked)
        num = 24;
      if (this.rbSilk.Checked)
        num = 25;
      if (num == 0)
        return;
      this.sendCommandToServer(1000 + num, int32FromString);
    }

    public void setReasonString(string reasonString) => this.m_reasonString = reasonString;

    private void sendCommandToServer(int command, int duration)
    {
      if (RemoteServices.Instance.Admin)
      {
        this.m_reasonString = "";
        ReasonPopup reasonPopup = new ReasonPopup();
        reasonPopup.initResources(this, command - 1000);
        int num1 = (int) reasonPopup.ShowDialog();
        if (this.m_reasonString.Length > 0)
        {
          RemoteServices.Instance.SendCommands(this.m_villageID, command, duration, this.m_reasonString);
          this.Close();
        }
        else
        {
          int num2 = (int) MyMessageBox.Show("Not reason given", "Admin Error");
        }
      }
      else
      {
        int num = (int) MyMessageBox.Show("Command not sent", "Admin Error");
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AGUR));
      this.btnGiveResources = new Button();
      this.btnCancel = new Button();
      this.tbAmount = new TextBox();
      this.label1 = new Label();
      this.rbWood = new RadioButton();
      this.rbStone = new RadioButton();
      this.rbIron = new RadioButton();
      this.rbPitch = new RadioButton();
      this.rbApples = new RadioButton();
      this.rbMeat = new RadioButton();
      this.rbCheese = new RadioButton();
      this.rbBread = new RadioButton();
      this.rbVeg = new RadioButton();
      this.rbFish = new RadioButton();
      this.rbAle = new RadioButton();
      this.rbBows = new RadioButton();
      this.rbPikes = new RadioButton();
      this.rbSwords = new RadioButton();
      this.rbArmour = new RadioButton();
      this.rbCatapults = new RadioButton();
      this.rbVenison = new RadioButton();
      this.rbClothes = new RadioButton();
      this.rbFurniture = new RadioButton();
      this.rbWine = new RadioButton();
      this.rbSalt = new RadioButton();
      this.rbMetalware = new RadioButton();
      this.rbSpices = new RadioButton();
      this.rbSilk = new RadioButton();
      this.SuspendLayout();
      this.btnGiveResources.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnGiveResources.Location = new Point(294, 244);
      this.btnGiveResources.Name = "btnGiveResources";
      this.btnGiveResources.Size = new Size(116, 26);
      this.btnGiveResources.TabIndex = 0;
      this.btnGiveResources.Text = "Give Resources";
      this.btnGiveResources.UseVisualStyleBackColor = true;
      this.btnGiveResources.Click += new EventHandler(this.btnGiveResources_Click);
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.Location = new Point(172, 244);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(116, 26);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.tbAmount.Location = new Point(21, 207);
      this.tbAmount.Name = "tbAmount";
      this.tbAmount.Size = new Size(100, 20);
      this.tbAmount.TabIndex = 2;
      this.tbAmount.Text = "0";
      this.label1.AutoSize = true;
      this.label1.Location = new Point((int) sbyte.MaxValue, 210);
      this.label1.Name = "label1";
      this.label1.Size = new Size(100, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Amount (1-100,000)";
      this.rbWood.AutoSize = true;
      this.rbWood.Checked = true;
      this.rbWood.Location = new Point(21, 12);
      this.rbWood.Name = "rbWood";
      this.rbWood.Size = new Size(54, 17);
      this.rbWood.TabIndex = 4;
      this.rbWood.TabStop = true;
      this.rbWood.Text = "Wood";
      this.rbWood.UseVisualStyleBackColor = true;
      this.rbStone.AutoSize = true;
      this.rbStone.Location = new Point(21, 35);
      this.rbStone.Name = "rbStone";
      this.rbStone.Size = new Size(53, 17);
      this.rbStone.TabIndex = 5;
      this.rbStone.Text = "Stone";
      this.rbStone.UseVisualStyleBackColor = true;
      this.rbIron.AutoSize = true;
      this.rbIron.Location = new Point(21, 58);
      this.rbIron.Name = "rbIron";
      this.rbIron.Size = new Size(43, 17);
      this.rbIron.TabIndex = 6;
      this.rbIron.Text = "Iron";
      this.rbIron.UseVisualStyleBackColor = true;
      this.rbPitch.AutoSize = true;
      this.rbPitch.Location = new Point(21, 81);
      this.rbPitch.Name = "rbPitch";
      this.rbPitch.Size = new Size(49, 17);
      this.rbPitch.TabIndex = 7;
      this.rbPitch.Text = "Pitch";
      this.rbPitch.UseVisualStyleBackColor = true;
      this.rbApples.AutoSize = true;
      this.rbApples.Location = new Point(105, 12);
      this.rbApples.Name = "rbApples";
      this.rbApples.Size = new Size(57, 17);
      this.rbApples.TabIndex = 8;
      this.rbApples.Text = "Apples";
      this.rbApples.UseVisualStyleBackColor = true;
      this.rbMeat.AutoSize = true;
      this.rbMeat.Location = new Point(105, 35);
      this.rbMeat.Name = "rbMeat";
      this.rbMeat.Size = new Size(49, 17);
      this.rbMeat.TabIndex = 9;
      this.rbMeat.Text = "Meat";
      this.rbMeat.UseVisualStyleBackColor = true;
      this.rbCheese.AutoSize = true;
      this.rbCheese.Location = new Point(105, 58);
      this.rbCheese.Name = "rbCheese";
      this.rbCheese.Size = new Size(61, 17);
      this.rbCheese.TabIndex = 10;
      this.rbCheese.Text = "Cheese";
      this.rbCheese.UseVisualStyleBackColor = true;
      this.rbBread.AutoSize = true;
      this.rbBread.Location = new Point(105, 81);
      this.rbBread.Name = "rbBread";
      this.rbBread.Size = new Size(53, 17);
      this.rbBread.TabIndex = 11;
      this.rbBread.Text = "Bread";
      this.rbBread.UseVisualStyleBackColor = true;
      this.rbVeg.AutoSize = true;
      this.rbVeg.Location = new Point(105, 104);
      this.rbVeg.Name = "rbVeg";
      this.rbVeg.Size = new Size(44, 17);
      this.rbVeg.TabIndex = 12;
      this.rbVeg.Text = "Veg";
      this.rbVeg.UseVisualStyleBackColor = true;
      this.rbFish.AutoSize = true;
      this.rbFish.Location = new Point(105, (int) sbyte.MaxValue);
      this.rbFish.Name = "rbFish";
      this.rbFish.Size = new Size(44, 17);
      this.rbFish.TabIndex = 13;
      this.rbFish.Text = "Fish";
      this.rbFish.UseVisualStyleBackColor = true;
      this.rbAle.AutoSize = true;
      this.rbAle.Location = new Point(21, (int) sbyte.MaxValue);
      this.rbAle.Name = "rbAle";
      this.rbAle.Size = new Size(40, 17);
      this.rbAle.TabIndex = 14;
      this.rbAle.Text = "Ale";
      this.rbAle.UseVisualStyleBackColor = true;
      this.rbBows.AutoSize = true;
      this.rbBows.Location = new Point(201, 12);
      this.rbBows.Name = "rbBows";
      this.rbBows.Size = new Size(51, 17);
      this.rbBows.TabIndex = 15;
      this.rbBows.Text = "Bows";
      this.rbBows.UseVisualStyleBackColor = true;
      this.rbPikes.AutoSize = true;
      this.rbPikes.Location = new Point(201, 35);
      this.rbPikes.Name = "rbPikes";
      this.rbPikes.Size = new Size(51, 17);
      this.rbPikes.TabIndex = 16;
      this.rbPikes.Text = "Pikes";
      this.rbPikes.UseVisualStyleBackColor = true;
      this.rbSwords.AutoSize = true;
      this.rbSwords.Location = new Point(201, 58);
      this.rbSwords.Name = "rbSwords";
      this.rbSwords.Size = new Size(60, 17);
      this.rbSwords.TabIndex = 17;
      this.rbSwords.Text = "Swords";
      this.rbSwords.UseVisualStyleBackColor = true;
      this.rbArmour.AutoSize = true;
      this.rbArmour.Location = new Point(201, 81);
      this.rbArmour.Name = "rbArmour";
      this.rbArmour.Size = new Size(58, 17);
      this.rbArmour.TabIndex = 18;
      this.rbArmour.Text = "Armour";
      this.rbArmour.UseVisualStyleBackColor = true;
      this.rbCatapults.AutoSize = true;
      this.rbCatapults.Location = new Point(201, 104);
      this.rbCatapults.Name = "rbCatapults";
      this.rbCatapults.Size = new Size(69, 17);
      this.rbCatapults.TabIndex = 19;
      this.rbCatapults.Text = "Catapults";
      this.rbCatapults.UseVisualStyleBackColor = true;
      this.rbVenison.AutoSize = true;
      this.rbVenison.Location = new Point(298, 12);
      this.rbVenison.Name = "rbVenison";
      this.rbVenison.Size = new Size(63, 17);
      this.rbVenison.TabIndex = 20;
      this.rbVenison.Text = "Venison";
      this.rbVenison.UseVisualStyleBackColor = true;
      this.rbClothes.AutoSize = true;
      this.rbClothes.Location = new Point(298, 35);
      this.rbClothes.Name = "rbClothes";
      this.rbClothes.Size = new Size(60, 17);
      this.rbClothes.TabIndex = 21;
      this.rbClothes.Text = "Clothes";
      this.rbClothes.UseVisualStyleBackColor = true;
      this.rbFurniture.AutoSize = true;
      this.rbFurniture.Location = new Point(298, 58);
      this.rbFurniture.Name = "rbFurniture";
      this.rbFurniture.Size = new Size(66, 17);
      this.rbFurniture.TabIndex = 22;
      this.rbFurniture.Text = "Furniture";
      this.rbFurniture.UseVisualStyleBackColor = true;
      this.rbWine.AutoSize = true;
      this.rbWine.Location = new Point(298, 81);
      this.rbWine.Name = "rbWine";
      this.rbWine.Size = new Size(50, 17);
      this.rbWine.TabIndex = 23;
      this.rbWine.Text = "Wine";
      this.rbWine.UseVisualStyleBackColor = true;
      this.rbSalt.AutoSize = true;
      this.rbSalt.Location = new Point(298, 104);
      this.rbSalt.Name = "rbSalt";
      this.rbSalt.Size = new Size(43, 17);
      this.rbSalt.TabIndex = 24;
      this.rbSalt.Text = "Salt";
      this.rbSalt.UseVisualStyleBackColor = true;
      this.rbMetalware.AutoSize = true;
      this.rbMetalware.Location = new Point(298, (int) sbyte.MaxValue);
      this.rbMetalware.Name = "rbMetalware";
      this.rbMetalware.Size = new Size(74, 17);
      this.rbMetalware.TabIndex = 25;
      this.rbMetalware.Text = "Metalware";
      this.rbMetalware.UseVisualStyleBackColor = true;
      this.rbSpices.AutoSize = true;
      this.rbSpices.Location = new Point(298, 150);
      this.rbSpices.Name = "rbSpices";
      this.rbSpices.Size = new Size(57, 17);
      this.rbSpices.TabIndex = 26;
      this.rbSpices.Text = "Spices";
      this.rbSpices.UseVisualStyleBackColor = true;
      this.rbSilk.AutoSize = true;
      this.rbSilk.Location = new Point(298, 173);
      this.rbSilk.Name = "rbSilk";
      this.rbSilk.Size = new Size(42, 17);
      this.rbSilk.TabIndex = 27;
      this.rbSilk.Text = "Silk";
      this.rbSilk.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(422, 282);
      this.Controls.Add((Control) this.rbSilk);
      this.Controls.Add((Control) this.rbSpices);
      this.Controls.Add((Control) this.rbMetalware);
      this.Controls.Add((Control) this.rbSalt);
      this.Controls.Add((Control) this.rbWine);
      this.Controls.Add((Control) this.rbFurniture);
      this.Controls.Add((Control) this.rbClothes);
      this.Controls.Add((Control) this.rbVenison);
      this.Controls.Add((Control) this.rbCatapults);
      this.Controls.Add((Control) this.rbArmour);
      this.Controls.Add((Control) this.rbSwords);
      this.Controls.Add((Control) this.rbPikes);
      this.Controls.Add((Control) this.rbBows);
      this.Controls.Add((Control) this.rbAle);
      this.Controls.Add((Control) this.rbFish);
      this.Controls.Add((Control) this.rbVeg);
      this.Controls.Add((Control) this.rbBread);
      this.Controls.Add((Control) this.rbCheese);
      this.Controls.Add((Control) this.rbMeat);
      this.Controls.Add((Control) this.rbApples);
      this.Controls.Add((Control) this.rbPitch);
      this.Controls.Add((Control) this.rbIron);
      this.Controls.Add((Control) this.rbStone);
      this.Controls.Add((Control) this.rbWood);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.tbAmount);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnGiveResources);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (AGUR);
      this.Text = "Give Resources";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
