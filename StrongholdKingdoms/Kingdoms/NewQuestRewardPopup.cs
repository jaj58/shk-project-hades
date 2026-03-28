// Decompiled with JetBrains decompiler
// Type: Kingdoms.NewQuestRewardPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class NewQuestRewardPopup : Form
  {
    private IContainer components;
    private NewQuestRewardPanel newQuestRewardPanel;

    public NewQuestRewardPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(int questID, int villageID, NewQuestsPanel parent)
    {
      this.newQuestRewardPanel.Visible = true;
      this.newQuestRewardPanel.init(questID, villageID, parent, this);
    }

    public void update() => this.newQuestRewardPanel.update();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.newQuestRewardPanel = new NewQuestRewardPanel();
      this.SuspendLayout();
      this.newQuestRewardPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.newQuestRewardPanel.BackColor = ARGBColors.Fuchsia;
      this.newQuestRewardPanel.ClickThru = false;
      this.newQuestRewardPanel.Location = new Point(0, 0);
      this.newQuestRewardPanel.Name = "newQuestRewardPanel";
      this.newQuestRewardPanel.PanelActive = true;
      this.newQuestRewardPanel.Size = new Size(500, 472);
      this.newQuestRewardPanel.StoredGraphics = (Graphics) null;
      this.newQuestRewardPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.White;
      this.ClientSize = new Size(500, 472);
      this.ControlBox = false;
      this.Controls.Add((Control) this.newQuestRewardPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (NewQuestRewardPopup);
      this.Opacity = 0.95;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Report Capture";
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.ResumeLayout(false);
    }
  }
}
