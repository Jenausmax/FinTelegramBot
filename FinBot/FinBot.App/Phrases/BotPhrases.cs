using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBot.App.Phrases
{
    public static class BotPhrases
    {
        internal static string Start = "Привет! Я CoinBot, помогу вам записывать свои доходы, расходы и получать статистику по балансу.              ";
        internal static string Income = "Запишите свои доходы: ";
        internal static string Consumption = "Запишите свои расходы: ";
        internal static string Help = "CoinBot помогает Вам вести свои личные финансы. Для настройки отправьте команду /start и потом выберите Home. " +
                                      "Далее вы сможете добавить категории Дохода или Расхода. " +
                                      "Для записи Дохода введите команду Income. " +
                                      "Для записи Расхода введите команду Consumption. " +
                                      "Для просмотра статистики введите команду Balance.";

        internal static string AddSettingMenuHelp = "Add Category - Добавить новую категорию; Remove Category - Удалить категорию(пока не реализовано).";
        internal static string HomeMenuHelp = "Setting - Можно добавить или удалить категорию; Help - помощь по использованию бота.";

        internal static string UpdateSuccessful = "update successful!";
        internal static string CategoryExist = "This category exists! Please enter a different name.";
        internal static string EnterCategoryName = "Enter category name: ";
        internal static string IncomeCategory = "Income category";
        internal static string ConsumptionCategory = "Consumption category";
        internal static string EnterTheAmount = "Enter the amount";
        internal static string SumAddingError = "Sum adding error";
        public static string Reminder = "Не забудь вписать свои траты!";

        /// <summary>
        /// Полное командное меню для чата.
        /// </summary>
        /// <returns></returns>
        internal static List<string> AllCommandMenu() //Командная клавиатура
        {
            var start = new List<string>()
            {
                "Consumption",
                "Income",
                "Balance",
                "Menu"
            };
            return start;
        }

        internal static List<string> SettingMenu() //Setting клавиатура
        {
            var start = new List<string>()
            {
                "Add category",
                "Remove category",
                "<---Back Home"
            };
            return start;
        }

        internal static List<string> AddSettingMenu() //Add Setting клавиатура
        {
            var start = new List<string>()
            {
                "Income category add",
                "Consumption category add",
                "<---Back Home"
            };
            return start;
        }

        internal static List<string> HomeMenu() //Add Setting клавиатура
        {
            var start = new List<string>()
            {
                "Setting",
                "Help",
                "<---Back Home"
            };
            return start;
        }

        internal static string Back() //button back
        {
            return "<---Back Home";
        }
    }
}
