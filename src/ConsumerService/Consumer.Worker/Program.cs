using Consumer.Application.Handlers;
using Consumer.Application.Ports.Messaging;
using Consumer.Application.Ports.Persistence;
using Consumer.Infra.Data.Context;
using Consumer.Infra.Kafka.Consumer;
using Consumer.Infra.Persistence;
using Consumer.Worker;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

var connection =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection não configurada");

builder.Services.AddDbContext<ConsumerContext>(options =>
    options.UseSqlServer(connection));

// Infra
builder.Services.AddScoped<IProcessingResultRepository, ProcessingResultRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<UserRegisteredEventHandler>();

// Kafka
builder.Services.AddScoped<IEventConsumer, KafkaConsumerWorker>();

// Worker
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();