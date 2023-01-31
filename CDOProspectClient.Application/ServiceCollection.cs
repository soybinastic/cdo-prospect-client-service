using CDOProspectClient.Application.Repositories.AdminRepo;
using CDOProspectClient.Application.Repositories.AgentRepo;
using CDOProspectClient.Application.Repositories.AppointmentRepo;
using CDOProspectClient.Application.Repositories.EvaluationRepo;
using CDOProspectClient.Application.Repositories.ProfileRepo;
using CDOProspectClient.Application.Repositories.PropertyRepo;
using CDOProspectClient.Application.Repositories.RequirementRepo;
using Microsoft.Extensions.DependencyInjection;

namespace CDOProspectClient.Application;

public static class ServiceCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ProfileAbstractRepository, ProfileRepository>();
        services.AddScoped<AgentAbstractRepository, AgentRepository>();
        services.AddScoped<PropertyAbstractRepository, PropertyRepository>();
        services.AddScoped<RequirementAbstractRepository, RequirementRepository>();
        services.AddScoped<EvaluationAbstractRepository, EvaluationRepository>();
        services.AddScoped<AppointmentAbstractRepository, AppointmentRepository>();
        services.AddScoped<AdminAbstractRepository, AdminRepository>();
        return services;
    }
}