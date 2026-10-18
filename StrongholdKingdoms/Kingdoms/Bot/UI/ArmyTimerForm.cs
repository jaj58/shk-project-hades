using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Kingdoms.Bot.UI
{
    /// <summary>
    /// Army Timer - a small always-on-top countdown popped out of the army side panel
    /// (SelectArmyPanel2) for one army's current leg: until it hits the target, or until
    /// it gets home when it is returning.
    ///
    /// Every window runs on its own UI thread with its own message loop, so it keeps
    /// counting while the game's main loop is stalled (a long DoEvents burst, the game
    /// window being dragged, a hitch). That means it must never touch game state: the
    /// caller snapshots everything on the game thread and the deadline is held on the
    /// Stopwatch clock, which is monotonic and safe to read from any thread.
    /// </summary>
    internal class ArmyTimerForm : Form
    {
        private static readonly Color FormBg = Color.FromArgb(28, 30, 38);
        private static readonly Color AlarmBg = Color.FromArgb(190, 30, 30);
        private static readonly Color BorderColor = Color.FromArgb(70, 74, 92);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);

        private const int FormWidth = 224;
        private const int FormHeight = 66;
        private const int CascadeStep = 28;     // vertical offset between timers opened together
        private const int CascadeSlots = 6;

        private const int WM_NCLBUTTONDOWN = 161;
        private const int HT_CAPTION = 2;
        private const int WS_EX_TOPMOST = 0x8;
        private const uint SWP_NOSIZE = 0x1;
        private const uint SWP_NOMOVE = 0x2;
        private const uint SWP_NOACTIVATE = 0x10;
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint flags);

        private static int _openCount;

        private readonly long _endTicks;
        private readonly string _destination;
        private bool _onTop = true;
        private bool _alarmAcknowledged;
        private string _lastCountdown;

        private IContainer _components;
        private Label _captionLabel;
        private Label _countdownLabel;
        private Label _closeLabel;
        private ToolStripMenuItem _onTopItem;
        private System.Windows.Forms.Timer _tickTimer;
        private Font _captionFont;
        private Font _countdownFont;
        private Font _closeFont;

        /// <summary>
        /// Pops out a new timer window. Call on the game thread with everything already
        /// resolved to plain values; secsLeft is measured the same way the side panel
        /// measures its own countdown, so the two read the same.
        /// </summary>
        public static void Open(string action, string from, string to, double secsLeft)
        {
            try
            {
                // Fix the deadline now, so the new thread's start-up time doesn't shift it
                long endTicks = Stopwatch.GetTimestamp() + (long)(secsLeft * Stopwatch.Frequency);
                Point anchor = Cursor.Position;
                int cascade = (Interlocked.Increment(ref _openCount) - 1) % CascadeSlots;

                Thread thread = new Thread(() => RunTimer(action, from, to, endTicks, anchor, cascade));
                thread.Name = "ArmyTimer";
                thread.IsBackground = true;     // never keeps the game process alive
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            catch (Exception ex)
            {
                UniversalDebugLog.Log("ArmyTimerForm.Open failed: " + ex.Message);
            }
        }

        private static void RunTimer(string action, string from, string to, long endTicks, Point anchor, int cascade)
        {
            try
            {
                // Per-thread handler: keeps a fault in this window from raising WinForms'
                // exception dialog, and leaves the game thread's handler alone.
                Application.ThreadException += (sender, e) =>
                    UniversalDebugLog.Log("ArmyTimerForm error: " + e.Exception.Message);
                Application.Run(new ArmyTimerForm(action, from, to, endTicks, anchor, cascade));
            }
            catch (Exception ex)
            {
                UniversalDebugLog.Log("ArmyTimerForm thread failed: " + ex.Message);
            }
            finally
            {
                Interlocked.Decrement(ref _openCount);
            }
        }

        private ArmyTimerForm(string action, string from, string to, long endTicks, Point anchor, int cascade)
        {
            _endTicks = endTicks;
            _destination = to;
            _components = new Container();

            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = true;
            this.DoubleBuffered = true;
            this.BackColor = FormBg;
            this.ClientSize = new Size(FormWidth, FormHeight);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = StartLocation(anchor, cascade);

            try { this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); }
            catch (Exception) { }

            BuildUI(action, from, to);

            _tickTimer = new System.Windows.Forms.Timer(_components);
            _tickTimer.Interval = 50;
            _tickTimer.Tick += new EventHandler(this.TickTimerTick);
            _tickTimer.Start();

            UpdateDisplay();
        }

        /// <summary>
        /// Left of the cursor - the button that opened it sits in the right-hand dock - and
        /// stepped down per open timer so a batch doesn't stack exactly.
        /// </summary>
        private static Point StartLocation(Point anchor, int cascade)
        {
            Rectangle work = Screen.FromPoint(anchor).WorkingArea;
            int x = anchor.X - FormWidth - 24;
            int y = anchor.Y - (FormHeight / 2) + (cascade * CascadeStep);
            x = Math.Max(work.Left, Math.Min(x, work.Right - FormWidth));
            y = Math.Max(work.Top, Math.Min(y, work.Bottom - FormHeight));
            return new Point(x, y);
        }

        /// <summary>
        /// "action: from → to", shortening the from name first when it doesn't fit, since
        /// where the army is heading matters more than where it left.
        /// </summary>
        private static string FitCaption(string action, string from, string to, Font font, int width)
        {
            string caption = action + ": " + from + " → " + to;
            for (int keep = from.Length - 1; keep > 0 && TextRenderer.MeasureText(caption, font).Width > width; keep--)
                caption = action + ": " + from.Substring(0, keep).TrimEnd() + "… → " + to;
            return caption;     // a destination too long on its own is left to AutoEllipsis
        }

        private void BuildUI(string action, string from, string to)
        {
            this.SuspendLayout();

            _captionFont = new Font("Arial", 8.25f, FontStyle.Regular);
            _countdownFont = new Font("Arial", 24f, FontStyle.Bold);
            _closeFont = new Font("Arial", 11f, FontStyle.Bold);

            ContextMenuStrip menu = new ContextMenuStrip(_components);
            _onTopItem = new ToolStripMenuItem("Always on top");
            _onTopItem.Checked = true;
            _onTopItem.Click += new EventHandler(this.OnTopItemClick);
            ToolStripMenuItem closeItem = new ToolStripMenuItem("Close");
            closeItem.Click += delegate { this.Close(); };
            menu.Items.Add(_onTopItem);
            menu.Items.Add(closeItem);
            this.ContextMenuStrip = menu;

            _captionLabel = new Label();
            _captionLabel.AutoSize = false;
            _captionLabel.AutoEllipsis = true;
            _captionLabel.Location = new Point(8, 6);
            _captionLabel.Size = new Size(FormWidth - 8 - 24, 16);
            _captionLabel.Font = _captionFont;
            _captionLabel.ForeColor = TextSec;
            _captionLabel.BackColor = Color.Transparent;
            _captionLabel.Text = FitCaption(action, from, to, _captionFont, _captionLabel.Width - 4);
            _captionLabel.ContextMenuStrip = menu;
            _captionLabel.MouseDown += new MouseEventHandler(this.DragMouseDown);
            this.Controls.Add(_captionLabel);

            ToolTip tip = new ToolTip(_components);
            tip.SetToolTip(_captionLabel, action + ": " + from + " → " + to);

            _countdownLabel = new Label();
            _countdownLabel.AutoSize = false;
            _countdownLabel.Location = new Point(4, 21);
            _countdownLabel.Size = new Size(FormWidth - 8, FormHeight - 21 - 4);
            _countdownLabel.Font = _countdownFont;
            _countdownLabel.ForeColor = TextPri;
            _countdownLabel.BackColor = Color.Transparent;
            _countdownLabel.TextAlign = ContentAlignment.MiddleCenter;
            _countdownLabel.ContextMenuStrip = menu;
            _countdownLabel.MouseDown += new MouseEventHandler(this.DragMouseDown);
            this.Controls.Add(_countdownLabel);

            _closeLabel = new Label();
            _closeLabel.AutoSize = false;
            _closeLabel.Location = new Point(FormWidth - 22, 2);
            _closeLabel.Size = new Size(20, 20);
            _closeLabel.Font = _closeFont;
            _closeLabel.ForeColor = TextSec;
            _closeLabel.BackColor = Color.Transparent;
            _closeLabel.TextAlign = ContentAlignment.MiddleCenter;
            _closeLabel.Text = "×";
            _closeLabel.Cursor = Cursors.Hand;
            _closeLabel.MouseEnter += delegate { _closeLabel.ForeColor = TextPri; };
            _closeLabel.MouseLeave += delegate { _closeLabel.ForeColor = TextSec; };
            _closeLabel.Click += delegate { this.Close(); };
            this.Controls.Add(_closeLabel);
            _closeLabel.BringToFront();

            this.MouseDown += new MouseEventHandler(this.DragMouseDown);

            this.ResumeLayout(false);
        }

        // Popping a timer out shouldn't pull focus off the game
        protected override bool ShowWithoutActivation => true;

        /// <summary>
        /// Topmost is set through the window style and SetWindowPos(SWP_NOACTIVATE) rather
        /// than the TopMost property, which activates the window when it is applied.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (_onTop)
                    cp.ExStyle |= WS_EX_TOPMOST;
                return cp;
            }
        }

        private void OnTopItemClick(object sender, EventArgs e)
        {
            _onTop = !_onTop;
            _onTopItem.Checked = _onTop;
            if (this.IsHandleCreated)
                SetWindowPos(this.Handle, _onTop ? HWND_TOPMOST : HWND_NOTOPMOST, 0, 0, 0, 0,
                    SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
        }

        private void DragMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Point before = this.Location;
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

            // The move loop returns on release; a press that didn't move the window was a
            // click, which silences a flashing alarm.
            if (this.Location == before && SecondsLeft() <= 0.0 && !_alarmAcknowledged)
            {
                _alarmAcknowledged = true;
                UpdateDisplay();
            }
        }

        private void TickTimerTick(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private double SecondsLeft()
        {
            return (double)(_endTicks - Stopwatch.GetTimestamp()) / Stopwatch.Frequency;
        }

        private void UpdateDisplay()
        {
            double remaining = SecondsLeft();

            // Truncated like the side panel's (int) cast, so the two tick over together
            string countdown = FormatRemaining(remaining > 0.0 ? (int)remaining : 0);
            if (countdown != _lastCountdown)
            {
                _lastCountdown = countdown;
                _countdownLabel.Text = countdown;
                this.Text = countdown + " " + _destination;
            }

            Color bg = FormBg;
            if (remaining <= 0.0)
            {
                // Flash at 2Hz off the same clock until clicked, then hold red
                bool redPhase = _alarmAcknowledged || ((long)(-remaining * 2.0)) % 2 == 0;
                bg = redPhase ? AlarmBg : FormBg;
            }
            if (this.BackColor != bg)
            {
                this.BackColor = bg;
                _captionLabel.ForeColor = bg == AlarmBg ? TextPri : TextSec;    // grey is hard to read on red
            }
        }

        private static string FormatRemaining(int secs)
        {
            int hours = secs / 3600;
            int minutes = (secs / 60) % 60;
            int seconds = secs % 60;
            if (hours > 0)
                return hours + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
            return minutes.ToString("00") + ":" + seconds.ToString("00");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen pen = new Pen(BorderColor))
                e.Graphics.DrawRectangle(pen, 0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _components != null)
                _components.Dispose();
            base.Dispose(disposing);

            // After the labels are gone, since they paint with these
            if (disposing)
            {
                if (_captionFont != null) _captionFont.Dispose();
                if (_countdownFont != null) _countdownFont.Dispose();
                if (_closeFont != null) _closeFont.Dispose();
            }
        }
    }
}
