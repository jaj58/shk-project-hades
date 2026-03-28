// Decompiled with JetBrains decompiler
// Type: Kingdoms.ContestPrizeLine
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Stronghold.AuthClient;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class ContestPrizeLine : CustomSelfDrawPanel.CSDControl
  {
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel rewardPositionsHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel rewardPositionsValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel ranksNeededLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel ranksNeededValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton infoButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDExtendingPanel backgroundInset = new CustomSelfDrawPanel.CSDExtendingPanel();

    public void init(
      ContestPrizeDefinition def,
      CustomSelfDrawPanel.CSDControl parentControl,
      ContestsPanel parentPanel)
    {
      this.clearControls();
      this.Size = new Size(parentControl.Width - 4, parentControl.Height / 3 - 2);
      this.backgroundInset.Size = new Size(parentControl.Width - 4, parentControl.Height / 3 - 2);
      this.backgroundInset.Position = new Point(0, 0);
      this.backgroundInset.Create((Image) GFXLibrary.quest_9sclice_grey_inset_top_left, (Image) GFXLibrary.quest_9sclice_grey_inset_top_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_top_right, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_left, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_right, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_left, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_right);
      switch (def.Tier)
      {
        case 1:
          this.titleLabel.Text = SK.Text("GENERIC_Silver", "Silver");
          break;
        case 2:
          this.titleLabel.Text = SK.Text("GENERIC_Bronze", "Bronze");
          break;
        default:
          this.titleLabel.Text = SK.Text("GENERIC_Gold", "Gold");
          break;
      }
      this.titleLabel.Color = ARGBColors.Black;
      this.titleLabel.Position = new Point(this.Width / 5, 3);
      this.titleLabel.Size = new Size(this.Width * 4 / 5, this.backgroundInset.Height / 2);
      this.titleLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.titleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.titleLabel);
      this.rewardPositionsHeaderLabel.Text = "";
      if (def.TierWidth > 0)
      {
        this.rewardPositionsHeaderLabel.Text = SK.Text("Event_Prize_Positions", "Positions") + " ";
        CustomSelfDrawPanel.CSDLabel positionsHeaderLabel = this.rewardPositionsHeaderLabel;
        positionsHeaderLabel.Text = positionsHeaderLabel.Text + def.MaxPosition.ToString() + " - " + def.MinPosition.ToString();
      }
      if (def.QualifyingScore > 0)
      {
        this.rewardPositionsHeaderLabel.Text = SK.Text("Event_Required_Score", "Score required") + " ";
        this.rewardPositionsHeaderLabel.Text += def.QualifyingScore.ToString();
      }
      this.rewardPositionsHeaderLabel.Color = ARGBColors.Black;
      this.rewardPositionsHeaderLabel.Position = new Point(this.Width / 5, this.backgroundInset.Height / 2);
      this.rewardPositionsHeaderLabel.Size = new Size(this.Width * 4 / 5, this.backgroundInset.Height / 2);
      this.rewardPositionsHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.rewardPositionsHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.rewardPositionsHeaderLabel);
      this.infoButton.ImageNorm = (Image) GFXLibrary.help_normal;
      this.infoButton.ImageOver = (Image) GFXLibrary.help_over;
      this.infoButton.ImageClick = (Image) GFXLibrary.help_pushed;
      this.infoButton.setSizeToImage();
      this.infoButton.Position = new Point(this.backgroundInset.Width / 5 - this.infoButton.Width - 3, this.titleLabel.Y + this.titleLabel.Height / 2 - this.infoButton.Height / 2);
      this.infoButton.Data = def.Content.ID;
      this.infoButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(parentPanel.OnPrizeInfoClicked));
      this.addControl((CustomSelfDrawPanel.CSDControl) this.infoButton);
    }
  }
}
