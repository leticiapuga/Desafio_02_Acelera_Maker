using BlogPessoal.DTOs.Postagens;
using BlogPessoal.Models;
using BlogPessoal.Repositories;
using BlogPessoal.Services.IA;
using Microsoft.AspNetCore.Identity;

namespace BlogPessoal.Services;

public class PostagemService : IPostagemService
{
    private readonly IPostagemRepository _postagemRepository;
    private readonly ITemaRepository _temaRepository;
    private readonly UserManager<Usuario> _userManager;
    private readonly IIAService _iaService;

    public PostagemService(
        IPostagemRepository postagemRepository,
        ITemaRepository temaRepository,
        UserManager<Usuario> userManager,
        IIAService iaService)
    {
        _postagemRepository = postagemRepository;
        _temaRepository = temaRepository;
        _userManager = userManager;
        _iaService = iaService;
    }

    public async Task<List<PostagemRespostaDto>> ListarAsync() =>
        (await _postagemRepository.ListarAsync()).Select(Mapear).ToList();

    public async Task<List<PostagemRespostaDto>> FiltrarAsync(long? autorId, long? temaId) =>
        (await _postagemRepository.FiltrarAsync(autorId, temaId)).Select(Mapear).ToList();

    public async Task<PostagemRespostaDto> BuscarPorIdAsync(long id)
    {
        var postagem = await _postagemRepository.BuscarPorIdAsync(id) ?? throw new KeyNotFoundException("Postagem não encontrada.");
        return Mapear(postagem);
    }

    public async Task<PostagemRespostaDto> CriarAsync(PostagemCriacaoDto dto, long usuarioLogadoId)
    {
        var usuario = await _userManager.FindByIdAsync(usuarioLogadoId.ToString())
            ?? throw new KeyNotFoundException("Usuário autenticado não encontrado.");

        if (!await _temaRepository.ExisteAsync(dto.TemaId))
        {
            throw new KeyNotFoundException("Tema informado não existe.");
        }

        var resultadoIA = await _iaService.GerarResumoAsync(dto.Conteudo);

        var postagem = new Postagem
        {
            Titulo = dto.Titulo.Trim(),
            Conteudo = dto.Conteudo.Trim(),
            UsuarioId = usuario.Id,
            TemaId = dto.TemaId,
            ResumoIA = resultadoIA.Resumo,
            TagsIA = resultadoIA.Tags,
            CategoriaIA = resultadoIA.Categoria
        };

        return Mapear(await _postagemRepository.AdicionarAsync(postagem));
    }

    public async Task<PostagemRespostaDto> AtualizarAsync(long id, PostagemAtualizacaoDto dto, long usuarioLogadoId, bool usuarioLogadoAdmin)
    {
        var postagem = await _postagemRepository.BuscarPorIdAsync(id) ?? throw new KeyNotFoundException("Postagem não encontrada.");
        ValidarPermissao(postagem, usuarioLogadoId, usuarioLogadoAdmin);

        if (!await _temaRepository.ExisteAsync(dto.TemaId))
        {
            throw new KeyNotFoundException("Tema informado não existe.");
        }

        var resultadoIA = await _iaService.GerarResumoAsync(dto.Conteudo);

        postagem.Titulo = dto.Titulo.Trim();
        postagem.Conteudo = dto.Conteudo.Trim();
        postagem.TemaId = dto.TemaId;
        postagem.DataAtualizacao = DateTime.UtcNow;
        postagem.ResumoIA = resultadoIA.Resumo;
        postagem.TagsIA = resultadoIA.Tags;
        postagem.CategoriaIA = resultadoIA.Categoria;

        return Mapear(await _postagemRepository.AtualizarAsync(postagem));
    }

    public async Task RemoverAsync(long id, long usuarioLogadoId, bool usuarioLogadoAdmin)
    {
        var postagem = await _postagemRepository.BuscarPorIdAsync(id) ?? throw new KeyNotFoundException("Postagem não encontrada.");
        ValidarPermissao(postagem, usuarioLogadoId, usuarioLogadoAdmin);
        await _postagemRepository.RemoverAsync(postagem);
    }

    private static void ValidarPermissao(Postagem postagem, long usuarioLogadoId, bool usuarioLogadoAdmin)
    {
        if (!usuarioLogadoAdmin && postagem.UsuarioId != usuarioLogadoId)
        {
            throw new UnauthorizedAccessException("Você só pode alterar ou excluir as próprias postagens.");
        }
    }

    private static PostagemRespostaDto Mapear(Postagem postagem) => new()
    {
        Id = postagem.Id,
        Titulo = postagem.Titulo,
        Conteudo = postagem.Conteudo,
        DataCriacao = postagem.DataCriacao,
        DataAtualizacao = postagem.DataAtualizacao,
        UsuarioId = postagem.UsuarioId,
        Autor = postagem.Usuario?.Nome,
        TemaId = postagem.TemaId,
        Tema = postagem.Tema?.Descricao,
        ResumoIA = postagem.ResumoIA,
        TagsIA = postagem.TagsIA,
        CategoriaIA = postagem.CategoriaIA
    };
}
