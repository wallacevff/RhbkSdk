using Refit;

namespace RhbkSdk.RequestBody;

public class GetTokenRequestBody
{
    [AliasAs("grant_type")] public string GrantType { get; set; } = "authorization_code";
    [AliasAs("code")] public string Code { get; set; } = string.Empty;
    [AliasAs("client_id")] public string ClientId { get; set; } = "";
    [AliasAs("client_secret")] public string ClientSecret { get; set; } = "";
    [AliasAs("redirect_uri")] public string RedirectUri { get; set; } = "";
    [AliasAs("username")] public string? UserName { get; set; } = "";
    [AliasAs("password")] public string? Password { get; set; } = "";
    [AliasAs("refresh_token")] public string? RefreshToken { get; set; } = "";
    [AliasAs("scope")] public string? Scope { get; set; } = "openid email profile";

    public GetTokenRequestBody()
    {
        
    }

    public GetTokenRequestBody(
        string clientId,
        string clientSecret,
        string redirectUri,
        string code,
        string grantType = GrantTypeOption.AuthorizationCode
        )
    {
        GrantType = grantType;
        ClientId = clientId;
        ClientSecret = clientSecret;
        RedirectUri = redirectUri;
        Code = code;
    }
    
    public GetTokenRequestBody(
        string clientId,
        string clientSecret,
        string userName,
        string password
        )
    {
        GrantType = GrantTypeOption.Password;
        ClientId = clientId;
        ClientSecret = clientSecret;
        UserName = userName;
        Password = password;
    }
    
    
    public GetTokenRequestBody(
        string clientId,
        string clientSecret,
        string refreshToken
    )
    {
        GrantType = GrantTypeOption.RefreshToken;
        ClientId = clientId;
        ClientSecret = clientSecret;
        RefreshToken = refreshToken;
    }
    
}

