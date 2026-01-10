using PokerDatabaseCLI.CLI;
using System.Text;

class Program {
    static void 
    Main(string[] args) {
        var database = new Database();
        var context = new CommandContext(database);
       context.Run();
       
    }
}
