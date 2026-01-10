using System;
using System.Collections.Generic;
using System.Text;

namespace PokerDatabaseCLI.CLI;

public static class CommandDictionary {
public static readonly Dictionary<string, string> Commands = new() {
    { "help", "Displays this help message." },
    { "exit", "Exits the application." },
    { "overview", "Displays an overview of the poker database." },
    { "lasthands", "Displays the last hands played." },
    { "deletehands", "Deletes specified hands from the database." }
}
