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
        private readonly int? _min;
        public ValidationService(IConfiguration config) {
            var setting = config.GetSection("Settings").Get<Settings>();
            _min = setting?.Min;
        }
        public List<int> ValidateInput(List<string> userInput) {
            List<int> result = new List<int>();
            if (userInput?.Count < 1)
                return result;

            var lessThanMinVar = new List<int>();

            foreach (var s in userInput) { 
                // verify is valid int, else skip it
                if(int.TryParse(s, out int i))
                {
                    if (i < _min) // add to erroneous list
                        lessThanMinVar.Add(i);
                    else
                        result.Add(i);
                }
            }

            if(lessThanMinVar.Count > 0)
            {
                throw new Exception($"Numbers less than {_min} are not allowed : {String.Join(",",lessThanMinVar)}");
            }

            return result;
        }

    }
}
