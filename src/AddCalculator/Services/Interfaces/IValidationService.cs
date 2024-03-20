using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddCalculator.Services.Interfaces
{
    public interface IValidationService
    {
        List<int> ValidateInput(List<string> userInput);
    }
}
