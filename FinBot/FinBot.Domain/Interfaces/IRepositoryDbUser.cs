using FinBot.Domain.Models;
using System.Collections.Generic;

namespace FinBot.Domain.Interfaces
{
    public interface IRepositoryDbUser
    {
        bool CreateUser(User user);
        bool DeleteUser(int id);
        User UpdateUser(User user);
        User SearchUser(int id);
        User SearchUser(string name);
        User SearchUserToChatId(int chatId);
        User SearchRandomUserToGender(bool gender);
        List<User> GetCollectionsUser();
        
    }
}
