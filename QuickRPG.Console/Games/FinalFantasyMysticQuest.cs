namespace QuickRPG.Console.Games;

// https://wiki.superfamicom.org/final-fantasy-mystic-quest#monster-chart
// https://gamefaqs.gamespot.com/boards/532476-final-fantasy-mystic-quest/66150631

public class FinalFantasyMysticQuest
{
    private readonly string _romPath;
    private byte[]? _romData;

    public FinalFantasyMysticQuest(string romPath)
    {
        _romPath = romPath;
    }

    private byte[] RomData
    {
        get
        {
            _romData ??= File.ReadAllBytes(_romPath);
            return _romData;
        }
    }

    public IList<EnemyData> GetEnemiesData()
    {
        var baseOffset = 0x14475;

        EnemyData NextEnemyData(string name)
        {
            var enemyData = ExtractEnemyData(baseOffset, name);
            baseOffset += 14;
            return enemyData;
        }

        return
        [
            NextEnemyData("Brownies"),
            NextEnemyData("MintMint"),
            NextEnemyData("Red Cap"),
            NextEnemyData("Mad Plant"),
            NextEnemyData("Plant Man"),
        ];
    }

    private EnemyData ExtractEnemyData(int offset, string name)
    {
        var hp = RomData[offset + 0];
        var strength = RomData[offset + 2];
        var defense = RomData[offset + 3];
        var speed = RomData[offset + 4];
        var magic = RomData[offset + 5];
        return new EnemyData(name, hp, strength, defense, speed, magic);
    }
}

public record EnemyData(string Name, int HP, int Strength, int Defense, int Speed, int Magic);
