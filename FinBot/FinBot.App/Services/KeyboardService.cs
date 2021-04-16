using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinBot.Domain.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinBot.App.Services
{
    /// <summary>
    /// Класс создания кастомных клавиатур.
    /// </summary>
    public class KeyboardService : IKeyboardBotCreate
    {
        public InlineKeyboardMarkup CreateInlineKeyboard(IList<string> keyCollection)
        {
            throw new NotImplementedException();
        }
    }
}
