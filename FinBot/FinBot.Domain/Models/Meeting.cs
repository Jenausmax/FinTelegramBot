using System;
using System.Collections.Generic;

namespace FinBot.Domain.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public DateTime DateOfMeeting { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
