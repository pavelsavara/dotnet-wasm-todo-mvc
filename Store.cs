namespace TodoMVC;

public partial class Store
{
    #region in-memory
    private List<Todo>? _liveTodos;
    private List<Todo> GetLocalStorage() => _liveTodos ?? Interop.GetLocalStorage();

    private void SetLocalStorage(List<Todo> items)
    {
        _liveTodos = items;
        Interop.SetLocalStorage(items);
    }

    public void Insert(Todo todo)
    {
        var todos = GetLocalStorage();
        todos.Add(todo);
        SetLocalStorage(todos);
    }
    #endregion

    public void Update(Todo todo)
    {
        var todos = GetLocalStorage();
        var existing = todos.FirstOrDefault(t => t.Id == todo.Id);
        if (existing is null)
        {
            todos.Add(todo);
        }
        else
        {
            if (todo.Title is not null)
            {
                existing.Title = todo.Title;
            }
            if (todo.Completed is not null)
            {
                existing.Completed = todo.Completed;
            }
        }
        SetLocalStorage(todos);
    }

    public void Remove(long? id, string? title, bool? completed)
    {
        var todos = GetLocalStorage();
        todos = todos.Where(t => t switch
        {
            _ when id is not null && t.Id == id => false,
            _ when title is not null && t.Title == title => false,
            _ when completed is not null && t.Completed == completed => false,
            _ => true
        }).ToList();
        SetLocalStorage(todos);
    }

    public List<Todo> Find(long? id, string? title, bool? completed)
    {
        var todos = GetLocalStorage();
        return todos.Where(t => t switch
            {
                _ when id is not null && t.Completed != completed => false,
                _ when title is not null && t.Title != title => false,
                _ => completed is null || t.Completed == completed,
            }).ToList();
    }

    public (int total, int completed, int active) Count()
    {
        var todos = GetLocalStorage();
        return (todos.Count, todos.Count(t => t is { Completed: true }), todos.Count(todo => todo is not { Completed: true }));
    }

    public static partial class Interop
    {
        [JsonSerializable(typeof(List<Todo>))]
        private partial class ItemListSerializerContext : JsonSerializerContext { }

        public static List<Todo> GetLocalStorage()
        {
            var json = _getLocalStorage();
            return JsonSerializer.Deserialize(json, ItemListSerializerContext.Default.ListTodo) ?? new List<Todo>();
        }

        public static void SetLocalStorage(List<Todo> items)
        {
            var json = JsonSerializer.Serialize(items, ItemListSerializerContext.Default.ListTodo);
            _setLocalStorage(json);
        }

        [JSImport("setLocalStorage", "todoMVC/store.js")]
        internal static partial void _setLocalStorage(string json);

        [JSImport("getLocalStorage", "todoMVC/store.js")]
        internal static partial string _getLocalStorage();
    }
}
