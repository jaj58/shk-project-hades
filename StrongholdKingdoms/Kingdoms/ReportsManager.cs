// Decompiled with JetBrains decompiler
// Type: Kingdoms.ReportsManager
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

//#nullable disable
namespace Kingdoms
{
  public class ReportsManager
  {
    private static ReportsManager m_instance;
    private SparseArray m_storedReportHeaders = new SparseArray();
    private SparseArray m_storedReportBodies = new SparseArray();
    private SparseArray m_storedReportData = new SparseArray();
    private int m_readFilterTypeDownloaded = 3;
    public ReportsManager.ReportsComparer reportsMainComparer = new ReportsManager.ReportsComparer();
    private ReportFilterList filters = new ReportFilterList();
    private bool showReadMessages = true;
    private bool showParishAttacks = true;
    private bool showVillageLost = true;
    private bool showForwardedMessagesOnly;
    public bool HasNewReports;
    private bool initialLoad;
    private long[] m_moveArray;
    private string[] folderNamesList;
    private long[] folderIDList;
    private RemoteServices.GetReportsList_UserCallBack m_getReportListUICallback;
    public RecipientList ForwardTo = new RecipientList();

    public static ReportsManager instance
    {
      get
      {
        if (ReportsManager.m_instance == null)
          ReportsManager.m_instance = new ReportsManager();
        return ReportsManager.m_instance;
      }
    }

    public SparseArray storedReportHeaders
    {
      set => this.m_storedReportHeaders = value;
      get => this.m_storedReportHeaders;
    }

    public SparseArray storedReportBodies
    {
      set => this.m_storedReportBodies = value;
      get => this.m_storedReportBodies;
    }

    public SparseArray storedReportData
    {
      set => this.m_storedReportData = value;
      get => this.m_storedReportData;
    }

    public int readFilterTypeDownloaded
    {
      set => this.m_readFilterTypeDownloaded = value;
      get => this.m_readFilterTypeDownloaded;
    }

    public ReportFilterList Filters => this.filters;

    public bool ShowReadMessages
    {
      get => this.showReadMessages;
      set => this.showReadMessages = value;
    }

    public bool ShowParishAttacks
    {
      get => this.showParishAttacks;
      set => this.showParishAttacks = value;
    }

    public bool ShowVillageLost
    {
      get => this.showVillageLost;
      set => this.showVillageLost = value;
    }

    public bool ShowForwardedMessagesOnly
    {
      get => this.showForwardedMessagesOnly;
      set => this.showForwardedMessagesOnly = value;
    }

    public void deleteReport(long reportToDelete)
    {
      RemoteServices.Instance.DeleteReports(new long[1]
      {
        reportToDelete
      });
      foreach (ReportListItem storedReportHeader in this.m_storedReportHeaders)
      {
        if (Math.Abs(storedReportHeader.reportID) == reportToDelete)
        {
          this.m_storedReportHeaders[Math.Abs(storedReportHeader.reportID)] = (object) null;
          break;
        }
      }
    }

    public object getReportData(long reportID) => this.m_storedReportData[reportID];

    public void setReportData(object reportData, long reportID)
    {
      this.m_storedReportData[reportID] = reportData;
    }

    public void setReportAlreadyRead(long reportID)
    {
      UniversalDebugLog.Log("set read " + (object) reportID + " / " + (object) this.m_storedReportBodies.Count);
      ((GetReport_ReturnType) this.m_storedReportBodies[reportID]).wasAlreadyRead = true;
    }

    public long findHighestReportID()
    {
      long highestReportId = -1;
      foreach (ReportListItem storedReportHeader in this.m_storedReportHeaders)
      {
        if (Math.Abs(storedReportHeader.reportID) > highestReportId)
          highestReportId = Math.Abs(storedReportHeader.reportID);
      }
      return highestReportId;
    }

    public List<ReportListItem> getReportEntries(DateTime serverTime)
    {
      List<ReportListItem> reportEntries = new List<ReportListItem>();
      foreach (ReportListItem storedReportHeader in this.storedReportHeaders)
      {
        TimeSpan timeSpan = serverTime - storedReportHeader.reportTime;
        if ((this.showReadMessages || !storedReportHeader.readStatus) && (!this.showForwardedMessagesOnly || storedReportHeader.reportAboutUser != null && storedReportHeader.reportAboutUser.Length != 0) && !this.isReportTypeFiltered(storedReportHeader))
        {
          ReportListItem reportListItem = storedReportHeader;
          reportEntries.Add(reportListItem);
        }
      }
      return reportEntries;
    }

