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
builder.Services.AddDbContext<RS3PriceCheckerDBContext>(options =>
{
    options.UseSqlServer("name=ConnectionStrings:RS3PriceCheckerAlias");
    //options.UseSqlServer("name=ConnectionStrings:RS3PriceCheckerAlias",
    //    sqlServerOptionsAction: sqlOptions => {
    //        sqlOptions.EnableRetryOnFailure();
    //    });
});

builder.Services.AddTransient<IItemDetailRepository, ItemDetailRepository>();

builder.Services.AddScoped<IGrandExchangeService, GrandExchangeService>();
builder.Services.AddTransient<IItemDetailService, ItemDetailService>();
builder.Services.AddTransient<ILoadDataService, LoadDataService>();

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
