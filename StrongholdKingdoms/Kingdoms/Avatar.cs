// Decompiled with JetBrains decompiler
// Type: Kingdoms.Avatar
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

//#nullable disable
namespace Kingdoms
{
  public class Avatar
  {
    public static Bitmap CreateAvatar(AvatarData avatar, Color backgroundColour)
    {
      Bitmap bitmap = new Bitmap(154, 500);
      return Avatar.CreateAvatar(avatar, bitmap, backgroundColour);
    }

    public static Bitmap CreateAvatar(AvatarData avatar, int height)
    {
      Bitmap bitmap = new Bitmap(154, 500);
      Avatar.CreateAvatar(avatar, bitmap, ARGBColors.LightGoldenrodYellow, true);
      Bitmap avatar1 = new Bitmap(154 * height / 500, height);
      Graphics graphics = Graphics.FromImage((Image) avatar1);
      graphics.DrawImage((Image) bitmap, new Rectangle(0, 0, avatar1.Width, avatar1.Height), new Rectangle(0, 0, 154, 500), GraphicsUnit.Pixel);
      graphics.Dispose();
      bitmap.Dispose();
      return avatar1;
    }

    public static Bitmap CreateAvatar(
      AvatarData avatar,
      int height,
      Color backgroundColour,
      bool drawParchment)
    {
      Bitmap bitmap = new Bitmap(154, 500);
      Avatar.CreateAvatar(avatar, bitmap, backgroundColour, drawParchment);
      Bitmap avatar1 = new Bitmap(154 * height / 500, height);
      Graphics graphics = Graphics.FromImage((Image) avatar1);
      graphics.DrawImage((Image) bitmap, new Rectangle(0, 0, avatar1.Width, avatar1.Height), new Rectangle(0, 0, 154, 500), GraphicsUnit.Pixel);
      graphics.Dispose();
      bitmap.Dispose();
      return avatar1;
    }

    public static Bitmap CreateExportAvatar(
      AvatarData avatar,
      Color backgroundColour,
      bool drawParchment)
    {
      Bitmap bitmap = new Bitmap(154, 500);
      Avatar.CreateAvatar(avatar, bitmap, backgroundColour, drawParchment);
      Bitmap exportAvatar = new Bitmap(90, 256);
      Graphics graphics = Graphics.FromImage((Image) exportAvatar);
      graphics.DrawImage((Image) bitmap, new Rectangle(3, -11, 83, 272), new Rectangle(0, 0, 154, 500), GraphicsUnit.Pixel);
      graphics.Dispose();
      bitmap.Dispose();
      return exportAvatar;
    }

    public static Bitmap CreateAvatar(AvatarData avatarData, Bitmap bitmap)
    {
      return Avatar.CreateAvatar(avatarData, bitmap, Color.FromArgb(105, 119, 129), true);
    }

    public static Bitmap CreateAvatar(AvatarData avatarData, Bitmap bitmap, Color backgroundColour)
    {
      return Avatar.CreateAvatar(avatarData, bitmap, backgroundColour, true);
    }

    public static void CreateExportAvatar(WorldMap.CachedUserInfo userInfo, string path)
    {
      Avatar.CreateExportAvatar(userInfo.avatarData, ARGBColors.Transparent, false).Save(path, ImageFormat.Png);
    }

    public static AvatarData getRatAvatar()
    {
      return new AvatarData()
      {
        male = true,
        floor = 4,
        body = 0,
        legs = 2,
        feet = 2,
        torso = 3,
        tabard = -1,
        arms = 2,
        hands = 2,
        shoulder = 1,
        face = 7,
        hair = -1,
        head = 12,
        weapon = 0,
        bodyColour = -2307931,
        legsColour = -4275256,
        feetColour = -4275256,
        torsoColour = -4275256,
        tabardColour = -9217456,
        armsColour = -2633011,
        handsColour = -4275256,
        shouldersColour = -4275256,
        hairColour = -6588326,
        headColour = -2633011,
        weaponColour = -10866131
      };
    }

