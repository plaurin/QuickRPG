using QuickRPG.Console.Configs;
using QuickRPG.Console.Games;
using QuickRPG.Console.Rendering;
using Spectre.Console;
using Stateless;

namespace QuickRPG.Console.States;

public class MapElementsState
{
    private readonly Navigation _navigation;
    private readonly ConfigManager _configManager;

    private int _offset;
    private int? _previousMapIndex;
    private int? _nextMapIndex;

    public MapElementsState(Navigation navigation, ConfigManager configManager)
    {
        _navigation = navigation;
        _configManager = configManager;

        MapSelectedTrigger = _navigation.StateMachine.SetTriggerParameters<int>(NavigationTriggers.OpenMapElements);
        PreviousMapTrigger = _navigation.StateMachine.SetTriggerParameters<int>(NavigationTriggers.PreviousMapElement);
        NextMapTrigger = _navigation.StateMachine.SetTriggerParameters<int>(NavigationTriggers.NextMapElement);

        _navigation.StateMachine.Configure(NavigationStates.MapElements)
            .PermitReentry(NavigationTriggers.PreviousMapElement)
            .PermitReentry(NavigationTriggers.NextMapElement)
            .OnEntryFrom(MapSelectedTrigger, Enter)
            .OnEntryFrom(PreviousMapTrigger, Enter)
            .OnEntryFrom(NextMapTrigger, Enter)
            .Permit(NavigationTriggers.CloseMapElements, NavigationStates.MapsGallery);
    }

    public StateMachine<NavigationStates, NavigationTriggers>.TriggerWithParameters<int> MapSelectedTrigger { get; }
    public StateMachine<NavigationStates, NavigationTriggers>.TriggerWithParameters<int> PreviousMapTrigger { get; }
    public StateMachine<NavigationStates, NavigationTriggers>.TriggerWithParameters<int> NextMapTrigger { get; }

    private void Enter(int mapIndex)
    {
        var mapsData = new FinalFantasyMysticQuest(_configManager, _navigation.RomPath!).GetMapsData();
        var mapData = mapsData.ElementAt(mapIndex);

        _navigation.SetPaths("Gallery", "Maps", mapData.Name);

        _offset = mapData.ElementsOffset;
        _previousMapIndex = mapIndex > 0 ? mapIndex - 1 : null;
        _nextMapIndex = mapIndex < mapsData.Count() - 1 ? mapIndex + 1 : null;

        Render();
    }

    private void Render()
    {
        var mapElements = new FinalFantasyMysticQuest(_configManager, _navigation.RomPath!).GetMapElements(_offset);

        new MainWindow(_navigation)
            .WithContent(new Rows(mapElements.Select(map => new Markup(RenderMapElements(map))).ToArray()))
            .AddCommand("[yellow]Page Up[/]", ConsoleKey.PageUp, PageUp)
            .AddCommand("[yellow]Page Down[/]", ConsoleKey.PageDown, PageDown)
            .AddCommand("[yellow]Left[/]", ConsoleKey.LeftArrow, LeftOne)
            .AddCommand("[yellow]Right[/]", ConsoleKey.RightArrow, RightOne)
            .AddCommandIf(_previousMapIndex != null, "[yellow]Shift+Tab[/] Previous Map", ConsoleModifiers.Shift, ConsoleKey.Tab, PreviousMap)
            .AddCommandIf(_nextMapIndex != null, "[yellow]Tab[/] Next Map", ConsoleKey.Tab, NextMap)
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

    private void PageUp() { _offset -= (MainWindow.PageSize - 1) * 7; Render(); }
    private void PageDown() { _offset += (MainWindow.PageSize - 1) * 7; Render(); }
    private void LeftOne() { _offset--; Render(); }
    private void RightOne() { _offset++; Render(); }

    private void PreviousMap() => _navigation.StateMachine.Fire(_navigation.MapElementsState.PreviousMapTrigger, _previousMapIndex);
    private void NextMap() => _navigation.StateMachine.Fire(_navigation.MapElementsState.NextMapTrigger, _nextMapIndex);

}
