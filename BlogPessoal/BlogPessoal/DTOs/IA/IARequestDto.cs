using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs.IA;

public class IARequestDto
{
    [Required(ErrorMessage = "O texto da postagem é obrigatório.")]
    [MinLength(20, ErrorMessage = "Informe um texto com pelo menos 20 caracteres.")]
    public string Texto { get; set; } = string.Empty;
}
