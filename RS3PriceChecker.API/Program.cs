using CustomLogger;
using Microsoft.EntityFrameworkCore;
using RS3PriceChecker.Database;
using RS3PriceChecker.Repository;
using RS3PriceChecker.Services;
using RS3PriceChecker.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//initialize configuration
var config = builder.Configuration;

// Use a DbContextFactory
builder.Services.AddDbContextFactory<RS3DbContext>(options =>
{
    // get configuration
    options.UseSqlServer(config["Rs3DbContext"]);
});


// Add CustomLogger factory
builder.Services.AddSingleton<ICustomLogger>(sp =>
{
    var filePath = config["CustomLogging:FilePath"] ?? string.Empty;
    var fileName = config["CustomLogging:FileName"] ?? string.Empty;

    // Read configured log level and try to parse it into the enum.
    // If parsing fails or value is missing, default to Info.
    var tempLevel = config["CustomLogging:LogLevel"];
    if (string.IsNullOrWhiteSpace(tempLevel))
    {
        tempLevel = "Info";
    }

    if (!Enum.TryParse<CustomLogger.LogLevel>(tempLevel, ignoreCase: true, out var logLevel))
    {
        logLevel = CustomLogger.LogLevel.Info;
    }

    return CustomLogger.LoggerFactory.Create(filePath, fileName, logLevel);
});

builder.Services.AddTransient<ItemDetailRepository, ItemDetailRepository>();

builder.Services.AddScoped<GrandExchangeService, GrandExchangeService>();
builder.Services.AddTransient<ItemDetailService, ItemDetailService>();
builder.Services.AddTransient<LoadDataService, LoadDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
