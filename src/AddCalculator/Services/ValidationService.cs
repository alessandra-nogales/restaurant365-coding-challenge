using AddCalculator.Models;
using AddCalculator.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddCalculator.Services
{
    public class ValidationService : IValidationService
    {
        public ValidationService() {   
        }
        public List<int> ValidateInput(List<string> userInput) {
            List<int> result = new List<int>();
            if (userInput?.Count < 1)
                return result;

            foreach (var s in userInput) { 
                // verify is valid int, else skip it
                if(int.TryParse(s, out int i))
                    result.Add(i);
            }
            return result;
        }

    }
}
