// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

import { dotnet } from './dotnet.js'
import { $on } from './helpers.js';
import {
    setLocalStorage, getLocalStorage
} from './store.js';
import {
    bindAddItem,
    bindEditItemCancel,
    bindEditItemSave,
    bindRemoveCompleted,
    bindRemoveItem,
    bindToggleAll,
    bindToggleItem,
    clearNewTodo,
    editItem,
    editItemDone,
    initView,
    removeItem,
    setClearCompletedButtonVisibility,
    setCompleteAllCheckbox,
    setItemComplete,
    setItemsLeft,
    setMainVisibility,
    showItems,
    updateFilterButtons,
} from './view.js';

let exports;
const onHashchange = () => exports.TodoMVC.MainJS.OnHashchange(document.location.hash);
$on(window, 'hashchange', onHashchange);

const { setModuleImports, getAssemblyExports, getConfig } = await dotnet.create();

setModuleImports("todoMVC", {
    view: {
        bindAddItem,
        bindEditItemCancel,
        bindEditItemSave,
        bindRemoveCompleted,
        bindRemoveItem,
        bindToggleAll,
        bindToggleItem,
        clearNewTodo,
        editItem,
        editItemDone,
        initView,
        removeItem,
        setClearCompletedButtonVisibility,
        setCompleteAllCheckbox,
        setItemComplete,
        setItemsLeft,
        setMainVisibility,
        showItems,
        updateFilterButtons,
    },
    store: {
        getLocalStorage,
        setLocalStorage
    }
});
const config = getConfig();
exports = await getAssemblyExports(config.mainAssemblyName);

await dotnet.run();
onHashchange();