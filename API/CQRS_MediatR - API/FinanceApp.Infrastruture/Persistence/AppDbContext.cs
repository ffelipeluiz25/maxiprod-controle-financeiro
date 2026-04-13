using FinanceApp.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
namespace FinanceApp.Infrastructure.Persistence
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

            base.OnModelCreating(modelBuilder);
        }
    }
}