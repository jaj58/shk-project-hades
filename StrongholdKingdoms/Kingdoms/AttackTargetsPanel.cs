// Decompiled with JetBrains decompiler
// Type: Kingdoms.AttackTargetsPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class AttackTargetsPanel : CustomSelfDrawPanel
  {
    private static List<WorldMap.VillageNameItem> villageHistory = new List<WorldMap.VillageNameItem>();
    private static List<WorldMap.VillageNameItem> villageFavourites = new List<WorldMap.VillageNameItem>();
    private int m_selectedVillageID = -1;
    private AttackTargetsPopup m_parent;
    private CustomSelfDrawPanel.CSDLabel favouritesHeader = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel recentHeader = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDListBox favouritesList = new CustomSelfDrawPanel.CSDListBox();
    private CustomSelfDrawPanel.CSDListBox recentList = new CustomSelfDrawPanel.CSDListBox();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton scoutButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton removeButton = new CustomSelfDrawPanel.CSDButton();

    public static List<WorldMap.VillageNameItem> VillageHistory
    {
      get => AttackTargetsPanel.villageHistory;
    }

    public static List<WorldMap.VillageNameItem> VillageFavourites
    {
      get => AttackTargetsPanel.villageFavourites;
    }

    public static void addHistory(List<GenericVillageHistoryData> newData)
    {
      AttackTargetsPanel.villageHistory.Clear();
      if (newData == null)
        return;
      foreach (GenericVillageHistoryData villageHistoryData in newData)
        AttackTargetsPanel.villageHistory.Add(new WorldMap.VillageNameItem()
        {
          villageID = villageHistoryData.villageID
        });
    }

    public static void addFavourites(List<GenericVillageHistoryData> newData)
    {
      AttackTargetsPanel.villageFavourites.Clear();
      if (newData == null)
        return;
      foreach (GenericVillageHistoryData villageHistoryData in newData)
        AttackTargetsPanel.villageFavourites.Add(new WorldMap.VillageNameItem()
        {
          villageID = villageHistoryData.villageID
        });
    }

    public static void addRecent(int villageID)
    {
      AttackTargetsPanel.villageHistory.Add(new WorldMap.VillageNameItem()
      {
        villageID = villageID
      });
    }

    public static void addFavourite(int villageID)
    {
      if (AttackTargetsPanel.isFavourite(villageID))
        return;
      AttackTargetsPanel.villageFavourites.Add(new WorldMap.VillageNameItem()
      {
        villageID = villageID
      });
      RemoteServices.Instance.UpdateVillageFavourites(4, villageID);
    }

    public static void removeFavourite(int villageID)
    {
      if (!AttackTargetsPanel.isFavourite(villageID))
        return;
      foreach (WorldMap.VillageNameItem villageFavourite in AttackTargetsPanel.villageFavourites)
      {
        if (villageFavourite.villageID == villageID)
        {
          AttackTargetsPanel.villageFavourites.Remove(villageFavourite);
          RemoteServices.Instance.UpdateVillageFavourites(5, villageID);
          break;
        }
      }
    }

    public static bool isFavourite(int villageID)
    {
      foreach (WorldMap.VillageNameItem villageFavourite in AttackTargetsPanel.villageFavourites)
      {
        if (villageFavourite.villageID == villageID)
          return true;
      }
      return false;
    }

    public AttackTargetsPanel()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(AttackTargetsPopup parent)
    {
      this.m_parent = parent;
      this.Size = this.m_parent.Size;
      this.BackColor = ARGBColors.Transparent;
      CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage();
      control.Alpha = 0.1f;
      control.Image = (Image) GFXLibrary.formations_img;
      control.Scale = 5.0;
      control.Position = new Point(0, 0);
      control.Size = this.Size;
      this.addControl((CustomSelfDrawPanel.CSDControl) control);
      this.favouritesHeader.Text = SK.Text("Attack_Targets_Favourites", "Favourite Targets");
      this.favouritesHeader.Color = ARGBColors.White;
      this.favouritesHeader.Position = new Point(30, 0);
      this.favouritesHeader.Size = new Size(300, 30);
      this.favouritesHeader.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.favouritesHeader.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.favouritesHeader);
      this.recentHeader.Text = SK.Text("Attack_Targets_Recent", "Recent Targets");
      this.recentHeader.Color = ARGBColors.White;
      this.recentHeader.Position = new Point(370, 0);
      this.recentHeader.Size = new Size(300, 30);
      this.recentHeader.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.recentHeader.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.recentHeader);
      this.favouritesList.Size = new Size(300, 342);
      this.favouritesList.Position = new Point(30, 30);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.favouritesList);
      this.favouritesList.Create(19, 18);
      this.favouritesList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.favouriteClick));
      this.favouritesList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.favouriteDoubleClick));
      this.recentList.Size = new Size(300, 342);
      this.recentList.Position = new Point(370, 30);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.recentList);
      this.recentList.Create(19, 18);
      this.recentList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.recentClick));
      this.recentList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.recentDoubleClick));
      this.closeButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      this.closeButton.Position = new Point(540, this.Height - 70);
      this.closeButton.Text.Text = SK.Text("GENERIC_Close", "Close");
      this.closeButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.closeButton.TextYOffset = -3;
      this.closeButton.Text.Color = ARGBColors.Black;
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick));
      control.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      this.attackButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      this.attackButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      this.attackButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      this.attackButton.Position = new Point(380, this.Height - 70);
      this.attackButton.Text.Text = SK.Text("GENERIC_Attack", "Attack");
      this.attackButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.attackButton.TextYOffset = -3;
      this.attackButton.Text.Color = ARGBColors.Black;
      this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.attackClicked));
      this.attackButton.Enabled = false;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.attackButton);
      this.scoutButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      this.scoutButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      this.scoutButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      this.scoutButton.Position = new Point(220, this.Height - 70);
      this.scoutButton.Text.Text = SK.Text("GENERIC_Scout", "Scout");
      this.scoutButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.scoutButton.TextYOffset = -3;
      this.scoutButton.Text.Color = ARGBColors.Black;
      this.scoutButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.scoutClicked));
      this.scoutButton.Enabled = false;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.scoutButton);
      this.removeButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      this.removeButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      this.removeButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      this.removeButton.Position = new Point(30, this.Height - 70);
      this.removeButton.Text.Text = SK.Text("MailScreen_Remove", "Remove");
      this.removeButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.removeButton.TextYOffset = -3;
      this.removeButton.Text.Color = ARGBColors.Black;
      this.removeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.removeClicked));
      this.removeButton.Visible = false;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.removeButton);
      this.fillBoxes();
    }

    private void fillBoxes()
    {
      List<CustomSelfDrawPanel.CSDListItem> items1 = new List<CustomSelfDrawPanel.CSDListItem>();
      foreach (WorldMap.VillageNameItem villageFavourite in AttackTargetsPanel.villageFavourites)
      {
        if (GameEngine.Instance.World.isVillageVisible(villageFavourite.villageID))
          items1.Add(new CustomSelfDrawPanel.CSDListItem()
          {
            Text = GameEngine.Instance.World.getVillageNameOrType(villageFavourite.villageID),
            Data = villageFavourite.villageID
          });
      }
      items1.Sort((Comparison<CustomSelfDrawPanel.CSDListItem>) ((first, next) => first.Text.CompareTo(next.Text)));
      this.favouritesList.populate(items1);
      List<CustomSelfDrawPanel.CSDListItem> items2 = new List<CustomSelfDrawPanel.CSDListItem>();
      foreach (WorldMap.VillageNameItem villageNameItem in AttackTargetsPanel.villageHistory)
      {
        if (GameEngine.Instance.World.isVillageVisible(villageNameItem.villageID))
          items2.Add(new CustomSelfDrawPanel.CSDListItem()
          {
            Text = GameEngine.Instance.World.getVillageNameOrType(villageNameItem.villageID),
            Data = villageNameItem.villageID
          });
      }
      items2.Sort((Comparison<CustomSelfDrawPanel.CSDListItem>) ((first, next) => first.Text.CompareTo(next.Text)));
      this.recentList.populate(items2);
    }

    private void closeClick() => InterfaceMgr.Instance.closeAttackTargetsPopup();

    private void favouriteClick(CustomSelfDrawPanel.CSDListItem item)
    {
      if (item == null)
        return;
      this.recentList.clearSelectedItem();
      if (item.Data < 0)
        return;
      this.m_selectedVillageID = item.Data;
      this.removeButton.Visible = true;
      this.updateButtons();
    }

    private void favouriteDoubleClick(CustomSelfDrawPanel.CSDListItem item)
    {
      if (item == null)
        return;
      this.recentList.clearSelectedItem();
      if (item.Data < 0)
        return;
      this.m_selectedVillageID = item.Data;
      GameEngine.Instance.World.zoomToVillage(item.Data);
      InterfaceMgr.Instance.displaySelectedVillagePanel(item.Data, false, true, false, false);
      this.removeButton.Visible = true;
      this.updateButtons();
    }

    private void recentClick(CustomSelfDrawPanel.CSDListItem item)
    {
      if (item == null)
        return;
      this.favouritesList.clearSelectedItem();
      if (item.Data < 0)
        return;
      this.m_selectedVillageID = item.Data;
      this.removeButton.Visible = false;
      this.updateButtons();
    }

    private void recentDoubleClick(CustomSelfDrawPanel.CSDListItem item)
    {
      if (item == null)
        return;
      this.favouritesList.clearSelectedItem();
      if (item.Data < 0)
        return;
      this.m_selectedVillageID = item.Data;
      GameEngine.Instance.World.zoomToVillage(item.Data);
      InterfaceMgr.Instance.displaySelectedVillagePanel(item.Data, false, true, false, false);
      this.removeButton.Visible = false;
      this.updateButtons();
    }

    private void listDoubleClick(CustomSelfDrawPanel.CSDListItem item)
    {
    }

    private void updateButtons()
    {
      if (this.m_selectedVillageID >= 0)
      {
        if (GameEngine.Instance.World.getSpecial(this.m_selectedVillageID) == 0)
        {
          if (GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
          {
            this.scoutButton.Enabled = false;
            if (!GameEngine.Instance.World.isUserVillage(InterfaceMgr.Instance.getSelectedMenuVillage()))
              this.attackButton.Enabled = false;
            else
              this.attackButton.Enabled = true;
          }
          else
          {
            this.scoutButton.Enabled = true;
            this.attackButton.Enabled = true;
          }
        }
        else
        {
          this.attackButton.Enabled = GameEngine.Instance.World.isAttackableSpecial(this.m_selectedVillageID);
          this.scoutButton.Enabled = GameEngine.Instance.World.isScoutableSpecial(this.m_selectedVillageID) && !GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage);
        }
      }
      else
      {
        this.attackButton.Enabled = false;
        this.scoutButton.Enabled = false;
      }
    }

    private void attackClicked()
    {
      this.closeClick();
      GameEngine.Instance.preAttackSetup(InterfaceMgr.Instance.OwnSelectedVillage, InterfaceMgr.Instance.OwnSelectedVillage, this.m_selectedVillageID);
    }

    private void scoutClicked()
    {
      InterfaceMgr.Instance.openScoutPopupWindow(this.m_selectedVillageID, true);
      this.closeClick();
    }

    private void removeClicked()
    {
      AttackTargetsPanel.removeFavourite(this.m_selectedVillageID);
      this.fillBoxes();
      this.removeButton.Visible = false;
    }
  }
}
