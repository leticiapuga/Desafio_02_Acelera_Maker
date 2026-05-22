using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs.Usuarios;

public class UsuarioAtualizacaoDto
{
    [Required]
    [StringLength(120, MinimumLength = 3)]
    public string Nome { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Foto { get; set; }

    [Phone]
    public string? Telefone { get; set; }
}
