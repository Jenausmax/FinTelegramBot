using System.Collections.Generic;
using FinBot.Domain.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FinBot.App.Services
{
    public class CommandService : ICommandBot
    {
        private Update _update;
        private readonly IKeyboardBotCreate _keyboardBotCreate;
        private readonly IUpdateService _updateService;

        public CommandService(IKeyboardBotCreate keyboardBotCreate, IUpdateService updateService)
        {
            _keyboardBotCreate = keyboardBotCreate;
            _updateService = updateService;
        }



        public void SetUpdateBot(Update update)
        {
            _update = update;
        }

        public void SetCommandBot(Telegram.Bot.Types.Enums.UpdateType type)
        {
            switch (type)
            {
                case UpdateType.Message:
                    MessageCommand(_update);
                    break;

                case UpdateType.CallbackQuery:
                    CallbackMessageCommand(_update);
                    break;

                default:
                    break;
            }
        }



        private async void MessageCommand(Update update)
        {
            var message = update.Message.Text;
            switch (message)
            {
                case "/start":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        "Привет я Бот и твой помошник!",
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: Start()));
                    break;
            }
        }

        private async void CallbackMessageCommand(Update update)
        {
            var callbackData = update.CallbackQuery.Data;
            switch (callbackData)
            {
                case "Помощь":
                    var help = "Мне можно отправлять свои траты.";
                    await _updateService.EchoTextMessageAsync(
                        update,
                        help,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            keyCollection: default,
                            key: Back()));
                    break;
            }
        }

        private List<string> Start() //Командная клавиатура
        {
            var start = new List<string>()
            {
                "Настройка",
                "Помощь"
            };
            return start;
        }

        private string Back()
        {
            return " <---Назад";
        }

    }
}
