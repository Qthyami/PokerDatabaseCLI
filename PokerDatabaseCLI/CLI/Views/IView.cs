using System;
using System.Collections.Generic;
using System.Text;

namespace PokerDatabaseCLI.CLI.Views;

public interface IView {
ViewResult RunView();
}

public enum ViewResult {
 Success,
    Retry,
    Exit,
    MainMenu
}
