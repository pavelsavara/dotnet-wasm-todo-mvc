using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TodoMVC
{
    public class Template
    {
        public string ItemList(List<Item> items)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in items)
            {
                sb.Append($@"
<li data-id=""{item.Id}""{(item.Completed!.Value ? " class=\"completed\"" : "")}>
	<div class=""view"">
		<input class=""toggle"" type=""checkbox"" {(item.Completed.Value ? "checked" : "")}>
		<label>{WebUtility.HtmlEncode(item.Title ?? "")}</label>
		<button class=""destroy""></button>
	</div>
</li>
");
            }
            return sb.ToString();
        }

        public string itemCounter(int activeTodos)
        {
            return $"{activeTodos} item{(activeTodos != 1 ? "s" : "")} left";
        }
    }
}
