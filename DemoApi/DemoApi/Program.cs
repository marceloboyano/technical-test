using DemoApi.Config;
using DemoApi.Repositories;
using DemoApi.Repositories.Interfaces;
using DemoApi.Services.Implementations;
using DemoApi.Services.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Reflection;


  
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Demo API",
        Version = "v1",
        Description = "API documentation for DemoApi",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Marcelo Boyanosky",
            Email = "marceloboyanosky@gmail.com"
        }
    });

   
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

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
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API V1");      
        c.DocumentTitle = "Demo API Documentation";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
   
