using Refit;
using RhbkSdk.Models;
using RhbkSdk.RequestBody;
using RhbkSdk.RequestParams;
using RhbkSdk.ResponseBody;

namespace RhbkSdk.Interfaces;

public partial interface IRhbkClientApi
{
    [Get("/admin/realms/{realm}/clients/{clientId}/roles")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<IList<RoleResponse>>> GetClientRolesAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [AliasAs("clientId")] Guid clientId,
        [Query] Params? queryParams = null,
        CancellationToken cancellationToken = default
    );
    
    [Post("/admin/realms/{realm}/clients/{clientId}/roles")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<string>> CreateClientRolesAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [AliasAs("clientId")] Guid clientId,
        [Body] RoleCreateRequestBody body,
        CancellationToken cancellationToken = default
    );
    
    [Delete("/admin/realms/{realm}/clients/{clientId}/roles/{roleName}")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<string>> DeleteClientRolesAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [AliasAs("clientId")] Guid clientId,
        [AliasAs("roleName")] string roleName,
        CancellationToken cancellationToken = default
    );
    
    [Get("/admin/realms/{realm}/clients")]
    [Headers("Content-Type; application/json")]
    public Task<ApiResponse<IList<ClientResponse>>> GetClientByNameAsync(
        [Header("Authorization")] string token,
        [AliasAs("realm")] string realm,
        [Query("clientId")] string clientName,
        CancellationToken cancellationToken = default
    );
}