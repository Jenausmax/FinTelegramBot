using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinBot.Domain.Interfaces
{
    public interface IKeyboardBotCreate
    {
        InlineKeyboardMarkup CreateInlineKeyboard(string callBack = default, string key = default, 
            IList<string> keyCollection = default, 
            IDictionary<string,string> keyCallbackCollection = default,
            List<List<string>> collectionButtonRows = default);
    }
}
