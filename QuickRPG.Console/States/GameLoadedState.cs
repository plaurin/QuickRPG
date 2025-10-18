using QuickRPG.Console.Rendering;
using Spectre.Console;
using Spectre.Console.Rendering;
using Stateless;
using Tomlyn;

namespace QuickRPG.Console.States;

public class GameLoadedState
{
    private readonly Navigation _navigation;

    public GameLoadedState(Navigation navigation)
    {
        _navigation = navigation;

        LoadGameTrigger = _navigation.StateMachine.SetTriggerParameters<string>(NavigationTriggers.LoadGame);

        _navigation.StateMachine.Configure(NavigationStates.GameLoaded)
            .OnEntryFrom(LoadGameTrigger, Enter)
            .OnEntryFrom(NavigationTriggers.CloseGallery, Enter)
            .Permit(NavigationTriggers.ChangeGame, NavigationStates.NoGameLoaded)
            .Permit(NavigationTriggers.OpenGallery, NavigationStates.GalleryMain);
    }

    public StateMachine<NavigationStates, NavigationTriggers>.TriggerWithParameters<string> LoadGameTrigger { get; }

    private void Enter(string game)
    {
        _navigation.Game = game;

        var configFile = File.ReadAllText("config.toml");
        var tomlConfig = Toml.ToModel<Config>(configFile);

        _navigation.RomPath = tomlConfig.RomPath;

        Render();
    }

    private void Enter()
    {
        Render();
    }

    private void Render(IRenderable? content = null, bool customUserInteraction = false)
    {
        content ??= new Rows(
            new Markup($"[green]Game loaded:[/] [yellow]{_navigation.Game}[/]"),
            new Markup($"[green]ROM path:[/] [yellow]{_navigation.RomPath}[/]"));

        var window = new MainWindow(_navigation)
            .WithContent(content);

        if (!customUserInteraction)
        {
            window
                .AddCommand("[yellow][underline]G[/]allery[/]", ConsoleKey.G, () => { _navigation.StateMachine.Fire(NavigationTriggers.OpenGallery); })
                .AddCommand("[yellow][underline]C[/]hange game[/]", ConsoleKey.C, () => { _navigation.StateMachine.Fire(NavigationTriggers.ChangeGame); })
                .AddCommand("[yellow][underline]S[/]et ROM path[/]", ConsoleKey.S, SetRomPath)
                .AddCommand("[red][underline]Q[/]uit[/]", ConsoleKey.Q, () => { Environment.Exit(0); });
        }

        window.Draw();
    }

    private void SetRomPath()
    {
        var content = new Panel(new Markup($"[green]Enter ROM path:[/]"))
        {
            Border = BoxBorder.Ascii,
            Padding = new Padding(1, 2, 1, 3),
        };

        AnsiConsole.Clear();
        AnsiConsole.Cursor.Show();

        var romPath = AnsiConsole.Prompt(new TextPrompt<string>("ROM Path: "));

        _navigation.RomPath = romPath;

        var config = new Config
        {
            RomPath = romPath
        };

        var tomlConfig = Toml.FromModel(config);
        File.WriteAllText("config.toml", tomlConfig);

        Render();
    }

    private class Config
    {
        public string RomPath { get; set; } = string.Empty;
    }
}