using FinBot.App.Phrases;
using FinBot.Domain.Interfaces;
using FinBot.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinBot.App.Model;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FinBot.App.Services
{
    public class CommandService : ICommandBot
    {
        private readonly IBaseRepositoryDb<Category> _categoryDb;
        private readonly IBaseRepositoryDb<Consumption> _consumptionDb;
        private readonly IBaseRepositoryDb<Income> _incomeDb;
        private readonly IUserControl _userControl;
        private Update _update;
        private readonly IKeyboardBotCreate _keyboardBotCreate;
        private readonly IUpdateService _updateService;

        private static bool _flagIncome = false;
        private static bool _flagConsumption = false;
        private static bool _incomeSetting = false;
        private static bool _consumptionSetting = false;
        private static bool _flagRemoveCategory = false;

        public CommandService(IKeyboardBotCreate keyboardBotCreate, 
            IUpdateService updateService, 
            IBaseRepositoryDb<Category> categoryDb, 
            IBaseRepositoryDb<Consumption> cons, 
            IBaseRepositoryDb<Income> inc, 
            IUserControl userControl)
        {
            _keyboardBotCreate = keyboardBotCreate;
            _updateService = updateService;
            _categoryDb = categoryDb;
            _consumptionDb = cons;
            _incomeDb = inc;
            _userControl = userControl;
        }



        public async Task SetUpdateBot(Update update)
        {
            _update = update;
        }

        public async Task SetCommandBot(Telegram.Bot.Types.Enums.UpdateType type)
        {
            switch (type)
            {
                case UpdateType.Message:
                    var resMessageUser = await _userControl.SetUser(_update.Message.From.Id);
                    if (resMessageUser == false)
                    {
                        var user = new Domain.Models.User()
                        {
                            ChatId = _update.Message.From.Id,
                            NickName = _update.Message.From.Username,
                            IsDelete = false,
                            FirstName = _update.Message.From.FirstName,
                            LastName = _update.Message.From.LastName
                        };
                        await _userControl.Create(user);
                        await _userControl.SetUser(user.ChatId);
                    }
                    await MessageCommand(_update);
                    break;

                case UpdateType.CallbackQuery:
                    var resCallbackUser = await _userControl.SetUser(_update.CallbackQuery.From.Id);
                    if (resCallbackUser == false)
                    {
                        var user = new Domain.Models.User()
                        {
                            ChatId = _update.CallbackQuery.From.Id,
                            NickName = _update.CallbackQuery.From.Username,
                            IsDelete = false,
                            FirstName = _update.Message.From.FirstName,
                            LastName = _update.Message.From.LastName
                        };
                        await _userControl.Create(user);
                        await _userControl.SetUser(user.ChatId);
                    }
                    await CallbackMessageCommand(_update);
                    break;

                default:
                    //TODO Доделать ответ на неправильное сообщение от пользователя
                    break;
            }
        }



        private async Task MessageCommand(Update update)
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
                            keyCollection: BotPhrases.AllCommandMenu()));
                    break;

                //TODO Обдумать а нужно ли это командное меню
                case "/income":
                    break;


                case "/consumption":
                    break;


                case "/balance":
                    break;


                default:
                    var resMessageUser = await _userControl.SetUser(_update.Message.From.Id);
                    ParseInputText(message);
                    break;
            }
        }



        private async Task CallbackMessageCommand(Update update)
        {
            var callbackData = update.CallbackQuery.Data;
            switch (callbackData)
            {
                #region Submenu Home
                //подменю Home
                case "Help":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        BotPhrases.Help,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            keyCollection: default,
                            key: BotPhrases.Back()));
                    break;

                case "Setting":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        BotPhrases.Start,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: BotPhrases.SettingMenu()));
                    break;

                #endregion

                #region Submenu Setting

                case "Add category":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        BotPhrases.AddSettingMenuHelp,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: BotPhrases.AddSettingMenu()));
                    break;
                case "Remove category":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        "Категории для удаления: ",
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: RemoveCategoryList().Result));
                    _flagRemoveCategory = true;
                    break;
                #endregion

                #region Submenu Add Category

                //подменю Add Category
                case "Income category add":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        BotPhrases.EnterCategoryName,
                        keyboard: null);
                    _incomeSetting = true;
                    break;

                case "Consumption category add":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        BotPhrases.EnterCategoryName,
                        keyboard: null);
                    _consumptionSetting = true;
                    break;

                #endregion

                #region All Menu

                //основное меню
                case "Home":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        BotPhrases.HomeMenuHelp,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: BotPhrases.HomeMenu()));
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
                            keyCollection: BotPhrases.AllCommandMenu()));
                    break;

                default:
                    var resMessageUser = await _userControl.SetUser(_update.Message.From.Id);
                    ParseCallbackInputText(update.CallbackQuery.Data);
                    break;
            }
        }

        /// <summary>
        /// Метод формирования списка категорий на удаление
        /// </summary>
        /// <returns></returns>
        private async Task<List<string>> RemoveCategoryList()
        {
            var categories = await _categoryDb.GetCollection();
            var cat = categories.Where(e => e.UserId == CurrentUser.Id && e.IsDelete == false).ToList();
            var removeListCategoriesName = new List<string>();
            foreach (var category in cat)
            {
                removeListCategoriesName.Add(category.Name);
            }

            return removeListCategoriesName;
        }


        private async void ParseInputText(string text, CancellationToken cancel = default)
        {
            var entity = new Category() { Name = text, UserId = CurrentUser.Id, IsDelete = false};
            if (_incomeSetting)
            {
                entity.Role = CategoryRole.Income;
                var categoryIncomes = await _categoryDb.GetCollection(cancel);
                var inc = categoryIncomes.FirstOrDefault(e => e.Name == text);
                if (inc == null)
                {
                    await _categoryDb.Create(entity, cancel);
                    await SendingShortCommand(BotPhrases.UpdateSuccessful);
                    _incomeSetting = false;
                }
                else
                {
                    await SendingShortCommand(BotPhrases.CategoryExist);
                }
            }
            if (_consumptionSetting)
            {
                entity.Role = CategoryRole.Consumption;
               var categoryConsumptions = await _categoryDb.GetCollection(cancel);
               var consumption = categoryConsumptions.FirstOrDefault(e => e.Name == text);
               if (consumption == null)
               {
                   await _categoryDb.Create(entity, cancel);
                   await SendingShortCommand(BotPhrases.UpdateSuccessful);
                   _consumptionSetting = false;
               }
               else
               {
                   await SendingShortCommand(BotPhrases.CategoryExist);
               }
            }
        }

        private void ParseCallbackInputText(string response)
        {
            if (_flagRemoveCategory)
            {
                //var category = _db.GetCategory(response);
                //if (category != null)
                //{
                //    _db.DeleteCategory(category.Id);
                //    SendingShortCommand("Выполнено!");
                //}

                //_flagRemoveCategory = false;
            }
        }

        private void ParseInputMoney(int idCategory, string money)
        {
            if (_flagIncome)
            {
                _flagIncome = false;
            }

            if (_flagConsumption)
            {
                _flagConsumption = false;
            }
        }


        /// <summary>
        /// Метод отправки короткого сообщение для пользователя о выполнении.
        /// </summary>
        /// <param name="message"></param>
        private async Task SendingShortCommand(string message)
        {
            await _updateService.EchoTextMessageAsync(_update,
                message);
        }

    }
}
