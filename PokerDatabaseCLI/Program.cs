class Program
{
    static void Main(string[] args) {
        var database= new Database();
        var context = new CommandContext(database);
        IView StartupView = context.GetStartUpViewObject();
        StartupView.RunView();
        IView MainView= context.GetMainViewObject();
        MainView.RunView();
    }
}
