using BlogPessoal.Data;
using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories;

public class PostagemRepository : IPostagemRepository
{
    private readonly AppDbContext _context;

    public PostagemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Postagem>> ListarAsync() =>
        await BaseQuery().OrderByDescending(p => p.DataCriacao).ToListAsync();

    public async Task<List<Postagem>> FiltrarAsync(long? autorId, long? temaId)
    {
        var query = BaseQuery();

        if (autorId.HasValue)
        {
            query = query.Where(p => p.UsuarioId == autorId.Value);
        }

        if (temaId.HasValue)
        {
            query = query.Where(p => p.TemaId == temaId.Value);
        }

        return await query.OrderByDescending(p => p.DataCriacao).ToListAsync();
    }

    public async Task<Postagem?> BuscarPorIdAsync(long id) =>
        await BaseQuery().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Postagem> AdicionarAsync(Postagem postagem)
    {
        _context.Postagens.Add(postagem);
        await _context.SaveChangesAsync();
        return (await BuscarPorIdAsync(postagem.Id))!;
    }

    public async Task<Postagem> AtualizarAsync(Postagem postagem)
    {
        _context.Postagens.Update(postagem);
        await _context.SaveChangesAsync();
        return (await BuscarPorIdAsync(postagem.Id))!;
    }

    public async Task RemoverAsync(Postagem postagem)
    {
        _context.Postagens.Remove(postagem);
        await _context.SaveChangesAsync();
    }

    private IQueryable<Postagem> BaseQuery() =>
        _context.Postagens
            .Include(p => p.Usuario)
            .Include(p => p.Tema)
            .AsQueryable();
}
