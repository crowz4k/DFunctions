using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableFunctions.Chaining;

public class ProgramCarFunction
{
    private readonly ILogger _logger;

    public ProgramCarFunction(ILogger logger)
    {
        _logger = logger;
    }
    
    [Function(nameof(ProgramCar))]
    public async Task<CarResponse> ProgramCar([ActivityTrigger] CarResponse car)
    {
        _logger.LogInformation("Writing some code");
        
        await Task.Delay(Constants.WorkflowStepDelay);
        
        _logger.LogInformation($"Car is programmed");

        return car;
    }
}