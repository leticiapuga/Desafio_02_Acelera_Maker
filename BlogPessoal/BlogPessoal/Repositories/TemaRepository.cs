using BlogPessoal.Data;
using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories;

public class TemaRepository : ITemaRepository
{
    private readonly AppDbContext _context;

    public TemaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tema>> ListarAsync() =>
        await _context.Temas.Include(t => t.Postagens).OrderBy(t => t.Descricao).ToListAsync();

    public async Task<Tema?> BuscarPorIdAsync(long id) =>
        await _context.Temas.Include(t => t.Postagens).FirstOrDefaultAsync(t => t.Id == id);

    public async Task<Tema> AdicionarAsync(Tema tema)
    {
        _context.Temas.Add(tema);
        await _context.SaveChangesAsync();
        return tema;
    }

    public async Task<Tema> AtualizarAsync(Tema tema)
    {
        _context.Temas.Update(tema);
        await _context.SaveChangesAsync();
        return tema;
    }

    public async Task RemoverAsync(Tema tema)
    {
        _context.Temas.Remove(tema);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExisteAsync(long id) => await _context.Temas.AnyAsync(t => t.Id == id);
}
