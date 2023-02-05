using amhWebAPI.DTO;
using amhWebAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//ADD MAPERS
builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<Cerveza, CervezaDTO>();
    config.CreateMap<CervezaDTO, Cerveza>();

    config.CreateMap<Ciudad, CiudadDTO>();
    config.CreateMap<CiudadDTO, Ciudad>();

    config.CreateMap<Estilo, EstiloDTO>();
    config.CreateMap<EstiloDTO, Estilo>();

    config.CreateMap<Marca, MarcaDTO>();
    config.CreateMap<MarcaDTO, Marca>();

    config.CreateMap<Pais, PaisDTO>();
    config.CreateMap<PaisDTO, Pais>();

    config.CreateMap<Usuario, UsuarioDTO>();
    config.CreateMap<UsuarioDTO, Usuario>();

    config.CreateMap<Expediente, ExpedienteDTO>();
    config.CreateMap<ExpedienteDTO, Expediente>();


}, typeof(Program));

//ADD CORS
builder.Services.AddCors(options => options.AddPolicy("AllowWebApp",
    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//JWT
builder.Configuration.AddJsonFile("appsettings.json");
var secretKey = builder.Configuration.GetSection("settings").GetSection("secretkey").ToString();
var keyBytes = Encoding.UTF8.GetBytes(secretKey);


builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ClockSkew = TimeSpan.FromMinutes(3600)
    };
});
//

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddDbContext<AmhWebDbContext>(x => x.UseSqlServer("Data Source=SQL5097.site4now.net;Initial Catalog=db_a934ba_mayibeercollection;User Id=db_a934ba_mayibeercollection_admin;Password=Caslacapo1908**"));
builder.Services.AddDbContext<AmhWebDbContext>(x => x.UseSqlServer("Server=localhost; Database=AmhWebDatabase; Trusted_Connection=True; TrustServerCertificate=True"));

//SERILOG
builder.Host.UseSerilog();

Serilog.Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog(((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)));


var app = builder.Build();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//USE CORS
app.UseCors(policy => policy.AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed(origin => true)
                            .AllowCredentials());

//app.UseCors("AllowWebApp");

app.UseHttpsRedirection();

//JWT
app.UseAuthentication();
//

app.UseAuthorization();

app.MapControllers();

app.Run();
