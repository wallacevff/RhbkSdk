using Refit;

namespace RhbkSdk.RequestParams;

public class Params
{
    [Query("search")] public string?  Search { get; set; }
    [Query("max")] public int?  Max { get; set; }
    [Query("first")] public int?  First { get; set; }
    
}