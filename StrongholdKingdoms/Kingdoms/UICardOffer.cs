// Decompiled with JetBrains decompiler
// Type: Kingdoms.UICardOffer
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;

//#nullable disable
namespace Kingdoms
{
  public class UICardOffer : CustomSelfDrawPanel.CSDControl
  {
    public CustomSelfDrawPanel.CSDImage baseImage = new CustomSelfDrawPanel.CSDImage();
    public CustomSelfDrawPanel.CSDImage packImage = new CustomSelfDrawPanel.CSDImage();
    public CustomSelfDrawPanel.CSDImage packOverImage = new CustomSelfDrawPanel.CSDImage();
    public CustomSelfDrawPanel.CSDImage fanImage = new CustomSelfDrawPanel.CSDImage();
    public CustomSelfDrawPanel.CSDImage crownImage = new CustomSelfDrawPanel.CSDImage();
    public CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
    public CustomSelfDrawPanel.CSDLabel descLabel = new CustomSelfDrawPanel.CSDLabel();
    public CustomSelfDrawPanel.CSDLabel cardLabel = new CustomSelfDrawPanel.CSDLabel();
    public CustomSelfDrawPanel.CSDLabel costLabel = new CustomSelfDrawPanel.CSDLabel();
    public CardTypes.CardOffer Offer;
  }
}
