using System.Collections.Generic;

namespace Microsoft.DurableTask;

public class PartsResponse
{
    public PartsResponse()
    {
        CarParts = new List<string>();
    }

    public List<string> CarParts { get; set; }
}