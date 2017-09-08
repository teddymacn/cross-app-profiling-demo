# cross-app-profiling-demo
Sample projects to demonstrate cross-application performance profiling with [coreprofiler](https://github.com/teddymacn/CoreProfiler)/[nanoprofiler](https://github.com/ef-labs/nanoprofiler).

#### This repo contains 3 projects:

- Core.UI - A dotnet core UI project consuming APIs
- Core.Api - A dotnet core Api project providing Web APIs
- Net45.Api - A .net 4.5 Api project providing some other Web APIs

The combination of these projects shows how coreprofiler/nanoprofiler simplifies cross-application performance profiling. coreprofiler/nanoprofiler not only provides a tree style UI visualizing the profiling result of each of your web request, but also enables drill down/drill up of related child/parent profiling results.

For instance, in profiling result UI of a Core.UI page request, you can drill down to profiling results of any of its child api calls, and vise versa.

#### To run the demo:

1. You need to have a windows OS with IIS7+, VS2017 and dotnet core SDK tool v2.0.0+ installed;
2. Git clone this repo;
3. Run run_api.cmd to start Core.Api self-hosting app;
4. Open Net45.Api project in VS2017 (with admin permission) and press F5 to run it and ensure the site is up running at http://127.0.0.1/Net45Api;
5. Run run_ui.cmd to start Core.UI self-hosting app;
6. In a browser, visit the home page of Core.UI: http://127.0.0.1:3001;
7. Follow the instructions on the home page for fun;
