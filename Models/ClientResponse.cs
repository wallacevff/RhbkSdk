﻿using System.Text.Json.Serialization;

namespace RhbkSdk.Models;

public class ClientResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("clientId")]
    public string ClientId { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("rootUrl")]
    public string RootUrl { get; set; } = string.Empty;

    [JsonPropertyName("adminUrl")]
    public string AdminUrl { get; set; } = string.Empty;

    [JsonPropertyName("baseUrl")]
    public string BaseUrl { get; set; } = string.Empty;

    [JsonPropertyName("surrogateAuthRequired")]
    public bool SurrogateAuthRequired { get; set; }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    [JsonPropertyName("alwaysDisplayInConsole")]
    public bool AlwaysDisplayInConsole { get; set; }

    [JsonPropertyName("clientAuthenticatorType")]
    public string ClientAuthenticatorType { get; set; } = string.Empty;

    [JsonPropertyName("secret")]
    public string Secret { get; set; } = string.Empty;

    [JsonPropertyName("redirectUris")]
    public IEnumerable<string> RedirectUris { get; set; } = new List<string>();

    [JsonPropertyName("webOrigins")]
    public IEnumerable<string> WebOrigins { get; set; } = new List<string>();

    [JsonPropertyName("notBefore")]
    public int NotBefore { get; set; }

    [JsonPropertyName("bearerOnly")]
    public bool BearerOnly { get; set; }

    [JsonPropertyName("consentRequired")]
    public bool ConsentRequired { get; set; }

    [JsonPropertyName("standardFlowEnabled")]
    public bool StandardFlowEnabled { get; set; }

    [JsonPropertyName("implicitFlowEnabled")]
    public bool ImplicitFlowEnabled { get; set; }

    [JsonPropertyName("directAccessGrantsEnabled")]
    public bool DirectAccessGrantsEnabled { get; set; }

    [JsonPropertyName("serviceAccountsEnabled")]
    public bool ServiceAccountsEnabled { get; set; }

    [JsonPropertyName("publicClient")]
    public bool PublicClient { get; set; }

    [JsonPropertyName("frontchannelLogout")]
    public bool FrontchannelLogout { get; set; }

    [JsonPropertyName("protocol")]
    public string Protocol { get; set; } = string.Empty;

    [JsonPropertyName("attributes")]
    public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

    [JsonPropertyName("authenticationFlowBindingOverrides")]
    public Dictionary<string, string> AuthenticationFlowBindingOverrides { get; set; } = new Dictionary<string, string>();

    [JsonPropertyName("fullScopeAllowed")]
    public bool FullScopeAllowed { get; set; }

    [JsonPropertyName("nodeReRegistrationTimeout")]
    public int NodeReRegistrationTimeout { get; set; }

    [JsonPropertyName("protocolMappers")]
    public IEnumerable<ProtocolMapper> ProtocolMappers { get; set; } = new List<ProtocolMapper>();

    [JsonPropertyName("defaultClientScopes")]
    public IEnumerable<string> DefaultClientScopes { get; set; } = new List<string>();

    [JsonPropertyName("optionalClientScopes")]
    public IEnumerable<string> OptionalClientScopes { get; set; } = new List<string>();

    [JsonPropertyName("access")]
    public Access Access { get; set; } = new Access();
    

    public class ProtocolMapper
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("protocol")]
        public string Protocol { get; set; } = string.Empty;

        [JsonPropertyName("protocolMapper")]
        public string ProtocolMapperName { get; set; } = string.Empty;

        [JsonPropertyName("consentRequired")]
        public bool ConsentRequired { get; set; }

        [JsonPropertyName("config")]
        public Dictionary<string, string> Config { get; set; } = new Dictionary<string, string>();
    }
    
}