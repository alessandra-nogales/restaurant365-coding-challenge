using AddCalculator.Services;
using Xunit;

namespace CalculatorTestApp.Services
{
    public class ParsingVerification
    {

        [Theory]
        [InlineData(@"1\n2,3")]
        [InlineData(@"")]
        [InlineData(@"100,57\n-37, 88")]
        public static void HappyPathDelimitersIntsReturnListOfStrings(string input)
        {
            var service = new ParsingService();
            var result = service.ParseList(input);
            Assert.NotNull(result);
        }

        [Fact]
        public static void DoubleDelimitersAreParsedToEmptyStrings()
        {
            var service = new ParsingService();
            var result = service.ParseList(@"100,-3\n,8,,12\n88");
            Assert.Equal(result, new List<string>() { "100", "-3", "", "8","", "12", "88"});
        }
    }
}
