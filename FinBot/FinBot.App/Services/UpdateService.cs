using System;
using System.Collections.Generic;
using System.Data;
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
            var key = new InlineKeyboardButton();
            var key2 = new InlineKeyboardButton().Text = "Пока!";
            key.Text = "sdf";
            key.CallbackData = "Lucky";
            if (update.Message != null)
            {
                var message = update.Message;
                await _botService.Client.SendTextMessageAsync(message.Chat.Id,
                    message.Text,
                    parseMode: default,
                    disableWebPagePreview: false,
                    disableNotification: false,
                    replyToMessageId: 0, 
                    new InlineKeyboardMarkup(new List<InlineKeyboardButton>()
                        {
                            key,
                            key2
                        }
                        ));
                //new ReplyKeyboardMarkup(new List<KeyboardButton>()
                //{
                //    new KeyboardButton("Привет!"),
                //    new KeyboardButton("Пока!")
                //}));
            }
        }
    }
}
