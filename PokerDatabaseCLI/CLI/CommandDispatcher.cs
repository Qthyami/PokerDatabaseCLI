using System;
using System.Collections.Generic;
using System.Text;

namespace PokerDatabaseCLI.CLI;

 public static class CommandDispatcher {
private static void Pause() {
        Console.WriteLine("\nPress any key to return to the main menu...");
        Console.ReadKey(true);
    }

public static ViewResult
 Dispatch(this CommandContext context, string input) {
        if (string.IsNullOrWhiteSpace(input))
                return ViewResult.MainMenu;

        var parser = new FluentParser(input);
        string command="";

        if (parser.TryReadWord(out command)) {
            command = command.ToLowerInvariant();
        }
        else if (parser.HasCurrent) {
            command = parser.Read(1).ToLowerInvariant();
        }
        switch(command) {
            case "q": return ViewResult.Exit;
                case "exit": return ViewResult.Exit;
                case "showstats" : {
                 IView LastHandsView = context.GetOverviewViewOblect();
                    Console.Clear();
                    LastHandsView.RunView();
                    Pause();
                    return ViewResult.Success;
                }
                case "lasthands" : {
                IView LastHandsView = 
            }

        }




    }


}
