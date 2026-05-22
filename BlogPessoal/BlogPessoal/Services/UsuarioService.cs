using BlogPessoal.DTOs.Usuarios;
using BlogPessoal.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogPessoal.Services;

public class UsuarioService : IUsuarioService
{
    private readonly UserManager<Usuario> _userManager;

    public UsuarioService(UserManager<Usuario> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UsuarioRespostaDto> CadastrarAsync(UsuarioCadastroDto dto)
    {
        var tipo = NormalizarTipo(dto.Tipo);
        var usuario = new Usuario
        {
            Nome = dto.Nome.Trim(),
            UserName = dto.Email.Trim().ToLowerInvariant(),
            Email = dto.Email.Trim().ToLowerInvariant(),
            Foto = dto.Foto,
            Tipo = tipo
        };

        var result = await _userManager.CreateAsync(usuario, dto.Senha);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(string.Join(" | ", result.Errors.Select(e => e.Description)));
        }

        await _userManager.AddToRoleAsync(usuario, tipo);
        return Mapear(usuario);
    }

    public async Task<UsuarioRespostaDto> AtualizarAsync(long id, UsuarioAtualizacaoDto dto, long usuarioLogadoId, bool usuarioLogadoAdmin)
    {
        if (!usuarioLogadoAdmin && usuarioLogadoId != id)
        {
            throw new UnauthorizedAccessException("Você só pode atualizar o próprio perfil.");
        }

        var usuario = await _userManager.FindByIdAsync(id.ToString())
            ?? throw new KeyNotFoundException("Usuário não encontrado.");

        usuario.Nome = dto.Nome.Trim();
        usuario.Foto = dto.Foto;
        usuario.PhoneNumber = dto.Telefone;

        var result = await _userManager.UpdateAsync(usuario);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(string.Join(" | ", result.Errors.Select(e => e.Description)));
        }

        return Mapear(usuario);
    }

    public async Task RemoverAsync(long id)
    {
        var usuario = await _userManager.FindByIdAsync(id.ToString())
            ?? throw new KeyNotFoundException("Usuário não encontrado.");

        var result = await _userManager.DeleteAsync(usuario);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(string.Join(" | ", result.Errors.Select(e => e.Description)));
        }
    }

    private static string NormalizarTipo(string? tipo)
    {
        var valor = string.IsNullOrWhiteSpace(tipo) ? "USUARIO" : tipo.Trim().ToUpperInvariant();
        return valor == "ADMIN" ? "ADMIN" : "USUARIO";
    }

    private static UsuarioRespostaDto Mapear(Usuario usuario) => new()
    {
        Id = usuario.Id,
        Nome = usuario.Nome,
        Email = usuario.Email ?? string.Empty,
        Foto = usuario.Foto,
        Tipo = usuario.Tipo
    };
}
