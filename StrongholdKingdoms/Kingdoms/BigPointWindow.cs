// Decompiled with JetBrains decompiler
// Type: Kingdoms.BigPointWindow
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
  public class BigPointWindow : MyFormBase
  {
    private static BigPointWindow instance = (BigPointWindow) null;
    private static ProfileLoginWindow m_parent = (ProfileLoginWindow) null;
    public static bool vidLoaded = false;
    public static string futureURL = "";
    private IContainer components;
    private WebBrowser webBrowser1;
    private BPForgottenPasswordPanel panel5;

    public event BigPointEventHandler login;

    public BigPointWindow()
    {
      this.InitializeComponent();
      this.Text = this.Title = SK.Text("BIGPOINT_LOGIN", "Bigpoint Login");
      this.webBrowser1.AllowWebBrowserDrop = false;
      this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
      this.webBrowser1.WebBrowserShortcutsEnabled = false;
      this.webBrowser1.ObjectForScripting = (object) this;
    }

    public void IframeLogin(object userguid, object token)
    {
      this.onlogin(new BigPointEventArgs((string) userguid, (string) token));
    }

    protected virtual void onlogin(BigPointEventArgs e)
    {
      this.login((object) this, e);
      this.bigpointLogin(e.userguid);
    }

    public static void ShowBigPointLogin(
      string url,
      string urlFirst,
      ProfileLoginWindow parent,
      BigPointEventHandler loginCallback)
    {
      if (BigPointWindow.instance != null)
      {
        try
        {
          BigPointWindow.instance.Close();
          BigPointWindow.instance = (BigPointWindow) null;
        }
        catch (Exception ex)
        {
        }
      }
      BigPointWindow.vidLoaded = false;
      BigPointWindow bigPointWindow = new BigPointWindow();
      BigPointWindow.m_parent = parent;
      Point point = new Point(parent.Location.X + (parent.Width - bigPointWindow.Width) / 2, parent.Location.Y + (parent.Height - bigPointWindow.Height) / 2);
      bigPointWindow.closeCallback = new MyFormBase.MFBClose(BigPointWindow.closing);
      bigPointWindow.Location = point;
      bigPointWindow.Show((IWin32Window) parent);
      BigPointWindow.instance = bigPointWindow;
      BigPointWindow.instance.login += loginCallback;
      while (!BigPointWindow.vidLoaded)
      {
        Thread.Sleep(100);
        Application.DoEvents();
      }
      Thread.Sleep(500);
      if (urlFirst.Length > 0)
      {
        BigPointWindow.futureURL = url;
        url = urlFirst;
      }
      bigPointWindow.webBrowser1.Navigate(url);
    }

    public static void closing()
    {
      if (BigPointWindow.m_parent != null)
        BigPointWindow.m_parent.bigpointClose();
      BigPointWindow.instance = (BigPointWindow) null;
    }

    private void bigpointLogin(string userGuid)
    {
      if (BigPointWindow.m_parent != null)
        BigPointWindow.m_parent.bigpointLogin(userGuid, "");
      this.Close();
      BigPointWindow.instance = (BigPointWindow) null;
    }

    private void BigPointWindow_Load(object sender, EventArgs e) => BigPointWindow.vidLoaded = true;

    private void webBrowser1_DocumentCompleted(
      object sender,
      WebBrowserDocumentCompletedEventArgs e)
    {
      if (BigPointWindow.futureURL.Length <= 0)
        return;
      for (int index = 0; index < 50; ++index)
      {
        Thread.Sleep(100);
        Application.DoEvents();
      }
      string futureUrl = BigPointWindow.futureURL;
      BigPointWindow.futureURL = "";
      this.webBrowser1.Navigate(futureUrl);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.webBrowser1 = new WebBrowser();
      this.panel5 = new BPForgottenPasswordPanel();
      this.SuspendLayout();
      this.webBrowser1.AllowWebBrowserDrop = false;
      this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
      this.webBrowser1.Location = new Point(2, 32);
      this.webBrowser1.MinimumSize = new Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.ScriptErrorsSuppressed = true;
      this.webBrowser1.ScrollBarsEnabled = false;
      this.webBrowser1.Size = new Size(519, 525);
      this.webBrowser1.TabIndex = 13;
      this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
      this.panel5.BackColor = Color.White;
      this.panel5.Location = new Point(2, 556);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(519, 35);
      this.panel5.TabIndex = 14;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(0, 0, 0);
      this.ClientSize = new Size(523, 593);
      this.Controls.Add((Control) this.panel5);
      this.Controls.Add((Control) this.webBrowser1);
      this.Name = nameof (BigPointWindow);
      this.ShowBar = true;
      this.ShowClose = true;
      this.Text = nameof (BigPointWindow);
      this.Load += new EventHandler(this.BigPointWindow_Load);
      this.Controls.SetChildIndex((Control) this.webBrowser1, 0);
      this.Controls.SetChildIndex((Control) this.panel5, 0);
      this.ResumeLayout(false);
    }
  }
}
