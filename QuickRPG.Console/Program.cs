using QuickRPG.Console;

var navigation = new Navigation();

navigation.StateMachine.Fire(NavigationTriggers.Start);

Console.ReadKey();
