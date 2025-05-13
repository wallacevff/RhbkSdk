using System.Text.Json.Serialization;

namespace RhbkSdk.RequestBody;

public class GroupCreateRequestBody
{
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
}