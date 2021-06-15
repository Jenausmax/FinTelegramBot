using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBot.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Пол пользователя. true - мужской, false - женский.
        /// </summary>
        public bool Gender { get; set; }
        public int ChatId { get; set; }
        public string AboutMy { get; set; }
        public int YearOfBirdth { get; set; }
        public bool MeetingReadinessStatus { get; set; }
        [Range(1, 5, ErrorMessage = "Недопустимый рейтинг")]
        public int Rating { get; set; }
        public List<Meeting> Meetings { get; set; } = new List<Meeting>();
    }
}
