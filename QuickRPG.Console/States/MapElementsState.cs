using QuickRPG.Console.Configs;
using QuickRPG.Console.Games;
using QuickRPG.Console.Rendering;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Spectre.Console;
using Spectre.Console.Rendering;
using Stateless;

namespace QuickRPG.Console.States;

public class MapElementsState
{
    private readonly Navigation _navigation;
    private readonly ConfigManager _configManager;

    private int _offset;
    private int? _previousMapIndex;
    private int? _nextMapIndex;
    private bool _isMapMode;

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
        var content = _isMapMode ? RenderMap() : RenderList();

        new MainWindow(_navigation)
            .WithContent(content)
            .AddCommand("[yellow]Page Up[/]", ConsoleKey.PageUp, PageUp)
            .AddCommand("[yellow]Page Down[/]", ConsoleKey.PageDown, PageDown)
            .AddCommand("[yellow]Left[/]", ConsoleKey.LeftArrow, LeftOne)
            .AddCommand("[yellow]Right[/]", ConsoleKey.RightArrow, RightOne)
            .AddCommandIf(_previousMapIndex != null, "[yellow]Shift+Tab[/] Previous Map", ConsoleModifiers.Shift, ConsoleKey.Tab, PreviousMap)
            .AddCommandIf(_nextMapIndex != null, "[yellow]Tab[/] Next Map", ConsoleKey.Tab, NextMap)
            .AddCommandIf(!_isMapMode, "Toggle [yellow]M[/]ap", ConsoleKey.M, ToggleMap)
            .AddCommandIf(_isMapMode, "Toggle [yellow]L[/]ist", ConsoleKey.L, ToggleList)
            .AddCommand("[green]ESC[/] Close Map Elements", ConsoleKey.Escape, () => { _navigation.StateMachine.Fire(NavigationTriggers.CloseMapElements); })
            .Draw();
    }

    public IRenderable RenderList()
    {
        var mapElements = new FinalFantasyMysticQuest(_configManager, _navigation.RomPath!).ExtractMapElements(_offset - 14, MainWindow.PageSize);

        return new Rows(mapElements.Select(map => new Markup(RenderMapElements(map))).ToArray());
    }

    public IRenderable RenderMap()
    {
        var mapElements = new FinalFantasyMysticQuest(_configManager, _navigation.RomPath!).ExtractMapElements(_offset - 14, MainWindow.PageSize);
        if (!mapElements.Any()) return new Markup("No map elements!");

        var realElements = mapElements.Where(me => !me.Type.Contains("?"));

        var left = realElements.Any() ? realElements.Min(me => me.X) - 1 : 0;
        var right = realElements.Any() ? realElements.Max(me => me.X) + 1 : 0;
        var top = realElements.Any() ? realElements.Min(me => me.Y) - 1 : 0;
        var bottom = realElements.Any() ? realElements.Max(me => me.Y) + 1 : 0;

        var image = new Image<Rgba32>(right - left + 1, bottom - top + 1, new Rgba32() { A = 255, R = 32, G = 32, B = 32 });
        foreach (var mapElement in realElements)
        {
            var color = new Rgba32() { A = 255, R = 128, G = 128, B = 128 };
            if (mapElement.Type.Contains("Chest")) color = new Rgba32() { A = 255, R = 0, G = 255, B = 0 };
            if (mapElement.Type.Contains("Enemy")) color = new Rgba32() { A = 255, R = 255, G = 0, B = 0 };
            if (mapElement.Type.Contains("Uniqu")) color = new Rgba32() { A = 255, R = 255, G = 255, B = 0 };
            if (mapElement.Type.Contains("Drop")) color = new Rgba32() { A = 255, R = 255, G = 0, B = 255 };

            image[mapElement.X - left, mapElement.Y - top] = color;
        }

        var tempFile = Path.Combine(Path.GetTempPath(), $"QuickRPGMap");
        image.SaveAsPng(tempFile);

        var canvasImage = new CanvasImage(tempFile);
        canvasImage.NearestNeighborResampler();
        return canvasImage;
    }

    private string RenderMapElements(MapElement map)
    {
        var green = map.Type.Contains("?") ? "grey" : "green";
        var blue = map.Type.Contains("?") ? "grey" : "blue";
        var white = map.Type.Contains("?") ? "grey" : "white";

        var typeColor = map.Type[3..] switch
        {
            "Uniqu" => "yellow",
            "Chest" => "green",
            "Enemy" => "red",
            "Drop" => "purple",
            _ => "grey"
        };

        var name = $"[{green}]{map.Name,-5}[/]";
        var mapX = $"X [{blue}]{map.X:X2}[/]";
        var mapY = $"Y [{blue}]{map.Y:X2}[/]";
        var pal = $"PAL [{blue}]{map.Palette:X2}[/]";
        var type = $"TYP [{typeColor}]{map.Type,-8}[/]";
        var subType = $"SUB [{blue}]{map.SubType,-20}[/]";

        var raw = string.Join(" ", map.Raw.Select(c => $"{c:X2}"));

        return $"{name} {mapX} {mapY} {pal} {type} {subType} [{white}]{raw}[/]";
    }

    private void PageUp() { _offset -= (MainWindow.PageSize - 1) * 7; Render(); }
    private void PageDown() { _offset += (MainWindow.PageSize - 1) * 7; Render(); }
    private void LeftOne() { _offset--; Render(); }
    private void RightOne() { _offset++; Render(); }

    private void PreviousMap() => _navigation.StateMachine.Fire(_navigation.MapElementsState.PreviousMapTrigger, _previousMapIndex);
    private void NextMap() => _navigation.StateMachine.Fire(_navigation.MapElementsState.NextMapTrigger, _nextMapIndex);

    private void ToggleMap()
    {
        _isMapMode = true;
        Render();
    }

    private void ToggleList()
    {
        _isMapMode = false;
        Render();
    }

}
