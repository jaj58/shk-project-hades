// Decompiled with JetBrains decompiler
// Type: Kingdoms.KingdomsBrowserGecko
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using Gecko;
using Gecko.Events;
using Gecko.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

//#nullable disable
namespace Kingdoms
{
  public class KingdomsBrowserGecko : GeckoWebBrowser
  {
    public List<object> imageObjects = new List<object>();
    public static string PostContentType = "Content-Type: application/x-www-form-urlencoded";
    public IDictionary<string, string> PageValues;

    public event ClientFedbackEventHandler ClientFeedback;

    protected virtual void OnClientFeedback(EventArgs e)
    {
      if (this.ClientFeedback == null)
        return;
      this.ClientFeedback((object) this, e);
    }

    public bool HasResponse => this.PageValues.Count > 0;

    protected override void OnStatusTextChanged(EventArgs e)
    {
      if (this.StatusText == "GiveClientFeedback")
      {
        this.GetPageValues();
        if (this.HasResponse)
          this.OnClientFeedback(e);
      }
      base.OnStatusTextChanged(e);
    }

    protected override void OnDocumentCompleted(GeckoDocumentCompletedEventArgs e)
    {
      this.GetPageValues();
      this.OnClientFeedback((EventArgs) e);
      base.OnDocumentCompleted(e);
      string query = this.Url.Query;
      if (!query.Contains("&url"))
        return;
      string[] strArray = query.Split(new string[1]
      {
        "&url="
      }, StringSplitOptions.None);
      new Process()
      {
        StartInfo = {
          FileName = strArray[1]
        }
      }.Start();
    }

    public void Navigate(Uri url) => this.Navigate(url.AbsoluteUri);

    public void Navigate(Uri url, IDictionary<string, string> postVars)
    {
      string str = string.Empty;
      foreach (KeyValuePair<string, string> postVar in (IEnumerable<KeyValuePair<string, string>>) postVars)
      {
        if (str != string.Empty)
          str += "&";
        str = str + postVar.Key + "=" + postVar.Value;
      }
      Encoding.UTF8.GetBytes(str);
      MimeInputStream postData = MimeInputStream.Create();
      postData.AddHeader("Content-Type", "application/x-www-form-urlencoded");
      postData.AddContentLength = true;
      postData.SetData(str);
      this.Navigate(url.AbsoluteUri, GeckoLoadFlags.None, (string) null, postData);
    }

    private void GetPageValues()
    {
      if (this.PageValues != null)
        this.PageValues.Clear();
      else
        this.PageValues = (IDictionary<string, string>) new Dictionary<string, string>();
      string[] strArray1 = this.Document.Cookie.Split(';');
      if (strArray1.Length <= 0)
        return;
      foreach (string str in strArray1)
      {
        if (str.Trim().StartsWith("KingdomsFeedback_", true, (CultureInfo) null))
        {
          string[] strArray2 = str.Split('=');
          if (strArray2.Length == 2)
            this.PageValues.Add(new KeyValuePair<string, string>(strArray2[0].Trim().Remove(0, "KingdomsFeedback_".Length), strArray2[1]));
        }
      }
    }
  }
}
