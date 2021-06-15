using FinBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBot.Domain.Interfaces
{
    public interface IRepositoryDbMeeting
    {
        void CreateMeeting(Meeting meeting);
        bool DeleteMeeting(int id);
        List<Meeting> GetCollectionMeeting();
        List<Meeting> SearchMeetingToUser(int userId);
        Meeting SearchMeeting(int id);


    }
}
