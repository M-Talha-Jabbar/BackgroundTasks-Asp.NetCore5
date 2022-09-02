# Background Tasks/Services
There are numerous reasons for creating long-running services such as:
- Processing CPU-intensive data.
- Queuing work items in the background.
- Performing time-based operation on a schedule.

In the early days with .NET Framework, Windows developers could create Windows Services for these reasons. Now with .NET, you can use the <b>BackgroundService</b>, which is an implementation of <b>IHostedService</b>, or implement your own.

Also with .NET, you're no longer restricted to Windows. You can develop cross-platform background services. 
