// Decompiled with JetBrains decompiler
// Type: Kingdoms.CardsManager
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Stronghold.AuthClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class CardsManager
  {
    public int SelectedUserCardID;
    public CardTypes.CardDefinition lastUserCardSearchCriteria;
    public string lastUserCardSortOrder = string.Empty;
    public string lastUserCardNameFilter = string.Empty;
    public CardTypes.CardDefinition lastCardCatalogSearchCriteria;
    public string lastCardCatalogSortOrder = string.Empty;
    private Dictionary<int, CardTypes.CardDefinition> mProfileCards;
    private List<int> mProfileCardsSearch;
    private List<int> mProfileCardsSet;
    private List<int> mCatalogCardsSearch;
    private List<int> mShoppingCartCards;
    public bool NewCategoriesAvailable_Salt;
    public bool NewCategoriesAvailable_Spice;
    public bool NewCategoriesAvailable_Silk;
    public bool NewCategoriesAvailable_Catapults;
    public bool NewCategoriesAvailable_Strategy;
    public bool NewCategoriesAvailable_Capacity;
    public bool NewCategoriesAvailable_Parish;
    public bool NewCategoriesAvailable_FullHeight;
    public int lastCountedCategory = -1;
    public int lastCountedCategoryValue;
    private CardData m_userCardData;
    public bool UserCardDataChanged;
    public List<int> recentCards = new List<int>();
    public PremiumOfferData[] PremiumOffers;
    public bool isGettingPremiumOffers;
    public bool PremiumOffersViewed;
    private bool pendingFullRefresh;
    private bool pendingAllVillageRefresh;
    private bool pendingVillageRefresh;
    private int pendingVillageRefreshID = -1;
    private bool pendingResearchRefresh;
    private DateTime pendingCardUpdateTime = DateTime.MinValue;

    public bool SelectedCardExists()
    {
      return this.ProfileCards != null && GameEngine.Instance.cardsManager.ProfileCards.ContainsKey(GameEngine.Instance.cardsManager.SelectedUserCardID);
    }

    public CardTypes.CardDefinition SelectedCardDefinition()
    {
      return GameEngine.Instance.cardsManager.ProfileCards[this.SelectedUserCardID];
    }

    public void addRecentCard(int newCard)
    {
      if (this.recentCards.Contains(newCard))
        this.recentCards.Remove(newCard);
      if (this.recentCards.Count == 8)
        this.recentCards.RemoveAt(7);
      this.recentCards.Insert(0, newCard);
    }

    public void addRecentCardsFromServer(int[] cards)
    {
      int num = 0;
      this.recentCards.Clear();
      if (cards == null)
        return;
      foreach (int card in cards)
      {
        ++num;
        this.recentCards.Add(CardTypes.getCardType(card));
        if (num == 8)
          break;
      }
    }

    public Dictionary<int, CardTypes.CardDefinition> ProfileCards
    {
      get
      {
        if (this.mProfileCards == null)
          this.mProfileCards = new Dictionary<int, CardTypes.CardDefinition>();
        return this.mProfileCards;
      }
    }

    public List<int> ProfileCardsSearch
    {
      get
      {
        if (this.mProfileCardsSearch == null)
          this.mProfileCardsSearch = new List<int>();
        return this.mProfileCardsSearch;
      }
    }

    public List<int> ProfileCardsSet
    {
      get
      {
        if (this.mProfileCardsSet == null)
          this.mProfileCardsSet = new List<int>();
        return this.mProfileCardsSet;
      }
    }

    public List<int> CatalogCardsSearch
    {
      get
      {
        if (this.mCatalogCardsSearch == null)
          this.mCatalogCardsSearch = new List<int>();
        return this.mCatalogCardsSearch;
      }
    }

    public List<int> ShoppingCartCards
    {
      get
      {
        if (this.mShoppingCartCards == null)
          this.mShoppingCartCards = new List<int>();
        return this.mShoppingCartCards;
      }
    }

    public void onLogout() => this.mProfileCardsSet = new List<int>();

    public CardData UserCardData
    {
      set
      {
        this.m_userCardData = value;
        if (this.m_userCardData != null)
          InterfaceMgr.Instance.setCardData(this.m_userCardData);
        this.UserCardDataChanged = true;
      }
      get
      {
        if (this.m_userCardData == null)
          return new CardData();
        DateTime currentServerTime = VillageMap.getCurrentServerTime();
        for (int index = 0; index < this.m_userCardData.cards.Length; ++index)
        {
          if (this.m_userCardData.cards[index] != 0 && currentServerTime > this.m_userCardData.cardsExpiry[index])
            this.m_userCardData.cards[index] = 0;
        }
        if (this.m_userCardData.premiumCard != 0)
        {
          if (this.m_userCardData.premiumCard == 4113 && currentServerTime > this.m_userCardData.premiumCardExpiry)
          {
            this.m_userCardData.premiumCard = 0;
            if (!Program.mySettings.AdvertShown)
              this.m_userCardData.premiumAdvertNeeded = true;
          }
          else if (currentServerTime > this.m_userCardData.premiumCardExpiry)
            this.m_userCardData.premiumCard = 0;
        }
        if (this.m_userCardData.premiumAdvertNeeded)
        {
          this.m_userCardData.premiumAdvertNeeded = false;
          Program.mySettings.AdvertShown = true;
          InterfaceMgr.Instance.openLogoutWindow(true, true);
        }
        return this.m_userCardData;
      }
    }

    public bool isCardInPlay(CardTypes.CardDefinition def)
    {
      if (this.UserCardData != null)
      {
        foreach (int card in this.UserCardData.cards)
        {
          if (CardTypes.getCardType(card) == def.id)
            return true;
        }
      }
      return false;
    }

    public List<CardTypes.CardDefinition> getCardsInPlay()
    {
      List<CardTypes.CardDefinition> cardsInPlay = new List<CardTypes.CardDefinition>();
      if (this.UserCardData != null)
      {
        foreach (int card in this.UserCardData.cards)
          cardsInPlay.Add(CardTypes.getCardDefinition(card));
      }
      return cardsInPlay;
    }

    public void addProfileCard(int id, string type)
    {
      if (this.ProfileCards.ContainsKey(id))
        throw new Exception("Tried to add a card that was already there: UserTradingCardID=" + (object) id);
      try
      {
        this.ProfileCards.Add(id, CardTypes.getCardDefinitionFromString(type.Trim()));
      }
      catch (Exception ex)
      {
        UniversalDebugLog.Log(ex.Message + " " + (object) ex.InnerException);
        throw new Exception("Tried to add a card and couldn't: UserTradingCardID= " + (object) id + " type= " + type);
      }
    }

    public void removeProfileCard(int id)
    {
      if (!this.ProfileCards.ContainsKey(id))
        throw new Exception("Tried to remove a card that wasn't there: UserTradingCardID=" + (object) id);
      this.ProfileCards.Remove(id);
      if (this.ProfileCardsSearch.Contains(id))
        this.ProfileCardsSearch.Remove(id);
      if (!this.ProfileCardsSet.Contains(id))
        return;
      this.ProfileCardsSet.Remove(id);
    }

    public void searchProfileCards(CardTypes.CardDefinition filter, string sort, string namefilter)
    {
      this.ProfileCardsSearch.Clear();
      List<int> intList = new List<int>();
      filter.cardFilter = 0;
      foreach (int key in this.ProfileCards.Keys)
      {
        if (this.filterCard(filter, this.ProfileCards[key]))
          intList.Add(key);
      }
      foreach (int key in intList)
      {
        if (CardTypes.isCardInNewCategory(this.ProfileCards[key].id, filter.newCardCategoryFilter) && (namefilter.Length == 0 || CardTypes.containsName(this.ProfileCards[key].id, namefilter)))
          this.ProfileCardsSearch.Add(key);
      }
      if (this.ProfileCardsSearch.Count > 0)
      {
        switch (sort)
        {
          case "rarity":
            this.ProfileCardsSearch.Sort((Comparison<int>) ((first, next) =>
            {
              int cardRarity1 = CardTypes.getCardDefinition(this.ProfileCards[first].id).cardRarity;
              int cardRarity2 = CardTypes.getCardDefinition(this.ProfileCards[next].id).cardRarity;
              return cardRarity1 != cardRarity2 ? cardRarity2.CompareTo(cardRarity1) : this.ProfileCards[first].id.CompareTo(this.ProfileCards[next].id);
            }));
            break;
          case "meta":
            this.ProfileCardsSearch.Sort((Comparison<int>) ((first, next) =>
            {
              int metaScore1 = CardTypes.getCardDefinition(this.ProfileCards[first].id).metaScore;
              int metaScore2 = CardTypes.getCardDefinition(this.ProfileCards[next].id).metaScore;
              return metaScore1 != metaScore2 ? metaScore2.CompareTo(metaScore1) : this.ProfileCards[first].id.CompareTo(this.ProfileCards[next].id);
            }));
            break;
          default:
            this.ProfileCardsSearch.Sort((Comparison<int>) ((first, next) =>
            {
              string descriptionFromCard1 = CardTypes.getDescriptionFromCard(this.ProfileCards[first].id);
              string descriptionFromCard2 = CardTypes.getDescriptionFromCard(this.ProfileCards[next].id);
              return descriptionFromCard1 != descriptionFromCard2 ? descriptionFromCard1.CompareTo(descriptionFromCard2) : this.ProfileCards[first].id.CompareTo(this.ProfileCards[next].id);
            }));
            break;
        }
      }
      this.lastUserCardSearchCriteria = filter;
      this.lastUserCardSortOrder = sort;
      this.lastUserCardNameFilter = namefilter;
    }

    public void searchProfileCardsRedoLast()
    {
      if (this.lastUserCardSearchCriteria == null)
        return;
      this.searchProfileCards(this.lastUserCardSearchCriteria, this.lastUserCardSortOrder, this.lastUserCardNameFilter);
    }

    public void searchProfileCardsRedoLast(string nameFilter)
    {
      if (this.lastUserCardSearchCriteria == null)
        return;
      this.searchProfileCards(this.lastUserCardSearchCriteria, this.lastUserCardSortOrder, nameFilter);
    }

    public bool filterCard(CardTypes.CardDefinition filter, CardTypes.CardDefinition card)
    {
      if ((filter.cardCategory != 7 || card.id != 2689 && card.id != 2690) && filter.cardCategory != 0 && filter.cardCategory != card.cardCategory && (filter.cardCategory != 9 || card.cardCategory != 6 && card.cardCategory != 7) || filter.cardColour != 0 && filter.cardColour != card.cardColour || filter.cardRank != 0 && filter.cardRank < card.cardRank || filter.cardFilter != 0 && filter.cardFilter != card.cardFilter || card.rewardcard && (!filter.rewardcard || card.worldID != RemoteServices.Instance.ProfileWorldID))
        return false;
      if (filter.keywords.Length > 0)
      {
        bool flag = false;
        foreach (string str in filter.keywords.Split(",".ToCharArray()))
        {
          if (card.keywords.Contains(str))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          return false;
      }
      return true;
    }

    public int countPlayableCardsInFilter(int filter)
    {
      int num = 0;
      foreach (int key in this.ProfileCards.Keys)
      {
        if (CardTypes.isCardInNewCategory(this.ProfileCards[key].id, filter))
          ++num;
      }
      return num;
    }

    public int countPlayableCardsInCardSection(int category)
    {
      int num = 0;
      if (this.ProfileCards != null && this.ProfileCards.Values != null)
      {
        foreach (CardTypes.CardDefinition cardDefinition in this.ProfileCards.Values)
        {
          if (cardDefinition != null)
          {
            if (cardDefinition.cardCategory != category)
            {
              switch (category)
              {
                case 0:
                  break;
                case 9:
                  if ((cardDefinition.cardCategory == 6 || cardDefinition.cardCategory == 7) && cardDefinition.name != "CARDTYPE_FLAG")
                  {
                    ++num;
                    continue;
                  }
                  continue;
                default:
                  continue;
              }
            }
            ++num;
          }
        }
      }
      return num;
    }

    public void CardPlayed(int section, int type, int villageid)
    {
      GameEngine.Instance.World.handleQuestObjectiveHappening_PlayedCard(CardTypes.getCardType(type));
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      switch (CardTypes.getCardType(type))
      {
        case 257:
        case 258:
        case 259:
          flag2 = true;
          break;
        case 513:
        case 514:
        case 515:
        case 516:
        case 517:
        case 518:
        case 519:
        case 520:
        case 521:
        case 522:
        case 523:
        case 524:
        case 525:
        case 526:
        case 527:
        case 528:
        case 529:
        case 530:
        case 531:
        case 532:
        case 533:
        case 534:
        case 535:
        case 536:
        case 537:
        case 538:
        case 539:
        case 540:
        case 541:
        case 542:
        case 769:
        case 770:
        case 771:
        case 772:
        case 773:
        case 774:
        case 775:
        case 776:
        case 777:
        case 778:
        case 779:
        case 780:
        case 781:
        case 782:
        case 783:
        case 1025:
        case 1026:
        case 1027:
        case 1028:
        case 1029:
        case 1030:
        case 1031:
        case 1032:
        case 1033:
        case 1034:
        case 1035:
        case 1036:
        case 1037:
        case 1038:
        case 1039:
        case 1281:
        case 1282:
        case 1283:
        case 1284:
        case 1285:
        case 1286:
        case 1287:
        case 1288:
        case 1289:
        case 1290:
        case 1291:
        case 1292:
        case 1293:
        case 1294:
        case 1295:
        case 1296:
        case 1297:
        case 1298:
        case 1299:
        case 1300:
        case 1301:
        case 1302:
        case 1303:
        case 1304:
        case 1305:
        case 1306:
        case 1307:
          flag3 = true;
          break;
        case 1800:
        case 1801:
        case 1802:
          flag2 = true;
          break;
        case 2817:
        case 2818:
        case 2819:
        case 2820:
        case 2821:
        case 2822:
        case 2823:
        case 2824:
        case 2825:
        case 2826:
        case 2827:
        case 2828:
        case 2829:
        case 2830:
        case 2831:
          flag3 = true;
          break;
        case 2945:
        case 2946:
        case 2947:
          flag3 = true;
          break;
        case 2970:
        case 2971:
        case 2972:
        case 2973:
        case 2974:
        case 2975:
          flag3 = true;
          break;
        case 3031:
        case 3032:
        case 3033:
        case 3034:
        case 3035:
        case 3036:
        case 3037:
        case 3038:
        case 3039:
        case 3040:
        case 3041:
        case 3042:
        case 3043:
        case 3044:
        case 3045:
        case 3046:
        case 3047:
        case 3048:
        case 3049:
        case 3050:
        case 3051:
        case 3052:
        case 3053:
        case 3055:
        case 3056:
        case 3057:
        case 3058:
        case 3059:
          flag2 = true;
          break;
        case 3073:
        case 3074:
        case 3075:
          flag4 = true;
          break;
        case 3076:
          flag1 = true;
          break;
        case 3077:
        case 3078:
        case 3079:
        case 3080:
        case 3081:
        case 3082:
        case 3083:
        case 3084:
          flag1 = true;
          break;
        case 3085:
        case 3086:
        case 3087:
        case 3088:
        case 3089:
        case 3090:
        case 3091:
        case 3092:
        case 3093:
        case 3094:
        case 3095:
        case 3096:
        case 3097:
        case 3098:
        case 3099:
        case 3100:
        case 3101:
        case 3102:
        case 3103:
        case 3104:
        case 3105:
        case 3106:
        case 3107:
        case 3108:
        case 3109:
        case 3110:
        case 3111:
        case 3112:
        case 3113:
        case 3114:
        case 3115:
        case 3116:
        case 3117:
        case 3118:
        case 3119:
        case 3120:
        case 3121:
        case 3122:
        case 3123:
        case 3124:
        case 3125:
        case 3126:
        case 3127:
        case 3128:
        case 3129:
        case 3130:
        case 3131:
        case 3132:
        case 3133:
        case 3134:
        case 3135:
        case 3136:
        case 3137:
        case 3138:
        case 3139:
        case 3140:
        case 3141:
        case 3142:
        case 3143:
        case 3144:
        case 3145:
        case 3146:
        case 3147:
        case 3148:
        case 3149:
        case 3150:
        case 3151:
        case 3152:
        case 3153:
        case 3154:
        case 3155:
        case 3156:
        case 3157:
        case 3158:
        case 3159:
        case 3160:
        case 3161:
        case 3162:
        case 3163:
        case 3164:
        case 3165:
        case 3166:
        case 3167:
        case 3168:
        case 3169:
        case 3170:
        case 3171:
        case 3172:
        case 3173:
        case 3174:
        case 3175:
        case 3176:
        case 3177:
        case 3178:
        case 3179:
        case 3180:
          flag2 = true;
          break;
        case 3201:
        case 3202:
        case 3203:
          flag2 = true;
          break;
        case 3233:
        case 3234:
        case 3235:
          flag4 = true;
          break;
        case 3236:
        case 3237:
        case 3238:
          flag2 = true;
          break;
        case 3249:
        case 3250:
        case 3251:
        case 3252:
          flag4 = true;
          break;
        case 3264:
        case 3265:
        case 3266:
        case 3267:
        case 3268:
        case 3269:
        case 3270:
        case 3271:
        case 3272:
        case 3273:
        case 3274:
        case 3275:
        case 3276:
        case 3277:
        case 3278:
        case 3279:
        case 3280:
        case 3281:
        case 3282:
        case 3283:
          flag2 = true;
          break;
        case 3284:
        case 3285:
        case 3286:
          flag2 = true;
          break;
        case 3287:
        case 3288:
        case 3289:
        case 3290:
        case 3291:
        case 3292:
        case 3293:
        case 3294:
        case 3295:
        case 3296:
        case 3297:
        case 3298:
          flag2 = true;
          break;
      }
      if (flag1)
      {
        this.pendingFullRefresh = true;
        this.pendingCardUpdateTime = DateTime.Now;
      }
      else
      {
        if (flag3)
        {
          this.pendingAllVillageRefresh = true;
          this.pendingCardUpdateTime = DateTime.Now;
        }
        else if (flag2 && villageid >= 0)
        {
          this.pendingVillageRefresh = true;
          this.pendingVillageRefreshID = villageid;
          this.pendingCardUpdateTime = DateTime.Now;
        }
        if (flag4)
        {
          this.pendingResearchRefresh = true;
          this.pendingCardUpdateTime = DateTime.Now;
        }
      }
      if (InterfaceMgr.Instance.getCardWindow() == null)
        return;
      CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.getCardWindow());
    }

    private void updateCurrentCardsCallback(UpdateCurrentCards_ReturnType returnData)
    {
      if (!returnData.Success || returnData.m_cardData == null)
        return;
      GameEngine.Instance.cardsManager.UserCardData = returnData.m_cardData;
      GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
      GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
      GameEngine.Instance.World.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
    }

    public void RetrievePremiumOffers()
    {
      if (this.isGettingPremiumOffers || this.PremiumOffers != null)
        return;
      this.isGettingPremiumOffers = true;
      this.PremiumOffers = (PremiumOfferData[]) null;
      XmlRpcPremiumOffersProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath).GetSpecialOffers((IPremiumOffersRequest) new XmlRpcPremiumOffersRequest()
      {
        UserGUID = RemoteServices.Instance.UserGuidProfileSite,
        SessionGUID = RemoteServices.Instance.SessionGuidProfileSite
      }, new PremiumOffersEndResponseDelegate(this.getSpecialOffersCallback), (Control) null);
    }

    private void getSpecialOffersCallback(
      IPremiumOffersProvider sender,
      IPremiumOffersResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) != 0)
      {
        try
        {
          this.PremiumOffers = ((XmlRpcPremiumOffersResponse) response).Offers.ToArray();
          this.PremiumOffersViewed = false;
        }
        catch (Exception ex)
        {
          this.PremiumOffers = new PremiumOfferData[0];
        }
      }
      else
        this.PremiumOffers = new PremiumOfferData[0];
      this.isGettingPremiumOffers = false;
    }

    public void purchasePremiumOffer()
    {
    }

    private void purchasePremiumOfferCallback()
    {
    }

    public void ResetPremiumOffers()
    {
      this.PremiumOffers = (PremiumOfferData[]) null;
      this.PremiumOffersViewed = true;
    }

    public bool ShowPremiumOfferAlert()
    {
      return this.PremiumOffers != null && this.PremiumOffers.Length > 0 && !this.isGettingPremiumOffers && !this.PremiumOffersViewed;
    }

    public bool PremiumOfferAvailable()
    {
      if (this.PremiumOffers == null || this.PremiumOffers.Length == 0)
        return false;
      foreach (PremiumOfferData premiumOffer in this.PremiumOffers)
      {
        if (!premiumOffer.HasBeenPurchased && premiumOffer.ExpirationDate > VillageMap.getCurrentServerTime())
          return true;
      }
      return false;
    }

    public void postcardPlayUpdate()
    {
      if ((DateTime.Now - this.pendingCardUpdateTime).TotalSeconds < 2.0)
        return;
      if (this.pendingFullRefresh)
      {
        GameEngine.Instance.flushVillages();
        GameEngine.Instance.World.doFullTick(true, 0);
      }
      else
      {
        bool flag = false;
        if (this.pendingAllVillageRefresh)
        {
          GameEngine.Instance.flushVillages();
          GameEngine.Instance.downloadCurrentVillage();
          Thread.Sleep(200);
          flag = true;
        }
        else if (this.pendingVillageRefresh && this.pendingVillageRefreshID >= 0)
        {
          GameEngine.Instance.flushVillage(this.pendingVillageRefreshID);
          GameEngine.Instance.downloadCurrentVillage();
          Thread.Sleep(200);
          flag = true;
        }
        if (this.pendingResearchRefresh)
        {
          GameEngine.Instance.World.updateResearch(true);
          Thread.Sleep(200);
          flag = true;
        }
        if (flag)
        {
          RemoteServices.Instance.set_UpdateCurrentCards_UserCallBack(new RemoteServices.UpdateCurrentCards_UserCallBack(this.updateCurrentCardsCallback));
          RemoteServices.Instance.UpdateCurrentCards();
        }
      }
      this.pendingFullRefresh = false;
      this.pendingAllVillageRefresh = false;
      this.pendingVillageRefresh = false;
      this.pendingVillageRefreshID = -1;
      this.pendingResearchRefresh = false;
    }

    public void calcAvailableCategories()
    {
      foreach (CardTypes.CardDefinition card in CardTypes.cardList)
      {
        if (card.cardRank > 0 && card.cardRarity > 0 && card.available == 1)
        {
          if (CardTypes.isCardInNewCategory(card.id, 16390))
            this.NewCategoriesAvailable_Salt = true;
          if (CardTypes.isCardInNewCategory(card.id, 16391))
            this.NewCategoriesAvailable_Spice = true;
          if (CardTypes.isCardInNewCategory(card.id, 16392))
            this.NewCategoriesAvailable_Silk = true;
          if (CardTypes.isCardInNewCategory(card.id, 32773))
            this.NewCategoriesAvailable_Catapults = true;
          if (CardTypes.isCardInNewCategory(card.id, 131077))
            this.NewCategoriesAvailable_Strategy = true;
          if (CardTypes.isCardInNewCategory(card.id, 262151))
            this.NewCategoriesAvailable_Capacity = true;
          if (CardTypes.isCardInNewCategory(card.id, 524288))
            this.NewCategoriesAvailable_Parish = true;
        }
      }
      if (!this.NewCategoriesAvailable_Parish || !this.NewCategoriesAvailable_Capacity && (!this.NewCategoriesAvailable_Salt || !this.NewCategoriesAvailable_Spice || !this.NewCategoriesAvailable_Silk))
        return;
      this.NewCategoriesAvailable_FullHeight = true;
    }

    public int countCardsInCategory(int newCategory)
    {
      if (newCategory == this.lastCountedCategory)
        return this.lastCountedCategoryValue;
      int num = 0;
      foreach (int key in this.ProfileCards.Keys)
      {
        if (CardTypes.isCardInNewCategory(this.ProfileCards[key].id, newCategory) || newCategory == 1048576 && GameEngine.Instance.cardsManager.recentCards.Contains(this.ProfileCards[key].id) || newCategory == 2097152 && this.ProfileCards[key].cardRank <= GameEngine.Instance.World.getRank() + 1)
          ++num;
      }
      this.lastCountedCategoryValue = num;
      this.lastCountedCategory = newCategory;
      return num;
    }

    public int getUserCardIDByDefinition(CardTypes.CardDefinition def)
    {
      foreach (KeyValuePair<int, CardTypes.CardDefinition> profileCard in this.ProfileCards)
      {
        if (def.id == profileCard.Value.id)
          return profileCard.Key;
      }
      return 0;
    }

    public bool HasCardAndCanPlayIt(int cardType)
    {
      List<int> inPlayCardSlots = GameEngine.Instance.cardsManager.getInPlayCardSlots();
      CardTypes.CardDefinition cardDefinition = CardTypes.getCardDefinition(cardType);
      int basicUniqueCardType = CardTypes.getBasicUniqueCardType(cardDefinition.id);
      List<int> cardIdsByDefinition = GameEngine.Instance.cardsManager.getAllUserCardIDsByDefinition(cardDefinition);
      bool flag1 = basicUniqueCardType != -1 && inPlayCardSlots.Contains(basicUniqueCardType);
      bool flag2 = cardIdsByDefinition.Count == 0;
      return !flag1 && !flag2;
    }

    public List<int> getAllUserCardIDsByDefinition(CardTypes.CardDefinition def)
    {
      List<int> cardIdsByDefinition = new List<int>();
      foreach (KeyValuePair<int, CardTypes.CardDefinition> profileCard in this.ProfileCards)
      {
        if (def.id == profileCard.Value.id)
          cardIdsByDefinition.Add(profileCard.Key);
      }
      return cardIdsByDefinition;
    }

    public static string translateCardError(string message, int cardType)
    {
      return CardsManager.translateCardError(message, cardType, -1);
    }

    public static string translateCardError(string message, int cardType, int altMethod)
    {
      SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play");
      if (message.Contains("More than one of this card (or this type of card) may not be played at the same time."))
        return SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_17", "More than one of this card (or this type of card) may not be played at the same time.");
      if (message.Contains("Troop type not researched.") || altMethod == 5)
        return SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_18", "Troop type not researched.");
      if (message.Contains("Not enough space in the barracks for those troops.") || altMethod == 1)
        return SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15", "Not enough space in the barracks for those troops.");
      if (message.Contains("No Room for Merchants.") || altMethod == 4)
        return SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_19", "No Room for Merchants.");
      if (message.Contains("No walls under construction."))
        return SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_20", "No walls under construction.");
      if (message.Contains("No moat under construction"))
        return SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_21", "No moat under construction");
      if (message.Contains("No pits under construction"))
        return SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_22", "No pits under construction");
      if (message.Contains("No room for Monks") || altMethod == 3)
        return SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_23", "No room for Monks");
      if (message.Contains("No room for Scouts") || altMethod == 2)
        return SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_24", "No room for Scouts");
      if (message.Contains("Nothing under construction"))
        return SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_25", "Nothing under construction");
      if (message.Contains("No current building queue"))
        return SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_12", "No current building queue");
      if (message.Contains("No current Research"))
        return SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_11", "No current Research");
      if (message.Contains("Premium card already in play"))
        return SK.Text("RETURNED_CARD_ERROR_6", "Premium token already in play");
      if (!message.Contains("Player Rank too low"))
        return message;
      return SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_8", "Player Rank too low");
    }

    public static string translateErrorShort(string message)
    {
      if (message.Contains("More than one of this card (or this type of card) may not be played at the same time."))
        return SK.Text("RETURNED_CARD_ERROR_17", "More than one of this card (or this type of card) may not be played at the same time.");
      if (message.Contains("Troop type not researched."))
        return SK.Text("RETURNED_CARD_ERROR_18", "Troop type not researched.");
      if (message.Contains("Not enough space in the barracks for those troops."))
        return SK.Text("RETURNED_CARD_ERROR_15", "Not enough space in the barracks for those troops.");
      if (message.Contains("No Room for Merchants."))
        return SK.Text("RETURNED_CARD_ERROR_19", "No Room for Merchants.");
      if (message.Contains("No walls under construction."))
        return SK.Text("RETURNED_CARD_ERROR_20", "No walls under construction.");
      if (message.Contains("No moat under construction"))
        return SK.Text("RETURNED_CARD_ERROR_21", "No moat under construction");
      if (message.Contains("No pits under construction"))
        return SK.Text("RETURNED_CARD_ERROR_22", "No pits under construction");
      if (message.Contains("No room for Monks"))
        return SK.Text("RETURNED_CARD_ERROR_23", "No room for Monks");
      if (message.Contains("No room for Scouts"))
        return SK.Text("RETURNED_CARD_ERROR_24", "No room for Scouts");
      if (message.Contains("Nothing under construction"))
        return SK.Text("RETURNED_CARD_ERROR_25", "Nothing under construction");
      if (message.Contains("No current building queue"))
        return SK.Text("RETURNED_CARD_ERROR_12", "No current building queue");
      if (message.Contains("No current Research"))
        return SK.Text("RETURNED_CARD_ERROR_11", "No current Research");
      if (message.Contains("Premium card already in play"))
        return SK.Text("RETURNED_CARD_ERROR_6", "Premium token already in play");
      return message.Contains("Player Rank too low") ? SK.Text("RETURNED_CARD_ERROR_8", "Player Rank too low") : message;
    }

    public List<int> getInPlayCardSlots()
    {
      List<int> inPlayCardSlots = new List<int>();
      foreach (int card in this.UserCardData.cards)
      {
        int basicUniqueCardType = CardTypes.getBasicUniqueCardType(CardTypes.getCardType(card));
        if (!inPlayCardSlots.Contains(basicUniqueCardType))
          inPlayCardSlots.Add(basicUniqueCardType);
      }
      return inPlayCardSlots;
    }
  }
}
