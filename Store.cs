#pragma warning disable IL2026
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
            return Interop.getLocalStorage();
        }

        private void SetLocalStorage(List<Item> items)
        {
            liveTodos = items;
            Interop.setLocalStorage(items);
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
            var todos = GetLocalStorage();
            todos = todos.Where(it =>
            {
                if (id.HasValue && it.Id == id.Value) return false;
                if (title != null && it.Title == title) return false;
                if (completed.HasValue && it.Completed!.Value == completed.Value) return false;
                return true;
            }).ToList();
            SetLocalStorage(todos);
        }

        public List<Item> Find(long? id, string? title, bool? completed)
        {
            var todos = GetLocalStorage();
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
            var todos = GetLocalStorage();
            return (todos.Count, todos.Where(it => it.Completed.Value).Count(), todos.Where(it => !it.Completed.Value).Count());
        }

        public static partial class Interop
        {
            [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
            [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
            public static List<Item> getLocalStorage()
            {
                var json = _getLocalStorage();
                return JsonSerializer.Deserialize<List<Item>>(json) ?? new List<Item>();
            }

            public static void setLocalStorage(List<Item> items)
            {
                var json = JsonSerializer.Serialize(items);
                _setLocalStorage(json);
            }

            [JSImport("store.setLocalStorage", "todoMVC")]
            internal static partial void _setLocalStorage(string json);

            [JSImport("store.getLocalStorage", "todoMVC")]
            internal static partial string _getLocalStorage();
        }
    }
}
