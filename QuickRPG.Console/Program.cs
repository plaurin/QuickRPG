using QuickRPG.Console;
using Spectre.Console;

//AnsiConsole.MarkupLine("[red](Welcome to Quick RPG)[/]");

var sm = new NavigationStateMachine();
var navigation = new Navigation(sm);

navigation.StateMachine.Fire(NavigationTriggers.Start);

//var game = AnsiConsole.Prompt(
//    new SelectionPrompt<string>()
//        .Title("Select a game")
//        .PageSize(5)
//        .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
//        .AddChoices([
//            "Final Fantasy Mystic Quest",
//            "Dragon Warrior 1",
//            "Dragon Warrior 2",
//        ]));

//// Echo the fruit back to the terminal
//AnsiConsole.WriteLine($"Selected Game : {game}");

//sm.Transition(game);

Console.ReadKey();
