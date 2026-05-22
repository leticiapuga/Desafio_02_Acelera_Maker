# Plano de Testes

## Objetivo

Garantir que os principais fluxos da API funcionem corretamente e que as regras de negócio sejam respeitadas.

## Cenários essenciais

### Usuários

- Deve cadastrar usuário com dados válidos.
- Não deve cadastrar usuário com e-mail inválido.
- Deve autenticar usuário com credenciais válidas.
- Não deve autenticar usuário com senha incorreta.
- Deve permitir atualização do próprio perfil.
- Deve bloquear atualização de perfil de outro usuário comum.
- Deve permitir exclusão de usuário apenas por ADMIN.

### Temas

- Deve criar tema com descrição válida.
- Deve listar todos os temas.
- Deve atualizar tema existente.
- Não deve excluir tema com postagem vinculada.

### Postagens

- Deve criar postagem vinculada ao usuário autenticado.
- Deve criar postagem vinculada a tema existente.
- Deve gerar resumo, tags e categoria com IA.
- Deve filtrar postagens por autor.
- Deve filtrar postagens por tema.
- Deve bloquear alteração de postagem de outro usuário comum.
- Deve permitir alteração por ADMIN.

### IA

- Deve gerar resumo inteligente para texto válido.
- Deve rejeitar texto vazio ou muito curto.
- Deve usar fallback local quando a chave da API não estiver configurada.

## Testes com Swagger/Postman

1. Cadastrar usuário.
2. Fazer login.
3. Autorizar com Bearer token.
4. Criar tema.
5. Criar postagem.
6. Verificar campos de IA na resposta.
7. Executar filtros.
8. Testar exclusões e permissões.

## Testes automatizados

Execute:

```bash
dotnet test
```

Para cobertura com Coverlet:

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```
