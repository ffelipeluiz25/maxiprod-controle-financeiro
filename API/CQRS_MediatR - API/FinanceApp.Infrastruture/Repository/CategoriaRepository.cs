using Dapper;
using FinanceApp.Application.DTOs;
using FinanceApp.Application.Repository;
using FinanceApp.Domain.Entidades;
using FinanceApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
namespace FinanceApp.Infrastructure.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        private readonly string _connectionString;

        public CategoriaRepository(AppDbContext context, IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _context = context;
        }

        public async Task<RetornoDTO<int>> Add(CategoriaEntity categoria)
        {
            try
            {
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
                return new RetornoDTO<int>(true, categoria.Id);
            }
            catch (Exception ex)
            {
                return new RetornoDTO<int>(false, ex.Message);
            }
        }

        public async Task<List<CategoriaEntity>> ListarTodas()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<RetornoDTO<CategoriaEntity>> ObterPorId(int idCategoria)
        {
            try
            {

                using var conn = new MySqlConnection(_connectionString);

                var sql = @"SELECT Id, Descricao, Finalidade, DataCriacao, DataAlteracao
                        FROM Categorias
                        WHERE Id = @Id";
                var categoria = await conn.QueryFirstOrDefaultAsync<CategoriaEntity>(sql, new { Id = idCategoria });
                return new RetornoDTO<CategoriaEntity>(true, categoria);
            }
            catch (Exception ex)
            {
                return new RetornoDTO<CategoriaEntity>(false, ex.Message);
            }
        }

    }
}