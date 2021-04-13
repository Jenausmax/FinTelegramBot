using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FinBot.Domain.Interfaces
{
    public interface IUpdateService
    {
        Task EchoAsync(Update update);
    }
}
