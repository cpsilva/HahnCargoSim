using HahnCargoSim.Abstractions.Simulation;

namespace HahnCargoSimAutomation.EndpointDefinitions.Simulation;

public record SimulationDependencies
(
    ISimulation simulation,
    ILogger<SimulationEndpointDefinition> Logger
);
