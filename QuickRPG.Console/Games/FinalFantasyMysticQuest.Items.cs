namespace QuickRPG.Console.Games;

public partial class FinalFantasyMysticQuest
{
    private string ExtractItemSubType(byte subType)
    {
        var chestItems = new Dictionary<byte, string>();

        void AddItemData(string name, params byte[] chestIds)
        {
            foreach (var chestId in chestIds)
            {
                chestItems.Add(chestId, name);
            }
        }

        //0x06 => $"Steel Shield?",

        AddItemData("Aero",         0x00);
        AddItemData("Aegis Shield", 0x01);
        AddItemData("Blizzard",     0x02);
        AddItemData("Fire",         0x03);

        AddItemData("Cure",         0x05);
        AddItemData("Steel Shield", 0x06);
        AddItemData("Quake",        0x07);
        AddItemData("Sand Coin",    0x08);
        
        AddItemData("Libra Crest",  0x09);

        AddItemData("Heal",         0x0A); // Charm Claw from Mine?
        AddItemData("Knight Sword", 0x0B);
        AddItemData("Noble Armor",  0x0C);
        AddItemData("Magic Mirror", 0x0D);
        AddItemData("River Coin",   0x0E);
        AddItemData("Mobius Crest", 0x0F);
        AddItemData("Charm Claw",   0x10);
        AddItemData("White",        0x11);
        AddItemData("Mask",         0x12);
        AddItemData("Moon Helm",    0x13);
        AddItemData("Sun Coin",     0x14);
        AddItemData("Giant Axe",    0x15);
        AddItemData("Meteor",       0x16);
        AddItemData("Apollo Helm",  0x17);
        AddItemData("Excalibur",    0x18);
        AddItemData("Flare",        0x19);
        AddItemData("Sky Coin",     0x1A);
        AddItemData("Gaia's Armor", 0x1B);
        AddItemData("Life",         0x1C);

        AddItemData("Cure Potion x3", 0x04, 0x1D, 0x1E, 0x22, 0x23, 0x25, 0x27, 0x28, 0x2A, 0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x36, 0x3F, 0x43, 0x48, 0x4C, 0x4E, 0x50);
        AddItemData("Heal Potion x3", 0x20, 0x24, 0x26, 0x29, 0x2B, 0x31, 0x34, 0x41, 0x44, 0x4A);
        AddItemData("Cure Potion x5", 0x21, 0x33, 0x35, 0x37, 0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3E, 0x40, 0x46, 0x47, 0x49, 0x4B, 0x4D, 0x4F);
        AddItemData("Refreshers",     0x1F, 0x32, 0x42, 0x45);
        AddItemData("Seeds",          0x3D);
        //AddItemData("Stars x10",      0x37, 0x38);

        if (chestItems.TryGetValue(subType, out var item))
        {
            return $"{subType:X2} {item}";
        }
        else
        {
            return $"{subType:X2} ?";
        }
    }
}
