# RhbkSdk

Bem-vindo ao **RhbkSdk**, um SDK desenvolvido para integrar aplicações com a API RHBK de forma simples e eficiente.

## Visão Geral

O **RhbkSdk** foi projetado para facilitar a comunicação com a API RHBK, fornecendo uma interface abstraída para desenvolvedores, com suporte à injeção de dependência e uso de ferramentas populares.

### Principais Funcionalidades

- Comunicação simplificada com a API RHBK.
- Baseado em **.NET 8.0**, com suporte às features mais recentes.
- Utilização de **Refit** para criação de clientes HTTP desacoplados.
- Suporte nativo à Injeção de Dependência com **Microsoft.Extensions.DependencyInjection**.

---

## Requisitos

### Para utilizar este SDK, você precisa:

- **.NET 8.0 ou superior**
- Gerenciador de pacotes NuGet para adicionar este SDK ao seu projeto.

---

## Como Instalar

O SDK está disponível no NuGet. Para instalá-lo no seu projeto, use o seguinte comando:

```bash
dotnet add package RhbkSdk --version 1.0.0
```

Ou adicione diretamente no arquivo `csproj` do seu projeto:

```xml
<PackageReference Include="RhbkSdk" Version="1.0.0" />
```

---

## Como Usar

### Configuração Básica

Antes de usar, certifique-se de configurar a Injeção de Dependência do SDK em seu projeto.

#### Exemplo de Configuração:

```csharp
using RhbkSdk.Extensions;

builder.AddRhbkClient("https://rhbk.url.com", ServiceLifetime.Scoped);

```

### Implementação no Projeto

Após configurar a injeção de dependência, você pode injetar e começar a utilizar os serviços fornecidos pelo **RhbkSdk**:

```csharp
public class MyService
{
    private readonly IRhbkApi _rhbkApi;

    public MyService(IRhbkApi rhbkApi)
    {
        _rhbkApi = rhbkApi;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _rhbkApi.GetUsersAsync();
    }
}
```

---

## Dependências Principais

- [Microsoft.Extensions.DependencyInjection.Abstractions](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection.Abstractions) (8.0.2): Para suporte à injeção de dependência.
- [Refit](https://www.nuget.org/packages/Refit) (8.0.0): Para criação e consumo de APIs RESTful.

---

## Contribuindo

Contribuições são bem-vindas! Siga os passos abaixo para contribuir:

1. Faça um **fork** do repositório.
2. Crie uma nova branch com sua feature: `git checkout -b minha-feature`.
3. Faça o commit das suas alterações: `git commit -m 'Adicionar minha nova feature'`.
4. Submeta sua branch: `git push origin minha-feature`.
5. Abra um Pull Request para revisão.

---

## Licença

O **RhbkSdk** é distribuído sob a licença MIT. Consulte o arquivo [LICENSE](./LICENSE) para mais detalhes.

---

## Autor

- **Wallace Vidal**  
  Para dúvidas ou sugestões, entre em contato pelo [GitHub](https://github.com/seu-usuario-aqui).

---

## Observações

Certifique-se de sempre acompanhar as atualizações da API RHBK para garantir a compatibilidade do SDK com a versão mais recente.
