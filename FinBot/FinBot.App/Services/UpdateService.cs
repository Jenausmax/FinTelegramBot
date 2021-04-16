using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
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

        public async Task EchoTextMessageAsync(Update update, string message, InlineKeyboardMarkup keyboard = default)
        {
            if (update.Type == UpdateType.Message) //обработка текстовых сообщений
            {
                if (update.Message != null)
                {
                    var newMessage = update.Message;
                    await _botService.Client.SendTextMessageAsync(newMessage.Chat.Id,
                        newMessage.Text,
                        parseMode: default,
                        disableWebPagePreview: false,
                        disableNotification: false,
                        replyToMessageId: 0,
                        keyboard);
                }
            }

            if (update.Type == UpdateType.CallbackQuery) //обработка калбеков
            {
                if (update.CallbackQuery.Message != null)
                {
                    var newMessage = update.CallbackQuery.Message;
                    await _botService.Client.SendTextMessageAsync(newMessage.Chat.Id,
                        newMessage.Text,
                        parseMode: default,
                        disableWebPagePreview: false,
                        disableNotification: false,
                        replyToMessageId: 0,
                        keyboard);
                }
            }

            if (update.Type == UpdateType.ChannelPost)
            {
                return;
            }


        }
    }
}
