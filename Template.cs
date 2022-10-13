namespace TodoMVC;

public class Template
{
    public static string ItemList(List<Todo> items)
    {
        var sb = new StringBuilder();
        foreach (var item in items)
        {
            sb.Append($@"
<li data-id=""{item.Id}""{(item.Completed!.Value ? " class=\"completed\"" : string.Empty)}>
	<div class=""view"">
		<input class=""toggle"" type=""checkbox"" {(item.Completed.Value ? "checked" : string.Empty)}>
		<label>{WebUtility.HtmlEncode(item.Title ?? string.Empty)}</label>
		<button class=""destroy""></button>
	</div>
</li>
");
        }
        return sb.ToString();
    }

    public static string ItemCounter(int activeTodos) => $"{activeTodos} item{(activeTodos != 1 ? "s" : string.Empty)} left";
}
