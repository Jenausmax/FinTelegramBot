using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinBot.Domain.Models.Entities;

namespace FinBot.Domain.Models
{
    public class User : Entity
    {
        //public User()
        //{
            
        //}
        public long ChatId { get; set; }
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDelete { get; set; }
        public List<Category> Categories { get; set; }
        public List<Consumption> Consumptions { get; set; }
        public List<Income> Incomes { get; set; }
    }
}
