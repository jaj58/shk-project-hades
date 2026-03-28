// Decompiled with JetBrains decompiler
// Type: Kingdoms.PlayCardsWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Kingdoms.Properties;
using StatTracking;
using Stronghold.AuthClient;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PlayCardsWindow : Form
  {
    private CustomSelfDrawPanel currentPanel;
    private PlayCardsPanel cardPanelPlay;
    private BuyCardsPanel cardPanelBuy;
    public ManageCardsPanel cardPanelManage;
    private PremiumCardsPanel cardPanelPremium;
    private ViewAllCardsPanel cardPanelViewAll;
    private PremiumOffersPanel cardPanelOffers;
    private BuyCrownsPanel crownsBuyPanel;
    private int currentPanelID;
    private int currentCardSection;
    private bool processTextChanged = true;
    public static bool CrownsOpened = false;
    private static DateTime m_lastRewardCardsCall = DateTime.MinValue;
    private bool m_fromOpen;
    private static DateTime lastUpdatedBPURL = DateTime.MinValue;
    private static string bigpointURL = string.Empty;
    private static DateTime lastCrownsOpened = DateTime.MinValue;
    private bool closing;
    private IContainer components;
    public TextBox tbSearchBox;

    public ManageCardsPanel CardPanelManage => this.cardPanelManage;

    public int CurrentPanelID => this.currentPanelID;

    public void SwitchPanel(int panel)
    {
      this.cardPanelPlay.clearControls();
      this.cardPanelBuy.clearControls();
      this.cardPanelManage.clearControls();
      this.cardPanelPremium.clearControls();
      this.cardPanelViewAll.clearControls();
      this.cardPanelOffers.clearControls();
      this.crownsBuyPanel.clearControls();
      if (panel == this.currentPanelID)
        panel = 1;
      switch (panel)
      {
        case 1:
          this.currentPanel = (CustomSelfDrawPanel) this.cardPanelPlay;
          break;
        case 2:
          this.currentPanel = (CustomSelfDrawPanel) this.cardPanelBuy;
          break;
        case 4:
          this.currentPanel = (CustomSelfDrawPanel) this.cardPanelPremium;
          break;
        case 6:
          this.currentPanel = (CustomSelfDrawPanel) this.cardPanelManage;
          break;
        case 7:
          this.currentPanel = (CustomSelfDrawPanel) this.crownsBuyPanel;
          break;
        case 8:
          this.currentPanel = (CustomSelfDrawPanel) this.cardPanelViewAll;
          break;
        case 9:
          this.currentPanel = (CustomSelfDrawPanel) this.cardPanelOffers;
          break;
        default:
          this.currentPanel = (CustomSelfDrawPanel) this.cardPanelPlay;
          break;
      }
      this.tbSearchBox.Parent.Controls.Remove((Control) this.tbSearchBox);
      this.currentPanel.Controls.Add((Control) this.tbSearchBox);
      this.processTextChanged = false;
      this.tbSearchBox.Text = "";
      this.processTextChanged = true;
      this.tbSearchBox.Visible = false;
      this.currentPanelID = panel;
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(this.currentPanel.GetType());
      this.SuspendLayout();
      this.currentPanel.Location = new Point(0, 0);
      this.currentPanel.Name = "cardPanel";
      this.currentPanel.Size = new Size(1000, 600);
      this.currentPanel.StoredGraphics = (Graphics) null;
      this.currentPanel.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(1000, 600);
      this.ControlBox = false;
      this.Controls.Clear();
      this.Controls.Add((Control) this.currentPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (PlayCardsWindow);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (PlayCardsWindow);
      this.ResumeLayout(false);
      this.init(this.currentCardSection, false);
    }

    public void SwitchToManageAndFilter(int filter, int cardType)
    {
      this.SwitchPanel(6);
      this.cardPanelManage.setFilter(filter);
      this.cardPanelManage.SwitchToBuy();
      this.cardPanelManage.addCardToCard(cardType, false);
    }

    public PlayCardsWindow()
    {
      this.cardPanelPlay = new PlayCardsPanel();
      this.cardPanelBuy = new BuyCardsPanel();
      this.cardPanelManage = new ManageCardsPanel();
      this.cardPanelPremium = new PremiumCardsPanel();
      this.cardPanelViewAll = new ViewAllCardsPanel();
      this.crownsBuyPanel = new BuyCrownsPanel();
      this.cardPanelOffers = new PremiumOffersPanel();
      this.currentPanel = (CustomSelfDrawPanel) this.cardPanelPlay;
      this.currentPanelID = 1;
      this.InitializeComponent();
      this.tbSearchBox.Font = FontManager.GetFont("Arial", 9.75f, FontStyle.Regular);
      this.tbSearchBox.Parent.Controls.Remove((Control) this.tbSearchBox);
      this.currentPanel.Controls.Add((Control) this.tbSearchBox);
      this.processTextChanged = false;
      this.tbSearchBox.Text = "";
      this.processTextChanged = true;
      this.tbSearchBox.Visible = false;
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.TransparencyKey = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.BackColor = this.TransparencyKey;
      if (GameEngine.Instance.World.TutorialIsAdvancing())
        return;
      if (GameEngine.Instance.World.getTutorialStage() == 8)
        GameEngine.Instance.World.checkQuestObjectiveComplete(7);
      if (GameEngine.Instance.World.getTutorialStage() == 12)
        GameEngine.Instance.World.checkQuestObjectiveComplete(11);
      if (GameEngine.Instance.World.getTutorialStage() != 102)
        return;
      GameEngine.Instance.World.checkQuestObjectiveComplete(13);
    }

    public static void resetRewardCardTimer()
    {
      PlayCardsWindow.m_lastRewardCardsCall = DateTime.MinValue;
    }

    public static void logout()
    {
      PlayCardsWindow.CrownsOpened = false;
      PlayCardsWindow.resetRewardCardTimer();
    }

    public void init(int cardSection, bool fromOpen)
    {
      this.m_fromOpen = fromOpen;
      this.currentCardSection = cardSection;
      int num = 180;
      if (PlayCardsWindow.CrownsOpened)
        num = 30;
      if (DateTime.Now.Subtract(GameEngine.Instance.World.LastUpdatedCrowns).TotalSeconds > (double) num)
        this.UpdateCrowns();
      if (this.m_fromOpen && GameEngine.Instance.World.isTutorialActive() && DateTime.Now.Subtract(PlayCardsWindow.m_lastRewardCardsCall).TotalSeconds > 600.0)
        this.UpdateRewardCards();
      ((CustomSelfDrawPanel.ICardsPanel) this.currentPanel).init(cardSection);
    }

    public void SetCardSection(int cardSection) => this.currentCardSection = cardSection;

    public void update() => ((CustomSelfDrawPanel.ICardsPanel) this.currentPanel).update();

    public void UpdateRewardCards()
    {
      ICardsProvider forEndpoint = (ICardsProvider) XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
      ICardsRequest req = (ICardsRequest) new XmlRpcCardsRequest();
      req.UserGUID = RemoteServices.Instance.UserGuid.ToString("N");
      req.SessionGUID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", "");
      req.WorldID = RemoteServices.Instance.ProfileWorldID.ToString();
      ((XmlRpcCardsProvider) forEndpoint).getRewardCards(req, new CardsEndResponseDelegate(this.getRewardcardsCallback), (Control) this);
    }

    private void getRewardcardsCallback(ICardsProvider sender, ICardsResponse response)
    {
      PlayCardsWindow.m_lastRewardCardsCall = DateTime.Now;
      foreach (int key in response.Cards.Keys)
      {
        if (!GameEngine.Instance.cardsManager.ProfileCards.ContainsKey(key))
        {
          GameEngine.Instance.cardsManager.addProfileCard(key, response.Cards[key]);
          GameEngine.Instance.cardsManager.ProfileCards[key].rewardcard = true;
          GameEngine.Instance.cardsManager.ProfileCards[key].worldID = RemoteServices.Instance.ProfileWorldID;
        }
      }
      if (response.Cardpoints.HasValue)
        GameEngine.Instance.World.FakeCardPoints = response.Cardpoints.Value;
      ((CustomSelfDrawPanel.ICardsPanel) this.currentPanel).init(this.currentCardSection);
    }

    public void UpdateCrowns()
    {
      XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath).getCrowns((ICardsRequest) new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""))
      {
        SessionGUID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", "")
      }, new CardsEndResponseDelegate(this.UpdateCrownsCallback), (Control) this);
    }

    public void UpdateCrownsCallback(ICardsProvider provider, ICardsResponse response)
    {
      if (response.SuccessCode.Value != 1)
        return;
      GameEngine.Instance.World.ProfileCrowns = response.Crowns.Value;
      GameEngine.Instance.World.LastUpdatedCrowns = DateTime.Now;
      GameEngine.Instance.cardPackManager.ProfileUserCardPacks = response.UserCardPacks;
    }

    public void InviteAFriend()
    {
      string fileName = URLs.InviteAFriendURL + "?webtoken=" + RemoteServices.Instance.WebToken + "&lang=" + Program.mySettings.LanguageIdent.ToLower();
      try
      {
        Process.Start(fileName);
      }
      catch (Exception ex)
      {
        int num = (int) MyMessageBox.Show(SK.Text("ERROR_Browser1", "Stronghold Kingdoms encountered an error when trying to open your system's Default Web Browser. Please check that your web browser is working correctly and there are no unresponsive copies showing in task manager->Processes and then try again.") + Environment.NewLine + Environment.NewLine + SK.Text("ERROR_Browser2", "If this problem persists, please contact support."), SK.Text("ERROR_Browser3", "Error opening Web Browser"));
      }
    }

    public void GetBigpointURL()
    {
      XmlRpcBPProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressBigpoint, URLs.ProfileServerPort, URLs.ProfileBPPath).GetPaymentURL((IBPRequest) new XmlRpcBPRequest()
      {
        SessionID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""),
        UserGUID = RemoteServices.Instance.UserGuid.ToString().Replace("-", "")
      }, new BPEndResponseDelegate(this.GetbigpointURLCallback), (Control) this);
    }

    public void GetbigpointURLCallback(IBPProvider provider, IBPResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 1 : (!successCode.HasValue ? 1 : 0)) != 0)
      {
        int num = (int) MyMessageBox.Show("");
      }
      else
      {
        PlayCardsWindow.lastUpdatedBPURL = DateTime.Now;
        PlayCardsWindow.bigpointURL = response.URL;
        try
        {
          if (PlayCardsWindow.bigpointURL.Length <= 0)
            return;
          Process.Start(PlayCardsWindow.bigpointURL);
        }
        catch (Exception ex)
        {
          this.fireURLError();
        }
      }
    }

    public void fireURLError()
    {
      int num = (int) MyMessageBox.Show("Stronghold Kingdoms encountered an error when trying to " + Environment.NewLine + "open your system's Default Web Browser. Please check that " + Environment.NewLine + "your web browser is working correctly and there are no unresponsive " + Environment.NewLine + "copies showing in task manager->Processes and then try again." + Environment.NewLine + "If this problem persists, please contact support.", "Error opening Web Browser");
    }

    public void GetCrowns() => this.GetCrowns("");

    public void GetCrowns(string urlExtra)
    {
      PlayCardsWindow.CrownsOpened = true;
      if (Program.steamActive || Program.aeriaInstall)
      {
        this.SwitchPanel(7);
      }
      else
      {
        if ((DateTime.Now - PlayCardsWindow.lastCrownsOpened).TotalSeconds < 5.0)
          return;
        PlayCardsWindow.lastCrownsOpened = DateTime.Now;
        if (GameEngine.Instance.World.isBigpointAccount || Program.bigpointPartnerInstall)
        {
          TimeSpan timeSpan = DateTime.Now - PlayCardsWindow.lastUpdatedBPURL;
          if (PlayCardsWindow.bigpointURL != string.Empty)
          {
            if (timeSpan.TotalMinutes < 2.0)
            {
              try
              {
                Process.Start(PlayCardsWindow.bigpointURL);
                return;
              }
              catch (Exception ex)
              {
                this.fireURLError();
                return;
              }
            }
          }
          this.GetBigpointURL();
        }
        else
        {
          string fileName = URLs.ProfilePaymentURL + "?u=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&s=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "") + "&lang=" + Program.mySettings.LanguageIdent.ToLower();
          if (Program.arcInstall)
          {
            Program.arc_openURL("https://billing.arcgames.com/" + Program.mySettings.languageIdent);
            InterfaceMgr.Instance.closePlayCardsWindow();
            TutorialPanel.minimizeTutorial();
          }
          else
          {
            try
            {
              if (urlExtra.Length >= 0)
                fileName += urlExtra;
              Process.Start(fileName);
            }
            catch (Exception ex)
            {
              this.fireURLError();
            }
          }
        }
      }
    }

    public void GetCrownsCallback(ICardsProvider provider, ICardsResponse response)
    {
    }

    private void PlayCardsWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing || this.closing)
        return;
      this.closing = true;
      StatTrackingClient.Instance().ActivateTrigger(21, (object) null);
      InterfaceMgr.Instance.closePlayCardsWindow();
    }

    private void tbSearchBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.processTextChanged)
        return;
      if (!this.tbSearchBox.Visible)
        return;
      try
      {
        if (this.currentPanelID == 1)
          ((PlayCardsPanel) this.currentPanel).handleSearchTextChanged();
        if (this.currentPanelID != 6)
          return;
        ((ManageCardsPanel) this.currentPanel).handleSearchTextChanged();
      }
      catch (Exception ex)
      {
      }
    }

    public void performSearch()
    {
      try
      {
        if (this.currentPanelID != 1)
          return;
        ((PlayCardsPanel) this.currentPanel).forceSearch();
      }
      catch (Exception ex)
      {
      }
    }

    public string getNameSearchText() => this.tbSearchBox.Visible ? this.tbSearchBox.Text : "";

    public void reactivatePanel() => this.currentPanel.PanelActive = true;

    public bool isCardWindowOnManage() => this.currentPanel == this.cardPanelManage;

    public bool isCardWindowOnPremium() => this.currentPanel == this.cardPanelPremium;

    public bool isCardWindowOnOffers() => this.currentPanel == this.cardPanelOffers;

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (Program.arcInstall && keyData == (Keys.Tab | Keys.Shift))
        Program.arc_forceoverlay();
      return base.ProcessCmdKey(ref msg, keyData);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PlayCardsWindow));
      this.currentPanel = (CustomSelfDrawPanel) new PlayCardsPanel();
      this.tbSearchBox = new TextBox();
      this.SuspendLayout();
      this.currentPanel.Location = new Point(0, 0);
      this.currentPanel.Name = "cardPanel";
      this.currentPanel.Size = new Size(1000, 600);
      this.currentPanel.StoredGraphics = (Graphics) null;
      this.currentPanel.TabIndex = 0;
      this.tbSearchBox.Name = "tbSearchBox";
      this.tbSearchBox.Location = new Point(770, 7);
      this.tbSearchBox.Size = new Size(160, 20);
      this.tbSearchBox.TabIndex = 1;
      this.tbSearchBox.TextChanged += new EventHandler(this.tbSearchBox_TextChanged);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(1000, 600);
      this.ControlBox = false;
      this.Controls.Add((Control) this.currentPanel);
      this.Controls.Add((Control) this.tbSearchBox);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (PlayCardsWindow);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (PlayCardsWindow);
      this.FormClosing += new FormClosingEventHandler(this.PlayCardsWindow_FormClosing);
      this.ResumeLayout(false);
    }
  }
}
