using QuickRPG.Console.Configs;
using QuickRPG.Console.Games;
using QuickRPG.Console.Rendering;
using Spectre.Console;

namespace QuickRPG.Console.States;

public class MapsGaleryState
{
    private readonly Navigation _navigation;
    private readonly ConfigManager _configManager;
    private int _selectionIndex;
    private int _selectionCount;
    private int _pageIndex;
    private int _pageSize;
    private IEnumerable<MapData> _mapsData;

    public MapsGaleryState(Navigation navigation, ConfigManager configManager)
    {
        _navigation = navigation;
        _configManager = configManager;
        _navigation.StateMachine.Configure(NavigationStates.MapsGallery)
            .OnEntry(Enter)
            .Permit(NavigationTriggers.CloseMapsGallery, NavigationStates.GalleryMain)
            .Permit(NavigationTriggers.OpenMapElements, NavigationStates.MapElements);
    }

    private void Enter()
    {
        Render();
    }

    private void Render()
    {
        _mapsData = new FinalFantasyMysticQuest(_configManager, _navigation.RomPath!).GetMapsData();

        _selectionCount = _mapsData.Count();
        _pageSize = AnsiConsole.Profile.Height - 5;

        _mapsData = _mapsData.Skip(_pageIndex * _pageSize).Take(_pageSize);

        new MainWindow(_navigation)
            .WithContent(new Rows(_mapsData.Select((map, index) => new Markup(RenderMap(map, index))).ToArray()))
            .AddCompositeCommands(_mapsData.Count(), OpenMap)
            .AddCommand("[yellow]Page Up[/]", ConsoleKey.PageUp, PageUp)
            .AddCommand("[yellow]Page Down[/]", ConsoleKey.PageDown, PageDown)
            .AddCommand("[green]ESC[/] Close Enemies Gallery", ConsoleKey.Escape, () => { _navigation.StateMachine.Fire(NavigationTriggers.CloseMapsGallery); })
            .Draw();
    }

    private string RenderMap(MapData map, int index)
    {
        var selector = $"[yellow]{RowIndex(index)}[/]";
        var name = $"[green]{map.Name,-17}[/]";

        return $"{selector} {name}";
    }

    private string RowIndex(int index)
    {
        char[] prefix = ['A', 'B', 'C', 'D', 'E', 'F', 'G'];
        return $"{prefix[index / 10]}{index % 10}";
    }

    private void OpenMap(int mapIndex)
    {
        _navigation.StateMachine.Fire(_navigation.MapElementsState.MapSelectedTrigger, _mapsData.ElementAt(mapIndex));
    }

    private void PageUp()
    {
        _pageIndex--;
        if (_pageIndex < 0) _pageIndex = 0;
        Render();
    }

    private void PageDown()
    {
        _pageIndex++;
        var nbPages = (_selectionCount / _pageSize) + 1;
        if (_pageIndex >= nbPages) _pageIndex = nbPages - 1;
        Render();
    }
}
