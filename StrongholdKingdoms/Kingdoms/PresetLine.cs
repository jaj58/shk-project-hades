// Decompiled with JetBrains decompiler
// Type: Kingdoms.PresetLine
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PresetLine : CustomSelfDrawPanel.CSDControl
  {
    private CastleMapPreset m_preset;
    private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton deployButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton memoriseButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton renameButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton deleteButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton infoButton = new CustomSelfDrawPanel.CSDButton();
    private PresetPanel m_parent;
    private bool selected;
    private int m_slotID;

    public CastleMapPreset GetPreset() => this.m_preset;

    public void init(PresetPanel parent, int slotID, PresetType type)
    {
      this.m_parent = parent;
      this.m_preset = PresetManager.Instance.GetPreset(type, slotID);
      bool flag1 = false;
      if (this.m_preset == null)
      {
        flag1 = true;
        this.m_preset = new CastleMapPreset("", DateTime.Now, type, 0);
        this.m_preset.SlotID = slotID;
        this.deployButton.Visible = false;
        this.deleteButton.Visible = false;
        this.renameButton.Visible = false;
        this.infoButton.Visible = false;
      }
      this.m_slotID = slotID;
      this.clearControls();
      this.backgroundImage.Image = (Image) GFXLibrary.quest_screen_bar1;
      this.backgroundImage.Position = new Point(0, 0);
      this.backgroundImage.Size = new Size(this.m_parent.Width - 100, this.backgroundImage.Height * 4 / 3);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
      this.Size = new Size(this.m_parent.Width - 80, this.backgroundImage.Height);
      this.nameLabel.Text = this.m_preset.Name;
      this.nameLabel.Color = ARGBColors.Black;
      this.nameLabel.Position = new Point(5, 0);
      this.nameLabel.Size = new Size(this.backgroundImage.Width / 2, this.backgroundImage.Height);
      this.nameLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.nameLabel);
      this.deployButton.ImageNorm = (Image) GFXLibrary.preset_castle_in;
      this.deployButton.ImageOver = (Image) GFXLibrary.preset_castle_in_over;
      this.deployButton.ImageClick = (Image) GFXLibrary.preset_castle_in_down;
      this.deployButton.setSizeToImage();
      this.deployButton.Size = new Size(this.deployButton.Width * 3 / 4, this.deployButton.Height * 3 / 4);
      this.deployButton.Position = new Point(this.Width * 3 / 5, this.Height / 2 - this.deployButton.Height / 2);
      this.deployButton.CustomTooltipID = 225;
      this.deployButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deployClick));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.deployButton);
      this.memoriseButton.ImageNorm = (Image) GFXLibrary.preset_castle_out;
      this.memoriseButton.ImageOver = (Image) GFXLibrary.preset_castle_out_over;
      this.memoriseButton.ImageClick = (Image) GFXLibrary.preset_castle_out_down;
      this.memoriseButton.setSizeToImage();
      if (flag1)
      {
        this.memoriseButton.setSizeToImage();
        this.memoriseButton.Position = new Point(10, this.Height / 2 - this.memoriseButton.Height / 2);
      }
      else
      {
        this.memoriseButton.Size = new Size(this.memoriseButton.Width * 3 / 4, this.memoriseButton.Height * 3 / 4);
        this.memoriseButton.Position = new Point(this.deployButton.Rectangle.Right, this.deployButton.Y);
      }
      this.memoriseButton.CustomTooltipID = 224;
      this.memoriseButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.memoriseClick));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.memoriseButton);
      int right1 = this.memoriseButton.Rectangle.Right;
      this.infoButton.ImageNorm = (Image) GFXLibrary.preset_info;
      this.infoButton.ImageOver = (Image) GFXLibrary.preset_info_over;
      this.infoButton.ImageClick = (Image) GFXLibrary.preset_info_down;
      this.infoButton.setSizeToImage();
      this.infoButton.Size = new Size(this.infoButton.Width * 2 / 3, this.infoButton.Height * 2 / 3);
      this.infoButton.Position = new Point(this.memoriseButton.Rectangle.Right + 5, this.Height / 2 - this.infoButton.Height / 2);
      this.infoButton.CustomTooltipID = 228;
      this.infoButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoClick));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.infoButton);
      int right2 = this.infoButton.Rectangle.Right;
      this.renameButton.ImageNorm = (Image) GFXLibrary.preset_rename;
      this.renameButton.ImageOver = (Image) GFXLibrary.preset_rename_over;
      this.renameButton.ImageClick = (Image) GFXLibrary.preset_rename_down;
      this.renameButton.setSizeToImage();
      this.renameButton.Size = new Size(this.renameButton.Width * 3 / 4, this.renameButton.Height * 3 / 4);
      this.renameButton.Position = new Point(right2, this.memoriseButton.Y);
      this.renameButton.CustomTooltipID = 226;
      this.renameButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.renameClick));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.renameButton);
      this.deleteButton.ImageNorm = (Image) GFXLibrary.preset_delete;
      this.deleteButton.ImageOver = (Image) GFXLibrary.preset_delete_over;
      this.deleteButton.ImageClick = (Image) GFXLibrary.preset_delete_down;
      this.deleteButton.setSizeToImage();
      this.deleteButton.Size = new Size(this.deleteButton.Width * 3 / 4, this.deleteButton.Height * 3 / 4);
      this.deleteButton.Position = new Point(this.renameButton.Rectangle.Right + 2, this.Height / 2 - this.deleteButton.Height / 2);
      this.deleteButton.CustomTooltipID = 227;
      this.deleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteClick));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.deleteButton);
      bool flag2 = true;
      int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
      if (GameEngine.Instance.World.isCapital(selectedMenuVillage) && !GameEngine.Instance.World.isUserVillage(selectedMenuVillage))
        flag2 = false;
      this.deployButton.Enabled = flag2 && this.m_preset.CanDeploy();
      this.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.onClick));
    }

    public void onSelect()
    {
      this.selected = true;
      this.backgroundImage.Image = (Image) GFXLibrary.quest_screen_bar2;
      this.backgroundImage.Size = new Size(this.m_parent.Width - 100, this.backgroundImage.Height * 4 / 3);
      this.invalidate();
    }

    public bool onDeselect()
    {
      this.selected = false;
      this.backgroundImage.Image = (Image) GFXLibrary.quest_screen_bar1;
      this.backgroundImage.Size = new Size(this.m_parent.Width - 100, this.backgroundImage.Height * 4 / 3);
      this.invalidate();
      return true;
    }

    private void onClick()
    {
      if (this.selected)
        return;
      this.m_parent.SetSelectedLine(this);
    }

    private void onDelete()
    {
      this.m_preset = new CastleMapPreset("", DateTime.Now, this.m_preset.Type, 0);
      this.nameLabel.Text = "";
      this.deleteButton.Visible = false;
      this.deployButton.Visible = false;
      this.renameButton.Visible = false;
      this.infoButton.Visible = false;
      this.m_parent.initLines();
    }

    public void onNameChange(string newName)
    {
      if (newName.Equals(this.m_preset.Name))
        return;
      this.nameLabel.Text = newName;
      this.nameLabel.invalidate();
      this.m_preset.Name = newName;
      int num = (int) PresetManager.Instance.UpdatePreset(this.m_preset);
      this.m_parent.initLines();
      this.invalidate();
    }

    public string GetName() => this.nameLabel.Text;

    private void deployClick()
    {
      this.onClick();
      PresetManager.Instance.DeployToMap(this.m_preset);
      PresetManager.Instance.GenerateFromMap(this.m_preset.Type);
    }

    private void memoriseClick()
    {
      if (PresetManager.Instance.CurrentElementCount() == 0)
        return;
      this.onClick();
      PresetManager.Instance.CopyCurrentToPreset(this.m_preset);
      if (this.m_preset.ElementCount <= 0)
        return;
      this.deployButton.Visible = true;
      this.deleteButton.Visible = true;
      this.renameButton.Visible = true;
      this.infoButton.Visible = true;
      if (this.m_preset.Name.Trim().Length == 0)
        this.m_parent.onRename(this);
      int num = (int) PresetManager.Instance.UpdatePreset(this.m_preset);
      this.m_parent.initLines();
    }

    private void renameClick()
    {
      this.onClick();
      this.m_parent.onRename(this);
    }

    private void deleteClick()
    {
      this.onClick();
      if (MyMessageBox.Show(SK.Text("Preset_Delete_Warning", "Are you sure you want to delete this preset?"), SK.Text("Preset_Delete_Title", "Delete Preset"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      int num = (int) PresetManager.Instance.DeletePreset(this.m_preset.Type, this.m_preset.SlotID);
      this.onDelete();
    }

    private void infoClick() => this.m_parent.showPresetInfo(this.m_preset);
  }
}
