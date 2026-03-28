// Decompiled with JetBrains decompiler
// Type: Kingdoms.MailManager
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

//#nullable disable
namespace Kingdoms
{
  public class MailManager
  {
    public const int GROUP_YESTERDAY = 1;
    public const int GROUP_3DAYS = 2;
    public const int GROUP_THISWEEK = 3;
    public const int GROUP_THISMONTH = 4;
    public const int GROUP_ALLTIME = 5;
    public const int GROUP_SINCE_LAST = 6;
    private static MailManager m_instance;
    private SparseArray m_storedThreadHeaders = new SparseArray();
    private SparseArray m_storedThreads = new SparseArray();
    public bool downloadedYesterday = true;
    public bool downloaded3Days = true;
    public bool downloadedThisWeek = true;
    public bool downloadedThisMonth = true;
    public bool downloadedAll = true;
    public bool openYesterday = true;
    public bool open3Days = true;
    public bool openThisWeek = true;
    public bool openThisMonth;
    public bool openAll;
    private List<MailFolderItem> m_mailFolders = new List<MailFolderItem>();
    public DateTime lastTimeThreadsReceived = DateTime.MinValue;
    public bool initialRequest = true;
    public bool gotFolders;
    private List<MailThreadListItem> m_preSortedHeaders = new List<MailThreadListItem>();
    private List<MailThreadListItem> m_preSortedALLHeader = new List<MailThreadListItem>();
    private MailManager.MailThreadHeaderComparer mailThreadHeaderComparer = new MailManager.MailThreadHeaderComparer();
    public MailManager.MailThreadComparer mailThreadComparer = new MailManager.MailThreadComparer();
    private bool aggressiveBlocking;
    public bool blockListChanged;
    public List<string> blockedList = new List<string>();
    private static DraftMail currentDraft = new DraftMail();
    public string[] mailUsersHistory;
    public string[] mailFavourites;
    private MailManager.GenericUIDelegate getFolderCallback;
    private MailManager.GetMailThreadUIDelegate getMailThreadCallback;
    private MailManager.GenericUIDelegate getMailThreadListDelegate;
    private double lastUpdateTime;
    private RemoteServices.SendMail_UserCallBack OnMailSent;
    private RemoteServices.SendSpecialMail_UserCallBack OnSpecialMailSent;
    private MailManager.GenericUIDelegate removeFolderUICallback;
    private MailManager.GenericUIDelegate createFolderUICallback;
    public long selectedFolderID = -1;
    public static bool HasUnreadMail = false;

    public static MailManager Instance
    {
      get
      {
        if (MailManager.m_instance == null)
          MailManager.m_instance = new MailManager();
        return MailManager.m_instance;
      }
    }

    public MailManager() => UniversalDebugLog.Log("Created new mail manager");

    public DraftMail CurrentDraft => MailManager.currentDraft;

    public void getRecipientHistory()
    {
      RemoteServices.Instance.set_GetMailRecipientsHistory_UserCallBack(new RemoteServices.GetMailRecipientsHistory_UserCallBack(this.mailRecipientsCallback));
      RemoteServices.Instance.GetMailRecipientsHistory();
    }

    private void mailRecipientsCallback(GetMailRecipientsHistory_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.mailFavourites = returnData.mailFavourites;
      this.mailUsersHistory = returnData.mailUsersHistory;
    }

    public void getFolders(MailManager.GenericUIDelegate callback)
    {
      this.getFolderCallback = callback;
      RemoteServices.Instance.set_GetMailFolders_UserCallBack(new RemoteServices.GetMailFolders_UserCallBack(this.mailFoldersCallback));
      RemoteServices.Instance.GetMailFolders();
    }

    public void mailFoldersCallback(GetMailFolders_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.mailFolders.Clear();
      this.mailFolders.AddRange((IEnumerable<MailFolderItem>) returnData.folders);
      this.getFolderCallback();
    }

    public void getMailThread(long threadID, MailManager.GetMailThreadUIDelegate callback)
    {
      long highestSegmentID = -1;
      int localCount = 0;
      if (this.storedThreads[threadID] != null)
      {
        MailThreadItem[] storedThread = (MailThreadItem[]) this.storedThreads[threadID];
        localCount = storedThread.Length;
        foreach (MailThreadItem mailThreadItem in storedThread)
        {
          if (mailThreadItem.mailID > highestSegmentID)
            highestSegmentID = mailThreadItem.mailID;
        }
      }
      this.getMailThreadCallback = callback;
      RemoteServices.Instance.set_GetMailThread_UserCallBack(new RemoteServices.GetMailThread_UserCallBack(this.mailThreadCallback));
      RemoteServices.Instance.GetMailThread(threadID, localCount, highestSegmentID);
    }

    private void mailThreadCallback(GetMailThread_ReturnType returnData)
    {
      if (returnData.Success && returnData.items != null)
      {
        if (returnData.items.Count > 0 && RemoteServices.Instance.UserOptions.profanityFilter)
        {
          foreach (MailThreadItem mailThreadItem in returnData.items)
            mailThreadItem.body = GameEngine.Instance.censorString(mailThreadItem.body);
        }
        if (this.storedThreads[returnData.threadID] == null)
        {
          List<MailThreadItem> mailThreadItemList = new List<MailThreadItem>();
          mailThreadItemList.AddRange((IEnumerable<MailThreadItem>) returnData.items);
          mailThreadItemList.Sort((IComparer<MailThreadItem>) this.mailThreadComparer);
          this.storedThreads[returnData.threadID] = (object) mailThreadItemList.ToArray();
        }
        else
        {
          List<MailThreadItem> mailThreadItemList = new List<MailThreadItem>();
          MailThreadItem[] storedThread = (MailThreadItem[]) this.storedThreads[returnData.threadID];
          mailThreadItemList.AddRange((IEnumerable<MailThreadItem>) storedThread);
          mailThreadItemList.AddRange((IEnumerable<MailThreadItem>) returnData.items);
          mailThreadItemList.Sort((IComparer<MailThreadItem>) this.mailThreadComparer);
          this.storedThreads[returnData.threadID] = (object) mailThreadItemList.ToArray();
        }
        this.saveMail();
      }
      this.getMailThreadCallback(returnData.threadID);
    }

    public string getGroupString(long group)
    {
      switch (group)
      {
        case 1:
          return SK.Text("MailScreen_Date_Yesterday", "Date: Yesterday");
        case 2:
          return SK.Text("MailScreen_Date_Last_3_Days", "Date: Last 3 Days");
        case 3:
          return SK.Text("MailScreen_Date_Last_7_Days", "Date: Last 7 Days");
        case 4:
          return SK.Text("MailScreen_Date_Last_30_Days", "Date: Last 30 Days");
        case 5:
          return SK.Text("MailScreen_Date_All", "Date: All");
        default:
          return "";
      }
    }

    private void addBreakBars()
    {
      DateTime currentServerTime = VillageMap.getCurrentServerTime();
      this.storedThreadHeaders[-1] = (object) new MailThreadListItem()
      {
        mailThreadID = -1L,
        mailTime = new DateTime(currentServerTime.Year, currentServerTime.Month, currentServerTime.Day, 0, 0, 1)
      };
      this.storedThreadHeaders[-2] = (object) new MailThreadListItem()
      {
        mailThreadID = -2L,
        mailTime = new DateTime(currentServerTime.Year, currentServerTime.Month, currentServerTime.Day, 0, 0, 1).AddDays(-1.0)
      };
      this.storedThreadHeaders[-3] = (object) new MailThreadListItem()
      {
        mailThreadID = -3L,
        mailTime = new DateTime(currentServerTime.Year, currentServerTime.Month, currentServerTime.Day, 0, 0, 1).AddDays(-3.0)
      };
      this.storedThreadHeaders[-4] = (object) new MailThreadListItem()
      {
        mailThreadID = -4L,
        mailTime = new DateTime(currentServerTime.Year, currentServerTime.Month, currentServerTime.Day, 0, 0, 1).AddDays(-7.0)
      };
    }

    public void mailThreadListCallback(GetMailThreadList_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.lastTimeThreadsReceived = returnData.currentSystemTime;
      if (returnData.items != null)
      {
        if (returnData.items.Count > 0 && RemoteServices.Instance.UserOptions.profanityFilter)
        {
          foreach (MailThreadListItem mailThreadListItem in returnData.items)
            mailThreadListItem.subject = GameEngine.Instance.censorString(mailThreadListItem.subject);
        }
        int count = returnData.items.Count;
        for (int index1 = 0; index1 < count; ++index1)
        {
          if (returnData.items[index1].otherUser.Length > 100)
          {
            string[] strArray = new string[100];
            for (int index2 = 0; index2 < 100; ++index2)
              strArray[index2] = returnData.items[index1].otherUser[index2];
            returnData.items[index1].otherUser = strArray;
          }
          this.storedThreadHeaders[returnData.items[index1].mailThreadID] = (object) returnData.items[index1];
        }
      }
      this.addBreakBars();
      this.preSortThreadHeaders();
      if (this.getMailThreadListDelegate != null)
        this.getMailThreadListDelegate();
      if (returnData.items == null)
        return;
      this.saveMail();
    }

    public void GetMailThreadList(
      bool initialRequest,
      int mode,
      MailManager.GenericUIDelegate uiDelegate)
    {
      this.getMailThreadListDelegate = uiDelegate;
      RemoteServices.Instance.set_GetMailThreadList_UserCallBack(new RemoteServices.GetMailThreadList_UserCallBack(this.mailThreadListCallback));
      RemoteServices.Instance.GetMailThreadList(initialRequest, mode, this.lastTimeThreadsReceived);
      if (mode >= 1)
        this.downloadedYesterday = true;
      if (mode >= 2)
        this.downloaded3Days = true;
      if (mode >= 3)
        this.downloadedThisWeek = true;
      if (mode >= 4)
        this.downloadedThisMonth = true;
      if (mode < 5)
        return;
      this.downloadedAll = true;
    }

