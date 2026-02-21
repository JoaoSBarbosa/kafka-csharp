using Consumer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Infra.Data.Context;

public class ConsumerContext(DbContextOptions<ConsumerContext> options) : DbContext(options)
{
    public DbSet<UserProcessingResult> UserProcessingResults => Set<UserProcessingResult>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConsumerContext).Assembly);
    }
}