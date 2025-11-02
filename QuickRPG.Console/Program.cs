using QuickRPG.Console;
using Spectre.Console;

//AnsiConsole.MarkupLine("[red](Welcome to Quick RPG)[/]");

var sm = new NavigationStateMachine();
var navigation = new Navigation(sm);

navigation.StateMachine.Fire(NavigationTriggers.Start);

Console.ReadKey();