    public void SetMailRead(long mailID)
    {
      RemoteServices.Instance.set_FlagMailRead_UserCallBack(new RemoteServices.FlagMailRead_UserCallBack(this.flagMailCallback));
      RemoteServices.Instance.FlagMailRead(mailID);
      this.saveMail();
    }

    public void SetThreadReadStatus(long threadID, bool read)
    {
      RemoteServices.Instance.set_FlagMailRead_UserCallBack(new RemoteServices.FlagMailRead_UserCallBack(this.flagMailCallback));
      if (read)
        RemoteServices.Instance.FlagThreadRead(threadID);
      else
        RemoteServices.Instance.FlagThreadUnread(threadID);
    }

    private void flagMailCallback(FlagMailRead_ReturnType returnData)
    {
      if (returnData.Success)
        return;
      UniversalDebugLog.Log("Failed to mark mail read status error_code: " + (object) returnData.m_errorCode + ", error_id: " + (object) returnData.m_errorID);
    }

    public void updateSearch(
      string searchText,
      RemoteServices.GetMailUserSearch_UserCallBack getMailUserSearchCallback)
    {
      if (this.lastUpdateTime == 0.0)
        return;
      double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
      if (currentMilliseconds - this.lastUpdateTime <= 1000.0)
        return;
      if (searchText.Length == 0)
        this.lastUpdateTime = 0.0;
      else if (searchText.Length <= 4 && "lord".Contains(searchText.ToLower()))
      {
        this.lastUpdateTime = 0.0;
      }
      else
      {
        if ((searchText.Length != 1 && searchText.Length != 2 || currentMilliseconds - this.lastUpdateTime <= 2000.0) && searchText.Length <= 2)
          return;
        this.lastUpdateTime = 0.0;
        RemoteServices.Instance.set_GetMailUserSearch_UserCallBack(getMailUserSearchCallback);
        RemoteServices.Instance.GetMailUserSearch(searchText);
      }
    }

    public void resetUpdateTimer() => this.lastUpdateTime = DXTimer.GetCurrentMilliseconds();

    public void SendDraft(
      bool sendAsForward,
      RemoteServices.SendMail_UserCallBack onMailSentCallback)
    {
      UniversalDebugLog.Log("mail subject: " + this.CurrentDraft.Subject + " body: " + this.CurrentDraft.Body);
      UniversalDebugLog.Log("Thread: " + (object) this.CurrentDraft.threadID + " forward: " + (object) sendAsForward);
      this.OnMailSent = onMailSentCallback;
      List<MailLink> targets = this.CurrentDraft.GetTargets();
      string subject = this.CurrentDraft.Subject;
      string body = this.CurrentDraft.Body + this.generateAttachmentsString(targets);
      string[] recipients = this.CurrentDraft.Recipients.GetRecipients();
      foreach (string name in recipients)
        this.AddNameToRecent(name);
      RemoteServices.Instance.set_SendMail_UserCallBack(new RemoteServices.SendMail_UserCallBack(this.internalMailSentCallback));
      RemoteServices.Instance.SendMail(subject, body, recipients, this.CurrentDraft.threadID, sendAsForward);
      this.CurrentDraft.Sent = true;
    }

    public void SendMail(
      string subject,
      string body,
      string[] recipients,
      long threadID,
      bool sendAsForward,
      RemoteServices.SendMail_UserCallBack onMailSentCallback)
    {
      UniversalDebugLog.Log("mail subject: " + subject + " body: " + body);
      UniversalDebugLog.Log("Thread: " + (object) threadID + " forward: " + (object) sendAsForward);
      this.OnMailSent = onMailSentCallback;
      RemoteServices.Instance.set_SendMail_UserCallBack(new RemoteServices.SendMail_UserCallBack(this.internalMailSentCallback));
      RemoteServices.Instance.SendMail(subject, body, recipients, threadID, sendAsForward);
    }

    public void SendProclamation(
      string subject,
      string body,
      int mailType,
      int area,
      RemoteServices.SendSpecialMail_UserCallBack onMailSentCallback)
    {
      this.OnSpecialMailSent = onMailSentCallback;
      RemoteServices.Instance.set_SendSpecialMail_UserCallBack(new RemoteServices.SendSpecialMail_UserCallBack(this.internalSpecialMailSentCallback));
      RemoteServices.Instance.SendSpecialMail(mailType, area, subject, body);
    }

    private void internalMailSentCallback(SendMail_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.CurrentDraft.ClearDraftMail();
      this.OnMailSent(returnData);
    }

    private void internalSpecialMailSentCallback(SendSpecialMail_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.CurrentDraft.ClearDraftMail();
      this.OnSpecialMailSent(returnData);
    }

    public void RemoveFolder(long folderID, MailManager.GenericUIDelegate uiCallback)
    {
      this.removeFolderUICallback = uiCallback;
      RemoteServices.Instance.set_RemoveMailFolder_UserCallBack(new RemoteServices.RemoveMailFolder_UserCallBack(this.removeMailFolderCallback));
      RemoteServices.Instance.RemoveMailFolder(folderID);
    }

    private void removeMailFolderCallback(RemoveMailFolder_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.mailFolders.Clear();
      this.mailFolders.AddRange((IEnumerable<MailFolderItem>) returnData.folders);
      foreach (MailThreadListItem storedThreadHeader in this.storedThreadHeaders)
      {
        if (storedThreadHeader.folderID == returnData.deletedFolderID)
          storedThreadHeader.folderID = -1L;
      }
      this.selectedFolderID = -1L;
      this.preSortThreadHeaders();
      if (this.removeFolderUICallback == null)
        return;
      this.removeFolderUICallback();
    }

    public void CreateFolder(string folderName, MailManager.GenericUIDelegate uiCallback)
    {
      this.createFolderUICallback = uiCallback;
      if (this.DoesFolderAlreadyExist(folderName))
        return;
      RemoteServices.Instance.set_CreateMailFolder_UserCallBack(new RemoteServices.CreateMailFolder_UserCallBack(this.createMailFolderCallback));
      RemoteServices.Instance.CreateMailFolder(folderName);
    }

    private void createMailFolderCallback(CreateMailFolder_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.mailFolders.Clear();
      this.mailFolders.AddRange((IEnumerable<MailFolderItem>) returnData.folders);
      this.createFolderUICallback();
    }

    public void DeleteThreads(List<long> selectedMailThreadIDList)
    {
      foreach (long selectedMailThreadId in selectedMailThreadIDList)
      {
        RemoteServices.Instance.DeleteMailThread(selectedMailThreadId);
        Thread.Sleep(200);
        this.storedThreads[selectedMailThreadId] = (object) null;
        this.storedThreadHeaders[selectedMailThreadId] = (object) null;
      }
      this.saveMail();
      this.preSortThreadHeaders();
    }

    public void MoveThreadsToFolder(List<long> threadIDs, long targetFolderID)
    {
      foreach (long threadId in threadIDs)
      {
        RemoteServices.Instance.MoveToMailFolder(threadId, targetFolderID);
        Thread.Sleep(200);
        ((MailThreadListItem) this.storedThreadHeaders[threadId]).folderID = targetFolderID;
      }
      this.saveMail();
    }

    public void AddNameToFavourites(string name)
    {
      List<string> stringList = new List<string>();
      if (this.mailFavourites != null)
      {
        foreach (string mailFavourite in this.mailFavourites)
        {
          if (mailFavourite == name)
            return;
          stringList.Add(mailFavourite);
        }
      }
      stringList.Add(name);
      this.mailFavourites = stringList.ToArray();
      RemoteServices.Instance.AddUserToFavourites(name);
    }

    public void RemoveNameFromFavourites(string name)
    {
      if (this.mailFavourites == null)
        return;
      List<string> stringList = new List<string>((IEnumerable<string>) this.mailFavourites);
      if (stringList.Contains(name))
      {
        stringList.Remove(name);
        RemoteServices.Instance.RemoveUserFromFavourites(name);
      }
      this.mailFavourites = stringList.ToArray();
    }

    public void AddNameToRecent(string name)
    {
      List<string> stringList = new List<string>();
      if (this.mailUsersHistory != null)
      {
        foreach (string str in this.mailUsersHistory)
        {
          if (str == name)
            return;
          stringList.Add(str);
        }
      }
      stringList.Add(name);
      this.mailUsersHistory = stringList.ToArray();
    }

    public SparseArray storedThreadHeaders
    {
      set => this.m_storedThreadHeaders = value;
      get => this.m_storedThreadHeaders;
    }

    public SparseArray storedThreads
    {
      set => this.m_storedThreads = value;
      get => this.m_storedThreads;
    }

    public List<MailFolderItem> mailFolders
    {
      set => this.m_mailFolders = value;
      get => this.m_mailFolders;
    }

    public List<MailThreadListItem> preSortedHeaders
    {
      set => this.m_preSortedHeaders = value;
      get => this.m_preSortedHeaders;
    }

    public List<MailThreadListItem> preSortedAllHeader
    {
      set => this.m_preSortedALLHeader = value;
      get => this.m_preSortedALLHeader;
    }

    public bool AggressiveBlocking
    {
      get => this.aggressiveBlocking;
      set
      {
        this.aggressiveBlocking = value;
        this.blockListChanged = true;
      }
    }

    public void clearStoredMail()
    {
      this.m_storedThreadHeaders = new SparseArray();
      this.m_storedThreads = new SparseArray();
      this.initialRequest = true;
      this.gotFolders = false;
      this.downloadedYesterday = false;
      this.downloaded3Days = false;
      this.downloadedThisWeek = false;
      this.downloadedThisMonth = false;
      this.downloadedAll = false;
      this.lastTimeThreadsReceived = DateTime.Now.AddYears(-50);
    }

    public void logout()
    {
      this.gotFolders = false;
      this.initialRequest = true;
    }

