using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableFunctions.Chaining;

public class TestCarFunction
{
    private readonly ILogger _logger;

    public TestCarFunction(ILogger logger)
    {
        _logger = logger;
    }
    
    [Function(nameof(TestCar))]
    public async Task<CarResponse> TestCar([ActivityTrigger] CarResponse car)
    {
        _logger.LogInformation("Testing the car ");
        
        await Task.Delay(Constants.WorkflowStepDelay);
        
        car.IsTested = true;
        _logger.LogInformation("Car tested!");
        return car;
    }
}