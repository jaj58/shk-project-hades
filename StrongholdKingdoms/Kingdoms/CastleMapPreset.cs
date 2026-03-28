// Decompiled with JetBrains decompiler
// Type: Kingdoms.CastleMapPreset
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.Xml;

//#nullable disable
namespace Kingdoms
{
  public class CastleMapPreset
  {
    public string Name = "";
    public DateTime ModifiedDate = DateTime.Now;
    public PresetType Type;
    public int ElementCount;
    public int SlotID;
    public string Data;
    public List<CastleMapPreset.CastleElementInfo> BasicData = new List<CastleMapPreset.CastleElementInfo>();

    public CastleMapPreset()
    {
    }

    public CastleMapPreset(string name, DateTime time, PresetType type, int count)
    {
      this.Name = name;
      this.ModifiedDate = time;
      this.Type = type;
      this.ElementCount = count;
    }

    public XmlElement GenerateXML(XmlDocument doc)
    {
      if (this.Type == PresetType.NONE)
        return (XmlElement) null;
      string name = "";
      switch (this.Type)
      {
        case PresetType.TROOP_ATTACK:
          name = "attack";
          break;
        case PresetType.TROOP_DEFEND:
          name = "troops";
          break;
        case PresetType.INFRASTRUCTURE:
          name = "infrastructure";
          break;
      }
      XmlElement element = doc.CreateElement(name);
      XmlAttribute attribute1 = doc.CreateAttribute("name");
      if (this.Name.Trim().Length == 0 && this.ElementCount > 0)
        this.Name = "(" + this.SlotID.ToString() + ")";
      attribute1.Value = this.Name;
      element.Attributes.Append(attribute1);
      XmlAttribute attribute2 = doc.CreateAttribute("last_modified");
      attribute2.Value = this.ModifiedDate.Ticks.ToString();
      element.Attributes.Append(attribute2);
      XmlAttribute attribute3 = doc.CreateAttribute("count");
      attribute3.Value = this.ElementCount.ToString();
      element.Attributes.Append(attribute3);
      XmlAttribute attribute4 = doc.CreateAttribute("slot");
      attribute4.Value = this.SlotID.ToString();
      element.Attributes.Append(attribute4);
      if (this.Data != null)
        element.InnerText = this.Data;
      return element;
    }

    public void ParseXML(XmlElement element)
    {
      switch (element.Name)
      {
        case "attack":
          this.Type = PresetType.TROOP_ATTACK;
          break;
        case "troops":
          this.Type = PresetType.TROOP_DEFEND;
          break;
        case "infrastructure":
          this.Type = PresetType.INFRASTRUCTURE;
          break;
      }
      this.Name = element.Attributes["name"].Value;
      this.ModifiedDate = new DateTime(Convert.ToInt64(element.Attributes["last_modified"].Value));
      this.ElementCount = Convert.ToInt32(element.Attributes["count"].Value);
      this.SlotID = Convert.ToInt32(element.Attributes["slot"].Value);
      this.Data = element.InnerText;
      CastleMap.PopulateBasicInfo(this);
    }

    public int GetElementTotal(byte elementType) => this.GetElementTotal(elementType, false);

    public int GetElementTotal(byte elementType, bool reinforcement)
    {
      int elementTotal = 0;
      foreach (CastleMapPreset.CastleElementInfo castleElementInfo in this.BasicData)
      {
        if ((int) castleElementInfo.elementType == (int) elementType && reinforcement == castleElementInfo.reinforcement)
          ++elementTotal;
      }
      return elementTotal;
    }

    public int GetRangeTotal(byte minType, byte maxType)
    {
      return this.GetRangeTotal(minType, maxType, false);
    }

    public int GetRangeTotal(byte minType, byte maxType, bool reinforcement)
    {
      int rangeTotal = 0;
      foreach (CastleMapPreset.CastleElementInfo castleElementInfo in this.BasicData)
      {
        if ((int) castleElementInfo.elementType >= (int) minType && (int) castleElementInfo.elementType <= (int) maxType && reinforcement == castleElementInfo.reinforcement)
          ++rangeTotal;
      }
      return rangeTotal;
    }

    public void CopyData(CastleMapPreset otherPreset)
    {
      this.Data = otherPreset.Data;
      this.ElementCount = otherPreset.ElementCount;
      CastleMap.PopulateBasicInfo(this);
    }

    public bool CanDeploy()
    {
      return this.Type != PresetType.INFRASTRUCTURE || this.ResearchRequirementsMet();
    }

    public bool ResearchRequirementsMet()
    {
      if (this.Type != PresetType.INFRASTRUCTURE)
        return true;
      ResearchData forCurrentVillage = GameEngine.Instance.World.GetResearchDataForCurrentVillage();
      int fortificationResearchLevel = this.GetFortificationResearchLevel();
      int defenceResearchLevel = this.GetDefenceResearchLevel();
      return fortificationResearchLevel <= (int) forCurrentVillage.Research_Fortification && defenceResearchLevel <= (int) forCurrentVillage.Research_Defences;
    }