    public void preSortThreadHeaders()
    {
      this.m_preSortedHeaders.Clear();
      foreach (MailThreadListItem storedThreadHeader in this.m_storedThreadHeaders)
      {
        if (storedThreadHeader.folderID == this.selectedFolderID || storedThreadHeader.mailThreadID < 0L)
          this.m_preSortedHeaders.Add(storedThreadHeader);
      }
      this.m_preSortedHeaders.Sort((IComparer<MailThreadListItem>) this.mailThreadHeaderComparer);
      this.m_preSortedALLHeader.Clear();
      foreach (MailThreadListItem preSortedHeader in this.m_preSortedHeaders)
        this.m_preSortedALLHeader.Add(preSortedHeader);
      DateTime currentServerTime = VillageMap.getCurrentServerTime();
      DateTime dateTime1 = new DateTime(currentServerTime.Year, currentServerTime.Month, currentServerTime.Day, 0, 0, 1);
      DateTime dateTime2 = new DateTime(currentServerTime.Year, currentServerTime.Month, currentServerTime.Day, 0, 0, 1).AddDays(-1.0);
      DateTime dateTime3 = new DateTime(currentServerTime.Year, currentServerTime.Month, currentServerTime.Day, 0, 0, 1).AddDays(-3.0);
      DateTime dateTime4 = new DateTime(currentServerTime.Year, currentServerTime.Month, currentServerTime.Day, 0, 0, 1).AddDays(-7.0);
      DateTime dateTime5 = new DateTime(currentServerTime.Year, currentServerTime.Month, currentServerTime.Day, 0, 0, 1).AddYears(-50);
      List<MailThreadListItem> mailThreadListItemList = new List<MailThreadListItem>();
      if (!this.openYesterday)
      {
        foreach (MailThreadListItem preSortedHeader in this.m_preSortedHeaders)
        {
          if (preSortedHeader.mailThreadID >= 0L && preSortedHeader.mailTime < dateTime1 && preSortedHeader.mailTime > dateTime2)
            mailThreadListItemList.Add(preSortedHeader);
        }
      }
      if (!this.open3Days)
      {
        foreach (MailThreadListItem preSortedHeader in this.m_preSortedHeaders)
        {
          if (preSortedHeader.mailThreadID >= 0L && preSortedHeader.mailTime < dateTime2 && preSortedHeader.mailTime > dateTime3)
            mailThreadListItemList.Add(preSortedHeader);
        }
      }
      if (!this.openThisWeek)
      {
        foreach (MailThreadListItem preSortedHeader in this.m_preSortedHeaders)
        {
          if (preSortedHeader.mailThreadID >= 0L && preSortedHeader.mailTime < dateTime3 && preSortedHeader.mailTime > dateTime4)
            mailThreadListItemList.Add(preSortedHeader);
        }
      }
      if (!this.openThisMonth)
      {
        foreach (MailThreadListItem preSortedHeader in this.m_preSortedHeaders)
        {
          if (preSortedHeader.mailThreadID >= 0L && preSortedHeader.mailTime < dateTime4 && preSortedHeader.mailTime > dateTime5)
            mailThreadListItemList.Add(preSortedHeader);
        }
      }
      foreach (MailThreadListItem mailThreadListItem in mailThreadListItemList)
        this.m_preSortedHeaders.Remove(mailThreadListItem);
    }

    public static string getDateRangeDescription(int groupID)
    {
      switch (groupID)
      {
        case 1:
          return "Yesterday";
        case 2:
          return "3 days";
        case 3:
          return "7 days";
        case 4:
          return "30 days";
        case 5:
          return "All";
        default:
          return "None";
      }
    }

    public void loadMail()
    {
      int userId = RemoteServices.Instance.UserID;
      string settingsPath = GameEngine.getSettingsPath(false);
      FileStream input = (FileStream) null;
      BinaryReader binaryReader = (BinaryReader) null;
      try
      {
        input = new FileStream(settingsPath + "\\MailData-" + userId.ToString() + "-" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + ".dat", FileMode.Open, FileAccess.Read);
        binaryReader = new BinaryReader((Stream) input);
        SparseArray sparseArray1 = new SparseArray();
        SparseArray sparseArray2 = new SparseArray();
        long ticks1 = binaryReader.ReadInt64();
        int num1 = binaryReader.ReadInt32();
        for (int index1 = 0; index1 < num1; ++index1)
        {
          long num2 = binaryReader.ReadInt64();
          if (num2 >= 0L)
          {
            MailThreadListItem mailThreadListItem = new MailThreadListItem();
            mailThreadListItem.mailThreadID = num2;
            mailThreadListItem.folderID = binaryReader.ReadInt64();
            long ticks2 = binaryReader.ReadInt64();
            mailThreadListItem.mailTime = new DateTime(ticks2);
            mailThreadListItem.mailTimeAsDouble = binaryReader.ReadDouble();
            int num3 = binaryReader.ReadInt32();
            if (num3 > 0)
            {
              List<string> stringList = new List<string>();
              for (int index2 = 0; index2 < num3; ++index2)
              {
                string str = binaryReader.ReadString();
                if (stringList.Count < 100)
                  stringList.Add(str);
              }
              mailThreadListItem.otherUser = stringList.ToArray();
            }
            else
              mailThreadListItem.otherUser = new string[0];
            mailThreadListItem.readStatus = binaryReader.ReadBoolean();
            mailThreadListItem.subject = binaryReader.ReadString();
            int num4 = binaryReader.ReadInt32();
            if (num4 == -5)
            {
              mailThreadListItem.readOnly = binaryReader.ReadBoolean();
              mailThreadListItem.specialType = binaryReader.ReadInt32();
              mailThreadListItem.specialArea = binaryReader.ReadInt32();
              num4 = binaryReader.ReadInt32();
            }
            List<MailThreadItem> mailThreadItemList = new List<MailThreadItem>();
            bool flag = false;
            DateTime dateTime = DateTime.MaxValue;
            for (int index3 = 0; index3 < num4; ++index3)
            {
              MailThreadItem mailThreadItem = new MailThreadItem();
              mailThreadItem.body = binaryReader.ReadString();
              mailThreadItem.mailID = binaryReader.ReadInt64();
              long ticks3 = binaryReader.ReadInt64();
              mailThreadItem.mailTime = new DateTime(ticks3);
              mailThreadItem.mailTimeAsDouble = binaryReader.ReadDouble();
              mailThreadItem.otherUser = binaryReader.ReadString();
              mailThreadItem.otherUserID = binaryReader.ReadInt32();
              mailThreadItem.readStatus = binaryReader.ReadBoolean();
              mailThreadItemList.Add(mailThreadItem);
              if (mailThreadItem.mailTime > dateTime)
                flag = true;
              else
                dateTime = mailThreadItem.mailTime;
            }
            if (num4 > 0)
            {
              if (flag)
                mailThreadItemList.Sort((IComparer<MailThreadItem>) this.mailThreadComparer);
              sparseArray1[mailThreadListItem.mailThreadID] = (object) mailThreadItemList.ToArray();
            }
            sparseArray2[mailThreadListItem.mailThreadID] = (object) mailThreadListItem;
          }
        }
        binaryReader.Close();
        input.Close();
        this.m_storedThreadHeaders = sparseArray2;
        this.m_storedThreads = sparseArray1;
        this.lastTimeThreadsReceived = new DateTime(ticks1);
      }
      catch (Exception ex1)
      {
        try
        {
          binaryReader?.Close();
        }
        catch (Exception ex2)
        {
        }
        try
        {
          input?.Close();
        }
        catch (Exception ex3)
        {
        }
      }
    }

    public void saveMail()
    {
      int userId = RemoteServices.Instance.UserID;
      string settingsPath = GameEngine.getSettingsPath(true);
      try
      {
        new FileInfo(settingsPath + "\\MailData-" + userId.ToString() + "-" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + ".dat").IsReadOnly = false;
      }
      catch (Exception ex)
      {
      }
      FileStream output = (FileStream) null;
      BinaryWriter binaryWriter = (BinaryWriter) null;
      try
      {
        output = new FileStream(settingsPath + "\\MailData-" + userId.ToString() + "-" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + ".dat", FileMode.Create);
        binaryWriter = new BinaryWriter((Stream) output);
        binaryWriter.Write(this.lastTimeThreadsReceived.Ticks);
        int count = this.m_storedThreadHeaders.Count;
        binaryWriter.Write(count);
        foreach (MailThreadListItem storedThreadHeader in this.m_storedThreadHeaders)
        {
          binaryWriter.Write(storedThreadHeader.mailThreadID);
          if (storedThreadHeader.mailThreadID >= 0L)
          {
            binaryWriter.Write(storedThreadHeader.folderID);
            binaryWriter.Write(storedThreadHeader.mailTime.Ticks);
            binaryWriter.Write(storedThreadHeader.mailTimeAsDouble);
            int num1 = 0;
            if (storedThreadHeader.otherUser != null)
              num1 = storedThreadHeader.otherUser.Length;
            binaryWriter.Write(num1);
            if (storedThreadHeader.otherUser != null)
            {
              foreach (string str in storedThreadHeader.otherUser)
                binaryWriter.Write(str);
            }
            binaryWriter.Write(storedThreadHeader.readStatus);
            binaryWriter.Write(storedThreadHeader.subject);
            int num2 = -5;
            binaryWriter.Write(num2);
            binaryWriter.Write(storedThreadHeader.readOnly);
            binaryWriter.Write(storedThreadHeader.specialType);
            binaryWriter.Write(storedThreadHeader.specialArea);
            MailThreadItem[] storedThread = (MailThreadItem[]) this.m_storedThreads[storedThreadHeader.mailThreadID];
            if (storedThread == null)
            {
              int num3 = 0;
              binaryWriter.Write(num3);
            }
            else
            {
              int length = storedThread.Length;
              binaryWriter.Write(length);
              foreach (MailThreadItem mailThreadItem in storedThread)
              {
                binaryWriter.Write(mailThreadItem.body);
                binaryWriter.Write(mailThreadItem.mailID);
                binaryWriter.Write(mailThreadItem.mailTime.Ticks);
                binaryWriter.Write(mailThreadItem.mailTimeAsDouble);
                binaryWriter.Write(mailThreadItem.otherUser);
                binaryWriter.Write(mailThreadItem.otherUserID);
                binaryWriter.Write(mailThreadItem.readStatus);
              }
            }
          }
        }
        binaryWriter.Close();
        output.Close();
      }
      catch (Exception ex1)
      {
        try
        {
          binaryWriter?.Close();
        }
        catch (Exception ex2)
        {
        }
        try
        {
          output?.Close();
        }
        catch (Exception ex3)
        {
        }
        int num = (int) MyMessageBox.Show(SK.Text("MailScreen_Saving_Problem", "A problem occurred saving 'MailData.data'") + "\n\n" + ex1.ToString(), SK.Text("WorldMapLoader_DataSaveError_Header", "Data Save Error"));
      }
    }

