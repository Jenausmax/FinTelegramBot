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
        private User _user;
        private readonly IBaseRepositoryDb<User> _userDb;
        
        public User User { get { return _user;} }

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
                _user = userGetCollection;
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
            return await _userDb.Create(user, cancel);
        }
    }
}
