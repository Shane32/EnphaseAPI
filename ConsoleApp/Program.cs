using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shane32.ConsoleDI;

internal sealed class Program
{
    private static async Task Main(string[] args)
        => await ConsoleHost.RunAsync<App>(args, CreateHostBuilder, app => app.RunAsync());

    // This method signature is required for Entity Framework Core tools — do not change it.
    public static IHostBuilder CreateHostBuilder(string[] args)
        => ConsoleHost.CreateHostBuilder(args, ConfigureServices);

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.Configure<ConsoleAppOptions>(context.Configuration.GetSection("Enphase"));
    }
}
