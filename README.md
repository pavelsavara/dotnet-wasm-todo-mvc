# Port of good old Todo-MCV

With Net7 RC1 or later do:
```
dotnet workload install wasm-tools
dotnet publish -c Release
dotnet serve --mime .wasm=application/wasm --mime .js=text/javascript --mime .json=application/json --directory bin\Release\net7.0\browser-wasm\AppBundle\
```

Live demo here https://pavelsavara.github.io/dotnet-wasm-todo-mvc/
Original code at: https://todomvc.com/