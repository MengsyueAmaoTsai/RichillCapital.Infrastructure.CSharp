using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Core.Domain.ValueObjects;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class BalanceConfiguration : IEntityTypeConfiguration<Balance>
{
    public void Configure(EntityTypeBuilder<Balance> builder)
    {
        builder
            .ToTable("balances")
            .HasKey(balance => new
            {
                balance.AccountId,
                balance.Currency,
            });

        // AccountId
        builder
            .Property(balance => balance.AccountId)
            .HasColumnName("account_id")
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(id => id.Value, id => AccountId.From(id))
            .IsRequired();

        builder.HasOne<Account>()
            .WithMany()
            .HasForeignKey("account_id");

        // Amount
        builder
            .Property(signal => signal.Amount)
            .HasColumnName("amount")
            .HasColumnType("decimal(20, 10)")
            .IsRequired();

        // Currency
        builder
            .Property(account => account.Currency)
            .HasColumnName("currency")
            .HasMaxLength(Currency.Members.Max(member => member.Name.Length))
            .HasConversion(
                currency => currency.Name,
                currency => Currency.FromName(currency, true).Value)
            .IsRequired();
    }
}