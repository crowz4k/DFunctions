using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableFunctions.Chaining;

public class FetchToolsFunction
{
    private readonly ILogger _logger;

    public FetchToolsFunction(ILogger logger)
    {
        _logger = logger;
    }
    
    [Function(nameof(FetchTools))]
    public async Task<ToolsResponse> FetchTools([ActivityTrigger] TaskActivityContext context)
    {
        _logger.LogInformation("Fetching tools");
        await Task.Delay(Constants.WorkflowStepDelay);
        var tools = new ToolsResponse
        {
            Tools = new List<string>
            {
                "Screw Driver",
                "Pliers",
                "Hammer"
            },
            
        };

        _logger.LogInformation($"Found my {string.Join(", ", tools.Tools)}");
        return tools;
    }
}