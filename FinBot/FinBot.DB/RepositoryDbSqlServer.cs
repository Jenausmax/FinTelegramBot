using FinBot.Domain.Interfaces;
using FinBot.Domain.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinBot.DB
{
    public class RepositoryDbSqlServer : IBaseRepositoryDb
    {
        private AppDbContext _db;

        public RepositoryDbSqlServer(AppDbContext context)
        {
            _db = context;
        }

        public void CreateMeeting(Meeting meeting)
        {
            if(meeting != null)
            {
                _db.Meetings.Add(meeting);
                Save();
            }
            
        }

        public bool CreateUser(User user)
        {
            if(user != null)
            {
                _db.Users.Add(user);
                Save();
                return true;
            }
            return false;
        }

        public bool DeleteMeeting(int id)
        {
            var user = _db.Meetings.FirstOrDefault(u => u.Id == id);
            if(user != null)
            {
                _db.Meetings.Remove(user);
                Save();
                return true;
            }

            return false;
        }

        public bool DeleteUser(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _db.Users.Remove(user);
                Save();
                return true;
            }

            return false;
        }

        public List<Meeting> GetCollectionMeeting()
        {
            return _db.Meetings.ToList();
        }

        public List<User> GetCollectionsUser()
        {
            return _db.Users.ToList();
        }

        public Meeting SearchMeeting(int id)
        {
            var meeting = _db.Meetings.FirstOrDefault(u => u.Id == id);
            if (meeting != null)
            {
                return meeting;
            }

            return null;
        }

        public List<Meeting> SearchMeetingToUser(int userId)
        {
            var user = SearchUser(userId);
            if(user != null)
            {
               return _db.Meetings.Include(u => u.Users.Where(x => x.Id == userId)).ToList();
            }
            return null;
        }

        public User SearchRandomUserToGender(bool gender)
        {
            var usersToGender = _db.Users.Where(i => i.Gender == gender).Where(x => x.MeetingReadinessStatus == true).ToList();
            if(usersToGender != null && usersToGender.Count > 0)
            {
                var userRandom = new Random().Next(0, usersToGender.Count());
                return usersToGender[userRandom];
            }
            return null;
        }

        public User SearchUser(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return user;
            }

            return null;
        }

        public User SearchUser(string name)
        {
            var user = _db.Users.FirstOrDefault(u => u.Name == name);
            if (user != null)
            {
                return user;
            }

            return null;
        }

        public User SearchUserToChatId(int chatId)
        {
            var user = _db.Users.FirstOrDefault(u => u.ChatId == chatId);
            if(user != null)
            {
                return user;
            }

            return null;
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
