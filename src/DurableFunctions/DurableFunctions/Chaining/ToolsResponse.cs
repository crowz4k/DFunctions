using System.Collections.Generic;

namespace Microsoft.DurableTask;

public class ToolsResponse
{
    public ToolsResponse()
    {
        Tools = new List<string>();
    }

    public List<string> Tools { get; set; }
}