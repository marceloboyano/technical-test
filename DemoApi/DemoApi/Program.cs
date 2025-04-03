
using DemoApi.Config;
using DemoApi.Repositories;
using DemoApi.Repositories.Interfaces;
using DemoApi.Services.Implementations;
using DemoApi.Services.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;

  
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "chinook.db");
builder.Services.AddScoped<IDbConnection>(_ =>
{
var connection = new SqliteConnection($"Data Source={dbPath}");
connection.Open();
return connection;
});
builder.Services.AddScoped<IArtistsRepository, ArtistsRepository>();
builder.Services.AddScoped<IGenresRepository, GenresRepository>();
builder.Services.AddScoped<ITracksRepository, TracksRepository>();

builder.Services.AddScoped<IArtistsService, ArtistsService>();                    
builder.Services.AddScoped<IGenresService, GenresService>();                    
builder.Services.AddScoped<ITracksService, TracksService>();  
        
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(opt =>
{
opt.AddDefaultPolicy(o => o
    .AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod());
});
var app = builder.Build();
app.UseCors();
if (app.Environment.IsDevelopment())
{
app.UseSwagger(c =>
{
    c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
});
app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
   
