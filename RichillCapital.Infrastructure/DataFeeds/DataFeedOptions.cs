namespace RichillCapital.Infrastructure.DataFeeds;

public sealed record DataFeedOptions
{
    public bool EstablishConnectionOnStart { get; init; }

    public IEnumerable<DataFeedConfiguration> Configurations { get; init; } =
        new List<DataFeedConfiguration>();
}

public sealed record DataFeedConfiguration
{
    public string ProviderName { get; init; } = string.Empty;

    public bool Enable { get; init; }

    public string ConnectionName { get; init; } = string.Empty;

    public Dictionary<string, object> Arguments { get; init; } = [];
}