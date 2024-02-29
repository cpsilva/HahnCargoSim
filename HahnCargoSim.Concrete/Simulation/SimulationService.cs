using CSharpFunctionalExtensions;
using HahnCargoSim.Abstractions.Simulation;

namespace HahnCargoSim.Concrete.Simulation;

public class SimulationService : ISimulation
{
    public async Task<Result<bool>> Start()
    {
        var t = await Task.FromResult(true);
        return Result.Success(t);
    }

    public async Task<Result<bool>> Stop()
    {
        var t = await Task.FromResult(true);
        return Result.Success(t);
    }
}
