using BlogPessoal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Data;

public class AppDbContext : IdentityDbContext<Usuario, IdentityRole<long>, long>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Postagem> Postagens => Set<Postagem>();
    public DbSet<Tema> Temas => Set<Tema>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuarios");
            entity.Property(u => u.Nome).HasMaxLength(120).IsRequired();
            entity.Property(u => u.Foto).HasMaxLength(500);
            entity.Property(u => u.Tipo).HasMaxLength(20).HasDefaultValue("USUARIO");
        });

        builder.Entity<Tema>(entity =>
        {
            entity.ToTable("temas");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Descricao).HasMaxLength(100).IsRequired();
        });

        builder.Entity<Postagem>(entity =>
        {
            entity.ToTable("postagens");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Titulo).HasMaxLength(100).IsRequired();
            entity.Property(p => p.Conteudo).IsRequired();
            entity.Property(p => p.ResumoIA).HasMaxLength(500);
            entity.Property(p => p.TagsIA).HasMaxLength(300);
            entity.Property(p => p.CategoriaIA).HasMaxLength(100);

            entity.HasOne(p => p.Usuario)
                .WithMany(u => u.Postagens)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Tema)
                .WithMany(t => t.Postagens)
                .HasForeignKey(p => p.TemaId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
