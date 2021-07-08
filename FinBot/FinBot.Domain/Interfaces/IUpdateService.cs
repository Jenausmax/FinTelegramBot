using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinBot.Domain.Interfaces
{
    public interface IUpdateService
    {
        /// <summary>
        /// Метод отправки сообщения в чат от бота.
        /// </summary>
        /// <param name="update">Пришедший апдейт</param>
        /// <param name="message">Новое сообщение.</param>
        /// <param name="keyboard">Клавиатура для взаимодействия.</param>
        /// <returns></returns>
        Task EchoTextMessageAsync(Update update, string message, InlineKeyboardMarkup keyboard = default);

        /// <summary>
        /// Отправка ботом стикеров.
        /// </summary>
        /// <param name="update"></param>
        /// <param name="messageStickerFileId">Строка fileId указывающая на телеграм стикер</param>
        /// <returns></returns>
        Task EchoSendStickerAsync(Update update, string messageStickerFileId);
    }
}
