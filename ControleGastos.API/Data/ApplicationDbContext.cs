using Microsoft.EntityFrameworkCore;
using ControleGastos.API.Models;

namespace ControleGastos.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Nome).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Idade).IsRequired();

            entity.HasMany(p => p.Transacoes)
                  .WithOne(t => t.Pessoa)
                  .HasForeignKey(t => t.PessoaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Descricao).IsRequired().HasMaxLength(200);
            entity.Property(c => c.Finalidade).IsRequired();

            entity.HasMany(c => c.Transacoes)
                  .WithOne(t => t.Categoria)
                  .HasForeignKey(t => t.CategoriaId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Transacao>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Descricao).IsRequired().HasMaxLength(500);
            entity.Property(t => t.Valor).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(t => t.Tipo).IsRequired();

            entity.HasOne(t => t.Pessoa)
                  .WithMany(p => p.Transacoes)
                  .HasForeignKey(t => t.PessoaId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(t => t.Categoria)
                  .WithMany(c => c.Transacoes)
                  .HasForeignKey(t => t.CategoriaId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
