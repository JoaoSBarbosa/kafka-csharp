using Microsoft.EntityFrameworkCore;
using Producer.Domain.Entities;

namespace Producer.Infra.Context;

public class KafkaPlaygroundContext(DbContextOptions<KafkaPlaygroundContext> options) : DbContext(options)
{
    public DbSet<User> User => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KafkaPlaygroundContext).Assembly);
    }
}