using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.ValueObjects;

namespace RichillCapital.Infrastructure.Persistence;

public static class Seeds
{
    private static readonly List<User> Users = [
        User.Create(UserId.From("1"), Email.From("mengsyue.tsai@outlook.com"), new Name("Meng Syue Tsai")),
        User.Create(UserId.From("2"), Email.From("mengsyue.tsai@gmail.com"), new Name("Meng Syue Tsai")),
    ];

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.GetRequiredService<MsSqlDbContext>();

        context.Set<User>().AddRange(Users);

        context.SaveChanges();
    }
}