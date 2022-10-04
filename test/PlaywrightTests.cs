using Microsoft.Playwright;
using Xunit;

public class PlaywrightTests
{
    [Fact]
    public async Task Full()
    {
#if DEBUG
        var headless = false;
        var slowMo = 250;
#else
        var headless = true;
        var slowMo = 0;
#endif

        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new() { Headless = headless, SlowMo = slowMo });
        var page = await browser.NewPageAsync();

        await page.GotoAsync("http://127.0.0.1:9000/index.html");
        Assert.Equal("todos", await page.Locator("h1").TextContentAsync());
        
        // Wait to bind events
        await Task.Delay(1500);

        var todoCount = page.Locator(".todo-count");
        var todoLi = page.Locator(".todo-list li");
        var newTodo = page.Locator(".new-todo");

        // Check we have 0 items
        Assert.Equal("0 items left", await todoCount.TextContentAsync());
        Assert.Equal(0, await todoLi.CountAsync());

        // Add new item
        await newTodo.TypeAsync("Buy milk");
        Assert.Equal("Buy milk", await newTodo.InputValueAsync());
        await newTodo.PressAsync("Tab");

        // Check we have 1 not-completed item
        Assert.Equal(string.Empty, await newTodo.InputValueAsync());
        Assert.Equal("1 item left", await todoCount.TextContentAsync());
        Assert.Equal(1, await todoLi.CountAsync());
        Assert.Equal( "Buy milk", await todoLi.First.Locator("label").TextContentAsync());
        Assert.DoesNotContain("completed", await todoLi.First.GetAttributeAsync("class"));

        // Complete item
        await todoLi.First.Locator("input[type=checkbox]").ClickAsync();

        // Check we have 0 not-completed items
        Assert.Equal("0 items left", await todoCount.TextContentAsync());
        Assert.Equal(1, await todoLi.CountAsync());
        Assert.Contains("completed", await todoLi.First.GetAttributeAsync("class"));

        // Delete item
        await todoLi.First.Locator(".destroy").ClickAsync();

        // Check we have 0 items
        Assert.Equal("0 items left", await todoCount.TextContentAsync());
        Assert.Equal(0, await todoLi.CountAsync());
    }
}
