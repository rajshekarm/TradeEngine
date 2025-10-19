using TradeEngine.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddInfrastructure();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowTradeEngineUI", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000") // React dev servers
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.UseAuthorization();

app.UseCors("AllowTradeEngineUI");

app.MapControllers();

app.Run();
