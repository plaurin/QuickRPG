using QuickRPG.Console.Games;
using QuickRPG.Console.Rendering;
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
        var enemiesData = new FinalFantasyMysticQuest(_navigation.RomPath!).GetEnemiesData();

        new MainWindow(_navigation)
            .WithContent(new Rows(
                new Markup($"[green]In Main Gallery[/]"),
                new Markup($"[green]Total Enemies:[/] [red]{enemiesData.Count}[/]")))
            .AddCommand("[yellow][underline]E[/]nemies Gallery[/]", ConsoleKey.E, () => { _navigation.StateMachine.Fire(NavigationTriggers.OpenEnemiesGallery); })
            .AddCommand("[green]ESC[/] Close Gallery", ConsoleKey.Escape, () => { _navigation.StateMachine.Fire(NavigationTriggers.CloseGallery); })
            .Draw();
    }
}
