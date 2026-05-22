using BlogPessoal.Models;

namespace BlogPessoal.Repositories;

public interface ITemaRepository
{
    Task<List<Tema>> ListarAsync();
    Task<Tema?> BuscarPorIdAsync(long id);
    Task<Tema> AdicionarAsync(Tema tema);
    Task<Tema> AtualizarAsync(Tema tema);
    Task RemoverAsync(Tema tema);
    Task<bool> ExisteAsync(long id);
}
