using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddDbContext<DBContext>(
    optionsBuilder => optionsBuilder.UseMySQL(
        builder.Configuration.GetConnectionString("DatabaseProductExercise")
    )
);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.AllowAnyMethod() // Autorise toutes les méthodes HTTP (GET, POST, etc.)
              .AllowAnyHeader(); // Autorise tous les headers (Authorization, Content-Type, etc.)
    });
});


var app = builder.Build();
app.UseCors("AllowAngularApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();