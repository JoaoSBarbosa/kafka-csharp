using Consumer.Infra.Data.Context;
using Consumer.Worker;
using Consumer.Worker.Config;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

var connection =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection não configurada");

builder.Services.AddDbContext<ConsumerContext>(options => options.UseSqlServer(connection));

builder.Services.AddWorkerServices(builder.Configuration);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();