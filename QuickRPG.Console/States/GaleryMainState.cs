using Spectre.Console;

namespace QuickRPG.Console.States;

public class GaleryMainState
{
    private readonly Navigation _navigation;

    public GaleryMainState(Navigation navigation)
    {
        _navigation = navigation;

        _navigation.StateMachine.Configure(NavigationStates.GalleryMain)
            .OnEntry(Enter)
            .Permit(NavigationTriggers.CloseGallery, NavigationStates.GameLoaded)
            .Permit(NavigationTriggers.OpenEnemiesGallery, NavigationStates.EnemiesGallery);
    }

    private void Enter()
    {
        var layout = new Layout("Root")
            .SplitRows(
                new Layout("Main"),
                new Layout("Command"));

        var mainPanel = new Panel("Temp Gallery")
        {
            Header = new PanelHeader($"   [blue]{_navigation.Path}[/]   "),
            Border = BoxBorder.Rounded,
            Expand = true
        };

        var commands = new[]
        {
            "[yellow][underline]E[/]nemies Gallery[/]",
            "[green]ESC[/] Close Gallery",
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
                case ConsoleKey.E:
                    _navigation.StateMachine.Fire(NavigationTriggers.OpenEnemiesGallery);
                    return;
                case ConsoleKey.Escape:
                    _navigation.StateMachine.Fire(NavigationTriggers.CloseGallery);
                    return;
                default:
                    break;
            }
        }
    }
}
