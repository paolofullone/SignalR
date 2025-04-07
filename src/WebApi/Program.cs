using WebApi.Hubs;
using WebApi.Infrastructure;
using WebApi.Infrastructure.Repositories;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDbConnectionFactory>(sb =>
    new DbConnectionFactory(builder.Configuration.GetConnectionString("SqlServer")?.Trim()!));

builder.Services.AddSingleton<ISampleMessageService, SampleMessageService>();
builder.Services.AddTransient<ISampleMessageRepository, SampleMessageRepository>();

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<SampleMessageHub>("/samplemessage");

app.Run();