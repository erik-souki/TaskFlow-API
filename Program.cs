using Microsoft.EntityFrameworkCore;
using TaskFlow_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var corsOrigins = builder.Configuration.GetSection("CorsOrigins").Get<string[]>();

// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins(corsOrigins)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi



builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer(); // Required for Swagger
builder.Services.AddSwaggerGen(); // Add Swagger support

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger(); // Enable Swagger
    app.UseSwaggerUI(); // Enable Swagger UI

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
