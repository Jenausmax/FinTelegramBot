using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinBot.Maintenance.Abstractions
{
    public abstract class BackgroundTaskAbstract<T> : BackgroundService 
        where T : BackgroundTaskAbstract<T>
    {
        private readonly TimeSpan _period;
        private readonly IServiceProvider _services;

        public BackgroundTaskAbstract(IServiceProvider services, TimeSpan period)
        {
            _period = period;
            _services = services;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
			while (!stoppingToken.IsCancellationRequested)
            {
                var scope = _services.CreateScope();
                try
                {
                    await DoWorkAsync(stoppingToken, scope.ServiceProvider);
                }
                catch (Exception e)
                {
                   // Log(LogLevel.Error, e, $"Task failed: {typeof(T).Name}");
                }
                finally
                {
                    scope.Dispose();
                }
                await Task.Delay(_period, stoppingToken);
            }
		}

        protected abstract Task DoWorkAsync(CancellationToken stoppingToken, IServiceProvider scope);
    }
}
