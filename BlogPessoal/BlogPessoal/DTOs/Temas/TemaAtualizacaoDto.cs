using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs.Temas;

public class TemaAtualizacaoDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Descricao { get; set; } = string.Empty;
}
