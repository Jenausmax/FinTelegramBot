using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBot.Domain.Interfaces
{
    public interface ILogerBot
    {
        void WorkLogCreate(string message);
        void ErrorLogCreate(string message);
    }
}
