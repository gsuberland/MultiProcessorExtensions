# Multi Processor Extensions

Managed multi-processor extensions for .NET, inspired by the [C++/CLI multiproc library](http://blogs.microsoft.co.il/sasha/2009/07/25/net-support-for-more-than-64-processors/) by sasha.

This library's capabilities include:

* Query information about processor cores, processor packages, caches, NUMA nodes, and processor groups.
* Query the default processor group for processes.
* Query the assigned processor group for threads.
* Assign processes and threads to specific processor groups.
* Create new processor groups.

## Compatibility

This library targets .NET Standard 2.0, which means it should be usable from both .NET Framework and .NET Core (Windows only).

The functionality and API calls that this library relies upon were added in Windows 7 and Windows Server 2008 R2. Operating system versions prior to this are unsupported.

## License

This code is released under the MIT license.
