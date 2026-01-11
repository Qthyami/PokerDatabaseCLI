
namespace PokerDatabaseCLI.Commands;

public static class Handlers {
public static void
AddHandsFromDirectory(this Database database, string path) {
    var hands =path.GetHandHistoriesFromDirectory().ToImmutableList();
     database.AddHands(hands);
    }

public static IEnumerable<(long HandId,  HandHistoryPlayer heroLine)>
GetIdCardsStackOfHeroHandler(this Database database, int requiredHands)=>
    database.GetIdCardsStackOfHero(requiredHands:requiredHands);
 
public static bool
DeleteHandByIdHandler(this Database database, long handId) =>database.DeleteHandById(handId:handId);
}

