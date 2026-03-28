// Decompiled with JetBrains decompiler
// Type: Kingdoms.PresetManager
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using Stronghold.AuthClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Windows.Forms;
using System.Xml;

//#nullable disable
namespace Kingdoms
{
  public class PresetManager
  {
    private static PresetManager instance;
    private string m_presetURL = "";
    public bool ShowPresetImport;
    public bool ImportComplete;
    public bool LocalChangesAvailable;
    public List<CastleMapPreset> m_presets = new List<CastleMapPreset>();
    private Dictionary<PresetType, int> SlotLimits = new Dictionary<PresetType, int>();
    private Dictionary<PresetType, int> SlotCounts = new Dictionary<PresetType, int>();
    private PresetManager.AsyncDelegate m_loadDel;
    private PresetManager.ResponseDelegate m_responseDel;
    private bool isPresetDataLoaded;
    private bool isPresetDataLoading;
    private PresetPanel m_responsePanel;
    private bool isLoaded;
    private CastleMapPreset m_currentMapPreset;
    private PresetType m_currentMapType;

    private PresetManager()
    {
      this.SlotLimits.Add(PresetType.TROOP_ATTACK, 40);
      this.SlotLimits.Add(PresetType.TROOP_DEFEND, 20);
      this.SlotLimits.Add(PresetType.INFRASTRUCTURE, 20);
      this.SlotCounts.Add(PresetType.TROOP_ATTACK, 0);
      this.SlotCounts.Add(PresetType.TROOP_DEFEND, 0);
      this.SlotCounts.Add(PresetType.INFRASTRUCTURE, 0);
    }

    public static PresetManager Instance
    {
      get
      {
        if (PresetManager.instance == null)
          PresetManager.instance = new PresetManager();
        return PresetManager.instance;
      }
    }

    public void SetPresetURL(string url) => this.m_presetURL = url;

    public bool ParseXMLString(string source)
    {
      source = source.Replace("True", "1").Replace("False", "0");
      XmlDocument xmlDocument = new XmlDocument();
      try
      {
        xmlDocument.LoadXml(source);
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        return false;
      }
      this.m_presets.Clear();
      this.ResetSlotCounts();
      foreach (XmlElement childNode in xmlDocument.ChildNodes[0].ChildNodes)
      {
        CastleMapPreset castleMapPreset = new CastleMapPreset();
        castleMapPreset.ParseXML(childNode);
        Dictionary<PresetType, int> slotCounts;
        PresetType type;
        (slotCounts = this.SlotCounts)[type = castleMapPreset.Type] = slotCounts[type] + 1;
        this.m_presets.Add(castleMapPreset);
      }
      return true;
    }

    public string GenerateXMLString()
    {
      XmlDocument doc = new XmlDocument();
      XmlElement element = doc.CreateElement("presets");
      foreach (CastleMapPreset preset in this.m_presets)
        element.AppendChild((XmlNode) preset.GenerateXML(doc));
      return element.OuterXml;
    }

    public bool ParseXMLFile(string filename)
    {
      XmlDocument xmlDocument = new XmlDocument();
      try
      {
        xmlDocument.Load(filename);
      }
      catch (Exception ex)
      {
        return false;
      }
      return this.ParseXMLString(xmlDocument.OuterXml);
    }

    public void GenerateXMLFile(string filename)
    {
      System.IO.File.WriteAllText(filename, this.GenerateXMLString());
    }

    public int GetSlotCount(PresetType type) => this.SlotCounts[type];

    public int GetSlotLimit(PresetType type) => this.SlotLimits[type];

    public int GetHighestAvailableSlot(PresetType type)
    {
      int num = 0;
      foreach (CastleMapPreset preset in this.m_presets)
      {
        if (preset.Type == type && preset.SlotID > num)
          num = preset.SlotID;
      }
      return Math.Min(this.SlotLimits[type], num - num % 5 + 5);
    }

    public int GetFreeSlotCount(PresetType type)
    {
      int num = 0;
      foreach (CastleMapPreset preset in this.m_presets)
      {
        if (preset.Type == type)
          ++num;
      }
      return this.GetSlotLimit(type) - num;
    }

    private void ResetSlotCounts()
    {
      this.SlotCounts[PresetType.TROOP_ATTACK] = 0;
      this.SlotCounts[PresetType.TROOP_DEFEND] = 0;
      this.SlotCounts[PresetType.INFRASTRUCTURE] = 0;
    }

    public CastleMapPreset GetPreset(string name)
    {
      foreach (CastleMapPreset preset in this.m_presets)
      {
        if (name.Equals(preset.Name))
          return preset;
      }
      return (CastleMapPreset) null;
    }

