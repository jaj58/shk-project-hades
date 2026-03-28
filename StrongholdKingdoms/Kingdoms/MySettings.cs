// Decompiled with JetBrains decompiler
// Type: Kingdoms.MySettings
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using DXGraphics;
using System;
using System.IO;
using System.Xml.Serialization;

//#nullable disable
namespace Kingdoms
{
  [Serializable]
  public class MySettings
  {
    public const int LOGINS_UNTILL_POPUP = 3;
    public bool usingPlayerPrefs;
    public bool NeverLoggedIn = true;
    public bool IsGuestAccount;
    public string GuestID = string.Empty;
    public string GuestUsername = string.Empty;
    public string Username = "";
    public string Password = "";
    public int ScreenWidth = -1;
    public int ScreenHeight = -1;
    public bool LicenseViewed;
    public bool LicenseAlpha3Viewed;
    public bool CastleWalls = true;
    public bool NotifyChatUpdate = true;
    public bool ConfirmPlayCard = true;
    public bool BuyMultipleCardPacks = true;
    public bool OpenMultipleCardPacks = true;
    public bool SETTINGS_instantTooltips;
    public int SETTINGS_staticMouseTime = 400;
    public bool SETTINGS_showTooltips = true;
    public string languageIdent = "";
    public string InstalledLanguageIdent = "";
    public bool OwnLanguageAvailableAndChecked;
    public bool Music = true;
    public int MusicVolume = 13;
    public int AAMode;
    public int LastWorldID = -1;
    public bool IsAutomatedBuild;
    public bool AutoLogin;
    public int NumWorldsCount = -1;
    public DateTime NumWorldsLastChanged = DateTime.MinValue;
    public bool HasLoggedIn;
    public bool fastCashIn;
    public bool SFX = true;
    public bool BattleSFX = true;
    public int SFXVolume = 100;
    public bool Environmentals = true;
    public int EnvironmentalVolume = 34;
    public bool Maximize;
    public bool viewVillageIDs;
    public bool viewCapitalIDs;
    public bool showGameFeaturesScreenIcon = true;
    public bool SeasonalSpecialFX = true;
    public bool SeasonalWinterLandscape = true;
    public bool FlashingTaskbarAttack = true;
    public bool ShowProductionInfo = true;
    public bool AdvancedTrading;
    public bool AdvertShown;
    public bool AttackSetupsUpdated;
    public bool UseMapTextBorders = true;
    public bool SendAnalytics = true;
    public bool SeenAnalyticsPrompt;
    public bool DirectMapRendering;
    public bool MapScreenNoUpdate;
    public bool MapScreenNoArmyDraw;
    public bool MapScreenNoArmyUpdate;
    public bool MapScreenNoVillageDraw;
    public bool MapScreenNoVillageUpdate;
    public bool MapScreenNoTiles;
    public bool MapScreenNoText = true;
    public bool OldSprites;
    public bool NoSprites;
    public int LoginCount;
    public bool HasShownFreshSignupInvitation;
    public bool FTUECompleted;
    public bool AdvisorsCompleted;
    public bool DisableParishFTUE;
    public bool DisableAge4Warning;
    public bool DisableAge5Warning;
    public bool DisableAge6Warning;
    public bool DisableAge7Warning;
    public int palette;
    public string facebookaccesstoken = "";
    public int LastContestViewed;
    public string advicePanelsViewed = "";
    public bool adviceEnabled = true;

    public string LanguageIdent
    {
      get => this.languageIdent;
      set
      {
        this.languageIdent = value;
        BaseImage.currentLangSetting = this.languageIdent;
      }
    }

    public bool hasLoggedIn() => this.HasLoggedIn || this.Username.Length > 0;

