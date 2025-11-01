using Tomlyn;

namespace QuickRPG.Console.Configs;

public class ConfigManager
{
    private readonly GameConfig _config;

    public ConfigManager()
    {
        var configFile = File.ReadAllText("config.toml");
        _config = Toml.ToModel<GameConfig>(configFile);
    }

    public GameConfig Config => _config;

    public void SaveConfig()
    {
        var tomlConfig = Toml.FromModel(_config);
        File.WriteAllText("config.toml", tomlConfig);
    }
}
