namespace RhbkSdk.Models;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EmailVerified { get; set; }
    public long CreatedTimestamp { get; set; }
    public bool Enabled { get; set; }
    public bool Totp { get; set; }
    public List<string> DisableableCredentialTypes { get; set; } = new();
    public List<string> RequiredActions { get; set; } = new();
    public int NotBefore { get; set; }
    public Access Access { get; set; } = new Access();
    public Dictionary<string, IList<string>> Attributes { get; set; } = new Dictionary<string, IList<string>>();
}