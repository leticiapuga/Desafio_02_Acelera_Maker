using BlogPessoal.DTOs.Usuarios;

namespace BlogPessoal.Services;

public interface IAuthService
{
    Task<TokenDto> LoginAsync(UsuarioLoginDto dto);
}
