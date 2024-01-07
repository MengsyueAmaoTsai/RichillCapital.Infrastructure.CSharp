using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.ValueObjects;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .ToTable("orders")
            .HasKey(order => order.Id);

        // Id
        builder
            .Property(order => order.Id)
            .HasColumnName("id")
            .HasMaxLength(OrderId.MaxLength)
            .HasConversion(id => id.Value, id => OrderId.From(id))
            .IsRequired();

        // Account Id
        builder
            .Property(order => order.AccountId)
            .HasColumnName("account_id")
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(id => id.Value, id => AccountId.From(id))
            .IsRequired();
    }
}