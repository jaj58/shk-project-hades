// Decompiled with JetBrains decompiler
// Type: Kingdoms.ClickLock
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;

//#nullable disable
namespace Kingdoms
{
  public class ClickLock
  {
    private DateTime lastClick = DateTime.MinValue;
    private bool inClick;

    public bool canCall()
    {
      if (this.inClick && (DateTime.Now - this.lastClick).TotalSeconds < 45.0)
        return false;
      this.inClick = true;
      this.lastClick = DateTime.Now;
      return true;
    }

    public void called() => this.inClick = false;

    public bool IsInCooldown()
    {
      return this.inClick && (DateTime.Now - this.lastClick).TotalSeconds < 45.0;
    }
  }
}
