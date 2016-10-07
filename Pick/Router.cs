using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pick2
{
    public enum Keyword
    {
        MODE, PATTERN, SEARCH, MISMATCH
    }

    public enum Mode { 
        BAOSHEN, PICK, MISMATCH
    }

    public class Router
    {
        public const string PREFIX = "-";
        public string origin;
        public List<string> arguments;
        public Dictionary<string, List<string>> argumentsDic;

        public Router(){}

        public void Startup(string input)
        {
            origin = input;
            arguments = ParseInputArguments(input);
            Startup();
        }

        public void Startup(List<string> args) { 
            origin = null;
            arguments = args;
            Startup();
        }

        private void Startup() {
            Parse();
            Dispatch();
        }

        private List<string> ParseInputArguments(string input)
        {
            List<string> result = new List<string>();
            Regex splitRegex = new Regex(@"\S+");
            MatchCollection matches = splitRegex.Matches(input);

            foreach (Match match in matches)
            {
                result.Add(match.Value);
            }

            return result;
        }

        public void Dispatch() {
            Mode mode = Mode.PICK;

            if (ArgumentExpect(Keyword.MODE.ToString())) {
                try
                {
                    mode = (Mode)Enum.Parse(typeof(Mode), argumentsDic[Keyword.MODE.ToString()][0].ToUpper());
                }
                catch (Exception) {
                    mode = Mode.MISMATCH;
                }
            }

            switch(mode){
                case Mode.PICK: 
                    string pattern = null, search = null;

                    if (ArgumentExpect(Keyword.PATTERN.ToString())) {
                        pattern = argumentsDic[Keyword.PATTERN.ToString()][0];                      
                    } else {
                        throw new ArgumentException("参数pattern不能为空。");
                    }

                    if (ArgumentExpect(Keyword.SEARCH.ToString())) {
                        search = argumentsDic[Keyword.SEARCH.ToString()][0];                      
                    } else {
                        throw new ArgumentException("参数search不能为空。");
                    }

                    PickArguments actionArgs = new PickArguments(){
                        Pattern = pattern,
                        Search = search
                    };

                    IEnumerable<string> executeResult = new PickAction().Execute(actionArgs);
                    Utility.PrintCollection(executeResult);

                    break;
                case Mode.BAOSHEN:
                    string routeStr = BaoshenHelper.Prompt();
                    Startup(routeStr);
                    break;
                default:
                    throw new Exception("未找到任何匹配的模式。");
            }

        }

        public void Parse()
        {
            argumentsDic = new Dictionary<string, List<string>>();

            string lastKeyword = Keyword.MODE.ToString();
            bool firstKeyword = true;

            for (int i = 0; i < arguments.Count; i++)
            {
                string argument = arguments[i];

                if (firstKeyword)
                {
                    if (!IsKeyword(argument))
                    {
                        argumentsDic.Add(lastKeyword, new List<string>());
                    }
                    firstKeyword = false;
                }

                if (IsKeyword(argument))
                {
                    argument = RemovePrefix(argument);
                    if (!argumentsDic.Keys.Contains(argument))
                    {
                        argument = argument.ToUpper();
                        argumentsDic.Add(argument, new List<string>());
                        lastKeyword = argument;
                    }
                }
                else
                {
                    argumentsDic[lastKeyword].Add(argument);
                }
            }
        }

        private bool IsKeyword(string word)
        {
            return word.StartsWith(PREFIX);
        }

        private string RemovePrefix(string word) { 
            if (IsKeyword(word)){
                return word.Substring(1);
            }
            return word;
        }

        private bool ArgumentExpect(string keyword, int count = 1) {
            return argumentsDic.Keys.Contains(keyword) && argumentsDic[keyword].Count >= count;
        }
    }
}