    public bool IsBlocked(string user)
    {
      return this.blockedList != null && this.blockedList.Contains(user);
    }

    public List<string> getBlockedList()
    {
      List<string> blockedList = new List<string>();
      blockedList.AddRange((IEnumerable<string>) this.blockedList);
      return blockedList;
    }

    public void updateBlockedList(List<string> newList)
    {
      this.blockedList.Clear();
      this.blockedList.AddRange((IEnumerable<string>) newList);
      this.blockListChanged = true;
      this.saveBlockedList();
    }

    public void loadBlockedList()
    {
      int userId = RemoteServices.Instance.UserID;
      string settingsPath = GameEngine.getSettingsPath(false);
      FileStream input = (FileStream) null;
      BinaryReader binaryReader = (BinaryReader) null;
      this.blockedList.Clear();
      this.aggressiveBlocking = false;
      try
      {
        input = new FileStream(settingsPath + "\\MailBlockList-" + userId.ToString() + ".dat", FileMode.Open, FileAccess.Read);
        binaryReader = new BinaryReader((Stream) input);
        SparseArray sparseArray1 = new SparseArray();
        SparseArray sparseArray2 = new SparseArray();
        int num = binaryReader.ReadInt32();
        for (int index = 0; index < num; ++index)
          this.blockedList.Add(binaryReader.ReadString());
        try
        {
          this.aggressiveBlocking = binaryReader.ReadBoolean();
        }
        catch (Exception ex)
        {
        }
        binaryReader.Close();
        input.Close();
      }
      catch (Exception ex1)
      {
        try
        {
          binaryReader?.Close();
        }
        catch (Exception ex2)
        {
        }
        try
        {
          input?.Close();
        }
        catch (Exception ex3)
        {
        }
      }
    }

    public void saveBlockedList()
    {
      int userId = RemoteServices.Instance.UserID;
      string settingsPath = GameEngine.getSettingsPath(true);
      try
      {
        new FileInfo(settingsPath + "\\MailBlockList-" + userId.ToString() + ".dat").IsReadOnly = false;
      }
      catch (Exception ex)
      {
      }
      FileStream output = (FileStream) null;
      BinaryWriter binaryWriter = (BinaryWriter) null;
      try
      {
        output = new FileStream(settingsPath + "\\MailBlockList-" + userId.ToString() + ".dat", FileMode.Create);
        binaryWriter = new BinaryWriter((Stream) output);
        int count = this.blockedList.Count;
        binaryWriter.Write(count);
        foreach (string blocked in this.blockedList)
          binaryWriter.Write(blocked);
        binaryWriter.Write(this.aggressiveBlocking);
        binaryWriter.Close();
        output.Close();
      }
      catch (Exception ex1)
      {
        try
        {
          binaryWriter?.Close();
        }
        catch (Exception ex2)
        {
        }
        try
        {
          output?.Close();
        }
        catch (Exception ex3)
        {
        }
      }
    }

    public bool DoesFolderAlreadyExist(string folderName)
    {
      if (folderName.Length == 0)
        return true;
      foreach (MailFolderItem mailFolder in this.mailFolders)
      {
        if (string.Compare(mailFolder.title, folderName, true) == 0)
          return true;
      }
      return false;
    }

    public string GetFolderName(long folderID)
    {
      if (folderID == -1L)
        return SK.Text("MailScreen_Inbox", "Inbox");
      foreach (MailFolderItem mailFolder in this.mailFolders)
      {
        if (mailFolder.mailFolderID == folderID)
          return mailFolder.title;
      }
      return "?";
    }

    public string generateAttachmentsString(List<MailLink> linkList)
    {
      if (linkList.Count == 0)
        return "";
      string attachmentsString = "<!attch!>";
      foreach (MailLink link in linkList)
      {
        attachmentsString = attachmentsString + link.linkType.ToString() + "#";
        attachmentsString = attachmentsString + link.objectName + "#";
        attachmentsString += link.objectID.ToString();
        attachmentsString += "%";
      }
      return attachmentsString;
    }

    public List<MailLink> parseAttachmentString(string bodyText)
    {
      string[] separator = new string[1]{ "<!attch!>" };
      string[] strArray = bodyText.Split(separator, StringSplitOptions.RemoveEmptyEntries);
      List<MailLink> attachmentString = new List<MailLink>();
      if (strArray.Length <= 1)
      {
        if (bodyText.IndexOf(separator[0]) != 0)
          goto label_9;
      }
      try
      {
        char[] chArray = new char[1]{ '%' };
        foreach (string attString in strArray[strArray.Length - 1].Split(chArray))
        {
          if (!(attString == ""))
          {
            MailLink attachment = this.parseAttachment(attString);
            if (attachment.linkType != 4 && attachment != null)
              attachmentString.Add(attachment);
          }
        }
      }
      catch
      {
      }
label_9:
      return attachmentString;
    }

    private string getSpecialSubjectLine(MailThreadListItem item, string subject)
    {
      switch (item.specialType)
      {
        case 1:
          subject = MailManager.languageSplitString(item.subject);
          break;
        case 2:
          subject = SK.Text("MailScreen_House_Proclamation", "House Proclamation");
          break;
        case 3:
          subject = "";
          break;
        case 4:
          subject = SK.Text("MailScreen_Parish_Proclamation", "Parish Proclamation") + " : " + GameEngine.Instance.World.getParishName(item.specialArea);
          break;
        case 5:
          subject = SK.Text("MailScreen_County_Proclamation", "County Proclamation") + " : " + GameEngine.Instance.World.getCountyName(item.specialArea);
          break;
        case 6:
          subject = SK.Text("MailScreen_Province_Proclamation", "Province Proclamation") + " : " + GameEngine.Instance.World.getProvinceName(item.specialArea);
          break;
        case 7:
          subject = SK.Text("MailScreen_Country_Proclamation", "Country Proclamation") + " : " + GameEngine.Instance.World.getCountryName(item.specialArea);
          break;
      }
      return subject;
    }

    public string GetSubject(MailThreadListItem item)
    {
      string str = item.subject;
      if (item.readOnly)
        str = this.getSpecialSubjectLine(item, str);
      return GameEngine.Instance.censorString(str);
    }

    public string getBodyTextFromString(string bodyText)
    {
      string[] separator = new string[1]{ "<!attch!>" };
      string[] strArray = bodyText.Split(separator, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length > 1)
        return strArray[0];
      return bodyText.IndexOf(separator[0]) == 0 ? "" : bodyText;
    }

    public bool stringContainsAttachments(string bodyText)
    {
      return this.parseAttachmentString(bodyText).Count > 0;
    }

    private MailLink parseAttachment(string attString)
    {
      char[] chArray = new char[1]{ '#' };
      string[] strArray = attString.Split(chArray);
      if (strArray.Length > 0)
      {
        try
        {
          MailLink attachment = new MailLink();
          attachment.linkType = Convert.ToInt32(strArray[0]);
          attachment.objectName = strArray[1];
          if (strArray.Length > 2)
            attachment.objectID = Convert.ToInt32(strArray[2]);
          return attachment;
        }
        catch (Exception ex)
        {
        }
      }
      return (MailLink) null;
    }

