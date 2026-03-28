// Decompiled with JetBrains decompiler
// Type: Kingdoms.NewQuestsCompletedWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using Kingdoms.Properties;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class NewQuestsCompletedWindow : Form
  {
    private IContainer components;
    private NewQuestsCompletedPanel newQuestsCompletedPanel;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.newQuestsCompletedPanel = new NewQuestsCompletedPanel();
      this.SuspendLayout();
      this.newQuestsCompletedPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.newQuestsCompletedPanel.ClickThru = false;
      this.newQuestsCompletedPanel.Location = new Point(0, 0);
      this.newQuestsCompletedPanel.Name = "newQuestsCompletedPanel";
      this.newQuestsCompletedPanel.PanelActive = true;
      this.newQuestsCompletedPanel.Size = new Size(489, 350);
      this.newQuestsCompletedPanel.StoredGraphics = (Graphics) null;
      this.newQuestsCompletedPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(489, 350);
      this.Controls.Add((Control) this.newQuestsCompletedPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.MaximumSize = new Size(700, 443);
      this.MinimumSize = new Size(475, 350);
      this.Name = nameof (NewQuestsCompletedWindow);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.ResumeLayout(false);
    }

    public NewQuestsCompletedWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(
      Form parent,
      List<int> quests,
      bool forQuestList,
      string questTag,
      int questID)
    {
      if (parent != null)
        this.Location = new Point(parent.Location.X + (parent.Width - this.Width) / 2, parent.Location.Y + (parent.Height - this.Height) / 2);
      this.newQuestsCompletedPanel.setCompletedQuests(quests);
      this.newQuestsCompletedPanel.init((Form) this, forQuestList, questTag, questID);
    }
  }
}