    public static AvatarData getSnakeAvatar()
    {
      return new AvatarData()
      {
        male = true,
        floor = 1,
        body = 0,
        legs = 0,
        feet = 1,
        torso = 1,
        tabard = -1,
        arms = 1,
        hands = -1,
        shoulder = -1,
        face = 8,
        hair = 0,
        head = 2,
        weapon = -1,
        bodyColour = -2307931,
        legsColour = -14803426,
        feetColour = -10526881,
        torsoColour = -14803426,
        tabardColour = -9217456,
        armsColour = -14803426,
        handsColour = -14803426,
        shouldersColour = -2633011,
        hairColour = -13816536,
        headColour = -9217456,
        weaponColour = -14803426
      };
    }

    public static AvatarData getPigAvatar()
    {
      return new AvatarData()
      {
        male = true,
        floor = 0,
        body = 0,
        legs = 1,
        feet = 3,
        torso = 0,
        tabard = 7,
        arms = 2,
        hands = 0,
        shoulder = 0,
        face = 9,
        hair = -1,
        head = -1,
        weapon = 2,
        bodyColour = -2307931,
        legsColour = -12828346,
        feetColour = -7896461,
        torsoColour = -6259366,
        tabardColour = -10526881,
        armsColour = -6259366,
        handsColour = -2633011,
        shouldersColour = -3619906,
        hairColour = -10526881,
        headColour = -2633011,
        weaponColour = -10529476
      };
    }

    public static AvatarData getWolfAvatar()
    {
      return new AvatarData()
      {
        male = true,
        floor = 5,
        body = 0,
        legs = 2,
        feet = 3,
        torso = 3,
        tabard = -1,
        arms = 2,
        hands = 2,
        shoulder = 3,
        face = 10,
        hair = -1,
        head = 13,
        weapon = 3,
        bodyColour = -2307931,
        legsColour = -7896461,
        feetColour = -12172996,
        torsoColour = -7896461,
        tabardColour = -6259366,
        armsColour = -9217456,
        handsColour = -12172996,
        shouldersColour = -10529476,
        hairColour = -3618626,
        headColour = -7896461,
        weaponColour = -10866131
      };
    }

