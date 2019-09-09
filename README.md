# Multi Processor Extensions

Managed multi-processor extensions for .NET, inspired by the [C++/CLI multiproc library](http://blogs.microsoft.co.il/sasha/2009/07/25/net-support-for-more-than-64-processors/) by sasha.

This library's capabilities include:

* Query information about processor cores, processor packages, caches, NUMA nodes, and processor groups.
* Query the default processor group for processes.
* Query the assigned processor group for threads.
* Assign processes and threads to specific processor groups.
* Create new processor groups.

## Why is this library useful?

Historically, Windows processes have maintained an associated affinity mask which specifies which processors the process' threads can be assigned to. The affinity mask's size is limited to the pointer size (64 on modern systems), which limits this mechanism to 64 processors at most. In order to support more than 64 processors, Windows introduces a concept called processor groups. A process is assigned a preferred processor group, and the affinity mask specifies which processors in that group it should have its threads allocated to (by default, any). The preferred processor group from the process is then inherited by its threads.

Multiple processor groups are also used in systems with less than 64 cores, but with more than one physical processor or when non-uniform memory architecture (NUMA) is in use. This is because it makes sense to try to group loads onto the same physical procesor or NUMA node in order to avoid latency and performance reductions casued by transfers over the inter-processor bus.

High-performance applications running on systems with the above described properties may wish to manually specify processor groups for its threads in order to better distribute multi-threaded compute loads.

## Compatibility

This library targets most framework versions:

* .NET Standard 2.1, 2.0, 1.6, 1.5, 1.4, 1.3, 1.2, 1.1
* .NET Core 3.0, 2.2, 2.1
* .NET Framework 4.8, 4.7.2, 4.7.1, 4.7, 4.6, 4.5.2, 4.5.1, 4.5, 4.0

It should be usable from both .NET Framework and .NET Core (Windows only).

The functionality and API calls that this library relies upon were added in Windows 7 and Windows Server 2008 R2. Operating system versions prior to this are unsupported.

Tests use xUnit and are built for .NET Core 3.0, 2.2, and 2.1.

## Contributions

Pull requests accepted and encouraged. Please try to follow existing code style and include test coverage for any new functionality. Thank you!

## License

This code is released under the [MIT license](LICENSE.md).

If you do use this library, please let me know! You can reach me [on Twitter](https://twitter.com/gsuberland) or via email at {github handle} @ gmail.
