using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Infrastructure.Persistence;

public static class Seeds
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.GetRequiredService<MsSqlDbContext>();

        context.SaveChanges();
    }
}