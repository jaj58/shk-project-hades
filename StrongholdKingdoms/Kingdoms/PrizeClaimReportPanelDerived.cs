// Decompiled with JetBrains decompiler
// Type: Kingdoms.PrizeClaimReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Stronghold.AuthClient;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  internal class PrizeClaimReportPanelDerived : GenericReportPanelBasic
  {
    private int m_prizeID;
    private int m_contestID;
    private CustomSelfDrawPanel.CSDArea prizeArea = new CustomSelfDrawPanel.CSDArea();
    private PrizeClaimReportPanelDerived.ResponseDelegate m_PrizeResponseDel;
    private ContestPrizeList m_PrizeContent = new ContestPrizeList();

    public PrizeClaimReportPanelDerived()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.Size = new Size(580, 500);
    }

    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      this.m_prizeID = returnData.genericData1;
      this.m_contestID = returnData.genericData2;
      this.lblDate.Position = new Point(0, this.lblSubTitle.Rectangle.Bottom);
      this.prizeArea.Position = new Point(this.Width / 6, this.lblDate.Rectangle.Bottom);
      this.prizeArea.Size = new Size(this.Width * 2 / 3, this.btnDelete.Y - this.lblDate.Rectangle.Bottom - 10);
      if (this.hasBackground())
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.prizeArea);
      else
        this.addControl((CustomSelfDrawPanel.CSDControl) this.prizeArea);
      this.prizeArea.addControl((CustomSelfDrawPanel.CSDControl) this.m_PrizeContent);
      this.lblMainText.Text = SK.Text("Reports_Prize_Claimed", "Prize Claimed");
      ContestPrizeDefinition prize = new ContestPrizeDefinition();
      prize.Content.Gold = returnData.genericData3;
      prize.Content.FaithPoints = returnData.genericData4;
      prize.Content.Honour = returnData.genericData5;
      prize.Content.RepPoints = returnData.genericData6;
      prize.Content.ShieldCharges = new List<int>();
      for (int index = 0; index < returnData.genericData7; ++index)
        prize.Content.ShieldCharges.Add(1);
      prize.Content.WheelSpins = new List<int>();
      for (int index = 0; index < 6; ++index)
        prize.Content.WheelSpins.Add(0);
      if (returnData.genericData8 > 0)
        this.addCompoundPrize((ContestCompoundPrize) returnData.genericData8, returnData.genericData9, returnData.genericData10, ref prize.Content);
      if (returnData.genericData11 > 0)
        this.addCompoundPrize((ContestCompoundPrize) returnData.genericData11, returnData.genericData12, returnData.genericData13, ref prize.Content);
      if (returnData.genericData14 > 0)
        this.addCompoundPrize((ContestCompoundPrize) returnData.genericData14, returnData.genericData15, returnData.genericData16, ref prize.Content);
      if (returnData.genericData17 > 0)
        this.addCompoundPrize((ContestCompoundPrize) returnData.genericData17, returnData.genericData18, returnData.genericData19, ref prize.Content);
      if (returnData.genericData20 > 0)
        this.addCompoundPrize((ContestCompoundPrize) returnData.genericData20, returnData.genericData21, returnData.genericData22, ref prize.Content);
      if (returnData.genericData23 > 0)
        this.addCompoundPrize((ContestCompoundPrize) returnData.genericData23, returnData.genericData24, returnData.genericData25, ref prize.Content);
      if (returnData.genericData26 > 0)
        this.addCompoundPrize((ContestCompoundPrize) returnData.genericData26, returnData.genericData27, returnData.genericData28, ref prize.Content);
      if (returnData.genericData29 > 0)
        this.addCompoundPrize((ContestCompoundPrize) returnData.genericData29, returnData.genericData30, returnData.genericData31, ref prize.Content);
      if (returnData.genericData32 > 0)
        this.addCompoundPrize((ContestCompoundPrize) returnData.genericData32, returnData.genericData33, returnData.genericData34, ref prize.Content);
      this.m_PrizeContent.init(prize, (CustomSelfDrawPanel.CSDControl) this.prizeArea, 10, 0);
      this.m_PrizeContent.Visible = returnData.reportType == (short) 141;
      this.m_PrizeContent.invalidate();
      this.prizeArea.invalidate();
    }

    private void addCompoundPrize(
      ContestCompoundPrize type,
      int id,
      int amount,
      ref ContestPrizeContent content)
    {
      switch (type)
      {
        case ContestCompoundPrize.CARD:
          content.Cards.Add(new ContestPrizeCardDefinition()
          {
            Amount = amount,
            ID = id,
            Name = CardTypes.getDescriptionFromCard(id)
          });
          break;
        case ContestCompoundPrize.PACK:
          content.Packs.Add(new ContestPrizePackDefinition()
          {
            Amount = amount,
            OfferID = id,
            Name = GameEngine.Instance.cardPackManager.GetCardOffer(id).Name
          });
          break;
        case ContestCompoundPrize.TOKEN:
          content.Tokens.Add(new ContestPrizeTokenDefinition()
          {
            Amount = amount,
            TokenType = id,
            Name = CardTypes.GetTokenName(id)
          });
          break;
        case ContestCompoundPrize.SPIN:
          List<int> wheelSpins;
          int index;
          (wheelSpins = content.WheelSpins)[index = id] = wheelSpins[index] + amount;
          break;
      }
    }

    protected override void utilityClick() => this.RetrievePrizeData();

    private void RetrievePrizeData()
    {
      XmlRpcContestProvider forEndpoint = XmlRpcContestProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
      XmlRpcContestRequest req = new XmlRpcContestRequest();
      req.SessionID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", "");
      req.UserGUID = RemoteServices.Instance.UserGuid.ToString().Replace("-", "");
      req.EventID = new int?(this.m_contestID);
      this.m_PrizeResponseDel = new PrizeClaimReportPanelDerived.ResponseDelegate(this.OnPrizeDataReceived);
      forEndpoint.RetrieveContestMetaData((IContestRequest) req, new ContestEndResponseDelegate(this.RetrievePrizeDataCallback), (Control) null);
    }

    private void RetrievePrizeDataCallback(IContestProvider provider, IContestResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) == 0)
        return;
      List<ContestPrizeDefinition> prizes = ((XmlRpcContestResponse) response).Prizes;
      if (prizes.Count <= 0)
        return;
      foreach (ContestPrizeDefinition prize in prizes)
      {
        if (prize.Content.ID == this.m_prizeID)
        {
          this.m_PrizeContent.init(prize, (CustomSelfDrawPanel.CSDControl) this.prizeArea, 5, 5);
          this.lblMainText.Text = prize.Content.Name;
          this.prizeArea.invalidate();
          break;
        }
      }
    }

    private void OnPrizeDataReceived(bool success)
    {
    }

    private delegate void ResponseDelegate(bool success);
  }
}
