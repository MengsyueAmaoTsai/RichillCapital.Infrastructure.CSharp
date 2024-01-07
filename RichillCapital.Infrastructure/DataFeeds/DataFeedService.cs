// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Options;

// using RichillCapital.Core.Abstractions;
// using RichillCapital.Core.Shared.DataFeeds;

// namespace RichillCapital.Infrastructure.DataFeeds;

// public sealed class DataFeedService : IDataFeedService
// {
//     private readonly ILogger<DataFeedService> _logger;
//     private readonly DataFeedOptions _options;
//     private readonly IServiceProvider _serviceProvider;
//     private readonly IDataFeedEventDispatcher _dataFeedEventDispatcher;
//     public DataFeedService(
//         ILogger<DataFeedService> logger,
//         IOptions<DataFeedOptions> options,
//         IServiceProvider serviceProvider,
//         IDataFeedEventDispatcher dataFeedEventDispatcher)
//     {
//         _logger = logger;
//         _options = options.Value;
//         _serviceProvider = serviceProvider;
//         _dataFeedEventDispatcher = dataFeedEventDispatcher;
//         LogConfigurations(_options.Configurations);
//     }
//     public async Task InitializeAsync()
//     {
//         foreach (var config in GetEnabledConfigurations())
//         {
//             var dataFeed = _serviceProvider.GetKeyedService<IDataFeed>(config.ConnectionName);
//             if (dataFeed is null)
//             {
//                 continue;
//             }
//             dataFeed.SetConfiguration(config);
//             if (_options.EstablishConnectionOnStart)
//             {
//                 await dataFeed.EstablishConnectionAsync();
//             }
//         }
//     }
//     private IEnumerable<DataFeedConfiguration> GetEnabledConfigurations() =>
//         _options.Configurations.Where(configuration => configuration.Enable);
//     private void LogConfigurations(IEnumerable<DataFeedConfiguration> configurations)
//     {
//         foreach (var configuration in configurations)
//         {
//             _logger.LogInformation(
//                 "  - DataFeed {providerName} - {connectionName} Enabled: {enable}",
//                 configuration.ProviderName,
//                 configuration.ConnectionName,
//                 configuration.Enable);
//             if (!configuration.Enable)
//             {
//                 continue;
//             }
//             foreach (var arg in configuration.Arguments)
//             {
//                 _logger.LogInformation("    - {key}: {value}", arg.Key, arg.Value);
//             }
//         }
//     }
// }