using FinBot.App.Model;
using FinBot.Domain.Interfaces;
using FinBot.Domain.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinBot.App.Services
{
    public class UserControlService : IUserControl
    {
        private readonly IBaseRepositoryDb<User> _userDb;
        
        public UserControlService(IBaseRepositoryDb<User> userDb)
        {
            _userDb = userDb;
        }


        public async Task<bool> SetUser(long chatId, User user = default, CancellationToken cancel = default)
        {
            var users = await _userDb.GetCollection(cancel);
            if (chatId == 0) throw new NullReferenceException("User chatId null");

            var userGetCollection = users.FirstOrDefault(u => u.ChatId == chatId);
            if (userGetCollection is not null)
            {
                CurrentUser.Id = userGetCollection.Id;
                CurrentUser.ChatId = userGetCollection.ChatId;
                CurrentUser.NickName = userGetCollection.NickName;
                await _userDb.Update(userGetCollection, cancel);
                return true;
            }

            if (user is not null)
            {
                return await Create(user, cancel);
            }

            return false;
        }

        public async Task<bool> Create(User user, CancellationToken cancel)
        {
            if(user is null) throw new ArgumentNullException(nameof(user));

            return await _userDb.Create(user, cancel);
        }
    }
}
