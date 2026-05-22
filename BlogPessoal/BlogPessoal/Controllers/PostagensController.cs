using System.Security.Claims;
using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Postagens;
using BlogPessoal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers;

[ApiController]
[Route("api/postagens")]
public class PostagensController : ControllerBase
{
    private readonly IPostagemService _postagemService;

    public PostagensController(IPostagemService postagemService)
    {
        _postagemService = postagemService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponseDto<List<PostagemRespostaDto>>>> Listar()
    {
        var postagens = await _postagemService.ListarAsync();
        return Ok(ApiResponseDto<List<PostagemRespostaDto>>.Ok(postagens));
    }

    [HttpGet("{id:long}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponseDto<PostagemRespostaDto>>> BuscarPorId(long id)
    {
        var postagem = await _postagemService.BuscarPorIdAsync(id);
        return Ok(ApiResponseDto<PostagemRespostaDto>.Ok(postagem));
    }

    [HttpGet("filtro")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponseDto<List<PostagemRespostaDto>>>> Filtrar([FromQuery] long? autor, [FromQuery] long? tema)
    {
        var postagens = await _postagemService.FiltrarAsync(autor, tema);
        return Ok(ApiResponseDto<List<PostagemRespostaDto>>.Ok(postagens));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ApiResponseDto<PostagemRespostaDto>>> Criar([FromBody] PostagemCriacaoDto dto)
    {
        var postagem = await _postagemService.CriarAsync(dto, UsuarioLogadoId());
        return Created($"/api/postagens/{postagem.Id}", ApiResponseDto<PostagemRespostaDto>.Ok(postagem, "Postagem criada com resumo inteligente."));
    }

    [HttpPut("{id:long}")]
    [Authorize]
    public async Task<ActionResult<ApiResponseDto<PostagemRespostaDto>>> Atualizar(long id, [FromBody] PostagemAtualizacaoDto dto)
    {
        var postagem = await _postagemService.AtualizarAsync(id, dto, UsuarioLogadoId(), User.IsInRole("ADMIN"));
        return Ok(ApiResponseDto<PostagemRespostaDto>.Ok(postagem, "Postagem atualizada com sucesso."));
    }

    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<ActionResult<ApiResponseDto<string>>> Remover(long id)
    {
        await _postagemService.RemoverAsync(id, UsuarioLogadoId(), User.IsInRole("ADMIN"));
        return Ok(ApiResponseDto<string>.Ok("", "Postagem excluída com sucesso."));
    }

    private long UsuarioLogadoId() => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
}
