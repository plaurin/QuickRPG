namespace QuickRPG.Console.Games;

public partial class FinalFantasyMysticQuest
{
    public  IEnumerable<EnemyData> GetEnemiesData()
    {
        var baseOffset = 0x14275;

        EnemyData NextEnemyData(int dropsLocation, string name, params byte[] enemyGroupIds)
        {
            var enemyData = ExtractEnemyData(baseOffset, name, dropsLocation, enemyGroupIds);
            baseOffset += 14;
            return enemyData;
        }

        return
        [
            NextEnemyData(0x2C17D, "Brownie",     0x15),
            NextEnemyData(0x00000, "MintMint",    0x03),
            NextEnemyData(0x2C183, "Red Cap",     0x0F, 0x54),
            NextEnemyData(0x00000, "Mad Plant",   0x00),
            NextEnemyData(0x00000, "Plant Man",   0x0D),
            NextEnemyData(0x00000, "Live Oak",    0x83, 0x84),
            NextEnemyData(0x00000, "Slime",       0x16),
            NextEnemyData(0x00000, "Jelly",       0x0B),
            NextEnemyData(0x00000, "Ooze",        0x8D, 0x8F),
            NextEnemyData(0x00000, "Poison Toad", 0x01, 0x18),
            NextEnemyData(0x00000, "Giant Toad",  0x04),
            NextEnemyData(0x00000, "Mad Toad",    0x86, 0x87, 0x89),
            NextEnemyData(0x00000, "Basilisk",    0x19),
            NextEnemyData(0x00000, "Flazzard",    0x0E, 0x53),
            NextEnemyData(0x00000, "Salamand",    0x6F, 0x79, 0x7B),
            NextEnemyData(0x00000, "Sand Worm",   0x1A, 0x1C),
            NextEnemyData(0x00000, "Land Worm",   0x24, 0x27, 0x2A),
            NextEnemyData(0x00000, "Leech",       0x8A, 0x8B, 0x8C),
            NextEnemyData(0x00000, "Skeleton",    0x1D, 0x1F),
            NextEnemyData(0x00000, "Red Bone",    0x51, 0x56, 0x57),
            NextEnemyData(0x00000, "Skulder",     0x12, 0x8E, 0x90, 0x92),
            NextEnemyData(0x00000, "Roc",         0x1B),
            NextEnemyData(0x00000, "Sparna",      0x2D, 0x30),
            NextEnemyData(0x00000, "Garuda",      0x97, 0x9A, 0xA0),
            NextEnemyData(0x00000, "Zombie",      0x50, 0x55),
            NextEnemyData(0x00000, "Mummy",       0x7E, 0x7F, 0x81),
            NextEnemyData(0x00000, "Desert Hag",  0x07, 0x32, 0x35, 0x36, 0x39, 0x41),
            NextEnemyData(0x00000, "Water Hag",   0x13, 0x93),
            NextEnemyData(0x00000, "Ninja",       0x62, 0x66, 0x69, 0x6D, 0x70),
            NextEnemyData(0x00000, "Shadow",      0xBE, 0xBF, 0xC0, 0xC2),
            NextEnemyData(0x00000, "Sphinx",      0x4B, 0x4E),
            NextEnemyData(0x00000, "Manticor",    0x9C, 0xA4, 0xA5, 0xB0),
            NextEnemyData(0x00000, "Centaur",     0x25, 0x28, 0x2B, 0x2E),
            NextEnemyData(0x00000, "Nitemare",    0x59, 0x5C, 0x5F),
            NextEnemyData(0x00000, "Stooney Roost", 0x3F, 0x42, 0x49),
            NextEnemyData(0x00000, "Hot Wings",   0x61, 0x65, 0x6E),
            NextEnemyData(0x00000, "Ghost",       0x10, 0x58, 0x5B),
            NextEnemyData(0x00000, "Spector",     0x80, 0x82),
            NextEnemyData(0x00000, "Gather",      0x43, 0x46, 0x4A, 0x4C, 0x4d),
            NextEnemyData(0x00000, "Beholder",    0x98, 0x9B, 0x9F),
            NextEnemyData(0x00000, "Fangpire",    0x6A, 0x71, 0x73, 0x74, 0x76),
            NextEnemyData(0x00000, "Vampire",     0x94, 0x95),
            NextEnemyData(0x00000, "Mage",        0x3C, 0x3E, 0x45),
            NextEnemyData(0x00000, "Sorcerer",    0xA1, 0xA6, 0xA8, 0xAC, 0xAE),
            NextEnemyData(0x00000, "Land Turtle", 0x29, 0x2C, 0x2F),
            NextEnemyData(0x00000, "Adamant Turtle", 0x6B, 0x75, 0x78),
            NextEnemyData(0x00000, "Scorpion",    0x05, 0x22),
            NextEnemyData(0x00000, "Snipion",     0x85, 0x88),
            NextEnemyData(0x00000, "Werewolf",    0x5A, 0x5D, 0x60),
            NextEnemyData(0x00000, "Cerberus",    0xC6, 0xCA),
            NextEnemyData(0x00000, "EdgeHog",     0x06, 0x23, 0x26),
            NextEnemyData(0x00000, "Sting Rat",   0x0C),
            NextEnemyData(0x00000, "Lamia",       0x08, 0x33, 0x37, 0x38, 0x3A, 0x3B, 0x3D),
            NextEnemyData(0x00000, "Naga",        0xA9, 0xB1, 0xB5, 0xB8),
            NextEnemyData(0x00000, "Avizzard",    0x63, 0x67, 0x72, 0x77, 0x7A, 0x7C),
            NextEnemyData(0x00000, "Gargoyle",    0xAD, 0xB2, 0xB9, 0xBB, 0xBC),
            NextEnemyData(0x00000, "Gorgon",      0x1E),
            NextEnemyData(0x00000, "Minotaur Zombie", 0x02, 0x20),
            NextEnemyData(0x00000, "Phanquid",    0x09, 0x40, 0xC3),
            NextEnemyData(0x00000, "Freezer Crab", 0x44, 0xC4),
            NextEnemyData(0x00000, "Iflyte",      0x11, 0x64, 0x6C, 0xC7, 0xC9),
            NextEnemyData(0x00000, "Stheno",      0x68, 0xC8),
            NextEnemyData(0x00000, "Chimera",     0x9D, 0xAB, 0xB6, 0xBA, 0xCB),
            NextEnemyData(0x00000, "Thanatos",    0xA2, 0xA7, 0xB7, 0xBD, 0xCC),
            NextEnemyData(0x00000, "Stone Golem", 0xC5),
            NextEnemyData(0x00000, "Skullerus Rex", 0xC1),
            NextEnemyData(0x00000, "Behemoth",    0x14),
            NextEnemyData(0x00000, "Minotaur",    0x17),
            NextEnemyData(0x00000, "Squiddle",    0x31),
            NextEnemyData(0x00000, "Snow Crab",   0x0A, 0x34),
            NextEnemyData(0x00000, "Jinn",        0x52),
            NextEnemyData(0x00000, "Medusa",      0x5E),
            NextEnemyData(0x00000, "Gidrah",      0x91),
            NextEnemyData(0x00000, "Dullahan",    0x96),
            NextEnemyData(0x00000, "Flamerus Rex", 0x21),
            NextEnemyData(0x00000, "Ice Golem",   0x4F),
            NextEnemyData(0x00000, "Dual Headed Hydra", 0x7D),
            NextEnemyData(0x00000, "Twin Headed Hydra", 0xC9),
            NextEnemyData(0x00000, "Pazuza",      0x99, 0x9E, 0xA3, 0xAA, 0xAF, 0xB4),
            NextEnemyData(0x00000, "Zuh",         0xCD),
            NextEnemyData(0x00000, "Dark King",   0xCE),
        ];
    }

    private EnemyData ExtractEnemyData(int offset, string name, int dropsLocation, byte[] enemyGroupIds)
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
            strongElement1, strongElement2, weakElement, xp, gil, enemyGroupIds, raw);
    }

    private string ExtractEnemySubType(byte subType)
    {
        var enemyData = GetEnemiesData().FirstOrDefault(ed => ed.EnemyGroupsIds.Contains(subType));
        if (enemyData != null)
        {
            return $"{subType:X2} {enemyData.Name}";
        }
        else
        {
            return $"{subType:X2} ?";
        }
    }
}

public record EnemyData(string Name, int HP, int Strength, int Defense, int Speed, int Magic,
    int StrongElement1, int StrongElement2, int WeakElement, int XP, int Gil, byte[] EnemyGroupsIds, byte[] Raw);
