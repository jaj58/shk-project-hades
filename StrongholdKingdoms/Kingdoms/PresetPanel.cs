// Decompiled with JetBrains decompiler
// Type: Kingdoms.PresetPanel
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
  public class PresetPanel : CustomSelfDrawPanel
  {
    private PresetPopup m_parent;
    private PresetType m_type;
    private CustomSelfDrawPanel.CSDLabel existingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel loadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private PresetLine m_selectedLine;
    private CustomSelfDrawPanel.CSDArea presetsScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDVertScrollBar presetsScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDExtendingPanel insetImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private int mouseWheelDelta = 40;
    private CustomSelfDrawPanel.CSDArea presetInfoArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDButton uploadButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton importButton = new CustomSelfDrawPanel.CSDButton();
    private PresetEditPopup m_renamePopup;
    private bool showScrollbar = true;

    public PresetPanel()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(PresetPopup parent, PresetType type)
    {
      this.m_parent = parent;
      this.m_type = type;
      this.Size = this.m_parent.Size;
      this.BackColor = ARGBColors.Transparent;
      CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage();
      control.Alpha = 0.1f;
      control.Scale = 5.0;
      control.Position = new Point(0, 0);
      control.Size = this.Size;
      this.addControl((CustomSelfDrawPanel.CSDControl) control);
      this.insetImage.Size = new Size(this.Width - 40, this.Height - 140);
      this.insetImage.Position = new Point(20, 40);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.insetImage);
      this.insetImage.Create((Image) GFXLibrary.quest_9sclice_grey_inset_top_left, (Image) GFXLibrary.quest_9sclice_grey_inset_top_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_top_right, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_left, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_right, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_left, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_right);
      this.presetsScrollArea.Position = new Point(this.insetImage.X + 10, this.insetImage.Y + 10);
      this.presetsScrollArea.Size = new Size(this.insetImage.Width - 40, this.insetImage.Height - 20);
      this.presetsScrollArea.ClipRect = new Rectangle(new Point(0, 0), this.presetsScrollArea.Size);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.presetsScrollArea);
      int num = this.presetsScrollBar.Value;
      this.presetsScrollBar.Size = new Size(24, this.presetsScrollArea.Height + 3);
      this.presetsScrollBar.Position = new Point(this.insetImage.Position.X + this.insetImage.Width - (this.insetImage.Width - this.presetsScrollArea.Width) / 2 - this.presetsScrollBar.Width / 2, this.insetImage.Position.Y + 10);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.presetsScrollBar);
      this.presetsScrollBar.Value = 0;
      this.presetsScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.presetsScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.presetsScrollBarMoved));
      this.mouseWheelOverlay.Position = this.presetsScrollArea.Position;
      this.mouseWheelOverlay.Size = this.presetsScrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.onMouseWheelMoved));
      control.addControl(this.mouseWheelOverlay);
      this.presetInfoArea.Position = new Point(this.insetImage.X + 10, this.insetImage.Y + 10);
      this.presetInfoArea.Size = new Size(this.insetImage.Width - 40, this.insetImage.Height - 20);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.presetInfoArea);
      this.presetInfoArea.Visible = false;
      this.existingLabel.Text = SK.Text("Preset_Saved_Title", "Saved Presets");
      this.existingLabel.Color = ARGBColors.Black;
      this.existingLabel.Size = new Size(this.insetImage.Width, 20);
      this.existingLabel.Position = new Point(this.insetImage.X, this.insetImage.Y - this.existingLabel.Height - 2);
      this.existingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.existingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.existingLabel);
      PresetManager.Instance.GenerateFromMap(this.m_type);
      this.uploadButton.ImageNorm = (Image) GFXLibrary.preset_upload;
      this.uploadButton.ImageOver = (Image) GFXLibrary.preset_upload_over;
      this.uploadButton.ImageClick = (Image) GFXLibrary.preset_upload_down;
      this.uploadButton.setSizeToImage();
      this.uploadButton.Position = new Point(this.Width / 2 - this.uploadButton.Width / 2, this.insetImage.Rectangle.Bottom + 10);
      this.uploadButton.Text.Text = SK.Text("Preset_Upload", "Save to Cloud");
      this.uploadButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.uploadButton.TextYOffset = -2;
      this.uploadButton.Text.Color = ARGBColors.Black;
      this.uploadButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.uploadClick));
      this.uploadButton.Enabled = PresetManager.Instance.LocalChangesAvailable;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.uploadButton);
      this.loadingLabel.Text = SK.Text("Preset_Loading_data", "Retrieving Cloud Data");
      this.loadingLabel.Color = ARGBColors.White;
      this.loadingLabel.Size = new Size(this.insetImage.Width, 20);
      this.loadingLabel.Position = new Point(this.insetImage.X, (this.insetImage.Y + this.insetImage.Rectangle.Bottom) / 2);
      this.loadingLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.loadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.loadingLabel);
      this.importButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.importButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.importButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.importButton.setSizeToImage();
      this.importButton.Position = new Point((this.uploadButton.Rectangle.Right + this.Width) / 2 - this.importButton.Width / 2, this.uploadButton.Y + this.uploadButton.Height / 2 - this.importButton.Height / 2);
      this.importButton.Text.Text = SK.Text("Preset_Import", "Import Formations");
      this.importButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.importButton.TextYOffset = -2;
      this.importButton.Text.Color = ARGBColors.Black;
      this.importButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.importClick));
      this.importButton.Enabled = true;
      if (PresetManager.Instance.IsDataReady)
      {
        this.processImport();
        this.initLines();
        this.uploadButton.Visible = true;
        this.importButton.Visible = PresetManager.Instance.ShowPresetImport && !PresetManager.Instance.ImportComplete;
        this.loadingLabel.Visible = false;
      }
      else
      {
        this.uploadButton.Visible = false;
        this.importButton.Visible = false;
        this.loadingLabel.Visible = true;
      }
      this.Invalidate();
    }

    public void initLines()
    {
      this.presetsScrollArea.Controls.Clear();
      if (PresetManager.Instance.LocalChangesAvailable)
        this.uploadButton.Enabled = true;
      PresetLine line = (PresetLine) null;
      int y = 0;
      int highestAvailableSlot = PresetManager.Instance.GetHighestAvailableSlot(this.m_type);
      for (int index = 0; index < highestAvailableSlot; ++index)
      {
        if (y > 0)
          y += 5;
        PresetLine control = new PresetLine();
        control.init(this, index + 1, this.m_type);
        control.Position = new Point(0, y);
        this.presetsScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        y += control.Height;
        if (index == 0)
        {
          this.mouseWheelDelta = control.Height + 5;
          line = control;
        }
      }
      if (y < this.presetsScrollArea.Height)
      {
        this.presetsScrollBar.Visible = false;
      }
      else
      {
        this.presetsScrollBar.Visible = true;
        this.presetsScrollBar.NumVisibleLines = this.presetsScrollBar.Height;
        this.presetsScrollBar.Max = y - this.presetsScrollBar.Height;
        this.presetsScrollBar.invalidateXtra();
      }
      this.SetSelectedLine(line);
    }

    public void processImport()
    {
    }

    public void onClose()
    {
      if (GameEngine.Instance.GameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_PREVIEW)
      {
        GameEngine.Instance.villageTabChange(1);
        InterfaceMgr.Instance.initCastleTab();
      }
      this.closeRenamePopup();
    }

    private void onMouseWheelMoved(int delta)
    {
      if (delta < 0)
      {
        this.presetsScrollBar.scrollDown(this.mouseWheelDelta);
      }
      else
      {
        if (delta <= 0)
          return;
        this.presetsScrollBar.scrollUp(this.mouseWheelDelta);
      }
    }

    private void presetsScrollBarMoved()
    {
      int y = this.presetsScrollBar.Value;
      this.presetsScrollArea.Position = new Point(this.presetsScrollArea.X, this.insetImage.Y + 10 - y);
      this.presetsScrollArea.ClipRect = new Rectangle(this.presetsScrollArea.ClipRect.X, y, this.presetsScrollArea.ClipRect.Width, this.presetsScrollArea.ClipRect.Height);
      this.presetsScrollArea.invalidate();
      this.presetsScrollBar.invalidate();
      this.insetImage.invalidate();
    }

    public void SetSelectedLine(PresetLine line)
    {
      if (this.m_selectedLine != null)
        this.m_selectedLine.onDeselect();
      this.m_selectedLine = line;
      if (this.m_selectedLine == null)
        return;
      this.m_selectedLine.onSelect();
    }

    private void uploadClick()
    {
      this.uploadButton.Enabled = false;
      PresetManager.Instance.SendPresetsToServer(this);
      foreach (CustomSelfDrawPanel.CSDControl control in this.presetsScrollArea.Controls)
        control.Enabled = false;
    }

    public void onLogout()
    {
    }

    private void importClick()
    {
      int legacyCount = PresetManager.Instance.getLegacyCount(this.m_type);
      if (legacyCount <= PresetManager.Instance.GetFreeSlotCount(this.m_type))
      {
        if (PresetManager.Instance.transferLegacy(this.m_type))
        {
          int num = (int) MyMessageBox.Show(SK.Text("Presets_Import_Success", "Formations successfully imported"));
          PresetManager.Instance.ShowPresetImport = false;
          PresetManager.Instance.ImportComplete = true;
          this.importButton.Visible = false;
          this.initLines();
          this.Invalidate();
        }
        else
        {
          int num1 = (int) MyMessageBox.Show(SK.Text("Presets_Import_Failure", "Formation import failed"));
        }
      }
      else
      {
        if (MyMessageBox.Show(SK.Text("Presets_Confirm_Delete1", "The number of saved formations") + " (" + legacyCount.ToString() + ") " + SK.Text("Presets_Confirm_Delete2", "exceeds the number of available slots") + " (" + PresetManager.Instance.GetFreeSlotCount(this.m_type).ToString() + ") " + Environment.NewLine + Environment.NewLine + SK.Text("Presets_Confirm_Delete3", "You must delete saved formations before importing. Do this now?"), "", MessageBoxButtons.YesNo) != DialogResult.Yes)
          return;
        InterfaceMgr.Instance.closePresetPopup();
        InterfaceMgr.Instance.openFormationPopup();
      }
    }

    private void debugClick() => InterfaceMgr.Instance.openFormationPopup();

    public void onRename(PresetLine line)
    {
      if (this.OverControl == null)
        return;
      int data = this.OverControl.Data;
      this.closeRenamePopup();
      this.m_renamePopup = new PresetEditPopup();
      this.m_renamePopup.init(line);
      this.m_renamePopup.Show();
    }

    public void closeRenamePopup()
    {
      if (this.m_renamePopup == null)
        return;
      if (this.m_renamePopup.Created)
        this.m_renamePopup.Close();
      this.m_renamePopup = (PresetEditPopup) null;
      if (!PresetManager.Instance.LocalChangesAvailable)
        return;
      this.uploadButton.Enabled = true;
    }

    public void onServerResponse(bool success)
    {
      if (success)
      {
        this.uploadButton.Visible = true;
        this.loadingLabel.Visible = false;
        this.processImport();
        if (PresetManager.Instance.ShowPresetImport)
          this.importButton.Visible = true;
        this.initLines();
        this.Invalidate();
      }
      else
        this.loadingLabel.Text = SK.Text("Preset_Cannot_Load", "Unable to Access Cloud Data");
    }

    public void onUploadComplete(bool success)
    {
      foreach (CustomSelfDrawPanel.CSDControl control in this.presetsScrollArea.Controls)
        control.Enabled = true;
      if (!success)
      {
        int num = (int) MyMessageBox.Show(SK.Text("Preset_Upload_Failed", "Failed to Upload Data to cloud"));
        this.uploadButton.Enabled = true;
      }
      else
      {
        int num = (int) MyMessageBox.Show(SK.Text("Preset_Upload_Complete", "Presets saved to the cloud"));
        this.uploadButton.Enabled = false;
      }
    }

    public void showPresetInfo(CastleMapPreset preset)
    {
      this.presetsScrollArea.Visible = false;
      this.showScrollbar = this.presetsScrollBar.Visible;
      this.presetsScrollBar.Visible = false;
      this.presetInfoArea.Visible = true;
      this.presetInfoArea.clearControls();
      BasePreviewPanel control = (BasePreviewPanel) null;
      switch (preset.Type)
      {
        case PresetType.TROOP_ATTACK:
          control = (BasePreviewPanel) new AttackTroopPreviewPanel();
          break;
        case PresetType.TROOP_DEFEND:
          control = (BasePreviewPanel) new DefenseTroopPreviewPanel();
          break;
        case PresetType.INFRASTRUCTURE:
          control = (BasePreviewPanel) new CastlePreviewPanel();
          break;
      }
      control.Position = new Point(5, 5);
      control.init(preset, (CustomSelfDrawPanel.CSDControl) this.insetImage);
      control.CloseCallback = new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.hidePresetInfo);
      this.presetInfoArea.addControl((CustomSelfDrawPanel.CSDControl) control);
      this.Invalidate();
    }

    private void hidePresetInfo()
    {
      this.presetsScrollArea.Visible = true;
      this.presetsScrollBar.Visible = this.showScrollbar;
      this.presetInfoArea.Visible = false;
      this.Invalidate();
    }
  }
}
