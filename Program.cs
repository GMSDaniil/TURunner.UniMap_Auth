using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UserManagementAPI.Data;
using UserManagementAPI.Extensions;
using UserManagementAPI.Repositories;
using UserManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "User Management API",
        Version = "v1"
    });
});


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<UsersService>();

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("JwtOptions"));

JwtOptions jwtOptions = builder.Configuration
    .GetSection("JwtOptions")
    .Get<JwtOptions>()!;

builder.Services.AddApiAuthentication(Options.Create(jwtOptions));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API v1");
        c.RoutePrefix = string.Empty; 
    });


}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
