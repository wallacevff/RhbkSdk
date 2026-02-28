using System.Collections;
using System.Text.Json;
using Refit;
using RhbkSdk.Exceptions;
using RhbkSdk.Interfaces;
using RhbkSdk.Models;
using RhbkSdk.RequestBody;
using RhbkSdk.RequestParams;
using RhbkSdk.ResponseBody;

namespace RhbkSdk.Providers;

public class RhbkClient : IRhbkClient
{
    private readonly string _baseUrl;
    private readonly IRhbkClientApi _clientApi;


    public RhbkClient(string baseUrl = "https://skh.saude.rj.gov.br")
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

    public DefaultResponseBody<string> GetLoginUrl(string realm)
    {
        return new DefaultResponseBody<string>()
        {
            StatusCode = 200,
            Data = $"{_baseUrl}/realms/{realm}/protocol/openid-connect/auth"
        };
    }

    public DefaultResponseBody<string> GetLoginProviderUrl(string realm, string clientId, string redirectUri,
        string scope = "openid email profile")
    {
        return new DefaultResponseBody<string>()
        {
            StatusCode = 200,
            Data = $"{GetLoginUrl(realm).Data}?client_id={clientId}&response_type=code&redirect_uri={redirectUri}&scope={scope}"
        };
    }

    public DefaultResponseBody<string> GetTokenUrl(string realm)
    {
        return new DefaultResponseBody<string>()
        {
            StatusCode = 200,
            Data = $"{_baseUrl}/realms/{realm}/protocol/openid-connect/token"
        };
    }

    public DefaultResponseBody<string> GetUserInfoUrl(string realm)
    {
        return new DefaultResponseBody<string>()
        {
            StatusCode = 200,
            Data = $"{_baseUrl}/realms/{realm}/protocol/openid-connect/userinfo"
        };
    }

    public DefaultResponseBody<string> GetLogoutUrl(string realm, string token, string url)
    {
        return new DefaultResponseBody<string>()
        {
            StatusCode = 200,
            Data = $"{_baseUrl}/realms/{realm}/protocol/openid-connect/logout?id_token_hint={token}&post_logout_redirect_uri={url}"
        };
    }

    public DefaultResponseBody<string> GetGroupsUrl(string realm)
    {
        return new DefaultResponseBody<string>()
        {
            StatusCode = 200,
            Data = $"{_baseUrl}/realms/{realm}/protocol/openid-connect/groups"
        };
    }

    #endregion


    #region Token Methods

