using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BlogPessoal.Config;
using BlogPessoal.DTOs.IA;
using Microsoft.Extensions.Options;

namespace BlogPessoal.Services.IA;

public class OpenAIService : IIAService
{
    private readonly HttpClient _httpClient;
    private readonly OpenAIOptions _options;
    private readonly ILogger<OpenAIService> _logger;

    public OpenAIService(HttpClient httpClient, IOptions<OpenAIOptions> options, ILogger<OpenAIService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<ResultadoIA> GerarResumoAsync(string conteudo)
    {
        if (string.IsNullOrWhiteSpace(conteudo))
        {
            throw new ArgumentException("O conteúdo da postagem não pode ser vazio.", nameof(conteudo));
        }

        if (string.IsNullOrWhiteSpace(_options.ApiKey))
        {
            return GerarResultadoLocal(conteudo);
        }

        try
        {
            using var request = CriarRequest(conteudo);
            using var response = await _httpClient.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Falha ao consultar OpenAI. Status: {Status}. Body: {Body}", response.StatusCode, body);
                return GerarResultadoLocal(conteudo);
            }

            var textoResposta = ExtrairTextoResposta(body);
            var resultado = DesserializarResultado(textoResposta);
            return resultado ?? GerarResultadoLocal(conteudo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao gerar resumo inteligente com IA.");
            return GerarResultadoLocal(conteudo);
        }
    }

    private HttpRequestMessage CriarRequest(string conteudo)
    {
        var payload = new
        {
            model = _options.Model,
            input = PromptBuilder.CriarPromptResumo(conteudo)
        };

        var request = new HttpRequestMessage(HttpMethod.Post, _options.BaseUrl);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiKey);
        request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        return request;
    }

    private static string ExtrairTextoResposta(string responseBody)
    {
        using var document = JsonDocument.Parse(responseBody);
        var root = document.RootElement;

        if (root.TryGetProperty("output_text", out var outputText))
        {
            return outputText.GetString() ?? string.Empty;
        }

        if (root.TryGetProperty("output", out var output) && output.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in output.EnumerateArray())
            {
                if (!item.TryGetProperty("content", out var content) || content.ValueKind != JsonValueKind.Array)
                {
                    continue;
                }

                foreach (var contentItem in content.EnumerateArray())
                {
                    if (contentItem.TryGetProperty("text", out var text))
                    {
                        return text.GetString() ?? string.Empty;
                    }
                }
            }
        }

        return string.Empty;
    }

    private static ResultadoIA? DesserializarResultado(string texto)
    {
        var limpo = texto.Trim()
            .Replace("```json", string.Empty, StringComparison.OrdinalIgnoreCase)
            .Replace("```", string.Empty, StringComparison.OrdinalIgnoreCase)
            .Trim();

        if (string.IsNullOrWhiteSpace(limpo))
        {
            return null;
        }

        return JsonSerializer.Deserialize<ResultadoIA>(limpo, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    private static ResultadoIA GerarResultadoLocal(string conteudo)
    {
        var texto = conteudo.Trim();
        var resumo = texto.Length <= 220 ? texto : texto[..220].TrimEnd() + "...";
        var palavras = texto.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(p => new string(p.Where(char.IsLetterOrDigit).ToArray()).ToLowerInvariant())
            .Where(p => p.Length > 4)
            .GroupBy(p => p)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Take(5)
            .Select(g => g.Key);

        return new ResultadoIA
        {
            Resumo = resumo,
            Tags = string.Join(", ", palavras.DefaultIfEmpty("blog")),
            Categoria = InferirCategoria(texto)
        };
    }

    private static string InferirCategoria(string texto)
    {
        var lower = texto.ToLowerInvariant();
        if (lower.Contains("api") || lower.Contains("tecnologia") || lower.Contains("programação") || lower.Contains("software"))
        {
            return "Tecnologia";
        }

        if (lower.Contains("estudo") || lower.Contains("educação") || lower.Contains("aprendizado"))
        {
            return "Educação";
        }

        return "Geral";
    }
}
