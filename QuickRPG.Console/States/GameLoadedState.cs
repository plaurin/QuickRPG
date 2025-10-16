using Spectre.Console;
using Stateless;

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
            .Permit(NavigationTriggers.ChangeGame, NavigationStates.NoGameLoaded)
            .Permit(NavigationTriggers.OpenGallery, NavigationStates.GalleryMain);
    }

    public StateMachine<NavigationStates, NavigationTriggers>.TriggerWithParameters<string> LoadGameTrigger { get; }

    private void Enter(string game)
    {
        _navigation.Game = game;

        var layout = new Layout("Root")
            .SplitRows(
                new Layout("Main"),
                new Layout("Command"));

        var mainPanel = new Panel("Temp Content")
        {
            Header = new PanelHeader($"   [blue]{_navigation.Path}[/]   "),
            Border = BoxBorder.Rounded,
            Expand = true
        };

        var commands = new[]
        {
            "[yellow][underline]G[/]allery[/]",
            "[yellow][underline]C[/]hange game[/]",
            "[red]Q[/]uit",
        };

        var commandPanel = new Panel(new Columns(commands))
        {
            Header = new PanelHeader("Commands"),
            Border = BoxBorder.Rounded,
            Expand = true
        }.BorderColor(Color.Yellow);

        layout["Main"].Update(mainPanel);
        layout["Command"].Update(commandPanel).Size(3);

        AnsiConsole.Clear();
        AnsiConsole.Write(layout);

        while (true)
        {
            var key = AnsiConsole.Console.Input.ReadKey(true);

            switch (key?.Key)
            {
                case ConsoleKey.G:
                    _navigation.StateMachine.Fire(NavigationTriggers.OpenGallery);
                    return;
                case ConsoleKey.C:
                    _navigation.StateMachine.Fire(NavigationTriggers.ChangeGame);
                    return;
                case ConsoleKey.Q:
                    Environment.Exit(0);
                    return;
                default:
                    break;
            }
        }
    }
}