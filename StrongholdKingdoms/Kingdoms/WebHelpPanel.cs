// Decompiled with JetBrains decompiler
// Type: Kingdoms.WebHelpPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class WebHelpPanel : UserControl, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private KingdomsBrowserGecko geckoWebBrowser1;

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
      this.geckoWebBrowser1 = new KingdomsBrowserGecko();
      this.SuspendLayout();
      this.geckoWebBrowser1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.geckoWebBrowser1.Location = new Point(0, 0);
      this.geckoWebBrowser1.MinimumSize = new Size(20, 20);
      this.geckoWebBrowser1.Name = "geckoWebBrowser1";
      this.geckoWebBrowser1.Size = new Size(413, 350);
      this.geckoWebBrowser1.TabIndex = 280;
      this.geckoWebBrowser1.ClientFeedback += new ClientFedbackEventHandler(this.geckoWebBrowser1_ClientFeedback);
      this.Controls.Add((Control) this.geckoWebBrowser1);
      this.Name = nameof (WebHelpPanel);
      this.Size = new Size(413, 350);
      this.ResumeLayout(false);
    }

    public WebHelpPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
    }

    public void recreateWebControl()
    {
    }

    public void openPage(string address) => this.geckoWebBrowser1.Navigate(new Uri(address));

    public void openUrl(string address)
    {
      this.recreateWebControl();
      if (string.IsNullOrEmpty(address) || address.Equals("about:blank"))
        return;
      if (!address.StartsWith("http://"))
      {
        if (!address.StartsWith("https://"))
          address = "http://" + address;
      }
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        IDictionary<string, string> postVars = (IDictionary<string, string>) new Dictionary<string, string>();
        postVars.Add(new KeyValuePair<string, string>("uid", RemoteServices.Instance.UserGuid.ToString("N")));
        postVars.Add(new KeyValuePair<string, string>("sid", RemoteServices.Instance.SessionGuid.ToString("N")));
        int num = -1;
        int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
        if (!GameEngine.Instance.World.isCapital(selectedMenuVillage))
          num = selectedMenuVillage;
        postVars.Add(new KeyValuePair<string, string>("CurrentvillageID", num.ToString()));
        postVars.Add(new KeyValuePair<string, string>("CurrentWorldID", GameEngine.Instance.World.GetGlobalWorldID().ToString()));
        this.geckoWebBrowser1.Navigate(new Uri(address), postVars);
        Cursor.Current = Cursors.Default;
      }
      catch (UriFormatException ex)
      {
      }
    }

    public void GoBack()
    {
    }

    public void GoForward()
    {
    }

    private void webBrowserHelp_CanGoBackChanged(object sender, EventArgs e)
    {
    }

    private void webBrowserHelp_CanGoForwardChanged(object sender, EventArgs e)
    {
    }

    private void webBrowserHelp_Navigating(object sender, WebBrowserNavigatingEventArgs e)
    {
    }

    private void webBrowserHelp_DocumentCompleted(
      object sender,
      WebBrowserDocumentCompletedEventArgs e)
    {
    }

    private void updateCurrentCardsCallback(UpdateCurrentCards_ReturnType returnData)
    {
    }

    private void geckoWebBrowser1_ClientFeedback(object sender, EventArgs e)
    {
      foreach (string key in (IEnumerable<string>) this.geckoWebBrowser1.PageValues.Keys)
      {
        if (key != "")
        {
          string pageValue = this.geckoWebBrowser1.PageValues[key];
          if (key.Trim().ToLowerInvariant() == "openlink")
            Process.Start("http://" + pageValue.Replace("%2F", "/"));
        }
      }
    }
  }
}
