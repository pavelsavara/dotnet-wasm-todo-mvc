using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace TodoMVC
{
    public partial class MainJS
    {
        static Controller? controller;

        public static async Task Main()
        {
            await JSHost.ImportAsync("todoMVC/store.js", "./store.js");
            await JSHost.ImportAsync("todoMVC/view.js", "./view.js");

            var store = new Store();
            var view = new View(new Template());
            controller = new Controller(store, view);
            Console.WriteLine("Ready!");
        }

        [JSExport]
        public static void OnHashchange(string url)
        {
            controller?.SetView(url);
        }
    }
}