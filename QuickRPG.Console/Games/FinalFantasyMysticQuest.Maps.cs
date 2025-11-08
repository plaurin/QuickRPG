using QuickRPG.Console.Configs;

namespace QuickRPG.Console.Games;

// https://wiki.superfamicom.org/final-fantasy-mystic-quest#monster-chart
// https://gamefaqs.gamespot.com/boards/532476-final-fantasy-mystic-quest/66150631
// https://datacrystal.tcrf.net/wiki/Final_Fantasy:_Mystic_Quest/RAM_map
// https://finalfantasy.fandom.com/wiki/Final_Fantasy_Mystic_Quest_enemies
// https://mikesrpgcenter.com/ffmq/bestiary.html
// https://gamefaqs.gamespot.com/snes/532476-final-fantasy-mystic-quest/faqs/39031

// AddGainedXPMaybe $1096F
// IncXP_X $2874
// IncXP_Y $289B

// XPTooMaybe $3369
// CheckMaybeXP $009E
// Experience $1011
// GainXpMaybe $04A0

// DropMultiply $10962

// Level Forest Chest $0ECD-0ECE

// $0C40-$0DF0 Sprite Animation?

// Remove 1st slime LeveL Forest $3B318 = 0 

public partial class FinalFantasyMysticQuest
{
    private const int LevelForestMapElements = 0x3B2EB;

    private const int BoneDungeon1FMapElements = 0x3B445;
    private const int BoneDungeonB1MapElements = 0x3B4CC;
    private const int BoneDungeonB2MapElements = 0x3B568;
    
    private const int WintryCaveMapElements = 0x3B760;
    private const int WintryCave2MapElements = 0x3B7EE;
    private const int WintryCave3MapElements = 0x3B883;

    private const int FallBasinMapElements = 0x3B8E0;

    private const int MapElements = 0x3B9FE;// Desert Hag + Lamia

    private const int IcePyramidMapElements = 0x3BA85;
    private const int IcePyramid2MapElements = 0x3BB21;
    private const int IcePyramid3MapElements = 0x3BBC4;
    private const int IcePyramid4MapElements = 0x3BC67;

    private const int MapElements2 = 0x3BCFC;// Sphinx + Gather
    private const int MapElements3 = 0x3BD59;// Sphinx + Gather
    private const int MapElements4 = 0x3BE2E;// Chests 70-73

    private const int MineMapElements = 0x3BF7F;
    private const int MineMap2Elements = 0x3BFD5;
    private const int MineMap3Elements = 0x3C086;// Chests 79-7C 
    
    private const int VolcanoMapElements = 0x3C0EC;
    private const int VolcanoMap2Elements = 0x3C16C;
    private const int VolcanoMap3Elements = 0x3C1EC;
    private const int VolcanoMap4Elements = 0x3C22D;
    private const int VolcanoMap5Elements = 0x3C2C2;
    private const int VolcanoMap6Elements = 0x3C365;
    private const int VolcanoMap7Elements = 0x3C401;
    private const int VolcanoMap8Elements = 0x3C488;
    private const int VolcanoMap9Elements = 0x3C51D;
    private const int VolcanoMap10Elements = 0x3C5B2;
    private const int VolcanoMap11Elements = 0x3C624;
    
    private const int MapElements5 = 0x3C683;

    private const int AliveForestMapElements = 0x3C6B6;
    private const int AliveForestMap2Elements = 0x3C73C;

    private const int GiantTreeMapElements = 0x3CA1E;
    
    private const int MountGaleMapElements = 0x3CB7B;
    
    private const int MapElements6 = 0x3CDBF;
    
    private const int PazuzuTowerMapElements = 0x3CE95;
    private const int PazuzuTowerMap2Elements = 0x3CF2A;
    private const int PazuzuTowerMap3Elements = 0x3CFCD;
    private const int PazuzuTowerMap4Elements = 0x3D069;
    private const int PazuzuTowerMap5Elements = 0x3D0F5;
    private const int PazuzuTowerMap6Elements = 0x3D1A8;
    
    private const int MapElements7 = 0x3D256;
    private const int MapElements8 = 0x3D274;
    private const int MapElements9 = 0x3D292;
    
    private const int MacsShipMapElements = 0x3D301;
    private const int MacsShipMap2Elements = 0x3D39D;
    private const int MacsShipMap3Elements = 0x3D439;

