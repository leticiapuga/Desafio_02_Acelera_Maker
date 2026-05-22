using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.Models;

public class Postagem
{
    public long Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 100 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "O conteúdo é obrigatório.")]
    [MinLength(20, ErrorMessage = "O conteúdo deve ter pelo menos 20 caracteres.")]
    public string Conteudo { get; set; } = string.Empty;

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime? DataAtualizacao { get; set; }

    public long UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }

    public long TemaId { get; set; }
    public Tema? Tema { get; set; }

    [StringLength(500)]
    public string? ResumoIA { get; set; }

    [StringLength(300)]
    public string? TagsIA { get; set; }

    [StringLength(100)]
    public string? CategoriaIA { get; set; }
}
