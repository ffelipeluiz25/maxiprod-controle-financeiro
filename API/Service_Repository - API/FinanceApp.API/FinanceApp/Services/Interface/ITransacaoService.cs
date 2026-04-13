using FinanceApp.DTOs;
using FinanceApp.DTOs.Transacao;
using FinanceApp.DTOs.Transacao.Request;
namespace FinanceApp.Services.Interface
{
    public interface ITransacaoService
    {
        Task<RetornoDTO<int>> Criar(CriarTransacaoDTO request);
        Task<RetornoDTO<List<TransacaoDTO>>> ListarTransacoes();
    }
}