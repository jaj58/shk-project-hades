// Decompiled with JetBrains decompiler
// Type: Kingdoms.MainRightHandPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using Kingdoms.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MainRightHandPanel : UserControl, IDockWindow
  {
    private DockWindow dockWindow;
    private IContainer components;

    public void AddControl(UserControl control, int x, int y)
    {
      this.dockWindow.AddControl(control, x, y);
    }

    public void RemoveControl(UserControl control) => this.dockWindow.RemoveControl(control);

    public MainRightHandPanel()
    {
      this.dockWindow = new DockWindow((ContainerControl) this);
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public static CustomSelfDrawPanel.CSDButton getMRHPButton(
      MainRightHandPanel.MRHPButton buttonType)
    {
      CustomSelfDrawPanel.CSDButton mrhpButton = new CustomSelfDrawPanel.CSDButton();
      switch (buttonType)
      {
        case MainRightHandPanel.MRHPButton.LAST_REPORT:
          mrhpButton.ImageNorm = (Image) GFXLibrary.mrhp_reports;
          mrhpButton.OverBrighten = true;
          mrhpButton.MoveOnClick = true;
          break;
        case MainRightHandPanel.MRHPButton.ATTACK:
          mrhpButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[1];
          mrhpButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[8];
          mrhpButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[15];
          break;
        case MainRightHandPanel.MRHPButton.REINFORCE:
          mrhpButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[2];
          mrhpButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[9];
          mrhpButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[16];
          break;
        case MainRightHandPanel.MRHPButton.SCOUT:
          mrhpButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[3];
          mrhpButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[10];
          mrhpButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[17];
          break;
        case MainRightHandPanel.MRHPButton.MONK:
          mrhpButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[4];
          mrhpButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[11];
          mrhpButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[18];
          break;
        case MainRightHandPanel.MRHPButton.VASSAL:
          mrhpButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[5];
          mrhpButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[12];
          mrhpButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[19];
          break;
        case MainRightHandPanel.MRHPButton.TRADE:
          mrhpButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0];
          mrhpButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[7];
          mrhpButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[14];
          break;
      }
      return mrhpButton;
    }

    public const int BOT_TOOLTIP_ATTACK = 11111131;
    public const int BOT_TOOLTIP_EXCOM = 11111132;
    public const int BOT_TOOLTIP_ABSOLUTION = 11111133;
    public const int BOT_TOOLTIP_VILLAGE_RADAR = 11111134;

    // The bot's attack button reuses the stock attack icon, which is identical to the real attack
    // button sitting next to it. Rotating the hue turns the blue disc red while leaving the white
    // axe heads white - the same glyph-in-another-colour trick the game's own art uses for the
    // green reinforce variant. 140 degrees lands on red; lower goes pink, higher goes orange.
    private const double BOT_ICON_HUE_DEGREES = 140.0;
    // The village radar button reuses the same attack icon again, so it needs its own hue to
    // read as a different button: further round than the bot attack red, landing on orange.
    private const double RADAR_ICON_HUE_DEGREES = 185.0;
    private static Image botAttackNorm;
    private static Image botAttackOver;
    private static Image botAttackClick;
    private static Image radarNorm;
    private static Image radarOver;
    private static Image radarClick;

    // Bot-added buttons get a tooltip naming the module they drive. The monk buttons already use
    // their own artwork, so they only need this; the attack button also gets recoloured below.
    public static void markAsBotButton(
      CustomSelfDrawPanel.CSDButton button,
      int tooltipID)
    {
      button.CustomTooltipID = tooltipID;
    }

    public static CustomSelfDrawPanel.CSDButton getBotAttackButton()
    {
      CustomSelfDrawPanel.CSDButton mrhpButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.ATTACK);
      if (MainRightHandPanel.botAttackNorm == null)
      {
        Image image = MainRightHandPanel.recolourBotIcon(mrhpButton.ImageNorm, MainRightHandPanel.BOT_ICON_HUE_DEGREES);
        // The atlas images load lazily, so don't cache a placeholder - fall back to the stock icon.
        if (image != null && image.Width > 1)
        {
          MainRightHandPanel.botAttackNorm = image;
          MainRightHandPanel.botAttackOver = MainRightHandPanel.recolourBotIcon(mrhpButton.ImageOver, MainRightHandPanel.BOT_ICON_HUE_DEGREES);
          MainRightHandPanel.botAttackClick = MainRightHandPanel.recolourBotIcon(mrhpButton.ImageClick, MainRightHandPanel.BOT_ICON_HUE_DEGREES);
        }
      }
      if (MainRightHandPanel.botAttackNorm != null)
      {
        mrhpButton.ImageNorm = MainRightHandPanel.botAttackNorm;
        mrhpButton.ImageOver = MainRightHandPanel.botAttackOver;
        mrhpButton.ImageClick = MainRightHandPanel.botAttackClick;
      }
      MainRightHandPanel.markAsBotButton(mrhpButton, MainRightHandPanel.BOT_TOOLTIP_ATTACK);
      return mrhpButton;
    }

    // Opens the Village Radar window for the selected village - same attack glyph, orange.
    public static CustomSelfDrawPanel.CSDButton getVillageRadarButton()
    {
      CustomSelfDrawPanel.CSDButton mrhpButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.ATTACK);
      if (MainRightHandPanel.radarNorm == null)
      {
        Image image = MainRightHandPanel.recolourBotIcon(mrhpButton.ImageNorm, MainRightHandPanel.RADAR_ICON_HUE_DEGREES);
        // The atlas images load lazily, so don't cache a placeholder - fall back to the stock icon.
        if (image != null && image.Width > 1)
        {
          MainRightHandPanel.radarNorm = image;
          MainRightHandPanel.radarOver = MainRightHandPanel.recolourBotIcon(mrhpButton.ImageOver, MainRightHandPanel.RADAR_ICON_HUE_DEGREES);
          MainRightHandPanel.radarClick = MainRightHandPanel.recolourBotIcon(mrhpButton.ImageClick, MainRightHandPanel.RADAR_ICON_HUE_DEGREES);
        }
      }
      if (MainRightHandPanel.radarNorm != null)
      {
        mrhpButton.ImageNorm = MainRightHandPanel.radarNorm;
        mrhpButton.ImageOver = MainRightHandPanel.radarOver;
        mrhpButton.ImageClick = MainRightHandPanel.radarClick;
      }
      MainRightHandPanel.markAsBotButton(mrhpButton, MainRightHandPanel.BOT_TOOLTIP_VILLAGE_RADAR);
      return mrhpButton;
    }

    private static Image recolourBotIcon(Image source, double hueDegrees)
    {
      if (source == null || source.Width <= 1 || source.Height <= 1)
        return source;
      float c = (float) Math.Cos(hueDegrees * Math.PI / 180.0);
      float s = (float) Math.Sin(hueDegrees * Math.PI / 180.0);
      ColorMatrix colorMatrix = new ColorMatrix(new float[5][]
      {
        new float[5] { 0.213f + c * 0.787f - s * 0.213f, 0.213f - c * 0.213f + s * 0.143f, 0.213f - c * 0.213f - s * 0.787f, 0.0f, 0.0f },
        new float[5] { 0.715f - c * 0.715f - s * 0.715f, 0.715f + c * 0.285f + s * 0.140f, 0.715f - c * 0.715f + s * 0.715f, 0.0f, 0.0f },
        new float[5] { 0.072f - c * 0.072f + s * 0.928f, 0.072f - c * 0.072f - s * 0.283f, 0.072f + c * 0.928f + s * 0.072f, 0.0f, 0.0f },
        new float[5] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f },
        new float[5] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f }
      });
      Bitmap bitmap = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        using (ImageAttributes imageAttr = new ImageAttributes())
        {
          imageAttr.SetColorMatrix(colorMatrix);
          graphics.DrawImage(source, new Rectangle(0, 0, source.Width, source.Height), 0, 0, source.Width, source.Height, GraphicsUnit.Pixel, imageAttr);
        }
      }
      return (Image) bitmap;
    }


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
      this.BackColor = ARGBColors.Black;
      this.BackgroundImage = (Image) Resources.right_side_panel_large_stone_tan;
      this.Name = nameof (MainRightHandPanel);
      this.Size = new Size(200, 566);
      this.ResumeLayout(false);
    }

    public enum MRHPButton
    {
      LAST_REPORT,
      ATTACK,
      REINFORCE,
      SCOUT,
      MONK,
      VASSAL,
      TRADE,
    }
  }
}