    private bool isReportTypeFiltered(ReportListItem item)
    {
      bool flag = false;
      switch (item.reportType)
      {
        case 1:
        case 2:
        case 24:
        case 25:
        case 58:
        case 59:
        case 60:
        case 61:
        case 123:
        case 124:
        case 125:
        case 132:
          if (GameEngine.Instance.World.isCapital(item.sourceVillage))
          {
            if (!this.ShowParishAttacks)
            {
              flag = true;
              break;
            }
            break;
          }
          if (!this.filters.attacks)
          {
            flag = true;
            break;
          }
          break;
        case 3:
        case 4:
        case 62:
        case 63:
        case 64:
        case 65:
        case 79:
        case 86:
        case 87:
        case 88:
        case 89:
        case 90:
          if (!this.filters.defense)
          {
            flag = true;
            break;
          }
          break;
        case 5:
        case 6:
        case 7:
        case 8:
        case 9:
        case 10:
          flag = true;
          break;
        case 13:
        case 14:
        case 15:
        case 16:
        case 46:
        case 47:
        case 48:
        case 49:
          if (!this.filters.vassals)
          {
            flag = true;
            break;
          }
          break;
        case 17:
        case 18:
        case 19:
          if (!this.filters.reinforcements)
          {
            flag = true;
            break;
          }
          break;
        case 20:
          if (!this.filters.research)
          {
            flag = true;
            break;
          }
          break;
        case 21:
        case 22:
        case 26:
        case 27:
        case 54:
        case 55:
        case 56:
        case 57:
        case 121:
        case 122:
        case 126:
        case 133:
          if (!this.filters.scouting)
          {
            flag = true;
            break;
          }
          break;
        case 23:
          if (!this.filters.foraging)
          {
            flag = true;
            break;
          }
          break;
        case 28:
        case 51:
        case 52:
        case 53:
        case 74:
        case 75:
          if (!this.filters.elections)
          {
            flag = true;
            break;
          }
          break;
        case 29:
        case 30:
        case 31:
        case 32:
        case 33:
        case 34:
        case 35:
        case 36:
        case 37:
        case 38:
        case 39:
        case 40:
          flag = true;
          break;
        case 50:
        case 107:
        case 108:
        case 109:
        case 110:
        case 111:
        case 112:
        case 115:
        case 116:
        case 117:
        case 118:
          if (!this.filters.factions)
          {
            flag = true;
            break;
          }
          break;
        case 66:
        case 67:
        case 68:
        case 69:
        case 70:
        case 71:
        case 72:
        case 91:
        case 103:
        case 104:
        case 105:
        case 106:
          if (!this.filters.religion)
          {
            flag = true;
            break;
          }
          break;
        case 73:
        case 78:
          if (!this.filters.trade)
          {
            flag = true;
            break;
          }
          break;
        case 76:
        case 77:
        case 99:
          if (!this.filters.cards)
          {
            flag = true;
            break;
          }
          break;
        case 80:
        case 81:
        case 82:
        case 83:
        case 84:
        case 85:
          if (!this.filters.enemyWarnings)
          {
            flag = true;
            break;
          }
          break;
        case 92:
          if (!this.filters.achievements)
          {
            flag = true;
            break;
          }
          break;
        case 93:
        case 94:
        case 95:
        case 96:
          if (!this.filters.buyVillages)
          {
            flag = true;
            break;
          }
          break;
        case 100:
        case 101:
          if (!this.filters.quests)
          {
            flag = true;
            break;
          }
          break;
        case 102:
        case 129:
        case 130:
        case 131:
        case 136:
        case 140:
        case 141:
          if (!this.filters.spins)
          {
            flag = true;
            break;
          }
          break;
        case 113:
        case 114:
        case 120:
        case 134:
        case 135:
          if (!this.filters.house)
          {
            flag = true;
            break;
          }
          break;
        case (short) sbyte.MaxValue:
        case 128:
          if (!this.ShowVillageLost)
          {
            flag = true;
            break;
          }
          break;
      }
      return flag;
    }

    public void loadReports()
    {
    }

