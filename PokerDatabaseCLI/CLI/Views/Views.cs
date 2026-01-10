
namespace PokerDatabaseCLI.CLI.Views;

public class StartupView : IView {

    private CommandContext _context { get; }
    public StartupView(CommandContext context) {
        _context = context;
    }
    public ViewResult
    RunView() {
        Console.WriteLine("Welcome to Poker Database CLI!");
        Console.WriteLine("Please enter path to hand history folder:");
        var folderPath = Console.ReadLine();
        if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath)) {
            Console.WriteLine("Invalid folder path. Try again.");
            Console.ReadKey();
            return ViewResult.Retry;
        }
        _context.Database.HandsAdded += count => {
            Console.WriteLine($"{count} hands added to the database.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        };
        _context.Database.AddHandsFromDirectory(folderPath);//асинхронная логика не нужна я думаю
        return ViewResult.Success;
    }

}

public class MainView : IView {
    private CommandContext _context { get; }
    public MainView(CommandContext context) {
        _context = context;
    }
    private static readonly string HelpText = @"
Available commands:
  showstats                        : Show total hands and players in database
  ShowLastHands              | -h  : Show last 10 hands of a hero
  DeleteHand --HandNumber <id> | -n <id>   : Delete a hand by ID
  Exit                          -q     : Exit the program
  Help                          : Show this help
";
    public ViewResult
    RunView() {

        Console.Clear();
        Console.WriteLine(HelpText);
        Console.Write("> ");

        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input)) {
            return ViewResult.MainMenu;
        }

        try {
            return CommandDispatcher.Dispatch(context: _context, input: input);
            }
        catch (Exception ex)
            {
            Console.WriteLine($"Error: {ex.Message}");
            Pause();
            return ViewResult.MainMenu;
            }



        //    ConsoleKey key = Console.ReadKey().Key;
        //    switch (key) {
        //        case ConsoleKey.D1:
        //            IView OverviewView = _context.GetOverviewViewOblect();
        //            Console.Clear();
        //            OverviewView.RunView();
        //            Pause();
        //            break;
        //        case ConsoleKey.D2:
        //            IView LastHandsView = _context.GetLastHandsViewObject();
        //            Console.Clear();
        //            LastHandsView.RunView();
        //            Pause();
        //            break;
        //        case ConsoleKey.D3:
        //            IView DeleteHandsView = _context.GetDeleteHandsViewObject();
        //            Console.Clear();
        //            DeleteHandsView.RunView();
        //            Pause();
        //            break;
        //        case ConsoleKey.Q:
        //            return ViewResult.Exit;

        //}
        return ViewResult.MainMenu;
    }
    private void Pause() {
        Console.WriteLine("\nPress any key to return to the main menu...");
        Console.ReadKey(true);
    }
}

public class OverviewView : IView {
    private CommandContext _context { get; }
    public OverviewView(CommandContext context) {
        _context = context;
    }
    public ViewResult
    RunView() {
        var (totalHands, totalPlayers) = _context.Database.GetOverviewStats();
        Console.WriteLine($"Total in database:{totalHands}hands and {totalPlayers} players");
        return ViewResult.Success;
    }
}

public class LastHandsView : IView {
    private CommandContext _context { get; }
    public LastHandsView(CommandContext context) {
        _context = context;
    }
    public ViewResult
    RunView() {
        var result = _context.Database.GetIdCardsStackOfHeroHandler(10);
        foreach (var (handId, hero) in result) {
            var cards = string.Join(" ", hero.DealtCards.Select(card => card.ToString()));
            Console.WriteLine($"HandId: {handId}, Hero nickname: {hero.Nickname}, Cards: {cards}, StackSize: {hero.StackSize}");

        }
        return ViewResult.Success;
    }

public class DeleteHandsView : IView {
    private CommandContext _context { get; }
    public DeleteHandsView(CommandContext context) {
        _context = context;
    }
    public ViewResult
    RunView() {
        Console.WriteLine("Enter Hand ID to delete:");
        var input = Console.ReadLine();
        if (long.TryParse(input, out long handId)) {
            bool success = _context.Database.DeleteHandByIdHandler(handId);
            if (success) {
                Console.WriteLine($"Hand {handId} deleted successfully.");
            }
            else {
                Console.WriteLine($"Hand {handId} not found.");
            }
        }
        else {
            Console.WriteLine("Invalid Hand ID.");
        }
        return ViewResult.Success;
    }
}
}















