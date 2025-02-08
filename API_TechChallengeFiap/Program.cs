using Business_TechChallengeFiap.Consulta.Domain;
using Business_TechChallengeFiap.Consulta.Interface;
using DataAccess_TechChallengeFiap.Consultas.Commands;
using DataAccess_TechChallengeFiap.Consultas.Interface;
using DataAccess_TechChallengeFiap.Consultas.Queries;
using DataAccess_TechChallengeFiap.Medico.Command;
using DataAccess_TechChallengeFiap.Medico.Interfaces;
using DataAccess_TechChallengeFiap.Medico.Queries;
using DataAccess_TechChallengeFiap.Paciente.Command;
using DataAccess_TechChallengeFiap.Paciente.Interfaces;
using DataAccess_TechChallengeFiap.Paciente.Queries;
using Infrastructure_FiapTechChallenge;
using Infrastructure_FiapTechChallenge.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();

// Add services to the container.
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("ConnectionString"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("ConnectionString"));
});

builder.Services.AddScoped<IConsultaQueries, ConsultaQueries>();
builder.Services.AddScoped<IConsultaCommand, ConsultaCommand>();
builder.Services.AddScoped<IPacienteCommand, PacienteCommand>();
builder.Services.AddScoped<IPacienteQueries, PacienteQueries>();
builder.Services.AddScoped<IMedicoCommand, MedicoCommand>();
builder.Services.AddScoped<IMedicoQueries, MedicoQueries>();
builder.Services.AddScoped<IMedicoCommand,   MedicoCommand>();
builder.Services.AddScoped<IConsultaBusiness, ConsultaBusiness>();

builder.Services.AddControllers();
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

//app.MapIdentityApi<IdentityUser>();

app.MapSwagger().RequireAuthorization();

app.Run();
