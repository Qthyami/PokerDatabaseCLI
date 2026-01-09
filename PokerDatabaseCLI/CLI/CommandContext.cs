using System;
using System.Collections.Generic;
using System.Text;

namespace PokerDatabaseCLI.CLI;

public  class CommandContext {
    public Database Database { get;}
    public CommandContext(Database database) {
    Database = database;
}
}
