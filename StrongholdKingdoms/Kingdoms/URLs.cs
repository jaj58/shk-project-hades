// Decompiled with JetBrains decompiler
// Type: Kingdoms.URLs
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

//#nullable disable
namespace Kingdoms
{
  public class URLs
  {
    public static string GameRPCAddress = "";
    public static string ChatRPCAddress = "";
    public static string ProfileProtocol = "http";
    public static string WebProtocol = "https";
    public static string ProfilePath = "/services/auth.php";
    public static string ProfileCardPath = "/services/cardserver.php";
    public static string ProfileBPPath = "/services/bpEndpoint.php";
    public static string ProfilePresetsPath = "/services/auth.php";
    public static string ProfileServerAddressCMS = "news.strongholdkingdoms.com";
    public static string ProfileNewOffersPath = "/kingdoms/viewRedeemedOffers.php";
    public static string ProfileServerPort = "80";
    public static string ProfileServerAddressCards = "cardsxml.prd.external.fireflyops.com";
    public static string ProfileServerAddressBigpoint = "cardsxml.prd.external.fireflyops.com";
    public static string ProfileServerAddressWeb = "login.strongholdkingdoms.com";
    public static string ProfileServerAddressLogin = "login.strongholdkingdoms.com";
    public static string ProfileServerAddressPresets = "login.strongholdkingdoms.com";
    public static string ProfilePaymentURL = URLs.WebProtocol + "://login.strongholdkingdoms.com/pay.php";
    public static string AccountInfoURL = URLs.WebProtocol + "://login.strongholdkingdoms.com/kingdoms/account.php";
    public static string InviteAFriendURL = URLs.WebProtocol + "://login.strongholdkingdoms.com/invitefriend";

    public static string AccountMainPage
    {
      get => URLs.WebProtocol + "://" + URLs.ProfileServerAddressWeb + "/kingdoms/account.php";
    }

    public static string AccountOffersPage
    {
      get => URLs.WebProtocol + "://" + URLs.ProfileServerAddressWeb + URLs.ProfileNewOffersPath;
    }

    public static string ForgottenPasswordLink
    {
      get => URLs.WebProtocol + "://" + URLs.ProfileServerAddressWeb + "/kingdoms/forgotten.php";
    }

    public static string shieldDesignerURL
    {
      get => URLs.WebProtocol + "://" + URLs.ProfileServerAddressWeb + "/shield/shield.html";
    }

    public static string ServerNewsFeed
    {
      get
      {
        return URLs.WebProtocol + "://" + URLs.ProfileServerAddressCMS + "/servernews.php?lang=" + Program.mySettings.LanguageIdent;
      }
    }

    public static string NewsMainPage
    {
      get
      {
        return !Program.bigpointPartnerInstall ? URLs.WebProtocol + "://" + URLs.ProfileServerAddressCMS + "/clientnews.php?lang=" + Program.mySettings.LanguageIdent : URLs.WebProtocol + "://" + URLs.ProfileServerAddressCMS + "/clientnews.php?lang=" + Program.mySettings.LanguageIdent + "&noads=1";
      }
    }

    public static string NewsMainPageBrowserVersion
    {
      get
      {
        return !Program.bigpointPartnerInstall ? URLs.WebProtocol + "://" + URLs.ProfileServerAddressCMS + "/frontpagenews.php?lang=" + Program.mySettings.LanguageIdent : URLs.WebProtocol + "://" + URLs.ProfileServerAddressCMS + "/frontpagenews.php?lang=" + Program.mySettings.LanguageIdent + "&noads=1";
      }
    }

    public static string GameRPC
    {
      get => "http://" + URLs.GameRPCAddress + ":80/KingdomsRPC/Kingdoms.rem";
    }

    public static string ChatRPC
    {
      get => "http://" + URLs.ChatRPCAddress + ":80/KingdomsChatRPC/KingdomsChat.rem";
    }

    public static string FireflyHomepage => URLs.WebProtocol + "://www.fireflyworlds.com";

    public static string ForumHomepage => "https://forum.strongholdkingdoms.com";

