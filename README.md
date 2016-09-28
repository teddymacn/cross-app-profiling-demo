# cross-app-profiling-demo
Sample projects to demonstrate cross-application performance profiling with [coreprofiler](https://github.com/teddymacn/CoreProfiler)/[nanoprofiler](https://github.com/ef-labs/nanoprofiler).

#### This repo contains 4 projects:

- Core.UI - A dotnet core UI project consuming APIs
- Core.Console - A dotnet core console application consuming APIs
- Core.Api - A dotnet core Api project providing Web APIs
- Net45.Api - A .net 4.5 Api project providing some other Web APIs

The combination of these projects shows how coreprofiler/nanoprofiler simplifies cross-application performance profiling. coreprofiler/nanoprofiler not only provides a tree style UI visualizing the profiling result of each of your web request, but also enables drill down/drill up of related child/parent profiling results.

For instance, in profiling result UI of a Core.UI page request, you can drill down to profiling results of any of its child api calls, and vise versa.

#### To run the demo:

1. You need to have a windows OS with IIS7+, VS2015 and dotnet core SDK tool v1.0.0+ installed;
2. Git clone this repo;
3. Run .\build.ps1 to build the projects;
4. Open Net45.Api project in VS2015 and press F5 to run it and ensure the site is up running at http://127.0.0.1/Net45Api;
5. Run .\run_netcore.ps1 to start Core.UI, Core.Console and Core.Api self-hosting apps;
6. In a browser, visit the home page of Core.UI: http://127.0.0.1:3001;
7. Follow the instructions on the home page for fun;

*code to be uploaded soon...*