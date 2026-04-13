using FinanceApp.DTOs;
using FinanceApp.DTOs.Relatorio.RelatorioPorCategoria;
using FinanceApp.DTOs.Relatorio.RelatorioPorPessoa;
namespace FinanceApp.Services.Interface
{
    public interface IRelatorioService
    {
        Task<RetornoDTO<RelatorioTotaisPorCategoriaDTO>> TotaisPorCategoria();
        Task<RetornoDTO<RelatorioTotaisPorPessoaDTO>> TotaisPorPessoa();
    }
}