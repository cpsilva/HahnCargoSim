using CSharpFunctionalExtensions;

namespace HahnCargoSim.Abstractions.Simulation;

public interface ISimulation
{
    Task<Result<bool>> Start();
    Task<Result<bool>> Stop();
}
