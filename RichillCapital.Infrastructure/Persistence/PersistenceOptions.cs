namespace RichillCapital.Infrastructure.Persistence;

public sealed record PersistenceOptions
{
    public MsSqlOptions MsSqlOptions { get; init; } = new();
}

public sealed record MsSqlOptions
{
    public string ConnectionString { get; init; } = string.Empty;
    public int MaxRetryCount { get; init; }
    public int CommandTimeout { get; init; }
    public bool EnableDetailedErrors { get; init; }
    public bool EnableSensitiveDataLogging { get; init; }
}