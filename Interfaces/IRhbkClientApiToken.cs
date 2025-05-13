using Refit;
using RhbkSdk.RequestBody;
using RhbkSdk.ResponseBody;

namespace RhbkSdk.Interfaces;

public partial interface IRhbkClientApi
{
    [Post("/realms/{realm}/protocol/openid-connect/token")]
    [Headers("Content-Type; application/x-www-form-urlencoded")]
    public Task<ApiResponse<GetTokenResponseBody?>> GetTokenAsync(
        [AliasAs("realm")] string realm,
        [Body(BodySerializationMethod.UrlEncoded)]
        GetTokenRequestBody body,
        CancellationToken cancellationToken = default
    );
}