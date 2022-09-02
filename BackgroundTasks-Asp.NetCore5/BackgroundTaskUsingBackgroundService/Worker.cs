using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTaskUsingBackgroundService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient client;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            // We want to leave that HttpClient open as long as the service stays open but when the service shuts down, we should properly shut down the HttpClient as well.
            client.Dispose(); // Releases the unmanaged resources and disposes the managed resources.

            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) // this CancellationToken is someone stopping the service while asynchronous event was going on.
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var result = await client.GetAsync("https://www.iamtimcorey.com");

                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("The website was up. Status code {StatusCode}", result.StatusCode);
                }
                else
                {
                    _logger.LogInformation("The website was down. Status code {StatusCode}", result.StatusCode);
                }

                await Task.Delay(1000 * 5, stoppingToken); 
            }
        }
    }
}
