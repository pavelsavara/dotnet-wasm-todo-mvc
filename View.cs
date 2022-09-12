using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;

namespace TodoMVC
{
    public partial class View
    {
        private Template template { get; }

        public View(Template template)
        {
            this.template = template;
            Interop.initView();
        }

        public void ClearNewTodo() => Interop.clearNewTodo();
        public void ShowItems(List<Item> items) => Interop.showItems(template.ItemList(items));
        public void SetItemsLeft(int activeTodos) => Interop.setItemsLeft(template.itemCounter(activeTodos));
        public void SetClearCompletedButtonVisibility(bool visible) => Interop.setClearCompletedButtonVisibility(visible);
        public void SetCompleteAllCheckbox(bool ischecked) => Interop.setCompleteAllCheckbox(ischecked);
        public void SetItemComplete(long id, bool completed) => Interop.setItemComplete(id, completed);
        public void SetMainVisibility(bool visible) => Interop.setMainVisibility(visible);
        public void RemoveItem(long id) => Interop.removeItem(id);
        public void EditItemDone(long id, string title) => Interop.editItemDone(id, title);
        public void UpdateFilterButtons(string route) => Interop.updateFilterButtons(route);

        public void BindAddItem(Action<string> handler) => Interop.bindAddItem(handler);
        public void BindEditItemSave(Action<long, string> handler) => Interop.bindEditItemSave(handler);
        public void BindEditItemCancel(Action<long> handler) => Interop.bindEditItemCancel(handler);
        public void BindRemoveItem(Action<long> handler) => Interop.bindRemoveItem(handler);
        public void BindToggleItem(Action<long,bool> handler) => Interop.bindToggleItem(handler);
        public void BindRemoveCompleted(Action handler) => Interop.bindRemoveCompleted(handler);
        public void BindToggleAll(Action<bool> handler) => Interop.bindToggleAll(handler);

        public static partial class Interop
        {
            [JSImport("initView", "todoMVC/view.js")]
            public static partial void initView();
            [JSImport("clearNewTodo", "todoMVC/view.js")]
            public static partial void clearNewTodo();
            [JSImport("showItems", "todoMVC/view.js")]
            public static partial void showItems(string itemsHtml);
            [JSImport("setItemsLeft", "todoMVC/view.js")]
            public static partial void setItemsLeft(string itemsLeftHtml);
            [JSImport("setClearCompletedButtonVisibility", "todoMVC/view.js")]
            public static partial void setClearCompletedButtonVisibility(bool visible);
            [JSImport("setCompleteAllCheckbox", "todoMVC/view.js")]
            public static partial void setCompleteAllCheckbox(bool ischecked);
            [JSImport("setItemComplete", "todoMVC/view.js")]
            public static partial void setItemComplete([JSMarshalAs<JSType.Number>] long id, bool completed);
            [JSImport("setMainVisibility", "todoMVC/view.js")]
            public static partial void setMainVisibility(bool visible);
            [JSImport("removeItem", "todoMVC/view.js")]
            public static partial void removeItem([JSMarshalAs<JSType.Number>] long id);
            [JSImport("editItemDone", "todoMVC/view.js")]
            public static partial void editItemDone([JSMarshalAs<JSType.Number>] long id, string title);
            [JSImport("updateFilterButtons", "todoMVC/view.js")]
            public static partial void updateFilterButtons(string route);

            [JSImport("bindAddItem", "todoMVC/view.js")]
            public static partial void bindAddItem([JSMarshalAs<JSType.Function<JSType.String>>] Action<string> handler);
            [JSImport("bindEditItemSave", "todoMVC/view.js")]
            public static partial void bindEditItemSave([JSMarshalAs<JSType.Function<JSType.Number, JSType.String>>] Action<long, string> handler);
            [JSImport("bindEditItemCancel", "todoMVC/view.js")]
            public static partial void bindEditItemCancel([JSMarshalAs<JSType.Function<JSType.Number>>] Action<long> handler);
            [JSImport("bindRemoveCompleted", "todoMVC/view.js")]
            public static partial void bindRemoveCompleted([JSMarshalAs<JSType.Function>] Action handler);
            [JSImport("bindRemoveItem", "todoMVC/view.js")]
            public static partial void bindRemoveItem([JSMarshalAs<JSType.Function<JSType.Number>>] Action<long> handler);
            [JSImport("bindToggleAll", "todoMVC/view.js")]
            public static partial void bindToggleAll([JSMarshalAs<JSType.Function<JSType.Boolean>>] Action<bool> handler);
            [JSImport("bindToggleItem", "todoMVC/view.js")]
            public static partial void bindToggleItem([JSMarshalAs<JSType.Function<JSType.Number, JSType.Boolean>>] Action<long, bool> handler);
        }
    }
}
