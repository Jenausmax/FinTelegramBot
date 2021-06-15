using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBot.Domain.Interfaces
{
    public interface IBaseCoInEntity : IEntity
    {
        DateTime Date { get; set; }
        double Money { get; set; }
        int CategoryId { get; set; }
        int UserId { get; set; }
    }
}
