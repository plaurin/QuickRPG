namespace QuickRPG.Console;

internal enum NavigationStates
{
    NoGameLoaded,
    GameLoaded,
    GalleryMain,
    EnemiesGallery,
    EnemyDetails,
}

internal enum NavigationTriggers
{
    LoadGame,
    ChangeGame,
    OpenGallery,
    CloseGallery,
    OpenEnemiesGallery,
    CloseEnemiesGallery,
    OpenEnemyDetails,
    CloseEnemyDetails,
    NextEnemyDetails,
}