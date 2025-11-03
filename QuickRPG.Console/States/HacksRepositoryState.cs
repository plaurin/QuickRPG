using QuickRPG.Console.Configs;
using QuickRPG.Console.Games;
using QuickRPG.Console.Rendering;
using Spectre.Console;

namespace QuickRPG.Console.States;

public class HacksRepositoryState
{
    private readonly Navigation _navigation;
    private readonly ConfigManager _configManager;
    private readonly FinalFantasyMysticQuest _hackedRom;

    public HacksRepositoryState(Navigation navigation, ConfigManager configManager)
    {
        _navigation = navigation;
        _configManager = configManager;

        _navigation.StateMachine.Configure(NavigationStates.HacksRepository)
            .OnEntry(Enter)
            .Permit(NavigationTriggers.CloseHacksRepository, NavigationStates.GameLoaded);

        var originalRom = new FinalFantasyMysticQuest(configManager, configManager.Config.RomPath!);
        _hackedRom = new FinalFantasyMysticQuest(configManager, configManager.Config.RomHackPath!, originalRom);
    }

    private void Enter()
    {
        _hackedRom.RunHacks();
        Render();
    }

    private void Render()
    {
        var hacks = _hackedRom.RomHacks
            .Select((rh, index) => new Markup($"[yellow]{index + 1}[/] [green]{rh.HackName}:[/] [yellow]{rh.CurrentValue}[/]"));

        var content = new Rows(hacks);

        var window = new MainWindow(_navigation)
            .WithContent(content)
            .AddCommand("[green]ESC[/] Close Hacks Repository", ConsoleKey.Escape, () => { _navigation.StateMachine.Fire(NavigationTriggers.CloseHacksRepository); });

        var index = 0;
        foreach (var romHack in _hackedRom.RomHacks)
        {
            window.AddCommandWithoutText(ConsoleKey.D1 + index, () => ActivateHack(romHack));
            index++;
        }

        window.Draw();
    }

    private void ActivateHack(RomHack romHack)
    {
        AnsiConsole.Clear();
        AnsiConsole.Cursor.Show();

        var type = romHack.CurrentValue.GetType();

        if (type == typeof(byte))
        {
            var value = PromptByte(romHack.HackName, (byte)(int)romHack.DefaultValue, (byte)romHack.CurrentValue);
            romHack.UpdateValue(value);
        }
        else if (type == typeof(bool))
        {
            var value = PromptBool(romHack.HackName, (bool)romHack.DefaultValue, (bool)romHack.CurrentValue);
            romHack.UpdateValue(value);
        }
        else
            throw new NotSupportedException($"Type {type} is not supported");

        _hackedRom.RunHacks();
        _configManager.SaveConfig();

        Render();
    }

    private static byte PromptByte(string title, byte originalValue, byte currentValue)
    {
        var answer = AnsiConsole.Prompt(
            new TextPrompt<byte>($"{title} (original value = {originalValue}) ")
                .DefaultValue(currentValue));

        return answer;
    }

    private static bool PromptBool(string title, bool originalValue, bool currentValue)
    {
        var answer = AnsiConsole.Prompt(
            new TextPrompt<bool>($"{title} (original value = {originalValue}): ")
                .AddChoice(true)
                .AddChoice(false)
                .DefaultValue(currentValue)
                .WithConverter(choice => choice ? "y" : "n"));

        return answer;
    }
}
