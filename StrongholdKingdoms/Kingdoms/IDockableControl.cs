// Decompiled with JetBrains decompiler
// Type: Kingdoms.IDockableControl
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public interface IDockableControl
  {
    void initProperties(bool dockable, string title, ContainerControl parent);

    void display(ContainerControl parent, int x, int y);

    void display(bool asPopup, ContainerControl parent, int x, int y);

    void controlDockToggle();

    void closeControl(bool includePopups);

    bool isVisible();

    bool isPopup();
  }
}
