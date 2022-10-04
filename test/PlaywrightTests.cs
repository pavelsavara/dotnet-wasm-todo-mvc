using Microsoft.Playwright;
using Xunit;

public class PlaywrightTests
{
    [Fact]
    public async Task Full()
    {
#if DEBUG
        var headless = false;
#else
        var headless = true;
#endif

        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new() { Headless = headless });
        var page = await browser.NewPageAsync();

        await page.GotoAsync("http://127.0.0.1:9000/index.html");
        await page.AssertText("h1", "todos");
        
        // Wait to bind events
        await Task.Delay(1500);

        var todoLi = page.Locator(".todo-list li");
        var newTodo = page.Locator(".new-todo");

        // Check we have 0 items
        await page.AssertText(".todo-count", "0 items left");
        Assert.Equal(0, await todoLi.CountAsync());

        // Add new item
        await newTodo.TypeAsync("Buy milk");
        Assert.Equal("Buy milk", await newTodo.InputValueAsync());
        await DebugDelayAsync();
        await newTodo.PressAsync("Tab");
        await DebugDelayAsync();

        // Check we have 1 not-completed item
        Assert.Equal(string.Empty, await newTodo.InputValueAsync());
        await page.AssertText(".todo-count", "1 item left");
        Assert.Equal(1, await todoLi.CountAsync());
        await page.AssertText(todoLi.First.Locator("label"), "Buy milk");
        Assert.DoesNotContain("completed", await todoLi.First.GetAttributeAsync("class"));

        // Complete item
        await todoLi.First.Locator("input[type=checkbox]").ClickAsync();
        await DebugDelayAsync();

        // Check we have 0 not-completed items
        await page.AssertText(".todo-count", "0 items left");
        Assert.Equal(1, await todoLi.CountAsync());
        Assert.Contains("completed", await todoLi.First.GetAttributeAsync("class"));

        // Delete item
        await todoLi.First.Locator(".destroy").ClickAsync();
        await DebugDelayAsync();

        // Check we have 0 items
        await page.AssertText(".todo-count", "0 items left");
        Assert.Equal(0, await todoLi.CountAsync());

        await DebugDelayAsync(5 * 1000);
    }

    private async Task DebugDelayAsync(int millisecondsDelay = 500)
    {
#if DEBUG
        await Task.Delay(millisecondsDelay);
#endif
    }
}

public static class AssertExtensions
{
    public static Task AssertText(this IPage page, string locator, string text) 
        => AssertText(page, page.Locator(locator), text);

    public static async Task AssertText(this IPage page, ILocator locator, string text) 
        => Assert.Equal(text, await locator.TextContentAsync());
}