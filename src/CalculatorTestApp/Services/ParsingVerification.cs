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
            var result = service.ParseList(input, null);
            Assert.NotNull(result);
        }

        [Fact]
        public static void DoubleDelimitersAreParsedToEmptyStrings()
        {
            var service = new ParsingService();
            var result = service.ParseList(@"100,-3\n,8,,12\n88", null);
            Assert.Equal(new List<string>() { "100", "-3", "", "8", "", "12", "88" }, result);
        }

        [Fact]
        public static void CustomCharDelimiterIsReturned() {
            var service = new ParsingService();
            var result = service.ParseParameters(@"//[#]\n55,4#ff#1005,3\n6,,18");
            Assert.Equal( "#", result);
        }

        [Fact]
        public static void EmptyCharDelimiterIsReturned()
        {
            var service = new ParsingService();
            var result = service.ParseParameters(@"//\n55,4#ff#1005,3\n6,,18");
            Assert.Equal("", result);
        }

        [Theory]
        [InlineData(@"//[[]\n3,6,9[100[500")]
        [InlineData(@"//[***]\n3,6,9***100***500")]
        [InlineData(@"//[\.\]\n3,6,9\.\100\.\500")]
        public static void TestDelimiterReturned(string input)
        {
            var service = new ParsingService();
            var result = service.ParseParameters(input);
            
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(@"//***\n3,6,9***100***500")]
        public static void InvalidFormatShouldThrowException(string input)
        {
            var service = new ParsingService();

            Assert.Throws<Exception>(() => service.ParseParameters(input));
        }
    }
}
