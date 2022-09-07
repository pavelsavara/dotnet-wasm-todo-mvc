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
        public void BindAddItem(Action<string> handler) => Interop.bindAddItem(handler);
        public void SetItemsLeft(int activeTodos) => Interop.setItemsLeft(template.itemCounter(activeTodos));
        public void SetClearCompletedButtonVisibility(bool visible) => Interop.setClearCompletedButtonVisibility(visible);
        public void SetCompleteAllCheckbox(bool ischecked) => Interop.setCompleteAllCheckbox(ischecked);
        public void SetItemComplete([JSMarshalAs<JSType.Number>] long id, bool completed) => Interop.setItemComplete(id, completed);
        public void SetMainVisibility(bool visible) => Interop.setMainVisibility(visible);



        public static partial class Interop
        {
            [JSImport("view.initView", "todoMVC")]
            public static partial void initView();
            [JSImport("view.clearNewTodo", "todoMVC")]
            public static partial void clearNewTodo();
            [JSImport("view.showItems", "todoMVC")]
            public static partial void showItems(string itemsHtml);
            [JSImport("view.bindAddItem", "todoMVC")]
            public static partial void bindAddItem([JSMarshalAs<JSType.Function<JSType.String>>] Action<string> handler);
            [JSImport("view.setItemsLeft", "todoMVC")]
            public static partial void setItemsLeft(string itemsLeftHtml);
            [JSImport("view.setClearCompletedButtonVisibility", "todoMVC")]
            public static partial void setClearCompletedButtonVisibility(bool visible);
            [JSImport("view.setCompleteAllCheckbox", "todoMVC")]
            public static partial void setCompleteAllCheckbox(bool ischecked);
            [JSImport("view.setItemComplete", "todoMVC")]
            public static partial void setItemComplete([JSMarshalAs<JSType.Number>] long id, bool completed);
            [JSImport("view.setMainVisibility", "todoMVC")]
            public static partial void setMainVisibility(bool visible);


            /*


                        [JSImport("view.bindEditItemCancel", "todoMVC")] 
                        public static partial void bindEditItemCancel(handler);

                        [JSImport("view.bindEditItemSave", "todoMVC")] 
                        public static partial void bindEditItemSave(handler);



                        [JSImport("view.bindRemoveCompleted", "todoMVC")] public static partial void bindRemoveCompleted(handler);
                        [JSImport("view.bindRemoveItem", "todoMVC")] public static partial void bindRemoveItem(handler);
                        [JSImport("view.bindToggleAll", "todoMVC")] public static partial void bindToggleAll(handler);
                        [JSImport("view.bindToggleItem", "todoMVC")] public static partial void bindToggleItem(handler);
                        [JSImport("view.editItem", "todoMVC")] public static partial void editItem(target);
                        [JSImport("view.editItemDone", "todoMVC")] public static partial void editItemDone(id, title);
                        [JSImport("view.removeItem", "todoMVC")] public static partial void removeItem(id);
                        [JSImport("view.updateFilterButtons", "todoMVC")] public static partial void updateFilterButtons(route);
            */
        }
    }
}
