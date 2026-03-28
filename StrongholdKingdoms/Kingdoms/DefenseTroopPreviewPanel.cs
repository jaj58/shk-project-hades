// Decompiled with JetBrains decompiler
// Type: Kingdoms.DefenseTroopPreviewPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class DefenseTroopPreviewPanel : BasePreviewPanel
  {
    protected override void previewClick()
    {
      if (GameEngine.Instance.GameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_DEFAULT)
      {
        GameEngine.Instance.InitCastlePreview(this.m_preset);
      }
      else
      {
        GameEngine.Instance.villageTabChange(1);
        InterfaceMgr.Instance.initCastleTab();
      }
    }

    protected override void populateRequirements() => this.populateTroopCounts();

    private void populateTroopCounts()
    {
      CustomSelfDrawPanel.CSDExtendingPanel control1 = new CustomSelfDrawPanel.CSDExtendingPanel();
      control1.Size = new Size(this.Width - 40, this.Height / 2);
      control1.Position = new Point(20, this.Height / 4);
      control1.Create((Image) GFXLibrary.quest_9sclice_grey_inset_top_left, (Image) GFXLibrary.quest_9sclice_grey_inset_top_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_top_right, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_left, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_right, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_left, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_right);
      this.addControl((CustomSelfDrawPanel.CSDControl) control1);
      CustomSelfDrawPanel.CSDLabel control2 = new CustomSelfDrawPanel.CSDLabel();
      control2.Text = SK.Text("GENERIC_Contents", "Contents");
      control2.Color = ARGBColors.Black;
      control2.Size = new Size(control1.Width, 30);
      control2.Position = new Point(control1.X, control1.Y - control2.Height);
      control2.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      control2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.addControl((CustomSelfDrawPanel.CSDControl) control2);
      int elementTotal1 = this.m_preset.GetElementTotal((byte) 70);
      int elementTotal2 = this.m_preset.GetElementTotal((byte) 72);
      int elementTotal3 = this.m_preset.GetElementTotal((byte) 73);
      int elementTotal4 = this.m_preset.GetElementTotal((byte) 71);
      int rangeTotal = this.m_preset.GetRangeTotal((byte) 100, (byte) 109);
      TroopCount placeableDefenders = GameEngine.Instance.Castle.getRemainingPlaceableDefenders();
      CustomSelfDrawPanel.CSDImage control3 = new CustomSelfDrawPanel.CSDImage();
      control3.Image = (Image) (elementTotal1 <= placeableDefenders.peasants ? GFXLibrary.preset_req_peasant : GFXLibrary.preset_req_peasant_red);
      control3.setSizeToImage();
      control3.Position = new Point(control1.Width / 5 - control3.Width, control1.Height / 4 - control3.Height / 2);
      control1.addControl((CustomSelfDrawPanel.CSDControl) control3);
      CustomSelfDrawPanel.CSDLabel control4 = new CustomSelfDrawPanel.CSDLabel();
      control4.Text = elementTotal1.ToString();
      control4.Color = ARGBColors.White;
      control4.Position = new Point(control3.Rectangle.Right + 5, control3.Y);
      control4.Size = new Size(control1.Width / 3, control3.Height);
      control4.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      control4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control4);
      CustomSelfDrawPanel.CSDImage control5 = new CustomSelfDrawPanel.CSDImage();
      control5.Image = (Image) (elementTotal2 <= placeableDefenders.archers ? GFXLibrary.preset_req_archer : GFXLibrary.preset_req_archer_red);
      control5.setSizeToImage();
      control5.Position = new Point(control1.Width / 2 - control5.Width, control1.Height / 4 - control5.Height / 2);
      control1.addControl((CustomSelfDrawPanel.CSDControl) control5);
      CustomSelfDrawPanel.CSDLabel control6 = new CustomSelfDrawPanel.CSDLabel();
      control6.Text = elementTotal2.ToString();
      control6.Color = ARGBColors.White;
      control6.Position = new Point(control5.Rectangle.Right + 5, control5.Y);
      control6.Size = new Size(control1.Width / 3, control5.Height);
      control6.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      control6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control6);
      CustomSelfDrawPanel.CSDImage control7 = new CustomSelfDrawPanel.CSDImage();
      control7.Image = (Image) (elementTotal3 <= placeableDefenders.pikemen ? GFXLibrary.preset_req_pikeman : GFXLibrary.preset_req_pikeman_red);
      control7.setSizeToImage();
      control7.Position = new Point(control1.Width * 4 / 5 - control7.Width, control1.Height / 4 - control7.Height / 2);
      control1.addControl((CustomSelfDrawPanel.CSDControl) control7);
      CustomSelfDrawPanel.CSDLabel control8 = new CustomSelfDrawPanel.CSDLabel();
      control8.Text = elementTotal3.ToString();
      control8.Color = ARGBColors.White;
      control8.Position = new Point(control7.Rectangle.Right + 5, control7.Y);
      control8.Size = new Size(control1.Width / 3, control7.Height);
      control8.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      control8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control8);
      CustomSelfDrawPanel.CSDImage control9 = new CustomSelfDrawPanel.CSDImage();
      control9.Image = (Image) (elementTotal4 <= placeableDefenders.swordsmen ? GFXLibrary.preset_req_swordsman : GFXLibrary.preset_req_swordsman_red);
      control9.setSizeToImage();
      control9.Position = new Point(control1.Width / 3 - control9.Width, control1.Height * 3 / 4 - control9.Height / 2);
      control1.addControl((CustomSelfDrawPanel.CSDControl) control9);
      CustomSelfDrawPanel.CSDLabel control10 = new CustomSelfDrawPanel.CSDLabel();
      control10.Text = elementTotal4.ToString();
      control10.Color = ARGBColors.White;
      control10.Position = new Point(control9.Rectangle.Right + 5, control9.Y);
      control10.Size = new Size(control1.Width / 2, control9.Height);
      control10.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      control10.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control10);
      CustomSelfDrawPanel.CSDImage control11 = new CustomSelfDrawPanel.CSDImage();
      control11.Image = (Image) (rangeTotal <= placeableDefenders.captains ? GFXLibrary.preset_req_captain : GFXLibrary.preset_req_captain_red);
      control11.setSizeToImage();
      control11.Position = new Point(control1.Width * 2 / 3 - control11.Width, control1.Height * 3 / 4 - control11.Height / 2);
      control1.addControl((CustomSelfDrawPanel.CSDControl) control11);
      CustomSelfDrawPanel.CSDLabel control12 = new CustomSelfDrawPanel.CSDLabel();
      control12.Text = rangeTotal.ToString();
      control12.Color = ARGBColors.White;
      control12.Position = new Point(control11.Rectangle.Right + 5, control11.Y);
      control12.Size = new Size(control1.Width / 2, control11.Height);
      control12.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      control12.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control12);
    }
  }
}
