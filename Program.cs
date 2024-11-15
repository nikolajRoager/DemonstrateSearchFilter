using DemonstrateSearchFilter;
using DemonstrateSearchFilter.Models;
using DemonstrateSearchFilter.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IBoxService, BoxService>();//Inject the query service
builder.Services.AddDbContext<BoxContext>(opt =>
    opt.UseInMemoryDatabase("Boxes"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//I AM NOT SURE THIS IS THE BEST WAY OF INJECTING A DATABASE SEEDING 
//I found this on the internet and copied it
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<BoxContext>();
        context.Seed(50);
    }
    catch(Exception e)
    {
        Console.WriteLine(" While seeding boxes");
        Console.WriteLine(e.Message);
        return;
    }
}

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
