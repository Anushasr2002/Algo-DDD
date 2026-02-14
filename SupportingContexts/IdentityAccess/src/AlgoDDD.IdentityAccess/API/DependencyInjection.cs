using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MediatR;
using System.Reflection;
using AlgoDDD.IdentityAccess.Domain.Repositories;
using AlgoDDD.IdentityAccess.Infrastructure.Services;
using AlgoDDD.IdentityAccess.Infrastructure.Persistence;

namespace AlgoDDD.IdentityAccess.API;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityAccess(this IServiceCollection services, JwtSettings jwtSettings)
    {
        // Add MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        // Add Repositories (using in-memory for development)
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();
        
        // Add Services
        services.AddSingleton<IJwtService>(sp => 
            new JwtService(jwtSettings.Secret, jwtSettings.Issuer, jwtSettings.Audience, jwtSettings.ExpiryInMinutes));
        
        // Add Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
        
        // Add Authorization policies
        services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
            options.AddPolicy("RequireTrader", policy => policy.RequireRole("Trader"));
            options.AddPolicy("RequireRiskManager", policy => policy.RequireRole("RiskManager"));
        });
        
        return services;
    }
}

public class JwtSettings
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpiryInMinutes { get; set; } = 60;
}
