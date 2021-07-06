using FinBot.App.Phrases;
using FinBot.Domain.Interfaces;
using FinBot.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FinBot.App.Services
{
    public class CommandService : ICommandBot
    {
        private IBaseRepositoryDb<Category> _categoryDb;
        private IBaseRepositoryDb<Consumption> _consumptionDb;
        private IBaseRepositoryDb<Income> _incomeDb;
        private IUserControl _userControl;
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
                    await MessageCommand(_update);
                    var resMessageUser = await _userControl.SetUser(_update.Message.From.Id);
                    if (resMessageUser == false)
                    {
                        var user = new Domain.Models.User()
                        {
                            ChatId = _update.Message.From.Id,
                            NickName = _update.Message.From.Username
                        };
                        await _userControl.Create(user);
                    }
                    break;

                case UpdateType.CallbackQuery:
                    await CallbackMessageCommand(_update);
                    var resCallbackUser = await _userControl.SetUser(_update.CallbackQuery.From.Id);
                    if (resCallbackUser == false)
                    {
                        var user = new Domain.Models.User()
                        {
                            ChatId = _update.CallbackQuery.From.Id,
                            NickName = _update.CallbackQuery.From.Username                                                       
                        };
                        await _userControl.Create(user);
                    }
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
                        BotPhrases.AddSettingMenu,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: AddSettingMenu()));
                    break;
                case "Remove category":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        "Категории для удаления: ",
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: RemoveCategoryList()));
                    _flagRemoveCategory = true;
                    break;
                #endregion

                #region Submenu Add Category

                //подменю Add Category
                case "Income Setting":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        InputCategory(),
                        keyboard: null);
                    _incomeSetting = true;
                    break;

                case "Consumption Setting":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        InputCategory(),
                        keyboard: null);
                    _consumptionSetting = true;
                    break;

                #endregion

                #region All Menu

                //основное меню
                case "Home":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        BotPhrases.HomeMenu,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: HomeMenu()));
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

                default:
                    ParseCallbackInputText(update.CallbackQuery.Data);
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

        private List<string> HomeMenu() //Add Setting клавиатура
        {
            var start = new List<string>()
            {
                "Setting",
                "Help"
            };
            return start;
        }

        private string Back() //button back
        {
            return " <---Back Home";
        }

        private string InputCategory() => "Введите название категории: ";

        /// <summary>
        /// Метод формирования списка категорий на удаление
        /// </summary>
        /// <returns></returns>
        private List<string> RemoveCategoryList()
        {
            //var categories = _db.GetCollectionCategories();
            //var removeListCategoriesName = new List<string>();
            //foreach (var category in categories)
            //{
            //    removeListCategoriesName.Add(category.Name);
            //}

            //return removeListCategoriesName;
            return null;
        }


        private async void ParseInputText(string text, CancellationToken cancel = default)
        {
            var entity = new Category() { Name = text, UserId = CurrentUser.User.Id};
            if (_incomeSetting)
            {
                entity.Role = false;
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
                entity.Role = true;
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
