using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinBot.Domain.Models.Entities;

namespace FinBot.Domain.Models
{
    public class LogBot : Entity
    {
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public TypeMessage TypeMessage { get; set; }

    }

    public enum TypeMessage
    {
        work = 1,
        error = 2,
    }
}
