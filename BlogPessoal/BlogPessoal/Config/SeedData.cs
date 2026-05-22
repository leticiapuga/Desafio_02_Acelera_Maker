using Microsoft.AspNetCore.Identity;

namespace BlogPessoal.Config;

public static class SeedData
{
    public static async Task CriarRolesPadraoAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("SeedData");
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<long>>>();

        try
        {
            foreach (var role in new[] { "ADMIN", "USUARIO" })
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<long>(role));
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Roles padrão não foram criadas. Execute as migrations e rode a aplicação novamente.");
        }
    }
}
