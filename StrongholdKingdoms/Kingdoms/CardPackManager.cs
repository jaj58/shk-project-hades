// Decompiled with JetBrains decompiler
// Type: Kingdoms.CardPackManager
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using Stronghold.AuthClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class CardPackManager
  {
    private Dictionary<int, CardTypes.UserCardPack> mProfileUserCardPacks;
    private Dictionary<int, CardTypes.CardOffer> mProfileCardOffers;
    private CardTypes.CardOffer offerBeingPurchased;
    private CardsEndResponseUIDelegate m_uiCallback;
    private CardsEndResponseDelegate m_callback;
    private bool extendedMultiOpen;
    private int extendedMultiOpenLeft;
    private int extendedMultiOpened;
    private CardTypes.CardOffer extendedPackClicked;
    private int openedPackID = -1;
    public bool openingPack;
    private Control control;

    public Dictionary<int, CardTypes.CardOffer> ProfileCardOffers
    {
      get
      {
        if (this.mProfileCardOffers == null)
          this.mProfileCardOffers = new Dictionary<int, CardTypes.CardOffer>();
        return this.mProfileCardOffers;
      }
    }

    public Dictionary<int, CardTypes.UserCardPack> ProfileUserCardPacks
    {
      get
      {
        if (this.mProfileUserCardPacks == null)
          this.mProfileUserCardPacks = new Dictionary<int, CardTypes.UserCardPack>();
        return this.mProfileUserCardPacks;
      }
      set => this.mProfileUserCardPacks = value;
    }

    public int ProfileUserTotalOwnedCardPacks
    {
      get
      {
        if (this.mProfileUserCardPacks == null)
          return 0;
        int totalOwnedCardPacks = 0;
        foreach (CardTypes.UserCardPack userCardPack in this.mProfileUserCardPacks.Values)
          totalOwnedCardPacks += userCardPack.Count;
        return totalOwnedCardPacks;
      }
    }

    public Dictionary<int, int> ProfileUserOwnedCardPackCounts()
    {
      Dictionary<int, int> dictionary1 = new Dictionary<int, int>();
      foreach (CardTypes.UserCardPack userCardPack in GameEngine.Instance.cardPackManager.ProfileUserCardPacks.Values)
      {
        if (!dictionary1.ContainsKey(userCardPack.OfferID))
          dictionary1.Add(userCardPack.OfferID, 0);
        Dictionary<int, int> dictionary2;
        int offerId;
        (dictionary2 = dictionary1)[offerId = userCardPack.OfferID] = dictionary2[offerId] + 1;
      }
      return dictionary1;
    }

    public int CountOwnedCardPacksInCategory(string category)
    {
      int num = 0;
      foreach (KeyValuePair<int, CardTypes.UserCardPack> profileUserCardPack in this.ProfileUserCardPacks)
      {
        if (this.ProfileCardOffers.ContainsKey(profileUserCardPack.Key) && this.ProfileCardOffers[profileUserCardPack.Key].Category == category)
          num += profileUserCardPack.Value.Count;
      }
      return num;
    }

    public void addCardPack(int packType, int amount)
    {
      if (this.ProfileUserCardPacks.ContainsKey(packType))
        this.ProfileUserCardPacks[packType].Count += amount;
      else
        this.ProfileUserCardPacks[packType] = new CardTypes.UserCardPack()
        {
          OfferID = packType,
          Count = amount
        };
    }

    public void PurchasePack(
      CardTypes.CardOffer offer,
      CardsEndResponseUIDelegate uiCallback,
      Control callbackControl)
    {
      this.control = callbackControl;
      this.offerBeingPurchased = offer != null ? offer : throw new ArgumentNullException("No card pack passed into purchase method");
      if (uiCallback != null)
        this.m_uiCallback = uiCallback;
      XmlRpcCardsProvider forEndpoint = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
      XmlRpcCardsRequest req = new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), offer.ID.ToString());
      req.SessionGUID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", "");
      if (InterfaceMgr.Instance.BuyOfferMultiple > 1)
        req.Multiple = new int?(InterfaceMgr.Instance.BuyOfferMultiple);
      this.m_callback = new CardsEndResponseDelegate(this.offerBought);
      forEndpoint.buyCardOffer((ICardsRequest) req, this.m_callback, this.control);
      GameEngine.Instance.World.ProfileCrowns -= offer.CrownCost * InterfaceMgr.Instance.BuyOfferMultiple;
    }

    private void offerBought(ICardsProvider provider, ICardsResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) != 0)
      {
        if (GameEngine.Instance.cardPackManager.ProfileUserCardPacks.ContainsKey(this.offerBeingPurchased.ID))
        {
          if (InterfaceMgr.Instance.BuyOfferMultiple < 1)
            ++GameEngine.Instance.cardPackManager.ProfileUserCardPacks[this.offerBeingPurchased.ID].Count;
          else
            GameEngine.Instance.cardPackManager.ProfileUserCardPacks[this.offerBeingPurchased.ID].Count += InterfaceMgr.Instance.BuyOfferMultiple;
        }
        else
          GameEngine.Instance.cardPackManager.ProfileUserCardPacks.Add(this.offerBeingPurchased.ID, new CardTypes.UserCardPack()
          {
            Count = InterfaceMgr.Instance.BuyOfferMultiple,
            OfferID = this.offerBeingPurchased.ID
          });
      }
      else
        GameEngine.Instance.World.ProfileCrowns += this.offerBeingPurchased.CrownCost * InterfaceMgr.Instance.BuyOfferMultiple;
      this.m_uiCallback(response);
    }

    public CardTypes.CardOffer GetCardOffer(string category)
    {
      foreach (KeyValuePair<int, CardTypes.CardOffer> profileCardOffer in this.ProfileCardOffers)
      {
        if (profileCardOffer.Value.Category == category)
          return profileCardOffer.Value;
      }
      return (CardTypes.CardOffer) null;
    }

    public CardTypes.CardOffer GetCardOffer(int OfferID)
    {
      foreach (KeyValuePair<int, CardTypes.CardOffer> profileCardOffer in this.ProfileCardOffers)
      {
        if (profileCardOffer.Value.ID == OfferID)
          return profileCardOffer.Value;
      }
      return (CardTypes.CardOffer) null;
    }

    public bool TryOpenPack(
      int offerID,
      CardsEndResponseUIDelegate uiClickDelegate,
      Control callbackControl)
    {
      this.control = callbackControl;
      string category = GameEngine.Instance.cardPackManager.ProfileCardOffers[offerID].Category;
      this.m_uiCallback = uiClickDelegate;
      bool flag = false;
      this.openingPack = true;
      this.extendedMultiOpen = false;
      this.extendedMultiOpenLeft = 0;
      this.extendedMultiOpened = 0;
      foreach (CardTypes.UserCardPack userCardPack in GameEngine.Instance.cardPackManager.ProfileUserCardPacks.Values)
      {
        if (GameEngine.Instance.cardPackManager.ProfileCardOffers[userCardPack.OfferID].Category == category && userCardPack.Count > 0)
        {
          this.openedPackID = userCardPack.OfferID;
          XmlRpcCardsProvider forEndpoint = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
          XmlRpcCardsRequest req = new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), (GameEngine.Instance.World.getRank() + 1).ToString(), userCardPack.OfferID.ToString(), RemoteServices.Instance.ProfileWorldID.ToString());
          req.SessionGUID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", "");
          req.Multiple = new int?(InterfaceMgr.Instance.OpenPackMultiple);
          UniversalDebugLog.Log("opening " + (object) req.Multiple);
          if (InterfaceMgr.Instance.OpenPackMultiple > 0 && userCardPack.Count < InterfaceMgr.Instance.OpenPackMultiple)
          {
            this.extendedMultiOpen = true;
            this.extendedMultiOpenLeft = InterfaceMgr.Instance.OpenPackMultiple - userCardPack.Count;
            this.extendedPackClicked = this.GetCardOffer(offerID);
            req.Multiple = new int?(userCardPack.Count);
            this.extendedMultiOpened = userCardPack.Count;
          }
          forEndpoint.openCardPack((ICardsRequest) req, new CardsEndResponseDelegate(this.onPackOpened), this.control);
          flag = true;
          break;
        }
      }
      if (!flag)
        this.openingPack = false;
      return flag;
    }

    public bool doExtendedMultiOpen(string searchCat)
    {
      foreach (CardTypes.UserCardPack userCardPack in GameEngine.Instance.cardPackManager.ProfileUserCardPacks.Values)
      {
        if (GameEngine.Instance.cardPackManager.ProfileCardOffers[userCardPack.OfferID].Category == searchCat && userCardPack.Count > 0)
        {
          this.openedPackID = userCardPack.OfferID;
          XmlRpcCardsProvider forEndpoint = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
          XmlRpcCardsRequest req = new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), (GameEngine.Instance.World.getRank() + 1).ToString(), userCardPack.OfferID.ToString(), RemoteServices.Instance.ProfileWorldID.ToString());
          req.SessionGUID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", "");
          req.Multiple = new int?(this.extendedMultiOpenLeft);
          if (userCardPack.Count < this.extendedMultiOpenLeft)
          {
            this.extendedMultiOpen = true;
            this.extendedMultiOpenLeft -= userCardPack.Count;
            req.Multiple = new int?(userCardPack.Count);
            this.extendedMultiOpened = userCardPack.Count;
          }
          else
          {
            this.extendedMultiOpened = this.extendedMultiOpenLeft;
            this.extendedMultiOpen = false;
            this.extendedPackClicked = (CardTypes.CardOffer) null;
          }
          forEndpoint.openCardPack((ICardsRequest) req, new CardsEndResponseDelegate(this.onPackOpened), this.control);
          return true;
        }
      }
      return false;
    }

    private void onPackOpened(ICardsProvider provider, ICardsResponse response)
    {
      if (response.SuccessCode.Value == 1)
      {
        foreach (CardTypes.UserCardPack userCardPack in GameEngine.Instance.cardPackManager.ProfileUserCardPacks.Values)
        {
          if (userCardPack.OfferID == this.openedPackID)
          {
            if (this.extendedMultiOpened > 0)
              userCardPack.Count -= this.extendedMultiOpened;
            else if (InterfaceMgr.Instance.OpenPackMultiple < 1)
              --userCardPack.Count;
            else
              userCardPack.Count -= InterfaceMgr.Instance.OpenPackMultiple;
            if (this.extendedMultiOpen)
            {
              if (this.extendedMultiOpenLeft > 0)
              {
                if (this.doExtendedMultiOpen(this.extendedPackClicked.Category))
                {
                  using (Dictionary<int, CardTypes.CardDefinition>.Enumerator enumerator = this.getCardsFromServerResponse(response.Strings).GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      KeyValuePair<int, CardTypes.CardDefinition> current = enumerator.Current;
                      GameEngine.Instance.cardsManager.ProfileCards.Add(current.Key, current.Value);
                    }
                    return;
                  }
                }
                else
                  break;
              }
              else
                break;
            }
            else
              break;
          }
        }
      }
      this.openingPack = false;
      this.m_uiCallback(response);
    }

    public Dictionary<int, CardTypes.CardDefinition> getCardsFromServerResponse(
      string responseString)
    {
      Dictionary<int, CardTypes.CardDefinition> fromServerResponse = new Dictionary<int, CardTypes.CardDefinition>();
      foreach (string str in responseString.Split(";".ToCharArray()))
      {
        string[] strArray = str.Split(",".ToCharArray());
        if (strArray.Length == 2)
        {
          int int32 = Convert.ToInt32(strArray[0].Trim());
          CardTypes.CardDefinition definitionFromString = CardTypes.getCardDefinitionFromString(strArray[1].Trim());
          fromServerResponse.Add(int32, definitionFromString);
        }
      }
      return fromServerResponse;
    }

    public BaseImage getCardPackBaseImage(string category)
    {
      string key;
      switch (category)
      {
        case "FARMING":
          key = "card_pack_food_standard_normal";
          break;
        case "CASTLE":
          key = "card_pack_castle_standard_normal";
          break;
        case "DEFENSE":
        case "DEFENCE":
          key = "card_pack_defence_standard_normal";
          break;
        case "ARMY":
          key = "card_pack_army_standard_normal";
          break;
        case "RANDOM":
          key = "card_pack_random_standard_normal";
          break;
        case "INDUSTRY":
          key = "card_pack_Industry_standard_normal";
          break;
        case "EXCLUSIVE":
          key = "card_pack_exclusive_silver_normal";
          break;
        case "RESEARCH":
          key = "card_pack_research_silver_normal";
          break;
        case "SUPERFARMING":
          key = "card_pack_food_silver_normal";
          break;
        case "SUPERDEFENSE":
        case "SUPERDEFENCE":
          key = "card_pack_defence_silver_normal";
          break;
        case "SUPERRANDOM":
          key = "card_pack_random_silver_normal";
          break;
        case "SUPERINDUSTRY":
          key = "card_pack_Industry_silver_normal";
          break;
        case "SUPERARMY":
          key = "card_pack_army_silver_normal";
          break;
        case "ULTIMATEFARMING":
          key = "card_pack_food_gold_normal";
          break;
        case "ULTIMATEDEFENSE":
        case "ULTIMATEDEFENCE":
          key = "card_pack_defence_gold_normal";
          break;
        case "ULTIMATERANDOM":
          key = "card_pack_random_gold_normal";
          break;
        case "ULTIMATEINDUSTRY":
          key = "card_pack_Industry_gold_normal";
          break;
        case "ULTIMATEARMY":
          key = "card_pack_army_gold_normal";
          break;
        case "PLATINUM":
          key = "card_pack_army_gold_normal";
          break;
        default:
          key = "card_pack_Industry_standard_normal";
          break;
      }
      try
      {
        return GFXLibrary.CardPackImages[key];
      }
      catch (Exception ex)
      {
        return GFXLibrary.CardPackImages["card_pack_open_Industry-Pack"];
      }
    }

    public BaseImage getCardPackOverImage(string category)
    {
      string key;
      switch (category)
      {
        case "FARMING":
          key = "card_pack_food_standard_over";
          break;
        case "CASTLE":
          key = "card_pack_castle_standard_over";
          break;
        case "DEFENSE":
        case "DEFENCE":
          key = "card_pack_defence_standard_over";
          break;
        case "ARMY":
          key = "card_pack_army_standard_over";
          break;
        case "RANDOM":
          key = "card_pack_random_standard_over";
          break;
        case "INDUSTRY":
          key = "card_pack_Industry_standard_over";
          break;
        case "EXCLUSIVE":
          key = "card_pack_exclusive_silver_over";
          break;
        case "RESEARCH":
          key = "card_pack_research_silver_over";
          break;
        case "SUPERFARMING":
          key = "card_pack_food_silver_over";
          break;
        case "SUPERDEFENSE":
        case "SUPERDEFENCE":
          key = "card_pack_defence_silver_over";
          break;
        case "SUPERRANDOM":
          key = "card_pack_random_silver_over";
          break;
        case "SUPERINDUSTRY":
          key = "card_pack_Industry_silver_over";
          break;
        case "SUPERARMY":
          key = "card_pack_army_silver_over";
          break;
        case "ULTIMATEFARMING":
          key = "card_pack_food_gold_over";
          break;
        case "ULTIMATEDEFENSE":
        case "ULTIMATEDEFENCE":
          key = "card_pack_defence_gold_over";
          break;
        case "ULTIMATERANDOM":
          key = "card_pack_random_gold_over";
          break;
        case "ULTIMATEINDUSTRY":
          key = "card_pack_Industry_gold_over";
          break;
        case "ULTIMATEARMY":
          key = "card_pack_army_gold_over";
          break;
        case "PLATINUM":
          key = "card_pack_army_gold_over";
          break;
        default:
          key = "card_pack_Industry_standard_over";
          break;
      }
      try
      {
        return GFXLibrary.CardPackImages[key];
      }
      catch (Exception ex)
      {
        return GFXLibrary.CardPackImages["card_pack_Industry_standard_over"];
      }
    }

    public int getCardPackTooltipID(string category)
    {
      int cardPackTooltipId = 0;
      switch (category)
      {
        case "FARMING":
          cardPackTooltipId = 10301;
          break;
        case "CASTLE":
          cardPackTooltipId = 10302;
          break;
        case "DEFENSE":
        case "DEFENCE":
          cardPackTooltipId = 10303;
          break;
        case "ARMY":
          cardPackTooltipId = 10304;
          break;
        case "RANDOM":
          cardPackTooltipId = 10305;
          break;
        case "INDUSTRY":
          cardPackTooltipId = 10306;
          break;
        case "EXCLUSIVE":
          cardPackTooltipId = 10308;
          break;
        case "RESEARCH":
          cardPackTooltipId = 10307;
          break;
        case "SUPERFARMING":
          cardPackTooltipId = 10309;
          break;
        case "SUPERDEFENSE":
        case "SUPERDEFENCE":
          cardPackTooltipId = 10310;
          break;
        case "SUPERRANDOM":
          cardPackTooltipId = 10312;
          break;
        case "SUPERINDUSTRY":
          cardPackTooltipId = 10313;
          break;
        case "SUPERARMY":
          cardPackTooltipId = 10311;
          break;
        case "ULTIMATEFARMING":
          cardPackTooltipId = 10314;
          break;
        case "ULTIMATEDEFENSE":
        case "ULTIMATEDEFENCE":
          cardPackTooltipId = 10315;
          break;
        case "ULTIMATERANDOM":
          cardPackTooltipId = 10317;
          break;
        case "ULTIMATEINDUSTRY":
          cardPackTooltipId = 10318;
          break;
        case "ULTIMATEARMY":
          cardPackTooltipId = 10316;
          break;
        case "PLATINUM":
          cardPackTooltipId = 10321;
          break;
      }
      return cardPackTooltipId;
    }

    public string getCardPackLocalizedStringID(string category)
    {
      string localizedStringId;
      switch (category)
      {
        case "FARMING":
          localizedStringId = "CARD_OFFERS_Food_Pack";
          break;
        case "CASTLE":
          localizedStringId = "CARD_OFFERS_Castle_Pack";
          break;
        case "DEFENSE":
        case "DEFENCE":
          localizedStringId = "CARD_OFFERS_Defense_Pack";
          break;
        case "ARMY":
          localizedStringId = "CARD_OFFERS_Army_Pack";
          break;
        case "RANDOM":
          localizedStringId = "CARD_OFFERS_Random_Pack";
          break;
        case "INDUSTRY":
          localizedStringId = "CARD_OFFERS_Industry_Pack";
          break;
        case "EXCLUSIVE":
          localizedStringId = "CARD_OFFERS_Exclusive_Pack";
          break;
        case "RESEARCH":
          localizedStringId = "CARD_OFFERS_Research_Pack";
          break;
        case "SUPERFARMING":
          localizedStringId = "CARD_OFFERS_Super_Food_Pack";
          break;
        case "SUPERDEFENSE":
        case "SUPERDEFENCE":
          localizedStringId = "CARD_OFFERS_Super_Defense_Pack";
          break;
        case "SUPERRANDOM":
          localizedStringId = "CARD_OFFERS_Super_Random_Pack";
          break;
        case "SUPERINDUSTRY":
          localizedStringId = "CARD_OFFERS_Super_Industry_Pack";
          break;
        case "SUPERARMY":
          localizedStringId = "CARD_OFFERS_Super_Army_Pack";
          break;
        case "ULTIMATEFARMING":
          localizedStringId = "CARD_OFFERS_Ultimate_Food_Pack";
          break;
        case "ULTIMATEDEFENSE":
        case "ULTIMATEDEFENCE":
          localizedStringId = "CARD_OFFERS_Ultimate_Defense_Pack";
          break;
        case "ULTIMATERANDOM":
          localizedStringId = "CARD_OFFERS_Ultimate_Random_Pack";
          break;
        case "ULTIMATEINDUSTRY":
          localizedStringId = "CARD_OFFERS_Ultimate_Industry_Pack";
          break;
        case "ULTIMATEARMY":
          localizedStringId = "CARD_OFFERS_Ultimate_Army_Pack";
          break;
        case "PLATINUM":
          localizedStringId = "CARD_OFFERS_Platinum_Pack";
          break;
        default:
          localizedStringId = "CARD_OFFERS_Industry_Pack";
          break;
      }
      return localizedStringId;
    }

    public CardTypes.CardOffer getBuyablePackInCategory(string offerID)
    {
      CardTypes.CardOffer buyablePackInCategory = (CardTypes.CardOffer) null;
      foreach (CardTypes.CardOffer cardOffer in GameEngine.Instance.cardPackManager.ProfileCardOffers.Values)
      {
        if (cardOffer.Category == offerID)
        {
          if (buyablePackInCategory == null)
            buyablePackInCategory = cardOffer;
          else if (cardOffer.Buyable == 1)
            buyablePackInCategory = cardOffer;
        }
      }
      return buyablePackInCategory;
    }
  }
}
