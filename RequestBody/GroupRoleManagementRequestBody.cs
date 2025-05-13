using System.Text.Json.Serialization;

namespace RhbkSdk.RequestBody;

public class GroupRoleManagementRequestBody
{
    [JsonPropertyName("id")] public Guid Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }

    public GroupRoleManagementRequestBody(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public GroupRoleManagementRequestBody()
    {
        Name = string.Empty;
    }
}