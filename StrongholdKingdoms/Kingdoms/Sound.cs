// Decompiled with JetBrains decompiler
// Type: Kingdoms.Sound
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.Collections.Generic;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class Sound
  {
    public const int SFX_PLACE_BUILDING = 10001;
    public const int WORLD_AREA_TYPE_LOWLAND = 0;
    public const int WORLD_AREA_TYPE_UPLAND = 1;
    public const int WORLD_AREA_TYPE_RIVER1 = 2;
    public const int WORLD_AREA_TYPE_RIVER2 = 3;
    public const int WORLD_AREA_TYPE_MOUNTAIN_PEAK = 4;
    public const int WORLD_AREA_TYPE_SALTFLAT = 5;
    public const int WORLD_AREA_TYPE_MARSH = 6;
    public const int WORLD_AREA_TYPE_PLAINS = 7;
    public const int WORLD_AREA_TYPE_VALLEYSIDE = 8;
    public const int WORLD_AREA_TYPE_FOREST = 9;
    public const int WORLD_AREA_TYPE_PARISH_CAPITAL = 10;
    public const int WORLD_AREA_TYPE_COUNTY_CAPITAL = 11;
    public const int WORLD_AREA_TYPE_PROVINCE_CAPITAL = 12;
    public const int WORLD_AREA_TYPE_COUNTRY_CAPITAL = 13;
    public const int WORLD_AREA_TYPE_CAPITAL_QUIET = 14;
    public const int WORLD_AREA_TYPE_CAPITAL_NORMAL = 15;
    public const int WORLD_AREA_TYPE_CAPITAL_BUSY = 16;
    public const int WORLD_AREA_TYPE_CASTLE = 17;
    public const int WORLD_AREA_TYPE_CASTLE_CONSTRUCTION = 18;
    public const int WORLD_AREA_TYPE_WORLD = 19;
    public const int RANK_SOUND_2 = 20;
    public const int RANK_SOUND_3 = 21;
    public const int RANK_SOUND_4 = 22;
    public const int RANK_SOUND_5 = 23;
    public const int RANK_SOUND_6 = 24;
    public const int RANK_SOUND_7 = 25;
    public const int RANK_SOUND_8 = 26;
    public const int RANK_SOUND_9 = 27;
    public const int RANK_SOUND_10 = 28;
    public const int RANK_SOUND_11 = 29;
    public const int RANK_SOUND_12 = 30;
    public const int RANK_SOUND_13 = 31;
    public const int RANK_SOUND_14 = 32;
    public const int RANK_SOUND_15 = 33;
    public const int RANK_SOUND_16 = 34;
    public const int RANK_SOUND_17 = 35;
    public const int RANK_SOUND_18 = 36;
    public const int RANK_SOUND_19 = 37;
    public const int RANK_SOUND_20 = 38;
    public const int RANK_SOUND_21 = 39;
    public const int RANK_SOUND_22 = 40;
    public const int RANK_SOUND_23 = 41;
    public const int WORLD_AREA_TYPE_BATTLE_1 = 42;
    public const int WORLD_AREA_TYPE_BATTLE_2 = 43;
    public const int WORLD_AREA_TYPE_BATTLE_3 = 44;
    public const int WORLD_AREA_TYPE_BATTLE_4 = 45;
    public const int REWARD_JINGLE = 46;
    private static bool musicActive = true;
    private static bool sfxActive = true;
    private static bool BattleSFXActive = true;
    private static bool envActive = true;
    private static Sound.PlayList defaultMusicPlayList = new Sound.PlayList();
    private static Sound.PlayList battleMusicPlayList = new Sound.PlayList();
    private static Sound.PlayList battleEndVictoryMusicPlayList = new Sound.PlayList();
    private static Sound.PlayList battleEndDefeatMusicPlayList = new Sound.PlayList();
    private static Sound.PlayList currentPlayingPlayList = (Sound.PlayList) null;
    private static bool playingBattleMusic = false;
    public static bool musicPaused = false;
    public static bool envPaused = false;
    private static List<Sound.DelayedSound> delayedSounds = new List<Sound.DelayedSound>();
    private static string[] environmentalSounds = new string[47]
    {
      "environment_lowland.mp3",
      "environment_highland.mp3",
      "environment_river1.mp3",
      "environment_river2.mp3",
      "environment_mountainpeak.mp3",
      "environment_saltflat.mp3",
      "environment_marsh.mp3",
      "environment_plains.mp3",
      "environment_valleyside.mp3",
      "environment_forest.mp3",
      "environment_parish.mp3",
      "environment_county.mp3",
      "environment_province.mp3",
      "environment_country.mp3",
      "environment_capital_quiet.mp3",
      "environment_capital_normal.mp3",
      "environment_capital_busy.mp3",
      "environment_castle.mp3",
      "environment_castle_construction.mp3",
      "environment_world.mp3",
      "environment_rank_2.mp3",
      "environment_rank_3.mp3",
      "environment_rank_4.mp3",
      "environment_rank_5.mp3",
      "environment_rank_6.mp3",
      "environment_rank_7.mp3",
      "environment_rank_8.mp3",
      "environment_rank_9.mp3",
      "environment_rank_10.mp3",
      "environment_rank_11.mp3",
      "environment_rank_12.mp3",
      "environment_rank_13.mp3",
      "environment_rank_14.mp3",
      "environment_rank_15.mp3",
      "environment_rank_16.mp3",
      "environment_rank_17.mp3",
      "environment_rank_18.mp3",
      "environment_rank_19.mp3",
      "environment_rank_20.mp3",
      "environment_rank_21.mp3",
      "environment_rank_22.mp3",
      "environment_rank_23.mp3",
      "environment_battle_1.mp3",
      "environment_battle_2.mp3",
      "environment_battle_3.mp3",
      "environment_battle_4.mp3",
      "environment_rank_18_short_4.mp3"
    };
    private static int s_currentVillageEnvironmental = -1;
    private static bool s_loop = true;
    private static bool s_silencedMusic = true;
    private static float s_storedMusicVolume = 1f;
    private static bool s_blockEnvWhilePlaying = false;
    private static bool s_fading = false;
    private static float s_targetFadeVolume = 1f;
    private static float s_currentFadeVolume = 1f;
    private static float s_startFadeVolume = 1f;
    private static DateTime s_startFadeDT = DateTime.MinValue;
    private static float FADE_DURATION = 1f;

    public static void setMusicState(bool active)
    {
      if (active == Sound.musicActive)
        return;
      Sound.musicActive = active;
      if (active)
      {
        if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE && GameEngine.Instance.GameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_BATTLE)
          Sound.playBattleMusic();
        else
          Sound.playMusic();
      }
      else
        Sound.stopMusic();
    }

    public static bool SFXActive
    {
      get
      {
        return GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE && GameEngine.Instance.GameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_BATTLE ? Sound.BattleSFXActive : Sound.sfxActive;
      }
    }

    public static void setSFXState(bool active)
    {
      if (active == Sound.sfxActive)
        return;
      Sound.sfxActive = active;
      if (active)
        return;
      Sound.stopVillageEnvironmentalSFXOnly();
    }

    public static void setBattleSFXState(bool active)
    {
      if (active == Sound.BattleSFXActive)
        return;
      Sound.BattleSFXActive = active;
      if (active)
        return;
      Sound.stopVillageEnvironmentalSFXOnly();
    }

    public static bool EnvironmentActive => Sound.envActive;

    public static void setEnvironmentalState(bool active)
    {
      if (active == Sound.envActive)
        return;
      Sound.envActive = active;
      if (active)
        return;
      Sound.stopVillageEnvironmentalOnly();
    }

    public static void createPlayLists()
    {
      Sound.defaultMusicPlayList.addEntry(new Sound.PlayListEntry(Application.StartupPath + "\\assets\\music\\monks1.mp3", 0.5f, 30, 4));
      Sound.defaultMusicPlayList.addEntry(new Sound.PlayListEntry(Application.StartupPath + "\\assets\\music\\mandloop1.mp3", 0.5f, 30));
      Sound.defaultMusicPlayList.addEntry(new Sound.PlayListEntry(Application.StartupPath + "\\assets\\music\\sadtimesb.mp3", 0.5f, 30));
      Sound.defaultMusicPlayList.addEntry(new Sound.PlayListEntry(Application.StartupPath + "\\assets\\music\\stainedglass-All.mp3", 0.5f, 30));
      Sound.defaultMusicPlayList.addEntry(new Sound.PlayListEntry(Application.StartupPath + "\\assets\\music\\the maidenA.mp3", 0.5f, 30));
      Sound.defaultMusicPlayList.addEntry(new Sound.PlayListEntry(Application.StartupPath + "\\assets\\music\\underanoldtree.mp3", 0.5f, 30));
      Sound.defaultMusicPlayList.addEntry(new Sound.PlayListEntry(Application.StartupPath + "\\assets\\music\\journeys.mp3", 0.5f, 30));
      Sound.defaultMusicPlayList.random = true;
      Sound.battleMusicPlayList.addEntry(new Sound.PlayListEntry(Application.StartupPath + "\\assets\\music\\Battle.mp3", 0.5f, 15));
      Sound.battleMusicPlayList.addEntry(new Sound.PlayListEntry(Application.StartupPath + "\\assets\\music\\glory_03.mp3", 0.5f, 15));
      Sound.battleMusicPlayList.addEntry(new Sound.PlayListEntry(Application.StartupPath + "\\assets\\music\\honor_04.mp3", 0.5f, 15));
      Sound.battleMusicPlayList.random = true;
      Sound.battleEndDefeatMusicPlayList.addEntry(new Sound.PlayListEntry(Application.StartupPath + "\\assets\\music\\battle end defeat.mp3", 0.5f, 86400));
      Sound.battleEndVictoryMusicPlayList.addEntry(new Sound.PlayListEntry(Application.StartupPath + "\\assets\\music\\battle end.mp3", 0.5f, 86400));
      Sound.loadInterfaceSounds();
    }

    public static void playMusic()
    {
      Sound.playPlayList(Sound.defaultMusicPlayList);
      Sound.playingBattleMusic = false;
    }

    public static void playBattleMusic()
    {
      Sound.playPlayList(Sound.battleMusicPlayList);
      Sound.playingBattleMusic = true;
    }

    public static void playBattleEndVictoryMusic()
    {
      Sound.playPlayList(Sound.battleEndVictoryMusicPlayList);
      Sound.playingBattleMusic = true;
    }

    public static void playBattleEndDefeatMusic()
    {
      Sound.playPlayList(Sound.battleEndDefeatMusicPlayList);
      Sound.playingBattleMusic = true;
    }

    private static void playPlayList(Sound.PlayList pl)
    {
      Sound.stopMusic();
      if (!Sound.musicActive)
        return;
      Sound.currentPlayingPlayList = pl;
      Sound.currentPlayingPlayList.play();
      Sound.musicPaused = false;
    }

    public static void stopMusic()
    {
      Sound.playingBattleMusic = false;
      if (Sound.currentPlayingPlayList == null)
        return;
      Sound.currentPlayingPlayList.stop();
      Sound.currentPlayingPlayList = (Sound.PlayList) null;
    }

    public static void pauseMusic()
    {
      if (!Sound.musicActive || Sound.musicPaused)
        return;
      Sound.musicPaused = true;
      GameEngine.Instance.AudioEngine.pauseMp3(0);
    }

    public static void resumeMusic()
    {
      if (!Sound.musicActive || !Sound.musicPaused)
        return;
      Sound.musicPaused = false;
      GameEngine.Instance.AudioEngine.resumeMp3(0);
    }

    public static void monitorMusic()
    {
      if (Sound.musicActive)
      {
        if (Sound.currentPlayingPlayList != null)
          Sound.currentPlayingPlayList.update();
        if (InterfaceMgr.Instance.ParentForm != null)
        {
          if (!InterfaceMgr.Instance.ParentForm.Visible || InterfaceMgr.Instance.ParentForm.WindowState == FormWindowState.Minimized)
            Sound.pauseMusic();
          else
            Sound.resumeMusic();
        }
        if (Sound.playingBattleMusic && (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_CASTLE || GameEngine.Instance.GameDisplayModeSubMode != GameEngine.GameDisplaySubModes.SUBMODE_BATTLE))
          Sound.playMusic();
      }
      if (Sound.EnvironmentActive && InterfaceMgr.Instance.ParentForm != null)
      {
        if (!InterfaceMgr.Instance.ParentForm.Visible || InterfaceMgr.Instance.ParentForm.WindowState == FormWindowState.Minimized)
          Sound.pauseEnv();
        else
          Sound.resumeEnv();
      }
      Sound.monitorEnvironmentals();
      Sound.processDelayedSounds();
    }

    public static void pauseEnv()
    {
      if (!Sound.EnvironmentActive || Sound.envPaused)
        return;
      Sound.envPaused = true;
      GameEngine.Instance.AudioEngine.pauseMp3(2);
      GameEngine.Instance.AudioEngine.pauseMp3(1);
    }

    public static void resumeEnv()
    {
      if (!Sound.EnvironmentActive || !Sound.envPaused)
        return;
      Sound.envPaused = false;
      GameEngine.Instance.AudioEngine.resumeMp3(2);
      GameEngine.Instance.AudioEngine.resumeMp3(1);
    }

    public static void loadInterfaceSounds()
    {
    }

    public static void playInterfaceSound(int sfx)
    {
    }

    public static void playDelayedInterfaceSound(string tag, int delayMS)
    {
      Sound.delayedSounds.Add(new Sound.DelayedSound()
      {
        tag = tag,
        playTime = DateTime.Now.AddMilliseconds((double) delayMS)
      });
    }

    public static void processDelayedSounds()
    {
      if (Sound.delayedSounds.Count <= 0)
        return;
      List<Sound.DelayedSound> delayedSoundList = new List<Sound.DelayedSound>();
      foreach (Sound.DelayedSound delayedSound in Sound.delayedSounds)
      {
        if (delayedSound.playTime < DateTime.Now)
        {
          GameEngine.Instance.playInterfaceSound(delayedSound.tag);
          delayedSoundList.Add(delayedSound);
        }
      }
      if (delayedSoundList.Count <= 0)
        return;
      foreach (Sound.DelayedSound delayedSound in delayedSoundList)
        Sound.delayedSounds.Remove(delayedSound);
    }

    public static void forceFullPlayOfNextEnvironmental() => Sound.s_blockEnvWhilePlaying = true;

    public static void playVillageEnvironmental(int villageType)
    {
      Sound.playVillageEnvironmental(villageType, true, false, false);
    }

    public static void fadeInVillageEnvironmental(int villageType)
    {
      Sound.playVillageEnvironmental(villageType, true, false, true);
    }

    public static void playVillageEnvironmental(int villageType, bool loop, bool silenceMusic)
    {
      Sound.playVillageEnvironmental(villageType, loop, silenceMusic, false);
    }

    public static void playVillageEnvironmental(
      int villageType,
      bool loop,
      bool silenceMusic,
      bool fadeIn)
    {
      if (villageType == Sound.s_currentVillageEnvironmental)
        return;
      if (Sound.s_blockEnvWhilePlaying)
      {
        int channel = 1;
        if (Sound.isEnvAnSFX(Sound.s_currentVillageEnvironmental))
          channel = 2;
        if (GameEngine.Instance.AudioEngine.isMP3Playing(channel))
          return;
        Sound.s_blockEnvWhilePlaying = false;
      }
      Sound.stopVillageEnvironmental();
      if (Sound.isEnvAnSFX(villageType) || villageType >= 42 && villageType <= 45)
      {
        if (!Sound.SFXActive)
          return;
      }
      else if (!Sound.envActive)
        return;
      Sound.s_currentVillageEnvironmental = villageType;
      Sound.s_loop = loop;
      Sound.s_silencedMusic = silenceMusic;
      if (silenceMusic)
      {
        Sound.s_storedMusicVolume = GameEngine.Instance.AudioEngine.getMP3Volume(0);
        GameEngine.Instance.AudioEngine.setMP3MasterVolume(1f / 1000f, 0);
      }
      string str = Application.StartupPath + "\\assets\\SFX\\";
      if (fadeIn)
      {
        Sound.s_fading = true;
        Sound.s_startFadeDT = DateTime.Now;
        Sound.s_currentFadeVolume = Sound.s_startFadeVolume = 0.0f;
        Sound.FADE_DURATION = GameEngine.Instance.AudioEngine.getVolumeFromTag("BattleFadeLength") * 100f;
      }
      if (Sound.isEnvAnSFX(Sound.s_currentVillageEnvironmental))
      {
        float volume = GameEngine.Instance.AudioEngine.getVolumeFromTag("VolumeOnly_rank");
        if (fadeIn)
        {
          Sound.s_targetFadeVolume = volume * GameEngine.Instance.AudioEngine.getMP3Volume(2);
          volume = 0.0f;
        }
        GameEngine.Instance.AudioEngine.playMp3(str + Sound.environmentalSounds[Sound.s_currentVillageEnvironmental], volume, 2);
      }
      else if (Sound.s_currentVillageEnvironmental >= 42 && Sound.s_currentVillageEnvironmental <= 45)
      {
        float volume = GameEngine.Instance.AudioEngine.getVolumeFromTag("VolumeOnly_battle");
        if (fadeIn)
        {
          Sound.s_targetFadeVolume = volume * GameEngine.Instance.AudioEngine.getMP3Volume(2);
          volume = 0.0f;
        }
        GameEngine.Instance.AudioEngine.playMp3(str + Sound.environmentalSounds[Sound.s_currentVillageEnvironmental], volume, 2);
      }
      else
      {
        float volume = GameEngine.Instance.AudioEngine.getVolumeFromTag("VolumeOnly_environment");
        if (fadeIn)
        {
          Sound.s_targetFadeVolume = volume * GameEngine.Instance.AudioEngine.getMP3Volume(1);
          volume = 0.0f;
        }
        GameEngine.Instance.AudioEngine.playMp3(str + Sound.environmentalSounds[Sound.s_currentVillageEnvironmental], volume, 1);
      }
    }

    public static void stopVillageEnvironmental()
    {
      if (Sound.s_currentVillageEnvironmental < 0)
        return;
      Sound.s_blockEnvWhilePlaying = false;
      Sound.s_currentVillageEnvironmental = -1;
      Sound.s_fading = false;
      Sound.restoreSilencedMusic();
      GameEngine.Instance.AudioEngine.stopMp3(1);
      GameEngine.Instance.AudioEngine.stopMp3(2);
    }

    public static void stopVillageEnvironmentalOnly()
    {
      if (Sound.s_currentVillageEnvironmental < 0)
        return;
      Sound.s_blockEnvWhilePlaying = false;
      Sound.s_currentVillageEnvironmental = -1;
      Sound.s_fading = false;
      Sound.restoreSilencedMusic();
      GameEngine.Instance.AudioEngine.stopMp3(1);
    }

    public static void stopVillageEnvironmentalSFXOnly()
    {
      if (Sound.s_currentVillageEnvironmental < 0)
        return;
      Sound.s_blockEnvWhilePlaying = false;
      Sound.s_currentVillageEnvironmental = -1;
      Sound.s_fading = false;
      Sound.restoreSilencedMusic();
      GameEngine.Instance.AudioEngine.stopMp3(2);
    }

    public static void stopVillageEnvironmentalExceptWorld()
    {
      if (Sound.s_currentVillageEnvironmental < 0 || Sound.s_currentVillageEnvironmental == 19)
        return;
      Sound.s_blockEnvWhilePlaying = false;
      Sound.s_currentVillageEnvironmental = -1;
      Sound.s_fading = false;
      Sound.restoreSilencedMusic();
      GameEngine.Instance.AudioEngine.stopMp3(1);
      GameEngine.Instance.AudioEngine.stopMp3(2);
    }

    public static void restoreSilencedMusic()
    {
      if (!Sound.s_silencedMusic)
        return;
      Sound.s_silencedMusic = false;
      GameEngine.Instance.AudioEngine.setMP3MasterVolume(Sound.s_storedMusicVolume, 0);
    }

    public static bool isEnvAnSFX(int env) => env >= 20 && env <= 41 || env == 46;

    public static bool isPlayingEnvironmental(int soundID)
    {
      return soundID == Sound.s_currentVillageEnvironmental;
    }

    public static void monitorEnvironmentals()
    {
      if (Sound.s_currentVillageEnvironmental < 0)
        return;
      int channel = 1;
      if (Sound.isEnvAnSFX(Sound.s_currentVillageEnvironmental))
        channel = 2;
      if (Sound.s_currentVillageEnvironmental >= 42 && Sound.s_currentVillageEnvironmental <= 45)
      {
        channel = 2;
        if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_CASTLE || GameEngine.Instance.GameDisplayModeSubMode != GameEngine.GameDisplaySubModes.SUBMODE_BATTLE)
        {
          Sound.stopVillageEnvironmental();
          return;
        }
      }
      if (!GameEngine.Instance.AudioEngine.isMP3Playing(channel) && !GameEngine.Instance.AudioEngine.isMP3Paused(channel))
      {
        if (Sound.s_loop)
        {
          string str = Application.StartupPath + "\\assets\\SFX\\";
          if (Sound.isEnvAnSFX(Sound.s_currentVillageEnvironmental))
          {
            float volumeFromTag = GameEngine.Instance.AudioEngine.getVolumeFromTag("VolumeOnly_rank");
            GameEngine.Instance.AudioEngine.playMp3(str + Sound.environmentalSounds[Sound.s_currentVillageEnvironmental], volumeFromTag, 2);
          }
          else if (Sound.s_currentVillageEnvironmental >= 42 && Sound.s_currentVillageEnvironmental <= 45)
          {
            float volumeFromTag = GameEngine.Instance.AudioEngine.getVolumeFromTag("VolumeOnly_battle");
            GameEngine.Instance.AudioEngine.playMp3(str + Sound.environmentalSounds[Sound.s_currentVillageEnvironmental], volumeFromTag, 2);
          }
          else
          {
            float volumeFromTag = GameEngine.Instance.AudioEngine.getVolumeFromTag("VolumeOnly_environment");
            GameEngine.Instance.AudioEngine.playMp3(str + Sound.environmentalSounds[Sound.s_currentVillageEnvironmental], volumeFromTag, 1);
          }
        }
        else
        {
          Sound.stopVillageEnvironmental();
          Sound.s_fading = false;
          Sound.restoreSilencedMusic();
        }
      }
      if (!Sound.s_fading)
        return;
      TimeSpan timeSpan = DateTime.Now - Sound.s_startFadeDT;
      if (timeSpan.TotalSeconds > (double) Sound.FADE_DURATION)
      {
        if ((double) Sound.s_targetFadeVolume == 0.0)
        {
          Sound.stopVillageEnvironmental();
        }
        else
        {
          Sound.s_fading = false;
          Sound.s_currentFadeVolume = Sound.s_targetFadeVolume;
          if (Sound.isEnvAnSFX(Sound.s_currentVillageEnvironmental) || Sound.s_currentVillageEnvironmental >= 42 && Sound.s_currentVillageEnvironmental <= 45)
            GameEngine.Instance.AudioEngine.setCurrentMP3Volume(Sound.s_currentFadeVolume, 2);
          else
            GameEngine.Instance.AudioEngine.setCurrentMP3Volume(Sound.s_currentFadeVolume, 1);
        }
      }
      else
      {
        float num = (float) timeSpan.TotalSeconds / Sound.FADE_DURATION;
        Sound.s_currentFadeVolume = (Sound.s_targetFadeVolume - Sound.s_startFadeVolume) * num + Sound.s_startFadeVolume;
        if (Sound.isEnvAnSFX(Sound.s_currentVillageEnvironmental) || Sound.s_currentVillageEnvironmental >= 42 && Sound.s_currentVillageEnvironmental <= 45)
          GameEngine.Instance.AudioEngine.setCurrentMP3Volume(Sound.s_currentFadeVolume, 2);
        else
          GameEngine.Instance.AudioEngine.setCurrentMP3Volume(Sound.s_currentFadeVolume, 1);
      }
    }

    public static void pauseEnvironmental(bool pause)
    {
      if (Sound.s_currentVillageEnvironmental < 0)
        return;
      if (Sound.isEnvAnSFX(Sound.s_currentVillageEnvironmental) || Sound.s_currentVillageEnvironmental >= 42 && Sound.s_currentVillageEnvironmental <= 45)
      {
        if (pause)
          GameEngine.Instance.AudioEngine.pauseMp3(2);
        else
          GameEngine.Instance.AudioEngine.resumeMp3(2);
      }
      else if (pause)
        GameEngine.Instance.AudioEngine.pauseMp3(1);
      else
        GameEngine.Instance.AudioEngine.resumeMp3(1);
    }

    public static int getCurrentEnvironmental() => Sound.s_currentVillageEnvironmental;

    public static bool isFading() => Sound.s_fading;

    public static void fadeOutCurrentPlaying()
    {
      if (Sound.s_fading)
        return;
      Sound.s_fading = true;
      Sound.s_targetFadeVolume = 0.0f;
      Sound.s_startFadeDT = DateTime.Now;
      Sound.FADE_DURATION = GameEngine.Instance.AudioEngine.getVolumeFromTag("BattleFadeLength") * 100f;
      if (Sound.isEnvAnSFX(Sound.s_currentVillageEnvironmental) || Sound.s_currentVillageEnvironmental >= 42 && Sound.s_currentVillageEnvironmental <= 45)
        Sound.s_currentFadeVolume = Sound.s_startFadeVolume = GameEngine.Instance.AudioEngine.getCurrentMP3Volume(2);
      else
        Sound.s_currentFadeVolume = Sound.s_startFadeVolume = GameEngine.Instance.AudioEngine.getCurrentMP3Volume(1);
    }

    public class PlayListEntry
    {
      public string filename = "";
      public float volume = 1f;
      public int trailingSilenceSeconds;
      public int numLoops;

      public PlayListEntry(string fn, float v, int silence)
      {
        this.filename = fn;
        this.volume = v;
        this.trailingSilenceSeconds = silence;
        this.numLoops = 0;
      }

      public PlayListEntry(string fn, float v, int silence, int loops)
      {
        this.filename = fn;
        this.volume = v;
        this.trailingSilenceSeconds = silence;
        this.numLoops = loops;
      }
    }

    public class PlayList
    {
      public bool playing;
      public List<Sound.PlayListEntry> entries = new List<Sound.PlayListEntry>();
      public int nextStep;
      public int currentStep = -1;
      public bool inSilence;
      public DateTime silenceEnd = DateTime.MinValue;
      public bool random;
      public int currentLoop;

      public void restart()
      {
        this.currentStep = -1;
        this.nextStep = 0;
        this.inSilence = false;
        this.silenceEnd = DateTime.MinValue;
        this.currentLoop = 0;
      }

      public void play()
      {
        this.restart();
        this.advance();
      }

      public void stop() => GameEngine.Instance.AudioEngine.stopMp3(0);

      public void update()
      {
        if (this.inSilence)
        {
          if (!(DateTime.Now > this.silenceEnd) || Sound.musicPaused)
            return;
          this.inSilence = false;
          this.advance();
        }
        else
        {
          if (GameEngine.Instance.AudioEngine.isMP3Playing(0))
            return;
          if (this.entries[this.currentStep].numLoops <= this.currentLoop)
          {
            if (this.entries[this.currentStep].trailingSilenceSeconds > 0)
            {
              this.inSilence = true;
              this.silenceEnd = DateTime.Now.AddSeconds((double) this.entries[this.currentStep].trailingSilenceSeconds);
            }
            else
              this.advance();
          }
          else
          {
            ++this.currentLoop;
            GameEngine.Instance.AudioEngine.playMp3(this.entries[this.currentStep].filename, this.entries[this.currentStep].volume, 0);
          }
        }
      }

      public void advance()
      {
        if (!this.random)
        {
          GameEngine.Instance.AudioEngine.playMp3(this.entries[this.nextStep].filename, this.entries[this.nextStep].volume, 0);
          this.currentStep = this.nextStep;
          ++this.nextStep;
          if (this.nextStep >= this.entries.Count)
            this.nextStep = 0;
        }
        else
        {
          this.nextStep = this.currentStep = new Random().Next(this.entries.Count);
          GameEngine.Instance.AudioEngine.playMp3(this.entries[this.nextStep].filename, this.entries[this.nextStep].volume, 0);
        }
        this.currentLoop = 0;
      }

      public void addEntry(Sound.PlayListEntry entry)
      {
        if (entry == null)
          return;
        this.entries.Add(entry);
      }
    }

    private class DelayedSound
    {
      public string tag;
      public DateTime playTime = DateTime.MaxValue;
    }
  }
}
