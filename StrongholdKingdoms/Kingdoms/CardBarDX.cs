// Decompiled with JetBrains decompiler
// Type: Kingdoms.CardBarDX
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class CardBarDX
  {
    private CardBarGDI cardbar = new CardBarGDI();
    private CustomSelfDrawPanel csd = new CustomSelfDrawPanel();
    private int delayedSection = -1;

    public void init(int cardSection)
    {
      this.delayedSection = -1;
      this.csd.clearControls();
      this.csd.addControl((CustomSelfDrawPanel.CSDControl) this.cardbar);
      this.cardbar.init(cardSection);
      this.update();
    }

    public void delayedInit(int cardSection) => this.delayedSection = cardSection;

    public void update()
    {
      if (!InterfaceMgr.Instance.allowDrawCircles())
        return;
      if (this.delayedSection >= 0)
      {
        this.init(this.delayedSection);
      }
      else
      {
        this.cardbar.update();
        if (!this.cardbar.Dirty)
          return;
        Graphics dxOverlayTexture = GameEngine.Instance.GFX.createDXOverlayTexture(this.cardbar.Size);
        if (dxOverlayTexture != null)
        {
          if (this.csd.initFromDX(dxOverlayTexture, (CustomSelfDrawPanel.CSDControl) this.cardbar))
          {
            this.csd.drawControls();
            this.csd.endPaint();
          }
          GameEngine.Instance.GFX.renderDXOverlayTexture(dxOverlayTexture);
        }
        this.cardbar.flagAsRendered();
      }
    }

    public bool click(Point mousePos) => this.csd.baseControl.parentClicked(mousePos);

    public void toggleEnabled(bool value) => this.cardbar.toggleActive(value);

    public void mouseMove(Point mousePos)
    {
      this.csd.tooltipSet = false;
      CustomTooltipManager.MouseLeaveTooltipArea();
      this.csd.baseControl.parentMouseOver(mousePos);
      TutorialWindow.tooltip(mousePos);
    }
  }
}
