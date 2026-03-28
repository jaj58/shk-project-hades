// Decompiled with JetBrains decompiler
// Type: Kingdoms.FontManager
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;

//#nullable disable
namespace Kingdoms
{
  public class FontManager
  {
    public const string DEFAULT_FONT = "Arial";
    public const string DEFAULT_FONT2 = "Microsoft Sans Serif";
    public static float dpi = 96f;
    private static bool dpiSet = false;
    private static Dictionary<string, int> privateFontNames = new Dictionary<string, int>();
    private static PrivateFontCollection pfc = new PrivateFontCollection();
    public static Dictionary<string, Font> fontCollection = new Dictionary<string, Font>();

    public static void setDPI(Graphics gfx)
    {
      FontManager.dpi = gfx.DpiX;
      FontManager.dpiSet = true;
    }

    public static Font GetPrivateFont(string fileName, float pointSize, FontStyle style)
    {
      string hashString = FontManager.createHashString(fileName, pointSize, style);
      try
      {
        Font font = FontManager.fontCollection[hashString];
        if (font != null)
          return font;
      }
      catch (Exception ex)
      {
      }
      try
      {
        FontFamily family;
        try
        {
          int privateFontName = FontManager.privateFontNames[fileName];
          family = FontManager.pfc.Families[privateFontName];
        }
        catch (Exception ex)
        {
          FontManager.pfc.AddFontFile(fileName);
          int index = FontManager.pfc.Families.Length - 1;
          family = FontManager.pfc.Families[index];
          FontManager.privateFontNames.Add(fileName, index);
        }
        Font privateFont = new Font(family, pointSize * 96f / FontManager.dpi, style);
        if (privateFont != null)
        {
          if (FontManager.dpiSet)
            FontManager.fontCollection.Add(hashString, privateFont);
          return privateFont;
        }
      }
      catch (Exception ex)
      {
        string message = ex.Message;
      }
      return (Font) null;
    }

    private static string createHashString(string fontFamilyName, float pointSize, FontStyle style)
    {
      return fontFamilyName + pointSize.ToString() + style.ToString();
    }

    public static Font GetFont(string fontFamilyName, float pointSize, FontStyle style)
    {
      if ((Program.mySettings.LanguageIdent == "zh" || Program.mySettings.LanguageIdent == "zhHK") && style == FontStyle.Bold && (double) pointSize < 12.0)
        style = FontStyle.Regular;
      string hashString = FontManager.createHashString(fontFamilyName, pointSize, style);
      try
      {
        if (FontManager.fontCollection.ContainsKey(hashString))
        {
          Font font = FontManager.fontCollection[hashString];
          if (font != null)
            return font;
        }
      }
      catch (Exception ex)
      {
      }
      Font font1 = FontManager.getFont(fontFamilyName, pointSize, style) ?? FontManager.GetFont("Microsoft Sans Serif", pointSize, style);
      if (font1 != null && FontManager.dpiSet)
        FontManager.fontCollection.Add(hashString, font1);
      return font1;
    }

    public static Font GetFont(string fontFamilyName, float pointSize)
    {
      return FontManager.GetFont(fontFamilyName, pointSize, FontStyle.Regular);
    }

    private static Font getFont(string fontFamilyName, float pointSize, FontStyle style)
    {
      try
      {
        Font font = new Font(fontFamilyName, pointSize * 96f / FontManager.dpi, style);
        if (font != null)
          return font;
      }
      catch (Exception ex)
      {
      }
      return (Font) null;
    }
  }
}
