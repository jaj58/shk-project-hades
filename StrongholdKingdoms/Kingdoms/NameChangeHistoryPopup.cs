// Decompiled with JetBrains decompiler
// Type: Kingdoms.NameChangeHistoryPopup
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
  public class NameChangeHistoryPopup : MyFormBase
  {
    private IContainer components;
    private ListBox listBox1;
    private BitmapButton btnOK;

    public NameChangeHistoryPopup() => this.InitializeComponent();

    public void importData(string[] names, int parishID)
    {
      this.Text = this.Title = "Parish Name History : " + parishID.ToString();
      for (int index = 0; index < names.Length; index += 2)
        this.listBox1.Items.Add((object) new NameChangeHistoryPopup.HistoryItem()
        {
          name = names[index],
          userguid = names[index + 1]
        });
    }

    private void btnOK_Click(object sender, EventArgs e) => this.Close();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.listBox1 = new ListBox();
      this.btnOK = new BitmapButton();
      this.SuspendLayout();
      this.listBox1.Font = new Font("Lucida Console", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.listBox1.FormattingEnabled = true;
      this.listBox1.ItemHeight = 11;
      this.listBox1.Location = new Point(14, 53);
      this.listBox1.Name = "listBox1";
      this.listBox1.Size = new Size(680, 290);
      this.listBox1.TabIndex = 13;
      this.btnOK.BackColor = Color.FromArgb(203, 215, 223);
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
      this.btnOK.Location = new Point(615, 369);
      this.btnOK.Name = "btnOK";
      this.btnOK.OffsetPressedContent = true;
      this.btnOK.Padding2 = 5;
      this.btnOK.Size = new Size(79, 20);
      this.btnOK.StretchImage = false;
      this.btnOK.TabIndex = 14;
      this.btnOK.Text = "OK";
      this.btnOK.TextDropShadow = false;
      this.btnOK.UseVisualStyleBackColor = false;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(706, 401);
      this.Controls.Add((Control) this.btnOK);
      this.Controls.Add((Control) this.listBox1);
      this.Name = nameof (NameChangeHistoryPopup);
      this.ShowClose = true;
      this.Text = nameof (NameChangeHistoryPopup);
      this.Controls.SetChildIndex((Control) this.listBox1, 0);
      this.Controls.SetChildIndex((Control) this.btnOK, 0);
      this.ResumeLayout(false);
    }

    private class HistoryItem
    {
      public string name = "";
      public string userguid = "";

      public override string ToString()
      {
        return string.Format("{0,-50}{1, -32}", (object) this.name, (object) this.userguid);
      }
    }
  }
}
