// Decompiled with JetBrains decompiler
// Type: Kingdoms.RemoteServices
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using CustomSinks;
using DXGraphics;
using ServerInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Messaging;

//#nullable disable
namespace Kingdoms
{
  public class RemoteServices
  {
    private static readonly RemoteServices instance = new RemoteServices();
    private IService service;
    private List<RemoteServices.CallBackEntryClass> resultList = new List<RemoteServices.CallBackEntryClass>();
    private ArrayList queuedResultList = new ArrayList();
    private bool inResultsProcessing;
    private bool chatActive;
    private int userID = -1;
    private int sessionID;
    private string username = "";
    private string realname = "";
    private int userFactionID;
    private Guid worldGUID = Guid.Empty;
    private int profileWorldID = -1;
    private bool admin;
    private bool mapEditor;
    private bool moderator;
    private bool showAdminMessage;
    private bool show2ndAgeMessage;
    private bool show3rdAgeMessage;
    private bool show4thAgeMessage;
    private bool show5thAgeMessage;
    private bool show6thAgeMessage;
    private bool show7thAgeMessage;
    private bool boxUser;
    private bool mobileWorld;
    private ReportFilterList reportFilters;
    private bool requiresVerification;
    private LoginLeadersInfo loginLeaderInfo;
    private AvatarData userAvatar = new AvatarData();
    private List<int> achievements = new List<int>();
    private GameOptionsData userOptions = new GameOptionsData();
    private Guid userGuid = Guid.Empty;
    private Guid sessionGuid = Guid.Empty;
    private string webToken = "";
    private AsyncCallback CreateNewUser_Callback;
    private RemoteServices.CreateNewUser_UserCallBack createNewUser_UserCallBack;
    private AsyncCallback LoginUser_Callback;
    private RemoteServices.LoginUser_UserCallBack loginUser_UserCallBack;
    private AsyncCallback LoginUserGuid_Callback;
    private RemoteServices.LoginUserGuid_UserCallBack loginUserGuid_UserCallBack;
    private AsyncCallback ResendVerificationEmail_Callback;
    private RemoteServices.ResendVerificationEmail_UserCallBack resendVerificationEmail_UserCallBack;
    private AsyncCallback GetAllVillageOwnerFactions_Callback;
    private RemoteServices.GetAllVillageOwnerFactions_UserCallBack getAllVillageOwnerFactions_UserCallBack;
    private int GetAllVillageOwnerFactions_Index;
    public bool GetAllVillageOwnerFactions_ValidDownload;
    private AsyncCallback GetVillageFactionChanges_Callback;
    private RemoteServices.GetVillageFactionChanges_UserCallBack getVillageFactionChanges_UserCallBack;
    private int GetVillageFactionChanges_Index;
    public bool GetVillageFactionChanges_ValidDownload;
    private AsyncCallback GetVillageNames_Callback;
    private RemoteServices.GetVillageNames_UserCallBack getVillageNames_UserCallBack;
    private int GetVillageNames_Index;
    public bool GetVillageNames_ValidDownload;
    private AsyncCallback GetAreaFactionChanges_Callback;
    private RemoteServices.GetAreaFactionChanges_UserCallBack getAreaFactionChanges_UserCallBack;
    private AsyncCallback GetUserVillages_Callback;
    private RemoteServices.GetUserVillages_UserCallBack getUserVillages_UserCallBack;
    private AsyncCallback GetOtherUserVillageIDList_Callback;
    private RemoteServices.GetOtherUserVillageIDList_UserCallBack getOtherUserVillageIDList_UserCallBack;
    private AsyncCallback BuyVillage_Callback;
    private RemoteServices.BuyVillage_UserCallBack buyVillage_UserCallBack;
    private AsyncCallback ConvertVillage_Callback;
    private RemoteServices.ConvertVillage_UserCallBack convertVillage_UserCallBack;
    private AsyncCallback FullTick_Callback;
    private RemoteServices.FullTick_UserCallBack fullTick_UserCallBack;
    private AsyncCallback LeaderBoard_Callback;
    private RemoteServices.LeaderBoard_UserCallBack leaderBoard_UserCallBack;
    private AsyncCallback LeaderBoardSearch_Callback;
    private RemoteServices.LeaderBoardSearch_UserCallBack leaderBoardSearch_UserCallBack;
    private AsyncCallback LogOut_Callback;
    private RemoteServices.LogOut_UserCallBack logOut_UserCallBack;
    private AsyncCallback GetLoginHistory_Callback;
    private RemoteServices.GetLoginHistory_UserCallBack getLoginHistory_UserCallBack;
    private AsyncCallback UserInfo_Callback;
    private RemoteServices.UserInfo_UserCallBack userInfo_UserCallBack;
    private AsyncCallback GetArmyData_Callback;
    private RemoteServices.GetArmyData_UserCallBack getArmyData_UserCallBack;
    private AsyncCallback ArmyAttack_Callback;
    private RemoteServices.ArmyAttack_UserCallBack armyAttack_UserCallBack;
    private AsyncCallback RetrieveAttackResult_Callback;
    private RemoteServices.RetrieveAttackResult_UserCallBack retrieveAttackResult_UserCallBack;
    private AsyncCallback SetAdminMessage_Callback;
    private RemoteServices.SetAdminMessage_UserCallBack setAdminMessage_UserCallBack;
    private AsyncCallback CompleteVillageCastle_Callback;
    private RemoteServices.CompleteVillageCastle_UserCallBack completeVillageCastle_UserCallBack;
    private AsyncCallback RetrieveStats_Callback;
    private RemoteServices.RetrieveStats_UserCallBack retrieveStats_UserCallBack;
    private AsyncCallback RetrieveStats2_Callback;
    private RemoteServices.RetrieveStats2_UserCallBack retrieveStats2_UserCallBack;
    private AsyncCallback GetAdminStats_Callback;
    private RemoteServices.GetAdminStats_UserCallBack getAdminStats_UserCallBack;
    private AsyncCallback GetReportsList_Callback;
    private RemoteServices.GetReportsList_UserCallBack getReportsList_UserCallBack;
    private AsyncCallback GetReport_Callback;
    private RemoteServices.GetReport_UserCallBack getReport_UserCallBack;
    private AsyncCallback ForwardReport_Callback;
    private RemoteServices.ForwardReport_UserCallBack forwardReport_UserCallBack;
    private AsyncCallback ViewBattle_Callback;
    private RemoteServices.ViewBattle_UserCallBack viewBattle_UserCallBack;
    private AsyncCallback ViewCastle_Callback;
    private RemoteServices.ViewCastle_UserCallBack viewCastle_UserCallBack;
    private AsyncCallback DeleteReports_Callback;
    private RemoteServices.DeleteReports_UserCallBack deleteReports_UserCallBack;
    private AsyncCallback UpdateReportFilters_Callback;
    private RemoteServices.UpdateReportFilters_UserCallBack updateReportFilters_UserCallBack;
    private AsyncCallback UpdateUserOptions_Callback;
    private RemoteServices.UpdateUserOptions_UserCallBack updateUserOptions_UserCallBack;
    private AsyncCallback ManageReportFolders_Callback;
    private RemoteServices.ManageReportFolders_UserCallBack manageReportFolders_UserCallBack;
    private AsyncCallback GetMailThreadList_Callback;
    private RemoteServices.GetMailThreadList_UserCallBack getMailThreadList_UserCallBack;
    private AsyncCallback GetMailThread_Callback;
    private RemoteServices.GetMailThread_UserCallBack getMailThread_UserCallBack;
    private AsyncCallback GetMailFolders_Callback;
    private RemoteServices.GetMailFolders_UserCallBack getMailFolders_UserCallBack;
    private AsyncCallback CreateMailFolder_Callback;
    private RemoteServices.CreateMailFolder_UserCallBack createMailFolder_UserCallBack;
    private AsyncCallback MoveToMailFolder_Callback;
    private RemoteServices.MoveToMailFolder_UserCallBack moveToMailFolder_UserCallBack;
    private AsyncCallback RemoveMailFolder_Callback;
    private RemoteServices.RemoveMailFolder_UserCallBack removeMailFolder_UserCallBack;
    private AsyncCallback ReportMail_Callback;
    private RemoteServices.ReportMail_UserCallBack reportMail_UserCallBack;
    private AsyncCallback FlagMailRead_Callback;
    private RemoteServices.FlagMailRead_UserCallBack flagMailRead_UserCallBack;
    private AsyncCallback SendMail_Callback;
    private RemoteServices.SendMail_UserCallBack sendMail_UserCallBack;
    private AsyncCallback SendSpecialMail_Callback;
    private RemoteServices.SendSpecialMail_UserCallBack sendSpecialMail_UserCallBack;
    private AsyncCallback DeleteMailThread_Callback;
    private RemoteServices.DeleteMailThread_UserCallBack deleteMailThread_UserCallBack;
    private AsyncCallback GetMailRecipientsHistory_Callback;
    private RemoteServices.GetMailRecipientsHistory_UserCallBack getMailRecipientsHistory_UserCallBack;
    private AsyncCallback GetMailUserSearch_Callback;
    private RemoteServices.GetMailUserSearch_UserCallBack getMailUserSearch_UserCallBack;
    private AsyncCallback AddUserToFavourites_Callback;
    private RemoteServices.AddUserToFavourites_UserCallBack addUserToFavourites_UserCallBack;
    private AsyncCallback GetHistoricalData_Callback;
    private RemoteServices.GetHistoricalData_UserCallBack getHistoricalData_UserCallBack;
    private AsyncCallback GetResourceLevel_Callback;
    private RemoteServices.GetResourceLevel_UserCallBack getResourceLevel_UserCallBack;
    private AsyncCallback GetVillageBuildingsList_Callback;
    private RemoteServices.GetVillageBuildingsList_UserCallBack getVillageBuildingsList_UserCallBack;
    private AsyncCallback PlaceVillageBuilding_Callback;
    private RemoteServices.PlaceVillageBuilding_UserCallBack placeVillageBuilding_UserCallBack;
    private AsyncCallback DeleteVillageBuilding_Callback;
    private RemoteServices.DeleteVillageBuilding_UserCallBack deleteVillageBuilding_UserCallBack;
    private AsyncCallback CancelDeleteVillageBuilding_Callback;
    private RemoteServices.CancelDeleteVillageBuilding_UserCallBack cancelDeleteVillageBuilding_UserCallBack;
    private AsyncCallback MoveVillageBuilding_Callback;
    private RemoteServices.MoveVillageBuilding_UserCallBack moveVillageBuilding_UserCallBack;
    private AsyncCallback VillageBuildingCompleteDataRetrieval_Callback;
    private RemoteServices.VillageBuildingCompleteDataRetrieval_UserCallBack villageBuildingCompleteDataRetrieval_UserCallBack;
    private AsyncCallback VillageBuildingSetActive_Callback;
    private RemoteServices.VillageBuildingSetActive_UserCallBack villageBuildingSetActive_UserCallBack;
    private AsyncCallback VillageBuildingChangeRates_Callback;
    private RemoteServices.VillageBuildingChangeRates_UserCallBack villageBuildingChangeRates_UserCallBack;
    private AsyncCallback VillageRename_Callback;
    private RemoteServices.VillageRename_UserCallBack villageRename_UserCallBack;
    private AsyncCallback VillageProduceWeapons_Callback;
    private RemoteServices.VillageProduceWeapons_UserCallBack villageProduceWeapons_UserCallBack;
    private AsyncCallback VillageHoldBanquet_Callback;
    private RemoteServices.VillageHoldBanquet_UserCallBack villageHoldBanquet_UserCallBack;
    private AsyncCallback GetCastle_Callback;
    private RemoteServices.GetCastle_UserCallBack getCastle_UserCallBack;
    private AsyncCallback AddCastleElement_Callback;
    private RemoteServices.AddCastleElement_UserCallBack addCastleElement_UserCallBack;
    private AsyncCallback DeleteCastleElement_Callback;
    private RemoteServices.DeleteCastleElement_UserCallBack deleteCastleElement_UserCallBack;
    private AsyncCallback ChangeCastleElementAggressiveDefender_Callback;
    private RemoteServices.ChangeCastleElementAggressiveDefender_UserCallBack changeCastleElementAggressiveDefender_UserCallBack;
    private AsyncCallback AutoRepairCastle_Callback;
    private RemoteServices.AutoRepairCastle_UserCallBack autoRepairCastle_UserCallBack;
    private AsyncCallback MemorizeCastleTroops_Callback;
    private RemoteServices.MemorizeCastleTroops_UserCallBack memorizeCastleTroops_UserCallBack;
    private AsyncCallback RestoreCastleTroops_Callback;
    private RemoteServices.RestoreCastleTroops_UserCallBack restoreCastleTroops_UserCallBack;
    private AsyncCallback CheatAddTroops_Callback;
    private RemoteServices.CheatAddTroops_UserCallBack cheatAddTroops_UserCallBack;
    private AsyncCallback LaunchCastleAttack_Callback;
    private RemoteServices.LaunchCastleAttack_UserCallBack launchCastleAttack_UserCallBack;
    private AsyncCallback SendScouts_Callback;
    private RemoteServices.SendScouts_UserCallBack sendScouts_UserCallBack;
    private AsyncCallback CancelCastleAttack_Callback;
    private RemoteServices.CancelCastleAttack_UserCallBack cancelCastleAttack_UserCallBack;
    private AsyncCallback SendReinforcements_Callback;
    private RemoteServices.SendReinforcements_UserCallBack sendReinforcements_UserCallBack;
    private AsyncCallback ReturnReinforcements_Callback;
    private RemoteServices.ReturnReinforcements_UserCallBack returnReinforcements_UserCallBack;
    private AsyncCallback SendMarketResources_Callback;
    private RemoteServices.SendMarketResources_UserCallBack sendMarketResources_UserCallBack;
    private AsyncCallback GetUserTraders_Callback;
    private RemoteServices.GetUserTraders_UserCallBack getUserTraders_UserCallBack;
    private AsyncCallback GetActiveTraders_Callback;
    private RemoteServices.GetActiveTraders_UserCallBack getActiveTraders_UserCallBack;
    private AsyncCallback GetStockExchangeData_Callback;
    private RemoteServices.GetStockExchangeData_UserCallBack getStockExchangeData_UserCallBack;
    private AsyncCallback StockExchangeTrade_Callback;
    private RemoteServices.StockExchangeTrade_UserCallBack stockExchangeTrade_UserCallBack;
    private AsyncCallback UpdateVillageFavourites_Callback;
    private RemoteServices.UpdateVillageFavourites_UserCallBack updateVillageFavourites_UserCallBack;
    private AsyncCallback MakeTroop_Callback;
    private RemoteServices.MakeTroop_UserCallBack makeTroop_UserCallBack;
    private AsyncCallback UpgradeRank_Callback;
    private RemoteServices.UpgradeRank_UserCallBack upgradeRank_UserCallBack;
    private AsyncCallback PreAttackSetup_Callback;
    private RemoteServices.PreAttackSetup_UserCallBack preAttackSetup_UserCallBack;
    private AsyncCallback GetBattleHonourRating_Callback;
    private RemoteServices.GetBattleHonourRating_UserCallBack getBattleHonourRating_UserCallBack;
    private AsyncCallback RetrieveArmyFromGarrison_Callback;
    private RemoteServices.RetrieveArmyFromGarrison_UserCallBack retrieveArmyFromGarrison_UserCallBack;
    private AsyncCallback RetrieveVillageUserInfo_Callback;
    private RemoteServices.RetrieveVillageUserInfo_UserCallBack retrieveVillageUserInfo_UserCallBack;
    private AsyncCallback SpecialVillageInfo_Callback;
    private RemoteServices.SpecialVillageInfo_UserCallBack specialVillageInfo_UserCallBack;
    private AsyncCallback GetVillageRankTaxTree_Callback;
    private RemoteServices.GetVillageRankTaxTree_UserCallBack getVillageRankTaxTree_UserCallBack;
    private AsyncCallback GetResearchData_Callback;
    private RemoteServices.GetResearchData_UserCallBack getResearchData_UserCallBack;
    private AsyncCallback DoResearch_Callback;
    private RemoteServices.DoResearch_UserCallBack doResearch_UserCallBack;
    private AsyncCallback BuyResearchPoint_Callback;
    private RemoteServices.BuyResearchPoint_UserCallBack buyResearchPoint_UserCallBack;
    private AsyncCallback VassalInfo_Callback;
    private RemoteServices.VassalInfo_UserCallBack vassalInfo_UserCallBack;
    private AsyncCallback VassalSendResources_Callback;
    private RemoteServices.VassalSendResources_UserCallBack vassalSendResources_UserCallBack;
    private AsyncCallback UpdateSelectedTitheType_Callback;
    private RemoteServices.UpdateSelectedTitheType_UserCallBack updateSelectedTitheType_UserCallBack;
    private AsyncCallback BreakVassalage_Callback;
    private RemoteServices.BreakVassalage_UserCallBack breakVassalage_UserCallBack;
    private AsyncCallback BreakLiegeLord_Callback;
    private RemoteServices.BreakLiegeLord_UserCallBack breakLiegeLord_UserCallBack;
    private AsyncCallback GetPreVassalInfo_Callback;
    private RemoteServices.GetPreVassalInfo_UserCallBack getPreVassalInfo_UserCallBack;
    private AsyncCallback SendVassalRequest_Callback;
    private RemoteServices.SendVassalRequest_UserCallBack sendVassalRequest_UserCallBack;
    private AsyncCallback HandleVassalRequest_Callback;
    private RemoteServices.HandleVassalRequest_UserCallBack handleVassalRequest_UserCallBack;
    private AsyncCallback GetVassalArmyInfo_Callback;
    private RemoteServices.GetVassalArmyInfo_UserCallBack getVassalArmyInfo_UserCallBack;
    private AsyncCallback SendTroopsToVassal_Callback;
    private RemoteServices.SendTroopsToVassal_UserCallBack sendTroopsToVassal_UserCallBack;
    private AsyncCallback RetrieveTroopsFromVassal_Callback;
    private RemoteServices.RetrieveTroopsFromVassal_UserCallBack retrieveTroopsFromVassal_UserCallBack;
    private AsyncCallback UpdateVillageResourcesInfo_Callback;
    private RemoteServices.UpdateVillageResourcesInfo_UserCallBack updateVillageResourcesInfo_UserCallBack;
    private AsyncCallback SetHighestArmySeen_Callback;
    private RemoteServices.SetHighestArmySeen_UserCallBack setHighestArmySeen_UserCallBack;
    private AsyncCallback GetForumList_Callback;
    private RemoteServices.GetForumList_UserCallBack getForumList_UserCallBack;
    private AsyncCallback GetForumThreadList_Callback;
    private RemoteServices.GetForumThreadList_UserCallBack getForumThreadList_UserCallBack;
    private AsyncCallback GetForumThread_Callback;
    private RemoteServices.GetForumThread_UserCallBack getForumThread_UserCallBack;
    private AsyncCallback NewForumThread_Callback;
    private RemoteServices.NewForumThread_UserCallBack newForumThread_UserCallBack;
    private AsyncCallback PostToForumThread_Callback;
    private RemoteServices.PostToForumThread_UserCallBack postToForumThread_UserCallBack;
    private AsyncCallback GiveForumAccess_Callback;
    private RemoteServices.GiveForumAccess_UserCallBack giveForumAccess_UserCallBack;
    private AsyncCallback CreateForum_Callback;
    private RemoteServices.CreateForum_UserCallBack createForum_UserCallBack;
    private AsyncCallback DeleteForum_Callback;
    private RemoteServices.DeleteForum_UserCallBack deleteForum_UserCallBack;
    private AsyncCallback DeleteForumThread_Callback;
    private RemoteServices.DeleteForumThread_UserCallBack deleteForumThread_UserCallBack;
    private AsyncCallback DeleteForumPost_Callback;
    private RemoteServices.DeleteForumPost_UserCallBack deleteForumPost_UserCallBack;
    private AsyncCallback GetCurrentElectionInfo_Callback;
    private RemoteServices.GetCurrentElectionInfo_UserCallBack getCurrentElectionInfo_UserCallBack;
    private AsyncCallback StandInElection_Callback;
    private RemoteServices.StandInElection_UserCallBack standInElection_UserCallBack;
    private AsyncCallback VoteInElection_Callback;
    private RemoteServices.VoteInElection_UserCallBack voteInElection_UserCallBack;
    private AsyncCallback UploadAvatar_Callback;
    private RemoteServices.UploadAvatar_UserCallBack uploadAvatar_UserCallBack;
    private AsyncCallback MakePeople_Callback;
    private RemoteServices.MakePeople_UserCallBack makePeople_UserCallBack;
    private AsyncCallback GetUserPeople_Callback;
    private RemoteServices.GetUserPeople_UserCallBack getUserPeople_UserCallBack;
    private AsyncCallback GetUserIDFromName_Callback;
    private RemoteServices.GetUserIDFromName_UserCallBack getUserIDFromName_UserCallBack;
    private AsyncCallback GetActivePeople_Callback;
    private RemoteServices.GetActivePeople_UserCallBack getActivePeople_UserCallBack;
    private AsyncCallback SendPeople_Callback;
    private RemoteServices.SendPeople_UserCallBack sendPeople_UserCallBack;
    private AsyncCallback RetrievePeople_Callback;
    private RemoteServices.RetrievePeople_UserCallBack retrievePeople_UserCallBack;
    private AsyncCallback SpyCommand_Callback;
    private RemoteServices.SpyCommand_UserCallBack spyCommand_UserCallBack;
    private AsyncCallback SpyGetVillageResourceInfo_Callback;
    private RemoteServices.SpyGetVillageResourceInfo_UserCallBack spyGetVillageResourceInfo_UserCallBack;
    private AsyncCallback SpyGetArmyInfo_Callback;
    private RemoteServices.SpyGetArmyInfo_UserCallBack spyGetArmyInfo_UserCallBack;
    private AsyncCallback SpyGetResearchInfo_Callback;
    private RemoteServices.SpyGetResearchInfo_UserCallBack spyGetResearchInfo_UserCallBack;
    private AsyncCallback CreateFaction_Callback;
    private RemoteServices.CreateFaction_UserCallBack createFaction_UserCallBack;
    private AsyncCallback DisbandFaction_Callback;
    private RemoteServices.DisbandFaction_UserCallBack disbandFaction_UserCallBack;
    private AsyncCallback FactionSendInvite_Callback;
    private RemoteServices.FactionSendInvite_UserCallBack factionSendInvite_UserCallBack;
    private AsyncCallback FactionWithdrawInvite_Callback;
    private RemoteServices.FactionWithdrawInvite_UserCallBack factionWithdrawInvite_UserCallBack;
    private AsyncCallback FactionReplyToInvite_Callback;
    private RemoteServices.FactionReplyToInvite_UserCallBack factionReplyToInvite_UserCallBack;
    private AsyncCallback FactionChangeMemberStatus_Callback;
    private RemoteServices.FactionChangeMemberStatus_UserCallBack factionChangeMemberStatus_UserCallBack;
    private AsyncCallback FactionLeave_Callback;
    private RemoteServices.FactionLeave_UserCallBack factionLeave_UserCallBack;
    private AsyncCallback GetFactionData_Callback;
    private RemoteServices.GetFactionData_UserCallBack getFactionData_UserCallBack;
    private AsyncCallback CreateUserRelationship_Callback;
    private RemoteServices.CreateUserRelationship_UserCallBack createUserRelationship_UserCallBack;
    private AsyncCallback SetUserMarker_Callback;
    private RemoteServices.SetUserMarker_UserCallBack setUserMarker_UserCallBack;
    private AsyncCallback CreateFactionRelationship_Callback;
    private RemoteServices.CreateFactionRelationship_UserCallBack createFactionRelationship_UserCallBack;
    private AsyncCallback CreateHouseRelationship_Callback;
    private RemoteServices.CreateHouseRelationship_UserCallBack createHouseRelationship_UserCallBack;
    private AsyncCallback GetHouseGloryPoints_Callback;
    private RemoteServices.GetHouseGloryPoints_UserCallBack getHouseGloryPoints_UserCallBack;
    private AsyncCallback ChangeFactionMotto_Callback;
    private RemoteServices.ChangeFactionMotto_UserCallBack changeFactionMotto_UserCallBack;
    private AsyncCallback FactionLeadershipVote_Callback;
    private RemoteServices.FactionLeadershipVote_UserCallBack factionLeadershipVote_UserCallBack;
    private AsyncCallback GetViewFactionData_Callback;
    private RemoteServices.GetViewFactionData_UserCallBack getViewFactionData_UserCallBack;
    private AsyncCallback GetViewHouseData_Callback;
    private RemoteServices.GetViewHouseData_UserCallBack getViewHouseData_UserCallBack;
    private AsyncCallback SelfJoinHouse_Callback;
    private RemoteServices.SelfJoinHouse_UserCallBack selfJoinHouse_UserCallBack;
    private AsyncCallback HouseVote_Callback;
    private RemoteServices.HouseVote_UserCallBack houseVote_UserCallBack;
    private AsyncCallback HouseVoteHouseLeader_Callback;
    private RemoteServices.HouseVoteHouseLeader_UserCallBack houseVoteHouseLeader_UserCallBack;
    private AsyncCallback TouchHouseVisitDate_Callback;
    private RemoteServices.TouchHouseVisitDate_UserCallBack touchHouseVisitDate_UserCallBack;
    private AsyncCallback LeaveHouse_Callback;
    private RemoteServices.LeaveHouse_UserCallBack leaveHouse_UserCallBack;
    private AsyncCallback FactionApplication_Callback;
    private RemoteServices.FactionApplication_UserCallBack factionApplication_UserCallBack;
    private AsyncCallback FactionApplicationProcessing_Callback;
    private RemoteServices.FactionApplicationProcessing_UserCallBack factionApplicationProcessing_UserCallBack;
    private AsyncCallback GetParishMembersList_Callback;
    private RemoteServices.GetParishMembersList_UserCallBack getParishMembersList_UserCallBack;
    private AsyncCallback GetParishFrontPageInfo_Callback;
    private RemoteServices.GetParishFrontPageInfo_UserCallBack getParishFrontPageInfo_UserCallBack;
    private AsyncCallback ParishWallDetailInfo_Callback;
    private RemoteServices.ParishWallDetailInfo_UserCallBack parishWallDetailInfo_UserCallBack;
    private AsyncCallback StandDownAsParishDespot_Callback;
    private RemoteServices.StandDownAsParishDespot_UserCallBack standDownAsParishDespot_UserCallBack;
    private AsyncCallback MakeParishVote_Callback;
    private RemoteServices.MakeParishVote_UserCallBack makeParishVote_UserCallBack;
    private AsyncCallback SendTroopsToCapital_Callback;
    private RemoteServices.SendTroopsToCapital_UserCallBack sendTroopsToCapital_UserCallBack;
    private AsyncCallback GetCapitalBarracksSpace_Callback;
    private RemoteServices.GetCapitalBarracksSpace_UserCallBack getCapitalBarracksSpace_UserCallBack;
    private AsyncCallback GetCountyElectionInfo_Callback;
    private RemoteServices.GetCountyElectionInfo_UserCallBack getCountyElectionInfo_UserCallBack;
    private AsyncCallback GetCountyFrontPageInfo_Callback;
    private RemoteServices.GetCountyFrontPageInfo_UserCallBack getCountyFrontPageInfo_UserCallBack;
    private AsyncCallback MakeCountyVote_Callback;
    private RemoteServices.MakeCountyVote_UserCallBack makeCountyVote_UserCallBack;
    private AsyncCallback GetProvinceElectionInfo_Callback;
    private RemoteServices.GetProvinceElectionInfo_UserCallBack getProvinceElectionInfo_UserCallBack;
    private AsyncCallback GetProvinceFrontPageInfo_Callback;
    private RemoteServices.GetProvinceFrontPageInfo_UserCallBack getProvinceFrontPageInfo_UserCallBack;
    private AsyncCallback MakeProvinceVote_Callback;
    private RemoteServices.MakeProvinceVote_UserCallBack makeProvinceVote_UserCallBack;
    private AsyncCallback GetCountryElectionInfo_Callback;
    private RemoteServices.GetCountryElectionInfo_UserCallBack getCountryElectionInfo_UserCallBack;
    private AsyncCallback GetCountryFrontPageInfo_Callback;
    private RemoteServices.GetCountryFrontPageInfo_UserCallBack getCountryFrontPageInfo_UserCallBack;
    private AsyncCallback MakeCountryVote_Callback;
    private RemoteServices.MakeCountryVote_UserCallBack makeCountryVote_UserCallBack;
    private AsyncCallback GetIngameMessage_Callback;
    private RemoteServices.GetIngameMessage_UserCallBack getIngameMessage_UserCallBack;
    private AsyncCallback CancelInterdiction_Callback;
    private RemoteServices.CancelInterdiction_UserCallBack cancelInterdiction_UserCallBack;
    private AsyncCallback GetExcommunicationStatus_Callback;
    private RemoteServices.GetExcommunicationStatus_UserCallBack getExcommunicationStatus_UserCallBack;
    private AsyncCallback DisbandTroops_Callback;
    private RemoteServices.DisbandTroops_UserCallBack disbandTroops_UserCallBack;
    private AsyncCallback DisbandPeople_Callback;
    private RemoteServices.DisbandPeople_UserCallBack disbandPeople_UserCallBack;
    private AsyncCallback GetVillageInfoForDonateCapitalGoods_Callback;
    private RemoteServices.GetVillageInfoForDonateCapitalGoods_UserCallBack getVillageInfoForDonateCapitalGoods_UserCallBack;
    private AsyncCallback DonateCapitalGoods_Callback;
    private RemoteServices.DonateCapitalGoods_UserCallBack donateCapitalGoods_UserCallBack;
    private AsyncCallback GetVillageStartLocations_Callback;
    private RemoteServices.GetVillageStartLocations_UserCallBack getVillageStartLocations_UserCallBack;
    private AsyncCallback SetStartingCounty_Callback;
    private RemoteServices.SetStartingCounty_UserCallBack setStartingCounty_UserCallBack;
    private AsyncCallback CancelCard_Callback;
    private RemoteServices.CancelCard_UserCallBack cancelCard_UserCallBack;
    private AsyncCallback UpdateCurrentCards_Callback;
    private RemoteServices.UpdateCurrentCards_UserCallBack updateCurrentCards_UserCallBack;
    private AsyncCallback TutorialCommand_Callback;
    private RemoteServices.TutorialCommand_UserCallBack tutorialCommand_UserCallBack;
    private AsyncCallback GetQuestStatus_Callback;
    private RemoteServices.GetQuestStatus_UserCallBack getQuestStatus_UserCallBack;
    private AsyncCallback CompleteQuest_Callback;
    private RemoteServices.CompleteQuest_UserCallBack completeQuest_UserCallBack;
    private AsyncCallback FlagQuestObjectiveComplete_Callback;
    private RemoteServices.FlagQuestObjectiveComplete_UserCallBack flagQuestObjectiveComplete_UserCallBack;
    private AsyncCallback CheckQuestObjectiveComplete_Callback;
    private RemoteServices.CheckQuestObjectiveComplete_UserCallBack checkQuestObjectiveComplete_UserCallBack;
    private AsyncCallback UpdateDiplomacyStatus_Callback;
    private RemoteServices.UpdateDiplomacyStatus_UserCallBack updateDiplomacyStatus_UserCallBack;
    private AsyncCallback SendCommands_Callback;
    private RemoteServices.SendCommands_UserCallBack sendCommands_UserCallBack;
    private AsyncCallback InitialiseFreeCards_Callback;
    private RemoteServices.InitialiseFreeCards_UserCallBack initialiseFreeCards_UserCallBack;
    private AsyncCallback TestAchievements_Callback;
    private RemoteServices.TestAchievements_UserCallBack testAchievements_UserCallBack;
    private AsyncCallback AchievementProgress_Callback;
    private RemoteServices.AchievementProgress_UserCallBack achievementProgress_UserCallBack;
    private AsyncCallback GetQuestData_Callback;
    private RemoteServices.GetQuestData_UserCallBack getQuestData_UserCallBack;
    private AsyncCallback StartNewQuest_Callback;
    private RemoteServices.StartNewQuest_UserCallBack startNewQuest_UserCallBack;
    private AsyncCallback CompleteAbandonNewQuest_Callback;
    private RemoteServices.CompleteAbandonNewQuest_UserCallBack completeAbandonNewQuest_UserCallBack;
    private AsyncCallback SpinTheWheel_Callback;
    private RemoteServices.SpinTheWheel_UserCallBack spinTheWheel_UserCallBack;
    private AsyncCallback SetVacationMode_Callback;
    private RemoteServices.SetVacationMode_UserCallBack setVacationMode_UserCallBack;
    private AsyncCallback PremiumOverview_Callback;
    private RemoteServices.PremiumOverview_UserCallBack premiumOverview_UserCallBack;
    private AsyncCallback GetLastAttacker_Callback;
    private RemoteServices.GetLastAttacker_UserCallBack getLastAttacker_UserCallBack;
    private AsyncCallback PreValidateCardToBePlayed_Callback;
    private RemoteServices.PreValidateCardToBePlayed_UserCallBack preValidateCardToBePlayed_UserCallBack;
    private AsyncCallback GetInvasionInfo_Callback;
    private RemoteServices.GetInvasionInfo_UserCallBack getInvasionInfo_UserCallBack;
    private AsyncCallback WorldInfo_Callback;
    private RemoteServices.WorldInfo_UserCallBack worldInfo_UserCallBack;
    private AsyncCallback EndWorld_Callback;
    private RemoteServices.EndWorld_UserCallBack endWorld_UserCallBack;
    private AsyncCallback EndOfTheWorldStats_Callback;
    private RemoteServices.EndOfTheWorldStats_UserCallBack endOfTheWorldStats_UserCallBack;
    private AsyncCallback GetKillStreakData_Callback;
    private RemoteServices.GetKillStreakData_UserCallBack getKillStreakData_UserCallBack;
    private AsyncCallback GetUserContestData_Callback;
    private RemoteServices.GetUserContestData_UserCallBack getUserContestData_UserCallBack;
    private AsyncCallback GetContestDataRange_Callback;
    private RemoteServices.GetContestDataRange_UserCallBack getContestDataRange_UserCallBack;
    private AsyncCallback GetContestHistoryIDs_Callback;
    private RemoteServices.GetContestHistoryIDs_UserCallBack getContestHistoryIDs_UserCallBack;
    private AsyncCallback Chat_Login_Callback;
    private RemoteServices.Chat_Login_UserCallBack chat_Login_UserCallBack;
    private AsyncCallback Chat_Logout_Callback;
    private RemoteServices.Chat_Logout_UserCallBack chat_Logout_UserCallBack;
    private AsyncCallback Chat_SetReceivingState_Callback;
    private RemoteServices.Chat_SetReceivingState_UserCallBack chat_SetReceivingState_UserCallBack;
    private AsyncCallback Chat_SendText_Callback;
    private RemoteServices.Chat_SendText_UserCallBack chat_SendText_UserCallBack;
    private AsyncCallback Chat_ReceiveText_Callback;
    private RemoteServices.Chat_ReceiveText_UserCallBack chat_ReceiveText_UserCallBack;
    private AsyncCallback Chat_SendParishText_Callback;
    private RemoteServices.Chat_SendParishText_UserCallBack chat_SendParishText_UserCallBack;
    private AsyncCallback Chat_ReceiveParishText_Callback;
    private RemoteServices.Chat_ReceiveParishText_UserCallBack chat_ReceiveParishText_UserCallBack;
    private AsyncCallback Chat_BackFillParishText_Callback;
    private RemoteServices.Chat_BackFillParishText_UserCallBack chat_BackFillParishText_UserCallBack;
    private AsyncCallback Chat_MarkParishTextRead_Callback;
    private RemoteServices.Chat_MarkParishTextRead_UserCallBack chat_MarkParishTextRead_UserCallBack;
    private AsyncCallback Chat_Admin_Command_Callback;
    private RemoteServices.Chat_Admin_Command_UserCallBack chat_Admin_Command_UserCallBack;
    private RemoteServices.CommonData_UserCallBack commonData_UserCallBack;
    private bool connectionErrored;
    private int consecutiveTimeOuts;
    private HttpChannel channel;
    private static object syncLock = new object();
    public double RTTAverageTime;
    public int RTTAverageCount;
    public double RTTAverageShortTime;
    public int RTTAverageShortCount;
    public double RTTAverageLongTime;
    public int RTTAverageLongCount;
    public int RTTTimeOuts;
    public List<RemoteServices.RTT_Log_data> rtt_logging = new List<RemoteServices.RTT_Log_data>();
    public int lastLatency;

    private RemoteServices()
    {
    }

    public static RemoteServices Instance => RemoteServices.instance;

    public bool InResultsProcessing => this.inResultsProcessing;

    public bool ChatActive
    {
      get => this.chatActive;
      set => this.chatActive = value;
    }

    public int ProfileWorldID
    {
      get => this.profileWorldID;
      set => this.profileWorldID = value;
    }

    public Guid UserGuid
    {
      get => this.userGuid;
      set => this.userGuid = value;
    }

    public Guid SessionGuid
    {
      get => this.sessionGuid;
      set => this.sessionGuid = value;
    }

    public string WebToken
    {
      get => this.webToken;
      set => this.webToken = value;
    }

    public string UserGuidProfileSite => this.UserGuid.ToString().Replace("-", "");

    public string SessionGuidProfileSite => this.SessionGuid.ToString().Replace("-", "");

    public int UserID
    {
      get => this.userID;
      set => this.userID = value;
    }

    public int SessionID
    {
      get => this.sessionID;
      set => this.sessionID = value;
    }

    public int UserFactionID
    {
      get => this.userFactionID;
      set => this.userFactionID = value;
    }

    public string UserName
    {
      get => this.username;
      set => this.username = value;
    }

    public string RealName
    {
      get => this.realname;
      set => this.realname = value;
    }

    public Guid WorldGUID
    {
      get => this.worldGUID;
      set => this.worldGUID = value;
    }

    public bool Admin
    {
      get => this.admin;
      set => this.admin = value;
    }

    public bool MapEditor
    {
      get => this.mapEditor;
      set => this.mapEditor = value;
    }

    public bool Moderator
    {
      get => this.moderator;
      set => this.moderator = value;
    }

    public bool ShowAdminMessage
    {
      get
      {
        bool showAdminMessage = this.showAdminMessage;
        this.showAdminMessage = false;
        return showAdminMessage;
      }
      set => this.showAdminMessage = value;
    }

    public bool Show2ndAgeMessage
    {
      get
      {
        bool show2ndAgeMessage = this.show2ndAgeMessage;
        this.show2ndAgeMessage = false;
        return show2ndAgeMessage;
      }
      set => this.show2ndAgeMessage = value;
    }

    public bool Show3rdAgeMessage
    {
      get
      {
        bool show3rdAgeMessage = this.show3rdAgeMessage;
        this.show3rdAgeMessage = false;
        return show3rdAgeMessage;
      }
      set => this.show3rdAgeMessage = value;
    }

    public bool Show4thAgeMessage
    {
      get
      {
        bool show4thAgeMessage = this.show4thAgeMessage;
        this.show4thAgeMessage = false;
        return show4thAgeMessage;
      }
      set => this.show4thAgeMessage = value;
    }

    public bool Show5thAgeMessage
    {
      get
      {
        bool show5thAgeMessage = this.show5thAgeMessage;
        this.show5thAgeMessage = false;
        return show5thAgeMessage;
      }
      set => this.show5thAgeMessage = value;
    }

    public bool Show6thAgeMessage
    {
      get
      {
        bool show6thAgeMessage = this.show6thAgeMessage;
        this.show6thAgeMessage = false;
        return show6thAgeMessage;
      }
      set => this.show6thAgeMessage = value;
    }

    public bool Show7thAgeMessage
    {
      get
      {
        bool show7thAgeMessage = this.show7thAgeMessage;
        this.show7thAgeMessage = false;
        return show7thAgeMessage;
      }
      set => this.show7thAgeMessage = value;
    }

    public ReportFilterList ReportFilters
    {
      get => this.reportFilters;
      set => this.reportFilters = value;
    }

    public bool RequiresVerification
    {
      get => this.requiresVerification;
      set => this.requiresVerification = value;
    }

    public LoginLeadersInfo LoginLeaderInfo
    {
      get => this.loginLeaderInfo;
      set => this.loginLeaderInfo = value;
    }

    public bool BoxUser
    {
      get => this.boxUser;
      set => this.boxUser = value;
    }

    public bool MobileWorld
    {
      get => this.mobileWorld;
      set => this.mobileWorld = value;
    }

    public AvatarData UserAvatar
    {
      get => this.userAvatar;
      set => this.userAvatar = value;
    }

    public GameOptionsData UserOptions
    {
      get => this.userOptions;
      set => this.userOptions = value;
    }

    public List<int> UserAchievements
    {
      get => this.achievements;
      set => this.achievements = value;
    }

    private bool UnityEndOfTheWorldHandler(Common_ReturnData returnData)
    {
      if (returnData == null || returnData.Success || returnData.m_errorCode != CommonTypes.ErrorCodes.ErrorCode.LOCK_OUT)
        return false;
      GameEngine.Instance.World.WorldEnded = true;
      return true;
    }

    public void set_CreateNewUser_UserCallBack(RemoteServices.CreateNewUser_UserCallBack callback)
    {
      this.createNewUser_UserCallBack = callback;
    }

    public void createNewUser(
      string username,
      string password,
      string realname,
      string emailaddress)
    {
      if (this.CreateNewUser_Callback == null)
        this.CreateNewUser_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CreateNewUser);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CreateNewUser(this.service.CreateNewUser).BeginInvoke(username, password, realname, emailaddress, BuildVersion.VersionNumber, "", this.CreateNewUser_Callback, (object) null), typeof (CreateNewUser_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CreateNewUser(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CreateNewUser asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CreateNewUser) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CreateNewUser_ReturnType returnData = new CreateNewUser_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_LoginUser_UserCallBack(RemoteServices.LoginUser_UserCallBack callback)
    {
      this.loginUser_UserCallBack = callback;
    }

    public void LoginUser(string username, string password, string verificationString)
    {
      bool needVillageData = true;
      if (VillageMap.villageBuildingData != null)
        needVillageData = false;
      if (this.LoginUser_Callback == null)
        this.LoginUser_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LoginUser);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_LoginUser(this.service.LoginUser).BeginInvoke(username, password, BuildVersion.VersionNumber, verificationString, needVillageData, this.LoginUser_Callback, (object) null), typeof (LoginUser_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_LoginUser(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_LoginUser asyncDelegate = (RemoteServices.RemoteAsyncDelegate_LoginUser) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        LoginUser_ReturnType returnData = new LoginUser_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_LoginUserGuid_UserCallBack(RemoteServices.LoginUserGuid_UserCallBack callback)
    {
      this.loginUserGuid_UserCallBack = callback;
    }

    public void LoginUserGuid(string username, Guid userGuid, Guid sessionGuid)
    {
      bool needVillageData = true;
      if (VillageMap.villageBuildingData != null)
        needVillageData = false;
      if (this.LoginUserGuid_Callback == null)
        this.LoginUserGuid_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LoginUserGuid);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_LoginUserGuid(this.service.LoginUserGuid).BeginInvoke(username, userGuid.ToString(), sessionGuid.ToString(), needVillageData, BuildVersion.VersionNumber, this.LoginUserGuid_Callback, (object) null), typeof (LoginUserGuid_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_LoginUserGuid(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_LoginUserGuid asyncDelegate = (RemoteServices.RemoteAsyncDelegate_LoginUserGuid) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        LoginUserGuid_ReturnType returnData = new LoginUserGuid_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_ResendVerificationEmail_UserCallBack(
      RemoteServices.ResendVerificationEmail_UserCallBack callback)
    {
      this.resendVerificationEmail_UserCallBack = callback;
    }

    public void ResendVerificationEmail(string username, string password)
    {
      if (this.ResendVerificationEmail_Callback == null)
        this.ResendVerificationEmail_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ResendVerificationEmail);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ResendVerificationEmail(this.service.ResendVerificationEmail).BeginInvoke(username, password, this.ResendVerificationEmail_Callback, (object) null), typeof (ResendVerificationEmail_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_ResendVerificationEmail(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_ResendVerificationEmail asyncDelegate = (RemoteServices.RemoteAsyncDelegate_ResendVerificationEmail) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        ResendVerificationEmail_ReturnType returnData = new ResendVerificationEmail_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetAllVillageOwnerFactions_UserCallBack(
      RemoteServices.GetAllVillageOwnerFactions_UserCallBack callback)
    {
      this.getAllVillageOwnerFactions_UserCallBack = callback;
    }

    public void GetAllVillageOwnerFactions()
    {
      if (this.GetAllVillageOwnerFactions_Callback == null)
        this.GetAllVillageOwnerFactions_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetAllVillageOwnerFactions);
      RemoteServices.RemoteAsyncDelegate_GetAllVillageOwnerFactions villageOwnerFactions = new RemoteServices.RemoteAsyncDelegate_GetAllVillageOwnerFactions(this.service.GetAllVillageOwnerFactions);
      this.GetAllVillageOwnerFactions_ValidDownload = false;
      ++this.GetAllVillageOwnerFactions_Index;
      this.registerRPCcall(villageOwnerFactions.BeginInvoke(this.UserID, this.SessionID, this.GetAllVillageOwnerFactions_Index, this.GetAllVillageOwnerFactions_Callback, (object) null), typeof (GetAllVillageOwnerFactions_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetAllVillageOwnerFactions(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetAllVillageOwnerFactions asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetAllVillageOwnerFactions) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        GetAllVillageOwnerFactions_ReturnType resultData = asyncDelegate.EndInvoke(ar);
        if (resultData.sendIndex == this.GetAllVillageOwnerFactions_Index)
        {
          this.GetAllVillageOwnerFactions_ValidDownload = true;
          this.storeRPCresult(ar, (Common_ReturnData) resultData);
        }
        else
          this.removeRPCresult(ar);
      }
      catch (Exception ex)
      {
        GetAllVillageOwnerFactions_ReturnType returnData = new GetAllVillageOwnerFactions_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetVillageFactionChanges_UserCallBack(
      RemoteServices.GetVillageFactionChanges_UserCallBack callback)
    {
      this.getVillageFactionChanges_UserCallBack = callback;
    }

    public void GetVillageFactionChanges(long startChangePos, long factionsChangePos)
    {
      if (startChangePos < -1L)
        startChangePos = -1L;
      if (factionsChangePos < -1L)
        factionsChangePos = -1L;
      if (this.GetVillageFactionChanges_Callback == null)
        this.GetVillageFactionChanges_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetVillageFactionChanges);
      RemoteServices.RemoteAsyncDelegate_GetVillageFactionChanges villageFactionChanges = new RemoteServices.RemoteAsyncDelegate_GetVillageFactionChanges(this.service.GetVillageFactionChanges);
      this.GetVillageFactionChanges_ValidDownload = false;
      ++this.GetVillageFactionChanges_Index;
      this.registerRPCcall(villageFactionChanges.BeginInvoke(this.UserID, this.SessionID, startChangePos, factionsChangePos, this.GetVillageFactionChanges_Index, this.GetVillageFactionChanges_Callback, (object) null), typeof (GetVillageFactionChanges_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetVillageFactionChanges(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetVillageFactionChanges asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetVillageFactionChanges) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        GetVillageFactionChanges_ReturnType resultData = asyncDelegate.EndInvoke(ar);
        if (resultData.sendIndex == this.GetVillageFactionChanges_Index)
        {
          this.GetVillageFactionChanges_ValidDownload = true;
          this.storeRPCresult(ar, (Common_ReturnData) resultData);
        }
        else
          this.removeRPCresult(ar);
      }
      catch (Exception ex)
      {
        GetVillageFactionChanges_ReturnType returnData = new GetVillageFactionChanges_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetVillageNames_UserCallBack(
      RemoteServices.GetVillageNames_UserCallBack callback)
    {
      this.getVillageNames_UserCallBack = callback;
    }

    public void GetVillageNames(long changePos)
    {
      if (this.GetVillageNames_Callback == null)
        this.GetVillageNames_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetVillageNames);
      RemoteServices.RemoteAsyncDelegate_GetVillageNames delegateGetVillageNames = new RemoteServices.RemoteAsyncDelegate_GetVillageNames(this.service.GetVillageNames);
      this.GetVillageNames_ValidDownload = false;
      ++this.GetVillageNames_Index;
      this.registerRPCcall(delegateGetVillageNames.BeginInvoke(this.UserID, this.SessionID, changePos, this.GetVillageNames_Index, this.GetVillageNames_Callback, (object) null), typeof (GetVillageNames_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetVillageNames(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetVillageNames asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetVillageNames) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        GetVillageNames_ReturnType resultData = asyncDelegate.EndInvoke(ar);
        if (resultData.sendIndex == this.GetVillageNames_Index)
        {
          this.GetVillageNames_ValidDownload = true;
          this.storeRPCresult(ar, (Common_ReturnData) resultData);
        }
        else
          this.removeRPCresult(ar);
      }
      catch (Exception ex)
      {
        GetVillageNames_ReturnType returnData = new GetVillageNames_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetAreaFactionChanges_UserCallBack(
      RemoteServices.GetAreaFactionChanges_UserCallBack callback)
    {
      this.getAreaFactionChanges_UserCallBack = callback;
    }

    public void GetAreaFactionChanges(
      long regionStartPos,
      long countyStartPos,
      long provinceStartPos,
      long countryStartPos,
      long parishFlagsPos,
      long countyFlagsPos,
      long provinceFlagsPos,
      long countryFlagsPos)
    {
      if (regionStartPos < -1L)
        regionStartPos = -1L;
      if (countyStartPos < -1L)
        countyStartPos = -1L;
      if (provinceStartPos < -1L)
        provinceStartPos = -1L;
      if (countryStartPos < -1L)
        countryStartPos = -1L;
      if (this.GetAreaFactionChanges_Callback == null)
        this.GetAreaFactionChanges_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetAreaFactionChanges);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetAreaFactionChanges(this.service.GetAreaFactionChanges).BeginInvoke(this.UserID, this.SessionID, regionStartPos, countyStartPos, provinceStartPos, countryStartPos, parishFlagsPos, countyFlagsPos, provinceFlagsPos, countryFlagsPos, this.GetAreaFactionChanges_Callback, (object) null), typeof (GetAreaFactionChanges_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetAreaFactionChanges(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetAreaFactionChanges asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetAreaFactionChanges) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetAreaFactionChanges_ReturnType returnData = new GetAreaFactionChanges_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetUserVillages_UserCallBack(
      RemoteServices.GetUserVillages_UserCallBack callback)
    {
      this.getUserVillages_UserCallBack = callback;
    }

    public void GetUserVillages()
    {
      if (this.GetUserVillages_Callback == null)
        this.GetUserVillages_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetUserVillages);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetUserVillages(this.service.GetUserVillages).BeginInvoke(this.UserID, this.SessionID, this.GetUserVillages_Callback, (object) null), typeof (GetUserVillages_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetUserVillages(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetUserVillages asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetUserVillages) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetUserVillages_ReturnType returnData = new GetUserVillages_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetOtherUserVillageIDList_UserCallBack(
      RemoteServices.GetOtherUserVillageIDList_UserCallBack callback)
    {
      this.getOtherUserVillageIDList_UserCallBack = callback;
    }

    public void GetOtherUserVillageIDList(string targetUser)
    {
      if (this.GetOtherUserVillageIDList_Callback == null)
        this.GetOtherUserVillageIDList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetOtherUserVillageIDList);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetOtherUserVillageIDList(this.service.GetOtherUserVillageIDList).BeginInvoke(this.UserID, targetUser, this.SessionID, this.GetOtherUserVillageIDList_Callback, (object) null), typeof (GetOtherUserVillageIDList_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetOtherUserVillageIDList(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetOtherUserVillageIDList asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetOtherUserVillageIDList) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetOtherUserVillageIDList_ReturnType returnData = new GetOtherUserVillageIDList_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_BuyVillage_UserCallBack(RemoteServices.BuyVillage_UserCallBack callback)
    {
      this.buyVillage_UserCallBack = callback;
    }

    public void BuyVillage(
      int fromVillageID,
      int villageID,
      int mapType,
      long startChangePos,
      bool peaceTime)
    {
      if (this.BuyVillage_Callback == null)
        this.BuyVillage_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_BuyVillage);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_BuyVillage(this.service.BuyVillage).BeginInvoke(this.UserID, this.SessionID, fromVillageID, villageID, mapType, startChangePos, peaceTime, this.BuyVillage_Callback, (object) null), typeof (BuyVillage_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_BuyVillage(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_BuyVillage asyncDelegate = (RemoteServices.RemoteAsyncDelegate_BuyVillage) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        BuyVillage_ReturnType returnData = new BuyVillage_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_ConvertVillage_UserCallBack(
      RemoteServices.ConvertVillage_UserCallBack callback)
    {
      this.convertVillage_UserCallBack = callback;
    }

    public void ConvertVillage(int villageID, int mapType)
    {
      if (this.ConvertVillage_Callback == null)
        this.ConvertVillage_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ConvertVillage);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ConvertVillage(this.service.ConvertVillage).BeginInvoke(this.UserID, this.SessionID, villageID, mapType, this.ConvertVillage_Callback, (object) null), typeof (ConvertVillage_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_ConvertVillage(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_ConvertVillage asyncDelegate = (RemoteServices.RemoteAsyncDelegate_ConvertVillage) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        ConvertVillage_ReturnType returnData = new ConvertVillage_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_FullTick_UserCallBack(RemoteServices.FullTick_UserCallBack callback)
    {
      this.fullTick_UserCallBack = callback;
    }

    public void FullTick(
      long startChangePos,
      long regionStartPos,
      long countyStartPos,
      long provinceStartPos,
      long countryStartPos,
      bool registerSession,
      long villageNamePos,
      long factionsChangePos,
      DateTime lastTraderTime,
      DateTime lastPeopleTime,
      long parishFlagsPos,
      long countyFlagsPos,
      long provinceFlagsPos,
      long countryFlagsPos,
      long highestArmyID,
      int mode,
      bool fullMode)
    {
      if (this.FullTick_Callback == null)
        this.FullTick_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FullTick);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FullTick(this.service.FullTick).BeginInvoke(this.UserID, this.SessionID, startChangePos, regionStartPos, countyStartPos, provinceStartPos, countryStartPos, registerSession, villageNamePos, factionsChangePos, lastTraderTime, lastPeopleTime, parishFlagsPos, countyFlagsPos, provinceFlagsPos, countryFlagsPos, highestArmyID, mode, fullMode, this.FullTick_Callback, (object) null), typeof (FullTick_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_FullTick(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_FullTick asyncDelegate = (RemoteServices.RemoteAsyncDelegate_FullTick) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        FullTick_ReturnType returnData = new FullTick_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_LeaderBoard_UserCallBack(RemoteServices.LeaderBoard_UserCallBack callback)
    {
      this.leaderBoard_UserCallBack = callback;
    }

    public void LeaderBoard(int mode)
    {
      if (this.LeaderBoard_Callback == null)
        this.LeaderBoard_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LeaderBoard);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_LeaderBoard(this.service.LeaderBoard).BeginInvoke(this.UserID, this.SessionID, mode, -1, -1, DateTime.MinValue, this.LeaderBoard_Callback, (object) null), typeof (LeaderBoard_ReturnType));
    }

    public void LeaderBoard(int mode, int minValue, int maxValue, DateTime lastUpdate)
    {
      if (this.LeaderBoard_Callback == null)
        this.LeaderBoard_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LeaderBoard);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_LeaderBoard(this.service.LeaderBoard).BeginInvoke(this.UserID, this.SessionID, mode, minValue, maxValue, lastUpdate, this.LeaderBoard_Callback, (object) null), typeof (LeaderBoard_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_LeaderBoard(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_LeaderBoard asyncDelegate = (RemoteServices.RemoteAsyncDelegate_LeaderBoard) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        LeaderBoard_ReturnType returnData = new LeaderBoard_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_LeaderBoardSearch_UserCallBack(
      RemoteServices.LeaderBoardSearch_UserCallBack callback)
    {
      this.leaderBoardSearch_UserCallBack = callback;
    }

    public void LeaderBoardSearch(int mode, string searchString, DateTime lastUpdate)
    {
      if (this.LeaderBoardSearch_Callback == null)
        this.LeaderBoardSearch_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LeaderBoardSearch);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_LeaderBoardSearch(this.service.LeaderBoardSearch).BeginInvoke(this.UserID, this.SessionID, mode, searchString, lastUpdate, this.LeaderBoardSearch_Callback, (object) null), typeof (LeaderBoardSearch_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_LeaderBoardSearch(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_LeaderBoardSearch asyncDelegate = (RemoteServices.RemoteAsyncDelegate_LeaderBoardSearch) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        LeaderBoardSearch_ReturnType returnData = new LeaderBoardSearch_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_LogOut_UserCallBack(RemoteServices.LogOut_UserCallBack callback)
    {
      this.logOut_UserCallBack = callback;
    }

    public void LogOut(
      bool manual,
      bool autoScout,
      bool autoTrade,
      bool autoAttack,
      bool autoAttackWolf,
      bool autoAttackBandit,
      bool autoAttackAI,
      int resourceType,
      int percent,
      bool autoRecruit,
      bool autoRecruitPeasant,
      bool autoRecruitArchers,
      bool autoRecruitPikemen,
      bool autoRecruitSwordsmen,
      bool autoRecruitCatapults,
      int autoRecruitPeasant_Cap,
      int autoRecruitArchers_Cap,
      int autoRecruitPikemen_Cap,
      int autoRecruitSwordsmen_Cap,
      int autoRecruitCatapults_Cap)
    {
      if (this.service == null)
        return;
      if (this.LogOut_Callback == null)
        this.LogOut_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LogOut);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_LogOut(this.service.LogOut).BeginInvoke(this.UserID, this.SessionID, manual, autoScout, autoTrade, autoAttack, autoAttackWolf, autoAttackBandit, autoAttackAI, resourceType, percent, autoRecruit, autoRecruitPeasant, autoRecruitArchers, autoRecruitPikemen, autoRecruitSwordsmen, autoRecruitCatapults, autoRecruitPeasant_Cap, autoRecruitArchers_Cap, autoRecruitPikemen_Cap, autoRecruitSwordsmen_Cap, autoRecruitCatapults_Cap, this.LogOut_Callback, (object) null), typeof (LogOut_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_LogOut(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_LogOut asyncDelegate = (RemoteServices.RemoteAsyncDelegate_LogOut) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        LogOut_ReturnType returnData = new LogOut_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetLoginHistory_UserCallBack(
      RemoteServices.GetLoginHistory_UserCallBack callback)
    {
      this.getLoginHistory_UserCallBack = callback;
    }

    public void GetLoginHistory()
    {
      if (this.GetLoginHistory_Callback == null)
        this.GetLoginHistory_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetLoginHistory);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetLoginHistory(this.service.GetLoginHistory).BeginInvoke(this.UserID, this.SessionID, this.GetLoginHistory_Callback, (object) null), typeof (GetLoginHistory_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetLoginHistory(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetLoginHistory asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetLoginHistory) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetLoginHistory_ReturnType returnData = new GetLoginHistory_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_UserInfo_UserCallBack(RemoteServices.UserInfo_UserCallBack callback)
    {
      this.userInfo_UserCallBack = callback;
    }

    public void UserInfo(int requestedUser)
    {
      if (this.UserInfo_Callback == null)
        this.UserInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UserInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_UserInfo(this.service.UserInfo).BeginInvoke(this.UserID, this.SessionID, requestedUser, this.UserInfo_Callback, (object) null), typeof (UserInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_UserInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_UserInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_UserInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        UserInfo_ReturnType returnData = new UserInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetArmyData_UserCallBack(RemoteServices.GetArmyData_UserCallBack callback)
    {
      this.getArmyData_UserCallBack = callback;
    }

    public void GetArmyData(long highestSeen)
    {
      if (this.GetArmyData_Callback == null)
        this.GetArmyData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetArmyData);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetArmyData(this.service.GetArmyData).BeginInvoke(this.UserID, this.SessionID, highestSeen, this.GetArmyData_Callback, (object) null), typeof (GetArmyData_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetArmyData(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetArmyData asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetArmyData) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetArmyData_ReturnType returnData = new GetArmyData_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_ArmyAttack_UserCallBack(RemoteServices.ArmyAttack_UserCallBack callback)
    {
      this.armyAttack_UserCallBack = callback;
    }

    public void ArmyAttack(int armyID, int targetVillage, int attackType)
    {
      if (this.ArmyAttack_Callback == null)
        this.ArmyAttack_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ArmyAttack);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ArmyAttack(this.service.ArmyAttack).BeginInvoke(this.UserID, this.SessionID, armyID, targetVillage, attackType, this.ArmyAttack_Callback, (object) null), typeof (ArmyAttack_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_ArmyAttack(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_ArmyAttack asyncDelegate = (RemoteServices.RemoteAsyncDelegate_ArmyAttack) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        ArmyAttack_ReturnType returnData = new ArmyAttack_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_RetrieveAttackResult_UserCallBack(
      RemoteServices.RetrieveAttackResult_UserCallBack callback)
    {
      this.retrieveAttackResult_UserCallBack = callback;
    }

    public void RetrieveAttackResult(long armyID, long startChangePos)
    {
      if (this.RetrieveAttackResult_Callback == null)
        this.RetrieveAttackResult_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RetrieveAttackResult);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_RetrieveAttackResult(this.service.RetrieveAttackResult).BeginInvoke(this.UserID, this.SessionID, armyID, startChangePos, this.RetrieveAttackResult_Callback, (object) null), typeof (RetrieveAttackResult_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_RetrieveAttackResult(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_RetrieveAttackResult asyncDelegate = (RemoteServices.RemoteAsyncDelegate_RetrieveAttackResult) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        RetrieveAttackResult_ReturnType returnData = new RetrieveAttackResult_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SetAdminMessage_UserCallBack(
      RemoteServices.SetAdminMessage_UserCallBack callback)
    {
      this.setAdminMessage_UserCallBack = callback;
    }

    public void SetAdminMessage(string message, int type)
    {
      if (this.SetAdminMessage_Callback == null)
        this.SetAdminMessage_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SetAdminMessage);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SetAdminMessage(this.service.SetAdminMessage).BeginInvoke(this.UserID, this.SessionID, message, type, this.SetAdminMessage_Callback, (object) null), typeof (SetAdminMessage_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SetAdminMessage(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SetAdminMessage asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SetAdminMessage) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SetAdminMessage_ReturnType returnData = new SetAdminMessage_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CompleteVillageCastle_UserCallBack(
      RemoteServices.CompleteVillageCastle_UserCallBack callback)
    {
      this.completeVillageCastle_UserCallBack = callback;
    }

    public void CompleteVillageCastle(int villageID, int mode)
    {
      if (this.CompleteVillageCastle_Callback == null)
        this.CompleteVillageCastle_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CompleteVillageCastle);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CompleteVillageCastle(this.service.CompleteVillageCastle).BeginInvoke(this.UserID, this.SessionID, villageID, mode, this.CompleteVillageCastle_Callback, (object) null), typeof (CompleteVillageCastle_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CompleteVillageCastle(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CompleteVillageCastle asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CompleteVillageCastle) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CompleteVillageCastle_ReturnType returnData = new CompleteVillageCastle_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_RetrieveStats_UserCallBack(RemoteServices.RetrieveStats_UserCallBack callback)
    {
      this.retrieveStats_UserCallBack = callback;
    }

    public void RetrieveStats()
    {
      if (this.RetrieveStats_Callback == null)
        this.RetrieveStats_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RetrieveStats);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_RetrieveStats(this.service.RetrieveStats).BeginInvoke(this.UserID, this.SessionID, this.RetrieveStats_Callback, (object) null), typeof (RetrieveStats_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_RetrieveStats(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_RetrieveStats asyncDelegate = (RemoteServices.RemoteAsyncDelegate_RetrieveStats) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        RetrieveStats_ReturnType returnData = new RetrieveStats_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_RetrieveStats2_UserCallBack(
      RemoteServices.RetrieveStats2_UserCallBack callback)
    {
      this.retrieveStats2_UserCallBack = callback;
    }

    public void RetrieveStats2()
    {
      if (this.RetrieveStats2_Callback == null)
        this.RetrieveStats2_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RetrieveStats2);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_RetrieveStats2(this.service.RetrieveStats2).BeginInvoke(this.UserID, this.SessionID, this.RetrieveStats2_Callback, (object) null), typeof (RetrieveStats2_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_RetrieveStats2(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_RetrieveStats2 asyncDelegate = (RemoteServices.RemoteAsyncDelegate_RetrieveStats2) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        RetrieveStats2_ReturnType returnData = new RetrieveStats2_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetAdminStats_UserCallBack(RemoteServices.GetAdminStats_UserCallBack callback)
    {
      this.getAdminStats_UserCallBack = callback;
    }

    public void GetAdminStats()
    {
      if (this.GetAdminStats_Callback == null)
        this.GetAdminStats_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetAdminStats);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetAdminStats(this.service.GetAdminStats).BeginInvoke(this.UserID, this.SessionID, this.GetAdminStats_Callback, (object) null), typeof (GetAdminStats_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetAdminStats(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetAdminStats asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetAdminStats) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetAdminStats_ReturnType returnData = new GetAdminStats_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetReportsList_UserCallBack(
      RemoteServices.GetReportsList_UserCallBack callback)
    {
      this.getReportsList_UserCallBack = callback;
    }

    public void GetReportsList(int readFilter, long clientHighest)
    {
      if (this.GetReportsList_Callback == null)
        this.GetReportsList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetReportsList);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetReportsList(this.service.GetReportsList).BeginInvoke(this.UserID, this.SessionID, readFilter, (int[]) null, -1L, clientHighest, this.GetReportsList_Callback, (object) null), typeof (GetReportsList_ReturnType));
    }

    public void GetReportsList(int readFilter, int[] typeFilters, long folderID)
    {
      if (this.GetReportsList_Callback == null)
        this.GetReportsList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetReportsList);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetReportsList(this.service.GetReportsList).BeginInvoke(this.UserID, this.SessionID, readFilter, typeFilters, folderID, -1L, this.GetReportsList_Callback, (object) null), typeof (GetReportsList_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetReportsList(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetReportsList asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetReportsList) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetReportsList_ReturnType returnData = new GetReportsList_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetReport_UserCallBack(RemoteServices.GetReport_UserCallBack callback)
    {
      this.getReport_UserCallBack = callback;
    }

    public void GetReport(long reportID)
    {
      if (this.GetReport_Callback == null)
        this.GetReport_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetReport);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetReport(this.service.GetReport).BeginInvoke(this.UserID, this.SessionID, reportID, this.GetReport_Callback, (object) null), typeof (GetReport_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetReport(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetReport asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetReport) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetReport_ReturnType returnData = new GetReport_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_ForwardReport_UserCallBack(RemoteServices.ForwardReport_UserCallBack callback)
    {
      this.forwardReport_UserCallBack = callback;
    }

    public void ForwardReport(long reportID, string[] recipients)
    {
      if (this.ForwardReport_Callback == null)
        this.ForwardReport_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ForwardReport);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ForwardReport(this.service.ForwardReport).BeginInvoke(this.UserID, this.SessionID, reportID, recipients, this.ForwardReport_Callback, (object) null), typeof (ForwardReport_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_ForwardReport(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_ForwardReport asyncDelegate = (RemoteServices.RemoteAsyncDelegate_ForwardReport) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        ForwardReport_ReturnType returnData = new ForwardReport_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_ViewBattle_UserCallBack(RemoteServices.ViewBattle_UserCallBack callback)
    {
      this.viewBattle_UserCallBack = callback;
    }

    public void ViewBattle(long reportID)
    {
      if (this.ViewBattle_Callback == null)
        this.ViewBattle_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ViewBattle);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ViewBattle(this.service.ViewBattle).BeginInvoke(this.UserID, this.SessionID, reportID, this.ViewBattle_Callback, (object) null), typeof (ViewBattle_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_ViewBattle(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_ViewBattle asyncDelegate = (RemoteServices.RemoteAsyncDelegate_ViewBattle) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        ViewBattle_ReturnType returnData = new ViewBattle_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_ViewCastle_UserCallBack(RemoteServices.ViewCastle_UserCallBack callback)
    {
      this.viewCastle_UserCallBack = callback;
    }

    public void ViewCastle_Report(long reportID)
    {
      if (this.ViewCastle_Callback == null)
        this.ViewCastle_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ViewCastle);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ViewCastle(this.service.ViewCastle).BeginInvoke(this.UserID, this.SessionID, -1, reportID, this.ViewCastle_Callback, (object) null), typeof (ViewCastle_ReturnType));
    }

    public void ViewCastle_Village(int villageID)
    {
      if (this.ViewCastle_Callback == null)
        this.ViewCastle_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ViewCastle);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ViewCastle(this.service.ViewCastle).BeginInvoke(this.UserID, this.SessionID, villageID, -1L, this.ViewCastle_Callback, (object) null), typeof (ViewCastle_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_ViewCastle(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_ViewCastle asyncDelegate = (RemoteServices.RemoteAsyncDelegate_ViewCastle) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        ViewCastle_ReturnType returnData = new ViewCastle_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_DeleteOrMoveReports_UserCallBack(
      RemoteServices.DeleteReports_UserCallBack callback)
    {
      this.deleteReports_UserCallBack = callback;
    }

    public void DeleteReports(long[] reportsToDelete)
    {
      if (this.DeleteReports_Callback == null)
        this.DeleteReports_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteReports);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DeleteReports(this.service.DeleteOrMoveReports).BeginInvoke(this.UserID, this.SessionID, 0, reportsToDelete, -1L, this.DeleteReports_Callback, (object) null), typeof (DeleteReports_ReturnType));
    }

    public void MoveReports(long[] reportsToDelete, long folderID)
    {
      if (this.DeleteReports_Callback == null)
        this.DeleteReports_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteReports);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DeleteReports(this.service.DeleteOrMoveReports).BeginInvoke(this.UserID, this.SessionID, 1, reportsToDelete, folderID, this.DeleteReports_Callback, (object) null), typeof (DeleteReports_ReturnType));
    }

    public void MarkReportsRead(long[] reportsToMark)
    {
      if (this.DeleteReports_Callback == null)
        this.DeleteReports_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteReports);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DeleteReports(this.service.DeleteOrMoveReports).BeginInvoke(this.UserID, this.SessionID, 2, reportsToMark, -1L, this.DeleteReports_Callback, (object) null), typeof (DeleteReports_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_DeleteReports(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_DeleteReports asyncDelegate = (RemoteServices.RemoteAsyncDelegate_DeleteReports) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        DeleteReports_ReturnType returnData = new DeleteReports_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_UpdateReportFilters_UserCallBack(
      RemoteServices.UpdateReportFilters_UserCallBack callback)
    {
      this.updateReportFilters_UserCallBack = callback;
    }

    public void UpdateReportFilters(ReportFilterList filters)
    {
      if (this.UpdateReportFilters_Callback == null)
        this.UpdateReportFilters_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpdateReportFilters);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_UpdateReportFilters(this.service.UpdateReportFilters).BeginInvoke(this.UserID, this.SessionID, filters, this.UpdateReportFilters_Callback, (object) null), typeof (UpdateReportFilters_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_UpdateReportFilters(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_UpdateReportFilters asyncDelegate = (RemoteServices.RemoteAsyncDelegate_UpdateReportFilters) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        UpdateReportFilters_ReturnType returnData = new UpdateReportFilters_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_UpdateUserOptions_UserCallBack(
      RemoteServices.UpdateUserOptions_UserCallBack callback)
    {
      this.updateUserOptions_UserCallBack = callback;
    }

    public void UpdateUserOptions(GameOptionsData options)
    {
      if (this.UpdateUserOptions_Callback == null)
        this.UpdateUserOptions_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpdateUserOptions);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_UpdateUserOptions(this.service.UpdateUserOptions).BeginInvoke(this.UserID, this.SessionID, options, this.UpdateUserOptions_Callback, (object) null), typeof (UpdateUserOptions_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_UpdateUserOptions(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_UpdateUserOptions asyncDelegate = (RemoteServices.RemoteAsyncDelegate_UpdateUserOptions) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        UpdateUserOptions_ReturnType returnData = new UpdateUserOptions_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_ManageReportFolders_UserCallBack(
      RemoteServices.ManageReportFolders_UserCallBack callback)
    {
      this.manageReportFolders_UserCallBack = callback;
    }

    public void getReportFolders()
    {
      if (this.ManageReportFolders_Callback == null)
        this.ManageReportFolders_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ManageReportFolders);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ManageReportFolders(this.service.ManageReportFolders).BeginInvoke(this.UserID, this.SessionID, 0, 0L, "", this.ManageReportFolders_Callback, (object) null), typeof (ManageReportFolders_ReturnType));
    }

    public void addReportFolder(string folderName)
    {
      if (this.ManageReportFolders_Callback == null)
        this.ManageReportFolders_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ManageReportFolders);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ManageReportFolders(this.service.ManageReportFolders).BeginInvoke(this.UserID, this.SessionID, 1, 0L, folderName, this.ManageReportFolders_Callback, (object) null), typeof (ManageReportFolders_ReturnType));
    }

    public void deleteReportFolder(long folderID, int mode)
    {
      if (this.ManageReportFolders_Callback == null)
        this.ManageReportFolders_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ManageReportFolders);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ManageReportFolders(this.service.ManageReportFolders).BeginInvoke(this.UserID, this.SessionID, mode, folderID, "", this.ManageReportFolders_Callback, (object) null), typeof (ManageReportFolders_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_ManageReportFolders(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_ManageReportFolders asyncDelegate = (RemoteServices.RemoteAsyncDelegate_ManageReportFolders) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        ManageReportFolders_ReturnType returnData = new ManageReportFolders_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetMailThreadList_UserCallBack(
      RemoteServices.GetMailThreadList_UserCallBack callback)
    {
      this.getMailThreadList_UserCallBack = callback;
    }

    public void GetMailThreadList(bool initialRequest, int mode, DateTime lastRetrieved)
    {
      if (this.GetMailThreadList_Callback == null)
        this.GetMailThreadList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetMailThreadList);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetMailThreadList(this.service.GetMailThreadList).BeginInvoke(this.UserID, this.SessionID, initialRequest, mode, lastRetrieved, this.GetMailThreadList_Callback, (object) null), typeof (GetMailThreadList_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetMailThreadList(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetMailThreadList asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetMailThreadList) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetMailThreadList_ReturnType returnData = new GetMailThreadList_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetMailThread_UserCallBack(RemoteServices.GetMailThread_UserCallBack callback)
    {
      this.getMailThread_UserCallBack = callback;
    }

    public void GetMailThread(long threadID, int localCount, long highestSegmentID)
    {
      if (this.GetMailThread_Callback == null)
        this.GetMailThread_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetMailThread);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetMailThread(this.service.GetMailThread).BeginInvoke(this.UserID, this.SessionID, threadID, localCount, highestSegmentID, this.GetMailThread_Callback, (object) null), typeof (GetMailThread_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetMailThread(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetMailThread asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetMailThread) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetMailThread_ReturnType returnData = new GetMailThread_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetMailFolders_UserCallBack(
      RemoteServices.GetMailFolders_UserCallBack callback)
    {
      this.getMailFolders_UserCallBack = callback;
    }

    public void GetMailFolders()
    {
      if (this.GetMailFolders_Callback == null)
        this.GetMailFolders_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetMailFolders);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetMailFolders(this.service.GetMailFolders).BeginInvoke(this.UserID, this.SessionID, this.GetMailFolders_Callback, (object) null), typeof (GetMailFolders_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetMailFolders(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetMailFolders asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetMailFolders) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetMailFolders_ReturnType returnData = new GetMailFolders_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CreateMailFolder_UserCallBack(
      RemoteServices.CreateMailFolder_UserCallBack callback)
    {
      this.createMailFolder_UserCallBack = callback;
    }

    public void CreateMailFolder(string folderName)
    {
      if (this.CreateMailFolder_Callback == null)
        this.CreateMailFolder_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CreateMailFolder);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CreateMailFolder(this.service.CreateMailFolder).BeginInvoke(this.UserID, this.SessionID, folderName, this.CreateMailFolder_Callback, (object) null), typeof (CreateMailFolder_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CreateMailFolder(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CreateMailFolder asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CreateMailFolder) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CreateMailFolder_ReturnType returnData = new CreateMailFolder_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_MoveToMailFolder_UserCallBack(
      RemoteServices.MoveToMailFolder_UserCallBack callback)
    {
      this.moveToMailFolder_UserCallBack = callback;
    }

    public void MoveToMailFolder(long threadID, long folderID)
    {
      if (this.MoveToMailFolder_Callback == null)
        this.MoveToMailFolder_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MoveToMailFolder);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_MoveToMailFolder(this.service.MoveToMailFolder).BeginInvoke(this.UserID, this.SessionID, threadID, folderID, this.MoveToMailFolder_Callback, (object) null), typeof (MoveToMailFolder_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_MoveToMailFolder(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_MoveToMailFolder asyncDelegate = (RemoteServices.RemoteAsyncDelegate_MoveToMailFolder) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        MoveToMailFolder_ReturnType returnData = new MoveToMailFolder_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_RemoveMailFolder_UserCallBack(
      RemoteServices.RemoveMailFolder_UserCallBack callback)
    {
      this.removeMailFolder_UserCallBack = callback;
    }

    public void RemoveMailFolder(long folderID)
    {
      if (this.RemoveMailFolder_Callback == null)
        this.RemoveMailFolder_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RemoveMailFolder);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_RemoveMailFolder(this.service.RemoveMailFolder).BeginInvoke(this.UserID, this.SessionID, folderID, this.RemoveMailFolder_Callback, (object) null), typeof (RemoveMailFolder_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_RemoveMailFolder(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_RemoveMailFolder asyncDelegate = (RemoteServices.RemoteAsyncDelegate_RemoveMailFolder) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        RemoveMailFolder_ReturnType returnData = new RemoveMailFolder_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_ReportMail_UserCallBack(RemoteServices.ReportMail_UserCallBack callback)
    {
      this.reportMail_UserCallBack = callback;
    }

    public void ReportMail(long mailID, long threadID, string reason, string summary)
    {
      if (this.ReportMail_Callback == null)
        this.ReportMail_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ReportMail);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ReportMail(this.service.ReportMail).BeginInvoke(this.UserID, this.SessionID, mailID, threadID, reason, summary, this.ReportMail_Callback, (object) null), typeof (ReportMail_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_ReportMail(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_ReportMail asyncDelegate = (RemoteServices.RemoteAsyncDelegate_ReportMail) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        ReportMail_ReturnType returnData = new ReportMail_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_FlagMailRead_UserCallBack(RemoteServices.FlagMailRead_UserCallBack callback)
    {
      this.flagMailRead_UserCallBack = callback;
    }

    public void FlagMailRead(long mailID)
    {
      if (this.FlagMailRead_Callback == null)
        this.FlagMailRead_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FlagMailRead);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FlagMailRead(this.service.FlagMailRead).BeginInvoke(this.UserID, this.SessionID, mailID, -1L, true, this.FlagMailRead_Callback, (object) null), typeof (FlagMailRead_ReturnType));
    }

    public void FlagThreadRead(long threadID)
    {
      if (this.FlagMailRead_Callback == null)
        this.FlagMailRead_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FlagMailRead);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FlagMailRead(this.service.FlagMailRead).BeginInvoke(this.UserID, this.SessionID, -1L, threadID, true, this.FlagMailRead_Callback, (object) null), typeof (FlagMailRead_ReturnType));
    }

    public void FlagThreadUnread(long threadID)
    {
      if (this.FlagMailRead_Callback == null)
        this.FlagMailRead_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FlagMailRead);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FlagMailRead(this.service.FlagMailRead).BeginInvoke(this.UserID, this.SessionID, -1L, threadID, false, this.FlagMailRead_Callback, (object) null), typeof (FlagMailRead_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_FlagMailRead(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_FlagMailRead asyncDelegate = (RemoteServices.RemoteAsyncDelegate_FlagMailRead) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        FlagMailRead_ReturnType returnData = new FlagMailRead_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SendMail_UserCallBack(RemoteServices.SendMail_UserCallBack callback)
    {
      this.sendMail_UserCallBack = callback;
    }

    public void SendMail(
      string subject,
      string body,
      string[] recipients,
      long threadID,
      bool forwardThread)
    {
      if (this.SendMail_Callback == null)
        this.SendMail_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendMail);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SendMail(this.service.SendMail).BeginInvoke(this.UserID, this.SessionID, subject, body, recipients, threadID, forwardThread, this.SendMail_Callback, (object) null), typeof (SendMail_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SendMail(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SendMail asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SendMail) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SendMail_ReturnType returnData = new SendMail_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SendSpecialMail_UserCallBack(
      RemoteServices.SendSpecialMail_UserCallBack callback)
    {
      this.sendSpecialMail_UserCallBack = callback;
    }

    public void SendSpecialMail(int mailType, int area, string subject, string body)
    {
      if (this.SendSpecialMail_Callback == null)
        this.SendSpecialMail_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendSpecialMail);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SendSpecialMail(this.service.SendSpecialMail).BeginInvoke(this.UserID, this.SessionID, mailType, area, subject, body, this.SendSpecialMail_Callback, (object) null), typeof (SendSpecialMail_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SendSpecialMail(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SendSpecialMail asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SendSpecialMail) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SendSpecialMail_ReturnType returnData = new SendSpecialMail_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_DeleteMailThread_UserCallBack(
      RemoteServices.DeleteMailThread_UserCallBack callback)
    {
      this.deleteMailThread_UserCallBack = callback;
    }

    public void DeleteMailThread(long threadID)
    {
      if (this.DeleteMailThread_Callback == null)
        this.DeleteMailThread_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteMailThread);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DeleteMailThread(this.service.DeleteMailThread).BeginInvoke(this.UserID, this.SessionID, threadID, this.DeleteMailThread_Callback, (object) null), typeof (DeleteMailThread_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_DeleteMailThread(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_DeleteMailThread asyncDelegate = (RemoteServices.RemoteAsyncDelegate_DeleteMailThread) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        DeleteMailThread_ReturnType returnData = new DeleteMailThread_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetMailRecipientsHistory_UserCallBack(
      RemoteServices.GetMailRecipientsHistory_UserCallBack callback)
    {
      this.getMailRecipientsHistory_UserCallBack = callback;
    }

    public void GetMailRecipientsHistory()
    {
      if (this.GetMailRecipientsHistory_Callback == null)
        this.GetMailRecipientsHistory_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetMailRecipientsHistory);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetMailRecipientsHistory(this.service.GetMailRecipientsHistory).BeginInvoke(this.UserID, this.SessionID, this.GetMailRecipientsHistory_Callback, (object) null), typeof (GetMailRecipientsHistory_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetMailRecipientsHistory(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetMailRecipientsHistory asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetMailRecipientsHistory) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetMailRecipientsHistory_ReturnType returnData = new GetMailRecipientsHistory_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetMailUserSearch_UserCallBack(
      RemoteServices.GetMailUserSearch_UserCallBack callback)
    {
      this.getMailUserSearch_UserCallBack = callback;
    }

    public void GetMailUserSearch(string filter)
    {
      if (this.GetMailUserSearch_Callback == null)
        this.GetMailUserSearch_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetMailUserSearch);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetMailUserSearch(this.service.GetMailUserSearch).BeginInvoke(this.UserID, this.SessionID, filter, this.GetMailUserSearch_Callback, (object) null), typeof (GetMailUserSearch_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetMailUserSearch(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetMailUserSearch asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetMailUserSearch) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetMailUserSearch_ReturnType returnData = new GetMailUserSearch_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_AddUserToFavourites_UserCallBack(
      RemoteServices.AddUserToFavourites_UserCallBack callback)
    {
      this.addUserToFavourites_UserCallBack = callback;
    }

    public void AddUserToFavourites(string userName)
    {
      if (this.AddUserToFavourites_Callback == null)
        this.AddUserToFavourites_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_AddUserToFavourites);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_AddUserToFavourites(this.service.AddUserToFavourites).BeginInvoke(this.UserID, this.SessionID, userName, false, this.AddUserToFavourites_Callback, (object) null), typeof (AddUserToFavourites_ReturnType));
    }

    public void RemoveUserFromFavourites(string userName)
    {
      if (this.AddUserToFavourites_Callback == null)
        this.AddUserToFavourites_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_AddUserToFavourites);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_AddUserToFavourites(this.service.AddUserToFavourites).BeginInvoke(this.UserID, this.SessionID, userName, true, this.AddUserToFavourites_Callback, (object) null), typeof (AddUserToFavourites_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_AddUserToFavourites(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_AddUserToFavourites asyncDelegate = (RemoteServices.RemoteAsyncDelegate_AddUserToFavourites) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        AddUserToFavourites_ReturnType returnData = new AddUserToFavourites_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetHistoricalData_UserCallBack(
      RemoteServices.GetHistoricalData_UserCallBack callback)
    {
      this.getHistoricalData_UserCallBack = callback;
    }

    public void GetHistoricalData()
    {
      if (this.GetHistoricalData_Callback == null)
        this.GetHistoricalData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetHistoricalData);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetHistoricalData(this.service.GetHistoricalData).BeginInvoke(this.UserID, this.SessionID, this.GetHistoricalData_Callback, (object) null), typeof (GetHistoricalData_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetHistoricalData(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetHistoricalData asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetHistoricalData) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetHistoricalData_ReturnType returnData = new GetHistoricalData_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetResourceLevel_UserCallBack(
      RemoteServices.GetResourceLevel_UserCallBack callback)
    {
      this.getResourceLevel_UserCallBack = callback;
    }

    public void GetResourceLevel(int villageID, int buildingType)
    {
      if (this.GetResourceLevel_Callback == null)
        this.GetResourceLevel_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetResourceLevel);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetResourceLevel(this.service.GetResourceLevel).BeginInvoke(this.UserID, this.SessionID, villageID, buildingType, this.GetResourceLevel_Callback, (object) null), typeof (GetResourceLevel_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetResourceLevel(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetResourceLevel asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetResourceLevel) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetResourceLevel_ReturnType returnData = new GetResourceLevel_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetVillageBuildingsList_UserCallBack(
      RemoteServices.GetVillageBuildingsList_UserCallBack callback)
    {
      this.getVillageBuildingsList_UserCallBack = callback;
    }

    public void GetVillageBuildingsList(int villageID, bool fullUpdate, bool needParishPeople)
    {
      if (this.GetVillageBuildingsList_Callback == null)
        this.GetVillageBuildingsList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetVillageBuildingsList);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetVillageBuildingsList(this.service.GetVillageBuildingsList).BeginInvoke(this.UserID, this.SessionID, villageID, fullUpdate, false, needParishPeople, this.GetVillageBuildingsList_Callback, (object) null), typeof (GetVillageBuildingsList_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetVillageBuildingsList(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetVillageBuildingsList asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetVillageBuildingsList) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetVillageBuildingsList_ReturnType returnData = new GetVillageBuildingsList_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_PlaceVillageBuilding_UserCallBack(
      RemoteServices.PlaceVillageBuilding_UserCallBack callback)
    {
      this.placeVillageBuilding_UserCallBack = callback;
    }

    public void PlaceVillageBuilding(int villageID, int buildingType, Point buildingLocation)
    {
      if (this.PlaceVillageBuilding_Callback == null)
        this.PlaceVillageBuilding_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_PlaceVillageBuilding);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_PlaceVillageBuilding(this.service.PlaceVillageBuilding).BeginInvoke(this.UserID, this.SessionID, villageID, buildingType, buildingLocation, this.PlaceVillageBuilding_Callback, (object) null), typeof (PlaceVillageBuilding_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_PlaceVillageBuilding(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_PlaceVillageBuilding asyncDelegate = (RemoteServices.RemoteAsyncDelegate_PlaceVillageBuilding) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        PlaceVillageBuilding_ReturnType returnData = new PlaceVillageBuilding_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_DeleteVillageBuilding_UserCallBack(
      RemoteServices.DeleteVillageBuilding_UserCallBack callback)
    {
      this.deleteVillageBuilding_UserCallBack = callback;
    }

    public void DeleteVillageBuilding(int villageID, long buildingID)
    {
      if (this.DeleteVillageBuilding_Callback == null)
        this.DeleteVillageBuilding_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteVillageBuilding);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DeleteVillageBuilding(this.service.DeleteVillageBuilding).BeginInvoke(this.UserID, this.SessionID, villageID, buildingID, this.DeleteVillageBuilding_Callback, (object) null), typeof (DeleteVillageBuilding_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_DeleteVillageBuilding(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_DeleteVillageBuilding asyncDelegate = (RemoteServices.RemoteAsyncDelegate_DeleteVillageBuilding) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        DeleteVillageBuilding_ReturnType returnData = new DeleteVillageBuilding_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CancelDeleteVillageBuilding_UserCallBack(
      RemoteServices.CancelDeleteVillageBuilding_UserCallBack callback)
    {
      this.cancelDeleteVillageBuilding_UserCallBack = callback;
    }

    public void CancelDeleteVillageBuilding(int villageID, long buildingID)
    {
      if (this.CancelDeleteVillageBuilding_Callback == null)
        this.CancelDeleteVillageBuilding_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CancelDeleteVillageBuilding);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CancelDeleteVillageBuilding(this.service.CancelDeleteVillageBuilding).BeginInvoke(this.UserID, this.SessionID, villageID, buildingID, this.CancelDeleteVillageBuilding_Callback, (object) null), typeof (CancelDeleteVillageBuilding_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CancelDeleteVillageBuilding(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CancelDeleteVillageBuilding asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CancelDeleteVillageBuilding) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CancelDeleteVillageBuilding_ReturnType returnData = new CancelDeleteVillageBuilding_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_MoveVillageBuilding_UserCallBack(
      RemoteServices.MoveVillageBuilding_UserCallBack callback)
    {
      this.moveVillageBuilding_UserCallBack = callback;
    }

    public void MoveVillageBuilding(int villageID, long buildingID, Point buildingLocation)
    {
      if (this.MoveVillageBuilding_Callback == null)
        this.MoveVillageBuilding_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MoveVillageBuilding);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_MoveVillageBuilding(this.service.MoveVillageBuilding).BeginInvoke(this.UserID, this.SessionID, villageID, buildingID, buildingLocation, this.MoveVillageBuilding_Callback, (object) null), typeof (MoveVillageBuilding_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_MoveVillageBuilding(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_MoveVillageBuilding asyncDelegate = (RemoteServices.RemoteAsyncDelegate_MoveVillageBuilding) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        MoveVillageBuilding_ReturnType returnData = new MoveVillageBuilding_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_VillageBuildingCompleteDataRetrieval_UserCallBack(
      RemoteServices.VillageBuildingCompleteDataRetrieval_UserCallBack callback)
    {
      this.villageBuildingCompleteDataRetrieval_UserCallBack = callback;
    }

    public void VillageBuildingCompleteDataRetrieval(int villageID, long buildingID)
    {
      if (this.VillageBuildingCompleteDataRetrieval_Callback == null)
        this.VillageBuildingCompleteDataRetrieval_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageBuildingCompleteDataRetrieval);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_VillageBuildingCompleteDataRetrieval(this.service.VillageBuildingCompleteDataRetrieval).BeginInvoke(this.UserID, this.SessionID, villageID, buildingID, 0, this.VillageBuildingCompleteDataRetrieval_Callback, (object) null), typeof (VillageBuildingCompleteDataRetrieval_ReturnType));
    }

    public void VillageBuildingDeleteDataRetrieval(int villageID, long buildingID)
    {
      if (this.VillageBuildingCompleteDataRetrieval_Callback == null)
        this.VillageBuildingCompleteDataRetrieval_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageBuildingCompleteDataRetrieval);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_VillageBuildingCompleteDataRetrieval(this.service.VillageBuildingCompleteDataRetrieval).BeginInvoke(this.UserID, this.SessionID, villageID, buildingID, 1, this.VillageBuildingCompleteDataRetrieval_Callback, (object) null), typeof (VillageBuildingCompleteDataRetrieval_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_VillageBuildingCompleteDataRetrieval(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_VillageBuildingCompleteDataRetrieval asyncDelegate = (RemoteServices.RemoteAsyncDelegate_VillageBuildingCompleteDataRetrieval) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        VillageBuildingCompleteDataRetrieval_ReturnType returnData = new VillageBuildingCompleteDataRetrieval_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_VillageBuildingSetActive_UserCallBack(
      RemoteServices.VillageBuildingSetActive_UserCallBack callback)
    {
      this.villageBuildingSetActive_UserCallBack = callback;
    }

    public void VillageBuildingSetActive(int villageID, long buildingID, bool state)
    {
      if (this.VillageBuildingSetActive_Callback == null)
        this.VillageBuildingSetActive_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageBuildingSetActive);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_VillageBuildingSetActive(this.service.VillageBuildingSetActive).BeginInvoke(this.UserID, this.SessionID, buildingID, villageID, -1, state, this.VillageBuildingSetActive_Callback, (object) null), typeof (VillageBuildingSetActive_ReturnType));
    }

    public void VillageBuildingTypeSetActive(int villageID, int buildingType, bool state)
    {
      if (this.VillageBuildingSetActive_Callback == null)
        this.VillageBuildingSetActive_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageBuildingSetActive);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_VillageBuildingSetActive(this.service.VillageBuildingSetActive).BeginInvoke(this.UserID, this.SessionID, -1L, villageID, buildingType, state, this.VillageBuildingSetActive_Callback, (object) null), typeof (VillageBuildingSetActive_ReturnType));
    }

    public void VillageAllBuildingsSetActive(int villageID, bool state)
    {
      if (this.VillageBuildingSetActive_Callback == null)
        this.VillageBuildingSetActive_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageBuildingSetActive);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_VillageBuildingSetActive(this.service.VillageBuildingSetActive).BeginInvoke(this.UserID, this.SessionID, -1L, villageID, -1, state, this.VillageBuildingSetActive_Callback, (object) null), typeof (VillageBuildingSetActive_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_VillageBuildingSetActive(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_VillageBuildingSetActive asyncDelegate = (RemoteServices.RemoteAsyncDelegate_VillageBuildingSetActive) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        VillageBuildingSetActive_ReturnType returnData = new VillageBuildingSetActive_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_VillageBuildingChangeRates_UserCallBack(
      RemoteServices.VillageBuildingChangeRates_UserCallBack callback)
    {
      this.villageBuildingChangeRates_UserCallBack = callback;
    }

    public void VillageBuildingChangeRates(
      int villageID,
      int taxLevel,
      int rationsLevel,
      int aleRationsLevel,
      int capitalTaxRate)
    {
      if (this.VillageBuildingChangeRates_Callback == null)
        this.VillageBuildingChangeRates_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageBuildingChangeRates);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_VillageBuildingChangeRates(this.service.VillageBuildingChangeRates).BeginInvoke(this.UserID, this.SessionID, villageID, taxLevel, rationsLevel, aleRationsLevel, capitalTaxRate, this.VillageBuildingChangeRates_Callback, (object) null), typeof (VillageBuildingChangeRates_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_VillageBuildingChangeRates(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_VillageBuildingChangeRates asyncDelegate = (RemoteServices.RemoteAsyncDelegate_VillageBuildingChangeRates) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        VillageBuildingChangeRates_ReturnType returnData = new VillageBuildingChangeRates_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_VillageRename_UserCallBack(RemoteServices.VillageRename_UserCallBack callback)
    {
      this.villageRename_UserCallBack = callback;
    }

    public void VillageRename(int villageID, string villageName)
    {
      if (this.VillageRename_Callback == null)
        this.VillageRename_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageRename);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_VillageRename(this.service.VillageRename).BeginInvoke(this.UserID, this.SessionID, villageID, villageName, false, false, this.VillageRename_Callback, (object) null), typeof (VillageRename_ReturnType));
    }

    public void VillageResetName(int villageID)
    {
      if (this.VillageRename_Callback == null)
        this.VillageRename_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageRename);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_VillageRename(this.service.VillageRename).BeginInvoke(this.UserID, this.SessionID, villageID, "DoReset", false, true, this.VillageRename_Callback, (object) null), typeof (VillageRename_ReturnType));
    }

    public void VillageAbandon(int villageID)
    {
      if (this.VillageRename_Callback == null)
        this.VillageRename_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageRename);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_VillageRename(this.service.VillageRename).BeginInvoke(this.UserID, this.SessionID, villageID, "DoAbandon", true, false, this.VillageRename_Callback, (object) null), typeof (VillageRename_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_VillageRename(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_VillageRename asyncDelegate = (RemoteServices.RemoteAsyncDelegate_VillageRename) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        VillageRename_ReturnType returnData = new VillageRename_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_VillageProduceWeapons_UserCallBack(
      RemoteServices.VillageProduceWeapons_UserCallBack callback)
    {
      this.villageProduceWeapons_UserCallBack = callback;
    }

    public void VillageProduceWeapons(int villageID, int weaponType, int amount)
    {
      if (this.VillageProduceWeapons_Callback == null)
        this.VillageProduceWeapons_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageProduceWeapons);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_VillageProduceWeapons(this.service.VillageProduceWeapons).BeginInvoke(this.UserID, this.SessionID, villageID, weaponType, amount, this.VillageProduceWeapons_Callback, (object) null), typeof (VillageProduceWeapons_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_VillageProduceWeapons(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_VillageProduceWeapons asyncDelegate = (RemoteServices.RemoteAsyncDelegate_VillageProduceWeapons) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        VillageProduceWeapons_ReturnType returnData = new VillageProduceWeapons_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_VillageHoldBanquet_UserCallBack(
      RemoteServices.VillageHoldBanquet_UserCallBack callback)
    {
      this.villageHoldBanquet_UserCallBack = callback;
    }

    public void VillageHoldBanquet(
      int villageID,
      int venison,
      int furniture,
      int metalwork,
      int clothing,
      int wine,
      int salt,
      int spice,
      int silk)
    {
      if (this.VillageHoldBanquet_Callback == null)
        this.VillageHoldBanquet_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageHoldBanquet);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_VillageHoldBanquet(this.service.VillageHoldBanquet).BeginInvoke(this.UserID, this.SessionID, villageID, venison, wine, salt, spice, silk, clothing, furniture, metalwork, this.VillageHoldBanquet_Callback, (object) null), typeof (VillageHoldBanquet_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_VillageHoldBanquet(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_VillageHoldBanquet asyncDelegate = (RemoteServices.RemoteAsyncDelegate_VillageHoldBanquet) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        VillageHoldBanquet_ReturnType returnData = new VillageHoldBanquet_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetCastle_UserCallBack(RemoteServices.GetCastle_UserCallBack callback)
    {
      this.getCastle_UserCallBack = callback;
    }

    public void GetCastle(int villageID)
    {
      if (this.GetCastle_Callback == null)
        this.GetCastle_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetCastle);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetCastle(this.service.GetCastle).BeginInvoke(this.UserID, this.SessionID, villageID, this.GetCastle_Callback, (object) null), typeof (GetCastle_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetCastle(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetCastle asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetCastle) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetCastle_ReturnType returnData = new GetCastle_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_AddCastleElement_UserCallBack(
      RemoteServices.AddCastleElement_UserCallBack callback)
    {
      this.addCastleElement_UserCallBack = callback;
    }

    public void AddCastleElement(
      int villageID,
      int elementType,
      int x,
      int y,
      long clientElementNumber)
    {
      this.AddCastleElement(villageID, elementType, x, y, clientElementNumber, false, false, (byte[,]) null, (long[]) null, (MoveElementData[]) null);
    }

    public void AddCastleElement(
      int villageID,
      int elementType,
      int x,
      int y,
      long clientElementNumber,
      bool reinforcement)
    {
      this.AddCastleElement(villageID, elementType, x, y, clientElementNumber, reinforcement, false, (byte[,]) null, (long[]) null, (MoveElementData[]) null);
    }

    public void AddCastleElementList(int villageID, byte[,] elementList)
    {
      this.AddCastleElement(villageID, 0, 0, 0, -1L, false, false, elementList, (long[]) null, (MoveElementData[]) null);
    }

    public void AddCastleElementList(
      int villageID,
      byte[,] elementList,
      long[] troopsToDelete,
      MoveElementData[] troopsToMove)
    {
      this.AddCastleElement(villageID, 0, 0, 0, -1L, false, false, elementList, troopsToDelete, troopsToMove);
    }

    public void AddCastleElement(
      int villageID,
      int elementType,
      int x,
      int y,
      long clientElementNumber,
      bool reinforcement,
      bool vassalReinforcement,
      byte[,] elementList,
      long[] troopsToDelete,
      MoveElementData[] troopsToMove)
    {
      if (this.AddCastleElement_Callback == null)
        this.AddCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_AddCastleElement);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_AddCastleElement(this.service.AddCastleElement).BeginInvoke(this.UserID, this.SessionID, villageID, elementType, x, y, clientElementNumber, -1, -1, reinforcement, vassalReinforcement, elementList, troopsToDelete, troopsToMove, this.AddCastleElement_Callback, (object) null), typeof (AddCastleElement_ReturnType));
    }

    public void AddCastleWallElement(
      int villageID,
      int elementType,
      int x,
      int y,
      long clientElementNumber,
      int wallX,
      int wallY)
    {
      if (this.AddCastleElement_Callback == null)
        this.AddCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_AddCastleElement);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_AddCastleElement(this.service.AddCastleElement).BeginInvoke(this.UserID, this.SessionID, villageID, elementType, x, y, clientElementNumber, wallX, wallY, false, false, (byte[,]) null, (long[]) null, (MoveElementData[]) null, this.AddCastleElement_Callback, (object) null), typeof (AddCastleElement_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_AddCastleElement(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_AddCastleElement asyncDelegate = (RemoteServices.RemoteAsyncDelegate_AddCastleElement) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        AddCastleElement_ReturnType returnData = new AddCastleElement_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_DeleteCastleElement_UserCallBack(
      RemoteServices.DeleteCastleElement_UserCallBack callback)
    {
      this.deleteCastleElement_UserCallBack = callback;
    }

    public void DeleteCastleElement(int villageID, long elementID)
    {
      if (this.DeleteCastleElement_Callback == null)
        this.DeleteCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteCastleElement);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DeleteCastleElement(this.service.DeleteCastleElement).BeginInvoke(this.UserID, this.SessionID, villageID, elementID, (List<long>) null, this.DeleteCastleElement_Callback, (object) null), typeof (DeleteCastleElement_ReturnType));
    }

    public void DeleteCastleElement(int villageID, List<long> elementList)
    {
      if (this.DeleteCastleElement_Callback == null)
        this.DeleteCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteCastleElement);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DeleteCastleElement(this.service.DeleteCastleElement).BeginInvoke(this.UserID, this.SessionID, villageID, -1L, elementList, this.DeleteCastleElement_Callback, (object) null), typeof (DeleteCastleElement_ReturnType));
    }

    public void DeleteConstructingCastleElements(int villageID)
    {
      if (this.DeleteCastleElement_Callback == null)
        this.DeleteCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteCastleElement);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DeleteCastleElement(this.service.DeleteCastleElement).BeginInvoke(this.UserID, this.SessionID, villageID, -1L, (List<long>) null, this.DeleteCastleElement_Callback, (object) null), typeof (DeleteCastleElement_ReturnType));
    }

    public void DeleteAllCastleElements(int villageID)
    {
      if (this.DeleteCastleElement_Callback == null)
        this.DeleteCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteCastleElement);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DeleteCastleElement(this.service.DeleteCastleElement).BeginInvoke(this.UserID, this.SessionID, villageID, -51L, (List<long>) null, this.DeleteCastleElement_Callback, (object) null), typeof (DeleteCastleElement_ReturnType));
    }

    public void DeleteAllCastlePitsElements(int villageID)
    {
      if (this.DeleteCastleElement_Callback == null)
        this.DeleteCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteCastleElement);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DeleteCastleElement(this.service.DeleteCastleElement).BeginInvoke(this.UserID, this.SessionID, villageID, -61L, (List<long>) null, this.DeleteCastleElement_Callback, (object) null), typeof (DeleteCastleElement_ReturnType));
    }

    public void DeleteAllCastleMoatElements(int villageID)
    {
      if (this.DeleteCastleElement_Callback == null)
        this.DeleteCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteCastleElement);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DeleteCastleElement(this.service.DeleteCastleElement).BeginInvoke(this.UserID, this.SessionID, villageID, -71L, (List<long>) null, this.DeleteCastleElement_Callback, (object) null), typeof (DeleteCastleElement_ReturnType));
    }

    public void DeleteAllCastleOilPotsElements(int villageID)
    {
      if (this.DeleteCastleElement_Callback == null)
        this.DeleteCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteCastleElement);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DeleteCastleElement(this.service.DeleteCastleElement).BeginInvoke(this.UserID, this.SessionID, villageID, -81L, (List<long>) null, this.DeleteCastleElement_Callback, (object) null), typeof (DeleteCastleElement_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_DeleteCastleElement(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_DeleteCastleElement asyncDelegate = (RemoteServices.RemoteAsyncDelegate_DeleteCastleElement) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        DeleteCastleElement_ReturnType returnData = new DeleteCastleElement_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_ChangeCastleElementAggressiveDefender_UserCallBack(
      RemoteServices.ChangeCastleElementAggressiveDefender_UserCallBack callback)
    {
      this.changeCastleElementAggressiveDefender_UserCallBack = callback;
    }

    public void ChangeCastleElementAggressiveDefender(int villageID, long[] elementID, bool state)
    {
      if (this.ChangeCastleElementAggressiveDefender_Callback == null)
        this.ChangeCastleElementAggressiveDefender_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ChangeCastleElementAggressiveDefender);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ChangeCastleElementAggressiveDefender(this.service.ChangeCastleElementAggressiveDefender).BeginInvoke(this.UserID, this.SessionID, villageID, elementID, state, this.ChangeCastleElementAggressiveDefender_Callback, (object) null), typeof (ChangeCastleElementAggressiveDefender_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_ChangeCastleElementAggressiveDefender(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_ChangeCastleElementAggressiveDefender asyncDelegate = (RemoteServices.RemoteAsyncDelegate_ChangeCastleElementAggressiveDefender) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        ChangeCastleElementAggressiveDefender_ReturnType returnData = new ChangeCastleElementAggressiveDefender_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_AutoRepairCastle_UserCallBack(
      RemoteServices.AutoRepairCastle_UserCallBack callback)
    {
      this.autoRepairCastle_UserCallBack = callback;
    }

    public void AutoRepairCastle(int villageID)
    {
      if (this.AutoRepairCastle_Callback == null)
        this.AutoRepairCastle_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_AutoRepairCastle);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_AutoRepairCastle(this.service.AutoRepairCastle).BeginInvoke(this.UserID, this.SessionID, villageID, this.AutoRepairCastle_Callback, (object) null), typeof (AutoRepairCastle_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_AutoRepairCastle(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_AutoRepairCastle asyncDelegate = (RemoteServices.RemoteAsyncDelegate_AutoRepairCastle) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        AutoRepairCastle_ReturnType returnData = new AutoRepairCastle_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_MemorizeCastleTroops_UserCallBack(
      RemoteServices.MemorizeCastleTroops_UserCallBack callback)
    {
      this.memorizeCastleTroops_UserCallBack = callback;
    }

    public void MemorizeCastleTroops(int villageID)
    {
      if (this.MemorizeCastleTroops_Callback == null)
        this.MemorizeCastleTroops_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MemorizeCastleTroops);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_MemorizeCastleTroops(this.service.MemorizeCastleTroops).BeginInvoke(this.UserID, this.SessionID, villageID, this.MemorizeCastleTroops_Callback, (object) null), typeof (MemorizeCastleTroops_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_MemorizeCastleTroops(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_MemorizeCastleTroops asyncDelegate = (RemoteServices.RemoteAsyncDelegate_MemorizeCastleTroops) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        MemorizeCastleTroops_ReturnType returnData = new MemorizeCastleTroops_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_RestoreCastleTroops_UserCallBack(
      RemoteServices.RestoreCastleTroops_UserCallBack callback)
    {
      this.restoreCastleTroops_UserCallBack = callback;
    }

    public void RestoreCastleTroops(int villageID)
    {
      if (this.RestoreCastleTroops_Callback == null)
        this.RestoreCastleTroops_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RestoreCastleTroops);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_RestoreCastleTroops(this.service.RestoreCastleTroops).BeginInvoke(this.UserID, this.SessionID, villageID, this.RestoreCastleTroops_Callback, (object) null), typeof (RestoreCastleTroops_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_RestoreCastleTroops(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_RestoreCastleTroops asyncDelegate = (RemoteServices.RemoteAsyncDelegate_RestoreCastleTroops) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        RestoreCastleTroops_ReturnType returnData = new RestoreCastleTroops_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CheatAddTroops_UserCallBack(
      RemoteServices.CheatAddTroops_UserCallBack callback)
    {
      this.cheatAddTroops_UserCallBack = callback;
    }

    public void CheatAddTroops(int villageID, int troopsType, int numTroops)
    {
      if (this.CheatAddTroops_Callback == null)
        this.CheatAddTroops_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CheatAddTroops);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CheatAddTroops(this.service.CheatAddTroops).BeginInvoke(this.UserID, this.SessionID, villageID, troopsType, numTroops, this.CheatAddTroops_Callback, (object) null), typeof (CheatAddTroops_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CheatAddTroops(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CheatAddTroops asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CheatAddTroops) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CheatAddTroops_ReturnType returnData = new CheatAddTroops_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_LaunchCastleAttack_UserCallBack(
      RemoteServices.LaunchCastleAttack_UserCallBack callback)
    {
      this.launchCastleAttack_UserCallBack = callback;
    }

    public void LaunchCastleAttack(
      int parentOfAttackingVillageID,
      int sourceVillageID,
      int targetVillageID,
      byte[] troopMap,
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults,
      int attackType,
      int pillagePercent,
      int CaptainsCommand,
      int numCaptains)
    {
      if (this.LaunchCastleAttack_Callback == null)
        this.LaunchCastleAttack_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LaunchCastleAttack);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_LaunchCastleAttack(this.service.LaunchCastleAttack).BeginInvoke(this.UserID, this.SessionID, parentOfAttackingVillageID, targetVillageID, sourceVillageID, troopMap, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, attackType, pillagePercent, CaptainsCommand, numCaptains, this.LaunchCastleAttack_Callback, (object) null), typeof (LaunchCastleAttack_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_LaunchCastleAttack(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_LaunchCastleAttack asyncDelegate = (RemoteServices.RemoteAsyncDelegate_LaunchCastleAttack) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        LaunchCastleAttack_ReturnType returnData = new LaunchCastleAttack_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SendScouts_UserCallBack(RemoteServices.SendScouts_UserCallBack callback)
    {
      this.sendScouts_UserCallBack = callback;
    }

    public void SendScouts(int sourceVillageID, int targetVillageID, int numScouts)
    {
      if (this.SendScouts_Callback == null)
        this.SendScouts_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendScouts);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SendScouts(this.service.SendScouts).BeginInvoke(this.UserID, this.SessionID, targetVillageID, sourceVillageID, numScouts, this.SendScouts_Callback, (object) null), typeof (SendScouts_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SendScouts(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SendScouts asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SendScouts) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SendScouts_ReturnType returnData = new SendScouts_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CancelCastleAttack_UserCallBack(
      RemoteServices.CancelCastleAttack_UserCallBack callback)
    {
      this.cancelCastleAttack_UserCallBack = callback;
    }

    public void CancelCastleAttack(long armyID)
    {
      if (this.CancelCastleAttack_Callback == null)
        this.CancelCastleAttack_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CancelCastleAttack);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CancelCastleAttack(this.service.CancelCastleAttack).BeginInvoke(this.UserID, this.SessionID, armyID, this.CancelCastleAttack_Callback, (object) null), typeof (CancelCastleAttack_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CancelCastleAttack(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CancelCastleAttack asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CancelCastleAttack) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CancelCastleAttack_ReturnType returnData = new CancelCastleAttack_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SendReinforcements_UserCallBack(
      RemoteServices.SendReinforcements_UserCallBack callback)
    {
      this.sendReinforcements_UserCallBack = callback;
    }

    public void SendReinforcements(
      int sourceVillageID,
      int targetVillageID,
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults)
    {
      if (this.SendReinforcements_Callback == null)
        this.SendReinforcements_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendReinforcements);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SendReinforcements(this.service.SendReinforcements).BeginInvoke(this.UserID, this.SessionID, sourceVillageID, targetVillageID, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, this.SendReinforcements_Callback, (object) null), typeof (SendReinforcements_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SendReinforcements(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SendReinforcements asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SendReinforcements) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SendReinforcements_ReturnType returnData = new SendReinforcements_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_ReturnReinforcements_UserCallBack(
      RemoteServices.ReturnReinforcements_UserCallBack callback)
    {
      this.returnReinforcements_UserCallBack = callback;
    }

    public void ReturnReinforcements(long reinforcementID)
    {
      if (this.ReturnReinforcements_Callback == null)
        this.ReturnReinforcements_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ReturnReinforcements);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ReturnReinforcements(this.service.ReturnReinforcements).BeginInvoke(this.UserID, this.SessionID, reinforcementID, -1, -1, -1, -1, -1, this.ReturnReinforcements_Callback, (object) null), typeof (ReturnReinforcements_ReturnType));
    }

    public void ReturnReinforcements(
      long reinforcementID,
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults)
    {
      if (this.ReturnReinforcements_Callback == null)
        this.ReturnReinforcements_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ReturnReinforcements);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ReturnReinforcements(this.service.ReturnReinforcements).BeginInvoke(this.UserID, this.SessionID, reinforcementID, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, this.ReturnReinforcements_Callback, (object) null), typeof (ReturnReinforcements_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_ReturnReinforcements(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_ReturnReinforcements asyncDelegate = (RemoteServices.RemoteAsyncDelegate_ReturnReinforcements) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        ReturnReinforcements_ReturnType returnData = new ReturnReinforcements_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SendMarketResources_UserCallBack(
      RemoteServices.SendMarketResources_UserCallBack callback)
    {
      this.sendMarketResources_UserCallBack = callback;
    }

    public void SendMarketResources(
      int homeVillageID,
      int targetVillage,
      int resource,
      int amount)
    {
      if (this.SendMarketResources_Callback == null)
        this.SendMarketResources_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendMarketResources);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SendMarketResources(this.service.SendMarketResources).BeginInvoke(this.UserID, this.SessionID, homeVillageID, targetVillage, resource, amount, this.SendMarketResources_Callback, (object) null), typeof (SendMarketResources_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SendMarketResources(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SendMarketResources asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SendMarketResources) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SendMarketResources_ReturnType returnData = new SendMarketResources_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetUserTraders_UserCallBack(
      RemoteServices.GetUserTraders_UserCallBack callback)
    {
      this.getUserTraders_UserCallBack = callback;
    }

    public void GetUserTraders()
    {
      if (this.GetUserTraders_Callback == null)
        this.GetUserTraders_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetUserTraders);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetUserTraders(this.service.GetUserTraders).BeginInvoke(this.UserID, this.SessionID, -1, this.GetUserTraders_Callback, (object) null), typeof (GetUserTraders_ReturnType));
    }

    public void GetUserTraders(int villageID)
    {
      if (this.GetUserTraders_Callback == null)
        this.GetUserTraders_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetUserTraders);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetUserTraders(this.service.GetUserTraders).BeginInvoke(this.UserID, this.SessionID, villageID, this.GetUserTraders_Callback, (object) null), typeof (GetUserTraders_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetUserTraders(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetUserTraders asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetUserTraders) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetUserTraders_ReturnType returnData = new GetUserTraders_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetActiveTraders_UserCallBack(
      RemoteServices.GetActiveTraders_UserCallBack callback)
    {
      this.getActiveTraders_UserCallBack = callback;
    }

    public void GetActiveTraders(DateTime lastTime)
    {
      if (this.GetActiveTraders_Callback == null)
        this.GetActiveTraders_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetActiveTraders);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetActiveTraders(this.service.GetActiveTraders).BeginInvoke(this.UserID, this.SessionID, lastTime, this.GetActiveTraders_Callback, (object) null), typeof (GetActiveTraders_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetActiveTraders(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetActiveTraders asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetActiveTraders) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetActiveTraders_ReturnType returnData = new GetActiveTraders_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetStockExchangeData_UserCallBack(
      RemoteServices.GetStockExchangeData_UserCallBack callback)
    {
      this.getStockExchangeData_UserCallBack = callback;
    }

    public void GetStockExchangeData(int villageID, bool stockExchange)
    {
      if (this.GetStockExchangeData_Callback == null)
        this.GetStockExchangeData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetStockExchangeData);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetStockExchangeData(this.service.GetStockExchangeData).BeginInvoke(this.UserID, this.SessionID, villageID, stockExchange, (int[]) null, this.GetStockExchangeData_Callback, (object) null), typeof (GetStockExchangeData_ReturnType));
    }

    public void GetStockExchangePremiumData(int villageID, int[] closeVillages)
    {
      if (this.GetStockExchangeData_Callback == null)
        this.GetStockExchangeData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetStockExchangeData);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetStockExchangeData(this.service.GetStockExchangeData).BeginInvoke(this.UserID, this.SessionID, villageID, true, closeVillages, this.GetStockExchangeData_Callback, (object) null), typeof (GetStockExchangeData_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetStockExchangeData(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetStockExchangeData asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetStockExchangeData) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetStockExchangeData_ReturnType returnData = new GetStockExchangeData_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_StockExchangeTrade_UserCallBack(
      RemoteServices.StockExchangeTrade_UserCallBack callback)
    {
      this.stockExchangeTrade_UserCallBack = callback;
    }

    public void StockExchangeTrade(
      int villageID,
      int targetExchange,
      int resource,
      int amount,
      bool buy)
    {
      if (this.StockExchangeTrade_Callback == null)
        this.StockExchangeTrade_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_StockExchangeTrade);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_StockExchangeTrade(this.service.StockExchangeTrade).BeginInvoke(this.UserID, this.SessionID, villageID, targetExchange, resource, amount, buy, this.StockExchangeTrade_Callback, (object) null), typeof (StockExchangeTrade_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_StockExchangeTrade(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_StockExchangeTrade asyncDelegate = (RemoteServices.RemoteAsyncDelegate_StockExchangeTrade) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        StockExchangeTrade_ReturnType returnData = new StockExchangeTrade_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_UpdateVillageFavourites_UserCallBack(
      RemoteServices.UpdateVillageFavourites_UserCallBack callback)
    {
      this.updateVillageFavourites_UserCallBack = callback;
    }

    public void UpdateVillageFavourites(int mode, int villageID)
    {
      if (this.UpdateVillageFavourites_Callback == null)
        this.UpdateVillageFavourites_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpdateVillageFavourites);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_UpdateVillageFavourites(this.service.UpdateVillageFavourites).BeginInvoke(this.UserID, this.SessionID, mode, villageID, this.UpdateVillageFavourites_Callback, (object) null), typeof (UpdateVillageFavourites_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_UpdateVillageFavourites(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_UpdateVillageFavourites asyncDelegate = (RemoteServices.RemoteAsyncDelegate_UpdateVillageFavourites) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        UpdateVillageFavourites_ReturnType returnData = new UpdateVillageFavourites_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_MakeTroop_UserCallBack(RemoteServices.MakeTroop_UserCallBack callback)
    {
      this.makeTroop_UserCallBack = callback;
    }

    public void MakeTroop(int villageID, int troopType, int amount)
    {
      if (this.MakeTroop_Callback == null)
        this.MakeTroop_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MakeTroop);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_MakeTroop(this.service.MakeTroop).BeginInvoke(this.UserID, this.SessionID, villageID, troopType, amount, this.MakeTroop_Callback, (object) null), typeof (MakeTroop_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_MakeTroop(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_MakeTroop asyncDelegate = (RemoteServices.RemoteAsyncDelegate_MakeTroop) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        MakeTroop_ReturnType returnData = new MakeTroop_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_UpgradeRank_UserCallBack(RemoteServices.UpgradeRank_UserCallBack callback)
    {
      this.upgradeRank_UserCallBack = callback;
    }

    public void UpgradeRank(int rank, int rankSubLevel)
    {
      if (this.UpgradeRank_Callback == null)
        this.UpgradeRank_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpgradeRank);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_UpgradeRank(this.service.UpgradeRank).BeginInvoke(this.UserID, this.SessionID, rank, rankSubLevel, this.UpgradeRank_Callback, (object) null), typeof (UpgradeRank_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_UpgradeRank(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_UpgradeRank asyncDelegate = (RemoteServices.RemoteAsyncDelegate_UpgradeRank) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        UpgradeRank_ReturnType returnData = new UpgradeRank_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_PreAttackSetup_UserCallBack(
      RemoteServices.PreAttackSetup_UserCallBack callback)
    {
      this.preAttackSetup_UserCallBack = callback;
    }

    public void PreAttackSetup(
      int parentAttackingVillage,
      int attackingVillage,
      int targetVillage,
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults,
      int attackType,
      int pillagePercent,
      int captainsCommand)
    {
      if (this.PreAttackSetup_Callback == null)
        this.PreAttackSetup_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_PreAttackSetup);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_PreAttackSetup(this.service.PreAttackSetup).BeginInvoke(this.UserID, this.SessionID, parentAttackingVillage, attackingVillage, targetVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, attackType, pillagePercent, captainsCommand, this.PreAttackSetup_Callback, (object) null), typeof (PreAttackSetup_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_PreAttackSetup(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_PreAttackSetup asyncDelegate = (RemoteServices.RemoteAsyncDelegate_PreAttackSetup) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        PreAttackSetup_ReturnType returnData = new PreAttackSetup_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetBattleHonourRating_UserCallBack(
      RemoteServices.GetBattleHonourRating_UserCallBack callback)
    {
      this.getBattleHonourRating_UserCallBack = callback;
    }

    public void GetBattleHonourRating(int attackedVillage)
    {
      if (this.GetBattleHonourRating_Callback == null)
        this.GetBattleHonourRating_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetBattleHonourRating);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetBattleHonourRating(this.service.GetBattleHonourRating).BeginInvoke(this.UserID, this.SessionID, attackedVillage, this.GetBattleHonourRating_Callback, (object) null), typeof (GetBattleHonourRating_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetBattleHonourRating(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetBattleHonourRating asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetBattleHonourRating) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetBattleHonourRating_ReturnType returnData = new GetBattleHonourRating_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_RetrieveArmyFromGarrison_UserCallBack(
      RemoteServices.RetrieveArmyFromGarrison_UserCallBack callback)
    {
      this.retrieveArmyFromGarrison_UserCallBack = callback;
    }

    public void RetrieveArmyFromGarrison(int villageID)
    {
      if (this.RetrieveArmyFromGarrison_Callback == null)
        this.RetrieveArmyFromGarrison_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RetrieveArmyFromGarrison);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_RetrieveArmyFromGarrison(this.service.RetrieveArmyFromGarrison).BeginInvoke(this.UserID, this.SessionID, villageID, this.RetrieveArmyFromGarrison_Callback, (object) null), typeof (RetrieveArmyFromGarrison_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_RetrieveArmyFromGarrison(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_RetrieveArmyFromGarrison asyncDelegate = (RemoteServices.RemoteAsyncDelegate_RetrieveArmyFromGarrison) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        RetrieveArmyFromGarrison_ReturnType returnData = new RetrieveArmyFromGarrison_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_RetrieveVillageUserInfo_UserCallBack(
      RemoteServices.RetrieveVillageUserInfo_UserCallBack callback)
    {
      this.retrieveVillageUserInfo_UserCallBack = callback;
    }

    public void RetrieveVillageUserInfo(int villageID, int targetUserID, bool extended)
    {
      if (this.RetrieveVillageUserInfo_Callback == null)
        this.RetrieveVillageUserInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RetrieveVillageUserInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_RetrieveVillageUserInfo(this.service.RetrieveVillageUserInfo).BeginInvoke(this.UserID, this.SessionID, villageID, targetUserID, extended, this.RetrieveVillageUserInfo_Callback, (object) null), typeof (RetrieveVillageUserInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_RetrieveVillageUserInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_RetrieveVillageUserInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_RetrieveVillageUserInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        RetrieveVillageUserInfo_ReturnType returnData = new RetrieveVillageUserInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SpecialVillageInfo_UserCallBack(
      RemoteServices.SpecialVillageInfo_UserCallBack callback)
    {
      this.specialVillageInfo_UserCallBack = callback;
    }

    public void SpecialVillageInfo(int villageID)
    {
      if (this.SpecialVillageInfo_Callback == null)
        this.SpecialVillageInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SpecialVillageInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SpecialVillageInfo(this.service.SpecialVillageInfo).BeginInvoke(this.UserID, this.SessionID, villageID, this.SpecialVillageInfo_Callback, (object) null), typeof (SpecialVillageInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SpecialVillageInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SpecialVillageInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SpecialVillageInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SpecialVillageInfo_ReturnType returnData = new SpecialVillageInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetVillageRankTaxTree_UserCallBack(
      RemoteServices.GetVillageRankTaxTree_UserCallBack callback)
    {
      this.getVillageRankTaxTree_UserCallBack = callback;
    }

    public void GetVillageRankTaxTree()
    {
      if (this.GetVillageRankTaxTree_Callback == null)
        this.GetVillageRankTaxTree_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetVillageRankTaxTree);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetVillageRankTaxTree(this.service.GetVillageRankTaxTree).BeginInvoke(this.UserID, this.SessionID, this.GetVillageRankTaxTree_Callback, (object) null), typeof (GetVillageRankTaxTree_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetVillageRankTaxTree(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetVillageRankTaxTree asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetVillageRankTaxTree) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetVillageRankTaxTree_ReturnType returnData = new GetVillageRankTaxTree_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetResearchData_UserCallBack(
      RemoteServices.GetResearchData_UserCallBack callback)
    {
      this.getResearchData_UserCallBack = callback;
    }

    public void GetResearchData()
    {
      if (this.GetResearchData_Callback == null)
        this.GetResearchData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetResearchData);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetResearchData(this.service.GetResearchData).BeginInvoke(this.UserID, this.SessionID, this.GetResearchData_Callback, (object) null), typeof (GetResearchData_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetResearchData(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetResearchData asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetResearchData) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetResearchData_ReturnType returnData = new GetResearchData_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_DoResearch_UserCallBack(RemoteServices.DoResearch_UserCallBack callback)
    {
      this.doResearch_UserCallBack = callback;
    }

    public void DoResearch(int researchType)
    {
      if (this.DoResearch_Callback == null)
        this.DoResearch_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DoResearch);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DoResearch(this.service.DoResearch).BeginInvoke(this.UserID, this.SessionID, researchType, -1, this.DoResearch_Callback, (object) null), typeof (DoResearch_ReturnType));
    }

    public void CancelQueuedResearch(int researchType, int queuePos)
    {
      if (this.DoResearch_Callback == null)
        this.DoResearch_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DoResearch);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DoResearch(this.service.DoResearch).BeginInvoke(this.UserID, this.SessionID, researchType, queuePos, this.DoResearch_Callback, (object) null), typeof (DoResearch_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_DoResearch(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_DoResearch asyncDelegate = (RemoteServices.RemoteAsyncDelegate_DoResearch) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        DoResearch_ReturnType returnData = new DoResearch_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_BuyResearchPoint_UserCallBack(
      RemoteServices.BuyResearchPoint_UserCallBack callback)
    {
      this.buyResearchPoint_UserCallBack = callback;
    }

    public void BuyResearchPoint()
    {
      if (this.BuyResearchPoint_Callback == null)
        this.BuyResearchPoint_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_BuyResearchPoint);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_BuyResearchPoint(this.service.BuyResearchPoint).BeginInvoke(this.UserID, this.SessionID, this.BuyResearchPoint_Callback, (object) null), typeof (BuyResearchPoint_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_BuyResearchPoint(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_BuyResearchPoint asyncDelegate = (RemoteServices.RemoteAsyncDelegate_BuyResearchPoint) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        BuyResearchPoint_ReturnType returnData = new BuyResearchPoint_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_VassalInfo_UserCallBack(RemoteServices.VassalInfo_UserCallBack callback)
    {
      this.vassalInfo_UserCallBack = callback;
    }

    public void VassalInfo(int villageID)
    {
      if (this.VassalInfo_Callback == null)
        this.VassalInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VassalInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_VassalInfo(this.service.VassalInfo).BeginInvoke(this.UserID, this.SessionID, villageID, this.VassalInfo_Callback, (object) null), typeof (VassalInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_VassalInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_VassalInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_VassalInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        VassalInfo_ReturnType returnData = new VassalInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_VassalSendResources_UserCallBack(
      RemoteServices.VassalSendResources_UserCallBack callback)
    {
      this.vassalSendResources_UserCallBack = callback;
    }

    public void VassalSendResources(int villageID, int targetVillage, int type, int amount)
    {
      if (this.VassalSendResources_Callback == null)
        this.VassalSendResources_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VassalSendResources);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_VassalSendResources(this.service.VassalSendResources).BeginInvoke(this.UserID, this.SessionID, villageID, targetVillage, type, amount, this.VassalSendResources_Callback, (object) null), typeof (VassalSendResources_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_VassalSendResources(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_VassalSendResources asyncDelegate = (RemoteServices.RemoteAsyncDelegate_VassalSendResources) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        VassalSendResources_ReturnType returnData = new VassalSendResources_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_UpdateSelectedTitheType_UserCallBack(
      RemoteServices.UpdateSelectedTitheType_UserCallBack callback)
    {
      this.updateSelectedTitheType_UserCallBack = callback;
    }

    public void UpdateSelectedTitheType(int villageID, int type)
    {
      if (this.UpdateSelectedTitheType_Callback == null)
        this.UpdateSelectedTitheType_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpdateSelectedTitheType);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_UpdateSelectedTitheType(this.service.UpdateSelectedTitheType).BeginInvoke(this.UserID, this.SessionID, villageID, type, this.UpdateSelectedTitheType_Callback, (object) null), typeof (UpdateSelectedTitheType_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_UpdateSelectedTitheType(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_UpdateSelectedTitheType asyncDelegate = (RemoteServices.RemoteAsyncDelegate_UpdateSelectedTitheType) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        UpdateSelectedTitheType_ReturnType returnData = new UpdateSelectedTitheType_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_BreakVassalage_UserCallBack(
      RemoteServices.BreakVassalage_UserCallBack callback)
    {
      this.breakVassalage_UserCallBack = callback;
    }

    public void BreakVassalage(int villageID, int targetVillage)
    {
      if (this.BreakVassalage_Callback == null)
        this.BreakVassalage_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_BreakVassalage);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_BreakVassalage(this.service.BreakVassalage).BeginInvoke(this.UserID, this.SessionID, villageID, targetVillage, this.BreakVassalage_Callback, (object) null), typeof (BreakVassalage_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_BreakVassalage(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_BreakVassalage asyncDelegate = (RemoteServices.RemoteAsyncDelegate_BreakVassalage) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        BreakVassalage_ReturnType returnData = new BreakVassalage_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_BreakLiegeLord_UserCallBack(
      RemoteServices.BreakLiegeLord_UserCallBack callback)
    {
      this.breakLiegeLord_UserCallBack = callback;
    }

    public void BreakLiegeLord(int villageID, int targetVillage)
    {
      if (this.BreakLiegeLord_Callback == null)
        this.BreakLiegeLord_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_BreakLiegeLord);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_BreakLiegeLord(this.service.BreakLiegeLord).BeginInvoke(this.UserID, this.SessionID, villageID, targetVillage, this.BreakLiegeLord_Callback, (object) null), typeof (BreakLiegeLord_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_BreakLiegeLord(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_BreakLiegeLord asyncDelegate = (RemoteServices.RemoteAsyncDelegate_BreakLiegeLord) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        BreakLiegeLord_ReturnType returnData = new BreakLiegeLord_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetPreVassalInfo_UserCallBack(
      RemoteServices.GetPreVassalInfo_UserCallBack callback)
    {
      this.getPreVassalInfo_UserCallBack = callback;
    }

    public void GetPreVassalInfo(int villageID, int targetVillage)
    {
      if (this.GetPreVassalInfo_Callback == null)
        this.GetPreVassalInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetPreVassalInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetPreVassalInfo(this.service.GetPreVassalInfo).BeginInvoke(this.UserID, this.SessionID, villageID, targetVillage, this.GetPreVassalInfo_Callback, (object) null), typeof (GetPreVassalInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetPreVassalInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetPreVassalInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetPreVassalInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetPreVassalInfo_ReturnType returnData = new GetPreVassalInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SendVassalRequest_UserCallBack(
      RemoteServices.SendVassalRequest_UserCallBack callback)
    {
      this.sendVassalRequest_UserCallBack = callback;
    }

    public void SendVassalRequest(int villageID, int targetVillage)
    {
      if (this.SendVassalRequest_Callback == null)
        this.SendVassalRequest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendVassalRequest);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SendVassalRequest(this.service.SendVassalRequest).BeginInvoke(this.UserID, this.SessionID, villageID, targetVillage, this.SendVassalRequest_Callback, (object) null), typeof (SendVassalRequest_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SendVassalRequest(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SendVassalRequest asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SendVassalRequest) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SendVassalRequest_ReturnType returnData = new SendVassalRequest_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_HandleVassalRequest_UserCallBack(
      RemoteServices.HandleVassalRequest_UserCallBack callback)
    {
      this.handleVassalRequest_UserCallBack = callback;
    }

    public void AcceptVassalRequest(int requesterVillageID, int vassalVillageID)
    {
      if (this.HandleVassalRequest_Callback == null)
        this.HandleVassalRequest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_HandleVassalRequest);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_HandleVassalRequest(this.service.HandleVassalRequest).BeginInvoke(this.UserID, this.SessionID, 1, requesterVillageID, vassalVillageID, this.HandleVassalRequest_Callback, (object) null), typeof (HandleVassalRequest_ReturnType));
    }

    public void DeclineVassalRequest(int requesterVillageID, int vassalVillageID)
    {
      if (this.HandleVassalRequest_Callback == null)
        this.HandleVassalRequest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_HandleVassalRequest);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_HandleVassalRequest(this.service.HandleVassalRequest).BeginInvoke(this.UserID, this.SessionID, 2, requesterVillageID, vassalVillageID, this.HandleVassalRequest_Callback, (object) null), typeof (HandleVassalRequest_ReturnType));
    }

    public void CancelVassalRequest(int requesterVillageID, int vassalVillageID)
    {
      if (this.HandleVassalRequest_Callback == null)
        this.HandleVassalRequest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_HandleVassalRequest);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_HandleVassalRequest(this.service.HandleVassalRequest).BeginInvoke(this.UserID, this.SessionID, 3, requesterVillageID, vassalVillageID, this.HandleVassalRequest_Callback, (object) null), typeof (HandleVassalRequest_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_HandleVassalRequest(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_HandleVassalRequest asyncDelegate = (RemoteServices.RemoteAsyncDelegate_HandleVassalRequest) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        HandleVassalRequest_ReturnType returnData = new HandleVassalRequest_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetVassalArmyInfo_UserCallBack(
      RemoteServices.GetVassalArmyInfo_UserCallBack callback)
    {
      this.getVassalArmyInfo_UserCallBack = callback;
    }

    public void GetVassalArmyInfo(int vassalVillageID, int mode, int attackedVillage)
    {
      if (this.GetVassalArmyInfo_Callback == null)
        this.GetVassalArmyInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetVassalArmyInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetVassalArmyInfo(this.service.GetVassalArmyInfo).BeginInvoke(this.UserID, this.SessionID, vassalVillageID, mode, attackedVillage, this.GetVassalArmyInfo_Callback, (object) null), typeof (GetVassalArmyInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetVassalArmyInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetVassalArmyInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetVassalArmyInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetVassalArmyInfo_ReturnType returnData = new GetVassalArmyInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SendTroopsToVassal_UserCallBack(
      RemoteServices.SendTroopsToVassal_UserCallBack callback)
    {
      this.sendTroopsToVassal_UserCallBack = callback;
    }

    public void SendTroopsToVassal(
      int liegeLordVillageID,
      int vassalVillageID,
      int peasants,
      int archers,
      int pikemen,
      int swordsmen,
      int catapults)
    {
      if (this.SendTroopsToVassal_Callback == null)
        this.SendTroopsToVassal_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendTroopsToVassal);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SendTroopsToVassal(this.service.SendTroopsToVassal).BeginInvoke(this.UserID, this.SessionID, liegeLordVillageID, vassalVillageID, peasants, archers, pikemen, swordsmen, catapults, this.SendTroopsToVassal_Callback, (object) null), typeof (SendTroopsToVassal_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SendTroopsToVassal(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SendTroopsToVassal asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SendTroopsToVassal) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SendTroopsToVassal_ReturnType returnData = new SendTroopsToVassal_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_RetrieveTroopsFromVassal_UserCallBack(
      RemoteServices.RetrieveTroopsFromVassal_UserCallBack callback)
    {
      this.retrieveTroopsFromVassal_UserCallBack = callback;
    }

    public void RetrieveTroopsFromVassal(int liegeLordVillageID, int vassalVillageID)
    {
      if (this.RetrieveTroopsFromVassal_Callback == null)
        this.RetrieveTroopsFromVassal_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RetrieveTroopsFromVassal);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_RetrieveTroopsFromVassal(this.service.RetrieveTroopsFromVassal).BeginInvoke(this.UserID, this.SessionID, liegeLordVillageID, vassalVillageID, this.RetrieveTroopsFromVassal_Callback, (object) null), typeof (RetrieveTroopsFromVassal_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_RetrieveTroopsFromVassal(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_RetrieveTroopsFromVassal asyncDelegate = (RemoteServices.RemoteAsyncDelegate_RetrieveTroopsFromVassal) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        RetrieveTroopsFromVassal_ReturnType returnData = new RetrieveTroopsFromVassal_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_UpdateVillageResourcesInfo_UserCallBack(
      RemoteServices.UpdateVillageResourcesInfo_UserCallBack callback)
    {
      this.updateVillageResourcesInfo_UserCallBack = callback;
    }

    public void UpdateVillageResourcesInfo(int villageID)
    {
      if (this.UpdateVillageResourcesInfo_Callback == null)
        this.UpdateVillageResourcesInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpdateVillageResourcesInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_UpdateVillageResourcesInfo(this.service.UpdateVillageResourcesInfo).BeginInvoke(this.UserID, this.SessionID, villageID, this.UpdateVillageResourcesInfo_Callback, (object) null), typeof (UpdateVillageResourcesInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_UpdateVillageResourcesInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_UpdateVillageResourcesInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_UpdateVillageResourcesInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        UpdateVillageResourcesInfo_ReturnType returnData = new UpdateVillageResourcesInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SetHighestArmySeen_UserCallBack(
      RemoteServices.SetHighestArmySeen_UserCallBack callback)
    {
      this.setHighestArmySeen_UserCallBack = callback;
    }

    public void SetHighestArmySeen(long highestArmyIDSeen)
    {
      if (this.SetHighestArmySeen_Callback == null)
        this.SetHighestArmySeen_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SetHighestArmySeen);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SetHighestArmySeen(this.service.SetHighestArmySeen).BeginInvoke(this.UserID, this.SessionID, highestArmyIDSeen, this.SetHighestArmySeen_Callback, (object) null), typeof (SetHighestArmySeen_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SetHighestArmySeen(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SetHighestArmySeen asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SetHighestArmySeen) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SetHighestArmySeen_ReturnType returnData = new SetHighestArmySeen_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetForumList_UserCallBack(RemoteServices.GetForumList_UserCallBack callback)
    {
      this.getForumList_UserCallBack = callback;
    }

    public void GetForumList(int areaID, int areaType)
    {
      if (this.GetForumList_Callback == null)
        this.GetForumList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetForumList);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetForumList(this.service.GetForumList).BeginInvoke(this.UserID, this.SessionID, areaID, areaType, this.GetForumList_Callback, (object) null), typeof (GetForumList_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetForumList(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetForumList asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetForumList) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetForumList_ReturnType returnData = new GetForumList_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetForumThreadList_UserCallBack(
      RemoteServices.GetForumThreadList_UserCallBack callback)
    {
      this.getForumThreadList_UserCallBack = callback;
    }

    public void GetForumThreadList(long forumID, DateTime lastGet, bool forceGet)
    {
      if (this.GetForumThreadList_Callback == null)
        this.GetForumThreadList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetForumThreadList);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetForumThreadList(this.service.GetForumThreadList).BeginInvoke(this.UserID, this.SessionID, forumID, lastGet, forceGet, this.GetForumThreadList_Callback, (object) null), typeof (GetForumThreadList_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetForumThreadList(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetForumThreadList asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetForumThreadList) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetForumThreadList_ReturnType returnData = new GetForumThreadList_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetForumThread_UserCallBack(
      RemoteServices.GetForumThread_UserCallBack callback)
    {
      this.getForumThread_UserCallBack = callback;
    }

    public void GetForumThread(long forumID, long threadID, DateTime lastGet, bool forceGet)
    {
      if (this.GetForumThread_Callback == null)
        this.GetForumThread_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetForumThread);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetForumThread(this.service.GetForumThread).BeginInvoke(this.UserID, this.SessionID, forumID, threadID, lastGet, forceGet, this.GetForumThread_Callback, (object) null), typeof (GetForumThread_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetForumThread(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetForumThread asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetForumThread) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetForumThread_ReturnType returnData = new GetForumThread_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_NewForumThread_UserCallBack(
      RemoteServices.NewForumThread_UserCallBack callback)
    {
      this.newForumThread_UserCallBack = callback;
    }

    public void NewForumThread(long forumID, string headingText, string bodyText)
    {
      if (this.NewForumThread_Callback == null)
        this.NewForumThread_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_NewForumThread);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_NewForumThread(this.service.NewForumThread).BeginInvoke(this.UserID, this.SessionID, forumID, headingText, bodyText, this.NewForumThread_Callback, (object) null), typeof (NewForumThread_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_NewForumThread(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_NewForumThread asyncDelegate = (RemoteServices.RemoteAsyncDelegate_NewForumThread) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        NewForumThread_ReturnType returnData = new NewForumThread_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_PostToForumThread_UserCallBack(
      RemoteServices.PostToForumThread_UserCallBack callback)
    {
      this.postToForumThread_UserCallBack = callback;
    }

    public void PostToForumThread(long threadID, long forumID, string text)
    {
      if (this.PostToForumThread_Callback == null)
        this.PostToForumThread_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_PostToForumThread);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_PostToForumThread(this.service.PostToForumThread).BeginInvoke(this.UserID, this.SessionID, threadID, forumID, text, this.PostToForumThread_Callback, (object) null), typeof (PostToForumThread_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_PostToForumThread(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_PostToForumThread asyncDelegate = (RemoteServices.RemoteAsyncDelegate_PostToForumThread) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        PostToForumThread_ReturnType returnData = new PostToForumThread_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GiveForumAccess_UserCallBack(
      RemoteServices.GiveForumAccess_UserCallBack callback)
    {
      this.giveForumAccess_UserCallBack = callback;
    }

    public void GiveForumAccess(long forumID, int[] users)
    {
      if (this.GiveForumAccess_Callback == null)
        this.GiveForumAccess_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GiveForumAccess);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GiveForumAccess(this.service.GiveForumAccess).BeginInvoke(this.UserID, this.SessionID, forumID, users, this.GiveForumAccess_Callback, (object) null), typeof (GiveForumAccess_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GiveForumAccess(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GiveForumAccess asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GiveForumAccess) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GiveForumAccess_ReturnType returnData = new GiveForumAccess_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CreateForum_UserCallBack(RemoteServices.CreateForum_UserCallBack callback)
    {
      this.createForum_UserCallBack = callback;
    }

    public void CreateForum(int areaID, int areaType, string name)
    {
      if (this.CreateForum_Callback == null)
        this.CreateForum_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CreateForum);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CreateForum(this.service.CreateForum).BeginInvoke(this.UserID, this.SessionID, areaID, areaType, name, this.CreateForum_Callback, (object) null), typeof (CreateForum_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CreateForum(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CreateForum asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CreateForum) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CreateForum_ReturnType returnData = new CreateForum_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_DeleteForum_UserCallBack(RemoteServices.DeleteForum_UserCallBack callback)
    {
      this.deleteForum_UserCallBack = callback;
    }

    public void DeleteForum(int areaID, int areaType, long forumID)
    {
      if (this.DeleteForum_Callback == null)
        this.DeleteForum_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteForum);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DeleteForum(this.service.DeleteForum).BeginInvoke(this.UserID, this.SessionID, areaID, areaType, forumID, this.DeleteForum_Callback, (object) null), typeof (DeleteForum_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_DeleteForum(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_DeleteForum asyncDelegate = (RemoteServices.RemoteAsyncDelegate_DeleteForum) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        DeleteForum_ReturnType returnData = new DeleteForum_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_DeleteForumThread_UserCallBack(
      RemoteServices.DeleteForumThread_UserCallBack callback)
    {
      this.deleteForumThread_UserCallBack = callback;
    }

    public void DeleteForumThread(
      int areaID,
      int areaType,
      string name,
      long forumID,
      long forumThreadID)
    {
      if (this.DeleteForumThread_Callback == null)
        this.DeleteForumThread_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteForumThread);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DeleteForumThread(this.service.DeleteForumThread).BeginInvoke(this.UserID, this.SessionID, areaID, areaType, name, forumID, forumThreadID, this.DeleteForumThread_Callback, (object) null), typeof (DeleteForumThread_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_DeleteForumThread(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_DeleteForumThread asyncDelegate = (RemoteServices.RemoteAsyncDelegate_DeleteForumThread) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        DeleteForumThread_ReturnType returnData = new DeleteForumThread_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_DeleteForumPost_UserCallBack(
      RemoteServices.DeleteForumPost_UserCallBack callback)
    {
      this.deleteForumPost_UserCallBack = callback;
    }

    public void DeleteForumPost(
      int areaID,
      int areaType,
      string name,
      long forumID,
      long forumThreadID,
      long forumPostID)
    {
      if (this.DeleteForumPost_Callback == null)
        this.DeleteForumPost_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteForumPost);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DeleteForumPost(this.service.DeleteForumPost).BeginInvoke(this.UserID, this.SessionID, areaID, areaType, name, forumID, forumThreadID, forumPostID, this.DeleteForumPost_Callback, (object) null), typeof (DeleteForumPost_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_DeleteForumPost(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_DeleteForumPost asyncDelegate = (RemoteServices.RemoteAsyncDelegate_DeleteForumPost) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        DeleteForumPost_ReturnType returnData = new DeleteForumPost_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetCurrentElectionInfo_UserCallBack(
      RemoteServices.GetCurrentElectionInfo_UserCallBack callback)
    {
      this.getCurrentElectionInfo_UserCallBack = callback;
    }

    public void GetCurrentElectionInfo(int areaID, int areaType)
    {
      if (this.GetCurrentElectionInfo_Callback == null)
        this.GetCurrentElectionInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetCurrentElectionInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetCurrentElectionInfo(this.service.GetCurrentElectionInfo).BeginInvoke(this.UserID, this.SessionID, areaID, areaType, this.GetCurrentElectionInfo_Callback, (object) null), typeof (GetCurrentElectionInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetCurrentElectionInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetCurrentElectionInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetCurrentElectionInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetCurrentElectionInfo_ReturnType returnData = new GetCurrentElectionInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_StandInElection_UserCallBack(
      RemoteServices.StandInElection_UserCallBack callback)
    {
      this.standInElection_UserCallBack = callback;
    }

    public void StandInElection(int areaID, int areaType, bool state)
    {
      if (this.StandInElection_Callback == null)
        this.StandInElection_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_StandInElection);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_StandInElection(this.service.StandInElection).BeginInvoke(this.UserID, this.SessionID, areaID, areaType, state, this.StandInElection_Callback, (object) null), typeof (StandInElection_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_StandInElection(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_StandInElection asyncDelegate = (RemoteServices.RemoteAsyncDelegate_StandInElection) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        StandInElection_ReturnType returnData = new StandInElection_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_VoteInElection_UserCallBack(
      RemoteServices.VoteInElection_UserCallBack callback)
    {
      this.voteInElection_UserCallBack = callback;
    }

    public void VoteInElection(int areaID, int areaType, int candidate)
    {
      if (this.VoteInElection_Callback == null)
        this.VoteInElection_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VoteInElection);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_VoteInElection(this.service.VoteInElection).BeginInvoke(this.UserID, this.SessionID, areaID, areaType, candidate, this.VoteInElection_Callback, (object) null), typeof (VoteInElection_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_VoteInElection(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_VoteInElection asyncDelegate = (RemoteServices.RemoteAsyncDelegate_VoteInElection) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        VoteInElection_ReturnType returnData = new VoteInElection_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_UploadAvatar_UserCallBack(RemoteServices.UploadAvatar_UserCallBack callback)
    {
      this.uploadAvatar_UserCallBack = callback;
    }

    public void UploadAvatar(AvatarData avatarData)
    {
      if (this.UploadAvatar_Callback == null)
        this.UploadAvatar_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UploadAvatar);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_UploadAvatar(this.service.UploadAvatar).BeginInvoke(this.UserID, this.SessionID, avatarData, this.UploadAvatar_Callback, (object) null), typeof (UploadAvatar_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_UploadAvatar(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_UploadAvatar asyncDelegate = (RemoteServices.RemoteAsyncDelegate_UploadAvatar) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        UploadAvatar_ReturnType returnData = new UploadAvatar_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_MakePeople_UserCallBack(RemoteServices.MakePeople_UserCallBack callback)
    {
      this.makePeople_UserCallBack = callback;
    }

    public void MakePeople(int villageID, int personType)
    {
      if (this.MakePeople_Callback == null)
        this.MakePeople_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MakePeople);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_MakePeople(this.service.MakePeople).BeginInvoke(this.UserID, this.SessionID, villageID, personType, this.MakePeople_Callback, (object) null), typeof (MakePeople_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_MakePeople(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_MakePeople asyncDelegate = (RemoteServices.RemoteAsyncDelegate_MakePeople) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        MakePeople_ReturnType returnData = new MakePeople_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetUserPeople_UserCallBack(RemoteServices.GetUserPeople_UserCallBack callback)
    {
      this.getUserPeople_UserCallBack = callback;
    }

    public void GetUserPeople()
    {
      if (this.GetUserPeople_Callback == null)
        this.GetUserPeople_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetUserPeople);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetUserPeople(this.service.GetUserPeople).BeginInvoke(this.UserID, this.SessionID, this.GetUserPeople_Callback, (object) null), typeof (GetUserPeople_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetUserPeople(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetUserPeople asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetUserPeople) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetUserPeople_ReturnType returnData = new GetUserPeople_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetUserIDFromName_UserCallBack(
      RemoteServices.GetUserIDFromName_UserCallBack callback)
    {
      this.getUserIDFromName_UserCallBack = callback;
    }

    public void GetUserIDFromName(string targetUser)
    {
      if (this.GetUserIDFromName_Callback == null)
        this.GetUserIDFromName_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetUserIDFromName);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetUserIDFromName(this.service.GetUserIDFromName).BeginInvoke(this.UserID, this.SessionID, targetUser, this.GetUserIDFromName_Callback, (object) null), typeof (GetUserIDFromName_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetUserIDFromName(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetUserIDFromName asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetUserIDFromName) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetUserIDFromName_ReturnType returnData = new GetUserIDFromName_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetActivePeople_UserCallBack(
      RemoteServices.GetActivePeople_UserCallBack callback)
    {
      this.getActivePeople_UserCallBack = callback;
    }

    public void GetActivePeople(DateTime lastTime)
    {
      if (this.GetActivePeople_Callback == null)
        this.GetActivePeople_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetActivePeople);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetActivePeople(this.service.GetActivePeople).BeginInvoke(this.UserID, this.SessionID, lastTime, this.GetActivePeople_Callback, (object) null), typeof (GetActivePeople_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetActivePeople(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetActivePeople asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetActivePeople) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetActivePeople_ReturnType returnData = new GetActivePeople_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SendPeople_UserCallBack(RemoteServices.SendPeople_UserCallBack callback)
    {
      this.sendPeople_UserCallBack = callback;
    }

    public void SendPeople(
      int homeVillage,
      int targetVillage,
      int personType,
      int number,
      int command,
      int data)
    {
      if (this.SendPeople_Callback == null)
        this.SendPeople_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendPeople);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SendPeople(this.service.SendPeople).BeginInvoke(this.UserID, this.SessionID, homeVillage, targetVillage, personType, number, command, data, this.SendPeople_Callback, (object) null), typeof (SendPeople_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SendPeople(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SendPeople asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SendPeople) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SendPeople_ReturnType returnData = new SendPeople_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_RetrievePeople_UserCallBack(
      RemoteServices.RetrievePeople_UserCallBack callback)
    {
      this.retrievePeople_UserCallBack = callback;
    }

    public void RetrievePeople(List<long> people, int villageID, int personType)
    {
      if (this.RetrievePeople_Callback == null)
        this.RetrievePeople_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RetrievePeople);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_RetrievePeople(this.service.RetrievePeople).BeginInvoke(this.UserID, this.SessionID, villageID, people, personType, this.RetrievePeople_Callback, (object) null), typeof (RetrievePeople_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_RetrievePeople(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_RetrievePeople asyncDelegate = (RemoteServices.RemoteAsyncDelegate_RetrievePeople) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        RetrievePeople_ReturnType returnData = new RetrievePeople_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SpyCommand_UserCallBack(RemoteServices.SpyCommand_UserCallBack callback)
    {
      this.spyCommand_UserCallBack = callback;
    }

    public void SpyCommand(int villageID, int command)
    {
      if (this.SpyCommand_Callback == null)
        this.SpyCommand_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SpyCommand);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SpyCommand(this.service.SpyCommand).BeginInvoke(this.UserID, this.SessionID, villageID, command, this.SpyCommand_Callback, (object) null), typeof (SpyCommand_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SpyCommand(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SpyCommand asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SpyCommand) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SpyCommand_ReturnType returnData = new SpyCommand_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SpyGetVillageResourceInfo_UserCallBack(
      RemoteServices.SpyGetVillageResourceInfo_UserCallBack callback)
    {
      this.spyGetVillageResourceInfo_UserCallBack = callback;
    }

    public void SpyGetVillageResourceInfo(int villageID)
    {
      if (this.SpyGetVillageResourceInfo_Callback == null)
        this.SpyGetVillageResourceInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SpyGetVillageResourceInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SpyGetVillageResourceInfo(this.service.SpyGetVillageResourceInfo).BeginInvoke(this.UserID, this.SessionID, villageID, this.SpyGetVillageResourceInfo_Callback, (object) null), typeof (SpyGetVillageResourceInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SpyGetVillageResourceInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SpyGetVillageResourceInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SpyGetVillageResourceInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SpyGetVillageResourceInfo_ReturnType returnData = new SpyGetVillageResourceInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SpyGetArmyInfo_UserCallBack(
      RemoteServices.SpyGetArmyInfo_UserCallBack callback)
    {
      this.spyGetArmyInfo_UserCallBack = callback;
    }

    public void SpyGetArmyInfo(int villageID)
    {
      if (this.SpyGetArmyInfo_Callback == null)
        this.SpyGetArmyInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SpyGetArmyInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SpyGetArmyInfo(this.service.SpyGetArmyInfo).BeginInvoke(this.UserID, this.SessionID, villageID, this.SpyGetArmyInfo_Callback, (object) null), typeof (SpyGetArmyInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SpyGetArmyInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SpyGetArmyInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SpyGetArmyInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SpyGetArmyInfo_ReturnType returnData = new SpyGetArmyInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SpyGetResearchInfo_UserCallBack(
      RemoteServices.SpyGetResearchInfo_UserCallBack callback)
    {
      this.spyGetResearchInfo_UserCallBack = callback;
    }

    public void SpyGetResearchInfo(int villageID)
    {
      if (this.SpyGetResearchInfo_Callback == null)
        this.SpyGetResearchInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SpyGetResearchInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SpyGetResearchInfo(this.service.SpyGetResearchInfo).BeginInvoke(this.UserID, this.SessionID, villageID, this.SpyGetResearchInfo_Callback, (object) null), typeof (SpyGetResearchInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SpyGetResearchInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SpyGetResearchInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SpyGetResearchInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SpyGetResearchInfo_ReturnType returnData = new SpyGetResearchInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CreateFaction_UserCallBack(RemoteServices.CreateFaction_UserCallBack callback)
    {
      this.createFaction_UserCallBack = callback;
    }

    public void CreateFaction(
      string factionName,
      string factionNameabrv,
      string factionMotto,
      int flagdata)
    {
      if (this.CreateFaction_Callback == null)
        this.CreateFaction_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CreateFaction);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CreateFaction(this.service.CreateFaction).BeginInvoke(this.UserID, this.SessionID, factionName, factionNameabrv, factionMotto, flagdata, this.CreateFaction_Callback, (object) null), typeof (CreateFaction_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CreateFaction(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CreateFaction asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CreateFaction) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CreateFaction_ReturnType returnData = new CreateFaction_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_DisbandFaction_UserCallBack(
      RemoteServices.DisbandFaction_UserCallBack callback)
    {
      this.disbandFaction_UserCallBack = callback;
    }

    public void DisbandFaction()
    {
      if (this.DisbandFaction_Callback == null)
        this.DisbandFaction_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DisbandFaction);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DisbandFaction(this.service.DisbandFaction).BeginInvoke(this.UserID, this.SessionID, this.UserFactionID, this.DisbandFaction_Callback, (object) null), typeof (DisbandFaction_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_DisbandFaction(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_DisbandFaction asyncDelegate = (RemoteServices.RemoteAsyncDelegate_DisbandFaction) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        DisbandFaction_ReturnType returnData = new DisbandFaction_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_FactionSendInvite_UserCallBack(
      RemoteServices.FactionSendInvite_UserCallBack callback)
    {
      this.factionSendInvite_UserCallBack = callback;
    }

    public void FactionSendInvite(string targetUser)
    {
      if (this.FactionSendInvite_Callback == null)
        this.FactionSendInvite_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionSendInvite);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FactionSendInvite(this.service.FactionSendInvite).BeginInvoke(this.UserID, this.SessionID, targetUser, this.FactionSendInvite_Callback, (object) null), typeof (FactionSendInvite_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_FactionSendInvite(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_FactionSendInvite asyncDelegate = (RemoteServices.RemoteAsyncDelegate_FactionSendInvite) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        FactionSendInvite_ReturnType returnData = new FactionSendInvite_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_FactionWithdrawInvite_UserCallBack(
      RemoteServices.FactionWithdrawInvite_UserCallBack callback)
    {
      this.factionWithdrawInvite_UserCallBack = callback;
    }

    public void FactionWithdrawInvite(int targetUserID)
    {
      if (this.FactionWithdrawInvite_Callback == null)
        this.FactionWithdrawInvite_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionWithdrawInvite);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FactionWithdrawInvite(this.service.FactionWithdrawInvite).BeginInvoke(this.UserID, this.SessionID, targetUserID, this.FactionWithdrawInvite_Callback, (object) null), typeof (FactionWithdrawInvite_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_FactionWithdrawInvite(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_FactionWithdrawInvite asyncDelegate = (RemoteServices.RemoteAsyncDelegate_FactionWithdrawInvite) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        FactionWithdrawInvite_ReturnType returnData = new FactionWithdrawInvite_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_FactionReplyToInvite_UserCallBack(
      RemoteServices.FactionReplyToInvite_UserCallBack callback)
    {
      this.factionReplyToInvite_UserCallBack = callback;
    }

    public void FactionReplyToInvite(int factionID, bool accept)
    {
      if (this.FactionReplyToInvite_Callback == null)
        this.FactionReplyToInvite_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionReplyToInvite);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FactionReplyToInvite(this.service.FactionReplyToInvite).BeginInvoke(this.UserID, this.SessionID, factionID, accept, this.FactionReplyToInvite_Callback, (object) null), typeof (FactionReplyToInvite_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_FactionReplyToInvite(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_FactionReplyToInvite asyncDelegate = (RemoteServices.RemoteAsyncDelegate_FactionReplyToInvite) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        FactionReplyToInvite_ReturnType returnData = new FactionReplyToInvite_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_FactionChangeMemberStatus_UserCallBack(
      RemoteServices.FactionChangeMemberStatus_UserCallBack callback)
    {
      this.factionChangeMemberStatus_UserCallBack = callback;
    }

    public void FactionChangeMemberStatus(int memberUserID, int targetRank)
    {
      if (this.FactionChangeMemberStatus_Callback == null)
        this.FactionChangeMemberStatus_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionChangeMemberStatus);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FactionChangeMemberStatus(this.service.FactionChangeMemberStatus).BeginInvoke(this.UserID, this.SessionID, memberUserID, targetRank, this.FactionChangeMemberStatus_Callback, (object) null), typeof (FactionChangeMemberStatus_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_FactionChangeMemberStatus(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_FactionChangeMemberStatus asyncDelegate = (RemoteServices.RemoteAsyncDelegate_FactionChangeMemberStatus) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        FactionChangeMemberStatus_ReturnType returnData = new FactionChangeMemberStatus_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_FactionLeave_UserCallBack(RemoteServices.FactionLeave_UserCallBack callback)
    {
      this.factionLeave_UserCallBack = callback;
    }

    public void FactionLeave()
    {
      if (this.FactionLeave_Callback == null)
        this.FactionLeave_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionLeave);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FactionLeave(this.service.FactionLeave).BeginInvoke(this.UserID, this.SessionID, this.FactionLeave_Callback, (object) null), typeof (FactionLeave_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_FactionLeave(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_FactionLeave asyncDelegate = (RemoteServices.RemoteAsyncDelegate_FactionLeave) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        FactionLeave_ReturnType returnData = new FactionLeave_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetFactionData_UserCallBack(
      RemoteServices.GetFactionData_UserCallBack callback)
    {
      this.getFactionData_UserCallBack = callback;
    }

    public void GetFactionData(int factionID, long factionChangesPos)
    {
      if (this.GetFactionData_Callback == null)
        this.GetFactionData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetFactionData);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetFactionData(this.service.GetFactionData).BeginInvoke(this.UserID, this.SessionID, factionID, factionChangesPos, this.GetFactionData_Callback, (object) null), typeof (GetFactionData_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetFactionData(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetFactionData asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetFactionData) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetFactionData_ReturnType returnData = new GetFactionData_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CreateUserRelationship_UserCallBack(
      RemoteServices.CreateUserRelationship_UserCallBack callback)
    {
      this.createUserRelationship_UserCallBack = callback;
    }

    public void CreateUserRelationship(int targetUserID, int relationship)
    {
      if (this.CreateUserRelationship_Callback == null)
        this.CreateUserRelationship_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CreateUserRelationship);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CreateUserRelationship(this.service.CreateUserRelationship).BeginInvoke(this.UserID, this.SessionID, targetUserID, relationship, this.CreateUserRelationship_Callback, (object) null), typeof (CreateUserRelationship_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CreateUserRelationship(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CreateUserRelationship asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CreateUserRelationship) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CreateUserRelationship_ReturnType returnData = new CreateUserRelationship_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SetUserMarker_UserCallBack(RemoteServices.SetUserMarker_UserCallBack callback)
    {
      this.setUserMarker_UserCallBack = callback;
    }

    public void SetUserMarker(int targetUserID, int markerType)
    {
      if (this.SetUserMarker_Callback == null)
        this.SetUserMarker_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SetUserMarker);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SetUserMarker(this.service.SetUserMarker).BeginInvoke(this.UserID, this.SessionID, targetUserID, markerType, this.SetUserMarker_Callback, (object) null), typeof (SetUserMarker_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SetUserMarker(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SetUserMarker asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SetUserMarker) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SetUserMarker_ReturnType returnData = new SetUserMarker_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CreateFactionRelationship_UserCallBack(
      RemoteServices.CreateFactionRelationship_UserCallBack callback)
    {
      this.createFactionRelationship_UserCallBack = callback;
    }

    public void CreateFactionRelationship(int targetFactionID, int relationship)
    {
      if (this.CreateFactionRelationship_Callback == null)
        this.CreateFactionRelationship_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CreateFactionRelationship);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CreateFactionRelationship(this.service.CreateFactionRelationship).BeginInvoke(this.UserID, this.SessionID, targetFactionID, relationship, this.CreateFactionRelationship_Callback, (object) null), typeof (CreateFactionRelationship_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CreateFactionRelationship(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CreateFactionRelationship asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CreateFactionRelationship) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CreateFactionRelationship_ReturnType returnData = new CreateFactionRelationship_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CreateHouseRelationship_UserCallBack(
      RemoteServices.CreateHouseRelationship_UserCallBack callback)
    {
      this.createHouseRelationship_UserCallBack = callback;
    }

    public void CreateHouseRelationship(int targetHouseID, int relationship)
    {
      if (this.CreateHouseRelationship_Callback == null)
        this.CreateHouseRelationship_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CreateHouseRelationship);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CreateHouseRelationship(this.service.CreateHouseRelationship).BeginInvoke(this.UserID, this.SessionID, targetHouseID, relationship, this.CreateHouseRelationship_Callback, (object) null), typeof (CreateHouseRelationship_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CreateHouseRelationship(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CreateHouseRelationship asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CreateHouseRelationship) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CreateHouseRelationship_ReturnType returnData = new CreateHouseRelationship_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetHouseGloryPoints_UserCallBack(
      RemoteServices.GetHouseGloryPoints_UserCallBack callback)
    {
      this.getHouseGloryPoints_UserCallBack = callback;
    }

    public void GetHouseGloryPoints()
    {
      if (this.GetHouseGloryPoints_Callback == null)
        this.GetHouseGloryPoints_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetHouseGloryPoints);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetHouseGloryPoints(this.service.GetHouseGloryPoints).BeginInvoke(this.UserID, this.SessionID, this.GetHouseGloryPoints_Callback, (object) null), typeof (GetHouseGloryPoints_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetHouseGloryPoints(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetHouseGloryPoints asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetHouseGloryPoints) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetHouseGloryPoints_ReturnType returnData = new GetHouseGloryPoints_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_ChangeFactionMotto_UserCallBack(
      RemoteServices.ChangeFactionMotto_UserCallBack callback)
    {
      this.changeFactionMotto_UserCallBack = callback;
    }

    public void ChangeFactionMotto(
      string factionName,
      string factionNameAbrv,
      string motto,
      int flagData)
    {
      if (this.ChangeFactionMotto_Callback == null)
        this.ChangeFactionMotto_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ChangeFactionMotto);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ChangeFactionMotto(this.service.ChangeFactionMotto).BeginInvoke(this.UserID, this.SessionID, factionName, factionNameAbrv, motto, flagData, this.ChangeFactionMotto_Callback, (object) null), typeof (ChangeFactionMotto_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_ChangeFactionMotto(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_ChangeFactionMotto asyncDelegate = (RemoteServices.RemoteAsyncDelegate_ChangeFactionMotto) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        ChangeFactionMotto_ReturnType returnData = new ChangeFactionMotto_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_FactionLeadershipVote_UserCallBack(
      RemoteServices.FactionLeadershipVote_UserCallBack callback)
    {
      this.factionLeadershipVote_UserCallBack = callback;
    }

    public void FactionLeadershipVote(int factionID, int votedID)
    {
      if (this.FactionLeadershipVote_Callback == null)
        this.FactionLeadershipVote_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionLeadershipVote);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FactionLeadershipVote(this.service.FactionLeadershipVote).BeginInvoke(this.UserID, this.SessionID, factionID, votedID, this.FactionLeadershipVote_Callback, (object) null), typeof (FactionLeadershipVote_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_FactionLeadershipVote(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_FactionLeadershipVote asyncDelegate = (RemoteServices.RemoteAsyncDelegate_FactionLeadershipVote) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        FactionLeadershipVote_ReturnType returnData = new FactionLeadershipVote_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetViewFactionData_UserCallBack(
      RemoteServices.GetViewFactionData_UserCallBack callback)
    {
      this.getViewFactionData_UserCallBack = callback;
    }

    public void GetViewFactionData(int factionID)
    {
      if (this.GetViewFactionData_Callback == null)
        this.GetViewFactionData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetViewFactionData);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetViewFactionData(this.service.GetViewFactionData).BeginInvoke(this.UserID, this.SessionID, factionID, this.GetViewFactionData_Callback, (object) null), typeof (GetViewFactionData_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetViewFactionData(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetViewFactionData asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetViewFactionData) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetViewFactionData_ReturnType returnData = new GetViewFactionData_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetViewHouseData_UserCallBack(
      RemoteServices.GetViewHouseData_UserCallBack callback)
    {
      this.getViewHouseData_UserCallBack = callback;
    }

    public void GetViewHouseData(int houseID)
    {
      if (this.GetViewHouseData_Callback == null)
        this.GetViewHouseData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetViewHouseData);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetViewHouseData(this.service.GetViewHouseData).BeginInvoke(this.UserID, this.SessionID, houseID, this.GetViewHouseData_Callback, (object) null), typeof (GetViewHouseData_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetViewHouseData(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetViewHouseData asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetViewHouseData) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetViewHouseData_ReturnType returnData = new GetViewHouseData_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SelfJoinHouse_UserCallBack(RemoteServices.SelfJoinHouse_UserCallBack callback)
    {
      this.selfJoinHouse_UserCallBack = callback;
    }

    public void SelfJoinHouse(int factionID, int houseID, long factionsChangePos)
    {
      if (this.SelfJoinHouse_Callback == null)
        this.SelfJoinHouse_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SelfJoinHouse);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SelfJoinHouse(this.service.SelfJoinHouse).BeginInvoke(this.UserID, this.SessionID, factionID, houseID, factionsChangePos, this.SelfJoinHouse_Callback, (object) null), typeof (SelfJoinHouse_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SelfJoinHouse(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SelfJoinHouse asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SelfJoinHouse) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SelfJoinHouse_ReturnType returnData = new SelfJoinHouse_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_HouseVote_UserCallBack(RemoteServices.HouseVote_UserCallBack callback)
    {
      this.houseVote_UserCallBack = callback;
    }

    public void HouseVote(int targetFaction, bool application, bool vote, long factionsChangePos)
    {
      FactionData yourFaction = GameEngine.Instance.World.YourFaction;
      if (yourFaction == null)
        return;
      if (this.HouseVote_Callback == null)
        this.HouseVote_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_HouseVote);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_HouseVote(this.service.HouseVote).BeginInvoke(this.UserID, this.SessionID, yourFaction.factionID, yourFaction.houseID, targetFaction, application, vote, factionsChangePos, this.HouseVote_Callback, (object) null), typeof (HouseVote_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_HouseVote(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_HouseVote asyncDelegate = (RemoteServices.RemoteAsyncDelegate_HouseVote) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        HouseVote_ReturnType returnData = new HouseVote_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_HouseVoteHouseLeader_UserCallBack(
      RemoteServices.HouseVoteHouseLeader_UserCallBack callback)
    {
      this.houseVoteHouseLeader_UserCallBack = callback;
    }

    public void HouseVoteHouseLeader(
      int factionID,
      int houseID,
      int votedFactionID,
      long factionsChangePos)
    {
      if (this.HouseVoteHouseLeader_Callback == null)
        this.HouseVoteHouseLeader_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_HouseVoteHouseLeader);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_HouseVoteHouseLeader(this.service.HouseVoteHouseLeader).BeginInvoke(this.UserID, this.SessionID, factionID, houseID, votedFactionID, factionsChangePos, this.HouseVoteHouseLeader_Callback, (object) null), typeof (HouseVoteHouseLeader_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_HouseVoteHouseLeader(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_HouseVoteHouseLeader asyncDelegate = (RemoteServices.RemoteAsyncDelegate_HouseVoteHouseLeader) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        HouseVoteHouseLeader_ReturnType returnData = new HouseVoteHouseLeader_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_TouchHouseVisitDate_UserCallBack(
      RemoteServices.TouchHouseVisitDate_UserCallBack callback)
    {
      this.touchHouseVisitDate_UserCallBack = callback;
    }

    public void TouchHouseVisitDate(int factionID)
    {
      if (this.TouchHouseVisitDate_Callback == null)
        this.TouchHouseVisitDate_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_TouchHouseVisitDate);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_TouchHouseVisitDate(this.service.TouchHouseVisitDate).BeginInvoke(this.UserID, this.SessionID, factionID, this.TouchHouseVisitDate_Callback, (object) null), typeof (TouchHouseVisitDate_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_TouchHouseVisitDate(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_TouchHouseVisitDate asyncDelegate = (RemoteServices.RemoteAsyncDelegate_TouchHouseVisitDate) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        TouchHouseVisitDate_ReturnType returnData = new TouchHouseVisitDate_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_LeaveHouse_UserCallBack(RemoteServices.LeaveHouse_UserCallBack callback)
    {
      this.leaveHouse_UserCallBack = callback;
    }

    public void LeaveHouse(int factionID, int houseID, long factionsChangePos)
    {
      if (this.LeaveHouse_Callback == null)
        this.LeaveHouse_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LeaveHouse);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_LeaveHouse(this.service.LeaveHouse).BeginInvoke(this.UserID, this.SessionID, factionID, houseID, factionsChangePos, this.LeaveHouse_Callback, (object) null), typeof (LeaveHouse_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_LeaveHouse(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_LeaveHouse asyncDelegate = (RemoteServices.RemoteAsyncDelegate_LeaveHouse) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        LeaveHouse_ReturnType returnData = new LeaveHouse_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_FactionApplication_UserCallBack(
      RemoteServices.FactionApplication_UserCallBack callback)
    {
      this.factionApplication_UserCallBack = callback;
    }

    public void FactionApplication(int factionID)
    {
      if (this.FactionApplication_Callback == null)
        this.FactionApplication_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionApplication);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FactionApplication(this.service.FactionApplication).BeginInvoke(this.UserID, this.SessionID, factionID, false, this.FactionApplication_Callback, (object) null), typeof (FactionApplication_ReturnType));
    }

    public void FactionApplicationCancel(int factionID)
    {
      if (this.FactionApplication_Callback == null)
        this.FactionApplication_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionApplication);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FactionApplication(this.service.FactionApplication).BeginInvoke(this.UserID, this.SessionID, factionID, true, this.FactionApplication_Callback, (object) null), typeof (FactionApplication_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_FactionApplication(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_FactionApplication asyncDelegate = (RemoteServices.RemoteAsyncDelegate_FactionApplication) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        FactionApplication_ReturnType returnData = new FactionApplication_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_FactionApplicationProcessing_UserCallBack(
      RemoteServices.FactionApplicationProcessing_UserCallBack callback)
    {
      this.factionApplicationProcessing_UserCallBack = callback;
    }

    public void FactionApplicationAccept(int userID)
    {
      if (this.FactionApplicationProcessing_Callback == null)
        this.FactionApplicationProcessing_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionApplicationProcessing);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FactionApplicationProcessing(this.service.FactionApplicationProcessing).BeginInvoke(this.UserID, this.SessionID, userID, true, false, false, this.FactionApplicationProcessing_Callback, (object) null), typeof (FactionApplicationProcessing_ReturnType));
    }

    public void FactionApplicationReject(int userID)
    {
      if (this.FactionApplicationProcessing_Callback == null)
        this.FactionApplicationProcessing_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionApplicationProcessing);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FactionApplicationProcessing(this.service.FactionApplicationProcessing).BeginInvoke(this.UserID, this.SessionID, userID, false, true, false, this.FactionApplicationProcessing_Callback, (object) null), typeof (FactionApplicationProcessing_ReturnType));
    }

    public void FactionApplicationSetMode(bool accepting)
    {
      if (this.FactionApplicationProcessing_Callback == null)
        this.FactionApplicationProcessing_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionApplicationProcessing);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FactionApplicationProcessing(this.service.FactionApplicationProcessing).BeginInvoke(this.UserID, this.SessionID, -1, false, false, accepting, this.FactionApplicationProcessing_Callback, (object) null), typeof (FactionApplicationProcessing_ReturnType));
    }

    public void FactionApplicationGoHeretic()
    {
      if (this.FactionApplicationProcessing_Callback == null)
        this.FactionApplicationProcessing_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionApplicationProcessing);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FactionApplicationProcessing(this.service.FactionApplicationProcessing).BeginInvoke(this.UserID, this.SessionID, 4, true, true, true, this.FactionApplicationProcessing_Callback, (object) null), typeof (FactionApplicationProcessing_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_FactionApplicationProcessing(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_FactionApplicationProcessing asyncDelegate = (RemoteServices.RemoteAsyncDelegate_FactionApplicationProcessing) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        FactionApplicationProcessing_ReturnType returnData = new FactionApplicationProcessing_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetParishMembersList_UserCallBack(
      RemoteServices.GetParishMembersList_UserCallBack callback)
    {
      this.getParishMembersList_UserCallBack = callback;
    }

    public void GetParishMembersList(int villageID)
    {
      if (this.GetParishMembersList_Callback == null)
        this.GetParishMembersList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetParishMembersList);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetParishMembersList(this.service.GetParishMembersList).BeginInvoke(this.UserID, this.SessionID, villageID, this.GetParishMembersList_Callback, (object) null), typeof (GetParishMembersList_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetParishMembersList(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetParishMembersList asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetParishMembersList) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetParishMembersList_ReturnType returnData = new GetParishMembersList_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetParishFrontPageInfo_UserCallBack(
      RemoteServices.GetParishFrontPageInfo_UserCallBack callback)
    {
      this.getParishFrontPageInfo_UserCallBack = callback;
    }

    public void GetParishFrontPageInfo(int villageID, DateTime lastTime)
    {
      if (this.GetParishFrontPageInfo_Callback == null)
        this.GetParishFrontPageInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetParishFrontPageInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetParishFrontPageInfo(this.service.GetParishFrontPageInfo).BeginInvoke(this.UserID, this.SessionID, villageID, lastTime, this.GetParishFrontPageInfo_Callback, (object) null), typeof (GetParishFrontPageInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetParishFrontPageInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetParishFrontPageInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetParishFrontPageInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetParishFrontPageInfo_ReturnType returnData = new GetParishFrontPageInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_ParishWallDetailInfo_UserCallBack(
      RemoteServices.ParishWallDetailInfo_UserCallBack callback)
    {
      this.parishWallDetailInfo_UserCallBack = callback;
    }

    public void ParishWallDetailInfo(int parishCapitalID, long wallInfoID)
    {
      if (this.ParishWallDetailInfo_Callback == null)
        this.ParishWallDetailInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ParishWallDetailInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ParishWallDetailInfo(this.service.ParishWallDetailInfo).BeginInvoke(this.UserID, this.SessionID, parishCapitalID, wallInfoID, -1, -1, this.ParishWallDetailInfo_Callback, (object) null), typeof (ParishWallDetailInfo_ReturnType));
    }

    public void ParishWallDetailInfo(int parishCapitalID, int targetUserId, int wallType)
    {
      if (this.ParishWallDetailInfo_Callback == null)
        this.ParishWallDetailInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ParishWallDetailInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_ParishWallDetailInfo(this.service.ParishWallDetailInfo).BeginInvoke(this.UserID, this.SessionID, parishCapitalID, -1L, targetUserId, wallType, this.ParishWallDetailInfo_Callback, (object) null), typeof (ParishWallDetailInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_ParishWallDetailInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_ParishWallDetailInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_ParishWallDetailInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        ParishWallDetailInfo_ReturnType returnData = new ParishWallDetailInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_StandDownAsParishDespot_UserCallBack(
      RemoteServices.StandDownAsParishDespot_UserCallBack callback)
    {
      this.standDownAsParishDespot_UserCallBack = callback;
    }

    public void StandDownAsParishDespot(int villageID)
    {
      if (this.StandDownAsParishDespot_Callback == null)
        this.StandDownAsParishDespot_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_StandDownAsParishDespot);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_StandDownAsParishDespot(this.service.StandDownAsParishDespot).BeginInvoke(this.UserID, this.SessionID, villageID, this.StandDownAsParishDespot_Callback, (object) null), typeof (StandDownAsParishDespot_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_StandDownAsParishDespot(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_StandDownAsParishDespot asyncDelegate = (RemoteServices.RemoteAsyncDelegate_StandDownAsParishDespot) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        StandDownAsParishDespot_ReturnType returnData = new StandDownAsParishDespot_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_MakeParishVote_UserCallBack(
      RemoteServices.MakeParishVote_UserCallBack callback)
    {
      this.makeParishVote_UserCallBack = callback;
    }

    public void MakeParishVote(int villageID, int votedUserID)
    {
      if (this.MakeParishVote_Callback == null)
        this.MakeParishVote_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MakeParishVote);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_MakeParishVote(this.service.MakeParishVote).BeginInvoke(this.UserID, this.SessionID, villageID, votedUserID, this.MakeParishVote_Callback, (object) null), typeof (MakeParishVote_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_MakeParishVote(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_MakeParishVote asyncDelegate = (RemoteServices.RemoteAsyncDelegate_MakeParishVote) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        MakeParishVote_ReturnType returnData = new MakeParishVote_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SendTroopsToCapital_UserCallBack(
      RemoteServices.SendTroopsToCapital_UserCallBack callback)
    {
      this.sendTroopsToCapital_UserCallBack = callback;
    }

    public void SendTroopsToCapital(
      int sourceVillageID,
      int targetVillageID,
      int peasants,
      int archers,
      int pikemen,
      int swordsmen,
      int catapults)
    {
      if (this.SendTroopsToCapital_Callback == null)
        this.SendTroopsToCapital_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendTroopsToCapital);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SendTroopsToCapital(this.service.SendTroopsToCapital).BeginInvoke(this.UserID, this.SessionID, sourceVillageID, targetVillageID, peasants, archers, pikemen, swordsmen, catapults, this.SendTroopsToCapital_Callback, (object) null), typeof (SendTroopsToCapital_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SendTroopsToCapital(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SendTroopsToCapital asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SendTroopsToCapital) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SendTroopsToCapital_ReturnType returnData = new SendTroopsToCapital_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetCapitalBarracksSpace_UserCallBack(
      RemoteServices.GetCapitalBarracksSpace_UserCallBack callback)
    {
      this.getCapitalBarracksSpace_UserCallBack = callback;
    }

    public void GetCapitalBarracksSpace(int sourceVillageID, int targetVillageID)
    {
      if (this.GetCapitalBarracksSpace_Callback == null)
        this.GetCapitalBarracksSpace_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetCapitalBarracksSpace);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetCapitalBarracksSpace(this.service.GetCapitalBarracksSpace).BeginInvoke(this.UserID, this.SessionID, sourceVillageID, targetVillageID, this.GetCapitalBarracksSpace_Callback, (object) null), typeof (GetCapitalBarracksSpace_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetCapitalBarracksSpace(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetCapitalBarracksSpace asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetCapitalBarracksSpace) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetCapitalBarracksSpace_ReturnType returnData = new GetCapitalBarracksSpace_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetCountyElectionInfo_UserCallBack(
      RemoteServices.GetCountyElectionInfo_UserCallBack callback)
    {
      this.getCountyElectionInfo_UserCallBack = callback;
    }

    public void GetCountyElectionInfo(int villageID)
    {
      if (this.GetCountyElectionInfo_Callback == null)
        this.GetCountyElectionInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetCountyElectionInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetCountyElectionInfo(this.service.GetCountyElectionInfo).BeginInvoke(this.UserID, this.SessionID, villageID, this.GetCountyElectionInfo_Callback, (object) null), typeof (GetCountyElectionInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetCountyElectionInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetCountyElectionInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetCountyElectionInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetCountyElectionInfo_ReturnType returnData = new GetCountyElectionInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetCountyFrontPageInfo_UserCallBack(
      RemoteServices.GetCountyFrontPageInfo_UserCallBack callback)
    {
      this.getCountyFrontPageInfo_UserCallBack = callback;
    }

    public void GetCountyFrontPageInfo(int villageID)
    {
      if (this.GetCountyFrontPageInfo_Callback == null)
        this.GetCountyFrontPageInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetCountyFrontPageInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetCountyFrontPageInfo(this.service.GetCountyFrontPageInfo).BeginInvoke(this.UserID, this.SessionID, villageID, this.GetCountyFrontPageInfo_Callback, (object) null), typeof (GetCountyFrontPageInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetCountyFrontPageInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetCountyFrontPageInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetCountyFrontPageInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetCountyFrontPageInfo_ReturnType returnData = new GetCountyFrontPageInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_MakeCountyVote_UserCallBack(
      RemoteServices.MakeCountyVote_UserCallBack callback)
    {
      this.makeCountyVote_UserCallBack = callback;
    }

    public void MakeCountyVote(int villageID, int votedParishID)
    {
      if (this.MakeCountyVote_Callback == null)
        this.MakeCountyVote_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MakeCountyVote);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_MakeCountyVote(this.service.MakeCountyVote).BeginInvoke(this.UserID, this.SessionID, villageID, votedParishID, this.MakeCountyVote_Callback, (object) null), typeof (MakeCountyVote_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_MakeCountyVote(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_MakeCountyVote asyncDelegate = (RemoteServices.RemoteAsyncDelegate_MakeCountyVote) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        MakeCountyVote_ReturnType returnData = new MakeCountyVote_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetProvinceElectionInfo_UserCallBack(
      RemoteServices.GetProvinceElectionInfo_UserCallBack callback)
    {
      this.getProvinceElectionInfo_UserCallBack = callback;
    }

    public void GetProvinceElectionInfo(int villageID)
    {
      if (this.GetProvinceElectionInfo_Callback == null)
        this.GetProvinceElectionInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetProvinceElectionInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetProvinceElectionInfo(this.service.GetProvinceElectionInfo).BeginInvoke(this.UserID, this.SessionID, villageID, this.GetProvinceElectionInfo_Callback, (object) null), typeof (GetProvinceElectionInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetProvinceElectionInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetProvinceElectionInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetProvinceElectionInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetProvinceElectionInfo_ReturnType returnData = new GetProvinceElectionInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetProvinceFrontPageInfo_UserCallBack(
      RemoteServices.GetProvinceFrontPageInfo_UserCallBack callback)
    {
      this.getProvinceFrontPageInfo_UserCallBack = callback;
    }

    public void GetProvinceFrontPageInfo(int villageID)
    {
      if (this.GetProvinceFrontPageInfo_Callback == null)
        this.GetProvinceFrontPageInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetProvinceFrontPageInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetProvinceFrontPageInfo(this.service.GetProvinceFrontPageInfo).BeginInvoke(this.UserID, this.SessionID, villageID, this.GetProvinceFrontPageInfo_Callback, (object) null), typeof (GetProvinceFrontPageInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetProvinceFrontPageInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetProvinceFrontPageInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetProvinceFrontPageInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetProvinceFrontPageInfo_ReturnType returnData = new GetProvinceFrontPageInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_MakeProvinceVote_UserCallBack(
      RemoteServices.MakeProvinceVote_UserCallBack callback)
    {
      this.makeProvinceVote_UserCallBack = callback;
    }

    public void MakeProvinceVote(int villageID, int votedParishID)
    {
      if (this.MakeProvinceVote_Callback == null)
        this.MakeProvinceVote_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MakeProvinceVote);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_MakeProvinceVote(this.service.MakeProvinceVote).BeginInvoke(this.UserID, this.SessionID, villageID, votedParishID, this.MakeProvinceVote_Callback, (object) null), typeof (MakeProvinceVote_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_MakeProvinceVote(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_MakeProvinceVote asyncDelegate = (RemoteServices.RemoteAsyncDelegate_MakeProvinceVote) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        MakeProvinceVote_ReturnType returnData = new MakeProvinceVote_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetCountryElectionInfo_UserCallBack(
      RemoteServices.GetCountryElectionInfo_UserCallBack callback)
    {
      this.getCountryElectionInfo_UserCallBack = callback;
    }

    public void GetCountryElectionInfo(int villageID)
    {
      if (this.GetCountryElectionInfo_Callback == null)
        this.GetCountryElectionInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetCountryElectionInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetCountryElectionInfo(this.service.GetCountryElectionInfo).BeginInvoke(this.UserID, this.SessionID, villageID, this.GetCountryElectionInfo_Callback, (object) null), typeof (GetCountryElectionInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetCountryElectionInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetCountryElectionInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetCountryElectionInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetCountryElectionInfo_ReturnType returnData = new GetCountryElectionInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetCountryFrontPageInfo_UserCallBack(
      RemoteServices.GetCountryFrontPageInfo_UserCallBack callback)
    {
      this.getCountryFrontPageInfo_UserCallBack = callback;
    }

    public void GetCountryFrontPageInfo(int villageID)
    {
      if (this.GetCountryFrontPageInfo_Callback == null)
        this.GetCountryFrontPageInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetCountryFrontPageInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetCountryFrontPageInfo(this.service.GetCountryFrontPageInfo).BeginInvoke(this.UserID, this.SessionID, villageID, this.GetCountryFrontPageInfo_Callback, (object) null), typeof (GetCountryFrontPageInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetCountryFrontPageInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetCountryFrontPageInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetCountryFrontPageInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetCountryFrontPageInfo_ReturnType returnData = new GetCountryFrontPageInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_MakeCountryVote_UserCallBack(
      RemoteServices.MakeCountryVote_UserCallBack callback)
    {
      this.makeCountryVote_UserCallBack = callback;
    }

    public void MakeCountryVote(int villageID, int votedParishID)
    {
      if (this.MakeCountryVote_Callback == null)
        this.MakeCountryVote_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MakeCountryVote);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_MakeCountryVote(this.service.MakeCountryVote).BeginInvoke(this.UserID, this.SessionID, villageID, votedParishID, this.MakeCountryVote_Callback, (object) null), typeof (MakeCountryVote_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_MakeCountryVote(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_MakeCountryVote asyncDelegate = (RemoteServices.RemoteAsyncDelegate_MakeCountryVote) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        MakeCountryVote_ReturnType returnData = new MakeCountryVote_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetIngameMessage_UserCallBack(
      RemoteServices.GetIngameMessage_UserCallBack callback)
    {
      this.getIngameMessage_UserCallBack = callback;
    }

    public void GetIngameMessage()
    {
      if (this.GetIngameMessage_Callback == null)
        this.GetIngameMessage_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetIngameMessage);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetIngameMessage(this.service.GetIngameMessage).BeginInvoke(this.UserID, this.SessionID, this.GetIngameMessage_Callback, (object) null), typeof (GetIngameMessage_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetIngameMessage(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetIngameMessage asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetIngameMessage) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetIngameMessage_ReturnType returnData = new GetIngameMessage_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CancelInterdiction_UserCallBack(
      RemoteServices.CancelInterdiction_UserCallBack callback)
    {
      this.cancelInterdiction_UserCallBack = callback;
    }

    public void CancelInterdiction(int villageID)
    {
      if (this.CancelInterdiction_Callback == null)
        this.CancelInterdiction_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CancelInterdiction);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CancelInterdiction(this.service.CancelInterdiction).BeginInvoke(this.UserID, this.SessionID, villageID, this.CancelInterdiction_Callback, (object) null), typeof (CancelInterdiction_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CancelInterdiction(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CancelInterdiction asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CancelInterdiction) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CancelInterdiction_ReturnType returnData = new CancelInterdiction_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetExcommunicationStatus_UserCallBack(
      RemoteServices.GetExcommunicationStatus_UserCallBack callback)
    {
      this.getExcommunicationStatus_UserCallBack = callback;
    }

    public void GetExcommunicationStatus(int villageID, int targetVillageID)
    {
      if (this.GetExcommunicationStatus_Callback == null)
        this.GetExcommunicationStatus_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetExcommunicationStatus);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetExcommunicationStatus(this.service.GetExcommunicationStatus).BeginInvoke(this.UserID, this.SessionID, villageID, targetVillageID, this.GetExcommunicationStatus_Callback, (object) null), typeof (GetExcommunicationStatus_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetExcommunicationStatus(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetExcommunicationStatus asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetExcommunicationStatus) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetExcommunicationStatus_ReturnType returnData = new GetExcommunicationStatus_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_DisbandTroops_UserCallBack(RemoteServices.DisbandTroops_UserCallBack callback)
    {
      this.disbandTroops_UserCallBack = callback;
    }

    public void DisbandTroops(int villageID, int troopType, int amount)
    {
      if (this.DisbandTroops_Callback == null)
        this.DisbandTroops_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DisbandTroops);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DisbandTroops(this.service.DisbandTroops).BeginInvoke(this.UserID, this.SessionID, villageID, troopType, amount, this.DisbandTroops_Callback, (object) null), typeof (DisbandTroops_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_DisbandTroops(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_DisbandTroops asyncDelegate = (RemoteServices.RemoteAsyncDelegate_DisbandTroops) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        DisbandTroops_ReturnType returnData = new DisbandTroops_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_DisbandPeople_UserCallBack(RemoteServices.DisbandPeople_UserCallBack callback)
    {
      this.disbandPeople_UserCallBack = callback;
    }

    public void DisbandPeople(int villageID, int troopType, int amount)
    {
      if (this.DisbandPeople_Callback == null)
        this.DisbandPeople_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DisbandPeople);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DisbandPeople(this.service.DisbandPeople).BeginInvoke(this.UserID, this.SessionID, villageID, troopType, amount, this.DisbandPeople_Callback, (object) null), typeof (DisbandPeople_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_DisbandPeople(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_DisbandPeople asyncDelegate = (RemoteServices.RemoteAsyncDelegate_DisbandPeople) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        DisbandPeople_ReturnType returnData = new DisbandPeople_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetVillageInfoForDonateCapitalGoods_UserCallBack(
      RemoteServices.GetVillageInfoForDonateCapitalGoods_UserCallBack callback)
    {
      this.getVillageInfoForDonateCapitalGoods_UserCallBack = callback;
    }

    public void GetVillageInfoForDonateCapitalGoods(int parishCapitalID, int targetBuildingType)
    {
      if (this.GetVillageInfoForDonateCapitalGoods_Callback == null)
        this.GetVillageInfoForDonateCapitalGoods_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetVillageInfoForDonateCapitalGoods);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetVillageInfoForDonateCapitalGoods(this.service.GetVillageInfoForDonateCapitalGoods).BeginInvoke(this.UserID, this.SessionID, parishCapitalID, targetBuildingType, this.GetVillageInfoForDonateCapitalGoods_Callback, (object) null), typeof (GetVillageInfoForDonateCapitalGoods_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetVillageInfoForDonateCapitalGoods(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetVillageInfoForDonateCapitalGoods asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetVillageInfoForDonateCapitalGoods) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetVillageInfoForDonateCapitalGoods_ReturnType returnData = new GetVillageInfoForDonateCapitalGoods_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_DonateCapitalGoods_UserCallBack(
      RemoteServices.DonateCapitalGoods_UserCallBack callback)
    {
      this.donateCapitalGoods_UserCallBack = callback;
    }

    public void DonateCapitalGoods(
      int targetVillageID,
      int sourceVillageID,
      int resourceType,
      int amount,
      int buildingType,
      long targetBuildingID)
    {
      if (this.DonateCapitalGoods_Callback == null)
        this.DonateCapitalGoods_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DonateCapitalGoods);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_DonateCapitalGoods(this.service.DonateCapitalGoods).BeginInvoke(this.UserID, this.SessionID, targetVillageID, sourceVillageID, resourceType, amount, buildingType, targetBuildingID, this.DonateCapitalGoods_Callback, (object) null), typeof (DonateCapitalGoods_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_DonateCapitalGoods(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_DonateCapitalGoods asyncDelegate = (RemoteServices.RemoteAsyncDelegate_DonateCapitalGoods) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        DonateCapitalGoods_ReturnType returnData = new DonateCapitalGoods_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetVillageStartLocations_UserCallBack(
      RemoteServices.GetVillageStartLocations_UserCallBack callback)
    {
      this.getVillageStartLocations_UserCallBack = callback;
    }

    public void GetVillageStartLocations()
    {
      if (this.GetVillageStartLocations_Callback == null)
        this.GetVillageStartLocations_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetVillageStartLocations);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetVillageStartLocations(this.service.GetVillageStartLocations).BeginInvoke(this.UserID, this.SessionID, this.GetVillageStartLocations_Callback, (object) null), typeof (GetVillageStartLocations_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetVillageStartLocations(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetVillageStartLocations asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetVillageStartLocations) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetVillageStartLocations_ReturnType returnData = new GetVillageStartLocations_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SetStartingCounty_UserCallBack(
      RemoteServices.SetStartingCounty_UserCallBack callback)
    {
      this.setStartingCounty_UserCallBack = callback;
    }

    public void SetStartingCounty(int countyID)
    {
      if (this.SetStartingCounty_Callback == null)
        this.SetStartingCounty_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SetStartingCounty);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SetStartingCounty(this.service.SetStartingCounty).BeginInvoke(this.UserID, this.SessionID, countyID, this.SetStartingCounty_Callback, (object) null), typeof (SetStartingCounty_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SetStartingCounty(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SetStartingCounty asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SetStartingCounty) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SetStartingCounty_ReturnType returnData = new SetStartingCounty_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CancelCard_UserCallBack(RemoteServices.CancelCard_UserCallBack callback)
    {
      this.cancelCard_UserCallBack = callback;
    }

    public void CancelCard(int card)
    {
      if (this.CancelCard_Callback == null)
        this.CancelCard_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CancelCard);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CancelCard(this.service.CancelCard).BeginInvoke(this.UserID, this.SessionID, card, this.CancelCard_Callback, (object) null), typeof (CancelCard_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CancelCard(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CancelCard asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CancelCard) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CancelCard_ReturnType returnData = new CancelCard_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_UpdateCurrentCards_UserCallBack(
      RemoteServices.UpdateCurrentCards_UserCallBack callback)
    {
      this.updateCurrentCards_UserCallBack = callback;
    }

    public void UpdateCurrentCards()
    {
      if (this.UpdateCurrentCards_Callback == null)
        this.UpdateCurrentCards_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpdateCurrentCards);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_UpdateCurrentCards(this.service.UpdateCurrentCards).BeginInvoke(this.UserID, this.SessionID, this.UpdateCurrentCards_Callback, (object) null), typeof (UpdateCurrentCards_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_UpdateCurrentCards(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_UpdateCurrentCards asyncDelegate = (RemoteServices.RemoteAsyncDelegate_UpdateCurrentCards) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        UpdateCurrentCards_ReturnType returnData = new UpdateCurrentCards_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_TutorialCommand_UserCallBack(
      RemoteServices.TutorialCommand_UserCallBack callback)
    {
      this.tutorialCommand_UserCallBack = callback;
    }

    public void TutorialCommand(int command)
    {
      if (this.TutorialCommand_Callback == null)
        this.TutorialCommand_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_TutorialCommand);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_TutorialCommand(this.service.TutorialCommand).BeginInvoke(this.UserID, this.SessionID, command, this.TutorialCommand_Callback, (object) null), typeof (TutorialCommand_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_TutorialCommand(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_TutorialCommand asyncDelegate = (RemoteServices.RemoteAsyncDelegate_TutorialCommand) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        TutorialCommand_ReturnType returnData = new TutorialCommand_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetQuestStatus_UserCallBack(
      RemoteServices.GetQuestStatus_UserCallBack callback)
    {
      this.getQuestStatus_UserCallBack = callback;
    }

    public void GetQuestStatus()
    {
      if (this.GetQuestStatus_Callback == null)
        this.GetQuestStatus_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetQuestStatus);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetQuestStatus(this.service.GetQuestStatus).BeginInvoke(this.UserID, this.SessionID, this.GetQuestStatus_Callback, (object) null), typeof (GetQuestStatus_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetQuestStatus(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetQuestStatus asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetQuestStatus) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetQuestStatus_ReturnType returnData = new GetQuestStatus_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CompleteQuest_UserCallBack(RemoteServices.CompleteQuest_UserCallBack callback)
    {
      this.completeQuest_UserCallBack = callback;
    }

    public void CompleteQuest(int quest)
    {
      if (this.CompleteQuest_Callback == null)
        this.CompleteQuest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CompleteQuest);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CompleteQuest(this.service.CompleteQuest).BeginInvoke(this.UserID, this.SessionID, quest, this.CompleteQuest_Callback, (object) null), typeof (CompleteQuest_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CompleteQuest(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CompleteQuest asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CompleteQuest) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CompleteQuest_ReturnType returnData = new CompleteQuest_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_FlagQuestObjectiveComplete_UserCallBack(
      RemoteServices.FlagQuestObjectiveComplete_UserCallBack callback)
    {
      this.flagQuestObjectiveComplete_UserCallBack = callback;
    }

    public void FlagQuestObjectiveComplete(int objective)
    {
      if (this.FlagQuestObjectiveComplete_Callback == null)
        this.FlagQuestObjectiveComplete_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FlagQuestObjectiveComplete);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_FlagQuestObjectiveComplete(this.service.FlagQuestObjectiveComplete).BeginInvoke(this.UserID, this.SessionID, objective, this.FlagQuestObjectiveComplete_Callback, (object) null), typeof (FlagQuestObjectiveComplete_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_FlagQuestObjectiveComplete(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_FlagQuestObjectiveComplete asyncDelegate = (RemoteServices.RemoteAsyncDelegate_FlagQuestObjectiveComplete) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        FlagQuestObjectiveComplete_ReturnType returnData = new FlagQuestObjectiveComplete_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CheckQuestObjectiveComplete_UserCallBack(
      RemoteServices.CheckQuestObjectiveComplete_UserCallBack callback)
    {
      this.checkQuestObjectiveComplete_UserCallBack = callback;
    }

    public void CheckQuestObjectiveComplete(int quest)
    {
      if (this.CheckQuestObjectiveComplete_Callback == null)
        this.CheckQuestObjectiveComplete_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CheckQuestObjectiveComplete);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CheckQuestObjectiveComplete(this.service.CheckQuestObjectiveComplete).BeginInvoke(this.UserID, this.SessionID, quest, this.CheckQuestObjectiveComplete_Callback, (object) null), typeof (CheckQuestObjectiveComplete_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CheckQuestObjectiveComplete(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CheckQuestObjectiveComplete asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CheckQuestObjectiveComplete) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CheckQuestObjectiveComplete_ReturnType returnData = new CheckQuestObjectiveComplete_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_UpdateDiplomacyStatus_UserCallBack(
      RemoteServices.UpdateDiplomacyStatus_UserCallBack callback)
    {
      this.updateDiplomacyStatus_UserCallBack = callback;
    }

    public void UpdateDiplomacyStatus(bool state)
    {
      if (this.UpdateDiplomacyStatus_Callback == null)
        this.UpdateDiplomacyStatus_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpdateDiplomacyStatus);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_UpdateDiplomacyStatus(this.service.UpdateDiplomacyStatus).BeginInvoke(this.UserID, this.SessionID, state, this.UpdateDiplomacyStatus_Callback, (object) null), typeof (UpdateDiplomacyStatus_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_UpdateDiplomacyStatus(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_UpdateDiplomacyStatus asyncDelegate = (RemoteServices.RemoteAsyncDelegate_UpdateDiplomacyStatus) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        UpdateDiplomacyStatus_ReturnType returnData = new UpdateDiplomacyStatus_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SendCommands_UserCallBack(RemoteServices.SendCommands_UserCallBack callback)
    {
      this.sendCommands_UserCallBack = callback;
    }

    public void SendCommands(int targetUserID, int command, int duration, string reason)
    {
      if (this.SendCommands_Callback == null)
        this.SendCommands_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendCommands);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SendCommands(this.service.SendCommands).BeginInvoke(this.UserID, this.SessionID, targetUserID, command, duration, reason, this.SendCommands_Callback, (object) null), typeof (SendCommands_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SendCommands(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SendCommands asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SendCommands) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SendCommands_ReturnType returnData = new SendCommands_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_InitialiseFreeCards_UserCallBack(
      RemoteServices.InitialiseFreeCards_UserCallBack callback)
    {
      this.initialiseFreeCards_UserCallBack = callback;
    }

    public void InitialiseFreeCards()
    {
      if (this.InitialiseFreeCards_Callback == null)
        this.InitialiseFreeCards_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_InitialiseFreeCards);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_InitialiseFreeCards(this.service.InitialiseFreeCards).BeginInvoke(this.UserID, this.SessionID, this.InitialiseFreeCards_Callback, (object) null), typeof (InitialiseFreeCards_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_InitialiseFreeCards(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_InitialiseFreeCards asyncDelegate = (RemoteServices.RemoteAsyncDelegate_InitialiseFreeCards) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        InitialiseFreeCards_ReturnType returnData = new InitialiseFreeCards_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_TestAchievements_UserCallBack(
      RemoteServices.TestAchievements_UserCallBack callback)
    {
      this.testAchievements_UserCallBack = callback;
    }

    public void TestAchievements(
      List<int> achievementsToTest,
      List<AchievementData> achievementData)
    {
      if (this.TestAchievements_Callback == null)
        this.TestAchievements_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_TestAchievements);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_TestAchievements(this.service.TestAchievements).BeginInvoke(this.UserID, this.SessionID, achievementsToTest, achievementData, this.TestAchievements_Callback, (object) null), typeof (TestAchievements_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_TestAchievements(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_TestAchievements asyncDelegate = (RemoteServices.RemoteAsyncDelegate_TestAchievements) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        TestAchievements_ReturnType returnData = new TestAchievements_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_AchievementProgress_UserCallBack(
      RemoteServices.AchievementProgress_UserCallBack callback)
    {
      this.achievementProgress_UserCallBack = callback;
    }

    public void AchievementProgress()
    {
      if (this.AchievementProgress_Callback == null)
        this.AchievementProgress_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_AchievementProgress);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_AchievementProgress(this.service.AchievementProgress).BeginInvoke(this.UserID, this.SessionID, this.AchievementProgress_Callback, (object) null), typeof (AchievementProgress_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_AchievementProgress(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_AchievementProgress asyncDelegate = (RemoteServices.RemoteAsyncDelegate_AchievementProgress) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        AchievementProgress_ReturnType returnData = new AchievementProgress_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetQuestData_UserCallBack(RemoteServices.GetQuestData_UserCallBack callback)
    {
      this.getQuestData_UserCallBack = callback;
    }

    public void GetQuestData(bool full)
    {
      if (this.GetQuestData_Callback == null)
        this.GetQuestData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetQuestData);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetQuestData(this.service.GetQuestData).BeginInvoke(this.UserID, this.SessionID, full, this.GetQuestData_Callback, (object) null), typeof (GetQuestData_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetQuestData(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetQuestData asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetQuestData) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetQuestData_ReturnType returnData = new GetQuestData_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_StartNewQuest_UserCallBack(RemoteServices.StartNewQuest_UserCallBack callback)
    {
      this.startNewQuest_UserCallBack = callback;
    }

    public void StartNewQuest(int questID)
    {
      if (this.StartNewQuest_Callback == null)
        this.StartNewQuest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_StartNewQuest);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_StartNewQuest(this.service.StartNewQuest).BeginInvoke(this.UserID, this.SessionID, questID, this.StartNewQuest_Callback, (object) null), typeof (StartNewQuest_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_StartNewQuest(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_StartNewQuest asyncDelegate = (RemoteServices.RemoteAsyncDelegate_StartNewQuest) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        StartNewQuest_ReturnType returnData = new StartNewQuest_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CompleteAbandonNewQuest_UserCallBack(
      RemoteServices.CompleteAbandonNewQuest_UserCallBack callback)
    {
      this.completeAbandonNewQuest_UserCallBack = callback;
    }

    public void CompleteNewQuest(int questID, bool glory, int villageID)
    {
      if (this.CompleteAbandonNewQuest_Callback == null)
        this.CompleteAbandonNewQuest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CompleteAbandonNewQuest);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CompleteAbandonNewQuest(this.service.CompleteAbandonNewQuest).BeginInvoke(this.UserID, this.SessionID, questID, false, glory, villageID, this.CompleteAbandonNewQuest_Callback, (object) null), typeof (CompleteAbandonNewQuest_ReturnType));
    }

    public void AbandonNewQuest(int questID)
    {
      if (this.CompleteAbandonNewQuest_Callback == null)
        this.CompleteAbandonNewQuest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CompleteAbandonNewQuest);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_CompleteAbandonNewQuest(this.service.CompleteAbandonNewQuest).BeginInvoke(this.UserID, this.SessionID, questID, true, false, -1, this.CompleteAbandonNewQuest_Callback, (object) null), typeof (CompleteAbandonNewQuest_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_CompleteAbandonNewQuest(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_CompleteAbandonNewQuest asyncDelegate = (RemoteServices.RemoteAsyncDelegate_CompleteAbandonNewQuest) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        CompleteAbandonNewQuest_ReturnType returnData = new CompleteAbandonNewQuest_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SpinTheWheel_UserCallBack(RemoteServices.SpinTheWheel_UserCallBack callback)
    {
      this.spinTheWheel_UserCallBack = callback;
    }

    public void SpinTheRoyalWheel(int villageID, int wheelType)
    {
      if (this.SpinTheWheel_Callback == null)
        this.SpinTheWheel_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SpinTheWheel);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SpinTheWheel(this.service.SpinTheWheel).BeginInvoke(this.UserID, this.SessionID, villageID, wheelType, this.SpinTheWheel_Callback, (object) null), typeof (SpinTheWheel_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SpinTheWheel(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SpinTheWheel asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SpinTheWheel) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SpinTheWheel_ReturnType returnData = new SpinTheWheel_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_SetVacationMode_UserCallBack(
      RemoteServices.SetVacationMode_UserCallBack callback)
    {
      this.setVacationMode_UserCallBack = callback;
    }

    public void SetVacationMode(int numDays)
    {
      if (this.SetVacationMode_Callback == null)
        this.SetVacationMode_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SetVacationMode);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_SetVacationMode(this.service.SetVacationMode).BeginInvoke(this.UserID, this.SessionID, numDays, this.SetVacationMode_Callback, (object) null), typeof (SetVacationMode_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_SetVacationMode(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_SetVacationMode asyncDelegate = (RemoteServices.RemoteAsyncDelegate_SetVacationMode) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        SetVacationMode_ReturnType returnData = new SetVacationMode_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_PremiumOverview_UserCallBack(
      RemoteServices.PremiumOverview_UserCallBack callback)
    {
      this.premiumOverview_UserCallBack = callback;
    }

    public void PremiumOverview()
    {
      if (this.PremiumOverview_Callback == null)
        this.PremiumOverview_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_PremiumOverview);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_PremiumOverview(this.service.PremiumOverview).BeginInvoke(this.UserID, this.SessionID, this.PremiumOverview_Callback, (object) null), typeof (PremiumOverview_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_PremiumOverview(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_PremiumOverview asyncDelegate = (RemoteServices.RemoteAsyncDelegate_PremiumOverview) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        PremiumOverview_ReturnType returnData = new PremiumOverview_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetLastAttacker_UserCallBack(
      RemoteServices.GetLastAttacker_UserCallBack callback)
    {
      this.getLastAttacker_UserCallBack = callback;
    }

    public void GetLastAttacker()
    {
      if (this.GetLastAttacker_Callback == null)
        this.GetLastAttacker_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetLastAttacker);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetLastAttacker(this.service.GetLastAttacker).BeginInvoke(this.UserID, this.SessionID, this.GetLastAttacker_Callback, (object) null), typeof (GetLastAttacker_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetLastAttacker(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetLastAttacker asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetLastAttacker) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetLastAttacker_ReturnType returnData = new GetLastAttacker_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_PreValidateCardToBePlayed_UserCallBack(
      RemoteServices.PreValidateCardToBePlayed_UserCallBack callback)
    {
      this.preValidateCardToBePlayed_UserCallBack = callback;
    }

    public void PreValidateCardToBePlayed(int card, int data)
    {
      if (this.PreValidateCardToBePlayed_Callback == null)
        this.PreValidateCardToBePlayed_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_PreValidateCardToBePlayed);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_PreValidateCardToBePlayed(this.service.PreValidateCardToBePlayed).BeginInvoke(this.UserID, this.SessionID, card, data, this.PreValidateCardToBePlayed_Callback, (object) null), typeof (PreValidateCardToBePlayed_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_PreValidateCardToBePlayed(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_PreValidateCardToBePlayed asyncDelegate = (RemoteServices.RemoteAsyncDelegate_PreValidateCardToBePlayed) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        PreValidateCardToBePlayed_ReturnType returnData = new PreValidateCardToBePlayed_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetInvasionInfo_UserCallBack(
      RemoteServices.GetInvasionInfo_UserCallBack callback)
    {
      this.getInvasionInfo_UserCallBack = callback;
    }

    public void GetInvasionInfo()
    {
      if (this.GetInvasionInfo_Callback == null)
        this.GetInvasionInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetInvasionInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetInvasionInfo(this.service.GetInvasionInfo).BeginInvoke(this.UserID, this.SessionID, this.GetInvasionInfo_Callback, (object) null), typeof (GetInvasionInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetInvasionInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetInvasionInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetInvasionInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetInvasionInfo_ReturnType returnData = new GetInvasionInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_WorldInfo_UserCallBack(RemoteServices.WorldInfo_UserCallBack callback)
    {
      this.worldInfo_UserCallBack = callback;
    }

    public void WorldInfo()
    {
      if (this.WorldInfo_Callback == null)
        this.WorldInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_WorldInfo);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_WorldInfo(this.service.WorldInfo).BeginInvoke(this.WorldInfo_Callback, (object) null), typeof (WorldInfo_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_WorldInfo(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_WorldInfo asyncDelegate = (RemoteServices.RemoteAsyncDelegate_WorldInfo) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        WorldInfo_ReturnType returnData = new WorldInfo_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_EndWorld_UserCallBack(RemoteServices.EndWorld_UserCallBack callback)
    {
      this.endWorld_UserCallBack = callback;
    }

    public void EndWorld()
    {
      if (this.EndWorld_Callback == null)
        this.EndWorld_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_EndWorld);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_EndWorld(this.service.EndWorld).BeginInvoke(this.UserID, this.SessionID, this.EndWorld_Callback, (object) null), typeof (EndWorld_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_EndWorld(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_EndWorld asyncDelegate = (RemoteServices.RemoteAsyncDelegate_EndWorld) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        EndWorld_ReturnType returnData = new EndWorld_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_EndOfTheWorldStats_UserCallBack(
      RemoteServices.EndOfTheWorldStats_UserCallBack callback)
    {
      this.endOfTheWorldStats_UserCallBack = callback;
    }

    public void EndOfTheWorldStats()
    {
      if (this.EndOfTheWorldStats_Callback == null)
        this.EndOfTheWorldStats_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_EndOfTheWorldStats);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_EndOfTheWorldStats(this.service.EndOfTheWorldStats).BeginInvoke(this.UserID, this.SessionID, this.EndOfTheWorldStats_Callback, (object) null), typeof (EndOfTheWorldStats_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_EndOfTheWorldStats(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_EndOfTheWorldStats asyncDelegate = (RemoteServices.RemoteAsyncDelegate_EndOfTheWorldStats) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        EndOfTheWorldStats_ReturnType returnData = new EndOfTheWorldStats_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetKillStreakData_UserCallBack(
      RemoteServices.GetKillStreakData_UserCallBack callback)
    {
      this.getKillStreakData_UserCallBack = callback;
    }

    public void GetKillStreakData()
    {
      if (this.GetKillStreakData_Callback == null)
        this.GetKillStreakData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetKillStreakData);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetKillStreakData(this.service.GetKillStreakData).BeginInvoke(this.UserID, this.SessionID, this.GetKillStreakData_Callback, (object) null), typeof (GetKillStreakData_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetKillStreakData(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetKillStreakData asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetKillStreakData) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetKillStreakData_ReturnType returnData = new GetKillStreakData_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetUserContestData_UserCallBack(
      RemoteServices.GetUserContestData_UserCallBack callback)
    {
      this.getUserContestData_UserCallBack = callback;
    }

    public void GetUserContestData(int contestID)
    {
      if (this.GetUserContestData_Callback == null)
        this.GetUserContestData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetUserContestData);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetUserContestData(this.service.GetUserContestData).BeginInvoke(this.UserID, this.SessionID, contestID, this.GetUserContestData_Callback, (object) null), typeof (GetUserContestData_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetUserContestData(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetUserContestData asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetUserContestData) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetUserContestData_ReturnType returnData = new GetUserContestData_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetContestDataRange_UserCallBack(
      RemoteServices.GetContestDataRange_UserCallBack callback)
    {
      this.getContestDataRange_UserCallBack = callback;
    }

    public void GetContestDataRange(int contestID, int topIndex, int numEntries, int rankBand)
    {
      if (this.GetContestDataRange_Callback == null)
        this.GetContestDataRange_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetContestDataRange);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetContestDataRange(this.service.GetContestDataRange).BeginInvoke(this.UserID, this.SessionID, contestID, topIndex, numEntries, rankBand, this.GetContestDataRange_Callback, (object) null), typeof (GetContestDataRange_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetContestDataRange(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetContestDataRange asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetContestDataRange) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetContestDataRange_ReturnType returnData = new GetContestDataRange_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_GetContestHistoryIDs_UserCallBack(
      RemoteServices.GetContestHistoryIDs_UserCallBack callback)
    {
      this.getContestHistoryIDs_UserCallBack = callback;
    }

    public void GetContestHistoryIDs()
    {
      if (this.GetContestHistoryIDs_Callback == null)
        this.GetContestHistoryIDs_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetContestHistoryIDs);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_GetContestHistoryIDs(this.service.GetContestHistoryIDs).BeginInvoke(this.UserID, this.SessionID, this.GetContestHistoryIDs_Callback, (object) null), typeof (GetContestHistoryIDs_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_GetContestHistoryIDs(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_GetContestHistoryIDs asyncDelegate = (RemoteServices.RemoteAsyncDelegate_GetContestHistoryIDs) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        GetContestHistoryIDs_ReturnType returnData = new GetContestHistoryIDs_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_Chat_Login_UserCallBack(RemoteServices.Chat_Login_UserCallBack callback)
    {
      this.chat_Login_UserCallBack = callback;
    }

    public void Chat_Login()
    {
      if (!this.ChatActive)
        return;
      if (this.Chat_Login_Callback == null)
        this.Chat_Login_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_Login);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_Chat_Login(this.service.Chat_Login).BeginInvoke(RemoteServices.Instance.UserID, RemoteServices.Instance.SessionID, this.Chat_Login_Callback, (object) null), typeof (Chat_Login_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_Chat_Login(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_Chat_Login asyncDelegate = (RemoteServices.RemoteAsyncDelegate_Chat_Login) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        Chat_Login_ReturnType returnData = new Chat_Login_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_Chat_Logout_UserCallBack(RemoteServices.Chat_Logout_UserCallBack callback)
    {
      this.chat_Logout_UserCallBack = callback;
    }

    public void Chat_Logout()
    {
      if (!this.ChatActive || this.SessionID == 0)
        return;
      if (this.Chat_Logout_Callback == null)
        this.Chat_Logout_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_Logout);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_Chat_Logout(this.service.Chat_Logout).BeginInvoke(RemoteServices.Instance.UserID, RemoteServices.Instance.SessionID, this.Chat_Logout_Callback, (object) null), typeof (Chat_Logout_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_Chat_Logout(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_Chat_Logout asyncDelegate = (RemoteServices.RemoteAsyncDelegate_Chat_Logout) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        Chat_Logout_ReturnType returnData = new Chat_Logout_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_Chat_SetReceivingState_UserCallBack(
      RemoteServices.Chat_SetReceivingState_UserCallBack callback)
    {
      this.chat_SetReceivingState_UserCallBack = callback;
    }

    public void Chat_StartReceiving()
    {
      if (!this.ChatActive)
        return;
      if (this.Chat_SetReceivingState_Callback == null)
        this.Chat_SetReceivingState_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_SetReceivingState);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_Chat_SetReceivingState(this.service.Chat_SetReceivingState).BeginInvoke(RemoteServices.Instance.UserID, RemoteServices.Instance.SessionID, true, this.Chat_SetReceivingState_Callback, (object) null), typeof (Chat_SetReceivingState_ReturnType));
    }

    public void Chat_StopReceiving()
    {
      if (!this.ChatActive)
        return;
      if (this.Chat_SetReceivingState_Callback == null)
        this.Chat_SetReceivingState_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_SetReceivingState);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_Chat_SetReceivingState(this.service.Chat_SetReceivingState).BeginInvoke(RemoteServices.Instance.UserID, RemoteServices.Instance.SessionID, false, this.Chat_SetReceivingState_Callback, (object) null), typeof (Chat_SetReceivingState_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_Chat_SetReceivingState(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_Chat_SetReceivingState asyncDelegate = (RemoteServices.RemoteAsyncDelegate_Chat_SetReceivingState) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        Chat_SetReceivingState_ReturnType returnData = new Chat_SetReceivingState_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_Chat_SendText_UserCallBack(RemoteServices.Chat_SendText_UserCallBack callback)
    {
      this.chat_SendText_UserCallBack = callback;
    }

    public void Chat_SendText(string text, int roomType, int roomID)
    {
      if (!this.ChatActive)
        return;
      if (this.Chat_SendText_Callback == null)
        this.Chat_SendText_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_SendText);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_Chat_SendText(this.service.Chat_SendText).BeginInvoke(RemoteServices.Instance.UserID, RemoteServices.Instance.SessionID, roomType, roomID, text, RemoteServices.Instance.UserOptions.profanityFilter, this.Chat_SendText_Callback, (object) null), typeof (Chat_SendText_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_Chat_SendText(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_Chat_SendText asyncDelegate = (RemoteServices.RemoteAsyncDelegate_Chat_SendText) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        Chat_SendText_ReturnType returnData = new Chat_SendText_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_Chat_ReceiveText_UserCallBack(
      RemoteServices.Chat_ReceiveText_UserCallBack callback)
    {
      this.chat_ReceiveText_UserCallBack = callback;
    }

    public void Chat_GetText(List<Chat_RoomID> roomsToRegister, bool changeRooms)
    {
      if (!this.ChatActive)
        return;
      if (this.Chat_ReceiveText_Callback == null)
        this.Chat_ReceiveText_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_ReceiveText);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_Chat_ReceiveText(this.service.Chat_ReceiveText).BeginInvoke(RemoteServices.Instance.UserID, RemoteServices.Instance.SessionID, roomsToRegister, changeRooms, RemoteServices.Instance.UserOptions.profanityFilter, this.Chat_ReceiveText_Callback, (object) null), typeof (Chat_ReceiveText_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_Chat_ReceiveText(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_Chat_ReceiveText asyncDelegate = (RemoteServices.RemoteAsyncDelegate_Chat_ReceiveText) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        Chat_ReceiveText_ReturnType returnData = new Chat_ReceiveText_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_Chat_SendParishText_UserCallBack(
      RemoteServices.Chat_SendParishText_UserCallBack callback)
    {
      this.chat_SendParishText_UserCallBack = callback;
    }

    public void Chat_SendParishText(string text, int parishID, int subForumID, DateTime lastTime)
    {
      if (!this.ChatActive)
        return;
      if (this.Chat_SendParishText_Callback == null)
        this.Chat_SendParishText_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_SendParishText);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_Chat_SendParishText(this.service.Chat_SendParishText).BeginInvoke(RemoteServices.Instance.UserID, RemoteServices.Instance.SessionID, parishID, subForumID, text, lastTime, RemoteServices.Instance.UserOptions.profanityFilter, this.Chat_SendParishText_Callback, (object) null), typeof (Chat_SendParishText_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_Chat_SendParishText(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_Chat_SendParishText asyncDelegate = (RemoteServices.RemoteAsyncDelegate_Chat_SendParishText) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        Chat_SendParishText_ReturnType returnData = new Chat_SendParishText_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_Chat_ReceiveParishText_UserCallBack(
      RemoteServices.Chat_ReceiveParishText_UserCallBack callback)
    {
      this.chat_ReceiveParishText_UserCallBack = callback;
    }

    public void Chat_ReceiveParishText(int parishID, DateTime lastTime)
    {
      if (!this.ChatActive)
        return;
      if (this.Chat_ReceiveParishText_Callback == null)
        this.Chat_ReceiveParishText_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_ReceiveParishText);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_Chat_ReceiveParishText(this.service.Chat_ReceiveParishText).BeginInvoke(RemoteServices.Instance.UserID, RemoteServices.Instance.SessionID, parishID, lastTime, RemoteServices.Instance.UserOptions.profanityFilter, this.Chat_ReceiveParishText_Callback, (object) null), typeof (Chat_ReceiveParishText_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_Chat_ReceiveParishText(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_Chat_ReceiveParishText asyncDelegate = (RemoteServices.RemoteAsyncDelegate_Chat_ReceiveParishText) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        Chat_ReceiveParishText_ReturnType returnData = new Chat_ReceiveParishText_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_Chat_BackFillParishText_UserCallBack(
      RemoteServices.Chat_BackFillParishText_UserCallBack callback)
    {
      this.chat_BackFillParishText_UserCallBack = callback;
    }

    public void Chat_BackFillParishText(
      int parishID,
      int pageID,
      long oldestKnownID,
      DateTime lastTime)
    {
      if (!this.ChatActive)
        return;
      if (this.Chat_BackFillParishText_Callback == null)
        this.Chat_BackFillParishText_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_BackFillParishText);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_Chat_BackFillParishText(this.service.Chat_BackFillParishText).BeginInvoke(RemoteServices.Instance.UserID, RemoteServices.Instance.SessionID, parishID, pageID, oldestKnownID, lastTime, RemoteServices.Instance.UserOptions.profanityFilter, this.Chat_BackFillParishText_Callback, (object) null), typeof (Chat_BackFillParishText_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_Chat_BackFillParishText(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_Chat_BackFillParishText asyncDelegate = (RemoteServices.RemoteAsyncDelegate_Chat_BackFillParishText) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        Chat_BackFillParishText_ReturnType returnData = new Chat_BackFillParishText_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_Chat_MarkParishTextRead_UserCallBack(
      RemoteServices.Chat_MarkParishTextRead_UserCallBack callback)
    {
      this.chat_MarkParishTextRead_UserCallBack = callback;
    }

    public void Chat_MarkParishTextRead(int parishID, int pageID, long readID)
    {
      if (!this.ChatActive)
        return;
      if (this.Chat_MarkParishTextRead_Callback == null)
        this.Chat_MarkParishTextRead_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_MarkParishTextRead);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_Chat_MarkParishTextRead(this.service.Chat_MarkParishTextRead).BeginInvoke(RemoteServices.Instance.UserID, RemoteServices.Instance.SessionID, parishID, pageID, readID, this.Chat_MarkParishTextRead_Callback, (object) null), typeof (Chat_MarkParishTextRead_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_Chat_MarkParishTextRead(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_Chat_MarkParishTextRead asyncDelegate = (RemoteServices.RemoteAsyncDelegate_Chat_MarkParishTextRead) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        Chat_MarkParishTextRead_ReturnType returnData = new Chat_MarkParishTextRead_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_Chat_Admin_Command_UserCallBack(
      RemoteServices.Chat_Admin_Command_UserCallBack callback)
    {
      this.chat_Admin_Command_UserCallBack = callback;
    }

    public void Chat_Admin_Command(int command, int targetUserID)
    {
      if (this.Chat_Admin_Command_Callback == null)
        this.Chat_Admin_Command_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_Admin_Command);
      this.registerRPCcall(new RemoteServices.RemoteAsyncDelegate_Chat_Admin_Command(this.service.Chat_Admin_Command).BeginInvoke(RemoteServices.Instance.UserID, RemoteServices.Instance.SessionID, command, targetUserID, this.Chat_Admin_Command_Callback, (object) null), typeof (Chat_Admin_Command_ReturnType));
    }

    [OneWay]
    public void OurRemoteAsyncCallBack_Chat_Admin_Command(IAsyncResult ar)
    {
      RemoteServices.RemoteAsyncDelegate_Chat_Admin_Command asyncDelegate = (RemoteServices.RemoteAsyncDelegate_Chat_Admin_Command) ((AsyncResult) ar).AsyncDelegate;
      try
      {
        this.storeRPCresult(ar, (Common_ReturnData) asyncDelegate.EndInvoke(ar));
      }
      catch (Exception ex)
      {
        Chat_Admin_Command_ReturnType returnData = new Chat_Admin_Command_ReturnType();
        this.manageRemoteExpection(ar, (Common_ReturnData) returnData, ex);
      }
    }

    public void set_CommonData_UserCallBack(RemoteServices.CommonData_UserCallBack callback)
    {
      this.commonData_UserCallBack = callback;
    }

    public void processData()
    {
      if (this.connectionErrored)
      {
        bool flag = true;
        foreach (RemoteServices.CallBackEntryClass result in this.resultList)
        {
          if (result.state == 1 && result.classType != typeof (FullTick_ReturnType) && result.classType != typeof (GetArmyData_ReturnType))
          {
            flag = false;
            break;
          }
        }
        if (flag)
          GameEngine.Instance.sessionExpired(11);
        else
          GameEngine.Instance.sessionExpired(1);
        this.connectionErrored = false;
      }
      this.inResultsProcessing = true;
      bool flag1 = false;
      foreach (RemoteServices.CallBackEntryClass result in this.resultList)
      {
        if (result.state == 1)
        {
          double timeTaken = DXTimer.GetCurrentMilliseconds() - result.timer;
          if (timeTaken > 30000.0)
          {
            if (result.classType == typeof (CreateNewUser_ReturnType))
            {
              result.state = 0;
              result.data = (Common_ReturnData) null;
              if (this.createNewUser_UserCallBack != null)
              {
                CreateNewUser_ReturnType returnData = new CreateNewUser_ReturnType();
                returnData.SetAsFailed();
                returnData.m_errorCode = CommonTypes.ErrorCodes.ErrorCode.CONNECTION_TIMED_OUT;
                this.createNewUser_UserCallBack(returnData);
              }
            }
            else if (result.classType == typeof (LoginUser_ReturnType))
            {
              result.state = 0;
              result.data = (Common_ReturnData) null;
              if (this.loginUser_UserCallBack != null)
              {
                LoginUser_ReturnType returnData = new LoginUser_ReturnType();
                returnData.SetAsFailed();
                returnData.m_errorCode = CommonTypes.ErrorCodes.ErrorCode.CONNECTION_TIMED_OUT;
                this.loginUser_UserCallBack(returnData);
              }
            }
            else if (result.classType == typeof (LoginUserGuid_ReturnType))
            {
              result.state = 0;
              result.data = (Common_ReturnData) null;
              if (this.loginUserGuid_UserCallBack != null)
              {
                LoginUserGuid_ReturnType returnData = new LoginUserGuid_ReturnType();
                returnData.SetAsFailed();
                returnData.m_errorCode = CommonTypes.ErrorCodes.ErrorCode.CONNECTION_TIMED_OUT;
                this.loginUserGuid_UserCallBack(returnData);
              }
            }
          }
          if (this.packetTimeOut(timeTaken, result) && result.classType != typeof (RetrieveStats_ReturnType) && result.classType != typeof (RetrieveStats2_ReturnType) && !InterfaceMgr.Instance.isConnectionErrorWindow())
          {
            ++this.RTTTimeOuts;
            result.state = 0;
            result.data = (Common_ReturnData) null;
            this.addPacket(result.classType, -1);
            ++this.consecutiveTimeOuts;
            if (this.consecutiveTimeOuts >= 10)
            {
              if (this.sessionID != 0)
              {
                this.sessionID = 0;
                GameEngine.Instance.sessionExpired(2);
                flag1 = true;
                break;
              }
              break;
            }
          }
        }
        if (result.state == 2)
        {
          result.state = 0;
          this.consecutiveTimeOuts = 0;
          if (!result.data.Success && result.data.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.CONNECTION_SESSION_ENDED)
          {
            if (this.sessionID != 0)
            {
              this.sessionID = 0;
              GameEngine.Instance.sessionExpired(0);
              flag1 = true;
            }
            result.data = (Common_ReturnData) null;
            break;
          }
          if (result.classType != typeof (CreateNewUser_ReturnType) && result.classType != typeof (LoginUser_ReturnType) && result.classType != typeof (LogOut_ReturnType) && result.classType != typeof (LoginUserGuid_ReturnType) && !result.data.Success && result.data.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.CONNECTION_NO_SERVER)
          {
            result.data = (Common_ReturnData) null;
            break;
          }
          if (InterfaceMgr.Instance.isConnectionErrorWindow())
            InterfaceMgr.Instance.closeConnectionErrorWindow();
          if (!result.data.Success && result.data.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.COMMUNICATION_BAN)
          {
            if (result.classType == typeof (SendMail_ReturnType) || result.classType == typeof (SendSpecialMail_ReturnType))
            {
              int num1 = (int) MyMessageBox.Show(SK.Text("ChatScreenManager_Ban_Message_mail", "Your mail posting privileges have been suspended for violations of the game rules or code of conduct.") + Environment.NewLine + URLs.IPSharingPage, SK.Text("GENERIC_Mail Banned", "Mail Posting Privileges Ban"));
            }
            else if (result.classType == typeof (NewForumThread_ReturnType) || result.classType == typeof (PostToForumThread_ReturnType))
            {
              int num2 = (int) MyMessageBox.Show(SK.Text("ChatScreenManager_Ban_Message_forum", "Your forum posting privileges have been suspended for violations of the game rules or code of conduct.") + Environment.NewLine + URLs.IPSharingPage, SK.Text("GENERIC_Forum Banned", "Forum Posting Privileges Ban"));
            }
            else
            {
              int num3 = (int) MyMessageBox.Show(SK.Text("ChatScreenManager_Ban_Message", "You have been banned from using this function, contact support if you wish to discuss this.") + Environment.NewLine + "https://support.strongholdkingdoms.com", SK.Text("GENERIC_Banned", "Banned"));
            }
            result.data = (Common_ReturnData) null;
          }
          else
          {
            if (this.commonData_UserCallBack != null && result.classType != typeof (WorldInfo_ReturnType))
            {
              Common_ReturnData data = result.data;
              if (data.Success)
                this.commonData_UserCallBack(data);
            }
            Common_ReturnData data1 = result.data;
            if (!data1.Success)
            {
              if (data1.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.ACTION_NOT_IMPLEMENTED)
              {
                int num = (int) MyMessageBox.Show("This Function is not Implement Yet.", "Error");
              }
              else
              {
                if (data1.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.LOCK_OUT)
                {
                  GameEngine.Instance.World.WorldEnded = true;
                  continue;
                }
                result.state = 0;
                switch (data1.m_errorCode)
                {
                  case CommonTypes.ErrorCodes.ErrorCode.SHARED_TRADING:
                  case CommonTypes.ErrorCodes.ErrorCode.SHARED_VASSALS:
                  case CommonTypes.ErrorCodes.ErrorCode.SHARED_ATTACK_TARGET:
                  case CommonTypes.ErrorCodes.ErrorCode.SHARED_REINFORCEMENTS:
                  case CommonTypes.ErrorCodes.ErrorCode.SHARED_MONKS:
                  case CommonTypes.ErrorCodes.ErrorCode.SHARED_VOTING:
                    SharedIPErrorPopup.showSharedIPPopup(CommonTypes.ErrorCodes.getErrorString(data1.m_errorCode));
                    using (List<RemoteServices.CallBackEntryClass>.Enumerator enumerator = this.resultList.GetEnumerator())
                    {
                      while (enumerator.MoveNext())
                        enumerator.Current.timer = DXTimer.GetCurrentMilliseconds();
                      break;
                    }
                }
              }
            }
            if (result.classType == typeof (CreateNewUser_ReturnType))
            {
              CreateNewUser_ReturnType data2 = (CreateNewUser_ReturnType) result.data;
              if (this.createNewUser_UserCallBack != null)
                this.createNewUser_UserCallBack(data2);
            }
            else if (result.classType == typeof (LoginUser_ReturnType))
            {
              LoginUser_ReturnType data3 = (LoginUser_ReturnType) result.data;
              if (this.loginUser_UserCallBack != null)
                this.loginUser_UserCallBack(data3);
            }
            else if (result.classType == typeof (LoginUserGuid_ReturnType))
            {
              LoginUserGuid_ReturnType data4 = (LoginUserGuid_ReturnType) result.data;
              if (this.loginUserGuid_UserCallBack != null)
                this.loginUserGuid_UserCallBack(data4);
            }
            else if (result.classType == typeof (ResendVerificationEmail_ReturnType))
            {
              ResendVerificationEmail_ReturnType data5 = (ResendVerificationEmail_ReturnType) result.data;
              if (this.resendVerificationEmail_UserCallBack != null)
                this.resendVerificationEmail_UserCallBack(data5);
            }
            else if (result.classType == typeof (RetrieveVillageUserInfo_ReturnType))
            {
              RetrieveVillageUserInfo_ReturnType data6 = (RetrieveVillageUserInfo_ReturnType) result.data;
              if (this.retrieveVillageUserInfo_UserCallBack != null)
                this.retrieveVillageUserInfo_UserCallBack(data6);
            }
            else if (result.classType == typeof (SpecialVillageInfo_ReturnType))
            {
              SpecialVillageInfo_ReturnType data7 = (SpecialVillageInfo_ReturnType) result.data;
              if (this.specialVillageInfo_UserCallBack != null)
                this.specialVillageInfo_UserCallBack(data7);
            }
            else if (result.classType == typeof (GetAllVillageOwnerFactions_ReturnType))
            {
              GetAllVillageOwnerFactions_ReturnType data8 = (GetAllVillageOwnerFactions_ReturnType) result.data;
              if (this.getAllVillageOwnerFactions_UserCallBack != null)
                this.getAllVillageOwnerFactions_UserCallBack(data8);
            }
            else if (result.classType == typeof (GetVillageNames_ReturnType))
            {
              GetVillageNames_ReturnType data9 = (GetVillageNames_ReturnType) result.data;
              if (this.getVillageNames_UserCallBack != null)
                this.getVillageNames_UserCallBack(data9);
            }
            else if (result.classType == typeof (GetVillageFactionChanges_ReturnType))
            {
              GetVillageFactionChanges_ReturnType data10 = (GetVillageFactionChanges_ReturnType) result.data;
              if (this.getVillageFactionChanges_UserCallBack != null)
                this.getVillageFactionChanges_UserCallBack(data10);
            }
            else if (result.classType == typeof (GetAreaFactionChanges_ReturnType))
            {
              GetAreaFactionChanges_ReturnType data11 = (GetAreaFactionChanges_ReturnType) result.data;
              if (this.getAreaFactionChanges_UserCallBack != null)
                this.getAreaFactionChanges_UserCallBack(data11);
            }
            else if (result.classType == typeof (GetUserVillages_ReturnType))
            {
              GetUserVillages_ReturnType data12 = (GetUserVillages_ReturnType) result.data;
              if (this.getUserVillages_UserCallBack != null)
                this.getUserVillages_UserCallBack(data12);
            }
            else if (result.classType == typeof (GetOtherUserVillageIDList_ReturnType))
            {
              GetOtherUserVillageIDList_ReturnType data13 = (GetOtherUserVillageIDList_ReturnType) result.data;
              if (this.getOtherUserVillageIDList_UserCallBack != null)
                this.getOtherUserVillageIDList_UserCallBack(data13);
            }
            else if (result.classType == typeof (BuyVillage_ReturnType))
            {
              BuyVillage_ReturnType data14 = (BuyVillage_ReturnType) result.data;
              if (this.buyVillage_UserCallBack != null)
                this.buyVillage_UserCallBack(data14);
            }
            else if (result.classType == typeof (ConvertVillage_ReturnType))
            {
              ConvertVillage_ReturnType data15 = (ConvertVillage_ReturnType) result.data;
              if (this.convertVillage_UserCallBack != null)
                this.convertVillage_UserCallBack(data15);
            }
            else if (result.classType == typeof (FullTick_ReturnType))
            {
              FullTick_ReturnType data16 = (FullTick_ReturnType) result.data;
              if (this.fullTick_UserCallBack != null)
                this.fullTick_UserCallBack(data16);
            }
            else if (result.classType == typeof (LeaderBoard_ReturnType))
            {
              LeaderBoard_ReturnType data17 = (LeaderBoard_ReturnType) result.data;
              if (this.leaderBoard_UserCallBack != null)
                this.leaderBoard_UserCallBack(data17);
            }
            else if (result.classType == typeof (LeaderBoardSearch_ReturnType))
            {
              LeaderBoardSearch_ReturnType data18 = (LeaderBoardSearch_ReturnType) result.data;
              if (this.leaderBoardSearch_UserCallBack != null)
                this.leaderBoardSearch_UserCallBack(data18);
            }
            else if (result.classType == typeof (LogOut_ReturnType))
            {
              LogOut_ReturnType data19 = (LogOut_ReturnType) result.data;
              if (this.logOut_UserCallBack != null)
                this.logOut_UserCallBack(data19);
            }
            else if (result.classType == typeof (UserInfo_ReturnType))
            {
              UserInfo_ReturnType data20 = (UserInfo_ReturnType) result.data;
              if (this.userInfo_UserCallBack != null)
                this.userInfo_UserCallBack(data20);
            }
            else if (result.classType == typeof (GetArmyData_ReturnType))
            {
              GetArmyData_ReturnType data21 = (GetArmyData_ReturnType) result.data;
              if (this.getArmyData_UserCallBack != null)
                this.getArmyData_UserCallBack(data21);
            }
            else if (result.classType == typeof (ArmyAttack_ReturnType))
            {
              ArmyAttack_ReturnType data22 = (ArmyAttack_ReturnType) result.data;
              if (this.armyAttack_UserCallBack != null)
                this.armyAttack_UserCallBack(data22);
            }
            else if (result.classType == typeof (RetrieveAttackResult_ReturnType))
            {
              RetrieveAttackResult_ReturnType data23 = (RetrieveAttackResult_ReturnType) result.data;
              if (this.retrieveAttackResult_UserCallBack != null)
                this.retrieveAttackResult_UserCallBack(data23);
            }
            else if (result.classType == typeof (SetAdminMessage_ReturnType))
            {
              SetAdminMessage_ReturnType data24 = (SetAdminMessage_ReturnType) result.data;
              if (this.setAdminMessage_UserCallBack != null)
                this.setAdminMessage_UserCallBack(data24);
            }
            else if (result.classType == typeof (CompleteVillageCastle_ReturnType))
            {
              CompleteVillageCastle_ReturnType data25 = (CompleteVillageCastle_ReturnType) result.data;
              if (this.completeVillageCastle_UserCallBack != null)
                this.completeVillageCastle_UserCallBack(data25);
            }
            else if (result.classType == typeof (RetrieveStats_ReturnType))
            {
              RetrieveStats_ReturnType data26 = (RetrieveStats_ReturnType) result.data;
              if (this.retrieveStats_UserCallBack != null)
                this.retrieveStats_UserCallBack(data26);
            }
            else if (result.classType == typeof (RetrieveStats2_ReturnType))
            {
              RetrieveStats2_ReturnType data27 = (RetrieveStats2_ReturnType) result.data;
              if (this.retrieveStats2_UserCallBack != null)
                this.retrieveStats2_UserCallBack(data27);
            }
            else if (result.classType == typeof (GetAdminStats_ReturnType))
            {
              GetAdminStats_ReturnType data28 = (GetAdminStats_ReturnType) result.data;
              if (this.getAdminStats_UserCallBack != null)
                this.getAdminStats_UserCallBack(data28);
            }
            else if (result.classType == typeof (GetReportsList_ReturnType))
            {
              GetReportsList_ReturnType data29 = (GetReportsList_ReturnType) result.data;
              if (this.getReportsList_UserCallBack != null)
                this.getReportsList_UserCallBack(data29);
            }
            else if (result.classType == typeof (GetReport_ReturnType))
            {
              GetReport_ReturnType data30 = (GetReport_ReturnType) result.data;
              if (this.getReport_UserCallBack != null)
                this.getReport_UserCallBack(data30);
            }
            else if (result.classType == typeof (ForwardReport_ReturnType))
            {
              ForwardReport_ReturnType data31 = (ForwardReport_ReturnType) result.data;
              if (this.forwardReport_UserCallBack != null)
                this.forwardReport_UserCallBack(data31);
            }
            else if (result.classType == typeof (ViewBattle_ReturnType))
            {
              ViewBattle_ReturnType data32 = (ViewBattle_ReturnType) result.data;
              if (this.viewBattle_UserCallBack != null)
                this.viewBattle_UserCallBack(data32);
            }
            else if (result.classType == typeof (ViewCastle_ReturnType))
            {
              ViewCastle_ReturnType data33 = (ViewCastle_ReturnType) result.data;
              if (this.viewCastle_UserCallBack != null)
                this.viewCastle_UserCallBack(data33);
            }
            else if (result.classType == typeof (DeleteReports_ReturnType))
            {
              DeleteReports_ReturnType data34 = (DeleteReports_ReturnType) result.data;
              if (this.deleteReports_UserCallBack != null)
                this.deleteReports_UserCallBack(data34);
            }
            else if (result.classType == typeof (UpdateReportFilters_ReturnType))
            {
              UpdateReportFilters_ReturnType data35 = (UpdateReportFilters_ReturnType) result.data;
              if (this.updateReportFilters_UserCallBack != null)
                this.updateReportFilters_UserCallBack(data35);
            }
            else if (result.classType == typeof (UpdateUserOptions_ReturnType))
            {
              UpdateUserOptions_ReturnType data36 = (UpdateUserOptions_ReturnType) result.data;
              if (this.updateUserOptions_UserCallBack != null)
                this.updateUserOptions_UserCallBack(data36);
            }
            else if (result.classType == typeof (ManageReportFolders_ReturnType))
            {
              ManageReportFolders_ReturnType data37 = (ManageReportFolders_ReturnType) result.data;
              if (this.manageReportFolders_UserCallBack != null)
                this.manageReportFolders_UserCallBack(data37);
            }
            else if (result.classType == typeof (GetHistoricalData_ReturnType))
            {
              GetHistoricalData_ReturnType data38 = (GetHistoricalData_ReturnType) result.data;
              if (this.getHistoricalData_UserCallBack != null)
                this.getHistoricalData_UserCallBack(data38);
            }
            else if (result.classType == typeof (GetMailThreadList_ReturnType))
            {
              GetMailThreadList_ReturnType data39 = (GetMailThreadList_ReturnType) result.data;
              if (this.getMailThreadList_UserCallBack != null)
                this.getMailThreadList_UserCallBack(data39);
            }
            else if (result.classType == typeof (GetMailThread_ReturnType))
            {
              GetMailThread_ReturnType data40 = (GetMailThread_ReturnType) result.data;
              if (this.getMailThread_UserCallBack != null)
                this.getMailThread_UserCallBack(data40);
            }
            else if (result.classType == typeof (GetMailFolders_ReturnType))
            {
              GetMailFolders_ReturnType data41 = (GetMailFolders_ReturnType) result.data;
              if (this.getMailFolders_UserCallBack != null)
                this.getMailFolders_UserCallBack(data41);
            }
            else if (result.classType == typeof (CreateMailFolder_ReturnType))
            {
              CreateMailFolder_ReturnType data42 = (CreateMailFolder_ReturnType) result.data;
              if (this.createMailFolder_UserCallBack != null)
                this.createMailFolder_UserCallBack(data42);
            }
            else if (result.classType == typeof (MoveToMailFolder_ReturnType))
            {
              MoveToMailFolder_ReturnType data43 = (MoveToMailFolder_ReturnType) result.data;
              if (this.moveToMailFolder_UserCallBack != null)
                this.moveToMailFolder_UserCallBack(data43);
            }
            else if (result.classType == typeof (RemoveMailFolder_ReturnType))
            {
              RemoveMailFolder_ReturnType data44 = (RemoveMailFolder_ReturnType) result.data;
              if (this.removeMailFolder_UserCallBack != null)
                this.removeMailFolder_UserCallBack(data44);
            }
            else if (result.classType == typeof (ReportMail_ReturnType))
            {
              ReportMail_ReturnType data45 = (ReportMail_ReturnType) result.data;
              if (this.reportMail_UserCallBack != null)
                this.reportMail_UserCallBack(data45);
            }
            else if (result.classType == typeof (FlagMailRead_ReturnType))
            {
              FlagMailRead_ReturnType data46 = (FlagMailRead_ReturnType) result.data;
              if (this.flagMailRead_UserCallBack != null)
                this.flagMailRead_UserCallBack(data46);
            }
            else if (result.classType == typeof (SendMail_ReturnType))
            {
              SendMail_ReturnType data47 = (SendMail_ReturnType) result.data;
              if (this.sendMail_UserCallBack != null)
                this.sendMail_UserCallBack(data47);
            }
            else if (result.classType == typeof (SendSpecialMail_ReturnType))
            {
              SendSpecialMail_ReturnType data48 = (SendSpecialMail_ReturnType) result.data;
              if (this.sendSpecialMail_UserCallBack != null)
                this.sendSpecialMail_UserCallBack(data48);
            }
            else if (result.classType == typeof (DeleteMailThread_ReturnType))
            {
              DeleteMailThread_ReturnType data49 = (DeleteMailThread_ReturnType) result.data;
              if (this.deleteMailThread_UserCallBack != null)
                this.deleteMailThread_UserCallBack(data49);
            }
            else if (result.classType == typeof (GetMailRecipientsHistory_ReturnType))
            {
              GetMailRecipientsHistory_ReturnType data50 = (GetMailRecipientsHistory_ReturnType) result.data;
              if (this.getMailRecipientsHistory_UserCallBack != null)
                this.getMailRecipientsHistory_UserCallBack(data50);
            }
            else if (result.classType == typeof (GetMailUserSearch_ReturnType))
            {
              GetMailUserSearch_ReturnType data51 = (GetMailUserSearch_ReturnType) result.data;
              if (this.getMailUserSearch_UserCallBack != null)
                this.getMailUserSearch_UserCallBack(data51);
            }
            else if (result.classType == typeof (AddUserToFavourites_ReturnType))
            {
              AddUserToFavourites_ReturnType data52 = (AddUserToFavourites_ReturnType) result.data;
              if (this.addUserToFavourites_UserCallBack != null)
                this.addUserToFavourites_UserCallBack(data52);
            }
            else if (result.classType == typeof (GetResourceLevel_ReturnType))
            {
              GetResourceLevel_ReturnType data53 = (GetResourceLevel_ReturnType) result.data;
              if (this.getResourceLevel_UserCallBack != null)
                this.getResourceLevel_UserCallBack(data53);
            }
            else if (result.classType == typeof (GetVillageBuildingsList_ReturnType))
            {
              GetVillageBuildingsList_ReturnType data54 = (GetVillageBuildingsList_ReturnType) result.data;
              if (this.getVillageBuildingsList_UserCallBack != null)
                this.getVillageBuildingsList_UserCallBack(data54);
            }
            else if (result.classType == typeof (PlaceVillageBuilding_ReturnType))
            {
              PlaceVillageBuilding_ReturnType data55 = (PlaceVillageBuilding_ReturnType) result.data;
              if (this.placeVillageBuilding_UserCallBack != null)
                this.placeVillageBuilding_UserCallBack(data55);
            }
            else if (result.classType == typeof (MoveVillageBuilding_ReturnType))
            {
              MoveVillageBuilding_ReturnType data56 = (MoveVillageBuilding_ReturnType) result.data;
              if (this.moveVillageBuilding_UserCallBack != null)
                this.moveVillageBuilding_UserCallBack(data56);
            }
            else if (result.classType == typeof (DeleteVillageBuilding_ReturnType))
            {
              DeleteVillageBuilding_ReturnType data57 = (DeleteVillageBuilding_ReturnType) result.data;
              if (this.deleteVillageBuilding_UserCallBack != null)
                this.deleteVillageBuilding_UserCallBack(data57);
            }
            else if (result.classType == typeof (CancelDeleteVillageBuilding_ReturnType))
            {
              CancelDeleteVillageBuilding_ReturnType data58 = (CancelDeleteVillageBuilding_ReturnType) result.data;
              if (this.cancelDeleteVillageBuilding_UserCallBack != null)
                this.cancelDeleteVillageBuilding_UserCallBack(data58);
            }
            else if (result.classType == typeof (VillageBuildingCompleteDataRetrieval_ReturnType))
            {
              VillageBuildingCompleteDataRetrieval_ReturnType data59 = (VillageBuildingCompleteDataRetrieval_ReturnType) result.data;
              if (this.villageBuildingCompleteDataRetrieval_UserCallBack != null)
                this.villageBuildingCompleteDataRetrieval_UserCallBack(data59);
            }
            else if (result.classType == typeof (VillageBuildingSetActive_ReturnType))
            {
              VillageBuildingSetActive_ReturnType data60 = (VillageBuildingSetActive_ReturnType) result.data;
              if (this.villageBuildingSetActive_UserCallBack != null)
                this.villageBuildingSetActive_UserCallBack(data60);
            }
            else if (result.classType == typeof (VillageBuildingChangeRates_ReturnType))
            {
              VillageBuildingChangeRates_ReturnType data61 = (VillageBuildingChangeRates_ReturnType) result.data;
              if (this.villageBuildingChangeRates_UserCallBack != null)
                this.villageBuildingChangeRates_UserCallBack(data61);
            }
            else if (result.classType == typeof (VillageRename_ReturnType))
            {
              VillageRename_ReturnType data62 = (VillageRename_ReturnType) result.data;
              if (this.villageRename_UserCallBack != null)
                this.villageRename_UserCallBack(data62);
            }
            else if (result.classType == typeof (VillageProduceWeapons_ReturnType))
            {
              VillageProduceWeapons_ReturnType data63 = (VillageProduceWeapons_ReturnType) result.data;
              if (this.villageProduceWeapons_UserCallBack != null)
                this.villageProduceWeapons_UserCallBack(data63);
            }
            else if (result.classType == typeof (VillageHoldBanquet_ReturnType))
            {
              VillageHoldBanquet_ReturnType data64 = (VillageHoldBanquet_ReturnType) result.data;
              if (this.villageHoldBanquet_UserCallBack != null)
                this.villageHoldBanquet_UserCallBack(data64);
            }
            else if (result.classType == typeof (GetCastle_ReturnType))
            {
              GetCastle_ReturnType data65 = (GetCastle_ReturnType) result.data;
              if (this.getCastle_UserCallBack != null)
                this.getCastle_UserCallBack(data65);
            }
            else if (result.classType == typeof (AddCastleElement_ReturnType))
            {
              AddCastleElement_ReturnType data66 = (AddCastleElement_ReturnType) result.data;
              if (this.addCastleElement_UserCallBack != null)
                this.addCastleElement_UserCallBack(data66);
            }
            else if (result.classType == typeof (DeleteCastleElement_ReturnType))
            {
              DeleteCastleElement_ReturnType data67 = (DeleteCastleElement_ReturnType) result.data;
              if (this.deleteCastleElement_UserCallBack != null)
                this.deleteCastleElement_UserCallBack(data67);
            }
            else if (result.classType == typeof (CheatAddTroops_ReturnType))
            {
              CheatAddTroops_ReturnType data68 = (CheatAddTroops_ReturnType) result.data;
              if (this.cheatAddTroops_UserCallBack != null)
                this.cheatAddTroops_UserCallBack(data68);
            }
            else if (result.classType == typeof (AutoRepairCastle_ReturnType))
            {
              AutoRepairCastle_ReturnType data69 = (AutoRepairCastle_ReturnType) result.data;
              if (this.autoRepairCastle_UserCallBack != null)
                this.autoRepairCastle_UserCallBack(data69);
            }
            else if (result.classType == typeof (MemorizeCastleTroops_ReturnType))
            {
              MemorizeCastleTroops_ReturnType data70 = (MemorizeCastleTroops_ReturnType) result.data;
              if (this.memorizeCastleTroops_UserCallBack != null)
                this.memorizeCastleTroops_UserCallBack(data70);
            }
            else if (result.classType == typeof (RestoreCastleTroops_ReturnType))
            {
              RestoreCastleTroops_ReturnType data71 = (RestoreCastleTroops_ReturnType) result.data;
              if (this.restoreCastleTroops_UserCallBack != null)
                this.restoreCastleTroops_UserCallBack(data71);
            }
            else if (result.classType == typeof (LaunchCastleAttack_ReturnType))
            {
              LaunchCastleAttack_ReturnType data72 = (LaunchCastleAttack_ReturnType) result.data;
              if (this.launchCastleAttack_UserCallBack != null)
                this.launchCastleAttack_UserCallBack(data72);
            }
            else if (result.classType == typeof (ChangeCastleElementAggressiveDefender_ReturnType))
            {
              ChangeCastleElementAggressiveDefender_ReturnType data73 = (ChangeCastleElementAggressiveDefender_ReturnType) result.data;
              if (this.changeCastleElementAggressiveDefender_UserCallBack != null)
                this.changeCastleElementAggressiveDefender_UserCallBack(data73);
            }
            else if (result.classType == typeof (SendMarketResources_ReturnType))
            {
              SendMarketResources_ReturnType data74 = (SendMarketResources_ReturnType) result.data;
              if (this.sendMarketResources_UserCallBack != null)
                this.sendMarketResources_UserCallBack(data74);
            }
            else if (result.classType == typeof (GetUserTraders_ReturnType))
            {
              GetUserTraders_ReturnType data75 = (GetUserTraders_ReturnType) result.data;
              if (this.getUserTraders_UserCallBack != null)
                this.getUserTraders_UserCallBack(data75);
            }
            else if (result.classType == typeof (GetActiveTraders_ReturnType))
            {
              GetActiveTraders_ReturnType data76 = (GetActiveTraders_ReturnType) result.data;
              if (this.getActiveTraders_UserCallBack != null)
                this.getActiveTraders_UserCallBack(data76);
            }
            else if (result.classType == typeof (GetStockExchangeData_ReturnType))
            {
              GetStockExchangeData_ReturnType data77 = (GetStockExchangeData_ReturnType) result.data;
              if (this.getStockExchangeData_UserCallBack != null)
                this.getStockExchangeData_UserCallBack(data77);
            }
            else if (result.classType == typeof (StockExchangeTrade_ReturnType))
            {
              StockExchangeTrade_ReturnType data78 = (StockExchangeTrade_ReturnType) result.data;
              if (this.stockExchangeTrade_UserCallBack != null)
                this.stockExchangeTrade_UserCallBack(data78);
            }
            else if (result.classType == typeof (UpdateVillageFavourites_ReturnType))
            {
              UpdateVillageFavourites_ReturnType data79 = (UpdateVillageFavourites_ReturnType) result.data;
              if (this.updateVillageFavourites_UserCallBack != null)
                this.updateVillageFavourites_UserCallBack(data79);
            }
            else if (result.classType == typeof (MakeTroop_ReturnType))
            {
              MakeTroop_ReturnType data80 = (MakeTroop_ReturnType) result.data;
              if (this.makeTroop_UserCallBack != null)
                this.makeTroop_UserCallBack(data80);
            }
            else if (result.classType == typeof (DisbandTroops_ReturnType))
            {
              DisbandTroops_ReturnType data81 = (DisbandTroops_ReturnType) result.data;
              if (this.disbandTroops_UserCallBack != null)
                this.disbandTroops_UserCallBack(data81);
            }
            else if (result.classType == typeof (DisbandPeople_ReturnType))
            {
              DisbandPeople_ReturnType data82 = (DisbandPeople_ReturnType) result.data;
              if (this.disbandPeople_UserCallBack != null)
                this.disbandPeople_UserCallBack(data82);
            }
            else if (result.classType == typeof (GetVillageInfoForDonateCapitalGoods_ReturnType))
            {
              GetVillageInfoForDonateCapitalGoods_ReturnType data83 = (GetVillageInfoForDonateCapitalGoods_ReturnType) result.data;
              if (this.getVillageInfoForDonateCapitalGoods_UserCallBack != null)
                this.getVillageInfoForDonateCapitalGoods_UserCallBack(data83);
            }
            else if (result.classType == typeof (DonateCapitalGoods_ReturnType))
            {
              DonateCapitalGoods_ReturnType data84 = (DonateCapitalGoods_ReturnType) result.data;
              if (this.donateCapitalGoods_UserCallBack != null)
                this.donateCapitalGoods_UserCallBack(data84);
            }
            else if (result.classType == typeof (GetVillageStartLocations_ReturnType))
            {
              GetVillageStartLocations_ReturnType data85 = (GetVillageStartLocations_ReturnType) result.data;
              if (this.getVillageStartLocations_UserCallBack != null)
                this.getVillageStartLocations_UserCallBack(data85);
            }
            else if (result.classType == typeof (SetStartingCounty_ReturnType))
            {
              SetStartingCounty_ReturnType data86 = (SetStartingCounty_ReturnType) result.data;
              if (this.setStartingCounty_UserCallBack != null)
                this.setStartingCounty_UserCallBack(data86);
            }
            else if (result.classType == typeof (UpdateCurrentCards_ReturnType))
            {
              UpdateCurrentCards_ReturnType data87 = (UpdateCurrentCards_ReturnType) result.data;
              if (this.updateCurrentCards_UserCallBack != null)
                this.updateCurrentCards_UserCallBack(data87);
            }
            else if (result.classType == typeof (CancelCard_ReturnType))
            {
              CancelCard_ReturnType data88 = (CancelCard_ReturnType) result.data;
              if (this.cancelCard_UserCallBack != null)
                this.cancelCard_UserCallBack(data88);
            }
            else if (result.classType == typeof (TutorialCommand_ReturnType))
            {
              TutorialCommand_ReturnType data89 = (TutorialCommand_ReturnType) result.data;
              if (this.tutorialCommand_UserCallBack != null)
                this.tutorialCommand_UserCallBack(data89);
            }
            else if (result.classType == typeof (FlagQuestObjectiveComplete_ReturnType))
            {
              FlagQuestObjectiveComplete_ReturnType data90 = (FlagQuestObjectiveComplete_ReturnType) result.data;
              if (this.flagQuestObjectiveComplete_UserCallBack != null)
                this.flagQuestObjectiveComplete_UserCallBack(data90);
            }
            else if (result.classType == typeof (CheckQuestObjectiveComplete_ReturnType))
            {
              CheckQuestObjectiveComplete_ReturnType data91 = (CheckQuestObjectiveComplete_ReturnType) result.data;
              if (this.checkQuestObjectiveComplete_UserCallBack != null)
                this.checkQuestObjectiveComplete_UserCallBack(data91);
            }
            else if (result.classType == typeof (UpdateDiplomacyStatus_ReturnType))
            {
              UpdateDiplomacyStatus_ReturnType data92 = (UpdateDiplomacyStatus_ReturnType) result.data;
              if (this.updateDiplomacyStatus_UserCallBack != null)
                this.updateDiplomacyStatus_UserCallBack(data92);
            }
            else if (result.classType == typeof (SendCommands_ReturnType))
            {
              SendCommands_ReturnType data93 = (SendCommands_ReturnType) result.data;
              if (this.sendCommands_UserCallBack != null)
                this.sendCommands_UserCallBack(data93);
            }
            else if (result.classType == typeof (GetQuestStatus_ReturnType))
            {
              GetQuestStatus_ReturnType data94 = (GetQuestStatus_ReturnType) result.data;
              if (this.getQuestStatus_UserCallBack != null)
                this.getQuestStatus_UserCallBack(data94);
            }
            else if (result.classType == typeof (CompleteQuest_ReturnType))
            {
              CompleteQuest_ReturnType data95 = (CompleteQuest_ReturnType) result.data;
              if (this.completeQuest_UserCallBack != null)
                this.completeQuest_UserCallBack(data95);
            }
            else if (result.classType == typeof (UpgradeRank_ReturnType))
            {
              UpgradeRank_ReturnType data96 = (UpgradeRank_ReturnType) result.data;
              if (this.upgradeRank_UserCallBack != null)
                this.upgradeRank_UserCallBack(data96);
            }
            else if (result.classType == typeof (PreAttackSetup_ReturnType))
            {
              PreAttackSetup_ReturnType data97 = (PreAttackSetup_ReturnType) result.data;
              if (this.preAttackSetup_UserCallBack != null)
                this.preAttackSetup_UserCallBack(data97);
            }
            else if (result.classType == typeof (GetBattleHonourRating_ReturnType))
            {
              GetBattleHonourRating_ReturnType data98 = (GetBattleHonourRating_ReturnType) result.data;
              if (this.getBattleHonourRating_UserCallBack != null)
                this.getBattleHonourRating_UserCallBack(data98);
            }
            else if (result.classType == typeof (RetrieveArmyFromGarrison_ReturnType))
            {
              RetrieveArmyFromGarrison_ReturnType data99 = (RetrieveArmyFromGarrison_ReturnType) result.data;
              if (this.retrieveArmyFromGarrison_UserCallBack != null)
                this.retrieveArmyFromGarrison_UserCallBack(data99);
            }
            else if (result.classType == typeof (GetVillageRankTaxTree_ReturnType))
            {
              GetVillageRankTaxTree_ReturnType data100 = (GetVillageRankTaxTree_ReturnType) result.data;
              if (this.getVillageRankTaxTree_UserCallBack != null)
                this.getVillageRankTaxTree_UserCallBack(data100);
            }
            else if (result.classType == typeof (GetResearchData_ReturnType))
            {
              GetResearchData_ReturnType data101 = (GetResearchData_ReturnType) result.data;
              if (this.getResearchData_UserCallBack != null)
                this.getResearchData_UserCallBack(data101);
            }
            else if (result.classType == typeof (DoResearch_ReturnType))
            {
              DoResearch_ReturnType data102 = (DoResearch_ReturnType) result.data;
              if (this.doResearch_UserCallBack != null)
                this.doResearch_UserCallBack(data102);
            }
            else if (result.classType == typeof (BuyResearchPoint_ReturnType))
            {
              BuyResearchPoint_ReturnType data103 = (BuyResearchPoint_ReturnType) result.data;
              if (this.buyResearchPoint_UserCallBack != null)
                this.buyResearchPoint_UserCallBack(data103);
            }
            else if (result.classType == typeof (SendReinforcements_ReturnType))
            {
              SendReinforcements_ReturnType data104 = (SendReinforcements_ReturnType) result.data;
              if (this.sendReinforcements_UserCallBack != null)
                this.sendReinforcements_UserCallBack(data104);
            }
            else if (result.classType == typeof (ReturnReinforcements_ReturnType))
            {
              ReturnReinforcements_ReturnType data105 = (ReturnReinforcements_ReturnType) result.data;
              if (this.returnReinforcements_UserCallBack != null)
                this.returnReinforcements_UserCallBack(data105);
            }
            else if (result.classType == typeof (CancelCastleAttack_ReturnType))
            {
              CancelCastleAttack_ReturnType data106 = (CancelCastleAttack_ReturnType) result.data;
              if (this.cancelCastleAttack_UserCallBack != null)
                this.cancelCastleAttack_UserCallBack(data106);
            }
            else if (result.classType == typeof (VassalInfo_ReturnType))
            {
              VassalInfo_ReturnType data107 = (VassalInfo_ReturnType) result.data;
              if (this.vassalInfo_UserCallBack != null)
                this.vassalInfo_UserCallBack(data107);
            }
            else if (result.classType == typeof (HandleVassalRequest_ReturnType))
            {
              HandleVassalRequest_ReturnType data108 = (HandleVassalRequest_ReturnType) result.data;
              if (this.handleVassalRequest_UserCallBack != null)
                this.handleVassalRequest_UserCallBack(data108);
            }
            else if (result.classType == typeof (GetVassalArmyInfo_ReturnType))
            {
              GetVassalArmyInfo_ReturnType data109 = (GetVassalArmyInfo_ReturnType) result.data;
              if (this.getVassalArmyInfo_UserCallBack != null)
                this.getVassalArmyInfo_UserCallBack(data109);
            }
            else if (result.classType == typeof (SendTroopsToVassal_ReturnType))
            {
              SendTroopsToVassal_ReturnType data110 = (SendTroopsToVassal_ReturnType) result.data;
              if (this.sendTroopsToVassal_UserCallBack != null)
                this.sendTroopsToVassal_UserCallBack(data110);
            }
            else if (result.classType == typeof (RetrieveTroopsFromVassal_ReturnType))
            {
              RetrieveTroopsFromVassal_ReturnType data111 = (RetrieveTroopsFromVassal_ReturnType) result.data;
              if (this.retrieveTroopsFromVassal_UserCallBack != null)
                this.retrieveTroopsFromVassal_UserCallBack(data111);
            }
            else if (result.classType == typeof (VassalSendResources_ReturnType))
            {
              VassalSendResources_ReturnType data112 = (VassalSendResources_ReturnType) result.data;
              if (this.vassalSendResources_UserCallBack != null)
                this.vassalSendResources_UserCallBack(data112);
            }
            else if (result.classType == typeof (UpdateSelectedTitheType_ReturnType))
            {
              UpdateSelectedTitheType_ReturnType data113 = (UpdateSelectedTitheType_ReturnType) result.data;
              if (this.updateSelectedTitheType_UserCallBack != null)
                this.updateSelectedTitheType_UserCallBack(data113);
            }
            else if (result.classType == typeof (BreakVassalage_ReturnType))
            {
              BreakVassalage_ReturnType data114 = (BreakVassalage_ReturnType) result.data;
              if (this.breakVassalage_UserCallBack != null)
                this.breakVassalage_UserCallBack(data114);
            }
            else if (result.classType == typeof (SendVassalRequest_ReturnType))
            {
              SendVassalRequest_ReturnType data115 = (SendVassalRequest_ReturnType) result.data;
              if (this.sendVassalRequest_UserCallBack != null)
                this.sendVassalRequest_UserCallBack(data115);
            }
            else if (result.classType == typeof (GetPreVassalInfo_ReturnType))
            {
              GetPreVassalInfo_ReturnType data116 = (GetPreVassalInfo_ReturnType) result.data;
              if (this.getPreVassalInfo_UserCallBack != null)
                this.getPreVassalInfo_UserCallBack(data116);
            }
            else if (result.classType == typeof (BreakLiegeLord_ReturnType))
            {
              BreakLiegeLord_ReturnType data117 = (BreakLiegeLord_ReturnType) result.data;
              if (this.breakLiegeLord_UserCallBack != null)
                this.breakLiegeLord_UserCallBack(data117);
            }
            else if (result.classType == typeof (UpdateVillageResourcesInfo_ReturnType))
            {
              UpdateVillageResourcesInfo_ReturnType data118 = (UpdateVillageResourcesInfo_ReturnType) result.data;
              if (this.updateVillageResourcesInfo_UserCallBack != null)
                this.updateVillageResourcesInfo_UserCallBack(data118);
            }
            else if (result.classType == typeof (SendScouts_ReturnType))
            {
              SendScouts_ReturnType data119 = (SendScouts_ReturnType) result.data;
              if (this.sendScouts_UserCallBack != null)
              {
                try
                {
                  this.sendScouts_UserCallBack(data119);
                }
                catch (Exception ex)
                {
                }
              }
            }
            else if (result.classType == typeof (SetHighestArmySeen_ReturnType))
            {
              SetHighestArmySeen_ReturnType data120 = (SetHighestArmySeen_ReturnType) result.data;
              if (this.setHighestArmySeen_UserCallBack != null)
                this.setHighestArmySeen_UserCallBack(data120);
            }
            else if (result.classType == typeof (GetForumList_ReturnType))
            {
              GetForumList_ReturnType data121 = (GetForumList_ReturnType) result.data;
              if (this.getForumList_UserCallBack != null)
                this.getForumList_UserCallBack(data121);
            }
            else if (result.classType == typeof (GetForumThreadList_ReturnType))
            {
              GetForumThreadList_ReturnType data122 = (GetForumThreadList_ReturnType) result.data;
              if (this.getForumThreadList_UserCallBack != null)
                this.getForumThreadList_UserCallBack(data122);
            }
            else if (result.classType == typeof (GetForumThread_ReturnType))
            {
              GetForumThread_ReturnType data123 = (GetForumThread_ReturnType) result.data;
              if (this.getForumThread_UserCallBack != null)
                this.getForumThread_UserCallBack(data123);
            }
            else if (result.classType == typeof (NewForumThread_ReturnType))
            {
              NewForumThread_ReturnType data124 = (NewForumThread_ReturnType) result.data;
              if (this.newForumThread_UserCallBack != null)
                this.newForumThread_UserCallBack(data124);
            }
            else if (result.classType == typeof (PostToForumThread_ReturnType))
            {
              PostToForumThread_ReturnType data125 = (PostToForumThread_ReturnType) result.data;
              if (this.postToForumThread_UserCallBack != null)
                this.postToForumThread_UserCallBack(data125);
            }
            else if (result.classType == typeof (GiveForumAccess_ReturnType))
            {
              GiveForumAccess_ReturnType data126 = (GiveForumAccess_ReturnType) result.data;
              if (this.giveForumAccess_UserCallBack != null)
                this.giveForumAccess_UserCallBack(data126);
            }
            else if (result.classType == typeof (CreateForum_ReturnType))
            {
              CreateForum_ReturnType data127 = (CreateForum_ReturnType) result.data;
              if (this.createForum_UserCallBack != null)
                this.createForum_UserCallBack(data127);
            }
            else if (result.classType == typeof (DeleteForum_ReturnType))
            {
              DeleteForum_ReturnType data128 = (DeleteForum_ReturnType) result.data;
              if (this.deleteForum_UserCallBack != null)
                this.deleteForum_UserCallBack(data128);
            }
            else if (result.classType == typeof (DeleteForumThread_ReturnType))
            {
              DeleteForumThread_ReturnType data129 = (DeleteForumThread_ReturnType) result.data;
              if (this.deleteForumThread_UserCallBack != null)
                this.deleteForumThread_UserCallBack(data129);
            }
            else if (result.classType == typeof (DeleteForumPost_ReturnType))
            {
              DeleteForumPost_ReturnType data130 = (DeleteForumPost_ReturnType) result.data;
              if (this.deleteForumPost_UserCallBack != null)
                this.deleteForumPost_UserCallBack(data130);
            }
            else if (result.classType == typeof (GetCurrentElectionInfo_ReturnType))
            {
              GetCurrentElectionInfo_ReturnType data131 = (GetCurrentElectionInfo_ReturnType) result.data;
              if (this.getCurrentElectionInfo_UserCallBack != null)
                this.getCurrentElectionInfo_UserCallBack(data131);
            }
            else if (result.classType == typeof (StandInElection_ReturnType))
            {
              StandInElection_ReturnType data132 = (StandInElection_ReturnType) result.data;
              if (this.standInElection_UserCallBack != null)
                this.standInElection_UserCallBack(data132);
            }
            else if (result.classType == typeof (VoteInElection_ReturnType))
            {
              VoteInElection_ReturnType data133 = (VoteInElection_ReturnType) result.data;
              if (this.voteInElection_UserCallBack != null)
                this.voteInElection_UserCallBack(data133);
            }
            else if (result.classType == typeof (UploadAvatar_ReturnType))
            {
              UploadAvatar_ReturnType data134 = (UploadAvatar_ReturnType) result.data;
              if (this.uploadAvatar_UserCallBack != null)
                this.uploadAvatar_UserCallBack(data134);
            }
            else if (result.classType == typeof (MakePeople_ReturnType))
            {
              MakePeople_ReturnType data135 = (MakePeople_ReturnType) result.data;
              if (this.makePeople_UserCallBack != null)
                this.makePeople_UserCallBack(data135);
            }
            else if (result.classType == typeof (GetUserPeople_ReturnType))
            {
              GetUserPeople_ReturnType data136 = (GetUserPeople_ReturnType) result.data;
              if (this.getUserPeople_UserCallBack != null)
                this.getUserPeople_UserCallBack(data136);
            }
            else if (result.classType == typeof (GetUserIDFromName_ReturnType))
            {
              GetUserIDFromName_ReturnType data137 = (GetUserIDFromName_ReturnType) result.data;
              if (this.getUserIDFromName_UserCallBack != null)
                this.getUserIDFromName_UserCallBack(data137);
            }
            else if (result.classType == typeof (GetActivePeople_ReturnType))
            {
              GetActivePeople_ReturnType data138 = (GetActivePeople_ReturnType) result.data;
              if (this.getActivePeople_UserCallBack != null)
                this.getActivePeople_UserCallBack(data138);
            }
            else if (result.classType == typeof (SendPeople_ReturnType))
            {
              SendPeople_ReturnType data139 = (SendPeople_ReturnType) result.data;
              if (this.sendPeople_UserCallBack != null)
                this.sendPeople_UserCallBack(data139);
            }
            else if (result.classType == typeof (RetrievePeople_ReturnType))
            {
              RetrievePeople_ReturnType data140 = (RetrievePeople_ReturnType) result.data;
              if (this.retrievePeople_UserCallBack != null)
                this.retrievePeople_UserCallBack(data140);
            }
            else if (result.classType == typeof (SpyCommand_ReturnType))
            {
              SpyCommand_ReturnType data141 = (SpyCommand_ReturnType) result.data;
              if (this.spyCommand_UserCallBack != null)
                this.spyCommand_UserCallBack(data141);
            }
            else if (result.classType == typeof (SpyGetVillageResourceInfo_ReturnType))
            {
              SpyGetVillageResourceInfo_ReturnType data142 = (SpyGetVillageResourceInfo_ReturnType) result.data;
              if (this.spyGetVillageResourceInfo_UserCallBack != null)
                this.spyGetVillageResourceInfo_UserCallBack(data142);
            }
            else if (result.classType == typeof (SpyGetArmyInfo_ReturnType))
            {
              SpyGetArmyInfo_ReturnType data143 = (SpyGetArmyInfo_ReturnType) result.data;
              if (this.spyGetArmyInfo_UserCallBack != null)
                this.spyGetArmyInfo_UserCallBack(data143);
            }
            else if (result.classType == typeof (SpyGetResearchInfo_ReturnType))
            {
              SpyGetResearchInfo_ReturnType data144 = (SpyGetResearchInfo_ReturnType) result.data;
              if (this.spyGetResearchInfo_UserCallBack != null)
                this.spyGetResearchInfo_UserCallBack(data144);
            }
            else if (result.classType == typeof (GetLoginHistory_ReturnType))
            {
              GetLoginHistory_ReturnType data145 = (GetLoginHistory_ReturnType) result.data;
              if (this.getLoginHistory_UserCallBack != null)
                this.getLoginHistory_UserCallBack(data145);
            }
            else if (result.classType == typeof (CreateFaction_ReturnType))
            {
              CreateFaction_ReturnType data146 = (CreateFaction_ReturnType) result.data;
              if (this.createFaction_UserCallBack != null)
                this.createFaction_UserCallBack(data146);
            }
            else if (result.classType == typeof (DisbandFaction_ReturnType))
            {
              DisbandFaction_ReturnType data147 = (DisbandFaction_ReturnType) result.data;
              if (this.disbandFaction_UserCallBack != null)
                this.disbandFaction_UserCallBack(data147);
            }
            else if (result.classType == typeof (FactionSendInvite_ReturnType))
            {
              FactionSendInvite_ReturnType data148 = (FactionSendInvite_ReturnType) result.data;
              if (this.factionSendInvite_UserCallBack != null)
                this.factionSendInvite_UserCallBack(data148);
            }
            else if (result.classType == typeof (FactionWithdrawInvite_ReturnType))
            {
              FactionWithdrawInvite_ReturnType data149 = (FactionWithdrawInvite_ReturnType) result.data;
              if (this.factionWithdrawInvite_UserCallBack != null)
                this.factionWithdrawInvite_UserCallBack(data149);
            }
            else if (result.classType == typeof (FactionReplyToInvite_ReturnType))
            {
              FactionReplyToInvite_ReturnType data150 = (FactionReplyToInvite_ReturnType) result.data;
              if (this.factionReplyToInvite_UserCallBack != null)
                this.factionReplyToInvite_UserCallBack(data150);
            }
            else if (result.classType == typeof (FactionChangeMemberStatus_ReturnType))
            {
              FactionChangeMemberStatus_ReturnType data151 = (FactionChangeMemberStatus_ReturnType) result.data;
              if (this.factionChangeMemberStatus_UserCallBack != null)
                this.factionChangeMemberStatus_UserCallBack(data151);
            }
            else if (result.classType == typeof (FactionLeave_ReturnType))
            {
              FactionLeave_ReturnType data152 = (FactionLeave_ReturnType) result.data;
              if (this.factionLeave_UserCallBack != null)
                this.factionLeave_UserCallBack(data152);
            }
            else if (result.classType == typeof (FactionApplication_ReturnType))
            {
              FactionApplication_ReturnType data153 = (FactionApplication_ReturnType) result.data;
              if (this.factionApplication_UserCallBack != null)
                this.factionApplication_UserCallBack(data153);
            }
            else if (result.classType == typeof (FactionApplicationProcessing_ReturnType))
            {
              FactionApplicationProcessing_ReturnType data154 = (FactionApplicationProcessing_ReturnType) result.data;
              if (this.factionApplicationProcessing_UserCallBack != null)
                this.factionApplicationProcessing_UserCallBack(data154);
            }
            else if (result.classType == typeof (GetFactionData_ReturnType))
            {
              GetFactionData_ReturnType data155 = (GetFactionData_ReturnType) result.data;
              if (this.getFactionData_UserCallBack != null)
                this.getFactionData_UserCallBack(data155);
            }
            else if (result.classType == typeof (FactionLeadershipVote_ReturnType))
            {
              FactionLeadershipVote_ReturnType data156 = (FactionLeadershipVote_ReturnType) result.data;
              if (this.factionLeadershipVote_UserCallBack != null)
                this.factionLeadershipVote_UserCallBack(data156);
            }
            else if (result.classType == typeof (CreateUserRelationship_ReturnType))
            {
              CreateUserRelationship_ReturnType data157 = (CreateUserRelationship_ReturnType) result.data;
              if (this.createUserRelationship_UserCallBack != null)
                this.createUserRelationship_UserCallBack(data157);
            }
            else if (result.classType == typeof (CreateFactionRelationship_ReturnType))
            {
              CreateFactionRelationship_ReturnType data158 = (CreateFactionRelationship_ReturnType) result.data;
              if (this.createFactionRelationship_UserCallBack != null)
                this.createFactionRelationship_UserCallBack(data158);
            }
            else if (result.classType == typeof (ChangeFactionMotto_ReturnType))
            {
              ChangeFactionMotto_ReturnType data159 = (ChangeFactionMotto_ReturnType) result.data;
              if (this.changeFactionMotto_UserCallBack != null)
                this.changeFactionMotto_UserCallBack(data159);
            }
            else if (result.classType == typeof (CreateHouseRelationship_ReturnType))
            {
              CreateHouseRelationship_ReturnType data160 = (CreateHouseRelationship_ReturnType) result.data;
              if (this.createHouseRelationship_UserCallBack != null)
                this.createHouseRelationship_UserCallBack(data160);
            }
            else if (result.classType == typeof (GetHouseGloryPoints_ReturnType))
            {
              GetHouseGloryPoints_ReturnType data161 = (GetHouseGloryPoints_ReturnType) result.data;
              if (this.getHouseGloryPoints_UserCallBack != null)
                this.getHouseGloryPoints_UserCallBack(data161);
            }
            else if (result.classType == typeof (GetViewFactionData_ReturnType))
            {
              GetViewFactionData_ReturnType data162 = (GetViewFactionData_ReturnType) result.data;
              if (this.getViewFactionData_UserCallBack != null)
                this.getViewFactionData_UserCallBack(data162);
            }
            else if (result.classType == typeof (GetViewHouseData_ReturnType))
            {
              GetViewHouseData_ReturnType data163 = (GetViewHouseData_ReturnType) result.data;
              if (this.getViewHouseData_UserCallBack != null)
                this.getViewHouseData_UserCallBack(data163);
            }
            else if (result.classType == typeof (SelfJoinHouse_ReturnType))
            {
              SelfJoinHouse_ReturnType data164 = (SelfJoinHouse_ReturnType) result.data;
              if (this.selfJoinHouse_UserCallBack != null)
                this.selfJoinHouse_UserCallBack(data164);
            }
            else if (result.classType == typeof (HouseVote_ReturnType))
            {
              HouseVote_ReturnType data165 = (HouseVote_ReturnType) result.data;
              if (this.houseVote_UserCallBack != null)
                this.houseVote_UserCallBack(data165);
            }
            else if (result.classType == typeof (HouseVoteHouseLeader_ReturnType))
            {
              HouseVoteHouseLeader_ReturnType data166 = (HouseVoteHouseLeader_ReturnType) result.data;
              if (this.houseVoteHouseLeader_UserCallBack != null)
                this.houseVoteHouseLeader_UserCallBack(data166);
            }
            else if (result.classType == typeof (TouchHouseVisitDate_ReturnType))
            {
              TouchHouseVisitDate_ReturnType data167 = (TouchHouseVisitDate_ReturnType) result.data;
              if (this.touchHouseVisitDate_UserCallBack != null)
                this.touchHouseVisitDate_UserCallBack(data167);
            }
            else if (result.classType == typeof (LeaveHouse_ReturnType))
            {
              LeaveHouse_ReturnType data168 = (LeaveHouse_ReturnType) result.data;
              if (this.leaveHouse_UserCallBack != null)
                this.leaveHouse_UserCallBack(data168);
            }
            else if (result.classType == typeof (GetParishMembersList_ReturnType))
            {
              GetParishMembersList_ReturnType data169 = (GetParishMembersList_ReturnType) result.data;
              if (this.getParishMembersList_UserCallBack != null)
                this.getParishMembersList_UserCallBack(data169);
            }
            else if (result.classType == typeof (GetParishFrontPageInfo_ReturnType))
            {
              GetParishFrontPageInfo_ReturnType data170 = (GetParishFrontPageInfo_ReturnType) result.data;
              if (this.getParishFrontPageInfo_UserCallBack != null)
                this.getParishFrontPageInfo_UserCallBack(data170);
            }
            else if (result.classType == typeof (ParishWallDetailInfo_ReturnType))
            {
              ParishWallDetailInfo_ReturnType data171 = (ParishWallDetailInfo_ReturnType) result.data;
              if (this.parishWallDetailInfo_UserCallBack != null)
                this.parishWallDetailInfo_UserCallBack(data171);
            }
            else if (result.classType == typeof (GetCountyElectionInfo_ReturnType))
            {
              GetCountyElectionInfo_ReturnType data172 = (GetCountyElectionInfo_ReturnType) result.data;
              if (this.getCountyElectionInfo_UserCallBack != null)
                this.getCountyElectionInfo_UserCallBack(data172);
            }
            else if (result.classType == typeof (GetCountyFrontPageInfo_ReturnType))
            {
              GetCountyFrontPageInfo_ReturnType data173 = (GetCountyFrontPageInfo_ReturnType) result.data;
              if (this.getCountyFrontPageInfo_UserCallBack != null)
                this.getCountyFrontPageInfo_UserCallBack(data173);
            }
            else if (result.classType == typeof (StandDownAsParishDespot_ReturnType))
            {
              StandDownAsParishDespot_ReturnType data174 = (StandDownAsParishDespot_ReturnType) result.data;
              if (this.standDownAsParishDespot_UserCallBack != null)
                this.standDownAsParishDespot_UserCallBack(data174);
            }
            else if (result.classType == typeof (MakeParishVote_ReturnType))
            {
              MakeParishVote_ReturnType data175 = (MakeParishVote_ReturnType) result.data;
              if (this.makeParishVote_UserCallBack != null)
                this.makeParishVote_UserCallBack(data175);
            }
            else if (result.classType == typeof (MakeCountyVote_ReturnType))
            {
              MakeCountyVote_ReturnType data176 = (MakeCountyVote_ReturnType) result.data;
              if (this.makeCountyVote_UserCallBack != null)
                this.makeCountyVote_UserCallBack(data176);
            }
            else if (result.classType == typeof (GetCountryElectionInfo_ReturnType))
            {
              GetCountryElectionInfo_ReturnType data177 = (GetCountryElectionInfo_ReturnType) result.data;
              if (this.getCountryElectionInfo_UserCallBack != null)
                this.getCountryElectionInfo_UserCallBack(data177);
            }
            else if (result.classType == typeof (GetCountryFrontPageInfo_ReturnType))
            {
              GetCountryFrontPageInfo_ReturnType data178 = (GetCountryFrontPageInfo_ReturnType) result.data;
              if (this.getCountryFrontPageInfo_UserCallBack != null)
                this.getCountryFrontPageInfo_UserCallBack(data178);
            }
            else if (result.classType == typeof (MakeCountryVote_ReturnType))
            {
              MakeCountryVote_ReturnType data179 = (MakeCountryVote_ReturnType) result.data;
              if (this.makeCountryVote_UserCallBack != null)
                this.makeCountryVote_UserCallBack(data179);
            }
            else if (result.classType == typeof (GetProvinceElectionInfo_ReturnType))
            {
              GetProvinceElectionInfo_ReturnType data180 = (GetProvinceElectionInfo_ReturnType) result.data;
              if (this.getProvinceElectionInfo_UserCallBack != null)
                this.getProvinceElectionInfo_UserCallBack(data180);
            }
            else if (result.classType == typeof (GetProvinceFrontPageInfo_ReturnType))
            {
              GetProvinceFrontPageInfo_ReturnType data181 = (GetProvinceFrontPageInfo_ReturnType) result.data;
              if (this.getProvinceFrontPageInfo_UserCallBack != null)
                this.getProvinceFrontPageInfo_UserCallBack(data181);
            }
            else if (result.classType == typeof (MakeProvinceVote_ReturnType))
            {
              MakeProvinceVote_ReturnType data182 = (MakeProvinceVote_ReturnType) result.data;
              if (this.makeProvinceVote_UserCallBack != null)
                this.makeProvinceVote_UserCallBack(data182);
            }
            else if (result.classType == typeof (SendTroopsToCapital_ReturnType))
            {
              SendTroopsToCapital_ReturnType data183 = (SendTroopsToCapital_ReturnType) result.data;
              if (this.sendTroopsToCapital_UserCallBack != null)
                this.sendTroopsToCapital_UserCallBack(data183);
            }
            else if (result.classType == typeof (GetCapitalBarracksSpace_ReturnType))
            {
              GetCapitalBarracksSpace_ReturnType data184 = (GetCapitalBarracksSpace_ReturnType) result.data;
              if (this.getCapitalBarracksSpace_UserCallBack != null)
                this.getCapitalBarracksSpace_UserCallBack(data184);
            }
            else if (result.classType == typeof (GetIngameMessage_ReturnType))
            {
              GetIngameMessage_ReturnType data185 = (GetIngameMessage_ReturnType) result.data;
              if (this.getIngameMessage_UserCallBack != null)
                this.getIngameMessage_UserCallBack(data185);
            }
            else if (result.classType == typeof (CancelInterdiction_ReturnType))
            {
              CancelInterdiction_ReturnType data186 = (CancelInterdiction_ReturnType) result.data;
              if (this.cancelInterdiction_UserCallBack != null)
                this.cancelInterdiction_UserCallBack(data186);
            }
            else if (result.classType == typeof (GetExcommunicationStatus_ReturnType))
            {
              GetExcommunicationStatus_ReturnType data187 = (GetExcommunicationStatus_ReturnType) result.data;
              if (this.getExcommunicationStatus_UserCallBack != null)
                this.getExcommunicationStatus_UserCallBack(data187);
            }
            else if (result.classType == typeof (InitialiseFreeCards_ReturnType))
            {
              InitialiseFreeCards_ReturnType data188 = (InitialiseFreeCards_ReturnType) result.data;
              if (this.initialiseFreeCards_UserCallBack != null)
                this.initialiseFreeCards_UserCallBack(data188);
            }
            else if (result.classType == typeof (TestAchievements_ReturnType))
            {
              TestAchievements_ReturnType data189 = (TestAchievements_ReturnType) result.data;
              if (this.testAchievements_UserCallBack != null)
                this.testAchievements_UserCallBack(data189);
            }
            else if (result.classType == typeof (AchievementProgress_ReturnType))
            {
              AchievementProgress_ReturnType data190 = (AchievementProgress_ReturnType) result.data;
              if (this.achievementProgress_UserCallBack != null)
                this.achievementProgress_UserCallBack(data190);
            }
            else if (result.classType == typeof (GetQuestData_ReturnType))
            {
              GetQuestData_ReturnType data191 = (GetQuestData_ReturnType) result.data;
              if (this.getQuestData_UserCallBack != null)
                this.getQuestData_UserCallBack(data191);
            }
            else if (result.classType == typeof (StartNewQuest_ReturnType))
            {
              StartNewQuest_ReturnType data192 = (StartNewQuest_ReturnType) result.data;
              if (this.startNewQuest_UserCallBack != null)
                this.startNewQuest_UserCallBack(data192);
            }
            else if (result.classType == typeof (CompleteAbandonNewQuest_ReturnType))
            {
              CompleteAbandonNewQuest_ReturnType data193 = (CompleteAbandonNewQuest_ReturnType) result.data;
              if (this.completeAbandonNewQuest_UserCallBack != null)
                this.completeAbandonNewQuest_UserCallBack(data193);
            }
            else if (result.classType == typeof (SpinTheWheel_ReturnType))
            {
              SpinTheWheel_ReturnType data194 = (SpinTheWheel_ReturnType) result.data;
              if (this.spinTheWheel_UserCallBack != null)
                this.spinTheWheel_UserCallBack(data194);
            }
            else if (result.classType == typeof (SetVacationMode_ReturnType))
            {
              SetVacationMode_ReturnType data195 = (SetVacationMode_ReturnType) result.data;
              if (this.setVacationMode_UserCallBack != null)
                this.setVacationMode_UserCallBack(data195);
            }
            else if (result.classType == typeof (PremiumOverview_ReturnType))
            {
              PremiumOverview_ReturnType data196 = (PremiumOverview_ReturnType) result.data;
              if (this.premiumOverview_UserCallBack != null)
                this.premiumOverview_UserCallBack(data196);
            }
            else if (result.classType == typeof (GetLastAttacker_ReturnType))
            {
              GetLastAttacker_ReturnType data197 = (GetLastAttacker_ReturnType) result.data;
              if (this.getLastAttacker_UserCallBack != null)
                this.getLastAttacker_UserCallBack(data197);
            }
            else if (result.classType == typeof (GetInvasionInfo_ReturnType))
            {
              GetInvasionInfo_ReturnType data198 = (GetInvasionInfo_ReturnType) result.data;
              if (this.getInvasionInfo_UserCallBack != null)
                this.getInvasionInfo_UserCallBack(data198);
            }
            else if (result.classType == typeof (PreValidateCardToBePlayed_ReturnType))
            {
              PreValidateCardToBePlayed_ReturnType data199 = (PreValidateCardToBePlayed_ReturnType) result.data;
              if (this.preValidateCardToBePlayed_UserCallBack != null)
                this.preValidateCardToBePlayed_UserCallBack(data199);
            }
            else if (result.classType == typeof (WorldInfo_ReturnType))
            {
              WorldInfo_ReturnType data200 = (WorldInfo_ReturnType) result.data;
              if (this.worldInfo_UserCallBack != null)
                this.worldInfo_UserCallBack(data200);
            }
            else if (result.classType == typeof (EndOfTheWorldStats_ReturnType))
            {
              EndOfTheWorldStats_ReturnType data201 = (EndOfTheWorldStats_ReturnType) result.data;
              if (this.endOfTheWorldStats_UserCallBack != null)
                this.endOfTheWorldStats_UserCallBack(data201);
            }
            else if (result.classType == typeof (GetKillStreakData_ReturnType))
            {
              GetKillStreakData_ReturnType data202 = (GetKillStreakData_ReturnType) result.data;
              if (this.getKillStreakData_UserCallBack != null)
                this.getKillStreakData_UserCallBack(data202);
            }
            else if (result.classType == typeof (EndWorld_ReturnType))
            {
              EndWorld_ReturnType data203 = (EndWorld_ReturnType) result.data;
              if (this.endWorld_UserCallBack != null)
                this.endWorld_UserCallBack(data203);
            }
            else if (result.classType == typeof (Chat_Login_ReturnType))
            {
              Chat_Login_ReturnType data204 = (Chat_Login_ReturnType) result.data;
              if (this.chat_Login_UserCallBack != null)
                this.chat_Login_UserCallBack(data204);
            }
            else if (result.classType == typeof (Chat_Logout_ReturnType))
            {
              Chat_Logout_ReturnType data205 = (Chat_Logout_ReturnType) result.data;
              if (this.chat_Logout_UserCallBack != null)
                this.chat_Logout_UserCallBack(data205);
            }
            else if (result.classType == typeof (Chat_SetReceivingState_ReturnType))
            {
              Chat_SetReceivingState_ReturnType data206 = (Chat_SetReceivingState_ReturnType) result.data;
              if (this.chat_SetReceivingState_UserCallBack != null)
                this.chat_SetReceivingState_UserCallBack(data206);
            }
            else if (result.classType == typeof (Chat_SendText_ReturnType))
            {
              Chat_SendText_ReturnType data207 = (Chat_SendText_ReturnType) result.data;
              if (this.chat_SendText_UserCallBack != null)
                this.chat_SendText_UserCallBack(data207);
            }
            else if (result.classType == typeof (Chat_ReceiveText_ReturnType))
            {
              Chat_ReceiveText_ReturnType data208 = (Chat_ReceiveText_ReturnType) result.data;
              if (this.chat_ReceiveText_UserCallBack != null)
                this.chat_ReceiveText_UserCallBack(data208);
            }
            else if (result.classType == typeof (Chat_SendParishText_ReturnType))
            {
              Chat_SendParishText_ReturnType data209 = (Chat_SendParishText_ReturnType) result.data;
              if (this.chat_SendParishText_UserCallBack != null)
                this.chat_SendParishText_UserCallBack(data209);
            }
            else if (result.classType == typeof (Chat_ReceiveParishText_ReturnType))
            {
              Chat_ReceiveParishText_ReturnType data210 = (Chat_ReceiveParishText_ReturnType) result.data;
              if (this.chat_ReceiveParishText_UserCallBack != null)
                this.chat_ReceiveParishText_UserCallBack(data210);
            }
            else if (result.classType == typeof (Chat_BackFillParishText_ReturnType))
            {
              Chat_BackFillParishText_ReturnType data211 = (Chat_BackFillParishText_ReturnType) result.data;
              if (this.chat_BackFillParishText_UserCallBack != null)
                this.chat_BackFillParishText_UserCallBack(data211);
            }
            else if (result.classType == typeof (Chat_MarkParishTextRead_ReturnType))
            {
              Chat_MarkParishTextRead_ReturnType data212 = (Chat_MarkParishTextRead_ReturnType) result.data;
              if (this.chat_MarkParishTextRead_UserCallBack != null)
                this.chat_MarkParishTextRead_UserCallBack(data212);
            }
            else if (result.classType == typeof (Chat_Admin_Command_ReturnType))
            {
              Chat_Admin_Command_ReturnType data213 = (Chat_Admin_Command_ReturnType) result.data;
              if (this.chat_Admin_Command_UserCallBack != null)
                this.chat_Admin_Command_UserCallBack(data213);
            }
            else if (result.classType == typeof (GetContestDataRange_ReturnType))
            {
              GetContestDataRange_ReturnType data214 = (GetContestDataRange_ReturnType) result.data;
              if (this.getContestDataRange_UserCallBack != null)
                this.getContestDataRange_UserCallBack(data214);
            }
            else if (result.classType == typeof (GetUserContestData_ReturnType))
            {
              GetUserContestData_ReturnType data215 = (GetUserContestData_ReturnType) result.data;
              if (this.getUserContestData_UserCallBack != null)
                this.getUserContestData_UserCallBack(data215);
            }
            else if (result.classType == typeof (GetContestHistoryIDs_ReturnType))
            {
              GetContestHistoryIDs_ReturnType data216 = (GetContestHistoryIDs_ReturnType) result.data;
              if (this.getContestHistoryIDs_UserCallBack != null)
                this.getContestHistoryIDs_UserCallBack(data216);
            }
            result.data = (Common_ReturnData) null;
          }
        }
      }
      if (flag1)
        this.resultList.Clear();
      this.inResultsProcessing = false;
      if (this.queuedResultList.Count <= 0)
        return;
      bool flag2 = false;
      bool flag3 = true;
      foreach (RemoteServices.CallBackEntryClass queuedResult in this.queuedResultList)
      {
        int count = this.resultList.Count;
        if (flag3)
        {
          for (int index = 0; index < count; ++index)
          {
            if (this.resultList[index].state == 0)
            {
              this.resultList[index] = queuedResult;
              flag2 = true;
              break;
            }
          }
        }
        if (!flag2)
        {
          flag3 = false;
          this.resultList.Add(queuedResult);
        }
        flag2 = false;
      }
      this.queuedResultList.Clear();
    }

    private bool packetTimeOut(double timeTaken, RemoteServices.CallBackEntryClass cbe)
    {
      double num = 180000.0;
      if (cbe.classType == typeof (GetVillageNames_ReturnType) || cbe.classType == typeof (GetVillageFactionChanges_ReturnType) || cbe.classType == typeof (FullTick_ReturnType) || cbe.classType == typeof (GetAllVillageOwnerFactions_ReturnType))
        num = 600000.0;
      return timeTaken > num;
    }

    public bool queueEmpty()
    {
      int count = this.resultList.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this.resultList[index].state != 0)
          return false;
      }
      return this.queuedResultList.Count <= 0;
    }

    public void clearQueues()
    {
      this.resultList.Clear();
      this.queuedResultList.Clear();
    }

    public void initChannel()
    {
      if (this.channel != null)
        return;
      ServicePointManager.Expect100Continue = false;
      ServicePointManager.MaxServicePointIdleTime = 1;
      ListDictionary properties1 = new ListDictionary();
      BinaryClientFormatterSinkProvider clientSinkProvider = new BinaryClientFormatterSinkProvider();
      ListDictionary properties2 = new ListDictionary();
      ListDictionary providerData = new ListDictionary();
      properties2.Add((object) "customSinkType", (object) "CompressedSink.CompressedClientSink, CustomSinks");
      clientSinkProvider.Next = (IClientChannelSinkProvider) new CustomClientSinkProvider((IDictionary) properties2, (ICollection) providerData);
      this.channel = new HttpChannel((IDictionary) properties1, (IClientChannelSinkProvider) clientSinkProvider, (IServerChannelSinkProvider) null);
      ChannelServices.RegisterChannel((IChannel) this.channel, false);
    }

    public void init(string remotePath)
    {
      this.connectionErrored = false;
      this.clearQueues();
      this.initChannel();
      this.service = (IService) Activator.GetObject(typeof (IService), remotePath);
      ChannelServices.GetChannelSinkProperties((object) this.service)[(object) "credentials"] = (object) CredentialCache.DefaultCredentials;
      ServicePointManager.Expect100Continue = false;
      ServicePointManager.MaxServicePointIdleTime = 1;
      this.chatActive = true;
    }

    public void registerRPCcall(IAsyncResult RemAr, Type classType)
    {
      lock (RemoteServices.syncLock)
      {
        if (this.SessionID == 0)
          ++this.RTTAverageTime;
        RemoteServices.CallBackEntryClass callBackEntryClass = new RemoteServices.CallBackEntryClass();
        callBackEntryClass.ar = RemAr;
        callBackEntryClass.classType = classType;
        callBackEntryClass.data = (Common_ReturnData) null;
        callBackEntryClass.timer = DXTimer.GetCurrentMilliseconds();
        if (!this.inResultsProcessing)
        {
          int count = this.resultList.Count;
          bool flag = false;
          for (int index = 0; index < count; ++index)
          {
            if (this.resultList[index].state == 0)
            {
              this.resultList[index] = callBackEntryClass;
              flag = true;
              break;
            }
          }
          if (flag)
            return;
          this.resultList.Add(callBackEntryClass);
        }
        else
          this.queuedResultList.Add((object) callBackEntryClass);
      }
    }

    public void addPacket(Type classType, int time)
    {
      this.rtt_logging.Add(new RemoteServices.RTT_Log_data()
      {
        packetType = classType,
        time = time
      });
    }

    public List<RemoteServices.RTT_Log_data> getDetailedLogging() => this.rtt_logging;

    public void removeRPCresult(IAsyncResult ar)
    {
      foreach (RemoteServices.CallBackEntryClass result in this.resultList)
      {
        if (result.state == 1 && result.ar == ar)
        {
          result.state = 0;
          return;
        }
      }
      foreach (RemoteServices.CallBackEntryClass queuedResult in this.queuedResultList)
      {
        if (queuedResult.state == 1 && queuedResult.ar == ar)
        {
          queuedResult.state = 0;
          break;
        }
      }
    }

    public void storeRPCresult(IAsyncResult ar, Common_ReturnData resultData)
    {
      foreach (RemoteServices.CallBackEntryClass result in this.resultList)
      {
        if (result.state == 1 && result.ar == ar)
        {
          result.data = resultData;
          result.state = 2;
          result.timer = DXTimer.GetCurrentMilliseconds() - result.timer;
          this.lastLatency = (int) result.timer;
          if (this.RTTAverageCount == 0)
          {
            this.RTTAverageCount = 1;
            this.RTTAverageTime = result.timer;
          }
          else
          {
            double num = this.RTTAverageTime * (double) this.RTTAverageCount + result.timer;
            ++this.RTTAverageCount;
            this.RTTAverageTime = num / (double) this.RTTAverageCount;
          }
          this.addPacket(result.classType, (int) result.timer);
          if (result.timer < 1000.0)
          {
            if (this.RTTAverageShortCount == 0)
            {
              this.RTTAverageShortCount = 1;
              this.RTTAverageShortTime = result.timer;
              break;
            }
            double num = this.RTTAverageShortTime * (double) this.RTTAverageShortCount + result.timer;
            ++this.RTTAverageShortCount;
            this.RTTAverageShortTime = num / (double) this.RTTAverageShortCount;
            break;
          }
          if (this.RTTAverageLongCount == 0)
          {
            this.RTTAverageLongCount = 1;
            this.RTTAverageLongTime = result.timer;
            break;
          }
          double num1 = this.RTTAverageLongTime * (double) this.RTTAverageLongCount + result.timer;
          ++this.RTTAverageLongCount;
          this.RTTAverageLongTime = num1 / (double) this.RTTAverageLongCount;
          break;
        }
      }
      foreach (RemoteServices.CallBackEntryClass queuedResult in this.queuedResultList)
      {
        if (queuedResult.state == 1 && queuedResult.ar == ar)
        {
          queuedResult.data = resultData;
          queuedResult.state = 2;
          queuedResult.timer = DXTimer.GetCurrentMilliseconds() - queuedResult.timer;
          if (this.RTTAverageCount == 0)
          {
            this.RTTAverageCount = 1;
            this.RTTAverageTime = queuedResult.timer;
          }
          else
          {
            double num = this.RTTAverageTime * (double) this.RTTAverageCount + queuedResult.timer;
            ++this.RTTAverageCount;
            this.RTTAverageTime = num / (double) this.RTTAverageCount;
          }
          this.addPacket(queuedResult.classType, (int) queuedResult.timer);
          if (queuedResult.timer < 1000.0)
          {
            if (this.RTTAverageShortCount == 0)
            {
              this.RTTAverageShortCount = 1;
              this.RTTAverageShortTime = queuedResult.timer;
              break;
            }
            double num = this.RTTAverageShortTime * (double) this.RTTAverageShortCount + queuedResult.timer;
            ++this.RTTAverageShortCount;
            this.RTTAverageShortTime = num / (double) this.RTTAverageShortCount;
            break;
          }
          if (this.RTTAverageLongCount == 0)
          {
            this.RTTAverageLongCount = 1;
            this.RTTAverageLongTime = queuedResult.timer;
            break;
          }
          double num2 = this.RTTAverageLongTime * (double) this.RTTAverageLongCount + queuedResult.timer;
          ++this.RTTAverageLongTime;
          this.RTTAverageLongTime = num2 / (double) this.RTTAverageLongCount;
          break;
        }
      }
    }

    private void manageRemoteExpection(IAsyncResult ar, Common_ReturnData returnData, Exception e)
    {
      if (e.GetType() != typeof (WebException))
        return;
      GameEngine.Instance.connectionErrorString = e.Message + "\n" + e.ToString();
      returnData.m_errorCode = CommonTypes.ErrorCodes.ErrorCode.CONNECTION_NO_SERVER;
      returnData.SetAsFailed();
      this.storeRPCresult(ar, returnData);
      this.connectionErrored = true;
    }

    private bool isDataNotNull(object data) => data != null;

    private class CallBackEntryClass
    {
      public IAsyncResult ar;
      public Type classType;
      public Common_ReturnData data;
      public int state = 1;
      public double timer;
    }

    public delegate CreateNewUser_ReturnType RemoteAsyncDelegate_CreateNewUser(
      string username,
      string password,
      string realname,
      string emailaddress,
      int versionNo,
      string securityString);

    public delegate void CreateNewUser_UserCallBack(CreateNewUser_ReturnType returnData);

    public delegate LoginUser_ReturnType RemoteAsyncDelegate_LoginUser(
      string username,
      string password,
      int versionNo,
      string verificationString,
      bool needVillageData);

    public delegate void LoginUser_UserCallBack(LoginUser_ReturnType returnData);

    public delegate LoginUserGuid_ReturnType RemoteAsyncDelegate_LoginUserGuid(
      string userName,
      string userGuid,
      string sessionGuid,
      bool needVillageData,
      int versionID);

    public delegate void LoginUserGuid_UserCallBack(LoginUserGuid_ReturnType returnData);

    public delegate ResendVerificationEmail_ReturnType RemoteAsyncDelegate_ResendVerificationEmail(
      string username,
      string password);

    public delegate void ResendVerificationEmail_UserCallBack(
      ResendVerificationEmail_ReturnType returnData);

    public delegate GetAllVillageOwnerFactions_ReturnType RemoteAsyncDelegate_GetAllVillageOwnerFactions(
      int userID,
      int sessionID,
      int sendIndex);

    public delegate void GetAllVillageOwnerFactions_UserCallBack(
      GetAllVillageOwnerFactions_ReturnType returnData);

    public delegate GetVillageFactionChanges_ReturnType RemoteAsyncDelegate_GetVillageFactionChanges(
      int userID,
      int sessionID,
      long startChangePos,
      long factionsChangePos,
      int sendIndex);

    public delegate void GetVillageFactionChanges_UserCallBack(
      GetVillageFactionChanges_ReturnType returnData);

    public delegate GetVillageNames_ReturnType RemoteAsyncDelegate_GetVillageNames(
      int userID,
      int sessionID,
      long currentPos,
      int sendIndex);

    public delegate void GetVillageNames_UserCallBack(GetVillageNames_ReturnType returnData);

    public delegate GetAreaFactionChanges_ReturnType RemoteAsyncDelegate_GetAreaFactionChanges(
      int userID,
      int sessionID,
      long regionStartPos,
      long countyStartPos,
      long provinceStartPos,
      long countryStartPos,
      long parishFlagsPos,
      long countyFlagsPos,
      long provinceFlagsPos,
      long countryFlagsPos);

    public delegate void GetAreaFactionChanges_UserCallBack(
      GetAreaFactionChanges_ReturnType returnData);

    public delegate GetUserVillages_ReturnType RemoteAsyncDelegate_GetUserVillages(
      int userID,
      int sessionID);

    public delegate void GetUserVillages_UserCallBack(GetUserVillages_ReturnType returnData);

    public delegate GetOtherUserVillageIDList_ReturnType RemoteAsyncDelegate_GetOtherUserVillageIDList(
      int userID,
      string userName,
      int sessionID);

    public delegate void GetOtherUserVillageIDList_UserCallBack(
      GetOtherUserVillageIDList_ReturnType returnData);

    public delegate BuyVillage_ReturnType RemoteAsyncDelegate_BuyVillage(
      int userID,
      int sessionID,
      int fromVillageID,
      int villageID,
      int mapType,
      long startChangePos,
      bool peaceTime);

    public delegate void BuyVillage_UserCallBack(BuyVillage_ReturnType returnData);

    public delegate ConvertVillage_ReturnType RemoteAsyncDelegate_ConvertVillage(
      int userID,
      int sessionID,
      int villageID,
      int mapType);

    public delegate void ConvertVillage_UserCallBack(ConvertVillage_ReturnType returnData);

    public delegate FullTick_ReturnType RemoteAsyncDelegate_FullTick(
      int userID,
      int sessionID,
      long startChangePos,
      long regionStartPos,
      long countyStartPos,
      long provinceStartPos,
      long countryStartPos,
      bool registerSession,
      long villageNamePos,
      long factionsChangePos,
      DateTime lastTraderTime,
      DateTime lastPeopleTime,
      long parishFlagsPos,
      long countyFlagsPos,
      long provinceFlagsPos,
      long countryFlagsPos,
      long highestArmyID,
      int mode,
      bool fullMode);

    public delegate void FullTick_UserCallBack(FullTick_ReturnType returnData);

    public delegate LeaderBoard_ReturnType RemoteAsyncDelegate_LeaderBoard(
      int userID,
      int sessionID,
      int mode,
      int minValue,
      int maxValue,
      DateTime lastUpdate);

    public delegate void LeaderBoard_UserCallBack(LeaderBoard_ReturnType returnData);

    public delegate LeaderBoardSearch_ReturnType RemoteAsyncDelegate_LeaderBoardSearch(
      int userID,
      int sessionID,
      int category,
      string searchString,
      DateTime lastUpdate);

    public delegate void LeaderBoardSearch_UserCallBack(LeaderBoardSearch_ReturnType returnData);

    public delegate LogOut_ReturnType RemoteAsyncDelegate_LogOut(
      int userID,
      int sessionID,
      bool manual,
      bool autoScout,
      bool autoTrade,
      bool autoAttack,
      bool autoAttackWolf,
      bool autoAttackBandit,
      bool autoAttackAI,
      int resourceType,
      int percent,
      bool autoRecruit,
      bool autoRecruitPeasant,
      bool autoRecruitArchers,
      bool autoRecruitPikemen,
      bool autoRecruitSwordsmen,
      bool autoRecruitCatapults,
      int autoRecruitPeasant_Cap,
      int autoRecruitArchers_Cap,
      int autoRecruitPikemen_Cap,
      int autoRecruitSwordsmen_Cap,
      int autoRecruitCatapults_Cap);

    public delegate void LogOut_UserCallBack(LogOut_ReturnType returnData);

    public delegate GetLoginHistory_ReturnType RemoteAsyncDelegate_GetLoginHistory(
      int userID,
      int sessionID);

    public delegate void GetLoginHistory_UserCallBack(GetLoginHistory_ReturnType returnData);

    public delegate UserInfo_ReturnType RemoteAsyncDelegate_UserInfo(
      int userID,
      int sessionID,
      int requestUserID);

    public delegate void UserInfo_UserCallBack(UserInfo_ReturnType returnData);

    public delegate GetArmyData_ReturnType RemoteAsyncDelegate_GetArmyData(
      int userID,
      int sessionID,
      long highestSeendID);

    public delegate void GetArmyData_UserCallBack(GetArmyData_ReturnType returnData);

    public delegate ArmyAttack_ReturnType RemoteAsyncDelegate_ArmyAttack(
      int userID,
      int sessionID,
      int armyID,
      int targetVillage,
      int attackType);

    public delegate void ArmyAttack_UserCallBack(ArmyAttack_ReturnType returnData);

    public delegate RetrieveAttackResult_ReturnType RemoteAsyncDelegate_RetrieveAttackResult(
      int userID,
      int sessionID,
      long armyID,
      long startChangePos);

    public delegate void RetrieveAttackResult_UserCallBack(
      RetrieveAttackResult_ReturnType returnData);

    public delegate SetAdminMessage_ReturnType RemoteAsyncDelegate_SetAdminMessage(
      int userID,
      int sessionID,
      string message,
      int type);

    public delegate void SetAdminMessage_UserCallBack(SetAdminMessage_ReturnType returnData);

    public delegate CompleteVillageCastle_ReturnType RemoteAsyncDelegate_CompleteVillageCastle(
      int userID,
      int sessionID,
      int villageID,
      int mode);

    public delegate void CompleteVillageCastle_UserCallBack(
      CompleteVillageCastle_ReturnType returnData);

    public delegate RetrieveStats_ReturnType RemoteAsyncDelegate_RetrieveStats(
      int userID,
      int sessionID);

    public delegate void RetrieveStats_UserCallBack(RetrieveStats_ReturnType returnData);

    public delegate RetrieveStats2_ReturnType RemoteAsyncDelegate_RetrieveStats2(
      int userID,
      int sessionID);

    public delegate void RetrieveStats2_UserCallBack(RetrieveStats2_ReturnType returnData);

    public delegate GetAdminStats_ReturnType RemoteAsyncDelegate_GetAdminStats(
      int userID,
      int sessionID);

    public delegate void GetAdminStats_UserCallBack(GetAdminStats_ReturnType returnData);

    public delegate GetReportsList_ReturnType RemoteAsyncDelegate_GetReportsList(
      int userID,
      int sessionID,
      int readFilter,
      int[] typeFilters,
      long folderID,
      long clientHighest);

    public delegate void GetReportsList_UserCallBack(GetReportsList_ReturnType returnData);

    public delegate GetReport_ReturnType RemoteAsyncDelegate_GetReport(
      int userID,
      int sessionID,
      long reportID);

    public delegate void GetReport_UserCallBack(GetReport_ReturnType returnData);

    public delegate ForwardReport_ReturnType RemoteAsyncDelegate_ForwardReport(
      int userID,
      int sessionID,
      long reportID,
      string[] recipients);

    public delegate void ForwardReport_UserCallBack(ForwardReport_ReturnType returnData);

    public delegate ViewBattle_ReturnType RemoteAsyncDelegate_ViewBattle(
      int userID,
      int sessionID,
      long reportID);

    public delegate void ViewBattle_UserCallBack(ViewBattle_ReturnType returnData);

    public delegate ViewCastle_ReturnType RemoteAsyncDelegate_ViewCastle(
      int userID,
      int sessionID,
      int villageID,
      long reportID);

    public delegate void ViewCastle_UserCallBack(ViewCastle_ReturnType returnData);

    public delegate DeleteReports_ReturnType RemoteAsyncDelegate_DeleteReports(
      int userID,
      int sessionID,
      int mode,
      long[] reportsToDelete,
      long folderID);

    public delegate void DeleteReports_UserCallBack(DeleteReports_ReturnType returnData);

    public delegate UpdateReportFilters_ReturnType RemoteAsyncDelegate_UpdateReportFilters(
      int userID,
      int sessionID,
      ReportFilterList filters);

    public delegate void UpdateReportFilters_UserCallBack(UpdateReportFilters_ReturnType returnData);

    public delegate UpdateUserOptions_ReturnType RemoteAsyncDelegate_UpdateUserOptions(
      int userID,
      int sessionID,
      GameOptionsData options);

    public delegate void UpdateUserOptions_UserCallBack(UpdateUserOptions_ReturnType returnData);

    public delegate ManageReportFolders_ReturnType RemoteAsyncDelegate_ManageReportFolders(
      int userID,
      int sessionID,
      int mode,
      long folderID,
      string groupNames);

    public delegate void ManageReportFolders_UserCallBack(ManageReportFolders_ReturnType returnData);

    public delegate GetMailThreadList_ReturnType RemoteAsyncDelegate_GetMailThreadList(
      int userID,
      int sessionID,
      bool initialRequest,
      int retrieveMode,
      DateTime lastRetrieved);

    public delegate void GetMailThreadList_UserCallBack(GetMailThreadList_ReturnType returnData);

    public delegate GetMailThread_ReturnType RemoteAsyncDelegate_GetMailThread(
      int userID,
      int sessionID,
      long threadID,
      int localCount,
      long highestSegmentID);

    public delegate void GetMailThread_UserCallBack(GetMailThread_ReturnType returnData);

    public delegate GetMailFolders_ReturnType RemoteAsyncDelegate_GetMailFolders(
      int userID,
      int sessionID);

    public delegate void GetMailFolders_UserCallBack(GetMailFolders_ReturnType returnData);

    public delegate CreateMailFolder_ReturnType RemoteAsyncDelegate_CreateMailFolder(
      int userID,
      int sessionID,
      string folderName);

    public delegate void CreateMailFolder_UserCallBack(CreateMailFolder_ReturnType returnData);

    public delegate MoveToMailFolder_ReturnType RemoteAsyncDelegate_MoveToMailFolder(
      int userID,
      int sessionID,
      long threadID,
      long folderID);

    public delegate void MoveToMailFolder_UserCallBack(MoveToMailFolder_ReturnType returnData);

    public delegate RemoveMailFolder_ReturnType RemoteAsyncDelegate_RemoveMailFolder(
      int userID,
      int sessionID,
      long folderID);

    public delegate void RemoveMailFolder_UserCallBack(RemoveMailFolder_ReturnType returnData);

    public delegate ReportMail_ReturnType RemoteAsyncDelegate_ReportMail(
      int userID,
      int sessionID,
      long mailID,
      long threadID,
      string reason,
      string summary);

    public delegate void ReportMail_UserCallBack(ReportMail_ReturnType returnData);

    public delegate FlagMailRead_ReturnType RemoteAsyncDelegate_FlagMailRead(
      int userID,
      int sessionID,
      long mailID,
      long threadID,
      bool asRead);

    public delegate void FlagMailRead_UserCallBack(FlagMailRead_ReturnType returnData);

    public delegate SendMail_ReturnType RemoteAsyncDelegate_SendMail(
      int userID,
      int sessionID,
      string subject,
      string body,
      string[] recipients,
      long threadID,
      bool forwardThread);

    public delegate void SendMail_UserCallBack(SendMail_ReturnType returnData);

    public delegate SendSpecialMail_ReturnType RemoteAsyncDelegate_SendSpecialMail(
      int userID,
      int sessionID,
      int mailType,
      int area,
      string subject,
      string body);

    public delegate void SendSpecialMail_UserCallBack(SendSpecialMail_ReturnType returnData);

    public delegate DeleteMailThread_ReturnType RemoteAsyncDelegate_DeleteMailThread(
      int userID,
      int sessionID,
      long threadID);

    public delegate void DeleteMailThread_UserCallBack(DeleteMailThread_ReturnType returnData);

    public delegate GetMailRecipientsHistory_ReturnType RemoteAsyncDelegate_GetMailRecipientsHistory(
      int userID,
      int sessionID);

    public delegate void GetMailRecipientsHistory_UserCallBack(
      GetMailRecipientsHistory_ReturnType returnData);

    public delegate GetMailUserSearch_ReturnType RemoteAsyncDelegate_GetMailUserSearch(
      int userID,
      int sessionID,
      string filter);

    public delegate void GetMailUserSearch_UserCallBack(GetMailUserSearch_ReturnType returnData);

    public delegate AddUserToFavourites_ReturnType RemoteAsyncDelegate_AddUserToFavourites(
      int userID,
      int sessionID,
      string userName,
      bool doRemove);

    public delegate void AddUserToFavourites_UserCallBack(AddUserToFavourites_ReturnType returnData);

    public delegate GetHistoricalData_ReturnType RemoteAsyncDelegate_GetHistoricalData(
      int userID,
      int sessionID);

    public delegate void GetHistoricalData_UserCallBack(GetHistoricalData_ReturnType returnData);

    public delegate GetResourceLevel_ReturnType RemoteAsyncDelegate_GetResourceLevel(
      int userID,
      int sessionID,
      int villageID,
      int buildingType);

    public delegate void GetResourceLevel_UserCallBack(GetResourceLevel_ReturnType returnData);

    public delegate GetVillageBuildingsList_ReturnType RemoteAsyncDelegate_GetVillageBuildingsList(
      int userID,
      int sessionID,
      int villageID,
      bool fullUpdate,
      bool viewOnly,
      bool needParishPeople);

    public delegate void GetVillageBuildingsList_UserCallBack(
      GetVillageBuildingsList_ReturnType returnData);

    public delegate PlaceVillageBuilding_ReturnType RemoteAsyncDelegate_PlaceVillageBuilding(
      int userID,
      int sessionID,
      int villageID,
      int buildingType,
      Point buildingLocation);

    public delegate void PlaceVillageBuilding_UserCallBack(
      PlaceVillageBuilding_ReturnType returnData);

    public delegate DeleteVillageBuilding_ReturnType RemoteAsyncDelegate_DeleteVillageBuilding(
      int userID,
      int sessionID,
      int villageID,
      long buildingID);

    public delegate void DeleteVillageBuilding_UserCallBack(
      DeleteVillageBuilding_ReturnType returnData);

    public delegate CancelDeleteVillageBuilding_ReturnType RemoteAsyncDelegate_CancelDeleteVillageBuilding(
      int userID,
      int sessionID,
      int villageID,
      long buildingID);

    public delegate void CancelDeleteVillageBuilding_UserCallBack(
      CancelDeleteVillageBuilding_ReturnType returnData);

    public delegate MoveVillageBuilding_ReturnType RemoteAsyncDelegate_MoveVillageBuilding(
      int userID,
      int sessionID,
      int villageID,
      long buildingID,
      Point buildingLocation);

    public delegate void MoveVillageBuilding_UserCallBack(MoveVillageBuilding_ReturnType returnData);

    public delegate VillageBuildingCompleteDataRetrieval_ReturnType RemoteAsyncDelegate_VillageBuildingCompleteDataRetrieval(
      int userID,
      int sessionID,
      int villageID,
      long buildingID,
      int mode);

    public delegate void VillageBuildingCompleteDataRetrieval_UserCallBack(
      VillageBuildingCompleteDataRetrieval_ReturnType returnData);

    public delegate VillageBuildingSetActive_ReturnType RemoteAsyncDelegate_VillageBuildingSetActive(
      int userID,
      int sessionID,
      long buildingID,
      int villageID,
      int buildingType,
      bool state);

    public delegate void VillageBuildingSetActive_UserCallBack(
      VillageBuildingSetActive_ReturnType returnData);

    public delegate VillageBuildingChangeRates_ReturnType RemoteAsyncDelegate_VillageBuildingChangeRates(
      int userID,
      int sessionID,
      int villageID,
      int taxLevel,
      int rationsLevel,
      int aleRationsLevel,
      int capitalTaxRate);

    public delegate void VillageBuildingChangeRates_UserCallBack(
      VillageBuildingChangeRates_ReturnType returnData);

    public delegate VillageRename_ReturnType RemoteAsyncDelegate_VillageRename(
      int userID,
      int sessionID,
      int villageID,
      string villageName,
      bool abandon,
      bool modReset);

    public delegate void VillageRename_UserCallBack(VillageRename_ReturnType returnData);

    public delegate VillageProduceWeapons_ReturnType RemoteAsyncDelegate_VillageProduceWeapons(
      int userID,
      int sessionID,
      int villageID,
      int weaponType,
      int amount);

    public delegate void VillageProduceWeapons_UserCallBack(
      VillageProduceWeapons_ReturnType returnData);

    public delegate VillageHoldBanquet_ReturnType RemoteAsyncDelegate_VillageHoldBanquet(
      int userID,
      int sessionID,
      int villageID,
      int venison,
      int wine,
      int salt,
      int spice,
      int silk,
      int clothing,
      int furniture,
      int metalwork);

    public delegate void VillageHoldBanquet_UserCallBack(VillageHoldBanquet_ReturnType returnData);

    public delegate GetCastle_ReturnType RemoteAsyncDelegate_GetCastle(
      int userID,
      int sessionID,
      int villageID);

    public delegate void GetCastle_UserCallBack(GetCastle_ReturnType returnData);

    public delegate AddCastleElement_ReturnType RemoteAsyncDelegate_AddCastleElement(
      int userID,
      int sessionID,
      int villageID,
      int elementType,
      int xPos,
      int yPos,
      long clientElementNumber,
      int wallEndX,
      int wallEndY,
      bool reinforcement,
      bool vassalReinforcement,
      byte[,] elementList,
      long[] troopsToDelete,
      MoveElementData[] troopsToMove);

    public delegate void AddCastleElement_UserCallBack(AddCastleElement_ReturnType returnData);

    public delegate DeleteCastleElement_ReturnType RemoteAsyncDelegate_DeleteCastleElement(
      int userID,
      int sessionID,
      int villageID,
      long elementNumber,
      List<long> elementList);

    public delegate void DeleteCastleElement_UserCallBack(DeleteCastleElement_ReturnType returnData);

    public delegate ChangeCastleElementAggressiveDefender_ReturnType RemoteAsyncDelegate_ChangeCastleElementAggressiveDefender(
      int userID,
      int sessionID,
      int villageID,
      long[] elementID,
      bool state);

    public delegate void ChangeCastleElementAggressiveDefender_UserCallBack(
      ChangeCastleElementAggressiveDefender_ReturnType returnData);

    public delegate AutoRepairCastle_ReturnType RemoteAsyncDelegate_AutoRepairCastle(
      int userID,
      int sessionID,
      int villageID);

    public delegate void AutoRepairCastle_UserCallBack(AutoRepairCastle_ReturnType returnData);

    public delegate MemorizeCastleTroops_ReturnType RemoteAsyncDelegate_MemorizeCastleTroops(
      int userID,
      int sessionID,
      int villageID);

    public delegate void MemorizeCastleTroops_UserCallBack(
      MemorizeCastleTroops_ReturnType returnData);

    public delegate RestoreCastleTroops_ReturnType RemoteAsyncDelegate_RestoreCastleTroops(
      int userID,
      int sessionID,
      int villageID);

    public delegate void RestoreCastleTroops_UserCallBack(RestoreCastleTroops_ReturnType returnData);

    public delegate CheatAddTroops_ReturnType RemoteAsyncDelegate_CheatAddTroops(
      int userID,
      int sessionID,
      int villageID,
      int troopType,
      int numToMake);

    public delegate void CheatAddTroops_UserCallBack(CheatAddTroops_ReturnType returnData);

    public delegate LaunchCastleAttack_ReturnType RemoteAsyncDelegate_LaunchCastleAttack(
      int userID,
      int sessionID,
      int parentOfAttackingVillageID,
      int targetVillageID,
      int sourceVillageID,
      byte[] attackersMap,
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults,
      int attackType,
      int pillagePercent,
      int CaptainsCommand,
      int numCaptains);

    public delegate void LaunchCastleAttack_UserCallBack(LaunchCastleAttack_ReturnType returnData);

    public delegate SendScouts_ReturnType RemoteAsyncDelegate_SendScouts(
      int userID,
      int sessionID,
      int targetVillageID,
      int sourceVillageID,
      int numScouts);

    public delegate void SendScouts_UserCallBack(SendScouts_ReturnType returnData);

    public delegate CancelCastleAttack_ReturnType RemoteAsyncDelegate_CancelCastleAttack(
      int userID,
      int sessionID,
      long armyID);

    public delegate void CancelCastleAttack_UserCallBack(CancelCastleAttack_ReturnType returnData);

    public delegate SendReinforcements_ReturnType RemoteAsyncDelegate_SendReinforcements(
      int userID,
      int sessionID,
      int homeVillageID,
      int supportedVillageID,
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults);

    public delegate void SendReinforcements_UserCallBack(SendReinforcements_ReturnType returnData);

    public delegate ReturnReinforcements_ReturnType RemoteAsyncDelegate_ReturnReinforcements(
      int userID,
      int sessionID,
      long reinforcementID,
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults);

    public delegate void ReturnReinforcements_UserCallBack(
      ReturnReinforcements_ReturnType returnData);

    public delegate SendMarketResources_ReturnType RemoteAsyncDelegate_SendMarketResources(
      int userID,
      int sessionID,
      int homeVillageID,
      int targetVillage,
      int resource,
      int amount);

    public delegate void SendMarketResources_UserCallBack(SendMarketResources_ReturnType returnData);

    public delegate GetUserTraders_ReturnType RemoteAsyncDelegate_GetUserTraders(
      int userID,
      int sessionID,
      int villageID);

    public delegate void GetUserTraders_UserCallBack(GetUserTraders_ReturnType returnData);

    public delegate GetActiveTraders_ReturnType RemoteAsyncDelegate_GetActiveTraders(
      int userID,
      int sessionID,
      DateTime lastTime);

    public delegate void GetActiveTraders_UserCallBack(GetActiveTraders_ReturnType returnData);

    public delegate GetStockExchangeData_ReturnType RemoteAsyncDelegate_GetStockExchangeData(
      int userID,
      int sessionID,
      int villageID,
      bool stockExchange,
      int[] closeVillages);

    public delegate void GetStockExchangeData_UserCallBack(
      GetStockExchangeData_ReturnType returnData);

    public delegate StockExchangeTrade_ReturnType RemoteAsyncDelegate_StockExchangeTrade(
      int userID,
      int sessionID,
      int villageID,
      int targetExchange,
      int resource,
      int amount,
      bool buy);

    public delegate void StockExchangeTrade_UserCallBack(StockExchangeTrade_ReturnType returnData);

    public delegate UpdateVillageFavourites_ReturnType RemoteAsyncDelegate_UpdateVillageFavourites(
      int userID,
      int sessionID,
      int mode,
      int villageID);

    public delegate void UpdateVillageFavourites_UserCallBack(
      UpdateVillageFavourites_ReturnType returnData);

    public delegate MakeTroop_ReturnType RemoteAsyncDelegate_MakeTroop(
      int userID,
      int sessionID,
      int villageID,
      int troopType,
      int amount);

    public delegate void MakeTroop_UserCallBack(MakeTroop_ReturnType returnData);

    public delegate UpgradeRank_ReturnType RemoteAsyncDelegate_UpgradeRank(
      int userID,
      int sessionID,
      int curRank,
      int curRankSubLevel);

    public delegate void UpgradeRank_UserCallBack(UpgradeRank_ReturnType returnData);

    public delegate PreAttackSetup_ReturnType RemoteAsyncDelegate_PreAttackSetup(
      int userID,
      int sessionID,
      int parentAttackingVillage,
      int attackingVillage,
      int targetVillage,
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults,
      int attackType,
      int pillagePercent,
      int captainsCommand);

    public delegate void PreAttackSetup_UserCallBack(PreAttackSetup_ReturnType returnData);

    public delegate GetBattleHonourRating_ReturnType RemoteAsyncDelegate_GetBattleHonourRating(
      int userID,
      int sessionID,
      int attackedVillage);

    public delegate void GetBattleHonourRating_UserCallBack(
      GetBattleHonourRating_ReturnType returnData);

    public delegate RetrieveArmyFromGarrison_ReturnType RemoteAsyncDelegate_RetrieveArmyFromGarrison(
      int userID,
      int sessionID,
      int villageID);

    public delegate void RetrieveArmyFromGarrison_UserCallBack(
      RetrieveArmyFromGarrison_ReturnType returnData);

    public delegate RetrieveVillageUserInfo_ReturnType RemoteAsyncDelegate_RetrieveVillageUserInfo(
      int userID,
      int sessionID,
      int villageID,
      int targetUserID,
      bool extended);

    public delegate void RetrieveVillageUserInfo_UserCallBack(
      RetrieveVillageUserInfo_ReturnType returnData);

    public delegate SpecialVillageInfo_ReturnType RemoteAsyncDelegate_SpecialVillageInfo(
      int userID,
      int sessionID,
      int villageID);

    public delegate void SpecialVillageInfo_UserCallBack(SpecialVillageInfo_ReturnType returnData);

    public delegate GetVillageRankTaxTree_ReturnType RemoteAsyncDelegate_GetVillageRankTaxTree(
      int userID,
      int sessionID);

    public delegate void GetVillageRankTaxTree_UserCallBack(
      GetVillageRankTaxTree_ReturnType returnData);

    public delegate GetResearchData_ReturnType RemoteAsyncDelegate_GetResearchData(
      int userID,
      int sessionID);

    public delegate void GetResearchData_UserCallBack(GetResearchData_ReturnType returnData);

    public delegate DoResearch_ReturnType RemoteAsyncDelegate_DoResearch(
      int userID,
      int sessionID,
      int researchType,
      int queuePos);

    public delegate void DoResearch_UserCallBack(DoResearch_ReturnType returnData);

    public delegate BuyResearchPoint_ReturnType RemoteAsyncDelegate_BuyResearchPoint(
      int userID,
      int sessionID);

    public delegate void BuyResearchPoint_UserCallBack(BuyResearchPoint_ReturnType returnData);

    public delegate VassalInfo_ReturnType RemoteAsyncDelegate_VassalInfo(
      int userID,
      int sessionID,
      int villageID);

    public delegate void VassalInfo_UserCallBack(VassalInfo_ReturnType returnData);

    public delegate VassalSendResources_ReturnType RemoteAsyncDelegate_VassalSendResources(
      int userID,
      int sessionID,
      int liegeLordVillageID,
      int vassalVillageID,
      int resourceType,
      int amount);

    public delegate void VassalSendResources_UserCallBack(VassalSendResources_ReturnType returnData);

    public delegate UpdateSelectedTitheType_ReturnType RemoteAsyncDelegate_UpdateSelectedTitheType(
      int userID,
      int sessionID,
      int villageID,
      int titheType);

    public delegate void UpdateSelectedTitheType_UserCallBack(
      UpdateSelectedTitheType_ReturnType returnData);

    public delegate BreakVassalage_ReturnType RemoteAsyncDelegate_BreakVassalage(
      int userID,
      int sessionID,
      int villageID,
      int targetVillage);

    public delegate void BreakVassalage_UserCallBack(BreakVassalage_ReturnType returnData);

    public delegate BreakLiegeLord_ReturnType RemoteAsyncDelegate_BreakLiegeLord(
      int userID,
      int sessionID,
      int villageID,
      int targetVillage);

    public delegate void BreakLiegeLord_UserCallBack(BreakLiegeLord_ReturnType returnData);

    public delegate GetPreVassalInfo_ReturnType RemoteAsyncDelegate_GetPreVassalInfo(
      int userID,
      int sessionID,
      int yourVillageID,
      int targetVillageID);

    public delegate void GetPreVassalInfo_UserCallBack(GetPreVassalInfo_ReturnType returnData);

    public delegate SendVassalRequest_ReturnType RemoteAsyncDelegate_SendVassalRequest(
      int userID,
      int sessionID,
      int yourVillageID,
      int targetVillageID);

    public delegate void SendVassalRequest_UserCallBack(SendVassalRequest_ReturnType returnData);

    public delegate HandleVassalRequest_ReturnType RemoteAsyncDelegate_HandleVassalRequest(
      int userID,
      int sessionID,
      int command,
      int liegeLordVillageID,
      int vassalVillageID);

    public delegate void HandleVassalRequest_UserCallBack(HandleVassalRequest_ReturnType returnData);

    public delegate GetVassalArmyInfo_ReturnType RemoteAsyncDelegate_GetVassalArmyInfo(
      int userID,
      int sessionID,
      int vassalVillageID,
      int mode,
      int attackedVillage);

    public delegate void GetVassalArmyInfo_UserCallBack(GetVassalArmyInfo_ReturnType returnData);

    public delegate SendTroopsToVassal_ReturnType RemoteAsyncDelegate_SendTroopsToVassal(
      int userID,
      int sessionID,
      int liegeLordVillageID,
      int vassalVillageID,
      int peasants,
      int archers,
      int pikemen,
      int swordsmen,
      int catapults);

    public delegate void SendTroopsToVassal_UserCallBack(SendTroopsToVassal_ReturnType returnData);

    public delegate RetrieveTroopsFromVassal_ReturnType RemoteAsyncDelegate_RetrieveTroopsFromVassal(
      int userID,
      int sessionID,
      int liegeLordVillageID,
      int vassalVillageID);

    public delegate void RetrieveTroopsFromVassal_UserCallBack(
      RetrieveTroopsFromVassal_ReturnType returnData);

    public delegate UpdateVillageResourcesInfo_ReturnType RemoteAsyncDelegate_UpdateVillageResourcesInfo(
      int userID,
      int sessionID,
      int villageID);

    public delegate void UpdateVillageResourcesInfo_UserCallBack(
      UpdateVillageResourcesInfo_ReturnType returnData);

    public delegate SetHighestArmySeen_ReturnType RemoteAsyncDelegate_SetHighestArmySeen(
      int userID,
      int sessionID,
      long highestArmyIDSeen);

    public delegate void SetHighestArmySeen_UserCallBack(SetHighestArmySeen_ReturnType returnData);

    public delegate GetForumList_ReturnType RemoteAsyncDelegate_GetForumList(
      int userID,
      int sessionID,
      int areaID,
      int areaType);

    public delegate void GetForumList_UserCallBack(GetForumList_ReturnType returnData);

    public delegate GetForumThreadList_ReturnType RemoteAsyncDelegate_GetForumThreadList(
      int userID,
      int sessionID,
      long forumID,
      DateTime lastGet,
      bool forceGet);

    public delegate void GetForumThreadList_UserCallBack(GetForumThreadList_ReturnType returnData);

    public delegate GetForumThread_ReturnType RemoteAsyncDelegate_GetForumThread(
      int userID,
      int sessionID,
      long forumID,
      long threadID,
      DateTime lastGet,
      bool forceGet);

    public delegate void GetForumThread_UserCallBack(GetForumThread_ReturnType returnData);

    public delegate NewForumThread_ReturnType RemoteAsyncDelegate_NewForumThread(
      int userID,
      int sessionID,
      long forumID,
      string headingText,
      string bodyText);

    public delegate void NewForumThread_UserCallBack(NewForumThread_ReturnType returnData);

    public delegate PostToForumThread_ReturnType RemoteAsyncDelegate_PostToForumThread(
      int userID,
      int sessionID,
      long threadID,
      long forumID,
      string text);

    public delegate void PostToForumThread_UserCallBack(PostToForumThread_ReturnType returnData);

    public delegate GiveForumAccess_ReturnType RemoteAsyncDelegate_GiveForumAccess(
      int userID,
      int sessionID,
      long forumID,
      int[] users);

    public delegate void GiveForumAccess_UserCallBack(GiveForumAccess_ReturnType returnData);

    public delegate CreateForum_ReturnType RemoteAsyncDelegate_CreateForum(
      int userID,
      int sessionID,
      int areaID,
      int areaType,
      string name);

    public delegate void CreateForum_UserCallBack(CreateForum_ReturnType returnData);

    public delegate DeleteForum_ReturnType RemoteAsyncDelegate_DeleteForum(
      int userID,
      int sessionID,
      int areaID,
      int areaType,
      long forumID);

    public delegate void DeleteForum_UserCallBack(DeleteForum_ReturnType returnData);

    public delegate DeleteForumThread_ReturnType RemoteAsyncDelegate_DeleteForumThread(
      int userID,
      int sessionID,
      int areaID,
      int areaType,
      string forumTitle,
      long forumID,
      long forumThreadID);

    public delegate void DeleteForumThread_UserCallBack(DeleteForumThread_ReturnType returnData);

    public delegate DeleteForumPost_ReturnType RemoteAsyncDelegate_DeleteForumPost(
      int userID,
      int sessionID,
      int areaID,
      int areaType,
      string forumTitle,
      long forumID,
      long forumThreadID,
      long forumPostID);

    public delegate void DeleteForumPost_UserCallBack(DeleteForumPost_ReturnType returnData);

    public delegate GetCurrentElectionInfo_ReturnType RemoteAsyncDelegate_GetCurrentElectionInfo(
      int userID,
      int sessionID,
      int areaID,
      int areaType);

    public delegate void GetCurrentElectionInfo_UserCallBack(
      GetCurrentElectionInfo_ReturnType returnData);

    public delegate StandInElection_ReturnType RemoteAsyncDelegate_StandInElection(
      int userID,
      int sessionID,
      int areaID,
      int areaType,
      bool state);

    public delegate void StandInElection_UserCallBack(StandInElection_ReturnType returnData);

    public delegate VoteInElection_ReturnType RemoteAsyncDelegate_VoteInElection(
      int userID,
      int sessionID,
      int areaID,
      int areaType,
      int candidate);

    public delegate void VoteInElection_UserCallBack(VoteInElection_ReturnType returnData);

    public delegate UploadAvatar_ReturnType RemoteAsyncDelegate_UploadAvatar(
      int userID,
      int sessionID,
      AvatarData avatarData);

    public delegate void UploadAvatar_UserCallBack(UploadAvatar_ReturnType returnData);

    public delegate MakePeople_ReturnType RemoteAsyncDelegate_MakePeople(
      int userID,
      int sessionID,
      int villageIDF,
      int personType);

    public delegate void MakePeople_UserCallBack(MakePeople_ReturnType returnData);

    public delegate GetUserPeople_ReturnType RemoteAsyncDelegate_GetUserPeople(
      int userID,
      int sessionID);

    public delegate void GetUserPeople_UserCallBack(GetUserPeople_ReturnType returnData);

    public delegate GetUserIDFromName_ReturnType RemoteAsyncDelegate_GetUserIDFromName(
      int userID,
      int sessionID,
      string targetUser);

    public delegate void GetUserIDFromName_UserCallBack(GetUserIDFromName_ReturnType returnData);

    public delegate GetActivePeople_ReturnType RemoteAsyncDelegate_GetActivePeople(
      int userID,
      int sessionID,
      DateTime lastTime);

    public delegate void GetActivePeople_UserCallBack(GetActivePeople_ReturnType returnData);

    public delegate SendPeople_ReturnType RemoteAsyncDelegate_SendPeople(
      int userID,
      int sessionID,
      int homeVillageID,
      int targetVillage,
      int personType,
      int number,
      int command,
      int data);

    public delegate void SendPeople_UserCallBack(SendPeople_ReturnType returnData);

    public delegate RetrievePeople_ReturnType RemoteAsyncDelegate_RetrievePeople(
      int userID,
      int sessionID,
      int villageID,
      List<long> people,
      int personType);

    public delegate void RetrievePeople_UserCallBack(RetrievePeople_ReturnType returnData);

    public delegate SpyCommand_ReturnType RemoteAsyncDelegate_SpyCommand(
      int userID,
      int sessionID,
      int villageID,
      int command);

    public delegate void SpyCommand_UserCallBack(SpyCommand_ReturnType returnData);

    public delegate SpyGetVillageResourceInfo_ReturnType RemoteAsyncDelegate_SpyGetVillageResourceInfo(
      int userID,
      int sessionID,
      int villageID);

    public delegate void SpyGetVillageResourceInfo_UserCallBack(
      SpyGetVillageResourceInfo_ReturnType returnData);

    public delegate SpyGetArmyInfo_ReturnType RemoteAsyncDelegate_SpyGetArmyInfo(
      int userID,
      int sessionID,
      int villageID);

    public delegate void SpyGetArmyInfo_UserCallBack(SpyGetArmyInfo_ReturnType returnData);

    public delegate SpyGetResearchInfo_ReturnType RemoteAsyncDelegate_SpyGetResearchInfo(
      int userID,
      int sessionID,
      int villageID);

    public delegate void SpyGetResearchInfo_UserCallBack(SpyGetResearchInfo_ReturnType returnData);

    public delegate CreateFaction_ReturnType RemoteAsyncDelegate_CreateFaction(
      int userID,
      int sessionID,
      string factionName,
      string factionNameabrv,
      string factionMotto,
      int flagdata);

    public delegate void CreateFaction_UserCallBack(CreateFaction_ReturnType returnData);

    public delegate DisbandFaction_ReturnType RemoteAsyncDelegate_DisbandFaction(
      int userID,
      int sessionID,
      int factionID);

    public delegate void DisbandFaction_UserCallBack(DisbandFaction_ReturnType returnData);

    public delegate FactionSendInvite_ReturnType RemoteAsyncDelegate_FactionSendInvite(
      int userID,
      int sessionID,
      string targetUser);

    public delegate void FactionSendInvite_UserCallBack(FactionSendInvite_ReturnType returnData);

    public delegate FactionWithdrawInvite_ReturnType RemoteAsyncDelegate_FactionWithdrawInvite(
      int userID,
      int sessionID,
      int targetUserID);

    public delegate void FactionWithdrawInvite_UserCallBack(
      FactionWithdrawInvite_ReturnType returnData);

    public delegate FactionReplyToInvite_ReturnType RemoteAsyncDelegate_FactionReplyToInvite(
      int userID,
      int sessionID,
      int factionID,
      bool accept);

    public delegate void FactionReplyToInvite_UserCallBack(
      FactionReplyToInvite_ReturnType returnData);

    public delegate FactionChangeMemberStatus_ReturnType RemoteAsyncDelegate_FactionChangeMemberStatus(
      int userID,
      int sessionID,
      int memberUserID,
      int targetRank);

    public delegate void FactionChangeMemberStatus_UserCallBack(
      FactionChangeMemberStatus_ReturnType returnData);

    public delegate FactionLeave_ReturnType RemoteAsyncDelegate_FactionLeave(
      int userID,
      int sessionID);

    public delegate void FactionLeave_UserCallBack(FactionLeave_ReturnType returnData);

    public delegate GetFactionData_ReturnType RemoteAsyncDelegate_GetFactionData(
      int userID,
      int sessionID,
      int factionID,
      long factionChangesPos);

    public delegate void GetFactionData_UserCallBack(GetFactionData_ReturnType returnData);

    public delegate CreateUserRelationship_ReturnType RemoteAsyncDelegate_CreateUserRelationship(
      int userID,
      int sessionID,
      int targetUserID,
      int relationship);

    public delegate void CreateUserRelationship_UserCallBack(
      CreateUserRelationship_ReturnType returnData);

    public delegate SetUserMarker_ReturnType RemoteAsyncDelegate_SetUserMarker(
      int userID,
      int sessionID,
      int targetUserID,
      int markerType);

    public delegate void SetUserMarker_UserCallBack(SetUserMarker_ReturnType returnData);

    public delegate CreateFactionRelationship_ReturnType RemoteAsyncDelegate_CreateFactionRelationship(
      int userID,
      int sessionID,
      int targetFactionID,
      int relationship);

    public delegate void CreateFactionRelationship_UserCallBack(
      CreateFactionRelationship_ReturnType returnData);

    public delegate CreateHouseRelationship_ReturnType RemoteAsyncDelegate_CreateHouseRelationship(
      int userID,
      int sessionID,
      int targetHouseID,
      int relationship);

    public delegate void CreateHouseRelationship_UserCallBack(
      CreateHouseRelationship_ReturnType returnData);

    public delegate GetHouseGloryPoints_ReturnType RemoteAsyncDelegate_GetHouseGloryPoints(
      int userID,
      int sessionID);

    public delegate void GetHouseGloryPoints_UserCallBack(GetHouseGloryPoints_ReturnType returnData);

    public delegate ChangeFactionMotto_ReturnType RemoteAsyncDelegate_ChangeFactionMotto(
      int userID,
      int sessionID,
      string factionName,
      string factionNameAbrv,
      string motto,
      int flagData);

    public delegate void ChangeFactionMotto_UserCallBack(ChangeFactionMotto_ReturnType returnData);

    public delegate FactionLeadershipVote_ReturnType RemoteAsyncDelegate_FactionLeadershipVote(
      int userID,
      int sessionID,
      int factionID,
      int votedID);

    public delegate void FactionLeadershipVote_UserCallBack(
      FactionLeadershipVote_ReturnType returnData);

    public delegate GetViewFactionData_ReturnType RemoteAsyncDelegate_GetViewFactionData(
      int userID,
      int sessionID,
      int factionID);

    public delegate void GetViewFactionData_UserCallBack(GetViewFactionData_ReturnType returnData);

    public delegate GetViewHouseData_ReturnType RemoteAsyncDelegate_GetViewHouseData(
      int userID,
      int sessionID,
      int houseID);

    public delegate void GetViewHouseData_UserCallBack(GetViewHouseData_ReturnType returnData);

    public delegate SelfJoinHouse_ReturnType RemoteAsyncDelegate_SelfJoinHouse(
      int userID,
      int sessionID,
      int factionID,
      int houseID,
      long factionsChangePos);

    public delegate void SelfJoinHouse_UserCallBack(SelfJoinHouse_ReturnType returnData);

    public delegate HouseVote_ReturnType RemoteAsyncDelegate_HouseVote(
      int userID,
      int sessionID,
      int factionID,
      int houseID,
      int targetFaction,
      bool application,
      bool vote,
      long factionsChangePos);

    public delegate void HouseVote_UserCallBack(HouseVote_ReturnType returnData);

    public delegate HouseVoteHouseLeader_ReturnType RemoteAsyncDelegate_HouseVoteHouseLeader(
      int userID,
      int sessionID,
      int factionID,
      int houseID,
      int leaderVote,
      long factionsChangePos);

    public delegate void HouseVoteHouseLeader_UserCallBack(
      HouseVoteHouseLeader_ReturnType returnData);

    public delegate TouchHouseVisitDate_ReturnType RemoteAsyncDelegate_TouchHouseVisitDate(
      int userID,
      int sessionID,
      int factionID);

    public delegate void TouchHouseVisitDate_UserCallBack(TouchHouseVisitDate_ReturnType returnData);

    public delegate LeaveHouse_ReturnType RemoteAsyncDelegate_LeaveHouse(
      int userID,
      int sessionID,
      int factionID,
      int houseID,
      long factionsChangePos);

    public delegate void LeaveHouse_UserCallBack(LeaveHouse_ReturnType returnData);

    public delegate FactionApplication_ReturnType RemoteAsyncDelegate_FactionApplication(
      int userID,
      int sessionID,
      int factionID,
      bool cancel);

    public delegate void FactionApplication_UserCallBack(FactionApplication_ReturnType returnData);

    public delegate FactionApplicationProcessing_ReturnType RemoteAsyncDelegate_FactionApplicationProcessing(
      int userID,
      int sessionID,
      int otherUserID,
      bool accept,
      bool reject,
      bool setMode);

    public delegate void FactionApplicationProcessing_UserCallBack(
      FactionApplicationProcessing_ReturnType returnData);

    public delegate GetParishMembersList_ReturnType RemoteAsyncDelegate_GetParishMembersList(
      int userID,
      int sessionID,
      int villageID);

    public delegate void GetParishMembersList_UserCallBack(
      GetParishMembersList_ReturnType returnData);

    public delegate GetParishFrontPageInfo_ReturnType RemoteAsyncDelegate_GetParishFrontPageInfo(
      int userID,
      int sessionID,
      int villageID,
      DateTime lastTime);

    public delegate void GetParishFrontPageInfo_UserCallBack(
      GetParishFrontPageInfo_ReturnType returnData);

    public delegate ParishWallDetailInfo_ReturnType RemoteAsyncDelegate_ParishWallDetailInfo(
      int userID,
      int sessionID,
      int parishCapitalID,
      long wallInfoID,
      int targetUserId,
      int type);

    public delegate void ParishWallDetailInfo_UserCallBack(
      ParishWallDetailInfo_ReturnType returnData);

    public delegate StandDownAsParishDespot_ReturnType RemoteAsyncDelegate_StandDownAsParishDespot(
      int userID,
      int sessionID,
      int villageID);

    public delegate void StandDownAsParishDespot_UserCallBack(
      StandDownAsParishDespot_ReturnType returnData);

    public delegate MakeParishVote_ReturnType RemoteAsyncDelegate_MakeParishVote(
      int userID,
      int sessionID,
      int villageID,
      int votedUserID);

    public delegate void MakeParishVote_UserCallBack(MakeParishVote_ReturnType returnData);

    public delegate SendTroopsToCapital_ReturnType RemoteAsyncDelegate_SendTroopsToCapital(
      int userID,
      int sessionID,
      int sourceVillageID,
      int targetVillageID,
      int peasants,
      int archers,
      int pikemen,
      int swordsmen,
      int catapults);

    public delegate void SendTroopsToCapital_UserCallBack(SendTroopsToCapital_ReturnType returnData);

    public delegate GetCapitalBarracksSpace_ReturnType RemoteAsyncDelegate_GetCapitalBarracksSpace(
      int userID,
      int sessionID,
      int sourceVillageID,
      int targetVillageID);

    public delegate void GetCapitalBarracksSpace_UserCallBack(
      GetCapitalBarracksSpace_ReturnType returnData);

    public delegate GetCountyElectionInfo_ReturnType RemoteAsyncDelegate_GetCountyElectionInfo(
      int userID,
      int sessionID,
      int villageID);

    public delegate void GetCountyElectionInfo_UserCallBack(
      GetCountyElectionInfo_ReturnType returnData);

    public delegate GetCountyFrontPageInfo_ReturnType RemoteAsyncDelegate_GetCountyFrontPageInfo(
      int userID,
      int sessionID,
      int villageID);

    public delegate void GetCountyFrontPageInfo_UserCallBack(
      GetCountyFrontPageInfo_ReturnType returnData);

    public delegate MakeCountyVote_ReturnType RemoteAsyncDelegate_MakeCountyVote(
      int userID,
      int sessionID,
      int villageID,
      int votedUserID);

    public delegate void MakeCountyVote_UserCallBack(MakeCountyVote_ReturnType returnData);

    public delegate GetProvinceElectionInfo_ReturnType RemoteAsyncDelegate_GetProvinceElectionInfo(
      int userID,
      int sessionID,
      int villageID);

    public delegate void GetProvinceElectionInfo_UserCallBack(
      GetProvinceElectionInfo_ReturnType returnData);

    public delegate GetProvinceFrontPageInfo_ReturnType RemoteAsyncDelegate_GetProvinceFrontPageInfo(
      int userID,
      int sessionID,
      int villageID);

    public delegate void GetProvinceFrontPageInfo_UserCallBack(
      GetProvinceFrontPageInfo_ReturnType returnData);

    public delegate MakeProvinceVote_ReturnType RemoteAsyncDelegate_MakeProvinceVote(
      int userID,
      int sessionID,
      int villageID,
      int votedUserID);

    public delegate void MakeProvinceVote_UserCallBack(MakeProvinceVote_ReturnType returnData);

    public delegate GetCountryElectionInfo_ReturnType RemoteAsyncDelegate_GetCountryElectionInfo(
      int userID,
      int sessionID,
      int villageID);

    public delegate void GetCountryElectionInfo_UserCallBack(
      GetCountryElectionInfo_ReturnType returnData);

    public delegate GetCountryFrontPageInfo_ReturnType RemoteAsyncDelegate_GetCountryFrontPageInfo(
      int userID,
      int sessionID,
      int villageID);

    public delegate void GetCountryFrontPageInfo_UserCallBack(
      GetCountryFrontPageInfo_ReturnType returnData);

    public delegate MakeCountryVote_ReturnType RemoteAsyncDelegate_MakeCountryVote(
      int userID,
      int sessionID,
      int villageID,
      int votedUserID);

    public delegate void MakeCountryVote_UserCallBack(MakeCountryVote_ReturnType returnData);

    public delegate GetIngameMessage_ReturnType RemoteAsyncDelegate_GetIngameMessage(
      int userID,
      int sessionID);

    public delegate void GetIngameMessage_UserCallBack(GetIngameMessage_ReturnType returnData);

    public delegate CancelInterdiction_ReturnType RemoteAsyncDelegate_CancelInterdiction(
      int userID,
      int sessionID,
      int villageID);

    public delegate void CancelInterdiction_UserCallBack(CancelInterdiction_ReturnType returnData);

    public delegate GetExcommunicationStatus_ReturnType RemoteAsyncDelegate_GetExcommunicationStatus(
      int userID,
      int sessionID,
      int villageID,
      int targetVillageID);

    public delegate void GetExcommunicationStatus_UserCallBack(
      GetExcommunicationStatus_ReturnType returnData);

    public delegate DisbandTroops_ReturnType RemoteAsyncDelegate_DisbandTroops(
      int userID,
      int sessionID,
      int villageID,
      int troopType,
      int amount);

    public delegate void DisbandTroops_UserCallBack(DisbandTroops_ReturnType returnData);

    public delegate DisbandPeople_ReturnType RemoteAsyncDelegate_DisbandPeople(
      int userID,
      int sessionID,
      int villageID,
      int troopType,
      int amount);

    public delegate void DisbandPeople_UserCallBack(DisbandPeople_ReturnType returnData);

    public delegate GetVillageInfoForDonateCapitalGoods_ReturnType RemoteAsyncDelegate_GetVillageInfoForDonateCapitalGoods(
      int userID,
      int sessionID,
      int parishCapitalID,
      int targetBuildingType);

    public delegate void GetVillageInfoForDonateCapitalGoods_UserCallBack(
      GetVillageInfoForDonateCapitalGoods_ReturnType returnData);

    public delegate DonateCapitalGoods_ReturnType RemoteAsyncDelegate_DonateCapitalGoods(
      int userID,
      int sessionID,
      int targetVillageID,
      int sourceVillageID,
      int resourceType,
      int amount,
      int buildingType,
      long targetBuildingID);

    public delegate void DonateCapitalGoods_UserCallBack(DonateCapitalGoods_ReturnType returnData);

    public delegate GetVillageStartLocations_ReturnType RemoteAsyncDelegate_GetVillageStartLocations(
      int userID,
      int sessionID);

    public delegate void GetVillageStartLocations_UserCallBack(
      GetVillageStartLocations_ReturnType returnData);

    public delegate SetStartingCounty_ReturnType RemoteAsyncDelegate_SetStartingCounty(
      int userID,
      int sessionID,
      int countyID);

    public delegate void SetStartingCounty_UserCallBack(SetStartingCounty_ReturnType returnData);

    public delegate CancelCard_ReturnType RemoteAsyncDelegate_CancelCard(
      int userID,
      int sessionID,
      int card);

    public delegate void CancelCard_UserCallBack(CancelCard_ReturnType returnData);

    public delegate UpdateCurrentCards_ReturnType RemoteAsyncDelegate_UpdateCurrentCards(
      int userID,
      int sessionID);

    public delegate void UpdateCurrentCards_UserCallBack(UpdateCurrentCards_ReturnType returnData);

    public delegate TutorialCommand_ReturnType RemoteAsyncDelegate_TutorialCommand(
      int userID,
      int sessionID,
      int tutorialAction);

    public delegate void TutorialCommand_UserCallBack(TutorialCommand_ReturnType returnData);

    public delegate GetQuestStatus_ReturnType RemoteAsyncDelegate_GetQuestStatus(
      int userID,
      int sessionID);

    public delegate void GetQuestStatus_UserCallBack(GetQuestStatus_ReturnType returnData);

    public delegate CompleteQuest_ReturnType RemoteAsyncDelegate_CompleteQuest(
      int userID,
      int sessionID,
      int quest);

    public delegate void CompleteQuest_UserCallBack(CompleteQuest_ReturnType returnData);

    public delegate FlagQuestObjectiveComplete_ReturnType RemoteAsyncDelegate_FlagQuestObjectiveComplete(
      int userID,
      int sessionID,
      int objective);

    public delegate void FlagQuestObjectiveComplete_UserCallBack(
      FlagQuestObjectiveComplete_ReturnType returnData);

    public delegate CheckQuestObjectiveComplete_ReturnType RemoteAsyncDelegate_CheckQuestObjectiveComplete(
      int userID,
      int sessionID,
      int quest);

    public delegate void CheckQuestObjectiveComplete_UserCallBack(
      CheckQuestObjectiveComplete_ReturnType returnData);

    public delegate UpdateDiplomacyStatus_ReturnType RemoteAsyncDelegate_UpdateDiplomacyStatus(
      int userID,
      int sessionID,
      bool state);

    public delegate void UpdateDiplomacyStatus_UserCallBack(
      UpdateDiplomacyStatus_ReturnType returnData);

    public delegate SendCommands_ReturnType RemoteAsyncDelegate_SendCommands(
      int userID,
      int sessionID,
      int targetUserID,
      int command,
      int duration,
      string reason);

    public delegate void SendCommands_UserCallBack(SendCommands_ReturnType returnData);

    public delegate InitialiseFreeCards_ReturnType RemoteAsyncDelegate_InitialiseFreeCards(
      int userID,
      int sessionID);

    public delegate void InitialiseFreeCards_UserCallBack(InitialiseFreeCards_ReturnType returnData);

    public delegate TestAchievements_ReturnType RemoteAsyncDelegate_TestAchievements(
      int userID,
      int sessionID,
      List<int> achievementsToTest,
      List<AchievementData> achievementData);

    public delegate void TestAchievements_UserCallBack(TestAchievements_ReturnType returnData);

    public delegate AchievementProgress_ReturnType RemoteAsyncDelegate_AchievementProgress(
      int userID,
      int sessionID);

    public delegate void AchievementProgress_UserCallBack(AchievementProgress_ReturnType returnData);

    public delegate GetQuestData_ReturnType RemoteAsyncDelegate_GetQuestData(
      int userID,
      int sessionID,
      bool full);

    public delegate void GetQuestData_UserCallBack(GetQuestData_ReturnType returnData);

    public delegate StartNewQuest_ReturnType RemoteAsyncDelegate_StartNewQuest(
      int userID,
      int sessionID,
      int questID);

    public delegate void StartNewQuest_UserCallBack(StartNewQuest_ReturnType returnData);

    public delegate CompleteAbandonNewQuest_ReturnType RemoteAsyncDelegate_CompleteAbandonNewQuest(
      int userID,
      int sessionID,
      int questID,
      bool abandon,
      bool glory,
      int villageID);

    public delegate void CompleteAbandonNewQuest_UserCallBack(
      CompleteAbandonNewQuest_ReturnType returnData);

    public delegate SpinTheWheel_ReturnType RemoteAsyncDelegate_SpinTheWheel(
      int userID,
      int sessionID,
      int villageID,
      int wheelType);

    public delegate void SpinTheWheel_UserCallBack(SpinTheWheel_ReturnType returnData);

    public delegate SetVacationMode_ReturnType RemoteAsyncDelegate_SetVacationMode(
      int userID,
      int sessionID,
      int numDays);

    public delegate void SetVacationMode_UserCallBack(SetVacationMode_ReturnType returnData);

    public delegate PremiumOverview_ReturnType RemoteAsyncDelegate_PremiumOverview(
      int userID,
      int sessionID);

    public delegate void PremiumOverview_UserCallBack(PremiumOverview_ReturnType returnData);

    public delegate GetLastAttacker_ReturnType RemoteAsyncDelegate_GetLastAttacker(
      int userID,
      int sessionID);

    public delegate void GetLastAttacker_UserCallBack(GetLastAttacker_ReturnType returnData);

    public delegate PreValidateCardToBePlayed_ReturnType RemoteAsyncDelegate_PreValidateCardToBePlayed(
      int userID,
      int sessionID,
      int card,
      int data);

    public delegate void PreValidateCardToBePlayed_UserCallBack(
      PreValidateCardToBePlayed_ReturnType returnData);

    public delegate GetInvasionInfo_ReturnType RemoteAsyncDelegate_GetInvasionInfo(
      int userID,
      int sessionID);

    public delegate void GetInvasionInfo_UserCallBack(GetInvasionInfo_ReturnType returnData);

    public delegate WorldInfo_ReturnType RemoteAsyncDelegate_WorldInfo();

    public delegate void WorldInfo_UserCallBack(WorldInfo_ReturnType returnData);

    public delegate EndWorld_ReturnType RemoteAsyncDelegate_EndWorld(int userID, int sessionsID);

    public delegate void EndWorld_UserCallBack(EndWorld_ReturnType returnData);

    public delegate EndOfTheWorldStats_ReturnType RemoteAsyncDelegate_EndOfTheWorldStats(
      int userID,
      int sessionsID);

    public delegate void EndOfTheWorldStats_UserCallBack(EndOfTheWorldStats_ReturnType returnData);

    public delegate GetKillStreakData_ReturnType RemoteAsyncDelegate_GetKillStreakData(
      int userID,
      int sessionsID);

    public delegate void GetKillStreakData_UserCallBack(GetKillStreakData_ReturnType returnData);

    public delegate GetUserContestData_ReturnType RemoteAsyncDelegate_GetUserContestData(
      int userID,
      int sessionID,
      int contestID);

    public delegate void GetUserContestData_UserCallBack(GetUserContestData_ReturnType returnData);

    public delegate GetContestDataRange_ReturnType RemoteAsyncDelegate_GetContestDataRange(
      int userID,
      int sessionID,
      int contestID,
      int topIndex,
      int numEntries,
      int rankBand);

    public delegate void GetContestDataRange_UserCallBack(GetContestDataRange_ReturnType returnData);

    public delegate GetContestHistoryIDs_ReturnType RemoteAsyncDelegate_GetContestHistoryIDs(
      int userID,
      int sessionID);

    public delegate void GetContestHistoryIDs_UserCallBack(
      GetContestHistoryIDs_ReturnType returnData);

    public delegate Chat_Login_ReturnType RemoteAsyncDelegate_Chat_Login(int userID, int sessionID);

    public delegate void Chat_Login_UserCallBack(Chat_Login_ReturnType returnData);

    public delegate Chat_Logout_ReturnType RemoteAsyncDelegate_Chat_Logout(
      int userID,
      int sessionID);

    public delegate void Chat_Logout_UserCallBack(Chat_Logout_ReturnType returnData);

    public delegate Chat_SetReceivingState_ReturnType RemoteAsyncDelegate_Chat_SetReceivingState(
      int userID,
      int sessionID,
      bool state);

    public delegate void Chat_SetReceivingState_UserCallBack(
      Chat_SetReceivingState_ReturnType returnData);

    public delegate Chat_SendText_ReturnType RemoteAsyncDelegate_Chat_SendText(
      int userID,
      int sessionID,
      int roomType,
      int roomID,
      string text,
      bool filter);

    public delegate void Chat_SendText_UserCallBack(Chat_SendText_ReturnType returnData);

    public delegate Chat_ReceiveText_ReturnType RemoteAsyncDelegate_Chat_ReceiveText(
      int userID,
      int sessionID,
      List<Chat_RoomID> roomsToRegister,
      bool changeRooms,
      bool filter);

    public delegate void Chat_ReceiveText_UserCallBack(Chat_ReceiveText_ReturnType returnData);

    public delegate Chat_SendParishText_ReturnType RemoteAsyncDelegate_Chat_SendParishText(
      int userID,
      int sessionID,
      int parishID,
      int subForumID,
      string text,
      DateTime lastTime,
      bool filter);

    public delegate void Chat_SendParishText_UserCallBack(Chat_SendParishText_ReturnType returnData);

    public delegate Chat_ReceiveParishText_ReturnType RemoteAsyncDelegate_Chat_ReceiveParishText(
      int userID,
      int sessionID,
      int parishID,
      DateTime lastTime,
      bool filter);

    public delegate void Chat_ReceiveParishText_UserCallBack(
      Chat_ReceiveParishText_ReturnType returnData);

    public delegate Chat_BackFillParishText_ReturnType RemoteAsyncDelegate_Chat_BackFillParishText(
      int userID,
      int sessionID,
      int parishID,
      int subPage,
      long oldestIDDownloaded,
      DateTime oldestTime,
      bool filter);

    public delegate void Chat_BackFillParishText_UserCallBack(
      Chat_BackFillParishText_ReturnType returnData);

    public delegate Chat_MarkParishTextRead_ReturnType RemoteAsyncDelegate_Chat_MarkParishTextRead(
      int userID,
      int sessionID,
      int parishID,
      int pageID,
      long readID);

    public delegate void Chat_MarkParishTextRead_UserCallBack(
      Chat_MarkParishTextRead_ReturnType returnData);

    public delegate Chat_Admin_Command_ReturnType RemoteAsyncDelegate_Chat_Admin_Command(
      int userID,
      int sessionID,
      int command,
      int targetUserID);

    public delegate void Chat_Admin_Command_UserCallBack(Chat_Admin_Command_ReturnType returnData);

    public delegate void CommonData_UserCallBack(Common_ReturnData returnData);

    public class RTT_Log_data
    {
      public Type packetType;
      public int time;
    }
  }
}
