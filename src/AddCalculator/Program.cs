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
    var parsingService = ActivatorUtilities.GetServiceOrCreateInstance<ParsingService>(host.Services);
    Console.CancelKeyPress += new ConsoleCancelEventHandler(cancelEventHandler);

    do
    {
        Console.WriteLine("Please enter numbers with a comma separator or newline separator to add, or Ctrl+C to exit.");

        try
        {
            // Rev 3: Add newline to valid delimiters
            var input = Console.ReadLine();
            var userList = parsingService.ParseList(input);

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
    } while (true);

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

static void cancelEventHandler(object sender, ConsoleCancelEventArgs args)
{
    Environment.Exit(-1);
}