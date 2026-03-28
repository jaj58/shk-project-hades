// Decompiled with JetBrains decompiler
// Type: Kingdoms.QuestRewardPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Kingdoms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class QuestRewardPopup : MyFormBase
  {
    private IContainer components;
    private BitmapButton btnOK;
    private Label label1;
    private Label lblReward;
    private Label lblInfo;

    public QuestRewardPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(int quest)
    {
      this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
      this.label1.Text = SK.Text("QuestRewardPopup_Reward", "Reward") + " : ";
      this.Text = this.Title = SK.Text("QuestRewardPopup_Quest_Reward", "Quest Reward");
      bool flag1 = false;
      List<Quests.QuestReward> questRewards = Quests.getQuestRewards(quest, true, GameEngine.NFI);
      string str = "";
      bool flag2 = true;
      foreach (Quests.QuestReward questReward in questRewards)
      {
        if (!flag2)
          str += ", ";
        else
          flag2 = false;
        str += questReward.explanation;
        if (questReward.type == 20004 || questReward.type == 20006)
          flag1 = true;
      }
      this.lblReward.Text = str;
      int questsTutorialStage = Tutorials.getQuestsTutorialStage(quest);
      if (questsTutorialStage == -1)
        this.lblInfo.Text = "";
      else
        this.lblInfo.Text = Tutorials.getTutorialRewardText(questsTutorialStage);
      if (!flag1)
        return;
      PlayCardsWindow.resetRewardCardTimer();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("QuestRewardPopup_ok");
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.btnOK = new BitmapButton();
      this.label1 = new Label();
      this.lblReward = new Label();
      this.lblInfo = new Label();
      this.SuspendLayout();
      this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnOK.BorderColor = ARGBColors.DarkBlue;
      this.btnOK.BorderDrawing = true;
      this.btnOK.FocusRectangleEnabled = false;
      this.btnOK.Image = (Image) null;
      this.btnOK.ImageBorderColor = ARGBColors.Chocolate;
      this.btnOK.ImageBorderEnabled = true;
      this.btnOK.ImageDropShadow = true;
      this.btnOK.ImageFocused = (Image) null;
      this.btnOK.ImageInactive = (Image) null;
      this.btnOK.ImageMouseOver = (Image) null;
      this.btnOK.ImageNormal = (Image) null;
      this.btnOK.ImagePressed = (Image) null;
      this.btnOK.InnerBorderColor = ARGBColors.LightGray;
      this.btnOK.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnOK.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnOK.Location = new Point(293, 206);
      this.btnOK.Name = "btnOK";
      this.btnOK.OffsetPressedContent = true;
      this.btnOK.Padding2 = 5;
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.StretchImage = false;
      this.btnOK.TabIndex = 0;
      this.btnOK.Text = "OK";
      this.btnOK.TextDropShadow = false;
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.label1.AutoSize = true;
      this.label1.BackColor = ARGBColors.Transparent;
      this.label1.Location = new Point(12, 50);
      this.label1.Name = "label1";
      this.label1.Size = new Size(53, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Reward : ";
      this.lblReward.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.lblReward.BackColor = ARGBColors.Transparent;
      this.lblReward.Location = new Point(71, 50);
      this.lblReward.Name = "lblReward";
      this.lblReward.Size = new Size(297, 116);
      this.lblReward.TabIndex = 2;
      this.lblReward.Text = "...";
      this.lblInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.lblInfo.BackColor = ARGBColors.Transparent;
      this.lblInfo.Location = new Point(12, 116);
      this.lblInfo.Name = "lblInfo";
      this.lblInfo.Size = new Size(356, 83);
      this.lblInfo.TabIndex = 3;
      this.lblInfo.Text = "....";
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(380, 237);
      this.Controls.Add((Control) this.lblInfo);
      this.Controls.Add((Control) this.lblReward);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.btnOK);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (QuestRewardPopup);
      this.ShowClose = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Quest Reward";
      this.Controls.SetChildIndex((Control) this.btnOK, 0);
      this.Controls.SetChildIndex((Control) this.label1, 0);
      this.Controls.SetChildIndex((Control) this.lblReward, 0);
      this.Controls.SetChildIndex((Control) this.lblInfo, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
