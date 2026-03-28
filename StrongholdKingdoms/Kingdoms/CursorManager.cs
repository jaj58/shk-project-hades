// Decompiled with JetBrains decompiler
// Type: Kingdoms.CursorManager
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class CursorManager
  {
    public static CursorManager.CursorType CurrentCursor = CursorManager.CursorType.Default;

    public static void SetCursor(CursorManager.CursorType type, Form ParentForm)
    {
      if (ParentForm == null)
        return;
      switch (type)
      {
        case CursorManager.CursorType.WaitCursor:
          ParentForm.Cursor = Cursors.WaitCursor;
          break;
        case CursorManager.CursorType.Default:
          ParentForm.Cursor = Cursors.Default;
          break;
        case CursorManager.CursorType.Hand:
          ParentForm.Cursor = Cursors.Hand;
          break;
        case CursorManager.CursorType.SizeWE:
          ParentForm.Cursor = Cursors.SizeWE;
          break;
        case CursorManager.CursorType.Cross:
          ParentForm.Cursor = Cursors.Cross;
          break;
        case CursorManager.CursorType.VSplit:
          ParentForm.Cursor = Cursors.VSplit;
          break;
      }
      CursorManager.CurrentCursor = type;
    }

    public enum CursorType
    {
      WaitCursor,
      Default,
      Hand,
      SizeWE,
      Cross,
      VSplit,
    }
  }
}
