using BlogPessoal.DTOs;
using BlogPessoal.DTOs.IA;
using BlogPessoal.Services.IA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers;

[ApiController]
[Route("api/ia")]
public class IAController : ControllerBase
{
    private readonly IIAService _iaService;

    public IAController(IIAService iaService)
    {
        _iaService = iaService;
    }

    [HttpPost("resumir")]
    [Authorize]
    public async Task<ActionResult<ApiResponseDto<ResultadoIA>>> Resumir([FromBody] IARequestDto dto)
    {
        var resultado = await _iaService.GerarResumoAsync(dto.Texto);
        return Ok(ApiResponseDto<ResultadoIA>.Ok(resultado, "Resumo inteligente gerado com sucesso."));
    }
}