    public bool ResourceRequirementsMet()
    {
      if (this.Type != PresetType.INFRASTRUCTURE)
        return true;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      bool flag = GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.getSelectedMenuVillage());
      CardData cardData = new CardData();
      if (!flag)
      {
        CardData userCardData = GameEngine.Instance.cardsManager.UserCardData;
      }
      foreach (CastleMapPreset.CastleElementInfo castleElementInfo in this.BasicData)
      {
        if (castleElementInfo.elementType <= (byte) 69 && castleElementInfo.elementType != (byte) 43)
        {
          int elementType = (int) castleElementInfo.elementType;
          if (elementType == 65)
            elementType = 34;
          if (elementType == 66)
            elementType = 33;
          int woodCost = 0;
          int stoneCost = 0;
          int ironCost = 0;
          int oilCost = 0;
          int goldCost = 0;
          CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, elementType, ref woodCost, ref stoneCost, ref goldCost, ref oilCost, ref ironCost);
          num1 += woodCost;
          num2 += stoneCost;
          num3 += ironCost;
          num4 += oilCost;
          num5 += goldCost;
        }
      }
      VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
      GameEngine.Instance.Village.getStockpileLevels(levels);
      if (levels.woodLevel < (double) num1 || levels.stoneLevel < (double) num2 || levels.ironLevel < (double) num3 || levels.pitchLevel < (double) num4)
        return false;
      return flag ? (double) num5 <= GameEngine.Instance.Village.m_capitalGold : (double) num5 <= GameEngine.Instance.World.getCurrentGold();
    }

    public bool ParishRequirementsMet()
    {
      if (!this.RequiresParishBuilding())
        return true;
      foreach (CastleMapPreset.CastleElementInfo castleElementInfo in this.BasicData)
      {
        if (castleElementInfo.elementType == (byte) 43)
        {
          if (GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Tunnellors <= (byte) 0)
            return false;
          break;
        }
      }
      int num1 = GameEngine.Instance.Castle.countBombards();
      int num2 = GameEngine.Instance.Castle.countTurrets();
      int num3 = GameEngine.Instance.Castle.countBallistas();
      foreach (CastleMapPreset.CastleElementInfo castleElementInfo in this.BasicData)
      {
        switch (castleElementInfo.elementType)
        {
          case 41:
            ++num2;
            continue;
          case 42:
            ++num3;
            continue;
          case 44:
            ++num1;
            continue;
          default:
            continue;
        }
      }
      return num2 < (int) GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Turrets && num3 < (int) GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Ballista && num1 < (int) GameEngine.Instance.Village.m_parishCapitalResearchData.Research_Leadership;
    }

    public bool RequiresParishBuilding()
    {
      if (this.Type != PresetType.INFRASTRUCTURE)
        return false;
      foreach (CastleMapPreset.CastleElementInfo castleElementInfo in this.BasicData)
      {
        switch (castleElementInfo.elementType)
        {
          case 41:
          case 42:
          case 43:
          case 44:
            return true;
          default:
            continue;
        }
      }
      return false;
    }

    public int GetFortificationResearchLevel()
    {
      if (this.Type != PresetType.INFRASTRUCTURE)
        return 0;
      int val2 = 0;
      foreach (CastleMapPreset.CastleElementInfo castleElementInfo in this.BasicData)
      {
        switch (castleElementInfo.elementType)
        {
          case 11:
            val2 = Math.Max(4, val2);
            continue;
          case 12:
            val2 = Math.Max(5, val2);
            continue;
          case 13:
            val2 = Math.Max(7, val2);
            continue;
          case 14:
            val2 = Math.Max(8, val2);
            continue;
          case 21:
            val2 = Math.Max(2, val2);
            continue;
          case 33:
          case 39:
          case 40:
            val2 = Math.Max(1, val2);
            continue;
          case 34:
            val2 = Math.Max(3, val2);
            continue;
          case 37:
          case 38:
            val2 = Math.Max(6, val2);
            continue;
          default:
            continue;
        }
      }
      return val2;
    }

    public int GetDefenceResearchLevel()
    {
      if (this.Type != PresetType.INFRASTRUCTURE)
        return 0;
      int val2 = 0;
      foreach (CastleMapPreset.CastleElementInfo castleElementInfo in this.BasicData)
      {
        switch (castleElementInfo.elementType)
        {
          case 31:
            val2 = Math.Max(1, val2);
            continue;
          case 32:
          case 75:
            val2 = Math.Max(5, val2);
            continue;
          case 35:
            val2 = Math.Max(7, val2);
            continue;
          case 36:
            val2 = Math.Max(2, val2);
            continue;
          default:
            continue;
        }
      }
      return val2;
    }

    public class CastleElementInfo
    {
      public byte xPos;
      public byte yPos;
      public byte elementType;
      public bool reinforcement;
    }
  }
}
