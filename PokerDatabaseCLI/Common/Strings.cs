
namespace PokerDatabaseCLI.Common;

public static class Strings {
public static IEnumerable<string>
SplitWords(this string text)=>
    text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
}
