using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs.Usuarios;

public class UsuarioLoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;
}
