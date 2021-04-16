using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinBot.Domain.Interfaces;
using Telegram.Bot.Types;

namespace FinBot.App.Services
{
    class CommandService : ICommandBot
    {
        private Update _update;
        public void SetUpdateBot(Update update)
        {
            update = _update;
        }

        public string Command { get; set; }
        public void SetCommandBot(string command)
        {
            throw new NotImplementedException();
        }
    }
}
