using BlogPessoal.Config;
using BlogPessoal.DTOs.Usuarios;
using BlogPessoal.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogPessoal.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<TokenDto> LoginAsync(UsuarioLoginDto dto)
    {
        var usuario = await _userManager.FindByEmailAsync(dto.Email);
        if (usuario is null)
        {
            throw new UnauthorizedAccessException("E-mail ou senha inválidos.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(usuario, dto.Senha, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException("E-mail ou senha inválidos.");
        }

        var token = await _jwtTokenService.GerarTokenAsync(usuario);
        return new TokenDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email ?? string.Empty,
            Tipo = usuario.Tipo,
            Token = token.Token,
            ExpiraEm = token.ExpiraEm
        };
    }
}
