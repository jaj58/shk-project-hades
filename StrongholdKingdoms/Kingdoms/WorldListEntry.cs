// Decompiled with JetBrains decompiler
// Type: Kingdoms.WorldListEntry
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class WorldListEntry : CustomSelfDrawPanel.CSDControl
  {
    private WorldInfo m_Info;
    private Font WebTextFont = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-Bold.ttf", 40f, FontStyle.Regular);
    private CustomSelfDrawPanel.CSDImage lblWorldFlag = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage imgWorldMap = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel lblWorldStatus = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage backGround = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage imgWorldName = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage secondAgeImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton btnWorldAction = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton btnWorldInfo = new CustomSelfDrawPanel.CSDButton();

    public WorldInfo Info => this.m_Info;

    public void Init(WorldInfo info, bool isDark, WorldSelectPopupPanel parentPanel)
    {
      DateTime dateTime = new DateTime(2019, 7, 4, 15, 0, 0);
      this.Width = 80;
      this.Height = 24;
      this.m_Info = info;
      this.backGround.Image = (Image) (isDark ? GFXLibrary.lineitem_strip_02_dark : GFXLibrary.lineitem_strip_02_light);
      this.backGround.Colorise = this.m_Info.Online ? ARGBColors.Green : ARGBColors.Red;
      this.backGround.setSizeToImage();
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround);
      this.imgWorldMap.Image = (Image) GFXLibrary.getLoginWorldMap(this.m_Info.MapCulture);
      this.imgWorldMap.setSizeToImage();
      this.imgWorldMap.X = 17;
      this.imgWorldMap.Y = 4;
      this.imgWorldMap.CustomTooltipID = this.GetMapTooltipID();
      this.backGround.addControl((CustomSelfDrawPanel.CSDControl) this.imgWorldMap);
      this.imgWorldName.Image = WebStyleButtonImage.Generate(200, this.backGround.Image.Height + 10, ProfileLoginWindow.getWorldShortDesc(this.m_Info), this.WebTextFont, ARGBColors.Black, ARGBColors.Transparent, 0);
      this.imgWorldName.setSizeToImage();
      this.imgWorldName.Y = this.backGround.Image.Height / 2 - this.imgWorldName.Height / 2;
      this.imgWorldName.X = 50;
      this.backGround.addControl((CustomSelfDrawPanel.CSDControl) this.imgWorldName);
      this.lblWorldFlag.X = this.imgWorldName.Rectangle.Right + 50;
      this.lblWorldFlag.Image = (Image) GFXLibrary.getLoginWorldFlag(this.m_Info.Supportculture);
      this.lblWorldFlag.Size = this.lblWorldFlag.Image.Size;
      this.lblWorldFlag.Y = 7;
      this.lblWorldFlag.CustomTooltipID = this.GetFlagTooltipID();
      this.lblWorldStatus.Width = 121;
      this.lblWorldStatus.Height = this.backGround.Image.Height;
      this.lblWorldStatus.Y = 0;
      this.lblWorldStatus.X = 529;
      this.lblWorldStatus.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
      if (this.m_Info.Online)
      {
        this.btnWorldAction.Width = this.Width;
        this.btnWorldAction.Height = this.Height;
        this.btnWorldAction.Y = 5;
        this.btnWorldAction.Tag = (object) this.m_Info;
        this.btnWorldAction.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(parentPanel.btnWorldAction_Click), "WorldSelectPopupPanel_world_select");
        if (this.m_Info.Playing)
        {
          this.btnWorldAction.ImageNorm = parentPanel.PlayImage;
          this.btnWorldAction.ImageOver = parentPanel.PlayImageOver;
        }
        else if (this.m_Info.AvailableToJoin)
        {
          this.btnWorldAction.ImageNorm = parentPanel.JoinImage;
          this.btnWorldAction.ImageOver = parentPanel.JoinImageOver;
        }
        else
        {
          this.btnWorldAction.ImageNorm = parentPanel.ClosedImage;
          this.btnWorldAction.ImageOver = parentPanel.ClosedImage;
          this.btnWorldAction.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
          this.btnWorldAction.Active = false;
        }
        this.btnWorldAction.Width = this.btnWorldAction.ImageNorm.Width;
        this.btnWorldAction.Height = this.btnWorldAction.ImageNorm.Height;
        this.btnWorldAction.X = 650 - this.btnWorldAction.Width;
        this.backGround.addControl((CustomSelfDrawPanel.CSDControl) this.btnWorldAction);
        if (this.btnWorldAction.Active)
        {
          if (this.m_Info.KingdomsWorldID % 100 < 50)
          {
            this.btnWorldInfo.ImageNorm = (Image) GFXLibrary.help_normal;
            this.btnWorldInfo.ImageOver = (Image) GFXLibrary.help_over;
            this.btnWorldInfo.ImageClick = (Image) GFXLibrary.help_pushed;
          }
          else
          {
            this.btnWorldInfo.ImageNorm = (Image) GFXLibrary.help_gold_normal;
            this.btnWorldInfo.ImageOver = (Image) GFXLibrary.help_gold_over;
            this.btnWorldInfo.ImageClick = (Image) GFXLibrary.help_gold_pushed;
          }
          this.btnWorldInfo.Position = new Point(490, 8);
          this.btnWorldInfo.Data = this.m_Info.KingdomsWorldID;
          this.btnWorldInfo.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(parentPanel.infoOverlayOpenedClick));
          this.backGround.addControl((CustomSelfDrawPanel.CSDControl) this.btnWorldInfo);
        }
        this.lblWorldStatus.CustomTooltipID = 4010;
      }
      else
      {
        if (this.m_Info.KingdomsWorldID == 2550 && DateTime.UtcNow > dateTime)
        {
          this.lblWorldStatus.Text = SK.Text("WorldEnded", "This World has ended.");
          this.lblWorldStatus.Width = 300;
        }
        else
        {
          this.lblWorldStatus.Text = SK.Text("WORLD_Offline", "Offline");
          this.lblWorldStatus.Color = ARGBColors.Red;
        }
        this.lblWorldStatus.CustomTooltipID = 4009;
        this.lblWorldStatus.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.backGround.addControl((CustomSelfDrawPanel.CSDControl) this.lblWorldStatus);
      }
      this.backGround.addControl((CustomSelfDrawPanel.CSDControl) this.imgWorldMap);
    }

    private int GetFlagTooltipID()
    {
      switch (this.m_Info.Supportculture)
      {
        case "en":
          return 4001;
        case "de":
          return 4002;
        case "fr":
          return 4003;
        case "ru":
          return 4004;
        case "es":
          return 4016;
        case "pl":
        case "pl=":
          return 4020;
        case "tr":
          return 4023;
        case "it":
          return 4027;
        case "pt":
          return 4035;
        case "eu":
          return 4031;
        case "zh":
          return 4046;
        default:
          return 4041;
      }
    }

    private int GetMapTooltipID()
    {
      switch (this.m_Info.MapCulture)
      {
        case "en":
          return 4005;
        case "de":
          return 4006;
        case "fr":
          return 4007;
        case "ru":
          return 4008;
        case "es":
          return 4017;
        case "pl":
          return 4021;
        case "tr":
          return 4024;
        case "it":
          return 4028;
        case "us":
          return 4030;
        case "eu":
          return 4032;
        case "pt":
          return 4036;
        case "ph":
          return 4043;
        case "zh":
          return 4047;
        case "kg":
          return 4049;
        case "jp":
          return 4050;
        case "hy":
          return 4051;
        case "vk":
          return 4052;
        case "gd":
          return 4053;
        case "cru":
          return 4054;
        case "sp":
          return 4055;
        default:
          return 4042;
      }
    }
  }
}
