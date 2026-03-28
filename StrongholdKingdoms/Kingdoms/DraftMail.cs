// Decompiled with JetBrains decompiler
// Type: Kingdoms.DraftMail
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.Collections.Generic;

//#nullable disable
namespace Kingdoms
{
  public class DraftMail
  {
    public long threadID = -1;
    public string Subject = "";
    public string Body = "";
    private List<MailLink> draftTargets = new List<MailLink>();
    public RecipientList Recipients = new RecipientList();
    public bool Sent;
    private static int count;

    public DraftMail()
    {
      ++DraftMail.count;
      UniversalDebugLog.Log("Created new draft mail " + (object) DraftMail.count);
    }

    public void ClearDraftMail()
    {
      UniversalDebugLog.Log("Cleared draft");
      this.Sent = false;
      this.Subject = "";
      this.Body = "";
      this.draftTargets.Clear();
      this.Recipients.ClearRecipients();
    }

    public List<MailLink> GetTargets() => this.draftTargets;

    public void ToggleTarget(MailLink target)
    {
      if (this.draftTargets.Contains(target))
        this.draftTargets.Remove(target);
      else
        this.draftTargets.Add(target);
    }

    public int TargetCount() => this.draftTargets.Count;

    public bool CanSendDraft()
    {
      return this.Recipients.RecipientCount() > 0 && this.Body != "" && this.Subject != "";
    }

    public bool InProgress()
    {
      return this.Recipients.RecipientCount() > 0 || this.Body != "" || this.Subject != "";
    }

    public void AttachVideo(string url)
    {
      this.DetatchVideos();
      this.draftTargets.Add(new MailLink()
      {
        linkType = 4,
        objectID = -1,
        objectName = url
      });
    }

    public void DetatchVideos()
    {
      foreach (MailLink draftTarget in this.draftTargets)
      {
        if (draftTarget.linkType == 4)
        {
          this.draftTargets.Remove(draftTarget);
          break;
        }
      }
    }
  }
}
