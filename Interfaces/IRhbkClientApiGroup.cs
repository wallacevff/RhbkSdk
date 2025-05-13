using Refit;
using RhbkSdk.Models;
using RhbkSdk.RequestBody;
using RhbkSdk.RequestParams;

namespace RhbkSdk.Interfaces;

public partial interface IRhbkClientApi
{
    [Post("/admin/realms/{realm}/groups")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<string>> CreateGroupAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [Body] GroupCreateRequestBody body,
        CancellationToken cancellationToken = default
    );


    [Get("/admin/realms/{realm}/groups")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<IList<GroupResponse>>> GetAllGroupAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [Query] Params? queryParams = null,
        CancellationToken cancellationToken = default
    );


    [Get("/admin/realms/{realm}/groups/{group_id}/children")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<IList<GroupResponse>>> GetAllSubgroupsAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [AliasAs("group_id")] Guid groupId,
        [Query] Params? queryParams = null,
        CancellationToken cancellationToken = default
    );

    [Post("/admin/realms/{realm}/groups/{group_id}/children")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<string>> CreateSubGroupAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [AliasAs("group_id")] Guid groupId,
        [Body] GroupCreateRequestBody body,
        CancellationToken cancellationToken = default
    );
    
    [Get("/admin/realms/{realm}/groups/{group_id}/members")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<IList<UserResponse>>> GetGroupMembersAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [AliasAs("group_id")] Guid groupId,
        [Query] Params? queryParams = null,
        CancellationToken cancellationToken = default
    );
    
    [Delete("/admin/realms/{realm}/groups/{group_id}")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<IList<UserResponse>>> DeleteGroupAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [AliasAs("group_id")] Guid groupId,
        CancellationToken cancellationToken = default
    );
    
    
    [Get("/admin/realms/{realm}/groups/{group_id}/role-mappings/clients/{client_id}")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<IList<RoleGroupMapping>>> GetGroupClientRolesAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [AliasAs("group_id")] Guid groupId,
        [AliasAs("client_id")] Guid clientId,
        [Query] Params? queryParams = null,
        CancellationToken cancellationToken = default
    );
    
    
    [Post("/admin/realms/{realm}/groups/{group_id}/role-mappings/clients/{client_id}")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<string>> CreateGroupClientRolesAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [AliasAs("group_id")] Guid groupId,
        [AliasAs("client_id")] Guid clientId,
        [Body] IList<RoleGroupMapping> roles,
        CancellationToken cancellationToken = default
    );
    
    [Delete("/admin/realms/{realm}/groups/{group_id}/role-mappings/clients/{client_id}")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<string>> DeleteGroupClientRolesAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [AliasAs("group_id")] Guid groupId,
        [AliasAs("client_id")] Guid clientId,
        [Body] IList<RoleGroupMapping> roles,
        CancellationToken cancellationToken = default
    );
}