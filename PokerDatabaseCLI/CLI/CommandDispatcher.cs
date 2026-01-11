
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
            case "q":
            case "exit": return ViewResult.Exit;
            case "showstats": {
                    IView overViewHandsView = context.GetOverviewViewObject();
                    return RunViewWithPause(overViewHandsView); //не получилсь применить чейниинг :(

                }
            case "lasthands": {
                    var count = parser.ReadOptionalInt(defaultValue: 10); //нормально ли делать такой получейнинг или лучше с 2мя аргументами-> var count = ReadOptionalInt(parser, 10); ??                
                    IView lastHandsView = context.GetLastHandsViewObject(count);
                    return RunViewWithPause(lastHandsView);
                }
            case "deletehand": {  //вынос логики в отдельные функции усложнил читаемость
                    parser.SkipSpaces();
                    if (!parser.EnsureDeleteHandToken())
                        return ViewResult.MainMenu;
                }

                if (!parser.EnsureNextIsHandNumber()) {
                    return ViewResult.MainMenu;
                }

                var handNumber = parser.ReadLong();
                IView deleteHandView = context.GetDeleteHandsViewObject(handNumber);
                Console.Clear();
                deleteHandView.RunView();
                Pause();
                return ViewResult.Success;

            default: {
                    Console.WriteLine($"Unknown command: {command}");
                    Pause();
                    return ViewResult.MainMenu;
                }
        }
    }
    //HElPER METHODS
    public static void
    Pause() {
        Console.WriteLine("\nPress any key to return to the main menu...");
        Console.ReadKey(true);
    }

    public static ViewResult
    RunViewWithPause(IView view) {
        Console.Clear();
        view.RunView();
        Pause();
        return ViewResult.Success;
    }

    public static int
    ReadOptionalInt(this FluentParser parser, int defaultValue) {
        parser.SkipSpaces();
        return (parser.HasCurrent && char.IsDigit(parser.NextChar))
            ? parser.ReadInt()
            : defaultValue;
    }

    private static bool
    EnsureNextIsHandNumber(this FluentParser parser) {
        parser.SkipSpaces();
        if (!parser.HasCurrent || !char.IsDigit(parser.NextChar)) {
            Console.WriteLine("Error: Missing or invalid hand number.");
            Pause();
            return false;
        }
        return true;
    }

    private static bool
    EnsureDeleteHandToken(this FluentParser parser) {
        parser.SkipSpaces();
        if (!parser.TryReadWord(out var tokenCommand) ||
            !(tokenCommand.Equals("--handnumber", StringComparison.OrdinalIgnoreCase) ||
              tokenCommand.Equals("-n", StringComparison.OrdinalIgnoreCase))) {
            Console.WriteLine(
                "Error: Use DeleteHand --handnumber <number> or DeleteHand -n <number>"
            );
            Pause();
            return false;
        }
        return true;
    }

}


