using FinanceApp.DTOs.Transacao;
namespace FinanceApp.Repository.Interface
{
    public interface ITransacaoRepository
    {
        Task<int> Add(TransacaoDTO transacao);
        Task<List<TransacaoDTO>> ListarTransacoes();
    }
}