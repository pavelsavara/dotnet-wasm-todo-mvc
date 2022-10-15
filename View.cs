namespace TodoMVC;

public partial class View
{
    private Template Template { get; }

    public View(Template template)
    {
        Template = template;
        Interop.InitView();
    }

    public void ClearNewTodo() => Interop.ClearNewTodo();
    public void ShowItems(List<Todo> items) => Interop.ShowItems(Template.ItemList(items));
    public void SetItemsLeft(int activeTodos) => Interop.SetItemsLeft(Template.ItemCounter(activeTodos));
    public void SetClearCompletedButtonVisibility(bool visible) => Interop.SetClearCompletedButtonVisibility(visible);
    public void SetCompleteAllCheckbox(bool ischecked) => Interop.SetCompleteAllCheckbox(ischecked);
    public void SetItemComplete(long id, bool completed) => Interop.SetItemComplete(id, completed);
    public void SetMainVisibility(bool visible) => Interop.SetMainVisibility(visible);
    public void RemoveItem(long id) => Interop.RemoveItem(id);
    public void EditItemDone(long id, string title) => Interop.EditItemDone(id, title);
    public void UpdateFilterButtons(string route) => Interop.UpdateFilterButtons(route);
    public void BindAddItem(Action<string> handler) => Interop.BindAddItem(handler);
    public void BindEditItemSave(Action<long, string> handler) => Interop.BindEditItemSave(handler);
    public void BindEditItemCancel(Action<long> handler) => Interop.BindEditItemCancel(handler);
    public void BindRemoveItem(Action<long> handler) => Interop.BindRemoveItem(handler);
    public void BindToggleItem(Action<long, bool> handler) => Interop.BindToggleItem(handler);
    public void BindRemoveCompleted(Action handler) => Interop.BindRemoveCompleted(handler);
    public void BindToggleAll(Action<bool> handler) => Interop.BindToggleAll(handler);

    public static partial class Interop
    {
        [JSImport("initView", "todoMVC/view.js")]
        public static partial void InitView();
        [JSImport("clearNewTodo", "todoMVC/view.js")]
        public static partial void ClearNewTodo();
        [JSImport("showItems", "todoMVC/view.js")]
        public static partial void ShowItems(string itemsHtml);
        [JSImport("setItemsLeft", "todoMVC/view.js")]
        public static partial void SetItemsLeft(string itemsLeftHtml);
        [JSImport("setClearCompletedButtonVisibility", "todoMVC/view.js")]
        public static partial void SetClearCompletedButtonVisibility(bool visible);
        [JSImport("setCompleteAllCheckbox", "todoMVC/view.js")]
        public static partial void SetCompleteAllCheckbox(bool ischecked);
        [JSImport("setItemComplete", "todoMVC/view.js")]
        public static partial void SetItemComplete([JSMarshalAs<JSType.Number>] long id, bool completed);
        [JSImport("setMainVisibility", "todoMVC/view.js")]
        public static partial void SetMainVisibility(bool visible);
        [JSImport("removeItem", "todoMVC/view.js")]
        public static partial void RemoveItem([JSMarshalAs<JSType.Number>] long id);
        [JSImport("editItemDone", "todoMVC/view.js")]
        public static partial void EditItemDone([JSMarshalAs<JSType.Number>] long id, string title);
        [JSImport("updateFilterButtons", "todoMVC/view.js")]
        public static partial void UpdateFilterButtons(string route);
        [JSImport("bindAddItem", "todoMVC/view.js")]
        public static partial void BindAddItem([JSMarshalAs<JSType.Function<JSType.String>>] Action<string> handler);
        [JSImport("bindEditItemSave", "todoMVC/view.js")]
        public static partial void BindEditItemSave([JSMarshalAs<JSType.Function<JSType.Number, JSType.String>>] Action<long, string> handler);
        [JSImport("bindEditItemCancel", "todoMVC/view.js")]
        public static partial void BindEditItemCancel([JSMarshalAs<JSType.Function<JSType.Number>>] Action<long> handler);
        [JSImport("bindRemoveCompleted", "todoMVC/view.js")]
        public static partial void BindRemoveCompleted([JSMarshalAs<JSType.Function>] Action handler);
        [JSImport("bindRemoveItem", "todoMVC/view.js")]
        public static partial void BindRemoveItem([JSMarshalAs<JSType.Function<JSType.Number>>] Action<long> handler);
        [JSImport("bindToggleAll", "todoMVC/view.js")]
        public static partial void BindToggleAll([JSMarshalAs<JSType.Function<JSType.Boolean>>] Action<bool> handler);
        [JSImport("bindToggleItem", "todoMVC/view.js")]
        public static partial void BindToggleItem([JSMarshalAs<JSType.Function<JSType.Number, JSType.Boolean>>] Action<long, bool> handler);
    }
}
