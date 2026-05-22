# Blog Pessoal - ASP.NET Core 8 Web API

API RESTful para um **Blog Pessoal**, desenvolvida com **ASP.NET Core 8**.

O projeto permite cadastrar usuários, fazer login com JWT, criar temas, publicar postagens e gerar informações inteligentes com IA, como resumo, tags e categoria sugerida.

---

## Sobre o projeto

A API possui três recursos principais:

- **Usuários**: cadastro, login, atualização e exclusão;
- **Temas**: organização das postagens por assunto;
- **Postagens**: criação, listagem, atualização, exclusão e filtros.

Também foi implementada uma funcionalidade de IA chamada **Resumo Inteligente de Postagens**, que pode gerar automaticamente:

- resumo da postagem;
- tags relacionadas;
- categoria sugerida.

---

## Tecnologias utilizadas

- ASP.NET Core 8
- C#
- Entity Framework Core
- PostgreSQL
- JWT
- Swagger
- SonarQube
- OpenAI API
- Docker

---

## Organização do projeto

```text
BlogPessoal/
├── Controllers/
├── Services/
│   └── IA/
├── Repositories/
├── Models/
├── DTOs/
├── Data/
├── Config/
├── Middlewares/
├── Migrations/
├── BlogPessoal.Tests/
├── appsettings.json
├── Program.cs
├── docker-compose.yml
└── BlogPessoal.csproj
```

---

## Banco de dados

O projeto utiliza **PostgreSQL**.

Crie um banco chamado:

```text
blog_pessoal
```

Depois configure a conexão no arquivo `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=blog_pessoal;Username=postgres;Password=SUA_SENHA"
}
```

Substitua `SUA_SENHA` pela senha do seu PostgreSQL local.

---

## Como rodar o projeto

### 1. Restaurar as dependências

```bash
dotnet restore
```

---

### 2. Aplicar as migrations

Caso ainda não tenha o Entity Framework CLI instalado:

```bash
dotnet tool install --global dotnet-ef
```

Depois rode:

```bash
dotnet ef database update
```

Esse comando cria as tabelas no banco `blog_pessoal`.

---

### 3. Configurar variáveis de ambiente

No Windows CMD:

```bat
set ASPNETCORE_ENVIRONMENT=Development
set Jwt__Secret=UMA_CHAVE_SEGURA_COM_PELO_MENOS_32_CARACTERES
set ConnectionStrings__DefaultConnection=Host=localhost;Port=5432;Database=blog_pessoal;Username=postgres;Password=SUA_SENHA
```

No PowerShell:

```powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
$env:Jwt__Secret="UMA_CHAVE_SEGURA_COM_PELO_MENOS_32_CARACTERES"
$env:ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Database=blog_pessoal;Username=postgres;Password=SUA_SENHA"
```

A chave da OpenAI é opcional. Caso queira usar a IA externa:

```bat
set OpenAI__ApiKey=SUA_CHAVE_AQUI
```

Se a chave não for configurada, o projeto usa um resumo local simples para permitir testes.

---

### 4. Executar a API

```bash
dotnet run --urls "http://localhost:5000"
```

Depois acesse o Swagger:

```text
http://localhost:5000/swagger
```

---

## Fluxo básico para testar

1. Cadastre um usuário em:

```text
POST /api/usuarios/cadastrar
```

2. Faça login em:

```text
POST /api/usuarios/login
```

3. Copie o token retornado.

4. Clique em **Authorize** no Swagger e informe:

```text
Bearer SEU_TOKEN
```

5. Crie um tema em:

```text
POST /api/temas
```

6. Crie uma postagem em:

```text
POST /api/postagens
```

Ao criar a postagem, a API pode retornar os campos:

```json
{
  "resumoIA": "...",
  "tagsIA": "...",
  "categoriaIA": "..."
}
```

---

## Orientações sobre PostgreSQL e Docker

### Usando PostgreSQL local

Se você já tem PostgreSQL instalado na máquina, não precisa usar Docker para o banco.

Basta:

1. Criar o banco `blog_pessoal`;
2. Configurar a string de conexão no `appsettings.json` ou por variável de ambiente;
3. Rodar:

```bash
dotnet ef database update
```

---

### Usando Docker

O projeto possui um arquivo `docker-compose.yml`.

Ele pode ser usado por quem quiser subir serviços como PostgreSQL ou SonarQube sem instalar tudo manualmente.

Para subir o SonarQube:

```bash
docker compose up -d sonarqube
```

Para subir todos os serviços configurados no Docker:

```bash
docker compose up -d
```

Acesse o SonarQube em:

```text
http://localhost:9000
```

O Docker é opcional. Quem já tiver PostgreSQL instalado pode usar o banco local normalmente.

---

## Testes

Para rodar os testes:

```bash
dotnet test
```

---


## Objetivo

Este projeto foi desenvolvido para praticar a criação de APIs RESTful com ASP.NET Core, aplicando conceitos de autenticação, banco de dados, arquitetura em camadas, integração com IA e qualidade de código.