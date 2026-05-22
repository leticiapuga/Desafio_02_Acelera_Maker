namespace BlogPessoal.DTOs.Usuarios;

public class UsuarioRespostaDto
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Foto { get; set; }
    public string Tipo { get; set; } = string.Empty;
}
