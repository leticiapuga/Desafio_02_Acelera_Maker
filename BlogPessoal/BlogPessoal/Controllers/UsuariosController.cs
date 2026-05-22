using System.Security.Claims;
using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Usuarios;
using BlogPessoal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly IAuthService _authService;

    public UsuariosController(IUsuarioService usuarioService, IAuthService authService)
    {
        _usuarioService = usuarioService;
        _authService = authService;
    }

    [HttpPost("cadastrar")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponseDto<UsuarioRespostaDto>>> Cadastrar([FromBody] UsuarioCadastroDto dto)
    {
        var usuario = await _usuarioService.CadastrarAsync(dto);
        return Created($"/api/usuarios/{usuario.Id}", ApiResponseDto<UsuarioRespostaDto>.Ok(usuario, "Usuário cadastrado com sucesso."));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponseDto<TokenDto>>> Login([FromBody] UsuarioLoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);
        return Ok(ApiResponseDto<TokenDto>.Ok(token, "Login realizado com sucesso."));
    }

    [HttpPut("{id:long}")]
    [Authorize]
    public async Task<ActionResult<ApiResponseDto<UsuarioRespostaDto>>> Atualizar(long id, [FromBody] UsuarioAtualizacaoDto dto)
    {
        var usuarioAtualizado = await _usuarioService.AtualizarAsync(id, dto, UsuarioLogadoId(), User.IsInRole("ADMIN"));
        return Ok(ApiResponseDto<UsuarioRespostaDto>.Ok(usuarioAtualizado, "Usuário atualizado com sucesso."));
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<ApiResponseDto<string>>> Remover(long id)
    {
        await _usuarioService.RemoverAsync(id);
        return Ok(ApiResponseDto<string>.Ok("", "Usuário excluído com sucesso."));
    }

    private long UsuarioLogadoId() => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
}
