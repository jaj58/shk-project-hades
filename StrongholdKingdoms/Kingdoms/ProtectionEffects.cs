// Decompiled with JetBrains decompiler
// Type: Kingdoms.ProtectionEffects
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;

//#nullable disable
namespace Kingdoms
{
  public class ProtectionEffects
  {
    public bool isUserVillage;
    public DateTime expireTime;
    public ProtectionEffects.protectionType Type;

    public TimeSpan remaining => this.expireTime - VillageMap.getCurrentServerTime();

    public override string ToString()
    {
      switch (this.Type)
      {
        case ProtectionEffects.protectionType.INTERDICTED:
          return SK.Text("TOUCH_Y_INTERDICTED", "Interdicted") + " " + VillageMap.createBuildTimeString((int) this.remaining.TotalSeconds);
        case ProtectionEffects.protectionType.PEACETIME:
          return SK.Text("TOUCH_Y_PEACETIME", "Peace Time") + " " + VillageMap.createBuildTimeString((int) this.remaining.TotalSeconds);
        case ProtectionEffects.protectionType.EXCOMMUNICATED:
          return this.isUserVillage ? SK.Text("OtherVillagePanel_Excom", "Excommunicated") + " " + VillageMap.createBuildTimeString((int) this.remaining.TotalSeconds) : SK.Text("OtherVillagePanel_Excom", "Excommunicated");
        case ProtectionEffects.protectionType.VACATION:
          return SK.Text("VACATION_CANCEL_HEADER", "Vacation Mode is Active");
        default:
          return SK.Text("TOUCH_Y_NoProtection", "No Protection");
      }
    }

    public enum protectionType
    {
      NONE,
      INTERDICTED,
      PEACETIME,
      EXCOMMUNICATED,
      VACATION,
    }
  }
}
