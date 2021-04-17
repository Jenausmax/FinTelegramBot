using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinBot.Domain.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FinBot.App.Services
{
    class CommandService : ICommandBot
    {
        private Update _update;
        public void SetUpdateBot(Update update)
        {
            _update = update;
        }

        public async void SetCommandBot(Telegram.Bot.Types.Enums.UpdateType type)
        {
            switch (type)
            {
                case UpdateType.Message:
                    break;

                case UpdateType.CallbackQuery:
                    break;

                default:
                    break;
            }
        }
    }
}
