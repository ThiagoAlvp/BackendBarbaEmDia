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
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
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
})
.AddJwtBearer("Cliente", options =>
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
})
.AddJwtBearer("Administrador", options =>
{
    var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
    var key = Encoding.ASCII.GetBytes(appSettings?.SecretAdministrador!);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Insira o token JWT no campo abaixo. Exemplo: Bearer {seu token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
});

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    connectionString = builder.Configuration.GetConnectionString("Barbearia");
}

builder.Services.AddDbContext<BarbeariaContext>(options =>
   options.UseMySql(
       connectionString ?? "",
       new MySqlServerVersion(new Version(8, 0, 32)),
       mySqlOptions => mySqlOptions.EnableRetryOnFailure()));

#region Services
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IBarbeiroService, BarbeiroService>();
builder.Services.AddScoped<IServicoService, ServicoService>();
builder.Services.AddScoped<IAdministradorService, AdministradorService>();
builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();
builder.Services.AddScoped<ITravamentoService, TravamentoService>();
builder.Services.AddScoped<IBarbeiroServicoService, BarbeiroServicoService>();
builder.Services.AddScoped<IParametrizacaoHorarioFuncionamentoService, ParametrizacaoHorarioFuncionamentoService>();
builder.Services.AddScoped<IEstatisticasService, EstatisticasService>();
#endregion

#region Repositories
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IBarbeiroRepository, BarbeiroRepository>();
builder.Services.AddScoped<IBarbeiroServicoRepository, BarbeiroServicoRepository>();
builder.Services.AddScoped<IAdministradorRepository, AdministradorRepository>();
builder.Services.AddScoped<IAgendamentosRepository, AgendamentosRepository>();
builder.Services.AddScoped<IServicoRepository, ServicoRepository>();
builder.Services.AddScoped<ITravamentosRepository, TravamentosRepository>();
builder.Services.AddScoped<IParametrizacaoHorarioFuncionamentoRepository, ParametrizacaoHorarioFuncionamentoRepository>();
#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowEspecificOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowEspecificOrigin");
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();

app.MapControllers();

app.Run();