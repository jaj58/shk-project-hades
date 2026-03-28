// Decompiled with JetBrains decompiler
// Type: Kingdoms.AssetIcons.RankAnims.RankAnims
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

//#nullable disable
namespace Kingdoms.AssetIcons.RankAnims
{
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
  [DebuggerNonUserCode]
  internal class RankAnims
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal RankAnims()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) Kingdoms.AssetIcons.RankAnims.RankAnims.resourceMan, (object) null))
          Kingdoms.AssetIcons.RankAnims.RankAnims.resourceMan = new ResourceManager("Kingdoms.AssetIcons.RankAnims.RankAnims", typeof (Kingdoms.AssetIcons.RankAnims.RankAnims).Assembly);
        return Kingdoms.AssetIcons.RankAnims.RankAnims.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => Kingdoms.AssetIcons.RankAnims.RankAnims.resourceCulture;
      set => Kingdoms.AssetIcons.RankAnims.RankAnims.resourceCulture = value;
    }

    internal static Bitmap crown_prince
    {
      get
      {
        return (Bitmap) Kingdoms.AssetIcons.RankAnims.RankAnims.ResourceManager.GetObject(nameof (crown_prince), Kingdoms.AssetIcons.RankAnims.RankAnims.resourceCulture);
      }
    }

    internal static Bitmap lords
    {
      get
      {
        return (Bitmap) Kingdoms.AssetIcons.RankAnims.RankAnims.ResourceManager.GetObject(nameof (lords), Kingdoms.AssetIcons.RankAnims.RankAnims.resourceCulture);
      }
    }

    internal static byte[] lords_uv
    {
      get
      {
        return (byte[]) Kingdoms.AssetIcons.RankAnims.RankAnims.ResourceManager.GetObject(nameof (lords_uv), Kingdoms.AssetIcons.RankAnims.RankAnims.resourceCulture);
      }
    }
  }
}
