using HahnCargoSimAutomation.EndpointDefinitions.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace HahnCargoSimAutomation.EndpointDefinitions.Simulation
{
    public class SimulationEndpointDefinition : IEndpointDefinition
    {
        public void DefineEndpoints(IEndpointRouteBuilder endpoints)
        {
            var endpointGroup = endpoints.MapGroup("/api/simulation");

            endpointGroup.MapPost("start", SimulationStartHandler.StartAsync)
            .WithName(nameof(SimulationStartHandler.StartAsync))
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);

            endpointGroup.MapPost("stop", SimulationStopHandler.StopAsync)
            .WithName(nameof(SimulationStopHandler.StopAsync))
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);
        }

        public void DefineServices(IServiceCollection services)
        {
            services.AddScoped<SimulationDependencies>();
        }
    }
}
