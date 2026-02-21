using Consumer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consumer.Infra.Data.Mappings;

public class UserProcessingResultMap : IEntityTypeConfiguration<UserProcessingResult>
{
    public void Configure(EntityTypeBuilder<UserProcessingResult> builder)
    {
        builder.ToTable("UserProcessingResult");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .HasColumnType("varchar(256)")
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ProcessedAt)
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.Success)
            .IsRequired();

        builder.Property(x => x.ErrorMessage)
            .HasColumnType("varchar(max)")
            .IsRequired(false);
    }
}