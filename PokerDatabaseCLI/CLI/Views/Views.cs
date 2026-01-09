
namespace PokerDatabaseCLI.CLI.Views;

public class StartupView : IView {
    
    private CommandContext _context {get;}
    public StartupView (CommandContext context) {
            _context= context;
        }
    public void
    RunView() {
        Console.WriteLine("Welcome to Poker Database CLI!");
        Console.WriteLine("Please enter path to hand history folder:");
        string folderPath = Console.ReadLine();
        if (string.IsNullOrEmpty(folderPath)) {
            Console.WriteLine("Invalid folder path");
            return;
        }
         _context.Database.HandsAdded += count => {
        Console.WriteLine($"{count} hands added to the database.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
      };
        _context.Database.AddHandsFromDirectory(folderPath); //асинхронная логика не нужна я думаю
    }
   
}

public class MainView : IView {
    private CommandContext _context { get; }
    public MainView(CommandContext context) {
        _context = context;
    }
    public void
    RunView() {

        while (true) {
            Console.Clear();
            Console.WriteLine(@"Please select an operation:\n
            1 - Show total hands and players 
            2 - Show hero last 10 hands
            3 - Delete hand
            Q - Exit");
            ConsoleKey key = Console.ReadKey().Key;
            switch (key) {
                case ConsoleKey.D1:
                    IView OverviewView =_context.GetOverviewViewOblect();
                    OverviewView.RunView();
                    Pause();
                    break;
                case ConsoleKey.D2:
                    IView LastHandsView =_context.GetLastHandsViewObject();
                    LastHandsView.RunView();
                    Pause();
                    break;
                case ConsoleKey.D3:
                    IView DeleteHandsView=_context.GetDeleteHandsViewObject();
                    DeleteHandsView.RunView();
                    Pause();
                    break;
                case ConsoleKey.Q:
                    return;
            }
        }
    }
    private void Pause() {
        Console.WriteLine("\nPress any key to return to the main menu...");
        Console.ReadKey(true);
    }
}
                            
public class OverviewView : IView {
    private CommandContext _context { get; }
    public OverviewView (CommandContext context) {
        _context = context;
    }
    public  void 
    RunView() {
    }
} 

public class LastHandsView : IView {
    private CommandContext _context { get; }
    public LastHandsView (CommandContext context) {
        _context = context;
    }
    public  void 
    RunView() {
    }
} 

public class DeleteHandsView : IView {
    private CommandContext _context { get; }
    public DeleteHandsView (CommandContext context) {
        _context = context;
    }
    public  void 
    RunView() {
    }
} 

                         
                         

                          


                          


                          
                          
        

