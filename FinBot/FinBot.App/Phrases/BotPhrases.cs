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

        internal static string AddSettingMenu = "Add Category - Добавить новую категорию; Remove Category - Удалить категорию(пока не реализовано).";
        internal static string HomeMenu = "Setting - Можно добавить или удалить категорию; Help - помощь по использованию бота.";

        internal static string UpdateSuccessful = "update successful!";
        internal static string CategoryExist = "This category exists! Please enter a different name.";
    }
}
