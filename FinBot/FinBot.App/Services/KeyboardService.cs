using System;
using System.Collections.Generic;
using FinBot.Domain.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinBot.App.Services
{
    /// <summary>
    /// Класс создания кастомных клавиатур.
    /// </summary>
    public class KeyboardService : IKeyboardBotCreate
    {
        public virtual InlineKeyboardMarkup CreateInlineKeyboard(string callBack = default, string key = default, IList<string> keyCollection = default)
        {
            if (callBack == null)
            {
                if (key != null)
                {
                    return new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData(key));
                }

                if (keyCollection != null)
                {
                    List<InlineKeyboardButton> list = new List<InlineKeyboardButton>();
                    foreach (var keyNew in keyCollection)
                    {
                        list.Add(new InlineKeyboardButton().Text = keyNew);
                    }
                    return new InlineKeyboardMarkup(list);
                }
            }
            else
            {
                if (key != null)
                {
                    return new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData(key, callBack));
                }
            }




            return null;
        }
    }
}
