using Spectre.Console;
using Spectre.Console.Rendering;

namespace QuickRPG.Console.Rendering;

public class MainWindow
{
    private readonly Navigation _navigation;

    private readonly List<(string Text, ConsoleModifiers Modifiers, ConsoleKey HotKey, Action Action)> _commands = [];
    private readonly List<(ConsoleKey LetterKey, ConsoleKey DigitKey, int Index, Action<int> Action)> _compositeCommands = [];
    private IRenderable _content = new Markup("[red]No content[/]");

    public static int PageSize => AnsiConsole.Profile.Height - 5;

    public MainWindow(Navigation navigation)
    {
        _navigation = navigation;
    }

    public MainWindow AddCommand(string text, ConsoleKey hotkey, Action action)
    {
        _commands.Add((text, ConsoleModifiers.None, hotkey, action));
        return this;
    }

    public MainWindow AddCommandIf(bool predicate, string text, ConsoleKey hotkey, Action action)
    {
        if (predicate) _commands.Add((text, ConsoleModifiers.None, hotkey, action));
        return this;
    }

    public MainWindow AddCommand(string text, ConsoleModifiers modifiers, ConsoleKey hotkey, Action action)
    {
        _commands.Add((text, modifiers, hotkey, action));
        return this;
    }

    public MainWindow AddCommandIf(bool predicate, string text, ConsoleModifiers modifiers, ConsoleKey hotkey, Action action)
    {
        if (predicate) _commands.Add((text, modifiers, hotkey, action));
        return this;
    }

    public MainWindow AddCommand(ConsoleKey hotkey, Action action)
    {
        _commands.Add(("", ConsoleModifiers.None, hotkey, action));
        return this;
    }

    public MainWindow AddCompositeCommands(int count, Action<int> action)
    {
        for (int i = 0; i < count; i++)
        {
            var letterKey = ConsoleKey.A + i / 10;
            var digitKey = ConsoleKey.D0 + i % 10;
            _compositeCommands.Add((letterKey, digitKey, i, action));
        }
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

        IRenderable mainContent;

        mainContent = _content;

        var mainPanel = new Panel(mainContent)
        {
            Header = new PanelHeader($"[blue]{_navigation.Path}[/]"),
            Border = BoxBorder.Rounded,
            Expand = true
        };

        var commands = _commands.Where(c => !string.IsNullOrWhiteSpace(c.Text)).Select(c => c.Text);

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
        AnsiConsole.Cursor.Hide();

        ConsoleKey? letterPressed = null;
        ConsoleKey? digitPressed = null;

        while (_commands.Count != 0 || _compositeCommands.Count != 0)
        {
            var readKey =  AnsiConsole.Console.Input.ReadKey(true);
            var key = readKey?.Key;
            var modifiers = readKey?.Modifiers;

            if (key == null) continue;

            foreach (var command in _commands)
            {
                if (key == command.HotKey && modifiers == command.Modifiers)
                {
                    command.Action();
                    return;
                }
            }

            if (key >= ConsoleKey.A && key <= ConsoleKey.Z) letterPressed = key;
            if (key >= ConsoleKey.D0 && key <= ConsoleKey.D9) digitPressed = key;

            foreach (var command in _compositeCommands)
            {
                if (letterPressed == command.LetterKey && digitPressed == command.DigitKey)
                {
                    command.Action(command.Index);
                    return;
                }
            }
        }
    }
}