    public static string languageSplitString(string str)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      string[] strArray = str.Split('§');
      Thread.CurrentThread.CurrentCulture = CultureInfo.InstalledUICulture;
      if (strArray.Length <= 0)
        return str;
      string lower = Program.mySettings.LanguageIdent.ToLower();
      string str1 = "";
      switch (lower)
      {
        case "de":
          if (strArray.Length >= 2)
          {
            str1 = strArray[1];
            break;
          }
          break;
        case "fr":
          if (strArray.Length >= 3)
          {
            str1 = strArray[2];
            break;
          }
          break;
        case "ru":
          if (strArray.Length >= 4)
          {
            str1 = strArray[3];
            break;
          }
          break;
        case "es":
          if (strArray.Length >= 5)
          {
            str1 = strArray[4];
            break;
          }
          break;
        case "pl":
          if (strArray.Length >= 6)
          {
            str1 = strArray[5];
            break;
          }
          break;
        case "pt":
          if (strArray.Length >= 7)
          {
            str1 = strArray[6];
            break;
          }
          break;
        case "it":
          if (strArray.Length >= 8)
          {
            str1 = strArray[7];
            break;
          }
          break;
        case "tr":
          if (strArray.Length >= 9)
          {
            str1 = strArray[8];
            break;
          }
          break;
        case "zhhk":
          if (strArray.Length >= 10)
          {
            str1 = strArray[9];
            break;
          }
          break;
        case "zh":
          if (strArray.Length >= 11)
          {
            str1 = strArray[10];
            break;
          }
          break;
        case "ko":
          if (strArray.Length >= 12)
          {
            str1 = strArray[11];
            break;
          }
          break;
        case "jp":
          if (strArray.Length >= 13)
          {
            str1 = strArray[12];
            break;
          }
          if (strArray.Length >= 1)
          {
            str1 = strArray[0];
            break;
          }
          break;
        default:
          if (strArray.Length >= 1)
          {
            str1 = strArray[0];
            break;
          }
          break;
      }
      if (str1.Length > 30 && strArray.Length > 1 && lower == "jp")
      {
        if (str1.Substring(0, 24) == "Settled in nicely, Sire?")
          return "ご道中は快適でしたか？それは幸いです。我らがキングダムにようこそ、ここは冒険の先に栄光が待つ世界、それは王座への道！" + Environment.NewLine + Environment.NewLine + "では手始めに、世界地図をご覧いただけますか？こちらを見るといくつもの村が存在していることにお気づきかと思います、このひとつひとつが実在するプレイヤーによって管理されています。その多くは同士として貴殿を迎え入れ有事の際には助力を申し出てくれるでしょうが、中には襲撃の機を伺っている勢力も存在します！「友人」は慎重にお選びを…" + Environment.NewLine + Environment.NewLine + "自らの意志でこの地を選んだにせよランダムな村を割り当てられたにせよ、これから貴殿は新たな村を制圧していき領土を広げるべく、未知の領域に足を踏み入れることとなります！" + Environment.NewLine + Environment.NewLine + "王座を勝ち取らんがための冒険は小さな村より始まります。すでに貴殿に忠誠を誓っている者たちが何人もいるとはいえ、まだまだ経済的に繁栄しているとは言い難い情勢、民を守るための強固な城塞と軍が戦果を上げるに必要です。" + Environment.NewLine + Environment.NewLine + "村の経済を活性化させるべく、新たな技術を研究しなくてはなりません。農業の研究は、小作人たちに日々の糧をもたらし労働意欲を増大させるでしょう。軍事力を増強すれば木製の城塞防衛を頑強な石壁に強化できます。産業が進歩すれば新たな武器などで軍をさらに強くできるはずです！さあ今はまだ小さき村を各種産業の一大拠点へと発展させるのです！" + Environment.NewLine + Environment.NewLine + "この世界での貴殿の最初の姿は「村の愚者」です。お気持ちは分かります、ですが絶望せぬように！栄誉を得ていくことで貴殿は「従士」となり、「騎士従者」となり、「伯爵」となり、ついには「皇太子」にまで登りつめるのです。ランクが上がれば新たな能力が解放されていきます。例えば、派閥を組んだりプレイヤーの村を壊滅させたりできるように。さらにランクアップにより手に入る研究ポイントを投資していくことで、やがては上位研究をも解放できるようになっていきます。" + Environment.NewLine + Environment.NewLine + "王座への道のりは自由意志に委ねられています。他プレイヤーへ力を示すも良し、派閥に加入するも良し、横暴なるハウスを打倒するも良し、または交易で力を蓄えるも静かに畜牛を育てるも良しなのです。" + Environment.NewLine + Environment.NewLine + "ただし油断は禁物…ここは派閥やハウス、領主や臣下、そして選挙といった政治的要素が無数に絡まる世界。近隣の勢力もまた貴殿同様の野心を心に抱き、達成するためにはなりふり構わないという者もいるでしょう。しかしここで重要なのは他プレイヤーとの協力関係の構築です。戦略を議論し、忠義を尽くし、この世界における確固たる礎を築くのです。それでも貴殿の勢力が拡大すれば、それに反発する者も出てくるはず、力づくで貴殿の村を奪おうとする輩にはご注意ください。" + Environment.NewLine + Environment.NewLine + "まずはゲームのチュートリアルをクリアすることをお勧めします。いくつかの基本を習得してから、クエストや実績、イベントなどに挑戦していき、まだ見ぬチャレンジやリワード獲得を目指すといいでしょう。" + Environment.NewLine + Environment.NewLine + "参戦に心より感謝します" + Environment.NewLine + "The Kingdoms Team";
        if (str1.Substring(0, 17) == "Good day My Lord,")
          return "我が主君よ、ここに素晴らしき新世界は開かれん、だが用心されよ。この新世界はこれまで以上に過酷な地、敵領主は我らが頼れる騎士よりも時に迅速かつ狡猾。ハウスの規模は縮小、それがために褒美や英雄の殿堂への不朽の名声を巡る争いは苛烈を極めます。紳士淑女はランキングのトップ10に達することができれば、もしくは勝利を挙げたハウスのメンバー権を保持できれば、褒美を獲得できる資格を得ます。とはいえ、見事資格を手にしたとしても、受け取れる褒美はわずかにひとつ。そして、これまで駆使してきた戦術や奇策は引き続き通用するかもしれませんが、ここでの戦いはかつてないほどの過酷さ…" + Environment.NewLine + Environment.NewLine + "Domination 世界の中核を成すのは、新たなゲームルール。以下の変更は只今から適用となります:" + Environment.NewLine + Environment.NewLine + "1. 禁止命令は研究/実行不可" + Environment.NewLine + "2. 村や首府内の建物や城塞の建造速度が4倍" + Environment.NewLine + "3. 研究時間が半分に短縮" + Environment.NewLine + "4. 時間制限の91日を経過後、世界は終焉" + Environment.NewLine + "5. グローリーラウンドや年代なし。世界が終焉した時に最も多くのグローリーを獲得していたハウスが勝利 " + Environment.NewLine + "6. 各ハウス所属数を3つの派閥に制限。最初の派閥は自動的に受け入れられ、残りは投票で決せられる" + Environment.NewLine + "7. 研究レベルの2倍の数の村を保有可能" + Environment.NewLine + "8. 宴から得られる栄誉が2倍" + Environment.NewLine + "9. 派閥を組めるようになるランクが「参事」（12）に低下 " + Environment.NewLine + "10. 最初の村の「平穏」時間が5日から3日に短縮" + Environment.NewLine + "11. 同ランク以上のプレイヤーを攻撃しても栄誉ペナルティなし（ただし壊滅には適用）" + Environment.NewLine + "12. 派閥メンバーも互いに互いを攻撃可能" + Environment.NewLine + "13. ランクごとの臣下数増加" + Environment.NewLine + "14. 首府の旗が通常の3倍の速さで出現。最大税率が9から50に増加。税から受け取れるゴールド量が2倍" + Environment.NewLine + "15. 修道僧が地域の首府に影響を与えることができるようになった。影響にかかるコストは投票一票につき100信仰ポイントに増加" + Environment.NewLine + "16. 宝の城塞の出現率が2倍となった。24時間ごとではなく12時間に一回攻撃可能" + Environment.NewLine + "17. クエストからのグローリー取得不可" + Environment.NewLine + "18. 15日経過後、村の許可書の出現率低下。30日経過後、村の許可書の新規出現なし" + Environment.NewLine + Environment.NewLine + "以上を見て分かる通り、Domination 世界は真の強者のみが参戦できる世界。しかしそこには真の栄光が、栄誉が、そしてお宝が待っている。" + Environment.NewLine + Environment.NewLine + "貴殿に幸運あれ！";
        if (str1.Substring(0, 26) == "Here are a few suggestions")
          return "いくつかのクエストをクリアする" + Environment.NewLine + Environment.NewLine + "ストロングホールド・キングダムには150を超えるクエストが存在します。そのひとつひとつをこなしていけば、気がつけば農園主、交易人、外交官、軍長などになっているでしょう。「病の根絶」から「建造ラッシュ」に至るまで、全てに対して規律を持って取り組んでください。クエストを無事にクリアすれば、プレイヤーには報酬が与えられます。中には報酬としてクエストホイールを回す権利が与えられるものもあり、運が良ければカードパックやカードポイント、研究ポイントなどが当たります。クエストにアクセスするには「聖杯」アイコンをクリックしてください。" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "紋章デザイン" + Environment.NewLine + Environment.NewLine + "紋章デザイナーを使ってオリジナルの紋章を作成して、ゲーム内の盾アイコンに配置することができます。世界地図上やプレイヤープロフィール画面、ランキングなどで表示されます。ランクを上げクエストをこなしていくことで、紋章に使えるデザインが増えていきます。すでに200種類以上のデザインが存在し、さらに今後のアップデートで増えていく予定です。加えてカラーリングも32種類もあるので、組み合わせの可能性はほぼ無限大！紋章のデザインを行いたい場合は、シンプルに自分の盾アイコンをクリックしてください。" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "フレンドをゲーム世界に招待" + Environment.NewLine + Environment.NewLine + "「フレンド招待」を利用したプレイヤーには、招待したプレイヤーが初めてクラウンを購入した場合に、最大で$80分のクラウンがプレゼントされます。フレンド1人につき、最大で1500クラウンが獲得できる計算です。「フレンド招待」は「マイ アカウント」内にあります。" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "報告を確認" + Environment.NewLine + Environment.NewLine + "報告画面にて過去のイベントやその結果などを確認できます。しばらくゲームをプレイしていなかった場合などに近況を確認するのに便利です。直近の襲撃、資源探索、管財人の選挙結果、新たに出現した敵や完了した研究などが報告画面には表示されます。ログイン時に毎回確認することを習慣にすると良いでしょう。「報告」は画面上のツールバーにある羊皮紙タブをクリックすると開くことができます。" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "アバターをデザイン" + Environment.NewLine + Environment.NewLine + "プレイヤーのアバターは男性または女性のポートレートで、ランキングや世界地図、プレイヤープロフィール画面などに表示されます。アバターは自由にカスタマイズが可能で、髪型や性別、服装や武器などを変更できます。アバターの性別はプレイヤーのランクの性別で決定されます。アバターをデザインするには「設定」内にある「アバター編集」をクリックしましょう。" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "プレミアムトークンを活用" + Environment.NewLine + Environment.NewLine + "チュートリアル期間中は「2日間" + Environment.NewLine + Environment.NewLine + "プレミアムトークン」が有効になっています。有効中は複数の建物を同時に建造、研究待機列の使用、オフライン中の軍や交易人、偵察の自動派遣など様々なアドバンテージを利用できます。2日間 プレミアムトークンの期限が切れても、画面上の「全カードを表示」をクリックして「プレミアムトークン」を選べば新たに購入できます。" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "行政区の首府へ寄付" + Environment.NewLine + Environment.NewLine + "行政区に所属する全てのプレイヤーは行政区の首府の建造物のアップグレードに貢献することができます。貢献すると自村に様々な恩恵がもたらされます。例えば教会は行政区内にあす全ての村の日々の信仰ポイントを増やしてくれます。公共庭園は人気度-栄誉のボーナス倍率を上昇させます。首府の建物への寄付やアップグレードを行うには、画面上のツールバーから首府タブを開いてください。そして村の画面を選び、寄付先の建物を選んで寄付する資源をクリックします。" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "プレイしてくれてありがとうございます" + Environment.NewLine + Environment.NewLine + "The Kingdoms Team";
      }
      if (str1.Length == 4 && strArray.Length > 1 && str1[0] == '<' && str1[3] == '>')
      {
        char ch1 = str1[1];
        char ch2 = str1[2];
        switch (ch1)
        {
          case '2':
            return SK.Text("WorldSelect_2ndEra_Mail_01", "I must congratulate you Sire, for you have made it to the end of the First Era still standing! My Lord has faced great adversity, but now the air about you is positively regal! Your intelligence, beauty and strength remain intact and you will need all these things for the road ahead...") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_2ndEra_Mail_02", "The Second Era of Stronghold Kingdoms is upon us with new challenges. Prepare yourself for added layers of strategy and complexity, testing the skills of even the most veteran players!") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_2ndEra_Mail_03", "These changes are now in effect:") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_2ndEra_Mail_04", "1. Armies, scouts, and monks move at increased speed.") + Environment.NewLine + SK.Text("WorldSelect_2ndEra_Mail_05", "2. Goods have been cleared from all Markets, with prices reset to their starting levels.") + Environment.NewLine + SK.Text("WorldSelect_2ndEra_Mail_06", "3. The Faith Point cost for Interdiction and Excommunication has increased.") + Environment.NewLine + SK.Text("WorldSelect_2ndEra_Mail_07", "4. Votes and officers in all capitals have been cleared and the Glory Race has restarted.") + Environment.NewLine + SK.Text("WorldSelect_2ndEra_Mail_08", "5. More Glory is earned for holding territory.") + Environment.NewLine + SK.Text("WorldSelect_2ndEra_Mail_09", "6. Monks can influence voting at the county level.") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_2ndEra_Mail_10", "For the skilled strategist there is much to gain, all members of the house to win this Era will be rewarded with five wheel spins!") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_Era_Mail_Good_Luck", "Good luck!");
          case '3':
            return SK.Text("WorldSelect_3rdEra_Mail_01", "Sire, the Third Era awaits! This exciting Era is designed to increase the pace of gameplay and give players more freedom to extend their power and influence within the world...") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_3rdEra_Mail_02", "These changes are now in effect:") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_3rdEra_Mail_03", "1. Armies, scouts, and monks move at increased speed.") + Environment.NewLine + SK.Text("WorldSelect_3rdEra_Mail_04", "2. Goods have been cleared from all Markets, with prices reset to their starting levels.") + Environment.NewLine + SK.Text("WorldSelect_3rdEra_Mail_05", "3. The Faith Point cost for Interdiction and Excommunication has increased.") + Environment.NewLine + SK.Text("WorldSelect_3rdEra_Mail_06", "4. Votes and officers in all capitals have been cleared and the Glory Race has restarted.") + Environment.NewLine + SK.Text("WorldSelect_3rdEra_Mail_07", "5. More Glory is earned for holding territory.") + Environment.NewLine + SK.Text("WorldSelect_3rdEra_Mail_08", "6. Crown Princes may now access up to 30 villages.") + Environment.NewLine + SK.Text("WorldSelect_3rdEra_Mail_09", "7. Honour production has been increased for Banqueting and for Popularity.") + Environment.NewLine + SK.Text("WorldSelect_3rdEra_Mail_00", "8. Honor received from destroying hostile AI castles has been increased.") + Environment.NewLine + SK.Text("WorldSelect_3rdEra_Mail_11", "9. Certain upgradeable parish buildings can now gain additional levels.") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_3rdEra_Mail_12", "For the skilled strategist there is much to gain, all members of the house to win this Era will be rewarded with five wheel spins!") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_Era_Mail_Good_Luck", "Good luck!");
          case '4':
            return SK.Text("WorldSelect_4thEra_Mail_01", "My Liege! How glad I am to see you in one piece. The brutal Third Era brought with it higher caps for villages, increased Honour production and more, however my spies tell me the Fourth Era is not quite what we were expecting… Although there are similarities, such as an increased village cap, there is also much to learn. ") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_4thEra_Mail_02", "These changes are now in effect:") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_4thEra_Mail_03", "1. Armies, scouts, and monks move at increased speed.") + Environment.NewLine + SK.Text("WorldSelect_4thEra_Mail_04", "2. Goods have been cleared from all Markets, with prices reset to their starting levels.") + Environment.NewLine + SK.Text("WorldSelect_4thEra_Mail_05", "3. The Faith Point cost for Interdiction and Excommunication has increased.") + Environment.NewLine + SK.Text("WorldSelect_4thEra_Mail_06", "4. Votes and officers in all capitals have been cleared and the Glory Race has restarted.") + Environment.NewLine + SK.Text("WorldSelect_4thEra_Mail_07", "5. Crown Princes may now own up to 40 villages.") + Environment.NewLine + SK.Text("WorldSelect_4thEra_Mail_08", "6. A Military School can be built in a parish, which gives access to Bombards.") + Environment.NewLine + SK.Text("WorldSelect_4thEra_Mail_09", "7. Certain upgradable Parish buildings can now gain 5 additional levels.") + Environment.NewLine + SK.Text("WorldSelect_4thEra_Mail_10", "8. Players who own more than 10 villages will no longer have to pay the extra honour cost to regain a village, if one of their villages is lost.") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_4thEra_Mail_11", "As you can see my Lord, the Fourth Era may prove challenging. However, I believe there is much to gain and this new weapon is quite intriguing!") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_4thEra_Mail_12", "For the skilled strategist there is much to gain, all members of the house to win this Era will be rewarded with five wheel spins!") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_Era_Mail_Good_Luck", "Good luck!");
          case '5':
            return SK.Text("WorldSelect_5thEra_Mail_01", "Greetings Sire!  It is good to see you have made it through the trials of the Fourth Era and ready to face the challenges that the Fifth Era brings!  Hopefully you have brought many loyal friends and staunch allies along with you in to this new era as well, for you will need them if you are to succeed!") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_5thEra_Mail_02", "These changes are now in effect:") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_5thEra_Mail_03", "1. Armies, scouts, and monks move at increased speed.") + Environment.NewLine + SK.Text("WorldSelect_5thEra_Mail_04", "2. Goods have been cleared from all Markets, with prices reset to their starting levels.") + Environment.NewLine + SK.Text("WorldSelect_5thEra_Mail_05", "3. The Faith Point cost for Interdiction and Excommunication has increased.") + Environment.NewLine + SK.Text("WorldSelect_5thEra_Mail_06", "4. Votes and officers in all capitals have been cleared and the Glory Race has restarted.") + Environment.NewLine + SK.Text("WorldSelect_5thEra_Mail_07", "5. Military Schools can be upgraded to level 5.") + Environment.NewLine + SK.Text("WorldSelect_5thEra_Mail_08", "6. Treasure Castles are twice as likely to appear.") + Environment.NewLine + SK.Text("WorldSelect_5thEra_Mail_09", "7. Only members of a House can be candidates for County elections.") + Environment.NewLine + SK.Text("WorldSelect_5thEra_Mail_10", "8. To be eligible for the office of sheriff, a candidate must belong to a House which controls at least 30% of the parishes in the county.") + Environment.NewLine + SK.Text("WorldSelect_5thEra_Mail_11", "9. Large Houses gain Glory more slowly than small Houses.  A House with 60 members will gain one-third the amount of Glory that would be normally gained.") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_5thEra_Mail_12", "As you can see Sire, you will need to maneuver skillfully in this new political arena and spread your villages and influence all over the realm in order to assure victory for you and your chosen comrades in arms.") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_5thEra_Mail_13", "For the skilled strategist there is much to gain, all members of the house to win this Era will be rewarded with five wheel spins!") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_Era_Mail_Good_Luck", "Good luck!");
          case '6':
            return SK.Text("WorldSelect_6thEra_Mail_01", "Exciting times are upon us, Sire! Your strength, tenacity and cunning are sure to serve you well as you confront the challenges ahead.  The Sixth Era is here and the changes it brings add yet another layer of strategy and complexity to the world!") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_6thEra_Mail_02", "These changes are now in effect:") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_6thEra_Mail_03", "1. Armies, scouts, and monks move at increased speed.") + Environment.NewLine + SK.Text("WorldSelect_6thEra_Mail_04", "2. Goods have been cleared from all Markets, with prices reset to their starting levels.") + Environment.NewLine + SK.Text("WorldSelect_6thEra_Mail_05", "3. The Faith Point cost for Interdiction and Excommunication has increased.") + Environment.NewLine + SK.Text("WorldSelect_6thEra_Mail_06", "4. Votes and officers in all capitals have been cleared and the Glory Race has restarted.") + Environment.NewLine + SK.Text("WorldSelect_6thEra_Mail_07", "5. The cost to recruit troops in capitals has been halved.") + Environment.NewLine + SK.Text("WorldSelect_6thEra_Mail_08", "6. Weapons can now be found in stashes.") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_6thEra_Mail_09", "For the skilled strategist there is much to gain, all members of the house to win this Era will be rewarded with five wheel spins!") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_6thEra_Mail_10", "You will need to keep your allies close and eyes open as you carve out a path for your medieval empire in this new age. This world is now a place where weapons are plentiful, diplomacy is limited and attacks are swift, so be vigilant! Old enemies and new rivals may emerge as the world prepares for The Final Era...") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_Era_Mail_Good_Luck", "Good luck!");
          case '7':
            return SK.Text("WorldSelect_7thEra_Mail_01", "The end times are upon us, my liege! This world has now entered its Final Era and while you have made it this far, the road ahead will prove the ultimate test of your courage, character and ingenuity!") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_02", "These changes are now in effect:") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_03", "1. Armies, scouts, and monks move at increased speed.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_04", "2. Goods have been cleared from all Markets, with prices reset to their starting levels.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_05", "3. The Faith Point cost for Interdiction and Excommunication has increased.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_06", "4. Votes and officers in all capitals have been cleared and the Glory Race has restarted.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_07", "5. Capital guilds can now be upgraded an additional five levels.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_08", "6. No new players may join this game world. Only those who previously had a village on this world may enter it.") + Environment.NewLine + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_09", "Please also note the unique gameplay mechanics that the Final Era brings with it:") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_10", "1. The Final Era begins with 150 'Royal Towers' on the map.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_11", "2. 'The Button' can be pressed by the Marshall of the House that controls all towers.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_12", "3. Pressing 'The Button' ends the world, determining which players claim each reward tier.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_13", "4. The Glory Race has no end and Houses are no longer eliminated.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_15", "5. After each Glory Round towers respawn and tower control is reset.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_16", "6. Each Round brings the number of towers down by 10, with a minimum of 20.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_17", "7. A new 'Royal Towers' tab has been added to the Glory screen.") + Environment.NewLine + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_18", "'Royal Towers' are the key to obtaining Final Victory and the ability for one player to end the game world. Once a single House controls all Royal Towers in a game world the House Marshall may choose to achieve Final Victory by pressing 'The Button', which will no longer be greyed out. Doing so will allow all House members to claim wondrous prizes and end the game world forever. ") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_19", "Here are the most important things to note about Royal Towers:") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_20", "1. A captain is needed to attack a Royal Tower and only capture-type attacks may be launched against them.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_21", "2. Attacking a Royal Tower will remove any interdiction from the source village.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_22", "3. Royal Towers cannot be repaired, rebuilt or reinforced by players.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_23", "4. Royal Towers cannot be interdicted.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_24", "5. If a player captures a Royal Tower their House gains control of that tower.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_25", "6. After they are captured Royal Towers are immediately rebuilt and repaired.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_26", "7. The Final Era begins with 150 Royal Towers spawning across the map in random locations.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_27", "8. After each Glory Round all towers disappear, with new towers appearing in random locations.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_28", "9. The total number of towers is reduced by 10 at the end of each Round and control of all towers is reset.") + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_29", "10. The minimum number of Royal Towers in a world is 20.") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_30", "The final struggle for this realm has begun! The time has come to claim your rightful place amongst the true legends, not just of this world but of all Stronghold Kingdoms worlds. You have come far and achieved much, it is only right that you should now be justly rewarded for your noble efforts.");
          case 'A':
            if (lower == "jp")
              return "ストロングホールド・キングダムの世界にようこそ";
            switch (ch2)
            {
              case 'A':
                return "Welcome to Stronghold Kingdoms!";
              case 'B':
                return "Willkommen bei Stronghold Kingdoms!";
              case 'C':
                return "Bienvenue dans Stronghold Kingdoms !";
              case 'D':
                return "Добро пожаловать в Stronghold Kingdoms!";
              case 'E':
                return "¡Bienvenido a Stronghold Kingdoms!";
              case 'F':
                return "Witaj w grze Twierdza: Królestwa!";
              case 'G':
                return "Boas-vindas ao Stronghold Kingdoms!";
              case 'H':
                return "Ti do il benvenuto a Stronghold Kingdoms!";
              case 'I':
                return "Stronghold Kingdoms’a hoş geldiniz!";
              case 'J':
                return "歡迎賞玩《要塞：王國》";
              case 'K':
                return "欢迎赏玩《要塞王国》";
              case 'L':
                return "스트롱홀드 킹덤즈에 오신 것을 환영합니다!";
            }
            break;
          case 'B':
            if (lower == "jp")
              return "お見事！ストロングホールド・キングダムのチュートリアルを無事に完遂しましたね。";
            switch (ch2)
            {
              case 'A':
                return "Congratulations! You have completed the Stronghold Kingdoms Tutorial.";
              case 'B':
                return "Herzlichen Glückwunsch! Sie haben das Stronghold Kingdoms Tutorial abgeschlossen.";
              case 'C':
                return "Félicitations ! Vous venez de compléter le tutoriel de Stronghold Kingdoms.";
              case 'D':
                return "Поздравляем! Вы прошли обучение Stronghold Kingdoms. ";
              case 'E':
                return "¡Felicidades! Has completado el Tutorial de Stronghold Kingdoms";
              case 'F':
                return "Gratulacje! Udało Ci się ukończyć samouczek Twierdza: Królestwa.";
              case 'G':
                return "Parabéns! Você completou o tutorial do Stronghold Kingdoms.";
              case 'H':
                return "Congratulazioni! Hai completato il tutorial di Stronghold Kingdoms.";
              case 'I':
                return "Tebrikler! Stronghold Kingdoms Eğitim Bölümü’nü tamamladınız.";
              case 'J':
                return "祝賀您！您已完成了《要塞：王國》教程。";
              case 'K':
                return "祝贺您！您已完成了《要塞王国》教程。";
              case 'L':
                return "축하합니다! 스트롱홀드 킹덤즈 튜토리얼을 완료했습니다.";
            }
            break;
          case 'C':
            switch (ch2)
            {
              case 'A':
                return "Welcome to the Second Age";
              case 'B':
                return "Willkommen in der zweiten Epoche!";
              case 'C':
                return "Bienvenue dans la Deuxième Ère";
              case 'D':
                return "Добро пожаловать во Вторую Эпоху!";
              case 'E':
                return "Bienvenido a la Segunda Edad";
              case 'F':
                return "Witaj w Drugiej Epoce";
              case 'G':
                return "Boas-vindas à Segunda Era";
              case 'H':
                return "Un caloroso benvenuto alla Seconda Epoca";
              case 'I':
                return "İkinci Çağ’a hoş geldiniz!";
              case 'J':
                return "歡迎來到第二紀元";
              case 'K':
                return "欢迎来到第二纪元";
              case 'L':
                return "두 번째 시대에 오신 것을 환영합니다";
            }
            break;
          case 'D':
            switch (ch2)
            {
              case 'A':
                return "Welcome to the Third Age";
              case 'B':
                return "Willkommen in der dritten Epoche!";
              case 'C':
                return "Bienvenue dans la Troisième Ère";
              case 'D':
                return "Добро пожаловать в Третью Эпоху";
              case 'E':
                return "Tercera Edad Correo In-Game:";
              case 'F':
                return "Witaj w Trzeciej Epoce";
              case 'G':
                return "Boas-vindas à Terceira Era";
              case 'H':
                return "Un caloroso benvenuto alla Terza Epoca";
              case 'I':
                return "Üçüncü Çağ’a Hoşgeldiniz!";
              case 'J':
                return "歡迎來到第三紀元！";
              case 'K':
                return "欢迎来到第三时代！";
              case 'L':
                return "세 번째 시대에 오신 것을 환영합니다!";
            }
            break;
          case 'E':
            if (lower == "jp")
              return "Domination 世界にようこそ";
            switch (ch2)
            {
              case 'A':
                return "Welcome to Domination World";
              case 'B':
                return "Willkommen auf der Domination Welt";
              case 'C':
                return "Bienvenue sur le Monde de Domination !";
              case 'D':
                return "Добро пожаловать в Мир Domination!";
              case 'E':
                return "Bienvenido al Mundo Domination.";
              case 'F':
                return "Witaj w Swiecie Domination!";
              case 'G':
                return "Boas vindas ao Mundo de Domination";
              case 'H':
                return "Benvenuto nel Mondo Domination";
              case 'I':
                return "Domination Dünyasina Hosgeldiniz";
              case 'J':
                return "歡迎來到統馭世界";
              case 'K':
                return "欢迎来到统治世界";
              case 'L':
                return "통치의 세계에 오신 것을 환영합니다.";
            }
            break;
          case 'F':
            return SK.Text("WorldSelect_4thAge_Header", "Welcome to the Fourth Age!");
          case 'G':
            return SK.Text("FourthAge_Mail_01", "My Liege! How glad I am to see you in one piece. The brutal Third Age brought with it higher caps for villages, increased Honour production and more, however my spies tell me the Fourth Age is not quite what we were expecting… Although there are similarities, such as an increased village cap, there is also much to learn. Your forces move faster than ever before, Houses and Factions have adopted a new structure to encourage combat and the cost of Interdiction has been raised. I’ve also heard word of a new Military School and Bombard, both of which could help us crush our enemies and take the crown.") + Environment.NewLine + Environment.NewLine + SK.Text("FourthAge_Mail_02", "Core to the Fourth Age is the new rule set, which is now in effect:") + Environment.NewLine + Environment.NewLine + SK.Text("FourthAge_Mail_03", "1. Crown Princes may now own up to 40 villages.") + Environment.NewLine + SK.Text("FourthAge_Mail_04", "2. Army and Scout movement speeds are three times faster than in the First Age.") + Environment.NewLine + SK.Text("FourthAge_Mail_05", "3. Weapons can no longer be sold or purchased at Markets.") + Environment.NewLine + SK.Text("FourthAge_Mail_06", "4. Goods have been cleared from all Markets, with prices reset to their starting level.") + Environment.NewLine + SK.Text("FourthAge_Mail_07", "5. The Faith Point cost for Interdiction has been increased.") + Environment.NewLine + SK.Text("FourthAge_Mail_08", "6. A Military School can be built in a parish, which gives access to Bombards.") + Environment.NewLine + SK.Text("FourthAge_Mail_09", "7. All in-game Factions and Houses have been disbanded.") + Environment.NewLine + SK.Text("FourthAge_Mail_10", "8. All capital forums and walls will be cleared.") + Environment.NewLine + SK.Text("FourthAge_Mail_11", "9. Houses are limited to 3 Factions, with the first Faction to apply accepted automatically and all other factions voted in.") + Environment.NewLine + SK.Text("FourthAge_Mail_12", "10. Certain upgradable Parish buildings can now gain 5 additional levels.") + Environment.NewLine + SK.Text("FourthAge_Mail_13", "11. Players who own more than 10 villages will no longer have to pay the extra honour cost to regain a village, if one of their villages is captured.") + Environment.NewLine + Environment.NewLine + SK.Text("FourthAge_Mail_14", "As you can see my Lord, the Fourth Age may prove challenging. However I believe there is much to gain, whether we fight or trade our way to victory.") + Environment.NewLine + Environment.NewLine + SK.Text("FourthAge_Mail_15", "Good luck Sire");
          case 'I':
            return SK.Text("WorldSelect_5thAge_Header", "Welcome to the Fifth Age!");
          case 'J':
            return SK.Text("FifthAge_Mail_01", "Greetings Sire!  It is good to see you have made it through the trials of the Fourth Age and ready to face the challenges that the Fifth Age brings!  Hopefully you have brought many loyal friends and staunch allies along with you in to this new era as well, for you will need them if you are to succeed!") + Environment.NewLine + Environment.NewLine + SK.Text("FifthAge_Mail_02", "Core to the Fifth Age is the new rule set, which is now in effect.") + Environment.NewLine + Environment.NewLine + SK.Text("FifthAge_Mail_03", "1. Military Schools can be upgraded to level 5.") + Environment.NewLine + SK.Text("FifthAge_Mail_04", "2. Treasure Castles are twice as likely to appear.") + Environment.NewLine + SK.Text("FifthAge_Mail_05", "3. All factions and houses have been disbanded.") + Environment.NewLine + SK.Text("FifthAge_Mail_06", "4. All capital forums and walls have been cleared.") + Environment.NewLine + SK.Text("FifthAge_Mail_07", "5. Only members of a House can be candidates for County elections.") + Environment.NewLine + SK.Text("FifthAge_Mail_08", "6. To be eligible for the office of sheriff, a candidate must belong to a House which controls at least 30% of the parishes in the county.") + Environment.NewLine + SK.Text("FifthAge_Mail_09", "7. Glory gained overall is increased by one-third.") + Environment.NewLine + SK.Text("FifthAge_Mail_10", "8. Large Houses gain Glory more slowly than small Houses.  A House with 60 members will gain one-third the amount of Glory that would be normally gained.") + Environment.NewLine + SK.Text("FifthAge_Mail_11", "As you can see Sire, you will need to maneuver skillfully in this new political arena and spread your villages and influence all over the realm in order to assure victory for you and your chosen comrades in arms.") + Environment.NewLine + Environment.NewLine + SK.Text("FourthAge_Mail_15", "Good luck Sire");
          case 'K':
            return SK.Text("WorldSelect_6thAge_Header", "Welcome to the Sixth Age!");
          case 'L':
            return SK.Text("SixthAge_Mail_01", "Exciting times are upon us, Sire! Your strength, tenacity and cunning are sure to serve you well as you confront the challenges ahead.  The Sixth Age is here and the changes it brings add yet another layer of strategy and complexity to the world!") + Environment.NewLine + Environment.NewLine + SK.Text("SixthAge_Mail_02", "Please be aware of the following changes:") + Environment.NewLine + Environment.NewLine + SK.Text("SixthAge_Mail_03", "1. The cost to recruit troops in capitals has been halved.") + Environment.NewLine + SK.Text("SixthAge_Mail_04", "2. Movement speed has increased for armies and scouts, they now move at four times the speed of the First Age.") + Environment.NewLine + SK.Text("SixthAge_Mail_05", "3. Monk movement speed has been doubled.") + Environment.NewLine + SK.Text("SixthAge_Mail_06", "4. Weapons can now be found in stashes.") + Environment.NewLine + SK.Text("SixthAge_Mail_07", "5. Weapons can once again be bought and sold at markets.") + Environment.NewLine + SK.Text("SixthAge_Mail_08", "6. The maximum faction size is now 10 players.") + Environment.NewLine + SK.Text("SixthAge_Mail_09", "7. 1 million Glory Points are required to win each Glory Round.") + Environment.NewLine + SK.Text("SixthAge_Mail_11", "You will need to keep your allies close and eyes open as you carve out a path for your medieval empire in this new age. This world is now a place where weapons are plentiful, diplomacy is limited and attacks are swift, so be vigilant! Old enemies and new rivals may emerge as the world prepares for The Final Age...") + Environment.NewLine + Environment.NewLine + SK.Text("FourthAge_Mail_15", "Good luck Sire");
          case 'M':
            return SK.Text("WorldSelect_7thAge_Header", "Welcome to the Final Age!");
          case 'N':
            return SK.Text("SeventhAge_Mail_01", "The end times are upon us, my liege! This world has now entered its Final Age and, while you have made it this far, the road ahead will prove the ultimate test of your courage, character and ingenuity! Please be aware of the changes that this final age brings with it:") + Environment.NewLine + Environment.NewLine + SK.Text("SeventhAge_Mail_02", "1. The Final Age begins with 150 'Royal Towers' on the map.") + Environment.NewLine + SK.Text("SeventhAge_Mail_03", "2. 'The Button' can be pressed by the Marshall of the House that controls all towers.") + Environment.NewLine + SK.Text("SeventhAge_Mail_04", "3. Pressing 'The Button' ends the world, determining which players claim each reward tier!") + Environment.NewLine + SK.Text("SeventhAge_Mail_05", "4. The Glory Race has no end and Houses are no longer eliminated.") + Environment.NewLine + SK.Text("SeventhAge_Mail_07", "5. After each Glory Round towers respawn and tower control is reset.") + Environment.NewLine + SK.Text("SeventhAge_Mail_08", "6. Each Round brings the number of towers down by 10, with a minimum of 20.") + Environment.NewLine + SK.Text("SeventhAge_Mail_09", "7. A new 'Royal Towers' tab has been added to the Glory screen.") + Environment.NewLine + SK.Text("SeventhAge_Mail_10", "8. Capital guilds can now be upgraded an additional five levels.") + Environment.NewLine + SK.Text("SeventhAge_Mail_11", "9. No new players may join this game world. Only those who previously had a village on this world may enter it.") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_7thEra_Mail_18", "'Royal Towers' are the key to obtaining Final Victory and the ability for one player to end the game world. Once a single House controls all Royal Towers in a game world the House Marshall may choose to achieve Final Victory by pressing 'The Button', which will no longer be greyed out. Doing so will allow all House members to claim wondrous prizes and end the game world forever. ") + Environment.NewLine + Environment.NewLine + SK.Text("SeventhAge_Mail_13", "Here are the most important things to note about Royal Towers:") + Environment.NewLine + Environment.NewLine + SK.Text("SeventhAge_Mail_14", "1. A captain is needed to attack a Royal Tower and only capture-type attacks may be launched against them.") + Environment.NewLine + SK.Text("SeventhAge_Mail_15", "2. Attacking a Royal Tower will remove any interdiction from the source village.") + Environment.NewLine + SK.Text("SeventhAge_Mail_16", "3. Royal Towers cannot be repaired, rebuilt or reinforced by players.") + Environment.NewLine + SK.Text("SeventhAge_Mail_17", "4. Royal Towers cannot be interdicted.") + Environment.NewLine + SK.Text("SeventhAge_Mail_18", "5. If a player captures a Royal Tower their House gains control of that tower.") + Environment.NewLine + SK.Text("SeventhAge_Mail_19", "6. After they are captured Royal Towers are immediately rebuilt and repaired.") + Environment.NewLine + SK.Text("SeventhAge_Mail_20", "7. The Final Age begins with 150 Royal Towers spawning across the map in random locations.") + Environment.NewLine + SK.Text("SeventhAge_Mail_21", "8. After each Glory Round all towers disappear, with new towers appearing in random locations.") + Environment.NewLine + SK.Text("SeventhAge_Mail_22", "9. The total number of towers is reduced by 10 at the end of each Round and control of all towers is reset.") + Environment.NewLine + SK.Text("SeventhAge_Mail_23", "10. The minimum number of Royal Towers in a world is 20.") + Environment.NewLine + Environment.NewLine + SK.Text("SeventhAge_Mail_24", "The final struggle for this realm has begun! The time has come to claim your rightful place amongst the true legends, not just of this world but of all Stronghold Kingdoms worlds. You have come far and achieved much, it is only right that you should now be justly rewarded for your noble efforts.");
          case 'U':
            return SK.Text("WorldSelect_2ndEra_Header", "Welcome to the Second Era!");
          case 'V':
            return SK.Text("WorldSelect_3rdEra_Header", "Welcome to the Third Era!");
          case 'W':
            return SK.Text("WorldSelect_4thEra_Header", "Welcome to the Fourth Era!");
          case 'X':
            return SK.Text("WorldSelect_5thEra_Header", "Welcome to the Fifth Era!");
          case 'Y':
            return SK.Text("WorldSelect_6thEra_Header", "Welcome to the Sixth Era!");
          case 'Z':
            return SK.Text("WorldSelect_FinalEra_Header", "Welcome to the Final Era!");
        }
      }
      return str1;
    }

    public enum MailType
    {
      SENDTO,
      NORMAL,
      REPLY,
      FORWARD,
      PROCLAMATION,
      VIDEO,
    }

    public delegate void GenericUIDelegate();

    public delegate void GetMailThreadUIDelegate(long threadID);

    public class MailThreadHeaderComparer : IComparer<MailThreadListItem>
    {
      public int Compare(MailThreadListItem x, MailThreadListItem y)
      {
        return x == null ? (y == null ? 0 : -1) : (y == null ? 1 : y.mailTime.CompareTo(x.mailTime));
      }
    }

    public class MailThreadComparer : IComparer<MailThreadItem>
    {
      public int Compare(MailThreadItem x, MailThreadItem y)
      {
        return x == null ? (y == null ? 0 : -1) : (y == null ? 1 : y.mailTime.CompareTo(x.mailTime));
      }
    }
  }
}
