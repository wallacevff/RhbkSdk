using Microsoft.Extensions.Configuration;

namespace RhbkSdk.Configurations;

public class RhbkConfiguration
{
    public static string ConfigurationSection = "RhbkConfiguration";

    [ConfigurationKeyName("Realm")] public string Realm { get; set; } = string.Empty;
    [ConfigurationKeyName("ClientId")] public string ClientId { get; set; } = string.Empty;
    [ConfigurationKeyName("ClientSecret")] public string ClientSecret { get; set; } = string.Empty;
    [ConfigurationKeyName("KeycloakBaseUrl")] public string KeycloakBaseUrl { get; set; } = string.Empty;
    [ConfigurationKeyName("RedirectUri")] public string RedirectUri { get; set; } = string.Empty;
    [ConfigurationKeyName("AdminUsername")] public string AdminUsername { get; set; } = string.Empty;
    [ConfigurationKeyName("AdminPassword")] public string AdminPassword { get; set; } = string.Empty;
}