using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace RichillCapital.Infrastructure.DataFeeds;

public static class DependencyInjection
{
    public static IServiceCollection AddDataFeeds(this IServiceCollection services)
    {
        services.AddOptions<DataFeedOptions>()
            .BindConfiguration(nameof(DataFeedOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // services.AddScoped<IDataFeedService, DataFeedService>();
        var options = services
               .BuildServiceProvider()
               .GetRequiredService<IOptions<DataFeedOptions>>()
               .Value;

        foreach (var config in options.Configurations)
        {
            switch (config.ProviderName)
            {
                case "Max":
                    // services.AddKeyedSingleton<IDataFeed, MaxDataFeed>(config.ConnectionName);
                    break;

                case "Binance":
                    break;

                default:
                    break;
            }
        }

        return services;
    }
}