# Port of famous Todo-MVC to .NET on WASM

With Net7 RC1 or later do:
```
dotnet workload install wasm-tools
dotnet publish -c Release

dotnet tool install --global dotnet-serve
dotnet serve --mime .wasm=application/wasm --mime .js=text/javascript --mime .json=application/json --directory bin\Release\net7.0\browser-wasm\AppBundle\
```

Live demo here https://pavelsavara.github.io/dotnet-wasm-todo-mvc/

Original code at: https://todomvc.com/


## How to debug with chrome dev tools

- Start the app with debugging enabled:
    ```
    dotnet run --debug
    ```
- In another console start the chrome with remote debugging enabled:
    ```
    chrome --remote-debugging-port=9222 http://127.0.0.1:9000/index.html
    ```
- Open the `chrome:\inspect` tab
- Click on configure button
- Copy the address printed on first console in this case `127.0.0.1:53485`
    ```
    Debug proxy for chrome now listening on http://127.0.0.1:53485/. 
    ```
- Paste the value like `127.0.0.1:53485` to the chome's `Target discovery settings`
- From the `Remote target ` Choose line `.NET 7 • TodoMVC http://127.0.0.1:9000/index.html` and click `inspect`
- In another browser tab you should see chrome dev tools
- Swith to `Sources` tab
- In the files tree expand `file://` and find `Store.cs`
- Add breakpoint into `void Insert` method
- In the running app browser tab add new todo item, the breakpoint should be hit

*All of above would be much simpler as just "F5" in Visual Studio when we release final .NET 7*

## How to test with Playwright
- In first console
    ```
    dotnet run
    ```
- In second console
    ```
    dotnet build test\PlaywrightTests.csproj
    pwsh test/bin/Debug/net7.0/playwright.ps1 install
    dotnet test test\PlaywrightTests.csproj
    ```
