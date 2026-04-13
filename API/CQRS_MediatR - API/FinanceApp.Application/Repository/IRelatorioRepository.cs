using FinanceApp.Application.DTOs.Relatorio.RelatorioPorCategoria;
using FinanceApp.Application.DTOs.Relatorio.RelatorioPorPessoa;

namespace FinanceApp.Application.Repository
{
    public interface IRelatorioRepository
    {
        Task<IEnumerable<TotalPessoaDTO>> TotaisPorPessoa();
        Task<IEnumerable<TotalCategoriaDTO>> TotaisPorCategoria();
    }
}