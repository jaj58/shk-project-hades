// Decompiled with JetBrains decompiler
// Type: Kingdoms.PremiumTokenManager
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Stronghold.AuthClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PremiumTokenManager
  {
    public int ProfilePremiumCards;
    private Dictionary<int, CardTypes.PremiumToken> mProfilePremiumTokens;
    private CardsEndResponseDelegate buyTokenUiDelegate;
    private int crowns;
    private int buyingTokenType;
    private CardsEndResponseDelegate m_playTokenUiDelegate;
    private CardTypes.PremiumToken playingToken;

    public Dictionary<int, CardTypes.PremiumToken> ProfilePremiumTokens
    {
      get
      {
        if (this.mProfilePremiumTokens == null)
          this.mProfilePremiumTokens = new Dictionary<int, CardTypes.PremiumToken>();
        return this.mProfilePremiumTokens;
      }
    }

    public int countPremiumTokensOfType(int tokenType)
    {
      int num = 0;
      foreach (CardTypes.PremiumToken premiumToken in this.ProfilePremiumTokens.Values)
      {
        if (premiumToken.Type == tokenType)
          ++num;
      }
      return num;
    }

    public void buyToken(
      int premiumType,
      CardsEndResponseDelegate uiDelegate,
      Control callbackControl)
    {
      this.buyTokenUiDelegate = uiDelegate;
      this.buyingTokenType = premiumType;
      XmlRpcCardsProvider forEndpoint = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
      XmlRpcCardsRequest req = new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""));
      req.SessionGUID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", "");
      if (premiumType == 4112)
      {
        req.PackID = "2";
        this.crowns = 30;
      }
      else
      {
        if (premiumType != 4114)
          throw new ArgumentException("Did not provide a purchasable premium token type");
        req.PackID = "6";
        this.crowns = 100;
      }
      forEndpoint.buyPremium((ICardsRequest) req, new CardsEndResponseDelegate(this.onBoughtToken), callbackControl);
      GameEngine.Instance.World.ProfileCrowns -= this.crowns;
    }

    private void onBoughtToken(ICardsProvider provider, ICardsResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 1 : (!successCode.HasValue ? 1 : 0)) != 0)
      {
        GameEngine.Instance.World.ProfileCrowns += this.crowns;
      }
      else
      {
        int result = 0;
        int.TryParse(response.Strings, out result);
        this.ProfilePremiumTokens.Add(result, new CardTypes.PremiumToken()
        {
          Reward = 0,
          Type = this.buyingTokenType,
          UserPremiumTokenID = result,
          WorldID = RemoteServices.Instance.ProfileWorldID
        });
      }
      this.buyTokenUiDelegate(provider, response);
    }

    public TimeSpan ExpiryTimeSpan
    {
      get
      {
        return GameEngine.Instance.cardsManager.UserCardData.premiumCardExpiry.Subtract(VillageMap.getCurrentServerTime());
      }
    }

    public bool PremiumInPlay => GameEngine.Instance.cardsManager.UserCardData.premiumCard > 0;

    public CardTypes.PremiumToken getUserTokenOfType(int tokenType)
    {
      if (tokenType != 4113 && tokenType != 4112 && tokenType != 4114)
        throw new ArgumentException("Tried to find invalid token type " + (object) tokenType);
      if (this.mProfilePremiumTokens != null)
      {
        foreach (CardTypes.PremiumToken userTokenOfType in this.mProfilePremiumTokens.Values)
        {
          if (userTokenOfType.Type == tokenType)
            return userTokenOfType;
        }
      }
      return (CardTypes.PremiumToken) null;
    }

    public CardTypes.PremiumToken getUserToken(int userTokenID)
    {
      return this.mProfilePremiumTokens != null && this.mProfilePremiumTokens.ContainsKey(userTokenID) ? this.mProfilePremiumTokens[userTokenID] : (CardTypes.PremiumToken) null;
    }

    public void PlayToken(
      CardTypes.PremiumToken token,
      CardsEndResponseDelegate uiDelegate,
      Control callbackControl)
    {
      this.playingToken = token;
      this.m_playTokenUiDelegate = uiDelegate;
      DateTime currentServerTime = VillageMap.getCurrentServerTime();
      double num = GameEngine.Instance.cardsManager.UserCardData.premiumCardExpiry.Subtract(VillageMap.getCurrentServerTime()).TotalSeconds;
      if (num < 0.0)
        num = 0.0;
      DateTime dateTime = currentServerTime.AddSeconds(num);
      XmlRpcCardsProvider forEndpoint = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
      XmlRpcCardsRequest req = new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""));
      req.SessionGUID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", "");
      req.WorldID = RemoteServices.Instance.ProfileWorldID.ToString();
      req.UserCardID = this.playingToken.UserPremiumTokenID.ToString();
      if (this.playingToken.Type == 4112)
        req.CardString = "CARDTYPE_PREMIUM";
      if (this.playingToken.Type == 4113)
        req.CardString = "CARDTYPE_PREMIUM2";
      if (this.playingToken.Type == 4114)
        req.CardString = "CARDTYPE_PREMIUM30";
      forEndpoint.playPremium((ICardsRequest) req, new CardsEndResponseDelegate(this.onPlayedToken), callbackControl);
      --this.ProfilePremiumCards;
      if (this.playingToken.Type == 4112)
        GameEngine.Instance.cardsManager.UserCardData.premiumCardExpiry = dateTime.AddDays(7.0);
      if (this.playingToken.Type == 4114)
        GameEngine.Instance.cardsManager.UserCardData.premiumCardExpiry = dateTime.AddDays(30.0);
      if (this.playingToken.Type == 4113)
        GameEngine.Instance.cardsManager.UserCardData.premiumCardExpiry = dateTime.AddDays(2.0);
      GameEngine.Instance.cardsManager.UserCardData.premiumCard = !this.PremiumInPlay ? this.playingToken.Type : 4116;
      GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens.Remove(this.playingToken.UserPremiumTokenID);
    }

    private void onPlayedToken(ICardsProvider provider, ICardsResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 1 : (!successCode.HasValue ? 1 : 0)) != 0)
      {
        GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens.Add(this.playingToken.UserPremiumTokenID, this.playingToken);
        GameEngine.Instance.cardsManager.UserCardData.premiumCard = 0;
        GameEngine.Instance.cardsManager.UserCardData.premiumCardExpiry = VillageMap.getCurrentServerTime();
      }
      else
        GameEngine.Instance.cardsManager.CardPlayed(-1, GameEngine.Instance.cardsManager.UserCardData.premiumCard, -1);
      if (this.m_playTokenUiDelegate == null)
        return;
      this.m_playTokenUiDelegate(provider, response);
    }

    public void AddToken(CardTypes.PremiumToken premiumToken)
    {
      if (this.ProfilePremiumTokens.ContainsKey(premiumToken.UserPremiumTokenID))
        return;
      this.ProfilePremiumTokens.Add(premiumToken.UserPremiumTokenID, premiumToken);
    }
  }
}
