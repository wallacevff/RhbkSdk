using System.Text.Json.Serialization;

namespace RhbkSdk.Models;

public class Access
{
    [JsonPropertyName("view")] public bool View { get; set; }

    [JsonPropertyName("viewMembers")] public bool ViewMembers { get; set; }

    [JsonPropertyName("manageMembers")] public bool ManageMembers { get; set; }

    [JsonPropertyName("manage")] public bool Manage { get; set; }

    [JsonPropertyName("manageMembership")] public bool ManageMembership { get; set; }

    public Access()
    {
    }

    public Access(bool view, bool viewMembers, bool manageMembers, bool manage, bool manageMembership)
    {
        View = view;
        ViewMembers = viewMembers;
        ManageMembers = manageMembers;
        Manage = manage;
        ManageMembership = manageMembership;
    }
}