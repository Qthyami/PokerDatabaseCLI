
namespace PokerDatabaseCLI.CLI;

public static class CommandDispatcher {
    public static ViewResult
     Dispatch(this CommandContext context, string input) {
        if (string.IsNullOrWhiteSpace(input))
            return ViewResult.MainMenu;
        var parser = new FluentParser(input);
        string command = "";
        parser.SkipSpaces();
        if (parser.TryReadWordFromTerminal(out command)) {
            command = command.ToLowerInvariant();
        }
        else if (parser.HasCurrent) {
            command = parser.Read(1).ToLowerInvariant();
        }
        switch (command) {
            case "q": return ViewResult.Exit;
            case "exit": return ViewResult.Exit;
            case "showstats": {
                    IView LastHandsView = context.GetOverviewViewOblect();
                    Console.Clear();
                    LastHandsView.RunView();
                    Pause();
                    return ViewResult.Success;
                }
            case "lasthands": {
                    int count = 10;
                    parser.SkipSpaces();
                    if (parser.HasCurrent && char.IsDigit(parser.NextChar))
                        count = parser.ReadInt();
                    IView LastHandsView = context.GetLastHandsViewObject(count);
                    Console.Clear();
                    LastHandsView.RunView();
                    Pause();
                    return ViewResult.Success;
                }
            case "deletehand": {
                    parser.SkipSpaces();
                    if (!parser.TryReadWord(out string nextToken) || !(nextToken.ToLowerInvariant() == "--handnumber" || nextToken == "-n")) {
                        Console.WriteLine("Error: Use DeleteHand --handnumber <number> or DeleteHand n <number> command");
                        Pause();
                        return ViewResult.MainMenu;
                    }
                    parser.SkipSpaces();
                    if (!parser.HasCurrent || !char.IsDigit(parser.NextChar)) {
                        Console.WriteLine("Error: Missing or invalid hand number.");
                        Pause();
                        return ViewResult.MainMenu;
                    }
                    var handNumber = parser.ReadLong();
                    IView DeleteHandView = context.GetDeleteHandsViewObject(handNumber);
                    Console.Clear();
                    DeleteHandView.RunView();
                    Pause();
                    return ViewResult.Success;
                }
            default: {
                    Console.WriteLine($"Unknown command: {command}");
                    Pause();
                    return ViewResult.MainMenu;
                }
        }
    }
    private static void
    Pause() {
        Console.WriteLine("\nPress any key to return to the main menu...");
        Console.ReadKey(true);
    }
}


