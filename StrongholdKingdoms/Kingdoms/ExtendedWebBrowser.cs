// Decompiled with JetBrains decompiler
// Type: Kingdoms.ExtendedWebBrowser
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ExtendedWebBrowser : WebBrowser
  {
    private const int WM_PARENTNOTIFY = 528;
    private const int WM_DESTROY = 2;

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 528)
      {
        try
        {
          if (!this.DesignMode)
          {
            if (m.WParam.ToInt32() == 2)
            {
             // new Message().Msg = 2;
              ((Form) this.Parent).Close();
            }
          }
        }
        catch (Exception ex)
        {
        }
        this.DefWndProc(ref m);
      }
      else
        base.WndProc(ref m);
    }
  }
}
