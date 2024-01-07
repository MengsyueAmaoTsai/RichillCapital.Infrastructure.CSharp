using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Core.Domain.ValueObjects;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class SignalConfiguration : IEntityTypeConfiguration<Signal>
{
    public void Configure(EntityTypeBuilder<Signal> builder)
    {
        builder
            .ToTable("signals")
            .HasKey(signal => new
            {
                signal.Time,
                signal.Symbol,
                signal.TradeType,
            });

        // Time
        builder
            .Property(signal => signal.Time)
            .HasColumnName("time")
            .HasColumnType("datetimeoffset(3)")
            .IsRequired();

        // Symbol
        builder
            .Property(signal => signal.Symbol)
            .HasColumnName("symbol")
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(symbol => symbol.Value, symbol => new Symbol(symbol))
            .IsRequired();

        // TradeType
        builder
            .Property(signal => signal.TradeType)
            .HasColumnName("trade_type")
            .HasMaxLength(TradeType.Members.Max(member => member.Name.Length))
            .HasConversion(
                tradeType => tradeType.Name,
                tradeType => TradeType.FromName(tradeType, true).Value)
            .IsRequired();

        // Quantity
        builder
            .Property(signal => signal.Quantity)
            .HasColumnName("quantity")
            .HasColumnType("decimal(20, 10)")
            .IsRequired();

        // Price
        builder
            .Property(signal => signal.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(20, 10)")
            .IsRequired();
    }
}