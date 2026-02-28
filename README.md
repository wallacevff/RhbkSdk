
# RhbkSdk

O **RhbkSdk** é um SDK para .NET 8 que facilita a integração com a API RHBK (Keycloak), oferecendo uma camada de abstração moderna com suporte a grupos, papéis, autenticação e gerenciamento de usuários.

---

## 📦 Injeção de Dependência

Registre o SDK no seu `Program.cs`:

```csharp
using RhbkSdk.Extensions;

builder.AddRhbkClient("https://seu.keycloak.url", ServiceLifetime.Scoped);
```

---

## ⚙️ Configuração por appsettings

Recomendado usar a classe `RhbkConfiguration`:

```json
"RhbkConfiguration": {
  "Realm": "meu-realm",
  "ClientId": "client-id",
  "ClientSecret": "segredo",
  "KeycloakBaseUrl": "https://keycloak.meuservidor.com",
  "RedirectUri": "https://meusite/callback"
}
```

E carregar no startup:

```csharp
builder.Services.Configure<RhbkConfiguration>(
    builder.Configuration.GetSection(RhbkConfiguration.ConfigurationSection));
```

---

## 📘 Como Usar

Você injeta a interface `IRhbkClient` diretamente:

```csharp
public class MeuServico
{
    private readonly IRhbkClient _rhbkClient;

    public MeuServico(IRhbkClient rhbkClient)
    {
        _rhbkClient = rhbkClient;
    }

    public async Task<IList<UserResponse>> BuscarUsuarios()
    {
        var token = await _rhbkClient.GetTokenAsync("realm", new GetTokenRequestBody { ... });
        var usuarios = await _rhbkClient.GetUsersAsync(token.Data?.AccessToken!, "realm");

        return usuarios.Data ?? [];
    }
}
```

---

## 🧪 Retorno Padrão

Todas as chamadas assíncronas retornam:

```csharp
DefaultResponseBody<T>
```

Esse objeto inclui:

- `StatusCode`: código HTTP da operação
- `Data`: resultado da chamada, que pode ser nulo

---

## 📂 Models disponíveis

- `Access`
- `ClientResponse`
- `GroupResponse`
- `UserResponse`
- `RoleResponse`
- `RoleGroupMapping`
- `GetTokenResponseBody`
- `GroupCreateRequestBody`
- `GroupUpdateRequestBody`
- `ClientRoleRequestBody`
- `GroupRoleManagementRequestBody`
- `RhbkConfiguration`

---

## ✅ Funcionalidades

### 🔐 Autenticação
- `GetTokenAsync`
- `GetLoginUrl`
- `GetLogoutUrl`
- `GetLoginProviderUrl`

### 👥 Grupos
- Criar grupos e subgrupos
- Editar grupo
- Habilitar/desabilitar grupo com backup/restore de roles
- Buscar todos os grupos ou subgrupos
- Buscar membros
- Adicionar/remover papéis de grupos
- Deletar grupos

Exemplo de edição de grupo:

```csharp
await _rhbkClient.EditGroupAsync(
    token: accessToken,
    realm: "meu-realm",
    groupId: groupId,
    body: new GroupUpdateRequestBody
    {
        Name = "novo-nome-do-grupo",
        Attributes = new Dictionary<string, IList<string>>
        {
            ["costCenter"] = new List<string> { "TI-01" },
            ["system"] = new List<string> { "rhbk-sdk" }
        }
    },
    cancellationToken: cancellationToken);
```

Exemplo de desabilitar/habilitar grupo preservando roles:

```csharp
await _rhbkClient.SetGroupEnabledAsync(
    token: accessToken,
    realm: "meu-realm",
    groupId: groupId,
    clientId: clientId,
    enabled: false,
    cancellationToken: cancellationToken);

await _rhbkClient.SetGroupEnabledAsync(
    token: accessToken,
    realm: "meu-realm",
    groupId: groupId,
    clientId: clientId,
    enabled: true,
    cancellationToken: cancellationToken);
```

### 🧑‍💼 Usuários
- Listar usuários
- Adicionar usuário a grupo
- Remover usuário de grupo

### 🧩 Clientes
- Buscar cliente por nome
- Gerenciar papéis do cliente

---

## 🔗 Dependências

- [Refit](https://www.nuget.org/packages/Refit) — cliente HTTP declarativo
- [Microsoft.Extensions.DependencyInjection.Abstractions](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection.Abstractions)
- [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration)

---

## 🚀 CI/CD Automatizado

Este projeto possui pipeline com **GitHub Actions** que:

- Compila
- Empacota o `.nupkg`
- Publica automaticamente no NuGet

### Como publicar:

1. Altere a versão no `csproj` (se necessário)
2. Faça o commit
3. Crie e envie uma tag:

```bash
git tag v8.0.2
git push origin v8.0.2
```

O pacote será publicado com a versão `v8.0.2`.

---

## 📝 Licença

Distribuído sob a licença MIT.  
Consulte o arquivo [LICENSE.txt](./LICENSE.txt) para mais detalhes.

---

## 👨‍💻 Autor

Desenvolvido por **Wallace Vidal de Figueiredo Fortuna**  
[GitHub - wallacevff](https://github.com/wallacevff)
```
