// Decompiled with JetBrains decompiler
// Type: Kingdoms.GFXLibrary
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public sealed class GFXLibrary
  {
    public static bool AssetsLoaded = false;
    private static readonly GFXLibrary instance = new GFXLibrary();
    private GraphicsMgr gfx;
    public static Dictionary<string, BaseImage> LoginWorldFlags;
    public static Dictionary<string, BaseImage> LoginWorldMaps;
    public static Dictionary<int, BaseImage> CardSlotStillSymbols;
    public static int ButtonStateNormal = 0;
    public static int ButtonStateOver = 1;
    public static int ButtonStatePressed = 2;
    public static Dictionary<int, BaseImage[][]> CardFilterButtons;
    public static Dictionary<int, BaseImage[]> PremiumTokens;
    public static int[] CardSlotAnimData;
    public static BaseImage bubble_normal = new BaseImage(AssetPaths.AssetIconsMainResources, "bubble_normal.png");
    public static BaseImage bubble_over = new BaseImage(AssetPaths.AssetIconsMainResources, "bubble_over.png");
    public static BaseImage com_16_ale = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_ale.png");
    public static BaseImage com_16_apples = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_apples.png");
    public static BaseImage com_16_armour = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_armour.png");
    public static BaseImage com_16_bows = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_bows.png");
    public static BaseImage com_16_bread = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_bread.png");
    public static BaseImage com_16_catapults = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_catapults.png");
    public static BaseImage com_16_cheese = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_cheese.png");
    public static BaseImage com_16_clothing = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_clothing.png");
    public static BaseImage com_16_fish = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_fish.png");
    public static BaseImage com_16_food = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_food.png");
    public static BaseImage com_16_furniture = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_furniture.png");
    public static BaseImage com_16_honour = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_honour.png");
    public static BaseImage com_16_iron = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_iron.png");
    public static BaseImage com_16_meat = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_meat.png");
    public static BaseImage com_16_metalwork = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_metalwork.png");
    public static BaseImage com_16_money = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_money.png");
    public static BaseImage com_16_people = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_people.png");
    public static BaseImage com_16_pikes = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_pikes.png");
    public static BaseImage com_16_pitch = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_pitch.png");
    public static BaseImage com_16_salt = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_salt.png");
    public static BaseImage com_16_silk = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_silk.png");
    public static BaseImage com_16_spice = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_spice.png");
    public static BaseImage com_16_stone = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_stone.png");
    public static BaseImage com_16_swords = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_swords.png");
    public static BaseImage com_16_veg = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_veg.png");
    public static BaseImage com_16_venison = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_venison.png");
    public static BaseImage com_16_wine = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_wine.png");
    public static BaseImage com_16_wood = new BaseImage(AssetPaths.AssetIconsMainResources, "com_16_wood.png");
    public static BaseImage com_32_ale = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_ale.png");
    public static BaseImage com_32_apples = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_apples.png");
    public static BaseImage com_32_armour = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_armour.png");
    public static BaseImage com_32_bows = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_bows.png");
    public static BaseImage com_32_bread = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_bread.png");
    public static BaseImage com_32_catapults = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_catapults.png");
    public static BaseImage com_32_cheese = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_cheese.png");
    public static BaseImage com_32_clothing = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_clothing.png");
    public static BaseImage com_32_fish = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_fish.png");
    public static BaseImage com_32_food = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_food.png");
    public static BaseImage com_32_furniture = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_furniture.png");
    public static BaseImage com_32_honour = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_honour.png");
    public static BaseImage com_32_iron = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_iron.png");
    public static BaseImage com_32_meat = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_meat.png");
    public static BaseImage com_32_metalwork = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_metalwork.png");
    public static BaseImage com_32_money = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_money.png");
    public static BaseImage com_32_people = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_people.png");
    public static BaseImage com_32_pikes = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_pikes.png");
    public static BaseImage com_32_pitch = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_pitch.png");
    public static BaseImage com_32_research = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_research.png");
    public static BaseImage com_32_salt = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_salt.png");
    public static BaseImage com_32_silk = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_silk.png");
    public static BaseImage com_32_spice = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_spice.png");
    public static BaseImage com_32_stone = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_stone.png");
    public static BaseImage com_32_swords = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_swords.png");
    public static BaseImage com_32_veg = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_veg.png");
    public static BaseImage com_32_venison = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_venison.png");
    public static BaseImage com_32_wine = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_wine.png");
    public static BaseImage com_32_wood = new BaseImage(AssetPaths.AssetIconsMainResources, "com_32_wood.png");
    public static BaseImage ContextMenuBackground = new BaseImage(AssetPaths.AssetIconsMainResources, "ContextMenuBackground.png");
    public static BaseImage int_world_icon_castle = new BaseImage(AssetPaths.AssetIconsMainResources, "int_world_icon_castle.png");
    public static BaseImage int_world_icon_no_attack = new BaseImage(AssetPaths.AssetIconsMainResources, "int_world_icon_no_attack.png");
    public static BaseImage int_world_icon_resource = new BaseImage(AssetPaths.AssetIconsMainResources, "int_world_icon_resource.png");
    public static BaseImage int_world_icon_troops = new BaseImage(AssetPaths.AssetIconsMainResources, "int_world_icon_troops.png");
    public static BaseImage int_world_icon_village = new BaseImage(AssetPaths.AssetIconsMainResources, "int_world_icon_village.png");
    public static BaseImage mail_menubar_closed = new BaseImage(AssetPaths.AssetIconsMainResources, "mail_menubar_closed.png");
    public static BaseImage mail_menubar_closed_bright = new BaseImage(AssetPaths.AssetIconsMainResources, "mail_menubar_closed_bright.png");
    public static BaseImage mail_menubar_open = new BaseImage(AssetPaths.AssetIconsMainResources, "mail_menubar_open.png");
    public static BaseImage mail_menubar_open_bright = new BaseImage(AssetPaths.AssetIconsMainResources, "mail_menubar_open_bright.png");
    public static BaseImage MainWindowBackground_paper = new BaseImage(AssetPaths.AssetIconsMainResources, "MainWindowBackground_paper.png");
    public static BaseImage menu_Background = new BaseImage(AssetPaths.AssetIconsMainResources, "menu_Background.png");
    public static BaseImage menubar_connecter_left = new BaseImage(AssetPaths.AssetIconsMainResources, "menubar_connecter_left.png");
    public static BaseImage menubar_left_faith = new BaseImage(AssetPaths.AssetIconsMainResources, "menubar_left_faith.png");
    public static BaseImage menubar_middle = new BaseImage(AssetPaths.AssetIconsMainResources, "menubar_middle.png");
    public static BaseImage menubar_top = new BaseImage(AssetPaths.AssetIconsMainResources, "menubar_top.png");
    public static BaseImage ParishFlag = new BaseImage(AssetPaths.AssetIconsMainResources, "ParishFlag.png");
    public static BaseImage Plague_32x32 = new BaseImage(AssetPaths.AssetIconsMainResources, "Plague_32x32.png");
    public static BaseImage points_menubar_bright = new BaseImage(AssetPaths.AssetIconsMainResources, "points_menubar_bright.png");
    public static BaseImage points_menubar_normal = new BaseImage(AssetPaths.AssetIconsMainResources, "points_menubar_normal.png");
    public static BaseImage population_bed = new BaseImage(AssetPaths.AssetIconsMainResources, "population_bed.png");
    public static BaseImage population_head = new BaseImage(AssetPaths.AssetIconsMainResources, "population_head.png");
    public static BaseImage r_arrow_small_left_norm = new BaseImage(AssetPaths.AssetIconsMainResources, "r_arrow_small_left_norm.png");
    public static BaseImage r_arrow_small_left_over = new BaseImage(AssetPaths.AssetIconsMainResources, "r_arrow_small_left_over.png");
    public static BaseImage r_arrow_small_right_norm = new BaseImage(AssetPaths.AssetIconsMainResources, "r_arrow_small_right_norm.png");
    public static BaseImage r_arrow_small_right_over = new BaseImage(AssetPaths.AssetIconsMainResources, "r_arrow_small_right_over.png");
    public static BaseImage ReadMail = new BaseImage(AssetPaths.AssetIconsMainResources, "ReadMail.png");
    public static BaseImage contest_menubar_bright = new BaseImage(AssetPaths.AssetIconsMainResources, "contest_menubar_over.png");
    public static BaseImage contest_menubar_normal = new BaseImage(AssetPaths.AssetIconsMainResources, "contest_menubar_normal.png");
    public static BaseImage scroll_inset_bottom = new BaseImage(AssetPaths.AssetIconsMainResources, "scroll_inset_bottom.png");
    public static BaseImage scroll_inset_mid = new BaseImage(AssetPaths.AssetIconsMainResources, "scroll_inset_mid.png");
    public static BaseImage scroll_inset_top = new BaseImage(AssetPaths.AssetIconsMainResources, "scroll_inset_top.png");
    public static BaseImage scroll_thumb_bottom = new BaseImage(AssetPaths.AssetIconsMainResources, "scroll_thumb_bottom.png");
    public static BaseImage scroll_thumb_mid = new BaseImage(AssetPaths.AssetIconsMainResources, "scroll_thumb_mid.png");
    public static BaseImage scroll_thumb_top = new BaseImage(AssetPaths.AssetIconsMainResources, "scroll_thumb_top.png");
    public static BaseImage UnreadMail = new BaseImage(AssetPaths.AssetIconsMainResources, "UnreadMail.png");
    public static BaseImage villagename_body = new BaseImage(AssetPaths.AssetIconsMainResources, "villagename_body.png");
    public static BaseImage villagename_button_left_highlight = new BaseImage(AssetPaths.AssetIconsMainResources, "villagename_button_left_highlight.png");
    public static BaseImage villagename_button_left_normal = new BaseImage(AssetPaths.AssetIconsMainResources, "villagename_button_left_normal.png");
    public static BaseImage villagename_button_left_selected = new BaseImage(AssetPaths.AssetIconsMainResources, "villagename_button_left_selected.png");
    public static BaseImage villagename_button_right_highlight = new BaseImage(AssetPaths.AssetIconsMainResources, "villagename_button_right_highlight.png");
    public static BaseImage villagename_button_right_normal = new BaseImage(AssetPaths.AssetIconsMainResources, "villagename_button_right_normal.png");
    public static BaseImage villagename_button_right_selected = new BaseImage(AssetPaths.AssetIconsMainResources, "villagename_button_right_selected.png");
    public static BaseImage[] blue_screen_button_array = BaseImage.createFromUV(AssetPaths.AssetIconsMisc, nameof (blue_screen_button_array), 6);
    public static BaseImage[] villageOverviewIcons = BaseImage.createFromUV(AssetPaths.AssetIconsMisc, "Village_Overview_icons", 20);
    public static BaseImage[] wl_moving_unit_icons = BaseImage.createFromUV(AssetPaths.AssetIconsMisc, nameof (wl_moving_unit_icons), 59);
    public static BaseImage[] attack_tabs_comp = BaseImage.createFromUV(AssetPaths.AssetIconsMisc, nameof (attack_tabs_comp), 15);
    public static BaseImage[] sea_conditions = BaseImage.createFromUV(AssetPaths.AssetIconsMisc, "seaconditions", 9);
    public static BaseImage aeriaPoints = new BaseImage(AssetPaths.AssetIconsMisc, "AeriaPoints");
    public static BaseImage facebookLogin_EN = new BaseImage(AssetPaths.AssetIconsMisc, "FacebookLogin_EN.png");
    public static BaseImage facebookLogin_FR = new BaseImage(AssetPaths.AssetIconsMisc, "FacebookLogin_fr.png");
    public static BaseImage facebookLogin_DE = new BaseImage(AssetPaths.AssetIconsMisc, "FacebookLogin_de.png");
    public static BaseImage facebookLogin_RU = new BaseImage(AssetPaths.AssetIconsMisc, "FacebookLogin_ru.png");
    public static BaseImage facebookLogin_ES = new BaseImage(AssetPaths.AssetIconsMisc, "FacebookLogin_es.png");
    public static BaseImage facebookLogin_PL = new BaseImage(AssetPaths.AssetIconsMisc, "FacebookLogin_pl.png");
    public static BaseImage facebookLogin_TR = new BaseImage(AssetPaths.AssetIconsMisc, "FacebookLogin_tr.png");
    public static BaseImage facebookLogin_IT = new BaseImage(AssetPaths.AssetIconsMisc, "FacebookLogin_it.png");
    public static BaseImage facebookLogin_PT = new BaseImage(AssetPaths.AssetIconsMisc, "FacebookLogin_pt.png");
    public static BaseImage facebookGeneric = new BaseImage(AssetPaths.AssetIconsMisc, "Facebook_generic.png");
    public static BaseImage r_popularity_panel_icon_ale_over = new BaseImage(AssetPaths.AssetIconsMisc, "r_popularity_panel_icon_ale_over.png");
    public static BaseImage r_popularity_panel_icon_buildings_over = new BaseImage(AssetPaths.AssetIconsMisc, "r_popularity_panel_icon_buildings_over.png");
    public static BaseImage r_popularity_panel_icon_housing_over = new BaseImage(AssetPaths.AssetIconsMisc, "r_popularity_panel_icon_housing_over.png");
    public static BaseImage r_popularity_panel_icon_rations_over = new BaseImage(AssetPaths.AssetIconsMisc, "r_popularity_panel_icon_rations_over.png");
    public static BaseImage r_popularity_panel_icon_taxes_over = new BaseImage(AssetPaths.AssetIconsMisc, "r_popularity_panel_icon_taxes_over.png");
    public static BaseImage r_popularity_panel_indent_b_over = new BaseImage(AssetPaths.AssetIconsMisc, "r_popularity_panel_indent_b_over.png");
    public static BaseImage r_popularity_panel_indent_over = new BaseImage(AssetPaths.AssetIconsMisc, "r_popularity_panel_indent_over.png");
    public static BaseImage r_popularity_panel_indent_a_over = new BaseImage(AssetPaths.AssetIconsMisc, "r_popularity_panel_indent_a_over.png");
    public static BaseImage mouse_help_middle_bottom = new BaseImage(AssetPaths.AssetIconsMisc, "mouse_help_middle_bottom.png");
    public static BaseImage mouse_help_left_top = new BaseImage(AssetPaths.AssetIconsMisc, "mouse_help_left_top.png");
    public static BaseImage mouse_help_left_middle = new BaseImage(AssetPaths.AssetIconsMisc, "mouse_help_left_middle.png");
    public static BaseImage mouse_help_left_bottom = new BaseImage(AssetPaths.AssetIconsMisc, "mouse_help_left_bottom.png");
    public static BaseImage mouse_help_right_top = new BaseImage(AssetPaths.AssetIconsMisc, "mouse_help_right_top.png");
    public static BaseImage mouse_help_right_middle = new BaseImage(AssetPaths.AssetIconsMisc, "mouse_help_right_middle.png");
    public static BaseImage mouse_help_right_bottom = new BaseImage(AssetPaths.AssetIconsMisc, "mouse_help_right_bottom.png");
    public static BaseImage mouse_help_middle_top = new BaseImage(AssetPaths.AssetIconsMisc, "mouse_help_middle_top.png");
    public static BaseImage mouse_help_middle_middle = new BaseImage(AssetPaths.AssetIconsMisc, "mouse_help_middle_middle.png");
    public static BaseImage worldSelect_Background = new BaseImage(AssetPaths.AssetIconsMisc, "enter_world_lrg");
    public static BaseImage worldSelect_manual_norm = new BaseImage(AssetPaths.AssetIconsMisc, "manual_up.png");
    public static BaseImage worldSelect_manual_over = new BaseImage(AssetPaths.AssetIconsMisc, "manual_over.png");
    public static BaseImage worldSelect_manual_pushed = new BaseImage(AssetPaths.AssetIconsMisc, "manual_down.png");
    public static BaseImage worldSelect_random_norm = new BaseImage(AssetPaths.AssetIconsMisc, "random_up.png");
    public static BaseImage worldSelect_random_over = new BaseImage(AssetPaths.AssetIconsMisc, "random_over.png");
    public static BaseImage worldSelect_random_pushed = new BaseImage(AssetPaths.AssetIconsMisc, "random_down.png");
    public static BaseImage world_select_map_en = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_a.png");
    public static BaseImage world_select_map_de = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_de.png");
    public static BaseImage world_select_map_fr = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_fr.png");
    public static BaseImage world_select_map_ru = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_ru.png");
    public static BaseImage world_select_map_sa = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_sa.png");
    public static BaseImage world_select_map_es = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_es.png");
    public static BaseImage world_select_map_pl = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_pl.png");
    public static BaseImage world_select_map_eu = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_eu.png");
    public static BaseImage world_select_map_tr = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_tr.png");
    public static BaseImage world_select_map_us = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_us.png");
    public static BaseImage world_select_map_it = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_it.png");
    public static BaseImage world_select_map_world = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_world.png");
    public static BaseImage world_select_map_china = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_China.png");
    public static BaseImage world_select_map_ph = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_ph.png");
    public static BaseImage world_select_map_kingmaker = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_king.png");
    public static BaseImage world_select_map_jp = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_jp.png");
    public static BaseImage world_select_map_hyw = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_hyw.png");
    public static BaseImage world_select_map_vk = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_vk.png");
    public static BaseImage world_select_map_tgd = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_tgd.png");
    public static BaseImage world_select_map_cru = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_cru.png");
    public static BaseImage world_select_map_sparta = new BaseImage(AssetPaths.AssetIconsMisc, "WorldMap_sparta.png");
    public static BaseImage world_list_background = new BaseImage(AssetPaths.AssetIconsMisc, "worldListBackground.png");
    public static BaseImage age_first_age_28x16 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (age_first_age_28x16));
    public static BaseImage age_first_age_x65 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (age_first_age_x65));
    public static BaseImage age_second_age_28x16 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (age_second_age_28x16));
    public static BaseImage age_second_age_x65 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (age_second_age_x65));
    public static BaseImage age_third_age_28x16 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (age_third_age_28x16));
    public static BaseImage age_third_age_x65 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (age_third_age_x65));
    public static BaseImage age_fourth_age_28x16 = new BaseImage(AssetPaths.AssetIconsMisc, "age_fourth_age_58x30");
    public static BaseImage age_fourth_age_x65 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (age_fourth_age_x65));
    public static BaseImage age_fifth_age_28x16 = new BaseImage(AssetPaths.AssetIconsMisc, "age_fifth_age_58x30");
    public static BaseImage age_fifth_age_x65 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (age_fifth_age_x65));
    public static BaseImage age_sixth_age_28x16 = new BaseImage(AssetPaths.AssetIconsMisc, "age_sixth_age_58x30");
    public static BaseImage age_sixth_age_x65 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (age_sixth_age_x65));
    public static BaseImage age_seventh_age_28x16 = new BaseImage(AssetPaths.AssetIconsMisc, "age_seventh_age_58x30");
    public static BaseImage age_seventh_age_x65 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (age_seventh_age_x65));
    public static BaseImage age_seventh_age_x130 = new BaseImage(AssetPaths.AssetIconsMisc, "age_seventh_300x130.png");
    public static BaseImage eow_globe = new BaseImage(AssetPaths.AssetIconsMisc, "globe.png");
    public static BaseImage secondAgeLogo = new BaseImage(AssetPaths.AssetIconsMisc, "age_second_age.png");
    public static BaseImage thirdAgeLogo = new BaseImage(AssetPaths.AssetIconsMisc, "age_third_age.png");
    public static BaseImage fourthAgeLogo = new BaseImage(AssetPaths.AssetIconsMisc, "age_fourth_age.png");
    public static BaseImage fifthAgeLogo = new BaseImage(AssetPaths.AssetIconsMisc, "age_fifth_age.png");
    public static BaseImage sixthAgeLogo = new BaseImage(AssetPaths.AssetIconsMisc, "age_sixth_age.png");
    public static BaseImage seventhAgeLogo = new BaseImage(AssetPaths.AssetIconsMisc, "age_seventh_age.png");
    public static BaseImage dominationWorldLogo = new BaseImage(AssetPaths.AssetIconsMisc, "age_domination_age.png");
    public static BaseImage AIWorldLogo = new BaseImage(AssetPaths.AssetIconsMisc, "age_AI_age.png");
    public static BaseImage flag_blue_icon = new BaseImage(AssetPaths.AssetIconsMisc, "flag_blue_icon.png");
    public static BaseImage monk_icon = new BaseImage(AssetPaths.AssetIconsMisc, "monk_icon.png");
    public static BaseImage merchant_icon = new BaseImage(AssetPaths.AssetIconsMisc, "merchant_icon.png");
    public static BaseImage selector_square_normal = new BaseImage(AssetPaths.AssetIconsMisc, "square_selector_normal.png");
    public static BaseImage selector_square_over = new BaseImage(AssetPaths.AssetIconsMisc, "square_selector_over.png");
    public static BaseImage selector_square_pressed = new BaseImage(AssetPaths.AssetIconsMisc, "square_selector_pressed.png");
    public static BaseImage selector_square_orange_normal = new BaseImage(AssetPaths.AssetIconsMisc, "square_selector_orange_normal.png");
    public static BaseImage selector_square_orange_over = new BaseImage(AssetPaths.AssetIconsMisc, "square_selector_orange_over.png");
    public static BaseImage selector_square_orange_pressed = new BaseImage(AssetPaths.AssetIconsMisc, "square_selector_orange_pressed.png");
    public static BaseImage selector_square_red_normal = new BaseImage(AssetPaths.AssetIconsMisc, "square_selector_red_normal.png");
    public static BaseImage selector_square_red_over = new BaseImage(AssetPaths.AssetIconsMisc, "square_selector_red_over.png");
    public static BaseImage selector_square_red_pressed = new BaseImage(AssetPaths.AssetIconsMisc, "square_selector_red_pressed.png");
    public static BaseImage int_but_small_normal = new BaseImage(AssetPaths.AssetIconsMisc, "int_but_small_normal.png");
    public static BaseImage int_but_small_over = new BaseImage(AssetPaths.AssetIconsMisc, "int_but_small_over.png");
    public static BaseImage but_move_building_normal = new BaseImage(AssetPaths.AssetIconsMisc, "but_move-building_normal.png");
    public static BaseImage but_move_building_over = new BaseImage(AssetPaths.AssetIconsMisc, "but_move-building_over.png");
    public static BaseImage but_move_building_pushed = new BaseImage(AssetPaths.AssetIconsMisc, "but_move-building_pushed.png");
    public static BaseImage VillageTabBar_FORUM_Normal = new BaseImage(AssetPaths.AssetIconsMisc, "VillageTabBar_FORUM_Normal.png");
    public static BaseImage VillageTabBar_FORUM_Selected = new BaseImage(AssetPaths.AssetIconsMisc, "VillageTabBar_FORUM_Selected.png");
    public static BaseImage VillageTabBar_INFO_Normal = new BaseImage(AssetPaths.AssetIconsMisc, "VillageTabBar_INFO_Normal.png");
    public static BaseImage VillageTabBar_INFO_Selected = new BaseImage(AssetPaths.AssetIconsMisc, "VillageTabBar_INFO_Selected.png");
    public static BaseImage VillageTabBar_VOTE_Normal = new BaseImage(AssetPaths.AssetIconsMisc, "VillageTabBar_VOTE_Normal.png");
    public static BaseImage VillageTabBar_VOTE_Selected = new BaseImage(AssetPaths.AssetIconsMisc, "VillageTabBar_VOTE_Selected.png");
    public static BaseImage icon_building = new BaseImage(AssetPaths.AssetIconsMisc, nameof (icon_building));
    public static BaseImage icon_research = new BaseImage(AssetPaths.AssetIconsMisc, nameof (icon_research));
    public static BaseImage messageboxtop = new BaseImage(AssetPaths.AssetIconsMisc, "message_box_top");
    public static BaseImage messageboxtop_left = new BaseImage(AssetPaths.AssetIconsMisc, "message_box_top_left");
    public static BaseImage messageboxtop_right = new BaseImage(AssetPaths.AssetIconsMisc, "message_box_top_right");
    public static BaseImage messageboxtop_middle = new BaseImage(AssetPaths.AssetIconsMisc, "message_box_top_middle");
    public static BaseImage messageboxclose = new BaseImage(AssetPaths.AssetIconsMisc, "message_box_close");
    public static BaseImage messageboxclose_over = new BaseImage(AssetPaths.AssetIconsMisc, "message_box_close_overl");
    public static BaseImage message_box_minimize_normal = new BaseImage(AssetPaths.AssetIconsMisc, nameof (message_box_minimize_normal));
    public static BaseImage message_box_minimize_over = new BaseImage(AssetPaths.AssetIconsMisc, nameof (message_box_minimize_over));
    public static BaseImage message_box_maximize_normal = new BaseImage(AssetPaths.AssetIconsMisc, nameof (message_box_maximize_normal));
    public static BaseImage message_box_maximize_over = new BaseImage(AssetPaths.AssetIconsMisc, nameof (message_box_maximize_over));
    public static BaseImage misc_button_blue_210wide_normal = new BaseImage(AssetPaths.AssetIconsMisc, "button_blue_210wide_normal.png");
    public static BaseImage misc_button_blue_210wide_pushed = new BaseImage(AssetPaths.AssetIconsMisc, "button_blue_210wide_pushed.png");
    public static BaseImage misc_button_blue_210wide_over = new BaseImage(AssetPaths.AssetIconsMisc, "button_blue_210wide_over.png");
    public static BaseImage armies_screen_troops = new BaseImage(AssetPaths.AssetIconsMisc, "armies_screen_troops.png");
    public static BaseImage facebook = new BaseImage(AssetPaths.AssetIconsMisc, nameof (facebook));
    public static BaseImage twitter = new BaseImage(AssetPaths.AssetIconsMisc, nameof (twitter));
    public static BaseImage youtube = new BaseImage(AssetPaths.AssetIconsMisc, nameof (youtube));
    public static BaseImage vk = new BaseImage(AssetPaths.AssetIconsMisc, "vkButton");
    public static BaseImage shieldOverlay_70x78 = new BaseImage(AssetPaths.AssetIconsMisc, "shield_overlay_70x78.png");
    public static BaseImage shieldOverlay_144x160 = new BaseImage(AssetPaths.AssetIconsMisc, "shield_overlay_144x160.png");
    public static BaseImage shieldOverlay_56x64 = new BaseImage(AssetPaths.AssetIconsMisc, "shield_overlay_56x64.png");
    public static BaseImage shieldOverlay_32x36 = new BaseImage(AssetPaths.AssetIconsMisc, "shield_overlay_32x36.png");
    public static BaseImage shieldOverlay_25x28 = new BaseImage(AssetPaths.AssetIconsMisc, "shield_overlay_25x28.png");
    public static BaseImage villageOverTab_down = new BaseImage(AssetPaths.AssetIconsMisc, "Village_Overview_tab_down");
    public static BaseImage villageOverTab_up = new BaseImage(AssetPaths.AssetIconsMisc, "Village_Overview_tab_up");
    public static BaseImage premium_menubar_normal = new BaseImage(AssetPaths.AssetIconsMisc, "premium_menubar_normal.png");
    public static BaseImage premium_menubar_over = new BaseImage(AssetPaths.AssetIconsMisc, "premium_menubar_over.png");
    public static BaseImage _9sclice_fancy_bottom_left = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_fancy_bottom_left");
    public static BaseImage _9sclice_fancy_bottom_mid = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_fancy_bottom_mid");
    public static BaseImage _9sclice_fancy_bottom_mid_over = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_fancy_bottom_mid_over");
    public static BaseImage _9sclice_fancy_bottom_right = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_fancy_bottom_right");
    public static BaseImage _9sclice_fancy_mid_left = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_fancy_mid_left");
    public static BaseImage _9sclice_fancy_mid_mid = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_fancy_mid_mid");
    public static BaseImage _9sclice_fancy_mid_right = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_fancy_mid_right");
    public static BaseImage _9sclice_fancy_top_left = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_fancy_top_left");
    public static BaseImage _9sclice_fancy_top_mid = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_fancy_top_mid");
    public static BaseImage _9sclice_fancy_top_mid_over_01 = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_fancy_top_mid_over_01");
    public static BaseImage _9sclice_fancy_top_mid_over_02 = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_fancy_top_mid_over_02");
    public static BaseImage _9sclice_fancy_top_mid_over_03 = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_fancy_top_mid_over_03");
    public static BaseImage _9sclice_fancy_top_right = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_fancy_top_right");
    public static BaseImage worldSelect_swap_norm = new BaseImage(AssetPaths.AssetIconsMisc, "swap_world_up.png");
    public static BaseImage worldSelect_swap_over = new BaseImage(AssetPaths.AssetIconsMisc, "swap_world_over.png");
    public static BaseImage worldSelect_swap_pushed = new BaseImage(AssetPaths.AssetIconsMisc, "swap_world_down.png");
    public static BaseImage pt_rank = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Rank_up.png");
    public static BaseImage pt_Quests = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Quests_up.png");
    public static BaseImage pt_Parish_Wall = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Parish_wall_up.png");
    public static BaseImage pt_Mail = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Mail_up.png");
    public static BaseImage pt_Invite_a_Friend = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Invite_a_friend_up.png");
    public static BaseImage pt_Coat_of_Arms = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Coat_of_arms_up.png");
    public static BaseImage pt_Avatar = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Avatar_up.png");
    public static BaseImage pt_Achievements = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Achievements_up.png");
    public static BaseImage pt_Research = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Research_up.png");
    public static BaseImage pt_Reports = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Reports_up.png");
    public static BaseImage pt_rank_down = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Rank_down.png");
    public static BaseImage pt_Quests_down = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Quest_down.png");
    public static BaseImage pt_Parish_Wall_down = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Parish_wall_down.png");
    public static BaseImage pt_Mail_down = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Mail_down.png");
    public static BaseImage pt_Invite_a_Friend_down = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Invite_a_friend_down.png");
    public static BaseImage pt_Coat_of_Arms_down = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Coat_of_arms_down.png");
    public static BaseImage pt_Avatar_down = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Avatar_down.png");
    public static BaseImage pt_Achievements_down = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Achievements_down.png");
    public static BaseImage pt_Research_down = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Research_down.png");
    public static BaseImage pt_Reports_down = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Reports_down.png");
    public static BaseImage pt_rank_over = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Rank_over.png");
    public static BaseImage pt_Quests_over = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Quests_over.png");
    public static BaseImage pt_Parish_Wall_over = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Parish_wall_over.png");
    public static BaseImage pt_Mail_over = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Mail_over.png");
    public static BaseImage pt_Invite_a_Friend_over = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Invite_a_friend_over.png");
    public static BaseImage pt_Coat_of_Arms_over = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Coat_of_arms_over.png");
    public static BaseImage pt_Avatar_over = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Avatar_over.png");
    public static BaseImage pt_Achievements_over = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Achievements_over.png");
    public static BaseImage pt_Research_over = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Research_over.png");
    public static BaseImage pt_Reports_over = new BaseImage(AssetPaths.AssetIconsMisc, "pt_Reports_over.png");
    public static BaseImage facebookBlueNorm = new BaseImage(AssetPaths.AssetIconsMisc, "button_blue_fb_141wide_normal.png");
    public static BaseImage facebookBlueOver = new BaseImage(AssetPaths.AssetIconsMisc, "button_blue_fb_141wide_over.png");
    public static BaseImage facebookBlueClick = new BaseImage(AssetPaths.AssetIconsMisc, "button_blue_fb_141wide_pushed.png");
    public static BaseImage facebookBrownNorm = new BaseImage(AssetPaths.AssetIconsMisc, "button_brown_fb_141wide_normal.png");
    public static BaseImage facebookBrownOver = new BaseImage(AssetPaths.AssetIconsMisc, "button_brown_fb_141wide_over.png");
    public static BaseImage facebookBrownClick = new BaseImage(AssetPaths.AssetIconsMisc, "button_brown_fb_141wide_pushed.png");
    public static BaseImage dominationEnd = new BaseImage(AssetPaths.AssetIconsMisc, "dominationEnd2.png");
    public static BaseImage HOLlink = new BaseImage(AssetPaths.AssetIconsMisc, "HOL_button.png");
    public static BaseImage playbackBackground = new BaseImage(AssetPaths.AssetIconsMisc, "playback_background.png");
    public static BaseImage playbackPlay = new BaseImage(AssetPaths.AssetIconsMisc, "playback_play.png");
    public static BaseImage playbackPause = new BaseImage(AssetPaths.AssetIconsMisc, "playback_pause.png");
    public static BaseImage playbackStop = new BaseImage(AssetPaths.AssetIconsMisc, "playback_stop.png");
    public static BaseImage playbackSpeed1 = new BaseImage(AssetPaths.AssetIconsMisc, "playback_speed1.png");
    public static BaseImage playbackSpeed2 = new BaseImage(AssetPaths.AssetIconsMisc, "playback_speed2.png");
    public static BaseImage playbackSpeed4 = new BaseImage(AssetPaths.AssetIconsMisc, "playback_speed4.png");
    public static BaseImage playbackTrack = new BaseImage(AssetPaths.AssetIconsMisc, "playback_track.png");
    public static BaseImage playbackBlank = new BaseImage(AssetPaths.AssetIconsMisc, "playback_blank.png");
    public static BaseImage playbackExpand = new BaseImage(AssetPaths.AssetIconsMisc, "playback_expand.png");
    public static BaseImage playbackContract = new BaseImage(AssetPaths.AssetIconsMisc, "playback_contract.png");
    public static BaseImage _9sclice_generic_bottom_left = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_generic_bottom_left");
    public static BaseImage _9sclice_generic_bottom_mid = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_generic_bottom_mid");
    public static BaseImage _9sclice_generic_bottom_right = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_generic_bottom_right");
    public static BaseImage _9sclice_generic_mid_left = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_generic_mid_left");
    public static BaseImage _9sclice_generic_mid_mid = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_generic_mid_mid");
    public static BaseImage _9sclice_generic_mid_right = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_generic_mid_right");
    public static BaseImage _9sclice_generic_top_left = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_generic_top_left");
    public static BaseImage _9sclice_generic_top_mid = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_generic_top_mid");
    public static BaseImage _9sclice_generic_top_right = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_generic_top_right");
    public static BaseImage _9sclice_bracket_bottom_left = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_bracket_bottom_left");
    public static BaseImage _9sclice_bracket_bottom_right = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_bracket_bottom_right");
    public static BaseImage _9sclice_bracket_mid_mid = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_bracket_mid_mid");
    public static BaseImage _9sclice_bracket_top_left = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_bracket_top_left");
    public static BaseImage _9sclice_bracket_top_right = new BaseImage(AssetPaths.AssetIconsMisc, "nineslice_bracket_top_right");
    public static BaseImage prizeGold = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizeGold));
    public static BaseImage prizeFaith = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizeFaith));
    public static BaseImage prizeHonour = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizeHonour));
    public static BaseImage prizeCardPack = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizeCardPack));
    public static BaseImage prizeReputation = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizeReputation));
    public static BaseImage prizeTokens = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizeTokens));
    public static BaseImage prizeWheelspins = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizeWheelspins));
    public static BaseImage prizeShield = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizeShield));
    public static BaseImage prizeBlank = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizeBlank));
    public static BaseImage prizePremium = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizePremium));
    public static BaseImage prizePremium2 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizePremium2));
    public static BaseImage prizePremium7 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizePremium7));
    public static BaseImage prizePremium30 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizePremium30));
    public static BaseImage prizeSpin1 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizeSpin1));
    public static BaseImage prizeSpin2 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizeSpin2));
    public static BaseImage prizeSpin3 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizeSpin3));
    public static BaseImage prizeSpin4 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizeSpin4));
    public static BaseImage prizeSpin5 = new BaseImage(AssetPaths.AssetIconsMisc, nameof (prizeSpin5));
    public static BaseImage contestSupportHorse = new BaseImage(AssetPaths.AssetIconsMisc, nameof (contestSupportHorse));
    public static BaseImage contestSupportLion = new BaseImage(AssetPaths.AssetIconsMisc, nameof (contestSupportLion));
    public static BaseImage contestTrumpetLeft = new BaseImage(AssetPaths.AssetIconsMisc, nameof (contestTrumpetLeft));
    public static BaseImage contestTrumpetRight = new BaseImage(AssetPaths.AssetIconsMisc, nameof (contestTrumpetRight));
    public static BaseImage contestTrumpetLeftSmall = new BaseImage(AssetPaths.AssetIconsMisc, nameof (contestTrumpetLeftSmall));
    public static BaseImage contestTrumpetRightSmall = new BaseImage(AssetPaths.AssetIconsMisc, nameof (contestTrumpetRightSmall));
    public static BaseImage contestTitleLeft = new BaseImage(AssetPaths.AssetIconsMisc, "contest_title_bar_left");
    public static BaseImage contestTitleMid = new BaseImage(AssetPaths.AssetIconsMisc, "contest_title_bar_mid");
    public static BaseImage contestTitleRight = new BaseImage(AssetPaths.AssetIconsMisc, "contest_title_bar_right");
    public static BaseImage contestArrowLeft = new BaseImage(AssetPaths.AssetIconsMisc, "contestLeftArrow");
    public static BaseImage contestArrowRight = new BaseImage(AssetPaths.AssetIconsMisc, "contestRightArrow");
    public static BaseImage castle_damage = new BaseImage(AssetPaths.AssetIconsMisc, nameof (castle_damage));
    public static BaseImage research_border_research_ill_normal = new BaseImage(AssetPaths.AssetIconsResearch, "border_research_ill_normal.png");
    public static BaseImage research_border_research_ill_over = new BaseImage(AssetPaths.AssetIconsResearch, "border_research_ill_over.png");
    public static BaseImage research_tech_tree_inset_54_tall_left = new BaseImage(AssetPaths.AssetIconsResearch, "tech-tree_inset_54-tall_left.png");
    public static BaseImage research_tech_tree_inset_54_tall_mid = new BaseImage(AssetPaths.AssetIconsResearch, "tech-tree_inset_54-tall_mid.png");
    public static BaseImage research_tech_tree_inset_54_tall_right = new BaseImage(AssetPaths.AssetIconsResearch, "tech-tree_inset_54-tall_right.png");
    public static BaseImage tech_tree_dots_yellow_x16 = new BaseImage(AssetPaths.AssetIconsResearch, "tech_tree_dots_yellow_x16.png");
    public static BaseImage mix_ycf_0001_bl_0010 = new BaseImage(AssetPaths.AssetIconsResearch, "mix-ycf_0001-bl_0010.png");
    public static BaseImage mix_ycf_0001_bl_0100 = new BaseImage(AssetPaths.AssetIconsResearch, "mix-ycf_0001-bl_0100.png");
    public static BaseImage mix_ycf_0001_bl_0110 = new BaseImage(AssetPaths.AssetIconsResearch, "mix-ycf_0001-bl_0110.png");
    public static BaseImage mix_ycf_0011_bl_0100 = new BaseImage(AssetPaths.AssetIconsResearch, "mix-ycf_0011-bl_0100.png");
    public static BaseImage mix_ycf_0101_bl_0010 = new BaseImage(AssetPaths.AssetIconsResearch, "mix-ycf_0101-bl_0010.png");
    public static BaseImage mix_ych_0001_bl_0010 = new BaseImage(AssetPaths.AssetIconsResearch, "mix-ych_0001-bl_0010.png");
    public static BaseImage mix_ych_0001_bl_0100 = new BaseImage(AssetPaths.AssetIconsResearch, "mix-ych_0001-bl_0100.png");
    public static BaseImage mix_ych_0001_bl_0110 = new BaseImage(AssetPaths.AssetIconsResearch, "mix-ych_0001-bl_0110.png");
    public static BaseImage ycf_0001 = new BaseImage(AssetPaths.AssetIconsResearch, "ycf_0001.png");
    public static BaseImage ycf_0011 = new BaseImage(AssetPaths.AssetIconsResearch, "ycf_0011.png");
    public static BaseImage ycf_0101 = new BaseImage(AssetPaths.AssetIconsResearch, "ycf_0101.png");
    public static BaseImage ycf_0111 = new BaseImage(AssetPaths.AssetIconsResearch, "ycf_0111.png");
    public static BaseImage ych_0001 = new BaseImage(AssetPaths.AssetIconsResearch, "ych_0001.png");
    public static BaseImage yline_1100 = new BaseImage(AssetPaths.AssetIconsResearch, "yline_1100.png");
    public static BaseImage yline_1110 = new BaseImage(AssetPaths.AssetIconsResearch, "yline_1110.png");
    public static BaseImage yline_vertical = new BaseImage(AssetPaths.AssetIconsResearch, "yline_vertical.png");
    public static BaseImage ill_back_yline_1100 = new BaseImage(AssetPaths.AssetIconsResearch, "ill_back_yline_1100.png");
    public static BaseImage ill_back_yline_0101 = new BaseImage(AssetPaths.AssetIconsResearch, "ill_back_yline_0101.png");
    public static BaseImage ill_back_yellow_textback = new BaseImage(AssetPaths.AssetIconsResearch, "ill_back_yellow_textback.png");
    public static BaseImage ycf_01gG = new BaseImage(AssetPaths.AssetIconsResearch, "ycf_01gG.png");
    public static BaseImage ycf_0g1G = new BaseImage(AssetPaths.AssetIconsResearch, "ycf_0g1G.png");
    public static BaseImage ycf_011G = new BaseImage(AssetPaths.AssetIconsResearch, "ycf_011G.png");
    public static BaseImage ycf_010G = new BaseImage(AssetPaths.AssetIconsResearch, "ycf_010G.png");
    public static BaseImage ycf_001G = new BaseImage(AssetPaths.AssetIconsResearch, "ycf_001G.png");
    public static BaseImage ycf_0ggG = new BaseImage(AssetPaths.AssetIconsResearch, "ycf_0ggG.png");
    public static BaseImage ycf_000G = new BaseImage(AssetPaths.AssetIconsResearch, "ycf_000G.png");
    public static BaseImage mix_ycf_000G_bl_0100 = new BaseImage(AssetPaths.AssetIconsResearch, "mix-ycf_000G-bl_0100.png");
    public static BaseImage ycf_00gG = new BaseImage(AssetPaths.AssetIconsResearch, "ycf_00gG.png");
    public static BaseImage border_research_ill_selected_normal = new BaseImage(AssetPaths.AssetIconsResearch, "border_research_ill_selected_normal.png");
    public static BaseImage[] tech_numbers = BaseImage.createFromUV(AssetPaths.AssetIconsResearch2, "numbers_array", 30);
    public static BaseImage bcf_0000 = new BaseImage(AssetPaths.AssetIconsResearch2, "bcf_0000.png");
    public static BaseImage bcf_0001 = new BaseImage(AssetPaths.AssetIconsResearch2, "bcf_0001.png");
    public static BaseImage bcf_0011 = new BaseImage(AssetPaths.AssetIconsResearch2, "bcf_0011.png");
    public static BaseImage bcf_0101 = new BaseImage(AssetPaths.AssetIconsResearch2, "bcf_0101.png");
    public static BaseImage bcf_0111 = new BaseImage(AssetPaths.AssetIconsResearch2, "bcf_0111.png");
    public static BaseImage bline_vertical = new BaseImage(AssetPaths.AssetIconsResearch2, "bline_vertical.png");
    public static BaseImage gcf_0001 = new BaseImage(AssetPaths.AssetIconsResearch2, "gcf_0001.png");
    public static BaseImage gcf_0011 = new BaseImage(AssetPaths.AssetIconsResearch2, "gcf_0011.png");
    public static BaseImage gcf_0101 = new BaseImage(AssetPaths.AssetIconsResearch2, "gcf_0101.png");
    public static BaseImage gcf_0111 = new BaseImage(AssetPaths.AssetIconsResearch2, "gcf_0111.png");
    public static BaseImage gch_0001 = new BaseImage(AssetPaths.AssetIconsResearch2, "gch_0001.png");
    public static BaseImage gch_0011 = new BaseImage(AssetPaths.AssetIconsResearch2, "gch_0011.png");
    public static BaseImage gch_0101 = new BaseImage(AssetPaths.AssetIconsResearch2, "gch_0101.png");
    public static BaseImage gch_0111 = new BaseImage(AssetPaths.AssetIconsResearch2, "gch_0111.png");
    public static BaseImage gline_1100 = new BaseImage(AssetPaths.AssetIconsResearch2, "gline_1100.png");
    public static BaseImage gline_1110 = new BaseImage(AssetPaths.AssetIconsResearch2, "gline_1110.png");
    public static BaseImage gline_vertical = new BaseImage(AssetPaths.AssetIconsResearch2, "gline_vertical.png");
    public static BaseImage ill_back_bline_0000 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_bline_0000.png");
    public static BaseImage ill_back_bline_0001 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_bline_0001.png");
    public static BaseImage ill_back_bline_0010 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_bline_0010.png");
    public static BaseImage ill_back_bline_0011 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_bline_0011.png");
    public static BaseImage ill_back_bline_0100 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_bline_0100.png");
    public static BaseImage ill_back_bline_0101 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_bline_0101.png");
    public static BaseImage ill_back_bline_0110 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_bline_0110.png");
    public static BaseImage ill_back_bline_1000 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_bline_1000.png");
    public static BaseImage ill_back_bline_1001 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_bline_1001.png");
    public static BaseImage ill_back_bline_1010 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_bline_1010.png");
    public static BaseImage ill_back_bline_1100 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_bline_1100.png");
    public static BaseImage ill_back_gline_0001 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_gline_0001.png");
    public static BaseImage ill_back_gline_0010 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_gline_0010.png");
    public static BaseImage ill_back_gline_0011 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_gline_0011.png");
    public static BaseImage ill_back_gline_0100 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_gline_0100.png");
    public static BaseImage ill_back_gline_0101 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_gline_0101.png");
    public static BaseImage ill_back_gline_0110 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_gline_0110.png");
    public static BaseImage ill_back_gline_1000 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_gline_1000.png");
    public static BaseImage ill_back_gline_1001 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_gline_1001.png");
    public static BaseImage ill_back_gline_1010 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_gline_1010.png");
    public static BaseImage ill_back_gline_1100 = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_gline_1100.png");
    public static BaseImage ill_back_green_textback = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_back_green_textback.png");
    public static BaseImage ill_shield = new BaseImage(AssetPaths.AssetIconsResearch2, "ill_shield.png");
    public static BaseImage mix_gcf_0001_bl_0010 = new BaseImage(AssetPaths.AssetIconsResearch2, "mix-gcf_0001-bl_0010.png");
    public static BaseImage mix_gcf_0001_bl_0100 = new BaseImage(AssetPaths.AssetIconsResearch2, "mix-gcf_0001-bl_0100.png");
    public static BaseImage mix_gcf_0001_bl_0110 = new BaseImage(AssetPaths.AssetIconsResearch2, "mix-gcf_0001-bl_0110.png");
    public static BaseImage mix_gcf_0011_bl_0100 = new BaseImage(AssetPaths.AssetIconsResearch2, "mix-gcf_0011-bl_0100.png");
    public static BaseImage mix_gcf_0101_bl_0010 = new BaseImage(AssetPaths.AssetIconsResearch2, "mix-gcf_0101-bl_0010.png");
    public static BaseImage mix_gch_0001_bl_0010 = new BaseImage(AssetPaths.AssetIconsResearch2, "mix-gch_0001-bl_0010.png");
    public static BaseImage mix_gch_0001_bl_0100 = new BaseImage(AssetPaths.AssetIconsResearch2, "mix-gch_0001-bl_0100.png");
    public static BaseImage mix_gch_0001_bl_0110 = new BaseImage(AssetPaths.AssetIconsResearch2, "mix-gch_0001-bl_0110.png");
    public static BaseImage tech_list_but_big_in = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-list_but_big_in.png");
    public static BaseImage tech_list_but_big_normal = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-list_but_big_normal.png");
    public static BaseImage tech_list_but_big_over = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-list_but_big_over.png");
    public static BaseImage tech_list_insets_X2 = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-list_insets_X2.png");
    public static BaseImage tech_number_10_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_10_green.png");
    public static BaseImage tech_number_10_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_10_olive.png");
    public static BaseImage tech_number_10_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_10_tan.png");
    public static BaseImage tech_number_11_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_11_green.png");
    public static BaseImage tech_number_11_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_11_olive.png");
    public static BaseImage tech_number_11_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_11_tan.png");
    public static BaseImage tech_number_12_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_12_green.png");
    public static BaseImage tech_number_12_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_12_olive.png");
    public static BaseImage tech_number_12_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_12_tan.png");
    public static BaseImage tech_number_13_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_13_green.png");
    public static BaseImage tech_number_13_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_13_olive.png");
    public static BaseImage tech_number_13_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_13_tan.png");
    public static BaseImage tech_number_14_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_14_green.png");
    public static BaseImage tech_number_14_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_14_olive.png");
    public static BaseImage tech_number_14_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_14_tan.png");
    public static BaseImage tech_number_15_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_15_green.png");
    public static BaseImage tech_number_15_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_15_olive.png");
    public static BaseImage tech_number_15_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_15_tan.png");
    public static BaseImage tech_number_16_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_16_green.png");
    public static BaseImage tech_number_16_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_16_olive.png");
    public static BaseImage tech_number_16_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_16_tan.png");
    public static BaseImage tech_number_1_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_1_green.png");
    public static BaseImage tech_number_1_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_1_olive.png");
    public static BaseImage tech_number_1_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_1_tan.png");
    public static BaseImage tech_number_2_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_2_green.png");
    public static BaseImage tech_number_2_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_2_olive.png");
    public static BaseImage tech_number_2_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_2_tan.png");
    public static BaseImage tech_number_3_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_3_green.png");
    public static BaseImage tech_number_3_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_3_olive.png");
    public static BaseImage tech_number_3_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_3_tan.png");
    public static BaseImage tech_number_4_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_4_green.png");
    public static BaseImage tech_number_4_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_4_olive.png");
    public static BaseImage tech_number_4_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_4_tan.png");
    public static BaseImage tech_number_5_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_5_green.png");
    public static BaseImage tech_number_5_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_5_olive.png");
    public static BaseImage tech_number_5_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_5_tan.png");
    public static BaseImage tech_number_6_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_6_green.png");
    public static BaseImage tech_number_6_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_6_olive.png");
    public static BaseImage tech_number_6_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_6_tan.png");
    public static BaseImage tech_number_7_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_7_green.png");
    public static BaseImage tech_number_7_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_7_olive.png");
    public static BaseImage tech_number_7_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_7_tan.png");
    public static BaseImage tech_number_8_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_8_green.png");
    public static BaseImage tech_number_8_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_8_olive.png");
    public static BaseImage tech_number_8_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_8_tan.png");
    public static BaseImage tech_number_9_green = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_9_green.png");
    public static BaseImage tech_number_9_olive = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_9_olive.png");
    public static BaseImage tech_number_9_tan = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-number_9_tan.png");
    public static BaseImage tech_tree_inset_left = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_inset_left.png");
    public static BaseImage tech_tree_inset_mid = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_inset_mid.png");
    public static BaseImage tech_tree_inset_right = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_inset_right.png");
    public static BaseImage tech_tree_inset_tall_left = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_inset_tall_left.png");
    public static BaseImage tech_tree_inset_tall_mid = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_inset_tall_mid.png");
    public static BaseImage tech_tree_inset_tall_right = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_inset_tall_right.png");
    public static BaseImage tech_tree_progbar_green_left = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_progbar_green_left.png");
    public static BaseImage tech_tree_progbar_green_mid = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_progbar_green_mid.png");
    public static BaseImage tech_tree_progbar_green_right = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_progbar_green_right.png");
    public static BaseImage tech_tree_progbar_olive_left = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_progbar_olive_left.png");
    public static BaseImage tech_tree_progbar_olive_mid = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_progbar_olive_mid.png");
    public static BaseImage tech_tree_progbar_olive_right = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_progbar_olive_right.png");
    public static BaseImage tech_tree_tab_list_highlight = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_tab-list_highlight.png");
    public static BaseImage tech_tree_tab_list_normal = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_tab-list_normal.png");
    public static BaseImage tech_tree_tab_tree_highlight = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_tab-tree_highlight.png");
    public static BaseImage tech_tree_tab_tree_normal = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_tab-tree_normal.png");
    public static BaseImage tech_tree_tab_01_highlight = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_tab-01_highlight.png");
    public static BaseImage tech_tree_tab_01_normal = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_tab-01_normal.png");
    public static BaseImage tech_tree_tab_highlight = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_tab_highlight.png");
    public static BaseImage tech_tree_tab_normal = new BaseImage(AssetPaths.AssetIconsResearch2, "tech-tree_tab_normal.png");
    public static BaseImage techtree_button_in = new BaseImage(AssetPaths.AssetIconsResearch2, "techtree_button_in.png");
    public static BaseImage techtree_button_normal = new BaseImage(AssetPaths.AssetIconsResearch2, "techtree_button_normal.png");
    public static BaseImage techtree_button_over = new BaseImage(AssetPaths.AssetIconsResearch2, "techtree_button_over.png");
    public static BaseImage techtree_inset_edge_bottom = new BaseImage(AssetPaths.AssetIconsResearch2, "techtree_inset-edge_bottom.png");
    public static BaseImage techtree_inset_edge_bottomleft = new BaseImage(AssetPaths.AssetIconsResearch2, "techtree_inset-edge_bottomleft.png");
    public static BaseImage techtree_inset_edge_bottomright = new BaseImage(AssetPaths.AssetIconsResearch2, "techtree_inset-edge_bottomright.png");
    public static BaseImage techtree_inset_edge_left = new BaseImage(AssetPaths.AssetIconsResearch2, "techtree_inset-edge_left.png");
    public static BaseImage techtree_inset_edge_right = new BaseImage(AssetPaths.AssetIconsResearch2, "techtree_inset-edge_right.png");
    public static BaseImage techtree_inset_edge_top = new BaseImage(AssetPaths.AssetIconsResearch2, "techtree_inset-edge_top.png");
    public static BaseImage techtree_inset_edge_topleft = new BaseImage(AssetPaths.AssetIconsResearch2, "techtree_inset-edge_topleft.png");
    public static BaseImage techtree_inset_edge_topright = new BaseImage(AssetPaths.AssetIconsResearch2, "techtree_inset-edge_topright.png");
    public static BaseImage tech_tree_dots_black_x04 = new BaseImage(AssetPaths.AssetIconsResearch2, "tech_tree_dots_black_x04.png");
    public static BaseImage tech_tree_dots_black_x05 = new BaseImage(AssetPaths.AssetIconsResearch2, "tech_tree_dots_black_x05.png");
    public static BaseImage tech_tree_dots_black_x08 = new BaseImage(AssetPaths.AssetIconsResearch2, "tech_tree_dots_black_x08.png");
    public static BaseImage tech_tree_dots_black_x10 = new BaseImage(AssetPaths.AssetIconsResearch2, "tech_tree_dots_black_x10.png");
    public static BaseImage tech_tree_dots_black_x16 = new BaseImage(AssetPaths.AssetIconsResearch2, "tech_tree_dots_black_x16.png");
    public static BaseImage tech_tree_dots_black_x15 = new BaseImage(AssetPaths.AssetIconsResearch2, "tech_tree_dots_black_x15.png");
    public static BaseImage tech_tree_dots_black_x13 = new BaseImage(AssetPaths.AssetIconsResearch2, "tech_tree_dots_black_x13.png");
    public static BaseImage tech_tree_dots_green_x16 = new BaseImage(AssetPaths.AssetIconsResearch2, "tech_tree_dots_green_x16.png");
    public static BaseImage r_building_bar_building_info_norm = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_building-info_norm.png");
    public static BaseImage r_building_bar_building_info_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_building-info_over.png");
    public static BaseImage r_building_bar_tab1_arrow_in = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab1-arrow_in.png");
    public static BaseImage r_building_bar_tab1_arrow_norm = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab1-arrow_norm.png");
    public static BaseImage r_building_bar_tab1_arrow_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab1-arrow_over.png");
    public static BaseImage r_building_bar_tab1_in = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab1_in.png");
    public static BaseImage r_building_bar_tab1_norm = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab1_norm.png");
    public static BaseImage r_building_bar_tab1_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab1_over.png");
    public static BaseImage r_building_bar_tab2_arrow_in = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab2-arrow_in.png");
    public static BaseImage r_building_bar_tab2_arrow_norm = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab2-arrow_norm.png");
    public static BaseImage r_building_bar_tab2_arrow_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab2-arrow_over.png");
    public static BaseImage r_building_bar_tab2_in = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab2_in.png");
    public static BaseImage r_building_bar_tab2_norm = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab2_norm.png");
    public static BaseImage r_building_bar_tab2_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab2_over.png");
    public static BaseImage r_building_bar_tab3_arrow_in = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab3-arrow_in.png");
    public static BaseImage r_building_bar_tab3_arrow_norm = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab3-arrow_norm.png");
    public static BaseImage r_building_bar_tab3_arrow_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab3-arrow_over.png");
    public static BaseImage r_building_bar_tab3_in = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab3_in.png");
    public static BaseImage r_building_bar_tab3_norm = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab3_norm.png");
    public static BaseImage r_building_bar_tab3_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab3_over.png");
    public static BaseImage r_building_bar_tab4_arrow_in = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab4-arrow_in.png");
    public static BaseImage r_building_bar_tab4_arrow_norm = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab4-arrow_norm.png");
    public static BaseImage r_building_bar_tab4_arrow_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab4-arrow_over.png");
    public static BaseImage r_building_bar_tab4_in = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab4_in.png");
    public static BaseImage r_building_bar_tab4_norm = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab4_norm.png");
    public static BaseImage r_building_bar_tab4_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab4_over.png");
    public static BaseImage r_building_bar_tab5_arrow_in = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab5-arrow_in.png");
    public static BaseImage r_building_bar_tab5_arrow_norm = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab5-arrow_norm.png");
    public static BaseImage r_building_bar_tab5_arrow_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab5-arrow_over.png");
    public static BaseImage r_building_bar_tab5_in = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab5_in.png");
    public static BaseImage r_building_bar_tab5_norm = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab5_norm.png");
    public static BaseImage r_building_bar_tab5_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_bar_tab5_over.png");
    public static BaseImage r_building_miltary_archer = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_archer.png");
    public static BaseImage r_building_miltary_archer_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_archer_over.png");
    public static BaseImage r_building_miltary_catapult = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_catapult.png");
    public static BaseImage r_building_miltary_catapult_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_catapult_over.png");
    public static BaseImage r_building_miltary_gatehouse = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_gatehouse.png");
    public static BaseImage r_building_miltary_gatehouse_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_gatehouse_over.png");
    public static BaseImage r_building_miltary_greattower = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_greattower.png");
    public static BaseImage r_building_miltary_greattower_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_greattower_over.png");
    public static BaseImage r_building_miltary_guardhouse = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_guardhouse.png");
    public static BaseImage r_building_miltary_guardhouse2 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_guardhouse2.png");
    public static BaseImage r_building_miltary_guardhouse2_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_guardhouse2_over.png");
    public static BaseImage r_building_miltary_guardhouse3 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_guardhouse3.png");
    public static BaseImage r_building_miltary_guardhouse3_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_guardhouse3_over.png");
    public static BaseImage r_building_miltary_guardhouse4 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_guardhouse4.png");
    public static BaseImage r_building_miltary_guardhouse4_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_guardhouse4_over.png");
    public static BaseImage r_building_miltary_guardhouse_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_guardhouse_over.png");
    public static BaseImage r_building_miltary_killingpits = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_killingpits.png");
    public static BaseImage r_building_miltary_killingpits_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_killingpits_over.png");
    public static BaseImage r_building_miltary_largetower = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_largetower.png");
    public static BaseImage r_building_miltary_largetower_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_largetower_over.png");
    public static BaseImage r_building_miltary_lookouttower = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_lookouttower.png");
    public static BaseImage r_building_miltary_lookouttower_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_lookouttower_over.png");
    public static BaseImage r_building_miltary_moat = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_moat.png");
    public static BaseImage r_building_miltary_moat_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_moat_over.png");
    public static BaseImage r_building_miltary_oilpots = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_oilpots.png");
    public static BaseImage r_building_miltary_oilpots_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_oilpots_over.png");
    public static BaseImage r_building_miltary_peasent = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_peasent.png");
    public static BaseImage r_building_miltary_peasent_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_peasent_over.png");
    public static BaseImage r_building_miltary_pikemen = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_pikemen.png");
    public static BaseImage r_building_miltary_pikemen_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_pikemen_over.png");
    public static BaseImage r_building_miltary_pitchrig = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_pitchrig.png");
    public static BaseImage r_building_miltary_pitchrig_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_pitchrig_over.png");
    public static BaseImage r_building_miltary_scout = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_scout.png");
    public static BaseImage r_building_miltary_scout_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_scout_over.png");
    public static BaseImage r_building_miltary_smalltower = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_smalltower.png");
    public static BaseImage r_building_miltary_smalltower_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_smalltower_over.png");
    public static BaseImage r_building_miltary_smelter = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_smelter.png");
    public static BaseImage r_building_miltary_smelter_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_smelter_over.png");
    public static BaseImage r_building_miltary_stonewall = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_stonewall.png");
    public static BaseImage r_building_miltary_stonewall_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_stonewall_over.png");
    public static BaseImage r_building_miltary_swordsman = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_swordsman.png");
    public static BaseImage r_building_miltary_swordsman_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_swordsman_over.png");
    public static BaseImage r_building_miltary_woodtower = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_woodtower.png");
    public static BaseImage r_building_miltary_woodtower_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_woodtower_over.png");
    public static BaseImage r_building_miltary_woodwall = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_woodwall.png");
    public static BaseImage r_building_miltary_woodwall_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_woodwall_over.png");
    public static BaseImage r_building_miltary_stonewallblock = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_stonewallblock.png");
    public static BaseImage r_building_miltary_stonewallblock_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_stonewallblock_over.png");
    public static BaseImage r_building_miltary_woodwallblock = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_woodwallblock.png");
    public static BaseImage r_building_miltary_woodwallblock_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_miltary_woodwallblock_over.png");
    public static BaseImage r_building_panel_back = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_back.png");
    public static BaseImage r_building_panel_bld_icon_food_apple_orchard = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_apple-orchard.png");
    public static BaseImage r_building_panel_bld_icon_food_apple_orchard_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_apple-orchard_over.png");
    public static BaseImage r_building_panel_bld_icon_food_bakery = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_bakery.png");
    public static BaseImage r_building_panel_bld_icon_food_bakery_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_bakery_over.png");
    public static BaseImage r_building_panel_bld_icon_food_brewery = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_brewery.png");
    public static BaseImage r_building_panel_bld_icon_food_brewery_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_brewery_over.png");
    public static BaseImage r_building_panel_bld_icon_food_dairy_farm = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_dairy-farm.png");
    public static BaseImage r_building_panel_bld_icon_food_dairy_farm_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_dairy-farm_over.png");
    public static BaseImage r_building_panel_bld_icon_food_fishing_jetty = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_fishing-jetty.png");
    public static BaseImage r_building_panel_bld_icon_food_fishing_jetty_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_fishing-jetty_over.png");
    public static BaseImage r_building_panel_bld_icon_food_granary = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_granary.png");
    public static BaseImage r_building_panel_bld_icon_food_granary_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_granary_over.png");
    public static BaseImage r_building_panel_bld_icon_food_inn = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_inn.png");
    public static BaseImage r_building_panel_bld_icon_food_inn_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_inn_over.png");
    public static BaseImage r_building_panel_bld_icon_food_pig_farm = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_pig-farm.png");
    public static BaseImage r_building_panel_bld_icon_food_pig_farm_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_pig-farm_over.png");
    public static BaseImage r_building_panel_bld_icon_food_vegetable_farm = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_vegetable-farm.png");
    public static BaseImage r_building_panel_bld_icon_food_vegetable_farm_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_food_vegetable-farm_over.png");
    public static BaseImage r_building_panel_bld_icon_hon_carpenters_workshop = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_carpenters-workshop.png");
    public static BaseImage r_building_panel_bld_icon_hon_carpenters_workshop_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_carpenters-workshop_over.png");
    public static BaseImage r_building_panel_bld_icon_hon_hunters_hut = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_hunters-hut.png");
    public static BaseImage r_building_panel_bld_icon_hon_hunters_hut_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_hunters-hut_over.png");
    public static BaseImage r_building_panel_bld_icon_hon_metalworks_workshop = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_metalworks-workshop.png");
    public static BaseImage r_building_panel_bld_icon_hon_metalworks_workshop_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_metalworks-workshop_over.png");
    public static BaseImage r_building_panel_bld_icon_hon_salt_pan = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_salt-pan.png");
    public static BaseImage r_building_panel_bld_icon_hon_salt_pan_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_salt-pan_over.png");
    public static BaseImage r_building_panel_bld_icon_hon_silk_docs = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_silk-docs.png");
    public static BaseImage r_building_panel_bld_icon_hon_silk_docs_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_silk-docs_over.png");
    public static BaseImage r_building_panel_bld_icon_hon_spice_docs = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_spice-docs.png");
    public static BaseImage r_building_panel_bld_icon_hon_spice_docs_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_spice-docs_over.png");
    public static BaseImage r_building_panel_bld_icon_hon_tailers_workshop = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_tailers-workshop.png");
    public static BaseImage r_building_panel_bld_icon_hon_tailers_workshop_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_tailers-workshop_over.png");
    public static BaseImage r_building_panel_bld_icon_hon_vinyard = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_vinyard.png");
    public static BaseImage r_building_panel_bld_icon_hon_vinyard_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_hon_vinyard_over.png");
    public static BaseImage r_building_panel_bld_icon_ind_iron_mine = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_ind_iron-mine.png");
    public static BaseImage r_building_panel_bld_icon_ind_iron_mine_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_ind_iron-mine_over.png");
    public static BaseImage r_building_panel_bld_icon_ind_market = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_ind_market.png");
    public static BaseImage r_building_panel_bld_icon_ind_market_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_ind_market_over.png");
    public static BaseImage r_building_panel_bld_icon_ind_pitch_rig = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_ind_pitch-rig.png");
    public static BaseImage r_building_panel_bld_icon_ind_pitch_rig_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_ind_pitch-rig_over.png");
    public static BaseImage r_building_panel_bld_icon_ind_stockpile = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_ind_stockpile.png");
    public static BaseImage r_building_panel_bld_icon_ind_stockpile_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_ind_stockpile_over.png");
    public static BaseImage r_building_panel_bld_icon_ind_stone_quarry = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_ind_stone-quarry.png");
    public static BaseImage r_building_panel_bld_icon_ind_stone_quarry_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_ind_stone-quarry_over.png");
    public static BaseImage r_building_panel_bld_icon_ind_woodcutters_hut = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_ind_woodcutters-hut.png");
    public static BaseImage r_building_panel_bld_icon_ind_woodcutters_hut_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_ind_woodcutters-hut_over.png");
    public static BaseImage r_building_panel_bld_icon_mil_armourer = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_mil_armourer.png");
    public static BaseImage r_building_panel_bld_icon_mil_armourer_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_mil_armourer_over.png");
    public static BaseImage r_building_panel_bld_icon_mil_armoury = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_mil_armoury.png");
    public static BaseImage r_building_panel_bld_icon_mil_armoury_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_mil_armoury_over.png");
    public static BaseImage r_building_panel_bld_icon_mil_barracks = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_mil_barracks.png");
    public static BaseImage r_building_panel_bld_icon_mil_barracks_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_mil_barracks_over.png");
    public static BaseImage r_building_panel_bld_icon_mil_blacksmith = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_mil_blacksmith.png");
    public static BaseImage r_building_panel_bld_icon_mil_blacksmith_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_mil_blacksmith_over.png");
    public static BaseImage r_building_panel_bld_icon_mil_fletcher = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_mil_fletcher.png");
    public static BaseImage r_building_panel_bld_icon_mil_fletcher_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_mil_fletcher_over.png");
    public static BaseImage r_building_panel_bld_icon_mil_pole_turner = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_mil_pole-turner.png");
    public static BaseImage r_building_panel_bld_icon_mil_pole_turner_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_mil_pole-turner_over.png");
    public static BaseImage r_building_panel_bld_icon_mil_siege_workshop = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_mil_siege-workshop.png");
    public static BaseImage r_building_panel_bld_icon_mil_siege_workshop_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld-icon_mil_siege-workshop_over.png");
    public static BaseImage r_building_panel_bld_back = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_back.png");
    public static BaseImage r_building_panel_bld_back_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_back_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_dovecote = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_dovecote.png");
    public static BaseImage r_building_panel_bld_civ_dec_dovecote_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_dovecote_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_garden_lrg_variant = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_garden-lrg_variant.png");
    public static BaseImage r_building_panel_bld_civ_dec_garden_lrg_variant_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_garden-lrg_variant_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_garden_med_variant = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_garden-med_variant.png");
    public static BaseImage r_building_panel_bld_civ_dec_garden_med_variant_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_garden-med_variant_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_garden_sml_variant = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_garden-sml_variant.png");
    public static BaseImage r_building_panel_bld_civ_dec_garden_sml_variant_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_garden-sml_variant_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_garden_water_variant = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_garden-water_variant.png");
    public static BaseImage r_building_panel_bld_civ_dec_garden_water_variant_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_garden-water_variant_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_large_garden = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_large-garden.png");
    public static BaseImage r_building_panel_bld_civ_dec_large_garden_01png = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_large-garden_01png.png");
    public static BaseImage r_building_panel_bld_civ_dec_large_garden_01png_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_large-garden_01png_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_large_garden_02 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_large-garden_02.png");
    public static BaseImage r_building_panel_bld_civ_dec_large_garden_02_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_large-garden_02_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_large_garden_03 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_large-garden_03.png");
    public static BaseImage r_building_panel_bld_civ_dec_large_garden_03_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_large-garden_03_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_large_garden_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_large-garden_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_large_statue = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_large-statue.png");
    public static BaseImage r_building_panel_bld_civ_dec_large_statue_01 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_large-statue_01.png");
    public static BaseImage r_building_panel_bld_civ_dec_large_statue_01_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_large-statue_01_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_large_statue_02 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_large-statue_02.png");
    public static BaseImage r_building_panel_bld_civ_dec_large_statue_02_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_large-statue_02_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_large_statue_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_large-statue_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_garden_01 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-garden_01.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_garden_01_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-garden_01_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_garden_02 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-garden_02.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_garden_02_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-garden_02_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_garden_03 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-garden_03.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_garden_03_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-garden_03_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_garden_04 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-garden_04.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_garden_04_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-garden_04_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_garden_05 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-garden_05.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_garden_05_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-garden_05_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_garden_06 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-garden_06.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_garden_06_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-garden_06_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_garden_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-garden_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_statue_01 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-statue_01.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_statue_01_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-statue_01_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_statue_02 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-statue_02.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_statue_02_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-statue_02_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_statue_03 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-statue_03.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_statue_03_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-statue_03_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_statue_04 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-statue_04.png");
    public static BaseImage r_building_panel_bld_civ_dec_small_statue_04_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_small-statue_04_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_statue_lrg_variant = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_statue-lrg_variant.png");
    public static BaseImage r_building_panel_bld_civ_dec_statue_lrg_variant_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_statue-lrg_variant_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_statue_sml_variant = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_statue-sml_variant.png");
    public static BaseImage r_building_panel_bld_civ_dec_statue_sml_variant_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_statue-sml_variant_over.png");
    public static BaseImage r_building_panel_bld_civ_dec_sub_category = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_sub-category.png");
    public static BaseImage r_building_panel_bld_civ_dec_sub_category_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-dec_sub-category_over.png");
    public static BaseImage r_building_panel_bld_civ_ent_sub_category = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-ent_sub-category.png");
    public static BaseImage r_building_panel_bld_civ_ent_sub_category_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-ent_sub-category_over.png");
    public static BaseImage r_building_panel_bld_civ_jus_sub_category = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-jus_sub-category.png");
    public static BaseImage r_building_panel_bld_civ_jus_sub_category_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-jus_sub-category_over.png");
    public static BaseImage r_building_panel_bld_civ_rel_large_church = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_large-church.png");
    public static BaseImage r_building_panel_bld_civ_rel_large_church_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_large-church_over.png");
    public static BaseImage r_building_panel_bld_civ_rel_large_shrines = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_large-shrines.png");
    public static BaseImage r_building_panel_bld_civ_rel_large_shrines_01 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_large-shrines_01.png");
    public static BaseImage r_building_panel_bld_civ_rel_large_shrines_01_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_large-shrines_01_over.png");
    public static BaseImage r_building_panel_bld_civ_rel_large_shrines_02 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_large-shrines_02.png");
    public static BaseImage r_building_panel_bld_civ_rel_large_shrines_02_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_large-shrines_02_over.png");
    public static BaseImage r_building_panel_bld_civ_rel_large_shrines_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_large-shrines_over.png");
    public static BaseImage r_building_panel_bld_civ_rel_medium_church = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_medium-church.png");
    public static BaseImage r_building_panel_bld_civ_rel_medium_church_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_medium-church_over.png");
    public static BaseImage r_building_panel_bld_civ_rel_shrine_lrg_variant = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_shrine-lrg_variant.png");
    public static BaseImage r_building_panel_bld_civ_rel_shrine_lrg_variant_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_shrine-lrg_variant_over.png");
    public static BaseImage r_building_panel_bld_civ_rel_shrine_sml_variant = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_shrine-sml_variant.png");
    public static BaseImage r_building_panel_bld_civ_rel_shrine_sml_variant_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_shrine-sml_variant_over.png");
    public static BaseImage r_building_panel_bld_civ_rel_small_church = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_small-church.png");
    public static BaseImage r_building_panel_bld_civ_rel_small_church_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_small-church_over.png");
    public static BaseImage r_building_panel_bld_civ_rel_small_shrines = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_small-shrines.png");
    public static BaseImage r_building_panel_bld_civ_rel_small_shrines_01 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_small-shrines_01.png");
    public static BaseImage r_building_panel_bld_civ_rel_small_shrines_01_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_small-shrines_01_over.png");
    public static BaseImage r_building_panel_bld_civ_rel_small_shrines_02 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_small-shrines_02.png");
    public static BaseImage r_building_panel_bld_civ_rel_small_shrines_02_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_small-shrines_02_over.png");
    public static BaseImage r_building_panel_bld_civ_rel_small_shrines_03 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_small-shrines_03.png");
    public static BaseImage r_building_panel_bld_civ_rel_small_shrines_03_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_small-shrines_03_over.png");
    public static BaseImage r_building_panel_bld_civ_rel_small_shrines_04 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_small-shrines_04.png");
    public static BaseImage r_building_panel_bld_civ_rel_small_shrines_04_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_small-shrines_04_over.png");
    public static BaseImage r_building_panel_bld_civ_rel_small_shrines_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_small-shrines_over.png");
    public static BaseImage r_building_panel_bld_civ_rel_sub_category = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_sub-category.png");
    public static BaseImage r_building_panel_bld_civ_rel_sub_category_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ-rel_sub-category_over.png");
    public static BaseImage r_building_panel_bld_civ_hall_1 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_hall_1.png");
    public static BaseImage r_building_panel_bld_civ_hall_1_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_hall_1_over.png");
    public static BaseImage r_building_panel_bld_civ_hall_2 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_hall_2.png");
    public static BaseImage r_building_panel_bld_civ_hall_2_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_hall_2_over.png");
    public static BaseImage r_building_panel_bld_civ_hall_3 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_hall_3.png");
    public static BaseImage r_building_panel_bld_civ_hall_3_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_hall_3_over.png");
    public static BaseImage r_building_panel_bld_civ_house_1 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_house_1.png");
    public static BaseImage r_building_panel_bld_civ_house_1_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_house_1_over.png");
    public static BaseImage r_building_panel_bld_civ_house_2 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_house_2.png");
    public static BaseImage r_building_panel_bld_civ_house_2_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_house_2_over.png");
    public static BaseImage r_building_panel_bld_civ_house_3 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_house_3.png");
    public static BaseImage r_building_panel_bld_civ_house_3_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_house_3_over.png");
    public static BaseImage r_building_panel_bld_civ_house_4 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_house_4.png");
    public static BaseImage r_building_panel_bld_civ_house_4_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_house_4_over.png");
    public static BaseImage r_building_panel_bld_civ_house_5 = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_house_5.png");
    public static BaseImage r_building_panel_bld_civ_house_5_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_civ_house_5_over.png");
    public static BaseImage r_building_panel_bld_ent_dancing_bear = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_ent_dancing-bear.png");
    public static BaseImage r_building_panel_bld_ent_dancing_bear_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_ent_dancing-bear_over.png");
    public static BaseImage r_building_panel_bld_ent_jesters_court = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_ent_jesters-court.png");
    public static BaseImage r_building_panel_bld_ent_jesters_court_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_ent_jesters-court_over.png");
    public static BaseImage r_building_panel_bld_ent_maypole = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_ent_maypole.png");
    public static BaseImage r_building_panel_bld_ent_maypole_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_ent_maypole_over.png");
    public static BaseImage r_building_panel_bld_ent_theatre = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_ent_theatre.png");
    public static BaseImage r_building_panel_bld_ent_theatre_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_ent_theatre_over.png");
    public static BaseImage r_building_panel_bld_ent_troubadours_arbor = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_ent_troubadours-arbor.png");
    public static BaseImage r_building_panel_bld_ent_troubadours_arbor_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_ent_troubadours-arbor_over.png");
    public static BaseImage r_building_panel_bld_jus_burning_post = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_jus_burning-post.png");
    public static BaseImage r_building_panel_bld_jus_burning_post_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_jus_burning-post_over.png");
    public static BaseImage r_building_panel_bld_jus_gibbet = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_jus_gibbet.png");
    public static BaseImage r_building_panel_bld_jus_gibbet_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_jus_gibbet_over.png");
    public static BaseImage r_building_panel_bld_jus_stocks = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_jus_stocks.png");
    public static BaseImage r_building_panel_bld_jus_stocks_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_jus_stocks_over.png");
    public static BaseImage r_building_panel_bld_jus_stretching_rack = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_jus_stretching-rack.png");
    public static BaseImage r_building_panel_bld_jus_stretching_rack_over = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_bld_jus_stretching-rack_over.png");
    public static BaseImage r_building_panel_inset = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_inset.png");
    public static BaseImage r_building_panel_inset_icon_clay = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_inset_icon_clay.png");
    public static BaseImage r_building_panel_inset_icon_gold = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_inset_icon_gold.png");
    public static BaseImage r_building_panel_inset_icon_stone = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_inset_icon_stone.png");
    public static BaseImage r_building_panel_inset_icon_time = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_inset_icon_time.png");
    public static BaseImage r_building_panel_inset_icon_wood = new BaseImage(AssetPaths.AssetIconsBuildings, "r_building_panel_inset_icon_wood.png");
    public static BaseImage extrasbar_01 = new BaseImage(AssetPaths.AssetIconsResources, "extrasbar_01.png");
    public static BaseImage extrasbar_01_over = new BaseImage(AssetPaths.AssetIconsResources, "extrasbar_01_over.png");
    public static BaseImage infobar_01 = new BaseImage(AssetPaths.AssetIconsResources, "infobar_01.png");
    public static BaseImage infobar_01_over = new BaseImage(AssetPaths.AssetIconsResources, "infobar_01_over.png");
    public static BaseImage infobar_02 = new BaseImage(AssetPaths.AssetIconsResources, "infobar_02.png");
    public static BaseImage infobar_02_over = new BaseImage(AssetPaths.AssetIconsResources, "infobar_02_over.png");
    public static BaseImage infobar_03 = new BaseImage(AssetPaths.AssetIconsResources, "infobar_03.png");
    public static BaseImage infobar_03_over = new BaseImage(AssetPaths.AssetIconsResources, "infobar_03_over.png");
    public static BaseImage int_but_delete_norm = new BaseImage(AssetPaths.AssetIconsResources, "int_but_delete_norm.png");
    public static BaseImage int_but_delete_over = new BaseImage(AssetPaths.AssetIconsResources, "int_but_delete_over.png");
    public static BaseImage int_but_delete_blue_norm = new BaseImage(AssetPaths.AssetIconsResources, "int_but_delete_blue_norm.png");
    public static BaseImage int_but_delete_blue_over = new BaseImage(AssetPaths.AssetIconsResources, "int_but_delete_blue_over.png");
    public static BaseImage int_but_industry_blank_norm = new BaseImage(AssetPaths.AssetIconsResources, "int_but_industry-blank_norm.png");
    public static BaseImage int_but_industry_blank_over = new BaseImage(AssetPaths.AssetIconsResources, "int_but_industry-blank_over.png");
    public static BaseImage interface_inner_shadow_128_bottom = new BaseImage(AssetPaths.AssetIconsResources, "interface_inner_shadow_128_bottom.png");
    public static BaseImage interface_inner_shadow_128_bottomleft = new BaseImage(AssetPaths.AssetIconsResources, "interface_inner_shadow_128_bottomleft.png");
    public static BaseImage interface_inner_shadow_128_bottomright = new BaseImage(AssetPaths.AssetIconsResources, "interface_inner_shadow_128_bottomright.png");
    public static BaseImage interface_inner_shadow_128_left = new BaseImage(AssetPaths.AssetIconsResources, "interface_inner_shadow_128_left.png");
    public static BaseImage interface_inner_shadow_128_right = new BaseImage(AssetPaths.AssetIconsResources, "interface_inner_shadow_128_right.png");
    public static BaseImage interface_inner_shadow_128_top = new BaseImage(AssetPaths.AssetIconsResources, "interface_inner_shadow_128_top.png");
    public static BaseImage interface_inner_shadow_128_topleft = new BaseImage(AssetPaths.AssetIconsResources, "interface_inner_shadow_128_topleft.png");
    public static BaseImage interface_inner_shadow_128_topright = new BaseImage(AssetPaths.AssetIconsResources, "interface_inner_shadow_128_topright.png");
    public static BaseImage interface_under_shadow_128_bottom = new BaseImage(AssetPaths.AssetIconsResources, "interface_under_shadow_128_bottom.png");
    public static BaseImage interface_under_shadow_128_bottomleft = new BaseImage(AssetPaths.AssetIconsResources, "interface_under_shadow_128_bottomleft.png");
    public static BaseImage interface_under_shadow_128_bottomright = new BaseImage(AssetPaths.AssetIconsResources, "interface_under_shadow_128_bottomright.png");
    public static BaseImage interface_under_shadow_128_left = new BaseImage(AssetPaths.AssetIconsResources, "interface_under_shadow_128_left.png");
    public static BaseImage interface_under_shadow_128_right = new BaseImage(AssetPaths.AssetIconsResources, "interface_under_shadow_128_right.png");
    public static BaseImage interface_under_shadow_128_top = new BaseImage(AssetPaths.AssetIconsResources, "interface_under_shadow_128_top.png");
    public static BaseImage interface_under_shadow_128_topleft = new BaseImage(AssetPaths.AssetIconsResources, "interface_under_shadow_128_topleft.png");
    public static BaseImage interface_under_shadow_128_topright = new BaseImage(AssetPaths.AssetIconsResources, "interface_under_shadow_128_topright.png");
    public static BaseImage VillageTabBar_1_Normal = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_1_Normal.png");
    public static BaseImage VillageTabBar_1_Selected = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_1_Selected.png");
    public static BaseImage VillageTabBar_2_Normal = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_2_Normal.png");
    public static BaseImage VillageTabBar_2_Selected = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_2_Selected.png");
    public static BaseImage VillageTabBar_3_Normal = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_3_Normal.png");
    public static BaseImage VillageTabBar_3_Selected = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_3_Selected.png");
    public static BaseImage VillageTabBar_4_Normal = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_4_Normal.png");
    public static BaseImage VillageTabBar_4_Selected = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_4_Selected.png");
    public static BaseImage VillageTabBar_5_Normal = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_5_Normal.png");
    public static BaseImage VillageTabBar_5_Selected = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_5_Selected.png");
    public static BaseImage VillageTabBar_6_Normal = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_6_Normal.png");
    public static BaseImage VillageTabBar_6_Selected = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_6_Selected.png");
    public static BaseImage VillageTabBar_7_Normal = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_7_Normal.png");
    public static BaseImage VillageTabBar_7_Selected = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_7_Selected.png");
    public static BaseImage VillageTabBar_8_Normal = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_8_Normal.png");
    public static BaseImage VillageTabBar_8_Selected = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_8_Selected.png");
    public static BaseImage VillageTabBar_9_Normal = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_9_Normal.png");
    public static BaseImage VillageTabBar_9_Selected = new BaseImage(AssetPaths.AssetIconsTabs, "VillageTabBar_9_Selected.png");
    public static BaseImage tab_3b_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_3b_normal.png");
    public static BaseImage tab_3b_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_3b_selected.png");
    public static BaseImage tab_3c_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_3c_normal.png");
    public static BaseImage tab_3c_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_3c_selected.png");
    public static BaseImage tab_3_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_3_normal.png");
    public static BaseImage tab_3_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_3_selected.png");
    public static BaseImage tab_4_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_4_normal.png");
    public static BaseImage tab_4_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_4_selected.png");
    public static BaseImage tab_4b_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_4b_normal.png");
    public static BaseImage tab_4b_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_4b_selected.png");
    public static BaseImage tab_5b_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_5b_normal.png");
    public static BaseImage tab_5b_normal_bright = new BaseImage(AssetPaths.AssetIconsTabs, "tab_5b_normal_bright.png");
    public static BaseImage tab_5b_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_5b_selected.png");
    public static BaseImage tab_5b_selected_bright = new BaseImage(AssetPaths.AssetIconsTabs, "tab_5b_selected_bright.png");
    public static BaseImage tab_5_normal_newReports = new BaseImage(AssetPaths.AssetIconsTabs, "tab_5_normal-newReports.png");
    public static BaseImage tab_5_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_5_normal.png");
    public static BaseImage tab_5_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_5_selected.png");
    public static BaseImage tab_6B_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_6B_normal.png");
    public static BaseImage tab_6B_normal_bright = new BaseImage(AssetPaths.AssetIconsTabs, "tab_6B_normal_bright.png");
    public static BaseImage tab_6B_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_6B_selected.png");
    public static BaseImage tab_6B_selected_bright = new BaseImage(AssetPaths.AssetIconsTabs, "tab_6B_selected_bright.png");
    public static BaseImage tab_6_normal_newReports = new BaseImage(AssetPaths.AssetIconsTabs, "tab_6_normal-newReports.png");
    public static BaseImage tab_6_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_6_normal.png");
    public static BaseImage tab_6_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_6_selected.png");
    public static BaseImage tab_7b_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_7b_normal.png");
    public static BaseImage tab_7b_normal_bright = new BaseImage(AssetPaths.AssetIconsTabs, "tab_7b_normal_bright.png");
    public static BaseImage tab_7b_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_7b_selected.png");
    public static BaseImage tab_7_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_7_normal.png");
    public static BaseImage tab_7_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_7_selected.png");
    public static BaseImage tab_8_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_8_normal.png");
    public static BaseImage tab_8_normal_bright = new BaseImage(AssetPaths.AssetIconsTabs, "tab_8_normal_bright.png");
    public static BaseImage tab_8_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_8_selected.png");
    public static BaseImage tab_8_selected_bright = new BaseImage(AssetPaths.AssetIconsTabs, "tab_8_selected_bright.png");
    public static BaseImage tab_9_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_9_normal.png");
    public static BaseImage tab_9_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_9_selected.png");
    public static BaseImage tab_capital_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_capital_normal.png");
    public static BaseImage tab_capital_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_capital_selected.png");
    public static BaseImage tab_quest_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_quest_normal.png");
    public static BaseImage tab_quest_glow = new BaseImage(AssetPaths.AssetIconsTabs, "tab_quest_glow.png");
    public static BaseImage tab_quest_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_quest_selected.png");
    public static BaseImage tab_village_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_village_normal.png");
    public static BaseImage tab_village_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_village_selected.png");
    public static BaseImage tab_world_normal = new BaseImage(AssetPaths.AssetIconsTabs, "tab_world_normal.png");
    public static BaseImage tab_world_rollover = new BaseImage(AssetPaths.AssetIconsTabs, "tab_world_rollover.png");
    public static BaseImage tab_world_selected = new BaseImage(AssetPaths.AssetIconsTabs, "tab_world_selected.png");
    public static BaseImage r_popularity_bar_back_glow = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_back_glow.png");
    public static BaseImage r_popularity_bar_back_green = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_back_green.png");
    public static BaseImage r_popularity_bar_back_red = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_back_red.png");
    public static BaseImage r_popularity_bar_back_yellow = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_back_yellow.png");
    public static BaseImage r_popularity_bar_walker_in = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_in.png");
    public static BaseImage r_popularity_bar_walker_in_x2 = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_in_x2.png");
    public static BaseImage r_popularity_bar_walker_in_x3 = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_in_x3.png");
    public static BaseImage r_popularity_bar_walker_in_x4 = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_in_x4.png");
    public static BaseImage r_popularity_bar_walker_in_x5 = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_in_x5.png");
    public static BaseImage r_popularity_bar_walker_out = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_out.png");
    public static BaseImage r_popularity_bar_walker_out_x2 = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_out_x02.png");
    public static BaseImage r_popularity_bar_walker_out_x3 = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_out_x03.png");
    public static BaseImage r_popularity_bar_walker_out_x4 = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_out_x04.png");
    public static BaseImage r_popularity_bar_walker_out_x5 = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_out_x05.png");
    public static BaseImage r_popularity_bar_walker_out_x6 = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_out_x06.png");
    public static BaseImage r_popularity_bar_walker_out_x7 = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_out_x07.png");
    public static BaseImage r_popularity_bar_walker_out_x8 = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_out_x08.png");
    public static BaseImage r_popularity_bar_walker_out_x9 = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_out_x09.png");
    public static BaseImage r_popularity_bar_walker_out_x10 = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_out_x10.png");
    public static BaseImage r_popularity_bar_walker_stand = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_bar_walker_stand.png");
    public static BaseImage r_popularity_panel_back = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_back.png");
    public static BaseImage r_popularity_panel_but_minus_in = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_but_minus_in.png");
    public static BaseImage r_popularity_panel_but_minus_norm = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_but_minus_norm.png");
    public static BaseImage r_popularity_panel_but_minus_over = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_but_minus_over.png");
    public static BaseImage r_popularity_panel_but_plus_in = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_but_plus_in.png");
    public static BaseImage r_popularity_panel_but_plus_norm = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_but_plus_norm.png");
    public static BaseImage r_popularity_panel_but_plus_over = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_but_plus_over.png");
    public static BaseImage r_popularity_panel_circle_inset_green = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_circle-inset_green.png");
    public static BaseImage r_popularity_panel_circle_inset_red = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_circle-inset_red.png");
    public static BaseImage r_popularity_panel_circle_inset_tan = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_circle-inset_tan.png");
    public static BaseImage r_popularity_panel_colorbar_green_back = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_colorbar_green_back.png");
    public static BaseImage r_popularity_panel_colorbar_green_bar_left = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_colorbar_green_bar-left.png");
    public static BaseImage r_popularity_panel_colorbar_green_bar_mid = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_colorbar_green_bar-mid.png");
    public static BaseImage r_popularity_panel_colorbar_green_bar_right = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_colorbar_green_bar-right.png");
    public static BaseImage r_popularity_panel_colorbar_red_back = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_colorbar_red_back.png");
    public static BaseImage r_popularity_panel_colorbar_red_bar_left = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_colorbar_red_bar-left.png");
    public static BaseImage r_popularity_panel_colorbar_red_bar_mid = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_colorbar_red_bar-mid.png");
    public static BaseImage r_popularity_panel_colorbar_red_bar_right = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_colorbar_red_bar-right.png");
    public static BaseImage r_popularity_panel_events_textbar_green = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_events_textbar_green.png");
    public static BaseImage r_popularity_panel_events_textbar_red = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_events_textbar_red.png");
    public static BaseImage r_popularity_panel_extension_back = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_extension_back.png");
    public static BaseImage r_popularity_panel_icon_ale = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_icon_ale.png");
    public static BaseImage r_popularity_panel_icon_buildings = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_icon_buildings.png");
    public static BaseImage r_popularity_panel_icon_housing = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_icon_housing.png");
    public static BaseImage r_popularity_panel_icon_rations = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_icon_rations.png");
    public static BaseImage r_popularity_panel_icon_taxes = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_icon_taxes.png");
    public static BaseImage r_popularity_panel_indent = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_indent.png");
    public static BaseImage r_popularity_panel_indent_a = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_indent_a.png");
    public static BaseImage r_popularity_panel_indent_b = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_indent_b.png");
    public static BaseImage r_popularity_panel_inset_small = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_inset_small.png");
    public static BaseImage r_popularity_panel_pop_change_green = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_pop-change_green.png");
    public static BaseImage r_popularity_panel_pop_change_red = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_pop-change_red.png");
    public static BaseImage r_popularity_panel_pop_change_yellow = new BaseImage(AssetPaths.AssetIconsPopularity, "r_popularity_panel_pop-change_yellow.png");
    public static BaseImage research_ill_animal_husbandry = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_animal_husbandry.png");
    public static BaseImage research_ill_apple_farming = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_apple_farming.png");
    public static BaseImage research_ill_architecture = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_architecture.png");
    public static BaseImage research_ill_armour_working = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_armour_working.png");
    public static BaseImage research_ill_armoury_capacity = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_armoury_capacity.png");
    public static BaseImage research_ill_arts = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_arts.png");
    public static BaseImage research_ill_bakery = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_bakery.png");
    public static BaseImage research_ill_baptism = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_baptism.png");
    public static BaseImage research_ill_blacksmithing = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_blacksmithing.png");
    public static BaseImage research_ill_boiling_oil = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_boiling_oil.png");
    public static BaseImage research_ill_bounties = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_bounties.png");
    public static BaseImage research_ill_brewing = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_brewing.png");
    public static BaseImage research_ill_butchery = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_butchery.png");
    public static BaseImage research_ill_captains = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_captains.png");
    public static BaseImage research_ill_carpentry = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_carpentry.png");
    public static BaseImage research_ill_castellation = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_castellation.png");
    public static BaseImage research_ill_catapult = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_catapult.png");
    public static BaseImage research_ill_civil_service = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_civil_service.png");
    public static BaseImage research_ill_command = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_command.png");
    public static BaseImage research_ill_commerce = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_commerce.png");
    public static BaseImage research_ill_confession = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_confession.png");
    public static BaseImage research_ill_confirmation = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_confirmation.png");
    public static BaseImage research_ill_conscription = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_conscription.png");
    public static BaseImage research_ill_construction = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_construction.png");
    public static BaseImage research_ill_counter_espionage = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_counter_espionage.png");
    public static BaseImage research_ill_counter_surveillance = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_counter_surveillance.png");
    public static BaseImage research_ill_courtiers = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_courtiers.png");
    public static BaseImage research_ill_craftsmanship = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_craftsmanship.png");
    public static BaseImage research_ill_dairy_farming = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_dairy_farming.png");
    public static BaseImage research_ill_defences = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_defences.png");
    public static BaseImage research_ill_diplomacy = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_diplomacy.png");
    public static BaseImage research_ill_education = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_education.png");
    public static BaseImage research_ill_engineering = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_engineering.png");
    public static BaseImage research_ill_espionage = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_espionage.png");
    public static BaseImage research_ill_eucharist = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_eucharist.png");
    public static BaseImage research_ill_extreme_unction = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_extreme_unction.png");
    public static BaseImage research_ill_farming = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_farming.png");
    public static BaseImage research_ill_fishing = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_fishing.png");
    public static BaseImage research_ill_fletching = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_fletching.png");
    public static BaseImage research_ill_forestry = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_forestry.png");
    public static BaseImage research_ill_fortification = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_fortification.png");
    public static BaseImage research_ill_gardening = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_gardening.png");
    public static BaseImage research_ill_granary_capacity = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_granary_capacity.png");
    public static BaseImage research_ill_guard_houses = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_guard_houses.png");
    public static BaseImage research_ill_hall_capacity = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_hall_capacity.png");
    public static BaseImage research_ill_hops_farming = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_hops_farming.png");
    public static BaseImage research_ill_horsemanship = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_horsemanship.png");
    public static BaseImage research_ill_housing_capacity = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_housing_capacity.png");
    public static BaseImage research_ill_hunting = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_hunting.png");
    public static BaseImage research_ill_industry = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_industry.png");
    public static BaseImage research_ill_inn_capacity = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_inn_capacity.png");
    public static BaseImage research_ill_intelligence = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_intelligence.png");
    public static BaseImage research_ill_intelligence_gathering = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_intelligence_gathering.png");
    public static BaseImage research_ill_iron_mining = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_iron_mining.png");
    public static BaseImage research_ill_justice = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_justice.png");
    public static BaseImage research_ill_land_trade = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_land_trade.png");
    public static BaseImage research_ill_leadership = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_leadership.png");
    public static BaseImage research_ill_literature = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_literature.png");
    public static BaseImage research_ill_longbow = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_longbow.png");
    public static BaseImage research_ill_marriage = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_marriage.png");
    public static BaseImage research_ill_mathematics = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_mathematics.png");
    public static BaseImage research_ill_metal_working = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_metal_working.png");
    public static BaseImage research_ill_military = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_military.png");
    public static BaseImage research_ill_moats = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_moats.png");
    public static BaseImage research_ill_ordination = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_ordination.png");
    public static BaseImage research_ill_overlay = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_overlay.png");
    public static BaseImage research_ill_philosophy = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_philosophy.png");
    public static BaseImage research_ill_pig_breeding = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_pig_breeding.png");
    public static BaseImage research_ill_pike = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_pike.png");
    public static BaseImage research_ill_pilgrimage = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_pilgrimage.png");
    public static BaseImage research_ill_pitch_extraction = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_pitch_extraction.png");
    public static BaseImage research_ill_plough = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_plough.png");
    public static BaseImage research_ill_pole_turning = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_pole_turning.png");
    public static BaseImage research_ill_salt_working = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_salt_working.png");
    public static BaseImage research_ill_scouts = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_scouts.png");
    public static BaseImage research_ill_shipping = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_shipping.png");
    public static BaseImage research_ill_siege_mechanics = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_siege_mechanics.png");
    public static BaseImage research_ill_silk_trade = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_silk_trade.png");
    public static BaseImage research_ill_spice_trade = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_spice_trade.png");
    public static BaseImage research_ill_spy_training = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_spy_training.png");
    public static BaseImage research_ill_stake_traps = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_stake traps.png");
    public static BaseImage research_ill_stockpile_capacity = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_stockpile_capacity.png");
    public static BaseImage research_ill_stone_quarrying = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_stone_quarrying.png");
    public static BaseImage research_ill_surveillance = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_surveillance.png");
    public static BaseImage research_ill_sword = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_sword.png");
    public static BaseImage research_ill_tactics = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_tactics.png");
    public static BaseImage research_ill_tailoring = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_tailoring.png");
    public static BaseImage research_ill_theology = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_theology.png");
    public static BaseImage research_ill_tools = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_tools.png");
    public static BaseImage research_ill_trade_agreements = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_trade_agreements.png");
    public static BaseImage research_ill_vaults = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_vaults.png");
    public static BaseImage research_ill_vegetable_cropping = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_vegetable_cropping.png");
    public static BaseImage research_ill_weapon_making = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_weapon_making.png");
    public static BaseImage research_ill_wheat_farming = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_wheat_farming.png");
    public static BaseImage research_ill_wine_production = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_wine_production.png");
    public static BaseImage research_ill_foraging = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_foraging.png");
    public static BaseImage research_ill_none = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_none.png");
    public static BaseImage research_ill_pillage = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_pillage.png");
    public static BaseImage research_ill_sally_forth = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_sally_forth.png");
    public static BaseImage research_ill_vassalage = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_vassalage.png");
    public static BaseImage research_ill_villages = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_villages.png");
    public static BaseImage research_ill_ransacking = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, "research_ill_ransacking.png");
    public static BaseImage research_ill_forced_march = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, nameof (research_ill_forced_march));
    public static BaseImage research_ill_logistics = new BaseImage(AssetPaths.AssetIconsResearchIllustrations, nameof (research_ill_logistics));
    public static BaseImage com_64_venison_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_venison_on-larger_dropshadow.png");
    public static BaseImage com_64_salt_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_salt_on-larger_dropshadow.png");
    public static BaseImage com_64_spices_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_spice_on-larger_dropshadow.png");
    public static BaseImage com_64_silk_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_silk_on-larger_dropshadow.png");
    public static BaseImage com_64_wine_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_wine_on-larger_dropshadow.png");
    public static BaseImage com_64_furniture_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_furniture_on-larger_dropshadow.png");
    public static BaseImage com_64_clothes_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_clothing_on-larger_dropshadow.png");
    public static BaseImage com_64_metalware_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_metalwork_on-larger_dropshadow.png");
    public static BaseImage com_64_ale_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_ale_on-larger_dropshadow.png");
    public static BaseImage com_64_apples_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_apples_on-larger_dropshadow.png");
    public static BaseImage com_64_armour_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_armour_on-larger_dropshadow.png");
    public static BaseImage com_64_bows_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_bows_on-larger_dropshadow.png");
    public static BaseImage com_64_bread_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_bread_on-larger_dropshadow.png");
    public static BaseImage com_64_catapults_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_catapults_on-larger_dropshadow.png");
    public static BaseImage com_64_cheese_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_cheese_on-larger_dropshadow.png");
    public static BaseImage com_64_fish_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_fish_on-larger_dropshadow.png");
    public static BaseImage com_64_food_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_food_on-larger_dropshadow.png");
    public static BaseImage com_64_honour_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_honour_on-larger_dropshadow.png");
    public static BaseImage com_64_iron_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_iron_on-larger_dropshadow.png");
    public static BaseImage com_64_meat_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_meat_on-larger_dropshadow.png");
    public static BaseImage com_64_money_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_money_on-larger_dropshadow.png");
    public static BaseImage com_64_people_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_people_on-larger_dropshadow.png");
    public static BaseImage com_64_pikes_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_pikes_on-larger_dropshadow.png");
    public static BaseImage com_64_pitch_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_pitch_on-larger_dropshadow.png");
    public static BaseImage com_64_stone_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_stone_on-larger_dropshadow.png");
    public static BaseImage com_64_swords_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_swords_on-larger_dropshadow.png");
    public static BaseImage com_64_veg_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_veg_on-larger_dropshadow.png");
    public static BaseImage com_64_wood_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_64_wood_on-larger_dropshadow.png");
    public static BaseImage com_32_venison_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_venison_on-larger_dropshadow.png");
    public static BaseImage com_32_salt_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_salt_on-larger_dropshadow.png");
    public static BaseImage com_32_spices_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_spice_on-larger_dropshadow.png");
    public static BaseImage com_32_silk_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_silk_on-larger_dropshadow.png");
    public static BaseImage com_32_wine_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_wine_on-larger_dropshadow.png");
    public static BaseImage com_32_furniture_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_furniture_on-larger_dropshadow.png");
    public static BaseImage com_32_clothes_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_clothing_on-larger_dropshadow.png");
    public static BaseImage com_32_metalware_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_metalwork_on-larger_dropshadow.png");
    public static BaseImage com_32_ale_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_ale_on-larger_dropshadow.png");
    public static BaseImage com_32_apples_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_apples_on-larger_dropshadow.png");
    public static BaseImage com_32_armour_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_armour_on-larger_dropshadow.png");
    public static BaseImage com_32_bows_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_bows_on-larger_dropshadow.png");
    public static BaseImage com_32_bread_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_bread_on-larger_dropshadow.png");
    public static BaseImage com_32_catapults_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_catapults_on-larger_dropshadow.png");
    public static BaseImage com_32_cheese_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_cheese_on-larger_dropshadow.png");
    public static BaseImage com_32_fish_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_fish_on-larger_dropshadow.png");
    public static BaseImage com_32_food_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_food_on-larger_dropshadow.png");
    public static BaseImage com_32_honour_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_honour_on-larger_dropshadow.png");
    public static BaseImage com_32_iron_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_iron_on-larger_dropshadow.png");
    public static BaseImage com_32_meat_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_meat_on-larger_dropshadow.png");
    public static BaseImage com_32_money_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_money_on-larger_dropshadow.png");
    public static BaseImage com_32_people_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_people_on-larger_dropshadow.png");
    public static BaseImage com_32_pikes_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_pikes_on-larger_dropshadow.png");
    public static BaseImage com_32_pitch_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_pitch_on-larger_dropshadow.png");
    public static BaseImage com_32_stone_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_stone_on-larger_dropshadow.png");
    public static BaseImage com_32_swords_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_swords_on-larger_dropshadow.png");
    public static BaseImage com_32_veg_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_veg_on-larger_dropshadow.png");
    public static BaseImage com_32_wood_DS = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_wood_on-larger_dropshadow.png");
    public static BaseImage com_32_honor_on_larger_dropshadow = new BaseImage(AssetPaths.AssetIconsCommodities, "com_32_honor_on-larger_dropshadow.png");
    public static BaseImage body_background_001 = new BaseImage(AssetPaths.AssetIconsCommon, "body_background_001.png");
    public static BaseImage body_background_002 = new BaseImage(AssetPaths.AssetIconsCommon, "body_background_002.png");
    public static BaseImage body_background_canvas = new BaseImage(AssetPaths.AssetIconsCommon, "background_canvas.png");
    public static BaseImage body_background_canvas_left_edge = new BaseImage(AssetPaths.AssetIconsCommon, "background_canvas_left_edge.png");
    public static BaseImage panel_border_top_left = new BaseImage(AssetPaths.AssetIconsCommon, "panel_corner.png");
    public static BaseImage panel_border_top = new BaseImage(AssetPaths.AssetIconsCommon, "panel_blank.png");
    public static BaseImage panel_border_left = new BaseImage(AssetPaths.AssetIconsCommon, "panel_blank.png");
    public static BaseImage int_banquette_background_tile_orig = new BaseImage(AssetPaths.AssetIconsCommon, "int_banquette_background_tile.png");
    public static BaseImage int_banquette_background_tile = new BaseImage(AssetPaths.AssetIconsCommon, "int_banquette_background_tile_tan.png");
    public static BaseImage int_banquette_background_tile_tan = new BaseImage(AssetPaths.AssetIconsCommon, "int_banquette_background_tile_tan.png");
    public static BaseImage int_button_close_normal = new BaseImage(AssetPaths.AssetIconsCommon, "int_button_close_normal.png");
    public static BaseImage int_button_close_over = new BaseImage(AssetPaths.AssetIconsCommon, "int_button_close_over.png");
    public static BaseImage int_button_close_in = new BaseImage(AssetPaths.AssetIconsCommon, "int_button_close_in.png");
    public static BaseImage int_button_Q_normal = new BaseImage(AssetPaths.AssetIconsCommon, "int_button_Q_normal.png");
    public static BaseImage int_button_Q_over = new BaseImage(AssetPaths.AssetIconsCommon, "int_button_Q_over.png");
    public static BaseImage int_button_Q_in = new BaseImage(AssetPaths.AssetIconsCommon, "int_button_Q_in.png");
    public static BaseImage int_insetbar_a_left = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetbar-a_left.png");
    public static BaseImage int_insetbar_a_middle = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetbar-a_middle.png");
    public static BaseImage int_insetbar_a_right = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetbar-a_right.png");
    public static BaseImage int_insetpanel_a_bottom_left = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetpanel-a_bottom-left.png");
    public static BaseImage int_insetpanel_a_bottom_right = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetpanel-a_bottom-right.png");
    public static BaseImage int_insetpanel_a_middle_bottom = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetpanel-a_middle-bottom.png");
    public static BaseImage int_insetpanel_a_middle_left = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetpanel-a_middle-left.png");
    public static BaseImage int_insetpanel_a_middle_right = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetpanel-a_middle-right.png");
    public static BaseImage int_insetpanel_a_middle_top = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetpanel-a_middle-top.png");
    public static BaseImage int_insetpanel_a_middle = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetpanel-a_middle.png");
    public static BaseImage int_insetpanel_a_top_left = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetpanel-a_top-left.png");
    public static BaseImage int_insetpanel_a_top_right = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetpanel-a_top-right.png");
    public static BaseImage int_buttonbar_left_normal = new BaseImage(AssetPaths.AssetIconsCommon, "int_buttonbar-left_normal.png");
    public static BaseImage int_buttonbar_left_over = new BaseImage(AssetPaths.AssetIconsCommon, "int_buttonbar-left_over.png");
    public static BaseImage int_buttonbar_middle_normal = new BaseImage(AssetPaths.AssetIconsCommon, "int_buttonbar-middle_normal.png");
    public static BaseImage int_buttonbar_middle_over = new BaseImage(AssetPaths.AssetIconsCommon, "int_buttonbar-middle_over.png");
    public static BaseImage int_buttonbar_right_normal = new BaseImage(AssetPaths.AssetIconsCommon, "int_buttonbar-right_normal.png");
    public static BaseImage int_buttonbar_right_over = new BaseImage(AssetPaths.AssetIconsCommon, "int_buttonbar-right_over.png");
    public static BaseImage int_insetpanel_lighten_bottom_left = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetpanel-lighten_bottom-left.png");
    public static BaseImage int_insetpanel_lighten_bottom_right = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetpanel-lighten_bottom-right.png");
    public static BaseImage int_insetpanel_lighten_middle = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetpanel-lighten_middle.png");
    public static BaseImage int_insetpanel_lighten_top_left = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetpanel-lighten_top-left.png");
    public static BaseImage int_insetpanel_lighten_top_right = new BaseImage(AssetPaths.AssetIconsCommon, "int_insetpanel-lighten_top-right.png");
    public static BaseImage int_parenthesis_left = new BaseImage(AssetPaths.AssetIconsCommon, "int_parenthesis_left.png");
    public static BaseImage int_parenthesis_right = new BaseImage(AssetPaths.AssetIconsCommon, "int_parenthesis_right.png");
    public static BaseImage int_multiplyer_shadow_x1 = new BaseImage(AssetPaths.AssetIconsCommon, "int_multiplyer_shadow_x1.png");
    public static BaseImage int_multiplyer_shadow_x2 = new BaseImage(AssetPaths.AssetIconsCommon, "int_multiplyer_shadow_x2.png");
    public static BaseImage int_multiplyer_shadow_x3 = new BaseImage(AssetPaths.AssetIconsCommon, "int_multiplyer_shadow_x3.png");
    public static BaseImage r_bld_icon_mil_guardhouse_2 = new BaseImage(AssetPaths.AssetIconsCommon, "r_bld-icon_mil_guardhouse_2.png");
    public static BaseImage r_bld_icon_mil_guardhouse_2_over = new BaseImage(AssetPaths.AssetIconsCommon, "r_bld-icon_mil_guardhouse_2_over.png");
    public static BaseImage r_bld_icon_mil_guardhouse_3 = new BaseImage(AssetPaths.AssetIconsCommon, "r_bld-icon_mil_guardhouse_3.png");
    public static BaseImage r_bld_icon_mil_guardhouse_3_over = new BaseImage(AssetPaths.AssetIconsCommon, "r_bld-icon_mil_guardhouse_3_over.png");
    public static BaseImage r_bld_icon_mil_guardhouse_4 = new BaseImage(AssetPaths.AssetIconsCommon, "r_bld-icon_mil_guardhouse_4.png");
    public static BaseImage r_bld_icon_mil_guardhouse_4_over = new BaseImage(AssetPaths.AssetIconsCommon, "r_bld-icon_mil_guardhouse_4_over.png");
    public static BaseImage r_popularity_panel_events_illustration_bandits = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_bandits.png");
    public static BaseImage r_popularity_panel_events_illustration_beginner = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_beginner.png");
    public static BaseImage r_popularity_panel_events_illustration_plague = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_plague.png");
    public static BaseImage r_popularity_panel_events_illustration_rats = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_rats.png");
    public static BaseImage r_popularity_panel_events_illustration_rebellion = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_rebellion.png");
    public static BaseImage r_popularity_panel_events_illustration_storms = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_storms.png");
    public static BaseImage r_popularity_panel_events_illustration_weather_bad_1 = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_weather_-1.png");
    public static BaseImage r_popularity_panel_events_illustration_weather_bad_2 = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_weather_-2.png");
    public static BaseImage r_popularity_panel_events_illustration_weather_bad_3 = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_weather_-3.png");
    public static BaseImage r_popularity_panel_events_illustration_weather_bad_4 = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_weather_-4.png");
    public static BaseImage r_popularity_panel_events_illustration_weather_bad_5 = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_weather_-5.png");
    public static BaseImage r_popularity_panel_events_illustration_weather_neutral = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_weather_0.png");
    public static BaseImage r_popularity_panel_events_illustration_weather_good_1 = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_weather_plus1.png");
    public static BaseImage r_popularity_panel_events_illustration_weather_good_2 = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_weather_plus2.png");
    public static BaseImage r_popularity_panel_events_illustration_weather_good_3 = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_weather_plus3.png");
    public static BaseImage r_popularity_panel_events_illustration_weather_good_4 = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_weather_plus4.png");
    public static BaseImage r_popularity_panel_events_illustration_weather_good_5 = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_weather_plus5.png");
    public static BaseImage r_popularity_panel_events_illustration_wolves = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_wolves.png");
    public static BaseImage r_popularity_panel_events_illustration_castle = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_castle.png");
    public static BaseImage r_popularity_panel_events_illustration_castle_green = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_castle_green.png");
    public static BaseImage r_popularity_panel_events_illustration_blessing = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_blessings.png");
    public static BaseImage r_popularity_panel_events_illustration_inquisition = new BaseImage(AssetPaths.AssetIconsCommon, "r_popularity_panel_events_illustration_Inquisition.png");
    public static BaseImage popularityFace = new BaseImage(AssetPaths.AssetIconsCommon, "popularity_face.png");
    public static BaseImage int_tax_panel_back_semipopulated = new BaseImage(AssetPaths.AssetIconsCommon, "int_tax_panel_back_semipopulated.png");
    public static BaseImage interface_bar_top_left_empty = new BaseImage(AssetPaths.AssetIconsCommon, "menubar_left.png");
    public static BaseImage goods_background = new BaseImage(AssetPaths.AssetIconsCommon, "goods_background.png");
    public static BaseImage building_icon_circle = new BaseImage(AssetPaths.AssetIconsCommon, "building_icon_circle.png");
    public static BaseImage r_building_panel_inset_small = new BaseImage(AssetPaths.AssetIconsCommon, "r_building_panel_inset_small.png");
    public static BaseImage lite_9slice_panel_top_left = new BaseImage(AssetPaths.AssetIconsCommon, "nineslice_lite_inset_top_left");
    public static BaseImage lite_9slice_panel_top_mid = new BaseImage(AssetPaths.AssetIconsCommon, "nineslice_lite_inset_top_mid");
    public static BaseImage lite_9slice_panel_top_right = new BaseImage(AssetPaths.AssetIconsCommon, "nineslice_lite_inset_top_right");
    public static BaseImage lite_9slice_panel_mid_left = new BaseImage(AssetPaths.AssetIconsCommon, "nineslice_lite_inset_mid_left");
    public static BaseImage lite_9slice_panel_mid_mid = new BaseImage(AssetPaths.AssetIconsCommon, "nineslice_lite_inset_mid_mid");
    public static BaseImage lite_9slice_panel_mid_right = new BaseImage(AssetPaths.AssetIconsCommon, "nineslice_lite_inset_mid_right");
    public static BaseImage lite_9slice_panel_bottom_left = new BaseImage(AssetPaths.AssetIconsCommon, "nineslice_lite_inset_bottom_left");
    public static BaseImage lite_9slice_panel_bottom_mid = new BaseImage(AssetPaths.AssetIconsCommon, "nineslice_lite_inset_bottom_mid");
    public static BaseImage lite_9slice_panel_bottom_right = new BaseImage(AssetPaths.AssetIconsCommon, "nineslice_lite_inset_bottom_right");
    public static BaseImage brown_24wide_thumb_bottom = new BaseImage(AssetPaths.AssetIconsCommon, "brown_24wide_thumb_bottom.png");
    public static BaseImage brown_24wide_thumb_middle = new BaseImage(AssetPaths.AssetIconsCommon, "brown_24wide_thumb_middle.png");
    public static BaseImage brown_24wide_thumb_top = new BaseImage(AssetPaths.AssetIconsCommon, "brown_24wide_thumb_top.png");
    public static BaseImage brown_mail2_button_blue_141wide_normal = new BaseImage(AssetPaths.AssetIconsCommon, "brown_button_blue_141wide_normal.png");
    public static BaseImage brown_mail2_button_blue_141wide_over = new BaseImage(AssetPaths.AssetIconsCommon, "brown_button_blue_141wide_over.png");
    public static BaseImage brown_mail2_button_blue_141wide_pushed = new BaseImage(AssetPaths.AssetIconsCommon, "brown_button_blue_141wide_pushed.png");
    public static BaseImage brown_misc_button_blue_210wide_normal = new BaseImage(AssetPaths.AssetIconsCommon, "brown_button_blue_210wide_normal.png");
    public static BaseImage brown_misc_button_blue_210wide_pushed = new BaseImage(AssetPaths.AssetIconsCommon, "brown_button_blue_210wide_pushed.png");
    public static BaseImage brown_misc_button_blue_210wide_over = new BaseImage(AssetPaths.AssetIconsCommon, "brown_button_blue_210wide_over.png");
    public static BaseImage brown_mail2_field_bar_mail_divider = new BaseImage(AssetPaths.AssetIconsCommon, "brown_field_bar_mail_divider.png");
    public static BaseImage brown_mail2_field_bar_mail_left = new BaseImage(AssetPaths.AssetIconsCommon, "brown_field_bar_mail_left.png");
    public static BaseImage brown_mail2_field_bar_mail_middle = new BaseImage(AssetPaths.AssetIconsCommon, "brown_field_bar_mail_middle.png");
    public static BaseImage brown_mail2_field_bar_mail_right = new BaseImage(AssetPaths.AssetIconsCommon, "brown_field_bar_mail_right.png");
    public static BaseImage brown_lineitem_strip_02_dark = new BaseImage(AssetPaths.AssetIconsCommon, "brown_lineitem_strip_02_dark.png");
    public static BaseImage brown_lineitem_strip_02_light = new BaseImage(AssetPaths.AssetIconsCommon, "brown_lineitem_strip_02_light.png");
    public static BaseImage[] se_tabs = BaseImage.createFromUV(AssetPaths.AssetIconsStockExchange, "tabs_market", 4);
    public static BaseImage[] int_hilow_buttons = BaseImage.createFromUV(AssetPaths.AssetIconsStockExchange, "int_button_hi_low", 6);
    public static BaseImage int_insetpanel_b_bottom_left = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_insetpanel-b_bottom_left.png");
    public static BaseImage int_insetpanel_b_bottom_right = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_insetpanel-b_bottom_right.png");
    public static BaseImage int_insetpanel_b_middle_bottom = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_insetpanel-b_bottom_middle.png");
    public static BaseImage int_insetpanel_b_middle_left = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_insetpanel-b_middle_left.png");
    public static BaseImage int_insetpanel_b_middle_right = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_insetpanel-b_middle_right.png");
    public static BaseImage int_insetpanel_b_middle_top = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_insetpanel-b_top_middle.png");
    public static BaseImage int_insetpanel_b_middle = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_insetpanel-b_middle_middle.png");
    public static BaseImage int_insetpanel_b_top_left = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_insetpanel-b_top_left.png");
    public static BaseImage int_insetpanel_b_top_right = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_insetpanel-b_top_right.png");
    public static BaseImage int_button_droparrow_down = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_button_droparrow_down.png");
    public static BaseImage int_button_droparrow_normal = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_button_droparrow_normal.png");
    public static BaseImage int_button_droparrow_over = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_button_droparrow_over.png");
    public static BaseImage int_button_droparrow_up_down = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_button_droparrow-up_down.png");
    public static BaseImage int_button_droparrow_up_normal = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_button_droparrow-up_normal.png");
    public static BaseImage int_button_droparrow_up_over = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_button_droparrow-up_over.png");
    public static BaseImage int_button_findonmap_in = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_button_findonmap_in.png");
    public static BaseImage int_button_findonmap_normal = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_button_findonmap_normal.png");
    public static BaseImage int_button_findonmap_over = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_button_findonmap_over.png");
    public static BaseImage int_icon_trader = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_icon_trader.png");
    public static BaseImage int_lineitem_inset_left = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_lineitem_inset_left.png");
    public static BaseImage int_lineitem_inset_middle = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_lineitem_inset_middle.png");
    public static BaseImage int_lineitem_inset_right = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_lineitem_inset_right.png");
    public static BaseImage int_slidebar_ruler = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_slidebar_ruler.png");
    public static BaseImage int_slidebar_thumb_left_in = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_slidebar_thumb_left_in.png");
    public static BaseImage int_slidebar_thumb_left_normal = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_slidebar_thumb_left_normal.png");
    public static BaseImage int_slidebar_thumb_left_over = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_slidebar_thumb_left_over.png");
    public static BaseImage int_slidebar_thumb_middle_in = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_slidebar_thumb_middle_in.png");
    public static BaseImage int_slidebar_thumb_middle_normal = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_slidebar_thumb_middle_normal.png");
    public static BaseImage int_slidebar_thumb_middle_over = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_slidebar_thumb_middle_over.png");
    public static BaseImage int_slidebar_thumb_right_in = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_slidebar_thumb_right_in.png");
    public static BaseImage int_slidebar_thumb_right_normal = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_slidebar_thumb_right_normal.png");
    public static BaseImage int_slidebar_thumb_right_over = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_slidebar_thumb_right_over.png");
    public static BaseImage int_storage_tab_01_normal = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_storage_tab_01_normal.png");
    public static BaseImage int_storage_tab_01_over = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_storage_tab_01_over.png");
    public static BaseImage int_storage_tab_01_selected = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_storage_tab_01_selected.png");
    public static BaseImage int_storage_tab_02_normal = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_storage_tab_02_normal.png");
    public static BaseImage int_storage_tab_02_over = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_storage_tab_02_over.png");
    public static BaseImage int_storage_tab_02_selected = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_storage_tab_02_selected.png");
    public static BaseImage int_storage_tab_03_normal = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_storage_tab_03_normal.png");
    public static BaseImage int_storage_tab_03_over = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_storage_tab_03_over.png");
    public static BaseImage int_storage_tab_03_selected = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_storage_tab_03_selected.png");
    public static BaseImage int_storage_tab_04_normal = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_storage_tab_04_normal.png");
    public static BaseImage int_storage_tab_04_over = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_storage_tab_04_over.png");
    public static BaseImage int_storage_tab_04_selected = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_storage_tab_04_selected.png");
    public static BaseImage int_white_highlight_bar = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_white_highlight_bar.png");
    public static BaseImage int_villagelist_panel = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_villagelist_panel.png");
    public static BaseImage int_villagelist_panel_highlight = new BaseImage(AssetPaths.AssetIconsStockExchange, "int_villagelist_panel-highlight.png");
    public static BaseImage tab_villagename_forward = new BaseImage(AssetPaths.AssetIconsStockExchange, "tab_villagename_forward.png");
    public static BaseImage tab_villagename_back = new BaseImage(AssetPaths.AssetIconsStockExchange, "tab_villagename_back.png");
    public static BaseImage tab_villagename_over = new BaseImage(AssetPaths.AssetIconsStockExchange, "tab_villagename_over.png");
    public static BaseImage star_market_1 = new BaseImage(AssetPaths.AssetIconsStockExchange, "star_market_01.png");
    public static BaseImage star_market_2 = new BaseImage(AssetPaths.AssetIconsStockExchange, "star_market_02.png");
    public static BaseImage[] medal_images = BaseImage.createFromUV(AssetPaths.AssetIconsAchievements, nameof (medal_images), 58);
    public static BaseImage[] achievement_ribbons_base = new BaseImage[13]
    {
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_solid_blue_desat"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_solid_cyan_desat"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_solid_green_desat"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_solid_grey_desat"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_solid_kelly_desat"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_solid_liteblue_desat"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_solid_magenta_desat"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_solid_mint_desat"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_solid_orange_desat"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_solid_pink_desat"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_solid_purple_desat"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_solid_red_desat"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_solid_yellow_desat")
    };
    public static BaseImage[] achievement_ribbons_edges = new BaseImage[13]
    {
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_edges_blue"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_edges_cyan"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_edges_green"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_edges_grey"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_edges_kelly"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_edges_liteblue"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_edges_magenta"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_edges_mint"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_edges_orange"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_edges_pink"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_edges_purple"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_edges_red"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_edges_yellow")
    };
    public static BaseImage[] achievement_ribbons_centre = new BaseImage[13]
    {
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_centerstripe_blue"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_centerstripe_cyan"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_centerstripe_green"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_centerstripe_grey"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_centerstripe_kelly"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_centerstripe_liteblue"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_centerstripe_magenta"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_centerstripe_mint"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_centerstripe_orange"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_centerstripe_pink"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_centerstripe_purple"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_centerstripe_red"),
      new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_centerstripe_yellow")
    };
    public static BaseImage achievement_woodback_middletile = new BaseImage(AssetPaths.AssetIconsAchievements, "achievements_woodback_middletile");
    public static BaseImage achievements_woodback_top_inset = new BaseImage(AssetPaths.AssetIconsAchievements, nameof (achievements_woodback_top_inset));
    public static BaseImage panel_cover_top = new BaseImage(AssetPaths.AssetIconsAchievements, nameof (panel_cover_top));
    public static BaseImage panel_cover_bottom = new BaseImage(AssetPaths.AssetIconsAchievements, nameof (panel_cover_bottom));
    public static BaseImage char_achievementOverlay = new BaseImage(AssetPaths.AssetIconsAchievements, "char_achievement_overlay");
    public static BaseImage ribbon_comp_centerstripe_gold = new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_centerstripe_gold.png");
    public static BaseImage ribbon_comp_centerstripe_silver = new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_centerstripe_silver.png");
    public static BaseImage ribbon_comp_nail = new BaseImage(AssetPaths.AssetIconsAchievements, "ribbon_comp_nail.png");
    public static BaseImage[] captains_commands_icons = BaseImage.createFromUV(AssetPaths.AssetIconsCastle, "CaptainCommands", 24);
    public static BaseImage castlebar_defenses_normal = new BaseImage(AssetPaths.AssetIconsCastle, "castlebar_defenses_normal.png");
    public static BaseImage castlebar_defenses_selected = new BaseImage(AssetPaths.AssetIconsCastle, "castlebar_defenses_selected.png");
    public static BaseImage castlebar_stone_normal = new BaseImage(AssetPaths.AssetIconsCastle, "castlebar_stone_normal.png");
    public static BaseImage castlebar_stone_selected = new BaseImage(AssetPaths.AssetIconsCastle, "castlebar_stone_selected.png");
    public static BaseImage castlebar_unit_normal = new BaseImage(AssetPaths.AssetIconsCastle, "castlebar_unit_normal.png");
    public static BaseImage castlebar_unit_selected = new BaseImage(AssetPaths.AssetIconsCastle, "castlebar_unit_selected.png");
    public static BaseImage castlebar_wood_normal = new BaseImage(AssetPaths.AssetIconsCastle, "castlebar_wood_normal.png");
    public static BaseImage castlebar_wood_selected = new BaseImage(AssetPaths.AssetIconsCastle, "castlebar_wood_selected.png");
    public static BaseImage castlebar_lock_normal = new BaseImage(AssetPaths.AssetIconsCastle, "castlebar_lock_normal.png");
    public static BaseImage castlebar_lock_selected = new BaseImage(AssetPaths.AssetIconsCastle, "castlebar_lock_selected.png");
    public static BaseImage castlescreen_panelback_A = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_panelback_A.png");
    public static BaseImage castlescreen_panelback_B = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_panelback_B.png");
    public static BaseImage castlescreen_panelback_C = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_panelback_C.png");
    public static BaseImage castlescreen_panel_halfinset_def_select = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_panel-halfinset_def-select.png");
    public static BaseImage castlescreen_panel_halfinset_off_select = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_panel-halfinset_off-select.png");
    public static BaseImage castlescreen_sendback_normal = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_sendback_normal.png");
    public static BaseImage castlescreen_sendback_over = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_sendback_over.png");
    public static BaseImage castlescreen_stance_def_normal = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_stance-def_normal.png");
    public static BaseImage castlescreen_stance_def_over = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_stance-def_over.png");
    public static BaseImage castlescreen_stance_mix_normal = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_stance-mix_normal.png");
    public static BaseImage castlescreen_stance_mix_over = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_stance-mix_over.png");
    public static BaseImage castlescreen_stance_off_normal = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_stance-off_normal.png");
    public static BaseImage castlescreen_stance_off_over = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_stance-off_over.png");
    public static BaseImage castlescreen_take_from_castle = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_take-from-castle.png");
    public static BaseImage castlescreen_unitbrush_1x1_normal = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_unitbrush_1x1_normal.png");
    public static BaseImage castlescreen_unitbrush_1x1_over = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_unitbrush_1x1_over.png");
    public static BaseImage castlescreen_unitbrush_1x5_normal = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_unitbrush_1x5_normal.png");
    public static BaseImage castlescreen_unitbrush_1x5_over = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_unitbrush_1x5_over.png");
    public static BaseImage castlescreen_unitbrush_3x3_normal = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_unitbrush_3x3_normal.png");
    public static BaseImage castlescreen_unitbrush_3x3_over = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_unitbrush_3x3_over.png");
    public static BaseImage castlescreen_unitbrush_5x5_normal = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_unitbrush_5x5_normal.png");
    public static BaseImage castlescreen_unitbrush_5x5_over = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_unitbrush_5x5_over.png");
    public static BaseImage castlescreen_unit_capsule = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_unit-capsule.png");
    public static BaseImage r_building_miltary_archer_green = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_archer_green.png");
    public static BaseImage r_building_miltary_archer_over_green = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_archer_over_green.png");
    public static BaseImage r_building_miltary_captain_normal = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_captain_normal.png");
    public static BaseImage r_building_miltary_captain_over = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_captain_over.png");
    public static BaseImage r_building_miltary_castleinfo_normal = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_castleinfo_normal.png");
    public static BaseImage r_building_miltary_castleinfo_over = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_castleinfo_over.png");
    public static BaseImage r_building_miltary_deletemode_off_normal = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_deletemode-off_normal.png");
    public static BaseImage r_building_miltary_deletemode_off_over = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_deletemode-off_over.png");
    public static BaseImage r_building_miltary_deletemode_on_normal = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_deletemode-on_normal.png");
    public static BaseImage r_building_miltary_deletemode_on_over = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_deletemode-on_over.png");
    public static BaseImage r_building_miltary_flag_normal = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_flag_normal.png");
    public static BaseImage r_building_miltary_flag_over = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_flag_over.png");
    public static BaseImage r_building_miltary_peasent_green = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_peasent_green.png");
    public static BaseImage r_building_miltary_peasent_over_green = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_peasent_over_green.png");
    public static BaseImage r_building_miltary_pikemen_green = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_pikemen_green.png");
    public static BaseImage r_building_miltary_pikemen_over_green = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_pikemen_over_green.png");
    public static BaseImage r_building_miltary_repair_normal = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_repair_normal.png");
    public static BaseImage r_building_miltary_repair_over = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_repair_over.png");
    public static BaseImage r_building_miltary_repair_pushed = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_repair_pushed.png");
    public static BaseImage r_building_miltary_sub_category_units = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_sub-category_units.png");
    public static BaseImage r_building_miltary_sub_category_units_green = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_sub-category_units_green.png");
    public static BaseImage r_building_miltary_sub_category_units_over = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_sub-category_units_over.png");
    public static BaseImage r_building_miltary_sub_category_units_over_green = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_sub-category_units_over_green.png");
    public static BaseImage r_building_miltary_swordsman_green = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_swordsman_green.png");
    public static BaseImage r_building_miltary_swordsman_over_green = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_swordsman_over_green.png");
    public static BaseImage r_building_miltary_viewmode_normal = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_viewmode_normal.png");
    public static BaseImage r_building_miltary_viewmode_over = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_viewmode_over.png");
    public static BaseImage r_building_miltary_viewmode_pushed = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_viewmode_pushed.png");
    public static BaseImage r_building_miltary_ballista = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_ballista.png");
    public static BaseImage r_building_miltary_ballista_over = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_ballista_over.png");
    public static BaseImage r_building_miltary_tunnels = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_tunnels.png");
    public static BaseImage r_building_miltary_tunnels_over = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_tunnels_over.png");
    public static BaseImage r_building_miltary_turrets = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_turrets.png");
    public static BaseImage r_building_miltary_turrets_over = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_turrets_over.png");
    public static BaseImage r_building_miltary_bombard = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_bombard.png");
    public static BaseImage r_building_miltary_bombard_over = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_bombard_over.png");
    public static BaseImage castlebar_unit_over = new BaseImage(AssetPaths.AssetIconsCastle, "castlebar_unit_over.png");
    public static BaseImage castlebar_wood_over = new BaseImage(AssetPaths.AssetIconsCastle, "castlebar_wood_over.png");
    public static BaseImage castlebar_stone_overl = new BaseImage(AssetPaths.AssetIconsCastle, "castlebar_stone_overl.png");
    public static BaseImage castlebar_defenses_over = new BaseImage(AssetPaths.AssetIconsCastle, "castlebar_defenses_over.png");
    public static BaseImage castlebar_lock_over = new BaseImage(AssetPaths.AssetIconsCastle, "castlebar_lock_over.png");
    public static BaseImage r_building_miltary_gatehouse_wood_over = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_gatehouse_wood_over.png");
    public static BaseImage r_building_miltary_gatehouse_wood = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_gatehouse_wood.png");
    public static BaseImage castlescreen_unitbrush_3x3_selected = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_unitbrush_3x3_selected.png");
    public static BaseImage r_building_miltary_flag_blue_over = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_flag-blue_over.png");
    public static BaseImage castlescreen_unitbrush_1x1_selected = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_unitbrush_1x1_selected.png");
    public static BaseImage castlescreen_unitbrush_5x5_selected = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_unitbrush_5x5_selected.png");
    public static BaseImage r_building_miltary_flag_blue_normal = new BaseImage(AssetPaths.AssetIconsCastle, "r_building_miltary_flag-blue_normal.png");
    public static BaseImage castlescreen_unitbrush_1x5_selected = new BaseImage(AssetPaths.AssetIconsCastle, "castlescreen_unitbrush_1x5_selected.png");
    public static BaseImage formations_img = new BaseImage(AssetPaths.AssetIconsCastle, "formation_img");
    public static BaseImage preset_castle_in = new BaseImage(AssetPaths.AssetIconsCastle, "preset_castle_in_up.png");
    public static BaseImage preset_castle_in_over = new BaseImage(AssetPaths.AssetIconsCastle, "preset_castle_in_over.png");
    public static BaseImage preset_castle_in_down = new BaseImage(AssetPaths.AssetIconsCastle, "preset_castle_in_down.png");
    public static BaseImage preset_castle_out = new BaseImage(AssetPaths.AssetIconsCastle, "preset_cloud_in_up.png");
    public static BaseImage preset_castle_out_over = new BaseImage(AssetPaths.AssetIconsCastle, "preset_cloud_in_over.png");
    public static BaseImage preset_castle_out_down = new BaseImage(AssetPaths.AssetIconsCastle, "preset_cloud_in_down.png");
    public static BaseImage preset_delete = new BaseImage(AssetPaths.AssetIconsCastle, "preset_delete_up.png");
    public static BaseImage preset_delete_over = new BaseImage(AssetPaths.AssetIconsCastle, "preset_delete_over.png");
    public static BaseImage preset_delete_down = new BaseImage(AssetPaths.AssetIconsCastle, "preset_delete_down.png");
    public static BaseImage preset_rename = new BaseImage(AssetPaths.AssetIconsCastle, "preset_rename_up.png");
    public static BaseImage preset_rename_over = new BaseImage(AssetPaths.AssetIconsCastle, "preset_rename_over.png");
    public static BaseImage preset_rename_down = new BaseImage(AssetPaths.AssetIconsCastle, "preset_rename_down.png");
    public static BaseImage preset_info = new BaseImage(AssetPaths.AssetIconsCastle, "preset_preview_up.png");
    public static BaseImage preset_info_over = new BaseImage(AssetPaths.AssetIconsCastle, "preset_preview_over.png");
    public static BaseImage preset_info_down = new BaseImage(AssetPaths.AssetIconsCastle, "preset_preview_down.png");
    public static BaseImage preset_list = new BaseImage(AssetPaths.AssetIconsCastle, "preset_list_up.png");
    public static BaseImage preset_list_over = new BaseImage(AssetPaths.AssetIconsCastle, "preset_list_over.png");
    public static BaseImage preset_list_down = new BaseImage(AssetPaths.AssetIconsCastle, "preset_list_down.png");
    public static BaseImage preset_upload = new BaseImage(AssetPaths.AssetIconsCastle, "preset_upload_normal.png");
    public static BaseImage preset_upload_over = new BaseImage(AssetPaths.AssetIconsCastle, "preset_upload_over.png");
    public static BaseImage preset_upload_down = new BaseImage(AssetPaths.AssetIconsCastle, "preset_upload_down.png");
    public static BaseImage preset_9slice_upload_top_left = new BaseImage(AssetPaths.AssetIconsCastle, "nineslice_upload_top_left.png");
    public static BaseImage preset_9slice_upload_top_mid = new BaseImage(AssetPaths.AssetIconsCastle, "nineslice_upload_top_mid.png");
    public static BaseImage preset_9slice_upload_top_right = new BaseImage(AssetPaths.AssetIconsCastle, "nineslice_upload_top_right.png");
    public static BaseImage preset_9slice_upload_mid_left = new BaseImage(AssetPaths.AssetIconsCastle, "nineslice_upload_mid_left.png");
    public static BaseImage preset_9slice_upload_mid_mid = new BaseImage(AssetPaths.AssetIconsCastle, "nineslice_upload_mid_mid.png");
    public static BaseImage preset_9slice_upload_mid_right = new BaseImage(AssetPaths.AssetIconsCastle, "nineslice_upload_mid_right.png");
    public static BaseImage preset_9slice_upload_bottom_left = new BaseImage(AssetPaths.AssetIconsCastle, "nineslice_upload_bottom_left.png");
    public static BaseImage preset_9slice_upload_bottom_mid = new BaseImage(AssetPaths.AssetIconsCastle, "nineslice_upload_bottom_mid.png");
    public static BaseImage preset_9slice_upload_bottom_right = new BaseImage(AssetPaths.AssetIconsCastle, "nineslice_upload_bottom_right.png");
    public static BaseImage preset_req_capital = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_capital.png");
    public static BaseImage preset_req_defence = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_defence.png");
    public static BaseImage preset_req_fortification = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_fortification.png");
    public static BaseImage preset_req_capital_red = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_capital_red.png");
    public static BaseImage preset_req_defence_red = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_defence_red.png");
    public static BaseImage preset_req_fortification_red = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_fortification_red.png");
    public static BaseImage preset_req_wood = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_wood.png");
    public static BaseImage preset_req_wood_red = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_wood_red.png");
    public static BaseImage preset_req_stone = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_stone.png");
    public static BaseImage preset_req_stone_red = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_stone_red.png");
    public static BaseImage preset_req_iron = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_iron.png");
    public static BaseImage preset_req_iron_red = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_iron_red.png");
    public static BaseImage preset_req_pitch = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_pitch.png");
    public static BaseImage preset_req_pitch_red = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_pitch_red.png");
    public static BaseImage preset_req_gold = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_gold.png");
    public static BaseImage preset_req_gold_red = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_gold_red.png");
    public static BaseImage preset_req_time = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_time.png");
    public static BaseImage preset_req_archer = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_archer.png");
    public static BaseImage preset_req_archer_red = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_archer_red.png");
    public static BaseImage preset_req_captain = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_captain.png");
    public static BaseImage preset_req_captain_red = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_captain_red.png");
    public static BaseImage preset_req_catapult = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_catapult.png");
    public static BaseImage preset_req_catapult_red = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_catapult_red.png");
    public static BaseImage preset_req_peasant = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_peasant.png");
    public static BaseImage preset_req_peasant_red = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_peasant_red.png");
    public static BaseImage preset_req_pikeman = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_pikeman.png");
    public static BaseImage preset_req_pikeman_red = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_pikeman_red.png");
    public static BaseImage preset_req_swordsman = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_swordsman.png");
    public static BaseImage preset_req_swordsman_red = new BaseImage(AssetPaths.AssetIconsCastle, "preset_req_swordsman_red.png");
    public static BaseImage barracks_background = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (barracks_background));
    public static BaseImage barracks_fillbar_back = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (barracks_fillbar_back));
    public static BaseImage barracks_fillbar_fill_left = new BaseImage(AssetPaths.AssetIconsBarracks, "barracks_fillbar_fill-left");
    public static BaseImage barracks_fillbar_fill_mid = new BaseImage(AssetPaths.AssetIconsBarracks, "barracks_fillbar_fill-mid");
    public static BaseImage barracks_fillbar_fill_right = new BaseImage(AssetPaths.AssetIconsBarracks, "barracks_fillbar_fill-right");
    public static BaseImage barracks_unit_archer = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (barracks_unit_archer));
    public static BaseImage barracks_unit_captain = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (barracks_unit_captain));
    public static BaseImage barracks_unit_catapult = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (barracks_unit_catapult));
    public static BaseImage barracks_unit_peasant = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (barracks_unit_peasant));
    public static BaseImage barracks_unit_pikemen = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (barracks_unit_pikemen));
    public static BaseImage barracks_unit_swordsman = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (barracks_unit_swordsman));
    public static BaseImage barracks_screen_bottom_units = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (barracks_screen_bottom_units));
    public static BaseImage barracks_i_button_normal = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (barracks_i_button_normal));
    public static BaseImage barracks_i_button_over = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (barracks_i_button_over));
    public static BaseImage barracks_little_button_normal = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (barracks_little_button_normal));
    public static BaseImage barracks_little_button_over = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (barracks_little_button_over));
    public static BaseImage button3comp_left_normal = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (button3comp_left_normal));
    public static BaseImage button3comp_left_over = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (button3comp_left_over));
    public static BaseImage button3comp_left_pressed = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (button3comp_left_pressed));
    public static BaseImage button3comp_mid_normal = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (button3comp_mid_normal));
    public static BaseImage button3comp_mid_over = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (button3comp_mid_over));
    public static BaseImage button3comp_mid_pushed = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (button3comp_mid_pushed));
    public static BaseImage button3comp_right_normal = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (button3comp_right_normal));
    public static BaseImage button3comp_right_over = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (button3comp_right_over));
    public static BaseImage button3comp_right_pushed = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (button3comp_right_pushed));
    public static BaseImage people_01 = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (people_01));
    public static BaseImage people_02 = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (people_02));
    public static BaseImage people_03 = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (people_03));
    public static BaseImage people_04 = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (people_04));
    public static BaseImage people_05 = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (people_05));
    public static BaseImage people_unitspace_icon_01 = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (people_unitspace_icon_01));
    public static BaseImage people_unitspace_icon_02 = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (people_unitspace_icon_02));
    public static BaseImage people_unitspace_icon_03 = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (people_unitspace_icon_03));
    public static BaseImage people_unitspace_icon_04 = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (people_unitspace_icon_04));
    public static BaseImage people_unitspace_icon_05 = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (people_unitspace_icon_05));
    public static BaseImage people_background = new BaseImage(AssetPaths.AssetIconsBarracks, nameof (people_background));
    public static BaseImage logout_ad_1premfor30crown_01 = new BaseImage(AssetPaths.AssetIconsLogout, "logout_ad_1premfor30crown_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage logout_ad_1premfor30crown_01_over = new BaseImage(AssetPaths.AssetIconsLogout, "logout_ad_1premfor30crown_%LANG%_over.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage[] logout_bits = BaseImage.createFromUV(AssetPaths.AssetIconsLogout, nameof (logout_bits), 15);
    public static BaseImage checkbox_checked = new BaseImage(AssetPaths.AssetIconsLogout, "checkbox_checked.png");
    public static BaseImage checkbox_unchecked = new BaseImage(AssetPaths.AssetIconsLogout, "checkbox_unchecked.png");
    public static BaseImage logout_slider_back = new BaseImage(AssetPaths.AssetIconsLogout, "logoit_slider_back.png");
    public static BaseImage logout_slider_back2 = new BaseImage(AssetPaths.AssetIconsLogout, "logoit_slider_back2.png");
    public static BaseImage logout_slider_thumb = new BaseImage(AssetPaths.AssetIconsLogout, "logoit_slider_thumb.png");
    public static BaseImage logout_gradation_band = new BaseImage(AssetPaths.AssetIconsLogout, "logout-gradation_band.png");
    public static BaseImage logout_background_lhs = new BaseImage(AssetPaths.AssetIconsLogout, "logout_background_lhs.png");
    public static BaseImage logout_premium_token = new BaseImage(AssetPaths.AssetIconsLogout, "logout_premium_token.png");
    public static BaseImage logout_premium_token_2 = new BaseImage(AssetPaths.AssetIconsLogout, "logout_premium_token_2.png");
    public static BaseImage logout_premium_token_30 = new BaseImage(AssetPaths.AssetIconsLogout, "logout_premium_token_30.png");
    public static BaseImage logout_premium_token_extendable = new BaseImage(AssetPaths.AssetIconsLogout, "logout_premium_token_x.png");
    public static BaseImage logout_text_inset = new BaseImage(AssetPaths.AssetIconsLogout, "logout_text_inset.png");
    public static BaseImage logout_text_inset_downarrow_normal = new BaseImage(AssetPaths.AssetIconsLogout, "logout_text_inset_downarrow_normal.png");
    public static BaseImage logout_text_inset_downarrow_over = new BaseImage(AssetPaths.AssetIconsLogout, "logout_text_inset_downarrow_over.png");
    public static BaseImage RH_button_getpremium_tokens_normal = new BaseImage(AssetPaths.AssetIconsLogout, "RH_button_getpremium_tokens_normal.png");
    public static BaseImage RH_button_getpremium_tokens_over = new BaseImage(AssetPaths.AssetIconsLogout, "RH_button_getpremium_tokens_over.png");
    public static BaseImage mail_folder_icon_plus = new BaseImage(AssetPaths.AssetIconsMail, "folder_icon_plus.png");
    public static BaseImage mail_folder_icon_64_open = new BaseImage(AssetPaths.AssetIconsMail, "folder_icon_64_open.png");
    public static BaseImage mail_folder_icon_closed = new BaseImage(AssetPaths.AssetIconsMail, "folder_icon_closed.png");
    public static BaseImage mail_folder_icon_open = new BaseImage(AssetPaths.AssetIconsMail, "folder_icon_open.png");
    public static BaseImage mail_folder_icon_delete = new BaseImage(AssetPaths.AssetIconsMail, "folder_icon_delete.png");
    public static BaseImage mail_inset_white_left = new BaseImage(AssetPaths.AssetIconsMail, "inset_white_left.png");
    public static BaseImage mail_inset_white_middle = new BaseImage(AssetPaths.AssetIconsMail, "inset_white_middle.png");
    public static BaseImage mail_inset_white_right = new BaseImage(AssetPaths.AssetIconsMail, "inset_white_right.png");
    public static BaseImage mail_letter_icon_closed = new BaseImage(AssetPaths.AssetIconsMail, "letter_icon_closed.png");
    public static BaseImage mail_letter_icon_open = new BaseImage(AssetPaths.AssetIconsMail, "letter_icon_open.png");
    public static BaseImage mail_shadow_bottom = new BaseImage(AssetPaths.AssetIconsMail, "shadow_bottom.png");
    public static BaseImage mail_shadow_bottom_left = new BaseImage(AssetPaths.AssetIconsMail, "shadow_bottom_left.png");
    public static BaseImage mail_shadow_bottom_right = new BaseImage(AssetPaths.AssetIconsMail, "shadow_bottom_right.png");
    public static BaseImage mail_shadow_right = new BaseImage(AssetPaths.AssetIconsMail, "shadow_right.png");
    public static BaseImage mail_shadow_top_right = new BaseImage(AssetPaths.AssetIconsMail, "shadow_top_right.png");
    public static BaseImage mail_top_drag_bar_left = new BaseImage(AssetPaths.AssetIconsMail, "top-drag-bar_left.png");
    public static BaseImage mail_top_drag_bar_middle = new BaseImage(AssetPaths.AssetIconsMail, "top-drag-bar_middle.png");
    public static BaseImage mail_top_drag_bar_right = new BaseImage(AssetPaths.AssetIconsMail, "top-drag-bar_right.png");
    public static BaseImage mail_topbar_left_in = new BaseImage(AssetPaths.AssetIconsMail, "topbar_left_in.png");
    public static BaseImage mail_topbar_left_normal = new BaseImage(AssetPaths.AssetIconsMail, "topbar_left_normal.png");
    public static BaseImage mail_topbar_middle_in = new BaseImage(AssetPaths.AssetIconsMail, "topbar_middle_in.png");
    public static BaseImage mail_topbar_middle_normal = new BaseImage(AssetPaths.AssetIconsMail, "topbar_middle_normal.png");
    public static BaseImage mail_topbar_right_in = new BaseImage(AssetPaths.AssetIconsMail, "topbar_right_in.png");
    public static BaseImage mail_topbar_right_normal = new BaseImage(AssetPaths.AssetIconsMail, "topbar_right_normal.png");
    public static BaseImage mail_horizontal_bar = new BaseImage(AssetPaths.AssetIconsMail, "mail bar horizontal.png");
    public static BaseImage mail_plus = new BaseImage(AssetPaths.AssetIconsMail, "mail_plus.png");
    public static BaseImage mail_minus = new BaseImage(AssetPaths.AssetIconsMail, "mail_minus.png");
    public static BaseImage button_132_in = new BaseImage(AssetPaths.AssetIconsMail, "button_132_in.png");
    public static BaseImage button_132_normal = new BaseImage(AssetPaths.AssetIconsMail, "button_132_normal.png");
    public static BaseImage button_132_over = new BaseImage(AssetPaths.AssetIconsMail, "button_132_over.png");
    public static BaseImage button_132_in_gold = new BaseImage(AssetPaths.AssetIconsMail, "button_132_in_gold.png");
    public static BaseImage button_132_normal_gold = new BaseImage(AssetPaths.AssetIconsMail, "button_132_normal_gold.png");
    public static BaseImage button_132_over_gold = new BaseImage(AssetPaths.AssetIconsMail, "button_132_over_gold.png");
    public static BaseImage[] quest_rewards = BaseImage.createFromUV(AssetPaths.AssetIconsQuests, "quest_screen_rewards", 13);
    public static BaseImage[] quest_icons = BaseImage.createFromUV(AssetPaths.AssetIconsQuests, "quest_screen_quest_icons", 45);
    public static BaseImage[] quest_checkboxes = BaseImage.createFromUV(AssetPaths.AssetIconsQuests, "quest_screen_check-x", 4);
    public static BaseImage[] wheel_arrowBlur_royal = BaseImage.createFromUV(AssetPaths.AssetIconsQuests, "Arrow_Blur_Royal", 3);
    public static BaseImage[] wheel_arrowBlurShadow = BaseImage.createFromUV(AssetPaths.AssetIconsQuests, "Arrow_Blur_shadow", 3);
    public static BaseImage[] wheel_icons = BaseImage.createFromUV(AssetPaths.AssetIconsQuests, "icons", 62);
    public static BaseImage[] wheel_numbers = BaseImage.createFromUV(AssetPaths.AssetIconsQuests, nameof (wheel_numbers), 36);
    public static BaseImage[] wheel_spinButton_royal = BaseImage.createFromUV(AssetPaths.AssetIconsQuests, "button_spin_royal", 2);
    public static BaseImage[] wheel_star = BaseImage.createFromUV(AssetPaths.AssetIconsQuests, "star_spinning", 3);
    public static BaseImage[] wheel_report_icons = BaseImage.createFromUV(AssetPaths.AssetIconsQuests, "WheelSpin_x5", 5);
    public static BaseImage quest_9sclice_grey_inset_bottom_left = new BaseImage(AssetPaths.AssetIconsQuests, "nineslice_grey_inset_bottom_left.png");
    public static BaseImage quest_9sclice_grey_inset_bottom_mid = new BaseImage(AssetPaths.AssetIconsQuests, "nineslice_grey_inset_bottom_mid.png");
    public static BaseImage quest_9sclice_grey_inset_bottom_right = new BaseImage(AssetPaths.AssetIconsQuests, "nineslice_grey_inset_bottom_right.png");
    public static BaseImage quest_9sclice_grey_inset_mid_left = new BaseImage(AssetPaths.AssetIconsQuests, "nineslice_grey_inset_mid_left.png");
    public static BaseImage quest_9sclice_grey_inset_mid_mid = new BaseImage(AssetPaths.AssetIconsQuests, "nineslice_grey_inset_mid_mid.png");
    public static BaseImage quest_9sclice_grey_inset_mid_right = new BaseImage(AssetPaths.AssetIconsQuests, "nineslice_grey_inset_mid_right.png");
    public static BaseImage quest_9sclice_grey_inset_top_left = new BaseImage(AssetPaths.AssetIconsQuests, "nineslice_grey_inset_top_left.png");
    public static BaseImage quest_9sclice_grey_inset_top_mid = new BaseImage(AssetPaths.AssetIconsQuests, "nineslice_grey_inset_top_mid.png");
    public static BaseImage quest_9sclice_grey_inset_top_right = new BaseImage(AssetPaths.AssetIconsQuests, "nineslice_grey_inset_top_right.png");
    public static BaseImage quest_screen_bar1 = new BaseImage(AssetPaths.AssetIconsQuests, "quest_screen_bar1.png");
    public static BaseImage quest_screen_bar2 = new BaseImage(AssetPaths.AssetIconsQuests, "quest_screen_bar2.png");
    public static BaseImage quest_screen_progbar_left = new BaseImage(AssetPaths.AssetIconsQuests, "quest_screen_progbar_left.png");
    public static BaseImage quest_screen_progbar_mid = new BaseImage(AssetPaths.AssetIconsQuests, "quest_screen_progbar_mid.png");
    public static BaseImage quest_screen_progbar_right = new BaseImage(AssetPaths.AssetIconsQuests, "quest_screen_progbar_right.png");
    public static BaseImage quest_screen_top = new BaseImage(AssetPaths.AssetIconsQuests, "quest_screen_top.png");
    public static BaseImage quest_screen_warm = new BaseImage(AssetPaths.AssetIconsQuests, "quest_screen_warm-corner.png");
    public static BaseImage quest_popup_hz_strip_01 = new BaseImage(AssetPaths.AssetIconsQuests, "quest_popup_hz_strip_01.png");
    public static BaseImage quest_popup_hz_strip_02 = new BaseImage(AssetPaths.AssetIconsQuests, "quest_popup_hz_strip_02.png");
    public static BaseImage quest_popup_hz_strip_03 = new BaseImage(AssetPaths.AssetIconsQuests, "quest_popup_hz_strip_03.png");
    public static BaseImage quest_popup_inset_bottom = new BaseImage(AssetPaths.AssetIconsQuests, "quest_popup_inset_bottom.png");
    public static BaseImage quest_popup_inset_highlight = new BaseImage(AssetPaths.AssetIconsQuests, "quest_popup_inset_highlight.png");
    public static BaseImage quest_popup_inset_middle = new BaseImage(AssetPaths.AssetIconsQuests, "quest_popup_inset_middle.png");
    public static BaseImage quest_popup_inset_top = new BaseImage(AssetPaths.AssetIconsQuests, "quest_popup_inset_top.png");
    public static BaseImage quest_button_glow = new BaseImage(AssetPaths.AssetIconsQuests, "quest_button_glowt.png");
    public static BaseImage reward_panel_background = new BaseImage(AssetPaths.AssetIconsQuests, nameof (reward_panel_background));
    public static BaseImage wheel_wheel_royal = new BaseImage(AssetPaths.AssetIconsQuests, "wheel_background_royal");
    public static BaseImage townbuilding_ale_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_ale_normal.png");
    public static BaseImage townbuilding_ale_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_ale_over.png");
    public static BaseImage townbuilding_apples_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_apples_normal.png");
    public static BaseImage townbuilding_apples_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_apples_over.png");
    public static BaseImage townbuilding_archeryrange_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_archeryrange_normal.png");
    public static BaseImage townbuilding_archeryrange_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_archeryrange_over.png");
    public static BaseImage townbuilding_architectsguild_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_architectsguild_normal.png");
    public static BaseImage townbuilding_architectsguild_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_architectsguild_over.png");
    public static BaseImage townbuilding_armour_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_armour_normal.png");
    public static BaseImage townbuilding_armour_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_armour_over.png");
    public static BaseImage townbuilding_ballistamaker_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_ballistamaker_normal.png");
    public static BaseImage townbuilding_ballistamaker_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_ballistamaker_over.png");
    public static BaseImage townbuilding_banquette_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_banquette_normal.png");
    public static BaseImage townbuilding_banquette_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_banquette_over.png");
    public static BaseImage townbuilding_banquette_food_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_banquette_food_normal.png");
    public static BaseImage townbuilding_banquette_food_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_banquette_food_over.png");
    public static BaseImage townbuilding_barracks_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_barracks_normal.png");
    public static BaseImage townbuilding_barracks_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_barracks_over.png");
    public static BaseImage townbuilding_blank_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_blank_normal.png");
    public static BaseImage townbuilding_blank_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_blank_over.png");
    public static BaseImage townbuilding_bows_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_bows_normal.png");
    public static BaseImage townbuilding_bows_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_bows_over.png");
    public static BaseImage townbuilding_bread_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_bread_normal.png");
    public static BaseImage townbuilding_bread_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_bread_over.png");
    public static BaseImage townbuilding_carpenter_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_carpenter_normal.png");
    public static BaseImage townbuilding_carpenter_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_carpenter_over.png");
    public static BaseImage townbuilding_castellanshouse_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_castellanshouse_normal.png");
    public static BaseImage townbuilding_castellanshouse_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_castellanshouse_over.png");
    public static BaseImage townbuilding_catapults_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_catapults_normal.png");
    public static BaseImage townbuilding_catapults_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_catapults_over.png");
    public static BaseImage townbuilding_cheese_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_cheese_normal.png");
    public static BaseImage townbuilding_cheese_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_cheese_over.png");
    public static BaseImage townbuilding_church_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_church_normal.png");
    public static BaseImage townbuilding_church_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_church_over.png");
    public static BaseImage townbuilding_combatarena_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_combatarena_normal.png");
    public static BaseImage townbuilding_combatarena_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_combatarena_over.png");
    public static BaseImage townbuilding_fish_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_fish_normal.png");
    public static BaseImage townbuilding_fish_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_fish_over.png");
    public static BaseImage townbuilding_food_ale_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_food-ale_normal.png");
    public static BaseImage townbuilding_food_ale_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_food-ale_over.png");
    public static BaseImage townbuilding_iron_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_iron_normal.png");
    public static BaseImage townbuilding_iron_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_iron_over.png");
    public static BaseImage townbuilding_Labourersbillets_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_Labourersbillets_normal.png");
    public static BaseImage townbuilding_Labourersbillets_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_Labourersbillets_over.png");
    public static BaseImage townbuilding_meat_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_meat_normal.png");
    public static BaseImage townbuilding_meat_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_meat_over.png");
    public static BaseImage townbuilding_metalware_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_metalware_normal.png");
    public static BaseImage townbuilding_metalware_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_metalware_over.png");
    public static BaseImage townbuilding_militaryschool_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_militaryschool_normal.png");
    public static BaseImage townbuilding_militaryschool_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_militaryschool_over.png");
    public static BaseImage townbuilding_officersquarters_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_officersquarters_normal.png");
    public static BaseImage townbuilding_officersquarters_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_officersquarters_over.png");
    public static BaseImage townbuilding_peasntshall_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_peasntshall_normal.png");
    public static BaseImage townbuilding_peasntshall_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_peasntshall_over.png");
    public static BaseImage townbuilding_pikemandrillyard_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_pikemandrillyard_normal.png");
    public static BaseImage townbuilding_pikemandrillyard_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_pikemandrillyard_over.png");
    public static BaseImage townbuilding_pikes_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_pikes_normal.png");
    public static BaseImage townbuilding_pikes_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_pikes_over.png");
    public static BaseImage townbuilding_pitch_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_pitch_normal.png");
    public static BaseImage townbuilding_pitch_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_pitch_over.png");
    public static BaseImage townbuilding_resource_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_resource_normal.png");
    public static BaseImage townbuilding_resource_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_resource_over.png");
    public static BaseImage townbuilding_salt_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_salt_normal.png");
    public static BaseImage townbuilding_salt_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_salt_over.png");
    public static BaseImage townbuilding_sergeantsatarmsoffice_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_sergeantsatarmsoffice_normal.png");
    public static BaseImage townbuilding_sergeantsatarmsoffice_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_sergeantsatarmsoffice_over.png");
    public static BaseImage townbuilding_siegeengineersguild_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_siegeengineersguild_normal.png");
    public static BaseImage townbuilding_siegeengineersguild_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_siegeengineersguild_over.png");
    public static BaseImage townbuilding_silk_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_silk_normal.png");
    public static BaseImage townbuilding_silk_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_silk_over.png");
    public static BaseImage townbuilding_spice_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_spice_normal.png");
    public static BaseImage townbuilding_spice_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_spice_over.png");
    public static BaseImage townbuilding_stables_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_stables_normal.png");
    public static BaseImage townbuilding_stables_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_stables_over.png");
    public static BaseImage townbuilding_statue_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_statue_normal.png");
    public static BaseImage townbuilding_statue_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_statue_over.png");
    public static BaseImage townbuilding_stonequarry_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_stonequarry_normal.png");
    public static BaseImage townbuilding_stonequarry_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_stonequarry_over.png");
    public static BaseImage townbuilding_supplydepot_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_supplydepot_normal.png");
    public static BaseImage townbuilding_supplydepot_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_supplydepot_over.png");
    public static BaseImage townbuilding_sword_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_sword_normal.png");
    public static BaseImage townbuilding_sword_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_sword_over.png");
    public static BaseImage townbuilding_tailor_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_tailor_normal.png");
    public static BaseImage townbuilding_tailor_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_tailor_over.png");
    public static BaseImage townbuilding_towngarden_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_towngarden_normal.png");
    public static BaseImage townbuilding_towngarden_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_towngarden_over.png");
    public static BaseImage townbuilding_townhall_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_townhall_normal.png");
    public static BaseImage townbuilding_townhall_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_townhall_over.png");
    public static BaseImage townbuilding_tunnellorsguild_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_tunnellorsguild_normal.png");
    public static BaseImage townbuilding_tunnellorsguild_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_tunnellorsguild_over.png");
    public static BaseImage townbuilding_turretmaker_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_turretmaker_normal.png");
    public static BaseImage townbuilding_turretmaker_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_turretmaker_over.png");
    public static BaseImage townbuilding_veg_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_veg_normal.png");
    public static BaseImage townbuilding_veg_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_veg_over.png");
    public static BaseImage townbuilding_venison_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_venison_normal.png");
    public static BaseImage townbuilding_venison_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_venison_over.png");
    public static BaseImage townbuilding_weapons_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_weapons_normal.png");
    public static BaseImage townbuilding_weapons_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_weapons_over.png");
    public static BaseImage townbuilding_wine_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_wine_normal.png");
    public static BaseImage townbuilding_wine_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_wine_over.png");
    public static BaseImage townbuilding_Woodcutter_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_Woodcutter_normal.png");
    public static BaseImage townbuilding_Woodcutter_over = new BaseImage(AssetPaths.AssetIconsCapital, "townbuilding_Woodcutter_over.png");
    public static BaseImage townscreen_army_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townscreen_army_normal.png");
    public static BaseImage townscreen_army_over = new BaseImage(AssetPaths.AssetIconsCapital, "townscreen_army_over.png");
    public static BaseImage townscreen_army_selected = new BaseImage(AssetPaths.AssetIconsCapital, "townscreen_army_selected.png");
    public static BaseImage townscreen_castle_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townscreen_castle_normal.png");
    public static BaseImage townscreen_castle_over = new BaseImage(AssetPaths.AssetIconsCapital, "townscreen_castle_over.png");
    public static BaseImage townscreen_castle_selected = new BaseImage(AssetPaths.AssetIconsCapital, "townscreen_castle_selected.png");
    public static BaseImage townscreen_civil_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townscreen_civil_normal.png");
    public static BaseImage townscreen_civil_over = new BaseImage(AssetPaths.AssetIconsCapital, "townscreen_civil_over.png");
    public static BaseImage townscreen_civil_selected = new BaseImage(AssetPaths.AssetIconsCapital, "townscreen_civil_selected.png");
    public static BaseImage townscreen_guild_normal = new BaseImage(AssetPaths.AssetIconsCapital, "townscreen_guild_normal.png");
    public static BaseImage townscreen_guild_over = new BaseImage(AssetPaths.AssetIconsCapital, "townscreen_guild_over.png");
    public static BaseImage townscreen_guild_selected = new BaseImage(AssetPaths.AssetIconsCapital, "townscreen_guild_selected.png");
    public static BaseImage help_over = new BaseImage(AssetPaths.AssetIconsCapital, "help_over.png");
    public static BaseImage help_normal = new BaseImage(AssetPaths.AssetIconsCapital, "help_normal.png");
    public static BaseImage help_pushed = new BaseImage(AssetPaths.AssetIconsCapital, "help_pushed.png");
    public static BaseImage help_gold_over = new BaseImage(AssetPaths.AssetIconsCapital, "help_over_gold.png");
    public static BaseImage help_gold_normal = new BaseImage(AssetPaths.AssetIconsCapital, "help_normal_gold.png");
    public static BaseImage help_gold_pushed = new BaseImage(AssetPaths.AssetIconsCapital, "help_pushed_gold.png");
    public static BaseImage donate_illustration = new BaseImage(AssetPaths.AssetIconsCapital, "parish_donate_illustration");
    public static BaseImage donate_tick = new BaseImage(AssetPaths.AssetIconsCapital, "parish_donate_check");
    public static BaseImage donate_type_banquet = new BaseImage(AssetPaths.AssetIconsCapital, "parish_donate_banquet.png");
    public static BaseImage donate_type_food = new BaseImage(AssetPaths.AssetIconsCapital, "parish_donate_food.png");
    public static BaseImage donate_type_weapons = new BaseImage(AssetPaths.AssetIconsCapital, "parish_donate_weapons.png");
    public static BaseImage donate_type_resources = new BaseImage(AssetPaths.AssetIconsCapital, "parish_donate_resource.png");
    public static BaseImage[] scout_screen_icons = BaseImage.createFromUV(AssetPaths.AssetIconsScouting, nameof (scout_screen_icons), 66);
    public static BaseImage scout_screen_slider_bar = new BaseImage(AssetPaths.AssetIconsScouting, "scout_screen_slider_bar.png");
    public static BaseImage scout_screen_arrowbox = new BaseImage(AssetPaths.AssetIconsScouting, "scout_screen_arrowbox.png");
    public static BaseImage scout_screen_slider = new BaseImage(AssetPaths.AssetIconsScouting, "scout_screen_slider.png");
    public static BaseImage button_with_inset_pushed = new BaseImage(AssetPaths.AssetIconsScouting, "button_with_inset_pushed.png");
    public static BaseImage button_with_inset_over = new BaseImage(AssetPaths.AssetIconsScouting, "button_with_inset_over.png");
    public static BaseImage button_with_inset_normal = new BaseImage(AssetPaths.AssetIconsScouting, "button_with_inset_normal.png");
    public static BaseImage scout_screen_illustration_01 = new BaseImage(AssetPaths.AssetIconsScouting, "scout_screen_illustration_01.png");
    public static BaseImage reinforce_back_left = new BaseImage(AssetPaths.AssetIconsScouting, "reinforce_back_left.png");
    public static BaseImage reinforce_back_right = new BaseImage(AssetPaths.AssetIconsScouting, "reinforce_back_right.png");
    public static BaseImage reinforce_slider = new BaseImage(AssetPaths.AssetIconsScouting, "reinforce_slider.png");
    public static BaseImage reinforce_Vassal_screen_back = new BaseImage(AssetPaths.AssetIconsScouting, "Reinfoce_Vassal_screen_back.png");
    public static BaseImage capital_troops_back = new BaseImage(AssetPaths.AssetIconsScouting, "capital_troops_back.png");
    public static BaseImage button4comp_left_normal = new BaseImage(AssetPaths.AssetIconsScouting, nameof (button4comp_left_normal));
    public static BaseImage button4comp_left_over = new BaseImage(AssetPaths.AssetIconsScouting, nameof (button4comp_left_over));
    public static BaseImage button4comp_left_pressed = new BaseImage(AssetPaths.AssetIconsScouting, nameof (button4comp_left_pressed));
    public static BaseImage button4comp_right_normal = new BaseImage(AssetPaths.AssetIconsScouting, nameof (button4comp_right_normal));
    public static BaseImage button4comp_right_over = new BaseImage(AssetPaths.AssetIconsScouting, nameof (button4comp_right_over));
    public static BaseImage button4comp_right_pushed = new BaseImage(AssetPaths.AssetIconsScouting, nameof (button4comp_right_pushed));
    public static BaseImage[] rank_images = BaseImage.createFromUV(AssetPaths.AssetIconsHonour, "rank_progression_array", 66);
    public static BaseImage honour_rank_slot_divider = new BaseImage(AssetPaths.AssetIconsHonour, "rank_slot_divider.png");
    public static BaseImage honour_rank_slot_green_left = new BaseImage(AssetPaths.AssetIconsHonour, "rank_slot_green_left.png");
    public static BaseImage honour_rank_slot_green_middle = new BaseImage(AssetPaths.AssetIconsHonour, "rank_slot_green_middle.png");
    public static BaseImage honour_rank_slot_green_right = new BaseImage(AssetPaths.AssetIconsHonour, "rank_slot_green_right.png");
    public static BaseImage honour_rank_slot_left = new BaseImage(AssetPaths.AssetIconsHonour, "rank_slot_left.png");
    public static BaseImage honour_rank_slot_middle = new BaseImage(AssetPaths.AssetIconsHonour, "rank_slot_middle.png");
    public static BaseImage honour_rank_slot_right = new BaseImage(AssetPaths.AssetIconsHonour, "rank_slot_right.png");
    public static BaseImage honour_rank_slot_yellow_left = new BaseImage(AssetPaths.AssetIconsHonour, "rank_slot_yellow_left.png");
    public static BaseImage honour_rank_slot_yellow_middle = new BaseImage(AssetPaths.AssetIconsHonour, "rank_slot_yellow_middle.png");
    public static BaseImage honour_rank_slot_yellow_right = new BaseImage(AssetPaths.AssetIconsHonour, "rank_slot_yellow_right.png");
    public static BaseImage rank_progression_crown_prince = new BaseImage(AssetPaths.AssetIconsHonour, nameof (rank_progression_crown_prince));
    public static BaseImage[] RankAnim_Images = BaseImage.createFromUV(AssetPaths.AssetIconsRankAnim, "lords", 22);
    public static BaseImage RankAnim_Images23 = new BaseImage(AssetPaths.AssetIconsRankAnim, "crown_prince");
    public static BaseImage[] monk_screen_button_array_75x75 = BaseImage.createFromUV(AssetPaths.AssetIconsMonks, nameof (monk_screen_button_array_75x75), 28);
    public static BaseImage[] monk_screen_button_array = BaseImage.createFromUV(AssetPaths.AssetIconsMonks, nameof (monk_screen_button_array), 6);
    public static BaseImage[] radio_green = BaseImage.createFromUV(AssetPaths.AssetIconsMonks, nameof (radio_green), 3);
    public static BaseImage[] send_army_buttons = BaseImage.createFromUV(AssetPaths.AssetIconsMonks, "send_army_button_comp", 36);
    public static BaseImage illustration_monks = new BaseImage(AssetPaths.AssetIconsMonks, "illustration_monks.png");
    public static BaseImage monk_screen_buttongroup_inset = new BaseImage(AssetPaths.AssetIconsMonks, "monk_screen_buttongroup_inset.png");
    public static BaseImage monk_screen_playerlist_inset = new BaseImage(AssetPaths.AssetIconsMonks, "monk_screen_playerlist_inset.png");
    public static BaseImage monk_screen_slider = new BaseImage(AssetPaths.AssetIconsMonks, "monk_screen_slider.png");
    public static BaseImage popup_border_bottom = new BaseImage(AssetPaths.AssetIconsMonks, "popup_border_bottom.png");
    public static BaseImage popup_border_rhs = new BaseImage(AssetPaths.AssetIconsMonks, "popup_border_rhs.png");
    public static BaseImage popup_title_bar = new BaseImage(AssetPaths.AssetIconsMonks, "title_bar.png");
    public static BaseImage send_army_illustration = new BaseImage(AssetPaths.AssetIconsMonks, "send_army_illustration.png");
    public static BaseImage send_army_slider = new BaseImage(AssetPaths.AssetIconsMonks, "send_army_slider.png");
    public static BaseImage send_army_timer = new BaseImage(AssetPaths.AssetIconsMonks, "send_army_screen_timer_back.png");
    public static BaseImage[] parishwall_village_center_achievement_icons = BaseImage.createFromUV(AssetPaths.AssetIconsParishWall, "village_center_achievement_icons", 20);
    public static BaseImage parishwall_dividing_line = new BaseImage(AssetPaths.AssetIconsParishWall, "dividing_line.png");
    public static BaseImage parishwall_tan_bar_01 = new BaseImage(AssetPaths.AssetIconsParishWall, "tan_bar_01.png");
    public static BaseImage parishwall_tan_bar_01_short = new BaseImage(AssetPaths.AssetIconsParishWall, "tan_bar_01_short.png");
    public static BaseImage parishwall_tan_bar_02 = new BaseImage(AssetPaths.AssetIconsParishWall, "tan_bar_02.png");
    public static BaseImage parishwall_tan_bar_03 = new BaseImage(AssetPaths.AssetIconsParishWall, "tan_bar_03.png");
    public static BaseImage parishwall_village_center_tab_down = new BaseImage(AssetPaths.AssetIconsParishWall, "village_center_tab_down.png");
    public static BaseImage parishwall_village_center_tab_up = new BaseImage(AssetPaths.AssetIconsParishWall, "village_center_tab_up.png");
    public static BaseImage parishwall_village_center_tab_outline_bottom_left = new BaseImage(AssetPaths.AssetIconsParishWall, "village_center_tab-outline_bottom_left.png");
    public static BaseImage parishwall_village_center_tab_outline_bottom_middle = new BaseImage(AssetPaths.AssetIconsParishWall, "village_center_tab-outline_bottom_middle.png");
    public static BaseImage parishwall_village_center_tab_outline_bottom_right = new BaseImage(AssetPaths.AssetIconsParishWall, "village_center_tab-outline_bottom_right.png");
    public static BaseImage parishwall_village_center_tab_outline_middle_left = new BaseImage(AssetPaths.AssetIconsParishWall, "village_center_tab-outline_middle_left.png");
    public static BaseImage parishwall_village_center_tab_outline_middle_right = new BaseImage(AssetPaths.AssetIconsParishWall, "village_center_tab-outline_middle_right.png");
    public static BaseImage parishwall_village_center_tab_outline_top_left = new BaseImage(AssetPaths.AssetIconsParishWall, "village_center_tab-outline_top_left.png");
    public static BaseImage parishwall_village_center_tab_outline_top_middle = new BaseImage(AssetPaths.AssetIconsParishWall, "village_center_tab-outline_top_middle.png");
    public static BaseImage parishwall_village_center_tab_outline_top_right = new BaseImage(AssetPaths.AssetIconsParishWall, "village_center_tab-outline_top_right.png");
    public static BaseImage parishwall_village_illlustration_01 = new BaseImage(AssetPaths.AssetIconsParishWall, "village_illlustration_01.png");
    public static BaseImage parishwall_village_illlustration_02 = new BaseImage(AssetPaths.AssetIconsParishWall, "vote_illustration_2.png");
    public static BaseImage parishwall_village_illlustration_03 = new BaseImage(AssetPaths.AssetIconsParishWall, "vote_illustration_3.png");
    public static BaseImage parishwall_village_illlustration_04 = new BaseImage(AssetPaths.AssetIconsParishWall, "vote_illustration_4.png");
    public static BaseImage parishwall_what_say_thou_box = new BaseImage(AssetPaths.AssetIconsParishWall, "what_say_thou_box.png");
    public static BaseImage _24wide_thumb_bottom = new BaseImage(AssetPaths.AssetIconsParishWall, "_24wide_thumb_bottom.png");
    public static BaseImage _24wide_thumb_middle = new BaseImage(AssetPaths.AssetIconsParishWall, "_24wide_thumb_middle.png");
    public static BaseImage _24wide_thumb_top = new BaseImage(AssetPaths.AssetIconsParishWall, "_24wide_thumb_top.png");
    public static BaseImage shield_blank_256 = new BaseImage(AssetPaths.AssetIconsParishWall, "shield_blank_256.png");
    public static BaseImage parishwall_solid_rounded_rectangle_tan_bottom_left = new BaseImage(AssetPaths.AssetIconsParishWall, "solid_rounded_rectangle_tan_bottom-left.png");
    public static BaseImage parishwall_solid_rounded_rectangle_tan_bottom_middle = new BaseImage(AssetPaths.AssetIconsParishWall, "solid_rounded_rectangle_tan_bottom-middle.png");
    public static BaseImage parishwall_solid_rounded_rectangle_tan_bottom_right = new BaseImage(AssetPaths.AssetIconsParishWall, "solid_rounded_rectangle_tan_bottom-right.png");
    public static BaseImage parishwall_solid_rounded_rectangle_tan_middle_left = new BaseImage(AssetPaths.AssetIconsParishWall, "solid_rounded_rectangle_tan_middle-left.png");
    public static BaseImage parishwall_solid_rounded_rectangle_tan_middle_middle = new BaseImage(AssetPaths.AssetIconsParishWall, "solid_rounded_rectangle_tan_middle-middle.png");
    public static BaseImage parishwall_solid_rounded_rectangle_tan_middle_right = new BaseImage(AssetPaths.AssetIconsParishWall, "solid_rounded_rectangle_tan_middle-right.png");
    public static BaseImage parishwall_solid_rounded_rectangle_tan_upper_left = new BaseImage(AssetPaths.AssetIconsParishWall, "solid_rounded_rectangle_tan_upper-left.png");
    public static BaseImage parishwall_solid_rounded_rectangle_tan_upper_middle = new BaseImage(AssetPaths.AssetIconsParishWall, "solid_rounded_rectangle_tan_upper-middle.png");
    public static BaseImage parishwall_solid_rounded_rectangle_tan_upper_right = new BaseImage(AssetPaths.AssetIconsParishWall, "solid_rounded_rectangle_tan_upper-right.png");
    public static BaseImage parishwall_button_vote_checked_normal = new BaseImage(AssetPaths.AssetIconsParishWall, "button_vote_checked_normal.png");
    public static BaseImage parishwall_button_vote_checked_over = new BaseImage(AssetPaths.AssetIconsParishWall, "button_vote_checked_over.png");
    public static BaseImage house_flag_001_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_001_small.png");
    public static BaseImage house_flag_002_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_002_small.png");
    public static BaseImage house_flag_003_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_003_small.png");
    public static BaseImage house_flag_004_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_004_small.png");
    public static BaseImage house_flag_005_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_005_small.png");
    public static BaseImage house_flag_006_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_006_small.png");
    public static BaseImage house_flag_007_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_007_small.png");
    public static BaseImage house_flag_008_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_008_small.png");
    public static BaseImage house_flag_009_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_009_small.png");
    public static BaseImage house_flag_010_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_010_small.png");
    public static BaseImage house_flag_011_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_011_small.png");
    public static BaseImage house_flag_012_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_012_small.png");
    public static BaseImage house_flag_013_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_013_small.png");
    public static BaseImage house_flag_014_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_014_small.png");
    public static BaseImage house_flag_015_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_015_small.png");
    public static BaseImage house_flag_016_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_016_small.png");
    public static BaseImage house_flag_017_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_017_small.png");
    public static BaseImage house_flag_018_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_018_small.png");
    public static BaseImage house_flag_019_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_019_small.png");
    public static BaseImage house_flag_020_small = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_020_small.png");
    public static BaseImage house_flag_001_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_001_small_WL.png");
    public static BaseImage house_flag_002_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_002_small_WL.png");
    public static BaseImage house_flag_003_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_003_small_WL.png");
    public static BaseImage house_flag_004_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_004_small_WL.png");
    public static BaseImage house_flag_005_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_005_small_WL.png");
    public static BaseImage house_flag_006_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_006_small_WL.png");
    public static BaseImage house_flag_007_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_007_small_WL.png");
    public static BaseImage house_flag_008_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_008_small_WL.png");
    public static BaseImage house_flag_009_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_009_small_WL.png");
    public static BaseImage house_flag_010_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_010_small_WL.png");
    public static BaseImage house_flag_011_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_011_small_WL.png");
    public static BaseImage house_flag_012_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_012_small_WL.png");
    public static BaseImage house_flag_013_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_013_small_WL.png");
    public static BaseImage house_flag_014_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_014_small_WL.png");
    public static BaseImage house_flag_015_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_015_small_WL.png");
    public static BaseImage house_flag_016_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_016_small_WL.png");
    public static BaseImage house_flag_017_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_017_small_WL.png");
    public static BaseImage house_flag_018_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_018_small_WL.png");
    public static BaseImage house_flag_019_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_019_small_WL.png");
    public static BaseImage house_flag_020_small_WL = new BaseImage(AssetPaths.AssetIconsParishWall, "house_flag_020_small_WL.png");
    public static BaseImage mail2_exclaimation = new BaseImage(AssetPaths.AssetIconsMail2, "exclamation.png");
    public static BaseImage mail2_blue_scrollbar_bar_bottom = new BaseImage(AssetPaths.AssetIconsMail2, "blue_scrollbar_bar_bottom.png");
    public static BaseImage mail2_blue_scrollbar_bar_middle = new BaseImage(AssetPaths.AssetIconsMail2, "blue_scrollbar_bar_middle.png");
    public static BaseImage mail2_blue_scrollbar_bar_top = new BaseImage(AssetPaths.AssetIconsMail2, "blue_scrollbar_bar_top.png");
    public static BaseImage mail2_blue_scrollbar_bottomarrow_in = new BaseImage(AssetPaths.AssetIconsMail2, "blue_scrollbar_bottomarrow_in.png");
    public static BaseImage mail2_blue_scrollbar_bottomarrow_normal = new BaseImage(AssetPaths.AssetIconsMail2, "blue_scrollbar_bottomarrow_normal.png");
    public static BaseImage mail2_blue_scrollbar_bottomarrow_over = new BaseImage(AssetPaths.AssetIconsMail2, "blue_scrollbar_bottomarrow_over.png");
    public static BaseImage mail2_blue_scrollbar_thumb_bottom = new BaseImage(AssetPaths.AssetIconsMail2, "blue_scrollbar_thumb_bottom.png");
    public static BaseImage mail2_blue_scrollbar_thumb_mid = new BaseImage(AssetPaths.AssetIconsMail2, "blue_scrollbar_thumb_mid.png");
    public static BaseImage mail2_blue_scrollbar_thumb_mid_lines = new BaseImage(AssetPaths.AssetIconsMail2, "blue_scrollbar_thumb_mid_lines.png");
    public static BaseImage mail2_blue_scrollbar_thumb_top = new BaseImage(AssetPaths.AssetIconsMail2, "blue_scrollbar_thumb_top.png");
    public static BaseImage mail2_blue_scrollbar_toparrow_in = new BaseImage(AssetPaths.AssetIconsMail2, "blue_scrollbar_toparrow_in.png");
    public static BaseImage mail2_blue_scrollbar_toparrow_normal = new BaseImage(AssetPaths.AssetIconsMail2, "blue_scrollbar_toparrow_normal.png");
    public static BaseImage mail2_blue_scrollbar_toparrow_over = new BaseImage(AssetPaths.AssetIconsMail2, "blue_scrollbar_toparrow_over.png");
    public static BaseImage mail2_button_blue_141wide_normal = new BaseImage(AssetPaths.AssetIconsMail2, "button_blue_141wide_normal.png");
    public static BaseImage mail2_button_blue_141wide_over = new BaseImage(AssetPaths.AssetIconsMail2, "button_blue_141wide_over.png");
    public static BaseImage mail2_button_blue_141wide_pushed = new BaseImage(AssetPaths.AssetIconsMail2, "button_blue_141wide_pushed.png");
    public static BaseImage mail2_button_thin_in = new BaseImage(AssetPaths.AssetIconsMail2, "button_thin_in.png");
    public static BaseImage mail2_button_thin_normal = new BaseImage(AssetPaths.AssetIconsMail2, "button_thin_normal.png");
    public static BaseImage mail2_button_thin_over = new BaseImage(AssetPaths.AssetIconsMail2, "button_thin_over.png");
    public static BaseImage mail2_corner_gradient_bottom_right = new BaseImage(AssetPaths.AssetIconsMail2, "corner_gradient_bottom_right.png");
    public static BaseImage mail2_corner_Gradient_upper_left = new BaseImage(AssetPaths.AssetIconsMail2, "corner_Gradient_upper_left.png");
    public static BaseImage mail2_detach_attach_window_in = new BaseImage(AssetPaths.AssetIconsMail2, "detach_attach_window_in.png");
    public static BaseImage mail2_detach_attach_window_normal = new BaseImage(AssetPaths.AssetIconsMail2, "detach_attach_window_normal.png");
    public static BaseImage mail2_detach_attach_window_over = new BaseImage(AssetPaths.AssetIconsMail2, "detach_attach_window_over.png");
    public static BaseImage mail2_detach_window_in = new BaseImage(AssetPaths.AssetIconsMail2, "detach_window_in.png");
    public static BaseImage mail2_detach_window_normal = new BaseImage(AssetPaths.AssetIconsMail2, "detach_window_normal.png");
    public static BaseImage mail2_detach_window_over = new BaseImage(AssetPaths.AssetIconsMail2, "detach_window_over.png");
    public static BaseImage mail2_field_bar_mail_divider = new BaseImage(AssetPaths.AssetIconsMail2, "field_bar_mail_divider.png");
    public static BaseImage mail2_field_bar_mail_left = new BaseImage(AssetPaths.AssetIconsMail2, "field_bar_mail_left.png");
    public static BaseImage mail2_field_bar_mail_middle = new BaseImage(AssetPaths.AssetIconsMail2, "field_bar_mail_middle.png");
    public static BaseImage mail2_field_bar_mail_right = new BaseImage(AssetPaths.AssetIconsMail2, "field_bar_mail_right.png");
    public static BaseImage mail2_folder_icon_plus = new BaseImage(AssetPaths.AssetIconsMail2, "folder_icon_plus.png");
    public static BaseImage mail2_folder_icon_64_open = new BaseImage(AssetPaths.AssetIconsMail2, "folder_icon_64_open.png");
    public static BaseImage mail2_folder_icon_closed = new BaseImage(AssetPaths.AssetIconsMail2, "folder_icon_closed.png");
    public static BaseImage mail2_folder_icon_delete = new BaseImage(AssetPaths.AssetIconsMail2, "folder_icon_delete.png");
    public static BaseImage mail2_folder_icon_open = new BaseImage(AssetPaths.AssetIconsMail2, "folder_icon_open.png");
    public static BaseImage mail2_item_bar_tan_darker = new BaseImage(AssetPaths.AssetIconsMail2, "item_bar_tan-darker.png");
    public static BaseImage mail2_item_bar_tan_darker_over = new BaseImage(AssetPaths.AssetIconsMail2, "item_bar_tan-darker_over.png");
    public static BaseImage mail2_item_bar_tan_lighter = new BaseImage(AssetPaths.AssetIconsMail2, "item_bar_tan-lighter.png");
    public static BaseImage mail2_item_bar_tan_lighter_over = new BaseImage(AssetPaths.AssetIconsMail2, "item_bar_tan-lighter_over.png");
    public static BaseImage mail2_item_bar_white = new BaseImage(AssetPaths.AssetIconsMail2, "item_bar_white.png");
    public static BaseImage mail2_item_bar_white_over = new BaseImage(AssetPaths.AssetIconsMail2, "item_bar_white_over.png");
    public static BaseImage mail2_mail_icon = new BaseImage(AssetPaths.AssetIconsMail2, "mail_icon.png");
    public static BaseImage mail2_mail_inset_textline_left = new BaseImage(AssetPaths.AssetIconsMail2, "mail_inset-textline_left.png");
    public static BaseImage mail2_mail_inset_textline_middle = new BaseImage(AssetPaths.AssetIconsMail2, "mail_inset-textline_middle.png");
    public static BaseImage mail2_mail_inset_textline_right = new BaseImage(AssetPaths.AssetIconsMail2, "mail_inset-textline_right.png");
    public static BaseImage mail2_mail_panel_lower_left = new BaseImage(AssetPaths.AssetIconsMail2, "mail_panel_lower_left.png");
    public static BaseImage mail2_mail_panel_lower_middle = new BaseImage(AssetPaths.AssetIconsMail2, "mail_panel_lower_middle.png");
    public static BaseImage mail2_mail_panel_lower_right = new BaseImage(AssetPaths.AssetIconsMail2, "mail_panel_lower_right.png");
    public static BaseImage mail2_mail_panel_middle_left = new BaseImage(AssetPaths.AssetIconsMail2, "mail_panel_middle_left.png");
    public static BaseImage mail2_mail_panel_middle_middle = new BaseImage(AssetPaths.AssetIconsMail2, "mail_panel_middle_middle.png");
    public static BaseImage mail2_mail_panel_middle_right = new BaseImage(AssetPaths.AssetIconsMail2, "mail_panel_middle_right.png");
    public static BaseImage mail2_mail_panel_upper_left = new BaseImage(AssetPaths.AssetIconsMail2, "mail_panel_upper_left.png");
    public static BaseImage mail2_mail_panel_upper_middle = new BaseImage(AssetPaths.AssetIconsMail2, "mail_panel_upper_middle.png");
    public static BaseImage mail2_mail_panel_upper_right = new BaseImage(AssetPaths.AssetIconsMail2, "mail_panel_upper_right.png");
    public static BaseImage mail2_new_mail_body_bottom = new BaseImage(AssetPaths.AssetIconsMail2, "new_mail_body_bottom.png");
    public static BaseImage mail2_new_mail_body_middle = new BaseImage(AssetPaths.AssetIconsMail2, "new_mail_body_middle.png");
    public static BaseImage mail2_new_mail_body_top = new BaseImage(AssetPaths.AssetIconsMail2, "new_mail_body_top.png");
    public static BaseImage mail2_new_mail_tab_panel = new BaseImage(AssetPaths.AssetIconsMail2, "new_mail_tab_panel.png");
    public static BaseImage mail2_rounded_rectangle_tan_bottom_left = new BaseImage(AssetPaths.AssetIconsMail2, "rounded_rectangle_tan_bottom-left.png");
    public static BaseImage mail2_rounded_rectangle_tan_bottom_middle = new BaseImage(AssetPaths.AssetIconsMail2, "rounded_rectangle_tan_bottom-middle.png");
    public static BaseImage mail2_rounded_rectangle_tan_bottom_right = new BaseImage(AssetPaths.AssetIconsMail2, "rounded_rectangle_tan_bottom-right.png");
    public static BaseImage mail2_rounded_rectangle_tan_middle_left = new BaseImage(AssetPaths.AssetIconsMail2, "rounded_rectangle_tan_middle-left.png");
    public static BaseImage mail2_rounded_rectangle_tan_middle_middle = new BaseImage(AssetPaths.AssetIconsMail2, "rounded_rectangle_tan_middle-middle.png");
    public static BaseImage mail2_rounded_rectangle_tan_middle_right = new BaseImage(AssetPaths.AssetIconsMail2, "rounded_rectangle_tan_middle-right.png");
    public static BaseImage mail2_rounded_rectangle_tan_upper_left = new BaseImage(AssetPaths.AssetIconsMail2, "rounded_rectangle_tan_upper-left.png");
    public static BaseImage mail2_rounded_rectangle_tan_upper_middle = new BaseImage(AssetPaths.AssetIconsMail2, "rounded_rectangle_tan_upper-middle.png");
    public static BaseImage mail2_rounded_rectangle_tan_upper_right = new BaseImage(AssetPaths.AssetIconsMail2, "rounded_rectangle_tan_upper-right.png");
    public static BaseImage mail2_scrollbar_bottom = new BaseImage(AssetPaths.AssetIconsMail2, "scrollbar_bottom.png");
    public static BaseImage mail2_scrollbar_bottomarrow_in = new BaseImage(AssetPaths.AssetIconsMail2, "scrollbar_bottomarrow_in.png");
    public static BaseImage mail2_scrollbar_bottomarrow_normal = new BaseImage(AssetPaths.AssetIconsMail2, "scrollbar_bottomarrow_normal.png");
    public static BaseImage mail2_scrollbar_bottomarrow_over = new BaseImage(AssetPaths.AssetIconsMail2, "scrollbar_bottomarrow_over.png");
    public static BaseImage mail2_scrollbar_middle = new BaseImage(AssetPaths.AssetIconsMail2, "scrollbar_middle.png");
    public static BaseImage mail2_scrollbar_thumb_bottom = new BaseImage(AssetPaths.AssetIconsMail2, "scrollbar_thumb_bottom.png");
    public static BaseImage mail2_scrollbar_thumb_middle = new BaseImage(AssetPaths.AssetIconsMail2, "scrollbar_thumb_middle.png");
    public static BaseImage mail2_scrollbar_thumb_top = new BaseImage(AssetPaths.AssetIconsMail2, "scrollbar_thumb_top.png");
    public static BaseImage mail2_scrollbar_top = new BaseImage(AssetPaths.AssetIconsMail2, "scrollbar_top.png");
    public static BaseImage mail2_scrollbar_toparrow_in = new BaseImage(AssetPaths.AssetIconsMail2, "scrollbar_toparrow_in.png");
    public static BaseImage mail2_scrollbar_toparrow_normal = new BaseImage(AssetPaths.AssetIconsMail2, "scrollbar_toparrow_normal.png");
    public static BaseImage mail2_scrollbar_toparrow_over = new BaseImage(AssetPaths.AssetIconsMail2, "scrollbar_toparrow_over.png");
    public static BaseImage mail2_subject_bar_tan = new BaseImage(AssetPaths.AssetIconsMail2, "subject_bar_tan.png");
    public static BaseImage mail2_textline_middle = new BaseImage(AssetPaths.AssetIconsMail2, "textline_middle.png");
    public static BaseImage mail2_titlebar_left = new BaseImage(AssetPaths.AssetIconsMail2, "titlebar_left.png");
    public static BaseImage mail2_titlebar_middle = new BaseImage(AssetPaths.AssetIconsMail2, "titlebar_middle.png");
    public static BaseImage mail2_titlebar_right = new BaseImage(AssetPaths.AssetIconsMail2, "titlebar_right.png");
    public static BaseImage mail2_users_favourites_normal = new BaseImage(AssetPaths.AssetIconsMail2, "users_favourites_normal.png");
    public static BaseImage mail2_users_favourites_over = new BaseImage(AssetPaths.AssetIconsMail2, "users_favourites_over.png");
    public static BaseImage mail2_users_favourites_selected = new BaseImage(AssetPaths.AssetIconsMail2, "users_favourites_selected.png");
    public static BaseImage mail2_users_find_normal = new BaseImage(AssetPaths.AssetIconsMail2, "users_find_normal.png");
    public static BaseImage mail2_users_find_over = new BaseImage(AssetPaths.AssetIconsMail2, "users_find_over.png");
    public static BaseImage mail2_users_find_selected = new BaseImage(AssetPaths.AssetIconsMail2, "users_find_selected.png");
    public static BaseImage mail2_users_groups_normal = new BaseImage(AssetPaths.AssetIconsMail2, "users_groups_normal.png");
    public static BaseImage mail2_users_groups_over = new BaseImage(AssetPaths.AssetIconsMail2, "users_groups_over.png");
    public static BaseImage mail2_users_groups_selected = new BaseImage(AssetPaths.AssetIconsMail2, "users_groups_selected.png");
    public static BaseImage mail2_users_recent_normal = new BaseImage(AssetPaths.AssetIconsMail2, "users_recent_normal.png");
    public static BaseImage mail2_users_recent_over = new BaseImage(AssetPaths.AssetIconsMail2, "users_recent_over.png");
    public static BaseImage mail2_users_recent_selected = new BaseImage(AssetPaths.AssetIconsMail2, "users_recent_selected.png");
    public static BaseImage mail2_large_button_normal = new BaseImage(AssetPaths.AssetIconsMail2, "blue_infobar_01.png");
    public static BaseImage mail2_large_button_over = new BaseImage(AssetPaths.AssetIconsMail2, "blue_infobar_01_over.png");
    public static BaseImage mail2_attach_player_normal = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_user_normal.png");
    public static BaseImage mail2_attach_player_over = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_user_over.png");
    public static BaseImage mail2_attach_player_selected = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_user_selected.png");
    public static BaseImage mail2_attach_village_normal = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_village_normal.png");
    public static BaseImage mail2_attach_village_over = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_village_over.png");
    public static BaseImage mail2_attach_village_selected = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_village_selected.png");
    public static BaseImage mail2_attach_parish_normal = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_region_normal.png");
    public static BaseImage mail2_attach_parish_over = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_region_over.png");
    public static BaseImage mail2_attach_parish_selected = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_region_selected.png");
    public static BaseImage mail2_attach_current_normal = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_attachment_normal.png");
    public static BaseImage mail2_attach_current_over = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_attachment_over.png");
    public static BaseImage mail2_attach_current_selected = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_attachment_selected.png");
    public static BaseImage mail2_attach_type_player = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_user_icon.png");
    public static BaseImage mail2_attach_type_village = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_village_icon.png");
    public static BaseImage mail2_attach_type_parish = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_region_icon.png");
    public static BaseImage mail2_attach_icon = new BaseImage(AssetPaths.AssetIconsMail2, "mail_hyperlinking_attachment_icon.png");
    public static BaseImage background_top = new BaseImage(AssetPaths.AssetIconsStats, "background_top.png");
    public static BaseImage catagory_icons_achiever_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_achiever_normal.png");
    public static BaseImage catagory_icons_achiever_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_achiever_over.png");
    public static BaseImage catagory_icons_achiever_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_achiever_pushed.png");
    public static BaseImage catagory_icons_aikiller_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_aikiller_normal.png");
    public static BaseImage catagory_icons_aikiller_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_aikiller_over.png");
    public static BaseImage catagory_icons_aikiller_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_aikiller_pushed.png");
    public static BaseImage catagory_icons_banditslayer_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_banditslayer_normal.png");
    public static BaseImage catagory_icons_banditslayer_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_banditslayer_over.png");
    public static BaseImage catagory_icons_banditslayer_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_banditslayer_pushed.png");
    public static BaseImage catagory_icons_banquet_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_banquet_normal.png");
    public static BaseImage catagory_icons_banquet_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_banquet_over.png");
    public static BaseImage catagory_icons_banquet_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_banquet_pushed.png");
    public static BaseImage catagory_icons_blacksmith_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_blacksmith_normal.png");
    public static BaseImage catagory_icons_blacksmith_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_blacksmith_over.png");
    public static BaseImage catagory_icons_blacksmith_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_blacksmith_pushed.png");
    public static BaseImage catagory_icons_brewer_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_brewer_normal.png");
    public static BaseImage catagory_icons_brewer_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_brewer_over.png");
    public static BaseImage catagory_icons_brewer_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_brewer_pushed.png");
    public static BaseImage catagory_icons_defender_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_defender_normal.png");
    public static BaseImage catagory_icons_defender_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_defender_over.png");
    public static BaseImage catagory_icons_defender_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_defender_pushed.png");
    public static BaseImage catagory_icons_destroyer_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_destroyer_normal.png");
    public static BaseImage catagory_icons_destroyer_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_destroyer_over.png");
    public static BaseImage catagory_icons_destroyer_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_destroyer_pushed.png");
    public static BaseImage catagory_icons_donator_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_donator_normal.png");
    public static BaseImage catagory_icons_donator_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_donator_over.png");
    public static BaseImage catagory_icons_donator_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_donator_pushed.png");
    public static BaseImage catagory_icons_factions_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_factions_normal.png");
    public static BaseImage catagory_icons_factions_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_factions_over.png");
    public static BaseImage catagory_icons_factions_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_factions_pushed.png");
    public static BaseImage catagory_icons_farmer_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_farmer_normal.png");
    public static BaseImage catagory_icons_farmer_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_farmer_over.png");
    public static BaseImage catagory_icons_farmer_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_farmer_pushed.png");
    public static BaseImage catagory_icons_forger_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_forger_normal.png");
    public static BaseImage catagory_icons_forger_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_forger_over.png");
    public static BaseImage catagory_icons_forger_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_forger_pushed.png");
    public static BaseImage catagory_icons_houses_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_houses_normal.png");
    public static BaseImage catagory_icons_houses_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_houses_over.png");
    public static BaseImage catagory_icons_houses_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_houses_pushed.png");
    public static BaseImage catagory_icons_merchant_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_merchant_normal.png");
    public static BaseImage catagory_icons_merchant_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_merchant_over.png");
    public static BaseImage catagory_icons_merchant_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_merchant_pushed.png");
    public static BaseImage catagory_icons_parishes_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_parishes_normal.png");
    public static BaseImage catagory_icons_parishes_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_parishes_over.png");
    public static BaseImage catagory_icons_parishes_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_parishes_pushed.png");
    public static BaseImage catagory_icons_parishflags_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_parishflags_normal.png");
    public static BaseImage catagory_icons_parishflags_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_parishflags_over.png");
    public static BaseImage catagory_icons_parishflags_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_parishflags_pushed.png");
    public static BaseImage catagory_icons_pillager_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_pillager_normal.png");
    public static BaseImage catagory_icons_pillager_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_pillager_over.png");
    public static BaseImage catagory_icons_pillager_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_pillager_pushed.png");
    public static BaseImage catagory_icons_points_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_points_normal.png");
    public static BaseImage catagory_icons_points_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_points_over.png");
    public static BaseImage catagory_icons_points_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_points_pushed.png");
    public static BaseImage catagory_icons_rank_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_rank_normal.png");
    public static BaseImage catagory_icons_rank_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_rank_over.png");
    public static BaseImage catagory_icons_rank_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_rank_pushed.png");
    public static BaseImage catagory_icons_villages_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_villages_normal.png");
    public static BaseImage catagory_icons_villages_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_villages_over.png");
    public static BaseImage catagory_icons_villages_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_villages_pushed.png");
    public static BaseImage catagory_icons_wolfbane_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_wolfbane_normal.png");
    public static BaseImage catagory_icons_wolfbane_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_wolfbane_over.png");
    public static BaseImage catagory_icons_wolfbane_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_wolfbane_pushed.png");
    public static BaseImage catagory_icons_worker_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_worker_normal.png");
    public static BaseImage catagory_icons_worker_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_worker_over.png");
    public static BaseImage catagory_icons_worker_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_worker_pushed.png");
    public static BaseImage catagory_icons_capture_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_capture_normal.png");
    public static BaseImage catagory_icons_capture_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_capture_over.png");
    public static BaseImage catagory_icons_capture_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_capture_pushed.png");
    public static BaseImage catagory_icons_raze_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_raze_normal.png");
    public static BaseImage catagory_icons_raze_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_raze_over.png");
    public static BaseImage catagory_icons_raze_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_raze_pushed.png");
    public static BaseImage catagory_icons_glory_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_glory_normal.png");
    public static BaseImage catagory_icons_glory_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_glory_over.png");
    public static BaseImage catagory_icons_glory_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_glory_pushed.png");
    public static BaseImage catagory_icons_killstreak_normal = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_killstreak_normal.png");
    public static BaseImage catagory_icons_killstreak_over = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_killstreak_over.png");
    public static BaseImage catagory_icons_killstreak_pushed = new BaseImage(AssetPaths.AssetIconsStats, "catagory_icons_killstreak_pushed.png");
    public static BaseImage house_flag_001 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_001.png");
    public static BaseImage house_flag_002 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_002.png");
    public static BaseImage house_flag_003 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_003.png");
    public static BaseImage house_flag_004 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_004.png");
    public static BaseImage house_flag_005 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_005.png");
    public static BaseImage house_flag_006 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_006.png");
    public static BaseImage house_flag_007 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_007.png");
    public static BaseImage house_flag_008 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_008.png");
    public static BaseImage house_flag_009 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_009.png");
    public static BaseImage house_flag_010 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_010.png");
    public static BaseImage house_flag_011 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_011.png");
    public static BaseImage house_flag_012 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_012.png");
    public static BaseImage house_flag_013 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_013.png");
    public static BaseImage house_flag_014 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_014.png");
    public static BaseImage house_flag_015 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_015.png");
    public static BaseImage house_flag_016 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_016.png");
    public static BaseImage house_flag_017 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_017.png");
    public static BaseImage house_flag_018 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_018.png");
    public static BaseImage house_flag_019 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_019.png");
    public static BaseImage house_flag_020 = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_020.png");
    public static BaseImage house_flag_001_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_001_WL.png");
    public static BaseImage house_flag_002_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_002_WL.png");
    public static BaseImage house_flag_003_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_003_WL.png");
    public static BaseImage house_flag_004_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_004_WL.png");
    public static BaseImage house_flag_005_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_005_WL.png");
    public static BaseImage house_flag_006_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_006_WL.png");
    public static BaseImage house_flag_007_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_007_WL.png");
    public static BaseImage house_flag_008_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_008_WL.png");
    public static BaseImage house_flag_009_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_009_WL.png");
    public static BaseImage house_flag_010_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_010_WL.png");
    public static BaseImage house_flag_011_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_011_WL.png");
    public static BaseImage house_flag_012_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_012_WL.png");
    public static BaseImage house_flag_013_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_013_WL.png");
    public static BaseImage house_flag_014_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_014_WL.png");
    public static BaseImage house_flag_015_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_015_WL.png");
    public static BaseImage house_flag_016_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_016_WL.png");
    public static BaseImage house_flag_017_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_017_WL.png");
    public static BaseImage house_flag_018_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_018_WL.png");
    public static BaseImage house_flag_019_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_019_WL.png");
    public static BaseImage house_flag_020_WL = new BaseImage(AssetPaths.AssetIconsStats, "house_flag_020_WL.png");
    public static BaseImage int_statsscreen_iconbar_arrow_left_normal = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_iconbar_arrow-left_normal.png");
    public static BaseImage int_statsscreen_iconbar_arrow_left_over = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_iconbar_arrow-left_over.png");
    public static BaseImage int_statsscreen_iconbar_arrow_left_pressed = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_iconbar_arrow-left_pressed.png");
    public static BaseImage int_statsscreen_iconbar_arrow_right_normal = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_iconbar_arrow-right_normal.png");
    public static BaseImage int_statsscreen_iconbar_arrow_right_over = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_iconbar_arrow-right_over.png");
    public static BaseImage int_statsscreen_iconbar_arrow_right_pressed = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_iconbar_arrow-right_pressed.png");
    public static BaseImage int_statsscreen_iconbar_left = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_iconbar_left.png");
    public static BaseImage int_statsscreen_iconbar_middle = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_iconbar_middle.png");
    public static BaseImage int_statsscreen_iconbar_right = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_iconbar_right.png");
    public static BaseImage int_statsscreen_listbar_darker = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_listbar_darker.png");
    public static BaseImage int_statsscreen_listbar_lighter = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_listbar_lighter.png");
    public static BaseImage int_statsscreen_maininset_bottom = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_maininset_bottom.png");
    public static BaseImage int_statsscreen_maininset_middle = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_maininset_middle.png");
    public static BaseImage int_statsscreen_maininset_top_top = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_maininset_top_top.png");
    public static BaseImage int_statsscreen_maininset_top_middle = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_maininset_top_middle.png");
    public static BaseImage int_statsscreen_maininset_top_bottom = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_maininset_top_bottom.png");
    public static BaseImage int_statsscreen_search_button_normal = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_search_button_normal.png");
    public static BaseImage int_statsscreen_search_button_over = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_search_button_over.png");
    public static BaseImage int_statsscreen_search_button_pushed = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_search_button_pushed.png");
    public static BaseImage int_statsscreen_search_clear_button_normal = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_search_clear_button_normal.png");
    public static BaseImage int_statsscreen_search_clear_button_over = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_search_clear_button_over.png");
    public static BaseImage int_statsscreen_search_clear_button_pushed = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_search_clear_button_pushed.png");
    public static BaseImage int_statsscreen_search_inset = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_search_inset.png");
    public static BaseImage int_statsscreen_secondinset_bar_darker = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_secondinset_bar-darker.png");
    public static BaseImage int_statsscreen_secondinset_bar_lighter = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_secondinset_bar-lighter.png");
    public static BaseImage int_statsscreen_secondinset_bottom = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_secondinset_bottom.png");
    public static BaseImage int_statsscreen_secondinset_middle = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_secondinset_middle.png");
    public static BaseImage int_statsscreen_secondinset_top = new BaseImage(AssetPaths.AssetIconsStats, "int_statsscreen_secondinset_top.png");
    public static BaseImage page_bottom_normal = new BaseImage(AssetPaths.AssetIconsStats, "page_bottom_normal.png");
    public static BaseImage page_bottom_over = new BaseImage(AssetPaths.AssetIconsStats, "page_bottom_over.png");
    public static BaseImage page_bottom_pushed = new BaseImage(AssetPaths.AssetIconsStats, "page_bottom_pushed.png");
    public static BaseImage page_down_normal = new BaseImage(AssetPaths.AssetIconsStats, "page_down_normal.png");
    public static BaseImage page_down_over = new BaseImage(AssetPaths.AssetIconsStats, "page_down_over.png");
    public static BaseImage page_down_pushed = new BaseImage(AssetPaths.AssetIconsStats, "page_down_pushed.png");
    public static BaseImage page_top_norrmal = new BaseImage(AssetPaths.AssetIconsStats, "page_top_norrmal.png");
    public static BaseImage page_top_over = new BaseImage(AssetPaths.AssetIconsStats, "page_top_over.png");
    public static BaseImage page_top_pushed = new BaseImage(AssetPaths.AssetIconsStats, "page_top_pushed.png");
    public static BaseImage page_up_normal = new BaseImage(AssetPaths.AssetIconsStats, "page_up_normal.png");
    public static BaseImage page_up_over = new BaseImage(AssetPaths.AssetIconsStats, "page_up_over.png");
    public static BaseImage page_up_pushed = new BaseImage(AssetPaths.AssetIconsStats, "page_up_pushed.png");
    public static BaseImage arrow_up = new BaseImage(AssetPaths.AssetIconsStats, "arrow_up.png");
    public static BaseImage arrow_down = new BaseImage(AssetPaths.AssetIconsStats, "arrow_down.png");
    public static BaseImage button_blue_01_in = new BaseImage(AssetPaths.AssetIconsReports, "button_blue_01_in.png");
    public static BaseImage button_blue_01_normal = new BaseImage(AssetPaths.AssetIconsReports, "button_blue_01_normal.png");
    public static BaseImage button_blue_01_over = new BaseImage(AssetPaths.AssetIconsReports, "button_blue_01_over.png");
    public static BaseImage reports_checkbox_empty = new BaseImage(AssetPaths.AssetIconsReports, "checkbox_empty.png");
    public static BaseImage reports_checkbox_faded = new BaseImage(AssetPaths.AssetIconsReports, "checkbox_faded.png");
    public static BaseImage reports_checkbox_checked = new BaseImage(AssetPaths.AssetIconsReports, "checkbox_checked.png");
    public static BaseImage icon_arrow_down = new BaseImage(AssetPaths.AssetIconsReports, "icon_arrow_down.png");
    public static BaseImage icon_bang = new BaseImage(AssetPaths.AssetIconsReports, "icon_bang.png");
    public static BaseImage icon_capture = new BaseImage(AssetPaths.AssetIconsReports, "icon_capture.png");
    public static BaseImage icon_capture_over = new BaseImage(AssetPaths.AssetIconsReports, "icon_capture_over.png");
    public static BaseImage icon_filter = new BaseImage(AssetPaths.AssetIconsReports, "icon_filter.png");
    public static BaseImage icon_folder = new BaseImage(AssetPaths.AssetIconsReports, "icon_folder.png");
    public static BaseImage icon_filter_over = new BaseImage(AssetPaths.AssetIconsReports, "icon_filter_over.png");
    public static BaseImage icon_filter_selected = new BaseImage(AssetPaths.AssetIconsReports, "icon_filter_selected_normal.png");
    public static BaseImage icon_filter_selected_over = new BaseImage(AssetPaths.AssetIconsReports, "icon_filter_selected_over.png");
    public static BaseImage icon_trash = new BaseImage(AssetPaths.AssetIconsReports, "icon_trash_normal.png");
    public static BaseImage icon_trash_over = new BaseImage(AssetPaths.AssetIconsReports, "icon_trash_over.png");
    public static BaseImage icon_folder_back = new BaseImage(AssetPaths.AssetIconsReports, "icon_folder_back.png");
    public static BaseImage icon_scroll_closed = new BaseImage(AssetPaths.AssetIconsReports, "icon_scroll_closed.png");
    public static BaseImage iconband = new BaseImage(AssetPaths.AssetIconsReports, "iconband.png");
    public static BaseImage lineitem_strip_01_dark = new BaseImage(AssetPaths.AssetIconsReports, "lineitem_strip_01_dark.png");
    public static BaseImage lineitem_strip_01_light = new BaseImage(AssetPaths.AssetIconsReports, "lineitem_strip_01_light.png");
    public static BaseImage lineitem_strip_02_dark = new BaseImage(AssetPaths.AssetIconsReports, "lineitem_strip_02_dark.png");
    public static BaseImage lineitem_strip_02_light = new BaseImage(AssetPaths.AssetIconsReports, "lineitem_strip_02_light.png");
    public static BaseImage popup_background_01 = new BaseImage(AssetPaths.AssetIconsReports, "popup_background_01.png");
    public static BaseImage[] char_but_achievement = BaseImage.createFromUV(AssetPaths.AssetIconsUser, nameof (char_but_achievement), 3);
    public static BaseImage[] char_but_invite = BaseImage.createFromUV(AssetPaths.AssetIconsUser, nameof (char_but_invite), 2);
    public static BaseImage[] char_but_mail = BaseImage.createFromUV(AssetPaths.AssetIconsUser, nameof (char_but_mail), 2);
    public static BaseImage[] char_but_quest = BaseImage.createFromUV(AssetPaths.AssetIconsUser, nameof (char_but_quest), 3);
    public static BaseImage[] char_position = BaseImage.createFromUV(AssetPaths.AssetIconsUser, nameof (char_position), 8);
    public static BaseImage[] char_village_icons = BaseImage.createFromUV(AssetPaths.AssetIconsUser, nameof (char_village_icons), 20);
    public static BaseImage char_line_01 = new BaseImage(AssetPaths.AssetIconsUser, "char_line_01.png");
    public static BaseImage char_line_02 = new BaseImage(AssetPaths.AssetIconsUser, "char_line_02.png");
    public static BaseImage char_portraite_shadow = new BaseImage(AssetPaths.AssetIconsUser, "char_portraite_shadow.png");
    public static BaseImage char_shieldcomp_back = new BaseImage(AssetPaths.AssetIconsUser, "char_shieldcomp_back.png");
    public static BaseImage char_villagelist_inset = new BaseImage(AssetPaths.AssetIconsUser, "char_villagelist_inset.png");
    public static BaseImage char_shadow_faction = new BaseImage(AssetPaths.AssetIconsUser, "shadow_faction.png");
    public static BaseImage char_shadow_house = new BaseImage(AssetPaths.AssetIconsUser, "shadow_house.png");
    public static BaseImage[] custom_player_marker = new BaseImage[4]
    {
      new BaseImage(AssetPaths.AssetIconsUser, "playerMarker1.png"),
      new BaseImage(AssetPaths.AssetIconsUser, "playerMarker2.png"),
      new BaseImage(AssetPaths.AssetIconsUser, "playerMarker3.png"),
      new BaseImage(AssetPaths.AssetIconsUser, "playerMarker4.png")
    };
    public static BaseImage[] custom_player_marker_selected = new BaseImage[4]
    {
      new BaseImage(AssetPaths.AssetIconsUser, "playerMarker1_select.png"),
      new BaseImage(AssetPaths.AssetIconsUser, "playerMarker2_select.png"),
      new BaseImage(AssetPaths.AssetIconsUser, "playerMarker3_select.png"),
      new BaseImage(AssetPaths.AssetIconsUser, "playerMarker4_select.png")
    };
    public static BaseImage[] tutorial_illustrations = BaseImage.createFromUV(AssetPaths.AssetIconsTutorial, "tutorial_illustration_array", 30);
    public static BaseImage[] tutorial_arrow_yellow = BaseImage.createFromUV(AssetPaths.AssetIconsTutorial, "tutorial_arrow_3d_yellow", 2);
    public static BaseImage[] tutorial_reward_anim = BaseImage.createFromUV(AssetPaths.AssetIconsTutorial, "reward_burst_x20", 20);
    public static BaseImage tutorial_background = new BaseImage(AssetPaths.AssetIconsTutorial, nameof (tutorial_background));
    public static BaseImage tutorial_button_normal = new BaseImage(AssetPaths.AssetIconsTutorial, nameof (tutorial_button_normal));
    public static BaseImage tutorial_button_over = new BaseImage(AssetPaths.AssetIconsTutorial, nameof (tutorial_button_over));
    public static BaseImage minimize_Normal = new BaseImage(AssetPaths.AssetIconsTutorial, nameof (minimize_Normal));
    public static BaseImage minimize_Over = new BaseImage(AssetPaths.AssetIconsTutorial, nameof (minimize_Over));
    public static BaseImage tutorial_button_glow = new BaseImage(AssetPaths.AssetIconsTutorial, "tut___but_glow");
    public static BaseImage tutorial_longarm1 = new BaseImage(AssetPaths.AssetIconsTutorial, "longarms1-1.png");
    public static BaseImage tutorial_longarm2 = new BaseImage(AssetPaths.AssetIconsTutorial, "longarms2-1.png");
    public static BaseImage tutorial_longarm3 = new BaseImage(AssetPaths.AssetIconsTutorial, "longarms3-1.png");
    public static BaseImage tutorial_longarm4 = new BaseImage(AssetPaths.AssetIconsTutorial, "longarms4-1.png");
    public static BaseImage tutorial_longarm5 = new BaseImage(AssetPaths.AssetIconsTutorial, "longarms5-1.png");
    public static BaseImage tutorial_longarm6 = new BaseImage(AssetPaths.AssetIconsTutorial, "longarms6-1.png");
    public static BaseImage tutorial_longarm7 = new BaseImage(AssetPaths.AssetIconsTutorial, "longarms7-1.png");
    public static BaseImage tutorial_longarm8 = new BaseImage(AssetPaths.AssetIconsTutorial, "longarms8-1.png");
    public static BaseImage tutorial_longarm9 = new BaseImage(AssetPaths.AssetIconsTutorial, "longarms9-1.png");
    public static BaseImage tutorial_longarm10 = new BaseImage(AssetPaths.AssetIconsTutorial, "longarms10-1.png");
    public static BaseImage tutorial_longarm11 = new BaseImage(AssetPaths.AssetIconsTutorial, "longarms11-1.png");
    public static BaseImage tutorial_longarm12 = new BaseImage(AssetPaths.AssetIconsTutorial, "longarms12-1.png");
    public static BaseImage glory_background = new BaseImage(AssetPaths.AssetIconsGlory, "background");
    public static BaseImage glory_thin_pole = new BaseImage(AssetPaths.AssetIconsGlory, "ploe_thin");
    public static BaseImage glory_thick_pole = new BaseImage(AssetPaths.AssetIconsGlory, "ploe_thick");
    public static BaseImage glory_star_large = new BaseImage(AssetPaths.AssetIconsGlory, "star_01");
    public static BaseImage glory_star_small = new BaseImage(AssetPaths.AssetIconsGlory, "star_02");
    public static BaseImage FactionTabBar_1_Normal = new BaseImage(AssetPaths.AssetIconsGlory, "FactionTabBar_1_Normal.png");
    public static BaseImage FactionTabBar_1_Selected = new BaseImage(AssetPaths.AssetIconsGlory, "FactionTabBar_1_Selected.png");
    public static BaseImage FactionTabBar_2_Normal = new BaseImage(AssetPaths.AssetIconsGlory, "FactionTabBar_2_Normal.png");
    public static BaseImage FactionTabBar_2_Selected = new BaseImage(AssetPaths.AssetIconsGlory, "FactionTabBar_2_Selected.png");
    public static BaseImage FactionTabBar_3_Normal = new BaseImage(AssetPaths.AssetIconsGlory, "FactionTabBar_3_Normal.png");
    public static BaseImage FactionTabBar_3_Selected = new BaseImage(AssetPaths.AssetIconsGlory, "FactionTabBar_3_Selected.png");
    public static BaseImage faction_pen = new BaseImage(AssetPaths.AssetIconsGlory, "rdit_icon");
    public static BaseImage faction_background = new BaseImage(AssetPaths.AssetIconsGlory, "faction_right_panel_back.png");
    public static BaseImage faction_background_bottom = new BaseImage(AssetPaths.AssetIconsGlory, "faction_right_panel_back_bottom.png");
    public static BaseImage faction_button_background = new BaseImage(AssetPaths.AssetIconsGlory, "faction_button_selected.png");
    public static BaseImage faction_button_background1 = new BaseImage(AssetPaths.AssetIconsGlory, "faction_button_selected_row1.png");
    public static BaseImage faction_button_background2 = new BaseImage(AssetPaths.AssetIconsGlory, "faction_button_selected_row2.png");
    public static BaseImage faction_button_background3 = new BaseImage(AssetPaths.AssetIconsGlory, "faction_button_selected_row3.png");
    public static BaseImage trashcan_normal = new BaseImage(AssetPaths.AssetIconsGlory, "trashcan_25tall_semitrans.png");
    public static BaseImage trashcan_over = new BaseImage(AssetPaths.AssetIconsGlory, "trashcan_25tal_over.png");
    public static BaseImage trashcan_clicked = new BaseImage(AssetPaths.AssetIconsGlory, "trashcan_25tall.png");
    public static BaseImage house_circles_medium_selected_top = new BaseImage(AssetPaths.AssetIconsGlory, "house_circles_medium_selected_top.png");
    public static BaseImage faction_flag_outline_100 = new BaseImage(AssetPaths.AssetIconsGlory, "flag_outline_100.png");
    public static BaseImage faction_flag_outline_50 = new BaseImage(AssetPaths.AssetIconsGlory, "flag_outline_50.png");
    public static BaseImage faction_flag_outline_25 = new BaseImage(AssetPaths.AssetIconsGlory, "flag_outline_25.png");
    public static BaseImage faction_inset = new BaseImage(AssetPaths.AssetIconsGlory, "color picker inset.png");
    public static BaseImage arrow_button_left_normal = new BaseImage(AssetPaths.AssetIconsGlory, "arrow_button_left_normal.png");
    public static BaseImage arrow_button_left_over = new BaseImage(AssetPaths.AssetIconsGlory, "arrow_button_left_over.png");
    public static BaseImage arrow_button_left_pushed = new BaseImage(AssetPaths.AssetIconsGlory, "arrow_button_left_pushed.png");
    public static BaseImage arrow_button_right_normal = new BaseImage(AssetPaths.AssetIconsGlory, "arrow_button_right_normal.png");
    public static BaseImage arrow_button_right_over = new BaseImage(AssetPaths.AssetIconsGlory, "arrow_button_right_over.png");
    public static BaseImage arrow_button_right_pushed = new BaseImage(AssetPaths.AssetIconsGlory, "arrow_button_right_pushed.png");
    public static BaseImage glory_frame = new BaseImage(AssetPaths.AssetIconsGlory, "glory frame.png");
    public static BaseImage faction_title_band = new BaseImage(AssetPaths.AssetIconsGlory, "title_band.png");
    public static BaseImage faction_tanback = new BaseImage(AssetPaths.AssetIconsGlory, "faction_tanback.png");
    public static BaseImage faction_bar_tan_1_lighter = new BaseImage(AssetPaths.AssetIconsGlory, "faction_bar_tan_1_lighter.png");
    public static BaseImage faction_bar_tan_1_heavier = new BaseImage(AssetPaths.AssetIconsGlory, "faction_bar_tan_1_heavier.png");
    public static BaseImage faction_bar_tan_2_lighter = new BaseImage(AssetPaths.AssetIconsGlory, "faction_bar_tan_2_lighter.png");
    public static BaseImage faction_bar_tan_2_heavier = new BaseImage(AssetPaths.AssetIconsGlory, "faction_bar_tan_2_heavier.png");
    public static BaseImage eow_left = new BaseImage(AssetPaths.AssetIconsGlory, "EoW_Screen-Background-Left.png");
    public static BaseImage eow_right = new BaseImage(AssetPaths.AssetIconsGlory, "EoW_Screen-Background-Right.png");
    public static BaseImage eow_right_paper = new BaseImage(AssetPaths.AssetIconsGlory, "EoW_Screen-Background-Right_paper.png");
    public static BaseImage[] eow_toggle = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, "EoW_button-toggle-array", 6);
    public static BaseImage[] eow_buttons = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, "EoW_EndWorld-button_array_with-highlight", 27);
    public static BaseImage[] factionFlags = new BaseImage[64]
    {
      new BaseImage(AssetPaths.AssetIconsGlory, "f_00.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_01.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_02.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_03.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_04.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_05.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_06.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_07.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_08.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_09.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_10.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_11.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_12.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_13.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_14.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_15.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_16.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_17.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_18.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_19.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_20.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_21.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_22.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_23.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_24.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_25.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_26.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_27.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_28.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_29.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_30.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_31.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_32.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_33.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_34.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_35.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_36.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_37.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_38.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_39.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_40.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_41.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_42.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_43.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_44.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_45.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_46.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_47.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_48.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_49.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_50.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_51.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_52.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_53.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_54.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_55.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_56.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_57.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_58.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_59.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_60.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_61.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_62.png"),
      new BaseImage(AssetPaths.AssetIconsGlory, "f_63.png")
    };
    public static BaseImage[] glory_flags_largest = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, "flags_array_largest", 20);
    public static BaseImage[] glory_flags_large = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, "flags_array_large", 20);
    public static BaseImage[] glory_flags_med = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, "flags_array_med", 20);
    public static BaseImage[] glory_flags_small = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, "flags_array_small", 20);
    public static BaseImage[] glory_flags_largest_warlords = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, "flags_array_largest_warlords", 20);
    public static BaseImage[] glory_flags_large_warlords = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, "flags_array_large_warlords", 20);
    public static BaseImage[] glory_flags_med_warlords = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, "flags_array_med_warlords", 20);
    public static BaseImage[] glory_flags_small_warlords = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, "flags_array_small_warlords", 20);
    public static BaseImage[] faction_leaders = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, "leader_officer", 2);
    public static BaseImage[] faction_relationships = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, "sword_rosetta", 3);
    public static BaseImage[] faction_buttons = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, nameof (faction_buttons), 24);
    public static BaseImage[] house_circles_medium = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, nameof (house_circles_medium), 40);
    public static BaseImage[] house_circles_large = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, nameof (house_circles_large), 20);
    public static BaseImage[] house_circles_medium_warlords = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, nameof (house_circles_medium_warlords), 40);
    public static BaseImage[] house_circles_large_warlords = BaseImage.createFromUV(AssetPaths.AssetIconsGlory, nameof (house_circles_large_warlords), 20);
    public static BaseImage[] free_card_screen_cardback_array = BaseImage.createFromUV(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_cardback_array), 14);
    public static BaseImage[] free_card_screen_wax_array = BaseImage.createFromUV(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_wax_array), 10);
    public static BaseImage free_card_screen_card_fan = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_card_fan));
    public static BaseImage free_card_screen_green_panel_bottom_left = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_green_panel_bottom_left));
    public static BaseImage free_card_screen_green_panel_bottom_mid = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_green_panel_bottom_mid));
    public static BaseImage free_card_screen_green_panel_bottom_right = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_green_panel_bottom_right));
    public static BaseImage free_card_screen_green_panel_mid_left = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_green_panel_mid_left));
    public static BaseImage free_card_screen_green_panel_mid_mid = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_green_panel_mid_mid));
    public static BaseImage free_card_screen_green_panel_mid_right = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_green_panel_mid_right));
    public static BaseImage free_card_screen_green_panel_top_left = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_green_panel_top_left));
    public static BaseImage free_card_screen_green_panel_top_mid = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_green_panel_top_mid));
    public static BaseImage free_card_screen_green_panel_top_right = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_green_panel_top_right));
    public static BaseImage free_card_screen_progbar_fill = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_progbar_fill));
    public static BaseImage free_card_screen_progbar_left = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_progbar_left));
    public static BaseImage free_card_screen_progbar_mid = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_progbar_mid));
    public static BaseImage free_card_screen_progbar_right = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (free_card_screen_progbar_right));
    public static BaseImage you_got_free_card_screen_cardback = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (you_got_free_card_screen_cardback));
    public static BaseImage you_got_free_card_screen_parchment = new BaseImage(AssetPaths.AssetIconsFreeCards, nameof (you_got_free_card_screen_parchment));
    public static BaseImage[] villageType_illustrations = BaseImage.createFromUV(AssetPaths.AssetIconsVillageType, "new_village_type_illustration_array", 20);
    public static BaseImage[] villageType_types = BaseImage.createFromUV(AssetPaths.AssetIconsVillageType, "new_village_type_array", 11);
    public static BaseImage[] villageType_helpButton = BaseImage.createFromUV(AssetPaths.AssetIconsVillageType, "new_village_button_help", 2);
    public static BaseImage villageType_inset_top = new BaseImage(AssetPaths.AssetIconsVillageType, "new_village_inset_top");
    public static BaseImage villageType_inset_mid = new BaseImage(AssetPaths.AssetIconsVillageType, "new_village_inset_middle");
    public static BaseImage villageType_inset_bottom = new BaseImage(AssetPaths.AssetIconsVillageType, "new_village_inset_bottom");
    public static BaseImage[] mrhp_button_more_info_solid = BaseImage.createFromUV(AssetPaths.AssetIconsMapPanel, "button_more_info_solid", 2);
    public static BaseImage[] mrhp_location_portrait = BaseImage.createFromUV(AssetPaths.AssetIconsMapPanel, "location_portrait", 51);
    public static BaseImage[] mrhp_world_icons_rhs_array = BaseImage.createFromUV(AssetPaths.AssetIconsMapPanel, "world_icons_rhs_array", 39);
    public static BaseImage[] mrhp_travelling_buttons = BaseImage.createFromUV(AssetPaths.AssetIconsMapPanel, "Travelling_buttons", 3);
    public static BaseImage[] mrhp_travelling_arrows = BaseImage.createFromUV(AssetPaths.AssetIconsMapPanel, "Travelling_arrows", 2);
    public static BaseImage[] mrhp_village_type_miniicons = BaseImage.createFromUV(AssetPaths.AssetIconsMapPanel, "village_type_miniicons", 30);
    public static BaseImage[] mrhp_button_150x25 = BaseImage.createFromUV(AssetPaths.AssetIconsMapPanel, "button_150x25", 3);
    public static BaseImage[] mrhp_world_filter_check = BaseImage.createFromUV(AssetPaths.AssetIconsMapPanel, "world_filter_check", 2);
    public static BaseImage[] mrhp_button_filter_off = BaseImage.createFromUV(AssetPaths.AssetIconsMapPanel, "button_filter_off", 12);
    public static BaseImage[] mrhp_button_filter_search = BaseImage.createFromUV(AssetPaths.AssetIconsMapPanel, "button_search", 3);
    public static BaseImage[] mrhp_button_filter_ai = BaseImage.createFromUV(AssetPaths.AssetIconsMapPanel, "world_icons_rhs_array_ai", 3);
    public static BaseImage[] mrhp_button_envelope = BaseImage.createFromUV(AssetPaths.AssetIconsMapPanel, "button_envelope", 3);
    public static BaseImage mrhp_avatar_frame = new BaseImage(AssetPaths.AssetIconsMapPanel, "avatar_frame");
    public static BaseImage mrhp_button_filter_over = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_filter_over");
    public static BaseImage mrhp_button_filter_normal = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_filter_normal");
    public static BaseImage mrhp_button_more_info = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_more_info");
    public static BaseImage mrhp_button_more_info_over = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_more_info_over");
    public static BaseImage mrhp_world_panel_102 = new BaseImage(AssetPaths.AssetIconsMapPanel, "world_panel_102");
    public static BaseImage mrhp_world_panel_132 = new BaseImage(AssetPaths.AssetIconsMapPanel, "world_panel_132");
    public static BaseImage mrhp_world_panel_192 = new BaseImage(AssetPaths.AssetIconsMapPanel, "world_panel_192");
    public static BaseImage mrhp_location_portrait_glow_short = new BaseImage(AssetPaths.AssetIconsMapPanel, "location_portrait_glow_small");
    public static BaseImage mrhp_location_portrait_glow_long = new BaseImage(AssetPaths.AssetIconsMapPanel, "location_portrait_glow_long");
    public static BaseImage mrhp_button_check_normal = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_check_normal");
    public static BaseImage mrhp_button_check_over = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_check_over");
    public static BaseImage mrhp_button_check_pushed = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_check_pushed");
    public static BaseImage mrhp_button_x_normal = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_x_normal");
    public static BaseImage mrhp_button_x_over = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_x_over");
    public static BaseImage mrhp_button_x_pushed = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_x_pushed");
    public static BaseImage mrhp_button_80_normal = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_80_normal");
    public static BaseImage mrhp_button_80_over = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_80_over");
    public static BaseImage mrhp_button_80_pushed = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_80_pushed");
    public static BaseImage mrhp_reports = new BaseImage(AssetPaths.AssetIconsMapPanel, "int_world_icon_scroll");
    public static BaseImage mrhp_avatar_frame_background = new BaseImage(AssetPaths.AssetIconsMapPanel, "avatar_frame_background.png");
    public static BaseImage mrhp_shield_blank = new BaseImage(AssetPaths.AssetIconsMapPanel, "shield_blank.png");
    public static BaseImage mrhp_world_icons_filter_selected = new BaseImage(AssetPaths.AssetIconsMapPanel, "world_icons_filter_selected.png");
    public static BaseImage mrhp_button_filter_off_normal = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_filter_off_normal.png");
    public static BaseImage mrhp_button_filter_off_over = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_filter_off_over.png");
    public static BaseImage mrhp_button_attack_normal = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_attack_normal.png");
    public static BaseImage mrhp_button_attack_over = new BaseImage(AssetPaths.AssetIconsMapPanel, "button_attack_over.png");
    public static BaseImage star_market_3 = new BaseImage(AssetPaths.AssetIconsMapPanel, "star_market_03.png");
    public static BaseImage BlankCard = new BaseImage(AssetPaths.AssetIconsCards, "card_back_39x56.png");
    public static BaseImage BlankCardShadow = new BaseImage(AssetPaths.AssetIconsCards, "card_back_39x56_plus_shadow.png");
    public static BaseImage BlankCardShadow_Highlight = new BaseImage(AssetPaths.AssetIconsCards, "card_back_39x56_plus_shadow_bright.png");
    public static BaseImage BlueCardOverlay = new BaseImage(AssetPaths.AssetIconsCards, "card_border_blue_39x56.png");
    public static BaseImage GreenCardOverlay = new BaseImage(AssetPaths.AssetIconsCards, "card_border_green_39x56.png");
    public static BaseImage PurpleCardOverlay = new BaseImage(AssetPaths.AssetIconsCards, "card_border_purple_39x56.png");
    public static BaseImage RedCardOverlay = new BaseImage(AssetPaths.AssetIconsCards, "card_border_red_39x56.png");
    public static BaseImage YellowCardOverlay = new BaseImage(AssetPaths.AssetIconsCards, "card_border_yellow_39x56.png");
    public static BaseImage GreyCardOverlay = new BaseImage(AssetPaths.AssetIconsCards, "card_greyscale_39x56.png");
    public static BaseImage BlueCardOverlayNobar = new BaseImage(AssetPaths.AssetIconsCards, "card_border_blue_39x56_nobar.png");
    public static BaseImage GreenCardOverlayNobar = new BaseImage(AssetPaths.AssetIconsCards, "card_border_green_39x56_nobar.png");
    public static BaseImage PurpleCardOverlayNobar = new BaseImage(AssetPaths.AssetIconsCards, "card_border_purple_39x56_nobar.png");
    public static BaseImage RedCardOverlayNobar = new BaseImage(AssetPaths.AssetIconsCards, "card_border_red_39x56_nobar.png");
    public static BaseImage YellowCardOverlayNobar = new BaseImage(AssetPaths.AssetIconsCards, "card_border_yellow_39x56_nobar.png");
    public static BaseImage menubar_middle_gold = new BaseImage(AssetPaths.AssetIconsCards, "menubar_middle_gold.png");
    public static BaseImage menubar_middle_over = new BaseImage(AssetPaths.AssetIconsCards, "menubar_middle_over.png");
    public static BaseImage menubar_middle_offer = new BaseImage(AssetPaths.AssetIconsCards, "menubar_middle_offer.png");
    public static BaseImage menubar_middle_gold_offer = new BaseImage(AssetPaths.AssetIconsCards, "menubar_middle_gold_offer.png");
    public static BaseImage menubar_middle_gold_over = new BaseImage(AssetPaths.AssetIconsCards, "menubar_middle_gold_over.png");
    public static BaseImage card_circles_card = new BaseImage(AssetPaths.AssetIconsCards, "card_circle_cards.png");
    public static BaseImage[] cardbar_expand = new BaseImage[3]
    {
      new BaseImage(AssetPaths.AssetIconsCards, "cardbar_open_up.png"),
      new BaseImage(AssetPaths.AssetIconsCards, "cardbar_open_over.png"),
      new BaseImage(AssetPaths.AssetIconsCards, "cardbar_open_down.png")
    };
    public static BaseImage[] cardbar_collapse = new BaseImage[3]
    {
      new BaseImage(AssetPaths.AssetIconsCards, "cardbar_close_up.png"),
      new BaseImage(AssetPaths.AssetIconsCards, "cardbar_close_over.png"),
      new BaseImage(AssetPaths.AssetIconsCards, "cardbar_close_down.png")
    };
    public static BaseImage[] cardbar_left = new BaseImage[3]
    {
      new BaseImage(AssetPaths.AssetIconsCards, "cardbar_left_up.png"),
      new BaseImage(AssetPaths.AssetIconsCards, "cardbar_left_over.png"),
      new BaseImage(AssetPaths.AssetIconsCards, "cardbar_left_down.png")
    };
    public static BaseImage[] cardbar_right = new BaseImage[3]
    {
      new BaseImage(AssetPaths.AssetIconsCards, "cardbar_right_up.png"),
      new BaseImage(AssetPaths.AssetIconsCards, "cardbar_right_over.png"),
      new BaseImage(AssetPaths.AssetIconsCards, "cardbar_right_down.png")
    };
    public static BaseImage[] card_circles_timer = BaseImage.createFromUV(AssetPaths.AssetIconsCards, "card_circle_timer_array", 65);
    public static BaseImage[] card_circles_icons = BaseImage.createFromUV(AssetPaths.AssetIconsCards, "card_circle_icons", 106);
    public static BaseImage NoImageCardBig = new BaseImage(AssetPaths.AssetIconsBigCards, "_no_image_yet.jpg");
    public static BaseImage CardGradeBronze = new BaseImage(AssetPaths.AssetIconsCardGrade, "card_175xX242_coin_bronze.png");
    public static BaseImage CardGradeSilver = new BaseImage(AssetPaths.AssetIconsCardGrade, "card_175xX242_coin_platinum.png");
    public static BaseImage CardGradeGold = new BaseImage(AssetPaths.AssetIconsCardGrade, "card_175xX242_coin_gold.png");
    public static BaseImage CardGradeDiamond = new BaseImage(AssetPaths.AssetIconsCardGrade, "card_175xX242_coin_gem.png");
    public static BaseImage CardGradeDiamond2 = new BaseImage(AssetPaths.AssetIconsCardGrade, "card_175xX242_coin_gem.png");
    public static BaseImage card_frame_overlay_diamond = new BaseImage(AssetPaths.AssetIconsCardGrade, nameof (card_frame_overlay_diamond));
    public static BaseImage card_frame_overlay_gold = new BaseImage(AssetPaths.AssetIconsCardGrade, nameof (card_frame_overlay_gold));
    public static BaseImage card_frame_overlay_sapphire = new BaseImage(AssetPaths.AssetIconsCardGrade, nameof (card_frame_overlay_sapphire));
    public static BaseImage[] card_diamond_anim = BaseImage.createFromUV(AssetPaths.AssetIconsCardGrade, "diamond_array_blueish", 64);
    public static BaseImage[] card_diamond2_anim = BaseImage.createFromUV(AssetPaths.AssetIconsCardGrade, "diamond_70_x2_blue", 64);
    public static BaseImage[] card_diamond3_anim = BaseImage.createFromUV(AssetPaths.AssetIconsCardGrade, "diamond_70_blue", 64);
    public static BaseImage[] card_gold_anim = BaseImage.createFromUV(AssetPaths.AssetIconsCardGrade, "coin_55_gold", 64);
    public static BaseImage[] card_sapphire_anim = BaseImage.createFromUV(AssetPaths.AssetIconsCardGrade, "sapphire", 64);
    public static BaseImage BlueCardOverlayBig = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_blue.png");
    public static BaseImage GreenCardOverlayBig = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_green.png");
    public static BaseImage RedCardOverlayBig = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_red.png");
    public static BaseImage YellowCardOverlayBig = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_yellow.png");
    public static BaseImage PurpleCardOverlayBig = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_purple.png");
    public static BaseImage BlueCardOverlayBigOver = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_blue_brigh.png");
    public static BaseImage GreenCardOverlayBigOver = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_green_bright.png");
    public static BaseImage RedCardOverlayBigOver = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_red_brigh.png");
    public static BaseImage YellowCardOverlayBigOver = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_yellow_brigh.png");
    public static BaseImage PurpleCardOverlayBigOver = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_purple_brigh.png");
    public static BaseImage BlueCardOverlayEmpty = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_blue_empty.png");
    public static BaseImage GreenCardOverlayEmpty = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_green_empty.png");
    public static BaseImage RedCardOverlayEmpty = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_red_empty.png");
    public static BaseImage YellowCardOverlayEmpty = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_yellow_empty.png");
    public static BaseImage PurpleCardOverlayEmpty = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_purple_empty.png");
    public static BaseImage CardBackBig = new BaseImage(AssetPaths.AssetIconsCardFrame, "card_175xX242_frame_empty.png");
    public static BaseImage[] card_offer_pieces = BaseImage.createFromUV(AssetPaths.AssetIconsCardOffers, "card_ad_bitz", 4);
    public static BaseImage card_offer_background = new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_back");
    public static BaseImage card_offer_background_over = new BaseImage(AssetPaths.AssetIconsCardOffers, "buy_pack_button_over");
    public static Dictionary<string, BaseImage> CardPackImages;
    public static BaseImage[] CardSlotAnimFrames = new BaseImage[21]
    {
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_apple_blur_middle.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_apple_blur_bottom.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_castle_blur_top.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_castle_blur_middle.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_castle_blur_bottom.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_crown_blur_top.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_crown_blur_middle.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_crown_blur_bottom.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_hawk_blur_top.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_hawk_blur_middle.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_hawk_blur_bottom.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_jester_blur_top.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_jester_blur_middle.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_jester_blur_bottom.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_shield_blur_top.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_shield_blur_middle.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_shield_blur_bottom.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_wolf_blur_top.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_wolf_blur_middle.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_wolf_blur_bottom.png"),
      new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_apple_blur_top.png")
    };
    public static BaseImage CardSlotFrame = new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_frame.png");
    public static BaseImage[] CardFilters_All = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_all_%STATE%.png");
    public static BaseImage[] CardFilters_Food = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_food_%STATE%.png");
    public static BaseImage[] CardFilters_Resources = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_resources_%STATE%.png");
    public static BaseImage[] CardFilters_Honour = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_honour_%STATE%.png");
    public static BaseImage[] CardFilters_Weapons = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_weapons_%STATE%.png");
    public static BaseImage[] CardFilters_Research = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_research_%STATE%.png");
    public static BaseImage[] CardFilters_Industry = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_village_%STATE%.png");
    public static BaseImage[] CardFilters_Castle = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_castle_%STATE%.png");
    public static BaseImage[] CardFilters_Army = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_army_%STATE%.png");
    public static BaseImage[] CardFilters_Scouting = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_scout_%STATE%.png");
    public static BaseImage[] CardFilters_Trading = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_merchant_%STATE%.png");
    public static BaseImage[] CardFilters_Religion = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_monk_%STATE%.png");
    public static BaseImage[] CardFilters_Weapons2 = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_anvil_%STATE%.png");
    public static BaseImage[] CardFilters_Bread = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_bread_%STATE%.png");
    public static BaseImage[] CardFilters_Fish = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_fish_%STATE%.png");
    public static BaseImage[] CardFilters_Apples = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_food_apples_%STATE%.png");
    public static BaseImage[] CardFilters_Cheese = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_food_cheese_%STATE%.png");
    public static BaseImage[] CardFilters_Meat = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_meat_%STATE%.png");
    public static BaseImage[] CardFilters_Veg = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_veg_%STATE%.png");
    public static BaseImage[] CardFilters_Specialist = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_facecrown_%STATE%.png");
    public static BaseImage[] CardFilters_Parish = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_flag_%STATE%.png");
    public static BaseImage[] CardFilters_Playable = BaseImage.createButton(AssetPaths.AssetIconsCardPanel, "card_type_buttons_chevron_%STATE%.png");
    public static BaseImage LoginShieldPlaceholder = new BaseImage(AssetPaths.AssetIconsCardPanel, "profile_COA_placeholder.png");
    public static BaseImage cardpanel_cashin_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "cash_in_normal.png");
    public static BaseImage cardpanel_cashin_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "cash_in_over.png");
    public static BaseImage cardpanel_cashin_in = new BaseImage(AssetPaths.AssetIconsCardPanel, "cash_in_in.png");
    public static BaseImage cardpanel_manage_tabs_white_left = new BaseImage(AssetPaths.AssetIconsCardPanel, "tabs_white_left.png");
    public static BaseImage cardpanel_manage_tabs_white_right = new BaseImage(AssetPaths.AssetIconsCardPanel, "tabs_white_right.png");
    public static BaseImage cardpanel_manage_card_points_icon = new BaseImage(AssetPaths.AssetIconsCardPanel, "card_point_icon.png");
    public static BaseImage cardpanel_prem_timer_back_left = new BaseImage(AssetPaths.AssetIconsCardPanel, "timerbar_back_left.png");
    public static BaseImage cardpanel_prem_timer_back_mid = new BaseImage(AssetPaths.AssetIconsCardPanel, "timerbar_back_mid.png");
    public static BaseImage cardpanel_prem_timer_back_right = new BaseImage(AssetPaths.AssetIconsCardPanel, "timerbar_back_right.png");
    public static BaseImage cardpanel_prem_timer_fill_left = new BaseImage(AssetPaths.AssetIconsCardPanel, "timerbar_fill_left.png");
    public static BaseImage cardpanel_prem_timer_fill_mid = new BaseImage(AssetPaths.AssetIconsCardPanel, "timerbar_fill_mid.png");
    public static BaseImage cardpanel_prem_timer_fill_right = new BaseImage(AssetPaths.AssetIconsCardPanel, "timerbar_fill_right.png");
    public static BaseImage cardpanel_premium_token = new BaseImage(AssetPaths.AssetIconsCardPanel, "premium_disk_144_134_normal.png");
    public static BaseImage cardpanel_premium_token_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "premium_disk_144_134_over.png");
    public static BaseImage cardpanel_pack_open_circle = new BaseImage(AssetPaths.AssetIconsCardPanel, "card_pack_open_circle.png");
    public static BaseImage card_screen_button_blank = new BaseImage(AssetPaths.AssetIconsCardPanel, "card_screen_button_blank.png");
    public static BaseImage card_screen_button_blank_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "card_screen_button_blank_over.png");
    public static BaseImage sort_back = new BaseImage(AssetPaths.AssetIconsCardPanel, "sort_back.png");
    public static BaseImage sort_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "sort_normal.png");
    public static BaseImage sort_in = new BaseImage(AssetPaths.AssetIconsCardPanel, "sort_in.png");
    public static BaseImage sort_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "sort_over.png");
    public static BaseImage sort_selected = new BaseImage(AssetPaths.AssetIconsCardPanel, "sort_selected.png");
    public static BaseImage cardpanel_symbol_apple = new BaseImage(AssetPaths.AssetIconsCardPanel, "symbol_apple.png");
    public static BaseImage cardpanel_symbol_crown = new BaseImage(AssetPaths.AssetIconsCardPanel, "symbol_crown.png");
    public static BaseImage cardpanel_symbol_hawk = new BaseImage(AssetPaths.AssetIconsCardPanel, "symbol_hawk.png");
    public static BaseImage cardpanel_symbol_jester = new BaseImage(AssetPaths.AssetIconsCardPanel, "symbol_jester.png");
    public static BaseImage cardpanel_symbol_shield = new BaseImage(AssetPaths.AssetIconsCardPanel, "symbol_shield.png");
    public static BaseImage cardpanel_symbol_tower = new BaseImage(AssetPaths.AssetIconsCardPanel, "symbol_tower.png");
    public static BaseImage cardpanel_symbol_wolf = new BaseImage(AssetPaths.AssetIconsCardPanel, "symbol_wolf.png");
    public static BaseImage cardpanel_button_blue_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "button_blue_141wide_normal.png");
    public static BaseImage cardpanel_button_blue_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "button_blue_141wide_over.png");
    public static BaseImage cardpanel_button_blue_pressed = new BaseImage(AssetPaths.AssetIconsCardPanel, "button_blue_141wide_pushed.png");
    public static BaseImage cardpanel_button_close_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "button_close_normal.png");
    public static BaseImage cardpanel_button_close_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "button_close_over.png");
    public static BaseImage cardpanel_button_close_pressed = new BaseImage(AssetPaths.AssetIconsCardPanel, "button_close_pressed.png");
    public static BaseImage cardpanel_panel_back_bar_divider = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_back_bar_divider.png");
    public static BaseImage cardpanel_panel_back_bottom_left = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_back_bottom_left.png");
    public static BaseImage cardpanel_panel_back_bottom_mid = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_back_bottom_mid.png");
    public static BaseImage cardpanel_panel_back_bottom_right = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_back_bottom_right.png");
    public static BaseImage cardpanel_panel_back_mid_left = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_back_mid_left.png");
    public static BaseImage cardpanel_panel_back_mid_mid = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_back_mid_mid.png");
    public static BaseImage cardpanel_panel_back_mid_right = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_back_mid_right.png");
    public static BaseImage cardpanel_panel_back_top_left = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_back_top_left.png");
    public static BaseImage cardpanel_panel_back_top_mid = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_back_top_mid.png");
    public static BaseImage cardpanel_panel_back_top_right = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_back_top_right.png");
    public static BaseImage cardpanel_panel_black_bottom_left = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_bottom_left.png");
    public static BaseImage cardpanel_panel_black_bottom_mid = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_bottom_mid.png");
    public static BaseImage cardpanel_panel_black_bottom_right = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_bottom_right.png");
    public static BaseImage cardpanel_panel_black_mid_left = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_mid_left.png");
    public static BaseImage cardpanel_panel_black_mid_mid = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_mid_mid.png");
    public static BaseImage cardpanel_panel_black_mid_right = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_mid_right.png");
    public static BaseImage cardpanel_panel_black_top_left = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_top_left.png");
    public static BaseImage cardpanel_panel_black_top_mid = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_top_mid.png");
    public static BaseImage cardpanel_panel_black_top_right = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_top_right.png");
    public static BaseImage cardpanel_panel_grey_bottom_mid = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_bottom_mid.png");
    public static BaseImage cardpanel_panel_grey_bottom_right = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_bottom_right.png");
    public static BaseImage cardpanel_panel_grey_mid_left = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_mid_left.png");
    public static BaseImage cardpanel_panel_grey_mid_mid = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_mid_mid.png");
    public static BaseImage cardpanel_panel_grey_mid_right = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_mid_right.png");
    public static BaseImage cardpanel_panel_grey_top_left = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_top_left.png");
    public static BaseImage cardpanel_panel_grey_top_mid = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_top_mid.png");
    public static BaseImage cardpanel_panel_grey_top_right = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_black_top_right.png");
    public static BaseImage cardpanel_grey_9slice_right_middle = new BaseImage(AssetPaths.AssetIconsCardPanel, "grey_9slice_right_middle.png");
    public static BaseImage cardpanel_grey_9slice_right_bottom = new BaseImage(AssetPaths.AssetIconsCardPanel, "grey_9slice_right_bottom.png");
    public static BaseImage cardpanel_grey_9slice_middle_top = new BaseImage(AssetPaths.AssetIconsCardPanel, "grey_9slice_middle_top.png");
    public static BaseImage cardpanel_grey_9slice_middle_middle = new BaseImage(AssetPaths.AssetIconsCardPanel, "grey_9slice_middle_middle.png");
    public static BaseImage cardpanel_grey_9slice_middle_bottom = new BaseImage(AssetPaths.AssetIconsCardPanel, "grey_9slice_middle_bottom.png");
    public static BaseImage cardpanel_grey_9slice_left_top = new BaseImage(AssetPaths.AssetIconsCardPanel, "grey_9slice_left_top.png");
    public static BaseImage cardpanel_grey_9slice_left_middle = new BaseImage(AssetPaths.AssetIconsCardPanel, "grey_9slice_left_middle.png");
    public static BaseImage cardpanel_grey_9slice_left_bottom = new BaseImage(AssetPaths.AssetIconsCardPanel, "grey_9slice_left_bottom.png");
    public static BaseImage cardpanel_grey_9slice_right_top = new BaseImage(AssetPaths.AssetIconsCardPanel, "grey_9slice_right_top.png");
    public static BaseImage cardpanel_grey_9slice_gradation_bottom = new BaseImage(AssetPaths.AssetIconsCardPanel, "grey_9slice_gradation_bottom right.png");
    public static BaseImage cardpanel_grey_9slice_gradation_top_left = new BaseImage(AssetPaths.AssetIconsCardPanel, "grey_9slice_gradation_top_left.png");
    public static BaseImage button_cards_all_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "button_cards_all_normal.png");
    public static BaseImage button_cards_all_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "button_cards_all_over.png");
    public static BaseImage button_cards_in_play_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "button_cards_in_play_normal.png");
    public static BaseImage button_cards_in_play_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "button_cards_in_play_over.png");
    public static BaseImage cardpanel_panel_gradient_bottom_right = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_gradient_bottom_right.png");
    public static BaseImage cardpanel_panel_gradient_top_left = new BaseImage(AssetPaths.AssetIconsCardPanel, "panel_gradient_top_left.png");
    public static BaseImage cardpanel_payment_button_crowns_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "payment_button_crowns_normal.png");
    public static BaseImage cardpanel_payment_button_crowns_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "payment_button_crowns_over.png");
    public static BaseImage cardpanel_payment_button_greywhite_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "payment_button_grey-white_normal");
    public static BaseImage cardpanel_payment_button_greywhite_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "payment_button_grey-white_over");
    public static BaseImage cardpanel_RH_button_back_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "RH_button-back_normal");
    public static BaseImage cardpanel_RH_button_back_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "RH_button-back_over");
    public static BaseImage cardpanel_scroll_thumb_botom = new BaseImage(AssetPaths.AssetIconsCardPanel, "scroll_thumb_botom.png");
    public static BaseImage cardpanel_scroll_thumb_mid = new BaseImage(AssetPaths.AssetIconsCardPanel, "scroll_thumb_mid.png");
    public static BaseImage cardpanel_scroll_thumb_top = new BaseImage(AssetPaths.AssetIconsCardPanel, "scroll_thumb_top.png");
    public static BaseImage SuperFan = new BaseImage(AssetPaths.AssetIconsCardPanel, "card_fan_super_random.png");
    public static BaseImage UltimateFan = new BaseImage(AssetPaths.AssetIconsCardPanel, "card_fan_ultimate_random.png");
    public static BaseImage card_type_buttons_recent_in = new BaseImage(AssetPaths.AssetIconsCardPanel, "card_type_buttons_recent_in.png");
    public static BaseImage card_type_buttons_recent_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "card_type_buttons_recent_normal.png");
    public static BaseImage card_type_buttons_recent_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "card_type_buttons_recent_over.png");
    public static BaseImage cardpanel_RH_button_v2_buycrowns_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "RH_button-v2_buycrowns_normal_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage cardpanel_RH_button_v2_buycrowns_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "RH_button-v2_buycrowns_over_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage cardpanel_RH_button_v2_choose_cards_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "RH_button-v2_choose_cards_normal_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage cardpanel_RH_button_v2_choose_cards_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "RH_button-v2_choose_cards_over_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage cardpanel_RH_button_v2_getcards_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "RH_button-v2_getcards_normal_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage cardpanel_RH_button_v2_getcards_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "RH_button-v2_getcards_over_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage cardpanel_RH_button_v2_getpremium_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "RH_button-v2_getpremium_normal_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage cardpanel_RH_button_v2_getpremium_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "RH_button-v2_getpremium_over_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage cardpanel_RH_button_v2_friend_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "RH_buttonX5_invite_normal_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage cardpanel_RH_button_v2_friend_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "RH_buttonX5_invite_over_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage cardpanel_RH_button_v2_offers_normal = new BaseImage(AssetPaths.AssetIconsCardPanel, "RH_buttonX5_offers_normal_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage cardpanel_RH_button_v2_offers_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "RH_buttonX5_offers_over_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage premiumAdvert30 = new BaseImage(AssetPaths.AssetIconsCardPanel, "ad_30premfor100crown_halfsize_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage premiumAdvert7 = new BaseImage(AssetPaths.AssetIconsCardPanel, "ad_1premfor30crown_half_size_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage premiumAdvert30_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "ad_30premfor100crown_halfsize_over_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage premiumAdvert7_over = new BaseImage(AssetPaths.AssetIconsCardPanel, "ad_1premfor30crown_half_size_over_%LANG%.png", BaseImage.loadType.LOCALIZED);
    public static BaseImage cardpanel_premium_ad = new BaseImage(AssetPaths.AssetIconsCardPanel, "ad_1premfor30crown_%LANG%.jpg", BaseImage.loadType.LOCALIZED);
    public static BaseImage[] cardTypeButtons = BaseImage.createFromUV(AssetPaths.AssetIconsCardPanel, "card_type_buttons_array", 114);
    public static BaseImage[] premiumIcons = BaseImage.createFromUV(AssetPaths.AssetIconsCardPanel, "premium_icons", 9);
    public static BaseImage offer_bundle_1_normal = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_bundle_1_normal.png");
    public static BaseImage offer_bundle_1_over = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_bundle_1_over.png");
    public static BaseImage offer_bundle_2_normal = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_bundle_2_normal.png");
    public static BaseImage offer_bundle_2_over = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_bundle_2_over.png");
    public static BaseImage offer_bundle_3_normal = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_bundle_3_normal.png");
    public static BaseImage offer_bundle_3_over = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_bundle_3_over.png");
    public static BaseImage offer_special_normal = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_special_normal.png");
    public static BaseImage offer_special_over = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_special_over.png");
    public static BaseImage offer_sale_normal = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_sale_normal.png");
    public static BaseImage offer_sale_over = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_sale_over.png");
    public static BaseImage offer_gold = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_gold.png");
    public static BaseImage offer_honour = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_honour.png");
    public static BaseImage offer_faith = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_faith.png");
    public static BaseImage offer_shield = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_shield.png");
    public static BaseImage offer_wheel_spin = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_wheel-spin.png");
    public static BaseImage offer_wheel_spin1 = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_wheel-spin-1.png");
    public static BaseImage offer_wheel_spin2 = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_wheel-spin-2.png");
    public static BaseImage offer_wheel_spin3 = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_wheel-spin-3.png");
    public static BaseImage offer_wheel_spin4 = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_wheel-spin-4.png");
    public static BaseImage offer_wheel_spin5 = new BaseImage(AssetPaths.AssetIconsCardOffers, "offer_wheel-spin-5.png");
    public static BaseImage avatar_arms01 = new BaseImage(AssetPaths.AssetIconsAvatar, "arms01.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_arms02 = new BaseImage(AssetPaths.AssetIconsAvatar, "arms02.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_arms03 = new BaseImage(AssetPaths.AssetIconsAvatar, "arms03.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_arms04 = new BaseImage(AssetPaths.AssetIconsAvatar, "arms04.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_face01_male = new BaseImage(AssetPaths.AssetIconsAvatar, "face01_male.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_face02_male = new BaseImage(AssetPaths.AssetIconsAvatar, "face02_male.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_face03_female = new BaseImage(AssetPaths.AssetIconsAvatar, "face03_female.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_face04_female = new BaseImage(AssetPaths.AssetIconsAvatar, "face04_female.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_face06_male = new BaseImage(AssetPaths.AssetIconsAvatar, "face06_male.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_face07_male = new BaseImage(AssetPaths.AssetIconsAvatar, "face07_male.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_face05_female = new BaseImage(AssetPaths.AssetIconsAvatar, "face05_female.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_face06_female = new BaseImage(AssetPaths.AssetIconsAvatar, "face06_female.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_face08_male = new BaseImage(AssetPaths.AssetIconsAvatar, "face---man-1.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_face08_female = new BaseImage(AssetPaths.AssetIconsAvatar, "face---woman-1.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_face09_male = new BaseImage(AssetPaths.AssetIconsAvatar, "face---man-2.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_face09_female = new BaseImage(AssetPaths.AssetIconsAvatar, "face---woman-2.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_face10_male = new BaseImage(AssetPaths.AssetIconsAvatar, "face---man-3.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_face10_female = new BaseImage(AssetPaths.AssetIconsAvatar, "face---woman-3.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_feet01 = new BaseImage(AssetPaths.AssetIconsAvatar, "feet01.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_feet02 = new BaseImage(AssetPaths.AssetIconsAvatar, "feet02.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_feet03 = new BaseImage(AssetPaths.AssetIconsAvatar, "feet03.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_feet04 = new BaseImage(AssetPaths.AssetIconsAvatar, "feet04.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_feet05 = new BaseImage(AssetPaths.AssetIconsAvatar, "feet---bare-hairy-feet.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_feet06 = new BaseImage(AssetPaths.AssetIconsAvatar, "feet---high-leather-boots.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_floor01 = new BaseImage(AssetPaths.AssetIconsAvatar, "floor01.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_floor02 = new BaseImage(AssetPaths.AssetIconsAvatar, "floor02.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_floor03 = new BaseImage(AssetPaths.AssetIconsAvatar, "floor03.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_floor04 = new BaseImage(AssetPaths.AssetIconsAvatar, "floor04.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_floor05 = new BaseImage(AssetPaths.AssetIconsAvatar, "floor05.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_floor06 = new BaseImage(AssetPaths.AssetIconsAvatar, "floor---fire.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_floor07 = new BaseImage(AssetPaths.AssetIconsAvatar, "floor---puddle.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_floor08 = new BaseImage(AssetPaths.AssetIconsAvatar, "floor---snow.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_floor09 = new BaseImage(AssetPaths.AssetIconsAvatar, "floor---spikes.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_floor10 = new BaseImage(AssetPaths.AssetIconsAvatar, "floor--tiled.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_floor11 = new BaseImage(AssetPaths.AssetIconsAvatar, "floor---wooden-boards.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_hair01_helmhide = new BaseImage(AssetPaths.AssetIconsAvatar, "hair01_helmhide.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_hair02 = new BaseImage(AssetPaths.AssetIconsAvatar, "hair02.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_hair03 = new BaseImage(AssetPaths.AssetIconsAvatar, "hair03.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_hair04 = new BaseImage(AssetPaths.AssetIconsAvatar, "hair04.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_hair05 = new BaseImage(AssetPaths.AssetIconsAvatar, "hair---female-frizzy.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_hair06 = new BaseImage(AssetPaths.AssetIconsAvatar, "hair---male-balding.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_hands01 = new BaseImage(AssetPaths.AssetIconsAvatar, "hands01.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_hands02 = new BaseImage(AssetPaths.AssetIconsAvatar, "hands02.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_hands03 = new BaseImage(AssetPaths.AssetIconsAvatar, "hands03.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_hands04 = new BaseImage(AssetPaths.AssetIconsAvatar, "hands04.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_head01_hairoff = new BaseImage(AssetPaths.AssetIconsAvatar, "head01_hairoff.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_head02_hairoff = new BaseImage(AssetPaths.AssetIconsAvatar, "head02_hairoff.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_head03 = new BaseImage(AssetPaths.AssetIconsAvatar, "head03.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_head04 = new BaseImage(AssetPaths.AssetIconsAvatar, "head04.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_head05 = new BaseImage(AssetPaths.AssetIconsAvatar, "hat---skull.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_head06 = new BaseImage(AssetPaths.AssetIconsAvatar, "hat---viking-hollywood.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_head07 = new BaseImage(AssetPaths.AssetIconsAvatar, "head---helmet-basic.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_head08 = new BaseImage(AssetPaths.AssetIconsAvatar, "head---helmet-russian.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_head09 = new BaseImage(AssetPaths.AssetIconsAvatar, "helmet---arabic.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_head10 = new BaseImage(AssetPaths.AssetIconsAvatar, "hat---jester.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_head11 = new BaseImage(AssetPaths.AssetIconsAvatar, "hat---rabbit-ears.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_head12 = new BaseImage(AssetPaths.AssetIconsAvatar, "hat---santa.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_legs01_female = new BaseImage(AssetPaths.AssetIconsAvatar, "legs01_female.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_legs01_male = new BaseImage(AssetPaths.AssetIconsAvatar, "legs01_male.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_legs02 = new BaseImage(AssetPaths.AssetIconsAvatar, "legs02.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_legs03 = new BaseImage(AssetPaths.AssetIconsAvatar, "legs03.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_legs04 = new BaseImage(AssetPaths.AssetIconsAvatar, "legs04.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_legs05 = new BaseImage(AssetPaths.AssetIconsAvatar, "legs05_female.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_legs06 = new BaseImage(AssetPaths.AssetIconsAvatar, "legs---chain-mail-skirt.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_legs07 = new BaseImage(AssetPaths.AssetIconsAvatar, "Legs---fine-silk-skirt.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_shoulders01 = new BaseImage(AssetPaths.AssetIconsAvatar, "shoulders01.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_shoulders02 = new BaseImage(AssetPaths.AssetIconsAvatar, "shoulders02.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_shoulders03 = new BaseImage(AssetPaths.AssetIconsAvatar, "shoulders03.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_shoulders04_back = new BaseImage(AssetPaths.AssetIconsAvatar, "shoulders04_back.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_shoulders04_front = new BaseImage(AssetPaths.AssetIconsAvatar, "shoulders04_front.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_tabard01 = new BaseImage(AssetPaths.AssetIconsAvatar, "tabard01.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_tabard02 = new BaseImage(AssetPaths.AssetIconsAvatar, "tabard02.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_tabard03 = new BaseImage(AssetPaths.AssetIconsAvatar, "tabard03.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_tabard04 = new BaseImage(AssetPaths.AssetIconsAvatar, "tabard04.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_tabard05 = new BaseImage(AssetPaths.AssetIconsAvatar, "tabard---arabic-swordsman.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_tabard06 = new BaseImage(AssetPaths.AssetIconsAvatar, "tabard---quilted-armour.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_tabard07 = new BaseImage(AssetPaths.AssetIconsAvatar, "tabard---ripped.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_tabard08 = new BaseImage(AssetPaths.AssetIconsAvatar, "tabard---studded.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_torso01_female_default = new BaseImage(AssetPaths.AssetIconsAvatar, "torso01_female_default.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_torso01_male_default = new BaseImage(AssetPaths.AssetIconsAvatar, "torso01_male_default.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_torso02_female = new BaseImage(AssetPaths.AssetIconsAvatar, "torso02_female.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_torso02_male = new BaseImage(AssetPaths.AssetIconsAvatar, "torso02_male.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_torso03 = new BaseImage(AssetPaths.AssetIconsAvatar, "torso03.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_torso04 = new BaseImage(AssetPaths.AssetIconsAvatar, "torso04.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_weapon_belt = new BaseImage(AssetPaths.AssetIconsAvatar, "weapon_belt.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_weapon01 = new BaseImage(AssetPaths.AssetIconsAvatar, "weapon01.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_weapon02 = new BaseImage(AssetPaths.AssetIconsAvatar, "weapon02.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_weapon03 = new BaseImage(AssetPaths.AssetIconsAvatar, "weapon03.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_weapon04 = new BaseImage(AssetPaths.AssetIconsAvatar, "weapon04.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_weapon05 = new BaseImage(AssetPaths.AssetIconsAvatar, "weapon---flail.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_weapon06 = new BaseImage(AssetPaths.AssetIconsAvatar, "weapon---scimitar.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_body01_default = new BaseImage(AssetPaths.AssetIconsAvatar, "body01_default.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_rat_face = new BaseImage(AssetPaths.AssetIconsAvatar, "rat1_face.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_rat_helm = new BaseImage(AssetPaths.AssetIconsAvatar, "rat_helm.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_snake_face = new BaseImage(AssetPaths.AssetIconsAvatar, "snake_face.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_pig_face = new BaseImage(AssetPaths.AssetIconsAvatar, "pig_face.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_wolf_face = new BaseImage(AssetPaths.AssetIconsAvatar, "wolf_face.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_wolf_helm = new BaseImage(AssetPaths.AssetIconsAvatar, "wolf_helm.png", BaseImage.loadType.SHRUNK);
    public static BaseImage avatar_randomise = new BaseImage(AssetPaths.AssetIconsAvatar, "randomise_wood2.png");
    public static BaseImage avatar_parchment_base_layer = new BaseImage(AssetPaths.AssetIconsAvatar, "parchment_base_layer.png");
    public static BaseImage avatar_parchment_top_multiply = new BaseImage(AssetPaths.AssetIconsAvatar, "parchment_top_multiply.png");
    private int worldMapIconsTexID = -1;
    private int effectLayerTexID = -1;
    private int mapElementsTexID = -1;
    private int worldMapTilesTexID = -1;
    private int goods1TexID = -1;
    private int townBuildindsTexID = -1;
    private int goods2TexID = -1;
    private int body_stonemasonTexID = -1;
    private int stonemasonAnimTexID = -1;
    private int body_troubadourTexID = -1;
    private int body_theaterworkerTexID = -1;
    private int villageOverlaysAnimTexID = -1;
    private int woodcutterAnimTexID = -1;
    private int farmerAnimTexID = -1;
    private int farmer2AnimTexID = -1;
    private int farmer3AnimTexID = -1;
    private int bakerAnimTexID = -1;
    private int metalWorkerAnimTexID = -1;
    private int fletcherAnimTexID = -1;
    private int pitchworkerAnimTexID = -1;
    private int poleturnerAnimTexID = -1;
    private int cowAnimTexID = -1;
    private int blacksmithAnimTexID = -1;
    private int armourerAnimTexID = -1;
    private int sheepAnimTexID = -1;
    private int chickenBrownAnimTexID = -1;
    private int chickenWhiteAnimTexID = -1;
    private int pigAnimTexID = -1;
    private int traderAnimTexID = -1;
    private int bld_17x17_1TexID = -1;
    private int bld_7x7_1TexID = -1;
    private int bld_9x9_1TexID = -1;
    private int bld_11x11_1TexID = -1;
    private int bld_13x13_1TexID = -1;
    private int bld_13x13_2TexID = -1;
    private int bld_4x4_1TexID = -1;
    private int bld_5x5_1TexID = -1;
    private int body_carpenterTexID = -1;
    private int dockworkerAnimTexID = -1;
    private int ironMinerAnimTexID = -1;
    private int body_iron_mine_workTexID = -1;
    private int body_hunterTexID = -1;
    private int body_brewerTexID = -1;
    private int body_tailorTexID = -1;
    private int body_siegeworkerTexID = -1;
    private int body_pitchworkerTexID = -1;
    private int traderHorseAnimTexID = -1;
    private int bld_8x8_1TexID = -1;
    private int woodcutter_animsTexID = -1;
    private int bld_6x6_1TexID = -1;
    private int body_farmer_3TexID = -1;
    private int body_metalworkerTexID = -1;
    private int body_bakerTexID = -1;
    private int body_poleturnerTexID = -1;
    private int body_fletcherTexID = -1;
    private int body_blacksmithTexID = -1;
    private int body_armourerTexID = -1;
    private int bld_Various_01TexID = -1;
    private int body_jesterTexID = -1;
    private int anim_dancing_bearTexID = -1;
    private int anim_maypoleTexID = -1;
    private int anim_rackTexID = -1;
    private int anim_stakeTexID = -1;
    private int anim_gibbetTexID = -1;
    private int anim_stocksTexID = -1;
    private int archerAnimTexID = -1;
    private int archerRedAnimTexID = -1;
    private int archerGreenAnimTexID = -1;
    private int peasantAnimTexID = -1;
    private int peasantRedAnimTexID = -1;
    private int peasantGreenAnimTexID = -1;
    private int pikemanAnimTexID = -1;
    private int pikemanRedAnimTexID = -1;
    private int pikemanGreenAnimTexID = -1;
    private int swordsmanAnimTexID = -1;
    private int swordsmanRedAnimTexID = -1;
    private int swordsmanGreenAnimTexID = -1;
    private int knightAnimTexID = -1;
    private int knightTopAnimTexID = -1;
    private int archerCarryAnimTexID = -1;
    private int peasantCarryAnimTexID = -1;
    private int pikemanCarryAnimTexID = -1;
    private int swordsmanCarryAnimTexID = -1;
    private int captainAnimTexID = -1;
    private int captainAnimRedTexID = -1;
    private int catapultAnimTexID = -1;
    private int manOnFireTexID = -1;
    private int peasant2AnimTexID = -1;
    private int peasant2RedAnimTexID = -1;
    private int peasant2GreenAnimTexID = -1;
    private int wolfAnimTexID = -1;
    private int archer2AnimTexID = -1;
    private int archer2RedAnimTexID = -1;
    private int archer2GreenAnimTexID = -1;
    private int missileTexID = -1;
    private int missile2TexID = -1;
    private int animKillingPitsTexID = -1;
    private int fireTexID = -1;
    private int smoke1TexID = -1;
    private int oilPotAnimTexID = -1;
    private int hpsBarsTexID = -1;
    private int castleBackgroundTexID = -1;
    private int castleSpritesTexID = -1;
    private int ballistaTexID = -1;
    private int armyAnimsTexID = -1;
    private int tutorialIconNormalID = -1;
    private int tutorialIconOverID = -1;
    private int freeCardIconsID = -1;
    public bool worldMapLoaded;
    public static Image parishwall_button_vote_disabled = (Image) null;
    public static Image parishwall_button_vote_unchecked_normal = (Image) null;
    public static Image parishwall_button_vote_unchecked_over = (Image) null;
    public static Image tutorial_character_mockup = (Image) null;
    public static BaseImage banner_ad_friend = (BaseImage) null;
    public static BaseImage banner_ad_friend_quest = (BaseImage) null;
    public static int invite_ad_colour = 0;
    public static Image dummy = (Image) null;
    public static byte[] parchementOverlay = (byte[]) null;
    public int ImageSurroundTexID2 = -1;
    public int ImageSurroundTexID3 = -1;
    public int ImageSurroundShadowTexID = -1;
    public int WikiHelpIconNormal = -1;
    public int WikiHelpIconOver = -1;
    private SparseArray cardImagesBig = new SparseArray();
    public Image NoImageCard;
    private ResourceLoader cachedBigCardsLoader;

    public void changeView(string view) => AssetLoader.instance.recordViewChange(view);

    public static string getPanelDescFromID(int panelid)
    {
      switch (panelid)
      {
        case 1:
          return "village_banquet";
        case 3:
          return "village_trade";
        case 4:
          return "village_army";
        case 5:
          return "village_resources";
        case 8:
          return "village_vassal";
        case 18:
          return "village_scouting";
        case 19:
          return "rankup";
        case 21:
          return "reports";
        case 22:
          return "glory_race";
        case 23:
          return "combat";
        case 24:
          return "vassal_overview";
        case 26:
          return "quests";
        case 41:
          return "faction";
        case 51:
          return "all_houses";
        case 52:
          return "house_view";
        case 201:
          return "tab_worldmap";
        case 202:
          return "tab_villagemap";
        case 203:
          return "tab_capital";
        case 204:
          return "tab_research";
        case 205:
          return "tab_ranking";
        case 206:
          return "tab_quests";
        case 207:
          return "tab_attacks";
        case 208:
          return "tab_reports";
        case 209:
          return "tab_factions";
        case 1003:
          return "parish_trade";
        case 1004:
          return "parish_army";
        case 1005:
          return "parish_resources";
        case 1006:
          return "parish_voting";
        case 1007:
          return "parish_forum";
        case 1008:
          return "parish_wall";
        default:
          return "unknown_panel";
      }
    }

    public void recordTextureUse(string texturename, int width, int height)
    {
      AssetLoader.instance.recordTextureUse(texturename, width, height);
    }

    private GFXLibrary()
    {
    }

    public static GFXLibrary Instance => GFXLibrary.instance;

    public static BaseImage getLoginWorldFlag(string code)
    {
      return GFXLibrary.LoginWorldFlags.ContainsKey(code) && GFXLibrary.LoginWorldFlags[code] != null ? GFXLibrary.LoginWorldFlags[code] : GFXLibrary.LoginWorldFlags["en"];
    }

    public static BaseImage getLoginWorldMap(string code)
    {
      return GFXLibrary.LoginWorldMaps.ContainsKey(code) && GFXLibrary.LoginWorldMaps[code] != null ? GFXLibrary.LoginWorldMaps[code] : GFXLibrary.LoginWorldMaps["en"];
    }

    public static BaseImage getResearchIllustration(int researchType)
    {
      switch (researchType)
      {
        case 0:
          return GFXLibrary.research_ill_stone_quarrying;
        case 1:
          return GFXLibrary.research_ill_forestry;
        case 2:
          return GFXLibrary.research_ill_iron_mining;
        case 3:
          return GFXLibrary.research_ill_pitch_extraction;
        case 4:
          return GFXLibrary.research_ill_tools;
        case 5:
          return GFXLibrary.research_ill_salt_working;
        case 6:
          return GFXLibrary.research_ill_craftsmanship;
        case 7:
          return GFXLibrary.research_ill_tailoring;
        case 8:
          return GFXLibrary.research_ill_carpentry;
        case 9:
          return GFXLibrary.research_ill_metal_working;
        case 10:
          return GFXLibrary.research_ill_brewing;
        case 11:
          return GFXLibrary.research_ill_butchery;
        case 12:
          return GFXLibrary.research_ill_bakery;
        case 13:
          return GFXLibrary.research_ill_weapon_making;
        case 14:
          return GFXLibrary.research_ill_siege_mechanics;
        case 15:
          return GFXLibrary.research_ill_blacksmithing;
        case 16:
          return GFXLibrary.research_ill_pole_turning;
        case 17:
          return GFXLibrary.research_ill_armour_working;
        case 18:
          return GFXLibrary.research_ill_fletching;
        case 19:
          return GFXLibrary.research_ill_castellation;
        case 20:
          return GFXLibrary.research_ill_construction;
        case 21:
          return GFXLibrary.research_ill_defences;
        case 22:
          return GFXLibrary.research_ill_vaults;
        case 23:
          return GFXLibrary.research_ill_fortification;
        case 24:
          return GFXLibrary.research_ill_command;
        case 25:
          return GFXLibrary.research_ill_captains;
        case 26:
          return GFXLibrary.research_ill_catapult;
        case 27:
          return GFXLibrary.research_ill_sword;
        case 28:
          return GFXLibrary.research_ill_pike;
        case 29:
          return GFXLibrary.research_ill_longbow;
        case 30:
          return GFXLibrary.research_ill_conscription;
        case 31:
          return GFXLibrary.research_ill_espionage;
        case 32:
          return GFXLibrary.research_ill_mathematics;
        case 33:
          return GFXLibrary.research_ill_architecture;
        case 34:
          return GFXLibrary.research_ill_commerce;
        case 35:
          return GFXLibrary.research_ill_trade_agreements;
        case 36:
          return GFXLibrary.research_ill_land_trade;
        case 37:
          return GFXLibrary.research_ill_foraging;
        case 38:
          return GFXLibrary.research_ill_silk_trade;
        case 39:
          return GFXLibrary.research_ill_spice_trade;
        case 40:
          return GFXLibrary.research_ill_engineering;
        case 41:
          return GFXLibrary.research_ill_hall_capacity;
        case 42:
          return GFXLibrary.research_ill_housing_capacity;
        case 43:
          return GFXLibrary.research_ill_armoury_capacity;
        case 44:
          return GFXLibrary.research_ill_inn_capacity;
        case 45:
          return GFXLibrary.research_ill_granary_capacity;
        case 46:
          return GFXLibrary.research_ill_stockpile_capacity;
        case 47:
          return GFXLibrary.research_ill_literature;
        case 48:
          return GFXLibrary.research_ill_philosophy;
        case 49:
          return GFXLibrary.research_ill_theology;
        case 50:
          return GFXLibrary.research_ill_extreme_unction;
        case 51:
          return GFXLibrary.research_ill_confession;
        case 52:
          return GFXLibrary.research_ill_ordination;
        case 53:
          return GFXLibrary.research_ill_eucharist;
        case 54:
          return GFXLibrary.research_ill_confirmation;
        case 55:
          return GFXLibrary.research_ill_baptism;
        case 56:
          return GFXLibrary.research_ill_marriage;
        case 57:
          return GFXLibrary.research_ill_diplomacy;
        case 58:
          return GFXLibrary.research_ill_justice;
        case 59:
          return GFXLibrary.research_ill_arts;
        case 60:
          return GFXLibrary.research_ill_gardening;
        case 61:
          return GFXLibrary.research_ill_animal_husbandry;
        case 62:
          return GFXLibrary.research_ill_pig_breeding;
        case 63:
          return GFXLibrary.research_ill_hunting;
        case 64:
          return GFXLibrary.research_ill_dairy_farming;
        case 65:
          return GFXLibrary.research_ill_fishing;
        case 66:
          return GFXLibrary.research_ill_apple_farming;
        case 67:
          return GFXLibrary.research_ill_hops_farming;
        case 68:
          return GFXLibrary.research_ill_plough;
        case 69:
          return GFXLibrary.research_ill_wine_production;
        case 70:
          return GFXLibrary.research_ill_wheat_farming;
        case 71:
          return GFXLibrary.research_ill_vegetable_cropping;
        case 72:
          return GFXLibrary.research_ill_pilgrimage;
        case 73:
          return GFXLibrary.research_ill_tactics;
        case 74:
          return GFXLibrary.research_ill_leadership;
        case 75:
          return GFXLibrary.research_ill_scouts;
        case 76:
          return GFXLibrary.research_ill_horsemanship;
        case 77:
          return GFXLibrary.research_ill_surveillance;
        case 78:
          return GFXLibrary.research_ill_pillage;
        case 79:
          return GFXLibrary.research_ill_intelligence_gathering;
        case 80:
          return GFXLibrary.research_ill_counter_surveillance;
        case 81:
          return GFXLibrary.research_ill_bounties;
        case 82:
          return GFXLibrary.research_ill_logistics;
        case 83:
          return GFXLibrary.research_ill_civil_service;
        case 84:
          return GFXLibrary.research_ill_sally_forth;
        case 85:
          return GFXLibrary.research_ill_ransacking;
        case 86:
          return GFXLibrary.research_ill_forced_march;
        default:
          return (BaseImage) null;
      }
    }

    public int WorldMapIconsTexID
    {
      get
      {
        if (this.worldMapIconsTexID == -1)
          this.worldMapIconsTexID = this.gfx.loadSprites("assets\\world_map_icons.uv");
        return this.worldMapIconsTexID;
      }
    }

    public int EffectLayerTexID
    {
      get
      {
        if (this.effectLayerTexID == -1)
          this.effectLayerTexID = !Program.ShowSeasonalGraphics ? this.gfx.loadSprites("assets\\effectLayer.uv") : this.gfx.loadSprites("assets\\effectLayer_snow.uv");
        return this.effectLayerTexID;
      }
    }

    public int MapElementsTexID
    {
      get
      {
        if (this.mapElementsTexID == -1)
          this.mapElementsTexID = !Program.ShowSeasonalGraphics ? this.gfx.loadSprites("assets\\map_elements.uv") : this.gfx.loadSprites("assets\\map_elements_snow.uv");
        return this.mapElementsTexID;
      }
    }

    public int WorldMapTilesTexID
    {
      get
      {
        if (this.worldMapTilesTexID == -1)
          this.worldMapTilesTexID = !Program.ShowSeasonalGraphics ? this.gfx.loadSprites("assets\\world_tiles.uv") : this.gfx.loadSprites("assets\\world_tiles_snow.uv");
        return this.worldMapTilesTexID;
      }
    }

    public int Goods1TexID
    {
      get
      {
        if (this.goods1TexID == -1)
          this.goods1TexID = this.gfx.loadSprites("assets\\bld_goods.uv");
        return this.goods1TexID;
      }
    }

    public int TownBuildindsTexID
    {
      get
      {
        if (this.townBuildindsTexID == -1)
          this.townBuildindsTexID = this.gfx.loadSprites("assets\\town_buildings_01.uv");
        return this.townBuildindsTexID;
      }
    }

    public int Goods2TexID
    {
      get
      {
        if (this.goods2TexID == -1)
          this.goods2TexID = this.gfx.loadSprites("assets\\bld_goods_2.uv");
        return this.goods2TexID;
      }
    }

    public int Body_stonemasonTexID
    {
      get
      {
        if (this.body_stonemasonTexID == -1)
          this.body_stonemasonTexID = this.gfx.loadSprites("assets\\MRG_stone_working.uv");
        return this.body_stonemasonTexID;
      }
    }

    public int StonemasonAnimTexID
    {
      get
      {
        if (this.stonemasonAnimTexID == -1)
          this.stonemasonAnimTexID = this.Body_stonemasonTexID * 10000 + 1;
        return this.stonemasonAnimTexID;
      }
    }

    public int Body_troubadourTexID
    {
      get
      {
        if (this.body_troubadourTexID == -1)
          this.body_troubadourTexID = this.Body_stonemasonTexID * 10000 + 2;
        return this.body_troubadourTexID;
      }
    }

    public int Body_theaterworkerTexID
    {
      get
      {
        if (this.body_theaterworkerTexID == -1)
          this.body_theaterworkerTexID = this.Body_stonemasonTexID * 10000 + 3;
        return this.body_theaterworkerTexID;
      }
    }

    public int VillageOverlaysAnimTexID
    {
      get
      {
        if (this.villageOverlaysAnimTexID == -1)
          this.villageOverlaysAnimTexID = this.Body_stonemasonTexID * 10000 + 4;
        return this.villageOverlaysAnimTexID;
      }
    }

    public int WoodcutterAnimTexID
    {
      get
      {
        if (this.woodcutterAnimTexID == -1)
          this.woodcutterAnimTexID = this.gfx.loadSprites("assets\\woodcutter_anims.uv");
        return this.woodcutterAnimTexID;
      }
    }

    public int FarmerAnimTexID
    {
      get
      {
        if (this.farmerAnimTexID == -1)
          this.farmerAnimTexID = this.gfx.loadSprites("assets\\body_farmer.uv");
        return this.farmerAnimTexID;
      }
    }

    public int Farmer2AnimTexID
    {
      get
      {
        if (this.farmer2AnimTexID == -1)
          this.farmer2AnimTexID = this.gfx.loadSprites("assets\\body_farmer_2.uv");
        return this.farmer2AnimTexID;
      }
    }

    public int Farmer3AnimTexID
    {
      get
      {
        if (this.farmer3AnimTexID == -1)
          this.farmer3AnimTexID = this.gfx.loadSprites("assets\\body_farmer_3.uv");
        return this.farmer3AnimTexID;
      }
    }

    public int BakerAnimTexID
    {
      get
      {
        if (this.bakerAnimTexID == -1)
          this.bakerAnimTexID = this.gfx.loadSprites("assets\\body_baker.uv");
        return this.bakerAnimTexID;
      }
    }

    public int MetalWorkerAnimTexID
    {
      get
      {
        if (this.metalWorkerAnimTexID == -1)
          this.metalWorkerAnimTexID = this.gfx.loadSprites("assets\\body_metalworker.uv");
        return this.metalWorkerAnimTexID;
      }
    }

    public int FletcherAnimTexID
    {
      get
      {
        if (this.fletcherAnimTexID == -1)
          this.fletcherAnimTexID = this.gfx.loadSprites("assets\\body_fletcher.uv");
        return this.fletcherAnimTexID;
      }
    }

    public int PitchworkerAnimTexID
    {
      get
      {
        if (this.pitchworkerAnimTexID == -1)
          this.pitchworkerAnimTexID = this.gfx.loadSprites("assets\\body_pitchworker.uv");
        return this.pitchworkerAnimTexID;
      }
    }

    public int PoleturnerAnimTexID
    {
      get
      {
        if (this.poleturnerAnimTexID == -1)
          this.poleturnerAnimTexID = this.gfx.loadSprites("assets\\body_poleturner.uv");
        return this.poleturnerAnimTexID;
      }
    }

    public int CowAnimTexID
    {
      get
      {
        if (this.cowAnimTexID == -1)
          this.cowAnimTexID = this.gfx.loadSprites("assets\\body_cow.uv");
        return this.cowAnimTexID;
      }
    }

    public int BlacksmithAnimTexID
    {
      get
      {
        if (this.blacksmithAnimTexID == -1)
          this.blacksmithAnimTexID = this.gfx.loadSprites("assets\\body_blacksmith.uv");
        return this.blacksmithAnimTexID;
      }
    }

    public int ArmourerAnimTexID
    {
      get
      {
        if (this.armourerAnimTexID == -1)
          this.armourerAnimTexID = this.gfx.loadSprites("assets\\body_armourer.uv");
        return this.armourerAnimTexID;
      }
    }

    public int SheepAnimTexID
    {
      get
      {
        if (this.sheepAnimTexID == -1)
          this.sheepAnimTexID = this.gfx.loadSprites("assets\\MRG_animals1.uv");
        return this.sheepAnimTexID;
      }
    }

    public int ChickenBrownAnimTexID
    {
      get
      {
        if (this.chickenBrownAnimTexID == -1)
          this.chickenBrownAnimTexID = this.SheepAnimTexID * 10000 + 1;
        return this.chickenBrownAnimTexID;
      }
    }

    public int ChickenWhiteAnimTexID
    {
      get
      {
        if (this.chickenWhiteAnimTexID == -1)
          this.chickenWhiteAnimTexID = this.SheepAnimTexID * 10000 + 2;
        return this.chickenWhiteAnimTexID;
      }
    }

    public int PigAnimTexID
    {
      get
      {
        if (this.pigAnimTexID == -1)
          this.pigAnimTexID = this.SheepAnimTexID * 10000 + 3;
        return this.pigAnimTexID;
      }
    }

    public int TraderAnimTexID
    {
      get
      {
        if (this.traderAnimTexID == -1)
          this.traderAnimTexID = this.SheepAnimTexID * 10000 + 4;
        return this.traderAnimTexID;
      }
    }

    public int Bld_17x17_1TexID
    {
      get
      {
        if (this.bld_17x17_1TexID == -1)
          this.bld_17x17_1TexID = this.gfx.loadSprites("assets\\MRG_large_buildings.uv");
        return this.bld_17x17_1TexID;
      }
    }

    public int Bld_7x7_1TexID
    {
      get
      {
        if (this.bld_7x7_1TexID == -1)
          this.bld_7x7_1TexID = this.Bld_17x17_1TexID * 10000 + 1;
        return this.bld_7x7_1TexID;
      }
    }

    public int Bld_9x9_1TexID
    {
      get
      {
        if (this.bld_9x9_1TexID == -1)
          this.bld_9x9_1TexID = this.Bld_17x17_1TexID * 10000 + 2;
        return this.bld_9x9_1TexID;
      }
    }

    public int Bld_11x11_1TexID
    {
      get
      {
        if (this.bld_11x11_1TexID == -1)
          this.bld_11x11_1TexID = this.Bld_17x17_1TexID * 10000 + 3;
        return this.bld_11x11_1TexID;
      }
    }

    public int Bld_13x13_1TexID
    {
      get
      {
        if (this.bld_13x13_1TexID == -1)
          this.bld_13x13_1TexID = this.Bld_17x17_1TexID * 10000 + 4;
        return this.bld_13x13_1TexID;
      }
    }

    public int Bld_13x13_2TexID
    {
      get
      {
        if (this.bld_13x13_2TexID == -1)
          this.bld_13x13_2TexID = this.Bld_17x17_1TexID * 10000 + 5;
        return this.bld_13x13_2TexID;
      }
    }

    public int Bld_4x4_1TexID
    {
      get
      {
        if (this.bld_4x4_1TexID == -1)
          this.bld_4x4_1TexID = this.gfx.loadSprites("assets\\MRG_small_buildings.uv");
        return this.bld_4x4_1TexID;
      }
    }

    public int Bld_5x5_1TexID
    {
      get
      {
        if (this.bld_5x5_1TexID == -1)
          this.bld_5x5_1TexID = this.Bld_4x4_1TexID * 10000 + 1;
        return this.bld_5x5_1TexID;
      }
    }

    public int Body_carpenterTexID
    {
      get
      {
        if (this.body_carpenterTexID == -1)
          this.body_carpenterTexID = this.gfx.loadSprites("assets\\MRG_carp_dock.uv");
        return this.body_carpenterTexID;
      }
    }

    public int DockworkerAnimTexID
    {
      get
      {
        if (this.dockworkerAnimTexID == -1)
          this.dockworkerAnimTexID = this.Body_carpenterTexID * 10000 + 1;
        return this.dockworkerAnimTexID;
      }
    }

    public int IronMinerAnimTexID
    {
      get
      {
        if (this.ironMinerAnimTexID == -1)
          this.ironMinerAnimTexID = this.gfx.loadSprites("assets\\MRG_iron.uv");
        return this.ironMinerAnimTexID;
      }
    }

    public int Body_iron_mine_workTexID
    {
      get
      {
        if (this.body_iron_mine_workTexID == -1)
          this.body_iron_mine_workTexID = this.IronMinerAnimTexID * 10000 + 1;
        return this.body_iron_mine_workTexID;
      }
    }

    public int Body_hunterTexID
    {
      get
      {
        if (this.body_hunterTexID == -1)
          this.body_hunterTexID = this.gfx.loadSprites("assets\\MRG_hunt_brew.uv");
        return this.body_hunterTexID;
      }
    }

    public int Body_brewerTexID
    {
      get
      {
        if (this.body_brewerTexID == -1)
          this.body_brewerTexID = this.Body_hunterTexID * 10000 + 1;
        return this.body_brewerTexID;
      }
    }

    public int Body_tailorTexID
    {
      get
      {
        if (this.body_tailorTexID == -1)
          this.body_tailorTexID = this.gfx.loadSprites("assets\\MRG_tailor_siege.uv");
        return this.body_tailorTexID;
      }
    }

    public int Body_siegeworkerTexID
    {
      get
      {
        if (this.body_siegeworkerTexID == -1)
          this.body_siegeworkerTexID = this.Body_tailorTexID * 10000 + 1;
        return this.body_siegeworkerTexID;
      }
    }

    public int Body_pitchworkerTexID
    {
      get
      {
        if (this.body_pitchworkerTexID == -1)
          this.body_pitchworkerTexID = this.gfx.loadSprites("assets\\MRG_pitch_horse.uv");
        return this.body_pitchworkerTexID;
      }
    }

    public int TraderHorseAnimTexID
    {
      get
      {
        if (this.traderHorseAnimTexID == -1)
          this.traderHorseAnimTexID = this.Body_pitchworkerTexID * 10000 + 1;
        return this.traderHorseAnimTexID;
      }
    }

    public int Bld_8x8_1TexID
    {
      get
      {
        if (this.bld_8x8_1TexID == -1)
          this.bld_8x8_1TexID = this.gfx.loadSprites("assets\\bld_8x8_1.uv");
        return this.bld_8x8_1TexID;
      }
    }

    public int Woodcutter_animsTexID
    {
      get
      {
        if (this.woodcutter_animsTexID == -1)
          this.woodcutter_animsTexID = this.gfx.loadSprites("assets\\woodcutter_anims.uv");
        return this.woodcutter_animsTexID;
      }
    }

    public int Bld_6x6_1TexID
    {
      get
      {
        if (this.bld_6x6_1TexID == -1)
          this.bld_6x6_1TexID = this.gfx.loadSprites("assets\\bld_6x6_1.uv");
        return this.bld_6x6_1TexID;
      }
    }

    public int Body_farmer_3TexID
    {
      get
      {
        if (this.body_farmer_3TexID == -1)
          this.body_farmer_3TexID = this.gfx.loadSprites("assets\\body_farmer_3.uv");
        return this.body_farmer_3TexID;
      }
    }

    public int Body_metalworkerTexID
    {
      get
      {
        if (this.body_metalworkerTexID == -1)
          this.body_metalworkerTexID = this.gfx.loadSprites("assets\\body_metalworker.uv");
        return this.body_metalworkerTexID;
      }
    }

    public int Body_bakerTexID
    {
      get
      {
        if (this.body_bakerTexID == -1)
          this.body_bakerTexID = this.gfx.loadSprites("assets\\body_baker.uv");
        return this.body_bakerTexID;
      }
    }

    public int Body_poleturnerTexID
    {
      get
      {
        if (this.body_poleturnerTexID == -1)
          this.body_poleturnerTexID = this.gfx.loadSprites("assets\\body_poleturner.uv");
        return this.body_poleturnerTexID;
      }
    }

    public int Body_fletcherTexID
    {
      get
      {
        if (this.body_fletcherTexID == -1)
          this.body_fletcherTexID = this.gfx.loadSprites("assets\\body_fletcher.uv");
        return this.body_fletcherTexID;
      }
    }

    public int Body_blacksmithTexID
    {
      get
      {
        if (this.body_blacksmithTexID == -1)
          this.body_blacksmithTexID = this.gfx.loadSprites("assets\\body_blacksmith.uv");
        return this.body_blacksmithTexID;
      }
    }

    public int Body_armourerTexID
    {
      get
      {
        if (this.body_armourerTexID == -1)
          this.body_armourerTexID = this.gfx.loadSprites("assets\\body_armourer.uv");
        return this.body_armourerTexID;
      }
    }

    public int Bld_Various_01TexID
    {
      get
      {
        if (this.bld_Various_01TexID == -1)
          this.bld_Various_01TexID = this.gfx.loadSprites("assets\\bld_Various_01.uv");
        return this.bld_Various_01TexID;
      }
    }

    public int Body_jesterTexID
    {
      get
      {
        if (this.body_jesterTexID == -1)
          this.body_jesterTexID = this.gfx.loadSprites("assets\\body_jester.uv");
        return this.body_jesterTexID;
      }
    }

    public int Anim_dancing_bearTexID
    {
      get
      {
        if (this.anim_dancing_bearTexID == -1)
          this.anim_dancing_bearTexID = this.gfx.loadSprites("assets\\MRG_fun_tort.uv");
        return this.anim_dancing_bearTexID;
      }
    }

    public int Anim_maypoleTexID
    {
      get
      {
        if (this.anim_maypoleTexID == -1)
          this.anim_maypoleTexID = this.Anim_dancing_bearTexID * 10000 + 1;
        return this.anim_maypoleTexID;
      }
    }

    public int Anim_rackTexID
    {
      get
      {
        if (this.anim_rackTexID == -1)
          this.anim_rackTexID = this.Anim_dancing_bearTexID * 10000 + 2;
        return this.anim_rackTexID;
      }
    }

    public int Anim_stakeTexID
    {
      get
      {
        if (this.anim_stakeTexID == -1)
          this.anim_stakeTexID = this.Anim_dancing_bearTexID * 10000 + 3;
        return this.anim_stakeTexID;
      }
    }

    public int Anim_gibbetTexID
    {
      get
      {
        if (this.anim_gibbetTexID == -1)
          this.anim_gibbetTexID = this.Anim_dancing_bearTexID * 10000 + 4;
        return this.anim_gibbetTexID;
      }
    }

    public int Anim_stocksTexID
    {
      get
      {
        if (this.anim_stocksTexID == -1)
          this.anim_stocksTexID = this.Anim_dancing_bearTexID * 10000 + 5;
        return this.anim_stocksTexID;
      }
    }

    public int ArcherAnimTexID
    {
      get
      {
        if (this.archerAnimTexID == -1)
          this.archerAnimTexID = this.gfx.loadSprites("assets\\MRG_archer1.uv");
        return this.archerAnimTexID;
      }
    }

    public int ArcherRedAnimTexID
    {
      get
      {
        if (this.archerRedAnimTexID == -1)
          this.archerRedAnimTexID = this.ArcherAnimTexID * 10000 + 1;
        return this.archerRedAnimTexID;
      }
    }

    public int ArcherGreenAnimTexID
    {
      get
      {
        if (this.archerGreenAnimTexID == -1)
          this.archerGreenAnimTexID = this.ArcherAnimTexID * 10000 + 2;
        return this.archerGreenAnimTexID;
      }
    }

    public int PeasantAnimTexID
    {
      get
      {
        if (this.peasantAnimTexID == -1)
          this.peasantAnimTexID = this.gfx.loadSprites("assets\\MRG_peasant1.uv");
        return this.peasantAnimTexID;
      }
    }

    public int PeasantRedAnimTexID
    {
      get
      {
        if (this.peasantRedAnimTexID == -1)
          this.peasantRedAnimTexID = this.PeasantAnimTexID * 10000 + 1;
        return this.peasantRedAnimTexID;
      }
    }

    public int PeasantGreenAnimTexID
    {
      get
      {
        if (this.peasantGreenAnimTexID == -1)
          this.peasantGreenAnimTexID = this.PeasantAnimTexID * 10000 + 2;
        return this.peasantGreenAnimTexID;
      }
    }

    public int PikemanAnimTexID
    {
      get
      {
        if (this.pikemanAnimTexID == -1)
          this.pikemanAnimTexID = this.gfx.loadSprites("assets\\MRG_pikeman1.uv");
        return this.pikemanAnimTexID;
      }
    }

    public int PikemanRedAnimTexID
    {
      get
      {
        if (this.pikemanRedAnimTexID == -1)
          this.pikemanRedAnimTexID = this.PikemanAnimTexID * 10000 + 1;
        return this.pikemanRedAnimTexID;
      }
    }

    public int PikemanGreenAnimTexID
    {
      get
      {
        if (this.pikemanGreenAnimTexID == -1)
          this.pikemanGreenAnimTexID = this.PikemanAnimTexID * 10000 + 2;
        return this.pikemanGreenAnimTexID;
      }
    }

    public int SwordsmanAnimTexID
    {
      get
      {
        if (this.swordsmanAnimTexID == -1)
          this.swordsmanAnimTexID = this.gfx.loadSprites("assets\\MRG_swordsman1.uv");
        return this.swordsmanAnimTexID;
      }
    }

    public int SwordsmanRedAnimTexID
    {
      get
      {
        if (this.swordsmanRedAnimTexID == -1)
          this.swordsmanRedAnimTexID = this.SwordsmanAnimTexID * 10000 + 1;
        return this.swordsmanRedAnimTexID;
      }
    }

    public int SwordsmanGreenAnimTexID
    {
      get
      {
        if (this.swordsmanGreenAnimTexID == -1)
          this.swordsmanGreenAnimTexID = this.SwordsmanAnimTexID * 10000 + 2;
        return this.swordsmanGreenAnimTexID;
      }
    }

    public int KnightAnimTexID
    {
      get
      {
        if (this.knightAnimTexID == -1)
          this.knightAnimTexID = this.gfx.loadSprites("assets\\MRG_knight.uv");
        return this.knightAnimTexID;
      }
    }

    public int KnightTopAnimTexID
    {
      get
      {
        if (this.knightTopAnimTexID == -1)
          this.knightTopAnimTexID = this.KnightAnimTexID * 10000 + 1;
        return this.knightTopAnimTexID;
      }
    }

    public int ArcherCarryAnimTexID
    {
      get
      {
        if (this.archerCarryAnimTexID == -1)
          this.archerCarryAnimTexID = this.gfx.loadSprites("assets\\MRG_troops_carrying.uv");
        return this.archerCarryAnimTexID;
      }
    }

    public int PeasantCarryAnimTexID
    {
      get
      {
        if (this.peasantCarryAnimTexID == -1)
          this.peasantCarryAnimTexID = this.ArcherCarryAnimTexID * 10000 + 1;
        return this.peasantCarryAnimTexID;
      }
    }

    public int PikemanCarryAnimTexID
    {
      get
      {
        if (this.pikemanCarryAnimTexID == -1)
          this.pikemanCarryAnimTexID = this.ArcherCarryAnimTexID * 10000 + 2;
        return this.pikemanCarryAnimTexID;
      }
    }

    public int SwordsmanCarryAnimTexID
    {
      get
      {
        if (this.swordsmanCarryAnimTexID == -1)
          this.swordsmanCarryAnimTexID = this.ArcherCarryAnimTexID * 10000 + 3;
        return this.swordsmanCarryAnimTexID;
      }
    }

    public int CaptainAnimTexID
    {
      get
      {
        if (this.captainAnimTexID == -1)
          this.captainAnimTexID = this.gfx.loadSprites("assets\\MRG_lord.uv");
        return this.captainAnimTexID;
      }
    }

    public int CaptainAnimRedTexID
    {
      get
      {
        if (this.captainAnimRedTexID == -1)
          this.captainAnimRedTexID = this.CaptainAnimTexID * 10000 + 1;
        return this.captainAnimRedTexID;
      }
    }

    public int CatapultAnimTexID
    {
      get
      {
        if (this.catapultAnimTexID == -1)
          this.catapultAnimTexID = this.gfx.loadSprites("assets\\MRG_castle_misc.uv");
        return this.catapultAnimTexID;
      }
    }

    public int ManOnFireTexID
    {
      get
      {
        if (this.manOnFireTexID == -1)
          this.manOnFireTexID = this.CatapultAnimTexID * 10000 + 1;
        return this.manOnFireTexID;
      }
    }

    public int Peasant2AnimTexID
    {
      get
      {
        if (this.peasant2AnimTexID == -1)
          this.peasant2AnimTexID = this.CatapultAnimTexID * 10000 + 2;
        return this.peasant2AnimTexID;
      }
    }

    public int Peasant2RedAnimTexID
    {
      get
      {
        if (this.peasant2RedAnimTexID == -1)
          this.peasant2RedAnimTexID = this.CatapultAnimTexID * 10000 + 3;
        return this.peasant2RedAnimTexID;
      }
    }

    public int Peasant2GreenAnimTexID
    {
      get
      {
        if (this.peasant2GreenAnimTexID == -1)
          this.peasant2GreenAnimTexID = this.CatapultAnimTexID * 10000 + 4;
        return this.peasant2GreenAnimTexID;
      }
    }

    public int WolfAnimTexID
    {
      get
      {
        if (this.wolfAnimTexID == -1)
          this.wolfAnimTexID = this.gfx.loadSprites("assets\\body_wolf.uv");
        return this.wolfAnimTexID;
      }
    }

    public int Archer2AnimTexID
    {
      get
      {
        if (this.archer2AnimTexID == -1)
          this.archer2AnimTexID = this.gfx.loadSprites("assets\\body_archer2.uv");
        return this.archer2AnimTexID;
      }
    }

    public int Archer2RedAnimTexID
    {
      get
      {
        if (this.archer2RedAnimTexID == -1)
          this.archer2RedAnimTexID = this.gfx.loadSprites("assets\\body_archer2_red.uv");
        return this.archer2RedAnimTexID;
      }
    }

    public int Archer2GreenAnimTexID
    {
      get
      {
        if (this.archer2GreenAnimTexID == -1)
          this.archer2GreenAnimTexID = this.gfx.loadSprites("assets\\body_archer2_green.uv");
        return this.archer2GreenAnimTexID;
      }
    }

    public int MissileTexID
    {
      get
      {
        if (this.missileTexID == -1)
          this.missileTexID = this.gfx.loadSprites("assets\\body_missile.uv");
        return this.missileTexID;
      }
    }

    public int Missile2TexID
    {
      get
      {
        if (this.missile2TexID == -1)
          this.missile2TexID = this.gfx.loadSprites("assets\\body_missile_2.uv");
        return this.missile2TexID;
      }
    }

    public int AnimKillingPitsTexID
    {
      get
      {
        if (this.animKillingPitsTexID == -1)
          this.animKillingPitsTexID = this.gfx.loadSprites("assets\\anim_killing_pits.uv");
        return this.animKillingPitsTexID;
      }
    }

    public int FireTexID
    {
      get
      {
        if (this.fireTexID == -1)
          this.fireTexID = this.gfx.loadSprites("assets\\body_fire2.uv");
        return this.fireTexID;
      }
    }

    public int Smoke1TexID
    {
      get
      {
        if (this.smoke1TexID == -1)
          this.smoke1TexID = this.gfx.loadSprites("assets\\anim_smoke_light.uv");
        return this.smoke1TexID;
      }
    }

    public int OilPotAnimTexID
    {
      get
      {
        if (this.oilPotAnimTexID == -1)
          this.oilPotAnimTexID = this.gfx.loadSprites("assets\\body_oil_pot.uv");
        return this.oilPotAnimTexID;
      }
    }

    public int HpsBarsTexID
    {
      get
      {
        if (this.hpsBarsTexID == -1)
          this.hpsBarsTexID = this.gfx.loadSprites("assets\\misc_bars.uv");
        return this.hpsBarsTexID;
      }
    }

    public int CastleBackgroundTexID
    {
      get
      {
        if (this.castleBackgroundTexID == -1)
          this.castleBackgroundTexID = this.gfx.loadSprites("assets\\MRG_castle.uv");
        return this.castleBackgroundTexID;
      }
    }

    public int CastleSpritesTexID
    {
      get
      {
        if (this.castleSpritesTexID == -1)
          this.castleSpritesTexID = this.CastleBackgroundTexID * 10000 + 1;
        return this.castleSpritesTexID;
      }
    }

    public int BallistaTexID
    {
      get
      {
        if (this.ballistaTexID == -1)
          this.ballistaTexID = this.gfx.loadSprites("assets\\body_ballista.uv");
        return this.ballistaTexID;
      }
    }

    public int ArmyAnimsTexID
    {
      get
      {
        if (this.armyAnimsTexID == -1)
          this.armyAnimsTexID = this.gfx.loadSprites("assets\\army_anim_bits.uv");
        return this.armyAnimsTexID;
      }
    }

    public int TutorialIconNormalID
    {
      get
      {
        if (this.tutorialIconNormalID == -1)
          this.tutorialIconNormalID = this.gfx.loadSprites("assets\\tutorial_button_open_normal.uv");
        return this.tutorialIconNormalID;
      }
    }

    public int TutorialIconOverID
    {
      get
      {
        if (this.tutorialIconOverID == -1)
          this.tutorialIconOverID = this.gfx.loadSprites("assets\\tutorial_button_open_over.uv");
        return this.tutorialIconOverID;
      }
    }

    public int FreeCardIconsID
    {
      get
      {
        if (this.freeCardIconsID == -1)
          this.freeCardIconsID = this.gfx.loadSprites("assets\\free_card_bits.uv");
        return this.freeCardIconsID;
      }
    }

    public void loadGFX(GraphicsMgr mgr)
    {
      this.gfx = mgr;
      if (Program.ShowSeasonalFXOption)
        SnowSystem.getInstance().loadSnowTexture(this.gfx.D3DDevice, Application.StartupPath + "\\assets\\snowball.png");
      int mapElementsTexId = this.MapElementsTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int effectLayerTexId = this.EffectLayerTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int worldMapTilesTexId = this.WorldMapTilesTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int worldMapIconsTexId = this.WorldMapIconsTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      this.worldMapLoaded = true;
      this.ImageSurroundTexID2 = this.gfx.loadTextureFromBitmap((Bitmap) (Image) GFXLibrary.int_banquette_background_tile_orig);
      this.ImageSurroundTexID3 = this.gfx.loadTextureFromBitmap((Bitmap) (Image) GFXLibrary.int_banquette_background_tile_tan);
      Bitmap newBitmap1 = new Bitmap(64, 64);
      newBitmap1.MakeTransparent();
      Graphics graphics1 = Graphics.FromImage((Image) newBitmap1);
      graphics1.DrawImage((Image) GFXLibrary.int_button_Q_normal, new Point(0, 0));
      graphics1.Dispose();
      this.WikiHelpIconNormal = this.gfx.loadTextureFromBitmap(newBitmap1);
      newBitmap1.Dispose();
      Bitmap newBitmap2 = new Bitmap(64, 64);
      newBitmap2.MakeTransparent();
      Graphics graphics2 = Graphics.FromImage((Image) newBitmap2);
      graphics2.DrawImage((Image) GFXLibrary.int_button_Q_over, new Point(0, 0));
      graphics2.Dispose();
      this.WikiHelpIconOver = this.gfx.loadTextureFromBitmap(newBitmap2);
      newBitmap2.Dispose();
      Bitmap newBitmap3 = new Bitmap(8, 8);
      for (int x = 0; x < newBitmap3.Height; ++x)
      {
        for (int y = 0; y < newBitmap3.Width; ++y)
          newBitmap3.SetPixel(x, y, Color.FromArgb(64, ARGBColors.Black));
      }
      this.ImageSurroundShadowTexID = this.gfx.loadTextureFromBitmap(newBitmap3);
      int woodcutterAnimTexId = this.WoodcutterAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int armourerAnimTexId = this.ArmourerAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bakerAnimTexId = this.BakerAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int stonemasonAnimTexId = this.StonemasonAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int ironMinerAnimTexId = this.IronMinerAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int farmerAnimTexId = this.FarmerAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int farmer2AnimTexId = this.Farmer2AnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int farmer3AnimTexId = this.Farmer3AnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int pitchworkerAnimTexId = this.PitchworkerAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int dockworkerAnimTexId = this.DockworkerAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int pigAnimTexId = this.PigAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int sheepAnimTexId = this.SheepAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int chickenWhiteAnimTexId = this.ChickenWhiteAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int chickenBrownAnimTexId = this.ChickenBrownAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int metalWorkerAnimTexId = this.MetalWorkerAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int fletcherAnimTexId = this.FletcherAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int poleturnerAnimTexId = this.PoleturnerAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int blacksmithAnimTexId = this.BlacksmithAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int cowAnimTexId = this.CowAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int goods1TexId = this.Goods1TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int goods2TexId = this.Goods2TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int townBuildindsTexId = this.TownBuildindsTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bld9x91TexId = this.Bld_9x9_1TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bld4x41TexId = this.Bld_4x4_1TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bld8x81TexId = this.Bld_8x8_1TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int woodcutterAnimsTexId = this.Woodcutter_animsTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bld6x61TexId = this.Bld_6x6_1TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bodyStonemasonTexId = this.Body_stonemasonTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int ironMineWorkTexId = this.Body_iron_mine_workTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int pitchworkerTexId = this.Body_pitchworkerTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bld13x131TexId = this.Bld_13x13_1TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bld13x132TexId = this.Bld_13x13_2TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bodyBrewerTexId = this.Body_brewerTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bodyFarmer3TexId = this.Body_farmer_3TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bodyBakerTexId = this.Body_bakerTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bld11x111TexId = this.Bld_11x11_1TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bld7x71TexId = this.Bld_7x7_1TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bld17x171TexId = this.Bld_17x17_1TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bodyTailorTexId = this.Body_tailorTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bodyCarpenterTexId = this.Body_carpenterTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bodyHunterTexId = this.Body_hunterTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int metalworkerTexId = this.Body_metalworkerTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bld5x51TexId = this.Bld_5x5_1TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bodyPoleturnerTexId = this.Body_poleturnerTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bodyFletcherTexId = this.Body_fletcherTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bodyBlacksmithTexId = this.Body_blacksmithTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bodyArmourerTexId = this.Body_armourerTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int siegeworkerTexId = this.Body_siegeworkerTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bldVarious01TexId = this.Bld_Various_01TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int animStocksTexId = this.Anim_stocksTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int animStakeTexId = this.Anim_stakeTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int animGibbetTexId = this.Anim_gibbetTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int animRackTexId = this.Anim_rackTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int animMaypoleTexId = this.Anim_maypoleTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int dancingBearTexId = this.Anim_dancing_bearTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int theaterworkerTexId = this.Body_theaterworkerTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bodyJesterTexId = this.Body_jesterTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int bodyTroubadourTexId = this.Body_troubadourTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int overlaysAnimTexId = this.VillageOverlaysAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      GC.Collect();
      GC.WaitForPendingFinalizers();
      int archerAnimTexId = this.ArcherAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int archer2AnimTexId = this.Archer2AnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int pikemanAnimTexId = this.PikemanAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int swordsmanAnimTexId = this.SwordsmanAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int peasantAnimTexId = this.PeasantAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int peasant2AnimTexId = this.Peasant2AnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int catapultAnimTexId = this.CatapultAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int wolfAnimTexId = this.WolfAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int archerRedAnimTexId = this.ArcherRedAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int archer2RedAnimTexId = this.Archer2RedAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int pikemanRedAnimTexId = this.PikemanRedAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int swordsmanRedAnimTexId = this.SwordsmanRedAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int peasantRedAnimTexId = this.PeasantRedAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int peasant2RedAnimTexId = this.Peasant2RedAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int archerGreenAnimTexId = this.ArcherGreenAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int archer2GreenAnimTexId = this.Archer2GreenAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int pikemanGreenAnimTexId = this.PikemanGreenAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int swordsmanGreenAnimTexId = this.SwordsmanGreenAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int peasantGreenAnimTexId = this.PeasantGreenAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int peasant2GreenAnimTexId = this.Peasant2GreenAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int archerCarryAnimTexId = this.ArcherCarryAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int peasantCarryAnimTexId = this.PeasantCarryAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int pikemanCarryAnimTexId = this.PikemanCarryAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int swordsmanCarryAnimTexId = this.SwordsmanCarryAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      GC.Collect();
      GC.WaitForPendingFinalizers();
      int knightAnimTexId = this.KnightAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int knightTopAnimTexId = this.KnightTopAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int captainAnimTexId = this.CaptainAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int missileTexId = this.MissileTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int missile2TexId = this.Missile2TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int killingPitsTexId = this.AnimKillingPitsTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int fireTexId = this.FireTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int oilPotAnimTexId = this.OilPotAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int manOnFireTexId = this.ManOnFireTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int smoke1TexId = this.Smoke1TexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int hpsBarsTexId = this.HpsBarsTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int traderAnimTexId = this.TraderAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int traderHorseAnimTexId = this.TraderHorseAnimTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int castleBackgroundTexId = this.CastleBackgroundTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int castleSpritesTexId = this.CastleSpritesTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int ballistaTexId = this.BallistaTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int armyAnimsTexId = this.ArmyAnimsTexID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int tutorialIconNormalId = this.TutorialIconNormalID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int tutorialIconOverId = this.TutorialIconOverID;
      if (GameEngine.Instance.cancelLoading())
        return;
      int freeCardIconsId = this.FreeCardIconsID;
      if (GameEngine.Instance.cancelLoading())
        return;
      GC.Collect();
      GC.WaitForPendingFinalizers();
    }

    public int getVillageBuildingTexture(string texName)
    {
      if (texName.Contains("bld_9x9_1.uv"))
        return this.Bld_9x9_1TexID;
      if (texName.Contains("bld_4x4_1.uv"))
        return this.Bld_4x4_1TexID;
      if (texName.Contains("bld_8x8_1.uv"))
        return this.Bld_8x8_1TexID;
      if (texName.Contains("woodcutter_anims.uv"))
        return this.Woodcutter_animsTexID;
      if (texName.Contains("bld_6x6_1.uv"))
        return this.Bld_6x6_1TexID;
      if (texName.Contains("body_stonemason anims.uv"))
        return this.Body_stonemasonTexID;
      if (texName.Contains("body_iron_mine_work.uv"))
        return this.Body_iron_mine_workTexID;
      if (texName.Contains("body_pitchworker anims.uv"))
        return this.Body_pitchworkerTexID;
      if (texName.Contains("bld_13x13_1.uv"))
        return this.Bld_13x13_1TexID;
      if (texName.Contains("bld_13x13_2.uv"))
        return this.Bld_13x13_2TexID;
      if (texName.Contains("body_brewer.uv"))
        return this.Body_brewerTexID;
      if (texName.Contains("body_farmer_3.uv"))
        return this.Body_farmer_3TexID;
      if (texName.Contains("body_baker.uv"))
        return this.Body_bakerTexID;
      if (texName.Contains("bld_11x11_1.uv"))
        return this.Bld_11x11_1TexID;
      if (texName.Contains("bld_7x7_1.uv"))
        return this.Bld_7x7_1TexID;
      if (texName.Contains("bld_17x17_1.uv"))
        return this.Bld_17x17_1TexID;
      if (texName.Contains("body_tailor.uv"))
        return this.Body_tailorTexID;
      if (texName.Contains("body_carpenter.uv"))
        return this.Body_carpenterTexID;
      if (texName.Contains("body_hunter.uv"))
        return this.Body_hunterTexID;
      if (texName.Contains("body_metalworker.uv"))
        return this.Body_metalworkerTexID;
      if (texName.Contains("bld_5x5_1.uv"))
        return this.Bld_5x5_1TexID;
      if (texName.Contains("body_poleturner.uv"))
        return this.Body_poleturnerTexID;
      if (texName.Contains("body_fletcher.uv"))
        return this.Body_fletcherTexID;
      if (texName.Contains("body_blacksmith.uv"))
        return this.Body_blacksmithTexID;
      if (texName.Contains("body_armourer.uv"))
        return this.Body_armourerTexID;
      if (texName.Contains("body_siegeworker.uv"))
        return this.Body_siegeworkerTexID;
      if (texName.Contains("bld_Various_01.uv"))
        return this.Bld_Various_01TexID;
      if (texName.Contains("anim_stocks.uv"))
        return this.Anim_stocksTexID;
      if (texName.Contains("anim_stake.uv"))
        return this.Anim_stakeTexID;
      if (texName.Contains("anim_gibbet.uv"))
        return this.Anim_gibbetTexID;
      if (texName.Contains("anim_rack.uv"))
        return this.Anim_rackTexID;
      if (texName.Contains("anim_maypole.uv"))
        return this.Anim_maypoleTexID;
      if (texName.Contains("anim_dancing_bear.uv"))
        return this.Anim_dancing_bearTexID;
      if (texName.Contains("body_theaterworker.uv"))
        return this.Body_theaterworkerTexID;
      if (texName.Contains("body_jester.uv"))
        return this.Body_jesterTexID;
      if (texName.Contains("body_troubadour.uv"))
        return this.Body_troubadourTexID;
      return texName.Contains("town_buildings_01.uv") ? this.TownBuildindsTexID : -1;
    }

    public void initAssetDictionaries()
    {
      GFXLibrary.CardPackImages = new Dictionary<string, BaseImage>();
      GFXLibrary.CardPackImages.Add("card_pack_army_gold_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_army_gold_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_army_gold_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_army_gold_over"));
      GFXLibrary.CardPackImages.Add("card_pack_army_silver_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_army_silver_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_army_silver_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_army_silver_over"));
      GFXLibrary.CardPackImages.Add("card_pack_army_standard_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_army_standard_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_army_standard_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_army_standard_over"));
      GFXLibrary.CardPackImages.Add("card_pack_castle_standard_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_castle_standard_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_castle_standard_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_castle_standard_over"));
      GFXLibrary.CardPackImages.Add("card_pack_defence_gold_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_defence_gold_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_defence_gold_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_defence_gold_over"));
      GFXLibrary.CardPackImages.Add("card_pack_defence_silver_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_defence_silver_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_defence_silver_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_defence_silver_over"));
      GFXLibrary.CardPackImages.Add("card_pack_defence_standard_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_defence_standard_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_defence_standard_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_defence_standard_over"));
      GFXLibrary.CardPackImages.Add("card_pack_food_gold_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_food_gold_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_food_gold_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_food_gold_over"));
      GFXLibrary.CardPackImages.Add("card_pack_food_silver_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_food_silver_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_food_silver_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_food_silver_over"));
      GFXLibrary.CardPackImages.Add("card_pack_food_standard_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_food_standard_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_food_standard_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_food_standard_over"));
      GFXLibrary.CardPackImages.Add("card_pack_Industry_gold_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_Industry_gold_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_Industry_gold_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_Industry_gold_over"));
      GFXLibrary.CardPackImages.Add("card_pack_Industry_silver_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_Industry_silver_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_Industry_silver_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_Industry_silver_over"));
      GFXLibrary.CardPackImages.Add("card_pack_Industry_standard_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_Industry_standard_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_Industry_standard_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_Industry_standard_over"));
      GFXLibrary.CardPackImages.Add("card_pack_random_gold_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_random_gold_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_random_gold_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_random_gold_over"));
      GFXLibrary.CardPackImages.Add("card_pack_random_silver_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_random_silver_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_random_silver_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_random_silver_over"));
      GFXLibrary.CardPackImages.Add("card_pack_random_standard_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_random_standard_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_random_standard_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_random_standard_over"));
      GFXLibrary.CardPackImages.Add("card_pack_exclusive_silver_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_exclusive_silver_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_exclusive_silver_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_exclusive_silver_over"));
      GFXLibrary.CardPackImages.Add("card_pack_research_silver_normal", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_research_silver_normal"));
      GFXLibrary.CardPackImages.Add("card_pack_research_silver_over", new BaseImage(AssetPaths.AssetIconsCardOffers, "card_pack_research_silver_over"));
      GFXLibrary.CardSlotStillSymbols = new Dictionary<int, BaseImage>();
      GFXLibrary.CardSlotStillSymbols.Add(16777216, new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_apple.png"));
      GFXLibrary.CardSlotStillSymbols.Add(268435456, new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_castle.png"));
      GFXLibrary.CardSlotStillSymbols.Add(1073741824, new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_crown.png"));
      GFXLibrary.CardSlotStillSymbols.Add(67108864, new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_hawk.png"));
      GFXLibrary.CardSlotStillSymbols.Add(536870912, new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_jester.png"));
      GFXLibrary.CardSlotStillSymbols.Add(134217728, new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_shield.png"));
      GFXLibrary.CardSlotStillSymbols.Add(33554432, new BaseImage(AssetPaths.AssetIconsCardSlots, "card-screen_slot_wolf.png"));
      GFXLibrary.LoginWorldFlags.Add("en", new BaseImage(AssetPaths.AssetIconsCardPanel, "flag_en.png"));
      GFXLibrary.LoginWorldFlags.Add("de", new BaseImage(AssetPaths.AssetIconsCardPanel, "flag_de.png"));
      GFXLibrary.LoginWorldFlags.Add("fr", new BaseImage(AssetPaths.AssetIconsCardPanel, "flag_fr.png"));
      GFXLibrary.LoginWorldFlags.Add("ru", new BaseImage(AssetPaths.AssetIconsCardPanel, "flag_ru.png"));
      GFXLibrary.LoginWorldFlags.Add("es", new BaseImage(AssetPaths.AssetIconsCardPanel, "flag_es.png"));
      GFXLibrary.LoginWorldFlags.Add("pl", new BaseImage(AssetPaths.AssetIconsCardPanel, "flag_pl.png"));
      GFXLibrary.LoginWorldFlags.Add("br", new BaseImage(AssetPaths.AssetIconsCardPanel, "flag_br.png"));
      GFXLibrary.LoginWorldFlags.Add("pt", new BaseImage(AssetPaths.AssetIconsCardPanel, "flag_esbr.png"));
      GFXLibrary.LoginWorldFlags.Add("it", new BaseImage(AssetPaths.AssetIconsCardPanel, "flag_it.png"));
      GFXLibrary.LoginWorldFlags.Add("tr", new BaseImage(AssetPaths.AssetIconsCardPanel, "flag_tk.png"));
      GFXLibrary.LoginWorldFlags.Add("eu", new BaseImage(AssetPaths.AssetIconsMisc, "flag_eu.png"));
      GFXLibrary.LoginWorldFlags.Add("wd", new BaseImage(AssetPaths.AssetIconsCardPanel, "flag_wd.png"));
      GFXLibrary.LoginWorldFlags.Add("zh", new BaseImage(AssetPaths.AssetIconsCardPanel, "flag_ch.png"));
      GFXLibrary.LoginWorldFlags.Add("jp", new BaseImage(AssetPaths.AssetIconsCardPanel, "flag_jp.png"));
      GFXLibrary.LoginWorldMaps.Add("en", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_en"));
      GFXLibrary.LoginWorldMaps.Add("de", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_de"));
      GFXLibrary.LoginWorldMaps.Add("fr", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_fr"));
      GFXLibrary.LoginWorldMaps.Add("ru", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_ru"));
      GFXLibrary.LoginWorldMaps.Add("es", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_es"));
      GFXLibrary.LoginWorldMaps.Add("pl", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_pl"));
      GFXLibrary.LoginWorldMaps.Add("tr", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_tk"));
      GFXLibrary.LoginWorldMaps.Add("us", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_us"));
      GFXLibrary.LoginWorldMaps.Add("it", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_it"));
      GFXLibrary.LoginWorldMaps.Add("eu", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_eu"));
      GFXLibrary.LoginWorldMaps.Add("pt", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_sa"));
      GFXLibrary.LoginWorldMaps.Add("wd", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_wd"));
      GFXLibrary.LoginWorldMaps.Add("ph", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_ph"));
      GFXLibrary.LoginWorldMaps.Add("zh", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_ch"));
      GFXLibrary.LoginWorldMaps.Add("kg", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_kg"));
      GFXLibrary.LoginWorldMaps.Add("jp", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_jp"));
      GFXLibrary.LoginWorldMaps.Add("hy", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_hy"));
      GFXLibrary.LoginWorldMaps.Add("vk", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_vk"));
      GFXLibrary.LoginWorldMaps.Add("gd", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_gd"));
      GFXLibrary.LoginWorldMaps.Add("cru", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_cru"));
      GFXLibrary.LoginWorldMaps.Add("sp", new BaseImage(AssetPaths.AssetIconsCardPanel, "map_sparta"));
      GFXLibrary.PremiumTokens = new Dictionary<int, BaseImage[]>();
      GFXLibrary.PremiumTokens.Add(4112, new BaseImage[2]
      {
        new BaseImage(AssetPaths.AssetIconsCardPanel, "premium_disk_normal.png"),
        new BaseImage(AssetPaths.AssetIconsCardPanel, "premium_disk_over.png")
      });
      GFXLibrary.PremiumTokens.Add(4113, new BaseImage[2]
      {
        new BaseImage(AssetPaths.AssetIconsCardPanel, "premium_disk_2_normal.png"),
        new BaseImage(AssetPaths.AssetIconsCardPanel, "premium_disk_2_over.png")
      });
      GFXLibrary.PremiumTokens.Add(4114, new BaseImage[2]
      {
        new BaseImage(AssetPaths.AssetIconsCardPanel, "premium_disk_30_normal.png"),
        new BaseImage(AssetPaths.AssetIconsCardPanel, "premium_disk_30_over.png")
      });
      GFXLibrary.PremiumTokens.Add(4116, new BaseImage[2]
      {
        new BaseImage(AssetPaths.AssetIconsCardPanel, "premium_disk_x_normal.png"),
        new BaseImage(AssetPaths.AssetIconsCardPanel, "premium_disk_x_over.png")
      });
    }

    public void flushSnowGFX()
    {
      this.effectLayerTexID = -1;
      this.mapElementsTexID = -1;
      this.worldMapTilesTexID = -1;
      UVSpriteLoader.loadUVX("assets\\uvx.resources");
      int effectLayerTexId = this.EffectLayerTexID;
      int mapElementsTexId = this.MapElementsTexID;
      int worldMapTilesTexId = this.WorldMapTilesTexID;
      UVSpriteLoader.closeUVX();
      GameEngine.Instance.World.updateSeasonalGFX();
    }

    public void loadResources()
    {
      GFXLibrary.LoginWorldFlags = new Dictionary<string, BaseImage>();
      GFXLibrary.LoginWorldMaps = new Dictionary<string, BaseImage>();
      this.initAssetDictionaries();
      this.loadCards();
      AssetLoader.instance.onStartup();
      int length = GFXLibrary.avatar_parchment_top_multiply.Width * GFXLibrary.avatar_parchment_top_multiply.Height * 4;
      GFXLibrary.parchementOverlay = new byte[length];
      Rectangle rect = new Rectangle(0, 0, GFXLibrary.avatar_parchment_top_multiply.Width, GFXLibrary.avatar_parchment_top_multiply.Height);
      Bitmap parchmentTopMultiply = (Bitmap) (Image) GFXLibrary.avatar_parchment_top_multiply;
      BitmapData bitmapdata = parchmentTopMultiply.LockBits(rect, ImageLockMode.ReadOnly, parchmentTopMultiply.PixelFormat);
      Marshal.Copy(bitmapdata.Scan0, GFXLibrary.parchementOverlay, 0, length);
      parchmentTopMultiply.UnlockBits(bitmapdata);
      GFXLibrary.avatar_parchment_top_multiply = (BaseImage) null;
      parchmentTopMultiply.Dispose();
      InterfaceMgr.Instance.getTopLeftMenu().init();
      InterfaceMgr.Instance.getTopRightMenu().init();
      InterfaceMgr.Instance.getMainTabBar().initImages();
      InterfaceMgr.Instance.getVillageTabBar().initImages();
      InterfaceMgr.Instance.getFactionTabBar().initImages();
    }

    public void loadCards()
    {
      GFXLibrary.CardSlotAnimData = new int[GFXLibrary.CardSlotAnimFrames.Length];
      for (int index = 0; index < GFXLibrary.CardSlotAnimData.Length; ++index)
      {
        switch (index)
        {
          case 0:
            GFXLibrary.CardSlotAnimData[index] = 16777216;
            break;
          case 3:
            GFXLibrary.CardSlotAnimData[index] = 268435456;
            break;
          case 6:
            GFXLibrary.CardSlotAnimData[index] = 1073741824;
            break;
          case 9:
            GFXLibrary.CardSlotAnimData[index] = 67108864;
            break;
          case 12:
            GFXLibrary.CardSlotAnimData[index] = 536870912;
            break;
          case 15:
            GFXLibrary.CardSlotAnimData[index] = 134217728;
            break;
          case 18:
            GFXLibrary.CardSlotAnimData[index] = 33554432;
            break;
          default:
            GFXLibrary.CardSlotAnimData[index] = 0;
            break;
        }
      }
      ResourceLoader resourceLoader = new ResourceLoader("AssetIcons\\Cards\\Panel\\Panel.resources");
      GFXLibrary.invite_ad_colour = new Random().Next(5);
      string str1;
      string str2;
      switch (Program.mySettings.LanguageIdent)
      {
        case "fr":
          str1 = "ad_invite__0006__fr";
          str2 = "ad_invite_quest_top__0006__fr";
          break;
        case "de":
          str1 = "ad_invite__0004__de";
          str2 = "ad_invite_quest_top__0004__de";
          break;
        case "ru":
          str1 = "ad_invite__0005__ru";
          str2 = "ad_invite_quest_top__0005__ru";
          break;
        case "pl":
          str1 = "ad_invite__0008__pl";
          str2 = "ad_invite_quest-top__0008__pl";
          break;
        case "pt":
          str1 = "ad_invite__0001__pt";
          str2 = "ad_invite_quest_top__0001__pt";
          break;
        case "tr":
          str1 = "ad_invite__0007__tr";
          str2 = "ad_invite_quest_top__0007__tr";
          break;
        case "es":
          str1 = "ad_invite__0002__sp";
          str2 = "ad_invite_quest_top__0002__sp";
          break;
        case "it":
          str1 = "ad_invite__0003__it";
          str2 = "ad_invite_quest_top__0003__it";
          break;
        case "zh":
          str1 = "ad_invite__0009__sc";
          str2 = "ad_invite_quest-top__0009__sc";
          break;
        case "zhhk":
          str1 = "ad_invite__0010__tc";
          str2 = "ad_invite_quest-top__0010__tc";
          break;
        case "ko":
          str1 = "ad_invite__0011__ko";
          str2 = "ad_invite_quest-top__0011__ko";
          break;
        case "jp":
          str1 = "ad_invite__0012__jp";
          str2 = "ad_invite_quest-top__0012__jp";
          break;
        default:
          str1 = "ad_invite__0000__en";
          str2 = "ad_invite_quest_top__0000__en";
          break;
      }
      string assetURI1 = str1 + ".png";
      GFXLibrary.banner_ad_friend = new BaseImage(AssetPaths.AssetIconsCardPanel, assetURI1);
      string assetURI2 = str2 + ".png";
      GFXLibrary.banner_ad_friend_quest = new BaseImage(AssetPaths.AssetIconsCardPanel, assetURI2);
      resourceLoader.Close();
      Image image = (Image) new Bitmap(1, 1);
      using (Graphics graphics = Graphics.FromImage(image))
        graphics.Clear(ARGBColors.Transparent);
      GFXLibrary.dummy = image;
    }

    public BaseImage getCardImageBig(int cardType)
    {
      BaseImage cardImageBig = GFXLibrary.NoImageCardBig;
      BaseImage baseImage = (BaseImage) this.cardImagesBig[CardTypes.getCardType(cardType)];
      if (baseImage == null)
      {
        CardTypes.CardDefinition cardDefinition = CardTypes.getCardDefinition(cardType);
        string assetURI = CardTypes.getStringFromCard(cardDefinition.id) + ".jpg";
        if (cardDefinition.available == 1 || assetURI.ToLower().Contains("cardtype_"))
        {
          this.cardImagesBig[CardTypes.getCardType(cardType)] = (object) new BaseImage(AssetPaths.AssetIconsBigCards, assetURI, BaseImage.loadType.CARD);
          if (this.cardImagesBig[CardTypes.getCardType(cardType)] != null)
            cardImageBig = (BaseImage) this.cardImagesBig[CardTypes.getCardType(cardType)];
        }
      }
      else
        cardImageBig = baseImage;
      return cardImageBig;
    }

    public void closeBigCardsLoader()
    {
      if (this.cachedBigCardsLoader != null)
        this.cachedBigCardsLoader.Close();
      this.cachedBigCardsLoader = (ResourceLoader) null;
    }

    public static int getCommodity32GFXno(int resource)
    {
      int commodity32GfXno = -1;
      switch (resource)
      {
        case 6:
          commodity32GfXno = 27;
          break;
        case 7:
          commodity32GfXno = 22;
          break;
        case 8:
          commodity32GfXno = 12;
          break;
        case 9:
          commodity32GfXno = 18;
          break;
        case 12:
          commodity32GfXno = 0;
          break;
        case 13:
          commodity32GfXno = 1;
          break;
        case 14:
          commodity32GfXno = 4;
          break;
        case 15:
          commodity32GfXno = 24;
          break;
        case 16:
          commodity32GfXno = 13;
          break;
        case 17:
          commodity32GfXno = 6;
          break;
        case 18:
          commodity32GfXno = 8;
          break;
        case 19:
          commodity32GfXno = 7;
          break;
        case 21:
          commodity32GfXno = 10;
          break;
        case 22:
          commodity32GfXno = 25;
          break;
        case 23:
          commodity32GfXno = 19;
          break;
        case 24:
          commodity32GfXno = 21;
          break;
        case 25:
          commodity32GfXno = 20;
          break;
        case 26:
          commodity32GfXno = 14;
          break;
        case 28:
          commodity32GfXno = 17;
          break;
        case 29:
          commodity32GfXno = 3;
          break;
        case 30:
          commodity32GfXno = 23;
          break;
        case 31:
          commodity32GfXno = 2;
          break;
        case 32:
          commodity32GfXno = 5;
          break;
        case 33:
          commodity32GfXno = 26;
          break;
      }
      return commodity32GfXno;
    }

    public static BaseImage getCommodity32Image(int resource)
    {
      switch (resource)
      {
        case 6:
          return GFXLibrary.com_32_wood;
        case 7:
          return GFXLibrary.com_32_stone;
        case 8:
          return GFXLibrary.com_32_iron;
        case 9:
          return GFXLibrary.com_32_pitch;
        case 12:
          return GFXLibrary.com_32_ale;
        case 13:
          return GFXLibrary.com_32_apples;
        case 14:
          return GFXLibrary.com_32_bread;
        case 15:
          return GFXLibrary.com_32_veg;
        case 16:
          return GFXLibrary.com_32_meat;
        case 17:
          return GFXLibrary.com_32_cheese;
        case 18:
          return GFXLibrary.com_32_fish;
        case 19:
          return GFXLibrary.com_32_clothing;
        case 21:
          return GFXLibrary.com_32_furniture;
        case 22:
          return GFXLibrary.com_32_venison;
        case 23:
          return GFXLibrary.com_32_salt;
        case 24:
          return GFXLibrary.com_32_spice;
        case 25:
          return GFXLibrary.com_32_silk;
        case 26:
          return GFXLibrary.com_32_metalwork;
        case 28:
          return GFXLibrary.com_32_pikes;
        case 29:
          return GFXLibrary.com_32_bows;
        case 30:
          return GFXLibrary.com_32_swords;
        case 31:
          return GFXLibrary.com_32_armour;
        case 32:
          return GFXLibrary.com_32_catapults;
        case 33:
          return GFXLibrary.com_32_wine;
        default:
          return (BaseImage) null;
      }
    }

    public static BaseImage getCommodity64DSImage(int resource)
    {
      switch (resource)
      {
        case 6:
          return GFXLibrary.com_64_wood_DS;
        case 7:
          return GFXLibrary.com_64_stone_DS;
        case 8:
          return GFXLibrary.com_64_iron_DS;
        case 9:
          return GFXLibrary.com_64_pitch_DS;
        case 12:
          return GFXLibrary.com_64_ale_DS;
        case 13:
          return GFXLibrary.com_64_apples_DS;
        case 14:
          return GFXLibrary.com_64_bread_DS;
        case 15:
          return GFXLibrary.com_64_veg_DS;
        case 16:
          return GFXLibrary.com_64_meat_DS;
        case 17:
          return GFXLibrary.com_64_cheese_DS;
        case 18:
          return GFXLibrary.com_64_fish_DS;
        case 19:
          return GFXLibrary.com_64_clothes_DS;
        case 21:
          return GFXLibrary.com_64_furniture_DS;
        case 22:
          return GFXLibrary.com_64_venison_DS;
        case 23:
          return GFXLibrary.com_64_salt_DS;
        case 24:
          return GFXLibrary.com_64_spices_DS;
        case 25:
          return GFXLibrary.com_64_silk_DS;
        case 26:
          return GFXLibrary.com_64_metalware_DS;
        case 28:
          return GFXLibrary.com_64_pikes_DS;
        case 29:
          return GFXLibrary.com_64_bows_DS;
        case 30:
          return GFXLibrary.com_64_swords_DS;
        case 31:
          return GFXLibrary.com_64_armour_DS;
        case 32:
          return GFXLibrary.com_64_catapults_DS;
        case 33:
          return GFXLibrary.com_64_wine_DS;
        default:
          return (BaseImage) null;
      }
    }

    public static BaseImage getCommodity32DSImage(int resource)
    {
      switch (resource)
      {
        case 6:
          return GFXLibrary.com_32_wood_DS;
        case 7:
          return GFXLibrary.com_32_stone_DS;
        case 8:
          return GFXLibrary.com_32_iron_DS;
        case 9:
          return GFXLibrary.com_32_pitch_DS;
        case 12:
          return GFXLibrary.com_32_ale_DS;
        case 13:
          return GFXLibrary.com_32_apples_DS;
        case 14:
          return GFXLibrary.com_32_bread_DS;
        case 15:
          return GFXLibrary.com_32_veg_DS;
        case 16:
          return GFXLibrary.com_32_meat_DS;
        case 17:
          return GFXLibrary.com_32_cheese_DS;
        case 18:
          return GFXLibrary.com_32_fish_DS;
        case 19:
          return GFXLibrary.com_32_clothes_DS;
        case 21:
          return GFXLibrary.com_32_furniture_DS;
        case 22:
          return GFXLibrary.com_32_venison_DS;
        case 23:
          return GFXLibrary.com_32_salt_DS;
        case 24:
          return GFXLibrary.com_32_spices_DS;
        case 25:
          return GFXLibrary.com_32_silk_DS;
        case 26:
          return GFXLibrary.com_32_metalware_DS;
        case 28:
          return GFXLibrary.com_32_pikes_DS;
        case 29:
          return GFXLibrary.com_32_bows_DS;
        case 30:
          return GFXLibrary.com_32_swords_DS;
        case 31:
          return GFXLibrary.com_32_armour_DS;
        case 32:
          return GFXLibrary.com_32_catapults_DS;
        case 33:
          return GFXLibrary.com_32_wine_DS;
        default:
          return (BaseImage) null;
      }
    }

    public static BaseImage GetLeaderboardCategoryIcon(int category)
    {
      switch (category)
      {
        case -6:
          return GFXLibrary.catagory_icons_villages_pushed;
        case -5:
          return GFXLibrary.catagory_icons_rank_pushed;
        case -1:
          return GFXLibrary.catagory_icons_points_pushed;
        case 0:
          return GFXLibrary.catagory_icons_pillager_pushed;
        case 1:
          return GFXLibrary.catagory_icons_defender_pushed;
        case 2:
          return GFXLibrary.catagory_icons_destroyer_pushed;
        case 3:
          return GFXLibrary.catagory_icons_wolfbane_pushed;
        case 4:
          return GFXLibrary.catagory_icons_banditslayer_pushed;
        case 5:
          return GFXLibrary.catagory_icons_aikiller_pushed;
        case 6:
          return GFXLibrary.catagory_icons_merchant_pushed;
        case 7:
          return GFXLibrary.catagory_icons_forger_pushed;
        case 8:
          return GFXLibrary.catagory_icons_worker_pushed;
        case 9:
          return GFXLibrary.catagory_icons_farmer_pushed;
        case 10:
          return GFXLibrary.catagory_icons_brewer_pushed;
        case 11:
          return GFXLibrary.catagory_icons_blacksmith_pushed;
        case 12:
          return GFXLibrary.catagory_icons_banquet_pushed;
        case 13:
          return GFXLibrary.catagory_icons_achiever_pushed;
        case 14:
          return GFXLibrary.catagory_icons_donator_pushed;
        case 15:
          return GFXLibrary.catagory_icons_capture_pushed;
        case 16:
          return GFXLibrary.catagory_icons_raze_pushed;
        case 17:
          return GFXLibrary.catagory_icons_glory_pushed;
        case 18:
          return GFXLibrary.catagory_icons_killstreak_pushed;
        default:
          return (BaseImage) null;
      }
    }

    public static Image getHouseBannerImage(int houseID, int size)
    {
      int globalWorldId = GameEngine.Instance.World.GetGlobalWorldID();
      if (globalWorldId >= 1600 && globalWorldId < 1700)
      {
        switch (size)
        {
          case 1:
            return (Image) GFXLibrary.glory_flags_med_warlords[houseID - 1];
          case 2:
            return (Image) GFXLibrary.glory_flags_large_warlords[houseID - 1];
          case 3:
            return (Image) GFXLibrary.glory_flags_largest_warlords[houseID - 1];
          default:
            return (Image) GFXLibrary.glory_flags_small_warlords[houseID - 1];
        }
      }
      else
      {
        switch (size)
        {
          case 1:
            return (Image) GFXLibrary.glory_flags_med[houseID - 1];
          case 2:
            return (Image) GFXLibrary.glory_flags_large[houseID - 1];
          case 3:
            return (Image) GFXLibrary.glory_flags_largest[houseID - 1];
          default:
            return (Image) GFXLibrary.glory_flags_small[houseID - 1];
        }
      }
    }

    public static BaseImage getHouseCircleMediumImage(int houseID)
    {
      int globalWorldId = GameEngine.Instance.World.GetGlobalWorldID();
      return globalWorldId >= 1600 && globalWorldId < 1700 ? GFXLibrary.house_circles_medium_warlords[houseID] : GFXLibrary.house_circles_medium[houseID];
    }

    public static BaseImage getHouseCircleLargeImage(int houseID)
    {
      int globalWorldId = GameEngine.Instance.World.GetGlobalWorldID();
      return globalWorldId >= 1600 && globalWorldId < 1700 ? GFXLibrary.house_circles_large_warlords[houseID] : GFXLibrary.house_circles_large[houseID];
    }

    public static BaseImage getHouseFlag(int houseID)
    {
      int globalWorldId = GameEngine.Instance.World.GetGlobalWorldID();
      if (globalWorldId >= 1600 && globalWorldId < 1700)
      {
        switch (houseID)
        {
          case 1:
            return GFXLibrary.house_flag_001_WL;
          case 2:
            return GFXLibrary.house_flag_002_WL;
          case 3:
            return GFXLibrary.house_flag_003_WL;
          case 4:
            return GFXLibrary.house_flag_004_WL;
          case 5:
            return GFXLibrary.house_flag_005_WL;
          case 6:
            return GFXLibrary.house_flag_006_WL;
          case 7:
            return GFXLibrary.house_flag_007_WL;
          case 8:
            return GFXLibrary.house_flag_008_WL;
          case 9:
            return GFXLibrary.house_flag_009_WL;
          case 10:
            return GFXLibrary.house_flag_010_WL;
          case 11:
            return GFXLibrary.house_flag_011_WL;
          case 12:
            return GFXLibrary.house_flag_012_WL;
          case 13:
            return GFXLibrary.house_flag_013_WL;
          case 14:
            return GFXLibrary.house_flag_014_WL;
          case 15:
            return GFXLibrary.house_flag_015_WL;
          case 16:
            return GFXLibrary.house_flag_016_WL;
          case 17:
            return GFXLibrary.house_flag_017_WL;
          case 18:
            return GFXLibrary.house_flag_018_WL;
          case 19:
            return GFXLibrary.house_flag_019_WL;
          case 20:
            return GFXLibrary.house_flag_020_WL;
          default:
            return (BaseImage) null;
        }
      }
      else
      {
        switch (houseID)
        {
          case 1:
            return GFXLibrary.house_flag_001;
          case 2:
            return GFXLibrary.house_flag_002;
          case 3:
            return GFXLibrary.house_flag_003;
          case 4:
            return GFXLibrary.house_flag_004;
          case 5:
            return GFXLibrary.house_flag_005;
          case 6:
            return GFXLibrary.house_flag_006;
          case 7:
            return GFXLibrary.house_flag_007;
          case 8:
            return GFXLibrary.house_flag_008;
          case 9:
            return GFXLibrary.house_flag_009;
          case 10:
            return GFXLibrary.house_flag_010;
          case 11:
            return GFXLibrary.house_flag_011;
          case 12:
            return GFXLibrary.house_flag_012;
          case 13:
            return GFXLibrary.house_flag_013;
          case 14:
            return GFXLibrary.house_flag_014;
          case 15:
            return GFXLibrary.house_flag_015;
          case 16:
            return GFXLibrary.house_flag_016;
          case 17:
            return GFXLibrary.house_flag_017;
          case 18:
            return GFXLibrary.house_flag_018;
          case 19:
            return GFXLibrary.house_flag_019;
          case 20:
            return GFXLibrary.house_flag_020;
          default:
            return (BaseImage) null;
        }
      }
    }

    public static BaseImage getHouseFlagSmall(int houseID)
    {
      int globalWorldId = GameEngine.Instance.World.GetGlobalWorldID();
      if (globalWorldId >= 1600 && globalWorldId < 1700)
      {
        switch (houseID)
        {
          case 1:
            return GFXLibrary.house_flag_001_small_WL;
          case 2:
            return GFXLibrary.house_flag_002_small_WL;
          case 3:
            return GFXLibrary.house_flag_003_small_WL;
          case 4:
            return GFXLibrary.house_flag_004_small_WL;
          case 5:
            return GFXLibrary.house_flag_005_small_WL;
          case 6:
            return GFXLibrary.house_flag_006_small_WL;
          case 7:
            return GFXLibrary.house_flag_007_small_WL;
          case 8:
            return GFXLibrary.house_flag_008_small_WL;
          case 9:
            return GFXLibrary.house_flag_009_small_WL;
          case 10:
            return GFXLibrary.house_flag_010_small_WL;
          case 11:
            return GFXLibrary.house_flag_011_small_WL;
          case 12:
            return GFXLibrary.house_flag_012_small_WL;
          case 13:
            return GFXLibrary.house_flag_013_small_WL;
          case 14:
            return GFXLibrary.house_flag_014_small_WL;
          case 15:
            return GFXLibrary.house_flag_015_small_WL;
          case 16:
            return GFXLibrary.house_flag_016_small_WL;
          case 17:
            return GFXLibrary.house_flag_017_small_WL;
          case 18:
            return GFXLibrary.house_flag_018_small_WL;
          case 19:
            return GFXLibrary.house_flag_019_small_WL;
          case 20:
            return GFXLibrary.house_flag_020_small_WL;
          default:
            return GFXLibrary.house_flag_001_small;
        }
      }
      else
      {
        switch (houseID)
        {
          case 1:
            return GFXLibrary.house_flag_001_small;
          case 2:
            return GFXLibrary.house_flag_002_small;
          case 3:
            return GFXLibrary.house_flag_003_small;
          case 4:
            return GFXLibrary.house_flag_004_small;
          case 5:
            return GFXLibrary.house_flag_005_small;
          case 6:
            return GFXLibrary.house_flag_006_small;
          case 7:
            return GFXLibrary.house_flag_007_small;
          case 8:
            return GFXLibrary.house_flag_008_small;
          case 9:
            return GFXLibrary.house_flag_009_small;
          case 10:
            return GFXLibrary.house_flag_010_small;
          case 11:
            return GFXLibrary.house_flag_011_small;
          case 12:
            return GFXLibrary.house_flag_012_small;
          case 13:
            return GFXLibrary.house_flag_013_small;
          case 14:
            return GFXLibrary.house_flag_014_small;
          case 15:
            return GFXLibrary.house_flag_015_small;
          case 16:
            return GFXLibrary.house_flag_016_small;
          case 17:
            return GFXLibrary.house_flag_017_small;
          case 18:
            return GFXLibrary.house_flag_018_small;
          case 19:
            return GFXLibrary.house_flag_019_small;
          case 20:
            return GFXLibrary.house_flag_020_small;
          default:
            return GFXLibrary.house_flag_001_small;
        }
      }
    }
  }
}
