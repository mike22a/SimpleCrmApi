using Crm.Application;
using Crm.Domain.Models;
using Crm.Infrastructure;
using Crm.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services from other layers
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // This converts enums to strings in the JSON payload
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Add this section in Program.cs where you register other services

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 1. Fetch JWT settings from environment variables (secrets)
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? builder.Configuration["Jwt:Issuer"];
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? builder.Configuration["Jwt:Audience"];
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? builder.Configuration["Jwt:Key"];

// Console.WriteLine("--- Reading JWT Configuration ---");
// Console.WriteLine(Environment.GetEnvironmentVariable("JWT_ISSUER"));
// Console.WriteLine(Environment.GetEnvironmentVariable("JWT_AUDIENCE"));
// // We don't log the key for security reasons, but we check if it exists.
// Console.WriteLine(Environment.GetEnvironmentVariable("JWT_KEY"));
// Console.WriteLine("---------------------------------");

// 2. Validate settings. If any are missing, the app cannot start securely.
if (string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience) || string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("JWT settings are not configured. Please ensure JWT_KEY, JWT_ISSUER, and JWT_AUDIENCE environment variables are set.");
}

// 3. Use validated settings to configure Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});


// Add API layer services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular's default dev port
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularDev");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Initialize database (gracefully handle connection issues)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Test database connectivity first
        logger.LogInformation("Testing database connection...");
        await context.Database.CanConnectAsync();
        logger.LogInformation("✅ Database connection successful!");

        // Apply migrations
        logger.LogInformation("Applying database migrations...");
        await context.Database.MigrateAsync();
        logger.LogInformation("✅ Database migrations completed!");

        // Seed initial data
        logger.LogInformation("Seeding database...");
        await SeedData.Initialize(services);
        logger.LogInformation("✅ Database seeding completed!");
    }
    catch (Exception ex)
    {
        logger.LogWarning("⚠️  Database initialization failed. API will run without database connectivity.");
        logger.LogWarning("Database error: {Error}", ex.Message);
        logger.LogInformation("💡 To fix: Ensure your ConnectionStrings__DefaultConnection secret has the correct database credentials.");
    }
}

app.Run();