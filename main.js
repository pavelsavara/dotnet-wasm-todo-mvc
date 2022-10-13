import { dotnet } from "./dotnet.js"
import { $on } from "./helpers.js";

let exports;
const onHashchange = () => exports.TodoMVC.MainJS.OnHashchange(document.location.hash);
$on(window, "hashchange", onHashchange);

const { getAssemblyExports, getConfig } = await dotnet.create();

exports = await getAssemblyExports(getConfig().mainAssemblyName);

await dotnet.run();
onHashchange();
