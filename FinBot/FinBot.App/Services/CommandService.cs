using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinBot.Domain.Interfaces;

namespace FinBot.App.Services
{
    class CommandService : ICommandBot
    {
        public string Command { get; set; }
        public void SetCommandBot(string command)
        {
            throw new NotImplementedException();
        }
    }
}
