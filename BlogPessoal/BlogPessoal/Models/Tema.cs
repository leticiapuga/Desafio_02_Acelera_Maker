using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.Models;

public class Tema
{
    public long Id { get; set; }

    [Required(ErrorMessage = "A descrição do tema é obrigatória.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "A descrição deve ter entre 3 e 100 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    public ICollection<Postagem> Postagens { get; set; } = new List<Postagem>();
}
