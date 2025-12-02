namespace QuickRPG.Console.Configs;

public class GameConfig
{
    public string RomPath { get; set; } = string.Empty;
    public string RomHackPath { get; set; } = string.Empty;

    public byte EnemiesDropRate { get; set; } = 3;
    public byte BattlefieldsBattleCount { get; set; } = 10;
    public bool LevelForestEnemiesGone { get; set; } = false;
    public float EnemiesEncounterRate { get; set; } = 1.0f;
}
