using AddCalculator.Services;
using AddCalculator.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

try
{
    var host = AppStartup();
    var validationService = ActivatorUtilities.GetServiceOrCreateInstance<ValidationService>(host.Services);
    var additionService = ActivatorUtilities.GetServiceOrCreateInstance<AdditionService>(host.Services);

    Console.WriteLine("Please enter two numbers with a comma separator to add.");

    try
    {
        // Rev 1: Add a maximum of 2 numbers
        var input = Console.ReadLine();
        var userList = input?.Split(new char[] { ',' }).ToList();

        // take in user input & validate
        var list = validationService.ValidateInput(userList);

        // pass them into the service to add
        var result = additionService.AddNumbers(list);

        // return the result
        Console.WriteLine($"Result = {result}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }

}
catch (Exception ex)
{
    Console.WriteLine(ex.StackTrace);
}


static IHost AppStartup()
{
    var builder = new ConfigurationBuilder();
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");
    var host = Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient<IAdditionService, AdditionService>();
        }).Build();
    return host;

}