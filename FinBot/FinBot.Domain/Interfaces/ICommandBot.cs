using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FinBot.Domain.Interfaces
{
    public interface ICommandBot
    {
        Task SetUpdateBot(Update update);
        Task SetCommandBot(Telegram.Bot.Types.Enums.UpdateType types);
    }
}
