using System;
using System.Collections.Generic;
using System.Text;
using PokerDatabaseCLI.HandHistoryParser;

namespace PokerDatabaseCLI.Commands;

public static class Handlers {
public static void
AddHandsFromDirectory(this Database database, string path) {
    var hands =path.GetHandHistoriesFromDirectory().ToImmutableList();
     database.AddHands(hands);
    }

public static IEnumerable<(long HandId,  HandHistoryPlayer heroLine)>
GetIdCardsStackOfHeroHandler(this Database database, int requiredHands)=>
    database.GetIdCardsStackOfHero(10);
 
public static bool
DeleteHandByIdHandler(this Database database, long handId) =>database.DeleteHandById(handId);
}

