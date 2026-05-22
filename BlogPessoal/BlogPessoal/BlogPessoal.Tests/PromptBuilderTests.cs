using BlogPessoal.Services.IA;
using Xunit;

namespace BlogPessoal.Tests;

public class PromptBuilderTests
{
    [Fact]
    public void CriarPromptResumo_DeveConterFormatoJsonObrigatorio()
    {
        var prompt = PromptBuilder.CriarPromptResumo("Texto de teste sobre APIs RESTful em ASP.NET Core.");

        Assert.Contains("resumo", prompt);
        Assert.Contains("tags", prompt);
        Assert.Contains("categoria", prompt);
        Assert.Contains("JSON válido", prompt);
    }
}
