using AddCalculator.Services;
using AddCalculator.Services.Interfaces;
using Castle.Core.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace CalculatorTestApp.Services
{
    public static class AdditionVerification
    {

        [Fact]
        public static void EmptyListInputReturnsZero() {
            var service = new AdditionService();
            var emptyList = new List<int>();
            var result = service.AddNumbers(emptyList);
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(-5, 20)]
        [InlineData(1,2,3)]
        [InlineData(15, 22, 100)]
        public static void IntListReturnsResult(params int[] input)
        {
            var service = new AdditionService();
            var result = service.AddNumbers(input.ToList());
            int testResult = 0;
            foreach (var i in input)
                testResult += i;
            Assert.Equal(testResult, result);
        }

    }
}
