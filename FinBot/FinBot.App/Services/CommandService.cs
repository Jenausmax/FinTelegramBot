using FinBot.App.Model;
using FinBot.App.Phrases;
using FinBot.Domain.Interfaces;
using FinBot.Domain.Models;
using System;
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
        private readonly IBaseRepositoryDb<Category> _categoryDb;
        private readonly IBaseRepositoryDb<Consumption> _consumptionDb;
        private readonly IBaseRepositoryDb<Income> _incomeDb;
        private readonly IBaseRepositoryDb<TelegramSticker> _stickerDb;
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
            IBaseRepositoryDb<TelegramSticker> sticker,
            IUserControl userControl)
        {
            _keyboardBotCreate = keyboardBotCreate;
            _updateService = updateService;
            _categoryDb = categoryDb;
            _consumptionDb = cons;
            _incomeDb = inc;
            _stickerDb = sticker;
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

                    if (_update.Message.Sticker != null) //обработка стикеров 
                    {
                        var stickerNow = _update.Message.Sticker;
                        var stickers = await _stickerDb.GetCollection();
                        var sticker = stickers.FirstOrDefault(e => e.FileidUnique == stickerNow.FileUniqueId);
                        if (sticker == null)
                        {
                            await _stickerDb.Create(new TelegramSticker()
                            {
                                Name = stickerNow.SetName,
                                FileidTelegram = stickerNow.FileId,
                                Emoji = stickerNow.Emoji,
                                IsAnimated = stickerNow.IsAnimated,
                                StickerRole = StickerRole.none,
                                FileidUnique = stickerNow.FileUniqueId

                            });
                        }
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
                                keyCollection: default,
                                collectionButtonRows: CategoryListCollection().Result));
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
                case "Menu":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        BotPhrases.HomeMenuHelp,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: BotPhrases.HomeMenu()));
                    break;
                case "Income":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        BotPhrases.IncomeCategory,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: default,
                            collectionButtonRows: CategoryListCollection(CategoryRole.Income).Result));
                    _flagIncome = true;
                    break;
                case "Consumption":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        BotPhrases.ConsumptionCategory,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: default,
                            collectionButtonRows: CategoryListCollection(CategoryRole.Consumption).Result));
                    _flagConsumption = true;
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
                    _flagIncome = false;
                    _flagConsumption = false;
                    _incomeSetting = false;
                    _consumptionSetting = false;
                    _flagRemoveCategory = false;
                    break;

                default:
                    var resMessageUser = await _userControl.SetUser(_update.CallbackQuery.From.Id);
                    ParseCallbackInputText(update.CallbackQuery.Data);
                    break;
            }
        }

        /// <summary>
        /// Метод формирования списка категорий на удаление
        /// </summary>
        /// <returns></returns>
        private async Task<List<List<string>>> CategoryListCollection(CategoryRole role = default)
        {
            var categories = await _categoryDb.GetCollection();
            var cat = categories.Where(e => e.UserId == CurrentUser.Id && e.IsDelete == false).ToList();

            if (role == CategoryRole.Income)
            {
                var catInc = cat.Where(e => e.Role == CategoryRole.Income).ToList();
                cat = catInc;
            }

            if (role == CategoryRole.Consumption)
            {
                var catCon = cat.Where(e => e.Role == CategoryRole.Consumption).ToList();
                cat = catCon;
            }

            var catListString = new List<string>();
            foreach (var category in cat)
            {
                catListString.Add(category.Name);
            }

            var res = catListString
                            .Select((x, y) => new { Index = y, Value = x })
                            .GroupBy(x => x.Index / 2)
                            .Select(x => x.Select(y => y.Value).ToList())
                            .ToList();
            res.Add(new List<string>(){ "<---Back Home" });
            return res;
        }


        /// <summary>
        /// Метод парсинга текста в message
        /// </summary>
        /// <param name="text"></param>
        /// <param name="cancel"></param>
        private async void ParseInputText(string text, CancellationToken cancel = default)
        {
            var entity = new Category() { Name = text, UserId = CurrentUser.Id, IsDelete = false};
            if (_incomeSetting)//создание категории Income
            {
                entity.Role = CategoryRole.Income;
                var categoryIncomes = await _categoryDb.GetCollection(cancel);
                var inc = categoryIncomes.FirstOrDefault(e => e.Name == text);
                if (inc == null)
                {
                    await _categoryDb.Create(entity, cancel);
                    await SendingShortCommand(BotPhrases.UpdateSuccessful, BotPhrases.Back());
                    _incomeSetting = false;
                }
                else
                {
                    await SendingShortCommand(BotPhrases.CategoryExist);
                }
            }
            if (_consumptionSetting) //создание категории Consumption
            {
                entity.Role = CategoryRole.Consumption;
               var categoryConsumptions = await _categoryDb.GetCollection(cancel);
               var consumption = categoryConsumptions.FirstOrDefault(e => e.Name == text);
               if (consumption == null)
               {
                   await _categoryDb.Create(entity, cancel);
                   await SendingShortCommand(BotPhrases.UpdateSuccessful, BotPhrases.Back());
                   _consumptionSetting = false;
               }
               else
               {
                   await SendingShortCommand(BotPhrases.CategoryExist);
               }
            }

            double res = 0;
            var boolParse = double.TryParse(text, out res);

            if (_flagIncome)
            {
                if (boolParse)
                {
                    var income = new Income()
                    {
                        IsDelete = false, 
                        Date = DateTime.Now, 
                        Money = res, 
                        UserId = CurrentUser.Id, 
                        CategoryId = CurrentCategoryIncome.Id
                    };

                    if (await _incomeDb.Create(income, cancel))
                    {
                        await SendingShortCommand(BotPhrases.UpdateSuccessful, BotPhrases.Back());
                    }
                    else
                    {
                        await SendingShortCommand(BotPhrases.SumAddingError);
                    }

                    _flagIncome = false;
                }
                else
                {
                    await SendingShortCommand(BotPhrases.SumAddingError);
                }
            }

            if (_flagConsumption)
            {
                if (boolParse)
                {
                    var consumption = new Consumption()
                    {
                        IsDelete = false,
                        Date = DateTime.Now,
                        Money = res,
                        UserId = CurrentUser.Id,
                        CategoryId = CurrentCategoryIncome.Id
                    };

                    if (await _consumptionDb.Create(consumption, cancel))
                    {
                        await SendingShortCommand(BotPhrases.UpdateSuccessful, BotPhrases.Back());
                    }
                    else
                    {
                        await SendingShortCommand(BotPhrases.SumAddingError);
                    }

                    _flagConsumption = false;
                }
                else
                {
                    await SendingShortCommand(BotPhrases.SumAddingError);
                }
            }
        }

        /// <summary>
        /// Метод парсинга Data в келбеке.
        /// </summary>
        /// <param name="response"></param>
        private async void ParseCallbackInputText(string response)
        {
            var categories = await _categoryDb.GetCollection();
            var category = categories.FirstOrDefault(e => e.Name == response);
            if (category is not null)
            {
                if (_flagRemoveCategory)
                {
                    category.IsDelete = true;
                    await _categoryDb.Update(category);
                    _flagRemoveCategory = false;
                    await SendingShortCommand(BotPhrases.UpdateSuccessful, BotPhrases.Back());
                }

                if (_flagIncome)
                {
                    CurrentCategoryIncome.Id = category.Id;
                    CurrentCategoryIncome.Name = category.Name;
                    await SendingShortCommand(BotPhrases.EnterTheAmount);
                }

                if (_flagConsumption)
                {
                    CurrentCategoryConsumption.Id = category.Id;
                    CurrentCategoryConsumption.Name = category.Name;
                    await SendingShortCommand(BotPhrases.EnterTheAmount);
                }
            }
            else
            {
                await SendingShortCommand(BotPhrases.CategoryExist);
            }
        }


        /// <summary>
        /// Метод отправки короткого сообщение для пользователя о выполнении.
        /// </summary>
        /// <param name="message"></param>
        private async Task SendingShortCommand(string message, string messageBack = default)
        {
            await _updateService.EchoTextMessageAsync(_update,
                message, _keyboardBotCreate.CreateInlineKeyboard(key: messageBack));
        }

    }
}
