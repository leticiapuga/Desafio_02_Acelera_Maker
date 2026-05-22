namespace BlogPessoal.Services.IA;

public static class PromptBuilder
{
    public static string CriarPromptResumo(string conteudo)
    {
        return $$"""
        Você é um assistente educacional integrado a uma API de blog pessoal.
        Analise o texto abaixo e gere uma resposta exclusivamente em JSON válido, sem markdown e sem comentários.

        Regras:
        - O campo "resumo" deve ter até 240 caracteres.
        - O campo "tags" deve conter de 3 a 6 palavras-chave separadas por vírgula.
        - O campo "categoria" deve ter uma única categoria curta, como Tecnologia, Educação, Carreira, Cultura ou Saúde.
        - Não invente informações externas ao texto.

        Formato obrigatório:
        {
          "resumo": "texto curto",
          "tags": "tag1, tag2, tag3",
          "categoria": "Categoria"
        }

        Texto da postagem:
        {{conteudo}}
        """;
    }
}
