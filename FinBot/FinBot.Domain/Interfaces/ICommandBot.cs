using Telegram.Bot.Types;

namespace FinBot.Domain.Interfaces
{
    public interface ICommandBot
    {
        void SetUpdateBot(Update update);
        void SetCommandBot(Telegram.Bot.Types.Enums.UpdateType types);
    }
}
