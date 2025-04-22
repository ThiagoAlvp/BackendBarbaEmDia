using BackendBarbaEmDia.Classes;
using BackendBarbaEmDia.Domain.Interfaces.Repositories;
using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Services;
using BackendBarbaEmDia.Infraestructure.Data.Contexts;
using BackendBarbaEmDia.Infraestructure.Data.Repositories;
using BackendBarbaEmDia.Services;
using BackendBarbaEmDia.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure AppSettings to read appsettings.json
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Add Authentication with JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
    var key = Encoding.ASCII.GetBytes(appSettings?.SecretCliente!);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddDbContext<BarbeariaContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("Barbearia") ?? ""));

#region Services
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ITokenService, TokenService>();
#endregion

#region Repositories
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();

app.MapControllers();

app.Run();