using QuickRPG.Console.States;
using Stateless;

namespace QuickRPG.Console;

public class NavigationStateMachine
{
    private readonly StateMachine<NavigationStates, NavigationTriggers> _stateMachine;
    private StateMachine<NavigationStates, NavigationTriggers>.TriggerWithParameters<string> _loadGameTrigger;

    public NavigationStateMachine()
    {
        _stateMachine = new StateMachine<NavigationStates, NavigationTriggers>(NavigationStates.Init);

        //_stateMachine.Configure(NavigationStates.Init)
        //    .Permit(NavigationTriggers.Start, NavigationStates.NoGameLoaded);

        //_loadGameTrigger = _stateMachine.SetTriggerParameters<string>(NavigationTriggers.LoadGame);

        //_stateMachine.Configure(NavigationStates.GameLoaded)
        //    .OnEntryFrom(_loadGameTrigger, game => GameLoadedState.Enter(game, navigation))
        //    .Permit(NavigationTriggers.ChangeGame, NavigationStates.NoGameLoaded)
        //    .Permit(NavigationTriggers.OpenGallery, NavigationStates.GalleryMain);

        //_stateMachine.Configure(NavigationStates.GalleryMain)
        //    .Permit(NavigationTriggers.CloseGallery, NavigationStates.GameLoaded)
        //    .Permit(NavigationTriggers.OpenEnemiesGallery, NavigationStates.EnemiesGallery);

        //_stateMachine.Configure(NavigationStates.EnemiesGallery)
        //    .Permit(NavigationTriggers.CloseEnemiesGallery, NavigationStates.GalleryMain)
        //    .Permit(NavigationTriggers.OpenEnemyDetails, NavigationStates.EnemyDetails);

        //_stateMachine.Configure(NavigationStates.EnemyDetails)
        //    .PermitReentry(NavigationTriggers.NextEnemyDetails)
        //    .Permit(NavigationTriggers.CloseEnemyDetails, NavigationStates.EnemiesGallery);
    }

    public void Transition(NavigationTriggers trigger)
    {
        _stateMachine.Fire(trigger);
    }

    public void Transition(string game)
    {
        _stateMachine.Fire(_loadGameTrigger, game);
    }
}