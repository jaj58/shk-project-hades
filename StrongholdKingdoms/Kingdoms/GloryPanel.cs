// Decompiled with JetBrains decompiler
// Type: Kingdoms.GloryPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class GloryPanel : CustomSelfDrawPanel, IDockableControl
  {
    private const int VERT_XPOS = 65;
    private const int VERT_XPOS2 = 59;
    private const int VERT_XPOS3 = 59;
    private const int NUM_BANDS = 15;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea viewableArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLine scaleVertLine = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine scaleMark0Line = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine scaleMark100Line = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine scaleMark1000Line = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine scaleMark10000Line = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine scaleMark100000Line = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine scaleMark1000000Line = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine topLine = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine bottomLine = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine scaleVertLineS = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine scaleMark0LineS = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine scaleMark100LineS = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine scaleMark1000LineS = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine scaleMark10000LineS = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine scaleMark100000LineS = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine scaleMark1000000LineS = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine topLineS = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine bottomLineS = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLabel mark0Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel mark100Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel mark1000Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel mark10000Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel mark100000Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel mark1000000Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage flagImage0 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage6 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage7 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage8 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage9 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage10 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage11 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage12 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage13 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage14 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage15 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage16 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage17 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage18 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImage19 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage0 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage6 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage7 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage8 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage9 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage10 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage11 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage12 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage13 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage14 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage15 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage16 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage17 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage18 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagpoleImage19 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag0ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag0ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag0ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag0ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag0ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag1ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag1ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag1ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag1ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag1ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag2ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag2ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag2ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag2ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag2ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag3ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag3ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag3ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag3ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag3ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag4ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag4ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag4ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag4ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag4ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag5ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag5ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag5ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag5ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag5ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag6ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag6ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag6ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag6ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag6ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag7ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag7ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag7ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag7ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag7ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag8ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag8ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag8ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag8ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag8ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag9ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag9ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag9ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag9ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag9ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag10ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag10ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag10ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag10ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag10ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag11ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag11ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag11ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag11ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag11ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag12ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag12ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag12ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag12ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag12ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag13ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag13ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag13ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag13ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag13ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag14ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag14ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag14ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag14ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag14ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag15ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag15ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag15ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag15ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag15ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag16ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag16ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag16ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag16ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag16ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag17ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag17ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag17ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag17ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag17ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag18ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag18ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag18ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag18ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag18ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag19ImageStar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag19ImageStar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag19ImageStar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag19ImageStar4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flag19ImageStar5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton gloryWinnerButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton playbackCountryButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton playbackProvinceButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton gloryValuesButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton endOfTheWorldButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel gloryRoundEnding = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage eraStar = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel eraStarText = new CustomSelfDrawPanel.CSDLabel();
    private int[] markPositionPercents = new int[6]
    {
      0,
      10,
      15,
      20,
      25,
      30
    };
    private int bandYPos0;
    private int bandYPos100;
    private int bandYPos1000;
    private int bandYPos10000;
    private int bandYPos100000;
    private int bandYPos1000000;
    private bool awaitingPlayback;
    private bool playbackIsCountry;
    private int[,] lastHousePoints = new int[20, 2];
    private int[] starSteps = new int[5]{ 0, -1, 1, -2, 2 };
    private bool secondAge;
    private bool thirdAge;
    private bool sixthAge;
    private int[] bands = new int[60]
    {
      0,
      0,
      1,
      19,
      0,
      0,
      2,
      18,
      0,
      1,
      1,
      18,
      0,
      1,
      2,
      17,
      0,
      1,
      3,
      16,
      0,
      1,
      4,
      15,
      0,
      1,
      5,
      14,
      0,
      1,
      6,
      13,
      0,
      2,
      6,
      12,
      1,
      2,
      6,
      11,
      1,
      3,
      6,
      10,
      1,
      4,
      6,
      9,
      1,
      5,
      6,
      8,
      1,
      6,
      6,
      7,
      2,
      6,
      6,
      6
    };
    private int[][] filledBands = new int[15][];
    private bool bandsMade;
    private int[] readOrder = new int[20]
    {
      18,
      16,
      14,
      12,
      10,
      8,
      6,
      4,
      2,
      0,
      1,
      3,
      5,
      7,
      9,
      11,
      13,
      15,
      17,
      19
    };
    private bool bAwaitingStats;
    private DockableControl dockableControl;
    private IContainer components;

    public GloryPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init()
    {
      this.secondAge = GameEngine.Instance.World.SecondAgeWorld;
      this.thirdAge = GameEngine.Instance.World.ThirdAgeWorld;
      this.sixthAge = GameEngine.Instance.World.SixthAgeWorld;
      int num1 = 100;
      int num2 = 1000;
      int num3 = 10000;
      int num4 = 100000;
      int num5 = 1000000;
      if (GameEngine.Instance.LocalWorldData.AIWorld)
      {
        num1 = 1000;
        num2 = 10000;
        num3 = 100000;
        num4 = 1000000;
        num5 = GameEngine.Instance.World.aiWorldGloryWinLevel;
      }
      else if (!GameEngine.Instance.LocalWorldData.EraWorld)
      {
        if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
        {
          num1 = 1000;
          num2 = 10000;
          num3 = 100000;
          num4 = 1000000;
          num5 = 50000000;
        }
        else if (this.thirdAge && !this.sixthAge)
        {
          num1 = 1000;
          num2 = 10000;
          num3 = 100000;
          num4 = 1000000;
          num5 = 4000000;
        }
        if (GameEngine.Instance.World.GetGlobalWorldID() >= 1200 && GameEngine.Instance.World.GetGlobalWorldID() <= 1201 && !this.secondAge)
        {
          if (!this.thirdAge || this.sixthAge)
          {
            num1 = 1000;
            num2 = 10000;
            num3 = 100000;
            num4 = 1000000;
            num5 = 5000000;
          }
          else
          {
            num1 = 1000;
            num2 = 10000;
            num3 = 100000;
            num4 = 1000000;
            num5 = 20000000;
          }
        }
      }
      else if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
      {
        num1 = 1000;
        num2 = 10000;
        num3 = 100000;
        num4 = 1000000;
        num5 = 50000000;
      }
      else
      {
        int age = 0;
        if (GameEngine.Instance.World.SecondAgeWorld)
          ++age;
        if (GameEngine.Instance.World.ThirdAgeWorld)
          ++age;
        if (GameEngine.Instance.World.FourthAgeWorld)
          ++age;
        if (GameEngine.Instance.World.FifthAgeWorld)
          ++age;
        if (GameEngine.Instance.World.SixthAgeWorld)
          ++age;
        if (GameEngine.Instance.World.SeventhAgeWorld)
          ++age;
        bool highCountryCount = false;
        int globalWorldId = GameEngine.Instance.World.GetGlobalWorldID();
        if (globalWorldId >= 1200 && globalWorldId <= 1299)
          highCountryCount = true;
        else if (globalWorldId >= 700 && globalWorldId <= 799)
          highCountryCount = true;
        num5 = VillageBuildingsData.getEraWorldGloryLimits(age, highCountryCount, globalWorldId);
        if (num5 <= 100000)
        {
          num1 = 10;
          num2 = 100;
          num3 = 1000;
          num4 = 10000;
        }
      }
      if (GameEngine.Instance.World.testGloryPointsUpdate())
      {
        RemoteServices.Instance.set_GetHouseGloryPoints_UserCallBack(new RemoteServices.GetHouseGloryPoints_UserCallBack(this.GetHouseGloryPointsCallBack));
        RemoteServices.Instance.GetHouseGloryPoints();
      }
      int num6 = 0;
      int num7 = 0;
      for (int index = 0; index < 20; ++index)
      {
        if (!GameEngine.Instance.World.HouseInfo[index + 1].loser)
        {
          this.lastHousePoints[index, 0] = GameEngine.Instance.World.HouseGloryPoints[index + 1];
          if (this.lastHousePoints[index, 0] > num6)
            num6 = this.lastHousePoints[index, 0];
        }
        else
        {
          this.lastHousePoints[index, 0] = -1;
          ++num7;
        }
        this.lastHousePoints[index, 1] = index;
      }
      for (int index1 = 0; index1 < 19; ++index1)
      {
        for (int index2 = 0; index2 < 19; ++index2)
        {
          if (this.lastHousePoints[index2, 0] < this.lastHousePoints[index2 + 1, 0])
          {
            int lastHousePoint1 = this.lastHousePoints[index2, 0];
            this.lastHousePoints[index2, 0] = this.lastHousePoints[index2 + 1, 0];
            this.lastHousePoints[index2 + 1, 0] = lastHousePoint1;
            int lastHousePoint2 = this.lastHousePoints[index2, 1];
            this.lastHousePoints[index2, 1] = this.lastHousePoints[index2 + 1, 1];
            this.lastHousePoints[index2 + 1, 1] = lastHousePoint2;
          }
        }
      }
      this.clearControls();
      this.mainBackgroundImage.Image = (Image) GFXLibrary.glory_background;
      this.mainBackgroundImage.Width = 1600;
      this.mainBackgroundImage.Height = 1024;
      int width = this.Width;
      int height = this.Height;
      this.mainBackgroundImage.Position = new Point((width - 1600) / 2, -(1024 - height));
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.viewableArea.Position = new Point((1600 - width) / 2, 1024 - height);
      this.viewableArea.Size = new Size(this.Size.Width, this.Size.Height - 50);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.viewableArea);
      int y1 = 25;
      int num8 = height - 50;
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.viewableArea, 22, new Point(this.Width - 38, 26));
      if (GameEngine.Instance.World.HouseGloryRoundData != null && GameEngine.Instance.World.HouseGloryRoundData.winnerHouseID > 0)
      {
        this.gloryWinnerButton.Position = new Point(this.viewableArea.Width - 200 - 15 - 30, y1);
        this.gloryWinnerButton.Size = new Size(200, 38);
        this.gloryWinnerButton.Text.Text = SK.Text("TEMP_ViewWinner", "Last Glory Round Result");
        this.gloryWinnerButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.gloryWinnerButton.Text.Font = !(Program.mySettings.LanguageIdent == "it") ? FontManager.GetFont("Arial", 10f, FontStyle.Bold) : FontManager.GetFont("Arial", 8f, FontStyle.Bold);
        this.gloryWinnerButton.TextYOffset = -1;
        this.gloryWinnerButton.Text.Color = ARGBColors.Black;
        this.gloryWinnerButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.gloryWinnerClick), "Glory_view_result");
        this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.gloryWinnerButton);
        this.gloryWinnerButton.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
        this.gloryWinnerButton.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      }
      int num9 = 0;
      if (GameEngine.Instance.World.SeventhAgeWorld && !GameEngine.Instance.LocalWorldData.AIWorld)
      {
        this.endOfTheWorldButton.Position = new Point(77, y1 + 5);
        this.endOfTheWorldButton.ImageNorm = (Image) GFXLibrary.eow_toggle[3];
        this.endOfTheWorldButton.ImageOver = (Image) GFXLibrary.eow_toggle[4];
        this.endOfTheWorldButton.ImageClick = (Image) GFXLibrary.eow_toggle[5];
        this.endOfTheWorldButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.viewEndWorldPanelClick), "Glory_view_result");
        this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.endOfTheWorldButton);
        num9 = this.endOfTheWorldButton.ImageNorm.Size.Height;
      }
      if (GameEngine.Instance.LocalWorldData.EraWorld && GameEngine.Instance.World.HouseGloryPoints.Length > 21)
      {
        this.eraStarText.Text = (5 - GameEngine.Instance.World.HouseGloryPoints[21]).ToString() + " / 5";
        this.eraStarText.Color = ARGBColors.White;
        this.eraStarText.DropShadowColor = ARGBColors.Black;
        this.eraStarText.Position = new Point(59, y1 + 5 + num9);
        this.eraStarText.Size = new Size(50, 20);
        this.eraStarText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
        this.eraStarText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
        this.eraStarText.CustomTooltipID = 1730;
        this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.eraStarText);
        this.eraStar.Image = (Image) GFXLibrary.glory_star_large;
        this.eraStar.Position = new Point(114, y1 + 5 + num9);
        this.eraStar.CustomTooltipID = 1730;
        this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.eraStar);
      }
      if (!GameEngine.Instance.World.gotPlaybackData())
        this.retrieveGameStats();
      this.playbackCountryButton.Position = new Point(this.viewableArea.Width - 200 - 15 - 30, y1 + 40);
      this.playbackCountryButton.Size = new Size(200, 38);
      this.playbackCountryButton.Text.Text = SK.Text("GLORY_PLAY_COUNTRY", "Show Country History");
      this.playbackCountryButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.playbackCountryButton.Text.Font = !(Program.mySettings.LanguageIdent == "it") ? FontManager.GetFont("Arial", 10f, FontStyle.Bold) : FontManager.GetFont("Arial", 8f, FontStyle.Bold);
      this.playbackCountryButton.TextYOffset = -1;
      this.playbackCountryButton.Text.Color = ARGBColors.Black;
      this.playbackCountryButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playbackCountryClick), "playback_country");
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.playbackCountryButton);
      this.playbackCountryButton.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
      this.playbackCountryButton.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      this.playbackProvinceButton.Position = new Point(this.viewableArea.Width - 200 - 15 - 30, y1 + 82);
      this.playbackProvinceButton.Size = new Size(200, 38);
      this.playbackProvinceButton.Text.Text = SK.Text("GLORY_PLAY_PROVINCE", "Show Province History");
      this.playbackProvinceButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.playbackProvinceButton.Text.Font = !(Program.mySettings.LanguageIdent == "it") ? FontManager.GetFont("Arial", 10f, FontStyle.Bold) : FontManager.GetFont("Arial", 8f, FontStyle.Bold);
      this.playbackProvinceButton.TextYOffset = -1;
      this.playbackProvinceButton.Text.Color = ARGBColors.Black;
      this.playbackProvinceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playbackProvinceClick), "playback_province");
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.playbackProvinceButton);
      this.playbackProvinceButton.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
      this.playbackProvinceButton.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      this.gloryValuesButton.Position = new Point(this.viewableArea.Width - 200 - 15 - 30, y1 + 82 + 42);
      this.gloryValuesButton.Size = new Size(200, 38);
      this.gloryValuesButton.Text.Text = SK.Text("GLORY_VALUES", "Glory Values");
      this.gloryValuesButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.gloryValuesButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.gloryValuesButton.TextYOffset = -1;
      this.gloryValuesButton.Text.Color = ARGBColors.Black;
      this.gloryValuesButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.gloryValuesClick), "playback_province");
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.gloryValuesButton);
      this.gloryValuesButton.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
      this.gloryValuesButton.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      if (num6 >= num5)
      {
        DateTime currentServerTime = VillageMap.getCurrentServerTime();
        DateTime dateTime = new DateTime(currentServerTime.Year, currentServerTime.Month, currentServerTime.Day, 8, 0, 0);
        if (currentServerTime > dateTime)
          dateTime = dateTime.AddDays(1.0);
        int totalHours = (int) (dateTime - currentServerTime).TotalHours;
        string str = SK.Text("GloryPanel_EndingSoon", "Glory Round Ending Soon");
        if (totalHours > 1)
          str = str + " ( " + SK.Text("GloryPanel_Approximately", "Approximately") + " : " + totalHours.ToString() + " " + SK.Text("Reports_Hours", "hours") + " )";
        this.gloryRoundEnding.Text = str;
        this.gloryRoundEnding.Color = ARGBColors.White;
        this.gloryRoundEnding.DropShadowColor = ARGBColors.Black;
        this.gloryRoundEnding.Position = new Point(100, 2);
        this.gloryRoundEnding.Size = new Size(700, 20);
        this.gloryRoundEnding.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
        this.gloryRoundEnding.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_LEFT;
        this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.gloryRoundEnding);
      }
      this.scaleVertLine.Position = new Point(65, y1);
      this.scaleVertLine.Size = new Size(0, num8 - y1);
      this.scaleVertLine.LineColor = ARGBColors.Black;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.scaleVertLine);
      this.scaleMark0LineS.Position = new Point(59, num8 - 1);
      this.scaleMark0LineS.Size = new Size(6, 0);
      this.scaleMark0LineS.LineColor = ARGBColors.Black;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.scaleMark0LineS);
      int num10 = (num8 - y1) * this.markPositionPercents[1] / 100;
      this.scaleMark100Line.Position = new Point(59, num8 - num10);
      this.scaleMark100Line.Size = new Size(6, 0);
      this.scaleMark100Line.LineColor = ARGBColors.Black;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.scaleMark100Line);
      int num11 = (num8 - y1) * (this.markPositionPercents[1] + this.markPositionPercents[2]) / 100;
      this.scaleMark1000Line.Position = new Point(59, num8 - num11);
      this.scaleMark1000Line.Size = new Size(6, 0);
      this.scaleMark1000Line.LineColor = ARGBColors.Black;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.scaleMark1000Line);
      int num12 = (num8 - y1) * (this.markPositionPercents[1] + this.markPositionPercents[2] + this.markPositionPercents[3]) / 100;
      this.scaleMark10000Line.Position = new Point(59, num8 - num12);
      this.scaleMark10000Line.Size = new Size(6, 0);
      this.scaleMark10000Line.LineColor = ARGBColors.Black;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.scaleMark10000Line);
      int num13 = (num8 - y1) * (this.markPositionPercents[1] + this.markPositionPercents[2] + this.markPositionPercents[3] + this.markPositionPercents[4]) / 100;
      this.scaleMark100000Line.Position = new Point(59, num8 - num13);
      this.scaleMark100000Line.Size = new Size(6, 0);
      this.scaleMark100000Line.LineColor = ARGBColors.Black;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.scaleMark100000Line);
      int num14 = num8 - y1;
      this.scaleMark1000000Line.Position = new Point(59, num8 - num14);
      this.scaleMark1000000Line.Size = new Size(6, 0);
      this.scaleMark1000000Line.LineColor = ARGBColors.Black;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.scaleMark1000000Line);
      this.scaleVertLineS.Position = new Point(64, y1 - 1);
      this.scaleVertLineS.Size = new Size(0, num8 - y1);
      this.scaleVertLineS.LineColor = ARGBColors.White;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.scaleVertLineS);
      this.topLineS.Position = new Point(66, y1 - 1);
      this.topLineS.Size = new Size(this.Width - 65 - 1, 0);
      this.topLineS.LineColor = ARGBColors.Yellow;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.topLineS);
      this.bottomLineS.Position = new Point(66, num8 - 1);
      this.bottomLineS.Size = new Size(this.Width - 65 - 1, 0);
      this.bottomLineS.LineColor = ARGBColors.Yellow;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.bottomLineS);
      this.scaleMark0Line.Position = new Point(58, num8 - 1 - 1);
      this.scaleMark0Line.Size = new Size(6, 0);
      this.scaleMark0Line.LineColor = ARGBColors.White;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.scaleMark0Line);
      this.bandYPos0 = num8;
      this.mark0Label.Text = "0";
      this.mark0Label.Color = ARGBColors.White;
      this.mark0Label.DropShadowColor = ARGBColors.Black;
      this.mark0Label.Position = new Point(0, num8 - 20 + 2);
      this.mark0Label.Size = new Size(59, 20);
      this.mark0Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.mark0Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.mark0Label);
      int num15 = (num8 - y1) * this.markPositionPercents[1] / 100;
      this.bandYPos100 = this.bandYPos0 - num15;
      this.scaleMark100LineS.Position = new Point(58, num8 - num15 - 1);
      this.scaleMark100LineS.Size = new Size(6, 0);
      this.scaleMark100LineS.LineColor = ARGBColors.White;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.scaleMark100LineS);
      this.mark100Label.Text = num1.ToString();
      this.mark100Label.Color = ARGBColors.White;
      this.mark100Label.DropShadowColor = ARGBColors.Black;
      this.mark100Label.Position = new Point(0, num8 - num15 - 9);
      this.mark100Label.Size = new Size(59, 20);
      this.mark100Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.mark100Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.mark100Label);
      int num16 = (num8 - y1) * (this.markPositionPercents[1] + this.markPositionPercents[2]) / 100;
      this.bandYPos1000 = this.bandYPos0 - num16;
      this.scaleMark1000LineS.Position = new Point(58, num8 - num16 - 1);
      this.scaleMark1000LineS.Size = new Size(6, 0);
      this.scaleMark1000LineS.LineColor = ARGBColors.White;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.scaleMark1000LineS);
      this.mark1000Label.Text = num2.ToString();
      this.mark1000Label.Color = ARGBColors.White;
      this.mark1000Label.DropShadowColor = ARGBColors.Black;
      this.mark1000Label.Position = new Point(0, num8 - num16 - 9);
      this.mark1000Label.Size = new Size(59, 20);
      this.mark1000Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.mark1000Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.mark1000Label);
      int num17 = (num8 - y1) * (this.markPositionPercents[1] + this.markPositionPercents[2] + this.markPositionPercents[3]) / 100;
      this.bandYPos10000 = this.bandYPos0 - num17;
      this.scaleMark10000LineS.Position = new Point(58, num8 - num17 - 1);
      this.scaleMark10000LineS.Size = new Size(6, 0);
      this.scaleMark10000LineS.LineColor = ARGBColors.White;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.scaleMark10000LineS);
      this.mark10000Label.Text = num3.ToString();
      this.mark10000Label.Color = ARGBColors.White;
      this.mark10000Label.DropShadowColor = ARGBColors.Black;
      this.mark10000Label.Position = new Point(0, num8 - num17 - 9);
      this.mark10000Label.Size = new Size(59, 20);
      this.mark10000Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.mark10000Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.mark10000Label);
      int num18 = (num8 - y1) * (this.markPositionPercents[1] + this.markPositionPercents[2] + this.markPositionPercents[3] + this.markPositionPercents[4]) / 100;
      this.bandYPos100000 = this.bandYPos0 - num18;
      this.scaleMark100000LineS.Position = new Point(58, num8 - num18 - 1);
      this.scaleMark100000LineS.Size = new Size(6, 0);
      this.scaleMark100000LineS.LineColor = ARGBColors.White;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.scaleMark100000LineS);
      this.mark100000Label.Text = num4.ToString();
      this.mark100000Label.Color = ARGBColors.White;
      this.mark100000Label.DropShadowColor = ARGBColors.Black;
      this.mark100000Label.Position = new Point(0, num8 - num18 - 9);
      this.mark100000Label.Size = new Size(59, 20);
      this.mark100000Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.mark100000Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.mark100000Label);
      int num19 = num8 - y1;
      this.bandYPos1000000 = this.bandYPos0 - num19;
      this.scaleMark1000000LineS.Position = new Point(58, num8 - num19 - 1);
      this.scaleMark1000000LineS.Size = new Size(6, 0);
      this.scaleMark1000000LineS.LineColor = ARGBColors.White;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.scaleMark1000000LineS);
      this.mark1000000Label.Text = num5.ToString();
      this.mark1000000Label.Color = ARGBColors.White;
      this.mark1000000Label.DropShadowColor = ARGBColors.Black;
      if (num5 >= 10000000)
      {
        this.mark1000000Label.Position = new Point(-11, num8 - num19 - 9);
        this.mark1000000Label.Size = new Size(69, 20);
      }
      else
      {
        this.mark1000000Label.Position = new Point(0, num8 - num19 - 9);
        this.mark1000000Label.Size = new Size(59, 20);
      }
      this.mark1000000Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.mark1000000Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
      this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) this.mark1000000Label);
      int num20 = this.Width - 65 - 5 - 5 + num7 * 45;
      this.makeSizes();
      int index3 = 14;
      for (int index4 = 0; index4 < 15; ++index4)
      {
        if (num20 < this.filledBands[index4][20])
        {
          index3 = index4 - 1;
          break;
        }
      }
      int x1 = 70;
      int num21 = num7 / 2;
      int num22 = 20 - (num7 - num21);
      for (int flag = num21; flag < num22; ++flag)
      {
        int houseID = this.lastHousePoints[this.readOrder[flag], 1] + 1;
        int lastHousePoint = this.lastHousePoints[this.readOrder[flag], 0];
        int num23 = lastHousePoint;
        int y2 = lastHousePoint >= num1 ? (lastHousePoint >= num2 ? (lastHousePoint >= num3 ? (lastHousePoint >= num4 ? (lastHousePoint >= num5 ? this.bandYPos1000000 : this.bandYPos100000 - (lastHousePoint - num4) * (this.bandYPos100000 - this.bandYPos1000000) / (num5 - num4)) : this.bandYPos10000 - (lastHousePoint - num3) * (this.bandYPos10000 - this.bandYPos100000) / (num4 - num3)) : this.bandYPos1000 - (lastHousePoint - num2) * (this.bandYPos1000 - this.bandYPos10000) / (num3 - num2)) : this.bandYPos100 - (lastHousePoint - num1) * (this.bandYPos100 - this.bandYPos1000) / (num2 - num1)) : this.bandYPos0 - lastHousePoint * (this.bandYPos0 - this.bandYPos100) / num1;
        int size = this.filledBands[index3][this.readOrder[flag]];
        CustomSelfDrawPanel.CSDImage flagImage = this.getFlagImage(flag);
        flagImage.Position = new Point(x1, y2);
        CustomSelfDrawPanel.CSDImage flagpoleImage = this.getFlagpoleImage(flag);
        int num24 = GameEngine.Instance.World.HouseInfo[houseID].numVictories;
        int num25 = num24;
        if (num24 > 5)
          num24 = 5;
        BaseImage baseImage = (BaseImage) null;
        int num26 = 0;
        int num27 = 0;
        int num28 = 0;
        int num29 = x1;
        switch (size)
        {
          case 0:
            flagpoleImage.Position = new Point(x1 + 11 + 1, y2 + 500);
            x1 += 45;
            flagImage.Width = 45;
            flagImage.Height = 575;
            flagImage.Image = GFXLibrary.getHouseBannerImage(houseID, size);
            flagpoleImage.Image = (Image) GFXLibrary.glory_thin_pole;
            baseImage = GFXLibrary.glory_star_small;
            num26 = 14;
            num27 = 9;
            num28 = 4;
            break;
          case 1:
            flagpoleImage.Position = new Point(x1 + 19, y2 + 500);
            x1 += 60;
            flagImage.Width = 60;
            flagImage.Height = 600;
            flagImage.Image = GFXLibrary.getHouseBannerImage(houseID, size);
            flagpoleImage.Image = (Image) GFXLibrary.glory_thin_pole;
            baseImage = GFXLibrary.glory_star_small;
            num26 = 20;
            num27 = 12;
            num28 = 4;
            break;
          case 2:
            flagpoleImage.Position = new Point(x1 + 30 - 5, y2 + 500);
            x1 += 90;
            flagImage.Width = 90;
            flagImage.Height = 600;
            flagImage.Image = GFXLibrary.getHouseBannerImage(houseID, size);
            flagpoleImage.Image = (Image) GFXLibrary.glory_thick_pole;
            baseImage = GFXLibrary.glory_star_large;
            num26 = 29;
            num27 = 16;
            num28 = 7;
            break;
          case 3:
            flagpoleImage.Position = new Point(x1 + 40 - 6, y2 + 500);
            x1 += 110;
            flagImage.Width = 110;
            flagImage.Height = 600;
            flagImage.Image = GFXLibrary.getHouseBannerImage(houseID, size);
            flagpoleImage.Image = (Image) GFXLibrary.glory_thick_pole;
            baseImage = GFXLibrary.glory_star_large;
            num26 = 39;
            num27 = 20;
            num28 = 7;
            break;
        }
        flagImage.CustomTooltipID = 1700 + houseID - 1;
        flagImage.CustomTooltipData = num23;
        flagImage.Data = houseID;
        flagImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.viewHouse), "GloryPanel_view_house");
        this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) flagpoleImage);
        this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) flagImage);
        for (int star1 = 0; star1 < num24; ++star1)
        {
          CustomSelfDrawPanel.CSDImage star2 = this.getStar(houseID - 1, star1);
          star2.Image = (Image) baseImage;
          star2.Position = new Point(num29 + num26 + this.starSteps[star1] * num27, y2 - num28);
          this.viewableArea.addControl((CustomSelfDrawPanel.CSDControl) star2);
          CustomSelfDrawPanel.CSDImage csdImage = star2;
          int num30 = 5;
          int x2 = 5;
          while (num30 < 100)
          {
            if (star1 + num30 + 1 <= num25)
            {
              CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage();
              control.Image = (Image) baseImage;
              control.Position = new Point(x2, -8);
              csdImage.addControl((CustomSelfDrawPanel.CSDControl) control);
              csdImage = control;
            }
            num30 += 5;
            x2 *= -1;
          }
        }
      }
      this.Invalidate();
    }

    public void makeSizes()
    {
      if (this.bandsMade)
        return;
      this.bandsMade = true;
      for (int index1 = 0; index1 < this.bands.Length; index1 += 4)
      {
        int band1 = this.bands[index1];
        int band2 = this.bands[index1 + 1];
        int band3 = this.bands[index1 + 2];
        int band4 = this.bands[index1 + 3];
        int[] numArray = new int[21];
        int index2 = 0;
        int num = 0;
        for (int index3 = 0; index3 < band1; ++index3)
        {
          numArray[index2++] = 3;
          num += 110;
        }
        for (int index4 = 0; index4 < band2; ++index4)
        {
          numArray[index2++] = 2;
          num += 90;
        }
        for (int index5 = 0; index5 < band3; ++index5)
        {
          numArray[index2++] = 1;
          num += 60;
        }
        for (int index6 = 0; index6 < band4; ++index6)
        {
          numArray[index2++] = 0;
          num += 45;
        }
        numArray[index2] = num;
        this.filledBands[index1 / 4] = numArray;
      }
    }

    public CustomSelfDrawPanel.CSDImage getFlagImage(int flag)
    {
      switch (flag)
      {
        case 0:
          return this.flagImage0;
        case 1:
          return this.flagImage1;
        case 2:
          return this.flagImage2;
        case 3:
          return this.flagImage3;
        case 4:
          return this.flagImage4;
        case 5:
          return this.flagImage5;
        case 6:
          return this.flagImage6;
        case 7:
          return this.flagImage7;
        case 8:
          return this.flagImage8;
        case 9:
          return this.flagImage9;
        case 10:
          return this.flagImage10;
        case 11:
          return this.flagImage11;
        case 12:
          return this.flagImage12;
        case 13:
          return this.flagImage13;
        case 14:
          return this.flagImage14;
        case 15:
          return this.flagImage15;
        case 16:
          return this.flagImage16;
        case 17:
          return this.flagImage17;
        case 18:
          return this.flagImage18;
        case 19:
          return this.flagImage19;
        default:
          return this.flagImage0;
      }
    }

    public CustomSelfDrawPanel.CSDImage getFlagpoleImage(int flag)
    {
      switch (flag)
      {
        case 0:
          return this.flagpoleImage0;
        case 1:
          return this.flagpoleImage1;
        case 2:
          return this.flagpoleImage2;
        case 3:
          return this.flagpoleImage3;
        case 4:
          return this.flagpoleImage4;
        case 5:
          return this.flagpoleImage5;
        case 6:
          return this.flagpoleImage6;
        case 7:
          return this.flagpoleImage7;
        case 8:
          return this.flagpoleImage8;
        case 9:
          return this.flagpoleImage9;
        case 10:
          return this.flagpoleImage10;
        case 11:
          return this.flagpoleImage11;
        case 12:
          return this.flagpoleImage12;
        case 13:
          return this.flagpoleImage13;
        case 14:
          return this.flagpoleImage14;
        case 15:
          return this.flagpoleImage15;
        case 16:
          return this.flagpoleImage16;
        case 17:
          return this.flagpoleImage17;
        case 18:
          return this.flagpoleImage18;
        case 19:
          return this.flagpoleImage19;
        default:
          return this.flagpoleImage0;
      }
    }

    public CustomSelfDrawPanel.CSDImage getStar(int flag, int star)
    {
      switch (flag)
      {
        case 0:
          switch (star)
          {
            case 0:
              return this.flag0ImageStar1;
            case 1:
              return this.flag0ImageStar2;
            case 2:
              return this.flag0ImageStar3;
            case 3:
              return this.flag0ImageStar4;
            case 4:
              return this.flag0ImageStar5;
          }
          break;
        case 1:
          switch (star)
          {
            case 0:
              return this.flag1ImageStar1;
            case 1:
              return this.flag1ImageStar2;
            case 2:
              return this.flag1ImageStar3;
            case 3:
              return this.flag1ImageStar4;
            case 4:
              return this.flag1ImageStar5;
          }
          break;
        case 2:
          switch (star)
          {
            case 0:
              return this.flag2ImageStar1;
            case 1:
              return this.flag2ImageStar2;
            case 2:
              return this.flag2ImageStar3;
            case 3:
              return this.flag2ImageStar4;
            case 4:
              return this.flag2ImageStar5;
          }
          break;
        case 3:
          switch (star)
          {
            case 0:
              return this.flag3ImageStar1;
            case 1:
              return this.flag3ImageStar2;
            case 2:
              return this.flag3ImageStar3;
            case 3:
              return this.flag3ImageStar4;
            case 4:
              return this.flag3ImageStar5;
          }
          break;
        case 4:
          switch (star)
          {
            case 0:
              return this.flag4ImageStar1;
            case 1:
              return this.flag4ImageStar2;
            case 2:
              return this.flag4ImageStar3;
            case 3:
              return this.flag4ImageStar4;
            case 4:
              return this.flag4ImageStar5;
          }
          break;
        case 5:
          switch (star)
          {
            case 0:
              return this.flag5ImageStar1;
            case 1:
              return this.flag5ImageStar2;
            case 2:
              return this.flag5ImageStar3;
            case 3:
              return this.flag5ImageStar4;
            case 4:
              return this.flag5ImageStar5;
          }
          break;
        case 6:
          switch (star)
          {
            case 0:
              return this.flag6ImageStar1;
            case 1:
              return this.flag6ImageStar2;
            case 2:
              return this.flag6ImageStar3;
            case 3:
              return this.flag6ImageStar4;
            case 4:
              return this.flag6ImageStar5;
          }
          break;
        case 7:
          switch (star)
          {
            case 0:
              return this.flag7ImageStar1;
            case 1:
              return this.flag7ImageStar2;
            case 2:
              return this.flag7ImageStar3;
            case 3:
              return this.flag7ImageStar4;
            case 4:
              return this.flag7ImageStar5;
          }
          break;
        case 8:
          switch (star)
          {
            case 0:
              return this.flag8ImageStar1;
            case 1:
              return this.flag8ImageStar2;
            case 2:
              return this.flag8ImageStar3;
            case 3:
              return this.flag8ImageStar4;
            case 4:
              return this.flag8ImageStar5;
          }
          break;
        case 9:
          switch (star)
          {
            case 0:
              return this.flag9ImageStar1;
            case 1:
              return this.flag9ImageStar2;
            case 2:
              return this.flag9ImageStar3;
            case 3:
              return this.flag9ImageStar4;
            case 4:
              return this.flag9ImageStar5;
          }
          break;
        case 10:
          switch (star)
          {
            case 0:
              return this.flag10ImageStar1;
            case 1:
              return this.flag10ImageStar2;
            case 2:
              return this.flag10ImageStar3;
            case 3:
              return this.flag10ImageStar4;
            case 4:
              return this.flag10ImageStar5;
          }
          break;
        case 11:
          switch (star)
          {
            case 0:
              return this.flag11ImageStar1;
            case 1:
              return this.flag11ImageStar2;
            case 2:
              return this.flag11ImageStar3;
            case 3:
              return this.flag11ImageStar4;
            case 4:
              return this.flag11ImageStar5;
          }
          break;
        case 12:
          switch (star)
          {
            case 0:
              return this.flag12ImageStar1;
            case 1:
              return this.flag12ImageStar2;
            case 2:
              return this.flag12ImageStar3;
            case 3:
              return this.flag12ImageStar4;
            case 4:
              return this.flag12ImageStar5;
          }
          break;
        case 13:
          switch (star)
          {
            case 0:
              return this.flag13ImageStar1;
            case 1:
              return this.flag13ImageStar2;
            case 2:
              return this.flag13ImageStar3;
            case 3:
              return this.flag13ImageStar4;
            case 4:
              return this.flag13ImageStar5;
          }
          break;
        case 14:
          switch (star)
          {
            case 0:
              return this.flag14ImageStar1;
            case 1:
              return this.flag14ImageStar2;
            case 2:
              return this.flag14ImageStar3;
            case 3:
              return this.flag14ImageStar4;
            case 4:
              return this.flag14ImageStar5;
          }
          break;
        case 15:
          switch (star)
          {
            case 0:
              return this.flag15ImageStar1;
            case 1:
              return this.flag15ImageStar2;
            case 2:
              return this.flag15ImageStar3;
            case 3:
              return this.flag15ImageStar4;
            case 4:
              return this.flag15ImageStar5;
          }
          break;
        case 16:
          switch (star)
          {
            case 0:
              return this.flag16ImageStar1;
            case 1:
              return this.flag16ImageStar2;
            case 2:
              return this.flag16ImageStar3;
            case 3:
              return this.flag16ImageStar4;
            case 4:
              return this.flag16ImageStar5;
          }
          break;
        case 17:
          switch (star)
          {
            case 0:
              return this.flag17ImageStar1;
            case 1:
              return this.flag17ImageStar2;
            case 2:
              return this.flag17ImageStar3;
            case 3:
              return this.flag17ImageStar4;
            case 4:
              return this.flag17ImageStar5;
          }
          break;
        case 18:
          switch (star)
          {
            case 0:
              return this.flag18ImageStar1;
            case 1:
              return this.flag18ImageStar2;
            case 2:
              return this.flag18ImageStar3;
            case 3:
              return this.flag18ImageStar4;
            case 4:
              return this.flag18ImageStar5;
          }
          break;
        case 19:
          switch (star)
          {
            case 0:
              return this.flag19ImageStar1;
            case 1:
              return this.flag19ImageStar2;
            case 2:
              return this.flag19ImageStar3;
            case 3:
              return this.flag19ImageStar4;
            case 4:
              return this.flag19ImageStar5;
          }
          break;
      }
      return this.flag0ImageStar1;
    }

    public void update()
    {
    }

    public void GetHouseGloryPointsCallBack(GetHouseGloryPoints_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      GameEngine.Instance.World.HouseGloryPoints = returnData.gloryPoints;
      GameEngine.Instance.World.HouseGloryRoundData = returnData.gloryRoundData;
      this.init();
    }

    public void viewHouse()
    {
      if (this.ClickedControl == null)
        return;
      InterfaceMgr.Instance.showHousePanel(this.ClickedControl.Data);
    }

    public void gloryWinnerClick() => InterfaceMgr.Instance.openGloryVictoryPopup();

    public void viewEndWorldPanelClick() => InterfaceMgr.Instance.setVillageTabSubMode(65, false);

    public void playbackCountryClick()
    {
      this.playbackIsCountry = true;
      if (GameEngine.Instance.World.gotPlaybackData())
      {
        GameEngine.Instance.World.playbackCountries();
      }
      else
      {
        this.awaitingPlayback = true;
        this.retrieveGameStats();
      }
    }

    public void playbackProvinceClick()
    {
      this.playbackIsCountry = false;
      if (GameEngine.Instance.World.gotPlaybackData())
      {
        GameEngine.Instance.World.playbackProvinces();
      }
      else
      {
        this.awaitingPlayback = true;
        this.retrieveGameStats();
      }
    }

    public void gloryValuesClick() => InterfaceMgr.Instance.openGloryValuesPopup();

    public void retrieveGameStats()
    {
      if (this.bAwaitingStats)
        return;
      this.bAwaitingStats = true;
      RemoteServices.Instance.set_RetrieveStats2_UserCallBack(new RemoteServices.RetrieveStats2_UserCallBack(this.retrieveStatsCallback2));
      RemoteServices.Instance.RetrieveStats2();
    }

    public void retrieveStatsCallback2(RetrieveStats2_ReturnType returnData)
    {
      if (returnData.Success && returnData.mapHistoryData != null)
      {
        RetrieveStats_ReturnType returnData1 = new RetrieveStats_ReturnType();
        returnData1.Success = true;
        returnData1.worldStartTime = returnData.worldStartTime;
        returnData1.mapHistory = new List<WorldHouseHistoryItem>();
        for (int index = 0; index < returnData.mapHistoryData.Length; index += 7)
        {
          WorldHouseHistoryItem houseHistoryItem = new WorldHouseHistoryItem();
          houseHistoryItem.houseID = (int) returnData.mapHistoryData[index];
          houseHistoryItem.provinceID = returnData.mapHistoryData[index + 1] != byte.MaxValue || returnData.mapHistoryData[index + 2] != byte.MaxValue ? (int) returnData.mapHistoryData[index + 1] * 256 + (int) returnData.mapHistoryData[index + 2] : -1;
          houseHistoryItem.countryID = returnData.mapHistoryData[index + 3] != byte.MaxValue || returnData.mapHistoryData[index + 4] != byte.MaxValue ? (int) returnData.mapHistoryData[index + 3] * 256 + (int) returnData.mapHistoryData[index + 4] : -1;
          int num = (int) returnData.mapHistoryData[index + 5] * 256 + (int) returnData.mapHistoryData[index + 6];
          houseHistoryItem.date = returnData.worldStartTime.AddDays((double) num);
          returnData1.mapHistory.Add(houseHistoryItem);
        }
        this.retrieveStatsCallback(returnData1);
      }
      this.bAwaitingStats = false;
    }

    public void retrieveStatsCallback(RetrieveStats_ReturnType returnData)
    {
      if (returnData.Success && returnData.mapHistory != null)
      {
        GameEngine.Instance.World.setPlaybackData(returnData.mapHistory, returnData.worldStartTime);
        if (this.awaitingPlayback)
        {
          this.awaitingPlayback = false;
          if (this.playbackIsCountry)
            GameEngine.Instance.World.playbackCountries();
          else
            GameEngine.Instance.World.playbackProvinces();
        }
      }
      this.bAwaitingStats = false;
    }

    public void initProperties(bool dockable, string title, ContainerControl parent)
    {
      this.dockableControl.initProperties(dockable, title, parent);
    }

    public void display(ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(parent, x, y);
    }

    public void display(bool asPopup, ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(asPopup, parent, x, y);
    }

    public void controlDockToggle() => this.dockableControl.controlDockToggle();

    public void closeControl(bool includePopups)
    {
      this.dockableControl.closeControl(includePopups);
      this.clearControls();
    }

    public bool isVisible() => this.dockableControl.isVisible();

    public bool isPopup() => this.dockableControl.isPopup();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleMode = AutoScaleMode.None;
      this.MaximumSize = new Size(1600, 1024);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (GloryPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }
  }
}
