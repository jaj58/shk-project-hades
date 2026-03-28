// Decompiled with JetBrains decompiler
// Type: Kingdoms.WheelPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class WheelPanel : CustomSelfDrawPanel
  {
    private const double DEG2RAD = 0.0174533;
    private IContainer components;
    private static WheelPanel Instance;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel labelTitle2 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel helpLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel numTicketsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDExtendingPanel greenArea = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel MainPanel = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDButton spinButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage spinGlow = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage wheelImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea wheelLayer1 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea wheelLayer2 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea wheelLayer3 = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDImage pointerShadowImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage pointerImage = new CustomSelfDrawPanel.CSDImage();
    private WheelPanel.RewardImage[] rewardImages = new WheelPanel.RewardImage[20];
    private WheelPanel.RewardImage centreRewardImage = new WheelPanel.RewardImage();
    private WheelPanel.RewardImage prizeRewardImage = new WheelPanel.RewardImage();
    private CustomSelfDrawPanel.CSDLabel rewardDescription = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage starImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage pegImage = new CustomSelfDrawPanel.CSDImage();
    private bool rewardImagesCreated;
    private int m_cardAdded = -1;
    private int m_wheelType = -1;
    private bool royal = true;
    private List<WheelReward> rewards;
    private float pointerRotate;
    private float pointerRotateSpeed;
    private float lastRotate = -1000f;
    private int spinMode;
    private int targetSegment = 5;
    private int pullbackCount;
    private int starSpinMode;
    private int starSpinCount;
    private float starRotate;
    private float starRotateSpeed;
    private float spinStopExtra;
    private int lastGlowSegment = -1;
    private WheelReward m_storedReward;

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

    public WheelPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool initialCall, int wheelType)
    {
      this.m_wheelType = wheelType;
      WheelPanel.Instance = this;
      this.clearControls();
      if (!this.rewardImagesCreated)
      {
        this.rewardImagesCreated = true;
        for (int index = 0; index < 20; ++index)
          this.rewardImages[index] = new WheelPanel.RewardImage();
      }
      this.mainBackgroundImage.Image = GFXLibrary.dummy;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = this.Size;
      this.mainBackgroundImage.Tile = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.MainPanel.Size = this.Size;
      this.MainPanel.Position = new Point(0, 0);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.MainPanel);
      this.MainPanel.Create((Image) GFXLibrary.cardpanel_panel_back_top_left, (Image) GFXLibrary.cardpanel_panel_back_top_mid, (Image) GFXLibrary.cardpanel_panel_back_top_right, (Image) GFXLibrary.cardpanel_panel_back_mid_left, (Image) GFXLibrary.cardpanel_panel_back_mid_mid, (Image) GFXLibrary.cardpanel_panel_back_mid_right, (Image) GFXLibrary.cardpanel_panel_back_bottom_left, (Image) GFXLibrary.cardpanel_panel_back_bottom_mid, (Image) GFXLibrary.cardpanel_panel_back_bottom_right);
      CustomSelfDrawPanel.CSDImage control1 = new CustomSelfDrawPanel.CSDImage();
      control1.Image = (Image) GFXLibrary.cardpanel_panel_gradient_top_left;
      control1.Size = GFXLibrary.cardpanel_panel_gradient_top_left.Size;
      control1.Position = new Point(0, 0);
      this.MainPanel.addControl((CustomSelfDrawPanel.CSDControl) control1);
      CustomSelfDrawPanel.CSDImage control2 = new CustomSelfDrawPanel.CSDImage();
      control2.Image = (Image) GFXLibrary.cardpanel_panel_gradient_bottom_right;
      control2.Size = GFXLibrary.cardpanel_panel_gradient_bottom_right.Size;
      control2.Position = new Point(this.MainPanel.Width - control2.Width - 6, this.MainPanel.Height - control2.Height - 6);
      this.MainPanel.addControl((CustomSelfDrawPanel.CSDControl) control2);
      this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
      this.closeImage.Size = this.closeImage.Image.Size;
      this.closeImage.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal));
      this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Cards_Close");
      this.closeImage.Position = new Point(this.Width - 14 - 17, 10);
      this.closeImage.CustomTooltipID = 10100;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeImage);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage, 32, new Point(this.Width - 40 - 40, 2));
      CustomSelfDrawPanel.CSDFill control3 = new CustomSelfDrawPanel.CSDFill();
      control3.FillColor = Color.FromArgb((int) byte.MaxValue, 130, 129, 126);
      control3.Size = new Size(this.Width - 10, 1);
      control3.Position = new Point(5, 34);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control3);
      this.labelTitle.Position = new Point(27, 5);
      this.labelTitle.Size = new Size(600, 64);
      switch (this.m_wheelType)
      {
        case -1:
          this.labelTitle.Text = SK.Text("WheelPanel_Royal_Wheel", "Quest Wheel");
          break;
        case 0:
          this.labelTitle.Text = SK.Text("WheelPanel_Treasure_Wheel_1", "Treasure Wheel Tier 1");
          break;
        case 1:
          this.labelTitle.Text = SK.Text("WheelPanel_Treasure_Wheel_2", "Treasure Wheel Tier 2");
          break;
        case 2:
          this.labelTitle.Text = SK.Text("WheelPanel_Treasure_Wheel_3", "Treasure Wheel Tier 3");
          break;
        case 3:
          this.labelTitle.Text = SK.Text("WheelPanel_Treasure_Wheel_4", "Treasure Wheel Tier 4");
          break;
        case 4:
          this.labelTitle.Text = SK.Text("WheelPanel_Treasure_Wheel_5", "Treasure Wheel Tier 5");
          break;
      }
      this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.labelTitle.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
      this.labelTitle.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelTitle);
      if (this.m_wheelType == -1)
      {
        if (Program.mySettings.LanguageIdent == "it")
          this.labelTitle2.Position = new Point(300, 10);
        else
          this.labelTitle2.Position = new Point(250, 10);
        this.labelTitle2.Size = new Size(600, 64);
        this.labelTitle2.Text = "(" + SK.Text("WheelPanel_Level", "Level") + " " + (Wheel.getWheelLevel(GameEngine.Instance.World.getRank()) + 1).ToString() + ")";
        this.labelTitle2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.labelTitle2.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
        this.labelTitle2.Color = ARGBColors.Black;
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelTitle2);
      }
      this.wheelImage.Image = (Image) GFXLibrary.wheel_wheel_royal;
      this.wheelImage.Position = new Point(3, 35);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wheelImage);
      this.numTicketsLabel.Position = new Point(725, 42);
      this.numTicketsLabel.Size = new Size(600, 54);
      this.numTicketsLabel.Text = SK.Text("WheelPanel_Spins", "Spins") + ": " + GameEngine.Instance.World.getTickets(this.m_wheelType).ToString();
      this.numTicketsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.numTicketsLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
      this.numTicketsLabel.Color = ARGBColors.Black;
      this.wheelImage.addControl((CustomSelfDrawPanel.CSDControl) this.numTicketsLabel);
      this.starImage.Image = (Image) GFXLibrary.wheel_star[0];
      this.starImage.Position = new Point(557, 47);
      this.starImage.RotateCentre = new PointF(128f, 128f);
      this.starImage.Visible = false;
      this.starSpinMode = 0;
      this.wheelImage.addControl((CustomSelfDrawPanel.CSDControl) this.starImage);
      this.pegImage.Image = !this.royal ? (Image) GFXLibrary.wheel_icons[13] : (Image) GFXLibrary.wheel_icons[12];
      this.pegImage.Position = new Point(622, 115);
      this.pegImage.Visible = true;
      this.wheelImage.addControl((CustomSelfDrawPanel.CSDControl) this.pegImage);
      this.rewardDescription.Position = new Point(733, 138);
      this.rewardDescription.Size = new Size(155, 80);
      this.rewardDescription.Text = "";
      this.rewardDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.rewardDescription.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.rewardDescription.Color = ARGBColors.Black;
      this.wheelImage.addControl((CustomSelfDrawPanel.CSDControl) this.rewardDescription);
      this.wheelLayer1.Position = new Point(0, 0);
      this.wheelLayer1.Size = this.wheelImage.Size;
      this.wheelImage.addControl((CustomSelfDrawPanel.CSDControl) this.wheelLayer1);
      this.wheelLayer2.Position = new Point(0, 0);
      this.wheelLayer2.Size = this.wheelImage.Size;
      this.wheelImage.addControl((CustomSelfDrawPanel.CSDControl) this.wheelLayer2);
      this.wheelLayer3.Position = new Point(0, 0);
      this.wheelLayer3.Size = this.wheelImage.Size;
      this.wheelImage.addControl((CustomSelfDrawPanel.CSDControl) this.wheelLayer3);
      this.spinGlow.Image = (Image) GFXLibrary.wheel_icons[0];
      this.spinGlow.Position = new Point(517, 423);
      this.wheelImage.addControl((CustomSelfDrawPanel.CSDControl) this.spinGlow);
      this.spinButton.ImageNorm = (Image) GFXLibrary.wheel_spinButton_royal[0];
      this.spinButton.ImageOver = (Image) GFXLibrary.wheel_spinButton_royal[1];
      this.spinButton.MoveOnClick = false;
      this.spinButton.Position = new Point(514, 442);
      this.spinButton.Text.Text = SK.Text("Wheel_Spin", "Spin");
      this.spinButton.TextYOffset = 32;
      this.spinButton.Text.Color = ARGBColors.Black;
      this.spinButton.Text.DropShadowColor = Color.FromArgb(160, 160, 160);
      this.spinButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.spinButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.spinCard));
      this.wheelImage.addControl((CustomSelfDrawPanel.CSDControl) this.spinButton);
      int num = 5;
      this.pointerShadowImage.Image = (Image) GFXLibrary.wheel_arrowBlurShadow[0];
      this.pointerShadowImage.Position = new Point(215 + num, 108 + num);
      this.pointerShadowImage.RotateCentre = new PointF(76.5f, 172.5f);
      this.pointerShadowImage.Alpha = 0.5f;
      this.wheelImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointerShadowImage);
      this.pointerImage.Image = (Image) GFXLibrary.wheel_arrowBlur_royal[0];
      this.pointerImage.RotateCentre = new PointF(76.5f, 172.5f);
      this.pointerImage.Position = new Point(-num, -num);
      this.pointerShadowImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointerImage);
      this.centreRewardImage.init((WheelReward) null, new Point(293, 283));
      this.wheelImage.addControl((CustomSelfDrawPanel.CSDControl) this.centreRewardImage);
      this.prizeRewardImage.init((WheelReward) null, new Point(690, 183));
      this.wheelImage.addControl((CustomSelfDrawPanel.CSDControl) this.prizeRewardImage);
      this.rewards = Wheel.getRewardWheel(GameEngine.Instance.World.getRank(), this.m_wheelType, GameEngine.Instance.LocalWorldData.AIWorld, GameEngine.Instance.LocalWorldData.EUAIWorld);
      if (this.rewards.Count == 20)
      {
        Random random = new Random();
        for (int index1 = 0; index1 < 20; ++index1)
        {
          int index2 = random.Next(20);
          WheelReward reward = this.rewards[index1];
          this.rewards[index1] = this.rewards[index2];
          this.rewards[index2] = reward;
        }
        for (int index = 0; index < 20; ++index)
        {
          Point point = this.rotatePoint(new Point(0, 230), (float) (index * 18 + 9));
          this.rewardImages[index].init(this.rewards[index], new Point(293 + point.X, 283 - point.Y), this.wheelLayer1, this.wheelLayer2, this.wheelLayer3);
          this.wheelImage.addControl((CustomSelfDrawPanel.CSDControl) this.rewardImages[index]);
        }
      }
      this.helpLabel.Text = GameEngine.Instance.World.getTickets(this.m_wheelType) <= 0 ? SK.Text("WheelPanel_nospin_help", "You have no spins available") : SK.Text("WheelPanel_spin_help", "Spin the wheel to win a prize");
      this.helpLabel.Position = new Point(686, 260);
      this.helpLabel.Size = new Size(199, 180);
      this.helpLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.helpLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.helpLabel.Color = ARGBColors.Black;
      this.wheelImage.addControl((CustomSelfDrawPanel.CSDControl) this.helpLabel);
      this.pointerRotate = 0.0f;
      this.pointerRotateSpeed = 0.0f;
      this.spinMode = -2;
      this.lastRotate = -1000f;
      this.updateSpinButton();
      this.Invalidate();
      this.update();
    }

    public void update()
    {
      float num1 = 1f;
      for (int index = 0; index < 20; ++index)
        this.rewardImages[index].update();
      if ((double) this.pointerRotateSpeed != 0.0 || (double) this.lastRotate != (double) this.pointerRotate)
      {
        float num2 = this.pointerRotate;
        if ((double) num2 >= 179.89999389648438 && (double) num2 <= 180.0)
          num2 = 179.9f;
        else if ((double) num2 > 180.0 && (double) num2 <= 180.10000610351563)
          num2 = 180.1f;
        this.pointerShadowImage.Rotate = num2;
        this.pointerImage.Rotate = num2;
        this.pointerRotate += num1 * this.pointerRotateSpeed;
        if ((double) this.pointerRotate >= 360.0)
          this.pointerRotate -= 360f;
        this.lastRotate = this.pointerRotate;
        if ((double) this.pointerRotateSpeed < 7.0)
        {
          this.pointerShadowImage.Image = (Image) GFXLibrary.wheel_arrowBlurShadow[0];
          this.pointerImage.Image = (Image) GFXLibrary.wheel_arrowBlur_royal[0];
        }
        else if ((double) this.pointerRotateSpeed < 15.0)
        {
          this.pointerShadowImage.Image = (Image) GFXLibrary.wheel_arrowBlurShadow[1];
          this.pointerImage.Image = (Image) GFXLibrary.wheel_arrowBlur_royal[1];
        }
        else
        {
          this.pointerShadowImage.Image = (Image) GFXLibrary.wheel_arrowBlurShadow[2];
          this.pointerImage.Image = (Image) GFXLibrary.wheel_arrowBlur_royal[2];
        }
        if (this.spinMode > 1 && this.spinMode < 50 && (double) this.pointerRotateSpeed < 18.0)
        {
          int index = (int) ((double) this.pointerRotate / 18.0);
          if (index >= 0 && index < 20 && index != this.lastGlowSegment)
          {
            this.lastGlowSegment = index;
            GameEngine.Instance.playInterfaceSound("Wheel_individual_segment_" + index.ToString());
            this.rewardImages[index].highlight();
          }
        }
        if (this.spinMode >= -1)
        {
          int index = (int) ((double) this.pointerRotate / 18.0);
          if (index >= 0 && index < 20)
          {
            this.centreRewardImage.fixedHighlight();
            this.centreRewardImage.updateImage(this.rewards[index]);
          }
        }
        this.Invalidate(new Rectangle(108, 130, 370, 370));
      }
      switch (this.spinMode)
      {
        case 0:
          if ((double) this.pointerRotateSpeed < 30.0)
          {
            this.pointerRotateSpeed += 0.4f;
            break;
          }
          this.pointerRotateSpeed = 30f;
          this.spinMode = 1;
          this.m_storedReward = (WheelReward) null;
          this.m_cardAdded = -1;
          RemoteServices.Instance.set_SpinTheWheel_UserCallBack(new RemoteServices.SpinTheWheel_UserCallBack(WheelPanel.s_SpinTheWheelCallback));
          RemoteServices.Instance.SpinTheRoyalWheel(-1, this.m_wheelType);
          break;
        case 2:
          float minAngle1 = (float) ((double) (this.targetSegment * 18 + 9 - 100 - 78) - (double) this.spinStopExtra + 54.0);
          if (this.isPointerInAngle(this.pointerRotate, minAngle1, minAngle1 + 90f))
          {
            ++this.spinMode;
            break;
          }
          break;
        case 3:
          float maxAngle = (float) ((double) (this.targetSegment * 18 + 9 - 196 - 78) - (double) this.spinStopExtra + 54.0);
          if (this.isPointerInAngle(this.pointerRotate, maxAngle - 30f, maxAngle))
          {
            ++this.spinMode;
            break;
          }
          break;
        case 4:
          if ((double) this.pointerRotateSpeed > 5.0)
          {
            --this.pointerRotateSpeed;
            break;
          }
          if ((double) this.pointerRotateSpeed > 1.3999999761581421)
          {
            this.pointerRotateSpeed -= 0.5f;
            break;
          }
          this.pointerRotateSpeed = 1.4f;
          ++this.spinMode;
          break;
        case 5:
          float minAngle2 = (float) (this.targetSegment * 18);
          if (this.isPointerInAngle(this.pointerRotate, minAngle2 - this.spinStopExtra / 3f, minAngle2 + 18f))
          {
            if ((double) this.pointerRotateSpeed > 0.0)
            {
              if ((double) this.pointerRotateSpeed >= 0.5 || this.isPointerInAngle(this.pointerRotate, minAngle2, minAngle2 + 18f))
              {
                this.pointerRotateSpeed -= 0.1f;
                break;
              }
              break;
            }
            this.pointerRotateSpeed = 0.0f;
            this.prizeRewardImage.updateImage(this.m_storedReward);
            this.pegImage.Visible = false;
            this.rewardDescription.Text = Wheel.getRewardText(this.m_storedReward.rewardType, this.m_storedReward.rewardAmount, GameEngine.NFI);
            this.giveReward();
            this.spinMode = -1;
            this.updateSpinButton();
            this.starSpinMode = 1;
            break;
          }
          break;
        case 50:
          --this.pullbackCount;
          if (this.pullbackCount == 0)
          {
            this.pointerRotateSpeed = 1f;
            this.spinMode = 51;
            break;
          }
          break;
        case 51:
          this.pointerRotateSpeed += 4f;
          if ((double) this.pointerRotateSpeed >= 30.0)
          {
            this.spinMode = 1;
            this.m_storedReward = (WheelReward) null;
            this.m_cardAdded = -1;
            RemoteServices.Instance.set_SpinTheWheel_UserCallBack(new RemoteServices.SpinTheWheel_UserCallBack(WheelPanel.s_SpinTheWheelCallback));
            RemoteServices.Instance.SpinTheRoyalWheel(-1, this.m_wheelType);
            break;
          }
          break;
      }
      if (this.starSpinMode <= 0)
        return;
      switch (this.starSpinMode)
      {
        case 1:
          this.starImage.Image = (Image) GFXLibrary.wheel_star[2];
          this.starImage.Visible = true;
          if (this.prizeRewardImage.iconImage.Image == null)
            this.starImage.Visible = false;
          else
            GameEngine.Instance.playInterfaceSound("Wheel_star_start");
          this.starImage.Alpha = 0.01f;
          ++this.starSpinMode;
          this.starRotate = 0.0f;
          this.starRotateSpeed = 8f;
          this.starSpinCount = 90;
          break;
        case 2:
          this.starImage.Alpha += 0.2f;
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
          this.starImage.Alpha -= 0.1f;
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
      float num3 = this.starRotate;
      if ((double) num3 >= 179.89999389648438 && (double) num3 <= 180.0)
        num3 = 179.9f;
      else if ((double) num3 > 180.0 && (double) num3 <= 180.10000610351563)
        num3 = 180.1f;
      this.starImage.Rotate = num3;
      this.starRotate += this.starRotateSpeed;
      if ((double) this.starRotate >= 360.0)
        this.starRotate -= 360f;
      this.starImage.Image = (double) this.starRotateSpeed > 8.0 ? ((double) this.starRotateSpeed >= 15.0 ? (Image) GFXLibrary.wheel_star[2] : (Image) GFXLibrary.wheel_star[1]) : (Image) GFXLibrary.wheel_star[0];
      this.starImage.invalidate();
    }

    private bool isPointerInAngle(float testAngle, float minAngle, float maxAngle)
    {
      return (double) testAngle >= (double) minAngle && (double) testAngle < (double) maxAngle || (double) minAngle < 0.0 && (double) testAngle - 360.0 >= (double) minAngle && (double) testAngle - 360.0 < (double) maxAngle || (double) maxAngle >= 360.0 && (double) testAngle + 360.0 >= (double) minAngle && (double) testAngle + 360.0 < (double) maxAngle;
    }

    private void startWheelSpinning()
    {
      this.lastGlowSegment = -1;
      this.pointerRotateSpeed = -2f;
      this.pullbackCount = 10;
      this.spinMode = 50;
      this.spinStopExtra = (float) new Random().Next(10);
      this.starImage.Visible = false;
      this.starImage.Alpha = 0.01f;
      this.starSpinMode = 0;
    }

    private void updateSpinButton()
    {
      if (GameEngine.Instance.World.getTickets(this.m_wheelType) == 0 || this.spinMode >= 0)
      {
        this.spinButton.Enabled = false;
        this.spinGlow.Visible = false;
      }
      else
      {
        this.spinButton.Enabled = true;
        this.spinGlow.Visible = true;
      }
    }

    private void closeClick()
    {
      InterfaceMgr.Instance.closeWheelPopup();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }

    public static void ClearInstance() => WheelPanel.Instance = (WheelPanel) null;

    private void spinCard()
    {
      if (this.spinMode >= 0)
        return;
      GameEngine.Instance.playInterfaceSound("Wheel_start");
      this.startWheelSpinning();
      this.m_storedReward = (WheelReward) null;
      this.prizeRewardImage.updateImage((WheelReward) null);
      this.rewardDescription.Text = "";
      this.updateSpinButton();
      GameEngine.Instance.World.useTickets(this.m_wheelType, 1);
      this.numTicketsLabel.Text = SK.Text("WheelPanel_Spins", "Spins") + ": " + GameEngine.Instance.World.getTickets(this.m_wheelType).ToString();
    }

    private static void s_SpinTheWheelCallback(SpinTheWheel_ReturnType returnData)
    {
      if (WheelPanel.Instance == null)
        return;
      WheelPanel.Instance.spinTheWheelCallback(returnData);
    }

    private void spinTheWheelCallback(SpinTheWheel_ReturnType returnData)
    {
      if (returnData.Success && returnData.reward != null)
      {
        this.targetSegment = 0;
        this.m_storedReward = returnData.reward;
        for (int index = 0; index < 20; ++index)
        {
          if (this.m_storedReward.rewardType == this.rewards[index].rewardType && this.m_storedReward.rewardAmount == this.rewards[index].rewardAmount)
          {
            this.targetSegment = index;
            break;
          }
        }
        this.spinMode = 2;
        this.m_cardAdded = returnData.cardAdded;
      }
      else
      {
        this.spinMode = -1;
        this.pointerRotateSpeed = 0.0f;
        GameEngine.Instance.World.addTickets(this.m_wheelType, 1);
        this.numTicketsLabel.Text = SK.Text("WheelPanel_Spins", "Spins") + ": " + GameEngine.Instance.World.getTickets(this.m_wheelType).ToString();
        this.updateSpinButton();
      }
    }

    private void giveReward()
    {
      switch (this.m_storedReward.rewardType)
      {
        case 200:
          GameEngine.Instance.World.addGold((double) this.m_storedReward.rewardAmount);
          break;
        case 202:
          GameEngine.Instance.World.addFaithPoints((double) this.m_storedReward.rewardAmount);
          break;
        case 203:
          GameEngine.Instance.World.ProfileCardpoints += this.m_storedReward.rewardAmount;
          break;
        case 204:
          GameEngine.Instance.cardPackManager.addCardPack(1, this.m_storedReward.rewardAmount);
          break;
        case 205:
          GameEngine.Instance.cardPackManager.addCardPack(4, this.m_storedReward.rewardAmount);
          break;
        case 206:
          GameEngine.Instance.cardPackManager.addCardPack(27, this.m_storedReward.rewardAmount);
          break;
        case 207:
          GameEngine.Instance.cardPackManager.addCardPack(3, this.m_storedReward.rewardAmount);
          break;
        case 208:
          GameEngine.Instance.cardPackManager.addCardPack(28, this.m_storedReward.rewardAmount);
          break;
        case 209:
          GameEngine.Instance.World.addResearchPoints(this.m_storedReward.rewardAmount);
          break;
        case 210:
          GameEngine.Instance.World.addTickets(this.m_wheelType, this.m_storedReward.rewardAmount);
          this.numTicketsLabel.Text = SK.Text("WheelPanel_Spins", "Spins") + ": " + GameEngine.Instance.World.getTickets(this.m_wheelType).ToString();
          break;
        case 211:
          GameEngine.Instance.cardPackManager.addCardPack(1, this.m_storedReward.rewardAmount);
          GameEngine.Instance.cardPackManager.addCardPack(3, this.m_storedReward.rewardAmount);
          GameEngine.Instance.cardPackManager.addCardPack(4, this.m_storedReward.rewardAmount);
          GameEngine.Instance.cardPackManager.addCardPack(27, this.m_storedReward.rewardAmount);
          GameEngine.Instance.cardPackManager.addCardPack(28, this.m_storedReward.rewardAmount);
          break;
        case 212:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3077));
            break;
          }
          break;
        case 213:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3114));
            break;
          }
          break;
        case 214:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3086));
            break;
          }
          break;
        case 215:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3161));
            break;
          }
          break;
        case 216:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3090));
            break;
          }
          break;
        case 217:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3118));
            break;
          }
          break;
        case 218:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3169));
            break;
          }
          break;
        case 219:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3165));
            break;
          }
          break;
        case 220:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3078));
            break;
          }
          break;
        case 221:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3079));
            break;
          }
          break;
        case 223:
          GameEngine.Instance.cardPackManager.addCardPack(49, this.m_storedReward.rewardAmount);
          break;
        case 224:
          GameEngine.Instance.cardPackManager.addCardPack(50, this.m_storedReward.rewardAmount);
          break;
        case 225:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3264));
            break;
          }
          break;
        case 226:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3201));
            break;
          }
          break;
        case 227:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3115));
            break;
          }
          break;
        case 228:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3091));
            break;
          }
          break;
        case 229:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3121));
            break;
          }
          break;
        case 230:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(257));
            break;
          }
          break;
        case 231:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3119));
            break;
          }
          break;
        case 232:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3269));
            break;
          }
          break;
        case 233:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(258));
            break;
          }
          break;
        case 234:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3202));
            break;
          }
          break;
        case 235:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3270));
            break;
          }
          break;
        case 236:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(2307));
            break;
          }
          break;
        case 237:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3273));
            break;
          }
          break;
        case 238:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3277));
            break;
          }
          break;
        case 239:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(259));
            break;
          }
          break;
        case 240:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3271));
            break;
          }
          break;
        case 241:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3203));
            break;
          }
          break;
        case 242:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3274));
            break;
          }
          break;
        case 243:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3268));
            break;
          }
          break;
        case 244:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3031));
            break;
          }
          break;
        case 245:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3123));
            break;
          }
          break;
        case 246:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3032));
            break;
          }
          break;
        case 247:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(2690));
            break;
          }
          break;
        case 248:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(2696));
            break;
          }
          break;
        case 250:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(2946));
            break;
          }
          break;
        case 251:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(2965));
            break;
          }
          break;
        case 252:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3278));
            break;
          }
          break;
        case 253:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3045));
            break;
          }
          break;
        case 254:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(2947));
            break;
          }
          break;
        case (int) byte.MaxValue:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(3283));
            break;
          }
          break;
        case 256:
          if (this.m_cardAdded >= 0)
          {
            GameEngine.Instance.cardsManager.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(2072));
            break;
          }
          break;
      }
      this.m_storedReward = (WheelReward) null;
      this.updateSpinButton();
    }

    public Point rotatePoint(Point pos, float angle)
    {
      double num1 = (double) angle * -0.0174533;
      double num2 = Math.Cos(num1);
      double num3 = Math.Sin(num1);
      double x = (double) pos.X;
      double y = (double) pos.Y;
      return new Point((int) (x * num2 - y * num3), (int) (x * num3 + y * num2));
    }

    public class RewardImage : CustomSelfDrawPanel.CSDControl
    {
      private const float FADE_MAX = 10f;
      private CustomSelfDrawPanel.CSDImage glowImage = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage iconImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage numberImage = new CustomSelfDrawPanel.CSDImage();
      private WheelReward m_reward;
      private float fadeValue;

      public void init(WheelReward reward, Point position)
      {
        this.init(reward, position, (CustomSelfDrawPanel.CSDArea) null, (CustomSelfDrawPanel.CSDArea) null, (CustomSelfDrawPanel.CSDArea) null);
      }

      public void init(
        WheelReward reward,
        Point position,
        CustomSelfDrawPanel.CSDArea layer1,
        CustomSelfDrawPanel.CSDArea layer2,
        CustomSelfDrawPanel.CSDArea layer3)
      {
        this.fadeValue = 0.0f;
        this.m_reward = reward;
        this.Position = new Point(position.X - 64, position.Y - 64);
        this.Size = new Size(128, 128);
        this.glowImage.Image = (Image) GFXLibrary.wheel_icons[0];
        if (layer1 != null)
        {
          this.glowImage.Position = this.Position;
          this.glowImage.Alpha = 0.0f;
          layer1.addControl((CustomSelfDrawPanel.CSDControl) this.glowImage);
          this.updateImage(reward);
          this.iconImage.Position = new Point(this.Position.X + 2, this.Position.Y + 2);
          layer2.addControl((CustomSelfDrawPanel.CSDControl) this.iconImage);
          this.numberImage.Position = new Point(this.Position.X + 13, this.Position.Y + 75);
          layer3.addControl((CustomSelfDrawPanel.CSDControl) this.numberImage);
        }
        else
        {
          this.glowImage.Alpha = 0.0f;
          this.addControl((CustomSelfDrawPanel.CSDControl) this.glowImage);
          this.updateImage(reward);
          this.iconImage.Position = new Point(2, 2);
          this.addControl((CustomSelfDrawPanel.CSDControl) this.iconImage);
          this.numberImage.Position = new Point(13, 75);
          this.addControl((CustomSelfDrawPanel.CSDControl) this.numberImage);
        }
      }

      public void updateImage(WheelReward reward)
      {
        this.m_reward = reward;
        this.numberImage.Visible = false;
        if (reward != null)
        {
          bool flag1 = false;
          bool flag2 = true;
          switch (reward.rewardType)
          {
            case 200:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[5];
              break;
            case 202:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[4];
              break;
            case 203:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[1];
              break;
            case 204:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[10];
              flag1 = true;
              break;
            case 205:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[8];
              flag1 = true;
              break;
            case 206:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[6];
              flag1 = true;
              break;
            case 207:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[9];
              flag1 = true;
              break;
            case 208:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[7];
              flag1 = true;
              break;
            case 209:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[2];
              break;
            case 210:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[3];
              flag2 = false;
              break;
            case 211:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[11];
              flag1 = true;
              break;
            case 212:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[26];
              flag2 = false;
              break;
            case 213:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[17];
              flag2 = false;
              break;
            case 214:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[19];
              flag2 = false;
              break;
            case 215:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[16];
              flag2 = false;
              break;
            case 216:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[20];
              flag2 = false;
              break;
            case 217:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[18];
              flag2 = false;
              break;
            case 218:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[21];
              flag2 = false;
              break;
            case 219:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[22];
              flag2 = false;
              break;
            case 220:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[27];
              flag2 = false;
              break;
            case 221:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[28];
              flag2 = false;
              break;
            case 222:
              this.iconImage.Image = (Image) null;
              this.numberImage.Visible = false;
              flag2 = false;
              break;
            case 223:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[30];
              flag1 = true;
              break;
            case 224:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[29];
              flag1 = true;
              break;
            case 225:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[31];
              flag2 = false;
              break;
            case 226:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[32];
              flag2 = false;
              break;
            case 227:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[33];
              flag2 = false;
              break;
            case 228:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[34];
              flag2 = false;
              break;
            case 229:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[35];
              flag2 = false;
              break;
            case 230:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[36];
              flag2 = false;
              break;
            case 231:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[37];
              flag2 = false;
              break;
            case 232:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[38];
              flag2 = false;
              break;
            case 233:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[39];
              flag2 = false;
              break;
            case 234:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[40];
              flag2 = false;
              break;
            case 235:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[41];
              flag2 = false;
              break;
            case 236:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[42];
              flag2 = false;
              break;
            case 237:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[43];
              flag2 = false;
              break;
            case 238:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[44];
              flag2 = false;
              break;
            case 239:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[45];
              flag2 = false;
              break;
            case 240:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[46];
              flag2 = false;
              break;
            case 241:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[47];
              flag2 = false;
              break;
            case 242:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[48];
              flag2 = false;
              break;
            case 243:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[49];
              flag2 = false;
              break;
            case 244:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[50];
              flag2 = false;
              break;
            case 245:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[51];
              flag2 = false;
              break;
            case 246:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[52];
              flag2 = false;
              break;
            case 247:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[53];
              flag2 = false;
              break;
            case 248:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[54];
              flag2 = false;
              break;
            case 250:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[55];
              flag2 = false;
              break;
            case 251:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[56];
              flag2 = false;
              break;
            case 252:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[57];
              flag2 = false;
              break;
            case 253:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[58];
              flag2 = false;
              break;
            case 254:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[59];
              flag2 = false;
              break;
            case (int) byte.MaxValue:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[60];
              flag2 = false;
              break;
            case 256:
              this.iconImage.Image = (Image) GFXLibrary.wheel_icons[61];
              flag2 = false;
              break;
          }
          if (flag2)
          {
            int index = -1;
            if (flag1)
            {
              switch (reward.rewardAmount)
              {
                case 1:
                  index = 28;
                  break;
                case 2:
                  index = 27;
                  break;
                case 3:
                  index = 26;
                  break;
                case 4:
                  index = 25;
                  break;
                case 5:
                  index = 24;
                  break;
                case 10:
                  index = 23;
                  break;
              }
            }
            else
            {
              switch (reward.rewardAmount)
              {
                case 1:
                  index = 22;
                  break;
                case 2:
                  index = 21;
                  break;
                case 10:
                  index = 20;
                  break;
                case 20:
                  index = 31;
                  break;
                case 25:
                  index = 19;
                  break;
                case 35:
                  index = 18;
                  break;
                case 40:
                  index = 32;
                  break;
                case 50:
                  index = 17;
                  break;
                case 75:
                  index = 16;
                  break;
                case 80:
                  index = 33;
                  break;
                case 100:
                  index = 15;
                  break;
                case 150:
                  index = 14;
                  break;
                case 200:
                  index = 12;
                  break;
                case 250:
                  index = 13;
                  break;
                case 400:
                  index = 11;
                  break;
                case 500:
                  index = 10;
                  break;
                case 1000:
                  index = 9;
                  break;
                case 2000:
                  index = 8;
                  break;
                case 5000:
                  index = 7;
                  break;
                case 10000:
                  index = 6;
                  break;
                case 15000:
                  index = 29;
                  break;
                case 20000:
                  index = 5;
                  break;
                case 25000:
                  index = 4;
                  break;
                case 30000:
                  index = 30;
                  break;
                case 40000:
                  index = 34;
                  break;
                case 50000:
                  index = 3;
                  break;
                case 100000:
                  index = 2;
                  break;
                case 150000:
                  index = 35;
                  break;
                case 250000:
                  index = 1;
                  break;
                case 500000:
                  index = 0;
                  break;
              }
            }
            if (index >= 0)
            {
              this.numberImage.Visible = true;
              this.numberImage.Image = (Image) GFXLibrary.wheel_numbers[index];
            }
            else
              this.numberImage.Visible = false;
          }
        }
        else
        {
          this.iconImage.Image = (Image) null;
          this.numberImage.Visible = false;
        }
        this.invalidate();
      }

      public void fixedHighlight()
      {
        this.fadeValue = 0.0f;
        this.glowImage.Alpha = 1f;
        this.invalidate();
      }

      public void highlight()
      {
        this.fadeValue = 10f;
        this.glowImage.Alpha = 1f;
        this.invalidate();
      }

      public bool update()
      {
        if ((double) this.fadeValue > 0.0)
        {
          --this.fadeValue;
          if ((double) this.fadeValue < 0.0)
            this.fadeValue = 0.0f;
          this.glowImage.Alpha = this.fadeValue / 10f;
          this.invalidate();
        }
        return true;
      }
    }
  }
}
