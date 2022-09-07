using System;
using System.Runtime.InteropServices.JavaScript;

namespace TodoMVC
{
    public partial class MainJS
    {
        static Controller controller;

        public static void Main()
        {
            var store = new Store();
            var template = new Template();
            var view = new View(template);
            controller = new Controller(store, view);
            Console.WriteLine("Ready!");
        }

        [JSExport]
        public static void OnHashchange(string url)
        {
            controller.SetView(url);
        }
    }
}