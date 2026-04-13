using FinanceApp.Entity;
using Microsoft.EntityFrameworkCore;
namespace FinanceApp.DbContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<PessoaEntity> Pessoas => Set<PessoaEntity>();
        public DbSet<CategoriaEntity> Categorias => Set<CategoriaEntity>();
        public DbSet<TransacaoEntity> Transacoes => Set<TransacaoEntity>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransacaoEntity>()
                        .HasOne<PessoaEntity>()
                        .WithMany(p => p.Transacoes)
                        .HasForeignKey(t => t.IdPessoa)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TransacaoEntity>()
                    .Property(t => t.IdPessoa)
                    .HasColumnName("IdPessoa");

            modelBuilder.Entity<TransacaoEntity>()
                .Property(t => t.IdCategoria)
                .HasColumnName("IdCategoria");

            modelBuilder.Entity<TransacaoEntity>()
                .HasOne(t => t.Pessoa)
                .WithMany(p => p.Transacoes)
                .HasForeignKey(t => t.IdPessoa);

            modelBuilder.Entity<TransacaoEntity>()
                .HasOne(t => t.Categoria)
                .WithMany()
                .HasForeignKey(t => t.IdCategoria);

            base.OnModelCreating(modelBuilder);
        }

    }
}