    public static string Supportpage
    {
      get
      {
        switch (Program.mySettings.LanguageIdent)
        {
          case "de":
            return "https://support.strongholdkingdoms.com/?de";
          case "fr":
            return "https://support.strongholdkingdoms.com/?fr";
          case "ru":
            return "https://support.strongholdkingdoms.com/?ru";
          case "es":
            return "https://support.strongholdkingdoms.com/?es";
          case "pl":
            return "https://support.strongholdkingdoms.com/?pl";
          case "it":
            return "https://support.strongholdkingdoms.com/?it";
          case "tr":
            return "https://support.strongholdkingdoms.com/?tr";
          default:
            return "https://support.strongholdkingdoms.com/?en";
        }
      }
    }

    public static string WikiPage
    {
      get
      {
        switch (Program.mySettings.LanguageIdent)
        {
          case "de":
            return "https://strongholdkingdoms-de.gamepedia.com";
          case "fr":
            return "https://strongholdkingdoms-fr.gamepedia.com";
          case "ru":
            return "https://strongholdkingdoms-ru.gamepedia.com";
          case "es":
            return "https://strongholdkingdoms-es.gamepedia.com";
          case "pl":
            return "https://strongholdkingdoms-pl.gamepedia.com/";
          case "br":
          case "pt":
            return "https://strongholdkingdoms-pt.gamepedia.com";
          case "it":
            return "https://strongholdkingdoms-it.gamepedia.com";
          case "tr":
            return "https://strongholdkingdoms-tr.gamepedia.com";
          default:
            return "https://strongholdkingdoms.gamepedia.com";
        }
      }
    }

    public static string IPSharingPage
    {
      get
      {
        switch (Program.mySettings.LanguageIdent)
        {
          case "de":
            return URLs.WebProtocol + "://www.strongholdkingdoms.com/spielregeln.html";
          case "fr":
            return URLs.WebProtocol + "://www.strongholdkingdoms.com/ReglesduJeu.html";
          case "ru":
            return URLs.WebProtocol + "://www.strongholdkingdoms.com/GameRules.ru.html";
          case "es":
            return URLs.WebProtocol + "://www.strongholdkingdoms.com/GameRules.es.html";
          case "pl":
            return URLs.WebProtocol + "://www.strongholdkingdoms.com/GameRules.pl.html";
          case "br":
          case "pt":
            return URLs.WebProtocol + "://www.strongholdkingdoms.com/GameRules.pt.html";
          case "it":
            return URLs.WebProtocol + "://www.strongholdkingdoms.com/GameRules.it.html";
          case "tr":
            return URLs.WebProtocol + "://www.strongholdkingdoms.com/GameRules.tr.html";
          case "zh":
            return "https://www.strongholdkingdoms.com/GameRules.sc.html";
          case "zhhk":
            return "https://www.strongholdkingdoms.com/GameRules.tc.html";
          case "ko":
            return "https://www.strongholdkingdoms.com/GameRules.ko.html";
          case "jp":
            return "https://www.strongholdkingdoms.com/GameRules.jp.html";
          default:
            return URLs.WebProtocol + "://www.strongholdkingdoms.com/GameRules.html";
        }
      }
    }

    public static string TermsAndConditions
    {
      get
      {
        switch (Program.mySettings.LanguageIdent)
        {
          case "de":
            return "https://fireflyworlds.com/terms-of-use-de/";
          case "fr":
            return "https://fireflyworlds.com/terms-of-use-fr/";
          case "ru":
            return "https://fireflyworlds.com/terms-of-use-ru/";
          case "es":
            return "https://fireflyworlds.com/terms-of-use-es/";
          case "pl":
            return "https://fireflyworlds.com/terms-of-use-pl/";
          case "br":
          case "pt":
            return "https://fireflyworlds.com/terms-of-use-pt/";
          case "it":
            return "https://fireflyworlds.com/terms-of-use-it/";
          case "tr":
            return "https://fireflyworlds.com/terms-of-use-tr/";
          case "zh":
            return "https://fireflyworlds.com/terms-of-use-sc/";
          case "zhhk":
            return "https://fireflyworlds.com/terms-of-use-tc/";
          case "ko":
            return "https://fireflyworlds.com/terms-of-use-ko/";
          case "jp":
            return "https://fireflyworlds.com/terms-of-use-jp/";
          default:
            return "https://fireflyworlds.com/terms-of-use-en/";
        }
      }
    }

