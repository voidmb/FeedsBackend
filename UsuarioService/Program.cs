using Microsoft.EntityFrameworkCore;
using UserManagementApi.Data;
using UsuarioService.Core.Contexts;
using static UsuarioService.Infrastructure.Interfaces.IMappingService;
using UsuarioService.Infrastructure.Services;
using UsuarioService.Core.Interfaces;
using UsuarioService.Core.Repositories;
using UsuarioService.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Agrega el servicio de Autenticación
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

// Configura el DbContext basado en el entorno
builder.Services.AddDbContext<UsuarioDBContext>(options =>
    options.UseSqlite("Data Source=Users.db"));

//builder.Services.AddDbContext<UsuarioDBContext>(options =>
//    options.UseSqlite("Data Source=Usuarios.db"));

// Registrar AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
// Registrar el servicio de mapeador
builder.Services.AddScoped<IMappingService, MappingService>();
// Registrar servicios de repositorios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService.Infrastructure.Services.UsuarioService>();

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<UsuarioDBContext>();
//    dbContext.Database.Migrate(); // Aplica todas las migraciones pendientes
//    SeedData.Initialize(dbContext); // Inicializa los datos
//}

// Ejecuta migraciones y datos de prueba solo en entornos no de prueba Seagregó para que Docker aplique estas configuraciones
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<UsuarioDBContext>();
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

app.UseHttpsRedirection();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }

