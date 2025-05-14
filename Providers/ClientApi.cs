using System.Collections;
using Refit;
using RhbkSdk.Interfaces;
using RhbkSdk.Models;
using RhbkSdk.RequestBody;
using RhbkSdk.RequestParams;
using RhbkSdk.ResponseBody;

namespace RhbkSdk.Providers;

public class ClientApi : IClientApi
{
    private readonly string _baseUrl;
    private readonly IRhbkClientApi _clientApi;


    public ClientApi(string baseUrl = "https://skh.saude.rj.gov.br")
    {
        _baseUrl = baseUrl;

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(_baseUrl),
            Timeout = TimeSpan.FromMinutes(3),
            DefaultRequestHeaders = { { "User-Agent", "Refit" } }
        };
        _clientApi = RestService.For<IRhbkClientApi>(
            httpClient, new RefitSettings
            {
                ContentSerializer = new SystemTextJsonContentSerializer()
            });
    }


    #region Url Methods

    public string GetLoginUrl(string realm)
    {
        return $"{_baseUrl}/realms/{realm}/protocol/openid-connect/auth";
    }

    public string GetLoginProviderUrl(string realm, string clientId, string redirectUri)
    {
        return $"{GetLoginUrl(realm)}?client_id={clientId}&response_type=code&redirect_uri={redirectUri}";
    }

    public string GetTokenUrl(string realm)
    {
        return $"{_baseUrl}/realms/{realm}/protocol/openid-connect/token";
    }

    public string GetUserInfoUrl(string realm)
    {
        return $"{_baseUrl}/realms/{realm}/protocol/openid-connect/userinfo";
    }

    public string GetLogoutUrl(string realm)
    {
        return $"{_baseUrl}/realms/{realm}/protocol/openid-connect/logout";
    }

    public string GetGroupsUrl(string realm)
    {
        return $"{_baseUrl}/realms/{realm}/protocol/openid-connect/groups";
    }

    #endregion


    #region Token Methods

    public async Task<GetTokenResponseBody?> GetTokenAsync(string realm, GetTokenRequestBody body,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.GetTokenAsync(realm, body, cancellationToken);
        return result.Content;
    }

    #endregion

    #region Group Methods

    public async Task<string?> CreateGroupAsync(string token, string realm, GroupCreateRequestBody body,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.CreateGroupAsync($"Bearer {token}", realm, body, cancellationToken);
        return result.Content;
    }

    public async Task<IList<GroupResponse>?> GetGroupAsync(string token, string realm, Params? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.GetAllGroupAsync($"Bearer {token}", realm, queryParams, cancellationToken);
        return result.Content;
    }

    public async Task<string?> CreateSubGroupAsync(string token, string realm, Guid groupId,
        GroupCreateRequestBody body, CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.CreateSubGroupAsync($"Bearer {token}", realm, groupId, body, cancellationToken);
        return result.Content;
    }

    public async Task<IList<GroupResponse>?> GetSubGroupAsync(string token, string realm, Guid groupId,
        Params? queryParams = null, CancellationToken cancellationToken = default)
    {
        var result =
            await _clientApi.GetAllSubgroupsAsync($"Bearer {token}", realm, groupId, queryParams, cancellationToken);
        return result.Content;
    }

    public async Task<IList<RoleGroupMapping>?> GetGroupClientRolesAsync(string token, string realm, Guid groupId,
        Guid clientId, Params? queryParams = null, CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.GetGroupClientRolesAsync($"Bearer {token}", realm, groupId, clientId, queryParams,
            cancellationToken);
        return result.Content;
    }

    public async Task<IList<UserResponse>?> GetGroupMembersAsync(string token, string realm, Guid groupId,
        Params? queryParams = null, CancellationToken cancellationToken = default)
    {
        var result =
            await _clientApi.GetGroupMembersAsync($"Bearer {token}", realm, groupId, queryParams, cancellationToken);
        return result.Content;
    }

    public async Task<IList<UserResponse>?> GetGroupMembersFromSubGroupsAsync(string token, string realm, Guid groupId,
        Params? queryParams = null, CancellationToken cancellationToken = default)
    {
        List<UserResponse> result = new List<UserResponse>();
        var groups = await GetSubGroupAsync(token, realm, groupId, cancellationToken: cancellationToken);
        if (groups != null)
        {
            foreach (var group in groups)
            {
                bool stillHasMembers = true;
                while (stillHasMembers)
                {
                    queryParams = queryParams ?? new Params() { First = 0 };
                    var users = await _clientApi.GetGroupMembersAsync($"Bearer {token}", realm, group.Id, queryParams,
                        cancellationToken);
                    queryParams.First += 1;
                    if (users.Content != null && users.Content.Count > 0)
                        result.AddRange(users.Content!);
                    else
                        stillHasMembers = false;
                }
            }
        }

        return result;
    }

    public async Task<IList<UserResponse>?> DeleteGroupOrSubGroupAsync(string token, string realm, Guid groupId,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.DeleteGroupAsync($"Bearer {token}", realm, groupId, cancellationToken);
        return result.Content;
    }

    public async Task<string?> CreateGroupClientRolesAsync(string token, string realm, Guid groupId, Guid clientId,
        IList<RoleGroupMapping> roles, CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.CreateGroupClientRolesAsync($"Bearer {token}", realm, groupId, clientId, roles,
            cancellationToken);
        return result.Content;
    }

    public async Task<string?> DeleteGroupClientRolesAsync(string token, string realm, Guid groupId, Guid clientId,
        IList<RoleGroupMapping> roles, CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.DeleteGroupClientRolesAsync($"Bearer {token}", realm, groupId, clientId, roles,
            cancellationToken);
        return result.Content;
    }

    #endregion

    #region Client Methods

    public async Task<IList<RoleResponse>?> GetClientRolesAsync(string token, string realm, Guid clientId,
        Params? queryParams = null)
    {
        var result = await _clientApi.GetClientRolesAsync($"Bearer {token}", realm, clientId, queryParams);
        return result.Content;
    }

    public async Task<string?> CreateClientRolesAsync(string token, string realm, Guid clientId,
        RoleCreateRequestBody body)
    {
        var result = await _clientApi.CreateClientRolesAsync($"Bearer {token}", realm, clientId, body);
        return result.Content;
    }

    public async Task<string?> DeleteClientRolesAsync(string token, string realm, Guid clientId,
        string roleName,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _clientApi.DeleteClientRolesAsync($"Bearer {token}", realm, clientId, roleName, cancellationToken);
        return result.Content;
    }

    public async Task<IList<ClientResponse>?> GetClientByNameAsync(string token, string realm, string clientName,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.GetClientByNameAsync($"Bearer {token}", realm, clientName, cancellationToken);
        return result.Content;
    }

    #endregion

    #region User Methods

    public async Task<IList<UserResponse>?> GetUsersAsync(string token, string realm, Params? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.GetUsersAsync($"Bearer {token}", realm, queryParams, cancellationToken);
        return result.Content;
    }

    public async Task<string?> UserJoinGroupAsync(string token, string realm, Guid userId, Guid groupId,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.UserJoinGroupAsync($"Bearer {token}", realm, userId, groupId, cancellationToken);
        return result.Content;
    }

    public async Task<string?> UserLeaveGroupAsync(string token, string realm, Guid userId, Guid groupId,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.UserLeaveGroupAsync($"Bearer {token}", realm, userId, groupId, cancellationToken);
        return result.Content;
    }

    #endregion
}