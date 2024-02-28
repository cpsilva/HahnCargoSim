using HahnCargoSimAutomation.EndpointDefinitions.Abstractions;

namespace HahnCargoSimAutomation.EndpointDefinitions.Configuration;

public static class EndpointDefinitionExtensions
{
    public static IServiceCollection AddEndpointDefinitions(this IServiceCollection services, params Type[] scanMarkers)
    {
        List<IEndpointDefinition> endpointDefinitions = [];

        foreach (var marker in scanMarkers)
        {
            endpointDefinitions.AddRange(marker.Assembly.ExportedTypes
                .Where(t => typeof(IEndpointDefinition).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>());
        }

        foreach (var endpointDefinition in endpointDefinitions)
        {
            endpointDefinition.DefineServices(services);
        }

        services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointDefinition>);

        return services;
    }

    public static WebApplication UseEndpointDefinitions(this WebApplication app)
    {
        var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();

        foreach (var definition in definitions)
        {
            definition.DefineEndpoints(app);
        }

        return app;
    }
}
