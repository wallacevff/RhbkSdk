using RhbkSdk.Models;
using RhbkSdk.RequestBody;
using RhbkSdk.RequestParams;
using RhbkSdk.ResponseBody;

namespace RhbkSdk.Interfaces;

public interface IClientApi
{

    public string GetLoginUrl(string realm);
    public string GetLoginProviderUrl(string realm, string clientId, string redirectUri);
    public string GetTokenUrl(string realm);
    public string GetUserInfoUrl(string realm);
    public string GetLogoutUrl(string realm);
    public string GetGroupsUrl(string realm);
    public Task<GetTokenResponseBody?> GetTokenAsync(string realm, GetTokenRequestBody body,
        CancellationToken cancellationToken = default);

    public Task<string?> CreateGroupAsync(string token, string realm, GroupCreateRequestBody body,
        CancellationToken cancellationToken = default);

    public Task<IList<GroupResponse>?> GetGroupAsync(string token, string realm, Params? queryParams = null,
        CancellationToken cancellationToken = default);

    public Task<IList<GroupResponse>?> GetSubGroupAsync(string token, string realm, Guid groupId,
        Params? queryParams = null, CancellationToken cancellationToken = default);

    public Task<IList<RoleGroupMapping>?> GetGroupClientRolesAsync(string token, string realm, Guid groupId,
        Guid clientId,
        Params? queryParams = null, CancellationToken cancellationToken = default);

    public Task<IList<UserResponse>?> GetGroupMembersAsync(string token, string realm, Guid groupId,
        Params? queryParams = null, CancellationToken cancellationToken = default);

    public Task<IList<UserResponse>?> GetGroupMembersFromSubGroupsAsync(string token, string realm, Guid groupId,
        Params? queryParams = null, CancellationToken cancellationToken = default);
    public Task<IList<UserResponse>?> DeleteGroupOrSubGroupAsync(string token, string realm, Guid groupId,
        CancellationToken cancellationToken = default);

    public Task<string?> CreateGroupClientRolesAsync(string token, string realm, Guid groupId, Guid clientId,
        IList<RoleGroupMapping> roles, CancellationToken cancellationToken = default);

    public Task<string?> DeleteGroupClientRolesAsync(string token, string realm, Guid groupId, Guid clientId,
        IList<RoleGroupMapping> roles, CancellationToken cancellationToken = default);
}