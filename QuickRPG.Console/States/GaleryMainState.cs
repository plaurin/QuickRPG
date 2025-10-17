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
        new MainWindow(_navigation)
            .WithContent(new Markup($"[green]In Main Gallery[/]"))
            .AddCommand("[yellow][underline]E[/]nemies Gallery[/]", ConsoleKey.E, () => { _navigation.StateMachine.Fire(NavigationTriggers.OpenEnemiesGallery); })
            .AddCommand("[green]ESC[/] Close Gallery", ConsoleKey.Escape, () => { _navigation.StateMachine.Fire(NavigationTriggers.CloseGallery); })
            .Draw();
    }
}
