using QuickRPG.Console.Configs;
using QuickRPG.Console.Games;
using QuickRPG.Console.Rendering;
using Spectre.Console;

namespace QuickRPG.Console.States;

public class EnemiesGaleryState
{
    private readonly Navigation _navigation;
    private readonly GameConfig _config;
    private int _selectionIndex;
    private int _selectionCount;
    private int _pageIndex;

    public EnemiesGaleryState(Navigation navigation, GameConfig config)
    {
        _navigation = navigation;
        _config = config;
        _navigation.StateMachine.Configure(NavigationStates.EnemiesGallery)
            .OnEntry(Enter)
            .Permit(NavigationTriggers.CloseEnemiesGallery, NavigationStates.GalleryMain)
            .Permit(NavigationTriggers.OpenEnemyDetails, NavigationStates.EnemyDetails);
    }

    private void Enter()
    {
        Render();
    }

    private void Render()
    {
        var enemiesData = new FinalFantasyMysticQuest(_config, _navigation.RomPath!).GetEnemiesData();

        if (_navigation.EnemySorting == EnemySorting.Name)
            enemiesData = enemiesData.OrderBy(e => e.Name);

        _selectionCount = enemiesData.Count();

        enemiesData = enemiesData.Skip(_pageIndex * 26);

        new MainWindow(_navigation)
            .WithContent(new Rows(enemiesData.Select((enemy, index) => new Markup(RenderEnemy(enemy, index))).ToArray()))
            .AddCommand("", ConsoleKey.DownArrow, SelectNext)
            .AddCommand("", ConsoleKey.UpArrow, SelectPrevious)
            .AddCommand("[yellow]Sort by [underline]N[/]ame[/]", ConsoleKey.N, () => { SortBy(EnemySorting.Name); })
            .AddCommand("[yellow]Sort by [underline]X[/]P[/]", ConsoleKey.X, () => { SortBy(EnemySorting.Xp); })
            .AddCommand("[yellow]Sort by [underline]R[/]om index[/]", ConsoleKey.R, () => { SortBy(EnemySorting.RomIndex); })
            .AddCommand("[yellow]Page Up[/]", ConsoleKey.PageUp, PageUp)
            .AddCommand("[yellow]Page Down[/]", ConsoleKey.PageDown, PageDown)
            .AddCommand("[green]ESC[/] Close Enemies Gallery", ConsoleKey.Escape, () => { _navigation.StateMachine.Fire(NavigationTriggers.CloseEnemiesGallery); })
            .Draw();
    }

    private string RenderEnemy(EnemyData enemy, int index)
    {
        var selector = index == _selectionIndex ? ">" : " ";
        var name = $"[green]{enemy.Name,-17}[/]";

        var hp = $"HP [blue]{enemy.HP,-5}[/]";
        var xp = $"EXP [blue]{enemy.XP,-3}[/]";
        var gil = $"GIL [blue]{enemy.Gil,-3}[/]";
        var str = $"STR [blue]{enemy.Strength,-3}[/]";
        var def = $"DEF [blue]{enemy.Defense,-3}[/]";
        var spd = $"SPD [blue]{enemy.Speed,-3}[/]";
        var mag = $"MAG [blue]{enemy.Magic,-3}[/]";

        var sElem1 = $"ELM [blue]{enemy.StrongElement1:B8}[/]";
        var sElem2 = $"ELM2 [blue]{enemy.StrongElement2:B8}[/]";
        var wElem = $"WEA [blue]{enemy.WeakElement:B8}[/]";

        var strongElem = StrongElement((byte)enemy.StrongElement1, (byte)enemy.StrongElement2);

        var restElem1 = enemy.StrongElement1 & 0b0001_1111;
        var rest = $"RES [blue]{restElem1:B8}[/]";

        var raw = string.Join(" ", enemy.Raw.Select(c => $"{c:X2}"));

        //return $"{selector} {name} {hp} {xp} {gil} {str} {def} {spd} {mag} {sElem1} {sElem2} {wElem} {selector}";
        return $"{selector} {name} {hp} {sElem1} {sElem2} {rest} {strongElem} {selector}";
    }

    private string StrongElement(byte data1, byte data2)
    {
        var elements = new List<string>();
        // x1000_0000 x0000_0000 Earth
        // x0100_0000 x0000_0000 Fire
        // x0010_0000 x0000_0000 Fire too
        // x0001_0000 x0000_0000 Thunder
        // x0000_0000 x1000_0000 Water
        // x0000_0000 x0100_0000 Petrify
        // x0000_0000 x0010_0000 Paralysis
        // x0000_0000 x0001_0000 Sleep
        // x0000_0000 x0000_1000 Immune Conf+Fatal
        // x0000_0000 x0000_0100 Poison
        // x0000_0000 x0000_0010 Bind
        // x0000_0000 x0000_0001 Sleep too
        if ((data1 & 0b1000_0000) == 0b1000_0000) elements.Add("EAR");
        if ((data1 & 0b0100_0000) == 0b0100_0000) elements.Add("FIR");
        if ((data1 & 0b0010_0000) == 0b0010_0000) elements.Add("FIR");
        if ((data1 & 0b0001_0000) == 0b0001_0000) elements.Add("THU");
        if ((data2 & 0b1000_0000) == 0b1000_0000) elements.Add("WAT");
        if ((data2 & 0b0100_0000) == 0b0100_0000) elements.Add("PET");
        if ((data2 & 0b0010_0000) == 0b0010_0000) elements.Add("PAR");
        if ((data2 & 0b0001_0000) == 0b0001_0000) elements.Add("SLE");
        if ((data2 & 0b0000_1000) == 0b0000_1000) elements.Add("CON");
        if ((data2 & 0b0000_0100) == 0b0000_0100) elements.Add("POI");
        if ((data2 & 0b0000_0010) == 0b0000_0010) elements.Add("BIN");
        if ((data2 & 0b0000_0001) == 0b0000_0001) elements.Add("SLE");

        return string.Join(" ", elements);
    }

    private void SelectNext()
    {
        _selectionIndex++;
        if (_selectionIndex >= _selectionCount) _selectionIndex = _selectionCount - 1;
        Render();
    }

    private void SelectPrevious()
    {
        _selectionIndex--;
        if (_selectionIndex < 0) _selectionIndex = 0;
        Render();
    }

    private void SortBy(EnemySorting sorting)
    {
        _navigation.EnemySorting = sorting;
        Render();
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
        var nbPages = (_selectionCount / 26) + 1;
        if (_pageIndex >= nbPages) _pageIndex = nbPages - 1;
        Render();
    }
}
