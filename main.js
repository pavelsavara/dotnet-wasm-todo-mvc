import { dotnet } from './dotnet.js'
import { $on } from './helpers.js';

const { getAssemblyExports, getConfig } = await dotnet.create();

let exports = await getAssemblyExports(getConfig().mainAssemblyName);

const onHashchange = () => exports.TodoMVC.MainJs.OnHashchange(document.location.hash);
$on(window, 'hashchange', onHashchange);

await dotnet.run();
onHashchange();
