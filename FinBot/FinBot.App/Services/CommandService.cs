using FinBot.App.Phrases;
using FinBot.Domain.Interfaces;
using FinBot.Domain.Models;
using System;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FinBot.App.Services
{
    public class CommandService : ICommandBot
    {
        private IBaseRepositoryDb _db;
        private Update _update;
        private readonly IKeyboardBotCreate _keyboardBotCreate;
        private readonly IUpdateService _updateService;

        private static bool _registrationFlag = false;

        //private static Domain.Models.User _user;
        private static Domain.Models.User _userMale;
        private static Domain.Models.User _userFemale;
        private static int _userOldChatId;
        private static Meeting _meeting = new Meeting();


        public CommandService(IKeyboardBotCreate keyboardBotCreate, IUpdateService updateService, IBaseRepositoryDb db)
        {
            _db = db;
            _keyboardBotCreate = keyboardBotCreate;
            _updateService = updateService;
        }



        public void SetUpdateBot(Update update)
        {
            _update = update;
        }

        /// <summary>
        /// Метод определения типа пришедшего update.
        /// </summary>
        /// <param name="type"></param>
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
                    //TODO Доделать ответ на неправильное сообщение от пользователя
                    break;
            }
        }


        /// <summary>
        /// Метод обработки сообщений.
        /// </summary>
        /// <param name="update"></param>
        private async void MessageCommand(Update update)
        {
            var message = update.Message.Text;
            switch (message)
            {
                case "/start":
                    if (CheckRegistration(update))
                    {
                        await _updateService.EchoTextMessageAsync(
                        update,
                        update.Message.From.Id,
                        BotPhrases.Start, 
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: AllCommandMenu()));
                    }
                    else
                    {
                        _registrationFlag = true;
                        await _updateService.EchoTextMessageAsync(
                            update,
                            update.Message.From.Id,
                            "Введите Ваше имя, год рождения и пол одной буквой(М - мужской, Ж - женский) через пробел: "
                            );
                    }
                    
                    break;

                default:
                    ParseInputText(message);
                    break;
            }
        }


        /// <summary>
        /// Метод обработки callback`ов
        /// </summary>
        /// <param name="update"></param>
        private async void CallbackMessageCommand(Update update)
        {
            var callbackData = update.CallbackQuery.Data;
            switch (callbackData)
            {

                case "Помощь":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        update.CallbackQuery.From.Id,
                        BotPhrases.Help,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            keyCollection: default,
                            key: Back()));
                    break;

                //основное меню
                case "Меню":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        update.CallbackQuery.From.Id,
                        BotPhrases.HomeMenu,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: HomeMenu()));
                    break;


                case "Мужчина":
                    var userMale = _db.SearchRandomUserToGender(true);
                    _userMale = userMale;
                    var userMaleString = GetUserInfo(userMale);
                    SendingShortCommand($"Смотрите что я нашел:       {userMaleString}", _update.CallbackQuery.From.Id);
                    SearchInterlocutorMenu(userMale);
                    break;

                case "Девушка":
                    var userFemale = _db.SearchRandomUserToGender(false);
                    _userFemale = userFemale;
                    var userFemaleString = GetUserInfo(userFemale);
                    SendingShortCommand($"Смотрите что я нашел:       {userFemaleString}", _update.CallbackQuery.From.Id);
                    SearchInterlocutorMenu(userFemale);
                    break;

                case "Мои встречи":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        update.CallbackQuery.From.Id,
                        "Ваши встречи: ",
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: MeetingAll(_update.CallbackQuery.From.Id)));
                    break;

                case "Следующий собеседник":
                case "Найти собеседника":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        update.CallbackQuery.From.Id,
                        BotPhrases.NextUser,
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: NextUser()));
                    break;

                case "Встреча c мужчиной":
                    SendingShortCommand("Встреча будет проходить на следующий день.", update.CallbackQuery.From.Id);
                    await _updateService.EchoTextMessageAsync(
                        update,
                        update.CallbackQuery.From.Id,
                        "Выберите время...  ",
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: default,
                            keyDictionary: СonfirmDictionary()));

                    _userOldChatId = update.CallbackQuery.From.Id;
                    break;


                case "Муж13":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        _userMale.ChatId,
                        "Вам предложена встреча завтра в 13:00",
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: СonfirmMenu()));
                    break;

                case "Муж14":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        _userMale.ChatId,
                        "Вам предложена встреча завтра в 14:00",
                        _keyboardBotCreate.CreateInlineKeyboard(
                            callBack: default,
                            key: default,
                            keyCollection: СonfirmMenuTwo()));
                    break;

                case "Подтвердить на 13:00":
                    ConfirmMeeting(13);
                    break;

                case "Подтвердить на 14:00":
                    ConfirmMeeting(14);
                    break;

                case "Встреча c девушкой":
                    break;


                default:
                    //ParseCallbackInputText(update.CallbackQuery.Data);
                    break;
            }
        }

        private void ConfirmMeeting(int time)
        {
            var date = DateTime.Today.AddDays(1).ToString();
            var dateSplit = date.Split('.', ' ');
            _meeting.DateOfMeeting = new DateTime(Convert.ToInt32(dateSplit[2]), Convert.ToInt32(dateSplit[1]), Convert.ToInt32(dateSplit[0]), time, 00, 00);
            _db.CreateMeeting(_meeting);

            var oldUser = _db.SearchUserToChatId(_userOldChatId);
            var currentUser = _db.SearchUserToChatId(_update.CallbackQuery.From.Id);

            oldUser.Meetings.Add(_meeting);
            currentUser.Meetings.Add(_meeting);
            _db.Save();

            SendingShortCommand("Встреча зарегистрирована!", _userOldChatId);
            SendingShortCommand("Встреча зарегистрирована!", _update.CallbackQuery.From.Id);
        }

        private Dictionary<string,string> СonfirmDictionary()
        {
            var start = new Dictionary<string,string>()
            {
                {"13:00", "Муж13"},
                {"14:00", "Муж14" }

            };
            return start;
        } 

        private List<string> СonfirmMenu()
        {
            var start = new List<string>()
            {
                "Подтвердить на 13:00",
                "Отмена"
            };
            return start;
        }

        private List<string> СonfirmMenuTwo()
        {
            var start = new List<string>()
            {
                "Подтвердить на 14:00",
                "Отмена"
            };
            return start;
        }

        private List<string> TimeMeeting()
        {
            var start = new List<string>()
            {
                "12:00",
                "13:00"
            };
            return start;
        }

        /// <summary>
        /// Командное меню для чата.
        /// </summary>
        /// <returns></returns>
        private List<string> AllCommandMenu() //Командная клавиатура
        {
            var start = new List<string>()
            {
                "Меню",
                "Помощь",
            };
            return start;
        }

        /// <summary>
        /// Home меню.
        /// </summary>
        /// <returns></returns>
        private List<string> HomeMenu() 
        {
            var start = new List<string>()
            {
                "Найти собеседника",
                "Мои встречи",
                "Редактировать профиль"
            };
            return start;
        }

        private List<string> NextUser()
        {
            var start = new List<string>()
            {
                "Мужчина",
                "Девушка"
            };
            return start;
        }

        private List<string> QuestionReadinessToMeet(bool gender)
        {
            if (gender)
            {
                var start = new List<string>()
            {
                "Встреча c мужчиной",
                "Следующий собеседник",
                "Меню"
            };
                return start;
            }
            else
            {
                var start = new List<string>()
            {
                "Встреча с девушкой",
                "Следующий собеседник",
                "Меню"
            };
                return start;
            }
           
        }

        private string Back() //button back
        {
            return "Меню";
        }


        /// <summary>
        /// Метод поиска собеседника.
        /// </summary>
        private async void SearchInterlocutorMenu(Domain.Models.User user)
        {
            int chatId = 0;
            if(_update.Type == UpdateType.Message)
            {
                chatId = _update.Message.From.Id;
            }
            if(_update.Type == UpdateType.CallbackQuery)
            {
                chatId = _update.CallbackQuery.From.Id;
            }
            await _updateService.EchoTextMessageAsync(
                _update,
                chatId,
                message: "Хотите встретиться?",
                keyboard: _keyboardBotCreate.CreateInlineKeyboard(
                    keyCollection: QuestionReadinessToMeet(user.Gender)));
        }


        /// <summary>
        /// Метод парсинга текстовых сообщений от пользователя.
        /// </summary>
        /// <param name="textMessage"></param>
        private void ParseInputText(string textMessage)
        {
            if (_registrationFlag)
            {
                var parseUser = textMessage.Split(' ');
                int birdth;
                int.TryParse(parseUser[1], out birdth);
                bool gender;
                parseUser[2].ToLower();
                if(parseUser[2] == "м")
                {
                    gender = true;
                }
                else if(parseUser[2] == "ж")
                {
                    gender = false;
                }
                else
                {
                    SendingShortCommand("Ошибка регистрации!", _update.Message.From.Id);
                    return;
                }

                var userCreateTrue = _db.CreateUser(new Domain.Models.User()
                        {
                            Name = parseUser[0],
                            MeetingReadinessStatus = true,
                            YearOfBirdth = birdth,
                            ChatId = _update.Message.From.Id,
                            AboutMy = "",
                            Gender = gender,
                            Rating = 1
                        });
                if (userCreateTrue)
                {
                    SendingShortCommand("Регистрация успешна", _update.Message.From.Id);
                    _registrationFlag = false;
                    var user = _db.SearchRandomUserToGender(gender);
                    var userString = GetUserInfo(user);
                    SendingShortCommand($"Могу сразу предложить Вам встречу:       {userString}", _update.Message.From.Id);
                    SearchInterlocutorMenu(user);
                }
                else
                {
                    SendingShortCommand("Ошибка регистрации!", _update.Message.From.Id);
                }
            }
        }


        private List<string> MeetingAll(int chatId)
        {

            var meetings = _db.SearchMeetingToUser(_db.SearchUserToChatId(chatId).Id);
            var meetingString = new List<string>();
            if(meetings != null)
            {
                foreach (var item in meetings)
                {
                    meetingString.Add(item.DateOfMeeting.ToString());
                }
                return meetingString;
            }
            else
            {
                return new List<string>() { "Встреч не зарегистрировано." };
            }
        }

        /// <summary>
        /// Метод проверки регистрации.
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        private bool CheckRegistration(Update update)
        {
            var userId = update.Message.From.Id;
            var user = _db.SearchUserToChatId(userId);

            if(user == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Метод формирования информации о пользователе.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GetUserInfo(Domain.Models.User user)
        {
            if(user == null)
            {
                return BotPhrases.BaseNull;
            }
            return $"Имя: {user.Name}  год рождения: {user.YearOfBirdth}   О себе: {user.AboutMy}";
        }

        /// <summary>
        /// Метод отправки короткого сообщение для пользователя.
        /// </summary>
        /// <param name="message"></param>
        private async void SendingShortCommand(string message, int chatId)
        {
            await _updateService.EchoTextMessageAsync(_update, chatId,
                message);
        }

    }
}
