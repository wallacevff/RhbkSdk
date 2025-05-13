namespace RhbkSdk.Models;

public class RoleResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Composite { get; set; }
    public bool ClientRole { get; set; }
    public Guid ContainerId { get; set; }
}

public class RoleGroupMapping
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}