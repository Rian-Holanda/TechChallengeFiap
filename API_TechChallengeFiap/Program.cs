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
