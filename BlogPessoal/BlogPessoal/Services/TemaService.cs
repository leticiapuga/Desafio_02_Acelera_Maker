using BlogPessoal.DTOs.Temas;
using BlogPessoal.Models;
using BlogPessoal.Repositories;

namespace BlogPessoal.Services;

public class TemaService : ITemaService
{
    private readonly ITemaRepository _repository;

    public TemaService(ITemaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TemaRespostaDto>> ListarAsync() =>
        (await _repository.ListarAsync()).Select(Mapear).ToList();

    public async Task<TemaRespostaDto> BuscarPorIdAsync(long id)
    {
        var tema = await _repository.BuscarPorIdAsync(id) ?? throw new KeyNotFoundException("Tema não encontrado.");
        return Mapear(tema);
    }

    public async Task<TemaRespostaDto> CriarAsync(TemaCriacaoDto dto)
    {
        var tema = new Tema { Descricao = dto.Descricao.Trim() };
        return Mapear(await _repository.AdicionarAsync(tema));
    }

    public async Task<TemaRespostaDto> AtualizarAsync(long id, TemaAtualizacaoDto dto)
    {
        var tema = await _repository.BuscarPorIdAsync(id) ?? throw new KeyNotFoundException("Tema não encontrado.");
        tema.Descricao = dto.Descricao.Trim();
        return Mapear(await _repository.AtualizarAsync(tema));
    }

    public async Task RemoverAsync(long id)
    {
        var tema = await _repository.BuscarPorIdAsync(id) ?? throw new KeyNotFoundException("Tema não encontrado.");
        if (tema.Postagens.Any())
        {
            throw new InvalidOperationException("Não é possível excluir um tema que possui postagens vinculadas.");
        }

        await _repository.RemoverAsync(tema);
    }

    private static TemaRespostaDto Mapear(Tema tema) => new()
    {
        Id = tema.Id,
        Descricao = tema.Descricao,
        QuantidadePostagens = tema.Postagens.Count
    };
}
