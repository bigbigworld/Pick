using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pick2
{
    class Program
    {
        static void Main(string[] args)
        {
            Router router = new Router();
            string input = null;
            do {
                try
                {
                    input = Prompt();
                    router.Startup(input);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            } while (!String.IsNullOrWhiteSpace(input));

            Console.ReadKey();
        }

        static string Prompt() {
            Console.Write(">");

            string input = Console.ReadLine();

            return input;
        }


    }
}
