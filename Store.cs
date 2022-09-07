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
        #region in-memory
        private List<Item>? liveTodos;
        private List<Item> GetLocalStorage()
        {
            if (liveTodos!=null) return liveTodos;
            return Interop.GetLocalStorage();
        }

        private void SetLocalStorage(List<Item> items)
        {
            liveTodos = items;
            Interop.SetLocalStorage(items);
        }

        public void Insert(Item item)
        {
            var todos = GetLocalStorage();
            todos.Add(item);
            SetLocalStorage(todos);
        }
        #endregion

        public void Update(Item item)
        {
            var todos = GetLocalStorage();
            var existing = todos.FirstOrDefault(it => it.Id == item.Id);
            if (existing == null)
            {
                todos.Add(item);
            }
            else
            {
                if (item.Title != null)
                {
                    existing.Title = item.Title;
                }
                if (item.Completed.HasValue)
                {
                    existing.Completed = item.Completed;
                }
            }
            SetLocalStorage(todos);
        }


        public void Remove(long? id, string? title, bool? completed)
        {
            var todos = Interop.GetLocalStorage();
            todos = todos.Where(it =>
            {
                if (id.HasValue && it.Id == id.Value) return false;
                if (title != null && it.Title == title) return false;
                if (completed.HasValue && it.Completed!.Value == completed.Value) return false;
                return true;
            }).ToList();
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
            return (todos.Count, todos.Where(it => it.Completed.Value).Count(), todos.Where(it => !it.Completed.Value).Count());
        }

        public static partial class Interop
        {
            [SuppressMessage("Trimming", "IL2026", Justification = "")]
            [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
            [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
            public static List<Item> GetLocalStorage()
            {
                var json = getLocalStorage();
                return JsonSerializer.Deserialize<List<Item>>(json) ?? new List<Item>();
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
