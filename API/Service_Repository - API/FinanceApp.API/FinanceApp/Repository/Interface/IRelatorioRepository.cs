using FinanceApp.DTOs.Relatorio.RelatorioPorCategoria;
using FinanceApp.DTOs.Relatorio.RelatorioPorPessoa;
namespace FinanceApp.Repository.Interface
{
    public interface IRelatorioRepository
    {
        Task<IEnumerable<TotalPessoaDTO>> TotaisPorPessoa();
        Task<IEnumerable<TotalCategoriaDTO>> TotaisPorCategoria();
    }
}