    public CastleMapPreset GetPreset(PresetType type, int slot)
    {
      foreach (CastleMapPreset preset in this.m_presets)
      {
        if (type == preset.Type && slot == preset.SlotID)
          return preset;
      }
      return (CastleMapPreset) null;
    }

    public List<CastleMapPreset> GetPresets(PresetType type)
    {
      List<CastleMapPreset> presets = new List<CastleMapPreset>();
      foreach (CastleMapPreset preset in this.m_presets)
      {
        if (preset.Type == type)
          presets.Add(preset);
      }
      return presets;
    }

    public PresetResult AddPreset(CastleMapPreset newPreset)
    {
      foreach (CastleMapPreset preset in this.m_presets)
      {
        int type1 = (int) preset.Type;
        int type2 = (int) newPreset.Type;
      }
      this.m_presets.Add(newPreset);
      Dictionary<PresetType, int> slotCounts;
      PresetType type;
      (slotCounts = this.SlotCounts)[type = newPreset.Type] = slotCounts[type] + 1;
      return PresetResult.OK;
    }

    public PresetResult UpdatePreset(CastleMapPreset newPreset)
    {
      if (newPreset.SlotID < 1)
      {
        if (this.GetFreeSlotCount(newPreset.Type) < 1)
          return PresetResult.NO_SLOT_AVAILABLE;
        for (int slot = 1; slot <= this.GetSlotLimit(newPreset.Type); ++slot)
        {
          if (this.GetPreset(newPreset.Type, slot) == null)
          {
            newPreset.SlotID = slot;
            break;
          }
        }
        this.LocalChangesAvailable = true;
        return this.AddPreset(newPreset);
      }
      List<CastleMapPreset.CastleElementInfo> castleElementInfoList = new List<CastleMapPreset.CastleElementInfo>((IEnumerable<CastleMapPreset.CastleElementInfo>) newPreset.BasicData);
      foreach (CastleMapPreset preset in this.m_presets)
      {
        if (preset.SlotID == newPreset.SlotID && preset.Type == newPreset.Type)
        {
          preset.Name = newPreset.Name;
          preset.ModifiedDate = newPreset.ModifiedDate;
          preset.ElementCount = newPreset.ElementCount;
          preset.Data = newPreset.Data;
          preset.BasicData.Clear();
          preset.BasicData = castleElementInfoList;
          this.LocalChangesAvailable = true;
          return PresetResult.OK;
        }
      }
      return this.AddPreset(newPreset);
    }

    public PresetResult DeletePreset(PresetType type, int slot)
    {
      bool flag = false;
      CastleMapPreset castleMapPreset = (CastleMapPreset) null;
      foreach (CastleMapPreset preset in this.m_presets)
      {
        if (preset.Type == type && preset.SlotID == slot)
        {
          castleMapPreset = preset;
          break;
        }
      }
      if (castleMapPreset != null)
      {
        this.m_presets.Remove(castleMapPreset);
        flag = true;
        this.LocalChangesAvailable = true;
      }
      return !flag ? PresetResult.PRESET_NOT_FOUND : PresetResult.OK;
    }

    public void SavePresetsToFile()
    {
      this.GenerateXMLFile(GameEngine.getSettingsPath(true) + "\\Presets.xml");
    }

    public void LoadPresetsFromFile()
    {
      if (this.isLoaded)
        return;
      this.ParseXMLFile(GameEngine.getSettingsPath(true) + "\\Presets.xml");
      this.isLoaded = true;
    }

    public void SendPresetsToServer(PresetPanel panel)
    {
      this.m_responsePanel = panel;
      XmlRpcPresetProvider forEndpoint = XmlRpcPresetProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressPresets, URLs.ProfileServerPort, URLs.ProfilePresetsPath);
      XmlRpcPresetRequest req = new XmlRpcPresetRequest();
      req.SessionID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", "");
      req.UserGUID = RemoteServices.Instance.UserGuid.ToString().Replace("-", "");
      req.FileData = this.GenerateXMLString();
      this.m_responseDel = new PresetManager.ResponseDelegate(panel.onUploadComplete);
      forEndpoint.UploadFileData((IPresetRequest) req, new PresetEndResponseDelegate(this.SendPresetsToServerCallback), (Control) null);
    }

    private void SendPresetsToServerCallback(IPresetProvider provider, IPresetResponse response)
    {
      int? successCode1 = response.SuccessCode;
      if ((successCode1.GetValueOrDefault() != 1 ? 0 : (successCode1.HasValue ? 1 : 0)) != 0 && this.isPresetDataLoaded)
      {
        this.LocalChangesAvailable = false;
        this.ShowPresetImport = false;
      }
      if (this.m_responsePanel == null)
        return;
      PresetPanel responsePanel = this.m_responsePanel;
      PresetManager.ResponseDelegate responseDel = this.m_responseDel;
      object[] objArray1 = new object[1];
      object[] objArray2 = objArray1;
      int? successCode2 = response.SuccessCode;
      bool local = Convert.ToBoolean(successCode2.GetValueOrDefault() != 1 ? 0 : (successCode2.HasValue ? 1 : 0));
      objArray2[0] = (object) local;
      object[] objArray3 = objArray1;
      responsePanel.Invoke((Delegate) responseDel, objArray3);
    }

