using Spectre.Console;

namespace QuickRPG.Console.States;

public class NoGameLoadedState
{
    private readonly Navigation _navigation;

    public NoGameLoadedState(Navigation navigation)
    {
        _navigation = navigation;

        _navigation.StateMachine.Configure(NavigationStates.NoGameLoaded)
            .OnEntry(Enter)
            .Permit(NavigationTriggers.LoadGame, NavigationStates.GameLoaded);
    }

    private void Enter()
    {
        _navigation.Game = null;

        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[red](Welcome to Quick RPG)[/]");

        var game = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a game")
                .PageSize(5)
                .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                .AddChoices([
                    "Final Fantasy Mystic Quest",
                    "Dragon Warrior 1",
                    "Dragon Warrior 2",
                ]));

        _navigation.StateMachine.Fire(_navigation.GameLoadedState.LoadGameTrigger, game);
    }
}