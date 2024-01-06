using Microsoft.EntityFrameworkCore;

using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Infrastructure.Persistence;

public sealed class MsSqlDbContext : DbContext, IUnitOfWork
{
    private readonly IDomainEventDispatcher? _dispatcher;

    public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options, IDomainEventDispatcher dispatcher)
        : base(options) => _dispatcher = dispatcher;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        if (_dispatcher is null)
        {
            return result;
        }

        var entitiesWithEvents = ChangeTracker.Entries<IHasDomainEvent>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .ToArray();

        await _dispatcher.DispatchAndClearDomainEvents(entitiesWithEvents);

        return result;
    }

    public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MsSqlDbContext).Assembly);
    }
}