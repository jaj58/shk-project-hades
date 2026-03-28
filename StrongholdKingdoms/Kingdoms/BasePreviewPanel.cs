// Decompiled with JetBrains decompiler
// Type: Kingdoms.BasePreviewPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class BasePreviewPanel : CustomSelfDrawPanel.CSDControl
  {
    protected CastleMapPreset m_preset;
    public CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate CloseCallback;

    public void init(CastleMapPreset preset, CustomSelfDrawPanel.CSDControl parentControl)
    {
      this.m_preset = preset;
      CastleMap.PopulateBasicInfo(preset);
      this.Size = new Size(parentControl.Width - 20, parentControl.Height - 20);
      CustomSelfDrawPanel.CSDButton control1 = new CustomSelfDrawPanel.CSDButton();
      control1.ImageNorm = (Image) GFXLibrary.preset_list;
      control1.ImageOver = (Image) GFXLibrary.preset_list_over;
      control1.ImageClick = (Image) GFXLibrary.preset_list_down;
      control1.setSizeToImage();
      control1.Position = new Point(this.Width - control1.Width, -5);
      control1.CustomTooltipID = 229;
      control1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick));
      this.addControl((CustomSelfDrawPanel.CSDControl) control1);
      CustomSelfDrawPanel.CSDButton control2 = new CustomSelfDrawPanel.CSDButton();
      control2.ImageNorm = (Image) GFXLibrary.preset_castle_in;
      control2.ImageOver = (Image) GFXLibrary.preset_castle_in_over;
      control2.ImageClick = (Image) GFXLibrary.preset_castle_in_down;
      control2.setSizeToImage();
      control2.Position = new Point(control1.X - control2.Width - 5, control1.Y - 5);
      control2.CustomTooltipID = 225;
      control2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deployClick));
      this.addControl((CustomSelfDrawPanel.CSDControl) control2);
      CustomSelfDrawPanel.CSDButton control3 = new CustomSelfDrawPanel.CSDButton();
      control3.ImageNorm = (Image) GFXLibrary.preset_info;
      control3.ImageOver = (Image) GFXLibrary.preset_info_over;
      control3.ImageClick = (Image) GFXLibrary.preset_info_down;
      control3.setSizeToImage();
      control3.Position = new Point(control2.X - control3.Width - 5, control1.Y);
      control3.CustomTooltipID = 240;
      control3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.previewClick));
      if (this.m_preset.Type != PresetType.TROOP_ATTACK)
        this.addControl((CustomSelfDrawPanel.CSDControl) control3);
      CustomSelfDrawPanel.CSDLabel control4 = new CustomSelfDrawPanel.CSDLabel();
      control4.Text = preset.Name;
      control4.Color = ARGBColors.Black;
      control4.Position = new Point(5, control1.Y);
      control4.Size = new Size(this.Width * 2 / 4, control1.Height);
      control4.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      control4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.addControl((CustomSelfDrawPanel.CSDControl) control4);
      this.populateRequirements();
      control2.Enabled = preset.CanDeploy();
      this.previewClick();
    }

    private void closeClick()
    {
      if (GameEngine.Instance.GameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_PREVIEW)
      {
        GameEngine.Instance.villageTabChange(1);
        InterfaceMgr.Instance.initCastleTab();
      }
      this.parent.removeControl((CustomSelfDrawPanel.CSDControl) this);
      if (this.CloseCallback == null)
        return;
      this.CloseCallback();
    }

    private void deployClick()
    {
      if (this.m_preset == null)
        return;
      if (GameEngine.Instance.GameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_PREVIEW)
      {
        GameEngine.Instance.villageTabChange(1);
        InterfaceMgr.Instance.initCastleTab();
      }
      PresetManager.Instance.DeployToMap(this.m_preset);
      PresetManager.Instance.GenerateFromMap(this.m_preset.Type);
    }

    protected virtual void previewClick()
    {
    }

    protected virtual void populateRequirements()
    {
    }
  }
}
