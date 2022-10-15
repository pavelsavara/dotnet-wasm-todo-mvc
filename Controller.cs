namespace TodoMVC;

public partial class Controller
{
    private string? _activeRoute;
    private string? _lastActiveRoute;
    private Store Store { get; }
    private View View { get; }

    public Controller(Store store, View view)
    {
        Store = store;
        View = view;

        view.BindAddItem(AddItem);
        view.BindEditItemSave(EditItemSave);
        view.BindEditItemCancel(EditItemCancel);
        view.BindRemoveItem(RemoveItem);
        view.BindToggleItem((id, completed) =>
        {
            ToggleCompleted(id, completed);
            Filter(true);
        });
        view.BindRemoveCompleted(RemoveCompletedItems);
        view.BindToggleAll(ToggleAll);

        _activeRoute = string.Empty;
        _lastActiveRoute = null;
    }

    [GeneratedRegex("^#\\/")]
    private static partial Regex GetUrlHashRegex();

    public void SetView(string? urlHash)
    {
        var route = GetUrlHashRegex().Replace(urlHash ?? string.Empty, string.Empty);
        _activeRoute = route;
        Filter();
        View.UpdateFilterButtons(route);
    }

    public void AddItem(string title)
    {
        Store.Insert(new Todo
        {
            Id = DateTime.UtcNow.Ticks / 10000,
            Title = title,
            Completed = false

        });

        View.ClearNewTodo();
        Filter(true);
    }

    public void EditItemSave(long id, string title)
    {
        if (title.Length != 0)
        {
            Store.Update(new Todo { Id = id, Title = title });
            View.EditItemDone(id, title);
        }
        else
        {
            RemoveItem(id);
        }
    }

    public void EditItemCancel(long id)
    {
        var items = Store.Find(id, null, null);
        var title = items[0].Title!;
        View.EditItemDone(id, title);
    }

    public void RemoveItem(long id)
    {
        Store.Remove(id, null, null);
        Filter();
        View.RemoveItem(id);
    }

    public void RemoveCompletedItems()
    {
        Store.Remove(null, null, true);
        Filter(true);
    }

    public void ToggleCompleted(long id, bool completed)
    {
        Store.Update(new Todo { Id = id, Completed = completed });
        View.SetItemComplete(id, completed);
    }

    public void ToggleAll(bool completed)
    {
        var todos = Store.Find(null, null, !completed);
        foreach (var item in todos)
        {
            ToggleCompleted(item.Id, completed);
        }
        Filter(true);
    }

    private void Filter(bool force = false)
    {
        var route = _activeRoute;

        if (force || _lastActiveRoute != string.Empty || _lastActiveRoute != route)
        {
            var todos = route switch
            {
                "active" => Store.Find(null, null, false),
                "completed" => Store.Find(null, null, true),
                _ => Store.Find(null, null, null),
            };
            View.ShowItems(todos);
        }

        var (total, completed, active) = Store.Count();
        View.SetItemsLeft(active);
        View.SetClearCompletedButtonVisibility(completed != 0);
        View.SetCompleteAllCheckbox(completed == total);
        View.SetMainVisibility(total != 0);
        _lastActiveRoute = route;
    }
}
