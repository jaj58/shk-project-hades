// Decompiled with JetBrains decompiler
// Type: Kingdoms.WorldMap
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using Kingdoms.Bot;
using StatTracking;
using Stronghold.AuthClient;
using Stronghold.ShieldClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class WorldMap
  {
    private const double ARMY_UPDATE_ZOOM_THRESHOLD = 15.0;
    private const double ARMY_DRAW_ZOOM_THRESHOLD = 5.5;
    private const double ARMY_ARROW_ZOOM_THRESHOLD = 10.0;
    private const int NUMLEVELS = 5;
    public const int downloadSteps = 4;
    private const int SAVEDATA_VERSION_ID = 10;
    public const int ZOOM_MAX_VAL = 27;
    private const int ZOOM_CENTRE_VAL = 23;
    public const double ZOOM_MIN_VAL_RETINA = 0.0;
    public const double ZOOM_MIN_VAL_NORMAL = 0.0;
    private const int SHIELD_CACHE_SIZE = 125;
    public const int ZOOM_MAX_VAL_CHANGE = 7;
    public const float WORLD_SCALE_OLD_MAX = 17f;
    public const float WORLD_SCALE_MAX = 24f;
    public const float WORLD_SCALE_MAX_CLOSE = 23.9f;
    public const double REGION_DRAW_ZOOM_LEVEL = 5.0;
    public const double REGION_BORDER_DRAW_ZOOM_LEVEL = 5.0;
    private const double ZOOM_TIME = 16.0;
    private const int NUM_VILLAGE_SPRITEWRAPPERS = 1;
    private const int READ_AROUND_RANGE = 50;
    public static bool USE_QUADTREE = false;
    public static bool LOG_ARMY_ERRORS = false;
    public bool UpdatingArmies;
    public bool DrawingArmies;
    public bool DrawingArmyArrows = true;
    public long tutorialArmyID = -1;
    public List<WorldMap.ArmyRetrieveData> requestedReturnedArmyIDs = new List<WorldMap.ArmyRetrieveData>();
    private SparseArray armyArray = new SparseArray();
    private SparseArray reinforcementArray = new SparseArray();
    private long highestArmySeen = -1;
    private long highestDownloadedArmy = -1;
    public bool doSelectTutorialArmy;
    private List<long> rememberedExistingArmies = new List<long>();
    private int a_startAt;
    private int a_endAt;
    private int a_perFrame = 100;
    public List<long> thisVillageArmies = new List<long>();
    private SparseArray scoutsForaging = new SparseArray();
    private SparseArray scoutsVillageForaging = new SparseArray();
    private SparseArray attackingArmies = new SparseArray();
    private SparseArray villagesInvolvedInAttacks = new SparseArray();
    private SparseArray villagesInvolvedInAIAttacks = new SparseArray();
    private int alphaPulse;
    private int alphaValue;
    private SparseArray armyIconsFilter = new SparseArray();
    private DateTime wolfsRevengeStart = DateTime.MinValue;
    private DateTime wolfsRevengeEnd = DateTime.MinValue;
    private SparseArray invasionMarkerState = new SparseArray();
    private List<AIWorldInvasionData> invasionInfo;
    private DateTime lastInvasionInfoTime = DateTime.MinValue;
    private DateTime lastUpdateInvasionInfoTime = DateTime.MinValue;
    public int aiWorldGloryWinLevel = 1000;
    public WorldMap.FWData[] fwDataList = new WorldMap.FWData[25];
    public int[] fwChickenOrder = new int[3];
    public int[] fwSheepOrder = new int[3];
    public int[] fwJesterOrder = new int[3];
    public int[] fwPigOrder = new int[3];
    private string fwSourceData = "3,10,30,60,30,10,5,8,12,5,2,6,0,0,0,9,0,0,0,25,0,0,0,1,1,0,1,0,300,0.06,0.3,0.3,0,0.3,0,7,1,0,0,1,1,0,1,0,1000,0.1,0.1,0.5,0.01,0,21,5,1,0,0,1,1,0,1,0,600,0.06,0.3,0.3,0,0,1,25,0,0,0,1,1,0,1,0,300,0.06,0.3,0.3,0,0.3,0,25,0,0,0,1,1,0,1,0,300,0.06,0.3,0.3,0,0.3,0,25,1,1,10,1,1,0,1,0,300,0.06,0.3,0.3,0,0.3,19,25,0,1,7,1,1,0,1,1000,2500,0.1,0.3,0.3,0,0,0,50,0,1,40,1,0.1,0.05,0.6,0,300,0.06,0.3,0.3,0,0.3,0,8,1,0,0,1,1,0,1,0,300,0.05,0.3,0.3,0,0,14,20,1,0,0,1,1,0,1,100,300,0.06,0.4,0.3,0,0,0,205,1,1,10,1,0,0,1,500,2000,0.06,0.3,0.3,0,0.3,0,25,0,0,0,1,1,0,1,0,300,0.06,0.3,0.3,0,0.3,0,5,1,1,20,1,1,0,1,0,3000,0.6,0.3,0.5,0,0.3,7,25,0,0,0,1,1,0,1,0,300,0.06,0.3,0.3,0,0.3,0,20,0,1,10,1,0.25,0.03,0.5,0,300,0.06,0.1,0.3,0,0,0,25,0,0,0,1,1,0,1,0,300,0.06,0.3,0.3,0,0.3,0,25,0,0,0,1,1,0,1,0,300,0.06,0.3,0.3,0,0.3,0,50,1,1,100,1,0.2,0.05,0.6,1000,3000,0.06,0.1,0.1,0,0,0,20,1,1,5,0,1,0,1,0,300,0.06,0.3,0.3,0,0.3,0,25,0,1,4,1,1,0.01,0.1,0,300,0.06,0.3,0.3,0,0.3,0,25,0,0,0,1,1,0,1,0,300,0.06,0.3,0.3,0,0.3,0,25,0,1,30,1,1,0,1,0,300,0.06,0.3,0.3,0,0.3,0,25,0,0,0,1,1,0,1,0,300,0.06,0.3,0.3,0,0.3,0,25,0,1,5,0,0.3,0.01,0.5,0,600,0.06,0.3,73,0.1,0.6,0,25,1,1,2,1,1,0,1,0,300,0.06,0.3,0.3,0,0.3,0,";
    private int[] fwSpriteIDs = new int[25]
    {
      -1,
      1,
      4,
      6,
      24,
      8,
      13,
      27,
      22,
      12,
      18,
      0,
      10,
      7,
      26,
      14,
      19,
      25,
      21,
      20,
      3,
      17,
      23,
      2,
      5
    };
    private List<WorldMap.ClusterBase> clusters = new List<WorldMap.ClusterBase>();
    public int fwMode;
    public int fwTick;
    public int totalNumFW = 3;
    public int totalNumFWBusy = 6;
    public int totalNumFWCrazy = 10;
    public int fwNormalChance = 80;
    public int fwBusyChance = 15;
    public int fwCrazyChance = 5;
    public int fwCycle = 5;
    private int fwUnique;
    private float fwDisplayClock;
    private List<WorldMap.UserVillageData> m_userVillages;
    private List<WorldMap.UserVillageData> m_userRelatedVillages = new List<WorldMap.UserVillageData>();
    private double m_userGoldLevel;
    private double m_userGoldIncomeRate;
    private double m_lastGoldUpdate;
    private double m_userHonourLevel;
    private double m_userHonourIncomeRate;
    private double m_lastHonourUpdate;
    private double m_userFaithPointsLevel;
    private double m_userFaithPointsRate;
    private double m_lastFaithPointsUpdate;
    private int m_userPoints;
    private int m_numMadeCaptains;
    private int m_userRank;
    private int m_userRankSubLevel;
    private int m_mostAge4Villages;
    private bool retrievingUserVillages;
    public DateTime m_worldStartDate = DateTime.Now;
    private WorldMap.VillageNameComparer villageNameComparer = new WorldMap.VillageNameComparer();
    public ResearchData userResearchData;
    private bool requestSent;
    private DateTime m_lastResearchCompleteTimeMatch = DateTime.MinValue;
    private DateTime m_lastResearchCompleteRequestTime = DateTime.MinValue;
    private int m_researchLagCount;
    private WorldMap.ResearchChangedDelegate uiResearchDelegate;
    private DateTime lastDoResearchClick = DateTime.MinValue;
    private bool inDoResearch;
    private DateTime lastBuyPointClick = DateTime.MinValue;
    private bool inBuyPoint;
    private CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate onResearchPointPurchaseDelegate;
    private bool newTutorialAvailable;
    private QuestsAndTutorialInfo m_tutorialInfo = new QuestsAndTutorialInfo();
    private List<int> tutorialQuestsObjectivesComplete = new List<int>();
    private bool inTutorialAdvance;
    private int targetTutorialStage;
    private SparseArray QuestObjectivesSent = new SparseArray();
    private int numVacationsAvailable = 2;
    private bool vacationNot30Days;
    private SparseArray personArray = new SparseArray();
    private List<CounterSpyInfo> counterSpyInfo = new List<CounterSpyInfo>();
    private DateTime lastPersonTime = DateTime.Now.AddYears(-5);
    private int p_startAt;
    private int p_endAt;
    private int p_perFrame = 100;
    public bool isBigpointAccount;
    public int ProfileCardpoints;
    public int ProfileCrowns;
    public int FakeCardPoints;
    public int ProfileNumFriends;
    public DateTime LastUpdatedCrowns = DateTime.Now.AddHours(-1.0);
    private FreeCardsData freeCardInfo = new FreeCardsData();
    private bool inviteSystemNotImplemented = true;
    private bool mapEditing;
    private string mapDataFilename = string.Empty;
    private bool worldEnded;
    private bool worldEnded_message;
    private int currentMapType;
    private int yMarkerOffset;
    private bool smallMapFont;
    public bool EUMap;
    public bool GSMap;
    public bool chMap;
    public bool KGMap;
    public bool aiWorldTreeBuilding;
    public List<int> aiWorldSpecialVillages = new List<int>();
    private bool m_cachesFlushed;
    private bool m_dataLoaded;
    private bool m_namesLoaded;
    private bool m_downloadedDataSafely = true;
    private Thread m_WorkerThread;
    private bool downloadComplete;
    private bool downloadFullyComplete;
    public bool delayedFactionSave;
    public int downloadingCounter;
    private bool loadingErrored;
    private int threadDelaySize = 50;
    private FactionData inactiveFaction = new FactionData();
    private static bool fullTickFullMode = true;
    public DateTime lastFullTickCall = DateTime.MinValue;
    public static int TreasureCastle_AttackGap = 86400;
    private SparseArray m_parishChatLog = new SparseArray();
    private WorldMap.ParishChatComparer parishChatComparer = new WorldMap.ParishChatComparer();
    private SparseArray m_parishWallDonateDetails = new SparseArray();
    private bool inTestAchievements;
    private DateTime lastTestAchievements = DateTime.MinValue;
    public bool FacebookFreePack;
    private SparseArray traderArray = new SparseArray();
    private DateTime lastTraderTime = DateTime.Now.AddYears(-5);
    private List<WorldMap.LocalTrader> thisVillageTraders;
    private int t_startAt;
    private int t_endAt;
    private int t_perFrame = 100;
    public List<int> tradingVillageList = new List<int>();
    public List<int> marketTradingVillageList = new List<int>();
    public int worldMapWidth;
    public int worldMapHeight;
    public int villageMapWidth;
    public int villageMapHeight;
    private long storedVillageFactionsPos = -1;
    private long storedRegionFactionsPos = -1;
    private long storedCountyFactionsPos = -1;
    private long storedProvinceFactionsPos = -1;
    private long storedCountryFactionsPos = -1;
    private long storedParishFlagsPos = -1;
    private long storedCountyFlagsPos = -1;
    private long storedProvinceFlagsPos = -1;
    private long storedCountryFlagsPos = -1;
    private long storedFactionChangesPos = -1;
    private long storedVillageNamePos = -1;
    private WorldMap.WorldPoint[] pointList;
    private WorldMap.WorldPointList[] countryList;
    private WorldMap.WorldPointList[] provincesList;
    private WorldMap.WorldPointList[] countyList;
    private VillageData[] villageList;
    private WorldMap.WorldPointList[] regionList;
    private WorldMap.WorldPointList[] seaList;
    private WorldMap.IslandInfoList[] islandList;
    private List<WorldMap.IslandInfoList> aiWorldInvasionLineList;
    public bool drawFakeProvinceBorders;
    private HouseData[] m_houseData;
    private HouseVoteData m_houseVoteData;
    private SparseArray m_factionData = new SparseArray();
    private FactionMemberData[] m_factionMembers;
    private FactionInviteData[] m_factionInvites;
    private List<FactionInviteData> m_factionApplications;
    private int m_factionLeaderVote = -1;
    private int[] m_factionAllies;
    private int[] m_factionEnemies;
    private int[] m_houseAllies;
    private int[] m_houseEnemies;
    private int[] m_gloryPoints;
    private GloryRoundData m_gloryRoundData;
    private DateTime lastHouseGloryPointsUpdate = DateTime.MinValue;
    private bool secondAgeWorld;
    private bool thirdAgeWorld;
    private bool fourthAgeWorld;
    private bool fifthAgeWorld;
    private bool sixthAgeWorld;
    private bool seventhAgeWorld;
    private int m_globalWorldID = -1;
    private DateTime lastTimeOwnMembersUpdated = DateTime.MinValue;
    private SparseArray cachedFactionMemberData = new SparseArray();
    private SparseArray cachedUserInfo = new SparseArray();
    private WorldMap.VillageQuadNode m_baseNode;
    private GraphicsMgr gfx;
    private double m_worldZoomInverted;
    private double m_worldScale = 1.0;
    private double m_screenCentreX;
    private double m_screenCentreY;
    public int m_screenWidth;
    public int m_screenHeight;
    private MapIconDrawCall mapIcon;
    public static int mapIconDrawCount = 0;
    private int seaConditionsDay = -1;
    private int seaConditionsEarly = -1;
    private int seaConditionsLate = -1;
    private int lastRetieveUserID = -1;
    private int lastRetieveVillageID = -1;
    private bool lastForceExtended;
    private DateTime lastRetieveUserTime = DateTime.MinValue;
    private DateTime lastRetieveVillageTime = DateTime.MinValue;
    private int cached_retrieveVillageID = -1;
    private int cached_retrieveUserID = -1;
    private DateTime cached_retrieveVillageUserInfoDate = DateTime.MinValue;
    private SparseArray specialVillageCache = new SparseArray();
    private int lastSpecialRequestSent = -1;
    private int lastActualSpecialRequestSent = -1;
    private DateTime lastActualSpecialRequestTime = DateTime.MinValue;
    private List<LoginHistoryInfo> loginHistory;
    private ShieldFactory playerShieldFactory;
    private Shield playerShield;
    private SparseArray worldShields = new SparseArray();
    private ShieldFactory.AsyncDelegate playerShieldCallback;
    private int activeShieldsWorldID = -1;
    private bool worldShieldsAvailable;
    private List<WorldMap.ShieldTextureCacheEntry> worldShieldCache = new List<WorldMap.ShieldTextureCacheEntry>();
    private SparseArray worldShieldCachePlayerIDs = new SparseArray();
    private Shield ratShield = new Shield(constants.RAT_SHIELD);
    private Shield snakeShield = new Shield(constants.SNAKE_SHIELD);
    private Shield wolfShield = new Shield(constants.WOLF_SHIELD);
    private Shield pigShield = new Shield(constants.PIG_SHIELD);
    private List<UserRelationship> userRelations = new List<UserRelationship>();
    private List<UserMarker> userMarkers = new List<UserMarker>();
    private DateTime m_lastTreasureCastleAttackTime = DateTime.MinValue;
    private int m_treasure1Tickets;
    private int m_treasure2Tickets;
    private int m_treasure3Tickets;
    private int m_treasure4Tickets;
    private int m_treasure5Tickets;
    private int m_numQuestTickets;
    private NewQuestsData m_newQuestData;
    public string lastAttacker = "";
    public bool newPlayer;
    public DateTime lastAttackerLastUpdate = DateTime.MinValue;
    public bool inUpdateLastAttackerInfo;
    public bool PickingStartCounty;
    public Dictionary<int, WorldMap.AvailableCounty> AvailableCounties;
    private List<WorldHouseHistoryItem> playbackItems;
    private int[,] playbackCountriesData;
    private int[,] playbackProvincesData;
    private bool playingCountries;
    private bool playingProvinces;
    public int playbackDay;
    public int playbackTotalDays;
    public int playbackBasedDay;
    private int lastSetPlaybackDay;
    private DateTime playbackStartTime;
    private DateTime playbackBaseTime;
    private DateTime playbackFrameTime;
    private DateTime playbackLastUpdateTime;
    private int playbackMaxCountries;
    private int playbackMaxProvinces;
    private bool playbackPaused = true;
    private double playbackFrameMS = 500.0;
    private double playbackFrameFraction;
    private DateTime nextRatsCalc = DateTime.MinValue;
    private int lastRatsValue;
    private DateTime nextSnakesCalc = DateTime.MinValue;
    private int lastSnakesValue;
    private DateTime nextPigsCalc = DateTime.MinValue;
    private int lastPigsValue;
    private DateTime nextWolfsCalc = DateTime.MinValue;
    private int lastWolfsValue;
    public int saleStartTime;
    public int saleEndTime;
    public int salePercentage;
    public int contestStartTime;
    public int contestEndTime;
    public string contestName = string.Empty;
    public string contestDescription = string.Empty;
    public int contestID;
    public List<int> previousContests = new List<int>();
    public ContestType contestType;
    public List<int> pendingPrizes = new List<int>();
    public bool showGloryResults;
    public DateTime KillStreakTimer = DateTime.MinValue;
    private int KillStreakPoints;
    private int m_KillStreakCount;
    private bool bLinelessMap;
    private bool overrideLinelessMap;
    public WorldMapFilter worldMapFilter = new WorldMapFilter();
    public static Color SEACOLOR = Color.FromArgb(140, 182, 206);
    private bool drawDebugNames;
    private bool drawDebugVillageNames;
    private static Color[] areaColorList = new Color[21]
    {
      Color.FromArgb((int) byte.MaxValue, 169, 202, 149),
      Color.FromArgb((int) byte.MaxValue, 236, 20, 20),
      Color.FromArgb((int) byte.MaxValue, 236, 128, 20),
      Color.FromArgb((int) byte.MaxValue, 240, 240, 25),
      Color.FromArgb((int) byte.MaxValue, 22, 236, 22),
      Color.FromArgb((int) byte.MaxValue, 4, 200, 200),
      Color.FromArgb((int) byte.MaxValue, 2, 2, 188),
      Color.FromArgb((int) byte.MaxValue, 155, 68, 196),
      Color.FromArgb((int) byte.MaxValue, 241, 123, 177),
      Color.FromArgb((int) byte.MaxValue, 231, 231, 231),
      Color.FromArgb((int) byte.MaxValue, 41, 41, 48),
      Color.FromArgb((int) byte.MaxValue, (int) sbyte.MaxValue, (int) sbyte.MaxValue, (int) sbyte.MaxValue),
      Color.FromArgb((int) byte.MaxValue, 109, 103, 3),
      Color.FromArgb((int) byte.MaxValue, 2, 94, 46),
      Color.FromArgb((int) byte.MaxValue, 3, 92, 125),
      Color.FromArgb((int) byte.MaxValue, 58, 162, 230),
      Color.FromArgb((int) byte.MaxValue, 157, 42, 41),
      Color.FromArgb((int) byte.MaxValue, 134, 89, 46),
      Color.FromArgb((int) byte.MaxValue, 194, 194, 82),
      Color.FromArgb((int) byte.MaxValue, 84, 3, 207),
      Color.FromArgb((int) byte.MaxValue, 210, 175, 226)
    };
    private static Color[] villageColorList = new Color[22]
    {
      Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue),
      Color.FromArgb((int) byte.MaxValue, 236, 20, 20),
      Color.FromArgb((int) byte.MaxValue, 236, 128, 20),
      Color.FromArgb((int) byte.MaxValue, 240, 240, 25),
      Color.FromArgb((int) byte.MaxValue, 22, 236, 22),
      Color.FromArgb((int) byte.MaxValue, 4, 200, 200),
      Color.FromArgb((int) byte.MaxValue, 2, 2, 188),
      Color.FromArgb((int) byte.MaxValue, 155, 68, 196),
      Color.FromArgb((int) byte.MaxValue, 241, 123, 177),
      Color.FromArgb((int) byte.MaxValue, 231, 231, 231),
      Color.FromArgb((int) byte.MaxValue, 41, 41, 48),
      Color.FromArgb((int) byte.MaxValue, (int) sbyte.MaxValue, (int) sbyte.MaxValue, (int) sbyte.MaxValue),
      Color.FromArgb((int) byte.MaxValue, 109, 103, 3),
      Color.FromArgb((int) byte.MaxValue, 2, 94, 46),
      Color.FromArgb((int) byte.MaxValue, 3, 92, 125),
      Color.FromArgb((int) byte.MaxValue, 58, 162, 230),
      Color.FromArgb((int) byte.MaxValue, 157, 42, 41),
      Color.FromArgb((int) byte.MaxValue, 134, 89, 46),
      Color.FromArgb((int) byte.MaxValue, 194, 194, 82),
      Color.FromArgb((int) byte.MaxValue, 84, 3, 207),
      Color.FromArgb((int) byte.MaxValue, 210, 175, 226),
      Color.FromArgb((int) byte.MaxValue, 0, 0, 0)
    };
    public bool TributeLines;
    public bool PoliticalMap = true;
    public bool PolitcalMapView = true;
    public bool GeographicalMap = true;
    private int pulse;
    private int pulseValue;
    private int contestPulseValue = 175;
    private int contestPulse = 5;
    private bool xmasPresents;
    public bool overIcon;
    public bool overWikiHelp;
    public bool overTicketsIcon;
    public bool overWolf;
    public bool overRoyalTower;
    public bool overSale;
    public bool overOffer;
    public bool overContest;
    private int[] experimentalColourRemapping = new int[5]
    {
      0,
      3,
      1,
      2,
      4
    };
    private Color islandLineColor = ARGBColors.DarkBlue;
    private List<WorldMap.MapText> textDrawList = new List<WorldMap.MapText>();
    private int m_rolloverTargetVillage = -1;
    private int m_rolloverTargetVillageNoDelay = -1;
    public int m_userInfoShieldRolloverUserID = -1;
    private int m_rolloverVillageShieldID = -1;
    private Point m_rolloverLastMousepos = new Point();
    private long m_rolloverLastTime;
    private List<WorldMap.InterVillageLine> interVillageLines = new List<WorldMap.InterVillageLine>();
    private List<WorldMap.InterVillageLine> dynamicVillageLines = new List<WorldMap.InterVillageLine>();
    private bool m_zooming;
    private double m_targetZoom;
    private double m_zoomDiff;
    private double m_zoomXPosDiff;
    private double m_zoomYPosDiff;
    private double m_zoomXPosTarget;
    private double m_zoomYPosTarget;
    private double m_zoomChangeThisFrame;
    public bool isDraggingMap;
    private bool m_leftMouseHeldDown;
    private double m_lastMousePressedTime;
    private Point m_baseMousePos = new Point();
    private double m_baseScreenX;
    private double m_baseScreenY;
    private double m_doubleClickTime = DXTimer.GetCurrentMilliseconds();
    private Point m_doubleClickMousePos = new Point();
    private bool dragMode;
    public static bool USE_MOMENTUM = true;
    public static bool KILL_SCROLLING = false;
    public static bool FORCE_VILLAGE_SELECTION_ALWAYS = false;
    private double lastX;
    private double lastY;
    public Point lastClickedLocation;
    public int LastClickedVillage = -1;
    private double m_zoomCap;
    private int m_zoomStage = -1;
    private double m_stagedTargetZoom;
    private double m_stagedTargetX;
    private double m_stagedTargetY;
    private int m_multiStageSoundMode;
    private WorldMap.WorldZoomCallback worldZoomCallback;
    private int zoomMin;
    private int zoomMax = 27000;
    private int zoomCurrent = 4000;
    private SpriteWrapper villageSprite;
    private SpriteWrapper worldTileSprite = new SpriteWrapper();
    private SpriteWrapper worldTreeSprite = new SpriteWrapper();
    private SpriteWrapper overlaySprite = new SpriteWrapper();
    private SpriteWrapper updateClockSprite = new SpriteWrapper();
    private SpriteWrapper tutorialOverlaySprite = new SpriteWrapper();
    private SpriteWrapper freeCardsSprite = new SpriteWrapper();
    private SpriteWrapper freeCardsSprite2 = new SpriteWrapper();
    private SpriteWrapper wikiHelpSprite = new SpriteWrapper();
    private SpriteWrapper wolfsRevengeSprite = new SpriteWrapper();
    private SpriteWrapper wolfsRevengeSprite2 = new SpriteWrapper();
    private SpriteWrapper saleSprite = new SpriteWrapper();
    private SpriteWrapper offerSprite = new SpriteWrapper();
    private SpriteWrapper contestSprite = new SpriteWrapper();
    private List<SpriteWrapper> saleDigits = new List<SpriteWrapper>();
    private List<SpriteWrapper> saleTimer = new List<SpriteWrapper>();
    private List<SpriteWrapper> offerTimer = new List<SpriteWrapper>();
    private SpriteWrapper saleClock = new SpriteWrapper();
    private SpriteWrapper offerClock = new SpriteWrapper();
    private SpriteWrapper seaSprite = new SpriteWrapper();
    private SpriteWrapper royalTowerSprite = new SpriteWrapper();
    private SpriteWrapper royalTowerSprite1 = new SpriteWrapper();
    private SpriteWrapper royalTowerSprite2 = new SpriteWrapper();
    private SpriteWrapper royalTowerSprite3 = new SpriteWrapper();
    private SpriteWrapper ticketsSprite = new SpriteWrapper();
    private SpriteWrapper ticketsSprite2 = new SpriteWrapper();
    public int TILEMAP_WIDTH = 691;
    public int TILEMAP_HEIGHT = 804;
    private short[,] mapTileGrid;
    private byte[,] tree1Grid;
    private byte[,] tree2Grid;
    public bool haveInitMapTiles;
    private int[] ukCountyColour = new int[114]
    {
      0,
      1,
      2,
      1,
      2,
      1,
      0,
      1,
      0,
      0,
      2,
      0,
      3,
      1,
      3,
      2,
      1,
      0,
      2,
      0,
      1,
      0,
      2,
      3,
      1,
      3,
      0,
      1,
      0,
      0,
      1,
      3,
      1,
      0,
      0,
      0,
      1,
      2,
      3,
      2,
      1,
      0,
      3,
      0,
      3,
      2,
      1,
      3,
      0,
      1,
      0,
      0,
      0,
      2,
      1,
      3,
      0,
      1,
      3,
      2,
      0,
      3,
      1,
      0,
      1,
      0,
      3,
      0,
      3,
      1,
      0,
      1,
      1,
      2,
      0,
      1,
      0,
      0,
      0,
      0,
      0,
      0,
      2,
      1,
      0,
      2,
      0,
      1,
      2,
      3,
      1,
      2,
      0,
      3,
      2,
      3,
      3,
      1,
      0,
      3,
      0,
      2,
      0,
      1,
      2,
      0,
      1,
      0,
      1,
      3,
      2,
      3,
      2,
      0
    };
    private int[] ukProvinceColour = new int[23]
    {
      0,
      1,
      2,
      0,
      1,
      3,
      1,
      1,
      2,
      0,
      2,
      0,
      2,
      0,
      1,
      2,
      2,
      0,
      1,
      0,
      1,
      2,
      0
    };
    private int[] ukCountryColour = new int[4]{ 0, 1, 1, 0 };
    private int[] deCountyColour = new int[65]
    {
      3,
      1,
      2,
      2,
      0,
      1,
      2,
      3,
      1,
      2,
      0,
      3,
      0,
      2,
      1,
      0,
      3,
      0,
      2,
      3,
      0,
      1,
      3,
      2,
      1,
      0,
      1,
      2,
      0,
      1,
      2,
      2,
      3,
      0,
      3,
      2,
      1,
      1,
      2,
      0,
      0,
      3,
      1,
      3,
      2,
      1,
      2,
      3,
      0,
      3,
      0,
      3,
      1,
      2,
      0,
      1,
      2,
      0,
      3,
      0,
      2,
      1,
      1,
      0,
      3
    };
    private int[] deProvinceColour = new int[15]
    {
      0,
      1,
      2,
      2,
      0,
      1,
      1,
      3,
      2,
      0,
      3,
      1,
      0,
      1,
      0
    };
    private int[] deCountryColour = new int[4]{ 0, 1, 2, 1 };
    private int[] frCountyColour = new int[98]
    {
      2,
      1,
      0,
      1,
      3,
      0,
      1,
      0,
      2,
      3,
      2,
      3,
      3,
      2,
      0,
      1,
      0,
      3,
      2,
      0,
      1,
      1,
      0,
      3,
      0,
      2,
      0,
      3,
      1,
      2,
      2,
      0,
      3,
      0,
      3,
      0,
      3,
      2,
      0,
      3,
      1,
      0,
      0,
      2,
      1,
      2,
      3,
      1,
      2,
      3,
      1,
      0,
      0,
      2,
      3,
      0,
      2,
      0,
      1,
      3,
      0,
      1,
      0,
      2,
      3,
      1,
      0,
      3,
      1,
      1,
      2,
      1,
      3,
      2,
      0,
      1,
      3,
      1,
      0,
      2,
      0,
      2,
      1,
      0,
      2,
      2,
      3,
      1,
      2,
      3,
      1,
      1,
      0,
      1,
      0,
      2,
      3,
      0
    };
    private int[] frProvinceColour = new int[18]
    {
      0,
      1,
      1,
      2,
      0,
      1,
      1,
      0,
      1,
      0,
      2,
      3,
      1,
      2,
      0,
      3,
      1,
      2
    };
    private int[] frCountryColour = new int[7]
    {
      0,
      1,
      2,
      1,
      0,
      2,
      0
    };
    private int[] ruCountyColour = new int[104]
    {
      0,
      1,
      0,
      2,
      1,
      2,
      0,
      1,
      3,
      0,
      2,
      3,
      0,
      1,
      1,
      3,
      0,
      3,
      0,
      1,
      2,
      0,
      1,
      3,
      0,
      2,
      3,
      0,
      1,
      3,
      2,
      0,
      3,
      1,
      1,
      3,
      2,
      3,
      0,
      1,
      0,
      2,
      2,
      1,
      2,
      0,
      3,
      2,
      0,
      2,
      1,
      0,
      2,
      0,
      1,
      3,
      0,
      2,
      0,
      1,
      3,
      1,
      0,
      3,
      1,
      0,
      2,
      1,
      2,
      2,
      2,
      0,
      2,
      3,
      1,
      3,
      0,
      2,
      1,
      0,
      2,
      3,
      1,
      3,
      0,
      1,
      2,
      3,
      0,
      3,
      2,
      3,
      1,
      2,
      1,
      3,
      0,
      2,
      1,
      0,
      2,
      0,
      1,
      2
    };
    private int[] ruProvinceColour = new int[20]
    {
      0,
      1,
      1,
      0,
      2,
      3,
      2,
      0,
      3,
      1,
      2,
      0,
      1,
      0,
      1,
      2,
      2,
      1,
      2,
      0
    };
    private int[] ruCountryColour = new int[7]
    {
      0,
      1,
      2,
      1,
      1,
      1,
      0
    };
    private int[] esCountyColour = new int[68]
    {
      0,
      1,
      2,
      0,
      0,
      2,
      1,
      0,
      1,
      2,
      0,
      3,
      2,
      0,
      2,
      2,
      0,
      1,
      3,
      0,
      2,
      0,
      1,
      1,
      2,
      0,
      2,
      0,
      3,
      2,
      3,
      0,
      2,
      3,
      1,
      0,
      1,
      1,
      3,
      2,
      1,
      0,
      2,
      0,
      3,
      1,
      0,
      0,
      0,
      0,
      0,
      1,
      2,
      3,
      0,
      3,
      1,
      0,
      3,
      1,
      0,
      1,
      2,
      3,
      1,
      3,
      0,
      2
    };
    private int[] esProvinceColour = new int[17]
    {
      0,
      1,
      2,
      0,
      1,
      0,
      2,
      0,
      3,
      1,
      0,
      3,
      2,
      0,
      2,
      1,
      3
    };
    private int[] esCountryColour = new int[2]{ 0, 1 };
    private int[] euCountyColour = new int[197]
    {
      2,
      3,
      0,
      1,
      1,
      0,
      3,
      0,
      3,
      1,
      0,
      2,
      1,
      0,
      2,
      3,
      1,
      0,
      2,
      3,
      1,
      2,
      3,
      1,
      2,
      3,
      1,
      2,
      1,
      3,
      3,
      2,
      1,
      3,
      0,
      1,
      3,
      1,
      0,
      0,
      2,
      3,
      1,
      2,
      3,
      1,
      2,
      0,
      1,
      3,
      2,
      0,
      2,
      0,
      1,
      0,
      2,
      1,
      2,
      3,
      1,
      0,
      2,
      1,
      3,
      1,
      2,
      3,
      0,
      2,
      1,
      0,
      2,
      0,
      0,
      1,
      3,
      2,
      3,
      1,
      3,
      2,
      0,
      1,
      0,
      1,
      1,
      2,
      2,
      1,
      0,
      2,
      3,
      0,
      1,
      3,
      2,
      1,
      0,
      3,
      1,
      1,
      2,
      0,
      0,
      3,
      0,
      1,
      1,
      2,
      0,
      3,
      2,
      3,
      1,
      2,
      3,
      0,
      3,
      2,
      1,
      1,
      2,
      1,
      0,
      3,
      2,
      0,
      0,
      3,
      1,
      0,
      3,
      1,
      2,
      1,
      0,
      2,
      0,
      3,
      1,
      1,
      2,
      0,
      3,
      3,
      0,
      1,
      3,
      2,
      0,
      0,
      1,
      2,
      3,
      0,
      1,
      2,
      0,
      3,
      2,
      0,
      3,
      3,
      2,
      2,
      0,
      1,
      2,
      0,
      3,
      1,
      3,
      0,
      2,
      1,
      2,
      1,
      2,
      3,
      2,
      0,
      1,
      3,
      2,
      3,
      0,
      1,
      0,
      1,
      1,
      2,
      2,
      2,
      3,
      0,
      2
    };
    private int[] euProvinceColour = new int[86]
    {
      0,
      1,
      0,
      1,
      0,
      1,
      2,
      0,
      0,
      1,
      2,
      0,
      3,
      1,
      0,
      2,
      0,
      1,
      0,
      2,
      1,
      0,
      2,
      3,
      0,
      1,
      2,
      3,
      0,
      2,
      0,
      0,
      0,
      1,
      2,
      1,
      0,
      0,
      2,
      1,
      3,
      1,
      2,
      0,
      0,
      3,
      1,
      1,
      2,
      3,
      1,
      2,
      0,
      2,
      1,
      0,
      0,
      3,
      0,
      2,
      0,
      3,
      2,
      1,
      0,
      2,
      3,
      2,
      1,
      0,
      1,
      1,
      2,
      0,
      2,
      2,
      0,
      0,
      1,
      1,
      0,
      2,
      1,
      2,
      1,
      0
    };
    private int[] euCountryColour = new int[33]
    {
      0,
      0,
      0,
      0,
      1,
      0,
      1,
      0,
      2,
      0,
      1,
      2,
      0,
      1,
      2,
      3,
      1,
      0,
      1,
      2,
      0,
      1,
      3,
      0,
      3,
      0,
      2,
      1,
      0,
      3,
      2,
      1,
      1
    };
    private int[] itCountyColour = new int[201]
    {
      0,
      0,
      3,
      1,
      0,
      1,
      2,
      0,
      3,
      0,
      3,
      1,
      1,
      0,
      2,
      3,
      2,
      2,
      2,
      3,
      0,
      1,
      0,
      3,
      2,
      1,
      1,
      3,
      0,
      0,
      2,
      3,
      1,
      1,
      0,
      0,
      3,
      2,
      1,
      0,
      1,
      3,
      3,
      2,
      0,
      2,
      2,
      3,
      2,
      0,
      1,
      1,
      1,
      0,
      1,
      2,
      0,
      3,
      2,
      0,
      0,
      1,
      1,
      2,
      0,
      1,
      2,
      0,
      3,
      2,
      1,
      2,
      0,
      1,
      3,
      1,
      2,
      0,
      1,
      2,
      1,
      0,
      1,
      2,
      0,
      0,
      2,
      1,
      0,
      2,
      1,
      0,
      2,
      1,
      0,
      0,
      2,
      0,
      1,
      1,
      0,
      0,
      0,
      2,
      1,
      0,
      2,
      1,
      0,
      2,
      0,
      2,
      1,
      0,
      3,
      2,
      0,
      2,
      1,
      0,
      1,
      3,
      1,
      2,
      0,
      2,
      1,
      2,
      0,
      2,
      1,
      2,
      1,
      0,
      0,
      1,
      0,
      3,
      0,
      1,
      3,
      2,
      2,
      0,
      2,
      1,
      3,
      1,
      0,
      3,
      1,
      1,
      2,
      2,
      0,
      3,
      0,
      2,
      1,
      0,
      1,
      2,
      0,
      3,
      1,
      0,
      2,
      1,
      2,
      1,
      2,
      0,
      1,
      0,
      3,
      0,
      1,
      2,
      3,
      0,
      3,
      2,
      3,
      0,
      1,
      3,
      2,
      0,
      3,
      0,
      2,
      1,
      3,
      0,
      2,
      0,
      1,
      2,
      1,
      0,
      2
    };
    private int[] itProvinceColour = new int[38]
    {
      0,
      1,
      0,
      2,
      0,
      3,
      1,
      0,
      2,
      3,
      1,
      0,
      1,
      0,
      2,
      1,
      0,
      0,
      1,
      0,
      1,
      2,
      0,
      2,
      0,
      1,
      3,
      0,
      1,
      0,
      2,
      1,
      0,
      2,
      1,
      2,
      0,
      0
    };
    private int[] itCountryColour = new int[8]
    {
      0,
      1,
      0,
      1,
      2,
      3,
      2,
      0
    };
    private int[] plCountyColour = new int[52]
    {
      0,
      1,
      2,
      1,
      2,
      0,
      3,
      0,
      1,
      3,
      0,
      2,
      1,
      0,
      3,
      0,
      1,
      2,
      1,
      0,
      2,
      2,
      0,
      1,
      2,
      0,
      2,
      1,
      0,
      0,
      3,
      2,
      1,
      3,
      2,
      2,
      0,
      1,
      0,
      3,
      3,
      1,
      2,
      0,
      1,
      3,
      2,
      1,
      2,
      1,
      0,
      3
    };
    private int[] plProvinceColour = new int[16]
    {
      0,
      1,
      2,
      1,
      0,
      1,
      3,
      0,
      3,
      0,
      1,
      2,
      0,
      3,
      0,
      3
    };
    private int[] plCountryColour = new int[1];
    private int[] saCountyColour = new int[139]
    {
      0,
      1,
      1,
      2,
      2,
      0,
      0,
      3,
      1,
      1,
      2,
      0,
      1,
      0,
      2,
      0,
      1,
      1,
      0,
      2,
      0,
      1,
      3,
      0,
      1,
      2,
      2,
      3,
      0,
      1,
      2,
      3,
      0,
      0,
      1,
      2,
      2,
      0,
      1,
      0,
      2,
      3,
      1,
      0,
      2,
      1,
      2,
      3,
      0,
      2,
      2,
      3,
      0,
      2,
      1,
      0,
      1,
      0,
      3,
      0,
      0,
      2,
      1,
      3,
      0,
      1,
      2,
      1,
      0,
      2,
      3,
      3,
      0,
      3,
      0,
      1,
      3,
      0,
      3,
      2,
      1,
      2,
      0,
      1,
      3,
      2,
      0,
      1,
      0,
      2,
      2,
      0,
      1,
      3,
      0,
      1,
      2,
      0,
      1,
      2,
      3,
      1,
      0,
      0,
      3,
      2,
      1,
      2,
      1,
      0,
      0,
      0,
      2,
      0,
      2,
      0,
      2,
      0,
      2,
      1,
      0,
      0,
      0,
      0,
      1,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0
    };
    private int[] saProvinceColour = new int[21]
    {
      0,
      1,
      0,
      1,
      0,
      1,
      0,
      1,
      0,
      1,
      2,
      0,
      1,
      3,
      0,
      2,
      0,
      3,
      2,
      0,
      1
    };
    private int[] saCountryColour = new int[5]
    {
      0,
      1,
      0,
      2,
      0
    };
    private int[] trCountyColour = new int[81]
    {
      2,
      3,
      1,
      2,
      1,
      3,
      2,
      3,
      0,
      3,
      1,
      2,
      1,
      2,
      0,
      3,
      0,
      2,
      1,
      0,
      3,
      2,
      3,
      0,
      1,
      1,
      3,
      2,
      3,
      0,
      1,
      0,
      3,
      0,
      1,
      1,
      2,
      3,
      0,
      0,
      1,
      3,
      0,
      1,
      2,
      3,
      1,
      0,
      2,
      1,
      3,
      1,
      0,
      2,
      1,
      3,
      0,
      2,
      0,
      2,
      3,
      1,
      2,
      0,
      1,
      0,
      2,
      3,
      2,
      0,
      1,
      3,
      0,
      2,
      1,
      3,
      2,
      0,
      3,
      0,
      0
    };
    private int[] trProvinceColour = new int[7]
    {
      0,
      1,
      0,
      2,
      1,
      3,
      1
    };
    private int[] trCountryColour = new int[1];
    private int[] uk2CountyColour = new int[114]
    {
      0,
      1,
      2,
      1,
      2,
      1,
      0,
      1,
      0,
      0,
      2,
      0,
      3,
      1,
      3,
      2,
      1,
      0,
      2,
      0,
      1,
      0,
      2,
      3,
      1,
      3,
      0,
      1,
      0,
      0,
      1,
      3,
      1,
      0,
      0,
      0,
      1,
      2,
      3,
      2,
      1,
      0,
      3,
      0,
      3,
      2,
      1,
      3,
      0,
      1,
      0,
      0,
      0,
      2,
      1,
      3,
      0,
      1,
      3,
      2,
      0,
      3,
      1,
      0,
      1,
      0,
      3,
      0,
      3,
      1,
      0,
      1,
      1,
      2,
      0,
      1,
      0,
      0,
      0,
      0,
      0,
      0,
      2,
      1,
      0,
      2,
      0,
      1,
      2,
      3,
      1,
      2,
      0,
      3,
      2,
      3,
      3,
      1,
      0,
      3,
      0,
      2,
      0,
      1,
      2,
      0,
      1,
      0,
      1,
      3,
      2,
      3,
      2,
      0
    };
    private int[] uk2ProvinceColour = new int[28]
    {
      0,
      0,
      2,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      1,
      0,
      1,
      0,
      1,
      2,
      2,
      0,
      1,
      0,
      2,
      0,
      2,
      1,
      1,
      1,
      1,
      0
    };
    private int[] uk2CountryColour = new int[14]
    {
      0,
      1,
      0,
      0,
      0,
      1,
      2,
      1,
      0,
      1,
      2,
      0,
      0,
      2
    };
    private int[] usCountyColour = new int[101]
    {
      0,
      1,
      2,
      1,
      0,
      1,
      0,
      0,
      2,
      2,
      0,
      1,
      0,
      2,
      0,
      1,
      2,
      3,
      0,
      0,
      1,
      2,
      1,
      3,
      0,
      3,
      0,
      1,
      0,
      1,
      0,
      2,
      1,
      1,
      2,
      0,
      1,
      0,
      0,
      1,
      2,
      0,
      3,
      0,
      2,
      3,
      2,
      3,
      0,
      1,
      2,
      1,
      2,
      0,
      1,
      2,
      3,
      2,
      1,
      2,
      0,
      1,
      3,
      2,
      0,
      2,
      3,
      0,
      1,
      2,
      1,
      0,
      2,
      2,
      1,
      3,
      1,
      2,
      0,
      3,
      0,
      1,
      0,
      1,
      2,
      3,
      0,
      1,
      1,
      3,
      0,
      3,
      1,
      0,
      2,
      0,
      1,
      2,
      0,
      2,
      1
    };
    private int[] usProvinceColour = new int[42]
    {
      0,
      1,
      1,
      0,
      2,
      2,
      1,
      0,
      2,
      1,
      0,
      2,
      3,
      0,
      3,
      1,
      0,
      0,
      3,
      1,
      0,
      3,
      0,
      2,
      1,
      2,
      1,
      2,
      1,
      0,
      3,
      3,
      1,
      0,
      1,
      2,
      3,
      2,
      0,
      3,
      0,
      1
    };
    private int[] usCountryColour = new int[11]
    {
      0,
      1,
      2,
      0,
      0,
      1,
      2,
      1,
      0,
      1,
      2
    };
    private int[] gcCountyColour = new int[251]
    {
      2,
      2,
      1,
      0,
      3,
      0,
      1,
      0,
      1,
      2,
      0,
      3,
      0,
      3,
      1,
      2,
      0,
      0,
      0,
      0,
      1,
      0,
      2,
      0,
      0,
      1,
      0,
      2,
      0,
      3,
      0,
      1,
      1,
      3,
      2,
      1,
      3,
      2,
      0,
      0,
      1,
      2,
      3,
      2,
      0,
      2,
      0,
      1,
      2,
      1,
      2,
      0,
      3,
      0,
      3,
      2,
      2,
      1,
      0,
      3,
      0,
      0,
      3,
      0,
      3,
      3,
      0,
      1,
      1,
      3,
      2,
      1,
      0,
      0,
      2,
      0,
      2,
      1,
      1,
      1,
      0,
      1,
      0,
      2,
      3,
      0,
      2,
      1,
      1,
      1,
      2,
      3,
      2,
      0,
      3,
      2,
      0,
      2,
      0,
      2,
      3,
      1,
      0,
      1,
      1,
      3,
      0,
      3,
      1,
      3,
      0,
      3,
      1,
      3,
      2,
      0,
      2,
      0,
      1,
      2,
      0,
      3,
      0,
      1,
      2,
      2,
      0,
      2,
      1,
      0,
      1,
      1,
      1,
      0,
      1,
      0,
      0,
      1,
      0,
      0,
      0,
      1,
      2,
      1,
      0,
      1,
      0,
      2,
      1,
      2,
      1,
      1,
      2,
      3,
      0,
      1,
      3,
      2,
      0,
      1,
      0,
      1,
      2,
      1,
      2,
      2,
      0,
      2,
      0,
      2,
      1,
      3,
      0,
      0,
      2,
      0,
      1,
      2,
      3,
      2,
      1,
      1,
      2,
      0,
      0,
      3,
      2,
      0,
      1,
      3,
      1,
      2,
      0,
      1,
      3,
      1,
      2,
      1,
      0,
      2,
      0,
      1,
      1,
      0,
      2,
      3,
      2,
      1,
      2,
      3,
      2,
      1,
      0,
      1,
      2,
      1,
      2,
      1,
      0,
      3,
      3,
      0,
      1,
      1,
      1,
      1,
      0,
      2,
      0,
      3,
      3,
      0,
      3,
      1,
      3,
      0,
      2,
      2,
      1,
      0,
      2,
      3,
      0,
      2,
      1,
      0,
      1,
      3,
      0,
      0,
      0
    };
    private int[] gcProvinceColour = new int[151]
    {
      1,
      0,
      0,
      1,
      3,
      2,
      1,
      0,
      3,
      0,
      3,
      1,
      2,
      3,
      2,
      0,
      2,
      1,
      0,
      2,
      1,
      2,
      3,
      1,
      0,
      1,
      2,
      3,
      0,
      1,
      0,
      1,
      2,
      3,
      3,
      1,
      0,
      2,
      1,
      1,
      3,
      2,
      0,
      3,
      1,
      2,
      0,
      2,
      1,
      3,
      0,
      1,
      3,
      1,
      0,
      1,
      1,
      2,
      0,
      2,
      1,
      0,
      1,
      3,
      1,
      1,
      1,
      0,
      1,
      0,
      1,
      0,
      1,
      1,
      0,
      3,
      0,
      3,
      2,
      3,
      0,
      1,
      2,
      0,
      1,
      2,
      0,
      1,
      2,
      0,
      3,
      2,
      3,
      1,
      2,
      1,
      0,
      2,
      3,
      1,
      0,
      1,
      0,
      1,
      0,
      3,
      2,
      1,
      0,
      0,
      2,
      0,
      3,
      2,
      1,
      2,
      1,
      1,
      1,
      2,
      1,
      3,
      1,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      1,
      0,
      2,
      0,
      1,
      2,
      1,
      2,
      3,
      0,
      1,
      1,
      2,
      1,
      0,
      3,
      1,
      2,
      0,
      1,
      3
    };
    private int[] gcCountryColour = new int[151]
    {
      1,
      0,
      0,
      1,
      3,
      2,
      1,
      0,
      3,
      0,
      3,
      1,
      2,
      3,
      2,
      0,
      2,
      1,
      0,
      2,
      1,
      2,
      3,
      1,
      0,
      1,
      2,
      3,
      0,
      1,
      0,
      1,
      2,
      3,
      3,
      1,
      0,
      2,
      1,
      1,
      3,
      2,
      0,
      3,
      1,
      2,
      0,
      2,
      1,
      3,
      0,
      1,
      3,
      1,
      0,
      1,
      1,
      2,
      0,
      2,
      1,
      0,
      1,
      3,
      1,
      1,
      1,
      0,
      1,
      0,
      1,
      0,
      1,
      1,
      0,
      3,
      0,
      3,
      2,
      3,
      0,
      1,
      2,
      0,
      1,
      2,
      0,
      1,
      2,
      0,
      3,
      2,
      3,
      1,
      2,
      1,
      0,
      2,
      3,
      1,
      0,
      1,
      0,
      1,
      0,
      3,
      2,
      1,
      0,
      0,
      2,
      0,
      3,
      2,
      1,
      2,
      1,
      1,
      1,
      2,
      1,
      3,
      1,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      1,
      0,
      2,
      0,
      1,
      2,
      1,
      2,
      3,
      0,
      1,
      1,
      2,
      1,
      0,
      3,
      1,
      2,
      0,
      1,
      3
    };
    private int[] phCountyColour = new int[99]
    {
      0,
      1,
      2,
      1,
      3,
      2,
      1,
      3,
      2,
      0,
      3,
      0,
      2,
      0,
      1,
      0,
      3,
      1,
      2,
      3,
      2,
      3,
      0,
      1,
      0,
      3,
      0,
      1,
      0,
      0,
      0,
      1,
      0,
      0,
      0,
      0,
      0,
      1,
      2,
      0,
      0,
      1,
      0,
      0,
      0,
      0,
      2,
      1,
      0,
      2,
      1,
      0,
      1,
      2,
      0,
      2,
      1,
      0,
      1,
      3,
      1,
      0,
      0,
      2,
      0,
      1,
      0,
      2,
      0,
      0,
      1,
      0,
      0,
      0,
      0,
      0,
      0,
      1,
      0,
      2,
      0,
      0,
      0,
      0,
      1,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      1
    };
    private int[] phProvinceColour = new int[18]
    {
      0,
      1,
      2,
      1,
      0,
      2,
      0,
      2,
      0,
      1,
      0,
      3,
      2,
      0,
      3,
      1,
      2,
      1
    };
    private int[] phCountryColour = new int[1];
    private int[] chCountyColour = new int[73]
    {
      0,
      1,
      2,
      0,
      3,
      1,
      0,
      3,
      0,
      0,
      2,
      0,
      2,
      0,
      2,
      1,
      2,
      0,
      0,
      1,
      0,
      3,
      2,
      1,
      2,
      3,
      0,
      1,
      0,
      1,
      3,
      0,
      3,
      1,
      0,
      1,
      2,
      3,
      0,
      1,
      1,
      2,
      0,
      3,
      1,
      0,
      0,
      0,
      0,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      2,
      1,
      3,
      0,
      2,
      0,
      1,
      3,
      2,
      1,
      0,
      2,
      3,
      0,
      2,
      0,
      0
    };
    private int[] chProvinceColour = new int[13]
    {
      0,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      0,
      1,
      2,
      0,
      0
    };
    private int[] chCountryColour = new int[6]
    {
      0,
      1,
      0,
      0,
      1,
      0
    };
    private int[] kgCountyColour = new int[257]
    {
      0,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      1,
      0,
      1,
      2,
      0,
      2,
      1,
      2,
      1,
      2,
      3,
      4,
      0,
      4,
      1,
      2,
      0,
      1,
      0,
      0,
      2,
      3,
      2,
      1,
      0,
      3,
      0,
      2,
      0,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      2,
      0,
      3,
      1,
      0,
      2,
      0,
      2,
      0,
      2,
      1,
      3,
      1,
      3,
      0,
      1,
      2,
      0,
      2,
      0,
      1,
      0,
      0,
      0,
      0,
      3,
      2,
      1,
      3,
      1,
      2,
      0,
      3,
      1,
      0,
      1,
      0,
      0,
      1,
      2,
      0,
      2,
      1,
      2,
      0,
      1,
      0,
      1,
      0,
      0,
      0,
      0,
      0,
      2,
      3,
      1,
      2,
      0,
      1,
      3,
      1,
      0,
      2,
      0,
      1,
      4,
      0,
      1,
      0,
      3,
      4,
      0,
      2,
      1,
      0,
      2,
      0,
      1,
      3,
      0,
      3,
      0,
      1,
      0,
      2,
      4,
      0,
      2,
      3,
      0,
      0,
      3,
      0,
      1,
      2,
      0,
      4,
      0,
      1,
      0,
      1,
      0,
      0,
      1,
      0,
      0,
      0,
      0,
      1,
      0,
      2,
      0,
      0,
      0,
      0,
      1,
      1,
      4,
      1,
      1,
      0,
      0,
      0,
      1,
      0,
      1,
      0,
      1,
      2,
      1,
      2,
      3,
      0,
      1,
      2,
      1,
      0,
      1,
      4,
      1,
      0,
      2,
      1,
      0,
      3,
      2,
      0,
      1,
      0,
      1,
      0,
      4,
      0,
      0,
      0,
      0,
      4,
      1,
      1,
      2,
      1,
      2,
      1,
      3,
      0,
      1,
      2,
      0,
      3,
      2,
      0,
      0,
      1,
      0,
      4,
      0,
      1,
      2,
      0,
      3,
      0,
      1,
      2,
      0,
      2,
      3,
      1,
      4,
      2,
      1,
      0,
      0,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      2,
      1,
      4,
      2,
      3,
      0,
      2,
      0,
      1,
      0,
      1,
      0,
      2,
      0
    };
    private int[] kgProvinceColour = new int[257]
    {
      0,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      1,
      0,
      1,
      2,
      0,
      2,
      1,
      2,
      1,
      2,
      3,
      4,
      0,
      4,
      1,
      2,
      0,
      1,
      0,
      0,
      2,
      3,
      2,
      1,
      0,
      3,
      0,
      2,
      0,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      2,
      0,
      3,
      1,
      0,
      2,
      0,
      2,
      0,
      2,
      1,
      3,
      1,
      3,
      0,
      1,
      2,
      0,
      2,
      0,
      1,
      0,
      0,
      0,
      0,
      3,
      2,
      1,
      3,
      1,
      2,
      0,
      3,
      1,
      0,
      1,
      0,
      0,
      1,
      2,
      0,
      2,
      1,
      2,
      0,
      1,
      0,
      1,
      0,
      0,
      0,
      0,
      0,
      2,
      3,
      1,
      2,
      0,
      1,
      3,
      1,
      0,
      2,
      0,
      1,
      4,
      0,
      1,
      0,
      3,
      4,
      0,
      2,
      1,
      0,
      2,
      0,
      1,
      3,
      0,
      3,
      0,
      1,
      0,
      2,
      4,
      0,
      2,
      3,
      0,
      0,
      3,
      0,
      1,
      2,
      0,
      4,
      0,
      1,
      0,
      1,
      0,
      0,
      1,
      0,
      0,
      0,
      0,
      1,
      0,
      2,
      0,
      0,
      0,
      0,
      1,
      1,
      4,
      1,
      1,
      0,
      0,
      0,
      1,
      0,
      1,
      0,
      1,
      2,
      1,
      2,
      3,
      0,
      1,
      2,
      1,
      0,
      1,
      4,
      1,
      0,
      2,
      1,
      0,
      3,
      2,
      0,
      1,
      0,
      1,
      0,
      4,
      0,
      0,
      0,
      0,
      4,
      1,
      1,
      2,
      1,
      2,
      1,
      3,
      0,
      1,
      2,
      0,
      3,
      2,
      0,
      0,
      1,
      0,
      4,
      0,
      1,
      2,
      0,
      3,
      0,
      1,
      2,
      0,
      2,
      3,
      1,
      4,
      2,
      1,
      0,
      0,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      2,
      1,
      4,
      2,
      3,
      0,
      2,
      0,
      1,
      0,
      1,
      0,
      2,
      0
    };
    private int[] kgCountryColour = new int[257]
    {
      0,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      1,
      0,
      1,
      2,
      0,
      2,
      1,
      2,
      1,
      2,
      3,
      4,
      0,
      4,
      1,
      2,
      0,
      1,
      0,
      0,
      2,
      3,
      2,
      1,
      0,
      3,
      0,
      2,
      0,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      2,
      0,
      3,
      1,
      0,
      2,
      0,
      2,
      0,
      2,
      1,
      3,
      1,
      3,
      0,
      1,
      2,
      0,
      2,
      0,
      1,
      0,
      0,
      0,
      0,
      3,
      2,
      1,
      3,
      1,
      2,
      0,
      3,
      1,
      0,
      1,
      0,
      0,
      1,
      2,
      0,
      2,
      1,
      2,
      0,
      1,
      0,
      1,
      0,
      0,
      0,
      0,
      0,
      2,
      3,
      1,
      2,
      0,
      1,
      3,
      1,
      0,
      2,
      0,
      1,
      4,
      0,
      1,
      0,
      3,
      4,
      0,
      2,
      1,
      0,
      2,
      0,
      1,
      3,
      0,
      3,
      0,
      1,
      0,
      2,
      4,
      0,
      2,
      3,
      0,
      0,
      3,
      0,
      1,
      2,
      0,
      4,
      0,
      1,
      0,
      1,
      0,
      0,
      1,
      0,
      0,
      0,
      0,
      1,
      0,
      2,
      0,
      0,
      0,
      0,
      1,
      1,
      4,
      1,
      1,
      0,
      0,
      0,
      1,
      0,
      1,
      0,
      1,
      2,
      1,
      2,
      3,
      0,
      1,
      2,
      1,
      0,
      1,
      4,
      1,
      0,
      2,
      1,
      0,
      3,
      2,
      0,
      1,
      0,
      1,
      0,
      4,
      0,
      0,
      0,
      0,
      4,
      1,
      1,
      2,
      1,
      2,
      1,
      3,
      0,
      1,
      2,
      0,
      3,
      2,
      0,
      0,
      1,
      0,
      4,
      0,
      1,
      2,
      0,
      3,
      0,
      1,
      2,
      0,
      2,
      3,
      1,
      4,
      2,
      1,
      0,
      0,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      2,
      1,
      4,
      2,
      3,
      0,
      2,
      0,
      1,
      0,
      1,
      0,
      2,
      0
    };
    private int[] jpCountyColour = new int[78]
    {
      0,
      0,
      0,
      1,
      0,
      2,
      1,
      2,
      1,
      0,
      3,
      0,
      3,
      1,
      0,
      0,
      2,
      1,
      2,
      2,
      1,
      0,
      3,
      2,
      3,
      1,
      0,
      2,
      1,
      1,
      0,
      2,
      0,
      0,
      1,
      2,
      1,
      0,
      2,
      0,
      0,
      1,
      0,
      2,
      1,
      0,
      1,
      0,
      0,
      0,
      2,
      0,
      1,
      0,
      1,
      1,
      2,
      1,
      0,
      3,
      2,
      1,
      0,
      0,
      2,
      0,
      2,
      3,
      1,
      0,
      1,
      0,
      2,
      1,
      0,
      3,
      1,
      0
    };
    private int[] jpProvinceColour = new int[12]
    {
      0,
      1,
      2,
      0,
      1,
      0,
      2,
      1,
      0,
      0,
      0,
      0
    };
    private int[] jpCountryColour = new int[12]
    {
      0,
      1,
      2,
      0,
      1,
      0,
      2,
      1,
      0,
      0,
      0,
      0
    };
    private int[] hywCountyColour = new int[222]
    {
      0,
      2,
      1,
      2,
      2,
      0,
      0,
      2,
      0,
      2,
      1,
      0,
      1,
      3,
      2,
      3,
      1,
      3,
      1,
      2,
      0,
      2,
      0,
      1,
      0,
      3,
      0,
      3,
      1,
      3,
      2,
      0,
      3,
      3,
      2,
      0,
      1,
      3,
      2,
      0,
      1,
      1,
      0,
      2,
      3,
      2,
      1,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      3,
      1,
      0,
      1,
      2,
      0,
      1,
      2,
      1,
      2,
      0,
      2,
      1,
      3,
      0,
      2,
      0,
      2,
      3,
      0,
      1,
      3,
      0,
      1,
      0,
      2,
      0,
      2,
      0,
      2,
      1,
      0,
      0,
      1,
      0,
      2,
      1,
      2,
      3,
      1,
      3,
      2,
      3,
      1,
      0,
      3,
      2,
      1,
      2,
      1,
      0,
      2,
      3,
      0,
      3,
      2,
      1,
      1,
      2,
      0,
      2,
      3,
      0,
      1,
      0,
      2,
      0,
      2,
      0,
      2,
      2,
      3,
      3,
      0,
      1,
      0,
      3,
      0,
      2,
      0,
      1,
      3,
      0,
      1,
      0,
      1,
      3,
      2,
      1,
      2,
      1,
      0,
      0,
      3,
      0,
      2,
      0,
      3,
      1,
      0,
      1,
      3,
      0,
      1,
      2,
      0,
      3,
      0,
      1,
      0,
      3,
      2,
      0,
      2,
      3,
      1,
      0,
      3,
      0,
      1,
      2,
      0,
      0,
      1,
      0,
      2,
      0,
      2,
      3,
      0,
      1,
      2,
      0,
      1,
      2,
      0,
      2,
      1,
      0,
      0,
      3,
      1,
      2,
      1,
      3,
      1,
      2,
      0,
      3,
      1,
      0,
      0,
      1,
      3,
      0,
      2,
      1,
      0,
      2,
      1,
      2,
      1,
      0,
      0,
      0,
      0,
      1,
      1
    };
    private int[] hywProvinceColour = new int[63]
    {
      0,
      1,
      2,
      3,
      1,
      2,
      3,
      0,
      2,
      1,
      2,
      1,
      3,
      0,
      3,
      0,
      1,
      2,
      1,
      0,
      0,
      2,
      0,
      2,
      0,
      3,
      0,
      1,
      1,
      3,
      0,
      2,
      0,
      0,
      3,
      0,
      1,
      0,
      2,
      1,
      3,
      2,
      3,
      0,
      3,
      2,
      1,
      2,
      0,
      2,
      1,
      0,
      0,
      1,
      0,
      1,
      2,
      1,
      0,
      2,
      0,
      0,
      1
    };
    private int[] hywCountryColour = new int[38]
    {
      0,
      1,
      0,
      2,
      1,
      0,
      1,
      0,
      2,
      1,
      0,
      2,
      0,
      1,
      0,
      2,
      3,
      1,
      2,
      0,
      1,
      2,
      0,
      1,
      0,
      3,
      1,
      2,
      3,
      0,
      1,
      2,
      1,
      0,
      2,
      1,
      0,
      0
    };
    private int[] vkCountyColour = new int[67]
    {
      0,
      0,
      1,
      0,
      2,
      1,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      1,
      0,
      1,
      0,
      0,
      1,
      2,
      0,
      0,
      2,
      0,
      0,
      0,
      0,
      0,
      0,
      1,
      0,
      0,
      0,
      1,
      0,
      1,
      0,
      2,
      0,
      2,
      1,
      0,
      2,
      1,
      0,
      1,
      0,
      0,
      1,
      0,
      0,
      1,
      0,
      2,
      1,
      2,
      0,
      2,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0
    };
    private int[] vkProvinceColour = new int[51]
    {
      0,
      0,
      1,
      0,
      2,
      1,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      1,
      0,
      1,
      0,
      0,
      1,
      2,
      0,
      0,
      2,
      0,
      1,
      0,
      0,
      0,
      0,
      1,
      0,
      2,
      0,
      1,
      2,
      0,
      1,
      0,
      1,
      2,
      1,
      0,
      0,
      2,
      1,
      2,
      0,
      2,
      0,
      1
    };
    private int[] vkCountryColour = new int[48]
    {
      0,
      1,
      2,
      1,
      0,
      1,
      0,
      0,
      1,
      0,
      0,
      0,
      0,
      1,
      0,
      0,
      1,
      1,
      0,
      0,
      1,
      0,
      0,
      0,
      0,
      0,
      0,
      1,
      0,
      2,
      0,
      1,
      2,
      0,
      1,
      0,
      1,
      2,
      1,
      0,
      0,
      2,
      1,
      2,
      0,
      2,
      0,
      1
    };
    private int[] tgdCountyColour = new int[83]
    {
      0,
      1,
      2,
      1,
      2,
      0,
      1,
      0,
      3,
      0,
      1,
      3,
      1,
      0,
      2,
      0,
      1,
      0,
      2,
      3,
      0,
      2,
      1,
      2,
      1,
      0,
      2,
      2,
      0,
      1,
      0,
      1,
      0,
      1,
      3,
      2,
      3,
      1,
      0,
      1,
      2,
      0,
      2,
      4,
      1,
      3,
      0,
      2,
      3,
      2,
      0,
      2,
      1,
      1,
      0,
      1,
      0,
      1,
      2,
      3,
      0,
      0,
      1,
      2,
      1,
      0,
      3,
      0,
      2,
      0,
      2,
      1,
      0,
      1,
      0,
      2,
      0,
      0,
      0,
      0,
      0,
      1,
      0
    };
    private int[] tgdProvinceColour = new int[20]
    {
      0,
      1,
      1,
      2,
      0,
      1,
      2,
      1,
      3,
      0,
      0,
      0,
      2,
      0,
      3,
      2,
      3,
      1,
      0,
      1
    };
    private int[] tgdCountryColour = new int[10]
    {
      0,
      1,
      2,
      3,
      0,
      3,
      1,
      0,
      1,
      0
    };
    private int[] cruCountyColour = new int[67]
    {
      0,
      1,
      1,
      1,
      0,
      1,
      2,
      1,
      4,
      0,
      4,
      0,
      1,
      0,
      4,
      2,
      1,
      3,
      0,
      3,
      2,
      0,
      4,
      1,
      0,
      3,
      1,
      0,
      2,
      3,
      0,
      2,
      1,
      0,
      1,
      0,
      2,
      1,
      2,
      1,
      0,
      1,
      3,
      0,
      1,
      2,
      1,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      2,
      3,
      2,
      1,
      0,
      1,
      2,
      1,
      2,
      3,
      0,
      2,
      0
    };
    private int[] cruProvinceColour = new int[67]
    {
      0,
      1,
      1,
      1,
      0,
      1,
      2,
      1,
      4,
      0,
      4,
      0,
      1,
      0,
      4,
      2,
      1,
      3,
      0,
      3,
      2,
      0,
      4,
      1,
      0,
      3,
      1,
      0,
      2,
      3,
      0,
      2,
      1,
      0,
      1,
      0,
      2,
      1,
      2,
      1,
      0,
      1,
      3,
      0,
      1,
      2,
      1,
      1,
      0,
      1,
      2,
      0,
      1,
      0,
      2,
      3,
      2,
      1,
      0,
      1,
      2,
      1,
      2,
      3,
      0,
      2,
      0
    };
    private int[] cruCountryColour = new int[21]
    {
      0,
      0,
      0,
      1,
      2,
      1,
      0,
      1,
      2,
      2,
      1,
      0,
      3,
      4,
      0,
      1,
      2,
      1,
      4,
      0,
      2
    };
    private int[] spartaCountyColour = new int[102]
    {
      0,
      1,
      1,
      0,
      0,
      2,
      0,
      0,
      0,
      0,
      0,
      1,
      1,
      0,
      3,
      3,
      1,
      0,
      0,
      0,
      0,
      1,
      0,
      2,
      1,
      0,
      0,
      1,
      1,
      0,
      0,
      1,
      1,
      0,
      0,
      1,
      0,
      0,
      0,
      2,
      0,
      0,
      1,
      0,
      0,
      1,
      1,
      1,
      1,
      1,
      2,
      0,
      1,
      2,
      0,
      2,
      0,
      0,
      1,
      3,
      1,
      0,
      0,
      0,
      2,
      3,
      3,
      1,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0
    };
    private int[] spartaProvinceColour = new int[31]
    {
      0,
      1,
      1,
      1,
      0,
      0,
      0,
      0,
      0,
      1,
      2,
      0,
      1,
      0,
      3,
      1,
      0,
      2,
      0,
      0,
      1,
      0,
      2,
      2,
      1,
      0,
      0,
      0,
      0,
      1,
      0
    };
    private int[] spartaCountryColour = new int[11]
    {
      0,
      1,
      0,
      0,
      0,
      1,
      2,
      0,
      1,
      0,
      0
    };
    private SparseArray leaderboard_Main = new SparseArray();
    private SparseArray leaderboard_MainRank = new SparseArray();
    private SparseArray leaderboard_MainVillages = new SparseArray();
    private SparseArray leaderboard_Factions = new SparseArray();
    private SparseArray leaderboard_Houses = new SparseArray();
    private SparseArray leaderboard_ParishFlags = new SparseArray();
    private SparseArray leaderboard_Sub_Pillager = new SparseArray();
    private SparseArray leaderboard_Sub_Defender = new SparseArray();
    private SparseArray leaderboard_Sub_Ransack = new SparseArray();
    private SparseArray leaderboard_Sub_Wolfsbane = new SparseArray();
    private SparseArray leaderboard_Sub_Banditkiller = new SparseArray();
    private SparseArray leaderboard_Sub_AIKiller = new SparseArray();
    private SparseArray leaderboard_Sub_Trader = new SparseArray();
    private SparseArray leaderboard_Sub_Forager = new SparseArray();
    private SparseArray leaderboard_Sub_Stockpiler = new SparseArray();
    private SparseArray leaderboard_Sub_Farmer = new SparseArray();
    private SparseArray leaderboard_Sub_Brewer = new SparseArray();
    private SparseArray leaderboard_Sub_Weaponsmith = new SparseArray();
    private SparseArray leaderboard_Sub_banquetter = new SparseArray();
    private SparseArray leaderboard_Sub_Achiever = new SparseArray();
    private SparseArray leaderboard_Sub_Donater = new SparseArray();
    private SparseArray leaderboard_Sub_Capture = new SparseArray();
    private SparseArray leaderboard_Sub_Raze = new SparseArray();
    private SparseArray leaderboard_Sub_Glory = new SparseArray();
    private SparseArray leaderboard_Sub_KillStreak = new SparseArray();
    private int max_leaderboard_Main = -1;
    private int max_leaderboard_MainRank = -1;
    private int max_leaderboard_MainVillages = -1;
    private int max_leaderboard_Factions = -1;
    private int max_leaderboard_Houses = -1;
    private int max_leaderboard_ParishFlags = -1;
    private int max_leaderboard_Sub_Pillager = -1;
    private int max_leaderboard_Sub_Defender = -1;
    private int max_leaderboard_Sub_Ransack = -1;
    private int max_leaderboard_Sub_Wolfsbane = -1;
    private int max_leaderboard_Sub_Banditkiller = -1;
    private int max_leaderboard_Sub_AIKiller = -1;
    private int max_leaderboard_Sub_Trader = -1;
    private int max_leaderboard_Sub_Forager = -1;
    private int max_leaderboard_Sub_Stockpiler = -1;
    private int max_leaderboard_Sub_Farmer = -1;
    private int max_leaderboard_Sub_Brewer = -1;
    private int max_leaderboard_Sub_Weaponsmith = -1;
    private int max_leaderboard_Sub_banquetter = -1;
    private int max_leaderboard_Sub_Achiever = -1;
    private int max_leaderboard_Sub_Donater = -1;
    private int max_leaderboard_Sub_Capture = -1;
    private int max_leaderboard_Sub_Raze = -1;
    private int max_leaderboard_Sub_Glory = -1;
    private int max_leaderboard_Sub_KillStreak = -1;
    private DateTime lastZeroDownload_leaderboard_Main = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_MainRank = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_MainVillages = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Factions = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Houses = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_ParishFlags = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Pillager = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Defender = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Ransack = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Wolfsbane = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Banditkiller = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_AIKiller = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Trader = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Forager = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Stockpiler = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Farmer = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Brewer = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Weaponsmith = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_banquetter = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Achiever = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Donater = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Capture = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Raze = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_Glory = DateTime.MinValue;
    private DateTime lastZeroDownload_leaderboard_Sub_KillStreak = DateTime.MinValue;
    private DateTime leaderboardLastUpdateTime = DateTime.MinValue;
    private List<LeaderBoardSelfRankings> leaderboardSelfRankings = new List<LeaderBoardSelfRankings>();
    private List<LeaderBoardSearchResults> leaderboardSearchResults = new List<LeaderBoardSearchResults>();
    private static LeaderBoardEntryData dummyEntry = (LeaderBoardEntryData) null;
    private bool inDownloading;
    private bool inLeaderboardSearch;
    private WorldMap.LeaderboardSelfRankingsComparer leaderboardSelfRankingsComparer = new WorldMap.LeaderboardSelfRankingsComparer();
    private WorldMap.LeaderboardSelfStaticComparer leaderboardSelfStaticComparer = new WorldMap.LeaderboardSelfStaticComparer();
    private bool dirtyStanding;
    private string[,] china_country_Simplified = new string[6, 2]
    {
      {
        "中国",
        "北京"
      },
      {
        "北朝鲜",
        "平壤"
      },
      {
        "南朝鲜",
        "首尔"
      },
      {
        "日本",
        "东京"
      },
      {
        "蒙古",
        "乌兰巴托"
      },
      {
        "中国台北",
        "台北"
      }
    };
    private string[,] china_country_English = new string[6, 2]
    {
      {
        "China",
        "Beijing"
      },
      {
        "North Korea",
        "Pyongyang"
      },
      {
        "South Korea",
        "Seoul"
      },
      {
        "Japan",
        "Tokyo"
      },
      {
        "Mongolia",
        "Ulaanbaatar"
      },
      {
        "Taiwan, Province of China",
        "Taipei"
      }
    };
    private string[,] china_country_Korean = new string[6, 2]
    {
      {
        "China",
        "Beijing"
      },
      {
        "North Korea",
        "Pyongyang"
      },
      {
        "South Korea",
        "Seoul"
      },
      {
        "Japan",
        "Tokyo"
      },
      {
        "Mongolia",
        "Ulaanbaatar"
      },
      {
        "Taiwan, Province of China",
        "Taipei"
      }
    };
    private string[,] china_country_Japanese = new string[6, 2]
    {
      {
        "China",
        "Beijing"
      },
      {
        "North Korea",
        "Pyongyang"
      },
      {
        "South Korea",
        "Seoul"
      },
      {
        "Japan",
        "Tokyo"
      },
      {
        "Mongolia",
        "Ulaanbaatar"
      },
      {
        "Taiwan, Province of China",
        "Taipei"
      }
    };
    private string[,] china_province_Simplified = new string[13, 2]
    {
      {
        "中国西北部",
        "西安"
      },
      {
        "中国北部",
        "石家庄"
      },
      {
        "中国东北部",
        "沈阳"
      },
      {
        "中国西南部",
        "南宁"
      },
      {
        "中国中部",
        "重庆"
      },
      {
        "中国东南部",
        "上海"
      },
      {
        "北朝鲜",
        "咸兴"
      },
      {
        "南朝鲜",
        "釜山"
      },
      {
        "日本南部",
        "大阪"
      },
      {
        "日本北部",
        "横滨"
      },
      {
        "蒙古西部",
        "木伦"
      },
      {
        "蒙古东部",
        "达尔汗"
      },
      {
        "台湾",
        "高雄"
      }
    };
    private string[,] china_province_English = new string[13, 2]
    {
      {
        "Northwest China",
        "Xi'an"
      },
      {
        "North China",
        "Shijiazhuang"
      },
      {
        "Northeast China",
        "Shenyang"
      },
      {
        "Southwest China",
        "Nanning"
      },
      {
        "Central China",
        "Chongqing"
      },
      {
        "Southeast China",
        "Shanghai"
      },
      {
        "Hamhung",
        "Hamhung"
      },
      {
        "Busan",
        "Busan"
      },
      {
        "South Japan",
        "Osaka"
      },
      {
        "North Japan",
        "Yokohama"
      },
      {
        "West Mongolia",
        "Mörön"
      },
      {
        "East Mongolia",
        "Darkhan"
      },
      {
        "Kaohsiung City",
        "Kaohsiung City"
      }
    };
    private string[,] china_province_Korean = new string[13, 2]
    {
      {
        "Northwest China",
        "Xi'an"
      },
      {
        "North China",
        "Shijiazhuang"
      },
      {
        "Northeast China",
        "Shenyang"
      },
      {
        "Southwest China",
        "Nanning"
      },
      {
        "Central China",
        "Chongqing"
      },
      {
        "Southeast China",
        "Shanghai"
      },
      {
        "Hamhung",
        "Hamhung"
      },
      {
        "Busan",
        "Busan"
      },
      {
        "South Japan",
        "Osaka"
      },
      {
        "North Japan",
        "Yokohama"
      },
      {
        "West Mongolia",
        "Mörön"
      },
      {
        "East Mongolia",
        "Darkhan"
      },
      {
        "Kaohsiung City",
        "Kaohsiung City"
      }
    };
    private string[,] china_province_Japanese = new string[13, 2]
    {
      {
        "Northwest China",
        "Xi'an"
      },
      {
        "North China",
        "Shijiazhuang"
      },
      {
        "Northeast China",
        "Shenyang"
      },
      {
        "Southwest China",
        "Nanning"
      },
      {
        "Central China",
        "Chongqing"
      },
      {
        "Southeast China",
        "Shanghai"
      },
      {
        "Hamhung",
        "Hamhung"
      },
      {
        "Busan",
        "Busan"
      },
      {
        "South Japan",
        "Osaka"
      },
      {
        "North Japan",
        "Yokohama"
      },
      {
        "West Mongolia",
        "Mörön"
      },
      {
        "East Mongolia",
        "Darkhan"
      },
      {
        "Kaohsiung City",
        "Kaohsiung City"
      }
    };
    private string[,] china_county_Simplified = new string[73, 2]
    {
      {
        "新疆",
        "乌鲁木齐"
      },
      {
        "青海",
        "西宁"
      },
      {
        "甘肃",
        "兰州"
      },
      {
        "宁夏",
        "银川"
      },
      {
        "陕西",
        "渭南"
      },
      {
        "内蒙古",
        "呼和浩特"
      },
      {
        "山西",
        "太原"
      },
      {
        "河北",
        "保定"
      },
      {
        "直辖",
        "天津"
      },
      {
        "山东",
        "济南"
      },
      {
        "河南",
        "郑州"
      },
      {
        "黑龙江",
        "哈尔滨"
      },
      {
        "吉林",
        "长春"
      },
      {
        "辽宁",
        "大连"
      },
      {
        "西藏",
        "拉萨"
      },
      {
        "云南",
        "昆明"
      },
      {
        "贵州",
        "贵阳"
      },
      {
        "广西",
        "玉林"
      },
      {
        "四川",
        "成都"
      },
      {
        "重庆市",
        "万州"
      },
      {
        "湖北",
        "武汉"
      },
      {
        "湖南",
        "长沙"
      },
      {
        "江西",
        "南昌"
      },
      {
        "安徽",
        "合肥"
      },
      {
        "江苏",
        "南京"
      },
      {
        "浙江",
        "杭州"
      },
      {
        "福建",
        "福州"
      },
      {
        "广东",
        "广州"
      },
      {
        "海南",
        "海口"
      },
      {
        "平安北道",
        "新义州"
      },
      {
        "慈江道",
        "江界"
      },
      {
        "两江道",
        "惠山"
      },
      {
        "咸镜北道",
        "清津"
      },
      {
        "咸镜南道",
        "新浦"
      },
      {
        "平安南道",
        "平城"
      },
      {
        "黄海南道",
        "海州"
      },
      {
        "黄海北道",
        "沙里院"
      },
      {
        "江原道",
        "元山"
      },
      {
        "京畿道",
        "仁川"
      },
      {
        "江原道",
        "春川"
      },
      {
        "忠清南道",
        "洪城"
      },
      {
        "忠清北道",
        "清州"
      },
      {
        "庆尚北道",
        "大邱"
      },
      {
        "全罗北道",
        "全州"
      },
      {
        "庆尚南道",
        "昌原"
      },
      {
        "全罗南道",
        "光州"
      },
      {
        "九州",
        "福冈"
      },
      {
        "中国地方",
        "广岛市"
      },
      {
        "四国",
        "松山"
      },
      {
        "关西",
        "神户"
      },
      {
        "中部",
        "名古屋"
      },
      {
        "关东",
        "千叶"
      },
      {
        "东北地方",
        "仙台"
      },
      {
        "北海道",
        "札幌"
      },
      {
        "巴彦乌列盖",
        "乌列盖"
      },
      {
        "乌布苏",
        "乌兰固木"
      },
      {
        "科布多",
        "科布多城"
      },
      {
        "扎布汗",
        "乌里雅苏台"
      },
      {
        "戈壁阿尔泰",
        "阿尔泰"
      },
      {
        "巴彦洪戈尔",
        "巴彦洪戈尔县"
      },
      {
        "后杭爱",
        "车车尔勒格"
      },
      {
        "庫苏古尔",
        "塔里亚兰"
      },
      {
        "布尔干",
        "布尔干县"
      },
      {
        "前杭爱",
        "阿尔拜赫雷"
      },
      {
        "南戈壁",
        "达兰扎德嘎德"
      },
      {
        "中戈壁",
        "曼达勒戈壁"
      },
      {
        "中央",
        "宗莫德"
      },
      {
        "色楞格",
        "苏赫巴托尔"
      },
      {
        "肯特",
        "温都尔汗"
      },
      {
        "东戈壁",
        "赛音山达"
      },
      {
        "苏赫巴托尔",
        "西乌尔特"
      },
      {
        "东方",
        "乔巴山"
      },
      {
        "台湾",
        "台中"
      }
    };
    private string[,] china_county_English = new string[73, 2]
    {
      {
        "Xinjiang",
        "Ürümqi"
      },
      {
        "Qinghai",
        "Xining"
      },
      {
        "Gansu",
        "Lanzhou"
      },
      {
        "Ningxia",
        "Yinchuan"
      },
      {
        "Shaanxi",
        "Weinan"
      },
      {
        "Inner Mongolia",
        "Hohhot"
      },
      {
        "Shanxi",
        "Taiyuan"
      },
      {
        "Hebei",
        "Baoding"
      },
      {
        "Capital District",
        "Tianjin"
      },
      {
        "Shandong",
        "Jinan"
      },
      {
        "Henan",
        "Zhengzhou"
      },
      {
        "Heilongjiang",
        "Harbin"
      },
      {
        "Jilin",
        "Changchun"
      },
      {
        "Liaoning",
        "Dalian"
      },
      {
        "Tibet",
        "Lhasa"
      },
      {
        "Yunnan",
        "Kunming"
      },
      {
        "Guizhou",
        "Guiyang"
      },
      {
        "Guangxi",
        "Yulin"
      },
      {
        "Sichuan",
        "Chengdu"
      },
      {
        "Chongqing Municipality",
        "Wanzhou"
      },
      {
        "Hubei",
        "Wuhan"
      },
      {
        "Hunan",
        "Changsha"
      },
      {
        "Jiangxi",
        "Nanchang"
      },
      {
        "Anhui",
        "Hefei"
      },
      {
        "Jiangsu",
        "Nanjing"
      },
      {
        "Zhejiang",
        "Hangzhou"
      },
      {
        "Fujian",
        "Fuzhou"
      },
      {
        "Guangdong",
        "Guangzhou"
      },
      {
        "Hainan",
        "Haikou"
      },
      {
        "Pyongan-bukto",
        "Sinŭiju"
      },
      {
        "Chagang-do",
        "Kanggye"
      },
      {
        "Yanggang-do",
        "Hyesan"
      },
      {
        "Hamgyong-bukto",
        "Chongjin"
      },
      {
        "Hamgyong-namdo",
        "Sinpo"
      },
      {
        "Pyongan-namdo",
        "Pyongsong"
      },
      {
        "Hwanghae-namdo",
        "Haeju"
      },
      {
        "Hwanghe-bukto",
        "Sariwon"
      },
      {
        "Kangwon-do",
        "Wonsan"
      },
      {
        "Gyeonggi",
        "Incheon"
      },
      {
        "Gangwon",
        "Chuncheon"
      },
      {
        "South Chungcheong",
        "Hongseong"
      },
      {
        "North Chungcheong",
        "Cheongju"
      },
      {
        "North Gyeongsang",
        "Daegu"
      },
      {
        "North Jeolla",
        "Jeonju"
      },
      {
        "South Gyeongsang",
        "Changwon"
      },
      {
        "South Jeolla",
        "Gwangju"
      },
      {
        "Kyushu",
        "Fukuoka"
      },
      {
        "Chugoku",
        "Hiroshima"
      },
      {
        "Shikoku",
        "Matsuyama"
      },
      {
        "Kansai",
        "Kobe"
      },
      {
        "Chubu",
        "Nagoya"
      },
      {
        "Kanto",
        "Chiba"
      },
      {
        "Tohoku",
        "Sendai"
      },
      {
        "Hokkaido",
        "Sapporo"
      },
      {
        "Bayan-Ölgii",
        "Ölgii"
      },
      {
        "Uvs",
        "Ulaangom"
      },
      {
        "Khovd",
        "Khovd"
      },
      {
        "Zavkhan",
        "Uliastai"
      },
      {
        "Govi-Altai",
        "Altai"
      },
      {
        "Bayankhongor",
        "Bayankhongor"
      },
      {
        "Arkhangai",
        "Tsetserleg"
      },
      {
        "Khövsgöl",
        "Tarialan"
      },
      {
        "Bulgan",
        "Bulgan"
      },
      {
        "Övörkhangai",
        "Arvaikheer"
      },
      {
        "Ömnögovi",
        "Dalanzadgad"
      },
      {
        "Dundgovi",
        "Mandalgovi"
      },
      {
        "Töv",
        "Zuunmod"
      },
      {
        "Selenge",
        "Sükhbaatar"
      },
      {
        "Khentii",
        "Öndörkhaan"
      },
      {
        "Dornogovi",
        "Sainshand"
      },
      {
        "Sükhbaatar",
        "Baruun-Urt"
      },
      {
        "Dornod",
        "Choibalsan"
      },
      {
        "Taichung City",
        "Taichung City"
      }
    };
    private string[,] china_county_Korean = new string[73, 2]
    {
      {
        "Xinjiang",
        "Ürümqi"
      },
      {
        "Qinghai",
        "Xining"
      },
      {
        "Gansu",
        "Lanzhou"
      },
      {
        "Ningxia",
        "Yinchuan"
      },
      {
        "Shaanxi",
        "Weinan"
      },
      {
        "Inner Mongolia",
        "Hohhot"
      },
      {
        "Shanxi",
        "Taiyuan"
      },
      {
        "Hebei",
        "Baoding"
      },
      {
        "Capital District",
        "Tianjin"
      },
      {
        "Shandong",
        "Jinan"
      },
      {
        "Henan",
        "Zhengzhou"
      },
      {
        "Heilongjiang",
        "Harbin"
      },
      {
        "Jilin",
        "Changchun"
      },
      {
        "Liaoning",
        "Dalian"
      },
      {
        "Tibet",
        "Lhasa"
      },
      {
        "Yunnan",
        "Kunming"
      },
      {
        "Guizhou",
        "Guiyang"
      },
      {
        "Guangxi",
        "Yulin"
      },
      {
        "Sichuan",
        "Chengdu"
      },
      {
        "Chongqing Municipality",
        "Wanzhou"
      },
      {
        "Hubei",
        "Wuhan"
      },
      {
        "Hunan",
        "Changsha"
      },
      {
        "Jiangxi",
        "Nanchang"
      },
      {
        "Anhui",
        "Hefei"
      },
      {
        "Jiangsu",
        "Nanjing"
      },
      {
        "Zhejiang",
        "Hangzhou"
      },
      {
        "Fujian",
        "Fuzhou"
      },
      {
        "Guangdong",
        "Guangzhou"
      },
      {
        "Hainan",
        "Haikou"
      },
      {
        "Pyongan-bukto",
        "Sinŭiju"
      },
      {
        "Chagang-do",
        "Kanggye"
      },
      {
        "Yanggang-do",
        "Hyesan"
      },
      {
        "Hamgyong-bukto",
        "Chongjin"
      },
      {
        "Hamgyong-namdo",
        "Sinpo"
      },
      {
        "Pyongan-namdo",
        "Pyongsong"
      },
      {
        "Hwanghae-namdo",
        "Haeju"
      },
      {
        "Hwanghe-bukto",
        "Sariwon"
      },
      {
        "Kangwon-do",
        "Wonsan"
      },
      {
        "Gyeonggi",
        "Incheon"
      },
      {
        "Gangwon",
        "Chuncheon"
      },
      {
        "South Chungcheong",
        "Hongseong"
      },
      {
        "North Chungcheong",
        "Cheongju"
      },
      {
        "North Gyeongsang",
        "Daegu"
      },
      {
        "North Jeolla",
        "Jeonju"
      },
      {
        "South Gyeongsang",
        "Changwon"
      },
      {
        "South Jeolla",
        "Gwangju"
      },
      {
        "Kyushu",
        "Fukuoka"
      },
      {
        "Chugoku",
        "Hiroshima"
      },
      {
        "Shikoku",
        "Matsuyama"
      },
      {
        "Kansai",
        "Kobe"
      },
      {
        "Chubu",
        "Nagoya"
      },
      {
        "Kanto",
        "Chiba"
      },
      {
        "Tohoku",
        "Sendai"
      },
      {
        "Hokkaido",
        "Sapporo"
      },
      {
        "Bayan-Ölgii",
        "Ölgii"
      },
      {
        "Uvs",
        "Ulaangom"
      },
      {
        "Khovd",
        "Khovd"
      },
      {
        "Zavkhan",
        "Uliastai"
      },
      {
        "Govi-Altai",
        "Altai"
      },
      {
        "Bayankhongor",
        "Bayankhongor"
      },
      {
        "Arkhangai",
        "Tsetserleg"
      },
      {
        "Khövsgöl",
        "Tarialan"
      },
      {
        "Bulgan",
        "Bulgan"
      },
      {
        "Övörkhangai",
        "Arvaikheer"
      },
      {
        "Ömnögovi",
        "Dalanzadgad"
      },
      {
        "Dundgovi",
        "Mandalgovi"
      },
      {
        "Töv",
        "Zuunmod"
      },
      {
        "Selenge",
        "Sükhbaatar"
      },
      {
        "Khentii",
        "Öndörkhaan"
      },
      {
        "Dornogovi",
        "Sainshand"
      },
      {
        "Sükhbaatar",
        "Baruun-Urt"
      },
      {
        "Dornod",
        "Choibalsan"
      },
      {
        "Taichung City",
        "Taichung City"
      }
    };
    private string[,] china_county_Japanese = new string[73, 2]
    {
      {
        "Xinjiang",
        "Ürümqi"
      },
      {
        "Qinghai",
        "Xining"
      },
      {
        "Gansu",
        "Lanzhou"
      },
      {
        "Ningxia",
        "Yinchuan"
      },
      {
        "Shaanxi",
        "Weinan"
      },
      {
        "Inner Mongolia",
        "Hohhot"
      },
      {
        "Shanxi",
        "Taiyuan"
      },
      {
        "Hebei",
        "Baoding"
      },
      {
        "Capital District",
        "Tianjin"
      },
      {
        "Shandong",
        "Jinan"
      },
      {
        "Henan",
        "Zhengzhou"
      },
      {
        "Heilongjiang",
        "Harbin"
      },
      {
        "Jilin",
        "Changchun"
      },
      {
        "Liaoning",
        "Dalian"
      },
      {
        "Tibet",
        "Lhasa"
      },
      {
        "Yunnan",
        "Kunming"
      },
      {
        "Guizhou",
        "Guiyang"
      },
      {
        "Guangxi",
        "Yulin"
      },
      {
        "Sichuan",
        "Chengdu"
      },
      {
        "Chongqing Municipality",
        "Wanzhou"
      },
      {
        "Hubei",
        "Wuhan"
      },
      {
        "Hunan",
        "Changsha"
      },
      {
        "Jiangxi",
        "Nanchang"
      },
      {
        "Anhui",
        "Hefei"
      },
      {
        "Jiangsu",
        "Nanjing"
      },
      {
        "Zhejiang",
        "Hangzhou"
      },
      {
        "Fujian",
        "Fuzhou"
      },
      {
        "Guangdong",
        "Guangzhou"
      },
      {
        "Hainan",
        "Haikou"
      },
      {
        "Pyongan-bukto",
        "Sinŭiju"
      },
      {
        "Chagang-do",
        "Kanggye"
      },
      {
        "Yanggang-do",
        "Hyesan"
      },
      {
        "Hamgyong-bukto",
        "Chongjin"
      },
      {
        "Hamgyong-namdo",
        "Sinpo"
      },
      {
        "Pyongan-namdo",
        "Pyongsong"
      },
      {
        "Hwanghae-namdo",
        "Haeju"
      },
      {
        "Hwanghe-bukto",
        "Sariwon"
      },
      {
        "Kangwon-do",
        "Wonsan"
      },
      {
        "Gyeonggi",
        "Incheon"
      },
      {
        "Gangwon",
        "Chuncheon"
      },
      {
        "South Chungcheong",
        "Hongseong"
      },
      {
        "North Chungcheong",
        "Cheongju"
      },
      {
        "North Gyeongsang",
        "Daegu"
      },
      {
        "North Jeolla",
        "Jeonju"
      },
      {
        "South Gyeongsang",
        "Changwon"
      },
      {
        "South Jeolla",
        "Gwangju"
      },
      {
        "Kyushu",
        "Fukuoka"
      },
      {
        "Chugoku",
        "Hiroshima"
      },
      {
        "Shikoku",
        "Matsuyama"
      },
      {
        "Kansai",
        "Kobe"
      },
      {
        "Chubu",
        "Nagoya"
      },
      {
        "Kanto",
        "Chiba"
      },
      {
        "Tohoku",
        "Sendai"
      },
      {
        "Hokkaido",
        "Sapporo"
      },
      {
        "Bayan-Ölgii",
        "Ölgii"
      },
      {
        "Uvs",
        "Ulaangom"
      },
      {
        "Khovd",
        "Khovd"
      },
      {
        "Zavkhan",
        "Uliastai"
      },
      {
        "Govi-Altai",
        "Altai"
      },
      {
        "Bayankhongor",
        "Bayankhongor"
      },
      {
        "Arkhangai",
        "Tsetserleg"
      },
      {
        "Khövsgöl",
        "Tarialan"
      },
      {
        "Bulgan",
        "Bulgan"
      },
      {
        "Övörkhangai",
        "Arvaikheer"
      },
      {
        "Ömnögovi",
        "Dalanzadgad"
      },
      {
        "Dundgovi",
        "Mandalgovi"
      },
      {
        "Töv",
        "Zuunmod"
      },
      {
        "Selenge",
        "Sükhbaatar"
      },
      {
        "Khentii",
        "Öndörkhaan"
      },
      {
        "Dornogovi",
        "Sainshand"
      },
      {
        "Sükhbaatar",
        "Baruun-Urt"
      },
      {
        "Dornod",
        "Choibalsan"
      },
      {
        "Taichung City",
        "Taichung City"
      }
    };
    private string[] china_parish_Simplified;
    private string[] china_parish_English;

    public static void LogArmyErrorIfUnity(string s)
    {
    }

    public bool checkRecentRetrievalSend(long armyID)
    {
      foreach (WorldMap.ArmyRetrieveData requestedReturnedArmyId in this.requestedReturnedArmyIDs)
      {
        if (requestedReturnedArmyId.armyID == armyID)
          return false;
      }
      return true;
    }

    public void updateArmyRetrievalData()
    {
      if (this.requestedReturnedArmyIDs.Count <= 0)
        return;
      DateTime now = DateTime.Now;
      List<WorldMap.ArmyRetrieveData> armyRetrieveDataList = new List<WorldMap.ArmyRetrieveData>();
      foreach (WorldMap.ArmyRetrieveData requestedReturnedArmyId in this.requestedReturnedArmyIDs)
      {
        if (requestedReturnedArmyId.expiryTime < now)
        {
          if (this.isArmyReallyReturning(requestedReturnedArmyId.armyID))
          {
            armyRetrieveDataList.Add(requestedReturnedArmyId);
          }
          else
          {
            RemoteServices.Instance.RetrieveAttackResult(requestedReturnedArmyId.armyID, GameEngine.Instance.World.StoredVillageFactionPos);
            this.requestedReturnedArmyIDs.Remove(requestedReturnedArmyId);
            break;
          }
        }
      }
      foreach (WorldMap.ArmyRetrieveData armyRetrieveData in armyRetrieveDataList)
        this.requestedReturnedArmyIDs.Remove(armyRetrieveData);
    }

    public void RemoveArmy(long id) => this.armyArray[id] = (object) null;

    public void SetArmy(WorldMap.LocalArmyData army) => this.armyArray[army.armyID] = (object) army;

    public WorldMap.LocalArmyData GetArmyByID(long id)
    {
      return (WorldMap.LocalArmyData) this.armyArray[id];
    }

    public void RemoveReinforcementArmy(long id) => this.reinforcementArray[id] = (object) null;

    public void SetReinforcementArmy(WorldMap.LocalArmyData army)
    {
      this.reinforcementArray[army.armyID] = (object) army;
    }

    public WorldMap.LocalArmyData GetReinformcementArmyByID(long id)
    {
      return (WorldMap.LocalArmyData) this.reinforcementArray[id];
    }

    public long HighestArmyIDSeen
    {
      get => this.highestArmySeen;
      set => this.highestArmySeen = value;
    }

    public void retrieveArmies()
    {
      this.highestDownloadedArmy = -1L;
      this.armyArray.Clear();
      RemoteServices.Instance.set_GetArmyData_UserCallBack(new RemoteServices.GetArmyData_UserCallBack(this.getArmyData));
      RemoteServices.Instance.set_RetrieveAttackResult_UserCallBack(new RemoteServices.RetrieveAttackResult_UserCallBack(this.retrieveAttackResultCallback));
      RemoteServices.Instance.GetArmyData(-2L);
    }

    public void getArmiesIfNewAttacks()
    {
      RemoteServices.Instance.GetArmyData(this.highestDownloadedArmy);
    }

    public void addExistingArmy(long armyID) => this.rememberedExistingArmies.Add(armyID);

    public void getArmyData(GetArmyData_ReturnType returnData)
    {
      if (returnData.Success)
      {
        if (returnData.m_newQuestsData != null)
          this.setNewQuestData(returnData.m_newQuestsData);
        this.loadingErrored = false;
        SparseArray sparseArray = new SparseArray();
        if (returnData.existingArmies != null)
        {
          foreach (long existingArmy in returnData.existingArmies)
            sparseArray[existingArmy] = (object) existingArmy;
        }
        if (returnData.armyData != null)
        {
          this.doGetArmyData((IEnumerable<ArmyReturnData>) returnData.armyData, (IEnumerable<ArmyReturnData>) returnData.reinforcementData, true);
          this.highestArmySeen = returnData.armyHighestSeen;
          foreach (WorldMap.LocalArmyData army in this.armyArray)
            army.singlyAdded = false;
          if (returnData.existingArmies != null)
          {
            foreach (ArmyReturnData armyReturnData in returnData.armyData)
            {
              if (sparseArray[armyReturnData.armyID] == null)
              {
                sparseArray[armyReturnData.armyID] = (object) armyReturnData.armyID;
                returnData.existingArmies.Add(armyReturnData.armyID);
              }
            }
          }
          GameEngine.Instance.setServerDownTime(returnData.serverDowntime);
        }
        List<long> longList = new List<long>();
        if (returnData.armyDataNew != null)
        {
          this.doGetArmyData((IEnumerable<ArmyReturnData>) returnData.armyDataNew, (IEnumerable<ArmyReturnData>) returnData.reinforcementData, false);
          foreach (WorldMap.LocalArmyData army in this.armyArray)
          {
            longList.Add(army.armyID);
            army.singlyAdded = false;
          }
          if (returnData.existingArmies != null)
          {
            foreach (ArmyReturnData armyReturnData in returnData.armyDataNew)
            {
              if (sparseArray[armyReturnData.armyID] == null)
              {
                sparseArray[armyReturnData.armyID] = (object) armyReturnData.armyID;
                returnData.existingArmies.Add(armyReturnData.armyID);
              }
            }
          }
        }
        else if (returnData.existingArmies != null)
        {
          foreach (WorldMap.LocalArmyData army in this.armyArray)
            longList.Add(army.armyID);
        }
        if (returnData.existingArmies != null)
        {
          List<long> existingArmies = returnData.existingArmies;
          existingArmies.AddRange((IEnumerable<long>) this.rememberedExistingArmies);
          sparseArray.Clear();
          foreach (long index in existingArmies)
            sparseArray[index] = (object) index;
          WorldMap.LocalArmyData.groupCurrentTime = DXTimer.GetCurrentMilliseconds();
          foreach (long num in longList)
          {
            if (sparseArray[num] == null)
            {
              WorldMap.LocalArmyData armyById = this.GetArmyByID(num);
              if (armyById != null)
              {
                if (armyById.attackType != 13 || (VillageMap.getCurrentServerTime() - armyById.serverEndTime).TotalSeconds >= 10.0)
                {
                  if (RemoteServices.Instance.UserID == armyById.userID && armyById.lootType >= 0)
                  {
                    armyById.localEndTime = armyById.localStartTime + 1.0;
                    armyById.updatePosition();
                  }
                }
                else
                  continue;
              }
              this.RemoveArmy(num);
            }
          }
        }
        long num1 = -1;
        foreach (WorldMap.LocalArmyData army in this.armyArray)
        {
          if (army.armyID > num1 && !army.singlyAdded)
            num1 = army.armyID;
        }
        this.highestDownloadedArmy = num1;
      }
      else
        this.loadingErrored = true;
      if (this.doSelectTutorialArmy)
      {
        this.doSelectTutorialArmy = false;
        InterfaceMgr.Instance.selectTutorialArmy();
      }
      this.rememberedExistingArmies.Clear();
    }

    public void updateExistingArmies(long[] existingArmiesX)
    {
      List<long> longList = new List<long>();
      foreach (WorldMap.LocalArmyData army in this.armyArray)
        longList.Add(army.armyID);
      SparseArray sparseArray = new SparseArray();
      foreach (long index in existingArmiesX)
        sparseArray[index] = (object) index;
      WorldMap.LocalArmyData.groupCurrentTime = DXTimer.GetCurrentMilliseconds();
      foreach (long num in longList)
      {
        if (sparseArray[num] == null)
        {
          WorldMap.LocalArmyData armyById = this.GetArmyByID(num);
          if (armyById != null && RemoteServices.Instance.UserID == armyById.userID && armyById.lootType >= 0)
          {
            armyById.localEndTime = armyById.localStartTime + 1.0;
            armyById.updatePosition();
          }
          this.RemoveArmy(num);
        }
      }
    }

    public int countIncomingAttacks(ref long highestAttackingArmy)
    {
      int num = 0;
      highestAttackingArmy = -1L;
      foreach (WorldMap.LocalArmyData army in this.armyArray)
      {
        if (this.isUserVillage(army.targetVillageID) && army.lootType < 0 && army.attackType != 30 && army.attackType != 31)
        {
          ++num;
          if (army.armyID > highestAttackingArmy)
            highestAttackingArmy = army.armyID;
        }
      }
      return num;
    }

    public void doGetArmyData(
      IEnumerable<ArmyReturnData> armyReturnData,
      IEnumerable<ArmyReturnData> reinforcementReturnData,
      bool clearArray)
    {
      if (armyReturnData != null)
      {
        WorldMap.LogArmyErrorIfUnity("Got some armies");
        SparseArray armyRequestSentFlag = new SparseArray();
        foreach (WorldMap.LocalArmyData army in this.armyArray)
          armyRequestSentFlag[army.armyID] = (object) army.requestSent;
        if (clearArray)
        {
          if (armyReturnData != null)
            this.armyArray.Clear();
          if (reinforcementReturnData != null)
            this.reinforcementArray.Clear();
        }
        WorldMap.LocalArmyData.groupCurrentTime = DXTimer.GetCurrentMilliseconds();
        if (armyReturnData != null)
        {
          WorldMap.LogArmyErrorIfUnity("Going to add armies to array");
          int num = 0;
          foreach (ArmyReturnData data in armyReturnData)
          {
            ++num;
            this.addArmyToArray(data, ref armyRequestSentFlag, clearArray);
          }
          WorldMap.LogArmyErrorIfUnity("Should have added " + num.ToString() + " armies");
        }
        if (reinforcementReturnData != null)
        {
          WorldMap.LogArmyErrorIfUnity("Going to add armies to reinforcement array");
          foreach (ArmyReturnData data in reinforcementReturnData)
            this.addArmyToArray(data, ref armyRequestSentFlag, false);
        }
        foreach (WorldMap.LocalArmyData army in this.armyArray)
        {
          if (army.lootType == 95)
          {
            double lootLevel = army.lootLevel;
            army.lootType = 1;
            long id = army.armyID - (long) army.lootLevel;
            if (this.GetArmyByID(id) != null)
              this.GetArmyByID(id).dead = true;
          }
        }
      }
      else
        WorldMap.LogArmyErrorIfUnity("Failed to load armies");
    }

    public void deleteArmy(long armyID) => this.RemoveArmy(armyID);

    public void addReinforcementArmy(ArmyReturnData data)
    {
      WorldMap.LocalArmyData.groupCurrentTime = DXTimer.GetCurrentMilliseconds();
      SparseArray armyRequestSentFlag = (SparseArray) null;
      this.addArmyToArray(data, ref armyRequestSentFlag, false);
    }

    public void AddFakeArmy(ArmyReturnData data)
    {
      SparseArray armyRequestSentFlag = new SparseArray();
      this.addArmyToArray(data, ref armyRequestSentFlag, false);
    }

    private void addArmyToArray(
      ArmyReturnData data,
      ref SparseArray armyRequestSentFlag,
      bool singleAdd)
    {
      if(data == null)
            {
                BotLogger.Log("WorldMap", BotLogLevel.Error,("ArmyReturnData is null, cannot add army to array."));
                return;
            }
      WorldMap.LocalArmyData army = new WorldMap.LocalArmyData();
      army.armyID = data.armyID;
      army.homeVillageID = data.homeVillageID;
      army.travelFromVillageID = data.travelFromVillageID;
      army.userID = data.userID;
      army.attackType = data.attackType;
      army.targetVillageID = data.targetVillageID;
      army.numPeasants = data.numPeasants;
      army.numArchers = data.numArchers;
      army.numPikemen = data.numPikemen;
      army.numSwordsmen = data.numSwordsmen;
      army.numCatapults = data.numCatapults;
      army.numCaptains = data.numCaptains;
      army.numScouts = data.numScouts;
      army.captainsCommand = data.captainsCommand;
      army.carryingFlag = data.carryingFlag;
      army.lootLevel = data.lootLevel;
      army.lootType = data.lootType;
      army.lootData = data.lootData;
      army.aiPlayer = data.aiPlayer;
      army.seaTravel = this.isIslandTravel(data.travelFromVillageID, data.targetVillageID);
      army.reinforcements = data.reinforcements;
      if (singleAdd)
        army.singlyAdded = true;
      try
      {
        if (army.targetVillageID >= 0 && army.targetVillageID < this.villageList.Length)
        {
          army.createJourney(data.startTime, data.curTime, data.endTime);
          army.targetDisplayX = (double) this.villageList[data.targetVillageID].x;
          army.targetDisplayY = (double) this.villageList[data.targetVillageID].y;
        }
        else
          army.serverEndTime = data.curTime;
        if (army.travelFromVillageID < this.villageList.Length)
        {
          army.baseDisplayX = (double) this.villageList[data.travelFromVillageID].x;
          army.baseDisplayY = (double) this.villageList[data.travelFromVillageID].y;
        }
        if (!army.reinforcements && armyRequestSentFlag != null && armyRequestSentFlag[army.armyID] != null)
          army.requestSent = (bool) armyRequestSentFlag[army.armyID];
        army.updatePosition();
        if (!data.reinforcements)
          this.SetArmy(army);
        else
          this.reinforcementArray[data.armyID] = (object) army;
      }
      catch
      {
      }
    }

    public void armyAttackCallback(ArmyAttack_ReturnType returnData)
    {
    }

    public void retrieveAttackResultCallback(RetrieveAttackResult_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      if (returnData.armyData != null)
      {
        if (this.tutorialArmyID != -1L && returnData.armyData.armyID == this.tutorialArmyID)
        {
          this.tutorialArmyID = -1L;
          TutorialBattleReportPopup battleReportPopup = new TutorialBattleReportPopup();
          battleReportPopup.init();
          battleReportPopup.Show((IWin32Window) InterfaceMgr.Instance.ParentForm);
        }
        if (this.isUserVillage(returnData.armyData.targetVillageID) || this.isUserRelatedVillage(returnData.armyData.targetVillageID))
          GameEngine.Instance.flushVillage(returnData.armyData.targetVillageID);
        if (this.GetArmyByID(returnData.armyData.armyID) == null)
          return;
        if (returnData.armyData.dead)
        {
          this.RemoveArmy(returnData.armyData.armyID);
          return;
        }
        this.doGetArmyData((IEnumerable<ArmyReturnData>) new ArmyReturnData[1]
        {
          returnData.armyData
        }, (IEnumerable<ArmyReturnData>) null, false);
        if (returnData.reinforcementData != null)
          this.doGetArmyData((IEnumerable<ArmyReturnData>) null, (IEnumerable<ArmyReturnData>) returnData.reinforcementData, true);
      }
      if (returnData.villageUpdateList != null)
      {
        if (returnData.userVillageList != null)
        {
          bool retrievingUserVillages = this.retrievingUserVillages;
          this.retrievingUserVillages = true;
          this.processVillageFactionChangesList(returnData.villageUpdateList, returnData.currentVillageChangePos);
          this.retrievingUserVillages = retrievingUserVillages;
        }
        else
          this.processVillageFactionChangesList(returnData.villageUpdateList, returnData.currentVillageChangePos);
      }
      else if (returnData.villageOwnerFactions != null)
        this.processVillageFactionList(returnData.villageOwnerFactions, returnData.currentVillageChangePos);
      if (returnData.userVillageList != null)
        GameEngine.Instance.World.doGetUserVillages(returnData.userVillageList, returnData.userVillageNameList);
      this.setPoints(returnData.currentPoints);
      this.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
      this.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
      this.setNumMadeCaptains(returnData.numMadeCaptains);
      if (returnData.cardData == null)
        return;
      GameEngine.Instance.cardsManager.UserCardData = returnData.cardData;
    }

    private long findNearestArmyFromScreenPos(Point mousePos, ref double bestDist)
    {
      if (InterfaceMgr.Instance.WorldMapMode != 0)
        return -1;
      double mapX = ((double) mousePos.X - (double) this.m_screenWidth / 2.0) / this.m_worldScale + this.m_screenCentreX;
      double mapY = ((double) mousePos.Y - (double) this.m_screenHeight / 2.0) / this.m_worldScale + this.m_screenCentreY;
      return mapX >= 0.0 && mapX < (double) this.worldMapWidth && mapY >= 0.0 && mapY < (double) this.worldMapHeight ? this.findNearestArmyFromMapPos(mapX, mapY, ref bestDist) : -1L;
    }

    private long findNearestArmyFromMapPos(double mapX, double mapY, ref double bestDist)
    {
      WorldMapFilter worldMapFilter = GameEngine.Instance.World.worldMapFilter;
      long nearestArmyFromMapPos = -1;
      double num1 = 2.25;
      foreach (WorldMap.LocalArmyData army in this.armyArray)
      {
        if (worldMapFilter.showArmy(army))
        {
          double num2 = (army.displayX - mapX) * (army.displayX - mapX) + (army.displayY - mapY) * (army.displayY - mapY);
          if (num2 < num1)
          {
            num1 = num2;
            nearestArmyFromMapPos = army.armyID;
          }
        }
      }
      bestDist = num1;
      return nearestArmyFromMapPos;
    }

    private long findNearestReinforcementFromScreenPos(Point mousePos, ref double bestDist)
    {
      if (InterfaceMgr.Instance.WorldMapMode != 0)
        return -1;
      double mapX = ((double) mousePos.X - (double) this.m_screenWidth / 2.0) / this.m_worldScale + this.m_screenCentreX;
      double mapY = ((double) mousePos.Y - (double) this.m_screenHeight / 2.0) / this.m_worldScale + this.m_screenCentreY;
      return mapX >= 0.0 && mapX < (double) this.worldMapWidth && mapY >= 0.0 && mapY < (double) this.worldMapHeight ? this.findNearestReinforcementFromMapPos(mapX, mapY, ref bestDist) : -1L;
    }

    private long findNearestReinforcementFromMapPos(double mapX, double mapY, ref double bestDist)
    {
      WorldMapFilter worldMapFilter = GameEngine.Instance.World.worldMapFilter;
      long reinforcementFromMapPos = -1;
      double num1 = 2.25;
      double num2 = DXTimer.GetCurrentMilliseconds() / 1000.0;
      foreach (WorldMap.LocalArmyData reinforcement in this.reinforcementArray)
      {
        if (reinforcement.localEndTime != 0.0 && reinforcement.localEndTime >= num2 && worldMapFilter.showReinforcements(reinforcement))
        {
          double num3 = (reinforcement.displayX - mapX) * (reinforcement.displayX - mapX) + (reinforcement.displayY - mapY) * (reinforcement.displayY - mapY);
          if (num3 < num1)
          {
            num1 = num3;
            reinforcementFromMapPos = reinforcement.armyID;
          }
        }
      }
      bestDist = num1;
      return reinforcementFromMapPos;
    }

    public bool isArmyMoving(long armyID)
    {
      WorldMap.LocalArmyData armyById = this.GetArmyByID(armyID);
      return armyById != null && armyById.targetVillageID >= 0;
    }

    public bool isArmyReallyReturning(long armyID)
    {
      WorldMap.LocalArmyData armyById = this.GetArmyByID(armyID);
      return armyById == null || armyById.lootType != 10000;
    }

    public WorldMap.LocalArmyData getArmy(long armyID)
    {
      return armyID >= 0L ? this.GetArmyByID(armyID) : (WorldMap.LocalArmyData) null;
    }

    public SparseArray getArmyArray() => this.armyArray;

    public int countYourArmyTroops(int villageID)
    {
      int num = 0;
      foreach (WorldMap.LocalArmyData army in this.armyArray)
      {
        if (army.travelFromVillageID == villageID && army.homeVillageID == villageID)
        {
          num += army.numPeasants;
          num += army.numArchers;
          num += army.numPikemen;
          num += army.numSwordsmen;
          num += army.numCatapults;
        }
      }
      return num;
    }

    public int countYourArmyScouts(int villageID)
    {
      int num = 0;
      foreach (WorldMap.LocalArmyData army in this.armyArray)
      {
        if (army.homeVillageID == villageID)
          num += army.numScouts;
      }
      return num;
    }

    public int countYourArmyCaptains(int villageID)
    {
      int num = 0;
      foreach (WorldMap.LocalArmyData army in this.armyArray)
      {
        if (army.travelFromVillageID == villageID)
          num += army.numCaptains;
      }
      return num;
    }

    public WorldMap.LocalArmyData getReinforcement(long armyID)
    {
      return armyID >= 0L ? (WorldMap.LocalArmyData) this.reinforcementArray[armyID] : (WorldMap.LocalArmyData) null;
    }

    public SparseArray getReinforcementsArray() => this.reinforcementArray;

    public void getReinforceTotals(
      int villageID,
      ref int numPeasants,
      ref int numArchers,
      ref int numPikemen,
      ref int numSwordsmen)
    {
      numPeasants = 0;
      numArchers = 0;
      numPikemen = 0;
      numSwordsmen = 0;
      int num = 0;
      foreach (WorldMap.LocalArmyData reinforcement in this.reinforcementArray)
      {
        if (reinforcement.targetVillageID == villageID && reinforcement.serverEndTime < VillageMap.getCurrentServerTime())
        {
          numPeasants += reinforcement.numPeasants;
          numArchers += reinforcement.numArchers;
          numPikemen += reinforcement.numPikemen;
          numSwordsmen += reinforcement.numSwordsmen;
          num += reinforcement.numCatapults;
        }
      }
    }

    public int countYourReinforcementTroops(int villageID)
    {
      int num = 0;
      foreach (WorldMap.LocalArmyData reinforcement in this.reinforcementArray)
      {
        if (reinforcement.homeVillageID == villageID)
        {
          num += reinforcement.numPeasants;
          num += reinforcement.numArchers;
          num += reinforcement.numPikemen;
          num += reinforcement.numSwordsmen;
          num += reinforcement.numCatapults;
        }
      }
      return num;
    }

    public void getTotalTroopsOutOfVillage(
      int villageID,
      ref int numPeasants,
      ref int numArchers,
      ref int numPikemen,
      ref int numSwordsmen,
      ref int numCatapults,
      ref int numCaptains,
      ref int numReinfPeasants,
      ref int numReinfArchers,
      ref int numReinfPikemen,
      ref int numReinfSwordsmen,
      ref int numReinfCatapults,
      ref int numReinfCaptains)
    {
      numPeasants = 0;
      numArchers = 0;
      numPikemen = 0;
      numSwordsmen = 0;
      numCatapults = 0;
      numCaptains = 0;
      numReinfPeasants = 0;
      numReinfArchers = 0;
      numReinfPikemen = 0;
      numReinfSwordsmen = 0;
      numReinfCatapults = 0;
      numReinfCaptains = 0;
      foreach (WorldMap.LocalArmyData army in this.armyArray)
      {
        if (army.travelFromVillageID == villageID && army.homeVillageID == villageID)
        {
          numPeasants += army.numPeasants;
          numArchers += army.numArchers;
          numPikemen += army.numPikemen;
          numSwordsmen += army.numSwordsmen;
          numCatapults += army.numCatapults;
          numCaptains += army.numCaptains;
        }
      }
      foreach (WorldMap.LocalArmyData reinforcement in this.reinforcementArray)
      {
        if (reinforcement.travelFromVillageID == villageID)
        {
          numReinfPeasants += reinforcement.numPeasants;
          numReinfArchers += reinforcement.numArchers;
          numReinfPikemen += reinforcement.numPikemen;
          numReinfSwordsmen += reinforcement.numSwordsmen;
          numReinfCatapults += reinforcement.numCatapults;
        }
      }
    }

    public static string getPeopleStatusString(int remaining, int total, double[] returnTimes)
    {
      if (remaining > 0 || total == 0)
        return remaining.ToString();
      double secsLeft = 0.0;
      double num1 = double.MaxValue;
      if (returnTimes != null && returnTimes.Length > 0)
      {
        for (int index = 0; index < returnTimes.Length; ++index)
        {
          if (returnTimes[index] < num1)
            num1 = returnTimes[index];
        }
        double num2 = DXTimer.GetCurrentMilliseconds() / 1000.0;
        secsLeft = num1 - num2;
        if (secsLeft < 0.0)
          secsLeft = 0.0;
      }
      return VillageMap.createBuildTimeString((int) secsLeft);
    }

    public double[] getScoutTimes()
    {
      if (this.thisVillageArmies == null)
        return (double[]) null;
      List<double> doubleList = new List<double>();
      for (int index = 0; index < this.thisVillageArmies.Count; ++index)
      {
        long thisVillageArmy = this.thisVillageArmies[index];
        if (this.armyArray[thisVillageArmy] != null)
        {
          WorldMap.LocalArmyData army = (WorldMap.LocalArmyData) this.armyArray[thisVillageArmy];
          if (army.isScouts())
          {
            bool flag = army.lootType >= 0;
            double localEndTime = army.localEndTime;
            if (!flag)
            {
              TimeSpan timeSpan = army.serverEndTime - army.serverStartTime;
              localEndTime += (double) timeSpan.Seconds;
            }
            doubleList.Add(localEndTime);
          }
        }
      }
      return doubleList.ToArray();
    }

    public void updateArmies()
    {
      try
      {
        List<long> longList1 = new List<long>();
        WorldMap.LocalArmyData.groupCurrentTime = DXTimer.GetCurrentMilliseconds();
        foreach (WorldMap.LocalArmyData army in this.armyArray)
        {
          army.updatePosition();
          if (army.dead)
            longList1.Add(army.armyID);
        }
        foreach (long index in longList1)
          this.armyArray.RemoveAt(index);
        List<long> longList2 = new List<long>();
        foreach (WorldMap.LocalArmyData reinforcement in this.reinforcementArray)
        {
          reinforcement.updatePosition();
          if (reinforcement.dead)
            longList2.Add(reinforcement.armyID);
        }
        foreach (long index in longList2)
          this.reinforcementArray.RemoveAt(index);
      }
      catch (Exception ex)
      {
        UniversalDebugLog.Log("exception updating armies " + ex.ToString());
      }
    }

    public bool isForagingArmy(long armyID) => this.scoutsForaging[armyID] != null;

    public bool isForagingVillage(int villageID) => this.scoutsVillageForaging[villageID] != null;

    public bool isAttackingArmy(long armyID) => this.attackingArmies[armyID] != null;

    public bool isVillageInvolvedInAttacks(int villageID)
    {
      return this.villagesInvolvedInAttacks[villageID] != null;
    }

    public bool isVillageInvolvedInAIAttacks(int villageID)
    {
      return this.villagesInvolvedInAIAttacks[villageID] != null;
    }

    public long getTutorialArmyID()
    {
      foreach (WorldMap.LocalArmyData army in this.armyArray)
      {
        if (army.attackType == 13)
          return army.armyID;
      }
      return -1;
    }

    public void drawArmies(RectangleF screenRect, bool normalMode)
    {
      this.alphaPulse += 20;
      if (this.alphaPulse > 755)
        this.alphaPulse -= 3011;
      if (this.alphaPulse > (int) byte.MaxValue)
        this.alphaValue = (int) byte.MaxValue;
      else if (this.alphaPulse >= 0)
      {
        this.alphaValue = this.alphaPulse;
      }
      else
      {
        this.alphaValue = -2000 - this.alphaPulse;
        if (this.alphaValue < 0)
          this.alphaValue = 0;
      }
      try
      {
        this.scoutsForaging.Clear();
        this.scoutsVillageForaging.Clear();
        this.villagesInvolvedInAttacks.Clear();
        this.attackingArmies.Clear();
        this.villagesInvolvedInAIAttacks.Clear();
        float scale = (float) (this.m_worldScale / 28.0 / 0.60000002384185791);
        if ((double) scale < 0.10000000149011612)
          scale = 0.1f;
        if ((double) scale > 1.0)
          scale = 1f;
        List<long> armiesDead = new List<long>();
        WorldMapFilter worldMapFilter = GameEngine.Instance.World.worldMapFilter;
        this.armyIconsFilter.Clear();
        float localScale = scale;
        bool filtering = worldMapFilter.FilterActive && InterfaceMgr.Instance.WorldMapMode == 0;
        foreach (WorldMap.LocalArmyData army in this.armyArray)
          this.DrawArmy(army, localScale, scale, filtering, worldMapFilter, armiesDead, normalMode, screenRect, false);
        foreach (long index in armiesDead)
          this.armyArray.RemoveAt(index);
        foreach (WorldMap.LocalArmyData reinforcement in this.reinforcementArray)
        {
          if (reinforcement.visible && reinforcement.isVisible(screenRect) && worldMapFilter.showReinforcements(reinforcement))
          {
            int num1 = 1;
            this.villageSprite.PosX = ((float) reinforcement.displayX - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
            this.villageSprite.PosY = ((float) reinforcement.displayY - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
            this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
            this.villageSprite.SpriteNo = num1;
            this.villageSprite.Center = new PointF(44f, 44f);
            this.villageSprite.RotationAngle = SpriteWrapper.getFacing(reinforcement.BasePoint(), reinforcement.TargetPoint());
            this.villageSprite.Scale = scale;
            this.villageSprite.Update();
            this.doDraw(this.villageSprite);
            int num2 = 5;
            this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
            this.villageSprite.SpriteNo = num2;
            this.villageSprite.Center = new PointF(44f, 44f);
            this.villageSprite.Scale = scale;
            this.villageSprite.Update();
            this.doDraw(this.villageSprite);
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    private bool DrawArmy(
      WorldMap.LocalArmyData army,
      float localScale,
      float scale,
      bool filtering,
      WorldMapFilter filter,
      List<long> armiesDead,
      bool normalMode,
      RectangleF screenRect,
      bool visChecked)
    {
      if (army.dead)
      {
        armiesDead.Add(army.armyID);
        return false;
      }
      if (!normalMode && army.attackType != 17)
        return false;
      bool flag1 = true;
      if (filtering)
      {
        if (army.isScouts())
        {
          if (this.isForagingSpecial(army.targetVillageID))
          {
            this.scoutsForaging[army.armyID] = (object) army.armyID;
            this.scoutsVillageForaging[army.homeVillageID] = (object) army.homeVillageID;
          }
          else
          {
            this.attackingArmies[army.armyID] = (object) army.armyID;
            this.villagesInvolvedInAttacks[army.homeVillageID] = (object) army.homeVillageID;
            this.villagesInvolvedInAttacks[army.targetVillageID] = (object) army.targetVillageID;
          }
        }
        else
        {
          this.attackingArmies[army.armyID] = (object) army.armyID;
          this.villagesInvolvedInAttacks[army.homeVillageID] = (object) army.homeVillageID;
          this.villagesInvolvedInAttacks[army.targetVillageID] = (object) army.targetVillageID;
        }
        flag1 = filter.showArmy(army);
        if (flag1)
        {
          this.villagesInvolvedInAIAttacks[army.homeVillageID] = (object) army.homeVillageID;
          this.villagesInvolvedInAIAttacks[army.targetVillageID] = (object) army.targetVillageID;
        }
      }
      if (!visChecked && !army.isVisible(screenRect) || !flag1)
        return false;
      localScale = scale;
      if (army.attackType == 17 && (double) localScale < 0.5)
        localScale = 0.5f;
      this.villageSprite.PosX = ((float) army.displayX - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
      this.villageSprite.PosY = ((float) army.displayY - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
      int index = (int) this.villageSprite.PosX / 3 * 100000 + (int) this.villageSprite.PosY / 3;
      if (this.armyIconsFilter[index] != null)
        return false;
      this.armyIconsFilter[index] = (object) 1;
      int num1 = 2;
      int num2 = 6;
      if (army.attackType == 31 || army.attackType == 30)
      {
        num2 = 5;
        num1 = 1;
      }
      else if (army.isScouts())
      {
        num2 = 14;
        num1 = 2;
        if (army.userID != RemoteServices.Instance.UserID)
        {
          ++num2;
          ++num1;
          if (GameEngine.Instance.LocalWorldData.AIWorld)
          {
            switch (this.villageList[army.travelFromVillageID].special)
            {
              case 7:
              case 8:
                num2 = 403;
                num1 = 404;
                break;
              case 9:
              case 10:
                num2 = 407;
                num1 = 408;
                break;
              case 11:
              case 12:
                num2 = 411;
                num1 = 412;
                break;
              case 13:
              case 14:
                num2 = 415;
                num1 = 416;
                break;
            }
          }
        }
      }
      else if (army.isCaptainAttack())
      {
        num2 = 384;
        num1 = 2;
        if (army.userID != RemoteServices.Instance.UserID)
        {
          ++num2;
          ++num1;
          if (GameEngine.Instance.LocalWorldData.AIWorld)
          {
            switch (this.villageList[army.travelFromVillageID].special)
            {
              case 7:
              case 8:
                num2 = 405;
                num1 = 404;
                break;
              case 9:
              case 10:
                num2 = 409;
                num1 = 408;
                break;
              case 11:
              case 12:
                num2 = 413;
                num1 = 412;
                break;
              case 13:
              case 14:
                num2 = 417;
                num1 = 416;
                break;
              case 30:
                bool flag2 = false;
                switch (this.villageList[army.targetVillageID].special)
                {
                  case 7:
                  case 8:
                  case 9:
                  case 10:
                  case 11:
                  case 12:
                  case 13:
                  case 14:
                    num2 = 5;
                    num1 = 1;
                    flag2 = true;
                    break;
                }
                if (!flag2)
                {
                  switch (army.aiPlayer)
                  {
                    case 0:
                      num2 = 405;
                      num1 = 404;
                      break;
                    case 1:
                      num2 = 409;
                      num1 = 408;
                      break;
                    case 2:
                      num2 = 413;
                      num1 = 412;
                      break;
                    case 3:
                      num2 = 417;
                      num1 = 416;
                      break;
                  }
                }
                else
                  break;
                break;
            }
          }
        }
      }
      else if (army.userID != RemoteServices.Instance.UserID)
      {
        num2 = 7;
        num1 = 3;
        if (GameEngine.Instance.LocalWorldData.AIWorld)
        {
          switch (this.villageList[army.travelFromVillageID].special)
          {
            case 7:
            case 8:
              num2 = 402;
              num1 = 404;
              break;
            case 9:
            case 10:
              num2 = 406;
              num1 = 408;
              break;
            case 11:
            case 12:
              num2 = 410;
              num1 = 412;
              break;
            case 13:
            case 14:
              num2 = 414;
              num1 = 416;
              break;
            case 30:
              bool flag3 = false;
              switch (this.villageList[army.targetVillageID].special)
              {
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                  num2 = 5;
                  num1 = 1;
                  flag3 = true;
                  break;
              }
              if (!flag3)
              {
                VillageData village = this.villageList[army.targetVillageID];
                if (village.visible && village.userID <= 0 && village.special == 0)
                {
                  switch (army.aiPlayer)
                  {
                    case 0:
                      num2 = 405;
                      num1 = 404;
                      break;
                    case 1:
                      num2 = 409;
                      num1 = 408;
                      break;
                    case 2:
                      num2 = 413;
                      num1 = 412;
                      break;
                    case 3:
                      num2 = 417;
                      num1 = 416;
                      break;
                  }
                  flag3 = true;
                }
              }
              if (!flag3)
              {
                switch (army.aiPlayer)
                {
                  case 0:
                    num2 = 402;
                    num1 = 404;
                    break;
                  case 1:
                    num2 = 406;
                    num1 = 408;
                    break;
                  case 2:
                    num2 = 410;
                    num1 = 412;
                    break;
                  case 3:
                    num2 = 414;
                    num1 = 416;
                    break;
                }
              }
              else
                break;
              break;
          }
        }
      }
      if (this.DrawingArmyArrows)
      {
        this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
        this.villageSprite.SpriteNo = num1;
        this.villageSprite.Center = new PointF(44f, 44f);
        this.villageSprite.RotationAngle = SpriteWrapper.getFacing(army.BasePoint(), army.TargetPoint());
        this.villageSprite.Scale = localScale;
        this.villageSprite.Update();
        this.doDraw(this.villageSprite);
      }
      this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
      this.villageSprite.SpriteNo = num2;
      this.villageSprite.Center = new PointF(44f, 44f);
      this.villageSprite.Scale = localScale;
      this.villageSprite.Update();
      this.doDraw(this.villageSprite);
      if (army.seaTravel)
      {
        this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
        switch (num1)
        {
          case 0:
            this.villageSprite.SpriteNo = 425;
            break;
          case 1:
            this.villageSprite.SpriteNo = 426;
            break;
          case 2:
            this.villageSprite.SpriteNo = 427;
            break;
          case 3:
            this.villageSprite.SpriteNo = 428;
            break;
          case 404:
            this.villageSprite.SpriteNo = 429;
            break;
          case 408:
            this.villageSprite.SpriteNo = 430;
            break;
          case 412:
            this.villageSprite.SpriteNo = 431;
            break;
          case 416:
            this.villageSprite.SpriteNo = 432;
            break;
        }
        this.villageSprite.Center = new PointF(44f, 44f);
        this.villageSprite.ColorToUse = Color.FromArgb(this.alphaValue, ARGBColors.White);
        this.villageSprite.Scale = localScale;
        this.villageSprite.Update();
        this.doDraw(this.villageSprite);
      }
      if (army.carryingFlag)
      {
        this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
        this.villageSprite.SpriteNo = 29;
        this.villageSprite.Center = new PointF(44f, 59f);
        this.villageSprite.Scale = localScale;
        this.villageSprite.Update();
        this.doDraw(this.villageSprite);
      }
      return true;
    }

    private void doDraw(SpriteWrapper wrapper) => wrapper.DrawAndClear();

    private bool isInWolfsRevenge() => !(this.wolfsRevengeEnd < VillageMap.getCurrentServerTime());

    public void updateAIInvasions()
    {
      if (!GameEngine.Instance.LocalWorldData.AIWorld)
        return;
      foreach (int invasionVillage in AIWorldSettings.getInvasionVillages(GameEngine.Instance.LocalWorldData.EUAIWorld))
        this.invasionMarkerState[invasionVillage] = (object) 0;
      foreach (WorldMap.LocalArmyData army in this.armyArray)
      {
        if (army.attackType == 17)
          this.invasionMarkerState[army.homeVillageID] = (object) 2;
      }
      if (this.invasionInfo == null)
        return;
      DateTime dateTime = VillageMap.getCurrentServerTime().AddDays(10.0);
      foreach (AIWorldInvasionData worldInvasionData in this.invasionInfo)
      {
        if (worldInvasionData.date < dateTime && this.invasionMarkerState[worldInvasionData.invasionVillageID] != null && (int) this.invasionMarkerState[worldInvasionData.invasionVillageID] != 2)
          this.invasionMarkerState[worldInvasionData.invasionVillageID] = (object) 1;
      }
    }

    public int getAIInvasionMarkerState(int villageID)
    {
      return this.invasionMarkerState[villageID] == null ? 0 : (int) this.invasionMarkerState[villageID];
    }

    public DateTime getNextAIInvasionDate(int villageID)
    {
      foreach (AIWorldInvasionData worldInvasionData in this.invasionInfo)
      {
        if (worldInvasionData.invasionVillageID == villageID)
          return worldInvasionData.date;
      }
      return DateTime.MinValue;
    }

    public void monitorAIInvasionActivity()
    {
      if (!GameEngine.Instance.LocalWorldData.AIWorld)
        return;
      if (this.lastInvasionInfoTime < DateTime.Now)
      {
        this.downloadAIInvasionInfo();
      }
      else
      {
        if (!(this.lastUpdateInvasionInfoTime < DateTime.Now))
          return;
        this.lastUpdateInvasionInfoTime = DateTime.Now.AddMinutes(5.0);
        this.updateAIInvasions();
      }
    }

    public void downloadAIInvasionInfo()
    {
      this.wolfsRevengeStart = DateTime.MinValue;
      this.wolfsRevengeEnd = DateTime.MinValue;
      this.lastInvasionInfoTime = DateTime.Now.AddHours(1.0);
      this.lastUpdateInvasionInfoTime = DateTime.Now.AddMinutes(5.0);
      RemoteServices.Instance.set_GetInvasionInfo_UserCallBack(new RemoteServices.GetInvasionInfo_UserCallBack(this.GetInvasionInfo_callback));
      RemoteServices.Instance.GetInvasionInfo();
    }

    public void GetInvasionInfo_callback(GetInvasionInfo_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.aiWorldGloryWinLevel = returnData.gloryRoundWinPoints;
      this.invasionInfo = returnData.invasions;
      if (this.invasionInfo == null)
        this.invasionInfo = new List<AIWorldInvasionData>();
      for (int index = 0; index < this.invasionInfo.Count; ++index)
      {
        AIWorldInvasionData worldInvasionData1 = this.invasionInfo[index];
        if (worldInvasionData1.invasionID == -12345)
        {
          AIWorldInvasionData worldInvasionData2 = this.invasionInfo[index + 1];
          this.wolfsRevengeStart = worldInvasionData1.date;
          this.wolfsRevengeEnd = worldInvasionData2.date;
          this.invasionInfo.Remove(worldInvasionData1);
          this.invasionInfo.Remove(worldInvasionData2);
          break;
        }
      }
      this.updateAIInvasions();
    }

    public int getInt32FromString(string text)
    {
      if (text.Length == 0)
        return 0;
      try
      {
        return Convert.ToInt32(text);
      }
      catch (Exception ex)
      {
      }
      return 0;
    }

    public double getDoubleFromString(string text)
    {
      if (text.Length == 0)
        return 0.0;
      try
      {
        return Convert.ToDouble(text);
      }
      catch (Exception ex)
      {
      }
      return 0.0;
    }

    private bool isVisible(RectangleF screenRect, float displayX, float displayY)
    {
      return (double) screenRect.Top - 50.0 <= (double) displayY && (double) screenRect.Left - 50.0 <= (double) displayX && (double) screenRect.Bottom + 50.0 >= (double) displayY && (double) screenRect.Right + 50.0 >= (double) displayX;
    }

    private bool isVisibleClose(RectangleF screenRect, float displayX, float displayY)
    {
      return (double) screenRect.Top - 5.0 <= (double) displayY && (double) screenRect.Left - 5.0 <= (double) displayX && (double) screenRect.Bottom + 5.0 >= (double) displayY && (double) screenRect.Right + 5.0 >= (double) displayX;
    }

    public void initFW()
    {
      for (int index = 0; index < 25; ++index)
        this.fwDataList[index] = new WorldMap.FWData()
        {
          spriteID = this.fwSpriteIDs[index] + 95,
          numToSpawn = 25,
          symmetrical = false,
          randomStartRotation = false,
          rotateSpeed = 0.0f,
          rotateClockwise = true,
          startScale = 1f,
          scaleSpeed = 0.0f,
          scaleTarget = 1f,
          fadeInTime = 0,
          fadeOutTime = 300,
          fadeRate = 0.06f,
          initialVelocity = 0.3f,
          maxVelocity = 0.3f,
          acceleration = 0.0f,
          speedVariance = 0.3f,
          childFirework = 0
        };
      this.fwChickenOrder[0] = 1;
      this.fwSheepOrder[0] = 1;
      this.fwJesterOrder[0] = 1;
      this.fwPigOrder[0] = 1;
      string[] strArray1 = this.fwSourceData.Split(',');
      int num1 = 0;
      string[] strArray2 = strArray1;
      int index1 = num1;
      int num2 = index1 + 1;
      this.totalNumFW = this.getInt32FromString(strArray2[index1]);
      string[] strArray3 = strArray1;
      int index2 = num2;
      int num3 = index2 + 1;
      this.totalNumFWBusy = this.getInt32FromString(strArray3[index2]);
      string[] strArray4 = strArray1;
      int index3 = num3;
      int num4 = index3 + 1;
      this.totalNumFWCrazy = this.getInt32FromString(strArray4[index3]);
      string[] strArray5 = strArray1;
      int index4 = num4;
      int num5 = index4 + 1;
      this.fwNormalChance = this.getInt32FromString(strArray5[index4]);
      string[] strArray6 = strArray1;
      int index5 = num5;
      int num6 = index5 + 1;
      this.fwBusyChance = this.getInt32FromString(strArray6[index5]);
      string[] strArray7 = strArray1;
      int index6 = num6;
      int num7 = index6 + 1;
      this.fwCrazyChance = this.getInt32FromString(strArray7[index6]);
      string[] strArray8 = strArray1;
      int index7 = num7;
      int num8 = index7 + 1;
      this.fwCycle = this.getInt32FromString(strArray8[index7]);
      for (int index8 = 0; index8 < 3; ++index8)
      {
        int[] fwChickenOrder = this.fwChickenOrder;
        int index9 = index8;
        string[] strArray9 = strArray1;
        int index10 = num8;
        int num9 = index10 + 1;
        int int32FromString1 = this.getInt32FromString(strArray9[index10]);
        fwChickenOrder[index9] = int32FromString1;
        int[] fwSheepOrder = this.fwSheepOrder;
        int index11 = index8;
        string[] strArray10 = strArray1;
        int index12 = num9;
        int num10 = index12 + 1;
        int int32FromString2 = this.getInt32FromString(strArray10[index12]);
        fwSheepOrder[index11] = int32FromString2;
        int[] fwJesterOrder = this.fwJesterOrder;
        int index13 = index8;
        string[] strArray11 = strArray1;
        int index14 = num10;
        int num11 = index14 + 1;
        int int32FromString3 = this.getInt32FromString(strArray11[index14]);
        fwJesterOrder[index13] = int32FromString3;
        int[] fwPigOrder = this.fwPigOrder;
        int index15 = index8;
        string[] strArray12 = strArray1;
        int index16 = num11;
        num8 = index16 + 1;
        int int32FromString4 = this.getInt32FromString(strArray12[index16]);
        fwPigOrder[index15] = int32FromString4;
      }
      for (int index17 = 0; index17 < 25; ++index17)
      {
        WorldMap.FWData fwData1 = this.fwDataList[index17];
        WorldMap.FWData fwData2 = fwData1;
        string[] strArray13 = strArray1;
        int index18 = num8;
        int num12 = index18 + 1;
        int int32FromString5 = this.getInt32FromString(strArray13[index18]);
        fwData2.numToSpawn = int32FromString5;
        WorldMap.FWData fwData3 = fwData1;
        string[] strArray14 = strArray1;
        int index19 = num12;
        int num13 = index19 + 1;
        int num14 = this.getInt32FromString(strArray14[index19]) > 0 ? 1 : 0;
        fwData3.symmetrical = num14 != 0;
        WorldMap.FWData fwData4 = fwData1;
        string[] strArray15 = strArray1;
        int index20 = num13;
        int num15 = index20 + 1;
        int num16 = this.getInt32FromString(strArray15[index20]) > 0 ? 1 : 0;
        fwData4.randomStartRotation = num16 != 0;
        WorldMap.FWData fwData5 = fwData1;
        string[] strArray16 = strArray1;
        int index21 = num15;
        int num17 = index21 + 1;
        double doubleFromString1 = this.getDoubleFromString(strArray16[index21]);
        fwData5.rotateSpeed = (float) doubleFromString1;
        WorldMap.FWData fwData6 = fwData1;
        string[] strArray17 = strArray1;
        int index22 = num17;
        int num18 = index22 + 1;
        int num19 = this.getInt32FromString(strArray17[index22]) > 0 ? 1 : 0;
        fwData6.rotateClockwise = num19 != 0;
        WorldMap.FWData fwData7 = fwData1;
        string[] strArray18 = strArray1;
        int index23 = num18;
        int num20 = index23 + 1;
        double doubleFromString2 = this.getDoubleFromString(strArray18[index23]);
        fwData7.startScale = (float) doubleFromString2;
        WorldMap.FWData fwData8 = fwData1;
        string[] strArray19 = strArray1;
        int index24 = num20;
        int num21 = index24 + 1;
        double doubleFromString3 = this.getDoubleFromString(strArray19[index24]);
        fwData8.scaleSpeed = (float) doubleFromString3;
        WorldMap.FWData fwData9 = fwData1;
        string[] strArray20 = strArray1;
        int index25 = num21;
        int num22 = index25 + 1;
        double doubleFromString4 = this.getDoubleFromString(strArray20[index25]);
        fwData9.scaleTarget = (float) doubleFromString4;
        WorldMap.FWData fwData10 = fwData1;
        string[] strArray21 = strArray1;
        int index26 = num22;
        int num23 = index26 + 1;
        int int32FromString6 = this.getInt32FromString(strArray21[index26]);
        fwData10.fadeInTime = int32FromString6;
        WorldMap.FWData fwData11 = fwData1;
        string[] strArray22 = strArray1;
        int index27 = num23;
        int num24 = index27 + 1;
        int int32FromString7 = this.getInt32FromString(strArray22[index27]);
        fwData11.fadeOutTime = int32FromString7;
        WorldMap.FWData fwData12 = fwData1;
        string[] strArray23 = strArray1;
        int index28 = num24;
        int num25 = index28 + 1;
        double doubleFromString5 = this.getDoubleFromString(strArray23[index28]);
        fwData12.fadeRate = (float) doubleFromString5;
        WorldMap.FWData fwData13 = fwData1;
        string[] strArray24 = strArray1;
        int index29 = num25;
        int num26 = index29 + 1;
        double doubleFromString6 = this.getDoubleFromString(strArray24[index29]);
        fwData13.initialVelocity = (float) doubleFromString6;
        WorldMap.FWData fwData14 = fwData1;
        string[] strArray25 = strArray1;
        int index30 = num26;
        int num27 = index30 + 1;
        double doubleFromString7 = this.getDoubleFromString(strArray25[index30]);
        fwData14.maxVelocity = (float) doubleFromString7;
        WorldMap.FWData fwData15 = fwData1;
        string[] strArray26 = strArray1;
        int index31 = num27;
        int num28 = index31 + 1;
        double doubleFromString8 = this.getDoubleFromString(strArray26[index31]);
        fwData15.acceleration = (float) doubleFromString8;
        WorldMap.FWData fwData16 = fwData1;
        string[] strArray27 = strArray1;
        int index32 = num28;
        int num29 = index32 + 1;
        double doubleFromString9 = this.getDoubleFromString(strArray27[index32]);
        fwData16.speedVariance = (float) doubleFromString9;
        WorldMap.FWData fwData17 = fwData1;
        string[] strArray28 = strArray1;
        int index33 = num29;
        num8 = index33 + 1;
        int int32FromString8 = this.getInt32FromString(strArray28[index33]);
        fwData17.childFirework = int32FromString8;
      }
    }

    public void clearFW()
    {
      this.clusters.Clear();
      this.fwMode = 0;
      this.fwTick = 0;
      this.fwDisplayClock = 0.0f;
    }

    public void updateFW()
    {
      if ((double) this.fwDisplayClock > 1800.0)
        return;
      int num1 = this.totalNumFW;
      ++this.fwTick;
      if ((double) this.fwDisplayClock > 1590.0)
        this.fwMode = 2;
      else if (this.fwTick > this.fwCycle * 30)
      {
        this.fwTick = 0;
        int maxValue = this.fwNormalChance + this.fwBusyChance + this.fwCrazyChance;
        if (maxValue > 0)
        {
          if (new Random().Next(maxValue) < this.fwNormalChance)
            this.fwMode = 0;
          this.fwMode = 1;
        }
      }
      switch (this.fwMode)
      {
        case 0:
          num1 = this.totalNumFW;
          break;
        case 1:
          num1 = this.totalNumFWBusy;
          break;
        case 2:
          num1 = this.totalNumFWCrazy;
          break;
      }
      if (this.clusters.Count >= num1)
        return;
      double width = (double) this.m_screenWidth / this.m_worldScale;
      double height = (double) this.m_screenHeight / this.m_worldScale;
      RectangleF screenRect = new RectangleF((float) (this.m_screenCentreX - width / 2.0), (float) (this.m_screenCentreY - height / 2.0), (float) width, (float) height);
      List<VillageData> villageDataList = new List<VillageData>();
      foreach (VillageData village in this.villageList)
      {
        if (village.visible && this.isVisibleClose(screenRect, (float) village.x, (float) village.y))
          villageDataList.Add(village);
      }
      if (villageDataList.Count <= 0)
        return;
      DateTime now = DateTime.Now;
      Random random = new Random();
      int num2 = num1 - this.clusters.Count;
      for (int index = 0; index < num2; ++index)
      {
        WorldMap.ClusterBase clusterBase = new WorldMap.ClusterBase();
        clusterBase.unique = this.fwUnique++;
        clusterBase.type = random.Next(4);
        float num3 = 1f;
        switch (clusterBase.type)
        {
          case 0:
            clusterBase.fwS = this.fwJesterOrder;
            num3 = 1.5f;
            break;
          case 1:
            clusterBase.fwS = this.fwChickenOrder;
            num3 = 0.5f;
            break;
          case 2:
            clusterBase.fwS = this.fwSheepOrder;
            num3 = 0.66f;
            break;
          case 3:
            clusterBase.fwS = this.fwPigOrder;
            num3 = 1f;
            break;
        }
        VillageData villageData = villageDataList[random.Next(villageDataList.Count)];
        clusterBase.startVillage = villageData.id;
        float num4 = (float) random.Next(15, 24);
        PointF point = new PointF(num4 * num3, 0.0f);
        point = GameEngine.Instance.GFX.rotatePoint(point, (float) random.Next(360));
        clusterBase.targetX = (int) villageData.x + (int) point.X;
        clusterBase.targetY = (int) villageData.y + (int) point.Y;
        clusterBase.startTime = now;
        clusterBase.endTime = now.AddSeconds((double) num4 / 10.0);
        clusterBase.spriteID = clusterBase.type + 456;
        this.clusters.Add(clusterBase);
        villageDataList.Remove(villageData);
        if (villageDataList.Count <= 0)
          break;
      }
    }

    public void drawFW(RectangleF screenRect)
    {
      float num1 = (float) (this.m_worldScale / 28.0 / 0.60000002384185791);
      float num2 = 1f;
      this.fwDisplayClock += num2;
      if ((double) num1 < 0.10000000149011612)
        num1 = 0.1f;
      if ((double) num1 > 1.0)
        num1 = 1f;
      DateTime now = DateTime.Now;
      Random random = new Random();
      List<WorldMap.ClusterBase> clusterBaseList = new List<WorldMap.ClusterBase>();
      foreach (WorldMap.ClusterBase cluster in this.clusters)
      {
        if (cluster.parentVisible)
        {
          float num3 = (float) ((now - cluster.startTime).TotalMilliseconds / (cluster.endTime - cluster.startTime).TotalMilliseconds);
          if ((double) num3 > 1.0)
            num3 = 1f;
          if (cluster.endTime <= now)
          {
            cluster.parentVisible = false;
            for (int index1 = 0; index1 < 3; ++index1)
            {
              int index2 = cluster.fwS[index1];
              if (index2 > 0)
              {
                WorldMap.FWData fwData = this.fwDataList[index2];
                int spriteId = fwData.spriteID;
                int numToSpawn = fwData.numToSpawn;
                for (int index3 = 0; index3 < numToSpawn; ++index3)
                {
                  WorldMap.Burst burst = new WorldMap.Burst();
                  burst.unique = this.fwUnique++;
                  burst.definition = fwData;
                  burst.startX = (float) cluster.targetX;
                  burst.startY = (float) cluster.targetY;
                  float x = 1f;
                  if ((double) burst.definition.speedVariance != 0.0)
                    x += (float) (random.NextDouble() * (double) burst.definition.speedVariance - (double) burst.definition.speedVariance / 2.0);
                  PointF point = new PointF(x, 0.0f);
                  point = !burst.definition.symmetrical ? GameEngine.Instance.GFX.rotatePoint(point, (float) random.Next(360)) : GameEngine.Instance.GFX.rotatePoint(point, (float) index3 * 360f / (float) numToSpawn);
                  if (burst.definition.randomStartRotation)
                    burst.curRotation = (float) random.Next(360);
                  burst.rotationValue = burst.definition.rotateSpeed;
                  if (!burst.definition.rotateClockwise)
                    burst.rotationValue = 0.0f - burst.rotationValue;
                  burst.scale = burst.definition.startScale;
                  if ((double) burst.definition.startScale != (double) burst.definition.scaleTarget)
                  {
                    burst.scaleDiff = burst.definition.scaleSpeed;
                    if ((double) burst.definition.startScale > (double) burst.definition.scaleTarget)
                      burst.scaleDiff = 0.0f - burst.scaleDiff;
                  }
                  burst.dX = point.X;
                  burst.dY = point.Y;
                  burst.speed = burst.definition.initialVelocity;
                  if ((double) burst.definition.initialVelocity != (double) burst.definition.maxVelocity)
                  {
                    burst.acceleration = burst.definition.acceleration;
                    if ((double) burst.definition.initialVelocity > (double) burst.definition.maxVelocity)
                      burst.acceleration = 0.0f - burst.acceleration;
                  }
                  burst.startTime = now;
                  burst.spriteID = spriteId;
                  cluster.bursts.Add(burst);
                }
              }
            }
          }
          VillageData village = this.villageList[cluster.startVillage];
          float x1 = (float) village.x;
          float y = (float) village.y;
          float displayX = ((float) cluster.targetX - x1) * num3 + x1;
          float displayY = ((float) cluster.targetY - y) * num3 + y;
          if (this.isVisible(screenRect, displayX, displayY))
          {
            this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
            this.villageSprite.SpriteNo = cluster.spriteID;
            this.villageSprite.PosX = (displayX - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
            this.villageSprite.PosY = (displayY - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
            this.villageSprite.Center = new PointF(44f, 44f);
            this.villageSprite.Scale = num1;
            this.villageSprite.Update();
            this.doDraw(this.villageSprite);
          }
        }
        if (!cluster.parentVisible)
        {
          List<WorldMap.Burst> burstList = new List<WorldMap.Burst>();
          List<WorldMap.Burst> collection = new List<WorldMap.Burst>();
          foreach (WorldMap.Burst burst1 in cluster.bursts)
          {
            TimeSpan timeSpan = now - burst1.startTime;
            switch (burst1.fadeState)
            {
              case 0:
                if ((int) timeSpan.TotalMilliseconds > burst1.definition.fadeInTime)
                {
                  burst1.fadeState = 1;
                  break;
                }
                break;
              case 1:
                burst1.alpha += burst1.definition.fadeRate;
                if ((double) burst1.alpha >= 1.0)
                {
                  burst1.alpha = 1f;
                  burst1.fadeState = 2;
                  break;
                }
                break;
              case 2:
                if ((int) timeSpan.TotalMilliseconds > burst1.definition.fadeOutTime)
                {
                  burst1.fadeState = 3;
                  break;
                }
                break;
              case 3:
                burst1.alpha -= burst1.definition.fadeRate;
                if ((double) burst1.alpha <= 0.0)
                {
                  burst1.alpha = 0.0f;
                  burst1.fadeState = 4;
                  break;
                }
                break;
            }
            if (burst1.fadeState == 4)
            {
              burstList.Add(burst1);
              if (burst1.definition.childFirework > 0)
              {
                int childFirework = burst1.definition.childFirework;
                if (childFirework > 0)
                {
                  WorldMap.FWData fwData = this.fwDataList[childFirework];
                  int spriteId = fwData.spriteID;
                  int numToSpawn = fwData.numToSpawn;
                  for (int index = 0; index < numToSpawn; ++index)
                  {
                    WorldMap.Burst burst2 = new WorldMap.Burst();
                    burst2.unique = this.fwUnique++;
                    burst2.definition = fwData;
                    burst2.startX = burst1.startX;
                    burst2.startY = burst1.startY;
                    float x = 1f;
                    if ((double) burst2.definition.speedVariance != 0.0)
                      x += (float) (random.NextDouble() * (double) burst2.definition.speedVariance - (double) burst2.definition.speedVariance / 2.0);
                    PointF point = new PointF(x, 0.0f);
                    point = !burst2.definition.symmetrical ? GameEngine.Instance.GFX.rotatePoint(point, (float) random.Next(360)) : GameEngine.Instance.GFX.rotatePoint(point, (float) index * 360f / (float) numToSpawn);
                    if (burst2.definition.randomStartRotation)
                      burst2.curRotation = (float) random.Next(360);
                    burst2.rotationValue = burst2.definition.rotateSpeed;
                    if (!burst2.definition.rotateClockwise)
                      burst2.rotationValue = 0.0f - burst2.rotationValue;
                    burst2.scale = burst2.definition.startScale;
                    if ((double) burst2.definition.startScale != (double) burst2.definition.scaleTarget)
                    {
                      burst2.scaleDiff = burst2.definition.scaleSpeed;
                      if ((double) burst2.definition.startScale > (double) burst2.definition.scaleTarget)
                        burst2.scaleDiff = 0.0f - burst2.scaleDiff;
                    }
                    burst2.dX = point.X;
                    burst2.dY = point.Y;
                    burst2.speed = burst2.definition.initialVelocity;
                    if ((double) burst2.definition.initialVelocity != (double) burst2.definition.maxVelocity)
                    {
                      burst2.acceleration = burst2.definition.acceleration;
                      if ((double) burst2.definition.initialVelocity > (double) burst2.definition.maxVelocity)
                        burst2.acceleration = 0.0f - burst2.acceleration;
                    }
                    burst2.startTime = now;
                    burst2.spriteID = spriteId;
                    collection.Add(burst2);
                  }
                }
              }
            }
            float startX = burst1.startX;
            float startY = burst1.startY;
            burst1.startX += burst1.dX * burst1.speed * num2;
            burst1.startY += burst1.dY * burst1.speed * num2;
            if ((double) burst1.acceleration != 0.0)
            {
              burst1.speed += burst1.acceleration * num2;
              if ((double) burst1.acceleration > 0.0)
              {
                if ((double) burst1.speed >= (double) burst1.definition.maxVelocity)
                {
                  burst1.speed = burst1.definition.maxVelocity;
                  burst1.acceleration = 0.0f;
                }
              }
              else if ((double) burst1.speed <= (double) burst1.definition.maxVelocity)
              {
                burst1.speed = burst1.definition.maxVelocity;
                burst1.acceleration = 0.0f;
              }
            }
            if ((double) burst1.rotationValue != 0.0)
            {
              burst1.curRotation += burst1.rotationValue * num2;
              if ((double) burst1.curRotation < 0.0)
                burst1.curRotation += 360f;
              else if ((double) burst1.curRotation >= 360.0)
                burst1.curRotation -= 360f;
            }
            if ((double) burst1.scaleDiff > 0.0)
            {
              burst1.scale += burst1.scaleDiff * num2;
              if ((double) burst1.scale > (double) burst1.definition.scaleTarget)
                burst1.scale = burst1.definition.scaleTarget;
            }
            else if ((double) burst1.scaleDiff < 0.0)
            {
              burst1.scale += burst1.scaleDiff * num2;
              if ((double) burst1.scale < (double) burst1.definition.scaleTarget)
                burst1.scale = burst1.definition.scaleTarget;
            }
            if (this.isVisible(screenRect, startX, startY))
            {
              this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
              this.villageSprite.SpriteNo = burst1.spriteID;
              this.villageSprite.PosX = (startX - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
              this.villageSprite.PosY = (startY - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
              this.villageSprite.Center = new PointF(44f, 44f);
              this.villageSprite.ColorToUse = Color.FromArgb((int) ((double) burst1.alpha * (double) byte.MaxValue), ARGBColors.White);
              this.villageSprite.RotationAngle = burst1.curRotation;
              this.villageSprite.Scale = burst1.scale * num1;
              this.villageSprite.Update();
              this.doDraw(this.villageSprite);
            }
          }
          if (burstList.Count > 0)
          {
            foreach (WorldMap.Burst burst in burstList)
              cluster.bursts.Remove(burst);
          }
          if (collection.Count > 0)
            cluster.bursts.AddRange((IEnumerable<WorldMap.Burst>) collection);
          if (cluster.bursts.Count == 0)
            clusterBaseList.Add(cluster);
        }
      }
      if (clusterBaseList.Count <= 0)
        return;
      foreach (WorldMap.ClusterBase clusterBase in clusterBaseList)
        this.clusters.Remove(clusterBase);
    }

    public int MostAge4Villages
    {
      get => !GameEngine.Instance.LocalWorldData.IsHereticEUAIWorld ? this.m_mostAge4Villages : 0;
      set => this.m_mostAge4Villages = value;
    }

    public void initUserVillages()
    {
    }

    public void retrieveUserVillages(bool force)
    {
      if (this.retrievingUserVillages && !force)
        return;
      this.retrievingUserVillages = true;
      RemoteServices.Instance.set_GetUserVillages_UserCallBack(new RemoteServices.GetUserVillages_UserCallBack(this.getUserVillages));
      RemoteServices.Instance.GetUserVillages();
    }

    public bool isRetrievingUserVillages() => this.retrievingUserVillages;

    public void getUserVillages(GetUserVillages_ReturnType returnData)
    {
      this.retrievingUserVillages = false;
      if (returnData.Success)
      {
        this.loadingErrored = false;
        this.doGetUserVillages(returnData.userVillageList, returnData.userVillageNameList);
        this.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
        this.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
        this.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
        this.setRanking(returnData.rank, returnData.rankSubLevel);
      }
      else
        this.loadingErrored = true;
    }

    public void doGetUserVillages(List<int> userVillageList, List<string> userVillageNameList)
    {
      foreach (VillageData village in this.villageList)
        village.userVillageID = -1;
      if (this.m_userVillages != null)
      {
        int count1 = this.m_userVillages.Count;
      }
      int count2 = userVillageList.Count;
      this.m_userVillages = new List<WorldMap.UserVillageData>();
      for (int index = 0; index < count2; ++index)
      {
        WorldMap.UserVillageData userVillageData = new WorldMap.UserVillageData();
        userVillageData.villageID = userVillageList[index];
        this.m_userVillages.Add(userVillageData);
        this.villageList[userVillageData.villageID].userVillageID = index;
        this.villageList[userVillageData.villageID].visible = true;
        this.villageList[userVillageData.villageID].userID = RemoteServices.Instance.UserID;
        this.villageList[userVillageData.villageID].factionID = RemoteServices.Instance.UserFactionID;
      }
      this.updateUserRelatedVillages();
      this.sortUserVillages();
      InterfaceMgr.Instance.validateUserVillage();
      this.updateUserVassals();
    }

    public void updateUserCapitals(int[] userCapitals)
    {
      foreach (int userCapital in userCapitals)
      {
        if (!this.isUserVillage(userCapital))
          this.addUserVillage(userCapital);
      }
      if (this.m_userVillages != null)
      {
        bool flag1 = false;
        foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        {
          if (userVillage.capital)
          {
            bool flag2 = false;
            for (int index = 0; index < userCapitals.Length; ++index)
            {
              if (userCapitals[index] == userVillage.villageID)
              {
                flag2 = true;
                break;
              }
            }
            if (!flag2)
            {
              flag1 = true;
              break;
            }
          }
        }
        if (flag1)
          this.retrieveUserVillages(false);
      }
      this.sortUserVillages();
      this.updateUserRelatedVillages();
    }

    public void addUserVillage(int villageID)
    {
      bool flag = false;
      int num = 0;
      if (this.m_userVillages != null)
      {
        foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        {
          if (userVillage.villageID == villageID)
          {
            this.villageList[userVillage.villageID].userVillageID = num;
            flag = true;
            break;
          }
          ++num;
        }
        if (!flag)
        {
          WorldMap.UserVillageData userVillageData = new WorldMap.UserVillageData();
          userVillageData.villageID = villageID;
          this.m_userVillages.Add(userVillageData);
          this.villageList[userVillageData.villageID].userVillageID = this.m_userVillages.Count - 1;
        }
        this.sortUserVillages();
      }
      this.updateUserRelatedVillages();
    }

    public void setWorldStartDate(DateTime startDate)
    {
      this.m_worldStartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0, 0);
    }

    public int getGameDay()
    {
      return (int) (VillageMap.getCurrentServerTime() - this.m_worldStartDate).TotalDays;
    }

    public void setGoldData(double goldLevel, double goldRate)
    {
      this.m_userGoldLevel = goldLevel;
      this.m_userGoldIncomeRate = goldRate;
      this.m_lastGoldUpdate = DXTimer.GetCurrentMilliseconds();
    }

    public void setHonourData(double honourLevel, double honourRate)
    {
      this.m_userHonourLevel = honourLevel;
      this.m_userHonourIncomeRate = honourRate;
      this.m_lastHonourUpdate = DXTimer.GetCurrentMilliseconds();
    }

    public void setFaithPointsData(double faithPointsLevel, double faithPointsRate)
    {
      this.m_userFaithPointsLevel = faithPointsLevel;
      this.m_userFaithPointsRate = faithPointsRate;
      this.m_lastFaithPointsUpdate = DXTimer.GetCurrentMilliseconds();
    }

    public void setPoints(int points) => this.m_userPoints = points;

    public void setNumMadeCaptains(int numCaptains) => this.m_numMadeCaptains = numCaptains;

    public void setRanking(int rank, int rankSubLevel)
    {
      this.m_userRank = rank;
      this.m_userRankSubLevel = rankSubLevel;
      InterfaceMgr.Instance.setRank(rank);
    }

    public void reSetRanking() => InterfaceMgr.Instance.setRank(this.m_userRank);

    public bool isUserVillage(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && this.villageList[villageID].userVillageID != -1;
    }

    public bool isUserRelatedVillage(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && this.villageList[villageID].userRelatedVillage;
    }

    public void updateLocalVillagesFromFactions()
    {
      if (this.m_userVillages == null)
        return;
      foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
      {
        if (this.villageList[userVillage.villageID].factionID != RemoteServices.Instance.UserFactionID)
          this.villageList[userVillage.villageID].userVillageID = -1;
      }
      this.updateUserRelatedVillages();
    }

    public void updateYourVillageFactions(int yourNewFaction)
    {
      if (this.m_userVillages == null)
        return;
      foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        this.villageList[userVillage.villageID].factionID = yourNewFaction;
    }

    public List<int> getListOfUserVillages()
    {
      List<int> listOfUserVillages = new List<int>();
      if (this.m_userVillages == null)
        return listOfUserVillages;
      foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
      {
        if (!userVillage.capital)
          listOfUserVillages.Add(userVillage.villageID);
      }
      return listOfUserVillages;
    }

    public List<int> getListOfUserParishes()
    {
      List<int> listOfUserParishes = new List<int>();
      if (this.m_userVillages == null)
        return listOfUserParishes;
      foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
      {
        if (!userVillage.capital)
        {
          int regionId = (int) this.villageList[userVillage.villageID].regionID;
          if (!listOfUserParishes.Contains(regionId))
            listOfUserParishes.Add(regionId);
        }
      }
      return listOfUserParishes;
    }

    public List<int> getListOfUserCounties()
    {
      List<int> listOfUserCounties = new List<int>();
      foreach (int listOfUserVillage in this.getListOfUserVillages())
      {
        int countyFromVillageId = this.getCountyFromVillageID(listOfUserVillage);
        if (!listOfUserCounties.Contains(countyFromVillageId))
          listOfUserCounties.Add(countyFromVillageId);
      }
      return listOfUserCounties;
    }

    public List<int> getListOfUserProvinces()
    {
      List<int> listOfUserProvinces = new List<int>();
      foreach (int listOfUserVillage in this.getListOfUserVillages())
      {
        int provinceFromVillageId = this.getProvinceFromVillageID(listOfUserVillage);
        if (!listOfUserProvinces.Contains(provinceFromVillageId))
          listOfUserProvinces.Add(provinceFromVillageId);
      }
      return listOfUserProvinces;
    }

    public List<int> getListOfUserCountries()
    {
      List<int> listOfUserCountries = new List<int>();
      foreach (int listOfUserVillage in this.getListOfUserVillages())
      {
        int countryFromVillageId = this.getCountryFromVillageID(listOfUserVillage);
        if (!listOfUserCountries.Contains(countryFromVillageId))
          listOfUserCountries.Add(countryFromVillageId);
      }
      return listOfUserCountries;
    }

    public void addGold(double gold) => this.m_userGoldLevel += gold;

    public double getCurrentGold()
    {
      double currentGold = this.m_userGoldLevel + this.m_userGoldIncomeRate * ((DXTimer.GetCurrentMilliseconds() - this.m_lastGoldUpdate) / 1000.0);
      if (currentGold < 0.0)
        currentGold = 0.0;
      return currentGold;
    }

    public double getCurrentGoldRate() => this.m_userGoldIncomeRate;

    public void addHonour(double honour) => this.m_userHonourLevel += honour;

    public double getCurrentHonour()
    {
      return this.m_userHonourLevel + this.m_userHonourIncomeRate * ((DXTimer.GetCurrentMilliseconds() - this.m_lastHonourUpdate) / 1000.0);
    }

    public bool IsPlayerVillage(int villageID)
    {
        if (villageID < 0 || villageID >= this.villageList.Length)
            return false;
        VillageData village = this.villageList[villageID];
        return village.visible && !village.Capital && village.special == 0 && village.userID >= 0;
    }

        public double getCurrentHonourRate() => this.m_userHonourIncomeRate;

    public double getCurrentFaithPoints()
    {
      double currentFaithPoints = this.m_userFaithPointsLevel + this.m_userFaithPointsRate * ((DXTimer.GetCurrentMilliseconds() - this.m_lastFaithPointsUpdate) / 1000.0);
      if (GameEngine.Instance.LocalWorldData.EraWorld)
      {
        int index = this.getRank();
        if (index < 0)
          index = 0;
        else if (index >= VillageBuildingsData.faithPointCap_EraWorlds.Length)
          index = VillageBuildingsData.faithPointCap_EraWorlds.Length - 1;
        int pointCapEraWorld = VillageBuildingsData.faithPointCap_EraWorlds[index];
        if (currentFaithPoints > (double) pointCapEraWorld)
          currentFaithPoints = (double) pointCapEraWorld;
      }
      return currentFaithPoints;
    }

    public void addFaithPoints(double amount) => this.m_userFaithPointsLevel += amount;

    public double getCurrentFaithPointsRate() => this.m_userFaithPointsRate;

    public int getCurrentPoints() => this.m_userPoints;

    public int getNumMadeCaptains() => this.m_numMadeCaptains <= 0 ? 1 : this.m_numMadeCaptains;

    public int getRank() => this.m_userRank;

    public int getRankSubLevel() => this.m_userRankSubLevel;

    public bool canUserOwnMoreVillages()
    {
      int num = this.numVillagesAllowed();
      return this.numVillagesOwned() < num;
    }

    public bool canUserOwnMoreVassals()
    {
      int userRank = this.m_userRank;
      int maxVassals = GameEngine.Instance.LocalWorldData.getMaxVassals(userRank, this.m_userRankSubLevel);
      return this.countVassals() < maxVassals;
    }

    public bool isVillageAVassal(int villageID)
    {
      if (this.m_userVillages == null)
        return false;
      foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
      {
        foreach (int vassal in userVillage.vassals)
        {
          if (vassal == villageID)
            return true;
        }
      }
      return false;
    }

    public int countVassals()
    {
      int num = 0;
      if (this.m_userVillages != null)
      {
        foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        {
          if (userVillage.vassals != null)
            num += userVillage.vassals.Count;
        }
      }
      return num;
    }

    public int numVillagesAllowed()
    {
      int num = ResearchData.leadershipVillages[(int) this.UserResearchData.Research_Leadership];
      if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
        num = ResearchData.leadershipVillages2[(int) this.UserResearchData.Research_Leadership];
      if (num >= 25 && GameEngine.Instance.World.FourthAgeWorld)
        num = 40;
      else if (num >= 25 && GameEngine.Instance.World.ThirdAgeWorld)
        num = 30;
      return num;
    }

    public int numVillagesOwned()
    {
      if (this.m_userVillages == null)
        return 1;
      int num = 0;
      int count = this.m_userVillages.Count;
      for (int index = 0; index < count; ++index)
      {
        if (!this.villageList[this.m_userVillages[index].villageID].Capital)
          ++num;
      }
      return num;
    }

    public int numVassalsAllowed()
    {
      int userRank = this.m_userRank;
      return userRank < 0 ? 0 : GameEngine.Instance.LocalWorldData.getMaxVassals(userRank, this.m_userRankSubLevel);
    }

    public int numVassalsAllowed(int rank)
    {
      return rank < 0 ? 0 : GameEngine.Instance.LocalWorldData.getMaxVassals(rank, this.m_userRankSubLevel);
    }

    public int numVassalsOwned() => 0;

    public double calcVillageDistance(int fromVillageID, int villageID)
    {
      if (this.m_userVillages == null || villageID < 0 || villageID >= this.villageList.Length || fromVillageID < 0 || fromVillageID >= this.villageList.Length)
        return 0.0;
      int regionId1 = (int) this.villageList[villageID].regionID;
      int capitalVillage1 = this.regionList[regionId1].capitalVillage;
      int regionId2 = (int) this.villageList[fromVillageID].regionID;
      if (regionId2 == regionId1 || regionId2 < 0 || regionId1 < 0)
        return 0.0;
      int capitalVillage2 = this.regionList[regionId2].capitalVillage;
      int num1 = (int) this.villageList[capitalVillage1].x - (int) this.villageList[capitalVillage2].x;
      int num2 = (int) this.villageList[capitalVillage1].y - (int) this.villageList[capitalVillage2].y;
      int num3 = (int) Math.Sqrt((double) (num1 * num1 + num2 * num2));
      if (num3 > GameEngine.Instance.LocalWorldData.maxVillageCostDistance)
        num3 = GameEngine.Instance.LocalWorldData.maxVillageCostDistance;
      return (double) num3 / (double) GameEngine.Instance.LocalWorldData.maxVillageCostDistance;
    }

    public void updateUserRelatedVillages()
    {
      if (this.m_userVillages == null)
        return;
      foreach (WorldMap.UserVillageData userRelatedVillage in this.m_userRelatedVillages)
      {
        if (userRelatedVillage.villageID >= 0 && userRelatedVillage.villageID < this.villageList.Length)
          this.villageList[userRelatedVillage.villageID].userRelatedVillage = false;
      }
      this.m_userRelatedVillages.Clear();
      List<int> intList = new List<int>();
      try
      {
        foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        {
          if (!this.villageList[userVillage.villageID].Capital)
          {
            int capitalVillage = this.regionList[(int) this.villageList[userVillage.villageID].regionID].capitalVillage;
            if (!intList.Contains(capitalVillage) && !this.isUserVillage(capitalVillage))
            {
              intList.Add(capitalVillage);
              this.m_userRelatedVillages.Add(new WorldMap.UserVillageData()
              {
                villageID = capitalVillage,
                capital = true,
                parishCapital = true
              });
              this.villageList[capitalVillage].userRelatedVillage = true;
            }
          }
        }
        foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        {
          if (!this.villageList[userVillage.villageID].Capital)
          {
            int capitalVillage = this.countyList[(int) this.villageList[userVillage.villageID].countyID].capitalVillage;
            if (!intList.Contains(capitalVillage) && !this.isUserVillage(capitalVillage))
            {
              intList.Add(capitalVillage);
              this.m_userRelatedVillages.Add(new WorldMap.UserVillageData()
              {
                villageID = capitalVillage,
                capital = true,
                countyCapital = true
              });
              this.villageList[capitalVillage].userRelatedVillage = true;
            }
          }
        }
        foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        {
          if (!this.villageList[userVillage.villageID].Capital)
          {
            int capitalVillage = this.provincesList[this.countyList[(int) this.villageList[userVillage.villageID].countyID].parentID].capitalVillage;
            if (!intList.Contains(capitalVillage) && !this.isUserVillage(capitalVillage))
            {
              intList.Add(capitalVillage);
              this.m_userRelatedVillages.Add(new WorldMap.UserVillageData()
              {
                villageID = capitalVillage,
                capital = true,
                provinceCapital = true
              });
              this.villageList[capitalVillage].userRelatedVillage = true;
            }
          }
        }
        foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        {
          if (!this.villageList[userVillage.villageID].Capital)
          {
            int capitalVillage = this.countryList[this.provincesList[this.countyList[(int) this.villageList[userVillage.villageID].countyID].parentID].parentID].capitalVillage;
            if (!intList.Contains(capitalVillage) && !this.isUserVillage(capitalVillage))
            {
              intList.Add(capitalVillage);
              this.m_userRelatedVillages.Add(new WorldMap.UserVillageData()
              {
                villageID = capitalVillage,
                capital = true,
                countryCapital = true
              });
              this.villageList[capitalVillage].userRelatedVillage = true;
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    public int getNextUserVillage(int curVillage, int searchDir)
    {
      if (this.m_userVillages == null || this.m_userVillages.Count == 0)
        return -1;
      int index = -1;
      bool flag = false;
      List<WorldMap.UserVillageData> userVillageDataList = new List<WorldMap.UserVillageData>();
      userVillageDataList.AddRange((IEnumerable<WorldMap.UserVillageData>) this.m_userVillages);
      userVillageDataList.AddRange((IEnumerable<WorldMap.UserVillageData>) this.m_userRelatedVillages);
      if (curVillage >= 0 && curVillage < this.villageList.Length)
      {
        index = 0;
        foreach (WorldMap.UserVillageData userVillageData in userVillageDataList)
        {
          if (userVillageData.villageID != curVillage)
            ++index;
          else
            break;
        }
        if (this.villageList[curVillage].Capital)
          flag = true;
      }
      int num1 = 0;
      int count = userVillageDataList.Count;
      int num2 = 0;
      while (num2 < count)
      {
        index += searchDir;
        if (index < 0)
          index = userVillageDataList.Count - 1;
        if (index >= count)
          index = 0;
        if (flag)
        {
          if (this.villageList[userVillageDataList[index].villageID].Capital)
            break;
        }
        else if (!this.villageList[userVillageDataList[index].villageID].Capital)
          break;
        ++num1;
        if (num1 > 1000)
          return -1;
      }
      return userVillageDataList[index].villageID;
    }

    public int getPlayerChildVillageFromCapital(int capitalID)
    {
      if (this.m_userVillages != null)
      {
        foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        {
          VillageData village = this.villageList[userVillage.villageID];
          if (this.getRegionCapitalVillage((int) village.regionID) == capitalID || this.getCountyCapitalVillage((int) village.countyID) == capitalID || this.getProvinceCapital(this.getProvinceFromVillageID(userVillage.villageID)) == capitalID || this.getCountryCapital(this.getCountryFromVillageID(userVillage.villageID)) == capitalID)
            return userVillage.villageID;
        }
      }
      return capitalID;
    }

    public List<WorldMap.UserVillageData> getUserVillageList() => this.m_userVillages;

    public List<int> getUserVillageIDList()
    {
      List<int> userVillageIdList = new List<int>();
      if (this.m_userVillages != null)
      {
        foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        {
          if (!this.villageList[userVillage.villageID].Capital)
            userVillageIdList.Add(userVillage.villageID);
        }
      }
      return userVillageIdList;
    }

    public List<WorldMap.VillageNameItem> getUserVillageNamesList()
    {
      List<WorldMap.VillageNameItem> villageNamesList = new List<WorldMap.VillageNameItem>();
      if (this.m_userVillages == null)
        return villageNamesList;
      int num1 = 0;
      foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
      {
        if (this.villageList[userVillage.villageID].Capital)
        {
          int num2 = 1;
          if (this.villageList[userVillage.villageID].regionCapital)
            num2 = 1;
          else if (this.villageList[userVillage.villageID].countyCapital)
            num2 = 2;
          else if (this.villageList[userVillage.villageID].provinceCapital)
            num2 = 3;
          else if (this.villageList[userVillage.villageID].countryCapital)
            num2 = 4;
          if (num1 != num2)
          {
            villageNamesList.Add(new WorldMap.VillageNameItem()
            {
              villageName = "-----------------",
              villageID = -1
            });
            num1 = num2;
          }
        }
        villageNamesList.Add(new WorldMap.VillageNameItem()
        {
          villageName = this.getVillageName(userVillage.villageID),
          villageID = userVillage.villageID,
          capital = userVillage.capital
        });
      }
      return villageNamesList;
    }

    public List<WorldMap.VillageNameItem> getUserVillageNamesListAndCapitals()
    {
      List<WorldMap.VillageNameItem> namesListAndCapitals = new List<WorldMap.VillageNameItem>();
      if (this.m_userVillages == null)
        return namesListAndCapitals;
      for (int index = 0; index < 5; ++index)
      {
        if (index > 0)
        {
          WorldMap.VillageNameItem villageNameItem = new WorldMap.VillageNameItem();
          switch (index)
          {
            case 1:
              villageNameItem.villageName = SK.Text("GENERIC_Parishes", "Parishes");
              villageNameItem.villageID = -1;
              namesListAndCapitals.Add(villageNameItem);
              break;
            case 2:
              villageNameItem.villageName = SK.Text("GENERIC_Counties", "Counties");
              villageNameItem.villageID = -1;
              namesListAndCapitals.Add(villageNameItem);
              break;
            case 3:
              villageNameItem.villageName = SK.Text("GENERIC_Provinces", "Provinces");
              villageNameItem.villageID = -1;
              namesListAndCapitals.Add(villageNameItem);
              break;
            case 4:
              villageNameItem.villageName = SK.Text("GENERIC_Countries", "Countries");
              villageNameItem.villageID = -1;
              namesListAndCapitals.Add(villageNameItem);
              break;
          }
        }
        foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        {
          bool flag = false;
          switch (index)
          {
            case 0:
              if (!userVillage.capital)
              {
                flag = true;
                break;
              }
              break;
            case 1:
              if (userVillage.parishCapital)
              {
                flag = true;
                break;
              }
              break;
            case 2:
              if (userVillage.countyCapital)
              {
                flag = true;
                break;
              }
              break;
            case 3:
              if (userVillage.provinceCapital)
              {
                flag = true;
                break;
              }
              break;
            case 4:
              if (userVillage.countryCapital)
              {
                flag = true;
                break;
              }
              break;
          }
          if (flag)
          {
            WorldMap.VillageNameItem villageNameItem1 = new WorldMap.VillageNameItem();
            villageNameItem1.villageName = this.getVillageName(userVillage.villageID);
            switch (index)
            {
              case 2:
                WorldMap.VillageNameItem villageNameItem2 = villageNameItem1;
                villageNameItem2.villageName = villageNameItem2.villageName + " / " + this.getCountyName(this.getCountyFromVillageID(userVillage.villageID));
                break;
              case 3:
                WorldMap.VillageNameItem villageNameItem3 = villageNameItem1;
                villageNameItem3.villageName = villageNameItem3.villageName + " / " + this.getProvinceName(this.getProvinceFromVillageID(userVillage.villageID));
                break;
              case 4:
                WorldMap.VillageNameItem villageNameItem4 = villageNameItem1;
                villageNameItem4.villageName = villageNameItem4.villageName + " / " + this.getCountryName(this.getCountryFromVillageID(userVillage.villageID));
                break;
            }
            villageNameItem1.villageID = userVillage.villageID;
            villageNameItem1.capital = userVillage.capital;
            namesListAndCapitals.Add(villageNameItem1);
          }
        }
        foreach (WorldMap.UserVillageData userRelatedVillage in this.m_userRelatedVillages)
        {
          bool flag = false;
          switch (index)
          {
            case 0:
              if (!userRelatedVillage.capital)
              {
                flag = true;
                break;
              }
              break;
            case 1:
              if (userRelatedVillage.parishCapital)
              {
                flag = true;
                break;
              }
              break;
            case 2:
              if (userRelatedVillage.countyCapital)
              {
                flag = true;
                break;
              }
              break;
            case 3:
              if (userRelatedVillage.provinceCapital)
              {
                flag = true;
                break;
              }
              break;
            case 4:
              if (userRelatedVillage.countryCapital)
              {
                flag = true;
                break;
              }
              break;
          }
          if (flag)
          {
            WorldMap.VillageNameItem villageNameItem5 = new WorldMap.VillageNameItem();
            villageNameItem5.villageName = this.getVillageName(userRelatedVillage.villageID);
            switch (index)
            {
              case 2:
                WorldMap.VillageNameItem villageNameItem6 = villageNameItem5;
                villageNameItem6.villageName = villageNameItem6.villageName + " / " + this.getCountyName(this.getCountyFromVillageID(userRelatedVillage.villageID));
                break;
              case 3:
                WorldMap.VillageNameItem villageNameItem7 = villageNameItem5;
                villageNameItem7.villageName = villageNameItem7.villageName + " / " + this.getProvinceName(this.getProvinceFromVillageID(userRelatedVillage.villageID));
                break;
              case 4:
                WorldMap.VillageNameItem villageNameItem8 = villageNameItem5;
                villageNameItem8.villageName = villageNameItem8.villageName + " / " + this.getCountryName(this.getCountryFromVillageID(userRelatedVillage.villageID));
                break;
            }
            villageNameItem5.villageID = userRelatedVillage.villageID;
            villageNameItem5.capital = userRelatedVillage.capital;
            namesListAndCapitals.Add(villageNameItem5);
          }
        }
      }
      return namesListAndCapitals;
    }

    public void sortUserVillages()
    {
      if (this.m_userVillages == null)
        return;
      foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
      {
        if (this.villageList[userVillage.villageID].Capital)
        {
          userVillage.capital = true;
          if (this.villageList[userVillage.villageID].regionCapital)
            userVillage.parishCapital = true;
          else if (this.villageList[userVillage.villageID].countyCapital)
            userVillage.countyCapital = true;
          else if (this.villageList[userVillage.villageID].provinceCapital)
            userVillage.provinceCapital = true;
          else if (this.villageList[userVillage.villageID].countryCapital)
            userVillage.countryCapital = true;
        }
      }
      this.m_userVillages.Sort((IComparer<WorldMap.UserVillageData>) this.villageNameComparer);
      int num = 0;
      foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
      {
        this.villageList[userVillage.villageID].userVillageID = num;
        ++num;
      }
    }

    public int numUserParishes()
    {
      int num = 0;
      if (this.m_userVillages != null)
      {
        foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        {
          if (userVillage.parishCapital)
            ++num;
        }
      }
      return num;
    }

    public int numUserCounties()
    {
      int num = 0;
      if (this.m_userVillages != null)
      {
        foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        {
          if (userVillage.countyCapital)
            ++num;
        }
      }
      return num;
    }

    public int numUserProvinces()
    {
      int num = 0;
      if (this.m_userVillages != null)
      {
        foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        {
          if (userVillage.provinceCapital)
            ++num;
        }
      }
      return num;
    }

    public int numUserCountries()
    {
      int num = 0;
      if (this.m_userVillages != null)
      {
        foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        {
          if (userVillage.countryCapital)
            ++num;
        }
      }
      return num;
    }

    public ResearchData GetResearchDataForCurrentVillage()
    {
      return this.isCapital(InterfaceMgr.Instance.getSelectedMenuVillage()) && GameEngine.Instance.Village != null ? GameEngine.Instance.Village.m_parishCapitalResearchData : this.UserResearchData;
    }

    public ResearchData GetResearchDataForVillage(int villageID)
    {
      if (this.isCapital(villageID))
      {
        VillageMap village = GameEngine.Instance.getVillage(villageID);
        if (village != null)
          return village.m_parishCapitalResearchData;
      }
      return this.UserResearchData;
    }

    public ResearchData UserResearchData => this.userResearchData;

    public void setResearchData(ResearchData data)
    {
      if (data != null)
        this.userResearchData = data;
      this.requestSent = false;
    }

    public void addResearchPoints(int amount)
    {
      if (this.userResearchData == null)
        return;
      this.userResearchData.research_points += amount;
    }

    public bool isResearchLagging()
    {
      if (this.userResearchData.researchingType >= 0 && VillageMap.getCurrentServerTime() > this.userResearchData.research_completionTime.AddSeconds(15.0))
      {
        DateTime now = DateTime.Now;
        if (this.m_lastResearchCompleteTimeMatch == this.userResearchData.research_completionTime)
          return true;
      }
      return false;
    }

    public void updateResearch(bool force)
    {
      if (force)
      {
        this.requestSent = true;
        RemoteServices.Instance.set_GetResearchData_UserCallBack(new RemoteServices.GetResearchData_UserCallBack(this.getResearchDataCallback));
        RemoteServices.Instance.GetResearchData();
      }
      else
      {
        if (this.userResearchData == null || this.userResearchData.researchingType < 0 || this.requestSent || !(VillageMap.getCurrentServerTime() > this.userResearchData.research_completionTime.AddSeconds(5.0)))
          return;
        DateTime now = DateTime.Now;
        if (this.m_lastResearchCompleteTimeMatch == this.userResearchData.research_completionTime)
        {
          int num = 40 * this.m_researchLagCount;
          if (num < 40)
            num = 40;
          else if (num > 300)
            num = 300;
          if ((now - this.m_lastResearchCompleteRequestTime).TotalSeconds < (double) num)
            return;
          ++this.m_researchLagCount;
        }
        else
          this.m_researchLagCount = 0;
        if (this.userResearchData.researchingType == 59 && this.getTutorialStage() == 5)
        {
          GameEngine.Instance.World.TutorialQuestCompleted(4);
          Thread.Sleep(200);
        }
        this.m_lastResearchCompleteRequestTime = now;
        this.m_lastResearchCompleteTimeMatch = this.userResearchData.research_completionTime;
        this.requestSent = true;
        RemoteServices.Instance.set_GetResearchData_UserCallBack(new RemoteServices.GetResearchData_UserCallBack(this.getResearchDataCallback));
        RemoteServices.Instance.GetResearchData();
      }
    }

    public void getResearchDataCallback(GetResearchData_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.setResearchData(returnData.researchData);
      VillageMap.setServerTime(returnData.currentTime);
      InterfaceMgr.Instance.researchDataChanged(returnData.researchData);
      GameEngine.Instance.World.setPoints(returnData.currentPoints);
      if (returnData.researchData == null || !(this.m_lastResearchCompleteTimeMatch != returnData.researchData.research_completionTime) && returnData.researchData.researchingType >= 0 || GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_VILLAGE)
        return;
      GameEngine.Instance.downloadCurrentVillage();
    }

    public void doResearch(int researchType)
    {
      this.doResearch(researchType, (WorldMap.ResearchChangedDelegate) null);
    }

    public void doResearch(int researchType, WorldMap.ResearchChangedDelegate uiDelegate)
    {
      this.uiResearchDelegate = uiDelegate;
      if (this.inDoResearch && (DateTime.Now - this.lastDoResearchClick).TotalSeconds < 120.0)
        return;
      this.inDoResearch = true;
      this.lastDoResearchClick = DateTime.Now;
      RemoteServices.Instance.set_DoResearch_UserCallBack(new RemoteServices.DoResearch_UserCallBack(this.doResearchCallback));
      RemoteServices.Instance.DoResearch(researchType);
    }

    public void CancelQueuedResearch(int researchType, int queuePos)
    {
      this.CancelQueuedResearch(researchType, queuePos, (WorldMap.ResearchChangedDelegate) null);
    }

    public void CancelQueuedResearch(
      int researchType,
      int queuePos,
      WorldMap.ResearchChangedDelegate uiDelegate)
    {
      this.uiResearchDelegate = uiDelegate;
      if (this.inDoResearch && (DateTime.Now - this.lastDoResearchClick).TotalSeconds < 120.0)
        return;
      this.inDoResearch = true;
      this.lastDoResearchClick = DateTime.Now;
      RemoteServices.Instance.set_DoResearch_UserCallBack(new RemoteServices.DoResearch_UserCallBack(this.doResearchCallback));
      RemoteServices.Instance.CancelQueuedResearch(researchType, queuePos);
    }

    public void doResearchCallback(DoResearch_ReturnType returnData)
    {
      this.inDoResearch = false;
      if (returnData.Success)
      {
        this.setResearchData(returnData.researchData);
        VillageMap.setServerTime(returnData.currentTime);
        InterfaceMgr.Instance.researchDataChanged(returnData.researchData);
      }
      else
      {
        switch (returnData.m_errorCode)
        {
          case ErrorCodes.ErrorCode.RESEARCH_CANNOT_DO_RESEARCH_ALREADY_RESEARCHING:
          case ErrorCodes.ErrorCode.RESEARCH_CANNOT_DO_RESEARCH_NOT_ENOUGH_POINTS:
          case ErrorCodes.ErrorCode.RESEARCH_CANNOT_DO_RESEARCH_NOT_AVAILABLE:
          case ErrorCodes.ErrorCode.RESEARCH_CANNOT_DO_RESEARCH_FULL:
            InterfaceMgr.Instance.researchDataChanged(returnData.researchData);
            break;
        }
      }
      if (this.uiResearchDelegate == null)
        return;
      this.uiResearchDelegate();
    }

    public void setOnResearchPointPurchaseDelegate(
      CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate del)
    {
      this.onResearchPointPurchaseDelegate = del;
    }

    public void buyResearchPoint()
    {
      if (this.inBuyPoint && (DateTime.Now - this.lastBuyPointClick).TotalSeconds < 120.0)
        return;
      this.inBuyPoint = true;
      this.lastBuyPointClick = DateTime.Now;
      RemoteServices.Instance.set_BuyResearchPoint_UserCallBack(new RemoteServices.BuyResearchPoint_UserCallBack(this.buyResearchPointCallback));
      RemoteServices.Instance.BuyResearchPoint();
    }

    public void buyResearchPointCallback(BuyResearchPoint_ReturnType returnData)
    {
      this.inBuyPoint = false;
      if (!returnData.Success && returnData.m_errorCode != ErrorCodes.ErrorCode.RESEARCH_NOT_ENOUGH_HONOUR)
        return;
      this.setResearchData(returnData.researchData);
      this.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
      InterfaceMgr.Instance.researchDataChanged(returnData.researchData);
      if (this.onResearchPointPurchaseDelegate == null)
        return;
      this.onResearchPointPurchaseDelegate();
    }

    public bool doesUserHaveVillageInParishByCapital(int capitalID)
    {
      return this.doesUserHaveVillageInParish(this.getParishFromVillageID(capitalID));
    }

    public bool doesUserHaveVillageInParish(int parishID)
    {
      if (this.m_userVillages != null)
      {
        foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        {
          if (this.getParishFromVillageID(userVillage.villageID) == parishID)
            return true;
        }
      }
      return false;
    }

    public void resetTutorialInfo()
    {
      this.m_tutorialInfo = new QuestsAndTutorialInfo();
      this.QuestObjectivesSent = new SparseArray();
      this.tutorialQuestsObjectivesComplete = new List<int>();
    }

    public void setTutorialInfo(QuestsAndTutorialInfo tutorialInfo)
    {
      if (tutorialInfo == null)
        return;
      int tutorialStage = this.m_tutorialInfo.tutorialStage;
      if (tutorialInfo.questsActive == null)
      {
        this.m_tutorialInfo.tutorialActive = tutorialInfo.tutorialActive;
        this.m_tutorialInfo.tutorialCompleted = tutorialInfo.tutorialCompleted;
        this.m_tutorialInfo.tutorialStage = tutorialInfo.tutorialStage;
        this.m_tutorialInfo.resumeStage = tutorialInfo.resumeStage;
      }
      else
        this.m_tutorialInfo = tutorialInfo;
      if (!this.m_tutorialInfo.tutorialActive || tutorialStage == this.m_tutorialInfo.tutorialStage)
        return;
      this.newTutorialAvailable = true;
    }

    public int getTutorialStage()
    {
      if (this.m_tutorialInfo == null)
        return 0;
      return !this.m_tutorialInfo.tutorialActive ? -1 : this.m_tutorialInfo.tutorialStage;
    }

    public bool isTutorialResumable()
    {
      return this.m_tutorialInfo != null && this.m_tutorialInfo.resumeStage >= 0;
    }

    public bool isTutorialActive()
    {
      switch (this.getTutorialStage())
      {
        case -3:
        case -1:
          return false;
        default:
          return true;
      }
    }

    public bool isNewTutorialAvailable() => this.newTutorialAvailable;

    public void tutorialPopupShown() => this.newTutorialAvailable = false;

    public void forceTutorialToBeShown() => this.newTutorialAvailable = true;

    public int[] getActiveQuests()
    {
      return this.m_tutorialInfo == null || this.m_tutorialInfo.questsActive == null ? new int[0] : this.m_tutorialInfo.questsActive;
    }

    public int[] getCompletedQuests()
    {
      return this.m_tutorialInfo == null || this.m_tutorialInfo.questsCompleted == null ? new int[0] : this.m_tutorialInfo.questsCompleted;
    }

    public bool isQuestComplete(int quest)
    {
      if (this.m_tutorialInfo == null || this.m_tutorialInfo.questsCompleted == null)
        return false;
      foreach (int num in this.m_tutorialInfo.questsCompleted)
      {
        if (num == quest)
          return true;
      }
      return false;
    }

    public bool isQuestObjectiveComplete(int quest)
    {
      return this.tutorialQuestsObjectivesComplete.Contains(quest);
    }

    public void advanceTutorialOLD()
    {
    }

    public void advanceTutorialTo(int targetStage)
    {
      UniversalDebugLog.Log("advancetutorialto " + (object) targetStage + "/" + (object) this.getTutorialStage());
      if (this.getTutorialStage() == -1)
      {
        this.targetTutorialStage = -1;
      }
      else
      {
        this.targetTutorialStage = targetStage;
        if (this.getTutorialStage() == this.targetTutorialStage)
          return;
        this.advanceTutorial();
      }
    }

    public void advanceTutorial()
    {
      UniversalDebugLog.Log("Advancing from " + (object) this.getTutorialStage());
      if (GameEngine.Instance.World.getTutorialStage() != 0)
        StatTrackingClient.Instance().ActivateTrigger(12, (object) GameEngine.Instance.World.getTutorialStage());
      this.inTutorialAdvance = true;
      RemoteServices.Instance.set_TutorialCommand_UserCallBack(new RemoteServices.TutorialCommand_UserCallBack(this.TutorialCommandCallback));
      RemoteServices.Instance.TutorialCommand(-2);
    }

    public void endTutorial()
    {
      StatTrackingClient.Instance().ActivateTrigger(13, (object) GameEngine.Instance.World.getTutorialStage());
      this.targetTutorialStage = -3;
      this.m_tutorialInfo.tutorialStage = -3;
      RemoteServices.Instance.set_TutorialCommand_UserCallBack(new RemoteServices.TutorialCommand_UserCallBack(this.TutorialCommandCallback));
      RemoteServices.Instance.TutorialCommand(-3);
    }

    public void resumeTutorial()
    {
      this.inTutorialAdvance = true;
      RemoteServices.Instance.set_TutorialCommand_UserCallBack(new RemoteServices.TutorialCommand_UserCallBack(this.TutorialCommandCallback));
      RemoteServices.Instance.TutorialCommand(-5);
    }

    public bool TutorialIsAdvancing() => this.inTutorialAdvance;

    public void restartTutorial()
    {
      RemoteServices.Instance.set_TutorialCommand_UserCallBack(new RemoteServices.TutorialCommand_UserCallBack(this.TutorialCommandCallback));
      RemoteServices.Instance.TutorialCommand(-4);
    }

    public void TutorialCommandCallback(TutorialCommand_ReturnType returnData)
    {
      this.inTutorialAdvance = false;
      if (!returnData.Success)
        return;
      this.setTutorialInfo(returnData.m_tutorialInfo);
      if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_QUESTS)
        InterfaceMgr.Instance.reloadQuestPanel();
      if (returnData.m_tutorialInfo == null || returnData.m_tutorialInfo.tutorialStage != -1)
        return;
      this.doSelectTutorialArmy = true;
      GameEngine.Instance.World.getArmiesIfNewAttacks();
    }

    public void handleQuestObjectiveHappening(int objective)
    {
      int fromObjectiveFlag = Quests.getQuestFromObjectiveFlag(objective);
      if (fromObjectiveFlag < 0 || this.m_tutorialInfo == null || this.m_tutorialInfo.questsActive == null || this.QuestObjectivesSent[objective] != null)
        return;
      foreach (int num in this.m_tutorialInfo.questsActive)
      {
        if (num == fromObjectiveFlag)
        {
          RemoteServices.Instance.set_FlagQuestObjectiveComplete_UserCallBack(new RemoteServices.FlagQuestObjectiveComplete_UserCallBack(this.FlagQuestObjectiveCompleteCallBack));
          RemoteServices.Instance.FlagQuestObjectiveComplete(objective);
          this.QuestObjectivesSent[objective] = (object) 1;
          break;
        }
      }
      if (objective != 10005 && objective != 10002)
        return;
      bool flag = false;
      foreach (int num in this.m_tutorialInfo.questsCompleted)
      {
        if (num == fromObjectiveFlag)
        {
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      RemoteServices.Instance.set_FlagQuestObjectiveComplete_UserCallBack(new RemoteServices.FlagQuestObjectiveComplete_UserCallBack(this.FlagQuestObjectiveCompleteCallBack));
      RemoteServices.Instance.FlagQuestObjectiveComplete(objective);
      this.QuestObjectivesSent[objective] = (object) 1;
    }

    private void FlagQuestObjectiveCompleteCallBack(FlagQuestObjectiveComplete_ReturnType returnData)
    {
      if (!returnData.Success || returnData.objectiveCompleted < 0)
        return;
      int fromObjectiveFlag = Quests.getQuestFromObjectiveFlag(returnData.objectiveCompleted);
      if (fromObjectiveFlag < 0)
        return;
      this.tutorialQuestsObjectivesComplete.Add(fromObjectiveFlag);
      this.TutorialQuestCompleted(fromObjectiveFlag);
    }

    public void TutorialQuestCompleted(int quest)
    {
      int questsTutorialStage = Tutorials.getQuestsTutorialStage(quest);
      if (questsTutorialStage < 0 || questsTutorialStage != this.getTutorialStage())
        return;
      this.forceTutorialToBeShown();
    }

    public void handleQuestObjectiveHappening_PlayedCard(int cardType)
    {
      int objective = -1;
      switch (cardType)
      {
        case 769:
        case 770:
        case 2950:
          objective = 10002;
          break;
        case 3201:
        case 3202:
        case 3203:
          objective = 10005;
          break;
      }
      if (objective < 0)
        return;
      this.handleQuestObjectiveHappening(objective);
      if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_QUESTS)
        return;
      InterfaceMgr.Instance.reloadQuestPanel();
    }

    public void checkQuestObjectiveComplete(int quest)
    {
      RemoteServices.Instance.set_CheckQuestObjectiveComplete_UserCallBack(new RemoteServices.CheckQuestObjectiveComplete_UserCallBack(this.CheckQuestObjectiveCompleteCallBack));
      RemoteServices.Instance.CheckQuestObjectiveComplete(quest);
    }

    private void CheckQuestObjectiveCompleteCallBack(
      CheckQuestObjectiveComplete_ReturnType returnData)
    {
      if (!returnData.Success || returnData.questCompleted < 0)
        return;
      this.tutorialQuestsObjectivesComplete.Add(returnData.questCompleted);
    }

    public void addCompletedQuestObjectives(int quest)
    {
      this.tutorialQuestsObjectivesComplete.Add(quest);
    }

    public int NumVacationsAvailable
    {
      get => this.numVacationsAvailable;
      set => this.numVacationsAvailable = value;
    }

    public bool VacationNot30Days
    {
      get => this.vacationNot30Days;
      set => this.vacationNot30Days = value;
    }

    public void getPersonData()
    {
      this.lastPersonTime = DateTime.Now.AddYears(-5);
      RemoteServices.Instance.set_GetUserPeople_UserCallBack(new RemoteServices.GetUserPeople_UserCallBack(this.getUserPeopleCallback));
      RemoteServices.Instance.GetUserPeople();
    }

    public void getUserPeopleCallback(GetUserPeople_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.loadingErrored = false;
        this.importOrphanedPeople(returnData.people, returnData.currentTime, -2);
      }
      else
        this.loadingErrored = true;
    }

    public void getActivePeople()
    {
      RemoteServices.Instance.set_GetActivePeople_UserCallBack(new RemoteServices.GetActivePeople_UserCallBack(this.getActivePeopleCallback));
      RemoteServices.Instance.GetActivePeople(this.lastPersonTime);
    }

    public void getActivePeopleCallback(GetActivePeople_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.loadingErrored = false;
        this.importOrphanedPeople(returnData.people, returnData.currentTime, -2);
        this.lastPersonTime = returnData.currentTime;
      }
      else
        this.loadingErrored = true;
    }

    public void importOrphanedPeople(
      List<PersonData> personData,
      DateTime curServerTime,
      int villageID)
    {
      this.clearPersonArray(villageID);
      if (personData != null)
      {
        AllArmiesPanel2.MonksUpdated = true;
        foreach (PersonData personData1 in personData)
          this.addPerson(personData1, curServerTime);
      }
      this.countChildren();
    }

    public void clearPersonArray(int villageID)
    {
      if (villageID == -2)
        return;
      if (villageID < 0)
      {
        this.personArray.Clear();
      }
      else
      {
        List<WorldMap.LocalPerson> localPersonList = new List<WorldMap.LocalPerson>();
        foreach (WorldMap.LocalPerson person in this.personArray)
        {
          if (person.person.homeVillageID == villageID)
            localPersonList.Add(person);
        }
        foreach (WorldMap.LocalPerson localPerson in localPersonList)
          this.personArray[localPerson.personID] = (object) null;
      }
    }

    public void addPerson(PersonData personData, DateTime curServerTime)
    {
      WorldMap.LocalPerson localPerson = new WorldMap.LocalPerson();
      localPerson.person = personData;
      localPerson.personID = personData.personID;
      if (personData.state > 0)
      {
        localPerson.createJourney(personData.startTime, curServerTime, personData.endTime);
        if (personData.targetVillageID < this.villageList.Length)
        {
          localPerson.targetDisplayX = (double) this.villageList[personData.targetVillageID].x;
          localPerson.targetDisplayY = (double) this.villageList[personData.targetVillageID].y;
        }
        localPerson.seaTravel = this.isIslandTravel(personData.homeVillageID, personData.targetVillageID);
        bool flag = true;
        if (GameEngine.Instance.LocalWorldData.AIWorld)
        {
          switch (this.getSpecial(personData.homeVillageID))
          {
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
              flag = false;
              break;
          }
        }
        if (flag)
        {
          foreach (WorldMap.LocalPerson person in this.personArray)
          {
            person.childrenCount = 0;
            if (person.parentPerson == -1L && person.personID != localPerson.personID && person.person.state == personData.state && person.person.targetVillageID == personData.targetVillageID && person.person.homeVillageID == personData.homeVillageID)
            {
              TimeSpan timeSpan = person.person.endTime - personData.endTime;
              if (timeSpan.TotalSeconds < 1.0 && timeSpan.TotalSeconds > -1.0)
              {
                localPerson.parentPerson = person.person.personID;
                break;
              }
            }
          }
        }
      }
      if (personData.homeVillageID < this.villageList.Length)
      {
        localPerson.baseDisplayX = (double) this.villageList[personData.homeVillageID].x;
        localPerson.baseDisplayY = (double) this.villageList[personData.homeVillageID].y;
      }
      double realTime = DXTimer.GetCurrentMilliseconds() / 1000.0;
      localPerson.updatePosition(realTime);
      this.personArray[localPerson.personID] = (object) localPerson;
    }

    public void countChildren()
    {
      foreach (WorldMap.LocalPerson person in this.personArray)
      {
        if (person.parentPerson != -1L && person.personID != person.parentPerson && this.personArray.ContainsKey(person.parentPerson))
          ++((WorldMap.LocalPerson) this.personArray[person.parentPerson]).childrenCount;
      }
    }

    public void updatePeople()
    {
      try
      {
        List<long> longList = new List<long>();
        int num = 0;
        bool flag = true;
        if (!flag)
        {
          this.p_startAt = this.p_endAt;
          if (this.p_startAt >= this.personArray.Count)
          {
            this.p_startAt = 0;
            this.p_endAt = 0;
          }
          this.p_endAt += this.p_perFrame;
          if (this.p_endAt > this.personArray.Count)
            this.p_endAt = this.personArray.Count;
        }
        double realTime = DXTimer.GetCurrentMilliseconds() / 1000.0;
        foreach (WorldMap.LocalPerson person in this.personArray)
        {
          if (!flag)
          {
            if (num < this.p_startAt)
            {
              ++num;
              continue;
            }
            if (num < this.p_endAt)
              ++num;
            else
              break;
          }
          person.updatePosition(realTime);
          if (person.dying)
            longList.Add(person.personID);
        }
        foreach (long index in longList)
          this.personArray.RemoveAt(index);
      }
      catch (Exception ex)
      {
        UniversalDebugLog.Log("exception updating armies " + ex.ToString());
      }
    }

    public void drawPeople(RectangleF screenRect)
    {
      float num1 = (float) (this.m_worldScale / 28.0 / 0.60000002384185791);
      if ((double) num1 < 0.10000000149011612)
        num1 = 0.1f;
      if ((double) num1 > 1.0)
        num1 = 1f;
      SparseArray sparseArray1 = new SparseArray();
      SparseArray sparseArray2 = new SparseArray();
      WorldMapFilter worldMapFilter = GameEngine.Instance.World.worldMapFilter;
      bool aiWorld = GameEngine.Instance.LocalWorldData.AIWorld;
      foreach (WorldMap.LocalPerson person in this.personArray)
      {
        if (person.person.state > 0 && person.isVisible(screenRect) && worldMapFilter.showPeople(person))
        {
          if ((person.person.state == 1 || person.person.state == 11 || person.person.state == 21 || person.person.state == 31 || person.person.state == 50 || person.person.state == 75) && person.parentPerson == -1L)
          {
            int num2 = 0;
            int num3 = 2;
            if (!this.isUserVillage(person.person.homeVillageID))
            {
              num2 = 1;
              num3 += num2;
              if (aiWorld)
              {
                switch (this.getSpecial(person.person.homeVillageID))
                {
                  case 7:
                  case 8:
                    num2 = 403;
                    num3 = 404;
                    break;
                  case 9:
                  case 10:
                    num2 = 404;
                    num3 = 408;
                    break;
                  case 11:
                  case 12:
                    num2 = 405;
                    num3 = 412;
                    break;
                  case 13:
                  case 14:
                    num2 = 406;
                    num3 = 416;
                    break;
                }
              }
            }
            if (person.person.personType == 100)
              num3 = 142;
            if (this.DrawingArmyArrows)
            {
              this.villageSprite.PosX = ((float) person.displayX - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
              this.villageSprite.PosY = ((float) person.displayY - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
              this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
              this.villageSprite.SpriteNo = num3;
              this.villageSprite.Center = new PointF(44f, 44f);
              this.villageSprite.RotationAngle = SpriteWrapper.getFacing(person.BasePoint(), person.TargetPoint());
              this.villageSprite.Scale = num1;
              this.villageSprite.Update();
              this.villageSprite.DrawAndClear();
            }
            int num4 = num3;
            switch (person.person.personType)
            {
              case 4:
                num3 = 18 + num2;
                break;
              case 100:
                num3 = 173;
                break;
            }
            this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
            this.villageSprite.SpriteNo = num3;
            this.villageSprite.Center = new PointF(44f, 44f);
            this.villageSprite.Scale = num1;
            this.villageSprite.Update();
            this.villageSprite.DrawAndClear();
            if (person.seaTravel)
            {
              this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
              switch (num4)
              {
                case 0:
                  this.villageSprite.SpriteNo = 425;
                  break;
                case 1:
                  this.villageSprite.SpriteNo = 426;
                  break;
                case 2:
                  this.villageSprite.SpriteNo = 427;
                  break;
                case 3:
                  this.villageSprite.SpriteNo = 428;
                  break;
                case 404:
                  this.villageSprite.SpriteNo = 429;
                  break;
                case 408:
                  this.villageSprite.SpriteNo = 430;
                  break;
                case 412:
                  this.villageSprite.SpriteNo = 431;
                  break;
                case 416:
                  this.villageSprite.SpriteNo = 432;
                  break;
              }
              this.villageSprite.Center = new PointF(44f, 44f);
              this.villageSprite.ColorToUse = Color.FromArgb(this.alphaValue, ARGBColors.White);
              this.villageSprite.Scale = num1;
              this.villageSprite.Update();
              this.villageSprite.DrawAndClear();
            }
          }
          else if (person.person.state == 2)
          {
            int targetVillageId = person.person.targetVillageID;
            WorldMap.CapitalPeopleGFX capitalPeopleGfx;
            if (sparseArray1[targetVillageId] == null)
            {
              capitalPeopleGfx = new WorldMap.CapitalPeopleGFX();
              capitalPeopleGfx.posX = ((float) person.displayX - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
              capitalPeopleGfx.posY = ((float) person.displayY - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
              sparseArray1[targetVillageId] = (object) capitalPeopleGfx;
            }
            else
              capitalPeopleGfx = (WorldMap.CapitalPeopleGFX) sparseArray1[targetVillageId];
            if (!this.isUserVillage(person.person.homeVillageID))
              ++capitalPeopleGfx.numOthers;
            else
              ++capitalPeopleGfx.numYours;
          }
          else if (person.person.state == 12 || person.person.state == 22)
          {
            int targetVillageId = person.person.targetVillageID;
            WorldMap.CapitalPeopleGFX capitalPeopleGfx;
            if (sparseArray2[targetVillageId] == null)
            {
              capitalPeopleGfx = new WorldMap.CapitalPeopleGFX();
              capitalPeopleGfx.posX = ((float) person.displayX - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
              capitalPeopleGfx.posY = ((float) person.displayY - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
              sparseArray2[targetVillageId] = (object) capitalPeopleGfx;
            }
            else
              capitalPeopleGfx = (WorldMap.CapitalPeopleGFX) sparseArray2[targetVillageId];
            if (person.person.state == 12)
              ++capitalPeopleGfx.numYours;
            else
              ++capitalPeopleGfx.numOthers;
          }
        }
      }
    }

    private long findNearestPersonFromScreenPos(Point mousePos, ref double bestDist)
    {
      if (InterfaceMgr.Instance.WorldMapMode != 0)
        return -1;
      double mapX = ((double) mousePos.X - (double) this.m_screenWidth / 2.0) / this.m_worldScale + this.m_screenCentreX;
      double mapY = ((double) mousePos.Y - (double) this.m_screenHeight / 2.0) / this.m_worldScale + this.m_screenCentreY;
      return mapX >= 0.0 && mapX < (double) this.worldMapWidth && mapY >= 0.0 && mapY < (double) this.worldMapHeight ? this.findNearestPersonFromMapPos(mapX, mapY, ref bestDist) : -1L;
    }

    private long findNearestPersonFromMapPos(double mapX, double mapY, ref double bestDist)
    {
      WorldMapFilter worldMapFilter = GameEngine.Instance.World.worldMapFilter;
      long personFromMapPos = -1;
      double num1 = 2.25;
      foreach (WorldMap.LocalPerson person in this.personArray)
      {
        if (person.person.state > 0 && person.person.state != 2 && person.person.state != 12 && person.person.state != 22 && person.parentPerson == -1L && worldMapFilter.showPeople(person))
        {
          double num2 = (person.displayX - mapX) * (person.displayX - mapX) + (person.displayY - mapY) * (person.displayY - mapY);
          if (num2 < num1)
          {
            num1 = num2;
            personFromMapPos = person.personID;
          }
        }
      }
      bestDist = num1;
      return personFromMapPos;
    }

    public WorldMap.LocalPerson getPerson(long personID)
    {
      return (WorldMap.LocalPerson) this.personArray[personID];
    }

    public SparseArray getPeopleArray() => this.personArray;

    public int countVillagePeople(int villageID, int personType, ref int athome)
    {
      athome = 0;
      int num = 0;
      foreach (WorldMap.LocalPerson person in this.personArray)
      {
        if (person.person.homeVillageID == villageID && person.person.personType == personType)
        {
          ++num;
          if (person.person.state == 0)
            ++athome;
        }
      }
      return num;
    }

    public bool isSpyCommandDataActive(int villageID, int commandBits)
    {
      foreach (WorldMap.LocalPerson person in this.personArray)
      {
        if (person.person.targetVillageID == villageID && person.person.personType == 1 && person.person.state == 12 && (person.person.spyCommandsDone & commandBits) != 0)
          return true;
      }
      return false;
    }

    public void importCounterSpyInfo(List<CounterSpyInfo> info) => this.counterSpyInfo = info;

    public List<CounterSpyInfo> getCounterSpyInfo(int villageID)
    {
      List<CounterSpyInfo> counterSpyInfo1 = new List<CounterSpyInfo>();
      if (this.counterSpyInfo != null)
      {
        foreach (CounterSpyInfo counterSpyInfo2 in this.counterSpyInfo)
        {
          if (counterSpyInfo2.targetVillageID == villageID)
            counterSpyInfo1.Add(counterSpyInfo2);
        }
      }
      return counterSpyInfo1;
    }

    public FreeCardsData FreeCardInfo => this.freeCardInfo;

    public void importFreeCardData(
      int currentLevel,
      bool[] stages,
      DateTime nextCardTime,
      DateTime serverTime)
    {
      this.freeCardInfo = FreeCardsData.createFreeCardData(currentLevel, stages, nextCardTime, serverTime);
    }

    public bool InviteSystemNotImplemented
    {
      get => this.inviteSystemNotImplemented;
      set => this.inviteSystemNotImplemented = value;
    }

    public bool MapEditing
    {
      get => this.mapEditing;
      set => this.mapEditing = value;
    }

    public string MapDataFilename => this.mapDataFilename;

    public bool WorldEnded
    {
      get => this.worldEnded;
      set
      {
        this.worldEnded = value;
        if (this.worldEnded)
          return;
        this.worldEnded_message = false;
      }
    }

    public bool WorldEnded_message
    {
      get => this.worldEnded_message;
      set => this.worldEnded_message = value;
    }

    public int WorldMapType => this.currentMapType;

    public string WorldDefaultLanguage
    {
      get
      {
        CommonTypes.WorldMapType mapData = GameEngine.Instance.WorldMapTypesData.getMapData(this.currentMapType);
        if (mapData.mapName.ToLower() == "uk.wmpData".ToLower())
          return "en";
        if (mapData.mapName.ToLower() == "de.wmpData".ToLower())
          return "de";
        if (mapData.mapName.ToLower() == "fr.wmpData".ToLower())
          return "fr";
        if (mapData.mapName.ToLower() == "ru.wmpData".ToLower())
          return "ru";
        if (mapData.mapName.ToLower() == "es.wmpData".ToLower())
          return "es";
        if (mapData.mapName.ToLower() == "pl.wmpData".ToLower())
          return "pl";
        if (mapData.mapName.ToLower() == "sa.wmpData".ToLower())
          return "pt";
        if (mapData.mapName.ToLower() == "it.wmpData".ToLower())
          return "it";
        if (mapData.mapName.ToLower() == "tr.wmpData".ToLower())
          return "tr";
        return mapData.mapName.ToLower() == "ch.wmpData".ToLower() ? "zh" : "en";
      }
    }

    public void initWorldMap(int mapType)
    {
      this.storedRegionFactionsPos = -1L;
      this.storedCountyFactionsPos = -1L;
      this.storedProvinceFactionsPos = -1L;
      this.storedVillageFactionsPos = -1L;
      this.storedCountryFactionsPos = -1L;
      this.storedFactionChangesPos = -1L;
      this.storedParishFlagsPos = -1L;
      this.storedCountyFlagsPos = -1L;
      this.storedProvinceFlagsPos = -1L;
      this.storedCountryFlagsPos = -1L;
      this.currentMapType = mapType;
      CommonTypes.WorldMapType mapData = GameEngine.Instance.WorldMapTypesData.getMapData(mapType);
      if (mapData == null)
        throw new Exception("Map Data was null");
      if (mapData.mapName == null)
        throw new Exception("Map Data Name was null");
      this.loadData(mapData.mapName);
      this.initMapTiles(mapData.tmapName, mapData.tileMapWidth, mapData.tileMapHeight);
      if (mapData.mapName.ToLower() == "uk.wmpData".ToLower())
      {
        WorldMap.WorldPointList region1 = this.regionList[2399];
        List<WorldMap.Triangle> triangleList1 = new List<WorldMap.Triangle>();
        triangleList1.AddRange((IEnumerable<WorldMap.Triangle>) region1.triangleList);
        triangleList1.Add(this.makeTriangle(region1.regionBorderList[9], region1.regionBorderList[11], region1.regionBorderList[10]));
        triangleList1.Add(this.makeTriangle(region1.regionBorderList[11], region1.regionBorderList[9], region1.regionBorderList[12]));
        region1.triangleList = triangleList1.ToArray();
        List<WorldMap.WorldPoint> worldPointList1 = new List<WorldMap.WorldPoint>();
        for (int index = 0; index < region1.regionBorderList.Length; ++index)
        {
          if (index < 9 || index > 12)
            worldPointList1.Add(region1.regionBorderList[index]);
        }
        region1.regionBorderList = worldPointList1.ToArray();
        WorldMap.WorldPointList region2 = this.regionList[3152];
        List<WorldMap.Triangle> triangleList2 = new List<WorldMap.Triangle>();
        triangleList2.AddRange((IEnumerable<WorldMap.Triangle>) region2.triangleList);
        triangleList2.Add(this.makeTriangle(region2.regionBorderList[10], region2.regionBorderList[12], region2.regionBorderList[11]));
        region2.triangleList = triangleList2.ToArray();
        List<WorldMap.WorldPoint> worldPointList2 = new List<WorldMap.WorldPoint>();
        for (int index = 0; index < region2.regionBorderList.Length; ++index)
        {
          if (index < 10 || index > 12)
            worldPointList2.Add(region2.regionBorderList[index]);
        }
        region2.regionBorderList = worldPointList2.ToArray();
        WorldMap.WorldPointList region3 = this.regionList[1880];
        List<WorldMap.Triangle> triangleList3 = new List<WorldMap.Triangle>();
        triangleList3.AddRange((IEnumerable<WorldMap.Triangle>) region3.triangleList);
        triangleList3.Add(this.makeTriangle(region3.regionBorderList[4], region3.regionBorderList[5], region3.regionBorderList[6]));
        region3.triangleList = triangleList3.ToArray();
        List<WorldMap.WorldPoint> worldPointList3 = new List<WorldMap.WorldPoint>();
        for (int index = 0; index < region3.regionBorderList.Length; ++index)
        {
          if (index < 4 || index > 6)
            worldPointList3.Add(region3.regionBorderList[index]);
        }
        region3.regionBorderList = worldPointList3.ToArray();
        WorldMap.WorldPointList region4 = this.regionList[907];
        List<WorldMap.Triangle> triangleList4 = new List<WorldMap.Triangle>();
        triangleList4.AddRange((IEnumerable<WorldMap.Triangle>) region4.triangleList);
        triangleList4.Add(this.makeTriangle(region4.regionBorderList[9], region4.regionBorderList[10], region4.regionBorderList[11]));
        region4.triangleList = triangleList4.ToArray();
        List<WorldMap.WorldPoint> worldPointList4 = new List<WorldMap.WorldPoint>();
        for (int index = 0; index < region4.regionBorderList.Length; ++index)
        {
          if (index != 10)
            worldPointList4.Add(region4.regionBorderList[index]);
        }
        region4.regionBorderList = worldPointList4.ToArray();
        WorldMap.WorldPointList region5 = this.regionList[35];
        List<WorldMap.Triangle> triangleList5 = new List<WorldMap.Triangle>();
        triangleList5.AddRange((IEnumerable<WorldMap.Triangle>) region5.triangleList);
        triangleList5.Add(this.makeTriangle(region5.regionBorderList[2], region5.regionBorderList[4], region5.regionBorderList[3]));
        region5.triangleList = triangleList5.ToArray();
        List<WorldMap.WorldPoint> worldPointList5 = new List<WorldMap.WorldPoint>();
        for (int index = 0; index < region5.regionBorderList.Length; ++index)
        {
          if (index != 3)
            worldPointList5.Add(region5.regionBorderList[index]);
        }
        region5.regionBorderList = worldPointList5.ToArray();
      }
      if (mapData.mapName.ToLower() == "uk2.wmpData".ToLower() || mapData.mapName.ToLower() == "ukai.wmpData".ToLower())
      {
        WorldMap.WorldPointList region = this.regionList[1162];
        region.regionBorderList[2].x = 1826f;
        region.regionBorderList[2].y = 2747f;
        region.triangleList[4].x2 = 1826f;
        region.triangleList[4].y2 = 2747f;
      }
      if (mapData.mapName.ToLower() == "de.wmpData".ToLower())
      {
        WorldMap.WorldPointList region6 = this.regionList[4058];
        List<WorldMap.Triangle> triangleList6 = new List<WorldMap.Triangle>();
        triangleList6.AddRange((IEnumerable<WorldMap.Triangle>) region6.triangleList);
        triangleList6.Add(this.makeTriangle(region6.regionBorderList[4], region6.regionBorderList[6], region6.regionBorderList[5]));
        region6.triangleList = triangleList6.ToArray();
        List<WorldMap.WorldPoint> worldPointList6 = new List<WorldMap.WorldPoint>();
        for (int index = 0; index < region6.regionBorderList.Length; ++index)
        {
          if (index < 4 || index > 6)
            worldPointList6.Add(region6.regionBorderList[index]);
        }
        region6.regionBorderList = worldPointList6.ToArray();
        WorldMap.WorldPointList region7 = this.regionList[551];
        List<WorldMap.Triangle> triangleList7 = new List<WorldMap.Triangle>();
        for (int index = 0; index < region7.triangleList.Length; ++index)
        {
          if (((double) region7.triangleList[index].x1 != (double) region7.regionBorderList[5].x || (double) region7.triangleList[index].y1 != (double) region7.regionBorderList[5].y) && ((double) region7.triangleList[index].x2 != (double) region7.regionBorderList[5].x || (double) region7.triangleList[index].y2 != (double) region7.regionBorderList[5].y) && ((double) region7.triangleList[index].x3 != (double) region7.regionBorderList[5].x || (double) region7.triangleList[index].y3 != (double) region7.regionBorderList[5].y))
            triangleList7.Add(region7.triangleList[index]);
        }
        region7.triangleList = triangleList7.ToArray();
        List<WorldMap.WorldPoint> worldPointList7 = new List<WorldMap.WorldPoint>();
        for (int index = 0; index < region7.regionBorderList.Length; ++index)
        {
          if (index < 3 || index > 5)
            worldPointList7.Add(region7.regionBorderList[index]);
        }
        region7.regionBorderList = worldPointList7.ToArray();
        WorldMap.WorldPointList region8 = this.regionList[2890];
        List<WorldMap.Triangle> triangleList8 = new List<WorldMap.Triangle>();
        for (int index = 0; index < region8.triangleList.Length; ++index)
        {
          if (((double) region8.triangleList[index].x1 != (double) region8.regionBorderList[7].x || (double) region8.triangleList[index].y1 != (double) region8.regionBorderList[7].y) && ((double) region8.triangleList[index].x2 != (double) region8.regionBorderList[7].x || (double) region8.triangleList[index].y2 != (double) region8.regionBorderList[7].y) && ((double) region8.triangleList[index].x3 != (double) region8.regionBorderList[7].x || (double) region8.triangleList[index].y3 != (double) region8.regionBorderList[7].y))
            triangleList8.Add(region8.triangleList[index]);
        }
        triangleList8.Add(this.makeTriangle(region8.regionBorderList[6], region8.regionBorderList[13], region8.regionBorderList[10]));
        region8.triangleList = triangleList8.ToArray();
        List<WorldMap.WorldPoint> worldPointList8 = new List<WorldMap.WorldPoint>();
        for (int index = 0; index < region8.regionBorderList.Length; ++index)
        {
          if (index < 6 || index > 8)
            worldPointList8.Add(region8.regionBorderList[index]);
        }
        region8.regionBorderList = worldPointList8.ToArray();
        WorldMap.WorldPointList region9 = this.regionList[3725];
        List<WorldMap.Triangle> triangleList9 = new List<WorldMap.Triangle>();
        for (int index = 0; index < region9.triangleList.Length; ++index)
        {
          if (((double) region9.triangleList[index].x1 != (double) region9.regionBorderList[4].x || (double) region9.triangleList[index].y1 != (double) region9.regionBorderList[4].y) && ((double) region9.triangleList[index].x2 != (double) region9.regionBorderList[4].x || (double) region9.triangleList[index].y2 != (double) region9.regionBorderList[4].y) && ((double) region9.triangleList[index].x3 != (double) region9.regionBorderList[4].x || (double) region9.triangleList[index].y3 != (double) region9.regionBorderList[4].y))
            triangleList9.Add(region9.triangleList[index]);
        }
        triangleList9.Add(this.makeTriangle(region9.regionBorderList[3], region9.regionBorderList[4], region9.regionBorderList[8]));
        triangleList9.Add(this.makeTriangle(region9.regionBorderList[8], region9.regionBorderList[4], region9.regionBorderList[9]));
        triangleList9.Add(this.makeTriangle(region9.regionBorderList[4], region9.regionBorderList[6], region9.regionBorderList[9]));
        region9.triangleList = triangleList9.ToArray();
        List<WorldMap.WorldPoint> worldPointList9 = new List<WorldMap.WorldPoint>();
        for (int index = 0; index < region9.regionBorderList.Length; ++index)
        {
          if (index < 3 || index > 9)
            worldPointList9.Add(region9.regionBorderList[index]);
          else if (index == 3)
          {
            worldPointList9.Add(region9.regionBorderList[index]);
            worldPointList9.Add(region9.regionBorderList[4]);
            worldPointList9.Add(region9.regionBorderList[6]);
            worldPointList9.Add(region9.regionBorderList[9]);
          }
        }
        region9.regionBorderList = worldPointList9.ToArray();
        WorldMap.WorldPointList region10 = this.regionList[624];
        List<WorldMap.Triangle> triangleList10 = new List<WorldMap.Triangle>();
        for (int index = 0; index < region10.triangleList.Length; ++index)
        {
          if (((double) region10.triangleList[index].x1 != (double) region10.regionBorderList[6].x || (double) region10.triangleList[index].y1 != (double) region10.regionBorderList[6].y) && ((double) region10.triangleList[index].x2 != (double) region10.regionBorderList[6].x || (double) region10.triangleList[index].y2 != (double) region10.regionBorderList[6].y) && ((double) region10.triangleList[index].x3 != (double) region10.regionBorderList[6].x || (double) region10.triangleList[index].y3 != (double) region10.regionBorderList[6].y))
            triangleList10.Add(region10.triangleList[index]);
        }
        region10.triangleList = triangleList10.ToArray();
        List<WorldMap.WorldPoint> worldPointList10 = new List<WorldMap.WorldPoint>();
        for (int index = 0; index < region10.regionBorderList.Length; ++index)
        {
          if (index < 4 || index > 6)
            worldPointList10.Add(region10.regionBorderList[index]);
        }
        region10.regionBorderList = worldPointList10.ToArray();
        WorldMap.WorldPointList region11 = this.regionList[274];
        List<WorldMap.Triangle> triangleList11 = new List<WorldMap.Triangle>();
        triangleList11.AddRange((IEnumerable<WorldMap.Triangle>) region11.triangleList);
        triangleList11.Add(this.makeTriangle(region11.regionBorderList[7], region11.regionBorderList[9], region11.regionBorderList[8]));
        region11.triangleList = triangleList11.ToArray();
        List<WorldMap.WorldPoint> worldPointList11 = new List<WorldMap.WorldPoint>();
        for (int index = 0; index < region11.regionBorderList.Length; ++index)
        {
          if (index < 7 || index > 9)
            worldPointList11.Add(region11.regionBorderList[index]);
        }
        region11.regionBorderList = worldPointList11.ToArray();
        WorldMap.WorldPointList region12 = this.regionList[1704];
        List<WorldMap.Triangle> triangleList12 = new List<WorldMap.Triangle>();
        for (int index = 0; index < region12.triangleList.Length; ++index)
        {
          if (((double) region12.triangleList[index].x1 != (double) region12.regionBorderList[5].x || (double) region12.triangleList[index].y1 != (double) region12.regionBorderList[5].y) && ((double) region12.triangleList[index].x2 != (double) region12.regionBorderList[5].x || (double) region12.triangleList[index].y2 != (double) region12.regionBorderList[5].y) && ((double) region12.triangleList[index].x3 != (double) region12.regionBorderList[5].x || (double) region12.triangleList[index].y3 != (double) region12.regionBorderList[5].y))
            triangleList12.Add(region12.triangleList[index]);
        }
        List<WorldMap.WorldPoint> worldPointList12 = new List<WorldMap.WorldPoint>();
        for (int index = 0; index < region12.regionBorderList.Length; ++index)
        {
          if (index < 5 || index > 7)
            worldPointList12.Add(region12.regionBorderList[index]);
        }
        region12.regionBorderList = worldPointList12.ToArray();
        triangleList12.Add(this.makeTriangle(region12.regionBorderList[4], region12.regionBorderList[5], region12.regionBorderList[8]));
        triangleList12.Add(this.makeTriangle(region12.regionBorderList[5], region12.regionBorderList[6], region12.regionBorderList[8]));
        region12.triangleList = triangleList12.ToArray();
        WorldMap.WorldPointList region13 = this.regionList[737];
        List<WorldMap.Triangle> triangleList13 = new List<WorldMap.Triangle>();
        for (int index = 0; index < region13.triangleList.Length; ++index)
        {
          if (((double) region13.triangleList[index].x1 != (double) region13.regionBorderList[9].x || (double) region13.triangleList[index].y1 != (double) region13.regionBorderList[9].y) && ((double) region13.triangleList[index].x2 != (double) region13.regionBorderList[9].x || (double) region13.triangleList[index].y2 != (double) region13.regionBorderList[9].y) && ((double) region13.triangleList[index].x3 != (double) region13.regionBorderList[9].x || (double) region13.triangleList[index].y3 != (double) region13.regionBorderList[9].y))
            triangleList13.Add(region13.triangleList[index]);
        }
        triangleList13.Add(this.makeTriangle(region13.regionBorderList[10], region13.regionBorderList[6], region13.regionBorderList[7]));
        triangleList13.Add(this.makeTriangle(region13.regionBorderList[10], region13.regionBorderList[7], region13.regionBorderList[8]));
        region13.triangleList = triangleList13.ToArray();
        List<WorldMap.WorldPoint> worldPointList13 = new List<WorldMap.WorldPoint>();
        for (int index = 0; index < region13.regionBorderList.Length; ++index)
        {
          if (index < 8 || index > 10)
            worldPointList13.Add(region13.regionBorderList[index]);
        }
        region13.regionBorderList = worldPointList13.ToArray();
        WorldMap.WorldPointList region14 = this.regionList[2002];
        List<WorldMap.Triangle> triangleList14 = new List<WorldMap.Triangle>();
        for (int index = 0; index < region14.triangleList.Length; ++index)
        {
          if (((double) region14.triangleList[index].x1 != (double) region14.regionBorderList[7].x || (double) region14.triangleList[index].y1 != (double) region14.regionBorderList[7].y) && ((double) region14.triangleList[index].x2 != (double) region14.regionBorderList[7].x || (double) region14.triangleList[index].y2 != (double) region14.regionBorderList[7].y) && ((double) region14.triangleList[index].x3 != (double) region14.regionBorderList[7].x || (double) region14.triangleList[index].y3 != (double) region14.regionBorderList[7].y))
            triangleList14.Add(region14.triangleList[index]);
        }
        region14.triangleList = triangleList14.ToArray();
        List<WorldMap.WorldPoint> worldPointList14 = new List<WorldMap.WorldPoint>();
        for (int index = 0; index < region14.regionBorderList.Length; ++index)
        {
          if (index < 6 || index > 8)
            worldPointList14.Add(region14.regionBorderList[index]);
        }
        region14.regionBorderList = worldPointList14.ToArray();
      }
      if (mapData.mapName.ToLower() == "fr.wmpData".ToLower())
      {
        WorldMap.WorldPointList region = this.regionList[1286];
        List<WorldMap.WorldPoint> worldPointList = new List<WorldMap.WorldPoint>();
        for (int index = 0; index < region.regionBorderList.Length; ++index)
        {
          if (index == 2)
            worldPointList.Add(this.pointList[this.pointList.Length - 1]);
          worldPointList.Add(region.regionBorderList[index]);
        }
        region.regionBorderList = worldPointList.ToArray();
        List<WorldMap.Triangle> triangleList = new List<WorldMap.Triangle>();
        for (int index = 0; index < region.triangleList.Length; ++index)
          triangleList.Add(region.triangleList[index]);
        triangleList.Add(this.makeTriangle(region.regionBorderList[1], region.regionBorderList[2], region.regionBorderList[3]));
        region.triangleList = triangleList.ToArray();
      }
      int num1 = mapData.mapName.ToLower() == "es.wmpData".ToLower() ? 1 : 0;
      if (mapData.mapName.ToLower() == "uk.wmpData".ToLower() || mapData.mapName.ToLower() == "ukai.wmpData".ToLower() || mapData.mapName.ToLower() == "uk2.wmpData".ToLower())
      {
        this.villageList[92752].whiteName = true;
        this.villageList[104470].whiteName = true;
        this.villageList[35971].whiteName = true;
        this.villageList[40877].whiteName = true;
        this.villageList[79880].whiteName = true;
        this.villageList[15606].whiteName = true;
        this.villageList[47150].whiteName = true;
        this.villageList[38968].whiteName = true;
        this.villageList[887].whiteName = true;
        this.villageList[63242].whiteName = true;
        this.villageList[48860].whiteName = true;
        this.villageList[9814].whiteName = true;
        this.villageList[63346].whiteName = true;
        this.villageList[7048].whiteName = true;
        this.villageList[26822].whiteName = true;
        this.villageList[92323].whiteName = true;
        this.villageList[20064].whiteName = true;
        this.villageList[101024].whiteName = true;
        this.villageList[4990].whiteName = true;
        this.villageList[57072].whiteName = true;
        this.villageList[68411].whiteName = true;
        this.villageList[97936].whiteName = true;
        this.villageList[96751].whiteName = true;
        this.villageList[47510].whiteName = true;
        this.villageList[100192].whiteName = true;
        this.villageList[67820].whiteName = true;
        this.villageList[40584].whiteName = true;
        this.villageList[7023].whiteName = true;
        this.villageList[20886].whiteName = true;
        this.villageList[47609].whiteName = true;
        this.villageList[51105].whiteName = true;
        this.villageList[17224].whiteName = true;
        this.villageList[40621].whiteName = true;
        this.villageList[78266].whiteName = true;
        this.villageList[99511].whiteName = true;
        this.villageList[86735].whiteName = true;
        this.villageList[96213].whiteName = true;
        this.villageList[5939].whiteName = true;
        this.villageList[33068].whiteName = true;
        this.villageList[96920].whiteName = true;
        this.villageList[72446].whiteName = true;
        this.villageList[65059].whiteName = true;
        this.villageList[80891].whiteName = true;
        this.villageList[37030].whiteName = true;
        this.villageList[44474].whiteName = true;
        this.villageList[92875].whiteName = true;
        this.villageList[1260].whiteName = true;
        this.villageList[98708].whiteName = true;
        this.villageList[84603].whiteName = true;
        this.villageList[13420].whiteName = true;
        this.villageList[102192].whiteName = true;
        this.villageList[61617].whiteName = true;
        this.villageList[6298].whiteName = true;
        this.villageList[1849].whiteName = true;
        this.villageList[1922].whiteName = true;
        this.villageList[46926].whiteName = true;
        this.villageList[8269].whiteName = true;
        this.villageList[47460].whiteName = true;
        this.villageList[69904].whiteName = true;
        this.villageList[73159].whiteName = true;
        this.villageList[90296].whiteName = true;
        this.villageList[2333].whiteName = true;
        this.villageList[75785].whiteName = true;
        this.villageList[61988].whiteName = true;
        this.villageList[95778].whiteName = true;
        this.villageList[4907].whiteName = true;
        this.villageList[80977].whiteName = true;
        this.villageList[63648].whiteName = true;
        this.villageList[59676].whiteName = true;
        this.villageList[97400].whiteName = true;
        this.villageList[73745].whiteName = true;
        this.villageList[6110].whiteName = true;
        this.villageList[89402].whiteName = true;
        this.villageList[66929].whiteName = true;
        this.villageList[71819].whiteName = true;
        this.villageList[4096].whiteName = true;
        this.villageList[77463].whiteName = true;
        this.villageList[48655].whiteName = true;
        this.villageList[70438].whiteName = true;
        this.villageList[67537].whiteName = true;
        this.villageList[81005].whiteName = true;
        this.villageList[74711].whiteName = true;
        this.villageList[78992].whiteName = true;
        this.villageList[73367].whiteName = true;
        this.villageList[16549].whiteName = true;
        this.villageList[88967].whiteName = true;
        this.villageList[11626].whiteName = true;
        this.villageList[90169].whiteName = true;
        this.villageList[6870].whiteName = true;
        this.villageList[3419].whiteName = true;
        this.villageList[16513].whiteName = true;
        this.villageList[82509].whiteName = true;
        this.villageList[28871].whiteName = true;
        this.villageList[26127].whiteName = true;
        this.villageList[34202].whiteName = true;
        this.villageList[73281].whiteName = true;
        this.villageList[35297].whiteName = true;
        this.villageList[97838].whiteName = true;
        this.villageList[83438].whiteName = true;
        this.villageList[5667].whiteName = true;
        this.villageList[84766].whiteName = true;
        this.villageList[8931].whiteName = true;
        this.villageList[104362].whiteName = true;
        this.villageList[40448].whiteName = true;
        this.villageList[56972].whiteName = true;
        this.villageList[55864].whiteName = true;
        this.villageList[70568].whiteName = true;
        this.villageList[28572].whiteName = true;
        this.villageList[92022].whiteName = true;
        this.villageList[70772].whiteName = true;
        this.villageList[72672].whiteName = true;
        this.villageList[5411].whiteName = true;
        this.villageList[1399].whiteName = true;
        this.villageList[72492].whiteName = true;
        this.villageList[15605].whiteName = true;
        this.villageList[104772].whiteName = true;
        this.villageList[67668].whiteName = true;
        this.villageList[50526].whiteName = true;
        this.villageList[72429].whiteName = true;
        this.villageList[46961].whiteName = true;
        this.villageList[50526].whiteName = true;
        this.villageList[72429].whiteName = true;
        this.villageList[27672].whiteName = true;
        this.villageList[21519].whiteName = true;
        this.villageList[22470].whiteName = true;
        this.villageList[10316].whiteName = true;
        this.villageList[64236].whiteName = true;
        this.villageList[27672].whiteName = true;
        this.villageList[42421].whiteName = true;
        this.villageList[55689].whiteName = true;
        this.villageList[35377].whiteName = true;
        this.villageList[1925].whiteName = true;
        this.villageList[81147].whiteName = true;
        this.villageList[97051].whiteName = true;
        this.villageList[61137].whiteName = true;
        this.villageList[8703].whiteName = true;
        this.villageList[28286].whiteName = true;
        this.villageList[38025].whiteName = true;
        this.villageList[6812].whiteName = true;
        this.villageList[14536].whiteName = true;
        this.villageList[76156].whiteName = true;
        this.villageList[73071].whiteName = true;
        this.villageList[23995].whiteName = true;
        this.villageList[43806].whiteName = true;
        this.villageList[93282].whiteName = true;
        this.villageList[98715].whiteName = true;
        this.villageList[18253].whiteName = true;
        this.villageList[85156].whiteName = true;
        this.villageList[1479].whiteName = true;
        this.villageList[46961].whiteName = true;
        this.villageList[43811].whiteName = true;
        this.villageList[86963].whiteName = true;
        this.villageList[92752].whiteFlags = true;
        this.villageList[104470].whiteFlags = true;
        this.villageList[35971].whiteFlags = true;
        this.villageList[40877].whiteFlags = true;
        this.villageList[79880].whiteFlags = true;
        this.villageList[15606].whiteFlags = true;
        this.villageList[47150].whiteFlags = true;
        this.villageList[38968].whiteFlags = true;
        this.villageList[887].whiteFlags = true;
        this.villageList[63242].whiteFlags = true;
        this.villageList[48860].whiteFlags = true;
        this.villageList[9814].whiteFlags = true;
        this.villageList[63346].whiteFlags = true;
        this.villageList[35630].whiteFlags = true;
        this.villageList[40448].whiteFlags = true;
        this.villageList[82058].whiteFlags = true;
        this.villageList[84107].whiteFlags = true;
        this.villageList[42482].whiteFlags = true;
        this.villageList[59108].whiteFlags = true;
        this.villageList[29859].whiteFlags = true;
        this.villageList[7023].whiteFlags = true;
        this.villageList[20886].whiteFlags = true;
        this.villageList[47609].whiteFlags = true;
        this.villageList[30203].whiteFlags = true;
        this.villageList[38132].whiteFlags = true;
        this.villageList[31740].whiteFlags = true;
        this.villageList[84652].whiteFlags = true;
        this.villageList[102224].whiteFlags = true;
        this.villageList[101258].whiteFlags = true;
        this.villageList[57133].whiteFlags = true;
        this.villageList[43001].whiteFlags = true;
        this.villageList[5939].whiteFlags = true;
        this.villageList[33068].whiteFlags = true;
        this.villageList[96920].whiteFlags = true;
        this.villageList[72446].whiteFlags = true;
        this.villageList[65059].whiteFlags = true;
        this.villageList[80891].whiteFlags = true;
        this.villageList[37030].whiteFlags = true;
        this.villageList[44474].whiteFlags = true;
        this.villageList[92875].whiteFlags = true;
        this.villageList[1260].whiteFlags = true;
        this.villageList[98708].whiteFlags = true;
        this.villageList[84603].whiteFlags = true;
        this.villageList[13420].whiteFlags = true;
        this.villageList[102192].whiteFlags = true;
        this.villageList[61617].whiteFlags = true;
        this.villageList[50767].whiteFlags = true;
        this.villageList[90761].whiteFlags = true;
        this.villageList[20978].whiteFlags = true;
        this.villageList[105064].whiteFlags = true;
        this.villageList[26816].whiteFlags = true;
        this.villageList[55705].whiteFlags = true;
        this.villageList[73094].whiteFlags = true;
        this.villageList[28559].whiteFlags = true;
        this.villageList[90770].whiteFlags = true;
        this.villageList[53288].whiteFlags = true;
        this.villageList[83101].whiteFlags = true;
        this.villageList[91326].whiteFlags = true;
        this.villageList[3767].whiteFlags = true;
        this.villageList[11626].whiteFlags = true;
        this.villageList[90169].whiteFlags = true;
        this.villageList[6870].whiteFlags = true;
        this.villageList[3419].whiteFlags = true;
        this.villageList[16513].whiteFlags = true;
        this.villageList[82509].whiteFlags = true;
        this.villageList[73071].whiteFlags = true;
        this.villageList[66848].whiteFlags = true;
        this.villageList[63476].whiteFlags = true;
        this.villageList[42267].whiteFlags = true;
        this.villageList[77643].whiteFlags = true;
        this.villageList[100192].whiteFlags = true;
        this.villageList[82907].whiteFlags = true;
        this.villageList[65895].whiteFlags = true;
        this.villageList[102469].whiteFlags = true;
        this.villageList[57072].whiteFlags = true;
        this.villageList[78406].whiteFlags = true;
        this.villageList[97936].whiteFlags = true;
        this.villageList[35377].whiteFlags = true;
        this.villageList[38025].whiteFlags = true;
        this.villageList[76156].whiteFlags = true;
        this.villageList[26815].whiteFlags = true;
        this.villageList[66929].whiteFlags = true;
        this.villageList[71819].whiteFlags = true;
        this.villageList[10586].whiteFlags = true;
        this.villageList[16433].whiteFlags = true;
        this.villageList[74985].whiteFlags = true;
        this.villageList[34202].whiteFlags = true;
        this.villageList[86448].whiteFlags = true;
        this.villageList[102588].whiteFlags = true;
        this.villageList[99511].whiteFlags = true;
        this.villageList[86735].whiteFlags = true;
        this.villageList[64236].whiteFlags = true;
        this.villageList[4325].whiteFlags = true;
      }
      int num2 = mapData.mapName.ToLower() == "de.wmpData".ToLower() ? 1 : 0;
      int num3 = mapData.mapName.ToLower() == "fr.wmpData".ToLower() ? 1 : 0;
      int num4 = mapData.mapName.ToLower() == "ru.wmpData".ToLower() ? 1 : 0;
      if (mapData.mapName.ToLower() == "uk2.wmpData".ToLower() || mapData.mapName.ToLower() == "ukai.wmpData".ToLower())
        this.provincesList[24].parentID = 3;
      if (mapData.mapName.ToLower() == "ph.wmpData".ToLower())
      {
        this.villageList[18492].x = (short) 2263;
        this.villageList[18492].y = (short) 1758;
        this.villageList[18492].regionID = this.villageList[63857].regionID;
        this.villageList[18492].countyID = this.villageList[63857].countyID;
      }
      this.buildVillageTree();
      this.experimentalStuff(mapData.mapName.ToLower());
    }

    private void fixupNames()
    {
      CommonTypes.WorldMapType mapData = GameEngine.Instance.WorldMapTypesData.getMapData(this.currentMapType);
      if (mapData.mapName.ToLower() == "de.wmpData".ToLower())
      {
        this.fixupParishName(2288, 63395, "Milovice");
        this.fixupParishName(1251, 35200, "Český Krumlov");
        this.fixupParishName(2165, 60225, "Lazec");
        this.fixupParishName(886, 25428, "Samtens");
        this.fixupParishName(4145, 111386, "Bergen auf Rügen");
        this.fixupParishName(2162, 60189, "Groß Kordshagen");
        this.fixupParishName(1855, 51571, "Gransee");
        this.fixupParishName(4027, 108332, "Oßnig");
        this.fixupParishName(762, 21819, "Tietzow");
        this.fixupParishName(706, 20155, "Wustermark");
        this.fixupParishName(3065, 84108, "Spremberg");
        this.fixupParishName(2255, 62603, "Lieske");
        this.fixupParishName(1040, 29069, "Welzow");
        this.fixupParishName(2636, 72969, "Großräschen");
        this.fixupParishName(792, 22556, "Spremberg");
        this.fixupParishName(3588, 98158, "Lugau");
        this.fixupParishName(35, 838, "Sayda");
        this.fixupParishName(3372, 92181, "Großenhain");
        this.fixupParishName(1364, 38191, "Starý Mateřov");
        this.fixupParishName(489, 13973, "Stavenhagen");
        this.fixupParishName(1117, 31347, "Lemberg");
        this.fixupParishName(3169, 86633, "Marienbaum");
        this.fixupParishName(2350, 65035, "Geeste");
        this.fixupParishName(1002, 28093, "Lastrup");
        this.fixupParishName(3394, 92600, "Markhausen");
        this.fixupParishName(1903, 53366, "Barßel");
        this.fixupParishName(134, 3889, "Leer");
        this.fixupParishName(700, 19945, "Dörverden");
        this.fixupParishName(3549, 97013, "Hoya");
        this.fixupParishName(81, 2493, "Strüth");
        this.fixupParishName(3310, 90139, "Nackenheim");
        this.fixupParishName(3003, 82636, "Hammersbach");
        this.fixupParishName(760, 21778, "Grenderich");
        this.fixupParishName(1164, 32535, "Blankenrath");
        this.fixupParishName(1064, 29645, "Beltheim");
        this.fixupParishName(372, 10174, "Stocksee");
        this.fixupParishName(1774, 49198, "Scharbeutz");
        this.fixupParishName(1803, 50090, "Potsdam");
        this.fixupParishName(3058, 83939, "Golßen");
        this.fixupParishName(1799, 49961, "Dahme-Spreewald");
        this.fixupParishName(1078, 30061, "Treuenbrietzen");
        this.fixupParishName(1824, 50711, "Hohenseefeld");
        this.fixupParishName(648, 18509, "Jessen");
        this.fixupParishName(624, 17930, "Sonnewalde");
        this.fixupParishName(3854, 104394, "Droyßig");
        this.fixupParishName(2251, 62489, "Rohr");
        this.fixupParishName(2051, 57386, "Martigny");
        this.fixupParishName(958, 26999, "Amberg-Sulzbach");
        this.fixupParishName(3900, 105547, "Amberg");
        this.fixupParishName(4183, 112373, "Ebersdorf");
        this.fixupParishName(1810, 50324, "Bad Bederkesa");
        this.fixupParishName(271, 7487, "Osnabrück");
        this.fixupParishName(4042, 108662, "Fürstenau");
        this.fixupParishName(4053, 108895, "Vörden");
        this.fixupParishName(1312, 36972, "Bad Holzhausen");
        this.fixupParishName(3201, 87470, "Spenge");
        this.fixupParishName(1784, 49433, "Löhne");
        this.fixupParishName(3990, 107532, "Steinegge");
        this.fixupParishName(757, 21657, "Porta Westfalica");
        this.fixupParishName(2229, 61985, "Auetal");
        this.fixupParishName(2200, 61308, "Merxhausen");
        this.fixupParishName(1880, 52315, "Schwarmstedt");
        this.fixupParishName(3066, 84109, "Bad Essen");
        this.fixupParishName(3160, 86407, "Hemke");
        this.fixupParishName(890, 25572, "Tábor");
        this.fixupParishName(4241, 113638, "Rok");
        this.fixupParishName(3093, 84820, "Bad Gandersheim");
        this.fixupParishName(253, 6915, "Dannenberg");
        this.fixupParishName(3353, 91736, "Lübbow");
        this.fixupParishName(808, 22963, "Spandau bei Berlin");
        this.countyList[6].areaName = "Braunschweig-Lüneburg";
        this.provincesList[9].areaName = "Böhmen";
        this.provincesList[10].areaName = "Mähren";
      }
      if (mapData.mapName.ToLower() == "fr.wmpData".ToLower())
      {
        this.fixupParishName(3633, 105754, "Aubépine");
        this.fixupParishName(952, 28077, "Uza");
        this.fixupParishName(1778, 53102, "Fontvieille");
        this.fixupParishName(1892, 56377, "Fourques");
        this.fixupParishName(3603, 104868, "Camargue");
        this.fixupParishName(994, 29154, "La Dynamite");
        this.fixupParishName(1007, 29559, "Pioch Badet");
        this.fixupParishName(1552, 46259, "Marignane");
        this.fixupParishName(1567, 46488, "Saint-Laurent d'Aigouze");
        this.fixupParishName(3158, 92105, "Desvres");
        this.fixupParishName(928, 27252, "Diksmuide");
        this.fixupParishName(2331, 68423, "Arendonk");
        this.fixupParishName(3707, 107765, "Dessel");
        this.fixupParishName(339, 10307, "Simmerath");
        this.fixupParishName(3888, 113266, "Hachiville");
        this.fixupParishName(281, 8155, "Germingen");
        this.fixupParishName(3491, 101446, "Montreux");
        this.fixupParishName(2357, 69163, "Mouthe");
        this.fixupParishName(3621, 105473, "Moissey");
        this.fixupParishName(1156, 34122, "La Giettaz");
        this.fixupParishName(3331, 96846, "Lanslevillard");
        this.fixupParishName(2871, 84197, "Le Lautaret");
        this.fixupParishName(2412, 70382, "Le Freney-d'Oisans");
        this.fixupParishName(211, 5780, "Mont Thabor");
        this.fixupParishName(3160, 92152, "Saint-Laurent-en-Beaumont");
        this.fixupParishName(17, 404, "Orcières");
        this.fixupParishName(2378, 69628, "La Martre");
        this.fixupParishName(3715, 108295, "Cuers");
        this.fixupParishName(2739, 80545, "Vauvenargues");
        this.fixupParishName(719, 21432, "Riez");
        this.fixupParishName(1003, 29435, "Le Liouquet");
        this.fixupParishName(3219, 93590, "Murat-sur-Vèbre");
        this.fixupParishName(248, 6896, "Le Marguès");
        this.fixupParishName(1865, 55509, "Sainte-Foi");
        this.fixupParishName(1259, 37483, "Saint Etienne de Baïgorry");
        this.fixupParishName(952, 28077, "Lévignac");
        this.fixupParishName(1934, 57380, "Contis Plage");
        this.fixupParishName(539, 16126, "Garein");
        this.fixupParishName(3169, 92380, "Sanguinet");
        this.fixupParishName(1924, 56991, "Lugos");
        this.fixupParishName(290, 8316, "Cazaux");
        this.fixupParishName(2970, 87091, "Le Pilat");
        this.fixupParishName(1436, 43119, "Lugos");
        this.fixupParishName(696, 20792, "Audenge");
        this.fixupParishName(790, 23367, "Saumos");
        this.fixupParishName(1181, 34850, "Lesparre-Médoc");
        this.fixupParishName(1455, 43735, "Trébivan");
        this.fixupParishName(3520, 102414, "Guiscriff");
        this.fixupParishName(2163, 63489, "Rosporden");
        this.fixupParishName(2255, 65775, "Plouasne");
        this.fixupParishName(575, 17435, "Villenauxe-la-Grande");
        this.fixupParishName(1515, 45219, "Nemours");
        this.fixupParishName(2754, 81141, "Charny");
        this.fixupParishName(2936, 85807, "Saint Gondon");
        this.fixupParishName(3012, 88362, "Ménestreau-en-Villette");
        this.fixupParishName(3653, 106284, "La Brosse");
        this.fixupParishName(1712, 51032, "Ménétréol-sur-Sauldre");
        this.fixupParishName(2084, 61426, "Saulieu");
        this.fixupParishName(2522, 73279, "Evron");
        this.fixupParishName(295, 8335, "Oudenaarde");
        this.fixupParishName(2964, 86839, "Bièvre");
        this.fixupParishName(193, 5277, "Eprave");
        this.fixupParishName(2171, 63715, "Etalle");
        this.fixupParishName(405, 11986, "Paulhenc");
        this.fixupParishName(1599, 47396, "Froidchapelle");
        this.fixupParishName(1017, 29861, "Momignies");
        this.fixupParishName(3566, 103690, "Sivry-Rance");
        this.fixupParishName(843, 24899, "Bièvre");
        this.fixupParishName(2048, 60392, "Jodoigne");
        this.fixupParishName(1258, 37476, "Fontenois-la-Ville");
        this.fixupParishName(614, 18330, "Ninove");
        this.fixupParishName(1535, 45754, "Landos");
        this.fixupParishName(3017, 88448, "Mallemort");
        this.fixupParishName(3897, 113669, "La Trimouille");
        this.fixupParishName(3778, 110225, "Saint-Germain-de-la-Coudre");
        this.fixupParishName(1141, 33761, "Hastière-par-dela");
        this.fixupParishName(105, 2957, "Aragnouet");
        this.fixupParishName(2143, 62832, "Thermes Maguoac");
        this.fixupParishName(1098, 32229, "Gavarnie");
        this.fixupParishName(71, 1735, "Castelnau-Rivière-Basse");
        this.fixupParishName(2679, 78363, "Barèges");
        this.fixupParishName(2010, 59464, "Cauterets");
        this.fixupParishName(332, 9563, "Launemezan");
        this.fixupParishName(1871, 55651, "Gèdre");
        this.fixupParishName(911, 26678, "Bagnères-de-Bigorre");
        this.fixupParishName(590, 17737, "Tournoy");
        this.fixupParishName(1413, 42443, "Arreau");
        this.fixupParishName(130, 3571, "Bordères-Louron");
        this.fixupParishName(1377, 41325, "Sost");
        this.fixupParishName(1198, 35405, "Sarrancolin");
        this.fixupParishName(425, 12567, "La Soula");
        this.fixupParishName(3534, 102847, "Siradan");
        this.fixupParishName(2942, 85973, "La Barthe-de-Neste");
        this.fixupParishName(2165, 63566, "Argelès-Gazost");
        this.fixupParishName(819, 24224, "Cauterets");
        this.fixupParishName(1065, 31248, "Arrens-Marsous");
        this.fixupParishName(1669, 49520, "Pouyastruc");
        this.fixupParishName(2914, 85342, "Labatut-Rivière");
        this.fixupParishName(3395, 98929, "Lafitole");
        this.fixupParishName(3227, 93882, "Trie-sur-Baise");
        this.fixupParishName(3879, 113082, "Issoudun");
        this.fixupParishName(96, 2427, "Trèves");
        this.fixupParishName(2905, 85039, "Vissec");
        this.fixupParishName(2694, 79033, "Centuri");
        this.fixupParishName(1, 95, "Sainte-Lucie-de-Tallano");
        this.fixupParishName(2639, 77006, "Falzet");
        this.fixupParishName(3861, 112648, "Chezal-Benoit");
        this.fixupParishName(2655, 77607, "Le Massegros");
        this.fixupParishName(3040, 89092, "Ocquier");
        this.fixupParishName(3273, 95292, "Lourdes");
        this.fixupParishName(2754, 81141, "Fontenouilles");
        this.fixupParishName(939, 27264, "San-Gavino-di-Carbini");
        this.villageList[28533].m_villageName = "Poitiers";
      }
      int num = mapData.mapName.ToLower() == "uk.wmpData".ToLower() ? 1 : 0;
      if (mapData.mapName.ToLower() == "ru.wmpData".ToLower())
      {
        this.countyList[55].areaName = "Минск";
        this.villageList[53871].villageName = "Кишинёв";
        this.countyList[54].areaName = "Витебская область";
      }
      if (mapData.mapName.ToLower() == "pl.wmpData".ToLower())
      {
        this.provincesList[0].areaName = "Zachodniopomorskie";
        this.provincesList[6].areaName = "Łódzkie";
      }
      if (mapData.mapName.ToLower() == "us.wmpData".ToLower())
      {
        this.fixupParishName(2166, 52241, "Niagara");
        this.fixupParishName(2698, 64654, "Juniata");
        this.fixupParishName(4336, 105829, "Barnstable");
        this.fixupParishName(2882, 69593, "Cuba");
        this.fixupParishName(1486, 35877, "Jacksonville");
        this.countyList[32].areaName = "East Kentucky";
        this.countyList[35].areaName = "South Indiana";
        this.countyList[51].areaName = "San Antonio";
      }
      if (mapData.mapName.ToLower() == "it.wmpData".ToLower())
      {
        this.countyList[112].areaName = "Osijek-Baranja";
        this.provincesList[37].areaName = "Sardegna";
        this.provincesList[7].areaName = "Toscana";
        this.fixupParishName(1505, 33173, "Krusevo");
        this.provincesList[27].areaName = "Južna Crna Gora";
        this.provincesList[28].areaName = "Sjeverna Crna Gora";
        this.countyList[116].areaName = "Bjelovar-Bilogora";
        this.villageList[28991].villageName = "Bjelovar";
        this.fixupParishName(3157, 69460, "Fagagna");
        this.fixupParishName(1400, 30548, "Cimolais");
      }
      if (mapData.mapName.ToLower() == "eu.wmpData".ToLower() || mapData.mapName.ToLower() == "euai.wmpData".ToLower())
      {
        this.countyList[191].areaName = "Götaland";
        this.countyList[189].areaName = "Svealand";
        this.countyList[188].areaName = "Norrland";
        this.countyList[192].areaName = "Dalarna";
        this.countyList[70].areaName = "Jylland";
        this.provincesList[30].areaName = "Region Jylland";
        this.countyList[60].areaName = "Gelre";
        this.countyList[58].areaName = "Holland";
        this.countyList[57].areaName = "Brabant";
        this.countyList[101].areaName = "Dunantùl Megye";
        this.countyList[102].areaName = "Duna-Tisza Köze Megye";
        this.countyList[103].areaName = "Tiszàntùl Megye";
        this.countyList[187].areaName = "Lapin Lääni";
        this.countyList[186].areaName = "Oulun Lääni";
        this.countyList[185].areaName = "Länsi-Suomen Lääni";
        this.countyList[184].areaName = "Itä-Suomen Lääni";
        this.countyList[125].areaName = "Област Враца";
        this.countyList[126].areaName = "Област Велико Търново";
        this.countyList[(int) sbyte.MaxValue].areaName = "Област Варна";
        this.countyList[129].areaName = "Област Пловдив";
        this.countyList[176].areaName = "Harjumaa";
        this.countyList[53].areaName = "Distrito de Vila Real";
        this.provincesList[81].areaName = "Pohjois-Suomi";
        this.provincesList[80].areaName = "Etelä-Suomi";
        this.countyList[124].areaName = "Κρήτη";
        this.countyList[123].areaName = "Πελοπόννησος";
        this.countyList[122].areaName = "Στερεά Ελλάδα";
        this.countyList[120].areaName = "Θεσσαλία";
        this.countyList[119].areaName = "Κεντρική Μακεδονία";
        this.countyList[121].areaName = "Ανατολική Μακεδονία και Θράκη";
        this.countyList[54].areaName = "Estremadura";
        this.countyList[53].areaName = "Douro";
        this.countyList[52].areaName = "Beiras";
        this.countyList[51].areaName = "Alentejo";
        this.countyList[65].areaName = "Sachsen";
        this.countyList[95].areaName = "Jihomoravský kraj";
        this.countyList[2].areaName = "Trøndelag";
        this.provincesList[10].areaName = "Eastern Ireland";
        this.fixupParishName(2932, 64799, "Builth Wells");
        this.fixupParishName(2187, 48426, "Skegness");
        this.countyList[4].areaName = "Inverness-shire";
        this.fixupParishName(4124, 93874, "Karlovy Vary");
        this.fixupParishName(26, 511, "Сергиев Посад");
        string villageName = this.villageList[55123].m_villageName;
        this.villageList[55123].m_villageName = this.villageList[31844].m_villageName;
        this.villageList[31844].m_villageName = villageName;
      }
      if (mapData.mapName.ToLower() == "wrld.wmpData".ToLower())
      {
        this.countryList[66].areaName = "Philippines";
        this.provincesList[66].areaName = "Philippines";
      }
      this.updateRegionsNamesBasedOnLanguage();
    }

    private void fixupParishName(int parishID, int villageID, string newName)
    {
      this.villageList[villageID].m_villageName = newName;
      this.regionList[parishID].areaName = newName;
    }

    private WorldMap.Triangle makeTriangle(
      WorldMap.WorldPoint p1,
      WorldMap.WorldPoint p2,
      WorldMap.WorldPoint p3)
    {
      return new WorldMap.Triangle()
      {
        x1 = p1.x,
        y1 = p1.y,
        x2 = p2.x,
        y2 = p2.y,
        x3 = p3.x,
        y3 = p3.y
      };
    }

    public bool SmallMapFont => this.smallMapFont;

    public void loadData(string dataName)
    {
      this.drawFakeProvinceBorders = false;
      this.EUMap = false;
      this.GSMap = false;
      this.KGMap = false;
      this.yMarkerOffset = 0;
      int num1 = 0;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      bool flag5 = false;
      bool flag6 = false;
      bool EUAI = false;
      this.chMap = false;
      this.mapDataFilename = dataName;
      if (dataName.ToLower() == "de.wmpData".ToLower())
        flag5 = true;
      if (dataName.ToLower() == "sa.wmpData".ToLower())
        this.yMarkerOffset = -10;
      if (dataName.ToLower() == "uk.wmpData".ToLower())
        flag1 = true;
      if (dataName.ToLower() == "ukai.wmpData".ToLower())
        flag6 = true;
      if (dataName.ToLower() == "uk2.wmpData".ToLower())
        flag2 = true;
      if (dataName.ToLower() == "fr.wmpData".ToLower())
      {
        num1 = 1;
        flag3 = true;
      }
      if (dataName.ToLower() == "es.wmpData".ToLower())
      {
        num1 = 1;
        flag4 = true;
      }
      this.smallMapFont = false;
      if (dataName.ToLower() == "eu.wmpData".ToLower() || dataName.ToLower() == "wrld.wmpData".ToLower() || dataName.ToLower() == "ch.wmpData".ToLower())
      {
        this.smallMapFont = true;
        this.EUMap = true;
        if (dataName.ToLower() == "ch.wmpData".ToLower())
          this.chMap = true;
      }
      if (dataName.ToLower() == "wrld.wmpData".ToLower())
        this.GSMap = true;
      if (dataName.ToLower() == "euai.wmpData".ToLower())
      {
        this.smallMapFont = true;
        this.EUMap = true;
        EUAI = true;
      }
      if (dataName.ToLower() == "king.wmpData".ToLower())
      {
        this.KGMap = true;
        this.smallMapFont = true;
        this.EUMap = true;
      }
      dataName = Application.StartupPath + "\\assets\\" + dataName;
      FileStream input = new FileStream(dataName, FileMode.Open, FileAccess.Read);
      BinaryReader binaryReader = new BinaryReader((Stream) input);
      this.LoadLocalisedParishNamesFromFiles();
      int num2 = binaryReader.ReadInt32();
      for (int index = 0; index < num2; ++index)
        binaryReader.ReadString();
      int length1 = binaryReader.ReadInt32();
      int[] numArray1 = new int[length1];
      for (int index = 0; index < length1; ++index)
        numArray1[index] = binaryReader.ReadInt32();
      binaryReader.Close();
      input.Close();
      int num3 = 0;
      int[] numArray2 = numArray1;
      int index1 = num3;
      int num4 = index1 + 1;
      this.worldMapWidth = numArray2[index1];
      int[] numArray3 = numArray1;
      int index2 = num4;
      int num5 = index2 + 1;
      this.worldMapHeight = numArray3[index2];
      int[] numArray4 = numArray1;
      int index3 = num5;
      int num6 = index3 + 1;
      this.villageMapWidth = numArray4[index3];
      int[] numArray5 = numArray1;
      int index4 = num6;
      int num7 = index4 + 1;
      this.villageMapHeight = numArray5[index4];
      int[] numArray6 = numArray1;
      int index5 = num7;
      int num8 = index5 + 1;
      int index6 = numArray6[index5];
      this.pointList = new WorldMap.WorldPoint[index6 + num1];
      for (int index7 = 0; index7 < index6; ++index7)
      {
        WorldMap.WorldPoint worldPoint1 = new WorldMap.WorldPoint();
        WorldMap.WorldPoint worldPoint2 = worldPoint1;
        int[] numArray7 = numArray1;
        int index8 = num8;
        int num9 = index8 + 1;
        double num10 = (double) numArray7[index8];
        worldPoint2.x = (float) num10;
        WorldMap.WorldPoint worldPoint3 = worldPoint1;
        int[] numArray8 = numArray1;
        int index9 = num9;
        num8 = index9 + 1;
        double num11 = (double) numArray8[index9];
        worldPoint3.y = (float) num11;
        this.pointList[index7] = worldPoint1;
      }
      if (flag3)
        this.pointList[index6] = new WorldMap.WorldPoint()
        {
          x = 1885f,
          y = 2204f
        };
      int num12 = flag4 ? 1 : 0;
      int[] numArray9 = numArray1;
      int index10 = num8;
      int num13 = index10 + 1;
      int length2 = numArray9[index10];
      this.countryList = new WorldMap.WorldPointList[length2];
      for (int index11 = 0; index11 < length2; ++index11)
      {
        WorldMap.WorldPointList worldPointList1 = new WorldMap.WorldPointList();
        WorldMap.WorldPointList worldPointList2 = worldPointList1;
        int[] numArray10 = numArray1;
        int index12 = num13;
        int num14 = index12 + 1;
        int num15 = numArray10[index12];
        worldPointList2.parentID = num15;
        WorldMap.WorldPointList worldPointList3 = worldPointList1;
        int[] numArray11 = numArray1;
        int index13 = num14;
        int num16 = index13 + 1;
        int num17 = numArray11[index13];
        worldPointList3.capitalVillage = num17;
        int[] numArray12 = numArray1;
        int index14 = num16;
        int num18 = index14 + 1;
        int x = numArray12[index14];
        int[] numArray13 = numArray1;
        int index15 = num18;
        int num19 = index15 + 1;
        int y = numArray13[index15];
        worldPointList1.marker = new Point(x, y);
        int num20 = num19 + 1;
        int[] numArray14 = numArray1;
        int index16 = num20;
        int num21 = index16 + 1;
        int length3 = numArray14[index16];
        worldPointList1.borderList = new int[length3];
        for (int index17 = 0; index17 < length3; ++index17)
        {
          worldPointList1.borderList[index17] = numArray1[num21++];
          worldPointList1.updateBounds(this.pointList[worldPointList1.borderList[index17]]);
        }
        int[] numArray15 = numArray1;
        int index18 = num21;
        int num22 = index18 + 1;
        int length4 = numArray15[index18];
        worldPointList1.childList = new int[length4];
        for (int index19 = 0; index19 < length4; ++index19)
          worldPointList1.childList[index19] = numArray1[num22++];
        int[] numArray16 = numArray1;
        int index20 = num22;
        num13 = index20 + 1;
        int length5 = numArray16[index20];
        worldPointList1.triangleList = new WorldMap.Triangle[length5];
        for (int index21 = 0; index21 < length5; ++index21)
        {
          WorldMap.Triangle triangle1 = new WorldMap.Triangle();
          WorldMap.Triangle triangle2 = triangle1;
          int[] numArray17 = numArray1;
          int index22 = num13;
          int num23 = index22 + 1;
          double num24 = (double) numArray17[index22] / 10000.0;
          triangle2.x1 = (float) num24;
          WorldMap.Triangle triangle3 = triangle1;
          int[] numArray18 = numArray1;
          int index23 = num23;
          int num25 = index23 + 1;
          double num26 = (double) numArray18[index23] / 10000.0;
          triangle3.y1 = (float) num26;
          WorldMap.Triangle triangle4 = triangle1;
          int[] numArray19 = numArray1;
          int index24 = num25;
          int num27 = index24 + 1;
          double num28 = (double) numArray19[index24] / 10000.0;
          triangle4.x2 = (float) num28;
          WorldMap.Triangle triangle5 = triangle1;
          int[] numArray20 = numArray1;
          int index25 = num27;
          int num29 = index25 + 1;
          double num30 = (double) numArray20[index25] / 10000.0;
          triangle5.y2 = (float) num30;
          WorldMap.Triangle triangle6 = triangle1;
          int[] numArray21 = numArray1;
          int index26 = num29;
          int num31 = index26 + 1;
          double num32 = (double) numArray21[index26] / 10000.0;
          triangle6.x3 = (float) num32;
          WorldMap.Triangle triangle7 = triangle1;
          int[] numArray22 = numArray1;
          int index27 = num31;
          num13 = index27 + 1;
          double num33 = (double) numArray22[index27] / 10000.0;
          triangle7.y3 = (float) num33;
          worldPointList1.triangleList[index21] = triangle1;
        }
        this.countryList[index11] = worldPointList1;
      }
      int[] numArray23 = numArray1;
      int index28 = num13;
      int num34 = index28 + 1;
      int length6 = numArray23[index28];
      this.provincesList = new WorldMap.WorldPointList[length6];
      for (int index29 = 0; index29 < length6; ++index29)
      {
        WorldMap.WorldPointList worldPointList4 = new WorldMap.WorldPointList();
        WorldMap.WorldPointList worldPointList5 = worldPointList4;
        int[] numArray24 = numArray1;
        int index30 = num34;
        int num35 = index30 + 1;
        int num36 = numArray24[index30];
        worldPointList5.parentID = num36;
        WorldMap.WorldPointList worldPointList6 = worldPointList4;
        int[] numArray25 = numArray1;
        int index31 = num35;
        int num37 = index31 + 1;
        int num38 = numArray25[index31];
        worldPointList6.capitalVillage = num38;
        int[] numArray26 = numArray1;
        int index32 = num37;
        int num39 = index32 + 1;
        int x = numArray26[index32];
        int[] numArray27 = numArray1;
        int index33 = num39;
        int num40 = index33 + 1;
        int y = numArray27[index33];
        worldPointList4.marker = new Point(x, y);
        int num41 = num40 + 1;
        int[] numArray28 = numArray1;
        int index34 = num41;
        int num42 = index34 + 1;
        int length7 = numArray28[index34];
        worldPointList4.borderList = new int[length7];
        for (int index35 = 0; index35 < length7; ++index35)
        {
          worldPointList4.borderList[index35] = numArray1[num42++];
          worldPointList4.updateBounds(this.pointList[worldPointList4.borderList[index35]]);
        }
        int[] numArray29 = numArray1;
        int index36 = num42;
        int num43 = index36 + 1;
        int length8 = numArray29[index36];
        worldPointList4.childList = new int[length8];
        for (int index37 = 0; index37 < length8; ++index37)
          worldPointList4.childList[index37] = numArray1[num43++];
        int[] numArray30 = numArray1;
        int index38 = num43;
        num34 = index38 + 1;
        int length9 = numArray30[index38];
        worldPointList4.triangleList = new WorldMap.Triangle[length9];
        for (int index39 = 0; index39 < length9; ++index39)
        {
          WorldMap.Triangle triangle8 = new WorldMap.Triangle();
          WorldMap.Triangle triangle9 = triangle8;
          int[] numArray31 = numArray1;
          int index40 = num34;
          int num44 = index40 + 1;
          double num45 = (double) numArray31[index40] / 10000.0;
          triangle9.x1 = (float) num45;
          WorldMap.Triangle triangle10 = triangle8;
          int[] numArray32 = numArray1;
          int index41 = num44;
          int num46 = index41 + 1;
          double num47 = (double) numArray32[index41] / 10000.0;
          triangle10.y1 = (float) num47;
          WorldMap.Triangle triangle11 = triangle8;
          int[] numArray33 = numArray1;
          int index42 = num46;
          int num48 = index42 + 1;
          double num49 = (double) numArray33[index42] / 10000.0;
          triangle11.x2 = (float) num49;
          WorldMap.Triangle triangle12 = triangle8;
          int[] numArray34 = numArray1;
          int index43 = num48;
          int num50 = index43 + 1;
          double num51 = (double) numArray34[index43] / 10000.0;
          triangle12.y2 = (float) num51;
          WorldMap.Triangle triangle13 = triangle8;
          int[] numArray35 = numArray1;
          int index44 = num50;
          int num52 = index44 + 1;
          double num53 = (double) numArray35[index44] / 10000.0;
          triangle13.x3 = (float) num53;
          WorldMap.Triangle triangle14 = triangle8;
          int[] numArray36 = numArray1;
          int index45 = num52;
          num34 = index45 + 1;
          double num54 = (double) numArray36[index45] / 10000.0;
          triangle14.y3 = (float) num54;
          worldPointList4.triangleList[index39] = triangle8;
        }
        this.provincesList[index29] = worldPointList4;
      }
      int[] numArray37 = numArray1;
      int index46 = num34;
      int num55 = index46 + 1;
      int length10 = numArray37[index46];
      this.countyList = new WorldMap.WorldPointList[length10];
      for (int index47 = 0; index47 < length10; ++index47)
      {
        WorldMap.WorldPointList worldPointList7 = new WorldMap.WorldPointList();
        WorldMap.WorldPointList worldPointList8 = worldPointList7;
        int[] numArray38 = numArray1;
        int index48 = num55;
        int num56 = index48 + 1;
        int num57 = numArray38[index48];
        worldPointList8.parentID = num57;
        WorldMap.WorldPointList worldPointList9 = worldPointList7;
        int[] numArray39 = numArray1;
        int index49 = num56;
        int num58 = index49 + 1;
        int num59 = numArray39[index49];
        worldPointList9.capitalVillage = num59;
        int[] numArray40 = numArray1;
        int index50 = num58;
        int num60 = index50 + 1;
        int x = numArray40[index50];
        int[] numArray41 = numArray1;
        int index51 = num60;
        int num61 = index51 + 1;
        int y = numArray41[index51];
        worldPointList7.marker = new Point(x, y);
        int num62 = num61 + 1;
        int[] numArray42 = numArray1;
        int index52 = num62;
        int num63 = index52 + 1;
        int length11 = numArray42[index52];
        worldPointList7.borderList = new int[length11];
        for (int index53 = 0; index53 < length11; ++index53)
        {
          worldPointList7.borderList[index53] = numArray1[num63++];
          worldPointList7.updateBounds(this.pointList[worldPointList7.borderList[index53]]);
        }
        int[] numArray43 = numArray1;
        int index54 = num63;
        int num64 = index54 + 1;
        int length12 = numArray43[index54];
        worldPointList7.childList = new int[length12];
        for (int index55 = 0; index55 < length12; ++index55)
          worldPointList7.childList[index55] = numArray1[num64++];
        int[] numArray44 = numArray1;
        int index56 = num64;
        num55 = index56 + 1;
        int length13 = numArray44[index56];
        worldPointList7.triangleList = new WorldMap.Triangle[length13];
        for (int index57 = 0; index57 < length13; ++index57)
        {
          WorldMap.Triangle triangle15 = new WorldMap.Triangle();
          WorldMap.Triangle triangle16 = triangle15;
          int[] numArray45 = numArray1;
          int index58 = num55;
          int num65 = index58 + 1;
          double num66 = (double) numArray45[index58] / 10000.0;
          triangle16.x1 = (float) num66;
          WorldMap.Triangle triangle17 = triangle15;
          int[] numArray46 = numArray1;
          int index59 = num65;
          int num67 = index59 + 1;
          double num68 = (double) numArray46[index59] / 10000.0;
          triangle17.y1 = (float) num68;
          WorldMap.Triangle triangle18 = triangle15;
          int[] numArray47 = numArray1;
          int index60 = num67;
          int num69 = index60 + 1;
          double num70 = (double) numArray47[index60] / 10000.0;
          triangle18.x2 = (float) num70;
          WorldMap.Triangle triangle19 = triangle15;
          int[] numArray48 = numArray1;
          int index61 = num69;
          int num71 = index61 + 1;
          double num72 = (double) numArray48[index61] / 10000.0;
          triangle19.y2 = (float) num72;
          WorldMap.Triangle triangle20 = triangle15;
          int[] numArray49 = numArray1;
          int index62 = num71;
          int num73 = index62 + 1;
          double num74 = (double) numArray49[index62] / 10000.0;
          triangle20.x3 = (float) num74;
          WorldMap.Triangle triangle21 = triangle15;
          int[] numArray50 = numArray1;
          int index63 = num73;
          num55 = index63 + 1;
          double num75 = (double) numArray50[index63] / 10000.0;
          triangle21.y3 = (float) num75;
          worldPointList7.triangleList[index57] = triangle15;
        }
        this.countyList[index47] = worldPointList7;
      }
      int[] numArray51 = numArray1;
      int index64 = num55;
      int num76 = index64 + 1;
      int length14 = numArray51[index64];
      this.villageList = new VillageData[length14];
      for (int index65 = 0; index65 < length14; ++index65)
      {
        VillageData villageData1 = new VillageData();
        VillageData villageData2 = villageData1;
        int[] numArray52 = numArray1;
        int index66 = num76;
        int num77 = index66 + 1;
        int num78 = numArray52[index66];
        villageData2.id = num78;
        VillageData villageData3 = villageData1;
        int[] numArray53 = numArray1;
        int index67 = num77;
        int num79 = index67 + 1;
        int num80 = (int) (short) numArray53[index67];
        villageData3.x = (short) num80;
        VillageData villageData4 = villageData1;
        int[] numArray54 = numArray1;
        int index68 = num79;
        int num81 = index68 + 1;
        int num82 = (int) (short) numArray54[index68];
        villageData4.y = (short) num82;
        if (index65 == 94886 && flag5)
          --villageData1.y;
        VillageData villageData5 = villageData1;
        int[] numArray55 = numArray1;
        int index69 = num81;
        int num83 = index69 + 1;
        int num84 = (int) (short) numArray55[index69];
        villageData5.countyID = (short) num84;
        VillageData villageData6 = villageData1;
        int[] numArray56 = numArray1;
        int index70 = num83;
        int num85 = index70 + 1;
        int num86 = (int) (short) numArray56[index70];
        villageData6.regionID = (short) num86;
        int[] numArray57 = numArray1;
        int index71 = num85;
        int num87 = index71 + 1;
        int num88 = numArray57[index71];
        villageData1.regionCapital = (num88 & 1) != 0;
        villageData1.countyCapital = (num88 & 8) != 0;
        villageData1.provinceCapital = (num88 & 16) != 0;
        villageData1.countryCapital = (num88 & 32) != 0;
        this.villageList[index65] = villageData1;
        num76 = num87 + 1;
      }
      int[] numArray58 = numArray1;
      int index72 = num76;
      int num89 = index72 + 1;
      int length15 = numArray58[index72];
      this.regionList = new WorldMap.WorldPointList[length15];
      for (int index73 = 0; index73 < length15; ++index73)
      {
        WorldMap.WorldPointList worldPointList10 = new WorldMap.WorldPointList();
        WorldMap.WorldPointList worldPointList11 = worldPointList10;
        int[] numArray59 = numArray1;
        int index74 = num89;
        int num90 = index74 + 1;
        int num91 = numArray59[index74];
        worldPointList11.parentID = num91;
        int num92 = num90 + 1;
        int[] numArray60 = numArray1;
        int index75 = num92;
        int num93 = index75 + 1;
        int length16 = numArray60[index75];
        worldPointList10.childList = new int[length16];
        for (int index76 = 0; index76 < length16; ++index76)
          worldPointList10.childList[index76] = numArray1[num93++];
        int[] numArray61 = numArray1;
        int index77 = num93;
        int num94 = index77 + 1;
        int length17 = numArray61[index77];
        worldPointList10.triangleList = new WorldMap.Triangle[length17];
        for (int index78 = 0; index78 < length17; ++index78)
        {
          WorldMap.Triangle triangle22 = new WorldMap.Triangle();
          WorldMap.Triangle triangle23 = triangle22;
          int[] numArray62 = numArray1;
          int index79 = num94;
          int num95 = index79 + 1;
          double num96 = (double) numArray62[index79] / 10000.0;
          triangle23.x1 = (float) num96;
          WorldMap.Triangle triangle24 = triangle22;
          int[] numArray63 = numArray1;
          int index80 = num95;
          int num97 = index80 + 1;
          double num98 = (double) numArray63[index80] / 10000.0;
          triangle24.y1 = (float) num98;
          WorldMap.Triangle triangle25 = triangle22;
          int[] numArray64 = numArray1;
          int index81 = num97;
          int num99 = index81 + 1;
          double num100 = (double) numArray64[index81] / 10000.0;
          triangle25.x2 = (float) num100;
          WorldMap.Triangle triangle26 = triangle22;
          int[] numArray65 = numArray1;
          int index82 = num99;
          int num101 = index82 + 1;
          double num102 = (double) numArray65[index82] / 10000.0;
          triangle26.y2 = (float) num102;
          WorldMap.Triangle triangle27 = triangle22;
          int[] numArray66 = numArray1;
          int index83 = num101;
          int num103 = index83 + 1;
          double num104 = (double) numArray66[index83] / 10000.0;
          triangle27.x3 = (float) num104;
          WorldMap.Triangle triangle28 = triangle22;
          int[] numArray67 = numArray1;
          int index84 = num103;
          num94 = index84 + 1;
          double num105 = (double) numArray67[index84] / 10000.0;
          triangle28.y3 = (float) num105;
          worldPointList10.triangleList[index78] = triangle22;
        }
        worldPointList10.updateBoundsFromTriangles();
        int[] numArray68 = numArray1;
        int index85 = num94;
        num89 = index85 + 1;
        int length18 = numArray68[index85];
        worldPointList10.regionBorderList = new WorldMap.WorldPoint[length18];
        for (int index86 = 0; index86 < length18; ++index86)
        {
          WorldMap.WorldPoint worldPoint4 = new WorldMap.WorldPoint();
          WorldMap.WorldPoint worldPoint5 = worldPoint4;
          int[] numArray69 = numArray1;
          int index87 = num89;
          int num106 = index87 + 1;
          double num107 = (double) numArray69[index87];
          worldPoint5.x = (float) num107;
          WorldMap.WorldPoint worldPoint6 = worldPoint4;
          int[] numArray70 = numArray1;
          int index88 = num106;
          num89 = index88 + 1;
          double num108 = (double) numArray70[index88];
          worldPoint6.y = (float) num108;
          worldPointList10.regionBorderList[index86] = worldPoint4;
        }
        this.regionList[index73] = worldPointList10;
      }
      foreach (VillageData village in this.villageList)
      {
        if (village.regionCapital)
          this.regionList[(int) village.regionID].capitalVillage = village.id;
      }
      int[] numArray71 = numArray1;
      int index89 = num89;
      int num109 = index89 + 1;
      int num110 = numArray71[index89];
      List<WorldMap.WorldPointList> worldPointListList = new List<WorldMap.WorldPointList>();
      for (int index90 = 0; index90 < num110; ++index90)
      {
        WorldMap.WorldPointList worldPointList12 = new WorldMap.WorldPointList();
        WorldMap.WorldPointList worldPointList13 = worldPointList12;
        int[] numArray72 = numArray1;
        int index91 = num109;
        int num111 = index91 + 1;
        int num112 = numArray72[index91];
        worldPointList13.data = num112;
        if (worldPointList12.data >= 0)
        {
          int[] numArray73 = numArray1;
          int index92 = num111;
          num109 = index92 + 1;
          int length19 = numArray73[index92];
          worldPointList12.triangleList = new WorldMap.Triangle[length19];
          for (int index93 = 0; index93 < length19; ++index93)
          {
            WorldMap.Triangle triangle29 = new WorldMap.Triangle();
            WorldMap.Triangle triangle30 = triangle29;
            int[] numArray74 = numArray1;
            int index94 = num109;
            int num113 = index94 + 1;
            double num114 = (double) numArray74[index94] / 10000.0;
            triangle30.x1 = (float) num114;
            WorldMap.Triangle triangle31 = triangle29;
            int[] numArray75 = numArray1;
            int index95 = num113;
            int num115 = index95 + 1;
            double num116 = (double) numArray75[index95] / 10000.0;
            triangle31.y1 = (float) num116;
            WorldMap.Triangle triangle32 = triangle29;
            int[] numArray76 = numArray1;
            int index96 = num115;
            int num117 = index96 + 1;
            double num118 = (double) numArray76[index96] / 10000.0;
            triangle32.x2 = (float) num118;
            WorldMap.Triangle triangle33 = triangle29;
            int[] numArray77 = numArray1;
            int index97 = num117;
            int num119 = index97 + 1;
            double num120 = (double) numArray77[index97] / 10000.0;
            triangle33.y2 = (float) num120;
            WorldMap.Triangle triangle34 = triangle29;
            int[] numArray78 = numArray1;
            int index98 = num119;
            int num121 = index98 + 1;
            double num122 = (double) numArray78[index98] / 10000.0;
            triangle34.x3 = (float) num122;
            WorldMap.Triangle triangle35 = triangle29;
            int[] numArray79 = numArray1;
            int index99 = num121;
            num109 = index99 + 1;
            double num123 = (double) numArray79[index99] / 10000.0;
            triangle35.y3 = (float) num123;
            worldPointList12.triangleList[index93] = triangle29;
          }
          worldPointList12.updateBoundsFromTriangles();
          worldPointListList.Add(worldPointList12);
        }
        else
        {
          this.drawFakeProvinceBorders = true;
          if (worldPointList12.data > -2000)
          {
            WorldMap.WorldPointList provinces = this.provincesList[-worldPointList12.data - 1000];
            List<int> intList = new List<int>();
            if (!provinces.rebuiltBorderList)
              provinces.rebuiltBorderList = true;
            intList.AddRange((IEnumerable<int>) provinces.borderList);
            intList.Add(-1);
            int[] numArray80 = numArray1;
            int index100 = num111;
            int num124 = index100 + 1;
            int num125 = numArray80[index100];
            for (int index101 = 0; index101 < num125; ++index101)
            {
              int index102 = numArray1[num124++];
              intList.Add(index102);
              provinces.updateBounds(this.pointList[index102]);
            }
            provinces.borderList = intList.ToArray();
            int[] numArray81 = numArray1;
            int index103 = num124;
            num109 = index103 + 1;
            int num126 = numArray81[index103];
            List<WorldMap.Triangle> triangleList = new List<WorldMap.Triangle>();
            triangleList.AddRange((IEnumerable<WorldMap.Triangle>) provinces.triangleList);
            for (int index104 = 0; index104 < num126; ++index104)
            {
              WorldMap.Triangle triangle36 = new WorldMap.Triangle();
              WorldMap.Triangle triangle37 = triangle36;
              int[] numArray82 = numArray1;
              int index105 = num109;
              int num127 = index105 + 1;
              double num128 = (double) numArray82[index105] / 10000.0;
              triangle37.x1 = (float) num128;
              WorldMap.Triangle triangle38 = triangle36;
              int[] numArray83 = numArray1;
              int index106 = num127;
              int num129 = index106 + 1;
              double num130 = (double) numArray83[index106] / 10000.0;
              triangle38.y1 = (float) num130;
              WorldMap.Triangle triangle39 = triangle36;
              int[] numArray84 = numArray1;
              int index107 = num129;
              int num131 = index107 + 1;
              double num132 = (double) numArray84[index107] / 10000.0;
              triangle39.x2 = (float) num132;
              WorldMap.Triangle triangle40 = triangle36;
              int[] numArray85 = numArray1;
              int index108 = num131;
              int num133 = index108 + 1;
              double num134 = (double) numArray85[index108] / 10000.0;
              triangle40.y2 = (float) num134;
              WorldMap.Triangle triangle41 = triangle36;
              int[] numArray86 = numArray1;
              int index109 = num133;
              int num135 = index109 + 1;
              double num136 = (double) numArray86[index109] / 10000.0;
              triangle41.x3 = (float) num136;
              WorldMap.Triangle triangle42 = triangle36;
              int[] numArray87 = numArray1;
              int index110 = num135;
              num109 = index110 + 1;
              double num137 = (double) numArray87[index110] / 10000.0;
              triangle42.y3 = (float) num137;
              triangleList.Add(triangle36);
            }
            provinces.triangleList = triangleList.ToArray();
          }
          else
          {
            WorldMap.WorldPointList country = this.countryList[-worldPointList12.data - 3000];
            List<int> intList = new List<int>();
            if (!country.rebuiltBorderList)
              country.rebuiltBorderList = true;
            intList.AddRange((IEnumerable<int>) country.borderList);
            intList.Add(-1);
            int[] numArray88 = numArray1;
            int index111 = num111;
            int num138 = index111 + 1;
            int num139 = numArray88[index111];
            for (int index112 = 0; index112 < num139; ++index112)
            {
              int index113 = numArray1[num138++];
              intList.Add(index113);
              country.updateBounds(this.pointList[index113]);
            }
            country.borderList = intList.ToArray();
            int[] numArray89 = numArray1;
            int index114 = num138;
            num109 = index114 + 1;
            int num140 = numArray89[index114];
            List<WorldMap.Triangle> triangleList = new List<WorldMap.Triangle>();
            triangleList.AddRange((IEnumerable<WorldMap.Triangle>) country.triangleList);
            for (int index115 = 0; index115 < num140; ++index115)
            {
              WorldMap.Triangle triangle43 = new WorldMap.Triangle();
              WorldMap.Triangle triangle44 = triangle43;
              int[] numArray90 = numArray1;
              int index116 = num109;
              int num141 = index116 + 1;
              double num142 = (double) numArray90[index116] / 10000.0;
              triangle44.x1 = (float) num142;
              WorldMap.Triangle triangle45 = triangle43;
              int[] numArray91 = numArray1;
              int index117 = num141;
              int num143 = index117 + 1;
              double num144 = (double) numArray91[index117] / 10000.0;
              triangle45.y1 = (float) num144;
              WorldMap.Triangle triangle46 = triangle43;
              int[] numArray92 = numArray1;
              int index118 = num143;
              int num145 = index118 + 1;
              double num146 = (double) numArray92[index118] / 10000.0;
              triangle46.x2 = (float) num146;
              WorldMap.Triangle triangle47 = triangle43;
              int[] numArray93 = numArray1;
              int index119 = num145;
              int num147 = index119 + 1;
              double num148 = (double) numArray93[index119] / 10000.0;
              triangle47.y2 = (float) num148;
              WorldMap.Triangle triangle48 = triangle43;
              int[] numArray94 = numArray1;
              int index120 = num147;
              int num149 = index120 + 1;
              double num150 = (double) numArray94[index120] / 10000.0;
              triangle48.x3 = (float) num150;
              WorldMap.Triangle triangle49 = triangle43;
              int[] numArray95 = numArray1;
              int index121 = num149;
              num109 = index121 + 1;
              double num151 = (double) numArray95[index121] / 10000.0;
              triangle49.y3 = (float) num151;
              triangleList.Add(triangle43);
            }
            country.triangleList = triangleList.ToArray();
          }
        }
      }
      this.seaList = worldPointListList.ToArray();
      int[] numArray96 = numArray1;
      int index122 = num109;
      int num152 = index122 + 1;
      int length20 = numArray96[index122];
      this.islandList = new WorldMap.IslandInfoList[length20];
      for (int index123 = 0; index123 < length20; ++index123)
      {
        WorldMap.IslandInfoList islandInfoList1 = new WorldMap.IslandInfoList();
        WorldMap.IslandInfoList islandInfoList2 = islandInfoList1;
        int[] numArray97 = numArray1;
        int index124 = num152;
        int num153 = index124 + 1;
        int num154 = numArray97[index124];
        islandInfoList2.county = num154;
        WorldMap.IslandInfoList islandInfoList3 = islandInfoList1;
        int[] numArray98 = numArray1;
        int index125 = num153;
        int num155 = index125 + 1;
        int num156 = numArray98[index125];
        islandInfoList3.province = num156;
        WorldMap.IslandInfoList islandInfoList4 = islandInfoList1;
        int[] numArray99 = numArray1;
        int index126 = num155;
        int num157 = index126 + 1;
        int num158 = numArray99[index126];
        islandInfoList4.country = num158;
        WorldMap.IslandInfoList islandInfoList5 = islandInfoList1;
        int[] numArray100 = numArray1;
        int index127 = num157;
        int num159 = index127 + 1;
        int num160 = numArray100[index127];
        islandInfoList5.sx = num160;
        WorldMap.IslandInfoList islandInfoList6 = islandInfoList1;
        int[] numArray101 = numArray1;
        int index128 = num159;
        int num161 = index128 + 1;
        int num162 = numArray101[index128];
        islandInfoList6.sy = num162;
        WorldMap.IslandInfoList islandInfoList7 = islandInfoList1;
        int[] numArray102 = numArray1;
        int index129 = num161;
        int num163 = index129 + 1;
        int num164 = numArray102[index129];
        islandInfoList7.ex = num164;
        WorldMap.IslandInfoList islandInfoList8 = islandInfoList1;
        int[] numArray103 = numArray1;
        int index130 = num163;
        num152 = index130 + 1;
        int num165 = numArray103[index130];
        islandInfoList8.ey = num165;
        this.islandList[index123] = islandInfoList1;
      }
      if (flag1)
      {
        this.villageList[42538].x = (short) 1255;
        this.villageList[42538].y = (short) 1044;
        this.villageList[22358].x = (short) 1078;
        this.villageList[22358].y = (short) 525;
        this.villageList[73093].x = (short) 1060;
        this.villageList[73093].y = (short) 528;
        this.villageList[49242].x = (short) 1081;
        this.villageList[49242].y = (short) 533;
        this.villageList[77800].x = (short) 1070;
        this.villageList[77800].y = (short) 529;
        this.villageList[69149].x = (short) 1041;
        this.villageList[69149].y = (short) 545;
        this.villageList[49042].x = (short) 614;
        this.villageList[49042].y = (short) 2070;
        this.villageList[55962].x = (short) 1527;
        this.villageList[55962].y = (short) 1303;
        this.villageList[98865].x = (short) 1532;
        this.villageList[98865].y = (short) 1298;
        this.villageList[65073].x = (short) 978;
        this.villageList[65073].y = (short) 950;
        this.villageList[19033].x = (short) 1120;
        this.villageList[19033].y = (short) 867;
        this.villageList[38352].x = (short) 1115;
        this.villageList[38352].y = (short) 874;
      }
      if (flag1 || flag2 || flag6)
      {
        this.villageList[69936].x = (short) 1826;
        this.villageList[69936].y = (short) 2747;
      }
      if (flag5)
      {
        this.villageList[8650].x = (short) 1701;
        this.villageList[8650].y = (short) 2637;
      }
      if (flag4)
      {
        this.villageList[59419].x = (short) 1452;
        this.villageList[59419].y = (short) 1396;
        this.villageList[26428].x = (short) 1466;
        this.villageList[26428].y = (short) 1275;
        this.villageList[32451].x = (short) 1450;
        this.villageList[32451].y = (short) 1269;
      }
      this.aiWorldTreeBuilding = false;
      this.aiWorldInvasionLineList = (List<WorldMap.IslandInfoList>) null;
      this.aiWorldSpecialVillages.Clear();
      if (flag6 || EUAI)
      {
        this.aiWorldTreeBuilding = true;
        this.aiWorldInvasionLineList = new List<WorldMap.IslandInfoList>();
        foreach (VillageData village in this.villageList)
        {
          int x1 = (int) village.x;
          int y1 = (int) village.y;
          int x2 = (int) village.x;
          int y2 = (int) village.y;
          if (AIWorldSettings.getAIWorldVillageLocation(village.id, ref x2, ref y2, EUAI))
          {
            village.x = (short) x2;
            village.y = (short) y2;
            WorldMap.IslandInfoList islandInfoList = new WorldMap.IslandInfoList();
            islandInfoList.villageID = village.id;
            double num166 = (double) x1;
            double num167 = (double) y1;
            double x3 = (double) village.x;
            double y3 = (double) village.y;
            double num168 = num166 - x3;
            double num169 = num167 - y3;
            double num170 = Math.Sqrt(num168 * num168 + num169 * num169);
            double num171 = num168 / num170;
            double num172 = num169 / num170;
            double num173 = num166 - num171 * 50.0;
            double num174 = num167 - num172 * 50.0;
            double num175 = x3 + num171 * 25.0;
            double num176 = y3 + num172 * 25.0;
            islandInfoList.sx = (int) num173;
            islandInfoList.sy = (int) num174;
            islandInfoList.ex = (int) num175;
            islandInfoList.ey = (int) num176;
            this.aiWorldInvasionLineList.Add(islandInfoList);
            this.aiWorldSpecialVillages.Add(village.id);
          }
        }
      }
      this.initUserVillages();
    }

    public void buildVillageTree()
    {
      this.m_baseNode = new WorldMap.VillageQuadNode(0, 0, this.villageMapWidth, this.villageMapHeight, 0);
      this.m_baseNode.setWorld(this);
      foreach (VillageData village in this.villageList)
        this.m_baseNode.addVillage(village, 0);
      UniversalDebugLog.Log(WorldMap.VillageQuadNode.level0Nodes.ToString() + " level 0 nodes");
      UniversalDebugLog.Log(WorldMap.VillageQuadNode.level1Nodes.ToString() + " level 1 nodes");
      UniversalDebugLog.Log(WorldMap.VillageQuadNode.level2Nodes.ToString() + " level 2 nodes");
      UniversalDebugLog.Log(WorldMap.VillageQuadNode.level3Nodes.ToString() + " level 3 nodes");
      UniversalDebugLog.Log(WorldMap.VillageQuadNode.level4Nodes.ToString() + " level 4 nodes");
      UniversalDebugLog.Log(WorldMap.VillageQuadNode.level5Nodes.ToString() + " level 5 nodes");
      UniversalDebugLog.Log(WorldMap.VillageQuadNode.level6Nodes.ToString() + " level 6 nodes");
      UniversalDebugLog.Log(WorldMap.VillageQuadNode.level7Nodes.ToString() + " level 7 nodes");
      UniversalDebugLog.Log(WorldMap.VillageQuadNode.level8Nodes.ToString() + " level 8 nodes");
      UniversalDebugLog.Log(WorldMap.VillageQuadNode.level9Nodes.ToString() + " level 9 nodes");
      UniversalDebugLog.Log(this.villageList.Length.ToString() + " villages in total. " + (object) WorldMap.VillageQuadNode.villagesInNodes + " in nodes");
      UniversalDebugLog.Log(WorldMap.VillageQuadNode.parishesInNodes.ToString() + " parishes");
      UniversalDebugLog.Log(WorldMap.VillageQuadNode.capitalsInNodes.ToString() + " capitals");
      this.WorldZoom = 4.0;
      this.m_screenCentreX = (double) this.worldMapWidth / 2.0;
      this.m_screenCentreY = (double) this.worldMapHeight / 2.0;
    }

    public void loadLocalWorldData()
    {
      this.m_factionData.Clear();
      this.m_downloadedDataSafely = true;
      if (!this.m_cachesFlushed)
      {
        this.m_dataLoaded = this.loadFactionData();
        this.m_namesLoaded = this.loadNamesData();
        UniversalDebugLog.Log("m_dataLoaded = " + this.m_dataLoaded.ToString());
        UniversalDebugLog.Log("m_namesLoaded = " + this.m_namesLoaded.ToString());
        int num1 = 0;
        int num2 = 0;
        foreach (VillageData village in this.villageList)
        {
          if (village.villageName.Length == 0)
            ++num1;
          if (village.visible)
            ++num2;
        }
        if (num1 <= 500 && num2 >= 2)
          return;
        this.m_dataLoaded = false;
        this.m_namesLoaded = false;
      }
      else
      {
        this.m_cachesFlushed = false;
        this.m_dataLoaded = false;
        this.m_namesLoaded = false;
      }
    }

    public void invalidateWorldData()
    {
      this.m_dataLoaded = false;
      this.loginHistory = (List<LoginHistoryInfo>) null;
      this.m_userVillages = (List<WorldMap.UserVillageData>) null;
    }

    public void flushCaches() => this.m_cachesFlushed = true;

    public void updateWorldMapOwnership()
    {
      GameEngine.Instance.World.clearPersonArray(-1);
      this.downloadingCounter = 0;
      this.downloadComplete = false;
      this.downloadFullyComplete = false;
      this.delayedFactionSave = false;
      this.m_WorkerThread = new Thread(new ThreadStart(this.updateWorldMapOwnershipX));
      this.m_WorkerThread.Name = "Downloading";
      this.m_WorkerThread.Start();
    }

    public bool isDownloadComplete() => this.downloadComplete;

    public bool isDownloadFullyComplete() => this.downloadFullyComplete;

    public bool isWorkerThreadAlive() => this.m_WorkerThread != null && this.m_WorkerThread.IsAlive;

    private void downloadWait()
    {
      Thread.Sleep(this.threadDelaySize);
      DateTime now = DateTime.Now;
      while (!RemoteServices.Instance.queueEmpty())
      {
        Thread.Sleep(20);
        if (GameEngine.Instance.loginCancelled())
          this.m_WorkerThread.Abort();
        if ((DateTime.Now - now).TotalMinutes > 10.0)
          break;
      }
      if (GameEngine.Instance.loginCancelled())
        this.m_WorkerThread.Abort();
      if (!this.loadingErrored)
        ++this.downloadingCounter;
      else
        Thread.Sleep(2000 + new Random().Next(1000));
      GC.Collect();
      GC.WaitForPendingFinalizers();
    }

    public void maybeMultiAccount(int level)
    {
    }

    public void updateWorldMapOwnershipX()
    {
      RemoteServices.Instance.set_FullTick_UserCallBack(new RemoteServices.FullTick_UserCallBack(this.fullTickCallBack));
      this.requestedReturnedArmyIDs.Clear();
      GameEngine.Instance.setPendingSessionExpiredStat(-1);
      if (!this.m_namesLoaded)
        this.storedVillageNamePos = -1L;
      int num1 = 0;
      do
      {
        this.loadingErrored = false;
        RemoteServices.Instance.set_GetVillageNames_UserCallBack(new RemoteServices.GetVillageNames_UserCallBack(this.villageNamesCallBack));
        RemoteServices.Instance.GetVillageNames(this.storedVillageNamePos);
        this.downloadWait();
      }
      while ((this.loadingErrored || !RemoteServices.Instance.GetVillageNames_ValidDownload) && num1++ < 3);
      if (this.loadingErrored || !RemoteServices.Instance.GetVillageNames_ValidDownload)
      {
        GameEngine.Instance.setPendingSessionExpiredStat(2);
      }
      else
      {
        int num2 = 0;
        do
        {
          this.loadingErrored = false;
          this.retrieveUserVillages(true);
          this.downloadWait();
        }
        while (this.loadingErrored && num2++ < 3);
        if (this.loadingErrored)
        {
          GameEngine.Instance.setPendingSessionExpiredStat(2);
        }
        else
        {
          if (!this.m_dataLoaded)
          {
            this.storedRegionFactionsPos = -1L;
            this.storedCountyFactionsPos = -1L;
            this.storedProvinceFactionsPos = -1L;
            this.storedVillageFactionsPos = -1L;
            this.storedCountryFactionsPos = -1L;
            this.storedFactionChangesPos = -1L;
            this.storedParishFlagsPos = -1L;
            this.storedCountyFlagsPos = -1L;
            this.storedProvinceFlagsPos = -1L;
            this.storedCountryFlagsPos = -1L;
            foreach (VillageData village in this.villageList)
              village.numFlags = (short) 0;
          }
          int num3 = 0;
          do
          {
            this.loadingErrored = false;
            RemoteServices.Instance.set_GetAreaFactionChanges_UserCallBack(new RemoteServices.GetAreaFactionChanges_UserCallBack(this.getAreaFactionChangesCallback));
            RemoteServices.Instance.GetAreaFactionChanges(this.storedRegionFactionsPos - 50L, this.storedCountyFactionsPos - 10L, this.storedProvinceFactionsPos - 10L, this.storedCountryFactionsPos - 5L, this.storedParishFlagsPos, this.storedCountyFlagsPos, this.storedProvinceFlagsPos, this.storedCountryFlagsPos);
            this.downloadWait();
          }
          while (this.loadingErrored && num3++ < 3);
          if (this.loadingErrored)
          {
            GameEngine.Instance.setPendingSessionExpiredStat(2);
          }
          else
          {
            if (this.m_dataLoaded)
            {
              int num4 = 0;
              do
              {
                this.loadingErrored = false;
                RemoteServices.Instance.set_GetVillageFactionChanges_UserCallBack(new RemoteServices.GetVillageFactionChanges_UserCallBack(this.getVillageFactionChangesCallback));
                RemoteServices.Instance.GetVillageFactionChanges(this.storedVillageFactionsPos - 500L, this.storedFactionChangesPos - 10L);
                this.downloadWait();
              }
              while ((this.loadingErrored || !RemoteServices.Instance.GetVillageFactionChanges_ValidDownload) && num4++ < 3);
              if (this.loadingErrored || !RemoteServices.Instance.GetVillageFactionChanges_ValidDownload)
              {
                GameEngine.Instance.setPendingSessionExpiredStat(2);
                return;
              }
            }
            else
            {
              int num5 = 0;
              do
              {
                this.loadingErrored = false;
                RemoteServices.Instance.set_GetAllVillageOwnerFactions_UserCallBack(new RemoteServices.GetAllVillageOwnerFactions_UserCallBack(this.getAllVillageOwnerFactionsCallback));
                RemoteServices.Instance.GetAllVillageOwnerFactions();
                this.downloadWait();
              }
              while ((this.loadingErrored || !RemoteServices.Instance.GetAllVillageOwnerFactions_ValidDownload) && num5++ < 3);
              if (this.loadingErrored || !RemoteServices.Instance.GetAllVillageOwnerFactions_ValidDownload)
              {
                GameEngine.Instance.setPendingSessionExpiredStat(2);
                return;
              }
            }
            int num6 = 0;
            do
            {
              this.loadingErrored = false;
              this.retrieveArmies();
              this.downloadWait();
            }
            while (this.loadingErrored && num6++ < 3);
            if (this.loadingErrored)
            {
              GameEngine.Instance.setPendingSessionExpiredStat(2);
            }
            else
            {
              int num7 = 0;
              do
              {
                this.loadingErrored = false;
                this.getTraderData();
                this.downloadWait();
              }
              while (this.loadingErrored && num7++ < 3);
              if (this.loadingErrored)
              {
                GameEngine.Instance.setPendingSessionExpiredStat(2);
              }
              else
              {
                int num8 = 0;
                do
                {
                  this.loadingErrored = false;
                  this.getActiveTraders();
                  this.downloadWait();
                }
                while (this.loadingErrored && num8++ < 3);
                if (this.loadingErrored)
                {
                  GameEngine.Instance.setPendingSessionExpiredStat(2);
                }
                else
                {
                  int num9 = 0;
                  do
                  {
                    this.loadingErrored = false;
                    this.getPersonData();
                    this.downloadWait();
                  }
                  while (this.loadingErrored && num9++ < 3);
                  if (this.loadingErrored)
                  {
                    GameEngine.Instance.setPendingSessionExpiredStat(2);
                  }
                  else
                  {
                    int num10 = 0;
                    do
                    {
                      this.loadingErrored = false;
                      this.getActivePeople();
                      this.downloadWait();
                    }
                    while (this.loadingErrored && num10++ < 3);
                    if (this.loadingErrored)
                    {
                      GameEngine.Instance.setPendingSessionExpiredStat(2);
                    }
                    else
                    {
                      InterfaceMgr.Instance.downCurrentFactionInfo();
                      this.downloadWait();
                      List<AchievementData> achievementData = (List<AchievementData>) null;
                      List<int> achievementsToTest = this.getAchievementsToTest(ref achievementData);
                      if (achievementsToTest != null && achievementsToTest.Count > 0)
                      {
                        int num11 = 0;
                        do
                        {
                          this.loadingErrored = false;
                          this.inTestAchievements = false;
                          this.testAchievements(achievementsToTest, achievementData, true);
                          this.downloadWait();
                        }
                        while (this.loadingErrored && num11++ < 3);
                        if (this.loadingErrored)
                        {
                          GameEngine.Instance.setPendingSessionExpiredStat(2);
                          return;
                        }
                      }
                      this.fixupNames();
                      this.downloadComplete = true;
                      this.downloadFullyComplete = true;
                      GC.Collect();
                      GC.WaitForPendingFinalizers();
                      if (GameEngine.Instance.LocalWorldData.AIWorld)
                        this.downloadAIInvasionInfo();
                      this.delayedFactionSave = true;
                    }
                  }
                }
              }
            }
          }
        }
      }
    }

    public void villageNamesCallBack(GetVillageNames_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.loadingErrored = false;
        if (returnData.villageNames != null)
        {
          int num = 0;
          foreach (string villageName in returnData.villageNames)
          {
            if (num < this.villageList.Length)
              this.villageList[num++].villageName = villageName.Length != 0 ? villageName : "Village:" + (num - 1).ToString();
          }
        }
        if (returnData.regionNames != null)
        {
          int num = 0;
          foreach (string regionName in returnData.regionNames)
          {
            if (num < this.regionList.Length)
              this.regionList[num++].areaName = regionName;
          }
        }
        if (returnData.countyNames != null)
        {
          int num = 0;
          foreach (string countyName in returnData.countyNames)
          {
            if (num < this.countyList.Length)
              this.countyList[num++].areaName = countyName;
          }
        }
        if (returnData.provinceNames != null)
        {
          int num = 0;
          foreach (string provinceName in returnData.provinceNames)
          {
            if (num < this.provincesList.Length)
              this.provincesList[num++].areaName = provinceName;
          }
        }
        if (returnData.countryNames != null)
        {
          int num = 0;
          foreach (string countryName in returnData.countryNames)
          {
            if (num < this.countryList.Length)
              this.countryList[num++].areaName = countryName;
          }
        }
        if (returnData.villageChangedNames != null)
          this.changeVillageNames(returnData.villageChangedNames);
        this.storedVillageNamePos = returnData.currentVillageNameChangePos;
        for (int index = 0; index < returnData.worldMapCachedData.Length; ++index)
        {
          if (index < this.villageList.Length && this.villageList[index] != null)
            this.villageList[index].villageInfo = returnData.worldMapCachedData[index];
        }
        this.saveNamesData();
        VillageMap.setServerTime(returnData.currentTime);
        GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
        GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
        GameEngine.Instance.World.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
        GameEngine.Instance.World.setPoints(returnData.currentPoints);
      }
      else
      {
        this.loadingErrored = true;
        this.m_downloadedDataSafely = false;
      }
    }

    public void changeVillageNames(List<NameUpdateListItem> newNames)
    {
      if (newNames == null)
        return;
      foreach (NameUpdateListItem newName in newNames)
      {
        if (newName.areaID < this.villageList.Length && newName.areaID >= 0)
          this.villageList[newName.areaID].villageName = newName.newName;
      }
    }

    private void GetKillStreakDataCallback(GetKillStreakData_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.KillStreakTimer = returnData.killStreakExpiry;
      this.KillStreakCount = returnData.killStreakCount;
      this.KillStreakPoints = returnData.killStreakPoints;
    }

    public void getVillageFactionChangesCallback(GetVillageFactionChanges_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.loadingErrored = false;
        if (returnData.villageUpdateList != null)
          this.processVillageFactionChangesList(returnData.villageUpdateList, returnData.currentVillageChangePos);
        else if (returnData.villageOwnerFactions != null)
          this.processVillageFactionList(returnData.villageOwnerFactions, returnData.currentVillageChangePos);
        if (returnData.factionsList == null)
          return;
        this.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
      }
      else
      {
        this.loadingErrored = true;
        this.m_downloadedDataSafely = false;
      }
    }

    public void getAllVillageOwnerFactionsCallback(GetAllVillageOwnerFactions_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.loadingErrored = false;
        this.processVillageFactionList(returnData.villageOwnerFactions, returnData.currentVillageChangePos);
        if (returnData.factionsList == null)
          return;
        this.m_factionData.Clear();
        this.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
      }
      else
      {
        this.loadingErrored = true;
        this.m_downloadedDataSafely = false;
      }
    }

    public void processVillageFactionList(int[,] ownerList, long newPos)
    {
      int index1 = 0;
      int num = 0;
      if (ownerList.GetUpperBound(1) == 5)
      {
        for (int index2 = 0; index2 < ownerList.Length / 6; ++index2)
        {
          this.villageList[index1].factionID = ownerList[index2, 0];
          this.villageList[index1].userID = ownerList[index2, 1];
          if (ownerList[index2, 2] == 1)
          {
            this.villageList[index1].visible = true;
            ++num;
          }
          else
            this.villageList[index1].visible = false;
          this.villageList[index1].connecter = ownerList[index2, 3];
          this.villageList[index1].special = ownerList[index2, 4];
          this.villageList[index1].villageTerrain = (short) ownerList[index2, 5];
          if (this.villageList[index1].special == 20)
            this.villageList[index1].visible = false;
          this.villageList[index1].rolloverInfo = (WorldMap.VillageRolloverInfo) null;
          ++index1;
        }
      }
      else
      {
        for (int index3 = 0; index3 < ownerList.Length / 6 && index1 < this.villageList.Length; ++index3)
        {
          this.villageList[index1].factionID = ownerList[0, index3];
          this.villageList[index1].userID = ownerList[1, index3];
          if (ownerList[2, index3] == 1)
          {
            this.villageList[index1].visible = true;
            ++num;
          }
          else
            this.villageList[index1].visible = false;
          this.villageList[index1].connecter = ownerList[3, index3];
          this.villageList[index1].special = ownerList[4, index3];
          this.villageList[index1].villageTerrain = (short) ownerList[5, index3];
          if (this.villageList[index1].special == 20)
            this.villageList[index1].visible = false;
          this.villageList[index1].rolloverInfo = (WorldMap.VillageRolloverInfo) null;
          ++index1;
        }
      }
      this.updateUserVassals();
      this.storedVillageFactionsPos = num <= 2 ? -1L : newPos;
      if (ownerList.Length <= 0)
        return;
      this.fixupVisibleParishCapitals();
    }

    public void processVillageFactionChangesList(AreaUpdateListItem[] ownerList, long newPos)
    {
      int num = 0;
      bool flag = false;
      foreach (AreaUpdateListItem owner in ownerList)
      {
        if (owner != null && owner.areaID >= 0)
        {
          if (owner.areaID < this.villageList.Length)
          {
            if ((this.villageList[owner.areaID].userID == RemoteServices.Instance.UserID || owner.newOwnerID == RemoteServices.Instance.UserID) && this.villageList[owner.areaID].userID != owner.newOwnerID)
              flag = true;
            this.villageList[owner.areaID].factionID = owner.newFactionID;
            this.villageList[owner.areaID].userID = owner.newOwnerID;
            this.villageList[owner.areaID].visible = true;
            this.villageList[owner.areaID].connecter = owner.connectorID;
            if (owner.special != -1)
              this.villageList[owner.areaID].special = owner.special;
            if (owner.mapTerrain >= 0)
              this.villageList[owner.areaID].villageTerrain = (short) owner.mapTerrain;
            if (owner.special == 2 || owner.special == -1 || owner.special == 20)
              this.villageList[owner.areaID].visible = false;
            this.villageList[owner.areaID].rolloverInfo = (WorldMap.VillageRolloverInfo) null;
          }
        }
        else
          ++num;
      }
      if ((long) ownerList.Length < newPos - this.storedVillageFactionsPos || num > 1)
        num = 100;
      if (num >= 0)
      {
        this.storedVillageFactionsPos = newPos;
        this.updateUserVassals();
        if (flag)
          this.retrieveUserVillages(false);
      }
      if (ownerList.Length <= 0)
        return;
      this.fixupVisibleParishCapitals();
    }

    public long StoredVillageFactionPos => this.storedVillageFactionsPos;

    public void processFactionsList(List<FactionData> factionsList, long currentFactionChangePos)
    {
      foreach (FactionData factions in factionsList)
        this.m_factionData[factions.factionID] = (object) factions;
      if (GameEngine.Instance.LocalWorldData.AIWorld)
      {
        foreach (FactionData factionData in this.m_factionData)
        {
          if (factionData.factionID >= 1 && factionData.factionID <= 4)
          {
            if (!GameEngine.Instance.LocalWorldData.IsHereticEUAIWorld || factionData.factionID != 4)
              factionData.numMembers = 1;
            factionData.openForApplications = false;
            if (factionData.factionID == 4)
              factionData.houseRank = 10;
            switch (factionData.factionID)
            {
              case 1:
                factionData.flagData = 951615895;
                continue;
              case 2:
                factionData.flagData = 941629576;
                continue;
              case 3:
                factionData.flagData = 946006923;
                continue;
              case 4:
                factionData.flagData = 941809835;
                continue;
              default:
                continue;
            }
          }
        }
      }
      this.inactiveFaction.active = false;
      int num = -1;
      foreach (FactionData factionData in this.m_factionData)
      {
        if (factionData.factionID > num)
          num = factionData.factionID;
      }
      for (int index = 0; index <= num; ++index)
      {
        if (this.m_factionData[index] == null)
          this.m_factionData[index] = (object) this.inactiveFaction;
      }
      this.storedFactionChangesPos = currentFactionChangePos;
    }

    public void getVillageRankTaxTreeCallback(GetVillageRankTaxTree_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      for (int index = 0; index < returnData.villageConnecters.Length; ++index)
        this.villageList[index].connecter = returnData.villageConnecters[index];
      this.updateUserVassals();
      VillageMap.setServerTime(returnData.currentTime);
      GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
      GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
    }

    public void updateUserVassals()
    {
      if (this.m_userVillages == null)
        return;
      foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
        userVillage.vassals.Clear();
      foreach (VillageData village in this.villageList)
      {
        if (village.connecter >= 0)
        {
          foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
          {
            if (userVillage.villageID == village.connecter)
            {
              userVillage.vassals.Add(village.id);
              break;
            }
          }
        }
      }
    }

    public bool isVassal(int yourVillage, int potentialVassalVillage)
    {
      return potentialVassalVillage >= 0 && potentialVassalVillage < this.villageList.Length && this.villageList[potentialVassalVillage].connecter == yourVillage;
    }

    public void breakVassal(int lordVillage, int vassalVillage)
    {
      this.villageList[vassalVillage].connecter = -1;
    }

    public void fixupVisibleParishCapitals()
    {
      foreach (VillageData village in this.villageList)
      {
        if (village.visible && !village.Capital)
        {
          int regionId = (int) village.regionID;
          if (regionId >= 0)
            this.villageList[this.regionList[regionId].capitalVillage].visible = true;
        }
      }
    }

    public void doFullTick(bool registerSession, int mode)
    {
      if ((DateTime.Now - this.lastFullTickCall).TotalSeconds < 10.0)
        return;
      this.lastFullTickCall = DateTime.Now;
      RemoteServices.Instance.FullTick(this.storedVillageFactionsPos, this.storedRegionFactionsPos, this.storedCountyFactionsPos, this.storedProvinceFactionsPos, this.storedCountryFactionsPos, registerSession, this.storedVillageNamePos, this.storedFactionChangesPos, this.lastTraderTime, this.lastPersonTime, this.storedParishFlagsPos, this.storedCountyFlagsPos, this.storedProvinceFlagsPos, this.storedCountryFlagsPos, this.highestDownloadedArmy, mode, WorldMap.fullTickFullMode | mode > 1);
      WorldMap.fullTickFullMode = !WorldMap.fullTickFullMode;
    }

    public void fullTickCallBack(FullTick_ReturnType returnData)
    {
      if (InterfaceMgr.Instance.getCardWindow() != null)
        CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.getCardWindow());
      if (!returnData.Success)
        return;
      if (returnData.villageUpdateList != null)
        this.processVillageFactionChangesList(returnData.villageUpdateList, returnData.currentVillageChangePos);
      else if (returnData.villageOwnerFactions != null)
        this.processVillageFactionList(returnData.villageOwnerFactions, returnData.currentVillageChangePos);
      this.updateRegionFactions(returnData.regionUpdateList, returnData.regionFactions, returnData.currentRegionChangePos);
      this.updateCountyFactions(returnData.countyUpdateList, returnData.countyFactions, returnData.currentCountyChangePos);
      this.updateProvinceFactions(returnData.provinceUpdateList, returnData.provinceFactions, returnData.currentProvinceChangePos);
      this.updateCountryFactions(returnData.countryUpdateList, returnData.countryFactions, returnData.currentCountryChangePos);
      if (returnData.userCapitals != null)
        this.updateUserCapitals(returnData.userCapitals);
      if (returnData.villageChangedNames != null)
        this.changeVillageNames(returnData.villageChangedNames);
      if (returnData.factionsList != null)
        this.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
      if (returnData.parishFlagChanges != null)
        this.updateParishFlags(returnData.parishFlagChanges, returnData.currentParishFlagPos);
      if (returnData.countyFlagChanges != null)
        this.updateCountyFlags(returnData.countyFlagChanges, returnData.currentCountyFlagPos);
      if (returnData.provinceFlagChanges != null)
        this.updateProvinceFlags(returnData.provinceFlagChanges, returnData.currentProvinceFlagPos);
      if (returnData.countryFlagChanges != null)
        this.updateCountryFlags(returnData.countryFlagChanges, returnData.currentCountryFlagPos);
      this.storedVillageNamePos = returnData.currentVillageNameChangePos;
      if (returnData.armyDataReturn != null)
      {
        returnData.armyDataReturn.SetAsSucceeded();
        this.getArmyData(returnData.armyDataReturn);
      }
      this.highestArmySeen = returnData.armyHighestSeen;
      if (returnData.changedTraders != null && returnData.changedTraders.Count > 0)
      {
        this.importOrphanedTraders(returnData.changedTraders, returnData.currentTime, -2);
        this.lastTraderTime = returnData.currentTime;
      }
      if (returnData.people != null && returnData.people.Count > 0)
      {
        this.importOrphanedPeople(returnData.people, returnData.currentTime, -2);
        this.lastPersonTime = returnData.currentTime;
      }
      this.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
      this.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
      this.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
      this.setPoints(returnData.currentPoints);
      this.setNumMadeCaptains(returnData.numMadeCaptains);
      this.m_mostAge4Villages = returnData.mostAge4Villages;
      GameEngine.Instance.setServerDownTime(returnData.serverDowntime);
      this.setTickets(0, returnData.wheel_Treasure1Tickets);
      this.setTickets(1, returnData.wheel_Treasure2Tickets);
      this.setTickets(2, returnData.wheel_Treasure3Tickets);
      this.setTickets(3, returnData.wheel_Treasure4Tickets);
      this.setTickets(4, returnData.wheel_Treasure5Tickets);
      if (returnData.m_cardData != null)
        GameEngine.Instance.cardsManager.UserCardData = returnData.m_cardData;
      if (returnData.achievements != null)
        InterfaceMgr.Instance.processAchievements(returnData.achievements);
      if (returnData.m_newQuestsData != null)
        this.setNewQuestData(returnData.m_newQuestsData);
      if (!this.isIslandWorld())
      {
        WorldMap.TreasureCastle_AttackGap = returnData.m_treasureCastle_AttackGap;
      }
      else
      {
        this.SpecialSeaConditionsData = returnData.m_treasureCastle_AttackGap;
        WorldMap.TreasureCastle_AttackGap = 86400;
      }
    }

    public void getAreaFactionChangesCallback(GetAreaFactionChanges_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.loadingErrored = false;
        this.updateRegionFactions(returnData.regionUpdateList, returnData.regionFactions, returnData.currentRegionChangePos);
        this.updateCountyFactions(returnData.countyUpdateList, returnData.countyFactions, returnData.currentCountyChangePos);
        this.updateProvinceFactions(returnData.provinceUpdateList, returnData.provinceFactions, returnData.currentProvinceChangePos);
        this.updateCountryFactions(returnData.countryUpdateList, returnData.countryFactions, returnData.currentCountryChangePos);
        this.updateParishFlags(returnData.parishFlagChanges, returnData.currentParishFlagPos);
        this.updateCountyFlags(returnData.countyFlagChanges, returnData.currentCountyFlagPos);
        this.updateProvinceFlags(returnData.provinceFlagChanges, returnData.currentProvinceFlagPos);
        this.updateCountryFlags(returnData.countryFlagChanges, returnData.currentCountryFlagPos);
      }
      else
      {
        this.loadingErrored = true;
        this.m_downloadedDataSafely = false;
      }
    }

    private void updateParishFlags(
      List<CapitalFlagChangeInfo> parishFlagChanges,
      long parishFlagChangePos)
    {
      this.storedParishFlagsPos = parishFlagChangePos;
      foreach (CapitalFlagChangeInfo parishFlagChange in parishFlagChanges)
      {
        if (parishFlagChange.areaID >= 0)
        {
          int areaId = parishFlagChange.areaID;
          if (areaId < this.regionList.Length)
          {
            int capitalVillage = this.regionList[areaId].capitalVillage;
            if (capitalVillage >= 0)
              this.villageList[capitalVillage].numFlags = (short) parishFlagChange.numFlags;
          }
        }
      }
    }

    private void updateCountyFlags(
      List<CapitalFlagChangeInfo> countyFlagChanges,
      long countyFlagChangePos)
    {
      this.storedCountyFlagsPos = countyFlagChangePos;
      foreach (CapitalFlagChangeInfo countyFlagChange in countyFlagChanges)
      {
        if (countyFlagChange.areaID >= 0)
        {
          int areaId = countyFlagChange.areaID;
          if (areaId < this.countyList.Length)
          {
            int capitalVillage = this.countyList[areaId].capitalVillage;
            if (capitalVillage >= 0)
              this.villageList[capitalVillage].numFlags = (short) countyFlagChange.numFlags;
          }
        }
      }
    }

    private void updateProvinceFlags(
      List<CapitalFlagChangeInfo> provinceFlagChanges,
      long provinceFlagChangePos)
    {
      this.storedProvinceFlagsPos = provinceFlagChangePos;
      foreach (CapitalFlagChangeInfo provinceFlagChange in provinceFlagChanges)
      {
        if (provinceFlagChange.areaID >= 0)
        {
          int areaId = provinceFlagChange.areaID;
          if (areaId < this.provincesList.Length)
          {
            int capitalVillage = this.provincesList[areaId].capitalVillage;
            if (capitalVillage >= 0)
              this.villageList[capitalVillage].numFlags = (short) provinceFlagChange.numFlags;
          }
        }
      }
    }

    private void updateCountryFlags(
      List<CapitalFlagChangeInfo> countryFlagChanges,
      long countryFlagChangePos)
    {
      this.storedCountryFlagsPos = countryFlagChangePos;
      foreach (CapitalFlagChangeInfo countryFlagChange in countryFlagChanges)
      {
        if (countryFlagChange.areaID >= 0)
        {
          int areaId = countryFlagChange.areaID;
          if (areaId < this.countryList.Length)
          {
            int capitalVillage = this.countryList[areaId].capitalVillage;
            if (capitalVillage >= 0)
              this.villageList[capitalVillage].numFlags = (short) countryFlagChange.numFlags;
          }
        }
      }
    }

    private void updateRegionFactions(
      AreaUpdateListItem[] regionUpdateList,
      int[,] regionFactions,
      long currentRegionChangePos)
    {
      if (regionUpdateList != null)
      {
        foreach (AreaUpdateListItem regionUpdate in regionUpdateList)
        {
          if (regionUpdate != null && regionUpdate.areaID < this.regionList.Length)
          {
            this.regionList[regionUpdate.areaID].factionID = regionUpdate.newFactionID;
            this.regionList[regionUpdate.areaID].plague = regionUpdate.special;
          }
        }
      }
      else if (regionFactions != null)
      {
        int index1 = 0;
        for (int index2 = 0; index2 < regionFactions.Length / 3; ++index2)
        {
          if (index1 < this.regionList.Length)
          {
            this.regionList[index1].factionID = regionFactions[index2, 0];
            this.regionList[index1].userID = regionFactions[index2, 1];
            this.regionList[index1++].plague = regionFactions[index2, 2];
          }
        }
      }
      this.storedRegionFactionsPos = currentRegionChangePos;
    }

    private void updateCountyFactions(
      AreaUpdateListItem[] countyUpdateList,
      int[,] countyFactions,
      long currentCountyChangePos)
    {
      if (countyUpdateList != null)
      {
        foreach (AreaUpdateListItem countyUpdate in countyUpdateList)
        {
          if (countyUpdate != null && countyUpdate.areaID < this.countyList.Length)
            this.countyList[countyUpdate.areaID].factionID = countyUpdate.newFactionID;
        }
      }
      else if (countyFactions != null)
      {
        int index1 = 0;
        for (int index2 = 0; index2 < countyFactions.Length / 2; ++index2)
        {
          if (index1 < this.countyList.Length)
          {
            this.countyList[index1].factionID = countyFactions[index2, 0];
            this.countyList[index1++].userID = countyFactions[index2, 1];
          }
        }
      }
      this.storedCountyFactionsPos = currentCountyChangePos;
    }

    private void updateProvinceFactions(
      AreaUpdateListItem[] provinceUpdateList,
      int[,] provinceFactions,
      long currentProvinceChangePos)
    {
      if (provinceUpdateList != null)
      {
        foreach (AreaUpdateListItem provinceUpdate in provinceUpdateList)
        {
          if (provinceUpdate != null && provinceUpdate.areaID < this.provincesList.Length)
            this.provincesList[provinceUpdate.areaID].factionID = provinceUpdate.newFactionID;
        }
      }
      else if (provinceFactions != null)
      {
        int index1 = 0;
        for (int index2 = 0; index2 < provinceFactions.Length / 2; ++index2)
        {
          if (index1 < this.provincesList.Length)
          {
            this.provincesList[index1].factionID = provinceFactions[index2, 0];
            this.provincesList[index1++].userID = provinceFactions[index2, 1];
          }
        }
      }
      this.storedProvinceFactionsPos = currentProvinceChangePos;
    }

    private void updateCountryFactions(
      AreaUpdateListItem[] countryUpdateList,
      int[,] countryFactions,
      long currentCountryChangePos)
    {
      if (countryUpdateList != null)
      {
        foreach (AreaUpdateListItem countryUpdate in countryUpdateList)
        {
          if (countryUpdate != null && countryUpdate.areaID < this.countryList.Length)
            this.countryList[countryUpdate.areaID].factionID = countryUpdate.newFactionID;
        }
      }
      else if (countryFactions != null)
      {
        int index1 = 0;
        for (int index2 = 0; index2 < countryFactions.Length / 2; ++index2)
        {
          if (index1 < this.countryList.Length)
          {
            this.countryList[index1].factionID = countryFactions[index2, 0];
            this.countryList[index1++].userID = countryFactions[index2, 1];
          }
        }
      }
      this.storedCountryFactionsPos = currentCountryChangePos;
    }

    public void saveFactionData()
    {
      if (!this.m_downloadedDataSafely)
        return;
      string settingsPath = GameEngine.getSettingsPath(true);
      try
      {
        new FileInfo(settingsPath + "\\VillageData" + this.m_globalWorldID.ToString() + ".dat").IsReadOnly = false;
      }
      catch (Exception ex)
      {
        UniversalDebugLog.Log("Exception in saveFactionData " + ex.Message);
      }
      try
      {
        FileStream output = new FileStream(settingsPath + "\\VillageData" + this.m_globalWorldID.ToString() + ".dat", FileMode.Create);
        BinaryWriter binaryWriter = new BinaryWriter((Stream) output);
        byte[] byteArray = RemoteServices.Instance.WorldGUID.ToByteArray();
        int num1 = 10;
        binaryWriter.Write(num1);
        binaryWriter.Write(byteArray, 0, 16);
        binaryWriter.Write(this.storedVillageFactionsPos);
        for (int index = 0; index < this.villageList.Length; ++index)
        {
          binaryWriter.Write(this.villageList[index].factionID);
          binaryWriter.Write(this.villageList[index].userID);
          binaryWriter.Write(this.villageList[index].connecter);
          binaryWriter.Write(this.villageList[index].special);
          binaryWriter.Write((int) this.villageList[index].villageTerrain);
          binaryWriter.Write((int) this.villageList[index].numFlags);
        }
        binaryWriter.Write(this.storedRegionFactionsPos);
        binaryWriter.Write(this.storedParishFlagsPos);
        binaryWriter.Write(this.storedCountyFlagsPos);
        binaryWriter.Write(this.storedProvinceFlagsPos);
        binaryWriter.Write(this.storedCountryFlagsPos);
        for (int index = 0; index < this.regionList.Length; ++index)
        {
          binaryWriter.Write(this.regionList[index].factionID);
          binaryWriter.Write(this.regionList[index].userID);
          binaryWriter.Write(this.regionList[index].plague);
        }
        binaryWriter.Write(this.storedCountyFactionsPos);
        for (int index = 0; index < this.countyList.Length; ++index)
        {
          binaryWriter.Write(this.countyList[index].factionID);
          binaryWriter.Write(this.countyList[index].userID);
        }
        binaryWriter.Write(this.storedProvinceFactionsPos);
        for (int index = 0; index < this.provincesList.Length; ++index)
        {
          binaryWriter.Write(this.provincesList[index].factionID);
          binaryWriter.Write(this.provincesList[index].userID);
        }
        binaryWriter.Write(this.storedCountryFactionsPos);
        for (int index = 0; index < this.countryList.Length; ++index)
        {
          binaryWriter.Write(this.countryList[index].factionID);
          binaryWriter.Write(this.countryList[index].userID);
        }
        for (int index = 0; index < this.villageList.Length; ++index)
          binaryWriter.Write(this.villageList[index].visible);
        binaryWriter.Write(this.storedFactionChangesPos);
        int num2 = 0;
        foreach (FactionData factionData in this.m_factionData)
          ++num2;
        binaryWriter.Write(num2);
        foreach (FactionData factionData in this.m_factionData)
        {
          binaryWriter.Write(factionData.factionID);
          binaryWriter.Write(factionData.active);
          binaryWriter.Write(factionData.factionName);
          binaryWriter.Write(factionData.factionNameAbrv);
          binaryWriter.Write(factionData.houseID);
          binaryWriter.Write(factionData.numMembers);
          binaryWriter.Write(factionData.points);
          binaryWriter.Write(factionData.flagData);
          binaryWriter.Write(factionData.openForApplications);
        }
        binaryWriter.Close();
        output.Close();
      }
      catch (Exception ex)
      {
        int num = (int) MyMessageBox.Show(SK.Text("WorldMapLoader_DataSaveError_Text", "A problem occurred saving 'VillageData.data'") + "\n\n" + ex.ToString(), SK.Text("WorldMapLoader_DataSaveError_Header", "Data Save Error"));
      }
    }

    public bool loadFactionData()
    {
      string settingsPath = GameEngine.getSettingsPath(false);
      FileStream input = (FileStream) null;
      BinaryReader binaryReader = (BinaryReader) null;
      try
      {
        input = new FileStream(settingsPath + "\\VillageData" + this.m_globalWorldID.ToString() + ".dat", FileMode.Open, FileAccess.Read);
        binaryReader = new BinaryReader((Stream) input);
        if (binaryReader.ReadInt32() != 10)
        {
          binaryReader.Close();
          input.Close();
          return false;
        }
        byte[] numArray = new byte[16];
        if (RemoteServices.Instance.WorldGUID.CompareTo(new Guid(binaryReader.ReadBytes(16))) != 0)
        {
          binaryReader.Close();
          input.Close();
          return false;
        }
        this.storedVillageFactionsPos = binaryReader.ReadInt64();
        for (int index = 0; index < this.villageList.Length; ++index)
        {
          this.villageList[index].factionID = binaryReader.ReadInt32();
          this.villageList[index].userID = binaryReader.ReadInt32();
          this.villageList[index].connecter = binaryReader.ReadInt32();
          this.villageList[index].special = binaryReader.ReadInt32();
          this.villageList[index].villageTerrain = (short) binaryReader.ReadInt32();
          this.villageList[index].numFlags = (short) binaryReader.ReadInt32();
        }
        this.storedRegionFactionsPos = binaryReader.ReadInt64();
        this.storedParishFlagsPos = binaryReader.ReadInt64();
        this.storedCountyFlagsPos = binaryReader.ReadInt64();
        this.storedProvinceFlagsPos = binaryReader.ReadInt64();
        this.storedCountryFlagsPos = binaryReader.ReadInt64();
        for (int index = 0; index < this.regionList.Length; ++index)
        {
          this.regionList[index].factionID = binaryReader.ReadInt32();
          this.regionList[index].userID = binaryReader.ReadInt32();
          this.regionList[index].plague = binaryReader.ReadInt32();
        }
        this.storedCountyFactionsPos = binaryReader.ReadInt64();
        for (int index = 0; index < this.countyList.Length; ++index)
        {
          this.countyList[index].factionID = binaryReader.ReadInt32();
          this.countyList[index].userID = binaryReader.ReadInt32();
        }
        this.storedProvinceFactionsPos = binaryReader.ReadInt64();
        for (int index = 0; index < this.provincesList.Length; ++index)
        {
          this.provincesList[index].factionID = binaryReader.ReadInt32();
          this.provincesList[index].userID = binaryReader.ReadInt32();
        }
        this.storedCountryFactionsPos = binaryReader.ReadInt64();
        for (int index = 0; index < this.countryList.Length; ++index)
        {
          this.countryList[index].factionID = binaryReader.ReadInt32();
          this.countryList[index].userID = binaryReader.ReadInt32();
        }
        for (int index = 0; index < this.villageList.Length; ++index)
          this.villageList[index].visible = binaryReader.ReadBoolean();
        this.storedFactionChangesPos = binaryReader.ReadInt64();
        int num = binaryReader.ReadInt32();
        for (int index = 0; index < num; ++index)
        {
          FactionData factionData = new FactionData()
          {
            factionID = binaryReader.ReadInt32(),
            active = binaryReader.ReadBoolean(),
            factionName = binaryReader.ReadString(),
            factionNameAbrv = binaryReader.ReadString(),
            houseID = binaryReader.ReadInt32(),
            numMembers = binaryReader.ReadInt32(),
            points = binaryReader.ReadInt32(),
            flagData = binaryReader.ReadInt32(),
            openForApplications = binaryReader.ReadBoolean()
          };
          this.m_factionData[factionData.factionID] = (object) factionData;
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
        return false;
      }
      return true;
    }

    public void saveNamesData()
    {
      if (this.MapEditing)
        return;
      string settingsPath = GameEngine.getSettingsPath(true);
      try
      {
        new FileInfo(settingsPath + "\\NameData" + this.m_globalWorldID.ToString() + ".dat").IsReadOnly = false;
      }
      catch (Exception ex)
      {
      }
      try
      {
        FileStream output = new FileStream(settingsPath + "\\NameData" + this.m_globalWorldID.ToString() + ".dat", FileMode.Create);
        BinaryWriter binaryWriter = new BinaryWriter((Stream) output);
        byte[] byteArray = RemoteServices.Instance.WorldGUID.ToByteArray();
        binaryWriter.Write(byteArray, 0, 16);
        binaryWriter.Write(this.storedVillageNamePos);
        int num = 0;
        for (int index = 0; index < this.villageList.Length; ++index)
        {
          binaryWriter.Write(this.villageList[index].m_villageName);
          num ^= this.villageList[index].m_villageName.GetHashCode();
        }
        for (int index = 0; index < this.regionList.Length; ++index)
        {
          binaryWriter.Write(this.regionList[index].areaName);
          num ^= this.regionList[index].areaName.GetHashCode();
        }
        for (int index = 0; index < this.countyList.Length; ++index)
        {
          binaryWriter.Write(this.countyList[index].areaName);
          num ^= this.countyList[index].areaName.GetHashCode();
        }
        for (int index = 0; index < this.provincesList.Length; ++index)
        {
          binaryWriter.Write(this.provincesList[index].areaName);
          num ^= this.provincesList[index].areaName.GetHashCode();
        }
        for (int index = 0; index < this.countryList.Length; ++index)
        {
          binaryWriter.Write(this.countryList[index].areaName);
          num ^= this.countryList[index].areaName.GetHashCode();
        }
        binaryWriter.Write(num);
        binaryWriter.Close();
        output.Close();
      }
      catch (Exception ex)
      {
        int num = (int) MyMessageBox.Show(SK.Text("WorldMapLoader_NameSaveError_Text", "A problem occurred saving 'NameData.data'") + "\n\n" + ex.ToString(), SK.Text("WorldMapLoader_DataSaveError_Header", "Data Save Error"));
      }
    }

    public bool loadNamesData()
    {
      string settingsPath = GameEngine.getSettingsPath(false);
      FileStream input = (FileStream) null;
      BinaryReader binaryReader = (BinaryReader) null;
      try
      {
        input = new FileStream(settingsPath + "\\NameData" + this.m_globalWorldID.ToString() + ".dat", FileMode.Open, FileAccess.Read);
        binaryReader = new BinaryReader((Stream) input);
        byte[] numArray = new byte[16];
        if (RemoteServices.Instance.WorldGUID.CompareTo(new Guid(binaryReader.ReadBytes(16))) != 0)
        {
          binaryReader.Close();
          input.Close();
          return false;
        }
        bool flag = false;
        this.storedVillageNamePos = binaryReader.ReadInt64();
        int num1 = 0;
        for (int index = 0; index < this.villageList.Length; ++index)
        {
          this.villageList[index].villageName = binaryReader.ReadString();
          num1 ^= this.villageList[index].m_villageName.GetHashCode();
        }
        for (int index = 0; index < this.regionList.Length; ++index)
        {
          this.regionList[index].areaName = binaryReader.ReadString();
          num1 ^= this.regionList[index].areaName.GetHashCode();
          if (this.regionList[index].areaName.Length == 0)
            flag = true;
        }
        for (int index = 0; index < this.countyList.Length; ++index)
        {
          this.countyList[index].areaName = binaryReader.ReadString();
          num1 ^= this.countyList[index].areaName.GetHashCode();
        }
        for (int index = 0; index < this.provincesList.Length; ++index)
        {
          this.provincesList[index].areaName = binaryReader.ReadString();
          num1 ^= this.provincesList[index].areaName.GetHashCode();
        }
        for (int index = 0; index < this.countryList.Length; ++index)
        {
          this.countryList[index].areaName = binaryReader.ReadString();
          num1 ^= this.countryList[index].areaName.GetHashCode();
        }
        int num2 = binaryReader.ReadInt32();
        binaryReader.Close();
        input.Close();
        if (num2 != num1)
          return false;
        if (flag)
          return false;
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
        return false;
      }
      return true;
    }

    private void loadInitParishText()
    {
    }

    public void resetParishTextReadID()
    {
      foreach (WorldMap.ParishChatData parishChatData in this.m_parishChatLog)
      {
        parishChatData.m_readIDs[0] = -1L;
        parishChatData.m_readIDs[1] = -1L;
        parishChatData.m_readIDs[2] = -1L;
        parishChatData.m_readIDs[3] = -1L;
        parishChatData.m_readIDs[4] = -1L;
        parishChatData.m_readIDs[5] = -1L;
      }
    }

    public DateTime getParishChatNewestPostTime(int parishID, DateTime allowedMinTime)
    {
      DateTime minValue = DateTime.MinValue;
      DateTime chatNewestPostTime = this.m_parishChatLog[parishID] == null ? VillageMap.getCurrentServerTime().AddDays(-30.0) : ((WorldMap.ParishChatData) this.m_parishChatLog[parishID]).m_newestPost;
      if (chatNewestPostTime < allowedMinTime)
        chatNewestPostTime = allowedMinTime;
      if (chatNewestPostTime == DateTime.MaxValue && RemoteServices.Instance.Admin)
        chatNewestPostTime = DateTime.Now.AddDays(-30.0);
      return chatNewestPostTime;
    }

    public List<Chat_TextEntry> getParishChat(int parishID, int subForum, DateTime minTime)
    {
      if (minTime == DateTime.MaxValue && RemoteServices.Instance.Admin)
        minTime = DateTime.MinValue;
      if (this.m_parishChatLog[parishID] == null)
        return new List<Chat_TextEntry>();
      List<Chat_TextEntry> chatPage = ((WorldMap.ParishChatData) this.m_parishChatLog[parishID]).getChatPage(subForum);
      if (chatPage == null)
        return new List<Chat_TextEntry>();
      List<Chat_TextEntry> parishChat1 = new List<Chat_TextEntry>();
      foreach (Chat_TextEntry chatTextEntry in chatPage)
      {
        if (chatTextEntry.postedTime >= minTime)
          parishChat1.Add(chatTextEntry);
      }
      if (parishChat1.Count > 100)
      {
        List<Chat_TextEntry> parishChat2 = new List<Chat_TextEntry>();
        for (int index = parishChat1.Count - 100; index < parishChat1.Count; ++index)
          parishChat2.Add(parishChat1[index]);
        parishChat2.Sort((IComparer<Chat_TextEntry>) this.parishChatComparer);
        return parishChat2;
      }
      parishChat1.Sort((IComparer<Chat_TextEntry>) this.parishChatComparer);
      return parishChat1;
    }

    public List<Chat_TextEntry> addParishChat(int parishID, List<Chat_TextEntry> newText)
    {
      if (newText == null || newText.Count == 0)
        return (List<Chat_TextEntry>) null;
      if (this.m_parishChatLog[parishID] == null)
      {
        WorldMap.ParishChatData parishChatData = new WorldMap.ParishChatData();
        parishChatData.init();
        this.m_parishChatLog[parishID] = (object) parishChatData;
      }
      WorldMap.ParishChatData parishChatData1 = (WorldMap.ParishChatData) this.m_parishChatLog[parishID];
      List<Chat_TextEntry> chatTextEntryList = new List<Chat_TextEntry>();
      foreach (Chat_TextEntry textEntry in newText)
      {
        if (parishChatData1.addEntry(textEntry))
          chatTextEntryList.Add(textEntry);
      }
      return chatTextEntryList;
    }

    public int[] setReadIDs(int parishID, long[] readIDs)
    {
      int[] numArray = new int[6];
      if (this.m_parishChatLog[parishID] == null)
        return numArray;
      WorldMap.ParishChatData parishChatData = (WorldMap.ParishChatData) this.m_parishChatLog[parishID];
      parishChatData.setReadIDs(readIDs);
      for (int pageID = 0; pageID < 6; ++pageID)
      {
        numArray[pageID] = 0;
        bool flag = false;
        long readId = parishChatData.getReadID(pageID);
        foreach (Chat_TextEntry chatTextEntry in parishChatData.m_pages[pageID])
        {
          if (chatTextEntry.textID > readId)
            flag = true;
          if (flag)
            ++numArray[pageID];
        }
      }
      return numArray;
    }

    public long getHighestReadID(int parishID, int pageID)
    {
      if (this.m_parishChatLog[parishID] == null)
        return -1;
      List<Chat_TextEntry> page = ((WorldMap.ParishChatData) this.m_parishChatLog[parishID]).m_pages[pageID];
      if (page.Count <= 0)
        return -1;
      long highestReadId = -1;
      foreach (Chat_TextEntry chatTextEntry in page)
      {
        if (chatTextEntry.textID > highestReadId)
          highestReadId = chatTextEntry.textID;
      }
      return highestReadId;
    }

    public void clearParishChat()
    {
      this.m_parishChatLog = new SparseArray();
      this.m_parishWallDonateDetails = new SparseArray();
    }

    public void registerParishWallDonateDetails(ParishWallDetailInfo_ReturnType returnData)
    {
      this.m_parishWallDonateDetails[((long) returnData.parishCapitalID << 32) + (long) returnData.userID] = (object) new WorldMap.ParishWallDonateInfo()
      {
        returnData = returnData,
        lastUpdateTime = DateTime.Now
      };
    }

    public void flushParishWallDonation(int parishCapitalID, int userID)
    {
      this.m_parishWallDonateDetails[((long) parishCapitalID << 32) + (long) userID] = (object) null;
    }

    public ParishWallDetailInfo_ReturnType getParishWallDonateDetails(
      int parishCapitalID,
      int userID)
    {
      long index = ((long) parishCapitalID << 32) + (long) userID;
      if (this.m_parishWallDonateDetails[index] != null)
      {
        WorldMap.ParishWallDonateInfo wallDonateDetail = (WorldMap.ParishWallDonateInfo) this.m_parishWallDonateDetails[index];
        if ((DateTime.Now - wallDonateDetail.lastUpdateTime).TotalMinutes < 2.0)
          return wallDonateDetail.returnData;
      }
      return (ParishWallDetailInfo_ReturnType) null;
    }

    public List<int> getAchievementsToTest(ref List<AchievementData> achievementData)
    {
      achievementData = new List<AchievementData>();
      List<int> achievementsToTest = new List<int>();
      List<int> userAchievements = RemoteServices.Instance.UserAchievements;
      if (userAchievements != null)
      {
        double currentGold = this.getCurrentGold();
        if (currentGold >= 20000.0)
        {
          if (!userAchievements.Contains(100))
            achievementsToTest.Add(100);
          if (currentGold >= 200000.0)
          {
            if (!userAchievements.Contains(268435556))
              achievementsToTest.Add(268435556);
            if (currentGold >= 1000000.0)
            {
              if (!userAchievements.Contains(536871012))
                achievementsToTest.Add(536871012);
              if (currentGold >= 5000000.0)
              {
                if (!userAchievements.Contains(1073741924))
                  achievementsToTest.Add(1073741924);
                if (currentGold >= 10000000.0)
                {
                  if (!userAchievements.Contains(1342177380))
                    achievementsToTest.Add(1342177380);
                  if (currentGold >= 20000000.0)
                  {
                    if (!userAchievements.Contains(1610612836))
                      achievementsToTest.Add(1610612836);
                    if (currentGold >= 50000000.0 && !userAchievements.Contains(1879048292))
                      achievementsToTest.Add(1879048292);
                  }
                }
              }
            }
          }
        }
        int num1 = this.numUserParishes();
        if (num1 >= 1)
        {
          if (!userAchievements.Contains(385))
            achievementsToTest.Add(385);
          if (num1 >= 2)
          {
            if (!userAchievements.Contains(268435841))
              achievementsToTest.Add(268435841);
            if (num1 >= 3)
            {
              if (!userAchievements.Contains(536871297))
                achievementsToTest.Add(536871297);
              if (num1 >= 4 && !userAchievements.Contains(1073742209))
                achievementsToTest.Add(1073742209);
            }
          }
        }
        int num2 = this.numUserCounties();
        if (num2 >= 1)
        {
          if (!userAchievements.Contains(386))
            achievementsToTest.Add(386);
          if (num2 >= 2)
          {
            if (!userAchievements.Contains(268435842))
              achievementsToTest.Add(268435842);
            if (num2 >= 3)
            {
              if (!userAchievements.Contains(536871298))
                achievementsToTest.Add(536871298);
              if (num2 >= 4 && !userAchievements.Contains(1073742210))
                achievementsToTest.Add(1073742210);
            }
          }
        }
        int num3 = this.numUserProvinces();
        if (num3 >= 1)
        {
          if (!userAchievements.Contains(387))
            achievementsToTest.Add(387);
          if (num3 >= 2)
          {
            if (!userAchievements.Contains(268435843))
              achievementsToTest.Add(268435843);
            if (num3 >= 3)
            {
              if (!userAchievements.Contains(536871299))
                achievementsToTest.Add(536871299);
              if (num3 >= 4 && !userAchievements.Contains(1073742211))
                achievementsToTest.Add(1073742211);
            }
          }
        }
        int num4 = this.numUserCountries();
        if (num4 >= 1)
        {
          if (!userAchievements.Contains(388))
            achievementsToTest.Add(388);
          if (num4 >= 2)
          {
            if (!userAchievements.Contains(268435844))
              achievementsToTest.Add(268435844);
            if (num4 >= 3)
            {
              if (!userAchievements.Contains(536871300))
                achievementsToTest.Add(536871300);
              if (num4 >= 4 && !userAchievements.Contains(1073742212))
                achievementsToTest.Add(1073742212);
            }
          }
        }
        int num5 = 0;
        if (this.UserResearchData != null)
          num5 = this.UserResearchData.numBranchesComplete(GameEngine.Instance.LocalWorldData);
        if (num5 >= 1)
        {
          if (!userAchievements.Contains(225))
            achievementsToTest.Add(225);
          if (num5 >= 2)
          {
            if (!userAchievements.Contains(268435681))
              achievementsToTest.Add(268435681);
            if (num5 >= 3)
            {
              if (!userAchievements.Contains(536871137))
                achievementsToTest.Add(536871137);
              if (num5 >= 4 && !userAchievements.Contains(1073742049))
                achievementsToTest.Add(1073742049);
            }
          }
        }
        FactionData yourFaction = this.YourFaction;
        if (yourFaction != null)
        {
          int houseId = yourFaction.houseID;
          if (houseId > 0)
          {
            List<int> intList1 = new List<int>();
            int num6 = 0;
            foreach (WorldMap.WorldPointList county in this.countyList)
            {
              if (this.getHouse(county.factionID) == houseId)
              {
                ++num6;
                intList1.Add(county.capitalVillage);
              }
            }
            List<int> intList2 = new List<int>();
            int num7 = 0;
            foreach (WorldMap.WorldPointList provinces in this.provincesList)
            {
              if (this.getHouse(provinces.factionID) == houseId)
              {
                ++num7;
                intList2.Add(provinces.capitalVillage);
              }
            }
            List<int> intList3 = new List<int>();
            int num8 = 0;
            foreach (WorldMap.WorldPointList country in this.countryList)
            {
              if (this.getHouse(country.factionID) == houseId)
              {
                ++num8;
                intList3.Add(country.capitalVillage);
              }
            }
            if (num6 >= 1 && !userAchievements.Contains(194))
            {
              achievementsToTest.Add(194);
              foreach (int num9 in intList1)
                achievementData.Add(new AchievementData()
                {
                  data = num9,
                  achievement = 194
                });
            }
            if (num7 >= 1 && !userAchievements.Contains(268435650))
            {
              achievementsToTest.Add(268435650);
              foreach (int num10 in intList2)
                achievementData.Add(new AchievementData()
                {
                  data = num10,
                  achievement = 268435650
                });
            }
            if (num8 >= 1)
            {
              if (!userAchievements.Contains(536871106))
              {
                achievementsToTest.Add(536871106);
                foreach (int num11 in intList3)
                  achievementData.Add(new AchievementData()
                  {
                    data = num11,
                    achievement = 536871106
                  });
              }
              if (num8 > 1 && !userAchievements.Contains(1073742018))
              {
                achievementsToTest.Add(1073742018);
                foreach (int num12 in intList3)
                  achievementData.Add(new AchievementData()
                  {
                    data = num12,
                    achievement = 1073742018
                  });
              }
            }
          }
          VillageMap.getCurrentServerTime();
          if (this.FactionMembers != null)
          {
            foreach (FactionMemberData factionMember in this.FactionMembers)
            {
              if (factionMember.userID == RemoteServices.Instance.UserID)
              {
                TimeSpan timeSpan = VillageMap.getCurrentServerTime() - factionMember.dateJoined;
                if (timeSpan.TotalDays >= 14.0)
                {
                  if (!userAchievements.Contains(195))
                    achievementsToTest.Add(195);
                  if (timeSpan.TotalDays >= 70.0)
                  {
                    if (!userAchievements.Contains(268435651))
                      achievementsToTest.Add(268435651);
                    if (timeSpan.TotalDays >= 182.0)
                    {
                      if (!userAchievements.Contains(536871107))
                        achievementsToTest.Add(536871107);
                      if (timeSpan.TotalDays >= 364.0 && !userAchievements.Contains(1073742019))
                      {
                        achievementsToTest.Add(1073742019);
                        break;
                      }
                      break;
                    }
                    break;
                  }
                  break;
                }
                break;
              }
            }
          }
        }
      }
      return achievementsToTest;
    }

    public int getCurrentFactionDuration()
    {
      if (this.FactionMembers != null)
      {
        foreach (FactionMemberData factionMember in this.FactionMembers)
        {
          if (factionMember.userID == RemoteServices.Instance.UserID)
            return (int) (VillageMap.getCurrentServerTime() - factionMember.dateJoined).TotalDays / 7;
        }
      }
      return 0;
    }

    public void runClientAchievementTests()
    {
      List<AchievementData> achievementData = (List<AchievementData>) null;
      List<int> achievementsToTest = this.getAchievementsToTest(ref achievementData);
      if (achievementsToTest == null || achievementsToTest.Count <= 0)
        return;
      this.testAchievements(achievementsToTest, achievementData, false);
    }

    public void testAchievements(
      List<int> achievementToTest,
      List<AchievementData> achievementData,
      bool onLoading)
    {
      if (achievementToTest == null || achievementToTest.Count <= 0)
        return;
      if (this.inTestAchievements)
      {
        int num = 30;
        if (!onLoading)
          num = 60;
        if ((DateTime.Now - this.lastTestAchievements).TotalSeconds > (double) num)
          this.inTestAchievements = false;
      }
      if (this.inTestAchievements)
        return;
      this.inTestAchievements = true;
      this.lastTestAchievements = DateTime.Now;
      RemoteServices.Instance.set_TestAchievements_UserCallBack(new RemoteServices.TestAchievements_UserCallBack(this.testAchievementsCallback));
      RemoteServices.Instance.TestAchievements(achievementToTest, achievementData);
    }

    public void testAchievementsCallback(TestAchievements_ReturnType returnData)
    {
      this.inTestAchievements = false;
      if (returnData.Success)
      {
        this.loadingErrored = false;
        if (returnData.achievements == null)
          return;
        InterfaceMgr.Instance.processAchievements(returnData.achievements);
      }
      else
        this.loadingErrored = true;
    }

    public bool isHeretic()
    {
      if (GameEngine.Instance.LocalWorldData.IsHereticEUAIWorld)
      {
        FactionData yourFaction = this.YourFaction;
        if (yourFaction != null && yourFaction.factionID == 4)
          return true;
      }
      return false;
    }

    public void getTraderData()
    {
      this.clearTraderArray(-1);
      this.lastTraderTime = DateTime.Now.AddYears(-5);
      RemoteServices.Instance.set_GetUserTraders_UserCallBack(new RemoteServices.GetUserTraders_UserCallBack(this.getUserTradersCallback));
      RemoteServices.Instance.GetUserTraders();
    }

    public void getUserTradersCallback(GetUserTraders_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.loadingErrored = false;
        this.importOrphanedTraders(returnData.traders, returnData.currentTime, -2);
      }
      else
        this.loadingErrored = true;
    }

    public void getActiveTraders()
    {
      RemoteServices.Instance.set_GetActiveTraders_UserCallBack(new RemoteServices.GetActiveTraders_UserCallBack(this.getActiveTradersCallback));
      RemoteServices.Instance.GetActiveTraders(this.lastTraderTime);
    }

    public void getActiveTradersCallback(GetActiveTraders_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.loadingErrored = false;
        this.importOrphanedTraders(returnData.traders, returnData.currentTime, -2);
        this.lastTraderTime = returnData.currentTime;
      }
      else
        this.loadingErrored = true;
    }

    public void importOrphanedTraders(
      List<MarketTraderData> traderData,
      DateTime curServerTime,
      int villageID)
    {
      GameEngine.Instance.World.clearTraderArray(villageID);
      if (traderData == null)
        return;
      AllArmiesPanel2.TradersUpdated = true;
      foreach (MarketTraderData marketTrader in traderData)
        this.addTrader(marketTrader, curServerTime);
    }

    public void clearTraderArray(int villageID)
    {
      if (villageID == -2)
        return;
      if (villageID < 0)
      {
        this.traderArray.Clear();
      }
      else
      {
        List<WorldMap.LocalTrader> localTraderList = new List<WorldMap.LocalTrader>();
        foreach (WorldMap.LocalTrader trader in this.traderArray)
        {
          if (trader.trader.homeVillageID == villageID)
            localTraderList.Add(trader);
        }
        foreach (WorldMap.LocalTrader localTrader in localTraderList)
          this.traderArray[localTrader.traderID] = (object) null;
      }
    }

    public void addTrader(MarketTraderData marketTrader, DateTime curServerTime)
    {
      WorldMap.LocalTrader localTrader = new WorldMap.LocalTrader();
      localTrader.trader = marketTrader;
      localTrader.traderID = marketTrader.traderID;
      double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
      if (marketTrader.targetVillageID < 0 || marketTrader.homeVillageID < 0)
        return;
      if (marketTrader.traderState > 0)
      {
        localTrader.createJourney(marketTrader.startTime, curServerTime, marketTrader.endTime);
        localTrader.targetDisplayX = (double) this.villageList[marketTrader.targetVillageID].x;
        localTrader.targetDisplayY = (double) this.villageList[marketTrader.targetVillageID].y;
        localTrader.seaTravel = this.isIslandTravel(marketTrader.homeVillageID, marketTrader.targetVillageID);
        foreach (WorldMap.LocalTrader trader in this.traderArray)
        {
          if (trader.parentTrader == -1L && trader.traderID != localTrader.traderID && trader.trader.traderState == marketTrader.traderState && trader.trader.targetVillageID == marketTrader.targetVillageID && trader.trader.homeVillageID == marketTrader.homeVillageID && trader.trader.resource == marketTrader.resource)
          {
            TimeSpan timeSpan = trader.trader.endTime - marketTrader.endTime;
            if (timeSpan.TotalSeconds < 1.0 && timeSpan.TotalSeconds > -1.0)
              localTrader.parentTrader = trader.trader.traderID;
          }
        }
      }
      localTrader.baseDisplayX = (double) this.villageList[marketTrader.homeVillageID].x;
      localTrader.baseDisplayY = (double) this.villageList[marketTrader.homeVillageID].y;
      localTrader.updatePosition(currentMilliseconds);
      this.traderArray[localTrader.traderID] = (object) localTrader;
    }

    public double[] getTraderTimes()
    {
      if (this.thisVillageTraders == null)
        return (double[]) null;
      List<double> doubleList = new List<double>();
      for (int index = 0; index < this.thisVillageTraders.Count; ++index)
        doubleList.Add(this.thisVillageTraders[index].localEndTime);
      return doubleList.ToArray();
    }

    public void updateTraders()
    {
      try
      {
        int num = 0;
        bool flag = true;
        if (!flag)
        {
          this.t_startAt = this.t_endAt;
          if (this.t_startAt >= this.traderArray.Count)
          {
            this.t_startAt = 0;
            this.t_endAt = 0;
          }
          this.t_endAt += this.t_perFrame;
          if (this.t_endAt > this.traderArray.Count)
            this.t_endAt = this.traderArray.Count;
        }
        double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
        List<WorldMap.LocalTrader> localTraderList = new List<WorldMap.LocalTrader>();
        foreach (WorldMap.LocalTrader trader in this.traderArray)
        {
          if (!flag)
          {
            if (num < this.t_startAt)
            {
              ++num;
              continue;
            }
            if (num < this.t_endAt)
              ++num;
            else
              break;
          }
          trader.updatePosition(currentMilliseconds);
          if (trader.dying)
            localTraderList.Add(trader);
        }
        foreach (WorldMap.LocalTrader localTrader in localTraderList)
          this.traderArray[localTrader.trader.traderID] = (object) null;
      }
      catch (Exception ex)
      {
        UniversalDebugLog.Log("exception updating armies " + ex.ToString());
      }
    }

    public bool isVillageTrading(int villageID) => this.tradingVillageList.Contains(villageID);

    public bool isVillageMarketTrading(int villageID)
    {
      return this.marketTradingVillageList.Contains(villageID);
    }

    public void drawTraders(RectangleF screenRect)
    {
      this.tradingVillageList.Clear();
      this.marketTradingVillageList.Clear();
      float num1 = (float) (this.m_worldScale / 28.0 / 0.60000002384185791);
      if ((double) num1 < 0.10000000149011612)
        num1 = 0.1f;
      if ((double) num1 > 1.0)
        num1 = 1f;
      WorldMapFilter worldMapFilter = GameEngine.Instance.World.worldMapFilter;
      foreach (WorldMap.LocalTrader trader in this.traderArray)
      {
        if (trader.trader.traderState > 0 && trader.parentTrader == -1L)
        {
          switch (trader.trader.traderState)
          {
            case 1:
            case 2:
              this.tradingVillageList.Add(trader.trader.targetVillageID);
              this.tradingVillageList.Add(trader.trader.homeVillageID);
              break;
            case 3:
            case 4:
            case 5:
            case 6:
              this.marketTradingVillageList.Add(trader.trader.targetVillageID);
              this.marketTradingVillageList.Add(trader.trader.homeVillageID);
              break;
          }
          if (trader.isVisible(screenRect) && worldMapFilter.showTrader(trader))
          {
            int num2 = 0;
            if (!this.isUserVillage(trader.trader.homeVillageID))
              num2 = 1;
            int num3 = 2 + num2;
            int num4 = num3;
            if (this.DrawingArmyArrows)
            {
              this.villageSprite.PosX = ((float) trader.displayX - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
              this.villageSprite.PosY = ((float) trader.displayY - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
              this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
              this.villageSprite.SpriteNo = num3;
              this.villageSprite.Center = new PointF(44f, 44f);
              this.villageSprite.RotationAngle = SpriteWrapper.getFacing(trader.BasePoint(), trader.TargetPoint());
              this.villageSprite.Scale = num1;
              this.villageSprite.Update();
              this.villageSprite.DrawAndClear();
            }
            int num5 = 10 + num2;
            this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
            this.villageSprite.SpriteNo = num5;
            this.villageSprite.Center = new PointF(44f, 44f);
            this.villageSprite.Scale = num1;
            this.villageSprite.Update();
            this.villageSprite.DrawAndClear();
            if (trader.seaTravel)
            {
              this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
              switch (num4)
              {
                case 0:
                  this.villageSprite.SpriteNo = 425;
                  break;
                case 1:
                  this.villageSprite.SpriteNo = 426;
                  break;
                case 2:
                  this.villageSprite.SpriteNo = 427;
                  break;
                case 3:
                  this.villageSprite.SpriteNo = 428;
                  break;
                case 404:
                  this.villageSprite.SpriteNo = 429;
                  break;
                case 408:
                  this.villageSprite.SpriteNo = 430;
                  break;
                case 412:
                  this.villageSprite.SpriteNo = 431;
                  break;
                case 416:
                  this.villageSprite.SpriteNo = 432;
                  break;
              }
              this.villageSprite.Center = new PointF(44f, 44f);
              this.villageSprite.ColorToUse = Color.FromArgb(this.alphaValue, ARGBColors.White);
              this.villageSprite.Scale = num1;
              this.villageSprite.Update();
              this.villageSprite.DrawAndClear();
            }
          }
        }
      }
    }

    private long findNearestTraderFromScreenPos(Point mousePos, ref double bestDist)
    {
      if (InterfaceMgr.Instance.WorldMapMode != 0)
        return -1;
      double mapX = ((double) mousePos.X - (double) this.m_screenWidth / 2.0) / this.m_worldScale + this.m_screenCentreX;
      double mapY = ((double) mousePos.Y - (double) this.m_screenHeight / 2.0) / this.m_worldScale + this.m_screenCentreY;
      return mapX >= 0.0 && mapX < (double) this.worldMapWidth && mapY >= 0.0 && mapY < (double) this.worldMapHeight ? this.findNearestTraderFromMapPos(mapX, mapY, ref bestDist) : -1L;
    }

    private long findNearestTraderFromMapPos(double mapX, double mapY, ref double bestDist)
    {
      WorldMapFilter worldMapFilter = GameEngine.Instance.World.worldMapFilter;
      long traderFromMapPos = -1;
      double num1 = 2.25;
      foreach (WorldMap.LocalTrader trader in this.traderArray)
      {
        if (trader.trader.traderState > 0 && trader.parentTrader == -1L && worldMapFilter.showTrader(trader))
        {
          double num2 = (trader.displayX - mapX) * (trader.displayX - mapX) + (trader.displayY - mapY) * (trader.displayY - mapY);
          if (num2 < num1)
          {
            num1 = num2;
            traderFromMapPos = trader.traderID;
          }
        }
      }
      bestDist = num1;
      return traderFromMapPos;
    }

    public int getTradingAmount(long traderID)
    {
      int tradingAmount = 0;
      if (this.traderArray[traderID] != null)
      {
        WorldMap.LocalTrader trader1 = (WorldMap.LocalTrader) this.traderArray[traderID];
        tradingAmount += trader1.trader.amount;
        foreach (WorldMap.LocalTrader trader2 in this.traderArray)
        {
          if (trader2.parentTrader == traderID)
            tradingAmount += trader2.trader.amount;
        }
      }
      return tradingAmount;
    }

    public WorldMap.LocalTrader getTrader(long traderID)
    {
      return (WorldMap.LocalTrader) this.traderArray[traderID];
    }

    public SparseArray getTraderArray() => this.traderArray;

    public int getTotalMerchantsFromVillage(int villageID)
    {
      int merchantsFromVillage = 0;
      foreach (WorldMap.LocalTrader trader in this.traderArray)
      {
        if (trader.trader.homeVillageID == villageID)
          merchantsFromVillage += trader.trader.numTraders;
      }
      return merchantsFromVillage;
    }

    public bool allowExchangeTrade(int exchangeVillageID, int buyingVillageID)
    {
      if (exchangeVillageID < 0 || buyingVillageID < 0 || exchangeVillageID >= this.villageList.Length || buyingVillageID >= this.villageList.Length)
        return false;
      VillageData village1 = this.villageList[exchangeVillageID];
      VillageData village2 = this.villageList[buyingVillageID];
      if (!village1.Capital)
        return false;
      switch (this.UserResearchData.Research_Commerce)
      {
        case 0:
          if ((int) village1.regionID != (int) village2.regionID)
            return false;
          break;
        case 1:
          if ((int) village1.countyID != (int) village2.countyID)
            return false;
          break;
        case 2:
          if (village2.countyID < (short) 0 || village1.countyID < (short) 0 || this.countyList[(int) village1.countyID].parentID != this.countyList[(int) village2.countyID].parentID)
            return false;
          break;
        case 3:
          if (village2.countyID < (short) 0 || village1.countyID < (short) 0)
            return false;
          int parentId1 = this.countyList[(int) village1.countyID].parentID;
          int parentId2 = this.countyList[(int) village2.countyID].parentID;
          if (parentId1 < 0 || parentId2 < 0 || this.provincesList[parentId1].parentID != this.provincesList[parentId2].parentID)
            return false;
          break;
      }
      return true;
    }

    public WorldMap.WorldPointList GetProvinceById(int id) => this.provincesList[id];

    public WorldMap.WorldPointList GetCountryById(int id) => this.countryList[id];

    public WorldMap.WorldPointList GetCountryFromWorldPoint(
      WorldMap.WorldPoint candidate,
      RectangleF screenRect)
    {
      return this.GetRegionFromWorldPoint(candidate, screenRect, this.countryList);
    }

    public WorldMap.WorldPointList GetProvinceFromWorldPoint(
      WorldMap.WorldPoint candidate,
      RectangleF screenRect)
    {
      return this.GetRegionFromWorldPoint(candidate, screenRect, this.provincesList);
    }

    public WorldMap.WorldPointList GetCountyFromWorldPoint(
      WorldMap.WorldPoint candidate,
      RectangleF screenRect)
    {
      return this.GetRegionFromWorldPoint(candidate, screenRect, this.countyList);
    }

    public WorldMap.WorldPointList GetParishFromWorldPoint(
      WorldMap.WorldPoint candidate,
      RectangleF screenRect)
    {
      return this.GetRegionFromWorldPoint(candidate, screenRect, this.regionList);
    }

    public WorldMap.WorldPointList GetRegionFromWorldPoint(
      WorldMap.WorldPoint candidate,
      RectangleF screenRect,
      WorldMap.WorldPointList[] regions)
    {
      foreach (WorldMap.WorldPointList region in regions)
      {
        if (region.isVisible(screenRect) && region.PointWithinRegion(candidate))
          return region;
      }
      return (WorldMap.WorldPointList) null;
    }

    public long StoredFactionChangesPos => this.storedFactionChangesPos;

    public long CurrentVillageFactionsPos => this.storedVillageFactionsPos;

    public VillageData GetVllageByID(int id) => this.villageList[id];

    public bool SecondAgeWorld
    {
      get => this.secondAgeWorld;
      set => this.secondAgeWorld = value;
    }

    public bool ThirdAgeWorld
    {
      get => this.thirdAgeWorld;
      set => this.thirdAgeWorld = value;
    }

    public bool FourthAgeWorld
    {
      get => this.fourthAgeWorld;
      set => this.fourthAgeWorld = value;
    }

    public bool FifthAgeWorld
    {
      get => this.fifthAgeWorld;
      set => this.fifthAgeWorld = value;
    }

    public bool SixthAgeWorld
    {
      get => this.sixthAgeWorld;
      set => this.sixthAgeWorld = value;
    }

    public bool SeventhAgeWorld
    {
      get => this.seventhAgeWorld;
      set => this.seventhAgeWorld = value;
    }

    public bool FirstAgeWorld
    {
      get
      {
        return !this.SecondAgeWorld && !this.ThirdAgeWorld && !this.FourthAgeWorld && !this.FifthAgeWorld && !this.SixthAgeWorld && !this.SeventhAgeWorld;
      }
    }

    public HouseData[] HouseInfo
    {
      get => this.m_houseData;
      set => this.m_houseData = value;
    }

    public HouseVoteData HouseVoteInfo
    {
      get => this.m_houseVoteData;
      set => this.m_houseVoteData = value;
    }

    public FactionMemberData[] FactionMembers
    {
      get => this.m_factionMembers;
      set
      {
        this.m_factionMembers = value;
        this.lastTimeOwnMembersUpdated = DateTime.Now;
      }
    }

    public FactionInviteData[] FactionInvites
    {
      get => this.m_factionInvites;
      set => this.m_factionInvites = value;
    }

    public List<FactionInviteData> FactionApplications
    {
      get => this.m_factionApplications;
      set => this.m_factionApplications = value;
    }

    public int[] FactionAllies
    {
      get => this.m_factionAllies;
      set => this.m_factionAllies = value;
    }

    public int[] FactionEnemies
    {
      get => this.m_factionEnemies;
      set => this.m_factionEnemies = value;
    }

    public int YourFactionVote
    {
      get => this.m_factionLeaderVote;
      set => this.m_factionLeaderVote = value;
    }

    public int[] HouseAllies
    {
      get => this.m_houseAllies;
      set => this.m_houseAllies = value;
    }

    public int[] HouseEnemies
    {
      get => this.m_houseEnemies;
      set => this.m_houseEnemies = value;
    }

    public int[] HouseGloryPoints
    {
      get => this.m_gloryPoints;
      set
      {
        this.m_gloryPoints = value;
        this.lastHouseGloryPointsUpdate = DateTime.Now;
      }
    }

    public int getGloryPoints(int houseID)
    {
      return houseID > 0 && this.m_gloryPoints != null && houseID < this.m_gloryPoints.Length ? this.m_gloryPoints[houseID] : 0;
    }

    public GloryRoundData HouseGloryRoundData
    {
      get => this.m_gloryRoundData;
      set => this.m_gloryRoundData = value;
    }

    public bool alreadyGotFactionApplication(int factionID)
    {
      if (this.m_factionApplications != null)
      {
        foreach (FactionInviteData factionApplication in this.m_factionApplications)
        {
          if (factionApplication.factionID == factionID)
            return true;
        }
      }
      return false;
    }

    public bool testGloryPointsUpdate()
    {
      if ((DateTime.Now - this.lastHouseGloryPointsUpdate).TotalHours <= 2.0)
        return false;
      this.lastHouseGloryPointsUpdate = DateTime.Now;
      return true;
    }

    public void clearGloryHistory() => this.lastHouseGloryPointsUpdate = DateTime.MinValue;

    public int getGloryRank(int houseID)
    {
      int[,] numArray = new int[20, 2];
      int num1 = 0;
      for (int index = 0; index < 20; ++index)
      {
        if (!GameEngine.Instance.World.HouseInfo[index + 1].loser)
        {
          numArray[index, 0] = GameEngine.Instance.World.HouseGloryPoints[index + 1];
        }
        else
        {
          numArray[index, 0] = -1;
          ++num1;
        }
        numArray[index, 1] = index;
      }
      for (int index1 = 0; index1 < 19; ++index1)
      {
        for (int index2 = 0; index2 < 19; ++index2)
        {
          if (numArray[index2, 0] < numArray[index2 + 1, 0])
          {
            int num2 = numArray[index2, 0];
            numArray[index2, 0] = numArray[index2 + 1, 0];
            numArray[index2 + 1, 0] = num2;
            int num3 = numArray[index2, 1];
            numArray[index2, 1] = numArray[index2 + 1, 1];
            numArray[index2 + 1, 1] = num3;
          }
        }
      }
      for (int gloryRank = 0; gloryRank < 20 - num1; ++gloryRank)
      {
        if (numArray[gloryRank, 1] + 1 == houseID)
          return gloryRank;
      }
      return -1;
    }

    public FactionData getFaction(int factionID) => (FactionData) this.m_factionData[factionID];

    public void setFactionData(FactionData fd)
    {
      if (fd == null || fd.factionID < 0)
        return;
      this.m_factionData[fd.factionID] = (object) fd;
    }

    public int getFactionRank(int factionID)
    {
      FactionData faction = this.getFaction(factionID);
      if (faction == null)
        return -1;
      int points = faction.points;
      int factionRank = 0;
      foreach (FactionData factionData in this.m_factionData)
      {
        if (factionData.factionID != faction.factionID)
        {
          if (factionData.points > points)
            ++factionRank;
          else if (factionData.points == points && factionData.factionID < faction.factionID)
            ++factionRank;
        }
      }
      return factionRank;
    }

    public string getFactionName(int factionID)
    {
      FactionData faction = this.getFaction(factionID);
      return faction == null ? "" : faction.factionName;
    }

    public SparseArray getAllFactions() => this.m_factionData;

    public int getYourFactionRank()
    {
      if (this.m_factionMembers != null)
      {
        foreach (FactionMemberData factionMember in this.m_factionMembers)
        {
          if (factionMember.userID == RemoteServices.Instance.UserID)
            return factionMember.status;
        }
      }
      return 0;
    }

    public int getYourFactionRelation(int otherFactionID)
    {
      int userFactionId = RemoteServices.Instance.UserFactionID;
      if (userFactionId < 0 || otherFactionID == userFactionId)
        return 0;
      if (this.m_factionAllies != null)
      {
        foreach (int factionAlly in this.m_factionAllies)
        {
          if (factionAlly == otherFactionID)
            return 1;
        }
      }
      if (this.m_factionEnemies != null)
      {
        foreach (int factionEnemy in this.m_factionEnemies)
        {
          if (factionEnemy == otherFactionID)
            return -1;
        }
      }
      return 0;
    }

    public int getYourHouseRelation(int otherHouseID)
    {
      int userFactionId = RemoteServices.Instance.UserFactionID;
      if (userFactionId < 0)
        return 0;
      FactionData faction = this.getFaction(userFactionId);
      if (faction == null)
        return 0;
      int houseId = faction.houseID;
      if (houseId == 0 || otherHouseID == houseId)
        return 0;
      if (this.m_houseAllies != null)
      {
        foreach (int houseAlly in this.m_houseAllies)
        {
          if (houseAlly == otherHouseID)
            return 1;
        }
      }
      if (this.m_houseEnemies != null)
      {
        foreach (int houseEnemy in this.m_houseEnemies)
        {
          if (houseEnemy == otherHouseID)
            return -1;
        }
      }
      return 0;
    }

    public int getYourHouseRank()
    {
      int userFactionId = RemoteServices.Instance.UserFactionID;
      if (userFactionId < 0)
        return 0;
      FactionData faction = this.getFaction(userFactionId);
      return faction == null ? 0 : faction.houseRank;
    }

    public FactionData getFactionLeadingHouse(int houseID)
    {
      foreach (FactionData factionLeadingHouse in this.m_factionData)
      {
        if (factionLeadingHouse.houseID == houseID && factionLeadingHouse.houseRank == 10)
          return factionLeadingHouse;
      }
      return (FactionData) null;
    }

    public FactionData[] getHouseFactions(int houseID)
    {
      List<FactionData> factionDataList = new List<FactionData>();
      if (this.m_factionData != null)
      {
        foreach (FactionData factionData in this.m_factionData)
        {
          if (factionData.houseID == houseID)
            factionDataList.Add(factionData);
        }
      }
      WorldMap.FactionPointsComparer factionPointsComparer = new WorldMap.FactionPointsComparer();
      factionDataList.Sort((IComparer<FactionData>) factionPointsComparer);
      return factionDataList.ToArray();
    }

    public int YourHouse
    {
      get
      {
        FactionData yourFaction = this.YourFaction;
        return yourFaction != null ? yourFaction.houseID : 0;
      }
    }

    public FactionData YourFaction
    {
      get
      {
        return RemoteServices.Instance.UserFactionID >= 0 ? (FactionData) this.m_factionData[RemoteServices.Instance.UserFactionID] : (FactionData) null;
      }
      set
      {
        if (value != null && value.factionID >= 0)
        {
          this.m_factionData[value.factionID] = (object) value;
          RemoteServices.Instance.UserFactionID = value.factionID;
        }
        else
          RemoteServices.Instance.UserFactionID = -1;
      }
    }

    public void setFactionMemberData(int factionID, FactionMemberData[] memberData)
    {
      if (factionID == RemoteServices.Instance.UserFactionID)
        this.FactionMembers = memberData;
      else if (this.cachedFactionMemberData[factionID] == null)
      {
        this.cachedFactionMemberData[factionID] = (object) new FactionCachedMemberData()
        {
          factionID = factionID,
          memberData = memberData,
          lastRefreshed = DateTime.Now
        };
      }
      else
      {
        FactionCachedMemberData cachedMemberData = (FactionCachedMemberData) this.cachedFactionMemberData[factionID];
        cachedMemberData.memberData = memberData;
        cachedMemberData.lastRefreshed = DateTime.Now;
      }
    }

    public FactionMemberData[] getFactionMemberData(int factionID, ref bool uptodate)
    {
      if (factionID == RemoteServices.Instance.UserFactionID)
      {
        TimeSpan timeSpan = DateTime.Now - this.lastTimeOwnMembersUpdated;
        uptodate = timeSpan.TotalMinutes < 1.0;
        return this.FactionMembers;
      }
      uptodate = false;
      if (this.cachedFactionMemberData[factionID] == null)
        return (FactionMemberData[]) null;
      FactionCachedMemberData cachedMemberData = (FactionCachedMemberData) this.cachedFactionMemberData[factionID];
      if ((DateTime.Now - cachedMemberData.lastRefreshed).TotalMinutes < 3.0)
        uptodate = true;
      return cachedMemberData.memberData;
    }

    public int countHouseMembers(int houseID)
    {
      int num = 0;
      foreach (FactionData factionData in this.m_factionData)
      {
        if (factionData.houseID == houseID)
          num += factionData.numMembers;
      }
      return num;
    }

    public int countHouseFactions(int houseID)
    {
      int num = 0;
      foreach (FactionData factionData in this.m_factionData)
      {
        if (factionData.active && factionData.factionName.Length > 0 && factionData.numMembers > 0 && factionData.houseID == houseID)
          ++num;
      }
      return num;
    }

    public bool isAccountPremium()
    {
      return CardTypes.isPremiumToken(GameEngine.Instance.cardsManager.UserCardData.premiumCard) && VillageMap.getCurrentServerTime() < GameEngine.Instance.cardsManager.UserCardData.premiumCardExpiry;
    }

    public bool isAccount730Premium()
    {
      return CardTypes.is730PremiumToken(GameEngine.Instance.cardsManager.UserCardData.premiumCard) && VillageMap.getCurrentServerTime() < GameEngine.Instance.cardsManager.UserCardData.premiumCardExpiry;
    }

    public void logout()
    {
      this.worldEnded = false;
      this.clearGloryHistory();
      if (this.cachedUserInfo != null)
      {
        foreach (WorldMap.CachedUserInfo cachedUserInfo in this.cachedUserInfo)
        {
          if (cachedUserInfo != null && cachedUserInfo.avatarBitmap != null)
          {
            cachedUserInfo.avatarBitmap.Dispose();
            cachedUserInfo.avatarBitmap = (Bitmap) null;
          }
        }
      }
      this.cachedUserInfo = new SparseArray();
      this.cachedFactionMemberData = new SparseArray();
      if (this.villageList != null)
      {
        foreach (VillageData village in this.villageList)
          village.rolloverInfo = (WorldMap.VillageRolloverInfo) null;
      }
      this.cached_retrieveUserID = -1;
      this.cached_retrieveVillageID = -1;
      this.downloadingCounter = 0;
      GameEngine.Instance.cardsManager.onLogout();
      PresetManager.Instance.LogOut();
      this.playbackItems = (List<WorldHouseHistoryItem>) null;
      this.playingCountries = false;
      this.playingProvinces = false;
      this.invasionMarkerState = new SparseArray();
      this.m_userRelatedVillages.Clear();
      if (this.m_userVillages != null)
        this.m_userVillages.Clear();
      this.KillStreakTimer = DateTime.MinValue;
      this.KillStreakCount = 0;
      this.KillStreakPoints = 0;
      this.clearFW();
    }

    public void drawVillage(VillageData village, double scrX, double scrY)
    {
      if (!this.shouldDrawMapIcon(village))
        return;
      this.mapIcon = new MapIconDrawCall(this.gfx, this.villageSprite, this.m_worldZoomInverted, this.m_worldScale, this.mapEditing, new Size(this.m_screenWidth, this.m_screenHeight), this.pulse, this.pulseValue, this.xmasPresents);
      this.mapIcon.draw(village, scrX, scrY);
    }

    public int getVillageFaction(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? this.villageList[villageID].factionID : 0;
    }

    public int getVillageRegion(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? (int) this.villageList[villageID].regionID : 0;
    }

    public int getVillageCounty(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? (int) this.villageList[villageID].countyID : 0;
    }

    public int getCountyProvince(int countyID)
    {
      return countyID >= 0 && countyID < this.countyList.Length ? this.countyList[countyID].parentID : 0;
    }

    public int getProvinceCountry(int provinceID)
    {
      return provinceID >= 0 && provinceID < this.provincesList.Length ? this.provincesList[provinceID].parentID : 0;
    }

    public bool isCapital(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && (this.villageList[villageID].regionCapital || this.villageList[villageID].countyCapital || this.villageList[villageID].provinceCapital || this.villageList[villageID].countryCapital);
    }

    public int getCapitalType(int villageID)
    {
      if (villageID >= 0 && villageID < this.villageList.Length)
      {
        if (this.villageList[villageID].regionCapital)
          return 3;
        if (this.villageList[villageID].countyCapital)
          return 2;
        if (this.villageList[villageID].provinceCapital)
          return 1;
        if (this.villageList[villageID].countryCapital)
          return 0;
      }
      return 4;
    }

    public bool isSpecial(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && this.villageList[villageID].special > 2;
    }

    public bool isSpecialAIPlayer(int villageID)
    {
      if (villageID >= 0 && villageID < this.villageList.Length && this.villageList[villageID].special > 2)
      {
        switch (this.villageList[villageID].special)
        {
          case 7:
          case 9:
          case 11:
          case 13:
            return true;
        }
      }
      return false;
    }

    public int getVillageSize(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? Math.Min((int) this.villageList[villageID].villageInfo / 6, 19) : 0;
    }

    public int getSpecial(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? this.villageList[villageID].special : 0;
    }

    public bool isVillageVisible(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && this.villageList[villageID].visible;
    }

    public bool isScoutableSpecial(int villageID)
    {
      if (villageID >= 0 && villageID < this.villageList.Length)
      {
        if (this.villageList[villageID].special >= 100 && this.villageList[villageID].special <= 199)
          return true;
        switch (this.villageList[villageID].special)
        {
          case 3:
          case 5:
          case 7:
          case 9:
          case 11:
          case 13:
          case 15:
          case 17:
            return true;
          default:
            if (SpecialVillageTypes.IS_TREASURE_CASTLE(this.villageList[villageID].special) || SpecialVillageTypes.IS_ROYAL_TOWER(this.villageList[villageID].special))
              return true;
            break;
        }
      }
      return false;
    }

    public bool isForagingSpecial(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && this.villageList[villageID].special >= 100 && this.villageList[villageID].special <= 199;
    }

    public bool isAttackableSpecial(int villageID)
    {
      if (villageID >= 0 && villageID < this.villageList.Length)
      {
        switch (this.villageList[villageID].special)
        {
          case 3:
          case 5:
          case 7:
          case 9:
          case 11:
          case 13:
          case 15:
          case 17:
            return true;
          default:
            if (SpecialVillageTypes.IS_TREASURE_CASTLE(this.villageList[villageID].special) || SpecialVillageTypes.IS_ROYAL_TOWER(this.villageList[villageID].special))
              return true;
            break;
        }
      }
      return false;
    }

    public bool isRegionCapital(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && this.villageList[villageID].regionCapital;
    }

    public bool isCountyCapital(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && this.villageList[villageID].countyCapital;
    }

    public bool isProvinceCapital(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && this.villageList[villageID].provinceCapital;
    }

    public bool isCountryCapital(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && this.villageList[villageID].countryCapital;
    }

    public List<int> getCapitalList()
    {
      List<int> capitalList = new List<int>();
      foreach (VillageData village in this.villageList)
      {
        if (village.Capital && village.visible)
          capitalList.Add(village.id);
      }
      return capitalList;
    }

    public int getSquareDistance(int villageID1, int villageID2)
    {
      return ((int) this.villageList[villageID1].x - (int) this.villageList[villageID2].x) * ((int) this.villageList[villageID1].x - (int) this.villageList[villageID2].x) + ((int) this.villageList[villageID1].y - (int) this.villageList[villageID2].y) * ((int) this.villageList[villageID1].y - (int) this.villageList[villageID2].y);
    }

    public double getDistance(int from, int to)
    {
      Point villageLocation1 = this.getVillageLocation(from);
      Point villageLocation2 = this.getVillageLocation(to);
      int x1 = villageLocation1.X;
      int y1 = villageLocation1.Y;
      int x2 = villageLocation2.X;
      int y2 = villageLocation2.Y;
      return Math.Sqrt((double) ((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)));
    }

    public bool isValidArmyTarget(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && !this.villageList[villageID].countyCapital && !this.villageList[villageID].provinceCapital && !this.villageList[villageID].countryCapital && (!this.villageList[villageID].regionCapital || this.villageList[villageID].factionID != 0);
    }

    public int getVillageUserID(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? this.villageList[villageID].userID : -1;
    }

    public Point getVillageLocation(int villageID)
    {
      Point villageLocation = new Point(-1, -1);
      if (villageID >= 0 && villageID < this.villageList.Length)
      {
        villageLocation.X = (int) this.villageList[villageID].x;
        villageLocation.Y = (int) this.villageList[villageID].y;
      }
      return villageLocation;
    }

    public List<int> searchVillageNames(string searchString)
    {
      List<int> intList = new List<int>();
      searchString = searchString.ToLower();
      foreach (VillageData village in this.villageList)
      {
        if (village.special == 0 && village.visible && (village.userID >= 0 || village.Capital) && village.m_villageName.ToLower().Contains(searchString))
          intList.Add(village.id);
      }
      return intList;
    }

    public Point getRegionCapitalLocation(int regionID)
    {
      Point regionCapitalLocation = new Point(-1, -1);
      if (regionID >= 0 && regionID < this.regionList.Length)
      {
        int capitalVillage = this.regionList[regionID].capitalVillage;
        if (capitalVillage >= 0 && capitalVillage < this.villageList.Length)
        {
          regionCapitalLocation.X = (int) this.villageList[capitalVillage].x;
          regionCapitalLocation.Y = (int) this.villageList[capitalVillage].y;
          return regionCapitalLocation;
        }
      }
      return regionCapitalLocation;
    }

    public int getRegionCapitalVillage(int regionID)
    {
      return regionID >= 0 && regionID < this.regionList.Length ? this.regionList[regionID].capitalVillage : -1;
    }

    public Point getCountyCapitalLocation(int countyID)
    {
      Point countyCapitalLocation = new Point(-1, -1);
      if (countyID >= 0 && countyID < this.countyList.Length)
      {
        int capitalVillage = this.countyList[countyID].capitalVillage;
        if (capitalVillage >= 0 && capitalVillage < this.villageList.Length)
        {
          countyCapitalLocation.X = (int) this.villageList[capitalVillage].x;
          countyCapitalLocation.Y = (int) this.villageList[capitalVillage].y;
          return countyCapitalLocation;
        }
      }
      return countyCapitalLocation;
    }

    public int getCountyCapitalVillage(int countyID)
    {
      return countyID >= 0 && countyID < this.countyList.Length ? this.countyList[countyID].capitalVillage : -1;
    }

    public Point getCountyMarkerLocation(int countyID)
    {
      Point countyMarkerLocation = new Point(-1, -1);
      if (countyID >= 0 && countyID < this.countyList.Length)
        countyMarkerLocation = this.countyList[countyID].marker;
      return countyMarkerLocation;
    }

    public int numCounties() => this.countyList.Length;

    public Point getProvinceCapitalLocation(int provinceID)
    {
      Point provinceCapitalLocation = new Point(-1, -1);
      if (provinceID >= 0 && provinceID < this.provincesList.Length)
      {
        int capitalVillage = this.provincesList[provinceID].capitalVillage;
        if (capitalVillage >= 0 && capitalVillage < this.villageList.Length)
        {
          provinceCapitalLocation.X = (int) this.villageList[capitalVillage].x;
          provinceCapitalLocation.Y = (int) this.villageList[capitalVillage].y;
          return provinceCapitalLocation;
        }
      }
      return provinceCapitalLocation;
    }

    public int getProvinceCapital(int provinceID)
    {
      return provinceID >= 0 && provinceID < this.provincesList.Length ? this.provincesList[provinceID].capitalVillage : -1;
    }

    public int getVillageTerrainType(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? (int) this.villageList[villageID].villageTerrain : 0;
    }

    public string getVillageName(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? this.villageList[villageID].villageName : "";
    }

    public string getVillageNameOnly(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? this.villageList[villageID].m_villageName : "";
    }

    public VillageData getVillageData(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? this.villageList[villageID] : (VillageData) null;
    }

    public int getVillageParent(int villageID)
    {
      if (villageID < 0 || villageID >= this.villageList.Length)
        return villageID;
      VillageData village = this.villageList[villageID];
      if (!village.Capital)
        return this.getRegionCapitalVillage((int) village.regionID);
      if (village.regionCapital)
        return this.getCountyCapitalVillage((int) village.countyID);
      if (village.countyCapital)
        return this.getProvinceCapital(this.getProvinceFromVillageID(villageID));
      return village.provinceCapital ? this.getCountryCapital(this.getCountryFromVillageID(villageID)) : villageID;
    }

    public string getVillageNameOrType(int villageID)
    {
      if (this.isSpecial(villageID))
      {
        int special = this.getSpecial(villageID);
        if (GameEngine.Instance.LocalWorldData.AIWorld)
        {
          switch (special)
          {
            case 7:
            case 9:
            case 11:
            case 13:
              if (Program.mySettings.viewVillageIDs)
                return "[" + villageID.ToString() + "] " + SpecialVillageTypes.getName(special, Program.mySettings.LanguageIdent);
              break;
          }
        }
        if (SpecialVillageTypes.IS_ROYAL_TOWER(special) && Program.mySettings.viewVillageIDs)
          return "[" + villageID.ToString() + "] " + SpecialVillageTypes.getName(special, Program.mySettings.LanguageIdent);
        switch (special)
        {
          case 2:
            return SK.Text("GENERIC_Unknown", "Unknown");
          case 3:
          case 4:
            return SK.Text("GENERIC_Bandit_Camp", "Bandit Camp");
          case 5:
          case 6:
            return SK.Text("GENERIC_Wolf_Camp", "Wolf Lair");
          case 30:
            return SK.Text("GENERIC_Invasion", "Invasion");
          default:
            return SpecialVillageTypes.getName(special, Program.mySettings.LanguageIdent);
        }
      }
      else
      {
        if (!this.isVillageVisible(villageID))
          return "";
        return !this.isCapital(villageID) && this.getVillageUserID(villageID) < 0 ? SK.Text("ReportFilter_Village_Charter", "Village Charter") : GameEngine.Instance.World.getVillageName(villageID);
      }
    }

    public bool isVillageInterdictProtected(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && VillageMap.getCurrentServerTime() < this.villageList[villageID].interdictionTime;
    }

    public DateTime getInterdictTime(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? this.villageList[villageID].interdictionTime : DateTime.MinValue;
    }

    public void setInterdictTime(int villageID, DateTime interdictionTime)
    {
      if (villageID < 0 || villageID >= this.villageList.Length)
        return;
      this.villageList[villageID].interdictionTime = interdictionTime;
    }

    public void setPeaceTime(int villageID, DateTime peaceTime)
    {
      if (villageID < 0 || villageID >= this.villageList.Length)
        return;
      this.villageList[villageID].peaceTime = peaceTime;
    }

    public void setExcommunicationTime(int villageID, DateTime excommunicationTime)
    {
      if (villageID < 0 || villageID >= this.villageList.Length)
        return;
      this.villageList[villageID].excommunicationTime = excommunicationTime;
    }

    public DateTime getExcommunicationTime(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? this.villageList[villageID].excommunicationTime : DateTime.MinValue;
    }

    public bool isVillagePeaceTimeProtected(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && VillageMap.getCurrentServerTime() < this.villageList[villageID].peaceTime;
    }

    public bool isVillageVacationProtected(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && this.villageList[villageID].vacationMode;
    }

    public bool isVillageExcommunicated(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && VillageMap.getCurrentServerTime() < this.villageList[villageID].excommunicationTime;
    }

    public DateTime getPeaceTime(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? this.villageList[villageID].peaceTime : DateTime.MinValue;
    }

    public void setVillageName(int villageID, string villageName)
    {
      if (villageID < 0 || villageID >= this.villageList.Length)
        return;
      this.villageList[villageID].villageName = villageName;
      this.sortUserVillages();
    }

    public void setParishName(int villageID, string villageName)
    {
      if (villageID < 0 || villageID >= this.villageList.Length)
        return;
      this.villageList[villageID].villageName = villageName;
      int parishFromVillageId = this.getParishFromVillageID(villageID);
      if (parishFromVillageId >= 0 && parishFromVillageId < this.regionList.Length)
        this.regionList[parishFromVillageId].areaName = villageName;
      this.sortUserVillages();
    }

    public void ImportParishNames(string[] newNames)
    {
      if (newNames == null || newNames.Length <= 0)
        return;
      for (int index = 0; index < newNames.Length; ++index)
      {
        this.regionList[index].areaName = newNames[index];
        int capitalVillage = this.regionList[index].capitalVillage;
        if (capitalVillage >= 0 && capitalVillage < this.villageList.Length)
        {
          this.villageList[capitalVillage].villageName = newNames[index];
          this.villageList[capitalVillage].visible = true;
        }
      }
    }

    public string[] getParishNameList()
    {
      string[] parishNameList = new string[this.regionList.Length];
      for (int index = 0; index < this.regionList.Length; ++index)
        parishNameList[index] = this.regionList[index].areaName;
      return parishNameList;
    }

    public string getParishName(int parishID)
    {
      return parishID >= 0 && parishID < this.regionList.Length ? this.regionList[parishID].areaName : "";
    }

    public string getParishNameFromVillageID(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? this.regionList[(int) this.villageList[villageID].regionID].areaName : "";
    }

    public int getParishIDFromName(string parishName)
    {
      for (int parishIdFromName = 0; parishIdFromName < this.regionList.Length; ++parishIdFromName)
      {
        if (this.regionList[parishIdFromName].areaName == parishName)
          return parishIdFromName;
      }
      return -1;
    }

    public int getParishPlague(int parishID)
    {
      return parishID >= 0 && parishID < this.regionList.Length ? this.regionList[parishID].plague : 0;
    }

    public int getParishCapital(int parishID)
    {
      return parishID >= 0 && parishID < this.regionList.Length ? this.regionList[parishID].capitalVillage : -1;
    }

    public int getCountyIDFromParishID(int parishID)
    {
      return parishID >= 0 && parishID < this.regionList.Length ? this.regionList[parishID].parentID : -1;
    }

    public string getExchangeName(int villageID)
    {
      return this.isRegionCapital(villageID) ? this.getParishNameFromVillageID(villageID) : this.getVillageName(villageID);
    }

    public string getCountyName(int countyID)
    {
      return countyID >= 0 && countyID < this.countyList.Length ? this.countyList[countyID].areaName : "";
    }

    public string getProvinceName(int provinceID)
    {
      return provinceID >= 0 && provinceID < this.provincesList.Length ? this.provincesList[provinceID].areaName : "";
    }

    public string getCountryName(int countryID)
    {
      return countryID >= 0 && countryID < this.countryList.Length ? this.countryList[countryID].areaName : "";
    }

    public int getCountryCapital(int countryID)
    {
      return countryID >= 0 && countryID < this.countryList.Length ? this.countryList[countryID].capitalVillage : -1;
    }

    public int getNumParishes() => this.regionList.Length;

    public int getParishFromVillageID(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? (int) this.villageList[villageID].regionID : -1;
    }

    public int getCountyFromVillageID(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length ? (int) this.villageList[villageID].countyID : -1;
    }

    public int getProvinceFromVillageID(int villageID)
    {
      int countyFromVillageId = this.getCountyFromVillageID(villageID);
      return countyFromVillageId >= 0 ? this.countyList[countyFromVillageId].parentID : -1;
    }

    public int getCountryFromVillageID(int villageID)
    {
      int provinceFromVillageId = this.getProvinceFromVillageID(villageID);
      return provinceFromVillageId >= 0 ? this.provincesList[provinceFromVillageId].parentID : -1;
    }

    public void givePlaguesToParish(int parishID)
    {
      if (parishID < 0 || parishID >= this.regionList.Length || this.regionList[parishID].plague != 0)
        return;
      this.regionList[parishID].plague = 1;
    }

    public string getVillageAddress(int villageID)
    {
      int parishFromVillageId = this.getParishFromVillageID(villageID);
      int countyFromVillageId = this.getCountyFromVillageID(villageID);
      int provinceFromVillageId = this.getProvinceFromVillageID(villageID);
      int countryFromVillageId = this.getCountryFromVillageID(villageID);
      return this.getParishName(parishFromVillageId) + ", " + this.getCountyName(countyFromVillageId) + ", " + this.getProvinceName(provinceFromVillageId) + ", " + this.getCountryName(countryFromVillageId);
    }

    public string getVillageDescription(int villageID)
    {
      string villageDescription = "";
      if (this.isCapital(villageID))
      {
        switch (this.getCapitalType(villageID))
        {
          case 0:
            int countryFromVillageId = this.getCountryFromVillageID(villageID);
            villageDescription = SK.Text("TOUCH_Y_CountryCapital", "The Country Capital of") + " " + this.getCountryName(countryFromVillageId);
            break;
          case 1:
            int provinceFromVillageId = this.getProvinceFromVillageID(villageID);
            villageDescription = SK.Text("TOUCH_Y_ProvinceCapital", "The Province Capital of") + " " + this.getProvinceName(provinceFromVillageId);
            break;
          case 2:
            int countyFromVillageId = this.getCountyFromVillageID(villageID);
            villageDescription = SK.Text("TOUCH_Y_CountyCapital", "The County Capital of") + " " + this.getCountyName(countyFromVillageId);
            break;
          case 3:
            villageDescription = SK.Text("TOUCH_Y_ParishCapital", "Parish Capital");
            break;
        }
      }
      if (this.isSpecial(villageID))
        villageDescription = SK.Text("TOUCH_Y_InParishOf", "In the parish of") + " " + this.getParishNameFromVillageID(villageID);
      return villageDescription;
    }

    public bool isScoutHonourOutOfRange(int userVillageID, int targetVillageID)
    {
      if (userVillageID < 0 || targetVillageID < 0 || this.isCapital(userVillageID) || !this.isSpecial(targetVillageID))
        return false;
      int num1 = CardTypes.adjustScoutingHonourRange(GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData.BaseScoutHonourRange);
      int num2 = num1 * num1;
      int x1 = (int) this.villageList[targetVillageID].x;
      int y1 = (int) this.villageList[targetVillageID].y;
      int x2 = (int) this.villageList[userVillageID].x;
      int y2 = (int) this.villageList[userVillageID].y;
      return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) >= num2;
    }

    public int[] getRoyalTowerCounts()
    {
      int[] royalTowerCounts = new int[21];
      for (int index = 0; index < 21; ++index)
        royalTowerCounts[index] = 0;
      foreach (VillageData village in this.villageList)
      {
        if (village.visible && SpecialVillageTypes.IS_ROYAL_TOWER(village.special))
        {
          int index = village.special - 200;
          if (index >= 0 && index < 21)
            ++royalTowerCounts[index];
        }
      }
      return royalTowerCounts;
    }

    public int countRemainingRoyalTowers()
    {
      int total = 0;
      return this.countRemainingRoyalTowers(ref total);
    }

    public int countRemainingRoyalTowers(ref int total)
    {
      total = 0;
      int[] royalTowerCounts = this.getRoyalTowerCounts();
      int num1 = 0;
      int num2 = -1;
      int num3 = 0;
      for (int index = 1; index < 21; ++index)
      {
        if (royalTowerCounts[index] > num3)
        {
          num3 = royalTowerCounts[index];
          num2 = index;
        }
      }
      for (int index = 0; index < 21; ++index)
      {
        if (num2 != index)
          num1 += royalTowerCounts[index];
        total += royalTowerCounts[index];
      }
      return num1;
    }

    public int SpecialSeaConditionsData
    {
      get
      {
        DateTime currentServerTime = VillageMap.getCurrentServerTime();
        return currentServerTime.Day == this.seaConditionsDay ? (currentServerTime.Hour < 19 ? this.seaConditionsEarly : this.seaConditionsLate) : (currentServerTime.Day == 1 && this.seaConditionsDay > 25 ? this.seaConditionsLate : 0);
      }
      set
      {
        this.seaConditionsDay = value >> 16;
        this.seaConditionsEarly = (value >> 8 & (int) byte.MaxValue) - 4;
        this.seaConditionsLate = (value & (int) byte.MaxValue) - 4;
      }
    }

    public bool isIslandWorld() => WorldMapTypes.isIslandWorld(this.m_globalWorldID);

    public bool isIslandTravel(int fromVillageID, int targetVillageID)
    {
      return WorldMapTypes.isIslandTravel(this.m_globalWorldID, this.getCountyFromVillageID(fromVillageID), this.getCountyFromVillageID(targetVillageID));
    }

    public double adjustIfIslandTravel(double distance, int homeVillage, int targetVillage)
    {
      if (WorldMapTypes.isIslandWorld(this.m_globalWorldID) && this.isIslandTravel(homeVillage, targetVillage))
      {
        int seaConditionsData = this.SpecialSeaConditionsData;
        distance = WorldMapTypes.adjustTravelTimes(distance, seaConditionsData);
      }
      return distance;
    }

    public void clearVillageRolloverInfo(int villageID)
    {
      if (villageID < 0 || villageID >= this.villageList.Length)
        return;
      this.villageList[villageID].rolloverInfo = (WorldMap.VillageRolloverInfo) null;
    }

    public bool retrieveUserData(
      int villageID,
      int userID,
      ref WorldMap.VillageRolloverInfo villageInfo,
      ref WorldMap.CachedUserInfo userInfo,
      bool doServerRetrieve,
      bool forceExtended)
    {
      try
      {
        if (doServerRetrieve)
          villageInfo = (WorldMap.VillageRolloverInfo) null;
        villageInfo = (WorldMap.VillageRolloverInfo) null;
        userInfo = (WorldMap.CachedUserInfo) null;
        if (villageID >= 0)
        {
          userID = this.villageList[villageID].userID;
          if (userID == -1 && GameEngine.Instance.LocalWorldData.AIWorld)
          {
            this.getSpecial(villageID);
            switch (GameEngine.Instance.World.getSpecial(villageID))
            {
              case 7:
                userID = 1;
                break;
              case 9:
                userID = 2;
                break;
              case 11:
                userID = 3;
                break;
              case 13:
                userID = 4;
                break;
            }
          }
          villageInfo = this.villageList[villageID].rolloverInfo;
          if (villageInfo != null && (DateTime.Now - villageInfo.lastUpdateTime).TotalMinutes > 3.0)
            villageInfo = (WorldMap.VillageRolloverInfo) null;
        }
        bool flag1 = false;
        if (userID >= 0)
        {
          userInfo = (WorldMap.CachedUserInfo) this.cachedUserInfo[userID];
          if (userInfo != null)
          {
            if ((DateTime.Now - userInfo.lastUpdateTime).TotalMinutes > 2.0)
              flag1 = true;
            if (!flag1 && userInfo.villages == null)
              flag1 = true;
          }
        }
        if ((villageID < 0 || villageInfo != null) && (userID < 0 || userInfo != null && !flag1))
          return true;
        if (doServerRetrieve)
        {
          bool flag2 = false;
          if (userID >= 0)
          {
            if (userID == this.lastRetieveUserID && forceExtended == this.lastForceExtended)
            {
              if ((DateTime.Now - this.lastRetieveUserTime).TotalSeconds < 30.0 && (villageID < 0 || villageInfo != null || villageID == this.lastRetieveVillageID))
                flag2 = true;
              else
                this.lastRetieveUserTime = DateTime.Now;
            }
            else
            {
              this.lastRetieveUserID = userID;
              this.lastRetieveUserTime = DateTime.Now;
              this.lastForceExtended = forceExtended;
            }
          }
          else if (villageID == this.lastRetieveVillageID)
          {
            if ((DateTime.Now - this.lastRetieveVillageTime).TotalSeconds < 30.0)
              flag2 = true;
            else
              this.lastRetieveVillageTime = DateTime.Now;
          }
          else
          {
            this.lastRetieveVillageID = villageID;
            this.lastRetieveVillageTime = DateTime.Now;
          }
          if (!flag2)
          {
            if (forceExtended)
            {
              RemoteServices.Instance.set_RetrieveVillageUserInfo_UserCallBack(new RemoteServices.RetrieveVillageUserInfo_UserCallBack(this.villageUserInfoCallback));
              RemoteServices.Instance.RetrieveVillageUserInfo(villageID, userID, forceExtended);
              this.cached_retrieveVillageID = -1;
              this.cached_retrieveUserID = -1;
            }
            else if (this.cached_retrieveUserID != userID || this.cached_retrieveVillageID != villageID)
            {
              this.cached_retrieveUserID = userID;
              this.cached_retrieveVillageID = villageID;
              this.cached_retrieveVillageUserInfoDate = DateTime.Now;
            }
          }
        }
        return false;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public void clearCachedVillageUserInfo()
    {
      this.cached_retrieveVillageID = -1;
      this.cached_retrieveUserID = -1;
    }

    public void monitorCachedVillageUserInfo()
    {
      if (this.cached_retrieveVillageID == -1 && this.cached_retrieveUserID == -1 || (DateTime.Now - this.cached_retrieveVillageUserInfoDate).TotalMilliseconds <= 800.0)
        return;
      RemoteServices.Instance.set_RetrieveVillageUserInfo_UserCallBack(new RemoteServices.RetrieveVillageUserInfo_UserCallBack(this.villageUserInfoCallback));
      RemoteServices.Instance.RetrieveVillageUserInfo(this.cached_retrieveVillageID, this.cached_retrieveUserID, false);
      this.cached_retrieveVillageID = -1;
      this.cached_retrieveUserID = -1;
    }

    public void villageUserInfoCallback(RetrieveVillageUserInfo_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      if (returnData.villageID >= 0)
      {
        this.villageList[returnData.villageID].rolloverInfo = new WorldMap.VillageRolloverInfo()
        {
          lastUpdateTime = DateTime.Now,
          interdictionTime = returnData.interdictionDate,
          vacationMode = returnData.vacationMode,
          peaceTime = returnData.peaceTime,
          villageID = returnData.villageID,
          plagueLevel = returnData.plagueLevel
        };
        this.villageList[returnData.villageID].userID = returnData.userID;
        this.villageList[returnData.villageID].villageTerrain = (short) returnData.mapType;
        this.villageList[returnData.villageID].villageInfo = returnData.numVillageBuildings < (int) byte.MaxValue ? (byte) returnData.numVillageBuildings : byte.MaxValue;
        this.villageList[returnData.villageID].interdictionTime = returnData.interdictionDate;
        this.villageList[returnData.villageID].peaceTime = returnData.peaceTime;
        this.villageList[returnData.villageID].excommunicationTime = returnData.excommunicationTime;
        this.villageList[returnData.villageID].vacationMode = returnData.vacationMode;
      }
      if (returnData.userID < 0)
        return;
      WorldMap.CachedUserInfo cachedUserInfo = new WorldMap.CachedUserInfo();
      cachedUserInfo.userID = returnData.userID;
      cachedUserInfo.userName = returnData.userName;
      cachedUserInfo.rank = returnData.userRank;
      cachedUserInfo.numVillages = returnData.numVillages;
      cachedUserInfo.numQuests = returnData.numQuests;
      cachedUserInfo.completedQuests = returnData.completedQuests;
      cachedUserInfo.points = returnData.points;
      cachedUserInfo.standing = returnData.standing;
      cachedUserInfo.factionID = returnData.factionID;
      cachedUserInfo.lastUpdateTime = DateTime.Now;
      cachedUserInfo.avatarData = returnData.avatarData;
      if (GameEngine.Instance.LocalWorldData.AIWorld)
      {
        switch (returnData.userID)
        {
          case 1:
            cachedUserInfo.avatarData = Avatar.getRatAvatar();
            break;
          case 2:
            cachedUserInfo.avatarData = Avatar.getSnakeAvatar();
            break;
          case 3:
            cachedUserInfo.avatarData = Avatar.getPigAvatar();
            break;
          case 4:
            cachedUserInfo.avatarData = Avatar.getWolfAvatar();
            break;
        }
      }
      cachedUserInfo.avatarData.validateColours();
      if (cachedUserInfo.avatarBitmap != null)
        cachedUserInfo.avatarBitmap.Dispose();
      cachedUserInfo.avatarBitmap = Avatar.CreateAvatar(cachedUserInfo.avatarData, 214, ARGBColors.Transparent, false);
      if (returnData.villages != null)
      {
        cachedUserInfo.villages = returnData.villages.ToArray();
        cachedUserInfo.admin = returnData.admin;
        cachedUserInfo.moderator = returnData.moderator;
        cachedUserInfo.stuff = returnData.stuff;
      }
      if (returnData.achievements != null)
        cachedUserInfo.achievements = returnData.achievements;
      this.cachedUserInfo[returnData.userID] = (object) cachedUserInfo;
    }

    public WorldMap.CachedUserInfo getStoredUserInfo(int userID)
    {
      return (WorldMap.CachedUserInfo) this.cachedUserInfo[userID];
    }

    public int getParishPlagueLevel(int villageID)
    {
      return villageID >= 0 && villageID < this.villageList.Length && this.villageList[villageID].rolloverInfo != null ? this.villageList[villageID].rolloverInfo.plagueLevel : -1;
    }

    public WorldMap.SpecialVillageCache getSpecialVillageData(int villageID, bool download)
    {
      WorldMap.SpecialVillageCache specialVillageData = (WorldMap.SpecialVillageCache) null;
      if (villageID >= 0)
      {
        bool flag1 = false;
        if (this.specialVillageCache[villageID] == null)
        {
          flag1 = true;
        }
        else
        {
          specialVillageData = (WorldMap.SpecialVillageCache) this.specialVillageCache[villageID];
          if ((DateTime.Now - specialVillageData.lastUpdate).TotalMinutes > 1.0)
            flag1 = true;
          if (this.villageList[villageID].special > 100 && this.villageList[villageID].special <= 199)
          {
            if (this.villageList[villageID].special - 100 != specialVillageData.resourceType)
            {
              this.specialVillageCache[villageID] = (object) null;
              specialVillageData = (WorldMap.SpecialVillageCache) null;
            }
          }
          else
          {
            this.specialVillageCache[villageID] = (object) null;
            specialVillageData = (WorldMap.SpecialVillageCache) null;
          }
        }
        if (flag1 && this.lastSpecialRequestSent != villageID)
        {
          bool flag2 = true;
          if (this.lastActualSpecialRequestSent == villageID && (DateTime.Now - this.lastActualSpecialRequestTime).TotalMinutes < 1.0)
            flag2 = false;
          if (flag2 && this.villageList[villageID].special > 100 && this.villageList[villageID].special <= 199)
          {
            RemoteServices.Instance.set_SpecialVillageInfo_UserCallBack(new RemoteServices.SpecialVillageInfo_UserCallBack(this.specialVillageInfoCallback));
            RemoteServices.Instance.SpecialVillageInfo(villageID);
            this.lastSpecialRequestSent = villageID;
            this.lastActualSpecialRequestSent = villageID;
            this.lastActualSpecialRequestTime = DateTime.Now;
          }
        }
      }
      return specialVillageData;
    }

    public void specialVillageInfoCallback(SpecialVillageInfo_ReturnType returnData)
    {
      this.lastSpecialRequestSent = -1;
      if (!returnData.Success || returnData.villageID < 0)
        return;
      this.specialVillageCache[returnData.villageID] = (object) new WorldMap.SpecialVillageCache()
      {
        resourceType = returnData.resourceType,
        resourceLevel = returnData.resourceLevel
      };
    }

    public List<LoginHistoryInfo> getLoginHistory(bool request)
    {
      if (this.loginHistory != null)
        return this.loginHistory;
      if (request)
      {
        RemoteServices.Instance.set_GetLoginHistory_UserCallBack(new RemoteServices.GetLoginHistory_UserCallBack(this.getLoginHistoryCallback));
        RemoteServices.Instance.GetLoginHistory();
      }
      return (List<LoginHistoryInfo>) null;
    }

    public void getLoginHistoryCallback(GetLoginHistory_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.loginHistory = returnData.history;
    }

    public void registerWorldIdentifier(int worldID)
    {
      if (worldID == this.m_globalWorldID)
        return;
      this.m_globalWorldID = worldID;
    }

    public int GetGlobalWorldID() => this.m_globalWorldID;

    public void RetrievePreviousContestIDs()
    {
      this.previousContests.Clear();
      RemoteServices.Instance.set_GetContestHistoryIDs_UserCallBack(new RemoteServices.GetContestHistoryIDs_UserCallBack(this.RetrievePreviousContestIDsCallback));
      RemoteServices.Instance.GetContestHistoryIDs();
    }

    private void RetrievePreviousContestIDsCallback(GetContestHistoryIDs_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      for (int index = 0; index < returnData.contestIDs.Length; ++index)
      {
        if (returnData.contestIDs[index] > 0)
          this.previousContests.Add(returnData.contestIDs[index]);
      }
      InterfaceMgr.Instance.getMainMenuBar().setContestLeaderboardButtonVisible(this.previousContests.Count > 0 || GameEngine.Instance.World.contestID > 0);
    }

    public void downloadPlayerShield(string md5, ShieldFactory.AsyncDelegate callback)
    {
      this.playerShieldCallback = callback;
      if (this.playerShieldFactory == null)
        this.playerShieldFactory = new ShieldFactory();
      this.playerShieldFactory.clear();
      this.playerShield = (Shield) null;
      this.playerShieldFactory.downloadPlayerShieldAsync(md5, new ShieldFactory.AsyncDelegate(this.shieldDownloaded));
    }

    public void shieldDownloaded()
    {
      if (this.playerShieldCallback == null)
        return;
      this.playerShieldCallback();
    }

    public Image getPlayerShieldImage(int width, int height)
    {
      return this.getPlayerShieldImage(width, height, width, height);
    }

    public Image getPlayerShieldImage(int width, int height, int bmapWidth, int bmapHeight)
    {
      if (this.playerShieldFactory == null || !this.playerShieldFactory.PlayerAvailable)
        return this.getDummyShield(width, height, bmapWidth, bmapHeight);
      if (this.playerShield == null)
        this.playerShield = this.playerShieldFactory.getPlayerShield();
      return this.playerShield != null ? this.shieldOverlay((Image) this.playerShield.Render(width, height, bmapWidth, bmapHeight), width, height, bmapWidth, bmapHeight) : (Image) null;
    }

    public string getPlayerShieldString()
    {
      if (this.playerShieldFactory != null && this.playerShieldFactory.PlayerAvailable)
      {
        if (this.playerShield == null)
          this.playerShield = this.playerShieldFactory.getPlayerShield();
        if (this.playerShield != null)
          return this.playerShield.getString();
      }
      return "";
    }

    public void downloadWorldShields(int worldID)
    {
      this.activeShieldsWorldID = worldID;
      ShieldFactory worldShield = (ShieldFactory) this.worldShields[worldID];
      ShieldFactory shieldFactory;
      if (worldShield != null)
      {
        shieldFactory = worldShield;
        if (!shieldFactory.WorldsRequireRefresh(new TimeSpan(1, 0, 0)))
        {
          this.worldShieldsAvailable = true;
          this.clearShieldCache();
          return;
        }
        shieldFactory.clearWorld();
      }
      else
      {
        shieldFactory = new ShieldFactory();
        this.worldShields[worldID] = (object) shieldFactory;
      }
      this.worldShieldsAvailable = false;
      this.clearShieldCache();
      shieldFactory.downloadWorldShieldsAsync(worldID, new ShieldFactory.AsyncDelegate(this.worldShieldsDownloaded));
    }

    private void worldShieldsDownloaded()
    {
      this.worldShieldsAvailable = true;
      this.clearShieldCache();
    }

    public bool isWorldShieldAvailable(int playerID)
    {
      if (playerID == RemoteServices.Instance.UserID)
      {
        if (this.playerShieldFactory != null)
          return this.playerShieldFactory.PlayerAvailable;
      }
      else if (this.worldShieldsAvailable)
      {
        ShieldFactory worldShield = (ShieldFactory) this.worldShields[this.activeShieldsWorldID];
        if (worldShield != null && worldShield.WorldAvailable)
          return worldShield.isWorldShieldAvailable(playerID);
      }
      return false;
    }

    public Image getWorldShieldOrBlank(int playerID, int width, int height)
    {
      return this.getWorldShield(playerID, width, height, width, height) ?? this.getDummyShield(width, height);
    }

    public Image getWorldShield(int playerID, int width, int height)
    {
      return this.getWorldShield(playerID, width, height, width, height);
    }

    public Image getWorldShield(
      int playerID,
      int width,
      int height,
      int bmapWidth,
      int bmapHeight)
    {
      if (GameEngine.Instance.LocalWorldData.AIWorld)
      {
        switch (playerID)
        {
          case 1:
            playerID = -1;
            break;
          case 2:
            playerID = -2;
            break;
          case 3:
            playerID = -3;
            break;
          case 4:
            playerID = -4;
            break;
        }
      }
      if (playerID == RemoteServices.Instance.UserID)
        return this.getPlayerShieldImage(width, height, bmapWidth, bmapHeight);
      if (playerID < 0)
        return this.renderAIShield(playerID, width, height, bmapWidth, bmapHeight);
      if (this.worldShieldsAvailable)
      {
        ShieldFactory worldShield1 = (ShieldFactory) this.worldShields[this.activeShieldsWorldID];
        if (worldShield1 != null && worldShield1.WorldAvailable)
        {
          Shield worldShield2 = worldShield1.getWorldShield(playerID);
          if (worldShield2 != null)
            return this.shieldOverlay((Image) worldShield2.Render(width, height, bmapWidth, bmapHeight), width, height, bmapWidth, bmapHeight);
        }
      }
      return (Image) null;
    }

    private Image shieldOverlay(
      Image sourceImage,
      int width,
      int height,
      int bmapWidth,
      int bmapHeight)
    {
      int width1 = width;
      int height1 = height;
      int x = 0;
      int y = 0;
      Image image = (Image) null;
      if (width == 140 && height == 156)
      {
        width1 = 158;
        height1 = 175;
        x = 8;
        y = 9;
        image = (Image) GFXLibrary.shieldOverlay_144x160;
      }
      else if (width == 69 && height == 77)
      {
        width1 = 81;
        height1 = 88;
        x = 4;
        y = 5;
        image = (Image) GFXLibrary.shieldOverlay_70x78;
      }
      else if (width == 47 && height == 54)
      {
        width1 = 55;
        height1 = 61;
        x = 3;
        y = 3;
        image = (Image) GFXLibrary.shieldOverlay_56x64;
      }
      else if (width == 32 && height == 36)
      {
        width1 = 37;
        height1 = 41;
        x = 2;
        y = 2;
        image = (Image) GFXLibrary.shieldOverlay_32x36;
      }
      else if (width == 25 && height == 28)
      {
        width1 = 30;
        height1 = 32;
        x = 2;
        y = 2;
        image = (Image) GFXLibrary.shieldOverlay_25x28;
      }
      if (image == null)
        return sourceImage;
      if (width != bmapWidth)
        width1 = bmapWidth;
      if (height != bmapHeight)
        height1 = bmapHeight;
      Bitmap bitmap = new Bitmap(width1, height1);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.PageUnit = GraphicsUnit.Pixel;
      if (sourceImage != null)
        graphics.DrawImage(sourceImage, x, y, new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), GraphicsUnit.Pixel);
      graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
      graphics.Dispose();
      return (Image) bitmap;
    }

    public Image getDummyShield(int width, int height)
    {
      return this.shieldOverlay((Image) Shield.RenderDummyShield(width, height), width, height, width, height);
    }

    public Image getDummyShield(int width, int height, int bmapWidth, int bmapHeight)
    {
      return this.shieldOverlay((Image) Shield.RenderDummyShield(width, height, bmapWidth, bmapHeight), width, height, bmapWidth, bmapHeight);
    }

    public int getWorldShieldTexture(int playerID) => this.getWorldShieldTexture(playerID, false);

    public int getWorldShieldTexture(int playerID, bool force)
    {
      WorldMap.ShieldTextureCacheEntry textureCacheEntry1 = (WorldMap.ShieldTextureCacheEntry) null;
      WorldMap.ShieldTextureCacheEntry textureCacheEntry2 = (WorldMap.ShieldTextureCacheEntry) null;
      if (this.worldShieldCachePlayerIDs[playerID] != null)
      {
        int shieldCachePlayerId = (int) this.worldShieldCachePlayerIDs[playerID];
        if (shieldCachePlayerId >= 0)
        {
          if (shieldCachePlayerId != 12345678)
            return this.worldShieldCache[shieldCachePlayerId].textureID;
          if (!force)
            return -1;
        }
      }
      Bitmap newBitmap;
      if (playerID == RemoteServices.Instance.UserID)
      {
        newBitmap = (Bitmap) this.getWorldShield(playerID, 32, 36, 64, 64);
        if (newBitmap == null && force)
          newBitmap = (Bitmap) this.getDummyShield(32, 36, 64, 64);
      }
      else
      {
        newBitmap = (Bitmap) this.getWorldShield(playerID, 25, 28, 32, 32);
        if (newBitmap == null && force)
          newBitmap = (Bitmap) this.getDummyShield(25, 28, 32, 32);
      }
      if (newBitmap != null)
      {
        foreach (WorldMap.ShieldTextureCacheEntry textureCacheEntry3 in this.worldShieldCache)
        {
          if (textureCacheEntry1 == null && textureCacheEntry3.playerID < -1000)
          {
            textureCacheEntry1 = textureCacheEntry3;
            break;
          }
          if (textureCacheEntry2 == null || textureCacheEntry3.lastUsage < textureCacheEntry2.lastUsage)
            textureCacheEntry2 = textureCacheEntry3;
        }
        if (textureCacheEntry1 != null)
        {
          textureCacheEntry1.playerID = playerID;
          textureCacheEntry1.lastUsage = DateTime.Now;
          textureCacheEntry1.textureID = GameEngine.Instance.GFX.loadTextureFromBitmap(newBitmap, textureCacheEntry1.textureID);
          this.worldShieldCachePlayerIDs[playerID] = (object) textureCacheEntry1.index;
          return textureCacheEntry1.textureID;
        }
        if (this.worldShieldCache.Count < 125)
        {
          WorldMap.ShieldTextureCacheEntry textureCacheEntry4 = new WorldMap.ShieldTextureCacheEntry();
          textureCacheEntry4.playerID = playerID;
          textureCacheEntry4.lastUsage = DateTime.Now;
          textureCacheEntry4.textureID = GameEngine.Instance.GFX.loadTextureFromBitmap(newBitmap);
          if (textureCacheEntry4.textureID >= 0)
          {
            textureCacheEntry4.index = this.worldShieldCache.Count;
            this.worldShieldCachePlayerIDs[playerID] = (object) this.worldShieldCache.Count;
            this.worldShieldCache.Add(textureCacheEntry4);
            return textureCacheEntry4.textureID;
          }
        }
        if (textureCacheEntry2 != null)
        {
          this.worldShieldCachePlayerIDs[textureCacheEntry2.playerID] = (object) -1;
          textureCacheEntry2.playerID = playerID;
          textureCacheEntry2.lastUsage = DateTime.Now;
          textureCacheEntry2.textureID = GameEngine.Instance.GFX.loadTextureFromBitmap(newBitmap, textureCacheEntry2.textureID);
          this.worldShieldCachePlayerIDs[playerID] = (object) textureCacheEntry2.index;
          return textureCacheEntry2.textureID;
        }
      }
      else
        this.worldShieldCachePlayerIDs[playerID] = (object) 12345678;
      return -1;
    }

    public Image renderAIShield(int AI, int width, int height, int bmapWidth, int bmapHeight)
    {
      switch (AI)
      {
        case -4:
          return this.shieldOverlay((Image) this.wolfShield.Render(width, height, bmapWidth, bmapHeight), width, height, width, height);
        case -3:
          return this.shieldOverlay((Image) this.pigShield.Render(width, height, bmapWidth, bmapHeight), width, height, width, height);
        case -2:
          return this.shieldOverlay((Image) this.snakeShield.Render(width, height, bmapWidth, bmapHeight), width, height, width, height);
        case -1:
          return this.shieldOverlay((Image) this.ratShield.Render(width, height, bmapWidth, bmapHeight), width, height, width, height);
        default:
          return (Image) null;
      }
    }

    public void clearShieldCache()
    {
      foreach (WorldMap.ShieldTextureCacheEntry textureCacheEntry in this.worldShieldCache)
        textureCacheEntry.playerID = -10000;
      this.worldShieldCachePlayerIDs.Clear();
    }

    public List<UserRelationship> UserRelations
    {
      set
      {
        if (value == null)
          return;
        this.userRelations = value;
      }
      get => this.userRelations;
    }

    public void setUserRelationship(int userID, int relationship, string username)
    {
      foreach (UserRelationship userRelation in this.userRelations)
      {
        if (userRelation.userID == userID)
        {
          if (relationship == 0)
          {
            this.userRelations.Remove(userRelation);
            return;
          }
          userRelation.friendly = relationship > 0;
          return;
        }
      }
      if (relationship == 0)
        return;
      this.userRelations.Add(new UserRelationship()
      {
        userID = userID,
        userName = username,
        friendly = relationship > 0
      });
    }

    public int getUserRelationship(int userID)
    {
      foreach (UserRelationship userRelation in this.userRelations)
      {
        if (userRelation.userID == userID)
          return userRelation.friendly ? 1 : -1;
      }
      return 0;
    }

    public UserRelationship getUserRelationshipData(int userID)
    {
      foreach (UserRelationship userRelation in this.userRelations)
      {
        if (userRelation.userID == userID)
          return userRelation;
      }
      return (UserRelationship) null;
    }

    public List<UserMarker> UserMarkers
    {
      set
      {
        if (value == null)
          return;
        this.userMarkers = value;
      }
      get => this.userMarkers;
    }

    public UserMarker getUserMarker(int userID)
    {
      foreach (UserMarker userMarker in this.userMarkers)
      {
        if (userMarker.userID == userID)
          return userMarker;
      }
      return (UserMarker) null;
    }

    public void setUserMarker(int userID, int markerType, string name)
    {
      foreach (UserMarker userMarker in this.userMarkers)
      {
        if (userMarker.userID == userID)
        {
          userMarker.markerType = markerType;
          userMarker.userName = name;
          return;
        }
      }
      this.userMarkers.Add(new UserMarker()
      {
        userID = userID,
        markerType = markerType,
        userName = name
      });
    }

    public void setLastTreasureCastleAttackTime(DateTime lastTime)
    {
      this.m_lastTreasureCastleAttackTime = lastTime;
    }

    public DateTime getLastTreasureCastleAttackTime() => this.m_lastTreasureCastleAttackTime;

    public void setTickets(int level, int number)
    {
      switch (level)
      {
        case -1:
          this.m_numQuestTickets = number;
          break;
        case 0:
          this.m_treasure1Tickets = number;
          break;
        case 1:
          this.m_treasure2Tickets = number;
          break;
        case 2:
          this.m_treasure3Tickets = number;
          break;
        case 3:
          this.m_treasure4Tickets = number;
          break;
        case 4:
          this.m_treasure5Tickets = number;
          break;
      }
    }

    public int getTickets(int level)
    {
      switch (level)
      {
        case -1:
          return this.m_numQuestTickets;
        case 0:
          return this.m_treasure1Tickets;
        case 1:
          return this.m_treasure2Tickets;
        case 2:
          return this.m_treasure3Tickets;
        case 3:
          return this.m_treasure4Tickets;
        case 4:
          return this.m_treasure5Tickets;
        default:
          return 0;
      }
    }

    public void addTickets(int level, int numberToUse)
    {
      switch (level)
      {
        case -1:
          this.m_numQuestTickets += numberToUse;
          break;
        case 0:
          this.m_treasure1Tickets += numberToUse;
          break;
        case 1:
          this.m_treasure2Tickets += numberToUse;
          break;
        case 2:
          this.m_treasure3Tickets += numberToUse;
          break;
        case 3:
          this.m_treasure4Tickets += numberToUse;
          break;
        case 4:
          this.m_treasure5Tickets += numberToUse;
          break;
      }
    }

    public void useTickets(int level, int numberToUse)
    {
      switch (level)
      {
        case -1:
          this.m_numQuestTickets -= numberToUse;
          break;
        case 0:
          this.m_treasure1Tickets -= numberToUse;
          break;
        case 1:
          this.m_treasure2Tickets -= numberToUse;
          break;
        case 2:
          this.m_treasure3Tickets -= numberToUse;
          break;
        case 3:
          this.m_treasure4Tickets -= numberToUse;
          break;
        case 4:
          this.m_treasure5Tickets -= numberToUse;
          break;
      }
    }

    public int numWheelTypesAvailable()
    {
      int num = 0;
      if (this.m_treasure1Tickets > 0)
        ++num;
      if (this.m_treasure2Tickets > 0)
        ++num;
      if (this.m_treasure3Tickets > 0)
        ++num;
      if (this.m_treasure4Tickets > 0)
        ++num;
      if (this.m_treasure5Tickets > 0)
        ++num;
      if (this.m_numQuestTickets > 0)
        ++num;
      return num;
    }

    private List<int> removeDuplicateQuests(List<int> availableQuests)
    {
      List<int> intList1 = new List<int>();
      intList1.AddRange((IEnumerable<int>) this.m_newQuestData.completedQuests);
      List<int> intList2 = new List<int>();
      foreach (int availableQuest in availableQuests)
      {
        if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
        {
          switch (availableQuest)
          {
            case 188:
            case 189:
            case 190:
            case 197:
            case 198:
              continue;
          }
        }
        if (!intList1.Contains(availableQuest) && !intList2.Contains(availableQuest) && this.m_newQuestData.questID != availableQuest)
          intList2.Add(availableQuest);
      }
      return intList2;
    }

    public void setNewQuestData(NewQuestsData data)
    {
      try
      {
        if (data.availableQuests != null || this.m_newQuestData == null)
        {
          this.m_newQuestData = data;
          List<int> availableQuests = new List<int>();
          availableQuests.AddRange((IEnumerable<int>) this.m_newQuestData.availableQuests);
          List<int> intList = this.removeDuplicateQuests(availableQuests);
          intList.Sort((Comparison<int>) ((first, second) =>
          {
            try
            {
              return NewQuests.questSortOrder[first].CompareTo(NewQuests.questSortOrder[second]);
            }
            catch (Exception ex)
            {
              return first.CompareTo(second);
            }
          }));
          this.m_newQuestData.availableQuests = intList.ToArray();
        }
        else
        {
          this.m_newQuestData.completionState = data.completionState;
          this.m_newQuestData.data = data.data;
          this.m_newQuestData.questID = data.questID;
          this.m_newQuestData.startingData = data.startingData;
          this.m_newQuestData.startTime = data.startTime;
          this.m_newQuestData.totalCompleted = data.totalCompleted;
        }
        switch (this.m_newQuestData.questID)
        {
          case 4:
          case 16:
          case 34:
          case 48:
          case 64:
          case 84:
          case 101:
          case 122:
            this.m_newQuestData.data = 1000;
            break;
        }
        QuestsHelper.questReadyToHandIn = QuestsHelper.isQuestComplete(this.m_newQuestData) || this.m_newQuestData.questID < 0 && this.m_newQuestData.totalCompleted == 0;
        bool newQuests = QuestsHelper.questReadyToHandIn && GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_QUESTS;
        InterfaceMgr.Instance.getMainTabBar().newQuestsCompleted(newQuests);
      }
      catch
      {
        UniversalDebugLog.Log("setNewQuestData had an error");
      }
    }

    public NewQuestsData getNewQuestData() => this.m_newQuestData;

    public int[] getNewQuestList()
    {
      return this.m_newQuestData != null ? this.m_newQuestData.availableQuests : (int[]) null;
    }

    public void updateLastAttackerInfo()
    {
      if (this.inUpdateLastAttackerInfo)
        return;
      this.inUpdateLastAttackerInfo = true;
      RemoteServices.Instance.set_GetLastAttacker_UserCallBack(new RemoteServices.GetLastAttacker_UserCallBack(this.getLastAttackerCallback));
      RemoteServices.Instance.GetLastAttacker();
    }

    public void getLastAttackerCallback(GetLastAttacker_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.lastAttacker = returnData.lastAttacker;
        this.newPlayer = returnData.newPlayer;
        this.lastAttackerLastUpdate = DateTime.Now;
        InterfaceMgr.Instance.ParentForm.Enabled = false;
        if (!this.newPlayer)
          GameEngine.Instance.openLostVillage(0);
        else
          GameEngine.Instance.openSimpleSelectVillage();
      }
      this.inUpdateLastAttackerInfo = false;
    }

    public void setPlaybackData(List<WorldHouseHistoryItem> items, DateTime startDate)
    {
      if (items == null)
        return;
      this.playbackItems = items;
      int num1 = 0;
      int num2 = 0;
      int num3 = (int) (VillageMap.getCurrentServerTime() - startDate).TotalDays + 1;
      int num4 = 10000000;
      foreach (WorldHouseHistoryItem houseHistoryItem in items)
      {
        if (houseHistoryItem.countryID > num1)
          num1 = houseHistoryItem.countryID;
        if (houseHistoryItem.provinceID > num2)
          num2 = houseHistoryItem.provinceID;
        int totalDays = (int) (houseHistoryItem.date - startDate).TotalDays;
        if (totalDays < num4)
          num4 = totalDays;
      }
      int num5 = num4 >= 20 ? num4 - 20 : 0;
      if (items.Count == 0)
        num5 = 0;
      this.playbackTotalDays = num3 - num5;
      if (this.playbackTotalDays <= 0 || this.playbackTotalDays > 100000)
      {
        this.playbackTotalDays = 0;
        this.playbackItems = (List<WorldHouseHistoryItem>) null;
      }
      else
      {
        this.playbackCountriesData = new int[this.playbackTotalDays, num1 + 1];
        this.playbackProvincesData = new int[this.playbackTotalDays, num2 + 1];
        this.playbackBasedDay = num5;
        foreach (WorldHouseHistoryItem houseHistoryItem in items)
        {
          int index = (int) (houseHistoryItem.date - startDate).TotalDays - num5;
          if (houseHistoryItem.countryID >= 0)
            this.playbackCountriesData[index, houseHistoryItem.countryID] = houseHistoryItem.houseID;
          if (houseHistoryItem.provinceID >= 0)
            this.playbackProvincesData[index, houseHistoryItem.provinceID] = houseHistoryItem.houseID;
        }
        this.playbackMaxCountries = num1 + 1;
        this.playbackMaxProvinces = num2 + 1;
      }
    }

    private int getPlaybackCountryHouse(int day, int countryID)
    {
      return day >= 0 && day < this.playbackTotalDays && countryID < this.playbackMaxCountries ? this.playbackCountriesData[day, countryID] : 0;
    }

    private int getPlaybackProvinceHouse(int day, int provinceID)
    {
      return day >= 0 && day < this.playbackTotalDays && provinceID < this.playbackMaxProvinces ? this.playbackProvincesData[day, provinceID] : 0;
    }

    public bool gotPlaybackData() => this.playbackItems != null;

    public void togglePlaybackPause()
    {
      this.playbackPaused = !this.playbackPaused;
      if (this.playbackPaused)
        return;
      this.playbackLastUpdateTime = DateTime.Now;
    }

    public void playbackCountries()
    {
      if (!this.gotPlaybackData())
        return;
      InterfaceMgr.Instance.getMainTabBar().changeTab(0);
      GameEngine.Instance.mainTabChange(0);
      GameEngine.Instance.World.zoomOutMax();
      InterfaceMgr.Instance.togglePlaybackBarDXActive(true);
      this.playingCountries = true;
      this.playingProvinces = false;
      this.playbackDay = 0;
      this.lastSetPlaybackDay = 0;
      this.playbackStartTime = DateTime.Now;
      this.playbackBaseTime = DateTime.Now;
      this.playbackFrameTime = DateTime.Now;
      this.playbackLastUpdateTime = DateTime.Now;
      this.playbackFrameFraction = 0.0;
      this.playbackPaused = false;
      this.playbackFrameMS = 500.0;
    }

    public void playbackProvinces()
    {
      if (!this.gotPlaybackData())
        return;
      InterfaceMgr.Instance.getMainTabBar().changeTab(0);
      GameEngine.Instance.mainTabChange(0);
      GameEngine.Instance.World.zoomOutMax();
      InterfaceMgr.Instance.togglePlaybackBarDXActive(true);
      this.playingCountries = false;
      this.playingProvinces = true;
      this.playbackDay = 0;
      this.lastSetPlaybackDay = 0;
      this.playbackStartTime = DateTime.Now;
      this.playbackBaseTime = DateTime.Now;
      this.playbackFrameTime = DateTime.Now;
      this.playbackLastUpdateTime = DateTime.Now;
      this.playbackFrameFraction = 0.0;
      this.playbackPaused = false;
      this.playbackFrameMS = 500.0;
    }

    public void setPlaybackDay(int day)
    {
      this.playbackBaseTime = DateTime.Now;
      this.playbackLastUpdateTime = DateTime.Now;
      this.lastSetPlaybackDay = day;
      this.playbackDay = day;
      this.playbackFrameFraction = 0.0;
    }

    public void updatePlaybackDay()
    {
      if (this.playbackPaused)
        return;
      double totalMilliseconds = (DateTime.Now - this.playbackLastUpdateTime).TotalMilliseconds;
      if (totalMilliseconds > 1.9 * this.playbackFrameMS)
      {
        this.playbackLastUpdateTime = DateTime.Now;
      }
      else
      {
        if (this.playbackDay < this.playbackTotalDays - 1)
        {
          this.playbackFrameFraction += totalMilliseconds / this.playbackFrameMS;
          if (this.playbackFrameFraction > 1.0)
          {
            --this.playbackFrameFraction;
            ++this.playbackDay;
          }
        }
        this.playbackLastUpdateTime = DateTime.Now;
      }
    }

    public void changePlaybackSpeed(double modifier)
    {
      this.playbackFrameMS = 500.0 / modifier;
      this.setPlaybackDay(this.playbackDay);
    }

    public double getPlaybackFrameTime() => this.playbackFrameMS;

    public void stopPlayback()
    {
      this.playingCountries = false;
      this.playingProvinces = false;
      InterfaceMgr.Instance.togglePlaybackBarDXActive(false);
    }

    public int getPlaybackDay()
    {
      return this.playingCountries || this.playingProvinces ? this.playbackDay + this.playbackBasedDay : -1;
    }

    public bool playbackActive() => this.playingCountries || this.playingProvinces;

    public void clearPlaybackData()
    {
      if (this.playbackActive())
        this.stopPlayback();
      this.playbackItems = (List<WorldHouseHistoryItem>) null;
    }

    public int countRatsCastles()
    {
      if (!(this.nextRatsCalc < DateTime.Now))
        return this.lastRatsValue;
      int num = 0;
      foreach (VillageData village in this.villageList)
      {
        if (village.special == 7 && village.visible)
          ++num;
      }
      this.lastRatsValue = num;
      this.nextRatsCalc = DateTime.Now.AddSeconds(30.0);
      return num;
    }

    public int countSnakesCastles()
    {
      if (!(this.nextSnakesCalc < DateTime.Now))
        return this.lastSnakesValue;
      int num = 0;
      foreach (VillageData village in this.villageList)
      {
        if (village.special == 9 && village.visible)
          ++num;
      }
      this.lastSnakesValue = num;
      this.nextSnakesCalc = DateTime.Now.AddSeconds(30.0);
      return num;
    }

    public int countPigsCastles()
    {
      if (!(this.nextPigsCalc < DateTime.Now))
        return this.lastPigsValue;
      int num = 0;
      foreach (VillageData village in this.villageList)
      {
        if (village.special == 11 && village.visible)
          ++num;
      }
      this.lastPigsValue = num;
      this.nextPigsCalc = DateTime.Now.AddSeconds(30.0);
      return num;
    }

    public int countWolfsCastles()
    {
      if (!(this.nextWolfsCalc < DateTime.Now))
        return this.lastWolfsValue;
      int num = 0;
      foreach (VillageData village in this.villageList)
      {
        if (village.special == 13 && village.visible)
          ++num;
      }
      this.lastWolfsValue = num;
      this.nextWolfsCalc = DateTime.Now.AddSeconds(30.0);
      return num;
    }

    public int KillStreakCount
    {
      get => this.KillStreakTimer < VillageMap.getCurrentServerTime() ? 0 : this.m_KillStreakCount;
      set => this.m_KillStreakCount = value;
    }

    public bool LinelessMaps => this.bLinelessMap && !this.overrideLinelessMap;

    public double ScreenCentreY
    {
      get => this.m_screenCentreY;
      set => this.m_screenCentreY = value;
    }

    public double ScreenCentreX
    {
      get => this.m_screenCentreX;
      set => this.m_screenCentreX = value;
    }

    public bool DrawDebugNames
    {
      get => this.drawDebugNames;
      set => this.drawDebugNames = value;
    }

    public bool DrawDebugVillageNames
    {
      get => this.drawDebugVillageNames;
      set => this.drawDebugVillageNames = value;
    }

    public static Color getVillageColor(int colourid)
    {
      if (colourid < 0)
        colourid = 0;
      if (colourid >= WorldMap.villageColorList.Length)
        colourid = 0;
      return WorldMap.villageColorList[colourid];
    }

    public void drawVillageTree(GraphicsMgr newGFX)
    {
      this.xmasPresents = HolidayPeriods.xmas(VillageMap.getCurrentServerTime()) && !GameEngine.Instance.LocalWorldData.AIWorld;
      this.pulse += 8;
      if (this.pulse > (int) byte.MaxValue)
        this.pulse -= (int) byte.MaxValue;
      this.pulseValue = this.pulse <= (int) sbyte.MaxValue ? this.pulse + (int) sbyte.MaxValue : (int) byte.MaxValue - this.pulse + (int) sbyte.MaxValue;
      Point point1 = new Point(0, 64);
      Point point2 = new Point(0, 144);
      this.gfx = newGFX;
      double width = (double) this.m_screenWidth / this.m_worldScale;
      double height = (double) this.m_screenHeight / this.m_worldScale;
      RectangleF screenRect = new RectangleF((float) (this.m_screenCentreX - width / 2.0), (float) (this.m_screenCentreY - height / 2.0), (float) width, (float) height);
      if (this.m_worldScale == 24.0)
      {
        this.GeographicalMap = true;
        this.PoliticalMap = false;
      }
      else if (this.m_worldScale >= 7.0)
      {
        if (this.m_worldScale >= 23.899999998509884 && !this.Zooming)
          this.m_worldScale = 24.0;
        this.GeographicalMap = true;
        this.PoliticalMap = true;
      }
      else
      {
        this.GeographicalMap = false;
        this.PoliticalMap = true;
      }
      this.gfx.beginSprites();
      for (int y = 0; y < this.m_screenHeight; y += 512)
      {
        for (int x = 0; x < this.m_screenWidth; x += 512)
          this.gfx.addSprite(GFXLibrary.Instance.ImageSurroundTexID2, new Rectangle(0, 0, 512, 512), (SizeF) new Size(512, 512), new PointF((float) x, (float) y));
      }
      this.gfx.drawSprites();
      this.gfx.endSprites();
      float x1 = 0.0f;
      float y1 = 0.0f;
      float worldMapWidth = (float) this.worldMapWidth;
      float worldMapHeight = (float) this.worldMapHeight;
      this.gfx.startPoly();
      this.drawSurroundBox(screenRect, Color.FromArgb(64, ARGBColors.Black), x1 + 100f, y1 + 100f, worldMapWidth + 100f, worldMapHeight + 100f);
      this.drawSurroundBox(screenRect, Color.FromArgb(64, ARGBColors.Black), x1 + 75f, y1 + 75f, worldMapWidth + 75f, worldMapHeight + 75f);
      this.drawSurroundBox(screenRect, Color.FromArgb(64, ARGBColors.Black), x1 + 50f, y1 + 50f, worldMapWidth + 50f, worldMapHeight + 50f);
      this.drawSurroundBox(screenRect, Color.FromArgb(64, ARGBColors.Black), x1 + 25f, y1 + 25f, worldMapWidth + 25f, worldMapHeight + 25f);
      this.drawSurroundBox(screenRect, Color.FromArgb(192, ARGBColors.Black), x1 - 2f, y1 - 2f, worldMapWidth + 2f, worldMapHeight + 2f);
      this.drawSurroundBox(screenRect, WorldMap.SEACOLOR, x1, y1, worldMapWidth, worldMapHeight);
      this.gfx.drawBufferedPolygons();
      if (this.playingCountries)
      {
        this.updatePlaybackDay();
        this.drawCountryPolyPlayback(screenRect);
        this.drawSeas(screenRect);
        this.drawCountryBorders(screenRect);
      }
      else if (this.playingProvinces)
      {
        this.updatePlaybackDay();
        this.drawProvincePolyPlayback(screenRect);
        this.drawSeas(screenRect);
        this.drawProvinceBorders(screenRect, true, !this.GeographicalMap);
      }
      else
      {
        if (this.GeographicalMap)
        {
          this.gfx.beginSprites();
          if (this.m_worldScale >= 3.0)
          {
            int num1 = (int) ((-64.0 * (double) screenRect.Width / (double) this.m_screenWidth + (double) screenRect.Left) * 17.0 / 64.0);
            int num2 = (int) ((-64.0 * (double) screenRect.Height / (double) this.m_screenHeight + (double) screenRect.Top) * 17.0 / 64.0);
            int num3 = (int) (((double) this.m_screenWidth * (double) screenRect.Width / (double) this.m_screenWidth + (double) screenRect.Left) * 17.0 / 64.0);
            int num4 = (int) (((double) this.m_screenHeight * (double) screenRect.Height / (double) this.m_screenHeight + (double) screenRect.Top) * 17.0 / 64.0);
            if (num1 < 0)
              num1 = 0;
            else if (num1 > this.TILEMAP_WIDTH - 1)
              num1 = this.TILEMAP_WIDTH - 1;
            if (num2 < 0)
              num2 = 0;
            else if (num2 > this.TILEMAP_HEIGHT - 1)
              num2 = this.TILEMAP_HEIGHT - 1;
            if (num3 < 0)
              num3 = 0;
            else if (num3 > this.TILEMAP_WIDTH - 1)
              num3 = this.TILEMAP_WIDTH - 1;
            if (num4 < 0)
              num4 = 0;
            else if (num4 > this.TILEMAP_HEIGHT - 1)
              num4 = this.TILEMAP_HEIGHT - 1;
            float num5 = (float) this.m_screenWidth / screenRect.Width;
            float num6 = (float) this.m_screenHeight / screenRect.Height;
            for (int index1 = num2; index1 <= num4; ++index1)
            {
              for (int index2 = num1; index2 <= num3; ++index2)
              {
                float num7 = ((float) (64.0 * (double) index2 / 17.0) - screenRect.Left) * num5;
                float num8 = ((float) (64.0 * (double) index1 / 17.0) - screenRect.Top) * num6;
                float num9 = ((float) (64.0 * ((double) index2 + 1.0) / 17.0) - screenRect.Left) * num5;
                float num10 = ((float) (64.0 * ((double) index1 + 1.0) / 17.0) - screenRect.Top) * num6;
                this.worldTileSprite.PosX = num7;
                this.worldTileSprite.PosY = num8;
                this.worldTileSprite.SpriteNo = (int) this.mapTileGrid[index2, index1];
                this.worldTileSprite.specialTileScaleAdjust(num9 - num7, num10 - num8);
                this.worldTileSprite.Update();
                this.worldTileSprite.Draw();
              }
            }
            this.gfx.drawSprites();
            for (int index3 = num2; index3 <= num4; ++index3)
            {
              for (int index4 = num1; index4 <= num3; ++index4)
              {
                if (this.tree1Grid[index4, index3] > (byte) 0)
                {
                  float num11 = ((float) (64.0 * (double) index4 / 17.0) - screenRect.Left) * num5;
                  float num12 = ((float) ((64.0 * (double) index3 - 8.0) / 17.0) - screenRect.Top) * num6;
                  double num13 = 64.0 * ((double) index4 + 1.0) / 17.0;
                  double left = (double) screenRect.Left;
                  double num14 = 64.0 * ((double) index3 + 1.0 - 8.0) / 17.0;
                  double top = (double) screenRect.Top;
                  this.worldTreeSprite.PosX = num11;
                  this.worldTreeSprite.PosY = num12;
                  this.worldTreeSprite.SpriteNo = (int) this.tree1Grid[index4, index3] - 1;
                  this.worldTreeSprite.Scale = (float) (this.m_worldScale / 23.611);
                  this.worldTreeSprite.Update();
                  this.worldTreeSprite.Draw();
                }
                if (this.tree2Grid[index4, index3] > (byte) 0)
                {
                  float num15 = ((float) (64.0 * (double) index4 / 17.0) - screenRect.Left) * num5;
                  float num16 = ((float) ((64.0 * (double) index3 - 8.0) / 17.0) - screenRect.Top) * num6;
                  double num17 = 64.0 * ((double) index4 + 1.0) / 17.0;
                  double left = (double) screenRect.Left;
                  double num18 = 64.0 * ((double) index3 + 1.0 - 8.0) / 17.0;
                  double top = (double) screenRect.Top;
                  this.worldTreeSprite.PosX = num15;
                  this.worldTreeSprite.PosY = num16;
                  this.worldTreeSprite.SpriteNo = (int) this.tree2Grid[index4, index3] - 1;
                  this.worldTreeSprite.Scale = (float) (this.m_worldScale / 23.611);
                  this.worldTreeSprite.Update();
                  this.worldTreeSprite.Draw();
                }
              }
            }
            this.gfx.drawSprites();
          }
          this.gfx.endSprites();
          this.manageDynamicLines();
          this.overrideLinelessMap = false;
          if (!this.PoliticalMap)
          {
            this.overrideLinelessMap = true;
            if (this.WorldZoom > 2.3)
            {
              if (this.WorldZoom >= 5.0)
                this.drawRegionsBorder(screenRect, true);
              this.drawCountyBorders(screenRect, true);
              this.drawCountryBorders(screenRect);
              this.drawProvinceBorders(screenRect, true, false);
              this.drawRangeCircle(screenRect);
              if (this.WorldZoom >= 13.0)
                this.drawInterVillageLines(screenRect);
            }
            else if (this.WorldZoom > 0.1)
            {
              this.drawCountryBorders(screenRect);
              this.drawProvinceBorders(screenRect, true, false);
              this.drawRangeCircle(screenRect);
            }
            else
            {
              this.drawProvinceBorders(screenRect, false, false);
              this.drawCountryBorders(screenRect);
              this.drawRangeCircle(screenRect);
            }
          }
        }
        if (this.PoliticalMap)
        {
          this.overrideLinelessMap = false;
          if (this.WorldZoom > 9.5)
            this.overrideLinelessMap = true;
          if (this.WorldZoom > 2.3)
          {
            this.drawCountyPoly(screenRect);
            this.drawSeas(screenRect);
            if (this.WorldZoom >= 5.0)
              this.drawRegions(screenRect);
            if (!this.LinelessMaps && this.WorldZoom >= 5.0)
              this.drawRegionsBorder(screenRect, false);
            this.drawCountyBorders(screenRect, true);
            this.drawCountryBorders(screenRect);
            this.drawProvinceBorders(screenRect, true, !this.GeographicalMap);
            this.drawRangeCircle(screenRect);
            if (this.WorldZoom >= 13.0)
              this.drawInterVillageLines(screenRect);
          }
          else if (this.WorldZoom > 0.1)
          {
            this.drawProvincePoly(screenRect);
            this.drawSeas(screenRect);
            this.drawCountryBorders(screenRect);
            this.drawProvinceBorders(screenRect, true, !this.GeographicalMap);
            this.drawRangeCircle(screenRect);
          }
          else
          {
            this.drawCountryPoly(screenRect);
            this.drawSeas(screenRect);
            this.drawProvinceBorders(screenRect, false, !this.GeographicalMap);
            this.drawCountryBorders(screenRect);
            this.drawRangeCircle(screenRect);
          }
          if (this.m_worldScale < 0.5)
            this.drawIslandLines(screenRect);
        }
        if (this.m_worldScale >= 23.999)
        {
          this.gfx.beginSprites();
          this.gfx.testBlending(true);
          int num19 = (int) (1000000.0 / this.m_worldScale) - (int) (this.m_screenCentreX * this.m_worldScale);
          int num20 = (int) (1000000.0 / this.m_worldScale) - (int) (this.m_screenCentreY * this.m_worldScale);
          int num21 = num19 / 512 * 512;
          int num22 = num19 - num21;
          int num23 = num20 / 512 * 512;
          int num24 = num20 - num23;
          while (num22 > 0)
            num22 -= 512;
          while (num24 > 0)
            num24 -= 512;
          for (int index5 = num24; index5 < this.m_screenHeight; index5 += 512)
          {
            for (int index6 = num22; index6 < this.m_screenWidth; index6 += 512)
            {
              this.overlaySprite.PosX = (float) index6;
              this.overlaySprite.PosY = (float) index5;
              this.overlaySprite.Update();
              this.overlaySprite.Draw();
            }
          }
          this.gfx.drawSprites();
          this.gfx.endSprites();
          this.gfx.testBlending(false);
        }
        else if (!Program.ShowSeasonalGraphics)
        {
          this.gfx.testBlending(true);
          this.gfx.startPoly();
          this.gfx.addTriangle(Color.FromArgb(251, 251, 213), 0.0f, 0.0f, (float) this.m_screenWidth, 0.0f, 0.0f, (float) this.m_screenHeight);
          this.gfx.addTriangle(Color.FromArgb(251, 251, 213), 0.0f, (float) this.m_screenHeight, (float) this.m_screenWidth, 0.0f, (float) this.m_screenWidth, (float) this.m_screenHeight);
          this.gfx.drawBufferedPolygons();
          this.gfx.testBlending(false);
        }
        this.gfx.beginSprites();
        this.drawVillages(screenRect);
        if (this.WorldZoom >= 5.5)
        {
          this.gfx.endSprites();
          this.gfx.beginSprites();
          if (InterfaceMgr.Instance.WorldMapMode != 1 && InterfaceMgr.Instance.WorldMapMode != 2)
          {
            this.gfx.setSpriteSamplerStateNone(false);
            if (!this.WorldEnded)
            {
              this.drawPeople(screenRect);
              this.drawTraders(screenRect);
              this.drawArmies(screenRect, true);
            }
            else
              this.drawFW(screenRect);
            this.gfx.setSpriteSamplerStateNone(true);
          }
        }
        else if (GameEngine.Instance.LocalWorldData.AIWorld)
        {
          this.gfx.endSprites();
          this.gfx.beginSprites();
          this.gfx.setSpriteSamplerStateNone(false);
          this.drawArmies(screenRect, false);
          this.gfx.setSpriteSamplerStateNone(true);
        }
        this.gfx.endSprites();
        this.drawText();
        this.gfx.renderLines();
        this.gfx.setSpriteSamplerStateNone(false);
        this.gfx.beginSprites();
        switch (GameEngine.Instance.clockMode)
        {
          case 1:
            this.updateClockSprite.SpriteNo = 133 + GameEngine.Instance.clockFrame;
            break;
          case 2:
          case 3:
          case 4:
          case 5:
            this.updateClockSprite.SpriteNo = 197 + GameEngine.Instance.clockFrame;
            break;
          default:
            this.updateClockSprite.SpriteNo = 69 + GameEngine.Instance.clockFrame;
            break;
        }
        this.updateClockSprite.PosX = (float) ((double) this.m_screenWidth - 80.0 + 41.0);
        this.updateClockSprite.PosY = -10f;
        this.updateClockSprite.Scale = 0.8f;
        this.updateClockSprite.Update();
        this.updateClockSprite.Draw();
        if ((this.isTutorialActive() || Program.mySettings.showGameFeaturesScreenIcon) && !this.WorldEnded)
        {
          this.tutorialOverlaySprite.TextureID = TutorialWindow.overIcon ? GFXLibrary.Instance.TutorialIconOverID : GFXLibrary.Instance.TutorialIconNormalID;
          this.tutorialOverlaySprite.PosX = 0.0f;
          this.tutorialOverlaySprite.PosY = (float) this.m_screenHeight - 64f;
          this.tutorialOverlaySprite.Update();
          this.tutorialOverlaySprite.Draw();
        }
        TimeSpan timeSpan1 = this.freeCardInfo.timeUntilNextFreeCard();
        if (timeSpan1.TotalDays > 10.0 || timeSpan1.TotalSeconds < 1.0)
        {
          this.freeCardsSprite2.SpriteNo = 4;
          this.freeCardsSprite2.PosX = (float) point1.X;
          this.freeCardsSprite2.PosY = (float) point1.Y;
          this.freeCardsSprite2.FakeWidthShrink = 0;
          this.freeCardsSprite2.ColorToUse = Color.FromArgb(Math.Max((this.pulseValue - 128) * 2, 0), ARGBColors.White);
          this.freeCardsSprite2.Center = new PointF(0.0f, 0.0f);
          this.freeCardsSprite2.defaultScaling();
          this.freeCardsSprite2.Update();
          this.freeCardsSprite2.Draw();
          this.freeCardsSprite.SpriteNo = this.overIcon ? 3 : 2;
          this.freeCardsSprite.PosX = (float) point1.X;
          this.freeCardsSprite.PosY = (float) point1.Y;
          int num = (this.pulseValue - (int) sbyte.MaxValue) / 2 + 192;
          if (num > (int) byte.MaxValue)
            num = (int) byte.MaxValue;
          this.freeCardsSprite.ColorToUse = Color.FromArgb(num, num, num);
          this.freeCardsSprite.Update();
          this.freeCardsSprite.Draw();
        }
        else
        {
          this.freeCardsSprite.SpriteNo = this.overIcon ? 1 : 0;
          this.freeCardsSprite.PosX = (float) point1.X;
          this.freeCardsSprite.PosY = (float) point1.Y;
          this.freeCardsSprite.ColorToUse = ARGBColors.White;
          this.freeCardsSprite.Update();
          this.freeCardsSprite.Draw();
          double num25 = this.freeCardInfo.durationHours();
          double num26 = timeSpan1.TotalHours / num25 * 50.0;
          this.freeCardsSprite2.ColorToUse = ARGBColors.White;
          this.freeCardsSprite2.SpriteNo = 5;
          this.freeCardsSprite2.FakeWidthShrink = (int) num26;
          this.freeCardsSprite2.PosX = (float) point1.X;
          this.freeCardsSprite2.PosY = (float) point1.Y;
          this.freeCardsSprite2.Update();
          this.freeCardsSprite2.Draw();
        }
        if (this.numWheelTypesAvailable() > 0)
        {
          this.ticketsSprite2.SpriteNo = 4;
          this.ticketsSprite2.PosX = (float) point2.X;
          this.ticketsSprite2.PosY = (float) point2.Y;
          this.ticketsSprite2.FakeWidthShrink = 0;
          this.ticketsSprite2.ColorToUse = Color.FromArgb(Math.Max((this.pulseValue - 128) * 2, 0), ARGBColors.White);
          this.ticketsSprite2.Center = new PointF(0.0f, 0.0f);
          this.ticketsSprite2.defaultScaling();
          this.ticketsSprite2.Update();
          this.ticketsSprite2.Draw();
          this.ticketsSprite2.Draw();
          this.ticketsSprite.SpriteNo = this.overTicketsIcon ? 24 : 23;
          this.ticketsSprite.PosX = 0.0f;
          this.ticketsSprite.PosY = 144f;
          int num = (this.pulseValue - (int) sbyte.MaxValue) / 2 + 192;
          if (num > (int) byte.MaxValue)
            num = (int) byte.MaxValue;
          this.ticketsSprite.ColorToUse = Color.FromArgb(num, num, num);
          this.ticketsSprite.Update();
          this.ticketsSprite.Draw();
        }
        if (GameEngine.Instance.LocalWorldData.AIWorld && this.isInWolfsRevenge())
        {
          this.wolfsRevengeSprite.SpriteNo = 30;
          this.wolfsRevengeSprite.PosX = 0.0f;
          this.wolfsRevengeSprite.PosY = 224f;
          this.wolfsRevengeSprite.ColorToUse = ARGBColors.White;
          this.wolfsRevengeSprite.Update();
          this.wolfsRevengeSprite.Draw();
          double totalHours1 = (this.wolfsRevengeEnd - this.wolfsRevengeStart).TotalHours;
          double totalHours2 = (this.wolfsRevengeEnd - VillageMap.getCurrentServerTime()).TotalHours;
          if (totalHours1 > 0.0 && totalHours2 > 0.0)
          {
            double num = totalHours2 / totalHours1 * 50.0;
            this.wolfsRevengeSprite2.ColorToUse = ARGBColors.White;
            this.wolfsRevengeSprite2.FakeWidthShrink = (int) num;
            this.wolfsRevengeSprite2.SpriteNo = 31;
            this.wolfsRevengeSprite2.PosX = 0.0f;
            this.wolfsRevengeSprite2.PosY = 224f;
            this.wolfsRevengeSprite2.Update();
            this.wolfsRevengeSprite2.Draw();
          }
        }
        if (this.isIslandWorld())
        {
          this.seaSprite.SpriteNo = 58 + this.SpecialSeaConditionsData + 4;
          this.seaSprite.PosX = (float) this.m_screenWidth - 110f;
          this.seaSprite.PosY = 0.0f;
          this.seaSprite.ColorToUse = ARGBColors.White;
          this.seaSprite.Update();
          this.seaSprite.Draw();
        }
        if (this.SeventhAgeWorld && !GameEngine.Instance.LocalWorldData.AIWorld)
        {
          this.royalTowerSprite.SpriteNo = !this.overRoyalTower ? 262 : 264;
          this.royalTowerSprite.PosX = -10f;
          this.royalTowerSprite.PosY = 239f;
          this.royalTowerSprite.ColorToUse = ARGBColors.White;
          this.royalTowerSprite.Update();
          this.royalTowerSprite.Draw();
          int num27 = this.countRemainingRoyalTowers();
          if (num27 >= 100)
          {
            int num28 = num27 / 100;
            int num29 = num27 % 100 / 10;
            int num30 = num27 % 10;
            if (num28 == 0)
              this.royalTowerSprite2.SpriteNo = 43;
            else
              this.royalTowerSprite1.SpriteNo = 33 + num28;
            this.royalTowerSprite1.PosX = -4f;
            this.royalTowerSprite1.PosY = 309f;
            this.royalTowerSprite1.ColorToUse = ARGBColors.White;
            this.royalTowerSprite1.Update();
            this.royalTowerSprite1.Draw();
            this.royalTowerSprite2.SpriteNo = num29 != 0 ? 33 + num29 : 43;
            this.royalTowerSprite2.PosX = 23f;
            this.royalTowerSprite2.PosY = 309f;
            this.royalTowerSprite2.ColorToUse = ARGBColors.White;
            this.royalTowerSprite2.Update();
            this.royalTowerSprite2.Draw();
            this.royalTowerSprite3.SpriteNo = num30 != 0 ? 33 + num30 : 43;
            this.royalTowerSprite3.PosX = 50f;
            this.royalTowerSprite3.PosY = 309f;
            this.royalTowerSprite3.ColorToUse = ARGBColors.White;
            this.royalTowerSprite3.Update();
            this.royalTowerSprite3.Draw();
          }
          else if (num27 >= 10)
          {
            int num31 = num27 / 10;
            int num32 = num27 % 10;
            if (num31 == 0)
              this.royalTowerSprite2.SpriteNo = 43;
            else
              this.royalTowerSprite1.SpriteNo = 33 + num31;
            this.royalTowerSprite1.PosX = 6f;
            this.royalTowerSprite1.PosY = 309f;
            this.royalTowerSprite1.ColorToUse = ARGBColors.White;
            this.royalTowerSprite1.Update();
            this.royalTowerSprite1.Draw();
            this.royalTowerSprite2.SpriteNo = num32 != 0 ? 33 + num32 : 43;
            this.royalTowerSprite2.PosX = 36f;
            this.royalTowerSprite2.PosY = 309f;
            this.royalTowerSprite2.ColorToUse = ARGBColors.White;
            this.royalTowerSprite2.Update();
            this.royalTowerSprite2.Draw();
          }
          else
          {
            this.royalTowerSprite1.SpriteNo = num27 != 0 ? 33 + num27 : 43;
            this.royalTowerSprite1.PosX = 23f;
            this.royalTowerSprite1.PosY = 309f;
            this.royalTowerSprite1.ColorToUse = ARGBColors.White;
            this.royalTowerSprite1.Update();
            this.royalTowerSprite1.Draw();
          }
        }
        DateTime dateTime1 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.saleStartTime);
        DateTime dateTime2 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.saleEndTime);
        bool flag1 = false;
        if (dateTime1 <= VillageMap.getCurrentServerTime() && dateTime2 > VillageMap.getCurrentServerTime())
        {
          flag1 = true;
          TimeSpan timeSpan2 = dateTime2 - VillageMap.getCurrentServerTime();
          this.saleSprite.ColorToUse = ARGBColors.White;
          this.saleSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
          int num33 = 0;
          if (!this.overSale)
          {
            this.saleSprite.SpriteNo = 32;
          }
          else
          {
            num33 = 12;
            this.saleSprite.SpriteNo = 33;
          }
          this.saleSprite.PosX = (float) this.m_screenWidth - 140f;
          this.saleSprite.PosY = timeSpan2.Hours >= 24 ? (float) this.m_screenHeight - 130f : (float) this.m_screenHeight - 160f;
          this.saleSprite.Update();
          this.saleSprite.Draw();
          bool flag2 = GameEngine.Instance.World.salePercentage - 99 > 0;
          int salePercentage = GameEngine.Instance.World.salePercentage;
          this.saleDigits[0].SpriteNo = 44 + num33;
          this.saleDigits[this.saleDigits.Count - 1].SpriteNo = 45 + num33;
          List<int> intList1 = new List<int>()
          {
            salePercentage / (flag2 ? 100 : 10)
          };
          intList1.Add((salePercentage - intList1[0] * (flag2 ? 100 : 10)) / (flag2 ? 10 : 1));
          if (flag2)
            intList1.Add(flag2 ? salePercentage - intList1[0] * 100 - intList1[1] * 10 : 0);
          for (int index = 0; index < intList1.Count; ++index)
          {
            int num34 = intList1[index];
            this.saleDigits[index + 1].SpriteNo = num34 != 0 ? 33 + num34 + num33 : 43 + num33;
          }
          float num35 = this.saleSprite.PosX + (flag2 ? -20f : 10f);
          foreach (SpriteWrapper saleDigit in this.saleDigits)
          {
            saleDigit.PosX = num35;
            saleDigit.PosY = this.saleSprite.PosY + 70f;
            saleDigit.Update();
            saleDigit.Draw();
            num35 += 20f;
          }
          if (timeSpan2.TotalHours < 24.0)
          {
            List<int> intList2 = new List<int>();
            int hours = timeSpan2.Hours;
            int minutes = timeSpan2.Minutes;
            if (hours < 10)
              intList2.Add(-1);
            else
              intList2.Add((hours - hours % 10) / 10);
            if (hours > 0)
              intList2.Add(hours % 10);
            else
              intList2.Add(0);
            intList2.Add(0);
            if (minutes < 10)
              intList2.Add(0);
            else
              intList2.Add((minutes - minutes % 10) / 10);
            if (minutes > 0)
              intList2.Add(minutes % 10);
            else
              intList2.Add(0);
            for (int index = 0; index < 5; ++index)
            {
              int num36 = intList2[index];
              this.saleTimer[index].SpriteNo = index != 2 ? (num36 != 0 ? 33 + num36 : 43) : 271;
            }
            float num37 = this.saleSprite.PosX + 20f;
            for (int index = 0; index < 5; ++index)
            {
              if (intList2[index] > -1)
              {
                if (index == 2)
                {
                  this.saleTimer[index].PosX = num37 + 2f;
                  num37 += 13f;
                }
                else
                {
                  this.saleTimer[index].PosX = num37;
                  num37 += 18f;
                }
                this.saleTimer[index].PosY = (float) ((double) this.saleSprite.PosY + (double) this.saleSprite.Height + 5.0);
                this.saleTimer[index].Update();
                this.saleTimer[index].Draw();
                this.saleTimer[index].Scale = 0.8f;
              }
            }
            this.saleClock.TextureID = GFXLibrary.Instance.FreeCardIconsID;
            this.saleClock.SpriteNo = 270;
            this.saleClock.PosX = this.saleSprite.PosX - 10f;
            this.saleClock.PosY = (float) ((double) this.saleSprite.PosY + (double) this.saleSprite.Height - 3.0);
            this.saleClock.Scale = 0.8f;
            this.saleClock.Update();
            this.saleClock.Draw();
          }
        }
        if (GameEngine.Instance.cardsManager.PremiumOfferAvailable())
        {
          this.offerSprite.ColorToUse = ARGBColors.White;
          this.offerSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
          this.offerSprite.SpriteNo = this.overOffer ? 273 : 272;
          this.offerSprite.PosX = (float) this.m_screenWidth - 140f;
          this.offerSprite.PosY = !flag1 ? (float) this.m_screenHeight - 160f : (float) ((double) this.m_screenHeight - 160.0 - 160.0);
          this.offerSprite.Update();
          this.offerSprite.Draw();
          TimeSpan timeSpan3 = TimeSpan.Zero;
          foreach (PremiumOfferData premiumOffer in GameEngine.Instance.cardsManager.PremiumOffers)
          {
            TimeSpan timeSpan4 = premiumOffer.ExpirationDate - VillageMap.getCurrentServerTime();
            if (timeSpan4 > timeSpan3)
              timeSpan3 = timeSpan4;
          }
          if (timeSpan3.TotalSeconds > 0.0)
          {
            float posX = this.offerSprite.PosX;
            List<int> intList = new List<int>();
            int days = timeSpan3.Days;
            int hours = timeSpan3.Hours;
            int minutes = timeSpan3.Minutes;
            if (days < 10)
              intList.Add(-1);
            else
              intList.Add((days - days % 10) / 10);
            if (days > 0)
            {
              intList.Add(days % 10);
              intList.Add(0);
            }
            else
            {
              intList.Add(-1);
              intList.Add(-1);
            }
            if (hours < 10)
              intList.Add(0);
            else
              intList.Add((hours - hours % 10) / 10);
            if (hours > 0)
              intList.Add(hours % 10);
            else
              intList.Add(0);
            intList.Add(0);
            if (minutes < 10)
              intList.Add(0);
            else
              intList.Add((minutes - minutes % 10) / 10);
            if (minutes > 0)
              intList.Add(minutes % 10);
            else
              intList.Add(0);
            for (int index = 0; index < 8; ++index)
            {
              int num = intList[index];
              this.offerTimer[index].SpriteNo = index == 2 || index == 5 ? 271 : (num != 0 ? 33 + num : 43);
            }
            for (int index = 0; index < 8; ++index)
            {
              if (intList[index] > -1)
              {
                if (index == 2 || index == 5)
                {
                  this.offerTimer[index].PosX = posX + 2f;
                  posX += 13f;
                }
                else
                {
                  this.offerTimer[index].PosX = posX;
                  posX += 18f;
                }
                this.offerTimer[index].PosY = (float) ((double) this.offerSprite.PosY + (double) this.offerSprite.Height + 5.0);
                this.offerTimer[index].Update();
                this.offerTimer[index].Draw();
                this.offerTimer[index].Scale = 0.8f;
              }
            }
            this.offerClock.TextureID = GFXLibrary.Instance.FreeCardIconsID;
            this.offerClock.SpriteNo = 270;
            this.offerClock.PosX = days <= 9 ? this.offerSprite.PosX - 30f : this.offerSprite.PosX - 50f;
            this.offerClock.PosY = (float) ((double) this.offerSprite.PosY + (double) this.offerSprite.Height - 3.0);
            this.offerClock.Scale = 0.8f;
            this.offerClock.Update();
            this.offerClock.Draw();
          }
        }
        bool flag3 = false;
        if (GameEngine.Instance.World.pendingPrizes.Count > 0)
        {
          this.contestSprite.ColorToUse = Color.FromArgb(this.contestPulseValue, this.contestPulseValue, this.contestPulseValue);
          this.contestPulseValue += this.contestPulse;
          if (this.contestPulseValue >= (int) byte.MaxValue)
            this.contestPulse = -3;
          else if (this.contestPulseValue <= 200)
            this.contestPulse = 3;
          this.contestSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
          this.contestSprite.SpriteNo = this.overContest ? 268 : 267;
          flag3 = true;
        }
        else if (GameEngine.Instance.World.contestID >= 0)
        {
          DateTime dateTime3 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.contestStartTime);
          DateTime dateTime4 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.contestEndTime);
          if (dateTime3 <= VillageMap.getCurrentServerTime() && dateTime4 > VillageMap.getCurrentServerTime())
          {
            if (Program.mySettings.LastContestViewed != GameEngine.Instance.World.contestID)
            {
              this.contestSprite.ColorToUse = Color.FromArgb(this.contestPulseValue, this.contestPulseValue, this.contestPulseValue);
              this.contestPulseValue += this.contestPulse;
              if (this.contestPulseValue >= (int) byte.MaxValue)
                this.contestPulse = -3;
              else if (this.contestPulseValue <= 200)
                this.contestPulse = 3;
            }
            else
              this.contestSprite.ColorToUse = ARGBColors.White;
            this.contestSprite.SpriteNo = this.overContest ? 266 : 265;
            flag3 = true;
          }
        }
        if (flag3)
        {
          this.contestSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
          this.contestSprite.PosX = 0.0f;
          this.contestSprite.PosY = this.isTutorialActive() || Program.mySettings.showGameFeaturesScreenIcon ? (float) ((double) this.m_screenHeight - 130.0 - 64.0 + 32.0) : (float) ((double) this.m_screenHeight - 130.0 + 32.0);
          this.contestSprite.Update();
          this.contestSprite.Scale = 0.75f;
          this.contestSprite.Draw();
        }
        if (this.overWikiHelp)
          this.gfx.addSprite(GFXLibrary.Instance.WikiHelpIconOver, new Rectangle(0, 0, 64, 64), (SizeF) new Size(40, 40), new PointF((float) (this.m_screenWidth - 80 + 41 + 11), 32f));
        else
          this.gfx.addSprite(GFXLibrary.Instance.WikiHelpIconNormal, new Rectangle(0, 0, 64, 64), (SizeF) new Size(40, 40), new PointF((float) (this.m_screenWidth - 80 + 41 + 11), 32f));
        this.gfx.drawSprites();
        this.gfx.endSprites();
        this.gfx.setSpriteSamplerStateNone(true);
      }
    }

    public void freeCardTooltip(Point dxMousePos)
    {
      this.overIcon = false;
      if (dxMousePos.X < 70 && dxMousePos.Y >= 64 && dxMousePos.Y < 134)
      {
        this.overIcon = true;
        CustomTooltipManager.MouseEnterTooltipArea(10500);
      }
      this.overTicketsIcon = false;
      if (dxMousePos.X < 70 && dxMousePos.Y >= 144 && dxMousePos.Y < 214 && this.numWheelTypesAvailable() > 0)
      {
        this.overTicketsIcon = true;
        CustomTooltipManager.MouseEnterTooltipArea(10501);
      }
      this.overWikiHelp = false;
      if (dxMousePos.X > this.m_screenWidth - 30 && dxMousePos.Y > 30 && dxMousePos.Y < 60)
      {
        this.overWikiHelp = true;
        CustomTooltipManager.MouseEnterTooltipArea(4400, 0);
      }
      this.overWolf = false;
      this.overRoyalTower = false;
      if (GameEngine.Instance.LocalWorldData.AIWorld)
      {
        if (dxMousePos.X < 70 && dxMousePos.Y >= 224 && dxMousePos.Y < 294 && this.isInWolfsRevenge())
        {
          this.overWolf = true;
          CustomTooltipManager.MouseEnterTooltipArea(10502, (int) (this.wolfsRevengeEnd - VillageMap.getCurrentServerTime()).TotalSeconds);
        }
      }
      else if (this.SeventhAgeWorld && !GameEngine.Instance.LocalWorldData.AIWorld && dxMousePos.X < 90 && dxMousePos.Y >= 262 && dxMousePos.Y < 342)
      {
        this.overRoyalTower = true;
        CustomTooltipManager.MouseEnterTooltipArea(24000);
      }
      this.overSale = false;
      DateTime dateTime1 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.saleStartTime);
      DateTime dateTime2 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.saleEndTime);
      bool flag = dateTime1 <= VillageMap.getCurrentServerTime() && dateTime2 > VillageMap.getCurrentServerTime();
      if (dxMousePos.X > this.m_screenWidth - 140 && dxMousePos.Y > this.m_screenHeight - 130 && flag)
      {
        CustomTooltipManager.MouseEnterTooltipArea(25100, (int) (dateTime2 - VillageMap.getCurrentServerTime()).TotalSeconds);
        this.overSale = true;
      }
      this.overOffer = false;
      if (GameEngine.Instance.cardsManager.PremiumOfferAvailable())
      {
        int num = this.m_screenHeight - 130;
        if (flag)
          num = this.m_screenHeight - 130 - 160;
        if (dxMousePos.X > this.m_screenWidth - 140 && dxMousePos.Y > num && dxMousePos.Y < num + 130)
        {
          this.overOffer = true;
          CustomTooltipManager.MouseEnterTooltipArea(25101);
        }
      }
      if (dxMousePos.X > this.m_screenWidth - 110 && dxMousePos.X < this.m_screenWidth - 40 && dxMousePos.Y < 85 && this.isIslandWorld())
        CustomTooltipManager.MouseEnterTooltipArea(23010 + (this.SpecialSeaConditionsData + 4), 0);
      this.overContest = false;
      if ((double) dxMousePos.X >= (double) this.contestSprite.Width || (double) dxMousePos.Y <= (double) this.contestSprite.PosY || (double) dxMousePos.Y >= (double) this.contestSprite.PosY + (double) this.contestSprite.Height)
        return;
      if (GameEngine.Instance.World.pendingPrizes.Count > 0)
      {
        CustomTooltipManager.MouseEnterTooltipArea(25001, GameEngine.Instance.World.pendingPrizes.Count);
        this.overContest = true;
      }
      else
      {
        if (GameEngine.Instance.World.contestID < 0)
          return;
        DateTime dateTime3 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.contestStartTime);
        DateTime dateTime4 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.contestEndTime);
        if (!(dateTime3 <= VillageMap.getCurrentServerTime()) || !(dateTime4 > VillageMap.getCurrentServerTime()))
          return;
        this.overContest = true;
        CustomTooltipManager.MouseEnterTooltipArea(25002, (int) (dateTime4 - VillageMap.getCurrentServerTime()).TotalSeconds);
      }
    }

    private bool shouldDrawMapIcon(VillageData village)
    {
      if (!this.worldMapFilter.showVillage(village))
        return false;
      if (InterfaceMgr.Instance.WorldMapMode != 0)
      {
        if (InterfaceMgr.Instance.WorldMapMode == 1 && (this.isSpecial(village.id) || !village.Capital && village.userID < 0) || InterfaceMgr.Instance.WorldMapMode == 2 && (!village.Capital && (village.id != InterfaceMgr.Instance.StockExchangeBuyingVillage || InterfaceMgr.Instance.StockExchangeBuyingVillage < 0) || village.id != InterfaceMgr.Instance.StockExchangeBuyingVillage && !this.allowExchangeTrade(village.id, InterfaceMgr.Instance.StockExchangeBuyingVillage)) || (InterfaceMgr.Instance.WorldMapMode == 5 || InterfaceMgr.Instance.WorldMapMode == 7) && village.Capital)
          return false;
        if (InterfaceMgr.Instance.WorldMapMode == 3)
        {
          if (!GameEngine.Instance.LocalWorldData.IsHereticEUAIWorld)
          {
            if (this.isSpecial(village.id) && !this.isAttackableSpecial(village.id) || !this.isSpecial(village.id) && village.userID < 0 && !village.Capital)
              return false;
          }
          else if (this.isHeretic())
          {
            if (this.isSpecial(village.id))
            {
              switch (village.special)
              {
                case 3:
                case 5:
                case 15:
                case 17:
                  return true;
                default:
                  return SpecialVillageTypes.IS_TREASURE_CASTLE(village.special) || SpecialVillageTypes.IS_ROYAL_TOWER(village.special);
              }
            }
            else
              return village.userID > 4 && village.factionID != 4;
          }
          else
          {
            if (this.isSpecial(village.id))
            {
              if (this.isAttackableSpecial(village.id))
                return true;
            }
            else if (village.userID <= 4)
              return true;
            return village.factionID == 4;
          }
        }
        if (InterfaceMgr.Instance.WorldMapMode == 7 && (this.isSpecial(village.id) && !this.isAttackableSpecial(village.id) || !this.isSpecial(village.id) && village.userID < 0) || InterfaceMgr.Instance.WorldMapMode == 5 && (this.isSpecial(village.id) || village.Capital || village.userID < 0) || InterfaceMgr.Instance.WorldMapMode == 6 && (this.isSpecial(village.id) || village.Capital))
          return false;
        if (InterfaceMgr.Instance.WorldMapMode == 4)
        {
          if (!GameEngine.Instance.LocalWorldData.IsHereticEUAIWorld)
          {
            if (this.isSpecial(village.id) && !this.isScoutableSpecial(village.id) || !this.isSpecial(village.id) && village.userID < 0 && !this.isCapital(village.id))
              return false;
          }
          else
          {
            if (this.isHeretic())
            {
              if (this.isSpecial(village.id))
              {
                if (this.isScoutableSpecial(village.id))
                {
                  switch (village.special)
                  {
                    case 7:
                    case 9:
                    case 11:
                    case 13:
                      return false;
                  }
                }
                return true;
              }
              return village.userID > 4 && village.factionID != 4;
            }
            if (this.isSpecial(village.id))
            {
              if (this.isScoutableSpecial(village.id))
                return true;
            }
            else if (village.userID <= 4)
              return true;
            return village.factionID == 4;
          }
        }
        if (InterfaceMgr.Instance.WorldMapMode == 9 && (this.isSpecial(village.id) || village.userID < 0 && !village.Capital))
          return false;
      }
      return true;
    }

    private void drawPlayerVillageList(ref WorldMap.FastScreenRect fRect)
    {
      foreach (WorldMap.UserVillageData userVillage in this.m_userVillages)
      {
        if (!userVillage.capital)
        {
          VillageData village = this.villageList[userVillage.villageID];
          if (this.mapIcon == null)
            this.mapIcon = new MapIconDrawCall(this.gfx, this.villageSprite, this.m_worldZoomInverted, this.m_worldScale, this.mapEditing, new Size(this.m_screenWidth, this.m_screenHeight), this.pulse, this.pulseValue, this.xmasPresents);
          float xPos = ((float) village.x - fRect.Left) / fRect.Width;
          float yPos = ((float) village.y - fRect.Top) / fRect.Height;
          if ((double) xPos >= -0.10000000149011612 && (double) xPos <= 1.1000000238418579 && (double) yPos >= -0.10000000149011612 && (double) yPos <= 1.1000000238418579 && this.shouldDrawMapIcon(village))
            this.mapIcon.draw(village, (double) xPos, (double) yPos);
        }
      }
    }

    private void drawVillages(RectangleF screenRect)
    {
      WorldMap.FastScreenRect fRect = new WorldMap.FastScreenRect();
      fRect.left = (int) screenRect.Left;
      fRect.top = (int) screenRect.Top;
      fRect.Left = screenRect.Left;
      fRect.Top = screenRect.Top;
      fRect.Width = screenRect.Width;
      fRect.Height = screenRect.Height;
      fRect.right = (int) ((double) screenRect.Right + 0.99900001287460327);
      fRect.bottom = (int) ((double) screenRect.Bottom + 0.99900001287460327);
      fRect.zoomLevel = this.WorldZoom;
      this.m_baseNode.drawVillages(fRect);
      if (fRect.zoomLevel >= 8.0 || this.m_userVillages == null || this.PickingStartCounty)
        return;
      this.drawPlayerVillageList(ref fRect);
    }

    public PointF getDrawPosition(double displayX, double displayY)
    {
      double width = (double) this.m_screenWidth / this.m_worldScale;
      double height = (double) this.m_screenHeight / this.m_worldScale;
      RectangleF rectangleF = new RectangleF((float) (this.m_screenCentreX - width / 2.0), (float) (this.m_screenCentreY - height / 2.0), (float) width, (float) height);
      WorldMap.FastScreenRect fastScreenRect = new WorldMap.FastScreenRect();
      fastScreenRect.left = (int) rectangleF.Left;
      fastScreenRect.top = (int) rectangleF.Top;
      fastScreenRect.Left = rectangleF.Left;
      fastScreenRect.Top = rectangleF.Top;
      fastScreenRect.Width = rectangleF.Width;
      fastScreenRect.Height = rectangleF.Height;
      fastScreenRect.right = (int) ((double) rectangleF.Right + 0.99900001287460327);
      fastScreenRect.bottom = (int) ((double) rectangleF.Bottom + 0.99900001287460327);
      fastScreenRect.zoomLevel = this.WorldZoom;
      float num1 = ((float) displayX - fastScreenRect.Left) / fastScreenRect.Width;
      float num2 = ((float) displayY - fastScreenRect.Top) / fastScreenRect.Height;
      return new PointF()
      {
        X = num1 * (float) this.m_screenWidth,
        Y = num2 * (float) this.m_screenHeight
      };
    }

    public PointF getPersonDrawPosition(long personID)
    {
      WorldMap.LocalPerson person = this.getPerson(personID);
      return this.getDrawPosition(person.displayX, person.displayY);
    }

    public PointF getTraderDrawPosition(long traderID)
    {
      WorldMap.LocalTrader trader = this.getTrader(traderID);
      return this.getDrawPosition(trader.displayX, trader.displayY);
    }

    public PointF getArmyDrawPosition(long armyID)
    {
      WorldMap.LocalArmyData armyById = this.GetArmyByID(armyID);
      return armyById == null ? PointF.Empty : this.getDrawPosition(armyById.displayX, armyById.displayY);
    }

    public PointF getVillageDrawPosition(int villageid)
    {
      double width = (double) this.m_screenWidth / this.m_worldScale;
      double height = (double) this.m_screenHeight / this.m_worldScale;
      RectangleF rectangleF = new RectangleF((float) (this.m_screenCentreX - width / 2.0), (float) (this.m_screenCentreY - height / 2.0), (float) width, (float) height);
      WorldMap.FastScreenRect fastScreenRect = new WorldMap.FastScreenRect();
      fastScreenRect.left = (int) rectangleF.Left;
      fastScreenRect.top = (int) rectangleF.Top;
      fastScreenRect.Left = rectangleF.Left;
      fastScreenRect.Top = rectangleF.Top;
      fastScreenRect.Width = rectangleF.Width;
      fastScreenRect.Height = rectangleF.Height;
      fastScreenRect.right = (int) ((double) rectangleF.Right + 0.99900001287460327);
      fastScreenRect.bottom = (int) ((double) rectangleF.Bottom + 0.99900001287460327);
      fastScreenRect.zoomLevel = this.WorldZoom;
      if (villageid < 0 || this.villageList == null || villageid >= this.villageList.Length)
        return PointF.Empty;
      VillageData village = this.villageList[villageid];
      float num1 = ((float) village.x - fastScreenRect.Left) / fastScreenRect.Width;
      float num2 = ((float) village.y - fastScreenRect.Top) / fastScreenRect.Height;
      return new PointF()
      {
        X = num1 * (float) this.m_screenWidth,
        Y = num2 * (float) this.m_screenHeight
      };
    }

    public PointF getDrawPosition(PointF worldMapPosition)
    {
      double width = (double) this.m_screenWidth / this.m_worldScale;
      double height = (double) this.m_screenHeight / this.m_worldScale;
      RectangleF rectangleF = new RectangleF((float) (this.m_screenCentreX - width / 2.0), (float) (this.m_screenCentreY - height / 2.0), (float) width, (float) height);
      WorldMap.FastScreenRect fastScreenRect = new WorldMap.FastScreenRect();
      fastScreenRect.left = (int) rectangleF.Left;
      fastScreenRect.top = (int) rectangleF.Top;
      fastScreenRect.Left = rectangleF.Left;
      fastScreenRect.Top = rectangleF.Top;
      fastScreenRect.Width = rectangleF.Width;
      fastScreenRect.Height = rectangleF.Height;
      fastScreenRect.right = (int) ((double) rectangleF.Right + 0.99900001287460327);
      fastScreenRect.bottom = (int) ((double) rectangleF.Bottom + 0.99900001287460327);
      fastScreenRect.zoomLevel = this.WorldZoom;
      float num1 = (worldMapPosition.X - fastScreenRect.Left) / fastScreenRect.Width;
      float num2 = (worldMapPosition.Y - fastScreenRect.Top) / fastScreenRect.Height;
      return new PointF()
      {
        X = num1 * (float) this.m_screenWidth,
        Y = num2 * (float) this.m_screenHeight
      };
    }

    public PointF pixelAlignPoint(float x, float y)
    {
      return new PointF((float) Math.Round((double) x), (float) Math.Round((double) y));
    }

    public void showShieldUser(int userID) => this.m_userInfoShieldRolloverUserID = userID;

    public int getVillageColour(int factionID) => this.getHouse(factionID);

    public int getVillageColour(WorldMap.WorldPointList wpl)
    {
      if (!this.PickingStartCounty)
        return this.getHouse(wpl.factionID);
      int countyFromVillageId = GameEngine.Instance.World.getCountyFromVillageID(wpl.capitalVillage);
      if (GameEngine.Instance.World.AvailableCounties == null || !GameEngine.Instance.World.AvailableCounties.ContainsKey(countyFromVillageId))
        return 0;
      WorldMap.AvailableCounty availableCounty = this.AvailableCounties[countyFromVillageId];
      if (availableCounty.available <= 0)
        return 1;
      if (availableCounty.available > availableCounty.total / 2)
        return 0;
      return availableCounty.available > availableCounty.total / 7 ? 3 : 2;
    }

    public int getHouse(int factionID)
    {
      if (factionID < 0)
        return 0;
      try
      {
        FactionData factionData = (FactionData) this.m_factionData[factionID];
        return factionData != null && factionData.houseID >= 0 && factionData.houseID <= 20 ? factionData.houseID : 0;
      }
      catch (Exception ex)
      {
        return 0;
      }
    }

    private void experimentalStuff(string mapName)
    {
      int[] numArray1 = (int[]) null;
      int[] numArray2 = (int[]) null;
      int[] numArray3 = (int[]) null;
      int num1 = 4;
      switch (mapName)
      {
        case "uk.wmpdata":
          numArray1 = this.ukCountyColour;
          numArray2 = this.ukProvinceColour;
          numArray3 = this.ukCountryColour;
          break;
        case "de.wmpdata":
          numArray1 = this.deCountyColour;
          numArray2 = this.deProvinceColour;
          numArray3 = this.deCountryColour;
          break;
        case "fr.wmpdata":
          numArray1 = this.frCountyColour;
          numArray2 = this.frProvinceColour;
          numArray3 = this.frCountryColour;
          break;
        case "ru.wmpdata":
          numArray1 = this.ruCountyColour;
          numArray2 = this.ruProvinceColour;
          numArray3 = this.ruCountryColour;
          break;
        case "es.wmpdata":
          numArray1 = this.esCountyColour;
          numArray2 = this.esProvinceColour;
          numArray3 = this.esCountryColour;
          break;
        case "eu.wmpdata":
          numArray1 = this.euCountyColour;
          numArray2 = this.euProvinceColour;
          numArray3 = this.euCountryColour;
          break;
        case "it.wmpdata":
          numArray1 = this.itCountyColour;
          numArray2 = this.itProvinceColour;
          numArray3 = this.itCountryColour;
          break;
        case "pl.wmpdata":
          numArray1 = this.plCountyColour;
          numArray2 = this.plProvinceColour;
          numArray3 = this.plCountryColour;
          break;
        case "sa.wmpdata":
          numArray1 = this.saCountyColour;
          numArray2 = this.saProvinceColour;
          numArray3 = this.saCountryColour;
          break;
        case "tr.wmpdata":
          numArray1 = this.trCountyColour;
          numArray2 = this.trProvinceColour;
          numArray3 = this.trCountryColour;
          break;
        case "uk2.wmpdata":
          numArray1 = this.uk2CountyColour;
          numArray2 = this.uk2ProvinceColour;
          numArray3 = this.uk2CountryColour;
          break;
        case "us.wmpdata":
          numArray1 = this.usCountyColour;
          numArray2 = this.usProvinceColour;
          numArray3 = this.usCountryColour;
          break;
        case "wrld.wmpdata":
          numArray1 = this.gcCountyColour;
          numArray2 = this.gcProvinceColour;
          numArray3 = this.gcCountryColour;
          break;
        case "ph.wmpdata":
          numArray1 = this.phCountyColour;
          numArray2 = this.phProvinceColour;
          numArray3 = this.phCountryColour;
          break;
        case "ch.wmpdata":
          numArray1 = this.chCountyColour;
          numArray2 = this.chProvinceColour;
          numArray3 = this.chCountryColour;
          break;
        case "king.wmpdata":
          numArray1 = this.kgCountyColour;
          numArray2 = this.kgProvinceColour;
          numArray3 = this.kgCountryColour;
          num1 = 5;
          break;
        case "jpkr.wmpdata":
          numArray1 = this.jpCountyColour;
          numArray2 = this.jpProvinceColour;
          numArray3 = this.jpCountryColour;
          break;
        case "hyw.wmpdata":
          numArray1 = this.hywCountyColour;
          numArray2 = this.hywProvinceColour;
          numArray3 = this.hywCountryColour;
          break;
        case "vk.wmpdata":
          numArray1 = this.vkCountyColour;
          numArray2 = this.vkProvinceColour;
          numArray3 = this.vkCountryColour;
          break;
        case "tgd.wmpdata":
          numArray1 = this.tgdCountyColour;
          numArray2 = this.tgdProvinceColour;
          numArray3 = this.tgdCountryColour;
          break;
        case "cru.wmpdata":
          numArray1 = this.cruCountyColour;
          numArray2 = this.cruProvinceColour;
          numArray3 = this.cruCountryColour;
          break;
        case "sparta.wmpdata":
          numArray1 = this.spartaCountyColour;
          numArray2 = this.spartaProvinceColour;
          numArray3 = this.spartaCountryColour;
          break;
      }
      if (numArray1 != null)
      {
        int num2 = 0;
        foreach (WorldMap.WorldPointList county in this.countyList)
          county.experimentalColourVariant = this.experimentalColourRemapping[numArray1[num2++]];
      }
      if (numArray2 != null)
      {
        int num3 = 0;
        foreach (WorldMap.WorldPointList provinces in this.provincesList)
          provinces.experimentalColourVariant = this.experimentalColourRemapping[numArray2[num3++]];
      }
      if (numArray1 != null)
      {
        int num4 = 0;
        foreach (WorldMap.WorldPointList country in this.countryList)
          country.experimentalColourVariant = this.experimentalColourRemapping[numArray3[num4++]];
      }
      foreach (WorldMap.WorldPointList region in this.regionList)
      {
        int num5 = this.countyList[region.parentID].experimentalColourVariant + 2;
        if (num5 >= num1)
          num5 -= num1;
        region.experimentalColourVariant = num5;
      }
    }

    public Color getAreaColour(int areaCol, WorldMap.WorldPointList wpl)
    {
      if (this.PickingStartCounty && this.getCountyFromVillageID(this.LastClickedVillage) == this.getCountyFromVillageID(wpl.capitalVillage) && wpl.capitalVillage >= 0)
        return ARGBColors.White;
      Color areaColor = WorldMap.areaColorList[areaCol];
      if (!this.bLinelessMap || wpl.experimentalColourVariant <= 0)
        return areaColor;
      int num1 = 2;
      switch (areaCol)
      {
        case 0:
          num1 = 2;
          break;
        case 1:
          num1 = 5;
          break;
        case 2:
          num1 = 4;
          break;
        case 3:
          num1 = 4;
          break;
        case 4:
          num1 = 5;
          break;
        case 5:
          num1 = 4;
          break;
        case 6:
          num1 = 8;
          break;
        case 7:
          num1 = 4;
          break;
        case 8:
          num1 = 4;
          break;
        case 9:
          num1 = 3;
          break;
        case 10:
          num1 = 8;
          break;
        case 11:
          num1 = 4;
          break;
        case 12:
          num1 = 4;
          break;
        case 13:
          num1 = 4;
          break;
        case 14:
          num1 = 5;
          break;
        case 15:
          num1 = 4;
          break;
        case 16:
          num1 = 5;
          break;
        case 17:
          num1 = 4;
          break;
        case 18:
          num1 = 3;
          break;
        case 19:
          num1 = 5;
          break;
        case 20:
          num1 = 3;
          break;
      }
      int num2 = 50 - wpl.experimentalColourVariant * num1;
      return Color.FromArgb((int) byte.MaxValue, (int) areaColor.R * num2 / 50, (int) areaColor.G * num2 / 50, (int) areaColor.B * num2 / 50);
    }

    private void drawCountyBorders(RectangleF screenRect, bool solidBorders)
    {
      Color black = ARGBColors.Black;
      if (!solidBorders)
        Color.FromArgb(80, ARGBColors.Black);
      float thickness = (float) ((this.m_worldScale + 1.0 / 1000.0 - 2.0) / 3.0);
      if ((double) thickness < 1.0)
        thickness = 1f;
      double num1 = 0.5;
      double num2 = 5.0;
      if (this.EUMap)
        num1 = 1.5;
      if (!this.KGMap)
      {
        foreach (WorldMap.WorldPointList county in this.countyList)
        {
          if (county.isVisible(screenRect))
          {
            if (!this.LinelessMaps)
            {
              int length = county.borderList.Length;
              if (length > 1)
              {
                this.gfx.startThickLine(ARGBColors.Black, thickness);
                this.gfx.setThickLineRadius((float) this.m_worldScale);
                for (int index = 0; index < length; ++index)
                {
                  WorldMap.WorldPoint point = this.pointList[county.borderList[index]];
                  this.gfx.addThickLinePoint((point.x - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth, (point.y - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight);
                }
                this.gfx.drawThickLines(true);
              }
            }
            if (county.marker.X >= 0 && this.m_worldScale < num2 && this.m_worldScale > num1)
            {
              float x = ((float) county.marker.X - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
              float num3 = ((float) county.marker.Y - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
              if (this.smallMapFont)
                this.addText(county, new PointF(x, num3 + (float) this.yMarkerOffset), ARGBColors.Black, true, 0, true, WorldMap.MapTextType.COUNTY);
              else
                this.addText(county, new PointF(x, num3 + (float) this.yMarkerOffset), ARGBColors.Black, true, 1, true, WorldMap.MapTextType.COUNTY);
            }
          }
        }
      }
      else
      {
        int index1 = 0;
        foreach (WorldMap.WorldPointList county in this.countyList)
        {
          if (county.isVisible(screenRect))
          {
            if (!this.LinelessMaps)
            {
              int length = county.borderList.Length;
              if (length > 1)
              {
                this.gfx.startThickLine(ARGBColors.Black, thickness);
                this.gfx.setThickLineRadius((float) this.m_worldScale);
                for (int index2 = 0; index2 < length; ++index2)
                {
                  WorldMap.WorldPoint point = this.pointList[county.borderList[index2]];
                  this.gfx.addThickLinePoint((point.x - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth, (point.y - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight);
                }
                this.gfx.drawThickLines(true);
              }
            }
            if (county.marker.X >= 0 && this.m_worldScale < num2 && this.m_worldScale > num1)
            {
              float x = ((float) county.marker.X - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
              float num4 = ((float) county.marker.Y - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
              WorldMap.WorldPointList country = this.countryList[index1];
              if (this.smallMapFont)
                this.addText(country, new PointF(x, num4 + (float) this.yMarkerOffset), ARGBColors.Black, true, 0, true, WorldMap.MapTextType.COUNTY);
              else
                this.addText(country, new PointF(x, num4 + (float) this.yMarkerOffset), ARGBColors.Black, true, 1, true, WorldMap.MapTextType.COUNTY);
            }
          }
          ++index1;
        }
      }
      this.gfx.renderLines();
    }

    private void drawProvinceBorders(RectangleF screenRect, bool thickBorders, bool political)
    {
      Color col = ARGBColors.Green;
      if (!political)
        col = ARGBColors.Black;
      float num1 = (float) ((this.m_worldScale - 2.0) / 3.0);
      if ((double) num1 < 1.0)
        num1 = 1f;
      float scale = !thickBorders ? num1 * 2f : num1 * 3.5f;
      double num2 = 0.22;
      double num3 = 0.5;
      if (this.KGMap)
      {
        num2 = 0.65;
        num3 = 1.5;
      }
      else if (this.EUMap)
      {
        num2 = 0.5;
        num3 = 1.5;
      }
      if (this.playingProvinces)
        col = ARGBColors.Black;
      if (!this.KGMap)
      {
        foreach (WorldMap.WorldPointList provinces in this.provincesList)
        {
          if (this.drawProvinceBorder(screenRect, provinces, scale, col) && this.m_worldScale < num3 && provinces.marker.X >= 0 && this.m_worldScale <= num3 && this.m_worldScale > num2)
          {
            float x = ((float) provinces.marker.X - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
            float num4 = ((float) provinces.marker.Y - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
            if (this.smallMapFont)
              this.addText(provinces, new PointF(x, num4 + (float) this.yMarkerOffset), ARGBColors.Black, true, 0, true, WorldMap.MapTextType.PROVINCE);
            else
              this.addText(provinces, new PointF(x, num4 + (float) this.yMarkerOffset), ARGBColors.Black, true, 1, true, WorldMap.MapTextType.PROVINCE);
          }
        }
      }
      else
      {
        int index = 0;
        foreach (WorldMap.WorldPointList provinces in this.provincesList)
        {
          if (this.drawProvinceBorder(screenRect, provinces, scale, col) && this.m_worldScale < num3 && provinces.marker.X >= 0 && this.m_worldScale <= num3 && this.m_worldScale > num2)
          {
            float x = ((float) provinces.marker.X - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
            float num5 = ((float) provinces.marker.Y - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
            WorldMap.WorldPointList country = this.countryList[index];
            if (this.smallMapFont)
              this.addText(country, new PointF(x, num5 + (float) this.yMarkerOffset), ARGBColors.Black, true, 0, true, WorldMap.MapTextType.PROVINCE);
            else
              this.addText(country, new PointF(x, num5 + (float) this.yMarkerOffset), ARGBColors.Black, true, 1, true, WorldMap.MapTextType.PROVINCE);
          }
          ++index;
        }
      }
      if (!this.drawFakeProvinceBorders)
      {
        foreach (WorldMap.IslandInfoList island in this.islandList)
        {
          WorldMap.WorldPointList county = this.countyList[island.county];
          this.drawProvinceBorder(screenRect, county, scale, col);
        }
      }
      this.gfx.renderLines();
    }

    private bool drawProvinceBorder(
      RectangleF screenRect,
      WorldMap.WorldPointList wpl,
      float scale,
      Color col)
    {
      if (this.LinelessMaps)
        return true;
      if (!wpl.isVisible(screenRect))
        return false;
      int length = wpl.borderList.Length;
      if (length > 1)
      {
        this.gfx.startThickLine(col, scale);
        this.gfx.setThickLineRadius((float) this.m_worldScale);
        for (int index = 0; index < length; ++index)
        {
          if (wpl.borderList[index] == -1)
          {
            this.gfx.drawThickLines(true);
            this.gfx.startThickLine(col, scale);
            this.gfx.setThickLineRadius((float) this.m_worldScale);
          }
          else
          {
            WorldMap.WorldPoint point = this.pointList[wpl.borderList[index]];
            this.gfx.addThickLinePoint((point.x - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth, (point.y - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight);
          }
        }
        this.gfx.drawThickLines(true);
      }
      return true;
    }

    private void drawCountryBorders(RectangleF screenRect)
    {
      float num1 = (float) ((this.m_worldScale - 2.0) / 3.0);
      if ((double) num1 < 1.0)
        num1 = 1f;
      float scale = num1 * 7f;
      Color col = ARGBColors.Purple;
      if (this.m_worldScale <= 0.2)
      {
        scale = 3f;
        col = ARGBColors.Black;
      }
      double num2 = 0.0;
      double num3 = 0.22;
      if (this.KGMap)
        num2 = 10000.0;
      else if (this.EUMap)
        num3 = 0.5;
      if (this.playingCountries)
        col = ARGBColors.Black;
      foreach (WorldMap.WorldPointList country in this.countryList)
      {
        if (this.drawProvinceBorder(screenRect, country, scale, col) && country.marker.X >= 0 && this.m_worldScale <= num3 && this.m_worldScale > num2)
        {
          float x = ((float) country.marker.X - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
          float num4 = ((float) country.marker.Y - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
          if (this.smallMapFont)
            this.addText(country, new PointF(x, num4 + (float) this.yMarkerOffset), ARGBColors.Black, true, 0, true, WorldMap.MapTextType.COUNTRY);
          else
            this.addText(country, new PointF(x, num4 + (float) this.yMarkerOffset), ARGBColors.Black, true, 1, true, WorldMap.MapTextType.COUNTRY);
        }
      }
      if (!this.drawFakeProvinceBorders)
      {
        foreach (WorldMap.IslandInfoList island in this.islandList)
        {
          WorldMap.WorldPointList county = this.countyList[island.county];
          this.drawProvinceBorder(screenRect, county, scale, col);
        }
      }
      this.gfx.renderLines();
    }

    public void drawIslandLines(RectangleF screenRect)
    {
      float num = (float) ((this.m_worldScale - 2.0) / 3.0);
      if ((double) num < 1.0)
        num = 1f;
      float thickness = num * 2f;
      foreach (WorldMap.IslandInfoList island in this.islandList)
      {
        if (((double) island.sx >= (double) screenRect.Left || (double) island.ex >= (double) screenRect.Left) && ((double) island.sy >= (double) screenRect.Top || (double) island.ey >= (double) screenRect.Top) && ((double) island.sx <= (double) screenRect.Right || (double) island.ex <= (double) screenRect.Right) && ((double) island.sy <= (double) screenRect.Bottom || (double) island.ey <= (double) screenRect.Bottom))
        {
          this.gfx.startThickLine(this.islandLineColor, thickness);
          this.gfx.setThickLineRadius((float) this.m_worldScale);
          this.gfx.addThickLinePoint(((float) island.sx - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth, ((float) island.sy - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight);
          this.gfx.addThickLinePoint(((float) island.ex - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth, ((float) island.ey - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight);
          this.gfx.drawThickLines(true);
        }
      }
    }

    public void drawAIWorldLines(RectangleF screenRect)
    {
      float num = (float) ((this.m_worldScale - 2.0) / 3.0);
      if ((double) num < 1.0)
        num = 1f;
      float thickness = num * 2f;
      if (this.aiWorldInvasionLineList == null)
        return;
      foreach (WorldMap.IslandInfoList worldInvasionLine in this.aiWorldInvasionLineList)
      {
        if (((double) worldInvasionLine.sx >= (double) screenRect.Left || (double) worldInvasionLine.ex >= (double) screenRect.Left) && ((double) worldInvasionLine.sy >= (double) screenRect.Top || (double) worldInvasionLine.ey >= (double) screenRect.Top) && ((double) worldInvasionLine.sx <= (double) screenRect.Right || (double) worldInvasionLine.ex <= (double) screenRect.Right) && ((double) worldInvasionLine.sy <= (double) screenRect.Bottom || (double) worldInvasionLine.ey <= (double) screenRect.Bottom))
        {
          Color color = ARGBColors.DarkRed;
          if (this.getAIInvasionMarkerState(worldInvasionLine.villageID) == 0)
            color = Color.FromArgb(80, color);
          this.gfx.startThickLine(color, thickness);
          this.gfx.setThickLineRadius((float) this.m_worldScale);
          this.gfx.addThickLinePoint(((float) worldInvasionLine.sx - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth, ((float) worldInvasionLine.sy - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight);
          this.gfx.addThickLinePoint(((float) worldInvasionLine.ex - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth, ((float) worldInvasionLine.ey - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight);
          this.gfx.drawThickLines(true);
        }
      }
    }

    private void drawCountyPoly(RectangleF screenRect)
    {
      this.gfx.startPoly();
      foreach (WorldMap.WorldPointList county in this.countyList)
      {
        int villageColour = this.getVillageColour(county);
        Color color = this.getAreaColour(villageColour, county);
        if (this.GeographicalMap)
        {
          float maxValue = (float) byte.MaxValue;
          float num = (float) this.m_worldZoomInverted / 17.5f;
          if ((double) num < 1.0)
            maxValue *= num;
          if (villageColour != 0 || this.m_worldScale < 23.899999998509884)
            color = Color.FromArgb((int) maxValue, color);
          else
            continue;
        }
        this.drawAreaPoly(screenRect, county, color);
      }
      this.gfx.drawBufferedPolygons();
    }

    private void drawAreaPoly(RectangleF screenRect, WorldMap.WorldPointList wpl, Color col)
    {
      if (!wpl.isVisible(screenRect))
        return;
      int length = wpl.triangleList.Length;
      if (length <= 0)
        return;
      float num1 = screenRect.Width / (float) this.m_screenWidth;
      float num2 = screenRect.Height / (float) this.m_screenHeight;
      float left = screenRect.Left;
      float top = screenRect.Top;
      for (int index = 0; index < length; ++index)
      {
        float x1 = (wpl.triangleList[index].x1 - left) / num1;
        float x2 = (wpl.triangleList[index].x2 - left) / num1;
        float x3 = (wpl.triangleList[index].x3 - left) / num1;
        float y1 = (wpl.triangleList[index].y1 - top) / num2;
        float y2 = (wpl.triangleList[index].y2 - top) / num2;
        float y3 = (wpl.triangleList[index].y3 - top) / num2;
        this.gfx.addTriangle(col, x1, y1, x2, y2, x3, y3);
      }
    }

    private void drawProvincePoly(RectangleF screenRect)
    {
      this.gfx.startPoly();
      foreach (WorldMap.WorldPointList provinces in this.provincesList)
      {
        int villageColour = this.getVillageColour(provinces);
        Color color = this.getAreaColour(villageColour, provinces);
        if (this.GeographicalMap)
        {
          float maxValue = (float) byte.MaxValue;
          float num = (float) this.m_worldZoomInverted / 17.5f;
          if ((double) num < 1.0)
            maxValue *= num;
          if (villageColour != 0 || this.m_worldScale < 23.899999998509884)
            color = Color.FromArgb((int) maxValue, color);
          else
            continue;
        }
        this.drawAreaPoly(screenRect, provinces, color);
      }
      if (!this.drawFakeProvinceBorders)
      {
        foreach (WorldMap.IslandInfoList island in this.islandList)
        {
          WorldMap.WorldPointList provinces = this.provincesList[island.province];
          int villageColour = this.getVillageColour(provinces);
          Color color = this.getAreaColour(villageColour, provinces);
          if (this.GeographicalMap)
          {
            float maxValue = (float) byte.MaxValue;
            float num = (float) this.m_worldZoomInverted / 17.5f;
            if ((double) num < 1.0)
              maxValue *= num;
            if (villageColour != 0 || this.m_worldScale < 23.899999998509884)
              color = Color.FromArgb((int) maxValue, color);
            else
              continue;
          }
          WorldMap.WorldPointList county = this.countyList[island.county];
          this.drawAreaPoly(screenRect, county, color);
        }
      }
      this.gfx.drawBufferedPolygons();
    }

    private void drawProvincePolyPlayback(RectangleF screenRect)
    {
      this.gfx.startPoly();
      int provinceID = 0;
      foreach (WorldMap.WorldPointList provinces in this.provincesList)
      {
        int playbackProvinceHouse1 = this.getPlaybackProvinceHouse(this.playbackDay, provinceID);
        Color col = WorldMap.areaColorList[playbackProvinceHouse1];
        if (this.playbackDay < this.playbackTotalDays - 1)
        {
          int playbackProvinceHouse2 = this.getPlaybackProvinceHouse(this.playbackDay + 1, provinceID);
          if (playbackProvinceHouse1 != playbackProvinceHouse2)
          {
            double playbackFrameFraction = this.playbackFrameFraction;
            Color areaColor = WorldMap.areaColorList[playbackProvinceHouse2];
            col = Color.FromArgb((int) ((double) col.R * (1.0 - playbackFrameFraction) + (double) areaColor.R * playbackFrameFraction), (int) ((double) col.G * (1.0 - playbackFrameFraction) + (double) areaColor.G * playbackFrameFraction), (int) ((double) col.B * (1.0 - playbackFrameFraction) + (double) areaColor.B * playbackFrameFraction));
          }
        }
        ++provinceID;
        this.drawAreaPoly(screenRect, provinces, col);
      }
      if (!this.drawFakeProvinceBorders)
      {
        foreach (WorldMap.IslandInfoList island in this.islandList)
        {
          int playbackProvinceHouse3 = this.getPlaybackProvinceHouse(this.playbackDay, island.province);
          Color col = WorldMap.areaColorList[playbackProvinceHouse3];
          if (this.playbackDay < this.playbackTotalDays - 1)
          {
            int playbackProvinceHouse4 = this.getPlaybackProvinceHouse(this.playbackDay + 1, island.province);
            if (playbackProvinceHouse3 != playbackProvinceHouse4)
            {
              double playbackFrameFraction = this.playbackFrameFraction;
              Color areaColor = WorldMap.areaColorList[playbackProvinceHouse4];
              col = Color.FromArgb((int) ((double) col.R * (1.0 - playbackFrameFraction) + (double) areaColor.R * playbackFrameFraction), (int) ((double) col.G * (1.0 - playbackFrameFraction) + (double) areaColor.G * playbackFrameFraction), (int) ((double) col.B * (1.0 - playbackFrameFraction) + (double) areaColor.B * playbackFrameFraction));
            }
          }
          WorldMap.WorldPointList county = this.countyList[island.county];
          this.drawAreaPoly(screenRect, county, col);
        }
      }
      this.gfx.drawBufferedPolygons();
    }

    private void drawCountryPoly(RectangleF screenRect)
    {
      this.gfx.startPoly();
      foreach (WorldMap.WorldPointList country in this.countryList)
      {
        int villageColour = this.getVillageColour(country);
        Color color = this.getAreaColour(villageColour, country);
        if (this.GeographicalMap)
        {
          float maxValue = (float) byte.MaxValue;
          float num = (float) this.m_worldZoomInverted / 17.5f;
          if ((double) num < 1.0)
            maxValue *= num;
          if (villageColour != 0 || this.m_worldScale < 23.899999998509884)
            color = Color.FromArgb((int) maxValue, color);
          else
            continue;
        }
        this.drawAreaPoly(screenRect, country, color);
      }
      if (!this.drawFakeProvinceBorders)
      {
        foreach (WorldMap.IslandInfoList island in this.islandList)
        {
          WorldMap.WorldPointList country = this.countryList[island.country];
          int villageColour = this.getVillageColour(country);
          Color color = this.getAreaColour(villageColour, country);
          if (this.GeographicalMap)
          {
            float maxValue = (float) byte.MaxValue;
            float num = (float) this.m_worldZoomInverted / 17.5f;
            if ((double) num < 1.0)
              maxValue *= num;
            if (villageColour != 0 || this.m_worldScale < 23.899999998509884)
              color = Color.FromArgb((int) maxValue, color);
            else
              continue;
          }
          WorldMap.WorldPointList county = this.countyList[island.county];
          this.drawAreaPoly(screenRect, county, color);
        }
      }
      this.gfx.drawBufferedPolygons();
    }

    private void drawCountryPolyPlayback(RectangleF screenRect)
    {
      this.gfx.startPoly();
      int countryID = 0;
      foreach (WorldMap.WorldPointList country in this.countryList)
      {
        int playbackCountryHouse1 = this.getPlaybackCountryHouse(this.playbackDay, countryID);
        Color col = WorldMap.areaColorList[playbackCountryHouse1];
        if (this.playbackDay < this.playbackTotalDays - 2)
        {
          int playbackCountryHouse2 = this.getPlaybackCountryHouse(this.playbackDay + 1, countryID);
          if (playbackCountryHouse1 != playbackCountryHouse2)
          {
            double playbackFrameFraction = this.playbackFrameFraction;
            Color areaColor = WorldMap.areaColorList[playbackCountryHouse2];
            col = Color.FromArgb((int) ((double) col.R * (1.0 - playbackFrameFraction) + (double) areaColor.R * playbackFrameFraction), (int) ((double) col.G * (1.0 - playbackFrameFraction) + (double) areaColor.G * playbackFrameFraction), (int) ((double) col.B * (1.0 - playbackFrameFraction) + (double) areaColor.B * playbackFrameFraction));
          }
        }
        ++countryID;
        this.drawAreaPoly(screenRect, country, col);
      }
      if (!this.drawFakeProvinceBorders)
      {
        foreach (WorldMap.IslandInfoList island in this.islandList)
        {
          int playbackCountryHouse3 = this.getPlaybackCountryHouse(this.playbackDay, island.country);
          Color col = WorldMap.areaColorList[playbackCountryHouse3];
          if (this.playbackDay < this.playbackTotalDays - 2)
          {
            int playbackCountryHouse4 = this.getPlaybackCountryHouse(this.playbackDay + 1, island.country);
            if (playbackCountryHouse3 != playbackCountryHouse4)
            {
              double playbackFrameFraction = this.playbackFrameFraction;
              Color areaColor = WorldMap.areaColorList[playbackCountryHouse4];
              col = Color.FromArgb((int) ((double) col.R * (1.0 - playbackFrameFraction) + (double) areaColor.R * playbackFrameFraction), (int) ((double) col.G * (1.0 - playbackFrameFraction) + (double) areaColor.G * playbackFrameFraction), (int) ((double) col.B * (1.0 - playbackFrameFraction) + (double) areaColor.B * playbackFrameFraction));
            }
          }
          WorldMap.WorldPointList county = this.countyList[island.county];
          this.drawAreaPoly(screenRect, county, col);
        }
      }
      this.gfx.drawBufferedPolygons();
    }

    private void drawRegions(RectangleF screenRect)
    {
      this.gfx.startPoly();
      int num1 = -1;
      float num2 = screenRect.Width / (float) this.m_screenWidth;
      float num3 = screenRect.Height / (float) this.m_screenHeight;
      float left = screenRect.Left;
      float top = screenRect.Top;
      foreach (WorldMap.WorldPointList region in this.regionList)
      {
        ++num1;
        if (region.isVisible(screenRect))
        {
          int villageColour = this.getVillageColour(region);
          Color areaColour = this.getAreaColour(villageColour, region);
          int length = region.triangleList.Length;
          if (length > 0)
          {
            float maxValue = (float) byte.MaxValue;
            float num4 = (float) this.WorldZoom - 5f;
            if ((double) num4 < 1.0)
              maxValue *= num4;
            if (this.GeographicalMap)
            {
              float num5 = (float) this.m_worldZoomInverted / 8f;
              if ((double) num5 < 1.0)
                maxValue *= num5;
              if (villageColour == 0 && this.m_worldScale >= 23.899999998509884)
                continue;
            }
            Color col = Color.FromArgb((int) maxValue, areaColour);
            for (int index = 0; index < length; ++index)
            {
              float x1 = (region.triangleList[index].x1 - left) / num2;
              float x2 = (region.triangleList[index].x2 - left) / num2;
              float x3 = (region.triangleList[index].x3 - left) / num2;
              float y1 = (region.triangleList[index].y1 - top) / num3;
              float y2 = (region.triangleList[index].y2 - top) / num3;
              float y3 = (region.triangleList[index].y3 - top) / num3;
              this.gfx.addTriangle(col, x1, y1, x2, y2, x3, y3);
            }
          }
        }
      }
      this.gfx.drawBufferedPolygons();
    }

    public void drawRegionsBorder(RectangleF screenRect, bool forcedDraw)
    {
      Color color = ARGBColors.DarkGreen;
      float maxValue = (float) byte.MaxValue;
      float num1 = (float) this.WorldZoom - 5f;
      if ((double) num1 < 0.0)
        num1 = 0.0f;
      if ((double) num1 < 1.0)
        color = Color.FromArgb((int) (maxValue * num1), color);
      float num2 = screenRect.Width / (float) this.m_screenWidth;
      float num3 = screenRect.Height / (float) this.m_screenHeight;
      float left = screenRect.Left;
      float top = screenRect.Top;
      int num4 = -1;
      foreach (WorldMap.WorldPointList region in this.regionList)
      {
        ++num4;
        if (region.isVisible(screenRect))
        {
          int parentId = region.parentID;
          if (parentId < 0 || forcedDraw || this.getHouse(region.factionID) == this.getHouse(this.countyList[parentId].factionID) || this.GeographicalMap)
          {
            int length = region.regionBorderList.Length;
            if (length > 1)
            {
              this.gfx.startThickLine(color, 1f);
              this.gfx.setThickLineRadius((float) this.m_worldScale);
              for (int index = 0; index < length; ++index)
              {
                WorldMap.WorldPoint regionBorder = region.regionBorderList[index];
                if ((double) regionBorder.x != 0.0 || (double) regionBorder.y != 0.0)
                  this.gfx.addThickLinePoint((regionBorder.x - left) / num2, (regionBorder.y - top) / num3);
                else
                  break;
              }
              this.gfx.drawThickLines(true);
            }
          }
        }
      }
      this.gfx.renderLines();
    }

    private void drawSeas(RectangleF screenRect)
    {
      if (this.GeographicalMap)
        return;
      float num1 = screenRect.Width / (float) this.m_screenWidth;
      float num2 = screenRect.Height / (float) this.m_screenHeight;
      float left = screenRect.Left;
      float top = screenRect.Top;
      foreach (WorldMap.WorldPointList sea in this.seaList)
      {
        Color col = WorldMap.SEACOLOR;
        if (sea.data == 1)
          col = Color.FromArgb((int) byte.MaxValue, 152, 181, 134);
        if (sea.isVisible(screenRect))
        {
          int length = sea.triangleList.Length;
          if (length > 0)
          {
            this.gfx.startPoly();
            for (int index = 0; index < length; ++index)
            {
              float x1 = (sea.triangleList[index].x1 - left) / num1;
              float x2 = (sea.triangleList[index].x2 - left) / num1;
              float x3 = (sea.triangleList[index].x3 - left) / num1;
              float y1 = (sea.triangleList[index].y1 - top) / num2;
              float y2 = (sea.triangleList[index].y2 - top) / num2;
              float y3 = (sea.triangleList[index].y3 - top) / num2;
              this.gfx.addTriangle(col, x1, y1, x2, y2, x3, y3);
            }
            this.gfx.drawBufferedPolygons();
          }
        }
      }
    }

    private void drawSurroundBox(
      RectangleF screenRect,
      Color col,
      float x1,
      float y1,
      float x2,
      float y2)
    {
      x1 = (x1 - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
      x2 = (x2 - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
      y1 = (y1 - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
      y2 = (y2 - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
      this.gfx.addTriangle(col, x1, y1, x2, y1, x1, y2);
      this.gfx.addTriangle(col, x2, y1, x2, y2, x1, y2);
    }

    public void addText(string text, PointF loc, Color col, bool centered, int size)
    {
      this.addText(text, loc, col, centered, size, false);
    }

    public void addText(
      string text,
      PointF loc,
      Color col,
      bool centered,
      int size,
      bool bordered)
    {
      this.textDrawList.Add(new WorldMap.MapText()
      {
        text = text,
        loc = loc,
        col = col,
        size = size,
        centered = centered,
        bordered = bordered
      });
    }

    public void addText(
      string text,
      PointF loc,
      Color col,
      bool centered,
      int size,
      bool bordered,
      bool preAdjusted)
    {
      this.textDrawList.Add(new WorldMap.MapText()
      {
        text = text,
        loc = loc,
        col = col,
        size = size,
        centered = centered,
        bordered = bordered,
        preAdjustedForRetina = preAdjusted
      });
    }

    public void addText(
      string text,
      PointF loc,
      Color col,
      bool centered,
      int size,
      bool bordered,
      WorldMap.MapTextType type)
    {
      this.textDrawList.Add(new WorldMap.MapText()
      {
        text = text,
        loc = loc,
        col = col,
        size = size,
        centered = centered,
        bordered = bordered,
        type = type
      });
    }

    public void addText(
      string text,
      PointF loc,
      Color col,
      bool centered,
      int size,
      bool bordered,
      bool preAdjusted,
      WorldMap.MapTextType type)
    {
      this.textDrawList.Add(new WorldMap.MapText()
      {
        text = text,
        loc = loc,
        col = col,
        size = size,
        centered = centered,
        bordered = bordered,
        type = type,
        preAdjustedForRetina = preAdjusted
      });
    }

    public void addText(
      WorldMap.WorldPointList wpl,
      PointF loc,
      Color col,
      bool centered,
      int size,
      bool bordered,
      WorldMap.MapTextType type)
    {
      this.textDrawList.Add(new WorldMap.MapText()
      {
        text = wpl.areaName,
        wpl = wpl,
        loc = loc,
        col = col,
        size = size,
        centered = centered,
        bordered = bordered,
        type = type
      });
    }

    private void drawText()
    {
      this.gfx.startPoly();
      foreach (WorldMap.MapText textDraw in this.textDrawList)
      {
        if (textDraw.bordered && Program.mySettings.UseMapTextBorders)
        {
          Rectangle textSize = this.gfx.getTextSize(textDraw.text, textDraw.size);
          float num1 = (float) ((double) textDraw.loc.X - (double) (textSize.Width / 2) - 5.0);
          float num2 = (float) ((double) textDraw.loc.X + (double) (textSize.Width / 2) + 3.0);
          float num3 = textDraw.loc.Y - 2f;
          float num4 = textDraw.loc.Y + (float) textSize.Height;
          if (textDraw.size == 0)
            num3 += 2f;
          this.gfx.addTriangle(Color.FromArgb(144, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), num1, num3 + 2f, num2, num3 + 2f, num1, num4 - 2f);
          this.gfx.addTriangle(Color.FromArgb(144, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), num2, num3 + 2f, num2, num4 - 2f, num1, num4 - 2f);
          this.gfx.addTriangle(Color.FromArgb(144, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), num1 + 1f, num3 + 1f, num2 - 1f, num3 + 1f, num1 + 1f, num3 + 2f);
          this.gfx.addTriangle(Color.FromArgb(144, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), num2 - 1f, num3 + 1f, num2 - 1f, num3 + 2f, num1 + 1f, num3 + 2f);
          this.gfx.addTriangle(Color.FromArgb(144, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), num1 + 1f, num4 - 2f, num2 - 1f, num4 - 2f, num1 + 1f, num4 - 1f);
          this.gfx.addTriangle(Color.FromArgb(144, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), num2 - 1f, num4 - 2f, num2 - 1f, num4 - 1f, num1 + 1f, num4 - 1f);
          this.gfx.addTriangle(Color.FromArgb(144, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), num1 + 2f, num3, num2 - 2f, num3, num1 + 2f, num3 + 1f);
          this.gfx.addTriangle(Color.FromArgb(144, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), num2 - 2f, num3, num2 - 2f, num3 + 1f, num1 + 2f, num3 + 1f);
          this.gfx.addTriangle(Color.FromArgb(144, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), num1 + 2f, num4 - 1f, num2 - 2f, num4 - 1f, num1 + 2f, num4);
          this.gfx.addTriangle(Color.FromArgb(144, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), num2 - 2f, num4 - 1f, num2 - 2f, num4, num1 + 2f, num4);
        }
      }
      this.gfx.drawBufferedPolygons();
      foreach (WorldMap.MapText textDraw in this.textDrawList)
        this.gfx.drawText(textDraw.text, textDraw.loc.X, textDraw.loc.Y, textDraw.col, textDraw.centered, textDraw.size, textDraw.bordered, false);
      this.textDrawList.Clear();
    }

    public void setScreenSize(int screenWidth, int screenHeight)
    {
      this.m_screenWidth = screenWidth;
      this.m_screenHeight = screenHeight;
    }

    public double WorldScale => this.m_worldScale;

    public double WorldZoom
    {
      get => 27.0 - this.m_worldZoomInverted;
      set
      {
        this.m_worldZoomInverted = 27.0 - value;
        if (this.m_worldZoomInverted >= 23.0)
          this.m_worldScale = 1.0 / (this.m_worldZoomInverted - 22.0);
        else
          this.m_worldScale = 24.0 - this.m_worldZoomInverted;
      }
    }

    public void moveMap(double dx, double dy)
    {
      this.m_screenCentreX += dx;
      this.m_screenCentreY += dy;
      if (this.m_screenCentreX < 0.0)
        this.m_screenCentreX = 0.0;
      if (this.m_screenCentreY < 0.0)
        this.m_screenCentreY = 0.0;
      if (this.m_screenCentreX >= (double) this.worldMapWidth)
        this.m_screenCentreX = (double) (this.worldMapWidth - 1);
      if (this.m_screenCentreY < (double) this.worldMapHeight)
        return;
      this.m_screenCentreY = (double) (this.worldMapHeight - 1);
    }

    public void centreMap(bool useTarget)
    {
      if (this.m_zoomStage >= 0 && this.m_zoomStage < 6)
        return;
      double num1 = this.m_worldScale;
      double num2 = this.m_screenCentreX;
      double num3 = this.m_screenCentreY;
      if (useTarget && this.m_zoomDiff != 0.0)
      {
        double num4 = 27.0 - this.m_targetZoom;
        num1 = num4 < 23.0 ? 24.0 - num4 : 1.0 / (num4 - 22.0);
        num2 = this.m_zoomXPosTarget;
        num3 = this.m_zoomYPosTarget;
      }
      int num5 = 0;
      double num6 = (0.0 - (double) this.m_screenWidth / 2.0) / num1 + num2;
      double num7 = ((double) -num5 - (double) this.m_screenHeight / 2.0) / num1 + num3;
      double num8 = ((double) this.m_screenWidth - (double) this.m_screenWidth / 2.0) / num1 + num2;
      double num9 = ((double) this.m_screenHeight - (double) this.m_screenHeight / 2.0) / num1 + num3;
      bool flag = false;
      if (this.m_zooming && this.m_zoomDiff > 0.0)
        flag = true;
      if (num6 < 0.0 && num8 >= (double) this.worldMapWidth)
      {
        this.m_screenCentreX = (double) (this.worldMapWidth / 2);
        if (!flag)
          this.m_zoomXPosDiff = 0.0;
      }
      else if (num6 < 0.0)
      {
        double num10 = (double) this.m_screenWidth / num1;
        if (num10 > (double) this.worldMapWidth)
        {
          this.m_screenCentreX = (double) (this.worldMapWidth / 2);
          if (!flag)
            this.m_zoomXPosDiff = 0.0;
        }
        else
          this.m_screenCentreX = num10 / 2.0;
      }
      else if (num8 >= (double) this.worldMapWidth)
      {
        double num11 = (double) this.m_screenWidth / num1;
        if (num11 > (double) this.worldMapWidth)
        {
          this.m_screenCentreX = (double) (this.worldMapWidth / 2);
          if (!flag)
            this.m_zoomXPosDiff = 0.0;
        }
        else
          this.m_screenCentreX = (double) this.worldMapWidth - num11 / 2.0;
      }
      if (num7 < 0.0 && num9 >= (double) this.worldMapHeight)
      {
        this.m_screenCentreY = (double) (this.worldMapHeight / 2) + (double) num5 / 2.0 / num1;
        if (flag)
          return;
        this.m_zoomYPosDiff = 0.0;
      }
      else if (num7 < 0.0)
      {
        double num12 = (double) this.m_screenHeight / num1;
        if (num12 > (double) this.worldMapHeight)
        {
          this.m_screenCentreY = (double) (this.worldMapHeight / 2) + (double) num5 / 2.0 / num1;
          if (flag)
            return;
          this.m_zoomYPosDiff = 0.0;
        }
        else
          this.m_screenCentreY = num12 / 2.0 + (double) num5 / num1;
      }
      else
      {
        if (num9 < (double) this.worldMapHeight)
          return;
        double num13 = (double) this.m_screenHeight / num1;
        if (num13 > (double) this.worldMapHeight)
        {
          this.m_screenCentreY = (double) (this.worldMapHeight / 2) + (double) (num5 / 2) / num1;
          if (flag)
            return;
          this.m_zoomYPosDiff = 0.0;
        }
        else
          this.m_screenCentreY = (double) this.worldMapHeight - num13 / 2.0;
      }
    }

    public VillageData rolloverTargetVillage
    {
      get
      {
        return this.m_rolloverTargetVillageNoDelay > 0 && this.m_rolloverTargetVillageNoDelay < GameEngine.Instance.World.villageList.Length ? this.villageList[this.m_rolloverTargetVillageNoDelay] : (VillageData) null;
      }
    }

    public void moveMouse(Point mousePos)
    {
      this.m_rolloverTargetVillage = -1;
      this.m_rolloverTargetVillageNoDelay = -1;
      this.m_rolloverVillageShieldID = -1;
      if (this.WorldZoom > 13.0)
      {
        double bestDist1 = 100000.0;
        int villageID = this.findNearestVillageFromScreenPos(mousePos, ref bestDist1);
        if (bestDist1 > 4.0)
          villageID = -1;
        long num1 = -1;
        long num2 = -1;
        long num3 = -1;
        long num4 = -1;
        if (villageID < 0 && InterfaceMgr.Instance.WorldMapMode == 0)
        {
          double bestDist2 = 0.0;
          num1 = this.findNearestArmyFromScreenPos(mousePos, ref bestDist2);
          if (num1 >= 0L && bestDist2 > 4.0)
            num1 = -1L;
          if (num1 < 0L)
          {
            double bestDist3 = 0.0;
            num2 = this.findNearestTraderFromScreenPos(mousePos, ref bestDist3);
            if (num2 >= 0L && bestDist3 > 4.0)
              num2 = -1L;
            if (num2 < 0L)
            {
              double bestDist4 = 0.0;
              num3 = this.findNearestReinforcementFromScreenPos(mousePos, ref bestDist4);
              if (num3 >= 0L && bestDist4 > 4.0)
                num3 = -1L;
              if (num3 < 0L)
              {
                double bestDist5 = 0.0;
                num4 = this.findNearestPersonFromScreenPos(mousePos, ref bestDist5);
                if (num4 >= 0L && bestDist5 > 4.0)
                  num4 = -1L;
              }
            }
          }
        }
        if (num1 < 0L && villageID < 0 && num2 < 0L && num4 < 0L && num3 < 0L && !InterfaceMgr.Instance.isMenuPopupOpen() && !InterfaceMgr.Instance.isInsideAchievementPopup())
          CursorManager.SetCursor(CursorManager.CursorType.Hand, InterfaceMgr.Instance.ParentForm);
        else
          CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
        bool flag = false;
        if (this.m_rolloverLastMousepos == mousePos)
        {
          if (DXTimer.GetCurrentMillisecondsLong() - this.m_rolloverLastTime > 150L)
            flag = true;
        }
        else
        {
          this.m_rolloverLastMousepos = mousePos;
          this.m_rolloverLastTime = DXTimer.GetCurrentMillisecondsLong();
        }
        if (villageID < 0 || this.m_worldZoomInverted >= 0.001 || this.m_leftMouseHeldDown)
          return;
        this.m_rolloverTargetVillageNoDelay = villageID;
        if (!flag)
          return;
        this.m_rolloverTargetVillage = villageID;
        if (!this.isOverVillageShield(villageID, (PointF) mousePos, false))
          return;
        this.m_rolloverVillageShieldID = villageID;
        this.m_rolloverTargetVillage = -1;
      }
      else
      {
        double bestDist = 100000.0;
        int num = this.findNearestVillageFromScreenPos(mousePos, ref bestDist);
        if (bestDist > 4.0)
          num = -1;
        if (num < 0 && !InterfaceMgr.Instance.isMenuPopupOpen() && !InterfaceMgr.Instance.isInsideAchievementPopup())
          CursorManager.SetCursor(CursorManager.CursorType.Hand, InterfaceMgr.Instance.ParentForm);
        else
          CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
      }
    }

    public bool isOverVillageShield(int villageID, PointF mousePos, bool fromNotOwn)
    {
      if (this.isUserVillage(villageID) || this.isUserRelatedVillage(villageID))
      {
        VillageData village = this.villageList[villageID];
        double num1 = ((double) mousePos.X - (double) this.m_screenWidth / 2.0) / this.m_worldScale + this.m_screenCentreX;
        double num2 = ((double) mousePos.Y - (double) this.m_screenHeight / 2.0) / this.m_worldScale + this.m_screenCentreY;
        double num3 = num1 - (double) village.x;
        double num4 = num2 - (double) village.y;
        if (num3 > -0.5 && num3 < 0.3 && num4 < -1.1 && num4 > -2.0 || InterfaceMgr.Instance.OwnSelectedVillage < 0)
          return true;
      }
      return false;
    }

    public bool isOnlyOverVillageShield(int villageID, PointF mousePos)
    {
      if (this.isUserVillage(villageID) || this.isUserRelatedVillage(villageID))
      {
        VillageData village = this.villageList[villageID];
        double num1 = ((double) mousePos.X - (double) this.m_screenWidth / 2.0) / this.m_worldScale + this.m_screenCentreX;
        double num2 = ((double) mousePos.Y - (double) this.m_screenHeight / 2.0) / this.m_worldScale + this.m_screenCentreY;
        double num3 = num1 - (double) village.x;
        double num4 = num2 - (double) village.y;
        if (num3 > -0.5 && num3 < 0.3 && num4 < -1.1 && num4 > -2.0)
          return true;
      }
      return false;
    }

    public void createTributeLinesList(int villageID)
    {
      this.clearInterVillageLines();
      if (villageID < 0 || villageID >= this.villageList.Length)
        return;
      VillageData village1 = this.villageList[villageID];
      foreach (VillageData village2 in this.villageList)
      {
        if (village2.connecter == villageID)
        {
          float scale = (float) (2.0 - (double) village2.villageInfo / 120.0);
          this.addInterVillageLine(new Point((int) village1.x, (int) village1.y), new Point((int) village2.x, (int) village2.y), true, scale);
        }
      }
      if (village1.connecter < 0 || village1.connecter >= this.villageList.Length)
        return;
      VillageData village3 = this.villageList[village1.connecter];
      float scale1 = (float) (2.0 - (double) village1.villageInfo / 120.0);
      this.addInterVillageLine(new Point((int) village3.x, (int) village3.y), new Point((int) village1.x, (int) village1.y), false, scale1);
    }

    private void addInterVillageLine(Point start, Point end, bool yours)
    {
      WorldMap.InterVillageLine interVillageLine = new WorldMap.InterVillageLine();
      interVillageLine.start = new PointF((float) start.X, (float) start.Y);
      interVillageLine.end = new PointF((float) end.X, (float) end.Y);
      if (yours)
        interVillageLine.style = 2;
      this.interVillageLines.Add(interVillageLine);
    }

    private void addInterVillageLine(Point start, Point end, bool yours, float scale)
    {
      WorldMap.InterVillageLine interVillageLine = new WorldMap.InterVillageLine();
      interVillageLine.start = new PointF((float) start.X, (float) start.Y);
      interVillageLine.end = new PointF((float) end.X, (float) end.Y);
      if (yours)
        interVillageLine.style = 2;
      interVillageLine.widthScalar = scale;
      if ((double) interVillageLine.widthScalar < 1.0)
        interVillageLine.widthScalar = 1f;
      else if ((double) interVillageLine.widthScalar > 2.0)
        interVillageLine.widthScalar = 2f;
      this.interVillageLines.Add(interVillageLine);
    }

    private void addDynamicInterVillageLine(Point start, Point end, int style)
    {
      this.dynamicVillageLines.Add(new WorldMap.InterVillageLine()
      {
        start = new PointF((float) start.X, (float) start.Y),
        end = new PointF((float) end.X, (float) end.Y),
        style = style,
        minLength = false
      });
    }

    private void addDynamicInterVillageLine(PointF start, PointF end, int style)
    {
      this.addDynamicInterVillageLine(start, end, style, 1.1f);
    }

    private void addDynamicInterVillageLine(PointF start, PointF end, int style, float scalar)
    {
      WorldMap.InterVillageLine interVillageLine = new WorldMap.InterVillageLine();
      interVillageLine.start = start;
      interVillageLine.end = end;
      interVillageLine.style = style;
      interVillageLine.widthScalar = scalar;
      if ((double) interVillageLine.widthScalar < 1.0)
        interVillageLine.widthScalar = 1f;
      else if ((double) interVillageLine.widthScalar > 2.0)
        interVillageLine.widthScalar = 2f;
      this.dynamicVillageLines.Add(interVillageLine);
    }

    public void drawInterVillageLines(RectangleF screenRect)
    {
      Color outerColour1 = Color.FromArgb(128, ARGBColors.Blue);
      Color innerColour1 = Color.FromArgb(128, ARGBColors.LightBlue);
      Color outerColour2 = Color.FromArgb(128, ARGBColors.Green);
      Color innerColour2 = Color.FromArgb(128, ARGBColors.LightGreen);
      Color outerColour3 = Color.FromArgb(128, Color.FromArgb(128, (int) byte.MaxValue, 128));
      Color innerColour3 = Color.FromArgb(128, Color.FromArgb(192, (int) byte.MaxValue, 192));
      Color outerColour4 = Color.FromArgb(128, Color.FromArgb((int) byte.MaxValue, 128, 128));
      Color innerColour4 = Color.FromArgb(128, Color.FromArgb((int) byte.MaxValue, 192, 192));
      if (this.WorldZoom >= 8.0 && (this.interVillageLines.Count > 0 || this.dynamicVillageLines.Count > 0))
      {
        if ((this.m_worldScale - 2.0) / 3.0 < 0.30000001192092896)
          ;
        this.gfx.startPoly();
        if (InterfaceMgr.Instance.WorldMapMode == 0)
        {
          foreach (WorldMap.InterVillageLine interVillageLine in this.interVillageLines)
          {
            if (interVillageLine.style == 2)
              this.drawInterVillageLine(interVillageLine, screenRect, outerColour2, innerColour2);
            else if (interVillageLine.style == 1)
              this.drawInterVillageLine(interVillageLine, screenRect, outerColour1, innerColour1);
          }
        }
        foreach (WorldMap.InterVillageLine dynamicVillageLine in this.dynamicVillageLines)
        {
          if (dynamicVillageLine.style == 3)
            this.drawInterVillageLine(dynamicVillageLine, screenRect, outerColour3, innerColour3);
          else if (dynamicVillageLine.style == 4)
            this.drawInterVillageLine(dynamicVillageLine, screenRect, outerColour4, innerColour4);
          else if (dynamicVillageLine.style == 5)
            this.drawInterVillageLine(dynamicVillageLine, screenRect, ARGBColors.Yellow, Color.FromArgb(64, (int) byte.MaxValue, 64));
          else if (dynamicVillageLine.style == 6)
          {
            double num = (double) this.pulse / 128.0;
            if (num > 1.0)
              num = 2.0 - num;
            Color innerColour5 = Color.FromArgb(96, (int) byte.MaxValue - (int) (191.0 * num), (int) byte.MaxValue, (int) (64.0 * num));
            Color outerColour5 = Color.FromArgb(96, ARGBColors.Yellow);
            this.drawInterVillageLine(dynamicVillageLine, screenRect, outerColour5, innerColour5);
          }
        }
        this.gfx.drawBufferedPolygons();
      }
      this.dynamicVillageLines.Clear();
    }

    private void drawInterVillageLine(
      WorldMap.InterVillageLine line,
      RectangleF screenRect,
      Color outerColour,
      Color innerColour)
    {
      PointF point = new PointF(line.end.X - line.start.X, line.end.Y - line.start.Y);
      PointF pointF1 = point;
      float num1 = (float) Math.Sqrt((double) point.X * (double) point.X + (double) point.Y * (double) point.Y) * line.widthScalar;
      point.X /= num1;
      point.Y /= num1;
      PointF pointF2 = this.gfx.rotatePoint(point, -90);
      PointF pointF3 = this.gfx.rotatePoint(point, 90);
      if (line.style == 5 || line.style == 6)
      {
        pointF2 = this.gfx.rotatePoint(point, -135);
        pointF3 = this.gfx.rotatePoint(point, 135);
        pointF2.X *= 1.2f;
        pointF2.Y *= 1.2f;
        pointF3.X *= 1.2f;
        pointF3.Y *= 1.2f;
        pointF2.X += line.end.X - point.X;
        pointF2.Y += line.end.Y - point.Y;
        pointF3.X += line.end.X - point.X;
        pointF3.Y += line.end.Y - point.Y;
      }
      else
      {
        pointF2.X += line.end.X;
        pointF2.Y += line.end.Y;
        pointF3.X += line.end.X;
        pointF3.Y += line.end.Y;
      }
      if ((double) pointF1.X / 5.0 * ((double) pointF1.X / 5.0) + (double) pointF1.Y / 5.0 * ((double) pointF1.Y / 5.0) < (double) point.X * 1.5 * ((double) point.X * 1.5) + (double) point.Y * 1.5 * ((double) point.Y * 1.5) && line.minLength)
      {
        point.X *= 1.5f;
        point.Y *= 1.5f;
        point.X = line.end.X - point.X;
        point.Y = line.end.Y - point.Y;
      }
      else
      {
        point.X = line.end.X - pointF1.X / 5f;
        point.Y = line.end.Y - pointF1.Y / 5f;
      }
      float num2 = (pointF2.X - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
      float num3 = (pointF2.Y - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
      float x3 = (pointF3.X - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
      float y3 = (pointF3.Y - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
      float x2_1 = (line.start.X - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
      float y2_1 = (line.start.Y - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
      if (line.style == 5 || line.style == 6)
      {
        float x1 = (line.end.X - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
        float y1 = (line.end.Y - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
        this.gfx.addTriangle(Color.FromArgb(0, (int) outerColour.R, (int) outerColour.G, (int) outerColour.B), outerColour, outerColour, x1, y1, num2, num3, x3, y3, 2);
        this.gfx.addTriangle(outerColour, innerColour, outerColour, num2, num3, x2_1, y2_1, x3, y3, 2);
      }
      else
      {
        float x2_2 = (point.X - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
        float y2_2 = (point.Y - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
        this.gfx.addTriangle(outerColour, num2, num3, x2_1, y2_1, x3, y3, 2);
        this.gfx.addTriangle(innerColour, num2, num3, x2_2, y2_2, x3, y3, 3);
      }
    }

    private void clearInterVillageLines() => this.interVillageLines.Clear();

    private void manageDynamicLines()
    {
      if (InterfaceMgr.Instance.MapSelectedArmy >= 0L)
      {
        WorldMap.LocalArmyData armyById = this.GetArmyByID(InterfaceMgr.Instance.MapSelectedArmy);
        if (armyById != null && armyById.targetVillageID >= 0 && armyById.travelFromVillageID >= 0)
        {
          PointF start = armyById.TargetPoint();
          PointF pointF = new PointF((float) armyById.displayX, (float) armyById.displayY);
          PointF end = armyById.BasePoint();
          this.addDynamicInterVillageLine(start, pointF, 3, 1.2f);
          this.addDynamicInterVillageLine(pointF, end, 4, 1.2f);
        }
      }
      if (InterfaceMgr.Instance.MapSelectedReinforcement >= 0L)
      {
        WorldMap.LocalArmyData reinforcement = (WorldMap.LocalArmyData) this.reinforcementArray[InterfaceMgr.Instance.MapSelectedReinforcement];
        if (reinforcement != null && reinforcement.targetVillageID >= 0 && reinforcement.travelFromVillageID >= 0)
        {
          PointF start = reinforcement.TargetPoint();
          PointF pointF = new PointF((float) reinforcement.displayX, (float) reinforcement.displayY);
          PointF end = reinforcement.BasePoint();
          this.addDynamicInterVillageLine(start, pointF, 3, 1.2f);
          this.addDynamicInterVillageLine(pointF, end, 4, 1.2f);
        }
      }
      if (InterfaceMgr.Instance.MapSelectedTrader >= 0L)
      {
        WorldMap.LocalTrader trader = (WorldMap.LocalTrader) this.traderArray[InterfaceMgr.Instance.MapSelectedTrader];
        if (trader != null && trader.trader.targetVillageID >= 0 && trader.trader.homeVillageID >= 0)
        {
          PointF start = trader.TargetPoint();
          PointF pointF = new PointF((float) trader.displayX, (float) trader.displayY);
          PointF end = trader.BasePoint();
          this.addDynamicInterVillageLine(start, pointF, 3, 1.2f);
          this.addDynamicInterVillageLine(pointF, end, 4, 1.2f);
        }
      }
      if (InterfaceMgr.Instance.MapSelectedPerson >= 0L)
      {
        WorldMap.LocalPerson person = (WorldMap.LocalPerson) this.personArray[InterfaceMgr.Instance.MapSelectedPerson];
        if (person != null && person.person.targetVillageID >= 0 && person.person.homeVillageID >= 0)
        {
          PointF start = person.TargetPoint();
          PointF pointF = new PointF((float) person.displayX, (float) person.displayY);
          PointF end = person.BasePoint();
          this.addDynamicInterVillageLine(start, pointF, 3, 1.2f);
          this.addDynamicInterVillageLine(pointF, end, 4, 1.2f);
        }
      }
      if (InterfaceMgr.Instance.SelectedVassalVillage >= 0)
      {
        if (InterfaceMgr.Instance.SelectedVillage >= 0 && InterfaceMgr.Instance.SelectedVassalVillage >= 0 && InterfaceMgr.Instance.SelectedVillage < this.villageList.Length && InterfaceMgr.Instance.SelectedVassalVillage < this.villageList.Length && InterfaceMgr.Instance.SelectedVillage != InterfaceMgr.Instance.SelectedVassalVillage)
        {
          VillageData village1 = this.villageList[InterfaceMgr.Instance.SelectedVassalVillage];
          VillageData village2 = this.villageList[InterfaceMgr.Instance.SelectedVillage];
          float scalar = (float) (2.0 - (double) village1.villageInfo / 120.0);
          this.addDynamicInterVillageLine((PointF) new Point((int) village2.x, (int) village2.y), (PointF) new Point((int) village1.x, (int) village1.y), 5, scalar);
        }
      }
      else if (InterfaceMgr.Instance.SelectedVillage >= 0 && InterfaceMgr.Instance.OwnSelectedVillage >= 0 && InterfaceMgr.Instance.SelectedVillage < this.villageList.Length && InterfaceMgr.Instance.OwnSelectedVillage < this.villageList.Length && InterfaceMgr.Instance.SelectedVillage != InterfaceMgr.Instance.OwnSelectedVillage)
      {
        VillageData village3 = this.villageList[InterfaceMgr.Instance.OwnSelectedVillage];
        VillageData village4 = this.villageList[InterfaceMgr.Instance.SelectedVillage];
        float scalar = (float) (2.0 - (double) village3.villageInfo / 120.0);
        this.addDynamicInterVillageLine((PointF) new Point((int) village4.x, (int) village4.y), (PointF) new Point((int) village3.x, (int) village3.y), 5, scalar);
      }
      if (this.m_rolloverTargetVillage < 0 || InterfaceMgr.Instance.OwnSelectedVillage < 0 || this.m_rolloverTargetVillage >= this.villageList.Length || InterfaceMgr.Instance.OwnSelectedVillage >= this.villageList.Length || InterfaceMgr.Instance.OwnSelectedVillage == this.m_rolloverTargetVillage)
        return;
      VillageData village5 = this.villageList[InterfaceMgr.Instance.OwnSelectedVillage];
      VillageData village6 = this.villageList[this.m_rolloverTargetVillage];
      float scalar1 = (float) (2.0 - (double) village5.villageInfo / 120.0);
      this.addDynamicInterVillageLine((PointF) new Point((int) village6.x, (int) village6.y), (PointF) new Point((int) village5.x, (int) village5.y), 6, scalar1);
    }

    public void drawRangeCircle(RectangleF screenRect)
    {
      if (InterfaceMgr.Instance.OwnSelectedVillage < 0 && InterfaceMgr.Instance.SelectedVassalVillage < 0 || !this.isSpecial(InterfaceMgr.Instance.SelectedVillage) || this.getSpecial(InterfaceMgr.Instance.SelectedVillage) == 21 || this.getSpecial(InterfaceMgr.Instance.SelectedVillage) == 20)
        return;
      int index = InterfaceMgr.Instance.OwnSelectedVillage;
      if (InterfaceMgr.Instance.SelectedVassalVillage >= 0)
        index = InterfaceMgr.Instance.SelectedVassalVillage;
      if (index < 0 || index >= this.villageList.Length)
        return;
      VillageData village = this.villageList[index];
      this.drawRangeCircle((PointF) new Point((int) village.x, (int) village.y), (float) CardTypes.adjustScoutingHonourRange(GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData.BaseScoutHonourRange), screenRect);
    }

    public void drawRangeCircle(PointF centre, float radius, RectangleF screenRect)
    {
      int num1 = (int) ((double) radius * 2.0 * 10.0 / (this.m_worldZoomInverted + 10.0));
      if (num1 < 32)
        num1 = 32;
      float num2 = 6.28318f / (float) num1;
      float num3 = 0.0f;
      float num4 = 0.0f;
      Color color = Color.FromArgb(80, (int) byte.MaxValue, 0, 0);
      for (int index = -1; index < num1; ++index)
      {
        float num5 = (float) index * num2;
        float num6 = centre.X + radius * (float) Math.Cos((double) num5);
        float num7 = centre.Y - radius * (float) Math.Sin((double) num5);
        if (index >= 0)
        {
          this.addCircleTriangle(screenRect, num3, num4, num6, num7, centre, color);
          this.addCircleTriangle(screenRect, num6, num7, num3, num4, centre, color);
        }
        num3 = num6;
        num4 = num7;
      }
      this.gfx.drawBufferedPolygons();
    }

    private void addCircleTriangle(
      RectangleF screenRect,
      float x1,
      float y1,
      float x2,
      float y2,
      PointF centre,
      Color color)
    {
      x1 = (x1 - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
      y1 = (y1 - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
      float x3 = (centre.X - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
      float y3 = (centre.Y - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
      x2 = (x2 - screenRect.Left) / screenRect.Width * (float) this.m_screenWidth;
      y2 = (y2 - screenRect.Top) / screenRect.Height * (float) this.m_screenHeight;
      this.gfx.addTriangle(color, x1, y1, x2, y2, x3, y3);
    }

    public double ZoomChange => this.m_zoomChangeThisFrame;

    public void mouseNotDown(Point mousePos)
    {
      if (!this.m_leftMouseHeldDown)
        return;
      if (!this.isDraggingMap)
      {
        double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
        bool doubleClick = false;
        if (currentMilliseconds - this.m_doubleClickTime < 300.0 && Math.Abs(mousePos.X - this.m_doubleClickMousePos.X) < 3 && Math.Abs(mousePos.Y - this.m_doubleClickMousePos.Y) < 3)
          doubleClick = true;
        this.windowClicked(mousePos, doubleClick);
        this.m_doubleClickTime = currentMilliseconds;
        this.m_doubleClickMousePos = mousePos;
      }
      this.stopDrag();
      InterfaceMgr.Instance.mouseUpDXPlaybackBar(mousePos);
    }

    public bool holdingLeftMouse() => this.m_leftMouseHeldDown;

    public bool DragModeActive => this.dragMode;

    public void startDrag(Point mousePos)
    {
      this.m_lastMousePressedTime = DXTimer.GetCurrentMilliseconds();
      this.m_leftMouseHeldDown = true;
      this.m_baseMousePos = mousePos;
      this.m_baseScreenX = this.m_screenCentreX;
      this.m_baseScreenY = this.m_screenCentreY;
      this.isDraggingMap = false;
      InterfaceMgr.Instance.mouseDownDXPlaybackBar(mousePos);
      this.dragMode = true;
    }

    public void stopDrag()
    {
      this.m_leftMouseHeldDown = false;
      this.isDraggingMap = false;
      this.dragMode = false;
    }

    public void dragMapRelative(Point lastMousePos, Point newMousePos)
    {
      double mapPosX1 = 0.0;
      double mapPosY1 = 0.0;
      this.getMapCoords(newMousePos, ref mapPosX1, ref mapPosY1);
      double mapPosX2 = 0.0;
      double mapPosY2 = 0.0;
      this.getMapCoords(lastMousePos, ref mapPosX2, ref mapPosY2);
      this.m_screenCentreX += mapPosX2 - mapPosX1;
      this.m_screenCentreY += mapPosY2 - mapPosY1;
    }

    public void dragMap(Point mousePos)
    {
      this.m_zooming = false;
      this.isDraggingMap = true;
      this.m_screenCentreX = this.m_baseScreenX;
      this.m_screenCentreY = this.m_baseScreenY;
      double mapPosX1 = 0.0;
      double mapPosY1 = 0.0;
      this.getMapCoords(mousePos, ref mapPosX1, ref mapPosY1);
      double mapPosX2 = 0.0;
      double mapPosY2 = 0.0;
      this.getMapCoords(this.m_baseMousePos, ref mapPosX2, ref mapPosY2);
      this.m_screenCentreX = this.m_baseScreenX + mapPosX2 - mapPosX1;
      this.m_screenCentreY = this.m_baseScreenY + mapPosY2 - mapPosY1;
      this.moveMap(0.0, 0.0);
      this.centreMap(false);
    }

    public void leftMouseDown(Point mousePos)
    {
      if (!this.holdingLeftMouse())
      {
        this.startDrag(mousePos);
      }
      else
      {
        if (DXTimer.GetCurrentMilliseconds() - this.m_lastMousePressedTime <= 250.0 && Math.Abs(this.m_baseMousePos.X - mousePos.X) <= 3 && Math.Abs(this.m_baseMousePos.Y - mousePos.Y) <= 3)
          return;
        bool flag = true;
        if (!this.isDraggingMap && Math.Abs(this.m_baseMousePos.X - mousePos.X) <= 3 && Math.Abs(this.m_baseMousePos.Y - mousePos.Y) <= 3 && this.WorldZoom > 18.5)
        {
          double bestDist1 = 100000.0;
          int num1 = this.findNearestVillageFromScreenPos(mousePos, ref bestDist1);
          if (bestDist1 > 4.0)
            num1 = -1;
          long num2 = -1;
          long num3 = -1;
          long num4 = -1;
          long num5 = -1;
          if (num1 < 0 && InterfaceMgr.Instance.WorldMapMode == 0)
          {
            double bestDist2 = 0.0;
            num2 = this.findNearestArmyFromScreenPos(mousePos, ref bestDist2);
            if (num2 >= 0L && bestDist2 > 4.0)
              num2 = -1L;
            if (num2 < 0L)
            {
              double bestDist3 = 0.0;
              num3 = this.findNearestTraderFromScreenPos(mousePos, ref bestDist3);
              if (num3 >= 0L && bestDist3 > 4.0)
                num3 = -1L;
              if (num3 < 0L)
              {
                double bestDist4 = 0.0;
                num4 = this.findNearestReinforcementFromScreenPos(mousePos, ref bestDist4);
                if (num4 >= 0L && bestDist4 > 4.0)
                  num4 = -1L;
                if (num4 < 0L)
                {
                  double bestDist5 = 0.0;
                  num5 = this.findNearestPersonFromScreenPos(mousePos, ref bestDist5);
                  if (num5 >= 0L && bestDist5 > 4.0)
                    num5 = -1L;
                }
              }
            }
          }
          if (num2 >= 0L || num1 >= 0 || num3 >= 0L || num4 >= 0L || num5 >= 0L)
            flag = false;
        }
        if (!flag)
          return;
        this.dragMap(mousePos);
      }
    }

    public int lastClickedVillage()
    {
      double bestDist = -1.0;
      return this.findNearestVillageFromScreenPosAnyVis(this.lastClickedLocation, ref bestDist);
    }

    public void clickOnMapIcon(Point mousePos, bool doubleClick)
    {
      bool flag1 = true;
      double bestDist1 = 0.0;
      double bestDist2 = 0.0;
      double bestDist3 = 0.0;
      double bestDist4 = 0.0;
      double bestDist5 = 0.0;
      int villageFromScreenPos = this.findNearestVillageFromScreenPos(mousePos, ref bestDist1);
      this.LastClickedVillage = villageFromScreenPos;
      if (this.WorldEnded && villageFromScreenPos > 0 && this.villageList[villageFromScreenPos].Capital)
        this.fwDisplayClock = 0.0f;
      bool flag2 = false;
      if (villageFromScreenPos >= 0 && this.WorldZoom > 18.5 && this.isOnlyOverVillageShield(villageFromScreenPos, (PointF) mousePos))
        flag2 = true;
      if ((villageFromScreenPos == -1 || this.WorldZoom > 18.5 || this.PickingStartCounty) && !flag2 && InterfaceMgr.Instance.WorldMapMode == 0)
      {
        long armyFromScreenPos = this.findNearestArmyFromScreenPos(mousePos, ref bestDist2);
        if (armyFromScreenPos >= 0L && (villageFromScreenPos == -1 || bestDist2 <= bestDist1))
        {
          WorldMap.LocalArmyData armyById = this.GetArmyByID(armyFromScreenPos);
          if (armyById.numScouts > 0)
            GameEngine.Instance.playInterfaceSound("WorldMap_scouts", false);
          else
            GameEngine.Instance.playInterfaceSound("WorldMap_army", false);
          if (armyById.attackType != 17 && flag1)
            this.setZooming(27.0, armyById.displayX, armyById.displayY);
          InterfaceMgr.Instance.closeFilterPanel();
          InterfaceMgr.Instance.closeSelectedVillagePanel();
          InterfaceMgr.Instance.closeTraderInfoPanel();
          InterfaceMgr.Instance.closeReinforcementSelectedPanel();
          InterfaceMgr.Instance.closePersonInfoPanel();
          InterfaceMgr.Instance.clearAndCloseUserInfo();
          InterfaceMgr.Instance.displayArmySelectPanel(armyFromScreenPos);
          return;
        }
        long reinforcementFromScreenPos = this.findNearestReinforcementFromScreenPos(mousePos, ref bestDist3);
        if (reinforcementFromScreenPos >= 0L && (villageFromScreenPos == -1 || bestDist3 <= bestDist1))
        {
          GameEngine.Instance.playInterfaceSound("WorldMap_reinforcement", false);
          WorldMap.LocalArmyData reinforcement = (WorldMap.LocalArmyData) this.reinforcementArray[reinforcementFromScreenPos];
          if (flag1)
            this.setZooming(27.0, reinforcement.displayX, reinforcement.displayY);
          InterfaceMgr.Instance.closeFilterPanel();
          InterfaceMgr.Instance.closeSelectedVillagePanel();
          InterfaceMgr.Instance.closeTraderInfoPanel();
          InterfaceMgr.Instance.closeArmySelectedPanel();
          InterfaceMgr.Instance.closePersonInfoPanel();
          InterfaceMgr.Instance.clearAndCloseUserInfo();
          InterfaceMgr.Instance.displayReinforcementSelectPanel(reinforcementFromScreenPos);
          return;
        }
        long traderFromScreenPos = this.findNearestTraderFromScreenPos(mousePos, ref bestDist4);
        if (traderFromScreenPos >= 0L && (villageFromScreenPos == -1 || bestDist4 <= bestDist1))
        {
          GameEngine.Instance.playInterfaceSound("WorldMap_trader", false);
          WorldMap.LocalTrader trader = (WorldMap.LocalTrader) this.traderArray[traderFromScreenPos];
          if (flag1)
            this.setZooming(27.0, trader.displayX, trader.displayY);
          InterfaceMgr.Instance.closeFilterPanel();
          InterfaceMgr.Instance.closeSelectedVillagePanel();
          InterfaceMgr.Instance.closeArmySelectedPanel();
          InterfaceMgr.Instance.closeReinforcementSelectedPanel();
          InterfaceMgr.Instance.closePersonInfoPanel();
          InterfaceMgr.Instance.clearAndCloseUserInfo();
          InterfaceMgr.Instance.displayTraderInfoPanel(traderFromScreenPos);
          return;
        }
        long personFromScreenPos = this.findNearestPersonFromScreenPos(mousePos, ref bestDist5);
        if (personFromScreenPos >= 0L && (villageFromScreenPos == -1 || bestDist5 <= bestDist1))
        {
          WorldMap.LocalPerson person = (WorldMap.LocalPerson) this.personArray[personFromScreenPos];
          if (person.person.personType == 100)
            GameEngine.Instance.playInterfaceSound("WorldMap_rat", false);
          else
            GameEngine.Instance.playInterfaceSound("WorldMap_monk", false);
          if (flag1)
            this.setZooming(27.0, person.displayX, person.displayY);
          InterfaceMgr.Instance.closeFilterPanel();
          InterfaceMgr.Instance.closeSelectedVillagePanel();
          InterfaceMgr.Instance.closeArmySelectedPanel();
          InterfaceMgr.Instance.closeReinforcementSelectedPanel();
          InterfaceMgr.Instance.closeTraderInfoPanel();
          InterfaceMgr.Instance.clearAndCloseUserInfo();
          InterfaceMgr.Instance.displayPersonInfoPanel(personFromScreenPos);
          return;
        }
      }
      InterfaceMgr.Instance.closeArmySelectedPanel();
      InterfaceMgr.Instance.closeTraderInfoPanel();
      InterfaceMgr.Instance.closeReinforcementSelectedPanel();
      InterfaceMgr.Instance.closePersonInfoPanel();
      if (villageFromScreenPos >= 0)
      {
        bool flag3 = true;
        if (this.PickingStartCounty && (this.worldMapFilter.showVillage(villageFromScreenPos) >= 0 || GameEngine.Instance.World.isCountyCapital(villageFromScreenPos) && GameEngine.Instance.World.worldMapFilter.FilterMode == 11))
          InterfaceMgr.Instance.displaySelectedVillagePanel(villageFromScreenPos, doubleClick, true, false, false);
        if (this.villageList[villageFromScreenPos].special == 30)
          InterfaceMgr.Instance.displaySelectedVillagePanel(villageFromScreenPos, doubleClick, true, false, false);
        else if (this.WorldZoom < 1.1)
        {
          int countyId = (int) this.villageList[villageFromScreenPos].countyID;
          if (countyId < 0)
            return;
          PointF centrePoint = this.countyList[countyId].getCentrePoint();
          if (flag1)
            this.setZooming(2.1, (double) centrePoint.X, (double) centrePoint.Y);
          GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
        }
        else if (this.WorldZoom < 3.1)
        {
          int countyId = (int) this.villageList[villageFromScreenPos].countyID;
          if (countyId < 0)
            return;
          PointF centrePoint = this.countyList[countyId].getCentrePoint();
          if (flag1)
            this.setZooming(3.5, (double) centrePoint.X, (double) centrePoint.Y);
          GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
        }
        else if (this.WorldZoom < 5.5)
        {
          int regionId = (int) this.villageList[villageFromScreenPos].regionID;
          if (regionId < 0)
            return;
          PointF centrePoint = this.regionList[regionId].getCentrePoint();
          if (flag1)
            this.setZooming(6.01, (double) centrePoint.X, (double) centrePoint.Y);
          GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
        }
        else if (this.WorldZoom < 8.0)
        {
          int regionId = (int) this.villageList[villageFromScreenPos].regionID;
          if (regionId < 0)
            return;
          PointF centrePoint = this.regionList[regionId].getCentrePoint();
          if (flag1)
            this.setZooming(9.51, (double) centrePoint.X, (double) centrePoint.Y);
          GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
        }
        else if (this.WorldZoom > 14.5)
        {
          if (flag1)
            this.setZooming(27.0, (double) this.villageList[villageFromScreenPos].x, (double) this.villageList[villageFromScreenPos].y);
          if (this.m_worldZoomInverted > 0.10000000149011612)
            GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
          if (this.isVillageVisible(villageFromScreenPos))
          {
            if (this.isCapital(villageFromScreenPos))
            {
              if (this.isRegionCapital(villageFromScreenPos))
              {
                if (this.isUserVillage(villageFromScreenPos))
                  GameEngine.Instance.playInterfaceSound("WorldMap_parish_capital_clicked_player_owned", false);
                else
                  GameEngine.Instance.playInterfaceSound("WorldMap_parish_capital_clicked", false);
              }
              if (this.isCountyCapital(villageFromScreenPos))
              {
                if (this.isUserVillage(villageFromScreenPos))
                  GameEngine.Instance.playInterfaceSound("WorldMap_county_capital_clicked_player_owned", false);
                else
                  GameEngine.Instance.playInterfaceSound("WorldMap_county_capital_clicked", false);
              }
              if (this.isProvinceCapital(villageFromScreenPos))
              {
                if (this.isUserVillage(villageFromScreenPos))
                  GameEngine.Instance.playInterfaceSound("WorldMap_province_capital_clicked_player_owned", false);
                else
                  GameEngine.Instance.playInterfaceSound("WorldMap_province_capital_clicked", false);
              }
              if (this.isCountryCapital(villageFromScreenPos))
              {
                if (this.isUserVillage(villageFromScreenPos))
                  GameEngine.Instance.playInterfaceSound("WorldMap_country_capital_clicked_player_owned", false);
                else
                  GameEngine.Instance.playInterfaceSound("WorldMap_country_capital_clicked", false);
              }
            }
            else if (this.isSpecial(villageFromScreenPos))
            {
              int special = this.getSpecial(villageFromScreenPos);
              if (SpecialVillageTypes.IS_TREASURE_CASTLE(special) || SpecialVillageTypes.IS_ROYAL_TOWER(special))
              {
                GameEngine.Instance.playInterfaceSound("WorldMap_AI_Castle_clicked", false);
              }
              else
              {
                switch (special)
                {
                  case 3:
                    GameEngine.Instance.playInterfaceSound("WorldMap_bandit_camp_clicked", false);
                    break;
                  case 4:
                    GameEngine.Instance.playInterfaceSound("WorldMap_bandit_camp_destroyed_clicked", false);
                    break;
                  case 5:
                    GameEngine.Instance.playInterfaceSound("WorldMap_wolf_lair_clicked", false);
                    break;
                  case 6:
                    GameEngine.Instance.playInterfaceSound("WorldMap_wolf_lair_destroyed_clicked", false);
                    break;
                  case 7:
                  case 9:
                  case 11:
                  case 13:
                  case 15:
                  case 17:
                    GameEngine.Instance.playInterfaceSound("WorldMap_AI_Castle_clicked", false);
                    break;
                  case 8:
                  case 10:
                  case 12:
                  case 14:
                  case 16:
                  case 18:
                  case 40:
                    GameEngine.Instance.playInterfaceSound("WorldMap_AI_Castle_destroyed_clicked", false);
                    break;
                  case 21:
                    GameEngine.Instance.playInterfaceSound("WorldMap_enemy_camp_clicked", false);
                    break;
                }
              }
              if (special == 100)
                GameEngine.Instance.playInterfaceSound("WorldMap_unknown_resource_stash_clicked", false);
              else if (special > 100 && special <= 199)
              {
                switch (special)
                {
                  case 106:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_wood", false);
                    break;
                  case 107:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_stone", false);
                    break;
                  case 108:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_iron", false);
                    break;
                  case 109:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_pitch", false);
                    break;
                  case 112:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_ale", false);
                    break;
                  case 113:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_apple", false);
                    break;
                  case 114:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_bread", false);
                    break;
                  case 115:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_veg", false);
                    break;
                  case 116:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_meat", false);
                    break;
                  case 117:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_cheese", false);
                    break;
                  case 118:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_fish", false);
                    break;
                  case 119:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_clothes", false);
                    break;
                  case 121:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_furniture", false);
                    break;
                  case 122:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_venison", false);
                    break;
                  case 123:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_salt", false);
                    break;
                  case 124:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_spices", false);
                    break;
                  case 125:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_silk", false);
                    break;
                  case 126:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_metalware", false);
                    break;
                  case 133:
                    GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_wine", false);
                    break;
                }
              }
            }
            else if (this.isUserVillage(villageFromScreenPos))
              GameEngine.Instance.playInterfaceSound("WorldMap_user_village_clicked", false);
            else if (this.getVillageUserID(villageFromScreenPos) >= 0)
              GameEngine.Instance.playInterfaceSound("WorldMap_normal_village_clicked", false);
            else
              GameEngine.Instance.playInterfaceSound("WorldMap_charter_clicked", false);
          }
          if (InterfaceMgr.Instance.OwnSelectedVillage >= 0 || InterfaceMgr.Instance.WorldMapMode == 0 && this.isOverVillageShield(villageFromScreenPos, (PointF) mousePos, true))
          {
            if (InterfaceMgr.Instance.WorldMapMode == 1)
            {
              VillageData villageData = this.getVillageData(villageFromScreenPos);
              if (!this.isSpecial(villageFromScreenPos) && (this.isCapital(villageFromScreenPos) || villageData.userID >= 0))
              {
                flag3 = false;
                InterfaceMgr.Instance.SelectedVillage = villageFromScreenPos;
                InterfaceMgr.Instance.setTradeWithVillage(villageFromScreenPos);
              }
              else
              {
                InterfaceMgr.Instance.SelectedVillage = -1;
                InterfaceMgr.Instance.setTradeWithVillage(-1);
              }
            }
            else if (InterfaceMgr.Instance.WorldMapMode == 2)
            {
              if (this.isCapital(villageFromScreenPos))
              {
                bool flag4 = true;
                if (!this.allowExchangeTrade(villageFromScreenPos, InterfaceMgr.Instance.StockExchangeBuyingVillage))
                  flag4 = false;
                if (flag4)
                {
                  flag3 = false;
                  InterfaceMgr.Instance.SelectedVillage = villageFromScreenPos;
                  InterfaceMgr.Instance.setStockExchangeSidePanelVillage(villageFromScreenPos);
                }
                else
                {
                  InterfaceMgr.Instance.SelectedVillage = -1;
                  InterfaceMgr.Instance.setStockExchangeSidePanelVillage(-1);
                }
              }
            }
            else if (InterfaceMgr.Instance.WorldMapMode == 3)
            {
              VillageData villageData = this.getVillageData(villageFromScreenPos);
              bool flag5 = true;
              if (this.isSpecial(villageFromScreenPos) && !this.isAttackableSpecial(villageFromScreenPos))
                flag5 = false;
              else if (!this.isSpecial(villageFromScreenPos) && villageData.userID < 0 && !this.isCapital(villageFromScreenPos))
                flag5 = false;
              if (flag5)
              {
                flag3 = false;
                InterfaceMgr.Instance.SelectedVillage = villageFromScreenPos;
                InterfaceMgr.Instance.setAttackTargetSidePanelVillage(villageFromScreenPos);
              }
              else
              {
                InterfaceMgr.Instance.SelectedVillage = -1;
                InterfaceMgr.Instance.setAttackTargetSidePanelVillage(-1);
              }
            }
            else if (InterfaceMgr.Instance.WorldMapMode == 4)
            {
              VillageData villageData = this.getVillageData(villageFromScreenPos);
              bool flag6 = true;
              if (this.isSpecial(villageFromScreenPos) && !this.isScoutableSpecial(villageFromScreenPos))
                flag6 = false;
              else if (!this.isSpecial(villageFromScreenPos) && villageData.userID < 0 && !this.isCapital(villageFromScreenPos))
                flag6 = false;
              if (flag6)
              {
                flag3 = false;
                InterfaceMgr.Instance.SelectedVillage = villageFromScreenPos;
                InterfaceMgr.Instance.setScoutTargetSidePanelVillage(villageFromScreenPos);
              }
              else
              {
                InterfaceMgr.Instance.SelectedVillage = -1;
                InterfaceMgr.Instance.setScoutTargetSidePanelVillage(-1);
              }
            }
            else if (InterfaceMgr.Instance.WorldMapMode == 5)
            {
              VillageData villageData = this.getVillageData(villageFromScreenPos);
              if (!this.isCapital(villageFromScreenPos) && !this.isSpecial(villageFromScreenPos) && villageData.userID >= 0)
              {
                flag3 = false;
                InterfaceMgr.Instance.SelectedVillage = villageFromScreenPos;
                InterfaceMgr.Instance.setReinforcementTargetSidePanelVillage(villageFromScreenPos);
              }
              else
              {
                InterfaceMgr.Instance.SelectedVillage = -1;
                InterfaceMgr.Instance.setReinforcementTargetSidePanelVillage(-1);
              }
            }
            else if (InterfaceMgr.Instance.WorldMapMode != 6)
            {
              if (InterfaceMgr.Instance.WorldMapMode == 7)
              {
                VillageData villageData = this.getVillageData(villageFromScreenPos);
                bool flag7 = true;
                if (this.isCapital(villageFromScreenPos))
                  flag7 = false;
                else if (this.isSpecial(villageFromScreenPos) && !this.isAttackableSpecial(villageFromScreenPos))
                  flag7 = false;
                else if (!this.isSpecial(villageFromScreenPos) && villageData.userID < 0)
                  flag7 = false;
                if (flag7)
                {
                  flag3 = false;
                  InterfaceMgr.Instance.SelectedVillage = villageFromScreenPos;
                  InterfaceMgr.Instance.setVassalSelectSidePanelVillage(villageFromScreenPos);
                }
                else
                {
                  InterfaceMgr.Instance.SelectedVillage = -1;
                  InterfaceMgr.Instance.setVassalSelectSidePanelVillage(-1);
                }
              }
              else if (InterfaceMgr.Instance.WorldMapMode == 9)
              {
                VillageData villageData = this.getVillageData(villageFromScreenPos);
                if (!this.isSpecial(villageFromScreenPos) && (villageData.userID >= 0 || villageData.Capital))
                {
                  flag3 = false;
                  InterfaceMgr.Instance.SelectedVillage = villageFromScreenPos;
                  InterfaceMgr.Instance.setMonkSelectSidePanelVillage(villageFromScreenPos);
                }
                else
                {
                  InterfaceMgr.Instance.SelectedVillage = -1;
                  InterfaceMgr.Instance.setMonkSelectSidePanelVillage(-1);
                }
              }
              else
              {
                bool forceSelfClick = this.isOverVillageShield(villageFromScreenPos, (PointF) mousePos, false);
                flag3 = false;
                if (this.worldMapFilter.showVillage(villageFromScreenPos) >= 0)
                  InterfaceMgr.Instance.displaySelectedVillagePanel(villageFromScreenPos, doubleClick, true, forceSelfClick, false);
              }
            }
          }
          else
          {
            InterfaceMgr.Instance.clearAndCloseUserInfo();
            InterfaceMgr.Instance.displaySelectedVillagePanel(villageFromScreenPos, doubleClick, true, false, true);
            flag3 = false;
          }
          if (!flag3)
            return;
          InterfaceMgr.Instance.closeSelectedVillagePanelButNotSelect();
        }
        else
        {
          GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
          if (!flag1)
            return;
          this.setZooming(27.0, (double) this.villageList[villageFromScreenPos].x, (double) this.villageList[villageFromScreenPos].y);
        }
      }
      else
      {
        double mapPosX = 0.0;
        double mapPosY = 0.0;
        this.getMapCoords(mousePos, ref mapPosX, ref mapPosY);
        if (this.WorldZoom < 1.1 && flag1)
        {
          this.setZooming(2.1, mapPosX, mapPosY);
          GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
        }
        else if (this.WorldZoom < 3.1 && flag1)
        {
          this.setZooming(3.5, mapPosX, mapPosY);
          GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
        }
        else if (this.WorldZoom < 5.5 && flag1)
        {
          this.setZooming(6.01, mapPosX, mapPosY);
          GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
        }
        else if (this.WorldZoom < 8.0 && flag1)
        {
          this.setZooming(9.51, mapPosX, mapPosY);
          GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
        }
        else
        {
          if (!flag1)
            return;
          this.setZooming(27.0, mapPosX, mapPosY);
          if (this.m_worldZoomInverted <= 0.10000000149011612)
            return;
          GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
        }
      }
    }

    public void windowClicked(Point mousePos, bool doubleClick)
    {
      this.lastClickedLocation = mousePos;
      if (this.WorldEnded)
      {
        if (this.SeventhAgeWorld && !GameEngine.Instance.LocalWorldData.AIWorld && mousePos.X < 90 && mousePos.Y >= 262 && mousePos.Y < 342)
        {
          InterfaceMgr.Instance.showRoyalTowerPanel();
          return;
        }
        if (InterfaceMgr.Instance.clickDXPlaybackBar(mousePos))
          return;
      }
      if (!this.WorldEnded)
      {
        if (InterfaceMgr.Instance.clickDXCardBar(mousePos) || InterfaceMgr.Instance.clickDXPlaybackBar(mousePos))
          return;
        if ((GameEngine.Instance.World.isTutorialActive() || Program.mySettings.showGameFeaturesScreenIcon) && !this.WorldEnded)
        {
          int num = this.gfx.ViewportHeight - 64;
          if (mousePos.X < 64 && mousePos.Y >= num)
          {
            if (GameEngine.Instance.World.isTutorialActive())
            {
              GameEngine.Instance.World.forceTutorialToBeShown();
              GameEngine.Instance.playInterfaceSound("WorldMap_tutorial_open");
              return;
            }
            if (Program.mySettings.showGameFeaturesScreenIcon)
            {
              PostTutorialWindow.CreatePostTutorialWindow(false);
              return;
            }
          }
        }
        if (mousePos.X < 70 && mousePos.Y >= 64 && mousePos.Y < 134)
        {
          InterfaceMgr.Instance.openFreeCardsPopup();
          GameEngine.Instance.playInterfaceSound("WorldMap_open_free_Cards");
          return;
        }
        int num1 = this.numWheelTypesAvailable();
        if (num1 > 0 && mousePos.X < 70 && mousePos.Y >= 144 && mousePos.Y < 214)
        {
          if (num1 == 1)
          {
            for (int index = -1; index < 5; ++index)
            {
              if (this.getTickets(index) > 0)
              {
                InterfaceMgr.Instance.openWheelPopup(index);
                break;
              }
            }
          }
          else
            InterfaceMgr.Instance.openWheelSelectPopup();
          GameEngine.Instance.playInterfaceSound("WorldMap_open_wheel");
          return;
        }
        if (mousePos.X > this.m_screenWidth - 30 && mousePos.Y > 30 && mousePos.Y < 60)
        {
          CustomSelfDrawPanel.WikiLinkControl.openHelpLink(0);
          return;
        }
        if (this.SeventhAgeWorld && !GameEngine.Instance.LocalWorldData.AIWorld && mousePos.X < 90 && mousePos.Y >= 262 && mousePos.Y < 342)
        {
          InterfaceMgr.Instance.showRoyalTowerPanel();
          return;
        }
        DateTime dateTime1 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.saleStartTime);
        DateTime dateTime2 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.saleEndTime);
        bool flag = dateTime1 <= VillageMap.getCurrentServerTime() && dateTime2 > VillageMap.getCurrentServerTime();
        if (flag && mousePos.X > this.m_screenWidth - 140 && mousePos.Y > this.m_screenHeight - 130)
        {
          InterfaceMgr.Instance.openPlayCardsWindow(0);
          ((PlayCardsWindow) InterfaceMgr.Instance.getCardWindow()).GetCrowns("&click=saleindicator");
          return;
        }
        if (GameEngine.Instance.cardsManager.PremiumOfferAvailable())
        {
          int num2 = this.m_screenHeight - 130;
          if (flag)
            num2 = this.m_screenHeight - 130 - 160;
          if (mousePos.X > this.m_screenWidth - 140 && mousePos.Y > num2 && mousePos.Y < num2 + 130)
          {
            InterfaceMgr.Instance.openPlayCardsWindow(0);
            ((PlayCardsWindow) InterfaceMgr.Instance.getCardWindow()).SwitchPanel(9);
            GameEngine.Instance.cardsManager.PremiumOffersViewed = true;
            return;
          }
        }
      }
      if (GameEngine.Instance.World.pendingPrizes != null && GameEngine.Instance.World.pendingPrizes.Count > 0)
      {
        if ((double) mousePos.X < (double) this.contestSprite.Width && (double) mousePos.Y > (double) this.contestSprite.PosY && (double) mousePos.Y < (double) this.contestSprite.PosY + (double) this.contestSprite.Height)
        {
          GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_leaderboard");
          PrizeClaimWindow.CreatePrizeClaimWindow();
          return;
        }
      }
      else if (GameEngine.Instance.World.contestID >= 0)
      {
        DateTime dateTime3 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.contestStartTime);
        DateTime dateTime4 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.contestEndTime);
        if (dateTime3 <= VillageMap.getCurrentServerTime() && dateTime4 > VillageMap.getCurrentServerTime() && (double) mousePos.X < (double) this.contestSprite.Width && (double) mousePos.Y > (double) this.contestSprite.PosY && (double) mousePos.Y < (double) this.contestSprite.PosY + (double) this.contestSprite.Height)
        {
          GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_leaderboard");
          InterfaceMgr.Instance.getMainTabBar().selectDummyTab(30);
          return;
        }
      }
      this.clickOnMapIcon(mousePos, doubleClick);
    }

    public void centerOverVillage(int villageID)
    {
      if (this.villageList == null || this.villageList.Length <= villageID || villageID < 0)
        return;
      this.m_screenCentreX = (double) this.villageList[villageID].x;
      this.m_screenCentreY = (double) this.villageList[villageID].y;
    }

    public void zoomToVillage(int villageID)
    {
      this.startMultiStageZoom(27.0, (double) this.villageList[villageID].x, (double) this.villageList[villageID].y);
    }

    public void zoomToArmy(long armyID)
    {
      WorldMap.LocalArmyData armyById = this.GetArmyByID(armyID);
      if (armyById == null)
        return;
      this.startMultiStageZoom(27.0, armyById.displayX, armyById.displayY);
    }

    public void zoomtoReinforcement(long armyID)
    {
      WorldMap.LocalArmyData reinformcementArmyById = this.GetReinformcementArmyByID(armyID);
      if (reinformcementArmyById == null)
        return;
      this.startMultiStageZoom(27.0, reinformcementArmyById.displayX, reinformcementArmyById.displayY);
    }

    public void zoomToTrader(long traderID)
    {
      WorldMap.LocalTrader trader = this.getTrader(traderID);
      if (trader == null)
        return;
      this.startMultiStageZoom(27.0, trader.displayX, trader.displayY);
    }

    public void zoomToPerson(long personID)
    {
      WorldMap.LocalPerson person = this.getPerson(personID);
      if (person == null)
        return;
      this.startMultiStageZoom(27.0, person.displayX, person.displayY);
    }

    public void capZoom(double cap)
    {
      this.m_zoomCap = cap;
      this.capZoomIFace((float) cap);
    }

    public double ZoomCap => this.m_zoomCap;

    public void zoomOut()
    {
      InterfaceMgr.Instance.clearAndCloseUserInfo();
      InterfaceMgr.Instance.closeSelectedVillagePanelButNotSelect();
      InterfaceMgr.Instance.closeArmySelectedPanel();
      InterfaceMgr.Instance.closeReinforcementSelectedPanel();
      InterfaceMgr.Instance.closePersonInfoPanel();
      InterfaceMgr.Instance.closeTraderInfoPanel();
      if (this.m_zooming && this.m_zoomDiff < 0.0)
        return;
      if (this.WorldZoom > 9.51)
      {
        this.setZooming(9.51, this.m_screenCentreX, this.m_screenCentreY);
        GameEngine.Instance.playInterfaceSound("WorldMap_zoomout");
      }
      else if (this.WorldZoom > 6.02)
      {
        this.setZooming(6.01, this.m_screenCentreX, this.m_screenCentreY);
        GameEngine.Instance.playInterfaceSound("WorldMap_zoomout");
      }
      else if (this.WorldZoom > 201.0 / 50.0)
      {
        this.setZooming(4.01, this.m_screenCentreX, this.m_screenCentreY);
        GameEngine.Instance.playInterfaceSound("WorldMap_zoomout");
      }
      else if (this.WorldZoom > 2.12)
      {
        this.setZooming(2.11, this.m_screenCentreX, this.m_screenCentreY);
        GameEngine.Instance.playInterfaceSound("WorldMap_zoomout");
      }
      else
      {
        this.setZooming(this.m_zoomCap, this.m_screenCentreX, this.m_screenCentreY);
        if (this.m_worldZoomInverted >= 26.899999998509884)
          return;
        GameEngine.Instance.playInterfaceSound("WorldMap_zoomout");
      }
    }

    public void zoomOutMax()
    {
      this.setZooming(this.m_zoomCap, this.m_screenCentreX, this.m_screenCentreY);
    }

    public void startMultiStageZoom(double targetZoom, double xPos, double yPos)
    {
      if (targetZoom > 27.0)
        targetZoom = 27.0;
      this.m_stagedTargetZoom = targetZoom;
      this.m_stagedTargetX = xPos;
      this.m_stagedTargetY = yPos;
      double worldZoom = this.WorldZoom;
      this.m_zoomStage = worldZoom <= 9.51 ? (worldZoom <= 3.51 ? (worldZoom <= this.m_zoomCap + 0.5 ? 3 : 2) : 1) : 0;
      this.m_multiStageSoundMode = 0;
      this.nextStageZoom(true);
    }

    public void nextStageZoom(bool initialStage)
    {
      UniversalDebugLog.Log("stage: " + (object) this.m_zoomStage);
      if (this.m_zoomStage < 0)
        return;
      int num1 = this.m_zoomStage;
      switch (num1)
      {
        case 0:
          if (this.m_stagedTargetZoom == 27.0)
          {
            double num2 = (this.m_screenCentreX - this.m_stagedTargetX) * (this.m_screenCentreX - this.m_stagedTargetX) + (this.m_screenCentreY - this.m_stagedTargetY) * (this.m_screenCentreY - this.m_stagedTargetY);
            if (num2 < 10000.0)
              num1 = 5;
            else if (num2 < 30625.0)
              num1 = 4;
          }
          switch (num1)
          {
            case 0:
              this.setZooming(9.5, this.m_screenCentreX, this.m_screenCentreY, initialStage);
              break;
            case 4:
              this.setZooming(9.5, (this.m_screenCentreX - this.m_stagedTargetX) / 2.0 + this.m_stagedTargetX, (this.m_screenCentreY - this.m_stagedTargetY) / 2.0 + this.m_stagedTargetY, initialStage);
              break;
            case 5:
              this.m_zoomStage = num1 + 1;
              this.setZooming(27.0, this.m_stagedTargetX, this.m_stagedTargetY, initialStage);
              break;
          }
          break;
        case 1:
          if (this.m_stagedTargetZoom >= 9.5 && (this.m_screenCentreX - this.m_stagedTargetX) * (this.m_screenCentreX - this.m_stagedTargetX) + (this.m_screenCentreY - this.m_stagedTargetY) * (this.m_screenCentreY - this.m_stagedTargetY) < 360000.0)
            num1 = 3;
          if (num1 == 1)
          {
            this.setZooming(3.5, this.m_screenCentreX, this.m_screenCentreY, initialStage);
            break;
          }
          this.setZooming(3.5, (this.m_screenCentreX - this.m_stagedTargetX) / 2.0 + this.m_stagedTargetX, (this.m_screenCentreY - this.m_stagedTargetY) / 2.0 + this.m_stagedTargetY, initialStage);
          break;
        case 2:
          this.setZooming(this.m_zoomCap, (this.m_screenCentreX - this.m_stagedTargetX) / 2.0 + this.m_stagedTargetX, (this.m_screenCentreY - this.m_stagedTargetY) / 2.0 + this.m_stagedTargetY, initialStage);
          break;
        case 3:
          this.setZooming(3.5, this.m_stagedTargetX, this.m_stagedTargetY, initialStage);
          break;
        case 4:
          this.setZooming(9.5, this.m_stagedTargetX, this.m_stagedTargetY, initialStage);
          break;
        case 5:
          this.m_zoomStage = num1 + 1;
          this.setZooming(27.0, this.m_stagedTargetX, this.m_stagedTargetY, initialStage);
          break;
      }
      this.m_zoomStage = num1 + 1;
      if (num1 < 3)
        return;
      if (this.m_stagedTargetZoom <= this.m_targetZoom)
        this.m_zoomStage = -1;
      this.centreMap(true);
    }

    public void setZooming(double targetZoom, double xPos, double yPos)
    {
      UniversalDebugLog.Log("setZooming 3");
      this.setZooming(targetZoom, xPos, yPos, 16.0);
    }

    public void setZooming(double targetZoom, double xPos, double yPos, bool initialStage)
    {
      UniversalDebugLog.Log("setZooming 1");
      if (initialStage)
      {
        double num = (targetZoom - this.WorldZoom) / 16.0;
        if (num == 0.0)
        {
          if (xPos != this.m_screenCentreX || yPos != this.m_screenCentreY)
            GameEngine.Instance.playInterfaceSound("WorldMap_map_moving_sideways");
        }
        else if (num < 0.0)
        {
          this.m_multiStageSoundMode = 1;
          GameEngine.Instance.playInterfaceSound("WorldMap_map_zooming_out");
        }
        else
          GameEngine.Instance.playInterfaceSound("WorldMap_map_zooming_in");
      }
      else if (this.m_multiStageSoundMode == 1 && (targetZoom - this.WorldZoom) / 16.0 > 0.0)
      {
        GameEngine.Instance.playInterfaceSound("WorldMap_map_zooming_in");
        this.m_multiStageSoundMode = 2;
      }
      this.setZooming(targetZoom, xPos, yPos, 16.0);
    }

    public void setZoomingPaced(double targetZoom, double xPos, double yPos)
    {
      if (targetZoom > 27.0)
        targetZoom = 27.0;
      double zoomTime = 16.0;
      double num1 = Math.Abs(xPos - this.m_screenCentreX);
      double num2 = Math.Abs(yPos - this.m_screenCentreY);
      if (num2 > num1)
        num1 = num2;
      if (num1 > 300.0)
        zoomTime *= num1 / 300.0;
      this.setZooming(targetZoom, xPos, yPos, zoomTime);
    }

    public void setZooming(double targetZoom, double xPos, double yPos, double zoomTime)
    {
      UniversalDebugLog.Log("setZooming 2");
      if (GameEngine.Instance.World.playbackActive())
        return;
      bool flag = false;
      if (this.m_zoomStage >= 0 && this.m_zoomStage < 6)
        flag = true;
      this.m_zoomStage = -1;
      this.m_zooming = true;
      this.m_targetZoom = targetZoom;
      this.m_zoomDiff = (this.m_targetZoom - this.WorldZoom) / zoomTime;
      if (targetZoom == 27.0 && this.m_worldZoomInverted < 0.001)
        this.m_zoomDiff = 0.0;
      if (!flag)
      {
        this.m_zoomXPosTarget = xPos;
        this.m_zoomYPosTarget = yPos;
        double screenCentreX = this.m_screenCentreX;
        double screenCentreY = this.m_screenCentreY;
        this.m_screenCentreX = xPos;
        this.m_screenCentreY = yPos;
        this.centreMap(true);
        xPos = this.m_screenCentreX;
        yPos = this.m_screenCentreY;
        this.m_screenCentreX = screenCentreX;
        this.m_screenCentreY = screenCentreY;
      }
      if (this.m_zoomDiff != 0.0 && Math.Abs(this.m_zoomDiff) < 0.07)
        this.m_zoomDiff = this.m_zoomDiff >= 0.0 ? 0.07 : -0.07;
      this.m_zoomXPosTarget = xPos;
      this.m_zoomYPosTarget = yPos;
      this.m_zoomXPosDiff = (xPos - this.m_screenCentreX) / zoomTime;
      this.m_zoomYPosDiff = (yPos - this.m_screenCentreY) / zoomTime;
    }

    private int findNearestVillageFromScreenPos(Point mousePos, ref double bestDist)
    {
      double mapX = ((double) mousePos.X - (double) this.m_screenWidth / 2.0) / this.m_worldScale + this.m_screenCentreX;
      double mapY = ((double) mousePos.Y - (double) this.m_screenHeight / 2.0) / this.m_worldScale + this.m_screenCentreY;
      return mapX >= 0.0 && mapX < (double) this.worldMapWidth && mapY >= 0.0 && mapY < (double) this.worldMapHeight ? this.findNearestVillageFromMapPos(mapX, mapY, ref bestDist) : -1;
    }

    private int findNearestVillageFromScreenPosAnyVis(Point mousePos, ref double bestDist)
    {
      double mapX = ((double) mousePos.X - (double) this.m_screenWidth / 2.0) / this.m_worldScale + this.m_screenCentreX;
      double mapY = ((double) mousePos.Y - (double) this.m_screenHeight / 2.0) / this.m_worldScale + this.m_screenCentreY;
      return mapX >= 0.0 && mapX < (double) this.worldMapWidth && mapY >= 0.0 && mapY < (double) this.worldMapHeight ? this.findNearestVillageFromMapPosAnyVis(mapX, mapY, ref bestDist) : -1;
    }

    private void getMapCoords(Point mousePos, ref double mapPosX, ref double mapPosY)
    {
      if (WorldMap.KILL_SCROLLING)
        return;
      mapPosX = ((double) mousePos.X - (double) this.m_screenWidth / 2.0) / this.m_worldScale + this.m_screenCentreX;
      mapPosY = ((double) mousePos.Y - (double) this.m_screenHeight / 2.0) / this.m_worldScale + this.m_screenCentreY;
    }

    private Point getScreenPosFromMapCoords(double mapX, double mapY)
    {
      return new Point()
      {
        X = (int) ((mapX - this.m_screenCentreX) * this.m_worldScale + (double) this.m_screenWidth / 2.0),
        Y = (int) ((mapY - this.m_screenCentreY) * this.m_worldScale + (double) this.m_screenHeight / 2.0)
      };
    }

    public int findNearestVillageFromMapPos(double mapX, double mapY, ref double bestDist)
    {
      int villageFromMapPos = -1;
      double num1 = 64.0;
      if (this.PickingStartCounty)
        num1 = 16384.0;
      foreach (VillageData village in this.villageList)
      {
        if (village.visible && (!this.PickingStartCounty || village.countyCapital))
        {
          double num2 = ((double) village.x - mapX) * ((double) village.x - mapX) + ((double) village.y - mapY) * ((double) village.y - mapY);
          if (num2 < num1)
          {
            num1 = num2;
            villageFromMapPos = village.id;
          }
        }
      }
      bestDist = num1;
      return villageFromMapPos;
    }

    public int findNearestVillageFromMapPosAnyVis(double mapX, double mapY, ref double bestDist)
    {
      int fromMapPosAnyVis = -1;
      double num1 = 64.0;
      foreach (VillageData village in this.villageList)
      {
        double num2 = ((double) village.x - mapX) * ((double) village.x - mapX) + ((double) village.y - mapY) * ((double) village.y - mapY);
        if (num2 < num1)
        {
          num1 = num2;
          fromMapPosAnyVis = village.id;
        }
      }
      bestDist = num1;
      return fromMapPosAnyVis;
    }

    public bool Zooming
    {
      get => this.m_zooming;
      set => this.m_zooming = value;
    }

    public void stopZoom()
    {
      this.m_zooming = false;
      this.m_zoomStage = -1;
    }

    private void updateZooming()
    {
      this.m_zoomChangeThisFrame = 0.0;
      if (!this.m_zooming)
        return;
      double screenCentreX = this.m_screenCentreX;
      double screenCentreY = this.m_screenCentreY;
      double num = this.WorldZoom + this.m_zoomDiff;
      this.m_zoomChangeThisFrame = this.m_zoomDiff;
      this.moveMap(this.m_zoomXPosDiff, this.m_zoomYPosDiff);
      if (this.m_zoomXPosDiff < 0.0)
      {
        if (this.m_screenCentreX < this.m_zoomXPosTarget)
        {
          this.m_screenCentreX = this.m_zoomXPosTarget;
          this.m_zoomXPosDiff = 0.0;
        }
      }
      else if (this.m_screenCentreX > this.m_zoomXPosTarget)
      {
        this.m_screenCentreX = this.m_zoomXPosTarget;
        this.m_zoomXPosDiff = 0.0;
      }
      if (this.m_zoomYPosDiff < 0.0)
      {
        if (this.m_screenCentreY < this.m_zoomYPosTarget)
        {
          this.m_screenCentreY = this.m_zoomYPosTarget;
          this.m_zoomYPosDiff = 0.0;
        }
      }
      else if (this.m_screenCentreY > this.m_zoomYPosTarget)
      {
        this.m_screenCentreY = this.m_zoomYPosTarget;
        this.m_zoomYPosDiff = 0.0;
      }
      if (Math.Abs(this.m_zoomXPosTarget - this.m_screenCentreX) < 0.1)
        this.m_zoomXPosDiff = 0.0;
      if (Math.Abs(this.m_zoomYPosTarget - this.m_screenCentreY) < 0.1)
        this.m_zoomYPosDiff = 0.0;
      if (this.m_zoomDiff > 0.0)
      {
        if (num >= this.m_targetZoom)
        {
          this.m_zoomChangeThisFrame = this.m_targetZoom - this.WorldZoom;
          this.m_zoomDiff = 0.0;
          this.WorldZoom = this.m_targetZoom;
        }
      }
      else if (this.m_zoomDiff < 0.0 && num <= this.m_targetZoom)
      {
        this.m_zoomChangeThisFrame = this.m_targetZoom - this.WorldZoom;
        this.m_zoomDiff = 0.0;
        this.WorldZoom = this.m_targetZoom;
      }
      if (this.m_zoomStage < 0 && this.m_zoomDiff <= 0.0)
        this.centreMap(false);
      if (this.m_zoomDiff == 0.0 && screenCentreX == this.m_screenCentreX && screenCentreY == this.m_screenCentreY)
      {
        this.m_zoomXPosDiff = 0.0;
        this.m_zoomXPosDiff = 0.0;
      }
      if (this.m_zoomDiff != 0.0 || this.m_zoomXPosDiff != 0.0 || this.m_zoomYPosDiff != 0.0)
        return;
      this.m_zooming = false;
      this.nextStageZoom(false);
    }

    public void registerWorldZoomCallback(WorldMap.WorldZoomCallback newWorldZoomCallback)
    {
      this.worldZoomCallback = newWorldZoomCallback;
    }

    public void capZoomIFace(float cap)
    {
    }

    public void setCurrentZoom(float zoom) => this.zoomCurrent = (int) ((double) zoom * 1000.0);

    public void changeZoom(float change, Point mousePos)
    {
      if (GameEngine.Instance.World.playbackActive())
        return;
      double mapPosX1 = 0.0;
      double mapPosY1 = 0.0;
      this.getMapCoords(mousePos, ref mapPosX1, ref mapPosY1);
      int num1 = (int) ((double) change * 1000.0);
      if (num1 < this.zoomMin)
        num1 = this.zoomMin;
      if (num1 > this.zoomMax)
        num1 = this.zoomMax;
      this.zoomCurrent = num1;
      this.worldZoomCallback((double) this.zoomCurrent / 1000.0, false);
      if (num1 < this.zoomMax)
      {
        double mapPosX2 = 0.0;
        double mapPosY2 = 0.0;
        this.getMapCoords(mousePos, ref mapPosX2, ref mapPosY2);
        mapPosX1 += this.m_screenCentreX - mapPosX2;
        double num2 = mapPosY1 + (this.m_screenCentreY - mapPosY2);
        this.m_screenCentreX = mapPosX1;
        this.m_screenCentreY = num2;
      }
      if (this.m_zoomDiff > 0.0)
        return;
      this.centreMap(false);
    }

    public void setMouseWheelZoomOut(float change)
    {
      if ((double) this.zoomCurrent > 0.10000000149011612)
        GameEngine.Instance.playInterfaceSound("WorldMap_mousewheel_zoomout");
      int num = (int) ((double) change * 1000.0);
      if (num < this.zoomMin)
        num = this.zoomMin;
      if (num > this.zoomMax)
        num = this.zoomMax;
      this.zoomCurrent = num;
      this.worldZoomCallback((double) this.zoomCurrent / 1000.0, false);
      this.centreMap(false);
    }

    public void changeZoom(float change)
    {
      int num = this.zoomCurrent + (int) ((double) change * 1000.0);
      if (num < this.zoomMin)
        num = this.zoomMin;
      if (num > this.zoomMax)
        num = this.zoomMax;
      this.zoomCurrent = num;
      this.worldZoomCallback((double) this.zoomCurrent / 1000.0, false);
    }

    public void startGameZoom(int villageID)
    {
      double targetZoom = 0.0;
      if (villageID >= 0)
      {
        VillageData village = this.villageList[villageID];
        this.setZooming(27.0, (double) village.x, (double) village.y);
        while (this.Zooming)
        {
          this.updateZooming();
          if (this.ZoomChange != 0.0)
          {
            this.changeZoom((float) this.ZoomChange);
            this.centreMap(false);
          }
        }
      }
      else
      {
        this.setZooming(targetZoom, 0.0, 0.0);
        while (this.Zooming)
        {
          this.updateZooming();
          if (this.ZoomChange != 0.0)
          {
            this.changeZoom((float) this.ZoomChange);
            this.centreMap(false);
          }
        }
      }
    }

    public void Update() => this.updateZooming();

    public Color getColorFromFaction(int factionID)
    {
      if (factionID > 20)
        factionID = 0;
      return WorldMap.areaColorList[factionID];
    }

    public void initSprites(GraphicsMgr newGFX)
    {
      this.gfx = newGFX;
      this.villageSprite = new SpriteWrapper();
      this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapTilesTexID;
      this.villageSprite.SpriteNo = 0;
      this.villageSprite.Initialize(this.gfx);
      this.villageSprite.AutoCentre = true;
      this.villageSprite = new SpriteWrapper();
      this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapTilesTexID;
      this.villageSprite.SpriteNo = 0;
      this.villageSprite.Initialize(this.gfx);
      this.villageSprite.AutoCentre = true;
      this.worldTileSprite.TextureID = GFXLibrary.Instance.WorldMapTilesTexID;
      this.worldTileSprite.SpriteNo = 0;
      this.worldTileSprite.PolySprite = true;
      this.worldTileSprite.Initialize(this.gfx);
      this.worldTreeSprite.TextureID = GFXLibrary.Instance.MapElementsTexID;
      this.worldTreeSprite.SpriteNo = 0;
      this.worldTreeSprite.PolySprite = true;
      this.worldTreeSprite.Initialize(this.gfx);
      this.overlaySprite.TextureID = GFXLibrary.Instance.EffectLayerTexID;
      this.overlaySprite.SpriteNo = 0;
      this.overlaySprite.PolySprite = true;
      this.overlaySprite.Initialize(this.gfx);
      this.updateClockSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
      this.updateClockSprite.SpriteNo = 30;
      this.updateClockSprite.PolySprite = true;
      this.updateClockSprite.Initialize(this.gfx);
      this.tutorialOverlaySprite.TextureID = GFXLibrary.Instance.TutorialIconNormalID;
      this.tutorialOverlaySprite.SpriteNo = 0;
      this.tutorialOverlaySprite.PolySprite = true;
      this.tutorialOverlaySprite.Initialize(this.gfx);
      this.freeCardsSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
      this.freeCardsSprite.SpriteNo = 0;
      this.freeCardsSprite.PolySprite = false;
      this.freeCardsSprite.Initialize(this.gfx);
      this.freeCardsSprite2.TextureID = GFXLibrary.Instance.FreeCardIconsID;
      this.freeCardsSprite2.SpriteNo = 0;
      this.freeCardsSprite2.PolySprite = false;
      this.freeCardsSprite2.Initialize(this.gfx);
      this.wolfsRevengeSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
      this.wolfsRevengeSprite.SpriteNo = 0;
      this.wolfsRevengeSprite.PolySprite = false;
      this.wolfsRevengeSprite.Initialize(this.gfx);
      this.wolfsRevengeSprite2.TextureID = GFXLibrary.Instance.FreeCardIconsID;
      this.wolfsRevengeSprite2.SpriteNo = 0;
      this.wolfsRevengeSprite2.PolySprite = false;
      this.wolfsRevengeSprite2.Initialize(this.gfx);
      this.saleSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
      this.saleSprite.SpriteNo = 0;
      this.saleSprite.PolySprite = false;
      this.saleSprite.Initialize(this.gfx);
      bool flag = GameEngine.Instance.World.salePercentage > 99;
      for (int index = 0; index < (flag ? 5 : 4); ++index)
      {
        SpriteWrapper spriteWrapper = new SpriteWrapper();
        spriteWrapper.TextureID = GFXLibrary.Instance.FreeCardIconsID;
        spriteWrapper.SpriteNo = 0;
        spriteWrapper.PolySprite = false;
        spriteWrapper.Initialize(this.gfx);
        this.saleDigits.Add(spriteWrapper);
      }
      for (int index = 0; index < 5; ++index)
      {
        SpriteWrapper spriteWrapper = new SpriteWrapper();
        spriteWrapper.TextureID = GFXLibrary.Instance.FreeCardIconsID;
        spriteWrapper.SpriteNo = 0;
        spriteWrapper.PolySprite = false;
        spriteWrapper.Initialize(this.gfx);
        this.saleTimer.Add(spriteWrapper);
      }
      this.offerSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
      this.offerSprite.SpriteNo = 0;
      this.offerSprite.PolySprite = false;
      this.offerSprite.Initialize(this.gfx);
      for (int index = 0; index < 8; ++index)
      {
        SpriteWrapper spriteWrapper = new SpriteWrapper();
        spriteWrapper.TextureID = GFXLibrary.Instance.FreeCardIconsID;
        spriteWrapper.SpriteNo = 0;
        spriteWrapper.PolySprite = false;
        spriteWrapper.Initialize(this.gfx);
        this.offerTimer.Add(spriteWrapper);
      }
      this.seaSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
      this.seaSprite.SpriteNo = 0;
      this.seaSprite.PolySprite = false;
      this.seaSprite.Initialize(this.gfx);
      this.royalTowerSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
      this.royalTowerSprite.SpriteNo = 0;
      this.royalTowerSprite.PolySprite = false;
      this.royalTowerSprite.Initialize(this.gfx);
      this.royalTowerSprite1.TextureID = GFXLibrary.Instance.FreeCardIconsID;
      this.royalTowerSprite1.SpriteNo = 0;
      this.royalTowerSprite1.PolySprite = false;
      this.royalTowerSprite1.Initialize(this.gfx);
      this.royalTowerSprite2.TextureID = GFXLibrary.Instance.FreeCardIconsID;
      this.royalTowerSprite2.SpriteNo = 0;
      this.royalTowerSprite2.PolySprite = false;
      this.royalTowerSprite2.Initialize(this.gfx);
      this.royalTowerSprite3.TextureID = GFXLibrary.Instance.FreeCardIconsID;
      this.royalTowerSprite3.SpriteNo = 0;
      this.royalTowerSprite3.PolySprite = false;
      this.royalTowerSprite3.Initialize(this.gfx);
      this.contestSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
      this.contestSprite.SpriteNo = 0;
      this.contestSprite.PolySprite = false;
      this.contestSprite.Initialize(this.gfx);
      this.ticketsSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
      this.ticketsSprite.SpriteNo = 0;
      this.ticketsSprite.PolySprite = false;
      this.ticketsSprite.Initialize(this.gfx);
      this.ticketsSprite2.TextureID = GFXLibrary.Instance.FreeCardIconsID;
      this.ticketsSprite2.SpriteNo = 0;
      this.ticketsSprite2.PolySprite = false;
      this.ticketsSprite2.Initialize(this.gfx);
    }

    public void updateSeasonalGFX()
    {
      this.overlaySprite.TextureID = GFXLibrary.Instance.EffectLayerTexID;
      this.worldTreeSprite.TextureID = GFXLibrary.Instance.MapElementsTexID;
      if (this.villageSprite != null)
        this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapTilesTexID;
      this.worldTileSprite.TextureID = GFXLibrary.Instance.WorldMapTilesTexID;
    }

    public void initMapTiles(string fileName, int width, int height)
    {
      this.TILEMAP_WIDTH = width;
      this.TILEMAP_HEIGHT = height;
      this.mapTileGrid = new short[this.TILEMAP_WIDTH, this.TILEMAP_HEIGHT];
      this.tree1Grid = new byte[this.TILEMAP_WIDTH, this.TILEMAP_HEIGHT];
      this.tree2Grid = new byte[this.TILEMAP_WIDTH, this.TILEMAP_HEIGHT];
      try
      {
        FileStream input = new FileStream(Application.StartupPath + "\\assets\\" + fileName, FileMode.Open, FileAccess.Read);
        BinaryReader binaryReader = new BinaryReader((Stream) input);
        binaryReader.ReadInt32();
        int length = binaryReader.ReadInt32();
        byte[] _data = new byte[length];
        for (int index = 0; index < length; ++index)
          _data[index] = binaryReader.ReadByte();
        binaryReader.Close();
        input.Close();
        byte[] numArray = RLECompress.DecodeData(_data);
        int num = 0;
        for (int index1 = 0; index1 < this.TILEMAP_HEIGHT; ++index1)
        {
          for (int index2 = 0; index2 < this.TILEMAP_WIDTH; ++index2)
            this.mapTileGrid[index2, index1] = (short) numArray[num++];
        }
        for (int index3 = 0; index3 < this.TILEMAP_HEIGHT; ++index3)
        {
          for (int index4 = 0; index4 < this.TILEMAP_WIDTH; ++index4)
            this.mapTileGrid[index4, index3] |= (short) ((int) numArray[num++] << 8);
        }
        for (int index5 = 0; index5 < this.TILEMAP_HEIGHT; ++index5)
        {
          for (int index6 = 0; index6 < this.TILEMAP_WIDTH; ++index6)
            this.tree1Grid[index6, index5] = numArray[num++];
        }
        for (int index7 = 0; index7 < this.TILEMAP_HEIGHT; ++index7)
        {
          for (int index8 = 0; index8 < this.TILEMAP_WIDTH; ++index8)
            this.tree2Grid[index8, index7] = numArray[num++];
        }
        this.haveInitMapTiles = true;
      }
      catch (Exception ex)
      {
      }
    }

    public void resetLeaderboards()
    {
      this.leaderboardSearchResults = new List<LeaderBoardSearchResults>();
      this.leaderboard_Main = new SparseArray();
      this.leaderboard_MainRank = new SparseArray();
      this.leaderboard_MainVillages = new SparseArray();
      this.leaderboard_Factions = new SparseArray();
      this.leaderboard_Houses = new SparseArray();
      this.leaderboard_ParishFlags = new SparseArray();
      this.leaderboard_Sub_Pillager = new SparseArray();
      this.leaderboard_Sub_Defender = new SparseArray();
      this.leaderboard_Sub_Ransack = new SparseArray();
      this.leaderboard_Sub_Wolfsbane = new SparseArray();
      this.leaderboard_Sub_Banditkiller = new SparseArray();
      this.leaderboard_Sub_AIKiller = new SparseArray();
      this.leaderboard_Sub_Trader = new SparseArray();
      this.leaderboard_Sub_Forager = new SparseArray();
      this.leaderboard_Sub_Stockpiler = new SparseArray();
      this.leaderboard_Sub_Farmer = new SparseArray();
      this.leaderboard_Sub_Brewer = new SparseArray();
      this.leaderboard_Sub_Weaponsmith = new SparseArray();
      this.leaderboard_Sub_banquetter = new SparseArray();
      this.leaderboard_Sub_Achiever = new SparseArray();
      this.leaderboard_Sub_Donater = new SparseArray();
      this.leaderboard_Sub_Capture = new SparseArray();
      this.leaderboard_Sub_Raze = new SparseArray();
      this.leaderboard_Sub_Glory = new SparseArray();
      this.leaderboard_Sub_KillStreak = new SparseArray();
      this.max_leaderboard_Main = -1;
      this.max_leaderboard_MainRank = -1;
      this.max_leaderboard_MainVillages = -1;
      this.max_leaderboard_Factions = -1;
      this.max_leaderboard_Houses = -1;
      this.max_leaderboard_ParishFlags = -1;
      this.max_leaderboard_Sub_Pillager = -1;
      this.max_leaderboard_Sub_Defender = -1;
      this.max_leaderboard_Sub_Ransack = -1;
      this.max_leaderboard_Sub_Wolfsbane = -1;
      this.max_leaderboard_Sub_Banditkiller = -1;
      this.max_leaderboard_Sub_AIKiller = -1;
      this.max_leaderboard_Sub_Trader = -1;
      this.max_leaderboard_Sub_Forager = -1;
      this.max_leaderboard_Sub_Stockpiler = -1;
      this.max_leaderboard_Sub_Farmer = -1;
      this.max_leaderboard_Sub_Brewer = -1;
      this.max_leaderboard_Sub_Weaponsmith = -1;
      this.max_leaderboard_Sub_banquetter = -1;
      this.max_leaderboard_Sub_Achiever = -1;
      this.max_leaderboard_Sub_Donater = -1;
      this.max_leaderboard_Sub_Capture = -1;
      this.max_leaderboard_Sub_Raze = -1;
      this.max_leaderboard_Sub_Glory = -1;
      this.max_leaderboard_Sub_KillStreak = -1;
      this.lastZeroDownload_leaderboard_Main = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_MainRank = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_MainVillages = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Factions = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Houses = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_ParishFlags = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Pillager = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Defender = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Ransack = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Wolfsbane = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Banditkiller = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_AIKiller = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Trader = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Forager = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Stockpiler = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Farmer = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Brewer = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Weaponsmith = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_banquetter = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Achiever = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Donater = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Capture = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Raze = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_Glory = DateTime.MinValue;
      this.lastZeroDownload_leaderboard_Sub_KillStreak = DateTime.MinValue;
      this.inDownloading = false;
    }

    public void DownloadSectionOfLeaderboard(int mode, int position, int pageSize)
    {
      this.getLeaderboardEntry(mode, position, pageSize);
    }

    public LeaderBoardEntryData getLeaderboardEntry(int mode, int position, int pageSize)
    {
      DateTime dateTime = DateTime.MinValue;
      int num = -1;
      SparseArray currentArray = (SparseArray) null;
      switch (mode)
      {
        case -6:
          currentArray = this.leaderboard_MainVillages;
          num = this.max_leaderboard_MainVillages;
          dateTime = this.lastZeroDownload_leaderboard_MainVillages;
          break;
        case -5:
          currentArray = this.leaderboard_MainRank;
          num = this.max_leaderboard_MainRank;
          dateTime = this.lastZeroDownload_leaderboard_MainRank;
          break;
        case -4:
          currentArray = this.leaderboard_ParishFlags;
          num = this.max_leaderboard_ParishFlags;
          dateTime = this.lastZeroDownload_leaderboard_ParishFlags;
          break;
        case -3:
          currentArray = this.leaderboard_Houses;
          num = this.max_leaderboard_Houses;
          dateTime = this.lastZeroDownload_leaderboard_Houses;
          break;
        case -2:
          currentArray = this.leaderboard_Factions;
          num = this.max_leaderboard_Factions;
          dateTime = this.lastZeroDownload_leaderboard_Factions;
          break;
        case -1:
          currentArray = this.leaderboard_Main;
          num = this.max_leaderboard_Main;
          dateTime = this.lastZeroDownload_leaderboard_Main;
          break;
        case 0:
          currentArray = this.leaderboard_Sub_Pillager;
          num = this.max_leaderboard_Sub_Pillager;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Pillager;
          break;
        case 1:
          currentArray = this.leaderboard_Sub_Defender;
          num = this.max_leaderboard_Sub_Defender;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Defender;
          break;
        case 2:
          currentArray = this.leaderboard_Sub_Ransack;
          num = this.max_leaderboard_Sub_Ransack;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Ransack;
          break;
        case 3:
          currentArray = this.leaderboard_Sub_Wolfsbane;
          num = this.max_leaderboard_Sub_Wolfsbane;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Wolfsbane;
          break;
        case 4:
          currentArray = this.leaderboard_Sub_Banditkiller;
          num = this.max_leaderboard_Sub_Banditkiller;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Banditkiller;
          break;
        case 5:
          currentArray = this.leaderboard_Sub_AIKiller;
          num = this.max_leaderboard_Sub_AIKiller;
          dateTime = this.lastZeroDownload_leaderboard_Sub_AIKiller;
          break;
        case 6:
          currentArray = this.leaderboard_Sub_Trader;
          num = this.max_leaderboard_Sub_Trader;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Trader;
          break;
        case 7:
          currentArray = this.leaderboard_Sub_Forager;
          num = this.max_leaderboard_Sub_Forager;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Forager;
          break;
        case 8:
          currentArray = this.leaderboard_Sub_Stockpiler;
          num = this.max_leaderboard_Sub_Stockpiler;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Stockpiler;
          break;
        case 9:
          currentArray = this.leaderboard_Sub_Farmer;
          num = this.max_leaderboard_Sub_Farmer;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Farmer;
          break;
        case 10:
          currentArray = this.leaderboard_Sub_Brewer;
          num = this.max_leaderboard_Sub_Brewer;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Brewer;
          break;
        case 11:
          currentArray = this.leaderboard_Sub_Weaponsmith;
          num = this.max_leaderboard_Sub_Weaponsmith;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Weaponsmith;
          break;
        case 12:
          currentArray = this.leaderboard_Sub_banquetter;
          num = this.max_leaderboard_Sub_banquetter;
          dateTime = this.lastZeroDownload_leaderboard_Sub_banquetter;
          break;
        case 13:
          currentArray = this.leaderboard_Sub_Achiever;
          num = this.max_leaderboard_Sub_Achiever;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Achiever;
          break;
        case 14:
          currentArray = this.leaderboard_Sub_Donater;
          num = this.max_leaderboard_Sub_Donater;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Donater;
          break;
        case 15:
          currentArray = this.leaderboard_Sub_Capture;
          num = this.max_leaderboard_Sub_Capture;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Capture;
          break;
        case 16:
          currentArray = this.leaderboard_Sub_Raze;
          num = this.max_leaderboard_Sub_Raze;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Raze;
          break;
        case 17:
          currentArray = this.leaderboard_Sub_Glory;
          num = this.max_leaderboard_Sub_Glory;
          dateTime = this.lastZeroDownload_leaderboard_Sub_Glory;
          break;
        case 18:
          currentArray = this.leaderboard_Sub_KillStreak;
          num = this.max_leaderboard_Sub_KillStreak;
          dateTime = this.lastZeroDownload_leaderboard_Sub_KillStreak;
          break;
      }
      if (currentArray == null)
        return (LeaderBoardEntryData) null;
      if (num <= 0 && (DateTime.Now - dateTime).TotalMinutes < 1.0)
        return (LeaderBoardEntryData) null;
      if (currentArray.Count == 0)
      {
        this.downloadLeaderboard(currentArray, mode, -1, pageSize);
        return (LeaderBoardEntryData) null;
      }
      if (position < 0)
        return (LeaderBoardEntryData) null;
      if (currentArray[position] != null)
        return (LeaderBoardEntryData) currentArray[position];
      if (position <= num)
      {
        this.downloadLeaderboard(currentArray, mode, position, pageSize);
        return (LeaderBoardEntryData) null;
      }
      if (num < 0)
        return (LeaderBoardEntryData) null;
      if (WorldMap.dummyEntry == null)
      {
        WorldMap.dummyEntry = new LeaderBoardEntryData();
        WorldMap.dummyEntry.dummy = true;
      }
      return WorldMap.dummyEntry;
    }

    public void leaderboardLookHigher(int mode, int position, int pageSize)
    {
      SparseArray leaderboardArray = this.getLeaderboardArray(mode);
      int minValue = position;
      int maxValue = position;
      bool flag = false;
      for (int index1 = 1; index1 < 50 + pageSize; ++index1)
      {
        int index2 = position - index1;
        if (index2 < 1)
          index2 = 1;
        if (leaderboardArray[index2] != null)
        {
          if (index1 >= pageSize + 5 && !flag)
            return;
        }
        else if (!flag)
        {
          minValue = maxValue = index2;
          flag = true;
        }
        else
          minValue = index2;
      }
      if (minValue == position)
        return;
      RemoteServices.Instance.LeaderBoard(mode, minValue, maxValue, this.leaderboardLastUpdateTime);
    }

    public void leaderboardLookLower(int mode, int position, int pageSize)
    {
      position += pageSize;
      SparseArray leaderboardArray = this.getLeaderboardArray(mode);
      int leaderboardEntries = this.getMaxLeaderboardEntries(mode);
      int minValue = position;
      int maxValue = position;
      bool flag = false;
      for (int index1 = 1; index1 < 50 + pageSize; ++index1)
      {
        int index2 = position + index1;
        if (index2 >= leaderboardEntries)
          index2 = leaderboardEntries;
        if (leaderboardArray[index2] != null)
        {
          if (index1 >= pageSize + 5 && !flag)
            return;
        }
        else if (!flag)
        {
          minValue = maxValue = index2;
          flag = true;
        }
        else
          maxValue = index2;
      }
      if (minValue == position)
        return;
      RemoteServices.Instance.LeaderBoard(mode, minValue, maxValue, this.leaderboardLastUpdateTime);
    }

    public DateTime getLastLeaderboardUpdate() => this.leaderboardLastUpdateTime;

    public bool downloadingLeaderboard() => this.inDownloading || this.inLeaderboardSearch;

    public void downloadLeaderboard(
      SparseArray currentArray,
      int mode,
      int position,
      int pageSize)
    {
      if (this.inDownloading)
        return;
      this.inDownloading = true;
      RemoteServices.Instance.set_LeaderBoard_UserCallBack(new RemoteServices.LeaderBoard_UserCallBack(this.LeaderboardCallback));
      if (position < 0)
      {
        RemoteServices.Instance.LeaderBoard(mode, -1, -1, this.leaderboardLastUpdateTime);
      }
      else
      {
        int minValue = position;
        int maxValue = position;
        bool flag1 = false;
        bool flag2 = false;
        for (int index1 = 1; index1 < 50 + pageSize; ++index1)
        {
          int index2 = position - index1;
          if (index2 < 1)
            index2 = 1;
          int index3 = position + index1;
          if (!flag1)
          {
            if (currentArray[index2] != null)
              flag1 = true;
            else
              minValue = index2;
          }
          if (!flag2)
          {
            if (currentArray[index3] != null)
            {
              if (index1 > pageSize + 1)
                flag1 = true;
            }
            else
              maxValue = index3;
          }
        }
        RemoteServices.Instance.LeaderBoard(mode, minValue, maxValue, this.leaderboardLastUpdateTime);
      }
    }

    public void LeaderboardCallback(LeaderBoard_ReturnType returnData)
    {
      this.inDownloading = false;
      if (!returnData.Success)
        return;
      if (returnData.lastUpdate != this.leaderboardLastUpdateTime)
      {
        if (this.leaderboardLastUpdateTime != DateTime.MinValue)
          this.resetLeaderboards();
        this.leaderboardLastUpdateTime = returnData.lastUpdate;
      }
      if (returnData.ownStandings != null)
        this.importStandings(returnData.ownStandings);
      int maxValue = returnData.maxValue;
      SparseArray currentArray = (SparseArray) null;
      switch (returnData.leaderboardType)
      {
        case -6:
          currentArray = this.leaderboard_MainVillages;
          this.max_leaderboard_MainVillages = maxValue;
          break;
        case -5:
          currentArray = this.leaderboard_MainRank;
          this.max_leaderboard_MainRank = maxValue;
          break;
        case -4:
          currentArray = this.leaderboard_ParishFlags;
          this.max_leaderboard_ParishFlags = maxValue;
          break;
        case -3:
          currentArray = this.leaderboard_Houses;
          this.max_leaderboard_Houses = maxValue;
          break;
        case -2:
          currentArray = this.leaderboard_Factions;
          this.max_leaderboard_Factions = maxValue;
          break;
        case -1:
          currentArray = this.leaderboard_Main;
          this.max_leaderboard_Main = maxValue;
          break;
        case 0:
          currentArray = this.leaderboard_Sub_Pillager;
          this.max_leaderboard_Sub_Pillager = maxValue;
          break;
        case 1:
          currentArray = this.leaderboard_Sub_Defender;
          this.max_leaderboard_Sub_Defender = maxValue;
          break;
        case 2:
          currentArray = this.leaderboard_Sub_Ransack;
          this.max_leaderboard_Sub_Ransack = maxValue;
          break;
        case 3:
          currentArray = this.leaderboard_Sub_Wolfsbane;
          this.max_leaderboard_Sub_Wolfsbane = maxValue;
          break;
        case 4:
          currentArray = this.leaderboard_Sub_Banditkiller;
          this.max_leaderboard_Sub_Banditkiller = maxValue;
          break;
        case 5:
          currentArray = this.leaderboard_Sub_AIKiller;
          this.max_leaderboard_Sub_AIKiller = maxValue;
          break;
        case 6:
          currentArray = this.leaderboard_Sub_Trader;
          this.max_leaderboard_Sub_Trader = maxValue;
          break;
        case 7:
          currentArray = this.leaderboard_Sub_Forager;
          this.max_leaderboard_Sub_Forager = maxValue;
          break;
        case 8:
          currentArray = this.leaderboard_Sub_Stockpiler;
          this.max_leaderboard_Sub_Stockpiler = maxValue;
          break;
        case 9:
          currentArray = this.leaderboard_Sub_Farmer;
          this.max_leaderboard_Sub_Farmer = maxValue;
          break;
        case 10:
          currentArray = this.leaderboard_Sub_Brewer;
          this.max_leaderboard_Sub_Brewer = maxValue;
          break;
        case 11:
          currentArray = this.leaderboard_Sub_Weaponsmith;
          this.max_leaderboard_Sub_Weaponsmith = maxValue;
          break;
        case 12:
          currentArray = this.leaderboard_Sub_banquetter;
          this.max_leaderboard_Sub_banquetter = maxValue;
          break;
        case 13:
          currentArray = this.leaderboard_Sub_Achiever;
          this.max_leaderboard_Sub_Achiever = maxValue;
          break;
        case 14:
          currentArray = this.leaderboard_Sub_Donater;
          this.max_leaderboard_Sub_Donater = maxValue;
          break;
        case 15:
          currentArray = this.leaderboard_Sub_Capture;
          this.max_leaderboard_Sub_Capture = maxValue;
          break;
        case 16:
          currentArray = this.leaderboard_Sub_Raze;
          this.max_leaderboard_Sub_Raze = maxValue;
          break;
        case 17:
          currentArray = this.leaderboard_Sub_Glory;
          this.max_leaderboard_Sub_Glory = maxValue;
          break;
        case 18:
          currentArray = this.leaderboard_Sub_KillStreak;
          this.max_leaderboard_Sub_KillStreak = maxValue;
          break;
      }
      this.importLeaderboardData(currentArray, returnData.leaderboardType, returnData.mainLeaderboard, returnData.subLeaderboard, returnData.parishLeaderboard, returnData.houseLeaderboard, returnData.factionLeaderboard);
    }

    public bool isLeaderboardCategoryPopulated(int mode)
    {
      SparseArray sparseArray = (SparseArray) null;
      switch (mode)
      {
        case -6:
          sparseArray = this.leaderboard_MainVillages;
          break;
        case -5:
          sparseArray = this.leaderboard_MainRank;
          break;
        case -4:
          sparseArray = this.leaderboard_ParishFlags;
          break;
        case -3:
          sparseArray = this.leaderboard_Houses;
          break;
        case -2:
          sparseArray = this.leaderboard_Factions;
          break;
        case -1:
          sparseArray = this.leaderboard_Main;
          break;
        case 0:
          sparseArray = this.leaderboard_Sub_Pillager;
          break;
        case 1:
          sparseArray = this.leaderboard_Sub_Defender;
          break;
        case 2:
          sparseArray = this.leaderboard_Sub_Ransack;
          break;
        case 3:
          sparseArray = this.leaderboard_Sub_Wolfsbane;
          break;
        case 4:
          sparseArray = this.leaderboard_Sub_Banditkiller;
          break;
        case 5:
          sparseArray = this.leaderboard_Sub_AIKiller;
          break;
        case 6:
          sparseArray = this.leaderboard_Sub_Trader;
          break;
        case 7:
          sparseArray = this.leaderboard_Sub_Forager;
          break;
        case 8:
          sparseArray = this.leaderboard_Sub_Stockpiler;
          break;
        case 9:
          sparseArray = this.leaderboard_Sub_Farmer;
          break;
        case 10:
          sparseArray = this.leaderboard_Sub_Brewer;
          break;
        case 11:
          sparseArray = this.leaderboard_Sub_Weaponsmith;
          break;
        case 12:
          sparseArray = this.leaderboard_Sub_banquetter;
          break;
        case 13:
          sparseArray = this.leaderboard_Sub_Achiever;
          break;
        case 14:
          sparseArray = this.leaderboard_Sub_Donater;
          break;
        case 15:
          sparseArray = this.leaderboard_Sub_Capture;
          break;
        case 16:
          sparseArray = this.leaderboard_Sub_Raze;
          break;
        case 17:
          sparseArray = this.leaderboard_Sub_Glory;
          break;
        case 18:
          sparseArray = this.leaderboard_Sub_KillStreak;
          break;
      }
      return sparseArray != null && sparseArray.Count > 0;
    }

    public int findSelfInLeaderboard(int mode)
    {
      int num = RemoteServices.Instance.UserID;
      SparseArray sparseArray = (SparseArray) null;
      switch (mode)
      {
        case -6:
          sparseArray = this.leaderboard_MainVillages;
          break;
        case -5:
          sparseArray = this.leaderboard_MainRank;
          break;
        case -4:
          sparseArray = this.leaderboard_ParishFlags;
          num = 1;
          int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
          if (this.isCapital(selectedMenuVillage))
          {
            if (this.isRegionCapital(selectedMenuVillage))
            {
              num = this.getParishFromVillageID(selectedMenuVillage);
              break;
            }
            List<int> userVillageIdList = this.getUserVillageIDList();
            if (userVillageIdList.Count > 0)
            {
              using (List<int>.Enumerator enumerator = userVillageIdList.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  if (!this.isCapital(enumerator.Current))
                  {
                    num = this.getParishFromVillageID(selectedMenuVillage);
                    break;
                  }
                }
                break;
              }
            }
            else
              break;
          }
          else
          {
            num = this.getParishFromVillageID(selectedMenuVillage);
            break;
          }
        case -3:
          sparseArray = this.leaderboard_Houses;
          num = GameEngine.Instance.World.getHouse(RemoteServices.Instance.UserFactionID);
          break;
        case -2:
          sparseArray = this.leaderboard_Factions;
          num = RemoteServices.Instance.UserFactionID;
          break;
        case -1:
          sparseArray = this.leaderboard_Main;
          break;
        case 0:
          sparseArray = this.leaderboard_Sub_Pillager;
          break;
        case 1:
          sparseArray = this.leaderboard_Sub_Defender;
          break;
        case 2:
          sparseArray = this.leaderboard_Sub_Ransack;
          break;
        case 3:
          sparseArray = this.leaderboard_Sub_Wolfsbane;
          break;
        case 4:
          sparseArray = this.leaderboard_Sub_Banditkiller;
          break;
        case 5:
          sparseArray = this.leaderboard_Sub_AIKiller;
          break;
        case 6:
          sparseArray = this.leaderboard_Sub_Trader;
          break;
        case 7:
          sparseArray = this.leaderboard_Sub_Forager;
          break;
        case 8:
          sparseArray = this.leaderboard_Sub_Stockpiler;
          break;
        case 9:
          sparseArray = this.leaderboard_Sub_Farmer;
          break;
        case 10:
          sparseArray = this.leaderboard_Sub_Brewer;
          break;
        case 11:
          sparseArray = this.leaderboard_Sub_Weaponsmith;
          break;
        case 12:
          sparseArray = this.leaderboard_Sub_banquetter;
          break;
        case 13:
          sparseArray = this.leaderboard_Sub_Achiever;
          break;
        case 14:
          sparseArray = this.leaderboard_Sub_Donater;
          break;
        case 15:
          sparseArray = this.leaderboard_Sub_Capture;
          break;
        case 16:
          sparseArray = this.leaderboard_Sub_Raze;
          break;
        case 17:
          sparseArray = this.leaderboard_Sub_Glory;
          break;
        case 18:
          sparseArray = this.leaderboard_Sub_KillStreak;
          break;
      }
      foreach (LeaderBoardEntryData leaderBoardEntryData in sparseArray)
      {
        if (leaderBoardEntryData.entryID == num)
          return leaderBoardEntryData.standing;
      }
      return 1;
    }

    public int getMaxLeaderboardEntries(int mode)
    {
      int leaderboardEntries = -1;
      switch (mode)
      {
        case -6:
          leaderboardEntries = this.max_leaderboard_MainVillages;
          break;
        case -5:
          leaderboardEntries = this.max_leaderboard_MainRank;
          break;
        case -4:
          leaderboardEntries = this.max_leaderboard_ParishFlags;
          break;
        case -3:
          leaderboardEntries = this.max_leaderboard_Houses;
          break;
        case -2:
          leaderboardEntries = this.max_leaderboard_Factions;
          break;
        case -1:
          leaderboardEntries = this.max_leaderboard_Main;
          break;
        case 0:
          leaderboardEntries = this.max_leaderboard_Sub_Pillager;
          break;
        case 1:
          leaderboardEntries = this.max_leaderboard_Sub_Defender;
          break;
        case 2:
          leaderboardEntries = this.max_leaderboard_Sub_Ransack;
          break;
        case 3:
          leaderboardEntries = this.max_leaderboard_Sub_Wolfsbane;
          break;
        case 4:
          leaderboardEntries = this.max_leaderboard_Sub_Banditkiller;
          break;
        case 5:
          leaderboardEntries = this.max_leaderboard_Sub_AIKiller;
          break;
        case 6:
          leaderboardEntries = this.max_leaderboard_Sub_Trader;
          break;
        case 7:
          leaderboardEntries = this.max_leaderboard_Sub_Forager;
          break;
        case 8:
          leaderboardEntries = this.max_leaderboard_Sub_Stockpiler;
          break;
        case 9:
          leaderboardEntries = this.max_leaderboard_Sub_Farmer;
          break;
        case 10:
          leaderboardEntries = this.max_leaderboard_Sub_Brewer;
          break;
        case 11:
          leaderboardEntries = this.max_leaderboard_Sub_Weaponsmith;
          break;
        case 12:
          leaderboardEntries = this.max_leaderboard_Sub_banquetter;
          break;
        case 13:
          leaderboardEntries = this.max_leaderboard_Sub_Achiever;
          break;
        case 14:
          leaderboardEntries = this.max_leaderboard_Sub_Donater;
          break;
        case 15:
          leaderboardEntries = this.max_leaderboard_Sub_Capture;
          break;
        case 16:
          leaderboardEntries = this.max_leaderboard_Sub_Raze;
          break;
        case 17:
          leaderboardEntries = this.max_leaderboard_Sub_Glory;
          break;
        case 18:
          leaderboardEntries = this.max_leaderboard_Sub_KillStreak;
          break;
      }
      return leaderboardEntries;
    }

    public SparseArray getLeaderboardArray(int mode)
    {
      SparseArray leaderboardArray = (SparseArray) null;
      switch (mode)
      {
        case -6:
          leaderboardArray = this.leaderboard_MainVillages;
          break;
        case -5:
          leaderboardArray = this.leaderboard_MainRank;
          break;
        case -4:
          leaderboardArray = this.leaderboard_ParishFlags;
          break;
        case -3:
          leaderboardArray = this.leaderboard_Houses;
          break;
        case -2:
          leaderboardArray = this.leaderboard_Factions;
          break;
        case -1:
          leaderboardArray = this.leaderboard_Main;
          break;
        case 0:
          leaderboardArray = this.leaderboard_Sub_Pillager;
          break;
        case 1:
          leaderboardArray = this.leaderboard_Sub_Defender;
          break;
        case 2:
          leaderboardArray = this.leaderboard_Sub_Ransack;
          break;
        case 3:
          leaderboardArray = this.leaderboard_Sub_Wolfsbane;
          break;
        case 4:
          leaderboardArray = this.leaderboard_Sub_Banditkiller;
          break;
        case 5:
          leaderboardArray = this.leaderboard_Sub_AIKiller;
          break;
        case 6:
          leaderboardArray = this.leaderboard_Sub_Trader;
          break;
        case 7:
          leaderboardArray = this.leaderboard_Sub_Forager;
          break;
        case 8:
          leaderboardArray = this.leaderboard_Sub_Stockpiler;
          break;
        case 9:
          leaderboardArray = this.leaderboard_Sub_Farmer;
          break;
        case 10:
          leaderboardArray = this.leaderboard_Sub_Brewer;
          break;
        case 11:
          leaderboardArray = this.leaderboard_Sub_Weaponsmith;
          break;
        case 12:
          leaderboardArray = this.leaderboard_Sub_banquetter;
          break;
        case 13:
          leaderboardArray = this.leaderboard_Sub_Achiever;
          break;
        case 14:
          leaderboardArray = this.leaderboard_Sub_Donater;
          break;
        case 15:
          leaderboardArray = this.leaderboard_Sub_Capture;
          break;
        case 16:
          leaderboardArray = this.leaderboard_Sub_Raze;
          break;
        case 17:
          leaderboardArray = this.leaderboard_Sub_Glory;
          break;
        case 18:
          leaderboardArray = this.leaderboard_Sub_KillStreak;
          break;
      }
      return leaderboardArray;
    }

    public void leaderboardSearch(int category, string searchString)
    {
      searchString = searchString.ToLowerInvariant();
      foreach (LeaderBoardSearchResults leaderboardSearchResult in this.leaderboardSearchResults)
      {
        if (leaderboardSearchResult.category == category)
        {
          if (searchString == leaderboardSearchResult.searchString)
          {
            InterfaceMgr.Instance.leaderboardSearchComplete(leaderboardSearchResult);
            return;
          }
          if (searchString.Contains(leaderboardSearchResult.searchString))
          {
            LeaderBoardSearchResults results = new LeaderBoardSearchResults();
            results.category = category;
            results.searchString = searchString;
            switch (category)
            {
              case -4:
              case -3:
                results.entries.Sort();
                this.leaderboardSearchResults.Add(results);
                InterfaceMgr.Instance.leaderboardSearchComplete(results);
                return;
              default:
                SparseArray leaderboardArray = this.getLeaderboardArray(category);
                using (List<int>.Enumerator enumerator = leaderboardSearchResult.entries.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    int current = enumerator.Current;
                    LeaderBoardEntryData leaderBoardEntryData = (LeaderBoardEntryData) leaderboardArray[current];
                    if (leaderBoardEntryData != null && leaderBoardEntryData.name.ToLower().Contains(searchString))
                      results.entries.Add(current);
                  }
                  goto case -4;
                }
            }
          }
        }
      }
      this.inLeaderboardSearch = true;
      RemoteServices.Instance.set_LeaderBoardSearch_UserCallBack(new RemoteServices.LeaderBoardSearch_UserCallBack(this.leaderboardSearchCallback));
      RemoteServices.Instance.LeaderBoardSearch(category, searchString, this.leaderboardLastUpdateTime);
    }

    private void leaderboardSearchCallback(LeaderBoardSearch_ReturnType returnData)
    {
      if (returnData.Success)
      {
        if (returnData.lastUpdate != this.leaderboardLastUpdateTime)
        {
          if (this.leaderboardLastUpdateTime != DateTime.MinValue)
            this.resetLeaderboards();
          this.leaderboardLastUpdateTime = returnData.lastUpdate;
        }
        if (returnData.ownStandings != null)
          this.importStandings(returnData.ownStandings);
        int maxValue = returnData.maxValue;
        SparseArray currentArray = (SparseArray) null;
        switch (returnData.leaderboardType)
        {
          case -6:
            currentArray = this.leaderboard_MainVillages;
            this.max_leaderboard_MainVillages = maxValue;
            break;
          case -5:
            currentArray = this.leaderboard_MainRank;
            this.max_leaderboard_MainRank = maxValue;
            break;
          case -4:
            currentArray = this.leaderboard_ParishFlags;
            this.max_leaderboard_ParishFlags = maxValue;
            break;
          case -3:
            currentArray = this.leaderboard_Houses;
            this.max_leaderboard_Houses = maxValue;
            break;
          case -2:
            currentArray = this.leaderboard_Factions;
            this.max_leaderboard_Factions = maxValue;
            break;
          case -1:
            currentArray = this.leaderboard_Main;
            this.max_leaderboard_Main = maxValue;
            break;
          case 0:
            currentArray = this.leaderboard_Sub_Pillager;
            this.max_leaderboard_Sub_Pillager = maxValue;
            break;
          case 1:
            currentArray = this.leaderboard_Sub_Defender;
            this.max_leaderboard_Sub_Defender = maxValue;
            break;
          case 2:
            currentArray = this.leaderboard_Sub_Ransack;
            this.max_leaderboard_Sub_Ransack = maxValue;
            break;
          case 3:
            currentArray = this.leaderboard_Sub_Wolfsbane;
            this.max_leaderboard_Sub_Wolfsbane = maxValue;
            break;
          case 4:
            currentArray = this.leaderboard_Sub_Banditkiller;
            this.max_leaderboard_Sub_Banditkiller = maxValue;
            break;
          case 5:
            currentArray = this.leaderboard_Sub_AIKiller;
            this.max_leaderboard_Sub_AIKiller = maxValue;
            break;
          case 6:
            currentArray = this.leaderboard_Sub_Trader;
            this.max_leaderboard_Sub_Trader = maxValue;
            break;
          case 7:
            currentArray = this.leaderboard_Sub_Forager;
            this.max_leaderboard_Sub_Forager = maxValue;
            break;
          case 8:
            currentArray = this.leaderboard_Sub_Stockpiler;
            this.max_leaderboard_Sub_Stockpiler = maxValue;
            break;
          case 9:
            currentArray = this.leaderboard_Sub_Farmer;
            this.max_leaderboard_Sub_Farmer = maxValue;
            break;
          case 10:
            currentArray = this.leaderboard_Sub_Brewer;
            this.max_leaderboard_Sub_Brewer = maxValue;
            break;
          case 11:
            currentArray = this.leaderboard_Sub_Weaponsmith;
            this.max_leaderboard_Sub_Weaponsmith = maxValue;
            break;
          case 12:
            currentArray = this.leaderboard_Sub_banquetter;
            this.max_leaderboard_Sub_banquetter = maxValue;
            break;
          case 13:
            currentArray = this.leaderboard_Sub_Achiever;
            this.max_leaderboard_Sub_Achiever = maxValue;
            break;
          case 14:
            currentArray = this.leaderboard_Sub_Donater;
            this.max_leaderboard_Sub_Donater = maxValue;
            break;
          case 15:
            currentArray = this.leaderboard_Sub_Capture;
            this.max_leaderboard_Sub_Capture = maxValue;
            break;
          case 16:
            currentArray = this.leaderboard_Sub_Raze;
            this.max_leaderboard_Sub_Raze = maxValue;
            break;
          case 17:
            currentArray = this.leaderboard_Sub_Glory;
            this.max_leaderboard_Sub_Glory = maxValue;
            break;
          case 18:
            currentArray = this.leaderboard_Sub_KillStreak;
            this.max_leaderboard_Sub_KillStreak = maxValue;
            break;
        }
        this.importLeaderboardData(currentArray, returnData.leaderboardType, returnData.mainLeaderboard, returnData.subLeaderboard, returnData.parishLeaderboard, returnData.houseLeaderboard, returnData.factionLeaderboard);
        LeaderBoardSearchResults results = new LeaderBoardSearchResults();
        switch (returnData.leaderboardType)
        {
          case -6:
          case -5:
          case -1:
            using (List<LeaderboardDataMainClass>.Enumerator enumerator = returnData.mainLeaderboard.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                LeaderboardDataMainClass current = enumerator.Current;
                results.entries.Add(current.standing);
              }
              break;
            }
          case -4:
            using (List<ParishFlagLeaderboardInfo>.Enumerator enumerator = returnData.parishLeaderboard.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                ParishFlagLeaderboardInfo current = enumerator.Current;
                results.entries.Add(current.standing);
              }
              break;
            }
          case -2:
            using (List<FactionLeaderboardInfo>.Enumerator enumerator = returnData.factionLeaderboard.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                FactionLeaderboardInfo current = enumerator.Current;
                results.entries.Add(current.standing);
              }
              break;
            }
          default:
            using (List<LeaderboardSubDataClass>.Enumerator enumerator = returnData.subLeaderboard.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                LeaderboardSubDataClass current = enumerator.Current;
                results.entries.Add(current.standing);
              }
              break;
            }
        }
        results.entries.Sort();
        results.searchString = returnData.searchString;
        results.category = returnData.leaderboardType;
        this.leaderboardSearchResults.Add(results);
        InterfaceMgr.Instance.leaderboardSearchComplete(results);
      }
      this.inLeaderboardSearch = false;
    }

    private void importLeaderboardData(
      SparseArray currentArray,
      int leaderboardType,
      List<LeaderboardDataMainClass> mainLeaderboard,
      List<LeaderboardSubDataClass> subLeaderboard,
      List<ParishFlagLeaderboardInfo> parishLeaderboard,
      List<HouseLeaderboardInfo> houseLeaderboard,
      List<FactionLeaderboardInfo> factionLeaderboard)
    {
      switch (leaderboardType)
      {
        case -6:
          foreach (LeaderboardDataMainClass leaderboardDataMainClass in mainLeaderboard)
          {
            int standing = leaderboardDataMainClass.standing;
            currentArray[standing] = (object) new LeaderBoardEntryData()
            {
              standing = standing,
              name = leaderboardDataMainClass.userName,
              house = leaderboardDataMainClass.house,
              value = leaderboardDataMainClass.numVillages,
              entryID = leaderboardDataMainClass.userID
            };
          }
          if (mainLeaderboard.Count != 0)
            break;
          this.lastZeroDownload_leaderboard_MainVillages = DateTime.Now;
          break;
        case -5:
          foreach (LeaderboardDataMainClass leaderboardDataMainClass in mainLeaderboard)
          {
            int standing = leaderboardDataMainClass.standing;
            LeaderBoardEntryData leaderBoardEntryData = new LeaderBoardEntryData();
            leaderBoardEntryData.standing = standing;
            leaderBoardEntryData.name = leaderboardDataMainClass.userName;
            leaderBoardEntryData.house = leaderboardDataMainClass.house;
            if (leaderboardDataMainClass.rank >= 0)
            {
              leaderBoardEntryData.value = leaderboardDataMainClass.rank;
              leaderBoardEntryData.male = true;
            }
            else
            {
              leaderBoardEntryData.value = -1 - leaderboardDataMainClass.rank;
              leaderBoardEntryData.male = false;
            }
            leaderBoardEntryData.entryID = leaderboardDataMainClass.userID;
            currentArray[standing] = (object) leaderBoardEntryData;
          }
          if (mainLeaderboard.Count != 0)
            break;
          this.lastZeroDownload_leaderboard_MainRank = DateTime.Now;
          break;
        case -4:
          foreach (ParishFlagLeaderboardInfo flagLeaderboardInfo in parishLeaderboard)
          {
            int standing = flagLeaderboardInfo.standing;
            currentArray[standing] = (object) new LeaderBoardEntryData()
            {
              standing = standing,
              name = this.getParishName(flagLeaderboardInfo.regionID),
              house = 0,
              value = flagLeaderboardInfo.points,
              entryID = flagLeaderboardInfo.regionID
            };
          }
          if (parishLeaderboard.Count != 0)
            break;
          this.lastZeroDownload_leaderboard_ParishFlags = DateTime.Now;
          break;
        case -3:
          int num = 1;
          foreach (HouseLeaderboardInfo houseLeaderboardInfo in houseLeaderboard)
          {
            int index = num++;
            currentArray[index] = (object) new LeaderBoardEntryData()
            {
              standing = index,
              name = (SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + houseLeaderboardInfo.houseID.ToString()),
              house = houseLeaderboardInfo.houseID,
              value = houseLeaderboardInfo.housePoints,
              entryID = houseLeaderboardInfo.houseID
            };
          }
          if (houseLeaderboard.Count != 0)
            break;
          this.lastZeroDownload_leaderboard_Houses = DateTime.Now;
          break;
        case -2:
          foreach (FactionLeaderboardInfo factionLeaderboardInfo in factionLeaderboard)
          {
            int standing = factionLeaderboardInfo.standing;
            currentArray[standing] = (object) new LeaderBoardEntryData()
            {
              standing = standing,
              name = factionLeaderboardInfo.factionname,
              house = factionLeaderboardInfo.house,
              value = factionLeaderboardInfo.factionPoints,
              entryID = factionLeaderboardInfo.factionID
            };
          }
          if (factionLeaderboard.Count != 0)
            break;
          this.lastZeroDownload_leaderboard_Factions = DateTime.Now;
          break;
        case -1:
          foreach (LeaderboardDataMainClass leaderboardDataMainClass in mainLeaderboard)
          {
            int standing = leaderboardDataMainClass.standing;
            currentArray[standing] = (object) new LeaderBoardEntryData()
            {
              standing = standing,
              name = leaderboardDataMainClass.userName,
              house = leaderboardDataMainClass.house,
              value = leaderboardDataMainClass.numPoints,
              entryID = leaderboardDataMainClass.userID
            };
          }
          if (mainLeaderboard.Count != 0)
            break;
          this.lastZeroDownload_leaderboard_Main = DateTime.Now;
          break;
        default:
          foreach (LeaderboardSubDataClass leaderboardSubDataClass in subLeaderboard)
          {
            int standing = leaderboardSubDataClass.standing;
            currentArray[standing] = (object) new LeaderBoardEntryData()
            {
              standing = standing,
              name = leaderboardSubDataClass.userName,
              house = leaderboardSubDataClass.house,
              value = leaderboardSubDataClass.data,
              entryID = leaderboardSubDataClass.userID
            };
          }
          if (subLeaderboard.Count != 0)
            break;
          switch (leaderboardType)
          {
            case 0:
              this.lastZeroDownload_leaderboard_Sub_Pillager = DateTime.Now;
              return;
            case 1:
              this.lastZeroDownload_leaderboard_Sub_Defender = DateTime.Now;
              return;
            case 2:
              this.lastZeroDownload_leaderboard_Sub_Ransack = DateTime.Now;
              return;
            case 3:
              this.lastZeroDownload_leaderboard_Sub_Wolfsbane = DateTime.Now;
              return;
            case 4:
              this.lastZeroDownload_leaderboard_Sub_Banditkiller = DateTime.Now;
              return;
            case 5:
              this.lastZeroDownload_leaderboard_Sub_AIKiller = DateTime.Now;
              return;
            case 6:
              this.lastZeroDownload_leaderboard_Sub_Trader = DateTime.Now;
              return;
            case 7:
              this.lastZeroDownload_leaderboard_Sub_Forager = DateTime.Now;
              return;
            case 8:
              this.lastZeroDownload_leaderboard_Sub_Stockpiler = DateTime.Now;
              return;
            case 9:
              this.lastZeroDownload_leaderboard_Sub_Farmer = DateTime.Now;
              return;
            case 10:
              this.lastZeroDownload_leaderboard_Sub_Brewer = DateTime.Now;
              return;
            case 11:
              this.lastZeroDownload_leaderboard_Sub_Weaponsmith = DateTime.Now;
              return;
            case 12:
              this.lastZeroDownload_leaderboard_Sub_banquetter = DateTime.Now;
              return;
            case 13:
              this.lastZeroDownload_leaderboard_Sub_Achiever = DateTime.Now;
              return;
            case 14:
              this.lastZeroDownload_leaderboard_Sub_Donater = DateTime.Now;
              return;
            case 15:
              this.lastZeroDownload_leaderboard_Sub_Capture = DateTime.Now;
              return;
            case 16:
              this.lastZeroDownload_leaderboard_Sub_Raze = DateTime.Now;
              return;
            case 17:
              this.lastZeroDownload_leaderboard_Sub_Glory = DateTime.Now;
              return;
            case 18:
              this.lastZeroDownload_leaderboard_Sub_KillStreak = DateTime.Now;
              return;
            default:
              return;
          }
      }
    }

    private void importStandings(int[,] standings)
    {
      this.dirtyStanding = true;
      this.leaderboardSelfRankings.Clear();
      int leaderboardCategories = GameEngine.Instance.LocalWorldData.getNumberOfLeaderboardCategories();
      for (int index = 0; index < leaderboardCategories; ++index)
      {
        if (standings[index, 0] > 0)
        {
          LeaderBoardSelfRankings boardSelfRankings = new LeaderBoardSelfRankings();
          boardSelfRankings.place = standings[index, 0];
          boardSelfRankings.value = standings[index, 1];
          boardSelfRankings.oldPlace = standings[index, 2];
          if (index < 15)
          {
            boardSelfRankings.category = index;
          }
          else
          {
            switch (index)
            {
              case 15:
                boardSelfRankings.category = -1;
                break;
              case 16:
                boardSelfRankings.category = -5;
                break;
              case 17:
                boardSelfRankings.category = -6;
                break;
              case 18:
                boardSelfRankings.category = 15;
                break;
              case 19:
                boardSelfRankings.category = 16;
                break;
              case 20:
                boardSelfRankings.category = 17;
                break;
              case 21:
                boardSelfRankings.category = 18;
                break;
            }
          }
          this.leaderboardSelfRankings.Add(boardSelfRankings);
        }
      }
      this.leaderboardSelfRankings.Sort((IComparer<LeaderBoardSelfRankings>) this.leaderboardSelfRankingsComparer);
      if (this.getGameDay() < 30)
        return;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      foreach (LeaderBoardSelfRankings leaderboardSelfRanking in this.leaderboardSelfRankings)
      {
        if (leaderboardSelfRanking.place <= 1)
          flag1 = true;
        if (leaderboardSelfRanking.place <= 5)
          flag2 = true;
        if (leaderboardSelfRanking.place <= 20)
          flag3 = true;
        if (leaderboardSelfRanking.place <= 100)
          flag4 = true;
      }
      List<int> userAchievements = RemoteServices.Instance.UserAchievements;
      if (userAchievements == null)
        return;
      List<int> achievementToTest = new List<int>();
      if (flag4)
      {
        if (!userAchievements.Contains(321))
          achievementToTest.Add(321);
        if (flag3)
        {
          if (!userAchievements.Contains(268435777))
            achievementToTest.Add(268435777);
          if (flag2)
          {
            if (!userAchievements.Contains(536871233))
              achievementToTest.Add(536871233);
            if (flag1 && !userAchievements.Contains(1073742145))
              achievementToTest.Add(1073742145);
          }
        }
      }
      if (achievementToTest.Count <= 0)
        return;
      GameEngine.Instance.World.testAchievements(achievementToTest, new List<AchievementData>(), false);
    }

    public bool areSelfStandingsDirty()
    {
      bool dirtyStanding = this.dirtyStanding;
      this.dirtyStanding = false;
      return dirtyStanding;
    }

    public LeaderBoardSelfRankings getLeaderboardBestRanking(int row)
    {
      return row >= this.leaderboardSelfRankings.Count ? (LeaderBoardSelfRankings) null : this.leaderboardSelfRankings[row];
    }

    public string GetLeaderboardCategoryScore(int category, int value, WorldData worldData)
    {
      NumberFormatInfo nfi = GameEngine.NFI;
      switch (category)
      {
        case -6:
          return "(" + SK.Text("Stats_Villages", "Villages") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case -5:
          int rank = value / 100;
          int rankSubLevel = value % 100;
          return "(" + Rankings.getRankingName(worldData, rank, rankSubLevel, true) + ")";
        case -1:
          return "(" + SK.Text("Stats_Points", "Points") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 0:
          return "(" + SK.Text("Stats_Resources_Pillages", "Resources Pillaged") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 1:
          return "(" + SK.Text("Stats_Attacked_Killed", "Attackers Killed") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 2:
          return "(" + SK.Text("Stats_Buildings_Destroyed", "Buildings Destroyed") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 3:
          return "(" + SK.Text("Stats_Wolves_Killed", "Wolves Killed") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 4:
          return "(" + SK.Text("Stats_Bandits_Killed", "Bandits Killed") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 5:
          return "(" + SK.Text("Stats_AI_Killed", "AI Troops Killed") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 6:
          return "(" + SK.Text("Stats_Packets_Traded", "Packets Traded") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 7:
          return "(" + SK.Text("Stats_Packets_Foraged", "Packets Foraged") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 8:
          return "(" + SK.Text("Stats_Packets_Produced", "Packets Produced") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 9:
          return "(" + SK.Text("Stats_Packets_Produced", "Packets Produced") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 10:
          return "(" + SK.Text("Stats_Packets_Produced", "Packets Produced") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 11:
          return "(" + SK.Text("Stats_Packets_Produced", "Packets Produced") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 12:
          return "(" + SK.Text("Stats_Packets_Produced", "Packets Produced") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 13:
          return "(" + SK.Text("User_Quests_Complete", "Quests Completed") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 14:
          return "(" + SK.Text("Stats_Packets_Donated", "Packets Donated") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 15:
          return "(" + SK.Text("Stats_Villages_Captured", "Villages Captured") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 16:
          return "(" + SK.Text("Stats_Villages_Razed", "Villages Razed") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 17:
          return "(" + SK.Text("Stats_Glory_Generated", "Glory Generated") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        case 18:
          return "(" + SK.Text("Stats_killstreak_Generated", "Kill Streak") + " : " + value.ToString("N", (IFormatProvider) nfi) + ")";
        default:
          throw new ArgumentException("Invalid leaderboard category: " + (object) category);
      }
    }

    public void updateRegionsNamesBasedOnLanguage()
    {
      if (!this.chMap)
        return;
      string[,] strArray1;
      string[,] strArray2;
      string[,] strArray3;
      string[] strArray4;
      switch (Program.mySettings.languageIdent)
      {
        case "zh":
        case "zhhk":
          strArray1 = this.china_country_Simplified;
          strArray2 = this.china_province_Simplified;
          strArray3 = this.china_county_Simplified;
          strArray4 = this.china_parish_Simplified;
          break;
        default:
          strArray1 = this.china_country_English;
          strArray2 = this.china_province_English;
          strArray3 = this.china_county_English;
          strArray4 = this.china_parish_English;
          break;
      }
      try
      {
        for (int countryID = 0; countryID < 6; ++countryID)
        {
          this.getVillageData(this.getCountryCapital(countryID)).villageName = strArray1[countryID, 1];
          this.countryList[countryID].areaName = strArray1[countryID, 0];
        }
        for (int provinceID = 0; provinceID < 13; ++provinceID)
        {
          this.getVillageData(this.getProvinceCapital(provinceID)).villageName = strArray2[provinceID, 1];
          this.provincesList[provinceID].areaName = strArray2[provinceID, 0];
        }
        for (int countyID = 0; countyID < 73; ++countyID)
        {
          this.getVillageData(this.getCountyCapitalVillage(countyID)).villageName = strArray3[countyID, 1];
          this.countyList[countyID].areaName = strArray3[countyID, 0];
        }
        for (int parishID = 0; parishID < 5411; ++parishID)
          this.fixupParishName(parishID, this.getParishCapital(parishID), strArray4[parishID]);
      }
      catch (Exception ex)
      {
      }
    }

    public void LoadLocalisedParishNamesFromFiles()
    {
      if (this.china_parish_Simplified == null)
        this.china_parish_Simplified = this.getParishNamesFromFile("zh");
      if (this.china_parish_English != null)
        return;
      this.china_parish_English = this.getParishNamesFromFile("zhEN");
    }

    private string[] getParishNamesFromFile(string ident)
    {
      List<string> stringList = new List<string>();
      FileStream fileStream = new FileStream(Application.StartupPath + "\\Localization\\parishes-" + ident + ".txt", FileMode.Open, FileAccess.Read);
      StreamReader streamReader = new StreamReader((Stream) fileStream);
      string str;
      while ((str = streamReader.ReadLine()) != null)
      {
        int startIndex = str.IndexOf("//");
        stringList.Add(str.Remove(startIndex).Trim());
      }
      streamReader.Close();
      fileStream.Close();
      return stringList.ToArray();
    }

    public struct ArmyRetrieveData
    {
      public long armyID;
      public DateTime expiryTime;
    }

    public class LocalArmyData
    {
      public long armyID;
      public int homeVillageID;
      public int travelFromVillageID;
      public int userID;
      public int attackType;
      public int targetVillageID;
      public int numPeasants;
      public int numArchers;
      public int numPikemen;
      public int numSwordsmen;
      public int numCatapults;
      public int numCaptains;
      public int numScouts;
      public int captainsCommand;
      public int lootType;
      public double lootLevel;
      public ArmyLootData lootData;
      public double displayX;
      public double displayY;
      public double baseDisplayX;
      public double baseDisplayY;
      public double targetDisplayX;
      public double targetDisplayY;
      public double localStartTime;
      public double localEndTime;
      public double fakeEndTime;
      public int aiPlayer;
      public DateTime serverStartTime;
      public DateTime serverEndTime;
      public bool carryingFlag;
      public bool seaTravel;
      public bool requestSent;
      public bool dead;
      public bool realData = true;
      public bool reinforcements;
      public bool visible = true;
      public bool singlyAdded;
      public static double groupCurrentTime;

      public bool reinforcementsAtTarget
      {
        get
        {
          return this.reinforcements && this.displayX == this.baseDisplayX && this.displayY == this.baseDisplayY;
        }
      }

      public bool isVisible(RectangleF screenRect)
      {
        return (double) screenRect.Top - 50.0 <= this.displayY && (double) screenRect.Left - 50.0 <= this.displayX && (double) screenRect.Bottom + 50.0 >= this.displayY && (double) screenRect.Right + 50.0 >= this.displayX;
      }

      public bool isScouts() => this.numScouts > 0;

      public bool isCaptainAttack() => this.numCaptains > 0 || this.attackType == 18;

      public void createJourney(DateTime startTime, DateTime curTime, DateTime endTime)
      {
        endTime = endTime.AddSeconds(2.0);
        this.serverStartTime = startTime;
        this.serverEndTime = endTime;
        if (curTime > endTime && this.reinforcements)
        {
          this.visible = false;
        }
        else
        {
          this.visible = true;
          double num = DXTimer.GetCurrentMilliseconds() / 1000.0;
          TimeSpan timeSpan1 = curTime - startTime;
          this.localStartTime = num - timeSpan1.TotalSeconds;
          TimeSpan timeSpan2 = endTime - curTime;
          this.localEndTime = num + timeSpan2.TotalSeconds;
          this.fakeEndTime = this.localEndTime + 3.0;
          this.requestSent = false;
          if (this.attackType != 21 || GameEngine.Instance.World.isUserVillage(this.homeVillageID))
            return;
          this.visible = false;
        }
      }

      public void updatePosition()
      {
        if (this.targetVillageID >= 0 && this.visible)
        {
          double num1 = WorldMap.LocalArmyData.groupCurrentTime / 1000.0;
          double localStartTime = this.localStartTime;
          double localEndTime = this.localEndTime;
          if (this.lootType >= 0)
          {
            localStartTime += 3.0;
            localEndTime += 3.0;
          }
          double num2 = (num1 - localStartTime) / (localEndTime - localStartTime);
          double num3 = (num1 - localStartTime) / (this.fakeEndTime - localStartTime);
          double num4 = num2;
          if (!this.reinforcements)
          {
            if (this.lootType < 0)
            {
              num4 = num3;
              if (num2 > 1.0)
              {
                if (this.attackType == 30 || this.attackType == 31)
                  this.dead = true;
                else if (!this.requestSent && num1 - this.localEndTime > 1.0 && num1 - this.localEndTime < 60.0)
                {
                  if ((RemoteServices.Instance.UserID == this.userID || GameEngine.Instance.World.isUserVillage(this.targetVillageID)) && GameEngine.Instance.World.checkRecentRetrievalSend(this.armyID))
                  {
                    if (this.attackType == 13)
                      GameEngine.Instance.World.tutorialArmyID = this.armyID;
                    RemoteServices.Instance.RetrieveAttackResult(this.armyID, GameEngine.Instance.World.StoredVillageFactionPos);
                    GameEngine.Instance.World.requestedReturnedArmyIDs.Add(new WorldMap.ArmyRetrieveData()
                    {
                      armyID = this.armyID,
                      expiryTime = DateTime.Now.AddSeconds(30.0)
                    });
                  }
                  this.requestSent = true;
                }
              }
            }
            if (num4 > 1.0)
            {
              if (this.lootType < 0)
              {
                if (GameEngine.Instance.LocalWorldData.AIWorld && this.attackType == 17)
                {
                  this.dead = true;
                }
                else
                {
                  this.requestSent = false;
                  num4 = 0.0;
                  this.lootType = 10000;
                  double num5 = this.localEndTime - this.localStartTime;
                  this.localStartTime = this.localEndTime;
                  this.localEndTime += num5;
                  this.fakeEndTime += num5;
                  this.realData = false;
                }
              }
              else
              {
                this.dead = true;
                VillageMap village = GameEngine.Instance.getVillage(this.travelFromVillageID);
                if (village != null && this.realData)
                {
                  village.addTroopsArmyReturnSpecial(this.numPeasants, this.numArchers, this.numPikemen, this.numSwordsmen, this.numCatapults, this.numScouts, this.numCaptains);
                  if (this.numScouts > 0 && this.lootType >= 100 && this.lootType <= 199)
                    village.addResources(this.lootType - 100, (int) this.lootLevel);
                  if (this.lootType == 2 && this.lootData != null)
                  {
                    village.addResources(6, this.lootData.woodLevel);
                    village.addResources(7, this.lootData.stoneLevel);
                    village.addResources(8, this.lootData.ironLevel);
                    village.addResources(9, this.lootData.pitchLevel);
                    village.addResources(13, this.lootData.applesLevel);
                    village.addResources(14, this.lootData.breadLevel);
                    village.addResources(17, this.lootData.cheeseLevel);
                    village.addResources(16, this.lootData.meatLevel);
                    village.addResources(18, this.lootData.fishLevel);
                    village.addResources(15, this.lootData.vegLevel);
                    village.addResources(12, this.lootData.aleLevel);
                    village.addResources(21, this.lootData.furnitureLevel);
                    village.addResources(19, this.lootData.clothesLevel);
                    village.addResources(22, this.lootData.venisonLevel);
                    village.addResources(23, this.lootData.saltLevel);
                    village.addResources(33, this.lootData.wineLevel);
                    village.addResources(26, this.lootData.metalwareLevel);
                    village.addResources(24, this.lootData.spicesLevel);
                    village.addResources(25, this.lootData.silkLevel);
                    village.addResources(29, this.lootData.bowsLevel);
                    village.addResources(28, this.lootData.pikesLevel);
                    village.addResources(30, this.lootData.swordsLevel);
                    village.addResources(31, this.lootData.armourLevel);
                    village.addResources(32, this.lootData.catapultLevel);
                  }
                  else if (this.lootType >= 500 && this.lootType < 1000 && this.lootType < 700)
                    village.addResources(this.lootType, (int) this.lootLevel);
                }
                else if (village != null)
                  GameEngine.Instance.flushVillage(village.VillageID);
              }
            }
            if (num4 < 0.0)
              num4 = 0.0;
            if (this.lootType < 0)
            {
              this.displayX = (this.targetDisplayX - this.baseDisplayX) * num4 + this.baseDisplayX;
              this.displayY = (this.targetDisplayY - this.baseDisplayY) * num4 + this.baseDisplayY;
            }
            else
            {
              this.displayX = (this.baseDisplayX - this.targetDisplayX) * num4 + this.targetDisplayX;
              this.displayY = (this.baseDisplayY - this.targetDisplayY) * num4 + this.targetDisplayY;
            }
          }
          else if (num2 > 1.0)
          {
            this.visible = false;
            VillageMap village = GameEngine.Instance.getVillage(this.homeVillageID);
            if (this.attackType != 21 || village == null)
              return;
            this.dead = true;
            village.addTroops(this.numPeasants, this.numArchers, this.numPikemen, this.numSwordsmen, this.numCatapults, this.numScouts);
            this.numPeasants = this.numArchers = this.numPikemen = this.numSwordsmen = this.numCatapults = 0;
          }
          else if (this.attackType == 20)
          {
            this.displayX = (this.targetDisplayX - this.baseDisplayX) * num4 + this.baseDisplayX;
            this.displayY = (this.targetDisplayY - this.baseDisplayY) * num4 + this.baseDisplayY;
          }
          else
          {
            this.displayX = (this.baseDisplayX - this.targetDisplayX) * num4 + this.targetDisplayX;
            this.displayY = (this.baseDisplayY - this.targetDisplayY) * num4 + this.targetDisplayY;
          }
        }
        else
        {
          this.displayX = this.baseDisplayX;
          this.displayY = this.baseDisplayY;
        }
      }

      public PointF BasePoint()
      {
        return !this.reinforcements ? (this.lootType < 0 ? new PointF((float) this.baseDisplayX, (float) this.baseDisplayY) : new PointF((float) this.targetDisplayX, (float) this.targetDisplayY)) : (this.attackType == 20 ? new PointF((float) this.baseDisplayX, (float) this.baseDisplayY) : new PointF((float) this.targetDisplayX, (float) this.targetDisplayY));
      }

      public PointF TargetPoint()
      {
        return !this.reinforcements ? (this.lootType < 0 ? new PointF((float) this.targetDisplayX, (float) this.targetDisplayY) : new PointF((float) this.baseDisplayX, (float) this.baseDisplayY)) : (this.attackType == 20 ? new PointF((float) this.targetDisplayX, (float) this.targetDisplayY) : new PointF((float) this.baseDisplayX, (float) this.baseDisplayY));
      }

      public TroopCount GetTroopCount()
      {
        return new TroopCount()
        {
          peasants = this.numPeasants,
          archers = this.numArchers,
          pikemen = this.numPikemen,
          swordsmen = this.numSwordsmen,
          catapults = this.numCatapults,
          captains = this.numCaptains
        };
      }
    }

    public class FWData
    {
      public int spriteID;
      public int numToSpawn = 25;
      public bool symmetrical;
      public bool randomStartRotation;
      public float rotateSpeed;
      public bool rotateClockwise = true;
      public float startScale = 1f;
      public float scaleSpeed;
      public float scaleTarget = 1f;
      public int fadeInTime;
      public int fadeOutTime;
      public float fadeRate;
      public float initialVelocity = 1f;
      public float maxVelocity = 1f;
      public float acceleration = 1f;
      public float speedVariance = 0.3f;
      public int childFirework;
    }

    private enum FW_Resources
    {
      apple = 1,
      bread = 2,
      meat = 3,
      veg = 4,
      fish = 5,
      dairy = 6,
      wood = 7,
      stone = 8,
      iron = 9,
      pitch = 10, // 0x0000000A
      ale = 11, // 0x0000000B
      furniture = 12, // 0x0000000C
      clothes = 13, // 0x0000000D
      wine = 14, // 0x0000000E
      metalware = 15, // 0x0000000F
      salt = 16, // 0x00000010
      venison = 17, // 0x00000011
      spices = 18, // 0x00000012
      silk = 19, // 0x00000013
      bows = 20, // 0x00000014
      poles = 21, // 0x00000015
      swords = 22, // 0x00000016
      armour = 23, // 0x00000017
      catapults = 24, // 0x00000018
    }

    private class ClusterBase
    {
      public int unique;
      public List<WorldMap.Burst> bursts = new List<WorldMap.Burst>();
      public int startVillage = -1;
      public int targetX = -1;
      public int targetY = -1;
      public DateTime startTime;
      public DateTime endTime;
      public bool parentVisible = true;
      public int spriteID;
      public int type;
      public int[] fwS;
    }

    private class Burst
    {
      public int unique;
      public float startX = -1f;
      public float startY = -1f;
      public float dX;
      public float dY;
      public float speed = 0.1f;
      public float acceleration;
      public DateTime startTime;
      public int spriteID;
      public float curRotation;
      public float rotationValue;
      public float scale = 1f;
      public float scaleDiff;
      public float alpha;
      public int fadeState;
      public WorldMap.FWData definition;
    }

    public class UserVillageData
    {
      public int villageID;
      public bool capital;
      public bool parishCapital;
      public bool countyCapital;
      public bool provinceCapital;
      public bool countryCapital;
      public List<int> vassals = new List<int>();
    }

    public class VillageNameItem
    {
      public string villageName;
      public int villageID;
      public bool capital;
      public int sortLevel;

      public override string ToString()
      {
        return this.capital && GameEngine.Instance.World.isUserVillage(this.villageID) ? this.villageName + "*" : this.villageName;
      }
    }

    public class VillageNameComparer : IComparer<WorldMap.UserVillageData>
    {
      public int Compare(WorldMap.UserVillageData x, WorldMap.UserVillageData y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null || x.capital && !y.capital)
          return 1;
        return !x.capital && y.capital ? -1 : GameEngine.Instance.World.getVillageNameOnly(x.villageID).CompareTo(GameEngine.Instance.World.getVillageNameOnly(y.villageID));
      }
    }

    public delegate void ResearchChangedDelegate();

    public class LocalPerson
    {
      public long personID;
      public PersonData person;
      public double displayX;
      public double displayY;
      public double baseDisplayX;
      public double baseDisplayY;
      public double targetDisplayX;
      public double targetDisplayY;
      public double localStartTime;
      public double localEndTime;
      public DateTime serverEndTime;
      public long parentPerson = -1;
      public int childrenCount;
      public double lastRatio;
      public bool seaTravel;
      public bool dying;

      public PointF BasePoint()
      {
        return this.person.state == 0 || this.person.state == 1 || this.person.state == 2 || this.person.state == 11 || this.person.state == 12 || this.person.state == 21 || this.person.state == 22 || this.person.state == 31 || this.person.state == 75 ? new PointF((float) this.baseDisplayX, (float) this.baseDisplayY) : new PointF((float) this.targetDisplayX, (float) this.targetDisplayY);
      }

      public PointF TargetPoint()
      {
        return this.person.state == 0 || this.person.state == 1 || this.person.state == 2 || this.person.state == 11 || this.person.state == 12 || this.person.state == 21 || this.person.state == 22 || this.person.state == 31 || this.person.state == 75 ? new PointF((float) this.targetDisplayX, (float) this.targetDisplayY) : new PointF((float) this.baseDisplayX, (float) this.baseDisplayY);
      }

      public bool isVisible(RectangleF screenRect)
      {
        return (double) screenRect.Top - 50.0 <= this.displayY && (double) screenRect.Left - 50.0 <= this.displayX && (double) screenRect.Bottom + 50.0 >= this.displayY && (double) screenRect.Right + 50.0 >= this.displayX;
      }

      public void createJourney(DateTime startTime, DateTime curTime, DateTime endTime)
      {
        this.serverEndTime = endTime;
        double num = DXTimer.GetCurrentMilliseconds() / 1000.0;
        TimeSpan timeSpan1 = curTime - startTime;
        this.localStartTime = num - timeSpan1.TotalSeconds;
        TimeSpan timeSpan2 = endTime - curTime;
        this.localEndTime = num + timeSpan2.TotalSeconds;
      }

      public void updatePosition(double realTime)
      {
        if (this.person.state == 2 || this.person.state == 12 || this.person.state == 22)
        {
          this.displayX = (this.targetDisplayX - this.baseDisplayX) * 1.0 + this.baseDisplayX;
          this.displayY = (this.targetDisplayY - this.baseDisplayY) * 1.0 + this.baseDisplayY;
        }
        else if (this.person.state > 0)
        {
          double num = this.localEndTime == this.localStartTime ? 1.1 : (realTime - this.localStartTime) / (this.localEndTime - this.localStartTime);
          if (num > 1.0)
          {
            if (this.person.state == 1 || this.person.state == 11 || this.person.state == 21)
            {
              num = 1.0;
              ++this.person.state;
            }
            else if (this.person.state == 50)
            {
              num = 0.0;
              this.person.state = 0;
              this.person.lastSpyTime = VillageMap.getCurrentServerTime();
            }
            else if (this.person.state == 31 || this.person.state == 75)
            {
              this.dying = true;
              if (this.person.state == 75)
              {
                int parishFromVillageId = GameEngine.Instance.World.getParishFromVillageID(this.person.targetVillageID);
                GameEngine.Instance.World.givePlaguesToParish(parishFromVillageId);
              }
              GameEngine.Instance.World.clearVillageRolloverInfo(this.person.targetVillageID);
            }
          }
          else if (num < 0.0)
            num = 0.0;
          this.lastRatio = num;
          if (this.person.state == 1 || this.person.state == 11 || this.person.state == 21 || this.person.state == 31 || this.person.state == 75)
          {
            this.displayX = (this.targetDisplayX - this.baseDisplayX) * num + this.baseDisplayX;
            this.displayY = (this.targetDisplayY - this.baseDisplayY) * num + this.baseDisplayY;
          }
          else if (this.person.state == 50)
          {
            this.displayX = (this.baseDisplayX - this.targetDisplayX) * num + this.targetDisplayX;
            this.displayY = (this.baseDisplayY - this.targetDisplayY) * num + this.targetDisplayY;
          }
          else
          {
            this.displayX = this.baseDisplayX;
            this.displayY = this.baseDisplayY;
          }
        }
        else
        {
          this.displayX = this.baseDisplayX;
          this.displayY = this.baseDisplayY;
        }
      }
    }

    private class CapitalPeopleGFX
    {
      public int numYours;
      public int numOthers;
      public float posX;
      public float posY;
    }

    public class VillageQuadNode
    {
      private static WorldMap world;
      public int m_level;
      public int m_x;
      public int m_y;
      public int m_width;
      public int m_height;
      public int m_centreX;
      public int m_centreY;
      public WorldMap.VillageQuadNode m_TL;
      public WorldMap.VillageQuadNode m_TR;
      public WorldMap.VillageQuadNode m_BL;
      public WorldMap.VillageQuadNode m_BR;
      public List<VillageData> m_villageList;
      public List<VillageData> m_capitalList;
      public List<VillageData> m_parishCapitalList;
      public bool m_drawLevel;
      public bool m_childrenDisplayed;
      public static int level0Nodes;
      public static int level1Nodes;
      public static int level2Nodes;
      public static int level3Nodes;
      public static int level4Nodes;
      public static int level5Nodes;
      public static int level6Nodes;
      public static int level7Nodes;
      public static int level8Nodes;
      public static int level9Nodes;
      public static int villagesInNodes;
      public static int parishesInNodes;
      public static int capitalsInNodes;

      public VillageQuadNode(int x, int y, int width, int height, int level)
      {
        this.m_x = x;
        this.m_y = y;
        this.m_width = width;
        this.m_height = height;
        this.m_centreX = width / 2 + x;
        this.m_centreY = height / 2 + y;
        switch (level)
        {
          case 0:
            ++WorldMap.VillageQuadNode.level0Nodes;
            break;
          case 1:
            ++WorldMap.VillageQuadNode.level1Nodes;
            break;
          case 2:
            ++WorldMap.VillageQuadNode.level2Nodes;
            break;
          case 3:
            ++WorldMap.VillageQuadNode.level3Nodes;
            break;
          case 4:
            ++WorldMap.VillageQuadNode.level4Nodes;
            break;
          case 5:
            ++WorldMap.VillageQuadNode.level5Nodes;
            break;
          case 6:
            ++WorldMap.VillageQuadNode.level6Nodes;
            break;
          case 7:
            ++WorldMap.VillageQuadNode.level7Nodes;
            break;
          case 8:
            ++WorldMap.VillageQuadNode.level8Nodes;
            break;
          case 9:
            ++WorldMap.VillageQuadNode.level9Nodes;
            break;
        }
      }

      public void setWorld(WorldMap newWorld) => WorldMap.VillageQuadNode.world = newWorld;

      public void addVillage(VillageData village, int level)
      {
        if (level < 5)
        {
          if ((int) village.x < this.m_centreX)
          {
            if ((int) village.y < this.m_centreY)
            {
              if (this.m_TL == null)
                this.m_TL = new WorldMap.VillageQuadNode(this.m_x, this.m_y, this.m_width / 2 + 1, this.m_height / 2 + 1, level);
              this.m_TL.addVillage(village, level + 1);
            }
            else
            {
              if (this.m_BL == null)
                this.m_BL = new WorldMap.VillageQuadNode(this.m_x, this.m_centreY, this.m_width / 2 + 1, this.m_height / 2 + 1, level);
              this.m_BL.addVillage(village, level + 1);
            }
          }
          else if ((int) village.y < this.m_centreY)
          {
            if (this.m_TR == null)
              this.m_TR = new WorldMap.VillageQuadNode(this.m_centreX, this.m_y, this.m_width / 2 + 1, this.m_height / 2 + 1, level);
            this.m_TR.addVillage(village, level + 1);
          }
          else
          {
            if (this.m_BR == null)
              this.m_BR = new WorldMap.VillageQuadNode(this.m_centreX, this.m_centreY, this.m_width / 2 + 1, this.m_height / 2 + 1, level);
            this.m_BR.addVillage(village, level + 1);
          }
        }
        else
        {
          this.m_drawLevel = true;
          if (village.Capital || WorldMap.VillageQuadNode.world.aiWorldTreeBuilding && WorldMap.VillageQuadNode.world.aiWorldSpecialVillages.Contains(village.id))
          {
            if (this.m_capitalList == null)
              this.m_capitalList = new List<VillageData>();
            if (this.m_parishCapitalList == null)
              this.m_parishCapitalList = new List<VillageData>();
            if (village.regionCapital)
            {
              this.m_parishCapitalList.Add(village);
              ++WorldMap.VillageQuadNode.parishesInNodes;
            }
            else
            {
              this.m_capitalList.Add(village);
              ++WorldMap.VillageQuadNode.capitalsInNodes;
            }
          }
          else
          {
            if (this.m_villageList == null)
              this.m_villageList = new List<VillageData>();
            this.m_villageList.Add(village);
            ++WorldMap.VillageQuadNode.villagesInNodes;
          }
        }
      }

      public bool isAreaVisible(WorldMap.FastScreenRect screenRect)
      {
        int num = 50;
        return screenRect.top - num <= this.m_y + this.m_height && screenRect.left - num <= this.m_x + this.m_width && screenRect.bottom + num >= this.m_y && screenRect.right + num >= this.m_x;
      }

      private void drawVillagesInNode(WorldMap.FastScreenRect screenRect)
      {
        foreach (VillageData village in this.m_villageList)
        {
          if (village.visible)
          {
            float scrX = ((float) village.x - screenRect.Left) / screenRect.Width;
            float scrY = ((float) village.y - screenRect.Top) / screenRect.Height;
            if ((double) scrX >= -0.10000000149011612 && (double) scrX <= 1.1000000238418579 && (double) scrY >= -0.10000000149011612 && (double) scrY <= 1.1000000238418579)
              WorldMap.VillageQuadNode.world.drawVillage(village, (double) scrX, (double) scrY);
          }
        }
      }

      private void drawVillagesInNodeEditMode(WorldMap.FastScreenRect screenRect)
      {
        foreach (VillageData village in this.m_villageList)
        {
          if (village.visible || village.Capital)
          {
            float scrX = ((float) village.x - screenRect.Left) / screenRect.Width;
            float scrY = ((float) village.y - screenRect.Top) / screenRect.Height;
            if ((double) scrX >= -0.10000000149011612 && (double) scrX <= 1.1000000238418579 && (double) scrY >= -0.10000000149011612 && (double) scrY <= 1.1000000238418579)
              WorldMap.VillageQuadNode.world.drawVillage(village, (double) scrX, (double) scrY);
          }
        }
      }

      private void drawParishesInNode(WorldMap.FastScreenRect screenRect)
      {
        foreach (VillageData parishCapital in this.m_parishCapitalList)
        {
          if (parishCapital.visible)
          {
            float scrX = ((float) parishCapital.x - screenRect.Left) / screenRect.Width;
            float scrY = ((float) parishCapital.y - screenRect.Top) / screenRect.Height;
            if ((double) scrX >= -0.10000000149011612 && (double) scrX <= 1.1000000238418579 && (double) scrY >= -0.10000000149011612 && (double) scrY <= 1.1000000238418579)
              WorldMap.VillageQuadNode.world.drawVillage(parishCapital, (double) scrX, (double) scrY);
          }
        }
      }

      private void drawCapitalsInNode(WorldMap.FastScreenRect screenRect)
      {
        foreach (VillageData capital in this.m_capitalList)
        {
          if (capital.visible)
          {
            float scrX = ((float) capital.x - screenRect.Left) / screenRect.Width;
            float scrY = ((float) capital.y - screenRect.Top) / screenRect.Height;
            if ((double) scrX >= -0.10000000149011612 && (double) scrX <= 1.1000000238418579 && (double) scrY >= -0.10000000149011612 && (double) scrY <= 1.1000000238418579)
              WorldMap.VillageQuadNode.world.drawVillage(capital, (double) scrX, (double) scrY);
          }
        }
      }

      private void drawCapitalsInNodeEditMode(WorldMap.FastScreenRect screenRect)
      {
        foreach (VillageData capital in this.m_capitalList)
        {
          if (capital.visible || capital.countyCapital)
          {
            float scrX = ((float) capital.x - screenRect.Left) / screenRect.Width;
            float scrY = ((float) capital.y - screenRect.Top) / screenRect.Height;
            if ((double) scrX >= -0.10000000149011612 && (double) scrX <= 1.1000000238418579 && (double) scrY >= -0.10000000149011612 && (double) scrY <= 1.1000000238418579)
              WorldMap.VillageQuadNode.world.drawVillage(capital, (double) scrX, (double) scrY);
          }
        }
      }

      public void drawVillages(WorldMap.FastScreenRect screenRect)
      {
        if (this.m_drawLevel)
        {
          double num1 = 8.0;
          double num2 = 5.0;
          if ((screenRect.zoomLevel >= num1 || GameEngine.Instance.World.SeventhAgeWorld && screenRect.zoomLevel >= num2) && this.m_villageList != null)
          {
            if (!GameEngine.Instance.World.mapEditing)
              this.drawVillagesInNode(screenRect);
            else
              this.drawVillagesInNodeEditMode(screenRect);
          }
          if (screenRect.zoomLevel >= num2 && this.m_parishCapitalList != null)
            this.drawParishesInNode(screenRect);
          if (this.m_capitalList == null)
            return;
          if (!GameEngine.Instance.World.mapEditing)
            this.drawCapitalsInNode(screenRect);
          else
            this.drawCapitalsInNodeEditMode(screenRect);
        }
        else
        {
          if (!this.isAreaVisible(screenRect))
            return;
          if (this.m_TL != null)
            this.m_TL.drawVillages(screenRect);
          if (this.m_TR != null)
            this.m_TR.drawVillages(screenRect);
          if (this.m_BL != null)
            this.m_BL.drawVillages(screenRect);
          if (this.m_BR == null)
            return;
          this.m_BR.drawVillages(screenRect);
        }
      }
    }

    private class ParishChatData
    {
      public List<Chat_TextEntry>[] m_pages = new List<Chat_TextEntry>[6];
      public DateTime m_newestPost = DateTime.MinValue;
      public long[] m_readIDs = new long[6];

      public void init()
      {
        for (int index = 0; index < 6; ++index)
        {
          this.m_pages[index] = new List<Chat_TextEntry>();
          this.m_readIDs[index] = -1L;
        }
      }

      public List<Chat_TextEntry> getChatPage(int pageID)
      {
        List<Chat_TextEntry> chatPage = (List<Chat_TextEntry>) null;
        switch (pageID)
        {
          case 0:
            chatPage = this.m_pages[0];
            break;
          case 1:
            chatPage = this.m_pages[1];
            break;
          case 2:
            chatPage = this.m_pages[2];
            break;
          case 3:
            chatPage = this.m_pages[3];
            break;
          case 4:
            chatPage = this.m_pages[4];
            break;
          case 5:
            chatPage = this.m_pages[5];
            break;
        }
        return chatPage;
      }

      public long getReadID(int pageID) => this.m_readIDs[pageID];

      public void setReadIDs(long[] readIDs)
      {
        for (int index = 0; index < 6; ++index)
        {
          if (this.m_readIDs[index] < readIDs[index])
            this.m_readIDs[index] = readIDs[index];
        }
      }

      public bool addEntry(Chat_TextEntry textEntry)
      {
        List<Chat_TextEntry> chatPage = this.getChatPage(textEntry.roomID);
        if (chatPage == null)
          return false;
        foreach (Chat_TextEntry chatTextEntry in chatPage)
        {
          if (chatTextEntry.textID == textEntry.textID)
            return false;
        }
        chatPage.Add(textEntry);
        if (textEntry.postedTime > this.m_newestPost)
          this.m_newestPost = textEntry.postedTime;
        return true;
      }
    }

    public class ParishChatComparer : IComparer<Chat_TextEntry>
    {
      public int Compare(Chat_TextEntry x, Chat_TextEntry y)
      {
        return x == null ? (y == null ? 0 : -1) : (y == null ? 1 : y.textID.CompareTo(x.textID));
      }
    }

    private class ParishWallDonateInfo
    {
      public ParishWallDetailInfo_ReturnType returnData;
      public DateTime lastUpdateTime = DateTime.MinValue;
    }

    public class LocalTrader
    {
      public long traderID;
      public MarketTraderData trader;
      public double displayX;
      public double displayY;
      public double baseDisplayX;
      public double baseDisplayY;
      public double targetDisplayX;
      public double targetDisplayY;
      public double localStartTime;
      public double localEndTime;
      public DateTime serverEndTime;
      public long parentTrader = -1;
      public double lastRatio;
      public bool dying;
      public bool seaTravel;

      public PointF BasePoint()
      {
        return this.trader.traderState == 0 || this.trader.traderState == 1 || this.trader.traderState == 3 || this.trader.traderState == 5 ? new PointF((float) this.baseDisplayX, (float) this.baseDisplayY) : new PointF((float) this.targetDisplayX, (float) this.targetDisplayY);
      }

      public PointF TargetPoint()
      {
        return this.trader.traderState == 0 || this.trader.traderState == 1 || this.trader.traderState == 3 || this.trader.traderState == 5 ? new PointF((float) this.targetDisplayX, (float) this.targetDisplayY) : new PointF((float) this.baseDisplayX, (float) this.baseDisplayY);
      }

      public bool isVisible(RectangleF screenRect)
      {
        return (double) screenRect.Top - 50.0 <= this.displayY && (double) screenRect.Left - 50.0 <= this.displayX && (double) screenRect.Bottom + 50.0 >= this.displayY && (double) screenRect.Right + 50.0 >= this.displayX;
      }

      public void createJourney(DateTime startTime, DateTime curTime, DateTime endTime)
      {
        this.serverEndTime = endTime;
        double num = DXTimer.GetCurrentMilliseconds() / 1000.0;
        TimeSpan timeSpan1 = curTime - startTime;
        this.localStartTime = num - timeSpan1.TotalSeconds;
        TimeSpan timeSpan2 = endTime - curTime;
        this.localEndTime = num + timeSpan2.TotalSeconds;
      }

      public void updatePosition(double currentMilliseconds)
      {
        if (this.trader.traderState > 0)
        {
          double num1 = currentMilliseconds / 1000.0;
          double num2 = this.localStartTime + 3.0;
          double num3 = this.localEndTime + 3.0;
          double num4 = (num1 - num2) / (num3 - num2);
          if (num4 < 0.0)
            num4 = 0.0;
          if (num4 > 1.0)
          {
            if (this.trader.traderState == 1)
            {
              num4 = 0.0;
              ++this.trader.traderState;
              double num5 = this.localEndTime - this.localStartTime;
              this.localStartTime = this.localEndTime;
              this.localEndTime += num5;
              this.serverEndTime = this.serverEndTime.AddSeconds(num5);
              int targetVillageId = this.trader.targetVillageID;
              GameEngine.Instance.getVillage(targetVillageId)?.addResources(this.trader.resource, this.trader.amount);
            }
            else if (this.trader.traderState == 3)
            {
              num4 = 0.0;
              ++this.trader.traderState;
              double num6 = this.localEndTime - this.localStartTime;
              this.localStartTime = this.localEndTime;
              this.localEndTime += num6;
              this.serverEndTime = this.serverEndTime.AddSeconds(num6);
              int homeVillageId = this.trader.homeVillageID;
              if (GameEngine.Instance.World.isUserVillage(homeVillageId))
                GameEngine.Instance.World.addGold((double) this.trader.goldCost);
            }
            else if (this.trader.traderState == 2 || this.trader.traderState == 4)
            {
              this.dying = true;
              num4 = 0.0;
              this.trader.traderState = 0;
              GameEngine.Instance.getVillage(this.trader.homeVillageID)?.addTraders(this.trader.numTraders, this.trader.traderID);
            }
            else if (this.trader.traderState == 5)
            {
              num4 = 0.0;
              ++this.trader.traderState;
              double num7 = this.localEndTime - this.localStartTime;
              this.localStartTime = this.localEndTime;
              this.localEndTime += num7;
              this.serverEndTime = this.serverEndTime.AddSeconds(num7);
              int targetVillageId = this.trader.targetVillageID;
            }
            else if (this.trader.traderState == 6)
            {
              this.dying = true;
              num4 = 0.0;
              this.trader.traderState = 0;
              int homeVillageId = this.trader.homeVillageID;
              VillageMap village = GameEngine.Instance.getVillage(homeVillageId);
              if (village != null)
              {
                village.addResources(this.trader.resource, this.trader.amount);
                village.addTraders(this.trader.numTraders, this.trader.traderID);
              }
            }
          }
          if (num4 < 0.0)
            num4 = 0.0;
          this.lastRatio = num4;
          if (this.trader.traderState == 1 || this.trader.traderState == 3 || this.trader.traderState == 5)
          {
            this.displayX = (this.targetDisplayX - this.baseDisplayX) * num4 + this.baseDisplayX;
            this.displayY = (this.targetDisplayY - this.baseDisplayY) * num4 + this.baseDisplayY;
          }
          else if (this.trader.traderState == 2 || this.trader.traderState == 4 || this.trader.traderState == 6)
          {
            this.displayX = (this.baseDisplayX - this.targetDisplayX) * num4 + this.targetDisplayX;
            this.displayY = (this.baseDisplayY - this.targetDisplayY) * num4 + this.targetDisplayY;
          }
          else
          {
            this.displayX = this.baseDisplayX;
            this.displayY = this.baseDisplayY;
          }
        }
        else
        {
          this.displayX = this.baseDisplayX;
          this.displayY = this.baseDisplayY;
        }
      }
    }

    public class WorldPoint
    {
      public short mx;
      public short my;

      public float x
      {
        get => (float) this.mx;
        set => this.mx = (short) value;
      }

      public float y
      {
        get => (float) this.my;
        set => this.my = (short) value;
      }
    }

    public class Triangle
    {
      public short mx1;
      public short my1;
      public short mx2;
      public short my2;
      public short mx3;
      public short my3;

      public float x1
      {
        get => (float) this.mx1;
        set => this.mx1 = (short) value;
      }

      public float y1
      {
        get => (float) this.my1;
        set => this.my1 = (short) value;
      }

      public float x2
      {
        get => (float) this.mx2;
        set => this.mx2 = (short) value;
      }

      public float y2
      {
        get => (float) this.my2;
        set => this.my2 = (short) value;
      }

      public float x3
      {
        get => (float) this.mx3;
        set => this.mx3 = (short) value;
      }

      public float y3
      {
        get => (float) this.my3;
        set => this.my3 = (short) value;
      }
    }

    public class WorldPointList
    {
      public int parentID = -1;
      public int capitalVillage = -1;
      public int data;
      public int factionID;
      public int userID;
      public bool rebuiltBorderList;
      public int[] borderList;
      public int[] childList;
      public WorldMap.Triangle[] triangleList;
      public WorldMap.WorldPoint[] regionBorderList;
      public Point marker = new Point(-1, -1);
      public int experimentalColourVariant;
      public string areaName = "";
      public int plague;
      public float minX = 1E+08f;
      public float minY = 1E+08f;
      public float maxX = -1E+08f;
      public float maxY = -1E+08f;
      private PointF centroid = PointF.Empty;

      public void updateBounds(WorldMap.WorldPoint wp)
      {
        if ((double) wp.x < (double) this.minX)
          this.minX = wp.x;
        if ((double) wp.y < (double) this.minY)
          this.minY = wp.y;
        if ((double) wp.x > (double) this.maxX)
          this.maxX = wp.x;
        if ((double) wp.y <= (double) this.maxY)
          return;
        this.maxY = wp.y;
      }

      public void updateBoundsFromTriangles()
      {
        foreach (WorldMap.Triangle triangle in this.triangleList)
        {
          if ((double) triangle.x1 < (double) this.minX)
            this.minX = triangle.x1;
          if ((double) triangle.x2 < (double) this.minX)
            this.minX = triangle.x2;
          if ((double) triangle.x3 < (double) this.minX)
            this.minX = triangle.x3;
          if ((double) triangle.y1 < (double) this.minY)
            this.minY = triangle.y1;
          if ((double) triangle.y2 < (double) this.minY)
            this.minY = triangle.y2;
          if ((double) triangle.y3 < (double) this.minY)
            this.minY = triangle.y3;
          if ((double) triangle.x1 > (double) this.maxX)
            this.maxX = triangle.x1;
          if ((double) triangle.x2 > (double) this.maxX)
            this.maxX = triangle.x2;
          if ((double) triangle.x3 > (double) this.maxX)
            this.maxX = triangle.x3;
          if ((double) triangle.y1 > (double) this.maxY)
            this.maxY = triangle.y1;
          if ((double) triangle.y2 > (double) this.maxY)
            this.maxY = triangle.y2;
          if ((double) triangle.y3 > (double) this.maxY)
            this.maxY = triangle.y3;
        }
      }

      public bool PointWithinBounds(WorldMap.WorldPoint candidate)
      {
        return (double) candidate.x >= (double) this.minX && (double) candidate.x <= (double) this.maxX && (double) candidate.y >= (double) this.minY && (double) candidate.y <= (double) this.maxY;
      }

      public bool PointWithinRegion(WorldMap.WorldPoint candidate)
      {
        if (!this.PointWithinBounds(candidate))
          return false;
        int num1 = 0;
        for (int index = 0; index < this.regionBorderList.Length; ++index)
        {
          WorldMap.WorldPoint regionBorder = this.regionBorderList[index];
          WorldMap.WorldPoint worldPoint = index >= this.regionBorderList.Length - 1 ? this.regionBorderList[0] : this.regionBorderList[index + 1];
          float num2 = Math.Min(regionBorder.y, worldPoint.y);
          float num3 = Math.Max(regionBorder.y, worldPoint.y);
          if ((double) candidate.y >= (double) num2 && (double) candidate.y <= (double) num3 && ((double) regionBorder.x >= (double) candidate.x || (double) worldPoint.x >= (double) candidate.x))
          {
            float num4 = (worldPoint.y - regionBorder.y) / worldPoint.x - regionBorder.x;
            float num5 = regionBorder.y - num4 * regionBorder.x;
            if ((double) ((candidate.y - num5) / num4) >= (double) candidate.x)
              ++num1;
          }
        }
        return num1 % 2 == 1;
      }

      public PointF getCentrePoint()
      {
        return new PointF()
        {
          X = (float) (((double) this.maxX - (double) this.minX) / 2.0) + this.minX,
          Y = (float) (((double) this.maxY - (double) this.minY) / 2.0) + this.minY
        };
      }

      public bool isVisible(RectangleF screenRect)
      {
        return (double) this.maxX >= (double) screenRect.Left && (double) this.minX <= (double) screenRect.Right && (double) this.minY <= (double) screenRect.Bottom && (double) this.maxY >= (double) screenRect.Top;
      }
    }

    public class IslandInfoList
    {
      public int villageID = -1;
      public int county = -1;
      public int province = -1;
      public int country = -1;
      public int sx;
      public int sy;
      public int ex;
      public int ey;
    }

    public class VillageRolloverInfo
    {
      public DateTime lastUpdateTime = DateTime.Now.AddYears(-1);
      public DateTime interdictionTime = DateTime.MinValue;
      public DateTime peaceTime = DateTime.MinValue;
      public bool vacationMode;
      public string villageName = "";
      public int villageID = -1;
      public int plagueLevel = -1;
    }

    public class CachedUserInfo
    {
      public DateTime lastUpdateTime = DateTime.Now.AddYears(-1);
      public int userID = -1;
      public string userName = "";
      public double honour = -1.0;
      public double gold = -1.0;
      public int rank;
      public int points;
      public int standing;
      public int numVillages;
      public int factionID = -1;
      public AvatarData avatarData;
      public Bitmap avatarBitmap;
      public int[] villages;
      public List<int> achievements;
      public int numQuests;
      public List<int> completedQuests;
      public bool admin;
      public bool moderator;
      public string stuff = "";

      ~CachedUserInfo()
      {
        if (this.avatarBitmap == null)
          return;
        this.avatarBitmap.Dispose();
      }
    }

    public class FactionPointsComparer : IComparer<FactionData>
    {
      public int Compare(FactionData x, FactionData y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (x.points > y.points)
          return -1;
        return x.points < y.points ? 1 : x.factionName.CompareTo(y.factionName);
      }
    }

    public class SpecialVillageCache
    {
      public DateTime lastUpdate = DateTime.Now;
      public int resourceLevel;
      public int resourceType;
    }

    private class ShieldTextureCacheEntry
    {
      public int index = -1;
      public int playerID = -10000;
      public int textureID = -1;
      public DateTime lastUsage = DateTime.MinValue;
    }

    public struct AvailableCounty
    {
      public int countyID;
      public int available;
      public int total;
      public int maturity;
    }

    public class FastScreenRect
    {
      public int left;
      public int top;
      public int right;
      public int bottom;
      public float Left;
      public float Top;
      public float Width;
      public float Height;
      public double zoomLevel;
    }

    public class MapText
    {
      public string text = "";
      public int size = 1;
      public bool centered;
      public Color col = ARGBColors.Black;
      public PointF loc;
      public bool bordered;
      public WorldMap.MapTextType type = WorldMap.MapTextType.DEBUG;
      public WorldMap.WorldPointList wpl;
      public bool preAdjustedForRetina;
    }

    public enum MapTextType
    {
      PARISH,
      COUNTY,
      PROVINCE,
      COUNTRY,
      DEBUG,
    }

    public class InterVillageLinesStyles
    {
      public const int VASSAL_BLUE = 1;
      public const int VASSAL_GREEN = 2;
      public const int VASSAL_LIGHTGREEN = 3;
      public const int VASSAL_LIGHTRED = 4;
      public const int SELECT_GREEN_YELLOW_TAPER = 5;
      public const int SELECT_GREEN_YELLOW_TAPER_PULSE = 6;
    }

    private class InterVillageLine
    {
      public PointF start;
      public PointF end;
      public int style = 1;
      public float widthScalar = 1.1f;
      public bool minLength = true;
    }

    public delegate void WorldZoomCallback(double newWorldZoom, bool redraw);

    public class LeaderboardSelfRankingsComparer : IComparer<LeaderBoardSelfRankings>
    {
      public int Compare(LeaderBoardSelfRankings y, LeaderBoardSelfRankings x)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (x.value == 0)
          return y.value == 0 ? 0 : -1;
        if (y.value == 0 || x.place < y.place)
          return 1;
        return x.place > y.place ? -1 : 0;
      }
    }

    public class LeaderboardSelfStaticComparer : IComparer<LeaderBoardSelfRankings>
    {
      public int Compare(LeaderBoardSelfRankings y, LeaderBoardSelfRankings x)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null || x.category < y.category)
          return 1;
        return x.category > y.category ? -1 : 0;
      }
    }
  }
}
