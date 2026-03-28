// Decompiled with JetBrains decompiler
// Type: Kingdoms.Rot13
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

//#nullable disable
namespace Kingdoms
{
  internal static class Rot13
  {
    public static string Transform(string value)
    {
      char[] charArray = value.ToCharArray();
      for (int index = 0; index < charArray.Length; ++index)
      {
        int num = (int) charArray[index];
        if (num >= 97 && num <= 122)
        {
          if (num > 109)
            num -= 13;
          else
            num += 13;
        }
        else if (num >= 65 && num <= 90)
        {
          if (num > 77)
            num -= 13;
          else
            num += 13;
        }
        charArray[index] = (char) num;
      }
      return new string(charArray);
    }
  }
}
