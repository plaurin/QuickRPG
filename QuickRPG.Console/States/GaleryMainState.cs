using QuickRPG.Console.Configs;
using QuickRPG.Console.Games;
using QuickRPG.Console.Rendering;
using Spectre.Console;

namespace QuickRPG.Console.States;

public class GaleryMainState
{
    private readonly Navigation _navigation;
    private readonly ConfigManager _configManager;

    public GaleryMainState(Navigation navigation, ConfigManager configManager)
    {
        _navigation = navigation;
        _configManager = configManager;
        _navigation.StateMachine.Configure(NavigationStates.GalleryMain)
            .OnEntry(Enter)
            .Permit(NavigationTriggers.CloseGallery, NavigationStates.GameLoaded)
            .Permit(NavigationTriggers.OpenEnemiesGallery, NavigationStates.EnemiesGallery)
            .Permit(NavigationTriggers.OpenMapElements, NavigationStates.MapElements);
    }

    private void Enter()
    {
        var enemiesData = new FinalFantasyMysticQuest(_configManager, _navigation.RomPath!).GetEnemiesData();

        new MainWindow(_navigation)
            .WithContent(new Rows(
                new Markup($"[green]In Main Gallery[/]"),
                new Markup($"[green]Total Enemies:[/] [red]{enemiesData.Count()}[/]")))
            .AddCommand("[yellow][underline]E[/]nemies Gallery[/]", ConsoleKey.E, () => { _navigation.StateMachine.Fire(NavigationTriggers.OpenEnemiesGallery); })
            .AddCommand("[yellow][underline]M[/]ap Elements[/]", ConsoleKey.M, () => { _navigation.StateMachine.Fire(NavigationTriggers.OpenMapElements); })
            .AddCommand("[green]ESC[/] Close Gallery", ConsoleKey.Escape, () => { _navigation.StateMachine.Fire(NavigationTriggers.CloseGallery); })
            .Draw();
    }
}
