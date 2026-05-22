using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Temas;
using BlogPessoal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers;

[ApiController]
[Route("api/temas")]
public class TemasController : ControllerBase
{
    private readonly ITemaService _temaService;

    public TemasController(ITemaService temaService)
    {
        _temaService = temaService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponseDto<List<TemaRespostaDto>>>> Listar()
    {
        var temas = await _temaService.ListarAsync();
        return Ok(ApiResponseDto<List<TemaRespostaDto>>.Ok(temas));
    }

    [HttpGet("{id:long}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponseDto<TemaRespostaDto>>> BuscarPorId(long id)
    {
        var tema = await _temaService.BuscarPorIdAsync(id);
        return Ok(ApiResponseDto<TemaRespostaDto>.Ok(tema));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ApiResponseDto<TemaRespostaDto>>> Criar([FromBody] TemaCriacaoDto dto)
    {
        var tema = await _temaService.CriarAsync(dto);
        return Created($"/api/temas/{tema.Id}", ApiResponseDto<TemaRespostaDto>.Ok(tema, "Tema criado com sucesso."));
    }

    [HttpPut("{id:long}")]
    [Authorize]
    public async Task<ActionResult<ApiResponseDto<TemaRespostaDto>>> Atualizar(long id, [FromBody] TemaAtualizacaoDto dto)
    {
        var tema = await _temaService.AtualizarAsync(id, dto);
        return Ok(ApiResponseDto<TemaRespostaDto>.Ok(tema, "Tema atualizado com sucesso."));
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<ApiResponseDto<string>>> Remover(long id)
    {
        await _temaService.RemoverAsync(id);
        return Ok(ApiResponseDto<string>.Ok("", "Tema excluído com sucesso."));
    }
}
