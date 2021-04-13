using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBot.Domain.Interfaces
{
    public interface ICommandBot
    {
        string Command { get; set; }
        void SetCommandBot(string command);
    }
}
