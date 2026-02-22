using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Producer.Domain.Entities;

namespace Producer.Infra.Data.Map;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(256).HasColumnType("NVARCHAR");
        builder.Property(u => u.FistName).IsRequired().HasMaxLength(50).HasColumnType("NVARCHAR");
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(100).HasColumnType("NVARCHAR");
        builder.Property(u => u.Status).IsRequired();
        builder.Property(u => u.CreatedAt).IsRequired().HasColumnType("DATETIME2").HasDefaultValueSql("GETUTCDATE()");
    }
}