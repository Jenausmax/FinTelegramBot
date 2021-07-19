using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FinBot.Domain.Interfaces;
using FinBot.Maintenance.Abstractions;
using FinBot.WebApi.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace FinBot.Maintenance.Jobs
{
    public class UserTaskReminderJobs : BackgroundTaskAbstract<UserTaskReminderJobs>
    {
        public UserTaskReminderJobs(IServiceProvider services, TimeSpan period) : base(services, TimeSpan.FromMinutes(1))
        {
        }

        protected override async Task DoWorkAsync(CancellationToken stoppingToken, IServiceProvider scope)
        {
            var commandService = scope.GetRequiredService<ICommandBot>();
            var controller = scope.GetRequiredService<UpdateController>();
            var up = await controller.Request.BodyReader.ReadAsync();

        }
    }
}
