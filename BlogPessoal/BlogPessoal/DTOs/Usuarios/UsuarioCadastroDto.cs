using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs.Usuarios;

public class UsuarioCadastroDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(120, MinimumLength = 3)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres.")]
    public string Senha { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Foto { get; set; }

    [RegularExpression("^(ADMIN|USUARIO|usuario|admin)?$", ErrorMessage = "O tipo deve ser ADMIN ou USUARIO.")]
    public string? Tipo { get; set; } = "USUARIO";
}
