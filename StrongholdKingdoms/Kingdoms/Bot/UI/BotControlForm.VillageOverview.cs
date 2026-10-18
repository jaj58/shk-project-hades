using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kingdoms.Bot.UI
{
    // The bot window's way into the Village Overview. The main way in is the button beside
    // the premium overview on the game's top menu bar, but that one only fits when the game
    // window is wide enough - this footer button always works.
    public partial class BotControlForm
    {
        private Button _villageOverviewBtn;

        private void WireUpVillageOverviewButton()
        {
            _villageOverviewBtn = new Button();
            _villageOverviewBtn.BackColor = Color.FromArgb(40, 42, 54);
            _villageOverviewBtn.Cursor = Cursors.Hand;
            _villageOverviewBtn.FlatAppearance.BorderSize = 0;
            _villageOverviewBtn.FlatStyle = FlatStyle.Flat;
            _villageOverviewBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _villageOverviewBtn.ForeColor = Color.White;
            // Just right of Clear Log (248 + 90).
            _villageOverviewBtn.Location = new Point(346, 7);
            _villageOverviewBtn.Size = new Size(140, 30);
            _villageOverviewBtn.Text = "Village Overview";
            _villageOverviewBtn.UseVisualStyleBackColor = false;
            _villageOverviewBtn.Click += delegate { VillageOverviewForm.ShowInstance(); };
            _footerPanel.Controls.Add(_villageOverviewBtn);
        }
    }
}
