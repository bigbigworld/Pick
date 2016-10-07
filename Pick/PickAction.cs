using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pick2
{
    public  class PickAction
    {
        public IEnumerable<string> Execute(PickArguments arguments)
        {
            HashSet<string> result = new HashSet<string>();

            // 搜索路径
            string searchPath = arguments.Search.Replace(@"\", "/");
            List<string> searchFilePathList = new List<string>();

            if (searchPath.EndsWith(@"/"))
            {
                if (!Directory.Exists(searchPath)) {
                    throw new Exception(string.Format("\"{0}\"目录不存在！", searchPath));
                }

                searchFilePathList.AddRange(Directory.GetFiles(searchPath, "*", SearchOption.AllDirectories));
            }
            else {
                if (!File.Exists(searchPath)) { 
                    throw new Exception(string.Format("\"{0}\"文件不存在！", searchPath));
                }

                searchFilePathList.Add(searchPath);
            }

            // 匹配模式
            Regex searchRegex = new Regex(arguments.Pattern);

            foreach (string searchFilePath in searchFilePathList) {
                string searchText = File.ReadAllText(searchFilePath);
                MatchCollection matches = searchRegex.Matches(searchText);

                foreach (Match match in matches) {
                    result.Add(match.Groups[1].Value);
                }
            }

            return result;
        }
    }

    public class PickArguments {
        public string Pattern { get; set; }
        public string Search { get; set; }
    }
}
