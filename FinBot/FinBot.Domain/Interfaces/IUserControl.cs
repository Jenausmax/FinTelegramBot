using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FinBot.Domain.Models;

namespace FinBot.Domain.Interfaces
{
    public interface IUserControl
    {
        Task<bool> SetUser(long chatId, User user = default, CancellationToken cancel = default);
        Task<bool> Create(User user, CancellationToken cancel = default);
    }
}
