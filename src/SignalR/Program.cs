using SignalR.Hubs;
using SignalR.Infrastructure.DbFactories;
using SignalR.Infrastructure.Repositories;
using SignalR.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDbConnectionFactory>(sb =>
    new DbConnectionFactory(builder.Configuration.GetConnectionString("SqlServer")?.Trim()!));

builder.Services.AddScoped<ISampleMessageService, SampleMessageService>();
builder.Services.AddScoped<ISampleMessageRepository, SampleMessageRepository>();

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<SampleMessageHub>("/samplemessage");

app.Run();