using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundTaskUsingBackgroundService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Serilog will override the default logger that is built in ASP.NET Core.
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                            .Enrich.FromLogContext()
                            .WriteTo.File(@"D:\BackgroundTasks-Asp.NetCore5\LogFile.txt") // Serilog will help us to log into a file.
                            .CreateLogger();

            try
            {
                Log.Information("Starting up the service");
                CreateHostBuilder(args).Build().Run();
                return;
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "There was a problem starting the service");
                return;
            }
            finally
            {
                Log.CloseAndFlush(); // It ensures that if there's any message in a buffer so they get written before we close out the application.
                // See loggers have to be efficient and in order to be efficient one of the ways they do that is batch things.
                // They don't necessariy always write the log exactly when you will call it.
                // Instead they may wait for a little bit and then write in a batch or in a group.
                // So if you're halfway through that batching process and you close the application either intentionally by shutting it down or unintentionally crashing it. Either way you don't lose those last messages b/c this Log.CloseAndFlush() ensures that those messages are written out.
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                })
                .UseSerilog();
    }
}
