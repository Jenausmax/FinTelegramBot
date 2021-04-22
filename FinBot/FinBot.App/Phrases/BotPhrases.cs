using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBot.App.Phrases
{
    public static class BotPhrases
    {
        internal static string Start = "Привет! Я CoinBot, помогу вам записывать свои доходы, расходы и получать статистику по балансу.";
        internal static string Income = "Запишите свои доходы: ";
        internal static string Consumption = "Запишите свои расходы: ";
        internal static string Help = "CoinBot помогает Вам вести свои личные финансы. Для настройки отправьте команду /start и потом выберите Setting. " +
                                      "Далее вы сможете добавить категории Дохода или Расхода. " +
                                      "Для записи Дохода введите команду /income. " +
                                      "Для записи Расхода введите команду /consumption. " +
                                      "Для просмотра статистики введите команду /balance.";
    }
}
