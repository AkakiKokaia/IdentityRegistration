using System.Globalization;

namespace IdentityRegistration.API;

public static class ServiceRegistration
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services
            .AddHttpContextAccessor()
            .AddEndpointsApiExplorer()
            .AddCors();

        AddLocalizationConfiguration(services);
        return services;
    }

    public static void AddLocalizationConfiguration(this IServiceCollection services)
    {
        services.AddLocalization();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            CultureInfo[] supportedCultures = new[]
            {
                new CultureInfo("ka-GE"),
                new CultureInfo("en-US")
            };

            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
    }
}