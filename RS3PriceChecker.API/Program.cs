using Microsoft.EntityFrameworkCore;
using RS3PriceChecker.Database;
using RS3PriceChecker.Repository;
using RS3PriceChecker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext with connection string from appsettings.json
builder.Services.AddDbContext<RS3DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Rs3DbContext")));

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
