import { qs, $on, $delegate } from "./helpers.js";

const itemId = element => parseInt(element.parentNode.dataset.id || element.parentNode.parentNode.dataset.id, 10);
const enterKey = 13;
const escapeKey = 27;

let $todoList;
let $todoItemCounter;
let $clearCompleted;
let $main;
let $toggleAll;
let $newTodo;

export function initView() {
	$todoList = qs(".todo-list");
	$todoItemCounter = qs(".todo-count");
	$clearCompleted = qs(".clear-completed");
	$main = qs(".main");
	$toggleAll = qs(".toggle-all");
	$newTodo = qs(".new-todo");
	$delegate($todoList, "li label", "dblclick", ({ target }) => {
		editItem(target);
	});
}

export function editItem(target) {
	const listItem = target.parentElement.parentElement;

	listItem.classList.add("editing");

	const input = window.document.createElement("input");
	input.className = "edit";

	input.value = target.innerText;
	listItem.appendChild(input);
	input.focus();
}

export function showItems(itemsHtml) {
	$todoList.innerHTML = itemsHtml;
}

export function removeItem(id) {
	const elem = qs(`[data-id="${id}"]`);

	if (elem) {
		$todoList.removeChild(elem);
	}
}

export function setItemsLeft(itemsLeftHtml) {
	$todoItemCounter.innerHTML = itemsLeftHtml;
}

export function setClearCompletedButtonVisibility(visible) {
	$clearCompleted.style.display = !!visible ? "block" : "none";
}

export function setMainVisibility(visible) {
	$main.style.display = !!visible ? "block" : "none";
}

export function setCompleteAllCheckbox(checked) {
	$toggleAll.checked = !!checked;
}

export function updateFilterButtons(route) {
	qs(".filters .selected").className = "";
	qs(`.filters [href="#/${route}"]`).className = "selected";
}

export function clearNewTodo() {
	$newTodo.value = "";
}

export function setItemComplete(id, completed) {
	const listItem = qs(`[data-id="${id}"]`);

	if (!listItem) {
		return;
	}

	listItem.className = completed ? "completed" : "";

	// In case it was toggled from an event and not by clicking the checkbox
	qs("input", listItem).checked = completed;
}

export function editItemDone(id, title) {
	const listItem = qs(`[data-id="${id}"]`);

	const input = qs("input.edit", listItem);
	listItem.removeChild(input);

	listItem.classList.remove("editing");

	qs("label", listItem).textContent = title;
}

export function bindAddItem(handler) {
	$on($newTodo, "change", ({ target }) => {
		const title = target.value.trim();
		if (title) {
			handler(title);
		}
	});
}

export function bindRemoveCompleted(handler) {
	$on($clearCompleted, "click", handler);
}

export function bindToggleAll(handler) {
	$on($toggleAll, "click", ({ target }) => {
		handler(target.checked);
	});
}

export function bindRemoveItem(handler) {
	$delegate($todoList, ".destroy", "click", ({ target }) => {
		handler(itemId(target));
	});
}

export function bindToggleItem(handler) {
	$delegate($todoList, ".toggle", "click", ({ target }) => {
		handler(itemId(target), target.checked);
	});
}

export function bindEditItemSave(handler) {
	$delegate($todoList, "li .edit", "blur", ({ target }) => {
		if (!target.dataset.iscanceled) {
			handler(itemId(target), target.value.trim());
		}
	}, true);

	// Remove the cursor from the input when you hit enter just like if it were a real form
	$delegate($todoList, "li .edit", "keypress", ({ target, keyCode }) => {
		if (keyCode === enterKey) {
			target.blur();
		}
	});
}

export function bindEditItemCancel(handler) {
	$delegate($todoList, "li .edit", "keyup", ({ target, keyCode }) => {
		if (keyCode === escapeKey) {
			target.dataset.iscanceled = true;
			target.blur();

			handler(itemId(target));
		}
	});
}
