namespace TodoMVC;

public partial class MainJs
{
    private static Controller? _controller;

    public static async Task Main()
    {
        if (!OperatingSystem.IsBrowser())
        {
            throw new PlatformNotSupportedException("This demo is expected to run on browser platform");
        }

        await JSHost.ImportAsync("todoMVC/store.js", "./store.js");
        await JSHost.ImportAsync("todoMVC/view.js", "./view.js");

        var store = new Store();
        var view = new View(new Template());
        _controller = new Controller(store, view);
        Console.WriteLine("Ready!");
    }

    [JSExport]
    public static void OnHashchange(string url) => _controller?.SetView(url);
}
