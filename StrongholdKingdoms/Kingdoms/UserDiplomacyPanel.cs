// Decompiled with JetBrains decompiler
// Type: Kingdoms.UserDiplomacyPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class UserDiplomacyPanel : CustomSelfDrawPanel, IDockableControl
  {
    public const int PANEL_ID = 60;
    private DockableControl dockableControl;
    private IContainer components;
    public static UserDiplomacyPanel instance = (UserDiplomacyPanel) null;
    private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage2 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDVertScrollBar alliesScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea alliesScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay1 = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDVertScrollBar enemiesScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea enemiesScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay2 = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDLabel alliesLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel enemiesLabel = new CustomSelfDrawPanel.CSDLabel();
    private static List<string> customNotes = new List<string>();
    private int blockYSize;

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
      this.closing();
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
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (UserDiplomacyPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public UserDiplomacyPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      int height = this.Height;
      this.blockYSize = height / 2;
      UserDiplomacyPanel.instance = this;
      this.clearControls();
      this.loadDiplomacyData();
      this.mainBackgroundImage.FillColor = Color.FromArgb(134, 153, 165);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = new Size(this.Width, height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundFade.Image = (Image) GFXLibrary.background_top;
      this.backgroundFade.Position = new Point(0, 0);
      this.backgroundFade.Size = new Size(this.Width, this.backgroundFade.Image.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundFade);
      this.headerLabelsImage.Size = new Size(this.Width - 25 - 23, 28);
      this.headerLabelsImage.Position = new Point(25, 5);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.headerLabelsImage2.Size = new Size(this.Width - 25 - 23, 28);
      this.headerLabelsImage2.Position = new Point(25, this.blockYSize + 5);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage2);
      this.headerLabelsImage2.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.alliesLabel.Text = SK.Text("FactionDiplomacy_Allies", "Allies");
      this.alliesLabel.Color = ARGBColors.Black;
      this.alliesLabel.Position = new Point(9, -2);
      this.alliesLabel.Size = new Size(323, this.headerLabelsImage.Height);
      this.alliesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.alliesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.alliesLabel);
      this.enemiesLabel.Text = SK.Text("FactionDiplomacy_Enemies", "Enemies");
      this.enemiesLabel.Color = ARGBColors.Black;
      this.enemiesLabel.Position = new Point(9, -2);
      this.enemiesLabel.Size = new Size(323, this.headerLabelsImage.Height);
      this.enemiesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.enemiesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage2.addControl((CustomSelfDrawPanel.CSDControl) this.enemiesLabel);
      InterfaceMgr.Instance.setVillageHeading(SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy"));
      this.alliesScrollArea.Position = new Point(25, 40);
      this.alliesScrollArea.Size = new Size(915, this.blockYSize - 40 - 10);
      this.alliesScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(915, this.blockYSize - 40 - 10));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.alliesScrollArea);
      this.mouseWheelOverlay1.Position = this.alliesScrollArea.Position;
      this.mouseWheelOverlay1.Size = this.alliesScrollArea.Size;
      this.mouseWheelOverlay1.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved1));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay1);
      int num1 = this.alliesScrollBar.Value;
      this.alliesScrollBar.Position = new Point(933, 40);
      this.alliesScrollBar.Size = new Size(24, this.blockYSize - 40 - 10);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.alliesScrollBar);
      this.alliesScrollBar.Value = 0;
      this.alliesScrollBar.Max = 100;
      this.alliesScrollBar.NumVisibleLines = 25;
      this.alliesScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.alliesScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      this.enemiesScrollArea.Position = new Point(25, 35 + this.blockYSize + 5);
      this.enemiesScrollArea.Size = new Size(915, this.blockYSize - 40 - 10);
      this.enemiesScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(915, this.blockYSize - 40 - 10));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.enemiesScrollArea);
      this.mouseWheelOverlay2.Position = this.enemiesScrollArea.Position;
      this.mouseWheelOverlay2.Size = this.enemiesScrollArea.Size;
      this.mouseWheelOverlay2.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved2));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay2);
      int num2 = this.enemiesScrollBar.Value;
      this.enemiesScrollBar.Position = new Point(933, 35 + this.blockYSize + 5);
      this.enemiesScrollBar.Size = new Size(24, this.blockYSize - 40 - 10);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.enemiesScrollBar);
      this.enemiesScrollBar.Value = 0;
      this.enemiesScrollBar.Max = 100;
      this.enemiesScrollBar.NumVisibleLines = 25;
      this.enemiesScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.enemiesScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.incomingWallScrollBarMoved));
      this.addPlayers();
    }

    public void update()
    {
    }

    public void logout()
    {
    }

    private void wallScrollBarMoved()
    {
      int y = this.alliesScrollBar.Value;
      this.alliesScrollArea.Position = new Point(this.alliesScrollArea.X, 40 - y);
      this.alliesScrollArea.ClipRect = new Rectangle(this.alliesScrollArea.ClipRect.X, y, this.alliesScrollArea.ClipRect.Width, this.alliesScrollArea.ClipRect.Height);
      this.alliesScrollArea.invalidate();
      this.alliesScrollBar.invalidate();
    }

    private void mouseWheelMoved1(int delta)
    {
      if (!this.alliesScrollBar.Visible)
        return;
      if (delta < 0)
      {
        this.alliesScrollBar.scrollDown(40);
      }
      else
      {
        if (delta <= 0)
          return;
        this.alliesScrollBar.scrollUp(40);
      }
    }

    private void incomingWallScrollBarMoved()
    {
      int y = this.enemiesScrollBar.Value;
      this.enemiesScrollArea.Position = new Point(this.enemiesScrollArea.X, 35 + this.blockYSize + 5 - y);
      this.enemiesScrollArea.ClipRect = new Rectangle(this.enemiesScrollArea.ClipRect.X, y, this.enemiesScrollArea.ClipRect.Width, this.enemiesScrollArea.ClipRect.Height);
      this.enemiesScrollArea.invalidate();
      this.enemiesScrollBar.invalidate();
    }

    private void mouseWheelMoved2(int delta)
    {
      if (!this.enemiesScrollBar.Visible)
        return;
      if (delta < 0)
      {
        this.enemiesScrollBar.scrollDown(40);
      }
      else
      {
        if (delta <= 0)
          return;
        this.enemiesScrollBar.scrollUp(40);
      }
    }

    public void closing() => InterfaceMgr.Instance.closeDonatePopup();

    public void addPlayers()
    {
      this.alliesScrollArea.clearControls();
      this.enemiesScrollArea.clearControls();
      int num1 = 0;
      int num2 = 2;
      List<UserRelationship> userRelations = GameEngine.Instance.World.UserRelations;
      int position1 = 0;
      int position2 = 0;
      foreach (UserRelationship userRelationship in userRelations)
      {
        if (userRelationship.friendly)
        {
          UserDiplomacyPanel.UserAllianceLine control = new UserDiplomacyPanel.UserAllianceLine();
          if (num1 != 0)
            num1 += 5;
          control.Position = new Point(0, num1);
          control.init(userRelationship.userName, userRelationship.userID, position1, true, this);
          this.alliesScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
          num1 += control.Height;
          ++position1;
        }
        else
        {
          UserDiplomacyPanel.UserAllianceLine control = new UserDiplomacyPanel.UserAllianceLine();
          if (num2 != 0)
            num2 += 5;
          control.Position = new Point(0, num2);
          control.init(userRelationship.userName, userRelationship.userID, position2, false, this);
          this.enemiesScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
          num2 += control.Height;
          ++position2;
        }
      }
      this.alliesScrollArea.Size = new Size(this.alliesScrollArea.Width, num1);
      if (num1 < this.alliesScrollBar.Height)
      {
        this.alliesScrollBar.Visible = false;
      }
      else
      {
        this.alliesScrollBar.Visible = true;
        this.alliesScrollBar.NumVisibleLines = this.alliesScrollBar.Height;
        this.alliesScrollBar.Max = num1 - this.alliesScrollBar.Height;
      }
      this.alliesScrollArea.invalidate();
      this.alliesScrollBar.invalidate();
      this.enemiesScrollArea.Size = new Size(this.enemiesScrollArea.Width, num2);
      if (num2 < this.enemiesScrollBar.Height)
      {
        this.enemiesScrollBar.Visible = false;
      }
      else
      {
        this.enemiesScrollBar.Visible = true;
        this.enemiesScrollBar.NumVisibleLines = this.enemiesScrollBar.Height;
        this.enemiesScrollBar.Max = num2 - this.enemiesScrollBar.Height;
      }
      this.enemiesScrollArea.invalidate();
      this.enemiesScrollBar.invalidate();
      this.update();
      this.Invalidate();
    }

    private void loadDiplomacyData()
    {
      string settingsPath = GameEngine.getSettingsPath(false);
      try
      {
        FileStream input = new FileStream(settingsPath + "\\DiplomacyDataW" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + "U" + RemoteServices.Instance.UserID.ToString() + ".dat", FileMode.Open);
        BinaryReader binaryReader = new BinaryReader((Stream) input);
        byte[] numArray = new byte[16];
        if (RemoteServices.Instance.WorldGUID.CompareTo(new Guid(binaryReader.ReadBytes(16))) != 0)
        {
          binaryReader.Close();
          input.Close();
        }
        else
        {
          UserDiplomacyPanel.customNotes.Clear();
          for (int index = 0; index < GameEngine.Instance.World.UserRelations.Count; ++index)
            UserDiplomacyPanel.customNotes.Add(binaryReader.ReadString());
        }
      }
      catch (Exception ex)
      {
      }
    }

    public class UserAllianceLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel factionName = new CustomSelfDrawPanel.CSDLabel();
      private int m_position = -1000;
      private int m_userID = -1;
      private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDButton trash = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton playerNotesButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDLine divider = new CustomSelfDrawPanel.CSDLine();
      private CustomSelfDrawPanel.CSDLabel playerNotes = new CustomSelfDrawPanel.CSDLabel();
      private UserDiplomacyPanel m_parent;

      public void init(
        string username,
        int userID,
        int position,
        bool ally,
        UserDiplomacyPanel parent)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.m_userID = userID;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_02_dark : (Image) GFXLibrary.lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(40, 0);
        this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.userClick));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.shieldImage.Image = GameEngine.Instance.World.getWorldShield(userID, 25, 28);
        if (this.shieldImage.Image != null)
        {
          this.shieldImage.Position = new Point(0, 0);
          this.shieldImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.userClick));
          this.addControl((CustomSelfDrawPanel.CSDControl) this.shieldImage);
        }
        this.factionName.Text = username;
        this.factionName.Color = ARGBColors.Black;
        this.factionName.Position = new Point(9, 0);
        this.factionName.Size = new Size(500, this.backgroundImage.Height);
        this.factionName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.factionName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.factionName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.userClick));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionName);
        this.trash.ImageNorm = (Image) GFXLibrary.trashcan_normal;
        this.trash.ImageOver = (Image) GFXLibrary.trashcan_over;
        this.trash.ImageClick = (Image) GFXLibrary.trashcan_clicked;
        this.trash.Position = new Point(830, 4);
        this.trash.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteClicked), "FactionNewForumPanel_delete_thread");
        this.trash.CustomTooltipID = 3102;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.trash);
        this.playerNotesButton.ImageNormAndOver = (Image) GFXLibrary.faction_pen;
        this.playerNotesButton.Position = new Point(800, 4);
        this.playerNotesButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.setNote), "Set_Player_Notes");
        this.playerNotesButton.CustomTooltipID = 3103;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.playerNotesButton);
        this.playerNotes.Text = "";
        this.playerNotes.Color = ARGBColors.Black;
        this.playerNotes.Position = new Point(this.factionName.Position.X + 240, 4);
        this.playerNotes.Size = new Size(500, this.backgroundImage.Height);
        this.playerNotes.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.playerNotes.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.playerNotes);
        this.divider.Position = new Point(this.playerNotes.Position.X - 5, 5);
        this.divider.Height = this.backgroundImage.Height - 10;
        this.divider.Width = 0;
        this.divider.LineColor = ARGBColors.Black;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider);
        foreach (string customNote in UserDiplomacyPanel.customNotes)
        {
          string[] strArray = customNote.Split('#');
          if (strArray[0] == username && strArray[1] == GameEngine.Instance.World.GetGlobalWorldID().ToString())
          {
            this.playerNotes.Text = strArray[2];
            if (strArray.Length > 3)
            {
              for (int index = 3; index < strArray.Length; ++index)
              {
                CustomSelfDrawPanel.CSDLabel playerNotes = this.playerNotes;
                playerNotes.Text = playerNotes.Text + "#" + strArray[index];
              }
            }
          }
        }
      }

      public void update()
      {
      }

      private void userClick()
      {
        GameEngine.Instance.playInterfaceSound("FactionDiplomacyPanel_faction_clicked");
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.showUserInfoScreen(new WorldMap.CachedUserInfo()
        {
          userID = this.m_userID
        });
      }

      private void deleteClicked()
      {
        GameEngine.Instance.World.setUserRelationship(this.m_userID, 0, "");
        RemoteServices.Instance.CreateUserRelationship(this.m_userID, 0);
        this.m_parent.init(true);
      }

      private void setNote()
      {
        InterfaceMgr.Instance.setFloatingTextSentDelegate(new InterfaceMgr.FloatingTextSent(this.setText));
        Point screen = this.m_parent.PointToScreen(new Point(this.playerNotes.getPanelPosition().X, this.playerNotes.getPanelPosition().Y + 4));
        FloatingInputText.openDisband(screen.X, screen.Y, this.playerNotes.Text, InterfaceMgr.Instance.ParentForm);
      }

      private void setText(string str)
      {
        this.playerNotes.Text = str;
        bool flag = false;
        string str1 = this.factionName.Text + "#" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + "#" + str;
        for (int index = 0; index < UserDiplomacyPanel.customNotes.Count; ++index)
        {
          if (UserDiplomacyPanel.customNotes[index].Split('#')[0] == this.factionName.Text)
          {
            UserDiplomacyPanel.customNotes[index] = str1;
            flag = true;
            break;
          }
        }
        if (!flag)
          UserDiplomacyPanel.customNotes.Add(str1);
        this.saveDiplomacyData();
      }

      private void saveDiplomacyData()
      {
        string settingsPath = GameEngine.getSettingsPath(true);
        try
        {
          new FileInfo(settingsPath + "\\DiplomacyDataW" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + "U" + RemoteServices.Instance.UserID.ToString() + ".dat").IsReadOnly = false;
        }
        catch (Exception ex)
        {
        }
        try
        {
          FileStream output = new FileStream(settingsPath + "\\DiplomacyDataW" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + "U" + RemoteServices.Instance.UserID.ToString() + ".dat", FileMode.Create);
          BinaryWriter binaryWriter = new BinaryWriter((Stream) output);
          byte[] byteArray = RemoteServices.Instance.WorldGUID.ToByteArray();
          binaryWriter.Write(byteArray, 0, 16);
          for (int index = 0; index < UserDiplomacyPanel.customNotes.Count; ++index)
            binaryWriter.Write(UserDiplomacyPanel.customNotes[index]);
          binaryWriter.Close();
          output.Close();
        }
        catch (Exception ex)
        {
        }
      }
    }
  }
}
