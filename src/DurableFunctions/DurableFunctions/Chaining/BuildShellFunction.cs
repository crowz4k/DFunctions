using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableFunctions.Chaining;

public class BuildShellFunction
{
    private readonly ILogger _logger;

    public BuildShellFunction(ILogger logger)
    {
        _logger = logger;
    }
    
    [Function(nameof(BuildShell))]
    public async Task<CarResponse> BuildShell([ActivityTrigger] BuildShellInput buildInput)
    {
        _logger.LogInformation("Building shell!");
        
        await Task.Delay(Constants.WorkflowStepDelay);
        
        var result = new CarResponse
        {
            IsSolid = true,
            IsTested = false
        };
        
        _logger.LogInformation($"Shell built!");
        return result;
    }
}