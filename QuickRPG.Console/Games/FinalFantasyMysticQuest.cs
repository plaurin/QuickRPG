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

// Init Battlefield battle left $654D0 

// Remove 1st slime LeveL Forest $3B318 = 0 

public class FinalFantasyMysticQuest
{
    private readonly GameConfig _config;
    private readonly string _romPath;
    private byte[]? _originalRomData;
    private byte[]? _romData;
    private readonly List<RomHack> _romHacks;

    private const int BattlefieldBattleLeft = 0x654F0;

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
                hackName: "Level Forst Enemies Gone",
                currentValueFunc: () => _config.LevelForestEnemiesGone,
                defaultValue: false,
                updateValueAction: value => _config.LevelForestEnemiesGone = (bool)value,
                saveConfigAction: configManager.SaveConfig,
                runHackAction: SetLevelForestEnemiesGone),
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

    public IEnumerable<EnemyData> GetEnemiesData()
    {
        var baseOffset = 0x14275;

        EnemyData NextEnemyData(string name, int dropsLocation)
        {
            var enemyData = ExtractEnemyData(baseOffset, name, dropsLocation);
            baseOffset += 14;
            return enemyData;
        }

        return
        [
            NextEnemyData("Brownie", 0x2C17D),
            NextEnemyData("MintMint", 0x0),
            NextEnemyData("Red Cap", 0x2C183),
            NextEnemyData("Mad Plant", 0x0),
            NextEnemyData("Plant Man", 0x0),
            NextEnemyData("Live Oak", 0x0),
            NextEnemyData("Slime", 0x0),
            NextEnemyData("Jelly", 0x0),
            NextEnemyData("Ooze", 0x0),
            NextEnemyData("Poison Toad", 0x0),
            NextEnemyData("Giant Toad", 0x0),
            NextEnemyData("Mad Toad", 0x0),
            NextEnemyData("Basilisk", 0x0),
            NextEnemyData("Flazzard", 0x0),
            NextEnemyData("Salamand", 0x0),
            NextEnemyData("Sand Worm", 0x0),
            NextEnemyData("Land Worm", 0x0),
            NextEnemyData("Leech", 0x0),
            NextEnemyData("Skeleton", 0x0),
            NextEnemyData("Red Bone", 0x0),
            NextEnemyData("Skulder", 0x0),
            NextEnemyData("Roc", 0x0),
            NextEnemyData("Sparna", 0x0),
            NextEnemyData("Garuda", 0x0),
            NextEnemyData("Zombie", 0x0),
            NextEnemyData("Mummy", 0x0),
            NextEnemyData("Desert Hag", 0x0),
            NextEnemyData("Water Hag", 0x0),
            NextEnemyData("Ninja", 0x0),
            NextEnemyData("Shadow", 0x0),
            NextEnemyData("Sphinx", 0x0),
            NextEnemyData("Manticor", 0x0),
            NextEnemyData("Centaur", 0x0),
            NextEnemyData("NiteMare", 0x0),
            NextEnemyData("Stooney Roost", 0x0),
            NextEnemyData("Hot Wings", 0x0),
            NextEnemyData("Ghost", 0x0),
            NextEnemyData("Spector", 0x0),
            NextEnemyData("Gather", 0x0),
            NextEnemyData("Beholder", 0x0),
            NextEnemyData("Fangpire", 0x0),
            NextEnemyData("Vampire", 0x0),
            NextEnemyData("Mage", 0x0),
            NextEnemyData("Sorcerer", 0x0),
            NextEnemyData("Land Turtle", 0x0),
            NextEnemyData("Adamant Turtle", 0x0),
            NextEnemyData("Scorpion", 0x0),
            NextEnemyData("Snipion", 0x0),
            NextEnemyData("Werewolf", 0x0),
            NextEnemyData("Cerberus", 0x0),
            NextEnemyData("EdgeHog", 0x0),
            NextEnemyData("Sting Rat", 0x0),
            NextEnemyData("Lamia", 0x0),
            NextEnemyData("Naga", 0x0),
            NextEnemyData("Avizzard", 0x0),
            NextEnemyData("Gargoyle", 0x0),
            NextEnemyData("Gorgon", 0x0),
            NextEnemyData("Minotaur Zombie", 0x0),
            NextEnemyData("Phanquid", 0x0),
            NextEnemyData("Freezer Crab", 0x0),
            NextEnemyData("Iflyte", 0x0),
            NextEnemyData("Stheno", 0x0),
            NextEnemyData("Chimera", 0x0),
            NextEnemyData("Thanatos", 0x0),
            NextEnemyData("Stone Golem", 0x0),
            NextEnemyData("Skullerus Rex", 0x0),
            NextEnemyData("Behemoth", 0x0),
            NextEnemyData("Minotaur", 0x0),
            NextEnemyData("Squiddle", 0x0),
            NextEnemyData("Snow Crab", 0x0),
            NextEnemyData("Jinn", 0x0),
            NextEnemyData("Medusa", 0x0),
            NextEnemyData("Gidrah", 0x0),
            NextEnemyData("Dullahan", 0x0),
            NextEnemyData("Flamerus Rex", 0x0),
            NextEnemyData("Ice Golem", 0x0),
            NextEnemyData("Dual Headed Hydra", 0x0),
            NextEnemyData("Twin Headed Hydra", 0x0),
            NextEnemyData("Pazuza", 0x0),
            NextEnemyData("Zuh", 0x0),
            NextEnemyData("Dark King", 0x0),
        ];
    }

    private EnemyData ExtractEnemyData(int offset, string name, int dropsLocation)
    {
        var hp = RomData[offset + 0] + RomData[offset + 1] * 256;
        var strength = RomData[offset + 2];
        var defense = RomData[offset + 3];
        var speed = RomData[offset + 4];
        var magic = RomData[offset + 5];

        var strongElement1 = RomData[offset + 6];
        var strongElement2 = RomData[offset + 7];
        // x1000_0000 x0000_0000 Earth
        // x0100_0000 x0000_0000 Fire
        // x0000_0000 x1000_0000 Water
        // x0000_0000 x0100_0000 Petrify
        // x0000_0000 x0010_0000 Paralysis
        // x0000_0000 x0000_1000 Immune Conf+Fatal
        // x0000_0000 x0000_0100 Poison
        // x0000_0000 x0000_0010 Bind
        // x0000_0000 x0000_0001 Sleep

        var weakElement = RomData[offset + 12];
        // Earth 0x1000_0000
        // Ice   0x0100_0000
        // Fire  0x0010_0000
        // Wind  0x0001_0000
        // Cure  0x0000_1000
        // Axe   0x0000_0100
        // Bomb  0x0000_0010
        // Shoot 0x0000_0001

        var xp = RomData[dropsLocation] * 3;
        var gil = RomData[dropsLocation + 1] * 3;

        if (dropsLocation == 0)
        {
            xp = 0;
            gil = 0;
        }

        var raw = new byte[14];
        for (int i = 0; i < 14; i++)
        {
            raw[i] = RomData[offset + i];
        }

        return new EnemyData(name, hp, strength, defense, speed, magic,
            strongElement1, strongElement2, weakElement, xp, gil, raw);
    }

    public IEnumerable<MapElements> GetMapElements()
    {
        //var levelForestAddress = 0x3B2C1;
        var boneDungeon = 0x3B43E;

        for (int i = 0; i < 26; i++)
        {
            yield return ExtractMapElements(boneDungeon + i * 7);
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
            return $"{subType:X2} " + subType switch
            {
                0x15 => $"Brownie",
                0x16 => $"Slime",
                0x18 => $"Poison Toad",
                0x19 => $"Basilisk",
                0x1A => $"Sand Worm",
                _ => $"?",
            };
        }

        if (type == 0x13)
        {
            return $"{subType:X2} " + subType switch
            {
                0x15 => $"Giant Axe",
                0x16 => $"Meteor",
                0x17 => $"Apollo Helm",
                0x18 => $"Excalibur",
                0x19 => $"Flare",
                
                0x28 => $"Cure Potion x3",
                0x29 => $"Heal Potion x3",
                0x2A => $"Cure Potion x3",
                0x2B => $"Heal Potion x3",
                0x2C => $"Cure Potion x3",
                0x2D => $"Cure Potion x3",
                0x2E => $"Cure Potion x3",

                // Bone Dungeon
                0x35 => $"Stars x10",
                0x36 => $"Cure Potion x3",
                0x37 => $"Stars x10",
                _ => $"?",
            };
        }

        return $"{subType:X2} Unknown";
    }

    public void SetEnemiesDropRate()
    {
        RomData[0x10962] = _config.EnemiesDropRate;
        File.WriteAllBytes(_romPath, RomData);
    }

    public void SetLevelForestEnemiesGone()
    {
        if (_config.LevelForestEnemiesGone)
        {
            for (int i = 0; i < 8; i++)
            {
                RomData[0x3B2EE + i * 7] = 5;
            }

            File.WriteAllBytes(_romPath, RomData);
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

public record EnemyData(string Name, int HP, int Strength, int Defense, int Speed, int Magic,
    int StrongElement1, int StrongElement2, int WeakElement, int XP, int Gil, byte[] Raw);

public record MapElements(string Name, int X, int Y, string Type, string SubType, int Palette, byte[] Raw);
