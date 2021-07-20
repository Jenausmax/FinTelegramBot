using FinBot.App.Phrases;
using FinBot.Domain.Interfaces;
using FinBot.Domain.Models;
using FinBot.Maintenance.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.InputFiles;

namespace FinBot.Maintenance.Jobs
{
    public class UserTaskReminderJobs : BackgroundTaskAbstract<UserTaskReminderJobs>
    {
        public UserTaskReminderJobs(IServiceProvider services) : base(services, TimeSpan.FromHours(3))
        {
        }

        protected override async Task DoWorkAsync(CancellationToken stoppingToken, IServiceProvider scope)
        {
            var botService = scope.GetRequiredService<IBotService>();
            var repo = scope.GetRequiredService<IBaseRepositoryDb<User>>(); ;
            var users = repo.GetCollection(stoppingToken).Result.ToList();

            foreach (var user in users)
            {
                await botService.Client.SendTextMessageAsync(user.ChatId, BotPhrases.Reminder);
                await botService.Client.SendStickerAsync(user.ChatId, new InputOnlineFile("CAACAgIAAxkBAAIFgmDnUEZy2khDmpl3jPfMMxMsxOV9AAINAAPANk8TpPnh9NR4jVMgBA"));
            }
        }
    }
}
