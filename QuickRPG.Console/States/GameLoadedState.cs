using QuickRPG.Console.Rendering;
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
            .OnEntryFrom(NavigationTriggers.CloseGallery, Enter)
            .Permit(NavigationTriggers.ChangeGame, NavigationStates.NoGameLoaded)
            .Permit(NavigationTriggers.OpenGallery, NavigationStates.GalleryMain);
    }

    public StateMachine<NavigationStates, NavigationTriggers>.TriggerWithParameters<string> LoadGameTrigger { get; }

    private void Enter(string game)
    {
        _navigation.Game = game;
        Render();
    }

    private void Enter()
    {
        Render();
    }

    private void Render()
    {
        new MainWindow(_navigation)
            .WithContent(new Markup($"[green]Game loaded:[/] [yellow]{_navigation.Game}[/]"))
            .AddCommand("[yellow][underline]G[/]allery[/]", ConsoleKey.G, () => { _navigation.StateMachine.Fire(NavigationTriggers.OpenGallery); })
            .AddCommand("[yellow][underline]C[/]hange game[/]", ConsoleKey.C, () => { _navigation.StateMachine.Fire(NavigationTriggers.ChangeGame); })
            .AddCommand("[red][underline]Q[/]uit[/]", ConsoleKey.Q, () => { Environment.Exit(0); })
            .Draw();
    }
}