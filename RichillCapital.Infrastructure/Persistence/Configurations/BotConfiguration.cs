using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Core.Domain.ValueObjects;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class BotConfiguration : IEntityTypeConfiguration<Bot>
{
    public void Configure(EntityTypeBuilder<Bot> builder)
    {
        builder
            .ToTable("bots")
            .HasKey(bot => bot.Id);

        // Id
        builder
            .Property(bot => bot.Id)
            .HasColumnName("id")
            .HasMaxLength(BotId.MaxLength)
            .HasConversion(id => id.Value, id => new BotId(id))
            .IsRequired();

        // Id
        builder
            .Property(bot => bot.Name)
            .HasColumnName("name")
            .HasMaxLength(Name.MaxLength)
            .HasConversion(name => name.Value, name => new Name(name))
            .IsRequired();

        // Description
        builder
            .Property(bot => bot.Description)
            .HasColumnName("description")
            .HasMaxLength(Description.MaxLength)
            .HasConversion(
                description => description.Value,
                description => new Description(description))
            .IsRequired();

        // Platform
        builder
            .Property(bot => bot.Platform)
            .HasColumnName("platform")
            .HasMaxLength(TradingPlatform.Members.Max(member => member.Name.Length))
            .HasConversion(
                platform => platform.Name,
                platform => TradingPlatform.FromName(platform, true).Value)
            .IsRequired();
    }
}