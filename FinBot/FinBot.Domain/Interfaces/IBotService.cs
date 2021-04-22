using Telegram.Bot;

namespace FinBot.Domain.Interfaces
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }
    }
}
