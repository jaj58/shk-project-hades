// Decompiled with JetBrains decompiler
// Type: Kingdoms.QuestsHelper
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;

//#nullable disable
namespace Kingdoms
{
  internal class QuestsHelper
  {
    public static bool questReadyToHandIn;

    public static int GetTarget(int questID)
    {
      int target = 0;
      if (questID >= 0)
      {
        NewQuests.NewQuestDefinition newQuestDef = NewQuests.getNewQuestDef(questID);
        switch (questID)
        {
          case 29:
            target = 4;
            break;
          case 66:
            target = 6;
            break;
          case 146:
            target = 8;
            break;
          default:
            target = newQuestDef.parameter;
            break;
        }
      }
      return target;
    }

    public static int GetProgress(NewQuestsData questData)
    {
      int progress = 0;
      if (questData != null && questData.questID >= 0)
      {
        NewQuests.NewQuestDefinition newQuestDef = NewQuests.getNewQuestDef(questData.questID);
        if (questData.completionState >= 0 && QuestsHelper.isQuestComplete(questData))
          progress = QuestsHelper.GetTarget(questData.questID);
        else if (newQuestDef.parameter > 0 || questData.questID == 66 || questData.questID == 146 || questData.questID == 29)
        {
          switch (questData.questID)
          {
            case 5:
            case 20:
            case 42:
            case 67:
            case 99:
            case 125:
            case 140:
            case 148:
            case 157:
            case 167:
              progress = (int) (GameEngine.Instance.World.getCurrentGold() - (double) questData.startingData);
              break;
            case 29:
            case 66:
            case 146:
              progress = QuestsHelper.bitCount(questData.data);
              break;
            default:
              progress = questData.data;
              break;
          }
        }
      }
      return progress;
    }

    private static bool shouldAutoCompleteOnMobile(int QuestID)
    {
      switch (QuestID)
      {
        case 1:
        case 4:
        case 16:
        case 34:
        case 48:
        case 64:
        case 84:
        case 101:
        case 122:
          return true;
        default:
          return false;
      }
    }

    public static bool IsInviteFriendQuest(int questID)
    {
      switch (questID)
      {
        case 4:
        case 16:
        case 34:
        case 48:
        case 64:
        case 84:
        case 101:
        case 122:
          return true;
        default:
          return false;
      }
    }

    public static int bitCount(int n)
    {
      int num = 0;
      for (; n != 0; n &= n - 1)
        ++num;
      return num;
    }

    public static bool isQuestComplete(NewQuestsData questData)
    {
      if (questData.completionState > 0 && questData.questID >= 0)
        return true;
      NewQuests.NewQuestDefinition newQuestDef = NewQuests.getNewQuestDef(questData.questID);
      if (newQuestDef == null)
        return false;
      switch (questData.questID)
      {
        case 5:
        case 20:
        case 42:
        case 67:
        case 99:
        case 125:
        case 140:
        case 148:
        case 157:
        case 167:
          if (GameEngine.Instance.World.getCurrentGold() - (double) questData.startingData >= (double) newQuestDef.parameter)
            return true;
          break;
        case 29:
          if (questData.data == 15)
            return true;
          break;
        case 66:
          if (questData.data == 63)
            return true;
          break;
        case 146:
          if (questData.data == (int) byte.MaxValue)
            return true;
          break;
        default:
          if (newQuestDef.parameter == 0)
          {
            if (questData.data > 0)
              return true;
            break;
          }
          if (questData.data >= newQuestDef.parameter)
            return true;
          break;
      }
      return false;
    }

    public static string GetObjectiveString(NewQuests.NewQuestDefinition def)
    {
      switch (def.ID)
      {
        case 4:
        case 16:
        case 34:
        case 48:
        case 64:
        case 84:
        case 101:
        case 122:
          return SK.Text("QUESTS_Spread_New_description", "Learn about Invite a Friend");
        default:
          return SK.NoStoreText("Z_QUEST_DESCRIPTIONS_" + def.tagString);
      }
    }
  }
}
