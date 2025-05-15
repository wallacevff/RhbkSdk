RhbkSdk
========

O RhbkSdk é um SDK para .NET 8 que facilita a integração com a API RHBK — uma solução de gerenciamento de identidade baseada em OAuth2/OpenID Connect, com funcionalidades como gerenciamento de usuários, grupos e permissões.

--------------------------------------------------------------------------------
📦 Injeção de Dependência
--------------------------------------------------------------------------------

Adicione o SDK ao seu WebApplicationBuilder:

    using RhbkSdk.Extensions;

    var builder = WebApplication.CreateBuilder(args);
    builder.AddRhbkClient("https://api.seurhbk.com", ServiceLifetime.Scoped);

O método AddRhbkClient registra o serviço IClientApi para uso via injeção de dependência.

--------------------------------------------------------------------------------
📘 Manual de Uso
--------------------------------------------------------------------------------

O serviço IClientApi encapsula chamadas HTTP para a API RHBK. Você pode injetá-lo normalmente:

    public class MinhaClasse
    {
        private readonly IClientApi _clientApi;

        public MinhaClasse(IClientApi clientApi)
        {
            _clientApi = clientApi;
        }

        public async Task<IList<UserResponse>?> ObterUsuariosAsync()
        {
            return await _clientApi.GetUsersAsync("seu_token", "seu_realm");
        }
    }

--------------------------------------------------------------------------------
🧪 Exemplo Prático
--------------------------------------------------------------------------------

    var tokenResponse = await _clientApi.GetTokenAsync("meu_realm", new GetTokenRequestBody
    {
        ClientId = "app",
        GrantType = GrantTypeOption.Password,
        Username = "usuario",
        Password = "senha"
    });

    var grupos = await _clientApi.GetGroupAsync(tokenResponse?.AccessToken!, "meu_realm");

--------------------------------------------------------------------------------
📂 Models Disponíveis
--------------------------------------------------------------------------------

- Access
- ClientResponse
- GroupResponse
- UserResponse
- RoleResponse
- RoleGroupMapping
- GetTokenResponseBody
- GroupCreateRequestBody
- ClientRoleRequestBody
- GroupRoleManagementRequestBody

--------------------------------------------------------------------------------
📌 Métodos Suportados
--------------------------------------------------------------------------------

🔐 Token & Autenticação
- GetTokenAsync

👥 Grupos
- CreateGroupAsync
- GetGroupAsync
- GetSubGroupAsync
- DeleteGroupOrSubGroupAsync
- CreateSubGroupAsync
- GetGroupMembersAsync
- GetGroupMembersFromSubGroupsAsync

🔑 Papéis (Roles)
- GetClientRolesAsync
- CreateClientRolesAsync
- DeleteClientRolesAsync
- GetGroupClientRolesAsync
- CreateGroupClientRolesAsync
- DeleteGroupClientRolesAsync

🧑‍💼 Usuários
- GetUsersAsync
- UserJoinGroupAsync
- UserLeaveGroupAsync

🧩 Clientes
- GetClientByNameAsync

--------------------------------------------------------------------------------
🔗 Dependências
--------------------------------------------------------------------------------

- Refit (v8.0.0) — cliente HTTP declarativo
- Microsoft.Extensions.DependencyInjection.Abstractions (v8.0.2)

--------------------------------------------------------------------------------
💡 Sugestões Futuras
--------------------------------------------------------------------------------

- Adicionar suporte a ILogger para logs
- Adicionar documentação de endpoints em .http
- Automatizar versionamento e publicação com GitHub Actions
- Melhorar tratamento de erros de API e resposta

--------------------------------------------------------------------------------
📝 Licença
--------------------------------------------------------------------------------

Distribuído sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.

--------------------------------------------------------------------------------
👨‍💻 Autor
--------------------------------------------------------------------------------

Desenvolvido por Wallace Vidal
GitHub: https://github.com/wallacevff
