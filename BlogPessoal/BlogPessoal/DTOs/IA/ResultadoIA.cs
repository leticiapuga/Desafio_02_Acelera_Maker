using System.Text.Json.Serialization;

namespace BlogPessoal.DTOs.IA;

public class ResultadoIA
{
    [JsonPropertyName("resumo")]
    public string Resumo { get; set; } = string.Empty;

    [JsonPropertyName("tags")]
    public string Tags { get; set; } = string.Empty;

    [JsonPropertyName("categoria")]
    public string Categoria { get; set; } = string.Empty;
}
