using AddCalculator.Services.Interfaces;

namespace AddCalculator.Services
{
    public class AdditionService : IAdditionService
    {

        public AdditionService() { 
        
        }
        public int AddNumbers(List<int> numbers) {
            if(numbers?.Count == 0) return 0;

            var result = 0;
            foreach (var i in numbers) {
                result += i;
            }

            return result;
        }
    }
}
