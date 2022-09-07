const localStorage = window.localStorage;
const name = 'dotnet-wasm-todomvc'
export function setLocalStorage(todosJson) {
	localStorage.setItem(name, todosJson);
}

export function getLocalStorage() {
	return localStorage.getItem(name) || '[]';
};
