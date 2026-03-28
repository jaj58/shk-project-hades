// Decompiled with JetBrains decompiler
// Type: Kingdoms.SearchForVillagePopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Kingdoms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class SearchForVillagePopup : MyFormBase
  {
    private IContainer components;
    private Label lblSearchByName;
    private TextBox tbSearchName;
    private BitmapButton btnCancel;
    private Label label3;
    private TextBox tbVillageID;
    private Label lblSearchByID;
    private ListBox listBoxVillages;
    private BitmapButton btnSearchByName;
    private BitmapButton btnSearchByID;

    public SearchForVillagePopup()
    {
      this.InitializeComponent();
      this.lblSearchByID.Text = SK.Text("SearchForVillagePopup_search_by_ID", "Search By Village ID");
      this.lblSearchByName.Text = SK.Text("SearchForVillagePopup_search_by_Name", "Search By Village Name");
      this.btnCancel.Text = SK.Text("GENERIC_Close", "Close");
      this.Text = this.Title = SK.Text("SearchForVillagePopup_for_village", "Search For Village");
      this.btnSearchByID.Text = SK.Text("MailUserPopup_Search", "Search");
      this.btnSearchByName.Text = SK.Text("MailUserPopup_Search", "Search");
      if (!Program.mySettings.viewVillageIDs)
      {
        this.lblSearchByID.Visible = false;
        this.btnSearchByID.Visible = false;
        this.tbVillageID.Visible = false;
      }
      this.btnSearchByName.Enabled = false;
      this.btnSearchByID.Enabled = false;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("RenameVillagePopup_cancel");
      this.Close();
    }

    private void tbNewName_TextChanged(object sender, EventArgs e)
    {
      if (this.tbSearchName.Text.Length > 0)
        this.btnSearchByName.Enabled = true;
      else
        this.btnSearchByName.Enabled = false;
    }

    private void btnSearchByName_Click(object sender, EventArgs e)
    {
      if (this.tbSearchName.Text.Length <= 0)
        return;
      List<int> intList = GameEngine.Instance.World.searchVillageNames(this.tbSearchName.Text);
      this.listBoxVillages.Items.Clear();
      foreach (int num in intList)
        this.listBoxVillages.Items.Add((object) new SearchForVillagePopup.VillageItem()
        {
          villageID = num
        });
    }

    private void btnSearchByID_Click(object sender, EventArgs e)
    {
      int int32FromString = SearchForVillagePopup.getInt32FromString(this.tbVillageID.Text);
      if (int32FromString < 0)
        return;
      this.listBoxVillages.Items.Clear();
      if (GameEngine.Instance.World.isCapital(int32FromString) && !Program.mySettings.viewCapitalIDs || GameEngine.Instance.World.isSpecial(int32FromString) && !this.aiWorldSpecial(int32FromString) && !SpecialVillageTypes.IS_ROYAL_TOWER(GameEngine.Instance.World.getSpecial(int32FromString)) || !GameEngine.Instance.World.isVillageVisible(int32FromString))
        return;
      this.listBoxVillages.Items.Add((object) new SearchForVillagePopup.VillageItem()
      {
        villageID = int32FromString
      });
    }

    private bool aiWorldSpecial(int villageID)
    {
      if (!GameEngine.Instance.LocalWorldData.AIWorld || !GameEngine.Instance.World.isSpecial(villageID))
        return false;
      switch (GameEngine.Instance.World.getSpecial(villageID))
      {
        case 7:
        case 9:
        case 11:
        case 13:
          return true;
        default:
          return false;
      }
    }

    private void tbVillageID_TextChanged(object sender, EventArgs e)
    {
      if (SearchForVillagePopup.getInt32FromString(this.tbVillageID.Text) >= 0)
        this.btnSearchByID.Enabled = true;
      else
        this.btnSearchByID.Enabled = false;
    }

    public static int getInt32FromString(string text)
    {
      if (text.Length == 0)
        return -1;
      try
      {
        return Convert.ToInt32(text);
      }
      catch (Exception ex)
      {
      }
      return -1;
    }

    private void listBoxVillages_DoubleClick(object sender, EventArgs e)
    {
      if (this.listBoxVillages.SelectedIndex < 0)
        return;
      SearchForVillagePopup.VillageItem selectedItem = (SearchForVillagePopup.VillageItem) this.listBoxVillages.SelectedItem;
      if (selectedItem == null)
        return;
      GameEngine.Instance.World.zoomToVillage(selectedItem.villageID);
      this.Close();
    }

    private void tbSearchName_KeyUp(object sender, KeyEventArgs e)
    {
    }

    private void tbVillageID_KeyUp(object sender, KeyEventArgs e)
    {
    }

    private void tbSearchName_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      this.btnSearchByName_Click(sender, (EventArgs) e);
      e.Handled = true;
    }

    private void tbVillageID_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      this.btnSearchByID_Click(sender, (EventArgs) e);
      e.Handled = true;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lblSearchByName = new Label();
      this.tbSearchName = new TextBox();
      this.btnCancel = new BitmapButton();
      this.label3 = new Label();
      this.tbVillageID = new TextBox();
      this.lblSearchByID = new Label();
      this.listBoxVillages = new ListBox();
      this.btnSearchByName = new BitmapButton();
      this.btnSearchByID = new BitmapButton();
      this.SuspendLayout();
      this.lblSearchByName.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.lblSearchByName.ForeColor = ARGBColors.Black;
      this.lblSearchByName.Location = new Point(12, 41);
      this.lblSearchByName.Name = "lblSearchByName";
      this.lblSearchByName.Size = new Size(140, 40);
      this.lblSearchByName.TabIndex = 11;
      this.lblSearchByName.Text = "Search By Name";
      this.lblSearchByName.TextAlign = ContentAlignment.MiddleLeft;
      this.tbSearchName.BackColor = Color.FromArgb(235, 240, 243);
      this.tbSearchName.ForeColor = ARGBColors.Black;
      this.tbSearchName.Location = new Point(158, 49);
      this.tbSearchName.MaxLength = 32;
      this.tbSearchName.Name = "tbSearchName";
      this.tbSearchName.Size = new Size(155, 20);
      this.tbSearchName.TabIndex = 1;
      this.tbSearchName.TextChanged += new EventHandler(this.tbNewName_TextChanged);
      this.tbSearchName.KeyUp += new KeyEventHandler(this.tbSearchName_KeyUp);
      this.tbSearchName.KeyPress += new KeyPressEventHandler(this.tbSearchName_KeyPress);
      this.btnCancel.BackColor = Color.FromArgb(203, 215, 223);
      this.btnCancel.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnCancel.BorderDrawing = true;
      this.btnCancel.FocusRectangleEnabled = false;
      this.btnCancel.Image = (Image) null;
      this.btnCancel.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnCancel.ImageBorderEnabled = true;
      this.btnCancel.ImageDropShadow = true;
      this.btnCancel.ImageFocused = (Image) null;
      this.btnCancel.ImageInactive = (Image) null;
      this.btnCancel.ImageMouseOver = (Image) null;
      this.btnCancel.ImageNormal = (Image) null;
      this.btnCancel.ImagePressed = (Image) null;
      this.btnCancel.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnCancel.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnCancel.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnCancel.Location = new Point(322, 355);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.OffsetPressedContent = true;
      this.btnCancel.Padding2 = 5;
      this.btnCancel.Size = new Size(122, 32);
      this.btnCancel.StretchImage = false;
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Close";
      this.btnCancel.TextDropShadow = false;
      this.btnCancel.UseVisualStyleBackColor = false;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.label3.AutoSize = true;
      this.label3.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label3.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label3.ForeColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label3.Location = new Point(179, 7);
      this.label3.Name = "label3";
      this.label3.Size = new Size(0, 16);
      this.label3.TabIndex = 9;
      this.tbVillageID.BackColor = Color.FromArgb(235, 240, 243);
      this.tbVillageID.Location = new Point(158, 84);
      this.tbVillageID.MaxLength = 32;
      this.tbVillageID.Name = "tbVillageID";
      this.tbVillageID.Size = new Size(155, 20);
      this.tbVillageID.TabIndex = 13;
      this.tbVillageID.TextChanged += new EventHandler(this.tbVillageID_TextChanged);
      this.tbVillageID.KeyUp += new KeyEventHandler(this.tbVillageID_KeyUp);
      this.tbVillageID.KeyPress += new KeyPressEventHandler(this.tbVillageID_KeyPress);
      this.lblSearchByID.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.lblSearchByID.ForeColor = ARGBColors.Black;
      this.lblSearchByID.Location = new Point(12, 75);
      this.lblSearchByID.Name = "lblSearchByID";
      this.lblSearchByID.Size = new Size(140, 40);
      this.lblSearchByID.TabIndex = 14;
      this.lblSearchByID.Text = "Search By VillageID";
      this.lblSearchByID.TextAlign = ContentAlignment.MiddleLeft;
      this.listBoxVillages.BackColor = ARGBColors.White;
      this.listBoxVillages.ForeColor = ARGBColors.Black;
      this.listBoxVillages.FormattingEnabled = true;
      this.listBoxVillages.Location = new Point(34, 117);
      this.listBoxVillages.Name = "listBoxVillages";
      this.listBoxVillages.Size = new Size(385, 225);
      this.listBoxVillages.TabIndex = 15;
      this.listBoxVillages.DoubleClick += new EventHandler(this.listBoxVillages_DoubleClick);
      this.btnSearchByName.BackColor = Color.FromArgb(203, 215, 223);
      this.btnSearchByName.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnSearchByName.BorderDrawing = true;
      this.btnSearchByName.FocusRectangleEnabled = false;
      this.btnSearchByName.Image = (Image) null;
      this.btnSearchByName.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnSearchByName.ImageBorderEnabled = true;
      this.btnSearchByName.ImageDropShadow = true;
      this.btnSearchByName.ImageFocused = (Image) null;
      this.btnSearchByName.ImageInactive = (Image) null;
      this.btnSearchByName.ImageMouseOver = (Image) null;
      this.btnSearchByName.ImageNormal = (Image) null;
      this.btnSearchByName.ImagePressed = (Image) null;
      this.btnSearchByName.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnSearchByName.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnSearchByName.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnSearchByName.Location = new Point(331, 49);
      this.btnSearchByName.Name = "btnSearchByName";
      this.btnSearchByName.OffsetPressedContent = true;
      this.btnSearchByName.Padding2 = 5;
      this.btnSearchByName.Size = new Size(113, 21);
      this.btnSearchByName.StretchImage = false;
      this.btnSearchByName.TabIndex = 16;
      this.btnSearchByName.Text = "Search";
      this.btnSearchByName.TextDropShadow = false;
      this.btnSearchByName.UseVisualStyleBackColor = false;
      this.btnSearchByName.Click += new EventHandler(this.btnSearchByName_Click);
      this.btnSearchByID.BackColor = Color.FromArgb(203, 215, 223);
      this.btnSearchByID.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnSearchByID.BorderDrawing = true;
      this.btnSearchByID.FocusRectangleEnabled = false;
      this.btnSearchByID.Image = (Image) null;
      this.btnSearchByID.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnSearchByID.ImageBorderEnabled = true;
      this.btnSearchByID.ImageDropShadow = true;
      this.btnSearchByID.ImageFocused = (Image) null;
      this.btnSearchByID.ImageInactive = (Image) null;
      this.btnSearchByID.ImageMouseOver = (Image) null;
      this.btnSearchByID.ImageNormal = (Image) null;
      this.btnSearchByID.ImagePressed = (Image) null;
      this.btnSearchByID.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnSearchByID.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnSearchByID.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnSearchByID.Location = new Point(331, 83);
      this.btnSearchByID.Name = "btnSearchByID";
      this.btnSearchByID.OffsetPressedContent = true;
      this.btnSearchByID.Padding2 = 5;
      this.btnSearchByID.Size = new Size(113, 21);
      this.btnSearchByID.StretchImage = false;
      this.btnSearchByID.TabIndex = 17;
      this.btnSearchByID.Text = "Search";
      this.btnSearchByID.TextDropShadow = false;
      this.btnSearchByID.UseVisualStyleBackColor = false;
      this.btnSearchByID.Click += new EventHandler(this.btnSearchByID_Click);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(159, 180, 193);
      this.ClientSize = new Size(456 * InterfaceMgr.UIScale, 399 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.btnSearchByID);
      this.Controls.Add((Control) this.btnSearchByName);
      this.Controls.Add((Control) this.listBoxVillages);
      this.Controls.Add((Control) this.tbVillageID);
      this.Controls.Add((Control) this.lblSearchByID);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.tbSearchName);
      this.Controls.Add((Control) this.lblSearchByName);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (SearchForVillagePopup);
      this.ShowBar = true;
      this.ShowClose = true;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Controls.SetChildIndex((Control) this.lblSearchByName, 0);
      this.Controls.SetChildIndex((Control) this.tbSearchName, 0);
      this.Controls.SetChildIndex((Control) this.btnCancel, 0);
      this.Controls.SetChildIndex((Control) this.lblSearchByID, 0);
      this.Controls.SetChildIndex((Control) this.tbVillageID, 0);
      this.Controls.SetChildIndex((Control) this.listBoxVillages, 0);
      this.Controls.SetChildIndex((Control) this.btnSearchByName, 0);
      this.Controls.SetChildIndex((Control) this.btnSearchByID, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private class VillageItem
    {
      public int villageID = -1;

      public override string ToString()
      {
        return GameEngine.Instance.World.getVillageNameOrType(this.villageID);
      }
    }
  }
}
