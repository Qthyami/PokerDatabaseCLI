 using PokerDatabaseCLI.CLI.Views;
using System;
using System.Collections.Generic;
using System.Text;
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
GetOverviewViewOblect(this CommandContext context) {
        return new OverviewView(context);
    }
public static IView
GetLastHandsViewObject(this CommandContext context) {
        return new LastHandsView(context);
    }
public static IView
GetDeleteHandsViewObject(this CommandContext context) {
        return new DeleteHandsView(context);
    }
    
}
