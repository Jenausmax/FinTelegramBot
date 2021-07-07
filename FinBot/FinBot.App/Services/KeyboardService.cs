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
        public virtual InlineKeyboardMarkup CreateInlineKeyboard(string callBack = default, string key = default, 
            IList<string> keyCollection = default, 
            IDictionary<string,string> keyCallbackDictionary = default,
            List<List<string>> collectionButtonRows = default)
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

                if (collectionButtonRows is not null)
                {
                    List<List<InlineKeyboardButton>> buttonRowsList = new List<List<InlineKeyboardButton>>();

                    foreach (var collectionButtonRow in collectionButtonRows)
                    {
                        List<InlineKeyboardButton> buttonsRow1 = new List<InlineKeyboardButton>();
                        foreach (var item in collectionButtonRow)
                        {
                            buttonsRow1.Add(new InlineKeyboardButton().Text = item);
                        }
                        buttonRowsList.Add(buttonsRow1);
                    }

                    return new InlineKeyboardMarkup(buttonRowsList);
                }
            }
            else
            {
                if (key != null)
                {
                    return new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData(key, callBack));
                }

                if (keyCallbackDictionary is not null)
                {
                    List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
                    foreach (var item in keyCallbackDictionary)
                    {
                        buttons.Add(InlineKeyboardButton.WithCallbackData(item.Key, item.Value));
                    }

                    return new InlineKeyboardMarkup(buttons);
                }

                
            }

            return null;
        }
    }
}
