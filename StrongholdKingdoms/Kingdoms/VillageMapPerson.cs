// Decompiled with JetBrains decompiler
// Type: Kingdoms.VillageMapPerson
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class VillageMapPerson
  {
    private GraphicsMgr gfx;
    public SpriteWrapper workerSprite;
    public VillageMapPerson.VillagePeopleStates state;
    public PointF startPos;
    public PointF endPos;
    public PointF currentPos;
    public DateTime startTime = DateTime.Now;
    public DateTime endTime = DateTime.Now;
    public int facing;
    public bool idling;
    public bool working;
    public int fadeDir;

    public VillageMapPerson(GraphicsMgr newGfx) => this.gfx = newGfx;

    public void dispose()
    {
      if (this.workerSprite == null)
        return;
      this.workerSprite.RemoveSelfFromParent();
    }

    public void setPos(Point pos)
    {
      Point point = new Point(pos.X, pos.Y);
      point.X *= 32;
      point.Y *= 16;
      point.Y += 8;
      this.currentPos = (PointF) point;
    }

    public void setPixelPos(Point pos) => this.currentPos = (PointF) pos;

    public PointF getPos() => this.currentPos;

    public void fadeToSolid() => this.fadeDir = 10;

    public void fadeToTransparent() => this.fadeDir = -10;

    public void initWorkerSprite()
    {
      if (this.workerSprite != null)
        return;
      this.workerSprite = new SpriteWrapper();
      if (GameEngine.Instance.Village == null)
        return;
      GameEngine.Instance.Village.addChildSprite(this.workerSprite, 15);
    }

    public void initWorkerSpriteInBuilding(SpriteWrapper buildingSprite)
    {
      if (this.workerSprite != null)
        return;
      this.workerSprite = new SpriteWrapper();
      buildingSprite.AddChild(this.workerSprite, 1);
    }

    public void initAnim(int texID, int baseFrame, short[] animarray, int animTime)
    {
      this.initWorkerSprite();
      this.workerSprite.Initialize(this.gfx, texID, baseFrame);
      this.workerSprite.clearDirectionality();
      this.workerSprite.initAnim(baseFrame, animarray, animTime);
      this.workerSprite.Center = (PointF) new Point(50, 66);
      this.workerSprite.Visible = true;
    }

    public void initAnim(int texID, int baseID, int numFrames, int animTime)
    {
      this.initAnim(texID, 0, baseID, numFrames, 1, animTime, true);
    }

    public void initAnim(
      int texID,
      int upDir,
      int baseID,
      int numFrames,
      int frameSkip,
      int animTime,
      bool clockwise)
    {
      this.initWorkerSprite();
      this.workerSprite.Initialize(this.gfx, texID, baseID);
      if (frameSkip == 1)
      {
        this.workerSprite.clearDirectionality();
        this.workerSprite.initAnim(baseID, numFrames, frameSkip, animTime);
      }
      else
      {
        this.workerSprite.initDirectionality(8, upDir, !clockwise);
        this.workerSprite.initAnim(baseID, numFrames, frameSkip, animTime);
      }
      this.workerSprite.Center = (PointF) new Point(50, 66);
      this.workerSprite.Visible = true;
    }

    public void startJourneyTileBased(
      Point newStartPos,
      Point newEndPos,
      double distThroughJourney)
    {
      this.startJourney(VillageBuildingsData.tileToPixel(newStartPos), VillageBuildingsData.tileToPixel(newEndPos), distThroughJourney);
    }

    public void startJourney(Point realStart, Point realEnd, double distThroughJourney)
    {
      this.startPos = (PointF) realStart;
      this.endPos = (PointF) realEnd;
      if (distThroughJourney >= 1.0)
      {
        this.currentPos = this.endPos;
        this.state = VillageMapPerson.VillagePeopleStates.STATIONARY;
      }
      else
      {
        TimeSpan timeSpan = VillageBuildingsData.calcTravelTime(GameEngine.Instance.LocalWorldData, realStart, realEnd);
        this.startTime = DateTime.Now;
        this.endTime = DateTime.Now.Add(timeSpan);
        if (distThroughJourney != 0.0)
        {
          double num = timeSpan.TotalSeconds * distThroughJourney;
          this.startTime = this.startTime.AddSeconds(0.0 - num);
          this.endTime = this.endTime.AddSeconds(0.0 - num);
        }
        this.state = VillageMapPerson.VillagePeopleStates.MOVING;
        this.facing = SpriteWrapper.getFacing(this.startPos, this.endPos, 8);
        this.updateJourney();
      }
    }

    public void updateJourney()
    {
      if (this.state != VillageMapPerson.VillagePeopleStates.MOVING)
        return;
      DateTime now = DateTime.Now;
      if (now >= this.endTime)
      {
        this.currentPos = this.endPos;
        this.state = VillageMapPerson.VillagePeopleStates.STATIONARY;
      }
      else
      {
        TimeSpan timeSpan = this.endTime - this.startTime;
        double num = (now - this.startTime).TotalSeconds / timeSpan.TotalSeconds;
        this.currentPos = new PointF((float) (((double) this.endPos.X - (double) this.startPos.X) * num) + this.startPos.X, (float) (((double) this.endPos.Y - (double) this.startPos.Y) * num) + this.startPos.Y);
      }
    }

    public PointF getCurrentPos() => this.currentPos;

    public bool isJourneyOver() => this.state != VillageMapPerson.VillagePeopleStates.MOVING;

    public void update()
    {
      this.updateJourney();
      if (this.workerSprite == null)
        return;
      this.workerSprite.PosX = this.currentPos.X;
      this.workerSprite.PosY = this.currentPos.Y;
      this.workerSprite.Facing = this.facing;
      int alpha = (int) this.workerSprite.ColorToUse.A + this.fadeDir;
      if (alpha < 160)
        alpha = 160;
      else if (alpha > (int) byte.MaxValue)
        alpha = (int) byte.MaxValue;
      this.workerSprite.ColorToUse = Color.FromArgb((int) (byte) alpha, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
    }

    public bool Visible
    {
      set
      {
        if (this.workerSprite == null)
          return;
        this.workerSprite.Visible = value;
      }
    }

    public enum VillagePeopleStates
    {
      STATIONARY,
      MOVING,
    }
  }
}
