using Spectre.Console;
using Spectre.Console.Rendering;

namespace QuickRPG.Console.Rendering;

public class MainWindow
{
    private readonly Navigation _navigation;

    private readonly List<(string Text, ConsoleKey HotKey, Action action)> _commands = [];
    private IRenderable _content = new Markup("[red]No content[/]");

    public MainWindow(Navigation navigation)
    {
        _navigation = navigation;
    }

    public MainWindow AddCommand(string text, ConsoleKey hotkey, Action action)
    {
        _commands.Add((text, hotkey, action));
        return this;
    }

    public MainWindow WithContent(IRenderable content)
    {
        _content = content;
        return this;
    }

    public void Draw()
    {
        var layout = new Layout("Root")
            .SplitRows(
                new Layout("Main"),
                new Layout("Command"));

        var mainPanel = new Panel(_content)
        {
            Header = new PanelHeader($"[blue]{_navigation.Path}[/]"),
            Border = BoxBorder.Rounded,
            Expand = true
        };

        var commands = _commands.Select(c => c.Text);

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

            foreach (var command in _commands)
            {
                if (key?.Key == command.HotKey)
                {
                    command.action();
                    return;
                }
            }   
        }
    }
}
