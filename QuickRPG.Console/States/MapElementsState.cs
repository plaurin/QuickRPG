using QuickRPG.Console.Games;
using QuickRPG.Console.Rendering;
using Spectre.Console;
using System;

namespace QuickRPG.Console.States;

public class MapElementsState
{
    private readonly Navigation _navigation;

    public MapElementsState(Navigation navigation)
    {
        _navigation = navigation;

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
        var mapElements = new FinalFantasyMysticQuest(_navigation.RomPath!).GetMapElements();

        new MainWindow(_navigation)
            .WithContent(new Rows(mapElements.Select(map => new Markup(RenderMapElements(map))).ToArray()))
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
        var subType = $"SUB [blue]{map.SubType,-10}[/]";

        var raw = string.Join(" ", map.Raw.Select(c => $"{c:X2}"));

        return $"{name} {mapX} {mapY} {pal} {type} {subType} {raw}";
    }
}
