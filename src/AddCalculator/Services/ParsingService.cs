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
        private readonly string _pattern = $@"\[(.*?)\]";
        public ParsingService() { }

        /// <summary>
        /// Parse out the numbers
        /// </summary>
        /// <param name="input"></param>
        /// <param name="customDelimiters"></param>
        /// <returns></returns>
        public List<string> ParseList(string input, List<string> customDelimiters)
        {
            var splitStr = new List<string>() { ",", @"\n" };
            if(customDelimiters?.Count > 0)
                splitStr.AddRange(customDelimiters);
            return input?.Split(splitStr.ToArray(), StringSplitOptions.TrimEntries)?.ToList();
        }

        /// <summary>
        /// Parse out the custom delimiters
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<string> ParseParameters(string input)
        {
            List<string> customDelimiters = new List<string>();
            if (String.IsNullOrEmpty(input))
                return customDelimiters;

            if (input.StartsWith("//"))
            {
                var delimiterInputMany = input.Substring(2)?.Split(@"\n").FirstOrDefault();

                var regex = new Regex(_pattern);
                var matches = regex.Matches(delimiterInputMany);
                // if user tries to add a delimiter without the expected format, throw exception and print correct format
                if(!String.IsNullOrEmpty(delimiterInputMany) && matches?.Count == 0)
                    throw new Exception($@"Delimiter must be delineated with format //[{{delimiter}}][{{delimiter}}]...\n{{numbers}}");

                foreach (var d in matches.Select(x => x?.ToString()))
                {
                    if (d.IndexOf('[') > -1 && d.IndexOf(']') > -1)
                        customDelimiters.Add(d.Remove(d.Length - 1, 1)?.Remove(0, 1));
                }
            }

            return customDelimiters.Distinct().ToList();
        }
    }
}
