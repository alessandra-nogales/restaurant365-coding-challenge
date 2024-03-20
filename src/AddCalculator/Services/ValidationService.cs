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
        private readonly int _maxNumbersAllowed;
        public ValidationService(IConfiguration config) {
            var s = config.GetRequiredSection("Settings").Get<Settings>();
            _maxNumbersAllowed = s.MaxInputNumbers;
        }
        public List<int> ValidateInput(List<string> userInput) {
            List<int> result = new List<int>();
            // Note: unclear if we should throw out invalid numbers before performing the limit calculation
            if (userInput?.Count() > _maxNumbersAllowed)
                throw new Exception("Too many inputs");

            foreach (var s in userInput) { 
                // verify is valid int, else skip it
                if(int.TryParse(s, out int i))
                    result.Add(i);
            }
            return result;
        }

    }
}
