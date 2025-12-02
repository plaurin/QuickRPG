namespace QuickRPG.Console.Games;

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
        yield return new MapData("Level Forest", 0x3B2EB);
        
        yield return new MapData("Bone Dungeon 1F", 0x3B445);
        yield return new MapData("Bone Dungeon B1", 0x3B4CC);
        yield return new MapData("Bone Dungeon B2", 0x3B568);
        yield return new MapData("Bone Dungeon B2-2", 0x3B619);
        
        yield return new MapData("Wintry Cave 1F", 0x3B760);
        yield return new MapData("Wintry Cave 2F", 0x3B7EE);
        yield return new MapData("Wintry Cave 3F", 0x3B883);
        yield return new MapData("Wintry Cave 3F-2", 0x3B8E0);
            
        yield return new MapData("Fall Basin", 0x3B9A1);
        
        yield return new MapData("Ice Pyramid B1", 0x3B9FE);
        yield return new MapData("Ice Pyramid 1F", 0x3BA85);
        yield return new MapData("Ice Pyramid 2F", 0x3BB21);
        yield return new MapData("Ice Pyramid 3F", 0x3BBC4);
        yield return new MapData("Ice Pyramid 4F", 0x3BC67);
        yield return new MapData("Ice Pyramid 5F", 0x3BCFC);
        yield return new MapData("Ice Pyramid B1-2", 0x3BD59);

        yield return new MapData("Unknown Map 4", 0x3BE2E);
        
        yield return new MapData("Mine 1", 0x3BF7F);
        yield return new MapData("Mine 2", 0x3BFD5);
        yield return new MapData("Mine 3", 0x3C086);

        yield return new MapData("Volcano 1", 0x3C0EC);
        yield return new MapData("Volcano 3", 0x3C16C);
        yield return new MapData("Volcano 2", 0x3C1EC);

        yield return new MapData("Lava Dome 1", 0x3C22D); // Main
        yield return new MapData("Lava Dome 2", 0x3C2C2); // NOQ
        yield return new MapData("Lava Dome 3", 0x3C365); // IJ
        yield return new MapData("Lava Dome 4", 0x3C3F3); // EJKL
        yield return new MapData("Lava Dome 5", 0x3C488); // BC + A
        yield return new MapData("Lava Dome 6", 0x3C51D); // DEFHI
        yield return new MapData("Lava Dome 7", 0x3C5B2); // OP + FG
        yield return new MapData("Lava Dome 8", 0x3C624); // RS

        yield return new MapData("Rope Bridge", 0x3C683);
        
        yield return new MapData("Alive Forest", 0x3C6B6);

        yield return new MapData("Giant Tree 1F", 0x3C744);
        yield return new MapData("Giant Tree 2F", 0x3C7E7);
        yield return new MapData("Giant Tree 2F-2", 0x3C88A);
        yield return new MapData("Giant Tree 3F", 0x3C8CB);
        yield return new MapData("Giant Tree 3F-2", 0x3C96E);
        yield return new MapData("Giant Tree 4F", 0x3C9D9);
        yield return new MapData("Giant Tree 4F-2", 0x3CA7C);
        yield return new MapData("Giant Tree 5F", 0x3CAD9);

        yield return new MapData("Mount Gale", 0x3CB7B);

        yield return new MapData("Pazuzu Tower 1F", 0x3CE07);
        yield return new MapData("Pazuzu Tower 1F-2", 0x3D21A);
        yield return new MapData("Pazuzu Tower 2F-2", 0x3D274);
        yield return new MapData("Pazuzu Tower 2F", 0x3CE95);
        yield return new MapData("Pazuzu Tower 3F", 0x3CF2A);
        yield return new MapData("Pazuzu Tower 3F-2", 0x3D238);
        yield return new MapData("Pazuzu Tower 4F-2", 0x3D292);
        yield return new MapData("Pazuzu Tower 4F", 0x3CFCD);
        yield return new MapData("Pazuzu Tower 5F", 0x3D069);
        yield return new MapData("Pazuzu Tower 5F-2", 0x3D256);
        yield return new MapData("Pazuzu Tower 6F", 0x3D0FE);
        yield return new MapData("Pazuzu Tower 7F", 0x3D1A8);

        yield return new MapData("Mac's Ship Deck", 0x3D301);
        yield return new MapData("Mac's Ship B1", 0x3D39D);
        yield return new MapData("Mac's Ship B2", 0x3D439);
        yield return new MapData("Mac's Ship B2-2", 0x3D4FF);

        yield return new MapData("Doom Castle B2", 0x3B09F);
        yield return new MapData("Doom Castle 4F", 0x3D565);
        yield return new MapData("Doom Castle 5F", 0x3D5FA);
        yield return new MapData("Doom Castle 6F", 0x3D688);
        yield return new MapData("Doom Castle 7F", 0x3D77A);
    }

    public int ExtractMapEnemiesCount(int offset = 0) => ExtractMapElements(offset).Count(me => me.Type.Contains("Enemy"));
    public int ExtractMapChestsCount(int offset = 0) => ExtractMapElements(offset).Count(me => me.Type.Contains("Chest"));
    public int ExtractMapUniquesCount(int offset = 0) => ExtractMapElements(offset).Count(me => me.Type.Contains("Uniqu"));
    public int ExtractMapDropsCount(int offset = 0) => ExtractMapElements(offset).Count(me => me.Type.Contains("Drop"));

    public IEnumerable<MapElement> ExtractMapElements(int offset)
    {
        while (true)
        {
            var mapElement = ExtractMapElement(offset);
            if (mapElement.Type.Contains("?")) break;
            yield return mapElement;
            offset += 7;
        }
    }

    public IEnumerable<MapElement> ExtractMapElements(int offset, int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return ExtractMapElement(offset + i * 7);
        }
    }

    private MapElement ExtractMapElement(int offset)
    {
        var endOffset = offset + 7;

        var unique = RomData[offset + 0];
        var subType = RomData[offset + 1];
        var mapY = RomData[offset + 2];
        var mapX = RomData[offset + 3];
        var palette = RomData[offset + 4];
        var type = RomData[offset + 5];
        var raw = RomData[offset..endOffset];

        return new MapElement(
            offset,
            $"{offset:X}",
            ExtractMapX(type, mapX),
            ExtractMapY(type, mapY),
            ExtractMapElementType(type, unique, subType),
            ExtractMapElementSubType(type, subType),
            palette,
            raw);
    }

    private static bool IsEnemy(byte type) => type == 0x0B || type == 0x09 || type == 0x0C || type == 0x08 || type == 0x0A || type == 0x0E;
    private static bool IsChest(byte type) => type == 0x13 || type == 0x11 || type == 0x14 || type == 0x12 || type == 0x16;
    private static bool IsObject(byte type, byte unique) => type == 0x03 && unique == 0xFE;
    private static bool IsDrop(byte type) => type == 0x1B;

    private byte ExtractMapX(byte type, byte x)
    {
        return IsChest(type) || IsDrop(type) ? (byte)(x - 0x00) : x;
    }

    private byte ExtractMapY(byte type, byte y)
    {
        return IsChest(type) || IsDrop(type) ? (byte)(y - 0x40) : y;
    }

    private string ExtractMapElementType(byte type, byte unique, byte subType)
    {
        if (IsChest(type) && unique - subType == 0xA9) return $"{type:X2} Uniqu";
        if (IsChest(type) && unique != 0x00) return $"{type:X2} Uniq?";
        if (IsChest(type) && unique == 0x00) return $"{type:X2} Chest";
        if (IsEnemy(type) && (unique == 0x00 || unique == 0x28 || unique == 0x58)) return $"{type:X2} Enemy";
        if (IsObject(type, unique)) return $"{type:X2} Obj";
        if (IsDrop(type) && unique == 0x00) return $"{type:X2} Drop";
        return $"{type:X2} ?";
    }

    private string ExtractMapElementSubType(byte type, byte subType)
    {
        if (IsEnemy(type))
        {
            return ExtractEnemySubType(subType);
        }

        if (IsChest(type))
        {
            return ExtractItemSubType(subType);
        }

        return $"{subType:X2} Unknown";
    }
}

public record MapData(string Name, int ElementsOffset);

public record MapElement(int Offset, string Name, int X, int Y, string Type, string SubType, int Palette, byte[] Raw)
{
    public bool IsEnemy => Type.EndsWith("Enemy");
    public bool IsChest => Type.EndsWith("Chest");
    public bool IsUnique => Type.EndsWith("Unique");
    public bool IsDrop => Type.EndsWith("Drop");
}
