using Dapper;
using FinanceApp.DTOs.Relatorio.RelatorioPorCategoria;
using FinanceApp.DTOs.Relatorio.RelatorioPorPessoa;
using FinanceApp.Repository.Interface;
using MySqlConnector;
namespace FinanceApp.Repository
{
    public class RelatorioRepository : IRelatorioRepository
    {

        private readonly IConfiguration _configuration;

        public RelatorioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<TotalPessoaDTO>> TotaisPorPessoa()
        {
            using var conn = new MySqlConnection(_configuration["ConnectionStrings:Default"]);
            return await conn.QueryAsync<TotalPessoaDTO>("CALL spTotaisPorPessoa();");
        }

        public async Task<IEnumerable<TotalCategoriaDTO>> TotaisPorCategoria()
        {
            using var conn = new MySqlConnection(_configuration["ConnectionStrings:Default"]);
            return await conn.QueryAsync<TotalCategoriaDTO>("CALL spTotaisPorCategoria();");
        }

    }
}