using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBot.App.Keyboard
{
    internal static class KeyboardCollection
    {
        internal static List<string> CollectionsKey { get; set; } = new List<string>();

        internal static List<string> Back()
        {
            CollectionsKey.Add("Назад");
            return CollectionsKey;
        }
    }
}
