using RhbkSdk.Models;
using RhbkSdk.RequestBody;
using RhbkSdk.RequestParams;
using RhbkSdk.ResponseBody;

namespace RhbkSdk.Interfaces;

public interface IRhbkClient
{
    public DefaultResponseBody<string> GetLoginUrl(string realm);
    public DefaultResponseBody<string> GetLoginProviderUrl(string realm, string clientId, string redirectUri);
    public DefaultResponseBody<string> GetTokenUrl(string realm);
    public DefaultResponseBody<string> GetUserInfoUrl(string realm);
    public DefaultResponseBody<string> GetLogoutUrl(string realm);
    public DefaultResponseBody<string> GetGroupsUrl(string realm);

    public Task<DefaultResponseBody<GetTokenResponseBody?>> GetTokenAsync(string realm, GetTokenRequestBody body,
        CancellationToken cancellationToken = default);

    public Task<DefaultResponseBody<string?>> CreateGroupAsync(string token, string realm, GroupCreateRequestBody body,
        CancellationToken cancellationToken = default);

    public Task<DefaultResponseBody<string?>> CreateSubGroupAsync(string token, string realm, Guid groupId,
        GroupCreateRequestBody body, CancellationToken cancellationToken = default);
    
    public Task<DefaultResponseBody<IList<GroupResponse>?>> GetGroupAsync(string token, string realm, Params? queryParams = null,
        CancellationToken cancellationToken = default);

    public Task<DefaultResponseBody<IList<GroupResponse>?>> GetSubGroupAsync(string token, string realm, Guid groupId,
        Params? queryParams = null, CancellationToken cancellationToken = default);

    public Task<DefaultResponseBody<IList<RoleGroupMapping>?>> GetGroupClientRolesAsync(string token, string realm, Guid groupId,
        Guid clientId,
        Params? queryParams = null, CancellationToken cancellationToken = default);

    public Task<DefaultResponseBody<IList<UserResponse>?>> GetGroupMembersAsync(string token, string realm, Guid groupId,
        Params? queryParams = null, CancellationToken cancellationToken = default);

    public Task<DefaultResponseBody<IList<UserResponse>>> GetGroupMembersFromSubGroupsAsync(string token, string realm, Guid groupId,
        Params? queryParams = null, CancellationToken cancellationToken = default);

    public Task<DefaultResponseBody<IList<UserResponse>?>> DeleteGroupOrSubGroupAsync(string token, string realm, Guid groupId,
        CancellationToken cancellationToken = default);

    public Task<DefaultResponseBody<string?>> CreateGroupClientRolesAsync(string token, string realm, Guid groupId, Guid clientId,
        IList<RoleGroupMapping> roles, CancellationToken cancellationToken = default);

    public Task<DefaultResponseBody<string?>> DeleteGroupClientRolesAsync(string token, string realm, Guid groupId, Guid clientId,
        IList<RoleGroupMapping> roles, CancellationToken cancellationToken = default);


    public Task<DefaultResponseBody<IList<RoleResponse>?>> GetClientRolesAsync(string token, string realm, Guid clientId,
        Params? queryParams = null);

    public Task<DefaultResponseBody<string?>> CreateClientRolesAsync(string token, string realm, Guid clientId, RoleCreateRequestBody body);

    public Task<DefaultResponseBody<string?>> DeleteClientRolesAsync(string token, string realm, Guid clientId, string roleName,
        CancellationToken cancellationToken = default);

    public Task<DefaultResponseBody<IList<ClientResponse>?>> GetClientByNameAsync(string token, string realm, string clientName,
        CancellationToken cancellationToken = default);

    public Task<DefaultResponseBody<IList<UserResponse>?>> GetUsersAsync(string token, string realm, Params? queryParams = null,
        CancellationToken cancellationToken = default);

    public Task<DefaultResponseBody<string?>> UserJoinGroupAsync(string token, string realm, Guid userId, Guid groupId,
        CancellationToken cancellationToken = default);

    public Task<DefaultResponseBody<string?>> UserLeaveGroupAsync(string token, string realm, Guid userId, Guid groupId,
        CancellationToken cancellationToken = default);

    public Task<DefaultResponseBody<string>> LogoffAsync(string realm, LogoffRequestBody requestBody);
}