using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinBot.Domain.Interfaces;

namespace FinBot.App.Services
{
    public class LogerBot : ILogerBot
    {
        public void WorkLogCreate(string message)
        {
            throw new NotImplementedException();
        }

        public void ErrorLogCreate(string message)
        {
            throw new NotImplementedException();
        }
    }
}
