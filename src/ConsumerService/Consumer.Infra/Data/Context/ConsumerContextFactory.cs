using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Consumer.Infra.Data.Context;

public class ConsumerContextFactory : IDesignTimeDbContextFactory<ConsumerContext>
{
    public ConsumerContext CreateDbContext(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{env}.json", optional: true)
            .AddUserSecrets<ConsumerContext>(optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connection = config.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connection))
            throw new InvalidOperationException("String de conexção 'DefaultConnection' não foi configurado");
        var builder = new DbContextOptionsBuilder<ConsumerContext>();
        builder.UseSqlServer(connection);
        return new ConsumerContext(builder.Options);
    }
}