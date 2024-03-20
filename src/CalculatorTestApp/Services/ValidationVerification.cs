using AddCalculator.Services;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CalculatorTestApp.Services
{
    public static class ValidationVerification
    {
        [Fact]
        public static void NoMaxInputsDoesNotThrowException()
        {
            var service = new ValidationService();

            var inputList = new List<string> { "1", "2", "5", "200", "-20", "8", "15" };
            var list = service.ValidateInput(inputList);
            Assert.Equal(list.Select(x => x.ToString()).ToList(), inputList);
        }

        [Theory]
        [InlineData("a", "bcdef")]
        public static void VerifyEmptyListWhenNonIntStrings(params string[] input)
        {
            var service = new ValidationService();
            var result = service.ValidateInput(input.ToList());
            Assert.Equal(new List<int>(), result);
        }

        [Theory]
        [InlineData("a", "1")]
        [InlineData("a", "100")]
        [InlineData("a", "bcdef")]
        [InlineData("a")]
        [InlineData("22", "155")]
        [InlineData("-22", "45")]
        [InlineData("-22", "")]
        public static void VerifyOnlyIntsLeftInListWithMixedInput(params string[] input)
        {
            var service = new ValidationService();
            var result = service.ValidateInput(input.ToList());
            Assert.All(result, item => Assert.True(int.TryParse(item.ToString(), out int i)));
        }
    }
}
