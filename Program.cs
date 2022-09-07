using System;
using TodoMVC;

public partial class MainJS
{
    public static void Main()
    {

        var store = new Store();

        var template = new Template();
        var view = new View(template);

        var controller = new Controller(store, view);

        Console.WriteLine("Hello, World!");
    }
}
