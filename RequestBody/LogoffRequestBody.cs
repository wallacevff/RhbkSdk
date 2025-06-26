namespace RhbkSdk.RequestBody;

public class LogoffRequestBody
{
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;

    public Dictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string>
        {
            { "client_id", ClientId },
            { "client_secret", ClientSecret },
            { "refresh_token", RefreshToken }
        };
    }
}