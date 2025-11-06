using QuickRPG.Console.Configs;
using QuickRPG.Console.Games;
using QuickRPG.Console.Rendering;
using Spectre.Console;
using System;

namespace QuickRPG.Console.States;

public class MapElementsState
{
    private readonly Navigation _navigation;
    private readonly ConfigManager _configManager;

    private int offset;

    public MapElementsState(Navigation navigation, ConfigManager configManager)
    {
        _navigation = navigation;
        _configManager = configManager;
        _navigation.StateMachine.Configure(NavigationStates.MapElements)
            .OnEntry(Enter)
            .Permit(NavigationTriggers.CloseMapElements, NavigationStates.GalleryMain);
    }

    private void Enter()
    {
        Render();
    }

    private void Render()
    {
        var mapElements = new FinalFantasyMysticQuest(_configManager, _navigation.RomPath!).GetMapElements(offset);

        new MainWindow(_navigation)
            .WithContent(new Rows(mapElements.Select(map => new Markup(RenderMapElements(map))).ToArray()))
            .AddCommand("[yellow]Page Up[/]", ConsoleKey.PageUp, PageUp)
            .AddCommand("[yellow]Page Down[/]", ConsoleKey.PageDown, PageDown)
            .AddCommand("[yellow]Left[/]", ConsoleKey.LeftArrow, LeftOne)
            .AddCommand("[yellow]Right[/]", ConsoleKey.RightArrow, RightOne)
            .AddCommand("[green]ESC[/] Close Map Elements", ConsoleKey.Escape, () => { _navigation.StateMachine.Fire(NavigationTriggers.CloseMapElements); })
            .Draw();
    }

    private string RenderMapElements(MapElements map)
    {
        var name = $"[green]{map.Name,-15}[/]";
        var mapX = $"X [blue]{map.X:X2}[/]";
        var mapY = $"Y [blue]{map.Y:X2}[/]";
        var pal = $"PAL [blue]{map.Palette:X2}[/]";
        var type = $"TYP [blue]{map.Type,-8}[/]";
        var subType = $"SUB [blue]{map.SubType,-20}[/]";

        var raw = string.Join(" ", map.Raw.Select(c => $"{c:X2}"));

        return $"{name} {mapX} {mapY} {pal} {type} {subType} {raw}";
    }

    private void PageUp() { offset -= 24 * 7; Render(); }
    private void PageDown() { offset += 24 * 7; Render(); }
    private void LeftOne() { offset--; Render(); }
    private void RightOne() { offset++; Render(); }
}
