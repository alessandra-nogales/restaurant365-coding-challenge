using AddCalculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddCalculator.Services
{
    public class ParsingService : IParsingService
    {

        public ParsingService() { }

        public List<string> ParseList(string input) {
            return input?.Split(new string[] { ",", @"\n" }, StringSplitOptions.TrimEntries)?.ToList();
        }
    }
}
