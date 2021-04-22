using System.Collections.Generic;
using FinBot.App.Phrases;
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
                    //TODO Доделать ответ на неправильный запрос
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
                        BotPhrases.Start,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: AllCommandMenu()));
                    break;

                //TODO Обдумать а нужно ли это командное меню
                case "/income":
                    break;


                case "/consumption":
                    break;


                case "/balance":
                    break;


                default:
                    break;
            }
        }

        private async void CallbackMessageCommand(Update update)
        {
            var callbackData = update.CallbackQuery.Data;
            switch (callbackData)
            {
                #region Submenu Home
                //подменю старт
                case "Help":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        BotPhrases.Help,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            keyCollection: default,
                            key: Back()));
                    break;

                case "Setting":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        BotPhrases.Start,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: SettingMenu()));
                    break;

                #endregion

                #region Submenu Setting

                case "Add category":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        BotPhrases.Start,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: AddSettingMenu()));
                    break;
                case "Remove category":
                    break;
                #endregion

                #region Submenu Add Category

                //подменю Add Category
                case "Income Setting":
                    break;

                case "Consumption Setting":
                    break;

                #endregion

                #region All Menu

                //основное меню
                case "Home":
                    break;
                case "Income":
                    break;
                case "Consumption":
                    break;
                case "Balance":
                    break;
                #endregion

                case "<---Back Home":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        BotPhrases.Start,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: AllCommandMenu()));
                    break;
            }
        }


        /// <summary>
        /// Полное командное меню для чата.
        /// </summary>
        /// <returns></returns>
        private List<string> AllCommandMenu() //Командная клавиатура
        {
            var start = new List<string>()
            {
                "Home",
                "Income",
                "Consumption",
                "Balance"
            };
            return start;
        }


        private List<string> SettingMenu() //Setting клавиатура
        {
            var start = new List<string>()
            {
                "Add category",
                "Remove category"
            };
            return start;
        }

        private List<string> AddSettingMenu() //Add Setting клавиатура
        {
            var start = new List<string>()
            {
                "Income Setting",
                "Consumption Setting"
            };
            return start;
        }

        private string Back() //button back
        {
            return " <---Back Home";
        }

    }
}
