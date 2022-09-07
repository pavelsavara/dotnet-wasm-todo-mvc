using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TodoMVC
{
    public class Controller
    {
        private string _activeRoute;
        private string _lastActiveRoute;
        private Store store { get; }
        private View view { get; }

        public Controller(Store store, View view)
        {
            this.store = store;
            this.view = view;

            view.BindAddItem(AddItem);
        }

        public void AddItem(string title)
        {
            store.Insert(new Item
            {
                Id = DateTime.UtcNow.Ticks,
                Title = title,
                Completed = false

            });

            view.ClearNewTodo();
            _filter(true);
        }

        void _filter(bool force)
        {
            var route = _activeRoute;

            if (force || _lastActiveRoute != "" || _lastActiveRoute != route)
            {
                var todos = route switch
                {
                    "active" => store.Find(null, null, false),
                    "completed" => store.Find(null, null, true),
                    _ => store.Find(null, null, null),
                };
                view.ShowItems(todos);
            }

            var count = store.Count();
            view.SetItemsLeft(count.active);
            view.SetClearCompletedButtonVisibility(count.completed != 0);
            view.SetCompleteAllCheckbox(count.completed == count.total);
            view.SetMainVisibility(count.total != 0);
            _lastActiveRoute = route;
        }
    }
}
    /*
    constructor(store, view) {
        view.bindAddItem(addItem.bind(this));
        view.bindEditItemSave(editItemSave.bind(this));
        view.bindEditItemCancel(editItemCancel.bind(this));
        view.bindRemoveItem(removeItem.bind(this));
        view.bindToggleItem((id, completed) => {
            toggleCompleted(id, completed);
            _filter();
        });
        view.bindRemoveCompleted(removeCompletedItems.bind(this));
        view.bindToggleAll(toggleAll.bind(this));

        _activeRoute = '';
        _lastActiveRoute = null;
    }

    setView(raw) {
        const route = raw.replace(/ ^#\//, '');
            _activeRoute = route;
        _filter();
        view.updateFilterButtons(route);
    }


    editItemSave(id, title)
    {
        if (title.length)
        {
            store.update({ id, title}, () =>
            {
                view.editItemDone(id, title);
            });
        }
        else
        {
            removeItem(id);
        }
    }

    editItemCancel(id)
    {
        store.find({ id}, data =>
        {
            const title = data[0].title;
            view.editItemDone(id, title);
        });
    }

    removeItem(id)
    {
        store.remove({ id}, () =>
        {
            _filter();
            view.removeItem(id);
        });
    }

    removeCompletedItems()
    {
        store.remove({ completed: true}, _filter.bind(this));
    }

    toggleCompleted(id, completed)
    {
        store.update({ id, completed}, () =>
        {
            view.setItemComplete(id, completed);
        });
    }

    toggleAll(completed)
    {
        store.find({ completed: !completed}, data =>
        {
            for (let { id}
            of data) {
                toggleCompleted(id, completed);
            }
        });

        _filter();
    }


}

*/