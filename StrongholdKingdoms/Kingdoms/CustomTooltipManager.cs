// Decompiled with JetBrains decompiler
// Type: Kingdoms.CustomTooltipManager
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

//#nullable disable
namespace Kingdoms
{
  public class CustomTooltipManager
  {
    private static int showingTooltip = 0;
    private static int currentOverTooltip = 0;
    private static int lastOverTooltip = 0;
    private static DateTime lastLeaveTime = DateTime.MinValue;
    private static int currentOverData = 0;
    private static int currentOverLastData = 0;
    private static Form currentParentWindow = (Form) null;
    public static int storedCurrentOverData = 0;
    public static int storedCurrentOverTooltip = 0;
    public static Form storedCurrentParentWindow = (Form) null;
    private static bool overTooltip = false;
    public static WorldMap.CachedUserInfo UserInfo = (WorldMap.CachedUserInfo) null;
    private static bool inSystemControl = false;
    private static Point lastMousePosition = new Point();
    private static DateTime lastMouseMoveTime = DateTime.MinValue;
    private static bool staticMouse = false;

    public static void MouseEnterTooltipArea(int ID)
    {
      if (!Program.mySettings.SETTINGS_showTooltips)
        return;
      if (ID == 0)
      {
        CustomTooltipManager.MouseLeaveTooltipArea();
      }
      else
      {
        CustomTooltipManager.storedCurrentOverTooltip = CustomTooltipManager.currentOverTooltip = ID;
        CustomTooltipManager.storedCurrentOverData = CustomTooltipManager.currentOverData = 0;
        CustomTooltipManager.storedCurrentParentWindow = CustomTooltipManager.currentParentWindow = (Form) null;
      }
    }

    public static void MouseEnterTooltipArea(int ID, int data)
    {
      if (!Program.mySettings.SETTINGS_showTooltips)
        return;
      if (ID == 0)
      {
        CustomTooltipManager.MouseLeaveTooltipArea();
      }
      else
      {
        CustomTooltipManager.storedCurrentOverTooltip = CustomTooltipManager.currentOverTooltip = ID;
        CustomTooltipManager.storedCurrentOverData = CustomTooltipManager.currentOverData = data;
        CustomTooltipManager.storedCurrentParentWindow = CustomTooltipManager.currentParentWindow = (Form) null;
      }
    }

    public static void MouseEnterTooltipArea(int ID, int data, Form parentWindow)
    {
      if (!Program.mySettings.SETTINGS_showTooltips)
        return;
      if (ID == 0)
      {
        CustomTooltipManager.MouseLeaveTooltipArea();
      }
      else
      {
        CustomTooltipManager.storedCurrentOverTooltip = CustomTooltipManager.currentOverTooltip = ID;
        CustomTooltipManager.storedCurrentOverData = CustomTooltipManager.currentOverData = data;
        CustomTooltipManager.storedCurrentParentWindow = CustomTooltipManager.currentParentWindow = parentWindow;
      }
    }

    public static void MouseEnterTooltipAreaStored()
    {
      CustomTooltipManager.MouseEnterTooltipArea(CustomTooltipManager.storedCurrentOverTooltip, CustomTooltipManager.storedCurrentOverData, CustomTooltipManager.storedCurrentParentWindow);
      CustomTooltipManager.overTooltip = true;
    }

    public static void MouseLeaveTooltipAreaStored() => CustomTooltipManager.overTooltip = false;

    public static void MouseLeaveTooltipArea()
    {
      if (CustomTooltipManager.currentOverTooltip == 0 || CustomTooltipManager.overTooltip)
        return;
      CustomTooltipManager.lastOverTooltip = CustomTooltipManager.currentOverTooltip;
      CustomTooltipManager.lastLeaveTime = DateTime.Now;
      CustomTooltipManager.currentOverTooltip = 0;
    }

    public static void MouseLeaveTooltipAreaMapSpecial()
    {
      if (CustomTooltipManager.currentOverTooltip == 0 || CustomTooltipManager.inSystemControl || CustomTooltipManager.overTooltip)
        return;
      CustomTooltipManager.lastOverTooltip = CustomTooltipManager.currentOverTooltip;
      CustomTooltipManager.lastLeaveTime = DateTime.Now;
      CustomTooltipManager.currentOverTooltip = 0;
    }

    public static void addTooltipToSystemControl(Control control, int ID)
    {
      control.MouseLeave += new EventHandler(CustomTooltipManager.systemToolTipLeave);
      control.MouseEnter += new EventHandler(CustomTooltipManager.systemToolTipEnter);
      control.Name = ID.ToString();
    }

    public static void updateTooltipForSystemControl(Control control, int ID)
    {
      control.Name = ID.ToString();
    }

    private static void systemToolTipEnter(object sender, EventArgs e)
    {
      Control control = (Control) sender;
      CustomTooltipManager.MouseEnterTooltipArea(Convert.ToInt32(control.Name), 0, control.FindForm());
      CustomTooltipManager.inSystemControl = true;
    }

    private static void systemToolTipLeave(object sender, EventArgs e)
    {
      CustomTooltipManager.inSystemControl = false;
      CustomTooltipManager.MouseLeaveTooltipArea();
    }

    public static void addTooltipZonesToSystemControl(
      Control control,
      CustomTooltipManager.ToolTipZone[] zones)
    {
      CustomTooltipManager.ToolTipZoneControlChild zoneControl = CustomTooltipManager.findZoneControl((object) control);
      if (zoneControl != null)
      {
        control.Controls.Remove((Control) zoneControl);
      }
      else
      {
        control.MouseLeave += new EventHandler(CustomTooltipManager.systemToolTipZoneLeave);
        control.MouseEnter += new EventHandler(CustomTooltipManager.systemToolTipZoneEnter);
        control.MouseMove += new MouseEventHandler(CustomTooltipManager.systemToolTipZoneMove);
      }
      CustomTooltipManager.ToolTipZoneControlChild zoneControlChild = new CustomTooltipManager.ToolTipZoneControlChild();
      zoneControlChild.Visible = false;
      zoneControlChild.zones = zones;
      control.Controls.Add((Control) zoneControlChild);
    }

    private static void systemToolTipZoneMove(object sender, MouseEventArgs e)
    {
      CustomTooltipManager.systemToolTipZoneEnter(sender, (EventArgs) e);
    }

    private static void systemToolTipZoneEnter(object sender, EventArgs e)
    {
      CustomTooltipManager.ToolTipZoneControlChild zoneControl = CustomTooltipManager.findZoneControl(sender);
      if (zoneControl != null)
      {
        Point point = new Point(Cursor.Position.X, Cursor.Position.Y);
        Point screen = zoneControl.PointToScreen(((Control) sender).Location);
        Point pt = new Point(point.X - screen.X, point.Y - screen.Y);
        foreach (CustomTooltipManager.ToolTipZone zone in zoneControl.zones)
        {
          if (zone.rect.Contains(pt))
          {
            if ((zone.relatedControl == null || zone.relatedControl.Visible) && (zone.relatedCSDControl == null || zone.relatedCSDControl.Visible))
            {
              CustomTooltipManager.MouseEnterTooltipArea(zone.ID, 0, zoneControl.FindForm());
              return;
            }
            break;
          }
        }
      }
      CustomTooltipManager.MouseLeaveTooltipArea();
    }

    private static void systemToolTipZoneLeave(object sender, EventArgs e)
    {
      CustomTooltipManager.MouseLeaveTooltipArea();
    }

    private static CustomTooltipManager.ToolTipZoneControlChild findZoneControl(object parent)
    {
      Control control1 = (Control) parent;
      if (control1 != null)
      {
        foreach (Control control2 in (ArrangedElementCollection) control1.Controls)
        {
          if (control2.GetType() == typeof (CustomTooltipManager.ToolTipZoneControlChild))
            return (CustomTooltipManager.ToolTipZoneControlChild) control2;
        }
      }
      return (CustomTooltipManager.ToolTipZoneControlChild) null;
    }

    public static void addTooltipZonesToTabControl(
      TabControl control,
      CustomTooltipManager.ToolTipZone[] zones)
    {
      CustomTooltipManager.ToolTipZoneTabChild zoneControlInTab = CustomTooltipManager.findZoneControlInTab((object) control);
      if (zoneControlInTab != null)
      {
        control.Controls.Remove((Control) zoneControlInTab);
      }
      else
      {
        control.MouseLeave += new EventHandler(CustomTooltipManager.systemToolTipZoneTabLeave);
        control.MouseEnter += new EventHandler(CustomTooltipManager.systemToolTipZoneTabEnter);
        control.MouseMove += new MouseEventHandler(CustomTooltipManager.systemToolTipZoneTabMove);
      }
      CustomTooltipManager.ToolTipZoneTabChild toolTipZoneTabChild = new CustomTooltipManager.ToolTipZoneTabChild();
      toolTipZoneTabChild.Visible = false;
      toolTipZoneTabChild.zones = zones;
      control.Controls.Add((Control) toolTipZoneTabChild);
    }

    private static void systemToolTipZoneTabMove(object sender, MouseEventArgs e)
    {
      CustomTooltipManager.systemToolTipZoneTabEnter(sender, (EventArgs) e);
    }

    private static void systemToolTipZoneTabEnter(object sender, EventArgs e)
    {
      CustomTooltipManager.ToolTipZoneTabChild zoneControlInTab = CustomTooltipManager.findZoneControlInTab(sender);
      if (zoneControlInTab != null)
      {
        Point point = new Point(Cursor.Position.X, Cursor.Position.Y);
        Point screen = zoneControlInTab.PointToScreen(((Control) sender).Location);
        Point pt = new Point(point.X - screen.X, point.Y - screen.Y);
        foreach (CustomTooltipManager.ToolTipZone zone in zoneControlInTab.zones)
        {
          if (zone.rect.Contains(pt))
          {
            if ((zone.relatedControl == null || zone.relatedControl.Visible) && (zone.relatedCSDControl == null || zone.relatedCSDControl.Visible))
            {
              CustomTooltipManager.MouseEnterTooltipArea(zone.ID);
              return;
            }
            break;
          }
        }
      }
      CustomTooltipManager.MouseLeaveTooltipArea();
    }

    private static void systemToolTipZoneTabLeave(object sender, EventArgs e)
    {
      CustomTooltipManager.MouseLeaveTooltipArea();
    }

    private static CustomTooltipManager.ToolTipZoneTabChild findZoneControlInTab(object parent)
    {
      Control control1 = (Control) parent;
      if (control1 != null)
      {
        foreach (Control control2 in (ArrangedElementCollection) control1.Controls)
        {
          if (control2.GetType() == typeof (CustomTooltipManager.ToolTipZoneTabChild))
            return (CustomTooltipManager.ToolTipZoneTabChild) control2;
        }
      }
      return (CustomTooltipManager.ToolTipZoneTabChild) null;
    }

    public static void runTooltips()
    {
      DateTime now = DateTime.Now;
      bool flag = false;
      if (CustomTooltipManager.currentOverTooltip >= 4300 && CustomTooltipManager.currentOverTooltip <= 4309)
      {
        flag = true;
        CustomTooltipManager.staticMouse = true;
      }
      if (!Program.mySettings.SETTINGS_instantTooltips && !flag)
      {
        Point point = new Point(Cursor.Position.X, Cursor.Position.Y);
        if (!point.Equals((object) CustomTooltipManager.lastMousePosition))
        {
          CustomTooltipManager.lastMousePosition = point;
          CustomTooltipManager.staticMouse = false;
          CustomTooltipManager.lastMouseMoveTime = now;
        }
        else if (!CustomTooltipManager.staticMouse && (now - CustomTooltipManager.lastMouseMoveTime).TotalMilliseconds > (double) Program.mySettings.SETTINGS_staticMouseTime)
          CustomTooltipManager.staticMouse = true;
      }
      int ID = 0;
      if (CustomTooltipManager.currentOverTooltip != 0)
        ID = CustomTooltipManager.currentOverTooltip;
      else if (CustomTooltipManager.lastOverTooltip != 0 && (now - CustomTooltipManager.lastLeaveTime).TotalMilliseconds > 100.0)
        CustomTooltipManager.lastOverTooltip = 0;
      if (ID != 0)
      {
        CustomTooltipManager.lastOverTooltip = ID;
        CustomTooltipManager.showToolTip(ID);
      }
      else
      {
        if (CustomTooltipManager.showingTooltip == 0 || CustomTooltipManager.showingTooltip == CustomTooltipManager.lastOverTooltip)
          return;
        CustomTooltipManager.showingTooltip = 0;
        InterfaceMgr.Instance.closeCustomTooltip();
      }
    }