    public static string PrivacyPolicy
    {
      get
      {
        switch (Program.mySettings.LanguageIdent)
        {
          case "de":
            return "https://fireflyworlds.com/privacy-policy-de/";
          case "fr":
            return "https://fireflyworlds.com/privacy-policy-fr/";
          case "ru":
            return "https://fireflyworlds.com/privacy-policy-ru/";
          case "es":
            return "https://fireflyworlds.com/privacy-policy-es/";
          case "pl":
            return "https://fireflyworlds.com/privacy-policy-pl/";
          case "br":
          case "pt":
            return "https://fireflyworlds.com/privacy-policy-pt/";
          case "it":
            return "https://fireflyworlds.com/privacy-policy-it/";
          case "tr":
            return "https://fireflyworlds.com/privacy-policy-tr/";
          case "zh":
            return "https://fireflyworlds.com/privacy-policy-sc/";
          case "zhhk":
            return "https://fireflyworlds.com/privacy-policy-tc/";
          case "ko":
            return "https://fireflyworlds.com/privacy-policy-ko/";
          case "jp":
            return "https://fireflyworlds.com/privacy-policy-jp/";
          default:
            return "https://fireflyworlds.com/privacy-policy/";
        }
      }
    }

    public static string ForgottenPasswordSteam
    {
      get
      {
        switch (Program.mySettings.LanguageIdent)
        {
          case "de":
            return "https://de.strongholdkingdoms.com/forgotten-password/";
          case "fr":
            return "https://fr.strongholdkingdoms.com/forgotten-password/";
          case "ru":
            return "https://ru.strongholdkingdoms.com/forgotten-password/";
          case "es":
            return "https://es.strongholdkingdoms.com/forgotten-password/";
          case "pl":
            return "https://pl.strongholdkingdoms.com/forgotten-password/";
          case "br":
          case "pt":
            return "https://pt.strongholdkingdoms.com/forgotten-password/";
          case "it":
            return "https://it.strongholdkingdoms.com/forgotten-password/";
          case "tr":
            return "https://tr.strongholdkingdoms.com/forgotten-password/";
          case "zh":
            return "https://sc.strongholdkingdoms.com/forgotten-password/";
          case "zhhk":
            return "https://tc.strongholdkingdoms.com/forgotten-password/";
          case "ko":
            return "https://ko.strongholdkingdoms.com/forgotten-password/";
          case "jp":
            return "https://jp.strongholdkingdoms.com/forgotten-password/";
          default:
            return "https://www.strongholdkingdoms.com/forgotten-password/";
        }
      }
    }

    public static string TwitterURL => "https://twitter.com/fireflyworlds";

    public static string FacebookURL
    {
      get
      {
        switch (Program.mySettings.LanguageIdent)
        {
          case "de":
            return URLs.WebProtocol + "://www.facebook.com/strongholdkingdoms";
          case "fr":
            return URLs.WebProtocol + "://www.facebook.com/strongholdkingdoms";
          case "ru":
            return URLs.WebProtocol + "://www.facebook.com/StrongholdKingdoms";
          case "es":
            return URLs.WebProtocol + "://www.facebook.com/strongholdkingdoms";
          default:
            return URLs.WebProtocol + "://www.facebook.com/strongholdkingdoms";
        }
      }
    }

    public static string YoutubeURL
    {
      get
      {
        switch (Program.mySettings.LanguageIdent)
        {
          case "de":
            return URLs.WebProtocol + "://www.youtube.com/user/fireflyworlds?ob=0";
          case "fr":
            return URLs.WebProtocol + "://www.youtube.com/user/fireflyworlds?ob=0";
          case "ru":
            return URLs.WebProtocol + "://www.youtube.com/user/fireflyworlds?ob=0";
          default:
            return URLs.WebProtocol + "://www.youtube.com/user/fireflyworlds?ob=0";
        }
      }
    }
  }
}
