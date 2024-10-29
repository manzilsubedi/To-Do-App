using Microsoft.EntityFrameworkCore;
using To_Do_API.Data.DBContext;
using To_Do_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ToDoContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoDatabase")));
builder.Services.AddControllers();

builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddScoped<FetchToDoService>();
builder.Services.AddScoped<ToDoService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
