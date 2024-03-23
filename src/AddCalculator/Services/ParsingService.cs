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
                customDelimiter = input.Substring(2)?.Split(@"\n").FirstOrDefault();

                // error out if is greater than a single char
                if (customDelimiter.Length > 1)
                    throw new Exception("Custom delimiter must be a single character");
            }

            return customDelimiter;
        }
    }
}