    public static MySettings load()
    {
      try
      {
        string settingsPath = GameEngine.getSettingsPath(false);
        FileStream input = (FileStream) null;
        BinaryReader binaryReader = (BinaryReader) null;
        try
        {
          input = new FileStream(settingsPath + "\\config.dat", FileMode.Open, FileAccess.Read);
          binaryReader = new BinaryReader((Stream) input);
          MySettings mySettings = new MySettings();
          mySettings.MusicVolume = 13;
          mySettings.SFXVolume = 100;
          mySettings.EnvironmentalVolume = 34;
          mySettings.Username = binaryReader.ReadString();
          mySettings.Password = binaryReader.ReadString();
          mySettings.ScreenWidth = binaryReader.ReadInt32();
          mySettings.ScreenHeight = binaryReader.ReadInt32();
          mySettings.LicenseViewed = binaryReader.ReadBoolean();
          mySettings.LicenseAlpha3Viewed = binaryReader.ReadBoolean();
          mySettings.CastleWalls = binaryReader.ReadBoolean();
          mySettings.NotifyChatUpdate = binaryReader.ReadBoolean();
          try
          {
            mySettings.ConfirmPlayCard = binaryReader.ReadBoolean();
            mySettings.SETTINGS_instantTooltips = binaryReader.ReadBoolean();
            mySettings.SETTINGS_staticMouseTime = binaryReader.ReadInt32();
            mySettings.SETTINGS_showTooltips = binaryReader.ReadBoolean();
            mySettings.LanguageIdent = binaryReader.ReadString();
            mySettings.OwnLanguageAvailableAndChecked = binaryReader.ReadBoolean();
            mySettings.BuyMultipleCardPacks = binaryReader.ReadBoolean();
            mySettings.OpenMultipleCardPacks = binaryReader.ReadBoolean();
            mySettings.Music = binaryReader.ReadBoolean();
            mySettings.MusicVolume = binaryReader.ReadInt32();
            mySettings.AAMode = binaryReader.ReadInt32();
            mySettings.LastWorldID = binaryReader.ReadInt32();
            mySettings.AutoLogin = binaryReader.ReadBoolean();
            mySettings.NumWorldsCount = binaryReader.ReadInt32();
            mySettings.NumWorldsLastChanged = new DateTime(binaryReader.ReadInt64());
            mySettings.HasLoggedIn = binaryReader.ReadBoolean();
            mySettings.fastCashIn = binaryReader.ReadBoolean();
            mySettings.SFX = binaryReader.ReadBoolean();
            mySettings.SFXVolume = binaryReader.ReadInt32();
            mySettings.Environmentals = binaryReader.ReadBoolean();
            mySettings.EnvironmentalVolume = binaryReader.ReadInt32();
            mySettings.Maximize = binaryReader.ReadBoolean();
            if (mySettings.MusicVolume < 0)
            {
              mySettings.MusicVolume = binaryReader.ReadInt32();
              mySettings.SFXVolume = binaryReader.ReadInt32();
              mySettings.EnvironmentalVolume = binaryReader.ReadInt32();
            }
            else
            {
              if (mySettings.MusicVolume > 13)
                mySettings.MusicVolume = 13;
              mySettings.SFXVolume = 100;
              mySettings.EnvironmentalVolume = 34;
            }
            mySettings.BattleSFX = binaryReader.ReadBoolean();
            mySettings.viewVillageIDs = binaryReader.ReadBoolean();
            mySettings.showGameFeaturesScreenIcon = binaryReader.ReadBoolean();
            mySettings.SeasonalSpecialFX = binaryReader.ReadBoolean();
            try
            {
              mySettings.InstalledLanguageIdent = binaryReader.ReadString();
            }
            catch (Exception ex)
            {
              mySettings.InstalledLanguageIdent = Program.installedLangCode;
            }
            try
            {
              mySettings.FlashingTaskbarAttack = binaryReader.ReadBoolean();
            }
            catch
            {
              mySettings.FlashingTaskbarAttack = true;
            }
            try
            {
              mySettings.ShowProductionInfo = binaryReader.ReadBoolean();
            }
            catch
            {
              mySettings.ShowProductionInfo = true;
            }
            try
            {
              mySettings.AdvancedTrading = binaryReader.ReadBoolean();
            }
            catch
            {
              mySettings.AdvancedTrading = false;
            }
            try
            {
              mySettings.AdvertShown = binaryReader.ReadBoolean();
            }
            catch
            {
              mySettings.AdvertShown = false;
            }
            try
            {
              mySettings.viewCapitalIDs = binaryReader.ReadBoolean();
            }
            catch
            {
              mySettings.viewCapitalIDs = false;
            }
            try
            {
              mySettings.AttackSetupsUpdated = binaryReader.ReadBoolean();
            }
            catch
            {
              mySettings.AttackSetupsUpdated = false;
            }
            try
            {
              mySettings.SeasonalWinterLandscape = binaryReader.ReadBoolean();
            }
            catch
            {
              mySettings.SeasonalWinterLandscape = true;
            }
            try
            {
              mySettings.UseMapTextBorders = binaryReader.ReadBoolean();
            }
            catch
            {
              mySettings.UseMapTextBorders = true;
            }
            try
            {
              mySettings.facebookaccesstoken = binaryReader.ReadString();
            }
            catch
            {
              mySettings.facebookaccesstoken = "";
            }
            try
            {
              mySettings.SendAnalytics = binaryReader.ReadBoolean();
            }
            catch
            {
              mySettings.SendAnalytics = true;
            }
            try
            {
              mySettings.SeenAnalyticsPrompt = binaryReader.ReadBoolean();
            }
            catch
            {
              mySettings.SeenAnalyticsPrompt = false;
            }
            try
            {
              mySettings.GuestID = binaryReader.ReadString();
            }
            catch
            {
              mySettings.GuestID = "";
            }
            try
            {
              mySettings.IsGuestAccount = binaryReader.ReadBoolean();
            }
            catch
            {
              mySettings.IsGuestAccount = false;
            }
            try
            {
              mySettings.NeverLoggedIn = binaryReader.ReadBoolean();
            }
            catch
            {
              mySettings.NeverLoggedIn = true;
            }
            try
            {
              mySettings.GuestUsername = binaryReader.ReadString();
            }
            catch
            {
              mySettings.GuestUsername = string.Empty;
            }
            try
            {
              mySettings.LastContestViewed = binaryReader.ReadInt32();
            }
            catch
            {
              mySettings.LastContestViewed = 0;
            }
            try
            {
              mySettings.advicePanelsViewed = binaryReader.ReadString();
              mySettings.adviceEnabled = binaryReader.ReadBoolean();
            }
            catch
            {
              mySettings.advicePanelsViewed = string.Empty;
              mySettings.adviceEnabled = true;
            }
          }
          catch (Exception ex)
          {
            mySettings.BattleSFX = mySettings.SFX;
          }
          binaryReader.Close();
          input.Close();
          if (mySettings.Username != null && mySettings.Username.Length > 0 || mySettings.Password != null && mySettings.Password.Length > 0)
            mySettings.NeverLoggedIn = false;
          return mySettings;
        }
        catch (Exception ex1)
        {
          try
          {
            binaryReader?.Close();
          }
          catch (Exception ex2)
          {
          }
          try
          {
            input?.Close();
          }
          catch (Exception ex3)
          {
          }
        }
        FileStream fileStream = (FileStream) null;
        XmlSerializer xmlSerializer = new XmlSerializer(typeof (MySettings));
        try
        {
          fileStream = new FileStream(settingsPath + "\\settings.dat", FileMode.Open, FileAccess.Read);
          MySettings mySettings = (MySettings) xmlSerializer.Deserialize((Stream) fileStream);
          fileStream.Close();
          return mySettings;
        }
        catch (FileNotFoundException ex)
        {
          return new MySettings();
        }
        catch (DirectoryNotFoundException ex)
        {
          return new MySettings();
        }
        catch (Exception ex4)
        {
          try
          {
            fileStream?.Close();
          }
          catch (Exception ex5)
          {
          }
          return new MySettings();
        }
      }
      catch (Exception ex)
      {
        return new MySettings();
      }
    }

