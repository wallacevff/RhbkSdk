using System.Text.Json.Serialization;

namespace RhbkSdk.RequestBody;

public class GroupUpdateRequestBody
{
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("attributes")]
    public Dictionary<string, IList<string>>? Attributes { get; set; } = new Dictionary<string, IList<string>>();
}
