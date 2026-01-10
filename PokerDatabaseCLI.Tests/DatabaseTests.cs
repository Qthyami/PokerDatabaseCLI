using System.Collections.Immutable;
using NUnit.Framework;
using PokerDatabaseCLI.Data;
namespace PokerDatabaseCLI.Tests;

[TestFixture]
public class DatabaseTests {
    [Test]
    public void
    GetIdCardsStackOfHero_FromDirectory_ReturnsConsistentHeroSequence() {
        var dir = @"C:\\Poker\\1";
        if (!Directory.Exists(dir))
            Assert.Ignore($"Test data directory not found: {dir}");

        var db = new Database();
        var hands = dir.GetHandHistoriesFromDirectory().Take(10).ToImmutableList();
        if (hands.Count == 0)
            Assert.Inconclusive("No hand histories found in the directory");

        db.AddHands(hands);

        var required = Math.Min(10, hands.Count);
        var result = db.GetIdCardsStackOfHero(required).ToList();

        Assert.That(result.Count, Is.LessThanOrEqualTo(required));

        var byId = db.HandsDatabase.ToDictionary(h => h.HandId);
        string? heroName = null;
foreach (var item in result) {
    var handId = item.HandId;
    var hero = item.heroLine; 
    if (heroName == null)
        heroName = hero.Nickname;
    else
        Assert.That(hero.Nickname, Is.EqualTo(heroName), "All results should belong to the same hero");

    Assert.That(hero.DealtCards.Count, Is.GreaterThan(0));
    Assert.That(hero.DealtCards, Is.SameAs(hero.DealtCards));
    Assert.That(hero.StackSize, Is.EqualTo(hero.StackSize));

    TestContext.WriteLine(
        $"HandId: {handId}, Hero: {hero.Nickname}, Cards: {string.Join(",", hero.DealtCards)}, StackSize: {hero.StackSize}");
}
        
        var handIds = result.Select(r => r.HandId).ToList();
        var sorted = handIds.OrderByDescending(x => x).ToList();
        Assert.That(handIds, Is.EqualTo(sorted), "Results should be ordered by HandId descending");
    }
}
