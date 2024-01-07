using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.ValueObjects;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("users")
            .HasKey(user => user.Id);

        // Id
        builder
            .Property(user => user.Id)
            .HasColumnName("id")
            .HasMaxLength(UserId.MaxLength)
            .HasConversion(id => id.Value, id => UserId.From(id))
            .IsRequired();

        // Email
        builder
            .Property(user => user.Email)
            .HasColumnName("email")
            .HasMaxLength(Email.MaxLength)
            .HasConversion(email => email.Value, email => Email.From(email))
            .IsRequired();

        // Name
        builder
            .Property(user => user.Name)
            .HasColumnName("name")
            .HasMaxLength(Name.MaxLength)
            .HasConversion(name => name.Value, name => new Name(name))
            .IsRequired();
    }
}