﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinBot.Domain.Interfaces
{
    public interface IUpdateService
    {
        Task EchoTextMessageAsync(Update update, string message, InlineKeyboardMarkup keyboard = default);
    }
}
