using BlogPessoal.Models;

namespace BlogPessoal.Repositories;

public interface IPostagemRepository
{
    Task<List<Postagem>> ListarAsync();
    Task<List<Postagem>> FiltrarAsync(long? autorId, long? temaId);
    Task<Postagem?> BuscarPorIdAsync(long id);
    Task<Postagem> AdicionarAsync(Postagem postagem);
    Task<Postagem> AtualizarAsync(Postagem postagem);
    Task RemoverAsync(Postagem postagem);
}
