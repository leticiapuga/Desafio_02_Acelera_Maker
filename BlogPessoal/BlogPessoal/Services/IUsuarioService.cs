using BlogPessoal.DTOs.Usuarios;

namespace BlogPessoal.Services;

public interface IUsuarioService
{
    Task<UsuarioRespostaDto> CadastrarAsync(UsuarioCadastroDto dto);
    Task<UsuarioRespostaDto> AtualizarAsync(long id, UsuarioAtualizacaoDto dto, long usuarioLogadoId, bool usuarioLogadoAdmin);
    Task RemoverAsync(long id);
}
