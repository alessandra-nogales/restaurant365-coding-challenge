using AddCalculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddCalculator.Services
{
    public class ParsingService : IParsingService
    {
        private readonly Regex _delimiterRegex = new Regex($@"^[ (.) +  ]$");
        private readonly string _pattern = $@"^[(.\.)+]$";

        public ParsingService() { }

        public List<string> ParseList(string input, string customDelimiter)
        {
            return input?.Split(new string[] { ",", @"\n", customDelimiter }, StringSplitOptions.TrimEntries)?.ToList();
        }

        public string ParseParameters(string input)
        {
            string customDelimiter = String.Empty;
            if (String.IsNullOrEmpty(input))
                return customDelimiter;

            if (input.StartsWith("//"))
            {
                string delimiterInput = input.Substring(2)?.Split(@"\n").FirstOrDefault();
                if (!String.IsNullOrEmpty(delimiterInput))
                {
                    var delimiter = Regex.Split(delimiterInput, _pattern)?.FirstOrDefault();
                    if (delimiter.IndexOf('[') > -1 && delimiter.IndexOf(']') > -1) 
                        customDelimiter = delimiter.Remove(delimiter.Length - 1, 1)?.Remove(0, 1); 
                    else
                        throw new Exception($@"Delimiter must be delineated with format //[{{delimiter}}]\n{{numbers}}");
                    
                }
            }

            return customDelimiter;
        }
    }
}
