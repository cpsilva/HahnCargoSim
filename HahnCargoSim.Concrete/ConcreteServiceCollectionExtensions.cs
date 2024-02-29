using HahnCargoSim.Abstractions.Simulation;
using HahnCargoSim.Concrete.Simulation;
using Microsoft.Extensions.DependencyInjection;

namespace HahnCargoSim.Concrete;

public static class ConcreteServiceCollectionExtensions
{
    public static IServiceCollection AddConcrete(this IServiceCollection services)
    {
        return services
            .AddScoped<ISimulation, SimulationService>();
    }
}
