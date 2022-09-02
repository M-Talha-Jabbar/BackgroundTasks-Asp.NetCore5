# Background Tasks/Services
There are numerous reasons for creating long-running services such as:
- Processing CPU-intensive data.
- Queuing work items in the background.
- Performing time-based operation on a schedule.

In the early days with .NET Framework, Windows developers could create Windows Services for these reasons. Now with .NET, you can use the <b>BackgroundService</b>, which is an implementation of <b>IHostedService</b>, or implement your own.

Also with .NET, you're no longer restricted to Windows. You can develop cross-platform background services. 

<b>Note</b>: <b>BackgroundService</b> is more for "long-running processes", services that are continually being running. For example, a service checks a database table after specific amount of time to see if new entries have entered in or maybe even get triggered by database table when new entries come in, or a service which monitors the queue and is triggered when something comes in it and then do something with it. So here the service is running continuously and is waiting for a particular action to occur in order to get triggered or become active OR the service is monitoring or looking up to the things after specific amount of time and start doing the work needed. While <b>Hangfire</b> is for "scheduling" which means service will run at specific time periods, will perform its job and will shutdown, so will not be running continuously.