    public bool IsDataReady => this.isPresetDataLoaded && !this.isPresetDataLoading;

    public bool IsDownloading => this.isPresetDataLoading;

    public void LoadPresetsFromServer(PresetPanel panel)
    {
      if (this.isPresetDataLoaded || this.isPresetDataLoading)
        return;
      this.isPresetDataLoading = true;
      try
      {
        PresetManager.RequestState state = new PresetManager.RequestState();
        RequestCachePolicy requestCachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
        WebRequest webRequest = WebRequest.Create(this.m_presetURL);
        webRequest.CachePolicy = requestCachePolicy;
        state.req = webRequest;
        state.asyncCallback = new PresetManager.AsyncDelegate(this.onLoadComplete);
        webRequest.BeginGetResponse(new AsyncCallback(this.LoadPresetsFromServerCallback), (object) state);
        this.m_responsePanel = panel;
        this.m_responseDel = new PresetManager.ResponseDelegate(panel.onServerResponse);
      }
      catch (Exception ex)
      {
        this.onLoadComplete();
      }
    }

    private void LoadPresetsFromServerCallback(IAsyncResult ar)
    {
      PresetManager.AsyncDelegate asyncDelegate = (PresetManager.AsyncDelegate) null;
      try
      {
        PresetManager.RequestState asyncState = (PresetManager.RequestState) ar.AsyncState;
        WebRequest req = asyncState.req;
        asyncDelegate = asyncState.asyncCallback;
        Stream responseStream = req.EndGetResponse(ar).GetResponseStream();
        using (MemoryStream memoryStream = new MemoryStream())
        {
          byte[] buffer = new byte[4096];
          int count;
          do
          {
            count = responseStream.Read(buffer, 0, buffer.Length);
            memoryStream.Write(buffer, 0, count);
          }
          while (count != 0);
          if (this.ParseXMLString(Encoding.ASCII.GetString(memoryStream.ToArray())))
            this.isPresetDataLoaded = true;
        }
      }
      catch (Exception ex)
      {
        int num = (int) MyMessageBox.Show(ex.Message);
      }
      if (asyncDelegate == null)
        return;
      asyncDelegate();
    }

    private void onLoadComplete()
    {
      this.isPresetDataLoading = false;
      if (this.m_responsePanel == null)
        return;
      this.m_responsePanel.Invoke((Delegate) this.m_responseDel, (object) this.isPresetDataLoaded);
    }

    public void LogOut()
    {
      this.m_presets.Clear();
      this.ImportComplete = false;
      this.isPresetDataLoaded = false;
      this.isPresetDataLoading = false;
      this.LocalChangesAvailable = false;
    }

    public bool CopyCurrentToPreset(CastleMapPreset preset)
    {
      if (this.m_currentMapPreset == null || this.m_currentMapType != preset.Type)
        return false;
      preset.CopyData(this.m_currentMapPreset);
      return true;
    }

    public int CurrentElementCount()
    {
      return this.m_currentMapPreset == null ? 0 : this.m_currentMapPreset.ElementCount;
    }

    public void DeployToMap(CastleMapPreset preset)
    {
      this.m_currentMapType = preset.Type;
      switch (this.m_currentMapType)
      {
        case PresetType.TROOP_ATTACK:
          GameEngine.Instance.CastleAttackerSetup.restoreAttackPreset(preset);
          break;
        case PresetType.TROOP_DEFEND:
          GameEngine.Instance.Castle.restoreTroopsPreset(preset);
          break;
        case PresetType.INFRASTRUCTURE:
          GameEngine.Instance.Castle.restoreInfrastructurePreset(preset);
          break;
      }
      this.GenerateFromMap(this.m_currentMapType);
    }

    public void GenerateFromMap(PresetType type)
    {
      this.m_currentMapType = type;
      switch (this.m_currentMapType)
      {
        case PresetType.TROOP_ATTACK:
          this.m_currentMapPreset = GameEngine.Instance.CastleAttackerSetup.generateAttackPreset("current");
          break;
        case PresetType.TROOP_DEFEND:
          this.m_currentMapPreset = GameEngine.Instance.Castle.generateTroopsPreset("current");
          break;
        case PresetType.INFRASTRUCTURE:
          this.m_currentMapPreset = GameEngine.Instance.Castle.generateInfrastructurePreset("current");
          break;
      }
    }

