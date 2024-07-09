using Ordering.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine($"Starting {builder.Environment.ApplicationName} up");

try
{
    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    app.Run();
}
catch (Exception ex)
{
    var type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;

    Console.WriteLine($"Unhandled exception: {ex.Message}");
}
finally
{
    Console.WriteLine($"Shutdown {builder.Environment.ApplicationName} complete");
}


