using System.Text.Json.Serialization;

namespace RhbkSdk.Models;

public class GroupResponse
{
    [JsonPropertyName("id")] public Guid Id { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("path")] public string? Path { get; set; }
    [JsonPropertyName("parentId")] public Guid? ParentId { get; set; }
    [JsonPropertyName("subGroupCount")] public int SubGroupCount { get; set; }
    [JsonPropertyName("subGroups")] public IList<GroupResponse> SubGroups { get; set; } = [];
    
    [JsonPropertyName("attributes")]
    public Dictionary<string, IList<string>>? Attributes { get; set; } = new Dictionary<string, IList<string>>();
    
    [JsonPropertyName("realmRoles")] public IList<string> RealmRoles { get; set; } = new List<string>();
    
    [JsonPropertyName("clientRoles")]
    public Dictionary<string, IList<string>> ClientRoles { get; set; } = new Dictionary<string, IList<string>>();

    [JsonPropertyName("access")] public Access Access { get; set; } = new Access();
}