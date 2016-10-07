using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pick2
{
    public static class Utility
    {
        public static void PrintCollection(IEnumerable<string> collection)
        {
            if (collection == null) { return; }

            foreach (string item in collection)
            {
                Console.WriteLine(item);
            }
        }
    }
}
