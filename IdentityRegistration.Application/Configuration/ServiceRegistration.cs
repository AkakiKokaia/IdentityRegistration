using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityRegistration.Application.Configuration.Behaviors;
using IdentityRegistration.Domain.Interfaces;
using IdentityRegistration.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IdentityRegistration.Application.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        AddFluentValidation(services);
        AddCustomServices(services);

        return services;
    }

    public static void AddFluentValidation(this IServiceCollection services)
    {

        services
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();

        Assembly assembly = Assembly.Load("IdentityRegistration.Application");

        services.AddValidatorsFromAssembly(assembly);
    }

    public static void AddCustomServices(this IServiceCollection services)
    {
        services
            .AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssemblies(
                     AppDomain.CurrentDomain.GetAssemblies()));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOtpRepository, OtpRepository>();

    }
}