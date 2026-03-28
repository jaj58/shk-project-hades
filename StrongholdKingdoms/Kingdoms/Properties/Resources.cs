// Decompiled with JetBrains decompiler
// Type: Kingdoms.Properties.Resources
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
namespace Kingdoms.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) Kingdoms.Properties.Resources.resourceMan, (object) null))
          Kingdoms.Properties.Resources.resourceMan = new ResourceManager("Kingdoms.Properties.Resources", typeof (Kingdoms.Properties.Resources).Assembly);
        return Kingdoms.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => Kingdoms.Properties.Resources.resourceCulture;
      set => Kingdoms.Properties.Resources.resourceCulture = value;
    }

    internal static Bitmap connectinglogo
    {
      get
      {
        return (Bitmap) Kingdoms.Properties.Resources.ResourceManager.GetObject(nameof (connectinglogo), Kingdoms.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap right_side_panel_large
    {
      get
      {
        return (Bitmap) Kingdoms.Properties.Resources.ResourceManager.GetObject(nameof (right_side_panel_large), Kingdoms.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap right_side_panel_large_stone_tan
    {
      get
      {
        return (Bitmap) Kingdoms.Properties.Resources.ResourceManager.GetObject(nameof (right_side_panel_large_stone_tan), Kingdoms.Properties.Resources.resourceCulture);
      }
    }

    internal static Icon shk_icon
    {
      get
      {
        return (Icon) Kingdoms.Properties.Resources.ResourceManager.GetObject(nameof (shk_icon), Kingdoms.Properties.Resources.resourceCulture);
      }
    }

    internal static Icon shk_icon1
    {
      get
      {
        return (Icon) Kingdoms.Properties.Resources.ResourceManager.GetObject(nameof (shk_icon1), Kingdoms.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap splash_screen
    {
      get
      {
        return (Bitmap) Kingdoms.Properties.Resources.ResourceManager.GetObject(nameof (splash_screen), Kingdoms.Properties.Resources.resourceCulture);
      }
    }
  }
}
