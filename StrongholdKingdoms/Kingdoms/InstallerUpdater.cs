// Decompiled with JetBrains decompiler
// Type: Kingdoms.InstallerUpdater
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Cache;

//#nullable disable
namespace Kingdoms
{
  public class InstallerUpdater
  {
    public static string downloadSelfUpdater(Uri uri)
    {
      WebClient webClient = new WebClient();
      webClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
      string fileName = Path.GetTempPath() + Path.GetFileName(uri.AbsolutePath);
      try
      {
        webClient.DownloadFile(uri, fileName);
        return fileName;
      }
      catch (Exception ex)
      {
      }
      return "";
    }

    public static bool runInstaller(string path)
    {
      string fileName = Path.GetFileName(path);
      path = Path.GetDirectoryName(path);
      Process process = new Process()
      {
        StartInfo = new ProcessStartInfo(fileName)
      };
      process.StartInfo.WorkingDirectory = path;
      process.Start();
      return true;
    }
  }
}
