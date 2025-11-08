using QuickRPG.Console.Configs;
using QuickRPG.Console.States;
using Stateless;

namespace QuickRPG.Console;

public class Navigation
{
    private readonly StateMachine<NavigationStates, NavigationTriggers> _stateMachine;
    private readonly NoGameLoadedState _noGameLoadedState;

    public Navigation(NavigationStateMachine navigationStateMachine)
    {
        _stateMachine = new StateMachine<NavigationStates, NavigationTriggers>(NavigationStates.Init);

        _stateMachine.Configure(NavigationStates.Init)
            .Permit(NavigationTriggers.Start, NavigationStates.NoGameLoaded);

        var configManager = new ConfigManager();

        _noGameLoadedState = new NoGameLoadedState(this);
        GameLoadedState = new GameLoadedState(this, configManager);
        GaleryMainState = new GaleryMainState(this, configManager);

        EnemiesGaleryState = new EnemiesGaleryState(this, configManager);

        MapsGaleryState = new MapsGaleryState(this, configManager);
        MapElementsState = new MapElementsState(this, configManager);

        HacksRepositoryState = new HacksRepositoryState(this, configManager);
    }

    public StateMachine<NavigationStates, NavigationTriggers> StateMachine => _stateMachine;

    public string? Game { get; set; }
    public List<string> Nav { get; } = [];
    public string Path => $"> {Game}";
    public string RomPath { get; internal set; }
    public string RomHackPath { get; internal set; }
    public EnemySorting EnemySorting { get; internal set; }

    public GameLoadedState GameLoadedState { get; }
    public GaleryMainState GaleryMainState { get; }
    public EnemiesGaleryState EnemiesGaleryState { get; }
    public MapsGaleryState MapsGaleryState { get; }
    public MapElementsState MapElementsState { get; }
    public HacksRepositoryState HacksRepositoryState { get; }
}

public enum EnemySorting
{
    RomIndex,
    Name,
    Xp
}