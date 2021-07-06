using System.Threading.Tasks;
using FinBot.Domain.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinBot.App.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotService _botService;

        public UpdateService(IBotService botService)
        {
            _botService = botService;
        }

        /// <summary>
        /// Метод отправки сообщения в чат от бота.
        /// </summary>
        /// <param name="update">Пришедший апдейт</param>
        /// <param name="message">Новое сообщение.</param>
        /// <param name="keyboard">Клавиатура для взаимодействия.</param>
        /// <returns></returns>
        public async Task EchoTextMessageAsync(Update update, string message, InlineKeyboardMarkup keyboard = default)
        {
            if (update.Type == UpdateType.Message) //обработка текстовых сообщений
            {
                if (update.Message != null)
                {
                    var newMessage = update.Message;
                    newMessage.Text = message;
                    await _botService.Client.SendTextMessageAsync(newMessage.Chat.Id,
                        newMessage.Text,
                        parseMode: default,
                        entities: null,
                        disableWebPagePreview: false,
                        disableNotification: false,
                        replyToMessageId: 0,
                        allowSendingWithoutReply: false,
                        keyboard);
                }
            }

            if (update.Type == UpdateType.CallbackQuery) //обработка калбеков
            {
                if (update.CallbackQuery.Message != null)
                {
                    var newMessageCallbackQueryMessage = update.CallbackQuery.Message;
                    newMessageCallbackQueryMessage.Text = message;
                    await _botService.Client.SendTextMessageAsync(newMessageCallbackQueryMessage.Chat.Id,
                        newMessageCallbackQueryMessage.Text,
                        parseMode: default,
                        entities: null,
                        disableWebPagePreview: false,
                        disableNotification: false,
                        replyToMessageId: 0,
                        allowSendingWithoutReply: false,
                        keyboard);
                }
            }

            //TODO: описать обработку ботом сообщений из групповых чатов. 
            if (update.Type == UpdateType.ChannelPost)
            {
                return;
            }


        }
    }
}
