// Decompiled with JetBrains decompiler
// Type: Kingdoms.IDockWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public interface IDockWindow
  {
    void AddControl(UserControl control, int x, int y);

    void RemoveControl(UserControl control);
  }
}