    public static Bitmap CreateAvatar(
      AvatarData avatarData,
      Bitmap bitmap,
      Color backgroundColour,
      bool drawParchment)
    {
      if (GFXLibrary.avatar_parchment_base_layer != null)
      {
        Graphics graphics1 = Graphics.FromImage((Image) bitmap);
        Rectangle rectangle = new Rectangle(0, 0, 154, 500);
        SolidBrush solidBrush = new SolidBrush(backgroundColour);
        graphics1.FillRectangle((Brush) solidBrush, rectangle);
        solidBrush.Dispose();
        if (drawParchment)
          graphics1.DrawImage((Image) GFXLibrary.avatar_parchment_base_layer, rectangle, rectangle, GraphicsUnit.Pixel);
        Color white1 = ARGBColors.White;
        Color shouldersColour1 = avatarData.ShouldersColour;
        ShrunkImage rear = Avatar.getRear(avatarData);
        if (rear != null)
          graphics1.DrawImage(rear.image, rear.Dest, rear.Source.X, rear.Source.Y, rear.Source.Width, rear.Source.Height, GraphicsUnit.Pixel, Avatar.createColour(shouldersColour1));
        Color white2 = ARGBColors.White;
        ShrunkImage floor = Avatar.getFloor(avatarData);
        if (floor != null)
          graphics1.DrawImage(floor.image, floor.Dest, floor.Source.X, floor.Source.Y, floor.Source.Width, floor.Source.Height, GraphicsUnit.Pixel, Avatar.createColour(white2));
        Color bodyColour = avatarData.BodyColour;
        ShrunkImage body = Avatar.getBody(avatarData);
        if (body != null)
          graphics1.DrawImage(body.image, body.Dest, body.Source.X, body.Source.Y, body.Source.Width, body.Source.Height, GraphicsUnit.Pixel, Avatar.createColour(bodyColour));
        if (avatarData.legs != 4 && avatarData.legs != 5 && avatarData.legs != 6 && avatarData.feet != 4)
        {
          Color legsColour = avatarData.LegsColour;
          ShrunkImage legs = Avatar.getLegs(avatarData);
          if (legs != null)
            graphics1.DrawImage(legs.image, legs.Dest, legs.Source.X, legs.Source.Y, legs.Source.Width, legs.Source.Height, GraphicsUnit.Pixel, Avatar.createColour(legsColour));
          Color feetColour = avatarData.FeetColour;
          ShrunkImage feet = Avatar.getFeet(avatarData);
          if (feet != null)
            graphics1.DrawImage(feet.image, feet.Dest, feet.Source.X, feet.Source.Y, feet.Source.Width, feet.Source.Height, GraphicsUnit.Pixel, Avatar.createColour(feetColour));
        }
        else
        {
          Color feetColour = avatarData.FeetColour;
          ShrunkImage feet = Avatar.getFeet(avatarData);
          if (feet != null)
            graphics1.DrawImage(feet.image, feet.Dest, feet.Source.X, feet.Source.Y, feet.Source.Width, feet.Source.Height, GraphicsUnit.Pixel, Avatar.createColour(feetColour));
          Color legsColour = avatarData.LegsColour;
          ShrunkImage legs = Avatar.getLegs(avatarData);
          if (legs != null)
            graphics1.DrawImage(legs.image, legs.Dest, legs.Source.X, legs.Source.Y, legs.Source.Width, legs.Source.Height, GraphicsUnit.Pixel, Avatar.createColour(legsColour));
        }
        Color torsoColour = avatarData.TorsoColour;
        ShrunkImage torso = Avatar.getTorso(avatarData);
        if (torso != null)
          graphics1.DrawImage(torso.image, torso.Dest, torso.Source.X, torso.Source.Y, torso.Source.Width, torso.Source.Height, GraphicsUnit.Pixel, Avatar.createColour(torsoColour));
        Color tabardColour = avatarData.TabardColour;
        ShrunkImage tabard = Avatar.getTabard(avatarData);
        if (tabard != null)
          graphics1.DrawImage(tabard.image, tabard.Dest, tabard.Source.X, tabard.Source.Y, tabard.Source.Width, tabard.Source.Height, GraphicsUnit.Pixel, Avatar.createColour(tabardColour));
        Color armsColour = avatarData.ArmsColour;
        ShrunkImage arms = Avatar.getArms(avatarData);
        if (arms != null)
          graphics1.DrawImage(arms.image, arms.Dest, arms.Source.X, arms.Source.Y, arms.Source.Width, arms.Source.Height, GraphicsUnit.Pixel, Avatar.createColour(armsColour));
        Color handsColour = avatarData.HandsColour;
        ShrunkImage hands = Avatar.getHands(avatarData);
        if (hands != null)
          graphics1.DrawImage(hands.image, hands.Dest, hands.Source.X, hands.Source.Y, hands.Source.Width, hands.Source.Height, GraphicsUnit.Pixel, Avatar.createColour(handsColour));
        Color shouldersColour2 = avatarData.ShouldersColour;
        ShrunkImage shoulders = Avatar.getShoulders(avatarData);
        if (shoulders != null)
          graphics1.DrawImage(shoulders.image, shoulders.Dest, shoulders.Source.X, shoulders.Source.Y, shoulders.Source.Width, shoulders.Source.Height, GraphicsUnit.Pixel, Avatar.createColour(shouldersColour2));
        Color hairColour1 = avatarData.HairColour;
        ShrunkImage face = Avatar.getFace(avatarData);
        Rectangle source;
        if (face != null)
        {
          Graphics graphics2 = graphics1;
          Image image = face.image;
          Rectangle dest = face.Dest;
          int x = face.Source.X;
          source = face.Source;
          int y = source.Y;
          source = face.Source;
          int width = source.Width;
          source = face.Source;
          int height = source.Height;
          ImageAttributes colour = Avatar.createColour(hairColour1);
          graphics2.DrawImage(image, dest, x, y, width, height, GraphicsUnit.Pixel, colour);
        }
        Color hairColour2 = avatarData.HairColour;
        ShrunkImage hair = Avatar.getHair(avatarData);
        if (hair != null)
        {
          Graphics graphics3 = graphics1;
          Image image = hair.image;
          Rectangle dest = hair.Dest;
          source = hair.Source;
          int x = source.X;
          source = hair.Source;
          int y = source.Y;
          source = hair.Source;
          int width = source.Width;
          source = hair.Source;
          int height = source.Height;
          ImageAttributes colour = Avatar.createColour(hairColour2);
          graphics3.DrawImage(image, dest, x, y, width, height, GraphicsUnit.Pixel, colour);
        }
        Color headColour = avatarData.HeadColour;
        ShrunkImage head = Avatar.getHead(avatarData);
        if (head != null)
        {
          Graphics graphics4 = graphics1;
          Image image = head.image;
          Rectangle dest = head.Dest;
          source = head.Source;
          int x = source.X;
          source = head.Source;
          int y = source.Y;
          source = head.Source;
          int width = source.Width;
          source = head.Source;
          int height = source.Height;
          ImageAttributes colour = Avatar.createColour(headColour);
          graphics4.DrawImage(image, dest, x, y, width, height, GraphicsUnit.Pixel, colour);
        }
        Color weaponColour1 = avatarData.WeaponColour;
        ShrunkImage weapon = Avatar.getWeapon(avatarData);
        if (weapon != null)
        {
          Graphics graphics5 = graphics1;
          Image image = weapon.image;
          Rectangle dest = weapon.Dest;
          source = weapon.Source;
          int x = source.X;
          source = weapon.Source;
          int y = source.Y;
          source = weapon.Source;
          int width = source.Width;
          source = weapon.Source;
          int height = source.Height;
          ImageAttributes colour = Avatar.createColour(weaponColour1);
          graphics5.DrawImage(image, dest, x, y, width, height, GraphicsUnit.Pixel, colour);
        }
        Color weaponColour2 = avatarData.WeaponColour;
        ShrunkImage belt = Avatar.getBelt(avatarData);
        if (belt != null)
        {
          Graphics graphics6 = graphics1;
          Image image = belt.image;
          Rectangle dest = belt.Dest;
          source = belt.Source;
          int x = source.X;
          source = belt.Source;
          int y = source.Y;
          source = belt.Source;
          int width = source.Width;
          source = belt.Source;
          int height = source.Height;
          ImageAttributes colour = Avatar.createColour(weaponColour2);
          graphics6.DrawImage(image, dest, x, y, width, height, GraphicsUnit.Pixel, colour);
        }
        if (true)
        {
          int length = bitmap.Width * bitmap.Height * 4;
          byte[] numArray = new byte[length];
          Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
          BitmapData bitmapdata = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
          IntPtr scan0 = bitmapdata.Scan0;
          Marshal.Copy(scan0, numArray, 0, length);
          byte[] parchementOverlay = GFXLibrary.parchementOverlay;
          for (int index = 0; index < parchementOverlay.Length; index += 4)
          {
            if (parchementOverlay[index] < byte.MaxValue)
              numArray[index] = (byte) ((int) numArray[index] * (int) parchementOverlay[index] / (int) byte.MaxValue);
            if (parchementOverlay[index + 1] < byte.MaxValue)
              numArray[index + 1] = (byte) ((int) numArray[index + 1] * (int) parchementOverlay[index + 1] / (int) byte.MaxValue);
            if (parchementOverlay[index + 2] < byte.MaxValue)
              numArray[index + 2] = (byte) ((int) numArray[index + 2] * (int) parchementOverlay[index + 2] / (int) byte.MaxValue);
            int num = (int) parchementOverlay[index + 3];
          }
          Marshal.Copy(numArray, 0, scan0, length);
          bitmap.UnlockBits(bitmapdata);
        }
        graphics1.Dispose();
      }
      return bitmap;
    }

