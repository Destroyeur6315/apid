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
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .SetIsOriginAllowed(_ => true) // Autorise toutes les origines
            .AllowAnyMethod()              // Autorise toutes les méthodes HTTP
            .AllowAnyHeader();             // Autorise tous les headers
    });
});


var app = builder.Build();
app.UseCors("AllowAll");

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