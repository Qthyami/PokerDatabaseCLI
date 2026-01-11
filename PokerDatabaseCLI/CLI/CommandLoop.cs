
namespace PokerDatabaseCLI.CLI;

public static class CommandLoop {
    public static
        void Run(this CommandContext context) {
        IView currentView = context.GetStartUpViewObject();
        while (true) {
            var nowView = currentView.RunView();
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


