# AGENTS.md

## Objetivo do repositório
- Biblioteca `.NET 8` (`RhbkSdk`) para integração com RHBK/Keycloak.
- Camada de abstração para autenticação, grupos, clientes e usuários.
- Cliente principal: `Providers/RhbkClient.cs`, exposto por `IRhbkClient`.

## Estrutura principal
- `Providers/`: implementação do cliente (`RhbkClient`).
- `Interfaces/`: contratos públicos e contratos Refit.
- `Models/`: modelos de domínio/resposta.
- `RequestBody/`, `RequestParams/`, `ResponseBody/`: DTOs de entrada/saída.
- `Extensions/`: registro em DI (`AddRhbkClient`).
- `Configurations/`: opções de configuração (`RhbkConfiguration`).
- `Exceptions/`: exceções customizadas.

## Fluxo obrigatório para qualquer agente
1. Sempre estudar o projeto primeiro via `repomix`, antes de editar código.
2. Comando padrão:
   - `repomix -o repomix-output.xml`
3. Após gerar o arquivo, usar o conteúdo do `repomix-output.xml` como base inicial de contexto.
4. Só depois complementar com leitura pontual de arquivos específicos quando necessário.

## Boas práticas de alteração
- Preservar APIs públicas de `IRhbkClient` (evitar quebra de contrato).
- Manter compatibilidade com `net8.0`.
- Priorizar mudanças pequenas, testáveis e com impacto localizado.
