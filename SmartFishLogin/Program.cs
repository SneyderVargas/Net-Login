using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartFishLogin.Core.Helpers;
using SmartFishLogin.Core.Interfaces;
using SmartFishLogin.Helpers;
using SmartFishLogin.Infra;
using SmartFishLogin.Infra.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddLog4Net();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DefaultDbContext>();
builder.Services.AddScoped<ILogin, LoginRepo>();
builder.Services.AddScoped<IRole, RolesRepo>();
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtConfiguration"));
builder.Services.Configure<EncrypConfiguration>(builder.Configuration.GetSection("EncrypConfiguration"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfiguration:Key"])),
        //IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false

    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError($"Error de autenticación: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        // Otros eventos...
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Servicio de Auth",
        Version = "v1"
    });
    c.CustomSchemaIds(c => c.FullName);
});

builder.Services.AddMvc();

var app = builder.Build();

app.UseCors(option =>
{
    //option.WithOrigins("https://localhost:44378");
    option.AllowAnyOrigin();
    option.AllowAnyMethod();
    option.AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
