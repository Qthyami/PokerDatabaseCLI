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
        _context.Database.AddHandsFromDirectory(path: folderPath);//асинхронная логика не нужна я думаю
        return ViewResult.Success;
    }

}

public class MainView : IView {
    private CommandContext _context { get; }
    public MainView(CommandContext context) {
        _context = context;
    }
public static void ShowHelp()
{
    Console.WriteLine("Available commands:");
    Console.WriteLine($"{ "showstats".PadRight(40) }: Show total hands and players in database");
    Console.WriteLine($"{ "LastHands <number>".PadRight(40) }: Show last <number> hands of a hero");
    Console.WriteLine($"{ "DeleteHand --HandNumber <id> | -n <id>".PadRight(40) }: Delete a hand by ID");
    Console.WriteLine($"{ "Exit | -q".PadRight(40) }: Exit the program");
 }
    public ViewResult
    RunView() {

        Console.Clear();
        ShowHelp();
        Console.Write("> ");

        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input)) {
            return ViewResult.MainMenu;
        }
        try {
            return CommandDispatcher.Dispatch(context: _context, input: input);
        }
        catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            Pause();
            return ViewResult.MainMenu;
        }
    }
    private static void Pause() {
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
    private int _count;

    public LastHandsView(CommandContext context, int count) {
        _context = context;
        _count= count;
    }
    public ViewResult
    RunView() {
        var result = _context.Database.GetIdCardsStackOfHeroHandler(requiredHands: _count);
        foreach (var (handId, hero) in result) {
            var cards = string.Join(" ", hero.DealtCards.Select(card => card.ToString()));
            Console.WriteLine($"HandId: {handId}, Hero nickname: {hero.Nickname}, Cards: {cards}, StackSize: {hero.StackSize}");
        }
        return ViewResult.Success;
    }

public class DeleteHandsView : IView {
    private CommandContext _context { get; }
    private long _handId;
    public DeleteHandsView(CommandContext context, long handId ) {
        _context = context;
        _handId = handId;
    }
public ViewResult
RunView() {
    long handIdParsed = _handId;
    bool success = _context.Database.DeleteHandByIdHandler(handId: handIdParsed);
    var deletedHandsNumber=_context.Database.DeletedHandsIds.ToList();
    if (success) {
        Console.WriteLine($"Hand {handIdParsed} deleted successfully.");
        Console.WriteLine("Deleted Hands Ids: " + string.Join(", ", deletedHandsNumber));
    }
    else {
        Console.WriteLine($"Hand {handIdParsed} not found.");
    }
    return ViewResult.Success;
}
    }
}