    public async Task<DefaultResponseBody<GetTokenResponseBody?>> GetTokenAsync(string realm, GetTokenRequestBody body,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.GetTokenAsync(realm, body, cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<string>> LogoffAsync(string realm, LogoffRequestBody requestBody)
    {
        var body = requestBody.ToDictionary();
        var result = await _clientApi.LogoffAsync(realm, body);
        CaptureException(result);
        return GenResponse(result);
    }

    #endregion

    #region Group Methods

    public async Task<DefaultResponseBody<string?>> CreateGroupAsync(string token, string realm,
        GroupCreateRequestBody body,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.CreateGroupAsync($"Bearer {token}", realm, body, cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }
    
    public async Task<DefaultResponseBody<string?>> EditGroupAsync(string token, string realm, Guid groupId,
        GroupUpdateRequestBody body, CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.EditGroupAsync($"Bearer {token}", realm, groupId, body, cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<IList<GroupResponse>?>> GetGroupAsync(string token, string realm,
        Params? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.GetAllGroupAsync($"Bearer {token}", realm, queryParams, cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<string?>> CreateSubGroupAsync(string token, string realm, Guid groupId,
        GroupCreateRequestBody body, CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.CreateSubGroupAsync($"Bearer {token}", realm, groupId, body, cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<IList<GroupResponse>?>> GetSubGroupAsync(string token, string realm,
        Guid groupId,
        Params? queryParams = null, CancellationToken cancellationToken = default)
    {
        var result =
            await _clientApi.GetAllSubgroupsAsync($"Bearer {token}", realm, groupId, queryParams, cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<IList<RoleGroupMapping>?>> GetGroupClientRolesAsync(string token,
        string realm, Guid groupId,
        Guid clientId, Params? queryParams = null, CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.GetGroupClientRolesAsync($"Bearer {token}", realm, groupId, clientId, queryParams,
            cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<IList<UserResponse>?>> GetGroupMembersAsync(string token, string realm,
        Guid groupId,
        Params? queryParams = null, CancellationToken cancellationToken = default)
    {
        var result =
            await _clientApi.GetGroupMembersAsync($"Bearer {token}", realm, groupId, queryParams, cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<IList<UserResponse>>> GetGroupMembersFromSubGroupsAsync(string token,
        string realm, Guid groupId,
        Params? queryParams = null, CancellationToken cancellationToken = default)
    {
        List<UserResponse> result = new List<UserResponse>();
        var groups = await GetSubGroupAsync(token, realm, groupId, cancellationToken: cancellationToken);
        if (groups.Data != null)
        {
            foreach (var group in groups.Data)
            {
                bool stillHasMembers = true;
                while (stillHasMembers)
                {
                    queryParams = queryParams ?? new Params() { First = 0 };
                    var users = await _clientApi.GetGroupMembersAsync($"Bearer {token}", realm, group.Id, queryParams,
                        cancellationToken);
                    CaptureException(users);
                    queryParams.First += 1;
                    if (users.Content != null && users.Content.Count > 0)
                        result.AddRange(users.Content!);
                    else
                        stillHasMembers = false;
                }
            }
        }

        return new DefaultResponseBody<IList<UserResponse>>()
        {
            StatusCode = 200,
            Data = result
        };
    }

    public async Task<DefaultResponseBody<IList<UserResponse>?>> DeleteGroupOrSubGroupAsync(string token, string realm,
        Guid groupId,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.DeleteGroupAsync($"Bearer {token}", realm, groupId, cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<string?>> CreateGroupClientRolesAsync(string token, string realm,
        Guid groupId, Guid clientId,
        IList<RoleGroupMapping> roles, CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.CreateGroupClientRolesAsync($"Bearer {token}", realm, groupId, clientId, roles,
            cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<string?>> DeleteGroupClientRolesAsync(string token, string realm,
        Guid groupId, Guid clientId,
        IList<RoleGroupMapping> roles, CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.DeleteGroupClientRolesAsync($"Bearer {token}", realm, groupId, clientId, roles,
            cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<string?>> SetGroupEnabledAsync(
        string token,
        string realm,
        Guid groupId,
        Guid clientId,
        bool enabled,
        string backupAttributeName = "disabled_roles_backup",
        string disabledAttributeName = "disabled",
        CancellationToken cancellationToken = default)
    {
        var group = await FindGroupByIdAsync(token, realm, groupId, cancellationToken);
        var attributes = group.Attributes is null
            ? new Dictionary<string, IList<string>>(StringComparer.OrdinalIgnoreCase)
            : new Dictionary<string, IList<string>>(group.Attributes, StringComparer.OrdinalIgnoreCase);

        var currentRolesResponse = await GetGroupClientRolesAsync(
            token,
            realm,
            groupId,
            clientId,
            cancellationToken: cancellationToken
        );
        var currentRoles = (currentRolesResponse.Data ?? [])
            .Where(r => !string.IsNullOrWhiteSpace(r.Name))
            .ToDictionary(r => r.Name, StringComparer.OrdinalIgnoreCase);

        if (!enabled)
        {
            if (!attributes.ContainsKey(backupAttributeName))
            {
                var roleNamesToBackup = currentRoles.Keys
                    .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                    .ToList();
                attributes[backupAttributeName] = [JsonSerializer.Serialize(roleNamesToBackup)];
            }

            if (currentRoles.Count > 0)
            {
                await DeleteGroupClientRolesAsync(
                    token,
                    realm,
                    groupId,
                    clientId,
                    currentRoles.Values.ToList(),
                    cancellationToken
                );
            }

            attributes[disabledAttributeName] = ["true"];
        }
        else
        {
            attributes[disabledAttributeName] = ["false"];

            var backupRoleNames = ReadBackupRoleNames(attributes, backupAttributeName);
            if (backupRoleNames.Count > 0)
            {
                var allClientRolesResponse = await GetClientRolesAsync(token, realm, clientId);
                var allClientRoles = (allClientRolesResponse.Data ?? [])
                    .Where(r => !string.IsNullOrWhiteSpace(r.Name))
                    .ToDictionary(r => r.Name, StringComparer.OrdinalIgnoreCase);

                var rolesToRestore = backupRoleNames
                    .Where(allClientRoles.ContainsKey)
                    .Where(roleName => !currentRoles.ContainsKey(roleName))
                    .Select(roleName => new RoleGroupMapping
                    {
                        Id = allClientRoles[roleName].Id,
                        Name = allClientRoles[roleName].Name
                    })
                    .ToList();

                if (rolesToRestore.Count > 0)
                {
                    await CreateGroupClientRolesAsync(
                        token,
                        realm,
                        groupId,
                        clientId,
                        rolesToRestore,
                        cancellationToken
                    );
                }
            }

            attributes.Remove(backupAttributeName);
        }

        return await EditGroupAsync(
            token,
            realm,
            groupId,
            new GroupUpdateRequestBody
            {
                Name = group.Name ?? string.Empty,
                Attributes = attributes
            },
            cancellationToken
        );
    }

    #endregion

    #region Client Methods

    public async Task<DefaultResponseBody<IList<RoleResponse>?>> GetClientRolesAsync(string token, string realm,
        Guid clientId,
        Params? queryParams = null)
    {
        var result = await _clientApi.GetClientRolesAsync($"Bearer {token}", realm, clientId, queryParams);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<string?>> CreateClientRolesAsync(string token, string realm, Guid clientId,
        RoleCreateRequestBody body)
    {
        var result = await _clientApi.CreateClientRolesAsync($"Bearer {token}", realm, clientId, body);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<string?>> DeleteClientRolesAsync(string token, string realm, Guid clientId,
        string roleName,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _clientApi.DeleteClientRolesAsync($"Bearer {token}", realm, clientId, roleName, cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<IList<ClientResponse>?>> GetClientByNameAsync(string token, string realm,
        string clientName,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.GetClientByNameAsync($"Bearer {token}", realm, clientName, cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    #endregion

    #region User Methods

    public async Task<DefaultResponseBody<IList<UserResponse>?>> GetUsersAsync(string token, string realm,
        Params? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.GetUsersAsync($"Bearer {token}", realm, queryParams, cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<string?>> UserJoinGroupAsync(string token, string realm, Guid userId,
        Guid groupId,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.UserJoinGroupAsync($"Bearer {token}", realm, userId, groupId, cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<string?>> UserLeaveGroupAsync(string token, string realm, Guid userId,
        Guid groupId,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.UserLeaveGroupAsync($"Bearer {token}", realm, userId, groupId, cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }
    
    public async Task<DefaultResponseBody<List<GroupResponse>?>> UserGetGroupsAsync(string token, string realm, Guid userId,
        CancellationToken cancellationToken = default)
    {
        var result = await _clientApi.UserGetGroupsAsync($"Bearer {token}", realm, userId, cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    public async Task<DefaultResponseBody<List<GroupResponse>>> UserGetGroupsByParentGroupAsync(string token, string realm, Guid userId,
        Guid groupId,
        CancellationToken cancellationToken = default)
    {
        var result = await UserGetGroupsAsync(token, realm, userId, cancellationToken);
        var finteredResult = result!.Data!.Where(x => x.ParentId == groupId).ToList();
        return new DefaultResponseBody<List<GroupResponse>>
        {
            Data = finteredResult,
            StatusCode = 200
        };
    }
    
    public async Task<DefaultResponseBody<bool>> UserIsInGroupAsync(string token, string realm, Guid userId,
        Guid groupId,
        CancellationToken cancellationToken = default)
    {
        var result = await UserGetGroupsByParentGroupAsync(token, realm, userId, groupId, cancellationToken);
        var isInGroup = result.Data!.Any(x => x.Id == groupId);
        return new DefaultResponseBody<bool>
        {
            Data = isInGroup,
            StatusCode = 200
        };
    }

    public async Task<DefaultResponseBody<UserResponse>> CreateUserAsync(string adminToken, string realm, UserResponse userInfo, CancellationToken cancellationToken)
    {
        var result = await _clientApi.CreateUserAsync($"Bearer {adminToken}", realm, userInfo, cancellationToken);
        CaptureException(result);
        return GenResponse(result);
    }

    #endregion

    private void CaptureException<T>(ApiResponse<T> ex)
    {
        if (!ex.IsSuccessStatusCode && ex.Error != null)
        {
            var messageFromRequest = ex.Error.Content ?? string.Empty;
            throw new RhbkSdkDefaultException(
                (int)ex.StatusCode,
                $"{ex.Error?.Message}\r\n{messageFromRequest}");
        }
    }

    private DefaultResponseBody<T> GenResponse<T>(ApiResponse<T> response)
    {
        return new DefaultResponseBody<T>()
        {
            StatusCode = (int)response.StatusCode,
            Data = response.Content
        };
    }

    private async Task<GroupResponse> FindGroupByIdAsync(
        string token,
        string realm,
        Guid groupId,
        CancellationToken cancellationToken)
    {
        var rootsResponse = await GetGroupAsync(token, realm, cancellationToken: cancellationToken);
        var queue = new Queue<GroupResponse>(rootsResponse.Data ?? []);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.Id == groupId)
            {
                return current;
            }

            var childrenResponse = await GetSubGroupAsync(
                token,
                realm,
                current.Id,
                cancellationToken: cancellationToken
            );
            if (childrenResponse.Data is null)
            {
                continue;
            }

            foreach (var child in childrenResponse.Data)
            {
                queue.Enqueue(child);
            }
        }

        throw new RhbkSdkDefaultException(404, $"Group '{groupId}' not found.");
    }

    private static HashSet<string> ReadBackupRoleNames(
        IDictionary<string, IList<string>> attributes,
        string backupAttributeName)
    {
        if (!attributes.TryGetValue(backupAttributeName, out var backupRaw) ||
            backupRaw is null ||
            backupRaw.Count == 0 ||
            string.IsNullOrWhiteSpace(backupRaw[0]))
        {
            return [];
        }

        try
        {
            var names = JsonSerializer.Deserialize<List<string>>(backupRaw[0]) ?? [];
            return names
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
        }
        catch (JsonException)
        {
            return [];
        }
    }
}
