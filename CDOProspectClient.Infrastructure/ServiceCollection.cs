
using System.Text;
using CDOProspectClient.Infrastructure.Data;
using CDOProspectClient.Infrastructure.Helpers.Jwt;
using CDOProspectClient.Infrastructure.Helpers.Upload;
using CDOProspectClient.Infrastructure.Repositories;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CDOProspectClient.Infrastructure;

public static class ServiceCollection
{

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        
        services.AddMySQLDatabase(configuration)
            .AddAppIdentity()
            .AddAuth(configuration)
            .AddCloudinary(configuration);

        return services;
    }

    public static IServiceCollection AddMySQLDatabase(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {   
        services.AddDbContext<ApplicationDbContext>(
            option => option.UseMySql(
                configuration.GetConnectionString("DefaultConnection"), 
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
            )
        );

        return services;
    }
    public static IServiceCollection AddAuth(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // JwtService is a helper class that generate security token or JWT bearer
        services.AddSingleton<IJwtService, JwtService>();

        services.AddAuthentication(
            option => 
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option => 
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:SecretKey"]!))
                };
            });
        return services;
    }

    public static IServiceCollection AddAppIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(
            option => 
            {
                option.Password.RequiredLength = 5;
                option.Password.RequireLowercase = true;
                option.Password.RequireUppercase = true;
                option.Password.RequireNonAlphanumeric = true;
            }
        ).AddEntityFrameworkStores<ApplicationDbContext>();
        return services;
    }

    public static IServiceCollection AddCloudinary(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddSingleton<Cloudinary>(
            sp => {
                var cloudinary = new Cloudinary(
                    new Account(
                        configuration["CloudinarySettings:Name"],
                        configuration["CloudinarySettings:Key"],
                        configuration["CloudinarySettings:Secret"]
                    )
                );
                cloudinary.Api.Secure = true;
                return cloudinary;
            }
        );

        services.AddSingleton<IUploadService, UploadService>();
        return services;
    }

}