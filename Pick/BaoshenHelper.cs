using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick2
{
    public static class BaoshenHelper
    {
        public static string Prompt()
        {
            string result = null;

            StringBuilder promptText = new StringBuilder();
            promptText.AppendLine("预设的模式字符串如下：");
            promptText.AppendLine("1. @L(\"***\").");
            promptText.AppendLine("2. t(\"***\").");
            promptText.AppendLine("3. L(\"***\").");
            promptText.AppendLine("3. l(\"***\").");
            Console.Write(promptText.ToString());

            Console.WriteLine("请选择:");
            string choose = Console.ReadLine();

            Console.WriteLine("请输入你要搜索的路径:");
            string search = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    result = string.Format("-pattern {0} -search {1}", "@L\\(\"(.*)\"\\)", search);
                    break;
                case "2":
                    result = string.Format("-pattern {0} -search {1}", "t\\(\"(.*)\"\\)", search);
                    break;
                case "3":
                    result = string.Format("-pattern {0} -search {1}", "L\\(\"(.*)\"\\)", search);
                    break;
                case "4":
                    result = string.Format("-pattern {0} -search {1}", "l\\(\"(.*)\"\\)", search);
                    break;
                default:
                    Console.WriteLine("选择错误。");
                    break;
            }

            return result;
        }
    }
}
