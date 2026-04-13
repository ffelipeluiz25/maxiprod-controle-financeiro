using FinanceApp.Application.DTOs;
using FinanceApp.Application.DTOs.Relatorio;
using FinanceApp.Application.DTOs.Relatorio.RelatorioPorCategoria;
using FinanceApp.Application.Features.Relatorios.Queries;
using FinanceApp.Application.Repository;
using MediatR;
namespace FinanceApp.Application.Features.Relatorios.Handlers
{
    public class TotaisPorCategoriaHandler : IRequestHandler<TotaisPorCategoriaQuery, RetornoDTO<RelatorioTotaisPorCategoriaDTO>>
    {
        private readonly IRelatorioRepository _relatorioRepository;

        public TotaisPorCategoriaHandler(IRelatorioRepository relatorioRepository)
        {
            _relatorioRepository = relatorioRepository;
        }

        public async Task<RetornoDTO<RelatorioTotaisPorCategoriaDTO>> Handle(TotaisPorCategoriaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var lista = (await _relatorioRepository.TotaisPorCategoria()).ToList();
                var totalGeral = new TotalGeralDTO();
                totalGeral.Receita = lista.Sum(x => x.TotalReceitas);
                totalGeral.Despesa = lista.Sum(x => x.TotalDespesas);
                totalGeral.Saldo = lista.Sum(x => x.Saldo);
                var relatorio = new RelatorioTotaisPorCategoriaDTO(lista, totalGeral);
                return new RetornoDTO<RelatorioTotaisPorCategoriaDTO>(true, relatorio);
            }
            catch (Exception ex)
            {
                return new RetornoDTO<RelatorioTotaisPorCategoriaDTO>(false, ex.Message);
            }
        }

    }
}