    public IEnumerable<MapData> GetMapsData()
    {
        yield return new MapData("Level Forest", LevelForestMapElements, 25);
        
        yield return new MapData("Bone Dungeon 1F", BoneDungeon1FMapElements, 17);
        yield return new MapData("Bone Dungeon B1", BoneDungeonB1MapElements, 25);
        yield return new MapData("Bone Dungeon B2", BoneDungeonB2MapElements, 25);
        
        yield return new MapData("Wintry Cave 1", WintryCaveMapElements, 25);
        yield return new MapData("Wintry Cave 2", WintryCave2MapElements, 25);
        yield return new MapData("Wintry Cave 3", WintryCave3MapElements, 25);
        
        yield return new MapData("Fall Basin", FallBasinMapElements, 25);
        
        yield return new MapData("Unknown Map 1", MapElements, 25);

        yield return new MapData("Ice Pyramid 1", IcePyramidMapElements, 25);
        yield return new MapData("Ice Pyramid 2", IcePyramid2MapElements, 25);
        yield return new MapData("Ice Pyramid 3", IcePyramid3MapElements, 25);
        yield return new MapData("Ice Pyramid 4", IcePyramid4MapElements, 25);

        yield return new MapData("Unknown Map 2", MapElements2, 25);
        yield return new MapData("Unknown Map 3", MapElements3, 25);
        yield return new MapData("Unknown Map 4", MapElements4, 25);
        
        yield return new MapData("Mine 1", MineMapElements, 25);
        yield return new MapData("Mine 2", MineMap2Elements, 25);
        yield return new MapData("Mine 3", MineMap3Elements, 25);

        yield return new MapData("Volcano 1", VolcanoMapElements, 25);
        yield return new MapData("Volcano 2", VolcanoMap2Elements, 25);
        yield return new MapData("Volcano 3", VolcanoMap3Elements, 25);
        yield return new MapData("Volcano 4", VolcanoMap4Elements, 25);
        yield return new MapData("Volcano 5", VolcanoMap5Elements, 25);
        yield return new MapData("Volcano 6", VolcanoMap6Elements, 25);
        yield return new MapData("Volcano 7", VolcanoMap7Elements, 25);
        yield return new MapData("Volcano 8", VolcanoMap8Elements, 25);
        yield return new MapData("Volcano 9", VolcanoMap9Elements, 25);
        yield return new MapData("Volcano 10", VolcanoMap10Elements, 25);
        yield return new MapData("Volcano 11", VolcanoMap11Elements, 25);

        yield return new MapData("Unknown Map 5", MapElements5, 25);
        
        yield return new MapData("Alive Forest 1", AliveForestMapElements, 25);
        yield return new MapData("Alive Forest 2", AliveForestMap2Elements, 25);
        
        yield return new MapData("Giant Tree", GiantTreeMapElements, 25);
        
        yield return new MapData("Mount Gale", MountGaleMapElements, 25);

        yield return new MapData("Unknown Map 6", MapElements6, 25);

        yield return new MapData("Pazuzu Tower 1", PazuzuTowerMapElements, 25);
        yield return new MapData("Pazuzu Tower 2", PazuzuTowerMap2Elements, 25);
        yield return new MapData("Pazuzu Tower 3", PazuzuTowerMap3Elements, 25);
        yield return new MapData("Pazuzu Tower 4", PazuzuTowerMap4Elements, 25);
        yield return new MapData("Pazuzu Tower 5", PazuzuTowerMap5Elements, 25);
        yield return new MapData("Pazuzu Tower 6", PazuzuTowerMap6Elements, 25);

        yield return new MapData("Unknown Map 7", MapElements7, 25);
        yield return new MapData("Unknown Map 8", MapElements8, 25);
        yield return new MapData("Unknown Map 9", MapElements9, 25);

        yield return new MapData("Mac's Ship 1", MacsShipMapElements, 25);
        yield return new MapData("Mac's Ship 2", MacsShipMap2Elements, 25);
        yield return new MapData("Mac's Ship 3", MacsShipMap3Elements, 25);
    }

    public IEnumerable<MapElements> GetMapElements(int offset = 0)
    {
        for (int i = 0;i < 26;i++)
        {
            yield return ExtractMapElements(offset + i * 7 - 14);
        }
    }

    private MapElements ExtractMapElements(int offset)
    {
        var endOffset = offset + 7;

        var subType = RomData[offset + 1];
        var mapY = RomData[offset + 2];
        var mapX = RomData[offset + 3];
        var palette = RomData[offset + 4];
        var type = RomData[offset + 5];
        var raw = RomData[offset..endOffset];

        return new MapElements($"{offset:X}", ExtractMapX(type, mapX), ExtractMapY(type, mapY), ExtractMapElementType(type), ExtractMapElementSubType(type, subType), palette, raw);
    }

    private byte ExtractMapX(byte type, byte x)
    {
        return type == 0x13 ? (byte)(x - 0x00) : x;
    }

    private byte ExtractMapY(byte type, byte y)
    {
        return type == 0x13 ? (byte)(y - 0x40) : y;
    }

    private string ExtractMapElementType(byte type)
    {
        return $"{type:X2} " + type switch
        {
            0x0B => "Enemy",
            0x13 => "Chest",
            _ => $"?",
        };
    }

    private string ExtractMapElementSubType(byte type, byte subType)
    {
        if (type == 0x0B)
        {
            return ExtractEnemySubType(subType);
        }

        if (type == 0x13)
        {
            return ExtractItemSubType(subType);
        }

        return $"{subType:X2} Unknown";
    }
}

public record MapData(string Name, int ElementsOffset, int ElementsCount);

public record MapElements(string Name, int X, int Y, string Type, string SubType, int Palette, byte[] Raw);
