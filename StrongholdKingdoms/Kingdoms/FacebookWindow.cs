// Decompiled with JetBrains decompiler
// Type: Kingdoms.FacebookWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  [ComVisible(true)]
  [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
  public class FacebookWindow : MyFormBase
  {
    private IContainer components;
    private ExtendedWebBrowser webBrowser1;
    private static FacebookWindow instance = (FacebookWindow) null;
    private static ProfileLoginWindow m_parent = (ProfileLoginWindow) null;
    public static bool vidLoaded = false;
    public static string futureURL = "";

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.webBrowser1 = new ExtendedWebBrowser();
      this.SuspendLayout();
      this.webBrowser1.AllowWebBrowserDrop = false;
      this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
      this.webBrowser1.Location = new Point(2, 32);
      this.webBrowser1.MinimumSize = new Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.ScriptErrorsSuppressed = true;
      this.webBrowser1.ScrollBarsEnabled = false;
      this.webBrowser1.Size = new Size(919, 585);
      this.webBrowser1.TabIndex = 13;
      this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Black;
      this.ClientSize = new Size(923, 619);
      this.Controls.Add((Control) this.webBrowser1);
      this.Name = nameof (FacebookWindow);
      this.ShowClose = true;
      this.Text = nameof (FacebookWindow);
      this.Load += new EventHandler(this.FacebookWindow_Load);
      this.Controls.SetChildIndex((Control) this.webBrowser1, 0);
      this.ResumeLayout(false);
    }

    public event FacebookEventHandler login;

    public FacebookWindow()
    {
      this.InitializeComponent();
      this.Text = this.Title = SK.Text("Facebook_LOGIN", "Facebook Login");
      this.webBrowser1.AllowWebBrowserDrop = false;
      this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
      this.webBrowser1.WebBrowserShortcutsEnabled = false;
      this.webBrowser1.ObjectForScripting = (object) this;
    }

    public void IframeLogin(object userguid, object token)
    {
      this.onlogin(new FacebookEventArgs((string) userguid, (string) token));
    }

    protected virtual void onlogin(FacebookEventArgs e)
    {
      this.login((object) this, e);
      this.FacebookLogin(e.userguid);
    }

    public static void ShowFacebookLogin(
      string url,
      string urlFirst,
      ProfileLoginWindow parent,
      FacebookEventHandler loginCallback)
    {
      if (FacebookWindow.instance != null)
      {
        try
        {
          FacebookWindow.instance.Close();
          FacebookWindow.instance = (FacebookWindow) null;
        }
        catch (Exception ex)
        {
        }
      }
      FacebookWindow.vidLoaded = false;
      FacebookWindow facebookWindow = new FacebookWindow();
      FacebookWindow.m_parent = parent;
      Point point = new Point(parent.Location.X + (parent.Width - facebookWindow.Width) / 2, parent.Location.Y + (parent.Height - facebookWindow.Height) / 2);
      facebookWindow.closeCallback = new MyFormBase.MFBClose(FacebookWindow.closing);
      facebookWindow.Location = point;
      facebookWindow.Show((IWin32Window) parent);
      FacebookWindow.instance = facebookWindow;
      if (loginCallback != null)
        FacebookWindow.instance.login += loginCallback;
      while (!FacebookWindow.vidLoaded)
      {
        Thread.Sleep(100);
        Application.DoEvents();
      }
      Thread.Sleep(500);
      if (urlFirst.Length > 0)
      {
        FacebookWindow.futureURL = url;
        url = urlFirst;
      }
      facebookWindow.webBrowser1.Navigate(url);
    }

    public static void ShowFacebookLogin(string url, string urlFirst, Form parent)
    {
      if (FacebookWindow.instance != null)
      {
        try
        {
          FacebookWindow.instance.Close();
          FacebookWindow.instance = (FacebookWindow) null;
        }
        catch (Exception ex)
        {
        }
      }
      FacebookWindow.vidLoaded = false;
      FacebookWindow facebookWindow = new FacebookWindow();
      FacebookWindow.m_parent = (ProfileLoginWindow) null;
      Point point = new Point(parent.Location.X + (parent.Width - facebookWindow.Width) / 2, parent.Location.Y + (parent.Height - facebookWindow.Height) / 2);
      facebookWindow.Location = point;
      facebookWindow.Show((IWin32Window) parent);
      FacebookWindow.instance = facebookWindow;
      while (!FacebookWindow.vidLoaded)
      {
        Thread.Sleep(100);
        Application.DoEvents();
      }
      Thread.Sleep(500);
      if (urlFirst.Length > 0)
      {
        FacebookWindow.futureURL = url;
        url = urlFirst;
      }
      facebookWindow.webBrowser1.Navigate(url);
    }

    public static void closing()
    {
      if (FacebookWindow.m_parent != null)
        FacebookWindow.m_parent.FacebookClose();
      FacebookWindow.instance = (FacebookWindow) null;
    }

    private void FacebookLogin(string userGuid)
    {
      if (FacebookWindow.m_parent != null)
        FacebookWindow.m_parent.FacebookLogin(userGuid, "");
      this.Close();
      FacebookWindow.instance = (FacebookWindow) null;
    }

    private void FacebookWindow_Load(object sender, EventArgs e) => FacebookWindow.vidLoaded = true;

    private void webBrowser1_DocumentCompleted(
      object sender,
      WebBrowserDocumentCompletedEventArgs e)
    {
      if (FacebookWindow.futureURL.Length <= 0)
        return;
      for (int index = 0; index < 50; ++index)
      {
        Thread.Sleep(100);
        Application.DoEvents();
      }
      string futureUrl = FacebookWindow.futureURL;
      FacebookWindow.futureURL = "";
      this.webBrowser1.Navigate(futureUrl);
    }
  }
}
