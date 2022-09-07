using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Diagnostics.CodeAnalysis;

namespace TodoMVC
{
    public partial class Store
    {
        private List<Item>? liveTodos;

        public void Insert(Item item)
        {
            var todos = Interop.GetLocalStorage();
            todos.Add(item);
            liveTodos = todos;
            Interop.SetLocalStorage(todos);
        }

        public void Update(Item item)
        {
            var todos = Interop.GetLocalStorage();
            var existing = todos.FirstOrDefault(it => it.Id == item.Id);
            if (existing == null)
            {
                todos.Add(item);
            }
            else
            {
                existing.Title = item.Title;
                existing.Completed = item.Completed;
            }
            liveTodos = todos;
            Interop.SetLocalStorage(todos);
        }

        public void RemoveById(long id)
        {
            var todos = Interop.GetLocalStorage();
            liveTodos = todos.Where(item => item.Id != id).ToList();
            Interop.SetLocalStorage(todos);
        }

        public List<Item> Find(long? id, string? title, bool? completed)
        {
            var todos = Interop.GetLocalStorage();
            return todos.Where(it =>
            {
                if (id.HasValue && it.Id != id.Value) return false;
                if (title != null && it.Title != title) return false;
                if (completed.HasValue && it.Completed != completed.Value) return false;
                return true;
            }).ToList();
        }

        public (int total, int completed, int active) Count()
        {
            var todos = Interop.GetLocalStorage();
            return (todos.Count, todos.Where(it => it.Completed).Count(), todos.Where(it => !it.Completed).Count());
        }

        public static partial class Interop
        {
            [SuppressMessage("Trimming", "IL2026", Justification = "")]
            [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
            [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
            public static List<Item> GetLocalStorage()
            {
                var json = getLocalStorage();
                return JsonSerializer.Deserialize<List<Item>>(json);
            }

            [SuppressMessage("Trimming", "IL2026", Justification = "")]
            public static void SetLocalStorage(List<Item> items)
            {
                var json = JsonSerializer.Serialize(items);
                setLocalStorage(json);
            }

            [JSImport("store.setLocalStorage", "todoMVC")]
            internal static partial void setLocalStorage(string json);

            [JSImport("store.getLocalStorage", "todoMVC")]
            internal static partial string getLocalStorage();
        }
    }
}
