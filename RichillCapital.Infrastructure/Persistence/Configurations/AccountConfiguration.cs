using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Core.Domain.ValueObjects;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .ToTable("accounts")
            .HasKey(account => account.Id);

        // Id
        builder
            .Property(account => account.Id)
            .HasColumnName("id")
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(id => id.Value, id => new AccountId(id))
            .IsRequired();

        // Name
        builder
            .Property(account => account.Name)
            .HasColumnName("name")
            .HasMaxLength(Name.MaxLength)
            .HasConversion(name => name.Value, name => new Name(name))
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