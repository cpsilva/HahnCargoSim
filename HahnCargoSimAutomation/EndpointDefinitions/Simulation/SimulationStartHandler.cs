using CSharpFunctionalExtensions;
using HahnCargoSimAutomation.EndpointDefinitions.Simulation.Models;
using Microsoft.AspNetCore.Mvc;

namespace HahnCargoSimAutomation.EndpointDefinitions.Simulation
{
    public class SimulationStartHandler
    {
        public static async Task<Result<bool>> StartAsync(
        [FromServices] SimulationDependencies dependencies,
        HttpRequest request)
        {
          
            var result = await dependencies.simulation.Start();

            return result.IsSuccess switch
            {
                true => Result.Success(result.Value),
                _ => Result.Failure<bool>("Unexpected result")
            };
        }
    }
}
