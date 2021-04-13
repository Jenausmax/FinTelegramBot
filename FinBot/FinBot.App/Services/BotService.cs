using FinBot.Domain.Interfaces;
using FinBot.Domain.Models;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace FinBot.App.Services
{
    public class BotService : IBotService
    {
        public TelegramBotClient Client { get; }

        private readonly BotConfiguration _configuration;

        public BotService(IOptions<BotConfiguration> config)
        {
            _configuration = config.Value;

            Client = new TelegramBotClient(_configuration.BotToken);
        }
    }
}
