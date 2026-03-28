// Decompiled with JetBrains decompiler
// Type: Kingdoms.SharedIPErrorPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class SharedIPErrorPopup : MyFormBase
  {
    private IContainer components;
    private Label lblExplanation;
    private LinkLabel linkLabelMoreInfo;
    private BitmapButton btnOK;

    public SharedIPErrorPopup()
    {
      this.InitializeComponent();
      this.lblExplanation.Font = FontManager.GetFont("Microsoft Sans Serif", 9f, FontStyle.Regular);
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(string explanation)
    {
      this.linkLabelMoreInfo.Text = SK.Text("SharedIPErrorPopup_More_Info", "Click Here for More Information");
      this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
      this.Text = this.Title = SK.Text("SharedIPErrorPopup_Shared_Connwectin", "Shared Connection Detected");
      this.lblExplanation.Text = explanation;
    }

    private void linkLabelMoreInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process process = new Process();
      switch (Program.mySettings.LanguageIdent)
      {
        case "de":
          process.StartInfo.FileName = "https://strongholdkingdoms-de.gamepedia.com/Gemeinsame_IP-Adressen";
          break;
        case "fr":
          process.StartInfo.FileName = "https://strongholdkingdoms-fr.gamepedia.com/Adresse_IP_Partag%C3%A9e";
          break;
        case "ru":
          process.StartInfo.FileName = "https://strongholdkingdoms-ru.gamepedia.com/%D0%A0%D0%B0%D0%B7%D0%B4%D0%B5%D0%BB%D0%B5%D0%BD%D0%B8%D0%B5_IP";
          break;
        case "es":
          process.StartInfo.FileName = "https://strongholdkingdoms-es.gamepedia.com/Compartir_IP";
          break;
        case "pl":
          process.StartInfo.FileName = "https://strongholdkingdoms-pl.gamepedia.com/Wsp%C3%B3%C5%82u%C5%BCytkowanie_adresu_IP";
          break;
        case "it":
          process.StartInfo.FileName = "https://strongholdkingdoms-it.gamepedia.com/A_proposito_di_Stronghold_Kingdoms";
          break;
        case "tr":
          process.StartInfo.FileName = "https://strongholdkingdoms-tr.gamepedia.com/IP_Payla%C5%9F%C4%B1m%C4%B1";
          break;
        case "pt":
          process.StartInfo.FileName = "https://strongholdkingdoms-pt.gamepedia.com/Compartilhamento_de_IP";
          break;
        default:
          process.StartInfo.FileName = "https://strongholdkingdoms.gamepedia.com/IP_Sharing";
          break;
      }
      process.Start();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("SharedIPErrorPopup_close");
      this.Close();
    }

    public static void showSharedIPPopup(string explanation)
    {
      SharedIPErrorPopup sharedIpErrorPopup = new SharedIPErrorPopup();
      sharedIpErrorPopup.init(explanation);
      int num = (int) sharedIpErrorPopup.ShowDialog();
      sharedIpErrorPopup.Dispose();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lblExplanation = new Label();
      this.linkLabelMoreInfo = new LinkLabel();
      this.btnOK = new BitmapButton();
      this.SuspendLayout();
      this.lblExplanation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.lblExplanation.BackColor = ARGBColors.Transparent;
      this.lblExplanation.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblExplanation.Location = new Point(8, 39);
      this.lblExplanation.Name = "lblExplanation";
      this.lblExplanation.Size = new Size(345, 76);
      this.lblExplanation.TabIndex = 0;
      this.lblExplanation.Text = "label1";
      this.linkLabelMoreInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.linkLabelMoreInfo.BackColor = ARGBColors.Transparent;
      this.linkLabelMoreInfo.Location = new Point(12, 134);
      this.linkLabelMoreInfo.Name = "linkLabelMoreInfo";
      this.linkLabelMoreInfo.Size = new Size(189, 13);
      this.linkLabelMoreInfo.TabIndex = 3;
      this.linkLabelMoreInfo.TabStop = true;
      this.linkLabelMoreInfo.Text = "Click Here for More Information";
      this.linkLabelMoreInfo.TextAlign = ContentAlignment.MiddleLeft;
      this.linkLabelMoreInfo.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabelMoreInfo_LinkClicked);
      this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnOK.BackColor = Color.FromArgb(203, 215, 223);
      this.btnOK.Location = new Point(272, 126);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.TabIndex = 4;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = false;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(361, 153);
      this.Controls.Add((Control) this.btnOK);
      this.Controls.Add((Control) this.linkLabelMoreInfo);
      this.Controls.Add((Control) this.lblExplanation);
      this.Name = nameof (SharedIPErrorPopup);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Shared Connection Detected";
      this.Controls.SetChildIndex((Control) this.lblExplanation, 0);
      this.Controls.SetChildIndex((Control) this.linkLabelMoreInfo, 0);
      this.Controls.SetChildIndex((Control) this.btnOK, 0);
      this.ResumeLayout(false);
    }
  }
}
