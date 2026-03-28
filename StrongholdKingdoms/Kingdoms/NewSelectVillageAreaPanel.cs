// Decompiled with JetBrains decompiler
// Type: Kingdoms.NewSelectVillageAreaPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class NewSelectVillageAreaPanel : CustomSelfDrawPanel
  {
    private CustomSelfDrawPanel.CSDExtendingPanel background = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDArea backgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDFill transparentBackground = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage mapImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDRectangle mapBorder = new CustomSelfDrawPanel.CSDRectangle();
    private CustomSelfDrawPanel.CSDButton btnEnterGame = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton btnBack = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton btnLogout = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel loadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel headerLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lostMessageLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage lowImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage medImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage highImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel populationLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lowLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel medLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel highLabel = new CustomSelfDrawPanel.CSDLabel();
    private float divider = 5f;
    private NewSelectVillageAreaWindow m_parent;
    private bool smallButtons;
    private int selectedCounty = -1;
    private JoiningWorldPopup m_popup;
    private int retries;
    private DateTime delayedRetry = DateTime.MinValue;
    private IContainer components;

    public NewSelectVillageAreaPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(int tryingToJoinCounty, NewSelectVillageAreaWindow parent)
    {
      this.m_parent = parent;
      this.clearControls();
      this.transparentBackground.Size = this.Size;
      this.transparentBackground.FillColor = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.transparentBackground);
      this.background.Position = new Point(0, 0);
      this.background.Size = new Size(this.Width, this.Height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
      this.background.Create((Image) GFXLibrary._9sclice_fancy_top_left, (Image) GFXLibrary._9sclice_fancy_top_mid, (Image) GFXLibrary._9sclice_fancy_top_right, (Image) GFXLibrary._9sclice_fancy_mid_left, (Image) GFXLibrary._9sclice_fancy_mid_mid, (Image) GFXLibrary._9sclice_fancy_mid_right, (Image) GFXLibrary._9sclice_fancy_bottom_left, (Image) GFXLibrary._9sclice_fancy_bottom_mid, (Image) GFXLibrary._9sclice_fancy_bottom_right);
      this.background.ForceTiling();
      this.backgroundArea.Position = new Point(206, 53);
      this.backgroundArea.Size = new Size(514, 340);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundArea);
      this.smallButtons = false;
      int y = 0;
      int x = 0;
      int num1 = 0;
      this.divider = 5f;
      switch (GameEngine.Instance.World.WorldMapType)
      {
        case 1:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_de;
          break;
        case 3:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_fr;
          break;
        case 4:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_ru;
          break;
        case 5:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_sa;
          this.divider = 6f;
          y = 10;
          break;
        case 6:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_es;
          this.divider = 5.5f;
          y = 104;
          break;
        case 7:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_pl;
          y = 50;
          break;
        case 8:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_eu;
          y = 66;
          this.divider = 5.5f;
          break;
        case 9:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_tr;
          this.divider = 5.5f;
          y = 190;
          break;
        case 10:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_us;
          this.divider = 5.5f;
          y = 130;
          break;
        case 11:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_it;
          y = 85;
          break;
        case 16:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_world;
          y = 45;
          x = -67;
          num1 = -52;
          this.smallButtons = true;
          break;
        case 17:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_ph;
          x = 50;
          this.divider = 6.7f;
          break;
        case 19:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_china;
          y = 15;
          x = -67;
          num1 = -52;
          this.smallButtons = true;
          break;
        case 23:
        case 24:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_kingmaker;
          y = 65;
          x = -32;
          this.divider = 5.5f;
          num1 = -52;
          this.smallButtons = true;
          break;
        case 25:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_jp;
          y = 80;
          x = 50;
          this.divider = 6.7f;
          break;
        case 26:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_hyw;
          y = 33;
          x = 68;
          this.divider = 5.5f;
          this.smallButtons = true;
          break;
        case 27:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_vk;
          y = 60;
          x = 35;
          break;
        case 28:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_tgd;
          y = 60;
          x = 35;
          break;
        case 29:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_cru;
          y = 60;
          x = 35;
          this.divider = 6.7f;
          break;
        case 30:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_sparta;
          y = 60;
          x = 35;
          this.divider = 5.5f;
          break;
        default:
          this.mapImage.Image = (Image) GFXLibrary.world_select_map_en;
          break;
      }
      this.mapImage.Position = new Point(x, y);
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.mapImage);
      this.mapBorder.Position = new Point(x, y);
      this.mapBorder.Size = new Size(this.mapImage.Width, this.mapImage.Height);
      this.mapBorder.LineColor = ARGBColors.Black;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.mapBorder);
      this.btnEnterGame.ImageNorm = (Image) GFXLibrary.worldSelect_swap_norm;
      this.btnEnterGame.ImageOver = (Image) GFXLibrary.worldSelect_swap_over;
      this.btnEnterGame.ImageClick = (Image) GFXLibrary.worldSelect_swap_pushed;
      this.btnEnterGame.Position = new Point(565, 60);
      this.btnEnterGame.Text.Text = SK.Text("SelectVillageAreaPopup_Enter_Game", "Enter Game");
      this.btnEnterGame.TextYOffset = -2;
      this.btnEnterGame.Text.Color = ARGBColors.White;
      this.btnEnterGame.Text.DropShadowColor = ARGBColors.Black;
      this.btnEnterGame.Text.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
      this.btnEnterGame.Text.Position = new Point(-3, 0);
      this.btnEnterGame.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnEnterGame_Click));
      this.btnEnterGame.Enabled = false;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.btnEnterGame);
      this.btnBack.ImageNorm = (Image) GFXLibrary.worldSelect_swap_norm;
      this.btnBack.ImageOver = (Image) GFXLibrary.worldSelect_swap_over;
      this.btnBack.ImageClick = (Image) GFXLibrary.worldSelect_swap_pushed;
      this.btnBack.Position = new Point(565 + num1, 540);
      this.btnBack.Text.Text = SK.Text("FORUMS_Back", "Back");
      this.btnBack.TextYOffset = -2;
      this.btnBack.Text.Color = ARGBColors.White;
      this.btnBack.Text.DropShadowColor = ARGBColors.Black;
      this.btnBack.Text.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
      this.btnBack.Text.Position = new Point(-3, 0);
      this.btnBack.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnBack_Click));
      this.btnBack.Enabled = true;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.btnBack);
      this.btnLogout.ImageNorm = (Image) GFXLibrary.worldSelect_swap_norm;
      this.btnLogout.ImageOver = (Image) GFXLibrary.worldSelect_swap_over;
      this.btnLogout.ImageClick = (Image) GFXLibrary.worldSelect_swap_pushed;
      this.btnLogout.Position = new Point(565 + num1, 500);
      this.btnLogout.Text.Text = SK.Text("LogoutPanel_Swap_Worlds", "Swap Worlds");
      this.btnLogout.TextYOffset = -2;
      this.btnLogout.Text.Color = ARGBColors.White;
      this.btnLogout.Text.DropShadowColor = ARGBColors.Black;
      this.btnLogout.Text.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
      this.btnLogout.Text.Position = new Point(-3, 0);
      this.btnLogout.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.logoutClick));
      this.btnLogout.Enabled = true;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.btnLogout);
      this.headerLabel.Text = SK.Text("SelectVillageAreaPopup_Select_Village_Location", "Select Village Location");
      this.headerLabel.Position = new Point(0, 1);
      this.headerLabel.Size = new Size(this.background.Width, 150);
      this.headerLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.headerLabel.Color = ARGBColors.Black;
      this.headerLabel.DropShadowColor = ARGBColors.LightGray;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabel);
      this.loadingLabel.Text = SK.Text("SelectVillageAreaPopup_Downloading", "Downloading") + " .....";
      this.loadingLabel.Position = new Point(this.btnEnterGame.Position.X + this.btnEnterGame.Width / 2 - 100, this.btnEnterGame.Position.Y + 50);
      this.loadingLabel.Size = new Size(200, 200);
      this.loadingLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.loadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.loadingLabel.Color = ARGBColors.Black;
      this.loadingLabel.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.loadingLabel);
      this.populationLabel.Text = SK.Text("SelectVillagePopup_Population", "Population");
      this.populationLabel.Position = new Point(574, 245);
      this.populationLabel.Size = new Size(150, 30);
      this.populationLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.populationLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.populationLabel.Color = ARGBColors.Black;
      this.populationLabel.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.populationLabel);
      this.lowImage.Image = (Image) GFXLibrary.selector_square_normal;
      this.lowImage.Position = new Point(574, 270);
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.lowImage);
      this.lowLabel.Text = SK.Text("SelectVillagePopup_Low", "Low");
      this.lowLabel.Position = new Point(594, 270);
      this.lowLabel.Size = new Size(150, 30);
      this.lowLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
      this.lowLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lowLabel.Color = ARGBColors.Black;
      this.lowLabel.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.lowLabel);
      this.medImage.Image = (Image) GFXLibrary.selector_square_orange_normal;
      this.medImage.Position = new Point(574, 295);
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.medImage);
      this.medLabel.Text = SK.Text("SelectVillagePopup_Medium", "Medium");
      this.medLabel.Position = new Point(594, 295);
      this.medLabel.Size = new Size(150, 30);
      this.medLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
      this.medLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.medLabel.Color = ARGBColors.Black;
      this.medLabel.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.medLabel);
      this.highImage.Image = (Image) GFXLibrary.selector_square_red_normal;
      this.highImage.Position = new Point(574, 320);
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.highImage);
      this.highLabel.Text = SK.Text("SelectVillagePopup_High", "High");
      this.highLabel.Position = new Point(594, 320);
      this.highLabel.Size = new Size(150, 30);
      this.highLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
      this.highLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.highLabel.Color = ARGBColors.Black;
      this.highLabel.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.highLabel);
      if (GameEngine.Instance.World.WorldMapType == 16 || GameEngine.Instance.World.WorldMapType == 19 || GameEngine.Instance.World.WorldMapType == 23 || GameEngine.Instance.World.WorldMapType == 24 || GameEngine.Instance.World.WorldMapType == 27)
      {
        int num2 = 0;
        if (GameEngine.Instance.World.WorldMapType == 19)
        {
          num2 = 13;
          this.btnLogout.Position = new Point(565 + num1, 500 + num2);
          this.btnBack.Position = new Point(565 + num1, 540 + num2);
        }
        this.btnEnterGame.Position = new Point(65 + num1, 500 + num2);
        this.loadingLabel.Position = new Point(this.btnEnterGame.Position.X + this.btnEnterGame.Width / 2 - 100, this.btnEnterGame.Position.Y + 50 + num2);
        this.populationLabel.Position = new Point(324 + num1, 500 + num2);
        this.lowImage.Position = new Point(324 + num1, 525 + num2);
        this.lowLabel.Position = new Point(344 + num1, 525 + num2);
        this.medImage.Position = new Point(324 + num1, 550 + num2);
        this.medLabel.Position = new Point(344 + num1, 550 + num2);
        this.highImage.Position = new Point(324 + num1, 575 + num2);
        this.highLabel.Position = new Point(344 + num1, 575 + num2);
      }
      RemoteServices.Instance.set_GetVillageStartLocations_UserCallBack(new RemoteServices.GetVillageStartLocations_UserCallBack(this.GetVillageStartLocationsCallback));
      RemoteServices.Instance.GetVillageStartLocations();
      if (tryingToJoinCounty < 0)
        return;
      this.closePopup();
      this.m_popup = new JoiningWorldPopup();
      this.m_popup.init(tryingToJoinCounty, "");
      this.m_popup.Show((IWin32Window) this);
      this.btnEnterGame.Enabled = false;
      this.delayedRetry = DateTime.Now.AddSeconds(-25.0);
      GameEngine.Instance.tryingToJoinCounty = -2;
    }

    private void logoutClick()
    {
      GameEngine.Instance.playInterfaceSound("SelectVillageAreaPopup_logout");
      this.m_parent.closing = true;
      GameEngine.Instance.closeNoVillagePopup(false);
      LoggingOutPopup.open(true, false, false, false, false, false, false, 0, 100, false, false, false, false, false, false, 500, 500, 500, 500, 250);
    }

    private void btnEnterGame_Click()
    {
      if (this.selectedCounty < 0)
        return;
      GameEngine.Instance.playInterfaceSound("SelectVillageAreaPopup_enter_game");
      this.closePopup();
      this.m_popup = new JoiningWorldPopup();
      this.m_popup.init(this.selectedCounty, "");
      this.m_popup.Show((IWin32Window) this);
      this.btnEnterGame.Enabled = false;
      this.retries = 0;
      RemoteServices.Instance.set_SetStartingCounty_UserCallBack(new RemoteServices.SetStartingCounty_UserCallBack(this.SetStartingCountyCallback));
      RemoteServices.Instance.SetStartingCounty(this.selectedCounty);
    }

    public void closePopup()
    {
      if (this.m_popup == null)
        return;
      this.m_popup.Close();
      this.m_popup = (JoiningWorldPopup) null;
    }

    public void update()
    {
      if (!(this.delayedRetry != DateTime.MinValue) || (DateTime.Now - this.delayedRetry).TotalSeconds <= 30.0)
        return;
      this.delayedRetry = DateTime.MinValue;
      RemoteServices.Instance.set_SetStartingCounty_UserCallBack(new RemoteServices.SetStartingCounty_UserCallBack(this.SetStartingCountyCallback));
      RemoteServices.Instance.SetStartingCounty(-1);
    }

    public void GetVillageStartLocationsCallback(GetVillageStartLocations_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.loadingLabel.Visible = false;
      this.importCounties(returnData.availableCounties);
    }

    private void importCounties(List<int> counties)
    {
      this.selectedCounty = -1;
      if (counties == null)
        return;
      this.mapImage.clearControls();
      for (int index1 = 0; index1 < 2; ++index1)
      {
        for (int index2 = 0; index2 < counties.Count; index2 += 4)
        {
          int county1 = counties[index2];
          int county2 = counties[index2 + 1];
          int county3 = counties[index2 + 2];
          int county4 = counties[index2 + 3];
          Point countyMarkerLocation = GameEngine.Instance.World.getCountyMarkerLocation(county1);
          if (county2 == -1000)
          {
            if (index1 == 0)
            {
              CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage();
              control.Image = (Image) GFXLibrary.selector_square_red_normal;
              control.Size = new Size(control.Size.Width / 2, control.Size.Height / 2);
              control.CustomTooltipID = 1101;
              control.Position = new Point((int) ((double) countyMarkerLocation.X / (double) this.divider) - 4, (int) ((double) countyMarkerLocation.Y / (double) this.divider) - 4);
              control.Colorise = Color.FromArgb((int) byte.MaxValue, 128, 128, 128);
              this.mapImage.addControl((CustomSelfDrawPanel.CSDControl) control);
            }
          }
          else if (index1 == 1)
          {
            CustomSelfDrawPanel.CSDButton control = new CustomSelfDrawPanel.CSDButton();
            control.Position = new Point((int) ((double) countyMarkerLocation.X / (double) this.divider) - 8, (int) ((double) countyMarkerLocation.Y / (double) this.divider) - 8);
            control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconOver_Click));
            control.Data = county1;
            control.CustomTooltipID = 1100;
            this.mapImage.addControl((CustomSelfDrawPanel.CSDControl) control);
            if (county2 > county3 / 2)
            {
              control.ImageNorm = (Image) GFXLibrary.selector_square_normal;
              control.ImageOver = (Image) GFXLibrary.selector_square_over;
              control.ImageClick = (Image) GFXLibrary.selector_square_pressed;
            }
            else if (county2 > county3 / 7)
            {
              control.ImageNorm = (Image) GFXLibrary.selector_square_orange_normal;
              control.ImageOver = (Image) GFXLibrary.selector_square_orange_over;
              control.ImageClick = (Image) GFXLibrary.selector_square_orange_pressed;
            }
            else
            {
              control.ImageNorm = (Image) GFXLibrary.selector_square_red_normal;
              control.ImageOver = (Image) GFXLibrary.selector_square_red_over;
              control.ImageClick = (Image) GFXLibrary.selector_square_red_pressed;
            }
            if (this.smallButtons)
            {
              control.Position = new Point((int) ((double) countyMarkerLocation.X / (double) this.divider) - 4, (int) ((double) countyMarkerLocation.Y / (double) this.divider) - 4);
              control.Size = new Size(control.Size.Width / 2, control.Size.Height / 2);
            }
          }
        }
      }
      this.mapImage.invalidate();
    }

    private void iconOver_Click()
    {
      GameEngine.Instance.playInterfaceSound("SelectVillageAreaPopup_select_county");
      if (this.m_popup != null)
        return;
      CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) this.ClickedControl;
      if (clickedControl == null)
        return;
      foreach (CustomSelfDrawPanel.CSDControl control in this.mapImage.Controls)
      {
        if (control.GetType() == typeof (CustomSelfDrawPanel.CSDButton))
        {
          CustomSelfDrawPanel.CSDButton csdButton = (CustomSelfDrawPanel.CSDButton) control;
          if (csdButton.ImageClick == (Image) GFXLibrary.selector_square_pressed)
          {
            csdButton.ImageNorm = (Image) GFXLibrary.selector_square_normal;
            csdButton.ImageOver = (Image) GFXLibrary.selector_square_over;
          }
          else if (csdButton.ImageClick == (Image) GFXLibrary.selector_square_orange_pressed)
          {
            csdButton.ImageNorm = (Image) GFXLibrary.selector_square_orange_normal;
            csdButton.ImageOver = (Image) GFXLibrary.selector_square_orange_over;
          }
          else if (csdButton.ImageClick == (Image) GFXLibrary.selector_square_red_pressed)
          {
            csdButton.ImageNorm = (Image) GFXLibrary.selector_square_red_normal;
            csdButton.ImageOver = (Image) GFXLibrary.selector_square_red_over;
          }
          if (this.smallButtons)
            csdButton.Size = new Size(csdButton.Size.Width / 2, csdButton.Size.Height / 2);
        }
      }
      if (clickedControl.ImageClick == (Image) GFXLibrary.selector_square_pressed)
      {
        clickedControl.ImageNorm = (Image) GFXLibrary.selector_square_pressed;
        clickedControl.ImageOver = (Image) GFXLibrary.selector_square_pressed;
      }
      else if (clickedControl.ImageClick == (Image) GFXLibrary.selector_square_orange_pressed)
      {
        clickedControl.ImageNorm = (Image) GFXLibrary.selector_square_orange_pressed;
        clickedControl.ImageOver = (Image) GFXLibrary.selector_square_orange_pressed;
      }
      else if (clickedControl.ImageClick == (Image) GFXLibrary.selector_square_red_pressed)
      {
        clickedControl.ImageNorm = (Image) GFXLibrary.selector_square_red_pressed;
        clickedControl.ImageOver = (Image) GFXLibrary.selector_square_red_pressed;
      }
      if (this.smallButtons)
        clickedControl.Size = new Size(clickedControl.Size.Width / 2, clickedControl.Size.Height / 2);
      this.selectedCounty = clickedControl.Data;
      this.btnEnterGame.Enabled = true;
      this.loadingLabel.Text = GameEngine.Instance.World.getCountyName(this.selectedCounty);
      this.loadingLabel.Visible = true;
      this.mapImage.invalidate();
    }

    private void SetStartingCountyCallback(SetStartingCounty_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      if (returnData.availableCounties != null)
      {
        bool flag = false;
        for (int index = 0; index < returnData.availableCounties.Count; index += 4)
        {
          if (returnData.availableCounties[index] == this.selectedCounty)
          {
            flag = true;
            break;
          }
        }
        if (flag)
        {
          ++this.retries;
          if (this.retries < 2)
          {
            Thread.Sleep(2000);
            RemoteServices.Instance.set_SetStartingCounty_UserCallBack(new RemoteServices.SetStartingCounty_UserCallBack(this.SetStartingCountyCallback));
            RemoteServices.Instance.SetStartingCounty(this.selectedCounty);
            return;
          }
        }
        this.importCounties(returnData.availableCounties);
        this.btnEnterGame.Enabled = true;
        this.closePopup();
        int num = (int) MyMessageBox.Show(SK.Text("SelectVillageAreaPopup_Village_Placement_Error_Message", "The server failed to find you a village, please try again."), SK.Text("SelectVillageAreaPopup_Village_Placement_Error", "Village Placement Error"));
      }
      else if (returnData.villageID >= 0)
      {
        GameEngine.Instance.World.setVillageName(returnData.villageID, returnData.villageName);
        GameEngine.Instance.World.addUserVillage(returnData.villageID);
        GameEngine.Instance.World.updateWorldMapOwnership();
        this.m_parent.closing = true;
        GameEngine.Instance.closeNoVillagePopup(true);
        GameEngine.Instance.World.setResearchData(returnData.m_researchData);
        InterfaceMgr.Instance.selectUserVillage(returnData.villageID, false);
      }
      else
        this.delayedRetry = DateTime.Now;
    }

    private void btnBack_Click()
    {
      GameEngine.Instance.playInterfaceSound("SelectVillageAreaPopup_back");
      this.m_parent.closing = true;
      this.closePopup();
      GameEngine.Instance.closeNoVillagePopup(false);
      GameEngine.Instance.openSimpleSelectVillage();
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
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Name = nameof (NewSelectVillageAreaPanel);
      this.Size = new Size(600, 55);
      this.ResumeLayout(false);
    }
  }
}