    public static void Reset()
    {
      string settingsPath = GameEngine.getSettingsPath(true);
      try
      {
        string path = settingsPath + "\\config.dat";
        if (!File.Exists(path))
          return;
        File.Delete(path);
      }
      catch (Exception ex)
      {
        UniversalDebugLog.Log("Exception when resetting " + ex.ToString());
      }
    }

    public void Save()
    {
      string settingsPath = GameEngine.getSettingsPath(true);
      try
      {
        new FileInfo(settingsPath + "\\config.dat").IsReadOnly = false;
      }
      catch (Exception ex)
      {
      }
      int num = -1;
      FileStream output = (FileStream) null;
      BinaryWriter binaryWriter = (BinaryWriter) null;
      try
      {
        output = new FileStream(settingsPath + "\\config.dat", FileMode.Create);
        binaryWriter = new BinaryWriter((Stream) output);
        binaryWriter.Write(this.Username);
        binaryWriter.Write(this.Password);
        binaryWriter.Write(this.ScreenWidth);
        binaryWriter.Write(this.ScreenHeight);
        binaryWriter.Write(this.LicenseViewed);
        binaryWriter.Write(this.LicenseAlpha3Viewed);
        binaryWriter.Write(this.CastleWalls);
        binaryWriter.Write(this.NotifyChatUpdate);
        binaryWriter.Write(this.ConfirmPlayCard);
        binaryWriter.Write(this.SETTINGS_instantTooltips);
        binaryWriter.Write(this.SETTINGS_staticMouseTime);
        binaryWriter.Write(this.SETTINGS_showTooltips);
        binaryWriter.Write(this.LanguageIdent);
        binaryWriter.Write(this.OwnLanguageAvailableAndChecked);
        binaryWriter.Write(this.BuyMultipleCardPacks);
        binaryWriter.Write(this.OpenMultipleCardPacks);
        binaryWriter.Write(this.Music);
        binaryWriter.Write(num);
        binaryWriter.Write(this.AAMode);
        binaryWriter.Write(this.LastWorldID);
        binaryWriter.Write(this.AutoLogin);
        binaryWriter.Write(this.NumWorldsCount);
        binaryWriter.Write(this.NumWorldsLastChanged.Ticks);
        binaryWriter.Write(this.HasLoggedIn);
        binaryWriter.Write(this.fastCashIn);
        binaryWriter.Write(this.SFX);
        binaryWriter.Write(num);
        binaryWriter.Write(this.Environmentals);
        binaryWriter.Write(num);
        binaryWriter.Write(this.Maximize);
        binaryWriter.Write(this.MusicVolume);
        binaryWriter.Write(this.SFXVolume);
        binaryWriter.Write(this.EnvironmentalVolume);
        binaryWriter.Write(this.BattleSFX);
        binaryWriter.Write(this.viewVillageIDs);
        binaryWriter.Write(this.showGameFeaturesScreenIcon);
        binaryWriter.Write(this.SeasonalSpecialFX);
        binaryWriter.Write(this.InstalledLanguageIdent);
        binaryWriter.Write(this.FlashingTaskbarAttack);
        binaryWriter.Write(this.ShowProductionInfo);
        binaryWriter.Write(this.AdvancedTrading);
        binaryWriter.Write(this.AdvertShown);
        binaryWriter.Write(this.viewCapitalIDs);
        binaryWriter.Write(this.AttackSetupsUpdated);
        binaryWriter.Write(this.SeasonalWinterLandscape);
        binaryWriter.Write(this.UseMapTextBorders);
        binaryWriter.Write(this.facebookaccesstoken);
        binaryWriter.Write(this.SendAnalytics);
        binaryWriter.Write(this.SeenAnalyticsPrompt);
        binaryWriter.Write(this.GuestID);
        binaryWriter.Write(this.IsGuestAccount);
        binaryWriter.Write(this.NeverLoggedIn);
        binaryWriter.Write(this.GuestUsername);
        binaryWriter.Write(this.LastContestViewed);
        binaryWriter.Write(this.advicePanelsViewed);
        binaryWriter.Write(this.adviceEnabled);
        binaryWriter.Close();
        output.Close();
      }
      catch (Exception ex1)
      {
        try
        {
          binaryWriter?.Close();
        }
        catch (Exception ex2)
        {
        }
        try
        {
          output?.Close();
        }
        catch (Exception ex3)
        {
        }
      }
    }
  }
}
