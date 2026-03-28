// Decompiled with JetBrains decompiler
// Type: Kingdoms.PrizeClaimPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Stronghold.AuthClient;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PrizeClaimPanel : CustomSelfDrawPanel
  {
    private IContainer components;
    private CustomSelfDrawPanel.CSDExtendingPanel background = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDArea backgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDExtendingPanel prizeContentInset = new CustomSelfDrawPanel.CSDExtendingPanel();
    private ContestPrizeList m_PrizeContent = new ContestPrizeList();
    private CustomSelfDrawPanel.CSDLabel prizeNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel headerLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel prizeCountLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton claimButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private PrizeClaimPanel.ResponseDelegate m_ClaimResponseDel;
    private PrizeClaimWindow m_parent;
    private ClickLock claimLock = new ClickLock();
    private CustomSelfDrawPanel.CSDExtendingPanel initialBackground = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDArea initialBackgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel initialHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel initialPrizeCountLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton initialClaimButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton initialCloseButton = new CustomSelfDrawPanel.CSDButton();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Name = nameof (PrizeClaimPanel);
      this.Size = new Size(600, 55);
      this.ResumeLayout(false);
    }

    public PrizeClaimPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(PrizeClaimWindow parent)
    {
      this.m_parent = parent;
      this.clearControls();
      this.background.Size = new Size(parent.Width, parent.Height);
      this.background.Create((Image) GFXLibrary._9sclice_generic_top_left, (Image) GFXLibrary._9sclice_generic_top_mid, (Image) GFXLibrary._9sclice_generic_top_right, (Image) GFXLibrary._9sclice_generic_mid_left, (Image) GFXLibrary._9sclice_generic_mid_mid, (Image) GFXLibrary._9sclice_generic_mid_right, (Image) GFXLibrary._9sclice_generic_bottom_left, (Image) GFXLibrary._9sclice_generic_bottom_mid, (Image) GFXLibrary._9sclice_generic_bottom_right);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
      this.backgroundArea.Position = new Point(0, 0);
      this.backgroundArea.Size = new Size(625, 668);
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundArea);
      this.claimButton.ImageNorm = (Image) GFXLibrary.button_132_normal_gold;
      this.claimButton.ImageOver = (Image) GFXLibrary.button_132_over_gold;
      this.claimButton.ImageClick = (Image) GFXLibrary.button_132_in_gold;
      this.claimButton.setSizeToImage();
      this.claimButton.Position = new Point(this.background.Width / 4 - this.claimButton.Width / 2, this.background.Height - 40 - this.claimButton.Height / 2);
      this.claimButton.Text.Text = SK.Text("CONTEST_Claim_Prize", "Claim Prize");
      this.claimButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.claimButton.TextYOffset = -2;
      this.claimButton.Text.Color = ARGBColors.Black;
      this.claimButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.claimClick));
      this.claimButton.Enabled = true;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.claimButton);
      this.closeButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.closeButton.setSizeToImage();
      this.closeButton.Position = new Point(this.background.Width * 3 / 4 - this.closeButton.Width / 2, this.background.Rectangle.Bottom - 40 - this.closeButton.Height / 2);
      this.closeButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.closeButton.TextYOffset = -2;
      this.closeButton.Text.Color = ARGBColors.Black;
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick));
      this.closeButton.Enabled = true;
      this.closeButton.Text.Text = SK.Text("GENERIC_Close", "Close");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      this.m_PrizeContent.clearControls();
      this.m_PrizeContent.Visible = false;
      this.headerLabel = new CustomSelfDrawPanel.CSDLabel();
      this.headerLabel.Text = SK.Text("Reports_Prize_Claimed", "Prize Claimed");
      this.headerLabel.Color = ARGBColors.Black;
      this.headerLabel.Size = new Size(this.background.Width, 40);
      this.headerLabel.Position = new Point(0, 30);
      this.headerLabel.Font = FontManager.GetFont("Arial", 24f, FontStyle.Bold);
      this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabel);
      this.prizeNameLabel = new CustomSelfDrawPanel.CSDLabel();
      this.prizeNameLabel.Text = "";
      this.prizeNameLabel.Color = ARGBColors.Black;
      this.prizeNameLabel.Size = new Size(this.background.Width, 30);
      this.prizeNameLabel.Position = new Point(0, this.headerLabel.Rectangle.Bottom);
      this.prizeNameLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.prizeNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.prizeNameLabel);
      this.prizeContentInset.Size = new Size(this.background.Width - 80, this.closeButton.Y - this.prizeNameLabel.Rectangle.Bottom);
      this.prizeContentInset.Position = new Point(this.Width / 2 - this.prizeContentInset.Width / 2, this.prizeNameLabel.Rectangle.Bottom);
      this.prizeContentInset.Create((Image) GFXLibrary._9sclice_bracket_top_left, (Image) GFXLibrary._9sclice_bracket_mid_mid, (Image) GFXLibrary._9sclice_bracket_top_right, (Image) GFXLibrary._9sclice_bracket_mid_mid, (Image) GFXLibrary._9sclice_bracket_mid_mid, (Image) GFXLibrary._9sclice_bracket_mid_mid, (Image) GFXLibrary._9sclice_bracket_bottom_left, (Image) GFXLibrary._9sclice_bracket_mid_mid, (Image) GFXLibrary._9sclice_bracket_bottom_right);
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.prizeContentInset);
      this.prizeContentInset.addControl((CustomSelfDrawPanel.CSDControl) this.m_PrizeContent);
      this.prizeCountLabel = new CustomSelfDrawPanel.CSDLabel();
      this.prizeCountLabel.Color = ARGBColors.Black;
      this.prizeCountLabel.Position = new Point(0, this.prizeContentInset.Rectangle.Bottom - 15);
      this.prizeCountLabel.Size = new Size(this.background.Width, this.claimButton.Y - this.prizeCountLabel.Y);
      this.prizeCountLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.prizeCountLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      int count = GameEngine.Instance.World.pendingPrizes.Count;
      if (count > 0)
      {
        this.prizeCountLabel.Text = SK.Text("CONTEST_Prizes_Remaining", "Prizes waiting to be claimed") + ": ";
        this.prizeCountLabel.Text += count.ToString();
      }
      else
        this.prizeCountLabel.Text = "";
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.prizeCountLabel);
      this.background.Visible = false;
      this.initialBackground.Size = new Size(parent.Width, parent.Height / 2);
      this.initialBackground.Create((Image) GFXLibrary._9sclice_generic_top_left, (Image) GFXLibrary._9sclice_generic_top_mid, (Image) GFXLibrary._9sclice_generic_top_right, (Image) GFXLibrary._9sclice_generic_mid_left, (Image) GFXLibrary._9sclice_generic_mid_mid, (Image) GFXLibrary._9sclice_generic_mid_right, (Image) GFXLibrary._9sclice_generic_bottom_left, (Image) GFXLibrary._9sclice_generic_bottom_mid, (Image) GFXLibrary._9sclice_generic_bottom_right);
      this.initialBackground.Position = new Point(0, parent.Height / 4);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.initialBackground);
      this.initialBackgroundArea.Position = new Point(0, 0);
      this.initialBackgroundArea.Size = this.initialBackground.Size;
      this.initialBackground.addControl((CustomSelfDrawPanel.CSDControl) this.initialBackgroundArea);
      this.initialClaimButton.ImageNorm = (Image) GFXLibrary.button_132_normal_gold;
      this.initialClaimButton.ImageOver = (Image) GFXLibrary.button_132_over_gold;
      this.initialClaimButton.ImageClick = (Image) GFXLibrary.button_132_in_gold;
      this.initialClaimButton.setSizeToImage();
      this.initialClaimButton.Position = new Point(this.initialBackground.Width / 4 - this.initialClaimButton.Width / 2, this.initialBackground.Height - 40 - this.initialClaimButton.Height / 2);
      this.initialClaimButton.Text.Text = SK.Text("CONTEST_Claim_Prize", "Claim Prize");
      this.initialClaimButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.initialClaimButton.TextYOffset = -2;
      this.initialClaimButton.Text.Color = ARGBColors.Black;
      this.initialClaimButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.claimClick));
      this.initialClaimButton.Enabled = true;
      this.initialBackground.addControl((CustomSelfDrawPanel.CSDControl) this.initialClaimButton);
      this.initialCloseButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.initialCloseButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.initialCloseButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.initialCloseButton.setSizeToImage();
      this.initialCloseButton.Position = new Point(this.background.Width * 3 / 4 - this.initialCloseButton.Width / 2, this.initialClaimButton.Y);
      this.initialCloseButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.initialCloseButton.TextYOffset = -2;
      this.initialCloseButton.Text.Color = ARGBColors.Black;
      this.initialCloseButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick));
      this.initialCloseButton.Enabled = true;
      this.initialCloseButton.Text.Text = SK.Text("GENERIC_Close", "Close");
      this.initialBackground.addControl((CustomSelfDrawPanel.CSDControl) this.initialCloseButton);
      this.initialHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
      this.initialHeaderLabel.Text = SK.Text("CONTEST_Claim_Your_Prize", "Claim Your Prize!");
      this.initialHeaderLabel.Color = ARGBColors.Black;
      this.initialHeaderLabel.Size = new Size(this.initialBackground.Width, 40);
      this.initialHeaderLabel.Position = new Point(0, 30);
      this.initialHeaderLabel.Font = FontManager.GetFont("Arial", 24f, FontStyle.Bold);
      this.initialHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.initialBackground.addControl((CustomSelfDrawPanel.CSDControl) this.initialHeaderLabel);
      this.initialPrizeCountLabel = new CustomSelfDrawPanel.CSDLabel();
      this.initialPrizeCountLabel.Color = ARGBColors.Black;
      this.initialPrizeCountLabel.Position = new Point(0, this.initialHeaderLabel.Rectangle.Bottom);
      this.initialPrizeCountLabel.Size = new Size(this.initialBackground.Width, this.initialClaimButton.Y - this.initialPrizeCountLabel.Y);
      this.initialPrizeCountLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.initialPrizeCountLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      if (count > 0)
      {
        this.initialPrizeCountLabel.Text = SK.Text("CONTEST_Prizes_Remaining", "Prizes waiting to be claimed") + ": ";
        this.initialPrizeCountLabel.Text += count.ToString();
      }
      else
        this.initialPrizeCountLabel.Text = "";
      this.initialBackground.addControl((CustomSelfDrawPanel.CSDControl) this.initialPrizeCountLabel);
      this.background.Visible = false;
    }

    private void claimClick()
    {
      if (GameEngine.Instance.World.pendingPrizes.Count > 0)
      {
        if (!this.claimLock.canCall())
          return;
        this.ClaimPrize(GameEngine.Instance.World.pendingPrizes[0]);
      }
      else
        this.closeClick();
    }

    private void ClaimPrize(int prizeID)
    {
      this.m_PrizeContent.Visible = false;
      this.prizeNameLabel.Text = "";
      XmlRpcContestProvider forEndpoint = XmlRpcContestProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
      XmlRpcContestRequest req = new XmlRpcContestRequest();
      req.SessionID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", "");
      req.UserGUID = RemoteServices.Instance.UserGuid.ToString().Replace("-", "");
      req.PrizeID = new int?(prizeID);
      req.WorldID = new int?(RemoteServices.Instance.ProfileWorldID);
      this.m_ClaimResponseDel = new PrizeClaimPanel.ResponseDelegate(this.OnClaimResponse);
      forEndpoint.ClaimContestPrize((IContestRequest) req, new ContestEndResponseDelegate(this.ClaimPrizeCallback), (Control) null);
    }

    private void ClaimPrizeCallback(IContestProvider provider, IContestResponse response)
    {
      this.claimLock.called();
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) == 0 || ((XmlRpcContestResponse) response).ClaimedPrize == null)
        return;
      ContestPrizeDefinition prize = new ContestPrizeDefinition();
      prize.Content = ((XmlRpcContestResponse) response).ClaimedPrize;
      this.AddPrizeContentToAccount(prize.Content);
      this.prizeNameLabel.Text = prize.Content.Name;
      this.m_PrizeContent.Visible = true;
      this.m_PrizeContent.init(prize, (CustomSelfDrawPanel.CSDControl) this.prizeContentInset, 20, 10);
      GameEngine.Instance.World.pendingPrizes.RemoveAt(0);
      this.prizeNameLabel.invalidate();
      int count = GameEngine.Instance.World.pendingPrizes.Count;
      if (count > 0)
      {
        this.prizeCountLabel.Text = SK.Text("CONTEST_Prizes_Remaining", "Prizes waiting to be claimed") + ": ";
        this.prizeCountLabel.Text += count.ToString();
        this.claimButton.Text.Text = SK.Text("CONTEST_Next_Prize", "Next Prize");
      }
      else
      {
        this.claimButton.Visible = false;
        this.prizeCountLabel.Text = "";
      }
      this.prizeContentInset.invalidate();
      this.initialBackground.Visible = false;
      this.initialBackground.invalidate();
      this.background.Visible = true;
      this.background.invalidate();
    }

    private void OnClaimResponse(bool success)
    {
    }

    private void AddPrizeContentToAccount(ContestPrizeContent content)
    {
      if (content.Gold > 0)
        GameEngine.Instance.World.addGold((double) content.Gold);
      if (content.Honour > 0)
        GameEngine.Instance.World.addHonour((double) content.Honour);
      if (content.FaithPoints > 0)
        GameEngine.Instance.World.addFaithPoints((double) content.FaithPoints);
      int repPoints = content.RepPoints;
      for (int index = 0; index < content.WheelSpins.Count; ++index)
        GameEngine.Instance.World.addTickets(index, content.WheelSpins[index]);
      foreach (ContestPrizePackDefinition pack in content.Packs)
      {
        CardTypes.CardOffer cardOffer = GameEngine.Instance.cardPackManager.GetCardOffer(pack.OfferID);
        GameEngine.Instance.cardPackManager.addCardPack(cardOffer.ID, pack.Amount);
      }
      foreach (ContestPrizeTokenDefinition token in content.Tokens)
        ;
      foreach (ContestPrizeCardDefinition card in content.Cards)
        ;
    }

    private void closeClick() => PrizeClaimWindow.close();

    private delegate void AsyncDelegate();

    private delegate void ResponseDelegate(bool success);
  }
}
