# Checklist de Especificações Atendidas

Este documento resume como o projeto atende ao enunciado solicitado.

## Requisitos funcionais

| Requisito | Onde foi implementado |
|---|---|
| Cadastro de usuários | `UsuariosController`, `UsuarioService`, ASP.NET Core Identity |
| Atualização de perfil | `PUT /api/usuarios/{id}` |
| Exclusão de usuários | `DELETE /api/usuarios/{id}`, restrito a `ADMIN` |
| Login | `POST /api/usuarios/login` |
| JWT | `JwtTokenService`, configuração em `Program.cs` |
| Controle por tipo de usuário | Roles `ADMIN` e `USUARIO` |
| Criação de postagens | `POST /api/postagens` |
| Atualização de postagens | `PUT /api/postagens/{id}` |
| Exclusão de postagens | `DELETE /api/postagens/{id}` |
| Listagem de postagens | `GET /api/postagens` |
| Filtro por autor/tema | `GET /api/postagens/filtro?autor={id}&tema={id}` |
| Criação de temas | `POST /api/temas` |
| Atualização de temas | `PUT /api/temas/{id}` |
| Exclusão de temas | `DELETE /api/temas/{id}` |
| Listagem de temas | `GET /api/temas` |

## Funcionalidade de IA

| Requisito | Implementação |
|---|---|
| Serviço especializado de IA | `Services/IA/IIAService.cs` e `OpenAIService.cs` |
| Builder de prompt | `Services/IA/PromptBuilder.cs` |
| DTO `ResultadoIA` | `DTOs/IA/ResultadoIA.cs` |
| Endpoint `/api/ia/resumir` | `IAController.cs` |
| Campos na postagem | `ResumoIA`, `TagsIA`, `CategoriaIA` em `Postagem.cs` |
| Integração com API externa | OpenAI Responses API em `OpenAIService.cs` |
| Fallback local | `OpenAIService.GerarResultadoLocal` quando não há chave configurada |
| async/await | Métodos assíncronos em services, repositories e controllers |
| JSON | Serialização e desserialização com `System.Text.Json` |
| Segurança da chave | `OpenAI:ApiKey` configurável por variável de ambiente |

## Requisitos técnicos

| Requisito | Status |
|---|---|
| C# 12+ | Projeto em .NET 8 com `ImplicitUsings` e `Nullable` habilitados |
| ASP.NET Core 8 Web API | `Microsoft.NET.Sdk.Web` |
| Banco relacional | PostgreSQL por padrão; MySQL disponível via Pomelo |
| ORM Entity Framework Core | `AppDbContext` e repositories |
| Identity + JWT | Identity Core com JWT Bearer |
| Swagger/OpenAPI | Swashbuckle configurado em `Program.cs` |
| Arquitetura em camadas | Controllers, Services, Repositories, Models, DTOs |
| Data Annotations | Validações em Models e DTOs |
| Middleware de exceção | `ExceptionMiddleware.cs` |
| Padronização de respostas | `ApiResponseDto<T>` |

## SonarQube e qualidade

| Requisito | Arquivo |
|---|---|
| Configuração SonarQube | `sonar-project.properties` |
| Execução local do SonarQube | `docker-compose.yml` |
| Script de análise | `scripts/run-sonar.sh` |
| Pipeline de qualidade | `.github/workflows/quality.yml` |
| Testes | `BlogPessoal.Tests` |

## Documentação entregue

- `README.md`: guia principal do projeto.
- `docs/ENTREGA.md`: relatório humanizado da entrega.
- `docs/API.md`: exemplos de uso dos endpoints.
- `docs/SONARQUBE.md`: guia de análise de qualidade.
- `docs/TESTES.md`: plano de testes.
- `docs/ESPECIFICACOES_ATENDIDAS.md`: checklist de conformidade.
- `docs/BlogPessoal.postman_collection.json`: coleção inicial para Postman.
