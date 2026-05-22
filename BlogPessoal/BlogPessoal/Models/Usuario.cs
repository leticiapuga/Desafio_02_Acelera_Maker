using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BlogPessoal.Models;

public class Usuario : IdentityUser<long>
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(120, ErrorMessage = "O nome deve ter no máximo 120 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "A URL da foto deve ter no máximo 500 caracteres.")]
    public string? Foto { get; set; }

    [Required]
    [StringLength(20)]
    public string Tipo { get; set; } = "USUARIO";

    public ICollection<Postagem> Postagens { get; set; } = new List<Postagem>();
}
