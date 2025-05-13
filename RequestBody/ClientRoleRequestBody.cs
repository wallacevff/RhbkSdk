using System.Text.Json.Serialization;

namespace RhbkSdk.RequestBody;

public class ClientRoleRequestBody
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("composite")]
    public bool Composite { get; set; } = false;

    [JsonPropertyName("clientRole")]
    public bool ClientRoleFlag { get; set; } = true;

    
    public ClientRoleRequestBody(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public ClientRoleRequestBody()
    {
        Name = string.Empty;
        Description = string.Empty;
    }
}