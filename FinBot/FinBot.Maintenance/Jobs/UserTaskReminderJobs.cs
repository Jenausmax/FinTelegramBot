using FinBot.App.Model;
using FinBot.App.Phrases;
using FinBot.Domain.Interfaces;
using FinBot.Maintenance.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinBot.Maintenance.Jobs
{
    public class UserTaskReminderJobs : BackgroundTaskAbstract<UserTaskReminderJobs>
    {
        public UserTaskReminderJobs(IServiceProvider services) : base(services, TimeSpan.FromHours(3))
        {
        }

        protected override async Task DoWorkAsync(CancellationToken stoppingToken, IServiceProvider scope)
        {
            var commandService = scope.GetRequiredService<IUpdateService>();
            await commandService.EchoTextMessageAsync(CurrentUpdate.Update, BotPhrases.Reminder);
        }
    }
}
