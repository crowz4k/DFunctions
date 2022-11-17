using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableFunctions.Chaining;

public class ChainingFunction
{
    private readonly ILogger _logger;
    public ChainingFunction(ILogger logger)
    {
        _logger = logger;
    }
    
    [Function("ChainingFunction_HttpStart")]
    public async Task<HttpResponseData> HttpStart(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]
        HttpRequestData req,
        [DurableClient] DurableClientContext starter)
    {
        var instanceId = await starter.Client.ScheduleNewOrchestrationInstanceAsync(nameof(ChainingFunction));
        
        return starter.CreateCheckStatusResponse(req, instanceId);
    }

    [Function(nameof(ChainingFunction))]
    public async Task RunOrchestrator([OrchestrationTrigger] TaskOrchestrationContext context)
    {
        var toolsTask = context.CallActivityAsync<ToolsResponse>(nameof(FetchToolsFunction.FetchTools));
        var partsTask = context.CallActivityAsync<PartsResponse>(nameof(FetchPartsFunction.FetchParts));

        await Task.WhenAll(toolsTask, partsTask);

        var buildInput = new BuildShellInput { Tools = toolsTask.Result, Parts = partsTask.Result };
        
        var car = await context.CallActivityAsync<CarResponse>(nameof(BuildShellFunction.BuildShell), buildInput);
        
        car = await context.CallActivityAsync<CarResponse>(nameof(ProgramCarFunction.ProgramCar), car);
        
        await context.CallActivityAsync<CarResponse>(nameof(TestCarFunction.TestCar), car);
        
        _logger.LogInformation("Car is complete.");
    }
}