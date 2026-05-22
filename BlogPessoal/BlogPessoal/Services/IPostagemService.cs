using BlogPessoal.DTOs.Postagens;

namespace BlogPessoal.Services;

public interface IPostagemService
{
    Task<List<PostagemRespostaDto>> ListarAsync();
    Task<List<PostagemRespostaDto>> FiltrarAsync(long? autorId, long? temaId);
    Task<PostagemRespostaDto> BuscarPorIdAsync(long id);
    Task<PostagemRespostaDto> CriarAsync(PostagemCriacaoDto dto, long usuarioLogadoId);
    Task<PostagemRespostaDto> AtualizarAsync(long id, PostagemAtualizacaoDto dto, long usuarioLogadoId, bool usuarioLogadoAdmin);
    Task RemoverAsync(long id, long usuarioLogadoId, bool usuarioLogadoAdmin);
}
