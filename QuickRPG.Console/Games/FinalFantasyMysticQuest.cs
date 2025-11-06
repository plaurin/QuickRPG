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
    private readonly GameConfig _config;
    private readonly string _romPath;
    private byte[]? _originalRomData;
    private byte[]? _romData;
    private readonly List<RomHack> _romHacks;

    private const int BattlefieldBattleLeft = 0x654D0;

    private const int LevelForestMapElements = 0x3B2EE;
    private const int BoneDungeon1FMapElements = 0x3B445;
    private const int BoneDungeonB1MapElements = 0x3B4CC;
    private const int BoneDungeonB2MapElements = 0x3B568;
    
    private const int WintryCaveMapElements = 0x3B760;
    private const int WintryCave2MapElements = 0x3B7EE;
    private const int WintryCave3MapElements = 0x3B883;

    private const int FallBasinMapElements = 0x3B8E0;

    private const int MapElements = 0x3B9FE; // Desert Hag + Lamia

    private const int IcePyramidMapElements = 0x3BA85;
    private const int IcePyramid2MapElements = 0x3BB21;
    private const int IcePyramid3MapElements = 0x3BBC4;
    private const int IcePyramid4MapElements = 0x3BC67;

    private const int MapElements2 = 0x3BCFC; // Sphinx + Gather
    private const int MapElements3 = 0x3BD59; // Sphinx + Gather
    private const int MapElements4 = 0x3BE2E; // Chests 70-73

    private const int MineMapElements = 0x3BF7F;
    private const int MineMap2Elements = 0x3BFD5;
    private const int MineMap3Elements = 0x3C086; // Chests 79-7C 
    
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


    public FinalFantasyMysticQuest(ConfigManager configManager, string romPath, FinalFantasyMysticQuest? originalRom = null)
    {
        _config = configManager.Config;
        _romPath = romPath;
        _originalRomData = originalRom?.RomData;
        if (_originalRomData != null)
        {
            _romData = new byte[_originalRomData.Length];
            _originalRomData.CopyTo(_romData, 0);
        }

        _romHacks =
        [
            new(
                hackName: "Enemies Drops Rate",
                currentValueFunc: () => _config.EnemiesDropRate,
                defaultValue: 3, // TODO read from original rom
                updateValueAction: value => _config.EnemiesDropRate = (byte)value,
                saveConfigAction: configManager.SaveConfig,
                runHackAction: SetEnemiesDropRate),
            new(
                hackName: "Battlefield Battle Count",
                currentValueFunc: () => _config.BattlefieldsBattleCount,
                defaultValue: 10, // TODO read from original rom
                updateValueAction: value => _config.BattlefieldsBattleCount = (byte)value,
                saveConfigAction: configManager.SaveConfig,
                runHackAction: SetBattlefieldBattleCount),
            new(
                hackName: "Level Forst Enemies Gone",
                currentValueFunc: () => _config.LevelForestEnemiesGone,
                defaultValue: false,
                updateValueAction: value => _config.LevelForestEnemiesGone = (bool)value,
                saveConfigAction: configManager.SaveConfig,
                runHackAction: SetBoneDungeonEnemiesGone),
        ];
    }

    private byte[] RomData
    {
        get
        {
            _romData ??= File.ReadAllBytes(_romPath);
            return _romData;
        }
    }

    public IEnumerable<RomHack> RomHacks => _romHacks;

    public IEnumerable<MapElements> GetMapElements(int offset = 0)
    {
        //var levelForestAddress = 0x3B2C1;
        //var boneDungeon = 0x3B43E;

        for (int i = 0; i < 26; i++)
        {
            yield return ExtractMapElements(offset + MineMapElements + i * 7 - 14);
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
            return $"{subType:X2} " + subType switch
            {
                0x06 => $"Steel Shield?",

                0x15 => $"Giant Axe",
                0x16 => $"Meteor",
                0x17 => $"Apollo Helm",
                0x18 => $"Excalibur",
                0x19 => $"Flare",
                
                // Level Forest
                0x28 => $"Cure Potion x3",
                0x29 => $"Heal Potion x3",
                0x2A => $"Cure Potion x3",
                0x2B => $"Heal Potion x3",
                0x2C => $"Cure Potion x3",
                0x2D => $"Cure Potion x3",
                0x2E => $"Cure Potion x3",

                // Bone Dungeon 1F
                0x35 => $"Stars x10",
                0x36 => $"Cure Potion x3",
                0x37 => $"Stars x10",
                0x38 => $"Stars x10",
                0x39 => $"?? B1",
                _ => $"?",
            };
        }

        return $"{subType:X2} Unknown";
    }

    public void SetEnemiesDropRate()
    {
        RomData[0x10962] = _config.EnemiesDropRate;
    }

    public void SetBattlefieldBattleCount()
    {
        for (int i = 0; i < 21; i++)
        {
            RomData[BattlefieldBattleLeft + i] = _config.BattlefieldsBattleCount;
        }
    }

    public void SetLevelForestEnemiesGone()
    {
        if (_config.LevelForestEnemiesGone)
        {
            for (int i = 0; i < 8; i++)
            {
                RomData[LevelForestMapElements + i * 7] = 5;
            }
        }
    }

    public void SetBoneDungeonEnemiesGone()
    {
        if (_config.LevelForestEnemiesGone)
        {
            for (int i = 0; i < 14; i++)
            {
                //RomData[BoneDungeon1FMapElements + i * 7 + 3] = 0x25;
            }

            for (int i = 0; i < 9; i++)
                RomData[BoneDungeonB1MapElements + i * 7 + 3] = 0x05;
        }
    }

    internal void RunHacks()
    {
        if (_originalRomData != null)
        {
            _romData = new byte[_originalRomData.Length];
            _originalRomData.CopyTo(_romData, 0);
        }
        else
            throw new InvalidOperationException("Can only be run on rom hack!");

        foreach (var romHack in _romHacks)
        {
            if (romHack.CurrentValue.GetType() == typeof(bool))
            {
                if ((bool)romHack.CurrentValue)
                {
                    romHack.RunHack();
                }
            }
            else
            {
                if (!romHack.CurrentValue.Equals(romHack.DefaultValue))
                {
                    romHack.RunHack();
                }
            }
        }

        File.WriteAllBytes(_romPath, RomData);
    }
}

public class RomHack(string hackName, Func<object> currentValueFunc, object defaultValue, Action<object> updateValueAction, Action saveConfigAction, Action runHackAction)
{
    public string HackName => hackName;
    public object CurrentValue => currentValueFunc.Invoke();
    public object DefaultValue => defaultValue;

    public void UpdateValue(object value)
    {
        updateValueAction.Invoke(value);
        saveConfigAction.Invoke();
    }

    public void RunHack() => runHackAction.Invoke();
}

public record MapElements(string Name, int X, int Y, string Type, string SubType, int Palette, byte[] Raw);
