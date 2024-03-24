using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddCalculator.Services.Interfaces
{
    internal interface IParsingService
    {
        List<string> ParseList(string input, List<string> customDelimiters);
        List<string> ParseParameters(string input);
    }
}
