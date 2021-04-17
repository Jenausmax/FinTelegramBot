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
        void SetCommandBot(Telegram.Bot.Types.Enums.UpdateType types);
    }
}
