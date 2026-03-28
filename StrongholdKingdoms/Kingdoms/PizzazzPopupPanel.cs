// Decompiled with JetBrains decompiler
// Type: Kingdoms.PizzazzPopupPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PizzazzPopupPanel : CustomSelfDrawPanel
  {
    public const int PIZZAZZ_IMAGE_APPLES = 1;
    public const int PIZZAZZ_IMAGE_WOOD_STONE = 2;
    public const int PIZZAZZ_IMAGE_RESEARCH_POINT = 3;
    public const int PIZZAZZ_IMAGE_HONOUR = 4;
    public const int PIZZAZZ_IMAGE_CARD_WOOD = 5;
    public const int PIZZAZZ_IMAGE_CARD_POINTS = 6;
    public const int PIZZAZZ_IMAGE_PREMIUM = 7;
    public const int PIZZAZZ_IMAGE_GOLD = 8;
    public const int PIZZAZZ_IMAGE_WOOD = 9;
    public const int PIZZAZZ_IMAGE_CARD_CASTLE = 10;
    private const int CENTER_X = 191;
    private const int CENTER_Y = 41;
    private CustomSelfDrawPanel.CSDFill transparentBackground = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDExtendingPanel background = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDImage starImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea rewardArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDImage rewardImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage reward2Image = new CustomSelfDrawPanel.CSDImage();
    private int starSpinMode;
    private int starSpinCount;
    private float starRotate;
    private float starRotateSpeed;
    private float animCount;
    private PizzazzPopupPanel.Firework firework1 = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework2 = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework3 = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework4 = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework5 = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework6 = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework7 = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework8 = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework9 = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework10 = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework1a = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework2a = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework3a = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework4a = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework5a = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework6a = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework7a = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework8a = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework9a = new PizzazzPopupPanel.Firework();
    private PizzazzPopupPanel.Firework firework10a = new PizzazzPopupPanel.Firework();
    private IContainer components;

    public PizzazzPopupPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(int pizzazzImage)
    {
      Sound.playVillageEnvironmental(46, false, false);
      Sound.forceFullPlayOfNextEnvironmental();
      this.clearControls();
      this.transparentBackground.Size = this.Size;
      this.transparentBackground.FillColor = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.transparentBackground);
      this.background.Position = new Point(0, 0);
      this.background.Size = new Size(638, 328);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
      this.background.Create((Image) GFXLibrary._9sclice_fancy_top_left, (Image) GFXLibrary._9sclice_fancy_top_mid, (Image) GFXLibrary._9sclice_fancy_top_right, (Image) GFXLibrary._9sclice_fancy_mid_left, (Image) GFXLibrary._9sclice_fancy_mid_mid, (Image) GFXLibrary._9sclice_fancy_mid_right, (Image) GFXLibrary._9sclice_fancy_bottom_left, (Image) GFXLibrary._9sclice_fancy_bottom_mid, (Image) GFXLibrary._9sclice_fancy_bottom_right);
      this.background.ForceTiling();
      this.rewardArea.Position = new Point(191, 41);
      this.rewardArea.Size = new Size(256, 256);
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.rewardArea);
      this.firework1.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 36, 0);
      this.firework2.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 32, 0);
      this.firework3.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 28, 0);
      this.firework4.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 24, 0);
      this.firework5.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 20, 0);
      this.firework6.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 16, 0);
      this.firework7.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 12, 0);
      this.firework8.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 8, 0);
      this.firework9.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 4, 0);
      this.firework10.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 0, 0);
      this.firework1a.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 36, 1);
      this.firework2a.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 32, 1);
      this.firework3a.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 28, 1);
      this.firework4a.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 24, 1);
      this.firework5a.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 20, 1);
      this.firework6a.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 16, 1);
      this.firework7a.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 12, 1);
      this.firework8a.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 8, 1);
      this.firework9a.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 4, 1);
      this.firework10a.init((CustomSelfDrawPanel.CSDControl) this.rewardArea, 0, 1);
      this.starImage.Image = (Image) GFXLibrary.wheel_star[0];
      this.starImage.Position = new Point(0, 0);
      this.starImage.RotateCentre = new PointF(128f, 128f);
      this.starImage.Visible = false;
      this.starSpinMode = 1;
      this.rewardArea.addControl((CustomSelfDrawPanel.CSDControl) this.starImage);
      switch (pizzazzImage)
      {
        case 1:
          this.rewardImage.Image = (Image) GFXLibrary.getCommodity64DSImage(13);
          this.rewardImage.Position = new Point(83, 79);
          this.rewardArea.addControl((CustomSelfDrawPanel.CSDControl) this.rewardImage);
          break;
        case 2:
          this.rewardImage.Image = (Image) GFXLibrary.getCommodity64DSImage(6);
          this.rewardImage.Position = new Point(76, 76);
          this.rewardArea.addControl((CustomSelfDrawPanel.CSDControl) this.rewardImage);
          this.reward2Image.Image = (Image) GFXLibrary.getCommodity64DSImage(7);
          this.reward2Image.Position = new Point(96, 96);
          this.rewardArea.addControl((CustomSelfDrawPanel.CSDControl) this.reward2Image);
          break;
        case 3:
        case 5:
        case 6:
        case 8:
        case 10:
          int index = -1;
          switch (pizzazzImage - 3)
          {
            case 0:
              index = 2;
              break;
            case 2:
              index = 15;
              break;
            case 3:
              index = 1;
              break;
            case 5:
              index = 5;
              break;
            case 7:
              index = 14;
              break;
          }
          this.rewardImage.Image = (Image) GFXLibrary.wheel_icons[index];
          this.rewardImage.Size = new Size(128, 128);
          this.rewardImage.Position = new Point(64, 64);
          this.rewardArea.addControl((CustomSelfDrawPanel.CSDControl) this.rewardImage);
          break;
        case 4:
          this.rewardImage.Image = (Image) GFXLibrary.com_64_honour_DS;
          this.rewardImage.Position = new Point(84, 84);
          this.rewardArea.addControl((CustomSelfDrawPanel.CSDControl) this.rewardImage);
          break;
        case 7:
          this.rewardImage.Image = (Image) GFXLibrary.PremiumTokens[4113][0];
          this.rewardImage.Position = new Point(96, 96);
          this.rewardImage.Size = new Size(this.rewardImage.Image.Width / 2, this.rewardImage.Image.Height / 2);
          this.rewardArea.addControl((CustomSelfDrawPanel.CSDControl) this.rewardImage);
          break;
        case 9:
          this.rewardImage.Image = (Image) GFXLibrary.getCommodity64DSImage(6);
          this.rewardImage.Position = new Point(83, 79);
          this.rewardArea.addControl((CustomSelfDrawPanel.CSDControl) this.rewardImage);
          break;
      }
    }

    public void update()
    {
      float num1 = 1f;
      this.firework1.update();
      this.firework2.update();
      this.firework3.update();
      this.firework4.update();
      this.firework5.update();
      this.firework6.update();
      this.firework7.update();
      this.firework8.update();
      this.firework9.update();
      this.firework10.update();
      this.firework1a.update();
      this.firework2a.update();
      this.firework3a.update();
      this.firework4a.update();
      this.firework5a.update();
      this.firework6a.update();
      this.firework7a.update();
      this.firework8a.update();
      this.firework9a.update();
      this.firework10a.update();
      this.Invalidate();
      this.animCount += num1;
      if ((double) this.animCount >= 180.0)
        PizzazzPopupWindow.closePizzazz();
      else if ((double) this.animCount > 150.0)
        PizzazzPopupPanel.Firework.active = false;
      if (this.starSpinMode <= 0)
        return;
      switch (this.starSpinMode)
      {
        case 1:
          this.starImage.Image = (Image) GFXLibrary.wheel_star[2];
          this.starImage.Visible = true;
          this.starImage.Alpha = 0.01f;
          ++this.starSpinMode;
          this.starRotate = 0.0f;
          this.starRotateSpeed = 8f;
          this.starSpinCount = 200;
          break;
        case 2:
          this.starImage.Alpha += 0.2f * num1;
          if ((double) this.starImage.Alpha > 1.0)
          {
            this.starImage.Alpha = 1f;
            ++this.starSpinMode;
            break;
          }
          break;
        case 3:
          --this.starSpinCount;
          if (this.starSpinCount == 0)
          {
            ++this.starSpinMode;
            break;
          }
          break;
        case 4:
          this.starImage.Alpha -= 0.1f * num1;
          if ((double) this.starImage.Alpha < 0.0)
          {
            this.starImage.Alpha = 0.0f;
            this.starImage.Visible = false;
            this.starSpinMode = 0;
            this.starRotateSpeed = 0.0f;
            break;
          }
          break;
      }
      float num2 = this.starRotate;
      if ((double) num2 >= 179.89999389648438 && (double) num2 <= 180.0)
        num2 = 179.9f;
      else if ((double) num2 > 180.0 && (double) num2 <= 180.10000610351563)
        num2 = 180.1f;
      this.starImage.Rotate = num2;
      this.starRotate += this.starRotateSpeed * num1;
      if ((double) this.starRotate >= 360.0)
        this.starRotate -= 360f;
      this.starImage.Image = (double) this.starRotateSpeed > 8.0 ? ((double) this.starRotateSpeed >= 15.0 ? (Image) GFXLibrary.wheel_star[2] : (Image) GFXLibrary.wheel_star[1]) : (Image) GFXLibrary.wheel_star[0];
      this.starImage.invalidate();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.None;
    }

    public class Firework : CustomSelfDrawPanel.CSDImage
    {
      private const float rate = 4f;
      private const int max = 40;
      public static bool active = false;
      private static Random rand = new Random();
      private float dx;
      private float dy;
      private PointF pos;
      private float count;
      private static int baseAngle1 = 0;
      private static int baseAngle2 = 180;
      private int m_band;

      public void init(CustomSelfDrawPanel.CSDControl parentControl, int initialProgress, int band)
      {
        PizzazzPopupPanel.Firework.active = true;
        this.m_band = band;
        this.Image = (Image) GFXLibrary.tutorial_reward_anim[0];
        this.RotateCentre = new PointF(64f, 64f);
        this.Visible = true;
        parentControl.addControl((CustomSelfDrawPanel.CSDControl) this);
        this.restart();
        for (int index = 0; index < initialProgress; ++index)
          this.update();
      }

      public void restart()
      {
        if (PizzazzPopupPanel.Firework.active)
        {
          int degrees = PizzazzPopupPanel.Firework.baseAngle1;
          if (this.m_band == 0)
            PizzazzPopupPanel.Firework.baseAngle1 -= 37;
          else if (this.m_band == 1)
          {
            PizzazzPopupPanel.Firework.baseAngle2 -= 37;
            degrees = PizzazzPopupPanel.Firework.baseAngle2;
          }
          PointF pointF = GameEngine.Instance.GFX.rotatePoint(new PointF(1f, 0.0f), degrees);
          this.dx = pointF.X;
          this.dy = pointF.Y;
          this.pos = new PointF(64f, 64f);
          this.Position = new Point(64, 64);
          this.Rotate = 0.0f;
          this.count = 0.0f;
          this.Alpha = 1f;
        }
        else
          this.Visible = false;
      }

      public void update()
      {
        float num1 = 1f;
        if ((double) this.count > 30.0)
        {
          int num2 = (int) (40.0 - (double) this.count);
          if (num2 > 10)
            num2 = 10;
          else if (num2 < 0)
            num2 = 0;
          this.Alpha = (float) num2 / 10f;
        }
        this.pos = new PointF(this.pos.X + this.dx * 4f * num1, this.pos.Y + this.dy * 4f * num1);
        this.Position = new Point((int) this.pos.X, (int) this.pos.Y);
        this.count += num1;
        if ((double) this.count >= 40.0)
          this.restart();
        this.Image = (Image) GFXLibrary.tutorial_reward_anim[Math.Min((int) this.count / 2, 19)];
      }
    }
  }
}
