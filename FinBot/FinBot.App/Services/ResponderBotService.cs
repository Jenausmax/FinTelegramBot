using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinBot.Domain.Interfaces;
using Telegram.Bot.Types;

namespace FinBot.App.Services
{
    public class ResponderBotService : IResponderBot
    {
        private readonly IUpdateService _updateService;
        private Update _update;

        public ResponderBotService(IUpdateService service)
        {
            _updateService = service;
        }

        public async void ResponderAsync(string newMessage)
        {
            _update.Message.Text = newMessage;
            await _updateService.EchoAsync(_update);
        }

        public void SetUpdateBot(Update update)
        {
            _update = update;
        }
    }
}