    public void saveReports()
    {
      int userId = RemoteServices.Instance.UserID;
      string settingsPath = GameEngine.getSettingsPath(true);
      try
      {
        new FileInfo(settingsPath + "\\ReportData-" + userId.ToString() + "-" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + ".dat").IsReadOnly = false;
      }
      catch (Exception ex)
      {
      }
      FileStream output = (FileStream) null;
      BinaryWriter w = (BinaryWriter) null;
      try
      {
        output = new FileStream(settingsPath + "\\ReportData-" + userId.ToString() + "-" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + ".dat", FileMode.Create);
        w = new BinaryWriter((Stream) output);
        int num = -1;
        w.Write(num);
        int count = this.m_storedReportHeaders.Count;
        w.Write(count);
        foreach (ReportListItem storedReportHeader in this.m_storedReportHeaders)
        {
          long index = Math.Abs(storedReportHeader.reportID);
          w.Write(storedReportHeader.reportID);
          w.Write(storedReportHeader.otherUser);
          w.Write(storedReportHeader.reportAboutUser);
          w.Write(storedReportHeader.folderID);
          w.Write(storedReportHeader.sourceVillage);
          w.Write(storedReportHeader.targetVillage);
          w.Write(storedReportHeader.reportType);
          w.Write(storedReportHeader.readStatus);
          w.Write(storedReportHeader.successStatus);
          long ticks1 = storedReportHeader.reportTime.Ticks;
          w.Write(ticks1);
          if (this.m_storedReportBodies[index] == null)
          {
            w.Write(false);
          }
          else
          {
            w.Write(true);
            GetReport_ReturnType storedReportBody = (GetReport_ReturnType) this.m_storedReportBodies[index];
            w.Write(storedReportBody.otherUser);
            w.Write(storedReportBody.reportAboutUser);
            w.Write(storedReportBody.reportAboutUserID);
            long ticks2 = storedReportBody.reportTime.Ticks;
            w.Write(ticks2);
            w.Write(storedReportBody.reportType);
            w.Write(storedReportBody.successStatus);
            w.Write(storedReportBody.snapshotAvailable);
            w.Write(storedReportBody.wasAlreadyRead);
            w.Write(storedReportBody.genericData1);
            w.Write(storedReportBody.attackingVillage);
            w.Write(storedReportBody.defendingVillage);
            w.Write(storedReportBody.genericData2);
            w.Write(storedReportBody.genericData3);
            w.Write(storedReportBody.genericData4);
            w.Write(storedReportBody.genericData5);
            w.Write(storedReportBody.genericData6);
            w.Write(storedReportBody.genericData7);
            w.Write(storedReportBody.genericData8);
            w.Write(storedReportBody.genericData9);
            w.Write(storedReportBody.genericData10);
            w.Write(storedReportBody.genericData11);
            w.Write(storedReportBody.genericData12);
            w.Write(storedReportBody.genericData13);
            w.Write(storedReportBody.genericData14);
            w.Write(storedReportBody.genericData15);
            w.Write(storedReportBody.genericData16);
            w.Write(storedReportBody.genericData17);
            w.Write(storedReportBody.genericData18);
            w.Write(storedReportBody.genericData19);
            w.Write(storedReportBody.genericData20);
            w.Write(storedReportBody.genericData21);
            w.Write(storedReportBody.genericData22);
            w.Write(storedReportBody.genericData23);
            w.Write(storedReportBody.genericData24);
            w.Write(storedReportBody.genericData25);
            w.Write(storedReportBody.genericData26);
            w.Write(storedReportBody.genericData27);
            w.Write(storedReportBody.genericData28);
            w.Write(storedReportBody.genericData29);
            w.Write(storedReportBody.genericData30);
            w.Write(storedReportBody.genericData31);
            w.Write(storedReportBody.genericData32);
            w.Write(storedReportBody.genericData33);
            w.Write(storedReportBody.genericData34);
            w.Write(storedReportBody.genericData35);
          }
          if (this.m_storedReportData[index] == null)
          {
            w.Write(false);
          }
          else
          {
            w.Write(true);
            ViewBattle_ReturnType battleReturnType = (ViewBattle_ReturnType) this.m_storedReportData[index];
            if (battleReturnType.castleMapSnapshot == null)
            {
              w.Write(0);
            }
            else
            {
              w.Write(battleReturnType.castleMapSnapshot.Length);
              w.Write(battleReturnType.castleMapSnapshot, 0, battleReturnType.castleMapSnapshot.Length);
            }
            if (battleReturnType.damageMapSnapshot == null)
            {
              w.Write(0);
            }
            else
            {
              w.Write(battleReturnType.damageMapSnapshot.Length);
              w.Write(battleReturnType.damageMapSnapshot, 0, battleReturnType.damageMapSnapshot.Length);
            }
            if (battleReturnType.castleTroopsSnapshot == null)
            {
              w.Write(0);
            }
            else
            {
              w.Write(battleReturnType.castleTroopsSnapshot.Length);
              w.Write(battleReturnType.castleTroopsSnapshot, 0, battleReturnType.castleTroopsSnapshot.Length);
            }
            if (battleReturnType.attackMapSnapshot == null)
            {
              w.Write(0);
            }
            else
            {
              w.Write(battleReturnType.attackMapSnapshot.Length);
              w.Write(battleReturnType.attackMapSnapshot, 0, battleReturnType.attackMapSnapshot.Length);
            }
            w.Write(battleReturnType.keepLevel);
            w.Write(battleReturnType.landType);
            if (battleReturnType.defenderResearchData == null)
            {
              w.Write(false);
            }
            else
            {
              w.Write(true);
              battleReturnType.defenderResearchData.Write(w);
            }
            if (battleReturnType.attackerResearchData == null)
            {
              w.Write(false);
            }
            else
            {
              w.Write(true);
              battleReturnType.attackerResearchData.Write(w);
            }
          }
        }
        w.Close();
        output.Close();
      }
      catch (Exception ex1)
      {
        try
        {
          w?.Close();
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
        int num = (int) MyMessageBox.Show("A problem occurred saving 'ReportData.data'\n\n" + ex1.ToString(), "Data Save Error");
      }
    }

    public void ClearAllReports()
    {
      this.filters = new ReportFilterList();
      this.showReadMessages = true;
      this.showParishAttacks = true;
      this.ShowForwardedMessagesOnly = false;
      if (!this.initialLoad)
        this.saveReports();
      this.initialLoad = true;
      if (this.storedReportHeaders != null)
        this.storedReportHeaders.Clear();
      if (this.storedReportBodies != null)
        this.storedReportBodies.Clear();
      if (this.storedReportData == null)
        return;
      this.storedReportData.Clear();
    }

    public void filterAll()
    {
      this.Filters.attacks = false;
      this.Filters.defense = false;
      this.Filters.enemyWarnings = false;
      this.Filters.reinforcements = false;
      this.Filters.scouting = false;
      this.Filters.foraging = false;
      this.Filters.trade = false;
      this.Filters.vassals = false;
      this.Filters.religion = false;
      this.Filters.research = false;
      this.Filters.elections = false;
      this.Filters.factions = false;
      this.Filters.cards = false;
      this.Filters.quests = false;
      this.Filters.spins = false;
      this.Filters.achievements = false;
      this.ShowParishAttacks = false;
      this.ShowVillageLost = false;
    }

    public bool areFiltersClear()
    {
      return this.filters.attacks && this.filters.defense && this.filters.enemyWarnings && this.filters.reinforcements && this.filters.scouting && this.filters.foraging && this.filters.trade && this.filters.vassals && this.filters.religion && this.filters.research && this.filters.elections && this.filters.factions && this.filters.cards && this.filters.achievements && this.filters.buyVillages && this.filters.quests && this.filters.spins && this.showReadMessages && this.showParishAttacks && !this.showForwardedMessagesOnly;
    }

    public static string getReportTitle(ReportListItem item)
    {
      int reportType = (int) item.reportType;
      string str1 = "";
      if (item.otherUser != null)
        str1 = item.otherUser;
      if (item.reportAboutUser != null && item.reportAboutUser.Length > 0)
      {
        string reportAboutUser = item.reportAboutUser;
      }
      else
      {
        string userName = RemoteServices.Instance.UserName;
      }
      int targetVillage = item.targetVillage;
      int sourceVillage = item.sourceVillage;
      string reportTitle = "";
      switch (reportType)
      {
        case 1:
          if (GameEngine.Instance.World.isRegionCapital(sourceVillage) && GameEngine.Instance.World.isRegionCapital(targetVillage))
          {
            reportTitle = reportTitle + SK.Text("ReportsPanel_Attack_parish_parish", "Your Parish Attacks Parish : ") + GameEngine.Instance.World.getVillageName(targetVillage);
            break;
          }
          if (str1.Length == 0)
          {
            string str2 = !GameEngine.Instance.World.isRegionCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountyCapital(sourceVillage) ? (!GameEngine.Instance.World.isProvinceCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountryCapital(sourceVillage) ? reportTitle + SK.Text("ReportsPanel_Attack_Player", "Your Troops Attack Player : ") : reportTitle + SK.Text("ReportsPanel_Attack_Player_country", "Your Country Attacks Player : ")) : reportTitle + SK.Text("ReportsPanel_Attack_Player_province", "Your Province Attacks Player : ")) : reportTitle + SK.Text("ReportsPanel_Attack_Player_county", "Your County Attacks Player : ")) : reportTitle + SK.Text("ReportsPanel_Attack_Player_parish", "Your Parish Attacks Player : ");
            reportTitle = targetVillage < 0 ? str2 + SK.Text("ReportsPanel_An_Empty_Village", "An empty village") : str2 + GameEngine.Instance.World.getVillageName(targetVillage);
            break;
          }
          reportTitle = reportTitle + SK.Text("ReportsPanel_Attack_Village", "Your Troops Attack Village : ") + str1;
          break;
        case 2:
          if (str1.Length == 0)
          {
            string str3 = reportTitle + SK.Text("ReportsPanel_Parish_Attack_Player", "Your Parish Attacks Player : ");
            reportTitle = targetVillage < 0 ? str3 + SK.Text("ReportsPanel_An_Empty_Village", "An empty village") : str3 + GameEngine.Instance.World.getVillageName(targetVillage);
            break;
          }
          reportTitle = reportTitle + SK.Text("ReportsPanel_Parish_Attack_Village", "Your Parish Attacks Village : ") + str1;
          break;
        case 3:
          string str4 = !GameEngine.Instance.World.isRegionCapital(targetVillage) ? (!GameEngine.Instance.World.isCountyCapital(targetVillage) ? (!GameEngine.Instance.World.isProvinceCapital(targetVillage) ? (!GameEngine.Instance.World.isCountryCapital(targetVillage) ? reportTitle + SK.Text("ReportsPanel_Player_Attacks", "Player attacks your castle : ") : reportTitle + SK.Text("ReportsPanel_Player_Attacks_country", "Player attacks your Country : ")) : reportTitle + SK.Text("ReportsPanel_Player_Attacks_province", "Player attacks your Province : ")) : reportTitle + SK.Text("ReportsPanel_Player_Attacks_county", "Player attacks your County : ")) : reportTitle + SK.Text("ReportsPanel_Player_Attacks_parish", "Player attacks your Parish : ");
          reportTitle = str1.Length != 0 ? str4 + str1 : str4 + SK.Text("ReportsPanel_Unknown_Player", "An Unknown Player");
          break;
        case 4:
          string str5 = reportTitle + SK.Text("ReportsPanel_Parish_Attacks", "Parish attacks your castle : ");
          reportTitle = str1.Length != 0 ? str5 + str1 : str5 + SK.Text("ReportsPanel_Unknown_Player", "An Unknown Player");
          break;
        case 15:
          reportTitle = reportTitle + SK.Text("ReportsPanel_Lost_Vassal", "You have lost a vassal : ") + str1;
          break;
        case 16:
          reportTitle += SK.Text("ReportsPanel_No_Liege_Lord", "You no longer have a liege lord");
          break;
        case 17:
          reportTitle = reportTitle + SK.Text("ReportsPanel_Reinforcements_Arrived", "Reinforcements have arrived from : ") + str1;
          break;
        case 18:
          if (str1 == "")
            str1 = SK.Text("ReportsPanel_An_Empty_Village", "An empty village");
          reportTitle = reportTitle + SK.Text("ReportsPanel_Reinforcements_Returned", "Reinforcements have returned from : ") + str1;
          break;
        case 19:
          reportTitle = reportTitle + SK.Text("ReportsPanel_Reinforcements_Retrieved", "Reinforcements have been retrieved by : ") + str1;
          break;
        case 20:
          reportTitle += SK.Text("ReportsPanel_Research_Complete", "Research Task Complete");
          break;
        case 21:
          if (str1.Length == 0)
          {
            string str6 = reportTitle + SK.Text("ReportsPanel_Scouted_Out", "Your Scouts Scout-Out Village : ");
            reportTitle = targetVillage < 0 ? str6 + SK.Text("ReportsPanel_An_Empty_Village", "An empty village") : str6 + GameEngine.Instance.World.getVillageName(targetVillage);
            break;
          }
          reportTitle = reportTitle + SK.Text("ReportsPanel_Scouts", "Your Scouts Scout-Out Player : ") + str1;
          break;
        case 22:
          string str7 = reportTitle + SK.Text("ReportsPanel_Scouted", "Player scouts your castle : ");
          reportTitle = str1.Length != 0 ? str7 + str1 : str7 + SK.Text("ReportsPanel_Unknown_Player", "An Unknown Player");
          break;
        case 23:
          reportTitle += SK.Text("ReportsPanel_Stash_Foraged", "Stash Foraged");
          break;
        case 24:
          reportTitle = !GameEngine.Instance.World.isRegionCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountyCapital(sourceVillage) ? (!GameEngine.Instance.World.isProvinceCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountryCapital(sourceVillage) ? reportTitle + SK.Text("ReportsPanel_Attack_Bandit", "Your Troops Attack a Bandit Camp") : reportTitle + SK.Text("ReportsPanel_Attack_Bandit_country", "Your Country Attacks a Bandit Camp")) : reportTitle + SK.Text("ReportsPanel_Attack_Bandit_province", "Your Province Attacks a Bandit Camp")) : reportTitle + SK.Text("ReportsPanel_Attack_Bandit_county", "Your County Attacks a Bandit Camp")) : reportTitle + SK.Text("ReportsPanel_Attack_Bandit_parish", "Your Parish Attacks a Bandit Camp");
          break;
        case 25:
          reportTitle = !GameEngine.Instance.World.isRegionCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountyCapital(sourceVillage) ? (!GameEngine.Instance.World.isProvinceCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountryCapital(sourceVillage) ? reportTitle + SK.Text("ReportsPanel_Attack_Wolf_Lair", "Your Troops Attack a Wolf Lair") : reportTitle + SK.Text("ReportsPanel_Attack_Wolf_Lair_country", "Your Country Attacks a Wolf Lair")) : reportTitle + SK.Text("ReportsPanel_Attack_Wolf_Lair_province", "Your Province Attacks a Wolf Lair")) : reportTitle + SK.Text("ReportsPanel_Attack_Wolf_Lair_county", "Your County Attacks a Wolf Lair")) : reportTitle + SK.Text("ReportsPanel_Attack_Wolf_Lair_parish", "Your Parish Attacks a Wolf Lair");
          break;
        case 26:
          reportTitle += SK.Text("ReportsPanel_Bandit_Camp_Scouted", "Bandit Camp Scouted");
          break;
        case 27:
          reportTitle += SK.Text("ReportsPanel_Wolf_Lair_Scouted", "Wolf Lair Scouted");
          break;
        case 28:
          reportTitle = str1.Length <= 0 ? reportTitle + SK.Text("ReportsPanel_No_Parish_Winner", "No Winner of Parish Election") : reportTitle + SK.Text("ReportsPanel_New_Steward", "New Steward in Parish : ") + str1;
          break;
        case 29:
          reportTitle = str1.Length <= 0 ? reportTitle + "Spy Report - Command 'Player 1'" : reportTitle + "Spy Report - Command 'Player 1' of " + str1;
          break;
        case 30:
          reportTitle = str1.Length <= 0 ? reportTitle + "Spy Report - Command 'Village 1'" : reportTitle + "Spy Report - Command 'Village 1' of " + str1;
          break;
        case 31:
          reportTitle = str1.Length <= 0 ? reportTitle + "Spy Report - Command 'Castle 1'" : reportTitle + "Spy Report - Command 'Castle 1' of " + str1;
          break;
        case 32:
          reportTitle = str1.Length <= 0 ? reportTitle + "Spy Report - Command 'Army 1'" : reportTitle + "Spy Report - Command 'Army 1' of " + str1;
          break;
        case 33:
          reportTitle = str1.Length <= 0 ? reportTitle + "Spy Report - Command 'Village 2'" : reportTitle + "Spy Report - Command 'Village 2' of " + str1;
          break;
        case 34:
          reportTitle = str1.Length <= 0 ? reportTitle + "Spy Report - Command 'Castle 2'" : reportTitle + "Spy Report - Command 'Castle 2' of " + str1;
          break;
        case 35:
          reportTitle = str1.Length <= 0 ? reportTitle + "Spy Report - Command 'Player 2'" : reportTitle + "Spy Report - Command 'Player 2' of " + str1;
          break;
        case 36:
          reportTitle = str1.Length <= 0 ? reportTitle + "Spy Report - Command 'Castle 3'" : reportTitle + "Spy Report - Command 'Castle 3' of " + str1;
          break;
        case 37:
          reportTitle = str1.Length <= 0 ? reportTitle + "Spy Report - Command 'Army 2'" : reportTitle + "Spy Report - Command 'Army 2' of " + str1;
          break;
        case 38:
          reportTitle = str1.Length <= 0 ? reportTitle + "Spy Report - Command 'Castle 4'" : reportTitle + "Spy Report - Command 'Castle 4' of " + str1;
          break;
        case 39:
          reportTitle += "Spy Report - Command Failed ";
          break;
        case 40:
          reportTitle += "Spy Report - No Spies Found ";
          break;
        case 46:
          reportTitle = reportTitle + SK.Text("ReportsPanel_Liege_Lord_Offer_FRom", "Offer to be your liege lord from : ") + str1;
          break;
        case 47:
          reportTitle = reportTitle + SK.Text("ReportsPanel_Vassal_Request_Accepted", "Vassal Request accepted by : ") + str1;
          break;
        case 48:
          reportTitle = reportTitle + SK.Text("ReportsPanel_Vassal_request_Declined", "Vassal Request declined by : ") + str1;
          break;
        case 49:
          reportTitle = reportTitle + SK.Text("ReportsPanel_Liege_Lord_Offer_Withdrawn", "Liege lord offer withdrawn by : ") + str1;
          break;
        case 50:
          reportTitle += SK.Text("ReportsPanel_Faction_Invite", "Faction Invite");
          break;
        case 53:
          reportTitle = str1.Length <= 0 ? reportTitle + SK.Text("ReportsPanel_No_County_Winner", "No Winner of County Election") : reportTitle + SK.Text("ReportsPanel_New_Sheriff", "New Sheriff in County : ") + str1;
          break;
        case 54:
          reportTitle += SK.Text("ReportsPanel_Rat_Castle_Scouted", "Rat's Castle Scouted");
          break;
        case 55:
          reportTitle += SK.Text("ReportsPanel_Snake_Castle_Scouted", "Snake's Castle Scouted");
          break;
        case 56:
          reportTitle += SK.Text("ReportsPanel_Pig_Castle_Scouted", "Pig's Castle Scouted");
          break;
        case 57:
          reportTitle += SK.Text("ReportsPanel_Wolf_Castle_Scouted", "Wolf's Castle Scouted");
          break;
        case 58:
          reportTitle = !GameEngine.Instance.World.isRegionCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountyCapital(sourceVillage) ? (!GameEngine.Instance.World.isProvinceCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountryCapital(sourceVillage) ? reportTitle + SK.Text("ReportsPanel_Attack_Rat", "Your Troops Attack the Rat") : reportTitle + SK.Text("ReportsPanel_Attack_Rat_country", "Your Country Attacks the Rat")) : reportTitle + SK.Text("ReportsPanel_Attack_Rat_province", "Your Province Attacks the Rat")) : reportTitle + SK.Text("ReportsPanel_Attack_Rat_county", "Your County Attacks the Rat")) : reportTitle + SK.Text("ReportsPanel_Attack_Rat_parish", "Your Parish Attacks the Rat");
          break;
        case 59:
          reportTitle = !GameEngine.Instance.World.isRegionCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountyCapital(sourceVillage) ? (!GameEngine.Instance.World.isProvinceCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountryCapital(sourceVillage) ? reportTitle + SK.Text("ReportsPanel_Attack_Snake", "Your Troops Attack the Snake") : reportTitle + SK.Text("ReportsPanel_Attack_Snake_country", "Your Country Attacks the Snake")) : reportTitle + SK.Text("ReportsPanel_Attack_Snake_province", "Your Province Attacks the Snake")) : reportTitle + SK.Text("ReportsPanel_Attack_Snake_county", "Your County Attacks the Snake")) : reportTitle + SK.Text("ReportsPanel_Attack_Snake_parish", "Your Parish Attacks the Snake");
          break;
        case 60:
          reportTitle = !GameEngine.Instance.World.isRegionCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountyCapital(sourceVillage) ? (!GameEngine.Instance.World.isProvinceCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountryCapital(sourceVillage) ? reportTitle + SK.Text("ReportsPanel_Attack_Pig", "Your Troops Attack the Pig") : reportTitle + SK.Text("ReportsPanel_Attack_Pig_country", "Your Country Attacks the Pig")) : reportTitle + SK.Text("ReportsPanel_Attack_Pig_province", "Your Province Attacks the Pig")) : reportTitle + SK.Text("ReportsPanel_Attack_Pig_county", "Your County Attacks the Pig")) : reportTitle + SK.Text("ReportsPanel_Attack_Pig_parish", "Your Parish Attacks the Pig");
          break;
        case 61:
          reportTitle = !GameEngine.Instance.World.isRegionCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountyCapital(sourceVillage) ? (!GameEngine.Instance.World.isProvinceCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountryCapital(sourceVillage) ? reportTitle + SK.Text("ReportsPanel_Attack_Wolf", "Your Troops Attack the Wolf") : reportTitle + SK.Text("ReportsPanel_Attack_Wolf_country", "Your Country Attacks the Wolf")) : reportTitle + SK.Text("ReportsPanel_Attack_Wolf_province", "Your Province Attacks the Wolf")) : reportTitle + SK.Text("ReportsPanel_Attack_Wolf_county", "Your County Attacks the Wolf")) : reportTitle + SK.Text("ReportsPanel_Attack_Wolf_parish", "Your Parish Attacks the Wolf");
          break;
        case 62:
          reportTitle += SK.Text("ReportsPanel_Rat_Attacks", "The Rat Attacks");
          break;
        case 63:
          reportTitle += SK.Text("ReportsPanel_Snake_Attacks", "The Snake Attacks");
          break;
        case 64:
          reportTitle += SK.Text("ReportsPanel_Pig_Attacks", "The Pig Attacks");
          break;
        case 65:
          reportTitle += SK.Text("ReportsPanel_Wolf_Attacks", "The Wolf Attacks");
          break;
        case 66:
          reportTitle += SK.Text("ReportsPanel_Monk_Influence", "Monk Influence");
          break;
        case 67:
          reportTitle += SK.Text("ReportsPanel_Monk_Restoration", "Monk Restoration");
          break;
        case 68:
        case 105:
          reportTitle += SK.Text("ReportsPanel_Monk_Interdiction", "Monk Interdiction");
          break;
        case 69:
        case 104:
          reportTitle += SK.Text("ReportsPanel_Monk_Inquisition", "Monk Inquisition");
          break;
        case 70:
        case 91:
          reportTitle += SK.Text("ReportsPanel_Monk_Excommunication", "Monk Excommunication");
          break;
        case 71:
        case 103:
          reportTitle += SK.Text("ReportsPanel_Monk_Absolution", "Monk Absolution");
          break;
        case 72:
          reportTitle += SK.Text("ReportsPanel_Monk_Blessing", "Monk Blessing");
          break;
        case 73:
          reportTitle = reportTitle + SK.Text("ReportsPanel_Goods_Sent_From", "Goods Sent From : ") + str1;
          break;
        case 74:
          reportTitle = str1.Length <= 0 ? reportTitle + SK.Text("ReportsPanel_No_Province_Winner", "No Winner of Province Election") : reportTitle + SK.Text("ReportsPanel_New_Governor", "New Governor in Province : ") + str1;
          break;
        case 75:
          reportTitle = str1.Length <= 0 ? reportTitle + SK.Text("ReportsPanel_No_Country_Winner", "No Winner of Country Election") : reportTitle + SK.Text("ReportsPanel_New_Monarch", "New Monarch in Country : ") + str1;
          break;
        case 76:
          reportTitle += SK.Text("ReportsPanel_Card_Expires", "Card Expires");
          break;
        case 77:
          reportTitle += SK.Text("ReportsPanel_Instant_Card_Played", "Instant Card Played");
          break;
        case 78:
          reportTitle += SK.Text("ReportsPanel_Auto_Trade_Sent", "Auto Trade Sent");
          break;
        case 79:
          reportTitle += SK.Text("ReportsPanel_Enemy_Attacks", "The Enemy Attacks");
          break;
        case 80:
          reportTitle += SK.Text("ReportsPanel_Enemy_Arrive", "The enemy arrives in our parish!");
          break;
        case 81:
          reportTitle += SK.Text("ReportsPanel_Enemy_First", "Enemy probes castle defences");
          break;
        case 82:
          reportTitle += SK.Text("ReportsPanel_Enemy_Normal", "Enemy launches attack");
          break;
        case 83:
          reportTitle += SK.Text("ReportsPanel_Enemy_Prefinal", "Enemy troops advancing in large numbers");
          break;
        case 84:
          reportTitle += SK.Text("ReportsPanel_Enemy_Final", "Enemy launches final attack");
          break;
        case 85:
          reportTitle += SK.Text("ReportsPanel_Enemy_Leave", "The enemy is vanquished!");
          break;
        case 86:
          reportTitle += SK.Text("ReportsPanel_Diplomacy", "The enemy attack was stopped by Diplomacy");
          break;
        case 87:
          reportTitle += SK.Text("ReportsPanel_Rat_Diplomacy", "The Rat's attack was stopped by Diplomacy");
          break;
        case 88:
          reportTitle += SK.Text("ReportsPanel_Snake_Diplomacy", "The Snake's attack was stopped by Diplomacy");
          break;
        case 89:
          reportTitle += SK.Text("ReportsPanel_Pig_Diplomacy", "The Pig's attack was stopped by Diplomacy");
          break;
        case 90:
          reportTitle += SK.Text("ReportsPanel_Wolf_Diplomacy", "The Wolf's attack was stopped by Diplomacy");
          break;
        case 92:
          reportTitle += SK.Text("ReportsPanel_Achievement_Attained", "Achievement Attained");
          break;
        case 93:
          reportTitle += SK.Text("ReportsPanel_Buy_Village_Success", "Village Charter Purchased");
          break;
        case 94:
        case 95:
        case 96:
          reportTitle += SK.Text("ReportsPanel_Buy_Village_Failed", "Village Charter Purchase Failed");
          break;
        case 99:
          reportTitle += SK.Text("ReportsPanel_Card_Used", "Card Used and Expired");
          break;
        case 100:
          reportTitle += SK.Text("ReportsPanel_QuestCompleted", "Quest Completed");
          break;
        case 101:
          reportTitle += SK.Text("ReportsPanel_Quest Failed", "Quest Failed");
          break;
        case 102:
          reportTitle += SK.Text("Reports_Spins", "Wheel Spin Prize");
          break;
        case 106:
          reportTitle += SK.Text("ReportsPanel_Monk_Ended", "Monk Interdiction Ended");
          break;
        case 107:
          reportTitle = reportTitle + SK.Text("ReportsPanel_Faction_Member_Join", "New Faction Member") + " : " + str1;
          break;
        case 108:
          reportTitle = !(str1 == "") ? reportTitle + SK.Text("ReportsPanel_Faction_Member_Leave", "Faction Member Leaves") + " : " + str1 : reportTitle + SK.Text("ReportsPanel_Faction_Member_Leave_Self", "You are no longer a member of a faction");
          break;
        case 109:
          reportTitle = !(str1 == "") ? reportTitle + SK.Text("ReportsPanel_Faction_Member_Dismissed", "Faction Member Dismissed") + " : " + str1 : reportTitle + SK.Text("ReportsPanel_Faction_Member_Dismissed_Self", "You have been dismissed from your faction");
          break;
        case 110:
          reportTitle += SK.Text("ReportsPanel_Faction_Promotion", "Faction Promotion");
          break;
        case 111:
          reportTitle += SK.Text("ReportsPanel_Faction_Demotion", "Faction Demotion");
          break;
        case 112:
          reportTitle += SK.Text("ReportsPanel_Faction_Relationship_Change", "Faction Relationship Change");
          break;
        case 113:
          reportTitle += SK.Text("ReportsPanel_Faction_House_Change", "House Membership Change");
          break;
        case 114:
          reportTitle += SK.Text("ReportsPanel_House_Relationship_Change", "House Relationship Change");
          break;
        case 115:
          reportTitle = reportTitle + SK.Text("ReportsPanel_Faction_Application", "A Player has applied to your Faction") + " : " + str1;
          break;
        case 116:
          reportTitle += SK.Text("ReportsPanel_Faction_Application_accepted", "Your faction application has been accepted");
          break;
        case 117:
          reportTitle += SK.Text("ReportsPanel_Faction_Application_rejected", "Your faction application has been rejected");
          break;
        case 118:
          reportTitle += SK.Text("ReportsPanel_Faction_Member_Dismissed_Self", "You have been dismissed from your faction");
          break;
        case 120:
          reportTitle += SK.Text("ReportsPanel_Faction_Glory_Obtained", "Your house has been awarded glory points");
          break;
        case 121:
          reportTitle += SK.Text("ReportsPanel_Paladin_Castle_Scouted", "Paladin's Castle Scouted");
          break;
        case 122:
          reportTitle += SK.Text("ReportsPanel_Paladin_Castle_Scouted", "Paladin's Castle Scouted");
          break;
        case 123:
          reportTitle = !GameEngine.Instance.World.isRegionCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountyCapital(sourceVillage) ? (!GameEngine.Instance.World.isProvinceCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountryCapital(sourceVillage) ? reportTitle + SK.Text("ReportsPanel_Attack_Paladin", "Your Troops Attack the Paladin's Castle") : reportTitle + SK.Text("ReportsPanel_Attack_Paladin_country", "Your Country Attacks the Paladin's Castle")) : reportTitle + SK.Text("ReportsPanel_Attack_Paladin_province", "Your Province Attacks the Paladin's Castle")) : reportTitle + SK.Text("ReportsPanel_Attack_Paladin_county", "Your County Attacks the Paladin's Castle")) : reportTitle + SK.Text("ReportsPanel_Attack_Paladin_parish", "Your Parish Attacks the Paladin's Castle");
          break;
        case 124:
          reportTitle = !GameEngine.Instance.World.isRegionCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountyCapital(sourceVillage) ? (!GameEngine.Instance.World.isProvinceCapital(sourceVillage) ? (!GameEngine.Instance.World.isCountryCapital(sourceVillage) ? reportTitle + SK.Text("ReportsPanel_Attack_Paladin", "Your Troops Attack the Paladin's Castle") : reportTitle + SK.Text("ReportsPanel_Attack_Paladin_country", "Your Country Attacks the Paladin's Castle")) : reportTitle + SK.Text("ReportsPanel_Attack_Paladin_province", "Your Province Attacks the Paladin's Castle")) : reportTitle + SK.Text("ReportsPanel_Attack_Paladin_county", "Your County Attacks the Paladin's Castle")) : reportTitle + SK.Text("ReportsPanel_Attack_Paladin_parish", "Your Parish Attacks the Paladin's Castle");
          break;
        case 125:
          reportTitle += SK.Text("ReportsPanel_Attack_Treasure_Castle", "Your Troops Attack a Treasure Castle");
          break;
        case 126:
          reportTitle += SK.Text("ReportsPanel_Treasure_Castle_Scouted", "Treasure Castle Scouted");
          break;
        case (int) sbyte.MaxValue:
        case 128:
          reportTitle += SK.Text("Reports_VillageLost", "Village Lost");
          break;
        case 129:
          reportTitle += SK.Text("Reports_AI_Spins", "Wheel Spin Bonus from AI Razing");
          break;
        case 130:
          reportTitle += SK.Text("Reports_Forage_Spins", "Wheel Spin Bonus from Foraging");
          break;
        case 131:
          reportTitle += SK.Text("Reports_AI_Spins_capture", "Wheel Spin Bonus from AI Capture");
          break;
        case 132:
          reportTitle += SK.Text("ReportsPanel_Attack_Royal_twer", "Your Troops Attack a Royal Tower");
          break;
        case 133:
          reportTitle += SK.Text("ReportsPanel_Royal_Tower_Scouted", "Royal Tower Scouted");
          break;
        case 134:
          reportTitle += SK.Text("ReportsPanel_Royal_Tower_Gained", "Your house has captured a Royal Tower");
          break;
        case 135:
          reportTitle += SK.Text("ReportsPanel_Royal_Tower_Lost", "Royal Tower Lost!");
          break;
        case 136:
          reportTitle += SK.Text("Reports_Heretic_Spins", "Wheel Spin Bonus from Player Razing");
          break;
        case 140:
          reportTitle += SK.Text("Reports_Contest_Awarded", "Prize Won");
          break;
        case 141:
          reportTitle += SK.Text("Reports_Contest_Claimed", "Prize Claimed");
          break;
      }
      return reportTitle;
    }

    public void moveReports(string destFolderName)
    {
      long reportFolderId = this.getReportFolderID(destFolderName);
      if (this.m_moveArray == null || this.m_moveArray.Length <= 0)
        return;
      RemoteServices.Instance.MoveReports(this.m_moveArray, reportFolderId);
      foreach (long move in this.m_moveArray)
      {
        if (this.storedReportHeaders[move] != null)
        {
          ReportListItem storedReportHeader = (ReportListItem) this.storedReportHeaders[move];
          storedReportHeader.folderID = reportFolderId;
          this.storedReportHeaders[move] = (object) storedReportHeader;
        }
      }
    }

    public void deleteReportFolder(string folderName, int mode)
    {
      long reportFolderId = this.getReportFolderID(folderName);
      if (reportFolderId < 0L)
        return;
      RemoteServices.Instance.deleteReportFolder(reportFolderId, mode);
      if (mode != 3)
        return;
      foreach (ReportListItem storedReportHeader in this.storedReportHeaders)
      {
        if (storedReportHeader.folderID == reportFolderId)
          storedReportHeader.folderID = -1L;
      }
    }

    public long getReportFolderID() => -1;

    public long getReportFolderID(string folderName)
    {
      if (this.folderNamesList != null)
      {
        int index = 0;
        foreach (string folderNames in this.folderNamesList)
        {
          if (folderNames == folderName)
            return this.folderIDList[index];
          ++index;
        }
      }
      return -1;
    }

    public void manageReportFoldersCallback(ManageReportFolders_ReturnType returnData)
    {
    }

    public void reportListCallback(GetReportsList_ReturnType returnData)
    {
      if (returnData.Success)
      {
        int count = returnData.reports.Count;
        for (int index = 0; index < count; ++index)
        {
          if (this.storedReportHeaders[Math.Abs(returnData.reports[index].reportID)] == null)
            this.storedReportHeaders[Math.Abs(returnData.reports[index].reportID)] = (object) returnData.reports[index];
        }
      }
      this.m_getReportListUICallback(returnData);
    }

    public void init(
      RemoteServices.GetReportsList_UserCallBack getReportsListUICallback)
    {
      this.m_getReportListUICallback = getReportsListUICallback;
      RemoteServices.Instance.set_GetReportsList_UserCallBack(new RemoteServices.GetReportsList_UserCallBack(this.reportListCallback));
    }

    public class ReportsComparer : IComparer<ReportListItem>
    {
      public int Compare(ReportListItem x, ReportListItem y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null || Math.Abs(x.reportID) < Math.Abs(y.reportID))
          return 1;
        return Math.Abs(x.reportID) > Math.Abs(y.reportID) ? -1 : 0;
      }
    }
  }
}
