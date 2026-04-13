using FinanceApp.Application.DTOs;
using FinanceApp.Application.DTOs.Transacao;
using FinanceApp.Domain.Entidades;
namespace FinanceApp.Application.Repository
{
    public interface ITransacaoRepository
    {
        Task<RetornoDTO<int>> Add(TransacaoEntity transacao);
        Task<List<TransacaoDTO>> ListarTransacoes();
    }
}