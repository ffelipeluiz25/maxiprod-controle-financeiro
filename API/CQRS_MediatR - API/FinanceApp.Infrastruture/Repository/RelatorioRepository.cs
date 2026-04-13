using Dapper;
using FinanceApp.Application.DTOs.Relatorio.RelatorioPorCategoria;
using FinanceApp.Application.DTOs.Relatorio.RelatorioPorPessoa;
using FinanceApp.Application.Repository;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
namespace FinanceApp.Infrastructure.Repository
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly string _connectionString;

        public RelatorioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<IEnumerable<TotalPessoaDTO>> TotaisPorPessoa()
        {
            using var conn = new MySqlConnection(_connectionString);
            return await conn.QueryAsync<TotalPessoaDTO>("CALL spTotaisPorPessoa();");
        }

        public async Task<IEnumerable<TotalCategoriaDTO>> TotaisPorCategoria()
        {
            using var conn = new MySqlConnection(_connectionString);
            return await conn.QueryAsync<TotalCategoriaDTO>("CALL spTotaisPorCategoria();");
        }
    }
}