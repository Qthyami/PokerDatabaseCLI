using static PokerDatabaseCLI.CLI.Views.LastHandsView;
namespace PokerDatabaseCLI.CLI;

public static class Factory {
public static IView
GetStartUpViewObject(this CommandContext context) {
    return new StartupView(context);
}
public static IView 
GetMainViewObject(this CommandContext context) {
        return new MainView(context);
    }
public static IView
GetOverviewViewObject(this CommandContext context) {
        return new OverviewView(context);
    }
public static IView
GetLastHandsViewObject(this CommandContext context, int countHands) {
        return new LastHandsView(context, countHands);
    }
public static IView
GetDeleteHandsViewObject(this CommandContext context, long handId) {
        return new DeleteHandsView(context, handId);
    }
}
