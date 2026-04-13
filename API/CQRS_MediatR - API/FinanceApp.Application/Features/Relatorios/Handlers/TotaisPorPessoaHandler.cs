using FinanceApp.Application.DTOs;
using FinanceApp.Application.DTOs.Relatorio;
using FinanceApp.Application.DTOs.Relatorio.RelatorioPorPessoa;
using FinanceApp.Application.Features.Relatorios.Queries;
using FinanceApp.Application.Repository;
using MediatR;
namespace FinanceApp.Application.Features.Relatorios.Handlers
{

    public class TotaisPorPessoaHandler : IRequestHandler<TotaisPorPessoaQuery, RetornoDTO<RelatorioTotaisPorPessoaDTO>>
    {
        private readonly IRelatorioRepository _relatorioRepository;

        public TotaisPorPessoaHandler(IRelatorioRepository relatorioRepository)
        {
            _relatorioRepository = relatorioRepository;
        }

        public async Task<RetornoDTO<RelatorioTotaisPorPessoaDTO>> Handle(TotaisPorPessoaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var lista = (await _relatorioRepository.TotaisPorPessoa()).ToList();
                var totalGeral = new TotalGeralDTO();
                totalGeral.Receita = lista.Sum(x => x.TotalReceitas);
                totalGeral.Despesa = lista.Sum(x => x.TotalDespesas);
                totalGeral.Saldo = lista.Sum(x => x.Saldo);
                var relatorio = new RelatorioTotaisPorPessoaDTO(lista, totalGeral);
                return new RetornoDTO<RelatorioTotaisPorPessoaDTO>(true, relatorio);
            }
            catch (Exception ex)
            {
                return new RetornoDTO<RelatorioTotaisPorPessoaDTO>(false, ex.Message);
            }
        }

    }
}