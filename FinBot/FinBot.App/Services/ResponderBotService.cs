using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinBot.Domain.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinBot.App.Services
{
    public class ResponderBotService : IResponderBot
    {
        private readonly IUpdateService _updateService;

        public ResponderBotService(IUpdateService service)
        {
            _updateService = service;
        }

        public async void ResponderMessageAsync(Update update, string message, InlineKeyboardMarkup keyboard = default)
        {
            update.Message.Text = message;
            ResponderAsync(update);
        }

        public void ResponderCallBackAsync(Update update, string message, InlineKeyboardMarkup keyboard = default)
        {
            throw new NotImplementedException();
        }

        private async void ResponderAsync(Update update)
        {
            await _updateService.EchoTextMessageAsync(update, "asretg");
        }
    }
}
