namespace PokerDatabaseCLI.CLI;

public static class CommandDispatcher {
    public static ViewResult
     Dispatch(this CommandContext context, string input) {
        if (string.IsNullOrWhiteSpace(value: input))
            return ViewResult.MainMenu;
        var parser = new FluentParser(input);
        string command = "";
        parser.SkipSpaces();
        if (parser.TryReadWordFromTerminal(result: out command)) {
            command = command.ToLowerInvariant();
        }
        else if (parser.HasCurrent) {
            command = parser.Read(count: 1).ToLowerInvariant();
        }
        switch (command) {
            case "q": 
            case "exit": return ViewResult.Exit;
            case "showstats": {
                    IView LastHandsView = context.GetOverviewViewObject();
                    Console.Clear();
                    LastHandsView.RunView();
                    Pause();
                    return ViewResult.Success;
                }
            case "lasthands": {
                    int count = 10;
                    parser.SkipSpaces();
                    if (parser.HasCurrent && char.IsDigit(c: parser.NextChar))
                        count = parser.ReadInt();
                    IView LastHandsView = context.GetLastHandsViewObject(countHands: count);
                    Console.Clear();
                    LastHandsView.RunView();
                    Pause();
                    return ViewResult.Success;
                }
            case "deletehand": {
                    parser.SkipSpaces();
                    if (!parser.TryReadWord(result: out string nextToken) || !(nextToken.ToLowerInvariant() == "--handnumber" || nextToken == "-n")) {
                        Console.WriteLine("Error: Use DeleteHand --HandNumber <number> or DeleteHand -n <number> command");
                        Pause();
                        return ViewResult.MainMenu;
                    }
                    parser.SkipSpaces();
                    if (!parser.HasCurrent || !char.IsDigit(c: parser.NextChar)) {
                        Console.WriteLine("Error: Missing or invalid hand number.");
                        Pause();
                        return ViewResult.MainMenu;
                    }
                    var handNumber = parser.ReadLong();
                    IView DeleteHandView = context.GetDeleteHandsViewObject(handId: handNumber);
                    Console.Clear();
                    DeleteHandView.RunView();
                    Pause();
                    return ViewResult.Success;
                }
            default: {
                    Console.WriteLine(value: $"Unknown command: {command}");
                    Pause();
                    return ViewResult.MainMenu;
                }
        }
    }
    private static void
    Pause() {
        Console.WriteLine(value: "\nPress any key to return to the main menu...");
        Console.ReadKey(intercept: true);
    }
}