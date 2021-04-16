using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinBot.Domain.Interfaces
{
    public interface IResponderBot
    {
        void ResponderMessageAsync(Update update, string message, InlineKeyboardMarkup keyboard = default);
        void ResponderCallBackAsync(Update update, string message, InlineKeyboardMarkup keyboard = default);

    }
}
