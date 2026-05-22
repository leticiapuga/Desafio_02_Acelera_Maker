using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs.Postagens;

public class PostagemAtualizacaoDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    [MinLength(20)]
    public string Conteudo { get; set; } = string.Empty;

    [Required]
    [Range(1, long.MaxValue)]
    public long TemaId { get; set; }
}
