// Decompiled with JetBrains decompiler
// Type: Kingdoms.RecipientList
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Collections.Generic;

//#nullable disable
namespace Kingdoms
{
  public class RecipientList
  {
    public const int RECIPIENTS_LIMIT = 60;
    private List<string> draftRecipients = new List<string>();

    public string[] GetRecipients() => this.draftRecipients.ToArray();

    public void AddRecipient(string user)
    {
      if (!this.draftRecipients.Contains(user) && this.draftRecipients.Count < 60)
      {
        this.draftRecipients.Add(user);
        UniversalDebugLog.Log("Recipient added " + user);
      }
      else
        UniversalDebugLog.Log(user + " already added");
    }

    public void ToggleRecipient(string user)
    {
      if (this.draftRecipients.Contains(user))
        this.draftRecipients.Remove(user);
      else
        this.draftRecipients.Add(user);
    }

    public int RecipientCount() => this.draftRecipients.Count;

    public void ClearRecipients()
    {
      UniversalDebugLog.Log("recipients cleared");
      this.draftRecipients.Clear();
    }
  }
}
