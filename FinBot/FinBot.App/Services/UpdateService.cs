using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinBot.Domain.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinBot.App.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotService _botService;

        public UpdateService(IBotService botService)
        {
            _botService = botService;
        }

        public async Task EchoAsync(Update update)
        {
            if (update.Type != UpdateType.Message)
            {
                return;
            }

            if (update.Message != null)
            {
                var message = update.Message;
                await _botService.Client.SendTextMessageAsync(message.Chat.Id,
                    message.Text,
                    parseMode: default,
                    disableWebPagePreview: false,
                    disableNotification: false,
                    replyToMessageId: 0,
                    new ReplyKeyboardMarkup(new List<KeyboardButton>()
                    {
                        new KeyboardButton("Привет!"),
                        new KeyboardButton("Пока!")
                    }));
            }
        }
    }
}
