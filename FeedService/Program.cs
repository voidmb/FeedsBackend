using Microsoft.EntityFrameworkCore;
using FeedService;
using UserManagementApi.Data;
using FeedService.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using FeedService.Infrastructure.Services;
using FeedService.Core.Interfaces;
using FeedService.Core.Repositories;
using FeedService.Core.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ExternalApiService>();

builder.Services.AddDbContext<FeedContext>(options =>
    options.UseSqlite("Data Source=Feeds.db"));

// Registrar AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
// Registrar el servicio de mapeador
builder.Services.AddScoped<IMappingService, MappingService>();
// Registrar servicios de repositorios
builder.Services.AddScoped<IFeedRepository, FeedRepository>();
builder.Services.AddScoped<IFeedService, FeedService.Infrastructure.Services.FeedService>();

// Registrar servicio externo
builder.Services.AddScoped<IExternalApiService, ExternalApiService>();

builder.Services.AddControllers();

// Resgistra el servicio de autenticación
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdmin", policy =>
        policy.RequireRole("Administrador"));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Resource API", Version = "v1" });

    // Configuración de seguridad para Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter your token in the format **Bearer &lt;token&gt;**"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

//// Aplica las migraciones al iniciar la aplicación
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<FeedContext>();
//    dbContext.Database.Migrate(); // Aplica todas las migraciones pendientes
//    SeedData.Initialize(dbContext); // Inicializa los datos
//}

// Ejecuta migraciones y datos de prueba solo en entornos no de prueba Seagregó para que Docker aplique estas configuraciones
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<FeedContext>();
        dbContext.Database.Migrate(); // Aplica todas las migraciones pendientes
        SeedData.Initialize(dbContext); // Inicializa los datos
    }
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FeedContext>();
    SeedData.Initialize(dbContext);
}

app.UseHttpsRedirection();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }

