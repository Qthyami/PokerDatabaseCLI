using System;
using System.Collections.Generic;
using System.Text;
using PokerDatabaseCLI.HandHistoryParser;

namespace PokerDatabaseCLI.Commands;

public static class Handlers {
public static void AddHandsFromDirectory(this Database database, string path) {
       var hands =path.GetHandHistoriesFromDirectory().ToImmutableList();
         database.AddHands(hands);
    }


}
