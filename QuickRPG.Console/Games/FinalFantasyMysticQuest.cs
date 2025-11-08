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
