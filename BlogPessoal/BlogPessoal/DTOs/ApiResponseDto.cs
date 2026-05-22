namespace BlogPessoal.DTOs;

public class ApiResponseDto<T>
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public T? Dados { get; set; }
    public IEnumerable<string> Erros { get; set; } = Array.Empty<string>();

    public static ApiResponseDto<T> Ok(T dados, string mensagem = "Operação realizada com sucesso.") => new()
    {
        Sucesso = true,
        Mensagem = mensagem,
        Dados = dados
    };

    public static ApiResponseDto<T> Falha(string mensagem, IEnumerable<string>? erros = null) => new()
    {
        Sucesso = false,
        Mensagem = mensagem,
        Erros = erros ?? Array.Empty<string>()
    };
}
