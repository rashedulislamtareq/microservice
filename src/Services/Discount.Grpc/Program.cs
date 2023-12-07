using Discount.Grpc.Migrate_Database;
using Discount.Grpc.Repositories;
using Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

var configuration = builder.Configuration;
MigrateDatabase.MigrateInitialDatabase(configuration);

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
