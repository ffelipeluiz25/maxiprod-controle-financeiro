using AutoMapper;
using FinanceApp.DTOs;
using FinanceApp.DTOs.Relatorio.RelatorioPorCategoria;
using FinanceApp.DTOs.Relatorio.RelatorioPorPessoa;
using FinanceApp.Repository.Interface;
using FinanceApp.Services.Interface;
namespace FinanceApp.Services
{
    public class RelatorioService : IRelatorioService
    {

        private readonly IMapper _mapper;
        private readonly IRelatorioRepository _relatorioRepository;

        public RelatorioService(IMapper mapper,
                                IRelatorioRepository relatorioRepository)
        {
            _mapper = mapper;
            _relatorioRepository = relatorioRepository;
        }

        public async Task<RetornoDTO<RelatorioTotaisPorCategoriaDTO>> TotaisPorCategoria()
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

        public async Task<RetornoDTO<RelatorioTotaisPorPessoaDTO>> TotaisPorPessoa()
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