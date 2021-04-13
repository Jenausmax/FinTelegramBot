using Telegram.Bot.Types;

namespace FinBot.Domain.Interfaces
{
    public interface IResponderBot
    {
        void ResponderAsync(string message);
        void SetUpdateBot(Update update);
    }
}
