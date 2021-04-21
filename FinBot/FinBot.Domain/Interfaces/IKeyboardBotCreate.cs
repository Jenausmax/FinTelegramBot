﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinBot.Domain.Interfaces
{
    public interface IKeyboardBotCreate
    {
        InlineKeyboardMarkup CreateInlineKeyboard(string callBack = default, string key = default, IList<string> keyCollection = default);
    }
}
