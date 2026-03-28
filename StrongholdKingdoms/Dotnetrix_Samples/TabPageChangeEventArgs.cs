// Decompiled with JetBrains decompiler
// Type: Dotnetrix_Samples.TabPageChangeEventArgs
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.Windows.Forms;

//#nullable disable
namespace Dotnetrix_Samples
{
  public class TabPageChangeEventArgs : EventArgs
  {
    private TabPage _Selected;
    private TabPage _PreSelected;
    public bool Cancel;

    public TabPage CurrentTab => this._Selected;

    public TabPage NextTab => this._PreSelected;

    public TabPageChangeEventArgs(TabPage CurrentTab, TabPage NextTab)
    {
      this._Selected = CurrentTab;
      this._PreSelected = NextTab;
    }
  }
}
