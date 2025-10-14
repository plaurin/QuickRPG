using QuickRPG.Console;
using Stateless;

Console.WriteLine("Hello, World!");

var sm = new StateMachine<NavigationStates, NavigationTriggers>(NavigationStates.NoGameLoaded);

sm.Configure(NavigationStates.NoGameLoaded)
    .Permit(NavigationTriggers.LoadGame, NavigationStates.GameLoaded);

sm.Configure(NavigationStates.GameLoaded)
    .Permit(NavigationTriggers.ChangeGame, NavigationStates.NoGameLoaded)
    .Permit(NavigationTriggers.OpenGallery, NavigationStates.GalleryMain);

sm.Configure(NavigationStates.GalleryMain)
    .Permit(NavigationTriggers.CloseGallery, NavigationStates.GameLoaded)
    .Permit(NavigationTriggers.OpenEnemiesGallery, NavigationStates.EnemiesGallery);

sm.Configure(NavigationStates.EnemiesGallery)
    .Permit(NavigationTriggers.CloseEnemiesGallery, NavigationStates.GalleryMain)
    .Permit(NavigationTriggers.OpenEnemyDetails, NavigationStates.EnemyDetails);

sm.Configure(NavigationStates.EnemyDetails)
    .PermitReentry(NavigationTriggers.NextEnemyDetails)
    .Permit(NavigationTriggers.CloseEnemyDetails, NavigationStates.EnemiesGallery);

sm.Fire(NavigationTriggers.LoadGame);
sm.Fire(NavigationTriggers.OpenGallery);
sm.Fire(NavigationTriggers.OpenEnemiesGallery);
sm.Fire(NavigationTriggers.OpenEnemyDetails);

Console.WriteLine(sm.State);
Console.WriteLine(sm.IsInState(NavigationStates.EnemiesGallery));
Console.WriteLine(sm.PermittedTriggers);
Console.WriteLine(sm.GetDetailedPermittedTriggers());
Console.WriteLine(sm.ToString());
