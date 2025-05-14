namespace RhbkSdk.Models;

public class RoleCreateRequestBody
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
public class RoleResponse : RoleCreateRequestBody
{
    public Guid Id { get; set; }
    public bool Composite { get; set; }
    public bool ClientRole { get; set; }
    public Guid ContainerId { get; set; }
}



public class RoleGroupMapping
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}