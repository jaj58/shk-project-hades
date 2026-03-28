// Decompiled with JetBrains decompiler
// Type: Kingdoms.Censor
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

//#nullable disable
namespace Kingdoms
{
  public class Censor
  {
    public IList<string> CensoredWords;

    public Censor(IEnumerable<string> censoredWords)
    {
      this.CensoredWords = censoredWords != null ? (IList<string>) new List<string>(censoredWords) : throw new ArgumentNullException(nameof (censoredWords));
    }

    public string CensorText(string text)
    {
      string input = text != null ? text : throw new ArgumentNullException(nameof (text));
      foreach (string censoredWord in (IEnumerable<string>) this.CensoredWords)
      {
        string regexPattern = this.ToRegexPattern(censoredWord);
        input = Regex.Replace(input, regexPattern, new MatchEvaluator(Censor.StarCensoredMatch), RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
      }
      return input;
    }

    private static string StarCensoredMatch(Match m) => new string('*', m.Captures[0].Value.Length);

    private string ToRegexPattern(string wildcardSearch)
    {
      string str = Regex.Escape(wildcardSearch).Replace("\\*", ".*?").Replace("\\?", ".");
      return str.StartsWith(".*?") ? "(^\\b)*?" + ("(^\\b)*?" + str.Substring(3)) + "\\b" : "\\b" + str + "\\b";
    }
  }
}