    private static void showToolTip(int ID)
    {
      if ((!Program.mySettings.SETTINGS_instantTooltips && !CustomTooltipManager.staticMouse || CustomTooltipManager.showingTooltip != 0) && (ID == CustomTooltipManager.showingTooltip && CustomTooltipManager.currentOverData == CustomTooltipManager.currentOverLastData && !CustomTooltipManager.dynamicUpdateTooltips(ID) || CustomTooltipManager.showingTooltip == 0))
        return;
      Form currentParentWindow = CustomTooltipManager.currentParentWindow;
      CustomTooltipManager.currentOverLastData = CustomTooltipManager.currentOverData;
      CustomTooltipManager.showingTooltip = ID;
      string text = SK.Text("TOOLIPS_UNDEFINED", "Undefined Tooltip");
      switch (ID)
      {
        case 1:
          text = SK.Text("TOOLTIPS_MAINWINDOW_CARDS_SHOW_ALL_CARDS", "Show All Cards");
          if (GameEngine.Instance.cardsManager.UserCardData.premiumCard > 0)
          {
            double totalSeconds = GameEngine.Instance.cardsManager.UserCardData.premiumCardExpiry.Subtract(VillageMap.getCurrentServerTime()).TotalSeconds;
            double d1 = totalSeconds / 86400.0;
            double d2 = totalSeconds % 86400.0 / 3600.0;
            double d3 = totalSeconds % 3600.0 / 60.0;
            string[] strArray1 = new string[8]
            {
              text + Environment.NewLine + Environment.NewLine,
              SK.Text("TOOLTIPS_LOGOUT_PREMIUM", "Your Premium Token expires in : "),
              Math.Floor(d1).ToString().PadLeft(2, '0'),
              ":",
              null,
              null,
              null,
              null
            };
            string[] strArray2 = strArray1;
            double num = Math.Floor(d2);
            string str1 = num.ToString().PadLeft(2, '0');
            strArray2[4] = str1;
            strArray1[5] = ":";
            string[] strArray3 = strArray1;
            num = Math.Floor(d3);
            string str2 = num.ToString().PadLeft(2, '0');
            strArray3[6] = str2;
            strArray1[7] = " (dd:hh:mm)";
            text = string.Concat(strArray1);
            break;
          }
          break;
        case 2:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_USERNAME", "Your Username");
          break;
        case 3:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_RANKING", "Your Current Rank");
          break;
        case 4:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_HONOUR", "Your Current Honour Level");
          break;
        case 5:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_GOLD", "Your Current Gold Level");
          break;
        case 6:
          if (GameEngine.Instance.LocalWorldData.EraWorld)
          {
            NumberFormatInfo nfi = GameEngine.NFI;
            int index = GameEngine.Instance.World.getRank();
            if (index < 0)
              index = 0;
            else if (index >= VillageBuildingsData.faithPointCap_EraWorlds.Length)
              index = VillageBuildingsData.faithPointCap_EraWorlds.Length - 1;
            int pointCapEraWorld = VillageBuildingsData.faithPointCap_EraWorlds[index];
            text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_FAITHPOINTS_CAP", "Your Current Faith Point Cap") + " : " + pointCapEraWorld.ToString("N", (IFormatProvider) nfi);
            break;
          }
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_FAITHPOINTS", "Your Current Faith Points");
          break;
        case 7:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_POINTS", "Your Current Points");
          break;
        case 8:
          text = GameEngine.Instance.LocalWorldData.EraWorld ? SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_SECOND_ERA", "This World is in its Second Era. Click here for more details") : SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_SECOND_AGE", "This World is in its Second Age. Click here for more details");
          break;
        case 9:
          text = GameEngine.Instance.LocalWorldData.EraWorld ? SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_THIRD_ERA", "This World is in its Third Era. Click here for more details") : SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_THIRD_AGE", "This World is in its Third Age. Click here for more details");
          break;
        case 10:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_DOMINATION_WORLD", "This World uses the Domination World Rules. Click here for more details");
          break;
        case 11:
          if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
          {
            TimeSpan dominationTimeLeft = GameEngine.Instance.getDominationTimeLeft();
            int totalSeconds = (int) dominationTimeLeft.TotalSeconds;
            int num1 = totalSeconds % 60;
            int num2 = totalSeconds / 60 % 60;
            int num3 = totalSeconds / 3600 % 24;
            int num4 = totalSeconds / 86400;
            if (dominationTimeLeft.TotalHours >= 24.0)
            {
              string str3 = num4.ToString() + SK.Text("VillageMap_Day_Abbrev", "d") + ":";
              string str4 = num3 != 0 ? (num3 >= 10 ? str3 + num3.ToString() + ":" : str3 + "0" + num3.ToString() + ":") : str3 + "00:";
              string str5 = num2 != 0 ? (num2 >= 10 ? str4 + num2.ToString() + ":" : str4 + "0" + num2.ToString() + ":") : str4 + "00:";
              string str6 = num1 != 0 ? (num1 >= 10 ? str5 + num1.ToString() : str5 + "0" + num1.ToString()) : str5 + "00";
              text = SK.Text("Dom_Time_Left", "Time Remaining") + " " + str6;
              break;
            }
            string str7 = "";
            string str8 = num3 != 0 ? (num3 >= 10 ? str7 + num3.ToString() + ":" : str7 + "0" + num3.ToString() + ":") : str7 + "00:";
            string str9 = num2 != 0 ? (num2 >= 10 ? str8 + num2.ToString() + ":" : str8 + "0" + num2.ToString() + ":") : str8 + "00:";
            string str10 = num1 != 0 ? (num1 >= 10 ? str9 + num1.ToString() : str9 + "0" + num1.ToString()) : str9 + "00";
            text = SK.Text("Dom_Time_Left", "Time Remaining") + " " + str10;
            break;
          }
          break;
        case 12:
          text = GameEngine.Instance.LocalWorldData.EraWorld ? SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_FOURTH_ERA", "This World is in its Fourth Era. Click here for more details") : SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_FOURTH_AGE", "This World is in its Fourth Age. Click here for more details");
          break;
        case 13:
          text = GameEngine.Instance.LocalWorldData.EraWorld ? SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_FIFTH_ERA", "This World is in its Fifth Era. Click here for more details") : SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_FIFTH_AGE", "This World is in its Fifth Age. Click here for more details");
          break;
        case 14:
          text = GameEngine.Instance.LocalWorldData.EraWorld ? SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_SIXTH_ERA", "This World is in its Sixth Era. Click here for more details") : SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_SIXTH_AGE", "This World is in its Sixth Age. Click here for more details");
          break;
        case 15:
          text = GameEngine.Instance.LocalWorldData.EraWorld ? SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_FINAL_ERA", "This World is in its Final Era. Click here for more details") : SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_SEVENTH_AGE", "This World is in its Final Age. Click here for more details");
          break;
        case 16:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_AI_WORLD", "This World uses the AI World Rules. Click here for more details");
          break;
        case 20:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_SCROLL", "Click to Scroll Through Your Villages");
          break;
        case 21:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_LIST", "Click to View a List of Your Villages");
          break;
        case 22:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_WORLDMAP", "Click to View the Map");
          break;
        case 23:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_VILLAGEMAP", "Click to View Your Village");
          break;
        case 24:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_RESEARCH", "Click for Research");
          break;
        case 25:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_RANKING", "Click to Upgrade Your Rank");
          break;
        case 26:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_ATTACKS", "Click to View Attacks");
          break;
        case 27:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_REPORTS", "Click to View Reports");
          break;
        case 28:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_MAIL", "Click to View your Mail");
          break;
        case 29:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_LEADERBOARD", "Click to View the Leaderboards");
          break;
        case 30:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_FACTIONS", "Click to View your Faction and House");
          break;
        case 31:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_QUESTS", "Click to View your Current Quests");
          break;
        case 32:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_CAPITAL", "Click to View the Parish Capital");
          break;
        case 33:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_PREMIUM_VO", "Click to View the Premium Village Overview Screen");
          break;
        case 34:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_TOURNEY", "Click to View the Active Tourney");
          break;
        case 40:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_VILLAGE", "Click to View Your Village Map");
          break;
        case 41:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CASTLE", "Click to View Your Castle Map");
          break;
        case 42:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_RESOURCES", "Click to View Your Resources");
          break;
        case 43:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_TRADING", "Click to Trade");
          break;
        case 44:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_TROOPS", "Click to Make Troops");
          break;
        case 45:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_UNITS", "Click to Make Units");
          break;
        case 46:
          text = "Coming Soon";
          break;
        case 47:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_BANQUETING", "Click to Hold a Banquet");
          break;
        case 48:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_VASSALS", "Click to View Manage Your Vassals");
          break;
        case 49:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CAPITAL_INFO", "Click to View the Capital Info");
          break;
        case 50:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CAPITAL_VOTE", "Click to Vote");
          break;
        case 51:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CAPITAL_FORUM", "Click to View the Capital's Forum");
          break;
        case 52:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_NOT_IMPLEMENTED_YET", "Not Implemented Yet");
          break;
        case 53:
          text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_CHAT", "Click to Chat");
          break;
        case 90:
          text = GameEngine.Instance.World.getVillageNameOrType(CustomTooltipManager.currentOverData);
          break;
        case 91:
          text = SK.Text("MapFilterSelectPanel_Map_Filtering", "Map Filtering");
          break;
        case 92:
          text = SK.Text("GENERIC_Close", "Close");
          break;
        case 93:
          text = SK.Text("MapFilterSelectPanel_Filter_Active", "Filter Active");
          break;
        case 94:
          text = SK.Text("Attack_Targets", "Attack Targets");
          break;
        case 100:
          if (CustomTooltipManager.currentOverData < 1000)
          {
            int num = 0;
            bool flag = true;
            if (GameEngine.Instance.Village != null && GameEngine.Instance.World.isCapital(GameEngine.Instance.Village.VillageID))
            {
              VillageMap village = GameEngine.Instance.Village;
              if (village.m_parishCapitalResearchData != null)
              {
                num = village.m_parishCapitalResearchData.getCapitalResourceFromBuildingType(CustomTooltipManager.currentOverData);
                if (num > 0)
                  flag = false;
              }
            }
            if (flag)
            {
              text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_ROLLOVER", "Click to Place") + " : " + VillageBuildingsData.getBuildingName(CustomTooltipManager.currentOverData);
              break;
            }
            text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_ROLLOVER", "Click to Place") + " : " + VillageBuildingsData.getBuildingName(CustomTooltipManager.currentOverData) + " - " + SK.Text("VillageMapPanel_Current_Level", "Current Level") + " " + num.ToString();
            break;
          }
          switch (CustomTooltipManager.currentOverData)
          {
            case 1000:
              text = SK.Text("TOOLTIPS_SUBMENU_RELIGIOUS", "Click to Open Religious Sub Menu");
              break;
            case 1001:
              text = SK.Text("TOOLTIPS_SUBMENU_DECORATIVE", "Click to Open Decorative Sub Menu");
              break;
            case 1002:
              text = SK.Text("TOOLTIPS_SUBMENU_JUSTICE", "Click to Open Justice Sub Menu");
              break;
            case 1003:
              text = SK.Text("TOOLTIPS_SUBMENU_ENTERTAINMENT", "Click to Open Entertainment Sub Menu");
              break;
            case 1004:
              text = SK.Text("TOOLTIPS_SUBMENU_SMALL_SHRINE", "Click to Open Small Shrine Sub Menu");
              break;
            case 1005:
              text = SK.Text("TOOLTIPS_SUBMENU_LARGE_SHRINE", "Click to Open Large Shrine Sub Menu");
              break;
            case 1006:
              text = SK.Text("TOOLTIPS_SUBMENU_SMALL_GARDEN", "Click to Open Formal Garden Sub Menu");
              break;
            case 1007:
              text = "";
              break;
            case 1008:
              text = SK.Text("TOOLTIPS_SUBMENU_LARGE_GARDEN", "Click to Open Flower Bed Sub Menu");
              break;
            case 1009:
              text = "";
              break;
            case 1010:
              text = SK.Text("TOOLTIPS_SUBMENU_SMALL_STATUE", "Click to Open Gilded Statue Sub Menu");
              break;
            case 1111:
              text = SK.Text("TOOLTIPS_SUBMENU_LARGE_STATUE", "Click to Open Stone Statue Sub Menu");
              break;
            case 1112:
              text = SK.Text("TOOLTIPS_SUBMENU_CAPITAL_RESOURCE", "Click to Open Resources Sub Menu");
              break;
            case 1113:
              text = SK.Text("TOOLTIPS_SUBMENU_CAPITAL_FOOD", "Click to Open Food Sub Menu");
              break;
            case 1114:
              text = SK.Text("TOOLTIPS_SUBMENU_CAPITAL_BANQUET", "Click to Open Banquet Sub Menu");
              break;
            case 1115:
              text = SK.Text("TOOLTIPS_SUBMENU_CAPITAL_WEAPONS", "Click to Open Weapons Sub Menu");
              break;
            case 1116:
              text = SK.Text("TOOLTIPS_SUBMENU_CAPITAL_BANQUET_FOOD", "Click to Open Banquet Sub Menu");
              break;
            case 2000:
              text = SK.Text("TOOLTIPS_SUBMENU_BACK", "Click to go Back");
              break;
          }
          break;
        case 101:
          text = CustomTooltipManager.currentOverData != 0 ? SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_1_CLOSE", "Click to Close Town Buildings Tab") : SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_1_OPEN", "Click to Open Town Buildings Tab");
          break;
        case 102:
          text = CustomTooltipManager.currentOverData != 0 ? SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_2_CLOSE", "Click to Close Industry Buildings Tab") : SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_2_OPEN", "Click to Open Industry Buildings Tab");
          break;
        case 103:
          text = CustomTooltipManager.currentOverData != 0 ? SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_3_CLOSE", "Click to Close Farm Buildings Tab") : SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_3_OPEN", "Click to Open Farm Buildings Tab");
          break;
        case 104:
          text = CustomTooltipManager.currentOverData != 0 ? SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_4_CLOSE", "Click to Close Weapon Production Buildings Tab") : SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_4_OPEN", "Click to Open Weapon Production Buildings Tab");
          break;
        case 105:
          text = CustomTooltipManager.currentOverData != 0 ? SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_5_CLOSE", "Click to Close Banqueting Resource Buildings Tab") : SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_5_OPEN", "Click to Open Banqueting Resource Buildings Tab");
          break;
        case 106:
          text = CustomTooltipManager.currentOverData != 0 ? SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_1_CLOSE", "Click to Close Castle Buildings Tab") : SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_1_OPEN", "Click to Open Castle Buildings Tab");
          break;
        case 107:
          text = CustomTooltipManager.currentOverData != 0 ? SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_2_CLOSE", "Click to Close Army Buildings Tab") : SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_2_OPEN", "Click to Open Army Buildings Tab");
          break;
        case 108:
          text = CustomTooltipManager.currentOverData != 0 ? SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_3_CLOSE", "Click to Close Civil Buildings Tab") : SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_3_OPEN", "Click to Open Civil Buildings Tab");
          break;
        case 109:
          text = CustomTooltipManager.currentOverData != 0 ? SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_4_CLOSE", "Click to Close Guild Buildings Tab") : SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_4_OPEN", "Click to Open Guild Buildings Tab");
          break;
        case 110:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_EXTRAS_BAR", "Click to View Extra Info Bars");
          break;
        case 111:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_HONOUR_BAR", "Click to View Popularity To Honour Information");
          break;
        case 112:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_HONOUR_BAR_CLOSE", "Click to Close Popularity To Honour Information");
          break;
        case 113:
          string str11 = "";
          if (CustomTooltipManager.currentOverData >= 0)
            str11 = VillageBuildingsData.getBuildingName(CustomTooltipManager.currentOverData) + Environment.NewLine + Environment.NewLine;
          text = str11 + SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_IN_BUILDING_CLOSE", "Click to Close the Selected Building Information");
          break;
        case 114:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_MOVE_BUILDING", "Click to Move the Selected Building");
          break;
        case 115:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_TAX_INC", "Click to Increase Your Tax Rate");
          break;
        case 116:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_TAX_DEC", "Click to Decrease Your Tax Rate");
          break;
        case 117:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_RATIONS_INC", "Click to Increase Your Rations");
          break;
        case 118:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_RATIONS_DEC", "Click to Decrease Your Rations");
          break;
        case 119:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_ALE_INC", "Click to Increase Your Ale Rations");
          break;
        case 120:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_ALE_DEC", "Click to Decrease Your Ale Rations");
          break;
        case 121:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_POPULARITY_BAR", "Click to View Popularity Information");
          break;
        case 122:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_POPULARITY_BAR_CLOSE", "Click to Close Popularity Information");
          break;
        case 123:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_TAX_OPEN", "Click to View Detailed Tax Information");
          break;
        case 124:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_TAX_CLOSE", "Click to Close Detailed Tax Information");
          break;
        case 125:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_RATIONS_OPEN", "Click to View Detailed Rations Information");
          break;
        case 126:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_RATIONS_CLOSE", "Click to Close Detailed Rations Information");
          break;
        case (int) sbyte.MaxValue:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_ALE_OPEN", "Click to View Detailed Ale Rations Information");
          break;
        case 128:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_ALE_CLOSE", "Click to Close Detailed Ale Rations Information");
          break;
        case 129:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_HOUSING_OPEN", "Click to View Detailed Housing Information");
          break;
        case 130:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_HOUSING_CLOSE", "Click to Close Detailed Housing Information");
          break;
        case 131:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_ENTERTAINMENT_OPEN", "Click to View Detailed Buildings Information");
          break;
        case 132:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_ENTERTAINMENT_CLOSE", "Click to Close Detailed Buildings Information");
          break;
        case 133:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_EVENTS_OPEN", "Click to View Detailed Events Information");
          break;
        case 134:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_EVENTS_CLOSE", "Click to Close Detailed Events Information");
          break;
        case 140:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILD_INFO", "Time and Cost to Build");
          break;
        case 141:
          text = "";
          break;
        case 142:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_INFO_BAR_WOOD", "Current Wood Level");
          break;
        case 143:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_INFO_BAR_STONE", "Current Stone Level");
          break;
        case 144:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_INFO_BAR_FOOD", "Current Food Level");
          break;
        case 145:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_INFO_BAR_CAPITAL_GOLD", "Current Capital Gold Level");
          break;
        case 146:
          text = SK.Text("TOOLTIPS_VILLAGEMAP_INFO_BAR_CAPITAL_FLAGS", "Current Flags");
          break;
        case 147:
          text = SK.Text("VILLAGEMAP_INFO_BAR_DONATION_TYPE", "Type of Goods Needed to Upgrade");
          break;
        case 148:
          text = SK.Text("TOOLTIPS_VO_Pitch", "Current Pitch Level");
          break;
        case 149:
          text = SK.Text("TOOLTIPS_VO_Iron", "Current Iron Level");
          break;
        case 150:
          text = VillageBuildingsData.getBuildingName(CustomTooltipManager.currentOverData) + " : " + SK.Text("VILLAGEMAP_CAPITAL_OVER_COMPLETED", "Fully Upgraded");
          break;
        case 151:
          text = VillageBuildingsData.getBuildingName(CustomTooltipManager.currentOverData) + " : " + SK.Text("VILLAGEMAP_CAPITAL_OVER_NOTCOMPLETED", "Upgrades available");
          break;
        case 200:
          if (CustomTooltipManager.currentOverData < 1000)
          {
            text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_ROLLOVER", "Click to Place : ") + CastlesCommon.getPieceName(CustomTooltipManager.currentOverData);
            if (CustomTooltipManager.currentOverData == 38 || CustomTooltipManager.currentOverData == 37 || CustomTooltipManager.currentOverData == 40 || CustomTooltipManager.currentOverData == 39)
            {
              text = text + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_ROLLOVER_HELP", "Use Mouse Wheel or Spacebar to rotate.");
              break;
            }
            break;
          }
          break;
        case 201:
          text = CustomTooltipManager.currentOverData != 0 ? SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_1_CLOSE", "Click to Close Troops Tab") : SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_1_OPEN", "Click to Open Troops Tab");
          break;
        case 202:
          text = CustomTooltipManager.currentOverData != 0 ? SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_2_CLOSE", "Click to Close Wood Tab") : SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_2_OPEN", "Click to Open Wood Tab");
          break;
        case 203:
          text = CustomTooltipManager.currentOverData != 0 ? SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_3_CLOSE", "Click to Close Stone Tab") : SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_3_OPEN", "Click to Open Stone Tab");
          break;
        case 204:
          text = CustomTooltipManager.currentOverData != 0 ? SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_4_CLOSE", "Click to Close Buildings Tab") : SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_4_OPEN", "Click to Open Buildings Tab");
          break;
        case 205:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_TOGGLE_REINFORCEMENTS_ON", "Click to Place Reinforcements");
          break;
        case 206:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_TOGGLE_REINFORCEMENTS_OFF", "Click to Place Your Troops");
          break;
        case 207:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_1X1", "Click to Change Placement Pattern : 1x1");
          break;
        case 208:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_3X3", "Click to Change Placement Pattern : 3x3");
          break;
        case 209:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_5X5", "Click to Change Placement Pattern : 5x5");
          break;
        case 210:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_1X5", "Click to Change Placement Pattern : 1x5");
          break;
        case 211:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_TOGGLE_VIEWMODE_HIGH", "Click to View Castle in Full Mode");
          break;
        case 212:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_TOGGLE_VIEWMODE_LOW", "Click to View Castle in Collapsed Mode");
          break;
        case 213:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_TOGGLE_DELETE_ON", "Click to Start Delete Mode");
          break;
        case 214:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_TOGGLE_DELETE_OFF", "Click to Stop Delete Mode");
          break;
        case 215:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_CASTLE_CONSTRUCTION_OPTIONS_OPEN", "Click to View Castle Construction Options");
          break;
        case 216:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_CASTLE_CONSTRUCTION_OPTIONS_CLOSE", "Click to Close Castle Construction Options");
          break;
        case 217:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_REPAIR", "Click to Repair all damaged castle infrastructure.");
          break;
        case 218:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_DELETE_CONSTRUCTING", "Click to Delete all castle structures currently under construction");
          break;
        case 219:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_CONFIRM", "Click to Upload your castle changes to the server");
          break;
        case 220:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_CANCEL", "Click to Cancel all unconfirmed changes");
          break;
        case 221:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_DELETE_TROOPS", "Remove Placed Troops") + " : ";
          switch (CustomTooltipManager.currentOverData)
          {
            case 70:
              text += SK.Text("GENERIC_Peasants", "Peasants");
              break;
            case 71:
              text += SK.Text("GENERIC_Swordsmen", "Swordsmen");
              break;
            case 72:
              text += SK.Text("GENERIC_Archers", "Archers");
              break;
            case 73:
              text += SK.Text("GENERIC_Pikemen", "Pikemen");
              break;
            case 85:
              text += SK.Text("GENERIC_Captains", "Captains");
              break;
          }
          break;
        case 222:
          text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_TOGGLE_AGGRESSIVE", "Toggle Aggressive Defender State");
          break;
        case 223:
          text = CustomTooltipManager.currentOverData != 0 ? SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_5_CLOSE", "Click to Close Parish Unlocked Structures Tab") : SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_5_OPEN", "Click to Open Parish Unlocked Structures Tab");
          break;
        case 224:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_MEMORISE", "Memorise Preset");
          break;
        case 225:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_DEPLOY", "Deploy Preset");
          break;
        case 226:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_RENAME", "Rename Preset");
          break;
        case 227:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_DELETE", "Delete Preset");
          break;
        case 228:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_DETAILS", "View Details");
          break;
        case 229:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_RETURN", "Return to Presets");
          break;
        case 230:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_DEFENCE_REQ", "Required Defence Research Level");
          if (CustomTooltipManager.currentOverData == 0)
          {
            text = text + " " + SK.Text("TOOLTIPS_CASTLEMAP_PRESET_REQ_NOT_MET", "(Not Met)");
            break;
          }
          break;
        case 231:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_FORTIFICATION_REQ", "Required Fortification Research Level");
          if (CustomTooltipManager.currentOverData == 0)
          {
            text = text + " " + SK.Text("TOOLTIPS_CASTLEMAP_PRESET_REQ_NOT_MET", "(Not Met)");
            break;
          }
          break;
        case 232:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_PARISH_REQ", "Parish Buildings Required");
          if (CustomTooltipManager.currentOverData == 0)
          {
            text = text + " " + SK.Text("TOOLTIPS_CASTLEMAP_PRESET_REQ_NOT_MET", "(Not Met)");
            break;
          }
          break;
        case 233:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_WOOD", "Wood Required");
          if (CustomTooltipManager.currentOverData == 0)
          {
            text = text + " " + SK.Text("TOOLTIPS_CASTLEMAP_PRESET_REQ_NOT_MET", "(Not Met)");
            break;
          }
          break;
        case 234:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_STONE", "Stone Required");
          if (CustomTooltipManager.currentOverData == 0)
          {
            text = text + " " + SK.Text("TOOLTIPS_CASTLEMAP_PRESET_REQ_NOT_MET", "(Not Met)");
            break;
          }
          break;
        case 235:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_IRON", "Iron Required");
          if (CustomTooltipManager.currentOverData == 0)
          {
            text = text + " " + SK.Text("TOOLTIPS_CASTLEMAP_PRESET_REQ_NOT_MET", "(Not Met)");
            break;
          }
          break;
        case 236:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_PITCH", "Pitch Required");
          if (CustomTooltipManager.currentOverData == 0)
          {
            text = text + " " + SK.Text("TOOLTIPS_CASTLEMAP_PRESET_REQ_NOT_MET", "(Not Met)");
            break;
          }
          break;
        case 237:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_GOLD", "Gold Required");
          if (CustomTooltipManager.currentOverData == 0)
          {
            text = text + " " + SK.Text("TOOLTIPS_CASTLEMAP_PRESET_REQ_NOT_MET", "(Not Met)");
            break;
          }
          break;
        case 238:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_TIME", "Total Construction Time");
          break;
        case 240:
          text = SK.Text("TOOLTIPS_CASTLEMAP_PRESET_PREVIEW", "Toggle Preview");
          break;
        case 300:
          text = SK.Text("TOOLTIPS_RESEARCHTREE_LIST_MODE", "Click to View a List of Available Researches");
          break;
        case 301:
          text = SK.Text("TOOLTIPS_RESEARCHTREE_TREE_MODE", "Click to View the Research Tree");
          break;
        case 302:
          ResearchData userResearchData = GameEngine.Instance.World.UserResearchData;
          if (userResearchData != null && userResearchData.research_queueEntries != null && CustomTooltipManager.currentOverData < userResearchData.research_queueEntries.Length)
          {
            string str12 = ResearchData.getResearchName(userResearchData.research_queueEntries[CustomTooltipManager.currentOverData]) + Environment.NewLine + Environment.NewLine;
            DateTime currentServerTime = VillageMap.getCurrentServerTime();
            TimeSpan timeSpan1 = userResearchData.research_completionTime - currentServerTime;
            for (int index = 0; index < CustomTooltipManager.currentOverData + 1; ++index)
            {
              TimeSpan timeSpan2 = userResearchData.calcResearchTime(userResearchData.research_pointCount + index, GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData);
              if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
                timeSpan2 = new TimeSpan(timeSpan2.Ticks / 2L);
              timeSpan1 += timeSpan2;
            }
            text = str12 + SK.Text("Research_Completed_In", "Completed In") + " : " + VillageMap.createBuildTimeString((int) timeSpan1.TotalSeconds);
            break;
          }
          break;
        case 400:
          text = SK.Text("TOOLTIPS_RANKING_UPGRADE", "Click to Upgrade your Rank");
          break;
        case 401:
          text = Rankings.getRankingName(CustomTooltipManager.currentOverData, RemoteServices.Instance.UserAvatar.male) + " (" + (CustomTooltipManager.currentOverData + 1).ToString() + ")";
          break;
        case 500:
          text = SK.Text("TOOLTIPS_MAILSCREEN_FLOAT", "Click to Detach Mail Screen");
          break;
        case 501:
          text = SK.Text("TOOLTIPS_MAILSCREEN_DOCK", "Click to Dock the Mail Screen");
          break;
        case 502:
          text = SK.Text("TOOLTIPS_MAILSCREEN_CLOSE", "Click to Close");
          break;
        case 503:
          text = SK.Text("TOOLTIPS_MAILSCREEN_REPORT", "This is for reporting offensive language or personal abuse only");
          break;
        case 504:
          text = SK.Text("TOOLTIPS_MAILSCREEN_AGGRESSIVE_BLOCK", "Aggressive Mail Block, blocks from view any mail thread that contains someone on your block list. Normal mail block only removes threads that only contain users that are in your block list.");
          break;
        case 505:
          text = SK.Text("TOOLTIPS_MAIL_SEARCH", "Search For User");
          break;
        case 506:
          text = SK.Text("TOOLTIPS_MAIL_RECENT", "Recent Recipients");
          break;
        case 507:
          text = SK.Text("TOOLTIPS_MAIL_FAVOURITES", "Favourites");
          break;
        case 508:
          text = SK.Text("TOOLTIPS_MAIL_OTHERS_KNOWN", "Faction Members and Personal Allies");
          break;
        case 511:
          text = SK.Text("TOOLTIPS_MAIL_SELECT_VILLAGE", "Select Village");
          break;
        case 512:
          text = SK.Text("TOOLTIPS_MAIL_SEARCH_REGION", "Search for Parish");
          break;
        case 513:
          text = SK.Text("TOOLTIPS_MAIL_CURRENT_ATTACHMENTS", "Attached Targets");
          break;
        case 514:
          text = SK.Text("TOOLTIPS_MAIL_OPEN_ATTACHMENTS", "Open Targets");
          break;
        case 515:
          text = SK.Text("TOOLTIPS_MAIL_PLAYER_LINK", "Link to Player");
          break;
        case 516:
          text = SK.Text("TOOLTIPS_MAIL_VILLAGE_LINK", "Link to Village");
          break;
        case 517:
          text = SK.Text("TOOLTIPS_MAIL_PARISH_LINK", "Link to Parish");
          break;
        case 518:
          text = SK.Text("TOOLTIPS_VILLAGE_SEARCH_DISABLED", "Select a player by searching to view their villages");
          break;
        case 600:
          text = SK.Text("TOOLTIPS_BARRACKS_DISBAND", "Click for Disband Options");
          break;
        case 601:
          text = SK.Text("TOOLTIPS_BARRACKS_CLOSE", "Click to Close");
          break;
        case 602:
          text = SK.Text("GENERIC_Armed_Peasants", "Armed Peasants");
          break;
        case 603:
          text = SK.Text("GENERIC_Archers", "Archers");
          break;
        case 604:
          text = SK.Text("GENERIC_Pikemen", "Pikemen");
          break;
        case 605:
          text = SK.Text("GENERIC_Swordsmen", "Swordsmen");
          break;
        case 606:
          text = SK.Text("GENERIC_Catapults", "Catapults");
          break;
        case 607:
          text = SK.Text("GENERIC_Captains", "Captains");
          break;
        case 608:
          text = SK.Text("TOOLTIPS_BARRACKS_ARCHERS_NOT_RESEARCHED", "To recruit Archers you must be Rank 6 and research 'Long Bows'.");
          break;
        case 609:
          text = SK.Text("TOOLTIPS_BARRACKS_PIKEMEN_NOT_RESEARCHED", "To recruit Pikemen you must be Rank 11 and research 'Pike'.");
          break;
        case 610:
          text = SK.Text("TOOLTIPS_BARRACKS_SWORDSMEN_NOT_RESEARCHED", "To recruit Swordsmen you must be Rank 13 and research 'Sword'.");
          break;
        case 611:
          text = SK.Text("TOOLTIPS_BARRACKS_CATAPULTS_NOT_RESEARCHED", "To recruit Catapults you must be Rank 15 and research 'Catapult'.");
          break;
        case 612:
          text = SK.Text("TOOLTIPS_BARRACKS_CAPTAINS_NOT_RESEARCHED", "To recruit Captains you must be Rank 12 and research 'Leadership' and 'Captains'.");
          break;
        case 700:
          text = SK.Text("TOOLTIPS_UNITS_DISBAND", "Click for Disband Options");
          break;
        case 701:
          text = SK.Text("TOOLTIPS_UNITS_CLOSE", "Click to Close");
          break;
        case 702:
          text = SK.Text("TOOLTIPS_UNITS_SPACE_REQUIRED", "Unit Space Required") + " : " + CustomTooltipManager.currentOverData.ToString();
          break;
        case 703:
          text = SK.Text("GENERIC_Merchants", "Merchants");
          break;
        case 704:
          text = SK.Text("GENERIC_Monks", "Monks");
          break;
        case 705:
          text = SK.Text("GENERIC_Scouts", "Scouts");
          break;
        case 706:
          text = SK.Text("TOOLTIPS_UNITS_MERCHANTS_NOT_RESEARCHED", "To recruit Merchants you must be Rank 5, research 'Merchant Guilds' and build a Market.");
          break;
        case 707:
          text = SK.Text("TOOLTIPS_UNITS_MONKS_NOT_RESEARCHED", "To recruit Monks you must be Rank 8 and research 'Ordination'.");
          break;
        case 708:
          text = SK.Text("TOOLTIPS_UNITS_SCOUTS_NOT_RESEARCHED", "To recruit Scouts you must research 'Scouts'.");
          break;
        case 800:
          text = SK.Text("TOOLTIPS_STOCKEXCHANGE_CLOSE", "Click to Close");
          break;
        case 801:
          text = SK.Text("TOOLTIPS_TRADE_CLOSE", "Click to Close");
          break;
        case 802:
          text = SK.Text("TOOLTIPS_TRADE_RESOURCES", "Click to Trade Resources");
          break;
        case 803:
          text = SK.Text("TOOLTIPS_TRADE_FOOD", "Click to Trade Food");
          break;
        case 804:
          text = SK.Text("TOOLTIPS_TRADE_WEAPONS", "Click to Trade Weapons");
          break;
        case 805:
          text = SK.Text("TOOLTIPS_TRADE_BANQUETING", "Click to Trade Banqueting Goods");
          break;
        case 806:
          text = SK.Text("TOOLTIPS_TOGGLE_TO_STOCKEXCHANGE", "Click for Stock Exchange Trading");
          break;
        case 807:
          text = SK.Text("TOOLTIPS_TOGGLE_TO_TRADING", "Click for Village to Village Trading");
          break;
        case 808:
          text = SK.Text("TOOLTIPS_REMOVE_FAVOURITE", "Click here to remove this Stock Exchange from your Favourites");
          break;
        case 809:
          text = SK.Text("TOOLTIPS_ADD_FAVOURITE", "Click here to add this Stock Exchange to your Favourites");
          break;
        case 810:
          text = SK.Text("TOOLTIPS_REMOVE_RECENT", "Click here to remove this Stock Exchange from your recent Stock Exchanges");
          break;
        case 811:
          text = SK.Text("TOOLTIPS_REMOVE_FAVOURITE_MARKET", "Click here to remove this Village from your Favourites");
          break;
        case 812:
          text = SK.Text("TOOLTIPS_ADD_FAVOURITE_MARKET", "Click here to add this Village to your Favourites");
          break;
        case 813:
          text = SK.Text("TOOLTIPS_REMOVE_RECENT_MARKET", "Click here to remove this Village from your Recent List");
          break;
        case 814:
          text = SK.Text("TOOLTIPS_FIND_HIGHEST_PRICE", "Find the highest price for this item in the 20 closest Stock Exchanges. (Premium Token in play Required)");
          break;
        case 815:
          text = SK.Text("TOOLTIPS_FIND_LOWEST_PRICE", "Find the lowest price for this item in the 20 closest Stock Exchanges. (Premium Token in play Required)");
          break;
        case 900:
          text = SK.Text("TOOLTIPS_RESOURCES_CLOSE", "Click to Close");
          break;
        case 901:
          text = SK.Text("TOOLTIPS_RESOURCES_PRODUCTION_INFO", "Click for Production Information");
          break;
        case 1000:
          text = SK.Text("TOOLTIPS_BANQUET_CLOSE", "Click to Close");
          break;
        case 1100:
          text = SK.Text("TOOLTIPS_AREASELECT_OVER_TAG", "Click to Select this county as your starting area");
          break;
        case 1101:
          text = SK.Text("TOOLTIPS_AREASELECT_OVER_TAG_FULL", "This County is Full");
          break;
        case 1200:
          text = SK.Text("TOOLTIPS_MENUBAR_CONVERT", "This allows you to change your village type to another, all buildings are lost but all resources, units and your castle are kept.");
          break;
        case 1201:
          text = SK.Text("TOOLTIPS_MENUBAR_ABANDON", "This allows you to disown the selected village, allowing you to make a new village elsewhere, all buildings, resources, units and your castle are lost.");
          break;
        case 1300:
          text = StatsPanel.getCategoryTitle(CustomTooltipManager.currentOverData) + " : " + StatsPanel.getCategoryDescription(CustomTooltipManager.currentOverData);
          break;
        case 1400:
          text = SK.Text("TOOLTIPS_LOGOUT_CLOSE", "Close logout window without logging out");
          break;
        case 1401:
          text = SK.Text("TOOLTIPS_LOGOUT_AUTO_TRADE", "Toggle Auto-Trade On/Off");
          break;
        case 1402:
          text = SK.Text("TOOLTIPS_LOGOUT_AUTO_SCOUT", "Toggle Auto-Scouting On/Off");
          break;
        case 1403:
          text = SK.Text("TOOLTIPS_LOGOUT_AUTO_ATTACK", "Toggle Auto-Attack On/Off");
          break;
        case 1404:
          text = SK.Text("TOOLTIPS_LOGOUT_AUTO_RECRUIT", "Toggle Auto-Recruit On/Off");
          break;
        case 1405:
          text = SK.Text("TOOLTIPS_LOGOUT_AUTO_REBUILD", "Toggle Auto-Rebuild On/Off");
          break;
        case 1406:
          text = SK.Text("TOOLTIPS_LOGOUT_AUTO_TRANSFER", "Toggle Auto-Transfer On/Off");
          break;
        case 1407:
          text = SK.Text("TOOLTIPS_LOGOUT_SELECT_TRADE_RESOURCE", "Select Auto-Trade Resource") + ". " + SK.Text("TOOLTIPS_LOGOUT_SELECT_TRADE_RESOURCE_currently", "Currently") + " : " + VillageBuildingsData.getResourceNames(CustomTooltipManager.currentOverData);
          break;
        case 1408:
          text = SK.Text("TOOLTIPS_LOGOUT_SELECT_TRADE_PERCENT", "Adjust % at which Auto-Trade starts trading ");
          break;
        case 1409:
          text = SK.Text("TOOLTIPS_LOGOUT_ATTACK_BANDITS", "Toggle Auto-Attack Bandit Camps");
          break;
        case 1410:
          text = SK.Text("TOOLTIPS_LOGOUT_ATTACK_WOLVES", "Toggle Auto-Attack Wolf Lairs");
          break;
        case 1411:
          text = SK.Text("TOOLTIPS_LOGOUT_ATTACK_AI", "Toggle Auto-Attack AI Castles");
          break;
        case 1412:
          text = SK.Text("TOOLTIPS_LOGOUT_RECRUIT_PEASANT", "Toggle Auto-Recruit Peasants");
          break;
        case 1413:
          text = SK.Text("TOOLTIPS_LOGOUT_RECRUIT_ARCHER", "Toggle Auto-Recruit Archers");
          break;
        case 1414:
          text = SK.Text("TOOLTIPS_LOGOUT_RECRUIT_PIKEMAN", "Toggle Auto-Recruit Pikemen");
          break;
        case 1415:
          text = SK.Text("TOOLTIPS_LOGOUT_RECRUIT_SWORDSMAN", "Toggle Auto-Recruit Swordsmen");
          break;
        case 1416:
          text = SK.Text("TOOLTIPS_LOGOUT_RECRUIT_CATAPULT", "Toggle Auto-Recruit Catapults");
          break;
        case 1417:
          text = SK.Text("TOOLTIPS_LOGOUT_RESOURCES", "Select Auto-Trade Resource : " + VillageBuildingsData.getResourceNames(CustomTooltipManager.currentOverData));
          break;
        case 1418:
          text = SK.Text("TOOLTIPS_LOGOUT_EXIT", "Exit Stronghold Kingdoms");
          break;
        case 1419:
          text = SK.Text("TOOLTIPS_LOGOUT_CANCEL", "Close logout window without logging out");
          break;
        case 1420:
          text = SK.Text("TOOLTIPS_LOGOUT_SWAP_WORLDS", "Log out of this Game World and return to the World Selection Screen");
          break;
        case 1421:
          int premiumCard = GameEngine.Instance.cardsManager.UserCardData.premiumCard;
          double totalSeconds1 = GameEngine.Instance.cardsManager.UserCardData.premiumCardExpiry.Subtract(VillageMap.getCurrentServerTime()).TotalSeconds;
          double d4 = totalSeconds1 / 86400.0;
          double d5 = totalSeconds1 % 86400.0 / 3600.0;
          double d6 = totalSeconds1 % 3600.0 / 60.0;
          string[] strArray4 = new string[7];
          strArray4[0] = SK.Text("TOOLTIPS_LOGOUT_PREMIUM", "Your Premium Token expires in : ");
          string[] strArray5 = strArray4;
          double num5 = Math.Floor(d4);
          string str13 = num5.ToString().PadLeft(2, '0');
          strArray5[1] = str13;
          strArray4[2] = ":";
          string[] strArray6 = strArray4;
          num5 = Math.Floor(d5);
          string str14 = num5.ToString().PadLeft(2, '0');
          strArray6[3] = str14;
          strArray4[4] = ":";
          string[] strArray7 = strArray4;
          num5 = Math.Floor(d6);
          string str15 = num5.ToString().PadLeft(2, '0');
          strArray7[5] = str15;
          strArray4[6] = " (dd:hh:mm)";
          text = string.Concat(strArray4);
          break;
        case 1500:
          text = SK.Text("TOOLTIPS_REPORTS_FILTER", "Change Report Filtering");
          break;
        case 1501:
          text = SK.Text("TOOLTIPS_REPORTS_CAPTURE", "Change Report Capturing");
          break;
        case 1502:
          text = SK.Text("TOOLTIPS_REPORTS_DELETE", "Report Marking and Deleting options");
          break;
        case 1600:
          text = SK.Text("TOOLTIPS_TUTORIAL_REOPEN", "Show Adviser");
          break;
        case 1601:
          text = SK.Text("Options_Player_Guide", "Player Guide");
          break;
        case 1700:
        case 1701:
        case 1702:
        case 1703:
        case 1704:
        case 1705:
        case 1706:
        case 1707:
        case 1708:
        case 1709:
        case 1710:
        case 1711:
        case 1712:
        case 1713:
        case 1714:
        case 1715:
        case 1716:
        case 1717:
        case 1718:
        case 1719:
          int num6 = ID - 1700 + 1;
          NumberFormatInfo nfi1 = GameEngine.NFI;
          text = SK.Text("TOOLTIPS_GLORY_HOUSE", "House") + " " + num6.ToString() + " - " + SK.Text("TOOLTIPS_GLORY_POINTS", "Glory") + " " + CustomTooltipManager.currentOverData.ToString("N", (IFormatProvider) nfi1);
          break;
        case 1730:
          text = SK.Text("TOOLTIPS_ERA_GLORY_STAR", "A Maximum of 5 Stars are given out each Glory Round");
          break;
        case 1750:
          text = SK.Text("TOOLTIPS_EOW_INACTIVE", "Only a House Marshall, in possession of all the Royal towers, can press this button");
          break;
        case 1751:
          int currentOverData = CustomTooltipManager.currentOverData;
          text = SK.Text("TOOLTIPS_GLORY_HOUSE", "House") + " " + currentOverData.ToString() + " : " + SK.Text("TOOLTIPS_EOW_ACTIVE", "House Marshall, you may push the button");
          break;
        case 1800:
          text = SK.Text("TOOLTIPS_NEW_VILLAGE_ENTER", "Choose a starting location for me");
          break;
        case 1801:
          text = SK.Text("TOOLTIPS_NEW_VILLAGE_ADVANCED", "Try and join the game in a particular part of the kingdom");
          break;
        case 1802:
          text = SK.Text("TOOLTIPS_NEW_VILLAGE_LOGOUT", "Log out");
          break;
        case 1900:
          text = CapitalDonateResourcesPanel2.capitalTooltipText;
          break;
        case 2000:
          text = SK.Text("SendMonksPanel_Influence", "Influence");
          break;
        case 2001:
          text = SK.Text("VillageMapPanel_Blessing", "Blessing");
          break;
        case 2002:
          text = SK.Text("VillageMapPanel_Inquisition", "Inquisition");
          break;
        case 2003:
          text = SK.Text("SendMonksPanel_Interdiction", "Interdiction");
          break;
        case 2004:
          text = SK.Text("SendMonksPanel_Restoration", "Restoration");
          break;
        case 2005:
          text = SK.Text("SendMonksPanel_Absolution", "Absolution");
          break;
        case 2006:
          text = SK.Text("SendMonksPanel_Excommnunication", "Excommunication");
          break;
        case 2007:
          text = SK.Text("SendMonksPanel_Positive_Influence", "Positive Influence");
          break;
        case 2008:
          text = SK.Text("SendMonksPanel_Negative_Influence", "Negative Influence");
          break;
        case 2018:
          text = SK.Text("TOOLTIPS_FAVOURITE_ATTACK_TARGET_MAKE", "Mark this village as a Favourite");
          break;
        case 2100:
          text = SK.Text("LaunchAttackPopup_Vandalise", "Vandalise");
          break;
        case 2101:
          text = SK.Text("GENERIC_Capture", "Capture");
          break;
        case 2102:
          text = SK.Text("GENERIC_Pillage", "Pillage");
          break;
        case 2103:
          text = SK.Text("GENERIC_Ransack", "Ransack");
          break;
        case 2104:
          text = SK.Text("GENERIC_Raze", "Raze");
          break;
        case 2105:
          text = SK.Text("GENERIC_Gold_Raid", "Gold Raid");
          break;
        case 2106:
          text = SK.Text("GENERIC_Attack", "Attack");
          break;
        case 2107:
          text = SK.Text("TOOLTIPS_FAVOURITE_ATTACK_TARGET_CLEAR", "Remove this village from your Favourites");
          break;
        case 2300:
          text = SK.Text("TOOLTIPS_FACTIONTAB_GLORY", "Click to View the Glory Screen");
          break;
        case 2301:
          text = SK.Text("TOOLTIPS_FACTIONTAB_FACTIONS", "Click to View the Faction Screens");
          break;
        case 2302:
          text = SK.Text("TOOLTIPS_FACTIONTAB_HOUSE", "Click to View the House Screens");
          break;
        case 2303:
          text = SK.Text("GENERIC_Ally", "Ally");
          break;
        case 2304:
          text = SK.Text("GENERIC_Enemy", "Enemy");
          break;
        case 2305:
          text = SK.Text("TOOLTIPS_FACTION_LEADER", "Faction Leader");
          break;
        case 2306:
          text = SK.Text("TOOLTIPS_FACTION_OFFICER", "Faction Officer");
          break;
        case 2307:
          text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + CustomTooltipManager.currentOverData.ToString();
          break;
        case 2308:
          HouseData houseData = (HouseData) null;
          try
          {
            houseData = GameEngine.Instance.World.HouseInfo[CustomTooltipManager.currentOverData];
          }
          catch (Exception ex)
          {
          }
          int gloryRank = GameEngine.Instance.World.getGloryRank(CustomTooltipManager.currentOverData);
          string str16 = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + CustomTooltipManager.currentOverData.ToString() + Environment.NewLine + "\"" + CustomTooltipManager.getHouseMotto(CustomTooltipManager.currentOverData) + "\"" + Environment.NewLine + Environment.NewLine;
          if (gloryRank >= 0)
          {
            NumberFormatInfo nfi2 = GameEngine.NFI;
            string[] strArray8 = new string[5]
            {
              str16,
              SK.Text("FactionInvites_Glory_Rank", "Glory Rank"),
              " : ",
              null,
              null
            };
            string[] strArray9 = strArray8;
            int num7 = gloryRank + 1;
            string str17 = num7.ToString();
            strArray9[3] = str17;
            strArray8[4] = Environment.NewLine;
            string str18 = string.Concat(strArray8);
            string str19 = SK.Text("FactionInvites_Glory_Points", "Glory Points");
            num7 = GameEngine.Instance.World.getGloryPoints(CustomTooltipManager.currentOverData);
            string str20 = num7.ToString("N", (IFormatProvider) nfi2);
            text = str18 + str19 + " : " + str20;
            if (houseData != null && houseData.numVictories > 0)
            {
              text = text + Environment.NewLine + Environment.NewLine + SK.Text("FactionInvites_Glory Victories", "Glory Victories") + " : " + houseData.numVictories.ToString();
              break;
            }
            break;
          }
          text = str16 + SK.Text("FactionInvites_Glory_Eliminatated", "Eliminated From Glory Race");
          break;
        case 2350:
          text = SK.Text("FACTION_SIDEBAR_SHOW_ALL", "A List of All Factions in the Game World");
          break;
        case 2351:
          text = SK.Text("FACTION_SIDEBAR_MY_FACTION", "Details of Your Faction");
          break;
        case 2352:
          text = SK.Text("FACTION_SIDEBAR_DIPLOMACY", "Your Faction's Relationship to Other Factions");
          break;
        case 2353:
          text = SK.Text("FACTION_SIDEBAR_OFFICERS", "Manage Your Faction and View the List of Officers");
          break;
        case 2354:
          text = SK.Text("FACTION_SIDEBAR_FORUM", "Your Faction's Forum");
          break;
        case 2355:
          text = SK.Text("FACTION_SIDEBAR_MAIL", "Send a Mail to Everyone in Your Faction");
          break;
        case 2356:
          text = SK.Text("FACTION_SIDEBAR_INVITES", "Details of Your Faction Invites and Applications");
          break;
        case 2357:
          text = SK.Text("FACTION_SIDEBAR_CHAT", "Your Faction's Chat Channel");
          break;
        case 2358:
          text = SK.Text("FACTION_SIDEBAR_START", "Start a New Faction");
          break;
        case 2359:
          text = SK.Text("FACTION_SIDEBAR_LEAVE", "Leave your current Faction");
          break;
        case 2400:
          text = SK.Text("GENERIC_Cancel", "Cancel");
          break;
        case 2401:
          text = SK.Text("GENERIC_Select_Target", "Select Target");
          break;
        case 2402:
          text = SK.Text("GENERIC_Select_Target", "Select Target");
          break;
        case 2403:
          text = SK.Text("TradeWithPanel_Trade_With", "Trade With");
          break;
        case 2404:
          text = SK.Text("StockExchangeSidePanel_Select_Exchange", "Select Exchange");
          break;
        case 2405:
          text = SK.Text("MonkTargetSidePanel_Select_Target", "Select Target");
          break;
        case 2406:
          text = SK.Text("MonkTargetSidePanel_Select_Target", "Select Target");
          break;
        case 2407:
          text = SK.Text("VassalSelectSidePanel_Select_Vassal", "Select Vassal");
          break;
        case 2410:
          text = SK.Text("TradeWithPanel_Trade_With", "Trade With");
          break;
        case 2411:
          text = SK.Text("GENERIC_Attack", "Attack");
          break;
        case 2412:
          text = SK.Text("GENERIC_Scout_Out_Village", "Scout Out Village");
          break;
        case 2413:
          text = SK.Text("GENERIC_Send_Troops", "Send Troops");
          break;
        case 2414:
          text = SK.Text("GENERIC_Send_Monks", "Send Monks");
          break;
        case 2420:
          text = SK.Text("GENERIC_Parish", "Parish");
          break;
        case 2421:
          text = SK.Text("GENERIC_County", "County");
          break;
        case 2422:
          text = SK.Text("GENERIC_Province", "Province");
          break;
        case 2423:
          text = SK.Text("GENERIC_Country", "Country");
          break;
        case 2424:
          text = SK.Text("GENERIC_Bandit_Camp", "Bandit Camp");
          break;
        case 2425:
          text = SK.Text("GENERIC_Wolf_Camp", "Wolf Lair");
          break;
        case 2426:
          text = SK.Text("GENERIC_Rats_Castle", "Rat's Castle");
          break;
        case 2427:
          text = SK.Text("GENERIC_Snakes_Castle", "Snake's Castle");
          break;
        case 2428:
          text = SK.Text("GENERIC_Pigs_Castle", "Pig's Castle");
          break;
        case 2429:
          text = SK.Text("GENERIC_Wolfs_Castle", "Wolf's Castle");
          break;
        case 2430:
          text = SpecialVillageTypes.getName(CustomTooltipManager.currentOverData, "");
          break;
        case 2431:
          text = SK.Text("OwnVillagePanel_Send_Out_Resources", "Send Out Resources");
          break;
        case 2432:
          text = SK.Text("OwnVillagePanel_Send_Out_Troops", "Send Out Troops");
          break;
        case 2433:
          text = SK.Text("OwnVillagePanel_Send_Out_Scouts", "Send Out Scouts");
          break;
        case 2434:
          text = SK.Text("OwnVillagePanel_Send_Out_Reinforcements", "Send Out Reinforcements");
          break;
        case 2435:
          text = SK.Text("OwnVillagePanel_Send_Out_Monks", "Send Out Monks");
          break;
        case 2436:
          switch (CustomTooltipManager.currentOverData)
          {
            case 0:
              text = SK.Text("MapTypes_Lowland", "Lowland");
              break;
            case 1:
              text = SK.Text("MapTypes_Highland", "Highland");
              break;
            case 2:
              text = SK.Text("MapTypes_River", "River") + " 1";
              break;
            case 3:
              text = SK.Text("MapTypes_River", "River") + " 2";
              break;
            case 4:
              text = SK.Text("MapTypes_Mountain_Peak", "Mountain Peak");
              break;
            case 5:
              text = SK.Text("MapTypes_Salt_Flat", "Salt Flat");
              break;
            case 6:
              text = SK.Text("MapTypes_Marsh", "Marsh");
              break;
            case 7:
              text = SK.Text("MapTypes_Plains", "Plains");
              break;
            case 8:
              text = SK.Text("MapTypes_Valley_Side", "Valley Side");
              break;
            case 9:
              text = SK.Text("MapTypes_Forest", "Forest");
              break;
          }
          break;
        case 2437:
          text = SK.Text("TOOLTIPS_View_Village", "View Village");
          break;
        case 2438:
          text = SK.Text("TOOLTIPS_View_Castle", "View Castle");
          break;
        case 2439:
          text = SK.Text("TOOLTIPS_View_Resources", "View Resources");
          break;
        case 2440:
          text = SK.Text("TOOLTIPS_Make_Troops", "Make Troops");
          break;
        case 2441:
          text = SK.Text("CapitalTradePanel_", "Purchase Goods");
          break;
        case 2442:
          text = SK.Text("CapitalBarracksPanel_Mercenaries", "Mercenaries");
          break;
        case 2443:
          text = SK.Text("TOOLTIPS_Scout_Stash", "Scout Stash");
          break;
        case 2444:
          text = SK.Text("EmptyVillagePanel_Available_Village", "New Village Charter");
          break;
        case 2445:
          text = SK.Text("TOOLTIPS_View_Castle_Report", "View most recent report of this Castle");
          break;
        case 2446:
          text = SK.Text("OtherVillagePanel_Make_Vassal", "Make Vassal");
          break;
        case 2447:
          text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
          break;
        case 2448:
          text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
          break;
        case 2449:
          text = SpecialVillageTypes.getName(CustomTooltipManager.currentOverData, "");
          break;
        case 2450:
          text = SK.Text("GENERIC_Parish_Plague", "Disease Infested Parish") + " : " + InterfaceMgr.Instance.getPlagueText(CustomTooltipManager.currentOverData) + "  (" + CustomTooltipManager.currentOverData.ToString() + ")";
          break;
        case 2451:
          text = SK.Text("VassalVillagePanel_Manage_Vassal_Troops", "Manage Vassal's Troops");
          break;
        case 2452:
          text = SK.Text("VassalVillagePanel_Manage_Vassal", "Manage Vassal");
          break;
        case 2453:
          text = SK.Text("VassalVillagePanel_Attack_From_Here", "Attack From Here");
          break;
        case 2454:
          text = SK.Text("TOOLTIPS_Filter_Traders", "Show only Traders");
          break;
        case 2455:
          text = SK.Text("TOOLTIPS_Filter_Attacks", "Show only Attacking Troops");
          break;
        case 2456:
          text = SK.Text("TOOLTIPS_Filter_Foraging", "Show only Foraging Scouts");
          break;
        case 2457:
          text = SK.Text("TOOLTIPS_Filter_House", "Show only villages in your House");
          break;
        case 2458:
          text = SK.Text("TOOLTIPS_Filter_Faction", "Show only villages in your Faction");
          break;
        case 2459:
          text = SK.Text("TOOLTIPS_Clear_Filter", "Clear Filter");
          break;
        case 2460:
          text = SK.Text("TOOLTIPS_village_search", "Search For Villages");
          break;
        case 2461:
          text = SK.Text("TOOLTIPS_Filter_Faction_Open", "Show only villages in Factions accepting Invitations");
          break;
        case 2462:
          text = SK.Text("TOOLTIPS_Filter_AI", "Show only attackable Wolf Camps, Bandit Camps and AI Castles");
          break;
        case 2501:
          text = GameEngine.Instance.World.getFactionName(CustomTooltipManager.currentOverData);
          break;
        case 2502:
          text = SK.Text("MailScreen_Send_Mail", "Send Mail");
          break;
        case 2503:
          if (CustomTooltipManager.UserInfo != null)
          {
            NumberFormatInfo nfi3 = GameEngine.NFI;
            string str21 = SK.Text("STATS_CATEGORY_TITLE_RANK", "Rank") + " : ";
            text = (CustomTooltipManager.UserInfo.avatarData == null ? str21 + Rankings.getRankingName(CustomTooltipManager.UserInfo.rank) : str21 + Rankings.getRankingName(CustomTooltipManager.UserInfo.rank, CustomTooltipManager.UserInfo.avatarData.male)) + Environment.NewLine + SK.Text("GENERIC_Villages", "Villages") + " : " + CustomTooltipManager.UserInfo.numVillages.ToString("N", (IFormatProvider) nfi3) + Environment.NewLine + SK.Text("UserInfoPanel_Points", "Points") + " : " + CustomTooltipManager.UserInfo.points.ToString("N", (IFormatProvider) nfi3) + Environment.NewLine;
            if (CustomTooltipManager.UserInfo.standing >= 0)
            {
              text = text + SK.Text("UserInfoPanel_Position", "Position") + " : " + CustomTooltipManager.UserInfo.standing.ToString("N", (IFormatProvider) nfi3);
              break;
            }
            break;
          }
          text = "";
          break;
        case 2504:
          text = SK.Text("TOOLTIPS_MAPSIDE_BUY_CHARTER_RANK_TOO_LOW", "You can't own more villages at this time due your current Rank or your Leadership Research level.");
          break;
        case 2505:
          text = SK.Text("TOOLTIPS_MAPSIDE_CANT_BUY_FROM_HERE", "You cannot purchase other villages from your currently selected village.");
          break;
        case 2506:
          text = SK.Text("TOOLTIPS_MAPSIDE_BUY_CHARTER_RANK_TOO_LOW12", "You need to be rank 12 to own more villages.");
          break;
        case 2700:
          text = "";
          bool flag1 = false;
          if ((CustomTooltipManager.currentOverData & 1) != 0)
          {
            if (flag1)
              text += Environment.NewLine;
            text += SK.Text("TOOLTIPS_UNIT_MAKE_ERROR_PEASANTS", "- No Peasants Available");
            flag1 = true;
          }
          if ((CustomTooltipManager.currentOverData & 2) != 0)
          {
            if (flag1)
              text += Environment.NewLine;
            text += SK.Text("TOOLTIPS_UNIT_MAKE_ERROR_GOLD", "- Not enough Gold");
            flag1 = true;
          }
          if ((CustomTooltipManager.currentOverData & 32) != 0)
          {
            if (flag1)
              text += Environment.NewLine;
            text += SK.Text("TOOLTIPS_UNIT_MAKE_ERROR_TROOP_SPACE", "- Not enough Army Space");
            flag1 = true;
          }
          else if ((CustomTooltipManager.currentOverData & 4) != 0)
          {
            if (flag1)
              text += Environment.NewLine;
            text += SK.Text("TOOLTIPS_UNIT_MAKE_ERROR_SPACE", "- Not enough Unit Space");
            flag1 = true;
          }
          if ((CustomTooltipManager.currentOverData & 8) != 0)
          {
            if (flag1)
              text += Environment.NewLine;
            text += SK.Text("TOOLTIPS_UNIT_MAKE_ERROR_FULL", "- You have made the Maximum amount");
            flag1 = true;
          }
          if ((CustomTooltipManager.currentOverData & 16) != 0)
          {
            if (flag1)
              text += Environment.NewLine;
            text += SK.Text("TOOLTIPS_UNIT_MAKE_ERROR_WEAPON", "- No Weapons Available");
            break;
          }
          break;
        case 2800:
          text = SK.Text("TOOLTIPS_VASSAL_AVAILABLE_TROOPS", "Available Troops");
          break;
        case 2900:
          text = SK.Text("TOOLTIPS_ARMIES_ATTACKS", "View All Attacks");
          break;
        case 2901:
          text = SK.Text("TOOLTIPS_ARMIES_SCOUTS", "View All Scouts");
          break;
        case 2902:
          text = SK.Text("TOOLTIPS_ARMIES_REINFORCEMENTS", "View All Reinforcements");
          break;
        case 2903:
          text = SK.Text("TOOLTIPS_ARMIES_MERCHANTS", "View All Merchants");
          break;
        case 2904:
          text = SK.Text("TOOLTIPS_ARMIES_MONKS", "View All Monks");
          break;
        case 2905:
          switch (CustomTooltipManager.currentOverData)
          {
            case 1:
              text = SK.Text("TOOLTIPS_ARMIES_SORTING_FROM", "Sort By Target Village");
              break;
            case 2:
              text = SK.Text("TOOLTIPS_ARMIES_SORTING_HOME", "Sort By Source Village");
              break;
            case 3:
              text = SK.Text("TOOLTIPS_ARMIES_SORTING_PEASANTS", "Sort By Number of Peasants");
              break;
            case 4:
              text = SK.Text("TOOLTIPS_ARMIES_SORTING_ARCHERS", "Sort By Number of Archers");
              break;
            case 5:
              text = SK.Text("TOOLTIPS_ARMIES_SORTING_PIKEMEN", "Sort By Number of Pikemen");
              break;
            case 6:
              text = SK.Text("TOOLTIPS_ARMIES_SORTING_SWORDSMEN", "Sort By Number of Swordsmen");
              break;
            case 7:
              text = SK.Text("TOOLTIPS_ARMIES_SORTING_CATAPULTS", "Sort By Number of Catapults");
              break;
            case 8:
              text = SK.Text("TOOLTIPS_ARMIES_SORTING_TIME", "Sort By Arrival Time");
              break;
            case 9:
              text = SK.Text("TOOLTIPS_ARMIES_SORTING_SCOUTS", "Sort By Scouts");
              break;
            case 10:
              text = SK.Text("TOOLTIPS_ARMIES_SORTING_TRADE_AMOUNT", "Sort By Trade Size");
              break;
            case 11:
              text = SK.Text("TOOLTIPS_ARMIES_SORTING_TRADE_COMMAND", "Sort By Trade Status");
              break;
            case 12:
              text = SK.Text("TOOLTIPS_ARMIES_SORTING_MONKS", "Sort By Number of Monks");
              break;
            case 13:
              text = SK.Text("TOOLTIPS_ARMIES_SORTING_MONKS_COMMAND", "Sort By Monk Command");
              break;
            case 20:
              text = SK.Text("TOOLTIPS_ARMIES_SORTING_CAPTAINS", "Sort By Number of Captains");
              break;
          }
          break;
        case 3001:
        case 3002:
        case 3003:
        case 3004:
          int achievement = CustomTooltipManager.currentOverData & 4095;
          string achievementRank = CustomTooltipManager.getAchievementRank(CustomTooltipManager.currentOverData);
          int rankLevel;
          switch (CustomTooltipManager.currentOverData & 1879048192)
          {
            case 268435456:
              rankLevel = 1;
              break;
            case 536870912:
              rankLevel = 2;
              break;
            case 1073741824:
              rankLevel = 3;
              break;
            case 1342177280:
              rankLevel = 4;
              break;
            case 1610612736:
              rankLevel = 5;
              break;
            case 1879048192:
              rankLevel = 6;
              break;
            default:
              rankLevel = 0;
              break;
          }
          if (ID == 3001)
            rankLevel = -1;
          string achievementTitle = CustomTooltipManager.getAchievementTitle(achievement);
          string achievementRequirement1 = CustomTooltipManager.getAchievementRequirement(achievement, rankLevel);
          string achievementRequirement2 = CustomTooltipManager.getAchievementRequirement(achievement, rankLevel + 1);
          int num8 = 1;
          bool flag2 = true;
          if (ID == 3001 || ID == 3002)
            flag2 = false;
          else
            num8 = RankingsPanel.getProgressValue(achievement);
          NumberFormatInfo nfi4 = GameEngine.NFI;
          if (rankLevel >= 0 && rankLevel <= 5 && achievementRequirement2.Length > 0)
          {
            text = achievementTitle + Environment.NewLine + achievementRank + Environment.NewLine + achievementRequirement1 + Environment.NewLine + Environment.NewLine + SK.Text("Achievements_NextLevel", "Next Level") + Environment.NewLine + achievementRequirement2;
            if (flag2 && num8 > 0)
            {
              text = text + Environment.NewLine + Environment.NewLine + SK.Text("Achievements_CurrentProgress", "Current Progress") + " : " + num8.ToString("N", (IFormatProvider) nfi4);
              break;
            }
            break;
          }
          if (rankLevel < 0)
          {
            text = achievementTitle + Environment.NewLine + SK.Text("Achievements_NoLevel", "No Achievements Earned") + Environment.NewLine + achievementRequirement2;
            if (flag2 && num8 > 0)
            {
              text = text + Environment.NewLine + Environment.NewLine + SK.Text("Achievements_CurrentProgress", "Current Progress") + " : " + num8.ToString("N", (IFormatProvider) nfi4);
              break;
            }
            break;
          }
          if (rankLevel >= 3 && achievementRequirement2.Length == 0)
          {
            text = achievementTitle + Environment.NewLine + achievementRank + Environment.NewLine + achievementRequirement1;
            break;
          }
          break;
        case 3101:
          text = "Admin Functions";
          break;
        case 3102:
          text = SK.Text("TOOLTIPS_USER_CLEAR_DIPLOMACY", "Remove Diplomatic Status for this Player");
          break;
        case 3103:
          text = SK.Text("TOOLTIPS_USER_CLEAR_DIPLOMACY_NOTES", "Edit Notes for this Player");
          break;
        case 3201:
          text = SK.Text("TOOLTIPS_START_QUEST", "Start this quest");
          break;
        case 3202:
          text = SK.Text("TOOLTIPS_ABANDON_QUEST", "Abandon this quest and remove it from the list.");
          break;
        case 3203:
          text = SK.Text("TOOLTIPS_QUEST_REWARD_HONOUR", "Honour");
          break;
        case 3204:
          text = SK.Text("TOOLTIPS_QUEST_REWARD_GOLD", "Gold");
          break;
        case 3205:
          text = SK.Text("TOOLTIPS_QUEST_REWARD_WOOD", "Wood");
          break;
        case 3206:
          text = SK.Text("TOOLTIPS_QUEST_REWARD_STONE", "Stone");
          break;
        case 3207:
          text = SK.Text("TOOLTIPS_QUEST_REWARD_APPLES", "Apples");
          break;
        case 3208:
          text = SK.Text("TOOLTIPS_QUEST_REWARD_CARD_PACKS", "Card Packs");
          break;
        case 3209:
          text = SK.Text("TOOLTIPS_QUEST_REWARD_PREMIUM_CARD", "2 Day Premium Token");
          break;
        case 3210:
          text = SK.Text("TOOLTIPS_QUEST_REWARD_FAITHPOINTS", "Faith Points");
          break;
        case 3211:
          text = SK.Text("TOOLTIPS_QUEST_REWARD_GLORY", "Glory");
          break;
        case 3212:
          text = SK.Text("TOOLTIPS_QUEST_REWARD_SHIELD_CHARGES", "A new Charge for your Shield");
          break;
        case 3213:
          text = SK.Text("TOOLTIPS_QUEST_REWARD_TICKETS", "Quest Wheel Spins");
          break;
        case 3214:
          text = SK.Text("ResourceType_Fish", "Fish");
          break;
        case 4001:
          text = SK.Text("TOOLTIPS_LOGIN_ENGLISH_SUPPORT", "Customer Support for this world is in English");
          break;
        case 4002:
          text = SK.Text("TOOLTIPS_LOGIN_GERMAN_SUPPORT", "Customer Support for this world is in German");
          break;
        case 4003:
          text = SK.Text("TOOLTIPS_LOGIN_FRENCH_SUPPORT", "Customer Support for this world is in French");
          break;
        case 4004:
          text = SK.Text("TOOLTIPS_LOGIN_RUSSIAN_SUPPORT", "Customer Support for this world is in Russian");
          break;
        case 4005:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_ENGLAND", "The map of this world is centred around Great Britain");
          break;
        case 4006:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_GERMANY", "The map of this world is centred around Germany");
          break;
        case 4007:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_FRANCE", "The map of this world is centred around France");
          break;
        case 4008:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_RUSSIA", "The map of this world is centred around Russia");
          break;
        case 4009:
          text = SK.Text("TOOLTIPS_LOGIN_OFFLINE", "This world is currently Offline, most likely due to server maintenance and should be back Online soon.");
          break;
        case 4010:
          text = SK.Text("TOOLTIPS_LOGIN_ONLINE", "This world is currently Online");
          break;
        case 4011:
          text = SK.Text("TOOLTIPS_LOGIN_ENGLISH_FLAG", "View available worlds with English Support");
          break;
        case 4012:
          text = SK.Text("TOOLTIPS_LOGIN_GERMAN_FLAG", "View available worlds with German Support");
          break;
        case 4013:
          text = SK.Text("TOOLTIPS_LOGIN_FRENCH_FLAG", "View available worlds with French Support");
          break;
        case 4014:
          text = SK.Text("TOOLTIPS_LOGIN_RUSSIAN_FLAG", "View available worlds with Russian Support");
          break;
        case 4015:
          text = SK.Text("TOOLTIPS_LOGIN_EDIT_SHIELD", "Click to Edit Your Coat of Arms");
          break;
        case 4016:
          text = SK.Text("TOOLTIPS_LOGIN_SPANISH_SUPPORT", "Customer Support for this world is in Spanish");
          break;
        case 4017:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_SPAIN", "The map of this world is centred around Spain");
          break;
        case 4018:
          text = SK.Text("TOOLTIPS_LOGIN_SPANISH_FLAG", "View available worlds with Spanish Support");
          break;
        case 4019:
          text = SK.Text("TOOLTIPS_LOGIN_SECOND_AGE", "This World is in its Second Age.");
          break;
        case 4020:
          text = SK.Text("TOOLTIPS_LOGIN_POLISH_SUPPORT", "Customer Support for this world is in Polish");
          break;
        case 4021:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_POLAND", "The map of this world is Poland");
          break;
        case 4022:
          text = SK.Text("LOGIN_POLISH_FLAG", "View available worlds with Polish Support");
          break;
        case 4023:
          text = SK.Text("TOOLTIPS_LOGIN_TURKISH_SUPPORT", "Customer Support for this world is in Turkish");
          break;
        case 4024:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_TURKEY", "The map of this world is Turkey");
          break;
        case 4025:
          text = SK.Text("TOOLTIP_LOGIN_TURKISH_FLAG", "View available worlds with Turkish Support");
          break;
        case 4026:
          text = SK.Text("TOOLTIPS_LOGIN_THIRD_AGE", "This World is in its Third Age.");
          break;
        case 4027:
          text = SK.Text("TOOLTIPS_LOGIN_ITALIAN_SUPPORT", "Customer Support for this world is in Italian");
          break;
        case 4028:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_ITALY", "The map of this world is centred around Italy and the Adriatic");
          break;
        case 4029:
          text = SK.Text("TOOLTIP_LOGIN_ITALIAN_FLAG", "View available worlds with Italian Support");
          break;
        case 4030:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_USA", "The map of this world is the USA");
          break;
        case 4031:
        case 4041:
          text = SK.Text("TOOLTIPS_LOGIN_EUROPE_SUPPORT", "Customer Support for this world is available in all currently supported languages");
          break;
        case 4032:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_EUROPE", "The map of this world is Europe");
          break;
        case 4033:
          text = SK.Text("TOOLTIP_LOGIN_EUROPEAN_FLAG", "View available worlds with the European Map");
          break;
        case 4034:
          text = SK.Text("TOOLTIPS_LOGIN_FOURTH_AGE", "This World is in its Fourth Age.");
          break;
        case 4035:
          text = SK.Text("TOOLTIPS_LOGIN_PORTUGUESE_SUPPORT", "Customer Support for this world is in Brazilian Portuguese and Spanish");
          break;
        case 4036:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_SOUTH_AMERICA", "The map of this world is Central and South America");
          break;
        case 4037:
          text = SK.Text("TOOLTIP_LOGIN_PORTUGUESE_FLAG", "View available worlds with Brazilian-Portuguese Support");
          break;
        case 4038:
          text = SK.Text("TOOLTIPS_LOGIN_FIRST_AGE", "This World is in its First Age. It is recommended that new players join a world that in its First Age.");
          break;
        case 4039:
          text = SK.Text("TOOLTIPS_LOGIN_FIFTH_AGE", "This World is in its Fifth Age.");
          break;
        case 4040:
          text = SK.Text("TOOLTIP_LOGIN_WORLD_FLAG", "View available worlds with the Global Conflict Map");
          break;
        case 4042:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_WORLD", "The map of this world is Global Conflict");
          break;
        case 4043:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_PHILIPPINES", "The map of this world is Island Warfare: Philippines");
          break;
        case 4044:
          text = SK.Text("TOOLTIPS_LOGIN_SIXTH_AGE", "This World is in its Sixth Age.");
          break;
        case 4045:
          text = SK.Text("TOOLTIPS_LOGIN_SEVENTH_AGE", "This World is in its Final Age.");
          break;
        case 4046:
          text = SK.Text("TOOLTIPS_LOGIN_CHINESE_SUPPORT", "Customer Support for this world is in Chinese");
          break;
        case 4047:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_CHINA", "The map of this world is centred around China");
          break;
        case 4048:
          text = SK.Text("TOOLTIP_LOGIN_CHINESE_FLAG", "View available worlds with Chinese Support");
          break;
        case 4049:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_KINGMAKER", "The map of this world is Medieval Europe");
          break;
        case 4050:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_SEA_JAPAN", "The map of this world is The Sea of Japan");
          break;
        case 4051:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_HYW", "The map of this world is The Hundred Years' War");
          break;
        case 4052:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_VK", "The map of this world is The Viking Shores");
          break;
        case 4053:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_TGD", "The map of this world is The Grand Duchy");
          break;
        case 4054:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_CRUSADER", "The map of this world is Crusader States");
          break;
        case 4055:
          text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_SPARTA", "The map of this world is Sparta");
          break;
        case 4100:
          text = SK.Text("BARRACKS_Troops", "Troops");
          break;
        case 4101:
          text = SK.Text("GENERIC_Scouts", "Scouts");
          break;
        case 4102:
          text = SK.Text("GENERIC_Merchants", "Merchants");
          break;
        case 4103:
          text = SK.Text("GENERIC_Monks", "Monks");
          break;
        case 4104:
          text = SK.Text("TOOLTIPS_VO_POPULARITY", "Popularity");
          break;
        case 4105:
          text = SK.Text("TOOLTIPS_VO_NUM_BUILDINGS", "Number of Placed Buildings");
          break;
        case 4106:
          text = SK.Text("TOOLTIPS_VO_KEEP_ENCLOSED", "Keep Enclosed");
          break;
        case 4107:
          text = SK.Text("TOOLTIPS_VO_KEEP_NOT_ENCLOSED", "Keep Not Enclosed");
          break;
        case 4108:
          text = SK.Text("GENERIC_Peasants", "Peasants");
          break;
        case 4109:
          text = SK.Text("GENERIC_Archers", "Archers");
          break;
        case 4110:
          text = SK.Text("GENERIC_Pikemen", "Pikemen");
          break;
        case 4111:
          text = SK.Text("GENERIC_Swordsmen", "Swordsmen");
          break;
        case 4112:
          text = SK.Text("GENERIC_Catapults", "Catapults");
          break;
        case 4113:
          text = SK.Text("GENERIC_Captains", "Captains");
          break;
        case 4114:
          text = SK.Text("TOOLTIPS_VO_TROOPS_EXPAND", "Expand for Detailed Troop Information");
          break;
        case 4115:
          text = SK.Text("TOOLTIPS_VO_TROOPS_COLLAPSE", "Close Detailed Troop Information");
          break;
        case 4116:
          text = SK.Text("TOOLTIPS_VO_SCOUTS_EXTRA", "Total Scouts (Active Scouts)");
          break;
        case 4117:
          text = SK.Text("TOOLTIPS_VO_MERCHANTS_EXTRA", "Total Merchants (Active Merchants)");
          break;
        case 4118:
          text = SK.Text("TOOLTIPS_VO_MONKS_EXTRA", "Total Monks (Active Monks)");
          break;
        case 4119:
          text = SK.Text("VillageMapPanel_Tax_Income", "Tax Income");
          break;
        case 4120:
          text = SK.Text("TOOLTIPS_VO_RATIONS", "Rations");
          break;
        case 4121:
          text = SK.Text("TOOLTIPS_VO_ALE_RATIONS", "Ale Rations");
          break;
        case 4122:
          text = SK.Text("TOOLTIPS_VO_PEOPLE", "Workers / Housing Capacity - Spare Workers");
          break;
        case 4123:
          text = SK.Text("TOOLTIPS_VO_INTERDICTION", "This village is Interdicted.") + AllVillagesPanel.getTooltipDate(CustomTooltipManager.currentOverData);
          break;
        case 4124:
          text = SK.Text("TOOLTIPS_VO_EXCOMMUNICATION", "This village is Excommunicated.") + AllVillagesPanel.getTooltipDate(CustomTooltipManager.currentOverData);
          break;
        case 4125:
          text = SK.Text("TOOLTIPS_VO_PEACETIME", "This village is in Peacetime.") + AllVillagesPanel.getTooltipDate(CustomTooltipManager.currentOverData);
          break;
        case 4126:
          text = SK.Text("TOOLTIPS_VO_NOT_PREMIUM", "A Premium Token is required to view this information.");
          break;
        case 4127:
          text = SK.Text("TOOLTIPS_VO_Iron", "Current Iron Level");
          break;
        case 4128:
          text = SK.Text("TOOLTIPS_VO_Pitch", "Current Pitch Level");
          break;
        case 4140:
          text = SK.Text("TOOLTIPS_VO_Damaged", "Castle Requires Repair");
          break;
        case 4200:
          text = SK.Text("TOOLTIPS_PROCLAMATION_HELP_PARISH", "As Parish Steward, you can click here to send a mail to all your Parish members. This feature can only be used once every 7 days.");
          break;
        case 4201:
          text = SK.Text("TOOLTIPS_PROCLAMATION_HELP_COUNTY", "As County Sheriff, you can click here to send a mail to all your County members. This feature can only be used once every 7 days.");
          break;
        case 4202:
          text = SK.Text("TOOLTIPS_PROCLAMATION_HELP_PROVINCE", "As Province Governor, you can click here to send a mail to all Parish Stewards within your Province. This feature can only be used once every 3 days.");
          break;
        case 4203:
          text = SK.Text("TOOLTIPS_PROCLAMATION_HELP_COUNTRY", "As Monarch, you can click here to send a mail to all County Sheriffs within your Country. This feature can only be used once every 3 days.");
          break;
        case 4300:
          text = SK.Text("GENERIC_Research", "Research") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_RESEARCH", "Research new technology and boost productivity on the tech tree");
          break;
        case 4301:
          text = SK.Text("STATS_CATEGORY_TITLE_RANK", "Rank") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_RANK", "Rank up to unlock new research, villages and abilities");
          break;
        case 4302:
          text = SK.Text("GENERIC_Achievements", "Achievements") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_ACHIEVEMENTS", "Master the game by unlocking every achievement");
          break;
        case 4303:
          text = SK.Text("GENERIC_Quests", "Quests") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_QUESTS", "Complete Quests for gold, honour, resources, cards and more");
          break;
        case 4304:
          text = SK.Text("Reports_Reports", "Reports") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_REPORTS", "Check the reports screen for incoming attacks and local news");
          break;
        case 4305:
          text = SK.Text("GENERIC_CoatOfArms", "Coat of Arms") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_COAT_OF_ARMS", "Create a unique shield design and display it on the World Map");
          break;
        case 4306:
          text = SK.Text("AvatarEditor_Avatar", "Avatar") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_AVATAR", "Design your Avatar and display it on the World Map");
          break;
        case 4307:
          text = SK.Text("MENU_Invite_A_Friend", "Invite a Friend") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_INVITE_A_FRIEND", "Earn up to 1500 Crowns when you Invite a Friend");
          break;
        case 4308:
          text = SK.Text("GENERIC_ParishWall", "Parish Wall") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_PARISH_WALL", "Introduce yourself on the Parish Wall");
          break;
        case 4309:
          text = SK.Text("MailScreen_Mail", "Mail") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_MAIL", "Check your mail and communicate with other players");
          break;
        case 4400:
          text = SK.Text("TOOLTIPS_WIKI_HELP_LINK", "Wiki Help Link");
          switch (CustomTooltipManager.currentOverData)
          {
            case 0:
              text = SK.Text("TOOLTIP_WIKI_WORLD_MAP", "What is the World Map?");
              break;
            case 1:
              text = SK.Text("TOOLTIP_WIKI_VILLAGE_VILLAGE_MAP", "What are Villages?");
              break;
            case 2:
              text = SK.Text("TOOLTIP_WIKI_VILLAGE_CASTLE_MAP", "What are Castles?");
              break;
            case 3:
              text = SK.Text("TOOLTIP_WIKI_VILLAGE_RESOURCES", "What are Resources?");
              break;
            case 4:
              text = SK.Text("TOOLTIP_WIKI_VILLAGE_TRADE", "How To: Trade");
              break;
            case 5:
              text = SK.Text("TOOLTIP_WIKI_VILLAGE_TROOPS", "How To: Recruit Troops");
              break;
            case 6:
              text = SK.Text("TOOLTIP_WIKI_VILLAGE_UNITS", "How To: Recruit Units");
              break;
            case 7:
              text = SK.Text("TOOLTIP_WIKI_VILLAGE_HOLD_A_BANQUET", "What are Banquets?");
              break;
            case 8:
              text = SK.Text("TOOLTIP_WIKI_VILLAGE_VASSALS", "What are Vassals & Liege Lords?").Replace("&amp;", "&");
              break;
            case 9:
              text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_VILLAGE_MAP", "What is the Capital Town?");
              break;
            case 10:
              text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_CASTLE_MAP", "What is the Capital Castle?");
              break;
            case 11:
              text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_RESOURCES", "What are Resources?");
              break;
            case 12:
              text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_TRADE", "What are Parishes & Capitals").Replace("&amp;", "&");
              break;
            case 13:
              text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_TROOPS", "What are Capital Troops");
              break;
            case 14:
              text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_TRADE", "What are Parishes & Capitals").Replace("&amp;", "&");
              break;
            case 15:
              text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_VOTE", "How To: Vote");
              break;
            case 16:
              text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_PARISH_FORUM", "What is the Parish Forum?");
              break;
            case 17:
              text = SK.Text("TOOLTIP_WIKI_RESEARCH", "What is Research?");
              break;
            case 18:
              text = SK.Text("TOOLTIP_WIKI_RANK", "What are Ranks?");
              break;
            case 19:
              text = SK.Text("TOOLTIP_WIKI_QUESTS", "What are Quests?");
              break;
            case 20:
              text = SK.Text("TOOLTIP_WIKI_ATTACKS", "How To: Attack");
              break;
            case 21:
              text = SK.Text("TOOLTIP_WIKI_REPORTS", "What are Reports?");
              break;
            case 22:
              text = SK.Text("TOOLTIP_WIKI_FACTIONS_HOUSES_GLORY", "What is Glory?");
              break;
            case 23:
              text = SK.Text("TOOLTIP_WIKI_FACTIONS_HOUSES_FACTION", "What are Factions?");
              break;
            case 24:
              text = SK.Text("TOOLTIP_WIKI_FACTIONS_HOUSES_HOUSE", "What are Houses?");
              break;
            case 25:
              text = SK.Text("TOOLTIP_WIKI_CARDS", "What are Strategy Cards?");
              break;
            case 26:
              text = SK.Text("TOOLTIP_WIKI_MAIL", "How To: Use Mail");
              break;
            case 27:
              text = SK.Text("TOOLTIP_WIKI_CHAT", "How To: Use Chat");
              break;
            case 28:
              text = SK.Text("TOOLTIP_WIKI_LEADERBOARD", "What is the Leaderboard?");
              break;
            case 30:
              text = SK.Text("TOOLTIP_WIKI_SETTINGS", "How To: View Options & Settings").Replace("&amp;", "&");
              break;
            case 32:
              text = SK.Text("TOOLTIP_WIKI_QUEST_WHEEL", "What is the Quest Wheel?");
              break;
            case 33:
              text = SK.Text("TOOLTIP_WIKI_SEND_ATTACK", "How To: Send Attacks");
              break;
            case 34:
              text = SK.Text("TOOLTIP_WIKI_SEND_SCOUTS", "How To: Send Scouts");
              break;
            case 35:
              text = SK.Text("TOOLTIP_WIKI_SEND_MONKS", "How To: Send Monks");
              break;
            case 37:
              text = SK.Text("TOOLTIP_WIKI_BUY_PREMIUM_TOKENS", "What are Premium Tokens?");
              break;
            case 38:
              text = SK.Text("TOOLTIP_WIKI_BUY_CROWNS", "What are Crowns?");
              break;
            case 39:
              text = SK.Text("TOOLTIP_WIKI_BUY_CARDS", "What are Strategy Cards?");
              break;
            case 40:
              text = SK.Text("TOOLTIP_WIKI_SWAP_CARDS", "How To: Swap Cards");
              break;
            case 41:
              text = SK.Text("TOOLTIP_WIKI_LOGOUT", "What are Premium Tokens?");
              break;
            case 42:
              text = SK.Text("TOOLTIP_WIKI_DONATE_TO_PARISH", "How To: Donate to Parish");
              break;
            case 43:
              text = SK.Text("TOOLTIP_WIKI_VILLAGES_OVERVIEW", "What is the Village Overview?");
              break;
            case 44:
              text = SK.Text("TOOLTIP_WIKI_ACHIEVEMENTS", "What are Achievements?");
              break;
            case 45:
              text = GameEngine.Instance.LocalWorldData.EraWorld ? SK.Text("TOOLTIP_WIKI_SECONDERA", "What is the Second Era?") : SK.Text("TOOLTIP_WIKI_SECONDAGE", "What is the Second Age?");
              break;
            case 46:
              text = GameEngine.Instance.LocalWorldData.EraWorld ? SK.Text("TOOLTIP_WIKI_THIRDERA", "What is the Third Era?") : SK.Text("TOOLTIP_WIKI_THIRDAGE", "What is the Third Age?");
              break;
            case 47:
              text = SK.Text("TOOLTIP_WIKI_DOMINATION_RULES", "Domination World Explained?");
              break;
            case 48:
              text = GameEngine.Instance.LocalWorldData.EraWorld ? SK.Text("TOOLTIP_WIKI_FOURTHERA", "What is the Fourth Era?") : SK.Text("TOOLTIP_WIKI_FOURTHAGE", "What is the Fourth Age?");
              break;
            case 49:
              text = SK.Text("TOOLTIP_WIKI_TREASURE_CASTLES", "What is a Treasure Castle?");
              break;
            case 50:
              text = SK.Text("TOOLTIP_WIKI_PALADIN_CASTLES", "What is a Paladin Castle?");
              break;
            case 51:
              text = GameEngine.Instance.LocalWorldData.EraWorld ? SK.Text("TOOLTIP_WIKI_FIFTHERA", "What is the Fifth Era?") : SK.Text("TOOLTIP_WIKI_FIFTHAGE", "What is the Fifth Age?");
              break;
            case 52:
              text = GameEngine.Instance.LocalWorldData.EraWorld ? SK.Text("TOOLTIP_WIKI_SIXTHERA", "What is the Sixth Era?") : SK.Text("TOOLTIP_WIKI_SIXTHAGE", "What is the Sixth Age?");
              break;
            case 53:
              text = GameEngine.Instance.LocalWorldData.EraWorld ? SK.Text("TOOLTIP_WIKI_FINAL_ERA", "What is the Final Era?") : SK.Text("TOOLTIP_WIKI_FINAL_AGE", "What is the Final Age?");
              break;
          }
          break;
        case 4401:
          text = SK.Text("TOOLTIP_WIKI_CHAT", "How To: Use Chat");
          break;
        case 4402:
          text = SK.Text("TOOLTIP_WIKI_SETTINGS", "How To: View Options & Settings").Replace("&amp;", "&");
          break;
        case 10000:
        case 10101:
          text = "";
          break;
        case 10001:
          text = SK.Text("TOOLTIPS_CARD_BAR_PLAY_CARDS", "Click to Play Cards");
          break;
        case 10002:
          text = SK.Text("TOOLTIPS_CARD_BAR_EXPAND", "Show Available Cards");
          break;
        case 10003:
          text = SK.Text("TOOLTIPS_CARD_BAR_COLLAPSE", "Hide Available Cards");
          break;
        case 10004:
          text = SK.Text("TOOLTIPS_CARD_BAR_NEXT", "Show Next Set");
          break;
        case 10005:
          text = SK.Text("TOOLTIPS_CARD_BAR_PREV", "Show Previous Set");
          break;
        case 10100:
          text = SK.Text("TOOLTIPS_CARD_WINDOW_CLOSE", "Close Window");
          break;
        case 10102:
          text = SK.Text("TOOLTIPS_CARD_WINDOW_FILTER", "Filter Cards: ") + CardFilters.getName(CustomTooltipManager.currentOverData);
          break;
        case 10103:
          text = SK.Text("ManageCandsPanel_Cash_In", "Cash In");
          break;
        case 10104:
          text = SK.Text("ManageCandsPanel_Get_Cards", "Get Cards");
          break;
        case 10105:
          text = SK.Text("TOOLTIPS_CARD_WINDOW_FILTER", "Filter Cards: ") + CardFilters.getName2(CustomTooltipManager.currentOverData) + " (" + (object) GameEngine.Instance.cardsManager.countCardsInCategory(CustomTooltipManager.currentOverData) + ")";
          break;
        case 10301:
          text = SK.Text("CARD_OFFERS_Food_Pack", "Food Pack");
          break;
        case 10302:
          text = SK.Text("CARD_OFFERS_Castle_Pack", "Castle Pack");
          break;
        case 10303:
          text = SK.Text("CARD_OFFERS_Defense_Pack", "Defence Pack");
          break;
        case 10304:
          text = SK.Text("CARD_OFFERS_Army_Pack", "Army Pack");
          break;
        case 10305:
          text = SK.Text("CARD_OFFERS_Random_Pack", "Random Pack");
          break;
        case 10306:
          text = SK.Text("CARD_OFFERS_Industry_Pack", "Industry Pack");
          break;
        case 10307:
          text = SK.Text("CARD_OFFERS_Research_Pack", "Research Pack");
          break;
        case 10308:
          text = SK.Text("CARD_OFFERS_Exclusive_Pack", "Exclusive Pack");
          break;
        case 10309:
          text = SK.Text("CARD_OFFERS_Super_Food_Pack", "Super Food Pack");
          break;
        case 10310:
          text = SK.Text("CARD_OFFERS_Super_Defense_Pack", "Super Defence Pack");
          break;
        case 10311:
          text = SK.Text("CARD_OFFERS_Super_Army_Pack", "Super Army Pack");
          break;
        case 10312:
          text = SK.Text("CARD_OFFERS_Super_Random_Pack", "Super Random Pack");
          break;
        case 10313:
          text = SK.Text("CARD_OFFERS_Super_Industry_Pack", "Super Industry Pack");
          break;
        case 10314:
          text = SK.Text("CARD_OFFERS_Ultimate_Food_Pack", "Ultimate Food Pack");
          break;
        case 10315:
          text = SK.Text("CARD_OFFERS_Ultimate_Defense_Pack", "Ultimate Defence Pack");
          break;
        case 10316:
          text = SK.Text("CARD_OFFERS_Ultimate_Army_Pack", "Ultimate Army Pack");
          break;
        case 10317:
          text = SK.Text("CARD_OFFERS_Ultimate_Random_Pack", "Ultimate Random Pack");
          break;
        case 10318:
          text = SK.Text("CARD_OFFERS_Ultimate_Industry_Pack", "Ultimate Industry Pack");
          break;
        case 10319:
          text = SK.Text("CARDS_SEARCH_CARDS", "Search for Cards by Name");
          break;
        case 10320:
          text = SK.Text("CARDS_CLEAR_SEARCH_CARDS", "Close Search");
          break;
        case 10321:
          text = SK.Text("CARD_OFFERS_Platinum_Pack", "Platinum Pack");
          break;
        case 10350:
          text = SK.Text("TOOLTIPS_Aeria_Points", "Click to Purchase AP");
          break;
        case 10390:
          text = SK.Text("TOOLTIPS_MAPSIDE_RENAME", "Mod Command : Reset Village Name to Default");
          break;
        case 10500:
          text = SK.Text("TOOLTIPS_FREE_CARDS_MAIN", "Click to collect your Free Cards");
          break;
        case 10501:
          text = SK.Text("TOOLTIPS_ROYAL_TICKETS_MAIN", "Click to spin the Quest Wheel");
          break;
        case 10502:
          text = SK.Text("AI_WolfsRevenge", "Wolf's Revenge") + Environment.NewLine + SK.Text("AI_EndsIn", "Ends In") + " : " + VillageMap.createBuildTimeString(CustomTooltipManager.currentOverData);
          break;
        case 20000:
          text = VillageMap.createBuildTimeString(CustomTooltipManager.currentOverData);
          break;
        case 21000:
          text = SK.Text("TOOLTIPS_TRACK_BAR_HINT", "Double-click to enter amount");
          break;
        case 22000:
          text = SK.Text("TOOLTIP_PLAYBACK_STOP", "Stop");
          break;
        case 22001:
          text = SK.Text("TOOLTIP_PLAYBACK_PAUSE", "Pause");
          break;
        case 22002:
          text = SK.Text("TOOLTIP_PLAYBACK_PLAY", "Play");
          break;
        case 22003:
          text = SK.Text("TOOLTIP_PLAYBACK_SPEED1", "Normal speed");
          break;
        case 22004:
          text = SK.Text("TOOLTIP_PLAYBACK_SPEED2", "Double speed");
          break;
        case 22005:
          text = SK.Text("TOOLTIP_PLAYBACK_SPEED4", "Quadruple speed");
          break;
        case 22006:
          text = SK.Text("TOOLTIP_PLAYBACK_EXPAND", "Show time-line");
          break;
        case 22007:
          text = SK.Text("TOOLTIP_PLAYBACK_COLLAPSE", "Hide time-line");
          break;
        case 23000:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_TRAVEL_PANELS_MINUS_5", "A Hurricane at sea means the journey will take 5 times longer than usual.");
          break;
        case 23001:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_TRAVEL_PANELS_MINUS_4", "Violent storms mean the journey will take 4 times longer than usual.");
          break;
        case 23002:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_TRAVEL_PANELS_MINUS_3", "Stormy seas mean the journey will take 3 times longer than usual.");
          break;
        case 23003:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_TRAVEL_PANELS_MINUS_2", "Rough seas mean the journey will take twice as long as usual.");
          break;
        case 23004:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_TRAVEL_PANELS_NEUTRAL", "Calm seas mean the journey will take the standard journey time.");
          break;
        case 23005:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_TRAVEL_PANELS_PLUS_2", "A Fresh Breeze means the journey will take half as long as usual.");
          break;
        case 23006:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_TRAVEL_PANELS_PLUS_3", "Fair winds means the journey will take one third of the usual time.");
          break;
        case 23007:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_TRAVEL_PANELS_PLUS_4", "Strong winds mean the journey will take a quarter of the usual time.");
          break;
        case 23008:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_TRAVEL_PANELS_PLUS_5", "Tail winds mean the journey will take one fifth of the usual time.");
          break;
        case 23010:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_MAP_MINUS_5", "A Hurricane at sea means that inter-island journeys will take 5 times longer than usual.");
          break;
        case 23011:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_MAP_MINUS_4", "Violent storms mean that inter-island journeys will take 4 times longer than usual.");
          break;
        case 23012:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_MAP_MINUS_3", "Stormy seas mean that inter-island journeys will take 3 times longer than usual.");
          break;
        case 23013:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_MAP_MINUS_2", "Rough seas mean that inter-island journeys will take twice as long as usual.");
          break;
        case 23014:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_MAP_NEUTRAL", "Calm seas mean that inter-island journeys will take the standard journey time.");
          break;
        case 23015:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_MAP_PLUS_2", "A Fresh Breeze means that inter-island journeys will take half as long as usual.");
          break;
        case 23016:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_MAP_PLUS_3", "Fair winds means that inter-island journeys will take one third of the usual time.");
          break;
        case 23017:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_MAP_PLUS_4", "Strong winds mean that inter-island journeys will take a quarter of the usual time.");
          break;
        case 23018:
          text = SK.Text("TOOLTIP_SEA_CONDITIONS_MAP_PLUS_5", "Tail winds mean that inter-island journeys will take one fifth of the usual time.");
          break;
        case 24000:
          int total = 0;
          int num9 = GameEngine.Instance.World.countRemainingRoyalTowers(ref total);
          text = SK.Text("TOOLTIP_ROYAL_TOWER_ICON", "Royal Towers to be Captured Before a House Marshall can push the Button : ") + num9.ToString() + "/" + total.ToString();
          break;
        case 24100:
          int totalSeconds2 = (int) (GameEngine.Instance.World.KillStreakTimer - VillageMap.getCurrentServerTime()).TotalSeconds;
          if (totalSeconds2 < 0)
          {
            text = SK.Text("TOOLTIP_NO_KILL_STREAK", "You have no Kill Streak");
            break;
          }
          string buildTimeString = VillageMap.createBuildTimeString(totalSeconds2);
          int index1 = GameEngine.Instance.World.KillStreakCount - 1;
          int num10;
          int num11;
          double num12;
          if (index1 < 100)
          {
            num10 = Wheel.killstreak_honour[index1];
            num11 = Wheel.killstreak_fpcap[index1];
            num12 = (double) Wheel.killstreak_spinbonus[index1] / 100.0;
          }
          else
          {
            num10 = index1 - 100 + 251;
            num11 = (index1 - 100) * 2 + 202;
            num12 = 35.0;
          }
          NumberFormatInfo nfi5 = GameEngine.NFI;
          text = SK.Text("TOOLTIP_KILL_STREAK_1", "Current Kill Streak") + " : " + (index1 + 1).ToString("N", (IFormatProvider) nfi5) + Environment.NewLine + SK.Text("TOOLTIP_KILL_STREAK_2", "Kill Streak Expires in") + " : " + buildTimeString + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIP_KILL_STREAK_3", "Bonuses While Attacking the AI") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIP_KILL_STREAK_4", "Honour Boost") + " : " + num10.ToString() + "%" + Environment.NewLine + SK.Text("TOOLTIP_KILL_STREAK_5", "Faith Points Cap Increment Boost") + " : " + num11.ToString() + "%" + Environment.NewLine;
          if (num12 > 0.0)
          {
            text = text + SK.Text("TOOLTIP_KILL_STREAK_6", "Bonus Wheel Spin Chance") + " : " + num12.ToString("N", (IFormatProvider) GameEngine.NFI_D2) + "%" + Environment.NewLine;
            break;
          }
          break;
        case 25001:
          SK.Text("TOOLTIP_TOURNEY_PRIZE_AVAILABLE", "Collect Tourney Prize");
          text = CustomTooltipManager.currentOverData <= 1 ? SK.Text("TOOLTIP_TOURNEY_PRIZE_AVAILABLE", "Collect Tourney Prize") : SK.Text("TOOLTIP_TOURNEY_PRIZE_AVAILABLE_PLURAL", "Collect Tourney Prizes") + " (" + CustomTooltipManager.currentOverData.ToString() + ")";
          break;
        case 25002:
          text = SK.Text("TOOLTIP_TOURNEY_ONGOING", "Ongoing Tourney: ") + GameEngine.Instance.World.contestName + Environment.NewLine + SK.Text("TOOLTIP_TOURNEY_REMAINING_TIME", "Time Remaining: ") + VillageMap.createBuildTimeString(CustomTooltipManager.currentOverData);
          break;
        case 25003:
          text = SK.Text("STATS_CATEGORY_TITLE_RANK", "Rank");
          switch (CustomTooltipManager.currentOverData)
          {
            case 1:
              text += " 1 - 11";
              break;
            case 2:
              text += " 12 - 17";
              break;
            case 3:
              text += " 18 - 23";
              break;
          }
          break;
        case 25100:
          text = SK.Text("TOOLTIP_SALE_ENDS", "Sale Ends In") + " " + VillageMap.createBuildTimeString(CustomTooltipManager.currentOverData);
          break;
        case 25101:
          text = SK.Text("TOUCH_Z_PurchaseSpecialOffer", "Purchase Special Offer");
          break;
      }
      CustomTooltip.CreateToolTip(text, ID, CustomTooltipManager.currentOverData, currentParentWindow);
    }

    public static string getAchievementRank(int achievement)
    {
      switch (achievement & 1879048192)
      {
        case 268435456:
          return SK.Text("Achievements_Silver", "Silver");
        case 536870912:
          return SK.Text("Achievements_Gold", "Gold");
        case 1073741824:
          return SK.Text("Achievements_Diamond", "Diamond");
        case 1342177280:
          return SK.Text("Achievements_Diamond2", "Double Diamond");
        case 1610612736:
          return SK.Text("Achievements_Diamond3", "Triple Diamond");
        case 1879048192:
          return SK.Text("Achievements_Sapphire", "Sapphire");
        default:
          return SK.Text("Achievements_Iron", "Iron");
      }
    }

    public static string getAchievementTitle(int achievement)
    {
      switch (achievement & 268435455)
      {
        case 1:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR", "Protector");
        case 2:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER", "Law Bringer");
        case 3:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR", "Warrior");
        case 4:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER", "Wolf Hunter");
        case 5:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD", "Weregild");
        case 6:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN", "Ratty lost again!");
        case 7:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL", "Snakes Downfall");
        case 8:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY", "Squeal Piggy!");
        case 9:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE", "Wolf Bane");
        case 10:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER", "Flag Raider");
        case 11:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER", "Firestarter");
        case 12:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR", "Conqueror");
        case 13:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING", "Viking");
        case 14:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL", "Vandal");
        case 15:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD", "Evil Lord");
        case 16:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASURE_HUNTER", "Treasure Hunter");
        case 33:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_CANT_TOUCH_ME", "Can't touch me");
        case 34:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEFENSIVE_MASTER", "Defensive master");
        case 35:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_HELPING_HAND", "Helping Hand");
        case 36:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_ROCKHARD", "Rock hard");
        case 37:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER", "Vanquisher");
        case 65:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY", "Ballista Crazy");
        case 66:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT", "Feel the Heat");
        case 67:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP", "Deathtrap");
        case 97:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEAVY_DRINKER", "Heavy Drinker");
        case 98:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLUTTON", "Glutton");
        case 99:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_GARDENER", "Gardener");
        case 100:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER", "Miser");
        case 101:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY", "Charity");
        case 129:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER", "Healer");
        case 130:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER", "Peacebringer");
        case 131:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT", "Diplomat");
        case 161:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER", "Horse Master");
        case 162:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED", "Lightning Speed");
        case 163:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER", "Master Forager");
        case 193:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLORY_STAR", "Glory Star");
        case 194:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_KINGS_KIN", "King's Kin");
        case 195:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_LOYAL_MEMBER", "Loyal Member");
        case 225:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_GENIUS", "Genius");
        case 226:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_LEARNED_SCHOLAR", "Learned Scholar");
        case 257:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER", "Stockbroker");
        case 289:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_FINE_DINING", "Fine Dining");
        case 290:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING", "Banquet King");
        case 321:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_FAME", "Fame");
        case 353:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER", "Team player");
        case 354:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_SKILLED_RULER", "Skilled ruler");
        case 385:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_OFFICER", "Officer");
        case 386:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_HIGH_OFFICER", "High Office");
        case 387:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_MIGHTY_RULER", "Mighty Ruler");
        case 388:
          return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIONHEART", "Lionheart");
        default:
          return "";
      }
    }

    public static string getAchievementRequirement(int achievement, int rankLevel)
    {
      if (rankLevel < 0 || rankLevel > 6)
        return "";
      switch (achievement)
      {
        case 1:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR_1", "Kill 20 wolves");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR_2", "Kill 200 wolves ");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR_3", "Kill 1,000 wolves");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR_4", "Kill 10,000 wolves");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR_5", "Kill 50,000 wolves");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR_6", "Kill 100,000 wolves");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR_7", "Kill 250,000 wolves");
          }
          break;
        case 2:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER1", "Kill 10 bandits");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER2", "Kill 100 bandits");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER3", "Kill 500 bandits");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER4", "Kill 5,000 bandits");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER5", "Kill 10,000 bandits");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER6", "Kill 20,000 bandits");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER7", "Kill 50,000 bandits");
          }
          break;
        case 3:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR1", "Kill 50 troops as an attacker");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR2", "Kill 1,000 troops as an attacker");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR3", "Kill 20,000 troops as an attacker");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR5", "Kill 50,000 troops as an attacker");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR6", "Kill 75,000 troops as an attacker");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR7", "Kill 100,000 troops as an attacker");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR4", "Kill 250,000 troops as an attacker");
          }
          break;
        case 4:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER1", "Destroy 3 Wolf Lairs");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER2", "Destroy 20 Wolf Lairs");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER3", "Destroy 100 Wolf Lairs");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER4", "Destroy 300 Wolf Lairs");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER5", "Destroy 1,000 Wolf Lairs");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER6", "Destroy 5,000 Wolf Lairs");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER7", "Destroy 15,000 Wolf Lairs");
          }
          break;
        case 5:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD1", "Destroy 2 Bandit Camps");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD2", "Destroy 10 Bandit Camps");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD3", "Destroy 50 Bandit Camps");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD4", "Destroy 200 Bandit Camps");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD5", "Destroy 400 Bandit Camps");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD6", "Destroy 700 Bandit Camps");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD7", "Destroy 1,000 Bandit Camps");
          }
          break;
        case 6:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN1", "Destroy 1 Rat's Castle");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN2", "Destroy 3 Rat's Castles");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN3", "Destroy 10 Rat's Castles");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN4", "Destroy 25 Rat's Castles");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN5", "Destroy 50 Rat's Castles");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN6", "Destroy 75 Rat's Castles");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN7", "Destroy 100 Rat's Castles");
          }
          break;
        case 7:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL1", "Destroy 1 Snakes Castle");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL2", "Destroy 3 Snakes Castles");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL3", "Destroy 10 Snakes Castles");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL4", "Destroy 25 Snakes Castles");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL5", "Destroy 50 Snakes Castles");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL6", "Destroy 75 Snakes Castles");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL7", "Destroy 100 Snakes Castles");
          }
          break;
        case 8:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY1", "Destroy 1 Pig's Castle");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY2", "Destroy 3 Pig's Castles");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY3", "Destroy 10 Pig's Castles");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY4", "Destroy 25 Pig's Castles");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY5", "Destroy 50 Pig's Castles");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY6", "Destroy 75 Pig's Castles");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY7", "Destroy 100 Pig's Castles");
          }
          break;
        case 9:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE1", "Destroy 1 Wolf's Castles");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE2", "Destroy 3 Wolf's Castles");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE3", "Destroy 10 Wolf's Castles");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE4", "Destroy 25 Wolf's Castles");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE5", "Destroy 50 Wolf's Castles");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE6", "Destroy 75 Wolf's Castles");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE7", "Destroy 100 Wolf's Castles");
          }
          break;
        case 10:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER1", "Capture 2 Flags for your parish");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER2", "Capture 10 Flags for your parish");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER3", "Capture 50 Flags for your parish");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER4", "Capture 250 Flags for your parish");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER5", "Capture 500 Flags for your parish");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER6", "Capture 750 Flags for your parish");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER7", "Capture 1,000 Flags for your parish");
          }
          break;
        case 11:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER1", "Raze 1 village");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER2", "Raze 3 villages");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER3", "Raze 10 villages");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER4", "Raze 50 villages");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER5", "Raze 100 villages");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER6", "Raze 150 villages");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER7", "Raze 250 villages");
          }
          break;
        case 12:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR1", "Capture 1 village");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR2", "Capture 3 villages");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR3", "Capture 7 villages");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR4", "Capture 20 villages");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR5", "Capture 40 villages");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR6", "Capture 60 villages");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR7", "Capture 100 villages");
          }
          break;
        case 13:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING1", "Pillage a total of 10 packets of goods from another player");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING2", "Pillage a total of 100 packets of goods from another player");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING3", "Pillage a total of 2,000 packets of goods from another player");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING4a", "Pillage a total of 10,000 packets of goods from another player");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING5", "Pillage a total of 20,000 packets of goods from another player");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING6", "Pillage a total of 40,000 packets of goods from another player");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING7", "Pillage a total of 75,000 packets of goods from another player");
          }
          break;
        case 14:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL1", "Destroy 2 buildings through ransacking a village");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL2", "Destroy 20 buildings through ransacking a village");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL3", "Destroy 200 buildings through ransacking a village");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL4", "Destroy 2,000 buildings through ransacking a village");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL5", "Destroy 3,000 buildings through ransacking a village");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL6", "Destroy 4,000 buildings through ransacking a village");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL7", "Destroy 5,000 buildings through ransacking a village");
          }
          break;
        case 15:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD1", "Destroy 1 Paladin's Castle");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD2", "Destroy 3 Paladin's Castles");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD3", "Destroy 10 Paladin's Castles");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD4", "Destroy 25 Paladin's Castles");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD5", "Destroy 50 Paladin's Castles");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD6", "Destroy 75 Paladin's Castles");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD7", "Destroy 100 Paladin's Castles");
          }
          break;
        case 16:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASUREHUNTER1", "Capture 1 Treasure Chest from Treasure Castles");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASUREHUNTER2", "Capture 3 Treasure Chests from Treasure Castles");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASUREHUNTER3", "Capture 10 Treasure Chests from Treasure Castles");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASUREHUNTER4", "Capture 25 Treasure Chests from Treasure Castles");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASUREHUNTER5", "Capture 50 Treasure Chests from Treasure Castles");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASUREHUNTER6", "Capture 75 Treasure Chests from Treasure Castles");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASUREHUNTER7", "Capture 100 Treasure Chests from Treasure Castles");
          }
          break;
        case 33:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CANT_TOUCH_ME1", "Survive attacks 3 consecutive enemy attacks");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CANT_TOUCH_ME2", "Survive attacks 12 consecutive enemy attacks");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CANT_TOUCH_ME3", "Survive attacks 50 consecutive enemy attacks");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CANT_TOUCH_ME4", "Survive attacks 250 consecutive enemy attacks");
          }
          break;
        case 34:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEFENSIVE_MASTER1", "Survive an attack from an army of over 20 troops");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEFENSIVE_MASTER2", "Survive an attack from an army of over 100 troops");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEFENSIVE_MASTER3", "Survive an attack from an army of over 250 troops");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEFENSIVE_MASTER4", "Survive an attack from an army of over 400 troops");
          }
          break;
        case 35:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HELPING_HAND1", "Your reinforcements have helped defend 1 castle");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HELPING_HAND2", "Your reinforcements have helped defend 5 castles");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HELPING_HAND3", "Your reinforcements have helped defend 25 castles");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HELPING_HAND4", "Your reinforcements have helped defend 200 castles");
          }
          break;
        case 36:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_ROCKHARD1", "A castle survives - killing over 500 troops in a server day");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_ROCKHARD2", "A castle survives - killing over 1,000 troops in a server day");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_ROCKHARD3", "A castle survives - killing over 4,000 troops in a server day");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_ROCKHARD4", "A castle survives - killing over 10,000 troops in a server day");
          }
          break;
        case 37:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER1", "Kill 100 troops defending your castles");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER2", "Kill 1,000 troops defending your castles");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER3", "Kill 10,000 troops defending your castles");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER4", "Kill 100,000 troops defending your castles");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER5", "Kill 250,000 troops defending your castles");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER6", "Kill 500,000 troops defending your castles");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER7", "Kill 1,000,000 troops defending your castles");
          }
          break;
        case 65:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY1", "Fire over 50 ballistae bolts in your castles");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY2", "Fire over 250 ballistae bolts in your castles");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY3", "Fire over 4,000 ballistae bolts in your castles");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY4", "Fire over 25,000 ballistae bolts in your castles");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY5", "Fire over 100,000 ballistae bolts in your castles");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY6", "Fire over 500,000 ballistae bolts in your castles");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY7", "Fire over 1,000,000 ballistae bolts in your castles");
          }
          break;
        case 66:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT1", "Pour over 3 oil pots in your castles");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT2", "Pour over 25 oil pots in your castles");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT3", "Pour over 200 oil pots in your castles");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT4", "Pour over 3,000 oil pots in your castles");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT5", "Pour over 10,000 oil pots in your castles");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT6", "Pour over 50,000 oil pots in your castles");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT7", "Pour over 100,000 oil pots in your castles");
          }
          break;
        case 67:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP1", "Over 20 killing pits triggered in your castles");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP2", "Over 200 killing pits triggered in your castles");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP3", "Over 2,000 killing pits triggered in your castles");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP4", "Over 10,000 killing pits triggered in your castles");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP5", "Over 25,000 killing pits triggered in your castles");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP6", "Over 50,000 killing pits triggered in your castles");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP7", "Over 100,000 killing pits triggered in your castles");
          }
          break;
        case 97:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEAVY_DRINKER1", "Place 5 hop farms in your villages");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEAVY_DRINKER2", "Place 10 hop farms in your villages");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEAVY_DRINKER3", "Place 30 hop farms in your villages");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEAVY_DRINKER4", "Place 60 hop farms in your villages");
          }
          break;
        case 98:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLUTTON1", "Place 20 food farms in your villages");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLUTTON2", "Place 50 food farms in your villages");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLUTTON3", "Place 120 food farms in your villages");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLUTTON4", "Place 250 food farms in your villages");
          }
          break;
        case 99:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GARDENER1", "Place 5 gardens in your villages");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GARDENER2", "Place 15 gardens in your villages");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GARDENER3", "Place 30 gardens in your villages");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GARDENER4", "Place 60 gardens in your villages");
          }
          break;
        case 100:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER1", "Store over 20,000 gold");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER2", "Store over 200,000 gold");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER3", "Store over 1,000,000 gold");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER4", "Store over 5,000,000 gold");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER5", "Store over 10,000,000 gold");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER6", "Store over 20,000,000 gold");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER7", "Store over 50,000,000 gold");
          }
          break;
        case 101:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY1", "Send another player over 10 packets of goods");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY2", "Send another player over 500 packets of goods");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY3", "Send another player over 2,000 packets of goods");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY4", "Send another player over 10,000 packets of goods");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY5", "Send another player over 20,000 packets of goods");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY6", "Send another player over 40,000 packets of goods");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY7", "Send another player over 60,000 packets of goods");
          }
          break;
        case 129:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER1", "Cure over 10 points of disease");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER2", "Cure over 100 points of disease");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER3", "Cure over 1,000 points of disease");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER4", "Cure over 10,000 points of disease");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER5", "Cure over 20,000 points of disease");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER6", "Cure over 35,000 points of disease");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER7", "Cure over 50,000 points of disease");
          }
          break;
        case 130:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER1", "Interdict 2 villages that you do not own");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER2", "Interdict 10 villages that you do not own");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER3", "Interdict 50 villages that you do not own");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER4", "Interdict 200 villages that you do not own");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER5", "Interdict 400 villages that you do not own");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER6", "Interdict 600 villages that you do not own");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER7", "Interdict 1,000 villages that you do not own");
          }
          break;
        case 131:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT1", "Influence an election by 10 votes ");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT2", "Influence an election by 100 votes ");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT3", "Influence an election by 500 votes ");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT4", "Influence an election by 2,000 votes ");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT5", "Influence an election by 5,000 votes ");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT6", "Influence an election by 10,000 votes ");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT7", "Influence an election by 25,000 votes ");
          }
          break;
        case 161:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER1", "Scout 10 resource stashes");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER2", "Scout 50 resource stashes");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER3", "Scout 250 resource stashes");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER4", "Scout 1,000 resource stashes");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER5", "Scout 10,000 resource stashes");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER6", "Scout 50,000 resource stashes");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER7", "Scout 100,000 resource stashes");
          }
          break;
        case 162:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED1", "Uncover 2 resource stashes");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED2", "Uncover 20 resource stashes");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED3", "Uncover 200 resource stashes");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED4", "Uncover 2,000 resource stashes");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED5", "Uncover 5,000 resource stashes");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED6", "Uncover 10,000 resource stashes");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED7", "Uncover 25,000 resource stashes");
          }
          break;
        case 163:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER1", "Bring back over 20 packets of goods");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER2", "Bring back over 200 packets of goods");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER3", "Bring back over 2,000 packets of goods");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER4", "Bring back over 20,000 packets of goods");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER5", "Bring back over 50,000 packets of goods");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER6", "Bring back over 75,000 packets of goods");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER7", "Bring back over 150,000 packets of goods");
          }
          break;
        case 193:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLORY_STAR1", "Be a member of a house that wins 1 glory round");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLORY_STAR2", "Be a member of a house that wins 2 glory rounds");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLORY_STAR3", "Be a member of a house that wins 3 glory rounds");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLORY_STAR4", "Be a member of a house that wins 4 glory rounds");
          }
          break;
        case 194:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_KINGS_KIN1", "Be a member of a house that controls a county");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_KINGS_KIN2", "Be a member of a house that controls a province");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_KINGS_KIN3", "Be a member of a house that controls a country");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_KINGS_KIN4", "Be a member of a house that controls more than 1 country");
          }
          break;
        case 195:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LOYAL_MEMBER1", "Spend 2 weeks in the same faction");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LOYAL_MEMBER2", "Spend 10 weeks in the same faction");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LOYAL_MEMBER3", "Spend 26 weeks in the same faction");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LOYAL_MEMBER4", "Spend 52 weeks in the same faction");
          }
          break;
        case 225:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GENIUS1", "Research everything in 1 branch of the tree");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GENIUS2", "Research everything in 2 branches of the tree");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GENIUS3", "Research everything in 3 branches of the tree");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_GENIUS4", "Research everything in the research tree");
          }
          break;
        case 226:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LEARNED_SCHOLAR1", "Complete 20 researches");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LEARNED_SCHOLAR2", "Complete 80 researches");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LEARNED_SCHOLAR3", "Complete 150 researches");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LEARNED_SCHOLAR4", "Complete 500 researches");
          }
          break;
        case 257:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER1", "Make over 1,000 gold from selling goods");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER2", "Make over 10,000 gold from selling goods");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER3", "Make over 100,000 gold from selling goods");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER4", "Make over 1,000,000 gold from selling goods");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER5", "Make over 10,000,000 gold from selling goods");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER6", "Make over 25,000,000 gold from selling goods");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER7", "Make over 50,000,000 gold from selling goods");
          }
          break;
        case 289:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FINE_DINING1", "Hold a banquet with at least 3 goods types");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FINE_DINING2", "Hold a banquet with at least 5 goods types");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FINE_DINING3", "Hold a banquet with at least 7 goods types");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FINE_DINING4", "Hold a banquet with all 8 goods types");
          }
          break;
        case 290:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING1", "Raise over 1,000 honour from banqueting");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING2", "Raise over 10,000 honour from banqueting");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING3", "Raise over 100,000 honour from banqueting");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING4", "Raise over 1,000,000 honour from banqueting");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING5", "Raise over 10,000,000 honour from banqueting");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING6", "Raise over 100,000,000 honour from banqueting");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING7", "Raise over 250,000,000 honour from banqueting");
          }
          break;
        case 321:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FAME1", "Reach the top 100 in any of the leaderboards");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FAME2", "Reach the top 20 in any of the leaderboards");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FAME3", "Reach the top 5 in any of the leaderboards");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_FAME4", "Reach the top position in any of the leaderboards");
          }
          break;
        case 353:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER1", "Donate 10 packets to capital buildings");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER2", "Donate 100 packets to capital buildings");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER3", "Donate 1,000 packets to capital buildings");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER4", "Donate 10,000 packets to capital buildings");
            case 4:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER5", "Donate 100,000 packets to capital buildings");
            case 5:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER6", "Donate 250,000 packets to capital buildings");
            case 6:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER7", "Donate 500,000 packets to capital buildings");
          }
          break;
        case 354:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SKILLED_RULER1", "Place 3 buildings in any capital town");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SKILLED_RULER2", "Place 15 buildings in any capital town");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SKILLED_RULER3", "Place 50 buildings in any capital town");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_SKILLED_RULER4", "Place 250 buildings in any capital town");
          }
          break;
        case 385:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_OFFICER1", "Become a Parish Steward");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_OFFICER2", "Hold the office of Steward in 2 parishes");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_OFFICER3", "Hold the office of Steward in 3 parishes");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_OFFICER4", "Hold the office of Steward in 4 parishes");
          }
          break;
        case 386:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HIGH_OFFICER1", "Become a Sheriff of a County");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HIGH_OFFICER2", "Hold the office of Sheriff in 2 Counties");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HIGH_OFFICER3", "Hold the office of Sheriff in 3 Counties");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_HIGH_OFFICER4", "Hold the office of Sheriff in 4 Counties");
          }
          break;
        case 387:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MIGHTY_RULER1", "Become a Governor of a Province ");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MIGHTY_RULER2", "Hold the office of Governor in 2 Provinces");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MIGHTY_RULER3", "Hold the office of Governor in 3 Provinces");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_MIGHTY_RULER4", "Hold the office of Governor in 4 Provinces");
          }
          break;
        case 388:
          switch (rankLevel)
          {
            case 0:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIONHEART1_Monarch", "Become the Monarch of a Country");
            case 1:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIONHEART2_Monarch", "Be the Monarch of 2 Countries");
            case 2:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIONHEART3_Monarch", "Be the Monarch of 3 Countries");
            case 3:
              return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIONHEART4_Monarch", "Be the Monarch of 4 Countries");
          }
          break;
      }
      return "";
    }

    private static bool dynamicUpdateTooltips(int ID)
    {
      switch (ID)
      {
        case 10000:
        case 24100:
          return true;
        default:
          return false;
      }
    }

    public static string getHouseMotto(int houseID)
    {
      int globalWorldId = GameEngine.Instance.World.GetGlobalWorldID();
      if (globalWorldId >= 1600 && globalWorldId < 1700)
      {
        switch (houseID)
        {
          case 0:
            return SK.Text("House_Motto_0", "The Dispossessed");
          case 1:
            return SK.Text("House_Motto_1_wl", "House of the Dragon");
          case 2:
            return SK.Text("House_Motto_2_wl", "House of the Tiger");
          case 3:
            return SK.Text("House_Motto_3_wl", "House of the Lion");
          case 4:
            return SK.Text("House_Motto_4_wl", "House of the Grasshopper");
          case 5:
            return SK.Text("House_Motto_5_wl", "House of the Dog");
          case 6:
            return SK.Text("House_Motto_6_wl", "House of the Rat");
          case 7:
            return SK.Text("House_Motto_7_wl", "House of the Peacock");
          case 8:
            return SK.Text("House_Motto_8_wl", "House of the Pig");
          case 9:
            return SK.Text("House_Motto_9_wl", "House of the Mouse");
          case 10:
            return SK.Text("House_Motto_10_wl", "House of the Panda");
          case 11:
            return SK.Text("House_Motto_11_wl", "House of the Wolf");
          case 12:
            return SK.Text("House_Motto_12_wl", "House of the Monkey");
          case 13:
            return SK.Text("House_Motto_13_wl", "House of the Beetle");
          case 14:
            return SK.Text("House_Motto_14_wl", "House of the Turtle");
          case 15:
            return SK.Text("House_Motto_15_wl", "House of the Eagle");
          case 16:
            return SK.Text("House_Motto_16_wl", "House of the Bull");
          case 17:
            return SK.Text("House_Motto_17_wl", "House of the Bear");
          case 18:
            return SK.Text("House_Motto_18_wl", "House of the Horse");
          case 19:
            return SK.Text("House_Motto_19_wl", "House of the Crane");
          case 20:
            return SK.Text("House_Motto_20_wl", "House of the Snake");
        }
      }
      else
      {
        switch (houseID)
        {
          case 0:
            return SK.Text("House_Motto_0", "The Dispossessed");
          case 1:
            return SK.Text("House_Motto_1", "The Heavenly Ones");
          case 2:
            return SK.Text("House_Motto_2", "High Castle");
          case 3:
            return SK.Text("House_Motto_3", "The Dragons");
          case 4:
            return SK.Text("House_Motto_4", "The Farmers");
          case 5:
            return SK.Text("House_Motto_5", "The Navigators");
          case 6:
            return SK.Text("House_Motto_6", "The Free Folk");
          case 7:
            return SK.Text("House_Motto_7", "The Royals");
          case 8:
            return SK.Text("House_Motto_8", "The Roses");
          case 9:
            return SK.Text("House_Motto_9", "The Rams");
          case 10:
            return SK.Text("House_Motto_10", "Fighters");
          case 11:
            return SK.Text("House_Motto_11", "Heroes");
          case 12:
            return SK.Text("House_Motto_12", "Stags");
          case 13:
            return SK.Text("House_Motto_13", "Oak");
          case 14:
            return SK.Text("House_Motto_14", "Beasts");
          case 15:
            return SK.Text("House_Motto_15", "The Pure");
          case 16:
            return SK.Text("House_Motto_16", "Lionheart");
          case 17:
            return SK.Text("House_Motto_17", "The Insane");
          case 18:
            return SK.Text("House_Motto_18", "Sinners");
          case 19:
            return SK.Text("House_Motto_19", "The Double-Eagles");
          case 20:
            return SK.Text("House_Motto_20", "Maidens");
        }
      }
      return "";
    }

    public class TooltipID
    {
      public const int NO_TOOLTIP = 0;
      public const int MAINWINDOW_CARDS_SHOW_ALL_CARDS = 1;
      public const int MAINWINDOW_TOP_LEFT_USERNAME = 2;
      public const int MAINWINDOW_TOP_LEFT_RANKING = 3;
      public const int MAINWINDOW_TOP_LEFT_HONOUR = 4;
      public const int MAINWINDOW_TOP_LEFT_GOLD = 5;
      public const int MAINWINDOW_TOP_LEFT_FAITHPOINTS = 6;
      public const int MAINWINDOW_TOP_LEFT_POINTS = 7;
      public const int MAINWINDOW_TOP_LEFT_SECOND_AGE = 8;
      public const int MAINWINDOW_TOP_LEFT_THIRD_AGE = 9;
      public const int MAINWINDOW_TOP_LEFT_DOMINATION_WORLD = 10;
      public const int MAINWINDOW_TOP_LEFT_HC_TIME_LEFT = 11;
      public const int MAINWINDOW_TOP_LEFT_FOURTH_AGE = 12;
      public const int MAINWINDOW_TOP_LEFT_FIFTH_AGE = 13;
      public const int MAINWINDOW_TOP_LEFT_SIXTH_AGE = 14;
      public const int MAINWINDOW_TOP_LEFT_SEVENTH_AGE = 15;
      public const int MAINWINDOW_TOP_LEFT_AI_WORLD = 16;
      public const int MAINWINDOW_TOP_RIGHT_VILLAGE_SCROLL = 20;
      public const int MAINWINDOW_TOP_RIGHT_VILLAGE_LIST = 21;
      public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_WORLDMAP = 22;
      public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_VILLAGEMAP = 23;
      public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_RESEARCH = 24;
      public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_RANKING = 25;
      public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_ATTACKS = 26;
      public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_REPORTS = 27;
      public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_MAIL = 28;
      public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_LEADERBOARD = 29;
      public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_FACTIONS = 30;
      public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_QUESTS = 31;
      public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_CAPITAL = 32;
      public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_PREMIUM_VO = 33;
      public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_CONTEST = 34;
      public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_VILLAGE = 40;
      public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CASTLE = 41;
      public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_RESOURCES = 42;
      public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_TRADING = 43;
      public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_TROOPS = 44;
      public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_UNITS = 45;
      public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_UNKNOWN = 46;
      public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_BANQUETING = 47;
      public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_VASSALS = 48;
      public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CAPITAL_INFO = 49;
      public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CAPITAL_VOTE = 50;
      public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CAPITAL_FORUM = 51;
      public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_NOT_IMPLEMENTED_YET = 52;
      public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_CHAT = 53;
      public const int MAINWINDOW_VILLAGE_NAME_TEST = 90;
      public const int MAINWINDOW_MAP_FILTERING = 91;
      public const int MAINWINDOW_MAP_FILTERING_CLOSE = 92;
      public const int MAINWINDOW_MAP_FILTERING_ACTIVE = 93;
      public const int MAINWINDOW_ATTACK_TARGETS = 94;
      public const int VILLAGEMAP_RIGHT_PANEL_BUILDING_ROLLOVER = 100;
      public const int VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_1 = 101;
      public const int VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_2 = 102;
      public const int VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_3 = 103;
      public const int VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_4 = 104;
      public const int VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_5 = 105;
      public const int VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_1 = 106;
      public const int VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_2 = 107;
      public const int VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_3 = 108;
      public const int VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_4 = 109;
      public const int VILLAGEMAP_RIGHT_PANEL_EXTRAS_BAR = 110;
      public const int VILLAGEMAP_RIGHT_PANEL_HONOUR_BAR = 111;
      public const int VILLAGEMAP_RIGHT_PANEL_HONOUR_BAR_CLOSE = 112;
      public const int VILLAGEMAP_RIGHT_PANEL_IN_BUILDING_CLOSE = 113;
      public const int VILLAGEMAP_RIGHT_PANEL_MOVE_BUILDING = 114;
      public const int VILLAGEMAP_RIGHT_PANEL_TAX_INC = 115;
      public const int VILLAGEMAP_RIGHT_PANEL_TAX_DEC = 116;
      public const int VILLAGEMAP_RIGHT_PANEL_RATIONS_INC = 117;
      public const int VILLAGEMAP_RIGHT_PANEL_RATIONS_DEC = 118;
      public const int VILLAGEMAP_RIGHT_PANEL_ALE_INC = 119;
      public const int VILLAGEMAP_RIGHT_PANEL_ALE_DEC = 120;
      public const int VILLAGEMAP_RIGHT_PANEL_POPULARITY_BAR = 121;
      public const int VILLAGEMAP_RIGHT_PANEL_POPULARITY_BAR_CLOSE = 122;
      public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_TAX_OPEN = 123;
      public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_TAX_CLOSE = 124;
      public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_RATIONS_OPEN = 125;
      public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_RATIONS_CLOSE = 126;
      public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_ALE_OPEN = 127;
      public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_ALE_CLOSE = 128;
      public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_HOUSING_OPEN = 129;
      public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_HOUSING_CLOSE = 130;
      public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_ENTERTAINMENT_OPEN = 131;
      public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_ENTERTAINMENT_CLOSE = 132;
      public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_EVENTS_OPEN = 133;
      public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_EVENTS_CLOSE = 134;
      public const int VILLAGEMAP_RIGHT_PANEL_BUILD_INFO = 140;
      public const int VILLAGEMAP_INFO_BAR_PEOPLE = 141;
      public const int VILLAGEMAP_INFO_BAR_WOOD = 142;
      public const int VILLAGEMAP_INFO_BAR_STONE = 143;
      public const int VILLAGEMAP_INFO_BAR_FOOD = 144;
      public const int VILLAGEMAP_INFO_BAR_CAPITAL_GOLD = 145;
      public const int VILLAGEMAP_INFO_BAR_CAPITAL_FLAGS = 146;
      public const int VILLAGEMAP_INFO_BAR_DONATION_TYPE = 147;
      public const int VILLAGEMAP_INFO_BAR_PITCH = 148;
      public const int VILLAGEMAP_INFO_BAR_IRON = 149;
      public const int VILLAGEMAP_CAPITAL_OVER_COMPLETED = 150;
      public const int VILLAGEMAP_CAPITAL_OVER_NOTCOMPLETED = 151;
      public const int CASTLEMAP_RIGHT_PANEL_BUILDING_ROLLOVER = 200;
      public const int CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_1 = 201;
      public const int CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_2 = 202;
      public const int CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_3 = 203;
      public const int CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_4 = 204;
      public const int CASTLEMAP_RIGHT_PANEL_TOGGLE_REINFORCEMENTS_ON = 205;
      public const int CASTLEMAP_RIGHT_PANEL_TOGGLE_REINFORCEMENTS_OFF = 206;
      public const int CASTLEMAP_RIGHT_PANEL_1X1 = 207;
      public const int CASTLEMAP_RIGHT_PANEL_3X3 = 208;
      public const int CASTLEMAP_RIGHT_PANEL_5X5 = 209;
      public const int CASTLEMAP_RIGHT_PANEL_1X5 = 210;
      public const int CASTLEMAP_RIGHT_PANEL_TOGGLE_VIEWMODE_HIGH = 211;
      public const int CASTLEMAP_RIGHT_PANEL_TOGGLE_VIEWMODE_LOW = 212;
      public const int CASTLEMAP_RIGHT_PANEL_TOGGLE_DELETE_ON = 213;
      public const int CASTLEMAP_RIGHT_PANEL_TOGGLE_DELETE_OFF = 214;
      public const int CASTLEMAP_RIGHT_PANEL_CASTLE_CONSTRUCTION_OPTIONS_OPEN = 215;
      public const int CASTLEMAP_RIGHT_PANEL_CASTLE_CONSTRUCTION_OPTIONS_CLOSE = 216;
      public const int CASTLEMAP_RIGHT_PANEL_REPAIR = 217;
      public const int CASTLEMAP_RIGHT_PANEL_DELETE_CONSTRUCTING = 218;
      public const int CASTLEMAP_RIGHT_PANEL_CONFIRM = 219;
      public const int CASTLEMAP_RIGHT_PANEL_CANCEL = 220;
      public const int CASTLEMAP_RIGHT_PANEL_DELETE_TROOPS = 221;
      public const int CASTLEMAP_RIGHT_PANEL_TOGGLE_AGGRESSIVE = 222;
      public const int CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_5 = 223;
      public const int CASTLEMAP_PRESET_MEMORISE = 224;
      public const int CASTLEMAP_PRESET_DEPLOY = 225;
      public const int CASTLEMAP_PRESET_RENAME = 226;
      public const int CASTLEMAP_PRESET_DELETE = 227;
      public const int CASTLEMAP_PRESET_DETAILS = 228;
      public const int CASTLEMAP_PRESET_RETURN = 229;
      public const int CASTLEMAP_PRESET_DEFENCE_REQ = 230;
      public const int CASTLEMAP_PRESET_FORTIFICATION_REQ = 231;
      public const int CASTLEMAP_PRESET_PARISH_REQ = 232;
      public const int CASTLEMAP_PRESET_WOOD = 233;
      public const int CASTLEMAP_PRESET_STONE = 234;
      public const int CASTLEMAP_PRESET_IRON = 235;
      public const int CASTLEMAP_PRESET_PITCH = 236;
      public const int CASTLEMAP_PRESET_GOLD = 237;
      public const int CASTLEMAP_PRESET_TIME = 238;
      public const int CASTLEMAP_PRESET_REQ_NOT_MET = 239;
      public const int CASTLEMAP_PRESET_PREVIEW = 240;
      public const int RESEARCHTREE_LIST_MODE = 300;
      public const int RESEARCHTREE_TREE_MODE = 301;
      public const int RESEARCH_QUEUE = 302;
      public const int RANKING_UPGRADE = 400;
      public const int RANKING_IMAGE = 401;
      public const int MAILSCREEN_FLOAT = 500;
      public const int MAILSCREEN_DOCK = 501;
      public const int MAILSCREEN_CLOSE = 502;
      public const int MAILSCREEN_REPORT = 503;
      public const int MAILSCREEN_AGGRESSIVE_BLOCK = 504;
      public const int MAIL_SEARCH = 505;
      public const int MAIL_RECENT = 506;
      public const int MAIL_FAVOURITES = 507;
      public const int MAIL_OTHERS_KNOWN = 508;
      public const int MAIL_SEARCH_USER = 510;
      public const int MAIL_SELECT_VILLAGE = 511;
      public const int MAIL_SEARCH_REGION = 512;
      public const int MAIL_CURRENT_ATTACHMENTS = 513;
      public const int MAIL_OPEN_ATTACHMENTS = 514;
      public const int MAIL_LINK_PLAYER = 515;
      public const int MAIL_LINK_VILLAGE = 516;
      public const int MAIL_LINK_PARISH = 517;
      public const int MAIL_VILLAGE_SEARCH_DISABLED = 518;
      public const int BARRACKS_DISBAND = 600;
      public const int BARRACKS_CLOSE = 601;
      public const int BARRACKS_PEASANTS = 602;
      public const int BARRACKS_ARCHERS = 603;
      public const int BARRACKS_PIKEMEN = 604;
      public const int BARRACKS_SWORDSMEN = 605;
      public const int BARRACKS_CATAPULTS = 606;
      public const int BARRACKS_CAPTAINS = 607;
      public const int BARRACKS_ARCHERS_NOT_RESEARCHED = 608;
      public const int BARRACKS_PIKEMEN_NOT_RESEARCHED = 609;
      public const int BARRACKS_SWORDSMEN_NOT_RESEARCHED = 610;
      public const int BARRACKS_CATAPULTS_NOT_RESEARCHED = 611;
      public const int BARRACKS_CAPTAINS_NOT_RESEARCHED = 612;
      public const int UNITS_DISBAND = 700;
      public const int UNITS_CLOSE = 701;
      public const int UNITS_SPACE_REQUIRED = 702;
      public const int UNITS_MERCHANTS = 703;
      public const int UNITS_MONKS = 704;
      public const int UNITS_SCOUTS = 705;
      public const int UNITS_MERCHANTS_NOT_RESEARCHED = 706;
      public const int UNITS_MONKS_NOT_RESEARCHED = 707;
      public const int UNITS_SCOUTS_NOT_RESEARCHED = 708;
      public const int STOCKEXCHANGE_CLOSE = 800;
      public const int TRADE_CLOSE = 801;
      public const int TRADE_RESOURCES = 802;
      public const int TRADE_FOOD = 803;
      public const int TRADE_WEAPONS = 804;
      public const int TRADE_BANQUETING = 805;
      public const int TOGGLE_TO_STOCKEXCHANGE = 806;
      public const int TOGGLE_TO_TRADING = 807;
      public const int REMOVE_FAVOURITE = 808;
      public const int ADD_FAVOURITE = 809;
      public const int REMOVE_RECENT = 810;
      public const int REMOVE_FAVOURITE_MARKET = 811;
      public const int ADD_FAVOURITE_MARKET = 812;
      public const int REMOVE_RECENT_MARKET = 813;
      public const int FIND_HIGHEST_PRICE = 814;
      public const int FIND_LOWEST_PRICE = 815;
      public const int RESOURCES_CLOSE = 900;
      public const int RESOURCES_INFO = 901;
      public const int BANQUET_CLOSE = 1000;
      public const int AREASELECT_OVER_TAG = 1100;
      public const int AREASELECT_OVER_TAG_FULL = 1101;
      public const int MENUBAR_CONVERT = 1200;
      public const int MENUBAR_ABANDON = 1201;
      public const int STATS_CATEGORY_ICONS = 1300;
      public const int LOGOUT_CLOSE = 1400;
      public const int LOGOUT_AUTO_TRADE = 1401;
      public const int LOGOUT_AUTO_SCOUT = 1402;
      public const int LOGOUT_AUTO_ATTACK = 1403;
      public const int LOGOUT_AUTO_RECRUIT = 1404;
      public const int LOGOUT_AUTO_REBUILD = 1405;
      public const int LOGOUT_AUTO_TRANSFER = 1406;
      public const int LOGOUT_SELECT_TRADE_RESOURCE = 1407;
      public const int LOGOUT_SELECT_TRADE_PERCENT = 1408;
      public const int LOGOUT_ATTACK_BANDITS = 1409;
      public const int LOGOUT_ATTACK_WOLVES = 1410;
      public const int LOGOUT_ATTACK_AI = 1411;
      public const int LOGOUT_RECRUIT_PEASANT = 1412;
      public const int LOGOUT_RECRUIT_ARCHER = 1413;
      public const int LOGOUT_RECRUIT_PIKEMAN = 1414;
      public const int LOGOUT_RECRUIT_SWORDSMAN = 1415;
      public const int LOGOUT_RECRUIT_CATAPULT = 1416;
      public const int LOGOUT_RESOURCES = 1417;
      public const int LOGOUT_EXIT = 1418;
      public const int LOGOUT_CANCEL = 1419;
      public const int LOGOUT_SWAP_WORLDS = 1420;
      public const int LOGOUT_PREMIUM = 1421;
      public const int REPORTS_FILTER = 1500;
      public const int REPORTS_CAPTURE = 1501;
      public const int REPORTS_DELETE = 1502;
      public const int TUTORIAL_REOPEN = 1600;
      public const int TUTORIAL_PLAYER_GUILD = 1601;
      public const int GLORY_HOUSE = 1700;
      public const int GLORY_HOUSE_1 = 1700;
      public const int GLORY_HOUSE_2 = 1701;
      public const int GLORY_HOUSE_3 = 1702;
      public const int GLORY_HOUSE_4 = 1703;
      public const int GLORY_HOUSE_5 = 1704;
      public const int GLORY_HOUSE_6 = 1705;
      public const int GLORY_HOUSE_7 = 1706;
      public const int GLORY_HOUSE_8 = 1707;
      public const int GLORY_HOUSE_9 = 1708;
      public const int GLORY_HOUSE_10 = 1709;
      public const int GLORY_HOUSE_11 = 1710;
      public const int GLORY_HOUSE_12 = 1711;
      public const int GLORY_HOUSE_13 = 1712;
      public const int GLORY_HOUSE_14 = 1713;
      public const int GLORY_HOUSE_15 = 1714;
      public const int GLORY_HOUSE_16 = 1715;
      public const int GLORY_HOUSE_17 = 1716;
      public const int GLORY_HOUSE_18 = 1717;
      public const int GLORY_HOUSE_19 = 1718;
      public const int GLORY_HOUSE_20 = 1719;
      public const int ERA_GLORY_STAR = 1730;
      public const int EOW_INACTIVE = 1750;
      public const int EOW_ACTIVE = 1751;
      public const int NEW_VILLAGE_ENTER = 1800;
      public const int NEW_VILLAGE_ADVANCED = 1801;
      public const int NEW_VILLAGE_LOGOUT = 1802;
      public const int CAPITAL_DONATE_DESCRIPTION = 1900;
      public const int MONK_INFLUENCE = 2000;
      public const int MONK_BLESSING = 2001;
      public const int MONK_INQUISITION = 2002;
      public const int MONK_INTERDICTS = 2003;
      public const int MONK_RESTORATION = 2004;
      public const int MONK_ABSOLUTION = 2005;
      public const int MONK_EXCOMMUNICATION = 2006;
      public const int MONK_POSITIVE_INFLUENCE = 2007;
      public const int MONK_NEGATIVE_INFLUENCE = 2008;
      public const int ARMY_VANDALISE = 2100;
      public const int ARMY_CAPTURE = 2101;
      public const int ARMY_PILLAGE = 2102;
      public const int ARMY_RANSACK = 2103;
      public const int ARMY_RAZE = 2104;
      public const int ARMY_GOLD_RAID = 2105;
      public const int ARMY_ATTACK = 2106;
      public const int FAVOURITE_ATTACK_TARGET_CLEAR = 2107;
      public const int FAVOURITE_ATTACK_TARGET_MAKE = 2018;
      public const int FACTIONTAB_GLORY = 2300;
      public const int FACTIONTAB_FACTIONS = 2301;
      public const int FACTIONTAB_HOUSE = 2302;
      public const int FACTION_ALLY = 2303;
      public const int FACTION_ENEMY = 2304;
      public const int FACTION_LEADER = 2305;
      public const int FACTION_OFFICER = 2306;
      public const int HOUSE_ROLLOVER = 2307;
      public const int HOUSE_GLORY_ROLLOVER = 2308;
      public const int FACTION_SIDEBAR_SHOW_ALL = 2350;
      public const int FACTION_SIDEBAR_MY_FACTION = 2351;
      public const int FACTION_SIDEBAR_DIPLOMACY = 2352;
      public const int FACTION_SIDEBAR_OFFICERS = 2353;
      public const int FACTION_SIDEBAR_FORUM = 2354;
      public const int FACTION_SIDEBAR_MAIL = 2355;
      public const int FACTION_SIDEBAR_INVITES = 2356;
      public const int FACTION_SIDEBAR_CHAT = 2357;
      public const int FACTION_SIDEBAR_START = 2358;
      public const int FACTION_SIDEBAR_LEAVE = 2359;
      public const int MAPSIDE_CANCEL = 2400;
      public const int MAPSIDE_ATTACK = 2401;
      public const int MAPSIDE_REINFORCE = 2402;
      public const int MAPSIDE_TRADE = 2403;
      public const int MAPSIDE_MARKET = 2404;
      public const int MAPSIDE_SCOUT = 2405;
      public const int MAPSIDE_MONK = 2406;
      public const int MAPSIDE_VASSAL = 2407;
      public const int MAPSIDE_CAPITAL_TRADE = 2410;
      public const int MAPSIDE_CAPITAL_ATTACK = 2411;
      public const int MAPSIDE_CAPITAL_SCOUT = 2412;
      public const int MAPSIDE_CAPITAL_REINFORCE = 2413;
      public const int MAPSIDE_CAPITAL_MONK = 2414;
      public const int MAPSIDE_PARISH = 2420;
      public const int MAPSIDE_COUNTY = 2421;
      public const int MAPSIDE_PROVINCE = 2422;
      public const int MAPSIDE_COUNTRY = 2423;
      public const int MAPSIDE_BANDIT_CAMP = 2424;
      public const int MAPSIDE_WOLF_LAIR = 2425;
      public const int MAPSIDE_RATS_CASTLE = 2426;
      public const int MAPSIDE_SNAKES_CASTLE = 2427;
      public const int MAPSIDE_PIGS_CASTLE = 2428;
      public const int MAPSIDE_WOLFS_CASTLE = 2429;
      public const int MAPSIDE_STASH = 2430;
      public const int MAPSIDE_OWN_TRADE = 2431;
      public const int MAPSIDE_OWN_ATTACK = 2432;
      public const int MAPSIDE_OWN_SCOUT = 2433;
      public const int MAPSIDE_OWN_REINFORCE = 2434;
      public const int MAPSIDE_OWN_MONK = 2435;
      public const int MAPSIDE_TERRAIN_TYPE = 2436;
      public const int MAPSIDE_VIEW_VILLAGE = 2437;
      public const int MAPSIDE_VIEW_CASTLE = 2438;
      public const int MAPSIDE_VIEW_RESOURCES = 2439;
      public const int MAPSIDE_MAKE_TROOPS = 2440;
      public const int MAPSIDE_PARISH_TRADE = 2441;
      public const int MAPSIDE_MAKE_MERCENARIES = 2442;
      public const int MAPSIDE_SCOUT_STASH = 2443;
      public const int MAPSIDE_VILLAGE_CHARTER = 2444;
      public const int MAPSIDE_VIEW_CASTLE_REPORT = 2445;
      public const int MAPSIDE_MAKE_VASSAL = 2446;
      public const int MAPSIDE_SMALL_PALADIN_CASTLE = 2447;
      public const int MAPSIDE_MEDIUM_PALADIN_CASTLE = 2448;
      public const int MAPSIDE_TREASURE_CASTLE = 2449;
      public const int MAPSIDE_PARISH_PLAGUE = 2450;
      public const int MAPSIDE_VASSAL_MANAGE_TROOPS = 2451;
      public const int MAPSIDE_VASSAL_MANAGE_VASSAL = 2452;
      public const int MAPSIDE_VASSAL_ATTACK_FROM = 2453;
      public const int MAPSIDE_FILTER_TRADE = 2454;
      public const int MAPSIDE_FILTER_ATTACK = 2455;
      public const int MAPSIDE_FILTER_SCOUT = 2456;
      public const int MAPSIDE_FILTER_HOUSE = 2457;
      public const int MAPSIDE_FILTER_FACTION = 2458;
      public const int MAPSIDE_FILTER_CLEAR = 2459;
      public const int MAPSIDE_FILTER_SEARCH = 2460;
      public const int MAPSIDE_FILTER_OPEN_FACTION = 2461;
      public const int MAPSIDE_FILTER_AI = 2462;
      public const int MAPSIDE_FACTIONNAME = 2501;
      public const int MAPSIDE_SENDMAIL = 2502;
      public const int MAPSIDE_USERINFO = 2503;
      public const int MAPSIDE_BUY_CHARTER_RANK_TOO_LOW = 2504;
      public const int MAPSIDE_CANT_BUY_FROM_HERE = 2505;
      public const int MAPSIDE_BUY_CHARTER_RANK_TOO_LOW12 = 2506;
      public const int UNIT_MAKE_ERROR = 2700;
      public const int UNIT_MAKE_ERROR_PEASANTS = 1;
      public const int UNIT_MAKE_ERROR_GOLD = 2;
      public const int UNIT_MAKE_ERROR_SPACE = 4;
      public const int UNIT_MAKE_ERROR_FULL = 8;
      public const int UNIT_MAKE_ERROR_WEAPON = 16;
      public const int UNIT_MAKE_ERROR_TROOP_SPACE = 32;
      public const int VASSAL_AVAILABLE_TROOPS = 2800;
      public const int ARMIES_ATTACKS = 2900;
      public const int ARMIES_SCOUTS = 2901;
      public const int ARMIES_REINFORCEMENTS = 2902;
      public const int ARMIES_MERCHANTS = 2903;
      public const int ARMIES_MONKS = 2904;
      public const int ARMIES_SORTING = 2905;
      public const int ACHIEVEMENT_NOT_STARTED = 3001;
      public const int ACHIEVEMENT_INPROGRESS = 3002;
      public const int ACHIEVEMENT_NOT_STARTED_OWN = 3003;
      public const int ACHIEVEMENT_INPROGRESS_OWN = 3004;
      public const int ADMIN = 3101;
      public const int USER_CLEAR_DIPLOMACY = 3102;
      public const int USER_CLEAR_DIPLOMACY_NOTES = 3103;
      public const int START_QUEST = 3201;
      public const int ABANDON_QUEST = 3202;
      public const int QUEST_REWARD_HONOUR = 3203;
      public const int QUEST_REWARD_GOLD = 3204;
      public const int QUEST_REWARD_WOOD = 3205;
      public const int QUEST_REWARD_STONE = 3206;
      public const int QUEST_REWARD_APPLES = 3207;
      public const int QUEST_REWARD_CARD_PACKS = 3208;
      public const int QUEST_REWARD_PREMIUM_CARD = 3209;
      public const int QUEST_REWARD_FAITHPOINTS = 3210;
      public const int QUEST_REWARD_GLORY = 3211;
      public const int QUEST_REWARD_SHIELD_CHARGES = 3212;
      public const int QUEST_REWARD_TICKETS = 3213;
      public const int QUEST_REWARD_FISH = 3214;
      public const int LOGIN_ENGLISH_SUPPORT = 4001;
      public const int LOGIN_GERMAN_SUPPORT = 4002;
      public const int LOGIN_FRENCH_SUPPORT = 4003;
      public const int LOGIN_RUSSIAN_SUPPORT = 4004;
      public const int LOGIN_MAP_OF_ENGLAND = 4005;
      public const int LOGIN_MAP_OF_GERMANY = 4006;
      public const int LOGIN_MAP_OF_FRANCE = 4007;
      public const int LOGIN_MAP_OF_RUSSIA = 4008;
      public const int LOGIN_OFFLINE = 4009;
      public const int LOGIN_ONLINE = 4010;
      public const int LOGIN_ENGLISH_FLAG = 4011;
      public const int LOGIN_GERMAN_FLAG = 4012;
      public const int LOGIN_FRENCH_FLAG = 4013;
      public const int LOGIN_RUSSIAN_FLAG = 4014;
      public const int LOGIN_EDIT_SHIELD = 4015;
      public const int LOGIN_SPANISH_SUPPORT = 4016;
      public const int LOGIN_MAP_OF_SPAIN = 4017;
      public const int LOGIN_SPANISH_FLAG = 4018;
      public const int LOGIN_SECOND_AGE = 4019;
      public const int LOGIN_POLISH_SUPPORT = 4020;
      public const int LOGIN_MAP_OF_POLAND = 4021;
      public const int LOGIN_POLISH_FLAG = 4022;
      public const int LOGIN_TURKISH_SUPPORT = 4023;
      public const int LOGIN_MAP_OF_TURKEY = 4024;
      public const int LOGIN_TURKISH_FLAG = 4025;
      public const int LOGIN_THIRD_AGE = 4026;
      public const int LOGIN_ITALIAN_SUPPORT = 4027;
      public const int LOGIN_MAP_OF_ITALY = 4028;
      public const int LOGIN_ITALIAN_FLAG = 4029;
      public const int LOGIN_MAP_OF_USA = 4030;
      public const int LOGIN_EUROPE_SUPPORT = 4031;
      public const int LOGIN_MAP_OF_EUROPE = 4032;
      public const int LOGIN_EUROPEAN_FLAG = 4033;
      public const int LOGIN_FOURTH_AGE = 4034;
      public const int LOGIN_PORTUGUESE_SUPPORT = 4035;
      public const int LOGIN_MAP_OF_SOUTH_AMERICA = 4036;
      public const int LOGIN_PORTUGUESE_FLAG = 4037;
      public const int LOGIN_FIRST_AGE = 4038;
      public const int LOGIN_FIFTH_AGE = 4039;
      public const int LOGIN_WORLD_FLAG = 4040;
      public const int LOGIN_WORLD_SUPPORT = 4041;
      public const int LOGIN_MAP_OF_WORLD = 4042;
      public const int LOGIN_MAP_OF_PHILIPPINES = 4043;
      public const int LOGIN_SIXTH_AGE = 4044;
      public const int LOGIN_SEVENTH_AGE = 4045;
      public const int LOGIN_CHINESE_SUPPORT = 4046;
      public const int LOGIN_MAP_OF_CHINA = 4047;
      public const int LOGIN_CHINESE_FLAG = 4048;
      public const int LOGIN_MAP_OF_KINGMAKER = 4049;
      public const int LOGIN_MAP_OF_SEA_JAPAN = 4050;
      public const int LOGIN_MAP_OF_HYW = 4051;
      public const int LOGIN_MAP_OF_VK = 4052;
      public const int LOGIN_MAP_OF_TGD = 4053;
      public const int LOGIN_MAP_OF_CRUSADER = 4054;
      public const int LOGIN_MAP_OF_SPARTA = 4055;
      public const int VO_TROOPS = 4100;
      public const int VO_SCOUTS = 4101;
      public const int VO_MERCHANTS = 4102;
      public const int VO_MONKS = 4103;
      public const int VO_POPULARITY = 4104;
      public const int VO_NUM_BUILDINGS = 4105;
      public const int VO_KEEP_ENCLOSED = 4106;
      public const int VO_KEEP_NOT_ENCLOSED = 4107;
      public const int VO_TROOPS_PEASANTS = 4108;
      public const int VO_TROOPS_ARCHERS = 4109;
      public const int VO_TROOPS_PIKEMEN = 4110;
      public const int VO_TROOPS_SWORDSMEN = 4111;
      public const int VO_TROOPS_CATAPULTS = 4112;
      public const int VO_TROOPS_CAPTAINS = 4113;
      public const int VO_TROOPS_EXPAND = 4114;
      public const int VO_TROOPS_COLLAPSE = 4115;
      public const int VO_SCOUTS_EXTRA = 4116;
      public const int VO_MERCHANTS_EXTRA = 4117;
      public const int VO_MONKS_EXTRA = 4118;
      public const int VO_TAX_INCOME = 4119;
      public const int VO_RATIONS = 4120;
      public const int VO_ALE_RATIONS = 4121;
      public const int VO_PEOPLE = 4122;
      public const int VO_INTERDICTION = 4123;
      public const int VO_EXCOMMUNICATION = 4124;
      public const int VO_PEACETIME = 4125;
      public const int VO_NOT_PREMIUM = 4126;
      public const int VO_IRON = 4127;
      public const int VO_PITCH = 4128;
      public const int VO_DAMAGED = 4140;
      public const int PROCLAMATION_HELP_PARISH = 4200;
      public const int PROCLAMATION_HELP_COUNTY = 4201;
      public const int PROCLAMATION_HELP_PROVINCE = 4202;
      public const int PROCLAMATION_HELP_COUNTRY = 4203;
      public const int PT_RESEARCH = 4300;
      public const int PT_RANK = 4301;
      public const int PT_ACHIEVEMENTS = 4302;
      public const int PT_QUESTS = 4303;
      public const int PT_REPORTS = 4304;
      public const int PT_COAT_OF_ARMS = 4305;
      public const int PT_AVATAR = 4306;
      public const int PT_INVITE_A_FRIEND = 4307;
      public const int PT_PARISH_WALL = 4308;
      public const int PT_MAIL = 4309;
      public const int WIKI_HELP_LINK = 4400;
      public const int WIKI_HELP_LINK_CHAT_SPECIAL = 4401;
      public const int WIKI_HELP_LINK_SETTINGS_SPECIAL = 4402;
      public const int CARD_BAR_CIRCLES = 10000;
      public const int CARD_BAR_PLAY_CARDS = 10001;
      public const int CARD_BAR_EXPAND = 10002;
      public const int CARD_BAR_COLLAPSE = 10003;
      public const int CARD_BAR_NEXT = 10004;
      public const int CARD_BAR_PREV = 10005;
      public const int CARD_WINDOW_CLOSE = 10100;
      public const int CARD_WINDOW_CARDS = 10101;
      public const int CARD_WINDOW_FILTER = 10102;
      public const int CARD_WINDOW_CASH_IN = 10103;
      public const int CARD_WINDOW_GET_CARDS = 10104;
      public const int CARD_WINDOW_FILTER2 = 10105;
      public const int CARDS_FARMING_PACK = 10301;
      public const int CARDS_CASTLE_PACK = 10302;
      public const int CARDS_DEFENSE_PACK = 10303;
      public const int CARDS_ARMY_PACK = 10304;
      public const int CARDS_RANDOM_PACK = 10305;
      public const int CARDS_INDUSTRY_PACK = 10306;
      public const int CARDS_RESEARCH_PACK = 10307;
      public const int CARDS_EXCLUSIVE_PACK = 10308;
      public const int CARDS_SUPER_FARMING_PACK = 10309;
      public const int CARDS_SUPER_DEFENSE_PACK = 10310;
      public const int CARDS_SUPER_ARMY_PACK = 10311;
      public const int CARDS_SUPER_RANDOM_PACK = 10312;
      public const int CARDS_SUPER_INDUSTRY_PACK = 10313;
      public const int CARDS_ULTIMATE_FARMING_PACK = 10314;
      public const int CARDS_ULTIMATE_DEFENSE_PACK = 10315;
      public const int CARDS_ULTIMATE_ARMY_PACK = 10316;
      public const int CARDS_ULTIMATE_RANDOM_PACK = 10317;
      public const int CARDS_ULTIMATE_INDUSTRY_PACK = 10318;
      public const int CARDS_SEARCH_CARDS = 10319;
      public const int CARDS_CLEAR_SEARCH_CARDS = 10320;
      public const int CARDS_PLATINUM_PACK = 10321;
      public const int BUY_AERIA_POINTS = 10350;
      public const int MAPSIDE_RENAME = 10390;
      public const int FREE_CARDS_MAIN = 10500;
      public const int ROYAL_TICKETS_MAIN = 10501;
      public const int WOLFS_REVENGE = 10502;
      public const int GENERIC_TIME = 20000;
      public const int TRACK_BAR_HINT = 21000;
      public const int PLAYBACK_STOP = 22000;
      public const int PLAYBACK_PAUSE = 22001;
      public const int PLAYBACK_PLAY = 22002;
      public const int PLAYBACK_SPEED1 = 22003;
      public const int PLAYBACK_SPEED2 = 22004;
      public const int PLAYBACK_SPEED4 = 22005;
      public const int PLAYBACK_EXPAND = 22006;
      public const int PLAYBACK_COLLAPSE = 22007;
      public const int SEA_CONDITIONS_TRAVEL_PANELS_MINUS_5 = 23000;
      public const int SEA_CONDITIONS_TRAVEL_PANELS_MINUS_4 = 23001;
      public const int SEA_CONDITIONS_TRAVEL_PANELS_MINUS_3 = 23002;
      public const int SEA_CONDITIONS_TRAVEL_PANELS_MINUS_2 = 23003;
      public const int SEA_CONDITIONS_TRAVEL_PANELS_NEUTRAL = 23004;
      public const int SEA_CONDITIONS_TRAVEL_PANELS_PLUS_2 = 23005;
      public const int SEA_CONDITIONS_TRAVEL_PANELS_PLUS_3 = 23006;
      public const int SEA_CONDITIONS_TRAVEL_PANELS_PLUS_4 = 23007;
      public const int SEA_CONDITIONS_TRAVEL_PANELS_PLUS_5 = 23008;
      public const int SEA_CONDITIONS_MAP_MINUS_5 = 23010;
      public const int SEA_CONDITIONS_MAP_MINUS_4 = 23011;
      public const int SEA_CONDITIONS_MAP_MINUS_3 = 23012;
      public const int SEA_CONDITIONS_MAP_MINUS_2 = 23013;
      public const int SEA_CONDITIONS_MAP_NEUTRAL = 23014;
      public const int SEA_CONDITIONS_MAP_PLUS_2 = 23015;
      public const int SEA_CONDITIONS_MAP_PLUS_3 = 23016;
      public const int SEA_CONDITIONS_MAP_PLUS_4 = 23017;
      public const int SEA_CONDITIONS_MAP_PLUS_5 = 23018;
      public const int ROYAL_TOWER_ICON = 24000;
      public const int KILL_STREAK_ICON = 24100;
      public const int CONTEST_PRIZE_AVAILABLE = 25001;
      public const int CONTEST_ONGOING = 25002;
      public const int CONTEST_RANKBAND = 25003;
      public const int SALE_ONGOING = 25100;
      public const int SPECIAL_OFFER_AVAILABLE = 25101;
    }

    public class ToolTipZone
    {
      public Rectangle rect;
      public int ID;
      public Control relatedControl;
      public CustomSelfDrawPanel.CSDControl relatedCSDControl;

      public ToolTipZone(int x, int y, int width, int height, int tooltipID)
      {
        this.rect = new Rectangle(x, y, width, height);
        this.ID = tooltipID;
      }

      public ToolTipZone(int x, int y, int width, int height, int tooltipID, Control relative)
      {
        this.rect = new Rectangle(x, y, width, height);
        this.ID = tooltipID;
        this.relatedControl = relative;
      }

      public ToolTipZone(
        int x,
        int y,
        int width,
        int height,
        int tooltipID,
        CustomSelfDrawPanel.CSDControl relative)
      {
        this.rect = new Rectangle(x, y, width, height);
        this.ID = tooltipID;
        this.relatedCSDControl = relative;
      }
    }

    public class ToolTipZoneControlChild : Control
    {
      public CustomTooltipManager.ToolTipZone[] zones;
    }

    public class ToolTipZoneTabChild : TabPage
    {
      public CustomTooltipManager.ToolTipZone[] zones;
    }
  }
}
