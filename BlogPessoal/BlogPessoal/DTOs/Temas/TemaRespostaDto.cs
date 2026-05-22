namespace BlogPessoal.DTOs.Temas;

public class TemaRespostaDto
{
    public long Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public int QuantidadePostagens { get; set; }
}
