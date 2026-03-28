// Decompiled with JetBrains decompiler
// Type: Kingdoms.ContestCachedData
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Stronghold.AuthClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  internal class ContestCachedData
  {
    public int ID = -1;
    public int visibleTier = 3;
    private int? m_endTime = new int?(0);
    private int? m_startTime = new int?(0);
    public string name = string.Empty;
    public string description = string.Empty;
    public int userPosition;
    public int userRankBand = 1;
    public double userScore;
    public DateTime lastUpdate = DateTime.MinValue;
    public List<ContestPrizeDefinition> prizes = new List<ContestPrizeDefinition>();
    private ContestEntry[][] leaderboardData = new ContestEntry[3][];
    private int[] m_maxIndex = new int[3];
    private int[] m_topIndexPosition = new int[3]{ 1, 1, 1 };
    private int m_visibleLineCount = 10;
    public ContestCachedData.ContestCacheCallbackDelegate leaderboardCallback;
    public ContestCachedData.ContestCacheCallbackDelegate metaDataCallback;
    public ContestCachedData.ContestCacheCallbackDelegate userDataCallback;

    public ContestEntry[] activeLeaderboard
    {
      get => this.leaderboardData[this.visibleTier - 1];
      set => this.leaderboardData[this.visibleTier - 1] = value;
    }

    public int activeMaxIndex
    {
      get => this.m_maxIndex[this.visibleTier - 1];
      set => this.m_maxIndex[this.visibleTier - 1] = value;
    }

    public int activeTopIndex
    {
      get => this.m_topIndexPosition[this.visibleTier - 1];
      set => this.m_topIndexPosition[this.visibleTier - 1] = value;
    }

    public int visibleLineCount
    {
      set
      {
        if (value != this.m_visibleLineCount && this.activeLeaderboard != null && this.leaderboardCallback != null)
          this.leaderboardCallback(true);
        this.m_visibleLineCount = value;
      }
    }

    public void RetrieveLeaderboard()
    {
      RemoteServices.Instance.set_GetContestDataRange_UserCallBack(new RemoteServices.GetContestDataRange_UserCallBack(this.RetrieveLeaderboardCallback));
      RemoteServices.Instance.GetContestDataRange(this.ID, this.activeTopIndex, 30, this.visibleTier);
    }

    private void RetrieveLeaderboardCallback(GetContestDataRange_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.activeLeaderboard = returnData.contestInfo;
        this.activeMaxIndex = returnData.maxIndex;
      }
      if (this.leaderboardCallback == null)
        return;
      this.leaderboardCallback(returnData.Success);
    }

    public void RetrieveMetaData()
    {
      XmlRpcContestProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath).RetrieveContestMetaData((IContestRequest) new XmlRpcContestRequest()
      {
        SessionID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""),
        UserGUID = RemoteServices.Instance.UserGuid.ToString().Replace("-", ""),
        EventID = new int?(this.ID)
      }, new ContestEndResponseDelegate(this.RetrieveMetaDataCallback), (Control) null);
    }

    private void RetrieveMetaDataCallback(IContestProvider provider, IContestResponse response)
    {
      int? successCode1 = response.SuccessCode;
      if ((successCode1.GetValueOrDefault() != 1 ? 0 : (successCode1.HasValue ? 1 : 0)) != 0)
      {
        this.name = response.ContestName;
        this.description = response.ContestDescription;
        this.prizes = ((XmlRpcContestResponse) response).Prizes;
        this.m_startTime = response.StartTime;
        this.m_endTime = response.EndTime;
      }
      if (this.metaDataCallback == null)
        return;
      ContestCachedData.ContestCacheCallbackDelegate metaDataCallback = this.metaDataCallback;
      int? successCode2 = response.SuccessCode;
      int num = successCode2.GetValueOrDefault() != 1 ? 0 : (successCode2.HasValue ? 1 : 0);
      metaDataCallback(num != 0);
    }

    public void RetrieveUserData()
    {
      RemoteServices.Instance.set_GetUserContestData_UserCallBack(new RemoteServices.GetUserContestData_UserCallBack(this.RetrieveUserDataCallback));
      RemoteServices.Instance.GetUserContestData(this.ID);
    }

    private void RetrieveUserDataCallback(GetUserContestData_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      bool flag = returnData.Success && returnData.userInfo != null && returnData.userInfo.RankBand > 0;
      this.lastUpdate = returnData.lastUpdate;
      if (flag)
      {
        this.userPosition = returnData.userInfo.Placement;
        this.userRankBand = returnData.userInfo.RankBand;
        this.userScore = returnData.userInfo.Score;
      }
      else
      {
        this.userPosition = 0;
        this.userRankBand = 0;
        this.userScore = 0.0;
      }
      if (this.userDataCallback == null)
        return;
      this.userDataCallback(flag && this.userRankBand > 0);
    }

    public void SetAsVisible()
    {
      if (this.activeLeaderboard == null)
        this.RetrieveLeaderboard();
      else if (this.leaderboardCallback != null)
        this.leaderboardCallback(true);
      if (string.IsNullOrEmpty(this.name))
        this.RetrieveMetaData();
      else if (this.metaDataCallback != null)
        this.metaDataCallback(true);
      if (this.userPosition == 0 && this.userRankBand > 0)
      {
        this.RetrieveUserData();
      }
      else
      {
        if (this.userDataCallback == null)
          return;
        this.userDataCallback(true);
      }
    }

    public void NextTier()
    {
      if (this.visibleTier >= 3)
        return;
      ++this.visibleTier;
      if (this.activeLeaderboard == null)
      {
        this.RetrieveLeaderboard();
        this.RetrieveMetaData();
      }
      else
      {
        if (this.leaderboardCallback != null)
          this.leaderboardCallback(true);
        if (this.metaDataCallback == null)
          return;
        this.metaDataCallback(true);
      }
    }

    public void PrevTier()
    {
      if (this.visibleTier <= 1)
        return;
      --this.visibleTier;
      if (this.activeLeaderboard == null)
      {
        this.RetrieveLeaderboard();
        this.RetrieveMetaData();
      }
      else
      {
        if (this.leaderboardCallback != null)
          this.leaderboardCallback(true);
        if (this.metaDataCallback == null)
          return;
        this.metaDataCallback(true);
      }
    }

    public void ScrollUp()
    {
      if (this.activeTopIndex < 2)
        return;
      this.activeTopIndex = Math.Max(1, this.activeTopIndex - this.m_visibleLineCount);
      this.RetrieveLeaderboard();
    }

    public void ScrollDown()
    {
      if (this.activeTopIndex > this.activeMaxIndex - this.m_visibleLineCount)
        return;
      this.activeTopIndex = Math.Min(this.activeMaxIndex - this.m_visibleLineCount, this.activeTopIndex + this.m_visibleLineCount);
      this.RetrieveLeaderboard();
    }

    public void ScrollToTop()
    {
      if (this.activeTopIndex < 2)
        return;
      this.activeTopIndex = 1;
      this.RetrieveLeaderboard();
    }

    public void ScrollToBottom()
    {
      if (this.activeTopIndex > this.activeMaxIndex - this.m_visibleLineCount)
        return;
      this.activeTopIndex = this.activeMaxIndex - this.m_visibleLineCount;
      this.RetrieveLeaderboard();
    }

    public delegate void ContestCacheCallbackDelegate(bool success);
  }
}
