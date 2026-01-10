using PokerDatabaseCLI.CLI;

class Program {
    static void 
    Main(string[] args) {
        var database = new Database();
        var context = new CommandContext(database);
        IView currentView = context.GetStartUpViewObject();
        while (true) {
            var nowView=currentView.RunView();
            switch (nowView) {
                case ViewResult.Retry:
                break;
            case ViewResult.StartMenu:
                currentView = context.GetStartUpViewObject();
                break;
            case ViewResult.MainMenu:
                currentView = context.GetMainViewObject();
                break;
            case ViewResult.Success:
                currentView = context.GetMainViewObject();
                break;
            case ViewResult.Exit:
                return;
            }
      
        }
    }
}
