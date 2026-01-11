namespace PokerDatabaseCLI.Data;

public  class Database {

    private ImmutableList<HandHistory> _handsDatabase = ImmutableList<HandHistory>.Empty;
    private ImmutableList<long> _deletedHandsIds  = ImmutableList<long>.Empty;
    //acces to hands
    public ImmutableList<HandHistory> HandsDatabase => _handsDatabase;
    public ImmutableList<long> DeletedHandsIds => _deletedHandsIds;
    //event listener for adding hands
    public event Action<long>? HandsAdded; 
      public void
    AddHands( ImmutableList<HandHistory> handsToAdd) {
    _handsDatabase = [.._handsDatabase, ..handsToAdd];
    HandsAdded?.Invoke(handsToAdd.Count);
    }
    public bool
    DeleteHandById(long handId) {
        var handToDelete = _handsDatabase.FirstOrDefault(hand => hand.HandId == handId);
        if (handToDelete != null) {
            _handsDatabase = [.. _handsDatabase.Where(hand => hand.HandId != handId)];
            _deletedHandsIds = [.. _deletedHandsIds, handId];
            return true;
        }
        return false;
    }
    public (long totalHands, long totalPlayers)
    GetOverviewStats() {
        long totalHands = _handsDatabase.Count;
        long totalPlayers = _handsDatabase.SelectMany(h => h.Players).Distinct().Count();
        return (totalHands, totalPlayers);
    }
//TODO should to be simplified
    public IEnumerable<(long HandId,  HandHistoryPlayer heroLine)>
    GetIdCardsStackOfHero (int requiredHands) {
        var result= ImmutableList<(long HandId, HandHistoryPlayer Hero)>.Empty;
        string heroName = null;
        var databaseOrdered= _handsDatabase.OrderByDescending(hand => hand.HandId);
        var heroHands = databaseOrdered.TakeWhileAccum(hand=>  {
            hand.TryGetHeroPlayer(out var heroList);
            if (heroName == null)
                heroName = heroList.Nickname;
                return heroList.Nickname == heroName;
            }, requiredHands);
        foreach( var hand in heroHands) {
            hand.TryGetHeroPlayer(out var heroList);
            result=[..result, (hand.HandId,  heroList)];
        }
        return result;
    }
    public IEnumerable<string>
    GetDeletedHadsNumbers() {
        foreach (var handId in _deletedHandsIds) {
            yield return handId.ToString();
        }
    }
    
}

