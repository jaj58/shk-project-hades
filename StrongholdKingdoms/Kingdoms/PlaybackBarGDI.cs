// Decompiled with JetBrains decompiler
// Type: Kingdoms.PlaybackBarGDI
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  internal class PlaybackBarGDI : CustomSelfDrawPanel.CSDControl
  {
    private CustomSelfDrawPanel.CSDImage background = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage playPause = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage playStop = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage playSpeed1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage playSpeed2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage playSpeed4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage trackButton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage trackLine = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage expandToggle = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel dayLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel dayNumber = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage expandedBackground = new CustomSelfDrawPanel.CSDImage();
    public bool isDirty;
    public bool pauseTimeline;
    private bool isPaused;
    public bool trackHeld;
    private bool mouseInside;
    private bool isExpanded;
    private bool isAnimating;
    private DateTime animStartTime = DateTime.Now;
    private double animDuration = 200.0;
    private int trackLength;
    public Point relativeMousePos;

    public void init()
    {
      this.clearControls();
      this.Size = new Size(500, 100);
      this.trackLength = 4 * this.Width / 5;
      this.background.Image = (Image) GFXLibrary.playbackBackground;
      this.background.Position = new Point(0, this.Height / 2);
      this.background.Data = 0;
      this.background.Visible = true;
      this.background.Enabled = true;
      this.background.Alpha = 0.5f;
      this.background.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickBackground));
      this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
      this.playStop.Image = (Image) GFXLibrary.playbackStop;
      this.playStop.Position = new Point(this.Width / 10 - this.playStop.Image.Width / 2, this.background.Image.Size.Height / 2 - this.playStop.Image.Height / 2);
      this.playStop.Data = 0;
      this.playStop.Visible = true;
      this.playStop.Enabled = true;
      this.playStop.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickStop));
      this.playStop.CustomTooltipID = 22000;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.playStop);
      this.playPause.Image = (Image) GFXLibrary.playbackPause;
      this.playPause.Position = new Point(this.Width * 2 / 10 - this.playPause.Image.Width / 2, this.background.Image.Height / 2 - this.playPause.Image.Height / 2);
      this.playPause.Data = 0;
      this.playPause.Visible = true;
      this.playPause.Enabled = true;
      this.playPause.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickTogglePause));
      this.playPause.CustomTooltipID = 22001;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.playPause);
      this.playSpeed1.Image = (Image) GFXLibrary.playbackSpeed1;
      this.playSpeed1.Position = new Point(this.Width * 3 / 10 - this.playSpeed1.Image.Width / 2, this.background.Image.Height / 2 - this.playSpeed1.Image.Height / 2);
      this.playSpeed1.Data = 0;
      this.playSpeed1.Visible = true;
      this.playSpeed1.Enabled = true;
      this.playSpeed1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickNormalSpeed));
      this.playSpeed1.CustomTooltipID = 22003;
      this.playSpeed1.Colorise = ARGBColors.White;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.playSpeed1);
      this.playSpeed2.Image = (Image) GFXLibrary.playbackSpeed2;
      this.playSpeed2.Position = new Point(this.Width * 4 / 10 - this.playSpeed2.Image.Width / 2, this.background.Image.Height / 2 - this.playSpeed2.Image.Height / 2);
      this.playSpeed2.Data = 0;
      this.playSpeed2.Visible = true;
      this.playSpeed2.Enabled = true;
      this.playSpeed2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickDoubleSpeed));
      this.playSpeed2.CustomTooltipID = 22004;
      this.playSpeed2.Colorise = ARGBColors.Gray;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.playSpeed2);
      this.playSpeed4.Image = (Image) GFXLibrary.playbackSpeed4;
      this.playSpeed4.Position = new Point(this.Width * 5 / 10 - this.playSpeed2.Image.Width / 2, this.background.Image.Height / 2 - this.playSpeed2.Image.Height / 2);
      this.playSpeed4.Data = 0;
      this.playSpeed4.Visible = true;
      this.playSpeed4.Enabled = true;
      this.playSpeed4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickQuadSpeed));
      this.playSpeed4.CustomTooltipID = 22005;
      this.playSpeed4.Colorise = ARGBColors.Gray;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.playSpeed4);
      this.trackButton.Image = (Image) GFXLibrary.playbackBlank;
      this.trackButton.Position = new Point(this.Width / 2 - this.trackLength / 2, this.background.Image.Height / 2 - this.trackButton.Height / 2);
      this.trackButton.Data = 0;
      this.trackButton.Visible = true;
      this.trackButton.Enabled = true;
      this.trackButton.setMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.mouseDownDel), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.mouseUpDel));
      this.trackLine.Image = (Image) GFXLibrary.playbackTrack;
      this.trackLine.Position = new Point(this.Width / 2 - this.trackLength / 2, this.background.Image.Height / 2 - this.trackLine.Height / 2);
      this.trackLine.Data = 0;
      this.trackLine.Visible = true;
      this.trackLine.Enabled = true;
      this.trackLine.setMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.mouseDownDel), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.mouseUpDel));
      this.expandedBackground.addControl((CustomSelfDrawPanel.CSDControl) this.trackLine);
      this.expandedBackground.addControl((CustomSelfDrawPanel.CSDControl) this.trackButton);
      this.dayNumber.Text = "Day " + GameEngine.Instance.World.getPlaybackDay().ToString();
      this.dayNumber.Color = ARGBColors.White;
      this.dayNumber.RolloverColor = ARGBColors.White;
      this.dayNumber.Position = new Point(this.Width * 15 / 20, 0);
      this.dayNumber.Size = new Size(this.Width / 10, this.background.Image.Height);
      this.dayNumber.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.dayNumber.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.dayNumber);
      this.expandToggle.Image = (Image) GFXLibrary.playbackExpand;
      this.expandToggle.Position = new Point(this.Width * 9 / 10 - this.expandToggle.Image.Width / 2, this.background.Image.Height / 2 - this.expandToggle.Image.Height / 2);
      this.expandToggle.Data = 0;
      this.expandToggle.Visible = true;
      this.expandToggle.Enabled = true;
      this.expandToggle.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickExpand));
      this.expandToggle.CustomTooltipID = 22006;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.expandToggle);
      this.expandedBackground.Image = (Image) GFXLibrary.playbackBackground;
      this.expandedBackground.Position = new Point(0, this.Height / 2);
      this.expandedBackground.Data = 0;
      this.expandedBackground.Visible = false;
      this.expandedBackground.Enabled = false;
      this.expandedBackground.Alpha = 0.0f;
      this.expandedBackground.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickBackground));
      this.addControl((CustomSelfDrawPanel.CSDControl) this.expandedBackground);
      this.invalidate();
      this.refresh();
    }

    public void refresh() => this.update();

    public void update()
    {
      this.dayNumber.Text = "Day " + GameEngine.Instance.World.getPlaybackDay().ToString();
      if (this.trackHeld)
      {
        if (!this.mouseInside)
        {
          this.mouseUpDel();
          return;
        }
        this.trackButton.X = Math.Max(this.Width / 2 - this.trackLength / 2, Math.Min(this.relativeMousePos.X, this.Width / 2 + this.trackLength / 2)) - this.trackButton.Width / 2;
        this.isDirty = true;
        this.invalidate();
        int day = this.convertPosToDay(this.trackButton.X + this.trackButton.Width / 2);
        GameEngine.Instance.World.setPlaybackDay(day);
      }
      else
      {
        this.trackButton.X = this.convertDayToPos(GameEngine.Instance.World.playbackDay) - this.trackButton.Width / 2;
        this.isDirty = true;
        this.invalidate();
      }
      if (!this.isAnimating)
        return;
      double num = (DateTime.Now - this.animStartTime).TotalMilliseconds / this.animDuration;
      if (num >= 1.0)
      {
        this.isAnimating = false;
        this.expandedBackground.Visible = this.isExpanded;
        this.expandedBackground.Enabled = this.isExpanded;
      }
      else
      {
        if (this.isExpanded)
        {
          this.trackLine.Alpha = (float) num;
          this.trackButton.Alpha = (float) num;
          this.expandedBackground.Alpha = 0.5f * (float) num;
          this.expandedBackground.Y = this.Height / 2 - (int) ((double) (this.Height / 2) * num);
        }
        else
        {
          this.trackLine.Alpha = 1f - (float) num;
          this.trackButton.Alpha = 1f - (float) num;
          this.expandedBackground.Alpha = (float) (0.5 - 0.5 * num);
          this.expandedBackground.Y = (int) ((double) (this.Height / 2) * num);
        }
        this.invalidate();
      }
    }

    public void flagAsRendered() => this.isDirty = false;

    public void toggleActive(bool value)
    {
    }

    public void setMouseRelative(Point pos)
    {
      this.mouseInside = this.Rectangle.Contains(pos);
      this.relativeMousePos.X = Math.Max(0, Math.Min(this.Width, pos.X));
      this.relativeMousePos.Y = Math.Max(0, Math.Min(this.Height, pos.Y));
    }

    public void clickBackground()
    {
      int num = 0 + 1;
    }

    public void clickStop() => GameEngine.Instance.World.stopPlayback();

    public void clickTogglePause()
    {
      GameEngine.Instance.World.togglePlaybackPause();
      this.isPaused = !this.isPaused;
      this.playPause.Image = (Image) (this.isPaused ? GFXLibrary.playbackPlay : GFXLibrary.playbackPause);
      this.playPause.CustomTooltipID = this.isPaused ? 22002 : 22001;
    }

    public void clickNormalSpeed()
    {
      GameEngine.Instance.World.changePlaybackSpeed(1.0);
      this.playSpeed1.Colorise = ARGBColors.White;
      this.playSpeed2.Colorise = ARGBColors.Gray;
      this.playSpeed4.Colorise = ARGBColors.Gray;
    }

    public void clickDoubleSpeed()
    {
      GameEngine.Instance.World.changePlaybackSpeed(2.0);
      this.playSpeed1.Colorise = ARGBColors.Gray;
      this.playSpeed2.Colorise = ARGBColors.White;
      this.playSpeed4.Colorise = ARGBColors.Gray;
    }

    public void clickQuadSpeed()
    {
      GameEngine.Instance.World.changePlaybackSpeed(4.0);
      this.playSpeed1.Colorise = ARGBColors.Gray;
      this.playSpeed2.Colorise = ARGBColors.Gray;
      this.playSpeed4.Colorise = ARGBColors.White;
    }

    public void clickExpand()
    {
      if (this.isAnimating)
        return;
      this.isExpanded = true;
      this.expandToggle.Image = (Image) GFXLibrary.playbackContract;
      this.expandToggle.CustomTooltipID = 22007;
      this.expandToggle.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickContract));
      this.isAnimating = true;
      this.animStartTime = DateTime.Now;
      this.expandedBackground.Visible = true;
    }

    public void clickContract()
    {
      if (this.isAnimating)
        return;
      this.isExpanded = false;
      this.expandToggle.Image = (Image) GFXLibrary.playbackExpand;
      this.expandToggle.CustomTooltipID = 22006;
      this.expandToggle.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickExpand));
      this.isAnimating = true;
      this.animStartTime = DateTime.Now;
    }

    public void mouseDownDel()
    {
      if (this.trackHeld)
        return;
      this.trackHeld = true;
    }

    public void mouseUpDel()
    {
      if (!this.trackHeld)
        return;
      this.trackHeld = false;
    }

    public int convertDayToPos(int day)
    {
      int playbackBasedDay = GameEngine.Instance.World.playbackBasedDay;
      int playbackTotalDays = GameEngine.Instance.World.playbackTotalDays;
      int num1 = this.Width / 2 - this.trackLength / 2;
      int num2 = this.Width / 2 + this.trackLength / 2;
      double num3 = (double) day / (double) playbackTotalDays;
      return (int) ((double) num1 + (double) (num2 - num1) * num3);
    }

    public int convertPosToDay(int pos)
    {
      int num1 = this.Width / 2 - this.trackLength / 2;
      int num2 = this.Width / 2 + this.trackLength / 2;
      return (int) ((double) GameEngine.Instance.World.playbackTotalDays * ((double) (pos - num1) / (double) (num2 - num1)));
    }
  }
}
