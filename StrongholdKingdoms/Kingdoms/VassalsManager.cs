// Decompiled with JetBrains decompiler
// Type: Kingdoms.VassalsManager
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;

//#nullable disable
namespace Kingdoms
{
  public class VassalsManager
  {
    private VassalInfo liegeLordInfo = new VassalInfo();
    private VassalInfo[] cachedVassalInfo;
    private VassalRequestInfo[] cachedRequestsSentByYou;
    private VassalRequestInfo[] cachedRequestsSentToYou;
    private VassalsManager.VassalsUpdatedCallback OnUpdate;

    public VassalInfo GetLiegeLord() => this.liegeLordInfo;

    public VassalInfo[] GetVassals() => this.cachedVassalInfo;

    public VassalRequestInfo[] GetRequestsSentByYou() => this.cachedRequestsSentByYou;

    public VassalRequestInfo[] GetRequestsSentToYou() => this.cachedRequestsSentToYou;

    public void LoadVassals(int villageID, VassalsManager.VassalsUpdatedCallback callback)
    {
      this.cachedVassalInfo = (VassalInfo[]) null;
      this.OnUpdate = callback;
      RemoteServices.Instance.set_VassalInfo_UserCallBack(new RemoteServices.VassalInfo_UserCallBack(this.vassalInfoCallBack));
      RemoteServices.Instance.VassalInfo(villageID);
    }

    private void vassalInfoCallBack(VassalInfo_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.importVassals(returnData.liegeLordInfo, returnData.vassals);
      this.importVassalRequests(returnData.requestsYouveMade, returnData.requestsOfYou);
      GameEngine.Instance.World.updateUserVassals();
      if (this.OnUpdate == null)
        return;
      this.OnUpdate();
    }

    public void importVassals(VassalInfo liegeLord, VassalInfo[] vassals)
    {
      this.liegeLordInfo = liegeLord;
      this.cachedVassalInfo = vassals;
    }

    public void importVassalRequests(
      VassalRequestInfo[] requestsYouveSent,
      VassalRequestInfo[] requestsOfYou)
    {
      this.cachedRequestsSentByYou = requestsYouveSent;
      this.cachedRequestsSentToYou = requestsOfYou;
    }

    public void AskSomeoneToBeYourVassal(
      int villageID,
      int targetVillageID,
      VassalsManager.VassalsUpdatedCallback callback)
    {
      if (villageID < 0 || targetVillageID < 0 || GameEngine.Instance.World.WorldEnded)
        return;
      this.OnUpdate = callback;
      RemoteServices.Instance.set_SendVassalRequest_UserCallBack(new RemoteServices.SendVassalRequest_UserCallBack(this.askSomeoneToBeYourVassalCallBack));
      RemoteServices.Instance.SendVassalRequest(villageID, targetVillageID);
    }

    private void askSomeoneToBeYourVassalCallBack(SendVassalRequest_ReturnType returnData)
    {
      if (!returnData.Success || GameEngine.Instance.Village == null)
        return;
      this.importVassalRequests(returnData.requestsYouveMade, returnData.requestsOfYou);
      if (this.OnUpdate == null)
        return;
      this.OnUpdate();
    }

    public void BreakFromYourLiegeLord(
      int theirVillageID,
      int yourVillageID,
      VassalsManager.VassalsUpdatedCallback callback)
    {
      if (theirVillageID < 0 || yourVillageID < 0 || GameEngine.Instance.World.WorldEnded)
        return;
      this.OnUpdate = callback;
      RemoteServices.Instance.set_BreakLiegeLord_UserCallBack(new RemoteServices.BreakLiegeLord_UserCallBack(this.breakFromYourLiegeLordCallBack));
      RemoteServices.Instance.BreakLiegeLord(theirVillageID, yourVillageID);
      GameEngine.Instance.World.breakVassal(theirVillageID, yourVillageID);
    }

    private void breakFromYourLiegeLordCallBack(BreakLiegeLord_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.importVassals(returnData.liegeLordInfo, returnData.vassals);
      GameEngine.Instance.World.updateUserVassals();
      if (this.OnUpdate == null)
        return;
      this.OnUpdate();
    }

    public void BreakFromYourVassal(
      int yourVillageID,
      int theirVillageID,
      VassalsManager.VassalsUpdatedCallback callback)
    {
      if (theirVillageID < 0 || yourVillageID < 0 || GameEngine.Instance.World.WorldEnded)
        return;
      this.OnUpdate = callback;
      RemoteServices.Instance.set_BreakVassalage_UserCallBack(new RemoteServices.BreakVassalage_UserCallBack(this.breakVassalageCallBack));
      RemoteServices.Instance.BreakVassalage(yourVillageID, theirVillageID);
      GameEngine.Instance.World.breakVassal(yourVillageID, theirVillageID);
    }

    private void breakVassalageCallBack(BreakVassalage_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.importVassals(returnData.liegeLordInfo, returnData.vassals);
      GameEngine.Instance.World.updateUserVassals();
      if (this.OnUpdate == null)
        return;
      this.OnUpdate();
    }

    public void AcceptRequest(
      int theirVillageID,
      int yourVillageID,
      VassalsManager.VassalsUpdatedCallback callback)
    {
      if (GameEngine.Instance.World.WorldEnded)
        return;
      this.OnUpdate = callback;
      RemoteServices.Instance.set_HandleVassalRequest_UserCallBack(new RemoteServices.HandleVassalRequest_UserCallBack(this.handleVassalRequestCallBack));
      RemoteServices.Instance.AcceptVassalRequest(theirVillageID, yourVillageID);
    }

    public void DeclineRequest(
      int theirVillageID,
      int yourVillageID,
      VassalsManager.VassalsUpdatedCallback callback)
    {
      if (GameEngine.Instance.World.WorldEnded)
        return;
      this.OnUpdate = callback;
      RemoteServices.Instance.set_HandleVassalRequest_UserCallBack(new RemoteServices.HandleVassalRequest_UserCallBack(this.handleVassalRequestCallBack));
      RemoteServices.Instance.DeclineVassalRequest(theirVillageID, yourVillageID);
    }

    public void CancelRequest(
      int theirVillageID,
      int yourVillageID,
      VassalsManager.VassalsUpdatedCallback callback)
    {
      if (GameEngine.Instance.World.WorldEnded)
        return;
      this.OnUpdate = callback;
      RemoteServices.Instance.set_HandleVassalRequest_UserCallBack(new RemoteServices.HandleVassalRequest_UserCallBack(this.handleVassalRequestCallBack));
      RemoteServices.Instance.CancelVassalRequest(yourVillageID, theirVillageID);
    }

    public void handleVassalRequestCallBack(HandleVassalRequest_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.importVassals(returnData.liegeLordInfo, returnData.vassals);
        this.importVassalRequests(returnData.requestsYouveMade, returnData.requestsOfYou);
        GameEngine.Instance.World.updateUserVassals();
      }
      if (this.OnUpdate == null)
        return;
      this.OnUpdate();
    }

    public void Reset()
    {
      this.liegeLordInfo.villageID = -1;
      this.cachedVassalInfo = (VassalInfo[]) null;
      this.cachedRequestsSentByYou = (VassalRequestInfo[]) null;
      this.cachedRequestsSentToYou = (VassalRequestInfo[]) null;
    }

    public delegate void VassalsUpdatedCallback();
  }
}