    private static ImageAttributes createColour(Color color)
    {
      ColorMatrix newColorMatrix = new ColorMatrix();
      newColorMatrix.Matrix00 = (float) color.R / (float) byte.MaxValue;
      newColorMatrix.Matrix11 = (float) color.G / (float) byte.MaxValue;
      newColorMatrix.Matrix22 = (float) color.B / (float) byte.MaxValue;
      newColorMatrix.Matrix44 = 1f;
      newColorMatrix.Matrix33 = 1f;
      ImageAttributes colour = new ImageAttributes();
      colour.SetColorMatrix(newColorMatrix);
      return colour;
    }

    public static ShrunkImage getRear(AvatarData data)
    {
      return data.shoulder != 3 ? (ShrunkImage) null : (ShrunkImage) GFXLibrary.avatar_shoulders04_back;
    }

    public static ShrunkImage getFloor(AvatarData data)
    {
      switch (data.floor)
      {
        case 1:
          return (ShrunkImage) GFXLibrary.avatar_floor02;
        case 2:
          return (ShrunkImage) GFXLibrary.avatar_floor03;
        case 3:
          return (ShrunkImage) GFXLibrary.avatar_floor04;
        case 4:
          return (ShrunkImage) GFXLibrary.avatar_floor05;
        case 5:
          return (ShrunkImage) GFXLibrary.avatar_floor06;
        case 6:
          return (ShrunkImage) GFXLibrary.avatar_floor07;
        case 7:
          return (ShrunkImage) GFXLibrary.avatar_floor08;
        case 8:
          return (ShrunkImage) GFXLibrary.avatar_floor09;
        case 9:
          return (ShrunkImage) GFXLibrary.avatar_floor10;
        case 10:
          return (ShrunkImage) GFXLibrary.avatar_floor11;
        default:
          return (ShrunkImage) GFXLibrary.avatar_floor01;
      }
    }

