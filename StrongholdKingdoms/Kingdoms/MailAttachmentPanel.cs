// Decompiled with JetBrains decompiler
// Type: Kingdoms.MailAttachmentPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MailAttachmentPanel : CustomSelfDrawPanel
  {
    public List<MailLink> linkList = new List<MailLink>();
    private CustomSelfDrawPanel.CSDButton btnClose = new CustomSelfDrawPanel.CSDButton();
    private MailAttachmentPopup m_parent;
    private MailScreen m_mailParent;
    private CustomSelfDrawPanel.CSDArea parentArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDButton playerTabButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDArea playerSearchArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDListBox playerSearchList = new CustomSelfDrawPanel.CSDListBox();
    private CustomSelfDrawPanel.CSDButton playerAddButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageTabButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDArea villageSearchArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel villageUserLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDListBox villageSearchList = new CustomSelfDrawPanel.CSDListBox();
    private CustomSelfDrawPanel.CSDButton villageAddButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDArea villageScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDVertScrollBar villageBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDButton regionTabButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDArea regionSearchArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDListBox regionSearchList = new CustomSelfDrawPanel.CSDListBox();
    private CustomSelfDrawPanel.CSDButton regionAddButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton currentTabButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDArea currentAttachmentArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea currentAttachmentScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDVertScrollBar currentAttachmentBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDButton removeLinkButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage backImage = new CustomSelfDrawPanel.CSDImage();
    private int currentTab = -1;
    private bool readOnly;
    private string searchText = "";
    private List<MailAttachmentPanel.VillageLine> villageLines = new List<MailAttachmentPanel.VillageLine>();
    private MailAttachmentPanel.VillageLine selectedVillage;
    private string[] regionNames;
    private List<MailAttachmentPanel.LinkLine> lineList = new List<MailAttachmentPanel.LinkLine>();
    private MailAttachmentPanel.LinkLine selectedLine;

    public MailAttachmentPanel()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void clearContents(bool includeLinks)
    {
      if (includeLinks)
        this.linkList.Clear();
      this.playerSearchList.populate(new List<CustomSelfDrawPanel.CSDListItem>());
      this.villageSearchList.populate(new List<CustomSelfDrawPanel.CSDListItem>());
      this.regionSearchList.populate(new List<CustomSelfDrawPanel.CSDListItem>());
      this.villageTabButton.Active = false;
      this.villageTabButton.Alpha = 0.5f;
      this.villageTabButton.CustomTooltipID = 518;
      this.playerAddButton.Enabled = false;
      this.villageAddButton.Enabled = false;
      this.regionAddButton.Enabled = false;
      this.selectedVillage = (MailAttachmentPanel.VillageLine) null;
      this.selectedLine = (MailAttachmentPanel.LinkLine) null;
      this.changeTabIcons(-1);
    }

    public void init(Size parentSize, MailAttachmentPopup parent, MailScreen parentMail)
    {
      this.Size = parentSize;
      this.m_parent = parent;
      this.m_mailParent = parentMail;
      this.btnClose.Text.Text = SK.Text("GENERIC_Close", "Close");
      this.btnClose.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.btnClose.ImageOver = (Image) GFXLibrary.button_132_over;
      this.btnClose.ImageClick = (Image) GFXLibrary.button_132_in;
      this.btnClose.setSizeToImage();
      this.btnClose.Position = new Point(this.Width / 2 - this.btnClose.Width / 2, this.Height - this.btnClose.Height - 5);
      this.btnClose.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.btnClose.TextYOffset = -2;
      this.btnClose.Text.Color = ARGBColors.Black;
      this.btnClose.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Mail_Attachment_Close");
      this.btnClose.Enabled = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.btnClose);
      this.initIconArea();
    }

    private void initIconArea()
    {
      this.parentArea.Position = new Point(10, 10);
      this.parentArea.Size = new Size(this.Width - 20, this.Height - 20);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.parentArea);
      this.backImage.Image = (Image) GFXLibrary.mail2_new_mail_tab_panel;
      this.backImage.Position = new Point(0, 0);
      this.backImage.setSizeToImage();
      this.backImage.Visible = true;
      this.parentArea.addControl((CustomSelfDrawPanel.CSDControl) this.backImage);
      this.changeTabIcons(-1);
      this.playerTabButton.Position = this.backImage.Position;
      this.playerTabButton.setSizeToImage();
      this.playerTabButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playerTabClick), "Attachment_Player_Tab");
      this.playerTabButton.CustomTooltipID = 505;
      this.parentArea.addControl((CustomSelfDrawPanel.CSDControl) this.playerTabButton);
      this.playerSearchArea.Position = new Point(this.backImage.X, this.playerTabButton.Rectangle.Bottom);
      this.playerSearchArea.Size = new Size(this.backImage.Width, this.backImage.Height - this.playerTabButton.Height);
      this.parentArea.addControl((CustomSelfDrawPanel.CSDControl) this.playerSearchArea);
      this.playerSearchList.Size = new Size(160, 216);
      this.playerSearchList.Position = new Point(this.playerSearchArea.Width / 2 - this.playerSearchList.Width / 2, 40);
      this.playerSearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.playerSearchList);
      this.playerSearchList.Create(12, 18);
      this.playerSearchList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.playerListClick));
      this.playerSearchList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.playerListDoubleClick));
      this.playerAddButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.playerAddButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.playerAddButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.playerAddButton.setSizeToImage();
      this.playerAddButton.Position = new Point(this.playerSearchArea.Width / 2 - this.playerAddButton.Width / 2, this.playerSearchList.Rectangle.Bottom + 5);
      this.playerAddButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.playerAddButton.TextYOffset = -2;
      this.playerAddButton.Text.Text = SK.Text("MailScreen_Add", "Add");
      this.playerAddButton.Text.Color = ARGBColors.Black;
      this.playerAddButton.Enabled = false;
      this.playerAddButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playerAddClick), "Attachments_Add_Player");
      this.playerSearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.playerAddButton);
      this.playerSearchArea.Visible = false;
      this.villageTabButton.Position = new Point(this.playerTabButton.Rectangle.Right, this.playerTabButton.Y);
      this.villageTabButton.setSizeToImage();
      this.villageTabButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageTabClick), "Attachment_Village_Tab");
      this.parentArea.addControl((CustomSelfDrawPanel.CSDControl) this.villageTabButton);
      this.villageTabButton.Enabled = true;
      this.villageTabButton.Active = false;
      this.villageTabButton.Alpha = 0.5f;
      this.villageTabButton.CustomTooltipID = 518;
      this.villageSearchArea.Position = new Point(this.backImage.X, this.playerTabButton.Rectangle.Bottom);
      this.villageSearchArea.Size = new Size(this.backImage.Width, this.backImage.Height - this.playerTabButton.Height);
      this.parentArea.addControl((CustomSelfDrawPanel.CSDControl) this.villageSearchArea);
      this.villageUserLabel.Color = ARGBColors.Black;
      this.villageUserLabel.Position = new Point(1, 1);
      this.villageUserLabel.Size = new Size(this.villageSearchArea.Width - 7, 24);
      this.villageUserLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageUserLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.villageSearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.villageUserLabel);
      this.villageSearchList.Size = new Size(160, 216);
      this.villageSearchList.Position = new Point(this.villageSearchArea.Width / 2 - this.villageSearchList.Width / 2, 40);
      this.villageSearchList.Create(12, 18);
      this.villageSearchList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.villageListClick));
      this.villageSearchList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.villageListDoubleClick));
      this.villageAddButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.villageAddButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.villageAddButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.villageAddButton.setSizeToImage();
      this.villageAddButton.Position = new Point(this.villageSearchArea.Width / 2 - this.villageAddButton.Width / 2, this.villageSearchList.Rectangle.Bottom + 5);
      this.villageAddButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.villageAddButton.TextYOffset = -2;
      this.villageAddButton.Text.Text = SK.Text("MailScreen_Add", "Add");
      this.villageAddButton.Text.Color = ARGBColors.Black;
      this.villageAddButton.Enabled = false;
      this.villageAddButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageAddClick), "Attachments_Add_Village");
      this.villageSearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.villageAddButton);
      this.villageScrollArea.Position = new Point(5, 16);
      this.villageScrollArea.Size = new Size(this.villageSearchArea.Width - 39, this.villageSearchArea.Height - 84);
      this.villageScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(this.villageSearchArea.Width - 39, this.villageSearchArea.Height - 84));
      this.villageSearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.villageScrollArea);
      this.villageBar.Position = new Point(this.villageScrollArea.Rectangle.Right, this.villageScrollArea.Y);
      this.villageBar.Size = new Size(24, this.villageScrollArea.Height);
      this.villageSearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.villageBar);
      this.villageBar.Value = 0;
      this.villageBar.Max = 100;
      this.villageBar.NumVisibleLines = 5;
      this.villageBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.villageBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.villageScrollBarMoved));
      this.villageSearchArea.Visible = false;
      this.regionTabButton.Position = new Point(this.villageTabButton.Rectangle.Right, this.villageTabButton.Y);
      this.regionTabButton.setSizeToImage();
      this.regionTabButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionTabClick), "Attachment_Faction_Tab");
      this.regionTabButton.CustomTooltipID = 512;
      this.parentArea.addControl((CustomSelfDrawPanel.CSDControl) this.regionTabButton);
      this.regionSearchArea.Position = new Point(this.backImage.X, this.playerTabButton.Rectangle.Bottom);
      this.regionSearchArea.Size = new Size(this.backImage.Width, this.backImage.Height - this.playerTabButton.Height);
      this.parentArea.addControl((CustomSelfDrawPanel.CSDControl) this.regionSearchArea);
      this.regionSearchList.Size = new Size(160, 216);
      this.regionSearchList.Position = new Point(this.regionSearchArea.Width / 2 - this.regionSearchList.Width / 2, 40);
      this.regionSearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.regionSearchList);
      this.regionSearchList.Create(12, 18);
      this.regionSearchList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.regionListClick));
      this.regionSearchList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.regionListDoubleClick));
      this.regionAddButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.regionAddButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.regionAddButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.regionAddButton.setSizeToImage();
      this.regionAddButton.Position = new Point(this.regionSearchArea.Width / 2 - this.regionAddButton.Width / 2, this.regionSearchList.Rectangle.Bottom + 5);
      this.regionAddButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.regionAddButton.TextYOffset = -2;
      this.regionAddButton.Text.Text = SK.Text("MailScreen_Add", "Add");
      this.regionAddButton.Text.Color = ARGBColors.Black;
      this.regionAddButton.Enabled = false;
      this.regionAddButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.regionAddClick), "Attachments_Add_Region");
      this.regionSearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.regionAddButton);
      this.regionSearchArea.Visible = false;
      this.currentTabButton.Position = new Point(this.regionTabButton.Rectangle.Right, this.regionTabButton.Y);
      this.currentTabButton.setSizeToImage();
      this.currentTabButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.listTabclick), "Attachment_List_Tab");
      this.currentTabButton.CustomTooltipID = 513;
      this.parentArea.addControl((CustomSelfDrawPanel.CSDControl) this.currentTabButton);
      this.currentAttachmentArea.Position = new Point(this.backImage.X, this.playerTabButton.Rectangle.Bottom);
      this.currentAttachmentArea.Size = new Size(this.backImage.Width, this.backImage.Height - this.playerTabButton.Height);
      this.parentArea.addControl((CustomSelfDrawPanel.CSDControl) this.currentAttachmentArea);
      this.currentAttachmentScrollArea.Position = new Point(5, 10);
      this.currentAttachmentScrollArea.Size = new Size(this.currentAttachmentArea.Width - 39, this.currentAttachmentArea.Height - 60);
      this.currentAttachmentScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(this.currentAttachmentArea.Width - 39, this.currentAttachmentArea.Height - 60));
      this.currentAttachmentArea.addControl((CustomSelfDrawPanel.CSDControl) this.currentAttachmentScrollArea);
      this.currentAttachmentBar.Position = new Point(this.currentAttachmentScrollArea.Rectangle.Right, this.currentAttachmentScrollArea.Y);
      this.currentAttachmentBar.Size = new Size(24, this.currentAttachmentScrollArea.Height);
      this.currentAttachmentArea.addControl((CustomSelfDrawPanel.CSDControl) this.currentAttachmentBar);
      this.currentAttachmentBar.Value = 0;
      this.currentAttachmentBar.Max = 100;
      this.currentAttachmentBar.NumVisibleLines = 5;
      this.currentAttachmentBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.currentAttachmentBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.currentScrollBarMoved));
      this.removeLinkButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.removeLinkButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.removeLinkButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.removeLinkButton.setSizeToImage();
      this.removeLinkButton.Position = new Point(this.currentAttachmentArea.Width / 2 - this.removeLinkButton.Width / 2, this.playerSearchList.Rectangle.Bottom + 5);
      this.removeLinkButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.removeLinkButton.TextYOffset = -2;
      this.removeLinkButton.Text.Text = SK.Text("GENERIC_Remove", "Remove");
      this.removeLinkButton.Text.Color = ARGBColors.Black;
      this.removeLinkButton.Enabled = false;
      this.removeLinkButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.removeLinkLine), "Attachments_Remove_Link");
      this.currentAttachmentArea.addControl((CustomSelfDrawPanel.CSDControl) this.removeLinkButton);
    }

    private void playerTabClick() => this.changeTabIcons(0);

    private void villageTabClick()
    {
      if (this.playerSearchList.getSelectedItem() == null)
        return;
      if (this.villageUserLabel.Text == this.playerSearchList.getSelectedItem().Text)
      {
        this.changeTabIcons(1);
      }
      else
      {
        this.villageUserLabel.Text = this.playerSearchList.getSelectedItem().Text;
        this.loadVillageList(this.playerSearchList.getSelectedItem().Text);
      }
    }

    private void factionTabClick() => this.changeTabIcons(2);

    private void listTabclick()
    {
      this.changeTabIcons(3);
      this.initCurrentAttachments();
    }

    private void changeTabIcons(int tab)
    {
      this.currentTab = tab;
      this.playerTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_player_normal;
      this.playerTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_player_over;
      this.playerTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_player_normal;
      this.playerSearchArea.Visible = false;
      this.villageTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_village_normal;
      this.villageTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_village_over;
      this.villageTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_village_normal;
      this.villageSearchArea.Visible = false;
      this.regionTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_parish_normal;
      this.regionTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_parish_over;
      this.regionTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_parish_normal;
      this.regionSearchArea.Visible = false;
      this.currentTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_current_normal;
      this.currentTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_current_over;
      this.currentTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_current_normal;
      this.currentAttachmentArea.Visible = false;
      switch (tab)
      {
        case 0:
          this.playerTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_player_selected;
          this.playerTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_player_selected;
          this.playerTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_player_selected;
          this.playerSearchArea.Visible = true;
          this.m_parent.setTextBoxVisible(1);
          break;
        case 1:
          this.villageTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_village_selected;
          this.villageTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_village_selected;
          this.villageTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_village_selected;
          this.villageSearchArea.Visible = true;
          this.m_parent.setTextBoxVisible(-1);
          break;
        case 2:
          this.regionTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_parish_selected;
          this.regionTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_parish_selected;
          this.regionTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_parish_selected;
          this.regionSearchArea.Visible = true;
          this.m_parent.setTextBoxVisible(3);
          break;
        case 3:
          this.currentTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_current_selected;
          this.currentTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_current_selected;
          this.currentTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_current_selected;
          this.currentAttachmentArea.Visible = true;
          this.m_parent.setTextBoxVisible(-1);
          break;
        default:
          this.m_parent.setTextBoxVisible(-1);
          break;
      }
    }

    public void setReadOnly(bool value)
    {
      this.readOnly = value;
      if (this.readOnly)
      {
        this.playerTabButton.Enabled = false;
        this.playerSearchArea.Visible = false;
        this.villageTabButton.Active = false;
        this.villageTabButton.Alpha = 0.5f;
        this.villageTabButton.CustomTooltipID = 518;
        this.villageSearchArea.Visible = false;
        this.regionTabButton.Enabled = false;
        this.regionSearchArea.Visible = false;
        this.removeLinkButton.Visible = false;
        this.changeTabIcons(3);
      }
      else
      {
        this.playerTabButton.Enabled = true;
        this.playerSearchArea.Visible = true;
        if (this.playerSearchList.getSelectedItem() != null)
        {
          this.villageTabButton.Active = true;
          this.villageTabButton.Alpha = 1f;
          this.villageTabButton.CustomTooltipID = 511;
        }
        this.villageSearchArea.Visible = true;
        this.regionTabButton.Enabled = true;
        this.regionSearchArea.Visible = true;
        this.removeLinkButton.Visible = true;
        this.changeTabIcons(-1);
      }
    }

    public void searchPlayerUpdateCallback(string textInput)
    {
      switch (this.currentTab)
      {
        case 0:
          RemoteServices.Instance.set_GetMailUserSearch_UserCallBack(new RemoteServices.GetMailUserSearch_UserCallBack(this.getMailUserSearchCallback));
          RemoteServices.Instance.GetMailUserSearch(textInput);
          this.searchText = "";
          this.searchText = textInput;
          break;
      }
    }

    private void playerListClick(CustomSelfDrawPanel.CSDListItem item)
    {
      this.playerAddButton.Enabled = this.playerSearchList.getSelectedItem() != null;
      this.villageTabButton.Active = this.playerAddButton.Enabled;
      this.villageTabButton.Alpha = this.playerAddButton.Enabled ? 1f : 0.5f;
      this.villageTabButton.CustomTooltipID = this.playerAddButton.Enabled ? 511 : 518;
    }

    private void playerListDoubleClick(CustomSelfDrawPanel.CSDListItem item)
    {
      this.addPlayer(item);
    }

    private void playerAddClick()
    {
      if (this.playerSearchList.getSelectedItem() == null)
        return;
      this.addPlayer(this.playerSearchList.getSelectedItem());
    }

    private void addPlayer(CustomSelfDrawPanel.CSDListItem item)
    {
      bool flag = false;
      foreach (MailLink link in this.linkList)
      {
        if (link.linkType == 1 && link.objectName == item.Text)
          flag = true;
      }
      if (flag)
        return;
      this.linkList.Add(new MailLink()
      {
        linkType = 1,
        objectName = item.Text,
        objectID = -1
      });
      this.playerSearchList.highlightedItems.Add(item);
      this.playerSearchList.updateEntries();
    }

    private void getMailUserSearchCallback(GetMailUserSearch_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
      if (returnData.mailUsersSearch != null)
      {
        this.playerSearchList.highlightedItems.Clear();
        foreach (string str in returnData.mailUsersSearch)
        {
          CustomSelfDrawPanel.CSDListItem csdListItem = new CustomSelfDrawPanel.CSDListItem();
          csdListItem.Text = str;
          items.Add(csdListItem);
          foreach (MailLink link in this.linkList)
          {
            if (link.linkType == 1 && link.objectName.ToLower() == str.ToLower())
              this.playerSearchList.highlightedItems.Add(csdListItem);
          }
        }
      }
      string userName = RemoteServices.Instance.UserName;
      if (userName.ToLower().Contains(this.searchText.ToLower()))
      {
        CustomSelfDrawPanel.CSDListItem csdListItem = new CustomSelfDrawPanel.CSDListItem();
        csdListItem.Text = userName;
        items.Add(csdListItem);
        foreach (MailLink link in this.linkList)
        {
          if (link.linkType == 1 && link.objectName.ToLower() == userName.ToLower())
            this.playerSearchList.highlightedItems.Add(csdListItem);
        }
      }
      this.playerSearchList.populate(items);
      this.playerAddButton.Enabled = this.playerSearchList.getSelectedItem() != null;
      this.villageTabButton.Active = this.playerAddButton.Enabled;
      this.villageTabButton.Alpha = this.playerAddButton.Enabled ? 1f : 0.5f;
      this.villageTabButton.CustomTooltipID = this.playerAddButton.Enabled ? 511 : 518;
    }

    private void loadVillageList(string targetUser)
    {
      RemoteServices.Instance.set_GetOtherUserVillageIDList_UserCallBack(new RemoteServices.GetOtherUserVillageIDList_UserCallBack(this.villageUserInfoCallback));
      RemoteServices.Instance.GetOtherUserVillageIDList(targetUser);
    }

    public void villageUserInfoCallback(GetOtherUserVillageIDList_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.villageSearchList.populate(new List<CustomSelfDrawPanel.CSDListItem>());
      List<VillageData> villageDataList1 = new List<VillageData>();
      List<VillageData> villageDataList2 = new List<VillageData>();
      List<VillageData> villageDataList3 = new List<VillageData>();
      List<VillageData> villageDataList4 = new List<VillageData>();
      List<VillageData> villageDataList5 = new List<VillageData>();
      foreach (int userVillage in returnData.userVillageList)
      {
        VillageData villageData = GameEngine.Instance.World.getVillageData(userVillage);
        if (villageData != null)
        {
          if (villageData.regionCapital)
            villageDataList2.Add(villageData);
          else if (villageData.countyCapital)
            villageDataList3.Add(villageData);
          else if (villageData.provinceCapital)
            villageDataList4.Add(villageData);
          else if (villageData.countryCapital)
            villageDataList5.Add(villageData);
          else
            villageDataList1.Add(villageData);
        }
      }
      this.villageLines.Clear();
      this.villageScrollArea.clearControls();
      this.villageSearchArea.invalidate();
      int num = 0;
      int position = 0;
      foreach (VillageData data in villageDataList1)
      {
        MailAttachmentPanel.VillageLine control = new MailAttachmentPanel.VillageLine();
        if (num != 0)
          num += 2;
        control.Position = new Point(3, num);
        control.init(position, this.villageScrollArea.Width - 2, data, 1, this);
        this.villageScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num += control.Height;
        this.villageLines.Add(control);
        ++position;
      }
      foreach (VillageData data in villageDataList2)
      {
        MailAttachmentPanel.VillageLine control = new MailAttachmentPanel.VillageLine();
        if (num != 0)
          num += 2;
        control.Position = new Point(3, num);
        control.init(position, this.villageScrollArea.Width, data, 2, this);
        this.villageScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num += control.Height;
        this.villageLines.Add(control);
        ++position;
      }
      foreach (VillageData data in villageDataList3)
      {
        MailAttachmentPanel.VillageLine control = new MailAttachmentPanel.VillageLine();
        if (num != 0)
          num += 2;
        control.Position = new Point(3, num);
        control.init(position, this.villageScrollArea.Width, data, 3, this);
        this.villageScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num += control.Height;
        this.villageLines.Add(control);
        ++position;
      }
      foreach (VillageData data in villageDataList4)
      {
        MailAttachmentPanel.VillageLine control = new MailAttachmentPanel.VillageLine();
        if (num != 0)
          num += 2;
        control.Position = new Point(3, num);
        control.init(position, this.villageScrollArea.Width, data, 4, this);
        this.villageScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num += control.Height;
        this.villageLines.Add(control);
        ++position;
      }
      foreach (VillageData data in villageDataList5)
      {
        MailAttachmentPanel.VillageLine control = new MailAttachmentPanel.VillageLine();
        if (num != 0)
          num += 2;
        control.Position = new Point(3, num);
        control.init(position, this.villageScrollArea.Width, data, 5, this);
        this.villageScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num += control.Height;
        this.villageLines.Add(control);
        ++position;
      }
      this.villageAddButton.Enabled = false;
      this.villageScrollArea.Size = new Size(this.villageScrollArea.Width, num);
      if (num < this.villageBar.Height)
      {
        this.villageBar.Visible = false;
      }
      else
      {
        this.villageBar.Visible = true;
        this.villageBar.NumVisibleLines = this.villageBar.Height;
        this.villageBar.Max = num - this.villageBar.Height;
      }
      this.villageScrollArea.invalidate();
      this.villageBar.invalidate();
      this.changeTabIcons(1);
    }

    private void villageListClick(CustomSelfDrawPanel.CSDListItem item)
    {
      this.villageAddButton.Enabled = this.villageSearchList.getSelectedItem() != null;
    }

    private void villageListDoubleClick(CustomSelfDrawPanel.CSDListItem item)
    {
      this.addVillage(item);
    }

    private void villageAddClick()
    {
      if (this.selectedVillage == null)
        return;
      this.addVillage(this.selectedVillage);
    }

    private void addVillage(CustomSelfDrawPanel.CSDListItem item)
    {
      bool flag = false;
      foreach (MailLink link in this.linkList)
      {
        if (link.linkType == 2 && link.objectID == item.Data)
          flag = true;
      }
      if (flag)
        return;
      this.linkList.Add(new MailLink()
      {
        linkType = 2,
        objectName = item.Text,
        objectID = item.Data
      });
      int num = (int) MyMessageBox.Show(SK.Text("Attachments__Added", "Added to mail"));
    }

    private void addVillage(MailAttachmentPanel.VillageLine line)
    {
      bool flag = false;
      foreach (MailLink link in this.linkList)
      {
        if (link.linkType == 2 && link.objectID == line.villageID)
          flag = true;
      }
      if (flag)
        return;
      this.linkList.Add(new MailLink()
      {
        linkType = 2,
        objectName = line.nameLabel.Text,
        objectID = line.villageID
      });
      line.isAdded = true;
      line.invalidate();
    }

    private void villageScrollBarMoved()
    {
      int y = this.villageBar.Value;
      this.villageScrollArea.Position = new Point(this.villageScrollArea.X, 24 - y);
      this.villageScrollArea.ClipRect = new Rectangle(this.villageScrollArea.ClipRect.X, y, this.villageScrollArea.ClipRect.Width, this.villageScrollArea.ClipRect.Height);
      this.villageScrollArea.invalidate();
      this.villageBar.invalidate();
    }

    public void setSelectedVillage(MailAttachmentPanel.VillageLine inputLine)
    {
      this.selectedVillage = inputLine;
      foreach (MailAttachmentPanel.VillageLine villageLine in this.villageLines)
      {
        villageLine.isSelected(villageLine == inputLine);
        villageLine.invalidate();
      }
      this.villageAddButton.Enabled = this.selectedVillage != null;
    }

    public void searchRegionUpdateCallback(string textInput)
    {
      if (this.regionNames == null)
        this.regionNames = GameEngine.Instance.World.getParishNameList();
      List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
      this.regionSearchList.highlightedItems.Clear();
      foreach (string regionName in this.regionNames)
      {
        if (regionName.ToLower().Contains(textInput.ToLower()))
        {
          CustomSelfDrawPanel.CSDListItem csdListItem = new CustomSelfDrawPanel.CSDListItem();
          csdListItem.Text = regionName;
          csdListItem.Data = GameEngine.Instance.World.getParishIDFromName(regionName);
          items.Add(csdListItem);
          foreach (MailLink link in this.linkList)
          {
            if (link.linkType == 3 && link.objectName.ToLower() == regionName.ToLower())
              this.regionSearchList.highlightedItems.Add(csdListItem);
          }
        }
      }
      this.regionSearchList.populate(items);
      this.regionAddButton.Enabled = this.playerSearchList.getSelectedItem() != null;
    }

    private void regionListClick(CustomSelfDrawPanel.CSDListItem item)
    {
      this.regionAddButton.Enabled = this.regionSearchList.getSelectedItem() != null;
    }

    private void regionListDoubleClick(CustomSelfDrawPanel.CSDListItem item)
    {
      this.addRegion(item);
    }

    private void regionAddClick()
    {
      if (this.regionSearchList.getSelectedItem() == null)
        return;
      this.addRegion(this.regionSearchList.getSelectedItem());
    }

    private void addRegion(CustomSelfDrawPanel.CSDListItem item)
    {
      bool flag = false;
      foreach (MailLink link in this.linkList)
      {
        if (link.linkType == 3 && link.objectName == item.Text)
          flag = true;
      }
      if (flag)
        return;
      this.linkList.Add(new MailLink()
      {
        linkType = 3,
        objectName = item.Text,
        objectID = item.Data
      });
      this.regionSearchList.highlightedItems.Add(item);
      this.regionSearchList.clearSelectedItem();
    }

    public void initCurrentAttachments()
    {
      this.lineList.Clear();
      this.currentAttachmentScrollArea.clearControls();
      this.currentAttachmentArea.invalidate();
      int num = 0;
      int position = 0;
      foreach (MailLink link in this.linkList)
      {
        MailAttachmentPanel.LinkLine control = new MailAttachmentPanel.LinkLine();
        if (num != 0)
          num += 2;
        control.Position = new Point(3, num);
        control.init(link, position, this.currentAttachmentScrollArea.Width - 6, this.readOnly, this);
        this.currentAttachmentScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num += control.Height;
        this.lineList.Add(control);
        ++position;
      }
      this.removeLinkButton.Enabled = this.linkList.Count > 0;
      this.currentAttachmentScrollArea.Size = new Size(this.currentAttachmentScrollArea.Width, num);
      if (num < this.currentAttachmentBar.Height)
      {
        this.currentAttachmentBar.Visible = false;
      }
      else
      {
        this.currentAttachmentBar.Visible = true;
        this.currentAttachmentBar.NumVisibleLines = this.currentAttachmentBar.Height;
        this.currentAttachmentBar.Max = num - this.currentAttachmentBar.Height;
      }
      this.currentAttachmentScrollArea.invalidate();
      this.currentAttachmentBar.invalidate();
    }

    private void currentScrollBarMoved()
    {
      int y = this.currentAttachmentBar.Value;
      this.currentAttachmentScrollArea.Position = new Point(this.currentAttachmentScrollArea.X, -y);
      this.currentAttachmentScrollArea.ClipRect = new Rectangle(this.currentAttachmentScrollArea.ClipRect.X, y, this.currentAttachmentScrollArea.ClipRect.Width, this.currentAttachmentScrollArea.ClipRect.Height);
      this.currentAttachmentScrollArea.invalidate();
      this.currentAttachmentBar.invalidate();
    }

    public void setSelectedLine(MailAttachmentPanel.LinkLine inputLine)
    {
      this.selectedLine = inputLine;
      foreach (MailAttachmentPanel.LinkLine line in this.lineList)
      {
        line.isSelected(line == inputLine);
        line.invalidate();
      }
      this.removeLinkButton.Enabled = this.selectedLine != null;
    }

    private void removeLinkLine()
    {
      if (this.selectedLine == null)
        return;
      this.linkList.Remove(this.selectedLine.parentLink);
      this.initCurrentAttachments();
      this.removeLinkButton.Enabled = false;
      CustomSelfDrawPanel.CSDListItem csdListItem = (CustomSelfDrawPanel.CSDListItem) null;
      switch (this.selectedLine.parentLink.linkType)
      {
        case 1:
          foreach (CustomSelfDrawPanel.CSDListItem highlightedItem in this.playerSearchList.highlightedItems)
          {
            if (highlightedItem.Text == this.selectedLine.parentLink.objectName)
              csdListItem = highlightedItem;
          }
          if (csdListItem == null)
            break;
          this.playerSearchList.highlightedItems.Remove(csdListItem);
          this.playerSearchList.updateEntries();
          if (this.playerSearchList.getSelectedItem() != csdListItem)
            break;
          this.playerSearchList.clearSelectedItem();
          this.villageTabButton.Active = false;
          this.villageTabButton.Alpha = 0.5f;
          this.villageTabButton.CustomTooltipID = 518;
          break;
        case 2:
          using (List<MailAttachmentPanel.VillageLine>.Enumerator enumerator = this.villageLines.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              MailAttachmentPanel.VillageLine current = enumerator.Current;
              if (current.nameLabel.Text.ToLower() == this.selectedLine.parentLink.objectName.ToLower())
                current.isAdded = false;
            }
            break;
          }
        case 3:
          foreach (CustomSelfDrawPanel.CSDListItem highlightedItem in this.regionSearchList.highlightedItems)
          {
            if (highlightedItem.Text.ToLower() == this.selectedLine.parentLink.objectName.ToLower())
              csdListItem = highlightedItem;
          }
          if (csdListItem == null)
            break;
          this.regionSearchList.highlightedItems.Remove(csdListItem);
          this.regionSearchList.clearSelectedItem();
          break;
      }
    }

    public void closeClick()
    {
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_close");
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
    }

    public class VillageLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage sizeImage = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
      public int villageID = -1;
      public bool isAddedFlag;
      private MailAttachmentPanel m_parent;

      public bool isAdded
      {
        set
        {
          this.isAddedFlag = value;
          if (value)
            this.nameLabel.Color = ARGBColors.Chartreuse;
          else
            this.nameLabel.Color = ARGBColors.Black;
        }
      }

      public void init(
        int position,
        int width,
        VillageData data,
        int villageSize,
        MailAttachmentPanel parent)
      {
        this.m_parent = parent;
        this.villageID = data.id;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.char_line_02 : (Image) GFXLibrary.char_line_01;
        this.backgroundImage.Position = new Point(0, 5);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = new Size(width, 30);
        if (GameEngine.Instance.World.isCapital(this.villageID))
        {
          int num = 0;
          if (GameEngine.Instance.World.isRegionCapital(this.villageID))
            num = 0;
          else if (GameEngine.Instance.World.isCountyCapital(this.villageID))
            num = 1;
          else if (GameEngine.Instance.World.isProvinceCapital(this.villageID))
            num = 2;
          else if (GameEngine.Instance.World.isCountryCapital(this.villageID))
            num = 3;
          this.sizeImage.Image = (Image) GFXLibrary.char_position[num + 4];
          this.sizeImage.Position = new Point(5, -4);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.sizeImage);
        }
        else
        {
          int villageSize1 = GameEngine.Instance.World.getVillageSize(this.villageID);
          this.sizeImage.Image = (Image) GFXLibrary.char_village_icons[villageSize1];
          this.sizeImage.Position = new Point(-10, -18);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.sizeImage);
        }
        this.nameLabel.Color = this.isAddedFlag ? ARGBColors.Chartreuse : ARGBColors.Black;
        this.nameLabel.Position = new Point(35, -10);
        this.nameLabel.Size = new Size(this.Width, this.backgroundImage.Height + 20);
        this.nameLabel.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.nameLabel.Text = data.m_villageName;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.nameLabel);
        this.sizeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.nameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
      }

      public void isSelected(bool value)
      {
        this.nameLabel.Font = FontManager.GetFont("Arial", 8.25f, value ? FontStyle.Bold : FontStyle.Regular);
      }

      private void lineClicked() => this.m_parent.setSelectedVillage(this);
    }

    public class LinkLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage linkTypeImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
      public MailLink parentLink;
      private bool clickable;
      private MailAttachmentPanel m_parent;

      public void init(
        MailLink link,
        int position,
        int width,
        bool isClickable,
        MailAttachmentPanel parent)
      {
        this.m_parent = parent;
        this.parentLink = link;
        this.clickable = isClickable;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.char_line_02 : (Image) GFXLibrary.char_line_01;
        this.backgroundImage.Position = new Point(0, 5);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = new Size(width, 30);
        this.nameLabel.Color = ARGBColors.Black;
        this.nameLabel.RolloverColor = ARGBColors.White;
        this.nameLabel.Position = new Point(1, -10);
        this.nameLabel.Size = new Size(this.Width, this.backgroundImage.Height + 20);
        this.nameLabel.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.nameLabel.Text = this.parentLink.objectName;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.nameLabel);
        switch (this.parentLink.linkType)
        {
          case 1:
            this.linkTypeImage.Image = (Image) GFXLibrary.mail2_attach_type_player;
            this.backgroundImage.CustomTooltipID = 515;
            this.nameLabel.CustomTooltipID = 515;
            this.linkTypeImage.CustomTooltipID = 515;
            break;
          case 2:
            this.linkTypeImage.Image = (Image) GFXLibrary.mail2_attach_type_village;
            this.backgroundImage.CustomTooltipID = 516;
            this.nameLabel.CustomTooltipID = 516;
            this.linkTypeImage.CustomTooltipID = 516;
            break;
          case 3:
            this.linkTypeImage.Image = (Image) GFXLibrary.mail2_attach_type_parish;
            this.backgroundImage.CustomTooltipID = 517;
            this.nameLabel.CustomTooltipID = 517;
            this.linkTypeImage.CustomTooltipID = 517;
            break;
        }
        this.linkTypeImage.setSizeToImage();
        this.linkTypeImage.Position = new Point(this.Width - this.linkTypeImage.Width, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.linkTypeImage);
        this.backgroundImage.Width = this.Width - this.linkTypeImage.Width / 2;
        this.linkTypeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.nameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
      }

      public void isSelected(bool value)
      {
        this.nameLabel.Font = FontManager.GetFont("Arial", 8.25f, value ? FontStyle.Bold : FontStyle.Regular);
      }

      private void lineClicked()
      {
        this.m_parent.setSelectedLine(this);
        Point point = new Point(-1, -1);
        if (!this.clickable)
          return;
        switch (this.parentLink.linkType)
        {
          case 1:
            RemoteServices.Instance.set_GetUserIDFromName_UserCallBack(new RemoteServices.GetUserIDFromName_UserCallBack(this.getUserIDFromNameCallback));
            RemoteServices.Instance.GetUserIDFromName(this.parentLink.objectName);
            break;
          case 2:
            Point villageLocation1 = GameEngine.Instance.World.getVillageLocation(this.parentLink.objectID);
            if (villageLocation1.X != -1)
            {
              InterfaceMgr.Instance.changeTab(0);
              GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation1.X, (double) villageLocation1.Y);
              InterfaceMgr.Instance.displaySelectedVillagePanel(this.parentLink.objectID, false, true, false, false);
              InterfaceMgr.Instance.reactiveMainWindow();
              break;
            }
            int num1 = (int) MyMessageBox.Show(SK.Text("Attachment_Invalid", "This attachment is invalid"));
            break;
          case 3:
            int parishCapital = GameEngine.Instance.World.getParishCapital(this.parentLink.objectID);
            if (parishCapital == -1)
            {
              int num2 = (int) MyMessageBox.Show(SK.Text("Attachment_Invalid", "This attachment is invalid"));
              break;
            }
            Point villageLocation2 = GameEngine.Instance.World.getVillageLocation(parishCapital);
            if (villageLocation2.X != -1)
            {
              InterfaceMgr.Instance.changeTab(0);
              GameEngine.Instance.World.startMultiStageZoom(1000.0, (double) villageLocation2.X, (double) villageLocation2.Y);
              InterfaceMgr.Instance.displaySelectedVillagePanel(parishCapital, false, true, false, false);
              InterfaceMgr.Instance.reactiveMainWindow();
              break;
            }
            int num3 = (int) MyMessageBox.Show(SK.Text("Attachment_Invalid", "This attachment is invalid"));
            break;
        }
      }

      private void getUserIDFromNameCallback(GetUserIDFromName_ReturnType returnData)
      {
        if (!returnData.Success || returnData.userID == -1)
          return;
        InterfaceMgr.Instance.changeTab(0);
        InterfaceMgr.Instance.showUserInfoScreen(new WorldMap.CachedUserInfo()
        {
          userID = returnData.userID
        });
      }
    }
  }
}
