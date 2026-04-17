using Microsoft.EntityFrameworkCore;
using River_Pollution_Survey.Models.DBModels;
using River_Pollution_Survey.RPS_Contracts;
using River_Pollution_Survey.RPS_Repository;


var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<RiverDBContext>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

// For Entity Framework

builder.Services.AddDbContext<RiverDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();