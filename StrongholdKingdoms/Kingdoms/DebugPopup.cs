// Decompiled with JetBrains decompiler
// Type: Kingdoms.DebugPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CompressedSink;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class DebugPopup : Form
  {
    public int lastNumLines;
    private IContainer components;
    private Label label1;
    private Label label2;
    private Label lblNetworkDataSent;
    private Label lblNetworkDataReceived;
    private Label label3;
    private Label lblAverageRTT;
    private Label lblAverageLongRTT;
    private Label label5;
    private Label lblTimeouts;
    private Label label7;
    private Label lblAverageShortRTT;
    private Label label9;
    private Label lblShortCount;
    private Label lblLongCount;
    private TextBox tbDetailedLogging;

    public DebugPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void run()
    {
      string str1 = CompressedClientSink.realDataSent.ToString() + " (" + CompressedClientSink.rawDataSent.ToString() + ") Pkts: " + CompressedClientSink.packetsSent.ToString();
      string str2 = CompressedClientSink.rawDataReceived.ToString() + " (" + (CompressedClientSink.realDataReceived - CompressedClientSink.midDataReceived).ToString() + "/" + CompressedClientSink.realDataReceived.ToString() + ") Pkts: " + CompressedClientSink.packetsReceived.ToString();
      this.lblNetworkDataSent.Text = str1;
      this.lblNetworkDataReceived.Text = str2;
      this.lblAverageRTT.Text = ((int) RemoteServices.Instance.RTTAverageTime).ToString();
      this.lblAverageShortRTT.Text = ((int) RemoteServices.Instance.RTTAverageShortTime).ToString();
      this.lblAverageLongRTT.Text = ((int) RemoteServices.Instance.RTTAverageLongTime).ToString();
      this.lblTimeouts.Text = RemoteServices.Instance.RTTTimeOuts.ToString();
      this.lblShortCount.Text = RemoteServices.Instance.RTTAverageShortCount.ToString();
      this.lblLongCount.Text = RemoteServices.Instance.RTTAverageLongCount.ToString();
      List<RemoteServices.RTT_Log_data> detailedLogging = RemoteServices.Instance.getDetailedLogging();
      if (this.lastNumLines == detailedLogging.Count)
        return;
      bool flag = true;
      StringBuilder stringBuilder = new StringBuilder();
      try
      {
        foreach (RemoteServices.RTT_Log_data rttLogData in detailedLogging)
        {
          string str3 = rttLogData.packetType.Name + "               ";
          string str4 = rttLogData.time >= 0 ? str3 + rttLogData.time.ToString() : str3 + "TimeOut";
          stringBuilder.AppendLine(str4);
        }
      }
      catch (Exception ex)
      {
        flag = false;
      }
      if (!flag)
        return;
      this.tbDetailedLogging.Text = stringBuilder.ToString();
      this.lastNumLines = detailedLogging.Count;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.label2 = new Label();
      this.lblNetworkDataSent = new Label();
      this.lblNetworkDataReceived = new Label();
      this.label3 = new Label();
      this.lblAverageRTT = new Label();
      this.lblAverageLongRTT = new Label();
      this.label5 = new Label();
      this.lblTimeouts = new Label();
      this.label7 = new Label();
      this.lblAverageShortRTT = new Label();
      this.label9 = new Label();
      this.lblShortCount = new Label();
      this.lblLongCount = new Label();
      this.tbDetailedLogging = new TextBox();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(12, 26);
      this.label1.Name = "label1";
      this.label1.Size = new Size(98, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Network Data Sent";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(12, 51);
      this.label2.Name = "label2";
      this.label2.Size = new Size(122, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Network Data Received";
      this.lblNetworkDataSent.AutoSize = true;
      this.lblNetworkDataSent.Location = new Point(139, 26);
      this.lblNetworkDataSent.Name = "lblNetworkDataSent";
      this.lblNetworkDataSent.Size = new Size(13, 13);
      this.lblNetworkDataSent.TabIndex = 2;
      this.lblNetworkDataSent.Text = "0";
      this.lblNetworkDataReceived.AutoSize = true;
      this.lblNetworkDataReceived.Location = new Point(139, 51);
      this.lblNetworkDataReceived.Name = "lblNetworkDataReceived";
      this.lblNetworkDataReceived.Size = new Size(13, 13);
      this.lblNetworkDataReceived.TabIndex = 3;
      this.lblNetworkDataReceived.Text = "0";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(12, 76);
      this.label3.Name = "label3";
      this.label3.Size = new Size(72, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Average RTT";
      this.lblAverageRTT.AutoSize = true;
      this.lblAverageRTT.Location = new Point(139, 76);
      this.lblAverageRTT.Name = "lblAverageRTT";
      this.lblAverageRTT.Size = new Size(13, 13);
      this.lblAverageRTT.TabIndex = 5;
      this.lblAverageRTT.Text = "0";
      this.lblAverageLongRTT.AutoSize = true;
      this.lblAverageLongRTT.Location = new Point(139, (int) sbyte.MaxValue);
      this.lblAverageLongRTT.Name = "lblAverageLongRTT";
      this.lblAverageLongRTT.Size = new Size(13, 13);
      this.lblAverageLongRTT.TabIndex = 7;
      this.lblAverageLongRTT.Text = "0";
      this.label5.AutoSize = true;
      this.label5.Location = new Point(12, (int) sbyte.MaxValue);
      this.label5.Name = "label5";
      this.label5.Size = new Size(99, 13);
      this.label5.TabIndex = 6;
      this.label5.Text = "Average Long RTT";
      this.lblTimeouts.AutoSize = true;
      this.lblTimeouts.Location = new Point(139, 155);
      this.lblTimeouts.Name = "lblTimeouts";
      this.lblTimeouts.Size = new Size(13, 13);
      this.lblTimeouts.TabIndex = 9;
      this.lblTimeouts.Text = "0";
      this.label7.AutoSize = true;
      this.label7.Location = new Point(12, 155);
      this.label7.Name = "label7";
      this.label7.Size = new Size(50, 13);
      this.label7.TabIndex = 8;
      this.label7.Text = "Timeouts";
      this.lblAverageShortRTT.AutoSize = true;
      this.lblAverageShortRTT.Location = new Point(139, 102);
      this.lblAverageShortRTT.Name = "lblAverageShortRTT";
      this.lblAverageShortRTT.Size = new Size(13, 13);
      this.lblAverageShortRTT.TabIndex = 11;
      this.lblAverageShortRTT.Text = "0";
      this.label9.AutoSize = true;
      this.label9.Location = new Point(12, 102);
      this.label9.Name = "label9";
      this.label9.Size = new Size(100, 13);
      this.label9.TabIndex = 10;
      this.label9.Text = "Average Short RTT";
      this.lblShortCount.AutoSize = true;
      this.lblShortCount.Location = new Point(242, 102);
      this.lblShortCount.Name = "lblShortCount";
      this.lblShortCount.Size = new Size(13, 13);
      this.lblShortCount.TabIndex = 13;
      this.lblShortCount.Text = "0";
      this.lblLongCount.AutoSize = true;
      this.lblLongCount.Location = new Point(242, (int) sbyte.MaxValue);
      this.lblLongCount.Name = "lblLongCount";
      this.lblLongCount.Size = new Size(13, 13);
      this.lblLongCount.TabIndex = 12;
      this.lblLongCount.Text = "0";
      this.tbDetailedLogging.Location = new Point(12, 184);
      this.tbDetailedLogging.Multiline = true;
      this.tbDetailedLogging.Name = "tbDetailedLogging";
      this.tbDetailedLogging.ReadOnly = true;
      this.tbDetailedLogging.ScrollBars = ScrollBars.Vertical;
      this.tbDetailedLogging.Size = new Size(295, 136);
      this.tbDetailedLogging.TabIndex = 14;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(319, 332);
      this.Controls.Add((Control) this.tbDetailedLogging);
      this.Controls.Add((Control) this.lblShortCount);
      this.Controls.Add((Control) this.lblLongCount);
      this.Controls.Add((Control) this.lblAverageShortRTT);
      this.Controls.Add((Control) this.label9);
      this.Controls.Add((Control) this.lblTimeouts);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.lblAverageLongRTT);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.lblAverageRTT);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.lblNetworkDataReceived);
      this.Controls.Add((Control) this.lblNetworkDataSent);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Name = nameof (DebugPopup);
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Debug";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
