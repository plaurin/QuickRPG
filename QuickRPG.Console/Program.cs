using Spectre.Console;

AnsiConsole.MarkupLine("[red](Welcome to Quick RPG)[/]");

var navigation = new Navigation();

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

// Echo the fruit back to the terminal
AnsiConsole.WriteLine($"Selected Game : {game}");

navigation.Game = game;

var panel = new Panel("toto")
{
    Header = new PanelHeader(navigation.Path),
    Border = BoxBorder.Rounded,
    Expand = true
};

AnsiConsole.Write(panel);

Console.ReadKey();