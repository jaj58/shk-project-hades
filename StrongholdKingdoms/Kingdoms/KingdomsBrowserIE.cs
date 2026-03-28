// Decompiled with JetBrains decompiler
// Type: Kingdoms.KingdomsBrowserIE
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class KingdomsBrowserIE : WebBrowser
  {
    public static string PostContentType = "Content-Type: application/x-www-form-urlencoded";
    public IDictionary<string, string> PageValues;

    public bool HasResponse => this.PageValues.Count > 0;

    protected override void OnNavigated(WebBrowserNavigatedEventArgs e)
    {
      this.GetPageValues();
      base.OnNavigated(e);
    }

    public void Navigate(Uri url, IDictionary<string, string> postVars)
    {
      string s = string.Empty;
      foreach (KeyValuePair<string, string> postVar in (IEnumerable<KeyValuePair<string, string>>) postVars)
      {
        if (s != string.Empty)
          s += "&";
        s = s + postVar.Key + "=" + postVar.Value;
      }
      byte[] bytes = Encoding.UTF8.GetBytes(s);
      this.Navigate(url, string.Empty, bytes, KingdomsBrowserIE.PostContentType + Environment.NewLine);
    }

    private void GetPageValues()
    {
      this.PageValues = (IDictionary<string, string>) new Dictionary<string, string>();
      HtmlElement elementById = this.Document.GetElementById("ClientFeedbackDIV");
      if (!(elementById != (HtmlElement) null))
        return;
      foreach (HtmlElement htmlElement in elementById.GetElementsByTagName("input"))
        this.PageValues.Add(new KeyValuePair<string, string>(htmlElement.Id, htmlElement.GetAttribute("value")));
    }

    public string GetPageValueByID(string id)
    {
      return this.Document.GetElementById(id).GetAttribute("value");
    }
  }
}
