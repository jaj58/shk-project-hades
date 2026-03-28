// Decompiled with JetBrains decompiler
// Type: Kingdoms.IForumPostParent
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

//#nullable disable
namespace Kingdoms
{
  public interface IForumPostParent
  {
    void newTopic(long forumID, string heading, string body);
  }
}
