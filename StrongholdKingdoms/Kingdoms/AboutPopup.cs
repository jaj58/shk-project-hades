// Decompiled with JetBrains decompiler
// Type: Kingdoms.AboutPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Kingdoms.Properties;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class AboutPopup : MyFormBase
  {
    private IContainer components;
    private Label label1;
    private Label label2;
    private LinkLabel linkLabel1;
    private Label lblVersionNumber;
    private LinkLabel lblCredits;
    private Label lblSlimDX;
    private Label lblGeckFX;
    private Label lblXMLRPC;
    private Label lblZLIB;
    private Label label4;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      int num1 = 0;
      int num2 = 0;
      this.label1 = new Label();
      this.label2 = new Label();
      this.linkLabel1 = new LinkLabel();
      this.lblVersionNumber = new Label();
      this.lblCredits = new LinkLabel();
      this.lblSlimDX = new Label();
      this.lblGeckFX = new Label();
      this.lblXMLRPC = new Label();
      this.lblZLIB = new Label();
      this.label4 = new Label();
      this.SuspendLayout();
      this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.label1.BackColor = ARGBColors.Transparent;
      this.label1.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(14 - num2, 50);
      this.label1.Name = "label1";
      this.label1.Size = new Size(337 + num1, 23);
      this.label1.TabIndex = 0;
      this.label1.Text = "Stronghold Kingdoms";
      this.label1.TextAlign = ContentAlignment.TopCenter;
      this.label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.label2.BackColor = ARGBColors.Transparent;
      this.label2.Location = new Point(14 - num2, 83);
      this.label2.Name = "label2";
      this.label2.Size = new Size(337 + num1, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "(c)2010 Firefly Studios Ltd";
      this.label2.TextAlign = ContentAlignment.TopCenter;
      this.linkLabel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.linkLabel1.BackColor = ARGBColors.Transparent;
      this.linkLabel1.Location = new Point(12, 282);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(337 + num1, 13);
      this.linkLabel1.TabIndex = 2;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "www.strongholdkingdoms.com";
      this.linkLabel1.TextAlign = ContentAlignment.TopCenter;
      this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      this.lblVersionNumber.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.lblVersionNumber.BackColor = ARGBColors.Transparent;
      this.lblVersionNumber.Location = new Point(13 - num2, 313);
      this.lblVersionNumber.Name = "lblVersionNumber";
      this.lblVersionNumber.Size = new Size(337 + num1, 13);
      this.lblVersionNumber.TabIndex = 6;
      this.lblVersionNumber.Text = "1.1.1.1";
      this.lblVersionNumber.TextAlign = ContentAlignment.TopRight;
      this.lblCredits.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.lblCredits.BackColor = ARGBColors.Transparent;
      this.lblCredits.Location = new Point(12, 251);
      this.lblCredits.Name = "lblCredits";
      this.lblCredits.Size = new Size(337 + num1, 13);
      this.lblCredits.TabIndex = 12;
      this.lblCredits.TabStop = true;
      this.lblCredits.Text = "Credits";
      this.lblCredits.TextAlign = ContentAlignment.TopCenter;
      this.lblCredits.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lblCredits_LinkClicked);
      this.lblSlimDX.AutoSize = true;
      this.lblSlimDX.BackColor = ARGBColors.Transparent;
      this.lblSlimDX.Location = new Point(67 - num2, 118);
      this.lblSlimDX.Name = "lblSlimDX";
      this.lblSlimDX.Size = new Size(226 + num1, 13);
      this.lblSlimDX.TabIndex = 13;
      this.lblSlimDX.Text = "SlimDX Copyright (c) 2007-2010 SlimDX Group";
      this.lblGeckFX.AutoSize = true;
      this.lblGeckFX.BackColor = ARGBColors.Transparent;
      this.lblGeckFX.Location = new Point(63 - num2, 144);
      this.lblGeckFX.Name = "lblGeckFX";
      this.lblGeckFX.Size = new Size(234 + num1, 13);
      this.lblGeckFX.TabIndex = 14;
      this.lblGeckFX.Text = "GeckoFX Copyright © 2008 Skybound Software";
      this.lblXMLRPC.AutoSize = true;
      this.lblXMLRPC.BackColor = ARGBColors.Transparent;
      this.lblXMLRPC.Location = new Point(63 - num2, 170);
      this.lblXMLRPC.Name = "lblXMLRPC";
      this.lblXMLRPC.Size = new Size(234 + num1, 13);
      this.lblXMLRPC.TabIndex = 15;
      this.lblXMLRPC.Text = "XML-RPC.NET Copyright (c) 2006 Charles Cook";
      this.lblZLIB.AutoSize = true;
      this.lblZLIB.BackColor = ARGBColors.Transparent;
      this.lblZLIB.Location = new Point(35 - num2, 196);
      this.lblZLIB.Name = "lblZLIB";
      this.lblZLIB.Size = new Size(291 + num1, 13);
      this.lblZLIB.TabIndex = 16;
      this.lblZLIB.Text = "zlib Copyright (C) 1995-2004 Jean-loup Gailly and Mark Adler";
      this.label4.AutoSize = true;
      this.label4.BackColor = ARGBColors.Transparent;
      this.label4.Location = new Point(74 - num2, 222);
      this.label4.Name = "label4";
      this.label4.Size = new Size(212 + num1, 13);
      this.label4.TabIndex = 17;
      this.label4.Text = "NAudio Copyright (C) 2007 Ray Molenkamp";
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(361 * InterfaceMgr.UIScale, 335 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.lblZLIB);
      this.Controls.Add((Control) this.lblXMLRPC);
      this.Controls.Add((Control) this.lblGeckFX);
      this.Controls.Add((Control) this.lblSlimDX);
      this.Controls.Add((Control) this.lblCredits);
      this.Controls.Add((Control) this.lblVersionNumber);
      this.Controls.Add((Control) this.linkLabel1);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Icon = Resources.shk_icon;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (AboutPopup);
      this.ShowClose = true;
      this.ShowIcon = false;
      this.Text = "About Stronghold Kingdoms";
      this.Controls.SetChildIndex((Control) this.label1, 0);
      this.Controls.SetChildIndex((Control) this.label2, 0);
      this.Controls.SetChildIndex((Control) this.linkLabel1, 0);
      this.Controls.SetChildIndex((Control) this.lblVersionNumber, 0);
      this.Controls.SetChildIndex((Control) this.lblCredits, 0);
      this.Controls.SetChildIndex((Control) this.lblSlimDX, 0);
      this.Controls.SetChildIndex((Control) this.lblGeckFX, 0);
      this.Controls.SetChildIndex((Control) this.lblXMLRPC, 0);
      this.Controls.SetChildIndex((Control) this.lblZLIB, 0);
      this.Controls.SetChildIndex((Control) this.label4, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public AboutPopup()
    {
      this.InitializeComponent();
      this.label1.Font = FontManager.GetFont("Microsoft Sans Serif", 14f, FontStyle.Regular);
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.ShowClose = true;
    }

    public void init()
    {
      this.label1.Text = SK.Text("About_Stronghold_Kingdoms", "Stronghold Kingdoms");
      this.label2.Text = SK.Text("About_Firefly", "(c)2010 Firefly Studios Ltd");
      this.lblCredits.Text = SK.Text("About_Credits", "Credits");
      this.Text = this.Title = SK.Text("About_About_Stronghold_Kingdoms", "About Stronghold Kingdoms");
      this.lblSlimDX.Text = SK.Text("About_Copyright_SlimDX", "SlimDX Copyright (c) 2007-2010 SlimDX Group");
      this.lblGeckFX.Text = SK.Text("About_Copyright_GeckoFX", "GeckoFX Copyright (c) 2008 Skybound Software");
      this.lblXMLRPC.Text = SK.Text("About_Copyright_XMLRPC", "XML-RPC.NET Copyright (c) 2006 Charles Cook");
      this.lblZLIB.Text = SK.Text("About_Copyright_XLIB", "zlib Copyright (C) 1995-2004 Jean-loup Gailly and Mark Adler");
      string[] strArray = Regex.Split(Application.StartupPath, "\\\\");
      if (strArray.Length > 0)
      {
        this.lblVersionNumber.Visible = true;
        this.lblVersionNumber.Text = SK.Text("About_Version", "Version") + " : " + strArray[strArray.Length - 1];
      }
      if (Program.mySettings.LanguageIdent == "de")
        this.linkLabel1.Text = "de.strongholdkingdoms.com";
      else if (Program.mySettings.LanguageIdent == "fr")
        this.linkLabel1.Text = "fr.strongholdkingdoms.com";
      else if (Program.mySettings.LanguageIdent == "ru")
        this.linkLabel1.Text = "ru.strongholdkingdoms.com";
      else if (Program.mySettings.LanguageIdent == "es")
        this.linkLabel1.Text = "es.strongholdkingdoms.com";
      else if (Program.mySettings.LanguageIdent == "pl")
        this.linkLabel1.Text = "pl.strongholdkingdoms.com";
      else if (Program.mySettings.LanguageIdent == "tr")
        this.linkLabel1.Text = "tr.strongholdkingdoms.com";
      else if (Program.mySettings.LanguageIdent == "it")
        this.linkLabel1.Text = "it.strongholdkingdoms.com";
      else if (Program.mySettings.LanguageIdent == "pt")
        this.linkLabel1.Text = "pt.strongholdkingdoms.com";
      else
        this.linkLabel1.Text = "www.strongholdkingdoms.com";
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      string str = !(Program.mySettings.LanguageIdent == "de") ? (!(Program.mySettings.LanguageIdent == "fr") ? (!(Program.mySettings.LanguageIdent == "ru") ? (!(Program.mySettings.LanguageIdent == "es") ? (!(Program.mySettings.LanguageIdent == "pl") ? (!(Program.mySettings.LanguageIdent == "tr") ? (!(Program.mySettings.LanguageIdent == "it") ? (!(Program.mySettings.LanguageIdent == "pt") ? "https://www.strongholdkingdoms.com" : "https://pt.strongholdkingdoms.com") : "https://it.strongholdkingdoms.com") : "https://tr.strongholdkingdoms.com") : "https://pl.strongholdkingdoms.com") : "https://es.strongholdkingdoms.com") : "https://ru.strongholdkingdoms.com") : "https://fr.strongholdkingdoms.com") : "https://de.strongholdkingdoms.com";
      new Process() { StartInfo = { FileName = str } }.Start();
    }

    private void lblCredits_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      string str = !(Program.mySettings.LanguageIdent == "de") ? (!(Program.mySettings.LanguageIdent == "fr") ? (!(Program.mySettings.LanguageIdent == "ru") ? (!(Program.mySettings.LanguageIdent == "es") ? (!(Program.mySettings.LanguageIdent == "pl") ? (!(Program.mySettings.LanguageIdent == "tr") ? (!(Program.mySettings.LanguageIdent == "it") ? (!(Program.mySettings.LanguageIdent == "pt") ? "https://strongholdkingdoms.gamepedia.com/About_Stronghold_Kingdoms#Credits" : "https://strongholdkingdoms-pt.gamepedia.com/Sobre_o_Stronghold_Kingdoms") : "https://strongholdkingdoms-it.gamepedia.com/Condivisione_dell%E2%80%99IP") : "https://strongholdkingdoms-tr.gamepedia.com/Stronghold_Kingdoms_Hakk%C4%B1nda") : "https://strongholdkingdoms-pl.gamepedia.com/O_grze_Stronghold_Kingdoms") : "https://strongholdkingdoms-es.gamepedia.com/Acerca_de_Stronghold_Kingdoms#credits") : "https://strongholdkingdoms-ru.gamepedia.com/%D0%9E%D0%B1_%D0%B8%D0%B3%D1%80%D0%B5_Stronghold_Kingdoms#Credits") : "https://strongholdkingdoms-fr.gamepedia.com/%C3%80_propos_de_Stronghold_Kingdoms#Credits") : "https://strongholdkingdoms-de.gamepedia.com/%C3%9Cber_Stronghold_Kingdoms#Mitwirkende";
      new Process() { StartInfo = { FileName = str } }.Start();
    }
  }
}