    public int getLegacyCount(PresetType type)
    {
      char[] chArray = new char[1]{ '_' };
      int legacyCount = 0;
      foreach (string file in Directory.GetFiles(GameEngine.getSettingsPath(true), "*.cas"))
      {
        string[] strArray = Path.GetFileName(file.Remove(file.LastIndexOf('.'))).Split(chArray);
        if (strArray.Length >= 2 && !(strArray[0].ToLowerInvariant() != "attacksetup"))
          ++legacyCount;
      }
      return legacyCount;
    }

    public bool transferLegacy(PresetType type)
    {
      if (!Program.mySettings.AttackSetupsUpdated)
      {
        GameEngine.Instance.CastleAttackerSetup.cleanUpAttackSaveNames();
        Program.mySettings.AttackSetupsUpdated = true;
      }
      char[] chArray = new char[1]{ '_' };
      string[] files = Directory.GetFiles(GameEngine.getSettingsPath(true), "*.cas");
      List<string> stringList = new List<string>();
      string str = "";
      switch (type)
      {
        case PresetType.TROOP_ATTACK:
          str = "AttackSetup";
          break;
        case PresetType.TROOP_DEFEND:
          str = "CasTroop";
          break;
        case PresetType.INFRASTRUCTURE:
          str = "CasInfra";
          break;
      }
      foreach (string path in files)
      {
        string fileName = Path.GetFileName(path);
        string[] strArray = fileName.Split(chArray);
        if (strArray.Length >= 2 && !(strArray[0].ToLowerInvariant() != str.ToLowerInvariant()))
          stringList.Add(fileName);
      }
      if (stringList.Count > this.GetFreeSlotCount(type))
        return false;
      List<CastleMapPreset> castleMapPresetList = new List<CastleMapPreset>();
      foreach (string filename in stringList)
      {
        string displayName = filename.Remove(filename.LastIndexOf('.')).Replace(str + "_", "");
        CastleMapPreset preset = this.convertLegacyToPreset(filename, displayName, type);
        if (preset == null)
          return false;
        castleMapPresetList.Add(preset);
      }
      foreach (CastleMapPreset newPreset in castleMapPresetList)
      {
        int num = (int) this.UpdatePreset(newPreset);
      }
      return true;
    }

    public bool deleteLegacy(PresetType type) => true;

    private CastleMapPreset convertLegacyToPreset(
      string filename,
      string displayName,
      PresetType type)
    {
      if (type != PresetType.TROOP_ATTACK)
        return (CastleMapPreset) null;
      CastleMapPreset preset = new CastleMapPreset(displayName, DateTime.Now, type, 0);
      try
      {
        BinaryReader binaryReader = new BinaryReader((Stream) new FileStream(GameEngine.getSettingsPath(true) + "\\" + filename, FileMode.Open));
        preset.ElementCount = binaryReader.ReadInt32();
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < preset.ElementCount; ++index)
        {
          CastleMapPreset.CastleElementInfo castleElementInfo = new CastleMapPreset.CastleElementInfo();
          castleElementInfo.xPos = binaryReader.ReadByte();
          castleElementInfo.yPos = binaryReader.ReadByte();
          castleElementInfo.elementType = binaryReader.ReadByte();
          castleElementInfo.reinforcement = false;
          preset.BasicData.Add(castleElementInfo);
          stringBuilder.Append(castleElementInfo.xPos.ToString() + " ");
          stringBuilder.Append(castleElementInfo.yPos.ToString() + " ");
          stringBuilder.Append(castleElementInfo.elementType.ToString() + " ");
          if (castleElementInfo.elementType == (byte) 94)
          {
            stringBuilder.Append(binaryReader.ReadByte().ToString() + " ");
            stringBuilder.Append(binaryReader.ReadByte().ToString() + " ");
          }
          if (castleElementInfo.elementType >= (byte) 100 && castleElementInfo.elementType < (byte) 109)
          {
            stringBuilder.Append(binaryReader.ReadByte().ToString() + " ");
            if (castleElementInfo.elementType == (byte) 102 || castleElementInfo.elementType == (byte) 103)
            {
              stringBuilder.Append(binaryReader.ReadByte().ToString() + " ");
              stringBuilder.Append(binaryReader.ReadByte().ToString() + " ");
            }
          }
        }
        preset.Data = stringBuilder.ToString();
      }
      catch (Exception ex)
      {
        return (CastleMapPreset) null;
      }
      return preset;
    }

    private delegate void AsyncDelegate();

    public delegate void ResponseDelegate(bool success);

    private class RequestState
    {
      public WebRequest req;
      public PresetManager.AsyncDelegate asyncCallback;
    }
  }
}
