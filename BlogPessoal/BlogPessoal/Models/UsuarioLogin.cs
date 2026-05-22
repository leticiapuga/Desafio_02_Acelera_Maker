using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.Models;

public class UsuarioLogin
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;
}
