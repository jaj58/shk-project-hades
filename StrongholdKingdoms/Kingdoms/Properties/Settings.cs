// Decompiled with JetBrains decompiler
// Type: Kingdoms.Properties.Settings
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

//#nullable disable
namespace Kingdoms.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default => Settings.defaultInstance;

    [DefaultSettingValue("")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string Username
    {
      get => (string) this[nameof (Username)];
      set => this[nameof (Username)] = (object) value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("")]
    [DebuggerNonUserCode]
    public string Password
    {
      get => (string) this[nameof (Password)];
      set => this[nameof (Password)] = (object) value;
    }

    [DefaultSettingValue("-1")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public int ScreenWidth
    {
      get => (int) this[nameof (ScreenWidth)];
      set => this[nameof (ScreenWidth)] = (object) value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("-1")]
    [DebuggerNonUserCode]
    public int ScreenHeight
    {
      get => (int) this[nameof (ScreenHeight)];
      set => this[nameof (ScreenHeight)] = (object) value;
    }

    [DefaultSettingValue("False")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool LicenseViewed
    {
      get => (bool) this[nameof (LicenseViewed)];
      set => this[nameof (LicenseViewed)] = (object) value;
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    [UserScopedSetting]
    public bool CastleWalls
    {
      get => (bool) this[nameof (CastleWalls)];
      set => this[nameof (CastleWalls)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    public bool LicenseAlpha3Viewed
    {
      get => (bool) this[nameof (LicenseAlpha3Viewed)];
      set => this[nameof (LicenseAlpha3Viewed)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    public bool ShowAdvancedLoginOptions
    {
      get => (bool) this[nameof (ShowAdvancedLoginOptions)];
      set => this[nameof (ShowAdvancedLoginOptions)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string LastLoadedAdvancedLogin
    {
      get => (string) this[nameof (LastLoadedAdvancedLogin)];
      set => this[nameof (LastLoadedAdvancedLogin)] = (object) value;
    }
  }
}
