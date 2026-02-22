using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Producer.Infra.Data.Context;

public class KafkaPlaygroundContextFactory : IDesignTimeDbContextFactory<KafkaPlaygroundContext>
{
    public KafkaPlaygroundContext CreateDbContext(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{env}.json", optional: true)
            .AddUserSecrets<KafkaPlaygroundContext>(optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connection = config.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connection))
            throw new InvalidOperationException("String de conexão não encontrada ou não configurada");
        var optionsBuilder = new DbContextOptionsBuilder<KafkaPlaygroundContext>();
        optionsBuilder.UseSqlServer(connection);
        return new KafkaPlaygroundContext(optionsBuilder.Options);
    }
}