using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
           this IServiceCollection services)
    {
        services.AddOptions<PersistenceOptions>()
            .BindConfiguration(nameof(PersistenceOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddMsSql();

        return services;
    }

    private static IServiceCollection AddMsSql(this IServiceCollection services)
    {
        services.AddDbContext<MsSqlDbContext>((serviceProvider, options) =>
        {
            var sqlServerOptions = serviceProvider
                .GetRequiredService<IOptions<PersistenceOptions>>()
                .Value.MsSqlOptions;

            options.UseSqlServer(sqlServerOptions.ConnectionString, options =>
            {
                options.EnableRetryOnFailure(sqlServerOptions.MaxRetryCount);
                options.CommandTimeout(sqlServerOptions.CommandTimeout);
            });

            options.EnableDetailedErrors(sqlServerOptions.EnableDetailedErrors);
            options.EnableSensitiveDataLogging(sqlServerOptions.EnableSensitiveDataLogging);
        });

        services
            .AddScoped(typeof(IRepository<>), typeof(SqlServerRepository<>))
            .AddScoped(typeof(IReadonlyRepository<>), typeof(SqlServerRepository<>))
            .AddScoped<IUnitOfWork>(serviceProvider =>
                serviceProvider.GetRequiredService<MsSqlDbContext>());

        return services;
    }
}