    public static ShrunkImage getBody(AvatarData data)
    {
      int body = data.body;
      return (ShrunkImage) GFXLibrary.avatar_body01_default;
    }

    public static ShrunkImage getLegs(AvatarData data)
    {
      switch (data.legs)
      {
        case 1:
          return (ShrunkImage) GFXLibrary.avatar_legs02;
        case 2:
          return (ShrunkImage) GFXLibrary.avatar_legs03;
        case 3:
          return (ShrunkImage) GFXLibrary.avatar_legs04;
        case 4:
          return (ShrunkImage) GFXLibrary.avatar_legs05;
        case 5:
          return (ShrunkImage) GFXLibrary.avatar_legs06;
        case 6:
          return (ShrunkImage) GFXLibrary.avatar_legs07;
        default:
          return data.male ? (ShrunkImage) GFXLibrary.avatar_legs01_male : (ShrunkImage) GFXLibrary.avatar_legs01_female;
      }
    }

    public static ShrunkImage getFeet(AvatarData data)
    {
      switch (data.feet)
      {
        case -1:
          return (ShrunkImage) null;
        case 1:
          return (ShrunkImage) GFXLibrary.avatar_feet02;
        case 2:
          return (ShrunkImage) GFXLibrary.avatar_feet03;
        case 3:
          return (ShrunkImage) GFXLibrary.avatar_feet04;
        case 4:
          return (ShrunkImage) GFXLibrary.avatar_feet05;
        case 5:
          return (ShrunkImage) GFXLibrary.avatar_feet06;
        default:
          return (ShrunkImage) GFXLibrary.avatar_feet01;
      }
    }

