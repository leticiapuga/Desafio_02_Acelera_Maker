namespace BlogPessoal.Config;

public class JwtOptions
{
    public string Secret { get; set; } = "TROQUE_ESTA_CHAVE_POR_UMA_CHAVE_SEGURA_COM_32_CARACTERES_OU_MAIS";
    public string Issuer { get; set; } = "BlogPessoalAPI";
    public string Audience { get; set; } = "BlogPessoalClient";
    public int ExpirationMinutes { get; set; } = 120;
}
