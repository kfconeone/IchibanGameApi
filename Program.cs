using IChibanGameServer;
using IChibanGameServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//71E62E83006CBFA179F3FE71253169E01C65B5352AE41DDA7D7C921BC8F5A37A
//71E62E83006CBFA179F3FE71253169E01C65B5352AE41DDA7D7C921BC8F5A37A
//var result = HashManager.GenerateSHA256String("A2,B6,C13,D24,E55mykey");

builder.Services.AddControllers();
// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
               builder =>
               {
                   builder.WithOrigins("http://localhost:4200")
                       //builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddEntityFrameworkNpgsql().AddDbContext<IchibanGameContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("MyDbConnection"))
    );

builder.Services.AddScoped<GameManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