    public static ShrunkImage getTorso(AvatarData data)
    {
      switch (data.torso)
      {
        case 1:
          return data.male ? (ShrunkImage) GFXLibrary.avatar_torso02_male : (ShrunkImage) GFXLibrary.avatar_torso02_female;
        case 2:
          return (ShrunkImage) GFXLibrary.avatar_torso03;
        case 3:
          return (ShrunkImage) GFXLibrary.avatar_torso04;
        default:
          return data.male ? (ShrunkImage) GFXLibrary.avatar_torso01_male_default : (ShrunkImage) GFXLibrary.avatar_torso01_female_default;
      }
    }

    public static ShrunkImage getTabard(AvatarData data)
    {
      switch (data.tabard)
      {
        case -1:
          return (ShrunkImage) null;
        case 1:
          return (ShrunkImage) GFXLibrary.avatar_tabard02;
        case 2:
          return (ShrunkImage) GFXLibrary.avatar_tabard03;
        case 3:
          return (ShrunkImage) GFXLibrary.avatar_tabard04;
        case 4:
          return (ShrunkImage) GFXLibrary.avatar_tabard05;
        case 5:
          return (ShrunkImage) GFXLibrary.avatar_tabard06;
        case 6:
          return (ShrunkImage) GFXLibrary.avatar_tabard07;
        case 7:
          return (ShrunkImage) GFXLibrary.avatar_tabard08;
        default:
          return (ShrunkImage) GFXLibrary.avatar_tabard01;
      }
    }

    public static ShrunkImage getArms(AvatarData data)
    {
      switch (data.arms)
      {
        case -1:
          return (ShrunkImage) null;
        case 1:
          return (ShrunkImage) GFXLibrary.avatar_arms02;
        case 2:
          return (ShrunkImage) GFXLibrary.avatar_arms03;
        case 3:
          return (ShrunkImage) GFXLibrary.avatar_arms04;
        default:
          return (ShrunkImage) GFXLibrary.avatar_arms01;
      }
    }

    public static ShrunkImage getHands(AvatarData data)
    {
      switch (data.hands)
      {
        case -1:
          return (ShrunkImage) null;
        case 1:
          return (ShrunkImage) GFXLibrary.avatar_hands02;
        case 2:
          return (ShrunkImage) GFXLibrary.avatar_hands03;
        case 3:
          return (ShrunkImage) GFXLibrary.avatar_hands04;
        default:
          return (ShrunkImage) GFXLibrary.avatar_hands01;
      }
    }

    public static ShrunkImage getShoulders(AvatarData data)
    {
      switch (data.shoulder)
      {
        case -1:
          return (ShrunkImage) null;
        case 1:
          return (ShrunkImage) GFXLibrary.avatar_shoulders02;
        case 2:
          return (ShrunkImage) GFXLibrary.avatar_shoulders03;
        case 3:
          return (ShrunkImage) GFXLibrary.avatar_shoulders04_front;
        default:
          return (ShrunkImage) GFXLibrary.avatar_shoulders01;
      }
    }

