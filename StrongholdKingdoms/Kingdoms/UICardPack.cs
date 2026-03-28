// Decompiled with JetBrains decompiler
// Type: Kingdoms.UICardPack
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Collections.Generic;

//#nullable disable
namespace Kingdoms
{
  public class UICardPack : CustomSelfDrawPanel.CSDControl
  {
    public int OfferID;
    public CustomSelfDrawPanel.CSDImage baseImage = new CustomSelfDrawPanel.CSDImage();
    public CustomSelfDrawPanel.CSDImage overImage = new CustomSelfDrawPanel.CSDImage();
    public CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
    public CustomSelfDrawPanel.CSDLabel descriptionLabel = new CustomSelfDrawPanel.CSDLabel();
    public List<int> PackIDs = new List<int>();
    public string nameText = string.Empty;
  }
}
