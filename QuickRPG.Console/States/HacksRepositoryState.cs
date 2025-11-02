using Microsoft.Win32.SafeHandles;
using QuickRPG.Console.Configs;
using QuickRPG.Console.Games;
using QuickRPG.Console.Rendering;
using Spectre.Console;

namespace QuickRPG.Console.States;

public class HacksRepositoryState
{
    private readonly Navigation _navigation;
    private readonly ConfigManager _configManager;
    private FinalFantasyMysticQuest _hackedRom;

    public HacksRepositoryState(Navigation navigation, ConfigManager configManager)
    {
        _navigation = navigation;
        _configManager = configManager;

        _navigation.StateMachine.Configure(NavigationStates.HacksRepository)
            .OnEntry(Enter)
            .Permit(NavigationTriggers.CloseHacksRepository, NavigationStates.GameLoaded);

        var originalRom = new FinalFantasyMysticQuest(configManager.Config, configManager.Config.RomPath!);
        _hackedRom = new FinalFantasyMysticQuest(configManager.Config, configManager.Config.RomHackPath!, originalRom);
    }

    private void Enter()
    {
        Render();
    }

    private void Render()
    {
        var hacks = _hackedRom.RomHacks
            .Select((rh, index) => new Markup($"[yellow]{index + 1}[/] [green]{rh.HackName}:[/] [yellow]{rh.CurrentValue.Invoke()}[/]"));

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

        var currentValue = romHack.CurrentValue.Invoke();
        var type = currentValue.GetType();

        if (type == typeof(byte))
        {
            var dropRate = PromptByte(romHack.HackName, (byte)(int)romHack.DefaultValue, (byte)currentValue);
            romHack.UpdateValue(dropRate);
        }
        else if (type == typeof(bool))
        {
            var result = PromptBool(romHack.HackName, (bool)romHack.DefaultValue, (bool)currentValue);
            romHack.UpdateValue(result);
        }
        else
            throw new NotSupportedException($"Type {type} is not supported");

        romHack.RunHack();
        _configManager.SaveConfig();

        Render();
    }

    private byte PromptByte(string title, byte originalValue, byte currentValue)
    {
        var answer = AnsiConsole.Prompt(
            new TextPrompt<byte>($"{title} (original value = {originalValue}) ")
                .DefaultValue(currentValue));

        return answer;
    }

    private bool PromptBool(string title, bool originalValue, bool currentValue)
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
