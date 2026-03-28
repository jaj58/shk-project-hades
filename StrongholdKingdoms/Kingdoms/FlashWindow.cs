// Decompiled with JetBrains decompiler
// Type: Kingdoms.FlashWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public static class FlashWindow
  {
    public const uint FLASHW_STOP = 0;
    public const uint FLASHW_CAPTION = 1;
    public const uint FLASHW_TRAY = 2;
    public const uint FLASHW_ALL = 3;
    public const uint FLASHW_TIMER = 4;
    public const uint FLASHW_TIMERNOFG = 12;

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool FlashWindowEx(ref FlashWindow.FLASHWINFO pwfi);

    public static bool Flash(Form form)
    {
      if (!FlashWindow.Win2000OrLater || form == null)
        return false;
      FlashWindow.FLASHWINFO flashwinfo = FlashWindow.Create_FLASHWINFO(form.Handle, 15U, uint.MaxValue, 0U);
      return FlashWindow.FlashWindowEx(ref flashwinfo);
    }

    private static FlashWindow.FLASHWINFO Create_FLASHWINFO(
      IntPtr handle,
      uint flags,
      uint count,
      uint timeout)
    {
      FlashWindow.FLASHWINFO structure = new FlashWindow.FLASHWINFO();
      structure.cbSize = Convert.ToUInt32(Marshal.SizeOf((object) structure));
      structure.hwnd = handle;
      structure.dwFlags = flags;
      structure.uCount = count;
      structure.dwTimeout = timeout;
      return structure;
    }

    public static bool Flash(Form form, uint count)
    {
      if (!FlashWindow.Win2000OrLater || form == null)
        return false;
      FlashWindow.FLASHWINFO flashwinfo = FlashWindow.Create_FLASHWINFO(form.Handle, 3U, count, 0U);
      return FlashWindow.FlashWindowEx(ref flashwinfo);
    }

    public static bool Start(Form form)
    {
      if (!FlashWindow.Win2000OrLater || form == null)
        return false;
      FlashWindow.FLASHWINFO flashwinfo = FlashWindow.Create_FLASHWINFO(form.Handle, 3U, uint.MaxValue, 0U);
      return FlashWindow.FlashWindowEx(ref flashwinfo);
    }

    public static bool Stop(Form form)
    {
      if (!FlashWindow.Win2000OrLater || form == null)
        return false;
      FlashWindow.FLASHWINFO flashwinfo = FlashWindow.Create_FLASHWINFO(form.Handle, 0U, uint.MaxValue, 0U);
      return FlashWindow.FlashWindowEx(ref flashwinfo);
    }

    private static bool Win2000OrLater => Environment.OSVersion.Version.Major >= 5;

    private struct FLASHWINFO
    {
      public uint cbSize;
      public IntPtr hwnd;
      public uint dwFlags;
      public uint uCount;
      public uint dwTimeout;
    }
  }
}
