using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinBot.Domain.Interfaces;

namespace FinBot.Domain.Models.Entities
{
    public class BaseCoInEntity : 
        Entity, 
        IBaseCoInEntity
    {
        public DateTime Date { get; set; }
        public double Money { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
