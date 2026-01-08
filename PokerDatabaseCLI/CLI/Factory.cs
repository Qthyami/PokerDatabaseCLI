using System;
using System.Collections.Generic;
using System.Text;

namespace PokerDatabaseCLI.CLI;

public static class Factory {
public static IView
GetMainViewObject() {
    return new StartupView();
}
}
