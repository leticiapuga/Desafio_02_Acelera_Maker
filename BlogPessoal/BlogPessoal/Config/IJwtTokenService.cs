using BlogPessoal.Models;

namespace BlogPessoal.Config;

public interface IJwtTokenService
{
    Task<(string Token, DateTime ExpiraEm)> GerarTokenAsync(Usuario usuario);
}
