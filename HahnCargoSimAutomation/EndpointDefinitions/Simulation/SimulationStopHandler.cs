using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace HahnCargoSimAutomation.EndpointDefinitions.Simulation
{
    public class SimulationStopHandler
    {
        public static async Task<Result<bool>> StopAsync(
        [FromServices] SimulationDependencies dependencies,
        HttpRequest request)
        {

            var result = await dependencies.simulation.Stop();

            return result.IsSuccess switch
            {
                true => Result.Success(result.Value),
                _ => Result.Failure<bool>("Unexpected result")
            };
        }
    }
}
