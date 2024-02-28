namespace HahnCargoSimAutomation.EndpointDefinitions.Abstractions;

public interface IEndpointDefinition
{
    void DefineServices(IServiceCollection services);
    void DefineEndpoints(IEndpointRouteBuilder endpoints);
}
