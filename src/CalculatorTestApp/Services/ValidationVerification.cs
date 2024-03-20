using AddCalculator.Services;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CalculatorTestApp.Services
{
    public static class ValidationVerification
    {
        [Fact]
        public static void TooManyInputsThrowsException()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Settings:MaxInputNumbers", "2"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var service = new ValidationService(configuration);

            Assert.Throws<Exception>(() => service.ValidateInput(new List<string> { "1", "2", "5" }));

        }

        [Theory]
        [InlineData("a", "bcdef")]
        public static void VerifyEmptyListWhenNonIntStrings(params string[] input)
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Settings:MaxInputNumbers", "2"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var service = new ValidationService(configuration);
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
            var inMemorySettings = new Dictionary<string, string> {
                {"Settings:MaxInputNumbers", "2"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var service = new ValidationService(configuration);
            var result = service.ValidateInput(input.ToList());
            Assert.All(result, item => Assert.True(int.TryParse(item.ToString(), out int i)));
        }
    }
}
