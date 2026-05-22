# Documentação dos Endpoints

## Autenticação

### Cadastrar usuário

```http
POST /api/usuarios/cadastrar
```

```json
{
  "nome": "Letícia Silva",
  "email": "leticia@email.com",
  "senha": "Senha123",
  "foto": "https://exemplo.com/foto.png",
  "tipo": "USUARIO"
}
```

### Login

```http
POST /api/usuarios/login
```

```json
{
  "email": "leticia@email.com",
  "senha": "Senha123"
}
```

Use o token retornado no Swagger ou em clientes como Postman/Insomnia:

```text
Authorization: Bearer SEU_TOKEN
```

## Temas

### Criar tema

```http
POST /api/temas
Authorization: Bearer SEU_TOKEN
```

```json
{
  "descricao": "Tecnologia"
}
```

### Listar temas

```http
GET /api/temas
```

## Postagens

### Criar postagem com IA

```http
POST /api/postagens
Authorization: Bearer SEU_TOKEN
```

```json
{
  "titulo": "Boas práticas em APIs REST",
  "conteudo": "APIs RESTful bem planejadas facilitam manutenção, integração entre sistemas e evolução dos produtos digitais.",
  "temaId": 1
}
```

### Filtrar postagens

```http
GET /api/postagens/filtro?autor=1&tema=1
```

Também é possível informar apenas um dos filtros:

```http
GET /api/postagens/filtro?tema=1
GET /api/postagens/filtro?autor=1
```

## IA

### Gerar resumo inteligente

```http
POST /api/ia/resumir
Authorization: Bearer SEU_TOKEN
```

```json
{
  "texto": "APIs RESTful bem planejadas facilitam manutenção, integração entre sistemas e evolução dos produtos digitais."
}
```

Resposta esperada:

```json
{
  "sucesso": true,
  "mensagem": "Resumo inteligente gerado com sucesso.",
  "dados": {
    "resumo": "Texto resumido pela IA.",
    "tags": "API, REST, ASP.NET Core",
    "categoria": "Tecnologia"
  },
  "erros": []
}
```
