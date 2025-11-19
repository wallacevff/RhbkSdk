using Refit;
using RhbkSdk.Models;
using RhbkSdk.RequestParams;

namespace RhbkSdk.Interfaces;

public partial interface IRhbkClientApi
{
    [Get("/admin/realms/{realm}/users")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<IList<UserResponse>?>> GetUsersAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [Query] Params? queryParams = null,
        CancellationToken cancellationToken = default
    );
    
    [Put("/admin/realms/{realm}/users/{userId}/groups/{groupId}")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<string?>> UserJoinGroupAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [AliasAs("userId")] Guid clientId,
        [AliasAs("groupId")] Guid groupId,
        CancellationToken cancellationToken = default
    );
    
    [Delete("/admin/realms/{realm}/users/{userId}/groups/{groupId}")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<string?>> UserLeaveGroupAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [AliasAs("userId")] Guid clientId,
        [AliasAs("groupId")] Guid groupId,
        CancellationToken cancellationToken = default
    );
    
    [Get("/admin/realms/{realm}/users/{userId}/groups")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<List<GroupResponse>?>> UserGetGroupsAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [AliasAs("userId")] Guid clientId,
        CancellationToken cancellationToken = default
    );
    
    [Post("/admin/realms/{realm}/users")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<UserResponse>> CreateUserAsync(
        [Header("Authorization")] string adminToken,
        [AliasAs("realm")]string realm,
        [Body] UserResponse userResponse,
        CancellationToken cancellationToken = default
    );
}