using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FinBot.Domain.Interfaces
{
    public interface ICommandBot
    {
        void SetUpdateBot(Update update);
        string Command { get; set; }
        void SetCommandBot(string command);
    }
}
