using BlogPessoal.DTOs.Temas;
using BlogPessoal.Models;
using BlogPessoal.Repositories;
using BlogPessoal.Services;
using Xunit;

namespace BlogPessoal.Tests;

public class TemaServiceTests
{
    [Fact]
    public async Task CriarAsync_DeveRetornarTemaComDescricao()
    {
        var repository = new TemaRepositoryFake();
        var service = new TemaService(repository);

        var resultado = await service.CriarAsync(new TemaCriacaoDto { Descricao = "Tecnologia" });

        Assert.Equal("Tecnologia", resultado.Descricao);
        Assert.True(resultado.Id > 0);
    }

    private class TemaRepositoryFake : ITemaRepository
    {
        private readonly List<Tema> _temas = new();
        private long _nextId = 1;

        public Task<Tema> AdicionarAsync(Tema tema)
        {
            tema.Id = _nextId++;
            _temas.Add(tema);
            return Task.FromResult(tema);
        }

        public Task<Tema> AtualizarAsync(Tema tema) => Task.FromResult(tema);
        public Task<Tema?> BuscarPorIdAsync(long id) => Task.FromResult(_temas.FirstOrDefault(t => t.Id == id));
        public Task<bool> ExisteAsync(long id) => Task.FromResult(_temas.Any(t => t.Id == id));
        public Task<List<Tema>> ListarAsync() => Task.FromResult(_temas.ToList());
        public Task RemoverAsync(Tema tema)
        {
            _temas.Remove(tema);
            return Task.CompletedTask;
        }
    }
}
