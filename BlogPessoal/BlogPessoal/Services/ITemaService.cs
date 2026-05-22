using BlogPessoal.DTOs.Temas;

namespace BlogPessoal.Services;

public interface ITemaService
{
    Task<List<TemaRespostaDto>> ListarAsync();
    Task<TemaRespostaDto> BuscarPorIdAsync(long id);
    Task<TemaRespostaDto> CriarAsync(TemaCriacaoDto dto);
    Task<TemaRespostaDto> AtualizarAsync(long id, TemaAtualizacaoDto dto);
    Task RemoverAsync(long id);
}