    public static ShrunkImage getFace(AvatarData data)
    {
      switch (data.face)
      {
        case 1:
          return data.male ? (ShrunkImage) GFXLibrary.avatar_face02_male : (ShrunkImage) GFXLibrary.avatar_face04_female;
        case 2:
          return data.male ? (ShrunkImage) GFXLibrary.avatar_face06_male : (ShrunkImage) GFXLibrary.avatar_face05_female;
        case 3:
          return data.male ? (ShrunkImage) GFXLibrary.avatar_face07_male : (ShrunkImage) GFXLibrary.avatar_face06_female;
        case 4:
          return data.male ? (ShrunkImage) GFXLibrary.avatar_face08_male : (ShrunkImage) GFXLibrary.avatar_face08_female;
        case 5:
          return data.male ? (ShrunkImage) GFXLibrary.avatar_face09_male : (ShrunkImage) GFXLibrary.avatar_face09_female;
        case 6:
          return data.male ? (ShrunkImage) GFXLibrary.avatar_face10_male : (ShrunkImage) GFXLibrary.avatar_face10_female;
        case 7:
          return (ShrunkImage) GFXLibrary.avatar_rat_face;
        case 8:
          return (ShrunkImage) GFXLibrary.avatar_snake_face;
        case 9:
          return (ShrunkImage) GFXLibrary.avatar_pig_face;
        case 10:
          return (ShrunkImage) GFXLibrary.avatar_wolf_face;
        default:
          return data.male ? (ShrunkImage) GFXLibrary.avatar_face01_male : (ShrunkImage) GFXLibrary.avatar_face03_female;
      }
    }

    public static ShrunkImage getHair(AvatarData data)
    {
      if ((data.head == 0 || data.head == 1) && data.hair == 0)
        return (ShrunkImage) null;
      switch (data.hair)
      {
        case -1:
          return (ShrunkImage) null;
        case 1:
          return (ShrunkImage) GFXLibrary.avatar_hair02;
        case 2:
          return (ShrunkImage) GFXLibrary.avatar_hair03;
        case 3:
          return (ShrunkImage) GFXLibrary.avatar_hair04;
        case 4:
          return (ShrunkImage) GFXLibrary.avatar_hair05;
        case 5:
          return (ShrunkImage) GFXLibrary.avatar_hair06;
        default:
          return (ShrunkImage) GFXLibrary.avatar_hair01_helmhide;
      }
    }

    public static ShrunkImage getHead(AvatarData data)
    {
      switch (data.head)
      {
        case 0:
          return (ShrunkImage) GFXLibrary.avatar_head01_hairoff;
        case 1:
          return (ShrunkImage) GFXLibrary.avatar_head02_hairoff;
        case 2:
          return (ShrunkImage) GFXLibrary.avatar_head03;
        case 3:
          return (ShrunkImage) GFXLibrary.avatar_head04;
        case 4:
          return (ShrunkImage) GFXLibrary.avatar_head05;
        case 5:
          return (ShrunkImage) GFXLibrary.avatar_head06;
        case 6:
          return (ShrunkImage) GFXLibrary.avatar_head07;
        case 7:
          return (ShrunkImage) GFXLibrary.avatar_head08;
        case 8:
          return (ShrunkImage) GFXLibrary.avatar_head09;
        case 9:
          return (ShrunkImage) GFXLibrary.avatar_head10;
        case 10:
          return (ShrunkImage) GFXLibrary.avatar_head11;
        case 11:
          return (ShrunkImage) GFXLibrary.avatar_head12;
        case 12:
          return (ShrunkImage) GFXLibrary.avatar_rat_helm;
        case 13:
          return (ShrunkImage) GFXLibrary.avatar_wolf_helm;
        default:
          return (ShrunkImage) null;
      }
    }

    public static ShrunkImage getWeapon(AvatarData data)
    {
      switch (data.weapon)
      {
        case 0:
          return (ShrunkImage) GFXLibrary.avatar_weapon01;
        case 1:
          return (ShrunkImage) GFXLibrary.avatar_weapon02;
        case 2:
          return (ShrunkImage) GFXLibrary.avatar_weapon03;
        case 3:
          return (ShrunkImage) GFXLibrary.avatar_weapon04;
        case 4:
          return (ShrunkImage) GFXLibrary.avatar_weapon05;
        case 5:
          return (ShrunkImage) GFXLibrary.avatar_weapon06;
        default:
          return (ShrunkImage) null;
      }
    }

    public static ShrunkImage getBelt(AvatarData data)
    {
      return data.weapon >= 0 ? (ShrunkImage) GFXLibrary.avatar_weapon_belt : (ShrunkImage) null;
    }
  }
}
