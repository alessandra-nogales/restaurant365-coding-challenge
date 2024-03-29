﻿using AddCalculator.Services;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CalculatorTestApp.Services
{
    public static class ValidationVerification
    {
        [Fact]
        public static void NoMaxInputsDoesNotThrowException()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Settings:Min", "0"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var service = new ValidationService(configuration);

            var inputList = new List<string> { "1", "2", "5", "200", "8", "15" };
            var list = service.ValidateInput(inputList);
            Assert.Equal(list.Select(x => x.ToString()).ToList(), inputList);
        }

        [Fact]
        public static void NoMinAllowsNegativeNumbers()
        {
            // pass in no min in the configuration
            var inMemorySettings = new Dictionary<string, string> {
                
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var service = new ValidationService(configuration);

            var inputList = new List<string> { "1", "2", "5", "-200", "8", "-15" };
            var list = service.ValidateInput(inputList);
            Assert.Equal(list.Select(x => x.ToString()).ToList(), inputList); // no exception thrown
        }

        [Theory]
        [InlineData("-22", "45")]
        [InlineData("-22", "")]
        [InlineData("-11", "-8", "20", "15")]
        public static void VerifyExceptionThrownNegativeNumber(params string[] inputList) {
            var inMemorySettings = new Dictionary<string, string> {
                {"Settings:Min", "0"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var service = new ValidationService(configuration);

            //var list = service.ValidateInput(inputList.ToList());
            Assert.Throws<Exception>(() => service.ValidateInput(inputList.ToList()));
        }

        [Theory]
        [InlineData("a", "bcdef")]
        public static void VerifyEmptyListWhenNonIntStrings(params string[] input)
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Settings:Min", "0"}
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
        [InlineData("22", "155", "", "23", "88")]
        public static void VerifyOnlyIntsLeftInListWithMixedInput(params string[] input)
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Settings:Min", "0"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var service = new ValidationService(configuration);
            var result = service.ValidateInput(input.ToList());
            Assert.All(result, item => Assert.True(int.TryParse(item.ToString(), out int i)));
        }

        [Fact]
        public static void VerifyIntsOver1000AreThrownOut()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Settings:Max", "1000"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var service = new ValidationService(configuration);
            Random rnd = new Random();
            var input = new List<int>() { rnd.Next(0,1000), rnd.Next(1001,5000), rnd.Next(0,1000), rnd.Next(1001,7000)};
            
            var result = service.ValidateInput(input.Select(x=>x.ToString()).ToList());
            Assert.All(result, item => Assert.True(int.TryParse(item.ToString(), out int i) && i < 1000));

        }

        [Fact]
        public static void VerifyIntsOver5000AreThrownOut()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Settings:Max", "5000"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var service = new ValidationService(configuration);
            Random rnd = new Random();
            var input = new List<int>() { rnd.Next(0, 1000), rnd.Next(5001, 10000), rnd.Next(0, 1000), rnd.Next(5001, 7000) };

            var result = service.ValidateInput(input.Select(x => x.ToString()).ToList());
            Assert.All(result, item => Assert.True(int.TryParse(item.ToString(), out int i) && i < 5000));

        }
    }
}
